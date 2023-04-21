using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Utils;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Constants;
using System.Data;
using System.Text;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.WebAdmin.Controllers;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class RptSv_Sys_GroupOnlyAssignController : BaseController
    {
        // GET: RptSv_Sys_GroupOnlyAssign
        public ActionResult Index(string groupname, string init = "init", int RecordCount = 10, int page = 0)
        {
            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            ViewBag.CurrentUser = userState.RptSv_Sys_User;
            var pageInfo = new PageInfo<RptSv_Sys_Group>(0, RecordCount)
            {
                DataList = new List<RptSv_Sys_Group>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objRptSv_Sys_Group = new RptSv_Sys_Group()
                {
                    GroupName = groupname,
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroupName(objRptSv_Sys_Group);

                var objRQ_RptSv_Sys_Group = new RQ_RptSv_Sys_Group()
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
                    // RQ_RptSv_Sys_Group
                    Rt_Cols_RptSv_Sys_Group = "*",
                    Rt_Cols_RptSv_Sys_UserInGroup = null,
                    RptSv_Sys_Group = null,
                };

                var objRT_RptSv_Sys_GroupCur = RptSv_Sys_GroupService.Instance.WA_RptSv_Sys_Group_Get(objRQ_RptSv_Sys_Group, addressAPIs);
                if (objRT_RptSv_Sys_GroupCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_RptSv_Sys_GroupCur.MySummaryTable.MyCount);
                }
                if (objRT_RptSv_Sys_GroupCur != null && objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_Group != null && objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_Group.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_Group);
                    pageCount = (itemCount % RecordCount == 0) ? itemCount / RecordCount : itemCount / RecordCount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.GroupName = groupname;
            pageInfo.PageSize = RecordCount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * RecordCount).ToString();
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
                var waUserCode = userState.RptSv_Sys_User.UserCode;
                var waUserPassword = userState.RptSv_Sys_User.UserPassword;
                var listUserInGroup = new List<RptSv_Sys_UserInGroup>();
                var strWhereClause = "";
                strWhereClause = strWhereClause_SysGroup(groupcode);

                var objRQ_RptSv_Sys_Group = new RQ_RptSv_Sys_Group()
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
                    // RQ_RptSv_Sys_Group
                    Rt_Cols_RptSv_Sys_Group = null,
                    Rt_Cols_RptSv_Sys_UserInGroup = "*",
                    RptSv_Sys_Group = null,
                };

                var objRT_RptSv_Sys_GroupCur = RptSv_Sys_GroupService.Instance.WA_RptSv_Sys_Group_Get(objRQ_RptSv_Sys_Group, addressAPIs);
                if (objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup != null && objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup.Count > 0)
                {
                    listUserInGroup.AddRange(objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup);
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

        #region["Phân quyền"]
        #region["01.Gán người dùng vào nhóm"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSysUser(string groupcode = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listSysUser = new List<RptSv_Sys_User>();
                var listUserInGroup = new List<RptSv_Sys_UserInGroup>();
                #region["Danh sách người dùng trạng thái Active"]
                var sysUser = new RptSv_Sys_User()
                {
                    FlagActive = FlagActive,
                };
                var strWhereClauseSysUser = strWhereClause_SysUser(sysUser);
                var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSysUser,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_RptSv_Sys_User
                    Rt_Cols_RptSv_Sys_User = "*",
                    Rt_Cols_RptSv_Sys_UserInGroup = "",
                    RptSv_Sys_User = null,
                };
                var objRT_Sys_User = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);
                if (objRT_Sys_User.Lst_RptSv_Sys_User != null && objRT_Sys_User.Lst_RptSv_Sys_User.Count > 0)
                {
                    listSysUser.AddRange(objRT_Sys_User.Lst_RptSv_Sys_User);
                }
                #endregion

                #region["Danh sách người dùng trong nhóm"]
                var strWhereClauseSysGroup = "";
                strWhereClauseSysGroup = strWhereClause_SysGroup(groupcode);

                var objRQ_RptSv_Sys_Group = new RQ_RptSv_Sys_Group()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSysGroup,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_RptSv_Sys_Group
                    Rt_Cols_RptSv_Sys_Group = null,
                    Rt_Cols_RptSv_Sys_UserInGroup = "*",
                    RptSv_Sys_Group = null,
                };

                var objRT_RptSv_Sys_GroupCur = RptSv_Sys_GroupService.Instance.WA_RptSv_Sys_Group_Get(objRQ_RptSv_Sys_Group, addressAPIs);
                if (objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup != null && objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup.Count > 0)
                {
                    listUserInGroup.AddRange(objRT_RptSv_Sys_GroupCur.Lst_RptSv_Sys_UserInGroup);
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
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_RptSv_Sys_UserInGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_RptSv_Sys_UserInGroup>(model);
                if (objRQ_RptSv_Sys_UserInGroup != null)
                {
                    objRQ_RptSv_Sys_UserInGroup.Tid = GetNextTId();
                    objRQ_RptSv_Sys_UserInGroup.GwUserCode = GwUserCode;
                    objRQ_RptSv_Sys_UserInGroup.GwPassword = GwPassword;
                    objRQ_RptSv_Sys_UserInGroup.FuncType = null;
                    objRQ_RptSv_Sys_UserInGroup.Ft_RecordStart = Ft_RecordStart;
                    objRQ_RptSv_Sys_UserInGroup.Ft_RecordCount = Ft_RecordCount;
                    objRQ_RptSv_Sys_UserInGroup.Ft_WhereClause = null;
                    objRQ_RptSv_Sys_UserInGroup.Ft_Cols_Upd = null;
                    objRQ_RptSv_Sys_UserInGroup.WAUserCode = waUserCode;
                    objRQ_RptSv_Sys_UserInGroup.WAUserPassword = waUserPassword;


                    var title = "Gán người dùng vào nhóm thành công!";
                    if (objRQ_RptSv_Sys_UserInGroup.Lst_RptSv_Sys_UserInGroup == null || objRQ_RptSv_Sys_UserInGroup.Lst_RptSv_Sys_UserInGroup.Count == 0)
                    {
                        objRQ_RptSv_Sys_UserInGroup.Lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
                        title = "Xóa người dùng khỏi nhóm thành công!";
                    }
                    RptSv_Sys_UserInGroupService.Instance.WA_RptSv_Sys_UserInGroup_Save(objRQ_RptSv_Sys_UserInGroup, addressAPIs);
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
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listSysObject = new List<RptSv_Sys_Object>();
                var listSysAccess = new List<RptSv_Sys_Access>();
                #region["Danh sách Menu - Button"]
                var sysObject = new RptSv_Sys_Object()
                {
                    ObjectType = "FUNC",
                    FlagActive = FlagActive,
                };
                var strWhereClauseSysObject = strWhereClause_SysObject(sysObject);
                var objRQ_RptSv_Sys_Object = new RQ_RptSv_Sys_Object()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSysObject,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_RptSv_Sys_Object
                    Rt_Cols_RptSv_Sys_Object = "*",
                };

                var objRT_RptSv_Sys_Object = RptSv_Sys_ObjectService.Instance.WA_RptSv_Sys_Object_Get(objRQ_RptSv_Sys_Object, addressAPIs);
                if (objRT_RptSv_Sys_Object.Lst_RptSv_Sys_Object != null && objRT_RptSv_Sys_Object.Lst_RptSv_Sys_Object.Count > 0)
                {
                    listSysObject.AddRange(objRT_RptSv_Sys_Object.Lst_RptSv_Sys_Object);
                }
                #endregion

                #region["Danh sách Menu - Button gán vào nhóm"]
                var sysAccess = new RptSv_Sys_Access()
                {
                    GroupCode = groupcode,
                    ObjectCode = "",
                };
                var strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccess);
                var objRQ_RptSv_Sys_Access = new RQ_RptSv_Sys_Access()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSysAccess,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_RptSv_Sys_Access
                    Rt_Cols_RptSv_Sys_Access = "*",
                    RptSv_Sys_Group = null,
                    Lst_RptSv_Sys_Access = null,
                };

                var objRT_Sys_Access = RptSv_Sys_AccessService.Instance.WA_RptSv_Sys_Access_Get(objRQ_RptSv_Sys_Access, addressAPIs);
                if (objRT_Sys_Access.Lst_RptSv_Sys_Access != null && objRT_Sys_Access.Lst_RptSv_Sys_Access.Count > 0)
                {
                    listSysAccess.AddRange(objRT_Sys_Access.Lst_RptSv_Sys_Access);
                }
                #endregion
                ViewBag.GroupCode = groupcode;
                ViewBag.ListSysObject = listSysObject;
                ViewBag.ListSysAccess = listSysAccess;
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
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var listObjectCode = new List<string>();
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_RptSv_Sys_Access = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_RptSv_Sys_Access>(model);
                if (objRQ_RptSv_Sys_Access != null)
                {
                    objRQ_RptSv_Sys_Access.Tid = GetNextTId();
                    objRQ_RptSv_Sys_Access.GwUserCode = GwUserCode;
                    objRQ_RptSv_Sys_Access.GwPassword = GwPassword;
                    objRQ_RptSv_Sys_Access.FuncType = null;
                    objRQ_RptSv_Sys_Access.Ft_RecordStart = Ft_RecordStart;
                    objRQ_RptSv_Sys_Access.Ft_RecordCount = Ft_RecordCount;
                    objRQ_RptSv_Sys_Access.Ft_WhereClause = null;
                    objRQ_RptSv_Sys_Access.Ft_Cols_Upd = null;
                    objRQ_RptSv_Sys_Access.WAUserCode = waUserCode;
                    objRQ_RptSv_Sys_Access.WAUserPassword = waUserPassword;

                    var title = "Gán module vào nhóm người dùng thành công!";
                    if (objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access == null || objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access.Count == 0)
                    {
                        objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access = new List<RptSv_Sys_Access>();
                        title = "Xóa module khỏi nhóm thành công!";
                    }
                    else
                    {
                        var listFunctions = new List<string>();
                        int a = objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access.Count();
                        List<RptSv_Sys_Access> distinct = objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access.Distinct().ToList();
                        int b = distinct.Count();
                        int c = a - b;
                        foreach (var item in objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access)
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
                            objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access = new List<RptSv_Sys_Access>();
                            var listSysAccess = new List<RptSv_Sys_Access>();
                            foreach (var it in functions)
                            {
                                var sysAccess = new RptSv_Sys_Access()
                                {
                                    ObjectCode = it.Trim(),
                                    GroupCode = objRQ_RptSv_Sys_Access.RptSv_Sys_Group.GroupCode.Trim(),
                                    //ObjectCode = it.ToUpper().Trim(),
                                    //GroupCode = objRQ_RptSv_Sys_Access.RptSv_Sys_Group.GroupCode.ToUpper().Trim(),
                                };
                                listSysAccess.Add(sysAccess);
                            }
                            objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access.AddRange(listSysAccess);
                        }
                    }
                    RptSv_Sys_AccessService.Instance.WA_RptSv_Sys_Access_Save(objRQ_RptSv_Sys_Access, addressAPIs);

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

        #region["strWhereClause"]
        private string strWhereClause_SysGroup(string groupcode)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_Group = TableName.RptSv_Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(groupcode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Group + TblRptSv_Sys_Group.GroupCode, groupcode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysGroupName(string groupname)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_Group = TableName.RptSv_Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(groupname))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Group + TblRptSv_Sys_Group.GroupName, "%" + groupname + "%", "like");               
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysGroupName(RptSv_Sys_Group data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_Group = TableName.RptSv_Sys_Group + ".";
            if (!CUtils.IsNullOrEmpty(data.GroupName))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Group + TblRptSv_Sys_Group.GroupName, "%" + data.GroupName.ToString().Trim() + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysUser(RptSv_Sys_User data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_User = TableName.RptSv_Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(data.UserCode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.UserCode, data.UserCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(FlagActive))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.FlagActive, FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysObject(RptSv_Sys_Object data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_Object = TableName.RptSv_Sys_Object + ".";
            if (!CUtils.IsNullOrEmpty(data.ObjectType))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Object + TblRptSv_Sys_Object.ObjectType, data.ObjectType, "!=");
            }
            if (!CUtils.IsNullOrEmpty(FlagActive))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Object + TblSys_User.FlagActive, FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_SysAccess(RptSv_Sys_Access data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_Access = TableName.RptSv_Sys_Access + ".";
            if (!CUtils.IsNullOrEmpty(data.GroupCode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Access + TblRptSv_Sys_Access.GroupCode, data.GroupCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ObjectCode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_Access + TblRptSv_Sys_Access.ObjectCode, data.ObjectCode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
       
    }
}