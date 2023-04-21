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
    public class Mst_PartTypeController : AdminBaseController
    {
        // GET: Mst_PartType
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            ViewBag.PageCur = page.ToString();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_PartType>(0, recordcount)
            {
                DataList = new List<Mst_PartType>()
            };
            var itemCount = 0;
            var pageCount = 0;
            init = "search";
            if (init != "Search")
            {
                #region["Search"]
                var objRQ_Mst_PartType = new RQ_Mst_PartType()
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
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);

                if (objRT_Mst_PartTypeCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_PartTypeCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_PartTypeCur != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_PartTypeCur.Lst_Mst_PartType);
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
                var objMst_PartTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartType>(model);
                //var title = "";
                var objRQ_Mst_PartType = new RQ_Mst_PartType
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
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = null,
                    Mst_PartType = objMst_PartTypeInput
                };
                Mst_PartTypeService.Instance.WA_Mst_PartType_Create(objRQ_Mst_PartType, userState.AddressAPIs);

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
        public ActionResult Update(string parttype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(parttype))
            {
                var objMst_PartType = new Mst_PartType()
                {
                    PartType = parttype,
                };

                var strWhereClauseMst_PartType = strWhereClause_Mst_PartType(objMst_PartType);

                var objRQ_Mst_PartType = new RQ_Mst_PartType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                if (objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                {
                    return View(objRT_Mst_PartTypeCur.Lst_Mst_PartType[0]);
                }
            }
            return View(new Mst_PartType());
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
                var objMst_PartTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_PartType>(model);
                var objMst_PartType = new Mst_PartType()
                {
                    PartType = objMst_PartTypeInput.PartType,
                };

                var strWhereClauseMst_PartType = strWhereClause_Mst_PartType(objMst_PartType);

                var objRQ_Mst_PartType = new RQ_Mst_PartType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                if (objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                {
                    objRT_Mst_PartTypeCur.Lst_Mst_PartType[0].PartType = objMst_PartTypeInput.PartType;
                    objRT_Mst_PartTypeCur.Lst_Mst_PartType[0].FlagActive = objMst_PartTypeInput.FlagActive;
                    objRT_Mst_PartTypeCur.Lst_Mst_PartType[0].PartTypeName = objMst_PartTypeInput.PartTypeName;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_PartType = TableName.Mst_PartType + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartType, TblMst_PartType.PartTypeName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartType, TblMst_PartType.FlagActive);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_PartType, TblMst_PartType.PartTypeName);

                    objRQ_Mst_PartType.Ft_WhereClause = null;
                    objRQ_Mst_PartType.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_PartType.Rt_Cols_Mst_PartType = null;
                    objRQ_Mst_PartType.Mst_PartType = objRT_Mst_PartTypeCur.Lst_Mst_PartType[0];

                    Mst_PartTypeService.Instance.WA_Mst_PartType_Update(objRQ_Mst_PartType, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục loại sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại sản phẩm '" + objMst_PartTypeInput.PartType + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã loại sản phẩm!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string parttype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(parttype))
            {
                var objMst_PartType = new Mst_PartType()
                {
                    PartType = parttype,
                };

                var strWhereClauseMst_PartType = strWhereClause_Mst_PartType(objMst_PartType);

                var objRQ_Mst_PartType = new RQ_Mst_PartType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                if (objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                {
                    return View(objRT_Mst_PartTypeCur.Lst_Mst_PartType[0]);
                }
            }
            return View(new Mst_PartType());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string parttype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(parttype))
            {
                var objMst_PartType = new Mst_PartType()
                {
                    PartType = parttype,
                };

                var strWhereClauseMst_PartType = strWhereClause_Mst_PartType(objMst_PartType);

                var objRQ_Mst_PartType = new RQ_Mst_PartType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_PartType,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                if (objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                {
                    objRQ_Mst_PartType.Mst_PartType = objRT_Mst_PartTypeCur.Lst_Mst_PartType[0];

                    Mst_PartTypeService.Instance.WA_Mst_PartType_Delete(objRQ_Mst_PartType, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục loại sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại sản phẩm '" + parttype + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã loại sản phẩm!");
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
                var listMst_PartType = new List<Mst_PartType>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartType.PartType]))
                            {
                                exitsData = "Mã danh mục nhóm sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_PartType.PartTypeName]))
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
                            var Mst_PartTypeCur = table.Rows[i][TblMst_PartType.PartType].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_PartTypeCur = table.Rows[j][TblMst_PartType.PartType].ToString().Trim();
                                    if (Mst_PartTypeCur.Equals(_Mst_PartTypeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã loại sản phẩm '" + Mst_PartTypeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_PartType = DataTableCmUtils.ToListof<Mst_PartType>(table); ;
                    // Gọi hàm save data
                    if (listMst_PartType != null && listMst_PartType.Count > 0)
                    {
                        foreach (var item in listMst_PartType)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_PartType = new RQ_Mst_PartType
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
                                Rt_Cols_Mst_PartType = null,
                                Mst_PartType = item,
                            };
                            Mst_PartTypeService.Instance.WA_Mst_PartType_Create(objRQ_Mst_PartType, userState.AddressAPIs);
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
            var list = new List<Mst_PartType>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartType).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_PartType"));

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
            var listMst_PartType = new List<Mst_PartType>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_PartType = new RQ_Mst_PartType()
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
                    // RQ_Mst_PartType
                    Rt_Cols_Mst_PartType = "*",
                    Mst_PartType = null,
                };

                var objRT_Mst_PartTypeCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                if (objRT_Mst_PartTypeCur != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType != null)
                {
                    if (objRT_Mst_PartTypeCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_PartTypeCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_PartTypeCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeCur.Lst_Mst_PartType.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_PartType.AddRange(objRT_Mst_PartTypeCur.Lst_Mst_PartType);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_PartType).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_PartType, dicColNames, filePath, string.Format("Mst_PartType"));


                    #region["Export các trang còn lại"]
                    listMst_PartType.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_PartType.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_PartTypeExportCur = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, userState.AddressAPIs);
                            if (objRT_Mst_PartTypeExportCur != null && objRT_Mst_PartTypeExportCur.Lst_Mst_PartType != null && objRT_Mst_PartTypeExportCur.Lst_Mst_PartType.Count > 0)
                            {
                                listMst_PartType.AddRange(objRT_Mst_PartTypeExportCur.Lst_Mst_PartType);
                                ExcelExport.ExportToExcelFromList(listMst_PartType, dicColNames, filePath, string.Format("Mst_PartType" + i));
                                listMst_PartType.Clear();
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
        private string strWhereClause_Mst_PartType(Mst_PartType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartType = TableName.Mst_PartType + ".";
            if (!CUtils.IsNullOrEmpty(data.PartType))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.PartType, CUtils.StrValueOrNull(data.PartType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.PartTypeName, "%" + CUtils.StrValueOrNull(data.PartTypeName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
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
                 {"PartType","Mã loại sản phẩm"},
                 {"PartTypeName","Tên loại sản phẩm"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PartType","Mã loại sản phẩm"},
                 {"PartTypeName","Tên loại sản phẩm"},
            };
        }
        #endregion
    }
}