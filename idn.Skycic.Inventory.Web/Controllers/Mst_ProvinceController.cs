using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Controllers;
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
    public class Mst_ProvinceController : AdminBaseController
    {
        // GET: Mst_Province
        [HttpGet]
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_Province>(0, recordcount)
            {
                DataList = new List<Mst_Province>()
            };
            var itemCount = 0;
            var pageCount = 0;
            
                #region["Search"]
                var objRQ_Mst_Province = new RQ_Mst_Province()
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
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);

                if (objRT_Mst_ProvinceCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_ProvinceCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_ProvinceCur != null && objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_ProvinceCur.Lst_Mst_Province);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
            #endregion
            ViewBag.PageCur = page.ToString();
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới danh mục Tỉnh/Thành phố"]
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
                var objMst_ProvinceInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Province>(model);
                //var title = "";
                var objRQ_Mst_Province = new RQ_Mst_Province
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
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = null,
                    Mst_Province = objMst_ProvinceInput
                };
                Mst_ProvinceService.Instance.WA_Mst_Province_Create(objRQ_Mst_Province, userState.AddressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới danh mục Tỉnh/Thành phố thành công!");
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

        #region["Sửa danh mục Tỉnh/Thành phố"]
        [HttpGet]
        public ActionResult Update(string provincecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(provincecode))
            {
                var objMst_Province = new Mst_Province()
                {
                    ProvinceCode = provincecode,
                };

                var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

                var objRQ_Mst_Province = new RQ_Mst_Province()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Province,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                if (objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                {
                    return View(objRT_Mst_ProvinceCur.Lst_Mst_Province[0]);
                }
            }
            return View(new Mst_Province());
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
                var objMst_ProvinceInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Province>(model);
                var objMst_Province = new Mst_Province()
                {
                    ProvinceCode = objMst_ProvinceInput.ProvinceCode,
                };

                var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

                var objRQ_Mst_Province = new RQ_Mst_Province()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Province,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                if (objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                {
                    objRT_Mst_ProvinceCur.Lst_Mst_Province[0].ProvinceName = objMst_ProvinceInput.ProvinceName;
                    objRT_Mst_ProvinceCur.Lst_Mst_Province[0].FlagActive = objMst_ProvinceInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Province = TableName.Mst_Province + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Province, TblMst_Province.ProvinceName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Province, TblMst_Province.FlagActive);

                    objRQ_Mst_Province.Ft_WhereClause = null;
                    objRQ_Mst_Province.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Province.Rt_Cols_Mst_Province = null;
                    objRQ_Mst_Province.Mst_Province = objRT_Mst_ProvinceCur.Lst_Mst_Province[0];

                    Mst_ProvinceService.Instance.WA_Mst_Province_Update(objRQ_Mst_Province, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục Tỉnh/Thành phố thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Tỉnh/Thành phố '" + objMst_ProvinceInput.ProvinceCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã Tỉnh/Thành phố trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa danh mục Tỉnh/Thành phố"]
        [HttpGet]
        public ActionResult Detail(string Provincecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(Provincecode))
            {
                var objMst_Province = new Mst_Province()
                {
                    ProvinceCode = Provincecode,
                };

                var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

                var objRQ_Mst_Province = new RQ_Mst_Province()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Province,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                if (objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                {
                    return View(objRT_Mst_ProvinceCur.Lst_Mst_Province[0]);
                }
            }
            return View(new Mst_Province());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string Provincecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(Provincecode))
            {
                var objMst_Province = new Mst_Province()
                {
                    ProvinceCode = Provincecode,
                };

                var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

                var objRQ_Mst_Province = new RQ_Mst_Province()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Province,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                if (objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                {
                    objRQ_Mst_Province.Mst_Province = objRT_Mst_ProvinceCur.Lst_Mst_Province[0];

                    Mst_ProvinceService.Instance.WA_Mst_Province_Delete(objRQ_Mst_Province, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục Tỉnh/Thành phố thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Tỉnh/Thành phố '" + Provincecode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã Tỉnh/Thành phố trống!");
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
                var listMst_Province = new List<Mst_Province>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Province.ProvinceCode]))
                            {
                                exitsData = "Mã danh mục Tỉnh/Thành phố không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Province.ProvinceName]))
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
                            var mst_ProvinceCur = table.Rows[i][TblMst_Province.ProvinceCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _mst_ProvinceCur = table.Rows[j][TblMst_Province.ProvinceCode].ToString().Trim();
                                    if (mst_ProvinceCur.Equals(_mst_ProvinceCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Tỉnh/Thành phố '" + mst_ProvinceCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Province = DataTableCmUtils.ToListof<Mst_Province>(table); ;
                    // Gọi hàm save data
                    if (listMst_Province != null && listMst_Province.Count > 0)
                    {
                        foreach (var item in listMst_Province)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_Province = new RQ_Mst_Province()
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
                                // RQ_Mst_Province
                                Rt_Cols_Mst_Province = null,
                                Mst_Province = item,
                            };

                            Mst_ProvinceService.Instance.WA_Mst_Province_Create(objRQ_Mst_Province, userState.AddressAPIs);
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
            var list = new List<Mst_Province>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Province).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Province"));

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
            var listMst_Province = new List<Mst_Province>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_Province = new RQ_Mst_Province()
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
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };

                var objRT_Mst_ProvinceCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                if (objRT_Mst_ProvinceCur != null && objRT_Mst_ProvinceCur.Lst_Mst_Province != null)
                {
                    if (objRT_Mst_ProvinceCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_ProvinceCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_ProvinceCur.Lst_Mst_Province != null && objRT_Mst_ProvinceCur.Lst_Mst_Province.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Province.AddRange(objRT_Mst_ProvinceCur.Lst_Mst_Province);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Province).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Province, dicColNames, filePath, string.Format("Mst_Province"));


                    #region["Export các trang còn lại"]
                    listMst_Province.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Province.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_ProvinceExportCur = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, userState.AddressAPIs);
                            if (objRT_Mst_ProvinceExportCur != null && objRT_Mst_ProvinceExportCur.Lst_Mst_Province != null && objRT_Mst_ProvinceExportCur.Lst_Mst_Province.Count > 0)
                            {
                                listMst_Province.AddRange(objRT_Mst_ProvinceExportCur.Lst_Mst_Province);
                                ExcelExport.ExportToExcelFromList(listMst_Province, dicColNames, filePath, string.Format("Mst_Province" + i));
                                listMst_Province.Clear();
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
                {TblMst_Province.ProvinceCode,"Mã Tỉnh/Thành phố (*)"},
                {TblMst_Province.ProvinceName,"Tên Tỉnh/Thành phố (*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Province.ProvinceCode,"Mã Tỉnh/Thành phố (*)"},
                {TblMst_Province.ProvinceName,"Tên Tỉnh/Thành phố (*)"},
                {TblMst_Province.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.ProvinceCode, CUtils.StrValueOrNull(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProvinceName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.ProvinceName, "%" + CUtils.StrValueOrNull(data.ProvinceName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}