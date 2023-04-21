using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
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
    public class Mst_CustomerGroupController : AdminBaseController
    {
        // GET: Mst_CustomerGroup

        #region Index
        public ActionResult Index(string customerGrpName, string customerGrpDesc, string init = "init", int recordcount = 10, int page = 0)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_NHOM_KHACH_HANG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.PageCur = page.ToString();
            ViewBag.Recordcount = recordcount;

            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.CurrentUser = userState.SysUser;
            ViewBag.UserState = this.UserState;
            var pageInfo = new PageInfo<Mst_CustomerGroup>(0, recordcount)
            {
                DataList = new List<Mst_CustomerGroup>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_CustomerGroup = new Mst_CustomerGroup()
                {
                    CustomerGrpName = customerGrpName,
                    CustomerGrpDesc = customerGrpDesc
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroup);

                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                {
                    // WARQBase
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
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };

                var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                if (objRT_Mst_CustomerGroupCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_CustomerGroupCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_CustomerGroupCur != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.CustomerGrpName = customerGrpName;
            ViewBag.CustomerGrpDesc = customerGrpDesc;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }
        #endregion

        #region Tạo mới
        [HttpGet]
        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NHOM_NGUOI_DUNG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.ViewType = "create";
            var objMst_CustomerGroup = new Mst_CustomerGroup();
            return JsonView("Mst_CustomerGroup_Info", objMst_CustomerGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objMst_CustomerGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerGroup>(model);
            objMst_CustomerGroup.CustomerGrpCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerGrpCode);
            objMst_CustomerGroup.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
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
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerGroup = null,
                Mst_CustomerGroup = objMst_CustomerGroup,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup);
            Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Create(objRQ_Mst_CustomerGroup, UserState.AddressAPIs_Product_Customer_Centrer);

            resultEntry.Success = true;
            resultEntry.AddMessage("Tạo nhóm khách hàng thành công!");
            resultEntry.RedirectUrl = Url.Action("Index");
            return Json(resultEntry);
        }
        #endregion

        #region Cập nhật
        [HttpGet]
        public ActionResult Update(string customerGrpCode)
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NHOM_KHACH_HANG_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.ViewType = "edit";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(customerGrpCode))
            {
                var strWhereClause = "";
                var objMst_CustomerGroup = new Mst_CustomerGroup()
                {
                    CustomerGrpCode = customerGrpCode
                };
                strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroup);

                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };

                var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                if (objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                {
                    return JsonView("Mst_CustomerGroup_Info", objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0]);
                }
            }
            return JsonView(new Mst_CustomerGroup());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objMst_CustomerGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerGroup>(model);

            if (model != null && !CUtils.IsNullOrEmpty(objMst_CustomerGroup.CustomerGrpCode))
            {
                var strWhereClause = "";
                var objMst_CustomerGroupCur = new Mst_CustomerGroup()
                {
                    CustomerGrpCode = objMst_CustomerGroup.CustomerGrpCode
                };
                strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroupCur);
                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };
                var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                if (objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                {
                    objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0].CustomerGrpName = objMst_CustomerGroup.CustomerGrpName;
                    objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0].CustomerGrpDesc = objMst_CustomerGroup.CustomerGrpDesc;
                    objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0].FlagActive = objMst_CustomerGroup.FlagActive;
                    objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0].OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_CustomerGroup = TableName.Mst_CustomerGroup + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerGroup, TblMst_CustomerGroup.CustomerGrpName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerGroup, TblMst_CustomerGroup.CustomerGrpDesc);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerGroup, TblMst_CustomerGroup.FlagActive);

                    objRQ_Mst_CustomerGroup.Ft_WhereClause = null;
                    objRQ_Mst_CustomerGroup.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroup = null;
                    objRQ_Mst_CustomerGroup.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_CustomerGroup.GwPassword = OS_ProductCentrer_GwPassword;
                    objRQ_Mst_CustomerGroup.Mst_CustomerGroup = objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0];
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup);
                    Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Update(objRQ_Mst_CustomerGroup, UserState.AddressAPIs_Product_Customer_Centrer);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa nhóm khách hàng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm khách hàng '" + objMst_CustomerGroup.CustomerGrpName + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã nhóm khách hàng trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Load Detail"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDetailData(string customerGrpCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                ViewBag.ViewType = "detail";
                var strWhereClause = "";
                var objMst_CustomerGroup = new Mst_CustomerGroup()
                {
                    CustomerGrpCode = customerGrpCode
                };
                strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroup);

                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };

                var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                if (objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                {
                    objMst_CustomerGroup = objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0];
                }
                return JsonView("Mst_CustomerGroup_Info", objMst_CustomerGroup);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Mst_CustomerGroup_Info", null, resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string customerGrpName, string customerGrpDesc)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var list = new List<Mst_CustomerGroup>();
                var objMst_CustomerGroup = new Mst_CustomerGroup()
                {
                    CustomerGrpName = customerGrpName,
                    CustomerGrpDesc = customerGrpDesc
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroup);
                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                {
                    // WARQBase
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };

                var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                if (objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                {
                    list.AddRange(objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup);

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_CustomerGroup).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_CustomerGroup"));

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

        #endregion

        #region Xóa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string customerGrpCode = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                var title = "";
                if (!CUtils.IsNullOrEmpty(customerGrpCode))
                {
                    var strWhereClause = "";
                    var objMst_CustomerGroup = new Mst_CustomerGroup()
                    {
                        CustomerGrpCode = customerGrpCode
                    };
                    strWhereClause = strWhereClause_Mst_CustomerGroup(objMst_CustomerGroup);

                    var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
                    {
                        // WARQBase
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
                        Ft_WhereClause = strWhereClause,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_CustomerGroup
                        Rt_Cols_Mst_CustomerGroup = "*",
                        Mst_CustomerGroup = null,
                    };

                    var objRT_Mst_CustomerGroupCur = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
                    if (objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup.Count > 0)
                    {
                        objRQ_Mst_CustomerGroup.Ft_WhereClause = null;
                        objRQ_Mst_CustomerGroup.Ft_Cols_Upd = null;
                        objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroup = null;
                        objRQ_Mst_CustomerGroup.Mst_CustomerGroup = objRT_Mst_CustomerGroupCur.Lst_Mst_CustomerGroup[0];
                        objRQ_Mst_CustomerGroup.GwUserCode = OS_ProductCentrer_GwUserCode;
                        objRQ_Mst_CustomerGroup.GwPassword = OS_ProductCentrer_GwPassword;
                        Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Delete(objRQ_Mst_CustomerGroup, UserState.AddressAPIs_Product_Customer_Centrer);
                        title = "Xóa nhóm khách hàng '" + customerGrpCode + "' thành công!";

                        resultEntry.Success = true;
                        resultEntry.AddMessage(title);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        title = "Mã nhóm khách hàng '" + customerGrpCode + "' không có trong hệ thống!";
                        resultEntry.AddMessage(title);
                    }
                }
                else
                {
                    title = "Mã nhóm khách hàng trống!";
                    resultEntry.AddMessage(title);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);

        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"CustomerGrpName","Mã nhóm khách hàng"},
                 {"CustomerGrpDesc","Tên nhóm khách hàng"},
            };
        }

        #endregion

        #region strWhereClause
        private string strWhereClause_Mst_CustomerGroup(Mst_CustomerGroup data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerGroup = TableName.Mst_CustomerGroup + ".";
            if (!CUtils.IsNullOrEmpty(data.CustomerGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.CustomerGrpCode, data.CustomerGrpCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerGrpName))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.CustomerGrpName, "%" + data.CustomerGrpName.ToString().Trim() + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerGrpDesc))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.CustomerGrpDesc, "%" + data.CustomerGrpDesc.ToString().Trim() + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}