using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class InvFInventoryReturnSupController : AdminBaseController
    {
        //public ActionResult Index(string if_invreturnsupno, string customercode, string createdtimefrom, string createdtimeto, string approvedtimefrom, string approvedtimeto, string invcodeout, string orgid, string chkpending = "", string chkapproved = "", string chkcanceled = "", int page = 0, int recordcount = 10, string init = "init")
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    var itemCount = 0;
        //    var pageCount = 0;
        //    ViewBag.PageCur = page;
        //    ViewBag.Recordcount = recordcount;
        //    ViewBag.UserState = UserState;
        //    #region ["UserState Common Info"]
        //    var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
        //    var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
        //    var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
        //    var orgId = UserState.Mst_NNT.OrgID;
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    try
        //    {
        //        #region Thông tin kho xuất
        //        var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
        //        var lst_Mst_Inventory = new List<Mst_Inventory>();
        //        var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
        //            Rt_Cols_Mst_Inventory = "*",
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            Ft_WhereClause = strWhere_Mst_Inventory
        //        };
        //        var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
        //        if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
        //        {
        //            lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
        //        }
        //        ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
        //        #endregion

        //        #region Thông tin khách hàng
        //        var lstCustomer = new List<Mst_Customer>();
        //        var objRQ_Mst_Customer = new RQ_Mst_Customer()
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
        //            Rt_Cols_Mst_Customer = "*",
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            Ft_WhereClause = "Mst_Customer.FlagActive = '1' and Mst_Customer.FlagSupplier = '1'"
        //        };
        //        var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
        //        if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
        //        {
        //            lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
        //        }
        //        ViewBag.Lst_MstCustomer = lstCustomer;
        //        #endregion

        //        #region Thông tin chi nhánh
        //        string strWhereClauseNNT = "Mst_NNT.MSTBUPattern like '" + CUtils.StrValue(UserState.Mst_NNT.MSTBUPattern) + "' and Mst_NNT.FlagActive = '1'";
        //        var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);

        //        ViewBag.ListMst_NNT = listMst_NNT;
        //        #endregion

        //        var pageInfo = new PageInfo<InvF_InventoryReturnSup>(0, recordcount)
        //        {
        //            DataList = new List<InvF_InventoryReturnSup>()
        //        };
        //        InvF_InventoryReturnSup objInvF_InventoryReturnSup = new InvF_InventoryReturnSup
        //        {
        //            IF_InvReturnSupNo = if_invreturnsupno,                    
        //            CustomerCode = customercode,
        //            InvCodeOut = invcodeout,
        //            OrgID = orgId
        //        };
        //        var strWhere = WhereClauseInvFInventoryReturnSup(objInvF_InventoryReturnSup, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto, chkpending, chkapproved, chkcanceled);
        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
        //            Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
        //            Ft_RecordCount = recordcount.ToString(),
        //            Rt_Cols_InvF_InventoryReturnSup = "*",
        //            Ft_WhereClause = strWhere
        //        };
        //        var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);

        //        if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
        //        {
        //            pageInfo.DataList = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup;
        //            itemCount = objRT_InvF_InventoryReturnSup.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryReturnSup.MySummaryTable.MyCount);
        //            pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
        //        }

        //        pageInfo.PageSize = recordcount;
        //        pageInfo.PageIndex = page;
        //        pageInfo.ItemCount = itemCount;
        //        pageInfo.PageCount = pageCount;
        //        ViewBag.StartCount = (page * recordcount).ToString();
        //        if (!init.Equals("init"))
        //        {
        //            return JsonView("BindDataInvFInventoryReturnSup", pageInfo);
        //        }
        //        ViewBag.init = "loadindex";
        //        return View(pageInfo);
        //    }
        //    catch (ClientException ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return View(resultEntry);
        //}
        public ActionResult Index(
            string if_invreturnsupno = "",
            string customercode = "",
            string createdtimefrom = "",
            string createdtimeto = "",
            string approvedtimefrom = "",
            string approvedtimeto = "",
            string invcodeout = "",
            string orgid = "",
            string chkpending = "",
            string chkapproved = "",
            string chkcanceled = "",
            int page = 0,
            int recordcount = 10,
            string init = "init")
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYRETURNSUP");
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
                var pageInfo = new PageInfo<InvF_InventoryReturnSup>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryReturnSup>()
                };
                #region["Thông tin kho xuất"]
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
                var list_Mst_Inventory = new List<Mst_Inventory>();
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
                    Ft_WhereClause = strWhere_Mst_Inventory
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory != null)
                {
                    list_Mst_Inventory = objRT_Mst_Inventory.Lst_Mst_Inventory;
                }
                ViewBag.List_Mst_Inventory = list_Mst_Inventory;
                #endregion

                #region["Thông tin nhà cung cấp"]
                var listCustomer = new List<Mst_Customer>();
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
                    listCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.List_MstCustomer = listCustomer;

                #endregion

                #region["Thông tin chi nhánh"]
                string strWhereClauseNNT = "Mst_NNT.MSTBUPattern like '" + CUtils.StrValue(UserState.Mst_NNT.MSTBUPattern) + "' and Mst_NNT.FlagActive = '1'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                ViewBag.ListMst_NNT = listMst_NNT;
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
        public ActionResult DoSearch(
            string if_invreturnsupno = "",
            string customercode = "",
            string createdtimefrom = "",
            string createdtimeto = "",
            string approvedtimefrom = "",
            string approvedtimeto = "",
            string invcodeout = "",
            string orgid = "",
            string chkpending = "",
            string chkapproved = "",
            string chkcanceled = "",
            int page = 0,
            int recordcount = 10,
            string init = "init")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            ViewBag.UserState = UserState;
            ViewBag.init = "search";
            ViewBag.PageCur = page.ToString();
            ViewBag.RecordCount = recordcount.ToString();
            var itemCount = 0;
            var pageCount = 0;
            var pageInfo = new PageInfo<InvF_InventoryReturnSup>(0, recordcount)
            {
                DataList = new List<InvF_InventoryReturnSup>()
            };
            try
            {
                #region["Search"]
                InvF_InventoryReturnSup objInvF_InventoryReturnSup = new InvF_InventoryReturnSup
                {
                    IF_InvReturnSupNo = if_invreturnsupno,
                    CustomerCode = customercode,
                    InvCodeOut = invcodeout,
                    //OrgID = orgId
                };
                var strWhere = WhereClauseInvFInventoryReturnSup(objInvF_InventoryReturnSup, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto, chkpending, chkapproved, chkcanceled, orgid);
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Ft_WhereClause = strWhere
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryReturnSup);
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
                {
                    pageInfo.DataList = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup;
                    itemCount = objRT_InvF_InventoryReturnSup.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryReturnSup.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }
                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("BindDataInvFInventoryReturnSup", pageInfo);


                #endregion

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }


        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYRETURNSUP_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            try
            {
                // Sinh số phiếu trả hàng
                Seq_Common objSeq_Common = new Seq_Common();
                objSeq_Common.Param_Postfix = "";
                objSeq_Common.Param_Prefix = "";
                objSeq_Common.SequenceType = SeqType.IFInvReturnSupNo;
                var IF_InvReturnSupNo = SeqNo(objSeq_Common);
                ViewBag.IF_InvReturnSupNo = IF_InvReturnSupNo;

                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion               

                #region Thông tin kho xuất
                var lst_Mst_Inventory = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
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
                    Ft_WhereClause = strWhere_Mst_Inventory
                };
                var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                {
                    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                }
                ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
                #endregion

                #region Thông tin NCC
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

                #region Thông tin hàng hóa
                //var lst_Mst_Product = new List<Mst_Product>();
                //var objRQ_Mst_Product = new RQ_Mst_Product()
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
                //    Rt_Cols_Mst_Product = "*",
                //    Ft_RecordStart = Ft_RecordStart,
                //    Ft_RecordCount = Ft_RecordCount,
                //    Ft_WhereClause = "Mst_Product.FlagActive = 1"
                //};
                //var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                //if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                //{
                //    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                //}                
                #endregion

                #region Lấy thông tin sản phẩm              
                //var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                //foreach (var it in lst_Mst_Product)
                //{
                //    var productUI = new Mst_ProductUI();
                //    productUI.ProductCode = it.ProductCode;
                //    productUI.ProductName = it.ProductName;
                //    productUI.UnitCode = it.UnitCode;
                //    productUI.ProductCodeBase = it.ProductCodeBase;
                //    productUI.ProductCodeRoot = it.ProductCodeRoot;
                //    productUI.ValConvert = it.ValConvert;
                //    productUI.ProductCodeUser = it.ProductCodeUser;
                //    if (it.FlagSerial.Equals("1"))
                //    {
                //        productUI.FlagSerial = "1";
                //    }
                //    if (it.FlagLot.Equals("1"))
                //    {
                //        productUI.FlagLo = "1";
                //    }
                //    if (it.ProductType != null && it.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                //    {
                //        productUI.FlagCombo = "1";
                //    }
                //    // Check theo đơn hàng hay không
                //    productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                //    //                   
                //    lst_Mst_ProductUI.Add(productUI);
                //}

                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;

                #endregion

                return View();
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        public ActionResult Save(string model)
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
                var objInvF_InventoryReturnSupSave = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryReturnSupSave>(model);
                var objInvF_InventoryReturnSup = objInvF_InventoryReturnSupSave.InvF_InventoryReturnSup;

                var flagIsDelete = objInvF_InventoryReturnSupSave.FlagIsDelete;
                objInvF_InventoryReturnSup.OrgID = orgId;
                objInvF_InventoryReturnSup.NetworkID = networkID;
                objInvF_InventoryReturnSup.OrderType = "";

                //Thêm 1 số thông tin khác
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup();
                objRQ_InvF_InventoryReturnSup.Tid = GetNextTId();
                objRQ_InvF_InventoryReturnSup.GwUserCode = GwUserCode;
                objRQ_InvF_InventoryReturnSup.GwPassword = GwPassword;
                objRQ_InvF_InventoryReturnSup.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InventoryReturnSup.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InventoryReturnSup.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InventoryReturnSup.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InventoryReturnSup.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InventoryReturnSup.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_InventoryReturnSup.InvF_InventoryReturnSup = objInvF_InventoryReturnSup;
                objRQ_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl = objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupDtl;
                objRQ_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot = objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupInstLot;
                objRQ_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial = objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupInstSerial;
                //
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryReturnSup);
                InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Save(objRQ_InvF_InventoryReturnSup, addressAPIs);
                resultEntry.Success = true;
                if (flagIsDelete == "1")
                {
                    resultEntry.AddMessage("Xóa yêu cầu trả hàng NCC thành công!");
                }
                else
                {
                    resultEntry.AddMessage("Lưu yêu cầu trả hàng NCC thành công!");
                }

                var flagRedirect = CUtils.StrValue(objInvF_InventoryReturnSupSave.FlagRedirect);
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
            return JsonViewError("Index", null, resultEntry);
        }
        public ActionResult Update(string IF_InvReturnSupNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYRETURNSUP_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            var lst_MstProductUI = new List<Mst_ProductUI>();
            ViewBag.UserState = UserState;
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";
            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;

            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            try
            {

                #region Thông tin kho xuất
                var lst_Mst_Inventory = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
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
                    Ft_WhereClause = strWhere_Mst_Inventory
                };
                var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                {
                    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                }
                ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
                #endregion

                #region Thông tin NCC
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

                InvF_InventoryReturnSup objInvFInventoryReturnSup = new InvF_InventoryReturnSup() { IF_InvReturnSupNo = IF_InvReturnSupNo };
                var strWhere = WhereClauseInvFInventoryReturnSup(objInvFInventoryReturnSup, "", "", "", "", "", "", "", "");
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Rt_Cols_InvF_InventoryReturnSupDtl = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count != 0)
                {
                    objInvFInventoryReturnSup = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup[0];
                }


                var Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
                var listProductCode_Distinct = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => CUtils.StrValue(item.ProductCode)).Distinct().ToList();
                if (listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                {
                    foreach (var item in listProductCode_Distinct)
                    {
                        var lst_Mst_ProductUICur = new List<Mst_ProductUI>();
                        var uPInv = "0";
                        var objReturnSupDtl = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                        if (objReturnSupDtl != null)
                        {
                            var productUI = new Mst_ProductUI() {
                            };
                            uPInv = objReturnSupDtl.UPInv == null ? "0" : CUtils.StrValue(objReturnSupDtl.UPInv);
                        }
                        else
                        {
                            objReturnSupDtl = new InvF_InventoryReturnSupDtl();
                        }
                        #region["Thông tin master của hàng hoá chính"]
                        var objMst_Product = new Mst_Product()
                        {
                            ProductCode = item
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

                        var objProductUI = new Mst_ProductUI();
                        objProductUI.ProductCode = objMst_ProductBase.ProductCode;
                        objProductUI.ProductCodeUser = objMst_ProductBase.ProductCodeUser;
                        objProductUI.ProductCodeBase = objMst_ProductBase.ProductCodeBase;
                        objProductUI.ProductCodeRoot = objMst_ProductBase.ProductCodeRoot;
                        objProductUI.ProductName = objMst_ProductBase.ProductName;
                        objProductUI.UnitCode = objMst_ProductBase.UnitCode;
                        objProductUI.ValConvert = objMst_ProductBase.ValConvert;
                        objProductUI.FlagLot = objMst_ProductBase.FlagLot;
                        objProductUI.FlagSerial = objMst_ProductBase.FlagSerial;
                        objProductUI.ProductType = objMst_ProductBase.ProductType;
                        
                        if (objMst_ProductBase.FlagLot.Equals("1"))
                        {
                            objProductUI.FlagLo = "1";
                        }
                        if (objMst_ProductBase.ProductType != null && objMst_ProductBase.ProductType.ToString().ToUpper().Equals("COMBO") && objMst_ProductBase.ValConvert != null)
                        {
                            objProductUI.FlagCombo = "1";
                        }
                        objProductUI.UPIn = objReturnSupDtl.UPIN == null ? 0 : objReturnSupDtl.UPIN;
                        objProductUI.UPReturnSup = objReturnSupDtl.UPReturnSup == null ? 0 : objReturnSupDtl.UPReturnSup;
                        objProductUI.UPInv = uPInv;
                        var strInvCodeOutActual = "";
                        var listDtlCur = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                        if (listDtlCur != null && listDtlCur.Count > 0)
                        {
                            var fQty = 0.0;
                            var i = 0;
                            foreach (var _it in listDtlCur)
                            {
                                fQty += CUtils.ConvertToDouble(_it.Qty);
                                objProductUI.Qty = fQty;
                                objProductUI.ValReturnSup = fQty * CUtils.ConvertToDouble(_it.UPReturnSup);
                                if (i != 0)
                                {
                                    strInvCodeOutActual += ", ";
                                }
                                strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                objProductUI.InvCodeInActual = strInvCodeOutActual;
                                objProductUI.Remark = CUtils.StrValue(_it.Remark);
                                i++;
                            }
                        }
                        //lst_MstProductUI.Add(objProductUI);








                        #endregion

                        #region["Danh sách hnagf hoá cùng base"]

                        //foreach (var it1 in lst_MstProductUI)
                        //{

                        var strWhereCungBase = "";
                        var sbSqlCungBase = new StringBuilder();
                        if (!CUtils.IsNullOrEmpty(objMst_ProductBase.ProductCodeBase))
                        {
                            sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objMst_ProductBase.ProductCodeBase), "=");
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
                            foreach (var it2 in objRT_Mst_Product1.Lst_Mst_Product)
                            {
                                var productUI = new Mst_ProductUI();
                                productUI.ProductCode = it2.ProductCode;
                                productUI.ProductCodeUser = it2.ProductCodeUser;
                                productUI.ProductCodeBase = it2.ProductCodeBase;
                                productUI.ProductCodeRoot = it2.ProductCodeRoot;
                                productUI.ProductName = it2.ProductName;
                                productUI.UnitCode = it2.UnitCode;
                                productUI.ValConvert = it2.ValConvert;
                                productUI.FlagLot = it2.FlagLot;
                                productUI.FlagSerial = it2.FlagSerial;
                                productUI.ProductType = it2.ProductType;
                                if (it2.FlagLot.Equals("1"))
                                {
                                    productUI.FlagLo = "1";
                                }
                                if (it2.ProductType != null && it2.ProductType.ToString().ToUpper().Equals("COMBO") && it2.ValConvert != null)
                                {
                                    productUI.FlagCombo = "1";
                                }
                                // Check theo đơn hàng hay không
                                //productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                                productUI.UPIn = it2.UPBuy == null ? 0 : it2.UPBuy;
                                productUI.UPReturnSup = it2.UPBuy == null ? 0 : it2.UPBuy;
                                productUI.ValReturnSup = "0";

                                #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]


                                var objInventory = lst_Mst_Inventory.SingleOrDefault(m => CUtils.StrValue(m.InvCode) == CUtils.StrValue(objInvFInventoryReturnSup.InvCodeOut));
                                string invBUPattern = "";
                                if (objInventory != null)
                                {
                                    invBUPattern = CUtils.StrValue(objInventory.InvBUPattern);
                                }

                                var strWhereClauseTonKho1 = "";
                                var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                                var sb_SQL13 = new StringBuilder();
                                if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                                {
                                    sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                                }
                                //var InvBUPattern = "I." + objInvFInventoryReturnSup.InvCodeOut + "%";
                                if (invBUPattern != null && invBUPattern != "")
                                {
                                    sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
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
                                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Inv_InventoryBalanceTonKho1);
                                var objRT_Inv_InventoryBalanceTonKho1 = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalanceTonKho1, addressAPIs);
                                var lstInv_InventoryBalanceTonKho1 = new List<Inv_InventoryBalance>();
                                if (objRT_Inv_InventoryBalanceTonKho1 != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance.Count > 0)
                                {
                                    lstInv_InventoryBalanceTonKho1 = objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance;
                                    if (lstInv_InventoryBalanceTonKho1.Count > 0)
                                    {
                                        var objInv_InventoryBalanceTonKho1 = lstInv_InventoryBalanceTonKho1.Where(_it => _it.ProductCode.Equals(productUI.ProductCode)).FirstOrDefault();
                                        if(objInv_InventoryBalanceTonKho1 != null)
                                        {
                                            productUI.UPInv = objInv_InventoryBalanceTonKho1.UPInv == null ? 0 : objInv_InventoryBalanceTonKho1.UPInv;
                                        }
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
                                        productUI.Qty = qtymax1;
                                    }
                                }
                                lst_Mst_ProductUICur.Add(productUI);
                                objProductUI.lstUnitCodeUIByProduct = lst_Mst_ProductUICur;



                                #endregion
                            }
                        }
                        //}

                        #endregion
                        lst_MstProductUI.Add(objProductUI);
                    }
                }





                ViewBag.Lst_InvF_InventoryReturnSupDtl = lst_MstProductUI;
                //
                ViewBag.Lst_InvF_InventoryReturnSupDtlChild = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
                ViewBag.Lst_InvF_InventoryReturnSupInstLot = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot;
                ViewBag.Lst_InvF_InventoryReturnSupInstSerial = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial;
                //
                return View(objRT_InvF_InventoryReturnSup);
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


        //public 



        public ActionResult Detail(string IF_InvReturnSupNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYRETURNSUP_DETAIL");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };

            var lst_MstProductUI = new List<Mst_ProductUI>();
            ViewBag.UserState = UserState;
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";
            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;

            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            try
            {
                #region Thông tin kho xuất
                var lst_Mst_Inventory = new List<Mst_Inventory>();
                var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1' and Mst_Inventory.FlagIn_Out = '1'";
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
                    Ft_WhereClause = strWhere_Mst_Inventory
                };
                var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                {
                    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                }
                ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
                #endregion

                #region Thông tin NCC
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

                InvF_InventoryReturnSup objInvFInventoryReturnSup = new InvF_InventoryReturnSup() { IF_InvReturnSupNo = IF_InvReturnSupNo };
                var strWhere = WhereClauseInvFInventoryReturnSup(objInvFInventoryReturnSup, "", "", "", "", "", "", "", "");
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Rt_Cols_InvF_InventoryReturnSupDtl = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count != 0)
                {
                    objInvFInventoryReturnSup = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup[0];
                }
                var Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
                var listProductCode_Distinct = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => CUtils.StrValue(item.ProductCode)).Distinct().ToList();
                if (listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                {
                    foreach (var item in listProductCode_Distinct)
                    {
                        var objReturnSupDtl = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                        if (objReturnSupDtl != null)
                        {
                            var productUI = new Mst_ProductUI();

                        }

                        #region["Thông tin master của hàng hoá chính"]
                        var objMst_Product = new Mst_Product()
                        {
                            ProductCode = item
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

                        var objProductUI = new Mst_ProductUI();
                        objProductUI.ProductCode = objMst_ProductBase.ProductCode;
                        objProductUI.ProductCodeUser = objMst_ProductBase.ProductCodeUser;
                        objProductUI.ProductCodeBase = objMst_ProductBase.ProductCodeBase;
                        objProductUI.ProductCodeRoot = objMst_ProductBase.ProductCodeRoot;
                        objProductUI.ProductName = objMst_ProductBase.ProductName;
                        objProductUI.UnitCode = objMst_ProductBase.UnitCode;
                        objProductUI.ValConvert = objMst_ProductBase.ValConvert;
                        objProductUI.FlagLot = objMst_ProductBase.FlagLot;
                        objProductUI.FlagSerial = objMst_ProductBase.FlagSerial;
                        objProductUI.ProductType = objMst_ProductBase.ProductType;
                        objProductUI.UPInv = objReturnSupDtl.UPInv == null ? 0 : objReturnSupDtl.UPInv;
                        //objProductUI.Remark = objMst_ProductBase.Remark;
                        if (objMst_ProductBase.FlagLot.Equals("1"))
                        {
                            objProductUI.FlagLo = "1";
                        }
                        if (objMst_ProductBase.ProductType != null && objMst_ProductBase.ProductType.ToString().ToUpper().Equals("COMBO") && objMst_ProductBase.ValConvert != null)
                        {
                            objProductUI.FlagCombo = "1";
                        }
                        objProductUI.UPIn = objReturnSupDtl.UPIN == null ? 0 : objReturnSupDtl.UPIN;
                        objProductUI.UPReturnSup = objReturnSupDtl.UPReturnSup == null ? 0 : objReturnSupDtl.UPReturnSup;

                        var strInvCodeOutActual = "";
                        var listDtlCur = Lst_InvF_InventoryReturnSupDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                        if (listDtlCur != null && listDtlCur.Count > 0)
                        {
                            var fQty = 0.0;
                            var i = 0;
                            foreach (var _it in listDtlCur)
                            {
                                fQty += CUtils.ConvertToDouble(_it.Qty);
                                objProductUI.Qty = fQty;
                                objProductUI.ValReturnSup = fQty * CUtils.ConvertToDouble(_it.UPReturnSup);
                                if (i != 0)
                                {
                                    strInvCodeOutActual += ", ";
                                }
                                strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                objProductUI.InvCodeInActual = strInvCodeOutActual;
                                objProductUI.Remark = CUtils.StrValue(_it.Remark);
                                i++;
                            }
                        }
                        //lst_MstProductUI.Add(objProductUI);








                        #endregion

                        #region["Danh sách hnagf hoá cùng base"]

                        //foreach (var it1 in lst_MstProductUI)
                        //{

                        var strWhereCungBase = "";
                        var sbSqlCungBase = new StringBuilder();
                        if (!CUtils.IsNullOrEmpty(objMst_ProductBase.ProductCodeBase))
                        {
                            sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objMst_ProductBase.ProductCodeBase), "=");
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
                            foreach (var it2 in objRT_Mst_Product1.Lst_Mst_Product)
                            {
                                var productUI = new Mst_ProductUI();
                                productUI.ProductCode = it2.ProductCode;
                                productUI.ProductCodeUser = it2.ProductCodeUser;
                                productUI.ProductCodeBase = it2.ProductCodeBase;
                                productUI.ProductCodeRoot = it2.ProductCodeRoot;
                                productUI.ProductName = it2.ProductName;
                                productUI.UnitCode = it2.UnitCode;
                                productUI.ValConvert = it2.ValConvert;
                                productUI.FlagLot = it2.FlagLot;
                                productUI.FlagSerial = it2.FlagSerial;
                                productUI.ProductType = it2.ProductType;
                                if (it2.FlagLot.Equals("1"))
                                {
                                    productUI.FlagLo = "1";
                                }
                                if (it2.ProductType != null && it2.ProductType.ToString().ToUpper().Equals("COMBO") && it2.ValConvert != null)
                                {
                                    productUI.FlagCombo = "1";
                                }
                                // Check theo đơn hàng hay không
                                //productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                                productUI.UPIn = it2.UPBuy == null ? 0 : it2.UPBuy;
                                productUI.UPReturnSup = it2.UPBuy == null ? 0 : it2.UPBuy;
                                productUI.ValReturnSup = "0";

                                #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]


                                var objInventory = lst_Mst_Inventory.SingleOrDefault(m => CUtils.StrValue(m.InvCode) == CUtils.StrValue(objInvFInventoryReturnSup.InvCodeOut));
                                string invBUPattern = "";
                                if (objInventory != null)
                                {
                                    invBUPattern = CUtils.StrValue(objInventory.InvBUPattern);
                                }
                                var strWhereClauseTonKho1 = "";
                                var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                                var sb_SQL13 = new StringBuilder();
                                if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                                {
                                    sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                                }
                                //var InvBUPattern = "I." + objInvFInventoryReturnSup.InvCodeOut + "%";
                                if (invBUPattern != null && invBUPattern != "")
                                {
                                    sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
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
                                        productUI.Qty = qtymax1;
                                    }
                                }
                                lst_Mst_ProductUI.Add(productUI);
                                objProductUI.lstUnitCodeUIByProduct = lst_Mst_ProductUI;



                                #endregion
                            }
                        }
                        //}

                        #endregion
                        lst_MstProductUI.Add(objProductUI);
                    }
                }





                ViewBag.Lst_InvF_InventoryReturnSupDtl = lst_MstProductUI;
                //
                ViewBag.Lst_InvF_InventoryReturnSupDtlChild = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
                ViewBag.Lst_InvF_InventoryReturnSupInstLot = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot;
                ViewBag.Lst_InvF_InventoryReturnSupInstSerial = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial;
                return View(objRT_InvF_InventoryReturnSup);
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
        public string WhereClauseInvFInventoryReturnSup(InvF_InventoryReturnSup objInvF_InventoryReturnSup, string strdatefrom, string strdateto, string strApprdatefrom, string strApprdateto, string chkpending, string chkapproved, string chkcanceled, string orgID)
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
            var Tbl_InvF_InventoryReturnSup = TableName.InvF_InventoryReturnSup + ".";
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSup.IF_InvReturnSupNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StrValue(objInvF_InventoryReturnSup.IF_InvReturnSupNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(datefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(dateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdatefrom))
            {
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(apprdatefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdateto))
            {
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(apprdateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.ApprDTimeUTC, strApprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSup.CustomerCode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.CustomerCode, CUtils.StrValue(objInvF_InventoryReturnSup.CustomerCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSup.InvCodeOut))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.InvCodeOut, CUtils.StrValue(objInvF_InventoryReturnSup.InvCodeOut), "=");
            }
            if (!CUtils.IsNullOrEmpty(orgID))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.OrgID, CUtils.StrValue(orgID), "=");
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
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.IF_ReturnSupStatus, status, "in");
            }

            strWhere = sbSql.ToString();
            return strWhere;
        }
        public ActionResult Approve1(string IF_InvReturnSupNo)
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
                var objInvF_InventoryReturnSup = new InvF_InventoryReturnSup()
                {
                    IF_InvReturnSupNo = IF_InvReturnSupNo
                };
                objInvF_InventoryReturnSup.OrgID = orgId;
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    InvF_InventoryReturnSup = objInvF_InventoryReturnSup
                };
                InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Appr(objRQ_InvF_InventoryReturnSup, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt YC trả hàng thành công!");
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

        #region ["Approve"]
        [HttpPost]
        //public ActionResult Approve(string objlistinvf_inventoryreturnsup)
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    #region ["UserState Common Info"]            
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    try
        //    {
        //        List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);

        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
        //            InvF_InventoryReturnSup = null
        //        };

        //        foreach (var item in listInvF_InventoryReturnSup)
        //        {
        //            objRQ_InvF_InventoryReturnSup.Tid = GetNextTId();
        //            objRQ_InvF_InventoryReturnSup.InvF_InventoryReturnSup = new InvF_InventoryReturnSup()
        //            {
        //                IF_InvReturnSupNo = item.IF_InvReturnSupNo,
        //                Remark = item.Remark
        //            };

        //            InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Appr(objRQ_InvF_InventoryReturnSup, addressAPIs);
        //        }

        //        resultEntry.Success = true;
        //        resultEntry.AddMessage("Duyệt phiếu trả hàng thành công!");
        //        resultEntry.RedirectUrl = Url.Action("Index");
        //        return Json(resultEntry);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return JsonViewError("Index", null, resultEntry);
        //}
        public ActionResult Approve(string objlistinvf_inventoryreturnsup)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYRETURNSUP_APPROVE");
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
                List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    InvF_InventoryReturnSup = null
                };


                foreach (var item in listInvF_InventoryReturnSup)
                {
                    objRQ_InvF_InventoryReturnSup.Tid = GetNextTId();
                    objRQ_InvF_InventoryReturnSup.InvF_InventoryReturnSup = new InvF_InventoryReturnSup()
                    {
                        IF_InvReturnSupNo = item.IF_InvReturnSupNo,
                        Remark = item.Remark
                    };
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryReturnSup);
                    InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Appr(objRQ_InvF_InventoryReturnSup, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt phiếu trả hàng thành công!");
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
        //public ActionResult Cancel(string objlistinvf_inventoryreturnsup)
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    #region ["UserState Common Info"]            
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    try
        //    {
        //        List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);

        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
        //            InvF_InventoryReturnSup = null
        //        };

        //        foreach (var item in listInvF_InventoryReturnSup)
        //        {
        //            objRQ_InvF_InventoryReturnSup.Tid = GetNextTId();
        //            objRQ_InvF_InventoryReturnSup.InvF_InventoryReturnSup = new InvF_InventoryReturnSup()
        //            {
        //                IF_InvReturnSupNo = item.IF_InvReturnSupNo,
        //                Remark = item.Remark
        //            };

        //            InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Cancel(objRQ_InvF_InventoryReturnSup, addressAPIs);
        //        }

        //        resultEntry.Success = true;
        //        resultEntry.AddMessage("Hủy phiếu trả hàng thành công!");
        //        resultEntry.RedirectUrl = Url.Action("Index");
        //        return Json(resultEntry);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return JsonViewError("Index", null, resultEntry);
        //}
        public ActionResult Cancel(string objlistinvf_inventoryreturnsup)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYRETURNSUP_CANCEL");
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
                List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    InvF_InventoryReturnSup = null
                };

                foreach (var item in listInvF_InventoryReturnSup)
                {
                    objRQ_InvF_InventoryReturnSup.Tid = GetNextTId();
                    objRQ_InvF_InventoryReturnSup.InvF_InventoryReturnSup = new InvF_InventoryReturnSup()
                    {
                        IF_InvReturnSupNo = item.IF_InvReturnSupNo,
                        Remark = item.Remark
                    };
                    InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Cancel(objRQ_InvF_InventoryReturnSup, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Hủy phiếu trả hàng thành công!");
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
        //public ActionResult Delete(string objlistinvf_inventoryreturnsup)
        //{
        //    #region ["UserState Common Info"]
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion

        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    try
        //    {
        //        List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);
        //        var listIF_InvReturnSupNo = listInvF_InventoryReturnSup.Select(m => m.IF_InvReturnSupNo.ToString()).ToList();

        //        var strWhereInvF_InventoryReturnSup = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryReturnSup, TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StretchListString(listIF_InvReturnSupNo));

        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
        //        {
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
        //            Ft_WhereClause = strWhereInvF_InventoryReturnSup,
        //            Ft_Cols_Upd = null,
        //            // RQ_Mst_Customer
        //            Rt_Cols_InvF_InventoryReturnSup = "*",
        //            Rt_Cols_InvF_InventoryReturnSupDtl = "*",
        //            Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
        //            Rt_Cols_InvF_InventoryReturnSupInstSerial = "*"
        //        };
        //        var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
        //        if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
        //        {
        //            foreach (var objInvF_InventoryReturnSup in objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup)
        //            {
        //                RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSupDelete = new RQ_InvF_InventoryReturnSup()
        //                {
        //                    // WARQBase
        //                    Tid = GetNextTId(),
        //                    GwUserCode = GwUserCode,
        //                    GwPassword = GwPassword,
        //                    AccessToken = CUtils.StrValue(UserState.AccessToken),
        //                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
        //                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
        //                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
        //                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
        //                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
        //                    InvF_InventoryReturnSup = objInvF_InventoryReturnSup,
        //                    Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
        //                    Lst_InvF_InventoryReturnSupInstLot = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
        //                    Lst_InvF_InventoryReturnSupInstSerial = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
        //                    FlagIsDelete = "1",
        //                    FlagIsCheckTotal = "1"
        //                };
        //                InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Save(objRQ_InvF_InventoryReturnSupDelete, addressAPIs);
        //            }

        //        }
        //        resultEntry.Success = true;
        //        resultEntry.AddMessage("Xoá phiếu trả hàng thành công!");
        //        resultEntry.RedirectUrl = Url.Action("Index");
        //        return Json(resultEntry);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return JsonViewError("Index", null, resultEntry);
        //}
        public ActionResult Delete(string objlistinvf_inventoryreturnsup)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVF_INVENTORYRETURNSUP_DELETE");
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
                List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistinvf_inventoryreturnsup);
                var listIF_InvReturnSupNo = listInvF_InventoryReturnSup.Select(m => m.IF_InvReturnSupNo.ToString()).ToList();

                var strWhereInvF_InventoryReturnSup = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryReturnSup, TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StretchListString(listIF_InvReturnSupNo));

                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Ft_WhereClause = strWhereInvF_InventoryReturnSup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Rt_Cols_InvF_InventoryReturnSupDtl = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstSerial = "*"
                };
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
                {
                    foreach (var objInvF_InventoryReturnSup in objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup)
                    {
                        RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSupDelete = new RQ_InvF_InventoryReturnSup()
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
                            InvF_InventoryReturnSup = objInvF_InventoryReturnSup,
                            Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
                            Lst_InvF_InventoryReturnSupInstLot = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
                            Lst_InvF_InventoryReturnSupInstSerial = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial.Where(m => m.IF_InvReturnSupNo.ToString() == objInvF_InventoryReturnSup.IF_InvReturnSupNo.ToString()).ToList(),
                            FlagIsDelete = "1",
                            FlagIsCheckTotal = "1"
                        };
                        InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Save(objRQ_InvF_InventoryReturnSupDelete, addressAPIs);
                    }
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá phiếu trả hàng thành công!");
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

        #region Lô, Serial, Vị trí xuất
        [HttpPost]
        public ActionResult Lo(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string productName, string valConvert, string listLot, string ViewType)
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

                #region //Tính tồn kho theo ĐV quy đổi

                if (lstInv_InventoryBalanceLot.Count > 0)
                {
                    double dValConvert = 1;
                    if (!string.IsNullOrEmpty(valConvert))
                    {
                        dValConvert = Convert.ToDouble(valConvert);
                    }
                    foreach (var invBalLot in lstInv_InventoryBalanceLot)
                    {
                        invBalLot.QtyTotalOK = Math.Round(Convert.ToDouble(invBalLot.QtyTotalOK) / dValConvert, 2);
                    }
                }

                #endregion

                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                //
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
                ViewBag.InvBUPattern = invBUPattern;

                ViewBag.ValConvert = valConvert;

                List<InvF_InventoryReturnSupInstLot> lst_InvF_InventoryReturnSupInstLot = new List<InvF_InventoryReturnSupInstLot>();
                if (listLot != null && listLot.Length > 0)
                {
                    lst_InvF_InventoryReturnSupInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSupInstLot>>(listLot);
                }

                var listLotBalanceCur = new List<Inv_InventoryBalanceLot>();
                foreach (var item in lst_InvF_InventoryReturnSupInstLot)
                {
                    Inv_InventoryBalanceLot itNew = new Inv_InventoryBalanceLot();
                    itNew.ProductCode = item.ProductCode;
                    itNew.ProductLotNo = item.ProductLotNo;
                    itNew.QtyAvailOK = item.Qty;

                    var itBalanceLot = lstInv_InventoryBalanceLot.SingleOrDefault(m => CUtils.StrValue(m.ProductLotNo) == CUtils.StrValue(item.ProductLotNo).ToUpper() && CUtils.StrValue(m.InvCode) == CUtils.StrValue(item.InvCodeOutActual));
                    if (itBalanceLot != null)
                    {
                        itNew.ProductionDate = itBalanceLot.ProductionDate;
                        itNew.ExpiredDate = itBalanceLot.ExpiredDate;
                        itNew.InvCode = itBalanceLot.InvCode;
                        itNew.QtyTotalOK = itBalanceLot.QtyTotalOK;
                    }

                    listLotBalanceCur.Add(itNew);
                }

                ViewBag.ListLot = listLotBalanceCur;
                ViewBag.ViewType = ViewType;

                return JsonView("Lo"/*, lstInv_InventoryBalanceLot*/);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }


        #region["Tìm kiếm số lô"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchLo(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string valConvert, string productLotNo)
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
                if (!CUtils.IsNullOrEmpty(productLotNo))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductLotNo", "%" + CUtils.StrValue(productLotNo) + "%", "like");
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
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                var rt = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq, addressAPIs);
                if (rt != null && rt.Lst_Inv_InventoryBalanceLot != null)
                {
                    lstInv_InventoryBalanceLot = rt.Lst_Inv_InventoryBalanceLot;
                }

                #region //Tính tồn kho theo ĐV quy đổi

                if (lstInv_InventoryBalanceLot.Count > 0)
                {
                    double dValConvert = 1;
                    if (!string.IsNullOrEmpty(valConvert))
                    {
                        dValConvert = Convert.ToDouble(valConvert);
                    }
                    foreach (var invBalLot in lstInv_InventoryBalanceLot)
                    {
                        invBalLot.QtyTotalOK = Math.Round(Convert.ToDouble(invBalLot.QtyTotalOK) / dValConvert, 2);
                    }
                }

                #endregion

                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                //
                ViewBag.ProductCodeUser = productCodeUser;
                //ViewBag.ProductName = productName;
                ViewBag.InvBUPattern = invBUPattern;

                ViewBag.ValConvert = valConvert;

               

                return Json(new { Success = true, LstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot });
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


        #endregion

        [HttpPost]
        public ActionResult Serial(string productCode, string productCodeBase, string invBUPattern, string type, string productCodeUser, string productName, string listSerial, string ViewType)
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
                ViewBag.type = type;

                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.InvBUPattern = invBUPattern;


                List<InvF_InventoryReturnSupInstSerial> lst_InvF_InventoryReturnSupInstSerial = new List<InvF_InventoryReturnSupInstSerial>();
                if (listSerial != null && listSerial.Length > 0)
                {
                    lst_InvF_InventoryReturnSupInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSupInstSerial>>(listSerial);
                }
                ViewBag.ListSerial = lst_InvF_InventoryReturnSupInstSerial;
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


        #region["Tìm số serial popup"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchSerial(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string serialNo)
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
                if (!CUtils.IsNullOrEmpty(serialNo))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "SerialNo", "%" + CUtils.StrValue(serialNo) + "%", "like");
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

                ViewBag.ProductCodeBase = productCodeBase;
                ViewBag.InvBUPattern = invBUPattern;

                return Json(new { Success = true, LstInv_InventoryBalanceSerial = lstInv_InventoryBalanceSerial });
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
        #endregion


        [HttpPost]
        public ActionResult GetTonKho(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string listdetail, string ViewType)
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
        public ActionResult LoDtl(string listLot)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryReturnSupInstLot = new List<InvF_InventoryReturnSupInstLot>();
                List<InvF_InventoryReturnSupInstLot> lst_InvF_InventoryReturnSupInstLot = new List<InvF_InventoryReturnSupInstLot>();
                if (listLot != null && listLot.Length > 0)
                {
                    lst_InvF_InventoryReturnSupInstLot = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSupInstLot>>(listLot);

                    var listInvCodeActual_Distinct = lst_InvF_InventoryReturnSupInstLot.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual)).Select(item => CUtils.StrValue(item.InvCodeOutActual)).Distinct().ToList();


                    foreach (var invCodeOutActual in listInvCodeActual_Distinct)
                    {
                        var objLot = lst_InvF_InventoryReturnSupInstLot.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual) && CUtils.StrValue(it.InvCodeOutActual).Equals(invCodeOutActual)).FirstOrDefault();

                        if (objLot != null)
                        {
                            var fQty = 0.0;
                            var strInvCodeOutActual = "";
                            var objInvF_InventoryReturnSupInstLot = new InvF_InventoryReturnSupInstLot()
                            {
                                ProductCode = CUtils.StrValue(objLot.ProductCode),
                                ProductLotNo = CUtils.StrValue(objLot.ProductLotNo),
                                Qty = fQty,
                                InvCodeOutActual = invCodeOutActual
                            };


                            var listDtlLotCur = lst_InvF_InventoryReturnSupInstLot.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual) && CUtils.StrValue(it.InvCodeOutActual).Equals(invCodeOutActual)).ToList();
                            if (listDtlLotCur != null && listDtlLotCur.Count > 0)
                            {
                                var i = 0;
                                foreach (var _it in listDtlLotCur)
                                {
                                    //var objProduct_InvCodeInActual = new InvF_InventoryReturnSupInstLot()
                                    //{
                                    //    ProductCode = CUtils.StrValue(objLot.ProductCode),

                                    //}

                                    fQty += CUtils.ConvertToDouble(_it.Qty);
                                    objInvF_InventoryReturnSupInstLot.Qty = fQty;

                                }
                            }
                            listInvF_InventoryReturnSupInstLot.Add(objInvF_InventoryReturnSupInstLot);
                        }

                    }

                }
                return Json(new { Success = true, ListInvF_InventoryReturnSupInstLot = listInvF_InventoryReturnSupInstLot });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }


        [HttpPost]
        public ActionResult SerialDtl(string listSerial)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryReturnSupInstSerial = new List<InvF_InventoryReturnSupInstSerial>();
                List<InvF_InventoryReturnSupInstSerial> lst_InvF_InventoryReturnSupInstSerial = new List<InvF_InventoryReturnSupInstSerial>();
                if (listSerial != null && listSerial.Length > 0)
                {
                    lst_InvF_InventoryReturnSupInstSerial = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSupInstSerial>>(listSerial);
                    var listInvCodeActual_Distinct = lst_InvF_InventoryReturnSupInstSerial.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual)).Select(item => CUtils.StrValue(item.InvCodeOutActual)).Distinct().ToList();
                    foreach (var invCodeOutActual in listInvCodeActual_Distinct)
                    {
                        var objSerial = lst_InvF_InventoryReturnSupInstSerial.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual) && CUtils.StrValue(it.InvCodeOutActual).Equals(invCodeOutActual)).FirstOrDefault();
                        if (objSerial != null)
                        {
                            var fQty = 0.0;
                            var objInvF_InventoryReturnSupInstLot = new InvF_InventoryReturnSupInstSerialUI()
                            {
                                ProductCode = objSerial.ProductCode,
                                InvCodeOutActual = invCodeOutActual,
                                Qty = fQty
                            };


                            var listDtlSerialCur = lst_InvF_InventoryReturnSupInstSerial.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual) && CUtils.StrValue(it.InvCodeOutActual).Equals(invCodeOutActual)).ToList();
                            if (listDtlSerialCur != null && listDtlSerialCur.Count > 0)
                            {
                                //var i = 0;
                                //foreach(var _it in listDtlSerialCur)
                                //{
                                //    fQty += i;
                                //    objInvF_InventoryReturnSupInstLot.Qty = fQty;
                                //    i++;
                                //}
                                fQty = listDtlSerialCur.Count;
                                objInvF_InventoryReturnSupInstLot.Qty = fQty;

                            }
                            listInvF_InventoryReturnSupInstSerial.Add(objInvF_InventoryReturnSupInstLot);
                        }
                    }

                }
                return Json(new { Success = true, ListInvF_InventoryReturnSupInstSerial = listInvF_InventoryReturnSupInstSerial });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }


        [HttpPost]
        public ActionResult Get_TonKho_PhanBo(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string listdetail)
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
                var data = new
                {
                    Success = true,
                    ListInventoryBalance = lstInv_InventoryBalance
                };
                return Json(data, JsonRequestBehavior.AllowGet);
                //return JsonView("ShowTonKho", lstInv_InventoryBalance);
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
        #endregion

        #region["Combo"]
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

            var listPrd_BOM = new List<Prd_BOM>();
            var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
            #endregion

            try
            {
                var objPrd_Bom = new Prd_BOM()
                {
                    ProductCode = productCode
                };

                var str_WhereClause_Combo = strWhereClause_Combo(objPrd_Bom);

                var objRQPrd_Bom = new RQ_Mst_Product()
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
                    Ft_WhereClause = str_WhereClause_Combo,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Rt_Cols_Mst_Product = "*",
                    //Rt_Cols_Mst_ProductFiles = "*",
                    //Rt_Cols_Mst_ProductImages = "*",
                    //Rt_Cols_Prd_Attribute = "*",
                    Rt_Cols_Prd_BOM = "*"
                };
                var objRTPrd_Bom = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQPrd_Bom, addressAPIs);
                if(objRTPrd_Bom != null && objRTPrd_Bom.Lst_Prd_BOM != null && objRTPrd_Bom.Lst_Prd_BOM.Count > 0)
                {
                    listPrd_BOM.AddRange(objRTPrd_Bom.Lst_Prd_BOM);
                }


                #region["Thông tin tồn kho"]
                var list_StrProductCode = new List<string>();
                foreach(var item in listPrd_BOM)
                {
                    list_StrProductCode.Add(item.ProductCode.ToString());
                }
                var lstProductCode = "";
                if (list_StrProductCode != null && list_StrProductCode.Count > 0){
                    foreach(var it in list_StrProductCode)
                    {
                        if (lstProductCode == "")
                        {
                            lstProductCode += it;
                        }
                        else
                        {
                            lstProductCode += "," + it;
                        }
                    }
                }


                var objMst_ProductUI = new Mst_ProductUI()
                {
                    ProductCode = lstProductCode,
                    InvBUPattern = invBUPattern
                };
                var str_WhereClause_Invf_InventoryBalance = strWhereClause_Invf_InventoryBalance(objMst_ProductUI);
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
                    Ft_WhereClause = str_WhereClause_Invf_InventoryBalance
                };


                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count != 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                }

                #endregion


                List<Prd_BOMUI> lstPrd_BOMUI = new List<Prd_BOMUI>();

                foreach(var item in listPrd_BOM)
                {
                    Prd_BOMUI objPrd_BOMUI = new Prd_BOMUI();
                    objPrd_BOMUI.ProductCode = item.ProductCode;
                    objPrd_BOMUI.mp_ProductName = item.mp_ProductName;
                    objPrd_BOMUI.mp_UnitCode = item.mp_UnitCode;
                    objPrd_BOMUI.mp_UPSell = item.mp_UPSell;
                    objPrd_BOMUI.Qty = item.Qty;
                    objPrd_BOMUI.mp_ProductCodeUser = item.mp_ProductCodeUser;

                    if(lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                    {
                        var lstBalanceByProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode)).ToList();
                        if(lstBalanceByProduct != null && lstBalanceByProduct.Count > 0)
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
                            lstTonKhoVT.Add(tk);
                        }

                        objPrd_BOMUI.lstTonKhoVT = lstTonKhoVT;
                        ViewBag.ProductCode = productCode;
                        lstPrd_BOMUI.Add(objPrd_BOMUI);
                    }

                }

                ViewBag.qtyCombo = qtyCombo;
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
                ViewBag.ProductCode = productCode;

                return JsonView("Combo", lstPrd_BOMUI);


            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Combo", null, resultEntry);

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

            List<Mst_Product> listProduct = new List<Mst_Product>();
            listProduct.Add(objMst_ProductBase);

            return Json(new { Data = listProduct });
        }
        [HttpPost]
        public ActionResult SearchProduct(string prdid = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.NetworkID);
            var orgID = CUtils.StrValueOrNull(UserState.OrgID);
            var utcOffset = CUtils.StrValueOrNull(UserState.UtcOffset);
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
                //return Json(listMst_InvoiceType);
                return Json(new { Success = true, data = listMst_InvoiceType });
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #region["Lay thong tin san pham autocomplete lam lai"]

        [HttpPost]
        public ActionResult GetProductExactly1(string productCode, string productCodeBase, string productCodeRoot, string InvBUPattern, string valconvert, string FlagLot, string FlagSerial)
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
            decimal upinv = 0;
            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var Tbl_Mst_Product = "Mst_Product.";
            #region["Thoong tin chung"]
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
                #region["Lay thong tin master chinh xac cua hang hoa"]

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

                #region["Lay thong tin ton kho lon nhat cua hnag hoa duoc chon"]

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


                #region["Danh sach hang hoa cung base"]
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
                            productUI.UPIn = it.UPBuy == null ? 0 : it.UPBuy;
                            productUI.UPReturnSup = it.UPBuy == null ? 0 : it.UPBuy;
                            productUI.Remark = it.Remark;
                            productUI.ValReturnSup = "0";
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
                                    if (objInv_InventoryBalance_ByProductCode != null)
                                    {
                                        if (!CUtils.IsNullOrEmpty(objInv_InventoryBalance_ByProductCode.UPInv))
                                        {
                                            upinv = Convert.ToDecimal(objInv_InventoryBalance_ByProductCode.UPInv);
                                        }

                                    }
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
                                    productUI.Qty = qtymax1;
                                    productUI.UPOutDesc = upinv;
                                    productUI.UPInv = upinv;
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
                else if (CUtils.StrValue(FlagLot).Equals("1"))
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
        #endregion

        [HttpPost]
        public ActionResult Lo1(string productCodeBase, string invBUPattern, string productCode, string productCodeUser, string productName, string qty, string listLot)
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
                ViewBag.productCodeBase = productCodeBase;
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
                        Inv_InventoryBalanceLot lotCur = lstInv_InventoryBalanceLot.Where(m => CUtils.StrValue(m.ProductLotNo).Equals(lot.ProductLotNo) && CUtils.StrValue(m.InvCode).Equals(lot.InvCode)).FirstOrDefault();
                        lot.InvCode = lotCur.InvCode;
                        lot.ProductionDate = lotCur.ProductionDate;
                        lot.ExpiredDate = lotCur.ExpiredDate;
                        lot.QtyTotalOK = lotCur.QtyTotalOK;
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

                return JsonView("Lo");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
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

            var listProduct = new List<Mst_Product>();
            listProduct.Add(objMst_ProductBase);

            return Json(new { Success = true, ListProduct = listProduct });
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
            var pageInfo = new PageInfo<Mst_ProductUI>(0, recordcount)
            {
                DataList = new List<Mst_ProductUI>()
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

            var listProductUI = new List<Mst_ProductUI>();

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
                    var itemUI = new Mst_ProductUI()
                    {
                        ProductCode = itPrd.ProductCode,
                        ProductCodeRoot = itPrd.ProductCodeRoot,
                        ProductCodeBase = itPrd.ProductCodeBase,
                        ProductCodeUser = itPrd.ProductCodeUser,
                        ProductName = itPrd.ProductName,
                        UnitCode = itPrd.UnitCode,
                        UPBuy = itPrd.UPBuy,
                        FlagLot = itPrd.FlagLot,
                        FlagSerial = itPrd.FlagSerial,
                        ValConvert = itPrd.ValConvert
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
                    listProductUI.Add(itemUI);

                    lstPrdCodeBase.Remove(itPrd.ProductCodeBase.ToString());
                }
            }
            #endregion

            pageInfo.DataList = listProductUI;
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

        #region Get hàng hóa theo phiếu nhập

        public ActionResult SearchProductInInvIn(string customercode, string if_invinno, string productcode, string productname, int recordcount = 10, int page = 0)
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
                string strWhereClause = strWhereClause_InvF_InventoryIn(if_invinno, customercode, productcode, productname);
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

        public ActionResult SearchProductInInvInExactly(string if_invinno)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion            

            try
            {
                var listDtl = new List<InvF_InventoryInDtl>();
                string strWhereClause = strWhereClause_InvF_InventoryIn(if_invinno, "", "", "");
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
                    Ft_WhereClause = strWhereClause,
                    Rt_Cols_InvF_InventoryIn = "*",
                    Rt_Cols_InvF_InventoryInDtl = "*"
                };

                var objRT_InvF_InventoryIn = InvF_InventoryInService.Instance.WA_InvF_InventoryIn_Get(objRQ_InvF_InventoryIn, addressAPIs);
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl != null)
                {
                    listDtl = objRT_InvF_InventoryIn.Lst_InvF_InventoryInDtl;
                }
                string customerCodeSys = "";
                if (objRT_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn != null && objRT_InvF_InventoryIn.Lst_InvF_InventoryIn.Count > 0)
                {
                    customerCodeSys = CUtils.StrValue(objRT_InvF_InventoryIn.Lst_InvF_InventoryIn[0].CustomerCode);
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

                ViewBag.CustomerCodeSys = customerCodeSys;
                return JsonView("showOptionProductInInvIn", listDtlDistinct);
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

        #region["Export Excel"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Export(string objlistselected)
        //{
        //    #region ["UserState Common Info"]            
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    var resultEntry = new JsonResultEntry() { Success = false };

        //    string url = "";

        //    var ListInvF_InventoryReturnSup = new List<InvF_InventoryReturnSup>();
        //    var ListInvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSupDtl>();
        //    var ListInvF_InventoryReturnSupInstLot = new List<InvF_InventoryReturnSupInstLot>();
        //    var ListInvF_InventoryReturnSupInstSerial = new List<InvF_InventoryReturnSupInstSerial>();

        //    try
        //    {
        //        #region["Search"]
        //        List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistselected);
        //        var listIF_InvReturnSupNo = listInvF_InventoryReturnSup.Select(m => m.IF_InvReturnSupNo.ToString()).ToList();

        //        var strWhereInvF_InventoryReturnSup = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryReturnSup, TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StretchListString(listIF_InvReturnSupNo));

        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
        //        {
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
        //            Ft_WhereClause = strWhereInvF_InventoryReturnSup,
        //            Ft_Cols_Upd = null,
        //            // RQ_InvF_InventoryReturnSup
        //            Rt_Cols_InvF_InventoryReturnSup = "*",
        //            Rt_Cols_InvF_InventoryReturnSupDtl = "*",
        //            Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
        //            Rt_Cols_InvF_InventoryReturnSupInstSerial = "*",
        //        };
        //        var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
        //        if (objRT_InvF_InventoryReturnSup != null)
        //        {
        //            if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
        //            {
        //                ListInvF_InventoryReturnSup.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup);
        //            }
        //            if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl.Count > 0)
        //            {
        //                ListInvF_InventoryReturnSupDtl.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl);
        //            }

        //            if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot.Count > 0)
        //            {
        //                ListInvF_InventoryReturnSupInstLot.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot);
        //            }
        //            if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial.Count > 0)
        //            {
        //                ListInvF_InventoryReturnSupInstSerial.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial);
        //            }

        //            #region Tìm ProductCodeUser cho Lot, Serial

        //            foreach(var itLot in ListInvF_InventoryReturnSupInstLot)
        //            {
        //                var lstDtl = ListInvF_InventoryReturnSupDtl.Where(m => CUtils.StrValue(m.ProductCode) == CUtils.StrValue(itLot.ProductCode)).ToList();
        //                if(lstDtl.Count > 0)
        //                {
        //                    itLot.ProductCode = lstDtl[0].mp_ProductCodeUser;
        //                }
        //            }

        //            foreach (var itSerial in ListInvF_InventoryReturnSupInstSerial)
        //            {
        //                var lstDtl = ListInvF_InventoryReturnSupDtl.Where(m => CUtils.StrValue(m.ProductCode) == CUtils.StrValue(itSerial.ProductCode)).ToList();
        //                if (lstDtl.Count > 0)
        //                {
        //                    itSerial.ProductCode = lstDtl[0].mp_ProductCodeUser;
        //                }
        //            }

        //            #endregion

        //            Dictionary<string, string> dicColNamesMst = GetExportDicColumsMaster();
        //            Dictionary<string, string> dicColNamesDtl = GetExportDicColumsDtl();
        //            Dictionary<string, string> dicColNamesLot = GetExportDicColumsLot();
        //            Dictionary<string, string> dicColNamesSerial = GetExportDicColumsSerial();

        //            string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryReturnSup).Name), ref url);
        //            var ds = new DataSet();
        //            var table = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSup, dicColNamesMst);
        //            var tableDtl = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupDtl, dicColNamesDtl);
        //            var tableLot = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupInstLot, dicColNamesLot);
        //            var tableSerial = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupInstSerial, dicColNamesSerial);

        //            if (table != null && table.Rows.Count > 0)
        //            {
        //                table.TableName = "InvF_InventoryReturnSup";
        //                ds.Tables.Add(table);
        //            }
        //            if (tableDtl != null && tableDtl.Rows.Count > 0)
        //            {
        //                tableDtl.TableName = "InvF_InventoryReturnSupDtl";
        //                ds.Tables.Add(tableDtl);
        //            }
        //            if (tableLot != null && tableLot.Rows.Count > 0)
        //            {
        //                tableLot.TableName = "InvF_InventoryReturnSupInstLot";
        //                ds.Tables.Add(tableLot);
        //            }
        //            if (tableSerial != null && tableSerial.Rows.Count > 0)
        //            {
        //                tableSerial.TableName = "InvF_InventoryReturnSupInstSerial";
        //                ds.Tables.Add(tableSerial);
        //            }                    

        //            ExcelExport.ExportToExcelMultiSheet(ds, filePath);

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
        public ActionResult Export(string objlistselected)
        {
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            string url = "";
            var ListInvF_InventoryReturnSup = new List<InvF_InventoryReturnSup>();
            var ListInvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSupDtl>();
            var ListInvF_InventoryReturnSupInstLot = new List<InvF_InventoryReturnSupInstLot>();
            var ListInvF_InventoryReturnSupInstSerial = new List<InvF_InventoryReturnSupInstSerial>();

            try
            {
                #region["Search"]
                List<InvF_InventoryReturnSup> listInvF_InventoryReturnSup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryReturnSup>>(objlistselected);
                var listIF_InvReturnSupNo = listInvF_InventoryReturnSup.Select(m => m.IF_InvReturnSupNo.ToString()).ToList();
                var strWhereInvF_InventoryReturnSup = string.Format("{0}.{1} in '{2}'", TableName.InvF_InventoryReturnSup, TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StretchListString(listIF_InvReturnSupNo));

                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Ft_WhereClause = strWhereInvF_InventoryReturnSup,
                    Ft_Cols_Upd = null,
                    // RQ_InvF_InventoryReturnSup
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Rt_Cols_InvF_InventoryReturnSupDtl = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstLot = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstSerial = "*",
                };
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null)
                {
                    if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
                    {
                        ListInvF_InventoryReturnSup.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup);
                    }
                    if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl.Count > 0)
                    {
                        ListInvF_InventoryReturnSupDtl.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl);
                    }
                    if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot.Count > 0)
                    {
                        ListInvF_InventoryReturnSupInstLot.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstLot);
                    }
                    if (objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial.Count > 0)
                    {
                        ListInvF_InventoryReturnSupInstSerial.AddRange(objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupInstSerial);
                    }
                    #region["Tìm ProductCodeUser cho Lot, Serial"]
                    foreach (var itLot in ListInvF_InventoryReturnSupInstLot)
                    {
                        var lstDtl = ListInvF_InventoryReturnSupDtl.Where(m => CUtils.StrValue(m.ProductCode) == CUtils.StrValue(itLot.ProductCode)).ToList();
                        if (lstDtl != null && lstDtl.Count > 0)
                        {
                            itLot.ProductCode = lstDtl[0].mp_ProductCodeUser;
                        }
                    }

                    foreach (var itSerial in ListInvF_InventoryReturnSupInstSerial)
                    {
                        var lstDtl = ListInvF_InventoryReturnSupDtl.Where(m => CUtils.StrValue(m.ProductCode) == CUtils.StrValue(itSerial.ProductCode)).ToList();
                        if (lstDtl != null && lstDtl.Count > 0)
                        {
                            itSerial.ProductCode = lstDtl[0].mp_ProductCodeUser;
                        }
                    }
                    #endregion

                    Dictionary<string, string> dicColNamesMst = GetExportDicColumsMaster();
                    Dictionary<string, string> dicColNamesDtl = GetExportDicColumsDtl();
                    Dictionary<string, string> dicColNamesLot = GetExportDicColumsLot();
                    Dictionary<string, string> dicColNamesSerial = GetExportDicColumsSerial();

                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryReturnSup).Name), ref url);
                    var ds = new DataSet();
                    var table = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSup, dicColNamesMst);
                    var tableDtl = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupDtl, dicColNamesDtl);
                    var tableLot = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupInstLot, dicColNamesLot);
                    var tableSerial = ExcelExport.ConstructDataTable(ListInvF_InventoryReturnSupInstSerial, dicColNamesSerial);

                    if (table != null && table.Rows.Count > 0)
                    {
                        table.TableName = "InvF_InventoryReturnSup";
                        ds.Tables.Add(table);
                    }
                    if (tableDtl != null && tableDtl.Rows.Count > 0)
                    {
                        tableDtl.TableName = "InvF_InventoryReturnSupDtl";
                        ds.Tables.Add(tableDtl);
                    }
                    if (tableLot != null && tableLot.Rows.Count > 0)
                    {
                        tableLot.TableName = "InvF_InventoryReturnSupInstLot";
                        ds.Tables.Add(tableLot);
                    }
                    if (tableSerial != null && tableSerial.Rows.Count > 0)
                    {
                        tableSerial.TableName = "InvF_InventoryReturnSupInstSerial";
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
        #endregion

        #region["DicColums"]

        private Dictionary<string, string> GetImportDicColumsTemplateProduct()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa(*)"},
                 {"Qty","Số lượng(*)"},
                 {"InvCodeOutActual","Vị trí(*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateLot()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa"},
                 {"ProductLotNo","Số lô"},
                 {"Qty","Số lượng"},
                 {"ProductionDate","Ngày sản xuất"},
                 {"ExpiredDate","Ngày hết hạn"},
                 {"InvCodeInActual","Vị trí"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplateSerial()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa"},
                 {"SerialNo","Số serial"},
                 {"InvCodeInActual","Vị trí"},
            };
        }

        private Dictionary<string, string> GetExportDicColumsMaster()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvReturnSupNo","Số phiếu trả hàng"},
                {"ApprDTimeUTC","Thời gian trả"},
                {"InvCodeOut","Kho xuất"},
                {"mct_CustomerName","Nhà cung cấp"},
                {"TotalValReturnSupAfterDesc", "Tổng tiền"},
                {"IF_ReturnSupStatus", "Trạng thái"},
                {"Remark", "Nội dung"},
                {"IF_InvInNo", "Số phiếu nhập"},
                {"CreateDTimeUTC", "Thời gian tạo"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvReturnSupNo","Số phiếu trả hàng"},
                {"mp_ProductCodeUser","Mã hàng hóa"},
                {"mp_ProductName","Tên hàng hóa (TV)"},
                {"UnitCode","Đơn vị tính"},
                {"QtyTotalOK", "Tồn kho"},
                {"Qty", "Số lượng"},
                {"InvCodeOutActual", "Vị trí xuất"},
                {"UPIN", "Giá nhập"},
                {"UPReturnSup", "Giá trả hàng"},
                {"ValReturnSup", "Thành tiền"},
                {"Remark", "Ghi chú"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsLot()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvReturnSupNo","Số phiếu trả hàng"},
                {"ProductCode","Mã hàng hóa"},
                {"ProductLotNo","Số lô"},
                {"Qty", "Số lượng"},
                {"InvCodeOutActual", "Vị trí"}
            };
        }

        private Dictionary<string, string> GetExportDicColumsSerial()
        {
            return new Dictionary<string, string>()
            {
                {"IF_InvReturnSupNo","Số phiếu trả hàng"},
                {"ProductCode","Mã hàng hóa"},
                {"SerialNo","Số serial"},
                {"InvCodeOutActual", "Vị trí"}
            };
        }

        #endregion

        #region ["strWhereClause"]

        private string strWhereGetProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
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

        private string strWhereClause_Mst_Product(string productcode = "", string flagactive = "", string producttype = "", string flagsell = "")
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
            if (!CUtils.IsNullOrEmpty(flagsell))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, CUtils.StrValue(flagsell), "=");
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", "=");

            if (!CUtils.IsNullOrEmpty(data.ProductBarCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductBarCode, CUtils.StrValueOrNull(data.ProductBarCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_InvF_InventoryIn(string if_invinno, string customercode, string productcode, string productname)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryIn = TableName.InvF_InventoryIn + ".";
            var Tbl_InvF_InventoryInDtl = TableName.InvF_InventoryInDtl + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(if_invinno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.IF_InvInNo, CUtils.StrValue(if_invinno), "=");
            }
            if (!CUtils.IsNullOrEmpty(customercode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.CustomerCode, CUtils.StrValue(customercode), "=");
            }
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(productname))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(productname) + "%", "like");
            }

            sbSql.AddWhereClause(Tbl_InvF_InventoryIn + TblInvF_InventoryIn.IF_InvInStatus, "APPROVE", "=");

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_InvF_InventoryReturnSupByID(string if_invreturnsupno)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryReturnSup = TableName.InvF_InventoryReturnSup + ".";
            if (!CUtils.IsNullOrEmpty(if_invreturnsupno))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryReturnSup + TblInvF_InventoryReturnSup.IF_InvReturnSupNo, CUtils.StrValue(if_invreturnsupno), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Combo(Prd_BOM data)
        {
            var strWhere = "";
            var sbSql = new StringBuilder();
            var Tbl_Prd_BOM = TableName.Prd_BOM + ".";
            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Prd_BOM + TblPrd_BOM.ProductCodeParent, CUtils.StrValue(data.ProductCode), "=");
            }

            strWhere = sbSql.ToString();
            return strWhere;

        }

        private string strWhereClause_Invf_InventoryBalance(Mst_ProductUI data)
        {
            var strWhere = "";
            var sbSql = new StringBuilder();
            var tbl_Inv_InventoryBalance = TableName.Inv_InventoryBalance + ".";
            var tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(tbl_Inv_InventoryBalance + TblInv_InventoryBalance.ProductCode, CUtils.StrValue(data.ProductCode), "IN");
            }

            if (!CUtils.IsNullOrEmpty(data.InvBUPattern))
            {
                sbSql.AddWhereClause(tbl_Mst_Inventory + TblMst_Inventory.InvBUPattern, CUtils.StrValue(data.InvBUPattern), "like");
            }

            strWhere = sbSql.ToString();
            return strWhere;
        }

        #endregion

        #region ["In"]
        [HttpPost]
        //public ActionResult PrintTemp(string if_invreturnsupno)
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    #region ["UserState Common Info"]            
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    try
        //    {

        //        #region Thông tin chi nhánh

        //        string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
        //        var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);

        //        #endregion

        //        InvF_InventoryReturnSup objInvF_InventoryReturnSup = new InvF_InventoryReturnSup();
        //        var strWhere = strWhereClause_InvF_InventoryReturnSupByID(if_invreturnsupno);
        //        var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount,
        //            Rt_Cols_InvF_InventoryReturnSup = "*",
        //            Rt_Cols_InvF_InventoryReturnSupDtl = "*",
        //            Rt_Cols_InvF_InventoryReturnSupInstLot = "",
        //            Rt_Cols_InvF_InventoryReturnSupInstSerial = "",
        //            Ft_WhereClause = strWhere
        //        };
        //        var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
        //        if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count != 0)
        //        {
        //            objInvF_InventoryReturnSup = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup[0];
        //        }

        //        #region Get List đơn vị tính theo Product + Xử lý list detail UI

        //        var listInvFInventoryReturnSupDtlUI = new List<InvF_InventoryReturnSupDtlUI>();

        //        var Lst_InvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSupDtl>();
        //        if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl != null)
        //        {
        //            Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
        //        }

        //        var lstPrdDistinct = new List<string>();
        //        int idx = 1;
        //        foreach (var item in Lst_InvF_InventoryReturnSupDtl)
        //        {
        //            if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
        //                continue;

        //            var listInvInDtlCur = Lst_InvF_InventoryReturnSupDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
        //            var qty = 0.0;
        //            var valReturnSup = 0.0;
        //            var invCodeOutActual = "";
        //            var listViTri = new List<string>();
        //            foreach (var it in listInvInDtlCur)
        //            {
        //                qty += Convert.ToDouble(it.Qty);
        //                valReturnSup += Convert.ToDouble(it.ValReturnSup);
        //                if (!listViTri.Contains(it.InvCodeOutActual.ToString()))
        //                {
        //                    if (!string.IsNullOrEmpty(invCodeOutActual))
        //                    {
        //                        invCodeOutActual += ", " + it.InvCodeOutActual.ToString();
        //                    }
        //                    else
        //                    {
        //                        invCodeOutActual = it.InvCodeOutActual.ToString();
        //                    }
        //                    listViTri.Add(it.InvCodeOutActual.ToString());
        //                }
        //            }

        //            var itemUI = new InvF_InventoryReturnSupDtlUI()
        //            {
        //                IF_InvReturnSupNo = item.IF_InvReturnSupNo,
        //                InvCodeOutActual = invCodeOutActual,
        //                ProductCode = item.ProductCode,
        //                NetworkID = item.NetworkID,
        //                Qty = qty,
        //                UPReturnSup = item.UPReturnSup,
        //                ValReturnSup = valReturnSup,
        //                UnitCode = item.UnitCode,
        //                Remark = item.Remark,
        //                ProductName = item.mp_ProductName,
        //                ProductCodeUser = item.mp_ProductCodeUser,                        
        //                Idx = idx
        //            };

        //            //
        //            listInvFInventoryReturnSupDtlUI.Add(itemUI);
        //            idx++;

        //            lstPrdDistinct.Add(item.ProductCode.ToString());
        //        }

        //        #endregion
        //        //
        //        string strNNTFullName = "";
        //        string strNNTAddress = "";
        //        string strNNTPhone = "";
        //        if (listMst_NNT != null && listMst_NNT.Count > 0)
        //        {
        //            strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
        //            strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
        //            strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
        //        }

        //        InvF_InventoryReturnSupPrint objPrint = new InvF_InventoryReturnSupPrint();
        //        objPrint.NNTFullName = strNNTFullName;
        //        objPrint.NNTAddress = strNNTAddress;
        //        objPrint.NNTPhone = strNNTPhone;
        //        objPrint.NNTFullName = strNNTFullName;
        //        objPrint.IF_InvCusReturnNo = objInvF_InventoryReturnSup.IF_InvReturnSupNo;

        //        DateTime dtNow = DateTime.Now;
        //        objPrint.DatePrint = dtNow.ToString("dd");
        //        objPrint.MonthPrint = dtNow.ToString("MM");
        //        objPrint.YearPrint = dtNow.ToString("yyyy");
        //        objPrint.CustomerName = objInvF_InventoryReturnSup.mct_CustomerName;
        //        objPrint.OrderNo = objInvF_InventoryReturnSup.OrderNo;
        //        objPrint.InvNameOut = objInvF_InventoryReturnSup.InvCodeOut;
        //        objPrint.Remark = objInvF_InventoryReturnSup.Remark;
        //        objPrint.TotalQty = listInvFInventoryReturnSupDtlUI.Sum(m => Convert.ToDouble(m.Qty));
        //        objPrint.TotalValReturnSup = objInvF_InventoryReturnSup.TotalValReturnSup;
        //        objPrint.TotalValReturnSupDesc = objInvF_InventoryReturnSup.TotalValReturnSupDesc;
        //        objPrint.TotalValReturnSupAfterDesc = objInvF_InventoryReturnSup.TotalValReturnSupAfterDesc;
        //        objPrint.CreateUserName = objInvF_InventoryReturnSup.su_UserName_Create;
        //        objPrint.LogoFilePath = "";
        //        objPrint.Lst_InvF_InventoryReturnSupDtlUI = listInvFInventoryReturnSupDtlUI;

        //        #region Lấy mẫu in

        //        string linkPdf = "";
        //        var listInvF_TempPrint = ListInvF_TempPrint("PTH");
        //        if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
        //        {
        //            var objInvF_TempPrint = listInvF_TempPrint[0];
        //            var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
        //            dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
        //            var watermark = "";

        //            if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
        //            {
        //                string logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
        //                objPrint.LogoFilePath = logofilepath;
        //            }

        //            data.data = CreateDataObjectReportServer(objPrint, ref watermark);
        //            data.watermark = watermark;
        //            var outputFormat = data.outputFormat;
        //            if (CUtils.IsNullOrEmpty(outputFormat))
        //            {
        //                outputFormat = "pdf";
        //            }
        //            var content = PostReport(data);

        //            string serverUrl = ReportBro_ServerUrl;
        //            if (!CUtils.IsNullOrEmpty(content))
        //            {
        //                content = CUtils.StrReplace(content, "\"", "");
        //                if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
        //                {
        //                    var iReportBro_Key = ReportBro_Key.Length;
        //                    var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
        //                    var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
        //                    linkPdf = linkpdf + "#toolbar=0";
        //                    return Json(new { Success = true, LinkPDF = linkPdf });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            resultEntry.SetFailed();
        //            resultEntry.AddMessage("Không có mẫu in");
        //            return Json(resultEntry);
        //        }

        //        #endregion
        //    }
        //    catch (ClientException ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultEntry.SetFailed().AddException(this, ex);
        //    }
        //    resultEntry.AddModelState(ViewData.ModelState);
        //    return Json(resultEntry);
        //}
        public ActionResult PrintTemp(string if_invreturnsupno)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region["Thông tin chi nhánh"]
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);

                #endregion
                InvF_InventoryReturnSup objInvF_InventoryReturnSup = new InvF_InventoryReturnSup();
                var strWhere = strWhereClause_InvF_InventoryReturnSupByID(if_invreturnsupno);
                var objRQ_InvF_InventoryReturnSup = new RQ_InvF_InventoryReturnSup()
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
                    Rt_Cols_InvF_InventoryReturnSup = "*",
                    Rt_Cols_InvF_InventoryReturnSupDtl = "*",
                    Rt_Cols_InvF_InventoryReturnSupInstLot = "",
                    Rt_Cols_InvF_InventoryReturnSupInstSerial = "",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryReturnSup = InvFInventoryReturnSupService.Instance.WA_InvF_InventoryReturnSup_Get(objRQ_InvF_InventoryReturnSup, addressAPIs);
                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup.Count > 0)
                {
                    objInvF_InventoryReturnSup = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSup[0];
                }
                #region["Get List đơn vị tính theo Product + Xử lý list detail UI"]
                var listInvFInventoryReturnSupDtlUI = new List<InvF_InventoryReturnSupDtlUI>();
                var Lst_InvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSupDtl>();

                if (objRT_InvF_InventoryReturnSup != null && objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl != null)
                {
                    Lst_InvF_InventoryReturnSupDtl = objRT_InvF_InventoryReturnSup.Lst_InvF_InventoryReturnSupDtl;
                }

                var lstPrdDistinct = new List<string>();
                int idx = 1;
                foreach (var item in Lst_InvF_InventoryReturnSupDtl)
                {
                    if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
                        continue;
                    var listInvInDtlCur = Lst_InvF_InventoryReturnSupDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    var qty = 0.0;
                    var valReturnSup = 0.0;
                    var invCodeOutActual = "";
                    var listViTri = new List<string>();
                    foreach (var it in listInvInDtlCur)
                    {
                        qty += Convert.ToDouble(it.Qty);
                        valReturnSup += Convert.ToDouble(it.ValReturnSup);
                        if (!listViTri.Contains(it.InvCodeOutActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeOutActual))
                            {
                                invCodeOutActual += ", " + it.InvCodeOutActual.ToString();
                            }
                            else
                            {
                                invCodeOutActual = it.InvCodeOutActual.ToString();
                            }
                            listViTri.Add(it.InvCodeOutActual.ToString());
                        }
                    }


                    var itemUI = new InvF_InventoryReturnSupDtlUI()
                    {
                        IF_InvReturnSupNo = item.IF_InvReturnSupNo,
                        InvCodeOutActual = invCodeOutActual,
                        ProductCode = item.ProductCode,
                        NetworkID = item.NetworkID,
                        Qty = qty,
                        UPReturnSup = item.UPReturnSup,
                        ValReturnSup = valReturnSup,
                        UnitCode = item.UnitCode,
                        Remark = item.Remark,
                        ProductName = item.mp_ProductName,
                        ProductCodeUser = item.mp_ProductCodeUser,
                        Idx = idx
                    };

                    listInvFInventoryReturnSupDtlUI.Add(itemUI);
                    idx++;

                    lstPrdDistinct.Add(item.ProductCode.ToString());
                }
                #endregion

                string strNNTFullName = "";
                string strNNTAddress = "";
                string strNNTPhone = "";
                if (listMst_NNT != null && listMst_NNT.Count > 0)
                {
                    strNNTFullName = CUtils.StrValueNew(listMst_NNT[0].NNTFullName);
                    strNNTAddress = CUtils.StrValueNew(listMst_NNT[0].NNTAddress);
                    strNNTPhone = CUtils.StrValueNew(listMst_NNT[0].NNTPhone);
                }

                InvF_InventoryReturnSupPrint objPrint = new InvF_InventoryReturnSupPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.NNTFullName = strNNTFullName;
                objPrint.IF_InvCusReturnNo = objInvF_InventoryReturnSup.IF_InvReturnSupNo;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CustomerName = objInvF_InventoryReturnSup.mct_CustomerName;
                objPrint.OrderNo = objInvF_InventoryReturnSup.OrderNo;
                objPrint.InvNameOut = objInvF_InventoryReturnSup.InvCodeOut;
                objPrint.Remark = objInvF_InventoryReturnSup.Remark;
                objPrint.TotalQty = listInvFInventoryReturnSupDtlUI.Sum(m => Convert.ToDouble(m.Qty));
                objPrint.TotalValReturnSup = objInvF_InventoryReturnSup.TotalValReturnSup;
                objPrint.TotalValReturnSupDesc = objInvF_InventoryReturnSup.TotalValReturnSupDesc;
                objPrint.TotalValReturnSupAfterDesc = objInvF_InventoryReturnSup.TotalValReturnSupAfterDesc;
                objPrint.CreateUserName = objInvF_InventoryReturnSup.su_UserName_Create;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_InventoryReturnSupDtlUI = listInvFInventoryReturnSupDtlUI;

                #region Lấy mẫu in

                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("PTH");
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
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        public dynamic CreateDataObjectReportServer(InvF_InventoryReturnSupPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_InventoryReturnSupDtl;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new InvF_InventoryReturnSupReportServer();
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
                MyDynamic.InvNameOut = CUtils.StrValueNew(objTempPrint.InvNameOut);
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, NumericFormat(tableName, "TotalQty", defaultFormat));
                MyDynamic.TotalValReturnSup = CUtils.StrValueFormatNumber(objTempPrint.TotalValReturnSup, NumericFormat(tableName, "TotalValReturnSup", defaultFormat));
                MyDynamic.TotalValReturnSupDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValReturnSupDesc, NumericFormat(tableName, "TotalValReturnSupDesc", defaultFormat));
                MyDynamic.TotalValReturnSupAfterDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValReturnSupAfterDesc, NumericFormat(tableName, "TotalValReturnSupAfterDesc", defaultFormat));
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_InventoryReturnSupDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_InvF_InventoryReturnSupDtlUI != null && objTempPrint.Lst_InvF_InventoryReturnSupDtlUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_InventoryReturnSupDtlReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_InventoryReturnSupDtlUI)
                {
                    var objDtl_ReportServer = new InvF_InventoryReturnSupDtlReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.ProductCodeUser),
                        ProductName = CUtils.StrValue(item.ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        Dtl_InvOutName = CUtils.StrValue(item.InvCodeOutActual),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, strFormatQty),
                        UPReturnSup = CUtils.StrValueFormatNumber(item.UPReturnSup, NumericFormat(tableName, "UPReturnSup", defaultFormat)),
                        ValReturnSup = CUtils.StrValueFormatNumber(item.ValReturnSup, NumericFormat(tableName, "ValReturnSup", defaultFormat))
                    };

                    listDtl_ReportServer.Add(objDtl_ReportServer);
                }

                MyDynamic.DataTable.AddRange(listDtl_ReportServer);

            }
            return MyDynamic;
        }


        #endregion

        [HttpPost]
        public ActionResult ExportTemplateProduct()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryReturnSupDtl = new List<InvF_InventoryReturnSup>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateProduct();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryReturnSup).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInvF_InventoryReturnSupDtl, dicColNames, filePath, string.Format("InvF_InventoryInDtl"));
                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }

        #region["Import excell"]

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ImportProduct(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";


            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;

            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lst_MstProductUI = new List<Mst_ProductUI>();
            if (ModelState.IsValid)
            {
                try
                {
                    #region ["Mst_Inventory"]
                    var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";
                    if (!CUtils.IsNullOrEmpty(invBUPattern))
                    {
                        strWhere_Mst_Inventory += " and ";
                        strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                    }
                    var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);

                    #endregion


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
                        #region["Check null"]

                        foreach(DataRow dr in table.Rows)
                        {
                            if(dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã hàng hóa không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if(dr["Qty"] == null || dr["Qty"].ToString().Trim().Length == 0)
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

                            if (dr["InvCodeOutActual"] == null || dr["InvCodeOutActual"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Vị trí không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                string invCodeOutActual = dr["InvCodeOutActual"].ToString().Trim();
                                var lstInvCheck = list_Mst_Inventory.Where(m => Convert.ToString(m.InvCode) == invCodeOutActual).ToList();
                                if (lstInvCheck == null || lstInvCheck.Count == 0)
                                {
                                    exitsData = "Vị trí '" + invCodeOutActual + "' không có trong hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                        }
                        #endregion


                        #region["Check ma hnag hoa trung"]
                        for(var i = 0; i < table.Rows.Count; i++)
                        {
                            var productCodeUser1 = CUtils.StrValue(table.Rows[i]["ProductCodeUser"]);
                            var invCodeOutActual1 = CUtils.StrValue(table.Rows[i]["InvCodeOutActual"]);
                            for(var j = 0; j < table.Rows.Count; j++)
                            {
                                if(i != j)
                                {
                                    var productCodeUser2 = CUtils.StrValue(table.Rows[j]["ProductCodeUser"]);
                                    var invCodeOutActual2 = CUtils.StrValue(table.Rows[j]["InvCodeOutActual"]);
                                    if (productCodeUser1.Equals(productCodeUser2) && invCodeOutActual1.Equals(invCodeOutActual2))
                                    {
                                        exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có vị trí '" + invCodeOutActual1 + "' trong file excel lặp!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }

                        #endregion
                        #region["Check ma hang hoa ton tai trong he thong va add vao list"]
                        List<string> lstProductCodeUser = new List<string>();


                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryReturnSupDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryReturnSupDtlUI>();

                        lstProductCodeUser = listDtlImport.Select(m => m.ProductCodeUser.ToString()).ToList();
                        var lstPrdCodeBase = new List<string>();

                        #region["Lay danh sach hang hoa"]
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

                        var lst_StrProductCode = new List<string>();
                        foreach(var item in listDtlImport)
                        {
                            var listPrdCheck = new List<Mst_Product>();
                            if(listProduct != null && listProduct.Count > 0)
                            {
                                listPrdCheck = listProduct.Where(x => x.ProductCodeUser.ToString() == item.ProductCodeUser.ToString()).ToList();
                            }

                            if(listPrdCheck != null && listPrdCheck.Count > 0)
                            {
                                // Dùng lấy tồn kho
                                lst_StrProductCode.Add(listPrdCheck[0].ProductCodeBase.ToString());


                                item.ProductCode = listPrdCheck[0].ProductCode;
                                item.mp_ProductCodeBase = listPrdCheck[0].ProductCodeBase;
                                item.ProductCodeRoot = listPrdCheck[0].ProductCodeRoot;
                                item.UnitCode = listPrdCheck[0].UnitCode;
                                item.ProductName = listPrdCheck[0].ProductName;
                                item.ProductCodeUser = listPrdCheck[0].ProductCodeUser;
                                item.UPIN = listPrdCheck[0].UPBuy;
                                item.UPReturnSup = listPrdCheck[0].UPBuy;
                                item.mp_FlagLot = listPrdCheck[0].FlagLot;
                                item.mp_FlagSerial = listPrdCheck[0].FlagSerial;
                                item.ValConvert = listPrdCheck[0].ValConvert;

                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var giatra = item.UPReturnSup == null ? 0 : Convert.ToDouble(item.UPReturnSup);

                                var valreturnsup = qty * giatra;
                                item.ValReturnSup = valreturnsup;

                                lstPrdCodeBase.Add(listPrdCheck[0].ProductCodeBase.ToString());
                            }
                            else
                            {
                                exitsData = "Mã hàng hóa '" + item.mp_ProductCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }


                        var listProductCode_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => CUtils.StrValue(item.ProductCode)).Distinct().ToList();
                        if(listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                        {
                            foreach(var item in listProductCode_Distinct)
                            {
                                var objReturnSupDtl = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                                if(objReturnSupDtl!= null)
                                {
                                   
                           
                                    var objInvF_InventoryReturnSupInstLot = new Mst_ProductUI()
                                    {
                                        ProductCode = objReturnSupDtl.ProductCode,
                                        ProductCodeUser = objReturnSupDtl.ProductCodeUser,
                                        ProductCodeBase = objReturnSupDtl.mp_ProductCodeBase,
                                        ProductCodeRoot = objReturnSupDtl.ProductCodeRoot,
                                        ProductName = objReturnSupDtl.ProductName,
                                        UnitCode = objReturnSupDtl.UnitCode,
                                        ValConvert = objReturnSupDtl.ValConvert,
                                        FlagLot = objReturnSupDtl.mp_FlagLot,
                                        FlagSerial = objReturnSupDtl.mp_FlagSerial,
                                        UPIn = objReturnSupDtl.UPIN,
                                        UPReturnSup = objReturnSupDtl.UPReturnSup,

                                    };


                                   
                                    var strInvCodeOutActual = "";

                                    var listDtlCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                                    if(listDtlCur != null && listDtlCur.Count > 0)
                                    {
                                        var fQty = 0.0;
                                        var i = 0;
                                        foreach(var _it in listDtlCur)
                                        {
                                            fQty += CUtils.ConvertToDouble(_it.Qty);
                                            objInvF_InventoryReturnSupInstLot.Qty = fQty;
                                            objInvF_InventoryReturnSupInstLot.ValReturnSup = fQty * CUtils.ConvertToDouble(_it.UPReturnSup);
                                            if(i != 0)
                                            {
                                                strInvCodeOutActual += ", ";
                                            }

                                            strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                            objInvF_InventoryReturnSupInstLot.InvCodeInActual = strInvCodeOutActual;
                                            i++;
                                        }
                                    }


                                    #region["Danh sach hang hoa cung base"]
                                    var strWhereCungBase = "";
                                    var sbSqlCungBase = new StringBuilder();
                                    if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSupInstLot.ProductCodeBase))
                                    {
                                        sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objInvF_InventoryReturnSupInstLot.ProductCodeBase), "=");
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
                                    if(objRT_Mst_Product1 != null && objRT_Mst_Product1.Lst_Mst_Product != null && objRT_Mst_Product1.Lst_Mst_Product.Count > 0)
                                    {
                                        foreach(var it2 in objRT_Mst_Product1.Lst_Mst_Product)
                                        {
                                            var productUI = new Mst_ProductUI();
                                            productUI.ProductCode = it2.ProductCode;
                                            productUI.ProductCodeUser = it2.ProductCodeUser;
                                            productUI.ProductCodeBase = it2.ProductCodeBase;
                                            productUI.ProductCodeRoot = it2.ProductCodeRoot;
                                            productUI.ProductName = it2.ProductName;
                                            productUI.UnitCode = it2.UnitCode;
                                            productUI.ValConvert = it2.ValConvert;
                                            productUI.FlagLot = it2.FlagLot;
                                            productUI.FlagSerial = it2.FlagSerial;
                                            productUI.ProductType = it2.ProductType;
                                            if (it2.FlagLot.Equals("1"))
                                            {
                                                productUI.FlagLo = "1";
                                            }
                                            if (it2.ProductType != null && it2.ProductType.ToString().ToUpper().Equals("COMBO") && it2.ValConvert != null)
                                            {
                                                productUI.FlagCombo = "1";
                                            }
                                            productUI.UPIn = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.UPReturnSup = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.ValReturnSup = "0";
                                            #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]
                                            var strWhereClauseTonKho1 = "";
                                            var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                                            var sb_SQL13 = new StringBuilder();
                                            if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                                            {
                                                sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                                            }
                                            var InvBUPattern = invBUPattern;
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
                                                    productUI.Qty = qtymax1;
                                                }
                                            }
                                            lst_Mst_ProductUI.Add(productUI);
                                            objInvF_InventoryReturnSupInstLot.lstUnitCodeUIByProduct = lst_Mst_ProductUI;
                                            #endregion
                                        }
                                    }
                                    
                                    lst_MstProductUI.Add(objInvF_InventoryReturnSupInstLot);

                                    foreach(var item1 in lst_MstProductUI)
                                    {
                                        var productCodecur = item1.ProductCode;
                                        var listUI = item1.lstUnitCodeUIByProduct;
                                        var listobjUI = listUI.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(productCodecur)).ToList();
                                        item1.QtyTotalOK = listobjUI[0].QtyTotalOK;
                                    }
                                    #endregion
                                }


                            }
                        }

                        #endregion
                        return Json(new { Success = true, listDtlImport = listDtlImport, list_MstProductUI = lst_MstProductUI });

                    }
                    else
                    {
                        exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }


                }
                catch (ClientException ex)
                {
                    resultEntry.SetFailed().AddException(ex);
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
            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";
            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;
            //var lstProductCodeUser = new List<string>();
            var lstProductCodeUser_Distinct = new List<string>();


            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lst_MstProductUI = new List<Mst_ProductUI>();
            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var exitsData = "";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            if (ModelState.IsValid)
            {
                try
                {

                    #region ["Mst_Inventory"]
                    var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                    if (!string.IsNullOrEmpty(invBUPattern))
                    {
                        strWhere_Mst_Inventory += " and ";
                        strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                    }

                    var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
                    #endregion


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
                        #region["Check null"]
                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã hàng hóa không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            if (dr["ProductLotNo"] == null || dr["ProductLotNo"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Số lô không được để trống!";
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



                            //if (dr["InvCodeOutActual"] == null || dr["InvCodeOutActual"].ToString().Trim().Length == 0)
                            //{
                            //    exitsData = "Vị trí không được để trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    string invCodeOutActual = dr["InvCodeOutActual"].ToString().Trim();
                            //    var lstInvCheck = list_Mst_Inventory.Where(m => Convert.ToString(m.InvCode) == invCodeOutActual).ToList();
                            //    if (lstInvCheck == null || lstInvCheck.Count == 0)
                            //    {
                            //        exitsData = "Vị trí '" + invCodeOutActual + "' không có trong hệ thống!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
                        }
                        #endregion

                        #region["Check ma hang hoa trung"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var productCode1 = table.Rows[i]["ProductCodeUser"].ToString().Trim();
                            var productLotNo1 = table.Rows[i]["ProductLotNo"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var productCode2 = table.Rows[j]["ProductCodeUser"].ToString().Trim();
                                    var productLotNo2 = table.Rows[j]["ProductLotNo"].ToString().Trim();
                                    if (productCode1 == productCode2 && productLotNo1 == productLotNo2)
                                    {
                                        exitsData = "Số lô '" + productLotNo1 + "' của mã hàng hóa '" + productCode1 + "' trong file excel lặp!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]
                        List<string> lstProductCodeUser = new List<string>();

                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryReturnSupDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryReturnSupDtlUI>();

                        lstProductCodeUser = listDtlImport.Select(m => m.ProductCodeUser.ToString()).ToList();

                        var lstPrdCodeBase = new List<string>();

                        #region["Lay danh sach hang hoa"]
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

                        var lst_StrProductCode = new List<string>();
                        foreach(var item in listDtlImport)
                        {
                            var listPrdCheck = new List<Mst_Product>();
                            if(listProduct != null && listProduct.Count > 0)
                            {
                                listPrdCheck = listProduct.Where(x => x.ProductCodeUser.ToString() == item.ProductCodeUser.ToString()).ToList();
                            }


                            if(listPrdCheck != null && listPrdCheck.Count > 0)
                            {
                                lst_StrProductCode.Add(listPrdCheck[0].ProductCodeBase.ToString());


                                item.ProductCode = listPrdCheck[0].ProductCode;
                                item.mp_ProductCodeBase = listPrdCheck[0].ProductCodeBase;
                                item.ProductCodeRoot = listPrdCheck[0].ProductCodeRoot;
                                item.UnitCode = listPrdCheck[0].UnitCode;
                                item.ProductName = listPrdCheck[0].ProductName;
                                item.ProductCodeUser = listPrdCheck[0].ProductCodeUser;
                                item.UPIN = listPrdCheck[0].UPBuy;
                                item.UPReturnSup = listPrdCheck[0].UPBuy;
                                item.mp_FlagLot = listPrdCheck[0].FlagLot;
                                item.mp_FlagSerial = listPrdCheck[0].FlagSerial;
                                item.ValConvert = listPrdCheck[0].ValConvert;

                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var giatra = item.UPReturnSup == null ? 0 : Convert.ToDouble(item.UPReturnSup);

                                var valreturnsup = qty * giatra;
                                item.ValReturnSup = valreturnsup;


                                if (CUtils.IsNullOrEmpty(item.InvCodeOutActual))
                                {
                                    var strWhereClauseTonKho = "";
                                    var tblInv_InventoryBalance12 = "Inv_InventoryBalance.";

                                    var sb_SQL12 = new StringBuilder();
                                    if (CUtils.StrValue(item.mp_ProductCodeBase) != null && CUtils.StrValue(item.mp_ProductCodeBase) != "")
                                    {
                                        sb_SQL12.AddWhereClause(tblInv_InventoryBalance12 + "ProductCode", CUtils.StrValue(item.mp_ProductCodeBase), "=");
                                    }

                                    if (invBUPattern != null && invBUPattern != "")
                                    {
                                        sb_SQL12.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
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
                                    if (CUtils.StrValue(item.ValConvert) != null && CUtils.StrValue(item.ValConvert) != "")
                                    {
                                        ValConvert = Convert.ToDouble(CUtils.StrValue(item.ValConvert));
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


                                    item.InvCodeOutActual = invCodeMax;
                                }


                                #region["Check số lô có tồn tại hay không"]
                                var listInvBlanceLot = GetListInventoryBalanceLot(CUtils.StrValue(item.mp_ProductCodeBase), CUtils.StrValue(item.ProductLotNo), invBUPattern);
                                if (listInvBlanceLot == null || listInvBlanceLot.Count == 0)
                                {
                                    exitsData = "' Lô '" + item.ProductLotNo + "' không có trong hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                #endregion



                            }
                            else
                            {
                                exitsData = "Mã hàng hóa '" + item.mp_ProductCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }

                        var listProductCode_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => CUtils.StrValue(item.ProductCode)).Distinct().ToList();
                        if(listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                        {
                            foreach(var item in listProductCode_Distinct)
                            {
                                var objReturnSupDtl = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                                if(objReturnSupDtl != null)
                                {
                                    var objInvF_InventoryReturnSupInstLot = new Mst_ProductUI()
                                    {
                                        ProductCode = objReturnSupDtl.ProductCode,
                                        ProductCodeUser = objReturnSupDtl.ProductCodeUser,
                                        ProductCodeBase = objReturnSupDtl.mp_ProductCodeBase,
                                        ProductCodeRoot = objReturnSupDtl.ProductCodeRoot,
                                        ProductName = objReturnSupDtl.ProductName,
                                        UnitCode = objReturnSupDtl.UnitCode,
                                        ValConvert = objReturnSupDtl.ValConvert,
                                        FlagLot = objReturnSupDtl.mp_FlagLot,
                                        FlagSerial = objReturnSupDtl.mp_FlagSerial,
                                        UPIn = objReturnSupDtl.UPIN,
                                        UPReturnSup = objReturnSupDtl.UPReturnSup,
                                        InvCodeInActual = objReturnSupDtl.InvCodeOutActual
                                    };
                                    var strInvCodeOutActual = "";

                                    var listDtlCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                                    if(listDtlCur != null && listDtlCur.Count > 0)
                                    {
                                        var fQty = 0.0;
                                        var i = 0;
                                        var fValReturnSup = 0.0;
                                        foreach(var _it in listDtlCur)
                                        {
                                            fQty += CUtils.ConvertToDouble(_it.Qty);
                                            fValReturnSup += CUtils.ConvertToDouble(_it.ValReturnSup);
                                            objInvF_InventoryReturnSupInstLot.Qty = fQty;
                                            objInvF_InventoryReturnSupInstLot.ValReturnSup = fValReturnSup;
                                            if (i != 0)
                                            {
                                                strInvCodeOutActual += ", ";
                                            }

                                            //if(CUtils.IsNullOrEmpty())

                                            strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                            objInvF_InventoryReturnSupInstLot.InvCodeInActual = strInvCodeOutActual;
                                            i++;
                                        }
                                    }


                                    #region["Danh sach hang hoa cung base"]
                                    var strWhereCungBase = "";
                                    var sbSqlCungBase = new StringBuilder();
                                    if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSupInstLot.ProductCodeBase))
                                    {
                                        sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objInvF_InventoryReturnSupInstLot.ProductCodeBase), "=");
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
                                    if(objRT_Mst_Product1 != null && objRT_Mst_Product1.Lst_Mst_Product != null && objRT_Mst_Product1.Lst_Mst_Product.Count > 0)
                                    {
                                        foreach(var it2 in objRT_Mst_Product1.Lst_Mst_Product)
                                        {
                                            var productUI = new Mst_ProductUI();
                                            productUI.ProductCode = it2.ProductCode;
                                            productUI.ProductCodeUser = it2.ProductCodeUser;
                                            productUI.ProductCodeBase = it2.ProductCodeBase;
                                            productUI.ProductCodeRoot = it2.ProductCodeRoot;
                                            productUI.ProductName = it2.ProductName;
                                            productUI.UnitCode = it2.UnitCode;
                                            productUI.ValConvert = it2.ValConvert;
                                            productUI.FlagLot = it2.FlagLot;
                                            productUI.FlagSerial = it2.FlagSerial;
                                            productUI.ProductType = it2.ProductType;
                                            if (it2.FlagLot.Equals("1"))
                                            {
                                                productUI.FlagLo = "1";
                                            }
                                            if (it2.ProductType != null && it2.ProductType.ToString().ToUpper().Equals("COMBO") && it2.ValConvert != null)
                                            {
                                                productUI.FlagCombo = "1";
                                            }
                                            productUI.UPIn = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.UPReturnSup = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.ValReturnSup = "0";
                                            #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]

                                            var strWhereClauseTonKho1 = "";
                                            var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                                            var sb_SQL13 = new StringBuilder();
                                            if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                                            {
                                                sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                                            }
                                            var InvBUPattern = invBUPattern;
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
                                            if(objRT_Inv_InventoryBalanceTonKho1 != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance.Count > 0)
                                            {
                                                lstInv_InventoryBalanceTonKho1 = objRT_Inv_InventoryBalanceTonKho1.Lst_Inv_InventoryBalance;
                                                if(lstInv_InventoryBalanceTonKho1.Count > 0)
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
                                                    productUI.Qty = qtymax1;
                                                }
                                            }
                                            lst_Mst_ProductUI.Add(productUI);
                                            objInvF_InventoryReturnSupInstLot.lstUnitCodeUIByProduct = lst_Mst_ProductUI;
                                            #endregion
                                        }
                                    }

                                    lst_MstProductUI.Add(objInvF_InventoryReturnSupInstLot);


                                    foreach(var item1 in lst_MstProductUI)
                                    {
                                        var productCodecur = item1.ProductCode;
                                        var listUI = item1.lstUnitCodeUIByProduct;
                                        var listobjUI = listUI.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(productCodecur)).ToList();
                                        item1.QtyTotalOK = listobjUI[0].QtyTotalOK;
                                    }

                                    #endregion

                                }
                            }
                        }









                        #endregion
                        return Json(new { Success = true, listDtlImport = listDtlImport, list_MstProductUI = lst_MstProductUI });
                    }
                    else
                    {
                        exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }

                }
                catch (ClientException ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }

            }
            return Json(resultEntry);
        }





        private List<Inv_InventoryBalanceLot> GetListInventoryBalanceLot(string productCodeBase, string productLotNo, string invBUPattern)
        {
            var strWhere = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
            var Tbl_Mst_Inventory = "Mst_Inventory.";
            if (!string.IsNullOrEmpty(productCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
            }
            if (!string.IsNullOrEmpty(productLotNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductLotNo", CUtils.StrValue(productLotNo), "=");
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
            var rt = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(rq, CUtils.StrValue(UserState.AddressAPIs));
            if (rt != null && rt.Lst_Inv_InventoryBalanceLot != null)
            {
                lstInv_InventoryBalanceLot = rt.Lst_Inv_InventoryBalanceLot;
            }
            return lstInv_InventoryBalanceLot;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportSerial(HttpPostedFileWrapper file, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";
            var qtytotalok = 0.0;
            var invCodeMax = "";
            var qtymax = 0.0;
            var qtymax1 = 0.0;
            //var lstProductCodeUser = new List<string>();
            var lstProductCodeUser_Distinct = new List<string>();


            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lst_MstProductUI = new List<Mst_ProductUI>();
            var invCodeMax1 = "";
            var qtytotalok1 = 0.0;
            var exitsData = "";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    #region ["Mst_Inventory"]
                    var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

                    if (!string.IsNullOrEmpty(invBUPattern))
                    {
                        strWhere_Mst_Inventory += " and ";
                        strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
                    }

                    var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
                    #endregion


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
                        //if (table.Columns.Count != 2)
                        //{
                        //    exitsData = "File excel import không hợp lệ!";
                        //    resultEntry.AddMessage(exitsData);
                        //    return Json(resultEntry);
                        //}

                        #region["Check null"]
                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã hàng hóa không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (dr["SerialNo"] == null || dr["SerialNo"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Số Serial không được để trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (dr["InvCodeOutActual"] == null || dr["InvCodeOutActual"].ToString().Trim().Length == 0)
                            //{
                            //    exitsData = "Vị trí không được để trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    string invCodeInActual = CUtils.StrValue(dr["InvCodeOutActual"]);
                            //    var objMst_Inventory = list_Mst_Inventory.Where(it => !CUtils.IsNullOrEmpty(it.InvCode) && CUtils.StrValue(it.InvCode).Equals(invCodeInActual)).FirstOrDefault();
                            //    if (objMst_Inventory == null)
                            //    {
                            //        exitsData = "Vị trí '" + invCodeInActual + "' không có trong hệ thống!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
                        }

                        #endregion

                        #region["Check ma hang hoa trung"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var productCodeUser1 = table.Rows[i]["ProductCodeUser"].ToString().Trim();
                            var serialNo1 = table.Rows[i]["SerialNo"].ToString().Trim();
                            //var invCodeOutActual1 = table.Rows[i]["InvCodeOutActual"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var productCodeUser2 = table.Rows[j]["ProductCodeUser"].ToString().Trim();
                                    var serialNo2 = table.Rows[j]["SerialNo"].ToString().Trim();
                                    //var invCodeOutActual2 = table.Rows[j]["InvCodeOutActual"].ToString().Trim();
                                    //if (serialNo1 == serialNo2 && invCodeOutActual1 == invCodeOutActual2)
                                    //{
                                    //    //exitsData = "Số Serial '" + serialNo1 + "' của mã hàng hóa '" + productCodeUser1 + "' trong file excel lặp!";
                                    //    //resultEntry.AddMessage(exitsData);
                                    //    //return Json(resultEntry);

                                    //    exitsData = "Số Serial '" + serialNo1 + "' có vị trí '" + invCodeOutActual1 + "' trong file excel lặp!";
                                    //    resultEntry.AddMessage(exitsData);
                                    //    return Json(resultEntry);
                                    //}
                                }
                            }
                        }

                        #endregion

                        #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]
                        List<string> lstProductCodeUser = new List<string>();
                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryReturnSupDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryReturnSupDtlUI>();

                        lstProductCodeUser = listDtlImport.Select(m => m.ProductCodeUser.ToString()).ToList();
                        var lstPrdCodeBase = new List<string>();


                        #region["Lay danh sach hang hoa"]
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

                        var lst_StrProductCode = new List<string>();
                        foreach (var item in listDtlImport)
                        {
                            var listPrdCheck = new List<Mst_Product>();
                            if (listProduct != null && listProduct.Count > 0)
                            {
                                listPrdCheck = listProduct.Where(x => x.ProductCodeUser.ToString() == item.ProductCodeUser.ToString()).ToList();
                            }

                            if (listPrdCheck != null && listPrdCheck.Count > 0)
                            {
                                lst_StrProductCode.Add(listPrdCheck[0].ProductCodeBase.ToString());

                                item.ProductCode = listPrdCheck[0].ProductCode;
                                item.mp_ProductCodeBase = listPrdCheck[0].ProductCodeBase;
                                item.ProductCodeRoot = listPrdCheck[0].ProductCodeRoot;
                                item.UnitCode = listPrdCheck[0].UnitCode;
                                item.ProductName = listPrdCheck[0].ProductName;
                                item.ProductCodeUser = listPrdCheck[0].ProductCodeUser;
                                item.UPIN = listPrdCheck[0].UPBuy;
                                item.UPReturnSup = listPrdCheck[0].UPBuy;
                                item.mp_FlagLot = listPrdCheck[0].FlagLot;
                                item.mp_FlagSerial = listPrdCheck[0].FlagSerial;
                                item.ValConvert = listPrdCheck[0].ValConvert;

                                var strWhereClauseTonKho = "";
                                var tblInv_InventoryBalance12 = "Inv_InventoryBalance.";

                                var sb_SQL12 = new StringBuilder();
                                if (CUtils.StrValue(item.mp_ProductCodeBase) != null && CUtils.StrValue(item.mp_ProductCodeBase) != "")
                                {
                                    sb_SQL12.AddWhereClause(tblInv_InventoryBalance12 + "ProductCode", CUtils.StrValue(item.mp_ProductCodeBase), "=");
                                }

                                if (invBUPattern != null && invBUPattern != "")
                                {
                                    sb_SQL12.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
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
                                if (CUtils.StrValue(item.ValConvert) != null && CUtils.StrValue(item.ValConvert) != "")
                                {
                                    ValConvert = Convert.ToDouble(CUtils.StrValue(item.ValConvert));
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


                                item.InvCodeOutActual = invCodeMax;

                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var giatra = item.UPReturnSup == null ? 0 : Convert.ToDouble(item.UPReturnSup);

                                var valreturnsup = qty * giatra;
                                item.ValReturnSup = valreturnsup;

                                lstPrdCodeBase.Add(listPrdCheck[0].ProductCodeBase.ToString());


                                var listInvBalanceSerial = GetListInventoryBalanceSerial(CUtils.StrValue(item.mp_ProductCodeBase), invBUPattern);
                                var serialNo = CUtils.StrValue(item.SerialNo);
                                var lstSerialCur = listInvBalanceSerial.Where(m => m.SerialNo.Equals(serialNo)).ToList();
                                if (lstSerialCur == null || lstSerialCur.Count == 0)
                                {
                                    exitsData = "Mã serial'" + serialNo + "' không có trên hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            else
                            {
                                exitsData = "Mã hàng hóa '" + item.mp_ProductCodeUser + "' không có trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }

                        var listProductCode_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => CUtils.StrValue(item.ProductCode)).Distinct().ToList();
                        if (listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                        {
                            foreach (var item in listProductCode_Distinct)
                            {
                                var objReturnSupDtl = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                                if (objReturnSupDtl != null)
                                {
                                    var objInvF_InventoryReturnSupInstSerial = new Mst_ProductUI()
                                    {
                                        ProductCode = objReturnSupDtl.ProductCode,
                                        ProductCodeUser = objReturnSupDtl.ProductCodeUser,
                                        ProductCodeBase = objReturnSupDtl.mp_ProductCodeBase,
                                        ProductCodeRoot = objReturnSupDtl.ProductCodeRoot,
                                        ProductName = objReturnSupDtl.ProductName,
                                        UnitCode = objReturnSupDtl.UnitCode,
                                        ValConvert = objReturnSupDtl.ValConvert,
                                        FlagLot = objReturnSupDtl.mp_FlagLot,
                                        FlagSerial = objReturnSupDtl.mp_FlagSerial,
                                        UPIn = objReturnSupDtl.UPIN,
                                        UPReturnSup = objReturnSupDtl.UPReturnSup,
                                        InvCodeInActual = objReturnSupDtl.InvCodeOutActual
                                    };

                                    var strInvCodeOutActual = "";
                                    var fQty = 0.0;
                                    var listDtlCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                                    if (listDtlCur != null && listDtlCur.Count > 0)
                                    {
                                        fQty = listDtlCur.Count;
                                        objInvF_InventoryReturnSupInstSerial.Qty = fQty;
                                        var i = 0;

                                        foreach (var _it in listDtlCur)
                                        {

                                            //var objProduct_InvCodeOutActual = new InvF_InventoryReturnSupDtlUI()
                                            //{
                                            //    InvCodeOutActual = CUtils.StrValue(_it.InvCodeOutActual)
                                            //};
                                            objInvF_InventoryReturnSupInstSerial.ValReturnSup = fQty * CUtils.ConvertToDouble(_it.UPReturnSup);
                                            if (i != 0)
                                            {
                                                strInvCodeOutActual += ", ";
                                            }

                                            strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                            //objInvF_InventoryReturnSupInstSerial.InvCodeInActual = strInvCodeOutActual;
                                            i++;
                                        }
                                    }

                                    #region["Danh sach hang hoa cung base"]
                                    var strWhereCungBase = "";
                                    var sbSqlCungBase = new StringBuilder();
                                    if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSupInstSerial.ProductCodeBase))
                                    {
                                        sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objInvF_InventoryReturnSupInstSerial.ProductCodeBase), "=");
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
                                        foreach (var it2 in objRT_Mst_Product1.Lst_Mst_Product)
                                        {
                                            var productUI = new Mst_ProductUI();
                                            productUI.ProductCode = it2.ProductCode;
                                            productUI.ProductCodeUser = it2.ProductCodeUser;
                                            productUI.ProductCodeBase = it2.ProductCodeBase;
                                            productUI.ProductCodeRoot = it2.ProductCodeRoot;
                                            productUI.ProductName = it2.ProductName;
                                            productUI.UnitCode = it2.UnitCode;
                                            productUI.ValConvert = it2.ValConvert;
                                            productUI.FlagLot = it2.FlagLot;
                                            productUI.FlagSerial = it2.FlagSerial;
                                            productUI.ProductType = it2.ProductType;
                                            if (it2.FlagLot.Equals("1"))
                                            {
                                                productUI.FlagLo = "1";
                                            }
                                            if (it2.ProductType != null && it2.ProductType.ToString().ToUpper().Equals("COMBO") && it2.ValConvert != null)
                                            {
                                                productUI.FlagCombo = "1";
                                            }
                                            productUI.UPIn = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.UPReturnSup = it2.UPBuy == null ? 0 : it2.UPBuy;
                                            productUI.ValReturnSup = "0";
                                            #region["Lấy thông tin tồn kho của từng hàng hoá cùng base"]
                                            var strWhereClauseTonKho1 = "";
                                            var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                                            var sb_SQL13 = new StringBuilder();
                                            if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                                            {
                                                sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                                            }
                                            var InvBUPattern = invBUPattern;
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
                                                    productUI.Qty = qtymax1;
                                                }
                                            }
                                            lst_Mst_ProductUI.Add(productUI);
                                            objInvF_InventoryReturnSupInstSerial.lstUnitCodeUIByProduct = lst_Mst_ProductUI;
                                            #endregion
                                        }
                                    }

                                    lst_MstProductUI.Add(objInvF_InventoryReturnSupInstSerial);

                                    foreach (var item1 in lst_MstProductUI)
                                    {
                                        var productCodecur = item1.ProductCode;
                                        var listUI = item1.lstUnitCodeUIByProduct;
                                        var listobjUI = listUI.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(productCodecur)).ToList();
                                        item1.QtyTotalOK = listobjUI[0].QtyTotalOK;
                                    }

                                    #endregion
                                }
                            }
                        }


                        #endregion
                        return Json(new { Success = true, listDtlImport = listDtlImport, list_MstProductUI = lst_MstProductUI });

                    }
                    else
                    {
                        exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                }
                catch (ClientException ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
            }
            return Json(resultEntry);
        }



        private List<Inv_InventoryBalanceSerial> GetListInventoryBalanceSerial(string productCodeBase, string invBUPattern)
        {
            var strWhere = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
            var Tbl_Mst_Inventory = "Mst_Inventory.";
            if (!string.IsNullOrEmpty(productCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "in");
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
            var rt = InvInventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(rq, CUtils.StrValue(UserState.AddressAPIs));
            if (rt != null && rt.Lst_Inv_InventoryBalanceSerial != null)
            {
                lstInv_InventoryBalanceSerial = rt.Lst_Inv_InventoryBalanceSerial;
            }
            return lstInv_InventoryBalanceSerial;
        }
        #endregion

        #region["DicColums"]
        //private Dictionary<string, string> GetImportDicColumsTemplateProduct()
        //{
        //    return new Dictionary<string, string>()
        //    {
        //         {"ProductCodeUser","Mã hàng hóa"},
        //         {"Qty","Số lượng"},
        //         {"InvCodeInActual","Vị trí"},
        //    };
        //}
        #endregion
    }
}