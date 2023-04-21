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
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Sys_ModulesController : AdminBaseController
    {
        // Quản lý gói Module
        // GET: Sys_Modules
        #region["Index"]
        public ActionResult Index(string init = "init", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Sys_Modules>(0, PageSizeConfig)
            {
                DataList = new List<Sys_Modules>()
            };
            var itemCount = 0;
            var pageCount = 0;

            var solutionCode = "";
            var solutionName = "";
            var strWhereClauseSys_Solution = "";
            var listSys_Solution = List_Sys_Solution(strWhereClauseSys_Solution);
            if (listSys_Solution != null && listSys_Solution.Count > 0)
            {
                if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionCode))
                {
                    solutionCode = CUtils.StrValueOrNull(listSys_Solution[0].SolutionCode);
                }
                if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionName))
                {
                    solutionName = CUtils.StrValueOrNull(listSys_Solution[0].SolutionName);
                }
            }
            ViewBag.SolutionCode = solutionCode;
            ViewBag.SolutionName = solutionName;
            // (không có nút tìm kiếm => load data luôn)
            init = String.Format("{0}", "search");
            if (init != "init")
            {
                var strWhereClauseSys_Modules = "";

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSys_Modules,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);

                if (objRT_Sys_ModulesCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Sys_ModulesCur.MySummaryTable.MyCount);
                }
                if (objRT_Sys_ModulesCur != null && objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Sys_ModulesCur.Lst_Sys_Modules);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
            }
            else
            {
                init = "search";
            }

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }
        #endregion
        #region["Create"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var solutionCode = "";
            var solutionName = "";
            var strWhereClauseSys_Solution = "";
            var listSys_Solution = List_Sys_Solution(strWhereClauseSys_Solution);
            if (listSys_Solution != null && listSys_Solution.Count > 0)
            {
                if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionCode))
                {
                    solutionCode = CUtils.StrValueOrNull(listSys_Solution[0].SolutionCode);
                }
                if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionName))
                {
                    solutionName = CUtils.StrValueOrNull(listSys_Solution[0].SolutionName);
                }
            }
            ViewBag.SolutionCode = solutionCode;
            ViewBag.SolutionName = solutionName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objSys_ModulesInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_Modules>(model);
                var solutionCode = "";
                var strWhereClauseSys_Solution = "";
                var listSys_Solution = List_Sys_Solution(strWhereClauseSys_Solution);
                if (listSys_Solution != null && listSys_Solution.Count > 0)
                {
                    if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionCode))
                    {
                        solutionCode = CUtils.StrValueOrNull(listSys_Solution[0].SolutionCode);
                    }
                }

                objSys_ModulesInput.SolutionCode = solutionCode;
                var objRQ_Sys_Modules = new RQ_Sys_Modules
                {
                    FlagIsDelete = null,
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
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = null,
                    Sys_Modules = objSys_ModulesInput
                };
                Sys_ModulesService.Instance.WA_Sys_Modules_Create(objRQ_Sys_Modules, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới gói Module thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
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
        #region["Update"]
        [HttpGet]
        public ActionResult Update(string modulecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(modulecode))
            {
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = modulecode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    
                    return View(objRT_Sys_ModulesCur.Lst_Sys_Modules[0]);
                }
            }
            return View(new Sys_Modules());
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
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objSys_ModulesInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys_Modules>(model);
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = objSys_ModulesInput.ModuleCode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].ModuleName = objSys_ModulesInput.ModuleName;
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].Description = objSys_ModulesInput.Description;
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].QtyInvoice = objSys_ModulesInput.QtyInvoice;
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].ValCapacity = objSys_ModulesInput.ValCapacity;
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].FlagActive = objSys_ModulesInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_Modules = TableName.Sys_Modules + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.ModuleName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.Description);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.QtyInvoice);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.ValCapacity);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.FlagActive);

                    objRQ_Sys_Modules.Ft_WhereClause = null;
                    objRQ_Sys_Modules.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_Modules.Rt_Cols_Sys_Modules = null;
                    objRQ_Sys_Modules.Sys_Modules = objRT_Sys_ModulesCur.Lst_Sys_Modules[0];

                    Sys_ModulesService.Instance.WA_Sys_Modules_Update(objRQ_Sys_Modules, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa gói Module thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã gói Module '" + objSys_ModulesInput.ModuleCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã gói Module trống!");
            }
            return Json(resultEntry);
        }
        #endregion
        #region["Detail"]
        [HttpGet]
        public ActionResult Detail(string modulecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(modulecode))
            {
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = modulecode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    
                    return View(objRT_Sys_ModulesCur.Lst_Sys_Modules[0]);
                }
            }
            return View(new Sys_Modules());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string modulecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(modulecode))
            {
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = modulecode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    objRQ_Sys_Modules.Sys_Modules = objRT_Sys_ModulesCur.Lst_Sys_Modules[0];

                    Sys_ModulesService.Instance.WA_Sys_Modules_Delete(objRQ_Sys_Modules, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa gói Module thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã gói Module '" + modulecode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã gói Module trống!");
            }

            return Json(resultEntry);
        }


        #endregion

        #region["Active/Inactive Module"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActiveModule(string modulecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(modulecode))
            {
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = modulecode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].FlagActive = FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_Modules = TableName.Sys_Modules + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.FlagActive);

                    objRQ_Sys_Modules.Ft_WhereClause = null;
                    objRQ_Sys_Modules.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_Modules.Rt_Cols_Sys_Modules = null;
                    objRQ_Sys_Modules.Sys_Modules = objRT_Sys_ModulesCur.Lst_Sys_Modules[0];

                    Sys_ModulesService.Instance.WA_Sys_Modules_Update(objRQ_Sys_Modules, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Active gói Module thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã gói Module '" + modulecode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã gói Module trống!");
            }
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveModule(string modulecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(modulecode))
            {
                var objSys_Modules = new Sys_Modules()
                {
                    ModuleCode = modulecode,
                };

                var strWhereClauseMst_Country = strWhereClause_Sys_Modules(objSys_Modules);

                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Country,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                {
                    objRT_Sys_ModulesCur.Lst_Sys_Modules[0].FlagActive = FlagInActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Sys_Modules = TableName.Sys_Modules + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Sys_Modules, TblSys_Modules.FlagActive);

                    objRQ_Sys_Modules.Ft_WhereClause = null;
                    objRQ_Sys_Modules.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Sys_Modules.Rt_Cols_Sys_Modules = null;
                    objRQ_Sys_Modules.Sys_Modules = objRT_Sys_ModulesCur.Lst_Sys_Modules[0];

                    Sys_ModulesService.Instance.WA_Sys_Modules_Update(objRQ_Sys_Modules, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Inactive gói Module thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã gói Module '" + modulecode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã gói Module trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region["02.Phân quyền Menu - Button cho nhóm người dùng"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSysObject(string modulecode = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listSysObject = new List<Sys_Object>();
                var listSys_ObjectInModules = new List<Sys_ObjectInModules>();
                #region["Danh sách Menu - Button"]
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

                #region["Danh sách Menu - Button gán vào module"]
                var sys_ObjectInModules = new Sys_ObjectInModules()
                {
                    ModuleCode = modulecode,
                    ObjectCode = "",
                };
                var strWhereClauseSys_ObjectInModules = strWhereClause_Sys_ObjectInModules(sys_ObjectInModules);
                var objRQ_Sys_ObjectInModules = new RQ_Sys_ObjectInModules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseSys_ObjectInModules,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_Sys_ObjectInModules
                    Rt_Cols_Sys_ObjectInModules = "*",
                    Sys_Modules = null,
                    Lst_Sys_ObjectInModules = null,
                };

                var objRT_Sys_ObjectInModules = Sys_ObjectInModulesService.Instance.WA_Sys_ObjectInModules_Get(objRQ_Sys_ObjectInModules, addressAPIs);
                if (objRT_Sys_ObjectInModules.Lst_Sys_ObjectInModules != null && objRT_Sys_ObjectInModules.Lst_Sys_ObjectInModules.Count > 0)
                {
                    listSys_ObjectInModules.AddRange(objRT_Sys_ObjectInModules.Lst_Sys_ObjectInModules);
                }
                #endregion
                ViewBag.ModuleCode = modulecode;
                ViewBag.ListSysObject = listSysObject;
                ViewBag.ListSys_ObjectInModules = listSys_ObjectInModules;
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
        public ActionResult SaveObjectInModules(string model)
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
                var RQ_Sys_ObjectInModulesUI = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Sys_ObjectInModulesUI>(model);
                if (RQ_Sys_ObjectInModulesUI != null)
                {
                    var objRQ_Sys_ObjectInModules = new RQ_Sys_ObjectInModules()
                    {
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
                        // RQ_Sys_ObjectInModules
                        Sys_Modules = RQ_Sys_ObjectInModulesUI.Sys_Modules,
                        Lst_Sys_ObjectInModules = new List<Sys_ObjectInModules>(),
                    };
                    

                    var title = "Gán function vào module thành công!";
                    if (RQ_Sys_ObjectInModulesUI.Lst_Sys_ObjectInModulesUI == null || RQ_Sys_ObjectInModulesUI.Lst_Sys_ObjectInModulesUI.Count == 0)
                    {
                        //objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules = new List<Sys_ObjectInModules>();
                        title = "Xóa function vào module thành công!";
                    }
                    else
                    {
                        var listFunctions = new List<string>();
                        foreach (var item in RQ_Sys_ObjectInModulesUI.Lst_Sys_ObjectInModulesUI)
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
                            objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules = new List<Sys_ObjectInModules>();
                            var listSys_ObjectInModules = new List<Sys_ObjectInModules>();
                            foreach (var it in functions)
                            {
                                var sysAccess = new Sys_ObjectInModules()
                                {
                                    ObjectCode = it.Trim(),
                                    ModuleCode = objRQ_Sys_ObjectInModules.Sys_Modules.ModuleCode.ToString().Trim(),
                                };
                                listSys_ObjectInModules.Add(sysAccess);
                            }
                            objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules.AddRange(listSys_ObjectInModules);
                        }
                    }
                    Sys_ObjectInModulesService.Instance.WA_Sys_ObjectInModules_Save(objRQ_Sys_ObjectInModules, addressAPIs);

                    return Json(new { Success = true, Title = title });
                }
                else
                {
                    return Json(new { Success = true, Title = "Chưa chọn function gán vào module!" });
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

        #region["Import excel"]
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
                var listSys_Modules = new List<Sys_Modules>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 5)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            if (CUtils.IsNullOrEmpty(dr[TblSys_Modules.ModuleCode]))
                            {
                                exitsData = "Mã module không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblSys_Modules.ModuleName]))
                            {
                                exitsData = "Tên module không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (!CUtils.IsNullOrEmpty(dr[TblSys_Modules.QtyInvoice]))
                            {
                                var qtyInvoice = dr[TblSys_Modules.QtyInvoice].ToString().Trim();
                                if (CUtils.IsNumeric(qtyInvoice))
                                {
                                    if (Convert.ToDouble(qtyInvoice) < 0)
                                    {
                                        exitsData = "SL hóa đơn >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                else
                                {
                                    exitsData = "SL hóa đơn không phải số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (!CUtils.IsNullOrEmpty(dr[TblSys_Modules.ValCapacity]))
                            {
                                var valCapacity = dr[TblSys_Modules.ValCapacity].ToString().Trim();
                                if (CUtils.IsNumeric(valCapacity))
                                {
                                    if (Convert.ToDouble(valCapacity) < 0)
                                    {
                                        exitsData = "Dung lượng (GB) >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                else
                                {
                                    exitsData = "Dung lượng (GB) không phải số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblSys_Modules.Description]))
                            {
                                exitsData = "Description không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            idx++;
                        }
                        #endregion

                        #region["Check duplicate"]
                        iRows = 2;
                        strRows = " - Dòng ";
                        var jRows = 2;
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            iRows = 2;
                            iRows = (iRows + i + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;
                            var moduleCodeCur = table.Rows[i][TblSys_Modules.ModuleCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _moduleCodeCur = table.Rows[j][TblSys_Modules.ModuleCode].ToString().Trim();
                                    if (moduleCodeCur.Equals(_moduleCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã module '" + moduleCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listSys_Modules = DataTableCmUtils.ToListof<Sys_Modules>(table); ;
                    // Gọi hàm save data
                    if (listSys_Modules != null && listSys_Modules.Count > 0)
                    {
                        var solutionCode = "";
                        var strWhereClauseSys_Solution = "";
                        var listSys_Solution = List_Sys_Solution(strWhereClauseSys_Solution);
                        if (listSys_Solution != null && listSys_Solution.Count > 0)
                        {
                            if (!CUtils.IsNullOrEmpty(listSys_Solution[0].SolutionCode))
                            {
                                solutionCode = CUtils.StrValueOrNull(listSys_Solution[0].SolutionCode);
                            }
                            
                        }
                        
                        foreach (var item in listSys_Modules)
                        {
                            //item.FlagActive = "1";
                            item.SolutionCode = solutionCode;
                            var objRQ_Sys_Modules = new RQ_Sys_Modules
                            {
                                FlagIsDelete = null,
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
                                UtcOffset = userState.UtcOffset,
                                // RQ_Sys_Modules
                                Rt_Cols_Sys_Modules = null,
                                Sys_Modules = item
                            };

                            Sys_ModulesService.Instance.WA_Sys_Modules_Create(objRQ_Sys_Modules, addressAPIs);
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

        #region["Export excel"]
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
            var list = new List<Sys_Modules>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_Modules).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_Modules"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
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
        public ActionResult Export(string taxtypename = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listSys_Modules = new List<Sys_Modules>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                
                #region["Search"]
                var strWhereClauseSys_Modules = "";
                var objRQ_Sys_Modules = new RQ_Sys_Modules()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseSys_Modules,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Sys_Modules
                    Rt_Cols_Sys_Modules = "*",
                    Sys_Modules = null,
                };

                var objRT_Sys_ModulesCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                if (objRT_Sys_ModulesCur != null && objRT_Sys_ModulesCur.Lst_Sys_Modules != null)
                {
                    if (objRT_Sys_ModulesCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Sys_ModulesCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Sys_ModulesCur.Lst_Sys_Modules != null && objRT_Sys_ModulesCur.Lst_Sys_Modules.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listSys_Modules.AddRange(objRT_Sys_ModulesCur.Lst_Sys_Modules);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_Modules).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listSys_Modules, dicColNames, filePath, string.Format("Sys_Modules"));


                    #region["Export các trang còn lại"]
                    listSys_Modules.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Sys_Modules.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Sys_ModulesExportCur = Sys_ModulesService.Instance.WA_Sys_Modules_Get(objRQ_Sys_Modules, addressAPIs);
                            if (objRT_Sys_ModulesExportCur != null && objRT_Sys_ModulesExportCur.Lst_Sys_Modules != null && objRT_Sys_ModulesExportCur.Lst_Sys_Modules.Count > 0)
                            {
                                listSys_Modules.AddRange(objRT_Sys_ModulesExportCur.Lst_Sys_Modules);
                                ExcelExport.ExportToExcelFromList(listSys_Modules, dicColNames, filePath, string.Format("Sys_Modules_" + i));
                                listSys_Modules.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                #endregion

                else
                {
                    return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
                }
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
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblSys_Modules.ModuleCode,"Mã modules (*)"},
                {TblSys_Modules.ModuleName,"Tên module (*)"},
                {TblSys_Modules.QtyInvoice,"SL hóa đơn"},
                {TblSys_Modules.ValCapacity,"Dung lượng"},
                {TblSys_Modules.Description,"Description (*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblSys_Modules.ModuleCode,"Mã modules (*)"},
                {TblSys_Modules.ModuleName,"Tên module (*)"},
                {TblSys_Modules.QtyInvoice,"SL hóa đơn"},
                {TblSys_Modules.ValCapacity,"Dung lượng"},
                {TblSys_Modules.Description,"Description (*)"},
                {TblSys_Modules.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Sys_Modules(Sys_Modules data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_Modules = TableName.Sys_Modules + ".";
            if (!CUtils.IsNullOrEmpty(data.ModuleCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_Modules + TblSys_Modules.ModuleCode, CUtils.StrValueOrNull(data.ModuleCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Sys_Modules + TblSys_Modules.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
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

        private string strWhereClause_Sys_ObjectInModules(Sys_ObjectInModules data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_ObjectInModules = TableName.Sys_ObjectInModules + ".";
            if (!CUtils.IsNullOrEmpty(data.ModuleCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_ObjectInModules + TblSys_ObjectInModules.ModuleCode, data.ModuleCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ObjectCode))
            {
                sbSql.AddWhereClause(Tbl_Sys_ObjectInModules + TblSys_ObjectInModules.ObjectCode, data.ObjectCode.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}