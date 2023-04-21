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
    public class Mst_CountryController : AdminBaseController
    {
        // GET: Mst_Country
        [HttpGet]
        public ActionResult Index( string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_Country>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Country>()
            };
            var itemCount = 0;
            var pageCount = 0;
            // (không có nút tìm kiếm => load data luôn)
            init = String.Format("{0}", "search");
            if (init != "init")
            {
                #region["Search"]
                var objRQ_Mst_Country = new RQ_Mst_Country()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Country
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);

                if (objRT_Mst_CountryCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_CountryCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_CountryCur != null && objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_CountryCur.Lst_Mst_Country);
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

        #region["Tạo mới danh mục quốc gia"]
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
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_CountryInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Country>(model);
                //var title = "";
                var objRQ_Mst_Country = new RQ_Mst_Country
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
                    // RQ_Mst_Country
                    Rt_Cols_Mst_Country = null,
                    Mst_Country = objMst_CountryInput
                };
                Mst_CountryService.Instance.WA_Mst_Country_Create(objRQ_Mst_Country, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới danh mục quốc gia thành công!");
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

        #region["Sửa danh mục quốc gia"]
        [HttpGet]
        public ActionResult Update(string countrycode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(countrycode))
            {
                var objMst_Country = new Mst_Country()
                {
                    CountryCode = countrycode,
                };

                var strWhereClauseMst_Country = strWhereClause_Mst_Country(objMst_Country);

                var objRQ_Mst_Country = new RQ_Mst_Country()
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
                    // RQ_Mst_TaxType
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                if (objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                {
                    return View(objRT_Mst_CountryCur.Lst_Mst_Country[0]);
                }
            }
            return View(new Mst_Country());
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
                var objMst_CountryInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Country>(model);
                var objMst_Country = new Mst_Country()
                {
                    CountryCode = objMst_CountryInput.CountryCode,
                };

                var strWhereClauseMst_Country = strWhereClause_Mst_Country(objMst_Country);

                var objRQ_Mst_Country = new RQ_Mst_Country()
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
                    // RQ_Mst_TaxType
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                if (objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                {
                    objRT_Mst_CountryCur.Lst_Mst_Country[0].CountryName = objMst_CountryInput.CountryName;
                    objRT_Mst_CountryCur.Lst_Mst_Country[0].FlagActive = objMst_CountryInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Country = TableName.Mst_Country + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Country, TblMst_Country.CountryName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Country, TblMst_Country.FlagActive);

                    objRQ_Mst_Country.Ft_WhereClause = null;
                    objRQ_Mst_Country.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Country.Rt_Cols_Mst_Country = null;
                    objRQ_Mst_Country.Mst_Country = objRT_Mst_CountryCur.Lst_Mst_Country[0];

                    Mst_CountryService.Instance.WA_Mst_Country_Update(objRQ_Mst_Country, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục quốc gia thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã quốc gia '" + objMst_CountryInput.CountryCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã quốc gia trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa danh mục quốc gia"]
        [HttpGet]
        public ActionResult Detail(string countrycode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(countrycode))
            {
                var objMst_Country = new Mst_Country()
                {
                    CountryCode = countrycode,
                };

                var strWhereClauseMst_Country = strWhereClause_Mst_Country(objMst_Country);

                var objRQ_Mst_Country = new RQ_Mst_Country()
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
                    // RQ_Mst_Country
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                if (objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                {
                    return View(objRT_Mst_CountryCur.Lst_Mst_Country[0]);
                }
            }
            return View(new Mst_Country());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string countrycode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(countrycode))
            {
                var objMst_Country = new Mst_Country()
                {
                    CountryCode = countrycode,
                };

                var strWhereClauseMst_Country = strWhereClause_Mst_Country(objMst_Country);

                var objRQ_Mst_Country = new RQ_Mst_Country()
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
                    // RQ_Mst_Country
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                if (objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                {
                    objRQ_Mst_Country.Mst_Country = objRT_Mst_CountryCur.Lst_Mst_Country[0];

                    Mst_CountryService.Instance.WA_Mst_Country_Delete(objRQ_Mst_Country, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục quốc gia thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã danh mục quốc gia '" + countrycode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã danh mục quốc gia trống!");
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
                var listMst_Country= new List<Mst_Country>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Country.CountryCode]))
                            {
                                exitsData = "Mã danh mục quốc gia không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Country.CountryName]))
                            {
                                exitsData = "Tên quốc gia không được trống!" + strRows;
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
                            var mst_countryCur = table.Rows[i][TblMst_Country.CountryCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _mst_countryCur = table.Rows[j][TblMst_Country.CountryCode].ToString().Trim();
                                    if (mst_countryCur.Equals(_mst_countryCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã danh mục quốc gia '" + mst_countryCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Country = DataTableCmUtils.ToListof<Mst_Country>(table); ;
                    // Gọi hàm save data
                    if (listMst_Country != null && listMst_Country.Count > 0)
                    {
                        foreach (var item in listMst_Country)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_Country = new RQ_Mst_Country()
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
                                // RQ_Mst_Country
                                Rt_Cols_Mst_Country = null,
                                Mst_Country = item,
                            };

                            Mst_CountryService.Instance.WA_Mst_Country_Create(objRQ_Mst_Country, addressAPIs);
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
            var list = new List<Mst_Country>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Country).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Country"));

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
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Country = new List<Mst_Country>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_Country = new RQ_Mst_Country()
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
                    // RQ_Mst_Country
                    Rt_Cols_Mst_Country = "*",
                    Mst_Country = null,
                };

                var objRT_Mst_CountryCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                if (objRT_Mst_CountryCur != null && objRT_Mst_CountryCur.Lst_Mst_Country != null)
                {
                    if (objRT_Mst_CountryCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_CountryCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_CountryCur.Lst_Mst_Country != null && objRT_Mst_CountryCur.Lst_Mst_Country.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Country.AddRange(objRT_Mst_CountryCur.Lst_Mst_Country);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Country).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Country, dicColNames, filePath, string.Format("Mst_Country"));


                    #region["Export các trang còn lại"]
                    listMst_Country.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Country.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_CountryExportCur = Mst_CountryService.Instance.WA_Mst_Country_Get(objRQ_Mst_Country, addressAPIs);
                            if (objRT_Mst_CountryExportCur != null && objRT_Mst_CountryExportCur.Lst_Mst_Country != null && objRT_Mst_CountryExportCur.Lst_Mst_Country.Count > 0)
                            {
                                listMst_Country.AddRange(objRT_Mst_CountryExportCur.Lst_Mst_Country);
                                ExcelExport.ExportToExcelFromList(listMst_Country, dicColNames, filePath, string.Format("Mst_Country" + i));
                                listMst_Country.Clear();
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
                {TblMst_Country.CountryCode,"Mã quốc gia (*)"},
                {TblMst_Country.CountryName,"Tên quốc gia (*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Country.CountryCode,"Mã quốc gia (*)"},
                {TblMst_Country.CountryName,"Tên quốc gia (*)"},
                {TblMst_Country.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Mst_Country(Mst_Country data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Country = TableName.Mst_Country + ".";
            if (!CUtils.IsNullOrEmpty(data.CountryCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Country + TblMst_Country.CountryCode, CUtils.StrValueOrNull(data.CountryCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CountryName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Country + TblMst_Country.CountryName, "%" + CUtils.StrValueOrNull(data.CountryName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Country + TblMst_Country.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}