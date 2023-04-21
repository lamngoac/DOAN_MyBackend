using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Filters;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.State;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.Utils;
using System.Net;
using idn.Skycic.Inventory.Common.ModelsUI;
//using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.Web.Controllers
{
    [CustomErrorHandler]
    public class idnBaseController : Controller
    {
        #region["Variables"]
        public string OS_ProductCentrer_GwUserCode = "";
        public string OS_ProductCentrer_GwPassword = "";
        public string OS_ProductCentrer_WAUserCode = "";
        public string OS_ProductCentrer_WAUserPassword = "";

        public string OS_MasterServer_PrdCenter_API_Url = "";
        //public string OS_MasterServer_PrdCenter_UserCode = "";
        //public string OS_MasterServer_PrdCenter_UserPassword = "";
        public string OS_MasterServer_PrdCenter_TokenID = "";
        public string OS_MasterServer_PrdCenter_GwUserCode = "";
        public string OS_MasterServer_PrdCenter_GwPassword = "";
        public string OS_MasterServer_PrdCenter_WAUserCode = "";
        public string OS_MasterServer_PrdCenter_WAUserPassword = "";

        public string GwUserCode = "";
        public string GwPassword = "";
        public string WAUserCode_RptSV = "";
        public string WAUserPassword_RptSV = "";
        public string WAUserCode_MstSV = "";
        public string WAUserPassword_MstSV = "";
        public string WAUserCode_Network = "";
        public string WAUserPassword_Network = "";
        public string GwUserCode_PayGate = "";
        public string GwPassword_PayGate = "";
        public string WAUserCode_PayGate = "";
        public string WAPassword_PayGate = "";

        public string FuncType = "";
        public string Ft_RecordStart = "0";
        public string Ft_RecordStartExportExcel = "0";
        public string Ft_RecordCount = "123456000";
        public string Ft_WhereClause = "";

        public string SolutionCode = "";
        public string SolutionName = "";

        public double Offset = 7;
        public string NetworkID = "";
        public string TokenIDFix = "";
        public string Hethong = "";
        public string DiskCode = "";
        public string FolderInBrandCloud = "";
        public string RootFolder = "";
        //public static string SubPath = "";
        public int PageSizeConfig = 10;
        public int RowsWorksheets = 1048570; // If you are working on Excel 2007 or any of the latest versions - there are 1048576 rows and 16384 columns. 
        public string Uploads = "Uploads";
        public string TempFiles = "TempFiles";
        public double FileUploadSize = 26214400; // 25MB
        public double FileImageSize = 26214400; // 25MB
        public string FlagActive = "1";
        public string FlagInActive = "0";
        public string ReCaptchaPrivateKey = "";

        public string APIsSendMail = "";
        public string ApiKeySendMail = "";
        public string DisplayNameMailFrom = "";
        public string MailFrom = "";

        public string FolderLog = "";
        public string StrLogs = "";
        private int inext = 0;
        public string ReportBro_Key = "";
        public string ReportBro_OutputFormat = "";
        public string ReportBro_ServerUrl = "";
        #endregion

        #region["Get DateTime Server Client"]
        public string DateTimeNow()
        {
            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return datetime;
        }
        public string Now
        {
            get { return DateTimeNow(); }
        }
        public string Today
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.ToString(Nonsense.DATE_TIME_FORMAT);
                }
                else
                {
                    return null;
                }
            }
        }
        public string Year
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.Year.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public string MonthOfYear
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.ToString(Nonsense.MONTH_YEAR_FORMAT);
                }
                else
                {
                    return null;
                }
            }
        }
        public string DateToSearch()
        {
            var date = "";
            date = CUtils.GetDateToSearch(DateTimeNow());
            return date;
        }
        #endregion

        #region["Constructor"]
        public idnBaseController()
        {
            #region["ProductCentrer"]
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_GwUserCode))
            {
                OS_ProductCentrer_GwUserCode = ConfigurationManager.AppSettings["OS_ProductCentrer_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_GwPassword))
            {
                OS_ProductCentrer_GwPassword = ConfigurationManager.AppSettings["OS_ProductCentrer_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_WAUserCode))
            {
                OS_ProductCentrer_WAUserCode = ConfigurationManager.AppSettings["OS_ProductCentrer_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_WAUserPassword))
            {
                OS_ProductCentrer_WAUserPassword = ConfigurationManager.AppSettings["OS_ProductCentrer_WAUserPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_API_Url))
            {
                OS_MasterServer_PrdCenter_API_Url = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_API_Url"];
            }
            #endregion

            #region["MasterServer ProductCentrer"]
            //if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_UserCode))
            //{
            //    OS_MasterServer_PrdCenter_UserCode = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_UserCode"];
            //}
            //if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_UserPassword))
            //{
            //    OS_MasterServer_PrdCenter_UserPassword = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_UserPassword"];
            //}
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_TokenID))
            {
                OS_MasterServer_PrdCenter_TokenID = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_TokenID"];
            }
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_GwUserCode))
            {
                OS_MasterServer_PrdCenter_GwUserCode = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_GwPassword))
            {
                OS_MasterServer_PrdCenter_GwPassword = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_WAUserCode))
            {
                OS_MasterServer_PrdCenter_WAUserCode = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_WAUserPassword))
            {
                OS_MasterServer_PrdCenter_WAUserPassword = ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_WAUserPassword"];
            }
            #endregion

            #region["GwUserCode - GwPassword"]
            if (CUtils.IsNullOrEmpty(GwUserCode))
            {
                GwUserCode = ConfigurationManager.AppSettings["GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(GwPassword))
            {
                GwPassword = ConfigurationManager.AppSettings["GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_Network))
            {
                WAUserCode_Network = ConfigurationManager.AppSettings["WAUserCode_Network"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_Network))
            {
                WAUserPassword_Network = ConfigurationManager.AppSettings["WAUserPassword_Network"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_RptSV))
            {
                WAUserCode_RptSV = ConfigurationManager.AppSettings["WAUserCode_RptSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_RptSV))
            {
                WAUserPassword_RptSV = ConfigurationManager.AppSettings["WAUserPassword_RptSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_MstSV))
            {
                WAUserCode_MstSV = ConfigurationManager.AppSettings["WAUserCode_MstSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_MstSV))
            {
                WAUserPassword_MstSV = ConfigurationManager.AppSettings["WAUserPassword_MstSV"];
            }
            if (CUtils.IsNullOrEmpty(GwUserCode_PayGate))
            {
                GwUserCode_PayGate = ConfigurationManager.AppSettings["GwUserCode_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(GwPassword_PayGate))
            {
                GwPassword_PayGate = ConfigurationManager.AppSettings["GwPassword_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_PayGate))
            {
                WAUserCode_PayGate = ConfigurationManager.AppSettings["WAUserCode_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(WAPassword_PayGate))
            {
                WAPassword_PayGate = ConfigurationManager.AppSettings["WAPassword_PayGate"];
            }
            #endregion

            #region["APIs"]
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
            {
                ServiceAddress.BaseReportServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
            {
                ServiceAddress.BaseMasterServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BasePayGateAddress))
            {
                ServiceAddress.BasePayGateAddress = System.Configuration.ConfigurationManager.AppSettings["BasePayGateAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseINOSAddress))
            {
                ServiceAddress.BaseINOSAddress = System.Configuration.ConfigurationManager.AppSettings["BaseINOSAddress"];
                inos.common.Paths.SetInosServerBaseAddress(ServiceAddress.BaseINOSAddress);
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseAPIInbrand))
            {
                ServiceAddress.BaseAPIInbrand = System.Configuration.ConfigurationManager.AppSettings["BaseAPIInbrand"];
            }
            //var url1 = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddress"];
            //var url2 = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddressTest"];

            //Uri myUri1 = new Uri(url1);
            //string host1 = myUri1.Host;
            //Uri myUri2 = new Uri(url2);
            //string host2 = myUri2.Host;
            //var check1 = CUtils.IsIPAddress(host1);
            //var check2 = CUtils.IsIPAddress(host2);
            #endregion

            #region["InBrand Cloud Config"]
            var rowsWorksheets = ConfigurationManager.AppSettings["RowsWorksheets"];
            if (!CUtils.IsNullOrEmpty(rowsWorksheets))
            {
                RowsWorksheets = Convert.ToInt32(rowsWorksheets);
            }
            if (CUtils.IsNullOrEmpty(NetworkID))
            {
                NetworkID = ConfigurationManager.AppSettings["NetworkID"];
            }
            if (CUtils.IsNullOrEmpty(TokenIDFix))
            {
                TokenIDFix = ConfigurationManager.AppSettings["TokenID"];
            }
            #endregion

            #region["SendMail Config"]
            if (CUtils.IsNullOrEmpty(APIsSendMail))
            {
                APIsSendMail = ConfigurationManager.AppSettings["APIsSendMail"];
            }
            if (CUtils.IsNullOrEmpty(ApiKeySendMail))
            {
                ApiKeySendMail = ConfigurationManager.AppSettings["ApiKeySendMail"];
            }
            if (CUtils.IsNullOrEmpty(DisplayNameMailFrom))
            {
                DisplayNameMailFrom = ConfigurationManager.AppSettings["DisplayNameMailFrom"];
            }
            if (CUtils.IsNullOrEmpty(MailFrom))
            {
                MailFrom = ConfigurationManager.AppSettings["MailFrom"];
            }
            if (CUtils.IsNullOrEmpty(SolutionCode))
            {
                SolutionCode = ConfigurationManager.AppSettings["SolutionCode"];
            }
            #endregion

            #region["ReCaptchaPrivateKey"]
            if (CUtils.IsNullOrEmpty(ReCaptchaPrivateKey))
            {
                ReCaptchaPrivateKey = ConfigurationManager.AppSettings["ReCaptchaPrivateKey"];
            }
            #endregion

            #region["ReportBro"]
            if (CUtils.IsNullOrEmpty(ReportBro_Key))
            {
                ReportBro_Key = ConfigurationManager.AppSettings["ReportBro_Key"];
            }
            if (CUtils.IsNullOrEmpty(ReportBro_OutputFormat))
            {
                ReportBro_OutputFormat = ConfigurationManager.AppSettings["ReportBro_OutputFormat"];
            } 
            if (CUtils.IsNullOrEmpty(ReportBro_ServerUrl))
            {
                ReportBro_ServerUrl = ConfigurationManager.AppSettings["ReportBro_ServerUrl"];
            }
            #endregion

        }
        #endregion

        #region["RenderRazorViewToString"]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public string RenderRazorViewToString(string viewName, string masterName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, masterName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion

        #region["JsonView"]
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }
        public ActionResult JsonView(object model = null)
        {
            return JsonView("", model);
        }
        public ActionResult JsonView(string viewname, object model)
        {
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];

            try
            {
                var html = RenderRazorViewToString(viewname, model);
                var data = new
                {
                    Success = true,
                    Html = html
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = e.StackTrace
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult JsonView(string viewname, string masterName, object model)
        {
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];

            try
            {
                var html = RenderRazorViewToString(viewname, masterName, model);
                var data = new
                {
                    Success = true,
                    Html = html
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = e.StackTrace
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult JsonViewError(string viewname, object model, JsonResultEntry resultEntry)
        {
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];
            string html = "";

            try
            {
                html = RenderRazorViewToString(viewname, model);
                if (resultEntry.Success)
                {
                    var data = new
                    {
                        Success = true,
                        Html = html,
                        Title = resultEntry.Messages
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Html = html,
                        Title = resultEntry.Messages,
                        Detail = resultEntry.Detail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = resultEntry.Detail
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region["Commons"]
        public string GetNextTId()
        {
            var strDateTimeNow = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff").Trim();
            var strNextTId = string.Format("{0}.{1}", strDateTimeNow, inext++);
            return strNextTId;
        }
        public string GetNextDateTId()
        {
            var strNextTId = string.Format("{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            return strNextTId;
        }
        public string GetRandomId(string pre, int length = 8)
        {
            string ran = Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
            return string.Format("{0}{1}", pre, ran);
        }
        protected string RedirectActionToLogin()
        {
            FormsAuthentication.SignOut();
            var requestRawUrl = "/";
            var action = this.RouteData.Values["action"].ToString();
            if (!action.Equals("Login", StringComparison.InvariantCultureIgnoreCase))
            {
                requestRawUrl = Request.RawUrl;
            }
            var redirectUrl = Url.ActionLocalized("Login", "Account", new { redirectUrl = requestRawUrl });
            return redirectUrl;
        }
        #endregion

        #region["Logs"]
        public string pathFile = "";
        public string rootFolder = "";
        public string fileLogName = "";
        public string PathFileiCount = "";
        public string FilePath_Save_Root_ESign = @"C:\\Logs\Qinvoice\ESign\Save_Root_ESign.txt";

        public int ReturnCount()
        {
            var iCount = 0;
            if (!CUtils.IsNullOrEmpty(PathFileiCount))
            {
                StreamReader streamReader = new StreamReader(PathFileiCount, System.Text.Encoding.UTF8);
                var strCount = streamReader.ReadLine();
                if (CUtils.IsNumeric(strCount))
                {
                    iCount = Convert.ToInt32(strCount);
                }
                streamReader.Close();

                System.IO.File.WriteAllText(PathFileiCount, (iCount + 1).ToString(), Encoding.UTF8);
            }

            return iCount;
        }

        public void CreateFileWriteLog(string view, string date, int icount)
        {
            var stringBuilder = new StringBuilder();
            var folder = "";
            var strLogs = "";

            var dateCur = DateTimeNow();
            folder = date;
            var datetimenow = dateCur;
            CUtils.StrAppend(stringBuilder, "-------------------------------");
            CUtils.StrAppend(stringBuilder, datetimenow);
            CUtils.StrAppend(stringBuilder, "Màn hình: " + view);
            CUtils.StrAppend(stringBuilder, "");


            #region["Tạo file ghi Log"]
            fileLogName = icount + "_" + view + ".txt";
            pathFile = CUtils.PathFile(rootFolder, folder, fileLogName);
            System.Web.HttpContext.Current.Session["PathFileLog"] = pathFile;
            strLogs = stringBuilder.ToString();
            System.IO.File.AppendAllText(pathFile, strLogs, Encoding.UTF8);
            stringBuilder.Clear();
            #endregion
        }

        public void CreateFileWriteLogByFilePath(string filepath)
        {
            var stringBuilder = new StringBuilder();
            var folder = "";
            var strLogs = "";

            var dateCur = DateTimeNow();
            var datetimenow = dateCur;
            CUtils.StrAppend(stringBuilder, "-------------------------------");
            CUtils.StrAppend(stringBuilder, datetimenow);


            #region["Tạo file ghi Log"]
            var path = @"C:\\Logs\Qinvoice\ESign";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CUtils.GrantAccess(path);
            if (!System.IO.File.Exists(filepath))
            {
                System.IO.File.Create(filepath).Dispose();
            }

            strLogs = stringBuilder.ToString();
            System.IO.File.AppendAllText(filepath, strLogs, Encoding.UTF8);
            stringBuilder.Clear();
            #endregion
        }
        #endregion

        #region ["Put - Post Report Server"]
        public string PutReport(dynamic data)
        {
            string url = ReportBro_ServerUrl + "rp/preview";
            string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "PUT";
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "application/json";
            request.Accept = "Accept=application/json";
            request.SendChunked = true;
            request.ContentLength = serializedObject.Length;
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(serializedObject);
            }
            var response = request.GetResponse() as HttpWebResponse;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response
                return responseText;
            }
            return null;
        }

        public string PostReport(dynamic data)
        {
            string url = ReportBro_ServerUrl + "rp/preview";
            string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Object myObject = new
            {
                json = serializedObject
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(myObject);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "Post";
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "application/json";
            request.Accept = "Accept=application/json";
            request.SendChunked = true;
            request.ContentLength = serializedObject.Length;
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }
            var response = request.GetResponse() as HttpWebResponse;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response
                return responseText;
            }
            return null;
        }

        public string LinkFilePdf(string serverurl, string key, string outputFormat)
        {
            var linkPdf = String.Format("{0}{1}/{2}?key={3}&outputFormat={4}", serverurl, "rp", "preview", key, outputFormat);
            return linkPdf;
        }
        #endregion

        #region[""]

        public dynamic CreateDataObjectReportServer(List<Mst_Part> listMst_Part, ref string watermark)
        {
            var offset = Offset;
            var MyDynamic = new ObjectReportServer()
            {
                DataTable = new List<Mst_Part_ReportServer>()
            };

            if (listMst_Part != null && listMst_Part.Count > 0)
            {
                var listMst_Part_ReportServer = new List<Mst_Part_ReportServer>();
                foreach (var item in listMst_Part)
                {
                    var objMst_Part_ReportServer = new Mst_Part_ReportServer();
                    objMst_Part_ReportServer.PartCode = CUtils.StrValue(item.PartCode);
                    objMst_Part_ReportServer.PartBarCode = CUtils.StrValue(item.PartBarCode);
                    objMst_Part_ReportServer.PartName = CUtils.StrValue(item.PartName);
                    objMst_Part_ReportServer.PartNameFS = CUtils.StrValue(item.PartNameFS);
                    objMst_Part_ReportServer.PartDesc = CUtils.StrValue(item.PartDesc);
                    objMst_Part_ReportServer.PartType = CUtils.StrValue(item.PartType);
                    objMst_Part_ReportServer.PMType = CUtils.StrValue(item.PMType);
                    objMst_Part_ReportServer.PartUnitCodeStd = CUtils.StrValue(item.PartUnitCodeStd);
                    objMst_Part_ReportServer.PartUnitCodeDefault = CUtils.StrValue(item.PartUnitCodeDefault);
                    objMst_Part_ReportServer.UPIn = CUtils.StrValue(item.UPIn);
                    objMst_Part_ReportServer.UPOut = CUtils.StrValue(item.UPOut);
                    objMst_Part_ReportServer.QtyEffMonth = CUtils.StrValue(item.QtyEffMonth);
                    objMst_Part_ReportServer.PartOrigin = CUtils.StrValue(item.PartOrigin);
                    objMst_Part_ReportServer.PartComponents = CUtils.StrValue(item.PartComponents);
                    objMst_Part_ReportServer.InstructionForUse = CUtils.StrValue(item.InstructionForUse);
                    objMst_Part_ReportServer.PartStorage = CUtils.StrValue(item.PartStorage);
                    objMst_Part_ReportServer.UrlMnfSequence = CUtils.StrValue(item.UrlMnfSequence);
                    objMst_Part_ReportServer.MnfStandard = CUtils.StrValue(item.MnfStandard);
                    objMst_Part_ReportServer.PartStyle = CUtils.StrValue(item.PartStyle);
                    objMst_Part_ReportServer.PartIntroduction = CUtils.StrValue(item.PartIntroduction);
                    objMst_Part_ReportServer.LogLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT_TVAN_VN, offset);
                    objMst_Part_ReportServer.LogLUBy = CUtils.StrValue(item.LogLUBy);

                    listMst_Part_ReportServer.Add(objMst_Part_ReportServer);
                }

                MyDynamic.DataTable.AddRange(listMst_Part_ReportServer);
            }
            return MyDynamic;
        }
        #endregion
    }

    [Authorize]
    public class AdminBaseController : idnBaseController
    {
        #region["DateTimeUTC ServerBiz"]
        public string DateTimeServerBizUTC()
        {
            var datetimeUTC = "";
            var objRQ_Common = new RQ_Common()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = null,
                Ft_Cols_Upd = null,
                UtcOffset = UserState.UtcOffset,
            };
            var objRT_Common = CommonService.Instance.WA_Common_GetDTime(objRQ_Common, ServiceAddress.BaseReportServerAPIAddress);
            if (objRT_Common != null && objRT_Common.c_K_DT_Sys != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var objc_K_DT_SysInfo = objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(objc_K_DT_SysInfo.Remark))
                {
                    datetimeUTC = objc_K_DT_SysInfo.Remark.Trim();
                }
                //var convertingLocalTimeToUTC = CUtils.ConvertingLocalTimeToUTC(datetimeUTC);
                //var convertingUTCToLocalTime = CUtils.ConvertingUTCToLocalTime(datetimeUTC);
            }
            return datetimeUTC;
        }
        #endregion

        public void CheckApi_Url(string hethong)
        {
            if (hethong.Equals("TEST"))
            {
                // APIs phải là địa chỉ IP
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
                {
                    var api_Uri_ReportServer = new Uri(ServiceAddress.BaseReportServerAPIAddress);
                    var hostReportServer = api_Uri_ReportServer.Host;
                    if (!CUtils.IsIPAddress(hostReportServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
                {
                    var api_Uri_MasterServer = new Uri(ServiceAddress.BaseMasterServerAPIAddress);
                    var hostMasterServer = api_Uri_MasterServer.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_API_Url))
                {
                    var api_Uri_MasterServer_PrdCenter = new Uri(OS_MasterServer_PrdCenter_API_Url);
                    var hostMasterServer_PrdCenter = api_Uri_MasterServer_PrdCenter.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer_PrdCenter))
                    {
                        RedirectActionToLogin();
                    }
                }
            }
            else if (hethong.Equals("REAL"))
            {
                // APIs phải có domain
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
                {
                    var api_Uri_ReportServer = new Uri(ServiceAddress.BaseReportServerAPIAddress);
                    var hostReportServer = api_Uri_ReportServer.Host;
                    if (!CUtils.IsIPAddress(hostReportServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
                {
                    var api_Uri_MasterServer = new Uri(ServiceAddress.BaseMasterServerAPIAddress);
                    var hostMasterServer = api_Uri_MasterServer.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(OS_MasterServer_PrdCenter_API_Url))
                {
                    var api_Uri_MasterServer_PrdCenter = new Uri(OS_MasterServer_PrdCenter_API_Url);
                    var hostMasterServer_PrdCenter = api_Uri_MasterServer_PrdCenter.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer_PrdCenter))
                    {
                        RedirectActionToLogin();
                    }
                }
            }
            else
            {
                RedirectActionToLogin();
            }
        }
        public AdminBaseController()
        {
            #region["AdminBaseController"]
            if (CUtils.IsNullOrEmpty(Hethong))
            {
                Hethong = ConfigurationManager.AppSettings["HeThong"];
            }

            CheckApi_Url(Hethong);

            if (CUtils.IsNullOrEmpty(RootFolder))
            {
                DiskCode = CUtils.StrValue(ConfigurationManager.AppSettings["DiskCode"]);
                FolderInBrandCloud = CUtils.StrValue(ConfigurationManager.AppSettings["FolderInBrandCloud"]);
                RootFolder = DiskCode + @":\" + FolderInBrandCloud;
            }
            
            #endregion
        }
        #region ["State management"]
        private SessionWrapper _session;
        public SessionWrapper SessionWrapper
        {
            get
            {
                if (_session == null)
                {
                    var sessionCur = ControllerContext.HttpContext.Session;
                    _session = new SessionWrapper(ControllerContext.HttpContext.Session);
                }
                return _session;
            }
            set
            {
                _session = value;
            }
        }
        public UserState UserState
        {
            get
            {
                return SessionWrapper.UserState;
            }

            set
            {
                SessionWrapper.UserState = value;
            }
        }
        #endregion
        #region ["Init"]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //int sessionTimeout = System.Web.HttpContext.Current.Session.Timeout;

            //var networkid = (string)RouteData.Values["networkid"];
            //var routeData = RouteData.Values;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Init();
        }
        protected virtual void Init()
        {
            
            #region["Init"]

            if (Request.Cookies["tokenid"] != null)
            {
                var ckTokenId = new HttpCookie("tokenid") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(ckTokenId);
            }

            #region["Fonts"]
            if (CUtils.IsNullOrEmpty(FontPath.FontTimeNewRomanPath))
            {
                var pathFolderFonts = Server.MapPath(@"~\fonts\");
                FontPath.FontTimeNewRomanPath = Path.Combine(pathFolderFonts, "times_0.ttf");
            }
            if (CUtils.IsNullOrEmpty(FontPath.FontArialPath))
            {
                var pathFolderFonts = Server.MapPath(@"~\fonts\");
                FontPath.FontArialPath = Path.Combine(pathFolderFonts, "arial.ttf");
            }
            if (CUtils.IsNullOrEmpty(FontPath.FontChakraPetchBold))
            {
                var pathFolderFonts = Server.MapPath(@"~\fonts\");
                FontPath.FontChakraPetchBold = Path.Combine(pathFolderFonts, "ChakraPetch-Light.ttf");
            }
            #endregion

            if (UserState == null || UserState.SysUser == null)
            {
                var requestRawUrl = "/";
                var action = this.RouteData.Values["action"].ToString();
                var checkAction = action.ToLower().Trim().Equals("Login".ToLower().Trim(), StringComparison.InvariantCultureIgnoreCase);
                if (!checkAction)
                {
                    var checkActionOS = action.ToLower().Trim().Equals("LoginOS".ToLower().Trim(), StringComparison.InvariantCultureIgnoreCase);
                    if (!checkActionOS)
                    {
                        var usercode = "";
                        var password = "";
                        if (Request.Cookies["_ibcckuc"] != null)
                        {
                            HttpCookie ck_usercode = Request.Cookies["_ibcckuc"];
                            if (ck_usercode != null)
                            {
                                var ckusercode = ck_usercode.Value;
                                if (!CUtils.IsNullOrEmpty(ckusercode))
                                {
                                    // giải mã
                                    var usercodeDecrypt = CUtils.Decrypt(ckusercode, true);
                                    // đảo ngược chuỗi
                                    var usercodeReverse = CUtils.ReverseString(usercodeDecrypt);
                                    usercode = usercodeReverse;
                                    //usercode = ckusercode;
                                }
                            }
                        }
                        if (Request.Cookies["_ibcckup"] != null)
                        {
                            HttpCookie ck_password = Request.Cookies["_ibcckup"];
                            if (ck_password != null)
                            {
                                var ckpassword = ck_password.Value;
                                if (!CUtils.IsNullOrEmpty(ckpassword))
                                {
                                    // giải mã 
                                    var passwordDecrypt = CUtils.Decrypt(ckpassword, true);
                                    // đảo ngược chuỗi
                                    var passwordReverse = CUtils.ReverseString(passwordDecrypt);
                                    // cắt chuỗi
                                    var passwordSubstring = passwordReverse.Substring(23);
                                    password = passwordSubstring;
                                    //password = ckpassword;
                                }
                            }
                        }

                        if (!CUtils.IsNullOrEmpty(usercode) && !CUtils.IsNullOrEmpty(password))
                        {
                            #region["Login"]
                            System.Web.HttpContext.Current.Session["UserCodeLogin"] = usercode;
                            System.Web.HttpContext.Current.Session["PasswordLogin"] = password;
                            var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                AccessToken = TokenIDFix,
                                NetworkID = NetworkID,
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = usercode,
                                WAUserPassword = password,
                                // RQ_MstSv_Sys_User
                                Rt_Cols_MstSv_Sys_User = null,
                                MstSv_Sys_User = null,
                            };
                            var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_GetAccessToken(objRQ_MstSv_Sys_User, ServiceAddress.BaseMasterServerAPIAddress);
                            if (objRT_MstSv_Sys_User != null)
                            {
                                if (objRT_MstSv_Sys_User.c_K_DT_Sys != null &&
                                    objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                                    objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                                {
                                    var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                                    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                                    {
                                        System.Web.HttpContext.Current.Session["TokenID"] = CUtils.StrValueOrNull(c_K_DT_SysInfo.Remark);

                                        HttpCookie tokenid = new HttpCookie("tokenid")
                                        {
                                            Value = CUtils.StrValueOrNull(c_K_DT_SysInfo.Remark),
                                            Expires = DateTime.Now.AddDays(1)
                                        };
                                        Response.Cookies.Add(tokenid);

                                    }
                                    FormsAuthentication.SetAuthCookie(usercode, false);

                                    var strDateTimeNow = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff").Trim();
                                    // đảo ngược chuỗi
                                    var usercodeCur = CUtils.ReverseString(usercode.Trim());
                                    // mã hóa
                                    var usercodeEncrypt = CUtils.Encrypt(usercodeCur, true);
                                    HttpCookie ck_usercode = new HttpCookie("_ibcckuc")
                                    {
                                        Value = usercodeEncrypt,
                                        Expires = DateTime.Now.AddDays(1),
                                        //HttpOnly = true,
                                        //Secure = true,
                                        ////Shareable = 
                                    };
                                    Response.Cookies.Add(ck_usercode);

                                    // đảo ngược chuỗi
                                    var passwordLogin = CUtils.ReverseString(strDateTimeNow + "_" + password);
                                    // mã hóa
                                    var passwordLoginEncrypt = CUtils.Encrypt(passwordLogin, true);
                                    HttpCookie ck_password = new HttpCookie("_ibcckup")
                                    {
                                        Value = passwordLoginEncrypt,
                                        Expires = DateTime.Now.AddDays(1),
                                        //HttpOnly = true,
                                        //Secure = true,
                                    };
                                    Response.Cookies.Add(ck_password);

                                    Response.Redirect(Url.ActionLocalized("index", "home"));
                                }
                            }
                            else
                            {
                                FormsAuthentication.SignOut();
                                requestRawUrl = Request.RawUrl;
                                var redirectUrl = Url.ActionLocalized("Login", "Account", new { redirectUrl = requestRawUrl });
                                Response.Redirect(redirectUrl);
                            }
                            #endregion
                        }
                        else
                        {
                            FormsAuthentication.SignOut();
                            requestRawUrl = Request.RawUrl;
                            var redirectUrl = Url.ActionLocalized("Login", "Account", new { redirectUrl = requestRawUrl });
                            Response.Redirect(redirectUrl);
                        }

                    }

                }
            }
            else
            {
                var userName = "";
                var organName = "";
                if (!CUtils.IsNullOrEmpty(UserState.UserName))
                {
                    userName = UserState.UserName.Trim();
                }
                if (UserState.SysUser != null && !CUtils.IsNullOrEmpty(UserState.SysUser.mo_OrganName))
                {
                    organName = UserState.SysUser.mo_OrganName.Trim();
                }
                ViewBag.UserName = userName;
                ViewBag.OrganName = organName;
                var abc = System.Web.HttpContext.Current.Session["HelloCaoTo"];
            }
            #endregion
            
        }
        #endregion
        #region["File - Folder"]
        public string CheckFolderExists(string path = "")
        {
            var strFolder = "";
            if (!string.IsNullOrEmpty(path))
            {
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            else
            {
                path = "/Uploads";
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            return strFolder;
        }
        public string CheckFolderCurrentExists(string path = "")
        {
            var strFolder = "";
            if (!string.IsNullOrEmpty(path))
            {
                var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            else
            {
                path = "/Uploads";
                var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            return strFolder;
            //return System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension));
        }
        #endregion
        #region["GenFilePath"]
        public string GenExcelExportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportFilePath(string ext, string prefix, ref string virualPath)
        {
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".xls";
            }
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}{2}", subpath, filename, ext);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {

                virualPath = string.Format("{0}/{1}_{2}{3}", subpath, filename, ++i, ext);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportReportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/Reports/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }
        #endregion
        #region["SysObject Setting"]
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        public SysObjectSetting GetSysObjectSetting()
        {
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data"), "SysObjectSetting.config");
            if (!System.IO.File.Exists(filePath)) return null;
            SysObjectSetting ret = null;
            locker.EnterReadLock();
            try
            {
                ret = Serialization.Deserialize<SysObjectSetting>(filePath);
            }
            finally
            {
                locker.ExitReadLock();
            }
            return ret;
        }
        public void SaveSysObjectSetting(SysObjectSetting item)
        {
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data"), "SysObjectSetting.config");
            locker.EnterWriteLock();
            try
            {
                Serialization.SerializeSettings<SysObjectSetting>(item, filePath);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
        public void UpdateSysObjectFunctions(string objectCode, List<string> functions)
        {
            if (string.IsNullOrEmpty(objectCode)) return;
            SysObjectSetting setting = GetSysObjectSetting();
            if (setting == null)
            {
                setting = new SysObjectSetting();
            }
            if (setting.List == null) setting.List = new List<SysObjectFunction>();
            int idx = -1;
            for (int i = 0; i < setting.List.Count; ++i)
            {
                var f = setting.List[i];
                if (f.ObjectCode.Equals(objectCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    idx = i;
                    break;
                }
            }
            if (idx >= 0)
            {
                setting.List[idx].FunctionCodes = functions;
            }
            else
            {
                SysObjectFunction o = new SysObjectFunction()
                {
                    ObjectCode = objectCode.ToUpper().Trim(),
                    FunctionCodes = functions,
                };
                setting.List.Add(o);
            }
            SaveSysObjectSetting(setting);
        }
        public List<string> ResolveObjects(List<string> objectCodes)
        {
            List<string> list = new List<string>();
            var listAccess = new List<Sys_Access>();
            SysObjectSetting setting = GetSysObjectSetting();

            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            if (setting != null && setting.List != null)
            {
                foreach (var code in objectCodes)
                {
                    if (!dic.ContainsKey(code))
                    {
                        dic.Add(code, true);
                        list.Add(code);
                    }


                    foreach (var scode in setting.List)
                    {
                        if (scode.ObjectCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                        {
                            foreach (var f in scode.FunctionCodes)
                            {
                                if (!string.IsNullOrEmpty(f))
                                {
                                    if (!dic.ContainsKey(f))
                                    {
                                        dic.Add(f, true);
                                        var arrCode = f.Split(',');
                                        if (arrCode != null && arrCode.Length > 0)
                                        {
                                            for (int i = 0; i < arrCode.Length; i++)
                                            {
                                                var strCode = arrCode[i];
                                                list.Add(strCode);
                                            }
                                        }
                                        //list.Add(f);
                                    }
                                }

                            }
                        }
                    }
                }
            }

            else list = objectCodes;

            return list;
        }
        #endregion
        #region["Common"]
        #region["Bộ phận/Phòng ban"]
        protected List<Mst_Department> WA_Mst_Department_Get(RQ_Mst_Department objRQ_Mst_Department)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Department = new List<Mst_Department>();

            var objRT_Mst_Department = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
            if (objRT_Mst_Department.Lst_Mst_Department != null && objRT_Mst_Department.Lst_Mst_Department.Count > 0)
            {
                listMst_Department.AddRange(objRT_Mst_Department.Lst_Mst_Department);
            }
            return listMst_Department;
        }
        protected List<Mst_Department> List_Mst_Department(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Department = new List<Mst_Department>();

            var objRQ_Mst_Department = new RQ_Mst_Department()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                // RQ_Mst_Department
                Rt_Cols_Mst_Department = "*",
                Mst_Department = null,
            };
            listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
            return listMst_Department;
        }
        private string strWhereClause_Department_Base(Mst_Department obj)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Department = TableName.Mst_Department + ".";
            if (!CUtils.IsNullOrEmpty(obj.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.MST, obj.MST, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.NetworkID, obj.NetworkID, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, obj.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        protected List<Mst_DepartmentUI> ListDepartmentUI(string strWhereClause = "")
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Department = new List<Mst_Department>();
            var listMst_DepartmentUI = new List<Mst_DepartmentUI>();
            var _listMst_DepartmentUI = new List<Mst_DepartmentUI>();
            var strWhereClauseMst_Department = "";
            if (CUtils.IsNullOrEmpty(strWhereClause))
            {
                var objMst_Department = new Mst_Department()
                {
                    FlagActive = FlagActive,
                };
                strWhereClauseMst_Department = strWhereClause_Department_Base(objMst_Department);
            }
            else
            {
                strWhereClauseMst_Department = strWhereClause;
            }
            var objRQ_Mst_Department = new RQ_Mst_Department()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Department,
                Ft_Cols_Upd = null,
                FuncType = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                // RQ_Mst_PaymentPartnerType
                Rt_Cols_Mst_Department = "*"
            };
            listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
            if (listMst_Department != null && listMst_Department.Count > 0)
            {
                foreach (var item in listMst_Department)
                {
                    var objMst_DepartmentUI = new Mst_DepartmentUI()
                    {
                        DepartmentCode = item.DepartmentCode,
                        DepartmentCodeParent = item.DepartmentCodeParent,
                        DepartmentBUCode = item.DepartmentBUCode,
                        DepartmentBUPattern = item.DepartmentBUPattern,
                        DepartmentLevel = item.DepartmentLevel,
                        DepartmentName = item.DepartmentName,
                        MST = item.MST,
                        NetworkID = item.NetworkID,
                        FlagActive = item.FlagActive,
                        LogLUDTimeUTC = item.LogLUDTimeUTC,
                        LogLUBy = item.LogLUBy,
                    };
                    _listMst_DepartmentUI.Add(objMst_DepartmentUI);
                }

                var getGroupBaseList = Mst_DepartmentExtension.GetGroupBaseList(_listMst_DepartmentUI);
                var toGroupBaseTree = Mst_DepartmentExtension.ToGroupBaseTree(getGroupBaseList);
                var toFlatGroupBaseTree = Mst_DepartmentExtension.ToFlatGroupBaseTree(toGroupBaseTree);

                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    listMst_DepartmentUI.AddRange(toFlatGroupBaseTree);
                }
            }
            return listMst_DepartmentUI;
        }
        #endregion
        #region["Quận/Huyện"]
        protected List<Mst_District> WA_Mst_District_Get(RQ_Mst_District objRQ_Mst_District)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_District = new List<Mst_District>();

            var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
            if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
            {
                listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
            }
            return listMst_District;
        }
        protected List<Mst_District> List_Mst_District(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_District = new List<Mst_District>();

            var objRQ_Mst_District = new RQ_Mst_District()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_District = "*",
                Mst_District = null,
            };
            listMst_District = WA_Mst_District_Get(objRQ_Mst_District);
            return listMst_District;
        }

        #endregion
        #region["Tỉnh/Thành phố"]
        protected List<Mst_Province> WA_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Province = new List<Mst_Province>();

            Stopwatch stAddCustomerNNT_GetProvince_biz = new Stopwatch();
            stAddCustomerNNT_GetProvince_biz.Reset();
            stAddCustomerNNT_GetProvince_biz.Start();
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, addressAPIs);
            stAddCustomerNNT_GetProvince_biz.Stop();
            long timestAddCustomerNNT_GetProvince_biz = stAddCustomerNNT_GetProvince_biz.ElapsedMilliseconds;
            ViewBag.timestAddCustomerNNT_GetProvince_biz = timestAddCustomerNNT_GetProvince_biz;
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }
            return listMst_Province;
        }
        protected List<Mst_Province> List_Mst_Province(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Province = new List<Mst_Province>();

            var objRQ_Mst_Province = new RQ_Mst_Province()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            listMst_Province = WA_Mst_Province_Get(objRQ_Mst_Province);
            return listMst_Province;
        }

        #endregion
        #region["Quản lý đại lý"]
        protected List<Mst_Dealer> WA_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
            }
            return listMst_Dealer;
        }
        protected List<Mst_Dealer> List_Mst_Dealer(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            listMst_Dealer = WA_Mst_Dealer_Get(objRQ_Mst_Dealer);
            return listMst_Dealer;
        }


        #endregion
        #region["Loại giấy tờ"]
        protected List<Mst_GovIDType> WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_GovIDType = new List<Mst_GovIDType>();

            var objRT_Mst_GovIDType = Mst_GovIDTypeService.Instance.WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType, addressAPIs);
            if (objRT_Mst_GovIDType.Lst_Mst_GovIDType != null && objRT_Mst_GovIDType.Lst_Mst_GovIDType.Count > 0)
            {
                listMst_GovIDType.AddRange(objRT_Mst_GovIDType.Lst_Mst_GovIDType);
            }
            return listMst_GovIDType;
        }
        protected List<Mst_GovIDType> List_Mst_GovIDType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_GovIDType = new List<Mst_GovIDType>();

            var objRQ_Mst_GovIDType = new RQ_Mst_GovIDType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_GovIDType
                Rt_Cols_Mst_GovIDType = "*",
                Mst_GovIDType = null,
            };
            listMst_GovIDType = WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType);
            return listMst_GovIDType;
        }
        #endregion
        #region["Chi cục thuế"]
        protected List<Mst_GovTaxID> List_Mst_GovTaxID(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_GovTaxID = new List<Mst_GovTaxID>();

            var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_GovTaxID
                Rt_Cols_Mst_GovTaxID = "*",
                Mst_GovTaxID = null,
            };
            var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
            if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
            {
                listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
            }
            return listMst_GovTaxID;
        }

        #endregion
        #region["Solution"]
        protected List<Sys_Solution> WA_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listSys_Solution = new List<Sys_Solution>();

            var objRT_Mst_Dealer = Sys_SolutionService.Instance.WA_Sys_Solution_Get(objRQ_Sys_Solution, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Sys_Solution != null && objRT_Mst_Dealer.Lst_Sys_Solution.Count > 0)
            {
                listSys_Solution.AddRange(objRT_Mst_Dealer.Lst_Sys_Solution);
            }
            return listSys_Solution;
        }
        protected List<Sys_Solution> List_Sys_Solution(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listSys_Solution = new List<Sys_Solution>();

            var objRQ_Sys_Solution = new RQ_Sys_Solution()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Sys_Solution
                Rt_Cols_Sys_Solution = "*",
            };
            listSys_Solution = WA_Sys_Solution_Get(objRQ_Sys_Solution);
            return listSys_Solution;
        }
        #endregion
        #region["Seq_Common"]
        protected string SeqNo(Seq_Common objSeq_Common)
        {
            var seqNo = "";
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Tid = GetNextTId(),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Seq_Common = objSeq_Common,
            };
            var objRT_Seq_Common = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPIs);
            seqNo = objRT_Seq_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            return seqNo;
        }
        #endregion
        #region["WA_Mst_Brand_Get"]
        protected List<ProductCentrer.Mst_Brand> WA_Mst_Brand_Get(ProductCentrer.RQ_Mst_Brand objRQ_Mst_Brand)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_Brand = new List<ProductCentrer.Mst_Brand>();

            var objRT_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
            if (objRT_Mst_Brand.Lst_Mst_Brand != null && objRT_Mst_Brand.Lst_Mst_Brand.Count > 0)
            {
                listMst_Brand.AddRange(objRT_Mst_Brand.Lst_Mst_Brand);
            }
            return listMst_Brand;
        }

        protected List<ProductCentrer.Mst_Brand> List_Mst_Brand(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Brand = new List<ProductCentrer.Mst_Brand>();

            var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Brand
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            listMst_Brand = WA_Mst_Brand_Get(objRQ_Mst_Brand);

            return listMst_Brand;
        }
        #endregion
        #region["WA_Mst_Model_Get"]
        protected List<ProductCentrer.Mst_Model> WA_Mst_Model_Get(ProductCentrer.RQ_Mst_Model objRQ_Mst_Model)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_Model = new List<ProductCentrer.Mst_Model>();

            var objRT_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
            if (objRT_Mst_Model.Lst_Mst_Model != null && objRT_Mst_Model.Lst_Mst_Model.Count > 0)
            {
                listMst_Model.AddRange(objRT_Mst_Model.Lst_Mst_Model);
            }
            return listMst_Model;
        }

        protected List<ProductCentrer.Mst_Model> List_Mst_Model(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Model = new List<ProductCentrer.Mst_Model>();

            var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Model
                Rt_Cols_Mst_Model = "*",
                Mst_Model = null,
            };
            listMst_Model = WA_Mst_Model_Get(objRQ_Mst_Model);

            return listMst_Model;
        }
        #endregion
        #region["WA_Mst_Spec_Get"]
        protected List<ProductCentrer.Mst_Spec> WA_Mst_Spec_Get(ProductCentrer.RQ_Mst_Spec objRQ_Mst_Spec)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_Spec = new List<ProductCentrer.Mst_Spec>();

            var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
            if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
            {
                listMst_Spec.AddRange(objRT_Mst_Spec.Lst_Mst_Spec);
            }
            return listMst_Spec;
        }

        protected List<ProductCentrer.Mst_Spec> List_Mst_Spec(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Spec = new List<ProductCentrer.Mst_Spec>();

            var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Spec
                Rt_Cols_Mst_Spec = "*",
                Rt_Cols_Mst_SpecImage = null,
                Rt_Cols_Mst_SpecFiles = null,
                Mst_Spec = null,
            };
            listMst_Spec = WA_Mst_Spec_Get(objRQ_Mst_Spec);

            return listMst_Spec;
        }
        #endregion
        #region["WA_Mst_SpecType1_Get - Loại sản phẩm"]
        protected List<ProductCentrer.Mst_SpecType1> WA_Mst_SpecType1_Get(ProductCentrer.RQ_Mst_SpecType1 objRQ_Mst_SpecType1)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_SpecType1 = new List<ProductCentrer.Mst_SpecType1>();

            var objRT_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
            if (objRT_Mst_SpecType1.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1.Lst_Mst_SpecType1.Count > 0)
            {
                listMst_SpecType1.AddRange(objRT_Mst_SpecType1.Lst_Mst_SpecType1);
            }
            return listMst_SpecType1;
        }
        protected List<ProductCentrer.Mst_SpecType1> List_Mst_SpecType1(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_SpecType1 = new List<ProductCentrer.Mst_SpecType1>();

            var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_SpecType1
                Rt_Cols_Mst_SpecType1 = "*",
                Mst_SpecType1 = null,
            };
            listMst_SpecType1 = WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1);

            return listMst_SpecType1;
        }
        #endregion
        #region["WA_Mst_SpecType2_Get - Nhóm sản phẩm"]
        protected List<ProductCentrer.Mst_SpecType2> WA_Mst_SpecType2_Get(ProductCentrer.RQ_Mst_SpecType2 objRQ_Mst_SpecType2)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_SpecType2 = new List<ProductCentrer.Mst_SpecType2>();

            var objRT_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_Mst_SpecType2_Get(objRQ_Mst_SpecType2, addressAPIs_ProductCentrer);
            if (objRT_Mst_SpecType2.Lst_Mst_SpecType2 != null && objRT_Mst_SpecType2.Lst_Mst_SpecType2.Count > 0)
            {
                listMst_SpecType2.AddRange(objRT_Mst_SpecType2.Lst_Mst_SpecType2);
            }
            return listMst_SpecType2;
        }
        protected List<ProductCentrer.Mst_SpecType2> List_Mst_SpecType2(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_SpecType2 = new List<ProductCentrer.Mst_SpecType2>();

            var objRQ_Mst_SpecType2 = new ProductCentrer.RQ_Mst_SpecType2()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_SpecType2
                Rt_Cols_Mst_SpecType2 = "*",
                Mst_SpecType2 = null,
            };
            listMst_SpecType2 = WA_Mst_SpecType2_Get(objRQ_Mst_SpecType2);

            return listMst_SpecType2;
        }
        #endregion
        #region["WA_Mst_Unit_Get - Đơn vị"]
        protected List<ProductCentrer.Mst_Unit> WA_Mst_Unit_Get(ProductCentrer.RQ_Mst_Unit objRQ_Mst_Unit)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_Unit = new List<ProductCentrer.Mst_Unit>();

            var objRT_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
            if (objRT_Mst_Unit.Lst_Mst_Unit != null && objRT_Mst_Unit.Lst_Mst_Unit.Count > 0)
            {
                listMst_Unit.AddRange(objRT_Mst_Unit.Lst_Mst_Unit);
            }
            return listMst_Unit;
        }
        protected List<ProductCentrer.Mst_Unit> List_Mst_Unit(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Unit = new List<ProductCentrer.Mst_Unit>();

            var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Unit
                Rt_Cols_Mst_Unit = "*",
                Mst_Unit = null,
            };
            listMst_Unit = WA_Mst_Unit_Get(objRQ_Mst_Unit);

            return listMst_Unit;
        }
        #endregion
        #region["WA_Mst_SpecCustomField_Get - SpecCustom Field"]
        protected List<ProductCentrer.Mst_SpecCustomField> WA_Mst_SpecCustomField_Get(ProductCentrer.RQ_Mst_SpecCustomField objRQ_Mst_SpecCustomField)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_SpecCustomField = new List<ProductCentrer.Mst_SpecCustomField>();

            var objRT_Mst_SpecCustomField = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_Mst_SpecCustomField_Get(objRQ_Mst_SpecCustomField, addressAPIs_ProductCentrer);
            if (objRT_Mst_SpecCustomField.Lst_Mst_SpecCustomField != null && objRT_Mst_SpecCustomField.Lst_Mst_SpecCustomField.Count > 0)
            {
                listMst_SpecCustomField.AddRange(objRT_Mst_SpecCustomField.Lst_Mst_SpecCustomField);
            }
            return listMst_SpecCustomField;
        }
        protected List<ProductCentrer.Mst_SpecCustomField> List_Mst_SpecCustomField(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_SpecCustomField = new List<ProductCentrer.Mst_SpecCustomField>();

            var objRQ_Mst_SpecCustomField = new ProductCentrer.RQ_Mst_SpecCustomField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_SpecCustomField
                Rt_Cols_Mst_SpecCustomField = "*",
                Mst_SpecCustomField = null,
            };
            listMst_SpecCustomField = WA_Mst_SpecCustomField_Get(objRQ_Mst_SpecCustomField);

            return listMst_SpecCustomField;
        }
        #endregion
        #region["WA_Prd_PrdIDCustomField_Get - SpecCustom Field"]
        //protected List<ProductCentrer.Prd_PrdIDCustomField> WA_Prd_PrdIDCustomField_Get(ProductCentrer.RQ_Prd_PrdIDCustomField OS_PrdCenter_Prd_PrdIDCustomField)
        //{
        //    var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
        //    var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();

        //    var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(OS_PrdCenter_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
        //    if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null && objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
        //    {
        //        listPrd_PrdIDCustomField.AddRange(objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField);
        //    }
        //    return listPrd_PrdIDCustomField;
        //}
        //protected List<ProductCentrer.Prd_PrdIDCustomField> List_Prd_PrdIDCustomField(string strWhereClause)
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();

        //    var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = OS_ProductCentrer_GwUserCode,
        //        GwPassword = OS_ProductCentrer_GwPassword,
        //        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
        //        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClause,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = UserState.UtcOffset,
        //        // RQ_Prd_PrdIDCustomField
        //        Rt_Cols_Prd_PrdIDCustomField = "*",
        //        Prd_PrdIDCustomField = null,
        //    };
        //    listPrd_PrdIDCustomField = WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField);

        //    return listPrd_PrdIDCustomField;
        //}
        #endregion
        #region["WA_Mst_CurrencyEx_Get - Loại tiền"]
        protected List<ProductCentrer.Mst_CurrencyEx> WA_Mst_CurrencyEx_Get(ProductCentrer.RQ_Mst_CurrencyEx objRQ_Mst_CurrencyEx)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_CurrencyEx = new List<ProductCentrer.Mst_CurrencyEx>();

            var objRT_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
            if (objRT_Mst_CurrencyEx.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyEx.Lst_Mst_CurrencyEx.Count > 0)
            {
                listMst_CurrencyEx.AddRange(objRT_Mst_CurrencyEx.Lst_Mst_CurrencyEx);
            }
            return listMst_CurrencyEx;
        }
        protected List<ProductCentrer.Mst_CurrencyEx> List_Mst_CurrencyEx(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_CurrencyEx = new List<ProductCentrer.Mst_CurrencyEx>();

            var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_CurrencyEx
                Rt_Cols_Mst_CurrencyEx = "*",
                Mst_CurrencyEx = null,
            };
            listMst_CurrencyEx = WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx);

            return listMst_CurrencyEx;
        }
        #endregion
        #region["WA_Mst_CurrencyEx_Get - Thuế suất"]
        protected List<ProductCentrer.Mst_VATRate> WA_Mst_VATRate_Get(ProductCentrer.RQ_Mst_VATRate objRQ_Mst_VATRate)
        {
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            var listMst_VATRate = new List<ProductCentrer.Mst_VATRate>();

            var objRT_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
            if (objRT_Mst_VATRate.Lst_Mst_VATRate != null && objRT_Mst_VATRate.Lst_Mst_VATRate.Count > 0)
            {
                listMst_VATRate.AddRange(objRT_Mst_VATRate.Lst_Mst_VATRate);
            }
            return listMst_VATRate;
        }
        protected List<ProductCentrer.Mst_VATRate> List_Mst_VATRate(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_VATRate = new List<ProductCentrer.Mst_VATRate>();

            var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_VATRate
                Rt_Cols_Mst_VATRate = "*",
                Mst_VATRate = null,
            };
            listMst_VATRate = WA_Mst_VATRate_Get(objRQ_Mst_VATRate);

            return listMst_VATRate;
        }
        #endregion     
        #region["LoadDataInSolution"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_DistrictInSolution(string provincecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_District = new List<Mst_District>();

                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_District = "";
                    strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { ProvinceCode = provincecode, FlagActive = FlagActive });

                    var objRQ_Mst_District = new RQ_Mst_District()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_District,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
                    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                    {
                        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                    }
                }
                return JsonView("LoadMst_DistrictInSolution", listMst_District);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_District", null, resultEntry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_GovTaxIDInSolution(string districtcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_GovTaxID = new List<Mst_GovTaxID>();

                if (!CUtils.IsNullOrEmpty(districtcode))
                {
                    var strWhereClauseMst_GovTaxID = "";
                    strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(new Mst_GovTaxID() { DistrictCode = districtcode, FlagActive = FlagActive });

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = UserState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };
                    var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
                    {
                        listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
                    }
                }
                return JsonView("LoadMst_GovTaxIDInSolution", listMst_GovTaxID);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_GovTaxID", null, resultEntry);
        }
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, data.ProvinceCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_GovTaxID(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.DistrictCode, data.DistrictCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
        #region["Upload File"]
        protected string[] WA_UploadFileNew(RQ_File objFile, string addressAPIs)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRQ_File = new RQ_File()
            {
                folderUpload = objFile.folderUpload ?? "UploadedFiles",
                fileName = objFile.fileName,
                uploadFileAsBase64String = objFile.uploadFileAsBase64String,
                Tid = GetNextDateTId(),
                GwPassword = GwPassword,
                GwUserCode = GwUserCode,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword
            };
            var result = new FileService().WA_UploadFileNew(objRQ_File, addressAPIs);
            return result;
        }
        protected string[] WA_MoveFileNew(RQ_File objFile, string addressAPIs)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRQ_File = new RQ_File()
            {
                folderUpload = objFile.folderUpload ?? "UploadedFiles",
                fileName = "",
                sourceFileName = objFile.sourceFileName,
                destFileName = objFile.destFileName,
                uploadFileAsBase64String = objFile.uploadFileAsBase64String,
                Tid = GetNextDateTId(),
                GwPassword = GwPassword,
                GwUserCode = GwUserCode,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword
            };
            var result = new FileService().WA_MoveFileNew(objRQ_File, addressAPIs);
            return result;
        }
        #endregion
        #region ["Get link ảnh PRDCENTER WA_OS_GetFileFromPath"]
        public string WA_OS_GetFileFromPath(string Tid, string waUserCode, string waUserPassword)
        {
            var host = "";
            var addressAPIs_ProductCentrer = UserState.AddressAPIs_ProductCentrer;
            host = addressAPIs_ProductCentrer + "api/File";
            return host;
        }
        #endregion
        #region ["Nhóm sản phẩm"]
        protected List<Mst_PartMaterialType> WA_Mst_PartMaterialType_Get(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartMaterialType = new List<Mst_PartMaterialType>();

            Stopwatch stAddCustomerNNT_GetProvince_biz = new Stopwatch();
            stAddCustomerNNT_GetProvince_biz.Reset();
            stAddCustomerNNT_GetProvince_biz.Start();
            var objRT_Mst_PartMaterialType = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, addressAPIs);
            stAddCustomerNNT_GetProvince_biz.Stop();
            long timestAddCustomerNNT_GetProvince_biz = stAddCustomerNNT_GetProvince_biz.ElapsedMilliseconds;
            ViewBag.timestAddCustomerNNT_GetProvince_biz = timestAddCustomerNNT_GetProvince_biz;
            if (objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType.Count > 0)
            {
                listMst_PartMaterialType.AddRange(objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType);
            }
            return listMst_PartMaterialType;
        }
        protected List<Mst_PartMaterialType> List_Mst_PartMaterialType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_PartMaterialType = new List<Mst_PartMaterialType>();

            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Province
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };
            listMst_PartMaterialType = WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType);
            return listMst_PartMaterialType;
        }
        #endregion
        #region["Loại khách hàng"]
        protected List<Mst_NNTType> List_Mst_NNTType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_NNTType = new List<Mst_NNTType>();

            var objRQ_Mst_NNTType = new RQ_Mst_NNTType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNTType
                Rt_Cols_Mst_NNTType = "*",
                Mst_NNTType = null,
            };
            var objRT_Mst_NNTType = Mst_NNTTypeService.Instance.WA_Mst_NNTType_Get(objRQ_Mst_NNTType, addressAPIs);
            if (objRT_Mst_NNTType.Lst_Mst_NNTType != null && objRT_Mst_NNTType.Lst_Mst_NNTType.Count > 0)
            {
                listMst_NNTType.AddRange(objRT_Mst_NNTType.Lst_Mst_NNTType);
            }
            return listMst_NNTType;
        }

        #endregion
        #region["Khách hàng"]
        protected List<Mst_NNT> List_Mst_NNT(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_NNT = new List<Mst_NNT>();

            var objRQ_Mst_NNT = new RQ_Mst_NNT()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = "*",
                Mst_NNT = null,
            };
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            return listMst_NNT;
        }
        #endregion
        #region ["Thông tin số lượng hóa đơn"]

        public List<Invoice_license> WA_Invoice_license_Get_By_MST()
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPI = UserState.AddressAPIs;
            var listInvoice_license = new List<Invoice_license>();
            var objRQ_Invoice_license = new RQ_Invoice_license
            {
                Rt_Cols_Invoice_license = "*",
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount
            };
            var objRT_Invoice_license = Invoice_licenseService.Instance.WA_Invoice_license_Get(objRQ_Invoice_license, addressAPI);
            if (objRT_Invoice_license.Lst_Invoice_license != null && objRT_Invoice_license.Lst_Invoice_license.Count > 0)
            {
                var mst = CUtils.StrValue(UserState.Mst_NNT.MST);
                if (!CUtils.IsNullOrEmpty(mst))
                {
                    listInvoice_license = objRT_Invoice_license.Lst_Invoice_license.Where(x => x.MST.ToString() == mst.ToString()).ToList();
                }
            }

            return listInvoice_license;
        }
        #endregion

        #region ["Inbrand Clound"]
        protected List<Mst_Part> List_Mst_Part(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Part = new List<Mst_Part>();

            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNT
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            if (objRT_Mst_Part.Lst_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part.Count > 0)
            {
                listMst_Part.AddRange(objRT_Mst_Part.Lst_Mst_Part);
            }
            return listMst_Part;
        }
        protected List<Mst_PartType> List_Mst_PartType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartType = new List<Mst_PartType>();

            var objRQ_Mst_PartType = new RQ_Mst_PartType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNT
                Rt_Cols_Mst_PartType = "*",
                Mst_PartType = null,
            };
            var objRT_Mst_Part = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, addressAPIs);
            if (objRT_Mst_Part.Lst_Mst_PartType != null && objRT_Mst_Part.Lst_Mst_PartType.Count > 0)
            {
                listMst_PartType.AddRange(objRT_Mst_Part.Lst_Mst_PartType);
            }
            return listMst_PartType;
        }
        protected List<Mst_PartUnit> List_Mst_PartUnit(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartUnit = new List<Mst_PartUnit>();

            var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.TokenID.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNT
                Rt_Cols_Mst_PartUnit = "*",
                Mst_PartUnit = null,
            };
            var objRT_Mst_PartUnit = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, addressAPIs);
            if (objRT_Mst_PartUnit.Lst_Mst_PartUnit != null && objRT_Mst_PartUnit.Lst_Mst_PartUnit.Count > 0)
            {
                listMst_PartUnit.AddRange(objRT_Mst_PartUnit.Lst_Mst_PartUnit);
            }
            return listMst_PartUnit;
        }
        #endregion
        #endregion
    }
    public class BaseController : idnBaseController
    {
        public Stopwatch st = new Stopwatch();
        #region["Init"]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //int sessionTimeout = System.Web.HttpContext.Current.Session.Timeout;

            //var networkid = (string)RouteData.Values["networkid"];
            //var routeData = RouteData.Values;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Init();
        }
        protected virtual void Init()
        {

        }
        #endregion
        protected BaseController()
        {

        }
    }
}