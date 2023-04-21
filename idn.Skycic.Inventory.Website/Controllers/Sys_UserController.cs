using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Common.Extensions;
using inos.common.Model;
using System.Configuration;
using inos.common.Service;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Sys_UserController : AdminBaseController
    {
        // GET: Sys_User
        public ActionResult Index(string username, string init = "init", int recordcount = 10, int page = 0)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUOI_DUNG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.PageCur = page.ToString();
            ViewBag.Recordcount = recordcount;
            ViewBag.UserState = this.UserState;
            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var lst_Sys_User = new List<Sys_User>();
            var pageInfo = new PageInfo<Sys_User>(0, recordcount)
            {
                DataList = new List<Sys_User>()
            };
            var itemCount = 0;
            var pageCount = 0;

            if (init != "init")
            {
                #region["Search"]
                var objSys_User = new Sys_User()
                {
                    UserName = username,
                    EMail = username,
                    PhoneNo = username,
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUserName(objSys_User);
                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                if (objRT_Sys_UserCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Sys_UserCur.MySummaryTable.MyCount);
                }
                if (objRT_Sys_UserCur != null && objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Sys_UserCur.Lst_Sys_User);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.MyUserName = username;

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }

        #region["Tạo mới người dùng"]
        [HttpGet]
        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUOI_DUNG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            var isSysadmin = userState.SysUser.FlagSysAdmin;
            var isParent = false;
            if (userState.Mst_NNT.NetworkID.Equals(userState.Mst_NNT.OrgID))
            {
                isParent = true;
            }
            ViewBag.IsParent = isParent;

            #region["Mã số thuế"]
            var objMst_NNTCur = new Mst_NNT()
            {
                MST = waMST,
                FlagActive = Client_Flag.Active,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
            };
            var listMst_NNTCur = new List<Mst_NNT>();
            var strWhereClauseMst_NNTCur = "";
            if (isSysadmin == Client_Flag.Inactive)
            {
                strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
            }
            else
            {
                strWhereClauseMst_NNTCur = null;
            }
            listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));
            ViewBag.ListMst_NNTCur = listMst_NNTCur;

            #endregion
            #region["Danh sách phòng ban"]
            var strWhereClauseMst_Department = "";
            var sbSqlMst_Department = new StringBuilder();
            var Tbl_Mst_Department = TableName.Mst_Department + ".";

            if (!CUtils.IsNullOrEmpty(Client_Flag.Active))
            {
                sbSqlMst_Department.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, Client_Flag.Active, "=");
            }
            strWhereClauseMst_Department = sbSqlMst_Department.ToString();
            //var listMst_Department = List_Mst_Department(strWhereClauseMst_Department);
            var listMst_Department = new List<Mst_DepartmentExt>();
            var objRQ_Mst_Department = new RQ_Mst_Department()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Department,
                Ft_Cols_Upd = null,
                // RQ_Mst_Department
                Rt_Cols_Mst_Department = "*",
                Mst_Department = null,
            };
            listMst_Department = List_Mst_DepartmentExt_Async(objRQ_Mst_Department).Result;
            #endregion

            ViewBag.ListMst_Department = listMst_Department;
            //ViewBag.ListMst_Customer = listMst_Customer;
            return View(new Sys_User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model, string flagExist)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waOrgID = userState.Mst_NNT.OrgID.ToString();
            var passwordDefault = ConfigurationManager.AppSettings["PassDefault"];
            var addMessage = "";
            try
            {
                var objSys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_User>(model);
                objSys_UserInput.FlagDLAdmin = "0";
                objSys_UserInput.EMail = objSys_UserInput.UserCode;
                if (!CUtils.IsNullOrEmpty(flagExist) && flagExist.Equals("1"))
                {
                    objSys_UserInput.UserPassword = passwordDefault;
                }
                var objRQ_Sys_User = new RQ_Sys_User();
                {
                    // WARQBase
                    objRQ_Sys_User.Tid = GetNextTId();
                    objRQ_Sys_User.GwUserCode = GwUserCode;
                    objRQ_Sys_User.GwPassword = GwPassword;
                    objRQ_Sys_User.AccessToken = CUtils.StrValue(UserState.AccessToken);
                    objRQ_Sys_User.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Sys_User.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Sys_User.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                    objRQ_Sys_User.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    objRQ_Sys_User.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                    objRQ_Sys_User.FuncType = null;
                    objRQ_Sys_User.Ft_RecordStart = Ft_RecordStart;
                    objRQ_Sys_User.Ft_RecordCount = Ft_RecordCount;
                    objRQ_Sys_User.Ft_WhereClause = null;
                    objRQ_Sys_User.Ft_Cols_Upd = null;
                    // RQ_Sys_User
                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                    objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = null;
                    objRQ_Sys_User.Sys_User = objSys_UserInput;
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);
                var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Create(objRQ_Sys_User, addressAPIs);

                #region ["Send email kích hoạt tài khoản không cần nữa"]
                //if (objRT_Sys_User != null && objRT_Sys_User.Lst_Sys_User != null && objRT_Sys_User.Lst_Sys_User.Count > 0)
                //{
                //    var domain = System.Configuration.ConfigurationManager.AppSettings["ClientDomain"];
                //    var strBodyHTML = BodyEMail.GetContentMailSysUserActive(objRT_Sys_User.Lst_Sys_User[0].UUID == null ? "" : objRT_Sys_User.Lst_Sys_User[0].UUID, userState.Mst_NNT.NetworkID.ToString().Trim(), domain);
                //    var strTitle = "idocNet thông báo yêu cầu kích hoạt tài khoản";

                //    #region["Sendmail"]
                //    var listToMail = new List<string>();
                //    var listCcMail = new List<string>();
                //    var listBccMail = new List<string>();

                //    if (!CUtils.IsNullOrEmpty(objRT_Sys_User.Lst_Sys_User[0].EMail))
                //    {
                //        listToMail.Add(objRT_Sys_User.Lst_Sys_User[0].EMail);
                //    }

                //    var objSendMail = new SendMail()
                //    {
                //        ApiSendMail = APIsSendMail,
                //        ApiKeySendMail = ApiKeySendMail,
                //        DisplayNameMailFrom = DisplayNameMailFrom,
                //        MailFrom = MailFrom,
                //        ToMail = listToMail,
                //        CcMail = listCcMail,
                //        BccMail = listBccMail,
                //        Subject = strTitle,
                //        HtmlBody = strBodyHTML,
                //        SolutionCode = SolutionCode,
                //        OrgId = waOrgID,
                //    };
                //    var objJsonResultUtil = new SendMailUtil().SendMail(objSendMail);
                //    #endregion
                //}

                //if (objRT_Sys_User.c_K_DT_Sys != null &&
                //    objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                //    objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                //{
                //    var c_K_DT_SysInfoCur = objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                //    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                //    {
                //        var remark = CUtils.StrValue(c_K_DT_SysInfoCur.Remark);
                //        if (!CUtils.IsNullOrEmpty(remark))
                //        {
                //            if (remark.Equals("0"))
                //            {
                //                addMessage = "Tạo tài khoản thành công. Vui lòng kiểm tra email để kích hoạt tài khoản!";
                //            }
                //            else if (remark.Equals("1"))
                //            {
                //                addMessage = "Tài khoản đã được thêm vào tổ chức!";
                //            }
                //        }
                //    }
                //}

                #endregion

                #region ["Update thông tin trên inos với tài khoản chưa có trên inos"]
                if (!CUtils.IsNullOrEmpty(flagExist) && flagExist.Equals("0"))
                {
                    var objInosUser = new InosEditProfileModel();
                    string strInosBaseUrl = ConfigurationManager.AppSettings["InosBaseUrl"];

                    inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

                    AccountService objOrderServiceCheckUser = new AccountService(null);
                    inos.common.Service.AccountService objAccountServiceCheckUser = new AccountService(null);
                    var stateCheckUser = objAccountServiceCheckUser.RequestToken(objSys_UserInput.UserCode, objSys_UserInput.UserPassword, new string[] { HeThongInos });
                    objOrderServiceCheckUser.AccessToken = objAccountServiceCheckUser.AccessToken;

                    inos.common.Service.AccountService objAccountService = new AccountService(null);
                    objAccountService.AccessToken = CUtils.StrValue(objAccountServiceCheckUser.AccessToken);

                    InosUser inosUser = objAccountServiceCheckUser.GetUser(new InosUser() { Email = objSys_UserInput.UserCode });

                    var inosEditProfileModel = new InosEditProfileModel()
                    {
                        ChangePassword = false,
                        ChangeAvatar = false,
                        ChangeLanguage = false,
                        ChangeName = false,
                        ChangePhone = false,
                        ChangeTimeZone = false,
                    };
                    if (objSys_UserInput.UserName != null)
                    {
                        inosEditProfileModel.ChangeName = true;
                        inosEditProfileModel.Name = objSys_UserInput.UserName;
                    }
                    if (objSys_UserInput.ACTimeZone != null)
                    {
                        inosEditProfileModel.ChangeTimeZone = true;
                        inosEditProfileModel.TimeZone = Convert.ToInt32(objSys_UserInput.ACTimeZone);
                    }
                    if (objSys_UserInput.ACLanguage != null)
                    {
                        inosEditProfileModel.ChangeLanguage = true;
                        inosEditProfileModel.Language = objSys_UserInput.ACLanguage;
                    }
                    if (objSys_UserInput.PhoneNo != null)
                    {
                        inosEditProfileModel.ChangePhone = true;
                        inosEditProfileModel.Phone = objSys_UserInput.PhoneNo;
                    }
                    var status = objAccountServiceCheckUser.EditProfile(inosEditProfileModel);
                }

                #endregion

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo tài khoản thành công!");
                resultEntry.RedirectUrl = Url.Action("Index", "Sys_User");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region ["Load Customer"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMstCustomer(string mst)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            var isSysadmin = userState.SysUser.FlagSysAdmin;
            var isParent = false;
            var resultEntry = new JsonResultEntry() { Success = false };

            if (userState.Mst_NNT.NetworkID.Equals(userState.Mst_NNT.OrgID))
            {
                isParent = true;
            }
            ViewBag.IsParent = isParent;

            #region["Mã số thuế"]
            var objMst_NNTCur = new Mst_NNT()
            {
                MST = mst,
                FlagActive = Client_Flag.Active,
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
            };
            var listMst_NNTCur = new List<Mst_NNT>();
            var strWhereClauseMst_NNTCur = "";
            if (isSysadmin == Client_Flag.Inactive)
            {
                strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
            }
            else
            {
                strWhereClauseMst_NNTCur = null;
            }
            listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));
            #endregion
            try
            {
                var listMst_Customer = new List<Mst_Customer>();
                if (listMst_NNTCur != null && listMst_NNTCur.Count > 0)
                {
                    var objMst_NNT = listMst_NNTCur.Where(x => x.MST.Equals(mst)).FirstOrDefault();
                    if (objMst_NNT != null)
                    {
                        #region ["Danh sách Customer"]
                        var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(new Mst_Customer()
                        {
                            FlagDealer = Client_Flag.Active,
                            OrgID = CUtils.StrValue(objMst_NNT.OrgID),
                            FlagActive = Client_Flag.Active,
                        });
                        var objRQ_Mst_Customer = new RQ_Mst_Customer()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClauseMst_Customer,
                            Ft_Cols_Upd = null,
                            // RQ_Mst_Ward
                            Rt_Cols_Mst_Customer = "*",
                            Mst_Customer = null,
                        };
                        var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                        if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                        {
                            listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
                        }
                        #endregion
                    }

                }

                return JsonView("LoadMst_Customer", listMst_Customer);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("LoadMst_Customer", null, resultEntry);
        }
        #endregion

        #region["StopService"]
        [HttpGet]
        public ActionResult StopService()
        {
            return View();
        }
        #endregion

        #region["Sửa thông tin người dùng"]
        [HttpGet]
        public ActionResult Update(string usercode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUOI_DUNG_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            ViewBag.IsSysAdmin = userState.SysUser.FlagSysAdmin;
            var waMST = userState.SysUser.MST;
            var isSysadmin = userState.SysUser.FlagSysAdmin;
            var isParent = false;
            if (userState.Mst_NNT.NetworkID.Equals(userState.Mst_NNT.OrgID))
            {
                isParent = true;
            }
            ViewBag.IsParent = isParent;
            ViewBag.InosBaseUrl = System.Configuration.ConfigurationManager.AppSettings["InosBaseUrl"];
            if (!CUtils.IsNullOrEmpty(usercode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUser(usercode);

                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    #region["Mã số thuế"]
                    var objMst_NNTCur = new Mst_NNT()
                    {
                        MST = waMST,
                        FlagActive = Client_Flag.Active,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    };
                    var listMst_NNTCur = new List<Mst_NNT>();
                    var strWhereClauseMst_NNTCur = "";
                    if (isSysadmin == Client_Flag.Inactive)
                    {
                        strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
                    }
                    else
                    {
                        strWhereClauseMst_NNTCur = null;
                    }
                    listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));
                    ViewBag.ListMst_NNTCur = listMst_NNTCur;

                    #endregion
                    #region["Danh sách phòng ban"]
                    var strWhereClauseMst_Department = "";
                    var sbSqlMst_Department = new StringBuilder();
                    var Tbl_Mst_Department = TableName.Mst_Department + ".";

                    if (!CUtils.IsNullOrEmpty(Client_Flag.Active))
                    {
                        sbSqlMst_Department.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, Client_Flag.Active, "=");
                    }
                    strWhereClauseMst_Department = sbSqlMst_Department.ToString();
                    //var listMst_Department = List_Mst_Department(strWhereClauseMst_Department);
                    var listMst_Department = new List<Mst_DepartmentExt>();
                    var objRQ_Mst_Department = new RQ_Mst_Department()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Department,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Department
                        Rt_Cols_Mst_Department = "*",
                        Mst_Department = null,
                    };
                    listMst_Department = List_Mst_DepartmentExt_Async(objRQ_Mst_Department).Result;
                    #endregion

                    #region ["Danh sách Customer"]
                    var listMst_Customer = new List<Mst_Customer>();
                    var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(new Mst_Customer()
                    {
                        FlagDealer = Client_Flag.Active,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        FlagActive = Client_Flag.Active,
                    });
                    var objRQ_Mst_Customer = new RQ_Mst_Customer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Customer,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Ward
                        Rt_Cols_Mst_Customer = "*",
                        Mst_Customer = null,
                    };
                    var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                    if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                    {
                        listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
                    }
                    ViewBag.ListMst_Customer = listMst_Customer;
                    #endregion

                    ViewBag.ListMst_Department = listMst_Department;

                    return View(objRT_Sys_UserCur.Lst_Sys_User[0]);
                }
            }
            return View(new Sys_User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var requestUrl = Request.Url;
            var requestUrl1 = Request.RawUrl;
            string query = Request.Url.Query;
            string isAllowed = Request.QueryString["allowed"];
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objSys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_User>(model);
            if (objSys_UserInput != null && !CUtils.IsNullOrEmpty(objSys_UserInput.UserCode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUser(objSys_UserInput.UserCode);
                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };
                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    objRT_Sys_UserCur.Lst_Sys_User[0].UserName = objSys_UserInput.UserName;
                    objRT_Sys_UserCur.Lst_Sys_User[0].PhoneNo = objSys_UserInput.PhoneNo;
                    objRT_Sys_UserCur.Lst_Sys_User[0].Position = objSys_UserInput.Position;
                    objRT_Sys_UserCur.Lst_Sys_User[0].DepartmentCode = objSys_UserInput.DepartmentCode;
                    objRT_Sys_UserCur.Lst_Sys_User[0].FlagSysAdmin = objSys_UserInput.FlagSysAdmin;
                    objRT_Sys_UserCur.Lst_Sys_User[0].FlagNNTAdmin = objSys_UserInput.FlagNNTAdmin;
                    objRT_Sys_UserCur.Lst_Sys_User[0].FlagActive = objSys_UserInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_User = TableName.Sys_User + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.UserName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.PhoneNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.Position);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.DepartmentCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.FlagSysAdmin);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.FlagNNTAdmin);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.FlagActive);

                    objRQ_Sys_User.Tid = GetNextTId();
                    objRQ_Sys_User.Ft_WhereClause = null;
                    objRQ_Sys_User.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                    objRQ_Sys_User.Sys_User = objRT_Sys_UserCur.Lst_Sys_User[0];

                    Sys_UserService.Instance.WA_Sys_User_Update(objRQ_Sys_User, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa người dùng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã người dùng '" + objSys_UserInput.UserCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã người dùng trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Xem chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string usercode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUOI_DUNG_CHI_TIET");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            var isSysadmin = userState.SysUser.FlagSysAdmin;
            var isParent = false;
            if (userState.Mst_NNT.NetworkID.Equals(userState.Mst_NNT.OrgID))
            {
                isParent = true;
            }
            ViewBag.IsParent = isParent;
            ViewBag.InosBaseUrl = System.Configuration.ConfigurationManager.AppSettings["InosBaseUrl"];
            if (!CUtils.IsNullOrEmpty(usercode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUser(usercode);

                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    #region["Mã số thuế"]
                    var objMst_NNTCur = new Mst_NNT()
                    {
                        MST = waMST,
                        FlagActive = Client_Flag.Active,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    };
                    var listMst_NNTCur = new List<Mst_NNT>();
                    var strWhereClauseMst_NNTCur = "";
                    if (isSysadmin == Client_Flag.Inactive)
                    {
                        strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
                    }
                    else
                    {
                        strWhereClauseMst_NNTCur = null;
                    }
                    listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));
                    ViewBag.ListMst_NNTCur = listMst_NNTCur;

                    #endregion
                    #region["Danh sách phòng ban"]
                    var strWhereClauseMst_Department = "";
                    var sbSqlMst_Department = new StringBuilder();
                    var Tbl_Mst_Department = TableName.Mst_Department + ".";

                    if (!CUtils.IsNullOrEmpty(Client_Flag.Active))
                    {
                        sbSqlMst_Department.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, Client_Flag.Active, "=");
                    }
                    strWhereClauseMst_Department = sbSqlMst_Department.ToString();
                    //var listMst_Department = List_Mst_Department(strWhereClauseMst_Department);
                    var listMst_Department = new List<Mst_DepartmentExt>();
                    var objRQ_Mst_Department = new RQ_Mst_Department()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Department,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Department
                        Rt_Cols_Mst_Department = "*",
                        Mst_Department = null,
                    };
                    listMst_Department = List_Mst_DepartmentExt_Async(objRQ_Mst_Department).Result;
                    #endregion
                    #region ["Danh sách Customer"]
                    var listMst_Customer = new List<Mst_Customer>();
                    var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(new Mst_Customer()
                    {
                        FlagDealer = Client_Flag.Active,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        FlagActive = Client_Flag.Active,
                    });
                    var objRQ_Mst_Customer = new RQ_Mst_Customer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Customer,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Ward
                        Rt_Cols_Mst_Customer = "*",
                        Mst_Customer = null,
                    };
                    var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                    if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                    {
                        listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
                    }
                    ViewBag.ListMst_Customer = listMst_Customer;
                    #endregion
                    ViewBag.ListMst_Department = listMst_Department;

                    return View(objRT_Sys_UserCur.Lst_Sys_User[0]);
                }
            }
            return View(new Sys_User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string usercode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(usercode))
            {
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_SysUser(usercode);

                    var objRQ_Sys_User = new RQ_Sys_User()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClause,
                        Ft_Cols_Upd = null,
                        // RQ_Sys_User
                        Rt_Cols_Sys_User = "*",
                        Rt_Cols_Sys_UserInGroup = null,
                        Sys_User = null,
                    };

                    var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                    if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count != 0)
                    {
                        objRQ_Sys_User.Sys_User = objRT_Sys_UserCur.Lst_Sys_User[0];
                        Sys_UserService.Instance.WA_Sys_User_Delete(objRQ_Sys_User, addressAPIs);
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa người dùng thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã người dùng '" + usercode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã người dùng trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Change Password"]
        [HttpPost]
        public ActionResult ShowPopupChangePassword(string usercode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserCode = usercode;
                return JsonView("ShowPopupChangePassword", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupChangePassword", null, resultEntry);
        }

        [HttpPost]
        public ActionResult ShowPopupChangePasswordCheckOld(string usercode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserCode = usercode;
                return JsonView("ShowPopupChangePasswordCheckOld", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupChangePasswordCheckOld", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string usercode, string passnew, string confpass)
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
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    if (!CUtils.IsNullOrEmpty(passnew))
                    {
                        passnew = passnew.Trim();
                        if (!CUtils.IsNullOrEmpty(confpass))
                        {
                            confpass = confpass.Trim();
                            if (passnew.Equals(confpass))
                            {
                                var strWhereClause = "";
                                strWhereClause = strWhereClause_SysUser(usercode);

                                var objRQ_Sys_User = new RQ_Sys_User()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClause,
                                    Ft_Cols_Upd = null,
                                    // RQ_Sys_User
                                    Rt_Cols_Sys_User = "*",
                                    Rt_Cols_Sys_UserInGroup = null,
                                    Sys_User = null,
                                };
                                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                                {
                                    passnew = CUtils.GetEncodedHash(passnew);

                                    objRT_Sys_UserCur.Lst_Sys_User[0].UserPassword = passnew;
                                    var strFt_Cols_Upd = "";
                                    var Tbl_Sys_User = TableName.Sys_User + ".";
                                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.UserPassword);
                                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                                    objRQ_Sys_User.Sys_User = objRT_Sys_UserCur.Lst_Sys_User[0];
                                    objRQ_Sys_User.Ft_Cols_Upd = strFt_Cols_Upd;
                                    Sys_UserService.Instance.WA_Sys_User_Update(objRQ_Sys_User, addressAPIs);
                                    title = "Thay đổi mật khẩu thành công!";
                                    if (objRQ_Sys_User.Sys_User.UserCode.Equals(userState.SysUser.UserCode))
                                    {
                                        resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                                    }

                                }
                                else
                                {
                                    title = "Mã người dùng '" + usercode + "' không có trong hệ thống!";
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
                    title = "Mã người dùng trống!";
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(title);

                //return Json(resultEntry);

                //return Json(new { Success = true, Title = title });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            //return Json(new { Success = false, Detail = resultEntry.Detail });
            return Json(resultEntry);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordCheckOld(string usercode, string passold, string passnew, string confpass)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
            {
                networkid = CUtils.StrValue(userState.Mst_NNT.NetworkID);
            }

            var orgid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                orgid = CUtils.StrValue(userState.Mst_NNT.OrgID);
            }

            var AccessToken = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                AccessToken = CUtils.StrValue(userState.AccessToken);
            }
            try
            {
                #region ["Comment"]
                //if (!CUtils.IsNullOrEmpty(usercode))
                //{
                //    var strWhereClause = "";
                //    strWhereClause = strWhereClause_SysUser(usercode);

                //    var objRQ_Sys_User = new RQ_Sys_User()
                //    {
                //        // WARQBase
                //        Tid = GetNextTId(),
                //        GwUserCode = GwUserCode,
                //        GwPassword = GwPassword,
                //        AccessToken = CUtils.StrValue(UserState.AccessToken),
                //        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                //        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                //        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                //        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                //        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                //        FuncType = null,
                //        Ft_RecordStart = Ft_RecordStart,
                //        Ft_RecordCount = Ft_RecordCount,
                //        Ft_WhereClause = strWhereClause,
                //        Ft_Cols_Upd = null,
                //        // RQ_Sys_User
                //        Rt_Cols_Sys_User = "*",
                //        Rt_Cols_Sys_UserInGroup = null,
                //        Sys_User = null,
                //    };
                //    var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                //    var userPassword = passold;
                //    passold = CUtils.GetEncodedHash(passold);

                //    if (!CUtils.IsNullOrEmpty(userPassword) && userPassword == waUserPassword)
                //    {
                //        if (!CUtils.IsNullOrEmpty(passnew))
                //        {
                //            passnew = passnew.Trim();
                //            var userPasswordNew = passnew;
                //            if (!CUtils.IsNullOrEmpty(confpass))
                //            {
                //                confpass = confpass.Trim();
                //                if (passnew.Equals(confpass))
                //                {
                //                    if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                //                    {
                //                        objRQ_Sys_User.Tid = GetNextTId();
                //                        //objRQ_Sys_User.NetworkID = networkid;
                //                        //objRQ_Sys_User.OrgID = orgid;
                //                        //objRQ_Sys_User.AccessToken = AccessToken;

                //                        objRQ_Sys_User.Sys_User = new Sys_User()
                //                        {
                //                            UserCode = waUserCode,
                //                            UserPassword = userPassword,
                //                            UserPasswordNew = userPasswordNew,
                //                        };
                //                        Sys_UserService.Instance.WA_Sys_User_ChangePassword(objRQ_Sys_User, addressAPIs);
                //                        title = "Thay đổi mật khẩu thành công!";
                //                        resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                //                        //if (objRQ_Sys_User.Sys_User.UserCode.Equals(userState.SysUser.UserCode))
                //                        //{
                //                        //    resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                //                        //}

                //                    }
                //                    else
                //                    {
                //                        title = "Mã người dùng '" + usercode + "' không có trong hệ thống!";
                //                    }
                //                }
                //                else
                //                {
                //                    title = "Nhập lại mật khẩu mới không đúng, Vui lòng nhập lại!";
                //                }
                //            }
                //            else
                //            {
                //                title = "Nhập lại khẩu mới trống!";
                //            }
                //        }
                //        else
                //        {
                //            title = "Mật khẩu mới trống!";
                //        }
                //    }
                //    else
                //    {
                //        title = "Mật khẩu cũ không đúng!";
                //    }
                //}
                //else
                //{
                //    title = "Mã người dùng trống!";
                //}
                #endregion

                var title = "";
                string strInosBaseUrl = ConfigurationManager.AppSettings["InosBaseUrl"];

                inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

                inos.common.Service.AccountService objAccountService = new AccountService(null);

                var state = objAccountService.RequestToken(userState.SysUser.UserCode, passold, new string[] { "test" });


                //var old = objAccountService.GetCurrentUser();
                var inosEditProfileModel = new InosEditProfileModel()
                {
                    ChangePassword = true,
                    OldPassword = passold,
                    NewPassword = passnew,
                    ChangeAvatar = false,
                };

                var status = objAccountService.EditProfile(inosEditProfileModel);
                title = "Đổi mật khẩu thành công!";

                resultEntry.Success = true;
                resultEntry.AddMessage(title);

                //return Json(resultEntry);

                //return Json(new { Success = true, Title = title });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            //return Json(new { Success = false, Detail = resultEntry.Detail });
            return Json(resultEntry);

        }
        #endregion

        #region ["Change information"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfo(string model, string modelSys_User)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
            {
                networkid = CUtils.StrValue(userState.Mst_NNT.NetworkID);
            }

            var orgid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                orgid = CUtils.StrValue(userState.Mst_NNT.OrgID);
            }

            var AccessToken = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                AccessToken = CUtils.StrValue(userState.AccessToken);
            }
            try
            {
                var objSys_User = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_User>(modelSys_User);
                var objSys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<InosEditProfileModel>(model);
                var title = "";
                string strInosBaseUrl = ConfigurationManager.AppSettings["InosBaseUrl"];

                #region ["Update lên inos"]

                inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

                inos.common.Service.AccountService objAccountService = new AccountService(null);
                objAccountService.AccessToken = CUtils.StrValue(userState.AccessToken);
                var inosEditProfileModel = new InosEditProfileModel()
                {
                    ChangePassword = false,
                    ChangeAvatar = false,
                    ChangeLanguage = false,
                    ChangeName = false,
                    ChangePhone = false,
                    ChangeTimeZone = false,
                };
                var old = objAccountService.GetCurrentUser();

                if (objSys_UserInput.Name != old.Name)
                {
                    inosEditProfileModel.ChangeName = true;
                    inosEditProfileModel.Name = objSys_UserInput.Name;
                }
                if (objSys_UserInput.Phone != old.Phone)
                {
                    inosEditProfileModel.ChangePhone = true;
                    inosEditProfileModel.Phone = objSys_UserInput.Phone;
                }
                if (objSys_UserInput.Language != old.Language)
                {
                    inosEditProfileModel.ChangeLanguage = true;
                    inosEditProfileModel.Language = objSys_UserInput.Language;
                }
                if (objSys_UserInput.TimeZone != old.TimeZone)
                {
                    inosEditProfileModel.ChangeTimeZone = true;
                    inosEditProfileModel.TimeZone = objSys_UserInput.TimeZone;
                }
                if (objSys_UserInput.ChangeAvatar == true)
                {
                    inosEditProfileModel.AvatarBase64 = objSys_UserInput.AvatarBase64;
                    inosEditProfileModel.ChangeAvatar = true;
                }
                if (objSys_UserInput.ChangePassword == true)
                {
                    inosEditProfileModel.ChangePassword = true;
                    inosEditProfileModel.OldPassword = objSys_UserInput.OldPassword;
                    inosEditProfileModel.NewPassword = objSys_UserInput.NewPassword;
                }

                #endregion

                #region ["Update tại solution"]
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUser(objSys_User.EMail);
                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };
                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    objRT_Sys_UserCur.Lst_Sys_User[0].UserName = objSys_User.UserName;
                    objRT_Sys_UserCur.Lst_Sys_User[0].PhoneNo = objSys_User.PhoneNo;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_User = TableName.Sys_User + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.UserName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.PhoneNo);

                    objRQ_Sys_User.Tid = GetNextTId();
                    objRQ_Sys_User.Ft_WhereClause = null;
                    objRQ_Sys_User.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                    objRQ_Sys_User.Sys_User = objRT_Sys_UserCur.Lst_Sys_User[0];

                    Sys_UserService.Instance.WA_Sys_User_Update(objRQ_Sys_User, addressAPIs);
                    resultEntry.Success = true;
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã người dùng '" + objSys_User.UserCode + "' không có trong hệ thống!");
                }
                #endregion

                var status = objAccountService.EditProfile(inosEditProfileModel);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật thành công! Vui lòng đăng nhập lại để thay đổi có hiệu lực!");

            }
            catch (Exception ex)
            {
                //resultEntry.SetFailed().AddException(this, ex);
                resultEntry.AddMessage("Tên đăng nhập hoặc mật khẩu không hợp lệ!");
                resultEntry.Success = false;
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string username = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<Sys_User>();
                var objSys_User = new Sys_User()
                {
                    UserName = username,
                    EMail = username,
                    PhoneNo = username,
                };
                var strWhereClauseSys_User = strWhereClause_SysUserName(objSys_User);
                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSys_User,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    list.AddRange(objRT_Sys_UserCur.Lst_Sys_User);

                    //foreach (var item in list)
                    //{
                    //    item.UserPassword = "*********";
                    //}

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_User).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_User"));

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu!", CheckSuccess = "1" });
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<Sys_User>();

                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_User).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_User"));

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var exitsData = "";
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {
                    exitsData = "File excel import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                else
                {
                    throw new Exception("File excel import không hợp lệ!");
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                var list = new List<Sys_User>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != GetImportDicColums().Count)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        foreach (DataRow dr in table.Rows)
                        {
                            var customerCodeSys = "";
                            if (CUtils.IsNullOrEmpty(dr["EMail"]))
                            {
                                exitsData = "Email không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var usercode = CUtils.StrValue(dr["EMail"]);
                                if (!CUtils.IsValidEmail(usercode))
                                {
                                    exitsData = "Email không hợp lệ!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr["UserName"]))
                            {
                                exitsData = "Tên người dùng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            #region ["Comment CustomerCodeSys"]
                            //if (CUtils.IsNullOrEmpty(dr["CustomerCodeSys"]))
                            //{
                            //    exitsData = "Mã đại lý không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    customerCodeSys = dr["CustomerCodeSys"].ToString();
                            //    var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(new Mst_Customer()
                            //    {
                            //        CustomerCode = customerCodeSys,
                            //        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                            //        FlagDealer = "1",
                            //    });
                            //    var objRQ_Mst_Customer = new RQ_Mst_Customer()
                            //    {
                            //        // WARQBase
                            //        Tid = GetNextTId(),
                            //        GwUserCode = GwUserCode,
                            //        GwPassword = GwPassword,
                            //        AccessToken = CUtils.StrValue(UserState.AccessToken),
                            //        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            //        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            //        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            //        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            //        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                            //        Ft_WhereClause = strWhereClauseMst_Customer,
                            //        Ft_RecordStart = Ft_RecordStart,
                            //        Ft_RecordCount = Ft_RecordCount,
                            //        //
                            //        Rt_Cols_Mst_Customer = "*",
                            //    };
                            //    var objRT_Mst_Customer = RT_Mst_Customer_Get(objRQ_Mst_Customer);
                            //    if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                            //    {
                            //        dr["CustomerCodeSys"] = objRT_Mst_Customer.Lst_Mst_Customer[0].CustomerCodeSys;
                            //    }
                            //    else
                            //    {
                            //        exitsData = "Mã đại lý " + customerCodeSys + " không tồn tại trong hệ thống!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
                            #endregion

                            if (CUtils.IsNullOrEmpty(dr[TblSys_User.MST]))
                            {
                                exitsData = "Chi nhánh không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (CUtils.IsNullOrEmpty(dr["UserPassword"]))
                            {
                                exitsData = "Mật khẩu không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var password = dr["UserPassword"].ToString().Trim();
                                if (password.Length < 6)
                                {
                                    exitsData = "Mật khẩu > 5 ký tự!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            #region ["Cờ quản trị"]
                            if (CUtils.IsNullOrEmpty(dr["FlagNNTAdmin"]))
                            {
                                exitsData = "Cờ Quản trị đơn vị không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var sysAdmin = dr["FlagNNTAdmin"].ToString().Trim();
                                if (!sysAdmin.Equals("0") && !sysAdmin.Equals("1"))
                                {
                                    exitsData = "Cờ Quản trị đơn vị nhập '0' hoặc '1'!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            if (CUtils.IsNullOrEmpty(dr["FlagSysAdmin"]))
                            {
                                exitsData = "Cờ Quản trị HT không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var sysAdmin = dr["FlagSysAdmin"].ToString().Trim();
                                if (!sysAdmin.Equals("0") && !sysAdmin.Equals("1"))
                                {
                                    exitsData = "Cờ Quản trị HT nhập '0' hoặc '1'!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            //if (CUtils.IsNullOrEmpty(dr["FlagSysAdmin"]))
                            //{
                            //    exitsData = "QTHD đi không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var sysAdmin = dr["FlagSysAdmin"].ToString().Trim();
                            //    if (!sysAdmin.Equals("0") && !sysAdmin.Equals("1"))
                            //    {
                            //        exitsData = "QTHD đi nhập '0' hoặc '1'!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}

                            //if (CUtils.IsNullOrEmpty(dr["FlagViewOtherOrgCreate"]))
                            //{
                            //    exitsData = "QTHD đến không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var sysAdmin = dr["FlagSysAdmin"].ToString().Trim();
                            //    if (!sysAdmin.Equals("0") && !sysAdmin.Equals("1"))
                            //    {
                            //        exitsData = "QTHD đến nhập '0' hoặc '1'!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
                            #endregion

                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var userCodeCur = table.Rows[i]["EMail"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _userCodeCur = table.Rows[j]["EMail"].ToString().Trim();
                                    if (userCodeCur.Equals(_userCodeCur))
                                    {
                                        exitsData = "Email '" + userCodeCur + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<Sys_User>(table);
                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
                            item.UserCode = CUtils.StrValue(item.EMail);
                            var objRQ_Sys_User = new RQ_Sys_User()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                // RQ_Sys_User
                                Rt_Cols_Sys_User = null,
                                Rt_Cols_Sys_UserInGroup = null,
                                Sys_User = item,
                            };
                            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);
                            Sys_UserService.Instance.WA_Sys_User_Create(objRQ_Sys_User, addressAPIs);
                        }
                    }

                    resultEntry.Success = true;
                    exitsData = "Đã nhập dữ liệu excel thành công!";
                    resultEntry.AddMessage(exitsData);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["Check user exist in Inos"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckUserExist(string email)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var serverBaseAddress = "";
            var strEmail = "";
            var strEmail1 = CUtils.StrValue(email);
            var function = "";
            try
            {
                var flagexist = false;
                InosUser userInos = new InosUser();
                string inosUrl = System.Configuration.ConfigurationManager.AppSettings["InosBaseUrl"];
                inos.common.Paths.SetInosServerBaseAddress(inosUrl);
                serverBaseAddress = inos.common.Paths.AuthorizationServerBaseAddress;
                if (!CUtils.IsNullOrEmpty(email))
                {
                    #region["Check người dùng tồn tại trên Inos?"]
                    AccountService objOrderServiceCheckUser = new AccountService(null);
                    inos.common.Service.AccountService objAccountServiceCheckUser = new AccountService(null);
                    var stateCheckUser = objAccountServiceCheckUser.RequestToken(UserInos, UserInosPass, new string[] { HeThongInos });

                    objOrderServiceCheckUser.AccessToken = objAccountServiceCheckUser.AccessToken;

                    InosUser dummy = new InosUser
                    {
                        Email = CUtils.StrValue(email)
                    };
                    strEmail = dummy.Email;
                    function = "GetUser từ iNOS";
                    InosUser inosUser = AccountService.GetUser(dummy);
                    #endregion

                    if (inosUser == null)
                    {
                        flagexist = false;
                    }
                    else
                    {
                        flagexist = true;
                        userInos = inosUser;
                        if (!CUtils.IsNullOrEmpty(userInos.Avatar))
                        {
                            userInos.Avatar = CUtils.StrValue(inosUrl.ToString() + userInos.Avatar.Split('~')[1].ToString());
                        }
                    }

                }
                return Json(new { Success = true, FlagExist = flagexist, UserInos = userInos });
            }
            catch (Exception ex)
            {
                resultEntry.AddMessage(ex.StackTrace.ToString());
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(ex);
                //var detail = resultEntry.Detail;
                //var errorDetail = "BaseInosUrl :" + serverBaseAddress + "<br />" + "Email input: " + strEmail1 + "<br />" + "Email sau khi gán: " + strEmail + "<br />" + "Function: " + function + "<br />" + detail;
                //resultEntry.Detail = errorDetail;
            }
            return Json(resultEntry);
        }
        #endregion

        #region
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {TblSys_User.UserCode,"Mã người dùng"},
                 {TblSys_User.UserName,"Tên người dùng"},
                 {TblSys_User.Position,"Chức vụ"},
                 {TblSys_User.MST,"Mã chi nhánh"},
                 {TblSys_User.CustomerCode,"Khách hàng"},
                 //{TblSys_User.DepartmentCode,"Bộ phận/Phòng ban"},
                 {TblSys_User.EMail,"Email"},
                 {TblSys_User.PhoneNo,"Điện thoại"},
                 {TblSys_User.FlagNNTAdmin,"QT đơn vị"},
                 //{TblSys_User.FlagSysAdmin,"QT hệ thống"},
                 {TblSys_User.FlagActive,"Trạng thái"},
                 {TblSys_User.ACLanguage,"Ngôn ngữ"},
                 {TblSys_User.ACTimeZone,"Múi giờ"}
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {TblSys_User.UserName,"Họ tên(*)"},
                 {TblSys_User.EMail,"Email(*)"},
                 {TblSys_User.Position,"Chức vụ"},
                 {TblSys_User.UserPassword,"Mật khẩu(*)"},
                 {TblSys_User.MST,"Chi nhánh(*)"},
                 {TblSys_User.CustomerCodeSys,"Khách hàng"},
                 {TblSys_User.PhoneNo,"Điện thoại"},
                 {TblSys_User.FlagNNTAdmin,"QT đơn vị(*)"},
                 //{TblSys_User.FlagSysAdmin,"QT hệ thống(*)"},
                 {TblSys_User.FlagActive,"Trạng thái"}
            };
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

        private string strWhereClause_SysUserName(string username)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(username))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserName, "%" + username + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysUserName(Sys_User data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(data.UserName))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserName, "%" + data.UserName.ToString().Trim() + "%", "like", "or");
            }
            if (!CUtils.IsNullOrEmpty(data.PhoneNo))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.PhoneNo, "%" + data.PhoneNo.ToString().Trim() + "%", "like", "or");
            }
            if (!CUtils.IsNullOrEmpty(data.EMail))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.EMail, "%" + data.EMail.ToString().Trim() + "%", "like", "or");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValue(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValue(data.OrgID), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNTCurChil(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValue(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValue(data.OrgID), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Customer(Mst_Customer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Customer = TableName.Mst_Customer + ".";
            if (!CUtils.IsNullOrEmpty(data.CustomerCodeSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerCodeSys, data.CustomerCodeSys.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerCode, data.CustomerCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagDealer))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagDealer, data.FlagDealer.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagActive, data.FlagActive.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.OrgID, data.OrgID.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        #endregion
    }
}