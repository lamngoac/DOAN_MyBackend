using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Extensions;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.State;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class AccountController : BaseController
    {
        #region["Login"]
        public string GetIPAddress()
        {
            var IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        [AllowAnonymous]
        public ActionResult Login(string redirectUrl)
        {
            Response.Cookies.Clear();
            if (Session != null)
            {
                Session.Clear();
            }
            var usercode = "";
            var password = "";
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = "/";
            }
            if (Request.Cookies["ckucadmin"] != null)
            {
                HttpCookie ck_usercode = Request.Cookies["ckucadmin"];
                if (ck_usercode != null)
                {
                    var ckusercode = ck_usercode.Value;
                    if (!CUtils.IsNullOrEmpty(ckusercode))
                    {
                        usercode = ckusercode;
                    }
                }
            }
            if (Request.Cookies["ckpwadmin"] != null)
            {
                HttpCookie ck_password = Request.Cookies["ckpwadmin"];
                if (ck_password != null)
                {
                    var ckpassword = ck_password.Value;
                    if (!CUtils.IsNullOrEmpty(ckpassword))
                    {
                        password = ckpassword;
                    }
                }
            }
            ViewBag.UserCode = usercode;
            ViewBag.Password = password;
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            try
            {
                if (Session != null)
                {
                    Session.Clear();
                }
                var password = model.Password;
                var decode = CUtils.Base64_Decode(password);
                password = CUtils.ReverseString(decode);
                model.Password = password; //CUtils.GetEncodedHash(password);
                var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                    WAUserCode = model.UserCode,
                    WAUserPassword = model.Password,
                    // RptSv_Sys_User
                    Rt_Cols_RptSv_Sys_User = "*",
                    Rt_Cols_RptSv_Sys_UserInGroup = null,
                    RptSv_Sys_User = null,
                };

                var objRT_RptSv_Sys_User = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Login(objRQ_RptSv_Sys_User, addressReportAPIs);

                if (objRT_RptSv_Sys_User != null)
                {
                    var strWhereClause = "";
                    objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User = "*";
                    objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_UserInGroup = "*";
                    objRQ_RptSv_Sys_User.Ft_WhereClause = strWhereClause;

                    var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_GetForCurrentUser(objRQ_RptSv_Sys_User, addressReportAPIs);
                    if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                    {
                        objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].UserPassword = model.Password;
                        var userSate = new UserState(objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0], objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_Access)
                        {
                            AddressAPIs = ServiceAddress.BaseReportServerAPIAddress
                        };
                        this.UserState = userSate;
                    }
                    
                    FormsAuthentication.SetAuthCookie(model.UserCode, false);
                    if (model.RememberMe)
                    {
                        HttpCookie ck_usercode = new HttpCookie("ckucadmin")
                        {
                            Value = model.UserCode,
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Add(ck_usercode);

                        HttpCookie ck_password = new HttpCookie("ckpwadmin")
                        {
                            Value = model.Password,
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Add(ck_password);
                    }
                    if (string.IsNullOrEmpty(model.RedirectUrl))
                    {
                        var url = Url.ActionLocalized("Index", "Home");
                        model.RedirectUrl = url;
                    }
                    else
                    {
                        var url = model.RedirectUrl.Trim().LocalizedUrl();
                        model.RedirectUrl = url;
                    }
                }
                else
                {
                    var url = Url.Action("Login", "Account", new { returnUrl = "/" });
                    model.RedirectUrl = url;
                    resultEntry.AddMessage("Tên đăng nhập hoặc mật khẩu không hợp lệ!");
                }
                resultEntry.Success = true;
                resultEntry.RedirectToOpener = true;
                resultEntry.RedirectUrl = model.RedirectUrl;

                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            resultEntry.IsServiceException = true;
            return Json(resultEntry);

        }



        #endregion

        #region["LogOff"]
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();

            var cookie = new HttpCookie("ASP.NET_SessionId") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(cookie);

            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region[ChangePassword]
        [HttpPost]
        public ActionResult ChangePassword(string pass, string passnew, string confpass)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                var title = "";
                pass = CUtils.StrValue(pass);
                pass = CUtils.GetEncodedHash(pass);
                if (pass.Equals(waUserPassword))
                {
                    if (!CUtils.IsNullOrEmpty(passnew))
                    {
                        passnew = CUtils.StrValue(passnew);
                        if (!CUtils.IsNullOrEmpty(confpass))
                        {
                            confpass = CUtils.StrValue(confpass);
                            if (passnew.Equals(confpass))
                            {
                                var strWhereClause = "";
                                var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                                    // RQ_RptSv_Sys_User
                                    Rt_Cols_RptSv_Sys_User = "*",
                                    Rt_Cols_RptSv_Sys_UserInGroup = null,
                                    RptSv_Sys_User = null,
                                };

                                var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);

                                if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                                {
                                    objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].UserPasswordNew = CUtils.GetEncodedHash(passnew);

                                    objRQ_RptSv_Sys_User.Ft_WhereClause = null;
                                    objRQ_RptSv_Sys_User.Ft_Cols_Upd = null;
                                    objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User = null;
                                    objRQ_RptSv_Sys_User.RptSv_Sys_User = objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0];
                                    RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_ChangePassword(objRQ_RptSv_Sys_User, addressAPIs);
                                    title = "Thay đổi mật khẩu thành công!";
                                    resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                                }
                                else
                                {
                                    title = "Mã người dùng '" + waUserCode + "' không có trong hệ thống!";
                                }
                            }
                            else
                            {
                                title = "Nhập lại mật khẩu mới không đúng, Vui lòng nhập lại!";
                            }
                        }
                        else
                        {
                            title = "Nhập lại khẩu mới trống!";
                        }
                    }
                    else
                    {
                        title = "Mật khẩu mới trống!";
                    }
                }
                else
                {
                    title = "Mật khẩu cũ không khớp!";
                }
                resultEntry.Success = true;
                resultEntry.AddMessage(title);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }
        #endregion
    }
}