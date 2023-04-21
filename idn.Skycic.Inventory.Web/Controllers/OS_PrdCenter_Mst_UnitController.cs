using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using idn.Skycic.Inventory.Common.ModelsUI;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class OS_PrdCenter_Mst_UnitController : AdminBaseController
    {
        // Chú ý: Xử lý không sử dụng OS_PrdCenter_Mst_Unit.UnitCode mà sử dụng: Mst_Unit.UnitCode
        // GET: OS_PrdCenter_Mst_Unit
        public ActionResult Index(string unitcode = "", string unitname = "", string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_Unit>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_Unit>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_Unit = new ProductCentrer.Mst_Unit()
                {
                    UnitCode = unitcode,
                    UnitName = unitname,
                };

                var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordStart = (page * PageSizeConfig).ToString(),
                    //Ft_RecordCount = PageSizeConfig.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Unit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Unit
                    Rt_Cols_Mst_Unit = "*",
                    Mst_Unit = null,
                };

                var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

                if (objRT_Mst_UnitCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_UnitCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_UnitCur != null && objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_UnitCur.Lst_Mst_Unit);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.UnitCode = unitcode;
            ViewBag.UnitName = unitname;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới Đơn vị"]

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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_UnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Unit>(model);
                //var title = "";
                var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit
                {
                    FlagIsDelete = null,
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Unit
                    Rt_Cols_Mst_Unit = null,
                    Mst_Unit = objMst_UnitInput
                };
                OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Create(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới đơn vị thành công!");
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

        #region["Sửa Đơn vị"]
        [HttpGet]
        public ActionResult Update(string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(unitcode))
            {
                var objMst_Unit = new ProductCentrer.Mst_Unit()
                {
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Unit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Unit
                    Rt_Cols_Mst_Unit = "*",
                    Mst_Unit = null,
                };

                var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
                if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                {
                    return View(objRT_Mst_UnitCur.Lst_Mst_Unit[0]);
                }
            }
            return View(new ProductCentrer.Mst_Unit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_UnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Unit>(model);
                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        UnitCode = objMst_UnitInput.UnitCode,
                    };

                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                    var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Unit,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Unit
                        Rt_Cols_Mst_Unit = "*",
                        Mst_Unit = null,
                    };

                    var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
                    if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                    {
                        objRT_Mst_UnitCur.Lst_Mst_Unit[0].UnitName = objMst_UnitInput.UnitName;
                        objRT_Mst_UnitCur.Lst_Mst_Unit[0].FlagActive = objMst_UnitInput.FlagActive;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Unit = TableName.Mst_Unit + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Unit, TblMst_Unit.UnitName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Unit, TblMst_Unit.FlagActive);

                        objRQ_Mst_Unit.Ft_WhereClause = null;
                        objRQ_Mst_Unit.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Unit.Rt_Cols_Mst_Unit = null;
                        //objRQ_Mst_Unit.Mst_Unit = objRT_Mst_UnitCur.Lst_Mst_Unit[0];
                        objRQ_Mst_Unit.Mst_Unit = null;

                        OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Update(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa đơn vị thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đơn vị '" + objMst_UnitInput.UnitCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị trống!");
                }

                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa Đơn vị"]
        [HttpGet]
        public ActionResult Detail(string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(unitcode))
            {
                var objMst_Unit = new ProductCentrer.Mst_Unit()
                {
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Unit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Unit
                    Rt_Cols_Mst_Unit = "*",
                    Mst_Unit = null,
                };

                var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
                if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                {
                    return View(objRT_Mst_UnitCur.Lst_Mst_Unit[0]);
                }
            }
            return View(new ProductCentrer.Mst_Unit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string unitcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(unitcode))
                {
                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        UnitCode = unitcode,
                    };

                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                    var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Unit,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Unit
                        Rt_Cols_Mst_Unit = "*",
                        Mst_Unit = null,
                    };

                    var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
                    if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                    {
                        objRQ_Mst_Unit.Mst_Unit = objRT_Mst_UnitCur.Lst_Mst_Unit[0];

                        OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Delete(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa đơn vị thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đơn vị '" + unitcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị trống!");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
            //return Json(new { Success = false, Detail = resultEntry.Detail });
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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
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
                var listMst_Unit = new List<ProductCentrer.Mst_Unit>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Unit.UnitCode]))
                            {
                                exitsData = "Mã đơn vị không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Unit.UnitName]))
                            {
                                exitsData = "Tên đơn vị không được trống!" + strRows;
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
                            var unitcodeCur = table.Rows[i][TblMst_Unit.UnitCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _unitcodeCur = table.Rows[j][TblMst_Unit.UnitCode].ToString().Trim();
                                    if (unitcodeCur.Equals(_unitcodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã đơn vị '" + unitcodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Unit = DataTableCmUtils.ToListof<ProductCentrer.Mst_Unit>(table);
                    // Gọi hàm save data
                    if (listMst_Unit != null && listMst_Unit.Count > 0)
                    {

                        foreach (var item in listMst_Unit)
                        {
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_Mst_Unit
                                Rt_Cols_Mst_Unit = null,
                                Mst_Unit = item,
                            };
                            OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Create(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<OS_PrdCenter_Mst_Unit>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Unit).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Unit"));

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
        //public ActionResult Export(string unitcode = "", string brandname = "", string init = "init", int page = 0)
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Unit = new List<ProductCentrer.Mst_Unit>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                //var objMst_Unit = new ProductCentrer.Mst_Unit()
                //{
                //    UnitCode = "",
                //    UnitName = "",
                //    FlagActive = "",
                //};

                //var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                var strWhereClauseMst_Unit = "";

                var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordStart = (page * PageSizeConfig).ToString(),
                    //Ft_RecordCount = PageSizeConfig.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Unit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Unit
                    Rt_Cols_Mst_Unit = "*",
                    Mst_Unit = null,
                };

                var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

                if (objRT_Mst_UnitCur != null && objRT_Mst_UnitCur.Lst_Mst_Unit != null)
                {
                    if (objRT_Mst_UnitCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_UnitCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Unit.AddRange(objRT_Mst_UnitCur.Lst_Mst_Unit);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Unit).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Unit, dicColNames, filePath, string.Format("Mst_Unit"));


                    #region["Export các trang còn lại"]
                    listMst_Unit.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i < pageCount; i++)
                        {
                            objRQ_Mst_Unit.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_UnitExportCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
                            if (objRT_Mst_UnitExportCur != null && objRT_Mst_UnitExportCur.Lst_Mst_Unit != null && objRT_Mst_UnitExportCur.Lst_Mst_Unit.Count > 0)
                            {
                                listMst_Unit.AddRange(objRT_Mst_UnitExportCur.Lst_Mst_Unit);
                                ExcelExport.ExportToExcelFromList(listMst_Unit, dicColNames, filePath, string.Format("Mst_Unit_" + i));
                                listMst_Unit.Clear();
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Export(string model)
        //{
        //    var userState = this.UserState;
        //    Session["UserState"] = userState;
        //    var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
        //    var waUserCode = userState.SysUser.UserCode;
        //    var waUserPassword = userState.SysUser.UserPassword;
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    var listMst_Unit = new List<ProductCentrer.Mst_Unit>();
        //    string url = "";
        //    var itemCount = 0;
        //    var pageCount = 0;


        //    try
        //    {
        //        #region["Search"]
        //        var strWhereClauseMst_Unit = "";
        //        var objMst_Unit = new ProductCentrer.Mst_Unit()
        //        {
        //            UnitCode = "",
        //            UnitName = "",
        //            FlagActive = "",
        //        };
        //        var listObjectSearchInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ObjectSearchInput>>(model);
        //        if (listObjectSearchInput != null && listObjectSearchInput.Count > 0)
        //        {
        //            foreach (var item in listObjectSearchInput)
        //            {
        //                if (item != null)
        //                {
        //                    if (!CUtils.IsNullOrEmpty(item.Key))
        //                    {
        //                        var key = CUtils.StrValue(item.Key);
        //                        var value = CUtils.StrValue(item.Value);
        //                        switch (key)
        //                        {
        //                            case TblMst_Unit.UnitCode:
        //                            {
        //                                objMst_Unit.UnitCode = value;
        //                                break;
        //                            }
        //                            case TblMst_Unit.UnitName:
        //                            {
        //                                objMst_Unit.UnitName = value;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

        //        //var strWhereClauseMst_Unit = "";

        //        var objRQ_Mst_Unit = new ProductCentrer.RQ_Mst_Unit()
        //        {
        //            // WARQBase
        //            Tid = GetNextTId(),
        //            GwUserCode = OS_ProductCentrer_GwUserCode,
        //            GwPassword = OS_ProductCentrer_GwPassword,
        //            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
        //            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
        //            FuncType = null,
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            //Ft_RecordStart = (page * PageSizeConfig).ToString(),
        //            //Ft_RecordCount = PageSizeConfig.ToString(),
        //            Ft_WhereClause = strWhereClauseMst_Unit,
        //            Ft_Cols_Upd = null,
        //            WAUserCode = waUserCode,
        //            WAUserPassword = waUserPassword,
        //            UtcOffset = userState.UtcOffset,
        //            // RQ_Mst_Unit
        //            Rt_Cols_Mst_Unit = "*",
        //            Mst_Unit = null,
        //        };

        //        var objRT_Mst_UnitCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);

        //        if (objRT_Mst_UnitCur != null && objRT_Mst_UnitCur.Lst_Mst_Unit != null)
        //        {
        //            if (objRT_Mst_UnitCur.MySummaryTable != null)
        //            {
        //                itemCount = Convert.ToInt32(objRT_Mst_UnitCur.MySummaryTable.MyCount);
        //            }
        //            if (objRT_Mst_UnitCur.Lst_Mst_Unit != null && objRT_Mst_UnitCur.Lst_Mst_Unit.Count > 0)
        //            {
        //                pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
        //            }

        //            listMst_Unit.AddRange(objRT_Mst_UnitCur.Lst_Mst_Unit);

        //            Dictionary<string, string> dicColNames = GetImportDicColums();
        //            string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Unit).Name), ref url);
        //            ExcelExport.ExportToExcelFromList(listMst_Unit, dicColNames, filePath, string.Format("Mst_Unit"));


        //            #region["Export các trang còn lại"]
        //            listMst_Unit.Clear();
        //            if (pageCount > 1)
        //            {
        //                for (var i = 1; i <= pageCount; i++)
        //                {
        //                    objRQ_Mst_Unit.Ft_RecordStart = (i * RowsWorksheets).ToString();

        //                    var objRT_Mst_UnitExportCur = OS_PrdCenter_Mst_UnitService.Instance.WA_Mst_Unit_Get(objRQ_Mst_Unit, addressAPIs_ProductCentrer);
        //                    if (objRT_Mst_UnitExportCur != null && objRT_Mst_UnitExportCur.Lst_Mst_Unit != null && objRT_Mst_UnitExportCur.Lst_Mst_Unit.Count > 0)
        //                    {
        //                        listMst_Unit.AddRange(objRT_Mst_UnitExportCur.Lst_Mst_Unit);
        //                        ExcelExport.ExportToExcelFromList(listMst_Unit, dicColNames, filePath, string.Format("Mst_Unit_" + i));
        //                        listMst_Unit.Clear();
        //                    }
        //                }
        //            }
        //            #endregion

        //            return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
        //        }
        //        #endregion

        //        else
        //        {
        //            return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return Json(new { Success = false, Detail = resultEntry.Detail });
        //}
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Unit.UnitCode,"Mã đơn vị (*)"},
                {TblMst_Unit.UnitName,"Tên đơn vị (*)"},
                //{TblMst_Unit.Remark,"Mô tả"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Unit.UnitCode,"Mã đơn vị (*)"},
                {TblMst_Unit.UnitName,"Tên đơn vị (*)"},
                //{TblMst_Unit.Remark,"Mô tả"},
                {TblMst_Unit.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_Unit(ProductCentrer.Mst_Unit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Unit = TableName.Mst_Unit + ".";
            if (!CUtils.IsNullOrEmpty(data.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.UnitCode, CUtils.StrValueOrNull(data.UnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.UnitName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.UnitName, "%" + CUtils.StrValueOrNull(data.UnitName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}