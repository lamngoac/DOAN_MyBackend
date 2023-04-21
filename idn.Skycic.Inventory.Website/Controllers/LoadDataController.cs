using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using inos.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class LoadDataController : AdminBaseController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_District(string provincecode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_District = new List<Mst_District>();

                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_District = "";
                    strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { ProvinceCode = provincecode, FlagActive = Client_Flag.Active });

                    var objRQ_Mst_District = new RQ_Mst_District()
                    {
                        // WARQBase
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
                        Ft_WhereClause = strWhereClauseMst_District,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, CUtils.StrValue(UserState.AddressAPIs));
                    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                    {
                        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                    }
                }
                return JsonView("LoadMst_District", listMst_District);
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
            return JsonViewError("LoadMst_District", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_Ward(string provincecode, string districtcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_Ward = new List<Mst_Ward>();

                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_Ward = "";
                    strWhereClauseMst_Ward = strWhereClause_Mst_Ward(new Mst_Ward() { ProvinceCode = provincecode, DistrictCode = districtcode, FlagActive = Client_Flag.Active });

                    var objRQ_Mst_Ward = new RQ_Mst_Ward()
                    {
                        // WARQBase
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
                        Ft_WhereClause = strWhereClauseMst_Ward,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Ward
                        Rt_Cols_Mst_Ward = "*",
                        Mst_Ward = null,
                    };
                    var objRT_Mst_Ward = Mst_WardService.Instance.WA_Mst_Ward_Get(objRQ_Mst_Ward, CUtils.StrValue(UserState.AddressAPIs));
                    if (objRT_Mst_Ward.Lst_Mst_Ward != null && objRT_Mst_Ward.Lst_Mst_Ward.Count > 0)
                    {
                        listMst_Ward.AddRange(objRT_Mst_Ward.Lst_Mst_Ward);
                    }
                }
                return JsonView("LoadMst_Ward", listMst_Ward);
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
            return JsonViewError("LoadMst_Ward", null, resultEntry);
        }

        public static string GetInfoSolutionCloud(string solutioncode, ref string solutionname, ref string solutionimageslidebar, ref string solutionimagenavbar)
        {
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.HDDT) || solutioncode.ToUpper().Equals(SolutionCodeCloud.QINVOICE))
            {
                solutionimageslidebar = SolutionImageSlidebar.HDDT;
                solutionimagenavbar = SolutionImageNavbar.HDDT;
                solutionname = SolutionNameCloud.HDDT;
            }
            //if (solutioncode.ToUpper().Equals(SolutionCodeCloud.QINVOICE))
            //{
            //    solutionimageslidebar = SolutionImageSlidebar.QINVOICE;
            //    solutionimagenavbar = SolutionImageNavbar.QINVOICE;
            //    solutionname = SolutionNameCloud.QINVOICE;
            //}
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.DMSPLUS))
            {
                solutionimageslidebar = SolutionImageSlidebar.DMSPLUS;
                solutionimagenavbar = SolutionImageNavbar.DMSPLUS;
                solutionname = SolutionNameCloud.DMSPLUS;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.INBRAND))
            {
                solutionimageslidebar = SolutionImageSlidebar.INBRAND;
                solutionimagenavbar = SolutionImageNavbar.INBRAND;
                solutionname = SolutionNameCloud.INBRAND;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.QCONTRACT))
            {
                solutionimageslidebar = SolutionImageSlidebar.QCONTRACT;
                solutionimagenavbar = SolutionImageNavbar.QCONTRACT;
                solutionname = SolutionNameCloud.QCONTRACT;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.INVENTORY))
            {
                solutionimageslidebar = SolutionImageSlidebar.INVENTORY;
                solutionimagenavbar = SolutionImageNavbar.INVENTORY;
                solutionname = SolutionNameCloud.INVENTORY;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.SKYCIC))
            {
                solutionimageslidebar = SolutionImageSlidebar.SKYCIC;
                solutionimagenavbar = SolutionImageNavbar.SKYCIC;
                solutionname = SolutionNameCloud.SKYCIC;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.PRODUCTCENTER))
            {
                solutionimageslidebar = SolutionImageSlidebar.PRODUCTCENTER;
                solutionimagenavbar = SolutionImageNavbar.PRODUCTCENTER;
                solutionname = SolutionNameCloud.PRODUCTCENTER;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSTOMERCENTER))
            {
                solutionimageslidebar = SolutionImageSlidebar.CUSTOMERCENTER;
                solutionimagenavbar = SolutionImageNavbar.CUSTOMERCENTER;
                solutionname = SolutionNameCloud.CUSTOMERCENTER;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.PRDCENTER))
            {
                solutionimageslidebar = SolutionImageSlidebar.PRDCENTER;
                solutionimagenavbar = SolutionImageNavbar.PRDCENTER;
                solutionname = SolutionNameCloud.PRDCENTER;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSCENTER))
            {
                solutionimageslidebar = SolutionImageSlidebar.CUSCENTER;
                solutionimagenavbar = SolutionImageNavbar.CUSCENTER;
                solutionname = SolutionNameCloud.CUSCENTER;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSCENTER))
            {
                solutionimageslidebar = SolutionImageSlidebar.CUSCENTER;
                solutionimagenavbar = SolutionImageNavbar.CUSCENTER;
                solutionname = SolutionNameCloud.CUSCENTER;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.ICIC))
            {
                solutionimageslidebar = SolutionImageSlidebar.ICIC;
                solutionimagenavbar = SolutionImageNavbar.ICIC;
                solutionname = SolutionNameCloud.ICIC;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.VELOCA))
            {
                solutionimageslidebar = SolutionImageSlidebar.VELOCA;
                solutionimagenavbar = SolutionImageNavbar.VELOCA;
                solutionname = SolutionNameCloud.VELOCA;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.TRACEINFO))
            {
                solutionimageslidebar = SolutionImageSlidebar.TRACEINFO;
                solutionimagenavbar = SolutionImageNavbar.TRACEINFO;
                solutionname = SolutionNameCloud.TRACEINFO;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.DASHBOARD))
            {
                solutionimageslidebar = SolutionImageSlidebar.DASHBOARD;
                solutionimagenavbar = SolutionImageNavbar.DASHBOARD;
                solutionname = SolutionNameCloud.DASHBOARD;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.HTKK))
            {
                solutionimageslidebar = SolutionImageSlidebar.HTKK;
                solutionimagenavbar = SolutionImageNavbar.HTKK;
                solutionname = SolutionNameCloud.HTKK;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.REPORTSERVICE))
            {
                solutionimageslidebar = SolutionImageSlidebar.REPORTSERVICE;
                solutionimagenavbar = SolutionImageNavbar.REPORTSERVICE;
                solutionname = SolutionNameCloud.REPORTSERVICE;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.QSERVICE))
            {
                solutionimageslidebar = SolutionImageSlidebar.QSERVICE;
                solutionimagenavbar = SolutionImageNavbar.QSERVICE;
                solutionname = SolutionNameCloud.QSERVICE;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.LOGISTIC))
            {
                solutionimageslidebar = SolutionImageSlidebar.LOGISTIC;
                solutionimagenavbar = SolutionImageNavbar.LOGISTIC;
                solutionname = SolutionNameCloud.LOGISTIC;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.XNK))
            {
                solutionimageslidebar = SolutionImageSlidebar.XNK;
                solutionimagenavbar = SolutionImageNavbar.XNK;
                solutionname = SolutionNameCloud.XNK;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.LQDMS))
            {
                solutionimageslidebar = SolutionImageSlidebar.LQDMS;
                solutionimagenavbar = SolutionImageNavbar.LQDMS;
                solutionname = SolutionNameCloud.LQDMS;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.ETEMNN))
            {
                solutionimageslidebar = SolutionImageSlidebar.ETEMNN;
                solutionimagenavbar = SolutionImageNavbar.ETEMNN;
                solutionname = SolutionNameCloud.ETEMNN;
            }
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.FINAS))
            {
                solutionimageslidebar = SolutionImageSlidebar.FINAS;
                solutionimagenavbar = SolutionImageNavbar.FINAS;
                solutionname = SolutionNameCloud.FINAS;
            }
            return solutioncode;
        }

        public static bool CheckSolutionCodeNotShow(string solutioncode)
        {
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.PRODUCTCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSTOMERCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.PRDCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.REPORTSERVICE)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.DASHBOARD)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.TRACEINFO))
            {
                return false;
            }
            return true;
        }

        public static bool CheckSolutionCodeAllowUse(string solutioncode)
        {
            if (solutioncode.ToUpper().Equals(SolutionCodeCloud.HDDT)
                //|| solutioncode.ToUpper().Equals(SolutionCodeCloud.QINVOICE)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.INBRAND)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.DMSPLUS)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.QCONTRACT)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.INVENTORY)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.SKYCIC)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.PRODUCTCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSTOMERCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.PRDCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.CUSCENTER)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.ICIC)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.VELOCA)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.TRACEINFO)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.DASHBOARD)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.HTKK)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.REPORTSERVICE)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.QSERVICE)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.LOGISTIC)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.LQDMS)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.ETEMNN)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.FINAS)
                || solutioncode.ToUpper().Equals(SolutionCodeCloud.XNK))
            {
                return true;
            }
            return false;
        }

        public static List<OrgSolution> GetSolutionsNotUsed(List<OrgSolution> lstAllOrgSolution, List<OrgSolution> lstOrgSolutionUsed)
        {
            var lstOrgSolutionNotUsed = new List<OrgSolution>();

            foreach (var item in lstAllOrgSolution)
            {
                var check1 = CheckSolutionCodeAllowUse(item.SolutionCode);
                if (check1)
                {
                    var check = CheckSolutionCodeNotShow(item.SolutionCode);
                    if (check)
                    {
                        var check3 = lstOrgSolutionUsed.Where(m => m.SolutionCode.Equals(item.SolutionCode)).ToList();
                        if (check3.Count == 0)
                        {
                            //2021-05-20: Ẩn ở khối "More": Tức là get license có thì vẫn show, ko có thì ko show.
                            if (!item.SolutionCode.Equals(SolutionCodeCloud.LQDMS))
                            {
                                var solutionimagenavbar = "/Images/no-image.png";
                                var solutionimageslidebar = "/Images/no-image.png";
                                var solutionname = "not defined";
                                GetInfoSolutionCloud(item.SolutionCode, ref solutionname, ref solutionimageslidebar, ref solutionimagenavbar);
                                item.Name = solutionname;

                                lstOrgSolutionNotUsed.Add(item);
                            }
                        }
                    }
                }
            }

            return lstOrgSolutionNotUsed;
        }

        #region["Tìm kiếm hàng hóa - Autocomplete"]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchProduct(string prdid = "", string flagquetmavach = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var waUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValue(UserState.NetworkID);
            var orgID = CUtils.StrValue(UserState.OrgID);
            var utcOffset = CUtils.StrValue(UserState.UtcOffset);
            var listMst_Product = new List<Mst_Product>();
            if (CUtils.IsNullOrEmpty(flagquetmavach))
            {
                flagquetmavach = "";
            }
            try
            {
                var objMst_Product = new Mst_Product()
                {
                    ProductCodeUser = prdid,
                    ProductName = prdid,
                    OrgID = orgID
                };
                var strWhere_SearchProductProductId = "";
                if (flagquetmavach.Equals("1"))
                {
                    // Truy vấn cho trường hợp quét mã vạch
                    strWhere_SearchProductProductId = strWhereSearchProductProductId_QuetMaVach(objMst_Product);
                }
                else
                {
                    strWhere_SearchProductProductId = strWhereSearchProductProductId(objMst_Product);
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
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = "10", // Hỗ trợ lấy ra 10 bản ghi
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
                listMst_Product.AddRange(objRT_Mst_InvoiceType);
                return Json(new { Success = true, ListMst_Product = listMst_Product });
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
        [ValidateAntiForgeryToken]
        public ActionResult GetProductExactly(string invcode, string prdid = "")
        {
            var waUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var utcOffset = CUtils.StrValue(UserState.UtcOffset);
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var resultEntry = new JsonResultEntry() { Success = false };
            var objMst_Product = new Mst_Product(); // Hàng hóa người dùng chọn
            List<Mst_ProductUI> listProductUI = new List<Mst_ProductUI>(); // Danh sách hàng hóa cùng base với hàng hóa được chọn (Hàng hóa người dùng chọn mặc định ở vị trí đầu tiên)
            try
            {
                var objMst_ProductSearch = new Mst_Product()
                {
                    ProductCode = prdid
                };
                var strWhere_GetProductId = strWhereGetProductId(objMst_ProductSearch);
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
                var listMst_Product = WA_Mst_Product_Get(objRQ_Mst_Product);

                if(listMst_Product != null && listMst_Product.Count > 0)
                {
                    objMst_Product = listMst_Product[0];
                    var listProduct = new List<Mst_Product>();
                    listProduct.Add(objMst_Product);
                    #region["Danh sách hàng hóa cùng base"]
                    var strWhereClause_MstProductBase = strWhereClause_Mst_Product_Get_Base(objMst_Product);
                    var objRT_Mst_ProductBase = GetPrdBase(strWhereClause_MstProductBase);
                    if(objRT_Mst_ProductBase.Lst_Mst_Product != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
                    {
                        listProduct.AddRange(objRT_Mst_ProductBase.Lst_Mst_Product);
                    }

                    #endregion
                    #region ["Thông tin tồn kho - Tìm VT tồn lớn nhất"]

                    List<Rpt_Inv_InvBalance_LastUpdInvByProduct> listBalance = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();

                    List<Rpt_Inv_InvBalance_LastUpdInvByProduct> lstInvBalance_LastUpdInvByProduct = new List<Rpt_Inv_InvBalance_LastUpdInvByProduct>();
                    foreach (var it in listProduct)
                    {
                        if (!CUtils.StrValue(it.FlagLot).Equals("1") && !CUtils.StrValue(it.FlagSerial).Equals("1"))
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
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Inv_InventoryBalance);

                        var objRT_Inv_InventoryBalance = ReportsService.Instance.WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(objRQ_Inv_InventoryBalance, addressAPIs);

                        if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct != null && objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct.Count != 0)
                        {
                            listBalance = objRT_Inv_InventoryBalance.Lst_Rpt_Inv_InvBalance_LastUpdInvByProduct;
                        }
                    }
                    #endregion
                    #region ["Khởi tạo list ProductUI"]

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
                }

                return Json(new { Success = true, Data = listProductUI });
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


        #region[""]
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Ward(Mst_Ward data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Ward = TableName.Mst_Ward + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.DistrictCode, CUtils.StrValue(data.DistrictCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.FlagActive, CUtils.StrValue(data.FlagActive), "=");
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
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(data.ProductCodeUser) + "%", "like");
                strWhereClause += " and (" + Tbl_Mst_Product + TblMst_Product.ProductCodeUser + " like '%" + CUtils.StrValue(data.ProductCodeUser) + "%'";
            }

            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(data.ProductName) + "%", "like");
                strWhereClause += " or " + "Mst_Product.ProductName like '%" + data.ProductName + "%')";
            }
            return strWhereClause;
        }

        private string strWhereSearchProductProductId_QuetMaVach(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValue(data.ProductCodeUser), " = ");
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
            //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(data.ProductCode), "=");
            }

            strWhereClause = sbSql.ToString();
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
        private string strWhereClause_Mst_Product(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(data.ProductCodeUser))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(data.ProductCodeUser) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(data.ProductCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, CUtils.StrValue(data.ProductCodeBase), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(data.ProductName) + "%", "like");
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
            //if (!CUtils.IsNullOrEmpty(strWhere))
            //{
            //    if (!CUtils.IsNullOrEmpty(strWhereClause))
            //    {
            //        strWhereClause += " and ";
            //    }
            //    strWhereClause += strWhere;
            //}
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

        private string strWhereClause_Mst_Product_By_ProductCodeBase_In(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            if (!CUtils.IsNullOrEmpty(data.ProductCodeBase))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeBase, CUtils.StrValue(data.ProductCodeBase), "in");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Product_Search_By_ProductGroup(Mst_Product data, List<Mst_ProductGroup> listdatagroup)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();

            #region["Nhóm sản phẩm"]
            var productGroup = "";
            if (listdatagroup != null && listdatagroup.Count > 0)
            {
                foreach (var item in listdatagroup)
                {
                    if (!CUtils.IsNullOrEmpty(item.ProductGrpCode))
                    {
                        productGroup = !CUtils.IsNullOrEmpty(productGroup) ? string.Format("{0}," + "'" + item.ProductGrpCode + "'", productGroup) : string.Format("'" + item.ProductGrpCode + "'", productGroup);
                    }
                }
            }

            if (!string.IsNullOrEmpty(productGroup))
            {
                if (strWhereClause.Length > 0)
                {
                    strWhereClause = strWhereClause + " and " + Tbl_Mst_Product + TblMst_Product.ProductGrpCode + " in " + productGroup;
                }
                else
                {
                    strWhereClause = TblMst_Product.ProductGrpCode + " in " + productGroup;
                }
            }
            #endregion

            return strWhereClause;
        }
        #endregion
    }
}