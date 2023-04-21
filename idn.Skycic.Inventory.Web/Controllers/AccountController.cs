using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.State;
using TError = idn.Skycic.Inventory.Errors;

namespace idn.Skycic.Inventory.Web.Controllers
{
    //[Authorize]
    public class AccountController : AdminBaseController
    {
        #region["Forget"]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Forget()
        {
            return View();
        }
        #endregion

        #region["Create"]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region["Login"]
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
            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var resultEntry = new JsonResultEntry() { Success = false };
            var orgID = "";
            try
            {
                var password = model.PasswordLogin;
                var decode = CUtils.Base64_Decode(password);

                password = CUtils.ReverseString(decode);
                model.PasswordLogin = password;

                System.Web.HttpContext.Current.Session["UserCodeLogin"] = model.UserCodeLogin;
                System.Web.HttpContext.Current.Session["PasswordLogin"] = model.PasswordLogin;
                
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
                    WAUserCode = model.UserCodeLogin,
                    WAUserPassword = model.PasswordLogin,
                    // RQ_MstSv_Sys_User
                    Rt_Cols_MstSv_Sys_User = null,
                    MstSv_Sys_User = null,
                };
                
                var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_GetAccessToken(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                
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
                        }
                        FormsAuthentication.SetAuthCookie(model.UserCodeLogin, false);
                        var dateStartMaHoaUserPass = DateTime.Now;

                        if (model.RememberMe)
                        {
                            var strDateTimeNow = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff").Trim();
                            // đảo ngược chuỗi
                            var usercode = CUtils.ReverseString(model.UserCodeLogin.Trim());
                            // mã hóa
                            var usercodeEncrypt = CUtils.Encrypt(usercode, true);
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
                            var passwordLogin = CUtils.ReverseString(strDateTimeNow + "_" + model.PasswordLogin);
                            // mã hóa
                            var passwordLoginEncrypt = CUtils.Encrypt(passwordLogin, true);
                            HttpCookie ck_password = new HttpCookie("_ibcckup")
                            {
                                //Value = model.PasswordLogin,
                                Value = passwordLoginEncrypt,
                                Expires = DateTime.Now.AddDays(1),
                                //HttpOnly = true,
                                //Secure = true,
                            };
                            Response.Cookies.Add(ck_password);
                        }
                        
                        #region["Danh sách org"]
                        
                        var tokenID = System.Web.HttpContext.Current.Session["TokenID"];
                        var baseServiceAddress = "";
                        var listOS_Inos_Org = new List<OS_Inos_Org>();
                        var objRQ_OS_Inos_Org = new RQ_OS_Inos_Org()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = tokenID.ToString().Trim(),
                            WAUserCode = waUserCode_MstSV,
                            WAUserPassword = waUserPassword_MstSV,
                            // RQ_OS_Inos_Org
                            Rt_Cols_OS_Inos_Org = "*",
                        };
                        var objRT_OS_Inos_Org = OS_Inos_OrgService.Instance.WA_OS_Inos_Org_GetMyOrgList(objRQ_OS_Inos_Org, addressMasterServerAPIs);
                        if (objRT_OS_Inos_Org.Lst_OS_Inos_Org != null && objRT_OS_Inos_Org.Lst_OS_Inos_Org.Count > 0)
                        {
                            listOS_Inos_Org.AddRange(objRT_OS_Inos_Org.Lst_OS_Inos_Org);
                        }
                        
                        #endregion
                        if (listOS_Inos_Org != null && listOS_Inos_Org.Count > 0)
                        {
                            var dateStartListOS_Inos_Org = DateTime.Now;
                            listOS_Inos_Org = (from item in objRT_OS_Inos_Org.Lst_OS_Inos_Org where (!CUtils.IsNullOrEmpty(item.ParentId) && item.ParentId.ToString().Trim().Equals("0")) select item).ToList();
                            var listOS_Inos_OrgSub = (from item in objRT_OS_Inos_Org.Lst_OS_Inos_Org where (!CUtils.IsNullOrEmpty(item.ParentId) && !item.ParentId.ToString().Trim().Equals("0")) select item).ToList();
                            if (listOS_Inos_OrgSub != null && listOS_Inos_OrgSub.Count > 0)
                            {
                                foreach (var item in listOS_Inos_OrgSub)
                                {
                                    var objOS_Inos_Org = listOS_Inos_Org.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.Id) && !CUtils.IsNullOrEmpty(item.ParentId) && it.Id.ToString().Trim().Equals(item.ParentId.ToString().Trim())));
                                    if (objOS_Inos_Org == null)
                                    {
                                        listOS_Inos_Org.Add(objOS_Inos_Org);
                                    }
                                }
                            }
                            
                            if (listOS_Inos_Org.Count == 1)
                            {
                                // Nếu item.ParentId.Equals("0") => NetworkID = item.Id
                                // Nếu !item.ParentId.Equals("0") => NetworkID = item.ParentId và OrgID = item.Id
                                var networkid = listOS_Inos_Org[0].ParentId.ToString().Trim().Equals("0") ? listOS_Inos_Org[0].Id.ToString().Trim() : listOS_Inos_Org[0].ParentId.ToString().Trim();
                                orgID = listOS_Inos_Org[0].Id.ToString().Trim();
                                #region[""]
                                var objRQ_MstSv_Sys_UserCur = new RQ_MstSv_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = waUserCode_MstSV,
                                    WAUserPassword = waUserPassword_MstSV,
                                    AccessToken = tokenID.ToString().Trim(),
                                    NetworkID = networkid,
                                    OrgID = orgID,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = "",
                                    FuncType = null,
                                };
                                var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_UserCur, addressMasterServerAPIs);
                                if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null && objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                                    objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                                {
                                    var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                                    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                                    {
                                        baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                                        //baseServiceAddress = "http://14.232.244.217:12088/idocNet.Test.InBrandCloud.WA/";
                                    }
                                    #region["Login Solution và Get thông tin user đăng nhập"]
                                    var objRQ_Sys_User = new RQ_Sys_User()
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
                                        WAUserCode = model.UserCodeLogin,
                                        WAUserPassword = model.PasswordLogin,
                                        AccessToken = tokenID.ToString().Trim(),
                                        NetworkID = networkid,
                                        OrgID = orgID,
                                        // RQ_Sys_User
                                        Rt_Cols_Sys_User = null,
                                        Rt_Cols_Sys_UserInGroup = null,
                                        Sys_User = null,
                                    };
                                    var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Login(objRQ_Sys_User, baseServiceAddress);
                                    if (objRT_Sys_User != null)
                                    {
                                        var strWhereClause = "";
                                        strWhereClause = strWhereClause_SysUser(model.UserCodeLogin);
                                        objRQ_Sys_User.Tid = GetNextTId();
                                        objRQ_Sys_User.Ft_WhereClause = strWhereClause;
                                        objRQ_Sys_User.Rt_Cols_Sys_User = "*";
                                        objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = "*";
                                        objRQ_Sys_User.Sys_User = null;
                                        var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_GetForCurrentUser(objRQ_Sys_User, baseServiceAddress);
                                        if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                                        {
                                            //objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword = objRQ_Sys_User.WAUserPassword;
                                            //objRT_Sys_UserCur.Lst_Sys_User[0].NetworkID = networkid;
                                            //var mst = CUtils.StrValue(objRT_Sys_UserCur.Lst_Sys_User[0].MST);
                                            //var userSate_Temp = new UserState(
                                            //    objRT_Sys_UserCur.Lst_Sys_User[0],
                                            //    new Mst_NNT()
                                            //    {
                                            //        NetworkID = objRQ_Sys_User.NetworkID,
                                            //        OrgID = objRQ_Sys_User.OrgID,
                                            //        MST = mst,
                                            //    },
                                            //    objRT_Sys_UserCur.Lst_Sys_Access)
                                            //{
                                            //    MST = mst,
                                            //    AddressAPIs = baseServiceAddress,
                                            //    TokenID = tokenID.ToString(),
                                            //};
                                            objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword = objRQ_Sys_User.WAUserPassword;
                                            objRT_Sys_UserCur.Lst_Sys_User[0].NetworkID = networkid;
                                            var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(new Mst_NNT() { MST = objRT_Sys_UserCur.Lst_Sys_User[0].MST });

                                            var objRQ_Mst_NNT = new RQ_Mst_NNT()
                                            {
                                                // WARQBase
                                                Tid = GetNextTId(),
                                                GwUserCode = GwUserCode,
                                                GwPassword = GwPassword,
                                                FuncType = null,
                                                Ft_RecordStart = Ft_RecordStart,
                                                Ft_RecordCount = Ft_RecordCount,
                                                Ft_WhereClause = strWhereClauseMst_NNT,
                                                Ft_Cols_Upd = null,
                                                WAUserCode = objRT_Sys_UserCur.Lst_Sys_User[0].UserCode,
                                                WAUserPassword = objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword,
                                                AccessToken = tokenID.ToString().Trim(),
                                                NetworkID = networkid,
                                                OrgID = orgID,
                                                // RQ_Mst_NNT
                                                Rt_Cols_Mst_NNT = "*",
                                                Mst_NNT = null,
                                            };

                                            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, baseServiceAddress);

                                            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
                                            {
                                                var userSate_Temp = new UserState(objRT_Sys_UserCur.Lst_Sys_User[0],
                                                    objRT_Mst_NNT.Lst_Mst_NNT[0], objRT_Sys_UserCur.Lst_Sys_Access)
                                                {
                                                    MST = objRT_Sys_UserCur.Lst_Sys_User[0].MST.ToString(),
                                                    AddressAPIs = baseServiceAddress,
                                                    TokenID = tokenID.ToString(),
                                                };
                                                this.UserState = userSate_Temp;
                                                #region["Login MasterServer ProductCentrer"]
                                                //var objRQ_MstSv_Sys_User_MasterServer_ProductCentrer = new RQ_MstSv_Sys_User()
                                                //{
                                                //    // WARQBase
                                                //    Tid = GetNextTId(),
                                                //    GwUserCode = OS_MasterServer_PrdCenter_GwUserCode,
                                                //    GwPassword = OS_MasterServer_PrdCenter_GwPassword,
                                                //    Ft_RecordStart = Ft_RecordStart,
                                                //    Ft_RecordCount = Ft_RecordCount,
                                                //    WAUserCode = OS_MasterServer_PrdCenter_WAUserCode,
                                                //    WAUserPassword = OS_MasterServer_PrdCenter_WAUserPassword,
                                                //    TokenID = OS_MasterServer_PrdCenter_TokenID,
                                                //    NetworkID = objRQ_MstSv_Sys_UserCur.NetworkID,
                                                //    //MstSv_Sys_User = new MstSv_Sys_User()
                                                //    //{
                                                //    //    //UserCode = OS_MasterServer_PrdCenter_UserCode,
                                                //    //    //UserPassword = OS_MasterServer_PrdCenter_UserPassword,
                                                //    //    UserCode = model.UserCodeLogin,
                                                //    //    UserPassword = model.PasswordLogin,
                                                //    //},
                                                //    // RQ_MstSv_Sys_User
                                                //    Rt_Cols_MstSv_Sys_User = "*",
                                                //};
                                                //var objRT_MstSv_Sys_User_MasterServer_ProductCentrer = MasterServerService.Instance.MasterServer_PrdCenter_WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User_MasterServer_ProductCentrer, OS_MasterServer_PrdCenter_API_Url);
                                                //if (objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys != null &&
                                                //    objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                                                //    objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                                                //{
                                                //    var c_K_DT_SysInfo_MasterServer_ProductCentrer = objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                                                //    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo_MasterServer_ProductCentrer.Remark))
                                                //    {
                                                //        userSate_Temp.AddressAPIs_ProductCentrer = CUtils.StrValueOrNull(c_K_DT_SysInfo_MasterServer_ProductCentrer.Remark);
                                                //    }
                                                //}

                                                #endregion
                                            }
                                            
                                        }
                                    }
                                    #endregion
                                }
                                System.Web.HttpContext.Current.Session["networkid"] = networkid;
                                System.Web.HttpContext.Current.Session["orgid"] = this.UserState.Mst_NNT.OrgID;
                                model.RedirectUrl = Url.ActionLocalized("Index", "Dashboard");
                                #endregion
                            }
                            else
                            {
                                var url = Url.ActionLocalized("index", "home");
                                model.RedirectUrl = url;
                            }
                        }
                        else
                        {
                            var url = Url.ActionLocalized("index", "home");
                            model.RedirectUrl = url;
                        }
                    }
                }
                else
                {

                    var url = Url.ActionLocalized("Login", "Account", new { returnUrl = "/" });
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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginOS(string orgid, string parentid)
        {
            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var baseServiceAddress = "";
            try
            {
                if (!CUtils.IsNullOrEmpty(orgid) && !CUtils.IsNullOrEmpty(parentid))
                {
                    var tokenID = System.Web.HttpContext.Current.Session["TokenID"];
                    var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode_MstSV,
                        WAUserPassword = waUserPassword_MstSV,
                        //TokenID = tokenID.ToString().Trim(),
                        NetworkID = parentid,
                        OrgID = orgid,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = "",
                        FuncType = null,
                    };
                    var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                    if (objRT_MstSv_Sys_User.c_K_DT_Sys != null && objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                        objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                    {
                        var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                        if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                        {
                            baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfo.Remark);
                            //baseServiceAddress = "http://14.232.244.217:12088/idocNet.Test.InBrandCloud.WA/";
                        }

                        #region["Login Solution và Get thông tin user đăng nhập"]
                        
                        var usercode = "";
                        if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["UserCodeLogin"]))
                        {
                            usercode = System.Web.HttpContext.Current.Session["UserCodeLogin"].ToString().Trim();
                        }
                        var password = "";
                        if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PasswordLogin"]))
                        {
                            password = System.Web.HttpContext.Current.Session["PasswordLogin"].ToString().Trim();
                        }

                        if (CUtils.IsNullOrEmpty(usercode) || CUtils.IsNullOrEmpty(password))
                        {
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
                        }

                        var objRQ_Sys_User = new RQ_Sys_User()
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
                            WAUserCode = usercode,
                            WAUserPassword = password,
                            AccessToken = tokenID.ToString().Trim(),
                            NetworkID = parentid,
                            OrgID = orgid,
                            // RQ_Sys_User
                            Rt_Cols_Sys_User = null,
                            Rt_Cols_Sys_UserInGroup = null,
                            Sys_User = null,
                        };
                        var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Login(objRQ_Sys_User, baseServiceAddress);
                        if (objRT_Sys_User != null)
                        {
                            var strWhereClause = "";
                            strWhereClause = strWhereClause_SysUser(usercode);
                            objRQ_Sys_User.Tid = GetNextTId();
                            objRQ_Sys_User.Ft_WhereClause = strWhereClause;
                            objRQ_Sys_User.Rt_Cols_Sys_User = "*";
                            objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = "*";
                            objRQ_Sys_User.Sys_User = null;
                            var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_GetForCurrentUser(objRQ_Sys_User, baseServiceAddress);
                            if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                            {
                                //objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword = password;
                                //objRT_Sys_UserCur.Lst_Sys_User[0].NetworkID = parentid;
                                //var mst = CUtils.StrValue(objRT_Sys_UserCur.Lst_Sys_User[0].MST);
                                //var userSate_Temp = new UserState(
                                //    objRT_Sys_UserCur.Lst_Sys_User[0], 
                                //    new Mst_NNT()
                                //    {
                                //        NetworkID = objRQ_Sys_User.NetworkID,
                                //        OrgID = objRQ_Sys_User.OrgID,
                                //        MST = mst,
                                //    },
                                //    objRT_Sys_UserCur.Lst_Sys_Access)
                                //{
                                //    MST = mst,
                                //    AddressAPIs = baseServiceAddress,
                                //    TokenID = tokenID.ToString(),
                                //};
                                objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword = objRQ_Sys_User.WAUserPassword;
                                objRT_Sys_UserCur.Lst_Sys_User[0].NetworkID = parentid;
                                var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(new Mst_NNT() { MST = objRT_Sys_UserCur.Lst_Sys_User[0].MST });

                                var objRQ_Mst_NNT = new RQ_Mst_NNT()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClauseMst_NNT,
                                    Ft_Cols_Upd = null,
                                    WAUserCode = objRT_Sys_UserCur.Lst_Sys_User[0].UserCode,
                                    WAUserPassword = objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword,
                                    AccessToken = tokenID.ToString().Trim(),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    // RQ_Mst_NNT
                                    Rt_Cols_Mst_NNT = "*",
                                    Mst_NNT = null,
                                };

                                var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, baseServiceAddress);

                                if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
                                {
                                    var userSate_Temp = new UserState(objRT_Sys_UserCur.Lst_Sys_User[0],
                                        objRT_Mst_NNT.Lst_Mst_NNT[0], objRT_Sys_UserCur.Lst_Sys_Access)
                                    {
                                        MST = objRT_Sys_UserCur.Lst_Sys_User[0].MST.ToString(),
                                        AddressAPIs = baseServiceAddress,
                                        TokenID = tokenID.ToString(),
                                    };
                                    this.UserState = userSate_Temp;
                                    #region["Login MasterServer ProductCentrer"]
                                    //var objRQ_MstSv_Sys_User_MasterServer_ProductCentrer = new RQ_MstSv_Sys_User()
                                    //{
                                    //    // WARQBase
                                    //    Tid = GetNextTId(),
                                    //    GwUserCode = OS_MasterServer_PrdCenter_GwUserCode,
                                    //    GwPassword = OS_MasterServer_PrdCenter_GwPassword,
                                    //    Ft_RecordStart = Ft_RecordStart,
                                    //    Ft_RecordCount = Ft_RecordCount,
                                    //    WAUserCode = OS_MasterServer_PrdCenter_WAUserCode,
                                    //    WAUserPassword = OS_MasterServer_PrdCenter_WAUserPassword,
                                    //    TokenID = OS_MasterServer_PrdCenter_TokenID,
                                    //    NetworkID = objRQ_MstSv_Sys_User.NetworkID,
                                    //    //MstSv_Sys_User = new MstSv_Sys_User()
                                    //    //{
                                    //    //    //UserCode = OS_MasterServer_PrdCenter_UserCode,
                                    //    //    //UserPassword = OS_MasterServer_PrdCenter_UserPassword,
                                    //    //    UserCode = usercode,
                                    //    //    UserPassword = password,
                                    //    //},
                                    //    // RQ_MstSv_Sys_User
                                    //    Rt_Cols_MstSv_Sys_User = "*",
                                    //};
                                    //var objRT_MstSv_Sys_User_MasterServer_ProductCentrer = MasterServerService.Instance.MasterServer_PrdCenter_WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User_MasterServer_ProductCentrer, OS_MasterServer_PrdCenter_API_Url);
                                    //if (objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys != null &&
                                    //    objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                                    //    objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                                    //{
                                    //    var c_K_DT_SysInfo_MasterServer_ProductCentrer = objRT_MstSv_Sys_User_MasterServer_ProductCentrer.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                                    //    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo_MasterServer_ProductCentrer.Remark))
                                    //    {
                                    //        userSate_Temp.AddressAPIs_ProductCentrer = CUtils.StrValueOrNull(c_K_DT_SysInfo_MasterServer_ProductCentrer.Remark);
                                    //    }
                                    //}

                                    #endregion
                                }

                            }
                        }
                        #endregion
                    }
                    System.Web.HttpContext.Current.Session["networkid"] = parentid;
                    System.Web.HttpContext.Current.Session["orgid"] = this.UserState.Mst_NNT.OrgID;
                    resultEntry.Success = true;
                    resultEntry.RedirectUrl = Url.ActionLocalized("Index", "Dashboard");
                    return Json(new { Success = true, RedirectUrl = resultEntry.RedirectUrl });
                }
                else
                {
                    var requestRawUrl = "/";
                    var redirectUrl = Url.Action("Login", "Account", new { redirectUrl = requestRawUrl });
                    return Redirect(redirectUrl);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            resultEntry.IsServiceException = true;
            return Json(new { Success = false, Detail = resultEntry.Detail });
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
            if (Request.Cookies["MyCookie"] != null)
            {
                var c = new HttpCookie("MyCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            if (Request.Cookies["_ibcckuc"] != null)
            {
                var ckUserCode = new HttpCookie("_ibcckuc") {Expires = DateTime.Now.AddDays(-1)};
                Response.Cookies.Add(ckUserCode);
            }
            if (Request.Cookies["_ibcckup"] != null)
            {
                var ckPassword = new HttpCookie("_ibcckup") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(ckPassword);
            }
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region["LogOff"]
        // POST: /Account/LogOff
        public ActionResult LogOffNoLoginOS()
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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                var title = "";
                pass = CUtils.StrValueOrNull(pass);
                pass = CUtils.GetEncodedHash(pass);
                if (pass.Equals(waUserPassword))
                {
                    if (!CUtils.IsNullOrEmpty(passnew))
                    {
                        passnew = CUtils.StrValueOrNull(passnew);
                        if (!CUtils.IsNullOrEmpty(confpass))
                        {
                            confpass = CUtils.StrValueOrNull(confpass);
                            if (passnew.Equals(confpass))
                            {
                                var strWhereClause = "";
                                strWhereClause = strWhereClause_SysUser(userState.SysUser.UserCode);

                                var objRQ_Sys_User = new RQ_Sys_User()
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
                                    // RQ_Sys_User
                                    Rt_Cols_Sys_User = "*",
                                    Rt_Cols_Sys_UserInGroup = null,
                                    Sys_User = null,
                                };

                                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                                {
                                    objRT_Sys_UserCur.Lst_Sys_User[0].UserPasswordNew = CUtils.GetEncodedHash(passnew);
                                    
                                    objRQ_Sys_User.Ft_WhereClause = null;
                                    objRQ_Sys_User.Ft_Cols_Upd = null;
                                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                                    objRQ_Sys_User.Sys_User = objRT_Sys_UserCur.Lst_Sys_User[0];
                                    Sys_UserService.Instance.WA_Sys_User_ChangePassword(objRQ_Sys_User, addressAPIs);
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

        #region["strWhereClause"]
        private string strWhereClause_SysUser(string usercode)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(usercode))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserCode, usercode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, data.MST.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}