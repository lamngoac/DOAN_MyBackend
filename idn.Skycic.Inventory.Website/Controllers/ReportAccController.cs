using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class ReportAccController : RptInitController
    {
        #region["Login"]
        [AllowAnonymous]
        public ActionResult Login(string redirecturl)
        {
            InitSSO();
            Session["UserIsHere"] = true;
            var url = string.Format("{0}{1}", GetRequestBaseAddress(), redirecturl);
            var loginCallback = Url.Action("LoginCallback", "ReportAcc", new { redirecturl = url });
            loginCallback = loginCallback.IndexOf("//", StringComparison.Ordinal) == 0 ? loginCallback.Replace("//", "/") : loginCallback;
            string callbackUrl = string.Format("{0}{1}", GetRequestBaseAddress(), loginCallback);

            var accSv = new inos.common.Service.AccountService(Session);
            var userAuthorization = accSv.PrepareRequestUserAuthorization(callbackUrl);
            userAuthorization.Send(HttpContext);
            Response.End();
            return null;
        }

        [AllowAnonymous]
        public ActionResult LoginCallback(string redirecturl)
        {
            try
            {
                Uri myUri = new Uri(redirecturl);
                string networkID = HttpUtility.ParseQueryString(myUri.Query).Get("networkid");
                var orgID = "";
                InitSSO();
                var userReportState = new UserReportState();
                var waUserCode_MstSV = WAUserCode_MstSV;
                var waUserPassword_MstSV = WAUserPassword_MstSV;
                var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;

                var accSv = new inos.common.Service.AccountService(Session);
                var authorizationState = accSv.ProcessUserAuthorization(Request);

                if (authorizationState != null)
                {
                    var accessToken = authorizationState.AccessToken;
                    var refreshToken = authorizationState.RefreshToken;

                    accSv.AccessToken = accessToken;

                    var inosuser = accSv.GetCurrentUser();
                    if (inosuser != null)
                    {
                        userReportState.UserName = inosuser.Email;
                        userReportState.Email = inosuser.Email;
                        userReportState.NetworkID = networkID;
                        userReportState.AccessToken = accessToken;
                        userReportState.UtcOffset = 7.0;
                        FormsAuthentication.SetAuthCookie(inosuser.Email, false);
                        var objRQ_MstSv_Sys_UserCur = new RQ_MstSv_Sys_User()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            WAUserCode = waUserCode_MstSV,
                            WAUserPassword = waUserPassword_MstSV,
                            AccessToken = accessToken,
                            NetworkID = networkID, // bắt buộc truyền
                            OrgID = orgID, // không bắt buộc truyền
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = "",
                            FuncType = null,
                        };
                        var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_UserCur, addressMasterServerAPIs);

                        if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null
                            && objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null
                            && objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                            if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                            {
                                userReportState.AddressAPIs = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                                // không cần get thông tin user

                                this.UserReportState = userReportState;
                                //var urlCur = Url.ActionLocalized("Index", "Dashboard");
                                return Redirect(redirecturl);
                            }
                            else
                            {
                                var redirectUrl = RedirectActionToReportLogin();
                                Response.Redirect(redirectUrl, false);
                            }
                        }
                        else
                        {
                            var redirectUrl = RedirectActionToReportLogin();
                            Response.Redirect(redirectUrl, false);
                        }

                    }
                    else
                    {
                        var redirectUrl = RedirectActionToReportLogin();
                        Response.Redirect(redirectUrl, false);
                    }
                }

                return Json("invalid", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = CUtils.StrValue(ex.Message);
                if (message.Contains("Unexpected OAuth authorization response received with callback and client state that does not match an expected value"))
                {
                    if (!CUtils.IsNullOrEmpty(redirecturl))
                    {
                        return Redirect(redirecturl);
                    }
                }
                return null;
            }

            

        }
        #endregion
    }
}