using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.WebAdmin.Extensions
{
    public static class ErrorExtensions
    {
        public static IHtmlString ToHtml(this Exception ex)
        {

            var sb = new StringBuilder();
            if (ex is ClientException)
            {
                var cex = ex as ClientException;
                var errorMessage = "";
                //var errorCode = "";
                //var errorDetail = "";
                if (cex != null)
                {
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
                                //if(!CUtils.IsNullOrEmpty(item.ErrorCode))
                                //{
                                //    errorCode += item.ErrorCode;
                                //}
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
                                    strPVal = item.PVal.Replace("\n", "<br/>").Trim();
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
                                    strPVal = item.PVal.Replace("\n", "<br/>").Trim();
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
                        //sb.Append("<br/>--------------------------------------------------------<br/>");
                        //sb.Append(string.Format("Exception: {0}", ""));
                        //sb.Append("<br/>");
                        var strStackTrace = "";
                        var strMessage = "";
                        if (!CUtils.IsNullOrEmpty(exception.Message))
                        {
                            strMessage = exception.Message.Trim();
                        }
                        if (!CUtils.IsNullOrEmpty(exception.StackTrace))
                        {
                            strStackTrace = exception.StackTrace.Replace("\n", "<br/>").Trim();
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
                var error = sb.ToString();
                var strError = "<div style=\"width: 100%; padding: 5px 0 10px 0; float: left;\"><span style=\"font: 700 17px/20px arial; color: #e00;\">ErrorMessage: " + errorMessage + "</span></div>";
                strError += "<div class=\"error_detail_panel\"  style=\"width: 100%; float: left; border-top:solid 1px #efefef;\">";
                strError += "<p style=\"font: 500 12px/20px arial;\">" + error + "</p>";
                strError += "</div>";
                return new HtmlString(strError);
            }
            else
            {
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
                sb.Append("<br/>--------------------------------------------------------<br/>");
                sb.Append(string.Format("Exception: {0}", ""));
                sb.Append("<br/>");
                sb.Append(string.Format("Message: {0}", strMessage));
                sb.Append("<br/>");
                sb.Append(string.Format("StackTrace: {0}", strStackTrace));
            }
            return new HtmlString(sb.ToString());
        }
    }
}