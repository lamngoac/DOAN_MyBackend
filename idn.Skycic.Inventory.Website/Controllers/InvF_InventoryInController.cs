using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
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
using idn.Skycic.Inventory.ClientService;
using System.Net;
using System.IO;
using idn.Skycic.Inventory.ClientService.Services.LQDMS;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class InvF_InventoryInController : AdminBaseController
    {
        // GET: InvF_InventoryIn
        #region ["Search Index"]
        public async Task<ActionResult> Index(int page = 0, int recordcount = 10)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN");
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
                var pageInfo = new PageInfo<InvF_InventoryIn>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryIn>()
                };

                #region ["Mst_Inventory + Mst_InvInType"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
                var strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                var Mst_InvInType_Get_Task = Mst_InvInType_Get_Async(strWhere_Mst_InvInType);
                #endregion

                await Task.WhenAll(Mst_Inventory_Get_Task, Mst_InvInType_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();
                var list_Mst_InvInType = new List<Mst_InvInType>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }
                if (Mst_InvInType_Get_Task != null && Mst_InvInType_Get_Task.Result != null && Mst_InvInType_Get_Task.Result.Count > 0)
                {
                    list_Mst_InvInType.AddRange(Mst_InvInType_Get_Task.Result);
                }

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
                ViewBag.Lst_Mst_InvInType = list_Mst_InvInType;

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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.MSTBUPattern like '" + CUtils.StrValue(UserState.Mst_NNT.MSTBUPattern) + "' and Mst_NNT.FlagActive = '1'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                
                ViewBag.ListMst_NNT = listMst_NNT;
                #endregion
                #region["Thông tin hàng hóa"]
                var lstMst_Product = new List<Mst_Product>();
                var objRQ_Mst_Product = new RQ_Mst_Product() {
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
                    Ft_WhereClause = "Mst_Product.FlagActive = '1'"
                };
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if(objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null)
                {
                    lstMst_Product = objRT_Mst_Product.Lst_Mst_Product;
                }
                ViewBag.Lst_Mst_Product = lstMst_Product;
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
        public ActionResult DoSearch(string if_invinno = "", string createdtimefrom = "", string createdtimeto = "", string apprdtimefrom = "",
            string apprdtimeto = "", string invintype = "", string customercode = "", string invcodein = "", string productcode = "", string orgid = "", string refno = "", string chkpending = "", string chkapproved = "", string chkcanceled = "", int recordcount = 50, int page = 0
            )
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserState = UserState;
                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion

                var itemCount = 0;
                var pageCount = 0;
                ViewBag.PageCur = page;
                ViewBag.Recordcount = recordcount;
                var pageInfo = new PageInfo<InvF_InventoryIn>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryIn>()
                };

                var strWhereClause = strWhereClause_InvF_InventoryIn(if_invinno, createdtimefrom, createdtimeto, apprdtimefrom,
                apprdtimeto, invintype, customercode, invcodein, productcode, orgid, refno, chkpending, chkapproved, chkcanceled);
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
                    FuncType = null,
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "",
                    Rt_Cols_InvF_InventoryInInstLot = "",
                    Rt_Cols_InvF_InventoryInInstSerial = ""
                };
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                {
                    pageInfo.DataList = objRT_InvF_InventoryIn.Lst_InvF_InventoryIn;
                    itemCount = objRT_InvF_InventoryIn.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryIn.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("BindDataInvF_InventoryIn", pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #endregion

        #region Master + Show popup

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
        public async Task<ActionResult> ShowInvCodeInActual(string productcode, string productcodeuser, string productcodebase, string productname, string invbupattern, string listdetail, string ifinvinstatus, string viewtype)
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

                List<InvF_InventoryInDtl> lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryInDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInDtl>>(listdetail);
                }
                ViewBag.ListInvF_InventoryInDtl = lst_InvF_InventoryInDtl;
                
                ViewBag.IF_InvInStatus = ifinvinstatus;
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
        public async Task<ActionResult> ShowLo(string productcode, string productcodeuser, string productname, string invBUPattern, string listlot, string ifinvinstatus, string viewtype)
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
                ViewBag.ProductName = productcodeuser + " - " + productname;
                ViewBag.ListMst_Inventory = list_Mst_Inventory;

                List<InvF_InventoryInInstLot> lst_InvF_InventoryInInstLot = new List<InvF_InventoryInInstLot>();
                if (listlot != null && listlot.Length > 0)
                {
                    lst_InvF_InventoryInInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInInstLot>>(listlot);
                }
                ViewBag.ListInvF_InventoryInInstLot = lst_InvF_InventoryInInstLot;
                ViewBag.IF_InvInStatus = ifinvinstatus;
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
        public async Task<ActionResult> ShowSerial(string productcode, string productcodeuser, string productname, string invbupattern, string listserial, string ifinvinstatus, string viewtype)
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

                List<InvF_InventoryInInstSerial> lst_InvF_InventoryInInstSerial = new List<InvF_InventoryInInstSerial>();
                if (listserial != null && listserial.Length > 0)
                {
                    lst_InvF_InventoryInInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInInstSerial>>(listserial);
                }
                ViewBag.ListInvF_InventoryInInstSerial = lst_InvF_InventoryInInstSerial;
                ViewBag.IF_InvInStatus = ifinvinstatus;
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
        public async Task<ActionResult> ShowInvCodeInActual_Old(string productcode, string productcodeuser, string productcodebase, string productname, string invbupattern, string listdetail, string flaghidebtn)
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

                List<InvF_InventoryInDtl> lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                if (listdetail != null && listdetail.Length > 0)
                {
                    lst_InvF_InventoryInDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInDtl>>(listdetail);
                }
                ViewBag.ListInvF_InventoryInDtl = lst_InvF_InventoryInDtl;

                ViewBag.FlagHideBtn = flaghidebtn;

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
        public async Task<ActionResult> SearchCustomer(string txtsearch)
        {

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Mst_Customer"]

                var strWhere_Mst_Customer = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'";

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
        public async Task<ActionResult> ShowLo_Old(string productCode, string invBUPattern, string listLot, string flaghidebtn)
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

                List<InvF_InventoryInInstLot> lst_InvF_InventoryInInstLot = new List<InvF_InventoryInInstLot>();
                if (listLot != null && listLot.Length > 0)
                {
                    lst_InvF_InventoryInInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInInstLot>>(listLot);
                }
                ViewBag.ListLot = lst_InvF_InventoryInInstLot;
                ViewBag.FlagHideBtn = flaghidebtn;

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
        public async Task<ActionResult> ShowSerial_Old(string productCode, string invBUPattern, string listSerial, string flaghidebtn)
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

                List<InvF_InventoryInInstSerial> lst_InvF_InventoryInInstSerial = new List<InvF_InventoryInInstSerial>();
                if (listSerial != null && listSerial.Length > 0)
                {
                    lst_InvF_InventoryInInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInInstSerial>>(listSerial);
                }
                ViewBag.ListSerial = lst_InvF_InventoryInInstSerial;
                ViewBag.FlagHideBtn = flaghidebtn;

                return JsonView("Serial", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        #endregion

        #region["Tạo phiếu nhập kho"]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.UserState = UserState;
            //ViewBag.Lst_Mst_Sys_Config = UserState.RT_Mst_Sys_Config.Lst_Mst_Sys_Config;
            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var networkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);

            var iFInvInNo = SeqNo(new Seq_Common()
            {
                SequenceType = SeqType.IFInvInNo,
                Param_Postfix = "",
                Param_Prefix = ""
            });

            #region ["Common"]

            #region["Loại kho"]
            var strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";
            var Mst_InvInType_Get_Task = Mst_InvInType_Get_Async(strWhere_Mst_InvInType);
            #endregion
            #region["Kho nhập"]
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
            var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
            #endregion
            #region["Nhà cung cấp"]
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

            var objRT_Mst_Customer_Async = RT_Mst_Customer_Async(objRQ_Mst_Customer);
            #endregion

            await Task.WhenAll(Mst_Inventory_Get_Task, Mst_InvInType_Get_Task, objRT_Mst_Customer_Async);
            var listMst_Inventory = new List<Mst_Inventory>();
            var listMst_InvInType = new List<Mst_InvInType>();
            var listMst_Customer = new List<Mst_Customer>();
            if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
            {
                listMst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
            }
            if (Mst_InvInType_Get_Task != null && Mst_InvInType_Get_Task.Result != null && Mst_InvInType_Get_Task.Result.Count > 0)
            {
                listMst_InvInType.AddRange(Mst_InvInType_Get_Task.Result);
            }
            if (objRT_Mst_Customer_Async != null)
            {
                var objRT_Mst_Customer = objRT_Mst_Customer_Async.Result;
                if(objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                {
                    listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
                }
                
            }
            ViewBag.ListMst_Inventory = listMst_Inventory;
            ViewBag.ListMst_InvInType = listMst_InvInType;
            ViewBag.ListMst_Customer = listMst_Customer;
            #endregion

            return View(new RT_InvF_InventoryIn() {
                Lst_InvF_InventoryIn = new List<InvF_InventoryIn>()
                {
                    new InvF_InventoryIn()
                    {
                        NetworkID = networkID,
                        OrgID = orgID,
                        IF_InvInNo = iFInvInNo,
                        IF_InvInStatus = "PENDING",
                    }
                },
                Lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>(),
                Lst_InvF_InventoryInInstLot = new List<InvF_InventoryInInstLot>(),
                Lst_InvF_InventoryInInstSerial = new List<InvF_InventoryInInstSerial>(),
                Lst_InvF_InventoryInQR = new List<InvF_InventoryInQR>(),
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_CREATE");
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
                InvF_InventoryInSave objInvF_InventoryInSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInSave>(model);
                objInvF_InventoryInSave.Obj_InvF_InventoryIn.OrgID = orgId;
                objInvF_InventoryInSave.Obj_InvF_InventoryIn.NetworkID = networkID;

                RQ_InvF_InventoryIn objRQ_InvF_InventoryIn = new RQ_InvF_InventoryIn()
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
                    InvF_InventoryIn = objInvF_InventoryInSave.Obj_InvF_InventoryIn,
                    Lst_InvF_InventoryInDtl = objInvF_InventoryInSave.Lst_InvF_InventoryInDtl,
                    Lst_InvF_InventoryInInstLot = objInvF_InventoryInSave.Lst_InvF_InventoryInInstLot,
                    Lst_InvF_InventoryInInstSerial = objInvF_InventoryInSave.Lst_InvF_InventoryInInstSerial,
                    Lst_InvF_InventoryInQR = objInvF_InventoryInSave.Lst_InvF_InventoryInQR,
                    FlagIsDelete = "0",
                    FlagIsCheckTotal = "1"
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryIn);
                InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Save(objRQ_InvF_InventoryIn, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo phiếu nhập kho thành công!");
                var flagRedirect = CUtils.StrValue(objInvF_InventoryInSave.FlagRedirect);
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
        #endregion

        public async Task<ActionResult> Update(string if_invinno)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region ["Mst_Inventory + Mst_InvInType"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
                var strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                var Mst_InvInType_Get_Task = Mst_InvInType_Get_Async(strWhere_Mst_InvInType);

                await Task.WhenAll(Mst_Inventory_Get_Task, Mst_InvInType_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();
                var list_Mst_InvInType = new List<Mst_InvInType>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }
                if (Mst_InvInType_Get_Task != null && Mst_InvInType_Get_Task.Result != null && Mst_InvInType_Get_Task.Result.Count > 0)
                {
                    list_Mst_InvInType.AddRange(Mst_InvInType_Get_Task.Result);
                }

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
                ViewBag.Lst_Mst_InvInType = list_Mst_InvInType;
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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                InvF_InventoryIn objInvF_InventoryIn = new InvF_InventoryIn();
                var strWhere = strWhereClause_InvF_InventoryInByID(if_invinno);
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "*",
                    Rt_Cols_InvF_InventoryInInstLot = "*",
                    Rt_Cols_InvF_InventoryInInstSerial = "*",
                    Rt_Cols_InvF_InventoryInQR = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count != 0)
                {
                    objInvF_InventoryIn = objRT_InvF_InventoryIn.Lst_InvF_InventoryIn[0];
                }

                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();

                var Lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl != null)
                {
                    Lst_InvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                }
                
                // lấy danh sách hàng hóa base của list1

                var lstPrdCodeBase = Lst_InvF_InventoryInDtl.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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

                var lstPrdDistinct = new List<string>();
                foreach (var item in Lst_InvF_InventoryInDtl)
                {
                    if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
                        continue;
                    // lọc danh sách hàng hóa (list1) theo productcode để ko trùng
                    var listInvInDtlCur = Lst_InvF_InventoryInDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    var qty = 0.0;
                    var valInAfterDesc = 0.0;
                    var invCodeInActual = "";
                    var listViTri = new List<string>();
                    foreach (var it in listInvInDtlCur)
                    {
                        qty += Convert.ToDouble(it.Qty);
                        valInAfterDesc += Convert.ToDouble(it.ValInAfterDesc);
                        if (!listViTri.Contains(it.InvCodeInActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeInActual))
                            {
                                invCodeInActual += ", " + it.InvCodeInActual.ToString();
                            }
                            else
                            {
                                invCodeInActual = it.InvCodeInActual.ToString();
                            }
                            listViTri.Add(it.InvCodeInActual.ToString());
                        }
                    }

                    var itemUI = new InvF_InventoryInDtlUI()
                    {
                        IF_InvInNo = item.IF_InvInNo,
                        InvCodeInActual = invCodeInActual,
                        ProductCode = item.ProductCode,
                        NetworkID = item.NetworkID,
                        Qty = qty,
                        UPIn = item.UPIn,
                        UPInDesc = item.UPInDesc,
                        ValInvIn = item.ValInvIn,
                        ValInDesc = item.ValInDesc,
                        ValInAfterDesc = valInAfterDesc,
                        UnitCode = item.UnitCode,
                        IF_InvInStatusDtl = item.IF_InvInStatusDtl,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_ProductName,
                        mp_ProductCodeUser = item.mp_ProductCodeUser,
                        mp_ProductCodeBase = item.mp_ProductCodeBase,
                        mp_FlagLot = item.mp_FlagLot,
                        mp_FlagSerial = item.mp_FlagSerial
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
                    listInvFInventoryInDtlUI.Add(itemUI);
                    
                    lstPrdDistinct.Add(item.ProductCode.ToString());
                }
                #endregion
                ViewBag.lstInvF_InventoryInDtlUI = listInvFInventoryInDtlUI;
                ViewBag.lstInvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                ViewBag.lstInvF_InventoryInInstLot = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstLot;
                ViewBag.lstInvF_InventoryInInstSerial = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial;
                ViewBag.lstInvF_InventoryInQR = objRT_InvF_InventoryIn.Lst_InvF_InventoryInQR;
                //
                if (!CUtils.IsNullOrEmpty(objInvF_InventoryIn.RefNo))
                {
                    var listProductOrder = SearchProductFromOrder(CUtils.StrValue(objInvF_InventoryIn.RefNo));
                    var listProductUIDtl = new List<InvF_InventoryInDtlUI>();
                    if (listProductOrder != null && listProductOrder.Count > 0)
                    {
                        foreach (var item in listProductOrder)
                        {
                            if (!CUtils.IsNullOrEmpty(item.ProductCode))
                            {
                                var listProductUI = listInvFInventoryInDtlUI.Where(it => it.ProductCode.Equals(item.ProductCode)).ToList();
                                foreach (var i in listProductUI)
                                {
                                    i.PlanQty = item.PlanQty;
                                }
                                listProductUIDtl.AddRange(listProductUI);
                            }
                        }
                    }
                    ViewBag.ListProductUI = listProductUIDtl;
                }
                
                return View(objRT_InvF_InventoryIn);
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
        public ActionResult UpdateData(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_UPDATE");
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
                InvF_InventoryInSave objInvF_InventoryInSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInSave>(model);
                objInvF_InventoryInSave.Obj_InvF_InventoryIn.OrgID = orgId;
                objInvF_InventoryInSave.Obj_InvF_InventoryIn.NetworkID = networkID;

                RQ_InvF_InventoryIn objRQ_InvF_InventoryIn = new RQ_InvF_InventoryIn()
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
                    InvF_InventoryIn = objInvF_InventoryInSave.Obj_InvF_InventoryIn,
                    Lst_InvF_InventoryInDtl = objInvF_InventoryInSave.Lst_InvF_InventoryInDtl,
                    Lst_InvF_InventoryInInstLot = objInvF_InventoryInSave.Lst_InvF_InventoryInInstLot,
                    Lst_InvF_InventoryInInstSerial = objInvF_InventoryInSave.Lst_InvF_InventoryInInstSerial,
                    Lst_InvF_InventoryInQR = objInvF_InventoryInSave.Lst_InvF_InventoryInQR,
                    FlagIsDelete = "0",
                    FlagIsCheckTotal = "1"
                };
                InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Save(objRQ_InvF_InventoryIn, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Sửa phiếu nhập kho thành công!");
                var flagRedirect = CUtils.StrValue(objInvF_InventoryInSave.FlagRedirect);
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
            return JsonViewError("Update", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdAfterAppr(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_UPDATE");
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
                InvF_InventoryInSave objInvF_InventoryInSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInSave>(model);

                RQ_InvF_InventoryIn objRQ_InvF_InventoryIn = new RQ_InvF_InventoryIn()
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
                    InvF_InventoryIn = objInvF_InventoryInSave.Obj_InvF_InventoryIn,
                    FlagIsDelete = "0",
                    FlagIsCheckTotal = "1"
                };
                InvF_InventoryInService.Instance.WA_InvF_InventoryIn_UpdAfterAppr(objRQ_InvF_InventoryIn, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Sửa phiếu nhập kho thành công!");
                var flagRedirect = CUtils.StrValue(objInvF_InventoryInSave.FlagRedirect);
                if (flagRedirect.Equals("1"))
                {
                    resultEntry.RedirectUrl = Url.Action("Detail");
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
            return JsonViewError("Detail", null, resultEntry);
        }

        public async Task<ActionResult> Detail(string if_invinno)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYIN_DETAIL");
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
                #region ["Mst_Inventory + Mst_InvInType"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
                var strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";

                var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
                var Mst_InvInType_Get_Task = Mst_InvInType_Get_Async(strWhere_Mst_InvInType);

                await Task.WhenAll(Mst_Inventory_Get_Task, Mst_InvInType_Get_Task);
                var list_Mst_Inventory = new List<Mst_Inventory>();
                var list_Mst_InvInType = new List<Mst_InvInType>();

                if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
                {
                    list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
                }
                if (Mst_InvInType_Get_Task != null && Mst_InvInType_Get_Task.Result != null && Mst_InvInType_Get_Task.Result.Count > 0)
                {
                    list_Mst_InvInType.AddRange(Mst_InvInType_Get_Task.Result);
                }

                ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
                ViewBag.Lst_Mst_InvInType = list_Mst_InvInType;
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
                    Ft_WhereClause = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'"
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
                #endregion

                InvF_InventoryIn objInvF_InventoryIn = new InvF_InventoryIn();
                var strWhere = strWhereClause_InvF_InventoryInByID(if_invinno);
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "*",
                    Rt_Cols_InvF_InventoryInInstLot = "*",
                    Rt_Cols_InvF_InventoryInInstSerial = "*",
                    Rt_Cols_InvF_InventoryInQR = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count != 0)
                {
                    objInvF_InventoryIn = objRT_InvF_InventoryIn.Lst_InvF_InventoryIn[0];
                }

                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();

                var Lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl != null)
                {
                    Lst_InvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                }

                var lstPrdCodeBase = Lst_InvF_InventoryInDtl.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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

                var lstPrdDistinct = new List<string>();
                foreach (var item in Lst_InvF_InventoryInDtl)
                {
                    if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
                        continue;

                    var listInvInDtlCur = Lst_InvF_InventoryInDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    var qty = 0.0;
                    var valInAfterDesc = 0.0;
                    var invCodeInActual = "";
                    var listViTri = new List<string>();
                    foreach (var it in listInvInDtlCur)
                    {
                        qty += Convert.ToDouble(it.Qty);
                        valInAfterDesc += Convert.ToDouble(it.ValInAfterDesc);
                        if (!listViTri.Contains(it.InvCodeInActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeInActual))
                            {
                                invCodeInActual += ", " + it.InvCodeInActual.ToString();
                            }
                            else
                            {
                                invCodeInActual = it.InvCodeInActual.ToString();
                            }
                            listViTri.Add(it.InvCodeInActual.ToString());
                        }
                    }

                    var itemUI = new InvF_InventoryInDtlUI()
                    {
                        IF_InvInNo = item.IF_InvInNo,
                        InvCodeInActual = invCodeInActual,
                        ProductCode = item.ProductCode,
                        NetworkID = item.NetworkID,
                        Qty = qty,
                        UPIn = item.UPIn,
                        UPInDesc = item.UPInDesc,
                        ValInvIn = item.ValInvIn,
                        ValInDesc = item.ValInDesc,
                        ValInAfterDesc = valInAfterDesc,
                        UnitCode = item.UnitCode,
                        IF_InvInStatusDtl = item.IF_InvInStatusDtl,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_ProductName,
                        mp_ProductCodeUser = item.mp_ProductCodeUser,
                        mp_ProductCodeBase = item.mp_ProductCodeBase,
                        mp_FlagLot = item.mp_FlagLot,
                        mp_FlagSerial = item.mp_FlagSerial
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
                    listInvFInventoryInDtlUI.Add(itemUI);

                    lstPrdDistinct.Add(item.ProductCode.ToString());
                }

                #endregion
                ViewBag.lstInvF_InventoryInDtlUI = listInvFInventoryInDtlUI;
                ViewBag.lstInvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                ViewBag.lstInvF_InventoryInInstLot = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstLot;
                ViewBag.lstInvF_InventoryInInstSerial = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial;
                ViewBag.lstInvF_InventoryInQR = objRT_InvF_InventoryIn.Lst_InvF_InventoryInQR;
                //
                return View(objRT_InvF_InventoryIn);
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
        public ActionResult Approve(string objlistinvf_inventoryin)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYIN_APPROVE");
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
                List<InvF_InventoryIn> listInvF_InventoryIn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryIn>>(objlistinvf_inventoryin);

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
                    InvF_InventoryIn = null
                };

                foreach (var item in listInvF_InventoryIn)
                {
                    objRQ_InvF_InventoryIn.Tid = GetNextTId();
                    objRQ_InvF_InventoryIn.InvF_InventoryIn = new InvF_InventoryIn()
                    {
                        IF_InvInNo = item.IF_InvInNo,
                        Remark = item.Remark
                    };
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryIn);
                    InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Appr(objRQ_InvF_InventoryIn, addressAPIs);
                }

                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt phiếu nhập kho thành công!");
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
        public ActionResult Cancel(string objlistinvf_inventoryin)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYIN_CANCEL");
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
                List<InvF_InventoryIn> listInvF_InventoryIn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryIn>>(objlistinvf_inventoryin);

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
                    InvF_InventoryIn = null
                };

                foreach (var item in listInvF_InventoryIn)
                {
                    objRQ_InvF_InventoryIn.Tid = GetNextTId();
                    objRQ_InvF_InventoryIn.InvF_InventoryIn = new InvF_InventoryIn()
                    {
                        IF_InvInNo = item.IF_InvInNo,
                        Remark = item.Remark
                    };

                    InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Cancel(objRQ_InvF_InventoryIn, addressAPIs);
                }

                resultEntry.Success = true;
                resultEntry.AddMessage("Hủy phiếu nhập kho thành công!");
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
        public ActionResult Delete(string objlistinvf_inventoryin)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYIN_DELETE");
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
                List<InvF_InventoryIn> listInvF_InventoryIn = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryIn>>(objlistinvf_inventoryin);
                var listIF_InvInNo = listInvF_InventoryIn.Select(m => m.IF_InvInNo.ToString()).ToList();

                var strWhereInvF_InventoryIn = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryIn, TblInvF_InventoryIn.IF_InvInNo, CUtils.StretchListString(listIF_InvInNo));

                var objRQ_InvF_InventoryIn = new RQ_InvF_InventoryIn()
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
                    Ft_WhereClause = strWhereInvF_InventoryIn,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "*",
                    Rt_Cols_InvF_InventoryInInstLot = "*",
                    Rt_Cols_InvF_InventoryInInstSerial = "*",
                    Rt_Cols_InvF_InventoryInQR = "*"
                };
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                {
                    foreach (var objInvF_InventoryIn in objRT_InvF_InventoryIn.Lst_InvF_InventoryIn)
                    {
                        RQ_InvF_InventoryIn objRQ_InvF_InventoryInDelete = new RQ_InvF_InventoryIn()
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
                            InvF_InventoryIn = objInvF_InventoryIn,
                            Lst_InvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl.Where(m => m.IF_InvInNo.ToString() == objInvF_InventoryIn.IF_InvInNo.ToString()).ToList(),
                            Lst_InvF_InventoryInInstLot = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstLot.Where(m => m.IF_InvInNo.ToString() == objInvF_InventoryIn.IF_InvInNo.ToString()).ToList(),
                            Lst_InvF_InventoryInInstSerial = objRT_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial.Where(m => m.IF_InvInNo.ToString() == objInvF_InventoryIn.IF_InvInNo.ToString()).ToList(),
                            Lst_InvF_InventoryInQR = objRT_InvF_InventoryIn.Lst_InvF_InventoryInQR.Where(m => m.IF_InvInNo.ToString() == objInvF_InventoryIn.IF_InvInNo.ToString()).ToList(),
                            FlagIsDelete = "1",
                            FlagIsCheckTotal = "1"
                        };
                        InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Save(objRQ_InvF_InventoryInDelete, addressAPIs);
                    }

                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá phiếu nhập kho thành công!");
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

            List<Mst_Product> listProduct = new List<Mst_Product>();
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
                foreach(var itPrd in listProduct)
                {
                    var invCodeSuggest = "";
                    var lstBalanceCur = listBalance.Where(m => m.ProductCode == Convert.ToString(itPrd.ProductCode)).ToList();
                    if(lstBalanceCur.Count > 0)
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

            if (lstRT_Mst_Product == null || lstRT_Mst_Product.Count == 0)
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
            var pageInfo = new PageInfo<InvF_InventoryInDtlUI>(0, recordcount)
            {
                DataList = new List<InvF_InventoryInDtlUI>()
            };

            var strWhere_Mst_Product = strWhereClause_Mst_Product(productcode, "1", "PRODUCT", "1");
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
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
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

            var listDtlUI = new List<InvF_InventoryInDtlUI>();

            #region Thông tin tồn kho - Tìm VT tồn lớn nhất
            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

            List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
            foreach (var it in List_Mst_Product)
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

                    var itemUI = new InvF_InventoryInDtlUI()
                    {
                        ProductCode = itPrd.ProductCode,
                        mp_ProductCodeBase = itPrd.ProductCodeBase,
                        mp_ProductCodeUser = itPrd.ProductCodeUser,
                        ProductCodeUser = itPrd.ProductCodeUser,
                        mp_ProductName = itPrd.ProductName,
                        UnitCode = itPrd.UnitCode,
                        UPIn = itPrd.UPBuy,
                        mp_FlagLot = itPrd.FlagLot,
                        mp_FlagSerial = itPrd.FlagSerial,
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

        //public ActionResult SearchProduct(string refno)
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    #region ["UserState Common Info"]
        //    var waUserCode = CUtils.StrValueOrNull(WAUserCode_LQDMS_BG);
        //    var waUserPassword = CUtils.StrValueOrNull(WAUserPassword_LQDMS_BG);
        //    var networkID = CUtils.StrValueOrNull(LQDMS_NetworkID);
        //    var orgID = CUtils.StrValue(LQDMS_OrgID);
        //    var addressAPIs = CUtils.StrValue(BaseMasterServerLQDMSAPIAddress);
        //    var addressAPIsQRBox = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    var listMst_PrintOrderUI = new List<Mst_PrintOrderUI>();
        //    var listMst_PrintOrder = new List<Mst_PrintOrder>();
        //    var objMst_PrintOrder = new Mst_PrintOrder();
        //    var productCodeUser = "ETEMLT01";
        //    var productName = "Tem thông minh";
        //    #endregion
        //    try
        //    {
        //        #region["Lấy thông tin phiếu in"]
        //        var strWhereClauseSearchMst_PrintOrder = strWhereClauseSearch_Mst_PrintOrder(new Mst_PrintOrder()
        //        {
        //            PrintOrdNo = CUtils.StrValue(refno)
        //        });
        //        var objRQ_Mst_PrintOrder = new RQ_Mst_PrintOrder()
        //        {
        //            // WARQBase
        //            Tid = GetNextTId(),
        //            GwUserCode = CUtils.StrValue(GwUserCodeLQDMS),
        //            GwPassword = CUtils.StrValue(GwPasswordLQDMS),
        //            AccessToken = CUtils.StrValue(UserState.AccessToken),
        //            WAUserCode = waUserCode,
        //            WAUserPassword = waUserPassword,
        //            NetworkID = CUtils.StrValue(networkID),
        //            OrgID = CUtils.StrValue(orgID),
        //            UtcOffset = CUtils.StrValue(LQDMS_UtcOffset),
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            Ft_WhereClause = strWhereClauseSearchMst_PrintOrder,
        //            //RQ_Mst_PrintOrder
        //            Rt_Cols_Mst_PrintOrder = "*",
        //            Mst_PrintOrder = null
        //        };
        //        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_PrintOrder);
        //        var objRT_Mst_PrintOrder = Mst_PrintOrderService.Instance.WA_Mst_PrintOrder_Get(objRQ_Mst_PrintOrder, addressAPIs);
        //        if (objRT_Mst_PrintOrder != null && objRT_Mst_PrintOrder.Lst_Mst_PrintOrder != null && objRT_Mst_PrintOrder.Lst_Mst_PrintOrder.Count > 0)
        //        {
        //            listMst_PrintOrder.AddRange(objRT_Mst_PrintOrder.Lst_Mst_PrintOrder);
        //            objMst_PrintOrder = objRT_Mst_PrintOrder.Lst_Mst_PrintOrder[0];
        //        }
        //        #endregion
        //        #region["Lấy hàng hóa trên QRBox"]
        //        var objMst_Product = new Mst_Product();
        //        var strWhereClauseSearch_MstProduct = strWhereGetProductId(new Mst_Product
        //        {
        //            ProductCodeUser = productCodeUser
        //        });
        //        var objRQ_Mst_Product = new RQ_Mst_Product()
        //        {
        //            // WARQBase
        //            Tid = GetNextTId(),
        //            GwUserCode = GwUserCode,
        //            GwPassword = GwPassword,
        //            AccessToken = CUtils.StrValue(UserState.AccessToken),
        //            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
        //            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
        //            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
        //            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
        //            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
        //            FuncType = null,
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            Ft_WhereClause = strWhereClauseSearch_MstProduct,
        //            Ft_Cols_Upd = null,
        //            // RQ_Mst_Product
        //            Rt_Cols_Mst_Product = "*",
        //            Rt_Cols_Mst_ProductImages = "",
        //            Rt_Cols_Mst_ProductFiles = "",
        //            Rt_Cols_Prd_BOM = "",
        //            Rt_Cols_Prd_Attribute = "",
        //            Lst_Mst_Product = null,
        //            Lst_Mst_ProductImages = null,
        //            Lst_Mst_ProductFiles = null,
        //            Lst_Prd_BOM = null,
        //            Lst_Prd_Attribute = null,
        //        };
        //        //var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
        //        var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIsQRBox);
        //        if(objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
        //        {
        //            objMst_Product = objRT_Mst_Product.Lst_Mst_Product[0];
        //        }
        //        #endregion
        //        #region["Gán hàng hóa ra listUI(mặc định là ETEMLT01)"]

        //        var objMst_PrintOrderUI = new Mst_PrintOrderUI()
        //        {
        //            ProductCode = objMst_Product.ProductCode,
        //            ProductCodeUser = productCodeUser,
        //            ProductName = productName,
        //            PrintOrdNo = objMst_PrintOrder.PrintOrdNo,
        //            PrintedQty = objMst_PrintOrder.PrintedQty,
        //            UPIn = objMst_Product.UPBuy,
        //            UnitCode = objMst_Product.UnitCode,
        //            CustomerCodeSys = objMst_PrintOrder.CustomerCodeSys,
        //            FlagLot = objMst_Product.FlagLot,
        //            FlagSerial = objMst_Product.FlagSerial
        //        };
        //        listMst_PrintOrderUI.Add(objMst_PrintOrderUI);
        //        #endregion
        //        ViewBag.ListMst_PrintOrderUI = listMst_PrintOrderUI;
        //        return JsonView("ShowOptionProductInOrder", listMst_PrintOrderUI);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }

        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return View(resultEntry);
        //}


        public ActionResult SearchProduct(string refno)

        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(WAUserCode_LQDMS_BG);
            var waUserPassword = CUtils.StrValueOrNull(WAUserPassword_LQDMS_BG);
            var networkID = CUtils.StrValueOrNull(LQDMS_NetworkID);
            var orgID = CUtils.StrValue(LQDMS_OrgID);
            var addressAPIs = CUtils.StrValue(BaseMasterServerLQDMSAPIAddress);
            var addressAPIsQRBox = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var listOS_OrderPDUI = new List<Ord_OrderPDUI>();
            var listOS_OrderPD = new List<OS_Ord_OrderPD>();
            var listOS_OrderPDDtl = new List<OS_Ord_OrderPDDtl>();
            var objOS_Ord_OrderPD = new OS_Ord_OrderPD();
            var objOS_Ord_OrderPDDtl = new OS_Ord_OrderPDDtl();
            var productCodeUser = "ETEMLT01";
            var productName = "Tem thông minh";
            #endregion

            try
            {
                #region["Lấy thông tin đơn hàng sản xuất"]
                var objOrd_OrderPD = new Ord_OrderPDUI()
                {
                    OrderPDNo = refno,

                };

                var strWhereClauseOrd_OrderPDSearch = strWhereClause_Ord_OrderPD_Search(objOrd_OrderPD);

                var objRQ_Ord_OrderPD = new RQ_OS_Ord_OrderPD()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = CUtils.StrValue(GwUserCodeLQDMS),
                    GwPassword = CUtils.StrValue(GwPasswordLQDMS),
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(LQDMS_UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseOrd_OrderPDSearch,
                    Ft_Cols_Upd = null,
                    FlagIsDelete = null,
                    // RQ_MD_MD
                    Rt_Cols_Ord_OrderPD = "*",
                    Rt_Cols_Ord_OrderPDDtl = "*",
                    Ord_OrderPD = null,

                };

                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Ord_OrderPD);
                var objRT_Ord_OrderPD = OS_Ord_OrderPDService.Instance.WA_Ord_OrderPD_Get(objRQ_Ord_OrderPD, addressAPIs);
                if (objRT_Ord_OrderPD != null && objRT_Ord_OrderPD.Lst_Ord_OrderPD.Count > 0 || objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl.Count > 0)
                {

                    if (objRT_Ord_OrderPD.Lst_Ord_OrderPD != null && objRT_Ord_OrderPD.Lst_Ord_OrderPD.Count > 0)
                    {
                        listOS_OrderPD.AddRange(objRT_Ord_OrderPD.Lst_Ord_OrderPD);
                        objOS_Ord_OrderPD = objRT_Ord_OrderPD.Lst_Ord_OrderPD[0];
                    }

                    if (objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl != null && objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl.Count > 0)
                    {
                        listOS_OrderPDDtl.AddRange(objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl);

                    }
                    if (listOS_OrderPDDtl != null && listOS_OrderPDDtl.Count > 0)
                    {
                        foreach (var item in listOS_OrderPDDtl)
                        {
                            var objMst_Product = new Mst_Product();
                            var strWhereClauseSearch_MstProduct = strWhereGetProductId(new Mst_Product
                            {
                                ProductCode = item.ProductCode
                            });

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
                                Ft_WhereClause = strWhereClauseSearch_MstProduct,
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
                            var dejson1 = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIsQRBox);
                            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                            {
                                objMst_Product = objRT_Mst_Product.Lst_Mst_Product[0];

                                var objOrd_OrderPDUI = new Ord_OrderPDUI()
                                {
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    PlanQty = CUtils.StrValue(item.PGInvInRemainQty),
                                    OrderPDNo = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNo),
                                    OrderPDNoSys = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNoSys),
                                    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    CustomerCodeSys = CUtils.StrValue(objOS_Ord_OrderPD.CustomerCodeSys),
                                    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                };

                                listOS_OrderPDUI.Add(objOrd_OrderPDUI);
                            }

                            //var objOrd_OrderPDUI = new Ord_OrderPDUI()
                            //{
                            //    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                            //    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                            //    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                            //    PlanQty = CUtils.StrValue(item.PlanQty),
                            //    OrderPDNo = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNo),
                            //    OrderPDNoSys = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNoSys),
                            //    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                            //    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                            //    CustomerCodeSys = CUtils.StrValue(objOS_Ord_OrderPD.CustomerCodeSys),
                            //    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                            //    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                            //};

                            //listOS_OrderPDUI.Add(objOrd_OrderPDUI);



                        }

                        ViewBag.ListMst_PrintOrderUI = listOS_OrderPDUI;
                        //return JsonView("ShowOptionProductInOrder", listOS_OrderPDUI);
                        return Json(new { Success = true, LstRpt_OrderSummary_TotalProductForInv = listOS_OrderPDUI });
                    }
                    #endregion
                }
                else
                {
                    resultEntry.AddMessage("Số RefNo " + refno + " không tồn tại trên hệ thống");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
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
        #region["Search Product"]
        public List<Ord_OrderPDUI> SearchProductFromOrder(string refno)

        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(WAUserCode_LQDMS_BG);
            var waUserPassword = CUtils.StrValueOrNull(WAUserPassword_LQDMS_BG);
            var networkID = CUtils.StrValueOrNull(LQDMS_NetworkID);
            var orgID = CUtils.StrValue(LQDMS_OrgID);
            var addressAPIs = CUtils.StrValue(BaseMasterServerLQDMSAPIAddress);
            var addressAPIsQRBox = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var listOS_OrderPDUI = new List<Ord_OrderPDUI>();
            var listOS_OrderPD = new List<OS_Ord_OrderPD>();
            var listOS_OrderPDDtl = new List<OS_Ord_OrderPDDtl>();
            var objOS_Ord_OrderPD = new OS_Ord_OrderPD();
            var objOS_Ord_OrderPDDtl = new OS_Ord_OrderPDDtl();
            var productCodeUser = "ETEMLT01";
            var productName = "Tem thông minh";
            #endregion
            
            #region["Lấy thông tin đơn hàng sản xuất"]
            var objOrd_OrderPD = new Ord_OrderPDUI()
            {
                OrderPDNo = refno,

            };

            var strWhereClauseOrd_OrderPDSearch = strWhereClause_Ord_OrderPD_Search(objOrd_OrderPD);

            var objRQ_Ord_OrderPD = new RQ_OS_Ord_OrderPD()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = CUtils.StrValue(GwUserCodeLQDMS),
                GwPassword = CUtils.StrValue(GwPasswordLQDMS),
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(LQDMS_UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                //Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseOrd_OrderPDSearch,
                Ft_Cols_Upd = null,
                FlagIsDelete = null,
                // RQ_MD_MD
                Rt_Cols_Ord_OrderPD = "*",
                Rt_Cols_Ord_OrderPDDtl = "*",
                Ord_OrderPD = null,

            };

            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Ord_OrderPD);
            var objRT_Ord_OrderPD = OS_Ord_OrderPDService.Instance.WA_Ord_OrderPD_Get(objRQ_Ord_OrderPD, addressAPIs);
            if (objRT_Ord_OrderPD != null)
            {

                if (objRT_Ord_OrderPD.Lst_Ord_OrderPD != null && objRT_Ord_OrderPD.Lst_Ord_OrderPD.Count > 0)
                {
                    listOS_OrderPD.AddRange(objRT_Ord_OrderPD.Lst_Ord_OrderPD);
                    objOS_Ord_OrderPD = objRT_Ord_OrderPD.Lst_Ord_OrderPD[0];
                }

                if (objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl != null && objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl.Count > 0)
                {
                    listOS_OrderPDDtl.AddRange(objRT_Ord_OrderPD.Lst_Ord_OrderPDDtl);

                }
            }



            #endregion


            if (listOS_OrderPDDtl != null && listOS_OrderPDDtl.Count > 0)
            {
                foreach (var item in listOS_OrderPDDtl)
                {
                    var objMst_Product = new Mst_Product();
                    var strWhereClauseSearch_MstProduct = strWhereGetProductId(new Mst_Product
                    {
                        ProductCode = item.ProductCode
                    });

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
                        Ft_WhereClause = strWhereClauseSearch_MstProduct,
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
                    var dejson1 = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                    var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIsQRBox);
                    if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                    {
                        objMst_Product = objRT_Mst_Product.Lst_Mst_Product[0];

                        var objOrd_OrderPDUI = new Ord_OrderPDUI()
                        {
                            ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                            ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                            ProductName = CUtils.StrValue(objMst_Product.ProductName),
                            PlanQty = CUtils.StrValue(item.PGInvInRemainQty),
                            OrderPDNo = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNo),
                            OrderPDNoSys = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNoSys),
                            UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                            UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                            CustomerCodeSys = CUtils.StrValue(objOS_Ord_OrderPD.CustomerCodeSys),
                            FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                            FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                        };

                        listOS_OrderPDUI.Add(objOrd_OrderPDUI);
                    }

                    //var objOrd_OrderPDUI = new Ord_OrderPDUI()
                    //{
                    //    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                    //    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                    //    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                    //    PlanQty = CUtils.StrValue(item.PlanQty),
                    //    OrderPDNo = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNo),
                    //    OrderPDNoSys = CUtils.StrValue(objOS_Ord_OrderPD.OrderPDNoSys),
                    //    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                    //    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                    //    CustomerCodeSys = CUtils.StrValue(objOS_Ord_OrderPD.CustomerCodeSys),
                    //    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                    //    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                    //};

                    //listOS_OrderPDUI.Add(objOrd_OrderPDUI);



                }

                ViewBag.ListMst_PrintOrderUI = listOS_OrderPDUI;
            }
            
            return listOS_OrderPDUI;
        }
        #endregion
        #endregion

        #region strWhereClause


        private string strWhereClause_Ord_OrderPD_Search(Ord_OrderPDUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Ord_OrderPD = TableName.Ord_OrderPD + ".";
            if (!CUtils.IsNullOrEmpty(data.OrderPDNo))
            {
                //sbSql.AddWhereClause(Tbl_Ord_OrderPD + TblOrd_OrderPD.OrderPDNo, "%" + CUtils.StrValue(data.OrderPDNo) + "%", "like");
                sbSql.AddWhereClause(Tbl_Ord_OrderPD + TblOrd_OrderPD.OrderPDNo, CUtils.StrValue(data.OrderPDNo), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        private string strWhereClause_InvF_InventoryIn(string if_invinno, string createdtimefrom, string createdtimeto, string apprdtimefrom,
           string apprdtimeto, string invintype, string customercode, string invcodein, string productcode, string orgid, string refno, string chkpending, string chkapproved, string chkcanceled)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryIn = TableName.InvF_InventoryIn + ".";
            var Tbl_InvF_InventoryInDtl = TableName.InvF_InventoryInDtl + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(if_invinno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.IF_InvInNo, "%" + CUtils.StrValue(if_invinno) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(createdtimefrom))
            {
                var createDTimeUTCFrom = CUtils.StrValue(createdtimefrom) + " 00:00:00";
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(createdtimeto))
            {
                var createDTimeUTCTo = CUtils.StrValue(createdtimeto) + " 23:59:59";
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdtimefrom))
            {
                var apprDTimeUTCFrom = CUtils.StrValue(apprdtimefrom) + " 00:00:00";
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdtimeto))
            {
                var apprDTimeUTCTo = CUtils.StrValue(apprdtimeto) + " 23:59:59";
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.ApprDTimeUTC, CUtils.StrValue(apprdtimeto) + " 23:59:59", "<=");
            }
            if (!CUtils.IsNullOrEmpty(invintype))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.InvInType, CUtils.StrValue(invintype), "=");
            }
            if (!CUtils.IsNullOrEmpty(invcodein))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.InvCodeIn, CUtils.StrValue(invcodein), "=");
            }
            if (!CUtils.IsNullOrEmpty(customercode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.CustomerCode, CUtils.StrValue(customercode), "=");
            }
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, "%" + CUtils.StrValue(productcode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(orgid))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.OrgID, CUtils.StrValue(orgid), "=");
            }
            if (!CUtils.IsNullOrEmpty(refno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.RefNo, "%" + CUtils.StrValue(refno) + "%", "like");
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
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.IF_InvInStatus, status, "in");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_InvF_InventoryInByID(string if_invinno)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryIn = TableName.InvF_InventoryIn + ".";
            if (!CUtils.IsNullOrEmpty(if_invinno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.IF_InvInNo, CUtils.StrValue(if_invinno), "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");
            
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValueOrNull(data.ProductCodeUser), "=");
            }
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

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

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

        private string strWhereClause_Mst_Product(string productcode = "", string flagactive = "", string producttype = "", string flagbuy = "")
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                var sbSqlProductCode = new StringBuilder();
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
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
            if (!CUtils.IsNullOrEmpty(flagbuy))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, CUtils.StrValue(flagbuy), "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", "=");

            if (!CUtils.IsNullOrEmpty(data.ProductBarCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductBarCode, CUtils.StrValueOrNull(data.ProductBarCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClauseSearch_Mst_PrintOrder(Mst_PrintOrder data)
        {
            var strWhereClause = "";
            var Tbl_Mst_PrintOrder = TableName.Mst_PrintOrder + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(data.PrintOrdNo))
            {
                sbSql.AddWhereClause(Tbl_Mst_PrintOrder + TblMst_PrintOrder.PrintOrdNo, CUtils.StrValue(data.PrintOrdNo), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        #endregion

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportProduct(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
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
                    

                    var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryInDtlUI>(table);
                    //lstProductCodeUser = listDtlImport.Select(m => m.ProductCodeUser.ToString()).ToList();
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

                    foreach (var productCodeUser in lstProductCodeUser_Distinct)
                    {
                        var listPrdCheck = new List<Mst_Product>();
                        if (listProduct != null && listProduct.Count > 0)
                        {
                            var objMst_Product = listProduct.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).FirstOrDefault();
                            if(objMst_Product == null)
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
                                var objInvF_InventoryInDtlUI = new InvF_InventoryInDtlUI()
                                {
                                    ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                    ProductCodeBase = productCodeBase,
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    ProductType = CUtils.StrValue(objMst_Product.ProductType),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    Qty = fQty,
                                    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                                    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                    UPInDesc = 0,
                                    InvCodeInActual = strInvCodeInActual,
                                    Remark = "",
                                    Lst_Product_InvCodeInActual = new List<InvF_InventoryInDtlUI>(),
                                };

                                var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                if (listDtlImportCur != null && listDtlImportCur.Count > 0)
                                {
                                    var i = 0;
                                    foreach (var _it in listDtlImportCur)
                                    {
                                        var objProduct_InvCodeInActual = new InvF_InventoryInDtlUI()
                                        {
                                            //ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                            //ProductCodeBase = CUtils.StrValue(objMst_Product.ProductCodeBase),
                                            ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                            //ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                            //ProductName = CUtils.StrValue(objMst_Product.ProductName),

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

                                listInvFInventoryInDtlUI.Add(objInvF_InventoryInDtlUI);
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
                    if (listProductBase != null && listProductBase.Count > 0)
                    {
                        foreach (var item in listInvFInventoryInDtlUI)
                        {
                            var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                            if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                            {
                                foreach (var _it in listPrdCheckBase)
                                {
                                    var objInvFInventoryInDtlUI = new InvF_InventoryInDtlUI();
                                    var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                    var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                    var productCode = CUtils.StrValue(_it.ProductCode);
                                    var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                    var productName = CUtils.StrValue(_it.ProductName);
                                    var productType = CUtils.StrValue(_it.ProductType);
                                    var unitCode = CUtils.StrValue(_it.UnitCode);
                                    var qty = "0";
                                    var uPIn = CUtils.StrValue(_it.UPBuy);
                                    var uPInDesc = "0";
                                    var invCodeInActual = "";
                                    var flagLot = CUtils.StrValue(_it.FlagLot);
                                    var flagSerial = CUtils.StrValue(_it.FlagSerial);
                                    var remark = "";
                                    var objMst_Product = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(_it.ProductCode))).FirstOrDefault();
                                    if(objMst_Product != null)
                                    {
                                        qty = CUtils.StrValue(objMst_Product.Qty);
                                        uPIn = CUtils.StrValue(objMst_Product.UPIn);
                                        invCodeInActual = CUtils.StrValue(objMst_Product.InvCodeInActual);
                                    }
                                    objInvFInventoryInDtlUI.ProductCodeRoot = productCodeRoot;
                                    objInvFInventoryInDtlUI.ProductCodeBase = productCodeBase;
                                    objInvFInventoryInDtlUI.ProductCode = productCode;
                                    objInvFInventoryInDtlUI.ProductCodeUser = productCodeUser;
                                    objInvFInventoryInDtlUI.ProductName = productName;
                                    objInvFInventoryInDtlUI.ProductType = productType;
                                    objInvFInventoryInDtlUI.UnitCode = unitCode;
                                    objInvFInventoryInDtlUI.Qty = qty;
                                    objInvFInventoryInDtlUI.UPIn = uPIn;
                                    objInvFInventoryInDtlUI.FlagLot = flagLot;
                                    objInvFInventoryInDtlUI.FlagSerial = flagSerial;
                                    objInvFInventoryInDtlUI.UPInDesc = uPInDesc;
                                    objInvFInventoryInDtlUI.InvCodeInActual = invCodeInActual;
                                    objInvFInventoryInDtlUI.Remark = remark;
                                    if (item.Lst_InvF_InventoryInDtlUI == null || item.Lst_InvF_InventoryInDtlUI.Count == 0)
                                    {
                                        item.Lst_InvF_InventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
                                    }
                                    item.Lst_InvF_InventoryInDtlUI.Add(objInvFInventoryInDtlUI);
                                }
                            }
                        }
                    }
                    
                    #endregion
                    #endregion

                    return Json(new { Success = true, ListInvFInventoryInDtlUI = listInvFInventoryInDtlUI});
                    //return JsonView("LoadProduct", listInvFInventoryInDtlUI);

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
        public ActionResult ImportLot(HttpPostedFileWrapper file,string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInvFInventoryInInstLot = new List<InvF_InventoryInInstLot>();
            var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
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
                    if(table.Columns.Count != GetImportDicColumsTemplateLot().Count)
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
                        if (CUtils.IsNullOrEmpty(dr["ProductLotNo"]))
                        {
                            exitsData = "Số lô không được để trống!";
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

                        var productionDate = CUtils.StrValue(dr["ProductionDate"]);
                        if (!CUtils.IsNullOrEmpty(productionDate) && CUtils.IsDateTime(productionDate))
                        {
                            try
                            {
                                dr["ProductionDate"] = CUtils.ConvertToDateTime(productionDate).ToString(Nonsense.DATE_TIME_FORMAT);
                            }
                            catch
                            {

                            }
                           
                        }
                        var expiredDate = CUtils.StrValue(dr["ExpiredDate"]);
                        if (!CUtils.IsNullOrEmpty(expiredDate) && CUtils.IsDateTime(expiredDate))
                        {
                            try
                            {
                                dr["ExpiredDate"] = CUtils.ConvertToDateTime(expiredDate).ToString(Nonsense.DATE_TIME_FORMAT);
                            }
                            catch
                            {

                            }

                        }
                    }
                    #endregion
                    #region["Check mã hàng hóa trùng"]

                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        var productCodeUser1 = CUtils.StrValue(table.Rows[i][TblMst_Product.ProductCodeUser]);
                        var invCodeInActual1 = CUtils.StrValue(table.Rows[i][TblInvF_InventoryInDtl.InvCodeInActual]);
                        var productLotNo1 = CUtils.StrValue(table.Rows[i]["ProductLotNo"]);
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            if (i != j)
                            {
                                var productCodeUser2 = CUtils.StrValue(table.Rows[j][TblMst_Product.ProductCodeUser]);
                                var invCodeInActual2 = CUtils.StrValue(table.Rows[j][TblInvF_InventoryInDtl.InvCodeInActual]);
                                var productLotNo2 = CUtils.StrValue(table.Rows[j]["ProductLotNo"]);
                                if (productCodeUser1.Equals(productCodeUser2) && invCodeInActual1.Equals(invCodeInActual2) && productLotNo1.Equals(productLotNo2))
                                {
                                    exitsData = "Mã hàng hóa '" + productCodeUser1 + "' thuộc lô '" + productLotNo1 + "' có vị trí '" + invCodeInActual1 + "' trong file excel lặp!";
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
                                var objInvF_InventoryInDtlUI = new InvF_InventoryInDtlUI()
                                {
                                    ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                    ProductCodeBase = productCodeBase,
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    ProductType = CUtils.StrValue(objMst_Product.ProductType),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    Qty = fQty,
                                    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                                    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                    UPInDesc = 0,
                                    InvCodeInActual = strInvCodeInActual,
                                    Remark = "",
                                    Lst_Product_InvCodeInActual = new List<InvF_InventoryInDtlUI>(),
                                    Lst_Product_InvLot = new List<InvF_InventoryInDtlUI>(),
                                };
                                var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                var listInvCodeIn_Distinct = listDtlImportCur.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeInActual)).Select(item => CUtils.StrValue(item.InvCodeInActual)).Distinct().ToList();
                                if (listDtlImportCur != null && listDtlImportCur.Count > 0)
                                {
                                    var i = 0;
                                    if (listInvCodeIn_Distinct != null && listInvCodeIn_Distinct.Count > 0)
                                    {
                                        strInvCodeInActual = "";
                                        foreach (var invCode in listInvCodeIn_Distinct)
                                        {
                                            
                                            var listInvCodeIn = listDtlImportCur.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeInActual) && CUtils.StrValue(it.InvCodeInActual).Equals(invCode)).ToList();
                                            
                                            if(listInvCodeIn != null && listInvCodeIn.Count > 1)
                                            {
                                                foreach (var _item in listInvCodeIn)
                                                {
                                                    var objProduct = new InvF_InventoryInDtlUI()
                                                    {
                                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                                        ProductLotNo = CUtils.StrValue(_item.ProductLotNo),
                                                        InvCodeInActual = CUtils.StrValue(_item.InvCodeInActual),
                                                        Qty = _item.Qty,
                                                    };
                                                    objInvF_InventoryInDtlUI.Lst_Product_InvCodeInActual.Add(objProduct);
                                                    fQty += CUtils.ConvertToDouble(_item.Qty);
                                                    objInvF_InventoryInDtlUI.Qty = fQty;
                                                    objInvF_InventoryInDtlUI.InvCodeInActual = objProduct.InvCodeInActual;
                                                    objInvF_InventoryInDtlUI.ProductLotNo = objProduct.ProductLotNo;

                                                    if (!strInvCodeInActual.Contains(CUtils.StrValue(_item.InvCodeInActual)))
                                                    {
                                                        if (!CUtils.IsNullOrEmpty(strInvCodeInActual))
                                                        {
                                                            strInvCodeInActual += ", ";
                                                        }

                                                        strInvCodeInActual += CUtils.StrValue(_item.InvCodeInActual);
                                                    }
                                                }
                                           
                                            }
                                            else
                                            {
                                                // 2023-01-04: không hiểu code này làm gì, cứ để nguyên như cũ
                                                if (i != 0)
                                                {
                                                    strInvCodeInActual += ", ";
                                                }
                                                strInvCodeInActual += CUtils.StrValue(listInvCodeIn[0].InvCodeInActual);
                                                var qty2 = 0.0;
                                                qty2 = CUtils.ConvertToDouble(listInvCodeIn[0].Qty);
                                                var qty1 = CUtils.ConvertToDouble(objInvF_InventoryInDtlUI.Qty);
                                                qty1 += qty2;
                                                objInvF_InventoryInDtlUI.Qty = CUtils.StrValue(qty1);
                                                i++;
                                            }
                                        }
                                        objInvF_InventoryInDtlUI.InvCodeInActual = strInvCodeInActual;
                                    }
                                    else
                                    {
                                        foreach (var _it in listDtlImportCur)
                                        {
                                            var objProduct_InvCodeInActual = new InvF_InventoryInDtlUI()
                                            {
                                                ProductCode = CUtils.StrValue(objMst_Product.ProductCode),

                                                InvCodeInActual = CUtils.StrValue(_it.InvCodeInActual),
                                                Qty = _it.Qty,
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
                                }

                                foreach (var _is in listDtlImportCur)
                                {
                                    var objLot = new InvF_InventoryInDtlUI()
                                    {
                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                        InvCodeInActual = _is.InvCodeInActual,
                                        Qty = _is.Qty,
                                        ProductLotNo = _is.ProductLotNo,
                                        ProductionDate = _is.ProductionDate,
                                        ExpiredDate = _is.ExpiredDate,
                                    };
                                    objInvF_InventoryInDtlUI.Lst_Product_InvLot.Add(objLot);
                                }

                                listInvFInventoryInDtlUI.Add(objInvF_InventoryInDtlUI);
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
                        foreach (var item in listInvFInventoryInDtlUI)
                        {
                            var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                            if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                            {
                                foreach (var _it in listPrdCheckBase)
                                {
                                    var objInvFInventoryInDtlUI = new InvF_InventoryInDtlUI();
                                    var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                    var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                    var productCode = CUtils.StrValue(_it.ProductCode);
                                    var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                    var productName = CUtils.StrValue(_it.ProductName);
                                    var productType = CUtils.StrValue(_it.ProductType);
                                    var unitCode = CUtils.StrValue(_it.UnitCode);
                                    var qty = "0";
                                    var uPIn = CUtils.StrValue(_it.UPBuy);
                                    var uPInDesc = "0";
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

                                    objInvFInventoryInDtlUI.ProductCodeRoot = productCodeRoot;
                                    objInvFInventoryInDtlUI.ProductCodeBase = productCodeBase;
                                    objInvFInventoryInDtlUI.ProductCode = productCode;
                                    objInvFInventoryInDtlUI.ProductCodeUser = productCodeUser;
                                    objInvFInventoryInDtlUI.ProductName = productName;
                                    objInvFInventoryInDtlUI.ProductType = productType;
                                    objInvFInventoryInDtlUI.UnitCode = unitCode;
                                    objInvFInventoryInDtlUI.Qty = qty;
                                    objInvFInventoryInDtlUI.UPIn = uPIn;
                                    objInvFInventoryInDtlUI.FlagLot = flagLot;
                                    objInvFInventoryInDtlUI.FlagSerial = flagSerial;
                                    objInvFInventoryInDtlUI.UPInDesc = uPInDesc;
                                    objInvFInventoryInDtlUI.InvCodeInActual = invCodeInActual;
                                    objInvFInventoryInDtlUI.Remark = remark;

                                    if (item.Lst_InvF_InventoryInDtlUI == null || item.Lst_InvF_InventoryInDtlUI.Count == 0)
                                    {
                                        item.Lst_InvF_InventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
                                    }
                                    item.Lst_InvF_InventoryInDtlUI.Add(objInvFInventoryInDtlUI);
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                    return Json(new { Success = true, ListInvFInventoryInDtlUI = listInvFInventoryInDtlUI });
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
            var listInvFInventoryInInstSerial = new List<InvF_InventoryInInstSerial>();
            var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
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
                if(table != null && table.Rows.Count > 0)
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
                            exitsData = "Số serial không được để trống!";
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
                        var serialNo1 = CUtils.StrValue(table.Rows[i]["SerialNo"]);
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            if (i != j)
                            {
                                var productCodeUser2 = CUtils.StrValue(table.Rows[j][TblMst_Product.ProductCodeUser]);
                                var serialNo2 = CUtils.StrValue(table.Rows[j]["SerialNo"]);
                                if (productCodeUser1.Equals(productCodeUser2) && serialNo1.Equals(serialNo2))
                                {
                                    exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có số serial '" + serialNo1 + "' trong file excel lặp!";
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
                        if(listProduct != null && listProduct.Count > 0)
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
                                var objInvF_InventoryInDtlUI = new InvF_InventoryInDtlUI()
                                {
                                    ProductCodeRoot = CUtils.StrValue(objMst_Product.ProductCodeRoot),
                                    ProductCodeBase = productCodeBase,
                                    ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                    ProductCodeUser = CUtils.StrValue(objMst_Product.ProductCodeUser),
                                    ProductName = CUtils.StrValue(objMst_Product.ProductName),
                                    ProductType = CUtils.StrValue(objMst_Product.ProductType),
                                    UnitCode = CUtils.StrValue(objMst_Product.UnitCode),
                                    Qty = fQty,
                                    UPIn = CUtils.StrValue(objMst_Product.UPBuy),
                                    FlagLot = CUtils.StrValue(objMst_Product.FlagLot),
                                    FlagSerial = CUtils.StrValue(objMst_Product.FlagSerial),
                                    UPInDesc = 0,
                                    InvCodeInActual = strInvCodeInActual,
                                    Remark = "",
                                    SerialNo = "",
                                    Lst_Product_InvCodeInActual = new List<InvF_InventoryInDtlUI>(),
                                    Lst_Product_InvSerial = new List<InvF_InventoryInDtlUI>(),
                                };

                                var listDtlImportCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser) && CUtils.StrValue(it.ProductCodeUser).Equals(productCodeUser)).ToList();
                                if(listDtlImportCur != null && listDtlImportCur.Count > 0)
                                {
                                    var i = 0;
                                    foreach (var _it in listDtlImportCur)
                                    {
                                        var objProduct_InvCodeInActual = new InvF_InventoryInDtlUI()
                                        {
                                            ProductCode = CUtils.StrValue(objMst_Product.ProductCode),

                                            InvCodeInActual = CUtils.StrValue(_it.InvCodeInActual),
                                            Qty = 1,
                                        };
                                        objInvF_InventoryInDtlUI.Lst_Product_InvCodeInActual.Add(objProduct_InvCodeInActual);

                                        fQty += 1;
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
                                
                                foreach(var _is in listDtlImport)
                                {
                                    var objSerial = new InvF_InventoryInDtlUI()
                                    {
                                        ProductCode = CUtils.StrValue(objMst_Product.ProductCode),
                                        SerialNo = _is.SerialNo,
                                        InvCodeInActual = _is.InvCodeInActual
                                    };
                                    objInvF_InventoryInDtlUI.Lst_Product_InvSerial.Add(objSerial);
                                }

                                listInvFInventoryInDtlUI.Add(objInvF_InventoryInDtlUI);
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
                        foreach (var item in listInvFInventoryInDtlUI)
                        {
                            var listPrdCheckBase = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCode)
                                && CUtils.StrValue(it.ProductCodeBase).Equals(CUtils.StrValue(item.ProductCodeBase))
                                && !CUtils.StrValue(it.ProductCode).Equals(CUtils.StrValue(item.ProductCode))
                                ).ToList();
                            if (listPrdCheckBase != null && listPrdCheckBase.Count > 0)
                            {
                                foreach (var _it in listPrdCheckBase)
                                {
                                    var objInvFInventoryInDtlUI = new InvF_InventoryInDtlUI();
                                    var productCodeRoot = CUtils.StrValue(_it.ProductCodeRoot);
                                    var productCodeBase = CUtils.StrValue(_it.ProductCodeBase);
                                    var productCode = CUtils.StrValue(_it.ProductCode);
                                    var productCodeUser = CUtils.StrValue(_it.ProductCodeUser);
                                    var productName = CUtils.StrValue(_it.ProductName);
                                    var productType = CUtils.StrValue(_it.ProductType);
                                    var unitCode = CUtils.StrValue(_it.UnitCode);
                                    var qty = "0";
                                    var uPIn = CUtils.StrValue(_it.UPBuy);
                                    var uPInDesc = "0";
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

                                    objInvFInventoryInDtlUI.ProductCodeRoot = productCodeRoot;
                                    objInvFInventoryInDtlUI.ProductCodeBase = productCodeBase;
                                    objInvFInventoryInDtlUI.ProductCode = productCode;
                                    objInvFInventoryInDtlUI.ProductCodeUser = productCodeUser;
                                    objInvFInventoryInDtlUI.ProductName = productName;
                                    objInvFInventoryInDtlUI.ProductType = productType;
                                    objInvFInventoryInDtlUI.UnitCode = unitCode;
                                    objInvFInventoryInDtlUI.Qty = qty;
                                    objInvFInventoryInDtlUI.UPIn = uPIn;
                                    objInvFInventoryInDtlUI.FlagLot = flagLot;
                                    objInvFInventoryInDtlUI.FlagSerial = flagSerial;
                                    objInvFInventoryInDtlUI.UPInDesc = uPInDesc;
                                    objInvFInventoryInDtlUI.InvCodeInActual = invCodeInActual;
                                    objInvFInventoryInDtlUI.Remark = remark;

                                    if (item.Lst_InvF_InventoryInDtlUI == null || item.Lst_InvF_InventoryInDtlUI.Count == 0)
                                    {
                                        item.Lst_InvF_InventoryInDtlUI = new List<InvF_InventoryInDtlUI>();
                                    }
                                    item.Lst_InvF_InventoryInDtlUI.Add(objInvFInventoryInDtlUI);
                                }
                            }
                        }
                    }
                    #endregion
                    

                    #endregion
                    return Json(new { Success = true, ListInvFInventoryInDtlUI = listInvFInventoryInDtlUI });
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
        public ActionResult ImportQR(HttpPostedFileWrapper file, string listProduct)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var listInvF_InventoryInQR = new List<InvF_InventoryInQR>();
            if (ModelState.IsValid)
            {
                var listPrd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryInDtlUI>>(listProduct);
                if (listPrd == null || listPrd.Count == 0)
                {
                    exitsData = "Lưới danh mục hàng hóa trống!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {

                    resultEntry.AddMessage(Nonsense.MESS_CHECK_FILEIMPORT);
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
                    if (table.Columns.Count != 7)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    #region["Check null + Split data"]

                    foreach (DataRow dr in table.Rows)
                    {
                        if (dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
                        {
                            exitsData = "Mã hàng hóa không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }

                        if (dr["QRCode"] == null || dr["QRCode"].ToString().Trim().Length == 0)
                        {
                            exitsData = "Mã xác thực không được để trống!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            string qrCode = dr["QRCode"].ToString().Trim();
                            string[] arSplit = qrCode.Split('='); 
                            if(arSplit.Length >= 2)
                            {
                                dr["QRCode"] = arSplit[1];
                            }
                        }
                        
                        if (dr["BoxNo"] != null && dr["BoxNo"].ToString().Trim().Length > 0)
                        {
                            string boxNo = dr["BoxNo"].ToString().Trim();
                            string[] arSplit = boxNo.Split('=');
                            if (arSplit.Length >= 2)
                            {
                                dr["BoxNo"] = arSplit[1];
                            }
                        }
                        if (dr["CanNo"] != null && dr["CanNo"].ToString().Trim().Length > 0)
                        {
                            string canNo = dr["CanNo"].ToString().Trim();
                            string[] arSplit = canNo.Split('=');
                            if (arSplit.Length >= 2)
                            {
                                dr["CanNo"] = arSplit[1];
                            }
                        }
                    }
                    #endregion

                    #region["Check mã hàng hóa, QRcode trùng"]

                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        var productCode1 = table.Rows[i]["ProductCodeUser"].ToString().Trim();
                        var qRCode1 = table.Rows[i]["QRCode"].ToString().Trim();
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            if (i != j)
                            {
                                var productCode2 = table.Rows[j]["ProductCodeUser"].ToString().Trim();
                                var qRCode2 = table.Rows[j]["QRCode"].ToString().Trim();
                                if (productCode1 == productCode2 && qRCode1 == qRCode2)
                                {
                                    exitsData = "Mã xác thực '" + qRCode1 + "' của mã hàng hóa '" + productCode1 + "' trong file excel lặp!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }
                    }
                    #endregion

                    #region["Thêm mã xác thực"]
                    foreach (DataRow dr in table.Rows)
                    {
                        var productCodeUser = dr["ProductCodeUser"].ToString().Trim();
                        var qRCode = dr["QRCode"].ToString().Trim();

                        var checkExist = listPrd.Any(item => item.ProductCodeUser.ToString() == productCodeUser);
                        if (checkExist)
                        {
                            #region["Add"]

                            var lstPrdCur = listPrd.Where(m => m.ProductCodeUser.ToString() == productCodeUser).ToList();

                            var item = new InvF_InventoryInQR();
                            item.ProductCode = lstPrdCur[0].ProductCode;
                            item.mp_Productname = lstPrdCur[0].ProductName;
                            item.mp_ProductCodeUser = productCodeUser;
                            item.QRCode = qRCode;
                            item.BoxNo = Convert.ToString(dr["BoxNo"]);
                            item.CanNo = Convert.ToString(dr["CanNo"]);
                            item.ProductLotNo = Convert.ToString(dr["ProductLotNo"]);
                            item.ShiftInCode = Convert.ToString(dr["ShiftInCode"]);
                            item.UserKCS = Convert.ToString(dr["UserKCS"]);
                            listInvF_InventoryInQR.Add(item);

                            #endregion
                        }
                        else
                        {
                            exitsData = "Mã hàng hóa '" + productCodeUser + "' không có trong lưới danh mục hàng hóa!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }

                    }
                    #endregion
                    return Json(new { Success = true, Lst_InvF_InventoryInQR = listInvF_InventoryInQR });
                    //return JsonView("LoadQRCode", listInvF_InventoryInQR);
                }
                else
                {
                    exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
            }
            return Json(resultEntry);
        }

        #endregion

        #region["Export Excel"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(
            string if_invinno = "",
            string createdtimefrom = "",
            string createdtimeto = "", 
            string apprdtimefrom = "",
            string apprdtimeto = "",
            string invintype = "", 
            string customercode = "",
            string invcodein = "",
            string productcode = "",
            string orgid = "",
            string refno = "",
            string chkpending = "",
            string chkapproved = "",
            string chkcanceled = ""
            )
        {
            var userState = this.UserState;
            var resultEntry = new JsonResultEntry() { Success = false };
            //var itemCount = 0;
            //var pageCount = 0;
            var itemCount = 0;
            var pageCount = 0;
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            

            try
            {
                var listExport = new List<InvF_InventoryIn>();
                
                var strWhere = strWhereClause_InvF_InventoryIn(if_invinno, createdtimefrom, createdtimeto, apprdtimefrom, apprdtimeto, invintype, customercode, invcodein, productcode, refno, orgid, chkpending, chkapproved, chkcanceled);
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Rt_Cols_InvF_InventoryIn = "*",
                    Ft_WhereClause = strWhere
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryIn);
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                {
                    if (objRT_InvF_InventoryIn.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_InvF_InventoryIn.MySummaryTable.MyCount);
                    }
                    if (objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        listExport.AddRange(objRT_InvF_InventoryIn.Lst_InvF_InventoryIn);
                    }

                    foreach (var item in listExport)
                    {
                        var apprDate = CUtils.StrValue(item.ApprDTimeUTC);
                        var createDate = CUtils.StrValue(item.CreateDTimeUTC);

                        if (!CUtils.IsNullOrEmpty(apprDate))
                        {
                            item.ApprDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(apprDate, Nonsense.DATE_TIME_FORMAT, userState.UtcOffset);
                        }

                        if (!CUtils.IsNullOrEmpty(createDate))
                        {
                            item.CreateDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(createDate, Nonsense.DATE_TIME_FORMAT, userState.UtcOffset);
                        }

                    }

                    Dictionary<string, string> dicColNames = GetExportDicColumsMaster();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);

                    ExcelExport.ExportToExcelFromList(listExport, dicColNames, filePath, string.Format("InvF_InventoryIn"));


                    #region["Xuất excell các trang còn lại"]

                    listExport.Clear();
                    if (pageCount > 0)
                    {
                        for (var i = 0; i <= pageCount; i++)
                        {
                            objRQ_InvF_InventoryIn.Tid = GetNextTId();
                            objRQ_InvF_InventoryIn.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_InvF_InventoryIncur = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);


                            if (objRT_InvF_InventoryIncur.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                            {
                                listExport.AddRange(objRT_InvF_InventoryIncur.Lst_InvF_InventoryIn);

                                foreach (var item in listExport)
                                {
                                    var apprDate = CUtils.StrValue(item.ApprDTimeUTC);
                                    var createDate = CUtils.StrValue(item.CreateDTimeUTC);

                                    if (!CUtils.IsNullOrEmpty(apprDate))
                                    {
                                        item.ApprDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(apprDate, Nonsense.DATE_TIME_FORMAT, userState.UtcOffset);
                                    }

                                    if (!CUtils.IsNullOrEmpty(createDate))
                                    {
                                        item.CreateDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(createDate, Nonsense.DATE_TIME_FORMAT, userState.UtcOffset);
                                    }

                                }

                            }
                        }
                    }

                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu!", CheckSuccess = "1" });
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
                var listInvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateProduct();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInDtl).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInvF_InventoryInDtl, dicColNames, filePath, string.Format("InvF_InventoryInDtl"));

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
                var list = new List<InvF_InventoryInInstLot>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateLot();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInInstLot).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryInInstLot"));

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
                var list = new List<InvF_InventoryInInstSerial>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateSerial();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInInstSerial).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryInInstSerial"));

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
                var list = new List<InvF_InventoryInQR>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateQR();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInQR).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryInQR"));

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
                 {"ProductCodeUser","Mã hàng hóa *"},
                 {"Qty","Số lượng *"},
                 {"InvCodeInActual","Vị trí *"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateLot()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa *"},
                 {"ProductLotNo","Số lô *"},
                 {"Qty","Số lượng *"},
                 {"ProductionDate","Ngày sản xuất"},
                 {"ExpiredDate","Ngày hết hạn"},
                 {"InvCodeInActual","Vị trí *"},
                 {"Remark","Ghi chú"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplateSerial()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa *"},
                 {"SerialNo","Số serial *"},
                 {"InvCodeInActual","Vị trí *"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplateQR()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa *"},
                 {"QRCode","Mã xác thực *"},
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
                {"IF_InvInNo","Số phiếu nhập"},
                {"ApprDTimeUTC","Thời gian nhập"},
                {"InvCodeIn","Kho nhập"},
                {"mct_CustomerName","Nhà cung cấp"},
                {"TotalValInAfterDesc", "Tổng tiền"},
                {"IF_InvInStatus", "Trạng thái"},
                {"Remark", "Nội dung nhập"},
                {"InvoiceNo", "Số hóa đơn"},
                {"CreateDTimeUTC", "Thời gian tạo"},
                {"InvInType", "Loại phiếu nhập"},
                {"UserDeliver", "Người giao hàng"},
                {"OrderNo", "Số đơn hàng"},
                {"FlagQR", "Cờ FlagQR"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvInNo","Số phiếu nhập"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"mp_ProductName","Tên hàng hóa (TV)"},
                {"UnitCode","Đơn vị"},
                {"Qty", "Số lượng"},
                {"InvCodeInActual", "Vị trí nhập"},
                {"UPIn", "Giá nhập"},
                {"UPInDesc", "Giảm giá"},
                {"ValInAfterDesc", "Thành tiền"},
                {"Remark", "Ghi chú"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsLot()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvInNo","Số phiếu nhập"},
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
                {"IF_InvInNo","Số phiếu nhập"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"SerialNo","Số serial"},                
                {"InvCodeInActual", "Vị trí"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsQR()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvInNo","Số phiếu nhập"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"mp_Productname","Tên hàng hóa"},
                {"QRCode", "Mã xác thực"},
                {"BoxNo","Mã hộp"},
                {"CanNo","Mã thùng"},
                {"ProductLotNo","Lô sản xuất"},
                {"ShiftInCode", "Ca sản xuất"},
                {"UserKCS", "KCS"}
            };
        }
        #endregion
        #endregion

        #region ["In"]
        [HttpPost]
        public ActionResult PrintTemp(string if_invinno)
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

                InvF_InventoryIn objInvF_InventoryIn = new InvF_InventoryIn();
                var strWhere = strWhereClause_InvF_InventoryInByID(if_invinno);
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "*",
                    Rt_Cols_InvF_InventoryInInstLot = "",
                    Rt_Cols_InvF_InventoryInInstSerial = "",
                    Rt_Cols_InvF_InventoryInQR = "",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count != 0)
                {
                    objInvF_InventoryIn = objRT_InvF_InventoryIn.Lst_InvF_InventoryIn[0];
                }

                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvFInventoryInDtlUI = new List<InvF_InventoryInDtlUI>();

                var Lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl != null)
                {
                    Lst_InvF_InventoryInDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                }

                var lstPrdDistinct = new List<string>();
                int idx = 1;
                foreach (var item in Lst_InvF_InventoryInDtl)
                {
                    if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
                        continue;

                    var listInvInDtlCur = Lst_InvF_InventoryInDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    var qty = 0.0;
                    var valInAfterDesc = 0.0;
                    var invCodeInActual = "";
                    var invCodeInActualName = "";
                    var listViTri = new List<string>();
                    foreach (var it in listInvInDtlCur)
                    {
                        qty += Convert.ToDouble(it.Qty);
                        valInAfterDesc += Convert.ToDouble(it.ValInAfterDesc);
                        if (!listViTri.Contains(it.InvCodeInActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeInActual))
                            {
                                invCodeInActual += ", " + it.InvCodeInActual.ToString();
                                invCodeInActualName += ", " + it.mii_InvName.ToString();
                            }
                            else
                            {
                                invCodeInActual = it.InvCodeInActual.ToString();
                                invCodeInActualName = it.mii_InvName.ToString();
                            }
                            listViTri.Add(it.InvCodeInActual.ToString());
                        }
                    }

                    var itemUI = new InvF_InventoryInDtlUI()
                    {
                        IF_InvInNo = item.IF_InvInNo,
                        InvCodeInActual = invCodeInActual,
                        ProductCode = item.ProductCode,
                        NetworkID = item.NetworkID,
                        Qty = qty,
                        UPIn = item.UPIn,
                        UPInDesc = item.UPInDesc,
                        ValInvIn = item.ValInvIn,
                        ValInDesc = item.ValInDesc,
                        ValInAfterDesc = valInAfterDesc,
                        UnitCode = item.UnitCode,
                        IF_InvInStatusDtl = item.IF_InvInStatusDtl,
                        Remark = item.Remark,
                        ProductName = item.mp_ProductName,
                        ProductCodeUser = item.mp_ProductCodeUser,                        
                        InvName = invCodeInActualName,
                        Idx = idx
                    };

                    //
                    listInvFInventoryInDtlUI.Add(itemUI);
                    idx++;

                    lstPrdDistinct.Add(item.ProductCode.ToString());
                }

                #endregion
                //
                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if(listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                InvF_InventoryInPrint objPrint = new InvF_InventoryInPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.NNTFullName = strNNTFullName;
                objPrint.IF_InvInNo = objInvF_InventoryIn.IF_InvInNo;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CustomerName = objInvF_InventoryIn.mct_CustomerName;
                objPrint.OrderNo = objInvF_InventoryIn.OrderNo;
                objPrint.InvInTypeName = objInvF_InventoryIn.miit_InvInTypeName;
                objPrint.Remark = objInvF_InventoryIn.Remark;
                objPrint.TotalQty = listInvFInventoryInDtlUI.Sum(m => Convert.ToDouble(m.Qty));
                objPrint.TotalValIn = objInvF_InventoryIn.TotalValIn;
                objPrint.TotalValInDesc = objInvF_InventoryIn.TotalValInDesc;
                objPrint.TotalValInAfterDesc = objInvF_InventoryIn.TotalValInAfterDesc;
                objPrint.CreateUserName = objInvF_InventoryIn.su_UserName_Create;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_InventoryInDtlUI = listInvFInventoryInDtlUI;

                #region Lấy mẫu in

                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("PN");
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

        
        public dynamic CreateDataObjectReportServer(InvF_InventoryInPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_InventoryInDtl;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);

            var MyDynamic = new InvF_InventoryInReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.IF_InvInNo = CUtils.StrValueNew(objTempPrint.IF_InvInNo);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.CustomerName = CUtils.StrValueNew(objTempPrint.CustomerName);
                MyDynamic.OrderNo = CUtils.StrValueNew(objTempPrint.OrderNo);
                MyDynamic.InvInTypeName = CUtils.StrValueNew(objTempPrint.InvInTypeName);
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, NumericFormat(tableName, "TotalQty", defaultFormat));
                MyDynamic.TotalValIn = CUtils.StrValueFormatNumber(objTempPrint.TotalValIn, NumericFormat(tableName, "TotalValIn", defaultFormat));
                MyDynamic.TotalValInDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValInDesc, NumericFormat(tableName, "TotalValInDesc", defaultFormat));
                MyDynamic.TotalValInAfterDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValInAfterDesc, NumericFormat(tableName, "TotalValInAfterDesc", defaultFormat));
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_InventoryInDtlReportServer>();
            
            if (objTempPrint != null && objTempPrint.Lst_InvF_InventoryInDtlUI != null && objTempPrint.Lst_InvF_InventoryInDtlUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_InventoryInDtlReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_InventoryInDtlUI)
                {
                    var objDtl_ReportServer = new InvF_InventoryInDtlReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.ProductCodeUser),
                        ProductName = CUtils.StrValue(item.ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        InvName = CUtils.StrValue(item.InvName),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, strFormatQty),
                        UPIn = CUtils.StrValueFormatNumber(item.UPIn, NumericFormat(tableName, "UPIn", defaultFormat)),
                        UPInDesc = CUtils.StrValueFormatNumber(item.UPInDesc, NumericFormat(tableName, "UPInDesc", defaultFormat)),
                        ValInvIn = CUtils.StrValueFormatNumber(item.ValInvIn, NumericFormat(tableName, "ValInvIn", defaultFormat)),
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