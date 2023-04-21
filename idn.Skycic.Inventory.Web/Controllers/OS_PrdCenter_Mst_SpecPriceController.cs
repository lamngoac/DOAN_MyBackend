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
    public class OS_PrdCenter_Mst_SpecPriceController : AdminBaseController
    {
        // GET: OS_PrdCenter_Mst_SpecPrice
        public ActionResult Index(string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_SpecPrice>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_SpecPrice>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var strWhereClauseMst_SpecPrice = "";

                var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                    Ft_WhereClause = strWhereClauseMst_SpecPrice,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecPrice
                    Rt_Cols_Mst_SpecPrice = "*",
                    Mst_SpecPrice = null,
                };

                var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecPriceCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_SpecPriceCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_SpecPriceCur != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice);
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

        #region["Tạo mới Map Spec - Price"]

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;

            #region["Mst_Spec"]

            var objMst_Spec = new ProductCentrer.Mst_Spec()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
            var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
            ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
            #endregion

            #region["Mst_Unit - Đơn vị"]

            var objMst_Unit = new ProductCentrer.Mst_Unit()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
            var listOS_PrdCenter_Mst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
            ViewBag.ListOS_PrdCenter_Mst_Unit = listOS_PrdCenter_Mst_Unit;
            #endregion

            #region["Mst_CurrencyEx - Loại tiền"]

            var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
            {
            };
            var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);
            var listMst_CurrencyEx = List_Mst_CurrencyEx(strWhereClauseMst_CurrencyEx);
            ViewBag.ListOS_PrdCenter_Mst_CurrencyEx = listMst_CurrencyEx;
            #endregion

            #region["Mst_VATRate - Thuế suất"]

            var objMst_VATRate = new ProductCentrer.Mst_VATRate()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);
            var listMst_VATRate = List_Mst_VATRate(strWhereClauseMst_VATRate);
            ViewBag.ListOS_PrdCenter_Mst_VATRate = listMst_VATRate;
            ViewBag.OrgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            #endregion
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
                var objMst_SpecPriceInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecPrice>(model);
                //var title = "";
                var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                    // RQ_Mst_SpecPrice
                    Rt_Cols_Mst_SpecPrice = null,
                    Mst_SpecPrice = objMst_SpecPriceInput
                };
                OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Create(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới Map Spec - Price thành công!");
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
        public ActionResult Update(string speccode, string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
            {
                var objMst_SpecPrice = new ProductCentrer.Mst_SpecPrice()
                {
                    SpecCode = speccode,
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_SpecPrice = strWhereClause_Mst_SpecPrice(objMst_SpecPrice);

                var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                    Ft_WhereClause = strWhereClauseMst_SpecPrice,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecPrice
                    Rt_Cols_Mst_SpecPrice = "*",
                    Mst_SpecPrice = null,
                };


                var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                {
                    #region["Mst_Spec"]

                    var objMst_Spec = new ProductCentrer.Mst_Spec()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
                    var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
                    ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
                    #endregion

                    #region["Mst_Unit - Đơn vị"]

                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
                    var listMst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = listMst_Unit;
                    #endregion

                    #region["Mst_CurrencyEx - Loại tiền"]

                    var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                    {
                    };
                    var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);
                    var listMst_CurrencyEx = List_Mst_CurrencyEx(strWhereClauseMst_CurrencyEx);
                    ViewBag.ListOS_PrdCenter_Mst_CurrencyEx = listMst_CurrencyEx;
                    #endregion

                    #region["Mst_VATRate - Thuế suất"]

                    var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);
                    var listMst_VATRate = List_Mst_VATRate(strWhereClauseMst_VATRate);
                    ViewBag.ListOS_PrdCenter_Mst_VATRate = listMst_VATRate;
                    #endregion

                    return View(objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecPrice());
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
                    var objMst_SpecPriceInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecPrice>(model);
                    var objMst_SpecPrice = new ProductCentrer.Mst_SpecPrice()
                    {
                        CurrencyCode = objMst_SpecPriceInput.CurrencyCode,
                        SpecCode = objMst_SpecPriceInput.SpecCode,
                    };

                    var strWhereClauseMst_SpecPrice = strWhereClause_Mst_SpecPrice(objMst_SpecPrice);

                    var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                        Ft_WhereClause = strWhereClauseMst_SpecPrice,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecPrice
                        Rt_Cols_Mst_SpecPrice = "*",
                        Mst_SpecPrice = null,
                    };

                    var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                    {
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].BuyPrice = objMst_SpecPriceInput.BuyPrice;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].SellPrice = objMst_SpecPriceInput.SellPrice;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].CurrencyCode = objMst_SpecPriceInput.CurrencyCode;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].DiscountVND = objMst_SpecPriceInput.DiscountVND;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].VATRateCode = objMst_SpecPriceInput.VATRateCode;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].EffectDTimeStart = objMst_SpecPriceInput.EffectDTimeStart;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].Remark = objMst_SpecPriceInput.Remark;
                        objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0].FlagActive = objMst_SpecPriceInput.FlagActive;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_SpecPrice = TableName.Mst_SpecPrice + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.BuyPrice);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.SellPrice);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.CurrencyCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.DiscountVND);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.VATRateCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.EffectDTimeStart);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecPrice, TblMst_SpecPrice.FlagActive);

                        objRQ_Mst_SpecPrice.Ft_WhereClause = null;
                        objRQ_Mst_SpecPrice.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_SpecPrice.Rt_Cols_Mst_SpecPrice = null;
                        objRQ_Mst_SpecPrice.Mst_SpecPrice = objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0];

                        OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Update(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa Map Spec - Price thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Spec '" + objMst_SpecPriceInput.SpecCode + "' hoặc mã đơn vị '" + objMst_SpecPriceInput.UnitCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec hoặc mã đơn vị trống!");
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
        public ActionResult Detail(string speccode, string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
            {
                var objMst_SpecPrice = new ProductCentrer.Mst_SpecPrice()
                {
                    SpecCode = speccode,
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_SpecPrice = strWhereClause_Mst_SpecPrice(objMst_SpecPrice);

                var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                    Ft_WhereClause = strWhereClauseMst_SpecPrice,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecPrice
                    Rt_Cols_Mst_SpecPrice = "*",
                    Mst_SpecPrice = null,
                };

                var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                {
                    #region["Mst_Spec"]

                    var objMst_Spec = new ProductCentrer.Mst_Spec()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
                    var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
                    ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
                    #endregion

                    #region["Mst_Unit - Đơn vị"]

                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
                    var listMst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = listMst_Unit;
                    #endregion

                    #region["Mst_CurrencyEx - Loại tiền"]

                    var objMst_CurrencyEx = new ProductCentrer.Mst_CurrencyEx()
                    {
                    };
                    var strWhereClauseMst_CurrencyEx = strWhereClause_Mst_CurrencyEx(objMst_CurrencyEx);
                    var listMst_CurrencyEx = List_Mst_CurrencyEx(strWhereClauseMst_CurrencyEx);
                    ViewBag.ListOS_PrdCenter_Mst_CurrencyEx = listMst_CurrencyEx;
                    #endregion

                    #region["Mst_VATRate - Thuế suất"]

                    var objMst_VATRate = new ProductCentrer.Mst_VATRate()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_VATRate = strWhereClause_Mst_VATRate(objMst_VATRate);
                    var listMst_VATRate = List_Mst_VATRate(strWhereClauseMst_VATRate);
                    ViewBag.ListOS_PrdCenter_Mst_VATRate = listMst_VATRate;
                    #endregion
                    return View(objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecPrice());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string speccode, string unitcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
                {
                    var objMst_SpecPrice = new ProductCentrer.Mst_SpecPrice()
                    {
                        SpecCode = speccode,
                        UnitCode = unitcode,
                    };

                    var strWhereClauseMst_SpecPrice = strWhereClause_Mst_SpecPrice(objMst_SpecPrice);

                    var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                        Ft_WhereClause = strWhereClauseMst_SpecPrice,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecPrice
                        Rt_Cols_Mst_SpecPrice = "*",
                        Mst_SpecPrice = null,
                    };

                    var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                    {
                        objRQ_Mst_SpecPrice.Mst_SpecPrice = objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice[0];

                        OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Delete(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa Map Spec - Price thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Spec '" + speccode + "' hoặc mã đơn vị '" + unitcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec hoặc mã đơn vị trống!");
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
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
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
                var listMst_SpecPrice = new List<ProductCentrer.Mst_SpecPrice>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 7)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.SpecCode]))
                            {
                                exitsData = "Mã Spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.UnitCode]))
                            {
                                exitsData = "Mã đơn vị không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.BuyPrice]))
                            {
                                exitsData = "Giá mua không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var buyPrice = CUtils.StrValueOrNull(dr[TblMst_SpecPrice.BuyPrice]);
                                if (!CUtils.IsNumeric(buyPrice))
                                {
                                    exitsData = "Giá mua không phải kiểu số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDecimal(buyPrice) < 0)
                                    {
                                        exitsData = "Giá mua >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.SellPrice]))
                            {
                                exitsData = "Giá bán không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var sellPrice = CUtils.StrValueOrNull(dr[TblMst_SpecPrice.SellPrice]);
                                if (!CUtils.IsNumeric(sellPrice))
                                {
                                    exitsData = "Giá bán không phải kiểu số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDecimal(sellPrice) < 0)
                                    {
                                        exitsData = "Giá bán >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.CurrencyCode]))
                            {
                                exitsData = "Loại tiền không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (!CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.DiscountVND]))
                            {
                                var discountVND = CUtils.StrValueOrNull(dr[TblMst_SpecPrice.DiscountVND]);
                                if (!CUtils.IsNumeric(discountVND))
                                {
                                    exitsData = "Chiết khấu (VND) không phải kiểu số!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDecimal(discountVND) < 0)
                                    {
                                        exitsData = "Chiết khấu (VND) >= 0!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_SpecPrice.EffectDTimeStart]))
                            //{
                            //    exitsData = "Thời gian cập nhật không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var effectDTimeStart = CUtils.StrValueOrNull(dr[TblMst_SpecPrice.EffectDTimeStart]);
                            //    if (!CUtils.IsDateTime(effectDTimeStart))
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
                            var specCodeCur = table.Rows[i][TblMst_SpecPrice.SpecCode].ToString().Trim();
                            var unitCodeCur = table.Rows[i][TblMst_SpecPrice.UnitCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _brandCodeCur = table.Rows[j][TblMst_SpecPrice.SpecCode].ToString().Trim();
                                    var _unitCodeCur = table.Rows[j][TblMst_SpecPrice.UnitCode].ToString().Trim();
                                    if (specCodeCur.Equals(_brandCodeCur) && unitCodeCur.Equals(_unitCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Spec '" + specCodeCur + "' - mã đơn vị '" + unitCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_SpecPrice = DataTableCmUtils.ToListof<ProductCentrer.Mst_SpecPrice>(table);
                    // Gọi hàm save data
                    if (listMst_SpecPrice != null && listMst_SpecPrice.Count > 0)
                    {

                        foreach (var item in listMst_SpecPrice)
                        {
                            item.OrgID = orgID;
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice
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
                                // RQ_Mst_SpecPrice
                                Rt_Cols_Mst_SpecPrice = null,
                                Mst_SpecPrice = item,
                            };
                            OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Create(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

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
            var list = new List<ProductCentrer.Mst_SpecPrice>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecPrice).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_SpecPrice"));

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
            var listMst_SpecPrice = new List<ProductCentrer.Mst_SpecPrice>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                var strWhereClauseMst_SpecPrice = "";

                var objRQ_Mst_SpecPrice = new ProductCentrer.RQ_Mst_SpecPrice()
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
                    Ft_WhereClause = strWhereClauseMst_SpecPrice,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecPrice
                    Rt_Cols_Mst_SpecPrice = "*",
                    Mst_SpecPrice = null,
                };

                var objRT_Mst_SpecPriceCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecPriceCur != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null)
                {
                    if (objRT_Mst_SpecPriceCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_SpecPriceCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_SpecPrice.AddRange(objRT_Mst_SpecPriceCur.Lst_Mst_SpecPrice);
                    foreach (var item in listMst_SpecPrice)
                    {
                        if (!CUtils.IsNullOrEmpty(item.EffectDTimeStart) && CUtils.IsDateTime(item.EffectDTimeStart))
                        {
                            var dEffectDTimeStart = CUtils.ConvertingUTCToLocalTime(CUtils.StrValueOrNull(item.EffectDTimeStart));
                            item.EffectDTimeStart = dEffectDTimeStart.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                        } 
                    }

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecPrice).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_SpecPrice, dicColNames, filePath, string.Format("Mst_SpecPrice"));


                    #region["Export các trang còn lại"]
                    listMst_SpecPrice.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_SpecPrice.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_SpecPriceExportCur = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_Mst_SpecPrice_Get(objRQ_Mst_SpecPrice, addressAPIs_ProductCentrer);
                            if (objRT_Mst_SpecPriceExportCur != null && objRT_Mst_SpecPriceExportCur.Lst_Mst_SpecPrice != null && objRT_Mst_SpecPriceExportCur.Lst_Mst_SpecPrice.Count > 0)
                            {
                                listMst_SpecPrice.AddRange(objRT_Mst_SpecPriceExportCur.Lst_Mst_SpecPrice);
                                foreach (var item in listMst_SpecPrice)
                                {
                                    if (!CUtils.IsNullOrEmpty(item.EffectDTimeStart) && CUtils.IsDateTime(item.EffectDTimeStart))
                                    {
                                        var dEffectDTimeStart = CUtils.ConvertingUTCToLocalTime(CUtils.StrValueOrNull(item.EffectDTimeStart));
                                        item.EffectDTimeStart = dEffectDTimeStart.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                                    }
                                }
                                ExcelExport.ExportToExcelFromList(listMst_SpecPrice, dicColNames, filePath, string.Format("Mst_SpecPrice_" + i));
                                listMst_SpecPrice.Clear();
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
                {TblMst_SpecPrice.SpecCode,"Mã Spec (*)"},
                {TblMst_SpecPrice.UnitCode,"Mã đơn vị (*)"},
                {TblMst_SpecPrice.BuyPrice,"Giá mua (*)"},
                {TblMst_SpecPrice.SellPrice,"Giá bán (*)"},
                {TblMst_SpecPrice.CurrencyCode,"Loại tiền (*)"},
                {TblMst_SpecPrice.VATRateCode,"Mã thuế suất"},
                {TblMst_SpecPrice.DiscountVND,"Chiết khấu (VND)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_SpecPrice.OrgID,"Mã tổ chức (*)"},
                {TblMst_SpecPrice.SpecCode,"Mã Spec (*)"},
                {TblMst_SpecPrice.UnitCode,"Mã đơn vị (*)"},
                {TblMst_SpecPrice.BuyPrice,"Giá mua (*)"},
                {TblMst_SpecPrice.SellPrice,"Giá bán (*)"},
                {TblMst_SpecPrice.CurrencyCode,"Loại tiền (*)"},
                {TblMst_SpecPrice.VATRateCode,"Mã thuế suất"},
                {TblMst_SpecPrice.DiscountVND,"Chiết khấu (VND)"},
                {TblMst_SpecPrice.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_Unit(ProductCentrer.Mst_Unit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Unit = TableName.Mst_Unit + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Spec(ProductCentrer.Mst_Spec data)
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

        private string strWhereClause_Mst_VATRate(ProductCentrer.Mst_VATRate data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_VATRate = TableName.Mst_VATRate + ".";

            if (!CUtils.IsNullOrEmpty(data.VATRateCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_VATRate + TblMst_VATRate.VATRateCode, CUtils.StrValueOrNull(data.VATRateCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_VATRate + TblMst_VATRate.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecPrice(ProductCentrer.Mst_SpecPrice data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecPrice = TableName.Mst_SpecPrice + ".";
            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecPrice + TblMst_SpecUnit.SpecCode, CUtils.StrValueOrNull(data.SpecCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecPrice + TblMst_SpecUnit.UnitCode, CUtils.StrValueOrNull(data.UnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecPrice + TblMst_SpecUnit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion


    }
}