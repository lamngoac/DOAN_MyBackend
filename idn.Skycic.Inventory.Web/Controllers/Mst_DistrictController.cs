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
    public class Mst_DistrictController : AdminBaseController
    {
        // GET: Mst_District
        [HttpGet]
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_District>(0, PageSizeConfig)
            {
                DataList = new List<Mst_District>()
            };
            var itemCount = 0;
            var pageCount = 0;
            #region["Danh mục Tỉnh/Thành phố "]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = FlagActive,
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
            listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
            ViewBag.ListMst_Province = listMst_Province;
            #endregion
            
                #region["Search"]

                var objRQ_Mst_District = new RQ_Mst_District()
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
                    Rt_Cols_Mst_District = "*"
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);

                if (objRT_Mst_DistrictCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_DistrictCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_DistrictCur != null && objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_DistrictCur.Lst_Mst_District);
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

        #region["Tạo mới danh mục phụ lục"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            #region["Danh mục tờ khai"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = FlagActive,
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
            listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
            ViewBag.ListMst_Province = listMst_Province;
            #endregion
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
                var objMst_DistrictInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_District>(model);
                //var title = "";
                var objRQ_Mst_District = new RQ_Mst_District
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
                    Rt_Cols_Mst_District = null,
                    Mst_District = objMst_DistrictInput
                };
                Mst_DistrictService.Instance.WA_Mst_District_Create(objRQ_Mst_District, userState.AddressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới danh mục quận huyện thành công!");
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

        #region["Sửa danh mục phụ lục"]
        [HttpGet]
        public ActionResult Update(string districtcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(districtcode))
            {
                var objMst_District = new Mst_District()
                {
                    DistrictCode = districtcode
                };

                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                if (objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                {
                    #region["Danh mục Tỉnh/Thành phố"]
                    var objMst_Province = new Mst_Province()
                    {
                        FlagActive = FlagActive,
                    };
                    var listMst_Province = new List<Mst_Province>();
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                    ViewBag.ListMst_Province = listMst_Province;
                    #endregion
                    return View(objRT_Mst_DistrictCur.Lst_Mst_District[0]);
                }
            }
            return View(new Mst_District());
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
                var objMst_DistrictInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_District>(model);
                var objMst_District = new Mst_District()
                {
                    DistrictCode = objMst_DistrictInput.DistrictCode,
                    ProvinceCode = objMst_DistrictInput.ProvinceCode,
                };

                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                if (objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                {
                    objRT_Mst_DistrictCur.Lst_Mst_District[0].DistrictName = objMst_DistrictInput.DistrictName;
                    objRT_Mst_DistrictCur.Lst_Mst_District[0].ProvinceCode = objMst_DistrictInput.ProvinceCode;
                    objRT_Mst_DistrictCur.Lst_Mst_District[0].FlagActive = objMst_DistrictInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_District = TableName.Mst_District + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_District, TblMst_District.DistrictName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_District, TblMst_District.ProvinceCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_District, TblMst_District.FlagActive);

                    objRQ_Mst_District.Ft_WhereClause = null;
                    objRQ_Mst_District.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_District.Rt_Cols_Mst_District = null;
                    objRQ_Mst_District.Mst_District = objRT_Mst_DistrictCur.Lst_Mst_District[0];

                    Mst_DistrictService.Instance.WA_Mst_District_Update(objRQ_Mst_District, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục Quận huyện thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("ID danh mục quận huyện '" + objMst_DistrictInput.DistrictCode + "' - '" + objMst_DistrictInput.ProvinceCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("ID danh mục quận huyện trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa danh mục quận huyện"]
        [HttpGet]
        public ActionResult Detail(string districtcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(districtcode))
            {
                var objMst_District = new Mst_District()
                {
                    DistrictCode = districtcode
                };

                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                if (objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                {
                    #region["Danh mục tờ khai"]
                    var objMst_Province = new Mst_Province()
                    {
                        FlagActive = FlagActive,
                    };
                    var listMst_Province = new List<Mst_Province>();
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                    ViewBag.ListMst_Province = listMst_Province;
                    #endregion
                    return View(objRT_Mst_DistrictCur.Lst_Mst_District[0]);
                }
            }
            return View(new Mst_District());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string districtcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(districtcode))
            {
                var objMst_District = new Mst_District()
                {
                    DistrictCode = districtcode
                };

                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                if (objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                {
                    objRQ_Mst_District.Mst_District = objRT_Mst_DistrictCur.Lst_Mst_District[0];

                    Mst_DistrictService.Instance.WA_Mst_District_Delete(objRQ_Mst_District, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục quận huyện thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("ID danh mục quận huyện '" + districtcode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("ID danh mục quận huyện trống!");
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
                var listMst_District = new List<Mst_District>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_District.DistrictCode]))
                            {
                                exitsData = "ID danh mục quận huyện không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (CUtils.IsNullOrEmpty(dr[TblMst_District.DistrictName]))
                            {
                                exitsData = "Tên danh mục quận huyện không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (CUtils.IsNullOrEmpty(dr[TblMst_District.ProvinceCode]))
                            {
                                exitsData = "Mã Tỉnh/Thành phố không được trống!" + strRows;
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
                            var districtcodeCur = table.Rows[i][TblMst_District.DistrictCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _districtcodeCur = table.Rows[j][TblMst_District.DistrictCode].ToString().Trim();
                                    if (districtcodeCur.Equals(_districtcodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "ID danh mục quận huyện '" + districtcodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_District = DataTableCmUtils.ToListof<Mst_District>(table); ;
                    // Gọi hàm save data
                    if (listMst_District != null && listMst_District.Count > 0)
                    {
                        foreach (var item in listMst_District)
                        {
                            //item.FlagActive = "1";
                            var objRQ_Mst_District = new RQ_Mst_District()
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
                                Rt_Cols_Mst_District = null,
                                Mst_District = item,
                            };

                            Mst_DistrictService.Instance.WA_Mst_District_Create(objRQ_Mst_District,userState.AddressAPIs);
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
            var list = new List<Mst_District>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_District).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_District"));

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
            var listMst_District = new List<Mst_District>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            try
            {

                #region["Search"]

                var objRQ_Mst_District = new RQ_Mst_District()
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
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };

                var objRT_Mst_DistrictCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                if (objRT_Mst_DistrictCur != null && objRT_Mst_DistrictCur.Lst_Mst_District != null)
                {
                    if (objRT_Mst_DistrictCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_DistrictCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_DistrictCur.Lst_Mst_District != null && objRT_Mst_DistrictCur.Lst_Mst_District.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_District.AddRange(objRT_Mst_DistrictCur.Lst_Mst_District);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_District).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_District, dicColNames, filePath, string.Format("Mst_District"));


                    #region["Export các trang còn lại"]
                    listMst_District.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_District.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_DistrictExportCur = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, userState.AddressAPIs);
                            if (objRT_Mst_DistrictExportCur != null && objRT_Mst_DistrictExportCur.Lst_Mst_District != null && objRT_Mst_DistrictExportCur.Lst_Mst_District.Count > 0)
                            {
                                listMst_District.AddRange(objRT_Mst_DistrictExportCur.Lst_Mst_District);
                                ExcelExport.ExportToExcelFromList(listMst_District, dicColNames, filePath, string.Format("Mst_District_" + i));
                                listMst_District.Clear();
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
                {TblMst_District.DistrictCode,"ID danh mục quận huyện(*)"},
                {TblMst_District.ProvinceCode,"ID danh mục tỉnh/thành phố (*)"},
                {TblMst_District.DistrictName,"Tên danh mục quận huyện (*)"}
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
               {TblMst_District.DistrictCode,"ID danh mục quận huyện(*)"},
                {TblMst_District.ProvinceCode,"ID danh mục tỉnh/thành phố (*)"},
                {TblMst_District.DistrictName,"Tên danh mục quận huyện (*)"},
                {TblMst_Province.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.DistrictCode, CUtils.StrValueOrNull(data.DistrictCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DistrictName))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.DistrictName, "%" + CUtils.StrValueOrNull(data.DistrictName) + "%", "like");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
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