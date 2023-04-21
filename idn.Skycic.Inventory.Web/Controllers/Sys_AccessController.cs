using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Sys_AccessController : AdminBaseController
    {
        // GET: Sys_Access
        public ActionResult Index()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objectCode = "";
            var listSysObject = new List<Sys_Object>();
            var listSysAccess = new List<Sys_Access>();
            var sysObject = new Sys_Object()
            {
                ObjectType = "MENU",
                FlagActive = FlagActive,
            };
            var strWhereClauseSysObject = strWhereClause_SysObject(sysObject);
            // Danh sách Menu
            var objRQ_Sys_Object = new RQ_Sys_Object()
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
                // RQ_Sys_Object
                Rt_Cols_Sys_Object = "*",
            };
            var objRT_Sys_Object = Sys_ObjectService.Instance.WA_Sys_Object_Get(objRQ_Sys_Object, addressAPIs);
            if (objRT_Sys_Object.Lst_Sys_Object != null && objRT_Sys_Object.Lst_Sys_Object.Count > 0)
            {
                objectCode = objRT_Sys_Object.Lst_Sys_Object[0].ObjectCode.Trim();
                listSysObject.AddRange(objRT_Sys_Object.Lst_Sys_Object);

                #region["Lấy danh sách nhóm người dùng có menu được gán"]
                var sysAccess = new Sys_Access()
                {
                    GroupCode = "",
                    ObjectCode = objectCode,
                };
                var strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccess);
                var objRQ_Sys_Access = new RQ_Sys_Access()
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
                    // RQ_Sys_Access
                    Rt_Cols_Sys_Access = "*",
                    Sys_Group = null,
                    Lst_Sys_Access = null,
                };

                var objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access, addressAPIs);
                if (objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                {
                    listSysAccess.AddRange(objRT_Sys_Access.Lst_Sys_Access);
                }
                #endregion
            }
            ViewBag.ListSysAccess = listSysAccess;
            return View(listSysObject);
        }

        #region["Load Detail"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDetailData(string objectcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var listSysAccess = new List<Sys_Access>();

                #region["Lấy danh sách nhóm người dùng có menu được gán"]
                var sysAccess = new Sys_Access()
                {
                    GroupCode = "",
                    ObjectCode = objectcode,
                };
                var strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccess);
                var objRQ_Sys_Access = new RQ_Sys_Access()
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
                    // RQ_Sys_Access
                    Rt_Cols_Sys_Access = "*",
                    Sys_Group = null,
                    Lst_Sys_Access = null,
                };

                var objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access, addressAPIs);
                if (objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                {
                    listSysAccess.AddRange(objRT_Sys_Access.Lst_Sys_Access);
                }
                #endregion

                return JsonView("ShowDetailData", listSysAccess);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowDetailData", null, resultEntry);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetFunction(string objectcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var listSysObject = new List<Sys_Object>();
            var listStrObjectCode = new List<string>();
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region["Danh sách FUNC"]
                var sysObject = new Sys_Object()
                {
                    ObjectType = "FUNC",
                    FlagActive = FlagActive,
                };
                var strWhereClauseSysObject = strWhereClause_SysObject(sysObject);

                var objRQ_Sys_Object = new RQ_Sys_Object()
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
                    // RQ_Sys_Object
                    Rt_Cols_Sys_Object = "*",
                };
                var objRT_Sys_Object = Sys_ObjectService.Instance.WA_Sys_Object_Get(objRQ_Sys_Object, addressAPIs);
                if (objRT_Sys_Object.Lst_Sys_Object != null && objRT_Sys_Object.Lst_Sys_Object.Count > 0)
                {
                    listSysObject.AddRange(objRT_Sys_Object.Lst_Sys_Object);
                }
                #endregion

                #region["Danh sách FUNC gán vào Menu"]
                var setting = GetSysObjectSetting();
                if (setting != null && setting.List != null)
                {
                    foreach (var o in setting.List)
                    {
                        if (o.ObjectCode.Equals(objectcode, StringComparison.InvariantCultureIgnoreCase))
                        {
                            //if (o.FunctionCodes != null && o.FunctionCodes[0] != null)
                            //{
                            //    var arrCode = o.FunctionCodes[0].Split(',');
                            //    if (arrCode != null && arrCode.Length > 0)
                            //    {
                            //        for (int i = 0; i < arrCode.Length; i++)
                            //        {
                            //            var strCode = arrCode[i];
                            //            listStrObjectCode.Add(strCode);
                            //        }
                            //    }
                            //}

                            if (o.FunctionCodes != null && o.FunctionCodes.Count > 0)
                            {
                                foreach (var it in o.FunctionCodes)
                                {
                                    listStrObjectCode.Add(it.Trim());
                                }
                            }

                        }
                    }
                }
                #endregion
                ViewBag.SelectedList = listStrObjectCode;
                ViewBag.ObjectCode = objectcode;
                ViewBag.ListSysObject = listSysObject;
                return JsonView("GetFunction", listSysObject);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetFunction", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFuncObjectInMenu(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objFuncInMenu = Newtonsoft.Json.JsonConvert.DeserializeObject<FuncInMenu>(model);
                if (objFuncInMenu != null && !CUtils.IsNullOrEmpty(objFuncInMenu.MenuCode))
                {

                    var title = "Gán FUNC vào Menu thành công!";
                    if (objFuncInMenu.Lst_FuncCode == null || objFuncInMenu.Lst_FuncCode.Count == 0)
                    {
                        objFuncInMenu.Lst_FuncCode = new List<string>();
                        title = "Xóa FUNC khỏi Menu thành công!";
                    }
                    else
                    {
                        //var listFuncCode = new List<string>();
                        //listFuncCode = objFuncInMenu.Lst_FuncCode.ConvertAll(d => d.ToUpper());
                        //objFuncInMenu.Lst_FuncCode = new List<string>();
                        //objFuncInMenu.Lst_FuncCode.AddRange(listFuncCode);
                    }
                    UpdateSysObjectFunctions(objFuncInMenu.MenuCode, objFuncInMenu.Lst_FuncCode);

                    #region["Thực hiện gán quyền luôn"]
                    var listSysAccessCur = new List<Sys_Access>();
                    #region["Lấy danh sách nhóm người dùng có menu được gán"]
                    var sysAccess = new Sys_Access()
                    {
                        GroupCode = "",
                        ObjectCode = objFuncInMenu.MenuCode,
                    };
                    var strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccess);
                    var objRQ_Sys_Access = new RQ_Sys_Access()
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
                        // RQ_Sys_Access
                        Rt_Cols_Sys_Access = "*",
                        Sys_Group = null,
                        Lst_Sys_Access = null,
                    };

                    var objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access, addressAPIs);
                    if (objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                    {
                        listSysAccessCur.AddRange(objRT_Sys_Access.Lst_Sys_Access);
                    }
                    #endregion

                    #region["Lấy FUNC và Menu - Button trong nhóm người dùng và Lưu"]
                    if (listSysAccessCur != null && listSysAccessCur.Count > 0)
                    {
                        #region[""]
                        foreach (var item in listSysAccessCur)
                        {
                            var sysAccessCur = new Sys_Access()
                            {
                                GroupCode = item.GroupCode,
                            };
                            strWhereClauseSysAccess = strWhereClause_SysAccess(sysAccessCur);
                            objRQ_Sys_Access.Ft_WhereClause = strWhereClauseSysAccess;

                            objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access, addressAPIs);
                            if (objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                            {
                                var result = objRT_Sys_Access.Lst_Sys_Access.Where(s => s.so_ObjectType.Equals("MENU")).ToList();
                                if (result != null && result.Count > 0)
                                {

                                    var objRQ_Sys_Access_Save = new RQ_Sys_Access()
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
                                        // RQ_Sys_Access
                                        Rt_Cols_Sys_Access = null,
                                        Sys_Group = new Sys_Group() { GroupCode = item.GroupCode },
                                        Lst_Sys_Access = new List<Sys_Access>(),
                                    };

                                    // Danh sách Menu
                                    var listObjectCode = result.Select(objcode => objcode.ObjectCode).ToList();

                                    var functions = ResolveObjects(listObjectCode);
                                    if (functions != null && functions.Count > 0)
                                    {
                                        var listFunctions = new List<string>();
                                        var listSys_Access_Functions = objRT_Sys_Access.Lst_Sys_Access.Where(s => s.so_ObjectType.Equals("BUTTON")).ToList();
                                        if (listSys_Access_Functions != null && listSys_Access_Functions.Count > 0)
                                        {
                                            listFunctions = listSys_Access_Functions.Select(objcode => objcode.ObjectCode).ToList();
                                            if (listFunctions != null && listFunctions.Count > 0)
                                            {
                                                functions.AddRange(listFunctions);
                                            }
                                        }
                                        functions = functions.Distinct().ToList();
                                        objRQ_Sys_Access.Lst_Sys_Access = new List<Sys_Access>();
                                        var listSysAccess = new List<Sys_Access>();
                                        foreach (var it in functions)
                                        {
                                            var _sysAccessCur = new Sys_Access()
                                            {
                                                ObjectCode = it.Trim(),
                                                GroupCode = item.GroupCode.Trim(),

                                            };
                                            listSysAccess.Add(_sysAccessCur);
                                        }
                                        objRQ_Sys_Access_Save.Lst_Sys_Access.AddRange(listSysAccess);

                                        // Lưu luôn
                                        Sys_AccessService.Instance.WA_Sys_Access_Save(objRQ_Sys_Access_Save, addressAPIs);
                                    }
                                }
                            }

                        }
                        #endregion

                        #region["Comment"]
                        //var _listSysAccessCur = listSysAccessCur;
                        //if(_listSysAccessCur != null && _listSysAccessCur.Count > 0)
                        //{
                        //    var strGroupCodes = "";
                        //    for(var i = 0; i < _listSysAccessCur.Count; i++)
                        //    {
                        //        strGroupCodes += _listSysAccessCur[i].GroupCode;
                        //        if(i != _listSysAccessCur.Count)
                        //        {
                        //            strGroupCodes += ",";
                        //        }
                        //    }
                        //    if(!CUtils.IsNullOrEmpty(strGroupCodes))
                        //    {
                        //        strWhereClauseSysAccess = strWhereClause_SysAccess(strGroupCodes);
                        //        objRQ_Sys_Access.Ft_WhereClause = strWhereClauseSysAccess;

                        //        objRT_Sys_Access = Sys_AccessService.Instance.WA_Sys_Access_Get(objRQ_Sys_Access);

                        //        if(objRT_Sys_Access.Lst_Sys_Access != null && objRT_Sys_Access.Lst_Sys_Access.Count > 0)
                        //        {
                        //            var listObjectCode = new List<string>();

                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    #endregion

                    #endregion
                    return Json(new { Success = true, Title = title });
                }
                else
                {
                    return Json(new { Success = true, Title = "Chưa chọn FUNC gán vào Menu!" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysUser", null, resultEntry);
        }

        #region["strWhereClause"]
        private string strWhereClause_SysObject(Sys_Object data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Object = TableName.Sys_Object + ".";
            if (!CUtils.IsNullOrEmpty(data.ObjectType))
            {
                sbSql.AddWhereClause(Tbl_Sys_Object + TblSys_Object.ObjectType, data.ObjectType, "=");
            }
            if (!CUtils.IsNullOrEmpty(FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Sys_Object + TblSys_User.FlagActive, FlagActive, "=");
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

        private string strWhereClause_SysAccess(string strGroupCode)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Access = TableName.Sys_Access + ".";
            if (!CUtils.IsNullOrEmpty(strGroupCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_Access + TblSys_Access.GroupCode, strGroupCode, "in");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}