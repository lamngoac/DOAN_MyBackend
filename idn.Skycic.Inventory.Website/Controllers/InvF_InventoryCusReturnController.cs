using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Constants;
using System.Text;
using idn.Skycic.Inventory.Website.Utils;
using System.Data;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Common.Models.DMS;
using idn.Skycic.Inventory.ClientService.Services.DMS;
using idn.Skycic.Inventory.ClientService.Services.Inbrand;
using idn.Skycic.Inventory.Common.Models.Veloca;
using idn.Skycic.Inventory.ClientService.Services.Veloca;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class InvF_InventoryCusReturnController : AdminBaseController
    {
        // GET: InvF_InventoryCusReturn
        #region ["Search Index"]
        public async Task<ActionResult> Index(int page = 0, int recordcount = 10)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserState = UserState;
                #region ["UserState Common Info"]                
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var itemCount = 0;
                var pageCount = 0;
                ViewBag.PageCur = page;
                ViewBag.Recordcount = recordcount;
                var pageInfo = new PageInfo<InvF_InventoryCusReturn>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryCusReturn>()
                };

                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";                

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();               

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }                

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;

                #region Thông tin khách hàng

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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1'"// and Mst_Customer.CustomerType = 'NCC'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                return View(pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch(string if_invcusreturnno = "", string createdtimefrom = "", string createdtimeto = "", string apprdtimefrom = "",
            string apprdtimeto = "", string orderno = "", string customercode = "", string invcodein = "", string productcode = "", string chkpending = "", string chkapproved = "", string chkcanceled = "", int recordcount = 50, int page = 0
            )
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserState = UserState;
                #region ["UserState Common Info"]
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var itemCount = 0;
                var pageCount = 0;
                ViewBag.PageCur = page;
                ViewBag.Recordcount = recordcount;
                var pageInfo = new PageInfo<InvF_InventoryCusReturn>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryCusReturn>()
                };

                var strWhereClause = strWhereClause_InvF_InventoryCusReturn(if_invcusreturnno, createdtimefrom, createdtimeto, apprdtimefrom,
                apprdtimeto, orderno, customercode, invcodein, productcode, chkpending, chkapproved, chkcanceled);
                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = ""
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count > 0)
                {
                    foreach (var item in objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn)
                    {
                        if (CUtils.StrValue(item.RefType) == RefType.RO)
                        {
                            item.RefNo = "RO-" + CUtils.StrValue(item.RefNo);
                        }
                    }

                    pageInfo.DataList = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn;
                    itemCount = objRT_InvF_InventoryCusReturn.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryCusReturn.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("BindDataInvF_InventoryCusReturn", pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #endregion

        #region Master

        [HttpPost]
        public async Task<ActionResult> SearchInvInType(string txtsearch)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {

                #region ["Mst_InvInType"]

                var strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";
                if (!string.IsNullOrEmpty(txtsearch))
                {
                    strWhere_Mst_InvInType += " and ( ";
                    strWhere_Mst_InvInType += " Mst_InvInType.InvInType like '%" + txtsearch + "%' or ";
                    strWhere_Mst_InvInType += " Mst_InvInType.InvInTypeName like '%" + txtsearch + "%' )";
                }

                var Mst_InvInType_Get_Task = Mst_InvInType_Get_Async(strWhere_Mst_InvInType);
                #endregion

                await Task.WhenAll(Mst_InvInType_Get_Task);
                var list_Mst_InvInType = new List<Mst_InvInType>();

                if (Mst_InvInType_Get_Task != null && Mst_InvInType_Get_Task.Result != null && Mst_InvInType_Get_Task.Result.Count > 0)
                {
                    list_Mst_InvInType.AddRange(Mst_InvInType_Get_Task.Result);
                }

                ViewBag.List_Mst_InvInType = list_Mst_InvInType;

                return JsonView("SearchInvInType");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);

        }

        [HttpPost]
        public async Task<ActionResult> SearchInventoryIn(string txtsearch)
        {

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";

                if (!string.IsNullOrEmpty(txtsearch))
                {
                    strWhere_Mst_Inventory += " and ( ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvCode like '%" + txtsearch + "%' or ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvName like '%" + txtsearch + "%' )";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.List_Mst_Inventory = list_Mst_Inventory;

                return JsonView("SearchInventoryIn");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShowInvCodeInActual(string productcode, string productcodeuser, string productcodebase, string productname, string invbupattern, string listdetail, string viewtype, string ifinvcusreturnstatus)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invbupattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invbupattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion
                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productcode;
                ViewBag.ProductName = productcodeuser + " - " + productname;
                ViewBag.ListMst_Inventory = list_Mst_Inventory;

                List<InvF_InventoryCusReturnDtl> lst_InvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnDtl>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryCusReturnDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnDtl>>(listdetail);
                }
                ViewBag.ListInvF_InventoryCusReturnDtl = lst_InvF_InventoryCusReturnDtl;

                ViewBag.IF_InvCusReturnStatus = ifinvcusreturnstatus;
                ViewBag.ViewType = viewtype;
                return JsonView("showInvCodeInActual", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShowLo(string productcode, string productcoderoot, string productCodeBase, string invBUPattern, string listlot, string viewtype, string ifinvcusreturnstatus)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productcode;
                ViewBag.ProductCodeRoot = productcoderoot;
                ViewBag.ListMst_Inventory = list_Mst_Inventory;

                List<InvF_InventoryCusReturnInstLot> lst_InvF_InventoryCusReturnInstLot = new List<InvF_InventoryCusReturnInstLot>();
                if (listlot != null && listlot.Length > 0)
                {
                    lst_InvF_InventoryCusReturnInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnInstLot>>(listlot);
                }
                ViewBag.ListInvF_InventoryCusReturnInstLot = lst_InvF_InventoryCusReturnInstLot;
                ViewBag.IF_InvCusReturnStatus = ifinvcusreturnstatus;
                ViewBag.ViewType = viewtype;
                return JsonView("Lo", null);
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
        public async Task<ActionResult> ShowSerial(string productcode, string productcoderoot, string productname, string invbupattern, string listserial, string ifinvcusreturnstatus, string viewtype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invbupattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invbupattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productcode;
                ViewBag.ProductCodeRoot = productcoderoot;
                ViewBag.ProductName = productcode + " - " + productname;
                ViewBag.ListMst_Inventory = list_Mst_Inventory;

                List<InvF_InventoryCusReturnInstSerial> lst_InvF_InventoryCusReturnInstSerial = new List<InvF_InventoryCusReturnInstSerial>();
                if (listserial != null && listserial.Length > 0)
                {
                    lst_InvF_InventoryCusReturnInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnInstSerial>>(listserial);
                }
                ViewBag.ListInvF_InventoryCusReturnInstSerial = lst_InvF_InventoryCusReturnInstSerial;
                ViewBag.IF_InvInStatus = ifinvcusreturnstatus;
                ViewBag.ViewType = viewtype;

                return JsonView("Serial", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        [HttpPost]
        public async Task<ActionResult> ShowInvCodeInActual_Old(string productcode, string productcodeuser, string productcodebase, string productname, string invbupattern, string listdetail)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invbupattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invbupattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productcode;
                ViewBag.ProductName = productcodeuser + " - " + productname;
                ViewBag.ListInvCodeIn = list_Mst_Inventory;

                List<InvF_InventoryCusReturnDtl> lst_InvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnDtl>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryCusReturnDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnDtl>>(listdetail);
                }
                ViewBag.ListInvF_InventoryCusReturnDtl = lst_InvF_InventoryCusReturnDtl;

                #region Thông tin tồn kho - Tìm VT tồn lớn nhất
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (!string.IsNullOrEmpty(productcodebase))
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productcodebase, "=");
                    sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", invbupattern, "like");
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
                var invCodeMax = "";
                if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                {
                    var qtytotalMax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK);
                    var lstItBalanceMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK == qtytotalMax).ToList();

                    if (lstItBalanceMax.Count > 0)
                    {
                        invCodeMax = Convert.ToString(lstItBalanceMax[0].InvCode);
                    }
                }

                ViewBag.InvCodeMax = invCodeMax;

                #endregion

                return JsonView("showInvCodeInActual", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        [HttpPost]
        public async Task<ActionResult> ShowInvCodeInActualCombo(string productcode, string productcodeuser, string productname, string qty, string invbupattern, string listdetail)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invbupattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invbupattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productcode;
                ViewBag.ProductName = productcodeuser + " - " + productname;
                ViewBag.Qty = qty;
                ViewBag.ListInvCodeIn = list_Mst_Inventory;

                List<InvF_InventoryCusReturnDtlUI> lst_InvF_InventoryCusReturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryCusReturnDtlUI = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnDtlUI>>(listdetail);
                }
                ViewBag.ListInvF_InventoryCusReturnDtl = lst_InvF_InventoryCusReturnDtlUI;
                
                return JsonView("showInvCodeInActualCombo", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("showInvCodeInActualCombo", null, resultEntry);
        }

        [HttpPost]
        public async Task<ActionResult> SearchCustomer(string txtsearch)
        {

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Customer"]

                var strWhere_Mst_Customer = "Mst_Customer.FlagActive = '1'"; // and Mst_Customer.CustomerType = 'NCC'"

                if (!string.IsNullOrEmpty(txtsearch))
                {
                    strWhere_Mst_Customer += " and ( ";
                    strWhere_Mst_Customer += " Mst_Customer.CustomerCode like '%" + txtsearch + "%' or ";
                    strWhere_Mst_Customer += " Mst_Customer.CustomerName like '%" + txtsearch + "%' )";
                }

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
                    Ft_WhereClause = strWhere_Mst_Customer
                };

                var Mst_Customer_Get_Task = List_Mst_Customer_Async(objRQ_Mst_Customer);
                #endregion

                await Task.WhenAll(Mst_Customer_Get_Task);
                var list_Mst_Customer = new List<Mst_Customer>();

                if (Mst_Customer_Get_Task != null && Mst_Customer_Get_Task.Result != null && Mst_Customer_Get_Task.Result.Lst_Mst_Customer.Count > 0)
                {
                    list_Mst_Customer.AddRange(Mst_Customer_Get_Task.Result.Lst_Mst_Customer);
                }

                ViewBag.List_Mst_Customer = list_Mst_Customer;

                return JsonView("SearchCustomer");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("SearchCustomer", null, resultEntry);

        }

        [HttpPost]
        public async Task<ActionResult> ShowLo_Old(string productCode, string productCodeBase, string invBUPattern, string listLot)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productCode;
                ViewBag.ListInvCodeIn = list_Mst_Inventory;

                List<InvF_InventoryCusReturnInstLot> lst_InvF_InventoryCusReturnInstLot = new List<InvF_InventoryCusReturnInstLot>();
                if (listLot != null && listLot.Length > 0)
                {
                    lst_InvF_InventoryCusReturnInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnInstLot>>(listLot);
                }
                ViewBag.ListLot = lst_InvF_InventoryCusReturnInstLot;

                #region Thông tin tồn kho - Tìm VT tồn lớn nhất
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                    sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", invBUPattern, "like");
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
                var invCodeMax = "";
                if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                {
                    var qtytotalMax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK);
                    var lstItBalanceMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK == qtytotalMax).ToList();

                    if (lstItBalanceMax.Count > 0)
                    {
                        invCodeMax = Convert.ToString(lstItBalanceMax[0].InvCode);
                    }
                }

                ViewBag.InvCodeMax = invCodeMax;

                #endregion

                return JsonView("Lo", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }
        [HttpPost]
        public async Task<ActionResult> ShowSerial_Old(string productCode, string productCodeBase, string invBUPattern, string listSerial)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.ProductCode = productCode;
                ViewBag.ListInvCodeIn = list_Mst_Inventory;

                List<InvF_InventoryCusReturnInstSerial> lst_InvF_InventoryCusReturnInstSerial = new List<InvF_InventoryCusReturnInstSerial>();
                if (listSerial != null && listSerial.Length > 0)
                {
                    lst_InvF_InventoryCusReturnInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnInstSerial>>(listSerial);
                }
                ViewBag.ListSerial = lst_InvF_InventoryCusReturnInstSerial;

                #region Thông tin tồn kho - Tìm VT tồn lớn nhất
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (!string.IsNullOrEmpty(productCodeBase))
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                    sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", invBUPattern, "like");
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
                var invCodeMax = "";
                if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                {
                    var qtytotalMax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK);
                    var lstItBalanceMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK == qtytotalMax).ToList();

                    if (lstItBalanceMax.Count > 0)
                    {
                        invCodeMax = Convert.ToString(lstItBalanceMax[0].InvCode);
                    }
                }

                ViewBag.InvCodeMax = invCodeMax;

                #endregion

                return JsonView("Serial", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        [HttpPost]
        public async Task<ActionResult> ShowCombo(string productCode, string invBUPattern, string listCombo, string qtyCombo = "0")
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
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    strWhere_Mst_Inventory += " and ";
                    strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                }

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                
                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }
                                
                ViewBag.ListInvCodeIn = list_Mst_Inventory;
                #endregion

                List<InvF_InventoryCusReturnDtlUI> lst_InvF_InventoryCusReturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
                var lstPrd_BOM = new List<Prd_BOM>();
                if (listCombo != null && listCombo.Length > 0)
                {
                    lst_InvF_InventoryCusReturnDtlUI = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturnDtlUI>>(listCombo);
                }
                else
                {
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

                    var rt = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                    if (rt != null && rt.Lst_Prd_BOM != null)
                    {
                        lstPrd_BOM = rt.Lst_Prd_BOM;
                    }

                    var qtyCb = Convert.ToDouble(qtyCombo);
                    //Add
                    foreach (var it in lstPrd_BOM)
                    {
                        InvF_InventoryCusReturnDtlUI objDtlUI = new InvF_InventoryCusReturnDtlUI();
                        objDtlUI.ProductCodeRoot = productCode;
                        objDtlUI.ProductCode = it.ProductCode;
                        objDtlUI.mp_ProductCodeUser = it.mp_ProductCodeUser;
                        objDtlUI.mp_ProductCodeBase = it.mp_ProductCodeBase;
                        objDtlUI.mp_ProductName = it.mp_ProductName;
                        objDtlUI.UnitCode = it.mp_UnitCode;
                        objDtlUI.ificrc_UPCusReturn = it.mp_UPSell;
                        objDtlUI.QtyRoot = it.Qty;
                        objDtlUI.Qty = qtyCb * Convert.ToDouble(!string.IsNullOrEmpty(Convert.ToString(objDtlUI.QtyRoot)) ? Convert.ToString(objDtlUI.QtyRoot) : "0");                        
                        objDtlUI.ValConvert = it.mp_ValConvert;

                        lst_InvF_InventoryCusReturnDtlUI.Add(objDtlUI);
                    }
                }

                #region Thông tin tồn kho
                var lst_StrProductCodeBase = new List<string>();
                foreach (var it in lst_InvF_InventoryCusReturnDtlUI)
                {
                    lst_StrProductCodeBase.Add(CUtils.StrValue(it.mp_ProductCodeBase));
                }
                var strWhereClause = "";
                var sb_SQL = new StringBuilder();
                var lstProductCodeBase = CUtils.StretchListString(lst_StrProductCodeBase);
                
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (lstProductCodeBase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCodeBase, "IN");
                    sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", invBUPattern, "like");
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
                
                foreach (var it in lst_InvF_InventoryCusReturnDtlUI)
                {
                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                    {
                        var lstBalanceByProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.mp_ProductCodeBase)).ToList();
                        if (lstBalanceByProduct != null && lstBalanceByProduct.Count > 0)
                        {
                            var sumQtyTotalOK = lstBalanceByProduct.Sum(x => Convert.ToDouble(x.QtyTotalOK));
                            var qtytotalMax = lstBalanceByProduct.Max(x => x.QtyTotalOK);
                            var listItBalanceMax = lstBalanceByProduct.Where(x => x.QtyTotalOK == qtytotalMax).ToList();

                            //Tồn kho, Vị trí tồn lớn nhất
                            double valConvert = it.ValConvert != null && !string.IsNullOrEmpty(it.ValConvert.ToString()) ? Convert.ToDouble(it.ValConvert) : 1;
                            it.QtyTotalOK = sumQtyTotalOK / valConvert;
                            it.InvCodeMax = listItBalanceMax.Count > 0 ? listItBalanceMax[0].InvCode : "";
                        }
                    }
                }

                #region Gộp vị trí theo mã sản phẩm

                List<InvF_InventoryCusReturnDtlUI> lst_InvF_InventoryCusReturnDtlUIGroup = new List<InvF_InventoryCusReturnDtlUI>();
                List<string> lstProductCodeDistinct = new List<string>();
                foreach (var it in lst_InvF_InventoryCusReturnDtlUI)
                {
                    var prdCodeCur = Convert.ToString(it.ProductCode);
                    if(!lstProductCodeDistinct.Contains(prdCodeCur))
                    {
                        var itGroup = new InvF_InventoryCusReturnDtlUI
                        {
                            ProductCodeRoot = it.ProductCodeRoot,
                            ProductCode = it.ProductCode,
                            mp_ProductCodeUser = it.mp_ProductCodeUser,
                            mp_ProductCodeBase = it.mp_ProductCodeBase,
                            mp_ProductName = it.mp_ProductName,
                            UnitCode = it.UnitCode,
                            ificrc_UPCusReturn = it.ificrc_UPCusReturn,
                            QtyRoot = it.QtyRoot,
                            Qty = it.Qty,
                            ValConvert = it.ValConvert,
                            QtyTotalOK = it.QtyTotalOK,
                            InvCodeMax = it.InvCodeMax,
                            InvCodeInActual = it.InvCodeInActual
                        };

                        lst_InvF_InventoryCusReturnDtlUIGroup.Add(itGroup);

                        lstProductCodeDistinct.Add(prdCodeCur);
                    }
                    else
                    {
                        lst_InvF_InventoryCusReturnDtlUIGroup.Where(m => Convert.ToString(m.ProductCode) == prdCodeCur).ToList().ForEach(s => s.InvCodeInActual += ", " + it.InvCodeInActual);

                        var qtyCur = lst_InvF_InventoryCusReturnDtlUIGroup.SingleOrDefault(m => Convert.ToString(m.ProductCode) == prdCodeCur).Qty;
                        lst_InvF_InventoryCusReturnDtlUIGroup.Where(m => Convert.ToString(m.ProductCode) == prdCodeCur).ToList().ForEach(s => s.Qty = Convert.ToDouble(qtyCur) + Convert.ToDouble(it.Qty));
                    }

                }

                #endregion

                ViewBag.Lst_InvF_InventoryCusReturnDtlUIGroup = lst_InvF_InventoryCusReturnDtlUIGroup;
                ViewBag.ProductCode = productCode;
                return JsonView("Combo", lst_InvF_InventoryCusReturnDtlUI);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Combo", null, resultEntry);
        }
        #endregion

        public async Task<ActionResult> Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.UserState = UserState;
            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var networkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);

            var iFInvCusReturnNo = SeqNo(new Seq_Common()
            {
                SequenceType = SeqType.IFInvCusReturnNo,
                Param_Postfix = "",
                Param_Prefix = ""
            });
            #region ["Mst_Inventory"]
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";

            var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
            #endregion

            await Task.WhenAll(Mst_Inventory_Get_Task);
            var list_Mst_Inventory = new List<Mst_Inventory>();

            if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
            {
                list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
            }

            ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
            ViewBag.InvInType = "CUSTOMERRETURN";//Fix cứng mã loại nhập kho là KH trả hàng

            #region Thông tin khách hàng
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

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
                Ft_WhereClause = "Mst_Customer.FlagActive = '1'"// and Mst_Customer.CustomerType = 'NCC'"
            };
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
            {
                lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
            }
            ViewBag.Lst_MstCustomer = lstCustomer;
            #endregion

            string if_InvInNo = SeqNo(new Seq_Common() { SequenceType = "IFInvCusReturnNo", Param_Postfix = "", Param_Prefix = "" });

            ViewBag.IF_InvCusReturnNo = if_InvInNo;
            var if_InvCusReturnStatus = "PENDING";
            ViewBag.If_InvCusReturnStatus = if_InvCusReturnStatus;

            return View(new RT_InvF_InventoryCusReturn() {
                Lst_InvF_InventoryCusReturn = new List<InvF_InventoryCusReturn>()
                {
                    new InvF_InventoryCusReturn()
                    {
                        NetworkID = networkID,
                        OrgID = orgID,
                        IF_InvCusReturnNo = iFInvCusReturnNo,
                        IF_InvCusReturnStatus = "PENDING",
                    }
                },
                Lst_InvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnDtl>(),
                Lst_InvF_InventoryCusReturnCover = new List<InvF_InventoryCusReturnCover>(),
                Lst_InvF_InventoryCusReturnInstLot = new List<InvF_InventoryCusReturnInstLot>(),
                Lst_InvF_InventoryCusReturnInstSerial = new List<InvF_InventoryCusReturnInstSerial>(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            string orgId = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            string networkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                InvF_InventoryCusReturnSave objInvF_InventoryCusReturnSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryCusReturnSave>(model);
                objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn.OrgID = orgId;
                objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn.NetworkID = networkID;

                RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    InvF_InventoryCusReturn = objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn,
                    Lst_InvF_InventoryCusReturnCover = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnCover,
                    Lst_InvF_InventoryCusReturnDtl = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnDtl,
                    Lst_InvF_InventoryCusReturnInstLot = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnInstLot,
                    Lst_InvF_InventoryCusReturnInstSerial = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnInstSerial,                    
                    FlagIsDelete = "0",
                    FlagIsCheckTotal = "1"
                };
                InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Save(objRQ_InvF_InventoryCusReturn, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo phiếu khách hàng trả hàng thành công!");
                var flagRedirect = CUtils.StrValue(objInvF_InventoryCusReturnSave.FlagRedirect);
                if (flagRedirect.Equals("1"))
                {
                    resultEntry.RedirectUrl = Url.Action("Create");
                }
                else
                {
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        public async Task<ActionResult> Update(string if_invcusreturnno)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = UserState;
            #endregion
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
                
                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
                #endregion

                #region Thông tin khách hàng
                //var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1'"// and Mst_Customer.CustomerType = 'NCC'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                InvF_InventoryCusReturn objInvF_InventoryCusReturn = new InvF_InventoryCusReturn();
                var strWhere = strWhereClause_InvF_InventoryCusReturnByID(if_invcusreturnno);
                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count != 0)
                {
                    objInvF_InventoryCusReturn = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn[0];
                    if (CUtils.StrValue(objInvF_InventoryCusReturn.RefType) == RefType.RO)
                    {
                        objInvF_InventoryCusReturn.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryCusReturn.RefNo);
                    }
                }

                #region Get List đơn vị tính theo Product + Xử lý list cover UI

                var listInvF_InventoryCusReturnCoverUI = new List<InvF_InventoryCusReturnCoverUI>();

                var Lst_InvF_InventoryCusReturnCover = new List<InvF_InventoryCusReturnCover>();
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover != null)
                {
                    Lst_InvF_InventoryCusReturnCover = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover;
                }

                var lstPrdCodeBase = Lst_InvF_InventoryCusReturnCover.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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
                    Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase),
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "",
                    Rt_Cols_Mst_ProductFiles = "",
                    Rt_Cols_Prd_BOM = "",
                    Rt_Cols_Prd_Attribute = "",
                };

                var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
                                
                foreach (var item in Lst_InvF_InventoryCusReturnCover)
                {
                    var strListInvCodeInActual = "";
                    if(item.ListInvCodeInActual != null && !string.IsNullOrEmpty(item.ListInvCodeInActual.ToString()))
                    {
                        strListInvCodeInActual = item.ListInvCodeInActual.ToString().Substring(0, item.ListInvCodeInActual.ToString().Length - 1);
                    }
                    var itemUI = new InvF_InventoryCusReturnCoverUI()
                    {
                        IF_InvCusReturnNo = item.IF_InvCusReturnNo,
                        InvCodeInActual = strListInvCodeInActual,
                        ProductCodeRoot = item.ProductCodeRoot,
                        NetworkID = item.NetworkID,
                        Qty = item.Qty,
                        UPCusReturn = item.UPCusReturn,
                        UPCusReturnDesc = item.UPCusReturnDesc,
                        ValCusReturnAfterDesc = item.ValCusReturnAfterDesc,
                        ValCusReturn = item.ValCusReturn,
                        ValCusReturnDesc = item.ValCusReturnDesc,
                        UnitCode = item.UnitCode,
                        IF_InvCusReturnStatusCover = item.IF_InvCusReturnStatusCover,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_ProductName,
                        mp_ProductCodeUser = item.mp_ProductCodeUser,
                        mp_ProductCodeBase = item.mp_ProductCodeBase,
                        mp_FlagLot = item.mp_FlagLot,
                        mp_FlagSerial = item.mp_FlagSerial,
                        FlagCombo = Convert.ToString(item.mp_ProductType) == "COMBO" ?  "1" : "0"
                    };

                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        var listPrdCheckBase = listProductBase.Where(x => x.ProductCodeBase.ToString() == item.mp_ProductCodeBase.ToString()).ToList();
                        if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                        {
                            itemUI.Lst_Mst_ProductBase = listPrdCheckBase;
                        }
                    }
                    //
                    listInvF_InventoryCusReturnCoverUI.Add(itemUI);
                }

                #endregion

                #region DS hàng hóa thành phần combo

                var listInvF_InventoryCusReturnDtlCombo = new List<InvF_InventoryCusReturnDtlUI>();
                var listDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl;
                foreach(var itCover in listInvF_InventoryCusReturnCoverUI)
                {
                    if (Convert.ToString(itCover.FlagCombo) == "1")
                    {
                        var productCodeRoot = itCover.ProductCodeRoot.ToString();
                        var qtyCover = itCover.Qty.ToString();
                        var lstDtlCombo = listDtl.Where(m => m.ProductCodeRoot.ToString() == productCodeRoot).ToList();
                        if (lstDtlCombo.Count > 0)
                        {
                            foreach(var itDtl in lstDtlCombo)
                            {
                                var itemDtlCombo = new InvF_InventoryCusReturnDtlUI();
                                itemDtlCombo.IF_InvCusReturnNo = itDtl.IF_InvCusReturnNo;
                                itemDtlCombo.InvCodeInActual = itDtl.InvCodeInActual;
                                itemDtlCombo.ProductCodeRoot = itDtl.ProductCodeRoot;
                                itemDtlCombo.ProductCode = itDtl.ProductCode;
                                itemDtlCombo.NetworkID = itDtl.NetworkID;
                                itemDtlCombo.Qty = itDtl.Qty;
                                itemDtlCombo.QtyRoot = Convert.ToDouble(itDtl.Qty) / Convert.ToDouble(qtyCover);
                                itemDtlCombo.UnitCode = itDtl.UnitCode;
                                itemDtlCombo.IF_InvCusReturnStatusDtl = itDtl.IF_InvCusReturnStatusDtl;
                                itemDtlCombo.Remark = itDtl.Remark;
                                itemDtlCombo.mp_ProductName = itDtl.mp_ProductName;
                                itemDtlCombo.mp_ProductCodeUser = itDtl.mp_ProductCodeUser;
                                itemDtlCombo.mp_ProductCodeBase = itDtl.mp_ProductCodeBase;
                                itemDtlCombo.ificrc_UPCusReturn = itDtl.ificrc_UPCusReturn;

                                listInvF_InventoryCusReturnDtlCombo.Add(itemDtlCombo);
                            }
                        }
                    }
                }

                #endregion
                
                ViewBag.lstInvF_InventoryCusReturnInstLot = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot;
                ViewBag.lstInvF_InventoryCusReturnInstSerial = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial;
                ViewBag.lstInvF_InventoryCusReturnCoverUI = listInvF_InventoryCusReturnCoverUI;
                ViewBag.lstInvF_InventoryCusReturnDtlCombo = listInvF_InventoryCusReturnDtlCombo;
                ViewBag.lstInvF_InventoryCusReturnDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl;
                //
                return View(objRT_InvF_InventoryCusReturn);
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
            return Json(resultEntry, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateData(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            string orgId = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            string networkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                InvF_InventoryCusReturnSave objInvF_InventoryCusReturnSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryCusReturnSave>(model);
                objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn.OrgID = orgId;
                objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn.NetworkID = networkID;

                RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    InvF_InventoryCusReturn = objInvF_InventoryCusReturnSave.Obj_InvF_InventoryCusReturn,
                    Lst_InvF_InventoryCusReturnDtl = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnDtl,
                    Lst_InvF_InventoryCusReturnInstLot = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnInstLot,
                    Lst_InvF_InventoryCusReturnInstSerial = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnInstSerial,
                    Lst_InvF_InventoryCusReturnCover = objInvF_InventoryCusReturnSave.Lst_InvF_InventoryCusReturnCover,
                    FlagIsDelete = "0",
                    FlagIsCheckTotal = "1"
                };
                InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Save(objRQ_InvF_InventoryCusReturn, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Sửa phiếu khách hàng trả hàng thành công!");
                var flagRedirect = CUtils.StrValue(objInvF_InventoryCusReturnSave.FlagRedirect);
                if (flagRedirect.Equals("1"))
                {
                    resultEntry.RedirectUrl = Url.Action("Update");
                }
                else
                {
                    resultEntry.RedirectUrl = Url.Action("Index");
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

        public async Task<ActionResult> Detail(string if_invcusreturnno)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYCUSRETURN_DETAIL");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = UserState;

            #endregion
            try
            {
                #region ["Mst_Inventory"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);

                await Task.WhenAll(Mst_Inventory_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
                ViewBag.InvInType = "CUSTOMERRETURN";
                #endregion

                #region Thông tin khách hàng
                //var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);

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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1'"// and Mst_Customer.CustomerType = 'NCC'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                InvF_InventoryCusReturn objInvF_InventoryCusReturn = new InvF_InventoryCusReturn();
                var strWhere = strWhereClause_InvF_InventoryCusReturnByID(if_invcusreturnno);
                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count != 0)
                {
                    objInvF_InventoryCusReturn = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn[0];
                    if (CUtils.StrValue(objInvF_InventoryCusReturn.RefType) == RefType.RO)
                    {
                        objInvF_InventoryCusReturn.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryCusReturn.RefNo);
                    }
                }

                #region Get List đơn vị tính theo Product + Xử lý list cover UI

                var listInvF_InventoryCusReturnCoverUI = new List<InvF_InventoryCusReturnCoverUI>();

                var Lst_InvF_InventoryCusReturnCover = new List<InvF_InventoryCusReturnCover>();
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover != null)
                {
                    Lst_InvF_InventoryCusReturnCover = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover;
                }

                var lstPrdCodeBase = Lst_InvF_InventoryCusReturnCover.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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
                    Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase),
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "",
                    Rt_Cols_Mst_ProductFiles = "",
                    Rt_Cols_Prd_BOM = "",
                    Rt_Cols_Prd_Attribute = "",
                };

                var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
                
                foreach (var item in Lst_InvF_InventoryCusReturnCover)
                {
                    var strListInvCodeInActual = "";
                    if (item.ListInvCodeInActual != null && !string.IsNullOrEmpty(item.ListInvCodeInActual.ToString()))
                    {
                        strListInvCodeInActual = item.ListInvCodeInActual.ToString().Substring(0, item.ListInvCodeInActual.ToString().Length - 1);
                    }
                    var itemUI = new InvF_InventoryCusReturnCoverUI()
                    {
                        IF_InvCusReturnNo = item.IF_InvCusReturnNo,
                        InvCodeInActual = strListInvCodeInActual,
                        ProductCodeRoot = item.ProductCodeRoot,
                        NetworkID = item.NetworkID,
                        Qty = item.Qty,
                        UPCusReturn = item.UPCusReturn,
                        UPCusReturnDesc = item.UPCusReturnDesc,
                        ValCusReturnAfterDesc = item.ValCusReturnAfterDesc,
                        ValCusReturn = item.ValCusReturn,
                        ValCusReturnDesc = item.ValCusReturnDesc,
                        UnitCode = item.UnitCode,
                        IF_InvCusReturnStatusCover = item.IF_InvCusReturnStatusCover,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_ProductName,
                        mp_ProductCodeUser = item.mp_ProductCodeUser,
                        mp_ProductCodeBase = item.mp_ProductCodeBase,
                        mp_FlagLot = item.mp_FlagLot,
                        mp_FlagSerial = item.mp_FlagSerial,
                        FlagCombo = Convert.ToString(item.mp_ProductType) == "COMBO" ? "1" : "0"
                    };

                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        var listPrdCheckBase = listProductBase.Where(x => x.ProductCodeBase.ToString() == item.mp_ProductCodeBase.ToString()).ToList();
                        if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                        {
                            itemUI.Lst_Mst_ProductBase = listPrdCheckBase;
                        }
                    }
                    //
                    listInvF_InventoryCusReturnCoverUI.Add(itemUI);
                }

                #endregion

                #region DS hàng hóa thành phần combo

                var listInvF_InventoryCusReturnDtlCombo = new List<InvF_InventoryCusReturnDtlUI>();
                var listDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl;
                foreach (var itCover in listInvF_InventoryCusReturnCoverUI)
                {
                    if (Convert.ToString(itCover.FlagCombo) == "1")
                    {
                        var productCodeRoot = itCover.ProductCodeRoot.ToString();
                        var qtyCover = itCover.Qty.ToString();
                        var lstDtlCombo = listDtl.Where(m => m.ProductCodeRoot.ToString() == productCodeRoot).ToList();
                        if (lstDtlCombo.Count > 0)
                        {
                            foreach (var itDtl in lstDtlCombo)
                            {
                                var itemDtlCombo = new InvF_InventoryCusReturnDtlUI();
                                itemDtlCombo.IF_InvCusReturnNo = itDtl.IF_InvCusReturnNo;
                                itemDtlCombo.InvCodeInActual = itDtl.InvCodeInActual;
                                itemDtlCombo.ProductCodeRoot = itDtl.ProductCodeRoot;
                                itemDtlCombo.ProductCode = itDtl.ProductCode;
                                itemDtlCombo.NetworkID = itDtl.NetworkID;
                                itemDtlCombo.Qty = itDtl.Qty;
                                itemDtlCombo.QtyRoot = Convert.ToDouble(itDtl.Qty) / Convert.ToDouble(qtyCover);
                                itemDtlCombo.UnitCode = itDtl.UnitCode;
                                itemDtlCombo.IF_InvCusReturnStatusDtl = itDtl.IF_InvCusReturnStatusDtl;
                                itemDtlCombo.Remark = itDtl.Remark;
                                itemDtlCombo.mp_ProductName = itDtl.mp_ProductName;
                                itemDtlCombo.mp_ProductCodeUser = itDtl.mp_ProductCodeUser;
                                itemDtlCombo.mp_ProductCodeBase = itDtl.mp_ProductCodeBase;
                                itemDtlCombo.ificrc_UPCusReturn = itDtl.ificrc_UPCusReturn;

                                listInvF_InventoryCusReturnDtlCombo.Add(itemDtlCombo);
                            }
                        }
                    }
                }

                #endregion

                ViewBag.lstInvF_InventoryCusReturnInstLot = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot;
                ViewBag.lstInvF_InventoryCusReturnInstSerial = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial;
                ViewBag.lstInvF_InventoryCusReturnCoverUI = listInvF_InventoryCusReturnCoverUI;
                ViewBag.lstInvF_InventoryCusReturnDtlCombo = listInvF_InventoryCusReturnDtlCombo;
                ViewBag.lstInvF_InventoryCusReturnDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl;
                //
                //return View(objInvF_InventoryCusReturn);
                return View(objRT_InvF_InventoryCusReturn);
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

        #region ["Approve"]
        [HttpPost]
        public ActionResult Approve(string objlistinvf_inventorycusreturn)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYCUSRETURN_APPROVE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                List<InvF_InventoryCusReturn> listInvF_InventoryCusReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturn>>(objlistinvf_inventorycusreturn);

                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    InvF_InventoryCusReturn = null
                };

                foreach (var item in listInvF_InventoryCusReturn)
                {
                    objRQ_InvF_InventoryCusReturn.Tid = GetNextTId();
                    objRQ_InvF_InventoryCusReturn.InvF_InventoryCusReturn = new InvF_InventoryCusReturn()
                    {
                        IF_InvCusReturnNo = item.IF_InvCusReturnNo,
                        Remark = item.Remark
                    };

                    InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Appr(objRQ_InvF_InventoryCusReturn, addressAPIs);
                }

                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt phiếu khách hàng trả hàng thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion

        #region ["Cancel"]
        [HttpPost]
        public ActionResult Cancel(string objlistinvf_inventorycusreturn)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYCUSRETURN_CANCEL");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                List<InvF_InventoryCusReturn> listInvF_InventoryCusReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturn>>(objlistinvf_inventorycusreturn);

                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    InvF_InventoryCusReturn = null
                };

                foreach (var item in listInvF_InventoryCusReturn)
                {
                    objRQ_InvF_InventoryCusReturn.Tid = GetNextTId();
                    objRQ_InvF_InventoryCusReturn.InvF_InventoryCusReturn = new InvF_InventoryCusReturn()
                    {
                        IF_InvCusReturnNo = item.IF_InvCusReturnNo,
                        Remark = item.Remark
                    };

                    InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Cancel(objRQ_InvF_InventoryCusReturn, addressAPIs);
                }

                resultEntry.Success = true;
                resultEntry.AddMessage("Hủy phiếu khách hàng trả hàng thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion

        #region ["Delete"]
        [HttpPost]
        public ActionResult Delete(string objlistinvf_inventorycusreturn)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYCUSRETURN_DELETE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                List<InvF_InventoryCusReturn> listInvF_InventoryCusReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturn>>(objlistinvf_inventorycusreturn);
                var listIF_InvCusReturnNo = listInvF_InventoryCusReturn.Select(m => m.IF_InvCusReturnNo.ToString()).ToList();

                var strWhereInvF_InventoryCusReturn = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryCusReturn, TblInvF_InventoryCusReturn.IF_InvCusReturnNo, CUtils.StretchListString(listIF_InvCusReturnNo));

                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_WhereClause = strWhereInvF_InventoryCusReturn,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*"
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count > 0)
                {
                    foreach (var objInvF_InventoryCusReturn in objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn)
                    {
                        RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturnDelete = new RQ_InvF_InventoryCusReturn()
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
                            InvF_InventoryCusReturn = objInvF_InventoryCusReturn,
                            Lst_InvF_InventoryCusReturnDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl.Where(m => m.IF_InvCusReturnNo.ToString() == objInvF_InventoryCusReturn.IF_InvCusReturnNo.ToString()).ToList(),
                            Lst_InvF_InventoryCusReturnInstLot = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot.Where(m => m.IF_InvCusReturnNo.ToString() == objInvF_InventoryCusReturn.IF_InvCusReturnNo.ToString()).ToList(),
                            Lst_InvF_InventoryCusReturnInstSerial = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial.Where(m => m.IF_InvCusReturnNo.ToString() == objInvF_InventoryCusReturn.IF_InvCusReturnNo.ToString()).ToList(),
                            Lst_InvF_InventoryCusReturnCover = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover.Where(m => m.IF_InvCusReturnNo.ToString() == objInvF_InventoryCusReturn.IF_InvCusReturnNo.ToString()).ToList(),
                            FlagIsDelete = "1",
                            FlagIsCheckTotal = "1"
                        };
                        InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Save(objRQ_InvF_InventoryCusReturnDelete, addressAPIs);
                    }

                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá phiếu khách hàng trả hàng thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion

        #region[Lấy sản phẩm khi autocomplete]
        [HttpPost]
        public ActionResult GetProductExactly(string invcode, string prdid = "")
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.Mst_NNT.NetworkID);
            var orgID = CUtils.StrValueOrNull(UserState.Mst_NNT.OrgID);
            var utcOffset = CUtils.StrValueOrNull(UserState.UtcOffset);
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
                Rt_Cols_Mst_ProductImages = "",
                Rt_Cols_Mst_ProductFiles = "",
                Rt_Cols_Prd_BOM = "",
                Rt_Cols_Prd_Attribute = "",
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var objRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);

            var objMst_ProductBase = objRT_Mst_Product[0]; //Sản phẩm Hiển thị

            var strWhereClause_MstProductBase = strWhereClause_Mst_Product_Get_Base(objMst_ProductBase);
            var objRT_Mst_ProductBase = GetPrdBase(strWhereClause_MstProductBase);
            var lstPrdBase = objRT_Mst_ProductBase.Lst_Mst_Product;

            var listProduct = new List<Mst_Product>();
            listProduct.Add(objMst_ProductBase);
            if (lstPrdBase != null && lstPrdBase.Count > 0)
            {
                listProduct.AddRange(lstPrdBase);
            }

            #region Thông tin tồn kho - Tìm VT tồn lớn nhất
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
            foreach (var it in listProduct)
            {
                if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1" && Convert.ToString(it.ProductType) != "COMBO")
                {
                    lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                }
            }

            if (lstInvBalance_LastUpdInvByProduct.Count > 0)
            {
                var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    InvCode = invcode,
                    Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                };

                var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                {
                    listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                }
            }
            #endregion

            #region Khởi tạo list ProductUI

            List<Mst_ProductUI> listProductUI = new List<Mst_ProductUI>();
            if (listProduct != null && listProduct.Count > 0)
            {
                foreach (var itPrd in listProduct)
                {
                    var invCodeSuggest = "";
                    var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itPrd.ProductCode)).ToList();
                    if (lstBalanceCur.Count > 0)
                    {
                        invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                    }

                    Mst_ProductUI itPrdUI = new Mst_ProductUI
                    {
                        ProductCode = itPrd.ProductCode,
                        ProductCodeBase = itPrd.ProductCodeBase,
                        ProductCodeRoot = itPrd.ProductCodeRoot,
                        ProductCodeUser = itPrd.ProductCodeUser,
                        ProductName = itPrd.ProductName,
                        ProductType = itPrd.ProductType,
                        UnitCode = itPrd.UnitCode,
                        UPBuy = itPrd.UPBuy,
                        UPSell = itPrd.UPSell,
                        FlagActive = itPrd.FlagActive,
                        FlagLot = itPrd.FlagLot,
                        FlagSerial = itPrd.FlagSerial,
                        InvCodeSuggest = invCodeSuggest
                    };
                    listProductUI.Add(itPrdUI);
                }
            }

            #endregion

            return Json(new { Data = listProductUI });
        }

        [HttpPost]
        public ActionResult GetProductExactly1(string productCode, string productCodeBase, string productCodeRoot, string InvBUPattern, string valconvert, string mp_FlagLot, string mp_FlagSerial, string qty)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            List<Mst_Product> listProduct = new List<Mst_Product>();
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance12 = new List<Inv_InventoryBalance>();
            var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
            var tblInv_InventoryBalance = "Inv_InventoryBalance.";
            var tbMst_Inventory = "Mst_Inventory.";
            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;

            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var Tbl_Mst_Product = "Mst_Product.";
            #region["Thông tin chung"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.Mst_NNT.NetworkID);
            var orgID = CUtils.StrValueOrNull(UserState.Mst_NNT.OrgID);
            var utcOffset = CUtils.StrValueOrNull(UserState.UtcOffset);
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var objMST_ProductUI = new Mst_ProductUI();
            #endregion

            try
            {
                #region["Lấy thông tin master chính xác của hàng hóa"]

                var objMst_Product = new Mst_Product()
                {
                    ProductCode = productCode
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
                    Rt_Cols_Mst_ProductImages = "",
                    Rt_Cols_Mst_ProductFiles = "",
                    Rt_Cols_Prd_BOM = "",
                    Rt_Cols_Prd_Attribute = "",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var objRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);
                var objMst_ProductBase = objRT_Mst_Product[0];
                listProduct.Add(objMst_ProductBase);
                #endregion

                #region["Lấy thông tin tồn kho lớn nhất của hàng hóa được chọn"]

                var strWhereClauseTonKhoMax = "";
                var sb_SQLTonKhoMax = new StringBuilder();
                if (productCodeBase != null && productCodeBase != "")
                {
                    sb_SQLTonKhoMax.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                }
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    sb_SQLTonKhoMax.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                }
                strWhereClauseTonKhoMax = sb_SQLTonKhoMax.ToString();

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
                    Ft_WhereClause = strWhereClauseTonKhoMax
                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                var valConvert = 1.0;
                if (valconvert != null && valconvert != "")
                {
                    valConvert = Convert.ToDouble(valconvert);
                }
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                    if (lstInv_InventoryBalance.Count > 0)
                    {
                        qtymax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        var itMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK.Equals(qtymax)).FirstOrDefault();
                        if (itMax != null)
                        {
                            //qtytotalok = qtymax;
                            invCodeMax = itMax.InvCode == null ? "" : itMax.InvCode.ToString();
                        }


                        qtytotalok = lstInv_InventoryBalance.Sum(x => x.QtyTotalOK == null ? 0.0 : Convert.ToDouble(x.QtyTotalOK));
                    }
                }
                qtymax = qtymax / valConvert;
                qtytotalok = Math.Round(qtytotalok / valConvert, 2);


                #endregion


                #region["Danh sách hàng hóa cùng base"]
                var strWhereCungBase = "";
                var sbSqlCungBase = new StringBuilder();
                if (!CUtils.IsNullOrEmpty(productCodeBase))
                {
                    sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(productCodeBase), "=");
                }
                strWhereCungBase = sbSqlCungBase.ToString();
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
                    Ft_WhereClause = strWhereCungBase,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Product = "*",
                };
                var objRT_Mst_Product1 = Mst_ProductService.Instance.WA_Mst_Product_Get(rq, addressAPIs);
                if (objRT_Mst_Product1 != null && objRT_Mst_Product1.Lst_Mst_Product != null && objRT_Mst_Product1.Lst_Mst_Product.Count > 0)
                {
                    var listProductCode = objRT_Mst_Product1.Lst_Mst_Product.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
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
                            Ft_WhereClause = strWhereClause
                        };
                        var objRT_Inv_InventoryBalance1 = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance1, addressAPIs);
                        var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
                        var objInv_InventoryBalance = new Inv_InventoryBalance();
                        if (objRT_Inv_InventoryBalance1 != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance.Count > 0)
                        {
                            lstInv_InventoryBalance1 = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance;
                            objInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance[0];
                        }

                        foreach (var it in objRT_Mst_Product1.Lst_Mst_Product)
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
                            //productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                            productUI.UPOut = it.UPBuy == null ? 0 : it.UPBuy;
                            productUI.UPCusReturn = it.UPBuy == null ? 0 : it.UPBuy;
                            productUI.Remark = it.Remark;
                            productUI.ValUPReturn = "0";
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
                                    qtymax1 = lstInv_InventoryBalanceTonKho1.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                                    var itMax = lstInv_InventoryBalanceTonKho1.Where(x => x.QtyTotalOK.Equals(qtymax1)).FirstOrDefault();
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
                                    productUI.Qty = qty;
                                }
                            }


                            lst_Mst_ProductUI.Add(productUI);
                            objMST_ProductUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                            #endregion
                        }
                    }
                }
                #endregion

                #region["Phân bổ"]
                if (CUtils.StrValue(mp_FlagSerial).Equals("1"))
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
                        if (!CUtils.IsNullOrEmpty(objMST_ProductUI.Qty))
                        {
                            var qtyExport = Convert.ToDouble(objMST_ProductUI.Qty);
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
                else if (CUtils.StrValue(mp_FlagLot).Equals("1"))
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
                    if (!CUtils.IsNullOrEmpty(objMST_ProductUI.Qty))
                    {
                        var qtyExport = Convert.ToDouble(objMST_ProductUI.Qty);
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
                    var valConvert1 = Convert.ToDouble(valConvert_HH);
                    var strWhereClause12 = "";
                    var tblInv_InventoryBalance1 = "Inv_InventoryBalance.";

                    var sb_SQL2 = new StringBuilder();
                    if (productCodeBase != null && productCodeBase != "")
                    {
                        sb_SQL2.AddWhereClause(tblInv_InventoryBalance1 + "ProductCode", productCodeBase, "=");
                    }
                    if (InvBUPattern != null && InvBUPattern != "")
                    {
                        sb_SQL2.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
                    }
                    sb_SQL2.AddWhereClause(tblInv_InventoryBalance1 + "QtyTotalOK", 0, ">");
                    strWhereClause12 = sb_SQL2.ToString();

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
                        Ft_WhereClause = strWhereClause12
                    };
                    var objRT_Inv_InventoryBalance12 = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance1, addressAPIs);

                    if (objRT_Inv_InventoryBalance12 != null && objRT_Inv_InventoryBalance12.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance12.Lst_Inv_InventoryBalance.Count > 0)
                    {
                        lstInv_InventoryBalance12 = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;

                        if (!CUtils.IsNullOrEmpty(objMST_ProductUI.Qty))
                        {
                            var qtyExport = Convert.ToDouble(objMST_ProductUI.Qty);
                            lstInv_InventoryBalance12 = lstInv_InventoryBalance12.OrderByDescending(m => m.QtyTotalOK).ToList();
                            foreach (var it in lstInv_InventoryBalance12)
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
                return Json(new { Success = true, Data = lst_Mst_ProductUI, List_PhanBoHH = lstInv_InventoryBalance12, List_PhanBoLot = lstInv_InventoryBalanceLotView, List_PhanBoSerial = lstInv_InventoryBalanceSerial, qtytotalok = qtytotalok, invCodeMax = invCodeMax, qtyMax = qtymax, objMST_ProductUI = objMST_ProductUI, listProduct = listProduct });
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
        public ActionResult SearchProduct(string prdid = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            
            var listMst_InvoiceType = new List<Mst_Product>();
            try
            {
                var objMst_Product = new Mst_Product()
                {
                    ProductCodeUser = prdid,
                    ProductName = prdid,
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
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_SearchProductProductId,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "",
                    Rt_Cols_Mst_ProductFiles = "",
                    Rt_Cols_Prd_BOM = "",
                    Rt_Cols_Prd_Attribute = "",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };

                var objRT_Mst_InvoiceType = WA_Mst_Product_Get(objRQ_Mst_Product);
                listMst_InvoiceType.AddRange(objRT_Mst_InvoiceType);
                return Json(listMst_InvoiceType);
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        [HttpPost]
        public ActionResult GetProductFromPrdBarCode(string invcode, string prdbarcode = "")
        {
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.Mst_NNT.NetworkID);
            var orgID = CUtils.StrValueOrNull(UserState.Mst_NNT.OrgID);
            var utcOffset = CUtils.StrValueOrNull(UserState.UtcOffset);
            var resultEntry = new JsonResultEntry() { Success = false };
            var objMst_Product = new Mst_Product()
            {
                ProductBarCode = prdbarcode
            };
            var strWhere_GetProductByPrdBarcode = strWhereGetProductByPrdBarcode(objMst_Product);
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
                Ft_WhereClause = strWhere_GetProductByPrdBarcode,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = "",
                Rt_Cols_Mst_ProductFiles = "",
                Rt_Cols_Prd_BOM = "",
                Rt_Cols_Prd_Attribute = "",
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var lstRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);

            if(lstRT_Mst_Product == null || lstRT_Mst_Product.Count == 0)
            {
                return Json(new { Success = false, Messages = "Hàng hóa không có trong hệ thống!" });
            }

            var objMst_ProductBase = lstRT_Mst_Product[0]; //Sản phẩm Hiển thị

            var strWhereClause_MstProductBase = strWhereClause_Mst_Product_Get_Base(objMst_ProductBase);
            var objRT_Mst_ProductBase = GetPrdBase(strWhereClause_MstProductBase);
            var lstPrdBase = objRT_Mst_ProductBase.Lst_Mst_Product;

            var listProduct = new List<Mst_Product>();
            listProduct.Add(objMst_ProductBase);
            if (lstPrdBase != null && lstPrdBase.Count > 0)
            {
                listProduct.AddRange(lstPrdBase);
            }

            #region Thông tin tồn kho - Tìm VT tồn lớn nhất
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
            foreach (var it in listProduct)
            {
                if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1")
                {
                    lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                }
            }
            if (lstInvBalance_LastUpdInvByProduct.Count > 0)
            {
                var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    InvCode = invcode,
                    Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                };

                var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                {
                    listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                }
            }
            #endregion

            #region Khởi tạo list ProductUI

            List<Mst_ProductUI> listProductUI = new List<Mst_ProductUI>();
            if (listProduct != null && listProduct.Count > 0)
            {
                foreach (var itPrd in listProduct)
                {
                    var invCodeSuggest = "";
                    var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itPrd.ProductCode)).ToList();
                    if (lstBalanceCur.Count > 0)
                    {
                        invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                    }

                    Mst_ProductUI itPrdUI = new Mst_ProductUI
                    {
                        ProductCode = itPrd.ProductCode,
                        ProductCodeBase = itPrd.ProductCodeBase,
                        ProductCodeRoot = itPrd.ProductCodeRoot,
                        ProductCodeUser = itPrd.ProductCodeUser,
                        ProductName = itPrd.ProductName,
                        ProductType = itPrd.ProductType,
                        UnitCode = itPrd.UnitCode,
                        UPBuy = itPrd.UPBuy,
                        UPSell = itPrd.UPSell,
                        FlagActive = itPrd.FlagActive,
                        FlagLot = itPrd.FlagLot,
                        FlagSerial = itPrd.FlagSerial,
                        InvCodeSuggest = invCodeSuggest
                    };
                    listProductUI.Add(itPrdUI);
                }
            }

            #endregion

            return Json(new { Success = true, ListProduct = listProductUI });
        }

        public ActionResult SearchMstProduct(string productcode, string invcode, int recordcount = 10, int page = 0)
        {
            #region ["Product Center Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion

            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<InvF_InventoryCusReturnDtlUI>(0, recordcount)
            {
                DataList = new List<InvF_InventoryCusReturnDtlUI>()
            };

            var strWhere_Mst_Product = strWhereClause_Mst_Product(productcode, "1", "PRODUCT,COMBO", "");
            var List_Mst_Product = new List<Mst_Product>();
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
                Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                Ft_RecordCount = recordcount.ToString(),
                FlagIsDelete = "0",
                Ft_WhereClause = strWhere_Mst_Product,
                Ft_Cols_Upd = null,

                Rt_Cols_Mst_Product = "*"
            };
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
            {
                itemCount = objRT_Mst_Product.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Product.MySummaryTable.MyCount);
                pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
            }
            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
            {
                List_Mst_Product.AddRange(objRT_Mst_Product.Lst_Mst_Product);
            }

            var listDtlUI = new List<InvF_InventoryCusReturnDtlUI>();

            #region Thông tin tồn kho - Tìm VT tồn lớn nhất
            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
            foreach (var it in List_Mst_Product)
            {
                if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1" && Convert.ToString(it.ProductType) != "COMBO")
                {
                    lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                }
            }
            if (lstInvBalance_LastUpdInvByProduct.Count > 0)
            {
                var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    InvCode = invcode,
                    Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                };

                var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                {
                    listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                }
            }
            #endregion

            #region Lấy SP cùng base và Add vào list return

            var lstPrdCodeBase = List_Mst_Product.Select(m => m.ProductCodeBase.ToString()).Distinct().ToList();
            objRQ_Mst_Product.Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase);
            objRQ_Mst_Product.Tid = GetNextTId();
            objRQ_Mst_Product.Ft_RecordStart = Ft_RecordStart;
            objRQ_Mst_Product.Ft_RecordCount = Ft_RecordCount;
            var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);

            foreach (var itPrd in List_Mst_Product)
            {
                if (lstPrdCodeBase.Contains(itPrd.ProductCodeBase.ToString()))
                {
                    var invCodeSuggest = "";
                    var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itPrd.ProductCode)).ToList();
                    if (lstBalanceCur.Count > 0)
                    {
                        invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                    }

                    var itemUI = new InvF_InventoryCusReturnDtlUI()
                    {
                        ProductCode = itPrd.ProductCode,
                        ProductCodeRoot = itPrd.ProductCodeRoot,
                        mp_ProductCodeBase = itPrd.ProductCodeBase,
                        ProductCodeUser = itPrd.ProductCodeUser,
                        mp_ProductName = itPrd.ProductName,
                        UnitCode = itPrd.UnitCode,
                        UPBuy = itPrd.UPBuy,
                        mp_FlagLot = itPrd.FlagLot,
                        mp_FlagSerial = itPrd.FlagSerial,
                        FlagCombo = itPrd.ProductType != null && itPrd.ProductType.ToString().ToUpper().Equals("COMBO") ? "1" : "0",
                        InvCodeSuggest = invCodeSuggest
                    };

                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        var listPrdCheckBase = listProductBase.Where(x => x.ProductCodeBase.ToString() == itPrd.ProductCodeBase.ToString()).ToList();
                        if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                        {
                            itemUI.Lst_Mst_ProductBase = listPrdCheckBase;
                        }
                    }
                    //
                    listDtlUI.Add(itemUI);

                    //lstPrdCodeBase.Remove(itPrd.ProductCodeBase.ToString());
                }                
            }
            #endregion

            pageInfo.DataList = listDtlUI;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return JsonView("SearchProduct", pageInfo);
        }
        
        #region[Getrootbase]
        public RT_Mst_Product GetPrdBase(string strwhere)
        {
            var addressAPIs = UserState.AddressAPIs;
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
                Ft_WhereClause = strwhere,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = null,
                Rt_Cols_Mst_ProductFiles = null,
                Rt_Cols_Prd_BOM = null,
                Rt_Cols_Prd_Attribute = null,
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            return objRT_Mst_Product;
        }
        #endregion
        #endregion

        #region Get hàng hóa theo đơn hàng

        public ActionResult SearchProductInOrder(string customercode, string refno, string reftype, string productcode, string productname, string invcode, int recordcount = 10, int page = 0)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var addressDMSAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicDMS);
            var addressVelocaAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicVeloca);
            #endregion
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<InvF_InventoryCusReturnDtlUI>(0, recordcount)
            {
                DataList = new List<InvF_InventoryCusReturnDtlUI>()
            };

            try
            {
                var listDtlUI = new List<InvF_InventoryCusReturnDtlUI>();

                if (reftype == Constants.RefType.RO)//Gọi veloca
                {
                    string strRefNo = refno.Substring(3);
                    var strWhere = strWhereClause_ROProductPart(strRefNo, productname);
                    var objRQ_Ser_ROProductPart = new RQ_Ser_ROProductPart()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_Veloca_GwUserCode,
                        GwPassword = OS_Veloca_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = OS_Veloca_WAUserCode,
                        WAUserPassword = OS_Veloca_WAUserPassword,
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Ser_ROProductPart = "*",
                        Ft_WhereClause = strWhere
                    };
                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Ser_RO);
                    var objRT_Ser_ROProductPart = Ser_ROService.Instance.WA_Ser_ROProductPart_Get(objRQ_Ser_ROProductPart, addressVelocaAPIs);

                    if (objRT_Ser_ROProductPart != null && objRT_Ser_ROProductPart.Lst_Ser_ROProductPart != null && objRT_Ser_ROProductPart.Lst_Ser_ROProductPart.Count > 0)
                    {
                        var lstSer_ROProductPart = objRT_Ser_ROProductPart.Lst_Ser_ROProductPart;
                        itemCount = objRT_Ser_ROProductPart.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Ser_ROProductPart.MySummaryTable.MyCount);
                        pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);

                        // Lấy thêm các thông tin từ Product

                        var lstProductCodeSys = lstSer_ROProductPart.Select(m => m.ProductCodePart.ToString()).ToList();
                        var strProductCode = CUtils.StretchListString(lstProductCodeSys);

                        var tblMstProduct = TableName.Mst_Product + ".";
                        var sbSql = new StringBuilder();
                        if (strProductCode != "")
                        {
                            sbSql.AddWhereClause(tblMstProduct + TblMst_Product.ProductCode, strProductCode, "IN");
                        }
                        sbSql.AddWhereClause(tblMstProduct + TblMst_Product.FlagActive, "1", "=");
                        var strWhereClause = sbSql.ToString();
                        //
                        #region Thông tin hàng hóa
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
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClause,
                        };
                        var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                        if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                        {
                            lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                            if (lst_Mst_Product != null && lst_Mst_Product.Count != 0)
                            {
                                #region Thông tin tồn kho - Tìm VT tồn lớn nhất

                                List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

                                List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
                                foreach (var it in lst_Mst_Product)
                                {
                                    if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1" && Convert.ToString(it.ProductType) != "COMBO")
                                    {
                                        lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                                    }
                                }

                                if (lstInvBalance_LastUpdInvByProduct.Count > 0)
                                {
                                    var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        InvCode = invcode,
                                        Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                                    };

                                    var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                                    if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                                    {
                                        listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                                    }
                                }
                                #endregion

                                foreach (var it in lstSer_ROProductPart)
                                {
                                    // Thêm các thông tin từ MstProduct
                                    var productCode = it.ProductCodePart == null ? "" : it.ProductCodePart.ToString();
                                    var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.ToString().Equals(productCode)).FirstOrDefault();
                                    if (itMstProduct != null)
                                    {
                                        var invCodeSuggest = "";
                                        var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itMstProduct.ProductCode)).ToList();
                                        if (lstBalanceCur.Count > 0)
                                        {
                                            invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                                        }

                                        var itemUI = new InvF_InventoryCusReturnDtlUI()
                                        {
                                            ProductCode = itMstProduct.ProductCode,
                                            ProductCodeRoot = itMstProduct.ProductCodeRoot,
                                            mp_ProductCodeBase = itMstProduct.ProductCodeBase,
                                            ProductCodeUser = itMstProduct.ProductCodeUser,
                                            mp_ProductName = itMstProduct.ProductName,
                                            UnitCode = itMstProduct.UnitCode,
                                            UPBuy = itMstProduct.UPBuy,
                                            mp_FlagLot = itMstProduct.FlagLot,
                                            mp_FlagSerial = itMstProduct.FlagSerial,
                                            FlagCombo = itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") ? "1" : "0",
                                            QtyOrder = it.Qty,
                                            QtyOut = it.QtyOut,
                                            QtyReturnRemain = it.QtyReturnRemain,
                                            RefNo = it.sro_RONo,
                                            RefNoSys = it.RONoSys,
                                            InvCodeSuggest = invCodeSuggest
                                        };

                                        //
                                        listDtlUI.Add(itemUI);
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                else if (reftype == Constants.RefType.OrderDL || reftype == Constants.RefType.OrderSO) //DMS
                {
                    #region Kết nối DMS

                    var lstRpt_OrderSummary_TotalProductForInvReturn = new List<Rpt_OrderSummary_TotalProductForInvReturn>();

                    var objRQ_Rpt_OrderSummary_TotalProductForInvReturn = new RQ_Rpt_OrderSummary_TotalProductForInvReturn()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_DMS_GwUserCode,
                        GwPassword = OS_DMS_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                        Ft_RecordCount = recordcount.ToString(),
                        Rpt_OrderSummary_TotalProductForInvReturn = new Rpt_OrderSummary_TotalProductForInvReturn()
                        {
                            OrderNo = refno,
                            ProductCodeUser = !string.IsNullOrEmpty(productcode) ? productcode : null,
                            ProductName = !string.IsNullOrEmpty(productname) ? "%" + productname + "%" : null,
                            CustomerCode = !string.IsNullOrEmpty(customercode) ? customercode : null,
                            CustomerName = !string.IsNullOrEmpty(customercode) ? "%" + customercode + "%" : null,
                        },
                        Rt_Cols_Rpt_OrderSummary_TotalProductForInvReturn = "*"
                    };
                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_OrderSummary_TotalProductForInvReturn);
                    var objRT_Rpt_OrderSummary_TotalProductForInvReturn = DMS_ReportService.Instance.WA_Rpt_OrderSummary_TotalProductForInvReturn(objRQ_Rpt_OrderSummary_TotalProductForInvReturn, addressDMSAPIs);
                    if (objRT_Rpt_OrderSummary_TotalProductForInvReturn != null && objRT_Rpt_OrderSummary_TotalProductForInvReturn.Lst_Rpt_OrderSummary_TotalProductForInvReturn != null)
                    {
                        lstRpt_OrderSummary_TotalProductForInvReturn = objRT_Rpt_OrderSummary_TotalProductForInvReturn.Lst_Rpt_OrderSummary_TotalProductForInvReturn;

                        itemCount = objRT_Rpt_OrderSummary_TotalProductForInvReturn.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Rpt_OrderSummary_TotalProductForInvReturn.MySummaryTable.MyCount);
                        pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);

                        if (lstRpt_OrderSummary_TotalProductForInvReturn != null && lstRpt_OrderSummary_TotalProductForInvReturn.Count != 0)
                        {
                            // Lấy thêm các thông tin từ Product

                            var lstProductCodeSys = lstRpt_OrderSummary_TotalProductForInvReturn.Select(m => m.ProductCode.ToString()).ToList();
                            var strProductCode = CUtils.StretchListString(lstProductCodeSys);

                            var tblMstProduct = TableName.Mst_Product + ".";
                            var sbSql = new StringBuilder();
                            if (strProductCode != "")
                            {
                                sbSql.AddWhereClause(tblMstProduct + TblMst_Product.ProductCode, strProductCode, "IN");
                            }
                            sbSql.AddWhereClause(tblMstProduct + TblMst_Product.FlagActive, "1", "=");
                            var strWhereClause = sbSql.ToString();
                            //
                            #region Thông tin hàng hóa
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
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = strWhereClause,
                            };
                            var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                            if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                            {
                                lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                                if (lst_Mst_Product != null && lst_Mst_Product.Count != 0)
                                {
                                    #region Thông tin tồn kho - Tìm VT tồn lớn nhất

                                    List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

                                    List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
                                    foreach (var it in lst_Mst_Product)
                                    {
                                        if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1" && Convert.ToString(it.ProductType) != "COMBO")
                                        {
                                            lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                                        }
                                    }

                                    if (lstInvBalance_LastUpdInvByProduct.Count > 0)
                                    {
                                        var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                                            Ft_RecordStart = Ft_RecordStart,
                                            Ft_RecordCount = Ft_RecordCount,
                                            InvCode = invcode,
                                            Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                                        };

                                        var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                                        if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                                        {
                                            listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                                        }
                                    }
                                    #endregion

                                    foreach (var it in lstRpt_OrderSummary_TotalProductForInvReturn)
                                    {
                                        // Thêm các thông tin từ MstProduct
                                        var productCode = it.ProductCode == null ? "" : it.ProductCode.ToString();
                                        var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.ToString().Equals(productCode)).FirstOrDefault();
                                        if (itMstProduct != null)
                                        {
                                            var invCodeSuggest = "";
                                            var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itMstProduct.ProductCode)).ToList();
                                            if (lstBalanceCur.Count > 0)
                                            {
                                                invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                                            }

                                            var itemUI = new InvF_InventoryCusReturnDtlUI()
                                            {
                                                ProductCode = itMstProduct.ProductCode,
                                                ProductCodeRoot = itMstProduct.ProductCodeRoot,
                                                mp_ProductCodeBase = itMstProduct.ProductCodeBase,
                                                ProductCodeUser = itMstProduct.ProductCodeUser,
                                                mp_ProductName = itMstProduct.ProductName,
                                                UnitCode = itMstProduct.UnitCode,
                                                UPBuy = itMstProduct.UPBuy,
                                                mp_FlagLot = itMstProduct.FlagLot,
                                                mp_FlagSerial = itMstProduct.FlagSerial,
                                                FlagCombo = itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") ? "1" : "0",
                                                QtyOrder = it.QtyAppr,
                                                QtyOut = it.QtyInvOut,
                                                QtyReturnRemain = it.QtyInvReturnAvail,
                                                RefNo = refno,
                                                InvCodeSuggest = invCodeSuggest
                                            };

                                            //
                                            listDtlUI.Add(itemUI);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }

                pageInfo.DataList = listDtlUI;
                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("SearchProductInOrder", pageInfo);
                                
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
            return JsonViewError("SearchProductInOrder", null, resultEntry);
        }

        public ActionResult SearchProductInOrderExactly(string refno, string reftype, string invcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var addressDMSAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicDMS);
            var addressVelocaAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicVeloca);
            #endregion            

            try
            {
                var listDtlUI = new List<InvF_InventoryCusReturnDtlUI>();

                var lstRpt_OrderSummary_TotalProductForInvReturn = new List<Rpt_OrderSummary_TotalProductForInvReturn>();
                var lstSer_ROProductPart = new List<Ser_ROProductPart>();

                if (reftype == Constants.RefType.RO)//Gọi veloca
                {
                    string strRefNo = refno.Substring(3);
                    var strWhere = strWhereClause_ROProductPart(strRefNo, "");
                    var objRQ_Ser_ROProductPart = new RQ_Ser_ROProductPart()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_Veloca_GwUserCode,
                        GwPassword = OS_Veloca_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = OS_Veloca_WAUserCode,
                        WAUserPassword = OS_Veloca_WAUserPassword,
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Ser_ROProductPart = "*",
                        Ft_WhereClause = strWhere
                    };
                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Ser_RO);
                    var objRT_Ser_ROProductPart = Ser_ROService.Instance.WA_Ser_ROProductPart_Get(objRQ_Ser_ROProductPart, addressVelocaAPIs);

                    if (objRT_Ser_ROProductPart != null && objRT_Ser_ROProductPart.Lst_Ser_ROProductPart != null && objRT_Ser_ROProductPart.Lst_Ser_ROProductPart.Count > 0)
                    {
                        lstSer_ROProductPart = objRT_Ser_ROProductPart.Lst_Ser_ROProductPart;
                    }
                }
                else if (reftype == Constants.RefType.OrderDL || reftype == Constants.RefType.OrderSO) //DMS
                {

                    #region Kết nối DMS

                    var objRQ_Rpt_OrderSummary_TotalProductForInvReturn = new RQ_Rpt_OrderSummary_TotalProductForInvReturn()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_DMS_GwUserCode,
                        GwPassword = OS_DMS_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rpt_OrderSummary_TotalProductForInvReturn = new Rpt_OrderSummary_TotalProductForInvReturn()
                        {
                            OrderNo = refno
                        },
                        Rt_Cols_Rpt_OrderSummary_TotalProductForInvReturn = "*"
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Rpt_OrderSummary_TotalProductForInvReturn);
                    var objRT_Rpt_OrderSummary_TotalProductForInvReturn = DMS_ReportService.Instance.WA_Rpt_OrderSummary_TotalProductForInvReturn(objRQ_Rpt_OrderSummary_TotalProductForInvReturn, addressDMSAPIs);
                    if (objRT_Rpt_OrderSummary_TotalProductForInvReturn != null && objRT_Rpt_OrderSummary_TotalProductForInvReturn.Lst_Rpt_OrderSummary_TotalProductForInvReturn != null)
                    {
                        lstRpt_OrderSummary_TotalProductForInvReturn = objRT_Rpt_OrderSummary_TotalProductForInvReturn.Lst_Rpt_OrderSummary_TotalProductForInvReturn;
                    }

                    #endregion
                }

                #region //Lấy thêm các thông tin từ Product

                var lstProductCodeSys = new List<string>();
                if(lstSer_ROProductPart.Count > 0)
                {
                    lstProductCodeSys.AddRange(lstSer_ROProductPart.Select(m => m.ProductCodePart.ToString()).ToList());
                }
                if (lstRpt_OrderSummary_TotalProductForInvReturn.Count > 0)
                {
                    lstProductCodeSys = lstRpt_OrderSummary_TotalProductForInvReturn.Select(m => m.ProductCode.ToString()).ToList();
                }
                var strProductCode = CUtils.StretchListString(lstProductCodeSys);

                var tblMstProduct = TableName.Mst_Product + ".";
                var sbSql = new StringBuilder();
                if (strProductCode != "")
                {
                    sbSql.AddWhereClause(tblMstProduct + TblMst_Product.ProductCode, strProductCode, "IN");
                }
                sbSql.AddWhereClause(tblMstProduct + TblMst_Product.FlagActive, "1", "=");
                var strWhereClause = sbSql.ToString();
                //
                #region Thông tin hàng hóa
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
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                };
                var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                {
                    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                    if (lst_Mst_Product != null && lst_Mst_Product.Count != 0)
                    {
                        #region Thông tin tồn kho - Tìm VT tồn lớn nhất

                        List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

                        List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
                        foreach (var it in lst_Mst_Product)
                        {
                            if (Convert.ToString(it.FlagLot) != "1" && Convert.ToString(it.FlagSerial) != "1" && Convert.ToString(it.ProductType) != "COMBO")
                            {
                                lstInvBalance_LastUpdInvByProduct.Add(new Rpt_Inv_InvBalance_LastUpdInvByProduct { ProductCode = Convert.ToString(it.ProductCode) });
                            }
                        }

                        if (lstInvBalance_LastUpdInvByProduct.Count > 0)
                        {
                            var objRQ_Inv_InventoryBalance = new RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct()
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
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                InvCode = invcode,
                                Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct = lstInvBalance_LastUpdInvByProduct
                            };

                            var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                            if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                            {
                                listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                            }
                        }
                        #endregion

                        foreach (var it in lstSer_ROProductPart)
                        {
                            // Thêm các thông tin từ MstProduct
                            var productCode = it.ProductCodePart == null ? "" : it.ProductCodePart.ToString();
                            var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.ToString().Equals(productCode)).FirstOrDefault();
                            if (itMstProduct != null)
                            {
                                var invCodeSuggest = "";
                                var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itMstProduct.ProductCode)).ToList();
                                if (lstBalanceCur.Count > 0)
                                {
                                    invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                                }

                                var itemUI = new InvF_InventoryCusReturnDtlUI()
                                {
                                    ProductCode = itMstProduct.ProductCode,
                                    ProductCodeRoot = itMstProduct.ProductCodeRoot,
                                    mp_ProductCodeBase = itMstProduct.ProductCodeBase,
                                    ProductCodeUser = itMstProduct.ProductCodeUser,
                                    mp_ProductName = itMstProduct.ProductName,
                                    UnitCode = itMstProduct.UnitCode,
                                    UPBuy = itMstProduct.UPBuy,
                                    mp_FlagLot = itMstProduct.FlagLot,
                                    mp_FlagSerial = itMstProduct.FlagSerial,
                                    FlagCombo = itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") ? "1" : "0",
                                    QtyOrder = it.Qty,
                                    RefNo = it.sro_RONo,
                                    RefNoSys = it.RONoSys,
                                    RefType = "RO",
                                    CustomerCodeSys = it.Sro_CustomerCodeSys,
                                    InvCodeSuggest = invCodeSuggest
                                };

                                //
                                listDtlUI.Add(itemUI);
                            }
                        }

                        foreach (var it in lstRpt_OrderSummary_TotalProductForInvReturn)
                        {
                            // Thêm các thông tin từ MstProduct
                            var productCode = it.ProductCode == null ? "" : it.ProductCode.ToString();
                            var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.ToString().Equals(productCode)).FirstOrDefault();
                            if (itMstProduct != null)
                            {
                                var invCodeSuggest = "";
                                var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itMstProduct.ProductCode)).ToList();
                                if (lstBalanceCur.Count > 0)
                                {
                                    invCodeSuggest = lstBalanceCur[0].InvCode != null ? lstBalanceCur[0].InvCode : "";
                                }

                                var itemUI = new InvF_InventoryCusReturnDtlUI()
                                {
                                    ProductCode = itMstProduct.ProductCode,
                                    ProductCodeRoot = itMstProduct.ProductCodeRoot,
                                    mp_ProductCodeBase = itMstProduct.ProductCodeBase,
                                    ProductCodeUser = itMstProduct.ProductCodeUser,
                                    mp_ProductName = itMstProduct.ProductName,
                                    UnitCode = itMstProduct.UnitCode,
                                    UPBuy = itMstProduct.UPBuy,
                                    mp_FlagLot = itMstProduct.FlagLot,
                                    mp_FlagSerial = itMstProduct.FlagSerial,
                                    FlagCombo = itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") ? "1" : "0",
                                    QtyOrder = it.QtyAppr,
                                    RefNo = refno,
                                    RefNoSys = it.OrderNoSys,
                                    RefType = it.OrderType,
                                    CustomerCodeSys = it.CustomerCodeSys,
                                    InvCodeSuggest = invCodeSuggest
                                };

                                //
                                listDtlUI.Add(itemUI);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                return JsonView("showOptionProductInOrder", listDtlUI);
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
            return View(resultEntry);
        }

        #endregion

        #region Get hàng hóa theo phiếu xuất

        public ActionResult SearchProductInInvIn(string customercode, string refno, string productcode, string productname, int recordcount = 10, int page = 0)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<InvF_InventoryInDtl>(0, recordcount)
            {
                DataList = new List<InvF_InventoryInDtl>()
            };

            try
            {
                var listDtl = new List<InvF_InventoryInDtl>();
                string strWhereClause = strWhereClause_InvF_InventoryOut(refno, customercode, productcode, productname);
                var objRQ_InvF_InventoryIn = new RQ_InvF_InventoryIn()
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhereClause,
                    Rt_Cols_InvF_InventoryInDtl = "*"
                };

                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl != null)
                {
                    listDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;

                    itemCount = objRT_InvF_InventoryIn.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryIn.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);

                }

                var listDtlDistinct = new List<InvF_InventoryInDtl>();
                List<string> lstProductCode = new List<string>();
                foreach (var it in listDtl)
                {
                    string productCodeCur = CUtils.StrValue(it.ProductCode);
                    if (!lstProductCode.Contains(productCodeCur))
                    {
                        listDtlDistinct.Add(it);
                        lstProductCode.Add(productCodeCur);
                    }
                    else
                    {
                        var qtyCur = listDtlDistinct.SingleOrDefault(m => Convert.ToString(m.ProductCode) == productCodeCur).Qty;
                        listDtlDistinct.Where(m => Convert.ToString(m.ProductCode) == productCodeCur).ToList().ForEach(s => s.Qty = Convert.ToDouble(qtyCur) + Convert.ToDouble(it.Qty));
                    }
                }

                pageInfo.DataList = listDtlDistinct;
                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("SearchProductInInvIn", pageInfo);
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
            return View(resultEntry);
        }

        public ActionResult SearchProductInInvOutExactly(string refno)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion            

            try
            {
                var listDtl = new List<InvF_InventoryOutDtl>();
                string strWhereClause = strWhereClause_InvF_InventoryOut(refno, "", "", "");
                var objRQ_InvF_InventoryOut = new RQ_InvF_InventoryOut()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*"
                };

                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl != null)
                {
                    listDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl;
                }
                string customerCodeSys = "";
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
                {
                    customerCodeSys = CUtils.StrValue(objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0].CustomerCode);
                }

                var listDtlDistinct = new List<InvF_InventoryOutDtl>();
                List<string> lstProductCode = new List<string>();
                foreach (var it in listDtl)
                {
                    string productCodeCur = CUtils.StrValue(it.ProductCode);
                    if (!lstProductCode.Contains(productCodeCur))
                    {
                        listDtlDistinct.Add(it);
                        lstProductCode.Add(productCodeCur);
                    }
                    else
                    {
                        var qtyCur = listDtlDistinct.SingleOrDefault(m => Convert.ToString(m.ProductCode) == productCodeCur).Qty;
                        listDtlDistinct.Where(m => Convert.ToString(m.ProductCode) == productCodeCur).ToList().ForEach(s => s.Qty = Convert.ToDouble(qtyCur) + Convert.ToDouble(it.Qty));
                    }
                }

                ViewBag.CustomerCodeSys = customerCodeSys;
                return JsonView("showOptionProductInOrder", listDtlDistinct);
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
            return View(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComboAppr(string productCode, string IF_InvCusReturnNo/*, string productCodeBase*/, string productCodeUser, string productName)
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
                #region Thông tin phiếu KHTH
                InvF_InventoryCusReturn objInvF_InventoryCusReturn = new InvF_InventoryCusReturn() { IF_InvCusReturnNo = IF_InvCusReturnNo };
                var strWhere = WhereClauseInvF_InventoryCusReturn(objInvF_InventoryCusReturn, "", "", "", "", null, null, null, null);
                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                var Lst_InvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnDtl>();
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count != 0)
                {
                    Lst_InvF_InventoryCusReturnDtl = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl;
                }
                #endregion
                return JsonView("Combo", Lst_InvF_InventoryCusReturnDtl);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Combo", null, resultEntry);
        }

        #endregion

        #region strWhereClause

        private string strWhereClause_InvF_InventoryCusReturn(string if_invcusreturnno, string createdtimefrom, string createdtimeto, string apprdtimefrom,
           string apprdtimeto, string orderno, string customercode, string invcodein, string productcode, string chkpending, string chkapproved, string chkcanceled)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryCusReturn = TableName.InvF_InventoryCusReturn + ".";
            var Tbl_InvF_InventoryCusReturnDtl = TableName.InvF_InventoryCusReturnDtl + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(if_invcusreturnno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.IF_InvCusReturnNo, "%" + CUtils.StrValue(if_invcusreturnno) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(createdtimefrom))
            {
                var createDTimeUTCFrom = CUtils.StrValue(createdtimefrom) + " 00:00:00";
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(createdtimeto))
            {
                var createDTimeUTCTo = CUtils.StrValue(createdtimeto) + " 23:59:59";
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdtimefrom))
            {
                var apprDTimeUTCFrom = CUtils.StrValue(apprdtimefrom) + " 00:00:00";
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdtimeto))
            {
                var apprDTimeUTCTo = CUtils.StrValue(apprdtimeto) + " 23:59:59";
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.ApprDTimeUTC, strApprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(orderno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.OrderNo, "%" + CUtils.StrValue(orderno) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(invcodein))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.InvCodeIn, CUtils.StrValue(invcodein), "=");
            }
            if (!CUtils.IsNullOrEmpty(customercode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CustomerCode, CUtils.StrValue(customercode), "=");
            }
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
            }

            var status = "";
            if (!CUtils.IsNullOrEmpty(chkpending))
            {
                if (chkpending.Equals("1"))
                {
                    status += "PENDING";
                }

            }
            if (!CUtils.IsNullOrEmpty(chkapproved) && chkapproved.Equals("1"))
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",APPROVE";
                }
                else
                {
                    status = "APPROVE";
                }
            }
            if (!CUtils.IsNullOrEmpty(chkcanceled) && chkcanceled.Equals("1"))
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",CANCEL";
                }
                else
                {
                    status = "CANCEL";
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.IF_InvCusReturnStatus, status, "in");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_InvF_InventoryCusReturnByID(string if_invcusreturnno)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryCusReturn = TableName.InvF_InventoryCusReturn + ".";
            if (!CUtils.IsNullOrEmpty(if_invcusreturnno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.IF_InvCusReturnNo, CUtils.StrValue(if_invcusreturnno), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereGetProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");

            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValueOrNull(data.ProductCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereGetProductByPrdBarcode(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");
            
            if (!CUtils.IsNullOrEmpty(data.ProductBarCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductBarCode, CUtils.StrValueOrNull(data.ProductBarCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereSearchProductProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");

            strWhereClause = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValueOrNull(data.ProductCodeUser) + "%", "like");
                strWhereClause += " and (" + Tbl_Mst_Product + TblMst_Product.ProductCodeUser + " like '%" + CUtils.StrValueOrNull(data.ProductCodeUser) + "%'";
            }

            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValueOrNull(data.ProductName) + "%", "like");
                strWhereClause += " or " + "Mst_Product.ProductName like '%" + data.ProductName + "%')";
            }
            return strWhereClause;
        }
        private string strWhereSearchProductImport(List<string> lstProductCodeUser, List<string> lstProductCodeBase)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");

            if (lstProductCodeUser != null && lstProductCodeUser.Count > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StretchListString(lstProductCodeUser), "in");
            }

            if (lstProductCodeBase != null && lstProductCodeBase.Count > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, CUtils.StretchListString(lstProductCodeBase), "in");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Product_Search(Mst_Product model, string strStt, List<Prd_Attribute> listPrd_Attribute)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var Tbl_Prd_Attribute = TableName.Prd_Attribute + ".";
            var sbSql = new StringBuilder();

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");

            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, model.ProductCode, "=");
            }

            if (!CUtils.IsNullOrEmpty(model.ProductGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductGrpCode, model.ProductGrpCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductLevelSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductLevelSys, model.ProductLevelSys, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.BrandCode, model.BrandCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.UnitCode, model.UnitCode, "=");
            }

            if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
            {
                foreach (var item in listPrd_Attribute)
                {
                    sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeCode, item.AttributeCode, "=");
                    sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeValue, "%" + item.AttributeValue + "%", "like");
                }
            }

            strWhereClause = sbSql.ToString();
            strWhereClause += " and (";
            if (!CUtils.IsNullOrEmpty(model.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + model.ProductName + "%", "like", "or");
            }
            strWhereClause += sbSql.ToString();
            strWhereClause += ")";

            return strWhereClause;
        }

        private string strWhereClause_Mst_Product_Get_Base(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT,COMBO", " in ");
            if (!CUtils.IsNullOrEmpty(model.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, model.ProductCodeBase, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductCodeRoot))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCodeRoot, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, model.ProductCode, "!=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Product(string productcode = "", string flagactive = "", string producttype = "", string flagsell = "")
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                var sbSqlProductCode = new StringBuilder();
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode)  + "%", "like");
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(productcode) + "%", "like", "OR");
                sbSql.Append(string.Format("({0})", sbSqlProductCode));
            }
            if (!CUtils.IsNullOrEmpty(flagactive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, CUtils.StrValue(flagactive), "=");
            }
            if (!CUtils.IsNullOrEmpty(producttype))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, CUtils.StrValue(producttype), "in");
            }
            if (!CUtils.IsNullOrEmpty(flagsell))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, CUtils.StrValue(flagsell), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        public string strWhereClause_RO_By_RONo(string rono)
        {
            var strWhereClause = "";
            var Tbl_Ser_RO = TableName.Ser_RO + ".";
            var sbSql = new StringBuilder();

            sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.FlagRORepair, "1", "=");
            if (!CUtils.IsNullOrEmpty(rono))
            {
                sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.RONo, rono, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        public string strWhereClause_ROProductPart(string rono, string productnamepart)
        {
            var strWhereClause = "";
            var Tbl_Ser_RO = TableName.Ser_RO + ".";
            var Tbl_Ser_ROProductPart = TableName.Ser_ROProductPart + ".";
            var sbSql = new StringBuilder();

            sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.FlagRORepair, "1", "=");
            sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.ROStatus, "CANCEL", "!=");
            if (!CUtils.IsNullOrEmpty(rono))
            {
                sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.RONo, rono, "=");
            }
            strWhereClause = sbSql.ToString();

            var strWhereClausePart = "";
            if (!CUtils.IsNullOrEmpty(productnamepart))
            {
                var sbSqlPart = new StringBuilder();
                sbSqlPart.AddWhereClause(Tbl_Ser_ROProductPart + TblSer_ROProductPart.ProductCodeUserPart, "%" + productnamepart + "%", "like");
                sbSqlPart.AddWhereClause(Tbl_Ser_ROProductPart + TblSer_ROProductPart.ProductNamePart, "%" + productnamepart + "%", "like", "or");
                strWhereClausePart = sbSqlPart.ToString();
            }

            if (!string.IsNullOrEmpty(strWhereClausePart))
            {
                strWhereClause += " and (" + strWhereClausePart + ")";
            }

            return strWhereClause;
        }

        private string strWhereClause_InvF_InventoryOut(string refno, string customercode, string productcode, string productname)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryOut = TableName.InvF_InventoryOut + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(refno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.IF_InvOutNo, CUtils.StrValue(refno), "=");
            }
            if (!CUtils.IsNullOrEmpty(customercode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.CustomerCode, CUtils.StrValue(customercode), "=");
            }
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(productname))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(productname) + "%", "like");
            }

            sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.IF_InvOutStatus, "APPROVE", "=");

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        public string WhereClauseInvF_InventoryCusReturn(InvF_InventoryCusReturn objInvF_InventoryCusReturn, string strdatefrom, string strdateto, string strApprdatefrom, string strApprdateto, string pending, string approved, string canceled, string orgid)
        {
            var strWhere = "";
            var datefrom = "";
            if (strdatefrom != null && strdatefrom != "")
            {
                datefrom = strdatefrom + " 00:00:00";
            }
            var dateto = "";
            if (strdateto != null && strdateto != "")
            {
                dateto = strdateto + " 23:59:59";
            }
            var apprdatefrom = "";
            if (strApprdatefrom != null && strApprdatefrom != "")
            {
                apprdatefrom = strApprdatefrom + " 00:00:00";
            }
            var apprdateto = "";
            if (strApprdateto != null && strApprdateto != "")
            {
                apprdateto = strApprdateto + " 23:59:59";
            }

            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryCusReturn = TableName.InvF_InventoryCusReturn + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryCusReturn.IF_InvCusReturnNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.IF_InvCusReturnNo, CUtils.StrValue(objInvF_InventoryCusReturn.IF_InvCusReturnNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryCusReturn.RefNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.RefNo, CUtils.StrValue(objInvF_InventoryCusReturn.RefNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(datefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(dateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdatefrom))
            {
                var apprDTimeUTCFrom = CUtils.StrValue(apprdatefrom) + " 00:00:00";
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.ApprDTimeUTC, apprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdateto))
            {
                var apprDTimeUTCTo = CUtils.StrValue(apprdateto) + " 23:59:59";
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.ApprDTimeUTC, apprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryCusReturn.InvInType))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.InvInType, CUtils.StrValue(objInvF_InventoryCusReturn.InvInType), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryCusReturn.CustomerCode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.CustomerCode, CUtils.StrValue(objInvF_InventoryCusReturn.CustomerCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryCusReturn.InvCodeIn))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.InvCodeIn, CUtils.StrValue(objInvF_InventoryCusReturn.InvCodeIn), "=");
            }
            //if (!CUtils.IsNullOrEmpty(productcode))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
            //}
            if (!CUtils.IsNullOrEmpty(orgid))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.OrgID, CUtils.StrValue(orgid), "=");
            }
            var status = "";
            if (!CUtils.IsNullOrEmpty(pending))
            {
                if (pending.Equals("1"))
                {
                    status += "PENDING";
                }

            }
            if (!CUtils.IsNullOrEmpty(approved) && approved.Equals("1"))
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",APPROVE";
                }
                else
                {
                    status = "APPROVE";
                }
            }
            if (!CUtils.IsNullOrEmpty(canceled) && canceled.Equals("1"))
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",CANCEL";
                }
                else
                {
                    status = "CANCEL";
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryCusReturn + TblInvF_InventoryCusReturn.IF_InvCusReturnStatus, status, "in");
            }

            strWhere = sbSql.ToString();
            return strWhere;
        }
        #endregion

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportProduct(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInventoryCusReturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
            if (ModelState.IsValid)
            {
                try
                {

                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }

                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != GetImportDicColumsTemplateProduct().Count)
                        {
                            exitsData = "File excel import không hợp lệ!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        #region ["Mst_Inventory"]
                        var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                        if (!string.IsNullOrEmpty(invBUPattern))
                        {
                            strWhere_Mst_Inventory += " and ";
                            strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                        }

                        var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
                        #endregion
                        #region["Check null"]

                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã hàng hóa không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (dr["Qty"] == null || dr["Qty"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Số lượng không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                if (!CUtils.IsNumeric(dr["Qty"]))
                                {
                                    exitsData = "Số lượng không phải định dạng số!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDouble(dr["Qty"].ToString().Trim()) < 0)
                                    {
                                        exitsData = "Số lượng không được < 0!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }

                            if (dr["InvCodeInActual"] == null || dr["InvCodeInActual"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Vị trí không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                string invCodeInActual = dr["InvCodeInActual"].ToString().Trim();
                                var lstInvCheck = list_Mst_Inventory.Where(m => Convert.ToString(m.InvCode) == invCodeInActual).ToList();
                                if (lstInvCheck == null || lstInvCheck.Count == 0)
                                {
                                    exitsData = "Vị trí '" + invCodeInActual + "' không có trong hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }
                        #endregion

                        #region["Check mã hàng hóa trùng"]

                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var productCodeUser1 = CUtils.StrValue(table.Rows[i]["ProductCodeUser"]);
                            var invCodeInActual1 = CUtils.StrValue(table.Rows[i]["InvCodeInActual"]);
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var productCodeUser2 = CUtils.StrValue(table.Rows[j]["ProductCodeUser"]);
                                    var invCodeInActual2 = CUtils.StrValue(table.Rows[j]["InvCodeInActual"]);
                                    if (productCodeUser1.Equals(productCodeUser2) && invCodeInActual1.Equals(invCodeInActual2))
                                    {
                                        exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có vị trí '" + invCodeInActual1 + "' trong file excel lặp!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]                        
                        var lstProductCodeUser = new List<string>();
                        var lstProductCodeUser_Distinct = new List<string>();

                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryCusReturnDtlUI>(table);
                        lstProductCodeUser_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => CUtils.StrValue(item.ProductCodeUser)).Distinct().ToList();
                        lstProductCodeUser = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => string.Format("'{0}'", item.ProductCodeUser)).ToList();
                        lstProductCodeUser = lstProductCodeUser.Distinct().ToList();
                        var lstPrdCodeBase = new List<string>();

                        #region Lấy danh sách hàng hóa

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
                            Ft_WhereClause = strWhereSearchProductImport(lstProductCodeUser, null),
                            Ft_Cols_Upd = null,
                            // RQ_Mst_Product
                            Rt_Cols_Mst_Product = "*",
                            Rt_Cols_Mst_ProductImages = "",
                            Rt_Cols_Mst_ProductFiles = "",
                            Rt_Cols_Prd_BOM = "",
                            Rt_Cols_Prd_Attribute = "",
                        };

                        var listProduct = WA_Mst_Product_Get(objRQ_Mst_Product);

                        #endregion

                        foreach(var productCodeUser in lstProductCodeUser_Distinct)
                        {
                            var listPrdCheck = new List<Mst_Product>();
                            if (listProduct != null && listProduct.Count > 0)
                            {
                                var objMst_Product = listProduct.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).FirstOrDefault();
                                if (objMst_Product == null)
                                {
                                    exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var fQty = 0.0;
                                    var strInvCodeInActual = "";

                                    var productCodeBase = CUtils.StrValue(objMst_Product.ProductCodeBase);
                                    var objInvF_InventoryInDtlUI = new InvF_InventoryCusReturnDtlUI()
                                    {
                                        ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                        mp_ProductCodeBase = productCodeBase,
                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                        ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                        mp_ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                        UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                        Qty = fQty,
                                        ificrc_UPCusReturn = CUtils.StrValue(objMst_Product.UPBuy),
                                        mp_FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                        mp_FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                        ificrc_UPCusReturnDesc = 0,
                                        InvCodeInActual = strInvCodeInActual,
                                        Remark = "",
                                        Lst_Product_InvCodeInActual = new List<InvF_InventoryCusReturnDtlUI>(),
                                    };
                                    var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                    if(listDtlImportCur != null && listDtlImportCur.Count > 0)
                                    {
                                        var i = 0;
                                        foreach(var _it in listDtlImportCur)
                                        {
                                            var objProduct_InvCodeInActual = new InvF_InventoryCusReturnDtlUI()
                                            {
                                                ProductCode = CUtils.StrValue(objMst_Product.ProductCode),

                                                InvCodeInActual = CUtils.StrValue(_it.InvCodeInActual),
                                                Qty = CUtils.StrValue(_it.Qty),
                                            };
                                            objInvF_InventoryInDtlUI.Lst_Product_InvCodeInActual.Add(objProduct_InvCodeInActual);

                                            fQty += CUtils.ConvertToDouble(_it.Qty);
                                            if (i != 0)
                                            {
                                                strInvCodeInActual += ", ";
                                            }
                                            strInvCodeInActual += CUtils.StrValue(_it.InvCodeInActual);

                                            objInvF_InventoryInDtlUI.Qty = fQty;
                                            objInvF_InventoryInDtlUI.InvCodeInActual = strInvCodeInActual;
                                            i++;
                                        }
                                    }
                                    listInventoryCusReturnDtlUI.Add(objInvF_InventoryInDtlUI);
                                    if (!lstPrdCodeBase.Contains(productCodeBase))
                                    {
                                        lstPrdCodeBase.Add(productCodeBase);
                                    }
                                }
                            }
                            else
                            {
                                exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }

                        #region Lấy SP cùng base và Add vào list return

                        objRQ_Mst_Product.Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase);
                        var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
                        if(listProductBase != null && listProductBase.Count > 0)
                        {
                            foreach(var item in listInventoryCusReturnDtlUI)
                            {
                                var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.mp_ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                                if(listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                                {
                                    foreach(var _it in listPrdCheckBase)
                                    {
                                        var objInvFInventoryCusReturnDtlUI = new InvF_InventoryCusReturnDtlUI();
                                        var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                        var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                        var productCode = CUtils.StrValue(_it.ProductCode);
                                        var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                        var productName = CUtils.StrValue(_it.ProductName);
                                        var unitCode = CUtils.StrValue(_it.UnitCode);
                                        var qty = "0";
                                        var uPCusreturn = CUtils.StrValue(_it.UPBuy);
                                        var uPCusreturnDesc = "0";
                                        var invCodeInActual = "";
                                        var flagLot = CUtils.StrValue(_it.FlagLot);
                                        var flagSerial = CUtils.StrValue(_it.FlagSerial);
                                        var remark = "";
                                        var objMst_Product = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(_it.ProductCode))).FirstOrDefault();
                                        if(objMst_Product != null)
                                        {
                                            qty = CUtils.StrValue(objMst_Product.Qty);
                                            uPCusreturn = CUtils.StrValue(objMst_Product.ificrc_UPCusReturn);
                                            invCodeInActual = CUtils.StrValue(objMst_Product.InvCodeInActual);
                                        }
                                        objInvFInventoryCusReturnDtlUI.ProductCodeRoot = productCodeRoot;
                                        objInvFInventoryCusReturnDtlUI.mp_ProductCodeBase = productCodeBase;
                                        objInvFInventoryCusReturnDtlUI.ProductCode = productCode;
                                        objInvFInventoryCusReturnDtlUI.ProductCodeUser = productCodeUser;
                                        objInvFInventoryCusReturnDtlUI.mp_ProductName = productName;
                                        objInvFInventoryCusReturnDtlUI.UnitCode = unitCode;
                                        objInvFInventoryCusReturnDtlUI.Qty = qty;
                                        objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturn = uPCusreturn;
                                        objInvFInventoryCusReturnDtlUI.mp_FlagLot = flagLot;
                                        objInvFInventoryCusReturnDtlUI.mp_FlagSerial = flagSerial;
                                        objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturnDesc = uPCusreturnDesc;
                                        objInvFInventoryCusReturnDtlUI.InvCodeInActual = invCodeInActual;
                                        objInvFInventoryCusReturnDtlUI.Remark = remark;

                                        if(item.Lst_InvF_InventoryCusreturnDtlUI == null || item.Lst_InvF_InventoryCusreturnDtlUI.Count == 0)
                                        {
                                            item.Lst_InvF_InventoryCusreturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
                                        }
                                        item.Lst_InvF_InventoryCusreturnDtlUI.Add(objInvFInventoryCusReturnDtlUI);
                                    }
                                }
                            }
                        }

                        #endregion
                        #endregion
                        return Json(new { Success = true, ListInventoryCusReturnCoverUI = listInventoryCusReturnDtlUI });
                    }
                    else
                    {
                        exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
            }
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportLot(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInvFInventoryCusReturnInstLot = new List<InvF_InventoryCusReturnInstLot>();
            var listInvFInventoryCusReturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
            var lstProductCodeUser = new List<string>();
            var lstProductCodeUser_Distinct = new List<string>();
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {
                    resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != GetImportDicColumsTemplateLot().Count)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    #region ["Mst_Inventory"]
                    var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                    if (!string.IsNullOrEmpty(invBUPattern))
                    {
                        strWhere_Mst_Inventory += " and ";
                        strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                    }

                    var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
                    #endregion
                    #region["Check null"]
                    foreach (DataRow dr in table.Rows)
                    {
                        if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductCodeUser]))
                        {
                            exitsData = "Mã hàng hóa không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInDtl.Qty]))
                        {
                            exitsData = "Số lượng không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            var qty = CUtils.StrValue(dr[TblInvF_InventoryInDtl.Qty]);
                            if (!CUtils.IsNumeric(qty))
                            {
                                exitsData = "Số lượng không phải định dạng số!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var fQty = CUtils.ConvertToDecimal(qty);
                                if (fQty < 0)
                                {
                                    exitsData = "Số lượng không được < 0!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }

                        if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInDtl.InvCodeInActual]))
                        {
                            exitsData = "Vị trí không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            string invCodeInActual = CUtils.StrValue(dr[TblInvF_InventoryInDtl.InvCodeInActual]);
                            var objMst_Inventory = list_Mst_Inventory.Where(it => !CUtils.IsNullOrEmpty(it.InvCode) && CUtils.StrValue(it.InvCode).Equals(invCodeInActual)).FirstOrDefault();
                            if (objMst_Inventory == null)
                            {
                                exitsData = "Vị trí '" + invCodeInActual + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }
                    }
                    #endregion
                    #region["Check mã hàng hóa trùng"]

                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        var productCodeUser1 = CUtils.StrValue(table.Rows[i][TblMst_Product.ProductCodeUser]);
                        var invCodeInActual1 = CUtils.StrValue(table.Rows[i][TblInvF_InventoryInDtl.InvCodeInActual]);
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            if (i != j)
                            {
                                var productCodeUser2 = CUtils.StrValue(table.Rows[j][TblMst_Product.ProductCodeUser]);
                                var invCodeInActual2 = CUtils.StrValue(table.Rows[j][TblInvF_InventoryInDtl.InvCodeInActual]);
                                if (productCodeUser1.Equals(productCodeUser2) && invCodeInActual1.Equals(invCodeInActual2))
                                {
                                    exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có vị trí '" + invCodeInActual1 + "' trong file excel lặp!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }
                    }
                    #endregion
                    #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]
                    var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryCusReturnDtlUI>(table);
                    lstProductCodeUser_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => CUtils.StrValue(item.ProductCodeUser)).Distinct().ToList();
                    lstProductCodeUser = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => string.Format("'{0}'", item.ProductCodeUser)).ToList();
                    lstProductCodeUser = lstProductCodeUser.Distinct().ToList();
                    var lstPrdCodeBase = new List<string>();

                    #region["Lấy danh sách hàng hóa"]
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
                        Ft_WhereClause = strWhereSearchProductImport(lstProductCodeUser, null),
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Product
                        Rt_Cols_Mst_Product = "*",
                        Rt_Cols_Mst_ProductImages = "",
                        Rt_Cols_Mst_ProductFiles = "",
                        Rt_Cols_Prd_BOM = "",
                        Rt_Cols_Prd_Attribute = "",
                    };

                    var listProduct = WA_Mst_Product_Get(objRQ_Mst_Product);

                    #endregion
                    foreach (var productCodeUser in lstProductCodeUser_Distinct)
                    {
                        var listPrdCheck = new List<Mst_Product>();
                        if (listProduct != null && listProduct.Count > 0)
                        {
                            var objMst_Product = listProduct.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).FirstOrDefault();
                            if (objMst_Product == null)
                            {
                                exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var fQty = 0.0;
                                var strInvCodeInActual = "";

                                var productCodeBase = CUtils.StrValue(objMst_Product.ProductCodeBase);
                                var objInvF_InventoryCusReturnDtlUI = new InvF_InventoryCusReturnDtlUI()
                                {
                                    ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                    mp_ProductCodeBase = productCodeBase,
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    mp_ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    Qty = fQty,
                                    UPCusReturn = CUtils.StrValue(objMst_Product.UPBuy),
                                    mp_FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    mp_FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                    ificrc_UPCusReturnDesc = 0,
                                    InvCodeInActual = strInvCodeInActual,
                                    Remark = "",
                                    Lst_Product_InvCodeInActual = new List<InvF_InventoryCusReturnDtlUI>(),
                                    Lst_Product_InvLot = new List<InvF_InventoryCusReturnDtlUI>(),
                                };
                                var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                if (listDtlImportCur != null && listDtlImportCur.Count > 0)
                                {
                                    var i = 0;
                                    foreach (var _it in listDtlImportCur)
                                    {
                                        var objProduct_InvCodeInActual = new InvF_InventoryCusReturnDtlUI()
                                        {
                                            ProductCode = CUtils.StrValue(objMst_Product.ProductCode),

                                            InvCodeInActual = CUtils.StrValue(_it.InvCodeInActual),
                                            Qty = _it.Qty,
                                        };
                                        objInvF_InventoryCusReturnDtlUI.Lst_Product_InvCodeInActual.Add(objProduct_InvCodeInActual);

                                        fQty += CUtils.ConvertToDouble(_it.Qty);
                                        if (i != 0)
                                        {
                                            strInvCodeInActual += ", ";
                                        }
                                        strInvCodeInActual += CUtils.StrValue(_it.InvCodeInActual);

                                        objInvF_InventoryCusReturnDtlUI.Qty = fQty;
                                        objInvF_InventoryCusReturnDtlUI.InvCodeInActual = strInvCodeInActual;
                                        objInvF_InventoryCusReturnDtlUI.Remark = _it.Remark;
                                        i++;
                                    }
                                }

                                foreach (var _is in listDtlImport)
                                {
                                    var objLot = new InvF_InventoryCusReturnDtlUI()
                                    {
                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                        ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                        InvCodeInActual = _is.InvCodeInActual,
                                        Qty = _is.Qty,
                                        ProductLotNo = _is.ProductLotNo,
                                        ProductionDate = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(_is.ProductionDate), Nonsense.DATE_TIME_FORMAT, UserState.UtcOffset),
                                        ExpiredDate = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(_is.ExpiredDate), Nonsense.DATE_TIME_FORMAT, UserState.UtcOffset),
                                    };
                                    objInvF_InventoryCusReturnDtlUI.Lst_Product_InvLot.Add(objLot);
                                }

                                listInvFInventoryCusReturnDtlUI.Add(objInvF_InventoryCusReturnDtlUI);
                                if (!lstPrdCodeBase.Contains(productCodeBase))
                                {
                                    lstPrdCodeBase.Add(productCodeBase);
                                }
                            }
                        }
                        else
                        {
                            exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                    }
                    #region["Lấy SP cùng base và Add vào list return"]
                    objRQ_Mst_Product.Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase);
                    var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        foreach (var item in listInvFInventoryCusReturnDtlUI)
                        {
                            var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.mp_ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                            if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                            {
                                foreach (var _it in listPrdCheckBase)
                                {
                                    var objInvFInventoryCusReturnDtlUI = new InvF_InventoryCusReturnDtlUI();
                                    var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                    var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                    var productCode = CUtils.StrValue(_it.ProductCode);
                                    var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                    var productName = CUtils.StrValue(_it.ProductName);
                                    var productType = CUtils.StrValue(_it.ProductType);
                                    var unitCode = CUtils.StrValue(_it.UnitCode);
                                    var qty = "0";
                                    var ificrc_UPCusReturn = CUtils.StrValue(_it.UPBuy);
                                    var ificrc_UPCusReturnDesc = "0";
                                    var invCodeInActual = "";
                                    var flagLot = CUtils.StrValue(_it.FlagLot);
                                    var flagSerial = CUtils.StrValue(_it.FlagSerial);
                                    var remark = "";

                                    var objMst_Product = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(_it.ProductCode))).FirstOrDefault();
                                    if (objMst_Product != null)
                                    {
                                        qty = CUtils.StrValue(1);
                                        invCodeInActual = CUtils.StrValue(objMst_Product.InvCodeInActual);
                                    }

                                    objInvFInventoryCusReturnDtlUI.ProductCodeRoot = productCodeRoot;
                                    objInvFInventoryCusReturnDtlUI.mp_ProductCodeBase = productCodeBase;
                                    objInvFInventoryCusReturnDtlUI.ProductCode = productCode;
                                    objInvFInventoryCusReturnDtlUI.ProductCodeUser = productCodeUser;
                                    objInvFInventoryCusReturnDtlUI.mp_ProductName = productName;
                                    objInvFInventoryCusReturnDtlUI.UnitCode = unitCode;
                                    objInvFInventoryCusReturnDtlUI.Qty = qty;
                                    objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturn = ificrc_UPCusReturn;
                                    objInvFInventoryCusReturnDtlUI.mp_FlagLot = flagLot;
                                    objInvFInventoryCusReturnDtlUI.mp_FlagSerial = flagSerial;
                                    objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturnDesc = ificrc_UPCusReturnDesc;
                                    objInvFInventoryCusReturnDtlUI.InvCodeInActual = invCodeInActual;
                                    objInvFInventoryCusReturnDtlUI.Remark = remark;

                                    if (item.Lst_InvF_InventoryCusreturnDtlUI == null || item.Lst_InvF_InventoryCusreturnDtlUI.Count == 0)
                                    {
                                        item.Lst_InvF_InventoryCusreturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
                                    }
                                    item.Lst_InvF_InventoryCusreturnDtlUI.Add(objInvFInventoryCusReturnDtlUI);
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                    return Json(new { Success = true, ListInvFInventoryCusReturnDtlUI = listInvFInventoryCusReturnDtlUI });
                }
                else
                {
                    exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }

            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportSerial(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInvFInventoryCusReturnInstSerial = new List<InvF_InventoryCusReturnInstSerial>();
            var listInvFInventoryCusReturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
            var lstProductCodeUser = new List<string>();
            var lstProductCodeUser_Distinct = new List<string>();
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {
                    resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != GetImportDicColumsTemplateSerial().Count)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    #region ["Mst_Inventory"]
                    var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                    if (!string.IsNullOrEmpty(invBUPattern))
                    {
                        strWhere_Mst_Inventory += " and ";
                        strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                    }

                    var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
                    #endregion
                    #region["Check null"]
                    foreach (DataRow dr in table.Rows)
                    {
                        if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductCodeUser]))
                        {
                            exitsData = "Mã hàng hóa không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }

                        if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryOutFGInstSerial.SerialNo]))
                        {
                            exitsData = "Số lượng không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }

                        if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInDtl.InvCodeInActual]))
                        {
                            exitsData = "Vị trí không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            string invCodeInActual = CUtils.StrValue(dr[TblInvF_InventoryInDtl.InvCodeInActual]);
                            var objMst_Inventory = list_Mst_Inventory.Where(it => !CUtils.IsNullOrEmpty(it.InvCode) && CUtils.StrValue(it.InvCode).Equals(invCodeInActual)).FirstOrDefault();
                            if (objMst_Inventory == null)
                            {
                                exitsData = "Vị trí '" + invCodeInActual + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }
                    }
                    #endregion
                    #region["Check mã hàng hóa trùng"]

                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        var productCodeUser1 = CUtils.StrValue(table.Rows[i][TblMst_Product.ProductCodeUser]);
                        var invCodeInActual1 = CUtils.StrValue(table.Rows[i][TblInvF_InventoryInDtl.InvCodeInActual]);
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            if (i != j)
                            {
                                var productCodeUser2 = CUtils.StrValue(table.Rows[j][TblMst_Product.ProductCodeUser]);
                                var invCodeInActual2 = CUtils.StrValue(table.Rows[j][TblInvF_InventoryInDtl.InvCodeInActual]);
                                if (productCodeUser1.Equals(productCodeUser2) && invCodeInActual1.Equals(invCodeInActual2))
                                {
                                    exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có vị trí '" + invCodeInActual1 + "' trong file excel lặp!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }
                    }
                    #endregion
                    #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]
                    var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryInDtlUI>(table);
                    lstProductCodeUser_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => CUtils.StrValue(item.ProductCodeUser)).Distinct().ToList();
                    lstProductCodeUser = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => string.Format("'{0}'", item.ProductCodeUser)).ToList();
                    lstProductCodeUser = lstProductCodeUser.Distinct().ToList();
                    var lstPrdCodeBase = new List<string>();

                    #region["Lấy danh sách hàng hóa"]
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
                        Ft_WhereClause = strWhereSearchProductImport(lstProductCodeUser, null),
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Product
                        Rt_Cols_Mst_Product = "*",
                        Rt_Cols_Mst_ProductImages = "",
                        Rt_Cols_Mst_ProductFiles = "",
                        Rt_Cols_Prd_BOM = "",
                        Rt_Cols_Prd_Attribute = "",
                    };

                    var listProduct = WA_Mst_Product_Get(objRQ_Mst_Product);
                    #endregion
                    foreach (var productCodeUser in lstProductCodeUser_Distinct)
                    {
                        var listPrdCheck = new List<Mst_Product>();
                        if (listProduct != null && listProduct.Count > 0)
                        {
                            var objMst_Product = listProduct.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).FirstOrDefault();
                            if (objMst_Product == null)
                            {
                                exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var fQty = 0.0;
                                var strInvCodeInActual = "";

                                var productCodeBase = CUtils.StrValue(objMst_Product.ProductCodeBase);
                                var objInvF_InventoryCusReturnDtlUI = new InvF_InventoryCusReturnDtlUI()
                                {
                                    ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                    mp_ProductCodeBase = productCodeBase,
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    mp_ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    Qty = fQty,
                                    ificrc_UPCusReturn = CUtils.StrValue(objMst_Product.UPBuy),
                                    mp_FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    mp_FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                    ificrc_UPCusReturnDesc = 0,
                                    InvCodeInActual = strInvCodeInActual,
                                    Remark = "",
                                    SerialNo = "",
                                    Lst_Product_InvCodeInActual = new List<InvF_InventoryCusReturnDtlUI>(),
                                    Lst_Product_InvSerial = new List<InvF_InventoryCusReturnDtlUI>(),
                                };

                                var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                if (listDtlImportCur != null && listDtlImportCur.Count > 0)
                                {
                                    var i = 0;
                                    foreach (var _it in listDtlImportCur)
                                    {
                                        var objProduct_InvCodeInActual = new InvF_InventoryCusReturnDtlUI()
                                        {
                                            ProductCode = CUtils.StrValue(objMst_Product.ProductCode),

                                            InvCodeInActual = CUtils.StrValue(_it.InvCodeInActual),
                                            Qty = 1,
                                        };
                                        objInvF_InventoryCusReturnDtlUI.Lst_Product_InvCodeInActual.Add(objProduct_InvCodeInActual);

                                        fQty += 1;
                                        if (i != 0)
                                        {
                                            strInvCodeInActual += ", ";
                                        }
                                        strInvCodeInActual += CUtils.StrValue(_it.InvCodeInActual);

                                        objInvF_InventoryCusReturnDtlUI.Qty = fQty;
                                        objInvF_InventoryCusReturnDtlUI.InvCodeInActual = strInvCodeInActual;
                                        i++;
                                    }
                                }

                                foreach (var _is in listDtlImport)
                                {
                                    var objSerial = new InvF_InventoryCusReturnDtlUI()
                                    {
                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                        SerialNo = _is.SerialNo,
                                        InvCodeInActual = _is.InvCodeInActual
                                    };
                                    objInvF_InventoryCusReturnDtlUI.Lst_Product_InvSerial.Add(objSerial);
                                }

                                listInvFInventoryCusReturnDtlUI.Add(objInvF_InventoryCusReturnDtlUI);
                                if (!lstPrdCodeBase.Contains(productCodeBase))
                                {
                                    lstPrdCodeBase.Add(productCodeBase);
                                }
                            }
                        }
                        else
                        {
                            exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong hệ thống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                    }
                    #region["Lấy SP cùng base và Add vào list return"]
                    objRQ_Mst_Product.Ft_WhereClause = strWhereSearchProductImport(null, lstPrdCodeBase);
                    var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        foreach (var item in listInvFInventoryCusReturnDtlUI)
                        {
                            var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.mp_ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                            if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                            {
                                foreach (var _it in listPrdCheckBase)
                                {
                                    var objInvFInventoryCusReturnDtlUI = new InvF_InventoryCusReturnDtlUI();
                                    var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                    var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                    var productCode = CUtils.StrValue(_it.ProductCode);
                                    var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                    var productName = CUtils.StrValue(_it.ProductName);
                                    var productType = CUtils.StrValue(_it.ProductType);
                                    var unitCode = CUtils.StrValue(_it.UnitCode);
                                    var qty = "0";
                                    var ificrc_UPCusReturn = CUtils.StrValue(_it.UPBuy);
                                    var ificrc_UPCusReturnDesc = "0";
                                    var invCodeInActual = "";
                                    var flagLot = CUtils.StrValue(_it.FlagLot);
                                    var flagSerial = CUtils.StrValue(_it.FlagSerial);
                                    var remark = "";

                                    var objMst_Product = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(_it.ProductCode))).FirstOrDefault();
                                    if (objMst_Product != null)
                                    {
                                        qty = CUtils.StrValue(1);
                                        invCodeInActual = CUtils.StrValue(objMst_Product.InvCodeInActual);
                                    }

                                    objInvFInventoryCusReturnDtlUI.ProductCodeRoot = productCodeRoot;
                                    objInvFInventoryCusReturnDtlUI.mp_ProductCodeBase = productCodeBase;
                                    objInvFInventoryCusReturnDtlUI.ProductCode = productCode;
                                    objInvFInventoryCusReturnDtlUI.ProductCodeUser = productCodeUser;
                                    objInvFInventoryCusReturnDtlUI.mp_ProductName = productName;
                                    objInvFInventoryCusReturnDtlUI.UnitCode = unitCode;
                                    objInvFInventoryCusReturnDtlUI.Qty = qty;
                                    objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturn = ificrc_UPCusReturn;
                                    objInvFInventoryCusReturnDtlUI.mp_FlagLot = flagLot;
                                    objInvFInventoryCusReturnDtlUI.mp_FlagSerial = flagSerial;
                                    objInvFInventoryCusReturnDtlUI.ificrc_UPCusReturnDesc = ificrc_UPCusReturnDesc;
                                    objInvFInventoryCusReturnDtlUI.InvCodeInActual = invCodeInActual;
                                    objInvFInventoryCusReturnDtlUI.Remark = remark;

                                    if (item.Lst_InvF_InventoryCusreturnDtlUI == null || item.Lst_InvF_InventoryCusreturnDtlUI.Count == 0)
                                    {
                                        item.Lst_InvF_InventoryCusreturnDtlUI = new List<InvF_InventoryCusReturnDtlUI>();
                                    }
                                    item.Lst_InvF_InventoryCusreturnDtlUI.Add(objInvFInventoryCusReturnDtlUI);
                                }
                            }
                        }
                    }
                    #endregion


                    #endregion
                    return Json(new { Success = true, ListInvFInventoryCusReturnDtlUI = listInvFInventoryCusReturnDtlUI });
                }
                else
                {
                    exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }

            return Json(resultEntry);
        }
        
        #endregion

        #region["Export Excel"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string objlistselected)
        {
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            string url = "";

            var ListInvF_InventoryCusReturn = new List<InvF_InventoryCusReturn>();
            var ListInvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnDtl>();
            var ListInvF_InventoryCusReturnInstLot = new List<InvF_InventoryCusReturnInstLot>();
            var ListInvF_InventoryCusReturnInstSerial = new List<InvF_InventoryCusReturnInstSerial>();
            var ListInvF_InventoryCusReturnCover = new List<InvF_InventoryCusReturnCover>();

            try
            {
                #region["Search"]
                List<InvF_InventoryCusReturn> listInvF_InventoryCusReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryCusReturn>>(objlistselected);
                var listIF_InvCusReturnNo = listInvF_InventoryCusReturn.Select(m => m.IF_InvCusReturnNo.ToString()).ToList();

                var strWhereInvF_InventoryCusReturn = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryCusReturn, TblInvF_InventoryCusReturn.IF_InvCusReturnNo, CUtils.StretchListString(listIF_InvCusReturnNo));

                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_WhereClause = strWhereInvF_InventoryCusReturn,
                    Ft_Cols_Upd = null,
                    // RQ_InvF_InventoryCusReturn
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "*",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*"
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null)
                {
                    if (objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count > 0)
                    {
                        ListInvF_InventoryCusReturn.AddRange(objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn);
                        //Xử lý thêm RO- với RefType = RO
                        foreach (var item in ListInvF_InventoryCusReturn)
                        {
                            if (CUtils.StrValue(item.RefType) == RefType.RO)
                            {
                                item.RefNo = "RO-" + CUtils.StrValue(item.RefNo);
                            }
                        }
                    }
                    if (objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl.Count > 0)
                    {
                        ListInvF_InventoryCusReturnDtl.AddRange(objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnDtl);
                    }

                    if (objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot.Count > 0)
                    {
                        ListInvF_InventoryCusReturnInstLot.AddRange(objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstLot);
                    }
                    if (objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial.Count > 0)
                    {
                        ListInvF_InventoryCusReturnInstSerial.AddRange(objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnInstSerial);
                    }
                    if (objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover.Count > 0)
                    {
                        ListInvF_InventoryCusReturnCover.AddRange(objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover);
                    }


                    Dictionary<string, string> dicColNamesMst = GetExportDicColumsMaster();
                    Dictionary<string, string> dicColNamesDtl = GetExportDicColumsDtl();
                    Dictionary<string, string> dicColNamesLot = GetExportDicColumsLot();
                    Dictionary<string, string> dicColNamesSerial = GetExportDicColumsSerial();
                    Dictionary<string, string> dicColNamesCover = GetExportDicColumsCover();

                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryCusReturn).Name), ref url);
                    var ds = new DataSet();
                    var table = ExcelExport.ConstructDataTable(ListInvF_InventoryCusReturn, dicColNamesMst);
                    var tableDtl = ExcelExport.ConstructDataTable(ListInvF_InventoryCusReturnDtl, dicColNamesDtl);
                    var tableLot = ExcelExport.ConstructDataTable(ListInvF_InventoryCusReturnInstLot, dicColNamesLot);
                    var tableSerial = ExcelExport.ConstructDataTable(ListInvF_InventoryCusReturnInstSerial, dicColNamesSerial);
                    var tableCover = ExcelExport.ConstructDataTable(ListInvF_InventoryCusReturnCover, dicColNamesCover);

                    if (table != null && table.Rows.Count > 0)
                    {
                        table.TableName = "InvF_InventoryCusReturn";
                        ds.Tables.Add(table);
                    }
                    if (tableCover != null && tableCover.Rows.Count > 0)
                    {
                        tableCover.TableName = "InvF_InventoryCusReturnCover";
                        ds.Tables.Add(tableCover);
                    }
                    if (tableDtl != null && tableDtl.Rows.Count > 0)
                    {
                        tableDtl.TableName = "InvF_InventoryCusReturnDtl";
                        ds.Tables.Add(tableDtl);
                    }
                    if (tableLot != null && tableLot.Rows.Count > 0)
                    {
                        tableLot.TableName = "InvF_InventoryCusReturnInstLot";
                        ds.Tables.Add(tableLot);
                    }
                    if (tableSerial != null && tableSerial.Rows.Count > 0)
                    {
                        tableSerial.TableName = "InvF_InventoryCusReturnInstSerial";
                        ds.Tables.Add(tableSerial);
                    }                    

                    ExcelExport.ExportToExcelMultiSheet(ds, filePath);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateProduct()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryCusReturnDtl = new List<InvF_InventoryCusReturnCover>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateProduct();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryCusReturnCover).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInvF_InventoryCusReturnDtl, dicColNames, filePath, string.Format("InvF_InventoryCusReturnCover"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateLot()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<InvF_InventoryCusReturnInstLot>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateLot();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryCusReturnInstLot).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryCusReturnInstLot"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateSerial()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<InvF_InventoryCusReturnInstSerial>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateSerial();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryCusReturnInstSerial).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryCusReturnInstSerial"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateQR()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<InvF_InventoryCusReturnCover>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateQR();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryCusReturnCover).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryCusReturnCover"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        #region["DicColums"]

        private Dictionary<string, string> GetImportDicColumsTemplateProduct()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"Qty","Số lượng(*)"},
                 {"InvCodeInActual","Vị trí(*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateLot()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"ProductLotNo","Số lô(*)"},
                 {"Qty","Số lượng"},
                 {"ProductionDate","Ngày sản xuất"},
                 {"ExpiredDate","Ngày hết hạn"},
                 {"InvCodeInActual","Vị trí(*)"},
                 {"Remark","Ghi chú"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplateSerial()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"SerialNo","Số serial(*)"},
                 {"InvCodeInActual","Vị trí(*)"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplateQR()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"QRCode","Mã xác thực(*)"},
                 {"BoxNo","Mã hộp"},
                 {"CanNo","Mã thùng"},
                 {"ProductLotNo","Lô sản xuất"},
                 {"ShiftInCode","Ca sản xuất"},
                 {"UserKCS","KCS"},
            };
        }

        private Dictionary<string, string> GetExportDicColumsMaster()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvCusReturnNo","Số phiếu KH trả hàng"},
                {"ApprDTimeUTC","Thời gian nhập"},
                {"InvCodeIn","Kho nhập"},
                {"mct_CustomerName","Khách hàng"},
                {"TotalValCusReturnAfterDesc", "Tổng tiền"},
                {"IF_InvCusReturnStatus", "Trạng thái"},
                {"Remark", "Nội dung trả"},
                {"InvoiceNo", "Số hóa đơn"},
                {"CreateDTimeUTC", "Thời gian tạo"},
                {"RefNo", "Số RefNo"},
            };
        }

        private Dictionary<string, string> GetExportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvCusReturnNo","Số phiếu KH trả hàng"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"mp_ProductName","Tên hàng hóa (TV)"},
                {"UnitCode","Đơn vị"},
                {"Qty", "Số lượng"},
                {"InvCodeInActual", "Vị trí nhập"},
                {"Remark", "Ghi chú"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsLot()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvCusReturnNo","Số phiếu KH trả hàng"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"ProductLotNo","Số lô"},
                {"ProductionDate","Ngày sản xuất"},
                {"ExpiredDate", "Ngày hết hạn"},
                {"Qty", "Số lượng"},
                {"InvCodeInActual", "Vị trí"},
                {"Remark","Ghi chú"},
            };
        }

        private Dictionary<string, string> GetExportDicColumsSerial()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvCusReturnNo","Số phiếu KH trả hàng"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"SerialNo","Số serial"},
                {"InvCodeInActual", "Vị trí"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsCover()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvCusReturnNo","Số phiếu KH trả hàng"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"mp_ProductName","Tên hàng hóa (TV)"},
                {"UnitCode","Đơn vị"},
                {"Qty", "Số lượng"},
                {"InvCodeInActual", "Vị trí nhập"},
                {"UPCusReturn", "Giá nhập"},
                {"UPCusReturnDesc", "Giảm giá"},
                {"ValCusReturnAfterDesc", "Thành tiền"},
                {"Remark", "Ghi chú"}
            };
        }
        #endregion
        #endregion

        #region ["In"]
        [HttpPost]
        public ActionResult PrintTemp(string if_invcusreturnno)
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

                InvF_InventoryCusReturn objInvF_InventoryCusReturn = new InvF_InventoryCusReturn();
                var strWhere = strWhereClause_InvF_InventoryCusReturnByID(if_invcusreturnno);
                var objRQ_InvF_InventoryCusReturn = new RQ_InvF_InventoryCusReturn()
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryCusReturn = "*",
                    Rt_Cols_InvF_InventoryCusReturnCover = "*",
                    Rt_Cols_InvF_InventoryCusReturnDtl = "",
                    Rt_Cols_InvF_InventoryCusReturnInstLot = "",
                    Rt_Cols_InvF_InventoryCusReturnInstSerial = "",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryCusReturn = InvF_InventoryCusReturnService.Instance.WA_InvF_InventoryCusReturn_Get(objRQ_InvF_InventoryCusReturn, addressAPIs);
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn.Count != 0)
                {
                    objInvF_InventoryCusReturn = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturn[0];
                    if (CUtils.StrValue(objInvF_InventoryCusReturn.RefType) == RefType.RO)
                    {
                        objInvF_InventoryCusReturn.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryCusReturn.RefNo);
                    }
                }

                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvF_InventoryCusReturnCoverUI = new List<InvF_InventoryCusReturnCoverUI>();

                var Lst_InvF_InventoryCusReturnCover = new List<InvF_InventoryCusReturnCover>();
                if (objRT_InvF_InventoryCusReturn != null && objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover != null)
                {
                    Lst_InvF_InventoryCusReturnCover = objRT_InvF_InventoryCusReturn.Lst_InvF_InventoryCusReturnCover;
                }

                int idx = 1;
                foreach (var item in Lst_InvF_InventoryCusReturnCover)
                {
                    var strListInvCodeInActual = "";
                    if (item.ListInvCodeInActual != null && !string.IsNullOrEmpty(item.ListInvCodeInActual.ToString()))
                    {
                        strListInvCodeInActual = item.ListInvCodeInActual.ToString().Substring(0, item.ListInvCodeInActual.ToString().Length - 1);
                    }
                    var itemUI = new InvF_InventoryCusReturnCoverUI()
                    {
                        IF_InvCusReturnNo = item.IF_InvCusReturnNo,
                        InvCodeInActual = strListInvCodeInActual,
                        InvName = strListInvCodeInActual,
                        ProductCodeRoot = item.ProductCodeRoot,
                        NetworkID = item.NetworkID,
                        Qty = item.Qty,
                        UPCusReturn = item.UPCusReturn,
                        UPCusReturnDesc = item.UPCusReturnDesc,
                        ValCusReturnAfterDesc = item.ValCusReturnAfterDesc,
                        ValCusReturn = item.ValCusReturn,
                        ValCusReturnDesc = item.ValCusReturnDesc,
                        UnitCode = item.UnitCode,
                        IF_InvCusReturnStatusCover = item.IF_InvCusReturnStatusCover,
                        Remark = item.Remark,
                        ProductName = item.mp_ProductName,
                        ProductCodeUser = item.mp_ProductCodeUser,
                        Idx = idx
                    };

                    //
                    listInvF_InventoryCusReturnCoverUI.Add(itemUI);
                    idx++;
                }

                #endregion
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

                InvF_InventoryCusReturnPrint objPrint = new InvF_InventoryCusReturnPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.NNTFullName = strNNTFullName;
                objPrint.IF_InvCusReturnNo = objInvF_InventoryCusReturn.IF_InvCusReturnNo;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CustomerName = objInvF_InventoryCusReturn.mct_CustomerName;
                objPrint.OrderNo = objInvF_InventoryCusReturn.RefNo;
                objPrint.InvNameOut = objInvF_InventoryCusReturn.InvCodeIn;
                objPrint.Remark = objInvF_InventoryCusReturn.Remark;
                objPrint.TotalQty = listInvF_InventoryCusReturnCoverUI.Sum(m => Convert.ToDouble(m.Qty));
                objPrint.TotalValCusReturn = objInvF_InventoryCusReturn.TotalValCusReturn;
                objPrint.TotalValCusReturnDesc = objInvF_InventoryCusReturn.TotalValCusReturnDesc;
                objPrint.TotalValCusReturnAfterDesc = objInvF_InventoryCusReturn.TotalValCusReturnAfterDesc;
                objPrint.CreateUserName = objInvF_InventoryCusReturn.su_UserName_Create;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_InventoryCusReturnCoverUI = listInvF_InventoryCusReturnCoverUI;

                #region Lấy mẫu in

                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("KHTH");
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

                    data.data = CreateDataObjectReportServer(objPrint, ref watermark);
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


        public dynamic CreateDataObjectReportServer(InvF_InventoryCusReturnPrint objTempPrint, ref string watermark)
        {
            var defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_InventoryCusReturnCover;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat); 
            var MyDynamic = new InvF_InventoryCusReturnReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.IF_InvCusReturnNo = CUtils.StrValueNew(objTempPrint.IF_InvCusReturnNo);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.CustomerName = CUtils.StrValueNew(objTempPrint.CustomerName);
                MyDynamic.OrderNo = CUtils.StrValueNew(objTempPrint.OrderNo);
                MyDynamic.InvNameOut = CUtils.StrValueNew(objTempPrint.InvNameOut);//
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, NumericFormat(tableName, "TotalQty", defaultFormat));
                MyDynamic.TotalValCusReturn = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturn, NumericFormat(tableName, "TotalValCusReturn", defaultFormat));
                MyDynamic.TotalValCusReturnDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturnDesc, NumericFormat(tableName, "TotalValCusReturnDesc", defaultFormat));
                MyDynamic.TotalValCusReturnAfterDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturnAfterDesc, NumericFormat(tableName, "TotalValCusReturnAfterDesc", defaultFormat));
                //MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, strFormatQty);
                //MyDynamic.TotalValCusReturn = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturn, strFormatMoney);
                //MyDynamic.TotalValCusReturnDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturnDesc, strFormatMoney);
                //MyDynamic.TotalValCusReturnAfterDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValCusReturnAfterDesc, strFormatMoney);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_InventoryCusReturnCoverReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_InvF_InventoryCusReturnCoverUI != null && objTempPrint.Lst_InvF_InventoryCusReturnCoverUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_InventoryCusReturnCoverReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_InventoryCusReturnCoverUI)
                {
                    var objDtl_ReportServer = new InvF_InventoryCusReturnCoverReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.ProductCodeUser),
                        ProductName = CUtils.StrValue(item.ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        InvName = CUtils.StrValue(item.InvName),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, strFormatQty),
                        UPCusReturn = CUtils.StrValueFormatNumber(item.UPCusReturn, NumericFormat(tableName, "UPCusReturn", defaultFormat)),
                        UPCusReturnDesc = CUtils.StrValueFormatNumber(item.UPCusReturnDesc, NumericFormat(tableName, "UPCusReturnDesc", defaultFormat)),
                        ValCusReturn = CUtils.StrValueFormatNumber(item.ValCusReturn, NumericFormat(tableName, "ValCusReturn", defaultFormat)),
                    };

                    listDtl_ReportServer.Add(objDtl_ReportServer);
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }


        #endregion
    }
}