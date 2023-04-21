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
    public class OS_PrdCenter_Mst_VATRateController : AdminBaseController
    {
        // GET: OS_PrdCenter_Mst_VATRate
        public ActionResult Index(string vatratecode = "", string vatrate = "", string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_VATRate>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_VATRate>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                {
                    VATRateCode = vatratecode,
                    VATRate = vatrate,
                };

                var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);

                var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                    Ft_WhereClause = strWhereClauseMst_VATRate,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_VATRate
                    Rt_Cols_Mst_VATRate = "*",
                    Mst_VATRate = null,
                };

                var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

                if (objRT_Mst_VATRateCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_VATRateCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_VATRateCur != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_VATRateCur.Lst_Mst_VATRate);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.VATRateCode = vatratecode;
            ViewBag.VATRate = vatrate;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới thuế suất VAT"]
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
                var objMst_VATRateInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_VATRate>(model);
                //var title = "";
                var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate
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
                    // RQ_Mst_VATRate
                    Rt_Cols_Mst_VATRate = null,
                    Mst_VATRate = objMst_VATRateInput
                };
                OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Create(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới thuế suất VAT thành công!");
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

        #region["Sửa thuế suất VAT"]

        [HttpGet]
        public ActionResult Update(string vatratecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(vatratecode))
            {
                var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                {
                    VATRateCode = vatratecode,
                };

                var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);

                var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                    Ft_WhereClause = strWhereClauseMst_VATRate,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_VATRate
                    Rt_Cols_Mst_VATRate = "*",
                    Mst_VATRate = null,
                };

                var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
                if (objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                {
                    return View(objRT_Mst_VATRateCur.Lst_Mst_VATRate[0]);
                }
            }
            return View(new ProductCentrer.Mst_VATRate());
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
                    var objMst_VATRateInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_PrdCenter_Mst_VATRate>(model);
                    var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                    {
                        VATRateCode = objMst_VATRateInput.VATRateCode,
                    };

                    var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);

                    var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                        Ft_WhereClause = strWhereClauseMst_VATRate,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_VATRate
                        Rt_Cols_Mst_VATRate = "*",
                        Mst_VATRate = null,
                    };

                    var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
                    if (objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                    {
                        objRT_Mst_VATRateCur.Lst_Mst_VATRate[0].VATDesc = objMst_VATRateInput.VATDesc;
                        objRT_Mst_VATRateCur.Lst_Mst_VATRate[0].VATRate = objMst_VATRateInput.VATRate;
                        objRT_Mst_VATRateCur.Lst_Mst_VATRate[0].FlagActive = objMst_VATRateInput.FlagActive;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_VATRate = TableName.Mst_VATRate + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_VATRate, TblMst_VATRate.VATRate);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_VATRate, TblMst_VATRate.VATDesc);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_VATRate, TblMst_VATRate.FlagActive);

                        objRQ_Mst_VATRate.Ft_WhereClause = null;
                        objRQ_Mst_VATRate.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_VATRate.Rt_Cols_Mst_VATRate = null;
                        objRQ_Mst_VATRate.Mst_VATRate = objRT_Mst_VATRateCur.Lst_Mst_VATRate[0];

                        OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Update(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa thuế suất VAT thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã thuế suất VAT '" + objMst_VATRateInput.VATRateCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã thuế suất VAT trống!");
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

        #region["Xóa thuế suất VAT"]
        //[HttpGet]
        //public ActionResult Detail(string vatratecode)
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Detail(string vatratecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(vatratecode))
            {
                var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                {
                    VATRateCode = vatratecode,
                };

                var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);

                var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                    Ft_WhereClause = strWhereClauseMst_VATRate,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_VATRate
                    Rt_Cols_Mst_VATRate = "*",
                    Mst_VATRate = null,
                };

                var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
                if (objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                {
                    return View(objRT_Mst_VATRateCur.Lst_Mst_VATRate[0]);
                }
            }
            return View(new ProductCentrer.Mst_VATRate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string vatratecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(vatratecode))
                {
                    var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                    {
                        VATRateCode = vatratecode,
                    };

                    var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);

                    var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                        Ft_WhereClause = strWhereClauseMst_VATRate,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_VATRate
                        Rt_Cols_Mst_VATRate = "*",
                        Mst_VATRate = null,
                    };

                    var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
                    if (objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                    {
                        objRQ_Mst_VATRate.Mst_VATRate = objRT_Mst_VATRateCur.Lst_Mst_VATRate[0];

                        OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Delete(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa thuế suất VAT thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã thuế suất VAT '" + vatratecode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã thuế suất VAT trống!");
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
                var listMst_VATRate = new List<ProductCentrer.Mst_VATRate>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_VATRate.VATRateCode]))
                            {
                                exitsData = "Mã thuế suất VAT không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_VATRate.VATRate]))
                            {
                                exitsData = "Thuế suất VAT không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (!CUtils.IsNullOrEmpty(dr[TblMst_VATRate.VATDesc]))
                            {
                                var vatdesc = CUtils.StrValueOrNull(dr[TblMst_VATRate.VATDesc]);
                                if (vatdesc.Length > Nonsense.RemarkLength)
                                {
                                    exitsData = "Mô tả > " + Nonsense.RemarkLength + " ký tự!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                
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
                            var vatRateCodeCur = table.Rows[i][TblMst_VATRate.VATRateCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _vatRateCodeCur = table.Rows[j][TblMst_VATRate.VATRateCode].ToString().Trim();
                                    if (vatRateCodeCur.Equals(_vatRateCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã thuế suất VAT '" + vatRateCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_VATRate = DataTableCmUtils.ToListof<ProductCentrer.Mst_VATRate>(table);
                    // Gọi hàm save data
                    if (listMst_VATRate != null && listMst_VATRate.Count > 0)
                    {

                        foreach (var item in listMst_VATRate)
                        {
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate
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
                                // RQ_Mst_VATRate
                                Rt_Cols_Mst_VATRate = null,
                                Mst_VATRate = item,
                            };
                            OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Create(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

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
            var list = new List<ProductCentrer.Mst_VATRate>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_VATRate).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_VATRate"));

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
        //public ActionResult Export(string spectype1 = "", string spectype1name = "", string init = "init", int page = 0)
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_VATRate = new List<ProductCentrer.Mst_VATRate>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                
                var strWhereClauseMst_VATRate = "";

                var objRQ_Mst_VATRate = new ProductCentrer.RQ_Mst_VATRate()
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
                    Ft_WhereClause = strWhereClauseMst_VATRate,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_VATRate
                    Rt_Cols_Mst_VATRate = "*",
                    Mst_VATRate = null,
                };

                var objRT_Mst_VATRateCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);

                if (objRT_Mst_VATRateCur != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate != null)
                {
                    if (objRT_Mst_VATRateCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_VATRateCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_VATRateCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateCur.Lst_Mst_VATRate.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_VATRate.AddRange(objRT_Mst_VATRateCur.Lst_Mst_VATRate);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_VATRate).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_VATRate, dicColNames, filePath, string.Format("Mst_VATRate"));


                    #region["Export các trang còn lại"]
                    listMst_VATRate.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_VATRate.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_VATRateExportCur = OS_PrdCenter_Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs_ProductCentrer);
                            if (objRT_Mst_VATRateExportCur != null && objRT_Mst_VATRateExportCur.Lst_Mst_VATRate != null && objRT_Mst_VATRateExportCur.Lst_Mst_VATRate.Count > 0)
                            {
                                listMst_VATRate.AddRange(objRT_Mst_VATRateExportCur.Lst_Mst_VATRate);
                                ExcelExport.ExportToExcelFromList(listMst_VATRate, dicColNames, filePath, string.Format("Mst_VATRate_" + i));
                                listMst_VATRate.Clear();
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
                {TblMst_VATRate.VATRateCode,"Mã thuế suất VAT (*)"},
                {TblMst_VATRate.VATRate,"Thuế suất VAT (*)"},
                {TblMst_VATRate.VATDesc,"Mô tả"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_VATRate.VATRateCode,"Mã thuế suất VAT (*)"},
                {TblMst_VATRate.VATRate,"Thuế suất VAT (*)"},
                {TblMst_VATRate.VATDesc,"Mô tả"},
                {TblMst_VATRate.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_VATRate(ProductCentrer.Mst_VATRate data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_VATRate = TableName.Mst_VATRate+ ".";
            if (!CUtils.IsNullOrEmpty(data.VATRateCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_VATRate + TblMst_VATRate.VATRateCode, CUtils.StrValueOrNull(data.VATRateCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.VATRate))
            {
                sbSql.AddWhereClause(Tbl_Mst_VATRate + TblMst_VATRate.VATRate, "%" + CUtils.StrValueOrNull(data.VATRate) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_VATRate + TblMst_VATRate.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}