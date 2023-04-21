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

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class OS_PrdCenter_Mst_CurrencyExController : AdminBaseController
    {
        // GET: OS_PrdCenter_Mst_CurrencyEx
        public ActionResult Index(string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_CurrencyEx>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_CurrencyEx>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var strWhereClauseMst_CurrencyEx = "";

                var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                    Ft_WhereClause = strWhereClauseMst_CurrencyEx,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_CurrencyEx
                    Rt_Cols_Mst_CurrencyEx = "*",
                    Mst_CurrencyEx = null,
                };

                var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

                if (objRT_Mst_CurrencyExCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_CurrencyExCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_CurrencyExCur != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
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

        #region["Tạo mới loại tiền"]

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
                var objMst_CurrencyExInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_CurrencyEx>(model);
                //var title = "";
                var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx
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
                    // RQ_Mst_CurrencyEx
                    Rt_Cols_Mst_CurrencyEx = null,
                    Mst_CurrencyEx = objMst_CurrencyExInput
                };
                OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Create(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới loại tiền thành công!");
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

        #region["Sửa loại tiền"]
        [HttpGet]
        public ActionResult Update(string currencycode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(currencycode))
            {
                var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                {
                    CurrencyCode = currencycode,
                };

                var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);

                var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                    Ft_WhereClause = strWhereClauseMst_CurrencyEx,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_CurrencyEx
                    Rt_Cols_Mst_CurrencyEx = "*",
                    Mst_CurrencyEx = null,
                };

                var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
                if (objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                {
                    return View(objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0]);
                }
            }
            return View(new ProductCentrer.Mst_CurrencyEx());
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
                    var objMst_CurrencyExInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_PrdCenter_Mst_CurrencyEx>(model);
                    var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                    {
                        CurrencyCode = objMst_CurrencyExInput.CurrencyCode,
                    };

                    var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);

                    var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                        Ft_WhereClause = strWhereClauseMst_CurrencyEx,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_CurrencyEx
                        Rt_Cols_Mst_CurrencyEx = "*",
                        Mst_CurrencyEx = null,
                    };

                    var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
                    if (objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                    {
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].CurrencyName = objMst_CurrencyExInput.CurrencyName;
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].BaseCurrencyCode = objMst_CurrencyExInput.BaseCurrencyCode;
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].BuyRate = objMst_CurrencyExInput.BuyRate;
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].SellRate = objMst_CurrencyExInput.SellRate;
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].UpdatedTime = objMst_CurrencyExInput.UpdatedTime;
                        objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0].InterEx = objMst_CurrencyExInput.InterEx;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_CurrencyEx = TableName.Mst_CurrencyEx + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.CurrencyName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.BaseCurrencyCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.BuyRate);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.SellRate);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.UpdatedTime);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.InterEx);
                        //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CurrencyEx, TblMst_CurrencyEx.FlagActive);

                        objRQ_Mst_CurrencyEx.Ft_WhereClause = null;
                        objRQ_Mst_CurrencyEx.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyEx = null;
                        objRQ_Mst_CurrencyEx.Mst_CurrencyEx = objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0];

                        OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Update(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa loại tiền thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã loại tiền '" + objMst_CurrencyExInput.CurrencyCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại tiền trống!");
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

        #region["Chi tiết - Xóa loại tiền"]
        [HttpGet]
        public ActionResult Detail(string currencycode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(currencycode))
            {
                var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                {
                    CurrencyCode = currencycode,
                };

                var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);

                var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                    Ft_WhereClause = strWhereClauseMst_CurrencyEx,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_CurrencyEx
                    Rt_Cols_Mst_CurrencyEx = "*",
                    Mst_CurrencyEx = null,
                };

                var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
                if (objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                {
                    return View(objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0]);
                }
            }
            return View(new ProductCentrer.Mst_CurrencyEx());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string currencycode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(currencycode))
                {
                    var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                    {
                        CurrencyCode = currencycode,
                    };

                    var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);

                    var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                        Ft_WhereClause = strWhereClauseMst_CurrencyEx,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_CurrencyEx
                        Rt_Cols_Mst_CurrencyEx = "*",
                        Mst_CurrencyEx = null,
                    };

                    var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
                    if (objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                    {
                        objRQ_Mst_CurrencyEx.Mst_CurrencyEx = objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx[0];

                        OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Delete(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa loại tiền thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã loại tiền '" + currencycode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại tiền trống!");
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
                var listMst_CurrencyEx = new List<ProductCentrer.Mst_CurrencyEx>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 6)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_CurrencyEx.CurrencyCode]))
                            {
                                exitsData = "Mã loại tiền không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_CurrencyEx.CurrencyName]))
                            {
                                exitsData = "Tên loại tiền không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_CurrencyEx.BuyRate]))
                            {
                                exitsData = "Tỉ giá mua không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var buyRate = CUtils.StrValueOrNull(dr[TblMst_CurrencyEx.BuyRate]);
                                if (!CUtils.IsNumeric(buyRate))
                                {
                                    exitsData = "Tỉ giá mua không phải kiểu số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDecimal(buyRate) < 0)
                                    {
                                        exitsData = "Tỉ giá mua >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_CurrencyEx.SellRate]))
                            {
                                exitsData = "Tỉ giá bán không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var sellRate = CUtils.StrValueOrNull(dr[TblMst_CurrencyEx.SellRate]);
                                if (!CUtils.IsNumeric(sellRate))
                                {
                                    exitsData = "Tỉ giá bán không phải kiểu số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDecimal(sellRate) < 0)
                                    {
                                        exitsData = "Tỉ giá bán >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }

                            //if (CUtils.IsNullOrEmpty(dr[TblMst_CurrencyEx.UpdatedTime]))
                            //{
                            //    exitsData = "Thời gian cập nhật không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var updatedTime = CUtils.StrValueOrNull(dr[TblMst_CurrencyEx.UpdatedTime]);
                            //    if (!CUtils.IsDateTime(updatedTime))
                            //    {
                            //        exitsData = "Thời gian cập nhật không hợp lệ!" + strRows;
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
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
                            var currencyCodeCur = table.Rows[i][TblMst_CurrencyEx.CurrencyCode].ToString().Trim();
                            
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _currencyCodeCur = table.Rows[j][TblMst_CurrencyEx.CurrencyCode].ToString().Trim();
                                    
                                    if (currencyCodeCur.Equals(_currencyCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã loại tiền '" + currencyCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_CurrencyEx = DataTableCmUtils.ToListof<ProductCentrer.Mst_CurrencyEx>(table);
                    // Gọi hàm save data
                    if (listMst_CurrencyEx != null && listMst_CurrencyEx.Count > 0)
                    {

                        foreach (var item in listMst_CurrencyEx)
                        {
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx
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
                                // RQ_Mst_CurrencyEx
                                Rt_Cols_Mst_CurrencyEx = null,
                                Mst_CurrencyEx = item,
                            };
                            OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Create(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

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
            var list = new List<ProductCentrer.Mst_CurrencyEx>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_CurrencyEx).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_CurrencyEx"));

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
        //public ActionResult Export(string brandcode = "", string brandname = "", string init = "init", int page = 0)
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_CurrencyEx = new List<ProductCentrer.Mst_CurrencyEx>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                var strWhereClauseOS_PrdCenter_Mst_CurrencyEx = "";

                var objRQ_Mst_CurrencyEx = new ProductCentrer.RQ_Mst_CurrencyEx()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_CurrencyEx,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_CurrencyEx
                    Rt_Cols_Mst_CurrencyEx = "*",
                    Mst_CurrencyEx = null,
                };

                var objRT_Mst_CurrencyExCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);

                if (objRT_Mst_CurrencyExCur != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null)
                {
                    if (objRT_Mst_CurrencyExCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_CurrencyExCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_CurrencyEx.AddRange(objRT_Mst_CurrencyExCur.Lst_Mst_CurrencyEx);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_CurrencyEx).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_CurrencyEx, dicColNames, filePath, string.Format("Mst_CurrencyEx"));


                    #region["Export các trang còn lại"]
                    listMst_CurrencyEx.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_CurrencyEx.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_CurrencyExExportCur = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_Mst_CurrencyEx_Get(objRQ_Mst_CurrencyEx, addressAPIs_ProductCentrer);
                            if (objRT_Mst_CurrencyExExportCur != null && objRT_Mst_CurrencyExExportCur.Lst_Mst_CurrencyEx != null && objRT_Mst_CurrencyExExportCur.Lst_Mst_CurrencyEx.Count > 0)
                            {
                                listMst_CurrencyEx.AddRange(objRT_Mst_CurrencyExExportCur.Lst_Mst_CurrencyEx);
                                ExcelExport.ExportToExcelFromList(listMst_CurrencyEx, dicColNames, filePath, string.Format("OS_PrdCenter_Mst_CurrencyEx_" + i));
                                listMst_CurrencyEx.Clear();
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
                {TblMst_CurrencyEx.CurrencyCode,"Mã loại tiền (*)"},
                {TblMst_CurrencyEx.CurrencyName,"Tên loại tiền (*)"},
                {TblMst_CurrencyEx.BaseCurrencyCode,"Loại tiền gốc"},
                {TblMst_CurrencyEx.BuyRate,"Tỉ giá mua (*)"},
                {TblMst_CurrencyEx.SellRate,"Tỉ giá bán (*)"},
                {TblMst_CurrencyEx.InterEx,"Nguồn lấy tỉ giá"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_CurrencyEx.CurrencyCode,"Mã loại tiền (*)"},
                {TblMst_CurrencyEx.CurrencyName,"Tên loại tiền (*)"},
                {TblMst_CurrencyEx.BaseCurrencyCode,"Loại tiền gốc"},
                {TblMst_CurrencyEx.BuyRate,"Tỉ giá mua (*)"},
                {TblMst_CurrencyEx.SellRate,"Tỉ giá bán (*)"},
                {TblMst_CurrencyEx.InterEx,"Nguồn lấy tỉ giá"},
                {TblMst_CurrencyEx.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_CurrencyEx(ProductCentrer.Mst_CurrencyEx data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CurrencyEx = TableName.Mst_CurrencyEx + ".";

            if (!CUtils.IsNullOrEmpty(data.CurrencyCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_CurrencyEx + TblMst_CurrencyEx.CurrencyCode, CUtils.StrValueOrNull(data.CurrencyCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Spec(OS_PrdCenter_Mst_Spec data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}