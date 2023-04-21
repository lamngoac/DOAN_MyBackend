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
    public class Mst_CustomerSourceController : AdminBaseController
    {
        // GET: Mst_CustomerSource

        #region Index
        public ActionResult Index(string customerSourceName, string customerSourceDesc, string init = "init", int recordcount = 10, int page = 0)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_NGUON_KHACH_HANG");
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
            var pageInfo = new PageInfo<Mst_CustomerSource>(0, recordcount)
            {
                DataList = new List<Mst_CustomerSource>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_CustomerSource = new Mst_CustomerSource()
                {
                    CustomerSourceName = customerSourceName,
                    CustomerSourceDesc = customerSourceDesc
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSource);

                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
                {
                    // WARQBase
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
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };

                var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                if (objRT_Mst_CustomerSourceCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_CustomerSourceCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_CustomerSourceCur != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.CustomerSourceName = customerSourceName;
            ViewBag.CustomerSourceDesc = customerSourceDesc;
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
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUON_NGUOI_DUNG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.ViewType = "create";
            var objMst_CustomerSource = new Mst_CustomerSource();
            return JsonView("Mst_CustomerSource_Info", objMst_CustomerSource);
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
            var objMst_CustomerSource = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerSource>(model);
            objMst_CustomerSource.CustomerSourceCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerSourceCode);
            objMst_CustomerSource.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
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
                // RQ_Mst_CustomerSource
                Rt_Cols_Mst_CustomerSource = null,
                Mst_CustomerSource = objMst_CustomerSource,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource);
            Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Create(objRQ_Mst_CustomerSource, UserState.AddressAPIs_Product_Customer_Centrer);

            resultEntry.Success = true;
            resultEntry.AddMessage("Tạo nhóm khách hàng thành công!");
            resultEntry.RedirectUrl = Url.Action("Index");
            return Json(resultEntry);
        }
        #endregion

        #region Cập nhật
        [HttpGet]
        public ActionResult Update(string customerSourceCode)
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_NGUON_KHACH_HANG_SUA");
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

            if (!CUtils.IsNullOrEmpty(customerSourceCode))
            {
                var strWhereClause = "";
                var objMst_CustomerSource = new Mst_CustomerSource()
                {
                    CustomerSourceCode = customerSourceCode
                };
                strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSource);

                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
                {
                    // WARQBase
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
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };

                var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                if (objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                {
                    return JsonView("Mst_CustomerSource_Info", objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0]);
                }
            }
            return JsonView(new Mst_CustomerSource());
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
            var objMst_CustomerSource = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerSource>(model);

            if (model != null && !CUtils.IsNullOrEmpty(objMst_CustomerSource.CustomerSourceCode))
            {
                var strWhereClause = "";
                var objMst_CustomerSourceCur = new Mst_CustomerSource()
                {
                    CustomerSourceCode = objMst_CustomerSource.CustomerSourceCode
                };
                strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSourceCur);
                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
                {
                    // WARQBase
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
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };
                var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                if (objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                {
                    objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0].CustomerSourceName = objMst_CustomerSource.CustomerSourceName;
                    objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0].CustomerSourceDesc = objMst_CustomerSource.CustomerSourceDesc;
                    objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0].FlagActive = objMst_CustomerSource.FlagActive;
                    objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0].OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_CustomerSource = TableName.Mst_CustomerSource + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerSource, TblMst_CustomerSource.CustomerSourceName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerSource, TblMst_CustomerSource.CustomerSourceDesc);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_CustomerSource, TblMst_CustomerSource.FlagActive);

                    objRQ_Mst_CustomerSource.Ft_WhereClause = null;
                    objRQ_Mst_CustomerSource.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSource = null;
                    objRQ_Mst_CustomerSource.GwUserCode = OS_ProductCentrer_GwUserCode;
                    objRQ_Mst_CustomerSource.GwPassword = OS_ProductCentrer_GwPassword;
                    objRQ_Mst_CustomerSource.Mst_CustomerSource = objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0];
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource);
                    Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Update(objRQ_Mst_CustomerSource, UserState.AddressAPIs_Product_Customer_Centrer);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa nhóm khách hàng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm khách hàng '" + objMst_CustomerSource.CustomerSourceName + "' không có trong hệ thống!");
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

        #region Xóa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string customerSourceCode = "")
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
                if (!CUtils.IsNullOrEmpty(customerSourceCode))
                {
                    var strWhereClause = "";
                    var objMst_CustomerSource = new Mst_CustomerSource()
                    {
                        CustomerSourceCode = customerSourceCode
                    };
                    strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSource);

                    var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
                    {
                        // WARQBase
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
                        // RQ_Mst_CustomerSource
                        Rt_Cols_Mst_CustomerSource = "*",
                        Mst_CustomerSource = null,
                    };

                    var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                    if (objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                    {
                        objRQ_Mst_CustomerSource.Ft_WhereClause = null;
                        objRQ_Mst_CustomerSource.Ft_Cols_Upd = null;
                        objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSource = null;
                        objRQ_Mst_CustomerSource.Mst_CustomerSource = objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0];
                        objRQ_Mst_CustomerSource.GwUserCode = OS_ProductCentrer_GwUserCode;
                        objRQ_Mst_CustomerSource.GwPassword = OS_ProductCentrer_GwPassword;
                        Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Delete(objRQ_Mst_CustomerSource, UserState.AddressAPIs_Product_Customer_Centrer);
                        title = "Xóa nguồn khách hàng '" + customerSourceCode + "' thành công!";

                        resultEntry.Success = true;
                        resultEntry.AddMessage(title);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        title = "Mã nguồn khách hàng '" + customerSourceCode + "' không có trong hệ thống!";
                        resultEntry.AddMessage(title);
                    }
                }
                else
                {
                    title = "Mã nguồn khách hàng trống!";
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

        #region["Load Detail"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDetailData(string customerSourceCode)
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
                var objMst_CustomerSource = new Mst_CustomerSource()
                {
                    CustomerSourceCode = customerSourceCode
                };
                strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSource);

                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
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
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };

                var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                if (objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                {
                    objMst_CustomerSource = objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource[0];
                }
                return JsonView("Mst_CustomerSource_Info", objMst_CustomerSource);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Mst_CustomerSource_Info", null, resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string customerSourceName, string customerSourceDesc)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var list = new List<Mst_CustomerSource>();
                var objMst_CustomerSource = new Mst_CustomerSource()
                {
                    CustomerSourceName = customerSourceName,
                    CustomerSourceDesc = customerSourceDesc
                };
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_CustomerSource(objMst_CustomerSource);
                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
                {
                    // WARQBase
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
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };

                var objRT_Mst_CustomerSourceCur = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
                if (objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource.Count > 0)
                {
                    list.AddRange(objRT_Mst_CustomerSourceCur.Lst_Mst_CustomerSource);

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_CustomerSource).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_CustomerSource"));

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

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"CustomerSourceName","Mã nhóm khách hàng"},
                 {"CustomerSourceDesc","Tên nhóm khách hàng"},
            };
        }

        #endregion

        #region strWhereClause
        private string strWhereClause_Mst_CustomerSource(Mst_CustomerSource data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerSource = TableName.Mst_CustomerSource + ".";
            if (!CUtils.IsNullOrEmpty(data.CustomerSourceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.CustomerSourceCode, data.CustomerSourceCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerSourceName))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.CustomerSourceName, "%" + data.CustomerSourceName.ToString().Trim() + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerSourceDesc))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.CustomerSourceDesc, "%" + data.CustomerSourceDesc.ToString().Trim() + "%", "like");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}