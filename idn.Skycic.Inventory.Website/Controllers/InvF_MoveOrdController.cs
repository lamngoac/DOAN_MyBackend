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
    public class InvF_MoveOrdController : AdminBaseController
    {
        // GET: InvF_MoveOrd
        #region ["Search/Index"]
        public ActionResult Index(int page = 0, int recordcount = 10)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_MOVEORD");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
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
            var pageInfo = new PageInfo<InvF_MoveOrd>(0, recordcount)
            {
                DataList = new List<InvF_MoveOrd>()
            };

            #region ["Kho nhập/xuất"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var TblMst_Inventory = "Mst_Inventory.";
            var sbSql = new StringBuilder();
            sbSql.AddWhereClause(TblMst_Inventory + "FlagActive", "1", "=");
            sbSql.AddWhereClause(TblMst_Inventory + "FlagIn_Out", "1", "=");
            var strWhere_Mst_InventoryInOut = sbSql.ToString();
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
                Ft_WhereClause = strWhere_Mst_InventoryInOut,
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

            return View(pageInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch(string IF_MONo = "", string CreateTimeFrom = "", string CreateTimeTo = "", string InvCodeOut = "", string ApprTimeFrom = "",
            string ApprTimeTo = "", string InvCodeIn = "", string Remark = "", string chkPending = "", string chkApproved = "", string chkCanceled = "", int recordcount = 50, int page = 0
            )
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
            var pageInfo = new PageInfo<InvF_MoveOrd>(0, recordcount)
            {
                DataList = new List<InvF_MoveOrd>()
            };

            var strWhereClause = strWhereClause_InvF_MoveOrd(IF_MONo, CreateTimeFrom, CreateTimeTo, InvCodeOut, ApprTimeFrom,
            ApprTimeTo, InvCodeIn, Remark, chkPending, chkApproved, chkCanceled);
            var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
            {
                // WARQBase
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
                Rt_Cols_InvF_MoveOrd = "*",
                Rt_Cols_InvF_MoveOrdDtl = "*",
                Rt_Cols_InvF_MoveOrdInstLot = "*",
                Rt_Cols_InvF_MoveOrdInstSerial = "*"
            };
            var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);
            if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
            {
                pageInfo.DataList = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd;
                itemCount = objRT_InvF_MoveOrd.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_MoveOrd.MySummaryTable.MyCount);
                pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
            }

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return JsonView("BindDataInvF_MoveOrd", pageInfo);
        }

        #endregion

        #region ["Create"]
        [HttpGet]
        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_MOVEORD_CREATE");
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

            string InvFMoveOrdNo = SeqNo(new Seq_Common() { SequenceType = "IFMONo", Param_Postfix = "", Param_Prefix = "" });
            ViewBag.InvFMoveOrdNo = InvFMoveOrdNo;

            #region ["Kho nhập/xuất"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var List_Mst_InventoryPosition = new List<Mst_Inventory>();
            var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(new Mst_Inventory()
            {
                FlagActive = Client_Flag.Active,
            });
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
                Ft_WhereClause = strWhereClauseMst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryPosition.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                var objRT_Mst_InventoryInOut = objRT_Mst_Inventory.Lst_Mst_Inventory.Where(invIn => invIn.FlagIn_Out.Equals("1")).ToList();
                if (objRT_Mst_InventoryInOut != null && objRT_Mst_InventoryInOut.Count > 0)
                {
                    List_Mst_InventoryInOut.AddRange(objRT_Mst_InventoryInOut);
                }
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            ViewBag.List_Mst_InventoryPosition = List_Mst_InventoryPosition;
            #endregion

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };

            try
            {
                var objRQ_InvF_MoveOrd = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_MoveOrd>(model);
                //truyền thêm Network, OrgID 
                if (objRQ_InvF_MoveOrd.InvF_MoveOrd != null)
                {
                    objRQ_InvF_MoveOrd.InvF_MoveOrd.OrgID = orgId;
                    objRQ_InvF_MoveOrd.InvF_MoveOrd.NetworkID = networkID;
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl)
                    {
                        dtl.OrgID = orgId;
                        dtl.NetworkID = networkID;
                    }
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot)
                    {
                        dtl.NetworkID = networkID;
                    }
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial)
                    {
                        dtl.NetworkID = networkID;
                    }
                }

                objRQ_InvF_MoveOrd.GwUserCode = GwUserCode;
                objRQ_InvF_MoveOrd.GwPassword = GwPassword;
                var x = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_MoveOrd.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_MoveOrd.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_MoveOrd.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_MoveOrd.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_MoveOrd.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_MoveOrd.Ft_RecordStart = Ft_RecordStart;
                objRQ_InvF_MoveOrd.Ft_RecordCount = Ft_RecordCount;
                objRQ_InvF_MoveOrd.FlagIsDelete = "0";
                var j = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_MoveOrd);
                InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Save(objRQ_InvF_MoveOrd, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo điều chuyển kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
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

        #region ["Detail"]
        public ActionResult Detail(string IF_MONo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_MOVEORD_DETAIL");
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

            #region ["Kho nhập/xuất"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var List_Mst_InventoryPosition = new List<Mst_Inventory>();
            var strWhereClause_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";
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
                Ft_WhereClause = strWhereClause_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryPosition.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                var objRT_Mst_InventoryInOut = objRT_Mst_Inventory.Lst_Mst_Inventory.Where(invIn => invIn.FlagIn_Out.Equals("1")).ToList();
                if (objRT_Mst_InventoryInOut != null && objRT_Mst_InventoryInOut.Count > 0)
                {
                    List_Mst_InventoryInOut.AddRange(objRT_Mst_InventoryInOut);
                }
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            ViewBag.List_Mst_InventoryPosition = List_Mst_InventoryPosition;
            #endregion

            var sbSql = new StringBuilder();
            var Tbl_InvF_MoveOrd = "InvF_MoveOrd.";
            if (!CUtils.IsNullOrEmpty(IF_MONo))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.IF_MONo, IF_MONo, "=");
            }
            var strWhere = sbSql.ToString();

            var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
            {
                // WARQBase
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
                Ft_WhereClause = strWhere,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_InvF_MoveOrd = "*",
                Rt_Cols_InvF_MoveOrdDtl = "*",
                Rt_Cols_InvF_MoveOrdInstLot = "*",
                Rt_Cols_InvF_MoveOrdInstSerial = "*"
            };

            var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);

            var Lst_InvF_MoveOrdDtlUI = new List<InvF_MoveOrdDtlUI>();
            var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
            var lstProductCodeBase = new List<string>();//danh sách hàng hóa cùng base

            var objInvF_MoveOrd = new InvF_MoveOrd();

            if (objRT_InvF_MoveOrd != null)
            {
                if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                {
                    Lst_InvF_MoveOrdDtl.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl);
                }
                if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                {
                    objInvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                }
            }

            lstProductCodeBase = Lst_InvF_MoveOrdDtl.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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
                Ft_WhereClause = strWhereSearchProductImport(null, lstProductCodeBase.Distinct().ToList()),
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = "",
                Rt_Cols_Mst_ProductFiles = "",
                Rt_Cols_Prd_BOM = "",
                Rt_Cols_Prd_Attribute = "",
            };

            var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();

            #region ["Thông tin kho"]
            var objMst_InventoryInvF_MoveOrd = new Mst_Inventory();
            objMst_InventoryInvF_MoveOrd = List_Mst_InventoryInOut.Where(x => x.InvCode.Equals(objInvF_MoveOrd.InvCodeOut)).FirstOrDefault();
            #endregion

            #region["Lấy thông tin tồn kho"]
            //danh sách mã hàng hóa base
            var listProductCode = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && it.ProductLevelSys.Equals(ProductLevelSys.RootPrd)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
            if (listProductCode != null && listProductCode.Count > 0)
            {
                listProductCode = listProductCode.Distinct().ToList();
                var strProductCode = string.Join(",", listProductCode);

                var strWhereClause = "";
                var sb_SQL1 = new StringBuilder();
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (!CUtils.IsNullOrEmpty(strProductCode))
                {
                    sb_SQL1.AddWhereClause(tblInv_InventoryBalance + "ProductCode", strProductCode, "IN");
                }
                if (!CUtils.IsNullOrEmpty(objMst_InventoryInvF_MoveOrd.InvBUPattern))
                {
                    sb_SQL1.AddWhereClause("Mst_Inventory.InvBUCode", CUtils.StrValue(objMst_InventoryInvF_MoveOrd.InvBUPattern), "LIKE");
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
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                }
                foreach (var it in listProductBase)
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
                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                    {
                        if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                        {
                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                            {
                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                //sắp xếp giảm dần
                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                {
                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                }
                            }
                        }
                        else
                        {
                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                            {
                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                //sắp xếp giảm dần
                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                {
                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                }
                            }
                        }
                    }
                    lst_Mst_ProductUI.Add(productUI);
                }
            }
            #endregion

            foreach (var dtl in Lst_InvF_MoveOrdDtl)
            {
                var dtlUI = new InvF_MoveOrdDtlUI()
                {
                    ProductCode = dtl.ProductCode,
                    IF_MONo = dtl.IF_MONo,
                    IF_MOStatusDtl = dtl.IF_MOStatusDtl,
                    InvCodeOut = dtl.InvCodeOut,
                    InvCodeIn = dtl.InvCodeIn,
                    NetworkID = dtl.NetworkID,
                    OrgID = dtl.OrgID,
                    Qty = dtl.Qty,
                    UnitCode = dtl.UnitCode,
                    Remark = dtl.Remark,
                    LogLUDTimeUTC = dtl.LogLUDTimeUTC,
                    LogLUBy = dtl.LogLUBy,
                    mp_ProductCode = dtl.mp_ProductCode,
                    mp_ProductCodeUser = dtl.mp_ProductCodeUser,
                    mp_ProductName = dtl.mp_ProductName,
                    mp_ProductNameEN = dtl.mp_ProductNameEN,
                    mp_FlagLot = dtl.mp_FlagLot,
                    mp_FlagSerial = dtl.mp_FlagSerial,
                    Lst_Mst_ProductBase = new List<Mst_ProductUI>(),
                };
                var lstProductBaseItem = lst_Mst_ProductUI.Where(x => x.ProductCodeBase.Equals(dtl.mp_ProductCodeBase)).ToList();
                var prdUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(dtl.mp_ProductCode)).FirstOrDefault();
                if (lstProductBaseItem != null)
                {
                    dtlUI.Lst_Mst_ProductBase.AddRange(lstProductBaseItem);
                }
                if (prdUI != null)
                {
                    dtlUI.QtyTotalOK = Convert.ToDouble(prdUI.QtyTotalOK);
                }
                Lst_InvF_MoveOrdDtlUI.Add(dtlUI);
            }

            ViewBag.Lst_InvF_MoveOrdDtlUI = Lst_InvF_MoveOrdDtlUI;
            ViewBag.Lst_InvF_MoveOrdInstLot = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot;
            ViewBag.Lst_InvF_MoveOrdInstSerial = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial;
            return View(objInvF_MoveOrd);
        }

        #endregion

        #region ["Update"]
        [HttpGet]
        public ActionResult Update(string IF_MONo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_MOVEORD_UPDATE");
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

            #region ["Kho nhập/xuất"]
            var List_Mst_InventoryInOut = new List<Mst_Inventory>();
            var List_Mst_InventoryPosition = new List<Mst_Inventory>();
            var strWhereClause_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";
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
                Ft_WhereClause = strWhereClause_Mst_Inventory,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryPosition.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                var objRT_Mst_InventoryInOut = objRT_Mst_Inventory.Lst_Mst_Inventory.Where(invIn => invIn.FlagIn_Out.Equals("1")).ToList();
                if (objRT_Mst_InventoryInOut != null && objRT_Mst_InventoryInOut.Count > 0)
                {
                    List_Mst_InventoryInOut.AddRange(objRT_Mst_InventoryInOut);
                }
            }
            ViewBag.List_Mst_InventoryInOut = List_Mst_InventoryInOut;
            ViewBag.List_Mst_InventoryPosition = List_Mst_InventoryPosition;
            #endregion

            var sbSql = new StringBuilder();
            var Tbl_InvF_MoveOrd = "InvF_MoveOrd.";
            if (!CUtils.IsNullOrEmpty(IF_MONo))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.IF_MONo, IF_MONo, "=");
            }
            var strWhere = sbSql.ToString();

            var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
            {
                // WARQBase
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
                Ft_WhereClause = strWhere,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_InvF_MoveOrd = "*",
                Rt_Cols_InvF_MoveOrdDtl = "*",
                Rt_Cols_InvF_MoveOrdInstLot = "*",
                Rt_Cols_InvF_MoveOrdInstSerial = "*"
            };

            var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);

            var Lst_InvF_MoveOrdDtlUI = new List<InvF_MoveOrdDtlUI>();
            var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
            var lstProductCodeBase = new List<string>();//danh sách hàng hóa cùng base

            var objInvF_MoveOrd = new InvF_MoveOrd();

            if (objRT_InvF_MoveOrd != null)
            {
                if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                {
                    Lst_InvF_MoveOrdDtl.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl);
                }
                if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                {
                    objInvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                }
            }

            lstProductCodeBase = Lst_InvF_MoveOrdDtl.Select(m => m.mp_ProductCodeBase.ToString()).ToList();

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
                Ft_WhereClause = strWhereSearchProductImport(null, lstProductCodeBase.Distinct().ToList()),
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Product = "*",
                Rt_Cols_Mst_ProductImages = "",
                Rt_Cols_Mst_ProductFiles = "",
                Rt_Cols_Prd_BOM = "",
                Rt_Cols_Prd_Attribute = "",
            };

            var listProductBase = WA_Mst_Product_Get(objRQ_Mst_Product);
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();

            #region ["Thông tin kho"]
            var objMst_InventoryInvF_MoveOrd = new Mst_Inventory();
            objMst_InventoryInvF_MoveOrd = List_Mst_InventoryInOut.Where(x => x.InvCode.Equals(objInvF_MoveOrd.InvCodeOut)).FirstOrDefault();
            #endregion

            #region["Lấy thông tin tồn kho"]
            //danh sách mã hàng hóa base
            var listProductCode = listProductBase.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode) && it.ProductLevelSys.Equals(ProductLevelSys.RootPrd)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
            if (listProductCode != null && listProductCode.Count > 0)
            {
                listProductCode = listProductCode.Distinct().ToList();
                var strProductCode = string.Join(",", listProductCode);

                var strWhereClause = "";
                var sb_SQL1 = new StringBuilder();
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                if (!CUtils.IsNullOrEmpty(strProductCode))
                {
                    sb_SQL1.AddWhereClause(tblInv_InventoryBalance + "ProductCode", strProductCode, "IN");
                }
                if (!CUtils.IsNullOrEmpty(objMst_InventoryInvF_MoveOrd.InvBUPattern))
                {
                    sb_SQL1.AddWhereClause("Mst_Inventory.InvBUCode", CUtils.StrValue(objMst_InventoryInvF_MoveOrd.InvBUPattern), "LIKE");
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
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                }
                foreach (var it in listProductBase)
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
                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                    {
                        if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                        {
                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                            {
                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                //sắp xếp giảm dần
                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                {
                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                }
                            }
                        }
                        else
                        {
                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                            {
                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                //sắp xếp giảm dần
                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                {
                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                }
                            }
                        }
                    }
                    lst_Mst_ProductUI.Add(productUI);
                }
            }
            #endregion

            foreach (var dtl in Lst_InvF_MoveOrdDtl)
            {
                var dtlUI = new InvF_MoveOrdDtlUI()
                {
                    ProductCode = dtl.ProductCode,
                    IF_MONo = dtl.IF_MONo,
                    IF_MOStatusDtl = dtl.IF_MOStatusDtl,
                    InvCodeOut = dtl.InvCodeOut,
                    InvCodeIn = dtl.InvCodeIn,
                    NetworkID = dtl.NetworkID,
                    OrgID = dtl.OrgID,
                    Qty = dtl.Qty,
                    UnitCode = dtl.UnitCode,
                    Remark = dtl.Remark,
                    LogLUDTimeUTC = dtl.LogLUDTimeUTC,
                    LogLUBy = dtl.LogLUBy,
                    mp_ProductCode = dtl.mp_ProductCode,
                    mp_ProductCodeUser = dtl.mp_ProductCodeUser,
                    mp_ProductName = dtl.mp_ProductName,
                    mp_ProductNameEN = dtl.mp_ProductNameEN,
                    mp_FlagLot = dtl.mp_FlagLot,
                    mp_FlagSerial = dtl.mp_FlagSerial,
                    Lst_Mst_ProductBase = new List<Mst_ProductUI>(),
                };
                var lstProductBaseItem = lst_Mst_ProductUI.Where(x => x.ProductCodeBase.Equals(dtl.mp_ProductCodeBase)).ToList();
                var prdUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(dtl.mp_ProductCode)).FirstOrDefault();
                if (lstProductBaseItem != null)
                {
                    dtlUI.Lst_Mst_ProductBase.AddRange(lstProductBaseItem);
                }
                if (prdUI != null)
                {
                    dtlUI.QtyTotalOK = Convert.ToDouble(prdUI.QtyTotalOK);
                }
                Lst_InvF_MoveOrdDtlUI.Add(dtlUI);
            }

            ViewBag.Lst_InvF_MoveOrdDtlUI = Lst_InvF_MoveOrdDtlUI;
            ViewBag.Lst_InvF_MoveOrdInstLot = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot;
            ViewBag.Lst_InvF_MoveOrdInstSerial = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial;
            return View(objInvF_MoveOrd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };

            try
            {
                var objRQ_InvF_MoveOrd = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_MoveOrd>(model);
                if (objRQ_InvF_MoveOrd.InvF_MoveOrd != null)
                {
                    objRQ_InvF_MoveOrd.InvF_MoveOrd.OrgID = orgId;
                    objRQ_InvF_MoveOrd.InvF_MoveOrd.NetworkID = networkID;
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl)
                    {
                        dtl.OrgID = orgId;
                        dtl.NetworkID = networkID;
                    }
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot)
                    {
                        dtl.NetworkID = networkID;
                    }
                }
                if (objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial != null && objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.Count > 0)
                {
                    foreach (var dtl in objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial)
                    {
                        dtl.NetworkID = networkID;
                    }
                }
                var strFt_Cols_Upd = "*";

                objRQ_InvF_MoveOrd.GwUserCode = GwUserCode;
                objRQ_InvF_MoveOrd.GwPassword = GwPassword;
                objRQ_InvF_MoveOrd.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_MoveOrd.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_MoveOrd.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_MoveOrd.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_MoveOrd.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                objRQ_InvF_MoveOrd.Ft_RecordStart = Ft_RecordStart;
                objRQ_InvF_MoveOrd.Ft_RecordCount = Ft_RecordCount;
                objRQ_InvF_MoveOrd.FlagIsDelete = "0";
                objRQ_InvF_MoveOrd.Ft_Cols_Upd = strFt_Cols_Upd;
                var j = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_MoveOrd);
                InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Save(objRQ_InvF_MoveOrd, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật điều chuyển kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
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

        #region ["Approve"]
        [HttpPost]
        public ActionResult Approve(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry { Success = false };
            try
            {
                List<InvF_MoveOrd> listInvF_MoveOrd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_MoveOrd>>(model);
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
                {
                    // WARQBase
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                };
                if (listInvF_MoveOrd != null && listInvF_MoveOrd.Count > 0)
                {
                    foreach (var item in listInvF_MoveOrd)
                    {
                        objRQ_InvF_MoveOrd.Tid = GetNextTId();
                        objRQ_InvF_MoveOrd.InvF_MoveOrd = new InvF_MoveOrd()
                        {
                            IF_MONo = CUtils.StrValue(item.IF_MONo),
                            Remark = CUtils.StrValue(item.Remark)
                        };
                        InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Appr(objRQ_InvF_MoveOrd, addressAPIs);
                    }
                }


                resultEntry.Success = true;
                resultEntry.AddMessage("Duyệt điều chuyển kho thành công!");
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
        public ActionResult Cancel(string model)
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
                List<InvF_MoveOrd> listInvF_MoveOrd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_MoveOrd>>(model);
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
                {
                    // WARQBase
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                };
                if (listInvF_MoveOrd != null && listInvF_MoveOrd.Count > 0)
                {
                    foreach (var item in listInvF_MoveOrd)
                    {
                        objRQ_InvF_MoveOrd.Tid = GetNextTId();
                        objRQ_InvF_MoveOrd.InvF_MoveOrd = new InvF_MoveOrd()
                        {
                            IF_MONo = CUtils.StrValue(item.IF_MONo),
                            Remark = CUtils.StrValue(item.Remark)
                        };
                        InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Cancel(objRQ_InvF_MoveOrd, addressAPIs);
                    }
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Huỷ điều chuyển kho thành công!");
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

        [HttpPost]
        public ActionResult Cancel_Old(string IF_MONo)
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
                var strWhereInvF_MoveOrd = string.Format("{0}.{1} = '{2}'", TableName.InvF_MoveOrd, TblInvF_MoveOrd.IF_MONo, IF_MONo);
                var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                var Lst_InvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                var Lst_InvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
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
                    Ft_WhereClause = strWhereInvF_MoveOrd,
                    Ft_Cols_Upd = null,
                    FlagIsDelete = "0",
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_MoveOrd = "*",
                    Rt_Cols_InvF_MoveOrdDtl = "*",
                    Rt_Cols_InvF_MoveOrdInstLot = "*",
                    Rt_Cols_InvF_MoveOrdInstSerial = "*"
                };
                var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);
                if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                {
                    objRQ_InvF_MoveOrd.InvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                }
                InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Cancel(objRQ_InvF_MoveOrd, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Huỷ điều chuyển kho thành công!");
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

        #region ["Delete"]
        [HttpPost]
        public ActionResult Delete(string model)
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
                List<InvF_MoveOrd> listInvF_MoveOrd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_MoveOrd>>(model);

                var listIF_MONo = listInvF_MoveOrd.Select(m => string.Format("'{0}'", m.IF_MONo)).ToList();
                listIF_MONo = listIF_MONo.Distinct().ToList();
                var strIF_MONo = string.Join(",", listIF_MONo);
                var strWhereInvF_MoveOrd = string.Format("{0}.{1} in {2}", TableName.InvF_MoveOrd, TblInvF_MoveOrd.IF_MONo, strIF_MONo);

                var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                var Lst_InvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                var Lst_InvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
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
                    Ft_WhereClause = strWhereInvF_MoveOrd,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_MoveOrd = "*",
                    Rt_Cols_InvF_MoveOrdDtl = "*",
                    Rt_Cols_InvF_MoveOrdInstLot = "*",
                    Rt_Cols_InvF_MoveOrdInstSerial = "*"
                };
                var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);
                if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                {
                    foreach (var item in objRT_InvF_MoveOrd.Lst_InvF_MoveOrd)
                    {
                        var iF_MONoCur = CUtils.StrValue(item.IF_MONo);
                        objRQ_InvF_MoveOrd.Tid = GetNextTId();
                        objRQ_InvF_MoveOrd.FlagIsDelete = "1";
                        objRQ_InvF_MoveOrd.Ft_WhereClause = "";
                        objRQ_InvF_MoveOrd.InvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();

                        if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                        {
                            var listInvF_MoveOrdDtl = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Where(it => !CUtils.IsNullOrEmpty(it.IF_MONo) && CUtils.StrValue(it.IF_MONo).Equals(iF_MONoCur)).ToList();
                            if (listInvF_MoveOrdDtl == null || listInvF_MoveOrdDtl.Count == 0)
                            {
                                listInvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                            }
                            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.AddRange(listInvF_MoveOrdDtl);
                        }
                        if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.Count > 0)
                        {
                            var listInvF_MoveOrdInstLot = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.Where(it => !CUtils.IsNullOrEmpty(it.IF_MONo) && CUtils.StrValue(it.IF_MONo).Equals(iF_MONoCur)).ToList();
                            if (listInvF_MoveOrdInstLot == null || listInvF_MoveOrdInstLot.Count == 0)
                            {
                                listInvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                            }
                            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.AddRange(listInvF_MoveOrdInstLot);
                        }
                        if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.Count > 0)
                        {
                            var listInvF_MoveOrdInstSerial = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.Where(it => !CUtils.IsNullOrEmpty(it.IF_MONo) && CUtils.StrValue(it.IF_MONo).Equals(iF_MONoCur)).ToList();
                            if (listInvF_MoveOrdInstSerial == null || listInvF_MoveOrdInstSerial.Count == 0)
                            {
                                listInvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();
                            }
                            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.AddRange(listInvF_MoveOrdInstSerial);
                        }
                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_MoveOrd);
                        InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Save(objRQ_InvF_MoveOrd, addressAPIs);
                    }
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xoá điều chuyển kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Chưa chọn phiếu điều chuyển cần xóa!");
                    //resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Delete_Old(string IF_MONo)
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
                var strWhereInvF_MoveOrd = string.Format("{0}.{1} = '{2}'", TableName.InvF_MoveOrd, TblInvF_MoveOrd.IF_MONo, IF_MONo);
                var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                var Lst_InvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                var Lst_InvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
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
                    Ft_WhereClause = strWhereInvF_MoveOrd,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_InvF_MoveOrd = "*",
                    Rt_Cols_InvF_MoveOrdDtl = "*",
                    Rt_Cols_InvF_MoveOrdInstLot = "*",
                    Rt_Cols_InvF_MoveOrdInstSerial = "*"
                };
                var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);
                if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                {
                    objRQ_InvF_MoveOrd.InvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl);
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot = new List<InvF_MoveOrdInstLot>();
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot);
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial = new List<InvF_MoveOrdInstSerial>();
                    objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial);
                    objRQ_InvF_MoveOrd.Ft_WhereClause = "";
                    objRQ_InvF_MoveOrd.FlagIsDelete = "1";
                }
                InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Save(objRQ_InvF_MoveOrd, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá điều chuyển kho thành công!");
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

        #region ["Excel"]
        #region ["Export excel màn hình quản lý"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string IF_MONo = "",
            string CreateTimeFrom = "",
            string CreateTimeTo = "",
            string InvCodeOut = "",
            string ApprTimeFrom = "",
            string ApprTimeTo = "",
            string InvCodeIn = "",
            string Remark = "",
            string chkPending = "",
            string chkApproved = "",
            string chkCanceled = "",
            int recordcount = 50,
            int page = 0)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            string url = "";
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;

            var ListInvF_MoveOrd = new List<InvF_MoveOrd>();
            var ListInvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
            try
            {
                #region["Search"]
                var strWhereClause = strWhereClause_InvF_MoveOrd(IF_MONo, CreateTimeFrom, CreateTimeTo, InvCodeOut, ApprTimeFrom,
                    ApprTimeTo, InvCodeIn, Remark, chkPending, chkApproved, chkCanceled);

                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_InvF_MoveOrd = "*",
                    Rt_Cols_InvF_MoveOrdDtl = "*",
                    Rt_Cols_InvF_MoveOrdInstLot = "*",
                    Rt_Cols_InvF_MoveOrdInstSerial = "*"
                };
                var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);

                if (objRT_InvF_MoveOrd != null)
                {
                    if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count > 0)
                    {

                        ListInvF_MoveOrd.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrd);
                        foreach (var item in ListInvF_MoveOrd)
                        {
                            var createDTimeUTC = CUtils.StrValue(item.CreateDTimeUTC);
                            if (!CUtils.IsNullOrEmpty(createDTimeUTC))
                            {
                                createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(createDTimeUTC, Nonsense.DATE_TIME_FULL_DB_FORMAT, UserState.UtcOffset);
                            }
                            item.CreateDTimeUTC = createDTimeUTC;
                            var apprDTimeUTC = CUtils.StrValue(item.ApprDTimeUTC);
                            if (!CUtils.IsNullOrEmpty(apprDTimeUTC))
                            {
                                apprDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(apprDTimeUTC, Nonsense.DATE_TIME_FULL_DB_FORMAT, UserState.UtcOffset);
                            }
                            item.ApprDTimeUTC = apprDTimeUTC;
                        }
                    }
                    if (objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl.Count > 0)
                    {
                        ListInvF_MoveOrdDtl.AddRange(objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl);
                    }

                    var listDtl = new List<InvF_MoveOrdDtl>();
                    if (ListInvF_MoveOrdDtl.Count > 0)
                    {
                        foreach (var item in ListInvF_MoveOrd)
                        {
                            var listDtlCur = new List<InvF_MoveOrdDtl>();
                            listDtlCur = ListInvF_MoveOrdDtl.Where(it => item.IF_MONo.Equals(item.IF_MONo)).ToList();
                            listDtl.AddRange(listDtlCur);
                        }
                    }

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    Dictionary<string, string> dicColNamesDtl = GetImportDicColumsDtl();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_MoveOrd).Name), ref url);
                    var ds = new DataSet();
                    var table = ExcelExport.ConstructDataTable(ListInvF_MoveOrd, dicColNames);
                    var tableDtl = ExcelExport.ConstructDataTable(listDtl, dicColNamesDtl);
                    //ExcelExport.ExportToExcelFromList(ListInv_FInventoryInFGSearch, dicColNames, filePath, string.Format("InvF_InventoryInFG"));
                    if (table != null && table.Rows.Count > 0)
                    {
                        table.TableName = "InvF_MoveOrd";
                        ds.Tables.Add(table);
                    }
                    if (tableDtl != null && tableDtl.Rows.Count > 0)
                    {
                        tableDtl.TableName = "InvF_MoveOrdDtl";
                        ds.Tables.Add(tableDtl);
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

        #region ["Export hàng hoá màn hình tạo mới"]
        [HttpPost]
        public ActionResult ExportExcelPrdMoveOrd(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            var url = "";
            var ListInvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();

            ListInvF_MoveOrdDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_MoveOrdDtl>>(model);

            try
            {
                if (ListInvF_MoveOrdDtl != null && ListInvF_MoveOrdDtl.Count > 0)
                {
                    Dictionary<string, string> dicColNamesDtl = GetExportDicColumsProduct();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_MoveOrdDtl).Name), ref url);
                    ExcelExport.ExportToExcelFromList(ListInvF_MoveOrdDtl, dicColNamesDtl, filePath, string.Format("InvF_ProductMoveOrd"));

                    return Json(new { Success = true, Messages = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Messages = "Dữ liệu trống!", CheckSuccess = "1" });
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

        #region ["Excel hàng hoá"]
        public ActionResult ExportTempMoveOrdDtl()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv = new List<Mst_Inventory>();
                Dictionary<string, string> dicColNames = GetImportDicColumsMoveOrdPrdDtl();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_MoveOrdDtl).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv, dicColNames, filePath, string.Format("InvF_MoveOrdDtl"));

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
        public ActionResult ImportMoveOrdDtl(HttpPostedFileWrapper file, string bupatternout)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage(Nonsense.MESS_CHECK_FILEIMPORT);
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception(Nonsense.MESS_CHECK_FILEIMPORT);
                    }

                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var listPrdImport = new List<InvF_MoveOrdDtlUI>();


                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 4)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region ["Check null"]
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["mp_ProductCodeUser"] == null || dr["mp_ProductCodeUser"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã sản phẩm không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["Qty"] == null || dr["Qty"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Tên kho " + "'" + dr["InvCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Tồn kho không được trống";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeIn"] == null || dr["InvCodeIn"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Vị trí nhập " + "'" + dr["InvCodeIn"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Vị trí nhập không được trống";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeOut"] == null || dr["InvCodeOut"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Vị trí xuất " + "'" + dr["InvCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Vị trí xuất không được trống";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            #endregion

                            #region ["Check duplicate"]
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                var productCodeUserCur = table.Rows[i]["mp_ProductCodeUser"].ToString().Trim();
                                var invCodeIn = table.Rows[i]["InvCodeIn"].ToString().Trim();
                                var invCodeOut = table.Rows[i]["InvCodeOut"].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var _productCodeUser = table.Rows[j]["mp_ProductCodeUser"].ToString().Trim();
                                        var _invCodeIn = table.Rows[j]["InvCodeIn"].ToString().Trim();
                                        var _invCodeOut = table.Rows[j]["InvCodeOut"].ToString().Trim();
                                        if (productCodeUserCur.Equals(_productCodeUser) && _invCodeIn.Equals(invCodeIn) && _invCodeOut.Equals(invCodeOut))
                                        {
                                            var exitsData = "Hàng hoá điều chuyển '" + productCodeUserCur + "' bị lặp trong file excel!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion

                            listPrdImport = DataTableCmUtils.ToListof<InvF_MoveOrdDtlUI>(table);
                            var listProductCodeUserImport = listPrdImport.Select(pro => pro.mp_ProductCodeUser).Distinct().ToArray();
                            var listStrProductCodeUserImport = string.Join(",", listProductCodeUserImport);

                            #region [Get ProductCode]
                            var sbSQL_Mst_Product_GetPrdCode = new StringBuilder();
                            var strWhere_GetPrdCode = "";
                            var Tbl_Mst_Product = "Mst_Product.";
                            sbSQL_Mst_Product_GetPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, listStrProductCodeUserImport, "IN");
                            strWhere_GetPrdCode = sbSQL_Mst_Product_GetPrdCode.ToString();

                            var listPrdCodeImport = new List<string>();
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
                                FlagIsDelete = "0",
                                Ft_WhereClause = strWhere_GetPrdCode,
                                Ft_Cols_Upd = null,

                                Rt_Cols_Mst_Product = "*"
                            };
                            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                            var objRT_Mst_ProductBase = new RT_Mst_Product();
                            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                            {
                                //danh sách hàng cùng base
                                var listPrdCodeBase = objRT_Mst_Product.Lst_Mst_Product.Select(m => m.ProductCodeBase.ToString()).ToList();
                                var lstPrdCodeBase = string.Join(",", listPrdCodeBase);
                                #region ["Danh sách hàng cùng base"]
                                var sbSQL_Mst_Product_GetPrdCodeBase = new StringBuilder();
                                var strWhere_GetPrdCodeBase = "";
                                var Tbl_Mst_ProductBase = "Mst_Product.";
                                sbSQL_Mst_Product_GetPrdCodeBase.AddWhereClause(Tbl_Mst_ProductBase + TblMst_Product.ProductCodeBase, lstPrdCodeBase, "IN");
                                strWhere_GetPrdCodeBase = sbSQL_Mst_Product_GetPrdCodeBase.ToString();

                                var listPrdCodeImportBase = new List<string>();
                                var objRQ_Mst_Product_Base = new RQ_Mst_Product()
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
                                    FlagIsDelete = "0",
                                    Ft_WhereClause = strWhere_GetPrdCodeBase,
                                    Ft_Cols_Upd = null,

                                    Rt_Cols_Mst_Product = "*"
                                };
                                objRT_Mst_ProductBase = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product_Base, addressAPIs);
                                #endregion

                                foreach (var prdImport in listPrdImport)
                                {
                                    var productCur = objRT_Mst_ProductBase.Lst_Mst_Product
                                        .FirstOrDefault(prd => prd.ProductCodeUser.Equals(prdImport.mp_ProductCodeUser));
                                    if (productCur != null)
                                    {
                                        prdImport.ProductCode = productCur.ProductCode;
                                        prdImport.mp_ProductName = productCur.ProductName;
                                        prdImport.mp_ProductCodeBase = productCur.ProductCodeBase;
                                        prdImport.mp_ValConvert = productCur.ValConvert;
                                        prdImport.UnitCode = productCur.UnitCode;
                                    }
                                    listPrdCodeImport.Add(productCur.ProductCodeBase.ToString());
                                }
                            }
                            else
                            {
                                var exitsData = "Mã hàng hoá không tồn tại trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            var listStrPrdCodeImport = string.Join(",", listPrdCodeImport.ToArray());
                            #endregion

                            #region Lấy thông tin tồn kho
                            var strWhereClauseBalance = "";
                            var sb_SQL = new StringBuilder();
                            var TblInv_InventoryBalance = "Inv_InventoryBalance.";
                            if (listStrProductCodeUserImport != null && listProductCodeUserImport.Length > 0)
                            {
                                sb_SQL.AddWhereClause(TblInv_InventoryBalance + "ProductCode", listStrPrdCodeImport, "IN");
                            }
                            if (!CUtils.IsNullOrEmpty(bupatternout))
                            {
                                sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", bupatternout, "LIKE");
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
                            var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                            if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                            {
                                lstInv_InventoryBalance.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance);
                            }
                            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                            if (objRT_Mst_ProductBase != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
                            {
                                foreach (var it in objRT_Mst_ProductBase.Lst_Mst_Product)
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
                                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                                    {
                                        if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                                }
                                            }
                                        }
                                    }
                                    lst_Mst_ProductUI.Add(productUI);
                                }
                                foreach (var dtl in listPrdImport)
                                {
                                    dtl.Lst_Mst_ProductBase = new List<Mst_ProductUI>();
                                    var lstProductBaseItem = lst_Mst_ProductUI.Where(x => x.ProductCodeBase.Equals(dtl.mp_ProductCodeBase)).ToList();
                                    var prdUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(dtl.ProductCode)).FirstOrDefault();
                                    if (lstProductBaseItem != null)
                                    {
                                        dtl.Lst_Mst_ProductBase.AddRange(lstProductBaseItem);
                                    }
                                    if (prdUI != null)
                                    {
                                        dtl.QtyTotalOK = Convert.ToDouble(prdUI.QtyTotalOK);
                                        dtl.mp_FlagLot = CUtils.StrValue(prdUI.FlagLo);
                                        dtl.mp_FlagSerial = CUtils.StrValue(prdUI.FlagSerial);
                                    }

                                    if (Convert.ToDouble(dtl.Qty) > Convert.ToDouble(dtl.QtyTotalOK))
                                    {
                                        var exitsData = "Mã hàng hoá " + dtl.mp_ProductCodeUser + " tồn kho còn " + dtl.QtyTotalOK + "!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            #endregion                            

                            resultEntry.Success = true;
                            resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                            resultEntry.Detail = Newtonsoft.Json.JsonConvert.SerializeObject(listPrdImport);
                            return Json(resultEntry);
                        }
                    }
                    else
                    {
                        var exitsData = Nonsense.MESS_CHECK_FILE_NULL;
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
        #endregion

        #region ["Excel Lot"]
        public ActionResult ExportTempMoveOrdLot()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv = new List<Mst_Inventory>();
                Dictionary<string, string> dicColNames = GetImportDicColumsMoveOrdPrdLot();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_MoveOrdInstLot).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv, dicColNames, filePath, string.Format("InvF_MoveOrdInstLot"));

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
        public ActionResult ImportMoveOrdLot(HttpPostedFileWrapper file, string bupatternout)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage(Nonsense.MESS_CHECK_FILEIMPORT);
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception(Nonsense.MESS_CHECK_FILEIMPORT);
                    }
                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var listPrdLotImport = new List<InvF_MoveOrdInstLotUI>();

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 5)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region ["Check null"]
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["mp_ProductCodeUser"] == null || dr["mp_ProductCodeUser"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã sản phẩm không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["ProductLotNo"] == null || dr["ProductLotNo"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Số lô " + "'" + dr["ProductLotNo"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Số lô không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["Qty"] == null || dr["Qty"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Tên kho " + "'" + dr["InvCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Số lượng  không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeIn"] == null || dr["InvCodeIn"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Vị trí nhập " + "'" + dr["InvCodeIn"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Vị trí nhập không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeOut"] == null || dr["InvCodeOut"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Vị trí xuất " + "'" + dr["InvCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    var exitsData = "Vị trí xuất không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            #endregion

                            #region ["Check duplicate"]
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                var productCodeUserCur = table.Rows[i]["mp_ProductCodeUser"].ToString().Trim();
                                var productLotNo = table.Rows[i]["ProductLotNo"].ToString().Trim();
                                var invCodeOut = table.Rows[i]["InvCodeOut"].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var _productCodeUser = table.Rows[j]["mp_ProductCodeUser"].ToString().Trim();
                                        var _productLotNo = table.Rows[j]["ProductLotNo"].ToString().Trim();
                                        var _invCodeOut = table.Rows[j]["InvCodeOut"].ToString().Trim();
                                        if (productCodeUserCur.Equals(_productCodeUser) && productLotNo.Equals(_productLotNo) && invCodeOut.Equals(_invCodeOut))
                                        {
                                            var exitsData = "Mã hàng hoá '" + productCodeUserCur + "' - số lô"+ productLotNo + " - vị trí xuất " + invCodeOut + " bị lặp trong file excel!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion

                            listPrdLotImport = DataTableCmUtils.ToListof<InvF_MoveOrdInstLotUI>(table);
                            var listProductCodeUserImport = listPrdLotImport.Select(pro => pro.mp_ProductCodeUser).Distinct().ToArray();
                            var listStrProductCodeUserImport = string.Join(",", listProductCodeUserImport);

                            #region ["Get ProductCode"]
                            var listPrdCodeImport = new List<string>();
                            var sbSQL_Mst_Product_GetPrdCode = new StringBuilder();
                            var strWhere_GetPrdCode = "";
                            var Tbl_Mst_Product = "Mst_Product.";
                            sbSQL_Mst_Product_GetPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, listStrProductCodeUserImport, "IN");
                            sbSQL_Mst_Product_GetPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagLot, "1", "=");
                            sbSQL_Mst_Product_GetPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSerial, "0", "=");
                            strWhere_GetPrdCode = sbSQL_Mst_Product_GetPrdCode.ToString();

                            var listPrdCodeBaseImport = new List<string>();
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
                                FlagIsDelete = "0",
                                Ft_WhereClause = strWhere_GetPrdCode,
                                Ft_Cols_Upd = null,

                                Rt_Cols_Mst_Product = "*"
                            };
                            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                            var objRT_Mst_ProductBase = new RT_Mst_Product();
                            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                            {
                                //danh sách hàng cùng base
                                var listPrdCodeBase = objRT_Mst_Product.Lst_Mst_Product.Select(m => m.ProductCodeBase.ToString()).ToList();
                                var lstPrdCodeBase = string.Join(",", listPrdCodeBase);
                                #region ["Danh sách hàng cùng base"]
                                var sbSQL_Mst_Product_GetPrdCodeBase = new StringBuilder();
                                var strWhere_GetPrdCodeBase = "";
                                var Tbl_Mst_ProductBase = "Mst_Product.";
                                sbSQL_Mst_Product_GetPrdCodeBase.AddWhereClause(Tbl_Mst_ProductBase + TblMst_Product.ProductCodeBase, lstPrdCodeBase, "IN");
                                strWhere_GetPrdCodeBase = sbSQL_Mst_Product_GetPrdCodeBase.ToString();

                                var listPrdCodeImportBase = new List<string>();
                                var objRQ_Mst_Product_Base = new RQ_Mst_Product()
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
                                    FlagIsDelete = "0",
                                    Ft_WhereClause = strWhere_GetPrdCodeBase,
                                    Ft_Cols_Upd = null,

                                    Rt_Cols_Mst_Product = "*"
                                };
                                objRT_Mst_ProductBase = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product_Base, addressAPIs);
                                #endregion

                                foreach (var prdImport in listPrdLotImport)
                                {
                                    var productCur = objRT_Mst_Product.Lst_Mst_Product
                                        .FirstOrDefault(prd => prd.ProductCodeUser.Equals(prdImport.mp_ProductCodeUser));
                                    if (productCur != null)
                                    {
                                        prdImport.ProductCode = productCur.ProductCode;
                                        prdImport.ProductCodeBase = productCur.ProductCodeBase;
                                        prdImport.ValConvert = Convert.ToDouble(productCur.ValConvert);
                                        prdImport.mp_ProductName = productCur.ProductName;
                                        prdImport.UnitCode = productCur.UnitCode;
                                    }

                                    listPrdCodeImport.Add(productCur.ProductCodeBase.ToString());
                                }
                            }
                            else
                            {
                                var exitsData = "Mã hàng hoá không tồn tại trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            var listStrPrdCodeBaseImport = string.Join(",", listPrdCodeBaseImport.ToArray());
                            var listStrPrdCodeImport = string.Join(",", listPrdCodeImport.ToArray());
                            #endregion

                            #region ["Kiểm tra Lot tồn tại hay tồn kho"]
                            foreach (var prdImport in listPrdLotImport)
                            {
                                var strWhereClauseBalanceLot = "";
                                var sb_SQLLot = new StringBuilder();
                                var TblInv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                                // 
                                if (listStrPrdCodeBaseImport != null && listStrPrdCodeBaseImport.Length > 0)
                                {
                                    sb_SQLLot.AddWhereClause(TblInv_InventoryBalanceLot + "ProductCode", prdImport.ProductCodeBase, "=");
                                }
                                sb_SQLLot.AddWhereClause(TblInv_InventoryBalanceLot + "ProductLotNo", prdImport.ProductLotNo, "=");
                                strWhereClauseBalanceLot = sb_SQLLot.ToString();

                                var objRQ_Inv_InventoryBalanceLot = new RQ_Inv_InventoryBalanceLot()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    Rt_Cols_Inv_InventoryBalanceLot = "*",
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClauseBalanceLot
                                };
                                var objRT_Inv_InventoryBalanceLot = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(objRQ_Inv_InventoryBalanceLot, addressAPIs);
                                if (objRT_Inv_InventoryBalanceLot != null && objRT_Inv_InventoryBalanceLot.Lst_Inv_InventoryBalanceLot != null && objRT_Inv_InventoryBalanceLot.Lst_Inv_InventoryBalanceLot.Count > 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    var exitsData = "Mã lot " + prdImport.ProductLotNo + " không tồn kho hoặc không có trong hệ thống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                };
                            }

                            #endregion

                            #region Lấy thông tin tồn kho
                            var strWhereClauseBalance = "";
                            var sb_SQL = new StringBuilder();
                            var TblInv_InventoryBalance = "Inv_InventoryBalance.";
                            if (listStrProductCodeUserImport != null && listProductCodeUserImport.Length > 0)
                            {
                                sb_SQL.AddWhereClause(TblInv_InventoryBalance + "ProductCode", listStrPrdCodeImport, "IN");
                            }
                            if (!CUtils.IsNullOrEmpty(bupatternout))
                            {
                                sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", bupatternout, "LIKE");
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
                            var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                            if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                            {
                                lstInv_InventoryBalance.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance);
                            }
                            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                            if (objRT_Mst_ProductBase != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
                            {
                                foreach (var it in objRT_Mst_ProductBase.Lst_Mst_Product)
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
                                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                                    {
                                        if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                                }
                                            }
                                        }
                                    }
                                    lst_Mst_ProductUI.Add(productUI);
                                }
                                foreach (var dtl in listPrdLotImport)
                                {
                                    dtl.Lst_Mst_ProductBase = new List<Mst_ProductUI>();
                                    var lstProductBaseItem = lst_Mst_ProductUI.Where(x => x.ProductCodeBase.Equals(dtl.ProductCodeBase)).ToList();
                                    var prdUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(dtl.ProductCode)).FirstOrDefault();
                                    if (lstProductBaseItem != null)
                                    {
                                        dtl.Lst_Mst_ProductBase.AddRange(lstProductBaseItem);
                                    }
                                    if (prdUI != null)
                                    {
                                        dtl.QtyTotalOK = Convert.ToDouble(prdUI.QtyTotalOK);
                                        dtl.mp_FlagLot = CUtils.StrValue(prdUI.FlagLo);
                                        dtl.mp_FlagSerial = CUtils.StrValue(prdUI.FlagSerial);
                                    }
                                    if(Convert.ToDouble(dtl.Qty) > Convert.ToDouble(dtl.QtyTotalOK))
                                    {
                                        var exitsData = "Mã hàng hoá "+ dtl.mp_ProductCodeUser + " tồn kho còn "+ dtl.QtyTotalOK + "!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            #endregion                            

                            resultEntry.Success = true;
                            resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                            resultEntry.Detail = Newtonsoft.Json.JsonConvert.SerializeObject(listPrdLotImport);
                            return Json(resultEntry);
                        }
                    }
                    else
                    {
                        var exitsData = Nonsense.MESS_CHECK_FILE_NULL;
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
        #endregion

        #region ["Excel Serial"]
        public ActionResult ExportTempMoveOrdSerial()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv = new List<Mst_Inventory>();
                Dictionary<string, string> dicColNames = GetImportDicColumsMoveOrdSerial();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_MoveOrdInstSerial).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv, dicColNames, filePath, string.Format("InvF_MoveOrdInstSerial"));

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
        public ActionResult ImportMoveOrdSerial(HttpPostedFileWrapper file, string bupatternout)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage(Nonsense.MESS_CHECK_FILEIMPORT);
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception(Nonsense.MESS_CHECK_FILEIMPORT);
                    }

                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var listPrdSerialImport = new List<InvF_MoveOrdInstSerialUI>();


                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 4)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region ["Check null"]
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["mp_ProductCodeUser"] == null || dr["mp_ProductCodeUser"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã sản phẩm không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["SerialNo"] == null || dr["SerialNo"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Số serial không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeIn"] == null || dr["InvCodeIn"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Vị trí nhập không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeOut"] == null || dr["InvCodeOut"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Vị trí xuất không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            #endregion

                            #region ["Check duplicate"]
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                var productCodeUserCur = table.Rows[i]["mp_ProductCodeUser"].ToString().Trim();
                                var serialNo = table.Rows[i]["SerialNo"].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var _productUserCode = table.Rows[j]["mp_ProductCodeUser"].ToString().Trim();
                                        var _serialNo = table.Rows[j]["SerialNo"].ToString().Trim();
                                        if (productCodeUserCur.Equals(_productUserCode) && serialNo.Equals(_serialNo))
                                        {
                                            var exitsData = "Thông tin điều chuyển Serial '" + productCodeUserCur + "' bị lặp trong file excel!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion

                            listPrdSerialImport = DataTableCmUtils.ToListof<InvF_MoveOrdInstSerialUI>(table);
                            var listProductCodeUserImport = listPrdSerialImport.Select(pro => pro.mp_ProductCodeUser).Distinct().ToArray();
                            var listStrProductCodeUserImport = string.Join(",", listProductCodeUserImport);

                            #region ["Get ProductCode"]
                            var listPrdCodeImport = new List<string>();
                            var listPrdCodeBaseImport = new List<string>();
                            var sbSql = new StringBuilder();
                            var Tbl_Mst_Product = "Mst_Product.";
                            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, listStrProductCodeUserImport, "IN");
                            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSerial, "1", "=");
                            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagLot, "0", "=");
                            var strWhere_Mst_Product = sbSql.ToString();
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
                                FlagIsDelete = "0",
                                Ft_WhereClause = strWhere_Mst_Product,
                                Ft_Cols_Upd = null,

                                Rt_Cols_Mst_Product = "*"
                            };
                            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                            var objRT_Mst_ProductBase = new RT_Mst_Product();
                            if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                            {
                                //danh sách hàng cùng base
                                var listPrdCodeBase = objRT_Mst_Product.Lst_Mst_Product.Select(m => m.ProductCodeBase.ToString()).ToList();
                                var lstPrdCodeBase = string.Join(",", listPrdCodeBase);
                                #region ["Danh sách hàng cùng base"]
                                var sbSQL_Mst_Product_GetPrdCodeBase = new StringBuilder();
                                var strWhere_GetPrdCodeBase = "";
                                var Tbl_Mst_ProductBase = "Mst_Product.";
                                sbSQL_Mst_Product_GetPrdCodeBase.AddWhereClause(Tbl_Mst_ProductBase + TblMst_Product.ProductCodeBase, lstPrdCodeBase, "IN");
                                strWhere_GetPrdCodeBase = sbSQL_Mst_Product_GetPrdCodeBase.ToString();

                                var listPrdCodeImportBase = new List<string>();
                                var objRQ_Mst_Product_Base = new RQ_Mst_Product()
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
                                    FlagIsDelete = "0",
                                    Ft_WhereClause = strWhere_GetPrdCodeBase,
                                    Ft_Cols_Upd = null,

                                    Rt_Cols_Mst_Product = "*"
                                };
                                objRT_Mst_ProductBase = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product_Base, addressAPIs);
                                #endregion

                                foreach (var prdImport in listPrdSerialImport)
                                {
                                    var productCur = objRT_Mst_Product.Lst_Mst_Product
                                        .FirstOrDefault(prd => prd.ProductCodeUser.Equals(prdImport.mp_ProductCodeUser));
                                    if (productCur != null)
                                    {
                                        prdImport.ProductCode = productCur.ProductCode;
                                        prdImport.ProductCodeBase = productCur.ProductCodeBase;
                                        prdImport.UnitCode = productCur.UnitCode;
                                        prdImport.mp_ProductName = productCur.ProductName;
                                    }
                                    listPrdCodeImport.Add(prdImport.ProductCodeBase.ToString());
                                }
                            }
                            else
                            {
                                var exitsData = "Mã hàng hoá quản lý Serial không tồn tại trong hệ thống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            var listStrPrdCodeImport = string.Join(",", listPrdCodeImport.ToArray());
                            #endregion

                            var listStrPrdCodeBaseImport = string.Join(",", listPrdCodeBaseImport.ToArray());

                            #region ["Kiểm tra Serial"]
                            foreach (var prdImport in listPrdSerialImport)
                            {
                                var strWhereClauseBalanceSerial = "";
                                var sb_SQLSerial = new StringBuilder();
                                var TblInv_InventoryBalanceLot = "Inv_InventoryBalanceSerial.";
                                if (listStrPrdCodeBaseImport != null && listStrPrdCodeBaseImport.Length > 0)
                                {
                                    sb_SQLSerial.AddWhereClause(TblInv_InventoryBalanceLot + "ProductCode", prdImport.ProductCodeBase, "=");
                                }
                                sb_SQLSerial.AddWhereClause(TblInv_InventoryBalanceLot + "SerialNo", prdImport.SerialNo, "=");
                                strWhereClauseBalanceSerial = sb_SQLSerial.ToString();

                                var objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    Rt_Cols_Inv_InventoryBalanceSerial = "*",
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = strWhereClauseBalanceSerial
                                };
                                var objRT_Inv_InventoryBalanceSerial = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                                if (objRT_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    var exitsData = "Mã Serial " + prdImport.SerialNo + " không tồn kho hoặc không tồn tại!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                };
                            }

                            #endregion

                            #region Lấy thông tin tồn kho
                            var strWhereClauseBalance = "";
                            var sb_SQL = new StringBuilder();
                            var TblInv_InventoryBalance = "Inv_InventoryBalance.";
                            if (listStrProductCodeUserImport != null && listProductCodeUserImport.Length > 0)
                            {
                                sb_SQL.AddWhereClause(TblInv_InventoryBalance + "ProductCode", listStrPrdCodeImport, "IN");
                            }
                            if (!CUtils.IsNullOrEmpty(bupatternout))
                            {
                                sb_SQL.AddWhereClause("Mst_Inventory.InvBUCode", bupatternout, "LIKE");
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
                            var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                            if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                            {
                                lstInv_InventoryBalance.AddRange(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance);
                            }
                            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                            if (objRT_Mst_ProductBase != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
                            {
                                foreach (var it in objRT_Mst_ProductBase.Lst_Mst_Product)
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
                                    if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                                    {
                                        if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                                            if (lstProductByCode != null && lstProductByCode.Count > 0)
                                            {
                                                var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                                //sắp xếp giảm dần
                                                var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                                double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                                qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                                if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                                {
                                                    productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                                }
                                            }
                                        }
                                    }
                                    lst_Mst_ProductUI.Add(productUI);
                                }
                                var i = 1;
                                foreach (var dtl in listPrdSerialImport)
                                {
                                    dtl.Lst_Mst_ProductBase = new List<Mst_ProductUI>();
                                    var lstProductBaseItem = lst_Mst_ProductUI.Where(x => x.ProductCodeBase.Equals(dtl.ProductCodeBase)).ToList();
                                    var prdUI = lst_Mst_ProductUI.Where(x => x.ProductCode.Equals(dtl.ProductCode)).FirstOrDefault();
                                    if (lstProductBaseItem != null)
                                    {
                                        dtl.Lst_Mst_ProductBase.AddRange(lstProductBaseItem);
                                    }
                                    if (prdUI != null)
                                    {
                                        dtl.QtyTotalOK = Convert.ToDouble(prdUI.QtyTotalOK);
                                        dtl.mp_FlagLot = CUtils.StrValue(prdUI.FlagLo);
                                        dtl.mp_FlagSerial = CUtils.StrValue(prdUI.FlagSerial);
                                        
                                    }
                                }
                            }
                            #endregion                            

                            resultEntry.Success = true;
                            resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                            resultEntry.Detail = Newtonsoft.Json.JsonConvert.SerializeObject(listPrdSerialImport);
                            return Json(resultEntry);
                        }
                    }
                    else
                    {
                        var exitsData = Nonsense.MESS_CHECK_FILE_NULL;
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
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"IF_MONo","Số phiếu điều chuyển"},
                 //{"MoveOrdType","Loại điều chuyển"},
                 {"InvCodeOut","Kho xuất"},
                 {"InvCodeIn","Kho nhập"},
                 {"CreateDTimeUTC", "Thời gian tạo" },
                 {"ApprDTimeUTC", "Thời gian duyệt"},
                 {"Remark", "Nội dung điều chuyển"},
                 {"CreateBy", "Người tạo"}
            };
        }

        private Dictionary<string, string> GetImportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                {"IF_MONo","Số phiếu điều chuyển"},
                {"ProductCode", "Mã hàng hoá" },
                {"Qty","Số lượng" },
                {"IF_MOStatusDtl", "Trạng thái phiếu" }
            };
        }

        private Dictionary<string, string> GetImportDicColumsMoveOrdPrdDtl()
        {
            return new Dictionary<string, string>()
            {
                {"mp_ProductCodeUser","Mã hàng hoá(*)"},
                {"Qty", "Số lượng điều chuyển" },
                {"InvCodeOut","Vị trí xuất(*)" },
                {"InvCodeIn", "Vị trí nhập(*)" }
            };
        }

        private Dictionary<string, string> GetImportDicColumsMoveOrdSerial()
        {
            return new Dictionary<string, string>()
            {
                {"mp_ProductCodeUser","Mã hàng hoá(*)"},
                {"SerialNo", "Số serial(*)" },
                {"InvCodeOut","Vị trí xuất(*)" },
                {"InvCodeIn", "Vị trí nhập(*)" }
            };
        }

        private Dictionary<string, string> GetImportDicColumsMoveOrdPrdLot()
        {
            return new Dictionary<string, string>()
            {
                {"mp_ProductCodeUser","Mã hàng hoá(*)"},
                {"ProductLotNo","Số lô(*)"},
                {"Qty", "Số lượng điều chuyển" },
                {"InvCodeOut","Vị trí xuất(*)" },
                {"InvCodeIn", "Vị trí nhập(*)" }
            };
        }

        private Dictionary<string, string> GetExportDicColumsProduct()
        {
            return new Dictionary<string, string>()
            {
                {"mp_ProductCodeUser","Mã hàng hoá"},
                {"mp_ProductName", "Tên hàng hoá" },
                //{"Qty","Số lượng điều chuyển" },
                {"UnitCode", "Đơn vị" }
                //{"IF_MOStatusDtl", "Trạng thái phiếu" }
            };
        }

        #endregion
        #endregion

        #region ["Show Popup"]
        public ActionResult SearchProduct(string productcode, int recordcount = 10, int page = 0)
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

            var strWhere_Mst_Product = strWhereClause_Mst_Product(productcode, "1", "PRODUCT", "");
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
            #region Lấy thông tin tồn kho
            
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            foreach (var it in List_Mst_Product)
            {
                var productUI = new Mst_ProductUI()
                {
                    ProductCode = it.ProductCode,
                    ProductCodeUser = it.ProductCodeUser,
                    ProductName = it.ProductName,
                    UnitCode = it.UnitCode,
                    ProductCodeBase = it.ProductCodeBase,
                    ValConvert = it.ValConvert,                    
                };
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
                lst_Mst_ProductUI.Add(productUI);
            }

            pageInfo.DataList = lst_Mst_ProductUI;
            #endregion

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return JsonView("SearchProduct", pageInfo);
        }

        public ActionResult ShowPopupInvInOut(string productcode, string productname, string unitcode, string bupatternout, string bupatternin)
        {
            #region ["Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            ViewBag.ProductCode = productcode;
            ViewBag.ProductName = productname;
            ViewBag.UnitCode = unitcode;
            var List_Mst_Inventory = new List<Mst_Inventory>();
            try
            {
                #region ["Danh sách kho"]
                var strWhereInvOut = "";//kho xuất
                var sbSqlOut = new StringBuilder();
                var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
                sbSqlOut.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, Client_Flag.Active, "=");
                if (!CUtils.IsNullOrEmpty(bupatternout))
                {
                    sbSqlOut.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvBUPattern, bupatternout, "LIKE");
                }
                strWhereInvOut = sbSqlOut.ToString();

                var strWhereInvIn = "";//kho nhập
                var sbSqlIn = new StringBuilder();
                sbSqlIn.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, Client_Flag.Active, "=");
                if (!CUtils.IsNullOrEmpty(bupatternin))
                {
                    sbSqlIn.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvBUPattern, bupatternin, "LIKE");
                }
                strWhereInvIn = sbSqlIn.ToString();

                var List_Mst_InventoryOut = new List<Mst_Inventory>();
                var List_Mst_InventoryIn = new List<Mst_Inventory>();
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Inventory = "*",
                };

                objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvOut;
                var objRT_Mst_InventoryOut = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_InventoryOut != null && objRT_Mst_InventoryOut.Lst_Mst_Inventory.Count > 0)
                {
                    List_Mst_InventoryOut.AddRange(objRT_Mst_InventoryOut.Lst_Mst_Inventory);
                }
                ViewBag.List_Mst_InventoryOut = List_Mst_InventoryOut;

                objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvIn;
                var objRT_Mst_InventoryIn = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_InventoryIn != null && objRT_Mst_InventoryIn.Lst_Mst_Inventory.Count > 0)
                {
                    List_Mst_InventoryIn.AddRange(objRT_Mst_InventoryIn.Lst_Mst_Inventory);
                }
                ViewBag.List_Mst_InventoryIn = List_Mst_InventoryIn;
                #endregion

                var ListInvCodeOut = new List<string>();
                foreach (var invOut in List_Mst_InventoryOut)
                {
                    if (!CUtils.IsNullOrEmpty(ListInvCodeOut))
                    {
                        if (!ListInvCodeOut.Contains(invOut.InvCode.ToString()))
                        {
                            ListInvCodeOut.Add(invOut.InvCode.ToString());
                        }
                    }
                }
                var strInvCodeOut = string.Join(",", ListInvCodeOut.ToArray());

                #region ["Product"]
                var productCodeBase = "";
                double curConvert = 1.0;
                if (!CUtils.IsNullOrEmpty(productcode))
                {
                    var strWhere_Mst_ProductUser = "";
                    var sbSQL = new StringBuilder();
                    var Tbl_Mst_Product = TableName.Mst_Product + ".";
                    sbSQL.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, productcode, "=");
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
                        productCodeBase = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCodeBase.ToString();
                        curConvert = Convert.ToDouble(objRT_Mst_ProductUser.Lst_Mst_Product[0].ValConvert);
                    }
                }
                #endregion

                #region Lấy thông tin tồn kho
                var strWhereClause = "";
                var sb_SQL = new StringBuilder();

                var Tbl_Inv_InventoryBalance = TableName.Inv_InventoryBalance + ".";
                if (productCodeBase != "")
                {
                    sb_SQL.AddWhereClause(Tbl_Inv_InventoryBalance + TblInv_InventoryBalance.ProductCode, productCodeBase, "=");
                }
                if (!CUtils.IsNullOrEmpty(strInvCodeOut))
                {
                    sb_SQL.AddWhereClause(Tbl_Inv_InventoryBalance + TblInv_InventoryBalance.InvCode, strInvCodeOut, "IN");
                }
                sb_SQL.AddWhereClause(Tbl_Inv_InventoryBalance + TblInv_InventoryBalance.QtyTotalOK, 0, ">");
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
                    var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance, x => x.QtyTotalOK);
                    foreach (var invBalance in lstInv_InventoryBalance_ProductCode_OrderByDesc)
                    {
                        var objInvBalance = new Inv_InventoryBalance
                        {
                            InvCode = invBalance.InvCode,
                            ProductCode = invBalance.ProductCode,
                            QtyTotalOK = Convert.ToDouble(invBalance.QtyTotalOK) / curConvert,
                        };
                        lstInv_InventoryBalance.Add(objInvBalance);
                    }
                }
                #endregion
                return JsonView("ShowPopupInvInOut", lstInv_InventoryBalance);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult ShowPopupLo(string productcode, string productname, string bupatternout, string bupatternin, string viewtype)
        {
            #region ["Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            ViewBag.ProductCode = productcode;
            ViewBag.ProductName = productname;
            ViewBag.viewtype = viewtype;
            #region ["Product"]
            var productCodeBase = "";
            double curConvert = 1.0;
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                var strWhere_Mst_ProductUser = "";
                var sbSQL = new StringBuilder();
                sbSQL.AddWhereClause("Mst_Product.ProductCode", productcode, "=");
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
                    productCodeBase = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCodeBase.ToString();
                    curConvert = Convert.ToDouble(objRT_Mst_ProductUser.Lst_Mst_Product[0].ValConvert);
                }
            }
            #endregion

            #region ["Danh sách kho"]
            var strWhereInvOut = "";
            var sbSqlOut = new StringBuilder();
            var TblMst_Inventory = "Mst_Inventory.";
            sbSqlOut.AddWhereClause(TblMst_Inventory + "FlagActive", Client_Flag.Active, "=");
            if (!CUtils.IsNullOrEmpty(bupatternout))
            {
                sbSqlOut.AddWhereClause(TblMst_Inventory + "InvBUPattern", bupatternout, "LIKE");
            }
            strWhereInvOut = sbSqlOut.ToString();

            var strWhereInvIn = "";
            var sbSqlIn = new StringBuilder();
            sbSqlIn.AddWhereClause(TblMst_Inventory + "FlagActive", Client_Flag.Active, "=");
            if (!CUtils.IsNullOrEmpty(bupatternin))
            {
                sbSqlIn.AddWhereClause(TblMst_Inventory + "InvBUPattern", bupatternin, "LIKE");
            }
            strWhereInvIn = sbSqlIn.ToString();

            var List_Mst_InventoryOut = new List<Mst_Inventory>();
            var List_Mst_InventoryIn = new List<Mst_Inventory>();
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*",
            };

            objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvOut;
            var objRT_Mst_InventoryOut = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_InventoryOut != null && objRT_Mst_InventoryOut.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryOut.AddRange(objRT_Mst_InventoryOut.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryOut = List_Mst_InventoryOut;

            objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvIn;
            var objRT_Mst_InventoryIn = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_InventoryIn != null && objRT_Mst_InventoryIn.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryIn.AddRange(objRT_Mst_InventoryIn.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryIn = List_Mst_InventoryIn;
            #endregion

            var ListInvCodeOut = new List<string>();
            foreach (var invOut in List_Mst_InventoryOut)
            {
                if (!CUtils.IsNullOrEmpty(ListInvCodeOut))
                {
                    if (!ListInvCodeOut.Contains(invOut.InvCode.ToString()))
                    {
                        ListInvCodeOut.Add(invOut.InvCode.ToString());
                    }
                }
            }

            var strInvCodeOut = string.Join(",", ListInvCodeOut.ToArray());

            var strWhereClause = "";
            var sb_SQL = new StringBuilder();
            var TblInv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
            if (!CUtils.IsNullOrEmpty(productCodeBase))
            {
                sb_SQL.AddWhereClause(TblInv_InventoryBalanceLot + "ProductCode", productCodeBase, "=");
            }
            if (!CUtils.IsNullOrEmpty(strInvCodeOut))
            {
                sb_SQL.AddWhereClause(TblInv_InventoryBalanceLot + "InvCode", strInvCodeOut, "IN");
            }
            sb_SQL.AddWhereClause(TblInv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
            strWhereClause = sb_SQL.ToString();

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
                    Ft_WhereClause = strWhereClause,

                    Rt_Cols_Inv_InventoryBalanceLot = "*",

                };
                var objRT_Inv_InventoryBalance = Inv_InventoryBalanceLotService.Instance.WA_Inv_InventoryBalanceLot_Get(objRQ_Inv_InventoryBalanceLot, addressAPIs);
                if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceLot.Count > 0)
                {
                    foreach (var invBalance in objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceLot)
                    {
                        var objInv_InventoryBalanceLot = new Inv_InventoryBalanceLot()
                        {
                            ProductCode = invBalance.ProductCode,
                            ProductionDate = invBalance.ProductionDate,
                            ProductLotNo = invBalance.ProductLotNo,
                            ExpiredDate = invBalance.ExpiredDate,
                            InvCode = invBalance.InvCode,
                            QtyTotalOK = Convert.ToDouble(invBalance.QtyTotalOK) / curConvert
                        };
                        List_Inv_InventoryBalanceLot.Add(objInv_InventoryBalanceLot);
                    }
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

        public ActionResult ShowPopupSerial(string productcode, string productname, string bupatternout, string bupatternin)
        {
            #region ["Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            ViewBag.ProductCode = productcode;
            ViewBag.ProductName = productname;
            #region ["Danh sách kho nhập"]
            var strWhereInvOut = "";
            var sbSqlOut = new StringBuilder();
            var TblMst_Inventory = "Mst_Inventory.";
            if (!CUtils.IsNullOrEmpty(bupatternout))
            {
                sbSqlOut.AddWhereClause(TblMst_Inventory + "InvBUPattern", bupatternout, "LIKE");
                sbSqlOut.AddWhereClause(TblMst_Inventory + "FlagActive", FlagActive, "=");
            }
            strWhereInvOut = sbSqlOut.ToString();

            var strWhereInvIn = "";
            var sbSqlIn = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(bupatternin))
            {
                sbSqlIn.AddWhereClause(TblMst_Inventory + "InvBUPattern", bupatternin, "LIKE");
                sbSqlIn.AddWhereClause(TblMst_Inventory + "FlagActive", FlagActive, "=");
            }
            strWhereInvIn = sbSqlIn.ToString();

            var List_Mst_InventoryOut = new List<Mst_Inventory>();
            var List_Mst_InventoryIn = new List<Mst_Inventory>();
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Inventory = "*",
            };

            objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvOut;
            var objRT_Mst_InventoryOut = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_InventoryOut != null && objRT_Mst_InventoryOut.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryOut.AddRange(objRT_Mst_InventoryOut.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryOut = List_Mst_InventoryOut;

            objRQ_Mst_Inventory.Ft_WhereClause = strWhereInvIn;
            var objRT_Mst_InventoryIn = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (objRT_Mst_InventoryIn != null && objRT_Mst_InventoryIn.Lst_Mst_Inventory.Count > 0)
            {
                List_Mst_InventoryIn.AddRange(objRT_Mst_InventoryIn.Lst_Mst_Inventory);
            }
            ViewBag.List_Mst_InventoryIn = List_Mst_InventoryIn;
            #endregion

            #region ["Product"]
            var productCodeBase = "";
            double rootConvert = 0;
            double curConvert = 0;
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                var strWhere_Mst_ProductUser = "";
                var sbSQL = new StringBuilder();
                sbSQL.AddWhereClause("Mst_Product.ProductCode", productcode, "=");
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
                    productCodeBase = objRT_Mst_ProductUser.Lst_Mst_Product[0].ProductCodeBase.ToString();
                }
            }
            #endregion

            var ListInvCodeOut = new List<string>();
            foreach (var invOut in List_Mst_InventoryOut)
            {
                if (!CUtils.IsNullOrEmpty(ListInvCodeOut))
                {
                    if (!ListInvCodeOut.Contains(invOut.InvCode.ToString()))
                    {
                        ListInvCodeOut.Add(invOut.InvCode.ToString());
                    }
                }
            }

            var strInvCodeOut = string.Join(",", ListInvCodeOut.ToArray());

            var strWhereClause = "";
            var sb_SQL = new StringBuilder();
            var TblInv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
            if (!CUtils.IsNullOrEmpty(productCodeBase))
            {
                sb_SQL.AddWhereClause(TblInv_InventoryBalanceSerial + "ProductCode", productCodeBase, "=");
            }
            if (!CUtils.IsNullOrEmpty(strInvCodeOut))
            {
                sb_SQL.AddWhereClause(TblInv_InventoryBalanceSerial + "InvCode", strInvCodeOut, "IN");
            }
            strWhereClause = sb_SQL.ToString();

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
                    Ft_WhereClause = strWhereClause,

                    Rt_Cols_Inv_InventoryBalanceSerial = "*",

                };
                var objRT_Inv_InventoryBalanceSerial = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                if (objRT_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                {
                    List_Inv_InventoryBalanceSerial.AddRange(objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial);
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

        #region ["strWhereClause"]
        private string strWhereClause_InvF_MoveOrd(string IF_MONo = "", string CreateTimeFrom = "", string CreateTimeTo = "", string InvCodeOut = "", string ApprTimeFrom = "",
            string ApprTimeTo = "", string InvCodeIn = "", string Remark = "", string chkPending = "", string chkApproved = "", string chkCanceled = "")
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_MoveOrd = TableName.InvF_MoveOrd + ".";
            if (!CUtils.IsNullOrEmpty(IF_MONo))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.IF_MONo, CUtils.StrValue(IF_MONo), "IN");
            }
            if (!CUtils.IsNullOrEmpty(CreateTimeFrom))
            {
                var createDTimeUTCFrom = CUtils.StrValue(CreateTimeFrom) + " 00:00:00";
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(CreateTimeTo))
            {
                var createDTimeUTCTo = CUtils.StrValue(CreateTimeTo) + " 23:59:59";
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(createDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(InvCodeOut))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.InvCodeOut, CUtils.StrValue(InvCodeOut), "=");
            }
            if (!CUtils.IsNullOrEmpty(ApprTimeFrom))
            {
                var apprDTimeUTCFrom = CUtils.StrValue(ApprTimeFrom) + " 00:00:00";
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCFrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(ApprTimeTo))
            {
                var apprDTimeUTCTo = CUtils.StrValue(ApprTimeTo) + " 23:59:59";
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(apprDTimeUTCTo, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.ApprDTimeUTC, strApprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(InvCodeIn))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.InvCodeIn, CUtils.StrValue(InvCodeIn), "=");
            }
            if (!CUtils.IsNullOrEmpty(Remark))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.Remark, string.Format("%{0}%", CUtils.StrValue(Remark)), "LIKE");
            }
            var status = "";
            if (!CUtils.IsNullOrEmpty(chkPending))
            {
                if (chkPending.Equals("1"))
                {
                    status += "PENDING";
                }

            }
            if (!CUtils.IsNullOrEmpty(chkApproved) && chkApproved.Equals("1"))
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
            if (!CUtils.IsNullOrEmpty(chkCanceled) && chkCanceled.Equals("1"))
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
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.IF_MOStatus, status, "in");
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
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValue(productcode), "=", "OR");
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, CUtils.StrValue(productcode), "=", "OR");
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(productcode), "=", "OR");
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "like");
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(productcode) + "%", "like", "OR");
                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, "%" + CUtils.StrValue(productcode) + "%", "like", "OR");
                sbSql.Append(string.Format("({0})", sbSqlProductCode));
            }
            if (!CUtils.IsNullOrEmpty(flagactive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, CUtils.StrValue(flagactive), "=");
            }
            if (!CUtils.IsNullOrEmpty(producttype))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, CUtils.StrValue(producttype), "=");
            }
            if (!CUtils.IsNullOrEmpty(flagsell))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, CUtils.StrValue(flagsell), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_InvF_MoveOrdById(string if_mono)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_MoveOrd = TableName.InvF_MoveOrd + ".";
            if (!CUtils.IsNullOrEmpty(if_mono))
            {
                sbSql.AddWhereClause(Tbl_InvF_MoveOrd + TblInvF_MoveOrd.IF_MONo, CUtils.StrValue(if_mono), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Inventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagIn_Out))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagIn_Out, CUtils.StrValue(data.FlagIn_Out), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvCode, CUtils.StrValue(data.InvCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvType))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvType, CUtils.StrValue(data.InvType), "=");
            }

            strWhereClause = sbSql.ToString();
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
        #endregion

        #region ["Tìm kiếm hàng hóa nhanh"]
        [HttpPost]
        public ActionResult SearchProductOnKeyup(string productcode, int recordcount = 10, int page = 0)
        {
            #region ["Product Center Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            var strWhereClause = "";
            var strWherePrdCode = "";
            var sbSql = new StringBuilder();
            var sbSqlPrdCode = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagSell, "1", "=");
            //sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", "=");
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                sbSqlPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" + CUtils.StrValue(productcode) + "%", "LIKE");
                sbSqlPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, "%" + CUtils.StrValue(productcode) + "%", "LIKE", "OR");
                sbSqlPrdCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, "%" + CUtils.StrValue(productcode) + "%", "LIKE", "OR");
                strWherePrdCode = sbSqlPrdCode.ToString();
            }
            strWhereClause = sbSql.ToString();
            if (strWherePrdCode.Length > 0)
            {
                strWhereClause = strWhereClause + " AND (" + strWherePrdCode + ")";
            }

            try
            {
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,

                    Rt_Cols_Mst_Product = "*"
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Product);
                var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);

                if (objRT_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
                {
                    List_Mst_Product.AddRange(objRT_Mst_Product.Lst_Mst_Product);
                }
                return Json(new { Success = true, Data = List_Mst_Product });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #endregion

        #region ["Tìm theo mã vạch ProductBarCode"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProductFromPrdBarCode(string productbarcode, string bupatternout)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new Inv_InventoryBalance();
            var lstPhanBo = new List<Inv_InventoryBalance>();
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            try
            {
                #region["Danh sách hàng hoá cùng base"]
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!CUtils.IsNullOrEmpty(productbarcode))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductBarCode", CUtils.StrValue(productbarcode), "=");
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
                    #region ["Danh sách hàng cùng base"]
                    var strWhereProductBase = "";
                    var sbSqlProductBase = new StringBuilder();
                    if (!CUtils.IsNullOrEmpty(objRT_Mst_Product.Lst_Mst_Product[0].ProductCodeBase))
                    {
                        sbSqlProductBase.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(objRT_Mst_Product.Lst_Mst_Product[0].ProductCodeBase), "=");
                    }
                    strWhereProductBase = sbSqlProductBase.ToString();
                    var RQ_Mst_ProductBase = new RQ_Mst_Product()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        Ft_WhereClause = strWhereProductBase,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Mst_Product = "*",
                    };
                    var objRT_Mst_ProductBase = Mst_ProductService.Instance.WA_Mst_Product_Get(RQ_Mst_ProductBase, addressAPIs);
                    if(objRT_Mst_ProductBase!=null && objRT_Mst_ProductBase.Lst_Mst_Product!=null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
                    {
                        #region["Lấy thông tin tồn kho"]
                        var listProductCode = objRT_Mst_ProductBase.Lst_Mst_Product.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
                        if (listProductCode != null && listProductCode.Count > 0)
                        {
                            listProductCode = listProductCode.Distinct().ToList();
                            var strProductCode = string.Join(",", listProductCode);

                            var strWhereClause = "";
                            var sb_SQL1 = new StringBuilder();
                            var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                            if (!CUtils.IsNullOrEmpty(strProductCode))
                            {
                                sb_SQL1.AddWhereClause(tblInv_InventoryBalance + "ProductCode", strProductCode, "IN");
                            }
                            if (!CUtils.IsNullOrEmpty(bupatternout))
                            {
                                sb_SQL1.AddWhereClause("Mst_Inventory.InvBUCode", bupatternout, "LIKE");
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
                            if (objRT_Inv_InventoryBalance1 != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance.Count > 0)
                            {
                                lstInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance;
                            }
                            foreach (var it in objRT_Mst_ProductBase.Lst_Mst_Product)
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
                                productUI.ProductBarCode = it.ProductBarCode;

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
                                if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                                {
                                    if (!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                                    {
                                        var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                                        if (lstProductByCode != null && lstProductByCode.Count > 0)
                                        {
                                            var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                            //sắp xếp giảm dần
                                            var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                            lstPhanBo = lstInv_InventoryBalance_ProductCode_OrderByDesc;
                                            double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                            qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                            if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                            {
                                                productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                                        if (lstProductByCode != null && lstProductByCode.Count > 0)
                                        {
                                            var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                            //sắp xếp giảm dần
                                            var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                            lstPhanBo = lstInv_InventoryBalance_ProductCode_OrderByDesc;
                                            double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                            qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                            if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                            {
                                                productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                            }
                                        }
                                    }
                                }
                                lst_Mst_ProductUI.Add(productUI);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                return Json(new { Success = true, Data = lst_Mst_ProductUI, ListPhanBo = lstPhanBo });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetInventoryBalance(string productcode, string productcodebase, string valconvert, string bupatternout)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new Inv_InventoryBalance();
            var lstPhanBo = new List<Inv_InventoryBalance>();
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };
            try
            {
                #region["Danh sách hàng hoá cùng base"]
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Mst_Product = "Mst_Product.";
                if (!CUtils.IsNullOrEmpty(productcodebase))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(productcodebase), "=");
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
                        var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                        if (!CUtils.IsNullOrEmpty(strProductCode))
                        {
                            sb_SQL1.AddWhereClause(tblInv_InventoryBalance + "ProductCode", strProductCode, "IN");
                        }
                        if (!CUtils.IsNullOrEmpty(bupatternout))
                        {
                            sb_SQL1.AddWhereClause("Mst_Inventory.InvBUCode", bupatternout, "LIKE");
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
                        if (objRT_Inv_InventoryBalance1 != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance.Count > 0)
                        {
                            lstInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance;
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
                            if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                            {
                                if(!CUtils.IsNullOrEmpty(it.ProductCodeBase) && !it.ProductCodeBase.Equals(it.ProductCode))//sản phẩm phái sinh
                                {
                                    var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();//tồn kho với mã hàng base của sản phẩm phái sinh
                                    if (lstProductByCode != null && lstProductByCode.Count > 0)
                                    {
                                        var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCodeBase)).ToList();
                                        //sắp xếp giảm dần
                                        var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                        lstPhanBo = lstInv_InventoryBalance_ProductCode_OrderByDesc;
                                        double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                        qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                        if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                        {
                                            productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax) / Convert.ToDouble(it.ValConvert);                                            
                                        }
                                    }
                                }
                                else
                                {
                                    var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();//tồn kho với mã hàng base
                                    if (lstProductByCode != null && lstProductByCode.Count > 0)
                                    {
                                        var lstInv_InventoryBalance_ProductCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                                        //sắp xếp giảm dần
                                        var lstInv_InventoryBalance_ProductCode_OrderByDesc = CUtils.OrderByDesc(lstInv_InventoryBalance_ProductCode, x => x.QtyTotalOK);
                                        lstPhanBo = lstInv_InventoryBalance_ProductCode_OrderByDesc;
                                        double qtyTotalOKMax = 0;//tổng tồn kho của hàng hóa
                                        qtyTotalOKMax = lstInv_InventoryBalance_ProductCode_OrderByDesc.Sum(x => (x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK))); ;

                                        if (!CUtils.IsNullOrEmpty(qtyTotalOKMax))
                                        {
                                            productUI.QtyTotalOK = Convert.ToDouble(qtyTotalOKMax);
                                        }
                                    }
                                }
                            }
                            lst_Mst_ProductUI.Add(productUI);
                        }

                    }
                    #endregion
                }
                #endregion

                return Json(new { Success = true, Data = lst_Mst_ProductUI, ListPhanBo = lstPhanBo });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
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

                if (listMst_Product != null && listMst_Product.Count > 0)
                {
                    objMst_Product = listMst_Product[0];
                    var listProduct = new List<Mst_Product>();
                    listProduct.Add(objMst_Product);
                    #region["Danh sách hàng hóa cùng base"]
                    var strWhereClause_MstProductBase = strWhereClause_Mst_Product_Get_Base(objMst_Product);
                    var objRT_Mst_ProductBase = GetPrdBase(strWhereClause_MstProductBase);
                    if (objRT_Mst_ProductBase.Lst_Mst_Product != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
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
        private string strWhereGetProductId(Mst_Product data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";

            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagActive, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.FlagBuy, "1", "=");
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", " = ");

            if (!CUtils.IsNullOrEmpty(data.ProductCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(data.ProductCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #region ["Print"]
        [HttpPost]
        public ActionResult PrintTemp(string if_mono)
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

                InvF_MoveOrd objInvF_MoveOrd = new InvF_MoveOrd();
                var strWhere = strWhereClause_InvF_MoveOrdById(if_mono);
                var objRQ_InvF_MoveOrd = new RQ_InvF_MoveOrd()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_MoveOrd = "*",
                    Rt_Cols_InvF_MoveOrdDtl = "*",
                    Rt_Cols_InvF_MoveOrdInstLot = "",
                    Rt_Cols_InvF_MoveOrdInstSerial = "",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_MoveOrd = InvF_MoveOrdService.Instance.WA_InvF_MoveOrd_Get(objRQ_InvF_MoveOrd, addressAPIs);
                if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrd.Count != 0)
                {
                    objInvF_MoveOrd = objRT_InvF_MoveOrd.Lst_InvF_MoveOrd[0];
                }

                #region Get List đơn vị tính theo Product + Xử lý list detail UI
                var listInvF_MoveOrdDtlUI = new List<InvF_MoveOrdDtlUI>();

                var Lst_InvF_MoveOrdDtl = new List<InvF_MoveOrdDtl>();
                if (objRT_InvF_MoveOrd != null && objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl != null)
                {
                    Lst_InvF_MoveOrdDtl = objRT_InvF_MoveOrd.Lst_InvF_MoveOrdDtl;
                }

                var lstPrdDistinct = new List<string>();
                int idx = 1;
                foreach (var item in Lst_InvF_MoveOrdDtl)
                {
                    var itemUI = new InvF_MoveOrdDtlUI()
                    {
                        IF_MONo = item.IF_MONo,
                        Dtl_InvNameIn = item.InvCodeIn,
                        Dtl_InvNameOut = item.InvCodeOut,
                        NetworkID = item.NetworkID,
                        Qty = item.Qty,
                        ProductCodeUser = item.mp_ProductCodeUser,
                        ProductName = item.mp_ProductName,
                        UnitCode = item.UnitCode,
                        Dtl_Remark = item.Remark,
                        Idx = idx
                    };

                    //
                    listInvF_MoveOrdDtlUI.Add(itemUI);
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

                InvF_MoveOrdPrint objPrint = new InvF_MoveOrdPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.IF_MONo = objInvF_MoveOrd.IF_MONo;

                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.Remark = objInvF_MoveOrd.Remark;
                objPrint.TotalQty = listInvF_MoveOrdDtlUI.Sum(m => Convert.ToDouble(m.Qty));
                objPrint.InvNameIn = objInvF_MoveOrd.InvCodeIn;
                objPrint.InvNameOut = objInvF_MoveOrd.InvCodeOut;
                objPrint.CreateUserName = objInvF_MoveOrd.CreateBy;
                objPrint.ApprUserName = objInvF_MoveOrd.ApprBy;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_MoveOrdDtlUI = listInvF_MoveOrdDtlUI;

                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("DC");
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


        public dynamic CreateDataObjectReportServer(InvF_MoveOrdPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_MoveOrdDtl;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new InvF_MoveOrdReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.NNTFullName);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.IF_MONo = CUtils.StrValueNew(objTempPrint.IF_MONo);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.InvNameIn = CUtils.StrValueNew(objTempPrint.InvNameIn);
                MyDynamic.InvNameOut = CUtils.StrValueNew(objTempPrint.InvNameOut);
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.TotalQty = CUtils.StrValueFormatNumber(objTempPrint.TotalQty, NumericFormat(tableName, "TotalQty", defaultFormat));
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.ApprUserName = CUtils.StrValueNew(objTempPrint.ApprUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_MoveOrdDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_InvF_MoveOrdDtlUI != null && objTempPrint.Lst_InvF_MoveOrdDtlUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_MoveOrdDtlReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_MoveOrdDtlUI)
                {
                    var objDtl_ReportServer = new InvF_MoveOrdDtlReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.ProductCodeUser),
                        ProductName = CUtils.StrValue(item.ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        Dtl_InvNameOut = CUtils.StrValue(item.Dtl_InvNameOut),
                        Dtl_InvNameIn = CUtils.StrValue(item.Dtl_InvNameIn),
                        Qty = CUtils.StrValueFormatNumber(item.Qty, NumericFormat(tableName, "Qty", defaultFormat)),
                        Dtl_Remark = CUtils.StrValue(item.Dtl_Remark),
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