using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
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
    public class Mst_PartMaterialTypeController : AdminBaseController
    {
        // GET: Mst_PartMaterialType
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            ViewBag.PageCur = page.ToString();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_PartMaterialType>(0, recordcount)
            {
                DataList = new List<Mst_PartMaterialType>()
            };
            var itemCount = 0;
            var pageCount = 0;
            init = "search";
            if (init != "Search")
            {
                #region["Search"]
                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);

                if (objRT_Mst_PartMaterialTypeCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_PartMaterialTypeCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_PartMaterialTypeCur != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "init";
            }

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }
        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_PartMaterialTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartMaterialType>(model);
                //var title = "";
                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType
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
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = null,
                    Mst_PartMaterialType = objMst_PartMaterialTypeInput
                };
                Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Create(objRQ_Mst_PartMaterialType, userState.AddressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới danh mục loại sản phẩm thành công!");
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

        #region ["Sửa"]
        [HttpGet]
        public ActionResult Update(string pmtype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(pmtype))
            {
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    PMType = pmtype,
                };

                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);

                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartMaterialType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                if (objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                {
                    return View(objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0]);
                }
            }
            return View(new Mst_PartMaterialType());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_PartMaterialTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartMaterialType>(model);
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    PMType = objMst_PartMaterialTypeInput.PMType,
                };

                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);

                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartMaterialType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                if (objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                {
                    objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0].PMType = objMst_PartMaterialTypeInput.PMType;
                    objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0].FlagActive = objMst_PartMaterialTypeInput.FlagActive;
                    objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0].PMTypeName = objMst_PartMaterialTypeInput.PMTypeName;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_PartMaterialType = TableName.Mst_PartMaterialType + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartMaterialType, TblMst_PartMaterialType.PMType);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartMaterialType, TblMst_PartMaterialType.FlagActive);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartMaterialType, TblMst_PartMaterialType.PMTypeName);

                    objRQ_Mst_PartMaterialType.Ft_WhereClause = null;
                    objRQ_Mst_PartMaterialType.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_PartMaterialType.Rt_Cols_Mst_PartMaterialType = null;
                    objRQ_Mst_PartMaterialType.Mst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0];

                    Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Update(objRQ_Mst_PartMaterialType, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục nhóm sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm sản phẩm '" + objMst_PartMaterialTypeInput.PMType + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã nhóm sản phẩm!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string pmtype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(pmtype))
            {
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    PMType = pmtype,
                };

                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);

                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartMaterialType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                if (objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                {
                    return View(objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0]);
                }
            }
            return View(new Mst_PartMaterialType());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string pmtype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(pmtype))
            {
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    PMType = pmtype,
                };

                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);

                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartMaterialType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                if (objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                {
                    objRQ_Mst_PartMaterialType.Mst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType[0];

                    Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Delete(objRQ_Mst_PartMaterialType, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục nhóm sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm sản phẩm '" + pmtype + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã nhóm sản phẩm!");
            }

            return Json(resultEntry);
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
                var listMst_PartMaterialType = new List<Mst_PartMaterialType>();
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
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartMaterialType.PMType]))
                            {
                                exitsData = "Mã danh mục nhóm sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartMaterialType.PMTypeName]))
                            {
                                exitsData = "Tên nhóm sản phẩm không được trống!" + strRows;
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
                            var Mst_PartMaterialTypeCur = table.Rows[i][TblMst_PartMaterialType.PMType].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_PartMaterialTypeCur = table.Rows[j][TblMst_PartMaterialType.PMType].ToString().Trim();
                                    if (Mst_PartMaterialTypeCur.Equals(_Mst_PartMaterialTypeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã nhóm sản phẩm '" + Mst_PartMaterialTypeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_PartMaterialType = DataTableCmUtils.ToListof<Mst_PartMaterialType>(table); ;
                    // Gọi hàm save data
                    if (listMst_PartMaterialType != null && listMst_PartMaterialType.Count > 0)
                    {
                        foreach (var item in listMst_PartMaterialType)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType
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
                                // RQ_Mst_Province
                                Rt_Cols_Mst_PartMaterialType = null,
                                Mst_PartMaterialType = item,
                            };
                            Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Create(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_PartMaterialType>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartMaterialType).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_PartMaterialType"));

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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_PartMaterialType = new List<Mst_PartMaterialType>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_PartMaterialType = "*",
                    Mst_PartMaterialType = null,
                };

                var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                if (objRT_Mst_PartMaterialTypeCur != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null)
                {
                    if (objRT_Mst_PartMaterialTypeCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_PartMaterialTypeCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_PartMaterialType.AddRange(objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartMaterialType).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_PartMaterialType, dicColNames, filePath, string.Format("Mst_PartMaterialType"));


                    #region["Export các trang còn lại"]
                    listMst_PartMaterialType.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_PartMaterialType.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_PartMaterialTypeExportCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
                            if (objRT_Mst_PartMaterialTypeExportCur != null && objRT_Mst_PartMaterialTypeExportCur.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialTypeExportCur.Lst_Mst_PartMaterialType.Count > 0)
                            {
                                listMst_PartMaterialType.AddRange(objRT_Mst_PartMaterialTypeExportCur.Lst_Mst_PartMaterialType);
                                ExcelExport.ExportToExcelFromList(listMst_PartMaterialType, dicColNames, filePath, string.Format("Mst_PartMaterialType" + i));
                                listMst_PartMaterialType.Clear();
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

        #region["strWhereClause"]
        private string strWhereClause_Mst_PartMaterialType(Mst_PartMaterialType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartMaterialType = TableName.Mst_PartMaterialType + ".";
            if (!CUtils.IsNullOrEmpty(data.PMType))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.PMType, CUtils.StrValueOrNull(data.PMType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PMTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.PMTypeName, "%" + CUtils.StrValueOrNull(data.PMTypeName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"PMType","Mã nhóm sản phẩm"},
                 {"PMTypeName","Tên nhóm sản phẩm"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PMType","Mã nhóm sản phẩm"},
                 {"PMTypeName","Tên nhóm sản phẩm"},
            };
        }
        #endregion
    }
}