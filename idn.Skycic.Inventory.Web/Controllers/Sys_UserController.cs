using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Sys_UserController : AdminBaseController
    {
        // GET: Sys_User
        public ActionResult Index(string username, string init = "init", int RecordCount = 10, int page = 0)
        {
            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var lst_Sys_User = new List<Sys_User>();
            var pageInfo = new PageInfo<Sys_User>(0, RecordCount)
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
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysUserName(objSys_User);
                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = RecordCount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * RecordCount).ToString(),
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

                if (objRT_Sys_UserCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Sys_UserCur.MySummaryTable.MyCount);
                }
                if (objRT_Sys_UserCur != null && objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Sys_UserCur.Lst_Sys_User);
                    pageCount = (itemCount % RecordCount == 0) ? itemCount / RecordCount : itemCount / RecordCount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.MyUserName = username;

            pageInfo.PageSize = RecordCount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * RecordCount).ToString();
            return View(pageInfo);
        }

        #region["Tạo mới người dùng"]
        [HttpGet]
        public ActionResult Create()
        {
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
                FlagActive = FlagActive,
            };
            var listMst_NNTCur = new List<Mst_NNT>();
            var strWhereClauseMst_NNTCur = "";
            if (isSysadmin == FlagInActive)
            {
                strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
            }
            else
            {
                strWhereClauseMst_NNTCur = null;
            }
            listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));

            //var objMst_NNTCurChil = new Mst_NNT()
            //{
            //    MSTParent = waMST,
            //    FlagActive = FlagActive,
            //};
            //var strWhereClauseMst_NNTCurChil = strWhereClause_Mst_NNTCurChil(objMst_NNTCurChil);
            //listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCurChil));

            ViewBag.ListMst_NNTCur = listMst_NNTCur;
            #endregion
            #region["Danh sách phòng ban"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = userState.TokenID.ToString().Trim(),
                NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_Department
                Rt_Cols_Mst_Department = "*",
                Mst_Department = null,
            };
            listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
            #endregion
            ViewBag.ListMst_Department = listMst_Department;
            return View(new Sys_User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waOrgID = userState.Mst_NNT.OrgID.ToString();

            var addMessage = "";
            try
            {
                var objSys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_User>(model);
                objSys_UserInput.FlagDLAdmin = "0";
                objSys_UserInput.EMail = objSys_UserInput.UserCode;
                var objRQ_Sys_User = new RQ_Sys_User();
                {
                    // WARQBase
                    objRQ_Sys_User.Tid = GetNextTId();
                    objRQ_Sys_User.GwUserCode = GwUserCode;
                    objRQ_Sys_User.GwPassword = GwPassword;
                    objRQ_Sys_User.FuncType = null;
                    objRQ_Sys_User.Ft_RecordStart = Ft_RecordStart;
                    objRQ_Sys_User.Ft_RecordCount = Ft_RecordCount;
                    objRQ_Sys_User.Ft_WhereClause = null;
                    objRQ_Sys_User.Ft_Cols_Upd = null;
                    objRQ_Sys_User.WAUserCode = waUserCode;
                    objRQ_Sys_User.WAUserPassword = waUserPassword;
                    objRQ_Sys_User.AccessToken = userState.TokenID.ToString().Trim();
                    objRQ_Sys_User.NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim();
                    objRQ_Sys_User.OrgID = userState.Mst_NNT.OrgID.ToString().Trim();
                    // RQ_Sys_User
                    objRQ_Sys_User.Rt_Cols_Sys_User = null;
                    objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = null;
                    objRQ_Sys_User.Sys_User = objSys_UserInput;
                };
                var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Create(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_User != null && objRT_Sys_User.Lst_Sys_User != null && objRT_Sys_User.Lst_Sys_User.Count > 0)
                {
                    var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                    var strBodyHTML = BodyEMail.GetContentMailSysUserActive(objRT_Sys_User.Lst_Sys_User[0].UUID == null ? "" : objRT_Sys_User.Lst_Sys_User[0].UUID, userState.Mst_NNT.NetworkID.ToString().Trim(), domain);
                    var strTitle = "idocNet thông báo yêu cầu kích hoạt tài khoản";

                    #region["Sendmail"]
                    var listToMail = new List<string>();
                    var listCcMail = new List<string>();
                    var listBccMail = new List<string>();

                    if (!CUtils.IsNullOrEmpty(objRT_Sys_User.Lst_Sys_User[0].EMail))
                    {
                        listToMail.Add(objRT_Sys_User.Lst_Sys_User[0].EMail);
                    }

                    var objSendMail = new SendMail()
                    {
                        ApiSendMail = APIsSendMail,
                        ApiKeySendMail = ApiKeySendMail,
                        DisplayNameMailFrom = DisplayNameMailFrom,
                        MailFrom = MailFrom,
                        ToMail = listToMail,
                        CcMail = listCcMail,
                        BccMail = listBccMail,
                        Subject = strTitle,
                        HtmlBody = strBodyHTML,
                        SolutionCode = SolutionCode,
                        OrgId = waOrgID,
                    };
                    var objJsonResultUtil = new SendMailUtil().SendMail(objSendMail);
                    #endregion
                }
                if (objRT_Sys_User.c_K_DT_Sys != null &&
                    objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                    objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                {
                    var c_K_DT_SysInfoCur = objRT_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                    {
                        var remark = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                        if (!CUtils.IsNullOrEmpty(remark))
                        {
                            if (remark.Equals("0"))
                            {
                                addMessage = "Tạo tài khoản thành công. Vui lòng kiểm tra email để kích hoạt tài khoản!";
                            }
                            else if (remark.Equals("1"))
                            {
                                addMessage = "Tài khoản đã được thêm vào tổ chức!";
                            }
                        }
                    }
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(addMessage);
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
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            ViewBag.IsSysAdmin = userState.SysUser.FlagSysAdmin;

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
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = userState.TokenID.ToString().Trim(),
                    NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                    OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {

                    #region["Danh sách phòng ban"]
                    var strWhereClauseMst_Department = "";
                    var sbSqlMst_Department = new StringBuilder();
                    var Tbl_Mst_Department = TableName.Mst_Department + ".";

                    if (!CUtils.IsNullOrEmpty(FlagActive))
                    {
                        sbSqlMst_Department.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, FlagActive, "=");
                    }
                    strWhereClauseMst_Department = sbSqlMst_Department.ToString();
                    var listMst_Department = List_Mst_Department(strWhereClauseMst_Department);
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
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = userState.TokenID.ToString().Trim(),
                    NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                    OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
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
                    objRT_Sys_UserCur.Lst_Sys_User[0].DepartmentCode = objSys_UserInput.DepartmentCode;
                    objRT_Sys_UserCur.Lst_Sys_User[0].FlagSysAdmin = objSys_UserInput.FlagSysAdmin;
                    objRT_Sys_UserCur.Lst_Sys_User[0].FlagNNTAdmin = objSys_UserInput.FlagNNTAdmin;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_User = TableName.Sys_User + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.UserName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.PhoneNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.DepartmentCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.FlagSysAdmin);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_User, TblSys_User.FlagNNTAdmin);
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
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

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
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = userState.TokenID.ToString().Trim(),
                    NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                    OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };

                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    #region["Danh sách phòng ban"]
                    var strWhereClauseMst_Department = "";
                    var sbSqlMst_Department = new StringBuilder();
                    var Tbl_Mst_Department = TableName.Mst_Department + ".";

                    if (!CUtils.IsNullOrEmpty(FlagActive))
                    {
                        sbSqlMst_Department.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, FlagActive, "=");
                    }
                    strWhereClauseMst_Department = sbSqlMst_Department.ToString();
                    var listMst_Department = List_Mst_Department(strWhereClauseMst_Department);
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
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClause,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        AccessToken = userState.TokenID.ToString().Trim(),
                        NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                        OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
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
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClause,
                                    Ft_Cols_Upd = null,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    AccessToken = userState.TokenID.ToString().Trim(),
                                    NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                                    OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
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
                networkid = CUtils.StrValueOrNull(userState.Mst_NNT.NetworkID);
            }

            var orgid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                orgid = CUtils.StrValueOrNull(userState.Mst_NNT.OrgID);
            }

            var tokenid = "";
            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.OrgID))
            {
                tokenid = CUtils.StrValueOrNull(userState.TokenID);
            }
            try
            {
                var title = "";
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
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClause,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        AccessToken = tokenid,
                        NetworkID = networkid,
                        OrgID = orgid,
                        // RQ_Sys_User
                        Rt_Cols_Sys_User = "*",
                        Rt_Cols_Sys_UserInGroup = null,
                        Sys_User = null,
                    };
                    var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);

                    var userPassword = passold;
                    passold = CUtils.GetEncodedHash(passold);

                    if (!CUtils.IsNullOrEmpty(userPassword) && userPassword == waUserPassword)
                    {
                        if (!CUtils.IsNullOrEmpty(passnew))
                        {
                            passnew = passnew.Trim();
                            var userPasswordNew = passnew;
                            if (!CUtils.IsNullOrEmpty(confpass))
                            {
                                confpass = confpass.Trim();
                                if (passnew.Equals(confpass))
                                {
                                    if (objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                                    {
                                        objRQ_Sys_User.Tid = GetNextTId();
                                        //objRQ_Sys_User.NetworkID = networkid;
                                        //objRQ_Sys_User.OrgID = orgid;
                                        //objRQ_Sys_User.AccessToken = tokenid;

                                        objRQ_Sys_User.Sys_User = new Sys_User()
                                        {
                                            UserCode = waUserCode,
                                            UserPassword = userPassword,
                                            UserPasswordNew = userPasswordNew,
                                        };
                                        Sys_UserService.Instance.WA_Sys_User_ChangePassword(objRQ_Sys_User, addressAPIs);
                                        title = "Thay đổi mật khẩu thành công!";
                                        resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                                        //if (objRQ_Sys_User.Sys_User.UserCode.Equals(userState.SysUser.UserCode))
                                        //{
                                        //    resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                                        //}

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
                        title = "Mật khẩu cũ không đúng!";
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
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export()
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
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = userState.TokenID.ToString().Trim(),
                    NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                    OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
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
                    if (table.Columns.Count != 9)
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
                            if (CUtils.IsNullOrEmpty(dr["UserCode"]))
                            {
                                exitsData = "Mã người dùng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var usercode = CUtils.StrValueOrNull(dr["UserCode"]);
                                if (!CUtils.IsValidEmail(usercode))
                                {
                                    exitsData = "Mã người dùng không hợp lệ!";
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
                            if (CUtils.IsNullOrEmpty(dr[TblSys_User.MST]))
                            {
                                exitsData = "Trực thuộc đơn vị không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblSys_User.DepartmentCode]))
                            {
                                exitsData = "Bộ phận/phòng ban không được trống!";
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
                            if (!CUtils.IsNullOrEmpty(dr["PhoneNo"]))
                            {
                                var phone = dr["PhoneNo"].ToString().Trim();
                                if (phone.Length <= 11)
                                {
                                    if (CUtils.IsNumber(phone))
                                    {
                                        var index = phone.IndexOf(".");
                                        if (index >= 0)
                                        {
                                            exitsData = "Điện thoại không hợp lệ!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Điện thoại không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                else
                                {
                                    exitsData = "Điện thoại không hợp lệ!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
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
                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var userCodeCur = table.Rows[i]["UserCode"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _userCodeCur = table.Rows[j]["UserCode"].ToString().Trim();
                                    if (userCodeCur.Equals(_userCodeCur))
                                    {
                                        exitsData = "Mã người dùng '" + userCodeCur + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<Sys_User>(table); ;
                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
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
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                AccessToken = userState.TokenID.ToString().Trim(),
                                NetworkID = userState.Mst_NNT.NetworkID.ToString().Trim(),
                                OrgID = userState.Mst_NNT.OrgID.ToString().Trim(),
                                // RQ_Sys_User
                                Rt_Cols_Sys_User = null,
                                Rt_Cols_Sys_UserInGroup = null,
                                Sys_User = item,
                            };

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

        #region
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"UserCode","Mã người dùng"},                 
                 //{"UserPassword","Mật khẩu"},
                 {"UserName","Tên người dùng"},
                 {"MST","Mã đơn vị"},
                 {"DepartmentCode","Bộ phận/Phòng ban"},
                 {"EMail","Email"},
                 {"PhoneNo","Điện thoại"},
                 {"FlagNNTAdmin","Cờ Quản trị đơn vị"},
                 {"FlagSysAdmin","Cờ Quản trị HT"},
                 {"FlagActive","Trạng thái"}
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"UserCode","Mã người dùng"},
                 {"UserPassword","Mật khẩu"},
                 {"UserName","Tên người dùng"},
                 {"MST","Trực thuộc đơn vị"},
                 {"DepartmentCode","Bộ phận/Phòng ban"},
                 {"PhoneNo","Điện thoại"},
                 {"FlagNNTAdmin","Cờ Quản trị đơn vị"},
                 {"FlagSysAdmin","Cờ Quản trị HT"},
                 //{"FlagActive","Trạng thái"}
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
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserName, "%" + data.UserName.ToString().Trim() + "%", "like");
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
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValueOrNull(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValueOrNull(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValueOrNull(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValueOrNull(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValueOrNull(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
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
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValueOrNull(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValueOrNull(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValueOrNull(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValueOrNull(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValueOrNull(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }


            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        //private string strWhereClause_MstOrgan(Mst_Organ data)
        //{
        //    var strWhereClause = "";
        //    //var sbSql = new StringBuilder();
        //    //var Tbl_Mst_Organ = TableName.Mst_Organ + ".";
        //    //if (!CUtils.IsNullOrEmpty(data.OrganCode))
        //    //{
        //    //    sbSql.AddWhereClause(Tbl_Mst_Organ + TblMst_Organ.OrganCode, data.OrganCode.Trim(), "=");
        //    //}
        //    //if (!CUtils.IsNullOrEmpty(FlagActive))
        //    //{
        //    //    sbSql.AddWhereClause(Tbl_Mst_Organ + TblMst_Organ.FlagActive, FlagActive.Trim(), "=");
        //    //}
        //    //strWhereClause = sbSql.ToString();
        //    return strWhereClause;
        //}

        //private string strWhereClause_Map_UserInOrgan(Map_UserInOrgan data)
        //{
        //    var strWhereClause = "";
        //    //var sbSql = new StringBuilder();
        //    //var Tbl_Map_UserInOrgan = TableName.Map_UserInOrgan + ".";
        //    //if (!CUtils.IsNullOrEmpty(data.UserCode))
        //    //{
        //    //    sbSql.AddWhereClause(Tbl_Map_UserInOrgan + TblMap_UserInOrgan.UserCode, data.UserCode.Trim(), "=");
        //    //}
        //    //if (!CUtils.IsNullOrEmpty(data.OrganCode))
        //    //{
        //    //    sbSql.AddWhereClause(Tbl_Map_UserInOrgan + TblMap_UserInOrgan.OrganCode, data.OrganCode.Trim(), "=");
        //    //}

        //    //strWhereClause = sbSql.ToString();
        //    return strWhereClause;
        //}
        #endregion
    }
}