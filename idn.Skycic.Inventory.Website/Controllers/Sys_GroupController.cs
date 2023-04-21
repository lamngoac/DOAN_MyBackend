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

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Sys_GroupController : AdminBaseController
    {
        // GET: Sys_Group
        public ActionResult Index(string groupname, string init = "init", int recordcount = 10, int page = 0)
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_NHOM_NGUOI_DUNG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.PageCur = page.ToString();
            ViewBag.Recordcount = recordcount;
            var stringBuilder = new StringBuilder();
            var PathFileLog = "";
            CUtils.StrAppend(stringBuilder, "Màn hình Nhóm người dùng:");
            CUtils.StrAppend(stringBuilder, "Method: Get");
            if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PathFileLog"]))
            {
                PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
                var iindex = PathFileLog.IndexOf("File_Sys_Group");
                if (iindex < 0)
                {
                    var iCount = ReturnCount();
                    CreateFileWriteLog("File_Sys_Group", Today, iCount);
                }
            }
            else
            {
                var iCount = ReturnCount();
                CreateFileWriteLog("File_Sys_Group", Today, iCount);
            }

            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.CurrentUser = userState.SysUser;
            ViewBag.UserState = this.UserState;
            var pageInfo = new PageInfo<Sys_Group>(0, recordcount)
            {
                DataList = new List<Sys_Group>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objSys_Group = new Sys_Group()
                {
                    GroupName = groupname,
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroupName(objSys_Group);

                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Sys_GroupCur.MySummaryTable.MyCount);
                }
                if (objRT_Sys_GroupCur != null && objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Sys_GroupCur.Lst_Sys_Group);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.GroupName = groupname;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }

        #region["Load Detail"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDetailData(string groupcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var listUserInGroup = new List<Sys_UserInGroup>();
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroup(groupcode);

                var objRQ_Sys_Group = new RQ_Sys_Group()
                {
                    // WARQBase
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
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = null,
                    Rt_Cols_Sys_UserInGroup = "*",
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_UserInGroup != null && objRT_Sys_GroupCur.Lst_Sys_UserInGroup.Count > 0)
                {
                    listUserInGroup.AddRange(objRT_Sys_GroupCur.Lst_Sys_UserInGroup);
                }
                return JsonView("ShowDetailData", listUserInGroup);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowDetailData", null, resultEntry);
        }
        #endregion

        #region["Tạo mới nhóm người dùng"]
        [HttpGet]
        public ActionResult Create()
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NHOM_NGUOI_DUNG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            return View();
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
            var objSys_Group = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_Group>(model);
            var objRQ_Sys_Group = new RQ_Sys_Group()
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
                // RQ_Sys_Group
                Rt_Cols_Sys_Group = null,
                Rt_Cols_Sys_UserInGroup = null,
                Sys_Group = objSys_Group,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_Group);
            Sys_GroupService.Instance.WA_Sys_Group_Create(objRQ_Sys_Group, addressAPIs);

            resultEntry.Success = true;
            resultEntry.AddMessage("Tạo nhóm người dùng thành công!");
            resultEntry.RedirectUrl = Url.Action("Index");
            return Json(resultEntry);
        }
        #endregion

        #region["Sửa nhóm người dùng"]
        [HttpGet]
        public ActionResult Update(string groupcode)
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NHOM_NGUOI_DUNG_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(groupcode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroup(groupcode);

                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                {
                    return View(objRT_Sys_GroupCur.Lst_Sys_Group[0]);
                }
            }
            return View(new Sys_Group());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objSys_Group = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_Group>(model);

            if (model != null && !CUtils.IsNullOrEmpty(objSys_Group.GroupCode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroup(objSys_Group.GroupCode);
                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_Group = null,
                };
                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                {
                    objRT_Sys_GroupCur.Lst_Sys_Group[0].GroupName = objSys_Group.GroupName;
                    objRT_Sys_GroupCur.Lst_Sys_Group[0].FlagActive = objSys_Group.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_Group = TableName.Sys_Group + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Group, TblSys_Group.GroupName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Group, TblSys_Group.FlagActive);

                    objRQ_Sys_Group.Ft_WhereClause = null;
                    objRQ_Sys_Group.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_Group.Rt_Cols_Sys_Group = null;
                    objRQ_Sys_Group.Sys_Group = objRT_Sys_GroupCur.Lst_Sys_Group[0];

                    Sys_GroupService.Instance.WA_Sys_Group_Update(objRQ_Sys_Group, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa nhóm người dùng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm người dùng '" + objSys_Group.GroupCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã nhóm người dùng trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string groupcode)
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NHOM_NGUOI_DUNG_CHI_TIET");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(groupcode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroup(groupcode);

                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                {
                    return View(objRT_Sys_GroupCur.Lst_Sys_Group[0]);
                }
            }
            return View(new Sys_Group());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string groupcode = "")
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
                if (!CUtils.IsNullOrEmpty(groupcode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_SysGroup(groupcode);

                    var objRQ_Sys_Group = new RQ_Sys_Group()
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
                        // RQ_Sys_Group
                        Rt_Cols_Sys_Group = "*",
                        Rt_Cols_Sys_UserInGroup = null,
                        Sys_Group = null,
                    };

                    var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                    if (objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                    {
                        objRQ_Sys_Group.Ft_WhereClause = null;
                        objRQ_Sys_Group.Ft_Cols_Upd = null;
                        objRQ_Sys_Group.Rt_Cols_Sys_Group = null;
                        objRQ_Sys_Group.Sys_Group = objRT_Sys_GroupCur.Lst_Sys_Group[0];
                        Sys_GroupService.Instance.WA_Sys_Group_Delete(objRQ_Sys_Group, addressAPIs);
                        title = "Xóa nhóm người dùng '" + groupcode + "' thành công!";

                        resultEntry.Success = true;
                        resultEntry.AddMessage(title);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        title = "Mã nhóm người dùng '" + groupcode + "' không có trong hệ thống!";
                        resultEntry.AddMessage(title);
                    }
                }
                else
                {
                    title = "Mã nhóm người dùng trống!";
                    resultEntry.AddMessage(title);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);

        }
        #endregion

        #region["Phân quyền"]
        #region["01.Gán người dùng vào nhóm"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSysUser(string groupcode = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listSysUser = new List<Sys_User>();
                var listUserInGroup = new List<Sys_UserInGroup>();
                #region["Danh sách người dùng trạng thái Active"]
                var sysUser = new Sys_User()
                {
                    FlagActive = Client_Flag.Active,
                };
                var strWhereClauseSysUser = strWhereClause_SysUser(sysUser);
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
                    Ft_WhereClause = strWhereClauseSysUser,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = "",
                    Sys_User = null,
                };
                var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Get(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_User.Lst_Sys_User != null && objRT_Sys_User.Lst_Sys_User.Count > 0)
                {
                    listSysUser.AddRange(objRT_Sys_User.Lst_Sys_User);
                }
                #endregion

                #region["Danh sách người dùng trong nhóm"]
                var strWhereClauseSysGroup = "";
                strWhereClauseSysGroup = strWhereClause_SysGroup(groupcode);

                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    Ft_WhereClause = strWhereClauseSysGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = null,
                    Rt_Cols_Sys_UserInGroup = "*",
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_UserInGroup != null && objRT_Sys_GroupCur.Lst_Sys_UserInGroup.Count > 0)
                {
                    listUserInGroup.AddRange(objRT_Sys_GroupCur.Lst_Sys_UserInGroup);
                }
                #endregion
                ViewBag.GroupCode = groupcode;
                ViewBag.ListUserInGroup = listUserInGroup;
                return JsonView("GetSysUser", listSysUser);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysUser", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUserInGroup(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Sys_UserInGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Sys_UserInGroup>(model);
                if (objRQ_Sys_UserInGroup != null)
                {
                    objRQ_Sys_UserInGroup.Tid = GetNextTId();
                    objRQ_Sys_UserInGroup.GwUserCode = GwUserCode;
                    objRQ_Sys_UserInGroup.GwPassword = GwPassword;
                    objRQ_Sys_UserInGroup.AccessToken = CUtils.StrValue(UserState.AccessToken);
                    objRQ_Sys_UserInGroup.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Sys_UserInGroup.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Sys_UserInGroup.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                    objRQ_Sys_UserInGroup.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    objRQ_Sys_UserInGroup.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                    objRQ_Sys_UserInGroup.FuncType = null;
                    objRQ_Sys_UserInGroup.Ft_RecordStart = Ft_RecordStart;
                    objRQ_Sys_UserInGroup.Ft_RecordCount = Ft_RecordCount;
                    objRQ_Sys_UserInGroup.Ft_WhereClause = null;
                    objRQ_Sys_UserInGroup.Ft_Cols_Upd = null;


                    var title = "Gán người dùng vào nhóm thành công!";
                    if (objRQ_Sys_UserInGroup.Lst_Sys_UserInGroup == null || objRQ_Sys_UserInGroup.Lst_Sys_UserInGroup.Count == 0)
                    {
                        objRQ_Sys_UserInGroup.Lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                        title = "Xóa người dùng khỏi nhóm thành công!";
                    }
                    Sys_UserInGroupService.Instance.WA_Sys_UserInGroup_Save(objRQ_Sys_UserInGroup, addressAPIs);
                    return Json(new { Success = true, Title = title });
                }
                else
                {
                    return Json(new { Success = true, Title = "Chưa chọn người dùng gán vào nhóm!" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysUser", null, resultEntry);
        }
        #endregion
        #region["02.Phân quyền Menu - Button cho nhóm người dùng"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSysObject(string groupcode = "")
        {
            Stopwatch stGetSysObject = new Stopwatch();
            Stopwatch stSys_Object_Get = new Stopwatch();
            Stopwatch stSys_Object_Get_biz = new Stopwatch();
            Stopwatch stSys_Access_Get = new Stopwatch();
            Stopwatch stSys_Access_Get_biz = new Stopwatch();

            if (CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PathFileLog"]))
            {
                var iCount = ReturnCount();
                CreateFileWriteLog("File_Gan_Menu_Button", Today, iCount);

            }
            var stringBuilder = new StringBuilder();
            CUtils.StrAppend(stringBuilder, "Gán menu - button:");
            CUtils.StrAppend(stringBuilder, "Method: Post");

            stGetSysObject.Reset();
            stGetSysObject.Start();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listSysObject = new List<Sys_Object>();
                var listSysAccess = new List<Sys_Access>();

                stSys_Object_Get.Reset();
                stSys_Object_Get.Start();
                #region["Danh sách Menu - Button"]
                var sysObject = new Sys_Object()
                {
                    ObjectType = "FUNC",
                    FlagActive = Client_Flag.Active,
                };
                var strWhereClauseSysObject = strWhereClause_SysObject(sysObject);
                var objRQ_Sys_Object = new RQ_Sys_Object()
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
                    Ft_WhereClause = strWhereClauseSysObject,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_Object
                    Rt_Cols_Sys_Object = "*",
                };

                stSys_Object_Get_biz.Reset();
                stSys_Object_Get_biz.Start();
                var objRT_Sys_Object = Sys_ObjectService.Instance.WA_Sys_Object_Get(objRQ_Sys_Object, addressAPIs);
                stSys_Object_Get_biz.Stop();
                long timestSys_Object_Get_biz = stSys_Object_Get_biz.ElapsedMilliseconds;
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button (biz):" + timestSys_Object_Get_biz);

                if (objRT_Sys_Object.Lst_Sys_Object != null && objRT_Sys_Object.Lst_Sys_Object.Count > 0)
                {
                    listSysObject.AddRange(objRT_Sys_Object.Lst_Sys_Object);
                }
                #endregion
                stSys_Object_Get.Stop();
                long timestSys_Object_Get = stSys_Object_Get.ElapsedMilliseconds;
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button (client):" + (timestSys_Object_Get - timestSys_Object_Get_biz));
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button:" + timestSys_Object_Get);

                stSys_Access_Get.Reset();
                stSys_Access_Get.Start();
                #region["Danh sách Menu - Button gán vào nhóm"]
                var sysAccess = new Sys_Access()
                {
                    GroupCode = groupcode,
                    ObjectCode = "",
                };
                var strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccess);
                var objRQ_Sys_Access = new RQ_Sys_Access()
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
                    Ft_WhereClause = strWhereClauseSysAccess,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_Access
                    Rt_Cols_Sys_Access = "*",
                    Sys_Group = null,
                    Lst_Sys_Access = null,
                };

                stSys_Access_Get_biz.Reset();
                stSys_Access_Get_biz.Start();
                var objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access, addressAPIs);
                stSys_Access_Get_biz.Stop();
                long timestSys_Access_Get_biz = stSys_Access_Get_biz.ElapsedMilliseconds;
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button gán vào nhóm (biz):" + timestSys_Access_Get_biz);

                if (objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                {
                    listSysAccess.AddRange(objRT_Sys_Access.Lst_Sys_Access);
                }
                #endregion
                stSys_Access_Get.Stop();
                long timestSys_Access_Get = stSys_Access_Get.ElapsedMilliseconds;
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button gán vào nhóm (client):" + (timestSys_Access_Get - timestSys_Access_Get_biz));
                CUtils.StrAppend(stringBuilder, "Danh sách menu-button gán vào nhóm:" + timestSys_Access_Get);

                ViewBag.GroupCode = groupcode;
                ViewBag.ListSysObject = listSysObject;
                ViewBag.ListSysAccess = listSysAccess;

                stGetSysObject.Stop();
                long timestGetSysObject = stGetSysObject.ElapsedMilliseconds;
                CUtils.StrAppend(stringBuilder, "Tổng thời gian:" + timestGetSysObject);
                var strLogs = stringBuilder.ToString();
                var PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
                System.IO.File.AppendAllText(PathFileLog, strLogs, Encoding.UTF8);
                stringBuilder.Clear();
                strLogs = "";

                return JsonView("GetSysObject", listSysObject);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysObject", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveObjectInGroup(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var listObjectCode = new List<string>();
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Sys_Access = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Sys_Access>(model);
                if (objRQ_Sys_Access != null)
                {
                    objRQ_Sys_Access.Tid = GetNextTId();
                    objRQ_Sys_Access.GwUserCode = GwUserCode;
                    objRQ_Sys_Access.GwPassword = GwPassword;
                    objRQ_Sys_Access.AccessToken = CUtils.StrValue(UserState.AccessToken);
                    objRQ_Sys_Access.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Sys_Access.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Sys_Access.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                    objRQ_Sys_Access.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    objRQ_Sys_Access.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                    objRQ_Sys_Access.FuncType = null;
                    objRQ_Sys_Access.Ft_RecordStart = Ft_RecordStart;
                    objRQ_Sys_Access.Ft_RecordCount = Ft_RecordCount;
                    objRQ_Sys_Access.Ft_WhereClause = null;
                    objRQ_Sys_Access.Ft_Cols_Upd = null;
                    objRQ_Sys_Access.WAUserCode = waUserCode;
                    objRQ_Sys_Access.WAUserPassword = waUserPassword;

                    var title = "Gán module vào nhóm người dùng thành công!";
                    if (objRQ_Sys_Access.Lst_Sys_Access == null || objRQ_Sys_Access.Lst_Sys_Access.Count == 0)
                    {
                        objRQ_Sys_Access.Lst_Sys_Access = new List<Sys_Access>();
                        title = "Xóa module khỏi nhóm thành công!";
                    }
                    else
                    {
                        var listFunctions = new List<string>();
                        int a = objRQ_Sys_Access.Lst_Sys_Access.Count();
                        List<Sys_Access> distinct = objRQ_Sys_Access.Lst_Sys_Access.Distinct().ToList();
                        int b = distinct.Count();
                        int c = a - b;
                        foreach (var item in objRQ_Sys_Access.Lst_Sys_Access)
                        {
                            if (item.so_ObjectType.Equals("MENU"))
                            {
                                listObjectCode.Add(item.ObjectCode);
                            }
                            else if (item.so_ObjectType.Equals("BUTTON"))
                            {
                                listFunctions.Add(item.ObjectCode);
                            }
                        }
                        var functions = ResolveObjects(listObjectCode);
                        if (functions != null && functions.Count > 0)
                        {
                            if (listFunctions != null && listFunctions.Count > 0)
                            {
                                functions.AddRange(listFunctions);
                            }
                            functions = functions.Distinct().ToList();
                            objRQ_Sys_Access.Lst_Sys_Access = new List<Sys_Access>();
                            var listSysAccess = new List<Sys_Access>();
                            foreach (var it in functions)
                            {
                                var sysAccess = new Sys_Access()
                                {
                                    ObjectCode = it.Trim(),
                                    GroupCode = objRQ_Sys_Access.Sys_Group.GroupCode.Trim(),
                                    //ObjectCode = it.ToUpper().Trim(),
                                    //GroupCode = objRQ_Sys_Access.Sys_Group.GroupCode.ToUpper().Trim(),
                                };
                                listSysAccess.Add(sysAccess);
                            }
                            objRQ_Sys_Access.Lst_Sys_Access.AddRange(listSysAccess);
                        }
                    }
                    Sys_AccessService.Instance.WA_Sys_Access_Save(objRQ_Sys_Access, addressAPIs);

                    return Json(new { Success = true, Title = title });
                }
                else
                {
                    return Json(new { Success = true, Title = "Chưa chọn Menu - Button gán vào nhóm người dùng!" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysObject", null, resultEntry);
        }
        #endregion
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
                var list = new List<Sys_Group>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 2)
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
                            if (CUtils.IsNullOrEmpty(dr["GroupCode"]))
                            {
                                exitsData = "Mã nhóm người dùng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr["GroupName"]))
                            {
                                exitsData = "Tên nhóm người dùng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var groupCodeCur = table.Rows[i]["GroupCode"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _groupCodeCur = table.Rows[j]["GroupCode"].ToString().Trim();
                                    if (groupCodeCur.Equals(_groupCodeCur))
                                    {
                                        exitsData = "Mã nhóm người dùng '" + groupCodeCur + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<Sys_Group>(table);
                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
                            var objRQ_Sys_Group = new RQ_Sys_Group()
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
                                // RQ_Sys_Group
                                Rt_Cols_Sys_Group = null,
                                Rt_Cols_Sys_UserInGroup = null,
                                Sys_Group = item,
                            };
                            Sys_GroupService.Instance.WA_Sys_Group_Create(objRQ_Sys_Group, addressAPIs);
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

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string groupname="")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var list = new List<Sys_Group>();
                var strWhereClauseSys_Group = strWhereClause_SysGroupName(groupname);
                var objRQ_Sys_Group = new RQ_Sys_Group()
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
                    Ft_WhereClause = strWhereClauseSys_Group,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_Group
                    Rt_Cols_Sys_Group = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_Group = null,
                };

                var objRT_Sys_GroupCur = Sys_GroupService.Instance.WA_Sys_Group_Get(objRQ_Sys_Group, addressAPIs);
                if (objRT_Sys_GroupCur.Lst_Sys_Group != null && objRT_Sys_GroupCur.Lst_Sys_Group.Count > 0)
                {
                    list.AddRange(objRT_Sys_GroupCur.Lst_Sys_Group);

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_Group).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_Group"));

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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Sys_Group>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_Group).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_Group"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"MST", "Đơn vị" },
                 {"GroupCode","Mã nhóm người dùng"},
                 {"GroupName","Tên nhóm người dùng"},
                 {"FlagActive","Trạng thái"}
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"GroupCode","Mã nhóm người dùng (*)"},
                 {"GroupName","Tên nhóm người dùng (*)"},
            };
        }
        
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_SysGroup(string groupcode)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Group = TableName.Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(groupcode))
            {
                sbSql.AddWhereClause(Tbl_Sys_Group + TblSys_Group.GroupCode, groupcode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysGroupName(string groupname)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Group = TableName.Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(groupname))
            {
                sbSql.AddWhereClause(Tbl_Sys_Group + TblSys_Group.GroupName, "%" + groupname + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysGroupName(Sys_Group data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Group = TableName.Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(data.GroupName))
            {
                sbSql.AddWhereClause(Tbl_Sys_Group + TblSys_Group.GroupName, "%" + data.GroupName.ToString().Trim() + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysUser(Sys_User data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(data.UserCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserCode, data.UserCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.FlagActive, data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.OrgID, data.OrgID, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysObject(Sys_Object data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Object = TableName.Sys_Object + ".";
            if (!CUtils.IsNullOrEmpty(data.ObjectType))
            {
                sbSql.AddWhereClause(Tbl_Sys_Object + TblSys_Object.ObjectType, data.ObjectType, "!=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Sys_Object + TblSys_User.FlagActive, data.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysAccess(Sys_Access data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Access = TableName.Sys_Access + ".";
            if (!CUtils.IsNullOrEmpty(data.GroupCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_Access + TblSys_Access.GroupCode, data.GroupCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ObjectCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_Access + TblSys_Access.ObjectCode, data.ObjectCode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}