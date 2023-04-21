using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
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
    public class Mst_ProductGroupSubController : AdminBaseController
    {
        // GET: Mst_ProductGroupSub
        public ActionResult Index(
            string init = "init",
            int page = 0,
            int recordcount = 10,
            string productgrpcode = "",
            string productgrpname = "",
            string fromdate = "",
            string todate = "")
        {
            GetUrlProductSolution();
            // Lấy link url ProductCenter để cập nhật nếu cần

            var itemCount = 0;
            var pageCount = 0;
            var addressAPIs = UserState.AddressAPIs;

            #region["Nhóm sản phẩm cha"]
            var objRQ_Mst_ProductGroupParent = new RQ_Mst_ProductGroup()
            {
                // WARQBase
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
                Ft_WhereClause = "Mst_ProductGroup.FlagActive = '1' and Mst_ProductGroup.FlagFG = '0'",
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            var listMst_ProductGroupParent = WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroupParent);
            ViewBag.ListMstProductGroupParent = listMst_ProductGroupParent;
            #endregion

            var ProductGrpCodeCreate = WA_Seq_GenCommonCode_Get(SeqType.ProductGrpCode);


            var strWhereClause_Mst_ProductGroup = strWhereClauseMstProductGroup(new Mst_ProductGroupUI()
            {
                //FlagActive = "1",
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                //ProductGrpCode = productgrpcode,
                ProductGrpName = productgrpcode,
                ProductGrpDesc = productgrpname,
                CreateDateFrom = fromdate,
                CreateDateTo = todate,
                FlagFG = "0"
            });
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
                Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                Ft_RecordCount = recordcount.ToString(),
                Ft_WhereClause = strWhereClause_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);

            var pageInfo = new PageInfo<Mst_ProductGroup>(0, recordcount)
            {
                DataList = new List<Mst_ProductGroup>()
            };
            var lstMst_ProductGroup = new List<Mst_ProductGroup>();
            if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count != 0)
            {
                lstMst_ProductGroup = objRT_Mst_ProductGroup.Lst_Mst_ProductGroup;
            }
            pageInfo.DataList = lstMst_ProductGroup;
            itemCount = objRT_Mst_ProductGroup.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_ProductGroup.MySummaryTable.MyCount);
            pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            ViewBag.RecordCount = recordcount;
            ViewBag.Offset = 7;
            ViewBag.ProductGrpCode = productgrpcode;
            ViewBag.ProductGrpName = productgrpname;
            ViewBag.CreateDateFrom = fromdate;
            ViewBag.CreateDateTo = todate;
            ViewBag.ProductGrpCodeCreate = ProductGrpCodeCreate;
            ViewBag.PageCur = page;
            if (init == "search")
            {
                return JsonView("BindDataMst_ProductGroup", pageInfo);
            }
            else
            {
                return View(pageInfo);
            }
        }

        private string strWhereClauseMst_Brand(Mst_Brand data)
        {
            var strWhereClause = "";
            var sbSql = new System.Text.StringBuilder();
            var Tbl_Mst_Brand = "Mst_Brand.";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "FlagActive", data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "OrgID", data.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "BrandCode", data.BrandCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "BrandName", data.BrandName, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        public ActionResult Save(string ProductGrpName, string ProductGrpDesc, string ProductGrpCodeParent, string ProductGrpCode, string FlagActive)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            try
            {
                var objMst_ProductGroup = new Mst_ProductGroup()
                {
                    ProductGrpCode = ProductGrpCode,
                    ProductGrpName = ProductGrpName,
                    ProductGrpDesc = ProductGrpDesc,
                    ProductGrpCodeParent = ProductGrpCodeParent,
                    FlagActive = FlagActive,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    FlagFG = Flag.Inactive
                };

                var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                    GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_Cols_Upd = null,
                    Mst_ProductGroup = objMst_ProductGroup
                };
                Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Create(objRQ_Mst_ProductGroup, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo nhóm hàng thành công!");
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

        public ActionResult Update(string ProductGrpName, string ProductGrpDesc, string ProductGrpCodeParent, string ProductGrpCode, string FlagActive)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            try
            {
                var objMst_ProductGroup = new Mst_ProductGroup()
                {
                    ProductGrpCode = ProductGrpCode,
                    ProductGrpName = ProductGrpName,
                    ProductGrpDesc = ProductGrpDesc,
                    ProductGrpCodeParent = ProductGrpCodeParent,
                    FlagActive = FlagActive,
                    /*ProductGrpCodeParent = "ALL",*///ProductGrpCodeParent,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                };

                var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                    GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_Cols_Upd = "Mst_ProductGroup.FlagActive, Mst_ProductGroup.ProductGrpName",
                    Mst_ProductGroup = objMst_ProductGroup,
                };


                Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Update(objRQ_Mst_ProductGroup, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật nhóm hàng thành công!");
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

        [HttpPost]
        public ActionResult Detail(string productGrpCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var addressAPIs = UserState.AddressAPIs;
                #region Get Brand
                var strWhereClause_Mst_Brand = strWhereClauseMst_Brand(new Mst_Brand()
                {
                    FlagActive = "1",
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                });
                //var objRQ_Mst_Brand = new RQ_Mst_Brand()
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
                //    FuncType = null,
                //    Ft_RecordStart = Ft_RecordStart,
                //    Ft_RecordCount = Ft_RecordCount,
                //    Ft_WhereClause = strWhereClause_Mst_Brand,
                //    Ft_Cols_Upd = null,
                //    // RQ_Mst_CustomerGroup
                //    Rt_Cols_Mst_Brand = "*",
                //    Mst_Brand = null,
                //};
                //var objRT_Mst_Brand = Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs);
                //ViewBag.lstMstBrand = objRT_Mst_Brand.Lst_Mst_Brand;
                #endregion

                #region["Nhóm sản phẩm cha"]
                var objRQ_Mst_ProductGroupParent = new RQ_Mst_ProductGroup()
                {
                    // WARQBase
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
                    Ft_WhereClause = "Mst_ProductGroup.FlagActive = '1' and Mst_ProductGroup.FlagFG = '1'",
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_ProductGroup = "*",
                    Mst_ProductGroup = null,
                };
                var objRT_Mst_ProductGroupParent = WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroupParent);
                ViewBag.ListMstProductGroupParent = objRT_Mst_ProductGroupParent;
                #endregion

                var objMst_ProductGroup = new Mst_ProductGroup()
                {
                    ProductGrpCode = productGrpCode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                };
                var strWhereClause = strWhereClauseMstProductGroup(objMst_ProductGroup);
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
                    Ft_Cols_Upd = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Rt_Cols_Mst_ProductGroup = "*"
                };
                var rtMst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
                if (rtMst_ProductGroup != null && rtMst_ProductGroup.Lst_Mst_ProductGroup != null && rtMst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
                {
                    objMst_ProductGroup = rtMst_ProductGroup.Lst_Mst_ProductGroup[0];
                }
                return JsonView("ModalUpdate", objMst_ProductGroup);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult DeleteMulti(string lstProductGrpCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            #endregion
            try
            {
                var listProductGrpCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lstProductGrpCode);
                if (listProductGrpCode == null || listProductGrpCode.Count == 0)
                {
                    resultEntry.Success = false;
                    resultEntry.AddMessage("Không tồn tại danh sách cần xóa !");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                foreach (var productGrpCode in listProductGrpCode)
                {
                    var objMst_ProductGroup = new Mst_ProductGroup()
                    {
                        ProductGrpCode = productGrpCode,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                    };
                    var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                        GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_Cols_Upd = null,
                        Mst_ProductGroup = objMst_ProductGroup,
                        FlagIsDelete = "1"
                    };
                    Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Delete(objRQ_Mst_ProductGroup, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa model thành công!");
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
        private string strWhereClauseMstProductGroup(Mst_ProductGroupUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Model = "Mst_ProductGroup.";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "FlagActive", data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "OrgID", data.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "BrandCode", data.BrandCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "ProductGrpCode", data.ProductGrpCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "ProductGrpName", "%" + data.ProductGrpName + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "ProductGrpDesc", "%" + data.ProductGrpDesc + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "FlagFG", data.FlagFG, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CreateDateFrom))
            {
                var createDTimeUTCFrom = CUtils.StrValue(data.CreateDateFrom) + " 00:00:00";
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.LogLUDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.CreateDateTo))
            {
                var createDTimeUTCTo = CUtils.StrValue(data.CreateDateTo) + " 23:59:59";
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.LogLUDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClauseMstProductGroup(Mst_ProductGroup data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Model = "Mst_ProductGroup.";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "FlagActive", data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "OrgID", data.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "BrandCode", data.BrandCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "ProductGrpCode", data.ProductGrpCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductGrpName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "ProductGrpName", data.ProductGrpName, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagFG))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + "FlagFG", data.FlagFG, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Export(
            string init = "init",
            int page = 0,
            int recordcount = 10,
            string productgrpcode = "",
            string productgrpname = "",
            string fromdate = "",
            string todate = ""
            )
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressAPIs = UserState.AddressAPIs;
            var strWhereClause_Mst_ProductGroup = strWhereClauseMstProductGroup(new Mst_ProductGroupUI()
            {
                //FlagActive = "1",
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                //ProductGrpCode = productgrpcode,
                ProductGrpName = productgrpcode,
                ProductGrpDesc = productgrpname,
                CreateDateFrom = fromdate,
                CreateDateTo = todate
            });
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
                Ft_WhereClause = strWhereClause_Mst_ProductGroup,
                Ft_Cols_Upd = null,
                // 
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            try
            {
                var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
                var lstMst_Model = new List<Mst_ProductGroup>();
                if (objRT_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup != null)
                {
                    lstMst_Model = objRT_Mst_ProductGroup.Lst_Mst_ProductGroup;
                }
                if (lstMst_Model != null && lstMst_Model.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColums();
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Brand).Name), ref url);
                    ExcelExport.ExportToExcelFromList(lstMst_Model, dicColNames, filePath, string.Format("Mst_ProductGroup"));
                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
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
            return View(resultEntry);
        }

        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_ProductGroup = new List<Mst_ProductGroup>();
                Dictionary<string, string> dicColNames = GetImportDicColums();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_ProductGroup).Name), ref url);
                ExcelExport.ExportToExcelFromList(listMst_ProductGroup, dicColNames, filePath, string.Format("Mst_ProductGroup"));
                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = ServiceAddress.BaseAPIProductCenter;//CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
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
                    var lstMst_ProductGroup = new List<Mst_ProductGroup>();
                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        #region["Check null"]

                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["BrandCode"] == null || dr["BrandCode"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã nhãn hiệu không được để trống!";
                                resultEntry.AddDetail(exitsData);
                                return Json(resultEntry);
                            }

                            if (dr["ProductGrpName"] == null || dr["ProductGrpName"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã model không được để trống!";
                                resultEntry.AddDetail(exitsData);
                                return Json(resultEntry);
                            }

                            if (dr["ProductGrpDesc"] == null || dr["ProductGrpDesc"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Tên model không được để trống!";
                                resultEntry.AddDetail(exitsData);
                                return Json(resultEntry);
                            }
                        }
                        #endregion

                        var listImport = DataTableCmUtils.ToListof<Mst_ProductGroup>(table);
                        if (listImport != null)
                        {
                            lstMst_ProductGroup = listImport;
                        }

                        foreach (var it in lstMst_ProductGroup)
                        {
                            var ProductGrpCode = WA_Seq_GenCommonCode_Get(SeqType.ProductGrpCode);
                            var objMst_ProductGroup = new Mst_ProductGroup()
                            {
                                ProductGrpCode = ProductGrpCode,
                                BrandCode = it.BrandCode,
                                ProductGrpName = it.ProductGrpName,
                                ProductGrpDesc = it.ProductGrpDesc,
                                FlagActive = FlagActive,
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };

                            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                                GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                FuncType = null,
                                Ft_Cols_Upd = null,
                                Mst_ProductGroup = objMst_ProductGroup
                            };
                            Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Create(objRQ_Mst_ProductGroup, addressAPIs);
                        }
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Import model thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
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

        private Dictionary<string, string> GetExportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductGrpName","Mã nhóm hàng"},
                 {"ProductGrpDesc","Tên nhóm hàng"},
                 {"ProductGrpCodeParent","Mã nhóm hàng cha"},
                 {"FlagActive","Trạng thái"}
            };
        }
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"ProductGrpName","Mã nhóm hàng"},
                 {"ProductGrpDesc","Tên nhóm hàng"},
                 {"ProductGrpCodeParent","Nhóm hàng cha"},
                 {"FlagActive","Trạng thái"}
            };
        }
    }
}