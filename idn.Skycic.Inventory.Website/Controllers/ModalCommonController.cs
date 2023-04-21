using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.ClientService.Services.Inbrand;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.Inbrand;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class ModalCommonController : AdminBaseController
    {
        // GET: ModalCommon
        [HttpPost]
        public ActionResult Lo(string productCodeBase, string invBUPattern, string productCode, string productCodeUser, string productName, string qty, string listLot)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
                sbSql.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
                strWhere = sbSql.ToString();
                var rq = new RQ_Inv_InventoryBalanceLot()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceLot = "*"
                };
                var lstInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                var rt = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceLot != null)
                {
                    lstInv_InventoryBalanceLot = rt.Lst_Inv_InventoryBalanceLot;
                }
                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeBase = productCodeBase;
                //              

                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;

                var mstProduct = GetMstProduct(productCode);
                ViewBag.valconvert = mstProduct.ValConvert;
                double valConvert = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert = Convert.ToDouble(mstProduct.ValConvert);
                }

                //Tính lại tồn kho theo Đơn vị quy đổi
                foreach (var lot in lstInv_InventoryBalanceLot)
                {
                    lot.QtyTotalOK = lot.QtyTotalOK == null ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / valConvert, 2);
                }

                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;

                List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
                if (listLot != null && listLot.Length > 0)
                {
                    lstInv_InventoryBalanceLotView = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Inv_InventoryBalanceLotUI>>(listLot);
                    foreach (var lot in lstInv_InventoryBalanceLotView)
                    {
                        Inv_InventoryBalanceLot lotCur = lstInv_InventoryBalanceLot.Where(m => CUtils.StrValue(m.ProductLotNo).Equals(CUtils.StrValue(lot.ProductLotNo).ToUpper()) && CUtils.StrValue(m.InvCode).Equals(lot.InvCode)).FirstOrDefault();
                        if(lotCur != null)
                        {
                            lot.InvCode = lotCur.InvCode;
                            lot.ProductionDate = lotCur.ProductionDate;
                            lot.ExpiredDate = lotCur.ExpiredDate;
                            lot.QtyTotalOK = lotCur.QtyTotalOK;
                        }
                        
                    }
                }
                else
                {
                    foreach (var lot in lstInv_InventoryBalanceLot)
                    {
                        Inv_InventoryBalanceLotUI lotui = new Inv_InventoryBalanceLotUI
                        {
                            ProductCode = lot.ProductCode,
                            ProductLotNo = lot.ProductLotNo,
                            InvCode = lot.InvCode,
                            ProductionDate = lot.ProductionDate,
                            ExpiredDate = lot.ExpiredDate,
                            QtyTotalOK = lot.QtyTotalOK,
                            Qty = lot.QtyTotalOK
                        };
                        lstInv_InventoryBalanceLotView.Add(lotui);
                    }

                    #region Phân bổ số lượng 

                    if (!string.IsNullOrEmpty(qty))
                    {
                        var qtyExport = Convert.ToDouble(qty);
                        lstInv_InventoryBalanceLotView = lstInv_InventoryBalanceLotView.OrderByDescending(m => m.QtyTotalOK).ToList();
                        foreach (var lot in lstInv_InventoryBalanceLotView)
                        {
                            var qtyTotalOK = Convert.ToDouble(lot.QtyTotalOK);
                            if (qtyExport <= qtyTotalOK)
                            {
                                lot.Qty = qtyExport;
                                qtyExport = 0;
                            }
                            else
                            {
                                lot.Qty = qtyTotalOK;
                                qtyExport -= qtyTotalOK;
                            }
                        }
                    }

                    #endregion
                }

                ViewBag.lstInv_InventoryBalanceLotView = lstInv_InventoryBalanceLotView;

                return JsonView("Lo"/*, lstInv_InventoryBalanceLot*/);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLotTemp(string productCodeBase, string invBUPattern, string productCode, string productCodeUser, string productName, string qty)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
                sbSql.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
                strWhere = sbSql.ToString();
                var rq = new RQ_Inv_InventoryBalanceLot()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceLot = "*"
                };
                var lstInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                var rt = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceLot != null)
                {
                    lstInv_InventoryBalanceLot = rt.Lst_Inv_InventoryBalanceLot;
                }

                var mstProduct = GetMstProduct(productCode);
                double valConvert = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert = Convert.ToDouble(mstProduct.ValConvert);
                }

                //Tính lại tồn kho theo Đơn vị quy đổi
                foreach (var lot in lstInv_InventoryBalanceLot)
                {
                    lot.QtyTotalOK = lot.QtyTotalOK == null ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / valConvert, 2);
                }

                List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();

                foreach (var lot in lstInv_InventoryBalanceLot)
                {
                    Inv_InventoryBalanceLotUI lotui = new Inv_InventoryBalanceLotUI
                    {
                        ProductCode = lot.ProductCode,
                        ProductLotNo = lot.ProductLotNo,
                        InvCode = lot.InvCode,
                        ProductionDate = lot.ProductionDate,
                        ExpiredDate = lot.ExpiredDate,
                        QtyTotalOK = lot.QtyTotalOK,
                        Qty = lot.QtyTotalOK
                    };
                    lstInv_InventoryBalanceLotView.Add(lotui);
                }

                #region Phân bổ số lượng 

                if (!string.IsNullOrEmpty(qty))
                {
                    var qtyExport = Convert.ToDouble(qty);
                    lstInv_InventoryBalanceLotView = lstInv_InventoryBalanceLotView.OrderByDescending(m => m.QtyTotalOK).ToList();
                    foreach (var lot in lstInv_InventoryBalanceLotView)
                    {
                        var qtyTotalOK = Convert.ToDouble(lot.QtyTotalOK);
                        if (qtyExport <= qtyTotalOK)
                        {
                            lot.Qty = qtyExport;
                            qtyExport = 0;
                        }
                        else
                        {
                            lot.Qty = qtyTotalOK;
                            qtyExport -= qtyTotalOK;
                        }
                    }
                }

                #endregion

                return Json(new { Success = true, Data = lstInv_InventoryBalanceLotView });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }


        [HttpPost]
        public ActionResult Serial(string productCode, string productCodeBase, string invBUPattern, string type, string productCodeUser, string productName, string ViewType)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
                sbSql.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                strWhere = sbSql.ToString();
                var rq = new RQ_Inv_InventoryBalanceSerial()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceSerial = "*"
                };
                var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
                var rt = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceSerial != null)
                {
                    lstInv_InventoryBalanceSerial = rt.Lst_Inv_InventoryBalanceSerial;
                }
                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.type = type;
                ViewBag.ViewType = ViewType;
                return JsonView("Serial", lstInv_InventoryBalanceSerial);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSerialTemp(string productCodeBase, string invBUPattern, string qty)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
                sbSql.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                strWhere = sbSql.ToString();
                var rq = new RQ_Inv_InventoryBalanceSerial()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceSerial = "*"
                };
                var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
                var rt = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceSerial != null)
                {
                    if (!string.IsNullOrEmpty(qty))
                    {
                        var qtyExport = Convert.ToDouble(qty);
                        if (qtyExport >= rt.Lst_Inv_InventoryBalanceSerial.Count)
                        {
                            lstInv_InventoryBalanceSerial = rt.Lst_Inv_InventoryBalanceSerial;
                        }
                        else
                        {
                            int count = 0;
                            foreach (var it in rt.Lst_Inv_InventoryBalanceSerial)
                            {
                                if (count >= qtyExport)
                                {
                                    break;
                                }

                                lstInv_InventoryBalanceSerial.Add(it);
                                count++;
                            }
                        }
                    }
                }

                return Json(new { Success = true, Data = lstInv_InventoryBalanceSerial });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        [HttpPost]
        public ActionResult GetSerialBySerialNo(string serialNo)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                if (!string.IsNullOrEmpty(serialNo))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "SerialNo", CUtils.StrValue(serialNo), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Inv_InventoryBalanceSerial()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceSerial = "*"
                };
                var Inv_InventoryBalanceSerial = new Inv_InventoryBalanceSerial();
                var rt = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceSerial != null && rt.Lst_Inv_InventoryBalanceSerial.Count != 0)
                {
                    Inv_InventoryBalanceSerial = rt.Lst_Inv_InventoryBalanceSerial[0];
                }
                return Json(new { Success = true, data = Inv_InventoryBalanceSerial });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Combo(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string productName, string qtyCombo)
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
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;

                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Prd_BOM = "Prd_BOM.";
                if (!string.IsNullOrEmpty(productCode))
                {
                    sbSql.AddWhereClause(Tbl_Prd_BOM + "ProductCodeParent", CUtils.StrValue(productCode), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Product()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Rt_Cols_Mst_Product = "*",
                    //Rt_Cols_Mst_ProductFiles = "*",
                    //Rt_Cols_Mst_ProductImages = "*",
                    //Rt_Cols_Prd_Attribute = "*",
                    Rt_Cols_Prd_BOM = "*"
                };
                var lstPrd_BOM = new List<Prd_BOM>();
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                var rt = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Prd_BOM != null)
                {
                    lstPrd_BOM = rt.Lst_Prd_BOM;
                }

                #region Thông tin tồn kho
                var lst_StrProductCode = new List<string>();
                foreach (var it in lstPrd_BOM)
                {
                    lst_StrProductCode.Add(it.ProductCode.ToString());
                }
                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var lstProductCode = "";
                if (lst_StrProductCode.Count != 0)
                {
                    foreach (var productcode in lst_StrProductCode)
                    {
                        if (lstProductCode == "")
                        {
                            lstProductCode += productcode;
                        }
                        else
                        {
                            lstProductCode += "," + productcode;
                        }
                    }
                }
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (lstProductCode != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCode, "IN");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sb_SQL.AddWhereClause("Mst_Inventory." + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
                strWhereClause = sb_SQL.ToString();
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };

                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count != 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                }
                #endregion

                List<Prd_BOMUI> lstPrd_BOMUI = new List<Prd_BOMUI>();
                foreach (var it in lstPrd_BOM)
                {
                    Prd_BOMUI objPrd_BOMUI = new Prd_BOMUI();
                    objPrd_BOMUI.ProductCode = it.ProductCode;
                    objPrd_BOMUI.mp_ProductName = it.mp_ProductName;
                    objPrd_BOMUI.mp_UnitCode = it.mp_UnitCode;
                    objPrd_BOMUI.mp_UPSell = it.mp_UPSell;
                    objPrd_BOMUI.Qty = it.Qty;
                    objPrd_BOMUI.mp_ProductCodeUser = it.mp_ProductCodeUser;

                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                    {
                        var lstBalanceByProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                        if (lstBalanceByProduct != null && lstBalanceByProduct.Count > 0)
                        {
                            var qtytotalMax = lstBalanceByProduct.Max(x => x.QtyTotalOK);
                            var itBalanceMax = lstBalanceByProduct.Where(x => x.QtyTotalOK == qtytotalMax).FirstOrDefault();

                            // Giá trị lớn nhất
                            objPrd_BOMUI.QtyTotalOKMax = qtytotalMax;
                            objPrd_BOMUI.InvCodeMax = itBalanceMax.InvCode;
                        }

                        var lstTonKhoVT = new List<TonKhoVT>();
                        foreach (var bl in lstBalanceByProduct)
                        {
                            TonKhoVT tk = new TonKhoVT();
                            tk.InvCode = bl.InvCode;
                            tk.QtyTotalOK = bl.QtyTotalOK;
                            tk.ProductCode = bl.ProductCode;
                            lstTonKhoVT.Add(tk);
                        }

                        objPrd_BOMUI.lstTonKhoVT = lstTonKhoVT;
                        ViewBag.ProductCode = productCode;
                        lstPrd_BOMUI.Add(objPrd_BOMUI);
                    }
                }
                ViewBag.qtyCombo = qtyCombo;
                return JsonView("Combo", lstPrd_BOMUI);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Combo", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Customer(string customerCode)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!string.IsNullOrEmpty(customerCode))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCode", CUtils.StrValue(customerCode), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Customer()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Customer = "*",
                    Rt_Cols_UserOwner_Customer = "*"
                };
                var lsMst_Customer = new List<Mst_Customer>();
                var lstUserOwner_Customer = new List<UserOwner_Customer>();
                var rt = Mst_CustomerService.Instance.WA_Mst_Customer_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Mst_Customer != null)
                {
                    lsMst_Customer = rt.Lst_Mst_Customer;
                    lstUserOwner_Customer = rt.Lst_UserOwner_Customer;
                }
                ViewBag.lstUserOwner_Customer = lstUserOwner_Customer;
                return JsonView("ProductGetSolution", lsMst_Customer);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ProductGetSolution", null, resultEntry);
        }

        public Mst_Product GetMstProduct(string productCode)
        {
            Mst_Product objMst_Product = new Mst_Product();
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var strWhere = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = "Mst_Product.";
            if (!string.IsNullOrEmpty(productCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCode", CUtils.StrValue(productCode), "=");
            }
            strWhere = sbSql.ToString();
            var rq = new RQ_Mst_Product()
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
                Ft_WhereClause = strWhere,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductFiles = "*",
                Rt_Cols_Mst_ProductImages = "*",
                Rt_Cols_Prd_Attribute = "*",
                Rt_Cols_Prd_BOM = "*"
            };
            var lsMst_Product = new List<Mst_Product>();
            var rt = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
            if (rt != null && rt.Lst_Mst_Product != null && rt.Lst_Mst_Product.Count > 0)
            {
                objMst_Product = rt.Lst_Mst_Product[0];
            }
            return objMst_Product;
        }

        [HttpPost]
        public ActionResult ProductGetSolution(string productCode)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!string.IsNullOrEmpty(productCode))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCode", CUtils.StrValue(productCode), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Product()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductFiles = "*",
                    Rt_Cols_Mst_ProductImages = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Rt_Cols_Prd_BOM = "*"
                };
                var lsMst_Product = new List<Mst_Product>();
                var rt = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Mst_Product != null)
                {
                    lsMst_Product = rt.Lst_Mst_Product;
                }
                return JsonView("ProductGetSolution", lsMst_Product);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ProductGetSolution", null, resultEntry);
        }

        [HttpPost]
        public ActionResult GetDonViQuyDoiOld(string productCodeBase, string productCodeRoot)
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
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(productCodeBase), "=");
                }
                //if (!string.IsNullOrEmpty(productCodeRoot))
                //{
                //    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeRoot", CUtils.StrValue(productCodeRoot), "=");
                //}
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Product()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Product = "*",
                    //Rt_Cols_Mst_ProductFiles = "*",
                    //Rt_Cols_Mst_ProductImages = "*",
                    //Rt_Cols_Prd_Attribute = "*",
                    //Rt_Cols_Prd_BOM = "*"
                };
                var lsMst_Product = new List<Mst_Product>();
                var rt = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Mst_Product != null)
                {
                    lsMst_Product = rt.Lst_Mst_Product;
                }
                #region Lấy thông tin tồn kho
                var lst_StrProductCode = new List<string>();
                if (lsMst_Product.Count != 0)
                {
                    foreach (var it in lsMst_Product)
                    {
                        if (!lst_StrProductCode.Contains(it.ProductCode))
                        {
                            lst_StrProductCode.Add(it.ProductCode.ToString());
                        }
                    }
                }
                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var lstProductCode = "";
                if (lst_StrProductCode.Count != 0)
                {
                    foreach (var productcode in lst_StrProductCode)
                    {
                        if (lstProductCode == "")
                        {
                            lstProductCode += productcode;
                        }
                        else
                        {
                            lstProductCode += ", " + productcode;
                        }
                    }
                }
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (lstProductCode != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCode, "IN");
                }
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                }

                var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                foreach (var it in lsMst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductCodeUser = it.ProductCodeUser;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ValConvert = it.ValConvert;
                    productUI.FlagLot = it.FlagLot;
                    productUI.FlagSerial = it.FlagSerial;
                    productUI.ProductType = it.ProductType;
                    if (it.FlagLot.Equals("1"))
                    {
                        productUI.FlagLo = "1";
                    }
                    if (it.ProductType != null && it.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                    {
                        productUI.FlagCombo = "1";
                    }
                    // Check theo đơn hàng hay không
                    productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                    //
                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                    {
                        var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                        if (lstProductByCode != null && lstProductByCode.Count != 0)
                        {
                            var qtyTotalOkMax = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                            var itBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                            if (itBalance != null)
                            {
                                productUI.InvCode = itBalance.InvCode;
                                productUI.QtyTotalOK = qtyTotalOkMax;
                                productUI.DiscountPrice = "0"; // Tạm để
                            }
                        }
                    }
                    lst_Mst_ProductUI.Add(productUI);
                }
                #endregion                
                return JsonView("GetLstUnitCodeByProduct", lst_Mst_ProductUI);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetLstUnitCodeByProduct", null, resultEntry);
            //return Json(resultEntry);            
        }

        [HttpPost]
        public ActionResult GetDonViQuyDoi(string productCodeBase, string productCodeRoot)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(productCodeBase), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Product()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Product = "*",
                };
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null)
                {
                    #region Lấy thông tin tồn kho

                    var listProductCode = objRT_Mst_Product.Lst_Mst_Product.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
                    if (listProductCode != null && listProductCode.Count > 0)
                    {
                        listProductCode = listProductCode.Distinct().ToList();
                        var strProductCode = string.Join(",", listProductCode);

                        var strWhereClause = "";
                        var sb_SQL = new StringBuilder();

                        var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                        sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", strProductCode, "IN");
                        strWhereClause = sb_SQL.ToString();

                        var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                            Rt_Cols_Inv_InventoryBalance = "*",
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClause
                        };
                        var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                        var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                        if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                        {
                            lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                        }


                        foreach (var it in objRT_Mst_Product.Lst_Mst_Product)
                        {
                            var productUI = new Mst_ProductUI();
                            productUI.ProductCode = it.ProductCode;
                            productUI.ProductCodeUser = it.ProductCodeUser;
                            productUI.ProductCodeBase = it.ProductCodeBase;
                            productUI.ProductCodeRoot = it.ProductCodeRoot;
                            productUI.ProductName = it.ProductName;
                            productUI.UnitCode = it.UnitCode;
                            productUI.ValConvert = it.ValConvert;
                            productUI.FlagLot = it.FlagLot;
                            productUI.FlagSerial = it.FlagSerial;
                            productUI.ProductType = it.ProductType;
                            if (it.FlagLot.Equals("1"))
                            {
                                productUI.FlagLo = "1";
                            }
                            if (it.ProductType != null && it.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                            {
                                productUI.FlagCombo = "1";
                            }
                            // Check theo đơn hàng hay không
                            productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                            //
                            if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                            {
                                var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                if (lstProductByCode != null && lstProductByCode.Count != 0)
                                {
                                    var lstInv_InventoryBalance_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance, x => x.QtyTotalOK);
                                    // Danh sách hàng hóa tồn kho
                                    var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                    if (lstInv_InventoryBalance_ProductCode != null && lstInv_InventoryBalance_ProductCode.Count > 0)
                                    {
                                        // sắp xếp số lượng tồn kho giảm dần
                                        var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                        var itBalance = lstInv_InventoryBalance_ProductCode_OrderByDesc[0];
                                        var qtyTotalOKMax = "0";
                                        if (!CUtils.IsNullOrEmpty(itBalance.QtyTotalOK))
                                        {
                                            qtyTotalOKMax = CUtils.StrValue(itBalance.QtyTotalOK);
                                        }
                                        if (itBalance != null)
                                        {
                                            productUI.InvCode = itBalance.InvCode;
                                            productUI.QtyTotalOK = qtyTotalOKMax;
                                            productUI.DiscountPrice = "0"; // Tạm để


                                        }
                                    }
                                    //var qtyTotalOkMax = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                                    //var itBalance1 = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                                    //if (itBalance1 != null)
                                    //{
                                    //    productUI.InvCode = itBalance1.InvCode;
                                    //    productUI.QtyTotalOK = qtyTotalOkMax;
                                    //    productUI.DiscountPrice = "0"; // Tạm để
                                    //}
                                }
                            }
                            lst_Mst_ProductUI.Add(productUI);
                        }
                    }
                    #endregion
                }

                return JsonView("GetLstUnitCodeByProduct", lst_Mst_ProductUI);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetLstUnitCodeByProduct", null, resultEntry);
        }

        [HttpPost]
        public ActionResult GetInfoInventoryOutQR(string objectQRMix)
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
                #region Kết nối Inbrand
                var gwUserCode = System.Configuration.ConfigurationManager.AppSettings["Inbrand_GwUserCode"];
                var gwPassword = System.Configuration.ConfigurationManager.AppSettings["Inbrand_GwPassword"];
                if (ServiceAddress.BaseAPIInbrandSolution == null || ServiceAddress.BaseAPIInbrandSolution == "")
                {
                    var strNetWorkUrl = "";
                    string strMstSvUrl_InBrand = System.Configuration.ConfigurationManager.AppSettings["InbrandMstSv"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["InbrandMstSv"];

                    MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

                    /////
                    RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                    {
                        Tid = GetNextTId(),
                        TokenID = strMstSvUrl_InBrand,
                        NetworkID = networkID,
                        OrgID = orgId.ToString(),
                        GwUserCode = gwUserCode,
                        GwPassword = gwPassword,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword
                    };
                    var objRT_MstSv_Sys_User = OS_MstSv_InBrandServices.Instance.WA_OS_MstSv_InBrand_MstSv_Sys_User_Login(strMstSvUrl_InBrand, objRQ_MstSv_Sys_User);
                    strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                    if (strNetWorkUrl != null && strNetWorkUrl != "")
                    {
                        ServiceAddress.BaseAPIInbrandSolution = strNetWorkUrl;
                    }
                }
                var lstRpt_Inv_InventoryVerifiedIDForSearch = new List<Rpt_Inv_InventoryVerifiedIDForSearch>();

                if (ServiceAddress.BaseAPIInbrandSolution != null && ServiceAddress.BaseAPIInbrandSolution != "")
                {
                    var objRQ_Rpt_Inv_InventoryVerifiedIDForSearch = new RQ_Rpt_Inv_InventoryVerifiedIDForSearch()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = gwUserCode,
                        GwPassword = gwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        ObjectQRMix = objectQRMix
                    };
                    var objRT_Rpt_Inv_InventoryVerifiedIDForSearch = Report_InbrandService.Instance.Rpt_Inv_InventoryVerifiedIDForSearch(ServiceAddress.BaseAPIInbrandSolution, objRQ_Rpt_Inv_InventoryVerifiedIDForSearch);
                    if (objRT_Rpt_Inv_InventoryVerifiedIDForSearch != null && objRT_Rpt_Inv_InventoryVerifiedIDForSearch.Lst_Rpt_Inv_InventoryBalanceSerialForSearch != null)
                    {
                        lstRpt_Inv_InventoryVerifiedIDForSearch = objRT_Rpt_Inv_InventoryVerifiedIDForSearch.Lst_Rpt_Inv_InventoryBalanceSerialForSearch;
                    }
                    return Json(new { Success = true, data = lstRpt_Inv_InventoryVerifiedIDForSearch });
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

        [HttpPost]
        public ActionResult GetProductSearch(string productkey, string showview, string productcodeuser, string autosearch = "false", string flagquetmavach = "")
        {
            if (showview != null && showview == "1")
            {
                return JsonView();
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
                var sbSql = new StringBuilder();
                var tblMstProduct = "Mst_Product.";
                var strWhereProduct = "";
                if (flagquetmavach.Equals("0"))
                {
                    if (productkey != null && productkey != "")
                    {
                        strWhereProduct += "(Mst_Product.ProductCodeUser like %'" + productkey + "'% OR Mst_Product.ProductName like %'" + productkey + "'%)";
                    }
                }
                else
                {
                    if (productkey != null && productkey != "")
                    {
                        //strWhereProduct += "(Mst_Product.ProductCodeUser ='" + productkey + "')";
                        strWhereProduct += "(Mst_Product.ProductCodeUser like %'" + productkey + "'% OR Mst_Product.ProductName like %'" + productkey + "'%)";
                    }
                }
                if (!string.IsNullOrEmpty(productcodeuser))
                {
                    sbSql.AddWhereClause(tblMstProduct + "ProductCodeUser", CUtils.StrValue(productcodeuser), "=");
                }
                sbSql.AddWhereClause(tblMstProduct + "FlagActive", "1", "=");
                sbSql.AddWhereClause(tblMstProduct + "FlagSell", "1", "=");
                sbSql.AddWhereClause(tblMstProduct + "ProductType", "'PRODUCT,COMBO'", "IN");


                if (strWhereProduct == "")
                {
                    strWhereProduct = sbSql.ToString();
                }
                else
                {
                    strWhereProduct += " AND " + sbSql.ToString();
                }
                #region Thông tin hàng hóa
                var strCountFix = "10";
                var lst_Mst_Product = new List<Mst_Product>();
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
                    Rt_Cols_Mst_Product = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = strCountFix,
                    Ft_WhereClause = strWhereProduct
                };
                var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                {
                    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                }
                #endregion

                #region Lấy thông tin tồn kho
                var lst_StrProductCode = new List<string>();
                if (lst_Mst_Product.Count != 0)
                {
                    foreach (var it in lst_Mst_Product)
                    {
                        if (!lst_StrProductCode.Contains(it.ProductCodeBase))
                        {
                            lst_StrProductCode.Add(it.ProductCodeBase.ToString());
                        }
                    }
                }
                //var strWhereClause = "";
                //var sb_SQL = new StringBuilder();
                //var lstProductCode = "";
                //if (lst_StrProductCode.Count != 0)
                //{
                //    foreach (var productcode in lst_StrProductCode)
                //    {
                //        if (lstProductCode == "")
                //        {
                //            lstProductCode += productcode;
                //        }
                //        else
                //        {
                //            lstProductCode += ", " + productcode;
                //        }
                //    }
                //}
                //var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                //if (lstProductCode != "")
                //{
                //    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCode, "IN");
                //}
                //strWhereClause = sb_SQL.ToString();

                //var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
                //{
                //    // WARQBase
                //    Tid = GetNextTId(),
                //    GwUserCode = GwUserCode,
                //    GwPassword = GwPassword,
                //    AccessToken = CUtils.StrValue(UserState.AccessToken),
                //    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                //    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                //    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                //    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                //    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                //    Rt_Cols_Inv_InventoryBalance = "*",
                //    Ft_RecordStart = Ft_RecordStart,
                //    Ft_RecordCount = Ft_RecordCount,
                //    Ft_WhereClause = strWhereClause
                //};
                //var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                //var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                //if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                //{
                //    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                //}

                var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                foreach (var it in lst_Mst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductCodeUser = it.ProductCodeUser;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ValConvert = it.ValConvert;
                    if (it.FlagSerial.Equals("1"))
                    {
                        productUI.FlagSerial = "1";
                    }
                    if (it.FlagLot.Equals("1"))
                    {
                        productUI.FlagLo = "1";
                    }
                    if (it.ProductType != null && it.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                    {
                        productUI.FlagCombo = "1";
                    }
                    // Check theo đơn hàng hay không
                    productUI.SellPrice = it.UPSell == null ? 0 : it.UPSell;
                    //
                    //if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                    //{
                    //    var qtyTotalOkMax = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                    //    var itBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                    //    if (itBalance != null)
                    //    {
                    //        productUI.InvCode = itBalance.InvCode;
                    //        productUI.QtyTotalOK = qtyTotalOkMax;
                    //        productUI.DiscountPrice = "0"; // Tạm để
                    //    }
                    //}
                    lst_Mst_ProductUI.Add(productUI);
                }

                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                if (autosearch == "true")
                {
                    return Json(new { Success = true, data = lst_Mst_ProductUI });
                }
                else if (productcodeuser != null && productcodeuser != "")
                {
                    return Json(new { Success = true, data = lst_Mst_ProductUI[0] });
                }
                else
                {
                    return JsonView(lst_Mst_ProductUI);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProductExactly(string productCode, string InvBUPattern, string productCodeBase, string valconvert, string Qty, string FlagLo, string FlagSerial)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
            var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
            var resultEntry = new JsonResultEntry() { Success = false };
            var tbMst_Inventory = "Mst_Inventory.";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var objMST_ProductUI = new Mst_Product();
            #endregion
            try
            {
                #region["Thông tin tồn kho của hàng hoá chọn"]
                var qtytotalok = 0.0;
                var qtytotalok1 = 0.0;
                decimal upinv = 0;
                var invCodeMax = "";
                var invCodeMax1 = "";
                var qtymax = 0.0;
                var strWhereClauseTonKho = "";
                var tblInv_InventoryBalance12 = "Inv_InventoryBalance.";

                var sb_SQL12 = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL12.AddWhereClause(tblInv_InventoryBalance12 + "ProductCode", productCodeBase, "=");
                }

                if (InvBUPattern != null && InvBUPattern != "")
                {
                    sb_SQL12.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                strWhereClauseTonKho = sb_SQL12.ToString();


                var objRQ_Inv_InventoryBalanceTonKho = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseTonKho
                };

                var objRT_Inv_InventoryBalanceTonKho = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalanceTonKho, addressAPIs);
                var lstInv_InventoryBalanceTonKho = new List<Inv_InventoryBalance>();
                var ValConvert = 1.0;
                if (valconvert != null && valconvert != "")
                {
                    ValConvert = Convert.ToDouble(valconvert);
                }

                if (objRT_Inv_InventoryBalanceTonKho != null && objRT_Inv_InventoryBalanceTonKho.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalanceTonKho.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalanceTonKho = objRT_Inv_InventoryBalanceTonKho.Lst_Inv_InventoryBalance;
                    if (lstInv_InventoryBalanceTonKho.Count > 0)
                    {
                        qtymax = lstInv_InventoryBalanceTonKho.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        var itMax = lstInv_InventoryBalanceTonKho.Where(x => x.QtyTotalOK.Equals(qtymax)).FirstOrDefault();
                        if (itMax != null)
                        {
                            invCodeMax = itMax.InvCode == null ? "" : itMax.InvCode.ToString();
                        }
                        qtytotalok = lstInv_InventoryBalanceTonKho.Sum(x => x.QtyTotalOK == null ? 0.0 : Convert.ToDouble(x.QtyTotalOK));


                    }
                }
                // lấy giá trị sau khi quy đổi
                qtymax = qtymax / ValConvert;

                #endregion

                #region["Danh sách hàng hoá cùng base"]
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!CUtils.IsNullOrEmpty(productCodeBase))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(productCodeBase), "=");
                }
                strWhere = sbSql.ToString();
                var rq = new RQ_Mst_Product()
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
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Product = "*",
                };
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                {
                    #region["Lấy thông tin tồn kho"]
                    var listProductCode = objRT_Mst_Product.Lst_Mst_Product.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
                    if (listProductCode != null && listProductCode.Count > 0)
                    {
                        listProductCode = listProductCode.Distinct().ToList();
                        var strProductCode = string.Join(",", listProductCode);

                        var strWhereClause = "";
                        var sb_SQL1 = new StringBuilder();
                        var tblInv_InventoryBalance1 = "Inv_InventoryBalance.";
                        sb_SQL1.AddWhereClause(tblInv_InventoryBalance1 + "ProductCode", strProductCode, "IN");
                        if (InvBUPattern != null && InvBUPattern != "")
                        {
                            sb_SQL1.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                        }
                        strWhereClause = sb_SQL1.ToString();


                        var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                            Rt_Cols_Inv_InventoryBalance = "*",
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClause
                        };
                        var objRT_Inv_InventoryBalance1 = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                        var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                        var objInv_InventoryBalance1 = new Inv_InventoryBalance();
                        if (objRT_Inv_InventoryBalance1 != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance.Count > 0)
                        {
                            lstInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance;
                            objInv_InventoryBalance1 = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance[0];
                        }



                        foreach (var it in objRT_Mst_Product.Lst_Mst_Product)
                        {
                            var productUI = new Mst_ProductUI();
                            productUI.ProductCode = it.ProductCode;
                            productUI.ProductCodeUser = it.ProductCodeUser;
                            productUI.ProductCodeBase = it.ProductCodeBase;
                            productUI.ProductCodeRoot = it.ProductCodeRoot;
                            productUI.ProductName = it.ProductName;
                            productUI.UnitCode = it.UnitCode;
                            productUI.ValConvert = it.ValConvert;
                            productUI.FlagLot = it.FlagLot;
                            productUI.FlagSerial = it.FlagSerial;
                            productUI.ProductType = it.ProductType;

                            if (it.FlagLot.Equals("1"))
                            {
                                productUI.FlagLo = "1";
                            }
                            if (it.ProductType != null && it.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                            {
                                productUI.FlagCombo = "1";
                            }
                            // Check theo đơn hàng hay không
                            productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                            

                            #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]
                            var strWhereClauseTonKho1 = "";
                            var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                            var sb_SQL13 = new StringBuilder();
                            if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                            {
                                sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productCodeBase, "=");
                            }
                            if (InvBUPattern != null && InvBUPattern != "")
                            {
                                sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                            }
                            strWhereClauseTonKho1 = sb_SQL13.ToString();
                            var objRQ_Inv_InventoryBalanceTonKho1 = new RQ_Inv_InventoryBalance()
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
                                Rt_Cols_Inv_InventoryBalance = "*",
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = strWhereClauseTonKho1
                            };
                            var objRT_Inv_InventoryBalanceTonKho1 = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalanceTonKho1, addressAPIs);
                            var lstInv_InventoryBalanceTonKho1 = new List<Inv_InventoryBalance>();
                            if (objRT_Inv_InventoryBalanceTonKho1 != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance.Count > 0)
                            {
                                lstInv_InventoryBalanceTonKho1 = objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance;
                                if (lstInv_InventoryBalanceTonKho1.Count > 0)
                                {
                                    var objInv_InventoryBalance_ByProductCode = lstInv_InventoryBalanceTonKho1.Where(x => CUtils.StrValue(x.ProductCode).Equals(productUI.ProductCode)).FirstOrDefault();
                                    if(objInv_InventoryBalance_ByProductCode != null)
                                    {
                                        if (!CUtils.IsNullOrEmpty(objInv_InventoryBalance_ByProductCode.UPInv))
                                        {
                                            upinv = Convert.ToDecimal(objInv_InventoryBalance_ByProductCode.UPInv);
                                        }
                                        
                                    }
                                    var itMax = lstInv_InventoryBalanceTonKho1.Where(x => x.QtyTotalOK.Equals(qtymax)).FirstOrDefault();
                                    if (itMax != null)
                                    {
                                        invCodeMax1 = itMax.InvCode == null ? "" : itMax.InvCode.ToString();
                                    }
                                    qtytotalok1 = lstInv_InventoryBalanceTonKho1.Sum(x => x.QtyTotalOK == null ? 0.0 : Convert.ToDouble(x.QtyTotalOK));

                                    if (!CUtils.IsNullOrEmpty(productUI.ProductCodeBase) && !productUI.ProductCodeBase.Equals(productUI.ProductCode))
                                    {
                                        productUI.QtyTotalOK = Convert.ToDouble(qtytotalok1) / Convert.ToDouble(productUI.ValConvert);
                                    }
                                    else
                                    {
                                        productUI.QtyTotalOK = Convert.ToDouble(qtytotalok1);
                                    }
                                    productUI.DiscountPrice = "0";
                                    productUI.InvCode = invCodeMax1;
                                    productUI.InvCodeInActual = invCodeMax1;
                                    productUI.UPInv = upinv;
                                }
                            }
                            #endregion
                            
                            lst_Mst_ProductUI.Add(productUI);


                            objMST_ProductUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                            
                        }

                    }
                    #endregion

                    #region["Phân bổ"]

                    if (CUtils.StrValue(FlagSerial).Equals("1"))
                    {

                        var strWhere_Serial = "";
                        var sbSql_Serial = new StringBuilder();
                        var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                        if (!CUtils.IsNullOrEmpty(productCodeBase))
                        {
                            sbSql_Serial.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                        }
                        if (!CUtils.IsNullOrEmpty(InvBUPattern))
                        {
                            sbSql_Serial.AddWhereClause(tbMst_Inventory + "InvBUPattern", CUtils.StrValue(InvBUPattern), "like");
                        }
                        sbSql_Serial.AddWhereClause(tbMst_Inventory + "FlagActive", "1", "=");
                        strWhere_Serial = sbSql_Serial.ToString();
                        var rq_Serial = new RQ_Inv_InventoryBalanceSerial()
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
                            Ft_WhereClause = strWhere_Serial,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Rt_Cols_Inv_InventoryBalanceSerial = "*"
                        };


                        var rt_Serial = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq_Serial, addressAPIs);
                        if (rt_Serial != null && rt_Serial.Lst_Inv_InventoryBalanceSerial != null && rt_Serial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(Qty))
                            {
                                var qtyExport = Convert.ToDouble(Qty);
                                if (qtyExport >= rt_Serial.Lst_Inv_InventoryBalanceSerial.Count)
                                {
                                    lstInv_InventoryBalanceSerial = rt_Serial.Lst_Inv_InventoryBalanceSerial;
                                }
                                else
                                {
                                    int count = 0;
                                    foreach (var it in rt_Serial.Lst_Inv_InventoryBalanceSerial)
                                    {
                                        if (count >= qtyExport)
                                        {
                                            break;
                                        }

                                        lstInv_InventoryBalanceSerial.Add(it);
                                        count++;
                                    }
                                }
                            }
                        }
                    }

                    else if (CUtils.StrValue(FlagLo).Equals("1"))
                    {
                        var strWhere_Lo = "";
                        var sbSql_Lo = new StringBuilder();
                        var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                        if (!CUtils.IsNullOrEmpty(productCodeBase))
                        {
                            sbSql_Lo.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                        }
                        if (!CUtils.IsNullOrEmpty(InvBUPattern))
                        {
                            sbSql_Lo.AddWhereClause(tbMst_Inventory + "InvBUPattern", CUtils.StrValue(InvBUPattern), "like");
                        }
                        sbSql_Lo.AddWhereClause(tbMst_Inventory + "FlagActive", "1", "=");
                        sbSql_Lo.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
                        strWhere_Lo = sbSql_Lo.ToString();

                        var rq_Lo = new RQ_Inv_InventoryBalanceLot()
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
                            Ft_WhereClause = strWhere_Lo,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Rt_Cols_Inv_InventoryBalanceLot = "*"
                        };
                        var lstInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                        var rt_Lot = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq_Lo, addressAPIs);
                        if (rt_Lot != null && rt_Lot.Lst_Inv_InventoryBalanceLot != null && rt_Lot.Lst_Inv_InventoryBalanceLot.Count > 0)
                        {
                            lstInv_InventoryBalanceLot = rt_Lot.Lst_Inv_InventoryBalanceLot;
                        }
                        var mstProduct = GetMstProduct(productCode);

                        double valConvert_Lot = 1;
                        if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                        {
                            valConvert_Lot = Convert.ToDouble(mstProduct.ValConvert);
                        }

                        //Tính lại tồn kho theo Đơn vị quy đổi
                        foreach (var lot in lstInv_InventoryBalanceLot)
                        {
                            lot.QtyTotalOK = lot.QtyTotalOK == null ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / valConvert_Lot, 2);
                            Inv_InventoryBalanceLotUI lotui = new Inv_InventoryBalanceLotUI
                            {
                                ProductCode = lot.ProductCode,
                                ProductLotNo = lot.ProductLotNo,
                                InvCode = lot.InvCode,
                                ProductionDate = lot.ProductionDate,
                                ExpiredDate = lot.ExpiredDate,
                                QtyTotalOK = lot.QtyTotalOK,
                                Qty = lot.QtyTotalOK
                            };
                            lstInv_InventoryBalanceLotView.Add(lotui);


                        }
                        if (!string.IsNullOrEmpty(Qty))
                        {
                            var qtyExport = Convert.ToDouble(Qty);
                            lstInv_InventoryBalanceLotView = lstInv_InventoryBalanceLotView.OrderByDescending(m => m.QtyTotalOK).ToList();
                            foreach (var lot in lstInv_InventoryBalanceLotView)
                            {
                                var qtyTotalOK = Convert.ToDouble(lot.QtyTotalOK);
                                if (qtyExport <= qtyTotalOK)
                                {
                                    lot.Qty = qtyExport;
                                    qtyExport = 0;
                                }
                                else
                                {
                                    lot.Qty = qtyTotalOK;
                                    qtyExport -= qtyTotalOK;
                                }
                            }
                        }
                    }
                    else
                    {
                        var mstProduct = GetMstProduct(productCode);
                        double valConvert_HH = 1;
                        if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                        {
                            valConvert_HH = Convert.ToDouble(mstProduct.ValConvert);
                        }
                        var valConvert = string.IsNullOrEmpty(valconvert) ? 1 : Convert.ToDouble(valConvert_HH);
                        var strWhereClause1 = "";
                        var tblInv_InventoryBalance = "Inv_InventoryBalance.";

                        var sb_SQL2 = new StringBuilder();
                        if (productCodeBase != null && productCodeBase != "")
                        {
                            sb_SQL2.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                        }
                        if (InvBUPattern != null && InvBUPattern != "")
                        {
                            sb_SQL2.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                        }
                        sb_SQL2.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                        strWhereClause1 = sb_SQL2.ToString();


                        var objRQ_Inv_InventoryBalance1 = new RQ_Inv_InventoryBalance()
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
                            Rt_Cols_Inv_InventoryBalance = "*",
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClause1
                        };
                        var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance1, addressAPIs);

                        if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                        {
                            lstInv_InventoryBalance1 = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;

                            if (!CUtils.IsNullOrEmpty(Qty))
                            {
                                var qtyExport = Convert.ToDouble(Qty);
                                lstInv_InventoryBalance1 = lstInv_InventoryBalance1.OrderByDescending(m => m.QtyTotalOK).ToList();
                                foreach (var it in lstInv_InventoryBalance1)
                                {
                                    it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);

                                    var qtyTotalOK = Convert.ToDouble(it.QtyTotalOK);
                                    if (qtyExport <= qtyTotalOK)
                                    {
                                        it.QtyAvailOK = qtyExport;
                                        qtyExport = 0;
                                    }
                                    else
                                    {
                                        it.QtyAvailOK = qtyTotalOK;
                                        qtyExport -= qtyTotalOK;
                                    }
                                }
                            }
                        }
                    }



                    #endregion


                }
                #endregion
                return Json(new { Success = true, Data = lst_Mst_ProductUI, List = lstInv_InventoryBalance1, List_PhanBoLot = lstInv_InventoryBalanceLotView, List_PhanBoSerial = lstInv_InventoryBalanceSerial, qtytotalok = Math.Round(qtytotalok / ValConvert, 2), invCodeMax = invCodeMax, qtyMax = qtymax, objMST_ProductUI = objMST_ProductUI });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        public ActionResult GetTonKhoPhanBoHH(string productCode, string invBUPattern, string productCodeBase, string valconvert)
        {

            var tbMst_Inventory = "Mst_Inventory.";
            var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
            var Qty = 1;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var mstProduct = GetMstProduct(productCode);
                double valConvert_HH = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert_HH = Convert.ToDouble(mstProduct.ValConvert);
                }
                var valConvert = string.IsNullOrEmpty(valconvert) ? 1 : Convert.ToDouble(valConvert_HH);
                var strWhereClause1 = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";

                var sb_SQL2 = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL2.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (invBUPattern != null && invBUPattern != "")
                {
                    sb_SQL2.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
                }
                sb_SQL2.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause1 = sb_SQL2.ToString();


                var objRQ_Inv_InventoryBalance1 = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause1
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance1, UserState.AddressAPIs);

                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalance1 = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;

                    if (!CUtils.IsNullOrEmpty(Qty))
                    {
                        var qtyExport = Convert.ToDouble(Qty);
                        lstInv_InventoryBalance1 = lstInv_InventoryBalance1.OrderByDescending(m => m.QtyTotalOK).ToList();
                        foreach (var it in lstInv_InventoryBalance1)
                        {
                            it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);

                            var qtyTotalOK = Convert.ToDouble(it.QtyTotalOK);
                            if (qtyExport <= qtyTotalOK)
                            {
                                it.QtyAvailOK = qtyExport;
                                qtyExport = 0;
                            }
                            else
                            {
                                it.QtyAvailOK = qtyTotalOK;
                                qtyExport -= qtyTotalOK;
                            }
                        }
                    }
                }
                return Json(new { Success = true, List = lstInv_InventoryBalance1 });
            }


            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }


        [HttpPost]
        public ActionResult GetPhanBoLot(string productCodeBase, string InvBUPattern, string productCode, string Qty)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
            var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
            var resultEntry = new JsonResultEntry() { Success = false };
            var tbMst_Inventory = "Mst_Inventory.";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var objMST_ProductUI = new Mst_Product();
            #endregion
            try
            {
                var strWhere_Lo = "";
                var sbSql_Lo = new StringBuilder();
                var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                if (!CUtils.IsNullOrEmpty(productCodeBase))
                {
                    sbSql_Lo.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!CUtils.IsNullOrEmpty(InvBUPattern))
                {
                    sbSql_Lo.AddWhereClause(tbMst_Inventory + "InvBUPattern", CUtils.StrValue(InvBUPattern), "like");
                }
                sbSql_Lo.AddWhereClause(tbMst_Inventory + "FlagActive", "1", "=");
                sbSql_Lo.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
                strWhere_Lo = sbSql_Lo.ToString();

                var rq_Lo = new RQ_Inv_InventoryBalanceLot()
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
                    Ft_WhereClause = strWhere_Lo,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceLot = "*"
                };
                var lstInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                var rt_Lot = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq_Lo, addressAPIs);
                if (rt_Lot != null && rt_Lot.Lst_Inv_InventoryBalanceLot != null && rt_Lot.Lst_Inv_InventoryBalanceLot.Count > 0)
                {
                    lstInv_InventoryBalanceLot = rt_Lot.Lst_Inv_InventoryBalanceLot;
                }
                var mstProduct = GetMstProduct(productCode);

                double valConvert_Lot = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert_Lot = Convert.ToDouble(mstProduct.ValConvert);
                }

                //Tính lại tồn kho theo Đơn vị quy đổi
                foreach (var lot in lstInv_InventoryBalanceLot)
                {
                    lot.QtyTotalOK = lot.QtyTotalOK == null ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / valConvert_Lot, 2);
                    Inv_InventoryBalanceLotUI lotui = new Inv_InventoryBalanceLotUI
                    {
                        ProductCode = lot.ProductCode,
                        ProductLotNo = lot.ProductLotNo,
                        InvCode = lot.InvCode,
                        ProductionDate = lot.ProductionDate,
                        ExpiredDate = lot.ExpiredDate,
                        QtyTotalOK = lot.QtyTotalOK,
                        Qty = lot.QtyTotalOK
                    };
                    lstInv_InventoryBalanceLotView.Add(lotui);


                }
                if (!string.IsNullOrEmpty(Qty))
                {
                    var qtyExport = Convert.ToDouble(Qty);
                    lstInv_InventoryBalanceLotView = lstInv_InventoryBalanceLotView.OrderByDescending(m => m.QtyTotalOK).ToList();
                    foreach (var lot in lstInv_InventoryBalanceLotView)
                    {
                        var qtyTotalOK = Convert.ToDouble(lot.QtyTotalOK);
                        if (qtyExport <= qtyTotalOK)
                        {
                            lot.Qty = qtyExport;
                            qtyExport = 0;
                        }
                        else
                        {
                            lot.Qty = qtyTotalOK;
                            qtyExport -= qtyTotalOK;
                        }
                    }
                }

                return Json(new { Success = true, List_PhanBoLot = lstInv_InventoryBalanceLotView });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        public ActionResult GetPhanBoSerial(string productCodeBase, string InvBUPattern, string productCode, string Qty)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
            var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
            var resultEntry = new JsonResultEntry() { Success = false };
            var tbMst_Inventory = "Mst_Inventory.";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var objMST_ProductUI = new Mst_Product();
            #endregion
            try
            {
                var strWhere_Serial = "";
                var sbSql_Serial = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                if (!CUtils.IsNullOrEmpty(productCodeBase))
                {
                    sbSql_Serial.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                }
                if (!CUtils.IsNullOrEmpty(InvBUPattern))
                {
                    sbSql_Serial.AddWhereClause(tbMst_Inventory + "InvBUPattern", CUtils.StrValue(InvBUPattern), "like");
                }
                sbSql_Serial.AddWhereClause(tbMst_Inventory + "FlagActive", "1", "=");
                strWhere_Serial = sbSql_Serial.ToString();
                var rq_Serial = new RQ_Inv_InventoryBalanceSerial()
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
                    Ft_WhereClause = strWhere_Serial,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Inv_InventoryBalanceSerial = "*"
                };


                var rt_Serial = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq_Serial, addressAPIs);
                if (rt_Serial != null && rt_Serial.Lst_Inv_InventoryBalanceSerial != null && rt_Serial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Qty))
                    {
                        var qtyExport = Convert.ToDouble(Qty);
                        if (qtyExport >= rt_Serial.Lst_Inv_InventoryBalanceSerial.Count)
                        {
                            lstInv_InventoryBalanceSerial = rt_Serial.Lst_Inv_InventoryBalanceSerial;
                        }
                        else
                        {
                            int count = 0;
                            foreach (var it in rt_Serial.Lst_Inv_InventoryBalanceSerial)
                            {
                                if (count >= qtyExport)
                                {
                                    break;
                                }

                                lstInv_InventoryBalanceSerial.Add(it);
                                count++;
                            }
                        }
                    }
                }
                return Json(new { Success = true, List_PhanBoLot = lstInv_InventoryBalanceSerial });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        public ActionResult GetCustomerSearch(string customerkey, string showview)
        {
            if (showview != null && showview == "1")
            {
                return JsonView();
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
                #region Thông tin khách hàng
                var strWhere = "";
                var tblCustomer = "Mst_Customer.";
                var sbSql = new StringBuilder();
                sbSql.AddWhereClause(tblCustomer + TblMst_Customer.FlagActive, "1", "=");
                if (customerkey != null && customerkey != "")
                {
                    var sql1 = new StringBuilder();
                    var sql2 = new StringBuilder();
                    sql1.AddWhereClause(tblCustomer + TblMst_Customer.CustomerCode, "%" + customerkey + "%", "like");
                    sql2.AddWhereClause(tblCustomer + TblMst_Customer.CustomerName, "%" + customerkey + "%", "like");
                    strWhere = sbSql.ToString() + " AND (" + sql1.ToString() + " OR " + sql2.ToString() + ")";
                }
                else
                {
                    strWhere = sbSql.ToString();
                }

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
                    Ft_WhereClause = strWhere
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                //ViewBag.Lst_MstCustomer = lstCustomer;
                return JsonView(lstCustomer);
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

        [HttpPost]
        public ActionResult GetInvOutTypeSearch(string keyword, string showview)
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
                var strWhere = "";
                var tblMst_InvOutType = "Mst_InvOutType.";
                var sbSql = new StringBuilder();
                sbSql.AddWhereClause(tblMst_InvOutType + TblMst_InvOutType.FlagActive, "1", "=");
                if (keyword != null && keyword != "")
                {
                    var sql1 = new StringBuilder();
                    var sql2 = new StringBuilder();
                    sql1.AddWhereClause(tblMst_InvOutType + TblMst_InvOutType.InvOutType, keyword, "=");
                    sql2.AddWhereClause(tblMst_InvOutType + TblMst_InvOutType.InvOutTypeName, "%" + keyword + "%", "like");
                    strWhere = sbSql.ToString() + " AND (" + sql1.ToString() + " OR " + sql2.ToString() + ")";
                }
                else
                {
                    strWhere = sbSql.ToString();
                }
                #region Loại xuất kho
                var lst_Mst_InvOutType = new List<Mst_InvOutType>();
                var objRQ_Mst_InventoryOutType = new RQ_Mst_InvOutType()
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
                    Rt_Cols_Mst_InvOutType = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere
                };
                var rt = MstInvOutTypeService.Instance.WA_Mst_InventoryOutType_Get(objRQ_Mst_InventoryOutType, addressAPIs);
                if (rt != null && rt.Lst_Mst_InvOutType != null)
                {
                    lst_Mst_InvOutType = rt.Lst_Mst_InvOutType;
                }
                //ViewBag.Lst_Mst_InvOutType = lst_Mst_InvOutType;
                return JsonView(lst_Mst_InvOutType);
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

        [HttpPost]
        public ActionResult GetInvOutSearch(string invoutSearch, string showview, string selectid)
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
                ViewBag.selectid = selectid;
                var strWhere = "";
                var tbl_Mst_Inventory = "Mst_Inventory.";
                var sbSql = new StringBuilder();
                sbSql.AddWhereClause(tbl_Mst_Inventory + tblMst_Inventory.FlagActive, "1", "=");
                sbSql.AddWhereClause(tbl_Mst_Inventory + tblMst_Inventory.FlagIn_Out, "1", "=");
                if (invoutSearch != null && invoutSearch != "")
                {
                    var sql1 = new StringBuilder();
                    var sql2 = new StringBuilder();
                    sql1.AddWhereClause(tbl_Mst_Inventory + tblMst_Inventory.InvCode, invoutSearch, "=");
                    sql2.AddWhereClause(tbl_Mst_Inventory + tblMst_Inventory.InvName, "%" + invoutSearch + "%", "like");
                    strWhere = sbSql.ToString() + " AND (" + sql1.ToString() + " OR " + sql2.ToString() + ")";
                }
                else
                {
                    strWhere = sbSql.ToString();
                }
                #region Thông tin kho xuất
                var lst_Mst_Inventory = new List<Mst_Inventory>();
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
                    Rt_Cols_Mst_Inventory = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere
                };
                var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                {
                    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                }
                //ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
                return JsonView(lst_Mst_Inventory);

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

        [HttpPost]
        public ActionResult GetBalanceByProduct(string productCode, string InvBUPattern, string productCodeBase, string valconvert)
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
                var qtytotalok = 0.0;
                var invCodeMax = "";
                var qtymax = 0.0;
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                //if (productCode != null && productCode != "")
                //{
                //    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCode, "=");
                //}
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                var valConvert = 1.0;
                if (valconvert != null && valconvert != "")
                {
                    valConvert = Convert.ToDouble(valconvert);
                }
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    if (lstInv_InventoryBalance.Count != 0)
                    {
                        qtymax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        var itMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK.Equals(qtymax)).FirstOrDefault();
                        if (itMax != null)
                        {
                            //qtytotalok = qtymax;
                            invCodeMax = itMax.InvCode == null ? "" : itMax.InvCode.ToString();
                        }

                        //var lstInvCode = "";
                        qtytotalok = lstInv_InventoryBalance.Sum(x => x.QtyTotalOK == null ? 0.0 : Convert.ToDouble(x.QtyTotalOK));
                        //foreach(var it in lstInv_InventoryBalance)
                        //{
                        //    if(lstInvCode == "")
                        //    {
                        //        lstInvCode += it.InvCode == null ? "" : it.InvCode.ToString();
                        //    }
                        //    else
                        //    {
                        //        lstInvCode += ", "+ (it.InvCode == null ? "" : it.InvCode.ToString());
                        //    }
                        //}
                        //invCodeMax = "";//lstInvCode; // Mặc định trống để người dùng chọn
                    }
                }
                // Lấy giá trị sau quy đổi                
                qtymax = qtymax / valConvert;
                //
                return Json(new { Success = true, qtytotalok = Math.Round(qtytotalok / valConvert, 2), invCodeMax = invCodeMax, qtyMax = qtymax });
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

        [HttpPost]
        public ActionResult GetTonKho(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName)
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
                var valConvert = ValConvert == null ? 0 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);
                    }
                }
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.ProductCode = productCode;

                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                return JsonView("ShowTonKho", lstInv_InventoryBalance);
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

        [HttpPost]
        public ActionResult GetTonKhoPhanBo(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string qty)
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
                //var valConvert = ValConvert == null ? 0 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var mstProduct = GetMstProduct(productCode);
                double valConvert_HH = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert_HH = Convert.ToDouble(mstProduct.ValConvert);
                }
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[0].QtyTotalOK = 10000;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[1].QtyTotalOK = 500;
                    var ordersOrderedByDate = CUtils.OrderByDesc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert_HH, 2);
                    }
                }
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.ProductCode = productCode;

                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                ViewBag.Qty = qty;
                return JsonView("ShowTonKhoPhanBo", lstInv_InventoryBalance);
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

        [HttpPost]
        public ActionResult GetTonKho123(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string listdetail, string ViewType)
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
                var valConvert = ValConvert == null ? 1 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);
                        it.QtyAvailOK = it.QtyTotalOK;
                    }
                }

                List<InvF_InventoryReturnSupDtl> lst_InvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSupDtl>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryReturnSupDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSupDtl>>(listdetail);
                }
                if (lst_InvF_InventoryReturnSupDtl.Count > 0)
                {
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        var lstDtl = lst_InvF_InventoryReturnSupDtl.Where(m => Convert.ToString(m.InvCodeOutActual) == Convert.ToString(it.InvCode)).ToList();
                        if (lstDtl.Count > 0)
                        {
                            it.QtyAvailOK = lstDtl[0].Qty; //Dùng tạm QtyAvailOK để view SL xuất
                        }
                        else
                        {
                            it.QtyAvailOK = 0;
                        }
                    }
                }

                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.ProductCode = productCode;

                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                ViewBag.ViewType = ViewType;
                return JsonView("ShowTonKhoPhanBo", lstInv_InventoryBalance);
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

        [HttpPost]
        public ActionResult GetTonKhoPhanBo1(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string qty)
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
                //var valConvert = ValConvert == null ? 0 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var mstProduct = GetMstProduct(productCode);
                double valConvert_HH = 1;
                if (!CUtils.IsNullOrEmpty(mstProduct.ValConvert))
                {
                    valConvert_HH = Convert.ToDouble(mstProduct.ValConvert);
                }
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[0].QtyTotalOK = 10000;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[1].QtyTotalOK = 500;
                    var ordersOrderedByDate = CUtils.OrderByDesc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert_HH, 2);
                    }
                }
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.ProductCode = productCode;

                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                ViewBag.Qty = qty;
                return Json(new { Success = true, Data = lstInv_InventoryBalance });
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

        [HttpPost]
        public ActionResult Get_TonKho_PhanBo(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string qty)
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
                var valConvert = ValConvert == null ? 0 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[0].QtyTotalOK = 10000;
                    //objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[1].QtyTotalOK = 500;
                    var ordersOrderedByDate = CUtils.OrderByDesc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
                    foreach (var it in lstInv_InventoryBalance)
                    {
                        it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);
                    }
                }
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.ProductCode = productCode;

                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                ViewBag.Qty = qty;
                var data = new
                {
                    Success = true,
                    ListInventoryBalance = lstInv_InventoryBalance
                };
                return Json(data, JsonRequestBehavior.AllowGet);
                //return JsonView("ShowTonKhoPhanBo", lstInv_InventoryBalance);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTonKhoTemp(string productCodeBase, string InvBUPattern, string ValConvert, string Qty)
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
                var valConvert = string.IsNullOrEmpty(ValConvert) ? 1 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;

                    #region Đơn vị quy đổi + Phân bổ số lượng 

                    if (!string.IsNullOrEmpty(Qty))
                    {
                        var qtyExport = Convert.ToDouble(Qty);
                        lstInv_InventoryBalance = lstInv_InventoryBalance.OrderByDescending(m => m.QtyTotalOK).ToList();
                        foreach (var it in lstInv_InventoryBalance)
                        {
                            it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);

                            var qtyTotalOK = Convert.ToDouble(it.QtyTotalOK);
                            if (qtyExport <= qtyTotalOK)
                            {
                                it.QtyAvailOK = qtyExport;
                                qtyExport = 0;
                            }
                            else
                            {
                                it.QtyAvailOK = qtyTotalOK;
                                qtyExport -= qtyTotalOK;
                            }
                        }
                    }

                    #endregion
                }

                return Json(new { Success = true, Data = lstInv_InventoryBalance });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTonKhoTemp_ImportExcel(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string Qty, string strListDtlImport)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var listDtlImport = new List<InvF_InventoryOutDtlUI>();
            if (!CUtils.IsNullOrEmpty(strListDtlImport))
            {
                listDtlImport = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOutDtlUI>>(strListDtlImport);
            }
            if (listDtlImport == null || listDtlImport.Count == 0)
            {
                listDtlImport = new List<InvF_InventoryOutDtlUI>();
            }
            #endregion
            try
            {
                var valConvert = string.IsNullOrEmpty(ValConvert) ? 1 : Convert.ToDouble(ValConvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
                strWhereClause = sb_SQL.ToString();

                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;

                    #region Đơn vị quy đổi + Phân bổ số lượng 

                    if (listDtlImport != null && listDtlImport.Count > 0)
                    {
                        //var qtyExport = Convert.ToDouble(Qty);
                        lstInv_InventoryBalance = lstInv_InventoryBalance.OrderByDescending(m => m.QtyTotalOK).ToList();
                        foreach (var it in lstInv_InventoryBalance)
                        {
                            var objProduct = listDtlImport.Where(_it => !CUtils.IsNullOrEmpty(_it.ProductCode) && !CUtils.IsNullOrEmpty(_it.InvCodeOutActual) && CUtils.StrValue(_it.InvCodeOutActual).Equals(it.InvCode) && CUtils.StrValue(_it.ProductCode).Equals(it.ProductCode)).FirstOrDefault();
                            decimal qtyExport = 0;
                            if (objProduct != null && !CUtils.IsNullOrEmpty(objProduct.Qty) && CUtils.IsNumeric(objProduct.Qty))
                            {
                                qtyExport = Convert.ToDecimal(objProduct.Qty);
                            }
                            it.QtyTotalOK = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valConvert, 2);

                            var qtyTotalOK = Convert.ToDecimal(it.QtyTotalOK);
                            if (qtyExport <= qtyTotalOK)
                            {
                                it.QtyAvailOK = qtyExport;
                                qtyExport = 0;
                            }
                            else
                            {
                                it.QtyAvailOK = qtyTotalOK;
                                qtyExport -= qtyTotalOK;
                            }
                        }
                    }

                    #endregion
                }

                return Json(new { Success = true, Data = lstInv_InventoryBalance });
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


        #region Excel
        #region Serial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateSerial()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateSerial();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalanceSerial).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv_InventoryBalanceSerial, dicColNames, filePath, string.Format("Inv_InventoryBalanceSerial"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        private Dictionary<string, string> GetImportDicColumsTemplateSerial()
        {
            return new Dictionary<string, string>()
            {

                 {"ProductCodeUser","Mã hàng hóa(*)"},
                  {"SerialNo","Số Serial(*)"},
                //{"InvCodeOutActual", "Vị trí(*)" },
            };
        }
        #endregion

        #region Lô
        public ActionResult ExportTemplateLo()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateLo();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalanceLot).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv_InventoryBalanceLot, dicColNames, filePath, string.Format("Inv_InventoryBalanceLot"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        private Dictionary<string, string> GetImportDicColumsTemplateLo()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"ProductLotNo","Số Lô(*)"},
                 //{"InvCodeOutActual","Vị trí"},
                 {"Qty","Số lượng(*)"},
            };
        }
        public ActionResult ExportTemplateLo1()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateLo1();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalanceLot).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv_InventoryBalanceLot, dicColNames, filePath, string.Format("Inv_InventoryBalanceLot"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        private Dictionary<string, string> GetImportDicColumsTemplateLo1()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"ProductLotNo","Số Lô(*)"},
                   {"Qty","Số lượng(*)"},
                 {"InvCodeOutActual","Vị trí"},
               
            };
        }


        #endregion

        #region Thông tin xác thực
        public ActionResult ExportTemplateQR()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryOutQR = new List<InvF_InventoryOutQR>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateQR();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryOutQR).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInvF_InventoryOutQR, dicColNames, filePath, string.Format("InvF_InventoryOutQR"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        private Dictionary<string, string> GetImportDicColumsTemplateQR()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa"},
                 {"QRCode","Mã xác thực"},
                 {"BoxNo","Mã hộp"},
                 {"CanNo","Mã thùng"}
            };
        }
        #endregion



        #endregion

    }
}