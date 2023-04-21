using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Utils
{
    public class SendMailUtil
    {
        public JsonResultUtil SendMail(SendMail mailsend)
        {            
            JsonResultUtil objresult = new JsonResultUtil();            
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["apikey"] = mailsend.ApiKeySendMail;
                data["from"] = mailsend.DisplayNameMailFrom + " <" + mailsend.MailFrom + ">";
                var strmailsend = "";
                if(mailsend.ToMail != null && mailsend.ToMail.Count != 0)
                {
                    foreach (var it in mailsend.ToMail)
                    {
                        if (strmailsend == "")
                        {
                            strmailsend = it;
                        }
                        else
                        {
                            strmailsend += ", " + it;
                        }
                    }
                }
                data["to"] = strmailsend;
                data["subject"] = mailsend.Subject;
                var strmailcc = "";
                if (mailsend.CcMail != null && mailsend.CcMail.Count != 0)
                {
                    foreach (var it in mailsend.CcMail)
                    {
                        if (strmailcc == "")
                        {
                            strmailcc = it;
                        }
                        else
                        {
                            strmailcc += ", " + it;
                        }
                    }
                }
                data["cc"] = strmailcc;
                var strmailbcc = "";
                if (mailsend.BccMail != null && mailsend.BccMail.Count != 0)
                {
                    foreach (var it in mailsend.BccMail)
                    {
                        if (strmailbcc == "")
                        {
                            strmailbcc = it;
                        }
                        else
                        {
                            strmailbcc += ", " + it;
                        }
                    }
                }
                data["bcc"] = strmailbcc;
                //data["text"] = "";
                data["html"] = mailsend.HtmlBody;
                data["solutionCode"] = mailsend.SolutionCode;
                data["orgId"] = mailsend.OrgId;
                //data["save"] = "";

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var response = wb.UploadValues(mailsend.ApiSendMail, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
                var result = responseInString;
                dynamic objDynamicSendMail = System.Web.Helpers.Json.Decode(result);
                var sendMailSuccess = CUtils.StrValueNew(objDynamicSendMail.Success).ToLower();
                if (sendMailSuccess.Equals("true"))
                {
                    objresult.Success = true;
                }
                else
                {
                    objresult.Success = false;
                    var errorMessage = CUtils.StrValueNew(objDynamicSendMail.ErrorMessage);
                    var errorDetail = CUtils.StrValueNew(objDynamicSendMail.ErrorDetail);
                    objresult.ErrDetail = errorDetail;
                    objresult.ErrMessage = errorMessage;
                }
            }
            return objresult;
        }

        public static JsonResultUtil SendMailGun(SendMailGun sendMailGun)
        {
            JsonResultUtil objResult = new JsonResultUtil()
            {
                Success = false,
                ErrMessage = "",
                ErrDetail = ""
            };
            ServicePointManager.ServerCertificateValidationCallback = new
                RemoteCertificateValidationCallback
                (
                    delegate { return true; }
                );

            RestClient client = new RestClient
            {
                BaseUrl = CUtils.StrValue(sendMailGun.MailgateApi)
            };
            //client.BaseUrl = "http://mailgate.inos.vn/emailapi/Send";

            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.AddParameter("apiKey", sendMailGun.MailgateApiKey);
            if (!CUtils.IsNullOrEmpty(sendMailGun.FromMail))
            {
                request.AddParameter("from", string.Format("{0} <{1}>", CUtils.StrValue(sendMailGun.DisplayNameMailFrom), CUtils.StrValue(sendMailGun.FromMail)));
            }
            else
            {
                request.AddParameter("from", string.Format("{0} <{1}@{2}>", CUtils.StrValue(sendMailGun.DisplayNameMailFrom), CUtils.StrValue(sendMailGun.From), CUtils.StrValue(sendMailGun.MailgateDomain)));
            }

            var strToMail = "";
            if (!CUtils.IsNullOrEmpty(sendMailGun.ToMail))
            {
                foreach (var it in sendMailGun.ToMail)
                {
                    if (CUtils.IsNullOrEmpty(strToMail))
                    {
                        strToMail = it;
                    }
                    else
                    {
                        strToMail += ", " + it;
                    }
                }
            }
            request.AddParameter("to", strToMail);
            var strCcMail = "";
            if (!CUtils.IsNullOrEmpty(sendMailGun.CcMail))
            {
                foreach (var it in sendMailGun.CcMail)
                {
                    if (CUtils.IsNullOrEmpty(strCcMail))
                    {
                        strCcMail = it;
                    }
                    else
                    {
                        strCcMail += ", " + it;
                    }
                }
            }
            request.AddParameter("cc", strCcMail);
            var strBccMail = "";
            if (!CUtils.IsNullOrEmpty(sendMailGun.BccMail))
            {
                foreach (var it in sendMailGun.BccMail)
                {
                    if (CUtils.IsNullOrEmpty(strBccMail))
                    {
                        strBccMail = it;
                    }
                    else
                    {
                        strBccMail += ", " + it;
                    }
                }
            }
            request.AddParameter("bcc", strBccMail);

            request.AddParameter("subject", CUtils.StrValue(sendMailGun.Subject));
            request.AddParameter("html", CUtils.StrValue(sendMailGun.HtmlBody));
            request.AddParameter("solutionCode", CUtils.StrValue(sendMailGun.SolutionCode));
            request.AddParameter("orgId", CUtils.StrValue(sendMailGun.OrgId));

            if (sendMailGun.AttachmentFiles != null && sendMailGun.AttachmentFiles.Count > 0)
            {
                foreach (var fpath in sendMailGun.AttachmentFiles)
                {
                    request.AddFile("attachment", fpath); // fpath: đường dẫn vật lý của file trên server
                }
            }

            var ret = client.Execute(request);

            if (ret.ErrorException != null)
            {
                var objException = ret.ErrorException as Exception;
                objResult.ErrMessage = CUtils.StrValue(objException.Message);
                objResult.ErrDetail = CUtils.StrValue(objException.StackTrace);
            }
            else
            {
                objResult.Success = true;
            }

            return objResult;

        }
    }
}
