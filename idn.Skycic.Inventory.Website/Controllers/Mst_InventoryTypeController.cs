using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_InventoryTypeController : AdminBaseController
    {
        // GET: Mst_InventoryType
        public ActionResult Index(int page = 0, int recordcount = 10, string init = "init")
        {



            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_LOAI_KHO");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            ViewBag.PageCur = page.ToString();
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = this.UserState;
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_InventoryType>(0, recordcount)
            {
                DataList = new List<Mst_InventoryType>()
            };

            var strWhere = string.Format("Mst_InventoryType.NetworkID = '{0}'", networkID);

            try
            {
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhere,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_InventoryType = "*",
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_InventoryType);
                var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
                if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
                {
                    pageInfo.DataList = objRT_Mst_InventoryType.Lst_Mst_InventoryType;
                    itemCount = objRT_Mst_InventoryType.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_InventoryType.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                if (!init.Equals("init"))
                {
                    return JsonView("BindDataMst_InventoryType", pageInfo);
                }
                return View(pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return JsonView("Create", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var resultEntry = new JsonResultEntry() { Success = false };
            var orgID = UserState.Mst_NNT.OrgID;
            try
            {
                var objMst_InventoryType = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InventoryType>(model);
                objMst_InventoryType.OrgID = orgID;
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
                {
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
                    // RQ_Mst_Customer
                    Mst_InventoryType = objMst_InventoryType
                };
                Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Create(objRQ_Mst_InventoryType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới loại kho thành công!");
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

        [HttpGet]
        public ActionResult Update(string invType)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var orgID = UserState.Mst_NNT.OrgID;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var strWhere = string.Format("Mst_InventoryType.InvType = '{0}'", invType);
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
                {
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
                    Ft_WhereClause = strWhere,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_InventoryType = "*"
                };
                var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
                if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
                {
                    return JsonView("Update", objRT_Mst_InventoryType.Lst_Mst_InventoryType[0]);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var resultEntry = new JsonResultEntry() { Success = false };
            var orgID = UserState.Mst_NNT.OrgID;
            try
            {
                var objMst_InventoryType = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InventoryType>(model);
                objMst_InventoryType.OrgID = orgID;
                var strFt_Cols_Upd = "";
                var Tbl_Mst_InventoryType = TableName.Mst_InventoryType + ".";
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InventoryType, TblMst_InventoryType.InvTypeName);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InventoryType, TblMst_InventoryType.FlagActive);
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
                {
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
                    Ft_Cols_Upd = strFt_Cols_Upd,
                    // RQ_Mst_Customer
                    Mst_InventoryType = objMst_InventoryType
                };
                Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Update(objRQ_Mst_InventoryType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật loại kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }

        [HttpGet]
        public ActionResult Detail(string invType)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var orgID = UserState.Mst_NNT.OrgID;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var strWhere = string.Format("Mst_InventoryType.InvType = '{0}'", invType);
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
                {
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
                    Ft_WhereClause = strWhere,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_InventoryType = "*"
                };
                var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
                if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
                {
                    return JsonView("Detail", objRT_Mst_InventoryType.Lst_Mst_InventoryType[0]);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Detail", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Delete(string invType)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var orgID = UserState.Mst_NNT.OrgID;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
                {
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
                    // RQ_Mst_Customer
                };
                objRQ_Mst_InventoryType.Mst_InventoryType = new Mst_InventoryType()
                {
                    OrgID = orgID,
                    InvType = invType
                };
                var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Delete(objRQ_Mst_InventoryType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá loại kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var orgID = UserState.Mst_NNT.OrgID;
            var resultEntry = new JsonResultEntry() { Success = false };

            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception(Nonsense.MESS_CHECK_FILEIMPORT);
                    }

                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var list = new List<Mst_InventoryType>();

                    var ds = ExcelImport.ImportExcelXLS(filePath, true);

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        list = DataTableCmUtils.ToListof<Mst_InventoryType>(table);
                        if (table.Columns.Count != 2)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.Detail = exitsData;
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region["check data"]
                            var listCur = new List<Mst_InventoryType>();
                            var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType
                            {
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

                                // RQ_Mst_Province
                                Rt_Cols_Mst_InventoryType = "*"
                            };
                            var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
                            if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
                            {
                                listCur.AddRange(objRT_Mst_InventoryType.Lst_Mst_InventoryType);
                            }
                            foreach (DataRow dr in table.Rows)
                            {

                                if (dr["InvType"] == null || dr["InvType"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã loại kho không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                                if (dr["InvTypeName"] == null || dr["InvTypeName"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Tên loại kho" + "'" + dr["InvType"].ToString().ToUpper() + "'" +
                                                    " không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                            }
                            if (list != null && list.Count > 0)
                            {
                                foreach (var item in list)
                                {
                                    if (listCur != null && listCur.Count > 0 &&
                                        listCur.Any(m => m.InvType.ToString().ToUpper() == item.InvType.ToString().ToUpper()))
                                    {
                                        var exitsData = "Mã loại kho: " + "'" + item.InvType + "'" +
                                                        " đã tồn tại trong hệ thống. Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }
                                    if (
                                        list.Where(m => m != item)
                                            .Any(m => m.InvType.ToString().ToUpper() == item.InvType.ToString().ToUpper()))
                                    {
                                        var exitsData = "Trùng mã loại kho: " + "'" + item.InvType + "'" +
                                                        ". Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }
                                    item.InvType = item.InvType.ToString().ToUpper();
                                    item.FlagActive = "1";
                                    item.OrgID = UserState.Mst_NNT.OrgID.ToString();
                                    var objRQ_Mst_InventoryTypeCreate = new RQ_Mst_InventoryType
                                    {
                                        Tid = GetNextTId(),
                                        GwUserCode = GwUserCode,
                                        GwPassword = GwPassword,
                                        WAUserCode = waUserCode,
                                        WAUserPassword = waUserPassword,
                                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                        NetworkID = networkID,
                                        OrgID = UserState.Mst_NNT.OrgID.ToString(),
                                        FuncType = null,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = null,
                                        Ft_Cols_Upd = null,
                                        // RQ_Mst_InventoryType
                                        Mst_InventoryType = item
                                    };
                                    Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Create(objRQ_Mst_InventoryTypeCreate, addressAPIs);
                                    resultEntry.Success = true;
                                    resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                                    resultEntry.RedirectUrl = Url.Action("Index");
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        var exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.Detail = exitsData;
                        return Json(resultEntry);
                    }
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(int page = 0, int recordcount = 10)
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var orgID = UserState.Mst_NNT.OrgID;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMstInventoryType = new List<Mst_InventoryType>();

                var strWhere = string.Format("Mst_InventoryType.NetworkID = '{0}'", networkID);
                var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
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
                    Ft_WhereClause = strWhere,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryType = "*",
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_InventoryType);
                var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, addressAPIs);
                if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
                {
                    listMstInventoryType.AddRange(objRT_Mst_InventoryType.Lst_Mst_InventoryType);
                }
                if (listMstInventoryType != null && listMstInventoryType.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InventoryType).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMstInventoryType, dicColNames, filePath, string.Format("Mst_InventoryType"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMstInventoryType = new List<Mst_InventoryType>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InventoryType).Name), ref url);
                ExcelExport.ExportToExcelFromList(listMstInventoryType, dicColNames, filePath, string.Format("Mst_InventoryType"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"InvType","Mã loại kho"},
                 {"InvTypeName","Tên loại kho"},
                 {"FlagActive","Trạng thái"}
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"InvType","Mã loại kho (*)"},
                 {"InvTypeName","Tên loại kho (*)"},
            };
        }
        public JavaScriptResult SuccessHandler(string strMessage)
        {
            var strJavaScript = "";
            strJavaScript = "==error==" + strMessage;
            return JavaScript(strJavaScript);
        }
        #endregion
    }
}