using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
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
    public class ReportsController : AdminBaseController
    {
        // GET: Reports
        #region ["Báo cáo nhập kho"]
        public ActionResult RptInvF_InventoryInFGSum(string partcode = "", string fromdate = "", string todate = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.Offset = Offset;
            var listData = new List<Rpt_InvFInventoryInFGSum>();

            #region ["Danh sách sản phẩm"]
            var objMst_Part = new Mst_Part()
            {
                FlagActive = "1",
            };
            if (partcode.Equals("Tất cả"))
            {
                partcode = "";
            }
            var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                Ft_WhereClause = strWhereClause_MstPart,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_InvF_InventoryOutFG
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var listPart = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            ViewBag.ListPart = listPart.Lst_Mst_Part;
            #endregion

            if (init != "init")
            {
                var objRQ_Rpt_InvFInventoryInFGSum = new RQ_Rpt_InvFInventoryInFGSum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,

                    PartCode = partcode,
                    ApprDTimeUTCTo = todate + " 23:59:59",
                    ApprDTimeUTCFrom = fromdate + " 00:00:00",
                };
                var objRT_Rpt_InvFInventoryInFGSum = ReportsService.Instance.WA_Rpt_InvFInventoryInFGSum(objRQ_Rpt_InvFInventoryInFGSum, addressAPIs);
                if (objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum != null && objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum.Count > 0)
                {
                    listData.AddRange(objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum);
                }
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
            }
            ViewBag.PartCode = partcode;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;

            return View(listData);
        }
        public ActionResult ExportRptInvF_InventoryInFGSum(string partcode = "", string fromdate = "", string todate = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                string url = "";
                var itemCount = 0;
                var pageCount = 0;
                var offset = Offset;
                var listData = new List<Rpt_InvFInventoryInFGSum>();

                #region["Search"]
                var objRQ_Rpt_InvFInventoryInFGSum = new RQ_Rpt_InvFInventoryInFGSum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,

                    PartCode = partcode,
                    ApprDTimeUTCTo = todate + " 23:59:59",
                    ApprDTimeUTCFrom = fromdate + " 00:00:00",
                };
                var objRT_Rpt_InvFInventoryInFGSum = ReportsService.Instance.WA_Rpt_InvFInventoryInFGSum(objRQ_Rpt_InvFInventoryInFGSum, addressAPIs);
                #endregion

                if (objRT_Rpt_InvFInventoryInFGSum != null && objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum != null)
                {
                    if (objRT_Rpt_InvFInventoryInFGSum.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Rpt_InvFInventoryInFGSum.MySummaryTable.MyCount);
                    }
                    if (objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum != null && objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listData.AddRange(objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum);

                    Dictionary<string, string> dicColNames = GetImportDicColumsBCNK();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_InvFInventoryInFGSum).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvFInventoryInFGSum"));


                    #region["Export các trang còn lại"]
                    listData.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Rpt_InvFInventoryInFGSum.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRQ_Rpt_InvFInventoryInFGSumExportCur = ReportsService.Instance.WA_Rpt_InvFInventoryInFGSum(objRQ_Rpt_InvFInventoryInFGSum, addressAPIs);
                            if (objRQ_Rpt_InvFInventoryInFGSumExportCur != null && objRQ_Rpt_InvFInventoryInFGSumExportCur.Lst_Rpt_InvFInventoryInFGSum != null && objRQ_Rpt_InvFInventoryInFGSumExportCur.Lst_Rpt_InvFInventoryInFGSum.Count > 0)
                            {
                                listData.AddRange(objRQ_Rpt_InvFInventoryInFGSumExportCur.Lst_Rpt_InvFInventoryInFGSum);
                                ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvFInventoryInFGSum" + i));
                                listData.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsBCNK()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho"},
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"TotalQtyIn","Số lượng nhập"},
            };
        }
        #endregion
        #endregion

        #region ["Báo cáo xuất kho"]
        public ActionResult RptInvF_InventoryOutFGSum(string mst = "", string partcode = "", string fromdate = "", string todate = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.Offset = Offset;
            var waMST = userState.SysUser.MST;
            var listData = new List<Rpt_InvFInventoryOutFGSum>();

            #region ["Danh sách sản phẩm"]
            var objMst_Part = new Mst_Part()
            {
                FlagActive = "1",
            };
            if (partcode.Equals("Tất cả"))
            {
                partcode = "";
            }
            var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                Ft_WhereClause = strWhereClause_MstPart,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_InvF_InventoryOutFG
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var listPart = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            ViewBag.ListPart = listPart.Lst_Mst_Part;
            #endregion

            #region ["Danh sách NNT"]
            var objRT_Mst_NNTCur = new RT_Mst_NNT();
            
            if (!CUtils.IsNullOrEmpty(waMST))
            {
                var objMst_NNTCur = new Mst_NNT()
                {
                    MST = waMST,
                    FlagActive = FlagActive,
                };
                var listMst_NNTCur = new List<Mst_NNT>();
                var strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
                listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));

                var objMst_NNTCurChil = new Mst_NNT()
                {
                    MSTParent = waMST,
                    FlagActive = FlagActive,
                };
                var strWhereClauseMst_NNTCurChil = strWhereClause_Mst_NNTCurChil(objMst_NNTCurChil);
                listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCurChil));

                ViewBag.ListMst_NNTCur = listMst_NNTCur;
                #endregion
            }
            if (init != "init")
            {
                var objRQ_Rpt_InvFInventoryOutFGSum = new RQ_Rpt_InvFInventoryOutFGSum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    ApprDTimeUTCFrom = fromdate + " 00:00:00",
                    ApprDTimeUTCTo = todate + " 23:59:59",
                    AgentCode = mst,
                    PartCode = partcode,
                };
                var objRT_Rpt_InvFInventoryOutFGSum = ReportsService.Instance.WA_Rpt_InvFInventoryOutFGSum(objRQ_Rpt_InvFInventoryOutFGSum, addressAPIs);
                if (objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum != null && objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum.Count > 0)
                {
                    listData.AddRange(objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum);
                }
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
            }
            ViewBag.MST = mst;
            ViewBag.PartCode = partcode;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;

            return View(listData);
        }
        public ActionResult ExportRptInvF_InventoryOutFGSum(string mst = "", string partcode = "", string fromdate = "", string todate = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                string url = "";
                var itemCount = 0;
                var pageCount = 0;
                var offset = Offset;
                var listData = new List<Rpt_InvFInventoryOutFGSum>();

                #region["Search"]
                var objRpt_InvFInventoryOutFGSum = new Rpt_InvFInventoryOutFGSum()
                {
                    PartCode = partcode,
                };
                var objRQ_Rpt_InvFInventoryOutFGSum = new RQ_Rpt_InvFInventoryOutFGSum()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,

                    ApprDTimeUTCFrom = fromdate + " 00:00:00",
                    ApprDTimeUTCTo = todate + " 23:59:59",
                    AgentCode = mst,
                    PartCode = partcode,
                };
                var objRT_Rpt_InvFInventoryOutFGSum = ReportsService.Instance.WA_Rpt_InvFInventoryOutFGSum(objRQ_Rpt_InvFInventoryOutFGSum, addressAPIs);
                #endregion

                if (objRT_Rpt_InvFInventoryOutFGSum != null && objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum.Count > 0)
                {
                    if (objRT_Rpt_InvFInventoryOutFGSum.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Rpt_InvFInventoryOutFGSum.MySummaryTable.MyCount);
                    }
                    if (objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum != null && objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listData.AddRange(objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum);

                    Dictionary<string, string> dicColNames = GetImportDicColumsBCXK();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_InvFInventoryOutFGSum).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvFInventoryOutFGSum"));


                    #region["Export các trang còn lại"]
                    listData.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Rpt_InvFInventoryOutFGSum.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRQ_Rpt_InvFInventoryOutFGSumExportCur = ReportsService.Instance.WA_Rpt_InvFInventoryOutFGSum(objRQ_Rpt_InvFInventoryOutFGSum, addressAPIs);
                            if (objRQ_Rpt_InvFInventoryOutFGSumExportCur != null && objRQ_Rpt_InvFInventoryOutFGSumExportCur.Lst_Rpt_InvFInventoryOutFGSum != null && objRQ_Rpt_InvFInventoryOutFGSumExportCur.Lst_Rpt_InvFInventoryOutFGSum.Count > 0)
                            {
                                listData.AddRange(objRQ_Rpt_InvFInventoryOutFGSumExportCur.Lst_Rpt_InvFInventoryOutFGSum);
                                ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvFInventoryOutFGSum" + i));
                                listData.Clear();
                            }
                        }
                    }
                    #endregion
                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }

            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsBCXK()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho"},
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"AgentName","Tên tổ chức"},
                 {"TotalQtyOut","Số lượng"},
            };
        }
        #endregion
        #endregion

        #region ["Báo cáo tồn kho"]
        public ActionResult RptInv_BalanceSerial(string partcode = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.Offset = Offset;
            var listData = new List<Inv_InventoryBalance>();
            #region ["Danh sách sản phẩm"]
            var objMst_Part = new Mst_Part()
            {
                FlagActive = "1",
            };
            if(partcode.Equals("Tất cả"))
            {
                partcode = "";
            }
            var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                Ft_WhereClause = strWhereClause_MstPart,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_InvF_InventoryOutFG
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var listPart = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            ViewBag.ListPart = listPart.Lst_Mst_Part;
            #endregion

            if (init != "init")
            {
                var obj_Inv_InventoryBalance = new Inv_InventoryBalance()
                {
                    PartCode = partcode,
                };
                var strWhereClauseInv_InventoryBalance = strWhereClause_Inv_InventoryBalance(obj_Inv_InventoryBalance);
                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClauseInv_InventoryBalance,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Inv_InventoryBalance = null,
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                if (objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                {
                    listData.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance);
                }
            }
            else
            {
                init = "search";
            }
            ViewBag.PartCode = partcode;

            return View(listData);
        }
        public ActionResult ExportRptInv_BalanceSerial(string partcode = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                string url = "";
                var itemCount = 0;
                var pageCount = 0;
                var offset = Offset;
                var listData = new List<Inv_InventoryBalance>();
                #region[list part]
                var objMst_Part = new Mst_Part()
                {
                    FlagActive = "1",
                };
                var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClause_MstPart,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_Mst_Part = "*",
                    Mst_Part = null,
                };
                var listPart = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
                #endregion
                //listpart.AddRange(listPart.Lst_Mst_Part);
                #region["Search"]
                var obj_Inv_InventoryBalance = new Inv_InventoryBalance()
                {
                    PartCode = partcode,
                };
                var strWhereClauseInv_InventoryBalance = strWhereClause_Inv_InventoryBalance(obj_Inv_InventoryBalance);
                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClauseInv_InventoryBalance,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_Inv_InventoryBalance = "*",
                    Inv_InventoryBalance = null,
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                {
                    if (objRT_Inv_InventoryBalance.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Inv_InventoryBalance.MySummaryTable.MyCount);
                    }
                    if (objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listData.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance);
                    var list = new List<Inv_InventoryBalanceUI>();
                    if (objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                    {
                        var listCur = new List<Inv_InventoryBalanceUI>();
                        foreach (var item in objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance)
                        {
                            var obj = new Inv_InventoryBalanceUI();
                            var objpart = listPart.Lst_Mst_Part.FirstOrDefault(it => !CUtils.IsNullOrEmpty(it.PartCode) && !CUtils.IsNullOrEmpty(item.PartCode));
                            obj.PartName = objpart.PartName;
                            obj.InvCode = item.InvCode;
                            obj.PartCode = item.PartCode;
                            obj.QtyPlanTotal = item.QtyPlanTotal;
                            listCur.Add(obj);
                        }
                        list.AddRange(listCur);
                    }
                    Dictionary<string, string> dicColNames = GetImportDicColumsBCTK();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalance).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Inv_InventoryBalance"));


                    #region["Export các trang còn lại"]
                    listData.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Inv_InventoryBalance.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRQ_Inv_InventoryBalanceExportCur = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                            if (objRQ_Inv_InventoryBalanceExportCur != null && objRQ_Inv_InventoryBalanceExportCur.Lst_Inv_InventoryBalance != null && objRQ_Inv_InventoryBalanceExportCur.Lst_Inv_InventoryBalance.Count > 0)
                            {
                                listData.AddRange(objRQ_Inv_InventoryBalanceExportCur.Lst_Inv_InventoryBalance);
                                ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Inv_InventoryBalance" + i));
                                listData.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }

                #endregion

            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsBCTK()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho"},
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"QtyPlanTotal","Số lượng tồn"},
            };
        }
        #endregion
        #endregion

        #region ["Báo cáo nhập - xuất -tồn"]
        public ActionResult RptInv_InventoryBalanceMonth(string frommonth = "", string tomonth = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.Offset = Offset;
            var listData = new List<Rpt_InvInventoryBalanceMonth>();

            if (init != "init")
            {
                var objRQ_Rpt_InvInventoryBalanceMonth = new RQ_Rpt_InvInventoryBalanceMonth()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,

                    InvBalMonthFrom = frommonth + "-01 00:00:00",
                    InvBalMonthTo = tomonth + "-01 23:59:59",

                };
                var objRT_Rpt_InvInventoryBalanceMonth = ReportsService.Instance.WA_Rpt_InvInventoryBalanceMonth(objRQ_Rpt_InvInventoryBalanceMonth, addressAPIs);
                if (objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth != null && objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth.Count > 0)
                {
                    listData.AddRange(objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth);
                }
            }
            else
            {
                init = "search";
                frommonth = MonthOfYear;
            }

            ViewBag.FromMonth = frommonth;
            ViewBag.ToMonth = tomonth;

            return View(listData);
        }
        public ActionResult ExportRptInv_InventoryBalanceMonth(string frommonth = "", string tomonth = "", string init = "init", int page = 0, int recordcount = 10)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                string url = "";
                var itemCount = 0;
                var pageCount = 0;
                var offset = Offset;
                var listData = new List<Rpt_InvInventoryBalanceMonth>();

                #region["Search"]
                var objRQ_Rpt_InvInventoryBalanceMonth = new RQ_Rpt_InvInventoryBalanceMonth()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,

                    InvBalMonthTo = tomonth + "-01 23:59:59",
                    InvBalMonthFrom = frommonth + "-01 00:00:00",
                };
                var objRT_Rpt_InvInventoryBalanceMonth = ReportsService.Instance.WA_Rpt_InvInventoryBalanceMonth(objRQ_Rpt_InvInventoryBalanceMonth, addressAPIs);
                #endregion

                if (objRT_Rpt_InvInventoryBalanceMonth != null && objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth != null)
                {
                    if (objRT_Rpt_InvInventoryBalanceMonth.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Rpt_InvInventoryBalanceMonth.MySummaryTable.MyCount);
                    }
                    if (objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth != null && objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listData.AddRange(objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth);

                    Dictionary<string, string> dicColNames = GetImportDicColumsBCXNT();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Rpt_InvInventoryBalanceMonth).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvInventoryBalanceMonth"));


                    #region["Export các trang còn lại"]
                    listData.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Rpt_InvInventoryBalanceMonth.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRQ_Rpt_InvInventoryBalanceMonthExportCur = ReportsService.Instance.WA_Rpt_InvInventoryBalanceMonth(objRQ_Rpt_InvInventoryBalanceMonth, addressAPIs);
                            if (objRQ_Rpt_InvInventoryBalanceMonthExportCur != null && objRQ_Rpt_InvInventoryBalanceMonthExportCur.Lst_Rpt_InvInventoryBalanceMonth != null && objRQ_Rpt_InvInventoryBalanceMonthExportCur.Lst_Rpt_InvInventoryBalanceMonth.Count > 0)
                            {
                                listData.AddRange(objRQ_Rpt_InvInventoryBalanceMonthExportCur.Lst_Rpt_InvInventoryBalanceMonth);
                                ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Rpt_InvInventoryBalanceMonth" + i));
                                listData.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsBCXNT()
        {
            return new Dictionary<string, string>()
            {
                 {"PartCode","Mã vật tư"},
                 {"PartName","Tên vật tư"},
                 {"PartType","Mã loại vật tư"},
                 {"TotalQtyInvBegin","Tồn đầu"},
                 {"TotalQtyIn","Số lượng nhập"},
                 {"TotalQtyOut","Số lượng xuất"},
                 {"TotalQtyInvEnd","Tồn cuối"},
            };
        }
        #endregion
        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_Rpt_InvFInventoryInFGSum(Rpt_InvFInventoryInFGSumUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Rpt_InvFInventoryInFGSum = TableName.Rpt_InvFInventoryInFGSum + ".";
            var Tbl_Inv_InvFInventoryInFGSum = TableName.InvF_InventoryInFG + ".";

            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + TblRpt_InvFInventoryInFGSum.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartName))
            {
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + TblRpt_InvFInventoryInFGSum.PartName, CUtils.StrValueOrNull(data.PartName), "=");
            }
            //if (!CUtils.IsNullOrEmpty(data.FromDate))
            //{
            //    var CreateDTimeUTCFrom = data.FromDate.ToString().Trim() + " 00:00:00";
            //    var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
            //    sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + "CreateDTimeUTCFrom", strFromDate, ">=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.ToDate))
            //{
            //    var CreateDTimeUTCTo = data.ToDate.ToString().Trim() + " 23:59:59";
            //    var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
            //    sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + "CreateDTimeUTCTo", strToDate, "<=");
            //}
            if (!CUtils.IsNullOrEmpty(data.CreateDTimeUTCFrom))
            {
                var CreateDTimeUTCFrom = data.CreateDTimeUTCFrom.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + "CreateDTimeUTCFrom", strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.CreateDTimeUTCTo))
            {
                var CreateDTimeUTCFrom = data.CreateDTimeUTCTo.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryInFGSum + "CreateDTimeUTCFrom", strFromDate, ">=");
            }
            strWhereClause = sbSql.ToString();

            return strWhereClause;
        }
        private string strWhereClause_Rpt_InvFInventoryOutFGSum(Rpt_InvFInventoryOutFGSumUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Rpt_InvFInventoryOutFGSum = TableName.Rpt_InvFInventoryOutFGSum + ".";
            var Tbl_Inv_InvFInventoryOutFGSum = TableName.InvF_InventoryOutFG + ".";

            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryOutFGSum + TblRpt_InvFInventoryOutFGSum.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_Rpt_InvFInventoryOutFGSum + TblRpt_InvFInventoryOutFGSum.DLCode, CUtils.StrValueOrNull(data.DLCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                var CreateDTimeUTCFrom = data.FromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InvFInventoryOutFGSum + TblInvF_InventoryOutFG.CreateDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                var CreateDTimeUTCTo = data.ToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InvFInventoryOutFGSum + TblInvF_InventoryOutFG.CreateDTimeUTC, strToDate, "<=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Part(Mst_Part data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Part = TableName.Mst_Part + ".";
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Inv_InventoryBalance(Inv_InventoryBalance data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalance = TableName.Inv_InventoryBalance + ".";
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalance + TblInv_InventoryBalance.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Agent(Mst_Agent data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Agent = TableName.Mst_Agent + ".";
            if (!CUtils.IsNullOrEmpty(data.AgentCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Agent.AgentCode, CUtils.StrValueOrNull(data.AgentCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Agent.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValue(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }


            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNTCurChil(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValue(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }


            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}