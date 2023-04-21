using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.ClientService.Services.DMS;
using idn.Skycic.Inventory.ClientService.Services.Inbrand;
using idn.Skycic.Inventory.ClientService.Services.Veloca;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.DMS;
using idn.Skycic.Inventory.Common.Models.Veloca;
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
    public class InvF_InventoryOutController : AdminBaseController
    {
        // GET: InvF_InventoryOut
        //public ActionResult Index(string IF_InvOutNo, string InvOutType, string CustomerCode, string createdtimefrom, string createdtimeto, string approvedtimefrom, string approvedtimeto, string InvCodeOut, string ProductCode, string pending, string approved, string canceled, int page = 0, int recordcount = 10, string init = "init")
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
        //        #region Loại xuất kho
        //        var lst_Mst_InvOutType = new List<Mst_InvOutType>();
        //        var objRQ_Mst_InventoryOutType = new RQ_Mst_InvOutType()
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
        //            Rt_Cols_Mst_InvOutType = "*",
        //            Ft_RecordStart = Ft_RecordStart,
        //            Ft_RecordCount = Ft_RecordCount
        //        };
        //        var rt = MstInvOutTypeService.Instance.WA_Mst_InventoryOutType_Get(objRQ_Mst_InventoryOutType, addressAPIs);
        //        if (rt != null && rt.Lst_Mst_InvOutType != null)
        //        {
        //            lst_Mst_InvOutType = rt.Lst_Mst_InvOutType;
        //        }
        //        ViewBag.Lst_Mst_InvOutType = lst_Mst_InvOutType;
        //        #endregion

        //        #region Thông tin kho xuất                
        //        ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs); 
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
        //            Ft_RecordCount = Ft_RecordCount
        //        };
        //        var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
        //        if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
        //        {
        //            lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
        //        }
        //        ViewBag.Lst_MstCustomer = lstCustomer;
        //        #endregion
        //        var pageInfo = new PageInfo<InvF_InventoryOut>(0, recordcount)
        //        {
        //            DataList = new List<InvF_InventoryOut>()
        //        };
        //        InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut
        //        {
        //            IF_InvOutNo = IF_InvOutNo,
        //            InvOutType = InvOutType,
        //            CustomerCode = CustomerCode,
        //            InvCodeOut = InvCodeOut
        //        };
        //        var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto, pending, approved, canceled, ProductCode);
        //        var objRQ_InvF_InventoryOut = new RQ_InvF_InventoryOut()
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
        //            Rt_Cols_InvF_InventoryOut = "*",
        //            Ft_WhereClause = strWhere
        //        };
        //        var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);

        //        if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
        //        {
        //            var listInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut;
        //            ProcessRefNo(ref listInvF_InventoryOut);
        //            pageInfo.DataList = listInvF_InventoryOut;
        //            itemCount = objRT_InvF_InventoryOut.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryOut.MySummaryTable.MyCount);
        //            pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
        //        }

        //        pageInfo.PageSize = recordcount;
        //        pageInfo.PageIndex = page;
        //        pageInfo.ItemCount = itemCount;
        //        pageInfo.PageCount = pageCount;
        //        ViewBag.StartCount = (page * recordcount).ToString();
        //        if (!init.Equals("init"))
        //        {
        //            return JsonView("BindDataInFInventoryOut", pageInfo);
        //        }
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
        [HttpGet]
        public ActionResult Index(
            string IF_InvOutNo = "",
            string InvOutType = "",
            string CustomerCode = "",
            string createdtimefrom = "",
            string createdtimeto = "",
            string approvedtimefrom = "",
            string approvedtimeto = "",
            string InvCodeOut = "",
            string ProductCode = "",
            string pending = "",
            string approved = "",
            string canceled = "",
            int page = 0,
            int recordcount = 10,
            string init = "init")
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT");
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
                var pageInfo = new PageInfo<InvF_InventoryOut>(0, recordcount)
                {
                    DataList = new List<InvF_InventoryOut>()
                };
                #region["Loại phiếu xuất"]
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
                    Ft_RecordCount = Ft_RecordCount
                };
                var list_Mst_InventoryOutType = MstInvOutTypeService.Instance.WA_Mst_InventoryOutType_Get(objRQ_Mst_InventoryOutType, addressAPIs);
                if (list_Mst_InventoryOutType != null && list_Mst_InventoryOutType.Lst_Mst_InvOutType != null)
                {
                    lst_Mst_InvOutType = list_Mst_InventoryOutType.Lst_Mst_InvOutType;
                }
                ViewBag.Lst_Mst_InvOutType = lst_Mst_InvOutType;

                #endregion

                #region["Kho xuất"]
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion
                #region["Thông tin khách hàng"]
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
                    Ft_RecordCount = Ft_RecordCount
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
                {
                    lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
                }
                ViewBag.Lst_MstCustomer = lstCustomer;
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
             string IF_InvOutNo = "",
            string InvOutType = "",
            string CustomerCode = "",
            string createdtimefrom = "",
            string createdtimeto = "",
            string approvedtimefrom = "",
            string approvedtimeto = "",
            string InvCodeOut = "",
            string profileStatus = "",
            string refNo = "",
            string pending = "",
            string approved = "",
            string canceled = "",
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
            var pageInfo = new PageInfo<InvF_InventoryOut>(0, recordcount)
            {
                DataList = new List<InvF_InventoryOut>()
            };
            //var listInvF_InventoryOut = new List<InvF_InventoryOut>();
            try
            {
                #region["Search"]
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut
                {
                    IF_InvOutNo = IF_InvOutNo,
                    InvOutType = InvOutType,
                    CustomerCode = CustomerCode,
                    InvCodeOut = InvCodeOut,
                    RefNo = refNo
                };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto, pending, approved, canceled, profileStatus);
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Rt_Cols_InvF_InventoryOut = "*",
                    Ft_WhereClause = strWhere
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
                {
                    var listInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut;
                    ProcessRefNo(ref listInvF_InventoryOut);
                    pageInfo.DataList = listInvF_InventoryOut;
                    itemCount = objRT_InvF_InventoryOut.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InventoryOut.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("BindDataInFInventoryOut", pageInfo);
                #endregion
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }



        [HttpGet]
        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            try
            {
                //Ft_RecordCount = "10";
                // Sinh số phiếu xuất
                Seq_Common objSeq_Common = new Seq_Common();
                objSeq_Common.Param_Postfix = "";
                objSeq_Common.Param_Prefix = "";
                objSeq_Common.SequenceType = SeqType.IFInvOutNo;
                var IFInvOutNo = SeqNo(objSeq_Common);
                ViewBag.IFInvOutNo = IFInvOutNo;

                #region ["UserState Common Info"]
                var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
                var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
                var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
                var orgId = UserState.Mst_NNT.OrgID;
                var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
                #endregion
                #region Loại xuất kho                
                ViewBag.Lst_Mst_InvOutType = Mst_InvOutTypeGetAllActive(addressAPIs);
                #endregion

                #region Thông tin kho xuất                
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

                #region Thông tin khách hàng                
                ViewBag.Lst_MstCustomer = Mst_Customer_GetActive(addressAPIs, Ft_RecordCount);
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

                //var itds = lst_Mst_Product.Where(x => x.ProductCode.Equals("0A300010000GHRI")).FirstOrDefault();
                //ViewBag.Lst_Mst_Product = lst_Mst_Product;

                var lst_Mst_Product = GetProductProductSale(addressAPIs, "10");
                #endregion

                #region Lấy thông tin tồn kho               

                var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                foreach (var it in lst_Mst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
                    productUI.ProductCodeUser = it.ProductCodeUser;
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
                    productUI.SellPrice = it.UPBuy == null ? 0 : it.UPBuy;
                    //

                    lst_Mst_ProductUI.Add(productUI);
                }

                ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                return View();
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
            return JsonViewError("Index", null, resultEntry);
        }
        public ActionResult Save(string model, string flagIsDelete = "0")
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
                var objRQ_InvF_InventoryOut = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryOut>(model);

                var objRQ_InvF_InventoryOutCur = new RQ_InvF_InventoryOut();




                var objInvF_InventoryOut = objRQ_InvF_InventoryOut.InvF_InventoryOut;
                objInvF_InventoryOut.OrgID = orgId;
                objInvF_InventoryOut.NetworkID = networkID;
                //objInvF_InventoryOut.OrderType = "";
                objInvF_InventoryOut.UseReceive = "";


                //if(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null && objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover.Count > 0)
                //{

                //}


                //Thêm 1 số thông tin khác
                objRQ_InvF_InventoryOut.Tid = GetNextTId();
                objRQ_InvF_InventoryOut.GwUserCode = GwUserCode;
                objRQ_InvF_InventoryOut.GwPassword = GwPassword;
                objRQ_InvF_InventoryOut.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InventoryOut.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InventoryOut.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InventoryOut.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InventoryOut.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InventoryOut.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_InventoryOut.FlagIsDelete = flagIsDelete;
                //

                #region["Xử lý file"]
                var listInvF_InventoryOutAttachFile = new List<InvF_InventoryOutAttachFile>();


                if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile != null && objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile.Count > 0)
                {
                    foreach (var item in objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile)
                    {
                        if (CUtils.StrValue(item.FlagIsFilePath).Equals("0"))
                        {
                            string filePath = Server.MapPath(FileUtils.GetTempFileVirtualPath(CUtils.StrValue(item.AttachFileSpec), ""));
                            var contractFileSpec = CUtils.ConvertFileToBase64String(filePath);
                            item.AttachFileSpec = contractFileSpec;

                            //var objInvF_InventoryOutAttachFile = new InvF_InventoryOutAttachFile()
                            //{
                            //    Idx = CUtils.StrValue(item.Idx),
                            //    IF_InvOutNo = CUtils.StrValue(item.IF_InvOutNo),
                            //    AttachFileSpec = contractFileSpec,
                            //    FlagIsFilePath = CUtils.StrValue(item.FlagIsFilePath),
                            //    AttachFileName = CUtils.StrValue(item.AttachFileName),
                            //    AttachFileDesc = CUtils.StrValue(item.AttachFileDesc)
                            //};
                            //listInvF_InventoryOutAttachFile.Add(objInvF_InventoryOutAttachFile);
                        }
                    }
                }
                //objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile.AddRange(listInvF_InventoryOutAttachFile);

                #endregion

                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                InvF_InventoryOutService.Instance.InvF_InventoryOut_Save(objRQ_InvF_InventoryOut, addressAPIs);
                resultEntry.Success = true;
                if (flagIsDelete == "1")
                {
                    resultEntry.AddMessage("Xóa phiếu xuất kho thành công!");
                }
                else
                {
                    resultEntry.AddMessage("Lưu phiếu xuất kho thành công!");
                }
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult UpdProfile(string model)
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
                var objRQ_InvF_InventoryOut = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryOut>(model);

                var objRQ_InvF_InventoryOutCur = new RQ_InvF_InventoryOut();




                var objInvF_InventoryOut = objRQ_InvF_InventoryOut.InvF_InventoryOut;
                objInvF_InventoryOut.OrgID = orgId;
                objInvF_InventoryOut.NetworkID = networkID;
                //objInvF_InventoryOut.OrderType = "";
                objInvF_InventoryOut.UseReceive = "";


                //if(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null && objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover.Count > 0)
                //{

                //}


                //Thêm 1 số thông tin khác
                objRQ_InvF_InventoryOut.Tid = GetNextTId();
                objRQ_InvF_InventoryOut.GwUserCode = GwUserCode;
                objRQ_InvF_InventoryOut.GwPassword = GwPassword;
                objRQ_InvF_InventoryOut.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InventoryOut.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InventoryOut.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InventoryOut.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InventoryOut.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InventoryOut.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_InventoryOut.FlagIsDelete = "0";
                //

                #region["Xử lý file"]
                var listInvF_InventoryOutAttachFile = new List<InvF_InventoryOutAttachFile>();


                if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile != null && objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile.Count > 0)
                {
                    foreach (var item in objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile)
                    {
                        if (CUtils.StrValue(item.FlagIsFilePath).Equals("0"))
                        {
                            string filePath = Server.MapPath(FileUtils.GetTempFileVirtualPath(CUtils.StrValue(item.AttachFileSpec), ""));
                            var contractFileSpec = CUtils.ConvertFileToBase64String(filePath);
                            item.AttachFileSpec = contractFileSpec;

                            //var objInvF_InventoryOutAttachFile = new InvF_InventoryOutAttachFile()
                            //{
                            //    Idx = CUtils.StrValue(item.Idx),
                            //    IF_InvOutNo = CUtils.StrValue(item.IF_InvOutNo),
                            //    AttachFileSpec = contractFileSpec,
                            //    FlagIsFilePath = CUtils.StrValue(item.FlagIsFilePath),
                            //    AttachFileName = CUtils.StrValue(item.AttachFileName),
                            //    AttachFileDesc = CUtils.StrValue(item.AttachFileDesc)
                            //};
                            //listInvF_InventoryOutAttachFile.Add(objInvF_InventoryOutAttachFile);
                        }
                    }
                }
                //objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile.AddRange(listInvF_InventoryOutAttachFile);

                #endregion

                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_UpdProfile(objRQ_InvF_InventoryOut, addressAPIs);
                resultEntry.Success = true;
                //if (flagIsDelete == "1")
                //{
                //    resultEntry.AddMessage("Xóa phiếu xuất kho thành công!");
                //}
                //else
                //{
                resultEntry.AddMessage("Cập nhật hồ sơ phiếu xuất kho thành công!");
                //}
                //resultEntry.RedirectUrl = Url.Action("Index");
                resultEntry.RedirectUrl = Url.Action("Detail", "InvF_InventoryOut", new { IF_InvOutNo = CUtils.StrValue(objInvF_InventoryOut.IF_InvOutNo) });
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }


        public ActionResult DeleteMulti(string lstInvF_InventoryOut)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVENTORYOUT_DELETE");
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
                var listInvF_InventoryOut = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOut>>(lstInvF_InventoryOut);
                if (listInvF_InventoryOut == null || listInvF_InventoryOut.Count == 0)
                {
                    resultEntry.Success = false;
                    resultEntry.AddMessage("Không tồn tại danh sách cần xóa !");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                var objRQ_InvF_InventoryOut = new RQ_InvF_InventoryOut();
                //Thêm 1 số thông tin khác
                objRQ_InvF_InventoryOut.Tid = GetNextTId();
                objRQ_InvF_InventoryOut.GwUserCode = GwUserCode;
                objRQ_InvF_InventoryOut.GwPassword = GwPassword;
                objRQ_InvF_InventoryOut.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InventoryOut.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InventoryOut.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InventoryOut.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InventoryOut.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InventoryOut.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_InventoryOut.FlagIsDelete = "1";
                //

                foreach (var objInvF_InventoryOut in listInvF_InventoryOut)
                {
                    objRQ_InvF_InventoryOut.InvF_InventoryOut = objInvF_InventoryOut;
                    objRQ_InvF_InventoryOut.InvF_InventoryOut.OrgID = orgId;
                    objRQ_InvF_InventoryOut.InvF_InventoryOut.NetworkID = networkID;
                    objRQ_InvF_InventoryOut.InvF_InventoryOut.UseReceive = "";

                    InvF_InventoryOutService.Instance.InvF_InventoryOut_Save(objRQ_InvF_InventoryOut, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa phiếu xuất kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }
        [HttpGet]
        public ActionResult Update(string IF_InvOutNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.UserState = UserState;
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = UserState;
            #endregion
            try
            {
                //Ft_RecordCount = "10";
                #region Loại xuất kho               
                ViewBag.Lst_Mst_InvOutType = Mst_InvOutTypeGetAllActive(addressAPIs);
                #endregion

                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

                #region Thông tin khách hàng                                
                ViewBag.Lst_MstCustomer = Mst_Customer_GetActive(addressAPIs, Ft_RecordCount);
                #endregion

                #region Thông tin hàng hóa                
                //var lst_Mst_Product = GetProductProductSale(addressAPIs, "10");
                //var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                //foreach (var it in lst_Mst_Product)
                //{
                //    var productUI = new Mst_ProductUI();
                //    productUI.ProductCode = it.ProductCode;
                //    productUI.ProductName = it.ProductName;
                //    productUI.UnitCode = it.UnitCode;
                //    productUI.ProductCodeBase = it.ProductCodeBase;
                //    productUI.ProductCodeUser = it.ProductCodeUser;
                //    productUI.ProductCodeRoot = it.ProductCodeRoot;
                //    productUI.ValConvert = it.ValConvert;
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
                //    productUI.SellPrice = it.UPSell == null ? 0 : it.UPSell;
                //    //                    
                //    lst_Mst_ProductUI.Add(productUI);
                //}
                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                #endregion

                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Rt_Cols_InvF_InventoryOutAttachFile = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count != 0)
                {
                    objInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                    if (CUtils.StrValue(objInvF_InventoryOut.RefType) == RefType.RO)
                    {
                        objInvF_InventoryOut.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryOut.RefNo);
                    }
                }
                var orderNo = objInvF_InventoryOut.OrderNo == null ? "" : objInvF_InventoryOut.OrderNo.ToString();

                #region Thông tin hàng hóa
                //ViewBag.Lst_Mst_ProductUI = getListProduct(orderNo);
                #endregion

                #region Get List đơn vị tính theo Product 
                var Lst_InvF_InventoryOutCover = new List<InvF_InventoryOutCover>();
                if (objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null)
                {
                    Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                }
                foreach (var it in Lst_InvF_InventoryOutCover)
                {
                    #region Lấy danh sách đơn vị theo Product
                    var strWhereProduct = "";
                    var sbSql = new StringBuilder();
                    var Tbl_Mst_Product = "Mst_Product.";
                    if (it.mp_root_ProductCodeBase != null && it.mp_root_ProductCodeBase.ToString() != "")
                    {
                        sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_root_ProductCodeBase.ToString()), "=");
                    }
                    //if (it.ProductCodeRoot != null && it.ProductCodeRoot.ToString() != "")
                    //{
                    //    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeRoot", CUtils.StrValue(it.ProductCodeRoot), "=");
                    //}
                    strWhereProduct = sbSql.ToString();
                    var rqProduct = new RQ_Mst_Product()
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
                        Ft_WhereClause = strWhereProduct,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Mst_Product = "*"
                    };
                    var lsMst_Product = new List<Mst_Product>();
                    var rt_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(rqProduct, addressAPIs);
                    if (rt_Mst_Product != null && rt_Mst_Product.Lst_Mst_Product != null)
                    {
                        lsMst_Product = rt_Mst_Product.Lst_Mst_Product;
                    }
                    #endregion
                    #region Lấy thông tin tồn kho


                    var lst_MstProductUI = new List<Mst_ProductUI>();
                    foreach (var item in lsMst_Product)
                    {
                        var productUI = new Mst_ProductUI();
                        productUI.ProductCode = item.ProductCode;
                        productUI.ProductName = item.ProductName;
                        productUI.UnitCode = item.UnitCode;
                        productUI.ProductCodeUser = item.ProductCodeUser;
                        productUI.ProductCodeBase = item.ProductCodeBase;
                        productUI.ProductCodeRoot = item.ProductCodeRoot;
                        productUI.ValConvert = item.ValConvert;

                        if (item.FlagSerial.Equals("1"))
                        {
                            productUI.FlagSerial = "1";
                        }
                        if (item.FlagLot.Equals("1"))
                        {
                            productUI.FlagLo = "1";
                        }
                        if (item.ProductType != null && item.ProductType.ToString().ToUpper().Equals("COMBO") && item.ValConvert != null)
                        {
                            productUI.FlagCombo = "1";
                        }
                        // Check theo đơn hàng hay không
                        productUI.SellPrice = item.UPSell == null ? 0 : item.UPSell;
                        //
                        //if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                        //{
                        //    var lstProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode)).ToList();
                        //    if(lstProduct != null && lstProduct.Count != 0)
                        //    {
                        //        var qtyTotalOkMax = lstProduct.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        //        var itemBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                        //        if (itemBalance != null)
                        //        {
                        //            productUI.InvCode = itemBalance.InvCode;
                        //            productUI.QtyTotalOK = qtyTotalOkMax;
                        //            productUI.DiscountPrice = "0"; // Tạm để
                        //        }
                        //    }                           
                        //}
                        #region["Lấy thông tin hàng hoá cùng base "]
                        var qtytotalok1 = 0.0;
                        var invCodeMax = "";
                        var invCodeMax1 = "";
                        var qtymax = 0.0;
                        var strWhereClauseTonKho1 = "";
                        var tbMst_Inventory = "Mst_Inventory.";
                        var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                        var sb_SQL13 = new StringBuilder();
                        if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                        {
                            sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                        }


                        var invCodeOut = "I." + objInvF_InventoryOut.InvCodeOut + "%";
                        if (objInvF_InventoryOut.InvCodeOut != null && objInvF_InventoryOut.InvCodeOut != "")
                        {
                            sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", invCodeOut, "like");
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
                                var objInv_InventoryBalanceTonKho1 = lstInv_InventoryBalanceTonKho1.Where(_it => _it.ProductCode.Equals(productUI.ProductCode)).FirstOrDefault();
                                if (objInv_InventoryBalanceTonKho1 != null)
                                {
                                    productUI.UPInv = objInv_InventoryBalanceTonKho1.UPInv == null ? 0 : objInv_InventoryBalanceTonKho1.UPInv;
                                }
                                qtymax = lstInv_InventoryBalanceTonKho1.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
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
                                productUI.ValOUTAfterDesc = "0";
                                productUI.Qty = "1";

                            }
                        }

                        #endregion
                        lst_MstProductUI.Add(productUI);
                    }
                    #endregion
                    it.lstUnitCodeUIByProduct = lst_MstProductUI;
                }
                #endregion
                ViewBag.Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                //
                ViewBag.lstInvF_InventoryOutInstLot = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot;
                ViewBag.lstInvF_InventoryOutInstSerial = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial;
                ViewBag.lstInvF_InventoryOutDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl;
                ViewBag.lstInvF_InventoryOutQR = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutQR;
                ViewBag.lstInvF_InventoryOutAttachFile = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile;
                //
                return View(objRT_InvF_InventoryOut);
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
            //return View(new InvF_InventoryOut());            
            //return Json(resultEntry, JsonRequestBehavior.AllowGet);
            return View();
        }
        [HttpGet]
        public ActionResult Detail(string IF_InvOutNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT_DETAIL");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region Loại xuất kho               
                ViewBag.Lst_Mst_InvOutType = Mst_InvOutTypeGetAllActive(addressAPIs);
                #endregion

                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

                #region Thông tin khách hàng                                
                //ViewBag.Lst_MstCustomer = Mst_Customer_GetActive(addressAPIs, Ft_RecordCount);
                #endregion

                #region Thông tin hàng hóa                
                //var lst_Mst_Product = GetProductProductSale(addressAPIs, "10");
                //var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                //foreach (var it in lst_Mst_Product)
                //{
                //    var productUI = new Mst_ProductUI();
                //    productUI.ProductCode = it.ProductCode;
                //    productUI.ProductName = it.ProductName;
                //    productUI.UnitCode = it.UnitCode;
                //    productUI.ProductCodeBase = it.ProductCodeBase;
                //    productUI.ProductCodeUser = it.ProductCodeUser;
                //    productUI.ProductCodeRoot = it.ProductCodeRoot;
                //    productUI.ValConvert = it.ValConvert;
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
                //    productUI.SellPrice = it.UPSell == null ? 0 : it.UPSell;
                //    //                    
                //    lst_Mst_ProductUI.Add(productUI);
                //}
                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                #endregion

                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Rt_Cols_InvF_InventoryOutAttachFile = "*",
                    Ft_WhereClause = strWhere
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count != 0)
                {
                    objInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                    if (CUtils.StrValue(objInvF_InventoryOut.RefType) == RefType.RO)
                    {
                        objInvF_InventoryOut.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryOut.RefNo);
                    }
                }
                var orderNo = objInvF_InventoryOut.OrderNo == null ? "" : objInvF_InventoryOut.OrderNo.ToString();

                #region Thông tin hàng hóa
                //ViewBag.Lst_Mst_ProductUI = getListProduct(orderNo);
                #endregion

                #region Get List đơn vị tính theo Product 
                var Lst_InvF_InventoryOutCover = new List<InvF_InventoryOutCover>();
                if (objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null)
                {
                    Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                }
                foreach (var it in Lst_InvF_InventoryOutCover)
                {
                    #region Lấy danh sách đơn vị theo Product
                    var strWhereProduct = "";
                    var sbSql = new StringBuilder();
                    var Tbl_Mst_Product = "Mst_Product.";
                    if (it.mp_root_ProductCodeBase != null && it.mp_root_ProductCodeBase.ToString() != "")
                    {
                        sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_root_ProductCodeBase.ToString()), "=");
                    }
                    //if (it.ProductCodeRoot != null && it.ProductCodeRoot.ToString() != "")
                    //{
                    //    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeRoot", CUtils.StrValue(it.ProductCodeRoot), "=");
                    //}
                    strWhereProduct = sbSql.ToString();
                    var rqProduct = new RQ_Mst_Product()
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
                        Ft_WhereClause = strWhereProduct,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Mst_Product = "*"
                    };
                    var lsMst_Product = new List<Mst_Product>();
                    var rt_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(rqProduct, addressAPIs);
                    if (rt_Mst_Product != null && rt_Mst_Product.Lst_Mst_Product != null)
                    {
                        lsMst_Product = rt_Mst_Product.Lst_Mst_Product;
                    }
                    #endregion
                    #region Lấy thông tin tồn kho


                    var lst_MstProductUI = new List<Mst_ProductUI>();
                    foreach (var item in lsMst_Product)
                    {
                        var productUI = new Mst_ProductUI();
                        productUI.ProductCode = item.ProductCode;
                        productUI.ProductName = item.ProductName;
                        productUI.UnitCode = item.UnitCode;
                        productUI.ProductCodeUser = item.ProductCodeUser;
                        productUI.ProductCodeBase = item.ProductCodeBase;
                        productUI.ProductCodeRoot = item.ProductCodeRoot;
                        productUI.ValConvert = item.ValConvert;

                        if (item.FlagSerial.Equals("1"))
                        {
                            productUI.FlagSerial = "1";
                        }
                        if (item.FlagLot.Equals("1"))
                        {
                            productUI.FlagLo = "1";
                        }
                        if (item.ProductType != null && item.ProductType.ToString().ToUpper().Equals("COMBO") && item.ValConvert != null)
                        {
                            productUI.FlagCombo = "1";
                        }
                        // Check theo đơn hàng hay không
                        productUI.SellPrice = item.UPSell == null ? 0 : item.UPSell;
                        //
                        //if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                        //{
                        //    var lstProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode)).ToList();
                        //    if(lstProduct != null && lstProduct.Count != 0)
                        //    {
                        //        var qtyTotalOkMax = lstProduct.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        //        var itemBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                        //        if (itemBalance != null)
                        //        {
                        //            productUI.InvCode = itemBalance.InvCode;
                        //            productUI.QtyTotalOK = qtyTotalOkMax;
                        //            productUI.DiscountPrice = "0"; // Tạm để
                        //        }
                        //    }                           
                        //}
                        #region["Lấy thông tin hàng hoá cùng base "]
                        var qtytotalok1 = 0.0;
                        var invCodeMax = "";
                        var invCodeMax1 = "";
                        var qtymax = 0.0;
                        var strWhereClauseTonKho1 = "";
                        var tbMst_Inventory = "Mst_Inventory.";
                        var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                        var sb_SQL13 = new StringBuilder();
                        if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                        {
                            sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                        }


                        var invCodeOut = "I." + objInvF_InventoryOut.InvCodeOut + "%";
                        if (objInvF_InventoryOut.InvCodeOut != null && objInvF_InventoryOut.InvCodeOut != "")
                        {
                            sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", invCodeOut, "like");
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
                                qtymax = lstInv_InventoryBalanceTonKho1.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
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
                                productUI.ValOUTAfterDesc = "0";
                                productUI.Qty = "1";

                            }
                        }

                        #endregion
                        lst_MstProductUI.Add(productUI);
                    }
                    #endregion
                    it.lstUnitCodeUIByProduct = lst_MstProductUI;
                }
                #endregion
                ViewBag.Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                //
                ViewBag.lstInvF_InventoryOutInstLot = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot;
                ViewBag.lstInvF_InventoryOutInstSerial = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial;
                ViewBag.lstInvF_InventoryOutDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl;
                ViewBag.lstInvF_InventoryOutQR = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutQR;
                ViewBag.lstInvF_InventoryOutAttachFile = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile;
                //
                return View(objRT_InvF_InventoryOut);
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
            //return View(new InvF_InventoryOut());            
            return Json(resultEntry, JsonRequestBehavior.AllowGet);
        }



        public ActionResult EditFile(string IF_InvOutNo)
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
            try
            {
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Rt_Cols_InvF_InventoryOutAttachFile = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                return JsonView("ShowPopupProFile", objRT_InvF_InventoryOut);

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupCrCtCreditContract", null, resultEntry);
        }

        public List<Mst_ProductUI> getListProduct(string OrderNo)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            if (OrderNo == null || OrderNo == "")
            {
                #region Thông tin hàng hóa                
                var lst_Mst_Product = GetProductProductSale(addressAPIs, "10");

                foreach (var it in lst_Mst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
                    productUI.ProductCodeUser = it.ProductCodeUser;
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
                    lst_Mst_ProductUI.Add(productUI);
                }
                #endregion
            }
            else
            {
                #region Kết nối DMS
                var gwUserCode = OS_DMS_GwUserCode;
                var gwPassword = OS_DMS_GwPassword;
                if (ServiceAddress.BaseAPIDMSSolution == null || ServiceAddress.BaseAPIDMSSolution == "")
                {
                    var strNetWorkUrl = "";
                    string strMstSvUrl_DMS = System.Configuration.ConfigurationManager.AppSettings["DMSMstSv"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["DMSMstSv"];

                    MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

                    /////
                    RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                    {
                        Tid = GetNextTId(),
                        TokenID = strMstSvUrl_DMS,
                        NetworkID = networkID,
                        OrgID = orgId.ToString(),
                        GwUserCode = gwUserCode,
                        GwPassword = gwPassword,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword
                    };
                    var objRT_MstSv_Sys_User = OS_MstSv_InBrandServices.Instance.WA_OS_MstSv_InBrand_MstSv_Sys_User_Login(strMstSvUrl_DMS, objRQ_MstSv_Sys_User);
                    strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                    if (strNetWorkUrl != null && strNetWorkUrl != "")
                    {
                        ServiceAddress.BaseAPIDMSSolution = strNetWorkUrl;
                    }
                }
                var lstRpt_OrderSummary_TotalProductForInv = new List<Rpt_OrderSummary_TotalProductForInv>();

                if (ServiceAddress.BaseAPIDMSSolution != null && ServiceAddress.BaseAPIDMSSolution != "")
                {
                    var objRQ_Rpt_OrderSummary_TotalProductForInv = new RQ_Rpt_OrderSummary_TotalProductForInv()
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
                        Rpt_OrderSummary_TotalProductForInv = new Rpt_OrderSummary_TotalProductForInv()
                        {
                            OrderNo = OrderNo
                        }
                    };
                    var objRT_Rpt_OrderSummary_TotalProductForInv = DMS_ReportService.Instance.WA_Rpt_OrderSummary_TotalProductForInv(objRQ_Rpt_OrderSummary_TotalProductForInv, ServiceAddress.BaseAPIDMSSolution);
                    if (objRT_Rpt_OrderSummary_TotalProductForInv != null && objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv != null)
                    {
                        lstRpt_OrderSummary_TotalProductForInv = objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv;
                        if (lstRpt_OrderSummary_TotalProductForInv != null && lstRpt_OrderSummary_TotalProductForInv.Count != 0)
                        {
                            // Lấy thêm các thông tin từ Product
                            var strProductCode = "";
                            var lstProductCodeSys = new List<string>();
                            foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                            {
                                var productcode = it.ProductCode.ToString();
                                if (!lstProductCodeSys.Contains(productcode))
                                {
                                    lstProductCodeSys.Add(productcode);
                                    if (strProductCode == "")
                                    {
                                        strProductCode += productcode;
                                    }
                                    else
                                    {
                                        strProductCode += "," + productcode;
                                    }
                                }
                            }
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
                                    foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                                    {
                                        // Thêm các thông tin từ MstProduct
                                        var productCode = it.ProductCode == null ? "" : it.ProductCode.ToString();
                                        var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                                        if (itMstProduct != null)
                                        {
                                            var productUI = new Mst_ProductUI();
                                            productUI.ProductCode = itMstProduct.ProductCode;
                                            productUI.ProductName = itMstProduct.ProductName;
                                            productUI.UnitCode = itMstProduct.UnitCode;
                                            productUI.ProductCodeBase = itMstProduct.ProductCodeBase;
                                            productUI.ProductCodeRoot = itMstProduct.ProductCodeRoot;
                                            productUI.ProductCodeUser = itMstProduct.ProductCodeUser;
                                            productUI.ValConvert = itMstProduct.ValConvert;
                                            if (itMstProduct.FlagSerial.Equals("1"))
                                            {
                                                productUI.FlagSerial = "1";
                                            }
                                            if (itMstProduct.FlagLot.Equals("1"))
                                            {
                                                productUI.FlagLo = "1";
                                            }
                                            if (itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") && it.ValConvert != null)
                                            {
                                                productUI.FlagCombo = "1";
                                            }
                                            // Check theo đơn hàng hay không
                                            productUI.SellPrice = itMstProduct.UPSell == null ? 0 : itMstProduct.UPSell;
                                            //                    
                                            lst_Mst_ProductUI.Add(productUI);
                                        }
                                        //    
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }
            return lst_Mst_ProductUI;
        }
        public ActionResult Approve(/*string IF_InvOutNo, */string lstIF_InvOutNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVENTORYOUT_APPROVE");
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
                var listIF_InvOutNo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOut>>(lstIF_InvOutNo);
                var objInvF_InventoryOut = new InvF_InventoryOut()
                {
                    OrgID = orgId
                };
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
                    InvF_InventoryOut = objInvF_InventoryOut
                };
                foreach (var item in listIF_InvOutNo)
                {
                    objInvF_InventoryOut.IF_InvOutNo = CUtils.StrValue(item.IF_InvOutNo);
                    objInvF_InventoryOut.Remark = CUtils.StrValue(item.Remark);
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                    InvF_InventoryOutService.Instance.InvF_InventoryOut_Appr(objRQ_InvF_InventoryOut, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt phiếu xuất kho thành công!");
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
        public ActionResult Cancel(string lstIF_InvOutNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "BTN_INVENTORYOUT_CANCEL");
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
                var listIF_InvOutNo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOut>>(lstIF_InvOutNo);
                var objInvF_InventoryOut = new InvF_InventoryOut()
                {
                    OrgID = orgId
                };
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
                    InvF_InventoryOut = objInvF_InventoryOut
                };
                foreach (var item in listIF_InvOutNo)
                {
                    objInvF_InventoryOut.IF_InvOutNo = CUtils.StrValue(item.IF_InvOutNo);
                    objInvF_InventoryOut.Remark = CUtils.StrValue(item.Remark);
                    InvF_InventoryOutService.Instance.InvF_InventoryOut_Cancel(objRQ_InvF_InventoryOut, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Hủy phiếu xuất kho thành công!");
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
        public ActionResult Serial(string IF_InvOutNo, string ProductCode/*, string productCodeBase*/, string invBUPattern, string ProductCodeUser, string ProductName)
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
                ViewBag.ProductCodeUser = ProductCodeUser;
                ViewBag.ProductName = ProductName;
                #region Lấy thông tin đổ ra select
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(ProductCode), "=");
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
                #endregion

                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut()
                {
                    IF_InvOutNo = IF_InvOutNo
                };
                var strWhereInventoryOut = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOut = "*",
                    Ft_WhereClause = strWhereInventoryOut
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                var Lst_InvF_InventoryOutInstSerial = new List<InvF_InventoryOutInstSerial>();
                if (objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial.Count != 0)
                {
                    Lst_InvF_InventoryOutInstSerial = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial.Where(x => x.ProductCodeRoot.Equals(ProductCode)).ToList();
                }
                var iF_InvOutStatus = "";
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null)
                {
                    var objinvInventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                    iF_InvOutStatus = objinvInventoryOut.IF_InvOutStatus.ToString();

                }
                ViewBag.IF_InvOutStatus = iF_InvOutStatus;

                ViewBag.ProductCode = ProductCode;
                ViewBag.lstInv_InventoryBalanceSerial = lstInv_InventoryBalanceSerial;
                ViewBag.Lst_InvF_InventoryOutInstSerial = Lst_InvF_InventoryOutInstSerial;
                ViewBag.ProductCodeUser = ProductCodeUser;
                return JsonView("Serial", Lst_InvF_InventoryOutInstSerial);
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
        public ActionResult ShowLoDetail(string IF_InvOutNo, string ProductCode, string ProductCodeBase, string invBUPattern, string valconvert, string listLot, string ViewType, string viewonly = "0")
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
                List<InvF_InventoryOutInstLot> lst_InvF_InventoryOutInstLotCur = new List<InvF_InventoryOutInstLot>();
                if (listLot != null && listLot.Length > 0)
                {
                    lst_InvF_InventoryOutInstLotCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOutInstLot>>(listLot);
                }

                #region Lấy thông tin đổ ra select
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(ProductCodeBase), "=");
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

                //Tính lại tồn kho theo giá trị quy đổi
                double curValConvert = 1;
                if (!string.IsNullOrEmpty(valconvert))
                {
                    curValConvert = Convert.ToDouble(valconvert);
                }
                foreach (var lot in lstInv_InventoryBalanceLot)
                {
                    lot.QtyTotalOK = CUtils.IsNullOrEmpty(lot.QtyTotalOK) ? 0 : Math.Round(Convert.ToDouble(lot.QtyTotalOK) / curValConvert, 2);
                }

                //Gán QtyAvailOK theo Qty bảng cache hiện tại
                if (lst_InvF_InventoryOutInstLotCur.Count > 0)
                {
                    foreach (var lot in lstInv_InventoryBalanceLot)
                    {
                        var lotCur = lst_InvF_InventoryOutInstLotCur.Where(m => CUtils.StrValue(m.ProductCode).Equals(CUtils.StrValue(lot.ProductCode)) && CUtils.StrValue(m.ProductLotNo).Equals(CUtils.StrValue(lot.ProductLotNo))
                                        && CUtils.StrValue(m.InvCodeOutActual).Equals(CUtils.StrValue(lot.InvCode))).FirstOrDefault();
                        if (lotCur != null)
                        {
                            lot.QtyAvailOK = lotCur.Qty;
                            lot.LogLUBy = "1"; //Mượn tạm dùng cho view SL
                        }
                        else
                        {
                            lot.QtyAvailOK = 0;
                        }
                    }
                }

                #endregion

                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut()
                {
                    IF_InvOutNo = IF_InvOutNo
                };
                var strWhereInventoryOut = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOut = "*",
                    Ft_WhereClause = strWhereInventoryOut
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                var lst_InvF_InventoryOutInstLot = new List<InvF_InventoryOutInstLot>();
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot != null)
                {
                    lst_InvF_InventoryOutInstLot = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot.Where(x => x.ProductCodeRoot.Equals(ProductCode)).ToList();
                    if (lst_InvF_InventoryOutInstLotCur.Count > 0)
                    {
                        foreach (var lot in lst_InvF_InventoryOutInstLot)
                        {
                            var lotCur = lst_InvF_InventoryOutInstLotCur.Where(m => CUtils.StrValue(m.ProductCode).Equals(CUtils.StrValue(lot.ProductCode)) && CUtils.StrValue(m.ProductLotNo).Equals(CUtils.StrValue(lot.ProductLotNo)) && CUtils.StrValue(m.InvCodeOutActual).Equals(CUtils.StrValue(lot.InvCodeOutActual))).FirstOrDefault();
                            if (lotCur != null)
                            {
                                lot.Qty = lotCur.Qty;
                            }
                            else
                            {
                                lot.Qty = 0;
                            }
                        }
                    }
                }
                var iF_InvOutStatus = "";
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null)
                {
                    var objinvInventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                    iF_InvOutStatus = objinvInventoryOut.IF_InvOutStatus.ToString();

                }
                ViewBag.IF_InvOutStatus = iF_InvOutStatus;

                ViewBag.ProductCode = ProductCode;
                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                ViewBag.lst_InvF_InventoryOutInstLot = lst_InvF_InventoryOutInstLot;
                ViewBag.ViewOnly = viewonly;
                ViewBag.ViewType = ViewType;
                ViewBag.ProductCode = ProductCode;
                ViewBag.ProductCodeBase = ProductCodeBase;

                return JsonView("Lo", lst_InvF_InventoryOutInstLot);
                //return JsonView("../ModalCommon/Lo", lst_InvF_InventoryOutInstLot);
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
        public ActionResult ComboAppr(string productCode, string IF_InvOutNo/*, string productCodeBase*/, string productCodeUser, string productName)
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
                #region Thông tin phiếu xuất kho
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                var Lst_InvF_InventoryOutDtl = new List<InvF_InventoryOutDtl>();
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count != 0)
                {
                    Lst_InvF_InventoryOutDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl;
                }
                #endregion
                return JsonView("ComboAppr", Lst_InvF_InventoryOutDtl);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Combo", null, resultEntry);
        }

        #region ["strWhereClause"]

        public string WhereClauseInvF_InventoryOut(InvF_InventoryOut objInvF_InventoryOut, string strdatefrom, string strdateto, string strApprdatefrom, string strApprdateto, string pending, string approved, string canceled, string profileStatus)
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
            var Tbl_InvF_InventoryOut = TableName.InvF_InventoryOut + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryOut.IF_InvOutNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.IF_InvOutNo, CUtils.StrValue(objInvF_InventoryOut.IF_InvOutNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryOut.RefNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.RefNo, CUtils.StrValue(objInvF_InventoryOut.RefNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(datefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(dateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdatefrom))
            {
                var apprDTimeUTCFrom = CUtils.StrValue(apprdatefrom) + " 00:00:00";
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.ApprDTimeUTC, apprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdateto))
            {
                var apprDTimeUTCTo = CUtils.StrValue(apprdateto) + " 23:59:59";
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.ApprDTimeUTC, apprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryOut.InvOutType))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.InvOutType, CUtils.StrValue(objInvF_InventoryOut.InvOutType), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryOut.CustomerCode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.CustomerCode, CUtils.StrValue(objInvF_InventoryOut.CustomerCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(objInvF_InventoryOut.InvCodeOut))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.InvCodeOut, CUtils.StrValue(objInvF_InventoryOut.InvCodeOut), "=");
            }
            //if (!CUtils.IsNullOrEmpty(productcode))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
            //}
            if (!CUtils.IsNullOrEmpty(profileStatus))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.ProfileStatus, CUtils.StrValue(profileStatus), "=");
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
                sbSql.AddWhereClause(Tbl_InvF_InventoryOut + TblInvF_InventoryOut.IF_InvOutStatus, status, "in");
            }

            strWhere = sbSql.ToString();
            return strWhere;
        }
        public string strWhereClause_RO_By_RONo(string rono)
        {
            var strWhereClause = "";
            var Tbl_Ser_RO = TableName.Ser_RO + ".";
            var sbSql = new StringBuilder();

            sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.FlagRORepair, "1", "=");
            sbSql.AddWhereClause(Tbl_Ser_RO + TblSer_RO.ROStatus, "CANCEL", "!=");
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
        #endregion



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult GetRefNoDetail(string RefNo, string RefType, string invBUPattern)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new List<Inv_InventoryBalance>();
            var lstInv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
            List<Inv_InventoryBalanceLotUI> lstInv_InventoryBalanceLotView = new List<Inv_InventoryBalanceLotUI>();
            var tbMst_Inventory = "Mst_Inventory.";
            var orderNoSys = "";
            var userCode = "";

            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            var addressDMSAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicDMS);
            var addressVelocaAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs_SkycicVeloca);
            var customerCodeSys = "";
            var roNoSys = "";
            #endregion
            try
            {
                if (RefType == Constants.RefType.RO)//Gọi veloca
                {
                    var strWhere = strWhereClause_RO_By_RONo(RefNo);
                    var objRQ_Ser_RO = new RQ_Ser_RO()
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
                        Rt_Cols_Ser_RO = "*",
                        Rt_Cols_Ser_ROAttachFile = "",
                        Rt_Cols_Ser_ROProductPart = "*",
                        Rt_Cols_Ser_ROProductService = "*",
                        Rt_Cols_Ser_ROReasonStop = "",
                        Rt_Cols_Ser_ROProductEngineer = "",
                        Ft_WhereClause = strWhere
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Ser_RO);
                    var objRT_Ser_RO = Ser_ROService.Instance.WA_Ser_RO_Get(objRQ_Ser_RO, addressVelocaAPIs);
                    var listSer_RO = new List<Ser_RO>();
                    var listSer_ROProductPart = new List<Ser_ROProductPart>();
                    var listSer_ROProductService = new List<Ser_ROProductService>();
                    var listSer_ROProductPartUI = new List<Ser_ROProductPartUI>();

                    if (objRT_Ser_RO != null)
                    {
                        if(objRT_Ser_RO.Lst_Ser_RO != null && objRT_Ser_RO.Lst_Ser_RO.Count > 0)
                        {
                            listSer_RO = objRT_Ser_RO.Lst_Ser_RO;
                            customerCodeSys = CUtils.StrValue(listSer_RO[0].CustomerCodeSys);
                            roNoSys = CUtils.StrValue(listSer_RO[0].RONoSys);
                        }

                        if (objRT_Ser_RO.Lst_Ser_ROProductPart != null && objRT_Ser_RO.Lst_Ser_ROProductPart.Count > 0)
                        {
                            listSer_ROProductPart = objRT_Ser_RO.Lst_Ser_ROProductPart;
                        }

                        if (listSer_ROProductPart != null && listSer_ROProductPart.Count > 0)
                        {
                            var strProductCode = "";
                            var lstProductCodeSys = new List<string>();
                            foreach (var it in listSer_ROProductPart)
                            {
                                var productcode = it.ProductCodePart.ToString();
                                if (!lstProductCodeSys.Contains(productcode))
                                {
                                    lstProductCodeSys.Add(productcode);
                                    if (strProductCode == "")
                                    {
                                        strProductCode += productcode;
                                    }
                                    else
                                    {
                                        strProductCode += "," + productcode;
                                    }
                                }
                            }

                            var tblMstProduct = TableName.Mst_Product + ".";
                            var sbSql = new StringBuilder();
                            if (strProductCode != "")
                            {
                                sbSql.AddWhereClause(tblMstProduct + TblMst_Product.ProductCode, strProductCode, "IN");
                            }
                            sbSql.AddWhereClause(tblMstProduct + TblMst_Product.FlagActive, "1", "=");
                            var strWhereClause = sbSql.ToString();
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
                                    foreach (var it in listSer_ROProductPart)
                                    {
                                        // Thêm các thông tin từ MstProduct
                                        var productCode = it.ProductCodePart == null ? "" : it.ProductCodePart.ToString();
                                        var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                                        if (itMstProduct != null)
                                        {
                                            var objSer_ROProductPartUI = new Ser_ROProductPartUI()
                                            {
                                                ProductCodeBase = itMstProduct.ProductCodeBase,
                                                ProductCodeRoot = itMstProduct.ProductCodeRoot,
                                                UnitCode = itMstProduct.UnitCode,
                                                ProductName = itMstProduct.ProductName,
                                                ProductCodeUser = itMstProduct.ProductCodeUser,
                                                ValConvert = itMstProduct.ValConvert,
                                                FlagSerial = itMstProduct.FlagSerial,
                                                FlagLo = itMstProduct.FlagLot,
                                                UPSell = itMstProduct.UPSell,
                                                QtyAppr = it.Qty,
                                                QtyInvOutAvail = it.Qty,
                                                QtyTotalOK = it.Qty,
                                                ProductCode = itMstProduct.ProductCode,
                                                CustomerCodeSys = customerCodeSys,
                                                OrderNoSys  = roNoSys,
                                            };
                                            if (itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") && itMstProduct.ValConvert != null)
                                            {
                                                objSer_ROProductPartUI.FlagCombo = "1";
                                            }
                                            else
                                            {
                                                objSer_ROProductPartUI.FlagCombo = "0";
                                            }
                                            listSer_ROProductPartUI.Add(objSer_ROProductPartUI);
                                        }
                                    
                                    }
                                  
                                }
                            }
                            #endregion

                        }
                    }

                    return Json(new { Success = true, LstRpt_OrderSummary_TotalProductForInv = listSer_ROProductPartUI });
                }
                else if (RefType == Constants.RefType.OrderDL || RefType == Constants.RefType.OrderSO || RefType == Constants.RefType.OrderSR) //DMS
                {
                    #region Kết nối DMS

                    var lstRpt_OrderSummary_TotalProductForInv = new List<Rpt_OrderSummary_TotalProductForInv>();

                    var objRQ_Rpt_OrderSummary_TotalProductForInv = new RQ_Rpt_OrderSummary_TotalProductForInv()
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
                        Rpt_OrderSummary_TotalProductForInv = new Rpt_OrderSummary_TotalProductForInv()
                        {
                            OrderNo = RefNo
                        }
                    };

                    var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objRQ_Rpt_OrderSummary_TotalProductForInv);
                    var objRT_Rpt_OrderSummary_TotalProductForInv = DMS_ReportService.Instance.WA_Rpt_OrderSummary_TotalProductForInv(objRQ_Rpt_OrderSummary_TotalProductForInv, addressDMSAPIs);
                    if (objRT_Rpt_OrderSummary_TotalProductForInv != null && objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv != null)
                    {
                        lstRpt_OrderSummary_TotalProductForInv = objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv;
                        if (lstRpt_OrderSummary_TotalProductForInv != null && lstRpt_OrderSummary_TotalProductForInv.Count != 0)
                        {
                            orderNoSys = CUtils.StrValue(lstRpt_OrderSummary_TotalProductForInv[0].OrderNoSys);
                            // Lấy thêm các thông tin từ Product
                            var strProductCode = "";
                            var lstProductCodeSys = new List<string>();
                            foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                            {
                                var productcode = it.ProductCode.ToString();
                                if (!lstProductCodeSys.Contains(productcode))
                                {
                                    lstProductCodeSys.Add(productcode);
                                    if (strProductCode == "")
                                    {
                                        strProductCode += productcode;
                                    }
                                    else
                                    {
                                        strProductCode += "," + productcode;
                                    }
                                }
                            }
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
                                    foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                                    {
                                        // Thêm các thông tin từ MstProduct
                                        var productCode = it.ProductCode == null ? "" : it.ProductCode.ToString();
                                        var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                                        if (itMstProduct != null)
                                        {
                                            it.ProductCodeBase = itMstProduct.ProductCodeBase;
                                            it.ProductCodeRoot = itMstProduct.ProductCodeRoot;
                                            it.UnitCode = itMstProduct.UnitCode;
                                            it.ProductName = itMstProduct.ProductName;
                                            it.ProductCodeUser = itMstProduct.ProductCodeUser;
                                            it.ValConvert = itMstProduct.ValConvert;
                                            it.FlagSerial = itMstProduct.FlagSerial;
                                            it.FlagLo = itMstProduct.FlagLot;
                                            it.UPSell = itMstProduct.UPSell;
                                            if (itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") && itMstProduct.ValConvert != null)
                                            {
                                                it.FlagCombo = "1";
                                            }
                                            else
                                            {
                                                it.FlagCombo = "0";
                                            }





                                        }




                                        //                                        
                                    }




                                }
                            }
                            #endregion
                        }




                    }
                    //return Json(new { Success = true, data = lstRpt_OrderSummary_TotalProductForInv });

                    //return JsonView("showOptionProductInOrder", lstRpt_OrderSummary_TotalProductForInv);
                    return Json(new { Success = true, LstRpt_OrderSummary_TotalProductForInv = lstRpt_OrderSummary_TotalProductForInv });
                    #endregion
                }

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
            //return View(resultEntry);
            return JsonViewError("showOptionProductInOrder", null, resultEntry);
        }





        [HttpPost]
        public ActionResult GetTonKho(string IF_InvOutNo, string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string ProductCodeUser, string ProductName, string ViewType, string onlyView = "0")
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

                #region Thông tin từ phiếu xuất
                var lst_InvF_InventoryOutDtl = new List<InvF_InventoryOutDtl>();
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl != null)
                {
                    lst_InvF_InventoryOutDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl.Where(x => x.ProductCodeRoot.Equals(productCode)).ToList();
                }
                ViewBag.Lst_InvF_InventoryOutDtl = lst_InvF_InventoryOutDtl;
                #endregion
                ViewBag.onlyView = onlyView;

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
        [ValidateAntiForgeryToken]

        public ActionResult Export(string IF_InvOutNo,
            string createdtimefrom,
            string createdtimeto,
            string approvedtimefrom,
            string approvedtimeto,
            string InvOutType,
            string CustomerCode,
            string InvCodeOut,
            string OrgID,
            string refNo,
            string ckpending,
            string ckapprove,
            string ckcancle)
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
                var listExport = new List<InvF_InventoryOut>();
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut
                {
                    IF_InvOutNo = IF_InvOutNo,
                    InvOutType = InvOutType,
                    CustomerCode = CustomerCode,
                    InvCodeOut = InvCodeOut,
                    RefNo = refNo
                };


                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto, ckpending, ckapprove, ckcancle, OrgID);
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
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Rt_Cols_InvF_InventoryOut = "*",
                    Ft_WhereClause = strWhere
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);


                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
                {
                    if (objRT_InvF_InventoryOut.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_InvF_InventoryOut.MySummaryTable.MyCount);
                    }
                    if (objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        listExport.AddRange(objRT_InvF_InventoryOut.Lst_InvF_InventoryOut);
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

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);

                    ExcelExport.ExportToExcelFromList(listExport, dicColNames, filePath, string.Format("InvF_InventoryOut"));


                    #region["Xuất excell các trang còn lại"]

                    listExport.Clear();
                    if (pageCount > 0)
                    {
                        for (var i = 0; i <= pageCount; i++)
                        {
                            objRQ_InvF_InventoryOut.Tid = GetNextTId();
                            objRQ_InvF_InventoryOut.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_InvF_InventoryOutcur = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);


                            if (objRT_InvF_InventoryOutcur.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count > 0)
                            {
                                listExport.AddRange(objRT_InvF_InventoryOutcur.Lst_InvF_InventoryOut);

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


        #region Excel
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"IF_InvOutNo","Số phiếu xuất"},
                 {"ApprDTimeUTC","Thời gian xuất"},
                 {"InvCodeOut","Kho xuất"},
                 {"mct_CustomerCode","Khách hàng"},
                 {"TotalValOutAfterDesc","Tổng tiền"},
                 {"IF_InvOutStatus","Trạng thái"},
                 {"Remark","Nội dung xuất"},
                 //{"","Số hóa đơn"},
                 {"CreateDTimeUTC","Thời gian tạo"},
                 {"InvOutType","Loại phiếu xuất"},
                 //{"UseReceive","Người nhận hàng"},
                 {"RefNo","Số RefNo"}
            };
        }

        #region Sản phẩm _Product
        public ActionResult ExportTemplateProductInvOut()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvF_InventoryOutDtl = new List<InvF_InventoryOutDtl>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateProduct();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryOutDtl).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInvF_InventoryOutDtl, dicColNames, filePath, string.Format("InvF_InventoryOutDtl"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        private Dictionary<string, string> GetImportDicColumsTemplateProduct()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductCodeUser","Mã hàng hóa"},
                 {"Qty","Số lượng"},
                 {"InvCodeOutActual","Vị trí xuất"},
                 {"UPOut","Đơn giá"},
                 {"UPOutDesc","Giảm giá"},
                 {"ValOut","Thành tiền"},
                 {"Remark","Ghi chú"}
            };
        }
        #endregion
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProductSearchOrder(string productkey, string showview, string refno, string reftype, string InvBUPattern)
        {
            // Chưa có tìm kiếm productkey vì biz chưa hỗ trợ
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
                var lstRpt_OrderSummary_TotalProductForInv = new List<Rpt_OrderSummary_TotalProductForInv>();

                if (reftype == Constants.RefType.RO)//Gọi veloca
                {
                    string strRefNo = refno.Substring(3);
                    var strWhere = strWhereClause_ROProductPart(strRefNo, productkey);
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
                        foreach (var part in objRT_Ser_ROProductPart.Lst_Ser_ROProductPart)
                        {
                            double qtyOutRemain = CUtils.IsNullOrEmpty(part.QtyOutRemain) ? 0 : Convert.ToDouble(part.QtyOutRemain);
                            if (qtyOutRemain <= 0)
                            {
                                continue;
                            }

                            var objRpt = new Rpt_OrderSummary_TotalProductForInv()
                            {
                                OrderNoSys = part.RONoSys,
                                OrderNo = part.sro_RONo,
                                ProductCode = part.ProductCodePart,
                                QtyAppr = part.Qty, //Số lượng YC
                                QtyInvOutAvail = part.QtyOutRemain //Số lượng còn lại
                            };
                            lstRpt_OrderSummary_TotalProductForInv.Add(objRpt);
                        }
                    }
                }
                else if (reftype == Constants.RefType.OrderDL || reftype == Constants.RefType.OrderSO) //DMS
                {
                    #region Kết nối DMS

                    var objRQ_Rpt_OrderSummary_TotalProductForInv = new RQ_Rpt_OrderSummary_TotalProductForInv()
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
                        Rpt_OrderSummary_TotalProductForInv = new Rpt_OrderSummary_TotalProductForInv()
                        {
                            OrderNo = refno
                        }
                    };
                    var objRT_Rpt_OrderSummary_TotalProductForInv = DMS_ReportService.Instance.WA_Rpt_OrderSummary_TotalProductForInv(objRQ_Rpt_OrderSummary_TotalProductForInv, addressDMSAPIs);
                    if (objRT_Rpt_OrderSummary_TotalProductForInv != null && objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv != null
                        && objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv.Count > 0)
                    {
                        lstRpt_OrderSummary_TotalProductForInv.AddRange(objRT_Rpt_OrderSummary_TotalProductForInv.Lst_Rpt_OrderSummary_TotalProductForInv);
                    }

                    #endregion
                }

                #region Lấy thêm thông tin product

                if (lstRpt_OrderSummary_TotalProductForInv != null && lstRpt_OrderSummary_TotalProductForInv.Count > 0)
                {
                    // Lấy thêm các thông tin từ Product
                    var strProductCode = "";
                    var lstProductCodeSys = new List<string>();
                    foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                    {
                        var productcode = CUtils.StrValue(it.ProductCode);
                        if (!lstProductCodeSys.Contains(productcode))
                        {
                            lstProductCodeSys.Add(productcode);
                            if (strProductCode == "")
                            {
                                strProductCode += productcode;
                            }
                            else
                            {
                                strProductCode += "," + productcode;
                            }
                        }
                    }
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
                            foreach (var it in lstRpt_OrderSummary_TotalProductForInv)
                            {
                                it.OrderNo = refno;
                                // Thêm các thông tin từ MstProduct
                                var productCode = it.ProductCode == null ? "" : it.ProductCode.ToString();
                                var itMstProduct = lst_Mst_Product.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                                if (itMstProduct != null)
                                {
                                    it.ProductCodeBase = itMstProduct.ProductCodeBase;
                                    it.ProductCodeRoot = itMstProduct.ProductCodeRoot;
                                    it.UnitCode = itMstProduct.UnitCode;
                                    it.UPSell = itMstProduct.UPSell;
                                    it.ProductName = itMstProduct.ProductName;
                                    it.ProductCodeUser = itMstProduct.ProductCodeUser;
                                    it.ValConvert = itMstProduct.ValConvert;
                                    it.FlagSerial = itMstProduct.FlagSerial;
                                    it.FlagLo = itMstProduct.FlagLot;

                                    if (itMstProduct.ProductType != null && itMstProduct.ProductType.ToString().ToUpper().Equals("COMBO") && itMstProduct.ValConvert != null)
                                    {
                                        it.FlagCombo = "1";
                                    }
                                }
                                //   

                                #region Thông tin tồn kho
                                var strWhereClauseBalance = "";
                                var productCodeBase = itMstProduct.ProductCodeBase == null ? "" : itMstProduct.ProductCodeBase.ToString();
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
                                strWhereClauseBalance = sb_SQL.ToString();

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
                                    Ft_WhereClause = strWhereClauseBalance
                                };
                                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count != 0)
                                {
                                    var itTonKho = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance[0];
                                    if (itTonKho != null)
                                    {
                                        // Gán thông tin tồn kho
                                        it.QtyTotalOK = itTonKho.QtyTotalOK == null ? 0 : itTonKho.QtyTotalOK;
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    #endregion
                }

                #endregion

                ViewBag.RefNo = refno;
                return JsonView(lstRpt_OrderSummary_TotalProductForInv);
                //return Json(new { Success = true, data = lstRpt_OrderSummary_TotalProductForInv });
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

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult ImportProduct(HttpPostedFileWrapper file, string invBUPattern)
        //{
        //    var resultEntry = new JsonResultEntry() { Success = false };
        //    var exitsData = "";
        //    #region ["UserState Common Info"]
        //    var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
        //    var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
        //    var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
        //    var orgId = UserState.Mst_NNT.OrgID;
        //    var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
        //    #endregion
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            #region ["Mst_Inventory"]
        //            var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";

        //            if (!string.IsNullOrEmpty(invBUPattern))
        //            {
        //                strWhere_Mst_Inventory += " and ";
        //                strWhere_Mst_Inventory += " Mst_Inventory.InvBUPattern like '" + invBUPattern + "'";
        //            }
        //            var list_Mst_Inventory = List_Mst_Inventory_Get(strWhere_Mst_Inventory);
        //            #endregion

        //            string fileId = Guid.NewGuid().ToString("D");
        //            string oldFileName = file.FileName;
        //            var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
        //            if (ext != ".xlsx" && ext != ".xls")
        //            {
        //                resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
        //                return Json(resultEntry);
        //            }
        //            if (ext.Equals(".xlsx") || ext.Equals(".xls"))
        //            {
        //                FileUtils.SaveTempFile(file, fileId, ext);
        //            }

        //            string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

        //            var table = ExcelImportNew.Query(filePath, "A2");
        //            if (table != null && table.Rows.Count > 0)
        //            {
        //                //if (table.Columns.Count != 3)
        //                //{
        //                //    exitsData = "File excel import không hợp lệ!";
        //                //    resultEntry.AddMessage(exitsData);
        //                //    return Json(resultEntry);
        //                //}
        //                #region["Check null"]

        //                foreach (DataRow dr in table.Rows)
        //                {
        //                    if (dr["ProductCodeUser"] == null || dr["ProductCodeUser"].ToString().Trim().Length == 0)
        //                    {
        //                        exitsData = "Mã hàng hóa không được để trống!";
        //                        resultEntry.AddMessage(exitsData);
        //                        return Json(resultEntry);
        //                    }

        //                    if (dr["Qty"] == null || dr["Qty"].ToString().Trim().Length == 0)
        //                    {
        //                        exitsData = "Số lượng không được để trống!";
        //                        resultEntry.AddMessage(exitsData);
        //                        return Json(resultEntry);
        //                    }
        //                    else
        //                    {
        //                        if (!CUtils.IsNumeric(dr["Qty"]))
        //                        {
        //                            exitsData = "Số lượng không phải định dạng số!";
        //                            resultEntry.AddMessage(exitsData);
        //                            return Json(resultEntry);
        //                        }
        //                        else
        //                        {
        //                            if (Convert.ToDouble(dr["Qty"].ToString().Trim()) < 0)
        //                            {
        //                                exitsData = "Số lượng không được < 0!";
        //                                resultEntry.AddMessage(exitsData);
        //                                return Json(resultEntry);
        //                            }
        //                        }
        //                    }

        //                    if (dr["InvCodeOutActual"] == null || dr["InvCodeOutActual"].ToString().Trim().Length == 0)
        //                    {
        //                        exitsData = "Vị trí không được để trống!";
        //                        resultEntry.AddMessage(exitsData);
        //                        return Json(resultEntry);
        //                    }
        //                    else
        //                    {
        //                        string invCodeOutActual = dr["InvCodeOutActual"].ToString().Trim();
        //                        var lstInvCheck = list_Mst_Inventory.Where(m => Convert.ToString(m.InvCode) == invCodeOutActual).ToList();
        //                        if (lstInvCheck == null || lstInvCheck.Count == 0)
        //                        {
        //                            exitsData = "Vị trí '" + invCodeOutActual + "' không có trong hệ thống!";
        //                            resultEntry.AddMessage(exitsData);
        //                            return Json(resultEntry);
        //                        }
        //                    }
        //                }
        //                #endregion

        //                #region["Check mã hàng hóa trùng"]

        //                for (var i = 0; i < table.Rows.Count; i++)
        //                {
        //                    var productCodeUser1 = CUtils.StrValue(table.Rows[i]["ProductCodeUser"]);
        //                    var invCodeOutActual1 = CUtils.StrValue(table.Rows[i]["InvCodeOutActual"]);
        //                    for (var j = 0; j < table.Rows.Count; j++)
        //                    {
        //                        if (i != j)
        //                        {
        //                            var productCodeUser2 = CUtils.StrValue(table.Rows[j]["ProductCodeUser"]);
        //                            var invCodeOutActual2 = CUtils.StrValue(table.Rows[j]["InvCodeOutActual"]);
        //                            if (productCodeUser1.Equals(productCodeUser2) && invCodeOutActual1.Equals(invCodeOutActual2))
        //                            {
        //                                exitsData = "Mã hàng hóa '" + productCodeUser1 + "' có vị trí '" + invCodeOutActual1 + "' trong file excel lặp!";
        //                                resultEntry.AddMessage(exitsData);
        //                                return Json(resultEntry);
        //                            }
        //                        }
        //                    }
        //                }
        //                #endregion

        //                #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]                        
        //                List<string> lstProductCodeUser = new List<string>();

        //                var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryOutDtlUI>(table);
        //                var listDtlImportReturn = new List<InvF_InventoryOutDtlUI>();

        //                lstProductCodeUser = listDtlImport.Select(m => m.ProductCodeUser.ToString()).ToList();
        //                var lstPrdCodeBase = new List<string>();

        //                #region Lấy danh sách hàng hóa

        //                var objRQ_Mst_Product = new RQ_Mst_Product()
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
        //                    FuncType = null,
        //                    Ft_RecordStart = Ft_RecordStart,
        //                    Ft_RecordCount = Ft_RecordCount,
        //                    Ft_WhereClause = strWhereSearchProductImport(lstProductCodeUser, null),
        //                    Ft_Cols_Upd = null,
        //                    // RQ_Mst_Product
        //                    Rt_Cols_Mst_Product = "*",
        //                    Rt_Cols_Mst_ProductImages = "",
        //                    Rt_Cols_Mst_ProductFiles = "",
        //                    Rt_Cols_Prd_BOM = "",
        //                    Rt_Cols_Prd_Attribute = "",
        //                };

        //                var listProduct = WA_Mst_Product_Get(objRQ_Mst_Product);

        //                #endregion                        
        //                var lst_StrProductCode = new List<string>();
        //                foreach (var item in listDtlImport)
        //                {
        //                    var listPrdCheck = new List<Mst_Product>();
        //                    if (listProduct != null && listProduct.Count > 0)
        //                    {
        //                        listPrdCheck = listProduct.Where(x => x.ProductCodeUser.ToString() == item.ProductCodeUser.ToString()).ToList();
        //                    }
        //                    if (listPrdCheck != null && listPrdCheck.Count > 0)
        //                    {
        //                        // Dùng lấy tồn kho
        //                        lst_StrProductCode.Add(listPrdCheck[0].ProductCodeBase.ToString());
        //                        //

        //                        item.ProductCode = listPrdCheck[0].ProductCode;
        //                        item.mp_ProductCodeBase = listPrdCheck[0].ProductCodeBase;
        //                        item.ProductCodeRoot = listPrdCheck[0].ProductCodeRoot;
        //                        item.UnitCode = listPrdCheck[0].UnitCode;
        //                        item.mp_ProductName = listPrdCheck[0].ProductName;
        //                        item.ProductCodeUser = listPrdCheck[0].ProductCodeUser;
        //                        item.UPOut = listPrdCheck[0].UPSell;                                
        //                        item.FlagLot = listPrdCheck[0].FlagLot;
        //                        item.FlagSerial = listPrdCheck[0].FlagSerial;
        //                        //item.UPOutDesc = 0;
        //                        //item.UPOutDesc = listPrdCheck[0].UPOutDesc;

        //                        var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
        //                        var pricedesc = item.UPOutDesc == null ? 0 : Convert.ToDouble(item.UPOutDesc);
        //                        var priceInit = item.UPOut == null ? 0 : Convert.ToDouble(item.UPOut);
        //                        var valafterDes = qty * (priceInit - pricedesc);
        //                        item.ValOutAfterDesc = valafterDes;

        //                        lstPrdCodeBase.Add(listPrdCheck[0].ProductCodeBase.ToString());
        //                    }
        //                    else
        //                    {
        //                        exitsData = "Mã hàng hóa '" + item.mp_ProductCodeUser + "' không có trong hệ thống!";
        //                        resultEntry.AddMessage(exitsData);
        //                        return Json(resultEntry);
        //                    }
        //                }


        //                var strWhereClause = "";
        //                var sb_SQL = new StringBuilder();
        //                var lstProductCode = "";
        //                if (lst_StrProductCode.Count != 0)
        //                {
        //                    foreach (var productcode in lst_StrProductCode)
        //                    {
        //                        if (lstProductCode == "")
        //                        {
        //                            lstProductCode += productcode;
        //                        }
        //                        else
        //                        {
        //                            lstProductCode += ", " + productcode;
        //                        }
        //                    }
        //                }
        //                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
        //                if (lstProductCode != "")
        //                {
        //                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCode, "IN");
        //                }
        //                strWhereClause = sb_SQL.ToString();

        //                var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
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
        //                    Rt_Cols_Inv_InventoryBalance = "*",
        //                    Ft_RecordStart = Ft_RecordStart,
        //                    Ft_RecordCount = Ft_RecordCount,
        //                    Ft_WhereClause = strWhereClause
        //                };
        //                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
        //                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
        //                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
        //                {
        //                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
        //                }

        //                // Lấy tồn kho
        //                foreach(var it in listDtlImport)
        //                {
        //                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
        //                    {
        //                        var qtyTotalOk = 0.0;
        //                        var lsttonkhoSP = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.mp_ProductCodeBase)).ToList();
        //                        if (lsttonkhoSP != null)
        //                        {
        //                            qtyTotalOk = lsttonkhoSP.Sum(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
        //                        }

        //                        var qtyTotalOkMax = lsttonkhoSP.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
        //                        var itBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.mp_ProductCodeBase) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
        //                        if (itBalance != null)
        //                        {
        //                            it.InvCodeMax = itBalance.InvCode;
        //                            it.QtyInv = qtyTotalOk;
        //                            it.QtyTotalOkMax = qtyTotalOkMax;
        //                        }
        //                    }

        //                    listDtlImportReturn.Add(new InvF_InventoryOutDtlUI() {
        //                        ProductCode = CUtils.StrValue(it.ProductCode),
        //                        ProductCodeRoot = CUtils.StrValue(it.ProductCodeRoot),
        //                        ProductCodeUser = CUtils.StrValue(it.ProductCodeUser),
        //                        Qty = CUtils.StrValue(it.Qty),
        //                        InvCodeOutActual = CUtils.StrValue(it.InvCodeOutActual),
        //                    });
        //                }
        //                //

        //                #endregion

        //                //return JsonView("LoadProduct", listDtlImport);
        //                var listProductDtl = new List<InvF_InventoryOutDtlUI>();
        //                #region["Gom dữ liệu theo hàng hóa"]
        //                if(listDtlImport != null && listDtlImport.Count > 0)
        //                {
        //                    foreach(var item in listDtlImport)
        //                    {
        //                        var objProduct = listProductDtl.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCodeUser).ToUpper().Equals(CUtils.StrValue(item.ProductCodeUser).ToUpper())).FirstOrDefault();
        //                        if(objProduct == null)
        //                        {
        //                            if (CUtils.IsNullOrEmpty(item.UPOutDesc))
        //                            {
        //                                item.UPOutDesc = "0";
        //                            }
        //                            if (CUtils.IsNullOrEmpty(item.Qty))
        //                            {
        //                                item.Qty = "1";
        //                            }
        //                            listProductDtl.Add(item);
        //                        }
        //                        else
        //                        {
        //                            if (!CUtils.IsNullOrEmpty(item.UPOutDesc) && CUtils.IsNumeric(item.UPOutDesc))
        //                            {
        //                                // Giảm giá hiện tại
        //                                var dUPOutDescCur = Convert.ToDecimal(item.UPOutDesc);
        //                                // Giảm giá đã lưu từ trước
        //                                var dUPOutDesc = Convert.ToDecimal(objProduct.UPOutDesc);

        //                                if(dUPOutDescCur > dUPOutDesc)
        //                                {
        //                                    objProduct.UPOutDesc = dUPOutDescCur;
        //                                }
        //                            }

        //                            if (!CUtils.IsNullOrEmpty(item.Qty) && CUtils.IsNumeric(item.Qty))
        //                            {
        //                                // Số lượng hiện tại
        //                                var dQtyCur = Convert.ToDecimal(item.Qty);
        //                                // Số lượng đã lưu từ trước
        //                                var dQty = Convert.ToDecimal(objProduct.Qty);

        //                                dQty += dQtyCur;

        //                                objProduct.Qty = dQty;
        //                            }
        //                            objProduct.InvCodeOutActual += (", " + CUtils.StrValue(item.InvCodeOutActual));
        //                        }
        //                    }
        //                }
        //                #endregion

        //                return Json(new { Success = true, data = listProductDtl, listDtlImport = listDtlImportReturn });
        //            }
        //            else
        //            {
        //                exitsData = Nonsense.MESS_CHECK_FILE_NULL;
        //                resultEntry.AddMessage(exitsData);
        //                return Json(resultEntry);
        //            }
        //        }
        //        catch (ClientException ex)
        //        {
        //            resultEntry.SetFailed().AddException(ex);
        //        }
        //        catch (Exception ex)
        //        {
        //            resultEntry.SetFailed().AddException(ex);
        //        }
        //    }
        //    return Json(resultEntry);
        //}

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


                            if (dr["UPOut"] != null && dr["UPOut"].ToString().Trim().Length > 0)
                            {
                                if (!CUtils.IsNumeric(dr["UPOut"]))
                                {
                                    exitsData = "Đơn giá không phải định dạng số!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDouble(dr["UPOut"].ToString().Trim()) < 0)
                                    {
                                        exitsData = "Đơn giá không được < 0!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }

                            if (dr["UPOutDesc"] != null && dr["UPOutDesc"].ToString().Trim().Length > 0)
                            {
                                if (!CUtils.IsNumeric(dr["UPOutDesc"]))
                                {
                                    exitsData = "Giảm giá không phải định dạng số!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDouble(dr["UPOutDesc"].ToString().Trim()) < 0)
                                    {
                                        exitsData = "Giảm giá không được < 0!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }


                            if (dr["ValOut"] != null && dr["ValOut"].ToString().Trim().Length > 0)
                            {
                                if (!CUtils.IsNumeric(dr["ValOut"]))
                                {
                                    exitsData = "Thành tiền không phải định dạng số!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (Convert.ToDouble(dr["ValOut"].ToString().Trim()) < 0)
                                    {
                                        exitsData = "Thành tiền không được < 0!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }


                        }

                        #endregion

                        #region["Check ma hang hoa trung"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var productCodeUser1 = CUtils.StrValue(table.Rows[i]["ProductCodeUser"]);
                            var invCodeOutActual1 = CUtils.StrValue(table.Rows[i]["InvCodeOutActual"]);
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
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
                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryOutDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryOutDtlUI>();
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
                                //item.UPOut = listPrdCheck[0].UPSell;
                                item.FlagLot = listPrdCheck[0].FlagLot;
                                item.FlagSerial = listPrdCheck[0].FlagSerial;
                                item.ValConvert = listPrdCheck[0].ValConvert;
                                //item.UPOutDesc = listPrdCheck[0].
                                item.Remark = item.Remark;

                                if (listPrdCheck[0].ProductType != null && listPrdCheck[0].ProductType.ToString().ToUpper().Equals("COMBO") && listPrdCheck[0].ValConvert != null)
                                {
                                    item.FlagCombo = "1";
                                }
                                else
                                {
                                    item.FlagCombo = "0";
                                }

                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var pricedesc = item.UPOutDesc == null ? 0 : Convert.ToDouble(item.UPOutDesc);
                                var priceInit = item.UPOut == null ? 0 : Convert.ToDouble(item.UPOut);
                                var valafterDes = qty * (priceInit - pricedesc);
                                item.ValOutAfterDesc = valafterDes;
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
                                    var objInvF_InventoryReturnSupInstProduct = new Mst_ProductUI()
                                    {
                                        ProductCode = objReturnSupDtl.ProductCode,
                                        ProductCodeUser = objReturnSupDtl.ProductCodeUser,
                                        ProductCodeBase = objReturnSupDtl.mp_ProductCodeBase,
                                        ProductCodeRoot = objReturnSupDtl.ProductCodeRoot,
                                        ProductName = objReturnSupDtl.ProductName,
                                        UnitCode = objReturnSupDtl.UnitCode,
                                        ValConvert = objReturnSupDtl.ValConvert,
                                        FlagLot = objReturnSupDtl.FlagLot,
                                        FlagSerial = objReturnSupDtl.FlagSerial,
                                        FlagCombo = objReturnSupDtl.FlagCombo,
                                        Remark = objReturnSupDtl.Remark,
                                        UPOut = objReturnSupDtl.UPOut,
                                        UPOutDesc = objReturnSupDtl.UPOutDesc,
                                        //UPOutDesc = "0",

                                    };


                                    var strInvCodeOutActual = "";
                                    var listDtlCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                                    if (listDtlCur != null && listDtlCur.Count > 0)
                                    {
                                        var fQty = 0.0;
                                        var i = 0;
                                        foreach (var _it in listDtlCur)
                                        {
                                            fQty += CUtils.ConvertToDouble(_it.Qty);
                                            objInvF_InventoryReturnSupInstProduct.Qty = fQty;
                                            objInvF_InventoryReturnSupInstProduct.ValOutAfterDesc = fQty * (CUtils.ConvertToDouble(_it.UPOut) - CUtils.ConvertToDouble(_it.UPOutDesc));
                                            if (i != 0)
                                            {
                                                strInvCodeOutActual += ", ";
                                            }
                                            strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                            objInvF_InventoryReturnSupInstProduct.InvCodeInActual = strInvCodeOutActual;
                                            i++;
                                        }
                                    }

                                    #region["Danh sach hang hoa cung base"]
                                    var strWhereCungBase = "";
                                    var sbSqlCungBase = new StringBuilder();
                                    if (!CUtils.IsNullOrEmpty(objInvF_InventoryReturnSupInstProduct.ProductCodeBase))
                                    {
                                        sbSqlCungBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objInvF_InventoryReturnSupInstProduct.ProductCodeBase), "=");
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
                                            else
                                            {
                                                productUI.FlagCombo = "0";
                                            }
                                            productUI.UPOut = it2.UPSell == null ? 0 : it2.UPSell;
                                            productUI.UPOutDesc = "0";
                                            productUI.ValOutAfterDesc = "0";
                                            productUI.Remark = "";
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
                                            objInvF_InventoryReturnSupInstProduct.lstUnitCodeUIByProduct = lst_Mst_ProductUI;
                                            #endregion
                                        }
                                    }
                                    lst_MstProductUI.Add(objInvF_InventoryReturnSupInstProduct);
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
            var qty1 = 0.0;
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
                            //var invCodeOutActual1 = table.Rows[i]["InvCodeOutActual"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var productCode2 = table.Rows[j]["ProductCodeUser"].ToString().Trim();
                                    var productLotNo2 = table.Rows[j]["ProductLotNo"].ToString().Trim();
                                    //var invCodeOutActual2 = table.Rows[j]["InvCodeOutActual"].ToString().Trim();
                                    if (productCode1 == productCode2 && productLotNo1 == productLotNo2)
                                    {
                                        //exitsData = "Số lô '" + productLotNo1 + "' của mã hàng hóa '" + productCode1 + "' có vị trí '" + invCodeOutActual1 + "' trong file excel lặp!";
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


                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryOutDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryOutDtlUI>();

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
                        var strInvCodeInActual = "";
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
                                item.UPOut = listPrdCheck[0].UPSell;
                                item.FlagLot = listPrdCheck[0].FlagLot;
                                item.FlagSerial = listPrdCheck[0].FlagSerial;
                                if (listPrdCheck[0].ProductType != null && listPrdCheck[0].ProductType.ToString().ToUpper().Equals("COMBO") && listPrdCheck[0].ValConvert != null)
                                {
                                    item.FlagCombo = "1";
                                }
                                else
                                {
                                    item.FlagCombo = "0";
                                }
                                item.ValConvert = listPrdCheck[0].ValConvert;
                                item.Remark = "";


                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var pricedesc = item.UPOutDesc == null ? 0 : Convert.ToDouble(item.UPOutDesc);
                                var priceInit = item.UPOut == null ? 0 : Convert.ToDouble(item.UPOut);
                                var valafterDes = qty * (priceInit - pricedesc);
                                item.ValOutAfterDesc = valafterDes;



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
                        //var listProductCode_Distinct = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCodeUser)).Select(item => CUtils.StrValue(item.ProductCodeUser)).ToList();
                        //var listInvCodeOut_Distinct = listProductCode_Distinct.Where(it => !CUtils.IsNullOrEmpty(it.)).Select(item => CUtils.StrValue(item.)).Distinct().ToList();
                        if (listProductCode_Distinct != null && listProductCode_Distinct.Count > 0)
                        {
                            foreach (var item in listProductCode_Distinct)
                            {
                                var objReturnSupDtl = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).FirstOrDefault();
                                if (objReturnSupDtl != null)
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
                                        FlagLot = objReturnSupDtl.FlagLot,
                                        FlagSerial = objReturnSupDtl.FlagSerial,
                                        FlagCombo = objReturnSupDtl.FlagCombo,
                                        UPOut = objReturnSupDtl.UPOut,
                                        UPOutDesc = "0",
                                        Remark = objReturnSupDtl.Remark,
                                        InvCodeInActual = objReturnSupDtl.InvCodeOutActual


                                    };





                                    var strInvCodeOutActual = "";
                                    var listDtlCur = listDtlImport.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && CUtils.StrValue(it.ProductCode).Equals(item)).ToList();
                                    var listInvCodeIn_Distinct = listDtlCur.Where(i => !CUtils.IsNullOrEmpty(i.InvCodeOutActual)).Select(it => CUtils.StrValue(it.InvCodeOutActual)).Distinct().ToList();
                                    if (listDtlCur != null && listDtlCur.Count > 0)
                                    {
                                        var fQty = 0.0;
                                        var i = 0;
                                        if (listInvCodeIn_Distinct != null && listInvCodeIn_Distinct.Count > 0)
                                        {
                                            var j = 0;
                                            foreach (var invCode in listInvCodeIn_Distinct)
                                            {

                                                var listInvCodeIn = listDtlCur.Where(it => !CUtils.IsNullOrEmpty(it.InvCodeOutActual) && CUtils.StrValue(it.InvCodeOutActual).Equals(invCode)).ToList();

                                                if (listInvCodeIn != null && listInvCodeIn.Count > 1)
                                                {

                                                    foreach (var _ittem in listInvCodeIn)
                                                    {
                                                        fQty += CUtils.ConvertToDouble(_ittem.Qty);
                                                        objInvF_InventoryReturnSupInstLot.Qty = fQty;
                                                        objInvF_InventoryReturnSupInstLot.InvCodeInActual = _ittem.InvCodeOutActual;
                                                        objInvF_InventoryReturnSupInstLot.ValOutAfterDesc = fQty * (CUtils.ConvertToDouble(_ittem.UPOut) - CUtils.ConvertToDouble(_ittem.UPOutDesc));

                                                        if (!strInvCodeOutActual.Contains(CUtils.StrValue(_ittem.InvCodeOutActual)))
                                                        {
                                                            if (!CUtils.IsNullOrEmpty(strInvCodeOutActual))
                                                            {
                                                                strInvCodeOutActual += ", ";
                                                            }

                                                            strInvCodeOutActual += CUtils.StrValue(_ittem.InvCodeOutActual);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (j != 0)
                                                    {

                                                        strInvCodeOutActual += ", ";
                                                    }

                                                    strInvCodeOutActual += CUtils.StrValue(listInvCodeIn[0].InvCodeOutActual);
                                                    var qty2 = 0.0;
                                                    qty2 = CUtils.ConvertToDouble(listInvCodeIn[0].Qty);
                                                    qty1 = CUtils.ConvertToDouble(objInvF_InventoryReturnSupInstLot.Qty);
                                                    qty1 += qty2;
                                                    objInvF_InventoryReturnSupInstLot.Qty = CUtils.StrValue(qty1);
                                                    //objInvF_InventoryReturnSupInstLot.InvCodeInActual = strInvCodeOutActual;
                                                    objInvF_InventoryReturnSupInstLot.ValOutAfterDesc = qty1 * (CUtils.ConvertToDouble(objInvF_InventoryReturnSupInstLot.UPOut) - CUtils.ConvertToDouble(objInvF_InventoryReturnSupInstLot.UPOutDesc));
                                                    j++;
                                                }

                                            }
                                            objInvF_InventoryReturnSupInstLot.InvCodeInActual = strInvCodeOutActual;
                                            //objInvF_InventoryReturnSupInstLot.Qty = CUtils.StrValue(fQty + qty1);
                                            //objInvF_InventoryReturnSupInstLot.ValOutAfterDesc = (fQty + qty1) * (CUtils.ConvertToDouble(objInvF_InventoryReturnSupInstLot.UPOut) - CUtils.ConvertToDouble(objInvF_InventoryReturnSupInstLot.UPOutDesc));
                                        }
                                        else
                                        {
                                            foreach (var _it in listDtlCur)
                                            {
                                                fQty += CUtils.ConvertToDouble(_it.Qty);
                                                objInvF_InventoryReturnSupInstLot.Qty = fQty;
                                                objInvF_InventoryReturnSupInstLot.ValOutAfterDesc = fQty * (CUtils.ConvertToDouble(_it.UPOut) - CUtils.ConvertToDouble(_it.UPOutDesc));
                                                if (i != 0)
                                                {
                                                    strInvCodeOutActual += ", ";
                                                }
                                                strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                                //objInvF_InventoryReturnSupInstLot.InvCodeInActual = strInvCodeOutActual;
                                                i++;
                                            }
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
                                            else
                                            {
                                                productUI.FlagCombo = "0";
                                            }
                                            productUI.UPOut = it2.UPSell == null ? 0 : it2.UPSell;
                                            productUI.UPOutDesc = "0";
                                            productUI.ValOutAfterDesc = "0";
                                            productUI.Remark = "";



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
                                    if (serialNo1 == serialNo2 && productCodeUser1 == productCodeUser2)
                                    {
                                        exitsData = "Số Serial '" + serialNo1 + "' của mã hàng hóa '" + productCodeUser1 + "' trong file excel lặp!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);

                                        //exitsData = "Số Serial '" + serialNo1 + "' có vị trí '" + invCodeOutActual1 + "' trong file excel lặp!";
                                        //resultEntry.AddMessage(exitsData);
                                        //return Json(resultEntry);
                                    }
                                }
                            }

                            #endregion



                        }


                        #region["Check mã hàng hóa tồn tại trong hệ thống và add vào list"]
                        List<string> lstProductCodeUser = new List<string>();
                        var listDtlImport = DataTableCmUtils.ToListof<InvF_InventoryOutDtlUI>(table);
                        var listDtlImportReturn = new List<InvF_InventoryOutDtlUI>();
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
                                item.UPOut = listPrdCheck[0].UPSell;
                                item.FlagLot = listPrdCheck[0].FlagLot;
                                item.FlagSerial = listPrdCheck[0].FlagSerial;
                                item.ValConvert = listPrdCheck[0].ValConvert;
                                item.Remark = "";
                                if (listPrdCheck[0].ProductType != null && listPrdCheck[0].ProductType.ToString().ToUpper().Equals("COMBO") && listPrdCheck[0].ValConvert != null)
                                {
                                    item.FlagCombo = "1";
                                }
                                else
                                {
                                    item.FlagCombo = "0";
                                }

                                var qty = item.Qty == null ? 0 : Convert.ToDouble(item.Qty);
                                var pricedesc = item.UPOutDesc == null ? 0 : Convert.ToDouble(item.UPOutDesc);
                                var priceInit = item.UPOut == null ? 0 : Convert.ToDouble(item.UPOut);
                                var valafterDes = qty * (priceInit - pricedesc);
                                item.ValOutAfterDesc = valafterDes;


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
                                        FlagLot = objReturnSupDtl.FlagLot,
                                        FlagSerial = objReturnSupDtl.FlagSerial,
                                        FlagCombo = objReturnSupDtl.FlagCombo,
                                        Remark = objReturnSupDtl.Remark,
                                        UPOut = objReturnSupDtl.UPOut,
                                        //UPOutDesc = objReturnSupDtl.UPOutDesc,
                                        UPOutDesc = "0",
                                        InvCodeInActual = objReturnSupDtl.InvCodeOutActual
                                    };



                                    #region Lấy tồn kho serial

                                    //var listProductCodeBase = listPrd.Select(m => CUtils.StrValue(m.mp_ProductCodeBase)).ToList();
                                    //var strListProductCodeBase = CUtils.StretchListString(listProductCodeBase);
                                    var listInvBalanceSerial = GetListInventoryBalanceSerial(CUtils.StrValue(objReturnSupDtl.mp_ProductCodeBase), invBUPattern);





                                    #endregion



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
                                            #region["Check hàng hóa chứa serial"]
                                            var serialNo = CUtils.StrValue(_it.SerialNo);
                                            var lstSerialCur = listInvBalanceSerial.Where(m => m.SerialNo.Equals(serialNo)).ToList();
                                            if (lstSerialCur == null || lstSerialCur.Count == 0)
                                            {
                                                exitsData = "Mã serial'" + serialNo + "' không có trên hệ thống!";
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                            #endregion



                                            objInvF_InventoryReturnSupInstSerial.ValOutAfterDesc = fQty * (CUtils.ConvertToDouble(_it.UPOut) - CUtils.ConvertToDouble(_it.UPOutDesc));
                                            //if(i != 0)
                                            //{
                                            //    strInvCodeOutActual += ", ";
                                            //}
                                            //strInvCodeOutActual += CUtils.StrValue(_it.InvCodeOutActual);
                                            //objInvF_InventoryReturnSupInstSerial.InvCodeInActual = strInvCodeOutActual;
                                            //i++;
                                            //objInvF_InventoryReturnSupInstSerial.InvCodeInActual = invCodeMax;
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
                                            else
                                            {
                                                productUI.FlagCombo = "0";
                                            }
                                            productUI.UPOut = it2.UPSell == null ? 0 : it2.UPSell;
                                            productUI.UPOutDesc = "0";
                                            productUI.ValOutAfterDesc = "0";
                                            productUI.Remark = "";



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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportQR(HttpPostedFileWrapper file, string listProduct)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            var Lst_InvF_InventoryOutQR = new List<InvF_InventoryOutQR>();
            if (ModelState.IsValid)
            {
                var listPrd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InventoryOutDtlUI>>(listProduct);
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
                    if (table.Columns.Count != 4)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    #region["Check null"]

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

                            var item = new InvF_InventoryOutQR();
                            item.ProductCode = lstPrdCur[0].ProductCode;
                            item.mp_ProductName = lstPrdCur[0].mp_ProductName;
                            item.mp_ProductCodeUser = productCodeUser;
                            item.QRCode = qRCode;
                            item.BoxNo = Convert.ToString(dr["BoxNo"]);
                            item.CanNo = Convert.ToString(dr["CanNo"]);
                            Lst_InvF_InventoryOutQR.Add(item);
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
                    return Json(new { Success = true, data = Lst_InvF_InventoryOutQR });
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
        #endregion

        #region ["In"]
        [HttpPost]
        public ActionResult PrintTemp(string iF_InvOutNo)
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
                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut()
                {
                    IF_InvOutNo = iF_InvOutNo
                };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, null, null, null, null, null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "",
                    Rt_Cols_InvF_InventoryOutInstSerial = "",
                    Rt_Cols_InvF_InventoryOutQR = "",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count != 0)
                {
                    objInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                }
                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvFInventoryOutDtlUI = new List<InvF_InventoryOutDtlUI>();
                var Lst_InvF_InventoryOutCover = new List<InvF_InventoryOutCover>();
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null)
                {
                    Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                }
                var lstPrdDistinct = new List<string>();
                int idx = 1;
                foreach (var item in Lst_InvF_InventoryOutCover)
                {
                    if (lstPrdDistinct.Contains(item.ProductCodeRoot.ToString()))
                        continue;
                    var listInvInDtlCur = Lst_InvF_InventoryOutCover.Where(m => m.ProductCodeRoot.ToString() == item.ProductCodeRoot.ToString()).ToList();
                    var listInvDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl.Where(m => m.ProductCodeRoot.ToString() == item.ProductCodeRoot.ToString()).ToList();

                    var qty = 0.0;
                    var valOutAfterDesc = 0.0;
                    var invCodeOutActual = "";
                    var invCodeOutActualName = "";
                    var listViTri = new List<string>();
                    foreach (var it in listInvDtl)
                    {
                        qty += Convert.ToDouble(it.Qty);
                        //valOutAfterDesc += Convert.ToDouble(it.ValOutAfterDesc);
                        if (!listViTri.Contains(it.InvCodeOutActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeOutActual))
                            {
                                invCodeOutActual += ", " + it.InvCodeOutActual.ToString();
                                invCodeOutActualName += ", " + it.InvCodeOutActual.ToString(); // Tam de
                            }
                            else
                            {
                                invCodeOutActual = it.InvCodeOutActual.ToString();
                                invCodeOutActualName = it.InvCodeOutActual.ToString();
                            }
                            listViTri.Add(it.InvCodeOutActual.ToString());
                        }
                    }
                    valOutAfterDesc = Math.Round((Convert.ToDouble(item.ValOut) - Convert.ToDouble(item.ValOutDesc)), 2);
                    var itemUI = new InvF_InventoryOutDtlUI()
                    {
                        IF_InvOutNo = item.IF_InvOutNo,
                        InvCodeOutActual = invCodeOutActual,
                        ProductCode = item.ProductCodeRoot,
                        NetworkID = item.NetworkID,
                        Qty = qty,
                        ValOut = item.ValOut,
                        ValOutDesc = item.ValOutDesc,
                        ValOutAfterDesc = valOutAfterDesc,
                        UnitCode = item.UnitCode,
                        IF_InvOutStatusDtl = item.IF_InvOutStatusDtl,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_root_ProductName,
                        ProductCodeUser = item.mp_root_ProductCodeUser,
                        mii_InvName = invCodeOutActualName,
                        Idx = idx,
                        UPOut = item.UPOut,
                        UPOutDesc = item.UPOutDesc
                    };
                    //
                    listInvFInventoryOutDtlUI.Add(itemUI);
                    idx++;

                    lstPrdDistinct.Add(item.ProductCodeRoot.ToString());
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
                InvF_InventoryOutPrint objPrint = new InvF_InventoryOutPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.NNTFullName = strNNTFullName;
                objPrint.IF_InvOutNo = objInvF_InventoryOut.IF_InvOutNo;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.CustomerName = objInvF_InventoryOut.mct_CustomerName;
                objPrint.OrderNo = objInvF_InventoryOut.OrderNo;
                objPrint.InvOutTypeName = objInvF_InventoryOut.miot_InvOutTypeName;
                objPrint.Remark = objInvF_InventoryOut.Remark;
                objPrint.TotalQty = listInvFInventoryOutDtlUI.Sum(m => Convert.ToDouble(m.Qty));
                objPrint.TotalValOut = objInvF_InventoryOut.TotalValOut;
                objPrint.TotalValOutDesc = objInvF_InventoryOut.TotalValOutDesc;
                objPrint.TotalValOutAfterDesc = objInvF_InventoryOut.TotalValOutAfterDesc;
                objPrint.CreateUserCode = objInvF_InventoryOut.su_UserCode_Create;
                objPrint.CreateUserName = objInvF_InventoryOut.su_UserName_Create;
                //objPrint.CreateUserName = objInvF_InventoryOut.CreateBy;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_InventoryOutDtlUI = listInvFInventoryOutDtlUI;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("PX");
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
                    //objPrint.LogoFilePath = null;
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
        public dynamic CreateDataObjectReportServer(InvF_InventoryOutPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_InventoryOut;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new InvF_InventoryOutReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.IF_InvOutNo = CUtils.StrValueNew(objTempPrint.IF_InvOutNo);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.CustomerName = CUtils.StrValueNew(objTempPrint.CustomerName);
                MyDynamic.OrderNo = CUtils.StrValueNew(objTempPrint.OrderNo);
                MyDynamic.InvOutTypeName = CUtils.StrValueNew(objTempPrint.InvOutTypeName);
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, NumericFormat(tableName, "TotalQty", defaultFormat));
                MyDynamic.TotalValOut = CUtils.StrValueFormatNumber(objTempPrint.TotalValOut, NumericFormat(tableName, "TotalValOut", defaultFormat));
                MyDynamic.TotalValOutDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValOutDesc, NumericFormat(tableName, "TotalValOutDesc", defaultFormat));
                MyDynamic.TotalValOutAfterDesc = CUtils.StrValueFormatNumber(objTempPrint.TotalValOutAfterDesc, NumericFormat(tableName, "TotalValOutAfterDesc", defaultFormat));
                MyDynamic.CreateUserCode = CUtils.StrValueNew(objTempPrint.CreateUserCode);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_InventoryOutDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_InvF_InventoryOutDtlUI != null && objTempPrint.Lst_InvF_InventoryOutDtlUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_InventoryOutDtlReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_InventoryOutDtlUI)
                {
                    var objDtl_ReportServer = new InvF_InventoryOutDtlReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        InvName = CUtils.StrValue(item.mii_InvName),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, NumericFormat(tableName, "Qty", defaultFormat)),
                        UPOut = CUtils.StrValueFormatNumber(item.UPOut, NumericFormat(tableName, "UPOut", defaultFormat)),
                        UPOutDesc = CUtils.StrValueFormatNumber(item.UPOutDesc, NumericFormat(tableName, "UPOutDesc", defaultFormat)),
                        ValOut = CUtils.StrValueFormatNumber(item.ValOut, NumericFormat(tableName, "ValOut", defaultFormat)),
                        ValOutDesc = CUtils.StrValueFormatNumber(item.ValOutDesc, NumericFormat(tableName, "ValOutDesc", defaultFormat)),
                        ValOutAfterDesc = CUtils.StrValueFormatNumber(item.ValOutAfterDesc, NumericFormat(tableName, "ValOutAfterDesc", defaultFormat)),
                    };
                    listDtl_ReportServer.Add(objDtl_ReportServer);
                }
                MyDynamic.DataTable.AddRange(listDtl_ReportServer);
            }
            return MyDynamic;
        }
        #endregion


        #region["Tìm kiếm lô"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchLo(string productCodeBase, string invBUPattern, string productCode, string productLotNo)
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
                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeBase = productCodeBase;
                //              

                //ViewBag.ProductCodeUser = productCodeUser;

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


        #endregion


        #region["Tìm kiếm seri"]

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


        #region ["Xuất chéo"]

        public ActionResult ExportCross(string IF_InvOutNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT_EXPORTCROSS");
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
                // Sinh số phiếu xuất
                Seq_Common objSeq_Common = new Seq_Common();
                objSeq_Common.Param_Postfix = "";
                objSeq_Common.Param_Prefix = "";
                objSeq_Common.SequenceType = SeqType.IFInvOutNo;
                var IFInvOutNo = SeqNo(objSeq_Common);
                ViewBag.IFInvOutNo = IFInvOutNo;

                #region Loại xuất kho               
                ViewBag.Lst_Mst_InvOutType = Mst_InvOutTypeGetAllActive(addressAPIs);
                #endregion

                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

                #region Thông tin khách hàng                                
                ViewBag.Lst_MstCustomer = Mst_Customer_GetActive(addressAPIs, Ft_RecordCount);
                #endregion

                #region Thông tin hàng hóa                
                //var lst_Mst_Product = GetProductProductSale(addressAPIs, "10");
                //var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                //foreach (var it in lst_Mst_Product)
                //{
                //    var productUI = new Mst_ProductUI();
                //    productUI.ProductCode = it.ProductCode;
                //    productUI.ProductName = it.ProductName;
                //    productUI.UnitCode = it.UnitCode;
                //    productUI.ProductCodeBase = it.ProductCodeBase;
                //    productUI.ProductCodeUser = it.ProductCodeUser;
                //    productUI.ProductCodeRoot = it.ProductCodeRoot;
                //    productUI.ValConvert = it.ValConvert;
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
                //    productUI.SellPrice = it.UPSell == null ? 0 : it.UPSell;
                //    //                    
                //    lst_Mst_ProductUI.Add(productUI);
                //}
                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                #endregion

                InvF_InventoryOut objInvF_InventoryOut = new InvF_InventoryOut() { IF_InvOutNo = IF_InvOutNo };
                var strWhere = WhereClauseInvF_InventoryOut(objInvF_InventoryOut, "", "", "", "", null, null, null, null);
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
                    Rt_Cols_InvF_InventoryOut = "*",
                    Rt_Cols_InvF_InventoryOutDtl = "*",
                    Rt_Cols_InvF_InventoryOutCover = "*",
                    Rt_Cols_InvF_InventoryOutInstLot = "*",
                    Rt_Cols_InvF_InventoryOutInstSerial = "*",
                    Rt_Cols_InvF_InventoryOutQR = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InventoryOut = InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_Get(objRQ_InvF_InventoryOut, addressAPIs);
                if (objRT_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut != null && objRT_InvF_InventoryOut.Lst_InvF_InventoryOut.Count != 0)
                {
                    objInvF_InventoryOut = objRT_InvF_InventoryOut.Lst_InvF_InventoryOut[0];
                    if (CUtils.StrValue(objInvF_InventoryOut.RefType) == RefType.RO)
                    {
                        objInvF_InventoryOut.RefNo = "RO-" + CUtils.StrValue(objInvF_InventoryOut.RefNo);
                    }
                }
                var orderNo = objInvF_InventoryOut.OrderNo == null ? "" : objInvF_InventoryOut.OrderNo.ToString();

                #region Thông tin hàng hóa
                //ViewBag.Lst_Mst_ProductUI = getListProduct(orderNo);
                #endregion

                #region Get List đơn vị tính theo Product 
                var Lst_InvF_InventoryOutCover = new List<InvF_InventoryOutCover>();
                if (objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover != null)
                {
                    Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                }
                foreach (var it in Lst_InvF_InventoryOutCover)
                {
                    #region Lấy danh sách đơn vị theo Product
                    var strWhereProduct = "";
                    var sbSql = new StringBuilder();
                    var Tbl_Mst_Product = "Mst_Product.";
                    if (it.mp_root_ProductCodeBase != null && it.mp_root_ProductCodeBase.ToString() != "")
                    {
                        sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_root_ProductCodeBase.ToString()), "=");
                    }
                    //if (it.ProductCodeRoot != null && it.ProductCodeRoot.ToString() != "")
                    //{
                    //    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeRoot", CUtils.StrValue(it.ProductCodeRoot), "=");
                    //}
                    strWhereProduct = sbSql.ToString();
                    var rqProduct = new RQ_Mst_Product()
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
                        Ft_WhereClause = strWhereProduct,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Mst_Product = "*"
                    };
                    var lsMst_Product = new List<Mst_Product>();
                    var rt_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(rqProduct, addressAPIs);
                    if (rt_Mst_Product != null && rt_Mst_Product.Lst_Mst_Product != null)
                    {
                        lsMst_Product = rt_Mst_Product.Lst_Mst_Product;
                    }
                    #endregion
                    #region Lấy thông tin tồn kho


                    var lst_MstProductUI = new List<Mst_ProductUI>();
                    foreach (var item in lsMst_Product)
                    {
                        var productUI = new Mst_ProductUI();
                        productUI.ProductCode = item.ProductCode;
                        productUI.ProductName = item.ProductName;
                        productUI.UnitCode = item.UnitCode;
                        productUI.ProductCodeUser = item.ProductCodeUser;
                        productUI.ProductCodeBase = item.ProductCodeBase;
                        productUI.ProductCodeRoot = item.ProductCodeRoot;
                        productUI.ValConvert = item.ValConvert;

                        if (item.FlagSerial.Equals("1"))
                        {
                            productUI.FlagSerial = "1";
                        }
                        if (item.FlagLot.Equals("1"))
                        {
                            productUI.FlagLo = "1";
                        }
                        if (item.ProductType != null && item.ProductType.ToString().ToUpper().Equals("COMBO") && item.ValConvert != null)
                        {
                            productUI.FlagCombo = "1";
                        }
                        // Check theo đơn hàng hay không
                        productUI.SellPrice = item.UPSell == null ? 0 : item.UPSell;
                        //
                        //if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                        //{
                        //    var lstProduct = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode)).ToList();
                        //    if(lstProduct != null && lstProduct.Count != 0)
                        //    {
                        //        var qtyTotalOkMax = lstProduct.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        //        var itemBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(item.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                        //        if (itemBalance != null)
                        //        {
                        //            productUI.InvCode = itemBalance.InvCode;
                        //            productUI.QtyTotalOK = qtyTotalOkMax;
                        //            productUI.DiscountPrice = "0"; // Tạm để
                        //        }
                        //    }                           
                        //}
                        #region["Lấy thông tin hàng hoá cùng base "]
                        var qtytotalok1 = 0.0;
                        var invCodeMax = "";
                        var invCodeMax1 = "";
                        var qtymax = 0.0;
                        var strWhereClauseTonKho1 = "";
                        var tbMst_Inventory = "Mst_Inventory.";
                        var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                        var sb_SQL13 = new StringBuilder();
                        if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                        {
                            sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", productUI.ProductCodeBase, "=");
                        }


                        var invCodeOut = "I." + objInvF_InventoryOut.InvCodeOut + "%";
                        if (objInvF_InventoryOut.InvCodeOut != null && objInvF_InventoryOut.InvCodeOut != "")
                        {
                            sb_SQL13.AddWhereClause(tbMst_Inventory + "InvBUPattern", invCodeOut, "like");
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
                                qtymax = lstInv_InventoryBalanceTonKho1.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
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
                                productUI.ValOUTAfterDesc = "0";
                                productUI.Qty = "1";

                            }
                        }

                        #endregion
                        lst_MstProductUI.Add(productUI);
                    }
                    #endregion
                    it.lstUnitCodeUIByProduct = lst_MstProductUI;
                }
                #endregion
                ViewBag.Lst_InvF_InventoryOutCover = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutCover;
                //
                ViewBag.lstInvF_InventoryOutInstLot = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot;
                ViewBag.lstInvF_InventoryOutInstSerial = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial;
                ViewBag.lstInvF_InventoryOutDtl = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutDtl;
                ViewBag.lstInvF_InventoryOutQR = objRT_InvF_InventoryOut.Lst_InvF_InventoryOutQR;
                //
                return View(objRT_InvF_InventoryOut);
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
            return View();
        }

        public ActionResult SaveExportCross(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVENTORYOUT_EXPORTCROSS");
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
                var objRQ_InvF_InventoryOut = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryOut>(model);
                var objInvF_InventoryOut = objRQ_InvF_InventoryOut.InvF_InventoryOut;
                objInvF_InventoryOut.OrgID = orgId;
                objInvF_InventoryOut.NetworkID = networkID;
                //objInvF_InventoryOut.OrderType = "";
                objInvF_InventoryOut.UseReceive = "";

                //Thêm 1 số thông tin khác
                objRQ_InvF_InventoryOut.Tid = GetNextTId();
                objRQ_InvF_InventoryOut.GwUserCode = GwUserCode;
                objRQ_InvF_InventoryOut.GwPassword = GwPassword;
                objRQ_InvF_InventoryOut.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InventoryOut.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InventoryOut.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InventoryOut.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InventoryOut.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InventoryOut.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                //


                #region["xử lý upload file"]

                if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile != null && objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile.Count > 0)
                {
                    foreach (var item in objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile)
                    {
                        string filePath = Server.MapPath(FileUtils.GetTempFileVirtualPath(CUtils.StrValue(item.AttachFileSpec), ""));
                        var contractFileSpec = CUtils.ConvertFileToBase64String(filePath);
                        item.AttachFileSpec = contractFileSpec;
                    }
                }
                #endregion
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut);
                InvF_InventoryOutService.Instance.WA_InvF_InventoryOut_SaveAndAppr_Cheo(objRQ_InvF_InventoryOut, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Lưu phiếu xuất chéo thành công!");

                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }

        #endregion

        #region [""]

        /// <summary>
        /// Thêm "RO-" trước RefNo nếu RefType là RO
        /// </summary>
        /// <param name="lstInvF_InventoryOut"></param>
        private void ProcessRefNo(ref List<InvF_InventoryOut> lstInvF_InventoryOut)
        {
            foreach (var item in lstInvF_InventoryOut)
            {
                if (CUtils.StrValue(item.RefType) == RefType.RO)
                {
                    item.RefNo = "RO-" + CUtils.StrValue(item.RefNo);
                }
            }
        }

        #endregion
    }
}