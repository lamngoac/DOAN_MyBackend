using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class DashboardRptController : ReportsBaseController
    {
        #region WIDGET

        #region Báo cáo hàng tồn kho
        public ActionResult WG_Rpt_Inv_InventoryBalance(string networkid = "", string lstinvcode = "", string callajax = "0")
        {
            if (User.Identity.IsAuthenticated && UserReportState != null)
            {
                ViewBag.NetworkID = networkid;
                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserReportState.Email);
                var waUserPassword = "";
                var networkID = networkid;
                var orgId = "";
                var addressAPIs = CUtils.StrValueOrNull(UserReportState.AddressAPIs);
                #endregion
                #region ["Danh sách kho"]
                var lstMstInventory = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = waUserPassword,
                    NetworkID = networkid,
                    OrgID = orgId,
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhere_Mst_Inventory,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Inventory = "*"
                };
                var json1 = new JavaScriptSerializer().Serialize(objRQ_Mst_Inventory);
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    lstMstInventory.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                }
                ViewBag.lstMstInventory = lstMstInventory;
                #endregion
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                var lstMst_Inventory = new List<Mst_Inventory>();
                var list_InvCode = new List<string>();
                if (lstinvcode != null && lstinvcode.Length > 0)
                {
                    list_InvCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lstinvcode);
                    foreach (var invcode in list_InvCode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = invcode });
                    }
                }
                var reportdateutc = DateTime.Now.ToString("yyyy-MM-dd");
                var objRQ_Rpt_Inv_InventoryBalance = new RQ_Rpt_Inv_InventoryBalance()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = waUserPassword,
                    NetworkID = networkid,
                    OrgID = orgId,
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ReportDateUTC = reportdateutc,
                    //ProductCode = productcode,
                    Lst_Mst_Inventory = lstMst_Inventory,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };
                var objRT_Rpt_Inv_InventoryBalance = Rpt_Inv_InventoryBalanceService.Instance.WA_Rpt_Inv_InventoryBalance(objRQ_Rpt_Inv_InventoryBalance, addressAPIs);
                var List_Rpt_Inv_InventoryBalanceUIBase = new List<Rpt_Inv_InventoryBalanceUI>();
                if (objRT_Rpt_Inv_InventoryBalance != null && objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
                    var ListProductCodeBaseStr = string.Join(",", ListProductCode);

                    #region ["Product Base"]
                    var List_Mst_Product = new List<Mst_Product>();
                    var strWhere_ProductBalance = "";
                    var sbSQLBalance = new StringBuilder();
                    sbSQLBalance.AddWhereClause("Mst_Product.ProductCodeBase", ListProductCodeBaseStr, "IN");
                    strWhere_ProductBalance = sbSQLBalance.ToString();
                    var objRQ_Mst_ProductBalance = new RQ_Mst_Product()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserReportState.Email),
                        WAUserPassword = waUserPassword,
                        NetworkID = networkid,
                        OrgID = orgId,
                        UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhere_ProductBalance,

                        Rt_Cols_Mst_Product = "*"
                    };
                    var objRT_Mst_ProductBalance = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_ProductBalance, addressAPIs);
                    if (objRT_Mst_ProductBalance != null && objRT_Mst_ProductBalance.Lst_Mst_Product.Count > 0)
                    {
                        List_Mst_Product.AddRange(objRT_Mst_ProductBalance.Lst_Mst_Product);
                    }
                    #endregion

                    foreach (var code in ListProductCode)
                    {
                        var listBalance = objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        double qtyTotalOKSum = 0;
                        double qtyAvailOKSum = 0;
                        double qtyBlockOKSum = 0;
                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                        qtyAvailOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyAvailOK));
                        qtyBlockOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBlockOK));
                        var listInvCode = listBalance.Select(inv => inv.InvCode).ToList();
                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);

                        var objRpt_Inv_InventoryBalanceUI = new Rpt_Inv_InventoryBalanceUI
                        {
                            OrgID = listBalance[0].OrgID,
                            ProductCode = listBalance[0].ProductCode,
                            mp_ProductCodeUser = listBalance[0].mp_ProductCodeUser,
                            mp_ProductCodeRoot = listBalance[0].mp_ProductCodeRoot,
                            mp_ProductName = listBalance[0].mp_ProductName,
                            mp_ProductNameEN = listBalance[0].mp_ProductNameEN,
                            mp_UnitCode = listBalance[0].mp_UnitCode,
                            mp_FlagLot = listBalance[0].mp_FlagLot,
                            mp_FlagSerial = listBalance[0].mp_FlagSerial,
                            //ListUnitCode = listUnitCode,
                            mp_ValConvert = listBalance[0].mp_ValConvert,
                            QtyTotalOK = qtyTotalOKSum,
                            QtyAvailOK = qtyAvailOKSum,
                            QtyBlockOK = qtyBlockOKSum,
                            ValMixBase = listBalance[0].ValMixBase,
                            ListInvCode = string.Join(",", listInvCode)
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalanceUIChild = new List<Rpt_Inv_InventoryBalanceUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalanceUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalanceUI.QtyTotalOK) * ratio;
                                var qtyAvailOK = Convert.ToDouble(objRpt_Inv_InventoryBalanceUI.QtyAvailOK) * ratio;
                                var qtyBlockOK = Convert.ToDouble(objRpt_Inv_InventoryBalanceUI.QtyBlockOK) * ratio;
                                var objRpt_Inv_InventoryBalanceUIChild = new Rpt_Inv_InventoryBalanceUI
                                {
                                    ProductCode = child.ProductCode,
                                    mp_ProductCodeUser = child.ProductCodeUser,
                                    mp_ProductCodeRoot = child.ProductCodeRoot,
                                    mp_ProductCodeBase = child.ProductCodeBase,
                                    mp_ProductName = child.ProductName,
                                    mp_ProductNameEN = child.ProductNameEN,
                                    mp_UnitCode = child.UnitCode,
                                    mp_FlagLot = child.FlagLot,
                                    mp_FlagSerial = child.FlagSerial,
                                    mp_ValConvert = child.ValConvert,
                                    QtyTotalOK = qtyTotalOK,
                                    QtyAvailOK = qtyAvailOK,
                                    QtyBlockOK = qtyBlockOK,
                                    ListInvCode = objRpt_Inv_InventoryBalanceUI.ListInvCode,
                                };
                                ListRpt_Inv_InventoryBalanceUIChild.Add(objRpt_Inv_InventoryBalanceUIChild);
                            }
                            objRpt_Inv_InventoryBalanceUI.ListRpt_Inv_InventoryBalanceUIChild = ListRpt_Inv_InventoryBalanceUIChild;
                        }
                        List_Rpt_Inv_InventoryBalanceUIBase.Add(objRpt_Inv_InventoryBalanceUI);
                    }
                }
                if (callajax == "1")
                {
                    return JsonView("BindData_WG_Rpt_Inv_InventoryBalance", List_Rpt_Inv_InventoryBalanceUIBase);
                }
                else
                {
                    return View(List_Rpt_Inv_InventoryBalanceUIBase);
                }
            }
            else
            {
                var redirectUrl = Reports_RedirectUrl();
                return Redirect(redirectUrl);
            }
            
        }
        #endregion

        public ActionResult WG_Rpt_Summary_In_Pivot(string networkid = "", string lstinvcode = "", string init = "init")
        {
            if (User.Identity.IsAuthenticated && UserReportState != null)
            {
                ViewBag.NetworkID = networkid;
                DateTime date = GetDateTimeServerClient(); // Current Month
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var addressAPIs = UserReportState.AddressAPIs;
                #region ["Danh sách kho"]
                var List_Mst_InventoryInOut = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = "",
                    NetworkID = networkid,
                    OrgID = "",
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhere_Mst_Inventory,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Inventory = "Mst_Inventory.InvCode"
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                }
                ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;

                var Lst_Mst_Inventory_RQ = new List<Mst_Inventory>();
                if (!init.Equals("init"))
                {
                    var Lst_InvCode_Search = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lstinvcode);
                    if (Lst_InvCode_Search != null && Lst_InvCode_Search.Count > 0)
                    {
                        foreach (var invCode in Lst_InvCode_Search)
                        {
                            var objMst_Inventory = new Mst_Inventory
                            {
                                InvCode = invCode
                            };
                            Lst_Mst_Inventory_RQ.Add(objMst_Inventory);
                        }
                    }
                }
                else
                {
                    Lst_Mst_Inventory_RQ.AddRange(List_Mst_InventoryInOut);
                }
                #endregion
                var Lst_Rpt_Summary_In_Out_PivotUI = new List<Rpt_Summary_In_Out_Pivot>();
                var objRQ_Rpt_Summary_In_Out_Pivot = new RQ_Rpt_Summary_In_Out_Pivot()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = "",
                    NetworkID = networkid,
                    OrgID = "",
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = firstDayOfMonth.ToString("yyyy-MM-dd") + " 00:00:00",
                    ApprDTimeUTCTo = lastDayOfMonth.ToString("yyyy-MM-dd") + " 23:59:59",
                    InventoryAction = "IN",
                    Lst_Mst_Inventory = Lst_Mst_Inventory_RQ
                };
                var objRT_Rpt_Summary_In_Out_Pivot = ReportsService.Instance.WA_Rpt_Summary_In_Out_Pivot(objRQ_Rpt_Summary_In_Out_Pivot, addressAPIs);
                if (objRT_Rpt_Summary_In_Out_Pivot != null && objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot?.Count > 0)
                {
                    var Lst_ProductCode_Distinct = objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot
                        .Select(item => item.ProductCode)
                        .Distinct()
                        .ToList();
                    foreach (var productcode in Lst_ProductCode_Distinct)
                    {
                        var Lst_Rpt_Summary_In_Out_Pivot_ProductCode
                            = objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot?
                            .Where(it => it.ProductCode.Equals(productcode))
                            .ToList();
                        var qtySum = Lst_Rpt_Summary_In_Out_Pivot_ProductCode?
                            .Sum(itQty => Convert.ToDouble(itQty.Qty));
                        var objRpt_Summary_In_Out_PivotUI = new Rpt_Summary_In_Out_Pivot
                        {
                            ProductCode = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductCode,
                            ProductCodeUser = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductCodeUser,
                            ProductName = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductName,
                            UnitCode = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].UnitCode,
                            Qty = qtySum,
                        };
                        Lst_Rpt_Summary_In_Out_PivotUI.Add(objRpt_Summary_In_Out_PivotUI);
                    }
                }
                if (!init.Equals("init"))
                {
                    return JsonView("BindData_WG_Rpt_Summary_In_Pivot", Lst_Rpt_Summary_In_Out_PivotUI);
                }
                return View(Lst_Rpt_Summary_In_Out_PivotUI);
            }
            else
            {
                var redirectUrl = Reports_RedirectUrl();
                return Redirect(redirectUrl);
            }
                
        }

        public ActionResult WG_Rpt_Summary_Out_Pivot(string networkid = "",string lstinvcode = "", string init = "init")
        {
            if (User.Identity.IsAuthenticated && UserReportState != null)
            {
                ViewBag.NetworkID = networkid;
                DateTime date = GetDateTimeServerClient(); // Current Month
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var addressAPIs = UserReportState.AddressAPIs;
                #region ["Danh sách kho"]
                var List_Mst_InventoryInOut = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = "",
                    NetworkID = networkid,
                    OrgID = "",
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhere_Mst_Inventory,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Inventory = "Mst_Inventory.InvCode"
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                }
                ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;

                var Lst_Mst_Inventory_RQ = new List<Mst_Inventory>();
                if (!init.Equals("init"))
                {
                    var Lst_InvCode_Search = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lstinvcode);
                    if (Lst_InvCode_Search != null && Lst_InvCode_Search.Count > 0)
                    {
                        foreach (var invCode in Lst_InvCode_Search)
                        {
                            var objMst_Inventory = new Mst_Inventory
                            {
                                InvCode = invCode
                            };
                            Lst_Mst_Inventory_RQ.Add(objMst_Inventory);
                        }
                    }
                }
                else
                {
                    Lst_Mst_Inventory_RQ.AddRange(List_Mst_InventoryInOut);
                }
                #endregion
                var Lst_Rpt_Summary_In_Out_PivotUI = new List<Rpt_Summary_In_Out_Pivot>();
                var objRQ_Rpt_Summary_In_Out_Pivot = new RQ_Rpt_Summary_In_Out_Pivot()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserReportState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserReportState.Email),
                    WAUserPassword = "",
                    NetworkID = networkid,
                    OrgID = "",
                    UtcOffset = CUtils.StrValue(UserReportState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = firstDayOfMonth.ToString("yyyy-MM-dd") + " 00:00:00",
                    ApprDTimeUTCTo = lastDayOfMonth.ToString("yyyy-MM-dd") + " 23:59:59",
                    InventoryAction = "OUT",
                    Lst_Mst_Inventory = Lst_Mst_Inventory_RQ
                };
                var objRT_Rpt_Summary_In_Out_Pivot = ReportsService.Instance.WA_Rpt_Summary_In_Out_Pivot(objRQ_Rpt_Summary_In_Out_Pivot, addressAPIs);
                if (objRT_Rpt_Summary_In_Out_Pivot != null && objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot?.Count > 0)
                {
                    var Lst_ProductCode_Distinct = objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot
                        .Select(item => item.ProductCode)
                        .Distinct()
                        .ToList();
                    foreach (var productcode in Lst_ProductCode_Distinct)
                    {
                        var Lst_Rpt_Summary_In_Out_Pivot_ProductCode
                            = objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot?
                            .Where(it => it.ProductCode.Equals(productcode))
                            .ToList();
                        var qtySum = Lst_Rpt_Summary_In_Out_Pivot_ProductCode?
                            .Sum(itQty => Convert.ToDouble(itQty.Qty));
                        var objRpt_Summary_In_Out_PivotUI = new Rpt_Summary_In_Out_Pivot
                        {
                            ProductCode = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductCode,
                            ProductCodeUser = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductCodeUser,
                            ProductName = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].ProductName,
                            UnitCode = Lst_Rpt_Summary_In_Out_Pivot_ProductCode[0].UnitCode,
                            Qty = qtySum,
                        };
                        Lst_Rpt_Summary_In_Out_PivotUI.Add(objRpt_Summary_In_Out_PivotUI);
                    }
                }
                if (!init.Equals("init"))
                {
                    return JsonView("BindData_WG_Rpt_Summary_In_Pivot", Lst_Rpt_Summary_In_Out_PivotUI);
                }
                return View(Lst_Rpt_Summary_In_Out_PivotUI);
            }
            else
            {
                var redirectUrl = Reports_RedirectUrl();
                return Redirect(redirectUrl);
            }
            
        }
        #endregion
    }
}