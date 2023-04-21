using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class ReportController : AdminBaseController
    {
        #region WIDGET

        #region Báo cáo hàng tồn kho
        public ActionResult WG_Rpt_Inv_InventoryBalance(string networkid = "", string lstinvcode = "", string callajax="0")
        {            
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
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
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
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
                foreach(var invcode in list_InvCode)
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
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
            if(callajax == "1")
            {
                return JsonView("BindData_WG_Rpt_Inv_InventoryBalance", List_Rpt_Inv_InventoryBalanceUIBase);
            }
            else
            {
                return View(List_Rpt_Inv_InventoryBalanceUIBase);
            }           
        }
        #endregion

        public ActionResult WG_Rpt_Summary_In_Pivot(string networkid = "", string lstinvcode = "", string init = "init")
        {
            DateTime date = GetDateTimeServerClient(); // Current Month
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var addressAPIs = UserState.AddressAPIs;
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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

        public ActionResult WG_Rpt_Summary_Out_Pivot(string lstinvcode = "", string init = "init")
        {
            DateTime date = GetDateTimeServerClient(); // Current Month
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var addressAPIs = UserState.AddressAPIs;
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
        #endregion

        // GET: Report
        #region ["Báo cáo tồn kho"]
        public ActionResult Rpt_Inv_InventoryBalance()
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion
            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion
            return View(new List<Rpt_Inv_InventoryBalanceUI>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_InvInventoryBalance(string [] invcode = null, string productcode = "", string [] productgrpcode = null, string reportdateutc = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var lstMst_ProductGroup = new List<Mst_ProductGroup>();
            if (productgrpcode != null && productgrpcode.Length > 0)
            {
                foreach (var grpcode in productgrpcode)
                {
                    lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                }
            }
            var lstMst_Inventory = new List<Mst_Inventory>();
            if (invcode != null && invcode.Length > 0)
            {
                foreach (var inv in invcode)
                {
                    lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                }
            }

            if (string.IsNullOrEmpty(reportdateutc))
            {
                reportdateutc = DateTime.Now.ToString(Nonsense.DATE_TIME_DB_FORMAT);
            }

            var objRQ_Rpt_Inv_InventoryBalance = new RQ_Rpt_Inv_InventoryBalance()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                ReportDateUTC = reportdateutc,
                ProductCode = productcode,
                Lst_Mst_Inventory = lstMst_Inventory,
                Lst_Mst_ProductGroup = lstMst_ProductGroup,
                FlagIsDelete = "0",
                Ft_Cols_Upd = null,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance);
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
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                    double totalValInv = 0;
                    qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                    qtyAvailOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyAvailOK));
                    qtyBlockOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBlockOK));
                    totalValInv = listBalance.Sum(it => Convert.ToDouble(it.TotalValInv));
                    var listInvCode = listBalance.Select(inv => inv.InvCode).Distinct().ToList();
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
                        UPInv = listBalance[0].UPInv,
                        TotalValInv = totalValInv,
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
                            var uPInv = Convert.ToDouble(objRpt_Inv_InventoryBalanceUI.UPInv) / ratio;
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
                                //UPInv = objRpt_Inv_InventoryBalanceUI.UPInv,
                                UPInv = uPInv,
                                TotalValInv = objRpt_Inv_InventoryBalanceUI.TotalValInv,
                                ListInvCode = objRpt_Inv_InventoryBalanceUI.ListInvCode,
                            };
                            ListRpt_Inv_InventoryBalanceUIChild.Add(objRpt_Inv_InventoryBalanceUIChild);
                        }
                        objRpt_Inv_InventoryBalanceUI.ListRpt_Inv_InventoryBalanceUIChild = ListRpt_Inv_InventoryBalanceUIChild;
                    }
                    List_Rpt_Inv_InventoryBalanceUIBase.Add(objRpt_Inv_InventoryBalanceUI);
                }
            }

            ViewBag.FlagShowPopup = reportdateutc == DateTime.Now.ToString("yyyy-MM-dd") ? "1" : "";
            return JsonView("BindDataRpt_Inv_InventoryBalance", List_Rpt_Inv_InventoryBalanceUIBase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Inv_InventoryBalance_Export(string productcode = "", string [] invcode = null, string [] productgrpcode = null, string reportdateutc = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion           
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach (var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }
                if (string.IsNullOrEmpty(reportdateutc))
                {
                    reportdateutc = DateTime.Now.ToString(Nonsense.DATE_TIME_DB_FORMAT);
                }

                var List_Rpt_Inv_InventoryBalanceUI = new List<Rpt_Inv_InventoryBalanceUI>();
                var objRQ_Rpt_Inv_InventoryBalance = new RQ_Rpt_Inv_InventoryBalance()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ReportDateUTC = reportdateutc,
                    ProductCode = productcode,
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
                    foreach (var code in ListProductCode)
                    {
                        var listBalance = objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();

                        double qtyTotalOKSum = 0;
                        double qtyAvailOKSum = 0;
                        double qtyBlockOKSum = 0;
                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                        qtyAvailOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyAvailOK));
                        qtyBlockOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBlockOK));
                        var listInvCode = listBalance.Select(inv => inv.InvCode).ToList();

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
                            mp_ValConvert = listBalance[0].mp_ValConvert,
                            QtyTotalOK = qtyTotalOKSum,
                            QtyAvailOK = qtyAvailOKSum,
                            QtyBlockOK = qtyBlockOKSum,
                            ListInvCode = string.Join(",", listInvCode)
                        };
                        List_Rpt_Inv_InventoryBalanceUI.Add(objRpt_Inv_InventoryBalanceUI);
                    }
                }
                if (List_Rpt_Inv_InventoryBalanceUI != null && List_Rpt_Inv_InventoryBalanceUI.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Inv_Inventory_Balance();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Inv_InventoryBalance).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_Inv_InventoryBalanceUI, dicColNames, filePath, string.Format("Rpt_Inv_InventoryBalance"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu thỏa điều kiện xuất excel", CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Inv_Inventory_Balance()
        {
            return new Dictionary<string, string>()
            {
                 {"mp_ProductCodeUser","Mã hàng hoá"},
                 {"mp_ProductName","Tên hàng hoá"},
                 {"mp_UnitCode","Đơn vị"},
                 {"ListInvCode","Kho"},
                 {"QtyTotalOK","SL tổng"},
                 {"QtyAvailOK","SL Available"},
                 {"QtyBlockOK","SL block"},
                 {"TotalValInv ","Giá tồn kho"},
                 {"UPInv","Giá trị"},
            };
        }

        public ActionResult ShowPopupLo(string productcode, string listInvCode, string productname, string productcodeuser, string valconvert)
        {
            #region ["Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };

            ViewBag.ProductName = productname;
            ViewBag.ProductCodeUser = productcodeuser;

            var strWhereInventoryBalanceLot = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", productcode, "=");
            }
            if (!CUtils.IsNullOrEmpty(listInvCode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "InvCode", listInvCode, "IN");
            }
            strWhereInventoryBalanceLot = sbSql.ToString();
            var List_Inv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
            try
            {
                var objRQ_Inv_InventoryBalanceLot = new RQ_Inv_InventoryBalanceLot()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereInventoryBalanceLot,

                    Rt_Cols_Inv_InventoryBalanceLot = "*",

                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(objRQ_Inv_InventoryBalanceLot, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceLot.Count > 0)
                {
                    List_Inv_InventoryBalanceLot.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceLot);
                }
                double curValConvert = 1;
                if (!string.IsNullOrEmpty(valconvert))
                {
                    curValConvert = Convert.ToDouble(valconvert);
                }
                foreach(var lot in List_Inv_InventoryBalanceLot)
                {
                    lot.QtyTotalOK = CUtils.IsNullOrEmpty(lot.QtyTotalOK) ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / curValConvert, 2);
                }

                return JsonView("ShowPopupLo", List_Inv_InventoryBalanceLot);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult ShowPopupSerial(string productcode, string listInvCode, string productname, string productcodeuser)
        {
            #region ["Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };

            ViewBag.ProductName = productname;
            ViewBag.ProductCodeUser = productcodeuser;

            var strWhereInventoryBalanceSerial = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", productcode, "=");
            }
            if (!CUtils.IsNullOrEmpty(listInvCode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "InvCode", listInvCode, "IN");
            }
            strWhereInventoryBalanceSerial = sbSql.ToString();
            var List_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            try
            {
                var objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereInventoryBalanceSerial,

                    Rt_Cols_Inv_InventoryBalanceSerial = "*",

                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceSerial.Count > 0)
                {
                    List_Inv_InventoryBalanceSerial.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceSerial);
                }
                return JsonView("ShowPopupSerial", List_Inv_InventoryBalanceSerial);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion

        #region["Báo cáo tồn kho mở rộng"]
        public ActionResult Rpt_Inv_InventoryBalance_Extend()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE_EXTEND");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion

            #region["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";

            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if(objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion

            return View(new List<Rpt_Inv_InventoryBalance_ExtendUI>());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_InvInventoryBalanceExtend(string[] invcode = null, string productcode = "", string[] productgrpcode = null, string reportdateutc = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var lstMst_ProductGroup = new List<Mst_ProductGroup>();

            if(productgrpcode != null && productgrpcode.Length > 0)
            {
                foreach(var grpcode in productgrpcode)
                {
                    lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                }
            }

            var lstMst_Inventory = new List<Mst_Inventory>();
            if(invcode != null && invcode.Length > 0)
            {
                foreach(var inv in invcode)
                {
                    lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                }
            }

            if (string.IsNullOrEmpty(reportdateutc))
            {
                reportdateutc = DateTime.Now.ToString(Nonsense.DATE_TIME_DB_FORMAT);
            }

            var objRQ_Rpt_Inv_InventoryBalance_Extend = new RQ_Rpt_Inv_InventoryBalance_Extend()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                ReportDateUTC = reportdateutc,
                ProductCode = productcode,
                Lst_Mst_Inventory = lstMst_Inventory,
                Lst_Mst_ProductGroup = lstMst_ProductGroup,
                FlagIsDelete = "0",
                Ft_Cols_Upd = null,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Extend);
            var objRT_Rpt_Inv_InventoryBalance_Extend = Rpt_Inv_InventoryBalance_ExtendService.Instance.WA_Rpt_Inv_InventoryBalance_Extend(objRQ_Rpt_Inv_InventoryBalance_Extend, addressAPIs);
            var List_Rpt_Inv_InventoryBalance_ExtendUIBase = new List<Rpt_Inv_InventoryBalance_ExtendUI>();
            if(objRT_Rpt_Inv_InventoryBalance_Extend != null && objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Count > 0)
            {
                var ListProductCode = objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Select(rt => rt.ProductCode).Distinct().ToList();
                var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
                var ListProductCodeBaseStr = string.Join(",", ListProductCode);

                #region["ProductBase"]
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
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_ProductBalance,

                    Rt_Cols_Mst_Product = "*"
                };

                var objRT_Mst_ProductBalance = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_ProductBalance, addressAPIs);
                if(objRT_Mst_ProductBalance != null && objRT_Mst_ProductBalance.Lst_Mst_Product.Count > 0)
                {
                    List_Mst_Product.AddRange(objRT_Mst_ProductBalance.Lst_Mst_Product);
                }
                #endregion

                foreach(var code in ListProductCode)
                {
                    var listBalance = objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Where(item => item.ProductCode.Equals(code)).ToList();
                    var listProductChild = List_Mst_Product.Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode)).ToList();
                    var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                    double qtyTotalOKSum = 0;
                    double qtyAvailOKSum = 0;
                    double qtyBlockOKSum = 0;
                    double qtyBackOrderSum = 0;
                    double qtyStockExtSum = 0;
                    double qtyMinStSum = 0;
                    double qtyMaxStSum = 0;
                    qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                    qtyAvailOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyAvailOK));
                    qtyBlockOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBlockOK));
                    qtyBackOrderSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBackOrder));
                    qtyStockExtSum = listBalance.Sum(it => Convert.ToDouble(it.QtyStockExt));
                    //qtyMinStSum = listBalance.Sum(it => Convert.ToDouble(it.QtyMinSt));
                    qtyMinStSum = Convert.ToDouble(listBalance[0].QtyMinSt);
                    //qtyMaxStSum = listBalance.Sum(it => Convert.ToDouble(it.QtyMaxSt));
                    qtyMaxStSum = Convert.ToDouble(listBalance[0].QtyMaxSt);
                    var listInvCode = listBalance.Select(inv => inv.InvCode).Distinct().ToList();
                    var listUnitCode = new List<object>();
                    listUnitCode.Add(listBalance[0].mp_UnitCode);
                    listUnitCode.AddRange(listUnitCodeChild);

                    var objRpt_Inv_InventoryBalance_ExtendUI = new Rpt_Inv_InventoryBalance_ExtendUI
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
                        QtyBackOrder = qtyBackOrderSum,
                        QtyStockExt = qtyStockExtSum,
                        QtyMaxSt = qtyMaxStSum,
                        QtyMinSt = qtyMinStSum,
                        ValMixBase = listBalance[0].ValMixBase,
                        ListInvCode = string.Join(",", listInvCode)
                    };

                    if(listProductChild != null && listProductChild.Count > 0)
                    {
                        var List_Rpt_Inv_InventoryBalance_ExtendChild = new List<Rpt_Inv_InventoryBalance_ExtendUI>();
                        foreach(var child in listProductChild)
                        {
                            var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                            var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyTotalOK) * ratio;
                            var qtyAvailOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyAvailOK) * ratio;
                            var qtyBlockOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyBlockOK) * ratio;
                            var qtyBackOrder = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyBackOrder) * ratio;
                            var qtyStockExt = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyStockExt) * ratio;
                            var qtyMinSt = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyMinSt) * ratio;
                            var qtyMaxSt = Convert.ToDouble(objRpt_Inv_InventoryBalance_ExtendUI.QtyMaxSt) * ratio;

                            var objRpt_Inv_InventoryBalance_ExtendUIChild = new Rpt_Inv_InventoryBalance_ExtendUI
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
                                QtyBackOrder = qtyBackOrder,
                                QtyStockExt = qtyStockExt,
                                QtyMinSt = qtyMinSt,
                                QtyMaxSt = qtyMaxSt,
                                ListInvCode = objRpt_Inv_InventoryBalance_ExtendUI.ListInvCode,
                            };
                            List_Rpt_Inv_InventoryBalance_ExtendChild.Add(objRpt_Inv_InventoryBalance_ExtendUIChild);
                        }
                        objRpt_Inv_InventoryBalance_ExtendUI.ListRpt_Inv_InventoryBalanceExtendUIChild = List_Rpt_Inv_InventoryBalance_ExtendChild;
                    }
                    List_Rpt_Inv_InventoryBalance_ExtendUIBase.Add(objRpt_Inv_InventoryBalance_ExtendUI);
                }


            }

            ViewBag.FlagShowPopup = reportdateutc == DateTime.Now.ToString("yyyy-MM-dd") ? "1" : "";
            return JsonView("BindDataRpt_Inv_InventoryBalance_Extend", List_Rpt_Inv_InventoryBalance_ExtendUIBase);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Inv_InventoryBalance_Extend_Export(string productcode = "", string[] invcode = null, string[] productgrpcode = null, string reportdateutc = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var lstMst_ProductGroup = new  List<Mst_ProductGroup>();
                if(productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach(var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }

                var lstMst_Inventory = new List<Mst_Inventory>();

                if(invcode != null && invcode.Length > 0)
                {
                    foreach(var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }

                if (string.IsNullOrEmpty(reportdateutc))
                {
                    reportdateutc = DateTime.Now.ToString(Nonsense.DATE_TIME_DB_FORMAT);
                }

                var List_Rpt_Inv_InventoryBalance_ExtendUI = new List<Rpt_Inv_InventoryBalance_ExtendUI>();
                var objRQ_Rpt_Inv_InventoryBalance_Extend = new RQ_Rpt_Inv_InventoryBalance_Extend()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ReportDateUTC = reportdateutc,
                    ProductCode = productcode,
                    Lst_Mst_Inventory = lstMst_Inventory,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };

                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Extend);
                var objRT_Rpt_Inv_InventoryBalance_Extend = Rpt_Inv_InventoryBalance_ExtendService.Instance.WA_Rpt_Inv_InventoryBalance_Extend(objRQ_Rpt_Inv_InventoryBalance_Extend, addressAPIs);

                var List_Rpt_Inv_InventoryBalance_ExtendUIBase = new List<Rpt_Inv_InventoryBalance_ExtendUI>();

                if(objRT_Rpt_Inv_InventoryBalance_Extend != null && objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend.Select(rt => rt.ProductCode).Distinct().ToList();
                    foreach(var code in ListProductCode)
                    {
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();

                        double qtyTotalOKSum = 0;
                        double qtyAvailOKSum = 0;
                        double qtyBlockOKSum = 0;
                        double qtyBackOrderSum = 0;
                        double qtyStockExtSum = 0;
                        double qtyMinStSum = 0;
                        double qtyMaxStSum = 0;

                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                        qtyAvailOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyAvailOK));
                        qtyBlockOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBlockOK));
                        qtyBackOrderSum = listBalance.Sum(it => Convert.ToDouble(it.QtyBackOrder));
                        qtyStockExtSum = listBalance.Sum(it => Convert.ToDouble(it.QtyStockExt));
                        qtyMinStSum = listBalance.Sum(it => Convert.ToDouble(it.QtyMinSt));
                        qtyMaxStSum = listBalance.Sum(it => Convert.ToDouble(it.QtyMaxSt));


                        var listInvCode = listBalance.Select(inv => inv.InvCode).ToList();


                        var objRpt_Inv_InventoryBalance_ExtendUI = new Rpt_Inv_InventoryBalance_ExtendUI
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
                            mp_ValConvert = listBalance[0].mp_ValConvert,
                            QtyTotalOK = qtyTotalOKSum,
                            QtyAvailOK = qtyAvailOKSum,
                            QtyBlockOK = qtyBlockOKSum,
                            QtyBackOrder = qtyBackOrderSum,
                            QtyStockExt = qtyStockExtSum,
                            QtyMinSt = qtyMinStSum,
                            QtyMaxSt = qtyMaxStSum,
                            ListInvCode = string.Join(",", listInvCode)
                        };
                        List_Rpt_Inv_InventoryBalance_ExtendUI.Add(objRpt_Inv_InventoryBalance_ExtendUI);

                    }
                }

                if(List_Rpt_Inv_InventoryBalance_ExtendUI != null && List_Rpt_Inv_InventoryBalance_ExtendUI.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Inv_Inventory_Balance_Extend();
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Inv_InventoryBalance_Extend).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_Inv_InventoryBalance_ExtendUI, dicColNames, filePath, string.Format("Rpt_Inv_InventoryBalance_Extend"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu thỏa điều kiện xuất excel", CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }



        private Dictionary<string, string> GetExportDicColumsRpt_Inv_Inventory_Balance_Extend()
        {
            return new Dictionary<string, string>()
            {
                 {"mp_ProductCodeUser","Mã hàng hoá"},
                 {"mp_ProductName","Tên hàng hoá"},
                 {"mp_UnitCode","Đơn vị"},
                 {"QtyTotalOK","Tổng tồn kho"},
                 {"QtyBlockOK","SL block"},
                 {"QtyAvailOK","SL Available"},
                 {"ListInvCode","Mã Kho"},
                 {"QtyBackOrder","SL sắp về"},
                 {"QtyStockExt","Tồn kho mở rộng"},
                 {"QtyMinSt","Tồn kho tối thiểu"},
                 {"QtyMaxSt","Tồn kho tối đa" }
                
            };
        }


        #endregion



        #region ["In báo cáo tồn kho"]
        [HttpPost]
        public ActionResult PrintTemp_Rpt_InventoryBalance(string productcode, string [] invcode, string [] productgrpcode, string reportdateutc)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion

                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach (var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }
                if (string.IsNullOrEmpty(reportdateutc))
                {
                    reportdateutc = DateTime.Now.ToString(Nonsense.DATE_TIME_DB_FORMAT);
                }

                var objRQ_Rpt_Inv_InventoryBalance = new RQ_Rpt_Inv_InventoryBalance()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = productcode,
                    Lst_Mst_Inventory = lstMst_Inventory,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    ReportDateUTC = reportdateutc,
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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

                        double QtySum = 0;
                        double TotalValMixBaseSum = 0;
                        double totalValInv = 0;
                        QtySum = listBalance.Sum(it => Convert.ToDouble(it.Qty));
                        TotalValMixBaseSum = listBalance.Sum(it => Convert.ToDouble(it.TotalValMixBase));
                        totalValInv = listBalance.Sum(it => Convert.ToDouble(it.TotalValInv));
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
                            Qty = QtySum,
                            ValMixBase = listBalance[0].ValMixBase,
                            TotalValMixBase = TotalValMixBaseSum,
                            QtyAvailOK = listBalance[0].QtyAvailOK,
                            QtyBlockOK = listBalance[0].QtyBlockOK,
                            QtyTotalOK = listBalance[0].QtyTotalOK,
                            UPInv = listBalance[0].UPInv,
                            //TotalValInv = listBalance[0].TotalValInv,
                            TotalValInv = totalValInv,
                            ListInvCode = string.Join(",", listInvCode)
                        };

                        List_Rpt_Inv_InventoryBalanceUIBase.Add(objRpt_Inv_InventoryBalanceUI);
                    }
                }
                //
                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                Rpt_Inv_InventoryBalancePrint objPrint = new Rpt_Inv_InventoryBalancePrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                var strInvCode = "";
                if(invcode != null && invcode.Length > 0)
                {
                    for(var i = 0; i< invcode.Length; i++)
                    {
                        if(i != 0)
                        {
                            strInvCode += string.Format("{0} ", ",");
                        }
                        strInvCode += string.Format("{0}", CUtils.StrValue(invcode[i]));
                    }
                }
                objPrint.InvName = strInvCode;
                //objPrint.CreateUserName = strNNTFullName;
                objPrint.CreateUserName = CUtils.StrValue(UserState.SysUser.UserName);
                objPrint.LogoFilePath = "";
                objPrint.Lst_Rpt_Inv_InventoryBalanceUI = List_Rpt_Inv_InventoryBalanceUIBase;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("BCTONKHO");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                    }

                    data.data = CreateDataObjectRpt_InventoryBalance_ReportServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                            return Json(new { Success = true, LinkPDF = linkPdf });
                        }
                    }
                }
                else
                {
                    resultEntry.SetFailed();
                    resultEntry.AddMessage("Không có mẫu in");
                    return Json(resultEntry);
                }

                #endregion
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectRpt_InventoryBalance_ReportServer(Rpt_Inv_InventoryBalancePrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.Rpt_Inv_InventoryBalance;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new Rpt_Inv_InventoryBalanceReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvName = CUtils.StrValueNew(objTempPrint.InvName);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<Rpt_Inv_InventoryBalanceDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_Rpt_Inv_InventoryBalanceUI != null && objTempPrint.Lst_Rpt_Inv_InventoryBalanceUI.Count > 0)
            {
                var listDtl_ReportServer = new List<Rpt_Inv_InventoryBalanceDtlReportServer>();
                var idx = 1;
                foreach (var item in objTempPrint.Lst_Rpt_Inv_InventoryBalanceUI)
                {
                    var objDtl_ReportServer = new Rpt_Inv_InventoryBalanceDtlReportServer
                    {
                        Idx = CUtils.StrValue(idx),
                        ProductCodeUser = CUtils.StrValue(item.mp_ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        UnitCode = CUtils.StrValue(item.mp_UnitCode),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, NumericFormat(tableName, "Qty", defaultFormat)),
                        TotalVal = CUtils.StrValueFormatNumber(item.TotalValMixBase, NumericFormat(tableName, "TotalValMixBase", defaultFormat)),
                        Dtl_InvCode = CUtils.StrValue(item.ListInvCode),
                        QtyTotalOK = CUtils.StrValueFormatNumber(item.QtyTotalOK, NumericFormat(tableName, "QtyTotalOK", defaultFormat)),
                        QtyAvailOK = CUtils.StrValueFormatNumber(item.QtyAvailOK, NumericFormat(tableName, "QtyAvailOK", defaultFormat)),
                        QtyBlockOK = CUtils.StrValueFormatNumber(item.QtyBlockOK, NumericFormat(tableName, "QtyBlockOK", defaultFormat)),
                        TotalValInv = CUtils.StrValueFormatNumber(item.TotalValInv, NumericFormat(tableName, "TotalValInv", defaultFormat)),
                        UPInv = CUtils.StrValueFormatNumber(item.UPInv, NumericFormat(tableName, "UPInv", defaultFormat)),
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                    idx++;
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }
        #endregion

        #region ["Thẻ kho"]
        public ActionResult Rpt_InvF_WarehouseCard()
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INVF_WAREHOUSECARD");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion           

            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Inventory);

            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion

            return View(new List<Rpt_InvF_WarehouseCard>());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_InvF_WarehouseCard(string productcodeuser = "", string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string[] invcode = null)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Product Code User"]
            string productCode = "";
            if (!CUtils.IsNullOrEmpty(productcodeuser))
            {
                var strWhere_Mst_ProductUser = "";
                var sbSQL = new StringBuilder();
                sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                strWhere_Mst_ProductUser = sbSQL.ToString();

                var objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_Mst_ProductUser,

                    Rt_Cols_Mst_Product = "*"
                };

                var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                {
                    productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                }
            }
            #endregion

            apprDTimeUTCFrom = string.Format("{0} 00:00:00", apprDTimeUTCFrom);
            apprDTimeUTCTo = string.Format("{0} 23:59:59", apprDTimeUTCTo);
            var lstMst_Inventory = new List<Mst_Inventory>();
            if (invcode != null && invcode.Length > 0)
            {
                foreach (var inv in invcode)
                {
                    lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                }
            }
            //DateTime apprDTimeUTCToInput = DateTime.ParseExact(apprDTimeUTCTo, "HH:mm:ss", CultureInfo.InvariantCulture);
            var objRQ_Rpt_InvF_WarehouseCard = new RQ_Rpt_InvF_WarehouseCard()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                ProductCode = productCode,
                ApprDTimeUTCFrom = apprDTimeUTCFrom.ToString(),
                ApprDTimeUTCTo = apprDTimeUTCTo.ToString(),
                Lst_Mst_Inventory = lstMst_Inventory,
                FlagIsDelete = "0",
                Ft_Cols_Upd = null,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_InvF_WarehouseCard);
            var objRT_Rpt_InvF_WarehouseCard = Rpt_InvF_WarehouseCardService.Instance.WA_Rpt_InvF_WarehouseCard(objRQ_Rpt_InvF_WarehouseCard, addressAPIs);
            
            var List_Rpt_InvF_WarehouseCard = new List<Rpt_InvF_WarehouseCard>();
            if (objRT_Rpt_InvF_WarehouseCard != null && objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard.Count > 0)
            {
                List_Rpt_InvF_WarehouseCard.AddRange(objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard);                
            }
            return JsonView("BindDataRpt_InvF_WarehouseCard", List_Rpt_InvF_WarehouseCard);
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_InvF_WarehouseCard_Export(string productcodeuser = "", string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string invcode = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Product Code User"]
                string productCode = "";
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    var strWhere_Mst_ProductUser = "";
                    var sbSQL = new StringBuilder();
                    sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                    strWhere_Mst_ProductUser = sbSQL.ToString();

                    var objRQ_Mst_Product = new RQ_Mst_Product()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhere_Mst_ProductUser,

                        Rt_Cols_Mst_Product = "*"
                    };

                    var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                    if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                    {
                        productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                    }
                }
                #endregion

                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode.Split(','))
                    {
                        if (!string.IsNullOrEmpty(inv))
                        {
                            lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                        }
                    }
                }

                var List_Rpt_InvF_WarehouseCard = new List<Rpt_InvF_WarehouseCard>();
                var objRQ_Rpt_InvF_WarehouseCard = new RQ_Rpt_InvF_WarehouseCard()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = productCode,
                    ApprDTimeUTCFrom = apprDTimeUTCFrom,
                    ApprDTimeUTCTo = apprDTimeUTCTo,
                    Lst_Mst_Inventory = lstMst_Inventory,
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };
                var objRT_Rpt_InvF_WarehouseCard = Rpt_InvF_WarehouseCardService.Instance.WA_Rpt_InvF_WarehouseCard(objRQ_Rpt_InvF_WarehouseCard, addressAPIs);
                if (objRT_Rpt_InvF_WarehouseCard != null && objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard.Count > 0)
                {
                    List_Rpt_InvF_WarehouseCard.AddRange(objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard);
                }
                if (List_Rpt_InvF_WarehouseCard != null && List_Rpt_InvF_WarehouseCard.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_InvF_WarehouseCard();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_InvF_WarehouseCard).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_InvF_WarehouseCard, dicColNames, filePath, string.Format("Rpt_InvF_WarehouseCard"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_InvF_WarehouseCard()
        {
            return new Dictionary<string, string>()
            {
                 {"DocNo","Số chứng từ"},
                 {"DateDoc","Ngày duyệt"},
                 {"InventoryActionDesc","Diễn giải"},
                 {"InvName", "Kho" },
                 {"QtyTrans","Số lượng"},                 
                 {"TotalQtyInv","Tồn kho"},
                 {"ListInvCode","Vị trí"},
                 {"InvFCFInCode01","Số hợp đồng"},
                 {"InvFCFInCode02","Nhập.Số Container"},
                 {"InvFCFInCode03","Nhập.Biển số xe"},
                 {"InvFCFOutCode01","Code xuất khẩu"},
                 {"InvFCFOutCode02","Xuất.Số Container"},
                 {"InvFCFOutCode03","Xuất.Biển số xe"},
            };
        }


        #endregion

        #region ["In báo cáo thẻ kho"]
        [HttpPost]
        public ActionResult PrintTemp_Rpt_InvF_WarehouseCard(string productcodeuser = "", string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion

                #region ["Product Code User"]
                string productCode = "";
                string productName = "";
                string unitCode = "";
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    var strWhere_Mst_ProductUser = "";
                    var sbSQL = new StringBuilder();
                    sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                    strWhere_Mst_ProductUser = sbSQL.ToString();

                    var objRQ_Mst_Product = new RQ_Mst_Product()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhere_Mst_ProductUser,

                        Rt_Cols_Mst_Product = "*"
                    };

                    var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                    if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                    {
                        productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                        productName = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductName.ToString();
                        unitCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].UnitCode.ToString();
                    }
                }
                #endregion

                apprDTimeUTCFrom = string.Format("{0} 00:00:00", apprDTimeUTCFrom);
                apprDTimeUTCTo = string.Format("{0} 23:59:59", apprDTimeUTCTo);
                //DateTime apprDTimeUTCToInput = DateTime.ParseExact(apprDTimeUTCTo, "HH:mm:ss", CultureInfo.InvariantCulture);
                var objRQ_Rpt_InvF_WarehouseCard = new RQ_Rpt_InvF_WarehouseCard()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = productCode,
                    ApprDTimeUTCFrom = apprDTimeUTCFrom.ToString(),
                    ApprDTimeUTCTo = apprDTimeUTCTo.ToString(),
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };
                var objRT_Rpt_InvF_WarehouseCard = Rpt_InvF_WarehouseCardService.Instance.WA_Rpt_InvF_WarehouseCard(objRQ_Rpt_InvF_WarehouseCard, addressAPIs);
                var x = Newtonsoft.Json.JsonConvert.SerializeObject(objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard);
                var List_Rpt_InvF_WarehouseCardUI = new List<Rpt_InvF_WarehouseCardUI>();
                if (objRT_Rpt_InvF_WarehouseCard != null && objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard.Count > 0)
                {
                    var ListDocNo = new List<string>();
                    foreach (var rptitem in objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard)
                    {
                        if (!ListDocNo.Contains(rptitem.DocNo))
                        {
                            ListDocNo.Add(rptitem.DocNo);
                        }
                    }

                    foreach (var no in ListDocNo)
                    {
                        var listCard = objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard
                            .Where(item => item.DocNo.Equals(no))
                            .ToList();
                        double IN_TotalQtySum = 0;
                        double CUSRETURN_TotalQtySum = 0;
                        double AUDITIN_TotalQtySum = 0;
                        double TotalQtyInvSum = 0;
                        double MOVE_TotalQtySum = 0;
                        double OUT_TotalQtySum = 0;
                        double AUDITOUT_TotalQtySum = 0;
                        double RETURNSUP_TotalQtySum = 0;

                        IN_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.IN_TotalQty));
                        CUSRETURN_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.CUSRETURN_TotalQty));
                        AUDITIN_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.AUDITIN_TotalQty));
                        MOVE_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.MOVE_TotalQty));
                        OUT_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.OUT_TotalQty));
                        RETURNSUP_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.RETURNSUP_TotalQty));
                        AUDITOUT_TotalQtySum = listCard.Sum(it => Convert.ToDouble(it.AUDITOUT_TotalQty));
                        TotalQtyInvSum = listCard.Sum(it => Convert.ToDouble(it.TotalQtyInv));

                        var listInvCode = listCard.Select(inv => inv.InvCodeActual).ToList();

                        var objRpt_InvF_WarehouseCardUI = new Rpt_InvF_WarehouseCardUI
                        {
                            DocNo = listCard[0].DocNo,
                            OrgID = listCard[0].OrgID,
                            InventoryAction = listCard[0].InventoryAction,
                            InventoryActionDesc = listCard[0].InventoryActionDesc,
                            ProductCodeBase = listCard[0].ProductCodeBase,
                            ProductCode = listCard[0].ProductCode,
                            mp_ProductCodeUser = listCard[0].mp_ProductCodeUser,
                            mp_ProductName = listCard[0].mp_ProductName,
                            mp_ProductNameEN = listCard[0].mp_ProductNameEN,
                            DateDoc = listCard[0].DateDoc,
                            IN_TotalQty = IN_TotalQtySum.ToString(),
                            CUSRETURN_TotalQty = CUSRETURN_TotalQtySum.ToString(),
                            AUDITIN_TotalQty = AUDITIN_TotalQtySum.ToString(),
                            MOVE_TotalQty = MOVE_TotalQtySum.ToString(),
                            OUT_TotalQty = OUT_TotalQtySum.ToString(),
                            RETURNSUP_TotalQty = RETURNSUP_TotalQtySum.ToString(),
                            AUDITOUT_TotalQty = AUDITOUT_TotalQtySum.ToString(),
                            TotalQtyInv = TotalQtyInvSum.ToString(),
                            ListInvCodeActual = string.Join(",", listInvCode)
                        };
                        List_Rpt_InvF_WarehouseCardUI.Add(objRpt_InvF_WarehouseCardUI);
                    }
                }
                //
                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                Rpt_InvF_WarehouseCardPrint objPrint = new Rpt_InvF_WarehouseCardPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.UnitCode = unitCode;
                //objPrint.InvName = invcode;
                objPrint.ProductCodeUser = productcodeuser;
                objPrint.ProductName = productName;
                objPrint.CreateUserName = strNNTFullName;
                objPrint.LogoFilePath = "";
                objPrint.Lst_Rpt_InvF_WarehouseCardUI = List_Rpt_InvF_WarehouseCardUI;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("THEKHO");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                    }

                    data.data = CreateDataObjectRpt_InvF_WarehouseCard_ReportServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                            return Json(new { Success = true, LinkPDF = linkPdf });
                        }
                    }
                }
                else
                {
                    resultEntry.SetFailed();
                    resultEntry.AddMessage("Không có mẫu in");
                    return Json(resultEntry);
                }

                #endregion
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectRpt_InvF_WarehouseCard_ReportServer(Rpt_InvF_WarehouseCardPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.Rpt_InvF_WarehouseCard;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new Rpt_Inv_InvF_WarehouseCardReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvName = CUtils.StrValueNew(objTempPrint.InvName);
                MyDynamic.UnitCode = CUtils.StrValueNew(objTempPrint.UnitCode);
                MyDynamic.ProductCodeUser = CUtils.StrValueNew(objTempPrint.ProductCodeUser);
                MyDynamic.ProductName = CUtils.StrValueNew(objTempPrint.ProductName);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<Rpt_Inv_InvF_WarehouseCardDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_Rpt_InvF_WarehouseCardUI != null && objTempPrint.Lst_Rpt_InvF_WarehouseCardUI.Count > 0)
            {
                var listDtl_ReportServer = new List<Rpt_Inv_InvF_WarehouseCardDtlReportServer>();
                var idx = 1;
                foreach (var item in objTempPrint.Lst_Rpt_InvF_WarehouseCardUI)
                {
                    var objDtl_ReportServer = new Rpt_Inv_InvF_WarehouseCardDtlReportServer
                    {
                        Idx = CUtils.StrValue(idx),
                        //UnitCode = CUtils.StrValue(item.UnitCode),
                        DocNo = CUtils.StrValue(item.DocNo),
                        DateDoc = CUtils.StrValue(item.DateDoc),
                        InventoryActionDesc = CUtils.StrValue(item.InventoryActionDesc),
                        IN_TotalQty = CUtils.StrValueFormatNumber(item.IN_TotalQty, NumericFormat(tableName, "IN_TotalQty", defaultFormat)),
                        CUSRETURN_TotalQty = CUtils.StrValueFormatNumber(item.CUSRETURN_TotalQty, NumericFormat(tableName, "CUSRETURN_TotalQty", defaultFormat)),
                        AUDITIN_TotalQty = CUtils.StrValueFormatNumber(item.AUDITIN_TotalQty, NumericFormat(tableName, "AUDITIN_TotalQty", defaultFormat)),
                        MOVE_TotalQty = CUtils.StrValueFormatNumber(item.MOVE_TotalQty, NumericFormat(tableName, "MOVE_TotalQty", defaultFormat)),
                        OUT_TotalQty = CUtils.StrValueFormatNumber(item.OUT_TotalQty, NumericFormat(tableName, "OUT_TotalQty", defaultFormat)),
                        RETURNSUP_TotalQty = CUtils.StrValueFormatNumber(item.RETURNSUP_TotalQty, NumericFormat(tableName, "RETURNSUP_TotalQty", defaultFormat)),
                        AUDITOUT_TotalQty = CUtils.StrValueFormatNumber(item.AUDITOUT_TotalQty, NumericFormat(tableName, "AUDITOUT_TotalQty", defaultFormat)),
                        TotalQtyInv = CUtils.StrValueFormatNumber(item.TotalQtyInv, NumericFormat(tableName, "TotalQtyInv", defaultFormat)),
                        Dtl_InvName = CUtils.StrValue(item.ListInvCodeActual)
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                    idx++;
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }
        #endregion

        #region ["Nhập xuất tồn"]
        public ActionResult Rpt_Inventory_In_Out_Inv()
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INVENTORY_IN_OUT_INV");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion
            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion
            return View(new List<Rpt_Inventory_In_Out_InvUI>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_Inventory_In_Out_Inv(string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string productcodeuser = "", string productgrpcode = "", string invcode = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Product Code User"]
            string productCode = "";
            if (!CUtils.IsNullOrEmpty(productcodeuser))
            {
                var strWhere_Mst_ProductUser = "";
                var sbSQL = new StringBuilder();
                sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                strWhere_Mst_ProductUser = sbSQL.ToString();

                var objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_Mst_ProductUser,

                    Rt_Cols_Mst_Product = "*"
                };

                var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                {
                    productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                }
            }
            #endregion

            var objRQ_Rpt_Inventory_In_Out_Inv = new RQ_Rpt_Inventory_In_Out_Inv()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                ApprDTimeUTCFrom = apprDTimeUTCFrom,
                ApprDTimeUTCTo = apprDTimeUTCTo,
                ProductCode = productCode,
                ProductGrpCode = productgrpcode,
                InvCode = invcode,
                FlagIsDelete = "0",
                Ft_Cols_Upd = null,
            };
            var objRT_Rpt_Inventory_In_Out_Inv = Rpt_Inventory_In_Out_InvService.Instance.WA_Rpt_Inventory_In_Out_Inv(objRQ_Rpt_Inventory_In_Out_Inv, addressAPIs);
            var List_Rpt_Inventory_In_Out_InvUIBase = new List<Rpt_Inventory_In_Out_InvUI>();
            if (objRT_Rpt_Inventory_In_Out_Inv != null && objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Count > 0)
            {
                var ListProductCode = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Select(rt => rt.ProductCode).Distinct().ToList();
                var ListProductCodeBase = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
                var ListProductCodeBaseStr = string.Join(",", ListProductCodeBase);

                #region ["Product Base"]
                var List_Mst_Product = new List<Mst_Product>();
                var strWhere_Mst_Product = "";
                var sbSQL_ProductInv = new StringBuilder();
                sbSQL_ProductInv.AddWhereClause("Mst_Product.ProductCodeBase", ListProductCodeBaseStr, "IN");
                var objRQ_Mst_ProductInv = new RQ_Mst_Product()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_Mst_Product,

                    Rt_Cols_Mst_Product = "*"
                };
                var objRT_Mst_ProductInv = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_ProductInv, addressAPIs);
                if (objRT_Mst_ProductInv != null && objRT_Mst_ProductInv.Lst_Mst_Product.Count > 0)
                {
                    List_Mst_Product.AddRange(objRT_Mst_ProductInv.Lst_Mst_Product);
                }
                #endregion

                foreach (var code in ListProductCode)
                {
                    var listInOutInv = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv
                        .Where(item => item.ProductCode.Equals(code))
                        .ToList();
                    var listProductChild = List_Mst_Product
                        .Where(p => p.ProductCodeBase.Equals(listInOutInv[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listInOutInv[0].ProductCode))
                        .ToList();
                    var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                    double BeginPeriod_Inv_QtyBaseSum = 0; // SL tồn đầu kỳ
                    double BeginPeriod_ValSum = 0; // Giá trị đầu kỳ
                    double InPeriod_In_QtyBaseSum = 0; // SL nhập trong kỳ
                    double InPeriod_In_ValSum = 0; // Giá trị nhập trong kỳ
                    double InPeriod_Out_QtyBaseSum = 0; // SL xuất trong kỳ
                    double InPeriod_Out_ValSum = 0; // Giá trị xuất trong kỳ
                    double EndPeriod_Inv_QtyBaseSum = 0; // SL tồn cuối kỳ
                    double EndPeriod_ValSum = 0; // Giá trị tồn

                    BeginPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Inv_QtyBase));
                    BeginPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Val));
                    InPeriod_In_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_QtyBase));
                    InPeriod_In_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_Val));
                    InPeriod_Out_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_QtyBase));
                    InPeriod_Out_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_Val));
                    EndPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Inv_QtyBase));
                    EndPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Val));

                    var listInvCode = listInOutInv.Select(inv => inv.InvCodeActual).ToList();

                    var listUnitCode = new List<object>(); // Vị trí tồn
                    listUnitCode.Add(listInOutInv[0].mp_UnitCode);
                    listUnitCode.AddRange(listUnitCodeChild);

                    var objRpt_Inventory_In_Out_InvUI = new Rpt_Inventory_In_Out_InvUI
                    {
                        OrgID = listInOutInv[0].OrgID,
                        ProductCode = listInOutInv[0].ProductCode,
                        mp_ProductCodeUser = listInOutInv[0].mp_ProductCodeUser,
                        mp_ProductCodeRoot = listInOutInv[0].mp_ProductCodeRoot,
                        mp_ProductName = listInOutInv[0].mp_ProductName,
                        BeginPeriod_Inv_QtyBase = BeginPeriod_Inv_QtyBaseSum,
                        BeginPeriod_Val = BeginPeriod_ValSum,
                        InPeriod_In_QtyBase = InPeriod_In_QtyBaseSum,
                        InPeriod_In_Val = InPeriod_In_ValSum,
                        InPeriod_Out_QtyBase = InPeriod_Out_QtyBaseSum,
                        InPeriod_Out_Val = InPeriod_Out_ValSum,
                        EndPeriod_Inv_QtyBase = EndPeriod_Inv_QtyBaseSum,
                        EndPeriod_Val = EndPeriod_ValSum,
                        mp_UnitCode = listInOutInv[0].mp_UnitCode,
                        mp_ValConvert = listInOutInv[0].mp_ValConvert,
                        ListInvCode = string.Join(",", listInvCode)
                    };

                    if (listProductChild != null && listProductChild.Count > 0)
                    {
                        var ListRpt_Inventory_In_Out_InvUIChild = new List<Rpt_Inventory_In_Out_InvUI>();
                        foreach (var child in listProductChild)
                        {
                            var ratio = Convert.ToDouble(objRpt_Inventory_In_Out_InvUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                            var BeginPeriod_Inv_QtyBaseChild = Convert.ToDouble(objRpt_Inventory_In_Out_InvUI.BeginPeriod_Inv_QtyBase) * ratio;
                            var InPeriod_In_QtyBaseChild = Convert.ToDouble(objRpt_Inventory_In_Out_InvUI.InPeriod_In_QtyBase) * ratio;
                            var InPeriod_Out_QtyBaseChild = Convert.ToDouble(objRpt_Inventory_In_Out_InvUI.InPeriod_Out_QtyBase) * ratio;
                            var EndPeriod_Inv_QtyBaseChild = Convert.ToDouble(objRpt_Inventory_In_Out_InvUI.EndPeriod_Inv_QtyBase) * ratio;
                            var objRpt_Inventory_In_Out_InvUIChild = new Rpt_Inventory_In_Out_InvUI
                            {
                                ProductCode = child.ProductCode,
                                mp_ProductCodeUser = child.ProductCodeUser,
                                mp_ProductCodeRoot = child.ProductCodeRoot,
                                mp_ProductName = child.ProductName,
                                BeginPeriod_Inv_QtyBase = BeginPeriod_Inv_QtyBaseChild,
                                BeginPeriod_Val = BeginPeriod_ValSum,
                                InPeriod_In_QtyBase = InPeriod_In_QtyBaseChild,
                                InPeriod_In_Val = InPeriod_In_ValSum,
                                InPeriod_Out_QtyBase = InPeriod_Out_QtyBaseChild,
                                InPeriod_Out_Val = InPeriod_Out_ValSum,
                                EndPeriod_Inv_QtyBase = EndPeriod_Inv_QtyBaseChild,
                                EndPeriod_Val = EndPeriod_ValSum,
                                mp_UnitCode = child.UnitCode,
                                mp_ValConvert = child.ValConvert,
                                ListInvCode = objRpt_Inventory_In_Out_InvUI.ListInvCode,
                            };
                            ListRpt_Inventory_In_Out_InvUIChild.Add(objRpt_Inventory_In_Out_InvUIChild);
                        }
                        objRpt_Inventory_In_Out_InvUI.ListRpt_Inventory_In_Out_InvUIChild = ListRpt_Inventory_In_Out_InvUIChild;
                    }
                    List_Rpt_Inventory_In_Out_InvUIBase.Add(objRpt_Inventory_In_Out_InvUI);
                }
            }
            return JsonView("BindDataRpt_Inventory_In_Out_Inv", List_Rpt_Inventory_In_Out_InvUIBase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Inventory_In_Out_Inv_Export(string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string productcodeuser = "", string productgrpcode = "", string invcode = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Product Code User"]
            string productCode = "";
            if (!CUtils.IsNullOrEmpty(productcodeuser))
            {
                var strWhere_Mst_ProductUser = "";
                var sbSQL = new StringBuilder();
                sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                strWhere_Mst_ProductUser = sbSQL.ToString();

                var objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_Mst_ProductUser,

                    Rt_Cols_Mst_Product = "*"
                };

                var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                {
                    productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                }
            }
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var List_Rpt_Inventory_In_Out_InvUI = new List<Rpt_Inventory_In_Out_InvUI>();
                var objRQ_Rpt_Inventory_In_Out_Inv = new RQ_Rpt_Inventory_In_Out_Inv()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = apprDTimeUTCFrom,
                    ApprDTimeUTCTo = apprDTimeUTCTo,
                    ProductCode = productCode,
                    ProductGrpCode = productgrpcode,
                    InvCode = invcode,
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };
                var objRT_Rpt_Inventory_In_Out_Inv = Rpt_Inventory_In_Out_InvService.Instance.WA_Rpt_Inventory_In_Out_Inv(objRQ_Rpt_Inventory_In_Out_Inv, addressAPIs);
                var List_Rpt_Inventory_In_Out_InvUIBase = new List<Rpt_Inventory_In_Out_InvUI>();
                if (objRT_Rpt_Inventory_In_Out_Inv != null && objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeStr = string.Join(",", ListProductCode);

                    foreach (var code in ListProductCode)
                    {
                        var listInOutInv = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();

                        double BeginPeriod_Inv_QtyBaseSum = 0; // SL tồn đầu kỳ
                        double BeginPeriod_ValSum = 0; // Giá trị đầu kỳ
                        double InPeriod_In_QtyBaseSum = 0; // SL nhập trong kỳ
                        double InPeriod_In_ValSum = 0; // Giá trị nhập trong kỳ
                        double InPeriod_Out_QtyBaseSum = 0; // SL xuất trong kỳ
                        double InPeriod_Out_ValSum = 0; // Giá trị xuất trong kỳ
                        double EndPeriod_Inv_QtyBaseSum = 0; // SL tồn cuối kỳ
                        double EndPeriod_ValSum = 0; // Giá trị tồn

                        BeginPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Inv_QtyBase));
                        BeginPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Val));
                        InPeriod_In_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_QtyBase));
                        InPeriod_In_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_Val));
                        InPeriod_Out_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_QtyBase));
                        InPeriod_Out_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_Val));
                        EndPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Inv_QtyBase));
                        EndPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Val));

                        var listInvCode = listInOutInv.Select(inv => inv.InvCodeActual).ToList();

                        var objRpt_Inventory_In_Out_InvUI = new Rpt_Inventory_In_Out_InvUI
                        {
                            OrgID = listInOutInv[0].OrgID,
                            ProductCode = listInOutInv[0].ProductCode,
                            mp_ProductCodeUser = listInOutInv[0].mp_ProductCodeUser,
                            mp_ProductCodeRoot = listInOutInv[0].mp_ProductCodeRoot,
                            mp_ProductName = listInOutInv[0].mp_ProductName,
                            BeginPeriod_Inv_QtyBase = BeginPeriod_Inv_QtyBaseSum,
                            BeginPeriod_Val = BeginPeriod_ValSum,
                            InPeriod_In_QtyBase = InPeriod_In_QtyBaseSum,
                            InPeriod_In_Val = InPeriod_In_ValSum,
                            InPeriod_Out_QtyBase = InPeriod_Out_QtyBaseSum,
                            InPeriod_Out_Val = InPeriod_Out_ValSum,
                            EndPeriod_Inv_QtyBase = EndPeriod_Inv_QtyBaseSum,
                            EndPeriod_Val = EndPeriod_ValSum,
                            mp_UnitCode = listInOutInv[0].mp_UnitCode,
                            mp_ValConvert = listInOutInv[0].mp_ValConvert,
                            ListInvCode = string.Join(",", listInvCode)
                        };
                        List_Rpt_Inventory_In_Out_InvUIBase.Add(objRpt_Inventory_In_Out_InvUI);
                    }
                }
                if (List_Rpt_Inventory_In_Out_InvUIBase != null && List_Rpt_Inventory_In_Out_InvUIBase.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Inventory_In_Out_Inv();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Inventory_In_Out_Inv).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_Inventory_In_Out_InvUIBase, dicColNames, filePath, string.Format("Rpt_Inventory_In_Out_Inv"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Inventory_In_Out_Inv()
        {
            return new Dictionary<string, string>()
            {
                 {"mp_ProductCodeUser","Mã hàng hoá"},
                 {"mp_ProductName","Tên hàng hoá"},
                 {"mp_UnitCode","Đơn vị"},
                 {"BeginPeriod_Inv_QtyBase","Số lượng tồn đầu kỳ"},
                 //{"BeginPeriod_Val","Giá trị đầu kỳ"},
                 {"InPeriod_In_QtyBase","Số lượng nhập trong kỳ"},
                 //{"InPeriod_In_Val","Giá trị nhập trong kỳ"},
                 {"InPeriod_Out_QtyBase","Số lượng xuất trong kỳ"},
                 //{"InPeriod_Out_Val","Giá trị xuất trong kỳ"},
                 {"EndPeriod_Inv_QtyBase","Tồn cuối kỳ"},
                 //{"EndPeriod_Val","Giá trị tồn cuối kỳ"},
                 {"ListInvCode","Vị trí tồn"},
            };
        }
        #endregion

        #region ["In báo cáo nhập xuất tồn"]
        [HttpPost]
        public ActionResult PrintTemp_Rpt_Inventory_In_Out_Inv(string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string productcodeuser = "", string productgrpcode = "", string invcode = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion

                #region ["Product Code User"]
                string productCode = "";
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    var strWhere_Mst_ProductUser = "";
                    var sbSQL = new StringBuilder();
                    sbSQL.AddWhereClause("Mst_Product.ProductCodeUser", productcodeuser, "=");
                    strWhere_Mst_ProductUser = sbSQL.ToString();

                    var objRQ_Mst_Product = new RQ_Mst_Product()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhere_Mst_ProductUser,

                        Rt_Cols_Mst_Product = "*"
                    };

                    var objRT_Mst_ProductUser = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                    if (objRT_Mst_ProductUser != null && objRT_Mst_ProductUser.Lst_Mst_Product.Count > 0)
                    {
                        productCode = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCode.ToString();
                    }
                }
                #endregion

                var objRQ_Rpt_Inventory_In_Out_Inv = new RQ_Rpt_Inventory_In_Out_Inv()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = apprDTimeUTCFrom,
                    ApprDTimeUTCTo = apprDTimeUTCTo,
                    ProductCode = productCode,
                    ProductGrpCode = productgrpcode,
                    InvCode = invcode,
                    FlagIsDelete = "0",
                    Ft_Cols_Upd = null,
                };
                var objRT_Rpt_Inventory_In_Out_Inv = Rpt_Inventory_In_Out_InvService.Instance.WA_Rpt_Inventory_In_Out_Inv(objRQ_Rpt_Inventory_In_Out_Inv, addressAPIs);
                var List_Rpt_Inventory_In_Out_InvUIBase = new List<Rpt_Inventory_In_Out_InvUI>();
                if (objRT_Rpt_Inventory_In_Out_Inv != null && objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv.Select(rt => rt.ProductCode).Distinct().ToList();

                    foreach (var code in ListProductCode)
                    {
                        var listInOutInv = objRT_Rpt_Inventory_In_Out_Inv.Lst_Rpt_Inventory_In_Out_Inv
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();

                        double BeginPeriod_Inv_QtyBaseSum = 0; // SL tồn đầu kỳ
                        double BeginPeriod_ValSum = 0; // Giá trị đầu kỳ
                        double InPeriod_In_QtyBaseSum = 0; // SL nhập trong kỳ
                        double InPeriod_In_ValSum = 0; // Giá trị nhập trong kỳ
                        double InPeriod_Out_QtyBaseSum = 0; // SL xuất trong kỳ
                        double InPeriod_Out_ValSum = 0; // Giá trị xuất trong kỳ
                        double EndPeriod_Inv_QtyBaseSum = 0; // SL tồn cuối kỳ
                        double EndPeriod_ValSum = 0; // Giá trị tồn

                        BeginPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Inv_QtyBase));
                        BeginPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.BeginPeriod_Val));
                        InPeriod_In_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_QtyBase));
                        InPeriod_In_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_In_Val));
                        InPeriod_Out_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_QtyBase));
                        InPeriod_Out_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.InPeriod_Out_Val));
                        EndPeriod_Inv_QtyBaseSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Inv_QtyBase));
                        EndPeriod_ValSum = listInOutInv.Sum(it => Convert.ToDouble(it.EndPeriod_Val));

                        var listInvCode = listInOutInv.Select(inv => inv.InvCodeActual).ToList();

                        var listUnitCode = new List<object>(); // Vị trí tồn
                        listUnitCode.Add(listInOutInv[0].mp_UnitCode);

                        var objRpt_Inventory_In_Out_InvUI = new Rpt_Inventory_In_Out_InvUI
                        {
                            OrgID = listInOutInv[0].OrgID,
                            ProductCode = listInOutInv[0].ProductCode,
                            mp_ProductCodeUser = listInOutInv[0].mp_ProductCodeUser,
                            mp_ProductCodeRoot = listInOutInv[0].mp_ProductCodeRoot,
                            mp_ProductName = listInOutInv[0].mp_ProductName,
                            BeginPeriod_Inv_QtyBase = BeginPeriod_Inv_QtyBaseSum,
                            BeginPeriod_Val = BeginPeriod_ValSum,
                            InPeriod_In_QtyBase = InPeriod_In_QtyBaseSum,
                            InPeriod_In_Val = InPeriod_In_ValSum,
                            InPeriod_Out_QtyBase = InPeriod_Out_QtyBaseSum,
                            InPeriod_Out_Val = InPeriod_Out_ValSum,
                            EndPeriod_Inv_QtyBase = EndPeriod_Inv_QtyBaseSum,
                            EndPeriod_Val = EndPeriod_ValSum,
                            mp_UnitCode = listInOutInv[0].mp_UnitCode,
                            mp_ValConvert = listInOutInv[0].mp_ValConvert,
                            ListInvCode = string.Join(",", listInvCode)
                        };
                        List_Rpt_Inventory_In_Out_InvUIBase.Add(objRpt_Inventory_In_Out_InvUI);
                    }
                }
                //
                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                Rpt_Inventory_In_Out_InvPrint objPrint = new Rpt_Inventory_In_Out_InvPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.InvName = invcode;
                objPrint.CreateUserName = strNNTFullName;
                objPrint.LogoFilePath = "";
                objPrint.Lst_Rpt_Inventory_In_Out_InvUI = List_Rpt_Inventory_In_Out_InvUIBase;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("BCXNT");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                    }

                    data.data = CreateDataObjectRpt_Inventory_In_Out_Inv_ReportServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                            return Json(new { Success = true, LinkPDF = linkPdf });
                        }
                    }
                }
                else
                {
                    resultEntry.SetFailed();
                    resultEntry.AddMessage("Không có mẫu in");
                    return Json(resultEntry);
                }

                #endregion
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectRpt_Inventory_In_Out_Inv_ReportServer(Rpt_Inventory_In_Out_InvPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.Rpt_Inventory_In_Out_Inv;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);

            var MyDynamic = new Rpt_Inventory_In_Out_InvReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvName = CUtils.StrValueNew(objTempPrint.InvName);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<Rpt_Inventory_In_Out_InvDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_Rpt_Inventory_In_Out_InvUI != null && objTempPrint.Lst_Rpt_Inventory_In_Out_InvUI.Count > 0)
            {
                var listDtl_ReportServer = new List<Rpt_Inventory_In_Out_InvDtlReportServer>();
                var idx = 1;
                foreach (var item in objTempPrint.Lst_Rpt_Inventory_In_Out_InvUI)
                {
                    var objDtl_ReportServer = new Rpt_Inventory_In_Out_InvDtlReportServer
                    {
                        Idx = CUtils.StrValue(idx),
                        UnitCode = CUtils.StrValue(item.mp_UnitCode),
                        ProductCodeUser = CUtils.StrValue(item.mp_ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        BeginPeriod_Inv_QtyBase = CUtils.StrValueFormatNumber(item.BeginPeriod_Inv_QtyBase, NumericFormat(tableName, "BeginPeriod_Inv_QtyBase", defaultFormat)),
                        BeginPeriod_Val = CUtils.StrValueFormatNumber(item.BeginPeriod_Val, NumericFormat(tableName, "BeginPeriod_Val", defaultFormat)),
                        InPeriod_In_QtyBase = CUtils.StrValueFormatNumber(item.InPeriod_In_QtyBase, NumericFormat(tableName, "InPeriod_In_QtyBase", defaultFormat)),
                        InPeriod_In_Val = CUtils.StrValueFormatNumber(item.InPeriod_In_Val, NumericFormat(tableName, "InPeriod_In_Val", defaultFormat)),
                        InPeriod_Out_QtyBase = CUtils.StrValueFormatNumber(item.InPeriod_Out_QtyBase, NumericFormat(tableName, "InPeriod_Out_QtyBase", defaultFormat)),
                        InPeriod_Out_Val = CUtils.StrValueFormatNumber(item.InPeriod_Out_Val, NumericFormat(tableName, "InPeriod_Out_Val", defaultFormat)),
                        EndPeriod_Inv_QtyBase = CUtils.StrValueFormatNumber(item.EndPeriod_Inv_QtyBase, NumericFormat(tableName, "EndPeriod_Inv_QtyBase", defaultFormat)),
                        EndPeriod_Val = CUtils.StrValueFormatNumber(item.EndPeriod_Val, NumericFormat(tableName, "EndPeriod_Val", defaultFormat)),
                        Dtl_InvName = CUtils.StrValue(item.ListInvCode),
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                    idx++;
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }
        #endregion

        #region ["Báo cáo nhập xuất theo kỳ"]

        [HttpGet]
        public ActionResult Rpt_Summary_In_Out(string yearrpt = "", string productcodeuser = "", string productname = "", string unitcode = "", string init = "init")
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_IN_OUT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var list = new List<Rpt_Summary_In_Out>();
            var strYearRpt = "";
            if (!CUtils.IsNullOrEmpty(yearrpt))
            {
                strYearRpt = yearrpt + "-01-01";
            }

            #region ["Search"]

            if (init != "init")
            {
                var objRQ_Rpt_Summary_In_Out = new RQ_Rpt_Summary_In_Out()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    YearRpt = strYearRpt,
                    ProductCodeUser = productcodeuser,
                };
                var objRT_Rpt_Summary_In_Out = ReportsService.Instance.WA_Rpt_Summary_In_Out(objRQ_Rpt_Summary_In_Out, addressAPIs);

                if (objRT_Rpt_Summary_In_Out != null && objRT_Rpt_Summary_In_Out.Lst_Rpt_Summary_In_Out.Count > 0)
                {
                    list = objRT_Rpt_Summary_In_Out.Lst_Rpt_Summary_In_Out;
                }
            }
            else
            {
                var yearCur = DateTime.Now.ToString("yyyy");
                yearrpt = yearCur;
            }
            #endregion

            ViewBag.yearrpt = yearrpt;
            ViewBag.productcodeuser = productcodeuser;
            ViewBag.productname = productname;
            ViewBag.unitcode = unitcode;
            ViewBag.ListData = list;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Summary_In_Out_Export(string yearrpt = "", string productcodeuser = "")
        {
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var strYearRpt = "";
                if (!CUtils.IsNullOrEmpty(yearrpt))
                {
                    strYearRpt = yearrpt + "-01-01";
                }

                var list = new List<Rpt_Summary_In_Out>();
                var objRQ_Rpt_Summary_In_Out = new RQ_Rpt_Summary_In_Out()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    YearRpt = strYearRpt,
                    ProductCodeUser = productcodeuser,
                };
                var objRT_Rpt_Summary_In_Out = ReportsService.Instance.WA_Rpt_Summary_In_Out(objRQ_Rpt_Summary_In_Out, addressAPIs);

                if (objRT_Rpt_Summary_In_Out != null && objRT_Rpt_Summary_In_Out.Lst_Rpt_Summary_In_Out.Count > 0)
                {
                    list = objRT_Rpt_Summary_In_Out.Lst_Rpt_Summary_In_Out;
                }

                if (list != null && list.Count > 0)
                {
                    foreach (var it in list)
                    {
                        if (CUtils.StrValue(it.ActionRpt) == "IN")
                        {
                            it.ActionRpt = "Nhập";
                        }
                        else
                        {
                            it.ActionRpt = "Xuất";
                        }
                    }

                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Summary_In_Out();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Summary_In_Out).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Rpt_Summary_In_Out"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Summary_In_Out()
        {
            return new Dictionary<string, string>()
            {
                 {"ActionRpt","Loại"},
                 {"TotalQtyT1","T1"},
                 {"TotalQtyT2","T2"},
                 {"TotalQtyT3","T3"},
                 {"TotalQtyT4","T4"},
                 {"TotalQtyT5","T5"},
                 {"TotalQtyT6","T6"},
                 {"TotalQtyT7","T7"},
                 {"TotalQtyT8","T8"},
                 {"TotalQtyT9","T9"},
                 {"TotalQtyT10","T10"},
                 {"TotalQtyT11","T11"},
                 {"TotalQtyT12","T12"},
            };
        }
        #endregion

        #region ["Báo cáo tồn kho theo kỳ"]

        [HttpGet]
        public ActionResult Rpt_Summary_QtyInvByPeriod(string yearrpt = "", string productcodeuser = "", string productname = "", string unitcode = "", string init = "init")
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_QTYINVBYPERIOD");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var list = new List<Rpt_Summary_QtyInvByPeriod>();
            var strYearRpt = "";
            if (!CUtils.IsNullOrEmpty(yearrpt))
            {
                strYearRpt = yearrpt + "-01-01";
            }

            #region ["Search"]

            if (init != "init")
            {
                var objRQ_Rpt_Summary_QtyInvByPeriod = new RQ_Rpt_Summary_QtyInvByPeriod()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    YearRpt = strYearRpt,
                    ProductCodeUser = productcodeuser,
                };
                var objRT_Rpt_Summary_QtyInvByPeriod = ReportsService.Instance.WA_Rpt_Summary_QtyInvByPeriod(objRQ_Rpt_Summary_QtyInvByPeriod, addressAPIs);

                if (objRT_Rpt_Summary_QtyInvByPeriod != null && objRT_Rpt_Summary_QtyInvByPeriod.Lst_Rpt_Summary_QtyInvByPeriod.Count > 0)
                {
                    list = objRT_Rpt_Summary_QtyInvByPeriod.Lst_Rpt_Summary_QtyInvByPeriod;
                }
            }
            else
            {
                var yearCur = DateTime.Now.ToString("yyyy");
                yearrpt = yearCur;
            }
            #endregion

            ViewBag.yearrpt = yearrpt;
            ViewBag.productcodeuser = productcodeuser;
            ViewBag.productname = productname;
            ViewBag.unitcode = unitcode;
            ViewBag.ListData = list;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Summary_QtyInvByPeriod_Export(string yearrpt = "", string productcodeuser = "")
        {
            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var strYearRpt = "";
                if (!CUtils.IsNullOrEmpty(yearrpt))
                {
                    strYearRpt = yearrpt + "-01-01";
                }

                var list = new List<Rpt_Summary_QtyInvByPeriod>();
                var objRQ_Rpt_Summary_QtyInvByPeriod = new RQ_Rpt_Summary_QtyInvByPeriod()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    YearRpt = strYearRpt,
                    ProductCodeUser = productcodeuser,
                };
                var objRT_Rpt_Summary_QtyInvByPeriod = ReportsService.Instance.WA_Rpt_Summary_QtyInvByPeriod(objRQ_Rpt_Summary_QtyInvByPeriod, addressAPIs);

                if (objRT_Rpt_Summary_QtyInvByPeriod != null && objRT_Rpt_Summary_QtyInvByPeriod.Lst_Rpt_Summary_QtyInvByPeriod.Count > 0)
                {
                    list = objRT_Rpt_Summary_QtyInvByPeriod.Lst_Rpt_Summary_QtyInvByPeriod;
                }

                if (list != null && list.Count > 0)
                {
                    foreach (var it in list)
                    {
                        it.OrgID = "Tồn kho";
                    }

                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Summary_QtyInvByPeriod();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Summary_QtyInvByPeriod).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Rpt_Summary_QtyInvByPeriod"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Summary_QtyInvByPeriod()
        {
            return new Dictionary<string, string>()
            {
                {"OrgID","Thông tin"},
                 {"QtyTotalInv_T1","T1"},
                 {"QtyTotalInv_T2","T2"},
                 {"QtyTotalInv_T3","T3"},
                 {"QtyTotalInv_T4","T4"},
                 {"QtyTotalInv_T5","T5"},
                 {"QtyTotalInv_T6","T6"},
                 {"QtyTotalInv_T7","T7"},
                 {"QtyTotalInv_T8","T8"},
                 {"QtyTotalInv_T9","T9"},
                 {"QtyTotalInv_T10","T10"},
                 {"QtyTotalInv_T11","T11"},
                 {"QtyTotalInv_T12","T12"},
            };
        }
        #endregion

        #region ["Báo nhập kho theo NCC"]

        [HttpGet]
        public ActionResult Rpt_Summary_InAndReturnSup(string apprdtimeutcfrom = "", string apprdtimeutcto = "", string productcodeuser = "", string productname = "", string unitcode = "", string CustomerCode = "", string init = "init")
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_INANDRETURNSUP");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var list = new List<Rpt_Summary_InAndReturnSupUI>();
            var strapprdtimeutcfrom = "";
            if (!CUtils.IsNullOrEmpty(apprdtimeutcfrom))
            {
                strapprdtimeutcfrom = apprdtimeutcfrom + " 00:00:00";
            }
            var strapprdtimeutcto = "";
            if (!CUtils.IsNullOrEmpty(apprdtimeutcto))
            {
                strapprdtimeutcto = apprdtimeutcto + " 23:59:59";
            }

            #region["Danh sách NCC"]
            var lstCustomer = new List<Mst_Customer>();
            var objRQ_Mst_Customer = new RQ_Mst_Customer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                Rt_Cols_Mst_Customer = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'"
            };
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
            {
                lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
            }
            ViewBag.Lst_MstCustomer = lstCustomer;
            #endregion

            #region ["Search"]

            if (init != "init")
            {
                var objRQ_Rpt_Summary_InAndReturnSup = new RQ_Rpt_Summary_InAndReturnSup()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = strapprdtimeutcfrom,
                    ApprDTimeUTCTo = !string.IsNullOrEmpty(strapprdtimeutcto) ? strapprdtimeutcto : "2100-01-01 23:59:59",
                    ProductCodeUser = productcodeuser,
                    CustomerCodeSys = CustomerCode
                };
                var objRT_Rpt_Summary_InAndReturnSup = ReportsService.Instance.WA_Rpt_Summary_InAndReturnSup(objRQ_Rpt_Summary_InAndReturnSup, addressAPIs);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Summary_InAndReturnSup);
                if (objRT_Rpt_Summary_InAndReturnSup != null && objRT_Rpt_Summary_InAndReturnSup.Lst_Rpt_Summary_InAndReturnSup.Count > 0)
                {
                    var listRpt = objRT_Rpt_Summary_InAndReturnSup.Lst_Rpt_Summary_InAndReturnSup;

                    double total = listRpt.Sum(m => Convert.ToDouble(m.TotalQtyRemain));
                    foreach (var it in listRpt)
                    {
                        var itUI = new Rpt_Summary_InAndReturnSupUI();
                        itUI.OrgID = it.OrgID;
                        itUI.ProductCode = it.ProductCode;
                        itUI.ProductCodeUser = it.ProductCodeUser;
                        itUI.CustomerCode = it.CustomerCode;
                        itUI.CustomerCodeSys = it.CustomerCodeSys;
                        itUI.CustomerName = it.CustomerName;
                        itUI.TotalQtyIn = it.TotalQtyIn;
                        itUI.TotalQtyReturn = it.TotalQtyReturn;
                        itUI.TotalQtyRemain = it.TotalQtyRemain;
                        itUI.Percent = Math.Round(Convert.ToDouble(it.TotalQtyRemain) / total * 100, 2);

                        list.Add(itUI);
                    }
                }

                ViewBag.CustomerCode = CustomerCode;



            }
            else
            {
                var dateSearch = CUtils.GetDateToSearch(DateTime.Now);
                apprdtimeutcfrom = dateSearch;
            }
            #endregion

            ViewBag.apprdtimeutcfrom = apprdtimeutcfrom;
            ViewBag.apprdtimeutcto = apprdtimeutcto;
            ViewBag.productcodeuser = productcodeuser;
            ViewBag.productname = productname;
            ViewBag.unitcode = unitcode;
            ViewBag.ListData = list;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rpt_Summary_InAndReturnSup_Export(string apprdtimeutcfrom = "", string apprdtimeutcto = "", string productcodeuser = "", string customercodesys = "")
        {
            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var strapprdtimeutcfrom = "";
                if (!CUtils.IsNullOrEmpty(apprdtimeutcfrom))
                {
                    strapprdtimeutcfrom = apprdtimeutcfrom + " 00:00:00";
                }
                var strapprdtimeutcto = "";
                if (!CUtils.IsNullOrEmpty(apprdtimeutcto))
                {
                    strapprdtimeutcto = apprdtimeutcto + " 23:59:59";
                }
                else
                {
                    strapprdtimeutcto = "2100-01-01 23:59:59";
                }

                var list = new List<Rpt_Summary_InAndReturnSup>();
                var objRQ_Rpt_Summary_InAndReturnSup = new RQ_Rpt_Summary_InAndReturnSup()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = strapprdtimeutcfrom,
                    ApprDTimeUTCTo = strapprdtimeutcto,
                    ProductCodeUser = productcodeuser,
                    CustomerCodeSys = customercodesys,
                };
                var objRT_Rpt_Summary_InAndReturnSup = ReportsService.Instance.WA_Rpt_Summary_InAndReturnSup(objRQ_Rpt_Summary_InAndReturnSup, addressAPIs);

                if (objRT_Rpt_Summary_InAndReturnSup != null && objRT_Rpt_Summary_InAndReturnSup.Lst_Rpt_Summary_InAndReturnSup.Count > 0)
                {
                    list = objRT_Rpt_Summary_InAndReturnSup.Lst_Rpt_Summary_InAndReturnSup;
                }

                if (list != null && list.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Summary_InAndReturnSup();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Summary_InAndReturnSup).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Rpt_Summary_InAndReturnSup"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Summary_InAndReturnSup()
        {
            return new Dictionary<string, string>()
            {
                {"CustomerName","Tên nhà cung cấp"},
                {"TotalQtyRemain","Tổng nhập"}
            };
        }
        #endregion

        #region[Lấy sản phẩm khi autocomplete]
        [HttpPost]
        public ActionResult GetProductExactly(string prdid = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var objMst_Product = new Mst_Product()
            {
                ProductCode = prdid
            };
            var strWhere_GetProductId = strWhereGetProductId(objMst_Product);
            var objRQ_Mst_Product = new RQ_Mst_Product()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhere_GetProductId,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
            };
            var objRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);

            var objMst_ProductBase = objRT_Mst_Product[0];

            return Json(new { Data = objMst_ProductBase });
        }
        [HttpPost]
        public ActionResult SearchProduct(string prdid = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            var listMst_Product = new List<Mst_Product>();
            try
            {
                var objMst_Product = new Mst_Product()
                {
                    ProductCodeUser = prdid,
                    ProductName = prdid,
                    FlagActive = "1"
                };
                var strWhere_SearchProductProductId = strWhereSearchProductProductId(objMst_Product);
                var objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = "10",
                    Ft_WhereClause = strWhere_SearchProductProductId,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                };

                var objRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);
                listMst_Product.AddRange(objRT_Mst_Product);
                return Json(listMst_Product);
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region WhereClause

        private string strWhereGetProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");

            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValueOrNull(data.ProductCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereSearchProductProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");

            strWhereClause = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                //strWhereClause += " and (" + Tbl_Mst_Product + TblMst_Product.ProductCodeUser + " like '%" + CUtils.StrValueOrNull(data.ProductCodeUser) + "%'";
                strWhereClause += "(" + Tbl_Mst_Product + TblMst_Product.ProductCodeUser + " like '%" + CUtils.StrValueOrNull(data.ProductCodeUser) + "%'";
            }

            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                strWhereClause += " or " + "Mst_Product.ProductName like '%" + data.ProductName + "%')";
            }

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                //strWhereClause.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, data.FlagActive, "=");
                strWhereClause += " and " + "Mst_Product.FlagActive = " + data.FlagActive ;
            }
            return strWhereClause;
        }
        #endregion
        #endregion

        #region ["Báo cáo tổng hợp nhập"]
        public ActionResult Rpt_Summary_In_Pivot()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_IN_PIVOT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion
            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_Summary_In_Pivot(string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string productcodeuser = "", string[] productgrpcode = null, string[] invcode = null)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            try
            {
                var lstMst_Product = new List<Mst_Product>();
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    lstMst_Product.Add(new Mst_Product { ProductCodeUser = productcodeuser });
                }
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach (var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }

                var objRQ_Rpt_Summary_In_Out_Pivot = new RQ_Rpt_Summary_In_Out_Pivot()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = !string.IsNullOrEmpty(apprDTimeUTCFrom) ? apprDTimeUTCFrom + " 00:00:00" : apprDTimeUTCFrom,
                    ApprDTimeUTCTo = !string.IsNullOrEmpty(apprDTimeUTCTo) ? apprDTimeUTCTo + " 23:59:59" : apprDTimeUTCTo,
                    InventoryAction = "IN",
                    Lst_Mst_Product = lstMst_Product,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    Lst_Mst_Inventory = lstMst_Inventory
                };
                var objRT_Rpt_Summary_In_Out_Pivot = ReportsService.Instance.WA_Rpt_Summary_In_Out_Pivot(objRQ_Rpt_Summary_In_Out_Pivot, addressAPIs);

                return Json(new { Success = true, Data = objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #endregion

        #region ["Báo cáo lịch sử giao dịch nhập xuất theo đối tác"]
        public ActionResult Rpt_Summary_In_Out_Sup_Pivot()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_IN_OUT_SUP_PIVOT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion
            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion

            #region ["Danh sách đối tác"]
            
            var List_Mst_Customer = new List<Mst_Customer>();
            var strWhere_Mst_Customer = "Mst_Customer.FlagActive = '1'";
            var objRQ_Mst_Customer = new RQ_Mst_Customer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Customer,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Customer = "*"
            };
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
            {
                List_Mst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
            }
            ViewBag.Lst_Mst_Customer = List_Mst_Customer;
            #endregion

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_Summary_In_Out_Sup_Pivot(string apprDTimeUTCFrom = "", string apprDTimeUTCTo = "", string productcodeuser = "", string[] productgrpcode = null, string[] invcode = null, string[] customercodesys = null)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_SUMMARY_IN_OUT_SUP_PIVOT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            try
            {
                var lstMst_Product = new List<Mst_Product>();
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    lstMst_Product.Add(new Mst_Product { ProductCodeUser = productcodeuser });
                }
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach (var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }
                var lstMst_Customer = new List<Mst_Customer>();
                if (customercodesys != null && customercodesys.Length > 0)
                {
                    foreach (var cuscode in customercodesys)
                    {
                        lstMst_Customer.Add(new Mst_Customer { CustomerCodeSys = cuscode });
                    }
                }

                var objRQ_Rpt_Summary_In_Out_Sup_Pivot = new RQ_Rpt_Summary_In_Out_Sup_Pivot()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ApprDTimeUTCFrom = !string.IsNullOrEmpty(apprDTimeUTCFrom) ? apprDTimeUTCFrom + " 00:00:00" : apprDTimeUTCFrom,
                    ApprDTimeUTCTo = !string.IsNullOrEmpty(apprDTimeUTCTo) ? apprDTimeUTCTo + " 23:59:59" : apprDTimeUTCTo,
                    Lst_Mst_Product = lstMst_Product,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    Lst_Mst_Inventory = lstMst_Inventory,
                    Lst_Mst_Customer = lstMst_Customer
                };
                var objRT_Rpt_Summary_In_Out_Sup_Pivot = ReportsService.Instance.WA_Rpt_Summary_In_Out_Sup_Pivot(objRQ_Rpt_Summary_In_Out_Sup_Pivot, addressAPIs);

                return Json(new { Success = true, Data = objRT_Rpt_Summary_In_Out_Sup_Pivot.Lst_Rpt_Summary_In_Out_Sup_Pivot }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #endregion

        #region ["Báo cáo hạn sử dụng hàng hóa"]
        public ActionResult Rpt_InvBalLot_MaxExpiredDateByInv()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INVBALLOT_MAXEXPIREDDATEBYINV");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            #region ["Danh sách kho"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagIn_Out = '1'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryInOut.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            #endregion
            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_InvBalLot_MaxExpiredDateByInv(string productcodeuser = "", string[] productgrpcode = null, string[] invcode = null)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INVBALLOT_MAXEXPIREDDATEBYINV");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            try
            {
                var lstMst_Product = new List<Mst_Product>();
                if (!CUtils.IsNullOrEmpty(productcodeuser))
                {
                    lstMst_Product.Add(new Mst_Product { ProductCodeUser = productcodeuser });
                }
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (productgrpcode != null && productgrpcode.Length > 0)
                {
                    foreach (var grpcode in productgrpcode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
                var lstMst_Inventory = new List<Mst_Inventory>();
                if (invcode != null && invcode.Length > 0)
                {
                    foreach (var inv in invcode)
                    {
                        lstMst_Inventory.Add(new Mst_Inventory { InvCode = inv });
                    }
                }

                var objRQ_Rpt_InvBalLot_MaxExpiredDateByInv = new RQ_Rpt_InvBalLot_MaxExpiredDateByInv()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Lst_Mst_Product = lstMst_Product,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup,
                    Lst_Mst_Inventory = lstMst_Inventory
                };
                var objRT_Rpt_InvBalLot_MaxExpiredDateByInv = ReportsService.Instance.WA_Rpt_InvBalLot_MaxExpiredDateByInv(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv, addressAPIs);

                return Json(new { Success = true, Data = objRT_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Rpt_InvBalLot_MaxExpiredDateByInv }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #endregion

        #region ["2021-05-22: Báo cáo bản đồ lệnh giao hàng"]
        [HttpGet]
        public ActionResult Rpt_MapDeliveryOrder_ByInvFIOut()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_MAPDELIVERYORDER_BYINVFIOUT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = this.UserState;
            #endregion

            #region ["Danh sách ngày"]
            //Ngày từ init = today-7
            //Ngày đến init = today+7
            var today = CUtils.ConvertToDateTime(CUtils.ConvertingUTCToLocalTime(Today, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_DB_FORMAT));
            var datefrom = new DateTime();
            var dateto = new DateTime();

            datefrom = CUtils.ConvertToDateTime(CUtils.StrValue(today.AddDays(-7)));
            dateto = CUtils.ConvertToDateTime(CUtils.StrValue(today.AddDays(7)));

            //Danh sách ngày gen ra từ ngày từ đến
            var listDates = Enumerable.Range(0, (dateto - datefrom).Days + 1).Select(day => datefrom.AddDays(day)).ToList();

            ViewBag.ListDates = listDates;
            ViewBag.DateFrom = CUtils.StrValue(CUtils.FormatDate(datefrom,Nonsense.DATE_TIME_DB_FORMAT));
            ViewBag.DateTo = CUtils.StrValue(CUtils.FormatDate(dateto, Nonsense.DATE_TIME_DB_FORMAT));
            ViewBag.Today = CUtils.StrValue(CUtils.FormatDate(today, Nonsense.DATE_TIME_DB_FORMAT));
            #endregion

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch_Rpt_MapDeliveryOrder_ByInvFIOut(string datefrom, string dateto)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_MAPDELIVERYORDER_BYINVFIOUT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = this.UserState;
            #endregion

            try
            {
                var strDateFromCur = "";
                var strDateToCur = "";
                var listData = new List<Rpt_MapDeliveryOrder_ByInvFIOut>();
                var listDataUI = new List<Rpt_MapDeliveryOrder_ByInvFIOutUI>();

                #region ["Danh sách ngày"]
                var today = CUtils.ConvertToDateTime(CUtils.ConvertingUTCToLocalTime(Today, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_DB_FORMAT));
                //Danh sách ngày gen ra từ ngày từ đến
                var strDateTo = CUtils.ConvertToDateTime(dateto);
                var strDateFrom = CUtils.ConvertToDateTime(datefrom);

                var listDates = Enumerable.Range(0, (strDateTo - strDateFrom).Days + 1).Select(day => strDateFrom.AddDays(day)).ToList();

                ViewBag.ListDates = listDates;
                ViewBag.Today = CUtils.StrValue(CUtils.FormatDate(today, Nonsense.DATE_TIME_DB_FORMAT));
                #endregion

                if (!CUtils.IsNullOrEmpty(datefrom))
                {
                    var datefromCur = CUtils.StrValue(datefrom) + " 00:00:00";
                    strDateFromCur = CUtils.ConvertingLocalTimeToUTC(datefromCur, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);                    
                }
                if (!CUtils.IsNullOrEmpty(dateto))
                {
                    var datetoCur = CUtils.StrValue(dateto) + " 23:59:59";
                    strDateToCur = CUtils.ConvertingLocalTimeToUTC(datetoCur, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);                    
                }

                var objRQ_Rpt_MapDeliveryOrder_ByInvFIOut = new RQ_Rpt_MapDeliveryOrder_ByInvFIOut()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    DateFrom = CUtils.StrValue(strDateFromCur),
                    DateTo = CUtils.StrValue(strDateToCur),
                };
                var j = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_MapDeliveryOrder_ByInvFIOut);
                var objRT_Rpt_MapDeliveryOrder_ByInvFIOut = ReportsService.Instance.WA_Rpt_MapDeliveryOrder_ByInvFIOut(objRQ_Rpt_MapDeliveryOrder_ByInvFIOut, addressAPIs);
                if(objRT_Rpt_MapDeliveryOrder_ByInvFIOut!=null && objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut!=null && objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut.Count > 0)
                {
                    listData.AddRange(objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut);
                    foreach (var item in listData)
                    {
                        var listDataExist = listData.Where(x => x.IF_InvOutNo.Equals(item.IF_InvOutNo) && x.ProductCode.Equals(item.ProductCode)).ToList();
                        var existUI = listDataUI.Where(x => x.IF_InvOutNo.Equals(item.IF_InvOutNo) && x.ProductCode.Equals(item.ProductCode)).FirstOrDefault();
                        var listArea1 = new List<string>();
                        if (existUI == null)
                        {
                            var dataUI = new Rpt_MapDeliveryOrder_ByInvFIOutUI()
                            {
                                ProductCode = item.ProductCode,
                                AreaCode = item.AreaCode,
                                AreaCodeUser = item.AreaCodeUser,
                                AreaName = item.AreaName,
                                CreateDTimeUTC = item.CreateDTimeUTC,
                                CustomerCode = item.CustomerCode,
                                CustomerCodeSys  =item.CustomerCodeSys,
                                CustomerName = item.CustomerName,
                                IF_InvOutNo = item.IF_InvOutNo,
                                IF_InvOutStatus = item.IF_InvOutStatus,
                                OrgID_CTM = item.OrgID_CTM,
                                OrgID_PD = item.OrgID_PD,
                                ProductCodeUser = item.ProductCodeUser,
                                ProductName = item.ProductName,
                                ProfileStatus = item.ProfileStatus,
                                Qty = item.Qty,
                                UnitCode = item.UnitCode,
                                ListRpt_Date = new List<Rpt_MapDeliveryOrder_ByInvFIOut_Date>(),
                                ListMst_Area = new List<string>(),
                            };
                            foreach (var itDate in listDataExist)
                            {
                                if (!CUtils.IsNullOrEmpty(itDate.AreaCodeUser))
                                {
                                    //listArea1.Add(CUtils.StrValue(itDate.AreaCodeUser));
                                    //var listArea = listArea1.Distinct().ToList();
                                    dataUI.ListMst_Area.Add(CUtils.StrValue(itDate.AreaCodeUser));

                                }
                                
                                dataUI.ListRpt_Date.Add(new Rpt_MapDeliveryOrder_ByInvFIOut_Date()
                                {
                                    Rpt_Date = itDate.Rtp_Date,
                                    Qty = itDate.Qty,
                                });
                            }
                            dataUI.ListMst_Area.Distinct().ToList();
                            listDataUI.Add(dataUI);
                        }
                        else
                        {

                        }
                    }
                }
                return JsonView("BindDataRpt_MapDeliveryOrder_ByInvFIOut", listDataUI);
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
        public ActionResult Rpt_MapDeliveryOrder_ByInvFIOut_Export(string datefrom = "", string dateto = "")
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Danh sách ngày"]
            var strDateFromCur = "";
            var strDateToCur = "";

            var today = CUtils.ConvertToDateTime(CUtils.ConvertingUTCToLocalTime(Today, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_DB_FORMAT));
            //Danh sách ngày gen ra từ ngày từ đến
            var strDateTo = CUtils.ConvertToDateTime(dateto);
            var strDateFrom = CUtils.ConvertToDateTime(datefrom);

            var listDates = Enumerable.Range(0, (strDateTo - strDateFrom).Days + 1).Select(day => strDateFrom.AddDays(day)).ToList();

            #endregion

            var listData = new List<Rpt_MapDeliveryOrder_ByInvFIOut>();
            var listDataUI = new List<Rpt_MapDeliveryOrder_ByInvFIOutUI>();

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                if (!CUtils.IsNullOrEmpty(datefrom))
                {
                    var datefromCur = CUtils.StrValue(datefrom) + " 00:00:00";
                    strDateFromCur = CUtils.ConvertingLocalTimeToUTC(datefromCur, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                }
                if (!CUtils.IsNullOrEmpty(dateto))
                {
                    var datetoCur = CUtils.StrValue(dateto) + " 23:59:59";
                    strDateToCur = CUtils.ConvertingLocalTimeToUTC(datetoCur, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                }

                var objRQ_Rpt_MapDeliveryOrder_ByInvFIOut = new RQ_Rpt_MapDeliveryOrder_ByInvFIOut()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    DateFrom = CUtils.StrValue(strDateFromCur),
                    DateTo = CUtils.StrValue(strDateToCur),
                };
                var objRT_Rpt_MapDeliveryOrder_ByInvFIOut = ReportsService.Instance.WA_Rpt_MapDeliveryOrder_ByInvFIOut(objRQ_Rpt_MapDeliveryOrder_ByInvFIOut, addressAPIs);
                if (objRT_Rpt_MapDeliveryOrder_ByInvFIOut != null && objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut != null && objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut.Count > 0)
                {
                    listData.AddRange(objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut);
                }
                if (listData != null && listData.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_MapDeliveryOrder_ByInvFIOut();
                    #region ["Gen thêm column ngày động"]
                    if(listDates!=null && listDates.Count > 0)
                    {
                        foreach (var item in listDates)
                        {
                            dicColNames.Add(CUtils.StrValue(CUtils.FormatDate(item, Nonsense.DATE_TIME_DB_FORMAT)), CUtils.StrValue(CUtils.FormatDate(item, Nonsense.DATE_TIME_DB_FORMAT)));
                        }
                    }
                    #endregion
                    
                    var table = new DataTable("Rpt_MapDeliveryOrder_ByInvFIOut");
                    foreach (var col in dicColNames.Keys)
                    {
                        table.Columns.Add(dicColNames[col].Trim(), typeof(string));
                    }
                    var dtrow = table.NewRow();
                    foreach (DataColumn col in table.Columns)
                    {
                        foreach (var dicColumn in dicColNames.Where(dicColumn => dicColumn.Value.Trim().Equals(col.ColumnName)))
                        {
                            dtrow[col.ColumnName] = dicColumn.Key.Trim();
                            break;
                        }
                    }
                    foreach (var item in listData)
                    {
                        var listDataExist = listData.Where(x => x.IF_InvOutNo.Equals(item.IF_InvOutNo) && x.ProductCode.Equals(item.ProductCode)).ToList();
                        var existUI = listDataUI.Where(x => x.IF_InvOutNo.Equals(item.IF_InvOutNo) && x.ProductCode.Equals(item.ProductCode)).FirstOrDefault();
                        if (existUI == null)
                        {
                            var dataUI = new Rpt_MapDeliveryOrder_ByInvFIOutUI()
                            {
                                ProductCode = item.ProductCode,
                                AreaCode = item.AreaCode,
                                AreaCodeUser = item.AreaCodeUser,
                                AreaName = item.AreaName,
                                CreateDTimeUTC = item.CreateDTimeUTC,
                                CustomerCode = item.CustomerCode,
                                CustomerCodeSys = item.CustomerCodeSys,
                                CustomerName = item.CustomerName,
                                IF_InvOutNo = item.IF_InvOutNo,
                                IF_InvOutStatus = item.IF_InvOutStatus,
                                OrgID_CTM = item.OrgID_CTM,
                                OrgID_PD = item.OrgID_PD,
                                ProductCodeUser = item.ProductCodeUser,
                                ProductName = item.ProductName,
                                ProfileStatus = item.ProfileStatus,
                                Qty = item.Qty,
                                UnitCode = item.UnitCode,
                                ListRpt_Date = new List<Rpt_MapDeliveryOrder_ByInvFIOut_Date>(),
                            };
                            foreach (var itDate in listDataExist)
                            {
                                dataUI.ListRpt_Date.Add(new Rpt_MapDeliveryOrder_ByInvFIOut_Date()
                                {
                                    Rpt_Date = itDate.Rtp_Date,
                                    Qty = itDate.Qty,
                                });
                            }
                            listDataUI.Add(dataUI);
                        }
                        else
                        {

                        }
                    }
                    #region ["Xử lý fill dữ liệu vào column ngày động"]
                    foreach (var item in listDataUI)
                    {
                        //tạo row mới
                        var dr = table.NewRow();
                        // Add theo chỉ số index
                        dr[0] = CUtils.StrValue(item.AreaCode);
                        dr[1] = CUtils.StrValue(item.CustomerCode);
                        dr[2] = CUtils.StrValue(item.CustomerName);
                        dr[3] = CUtils.StrValue(item.IF_InvOutNo);
                        dr[4] = CUtils.StrValue(item.ProductCodeUser);
                        dr[5] = CUtils.StrValue(item.UnitCode);
                        //check tồn tại ngày nào đó trong list ngày tìm kiếm hay không để add giá trị tương ứng
                        var idx = 6;
                        foreach (var itDate in listDates)
                        {
                            var strDate = CUtils.StrValue(CUtils.FormatDate(itDate, Nonsense.DATE_TIME_DB_FORMAT));
                            if (item.ListRpt_Date != null && item.ListRpt_Date.Count > 0)
                            {
                                var existDate = item.ListRpt_Date.Where(x => x.Rpt_Date.Equals(strDate)).FirstOrDefault();
                                if (existDate != null)
                                {
                                    dr[idx] = existDate.Qty;
                                }
                                else
                                {
                                    dr[idx] = 0;
                                }
                            }
                            idx++;
                        }
                        table.Rows.Add(dr);
                    }
                    #endregion

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_MapDeliveryOrder_ByInvFIOut).Name), ref url);
                    ExcelExport.ExportToExcel(table, filePath);
                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_MapDeliveryOrder_ByInvFIOut()
        {
            return new Dictionary<string, string>()
            {
                 {"AreaCode","Khu vực"},
                 {"CustomerCode","Mã khách hàng"},
                 {"CustomerName","Tên khách hàng"},
                 {"IF_InvOutNo","Số phiếu xuất"},
                 {"ProductCodeUser","Mã hàng"},
                 {"UnitCode","Đơn vị"},
                 //cần phải code để add thêm column ngày động
            };
        }
        #endregion

        #region Báo cáo tuổi tồn kho
        public ActionResult Rpt_Inv_InventoryBalance_StorageTime()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE_STORAGETIME");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion

            ViewBag.Today = DateTime.Now.ToString("yyyy-MM-dd");

            return View(new List<Rpt_Inv_InventoryBalance_StorageTimeUI>());
        }

        public ActionResult DoSearch_Rpt_Inv_InventoryBalance_StorageTime(string productCodeUser, string productGrpCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE_STORAGETIME");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (!CUtils.IsNullOrEmpty(productGrpCode))
                {
                    string[] arrProductGrpCode = productGrpCode.Split(',');
                    if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                    {
                        foreach (var grpcode in arrProductGrpCode)
                        {
                            lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                        }
                    }
                }

                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_StorageTime = new List<Rpt_Inv_InventoryBalance_StorageTime>();
                var listMst_ProductGroup = new List<Mst_ProductGroup>();
                var objRQ_Rpt_Inv_InventoryBalance_StorageTime = new RQ_Rpt_Inv_InventoryBalance_StorageTime()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var objRT_Rpt_Inv_InventoryBalance_StorageTime = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_StorageTime(objRQ_Rpt_Inv_InventoryBalance_StorageTime, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_StorageTimeUIBase = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                if (objRT_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);
                        double qtyTotalOKSum = 0;
                        double totalValInvSum = 0;

                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                        totalValInvSum = listBalance.Sum(it => Convert.ToDouble(it.TotalValInv));
                        var objRpt_Inv_InventoryBalance_StorageTimeUI = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                            UPInv = listBalance[0].UPInv,
                            TotalValInv = totalValInvSum,
                            LastDTimeInvIn = listBalance[0].LastDTimeInvIn,
                            StorageTime = listBalance[0].StorageTime,
                            QtyTotalOK = qtyTotalOKSum,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_StorageTimeUIChild = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.QtyTotalOK) * ratio;

                                var upInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.UPInv) / ratio;
                                var totalValInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.TotalValInv);
                                var lastDTimeInvIn = objRpt_Inv_InventoryBalance_StorageTimeUI.LastDTimeInvIn;
                                var storageTime = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.StorageTime);
                                var objRpt_Inv_InventoryBalance_StorageTimeUIChild = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                                    UPInv = upInv,
                                    TotalValInv = totalValInv,
                                    LastDTimeInvIn = lastDTimeInvIn,
                                    StorageTime = storageTime,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = child.mpg_ProductGrpName
                                };
                                ListRpt_Inv_InventoryBalance_StorageTimeUIChild.Add(objRpt_Inv_InventoryBalance_StorageTimeUIChild);
                            }
                            objRpt_Inv_InventoryBalance_StorageTimeUI.ListRpt_Inv_InventoryBalance_StorageTimeUIChild = ListRpt_Inv_InventoryBalance_StorageTimeUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_StorageTimeUIBase.Add(objRpt_Inv_InventoryBalance_StorageTimeUI);
                    }
                }
                return JsonView("BindDataRpt_Inv_InventoryBalance_StorageTime", List_Rpt_Inv_InventoryBalance_StorageTimeUIBase);
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
        public ActionResult Rpt_Inv_InventoryBalance_StorageTime_Export(string productCodeUser, string productGrpCode)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion           
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (!CUtils.IsNullOrEmpty(productGrpCode))
                {
                    string[] arrProductGrpCode = productGrpCode.Split(',');
                    if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                    {
                        foreach (var grpcode in arrProductGrpCode)
                        {
                            lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                        }
                    }
                }
                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_StorageTime = new List<Rpt_Inv_InventoryBalance_StorageTime>();
                var listMst_ProductGroup = new List<Mst_ProductGroup>();
                var objRQ_Rpt_Inv_InventoryBalance_StorageTime = new RQ_Rpt_Inv_InventoryBalance_StorageTime()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var objRT_Rpt_Inv_InventoryBalance_StorageTime = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_StorageTime(objRQ_Rpt_Inv_InventoryBalance_StorageTime, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_StorageTimeUIBase = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                if (objRT_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);
                        double qtyTotalOKSum = 0;

                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));

                        var objRpt_Inv_InventoryBalance_StorageTimeUI = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                            UPInv = listBalance[0].UPInv,
                            TotalValInv = listBalance[0].TotalValInv,
                            LastDTimeInvIn = listBalance[0].LastDTimeInvIn,
                            StorageTime = listBalance[0].StorageTime,
                            QtyTotalOK = qtyTotalOKSum,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName,
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_StorageTimeUIChild = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.QtyTotalOK) * ratio;

                                var upInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.UPInv) * ratio;
                                var totalValInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.TotalValInv) * ratio;
                                var lastDTimeInvIn = objRpt_Inv_InventoryBalance_StorageTimeUI.LastDTimeInvIn;
                                var storageTime = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.StorageTime);
                                var objRpt_Inv_InventoryBalance_StorageTimeUIChild = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                                    UPInv = upInv,
                                    TotalValInv = totalValInv,
                                    LastDTimeInvIn = lastDTimeInvIn,
                                    StorageTime = storageTime,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = child.mpg_ProductGrpName
                                };
                                ListRpt_Inv_InventoryBalance_StorageTimeUIChild.Add(objRpt_Inv_InventoryBalance_StorageTimeUIChild);
                            }
                            objRpt_Inv_InventoryBalance_StorageTimeUI.ListRpt_Inv_InventoryBalance_StorageTimeUIChild = ListRpt_Inv_InventoryBalance_StorageTimeUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_StorageTimeUIBase.Add(objRpt_Inv_InventoryBalance_StorageTimeUI);
                    }
                }
                if (objRT_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Inv_InventoryBalance_StorageTime();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Inv_InventoryBalance_StorageTime).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_Inv_InventoryBalance_StorageTimeUIBase, dicColNames, filePath, string.Format("Rpt_Inv_InventoryBalance_StorageTime"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu thỏa điều kiện xuất excel", CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Inv_InventoryBalance_StorageTime()
        {
            return new Dictionary<string, string>()
            {
                 { "mp_ProductCodeUser", "Mã hàng hóa" },
                { "mp_ProductName", "Tên hàng hoá" },
                { "mp_UnitCode", "Đơn vị" },
                { "mpc_ProductGrpName", "Nhóm hàng" },
                { "QtyTotalOK", "SL tồn kho" },
                { "UPInv", "Giá vốn" },
                { "TotalValInv", "Giá trị tồn" },
                { "LastDTimeInvIn", "Ngày nhập kho gần nhất" },
                { "StorageTime", "Số ngày tồn" },
            };
        }
        #endregion

        #region ["In báo cáo tuổi tồn kho"]
        [HttpPost]
        public ActionResult PrintTemp_Rpt_Inv_InventoryBalance_StorageTime(string productCodeUser, string productGrpCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion

                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (!CUtils.IsNullOrEmpty(productGrpCode))
                {
                    string[] arrProductGrpCode = productGrpCode.Split(',');
                    if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                    {
                        foreach (var grpcode in arrProductGrpCode)
                        {
                            lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                        }
                    }
                }

                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_StorageTime = new List<Rpt_Inv_InventoryBalance_StorageTime>();
                var listMst_ProductGroup = new List<Mst_ProductGroup>();
                var objRQ_Rpt_Inv_InventoryBalance_StorageTime = new RQ_Rpt_Inv_InventoryBalance_StorageTime()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var objRT_Rpt_Inv_InventoryBalance_StorageTime = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_StorageTime(objRQ_Rpt_Inv_InventoryBalance_StorageTime, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_StorageTimeUIBase = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                if (objRT_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime != null && objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);
                        double qtyTotalOKSum = 0;

                        qtyTotalOKSum = listBalance.Sum(it => Convert.ToDouble(it.QtyTotalOK));
                        var objRpt_Inv_InventoryBalance_StorageTimeUI = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                            UPInv = listBalance[0].UPInv,
                            TotalValInv = listBalance[0].TotalValInv,
                            LastDTimeInvIn = listBalance[0].LastDTimeInvIn,
                            StorageTime = listBalance[0].StorageTime,
                            QtyTotalOK = qtyTotalOKSum,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_StorageTimeUIChild = new List<Rpt_Inv_InventoryBalance_StorageTimeUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.QtyTotalOK) * ratio;

                                var upInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.UPInv) * ratio;
                                var totalValInv = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.TotalValInv) * ratio;
                                var lastDTimeInvIn = objRpt_Inv_InventoryBalance_StorageTimeUI.LastDTimeInvIn;
                                var storageTime = Convert.ToDouble(objRpt_Inv_InventoryBalance_StorageTimeUI.StorageTime);
                                var objRpt_Inv_InventoryBalance_StorageTimeUIChild = new Rpt_Inv_InventoryBalance_StorageTimeUI
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
                                    UPInv = upInv,
                                    TotalValInv = totalValInv,
                                    LastDTimeInvIn = lastDTimeInvIn,
                                    StorageTime = storageTime,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = child.mpg_ProductGrpName
                                };
                                ListRpt_Inv_InventoryBalance_StorageTimeUIChild.Add(objRpt_Inv_InventoryBalance_StorageTimeUIChild);
                            }
                            objRpt_Inv_InventoryBalance_StorageTimeUI.ListRpt_Inv_InventoryBalance_StorageTimeUIChild = ListRpt_Inv_InventoryBalance_StorageTimeUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_StorageTimeUIBase.Add(objRpt_Inv_InventoryBalance_StorageTimeUI);
                    }
                }

                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                Rpt_Inv_InventoryBalance_StorageTimePrint objPrint = new Rpt_Inv_InventoryBalance_StorageTimePrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CreateUserName = strNNTFullName;
                objPrint.Lst_Rpt_Inv_InventoryBalance_StorageTimeUI = List_Rpt_Inv_InventoryBalance_StorageTimeUIBase;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("BCTTONKHO");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                    }

                    data.data = CreateDataObjectRpt_Inv_InventoryBalance_StorageTimeServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                            return Json(new { Success = true, LinkPDF = linkPdf });
                        }
                    }
                }
                else
                {
                    resultEntry.SetFailed();
                    resultEntry.AddMessage("Không có mẫu in");
                    return Json(resultEntry);
                }

                #endregion
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectRpt_Inv_InventoryBalance_StorageTimeServer(Rpt_Inv_InventoryBalance_StorageTimePrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.Rpt_Inv_InventoryBalance_StorageTime;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);

            var MyDynamic = new Rpt_Inv_InventoryBalance_StorageTimeReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvName = CUtils.StrValueNew(objTempPrint.InvName);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<Rpt_Inv_InventoryBalance_StorageTimeDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_Rpt_Inv_InventoryBalance_StorageTimeUI != null && objTempPrint.Lst_Rpt_Inv_InventoryBalance_StorageTimeUI.Count > 0)
            {
                var listDtl_ReportServer = new List<Rpt_Inv_InventoryBalance_StorageTimeDtlReportServer>();
                var idx = 1;
                foreach (var item in objTempPrint.Lst_Rpt_Inv_InventoryBalance_StorageTimeUI)
                {
                    var objDtl_ReportServer = new Rpt_Inv_InventoryBalance_StorageTimeDtlReportServer
                    {
                        Idx = CUtils.StrValue(idx),
                        ProductCodeUser = CUtils.StrValue(item.mp_ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        UnitCode = CUtils.StrValue(item.mp_UnitCode),
                        QtyTotalOK = CUtils.StrValueFormatNumber(item.QtyTotalOK, NumericFormat(tableName, "QtyTotalOK", defaultFormat)),
                        TotalValInv = CUtils.StrValueFormatNumber(item.TotalValInv, NumericFormat(tableName, "TotalValInv", defaultFormat)),
                        LastDTimeInvIn = CUtils.StrValue(item.LastDTimeInvIn),
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                    idx++;
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }
        #endregion

        #region Báo cáo chạm tồn tối thiểu
        public ActionResult Rpt_Inv_InventoryBalance_Minimum()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE_MINIMUM");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            #region ["Danh sách nhóm sp"]
            var strWhere = "";
            var List_Mst_ProductGroup = new List<Mst_ProductGroup>();
            var strWhere_Mst_ProductGroup = "Mst_ProductGroup.FlagActive = '1'";
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*"
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                List_Mst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            ViewBag.Lst_Mst_ProductGroup = List_Mst_ProductGroup;
            #endregion

            ViewBag.Today = DateTime.Now.ToString("yyyy-MM-dd");

            return View(new List<Rpt_Inv_InventoryBalance_MinimumUI>());
        }

        public ActionResult DoSearch_Rpt_Inv_InventoryBalance_Minimum(string productCodeUser, string productGrpCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_RPT_INV_INVENTORYBALANCE_MINIMUM");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var lstMst_ProductGroup = new List<Mst_ProductGroup>();
            if (!CUtils.IsNullOrEmpty(productGrpCode))
            {
                string[] arrProductGrpCode = productGrpCode.Split(',');
                if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                {
                    foreach (var grpcode in arrProductGrpCode)
                    {
                        lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                    }
                }
            }
            #endregion

            try
            {
                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_Minimum = new List<Rpt_Inv_InventoryBalance_Minimum>();
                var objRQ_Rpt_Inv_InventoryBalance_Minimum = new RQ_Rpt_Inv_InventoryBalance_Minimum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Minimum);
                var objRT_Rpt_Inv_InventoryBalance_Minimum = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_Minimum(objRQ_Rpt_Inv_InventoryBalance_Minimum, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_MinimumUIBase = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                if (objRT_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);

                        var objRpt_Inv_InventoryBalance_MinimumUI = new Rpt_Inv_InventoryBalance_MinimumUI
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
                            QtyMinSt = listBalance[0].QtyMinSt,
                            QtyTotalOK = listBalance[0].QtyTotalOK,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_MinimumUIChild = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyTotalOK) * ratio;
                                var qtyMinSt = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyMinSt) * ratio;
                                var mpc_ProductGrpNameItem = objRpt_Inv_InventoryBalance_MinimumUI.mpc_ProductGrpName;
                                var objRpt_Inv_InventoryBalance_MinimumUIChild = new Rpt_Inv_InventoryBalance_MinimumUI
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
                                    QtyMinSt = qtyMinSt,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = mpc_ProductGrpNameItem
                                };
                                ListRpt_Inv_InventoryBalance_MinimumUIChild.Add(objRpt_Inv_InventoryBalance_MinimumUIChild);
                            }
                            objRpt_Inv_InventoryBalance_MinimumUI.ListRpt_Inv_InventoryBalance_MinimumUIChild = ListRpt_Inv_InventoryBalance_MinimumUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_MinimumUIBase.Add(objRpt_Inv_InventoryBalance_MinimumUI);
                    }
                }
                return JsonView("BindDataRpt_Inv_InventoryBalance_Minimum", List_Rpt_Inv_InventoryBalance_MinimumUIBase);
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
        public ActionResult Rpt_Inv_InventoryBalance_Minimum_Export(string productCodeUser, string productGrpCode)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion           
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (!CUtils.IsNullOrEmpty(productGrpCode))
                {
                    string[] arrProductGrpCode = productGrpCode.Split(',');
                    if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                    {
                        foreach (var grpcode in arrProductGrpCode)
                        {
                            lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                        }
                    }
                }

                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_Minimum = new List<Rpt_Inv_InventoryBalance_Minimum>();
                var objRQ_Rpt_Inv_InventoryBalance_Minimum = new RQ_Rpt_Inv_InventoryBalance_Minimum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Minimum);
                var objRT_Rpt_Inv_InventoryBalance_Minimum = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_Minimum(objRQ_Rpt_Inv_InventoryBalance_Minimum, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_MinimumUIBase = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                if (objRT_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);

                        var objRpt_Inv_InventoryBalance_MinimumUI = new Rpt_Inv_InventoryBalance_MinimumUI
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
                            QtyMinSt = listBalance[0].QtyMinSt,
                            QtyTotalOK = listBalance[0].QtyTotalOK,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_MinimumUIChild = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyTotalOK) * ratio;
                                var qtyMinSt = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyMinSt) * ratio;
                                var mpc_ProductGrpNameItem = objRpt_Inv_InventoryBalance_MinimumUI.mpc_ProductGrpName;
                                var objRpt_Inv_InventoryBalance_MinimumUIChild = new Rpt_Inv_InventoryBalance_MinimumUI
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
                                    QtyMinSt = qtyMinSt,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = mpc_ProductGrpNameItem
                                };
                                ListRpt_Inv_InventoryBalance_MinimumUIChild.Add(objRpt_Inv_InventoryBalance_MinimumUIChild);
                            }
                            objRpt_Inv_InventoryBalance_MinimumUI.ListRpt_Inv_InventoryBalance_MinimumUIChild = ListRpt_Inv_InventoryBalance_MinimumUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_MinimumUIBase.Add(objRpt_Inv_InventoryBalance_MinimumUI);
                    }
                }
                if (List_Rpt_Inv_InventoryBalance_MinimumUIBase != null && List_Rpt_Inv_InventoryBalance_MinimumUIBase.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColumsRpt_Inv_InventoryBalance_Minimum();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_Inv_InventoryBalance_Minimum).Name), ref url);
                    ExcelExport.ExportToExcelFromList(List_Rpt_Inv_InventoryBalance_MinimumUIBase, dicColNames, filePath, string.Format("Rpt_Inv_InventoryBalance_Minimum"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu thỏa điều kiện xuất excel", CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetExportDicColumsRpt_Inv_InventoryBalance_Minimum()
        {
            return new Dictionary<string, string>()
            {
                 { "mp_ProductCodeUser", "Mã hàng hóa" },
                { "mp_ProductName", "Tên hàng hoá" },
                { "mp_UnitCode", "Đơn vị" },
                { "mpc_ProductGrpName", "Nhóm hàng" },
                { "QtyMinSt", "SL tồn tối thiểu" },
                { "QtyTotalOK", "SL tồn kho" },
            };
        }
        #endregion

        #region ["In báo cáo chạm tồn tối thiểu"]
        [HttpPost]
        public ActionResult PrintTemp_Rpt_Inv_InventoryBalance_Minimum(string productCodeUser, string productGrpCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion

                var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                if (!CUtils.IsNullOrEmpty(productGrpCode))
                {
                    string[] arrProductGrpCode = productGrpCode.Split(',');
                    if (arrProductGrpCode != null && arrProductGrpCode.Length > 0)
                    {
                        foreach (var grpcode in arrProductGrpCode)
                        {
                            lstMst_ProductGroup.Add(new Mst_ProductGroup { ProductGrpCode = grpcode });
                        }
                    }
                }

                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var listRpt_Inv_InventoryBalance_Minimum = new List<Rpt_Inv_InventoryBalance_Minimum>();
                var objRQ_Rpt_Inv_InventoryBalance_Minimum = new RQ_Rpt_Inv_InventoryBalance_Minimum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    ProductCode = CUtils.StrValue(productCodeUser),
                    ReportDateUTC = dateTimeNow,
                    Lst_Mst_ProductGroup = lstMst_ProductGroup
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Minimum);
                var objRT_Rpt_Inv_InventoryBalance_Minimum = ReportsService.Instance.WA_Rpt_Inv_InventoryBalance_Minimum(objRQ_Rpt_Inv_InventoryBalance_Minimum, addressAPIs);
                var List_Rpt_Inv_InventoryBalance_MinimumUIBase = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                if (objRT_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum != null && objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Count > 0)
                {
                    var ListProductCode = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.ProductCode).Distinct().ToList();
                    var ListProductCodeBase = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum.Select(rt => rt.mp_ProductCodeBase).Distinct().ToList();
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
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
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
                        var listBalance = objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum
                            .Where(item => item.ProductCode.Equals(code))
                            .ToList();
                        var listProductChild = List_Mst_Product
                            .Where(p => p.ProductCodeBase.Equals(listBalance[0].mp_ProductCodeBase) && !p.ProductCode.Equals(listBalance[0].ProductCode))
                            .ToList();
                        var listUnitCodeChild = listProductChild.Select(unit => unit.UnitCode).Distinct().ToList();

                        var listUnitCode = new List<object>();
                        listUnitCode.Add(listBalance[0].mp_UnitCode);
                        listUnitCode.AddRange(listUnitCodeChild);

                        var objRpt_Inv_InventoryBalance_MinimumUI = new Rpt_Inv_InventoryBalance_MinimumUI
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
                            QtyMinSt = listBalance[0].QtyMinSt,
                            QtyTotalOK = listBalance[0].QtyTotalOK,
                            mpc_ProductGrpName = listBalance[0].mpc_ProductGrpName
                        };

                        if (listProductChild != null && listProductChild.Count > 0)
                        {
                            var ListRpt_Inv_InventoryBalance_MinimumUIChild = new List<Rpt_Inv_InventoryBalance_MinimumUI>();
                            foreach (var child in listProductChild)
                            {
                                var ratio = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.mp_ValConvert) / Convert.ToDouble(child.ValConvert);
                                var qtyTotalOK = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyTotalOK) * ratio;
                                var qtyMinSt = Convert.ToDouble(objRpt_Inv_InventoryBalance_MinimumUI.QtyMinSt) * ratio;
                                var mpc_ProductGrpNameItem = objRpt_Inv_InventoryBalance_MinimumUI.mpc_ProductGrpName;
                                var objRpt_Inv_InventoryBalance_MinimumUIChild = new Rpt_Inv_InventoryBalance_MinimumUI
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
                                    QtyMinSt = qtyMinSt,
                                    QtyTotalOK = qtyTotalOK,
                                    mpc_ProductGrpName = mpc_ProductGrpNameItem
                                };
                                ListRpt_Inv_InventoryBalance_MinimumUIChild.Add(objRpt_Inv_InventoryBalance_MinimumUIChild);
                            }
                            objRpt_Inv_InventoryBalance_MinimumUI.ListRpt_Inv_InventoryBalance_MinimumUIChild = ListRpt_Inv_InventoryBalance_MinimumUIChild;
                        }
                        List_Rpt_Inv_InventoryBalance_MinimumUIBase.Add(objRpt_Inv_InventoryBalance_MinimumUI);
                    }
                }

                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                Rpt_Inv_InventoryBalance_MinimumPrint objPrint = new Rpt_Inv_InventoryBalance_MinimumPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CreateUserName = strNNTFullName;
                objPrint.Lst_Rpt_Inv_InventoryBalance_MinimumUI = List_Rpt_Inv_InventoryBalance_MinimumUIBase;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("BCTONKHOMIN");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                    }

                    data.data = CreateDataObjectRpt_Inv_InventoryBalance_MinimumServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                            return Json(new { Success = true, LinkPDF = linkPdf });
                        }
                    }
                }
                else
                {
                    resultEntry.SetFailed();
                    resultEntry.AddMessage("Không có mẫu in");
                    return Json(resultEntry);
                }

                #endregion
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectRpt_Inv_InventoryBalance_MinimumServer(Rpt_Inv_InventoryBalance_MinimumPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.Rpt_Inv_InventoryBalance_Minimum;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);

            var MyDynamic = new Rpt_Inv_InventoryBalance_MinimumReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvName = CUtils.StrValueNew(objTempPrint.InvName);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<Rpt_Inv_InventoryBalance_MinimumDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_Rpt_Inv_InventoryBalance_MinimumUI != null && objTempPrint.Lst_Rpt_Inv_InventoryBalance_MinimumUI.Count > 0)
            {
                var listDtl_ReportServer = new List<Rpt_Inv_InventoryBalance_MinimumDtlReportServer>();
                var idx = 1;
                foreach (var item in objTempPrint.Lst_Rpt_Inv_InventoryBalance_MinimumUI)
                {
                    var objDtl_ReportServer = new Rpt_Inv_InventoryBalance_MinimumDtlReportServer
                    {
                        Idx = CUtils.StrValue(idx),
                        ProductCodeUser = CUtils.StrValue(item.mp_ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        ProductGrpName = CUtils.StrValue(item.mpc_ProductGrpName),
                        UnitCode = CUtils.StrValue(item.mp_UnitCode),
                        QtyTotalOK = CUtils.StrValueFormatNumber(item.QtyTotalOK, NumericFormat(tableName, "QtyTotalOK", defaultFormat)),
                        QtyMinSt = CUtils.StrValueFormatNumber(item.QtyMinSt, NumericFormat(tableName, "QtyMinSt", defaultFormat)),
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                    idx++;
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }
        #endregion
    }
}