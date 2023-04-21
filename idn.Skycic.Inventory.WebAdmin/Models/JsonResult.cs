using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Controllers;

namespace idn.Skycic.Inventory.WebAdmin.Models
{
    public class FieldError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class JsonResultEntry
    {
        public JsonResultEntry()
        {
            this.Success = true;
            Messages = new string[0];
            FieldErrors = new FieldError[0];
        }

        public JsonResultEntry(ModelStateDictionary modelState)
            : this()
        {
            this.AddModelState(modelState);
        }

        public JsonResultEntry SetFailed()
        {
            this.Success = false;
            return this;
        }

        public JsonResultEntry SetSuccess()
        {
            this.Success = true;
            return this;
        }

        public JsonResultEntry AddModelState(ModelStateDictionary modelState)
        {
            foreach (var ms in modelState)
            {
                foreach (var err in ms.Value.Errors)
                {
                    this.AddFieldError(ms.Key, err.ErrorMessage);
                }
            }

            return this;
        }

        public bool Success { get; set; }
        public string[] Messages { get; set; }
        public object Model { get; set; }
        public string RedirectUrl { get; set; }
        private bool redirectToOpener = false;
        public bool RedirectToOpener { get { return redirectToOpener; } set { redirectToOpener = value; } }
        public FieldError[] FieldErrors { get; set; }
        public bool IsServiceException { get; set; }
        public string Detail { get; set; }


        public JsonResultEntry AddFieldError(string fieldName, string message)
        {
            Success = false;
            FieldErrors = FieldErrors.Concat(new[] { new FieldError() { FieldName = fieldName, ErrorMessage = message } }).ToArray();
            return this;
        }
        public JsonResultEntry AddMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();
            return this;
        }

        public JsonResultEntry AddException(BaseController context, Exception cex)
        {

            this.Success = false;
            return AddDetail(ShowException(cex));
        }

        public JsonResultEntry AddDetail(string detail)
        {
            var detailCur = (!CUtils.IsNullOrEmpty(detail)) ? detail : "Error";
            Detail = detailCur;
            return this;
        }

        public string ShowException(Exception ex)
        {
            var sb = new StringBuilder();
            var errorMessage = "";
            if (ex != null)
            {
                if (ex is ClientException)
                {
                    var cex = ex as ClientException;

                    var c_K_DT_Sys = cex.c_K_DT_Sys;
                    var exception = cex.Exception;

                    if (c_K_DT_Sys != null)
                    {
                        var Lst_c_K_DT_SysInfo = c_K_DT_Sys.Lst_c_K_DT_SysInfo;
                        var Lst_c_K_DT_SysError = c_K_DT_Sys.Lst_c_K_DT_SysError;
                        var Lst_c_K_DT_SysWarning = c_K_DT_Sys.Lst_c_K_DT_SysWarning;

                        #region["Lst_c_K_DT_SysInfo"]
                        sb.Append(string.Format("Lst_c_K_DT_SysInfo: {0}", ""));
                        sb.Append("<br/>");
                        if (Lst_c_K_DT_SysInfo != null && Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            var strTid = "";
                            var strDigitalSignature = "";
                            var strErrorCode = "";
                            var strFlagWarning = "";
                            var strRemark = "";
                            var strFlagCompress = "";
                            var strFlagEncrypt = "";
                            foreach (var item in Lst_c_K_DT_SysInfo)
                            {
                                if (!CUtils.IsNullOrEmpty(item.Tid))
                                {
                                    strTid = item.Tid.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.DigitalSignature))
                                {
                                    strDigitalSignature = item.DigitalSignature.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.ErrorCode))
                                {
                                    strErrorCode = item.ErrorCode.Trim();
                                    errorMessage = Constants.Error.GetErrorMessage(strErrorCode);
                                }
                                if (!CUtils.IsNullOrEmpty(item.FlagWarning))
                                {
                                    strFlagWarning = item.FlagWarning.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.Remark))
                                {
                                    strRemark = item.Remark.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.FlagCompress))
                                {
                                    strFlagCompress = item.FlagCompress.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.FlagEncrypt))
                                {
                                    strFlagEncrypt = item.FlagEncrypt.Trim();
                                }

                                sb.Append(string.Format("Tid: {0}", strTid));
                                sb.Append("<br/>");
                                sb.Append(string.Format("DigitalSignature: {0}", strDigitalSignature));
                                sb.Append("<br/>");
                                sb.Append(string.Format("ErrorCode: {0}", strErrorCode));
                                sb.Append("<br/>");
                                sb.Append(string.Format("FlagWarning: {0}", strFlagWarning));
                                sb.Append("<br/>");
                                sb.Append(string.Format("Remark: {0}", strRemark));
                                sb.Append("<br/>");
                                sb.Append(string.Format("FlagCompress: {0}", strFlagCompress));
                                sb.Append("<br/>");
                                sb.Append(string.Format("FlagEncrypt: {0}", strFlagEncrypt));
                            }
                        }
                        #endregion

                        #region["Lst_c_K_DT_SysError"]
                        sb.Append("<br/>--------------------------------------------------------<br/>");
                        sb.Append(string.Format("Lst_c_K_DT_SysError: {0}", ""));
                        sb.Append("<br/>");
                        if (Lst_c_K_DT_SysError != null && Lst_c_K_DT_SysError.Count > 0)
                        {
                            var strPCode = "";
                            var strPVal = "";
                            foreach (var item in Lst_c_K_DT_SysError)
                            {
                                strPCode = "";
                                strPVal = "";
                                if (!CUtils.IsNullOrEmpty(item.PCode))
                                {
                                    strPCode = item.PCode.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.PVal))
                                {
                                    strPVal = item.PVal.Trim();
                                }
                                sb.Append(string.Format("PCode: {0}", strPCode));
                                sb.Append("<br/>");
                                sb.Append(string.Format("PVal: {0}", strPVal));
                                sb.Append("<br/>");
                            }
                        }
                        #endregion

                        #region["Lst_c_K_DT_SysWarning"]
                        sb.Append("<br/>--------------------------------------------------------<br/>");
                        sb.Append(string.Format("Lst_c_K_DT_SysWarning: {0}", ""));
                        sb.Append("<br/>");
                        if (Lst_c_K_DT_SysWarning != null && Lst_c_K_DT_SysWarning.Count > 0)
                        {
                            var strPCode = "";
                            var strPVal = "";
                            foreach (var item in Lst_c_K_DT_SysWarning)
                            {
                                strPCode = "";
                                strPVal = "";
                                if (!CUtils.IsNullOrEmpty(item.PCode))
                                {
                                    strPCode = item.PCode.Trim();
                                }
                                if (!CUtils.IsNullOrEmpty(item.PVal))
                                {
                                    strPVal = item.PVal.Trim();
                                }
                                sb.Append(string.Format("PCode: {0}", strPCode));
                                sb.Append("<br/>");
                                sb.Append(string.Format("PVal: {0}", strPVal));
                                sb.Append("<br/>");
                            }
                        }
                        #endregion
                    }

                    if (exception != null)
                    {
                        var strStackTrace = "";
                        var strMessage = "";
                        if (!CUtils.IsNullOrEmpty(exception.Message))
                        {
                            strMessage = exception.Message.Trim();
                        }
                        if (!CUtils.IsNullOrEmpty(exception.StackTrace))
                        {
                            strStackTrace = exception.StackTrace.Trim();
                        }
                        if (!CUtils.IsNullOrEmpty(strStackTrace))
                        {
                            sb.Append("<br/>--------------------------------------------------------<br/>");
                            sb.Append(string.Format("Exception: {0}", ""));
                            sb.Append("<br/>");
                            sb.Append(string.Format("Message: {0}", strMessage));
                            sb.Append("<br/>");
                            sb.Append(string.Format("StackTrace: {0}", strStackTrace));
                        }

                    }
                }
                else
                {
                    sb.Append(string.Format("Exception: {0}", ""));
                    sb.Append("<br/>");

                    var strStackTrace = "";
                    var strMessage = "";
                    if (!CUtils.IsNullOrEmpty(ex.Message))
                    {
                        strMessage = ex.Message.Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(ex.StackTrace))
                    {
                        strStackTrace = ex.StackTrace.Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(strStackTrace))
                    {
                        sb.Append(string.Format("Message: {0}", strMessage));
                        sb.Append("<br/>");
                        sb.Append(string.Format("StackTrace: {0}", strStackTrace));
                    }
                }

            }
            var error = sb.ToString();
            var strError = "<div style=\"width: 100%; padding: 5px 0 10px 0; float: left;\"><span style=\"font: 700 17px/20px arial; color: #e00;\">ErrorMessage: " + errorMessage + "</span></div>";
            strError += "<div class=\"error_detail_panel\"  style=\"width: 100%; float: left; border-top:solid 1px #efefef;\">";
            strError += "<p style=\"font: 500 12px/20px arial;\">" + error + "</p>";
            strError += "</div>";
            return strError;
        }
    }
}