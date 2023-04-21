using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Extensions;
using idn.Skycic.Inventory.Website.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                double utcOffset = 7;
                var waUserCode_MstSV = WAUserCode_MstSV;
                var waUserPassword_MstSV = WAUserPassword_MstSV;
                var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
                var accessToken = System.Web.HttpContext.Current.Session["AccessToken"];
                var networkID = "";
                var orgID = "";
               

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

                if (!CUtils.IsNullOrEmpty(accessToken))
                {
                    InitSSO();
                    var accSv = new inos.common.Service.AccountService(Session);
                    var inosuser = accSv.GetCurrentUser();
                    utcOffset = Convert.ToDouble(inosuser.TimeZone);
                    var baseServiceAddress = "";
                    var listOS_Inos_Org = new List<OS_Inos_Org>();
                    System.Web.HttpContext.Current.Session["AccessToken"] = accessToken;
                    var rawUrl = Request.RawUrl.ToLower().Trim();
                    if (!rawUrl.Equals("/"))
                    {
                        var url = "/";
                        return Redirect(url);
                    }
                    var objRQ_OS_Inos_Org = new RQ_OS_Inos_Org()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = accessToken.ToString().Trim(),
                        WAUserCode = waUserCode_MstSV,
                        WAUserPassword = waUserPassword_MstSV,
                        // RQ_OS_Inos_Org
                        Rt_Cols_OS_Inos_Org = "*",
                    };
                    var objRT_OS_Inos_Org = OS_Inos_OrgService.Instance.WA_OS_Inos_Org_GetMyOrgList(objRQ_OS_Inos_Org, addressMasterServerAPIs);
                    if (objRT_OS_Inos_Org.Lst_OS_Inos_Org != null && objRT_OS_Inos_Org.Lst_OS_Inos_Org.Count > 0)
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
                            #region[""]
                            var objRQ_MstSv_Sys_UserCur = new RQ_MstSv_Sys_User()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                WAUserCode = waUserCode_MstSV,
                                WAUserPassword = waUserPassword_MstSV,
                                AccessToken = CUtils.StrValue(accessToken),
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
                                    //var apiMasterServerInventory = "";
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
                                        Ft_RecordCount = "1",
                                        Ft_WhereClause = strWhereClauseMst_Sys_Config,
                                        // RQ_Mst_Sys_Config
                                        Rt_Cols_Mst_Sys_Config = "*",
                                        //Rt_Cols_Mst_Sys_ConfigDtl = "*",
                                    };
                                    var objRT_Mst_Sys_Config_Login_Async = RT_Mst_Sys_Config_Login_Async(objRQ_Mst_Sys_Config, baseServiceAddress);
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
                                    
                                    if (list_Mst_NNT_Login_Async != null && list_Mst_NNT_Login_Async.Result != null &&
                                        list_Mst_NNT_Login_Async.Result.Count > 0)
                                    {
                                        objMst_NNT = list_Mst_NNT_Login_Async.Result[0];
                                    }
                                    
                                    if (returnAPIMasterServerProduct_Customer_Center_Async != null &&
                                        returnAPIMasterServerProduct_Customer_Center_Async.Result != null)
                                    {
                                        apiMasterServerProduct_Customer_Center = returnAPIMasterServerProduct_Customer_Center_Async.Result;
                                    }
                                    if(objRT_Mst_Sys_Config_Login_Async != null &&
                                        objRT_Mst_Sys_Config_Login_Async.Result != null)
                                    {
                                        objRT_Mst_Sys_Config = objRT_Mst_Sys_Config_Login_Async.Result;
                                    }

                                    #endregion
                                    
                                    var userSate_Temp = new UserState(objRT_Sys_User.Lst_Sys_User[0], inosuser,
                                        objMst_NNT, objRT_Mst_Sys_Config, objRT_Sys_User.Lst_Sys_Access)
                                    {
                                        MST = objRT_Sys_User.Lst_Sys_User[0].MST.ToString(),
                                        AddressAPIs = baseServiceAddress,
                                        AccessToken = CUtils.StrValue(accessToken),
                                        UtcOffset = utcOffset,
                                        AddressAPIs_Product_Customer_Centrer = apiMasterServerProduct_Customer_Center,
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
                    }
                    return View(listOS_Inos_Org);
                }
                else
                {
                    FormsAuthentication.SignOut();
                    var redirectUrl = RedirectActionToLogin();
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #region ["strWhereClause"]
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