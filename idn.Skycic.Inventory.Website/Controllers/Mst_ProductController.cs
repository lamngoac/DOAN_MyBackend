using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Constants;
using Newtonsoft.Json;
using idn.Skycic.Inventory.Common.Extensions;
using idn.Skycic.Inventory.Website.Utils;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_ProductController : AdminBaseController
    {
        // GET: Mst_Product
        [HttpGet]
        public async Task<ActionResult> Index(
            string productCodeUserName = "",
            string productGrpCode = "",
            string ckbproduct = "",
            string ckbservices = "",
            string ckbcombo = "",
            string flagactive = "",
            string init = "init",
            int page = 0,
            int recordcount = 10)
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MNU_MST_PRODUCT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            ViewBag.UserState = UserState;
            ViewBag.PageCur = page.ToString();
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_ProductUI>(0, recordcount)
            {
                DataList = new List<Mst_ProductUI>()
            };

            #region["Nhóm sản phẩm"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            var objRT_Mst_ProductGroup = WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup);
            ViewBag.ListMstProductGroup = objRT_Mst_ProductGroup;
            #endregion

            #region["Thuộc tính"]
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_Attribute = "*",
                Mst_Attribute = null,
            };

            var objRT_Mst_Attribute = WA_Mst_Attribute_Get(objRQ_Mst_Attribute);
            ViewBag.ListMstAttribute = objRT_Mst_Attribute;
            #endregion

            var listMst_InvoiceType = new List<Mst_Product>();
            var objMst_Product = new Mst_Product()
            {
                ProductLevelSys = "ROOTPRD",
                FlagActive = flagactive,
                FlagFG = "0",
            };
            var strWhereClause_MstProduct = strWhereClause_Mst_Product(objMst_Product);
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
                Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                Ft_RecordCount = recordcount.ToString(),
                Ft_WhereClause = strWhereClause_MstProduct,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = null,
                Rt_Cols_Mst_ProductFiles = null,
                Rt_Cols_Prd_BOM = null,
                Rt_Cols_Prd_Attribute = "*",
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var addressAPIs = UserState.AddressAPIs;
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);

            var listGetProduct = new List<Mst_ProductUI>();
            foreach (var prdroot in objRT_Mst_Product.Lst_Mst_Product)
            {
                var listGetCountBase = new Mst_ProductUI();
                var objMst_ProductBase = new Mst_Product()
                {
                    ProductLevelSys = "BASEPRD",
                    ProductCodeRoot = prdroot.ProductCode,
                    FlagFG = "0",
                };
                var strWhereClause_MstProductBsase = strWhereClause_Mst_Product(objMst_ProductBase);
                var listProductBase = GetPrdBase(strWhereClause_MstProductBsase);
                var countbase = (listProductBase.Lst_Mst_Product.Count + 1).ToString();

                //listGetCountBase.LstPrdBase.AddRange(listProductBase);

                listGetCountBase.CountPrdBase = countbase;
                listGetCountBase.ProductCodeUser = prdroot.ProductCodeUser;
                listGetCountBase.ProductName = prdroot.ProductName;
                listGetCountBase.ProductType = prdroot.ProductType;
                listGetCountBase.ProductGrpCode = prdroot.ProductGrpCode;
                listGetCountBase.BrandCode = prdroot.BrandCode;
                listGetCountBase.UnitCode = prdroot.UnitCode;
                listGetCountBase.FlagBuy = prdroot.FlagBuy;
                listGetCountBase.FlagActive = prdroot.FlagActive;
                listGetCountBase.FlagSell = prdroot.FlagSell;
                listGetCountBase.UPBuy = prdroot.UPBuy;
                listGetCountBase.UPSell = prdroot.UPSell;
                listGetCountBase.ProductCode = prdroot.ProductCode;
                listGetCountBase.ProductCodeBase = prdroot.ProductCodeBase;
                listGetCountBase.ProductCodeRoot = prdroot.ProductCodeRoot;
                listGetCountBase.OrgID = prdroot.OrgID;
                listGetCountBase.NetworkID = prdroot.NetworkID;
                listGetCountBase.mpt_ProductType = prdroot.mpt_ProductType;
                listGetCountBase.mpt_ProductTypeName = prdroot.mpt_ProductTypeName;
                listGetCountBase.mpg_ProductGrpName = prdroot.mpg_ProductGrpName;
                listGetCountBase.mb_BrandName = prdroot.mb_BrandName;

                listGetProduct.Add(listGetCountBase);
            }
            if (listGetProduct != null && listGetProduct.Count > 0)
            {
                pageInfo.DataList = listGetProduct;
                itemCount = objRT_Mst_Product.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Product.MySummaryTable.MyCount);
            }

            pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
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
                Rt_Cols_Prd_Attribute = "*",
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch(
            string productCodeUserName = "",
            string productGrpCode = "",
            string ckbproduct = "",
            string ckbservices = "",
            string ckbcombo = "",
            string flagactive = "",
            string lstattribute = "",
            string init = "init",
            int page = 0,
            int recordcount = 10)
        {
            
            var objListPrd_AttributeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Prd_Attribute>>(lstattribute);
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.init = "search";
                ViewBag.PageCur = page.ToString();
                ViewBag.RecordCount = recordcount.ToString();
                var strStt = "";
                var itemCount = 0;
                var pageCount = 0;

                var pageInfo = new PageInfo<Mst_ProductUI>(0, recordcount)
                {
                    DataList = new List<Mst_ProductUI>()
                };

                #region["CheckBox tìm kiếm"]
                if (!CUtils.IsNullOrEmpty(ckbproduct) && ckbproduct.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'PRODUCT'", strStt) : string.Format("'PRODUCT'", strStt);
                }
                if (!CUtils.IsNullOrEmpty(ckbservices) && ckbservices.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'SERVICE'", strStt) : string.Format("'SERVICE'", strStt);
                }
                if (!CUtils.IsNullOrEmpty(ckbcombo) && ckbcombo.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'COMBO'", strStt) : string.Format("'COMBO'", strStt);
                }
                #endregion

                var objMst_Product = new Mst_Product()
                {
                    ProductLevelSys = "ROOTPRD",
                    ProductCodeUser = productCodeUserName,
                    ProductName = productCodeUserName,
                    ProductGrpCode = productGrpCode,
                    FlagActive = flagactive,
                    FlagFG = "0",
                };

                //if (CUtils.IsNullOrEmpty(ckbproduct) && CUtils.IsNullOrEmpty(ckbservices) && CUtils.IsNullOrEmpty(ckbcombo))
                //{
                //    objMst_Product.FlagActive = "1";
                //}

                var strWhereClause_MstProduct = strWhereClause_Mst_Product_Search(objMst_Product, strStt, objListPrd_AttributeInput);
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhereClause_MstProduct,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = null,
                    Rt_Cols_Mst_ProductFiles = null,
                    Rt_Cols_Prd_BOM = null,
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var addressAPIs = UserState.AddressAPIs;
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                var listGetProduct = new List<Mst_ProductUI>();
                foreach (var prdroot in objRT_Mst_Product.Lst_Mst_Product)
                {
                    var listGetCountBase = new Mst_ProductUI();
                    var objMst_ProductBase = new Mst_Product()
                    {
                        ProductLevelSys = "BASEPRD",
                        ProductCodeRoot = prdroot.ProductCode,
                        FlagFG = "0",
                    };
                    var strWhereClause_MstProductBsase = strWhereClause_Mst_Product(objMst_ProductBase);
                    var listProductBase = GetPrdBase(strWhereClause_MstProductBsase);
                    var countbase = (listProductBase.Lst_Mst_Product.Count + 1).ToString();

                    listGetCountBase.CountPrdBase = countbase;
                    listGetCountBase.ProductCodeUser = prdroot.ProductCodeUser;
                    listGetCountBase.ProductName = prdroot.ProductName;
                    listGetCountBase.ProductType = prdroot.ProductType;
                    listGetCountBase.ProductGrpCode = prdroot.ProductGrpCode;
                    listGetCountBase.BrandCode = prdroot.BrandCode;
                    listGetCountBase.UnitCode = prdroot.UnitCode;
                    listGetCountBase.FlagBuy = prdroot.FlagBuy;
                    listGetCountBase.FlagSell = prdroot.FlagSell;
                    listGetCountBase.UPBuy = prdroot.UPBuy;
                    listGetCountBase.UPSell = prdroot.UPSell;
                    listGetCountBase.ProductCode = prdroot.ProductCode;
                    listGetCountBase.ProductCodeBase = prdroot.ProductCodeBase;
                    listGetCountBase.ProductCodeRoot = prdroot.ProductCodeRoot;
                    listGetCountBase.OrgID = prdroot.OrgID;
                    listGetCountBase.NetworkID = prdroot.NetworkID;
                    listGetCountBase.mpt_ProductType = prdroot.mpt_ProductType;
                    listGetCountBase.mpg_ProductGrpName = prdroot.mpg_ProductGrpName;
                    listGetCountBase.mb_BrandName = prdroot.mb_BrandName;
                    listGetCountBase.FlagActive = prdroot.FlagActive;
                    listGetCountBase.mpt_ProductTypeName = prdroot.mpt_ProductTypeName;

                    listGetProduct.Add(listGetCountBase);
                }
                if (listGetProduct != null && listGetProduct.Count > 0)
                {
                    pageInfo.DataList = listGetProduct;
                    itemCount = objRT_Mst_Product.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Product.MySummaryTable.MyCount);
                }

                pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                return JsonView("BindDataMst_Product", pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #region[LoadData]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadData(string prcode, string prbscode, string prlv, string prroot, string idx)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.Idx = idx;
            try
            {
                if (!CUtils.IsNullOrEmpty(prcode))
                {
                    #region ["Get Base Level2 product"]
                    var objMst_ProductBase = new Mst_Product()
                    {
                        ProductCode = prcode,
                        ProductLevelSys = "L2PRD",
                        FlagFG = "0",
                    };
                    var strWhereClause_MstProductBase = strWhereClause_Mst_Product_Get_Base(objMst_ProductBase);
                    var strWhereClause_MstProductLevel2 = strWhereClause_Mst_Product_Get_Level2(objMst_ProductBase);
                    var objRT_Mst_ProductBase = GetPrdBase(strWhereClause_MstProductBase);
                    var objRT_Mst_ProductLevel2 = GetPrdBase(strWhereClause_MstProductLevel2);
                    var listproductbase = new List<Mst_ProductUI>();
                    var productbase = new Mst_ProductUI();
                    productbase.LstPrdBase = objRT_Mst_ProductBase.Lst_Mst_Product;
                    productbase.LstPrdLeve2 = objRT_Mst_ProductLevel2.Lst_Mst_Product;
                    productbase.LstAttributeBase = objRT_Mst_ProductBase.Lst_Prd_Attribute;
                    productbase.LstAttributeLevel2 = objRT_Mst_ProductLevel2.Lst_Prd_Attribute;
                    listproductbase.Add(productbase);
                    #endregion

                    if (listproductbase != null)
                    {
                        return JsonView("LoadData", listproductbase);
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.RedirectUrl = Url.Action("Update", "Mst_Product", new { prcode = prcode, prbscode = prbscode, prroot = prroot, prlv = prlv });
                        return Json(resultEntry);
                    }
                }
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("LoadData", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search_Product_Auto(string productcodename)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            
            try
            {
                var listMst_ProductAutoSearch = new List<Mst_Product>();

                if (!CUtils.IsNullOrEmpty(productcodename))
                {
                    var strWhereClauseMst_Product = strWhereClause_Mst_ProductProductUserCodeOrProductName(new Mst_Product()
                    {
                        ProductCodeUser = productcodename,
                        ProductName = productcodename,
                        NetworkID = UserState.Mst_NNT.NetworkID,
                        OrgID = UserState.Mst_NNT.OrgID,
                        FlagActive = Client_Flag.Active,
                        FlagFG = "0",
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
                        Ft_RecordCount = "10", // hỗ trợ lấy tối đa 10 bản ghi
                        Ft_WhereClause = strWhereClauseMst_Product,
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
                    var obj_RT_Mst_Product = RT_Mst_Product_Get(objRQ_Mst_Product);
                    if (obj_RT_Mst_Product != null && obj_RT_Mst_Product.Lst_Mst_Product != null &&
                        obj_RT_Mst_Product.Lst_Mst_Product.Count > 0)
                    {
                        foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                        {
                            listMst_ProductAutoSearch.Add(item);
                        }
                    }
                }
                return Json(new { Success = true, ListProduct = listMst_ProductAutoSearch });
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
            return Json(new { Success = true, Detail = resultEntry.Detail });
        }
        #endregion

        #region["Tạo sản phẩm"]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_MST_PRODUCT_CREATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            string culture = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            
            #region["Mã sản phẩm"]
            var productCode = "";
            
            ViewBag.ProductCode = productCode;
            #endregion
            ViewBag.NetworkID = UserState.Mst_NNT.NetworkID;
            ViewBag.OrgID = UserState.Mst_NNT.OrgID;
            #region["Loại hàng"]
            var objRQ_Mst_ProductType = new RQ_Mst_ProductType()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductType
                Rt_Cols_Mst_ProductType = "*",
                Mst_ProductType = null,
            };
            var list_Mst_ProductType_Async = List_Mst_ProductType_Async(objRQ_Mst_ProductType);
            #endregion
            #region["Nhóm sản phẩm"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            var list_Mst_ProductGroup_Async = List_Mst_ProductGroup_Async(objRQ_Mst_ProductGroup);
            #endregion
            #region["Nhãn hiệu"]
            var objRQ_Mst_Brand = new RQ_Mst_Brand()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Brand
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            var list_Mst_Brand_Async = List_Mst_Brand_Async(objRQ_Mst_Brand);
            #endregion
            #region["Thông tin bổ sung"]
            var objPrd_DynamicField = new Prd_DynamicField()
            {
                OrgID = UserState.Mst_NNT.OrgID
            };
            var str_WhereClause_Prd_DynamicField = strWhereClause_Prd_DynamicField(objPrd_DynamicField);
            var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
            {
                // WARQBase
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
                Ft_WhereClause = str_WhereClause_Prd_DynamicField,
                Ft_Cols_Upd = null,
                // RQ_Prd_DynamicField
                Rt_Cols_Prd_DynamicField = "*",
                Prd_DynamicField = null,
            };
            var list_Prd_DynamicField_Async = List_Prd_DynamicField_Async(objRQ_Prd_DynamicField);
            #endregion
            #region["Thuộc tính"]
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_Attribute = "*",
                Mst_Attribute = null,
            };
            var list_Mst_Attribute_Async = List_Mst_Attribute_Async(objRQ_Mst_Attribute);
            #endregion
            #region["Mst_VATRate"]
            var objRQ_Mst_VATRate = new RQ_Mst_VATRate()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_VATRate = "*",
                Mst_VATRate = null,
            };
            var list_Mst_VATRate_Async = List_Mst_VATRate_Async(objRQ_Mst_VATRate);
            #endregion

            #region["Trường động"]
            var listProduct_CustomField = new List<Product_CustomField>();
            var strWhereClause_Product_CustomField = strWhereClause_Product_CustomField_Search(new Product_CustomField()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                FlagActive = "1"
            });
            var objRQ_Product_CustomField = new RQ_Product_CustomField()
            {
                // WARQBase
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
                Ft_RecordCount = "5",
                Ft_WhereClause = strWhereClause_Product_CustomField,
                Ft_Cols_Upd = null,
                // RQ_Mst_SpecCustomField
                Rt_Cols_Product_CustomField = "*",
                Product_CustomField = null,
            };
            var listProduct_CustomField_Async = List_Product_CustomField_Async(objRQ_Product_CustomField);
            #endregion

            await Task.WhenAll(list_Mst_ProductType_Async, list_Mst_Attribute_Async, list_Mst_ProductGroup_Async, list_Mst_Brand_Async, list_Prd_DynamicField_Async, list_Mst_VATRate_Async, listProduct_CustomField_Async);
            var listMst_ProductType = new List<Mst_ProductType>();
            var listMst_ProductGroup = new List<Mst_ProductGroup>();
            var _listMst_ProductGroupUI = new List<Mst_ProductGroupExt>();
            var listMst_ProductGroupUI = new List<Mst_ProductGroupExt>();
            var listMst_Brand = new List<Mst_Brand>();
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var listMst_Attribute = new List<Mst_Attribute>();
            var listMst_VATRate = new List<Mst_VATRate>();

            if (list_Mst_ProductType_Async != null && list_Mst_ProductType_Async.Result != null &&
                list_Mst_ProductType_Async.Result.Count > 0)
            {
                listMst_ProductType.AddRange(list_Mst_ProductType_Async.Result);
            }
            if (list_Mst_ProductGroup_Async != null && list_Mst_ProductGroup_Async.Result != null &&
                list_Mst_ProductGroup_Async.Result.Count > 0)
            {
                listMst_ProductGroup.AddRange(list_Mst_ProductGroup_Async.Result);
                foreach (var item in listMst_ProductGroup)
                {
                    var objMst_ProductGroupUI = new Mst_ProductGroupExt()
                    {
                        OrgID = item.OrgID,
                        ProductGrpCode = item.ProductGrpCode,
                        NetworkID = item.NetworkID,
                        ProductGrpCodeParent = item.ProductGrpCodeParent,
                        ProductGrpBUCode = item.ProductGrpBUCode,
                        ProductGrpBUPattern = item.ProductGrpBUPattern,
                        ProductGrpName = item.ProductGrpName,
                        ProductGrpLevel = item.ProductGrpLevel,
                        FlagActive = item.FlagActive,
                        LogLUDTimeUTC = item.LogLUDTimeUTC,
                        LogLUBy = item.LogLUBy,
                    };
                    _listMst_ProductGroupUI.Add(objMst_ProductGroupUI);
                }
                var toGroupBaseTree = Mst_ProductGroupExtension.ToGroupBaseTree(Mst_ProductGroupExtension.GetGroupBaseList(_listMst_ProductGroupUI));
                var toFlatGroupBaseTree = Mst_ProductGroupExtension.ToFlatGroupBaseTree(toGroupBaseTree);
                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    listMst_ProductGroupUI.AddRange(toFlatGroupBaseTree);

                }
            }
            if (list_Mst_Brand_Async != null && list_Mst_Brand_Async.Result != null &&
                list_Mst_Brand_Async.Result.Count > 0)
            {
                listMst_Brand.AddRange(list_Mst_Brand_Async.Result);
            }
            if (list_Prd_DynamicField_Async != null && list_Prd_DynamicField_Async.Result != null &&
                list_Prd_DynamicField_Async.Result.Count > 0)
            {
                listPrd_DynamicField.AddRange(list_Prd_DynamicField_Async.Result);
            }
            if (list_Mst_Attribute_Async != null && list_Mst_Attribute_Async.Result != null &&
                list_Mst_Attribute_Async.Result.Count > 0)
            {
                listMst_Attribute.AddRange(list_Mst_Attribute_Async.Result);
            }
            if (list_Mst_VATRate_Async != null && list_Mst_VATRate_Async.Result != null &&
                list_Mst_VATRate_Async.Result.Count > 0)
            {
                listMst_VATRate.AddRange(list_Mst_VATRate_Async.Result);
            }
            if (listProduct_CustomField_Async != null && listProduct_CustomField_Async.Result.Count > 0)
            {
                listProduct_CustomField.AddRange(listProduct_CustomField_Async.Result);
            }
            string sJSONResponse = JsonConvert.SerializeObject(listMst_Attribute);
            ViewBag.List_Mst_ProductType = listMst_ProductType;
            ViewBag.List_Mst_ProductGroup = listMst_ProductGroup;
            ViewBag.List_Mst_ProductGroupUI = listMst_ProductGroupUI;
            ViewBag.List_Mst_Brand = listMst_Brand;
            ViewBag.List_Prd_DynamicField = listPrd_DynamicField;
            ViewBag.List_Mst_Attribute = listMst_Attribute;
            ViewBag.List_Mst_VATRate = listMst_VATRate;
            ViewBag.ListProduct_CustomField = listProduct_CustomField;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Mst_ProductUI = new RQ_Mst_ProductUI();
                objRQ_Mst_ProductUI = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Mst_ProductUI>(model);
                var listMst_ProductUI = objRQ_Mst_ProductUI.Lst_Mst_ProductUI_Create;
                var listBOM = new List<Prd_BOM>();
                var listMst_Product = new List<Mst_Product>();
                var listPrd_Attribute = new List<Prd_Attribute>();
                var listMst_ProductImages = new List<Mst_ProductImages>();
                var listMst_ProductFiles = new List<Mst_ProductFiles>();
                var objRQ_Seq_ObjCode = new RQ_Seq_ObjCode()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_Cols_Upd = null,

                    ObjCodeAmount = listMst_ProductUI.Count,
                };

                var listProductCode = WA_Seq_GenObjCode_Get(objRQ_Seq_ObjCode);
                var productCodeRoot = "";
                var productCodeBase = "";
                for (var i = 0; i < listMst_ProductUI.Count; i++)
                {
                    if (i == 0)
                    {
                        productCodeRoot = CUtils.StrValue(listProductCode[i]);
                    }
                    if (CUtils.StrValue(listMst_ProductUI[i].ProductLevelSys).Equals(Client_ProductLevelSys.RootPrd) || CUtils.StrValue(listMst_ProductUI[i].ProductLevelSys).Equals(Client_ProductLevelSys.BasePrd))
                    {
                        productCodeBase = CUtils.StrValue(listProductCode[i]);
                    }

                    var objMst_Product = new Mst_Product();
                    objMst_Product.OrgID = UserState.Mst_NNT.OrgID;
                    objMst_Product.NetworkID = UserState.Mst_NNT.NetworkID;
                    objMst_Product.ProductCode = CUtils.StrValue(listProductCode[i]);
                    objMst_Product.ProductLevelSys = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductLevelSys);
                    objMst_Product.ProductCodeUser = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductCodeUser);
                    objMst_Product.BrandCode = CUtils.StrValueOrNull(listMst_ProductUI[i].BrandCode);
                    objMst_Product.ProductType = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductType);
                    objMst_Product.ProductGrpCode = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductGrpCode);
                    objMst_Product.ProductName = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductName);
                    objMst_Product.ProductNameEN = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductNameEN);
                    objMst_Product.ProductBarCode = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductBarCode);
                    objMst_Product.ProductCodeNetwork = null;
                    objMst_Product.ProductCodeBase = productCodeBase;
                    objMst_Product.ProductCodeRoot = productCodeRoot;
                    objMst_Product.ProductImagePathList = null;
                    objMst_Product.ProductFilePathList = null;
                    objMst_Product.UnitCode = CUtils.StrValueOrNull(listMst_ProductUI[i].UnitCode);
                    objMst_Product.ValConvert = CUtils.StrValueOrNull(listMst_ProductUI[i].ValConvert);
                    objMst_Product.FlagSell = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagSell); // cờ bán
                    objMst_Product.FlagBuy = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagBuy); // cờ mua
                    objMst_Product.UPBuy = CUtils.StrValueOrNull(listMst_ProductUI[i].UPBuy);
                    objMst_Product.UPSell = CUtils.StrValueOrNull(listMst_ProductUI[i].UPSell);
                    objMst_Product.QtyMaxSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyMaxSt);
                    objMst_Product.QtyMinSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyMinSt);
                    objMst_Product.QtyEffSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyEffSt);
                    objMst_Product.ListOfPrdDynamicFieldValue = CUtils.StrValueOrNull(listMst_ProductUI[i].ListOfPrdDynamicFieldValue); // danh sách trường động
                    objMst_Product.Remark = CUtils.StrValueOrNull(listMst_ProductUI[i].Remark);
                    objMst_Product.VATRateCode = CUtils.StrValueOrNull(listMst_ProductUI[i].VATRateCode);
                    objMst_Product.CustomField2 = CUtils.StrValueOrNull(listMst_ProductUI[i].CustomField2);
                    objMst_Product.CustomField3 = CUtils.StrValueOrNull(listMst_ProductUI[i].CustomField3);
                    objMst_Product.CustomField4 = CUtils.StrValueOrNull(listMst_ProductUI[i].CustomField4);
                    objMst_Product.CustomField5 = CUtils.StrValueOrNull(listMst_ProductUI[i].CustomField5);
                    objMst_Product.FlagFG = "0";
                    // Trang tập hợp dữ liệu gán ở đây
                    // dữ liệu bảng hàng hóa gán vào listBOM
                    objMst_Product.FlagSerial = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagSerial);
                    objMst_Product.FlagLot = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagLot);
                    objMst_Product.ProductStd = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductStd);
                    objMst_Product.ProductExpiry = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductExpiry);
                    objMst_Product.ProductQuyCach = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductQuyCach);
                    objMst_Product.ProductMnfUrl = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductMnfUrl);
                    objMst_Product.ProductIntro = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductIntro);
                    objMst_Product.ProductUserGuide = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductUserGuide);
                    objMst_Product.ProductDrawing = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductDrawing);
                    objMst_Product.ProductOrigin = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductOrigin);


                    listMst_Product.Add(objMst_Product);

                    if (listMst_ProductUI[i].Lst_Prd_Attribute != null && listMst_ProductUI[i].Lst_Prd_Attribute.Count > 0)
                    {
                        foreach (var item in listMst_ProductUI[i].Lst_Prd_Attribute)
                        {
                            var objPrd_Attribute = new Prd_Attribute();
                            objPrd_Attribute.OrgID = UserState.Mst_NNT.OrgID;
                            objPrd_Attribute.NetworkID = UserState.Mst_NNT.NetworkID;
                            objPrd_Attribute.ProductCode = objMst_Product.ProductCode;
                            objPrd_Attribute.AttributeCode = item.AttributeCode;
                            objPrd_Attribute.AttributeValue = item.AttributeValue;

                            listPrd_Attribute.Add(objPrd_Attribute);
                        }
                    }

                    //List images
                    if (objRQ_Mst_ProductUI.Lst_Mst_ProductImages != null && objRQ_Mst_ProductUI.Lst_Mst_ProductImages.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductImages)
                        {
                            var objMst_Product_Image = new Mst_ProductImages()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                                Idx = item.Idx,
                                FlagIsImagePath = "0",
                                ProductCode = objMst_Product.ProductCode,
                                ProductImageName = item.ProductImageName,
                                ProductImageSpec = item.ProductImageSpec,
                            };
                            listMst_ProductImages.Add(objMst_Product_Image);
                        }
                    }
                    //list files
                    if (objRQ_Mst_ProductUI.Lst_Mst_ProductFiles != null && objRQ_Mst_ProductUI.Lst_Mst_ProductFiles.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductFiles)
                        {
                            var objMst_Product_File = new Mst_ProductFiles()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                                Idx = item.Idx,
                                FlagIsFilePath = "0",
                                ProductCode = objMst_Product.ProductCode,
                                ProductFileName = item.ProductFileName,
                                ProductFileDesc = item.ProductFileDesc,
                            };
                            listMst_ProductFiles.Add(objMst_Product_File);
                        }
                    }
                    // ListBom
                    if (listMst_ProductUI[i].ProductType.Equals("COMBO"))
                    {
                        if (objRQ_Mst_ProductUI.Lst_Prd_BOM != null && objRQ_Mst_ProductUI.Lst_Prd_BOM.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_ProductUI.Lst_Prd_BOM)
                            {
                                var objMst_Product_Bom = new Prd_BOM()
                                {
                                    OrgID = UserState.Mst_NNT.OrgID,
                                    ProductCode = item.ProductCode,
                                    OrgIDParent = UserState.Mst_NNT.OrgID,
                                    ProductCodeParent = listProductCode[i],
                                    Qty = item.Qty,
                                    mp_UPSell = item.mp_UPBuy,
                                    mp_UPBuy = item.mp_UPSell,
                                    NetworkID = UserState.Mst_NNT.NetworkID,
                                };
                                listBOM.Add(objMst_Product_Bom);
                            }
                        }
                    }
                }
                //listBom of producttype != COMBO
                if (!listMst_ProductUI[0].ProductType.Equals("COMBO"))
                {
                    if (objRQ_Mst_ProductUI.Lst_Prd_BOM != null && objRQ_Mst_ProductUI.Lst_Prd_BOM.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Prd_BOM)
                        {
                            var objMst_Product_Bom = new Prd_BOM()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                ProductCode = item.ProductCode,
                                OrgIDParent = UserState.Mst_NNT.OrgID,
                                ProductCodeParent = productCodeRoot,
                                Qty = item.Qty,
                                mp_UPSell = item.mp_UPBuy,
                                mp_UPBuy = item.mp_UPSell,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                            };
                            listBOM.Add(objMst_Product_Bom);
                        }
                    }
                }

                var objRQ_Mst_Product = new RQ_Mst_Product();
                objRQ_Mst_Product.Tid = GetNextTId();
                objRQ_Mst_Product.GwUserCode = OS_ProductCentrer_GwUserCode;
                objRQ_Mst_Product.GwPassword = OS_ProductCentrer_GwPassword;
                objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword); ;
                objRQ_Mst_Product.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID); 
                objRQ_Mst_Product.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID); 
                objRQ_Mst_Product.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_Mst_Product.FuncType = null;
                objRQ_Mst_Product.Ft_RecordStart = null;
                objRQ_Mst_Product.Ft_RecordCount = null;
                objRQ_Mst_Product.Ft_WhereClause = null;
                objRQ_Mst_Product.Ft_Cols_Upd = null;
                objRQ_Mst_Product.Lst_Mst_Product = new List<Mst_Product>();
                if (listMst_Product != null && listMst_Product.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Mst_Product.AddRange(listMst_Product);
                }
                objRQ_Mst_Product.Lst_Prd_Attribute = new List<Prd_Attribute>();
                if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Prd_Attribute.AddRange(listPrd_Attribute);
                }
                // Danh sách BOM
                objRQ_Mst_Product.Lst_Prd_BOM = new List<Prd_BOM>();
                if (listBOM != null && listBOM.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Prd_BOM.AddRange(listBOM);
                }
                // Danh sách Files
                objRQ_Mst_Product.Lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
                if (listMst_ProductFiles != null && listMst_ProductFiles.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Mst_ProductFiles.AddRange(listMst_ProductFiles);
                }
                // Danh sách Images
                objRQ_Mst_Product.Lst_Mst_ProductImages = new List<Mst_ProductImages>();
                if (listMst_ProductImages != null && listMst_ProductImages.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Mst_ProductImages.AddRange(listMst_ProductImages);
                }
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                Mst_ProductService.Instance.WA_Mst_Product_Create(objRQ_Mst_Product, UserState.AddressAPIs_Product_Customer_Centrer);
                // Lưu tại network
                objRQ_Mst_Product.GwUserCode = GwUserCode;
                objRQ_Mst_Product.GwPassword = GwPassword;
                objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_Mst_Product.FlagIsDelete = "0";
                Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Hàng hóa đã được tạo thành công!");
                var flagRedirect = CUtils.StrValue(objRQ_Mst_ProductUI.FlagRedirect);
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

        #region["Sửa sản phẩm"]
        #region[Lấy sản phẩm khi autocomplete]
        [HttpPost]
        public ActionResult GetBOM(string prdid = "")
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            var objMst_Product = new Mst_Product()
            {
                ProductCodeUser = prdid,
                ProductName = prdid,
                FlagFG = "0",
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
                Rt_Cols_Mst_ProductImages = "*",
                Rt_Cols_Mst_ProductFiles = "*",
                Rt_Cols_Prd_BOM = "*",
                Rt_Cols_Prd_Attribute = "*",
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var objRT_Mst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);
            var objRT_Prd_Bom = WA_Prd_BOM_Get(objRQ_Mst_Product);
            var listRT_Mst_Product = new RT_Mst_Product();
            listRT_Mst_Product.Lst_Prd_BOM = objRT_Prd_Bom;
            //foreach (var bom in objRT_Mst_Product)
            //{
            //    var listBom = new Mst_ProductUI_Update();
            //    objRT_Prd_Bom = listBom.Lst_Prd_BOM;
            //    listMst_ProductUI.Add(listBom);
            //}

            return Json(new { Data = objRT_Mst_Product });
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
                    FlagFG = "0",
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
                    Rt_Cols_Mst_ProductImages = "*",
                    Rt_Cols_Mst_ProductFiles = "*",
                    Rt_Cols_Prd_BOM = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
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
            return JsonViewError("ProductIngredient", null, resultEntry);
        }

        public ActionResult GetSearch(
            string productCodeUserName = "",
            string productGrpCode = "",
            string ckbproduct = "",
            string ckbservices = "",
            string ckbcombo = "",
            string lstattribute = "",
            string init = "init",
            int page = 0,
            int recordcount = 10)
        {
            
            var objListPrd_AttributeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Prd_Attribute>>(lstattribute);

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.init = "search";
                ViewBag.PageCur = page.ToString();
                ViewBag.RecordCount = recordcount.ToString();
                var strStt = "";
                var itemCount = 0;
                var pageCount = 0;

                var pageInfo = new PageInfo<Mst_Product>(0, recordcount)
                {
                    DataList = new List<Mst_Product>()
                };

                #region["CheckBox tìm kiếm"]
                if (!CUtils.IsNullOrEmpty(ckbproduct) && ckbproduct.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'PRODUCT'", strStt) : string.Format("'PRODUCT'", strStt);
                }
                if (!CUtils.IsNullOrEmpty(ckbservices) && ckbservices.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'SERVICE'", strStt) : string.Format("'SERVICE'", strStt);
                }
                if (!CUtils.IsNullOrEmpty(ckbcombo) && ckbcombo.Equals("1"))
                {
                    strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'COMBO'", strStt) : string.Format("'COMBO'", strStt);
                }
                #endregion

                var objMst_Product = new Mst_Product()
                {
                    //ProductLevelSys = "ROOTPRD",
                    ProductCodeUser = productCodeUserName,
                    ProductName = productCodeUserName,
                    ProductGrpCode = productGrpCode,
                    FlagFG = "0",
                };

                if (CUtils.IsNullOrEmpty(ckbproduct) && CUtils.IsNullOrEmpty(ckbservices) && CUtils.IsNullOrEmpty(ckbcombo))
                {
                    objMst_Product.FlagActive = "1";
                }

                var strWhereClause_MstProduct = strWhereClause_Mst_Product_Search(objMst_Product, strStt, objListPrd_AttributeInput);
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
                    //Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    //Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhereClause_MstProduct,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = null,
                    Rt_Cols_Mst_ProductFiles = null,
                    Rt_Cols_Prd_BOM = null,
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var addressAPIs = UserState.AddressAPIs;
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                var RT_Mst_Product = new RT_Mst_Product();
                RT_Mst_Product.Lst_Mst_Product = objRT_Mst_Product.Lst_Mst_Product;
                return JsonView("SearchProduct", objRT_Mst_Product);
            }
            //catch (ValidationException valEx)
            //{
            //    foreach (var ver in valEx.ValidationResult)
            //    {
            //        resultEntry.AddFieldError(ver.FieldName, ver.ErrorMessage);
            //    }
            //}
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion
        #region
        public PartialViewResult ProductIngredient(string productCodeUserName = "",
            string modelproductGrpCode = "",
            string init = "init",
            int page = 0,
            int pagecount = 10)
        {
            
            var listprd = new List<Mst_Product>();
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.init = "search";
                ViewBag.PageCur = page.ToString();
                ViewBag.PageCount = pagecount.ToString();
                #region["group"]
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
                    Ft_WhereClause = "",
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_ProductGroup = "*",
                    Mst_ProductGroup = null,
                };
                var list_Mst_ProductGroup_Async = List_Mst_ProductGroup_Async(objRQ_Mst_ProductGroup);
                ViewBag.ListMstProductGroup = list_Mst_ProductGroup_Async.Result;
                #endregion
                var objMst_Product = new Mst_Product()
                {
                    ProductLevelSys = "ROOTPRD",
                    ProductCodeUser = productCodeUserName,
                    FlagFG = "0",
                };

                var strWhereClause_MstProduct = strWhereClause_Mst_Product(objMst_Product);
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
                    Ft_WhereClause = strWhereClause_MstProduct,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = null,
                    Rt_Cols_Mst_ProductFiles = null,
                    Rt_Cols_Prd_BOM = null,
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var lstMst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);
                ViewBag.List_Mst_Product = lstMst_Product;
                return PartialView();
            }
            //catch (ValidationException valEx)
            //{
            //    foreach (var ver in valEx.ValidationResult)
            //    {
            //        resultEntry.AddFieldError(ver.FieldName, ver.ErrorMessage);
            //    }
            //}
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return PartialView(listprd);
        }
        #endregion
        [HttpGet]
        public async Task<ActionResult> Update(string prcode, string prbscode, string prroot, string prlv)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_MST_PRODUCT_UPDATE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            ViewBag.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
            ViewBag.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var objRT_Mst_Product = new RT_Mst_Product();
            var objMst_Product_Root = new Mst_Product();
            var objMst_Product = new Mst_Product();
            var listUnitCode = new List<Mst_Product>();
            var unitCodeBase = "";
            if (prlv.Equals(Client_ProductLevelSys.RootPrd) || prlv.Equals(Client_ProductLevelSys.BasePrd))
            {
                // tìm kiếm sản phẩm root
                objMst_Product.ProductCodeRoot = prroot;
                objMst_Product.ProductCodeBase = prbscode;
                //objMst_Product.ProductLevelSys = prlv;
            }
            //else if (prlv.Equals(Client_ProductLevelSys.BasePrd))
            //{
            //    // tìm kiếm sản phẩm base
            //    objMst_Product.ProductCodeBase = prbscode;
            //}
            else if (prlv.Equals(Client_ProductLevelSys.L2Prd))
            {
                // tìm kiếm sản phẩm con
                //objMst_Product.ProductCodeRoot = prroot;
                objMst_Product.ProductCode = prcode;
                objMst_Product_Root.ProductCode = prroot;
                #region ["Get product root"]
                var strWhereClauseMst_Product_Root = strWhereClause_Mst_Product_Update(objMst_Product_Root);
                var objRQ_Mst_Product_Root = new RQ_Mst_Product()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClauseMst_Product_Root,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "*",
                    Rt_Cols_Mst_ProductFiles = "*",
                    Rt_Cols_Prd_BOM = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var obj_RT_Mst_Product_Root = RT_Mst_Product_Get(objRQ_Mst_Product_Root);
                if (obj_RT_Mst_Product_Root != null && obj_RT_Mst_Product_Root.Lst_Mst_Product.Count > 0)
                {
                    unitCodeBase = obj_RT_Mst_Product_Root.Lst_Mst_Product[0].UnitCode.ToString();
                }
                ViewBag.UnitCodeBase = unitCodeBase;
                #endregion
            }

            #region["Hàng hóa"]
            var strWhereClauseMst_Product = strWhereClause_Mst_Product_Update(objMst_Product);
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
                Ft_WhereClause = strWhereClauseMst_Product,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = "*",
                Rt_Cols_Mst_ProductFiles = "*",
                Rt_Cols_Prd_BOM = "*",
                Rt_Cols_Prd_Attribute = "*",
                Lst_Mst_Product = null,
                Lst_Mst_ProductImages = null,
                Lst_Mst_ProductFiles = null,
                Lst_Prd_BOM = null,
                Lst_Prd_Attribute = null,
            };
            var obj_RT_Mst_Product = RT_Mst_Product_Get(objRQ_Mst_Product);
            var objRT_Mst_Product_Async = List_Mst_Product_Async(objRQ_Mst_Product);
            #endregion
            #region["GetBOM"]
            var objRT_Prd_Bom = WA_Prd_BOM_Get(objRQ_Mst_Product);
            objRT_Mst_Product.Lst_Prd_BOM = objRT_Prd_Bom;
            #endregion
            #region["Loại hàng"]
            var objRQ_Mst_ProductType = new RQ_Mst_ProductType()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductType
                Rt_Cols_Mst_ProductType = "*",
                Mst_ProductType = null,
            };
            var list_Mst_ProductType_Async = List_Mst_ProductType_Async(objRQ_Mst_ProductType);
            #endregion
            #region["Nhóm sản phẩm"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            var list_Mst_ProductGroup_Async = List_Mst_ProductGroup_Async(objRQ_Mst_ProductGroup);
            #endregion
            #region["Nhãn hiệu"]
            var objRQ_Mst_Brand = new RQ_Mst_Brand()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Brand
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            var list_Mst_Brand_Async = List_Mst_Brand_Async(objRQ_Mst_Brand);
            #endregion
            #region["Thông tin bổ sung"]
            var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Prd_DynamicField
                Rt_Cols_Prd_DynamicField = "*",
                Prd_DynamicField = null,
            };
            var list_Prd_DynamicField_Async = List_Prd_DynamicField_Async(objRQ_Prd_DynamicField);
            #endregion
            #region["Thuộc tính"]
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_Attribute = "*",
                Mst_Attribute = null,
            };
            var list_Mst_Attribute_Async = List_Mst_Attribute_Async(objRQ_Mst_Attribute);
            #endregion
            #region["Mst_VATRate"]
            var objRQ_Mst_VATRate = new RQ_Mst_VATRate()
            {
                // WARQBase
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_VATRate = "*",
                Mst_VATRate = null,
            };
            var list_Mst_VATRate_Async = List_Mst_VATRate_Async(objRQ_Mst_VATRate);
            #endregion

            #region["Trường động"]
            var listProduct_CustomField = new List<Product_CustomField>();
            var strWhereClause_Product_CustomField = strWhereClause_Product_CustomField_Search(new Product_CustomField()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                FlagActive = "1"
            });
            var objRQ_Product_CustomField = new RQ_Product_CustomField()
            {
                // WARQBase
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
                Ft_RecordCount = "5",
                Ft_WhereClause = strWhereClause_Product_CustomField,
                Ft_Cols_Upd = null,
                // RQ_Mst_SpecCustomField
                Rt_Cols_Product_CustomField = "*",
                Product_CustomField = null,
            };
            var listProduct_CustomField_Async = List_Product_CustomField_Async(objRQ_Product_CustomField);
            #endregion

            await Task.WhenAll(objRT_Mst_Product_Async, list_Mst_ProductType_Async, list_Mst_ProductGroup_Async, list_Mst_Brand_Async, list_Prd_DynamicField_Async, list_Mst_Attribute_Async, list_Mst_VATRate_Async, listProduct_CustomField_Async);
            var listMst_Product_Search = new List<Mst_Product>();
            var listPrd_Attribute_Search = new List<Prd_Attribute>();
            var listMst_ProductType = new List<Mst_ProductType>();
            var listMst_ProductGroup = new List<Mst_ProductGroup>();
            var listMst_Brand = new List<Mst_Brand>();
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var listMst_Attribute = new List<Mst_Attribute>();
            var listMst_VATRate = new List<Mst_VATRate>();
            var listMst_ProductGroupUI = new List<Mst_ProductGroupExt>();
            var _listMst_ProductGroupUI = new List<Mst_ProductGroupExt>();

            if (objRT_Mst_Product_Async != null && objRT_Mst_Product_Async.Result != null)
            {
                objRT_Mst_Product = objRT_Mst_Product_Async.Result;
                if (objRT_Mst_Product.Lst_Mst_Product != null &&
                    objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                {
                    #region["Tìm đơn vị cơ bản"]
                    //// ???
                    ////listUnitCode.AddRange(objRT_Mst_Product.Lst_Mst_Product);
                    var objMst_Mst_Product_Root_Base = objRT_Mst_Product.Lst_Mst_Product.Where(it =>
                        !CUtils.IsNullOrEmpty(it.ProductLevelSys) &&
                        (it.ProductLevelSys.Equals(Client_ProductLevelSys.RootPrd) ||
                         it.ProductLevelSys.Equals(Client_ProductLevelSys.BasePrd))).FirstOrDefault();
                    //if (objMst_Mst_Product_Root_Base != null)
                    //{
                    //    listUnitCode.Add(objMst_Mst_Product_Root_Base);
                    //}
                    // add các đơn vị khác

                    foreach (var item in objRT_Mst_Product.Lst_Mst_Product)
                    {
                        var productLevelSys = CUtils.StrValue(item.ProductLevelSys);
                        if (productLevelSys.Equals(Client_ProductLevelSys.L2Prd))
                        {
                            listUnitCode.Add(item);
                        }
                    }
                    #endregion
                    objRT_Mst_Product.Lst_Mst_Product = new List<Mst_Product>();
                    objRT_Mst_Product.Lst_Mst_Product.Add(objMst_Mst_Product_Root_Base);
                    listMst_Product_Search.AddRange(objRT_Mst_Product.Lst_Mst_Product);
                }

                if (objRT_Mst_Product.Lst_Prd_Attribute != null &&
                    objRT_Mst_Product.Lst_Prd_Attribute.Count > 0)
                {
                    var _listPrd_Attribute_Search = objRT_Mst_Product.Lst_Prd_Attribute.Where(it =>
                        !CUtils.IsNullOrEmpty(it.ProductCode) && it.ProductCode.Equals(prcode)).ToList();
                    if (_listPrd_Attribute_Search != null && _listPrd_Attribute_Search.Count > 0)
                    {
                        listPrd_Attribute_Search.AddRange(_listPrd_Attribute_Search);
                    }
                    objRT_Mst_Product.Lst_Prd_Attribute = new List<Prd_Attribute>();
                    objRT_Mst_Product.Lst_Prd_Attribute.AddRange(listPrd_Attribute_Search);
                }

                // List Files, Images, BOM tương tự nếu có
            }

            if (list_Mst_ProductType_Async != null && list_Mst_ProductType_Async.Result != null &&
                list_Mst_ProductType_Async.Result.Count > 0)
            {
                listMst_ProductType.AddRange(list_Mst_ProductType_Async.Result);
            }
            if (list_Mst_ProductGroup_Async != null && list_Mst_ProductGroup_Async.Result != null &&
                list_Mst_ProductGroup_Async.Result.Count > 0)
            {
                listMst_ProductGroup.AddRange(list_Mst_ProductGroup_Async.Result);
                foreach (var item in listMst_ProductGroup)
                {
                    var objMst_ProductGroupUI = new Mst_ProductGroupExt()
                    {
                        OrgID = item.OrgID,
                        ProductGrpCode = item.ProductGrpCode,
                        NetworkID = item.NetworkID,
                        ProductGrpCodeParent = item.ProductGrpCodeParent,
                        ProductGrpBUCode = item.ProductGrpBUCode,
                        ProductGrpBUPattern = item.ProductGrpBUPattern,
                        ProductGrpName = item.ProductGrpName,
                        ProductGrpLevel = item.ProductGrpLevel,
                        FlagActive = item.FlagActive,
                        LogLUDTimeUTC = item.LogLUDTimeUTC,
                        LogLUBy = item.LogLUBy,
                    };
                    _listMst_ProductGroupUI.Add(objMst_ProductGroupUI);
                }
                var toGroupBaseTree = Mst_ProductGroupExtension.ToGroupBaseTree(Mst_ProductGroupExtension.GetGroupBaseList(_listMst_ProductGroupUI));
                var toFlatGroupBaseTree = Mst_ProductGroupExtension.ToFlatGroupBaseTree(toGroupBaseTree);
                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    listMst_ProductGroupUI.AddRange(toFlatGroupBaseTree);

                }
            }
            if (list_Mst_Brand_Async != null && list_Mst_Brand_Async.Result != null &&
                list_Mst_Brand_Async.Result.Count > 0)
            {
                listMst_Brand.AddRange(list_Mst_Brand_Async.Result);
            }
            if (list_Prd_DynamicField_Async != null && list_Prd_DynamicField_Async.Result != null &&
                list_Prd_DynamicField_Async.Result.Count > 0)
            {
                listPrd_DynamicField.AddRange(list_Prd_DynamicField_Async.Result);
            }
            if (list_Mst_Attribute_Async != null && list_Mst_Attribute_Async.Result != null &&
                list_Mst_Attribute_Async.Result.Count > 0)
            {
                listMst_Attribute.AddRange(list_Mst_Attribute_Async.Result);
            }
            if (list_Mst_VATRate_Async != null && list_Mst_VATRate_Async.Result != null &&
                list_Mst_VATRate_Async.Result.Count > 0)
            {
                listMst_VATRate.AddRange(list_Mst_VATRate_Async.Result);
            }

            if (listProduct_CustomField_Async != null && listProduct_CustomField_Async.Result.Count > 0)
            {
                listProduct_CustomField.AddRange(listProduct_CustomField_Async.Result);
            }
            var domainCur = UserState.AddressAPIs_Product_Customer_Centrer + "api/File/";
            var rex = new System.Text.RegularExpressions.Regex(domainCur);
            var listProductImages = new List<Mst_ProductImages>();
            var listProductFiles = new List<Mst_ProductFiles>();

            if (objRT_Mst_Product.Lst_Mst_ProductImages != null && objRT_Mst_Product.Lst_Mst_ProductImages.Count > 0)
            {
                foreach (var item in objRT_Mst_Product.Lst_Mst_ProductImages)
                {
                    if (!CUtils.IsNullOrEmpty(item.ProductImagePath))
                    {
                        if (!item.ProductImagePath.ToString().Contains("UploadedFiles"))
                        {
                            item.ProductImagePath = item.ProductImagePath;
                        }
                        else
                        {
                            if (item.ProductImagePath.ToString().Contains(rex.ToString()))
                            {
                                item.ProductImagePath = item.ProductImagePath;
                            }
                            else
                            {
                                item.ProductImagePath = domainCur + item.ProductImagePath.ToString().Replace(@"\", @"/");
                            }
                        }
                    }

                    listProductImages.Add(item);
                }
            }
            if (objRT_Mst_Product.Lst_Mst_ProductFiles != null && objRT_Mst_Product.Lst_Mst_ProductFiles.Count > 0)
            {
                foreach (var item in objRT_Mst_Product.Lst_Mst_ProductFiles)
                {
                    if (!CUtils.IsNullOrEmpty(item.ProductFilePath))
                    {
                        if (!item.ProductFilePath.ToString().Contains("UploadedFiles"))
                        {
                            item.ProductFilePath = item.ProductFilePath;
                        }
                        else
                        {
                            if (item.ProductFilePath.ToString().Contains(rex.ToString()))
                            {
                                item.ProductFilePath = item.ProductFilePath;
                            }
                            else
                            {
                                item.ProductFilePath = domainCur + item.ProductFilePath.ToString().Replace(@"\", @"/");
                            }
                        }
                    }

                    listProductFiles.Add(item);
                }
            }
            string sJSONResponse = JsonConvert.SerializeObject(listMst_Attribute);
            ViewBag.ListUnitCode = listUnitCode;
            ViewBag.List_Mst_ProductType = listMst_ProductType;
            ViewBag.List_Mst_ProductGroup = listMst_ProductGroup;
            ViewBag.List_Mst_Brand = listMst_Brand;
            ViewBag.List_Prd_DynamicField = listPrd_DynamicField;
            ViewBag.List_Mst_Attribute = listMst_Attribute;
            ViewBag.List_Mst_ProductImages = listProductImages;
            ViewBag.List_Mst_ProductFiles = listProductFiles;
            ViewBag.List_Mst_ProductGroupUI = listMst_ProductGroupUI;
            ViewBag.List_Mst_VATRate = listMst_VATRate;
            ViewBag.ListProduct_CustomField = listProduct_CustomField;



            return View(obj_RT_Mst_Product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(string model)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            var strFt_Cols_Upd = "";
            var objRQ_Mst_Product = new RQ_Mst_Product()
            {
                Lst_Mst_Product = new List<Mst_Product>(),
                Lst_Prd_Attribute = new List<Prd_Attribute>(),
                Lst_Mst_ProductImages = new List<Mst_ProductImages>(),
                Lst_Mst_ProductFiles = new List<Mst_ProductFiles>(),
                Lst_Prd_BOM = new List<Prd_BOM>()
            };
            try
            {
                var objRQ_Mst_ProductUI = new RQ_Mst_ProductUI();
                objRQ_Mst_ProductUI = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Mst_ProductUI>(model);
                var listMst_ProductUI = objRQ_Mst_ProductUI.Lst_Mst_ProductUI_Create; // sản phẩm mới khi thêm đơn vị khác đơn vị cơ bản                
                var listMst_Product = new List<Mst_Product>();
                var listPrd_Attribute = new List<Prd_Attribute>();
                var listMst_ProductImages = new List<Mst_ProductImages>();
                var listMst_ProductFiles = new List<Mst_ProductFiles>();
                var listMst_ProductBOM = new List<Prd_BOM>();
                //objRQ_Mst_ProductUI.Mst_Product.FlagActive = "1";
                var objMst_Product_Input = objRQ_Mst_ProductUI.Mst_Product;
               
                var objMst_Product_Search = new Mst_Product();
                var strWhereClauseMst_Product = "";
                var flagRedirect = CUtils.StrValue(objRQ_Mst_ProductUI.FlagRedirect);
                objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClauseMst_Product,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = null,
                    Rt_Cols_Mst_ProductFiles = null,
                    Rt_Cols_Prd_BOM = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var domainCur = UserState.AddressAPIs_Product_Customer_Centrer + "api/File/";
                var rex = new System.Text.RegularExpressions.Regex(domainCur);

                if (objMst_Product_Input.ProductLevelSys.Equals(Client_ProductLevelSys.L2Prd))
                {
                    // Không phải sản phẩm root hoặc base (sản phẩm cấp 2)
                    // Attribute: không được thêm, sửa, xóa
                    // Unit: không được thêm, sửa, xóa
                    // chỉ cập nhật thông tin cơ bản
                    objMst_Product_Search.ProductCode = CUtils.StrValueOrNull(objMst_Product_Input.ProductCode);
                    strWhereClauseMst_Product = strWhereClause_Mst_Product_Update(objMst_Product_Search);
                    objRQ_Mst_Product.Ft_WhereClause = strWhereClauseMst_Product;
                    var obj_RT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, UserState.AddressAPIs);
                    var objMst_Product_Edit = obj_RT_Mst_Product.Lst_Mst_Product[0];
                    var listAttribute = obj_RT_Mst_Product.Lst_Prd_Attribute;

                    objMst_Product_Edit = MapProduct(objMst_Product_Edit, objMst_Product_Input);

                    var Tbl_Mst_Product = TableName.Mst_Product + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductCodeUser);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductBarCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductNameEN);
                    // Xử lý thông tin BOM, Files, Images

                    #region ["List images"]
                    if (objRQ_Mst_ProductUI.Lst_Mst_ProductImages != null && objRQ_Mst_ProductUI.Lst_Mst_ProductImages.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductImages)
                        {
                            var objMst_Product_Image = new Mst_ProductImages()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                                Idx = item.Idx,
                                ProductCode = objMst_Product_Input.ProductCode,
                                ProductImageName = item.ProductImageName,
                                ProductImageSpec = item.ProductImageSpec,
                            };
                            //if (item.FlagIsImagePath.ToString().Equals("1"))
                            //{
                            //    if (!CUtils.IsNullOrEmpty(item.ProductImagePath))
                            //    {
                            //        if (item.ProductImagePath.ToString().Contains("UploadedFiles"))
                            //        {
                            //            objMst_Product_Image.ProductImagePath = rex.Split(item.ProductImagePath.ToString())[1];
                            //        }
                            //        else
                            //        {
                            //            objMst_Product_Image.ProductImagePath = item.ProductImagePath;
                            //        }
                            //    }
                            //}

                            listMst_ProductImages.Add(objMst_Product_Image);
                        }
                    }
                    #endregion

                    #region ["list files"]
                    if (objRQ_Mst_ProductUI.Lst_Mst_ProductFiles != null && objRQ_Mst_ProductUI.Lst_Mst_ProductFiles.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductFiles)
                        {
                            var objMst_Product_File = new Mst_ProductFiles()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                                Idx = item.Idx,
                                ProductCode = objMst_Product_Input.ProductCode,
                                ProductFileName = item.ProductFileName,
                                ProductFileDesc = item.ProductFileDesc,
                                FlagIsFilePath = item.FlagIsFilePath,
                            };
                            if (item.FlagIsFilePath.Equals("1"))
                            {
                                if (!CUtils.IsNullOrEmpty(item.ProductFilePath))
                                {
                                    if (item.ProductFilePath.ToString().Contains("UploadedFiles"))
                                    {
                                        objMst_Product_File.ProductFilePath = rex.Split(item.ProductFilePath.ToString())[1];
                                    }
                                    else
                                    {
                                        objMst_Product_File.ProductFilePath = item.ProductFilePath;
                                    }
                                }
                            }
                            listMst_ProductFiles.Add(objMst_Product_File);
                        }
                    }
                    #endregion

                    #region ["List BOM"]
                    if (objRQ_Mst_ProductUI.Lst_Prd_BOM != null && objRQ_Mst_ProductUI.Lst_Prd_BOM.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_ProductUI.Lst_Prd_BOM)
                        {
                            var objMst_Product_Bom = new Prd_BOM()
                            {
                                OrgID = UserState.Mst_NNT.OrgID,
                                ProductCode = item.ProductCode,
                                OrgIDParent = UserState.Mst_NNT.OrgID,
                                ProductCodeParent = objMst_Product_Input.ProductCodeRoot,
                                Qty = item.Qty,
                                mp_UPSell = item.mp_UPBuy,
                                mp_UPBuy = item.mp_UPSell,
                                NetworkID = UserState.Mst_NNT.NetworkID,
                            };
                            listMst_ProductBOM.Add(objMst_Product_Bom);
                        }
                    }
                    #endregion

                    objRQ_Mst_Product.Tid = GetNextTId();
                    objRQ_Mst_Product.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_Product.GwPassword = OS_ProductCentrer_GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                    objRQ_Mst_Product.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    objRQ_Mst_Product.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                    objRQ_Mst_Product.FuncType = null;
                    objRQ_Mst_Product.Ft_RecordStart = null;
                    objRQ_Mst_Product.Ft_RecordCount = null;
                    objRQ_Mst_Product.Ft_WhereClause = null;
                    objRQ_Mst_Product.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Product.Lst_Mst_Product = new List<Mst_Product>();
                    objRQ_Mst_Product.Lst_Mst_Product.Add(objMst_Product_Edit);
                    objRQ_Mst_Product.Lst_Prd_Attribute = new List<Prd_Attribute>();
                    objRQ_Mst_Product.Lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
                    objRQ_Mst_Product.Lst_Mst_ProductImages = new List<Mst_ProductImages>();
                    objRQ_Mst_Product.Lst_Prd_BOM = new List<Prd_BOM>();

                    if (listAttribute != null && listAttribute.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Prd_Attribute.AddRange(listAttribute);
                    }
                    if (listMst_ProductImages != null && listMst_ProductImages.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Mst_ProductImages.AddRange(listMst_ProductImages);
                    }
                    if (listMst_ProductFiles != null && listMst_ProductFiles.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Mst_ProductFiles.AddRange(listMst_ProductFiles);
                    }
                    if (listMst_ProductBOM != null && listMst_ProductBOM.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Prd_BOM.AddRange(listMst_ProductBOM);
                    }

                    objRQ_Mst_Product.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_Product.GwPassword = OS_ProductCentrer_GwPassword;
                    Mst_ProductService.Instance.WA_Mst_Product_Update(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                    // Lưu tại network
                    objRQ_Mst_Product.GwUserCode = GwUserCode;
                    objRQ_Mst_Product.GwPassword = GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.FlagIsDelete = "0";
                    Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Hàng hóa đã được sửa thành công!");
                    flagRedirect = CUtils.StrValue(objRQ_Mst_ProductUI.FlagRedirect);
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
                else if (objMst_Product_Input.ProductLevelSys.Equals(Client_ProductLevelSys.RootPrd) || objMst_Product_Input.ProductLevelSys.Equals(Client_ProductLevelSys.BasePrd))
                {
                    var proRoot = CUtils.StrValueOrNull(objMst_Product_Input.ProductCodeRoot);
                    var proBase = CUtils.StrValueOrNull(objMst_Product_Input.ProductCodeBase);
                    // là sản phẩm root hoặc base
                    // tìm kiếm sản phẩm root
                    var listProduct_Base = new List<Mst_Product>();
                    var listProduct_Search = new List<Mst_Product>();
                    var _listProduct_Search = new List<Mst_Product>();

                    objMst_Product_Search.ProductCodeRoot = CUtils.StrValueOrNull(objMst_Product_Input.ProductCodeRoot);
                    //objMst_Product_Search.ProductCodeBase = CUtils.StrValueOrNull(objMst_Product_Input.ProductCodeBase);
                    //objMst_Product_Search.ProductLevelSys = CUtils.StrValueOrNull(objMst_Product_Input.ProductLevelSys);
                    strWhereClauseMst_Product = strWhereClause_Mst_Product_Update(objMst_Product_Search);
                    objRQ_Mst_Product.Ft_WhereClause = strWhereClauseMst_Product;

                    //Tìm ra các sản phẩm có Root = ProductCodeRoot
                    //Danh sách sản phẩm ban đầu
                    var obj_RT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs));
                    if (obj_RT_Mst_Product.Lst_Mst_Product != null && obj_RT_Mst_Product.Lst_Mst_Product.Count > 0)
                    {
                        listProduct_Search.AddRange(obj_RT_Mst_Product.Lst_Mst_Product);
                        listMst_Product = listProduct_Search;
                        //listProduct_Base = obj_RT_Mst_Product.Lst_Mst_Product.Where(it =>
                        //    !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCodeRoot) &&
                        //    it.ProductCodeBase.Equals(proBase) && it.ProductCodeRoot.Equals(proRoot)).ToList();

                        var listPrd_AttributeCur = new List<Prd_Attribute>();

                        #region ["Update product"]

                        var Tbl_Mst_Product = TableName.Mst_Product + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductCodeUser);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductBarCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductNameEN);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.VATRateCode);
                        objRQ_Mst_Product.Ft_Cols_Upd = strFt_Cols_Upd;

                        foreach (var item in listMst_Product)
                        {
                            if (item.ProductCode.Equals(CUtils.StrValue(objMst_Product_Input.ProductCode)))
                            {
                                item.ProductCodeUser = objMst_Product_Input.ProductCodeUser;
                                item.ProductName = objMst_Product_Input.ProductName;
                                item.ProductNameEN = objMst_Product_Input.ProductNameEN;
                                item.ProductBarCode = objMst_Product_Input.ProductBarCode;
                                item.FlagBuy = objMst_Product_Input.FlagBuy;
                                item.FlagLot = objMst_Product_Input.FlagLot;
                                item.FlagSell = objMst_Product_Input.FlagSell;
                                item.FlagSerial = objMst_Product_Input.FlagSerial;
                                //item.FlagActive = objMst_Product_Input.FlagActive; // NC 20200807
                                item.ProductDrawing = objMst_Product_Input.ProductDrawing;
                                item.ProductExpiry = objMst_Product_Input.ProductExpiry;
                                item.ProductIntro = objMst_Product_Input.ProductIntro;
                                item.ProductMnfUrl = objMst_Product_Input.ProductMnfUrl;
                                item.ProductOrigin = objMst_Product_Input.ProductOrigin;
                                item.ProductQuyCach = objMst_Product_Input.ProductQuyCach;
                                item.ProductStd = objMst_Product_Input.ProductStd;
                                item.ProductUserGuide = objMst_Product_Input.ProductUserGuide;
                                item.QtyEffSt = objMst_Product_Input.QtyEffSt;
                                item.QtyMaxSt = objMst_Product_Input.QtyMaxSt;
                                item.QtyMinSt = objMst_Product_Input.QtyMinSt;
                                item.Remark = objMst_Product_Input.Remark;
                                item.UPBuy = objMst_Product_Input.UPBuy;
                                item.UPSell = objMst_Product_Input.UPSell;
                                item.BrandCode = objMst_Product_Input.BrandCode;
                                item.ProductGrpCode = objMst_Product_Input.ProductGrpCode;
                                item.VATRateCode = objMst_Product_Input.VATRateCode;
                                item.ListOfPrdDynamicFieldValue = objMst_Product_Input.ListOfPrdDynamicFieldValue;

                                item.CustomField1 = objMst_Product_Input.CustomField1;
                                item.CustomField2 = objMst_Product_Input.CustomField2;
                                item.CustomField3 = objMst_Product_Input.CustomField3;
                                item.CustomField4 = objMst_Product_Input.CustomField4;
                                item.CustomField5 = objMst_Product_Input.CustomField5;
                                item.FlagFG = objMst_Product_Input.FlagFG;
                            }
                        }
                        #endregion

                        #region ["Delete attribute"]

                        if (objRQ_Mst_ProductUI.Lst_Prd_Attribute_Delete != null &&
                            objRQ_Mst_ProductUI.Lst_Prd_Attribute_Delete.Count > 0)
                        {
                            var listPrd_Attribute_Delete = objRQ_Mst_ProductUI.Lst_Prd_Attribute_Delete;
                            foreach (var item in listPrd_Attribute_Delete)
                            {
                                foreach (var itPrd in obj_RT_Mst_Product.Lst_Mst_Product)
                                {
                                    obj_RT_Mst_Product.Lst_Prd_Attribute.RemoveAll(it => it.ProductCode.Equals(itPrd.ProductCode) && it.AttributeCode.Equals(item.AttributeCode));
                                }

                            }
                        }

                        #endregion

                        #region ["Update Attribute"]
                        if (objRQ_Mst_ProductUI.Lst_Prd_Attribute != null && objRQ_Mst_ProductUI.Lst_Prd_Attribute.Count > 0)
                        {
                            if (objMst_Product_Input.ProductLevelSys.Equals(Client_ProductLevelSys.BasePrd))
                            {
                                var listPrd_Attribute_Cur = objRQ_Mst_ProductUI.Lst_Prd_Attribute;

                                var listPrd_Base_Level2 = obj_RT_Mst_Product.Lst_Mst_Product.Where(it =>
                            !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCodeRoot) &&
                            it.ProductCodeBase.Equals(objMst_Product_Input.ProductCode) && it.ProductCodeRoot.Equals(proRoot)).ToList();

                                var listPrd_Attribute_Update = new List<Prd_Attribute>();

                                foreach (var item in listPrd_Attribute_Cur)
                                {
                                    foreach (var it in listPrd_Base_Level2)
                                    {
                                        obj_RT_Mst_Product.Lst_Prd_Attribute.RemoveAll(itPrd => itPrd.ProductCode.Equals(it.ProductCode) && itPrd.AttributeCode.Equals(item.AttributeCode));
                                    }
                                }
                                foreach (var item in listPrd_Attribute_Cur)
                                {
                                    foreach (var it in listPrd_Base_Level2)
                                    {
                                        var objPrd_Attribute = new Prd_Attribute()
                                        {
                                            OrgID = UserState.Mst_NNT.OrgID,
                                            ProductCode = it.ProductCode,
                                            AttributeCode = item.AttributeCode,
                                            AttributeValue = item.AttributeValue,
                                        };
                                        obj_RT_Mst_Product.Lst_Prd_Attribute.Add(objPrd_Attribute);
                                    }
                                }
                            }
                            else if (objMst_Product_Input.ProductLevelSys.Equals(Client_ProductLevelSys.RootPrd))
                            {
                                var listPrd_Attribute_Cur = objRQ_Mst_ProductUI.Lst_Prd_Attribute;

                                var listPrd_Base = obj_RT_Mst_Product.Lst_Mst_Product.Where(it =>
                            !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCodeRoot) &&
                            it.ProductCodeBase.Equals(objMst_Product_Input.ProductCode) && it.ProductCodeRoot.Equals(objMst_Product_Input.ProductCodeRoot)).ToList();

                                var listPrd_Attribute_Update = new List<Prd_Attribute>();

                                foreach (var item in listPrd_Attribute_Cur)
                                {
                                    foreach (var it in listPrd_Base)
                                    {
                                        obj_RT_Mst_Product.Lst_Prd_Attribute.RemoveAll(itPrd => itPrd.ProductCode.Equals(it.ProductCode) && itPrd.AttributeCode.Equals(item.AttributeCode));
                                    }
                                }
                                foreach (var item in listPrd_Attribute_Cur)
                                {
                                    foreach (var it in listPrd_Base)
                                    {
                                        var objPrd_Attribute = new Prd_Attribute()
                                        {
                                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                            ProductCode = it.ProductCode,
                                            AttributeCode = item.AttributeCode,
                                            AttributeValue = item.AttributeValue,
                                        };
                                        obj_RT_Mst_Product.Lst_Prd_Attribute.Add(objPrd_Attribute);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region ["Add New Attribute"]

                        if (objRQ_Mst_ProductUI.Lst_Prd_Attribute_New != null &&
                            objRQ_Mst_ProductUI.Lst_Prd_Attribute_New.Count > 0)
                        {
                            // thêm vào từng sản phẩm thuộc tính mới
                            foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                            {
                                foreach (var it in objRQ_Mst_ProductUI.Lst_Prd_Attribute_New)
                                {
                                    var objPrd_AttributeCur = new Prd_Attribute();
                                    objPrd_AttributeCur.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                    objPrd_AttributeCur.ProductCode = item.ProductCode;
                                    objPrd_AttributeCur.AttributeCode = CUtils.StrValueOrNull(it.AttributeCode);
                                    objPrd_AttributeCur.AttributeValue = CUtils.StrValueOrNull(it.AttributeValue);
                                    item.ProductName = item.ProductName + "-" + it.AttributeValue;
                                    listPrd_Attribute.Add(objPrd_AttributeCur);
                                }

                            }
                        }

                        #endregion

                        #region ["Delete Unit"]

                        if (objRQ_Mst_ProductUI.Lst_Unit_Delete != null &&
                                objRQ_Mst_ProductUI.Lst_Unit_Delete.Count > 0)
                        {

                            foreach (var it in objRQ_Mst_ProductUI.Lst_Unit_Delete)
                            {
                                listProduct_Search.RemoveAll(x => x.ProductCode.Equals(it.ProductCode) && x.UnitCode.ToString().Trim().Equals(it.UnitCode.ToString().Trim()));
                            }
                            listMst_Product = listProduct_Search;
                            if (_listProduct_Search != null && _listProduct_Search.Count > 0)
                            {
                                listMst_Product = _listProduct_Search;
                            }
                        }

                        #endregion

                        #region ["Add Unit"]
                        var listUnitOld = new List<Mst_Unit>();
                        foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                        {
                            var objMst_Unit = new Mst_Unit()
                            {
                                UnitCode = item.UnitCode,
                            };
                            listUnitOld.Add(objMst_Unit);
                        }
                        if (objRQ_Mst_ProductUI.Lst_Unit != null &&
                            objRQ_Mst_ProductUI.Lst_Unit.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductUI_Create)
                            {
                                foreach (var itUnit in listUnitOld)
                                {
                                    if (item.UnitCode.ToString().Trim().Equals(itUnit.UnitCode.ToString().Trim()))
                                    {
                                        resultEntry.Success = true;
                                        resultEntry.AddMessage("Mã đơn vị '" + item.UnitCode + "' không được trùng!");
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            var listPrd_Base_Update_Unit = new List<Mst_Product>();
                            var listPrd_Base_Level2 = obj_RT_Mst_Product.Lst_Mst_Product.Where(it =>
                        !CUtils.IsNullOrEmpty(it.ProductCodeBase) && !CUtils.IsNullOrEmpty(it.ProductCodeRoot) &&
                        it.ProductCodeBase.Equals(objMst_Product_Input.ProductCode) && it.ProductCodeRoot.Equals(proRoot)).ToList();

                            foreach (var item in objRQ_Mst_ProductUI.Lst_Unit)
                            {
                                foreach (var itPrd in listPrd_Base_Level2)
                                {
                                    if (item.ProductCode.Equals(itPrd.ProductCode))
                                    {
                                        itPrd.UnitCode = item.UnitCode;
                                        itPrd.UPBuy = item.UPBuy;
                                        itPrd.UPSell = item.UPSell;
                                        itPrd.ValConvert = item.ValConvert;
                                        itPrd.FlagBuy = item.FlagBuy;
                                        itPrd.FlagSell = item.FlagSell;
                                        itPrd.FlagActive = "1"; // Mặc định cờ kinh doanh hoạt động

                                        listPrd_Base_Update_Unit.Add(itPrd);
                                    }
                                }
                            }
                            foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                            {
                                foreach (var itProduct in listPrd_Base_Update_Unit)
                                {
                                    if (itProduct.ProductCode.Equals(item.ProductCode))
                                    {
                                        item.UnitCode = itProduct.UnitCode;
                                        item.UPBuy = itProduct.UPBuy;
                                        item.UPSell = itProduct.UPSell;
                                        item.ValConvert = itProduct.ValConvert;
                                        item.FlagBuy = itProduct.FlagBuy;
                                        item.FlagSell = itProduct.FlagSell;                                        
                                    }
                                }
                            }

                            if (listMst_ProductUI != null && listMst_ProductUI.Count > 0)
                            {
                                // có sản phẩm mới

                                var objRQ_Seq_ObjCode = new RQ_Seq_ObjCode()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_ProductCentrer_GwUserCode,
                                    GwPassword = OS_ProductCentrer_GwPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    FuncType = null,
                                    Ft_Cols_Upd = null,

                                    ObjCodeAmount = listMst_ProductUI.Count,
                                };

                                var listProductCode = WA_Seq_GenObjCode_Get(objRQ_Seq_ObjCode);
                                for (var i = 0; i < listMst_ProductUI.Count; i++)
                                {
                                    var objMst_Product = new Mst_Product();
                                    objMst_Product.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                    objMst_Product.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                    objMst_Product.ProductCode = CUtils.StrValue(listProductCode[i]);
                                    objMst_Product.ProductLevelSys = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductLevelSys);
                                    objMst_Product.ProductCodeUser = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductCodeUser + "-" + DateTime.Now.ToString("yyMMddHHmmsss"));
                                    objMst_Product.BrandCode = CUtils.StrValueOrNull(listMst_ProductUI[i].BrandCode);
                                    objMst_Product.ProductType = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductType);
                                    objMst_Product.ProductGrpCode = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductGrpCode);
                                    objMst_Product.ProductName = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductName);
                                    objMst_Product.ProductNameEN = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductNameEN);
                                    objMst_Product.ProductBarCode = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductBarCode);
                                    objMst_Product.ProductCodeNetwork = null;
                                    objMst_Product.ProductCodeBase = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductCodeBase);
                                    objMst_Product.ProductCodeRoot = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductCodeRoot);
                                    objMst_Product.ProductImagePathList = null;
                                    objMst_Product.ProductFilePathList = null;
                                    objMst_Product.UnitCode = CUtils.StrValueOrNull(listMst_ProductUI[i].UnitCode);
                                    objMst_Product.ValConvert = CUtils.StrValueOrNull(listMst_ProductUI[i].ValConvert);
                                    objMst_Product.FlagSell = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagSell); // cờ bán
                                    objMst_Product.FlagBuy = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagBuy); // cờ mua
                                    objMst_Product.UPBuy = CUtils.StrValueOrNull(listMst_ProductUI[i].UPBuy);
                                    objMst_Product.FlagActive = "1";// NC 20200807
                                    objMst_Product.UPSell = CUtils.StrValueOrNull(listMst_ProductUI[i].UPSell);
                                    objMst_Product.QtyMaxSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyMaxSt);
                                    objMst_Product.QtyMinSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyMinSt);
                                    objMst_Product.QtyEffSt = CUtils.StrValueOrNull(listMst_ProductUI[i].QtyEffSt);
                                    objMst_Product.ListOfPrdDynamicFieldValue = null; // danh sách trường động
                                    objMst_Product.Remark = CUtils.StrValueOrNull(listMst_ProductUI[i].Remark);
                                    // Trang tập hợp dữ liệu gán ở đây
                                    // dữ liệu bảng hàng hóa gán vào listBOM
                                    objMst_Product.FlagSerial = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagSerial);
                                    objMst_Product.FlagLot = CUtils.StrValueOrNull(listMst_ProductUI[i].FlagLot);
                                    objMst_Product.ProductStd = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductStd);
                                    objMst_Product.ProductExpiry = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductExpiry);
                                    objMst_Product.ProductQuyCach = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductQuyCach);
                                    objMst_Product.ProductMnfUrl = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductMnfUrl);
                                    objMst_Product.ProductIntro = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductIntro);
                                    objMst_Product.ProductUserGuide = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductUserGuide);
                                    objMst_Product.ProductDrawing = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductDrawing);
                                    objMst_Product.ProductOrigin = CUtils.StrValueOrNull(listMst_ProductUI[i].ProductOrigin);
                                    listMst_Product.Add(objMst_Product);
                                    if (listMst_ProductUI[i].Lst_Prd_Attribute != null && listMst_ProductUI[i].Lst_Prd_Attribute.Count > 0)
                                    {
                                        foreach (var item in listMst_ProductUI[i].Lst_Prd_Attribute)
                                        {
                                            var objPrd_Attribute = new Prd_Attribute();
                                            objPrd_Attribute.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                            objPrd_Attribute.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                            objPrd_Attribute.ProductCode = objMst_Product.ProductCode;
                                            objPrd_Attribute.AttributeCode = item.AttributeCode;
                                            objPrd_Attribute.AttributeValue = item.AttributeValue;

                                            listPrd_Attribute.Add(objPrd_Attribute);
                                        }
                                    }

                                }
                            }
                        }

                        #endregion

                        #region ["List images"]

                        if (objRQ_Mst_ProductUI.Lst_Mst_ProductImages != null && objRQ_Mst_ProductUI.Lst_Mst_ProductImages.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductImages)
                            {
                                var objMst_Product_Image = new Mst_ProductImages()
                                {
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    Idx = item.Idx,
                                    ProductCode = objMst_Product_Input.ProductCode,
                                    ProductImageName = item.ProductImageName,
                                    ProductImageSpec = item.ProductImageSpec,
                                    FlagIsImagePath = item.FlagIsImagePath,
                                };
                                if (item.FlagIsImagePath.Equals("1"))
                                {
                                    if (!CUtils.IsNullOrEmpty(item.ProductImagePath))
                                    {
                                        if (item.ProductImagePath.ToString().Contains("UploadedFiles"))
                                        {
                                            //objMst_Product_Image.ProductImagePath = rex.Split(item.ProductImagePath.ToString())[1];
                                            objMst_Product_Image.ProductImagePath = item.ProductImagePath;
                                        }
                                        else
                                        {
                                            objMst_Product_Image.ProductImageSpec = item.ProductImageSpec;
                                        }
                                    }
                                }

                                listMst_ProductImages.Add(objMst_Product_Image);
                            }
                        }
                        #endregion

                        #region ["list files"]
                        if (objRQ_Mst_ProductUI.Lst_Mst_ProductFiles != null && objRQ_Mst_ProductUI.Lst_Mst_ProductFiles.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_ProductUI.Lst_Mst_ProductFiles)
                            {
                                var objMst_Product_File = new Mst_ProductFiles()
                                {
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    Idx = item.Idx,
                                    ProductCode = objMst_Product_Input.ProductCode,
                                    ProductFileName = item.ProductFileName,
                                    ProductFileDesc = item.ProductFileDesc,
                                    FlagIsFilePath = item.FlagIsFilePath,
                                };
                                if (item.FlagIsFilePath.Equals("1"))
                                {
                                    if (!CUtils.IsNullOrEmpty(item.ProductFilePath))
                                    {
                                        if (item.ProductFilePath.ToString().Contains("UploadedFiles"))
                                        {
                                            objMst_Product_File.ProductFilePath = rex.Split(item.ProductFilePath.ToString())[1];
                                        }
                                        else
                                        {
                                            objMst_Product_File.ProductFilePath = item.ProductFilePath;
                                        }
                                    }
                                }
                                listMst_ProductFiles.Add(objMst_Product_File);
                            }
                        }
                        #endregion

                        #region ["List BOM"]
                        if (objRQ_Mst_ProductUI.Lst_Prd_BOM != null && objRQ_Mst_ProductUI.Lst_Prd_BOM.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_ProductUI.Lst_Prd_BOM)
                            {
                                var objMst_Product_Bom = new Prd_BOM()
                                {
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    ProductCode = item.ProductCode,
                                    OrgIDParent = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    ProductCodeParent = objMst_Product_Input.ProductCodeRoot,
                                    Qty = item.Qty,
                                    mp_UPSell = item.mp_UPBuy,
                                    mp_UPBuy = item.mp_UPSell,
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                };
                                listMst_ProductBOM.Add(objMst_Product_Bom);
                            }
                        }
                        #endregion

                    }
                    listPrd_Attribute.AddRange(obj_RT_Mst_Product.Lst_Prd_Attribute);


                    objRQ_Mst_Product.Lst_Mst_Product = new List<Mst_Product>();
                    if (listMst_Product != null && listMst_Product.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Mst_Product.AddRange(listMst_Product);
                    }
                    objRQ_Mst_Product.Lst_Prd_Attribute = new List<Prd_Attribute>();
                    if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Prd_Attribute.AddRange(listPrd_Attribute);
                    }
                    objRQ_Mst_Product.Lst_Mst_ProductImages = new List<Mst_ProductImages>();
                    if (listMst_ProductImages != null && listMst_ProductImages.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Mst_ProductImages.AddRange(listMst_ProductImages);
                    }
                    objRQ_Mst_Product.Lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
                    if (listMst_ProductFiles != null && listMst_ProductFiles.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Mst_ProductFiles.AddRange(listMst_ProductFiles);
                    }
                    objRQ_Mst_Product.Lst_Prd_BOM = new List<Prd_BOM>();
                    if (listMst_ProductBOM != null && listMst_ProductBOM.Count > 0)
                    {
                        objRQ_Mst_Product.Lst_Prd_BOM.AddRange(listMst_ProductBOM);
                    }

                    objRQ_Mst_Product.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_Product.GwPassword = OS_ProductCentrer_GwPassword;
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                    Mst_ProductService.Instance.WA_Mst_Product_Update(objRQ_Mst_Product, UserState.AddressAPIs_Product_Customer_Centrer);
                    // Lưu tại network
                    objRQ_Mst_Product.GwUserCode = GwUserCode;
                    objRQ_Mst_Product.GwPassword = GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.FlagIsDelete = "0";
                    Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Hàng hóa đã được sửa thành công!");

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

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);

        }

        public Mst_Product MapProduct(Mst_Product productOld, Mst_Product productNew)
        {
            productOld.OrgID = productNew.OrgID;
            productOld.NetworkID = productNew.NetworkID;
            productOld.ProductCode = productNew.ProductCode;
            productOld.ProductLevelSys = productNew.ProductLevelSys;
            productOld.ProductCodeUser = productNew.ProductCodeUser;
            productOld.BrandCode = productNew.BrandCode;
            productOld.ProductType = productNew.ProductType;
            productOld.ProductGrpCode = productNew.ProductGrpCode;
            productOld.ProductName = productNew.ProductName;
            productOld.ProductNameEN = productNew.ProductNameEN;
            productOld.ProductBarCode = productNew.ProductBarCode;
            productOld.ProductCodeNetwork = productNew.ProductCodeNetwork;
            productOld.ProductCodeBase = productNew.ProductCodeBase;
            productOld.ProductCodeRoot = productNew.ProductCodeRoot;
            productOld.ProductImagePathList = productNew.ProductImagePathList;
            productOld.ProductFilePathList = productNew.ProductFilePathList;
            productOld.UnitCode = productNew.UnitCode;
            productOld.ValConvert = productNew.ValConvert;
            productOld.FlagSell = productNew.FlagSell;
            productOld.FlagBuy = productNew.FlagBuy;
            productOld.UPBuy = productNew.UPBuy;
            productOld.UPSell = productNew.UPSell;
            productOld.QtyMaxSt = productNew.QtyMaxSt;
            productOld.QtyMinSt = productNew.QtyMinSt;
            productOld.QtyEffSt = productNew.QtyEffSt;
            productOld.ListOfPrdDynamicFieldValue = productNew.ListOfPrdDynamicFieldValue;
            productOld.Remark = productNew.Remark;
            productOld.FlagActive = productNew.FlagActive;
            productOld.FlagSerial = productNew.FlagSerial;
            productOld.FlagLot = productNew.FlagLot;
            productOld.ProductStd = productNew.ProductStd;
            productOld.ProductExpiry = productNew.ProductExpiry;
            productOld.ProductQuyCach = productNew.ProductQuyCach;
            productOld.ProductMnfUrl = productNew.ProductMnfUrl;
            productOld.ProductIntro = productNew.ProductIntro;
            productOld.ProductUserGuide = productNew.ProductUserGuide;
            productOld.ProductDrawing = productNew.ProductDrawing;
            productOld.ProductOrigin = productNew.ProductOrigin;
            return productOld;
        }

        public Mst_Product MapProduct_Product_Import(Mst_Product productOld, Mst_Product_Import productNew)
        {
            productOld.OrgID = productNew.OrgID;
            productOld.NetworkID = productNew.NetworkID;
            productOld.ProductCode = productNew.ProductCode;
            productOld.ProductLevelSys = productNew.ProductLevelSys;
            productOld.ProductCodeUser = productNew.ProductCodeUser;
            productOld.BrandCode = productNew.BrandCode;
            productOld.ProductType = productNew.ProductType;
            productOld.ProductGrpCode = productNew.ProductGrpCode;
            productOld.ProductName = productNew.ProductName;
            productOld.ProductNameEN = productNew.ProductNameEN;
            productOld.ProductBarCode = productNew.ProductBarCode;
            productOld.ProductCodeNetwork = productNew.ProductCodeNetwork;
            productOld.ProductCodeBase = productNew.ProductCodeBase;
            productOld.ProductCodeRoot = productNew.ProductCodeRoot;
            productOld.ProductImagePathList = productNew.ProductImagePathList;
            productOld.ProductFilePathList = productNew.ProductFilePathList;
            productOld.UnitCode = productNew.UnitCode;
            productOld.ValConvert = productNew.ValConvert;
            productOld.FlagSell = productNew.FlagSell;
            productOld.FlagBuy = productNew.FlagBuy;
            productOld.UPBuy = productNew.UPBuy;
            productOld.UPSell = productNew.UPSell;
            productOld.QtyMaxSt = productNew.QtyMaxSt;
            productOld.QtyMinSt = productNew.QtyMinSt;
            productOld.QtyEffSt = productNew.QtyEffSt;
            productOld.ListOfPrdDynamicFieldValue = productNew.ListOfPrdDynamicFieldValue;
            productOld.Remark = productNew.Remark;
            productOld.FlagActive = productNew.FlagActive;
            productOld.FlagSerial = productNew.FlagSerial;
            productOld.FlagLot = productNew.FlagLot;
            productOld.ProductStd = productNew.ProductStd;
            productOld.ProductExpiry = productNew.ProductExpiry;
            productOld.ProductQuyCach = productNew.ProductQuyCach;
            productOld.ProductMnfUrl = productNew.ProductMnfUrl;
            productOld.ProductIntro = productNew.ProductIntro;
            productOld.ProductUserGuide = productNew.ProductUserGuide;
            productOld.ProductDrawing = productNew.ProductDrawing;
            productOld.ProductOrigin = productNew.ProductOrigin;
            return productOld;
        }

        public Mst_Product_Import MapProduct_Import(Mst_Product_Import productOld, Mst_Product productNew, List<Prd_AttributeUI> listPrd_AttributeUI, List<Mst_ProductImages> listMst_ProductImages, List<Mst_ProductFiles> lisMst_ProductFiles, List<Prd_BOM> listPrd_BOM)
        {
            productOld = new Mst_Product_Import()
            {
                Lst_Prd_AttributeUI = new List<Prd_AttributeUI>(),
                Lst_Mst_ProductImages = new List<Mst_ProductImages>(),
                Lst_Mst_ProductFiles = new List<Mst_ProductFiles>(),
                Lst_Prd_BOM = new List<Prd_BOM>()
            };
            if (listPrd_AttributeUI != null && listPrd_AttributeUI.Count > 0)
            {
                productOld.Lst_Prd_AttributeUI.AddRange(listPrd_AttributeUI);
            }
            if (lisMst_ProductFiles != null && lisMst_ProductFiles.Count > 0)
            {
                productOld.Lst_Mst_ProductFiles.AddRange(lisMst_ProductFiles);
            }
            if (listMst_ProductImages != null && listMst_ProductImages.Count > 0)
            {
                productOld.Lst_Mst_ProductImages.AddRange(listMst_ProductImages);
            }
            if (listPrd_BOM != null && listPrd_BOM.Count > 0)
            {
                productOld.Lst_Prd_BOM.AddRange(listPrd_BOM);
            }

            productOld.OrgID = productNew.OrgID;
            productOld.NetworkID = productNew.NetworkID;
            productOld.ProductCode = productNew.ProductCode;
            productOld.ProductLevelSys = productNew.ProductLevelSys;
            productOld.ProductCodeUser = productNew.ProductCodeUser;
            productOld.BrandCode = productNew.BrandCode;
            productOld.ProductType = productNew.ProductType;
            productOld.ProductGrpCode = productNew.ProductGrpCode;
            productOld.ProductName = productNew.ProductName;
            productOld.ProductNameEN = productNew.ProductNameEN;
            productOld.ProductBarCode = productNew.ProductBarCode;
            productOld.ProductCodeNetwork = productNew.ProductCodeNetwork;
            productOld.ProductCodeBase = productNew.ProductCodeBase;
            productOld.ProductCodeRoot = productNew.ProductCodeRoot;
            productOld.ProductImagePathList = productNew.ProductImagePathList;
            productOld.ProductFilePathList = productNew.ProductFilePathList;
            productOld.UnitCode = productNew.UnitCode;
            productOld.ValConvert = productNew.ValConvert;
            productOld.FlagSell = productNew.FlagSell;
            productOld.FlagBuy = productNew.FlagBuy;
            productOld.UPBuy = productNew.UPBuy;
            productOld.UPSell = productNew.UPSell;
            productOld.QtyMaxSt = productNew.QtyMaxSt;
            productOld.QtyMinSt = productNew.QtyMinSt;
            productOld.QtyEffSt = productNew.QtyEffSt;
            productOld.ListOfPrdDynamicFieldValue = productNew.ListOfPrdDynamicFieldValue;
            productOld.Remark = productNew.Remark;
            productOld.FlagActive = productNew.FlagActive;
            productOld.FlagSerial = productNew.FlagSerial;
            productOld.FlagLot = productNew.FlagLot;
            productOld.ProductStd = productNew.ProductStd;
            productOld.ProductExpiry = productNew.ProductExpiry;
            productOld.ProductQuyCach = productNew.ProductQuyCach;
            productOld.ProductMnfUrl = productNew.ProductMnfUrl;
            productOld.ProductIntro = productNew.ProductIntro;
            productOld.ProductUserGuide = productNew.ProductUserGuide;
            productOld.ProductDrawing = productNew.ProductDrawing;
            productOld.ProductOrigin = productNew.ProductOrigin;
            return productOld;
        }
        #endregion

        #region["Xóa sản phẩm"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUnit(string prdCode, string prdBase, string prdRoot, string prdlvs)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
           
            if (!CUtils.IsNullOrEmpty(prdCode))
            {
                var objMst_Product = new Mst_Product()
                {
                    ProductCode = prdCode,
                };

                var strWhereClauseMst_Product = strWhereClause_Mst_Product_Update(objMst_Product);

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
                    Ft_WhereClause = strWhereClauseMst_Product,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Product = "*",
                    Lst_Mst_Product = null,
                };

                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs));
                if (objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                {
                    objRQ_Mst_Product.Lst_Mst_Product = new List<Mst_Product>();
                    objRT_Mst_Product.Lst_Mst_Product[0].ProductDelType = Client_ProductLevelSys.L2Prd;
                    objRQ_Mst_Product.Lst_Mst_Product.Add(objRT_Mst_Product.Lst_Mst_Product[0]);
                    objRQ_Mst_Product.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_Product.GwPassword = OS_ProductCentrer_GwPassword;
                    objRQ_Mst_Product.Tid = GetNextTId();
                    Mst_ProductService.Instance.WA_Mst_Product_Delete(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                    // Lưu tại network
                    objRQ_Mst_Product.GwUserCode = GwUserCode;
                    objRQ_Mst_Product.GwPassword = GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.FlagIsDelete = "1";
                    Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa đơn vị thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index", "Mst_Product");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã sản phẩm '" + prdCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã sản phẩm trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Import - Export Excel"]
        #region ["Export Template"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var list = new List<Mst_Product>();

            Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
            string url = "";
            string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Product).Name), ref url);
            ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Product"));

            return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
        }
        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file, string flagimport = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            
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
                var listMst_Product = new List<Mst_Product>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != GetImportDicColumsTemplate().Count)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        return Json(new { ErrorMessage = exitsData, Success = false });
                    }
                    else
                    {
                        #region["Loại hàng"]
                        var objRQ_Mst_ProductType = new RQ_Mst_ProductType()
                        {
                            // WARQBase
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
                            Ft_Cols_Upd = null,
                            // RQ_Mst_ProductType
                            Rt_Cols_Mst_ProductType = "*",
                            Mst_ProductType = null,
                        };
                        var list_Mst_ProductType_Async = List_Mst_ProductType_Async(objRQ_Mst_ProductType);
                        #endregion
                        #region["Nhóm sản phẩm"]
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
                            Ft_WhereClause = "",
                            Ft_Cols_Upd = null,
                            // RQ_Mst_ProductGroup
                            Rt_Cols_Mst_ProductGroup = "*",
                            Mst_ProductGroup = null,
                        };
                        var list_Mst_ProductGroup_Async = List_Mst_ProductGroup_Async(objRQ_Mst_ProductGroup);
                        #endregion
                        #region["Nhãn hiệu"]
                        var objRQ_Mst_Brand = new RQ_Mst_Brand()
                        {
                            // WARQBase
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
                            Ft_Cols_Upd = null,
                            // RQ_Mst_Brand
                            Rt_Cols_Mst_Brand = "*",
                            Mst_Brand = null,
                        };
                        var list_Mst_Brand_Async = List_Mst_Brand_Async(objRQ_Mst_Brand);
                        #endregion

                        #region ["add columns"]

                        if (!table.Columns.Contains(TblMst_Product.OrgID))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                               new DataColumn(TblMst_Product.OrgID, typeof (string))
                           });
                        }
                        if (!table.Columns.Contains(TblMst_Product.NetworkID))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                               new DataColumn(TblMst_Product.NetworkID, typeof (string))
                           });
                        }
                        if (!table.Columns.Contains(TblMst_Product.ProductLevelSys))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                new DataColumn(TblMst_Product.ProductLevelSys, typeof (string))
                            });
                        }
                        #endregion

                        #region["Check null và tập hợp dữ liệu"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        #region ["Variable"]
                        var prdCodeRoot = "";
                        var prdCodeBase = "";
                        var prdCodeUser = "";
                        var prdLevelSys = "";
                        var unitCode = "";
                        var unitCodeBase = "";
                        var valConvert = "";
                        var productCode = "";
                        var listMst_Product_ImportExcel = new List<Mst_Product_ImportExcel>();

                        #endregion["list data import"]
                        foreach (DataRow dr in table.Rows)
                        {

                            var objMst_Product = new Mst_Product();
                            var listPrd_AttributeUI = new List<Prd_AttributeUI>();
                            var listMst_ProductImages = new List<Mst_ProductImages>();
                            var listMst_ProductFiles = new List<Mst_ProductFiles>();
                            var listPrd_BOM = new List<Prd_BOM>();
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;
                            prdCodeRoot = "";
                            #region ["Variable"]
                            var productCodeCur = "";
                            var productCodeUserCur = "";
                            var productNameCur = "";
                            var productGrpCodeCur = "";
                            var productTypeCur = "";
                            var productCodeBaseCur = "";
                            var productCodeRootCur = "";
                            var productLevelSysCur = Client_ProductLevelSys.L2Prd;
                            var brandCodeCur = "";
                            var productBarCodeCur = "";
                            string productCodeNetworkCur = null;
                            var unitCodeCur = "";
                            var valConvertCur = "";
                            var flagSellCur = "";
                            var flagBuyCur = "";
                            var uPBuyCur = "";
                            var uPSellCur = "";
                            var qtyMaxStCur = "";
                            var qtyMinStCur = "";
                            var qtyEffStCur = "";
                            string listOfPrdDynamicFieldValueCur = null; // dữ liệu động
                            var productImagePathListCur = ""; // danh sách ảnh
                            var productFilePathListCur = ""; // danh sách file
                            var listAttributeCur = ""; // danh sách thuộc tính
                            var listBOMCur = ""; //
                            var remarkCur = "";
                            var flagActiveCur = "";
                            var flagSerialCur = "";
                            var flagLotCur = "";
                            var productStdCur = "";
                            var productExpiryCur = "";
                            var productQuyCachCur = "";
                            var productMnfUrlCur = "";
                            var productIntroCur = "";
                            var productUserGuideCur = "";
                            var productDrawingCur = "";
                            var productOriginCur = "";
                            #endregion

                            #region ["Check dữ liệu null"]

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductCodeUser]))
                            {
                                exitsData = "Mã hàng không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                productCodeUserCur = CUtils.StrValue(dr[TblMst_Product.ProductCodeUser]);
                            }

                            productCodeCur = productCodeUserCur;
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductName]))
                            {
                                exitsData = "Tên hàng không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                productNameCur = CUtils.StrValue(dr[TblMst_Product.ProductName]);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductGrpCode]))
                            {
                                exitsData = "Nhóm hàng không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                productGrpCodeCur = CUtils.StrValue(dr[TblMst_Product.ProductGrpCode]);
                                var objProductGroup = list_Mst_ProductGroup_Async.Result.Where(x => x.ProductGrpName.ToString().Trim().Equals(productGrpCodeCur)).FirstOrDefault();
                                if (objProductGroup != null)
                                {
                                    productGrpCodeCur = objProductGroup.ProductGrpCode.ToString();
                                }
                                else
                                {
                                    exitsData = "Nhóm hàng " + productGrpCodeCur + " không có trong hệ thống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Product.ProductType]))
                            {
                                productTypeCur = "PRODUCT";
                            }
                            else
                            {
                                productTypeCur = CUtils.StrValue(dr[TblMst_Product.ProductType]);
                                var objProductType = list_Mst_ProductType_Async.Result.Where(x => x.ProductTypeName.ToString().Trim().Equals(productTypeCur)).FirstOrDefault();
                                //var objProductType = list_Mst_ProductType_Async.Result.Where(x => x.ProductType.ToString().Trim().Equals(productTypeCur)).FirstOrDefault();
                                if (objProductType != null)
                                {
                                    productTypeCur = objProductType.ProductType.ToString();
                                }
                                else
                                {
                                    exitsData = "Loại hàng " + productTypeCur + " không có trong hệ thống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            #endregion

                            #region[""]
                            productCodeBaseCur = CUtils.StrValue(dr[TblMst_Product.ProductCodeBase]);
                            if (CUtils.IsNullOrEmpty(productCodeBaseCur))
                            {
                                productCodeBaseCur = productCodeCur; // Nếu user ko nhập,lưu =Mã hàng
                            }
                            if (productCodeCur.Equals(productCodeBaseCur))
                            {
                                productLevelSysCur = Client_ProductLevelSys.BasePrd;
                            }
                            productCodeRootCur = CUtils.StrValue(dr[TblMst_Product.ProductCodeRoot]);
                            if (CUtils.IsNullOrEmpty(productCodeRootCur))
                            {
                                productCodeRootCur = productCodeCur; //Nếu user ko nhập,lưu =Mã hàng
                            }
                            if (productCodeCur.Equals(productCodeBaseCur) && productCodeCur.Equals(productCodeRootCur))
                            {
                                productLevelSysCur = Client_ProductLevelSys.RootPrd;
                            }
                            #endregion

                            #region[""]
                            brandCodeCur = CUtils.StrValue(dr[TblMst_Product.BrandCode]);
                            if (!CUtils.IsNullOrEmpty(brandCodeCur))
                            {
                                var objBrand = list_Mst_Brand_Async.Result.Where(x => x.BrandName.ToString().Trim().Equals(brandCodeCur)).FirstOrDefault();
                                if (objBrand != null)
                                {
                                    brandCodeCur = objBrand.BrandCode.ToString();
                                }
                                else
                                {
                                    exitsData = "Nhãn hiệu " + brandCodeCur + " không có trong hệ thống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            productBarCodeCur = CUtils.StrValue(dr[TblMst_Product.ProductBarCode]);
                            unitCodeCur = CUtils.StrValue(dr[TblMst_Product.UnitCode]);
                            valConvertCur = CUtils.StrValueOrNull(dr[TblMst_Product.ValConvert]);
                            if (productLevelSysCur.Equals(Client_ProductLevelSys.RootPrd) ||
                                productLevelSysCur.Equals(Client_ProductLevelSys.BasePrd))
                            {
                                valConvertCur = "1";
                            }
                            else
                            {
                                if (!CUtils.IsNullOrEmpty(valConvertCur))
                                {
                                    if (!CUtils.IsNumeric(valConvertCur))
                                    {
                                        exitsData = "Giá trị quy đổi không đúng định dạng!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            flagSellCur = CUtils.StrValue(dr[TblMst_Product.FlagSell]);
                            if (CUtils.IsNullOrEmpty(flagSellCur))
                            {
                                flagSellCur = "1";
                            }
                            flagBuyCur = CUtils.StrValue(dr[TblMst_Product.FlagBuy]);
                            if (CUtils.IsNullOrEmpty(flagBuyCur))
                            {
                                flagBuyCur = "1";
                            }
                            uPBuyCur = CUtils.StrValueOrNull(dr[TblMst_Product.UPBuy]);
                            if (!CUtils.IsNullOrEmpty(uPBuyCur))
                            {
                                if (!CUtils.IsNumeric(uPBuyCur))
                                {
                                    exitsData = "Giá mua không đúng định dạng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            uPSellCur = CUtils.StrValueOrNull(dr[TblMst_Product.UPSell]);
                            if (!CUtils.IsNullOrEmpty(uPSellCur))
                            {
                                if (!CUtils.IsNumeric(uPSellCur))
                                {
                                    exitsData = "Giá bán không đúng định dạng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            qtyMaxStCur = CUtils.StrValueOrNull(dr[TblMst_Product.QtyMaxSt]);
                            if (!CUtils.IsNullOrEmpty(qtyMaxStCur))
                            {
                                if (!CUtils.IsNumeric(qtyMaxStCur))
                                {
                                    exitsData = "Tồn lớn nhất không đúng định dạng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            qtyEffStCur = CUtils.StrValueOrNull(dr[TblMst_Product.QtyEffSt]);
                            if (!CUtils.IsNullOrEmpty(qtyEffStCur))
                            {
                                if (!CUtils.IsNumeric(qtyEffStCur))
                                {
                                    exitsData = "Tồn tối ưu không đúng định dạng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            qtyMinStCur = CUtils.StrValueOrNull(dr[TblMst_Product.QtyMinSt]);
                            if (!CUtils.IsNullOrEmpty(qtyMinStCur))
                            {
                                if (!CUtils.IsNumeric(qtyMinStCur))
                                {
                                    exitsData = "Tồn nhỏ nhất không đúng định dạng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            listOfPrdDynamicFieldValueCur = CUtils.StrValue(dr[TblMst_Product.ListOfPrdDynamicFieldValue]);
                            if (!CUtils.IsNullOrEmpty(listOfPrdDynamicFieldValueCur))
                            {
                                // danh sách trường động
                                var arr = listOfPrdDynamicFieldValueCur.Split('|');
                                if (arr != null && arr.Length > 0)
                                {
                                    var listPrdDynamicFieldParamType = new List<PrdDynamicFieldParamType>();
                                    var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
                                    {
                                        // WARQBase
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
                                        Ft_RecordStart = Ft_RecordStartExportExcel,
                                        Ft_RecordCount = RowsWorksheets.ToString(),
                                        Ft_WhereClause = "",
                                        Ft_Cols_Upd = null,

                                        // RQ_Form_Payment
                                        Rt_Cols_Prd_DynamicField = "*",
                                    };
                                    var objRT_Prd_DynamicField = WA_Prd_DynamicField_Get(objRQ_Prd_DynamicField);
                                    if (objRT_Prd_DynamicField != null && objRT_Prd_DynamicField.Count > 0)
                                    {
                                        listPrdDynamicFieldParamType.AddRange(objRT_Prd_DynamicField[0].ParamTypes);
                                    }

                                    var listOfPrdDynamicFieldExcel = new List<PrdDynamicFieldParamType>();
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var arrSub = arr[i].Split(':');
                                        if (arrSub != null && arrSub.Length == 2)
                                        {
                                            listOfPrdDynamicFieldExcel.Add(new PrdDynamicFieldParamType()
                                            {
                                                Title = arrSub[0],
                                                DefaultValue = arrSub[1]
                                            });
                                        }
                                        else
                                        {
                                            exitsData = "Trường động mã hàng'" + productCodeUserCur + "' không hợp lệ" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }

                                    var listOfPrdDynamicFieldNotExist = new List<PrdDynamicFieldParamType>();
                                    if (listOfPrdDynamicFieldExcel.Count > 0)
                                    {
                                        foreach (var item in listOfPrdDynamicFieldExcel)
                                        {
                                            var key = listPrdDynamicFieldParamType.FirstOrDefault(param => param.Title.Equals(item.Title));
                                            if (key == null)
                                            {
                                                listOfPrdDynamicFieldNotExist.Add(new PrdDynamicFieldParamType()
                                                {
                                                    Title = item.Title,
                                                    DefaultValue = item.DefaultValue,
                                                    DataType = PrdDynamicFieldDataTypes.String,
                                                    Code = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.fff"),
                                                    ControlType = ControlTypes.Default
                                                });
                                            }
                                        }
                                    }

                                    // Them item trong list not exist

                                    #region["Thông tin bổ sung"]
                                    var objRQ_Prd_DynamicFieldEx = new RQ_Prd_DynamicField()
                                    {
                                        // WARQBase
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
                                        Ft_Cols_Upd = null,
                                        // RQ_Prd_DynamicField
                                        Rt_Cols_Prd_DynamicField = "*",
                                        Prd_DynamicField = null,
                                    };
                                    var list_Prd_DynamicField_Async = List_Prd_DynamicField_Async(objRQ_Prd_DynamicFieldEx);
                                    var listPrd_DynamicField = new List<Prd_DynamicField>();

                                    if (list_Prd_DynamicField_Async != null && list_Prd_DynamicField_Async.Result != null &&
                                    list_Prd_DynamicField_Async.Result.Count > 0)
                                    {
                                        listPrd_DynamicField.AddRange(list_Prd_DynamicField_Async.Result);
                                    }
                                    #endregion
                                    if (listPrd_DynamicField != null && listPrd_DynamicField.Count > 0)
                                    {
                                        if (listPrd_DynamicField[0].ParamTypes != null && listPrd_DynamicField[0].ParamTypes.Count > 0)
                                        {
                                            var paramTypes = new List<PrdDynamicFieldParamType>();
                                            paramTypes.AddRange(listPrd_DynamicField[0].ParamTypes);
                                            paramTypes.AddRange(listOfPrdDynamicFieldNotExist);

                                            #region["Xóa DynamicField cũ"]
                                            var prd_DynamicFieldDel = new Prd_DynamicField()
                                            {
                                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                                            };
                                            var objRQ_Prd_DynamicFieldDel = new RQ_Prd_DynamicField()
                                            {
                                                // WARQBase
                                                Tid = GetNextTId(),
                                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                                GwPassword = OS_ProductCentrer_GwPassword,
                                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                                // RQ_Prd_DynamicField
                                                Prd_DynamicField = prd_DynamicFieldDel,
                                            };
                                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Delete(objRQ_Prd_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer ));
                                            #endregion

                                            #region["Tạo DynamicField mới có add thêm objParamType"]
                                            var prd_DynamicFieldCreate = new Prd_DynamicField()
                                            {
                                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                                Detail = "",
                                                ParamTypes = paramTypes
                                            };
                                            var objRQ_Prd_DynamicFieldCreate = new RQ_Prd_DynamicField()
                                            {
                                                // WARQBase
                                                Tid = GetNextTId(),
                                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                                GwPassword = OS_ProductCentrer_GwPassword,
                                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                                // RQ_Prd_DynamicField
                                                Prd_DynamicField = prd_DynamicFieldCreate,
                                            };
                                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Create(objRQ_Prd_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                                            #endregion
                                        }
                                    }
                                    // => DynamicField To String 
                                    listOfPrdDynamicFieldValueCur = "";
                                    var listArr = new List<string>();
                                    foreach (var item in listOfPrdDynamicFieldExcel)
                                    {
                                        var itemDynamicField = "";
                                        itemDynamicField = string.Format("{0}:{1}", item.Code, item.DefaultValue);
                                        listArr.Add(itemDynamicField);
                                    }
                                    listOfPrdDynamicFieldValueCur = string.Join(",", listArr);
                                }
                            }

                            listAttributeCur = CUtils.StrValue(dr[TblMst_Product.ListAttribute]);
                            if (!CUtils.IsNullOrEmpty(listAttributeCur))
                            {
                                var arr = CUtils.StrSplit(listAttributeCur, new[] { "|" });
                                if (arr != null && arr.Length > 0)
                                {
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var arrSub = CUtils.StrSplit(arr[i], new[] { ":" });
                                        if (arrSub != null && arrSub.Length == 2)
                                        {
                                            if (!CUtils.IsNullOrEmpty(arrSub[0]) && !CUtils.IsNullOrEmpty(arrSub[1]))
                                            {
                                                var objAttributeUI = new Prd_AttributeUI()
                                                {
                                                    AttributeName = CUtils.StrValue(arrSub[0]),
                                                    AttributeValue = CUtils.StrValue(arrSub[1]),
                                                };
                                                listPrd_AttributeUI.Add(objAttributeUI);
                                            }
                                            else
                                            {
                                                exitsData = "Thuộc tính của mã hàng '" + productCodeUserCur + "' không hợp lệ" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        else
                                        {
                                            exitsData = "Thuộc tính của mã hàng '" + productCodeUserCur + "' không hợp lệ" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }

                            }
                            listBOMCur = CUtils.StrValue(dr[TblMst_Product.ListBOM]);
                            if (!CUtils.IsNullOrEmpty(listBOMCur))
                            {
                                var arr = CUtils.StrSplit(listBOMCur, new[] { "|" });
                                if (arr != null && arr.Length > 0)
                                {
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var arrSub = CUtils.StrSplit(arr[i], new[] { ":" });
                                        if (arrSub != null && arrSub.Length == 2)
                                        {
                                            if (!CUtils.IsNullOrEmpty(arrSub[0]) && !CUtils.IsNullOrEmpty(arrSub[1]))
                                            {
                                                var objPrd_BOM = new Prd_BOM()
                                                {
                                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                                    mp_ProductName = CUtils.StrValue(arrSub[0]),
                                                    Qty = CUtils.StrValue(arrSub[1]),
                                                };
                                                listPrd_BOM.Add(objPrd_BOM);
                                            }
                                            else
                                            {
                                                exitsData = "Hàng thành phần của mã hàng '" + productCodeUserCur + "' không hợp lệ" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        else
                                        {
                                            exitsData = "Thuộc tính của mã hàng '" + productCodeUserCur + "' không hợp lệ" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }

                            }
                            remarkCur = CUtils.StrValue(dr[TblMst_Product.Remark]);
                            flagSerialCur = CUtils.StrValue(dr[TblMst_Product.FlagSerial]);
                            if (CUtils.IsNullOrEmpty(flagSerialCur))
                            {
                                flagSerialCur = "0";
                            }
                            else
                            {
                                if (!flagSerialCur.Equals("0") && !flagSerialCur.Equals("1"))
                                {
                                    exitsData = "QL Serial nhập '0' hoặc '1'!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            flagLotCur = CUtils.StrValue(dr[TblMst_Product.FlagLot]);
                            if (CUtils.IsNullOrEmpty(flagLotCur))
                            {
                                flagLotCur = "0";
                            }
                            else
                            {
                                if (!flagLotCur.Equals("0") && !flagLotCur.Equals("1"))
                                {
                                    exitsData = "QL LOT nhập '0' hoặc '1'!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else if (flagSerialCur.Equals("1") && flagLotCur.Equals("1"))
                                {
                                    exitsData = "QL LOT và QL Serial chỉ chọn 1 trong 2!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            productImagePathListCur = CUtils.StrValue(dr[TblMst_Product.ProductImagePathList]);
                            if (!CUtils.IsNullOrEmpty(productImagePathListCur))
                            {
                                var arr = productImagePathListCur.Split('|');
                                if (arr != null && arr.Length > 0)
                                {
                                    foreach (var item in arr)
                                    {
                                        // cắt lấy tên file
                                        var objMst_ProductImages = new Mst_ProductImages()
                                        {
                                            ProductImageName = item.Split('/').Last(),
                                            ProductImagePath = item,
                                            ProductCode = productCodeCur,
                                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                            Idx = idx,
                                            FlagIsImagePath = "1",
                                        };
                                        listMst_ProductImages.Add(objMst_ProductImages);
                                    }
                                }
                            }

                            productFilePathListCur = CUtils.StrValue(dr[TblMst_Product.ProductFilePathList]);
                            if (!CUtils.IsNullOrEmpty(productFilePathListCur))
                            {
                                var arr = productFilePathListCur.Split('|');
                                if (arr != null && arr.Length > 0)
                                {
                                    foreach (var item in arr)
                                    {
                                        // cắt lấy tên file
                                        var objMst_ProductFiles = new Mst_ProductFiles()
                                        {
                                            ProductFileName = item.Split('/').Last(),
                                            ProductFilePath = item,
                                            ProductCode = productCodeCur,
                                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                            Idx = idx,
                                            FlagIsFilePath = "1",
                                        };
                                        listMst_ProductFiles.Add(objMst_ProductFiles);
                                    }
                                }
                            }
                            productStdCur = CUtils.StrValue(dr[TblMst_Product.ProductStd]);
                            productExpiryCur = CUtils.StrValue(dr[TblMst_Product.ProductExpiry]);
                            productQuyCachCur = CUtils.StrValue(dr[TblMst_Product.ProductQuyCach]);
                            productMnfUrlCur = CUtils.StrValue(dr[TblMst_Product.ProductMnfUrl]);
                            productIntroCur = CUtils.StrValue(dr[TblMst_Product.ProductIntro]);
                            productUserGuideCur = CUtils.StrValue(dr[TblMst_Product.ProductUserGuide]);
                            productDrawingCur = CUtils.StrValue(dr[TblMst_Product.ProductDrawing]);
                            productOriginCur = CUtils.StrValue(dr[TblMst_Product.ProductOrigin]);

                            #region["Gán dữ liệu vào đối tượng"]

                            objMst_Product.ProductCodeRoot = productCodeRootCur;
                            objMst_Product.ProductCodeBase = productCodeBaseCur;
                            objMst_Product.ProductCode = productCodeCur;
                            objMst_Product.ProductCodeUser = productCodeUserCur;
                            objMst_Product.ProductName = productNameCur;
                            objMst_Product.ProductNameEN = productNameCur;
                            objMst_Product.ProductGrpCode = productGrpCodeCur;
                            objMst_Product.ProductType = productTypeCur;
                            objMst_Product.ProductLevelSys = productLevelSysCur;
                            objMst_Product.BrandCode = brandCodeCur;
                            objMst_Product.ProductBarCode = productBarCodeCur;
                            objMst_Product.ProductCodeNetwork = null;
                            objMst_Product.UnitCode = unitCodeCur;
                            objMst_Product.ValConvert = valConvertCur;
                            objMst_Product.FlagSell = flagSellCur;
                            objMst_Product.FlagBuy = flagBuyCur;
                            objMst_Product.UPBuy = uPBuyCur;
                            objMst_Product.UPSell = uPSellCur;
                            objMst_Product.QtyMaxSt = qtyMaxStCur;
                            objMst_Product.QtyEffSt = qtyEffStCur;
                            objMst_Product.QtyMinSt = qtyMinStCur;
                            objMst_Product.ListOfPrdDynamicFieldValue = listOfPrdDynamicFieldValueCur;
                            objMst_Product.ProductImagePathList = null;
                            objMst_Product.ProductFilePathList = null;
                            objMst_Product.Remark = remarkCur;
                            //objMst_Product.FlagActive = flagActiveCur;
                            objMst_Product.FlagSerial = flagSerialCur;
                            objMst_Product.FlagLot = flagLotCur;
                            objMst_Product.ProductStd = productStdCur;
                            objMst_Product.ProductExpiry = productExpiryCur;
                            objMst_Product.ProductQuyCach = productQuyCachCur;
                            objMst_Product.ProductMnfUrl = productMnfUrlCur;
                            objMst_Product.ProductIntro = productIntroCur;
                            objMst_Product.ProductUserGuide = productUserGuideCur;
                            objMst_Product.ProductDrawing = productDrawingCur;
                            objMst_Product.ProductOrigin = productOriginCur;

                            #endregion


                            if (productLevelSysCur.Equals(Client_ProductLevelSys.RootPrd))
                            {
                                var check = false;
                                foreach (var it in listMst_Product_ImportExcel)
                                {
                                    if (it.Mst_Product != null)
                                    {
                                        var _productCodeRoot = CUtils.StrValue(it.Mst_Product.ProductCodeRoot);
                                        if (_productCodeRoot.Equals(productCodeRootCur))
                                        {
                                            check = true;
                                            break;
                                        }
                                    }
                                }

                                if (!check)
                                {
                                    // hàng hóa root chưa tồn tại trong list
                                    var objMst_Product_ImportExcel = new Mst_Product_ImportExcel()
                                    {
                                        Mst_Product = objMst_Product,
                                        Lst_Mst_Product = new List<Mst_Product_Import>(),
                                    };
                                    objMst_Product_ImportExcel.Lst_Mst_Product.Add(
                                        MapProduct_Import(new Mst_Product_Import(), objMst_Product, listPrd_AttributeUI,
                                            listMst_ProductImages, listMst_ProductFiles, listPrd_BOM));
                                    listMst_Product_ImportExcel.Add(objMst_Product_ImportExcel);
                                }
                                else
                                {
                                    exitsData = "Mã hàng gốc '" + productCodeRootCur + "' bị lặp trong file import!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                            }
                            else if (productLevelSysCur.Equals(Client_ProductLevelSys.BasePrd))
                            {
                                var check = false;
                                foreach (var it in listMst_Product_ImportExcel)
                                {
                                    if (it.Mst_Product != null)
                                    {
                                        var _productCodeRoot = CUtils.StrValue(it.Mst_Product.ProductCodeRoot);
                                        var productRoot = it.Lst_Mst_Product.Where(x => x.ProductCode.Equals(_productCodeRoot)).FirstOrDefault();

                                        if (_productCodeRoot.Equals(productCodeRootCur))
                                        {
                                            var _productCodeBase = CUtils.StrValue(it.Mst_Product.ProductCodeBase);
                                            check = true;
                                            // tìm được sản phẩm root
                                            // kiểm tra xem đã tồn tại trong base chưa
                                            if (it.Lst_Mst_Product == null || it.Lst_Mst_Product.Count == 0)
                                            {
                                                it.Lst_Mst_Product = new List<Mst_Product_Import>();
                                            }

                                            var objProductBase = it.Lst_Mst_Product.Where(_it =>
                                                !CUtils.IsNullOrEmpty(_it.ProductCodeRoot) &&
                                                !CUtils.IsNullOrEmpty(_it.ProductCodeBase) &&
                                                _it.ProductCodeRoot.Equals(productCodeRootCur) &&
                                                _it.ProductCodeBase.Equals(productCodeBaseCur)).FirstOrDefault();
                                            if (objProductBase == null)
                                            {
                                                // chưa tồn tại => add list
                                                it.Lst_Mst_Product.Add(MapProduct_Import(new Mst_Product_Import(), objMst_Product, listPrd_AttributeUI,
                                                    listMst_ProductImages, listMst_ProductFiles, listPrd_BOM));
                                            }
                                            else
                                            {
                                                exitsData = "Mã hàng gốc '" + productCodeRootCur + "' - mã hàng liên quan '" + productCodeBaseCur + "' bị lặp trong file import!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                            break;
                                        }
                                    }
                                }

                                if (!check)
                                {
                                    // đây là sản phẩm mà chưa có root trong list
                                    // => tạo 1 root mới lấy root của sản phẩm hiện tại




                                }
                            }
                            else
                            {
                                var check = false;
                                foreach (var it in listMst_Product_ImportExcel)
                                {
                                    if (it.Mst_Product != null)
                                    {
                                        var _productCodeRoot = CUtils.StrValue(it.Mst_Product.ProductCodeRoot);
                                        var productRoot = it.Lst_Mst_Product.Where(x => x.ProductCode.Equals(_productCodeRoot)).FirstOrDefault();

                                        if (_productCodeRoot.Equals(productCodeRootCur))
                                        {
                                            var _productCodeBase = CUtils.StrValue(it.Mst_Product.ProductCodeBase);
                                            check = true;
                                            // tìm được sản phẩm root
                                            // kiểm tra xem đã tồn tại trong base chưa
                                            if (it.Lst_Mst_Product == null || it.Lst_Mst_Product.Count == 0)
                                            {
                                                it.Lst_Mst_Product = new List<Mst_Product_Import>();
                                            }

                                            var objProduct = it.Lst_Mst_Product.Where(_it =>
                                                !CUtils.IsNullOrEmpty(_it.ProductCodeRoot) &&
                                                !CUtils.IsNullOrEmpty(_it.ProductCodeBase) &&
                                                !CUtils.IsNullOrEmpty(_it.ProductCode) &&
                                                _it.ProductCodeRoot.Equals(productCodeRootCur) &&
                                                _it.ProductCodeBase.Equals(productCodeBaseCur) &&
                                                _it.ProductCode.Equals(productCodeCur)).FirstOrDefault();
                                            if (objProduct == null)
                                            {
                                                if (!CUtils.IsNullOrEmpty(productRoot.ProductType))
                                                {
                                                    if (productRoot.ProductType.Equals(objMst_Product.ProductType))
                                                    {
                                                        if (productRoot.ProductType.Equals("COMBO"))
                                                        {
                                                            if (!productRoot.UnitCode.Equals(objMst_Product.UnitCode))
                                                            {
                                                                exitsData = "Mã hàng gốc '" + _productCodeRoot + "' không có đơn vị quy đổi!" + strRows;
                                                                resultEntry.AddMessage(exitsData);
                                                                return Json(resultEntry);
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        exitsData = "Mã hàng '" + objMst_Product.ProductCodeUser + "' phải có loại hàng là " + productRoot.ProductType + "!" + strRows;
                                                        resultEntry.AddMessage(exitsData);
                                                        return Json(resultEntry);
                                                    }
                                                }
                                                if (productRoot.FlagSerial.Equals("1"))
                                                {
                                                    if (!productRoot.UnitCode.Equals(objMst_Product.UnitCode))
                                                    {
                                                        exitsData = "Mã hàng gốc '" + _productCodeRoot + "' không có đơn vị quy đổi khi cờ Flag Serial là '1'!" + strRows;
                                                        resultEntry.AddMessage(exitsData);
                                                        return Json(resultEntry);
                                                    }
                                                }
                                                // chưa tồn tại => add list
                                                it.Lst_Mst_Product.Add(MapProduct_Import(new Mst_Product_Import(), objMst_Product, listPrd_AttributeUI,
                                                    listMst_ProductImages, listMst_ProductFiles, listPrd_BOM));
                                            }
                                            else
                                            {
                                                exitsData = "Mã hàng gốc '" + productCodeRootCur + "' - mã hàng liên quan '" + productCodeBaseCur + "' - mã hàng '" + productCodeCur + "' bị lặp trong file import!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }

                            #endregion
                            //if (flagimport.Equals("0"))
                            //{
                            //    var objMst_Product_ImportExcel = new Mst_Product_ImportExcel()
                            //    {
                            //        Lst_Mst_Product = new List<Mst_Product_Import>(),
                            //    };
                            //    objMst_Product_ImportExcel.Lst_Mst_Product.Add(
                            //        MapProduct_Import(new Mst_Product_Import(), objMst_Product, listPrd_AttributeUI,
                            //            listMst_ProductImages, listMst_ProductFiles, listPrd_BOM));
                            //    listMst_Product_ImportExcel.Add(objMst_Product_ImportExcel);
                            //}
                            idx++;
                        }
                        #endregion

                        if (listMst_Product_ImportExcel != null && listMst_Product_ImportExcel.Count > 0)
                        {
                            //flagimport = "1";
                            if (flagimport.Equals("1"))
                            {
                                // Báo lỗi và dừng import
                                // trường hợp tạo mới
                                var productCodeRoot = "";
                                var productCodeBase = "";
                                foreach (var item in listMst_Product_ImportExcel)
                                {
                                    // tạo từng root
                                    var listMst_Product_Input = new List<Mst_Product>();
                                    var listPrd_Attribute_Input = new List<Prd_Attribute>();
                                    var listPrd_BOM_Input = new List<Prd_BOM>();
                                    var listMst_ProductFiles_Input = new List<Mst_ProductFiles>();
                                    var listMst_ProductImages_Input = new List<Mst_ProductImages>();
                                    var productRoot = new Mst_Product();
                                    var listBOMRoot = new List<Prd_BOM>();

                                    if (item.Lst_Mst_Product != null && item.Lst_Mst_Product.Count > 0)
                                    {
                                        var objRQ_Seq_ObjCode = new RQ_Seq_ObjCode()
                                        {
                                            // WARQBase
                                            Tid = GetNextTId(),
                                            GwUserCode = OS_ProductCentrer_GwUserCode,
                                            GwPassword = OS_ProductCentrer_GwPassword,
                                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                            FuncType = null,
                                            Ft_Cols_Upd = null,

                                            ObjCodeAmount = item.Lst_Mst_Product.Count,
                                        };
                                        var listProductCode = WA_Seq_GenObjCode_Get(objRQ_Seq_ObjCode);
                                        // List thuộc tính
                                        var listAttributeCur = new List<Prd_AttributeUI>();
                                        var listAttributeName = new List<string>();
                                        // List hàng thần phần (List BOM)
                                        var listPrd_BOMCur = new List<Prd_BOM>();
                                        var listProductName = new List<string>();


                                        for (int i = 0; i < item.Lst_Mst_Product.Count; i++)
                                        {
                                            if (i == 0)
                                            {
                                                productCodeRoot = CUtils.StrValue(listProductCode[i]);
                                                productRoot = item.Lst_Mst_Product[0];
                                                listBOMRoot = item.Lst_Mst_Product[0].Lst_Prd_BOM;
                                            }
                                            if (CUtils.StrValue(item.Lst_Mst_Product[i].ProductLevelSys).Equals(ProductLevelSys.RootPrd) || CUtils.StrValue(item.Lst_Mst_Product[i].ProductLevelSys).Equals(ProductLevelSys.BasePrd))
                                            {
                                                productCodeBase = CUtils.StrValue(listProductCode[i]);
                                            }

                                            item.Lst_Mst_Product[i].ProductCodeRoot = productCodeRoot;
                                            item.Lst_Mst_Product[i].ProductCodeBase = productCodeBase;
                                            item.Lst_Mst_Product[i].ProductCode = CUtils.StrValue(listProductCode[i]);
                                            item.Lst_Mst_Product[i].OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                            item.Lst_Mst_Product[i].NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                            item.Lst_Mst_Product[i].FlagFG = "0";
                                            listMst_Product_Input.Add(MapProduct_Product_Import(new Mst_Product(), item.Lst_Mst_Product[i]));

                                            if (item.Lst_Mst_Product[i].Lst_Prd_AttributeUI != null &&
                                                item.Lst_Mst_Product[i].Lst_Prd_AttributeUI.Count > 0)
                                            {
                                                //listAttributeCur.AddRange(item.Lst_Mst_Product[i].Lst_Prd_AttributeUI);
                                                foreach (var _it in item.Lst_Mst_Product[i].Lst_Prd_AttributeUI)
                                                {
                                                    listAttributeCur.Add(new Prd_AttributeUI()
                                                    {
                                                        AttributeCode = CUtils.StrValue(_it.AttributeCode),
                                                        AttributeName = CUtils.StrValue(_it.AttributeName),
                                                        AttributeValue = CUtils.StrValue(_it.AttributeValue),
                                                        ProductCode = item.Lst_Mst_Product[i].ProductCode,
                                                        OrgID = item.Lst_Mst_Product[i].OrgID,
                                                        NetworkID = item.Lst_Mst_Product[i].NetworkID,
                                                    });
                                                    listAttributeName.Add(CUtils.StrValue(_it.AttributeName));
                                                }
                                            }
                                            if (item.Lst_Mst_Product[i].Lst_Prd_BOM != null &&
                                                item.Lst_Mst_Product[i].Lst_Prd_BOM.Count > 0)
                                            {
                                                //listPrd_BOMCur.AddRange(item.Lst_Mst_Product[i].Lst_Prd_BOM);
                                                foreach (var _it in item.Lst_Mst_Product[i].Lst_Prd_BOM)
                                                {
                                                    _it.ProductCodeParent = item.Lst_Mst_Product[i].ProductCode;
                                                    _it.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                    _it.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);

                                                    listProductName.Add(CUtils.StrValue(_it.mp_ProductName));
                                                    listPrd_BOMCur.Add(_it);
                                                }
                                                //if (listBOMRoot != null && listBOMRoot.Count > 0)
                                                //{
                                                //    foreach (var it_RootBOM in listBOMRoot)
                                                //    {

                                                //    }
                                                //}

                                            }
                                            else
                                            {
                                                if (listBOMRoot != null && listBOMRoot.Count > 0)
                                                {
                                                    foreach (var _itRoot in listBOMRoot)
                                                    {
                                                        var objPrd_BOM = new Prd_BOM();
                                                        objPrd_BOM.ProductCodeParent = item.Lst_Mst_Product[i].ProductCode;
                                                        objPrd_BOM.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                        objPrd_BOM.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                                        objPrd_BOM.mp_ProductName = _itRoot.mp_ProductName;
                                                        objPrd_BOM.Qty = _itRoot.Qty;
                                                        listProductName.Add(CUtils.StrValue(_itRoot.mp_ProductName));

                                                        listPrd_BOMCur.Add(objPrd_BOM);
                                                    }
                                                }
                                            }

                                            if (item.Lst_Mst_Product[i].Lst_Mst_ProductImages.Count > 0)
                                            {
                                                //listMst_ProductImages_Input.AddRange(item.Lst_Mst_Product[i].Lst_Mst_ProductImages);
                                                foreach (var it_Image in item.Lst_Mst_Product[i].Lst_Mst_ProductImages)
                                                {
                                                    it_Image.ProductCode = item.Lst_Mst_Product[i].ProductCode;
                                                    listMst_ProductImages_Input.Add(it_Image);
                                                }
                                            }
                                            if (item.Lst_Mst_Product[i].Lst_Mst_ProductFiles.Count > 0)
                                            {
                                                //listMst_ProductFiles_Input.AddRange(item.Lst_Mst_Product[i].Lst_Mst_ProductFiles);
                                                foreach (var it_File in item.Lst_Mst_Product[i].Lst_Mst_ProductFiles)
                                                {
                                                    it_File.ProductCode = item.Lst_Mst_Product[i].ProductCode;
                                                    listMst_ProductFiles_Input.Add(it_File);
                                                }
                                            }
                                        }

                                        if (listAttributeName != null && listAttributeName.Count > 0)
                                        {
                                            listAttributeName = listAttributeName.Distinct().ToList();
                                            var strAttributeName = string.Join(",", listAttributeName);
                                            var strWhereClauseMst_Attribute = strWhereClause_Mst_Attributet(new Mst_Attribute() { AttributeName = strAttributeName, NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID), FlagActive = "1" });
                                            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
                                            {
                                                // WARQBase
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
                                                Ft_WhereClause = strWhereClauseMst_Attribute,
                                                Ft_Cols_Upd = null,
                                                // RQ_Mst_Attribute
                                                Rt_Cols_Mst_Attribute = "*",
                                                Mst_Attribute = null,
                                            };
                                            var listMst_AttributeNotExsits = new List<Prd_AttributeUI>();
                                            var listMst_AttributeSearch = WA_Mst_Attribute_Get(objRQ_Mst_Attribute);
                                            if (listMst_AttributeSearch != null && listMst_AttributeSearch.Count > 0)
                                            {
                                                foreach (var it in listAttributeCur)
                                                {
                                                    var objAttributeCur = listMst_AttributeSearch
                                                        .Where(_it =>
                                                            !CUtils.IsNullOrEmpty(_it.AttributeName) &&
                                                            _it.AttributeName.Equals(it.AttributeName))
                                                        .FirstOrDefault();
                                                    if (objAttributeCur == null)
                                                    {
                                                        listMst_AttributeNotExsits.Add(it);
                                                    }
                                                    else
                                                    {
                                                        listPrd_Attribute_Input.Add(new Prd_Attribute()
                                                        {
                                                            AttributeCode = CUtils.StrValue(objAttributeCur.AttributeCode),
                                                            AttributeValue = CUtils.StrValue(it.AttributeValue),
                                                            ProductCode = CUtils.StrValue(it.ProductCode),
                                                            OrgID = CUtils.StrValue(it.OrgID),
                                                            NetworkID = CUtils.StrValue(it.NetworkID),
                                                        });
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                listMst_AttributeNotExsits.AddRange(listAttributeCur);
                                            }
                                            if (listMst_AttributeNotExsits != null &&
                                                listMst_AttributeNotExsits.Count > 0)
                                            {
                                                // thêm thuộc tính vào database
                                                foreach (var attribute in listMst_AttributeNotExsits)
                                                {
                                                    var objRQ_Mst_Attribute_Create = new RQ_Mst_Attribute()
                                                    {
                                                        // WARQBase
                                                        Tid = GetNextTId(),
                                                        GwUserCode = OS_ProductCentrer_GwUserCode,
                                                        GwPassword = OS_ProductCentrer_GwPassword,
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
                                                        Ft_Cols_Upd = null,
                                                        // RQ_Mst_Product
                                                        Rt_Cols_Mst_Attribute = null,
                                                    };
                                                    var attributeCode = WA_Seq_GenCommonCode_Get("ATTRIBUTECODE");
                                                    objRQ_Mst_Attribute_Create.Mst_Attribute = new Mst_Attribute
                                                    {
                                                        AttributeCode = attributeCode,
                                                        AttributeName = attribute.AttributeName,
                                                    };
                                                    Mst_AttributeService.Instance.WA_Mst_Attribute_Create(objRQ_Mst_Attribute_Create, UserState.AddressAPIs_Product_Customer_Centrer);
                                                    listPrd_Attribute_Input.Add(new Prd_Attribute()
                                                    {
                                                        AttributeCode = attributeCode,
                                                        AttributeValue = CUtils.StrValue(attribute.AttributeValue),
                                                        ProductCode = CUtils.StrValue(attribute.ProductCode),
                                                    });
                                                }
                                            }
                                        }

                                        if (listProductName != null && listProductName.Count > 0)
                                        {
                                            listProductName = listProductName.Distinct().ToList();
                                            var strProductName = string.Join(",", listProductName);
                                            var strWhereClauseMst_Product_SearchName = strWhereClause_Mst_Product_Search_In(new Mst_Product()
                                            {
                                                ProductCodeUser = strProductName,
                                                ProductName = strProductName,
                                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                                FlagActive = "1",
                                                FlagFG = "0",
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
                                                Ft_WhereClause = strWhereClauseMst_Product_SearchName,
                                                Ft_Cols_Upd = null,
                                                // RQ_Mst_Product
                                                Rt_Cols_Mst_Product = "*",
                                                Rt_Cols_Mst_ProductImages = null,
                                                Rt_Cols_Mst_ProductFiles = null,
                                                Rt_Cols_Prd_BOM = "",
                                                Rt_Cols_Prd_Attribute = "",
                                                Lst_Mst_Product = null,
                                                Lst_Mst_ProductImages = null,
                                                Lst_Mst_ProductFiles = null,
                                                Lst_Prd_BOM = null,
                                                Lst_Prd_Attribute = null,
                                            };
                                            var listMst_ProductSearch = WA_Mst_Product_Get(objRQ_Mst_Product);

                                            var listMst_ProductNotExsist = new List<Prd_BOM>();
                                            if (listMst_ProductSearch != null && listMst_ProductSearch.Count > 0)
                                            {
                                                foreach (var it in listPrd_BOMCur)
                                                {
                                                    // check theo mã
                                                    var objMst_Product_ProductCodeUser_Cur = listMst_ProductSearch
                                                        .Where(_it =>
                                                            !CUtils.IsNullOrEmpty(_it.ProductCodeUser) &&
                                                            _it.ProductCodeUser.Equals(it.mp_ProductName))
                                                        .FirstOrDefault();
                                                    var objMst_Product_ProductName_Cur = listMst_ProductSearch
                                                        .Where(_it =>
                                                            !CUtils.IsNullOrEmpty(_it.ProductName) &&
                                                            _it.ProductName.Equals(it.mp_ProductName))
                                                        .FirstOrDefault();
                                                    if (objMst_Product_ProductCodeUser_Cur == null && objMst_Product_ProductName_Cur == null)
                                                    {
                                                        listMst_ProductNotExsist.Add(it);
                                                    }
                                                    else
                                                    {
                                                        if (objMst_Product_ProductCodeUser_Cur != null)
                                                        {
                                                            if (productRoot.ProductType.Equals("COMBO") && productRoot.ProductType.Equals(objMst_Product_ProductCodeUser_Cur.ProductType))
                                                            {
                                                                exitsData = "Mã hàng gốc '" + productRoot.ProductCodeUser + "' - mã hàng liên quan '" + objMst_Product_ProductCodeUser_Cur.ProductCodeUser + "' không được là combo!";
                                                                resultEntry.AddMessage(exitsData);
                                                                return Json(resultEntry);
                                                            }

                                                            it.ProductCode = objMst_Product_ProductCodeUser_Cur.ProductCode;
                                                            it.OrgIDParent = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                            it.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                                            listPrd_BOM_Input.Add(it);
                                                        }
                                                        else if (objMst_Product_ProductName_Cur != null)
                                                        {
                                                            if (productRoot.ProductType.Equals("COMBO") && productRoot.ProductType.Equals(objMst_Product_ProductCodeUser_Cur.ProductType))
                                                            {
                                                                exitsData = "Mã hàng gốc '" + productRoot.ProductCodeUser + "' - mã hàng liên quan '" + objMst_Product_ProductCodeUser_Cur.ProductCodeUser + "' không được là combo!";
                                                                resultEntry.AddMessage(exitsData);
                                                                return Json(resultEntry);
                                                            }
                                                            it.ProductCode = objMst_Product_ProductName_Cur.ProductCode;
                                                            it.OrgIDParent = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                            it.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);

                                                            listPrd_BOM_Input.Add(it);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                listMst_ProductNotExsist.AddRange(listPrd_BOMCur);
                                            }
                                            if (listMst_ProductNotExsist != null && listMst_ProductNotExsist.Count > 0)
                                            {

                                                var listProductNameNotExsist = "";
                                                for (int i = 0; i < listMst_ProductNotExsist.Count; i++)
                                                {
                                                    listProductNameNotExsist += CUtils.StrValue(listMst_ProductNotExsist[i].mp_ProductName);
                                                    if (i < listMst_ProductNotExsist.Count - 1)
                                                    {
                                                        listProductNameNotExsist += ", ";
                                                    }
                                                }

                                                exitsData = "Mã hàng thành phần '" + listProductNameNotExsist + "' không có trên hệ thống!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }





                                        }

                                        // thêm file, ảnh ở đây

                                        // Tạo hàng hóa
                                        var objRQ_Mst_Product_Create = new RQ_Mst_Product();
                                        objRQ_Mst_Product_Create.Tid = GetNextTId();
                                        objRQ_Mst_Product_Create.GwUserCode = OS_ProductCentrer_GwUserCode;
                                        objRQ_Mst_Product_Create.GwPassword = OS_ProductCentrer_GwPassword;
                                        objRQ_Mst_Product_Create.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                                        objRQ_Mst_Product_Create.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                                        objRQ_Mst_Product_Create.NetworkID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                        objRQ_Mst_Product_Create.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                        objRQ_Mst_Product_Create.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                                        objRQ_Mst_Product_Create.FuncType = null;
                                        objRQ_Mst_Product_Create.Ft_RecordStart = null;
                                        objRQ_Mst_Product_Create.Ft_RecordCount = null;
                                        objRQ_Mst_Product_Create.Ft_WhereClause = null;
                                        objRQ_Mst_Product_Create.Ft_Cols_Upd = null;
                                        objRQ_Mst_Product_Create.Lst_Mst_Product = new List<Mst_Product>();
                                        if (listMst_Product_Input != null && listMst_Product_Input.Count > 0)
                                        {
                                            objRQ_Mst_Product_Create.Lst_Mst_Product.AddRange(listMst_Product_Input);
                                        }
                                        objRQ_Mst_Product_Create.Lst_Prd_Attribute = new List<Prd_Attribute>();
                                        if (listPrd_Attribute_Input != null && listPrd_Attribute_Input.Count > 0)
                                        {
                                            objRQ_Mst_Product_Create.Lst_Prd_Attribute.AddRange(listPrd_Attribute_Input);
                                        }
                                        // Danh sách BOM
                                        objRQ_Mst_Product_Create.Lst_Prd_BOM = new List<Prd_BOM>();
                                        if (listPrd_BOM_Input != null && listPrd_BOM_Input.Count > 0)
                                        {
                                            objRQ_Mst_Product_Create.Lst_Prd_BOM.AddRange(listPrd_BOM_Input);
                                        }
                                        // Danh sách Files
                                        objRQ_Mst_Product_Create.Lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
                                        if (listMst_ProductFiles_Input != null && listMst_ProductFiles_Input.Count > 0)
                                        {
                                            objRQ_Mst_Product_Create.Lst_Mst_ProductFiles.AddRange(listMst_ProductFiles_Input);
                                        }
                                        // Danh sách Images
                                        objRQ_Mst_Product_Create.Lst_Mst_ProductImages = new List<Mst_ProductImages>();
                                        if (listMst_ProductImages_Input != null && listMst_ProductImages_Input.Count > 0)
                                        {
                                            objRQ_Mst_Product_Create.Lst_Mst_ProductImages.AddRange(listMst_ProductImages_Input);
                                        }
                                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product_Create);
                                        Mst_ProductService.Instance.WA_Mst_Product_Create(objRQ_Mst_Product_Create, UserState.AddressAPIs_Product_Customer_Centrer);
                                        // Lưu tại network
                                        objRQ_Mst_Product_Create.GwUserCode = GwUserCode;
                                        objRQ_Mst_Product_Create.GwPassword = GwPassword;
                                        objRQ_Mst_Product_Create.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                                        objRQ_Mst_Product_Create.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                                        objRQ_Mst_Product_Create.FlagIsDelete = "0";
                                        Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product_Create, UserState.AddressAPIs);
                                    }


                                }
                            }
                            else
                            {
                                // 
                                // trường hợp tạo mới và update
                                var objRQ_Mst_Product_Update = new RQ_Mst_Product()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_ProductCentrer_GwUserCode,
                                    GwPassword = OS_ProductCentrer_GwPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStartExportExcel,
                                    Ft_RecordCount = RowsWorksheets.ToString(),
                                    Ft_WhereClause = "",
                                    Ft_Cols_Upd = null,

                                    // RQ_Form_Payment
                                    Rt_Cols_Mst_Product = "*",
                                    Rt_Cols_Mst_ProductFiles = "*",
                                    Rt_Cols_Mst_ProductImages = "*",
                                    Rt_Cols_Prd_Attribute = null,
                                    Rt_Cols_Prd_BOM = null,
                                    Lst_Mst_Product = null,

                                };
                                var strFt_Cols_Upd = "";
                                var Tbl_Mst_Product = TableName.Mst_Product + ".";

                                #region ["strFt_Cols_Upd"]

                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductCodeUser);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductBarCode);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductName);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductNameEN);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductMnfUrl);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductOrigin);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductQuyCach);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductStd);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductUserGuide);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.QtyEffSt);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.QtyMaxSt);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.QtyMinSt);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.UPBuy);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.UPSell);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductIntro);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductExpiry);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.ProductDrawing);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.FlagLot);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.FlagBuy);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.FlagSell);
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Product, TblMst_Product.FlagSerial);
                                #endregion
                                objRQ_Mst_Product_Update.Lst_Mst_Product = new List<Mst_Product>();
                                objRQ_Mst_Product_Update.Lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
                                objRQ_Mst_Product_Update.Lst_Mst_ProductImages = new List<Mst_ProductImages>();

                                foreach (var item in listMst_Product_ImportExcel)
                                {
                                    if (item.Lst_Mst_Product != null && item.Lst_Mst_Product.Count > 0)
                                    {
                                        foreach (var it_Product in item.Lst_Mst_Product)
                                        {
                                            var strWhereClauseMst_Product_Item = "";
                                            var objMst_Product_Item = new Mst_Product()
                                            {
                                                ProductCodeUser = it_Product.ProductCodeUser,
                                                FlagFG = "0"
                                            };
                                            strWhereClauseMst_Product_Item = strWhereClause_Mst_Product(objMst_Product_Item);
                                            var objRQ_Mst_Product_Item = new RQ_Mst_Product()
                                            {
                                                // WARQBase
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
                                                Ft_RecordStart = Ft_RecordStartExportExcel,
                                                Ft_RecordCount = RowsWorksheets.ToString(),
                                                Ft_WhereClause = strWhereClauseMst_Product_Item,
                                                Ft_Cols_Upd = null,

                                                // RQ_Form_Payment
                                                Rt_Cols_Mst_Product = "*",
                                                Rt_Cols_Mst_ProductFiles = "*",
                                                Rt_Cols_Mst_ProductImages = "*",
                                                Rt_Cols_Prd_Attribute = null,
                                                Rt_Cols_Prd_BOM = null
                                            };

                                            var objRT_Mst_Product_Item = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product_Item, CUtils.StrValue(UserState.AddressAPIs));

                                            if (objRT_Mst_Product_Item.Lst_Mst_Product != null && objRT_Mst_Product_Item.Lst_Mst_Product.Count > 0)
                                            {
                                                #region ["Gán dữ liệu"]

                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductName = it_Product.ProductName;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].FlagLot = it_Product.FlagLot;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].FlagSerial = it_Product.FlagSerial;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].FlagSell = it_Product.FlagSell;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].FlagBuy = it_Product.FlagBuy;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductDrawing = it_Product.ProductDrawing;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductExpiry = it_Product.ProductExpiry;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductIntro = it_Product.ProductIntro;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductMnfUrl = it_Product.ProductMnfUrl;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductOrigin = it_Product.ProductOrigin;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductQuyCach = it_Product.ProductQuyCach;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductStd = it_Product.ProductStd;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductUserGuide = it_Product.ProductUserGuide;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].QtyEffSt = it_Product.QtyEffSt;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].QtyMaxSt = it_Product.QtyMaxSt;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].QtyMinSt = it_Product.QtyMinSt;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].Remark = it_Product.Remark;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].UPBuy = it_Product.UPBuy;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].UPSell = it_Product.UPSell;
                                                objRT_Mst_Product_Item.Lst_Mst_Product[0].FlagFG = it_Product.FlagFG;

                                                #endregion

                                                objRQ_Mst_Product_Update.Lst_Mst_Product.Add(objRT_Mst_Product_Item.Lst_Mst_Product[0]);
                                                foreach (var itFile in it_Product.Lst_Mst_ProductFiles)
                                                {
                                                    itFile.FlagIsFilePath = "1";
                                                    itFile.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                    itFile.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                                    itFile.ProductCode = objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductCode;
                                                    itFile.ProductFilePath = itFile.ProductFilePath;

                                                    objRQ_Mst_Product_Update.Lst_Mst_ProductFiles.Add(itFile);
                                                }
                                                foreach (var itImage in it_Product.Lst_Mst_ProductImages)
                                                {
                                                    itImage.FlagIsImagePath = "1";
                                                    itImage.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                                                    itImage.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                                                    itImage.ProductCode = objRT_Mst_Product_Item.Lst_Mst_Product[0].ProductCode;
                                                    itImage.ProductImagePath = itImage.ProductImagePath;

                                                    objRQ_Mst_Product_Update.Lst_Mst_ProductImages.Add(itImage);
                                                }
                                            }
                                            else
                                            {
                                                resultEntry.Success = true;
                                                exitsData = "Mã hàng hóa " + item.Lst_Mst_Product[0].ProductCodeUser + " không có trong hệ thống !";
                                                resultEntry.AddMessage(exitsData);
                                            }
                                        }
                                    }

                                }

                                objRQ_Mst_Product_Update.Ft_Cols_Upd = strFt_Cols_Upd;
                                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product_Update);
                                Mst_ProductService.Instance.WA_Mst_Product_UpdateMaster(objRQ_Mst_Product_Update, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));

                                resultEntry.Success = true;
                                exitsData = "Đã nhập dữ liệu excel thành công!";
                                resultEntry.AddMessage(exitsData);
                                resultEntry.RedirectUrl = Url.Action("Index", "Mst_Product");
                            }
                        }
                    }
                    resultEntry.Success = true;
                    exitsData = "Đã nhập dữ liệu excel thành công!";
                    resultEntry.AddMessage(exitsData);
                    resultEntry.RedirectUrl = Url.Action("Index", "Mst_Product");
                }
                else
                {
                    resultEntry.Success = true;
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
        #endregion

        #region Export
        #region ["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(
            string productCodeUserName = "",
            string productGrpCode = "",
            string ckbproduct = "",
            string ckbservices = "",
            string ckbcombo = "",
            string lstattribute = "")
        {
            
            var list = new List<Mst_Product>();
            var listAttr = new List<Mst_Attribute>();
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var strStt = "";
            var objListPrd_AttributeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Prd_Attribute>>(lstattribute);

            #region["CheckBox tìm kiếm"]
            if (!CUtils.IsNullOrEmpty(ckbproduct) && ckbproduct.Equals("1"))
            {
                strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'PRODUCT'", strStt) : string.Format("'PRODUCT'", strStt);
            }
            if (!CUtils.IsNullOrEmpty(ckbservices) && ckbservices.Equals("1"))
            {
                strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'SERVICE'", strStt) : string.Format("'SERVICE'", strStt);
            }
            if (!CUtils.IsNullOrEmpty(ckbcombo) && ckbcombo.Equals("1"))
            {
                strStt = !CUtils.IsNullOrEmpty(strStt) ? string.Format("{0}," + "'COMBO'", strStt) : string.Format("'COMBO'", strStt);
            }
            #endregion

            var objMst_Product = new Mst_Product()
            {
                      ProductLevelSys = "ROOTPRD",
                ProductCodeUser = productCodeUserName,
                ProductName = productCodeUserName,
                ProductGrpCode = productGrpCode,
                FlagFG = "0",
            };

            if (CUtils.IsNullOrEmpty(ckbproduct) && CUtils.IsNullOrEmpty(ckbservices) && CUtils.IsNullOrEmpty(ckbcombo))
            {
                objMst_Product.FlagActive = "1";
            }

            var strWhereClause_MstProduct = strWhereClause_Mst_Product_Search(objMst_Product, strStt, objListPrd_AttributeInput);

            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
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
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,

                Rt_Cols_Mst_Attribute = "*",
            };
            var objRT_Mst_Attribute = WA_Mst_Attribute_Get(objRQ_Mst_Attribute);
            if (objRT_Mst_Attribute != null && objRT_Mst_Attribute.Count > 0)
            {
                listAttr.AddRange(objRT_Mst_Attribute);
            }

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
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause_MstProduct,
                Ft_Cols_Upd = null,

                // RQ_Form_Payment
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductFiles = "*",
                Rt_Cols_Mst_ProductImages = "*",
                Rt_Cols_Prd_Attribute = "*",
                Rt_Cols_Prd_BOM = "*"
            };
            var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
            {
                // WARQBase
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
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,

                // RQ_Form_Payment
                Rt_Cols_Prd_DynamicField = "*",
            };
            var objRT_Prd_DynamicField = WA_Prd_DynamicField_Get(objRQ_Prd_DynamicField);
            var lstParamType = new List<PrdDynamicFieldParamType>();
            if (objRT_Prd_DynamicField != null && objRT_Prd_DynamicField.Count > 0)
            {
                listPrd_DynamicField.AddRange(objRT_Prd_DynamicField);
                lstParamType = listPrd_DynamicField[0].ParamTypes;
            }
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, UserState.AddressAPIs);
            if (objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
            {
                foreach (var product in objRT_Mst_Product.Lst_Mst_Product)
                {
                    // Truong dong 
                    var ListOfPrdDynamicFieldValue = new List<string>();
                    if (product.CustomDataDict != null && product.CustomDataDict.Count > 0)
                    {
                        foreach (var pair in product.CustomDataDict)
                        {
                            var paramTitle = lstParamType.FirstOrDefault(param => param.Code.Equals(pair.Key));
                            if (paramTitle != null)
                            {
                                var fieldValue = string.Format("{0}:{1}", paramTitle.Title, pair.Value);
                                ListOfPrdDynamicFieldValue.Add(fieldValue);
                            }
                        }
                        product.ListOfPrdDynamicFieldValue = string.Join("|", ListOfPrdDynamicFieldValue.ToArray());
                    }

                    var productImagePathList = new List<string>();
                    var productFilePathList = new List<string>();
                    var listOfAttribute = new List<string>();
                    var listBOM = new List<string>();
                    if (objRT_Mst_Product.Lst_Mst_ProductImages != null && objRT_Mst_Product.Lst_Mst_ProductImages.Count > 0)
                    {
                        foreach (var image in objRT_Mst_Product.Lst_Mst_ProductImages)
                        {
                            if (product.ProductCode.Equals(image.ProductCode))
                            {
                                productImagePathList.Add(image.ProductImagePath.ToString());
                            }
                        }
                        product.ProductImagePathList = string.Join("|", productImagePathList.ToArray());
                    }
                    if (objRT_Mst_Product.Lst_Mst_ProductFiles != null && objRT_Mst_Product.Lst_Mst_ProductFiles.Count > 0)
                    {
                        foreach (var file in objRT_Mst_Product.Lst_Mst_ProductFiles)
                        {
                            if (product.ProductCode.Equals(file.ProductCode))
                            {
                                productFilePathList.Add(file.ProductFilePath.ToString());
                            }
                        }
                        product.ProductFilePathList = string.Join("|", productFilePathList.ToArray());
                    }
                    if (objRT_Mst_Product.Lst_Prd_Attribute != null && objRT_Mst_Product.Lst_Prd_Attribute.Count > 0)
                    {
                        foreach (var attr in objRT_Mst_Product.Lst_Prd_Attribute)
                        {
                            var attrString = "";
                            if (product.ProductCode.Equals(attr.ProductCode))
                            {
                                const string Format = "{0}{1}:{2}";
                                var attrName = listAttr.FirstOrDefault(item => item.AttributeCode.Equals(attr.AttributeCode));
                                if (attrName != null)
                                {
                                    attrString = string.Format(Format, attrString, attrName.AttributeName, attr.AttributeValue);
                                    listOfAttribute.Add(attrString);
                                }
                            }
                        }
                        product.ListAttribute = string.Join("|", listOfAttribute.ToArray());
                    }
                    if (objRT_Mst_Product.Lst_Prd_BOM != null && objRT_Mst_Product.Lst_Prd_BOM.Count > 0)
                    {
                        foreach (var bom in objRT_Mst_Product.Lst_Prd_BOM)
                        {
                            if (product.ProductCode.Equals(bom.ProductCode))
                            {
                                var bomItem = string.Format("{0}:{1}", bom.mp_ProductName.ToString(), bom.Qty);
                                listBOM.Add(bomItem);
                            }
                        }
                        product.ListBOM = string.Join("|", listBOM.ToArray());
                    }
                }

                list.AddRange(objRT_Mst_Product.Lst_Mst_Product);
            }
            Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
            dicColNames.Add(TblMst_Product.FlagActive, "Trạng thái");
            dicColNames.Remove(TblMst_Product.ProductCodeBase);
            string url = "";
            string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Product).Name), ref url);
            ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Product"));

            return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
        }
        #endregion

        #endregion
        #endregion

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowPopupImportExcel()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                return JsonView("_ImportExcel", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_ImportExcel", null, resultEntry);
        }
        #endregion

        #region ["GetDicCloumns"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>
            {
                //{TblMst_Product.ProductCode, "Mã sản phẩm(*)"},
                {TblMst_Product.ProductCodeUser, "Mã hàng(*)"},
                {TblMst_Product.ProductName, "Tên hàng(*)"},
                //{TblMst_Product.ProductNameEN, "Tên tiếng Anh"},
                {TblMst_Product.ProductBarCode, "Mã vạch"},
                {TblMst_Product.ProductCodeBase, "Mã hàng liên quan"},//Nếu user ko nhập,lưu =Mã hàng
                {TblMst_Product.ProductCodeRoot, "Mã hàng gốc"},//Nếu user ko nhập,lưu =Mã hàng
                {TblMst_Product.BrandCode, "Mã nhãn hiệu"},
                {TblMst_Product.ProductGrpCode, "Nhóm hàng (*)"},
                {TblMst_Product.ProductType, "Loại hàng"},//Nếu user ko nhập,lưu =PRODUCT (Sản phẩm)
                {TblMst_Product.ProductImagePathList, "Ảnh sản phẩm"},
                {TblMst_Product.ProductFilePathList, "File đính kèm"},
                {TblMst_Product.FlagSerial, "QL Serial"},//Nếu user ko nhập,lưu =0
                {TblMst_Product.FlagLot, "QL LOT"},//Nếu user ko nhập,lưu =0
                {TblMst_Product.UnitCode, "Đơn vị"},
                //{"UnitCodeBase", "Đơn vị cơ bản"},
                {TblMst_Product.ValConvert, "Quy đổi"},
                {TblMst_Product.FlagSell, "Được bán"},//Nếu user ko nhập,lưu =1
                {TblMst_Product.FlagBuy, "Được mua"},//Nếu user ko nhập,lưu =1
                {TblMst_Product.UPBuy, "Giá mua"},
                {TblMst_Product.UPSell, "Giá bán"},
                {TblMst_Product.QtyMinSt, "Tồn nhỏ nhất"},
                {TblMst_Product.QtyEffSt, "Tồn tối ưu"},
                {TblMst_Product.QtyMaxSt, "Tồn lớn nhất"},
                {TblMst_Product.ListAttribute, "Thuộc tính"},
                {TblMst_Product.ListBOM, "Hàng thành phần"},
                {TblMst_Product.ListOfPrdDynamicFieldValue, "List các trường động"},
                {TblMst_Product.ProductStd, "Tiêu chuẩn"},
                {TblMst_Product.ProductExpiry, "Hạn sử dụng"},
                {TblMst_Product.ProductQuyCach, "Quy cách"},
                {TblMst_Product.ProductMnfUrl, "Link quy trình sx"},
                {TblMst_Product.ProductIntro, "Chi tiết sản phẩm"},
                {TblMst_Product.ProductUserGuide, "Hướng dẫn sử dụng"},
                {TblMst_Product.ProductDrawing, "Bản vẽ"},
                {TblMst_Product.ProductOrigin, "Xuất xứ"},
                {TblMst_Product.Remark, "Ghi chú"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Product_CustomField_Search(Product_CustomField data)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Product_CustomField + ".";
            var sbSql = new StringBuilder();


            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblProduct_CustomField.OrgID, data.OrgID, "=");
            }

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblProduct_CustomField.FlagActive, data.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Prd_DynamicField(Prd_DynamicField model)
        {
            var strWhereClause = "";
            var Tbl_Prd_DynamicField = TableName.Prd_DynamicField + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Prd_DynamicField + "OrgID", CUtils.StrValue(model.OrgID), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Attributet(Mst_Attribute model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Attribute = TableName.Mst_Attribute + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.AttributeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Attribute + "AttributeName", CUtils.StrValue(model.AttributeName), "in");
            }
            if (!CUtils.IsNullOrEmpty(model.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Attribute + "NetworkID", model.NetworkID, "=");
            }

            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Attribute + "FlagActive", model.FlagActive, "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Product_Search_In(Mst_Product model)
        {
            var strWhere = "";
            var strWhereClause = "";
            var strWhereClause1 = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();
            var sbSql1 = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValue(model.ProductCodeUser), "in");

                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, CUtils.StrValue(model.ProductCodeUser), "in", "or");
            }

            if (!CUtils.IsNullOrEmpty(model.NetworkID))
            {
                sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.NetworkID, model.NetworkID, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.OrgID))
            {
                sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.OrgID, model.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, model.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            strWhereClause1 = sbSql1.ToString();
            if (!CUtils.IsNullOrEmpty(strWhereClause))
            {
                strWhere = "( " + strWhereClause + " )";
            }
            if (!CUtils.IsNullOrEmpty(strWhereClause1))
            {
                if (!CUtils.IsNullOrEmpty(strWhere))
                {
                    strWhere += (" and " + strWhereClause1);
                }
            }
            return strWhere;
        }
        private string strWhereClause_Mst_Product(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductCodeRoot))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCodeRoot, "=");
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
            if (!CUtils.IsNullOrEmpty(model.ProductType))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, model.ProductType, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.UnitCode, model.UnitCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, model.ProductName, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Product_Get_Children(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCode, "=");
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, model.ProductCode, "!=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereGetProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValueOrNull(data.ProductCodeUser), "=");
            }
            strWhereClause = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, CUtils.StrValueOrNull(data.ProductName), "=");
                strWhereClause += " or " + "Mst_Product.ProductName = '" + data.ProductName + "'";
            }
            if (!CUtils.IsNullOrEmpty(data.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, data.FlagFG, "=");
            }
            return strWhereClause;
        }
        private string strWhereSearchProductProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValueOrNull(data.ProductCodeUser) + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValueOrNull(data.ProductName) + "%", "like");
                strWhereClause += " or " + "Mst_Product.ProductName like %" + data.ProductName + "%";
            }
            if (!CUtils.IsNullOrEmpty(data.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, data.FlagFG, "=");
            }
            return strWhereClause;
        }

        private string strWhereSearchProductByListProduct(List<Mst_Product> data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();

            if (data != null && data.Count > 0)
            {
                var strPrd = "";
                foreach (var item in data)
                {
                    strPrd = !CUtils.IsNullOrEmpty(strPrd) ? string.Format("{0}," + "'" + item.ProductCode + "'", strPrd) : string.Format("'" + item.ProductCode + "'", strPrd);
                }

                if (!string.IsNullOrEmpty(strPrd))
                {
                    strWhereClause = "Mst_Product.ProductCode in " + strPrd;
                }
            }

            return strWhereClause;
        }
        //private string strWhereClause_Mst_Product_Search(Mst_Product model, string strStt, List<Prd_Attribute> listPrd_Attribute)
        //{
        //    var strWhereClause = "";
        //    var Tbl_Mst_Product = TableName.Mst_Product + ".";
        //    var Tbl_Prd_Attribute = TableName.Prd_Attribute + ".";
        //    var sbSql = new StringBuilder();

        //    if (!CUtils.IsNullOrEmpty(model.ProductCode))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, model.ProductCode, "=");
        //    }

        //    if (!CUtils.IsNullOrEmpty(model.ProductGrpCode))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductGrpCode, model.ProductGrpCode, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(model.ProductLevelSys))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductLevelSys, model.ProductLevelSys, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(model.BrandCode))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.BrandCode, model.BrandCode, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(model.UnitCode))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.UnitCode, model.UnitCode, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(model.FlagActive))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, model.FlagActive, "=");
        //    }
        //    if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
        //    {
        //        foreach (var item in listPrd_Attribute)
        //        {
        //            sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeCode, item.AttributeCode, "=");
        //            sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeValue, "%" + item.AttributeValue + "%", "like");
        //        }
        //    }

        //    strWhereClause = sbSql.ToString();
        //    var sbSql1 = new StringBuilder();
        //    if (!CUtils.IsNullOrEmpty(model.ProductCodeUser))
        //    {
        //        if (strWhereClause.Length > 0)
        //        {
        //            strWhereClause += " and (";
        //            sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
        //        }
        //        else
        //        {
        //            strWhereClause += " (";
        //            sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
        //        }
        //    }
        //    if (!CUtils.IsNullOrEmpty(model.ProductName))
        //    {
        //        if (strWhereClause.Length > 0)
        //        {
        //            sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + model.ProductName + "%", "like", "or");
        //            strWhereClause += sbSql1.ToString();
        //            strWhereClause += ")";
        //        }
        //    }





        //    if (!string.IsNullOrEmpty(strStt))
        //    {
        //        if (strWhereClause.Length > 0)
        //        {
        //            strWhereClause = strWhereClause + " and Mst_Product.ProductType in " + strStt;
        //        }
        //        else
        //        {
        //            strWhereClause = "Mst_Product.ProductType in " + strStt;
        //        }
        //    }
        //    return strWhereClause;
        //}




        private string strWhereClause_Mst_Product_Search(Mst_Product model, string strStt, List<Prd_Attribute> listPrd_Attribute)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var Tbl_Prd_Attribute = TableName.Prd_Attribute + ".";
            var sbSql = new StringBuilder();

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
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, model.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            #region["Nâng cấp tìm kiếm tương đối với thuộc tính - Lộc 2021-08-12"]

            var strWhereClause_Attribute = "";
            var sbSql_Attribute = new StringBuilder();
            if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
            {
                var idx = 0;
                strWhereClause_Attribute += " ( ";
                foreach (var item in listPrd_Attribute)
                {
                    idx++;
                    var strWhereClause_Item_Attribute = " ( ";
                    var sbSql_Item_Attribute = new StringBuilder();
                    sbSql_Item_Attribute.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeCode, item.AttributeCode, "=");
                    sbSql_Item_Attribute.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeValue, "%" + item.AttributeValue + "%", "like");
                    strWhereClause_Item_Attribute += sbSql_Item_Attribute.ToString();
                    if (idx < listPrd_Attribute.Count)
                    {
                        strWhereClause_Item_Attribute += " ) or";
                    }
                    else
                    {
                        strWhereClause_Item_Attribute += " ) ";
                    }
                    strWhereClause_Attribute += strWhereClause_Item_Attribute;
                }
                strWhereClause_Attribute += " ) ";
            }



            //if (listPrd_Attribute != null && listPrd_Attribute.Count > 0)
            //{
            //    foreach (var item in listPrd_Attribute)
            //    {
            //        sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeCode, item.AttributeCode, "=");
            //        sbSql.AddWhereClause(Tbl_Prd_Attribute + TblPrd_Attribute.AttributeValue, "%" + item.AttributeValue + "%", "like");
            //    }
            //}

            //strWhereClause = sbSql.ToString();

            #endregion

            strWhereClause = sbSql.ToString();
            if (!string.IsNullOrEmpty(strWhereClause_Attribute) && strWhereClause_Attribute.Length > 0)
            {
                if (strWhereClause.Length > 0)
                {
                    strWhereClause += " and " + strWhereClause_Attribute;
                }
                else
                {
                    strWhereClause = strWhereClause_Attribute;
                }
            }

            var sbSql1 = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(model.ProductCodeUser))
            {
                if (strWhereClause.Length > 0)
                {
                    strWhereClause += " and (";
                    sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
                }
                else
                {
                    strWhereClause += " (";
                    sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, model.ProductCodeUser, "=");
                }
            }
            if (!CUtils.IsNullOrEmpty(model.ProductName))
            {
                if (strWhereClause.Length > 0)
                {
                    sbSql1.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + model.ProductName + "%", "like", "or");
                    strWhereClause += sbSql1.ToString();
                    strWhereClause += ")";
                }
            }

            if (!string.IsNullOrEmpty(strStt))
            {
                if (strWhereClause.Length > 0)
                {
                    strWhereClause = strWhereClause + " and Mst_Product.ProductType in " + strStt;
                }
                else
                {
                    strWhereClause = "Mst_Product.ProductType in " + strStt;
                }
            }
            return strWhereClause;
        }

        private string strWhereClause_Mst_Product_Get_Base(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductLevelSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductLevelSys, model.ProductLevelSys, "!=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Product_Get_Level2(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();

            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductLevelSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductLevelSys, model.ProductLevelSys, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Product_Update(Mst_Product model)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            var sbSql = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(model.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, model.ProductCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, model.ProductCodeBase, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductCodeRoot))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeRoot, model.ProductCodeRoot, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ProductLevelSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductLevelSys, model.ProductLevelSys, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, model.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;


        }
        private string strWhereClause_Mst_ProductProductUserCodeOrProductName(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(data.ProductCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, CUtils.StrValue(data.ProductCodeBase), "=");
            }

            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.BrandCode, CUtils.StrValue(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductGrpCode, CUtils.StrValue(data.ProductGrpCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }

            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagFG, data.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            var strWhere = "";
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.Clear();

                strWhere += " ( ";

                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(data.ProductCodeUser) + "%", "like");
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(data.ProductName) + "%", "like", "or");

                strWhere += sbSql.ToString();

                strWhere += " ) ";

            }
            if (!CUtils.IsNullOrEmpty(strWhere))
            {
                if (!CUtils.IsNullOrEmpty(strWhereClause))
                {
                    strWhereClause += " and ";
                }
                strWhereClause += strWhere;
            }
            return strWhereClause;
        }
        #endregion

        #region ["Thong tin hang hoa"]

        #region ["Attribute"]
        [HttpPost]
        public ActionResult Mst_Attribute_Create(string attributename)
        {
            
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Attribute = "*",
            };
            if (attributename != null && attributename.Length > 0)
            {
                objRQ_Mst_Attribute.Mst_Attribute = new Mst_Attribute
                {
                    AttributeCode = WA_Seq_GenCommonCode_Get("ATTRIBUTECODE"),
                    AttributeName = attributename
                }; 
            }
            var json1 = new JavaScriptSerializer().Serialize(objRQ_Mst_Attribute);
            Mst_AttributeService.Instance.WA_Mst_Attribute_Create(objRQ_Mst_Attribute, UserState.AddressAPIs_Product_Customer_Centrer);
            return Json(new { Success = true, Data = objRQ_Mst_Attribute.Mst_Attribute });
        }

        // Update
        public void Mst_Attribute_Update(string attributecode, string attributename)
        {
            
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
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
                Ft_Cols_Upd = "Mst_Attribute.AttributeName",
                // RQ_Mst_Product
                Rt_Cols_Mst_Attribute = "*",
            };
            if (attributename != null && attributename.Length > 0)
            {
                objRQ_Mst_Attribute.Mst_Attribute = new Mst_Attribute
                {
                    AttributeCode = attributecode,
                    AttributeName = attributename,
                    FlagActive = 1
                }; ;
            }
            Mst_AttributeService.Instance.WA_Mst_Attribute_Update(objRQ_Mst_Attribute, UserState.AddressAPIs_Product_Customer_Centrer);
        }
        #endregion

        #region ["Add Brand"]
        [HttpPost]
        public void Mst_Brand_Create(string brandname)
        {
            var objRQ_Mst_Brand = new RQ_Mst_Brand()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
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
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Brand = "*",
            };
            if (brandname != null && brandname.Length > 0)
            {
                objRQ_Mst_Brand.Mst_Brand = new Mst_Brand
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    BrandCode = WA_Seq_GenCommonCode_Get("BRANDCODE"),
                    BrandName = brandname,
                }; ;
            }
            Mst_BrandService.Instance.WA_Mst_Brand_Create(objRQ_Mst_Brand, UserState.AddressAPIs_Product_Customer_Centrer);
        }
        #endregion

        #region ["Add Product Group"]
        public void Mst_ProductGrp_Create(string productGrpName, string productgrpcodeparent)
        {
            
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
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
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
            };
            if (productGrpName != null && productGrpName.Length > 0)
            {
                objRQ_Mst_ProductGroup.Mst_ProductGroup = new Mst_ProductGroup
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    ProductGrpCodeParent = productgrpcodeparent,
                    ProductGrpCode = WA_Seq_GenCommonCode_Get("PRODUCTGRPCODE"),
                    ProductGrpName = productGrpName
                };
                if (CUtils.IsNullOrEmpty(objRQ_Mst_ProductGroup.Mst_ProductGroup.ProductGrpCodeParent))
                {
                    objRQ_Mst_ProductGroup.Mst_ProductGroup.ProductGrpCodeParent = "ALL";
                }
            }
            Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Create(objRQ_Mst_ProductGroup, UserState.AddressAPIs_Product_Customer_Centrer);
        }
        #endregion


        #endregion

        #region["Xóa hàng hóa"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string listproduct)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            
            try
            {
                var objListMst_ProductInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_Product>>(listproduct);
                var objRQ_Mst_Product = new RQ_Mst_Product()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = null,
                    Rt_Cols_Mst_ProductImages = null,
                    Rt_Cols_Mst_ProductFiles = null,
                    Rt_Cols_Prd_BOM = null,
                    Rt_Cols_Prd_Attribute = null,
                    Lst_Mst_Product = objListMst_ProductInput,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var addressAPIs = UserState.AddressAPIs;
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Delete(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                // Lưu tại network
                objRQ_Mst_Product.GwUserCode = GwUserCode;
                objRQ_Mst_Product.GwPassword = GwPassword;
                objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_Mst_Product.FlagIsDelete = "1";
                Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa hàng hóa thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Ngừng kinh doanh hàng hóa"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StopProducts(string listproduct)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            
            try
            {
                var objListMst_ProductInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_Product>>(listproduct);

                #region["Hàng hóa"]
                var strWhereClauseMst_Product = strWhereSearchProductByListProduct(objListMst_ProductInput);
                var objRQ_Mst_ProductCur = new RQ_Mst_Product()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClauseMst_Product,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "*",
                    Rt_Cols_Mst_ProductFiles = "*",
                    Rt_Cols_Prd_BOM = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var obj_RT_Mst_Product = RT_Mst_Product_Get(objRQ_Mst_ProductCur);
                #endregion

                if (obj_RT_Mst_Product != null)
                {
                    if (obj_RT_Mst_Product.Lst_Mst_Product != null && obj_RT_Mst_Product.Lst_Mst_Product.Count > 0)
                    {
                        foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                        {
                            item.FlagActive = "0";
                        }
                    }

                    var objRQ_Mst_Product = new RQ_Mst_Product()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = "Mst_Product.FlagActive",
                        // RQ_Mst_Product
                        Rt_Cols_Mst_Product = null,
                        Rt_Cols_Mst_ProductImages = null,
                        Rt_Cols_Mst_ProductFiles = null,
                        Rt_Cols_Prd_BOM = null,
                        Rt_Cols_Prd_Attribute = null,
                        Lst_Mst_Product = obj_RT_Mst_Product.Lst_Mst_Product,
                        Lst_Mst_ProductImages = obj_RT_Mst_Product.Lst_Mst_ProductImages,
                        Lst_Mst_ProductFiles = obj_RT_Mst_Product.Lst_Mst_ProductFiles,
                        Lst_Prd_BOM = obj_RT_Mst_Product.Lst_Prd_BOM,
                        Lst_Prd_Attribute = obj_RT_Mst_Product.Lst_Prd_Attribute,
                    };
                    var addressAPIs = UserState.AddressAPIs;
                    var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Update(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                    // Lưu tại network
                    objRQ_Mst_Product.GwUserCode = GwUserCode;
                    objRQ_Mst_Product.GwPassword = GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.FlagIsDelete = "0";
                    Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Ngừng kinh doanh hàng hóa thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActiveProducts(string listproduct)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var objListMst_ProductInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_Product>>(listproduct);

                #region["Hàng hóa"]
                var strWhereClauseMst_Product = strWhereSearchProductByListProduct(objListMst_ProductInput);
                var objRQ_Mst_ProductCur = new RQ_Mst_Product()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClauseMst_Product,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Product = "*",
                    Rt_Cols_Mst_ProductImages = "*",
                    Rt_Cols_Mst_ProductFiles = "*",
                    Rt_Cols_Prd_BOM = "*",
                    Rt_Cols_Prd_Attribute = "*",
                    Lst_Mst_Product = null,
                    Lst_Mst_ProductImages = null,
                    Lst_Mst_ProductFiles = null,
                    Lst_Prd_BOM = null,
                    Lst_Prd_Attribute = null,
                };
                var obj_RT_Mst_Product = RT_Mst_Product_Get(objRQ_Mst_ProductCur);
                #endregion

                if (obj_RT_Mst_Product != null)
                {
                    if (obj_RT_Mst_Product.Lst_Mst_Product != null && obj_RT_Mst_Product.Lst_Mst_Product.Count > 0)
                    {
                        foreach (var item in obj_RT_Mst_Product.Lst_Mst_Product)
                        {
                            item.FlagActive = "1";
                        }
                    }

                    var objRQ_Mst_Product = new RQ_Mst_Product()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = "Mst_Product.FlagActive",
                        // RQ_Mst_Product
                        Rt_Cols_Mst_Product = null,
                        Rt_Cols_Mst_ProductImages = null,
                        Rt_Cols_Mst_ProductFiles = null,
                        Rt_Cols_Prd_BOM = null,
                        Rt_Cols_Prd_Attribute = null,
                        Lst_Mst_Product = obj_RT_Mst_Product.Lst_Mst_Product,
                        Lst_Mst_ProductImages = obj_RT_Mst_Product.Lst_Mst_ProductImages,
                        Lst_Mst_ProductFiles = obj_RT_Mst_Product.Lst_Mst_ProductFiles,
                        Lst_Prd_BOM = obj_RT_Mst_Product.Lst_Prd_BOM,
                        Lst_Prd_Attribute = obj_RT_Mst_Product.Lst_Prd_Attribute,
                    };
                    var addressAPIs = UserState.AddressAPIs;
                    var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Update(objRQ_Mst_Product, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                    // Lưu tại network
                    objRQ_Mst_Product.GwUserCode = GwUserCode;
                    objRQ_Mst_Product.GwPassword = GwPassword;
                    objRQ_Mst_Product.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_Mst_Product.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_Mst_Product.FlagIsDelete = "0";
                    Mst_ProductService.Instance.WA_Mst_Product_Save(objRQ_Mst_Product, UserState.AddressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Kinh doanh hàng hóa thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Tạo mới, cập nhật thông tin Prd_DynamicField"]
        [HttpPost]
        public ActionResult Prd_DynamicField_Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var objParamType = Newtonsoft.Json.JsonConvert.DeserializeObject<PrdDynamicFieldParamType>(model);
                var listPrd_DynamicField = new List<Prd_DynamicField>();

                #region["Thông tin bổ sung"]
                var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
                {
                    // WARQBase
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
                    Ft_Cols_Upd = null,
                    // RQ_Prd_DynamicField
                    Rt_Cols_Prd_DynamicField = "*",
                    Prd_DynamicField = null,
                };
                var list_Prd_DynamicField_Async = List_Prd_DynamicField_Async(objRQ_Prd_DynamicField);
                #endregion

                if (list_Prd_DynamicField_Async != null && list_Prd_DynamicField_Async.Result != null &&
                list_Prd_DynamicField_Async.Result.Count > 0)
                {
                    listPrd_DynamicField.AddRange(list_Prd_DynamicField_Async.Result);
                }

                if (listPrd_DynamicField != null && listPrd_DynamicField.Count > 0)
                {
                    if (listPrd_DynamicField[0].ParamTypes != null && listPrd_DynamicField[0].ParamTypes.Count > 0)
                    {
                        var paramTypes = new List<PrdDynamicFieldParamType>();
                        paramTypes.AddRange(listPrd_DynamicField[0].ParamTypes);
                        paramTypes.Add(objParamType);

                        if (listPrd_DynamicField[0].ParamTypes.Where(x => x.Code.Equals(objParamType.Code)).ToList().Count == 0)
                        {
                            #region["Xóa DynamicField cũ"]
                            var dejson_paramTypes = Newtonsoft.Json.JsonConvert.SerializeObject(paramTypes);
                            var prd_DynamicFieldDel = new Prd_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };
                            var objRQ_Prd_DynamicFieldDel = new RQ_Prd_DynamicField()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                // RQ_Prd_DynamicField
                                Prd_DynamicField = prd_DynamicFieldDel,
                            };
                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Delete(objRQ_Prd_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion

                            #region["Tạo DynamicField mới có add thêm objParamType"]
                            var prd_DynamicFieldCreate = new Prd_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                Detail = dejson_paramTypes,
                                //ParamTypes = paramTypes
                            };
                            var objRQ_Prd_DynamicFieldCreate = new RQ_Prd_DynamicField()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                // RQ_Prd_DynamicField
                                Prd_DynamicField = prd_DynamicFieldCreate,
                            };
                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Create(objRQ_Prd_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion
                            resultEntry.Success = true;
                        }
                        else
                        {
                            resultEntry.Success = false;
                            resultEntry.AddMessage("Mã thông tin đã tồn tại!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Prd_DynamicField_Update(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var objParamType = Newtonsoft.Json.JsonConvert.DeserializeObject<PrdDynamicFieldParamType>(model);
                var listPrd_DynamicField = new List<Prd_DynamicField>();

                #region["Thông tin bổ sung"]
                var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
                {
                    // WARQBase
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
                    Ft_Cols_Upd = null,
                    // RQ_Prd_DynamicField
                    Rt_Cols_Prd_DynamicField = "*",
                    Prd_DynamicField = null,
                };
                var list_Prd_DynamicField_Async = List_Prd_DynamicField_Async(objRQ_Prd_DynamicField);
                #endregion

                if (list_Prd_DynamicField_Async != null && list_Prd_DynamicField_Async.Result != null &&
                list_Prd_DynamicField_Async.Result.Count > 0)
                {
                    listPrd_DynamicField.AddRange(list_Prd_DynamicField_Async.Result);
                }

                if (listPrd_DynamicField != null && listPrd_DynamicField.Count > 0)
                {
                    if (listPrd_DynamicField[0].ParamTypes != null && listPrd_DynamicField[0].ParamTypes.Count > 0)
                    {
                        var paramTypes = new List<PrdDynamicFieldParamType>();
                        foreach (var item in listPrd_DynamicField[0].ParamTypes)
                        {
                            if (!item.Code.Equals(objParamType.Code))
                            {
                                paramTypes.Add(item);
                            }
                            else
                            {
                                paramTypes.Add(objParamType);
                            }
                        }

                        if (listPrd_DynamicField[0].ParamTypes.Where(x => x.Code.Equals(objParamType.Code)).ToList().Count > 0)
                        {
                            #region["Xóa DynamicField cũ"]
                            var prd_DynamicFieldDel = new Prd_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };
                            var objRQ_Prd_DynamicFieldDel = new RQ_Prd_DynamicField()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                // RQ_Prd_DynamicField
                                Prd_DynamicField = prd_DynamicFieldDel,
                            };
                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Delete(objRQ_Prd_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion
                            var dejson_paramTypes = Newtonsoft.Json.JsonConvert.SerializeObject(paramTypes);
                            #region["Tạo DynamicField mới có add thêm objParamType"]
                            var prd_DynamicFieldCreate = new Prd_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                Detail = dejson_paramTypes,
                                //ParamTypes = paramTypes
                            };
                            var objRQ_Prd_DynamicFieldCreate = new RQ_Prd_DynamicField()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                // RQ_Prd_DynamicField
                                Prd_DynamicField = prd_DynamicFieldCreate,
                            };
                            Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Create(objRQ_Prd_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion
                            resultEntry.Success = true;
                        }
                        else
                        {
                            resultEntry.Success = false;
                            resultEntry.AddMessage("Mã thông tin đã tồn tại!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }
        #endregion
    }
}