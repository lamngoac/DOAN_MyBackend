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
    public class Mst_PartUnitController : AdminBaseController
    {
        // GET: Mst_PartUnit
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            ViewBag.PageCur = page.ToString();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_PartUnit>(0, recordcount)
            {
                DataList = new List<Mst_PartUnit>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "Search")
            {
                #region["Search"]
                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
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
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);

                if (objRT_Mst_PartUnitCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_PartUnitCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_PartUnitCur != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_PartUnitCur.Lst_Mst_PartUnit);
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
                var objMst_PartUnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartUnit>(model);
                //var title = "";
                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit
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
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = null,
                    Mst_PartUnit = objMst_PartUnitInput
                };
                Mst_PartUnitService.Instance.WA_Mst_PartUnit_Create(objRQ_Mst_PartUnit, userState.AddressAPIs);

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
        public ActionResult Update(string partunitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(partunitcode))
            {
                var objMst_PartUnit = new Mst_PartUnit()
                {
                    PartUnitCode = partunitcode,
                };

                var strWhereClauseMst_PartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);

                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                if (objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                {
                    return View(objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0]);
                }
            }
            return View(new Mst_PartUnit());
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
                var objMst_PartUnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartUnit>(model);
                var objMst_PartUnit = new Mst_PartUnit()
                {
                    PartUnitCode = objMst_PartUnitInput.PartUnitCode,
                };

                var strWhereClauseMst_PartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);

                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                if (objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                {
                    objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0].PartUnitCode = objMst_PartUnitInput.PartUnitCode;
                    objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0].FlagActive = objMst_PartUnitInput.FlagActive;
                    objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0].PartUnitName = objMst_PartUnitInput.PartUnitName;
                    objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0].FlagUnitStd = objMst_PartUnitInput.FlagUnitStd;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_PartUnit = TableName.Mst_PartUnit + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartUnit, TblMst_PartUnit.PartUnitName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartUnit, TblMst_PartUnit.FlagActive);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartUnit, TblMst_PartUnit.FlagUnitStd);

                    objRQ_Mst_PartUnit.Ft_WhereClause = null;
                    objRQ_Mst_PartUnit.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_PartUnit.Rt_Cols_Mst_PartUnit = null;
                    objRQ_Mst_PartUnit.Mst_PartUnit = objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0];

                    Mst_PartUnitService.Instance.WA_Mst_PartUnit_Update(objRQ_Mst_PartUnit, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục đơn vị sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị sản phẩm '" + objMst_PartUnitInput.PartUnitCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã đơn vị sản phẩm!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string partunitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(partunitcode))
            {
                var objMst_PartUnit = new Mst_PartUnit()
                {
                    PartUnitCode = partunitcode,
                };

                var strWhereClauseMst_PartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);

                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                if (objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                {
                    return View(objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0]);
                }
            }
            return View(new Mst_PartUnit());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string partunitcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(partunitcode))
            {
                var objMst_PartUnit = new Mst_PartUnit()
                {
                    PartUnitCode = partunitcode,
                };

                var strWhereClauseMst_PartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);

                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                if (objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                {
                    objRQ_Mst_PartUnit.Mst_PartUnit = objRT_Mst_PartUnitCur.Lst_Mst_PartUnit[0];

                    Mst_PartUnitService.Instance.WA_Mst_PartUnit_Delete(objRQ_Mst_PartUnit, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục đơn vị sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị sản phẩm '" + partunitcode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã đơn vị sản phẩm!");
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
                var listMst_PartUnit = new List<Mst_PartUnit>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 3)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartUnit.PartUnitCode]))
                            {
                                exitsData = "Mã danh mục Tỉnh/Thành phố không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartUnit.PartUnitName]))
                            {
                                exitsData = "Tên Tỉnh/Thành phố không được trống!" + strRows;
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
                            var Mst_PartUnitCur = table.Rows[i][TblMst_PartUnit.PartUnitCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_PartUnitCur = table.Rows[j][TblMst_PartUnit.PartUnitCode].ToString().Trim();
                                    if (Mst_PartUnitCur.Equals(_Mst_PartUnitCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã đơn vị sản phẩm '" + Mst_PartUnitCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_PartUnit = DataTableCmUtils.ToListof<Mst_PartUnit>(table); ;
                    // Gọi hàm save data
                    if (listMst_PartUnit != null && listMst_PartUnit.Count > 0)
                    {
                        foreach (var item in listMst_PartUnit)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit
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
                                Rt_Cols_Mst_PartUnit = null,
                                Mst_PartUnit = item,
                            };
                            Mst_PartUnitService.Instance.WA_Mst_PartUnit_Create(objRQ_Mst_PartUnit, userState.AddressAPIs);
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
            var list = new List<Mst_PartUnit>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartUnit).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_PartUnit"));

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
            var listMst_PartUnit = new List<Mst_PartUnit>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
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
                    // RQ_Mst_PartUnit
                    Rt_Cols_Mst_PartUnit = "*",
                    Mst_PartUnit = null,
                };

                var objRT_Mst_PartUnitCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                if (objRT_Mst_PartUnitCur != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null)
                {
                    if (objRT_Mst_PartUnitCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_PartUnitCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_PartUnitCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitCur.Lst_Mst_PartUnit.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_PartUnit.AddRange(objRT_Mst_PartUnitCur.Lst_Mst_PartUnit);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartUnit).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_PartUnit, dicColNames, filePath, string.Format("Mst_PartUnit"));


                    #region["Export các trang còn lại"]
                    listMst_PartUnit.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_PartUnit.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_PartUnitExportCur = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, userState.AddressAPIs);
                            if (objRT_Mst_PartUnitExportCur != null && objRT_Mst_PartUnitExportCur.Lst_Mst_PartUnit != null && objRT_Mst_PartUnitExportCur.Lst_Mst_PartUnit.Count > 0)
                            {
                                listMst_PartUnit.AddRange(objRT_Mst_PartUnitExportCur.Lst_Mst_PartUnit);
                                ExcelExport.ExportToExcelFromList(listMst_PartUnit, dicColNames, filePath, string.Format("Mst_PartUnit" + i));
                                listMst_PartUnit.Clear();
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
        private string strWhereClause_Mst_PartUnit(Mst_PartUnit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartUnit = TableName.Mst_PartUnit + ".";
            if (!CUtils.IsNullOrEmpty(data.PartUnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.PartUnitCode, CUtils.StrValueOrNull(data.PartUnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartUnitName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.PartUnitName, "%" + CUtils.StrValueOrNull(data.PartUnitName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
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
                 {"PartUnitCode","Mã đơn vị"},
                 {"PartUnitName","Tên đơn vị"},
                 {"FlagUnitStd","Đơn vị cơ bản"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PartUnitCode","Mã đơn vị"},
                 {"PartUnitName","Tên đơn vị"},
                 {"FlagUnitStd","Đơn vị cơ bản"},
            };
        }
        #endregion
    }
}