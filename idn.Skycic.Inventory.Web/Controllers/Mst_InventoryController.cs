using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Mst_InventoryController : AdminBaseController
    {
        // GET: Mst_Inventory
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var pageInfo = new PageInfo<Mst_Inventory>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Inventory>()
            };
            var itemCount = 0;
            var pageCount = 0;
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordCount = recordcount.ToString(),
                Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                Ft_WhereClause = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory.MySummaryTable != null)
            {
                itemCount = Convert.ToInt32(objRT_Mst_Inventory.MySummaryTable.MyCount);
            }
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                pageInfo.DataList.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
            }
            ViewBag.PageCur = page.ToString();
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới kho"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region[kho]
            var objMst_Inventory = new Mst_Inventory()
            {
                FlagActive = "1",
            };
            var strWhere_Mst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhere_Mst_Inventory,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_Inventory = "*"
            };
            var listInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            ViewBag.ListInventory = listInventory.Lst_Mst_Inventory;
            #endregion

            #region[loại kho]
            var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_InventoryType = "*"
            };
            var listInventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
            ViewBag.ListInventoryType = listInventoryType.Lst_Mst_InventoryType;
            #endregion

            #region[Cấp kho]
            var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_InventoryLevelType = "*"
            };
            var listInventoryLevelType = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, addressAPIs);
            ViewBag.ListInventoryLevelType = listInventoryLevelType.Lst_Mst_InventoryLevelType; // Cấp kho
            #endregion
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_Inventory = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Inventory>(model);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
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
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Inventory = null,
                    Mst_Inventory = objMst_Inventory
                };
                Mst_InventoryService.Instance.WA_Mst_Inventory_Create(objRQ_Mst_Inventory, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới kho thành công!");
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

        #region[chi tiết - xóa]
        [HttpGet]
        public ActionResult Detail(string invcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(invcode))
            {
                var objMst_Inventory = new Mst_Inventory()
                {
                    InvCode = invcode
                };
                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);

                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Inventory
                    Rt_Cols_Mst_Inventory = "*",
                    Mst_Inventory = null,
                };
                var listInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                ViewBag.ListInventory = listInventory.Lst_Mst_Inventory;
                return View(listInventory.Lst_Mst_Inventory[0]);
            }

            return View(new Mst_Inventory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string invcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(invcode))
            {
                var objMst_Inventory = new Mst_Inventory()
                {
                    InvCode = invcode
                };

                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);

                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Inventory
                    Rt_Cols_Mst_Inventory = "*",
                    Mst_Inventory = null,
                };

                var objRT_Mst_InventoryCur = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_InventoryCur.Lst_Mst_Inventory != null && objRT_Mst_InventoryCur.Lst_Mst_Inventory.Count > 0)
                {
                    objRQ_Mst_Inventory.Mst_Inventory = objRT_Mst_InventoryCur.Lst_Mst_Inventory[0];

                    Mst_InventoryService.Instance.WA_Mst_Inventory_Delete(objRQ_Mst_Inventory, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã kho'" + invcode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Kho trống!");
            }

            return Json(resultEntry);
        }

        #endregion

        #region["Sửa kho"]
        [HttpGet]
        public ActionResult Update(string invcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(invcode))
            {
                var objMst_Inventory = new Mst_Inventory()
                {
                    InvCode = invcode
                };
                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);

                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Inventory
                    Rt_Cols_Mst_Inventory = "*",
                    Mst_Inventory = null,
                };
                var listInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                ViewBag.ListInventory = listInventory.Lst_Mst_Inventory;
                return View(listInventory.Lst_Mst_Inventory[0]);
            }

            return View(new Mst_Inventory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_InventoryInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Inventory>(model);
                var objMst_Inventory = new Mst_Inventory()
                {
                    InvCode = objMst_InventoryInput.InvCode
                };

                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);

                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Inventory
                    Rt_Cols_Mst_Inventory = "*",
                    Mst_Inventory = null,
                };
                var objRT_Mst_InventoryCur = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_InventoryCur.Lst_Mst_Inventory != null && objRT_Mst_InventoryCur.Lst_Mst_Inventory.Count > 0)
                {
                    //objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].TenantId = objMst_InventoryInput.TenantId;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvCode = objMst_InventoryInput.InvCode;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvName = objMst_InventoryInput.InvName;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvLevelType = objMst_InventoryInput.InvLevelType;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvType = objMst_InventoryInput.InvType;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvCodeParent = objMst_InventoryInput.InvCodeParent;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvAddress = objMst_InventoryInput.InvAddress;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvContactName = objMst_InventoryInput.InvContactName;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvContactEmail = objMst_InventoryInput.InvContactEmail;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].InvContactPhone = objMst_InventoryInput.InvContactPhone;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].Remark = objMst_InventoryInput.Remark;
                    objRT_Mst_InventoryCur.Lst_Mst_Inventory[0].FlagActive = objMst_InventoryInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
                    //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.TenantId);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvLevelType);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvType);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvCodeParent);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvAddress);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactEmail);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactPhone);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.Remark);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.FlagActive);

                    objRQ_Mst_Inventory.Ft_WhereClause = null;
                    objRQ_Mst_Inventory.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory = null;
                    objRQ_Mst_Inventory.Mst_Inventory = objRT_Mst_InventoryCur.Lst_Mst_Inventory[0];
                    Mst_InventoryService.Instance.WA_Mst_Inventory_Update(objRQ_Mst_Inventory, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã kho '" + objMst_InventoryInput.InvCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("kho trống!");
            }
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
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Inventory>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Inventory"));

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
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_InventorySearch = new List<Mst_Inventory>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;

            try
            {
                var list = new List<Mst_Inventory>();
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRT_Mst_InventoryCur = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_InventoryCur.Lst_Mst_Inventory != null && objRT_Mst_InventoryCur.Lst_Mst_Inventory.Count > 0)
                {
                    list.AddRange(objRT_Mst_InventoryCur.Lst_Mst_Inventory);
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Inventory"));

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
        #endregion

        #region[Import Excel]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
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
                var listMst_Inventory = new List<Mst_Inventory>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 10)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Inventory.InvCode]))
                            {
                                exitsData = "Mã kho không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Inventory.InvName]))
                            {
                                exitsData = "Tên kho không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Inventory.InvType]))
                            {
                                exitsData = "Loại kho không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Inventory.InvLevelType]))
                            {
                                exitsData = "Cấp kho không được trống!" + strRows;
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
                            var Mst_InventoryCodeCur = table.Rows[i][TblMst_Inventory.InvCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_InventoryCodeCur = table.Rows[j][TblMst_Inventory.InvCode].ToString().Trim();
                                    if (Mst_InventoryCodeCur.Equals(_Mst_InventoryCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã kho '" + Mst_InventoryCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Inventory = DataTableCmUtils.ToListof<Mst_Inventory>(table);
                    //Gọi hàm save data
                    if (listMst_Inventory != null && listMst_Inventory.Count > 0)
                    {

                        foreach (var item in listMst_Inventory)
                        {
                           

                            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                            {
                                // WARQBase
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
                                // RQ_Mst_Province
                                Rt_Cols_Mst_Inventory = null,
                                Mst_Inventory=item
                            };
                            Mst_InventoryService.Instance.WA_Mst_Inventory_Create(objRQ_Mst_Inventory, addressAPIs);

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

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho"},
                 {"InvName","Tên kho"},
                 {"InvCodeParent","Mã kho cha"},
                 {"InvAddress","Địa chỉ"},
                 {"InvLevelType","Cấp kho"},
                 {"InvType","Loại kho"},
                 {"InvContactName","Người quản lý"},
                 {"InvContactEmail","Email"},
                 {"InvContactPhone","Điện thoại"},
                 {"FlagActive","Trạng thái"},
                 {"Remark","Ghi chú"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho"},
                 {"InvName","Tên kho"},
                 {"InvCodeParent","Mã kho cha"},
                 {"InvAddress","Địa chỉ"},
                 {"InvLevelType","Cấp kho"},
                 {"InvType","Loại kho"},
                 {"InvContactName","Người quản lý"},
                 {"InvContactEmail","Email"},
                 {"InvContactPhone","Điện thoại"},
                 {"Remark","Ghi chú"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Mst_Inventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvCode, CUtils.StrValueOrNull(data.InvCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvName, "%" + CUtils.StrValueOrNull(data.InvName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        
        #endregion
    }
}