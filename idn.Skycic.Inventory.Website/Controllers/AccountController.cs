using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Extensions;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.State;
using inos.common.Model;
using TError = idn.Skycic.Inventory.Errors;

namespace idn.Skycic.Inventory.Website.Controllers
{
    //[Authorize]
    public class AccountController : BaseController
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
        public ActionResult Login()
        {
            InitSSO();
            var abc = User.Identity;
            var loginCallback = Url.Action("LoginCallback");
            loginCallback = loginCallback.IndexOf("//", StringComparison.Ordinal) == 0 ? loginCallback.Replace("//", "/") : loginCallback;
            string callbackUrl = string.Format("{0}{1}", GetRequestBaseAddress(), loginCallback);

            var accSv = new inos.common.Service.AccountService(Session);
            var userAuthorization = accSv.PrepareRequestUserAuthorization(callbackUrl);
            userAuthorization.Send(HttpContext);
            Response.End();
            return null;
        }

        [AllowAnonymous]
        public async Task<ActionResult> LoginCallback(string id)
        {
            double utcOffset = 7;
            InitSSO();
            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var networkID = "";
            var orgID = "";
            var accSv = new inos.common.Service.AccountService(Session);
            var authorizationState = accSv.ProcessUserAuthorization(Request);
            var url = Url.ActionLocalized("index", "home");
            if (authorizationState != null)
            {
                var accessToken = authorizationState.AccessToken;
                var refreshToken = authorizationState.RefreshToken;

                accSv.AccessToken = accessToken;
                //try
                //{
                //    var inosuser1 = accSv.GetCurrentUser();
                //    //throw new Exception(inos.common.Paths.AuthorizationServerBaseAddress)
                //    //{

                //    //};
                //}
                //catch (Exception e)
                //{
                //    throw new Exception(string.Format("{0}", inos.common.Paths.AuthorizationServerBaseAddress));
                //    {

                //    };
                //    //throw new Exception(accessToken)
                //    //{

                //    //};
                //}
                var inosuser = accSv.GetCurrentUser();
                if (inosuser != null)
                {
                    if (!CUtils.IsNullOrEmpty(inosuser.TimeZone))
                    {
                        utcOffset = Convert.ToDouble(inosuser.TimeZone);
                    }
                    System.Web.HttpContext.Current.Session["AccessToken"] = accessToken;
                    FormsAuthentication.SetAuthCookie(inosuser.Email, false);

                    #region["Mã hóa AccessToken và lưu vào cookie (đảo ngược chuỗi token, mã hóa chuỗi đảo token đảo ngược)"]
                    var accessTokenReverse = CUtils.ReverseString(accessToken);
                    var accessTokenEncrypt = CUtils.Encrypt(accessTokenReverse);
                    HttpCookie accessTokenCookie = new HttpCookie("accesstoken")
                    {
                        Value = accessTokenEncrypt,
                        Expires = DateTime.Now.AddDays(1),
                        //HttpOnly = true,
                        //Secure = true,
                        ////Shareable = 
                    };
                    Response.Cookies.Add(accessTokenCookie);
                    var refreshTokenReverse = CUtils.ReverseString(refreshToken);
                    var refreshTokenEncrypt = CUtils.Encrypt(refreshTokenReverse);
                    HttpCookie refreshTokenCookie = new HttpCookie("refreshtoken")
                    {
                        Value = refreshTokenEncrypt,
                        Expires = DateTime.Now.AddDays(1),
                        //HttpOnly = true,
                        //Secure = true,
                        ////Shareable = 
                    };
                    Response.Cookies.Add(refreshTokenCookie);
                    #endregion

                    #region["Danh sách org"]

                    var baseServiceAddress = "";
                    var listOS_Inos_Org = new List<OS_Inos_Org>();
                    var objRQ_OS_Inos_Org = new RQ_OS_Inos_Org()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = accessToken,
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
                            networkID = listOS_Inos_Org[0].ParentId.ToString().Trim().Equals("0") ? listOS_Inos_Org[0].Id.ToString().Trim() : listOS_Inos_Org[0].ParentId.ToString().Trim();
                            orgID = listOS_Inos_Org[0].Id.ToString().Trim();

                            #region[Solution được dùng]
                            var licService = new inos.common.Service.LicService(null);
                            licService.AccessToken = CUtils.StrValue(accessToken);
                            var getCurrentUserSolutions = licService.GetCurrentUserSolutions(Convert.ToInt64(orgID));

                            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(getCurrentUserSolutions);

                            var lstOrgSolution = new List<OrgSolution>();
                            if (getCurrentUserSolutions != null && getCurrentUserSolutions.Count > 0)
                            {
                                foreach (var item in getCurrentUserSolutions)
                                {
                                    var check = LoadDataController.CheckSolutionCodeNotShow(item.SolutionCode);
                                    if (check)
                                    {
                                        var exist = lstOrgSolution.Where(m => m.SolutionCode.Equals(item.SolutionCode)).ToList();

                                        if (exist.Count == 0)
                                        {
                                            var solutionimagenavbar = "/Images/no-image.png";
                                            var solutionimageslidebar = "/Images/no-image.png";
                                            var solutionname = "not defined";
                                            LoadDataController.GetInfoSolutionCloud(item.SolutionCode, ref solutionname, ref solutionimageslidebar, ref solutionimagenavbar);
                                            item.Name = solutionname;

                                            lstOrgSolution.Add(item);
                                        }
                                    }
                                }
                            }

                            System.Web.HttpContext.Current.Session["AllSolutions"] = lstOrgSolution;
                            #endregion

                            #region[Solution chưa được dùng]
                            var allsolutions = licService.GetAllSolutions();
                            var solutionsnotused = LoadDataController.GetSolutionsNotUsed(allsolutions, lstOrgSolution);

                            System.Web.HttpContext.Current.Session["AllSolutionsNotUsed"] = solutionsnotused;
                            #endregion


                            #region[""]
                            var objRQ_MstSv_Sys_UserCur = new RQ_MstSv_Sys_User()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                WAUserCode = waUserCode_MstSV,
                                WAUserPassword = waUserPassword_MstSV,
                                AccessToken = accessToken,
                                NetworkID = networkID,
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
                                }
                                #region["Get thông tin user đăng nhập"]
                                var strWhereClause = "";
                                strWhereClause = strWhereClause_SysUser(inosuser.Email);
                                var objRQ_Sys_User = new RQ_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = inosuser.Email,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = networkID,
                                    OrgID = orgID,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = "1",
                                    Ft_WhereClause = strWhereClause,
                                    Ft_Cols_Upd = null,
                                    // RQ_Sys_User
                                    Rt_Cols_Sys_User = null,
                                    Rt_Cols_Sys_UserInGroup = null,
                                    Sys_User = null,
                                };
                                var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_GetForCurrentUser(objRQ_Sys_User, baseServiceAddress);
                                if (objRT_Sys_User.Lst_Sys_User != null && objRT_Sys_User.Lst_Sys_User.Count > 0)
                                {
                                    var objMst_NNT = new Mst_NNT();
                                    var objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
                                    var apiMasterServerVeloca = "";
                                    var apiMasterServerDMS = "";
                                    var apiMasterServerLQDMS = "";
                                    var apiMasterServerProduct_Customer_Center = "";
                                    #region["Lấy thông tin người nộp thuế và cấu hình hệ thống"]
                                    #region["Mst_NNT"]
                                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(new Mst_NNT() { MST = objRT_Sys_User.Lst_Sys_User[0].MST });
                                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = GwUserCode,
                                        GwPassword = GwPassword,
                                        WAUserCode = inosuser.Email,
                                        AccessToken = CUtils.StrValue(accessToken),
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = "1",
                                        Ft_WhereClause = strWhereClauseMst_NNT,
                                        // RQ_Mst_NNT
                                        Rt_Cols_Mst_NNT = "*",
                                        Mst_NNT = null,
                                    };
                                    var list_Mst_NNT_Login_Async = List_Mst_NNT_Login_Async(objRQ_Mst_NNT, baseServiceAddress);
                                    #endregion
                                    #region["Cấu hình hệ thống"]
                                    var strWhereClauseMst_Sys_Config = strWhereClause_Mst_Sys_Config(new Mst_Sys_Config() { NetworkID = networkID });
                                    var objRQ_Mst_Sys_Config = new RQ_Mst_Sys_Config()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = GwUserCode,
                                        GwPassword = GwPassword,
                                        WAUserCode = inosuser.Email,
                                        AccessToken = CUtils.StrValue(accessToken),
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = strWhereClauseMst_Sys_Config,
                                        // RQ_Mst_Sys_Config
                                        Rt_Cols_Mst_Sys_Config = "*",
                                    };
                                    var objRT_Mst_Sys_Config_Login_Async = RT_Mst_Sys_Config_Login_Async(objRQ_Mst_Sys_Config, baseServiceAddress);
                                    #endregion
                                    #region["API Master Server Veloca"]
                                    var objRQ_MstSv_Sys_User_VelocaCur = new RQ_MstSv_Sys_User()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_Veloca_MasterServer_GwUserCode,
                                        GwPassword = OS_Veloca_MasterServer_GwPassword,
                                        WAUserCode = OS_Veloca_MasterServer_WAUserCode,
                                        WAUserPassword = OS_Veloca_MasterServer_WAUserPassword,
                                        AccessToken = accessToken,
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = "",
                                        FuncType = null,
                                    };
                                    var returnAPIMasterServerVeloca_Async =
                                        ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_VelocaCur,
                                            ServiceAddress.BaseMasterServerVelocaAPIAddress);
                                    #endregion
                                    #region["API Master Server DMS"]
                                    var objRQ_MstSv_Sys_User_DMSCur = new RQ_MstSv_Sys_User()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_DMS_MasterServer_GwUserCode,
                                        GwPassword = OS_DMS_MasterServer_GwPassword,
                                        WAUserCode = OS_DMS_MasterServer_WAUserCode,
                                        WAUserPassword = OS_DMS_MasterServer_WAUserPassword,
                                        AccessToken = CUtils.StrValue(accessToken),
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = "",
                                        FuncType = null,
                                    };
                                    var returnAPIMasterServerDMS_Async =
                                        ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_DMSCur,
                                            ServiceAddress.BaseMasterServerDMSAPIAddress);
                                    #endregion
                                    #region["API Master Server LQDMS"]
                                    var objRQ_MstSv_Sys_User_LQDMSCur = new RQ_MstSv_Sys_User()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = GwUserCodeLQDMS,//"idocNet.idn.Skycic.DMS.Sv",
                                        GwPassword = GwPasswordLQDMS,//"idocNet.idn.Skycic.DMS.Sv",
                                        WAUserCode = WAUserCode_LQDMS_BG,//"ENDUSER",
                                        WAUserPassword = WAUserPassword_LQDMS_BG,//"123456",
                                        AccessToken = "7",
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = "",
                                        FuncType = null,
                                    };
                                    var returnAPIMasterServerLQDMS_Async =
                                        ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_LQDMSCur,
                                            ServiceAddress.BaseMasterServerLQDMSAPIAddress);
                                    #endregion
                                    #region["API Master Server Product Customer Center"]
                                    var objRQ_MstSv_Sys_User_Product_Customer_CenterCur = new RQ_MstSv_Sys_User()
                                    {
                                        // WARQBase
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                                        GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                                        WAUserCode = OS_ProductCentrer_MasterServer_WAUserCode,
                                        WAUserPassword = OS_ProductCentrer_MasterServer_WAUserPassword,
                                        AccessToken = accessToken,
                                        NetworkID = networkID,
                                        OrgID = orgID,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = "",
                                        FuncType = null,
                                    };
                                    var returnAPIMasterServerProduct_Customer_Center_Async =
                                        ReturnAPIMasterServerProduct_Customer_Center_Async(objRQ_MstSv_Sys_User_Product_Customer_CenterCur,
                                            ServiceAddress.BaseMasterServerProduct_Customer_CenterAPIAddress);
                                    #endregion
                                    await Task.WhenAll(list_Mst_NNT_Login_Async, objRT_Mst_Sys_Config_Login_Async, returnAPIMasterServerProduct_Customer_Center_Async, returnAPIMasterServerVeloca_Async, returnAPIMasterServerDMS_Async);
                                    if (list_Mst_NNT_Login_Async != null && list_Mst_NNT_Login_Async.Result != null &&
                                        list_Mst_NNT_Login_Async.Result.Count > 0)
                                    {
                                        objMst_NNT = list_Mst_NNT_Login_Async.Result[0];
                                    }
                                    if (objRT_Mst_Sys_Config_Login_Async != null &&
                                        objRT_Mst_Sys_Config_Login_Async.Result != null)
                                    {
                                        objRT_Mst_Sys_Config = objRT_Mst_Sys_Config_Login_Async.Result;
                                    }

                                    if (returnAPIMasterServerVeloca_Async != null &&
                                        returnAPIMasterServerVeloca_Async.Result != null)
                                    {
                                        apiMasterServerVeloca = returnAPIMasterServerVeloca_Async.Result;
                                    }
                                    if (returnAPIMasterServerDMS_Async != null &&
                                        returnAPIMasterServerDMS_Async.Result != null)
                                    {
                                        apiMasterServerDMS = returnAPIMasterServerDMS_Async.Result;
                                    }
                                    if (returnAPIMasterServerProduct_Customer_Center_Async != null &&
                                        returnAPIMasterServerProduct_Customer_Center_Async.Result != null)
                                    {
                                        apiMasterServerProduct_Customer_Center = returnAPIMasterServerProduct_Customer_Center_Async.Result;
                                    }
                                    //if (returnAPIMasterServerLQDMS_Async != null && returnAPIMasterServerLQDMS_Async.Result != null)
                                    //{
                                    //    apiMasterServerLQDMS = returnAPIMasterServerLQDMS_Async.Result;
                                    //}
                                    #endregion
                                    //var userSate_Temp = new UserState(objRT_Sys_User.Lst_Sys_User[0], inosuser,
                                    //    objMst_NNT, objRT_Mst_Sys_Config, objRT_Sys_User.Lst_Sys_Access)
                                    //{
                                    //    MST = objRT_Sys_User.Lst_Sys_User[0].MST.ToString(),
                                    //    AddressAPIs = baseServiceAddress,
                                    //    AccessToken = CUtils.StrValue(accessToken),
                                    //    UtcOffset = 7,
                                    //    AddressAPIs_Product_Customer_Centrer = apiMasterServerProduct_Customer_Center,
                                    //    AddressAPIs_SkycicInventory = apiMasterServerInventory,
                                    //};

                                    var userSate_Temp = new UserState(objRT_Sys_User.Lst_Sys_User[0], inosuser, 
                                        objMst_NNT, objRT_Mst_Sys_Config, objRT_Sys_User.Lst_Sys_Access)
                                    {
                                        MST = objRT_Sys_User.Lst_Sys_User[0].MST.ToString(),
                                        AddressAPIs = baseServiceAddress,
                                        AccessToken = CUtils.StrValue(accessToken),
                                        UtcOffset = utcOffset,
                                        AddressAPIs_Product_Customer_Centrer = apiMasterServerProduct_Customer_Center,
                                        AddressAPIs_SkycicVeloca = apiMasterServerVeloca,
                                        AddressAPIs_SkycicDMS = apiMasterServerDMS,
                                        AddressAPIs_SkycicLQDMS = apiMasterServerLQDMS,
                                        ListOrgSolution = lstOrgSolution
                                    };
                                    this.UserState = userSate_Temp;
                                }
                                #endregion
                            }
                            System.Web.HttpContext.Current.Session["networkid"] = networkID;
                            var urlCur = Url.ActionLocalized("Index", "Dashboard");
                            return Redirect(urlCur);
                            #endregion

                        }
                        else
                        {
                            Response.Redirect(url, false);
                            //return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        Response.Redirect(url, false);
                        //var url = Url.ActionLocalized("index", "home");
                        //return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Response.Redirect(url, false);
                    //return RedirectToAction("Index", "Home");
                }
            }
            return Json("invalid", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginOS(string orgid, string parentid)
        {
            double utcOffset = 7;
            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var baseServiceAddress = "";
            try
            {
                if (!CUtils.IsNullOrEmpty(orgid) && !CUtils.IsNullOrEmpty(parentid))
                {
                    InitSSO();
                    var accessToken = System.Web.HttpContext.Current.Session["AccessToken"];

                    #region[Solution được dùng]
                    var licService = new inos.common.Service.LicService(null);
                    licService.AccessToken = CUtils.StrValue(accessToken);
                    var getCurrentUserSolutions = licService.GetCurrentUserSolutions(Convert.ToInt64(orgid));

                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(getCurrentUserSolutions);

                    var lstOrgSolution = new List<OrgSolution>();
                    if (getCurrentUserSolutions != null && getCurrentUserSolutions.Count > 0)
                    {
                        foreach (var item in getCurrentUserSolutions)
                        {
                            var check = LoadDataController.CheckSolutionCodeNotShow(item.SolutionCode);
                            if (check)
                            {
                                var exist = lstOrgSolution.Where(m => m.SolutionCode.Equals(item.SolutionCode)).ToList();

                                if (exist.Count == 0)
                                {
                                    var solutionimagenavbar = "/Images/no-image.png";
                                    var solutionimageslidebar = "/Images/no-image.png";
                                    var solutionname = "not defined";
                                    LoadDataController.GetInfoSolutionCloud(item.SolutionCode, ref solutionname, ref solutionimageslidebar, ref solutionimagenavbar);
                                    item.Name = solutionname;

                                    lstOrgSolution.Add(item);
                                }
                            }
                        }
                    }

                    System.Web.HttpContext.Current.Session["AllSolutions"] = lstOrgSolution;
                    #endregion

                    #region[Solution chưa được dùng]
                    var allsolutions = licService.GetAllSolutions();
                    var solutionsnotused = LoadDataController.GetSolutionsNotUsed(allsolutions, lstOrgSolution);

                    System.Web.HttpContext.Current.Session["AllSolutionsNotUsed"] = solutionsnotused;
                    #endregion

                    var accSv = new inos.common.Service.AccountService(null);
                    
                    if (CUtils.IsNullOrEmpty(accessToken))
                    {
                        if (Request.Cookies["accesstoken"] != null)
                        {
                            HttpCookie ck_tokenid = Request.Cookies["accesstoken"];
                            if (ck_tokenid != null)
                            {
                                var accessTokenEncrypt = ck_tokenid.Value;
                                var cktokenid = ck_tokenid.Value;
                                if (!CUtils.IsNullOrEmpty(accessTokenEncrypt))
                                {
                                    var accessTokenDecrypt = CUtils.Decrypt(accessTokenEncrypt);
                                    accessToken = CUtils.ReverseString(accessTokenDecrypt);
                                }
                            }
                        }
                    }
                    
                    accSv.AccessToken = CUtils.StrValue(accessToken);
                    var inosuser = accSv.GetCurrentUser();
                    if (inosuser != null)
                    {
                        if (!CUtils.IsNullOrEmpty(inosuser.TimeZone))
                        {
                            utcOffset = Convert.ToDouble(inosuser.TimeZone);
                        }

                        var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = accessToken.ToString().Trim(),
                            WAUserCode = waUserCode_MstSV,
                            WAUserPassword = waUserPassword_MstSV,
                            NetworkID = parentid,
                            OrgID = orgid,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = "",
                        };

                        var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                        if (objRT_MstSv_Sys_User.c_K_DT_Sys != null && objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                            objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                            if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                            {
                                baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfo.Remark);
                            }
                            #region["Get thông tin user đăng nhập"]
                            var strWhereClause = "";
                            strWhereClause = strWhereClause_SysUser(inosuser.Email);
                            var objRQ_Sys_User = new RQ_Sys_User()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                WAUserCode = inosuser.Email,
                                AccessToken = CUtils.StrValue(accessToken),
                                NetworkID = parentid,
                                OrgID = orgid,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = "1",
                                Ft_WhereClause = strWhereClause,
                                Ft_Cols_Upd = null,
                                // RQ_Sys_User
                                Rt_Cols_Sys_User = null,
                                Rt_Cols_Sys_UserInGroup = null,
                                Sys_User = null,
                            };
                            var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_GetForCurrentUser(objRQ_Sys_User, baseServiceAddress);
                            if (objRT_Sys_User.Lst_Sys_User != null && objRT_Sys_User.Lst_Sys_User.Count > 0)
                            {
                                var objMst_NNT = new Mst_NNT();
                                var objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
                                var apiMasterServerVeloca = "";
                                var apiMasterServerDMS = "";
                                var apiMasterServerLQDMS = "";
                                var apiMasterServerProduct_Customer_Center = "";
                                #region["Lấy thông tin người nộp thuế và cấu hình hệ thống"]
                                #region["Mst_NNT"]
                                var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(new Mst_NNT() { MST = objRT_Sys_User.Lst_Sys_User[0].MST });
                                var objRQ_Mst_NNT = new RQ_Mst_NNT()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = inosuser.Email,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = "1",
                                    Ft_WhereClause = strWhereClauseMst_NNT,
                                    // RQ_Mst_NNT
                                    Rt_Cols_Mst_NNT = "*",
                                    Mst_NNT = null,
                                };
                                var list_Mst_NNT_Login_Async = List_Mst_NNT_Login_Async(objRQ_Mst_NNT, baseServiceAddress);
                                #endregion
                                #region["Cấu hình hệ thống"]
                                var strWhereClauseMst_Sys_Config = strWhereClause_Mst_Sys_Config(new Mst_Sys_Config() { NetworkID = parentid });
                                var objRQ_Mst_Sys_Config = new RQ_Mst_Sys_Config()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = inosuser.Email,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClauseMst_Sys_Config,
                                    // RQ_Mst_Sys_Config
                                    Rt_Cols_Mst_Sys_Config = "*",
                                };
                                var objRT_Mst_Sys_Config_Login_Async = RT_Mst_Sys_Config_Login_Async(objRQ_Mst_Sys_Config, baseServiceAddress);
                                #endregion
                                #region["API Master Server Veloca"]
                                var objRQ_MstSv_Sys_User_VelocaCur = new RQ_MstSv_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_Veloca_MasterServer_GwUserCode,
                                    GwPassword = OS_Veloca_MasterServer_GwPassword,
                                    WAUserCode = OS_Veloca_MasterServer_WAUserCode,
                                    WAUserPassword = OS_Veloca_MasterServer_WAUserPassword,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = "",
                                    FuncType = null,
                                };
                                var returnAPIMasterServerVeloca_Async =
                                    ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_VelocaCur,
                                        ServiceAddress.BaseMasterServerVelocaAPIAddress);
                                #endregion
                                #region["API Master Server DMS"]
                                var objRQ_MstSv_Sys_User_DMSCur = new RQ_MstSv_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_DMS_MasterServer_GwUserCode,
                                    GwPassword = OS_DMS_MasterServer_GwPassword,
                                    WAUserCode = OS_DMS_MasterServer_WAUserCode,
                                    WAUserPassword = OS_DMS_MasterServer_WAUserPassword,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = "",
                                    FuncType = null,
                                };
                                var returnAPIMasterServerDMS_Async =
                                    ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_DMSCur,
                                        ServiceAddress.BaseMasterServerDMSAPIAddress);
                                #endregion
                                #region["API Master Server LQDMS"]
                                var objRQ_MstSv_Sys_User_LQDMSCur = new RQ_MstSv_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = "idocNet.idn.Skycic.DMS.Sv",//OS_LQDMS_MasterServer_GwUserCode,
                                    GwPassword = "idocNet.idn.Skycic.DMS.Sv",//OS_LQDMS_MasterServer_GwPassword,
                                    WAUserCode = "ENDUSER",//OS_LQDMS_MasterServer_WAUserCode,
                                    WAUserPassword = "123456",//OS_LQDMS_MasterServer_WAUserPassword,
                                    AccessToken = "7",
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = "",
                                    FuncType = null,
                                };
                                var returnAPIMasterServerLQDMS_Async =
                                    ReturnAPIMasterServer_Async(objRQ_MstSv_Sys_User_LQDMSCur,
                                        ServiceAddress.BaseMasterServerLQDMSAPIAddress);
                                #endregion
                                #region["API Master Server Product Customer Center"]
                                var objRQ_MstSv_Sys_User_Product_Customer_CenterCur = new RQ_MstSv_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                                    GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                                    WAUserCode = OS_ProductCentrer_MasterServer_WAUserCode,
                                    WAUserPassword = OS_ProductCentrer_MasterServer_WAUserPassword,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    NetworkID = parentid,
                                    OrgID = orgid,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = "",
                                    FuncType = null,
                                };
                                var returnAPIMasterServerProduct_Customer_Center_Async =
                                    ReturnAPIMasterServerProduct_Customer_Center_Async(objRQ_MstSv_Sys_User_Product_Customer_CenterCur,
                                        ServiceAddress.BaseMasterServerProduct_Customer_CenterAPIAddress);
                                #endregion
                                await Task.WhenAll(list_Mst_NNT_Login_Async, objRT_Mst_Sys_Config_Login_Async, returnAPIMasterServerProduct_Customer_Center_Async, returnAPIMasterServerVeloca_Async, returnAPIMasterServerDMS_Async);
                                if (list_Mst_NNT_Login_Async != null && list_Mst_NNT_Login_Async.Result != null &&
                                    list_Mst_NNT_Login_Async.Result.Count > 0)
                                {
                                    objMst_NNT = list_Mst_NNT_Login_Async.Result[0];
                                }
                                if (objRT_Mst_Sys_Config_Login_Async != null &&
                                    objRT_Mst_Sys_Config_Login_Async.Result != null)
                                {
                                    objRT_Mst_Sys_Config = objRT_Mst_Sys_Config_Login_Async.Result;
                                }
                                if (returnAPIMasterServerVeloca_Async != null &&
                                    returnAPIMasterServerVeloca_Async.Result != null)
                                {
                                    apiMasterServerVeloca = returnAPIMasterServerVeloca_Async.Result;
                                }
                                if (returnAPIMasterServerDMS_Async != null &&
                                    returnAPIMasterServerDMS_Async.Result != null)
                                {
                                    apiMasterServerDMS = returnAPIMasterServerDMS_Async.Result;
                                }
                                if (returnAPIMasterServerProduct_Customer_Center_Async != null &&
                                    returnAPIMasterServerProduct_Customer_Center_Async.Result != null)
                                {
                                    apiMasterServerProduct_Customer_Center = returnAPIMasterServerProduct_Customer_Center_Async.Result;
                                }
                                //if (returnAPIMasterServerLQDMS_Async != null && returnAPIMasterServerLQDMS_Async.Result != null)
                                //{
                                //    apiMasterServerLQDMS = returnAPIMasterServerLQDMS_Async.Result;
                                //}
                                #endregion
                                //var userSate_Temp = new UserState(objRT_Sys_User.Lst_Sys_User[0], inosuser,
                                //    objMst_NNT, objRT_Mst_Sys_Config, objRT_Sys_User.Lst_Sys_Access)
                                //{
                                //    MST = objRT_Sys_User.Lst_Sys_User[0].MST.ToString(),
                                //    AddressAPIs = baseServiceAddress,
                                //    AccessToken = CUtils.StrValue(accessToken),
                                //    UtcOffset = 7,
                                //    AddressAPIs_Product_Customer_Centrer = apiMasterServerProduct_Customer_Center,
                                //    AddressAPIs_SkycicInventory = apiMasterServerInventory,
                                //};
                                var userSate_Temp = new UserState(objRT_Sys_User.Lst_Sys_User[0], inosuser,
                                    objMst_NNT, objRT_Mst_Sys_Config, objRT_Sys_User.Lst_Sys_Access)
                                {
                                    MST = objRT_Sys_User.Lst_Sys_User[0].MST.ToString(),
                                    AddressAPIs = baseServiceAddress,
                                    AccessToken = CUtils.StrValue(accessToken),
                                    UtcOffset = utcOffset,
                                    AddressAPIs_Product_Customer_Centrer = apiMasterServerProduct_Customer_Center,
                                    AddressAPIs_SkycicVeloca = apiMasterServerVeloca,
                                    AddressAPIs_SkycicDMS = apiMasterServerDMS,
                                    AddressAPIs_SkycicLQDMS = apiMasterServerLQDMS,
                                    ListOrgSolution = lstOrgSolution
                                };
                                this.UserState = userSate_Temp;
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
                else
                {
                    FormsAuthentication.SignOut();
                    var redirectUrl = RedirectActionToLogin();
                    return Redirect(redirectUrl);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
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
            Session.Clear();
            InitSSO();
            Response.Cookies.Clear();
            
            FormsAuthentication.SignOut();
            if (Request.Cookies["accesstoken"] != null)
            {
                var ckAccessToken = new HttpCookie("accesstoken") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(ckAccessToken);
            }
            var cookie = new HttpCookie("ASP.NET_SessionId") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(cookie);
            Session.Clear();
            string url = inos.common.Paths.GetLogoffUrl();
            Response.Redirect(url, true);
            Response.End();
            return null;
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            Session.Clear();
            
            FormsAuthentication.SignOut();
            return null;
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
                resultEntry.SetFailed().AddException(ex);
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


        private string strWhereClause_Mst_Sys_Config(Mst_Sys_Config data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Sys_Config = TableName.Mst_Sys_Config + ".";
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Sys_Config + TblMst_Sys_Config.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}