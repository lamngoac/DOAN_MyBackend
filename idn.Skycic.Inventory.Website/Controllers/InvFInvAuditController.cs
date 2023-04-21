using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class InvFInvAuditController : AdminBaseController
    {
        // GET: InvFInvAudit
        public ActionResult Index(string IF_InvAudNo, string createdtimefrom, string createdtimeto, string approvedtimefrom, string approvedtimeto, string InvCodeAudit, int page = 0, int recordcount = 10, string init = "init")
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVAUDIT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<InvF_InvAudit>(0, recordcount)
            {
                DataList = new List<InvF_InvAudit>()
            };
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
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
                #region Thông tin kho xuất
                //var lst_Mst_Inventory = new List<Mst_Inventory>();
                //var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                //    Rt_Cols_Mst_Inventory = "*",
                //    Ft_RecordStart = Ft_RecordStart,
                //    Ft_RecordCount = Ft_RecordCount
                //};
                //var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                //if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                //{
                //    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                //}
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.MSTBUPattern like '" + CUtils.StrValue(UserState.Mst_NNT.MSTBUPattern) + "' and Mst_NNT.FlagActive = '1'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);

                ViewBag.ListMst_NNT = listMst_NNT;
                #endregion

                //var pageInfo = new PageInfo<InvF_InvAudit>(0, recordcount)
                //{
                //    DataList = new List<InvF_InvAudit>()
                //};
                //InvF_InvAudit objInvF_InvAuditSearch = new InvF_InvAudit
                //{
                //    InvCodeAudit = InvCodeAudit,
                //    IF_InvAudNo = IF_InvAudNo
                //};
                //var strWhere = WhereClause(objInvF_InvAuditSearch, createdtimefrom, createdtimeto, approvedtimefrom, approvedtimeto);
                //var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
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
                //    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                //    Ft_RecordCount = recordcount.ToString(),
                //    Rt_Cols_InvF_InvAudit = "*",
                //    Ft_WhereClause = strWhere
                //};
                //var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);

                //if (objRT_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAudit.Count > 0)
                //{
                //    pageInfo.DataList = objRT_InvF_InvAudit.Lst_InvF_InvAudit;
                //    itemCount = objRT_InvF_InvAudit.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InvAudit.MySummaryTable.MyCount);
                //    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                //}

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                return View(pageInfo);
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
        public ActionResult DoSearch
            (
                string if_InvAudNo = "",
                string createdtimefrom = "",
                string createdtimeto = "",
                string invCodeAudit = "",
                string approvedtimefrom = "",
                string approvedtimeto = "",
                string productcode = "",
                string orgid = "",
                string chkPending = "",
                string chkApproved = "",
                string chkCanceled = "",
                string chkFinished = "",
                int recordcount = 50,
                int page = 0
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
                var pageInfo = new PageInfo<InvF_InvAudit>(0, recordcount)
                {
                    DataList = new List<InvF_InvAudit>()
                };
                var strWhereClause = strWhereClause_InvFInvAudit_Search(if_InvAudNo, createdtimefrom, createdtimeto, invCodeAudit,
                approvedtimefrom, approvedtimeto, productcode, orgid, chkPending, chkApproved, chkCanceled, chkFinished);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "",
                    Rt_Cols_InvF_InvAuditInstLot = "",
                    Rt_Cols_InvF_InvAuditInstSerial = ""
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InvAudit);
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                if (objRT_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAudit.Count > 0)
                {
                    pageInfo.DataList = objRT_InvF_InvAudit.Lst_InvF_InvAudit;
                    itemCount = objRT_InvF_InvAudit.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_InvF_InvAudit.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return JsonView("BindDataInvF_InvAudit", pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }


        [HttpPost]
        public ActionResult Export1(string if_InvAudNo = "",
                string createdtimefrom = "",
                string createdtimeto = "",
                string invCodeAudit = "",
                string approvedtimefrom = "",
                string approvedtimeto = "",
                string productcode = "",
                string orgid = "",
                string chkPending = "",
                string chkApproved = "",
                string chkCanceled = "",
                string chkFinished = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };

            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);


            var ListInvF_InvAudit = new List<InvF_InvAudit>();
            var ListInvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
            try
            {
                #region["Search"]
                var strWhereClause = strWhereClause_InvFInvAudit_Search(if_InvAudNo, createdtimefrom, createdtimeto, invCodeAudit,
               approvedtimefrom, approvedtimeto, productcode, orgid, chkPending, chkApproved, chkCanceled, chkFinished);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Ft_RecordStart =Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    FlagIsDelete = "0",
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "",
                    Rt_Cols_InvF_InvAuditInstLot = "",
                    Rt_Cols_InvF_InvAuditInstSerial = ""
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InvAudit);
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                if(objRT_InvF_InvAudit != null)
                {
                    if(objRT_InvF_InvAudit.Lst_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAudit.Count > 0)
                    {
                        ListInvF_InvAudit = objRT_InvF_InvAudit.Lst_InvF_InvAudit;

                        if(ListInvF_InvAudit != null && ListInvF_InvAudit.Count > 0)
                        {
                            foreach(var item in ListInvF_InvAudit)
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
                    }
                    if(objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null && objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Count > 0)
                    {
                        ListInvF_InvAuditDtl = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl;
                    }

                    #region Master
                    DataTable dtInvF_InvAudit = new DataTable("InvF_InvAudit");
                    dtInvF_InvAudit.Columns.Add("Số phiếu kiểm kê", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Kho kiểm kê", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Ngày tạo", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Người tạo", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Ghi chú", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Trạng thái", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Ngày duyệt", typeof(string));
                    dtInvF_InvAudit.Columns.Add("Người duyệt", typeof(string));
                    foreach (var it in ListInvF_InvAudit)
                    {
                        DataRow dr = dtInvF_InvAudit.Rows.Add();
                        dr["Số phiếu kiểm kê"] = it.IF_InvAudNo;
                        dr["Kho kiểm kê"] = it.InvCodeAudit;
                        dr["Ngày tạo"] = it.CreateDTimeUTC;
                        dr["Người tạo"] = it.CreateBy;
                        dr["Ghi chú"] = it.Remark;
                        dr["Trạng thái"] = it.IF_InvAuditStatus;
                        dr["Ngày duyệt"] = it.ApprDTimeUTC;
                        dr["Người duyệt"] = it.ApprBy;
                    }
                    #endregion

                    #region["Detail"]
                    DataTable dtInvF_InvAuditDtl = new DataTable("InvF_InvAuditDtl");
                    dtInvF_InvAuditDtl.Columns.Add("Số phiếu kiểm kê", typeof(string));
                    dtInvF_InvAuditDtl.Columns.Add("Mã hàng hóa", typeof(string));
                    dtInvF_InvAuditDtl.Columns.Add("Tên hàng hóa (TV)", typeof(string));
                    dtInvF_InvAuditDtl.Columns.Add("Đơn vị tính", typeof(string));
                    dtInvF_InvAuditDtl.Columns.Add("Số lượng", typeof(string));
                    dtInvF_InvAuditDtl.Columns.Add("Vị trí", typeof(string));

                    var lstProductCode = new List<string>();
                    if (ListInvF_InvAuditDtl != null && ListInvF_InvAuditDtl.Count != 0)
                    {
                        foreach (var it in ListInvF_InvAuditDtl)
                        {
                            if (lstProductCode.Contains(it.ProductCode) == false)
                            {
                                lstProductCode.Add(it.ProductCode.ToString());
                            }
                        }
                    }
                    var IF_InvAuditStatus = "";
                    if (ListInvF_InvAudit != null && ListInvF_InvAudit.Count != 0)
                    {
                        IF_InvAuditStatus = ListInvF_InvAudit[0].IF_InvAuditStatus.ToString();
                    }
                    var sumqtyTotal = 0.0;
                    foreach (var productcode1 in lstProductCode)
                    {
                        var itFirst = new InvF_InvAuditDtl();
                        var lstInvCode = "";
                        var sumqty = 0.0;
                        var InvCodeInit = "";
                        var ProductCode = "";
                        var ProductName = "";
                        var ProductCodeUser = "";
                        var ProductType = "";
                        var lstInvF_InvAuditDtl = ListInvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productcode1)).ToList();
                        if (lstInvF_InvAuditDtl != null && lstInvF_InvAuditDtl.Count != 0)
                        {
                            itFirst = lstInvF_InvAuditDtl[0];
                            InvCodeInit = itFirst.InvCodeInit == null ? "" : itFirst.InvCodeInit.ToString();
                            ProductCode = itFirst.ProductCode == null ? "" : itFirst.ProductCode.ToString();
                            ProductCodeUser = itFirst.mp_ProductCodeUser == null ? "" : itFirst.mp_ProductCodeUser.ToString();
                            ProductName = itFirst.mp_ProductName == null ? "" : itFirst.mp_ProductName.ToString();
                        }
                        foreach (var it in lstInvF_InvAuditDtl)
                        {
                            var qty = it.QtyInit == null ? 0 : Convert.ToDouble(it.QtyInit);
                            var qtyActual = it.QtyActual == null ? 0 : Convert.ToDouble(it.QtyActual);

                            var invcode = it.InvCodeActual == null ? "" : it.InvCodeActual;
                            if (IF_InvAuditStatus == "FINISH")
                            {
                                sumqty += qtyActual;
                                sumqtyTotal += qtyActual;
                            }
                            else
                            {
                                sumqty += qty;
                                sumqtyTotal += qty;
                            }
                            if (lstInvCode != "")
                            {
                                lstInvCode += ", " + invcode;
                            }
                            else
                            {
                                lstInvCode += invcode;
                            }
                        }
                        DataRow dr = dtInvF_InvAuditDtl.Rows.Add();
                        dr["Số phiếu kiểm kê"] = itFirst.IF_InvAudNo;
                        dr["Mã hàng hóa"] = ProductCodeUser;
                        dr["Tên hàng hóa (TV)"] = ProductName;
                        dr["Đơn vị tính"] = InvCodeInit;
                        dr["Số lượng"] = sumqty;
                        dr["Vị trí"] = lstInvCode;
                    }

                    #endregion
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtInvF_InvAudit);
                    ds.Tables.Add(dtInvF_InvAuditDtl);
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InvAudit).Name), ref url);
                    ExcelExport.ExportToExcelDS(ds, filePath);
                    //return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
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
        public ActionResult Approve(string lstIF_InvAudNo)
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
                if (lstIF_InvAudNo != null && lstIF_InvAudNo != "")
                {
                    var listIF_InvAudNo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InvAudit>>(lstIF_InvAudNo);
                    foreach (var item in listIF_InvAudNo)
                    {
                        var objInvF_InvAudit = new InvF_InvAudit()
                        {
                            IF_InvAudNo = CUtils.StrValue(item.IF_InvAudNo),
                            Remark = CUtils.StrValue(item.Remark),

                        };
                        var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                            InvF_InvAudit = objInvF_InvAudit
                        };
                        InvF_InvAuditService.Instance.InvF_InvAudit_Appr(objRQ_InvF_InvAudit, addressAPIs);
                    }
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Duyệt phiếu kiểm kê thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = false;
                    resultEntry.AddDetail("Số phiếu kiểm kê không tồn tại");
                }
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Finish(string lstIF_InvAudNo)
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
                if (lstIF_InvAudNo != null && lstIF_InvAudNo != "")
                {
                    var listIF_InvAudNo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InvAudit>>(lstIF_InvAudNo);
                    foreach (var item in listIF_InvAudNo)
                    {
                        var objInvF_InvAudit = new InvF_InvAudit()
                        {
                            IF_InvAudNo = CUtils.StrValue(item.IF_InvAudNo),
                            Remark = CUtils.StrValue(item.Remark),

                        };
                        var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                            InvF_InvAudit = objInvF_InvAudit
                        };
                        InvF_InvAuditService.Instance.InvF_InvAudit_Finish(objRQ_InvF_InvAudit, addressAPIs);
                    }
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Duyệt phiếu kiểm kê thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = false;
                    resultEntry.AddDetail("Danh sách phiếu kiểm kê trống");
                }
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Cancel(string lstIF_InvAudNo)
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
                if (lstIF_InvAudNo != null && lstIF_InvAudNo != "")
                {
                    var listIF_InvAudNo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InvAudit>>(lstIF_InvAudNo);
                    foreach (var item in listIF_InvAudNo)
                    {
                        var objInvF_InvAudit = new InvF_InvAudit()
                        {
                            IF_InvAudNo = CUtils.StrValue(item.IF_InvAudNo),
                            Remark = CUtils.StrValue(item.Remark),

                        };
                        var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                            InvF_InvAudit = objInvF_InvAudit
                        };
                        InvF_InvAuditService.Instance.InvF_InvAudit_Cancel(objRQ_InvF_InvAudit, addressAPIs);
                    }
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Hủy phiếu kiểm kê thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = false;
                    resultEntry.AddDetail("Số phiếu kiểm kê không tồn tại");
                }
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Save(string model, string savetype, string invBUPattern, string lst_ProductCodeUser/*, string lstUnitCode_Product*/)
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
                var lst_InvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                var lst_InvF_InvAuditInstLot = new List<InvF_InvAuditInstLot>();
                var lst_InvF_InvAuditInstSerial = new List<InvF_InvAuditInstSerial>();

                string flagIsDelete = "0";
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit();
                if (model != null && model != "")
                {
                    var objRQ = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InvAudit>(model);
                    var lstProductCodeUser = new List<string>();
                    if (lst_ProductCodeUser != null)
                    {
                        lstProductCodeUser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lst_ProductCodeUser);
                    }
                    if (objRQ != null)
                    {
                        objRQ_InvF_InvAudit = objRQ;

                        #region Lấy thông tin từ hệ thống, không cần chọn                        
                        var listProductCodeUser = "";
                        foreach (var it in lstProductCodeUser)
                        {
                            if (listProductCodeUser == "")
                            {
                                listProductCodeUser = it;
                            }
                            else
                            {
                                listProductCodeUser += "," + it;
                            }
                        }
                        #region Lấy thông tin hàng hóa
                        var strWhereProduct = "";
                        var sbSql = new StringBuilder();
                        var Tbl_Mst_Product = "Mst_Product.";
                        if (listProductCodeUser != "")
                        {
                            sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeUser", listProductCodeUser, "IN");
                        }
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

                        #region Lấy thông tin Lot và Serial
                        foreach (var product in lsMst_Product)
                        {
                            var flagLot = product.FlagLot.ToString();
                            var flagSerial = product.FlagSerial.ToString();
                            var productCodeBase = product.ProductCodeBase.ToString();
                            var valconvert = 1.0;
                            if (product.ValConvert != null)
                            {
                                valconvert = Convert.ToDouble(product.ValConvert);
                            }
                            if (flagLot == "1")
                            {
                                var strWhere = "";
                                var sbSql1 = new StringBuilder();
                                var Tbl_Inv_InventoryBalanceLot = "Inv_InventoryBalanceLot.";
                                var Tbl_Mst_Inventory = "Mst_Inventory.";
                                if (!string.IsNullOrEmpty(productCodeBase))
                                {
                                    sbSql1.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                                }
                                if (!string.IsNullOrEmpty(invBUPattern))
                                {
                                    sbSql1.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                                }
                                sbSql1.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                                sbSql1.AddWhereClause(Tbl_Inv_InventoryBalanceLot + "QtyTotalOK", 0, ">");
                                strWhere = sbSql1.ToString();
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

                                foreach (var it in lstInv_InventoryBalanceLot)
                                {
                                    var qty = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valconvert, 2);
                                    var checkexist = lst_InvF_InvAuditDtl.Where(x => x.InvCodeInit.Equals(it.InvCode) && x.InvCodeActual.Equals(it.InvCode) && x.ProductCode.Equals(product.ProductCode)).ToList();
                                    if (checkexist.Count == 0)
                                    {
                                        var objInvF_InvAuditDtl = new InvF_InvAuditDtl();
                                        objInvF_InvAuditDtl.InvCodeInit = it.InvCode;
                                        objInvF_InvAuditDtl.InvCodeActual = it.InvCode;
                                        objInvF_InvAuditDtl.ProductCode = product.ProductCode;
                                        objInvF_InvAuditDtl.QtyInit = qty;
                                        objInvF_InvAuditDtl.QtyActual = qty;
                                        objInvF_InvAuditDtl.UnitCode = product.UnitCode;
                                        objInvF_InvAuditDtl.Remark = "";
                                        lst_InvF_InvAuditDtl.Add(objInvF_InvAuditDtl);
                                    }
                                    else
                                    {
                                        var objInvF_InvAuditDtl = checkexist[0];
                                        var qtyItem = Convert.ToDouble(objInvF_InvAuditDtl.QtyInit);
                                        objInvF_InvAuditDtl.QtyInit = qtyItem + qty;
                                        objInvF_InvAuditDtl.QtyActual = qtyItem + qty;
                                    }
                                    InvF_InvAuditInstLot objInvF_InvAuditInstLot = new InvF_InvAuditInstLot();
                                    objInvF_InvAuditInstLot.InvCodeInit = it.InvCode;
                                    objInvF_InvAuditInstLot.InvCodeActual = it.InvCode;

                                    objInvF_InvAuditInstLot.ProductCode = product.ProductCode;
                                    objInvF_InvAuditInstLot.ProductLotNo = it.ProductLotNo;
                                    objInvF_InvAuditInstLot.QtyInit = qty;
                                    objInvF_InvAuditInstLot.QtyInit = qty;
                                    objInvF_InvAuditInstLot.ProductionDate = it.ProductionDate == null ? "" : Convert.ToDateTime(it.ProductionDate).ToString("yyyy-MM-dd");
                                    objInvF_InvAuditInstLot.ExpiredDate = it.ExpiredDate == null ? "" : Convert.ToDateTime(it.ExpiredDate).ToString("yyyy-MM-dd");
                                    lst_InvF_InvAuditInstLot.Add(objInvF_InvAuditInstLot);
                                }
                            }
                            else if (flagSerial == "1")
                            {
                                var strWhere = "";
                                var sbSql2 = new StringBuilder();
                                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                                var Tbl_Mst_Inventory = "Mst_Inventory.";
                                if (!string.IsNullOrEmpty(productCodeBase))
                                {
                                    sbSql2.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCodeBase), "=");
                                }
                                if (!string.IsNullOrEmpty(invBUPattern))
                                {
                                    sbSql2.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                                }
                                sbSql2.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
                                strWhere = sbSql2.ToString();
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
                                foreach (var it in lstInv_InventoryBalanceSerial)
                                {
                                    InvF_InvAuditInstSerial objInvF_InvAuditInstSerial = new InvF_InvAuditInstSerial();
                                    objInvF_InvAuditInstSerial.InvCodeInit = it.InvCode;
                                    objInvF_InvAuditInstSerial.InvCodeActual = it.InvCode;
                                    objInvF_InvAuditInstSerial.ProductCode = product.ProductCode;
                                    objInvF_InvAuditInstSerial.SerialNo = it.SerialNo;
                                    lst_InvF_InvAuditInstSerial.Add(objInvF_InvAuditInstSerial);
                                    var checkexist = lst_InvF_InvAuditDtl.Where(x => x.InvCodeInit.Equals(it.InvCode) && x.InvCodeActual.Equals(it.InvCode) && x.ProductCode.Equals(product.ProductCode)).ToList();

                                    var qty = 1.0;
                                    if (checkexist.Count == 0)
                                    {
                                        var objInvF_InvAuditDtl = new InvF_InvAuditDtl();
                                        objInvF_InvAuditDtl.InvCodeInit = it.InvCode;
                                        objInvF_InvAuditDtl.InvCodeActual = it.InvCode;
                                        objInvF_InvAuditDtl.ProductCode = product.ProductCode;
                                        objInvF_InvAuditDtl.QtyInit = qty;
                                        objInvF_InvAuditDtl.QtyActual = qty;
                                        objInvF_InvAuditDtl.UnitCode = product.UnitCode;
                                        objInvF_InvAuditDtl.Remark = "";
                                        lst_InvF_InvAuditDtl.Add(objInvF_InvAuditDtl);
                                    }
                                    else
                                    {
                                        var objInvF_InvAuditDtl = checkexist[0];
                                        var qtyItem = Convert.ToDouble(objInvF_InvAuditDtl.QtyInit);
                                        objInvF_InvAuditDtl.QtyInit = qtyItem + qty;
                                        objInvF_InvAuditDtl.QtyActual = qtyItem + qty;
                                    }
                                }
                            }
                            else
                            {
                                var strWhereClause = "";
                                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                                var tbMst_Inventory = "Mst_Inventory.";
                                var sb_SQL = new StringBuilder();
                                if (productCodeBase != null && productCodeBase != "")
                                {
                                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productCodeBase, "=");
                                }
                                sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", invBUPattern, "like");
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
                                        var qty = Math.Round(Convert.ToDouble(it.QtyTotalOK) / valconvert, 2);
                                        //var objLst_InvF_InvAuditDtl = new InvF_InvAuditDtl();
                                        //objLst_InvF_InvAuditDtl.InvCodeInit = it.InvCode;
                                        //objLst_InvF_InvAuditDtl.ProductCode = product.ProductCode;
                                        //objLst_InvF_InvAuditDtl.InvCodeActual = it.InvCode;
                                        //objLst_InvF_InvAuditDtl.QtyInit = qty;
                                        //objLst_InvF_InvAuditDtl.UnitCode = product.UnitCode;
                                        //lst_InvF_InvAuditDtl.Add(objLst_InvF_InvAuditDtl);

                                        var checkexist = lst_InvF_InvAuditDtl.Where(x => x.InvCodeInit.Equals(it.InvCode) && x.InvCodeActual.Equals(it.InvCode) && x.ProductCode.Equals(product.ProductCode)).ToList();
                                        if (checkexist.Count == 0)
                                        {
                                            var objInvF_InvAuditDtl = new InvF_InvAuditDtl();
                                            objInvF_InvAuditDtl.InvCodeInit = it.InvCode;
                                            objInvF_InvAuditDtl.InvCodeActual = it.InvCode;
                                            objInvF_InvAuditDtl.ProductCode = product.ProductCode;
                                            objInvF_InvAuditDtl.QtyInit = qty;
                                            objInvF_InvAuditDtl.QtyActual = qty;
                                            objInvF_InvAuditDtl.UnitCode = product.UnitCode;
                                            objInvF_InvAuditDtl.Remark = "";
                                            lst_InvF_InvAuditDtl.Add(objInvF_InvAuditDtl);
                                        }
                                        else
                                        {
                                            var objInvF_InvAuditDtl = checkexist[0];
                                            var qtyItem = Convert.ToDouble(objInvF_InvAuditDtl.QtyInit);
                                            objInvF_InvAuditDtl.QtyInit = qtyItem + qty;
                                            objInvF_InvAuditDtl.QtyActual = qtyItem + qty;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #endregion


                        // Gan lai thong tin
                        objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl = lst_InvF_InvAuditDtl;
                        objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot = lst_InvF_InvAuditInstLot;
                        objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial = lst_InvF_InvAuditInstSerial;
                        //

                        // Thêm một số thông tin
                        objRQ_InvF_InvAudit.InvF_InvAudit.OrgID = orgId;
                        objRQ_InvF_InvAudit.InvF_InvAudit.NetworkID = networkID;
                        //
                        if (objRQ_InvF_InvAudit.FlagIsDelete != null)
                        {
                            flagIsDelete = objRQ_InvF_InvAudit.FlagIsDelete;
                        }
                    }
                }

                // Thêm 1 số thông tin khác
                objRQ_InvF_InvAudit.Tid = GetNextTId();
                objRQ_InvF_InvAudit.GwUserCode = GwUserCode;
                objRQ_InvF_InvAudit.GwPassword = GwPassword;
                objRQ_InvF_InvAudit.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InvAudit.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InvAudit.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InvAudit.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InvAudit.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InvAudit.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                //

                //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objRQ_InvF_InvAudit);
                InvF_InvAuditService.Instance.InvF_InvAudit_Save(objRQ_InvF_InvAudit, addressAPIs);

                resultEntry.Success = true;
                if (flagIsDelete == "1")
                {
                    resultEntry.AddMessage("Xóa phiếu kiểm kê thành công!");
                }
                else
                {
                    resultEntry.AddMessage("Lưu phiếu kiểm kê thành công!");
                }
                if (savetype != null && savetype == "saveandcreate")
                {
                    resultEntry.RedirectUrl = Url.Action("Create");
                }
                else
                {
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult DeleteMulti(string lstInvF_InvAudit)
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
                if (lstInvF_InvAudit != null && lstInvF_InvAudit != "")
                {
                    var listInvF_InvAudit = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvF_InvAudit>>(lstInvF_InvAudit);
                    // Thông tin chung
                    var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit();
                    objRQ_InvF_InvAudit.FlagIsDelete = "1";
                    // Thêm 1 số thông tin khác
                    objRQ_InvF_InvAudit.Tid = GetNextTId();
                    objRQ_InvF_InvAudit.GwUserCode = GwUserCode;
                    objRQ_InvF_InvAudit.GwPassword = GwPassword;
                    objRQ_InvF_InvAudit.AccessToken = CUtils.StrValue(UserState.AccessToken);
                    objRQ_InvF_InvAudit.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                    objRQ_InvF_InvAudit.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                    objRQ_InvF_InvAudit.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                    objRQ_InvF_InvAudit.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    objRQ_InvF_InvAudit.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                    //
                    foreach (var it in listInvF_InvAudit)
                    {
                        it.OrgID = orgId;
                        it.NetworkID = networkID;

                        objRQ_InvF_InvAudit.InvF_InvAudit = it;
                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InvAudit);
                        InvF_InvAuditService.Instance.InvF_InvAudit_Save(objRQ_InvF_InvAudit, addressAPIs);
                    }
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa phiếu kiểm kê thành công!");
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Audit(string IF_InvAudNo)
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
                if (IF_InvAudNo != null && IF_InvAudNo != "")
                {
                    var objInvF_InvAudit = new InvF_InvAudit()
                    {
                        IF_InvAudNo = IF_InvAudNo
                    };
                    var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        InvF_InvAudit = objInvF_InvAudit
                    };
                    InvF_InvAuditService.Instance.InvF_InvAuditDtl_FlagAudit(objRQ_InvF_InvAudit, addressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Kiểm kê thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = false;
                    resultEntry.AddDetail("Số phiếu kiểm kê không tồn tại");
                }
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
            return Json(resultEntry);
        }
        public ActionResult Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVAUDIT_CREATE");
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

                #region Sinh số kiểm kê
                Seq_Common objSeq_Common = new Seq_Common();
                objSeq_Common.Param_Postfix = "";
                objSeq_Common.Param_Prefix = "";
                objSeq_Common.SequenceType = SeqType.IFInvAudNo;
                var IFInvAudNo = SeqNo(objSeq_Common);
                ViewBag.IFInvAudNo = IFInvAudNo;
                #endregion

                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion
                #region ["Comment autocomplete search hàng hóa thì mới b.đầu tìm kiếm"]

                //#region Thông tin hàng hóa
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
                //    Ft_WhereClause = "Mst_Product.FlagActive = 1 AND Mst_Product.ProductType != 'COMBO'"
                //};
                //var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                //if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                //{
                //    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                //}
                //#endregion

                //#region Lấy thông tin tồn kho
                ////var lst_StrProductCode = new List<string>();
                ////if (lst_Mst_Product.Count != 0)
                ////{
                ////    foreach (var it in lst_Mst_Product)
                ////    {
                ////        if (!lst_StrProductCode.Contains(it.ProductCode))
                ////        {
                ////            lst_StrProductCode.Add(it.ProductCode.ToString());
                ////        }
                ////    }
                ////}
                ////var strWhereClause = "";
                ////var sb_SQL = new StringBuilder();
                ////var lstProductCode = "";
                ////if (lst_StrProductCode.Count != 0)
                ////{
                ////    foreach (var productcode in lst_StrProductCode)
                ////    {
                ////        if (lstProductCode == "")
                ////        {
                ////            lstProductCode += productcode;
                ////        }
                ////        else
                ////        {
                ////            lstProductCode += ", " + productcode;
                ////        }
                ////    }
                ////}
                ////var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                ////if (lstProductCode != "")
                ////{
                ////    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", lstProductCode, "IN");
                ////}
                ////strWhereClause = sb_SQL.ToString();

                ////var objRQ_Inv_InventoryBalance = new RQ_Inv_InventoryBalance()
                ////{
                ////    // WARQBase
                ////    Tid = GetNextTId(),
                ////    GwUserCode = GwUserCode,
                ////    GwPassword = GwPassword,
                ////    AccessToken = CUtils.StrValue(UserState.AccessToken),
                ////    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                ////    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                ////    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                ////    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                ////    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                ////    Rt_Cols_Inv_InventoryBalance = "*",
                ////    Ft_RecordStart = Ft_RecordStart,
                ////    Ft_RecordCount = Ft_RecordCount,
                ////    Ft_WhereClause = strWhereClause
                ////};
                ////var objRT_Inv_InventoryBalance = Inv_InventoryBalanceService.Instance.WA_Inv_InventoryBalance_Get(objRQ_Inv_InventoryBalance, addressAPIs);
                ////var lstInv_InventoryBalance = new List<Inv_InventoryBalance>();
                ////if (objRT_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance != null)
                ////{
                ////    lstInv_InventoryBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
                ////}

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
                //    //if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count != 0)
                //    //{
                //    //    var lstProductByCode = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                //    //    if (lstProductByCode != null && lstProductByCode.Count != 0)
                //    //    {
                //    //        var qtyTotalOkMax = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                //    //        var itBalance = lstInv_InventoryBalance.Where(x => x.ProductCode.Equals(it.ProductCode) && x.QtyTotalOK.Equals(qtyTotalOkMax)).FirstOrDefault();
                //    //        if (itBalance != null)
                //    //        {
                //    //            productUI.InvCode = itBalance.InvCode;
                //    //            productUI.QtyTotalOK = qtyTotalOkMax;
                //    //            productUI.DiscountPrice = "0"; // Tạm để
                //    //        }
                //    //    }
                //    //}
                //    lst_Mst_ProductUI.Add(productUI);
                //}
                //ViewBag.Lst_Mst_ProductUI = lst_Mst_ProductUI;
                //return View();
                //#endregion
                #endregion
                return View();
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


        public ActionResult GetProduct_MstInventory(/*string invCode*/string InvBUPattern)
        {
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #region Thông tin hàng hóa

            #endregion

            #region Lấy thông tin tồn kho
            var sb_SQL = new StringBuilder();
            var tblInv_InventoryBalance = "Inv_InventoryBalance.";
            var tbMst_Inventory = "Mst_Inventory.";
            //if (invCode != null && invCode != "")
            //{
            //    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "InvCode", invCode, "=");
            //}
            if (InvBUPattern != null && InvBUPattern != "")
            {
                sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", InvBUPattern, "like");
            }
            sb_SQL.AddWhereClause(tblInv_InventoryBalance + "QtyTotalOK", 0, ">");
            var strWhereClause = sb_SQL.ToString();

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
            var lstBalance = new List<Inv_InventoryBalance>();
            if (objRT_Inv_InventoryBalance != null)
            {
                lstBalance = objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance;
            }
            #region Lấy danh sách sản phẩm
            var lstProductCode = new List<string>();
            foreach (var it in lstBalance)
            {
                if (lstProductCode.Contains(it.ProductCode) == false)
                {
                    lstProductCode.Add(it.ProductCode.ToString());
                }
            }
            var listProductCode = "";
            foreach (var productCode in lstProductCode)
            {
                if (listProductCode == "")
                {
                    listProductCode += productCode;
                }
                else
                {
                    listProductCode += "," + productCode;
                }
            }
            var sb = new StringBuilder();
            var tblMstProduct = "Mst_Product.";
            var tblMstInventory = "Mst_Inventory.";
            sb.AddWhereClause(tblMstProduct + "FlagActive", "1", "=");
            sb.AddWhereClause(tblMstProduct + "ProductType", "COMBO", "!=");
            sb.AddWhereClause(tblMstProduct + "ProductCodeBase", listProductCode, "IN");
            var strWhere = sb.ToString();
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
                Ft_WhereClause = strWhere
            };
            var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
            {
                lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
            }
            #endregion
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();

            List<string> lst_ProductCodeDistin = new List<string>();

            foreach (var it in lstBalance)
            {
                if (lst_ProductCodeDistin.Contains(it.ProductCode) == false)
                {
                    lst_ProductCodeDistin.Add(it.ProductCode.ToString());
                    if (it.ProductCode.ToString().Equals("0A3007VS000001V"))
                    {
                        var abc = "";
                    }
                    var product = lst_Mst_Product.Where(x => x.ProductCode.Equals(it.ProductCode)).FirstOrDefault();
                    var lstTonKhoSP = lstBalance.Where(x => x.ProductCode.Equals(it.ProductCode)).ToList();
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;

                    if (product != null)
                    {
                        productUI.LstPrdBaseUI = new List<Mst_ProductUI>();
                        productUI.ProductName = product.ProductName;
                        productUI.ProductCode = product.ProductCode;
                        productUI.UnitCode = product.UnitCode;
                        productUI.ProductCodeBase = product.ProductCodeBase;
                        productUI.ProductCodeRoot = product.ProductCodeRoot;
                        productUI.ValConvert = product.ValConvert;
                        productUI.ProductCodeUser = product.ProductCodeUser;
                        if (product.FlagSerial.Equals("1"))
                        {
                            productUI.FlagSerial = "1";
                        }
                        if (product.FlagLot.Equals("1"))
                        {
                            productUI.FlagLot = "1";
                        }
                        if (product.ProductType != null && product.ProductType.ToString().ToUpper().Equals("COMBO") && product.ValConvert != null)
                        {
                            productUI.FlagCombo = "1";
                        }
                        // Check theo đơn hàng hay không
                        productUI.SellPrice = product.UPBuy == null ? 0 : product.UPBuy;
                        //    

                        productUI.QtyTotalOK = lstTonKhoSP.Sum(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                        
                        var listInvCode = new List<string>();
                        
                        var lstViTriKK = lstTonKhoSP.Select(x => x.InvCode).Distinct().ToList();
                        productUI.lstInvCode = lstViTriKK;
                        //danh sách sản phẩm phái sinh cùng mã base
                        var lstProductBase = lst_Mst_Product.Where(x => x.ProductCodeBase.Equals(product.ProductCode)).ToList();
                        if (lstProductBase != null && lstProductBase.Count > 0)
                        {
                            //quy đổi tồn kho cho sản phẩm phái sinh
                            foreach (var itPrdBase in lstProductBase)
                            {
                                var itPrdBaseUI = new Mst_ProductUI();
                                itPrdBaseUI.ProductCode = itPrdBase.ProductCode;
                                itPrdBaseUI.ProductName = itPrdBase.ProductName;
                                itPrdBaseUI.UnitCode = itPrdBase.UnitCode;
                                itPrdBaseUI.ProductCodeBase = itPrdBase.ProductCodeBase;
                                itPrdBaseUI.ProductCodeRoot = itPrdBase.ProductCodeRoot;
                                itPrdBaseUI.ValConvert = itPrdBase.ValConvert;
                                itPrdBaseUI.ProductCodeUser = itPrdBase.ProductCodeUser;
                                if (itPrdBase.FlagSerial.Equals("1"))
                                {
                                    itPrdBaseUI.FlagSerial = "1";
                                }
                                if (itPrdBase.FlagLot.Equals("1"))
                                {
                                    itPrdBaseUI.FlagLot = "1";
                                }
                                if (itPrdBase.ProductType != null && itPrdBase.ProductType.ToString().ToUpper().Equals("COMBO") && itPrdBase.ValConvert != null)
                                {
                                    itPrdBaseUI.FlagCombo = "1";
                                }
                                // Check theo đơn hàng hay không
                                itPrdBaseUI.SellPrice = itPrdBase.UPBuy == null ? 0 : itPrdBase.UPBuy;
                                itPrdBaseUI.QtyTotalOK = Convert.ToDouble(productUI.QtyTotalOK) / Convert.ToDouble(itPrdBaseUI.ValConvert);
                                itPrdBaseUI.lstInvCode = lstViTriKK;

                                productUI.LstPrdBaseUI.Add(itPrdBaseUI);
                            }
                            
                        }
                        lst_Mst_ProductUI.Add(productUI);
                    }

                }
            }
            return Json(new { Success = true, data = lst_Mst_ProductUI });
            #endregion
        }

        public ActionResult Update(string IF_InvAudNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVAUDIT_UPDATE");
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
                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

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
                    Ft_WhereClause = "Mst_Product.FlagActive = 1 AND Mst_Product.ProductType = 'PRODUCT'"
                };
                var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                {
                    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                }
                #endregion

                #region Lấy thông tin sản phẩm   
                var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                foreach (var it in lst_Mst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
                    productUI.ValConvert = it.ValConvert;
                    productUI.ProductCodeUser = it.ProductCodeUser;
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
                #endregion

                #region Thông tin phiếu kiểm kê
                InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
                {
                    IF_InvAudNo = IF_InvAudNo
                };

                var strWhere = WhereClause(objInvF_InvAudit, null, null, null, null);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "*",
                    Rt_Cols_InvF_InvAuditInstLot = "*",
                    Rt_Cols_InvF_InvAuditInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                var lst_InvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                if (objRT_InvF_InvAudit != null)
                {
                    if (objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                    {
                        var lstProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Select(x => x.ProductCode).Distinct();
                        if (lstProductCode != null)
                        {
                            foreach (var productcode in lstProductCode)
                            {
                                var lstInvF_InvAuditDtlProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productcode)).ToList();
                                if (lstInvF_InvAuditDtlProductCode == null || lstInvF_InvAuditDtlProductCode.Count == 0) continue;
                                var it = lstInvF_InvAuditDtlProductCode.FirstOrDefault();
                                #region Lấy danh sách đơn vị theo Product
                                var strWhereProduct = "";
                                var sbSql = new StringBuilder();
                                var Tbl_Mst_Product = "Mst_Product.";
                                if (it.mp_ProductCodeBase != null && it.mp_ProductCodeBase.ToString() != "")
                                {
                                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_ProductCodeBase.ToString()), "=");
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
                                var lst_Mst_ProductUI_ByProduct = new List<Mst_ProductUI>();
                                foreach (var item in lsMst_Product)
                                {
                                    var productUI = new Mst_ProductUI();
                                    productUI.ProductCode = item.ProductCode;
                                    productUI.ProductCodeUser = item.ProductCodeUser;
                                    productUI.ProductName = item.ProductName;
                                    productUI.UnitCode = item.UnitCode;
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
                                    productUI.SellPrice = item.UPBuy == null ? 0 : item.UPBuy;
                                    lst_Mst_ProductUI_ByProduct.Add(productUI);
                                }
                                foreach (var itInvAuditDtl in lstInvF_InvAuditDtlProductCode)
                                {
                                    itInvAuditDtl.lstUnitCodeUIByProduct = lst_Mst_ProductUI_ByProduct;

                                    lst_InvF_InvAuditDtl.Add(itInvAuditDtl);
                                }
                                #endregion
                            }
                        }
                    }
                    objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl = lst_InvF_InvAuditDtl;
                    return View(objRT_InvF_InvAudit);
                }
                else
                {
                    return View(new RT_InvF_InvAudit());
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
        public ActionResult Detail(string IF_InvAudNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVAUDIT_DETAIL");
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

                #region Thông tin kho xuất
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get_Inventory(addressAPIs);
                #endregion

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
                    Ft_WhereClause = "Mst_Product.FlagActive = 1 AND Mst_Product.ProductType = 'PRODUCT'"
                };
                var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                {
                    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                }
                #endregion

                #region Lấy thông tin sản phẩm   
                var lst_Mst_ProductUI = new List<Mst_ProductUI>();
                foreach (var it in lst_Mst_Product)
                {
                    var productUI = new Mst_ProductUI();
                    productUI.ProductCode = it.ProductCode;
                    productUI.ProductName = it.ProductName;
                    productUI.UnitCode = it.UnitCode;
                    productUI.ProductCodeBase = it.ProductCodeBase;
                    productUI.ProductCodeRoot = it.ProductCodeRoot;
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
                #endregion

                #region Thông tin phiếu kiểm kê
                InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
                {
                    IF_InvAudNo = IF_InvAudNo
                };

                var strWhere = WhereClause(objInvF_InvAudit, null, null, null, null);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "*",
                    Rt_Cols_InvF_InvAuditInstLot = "*",
                    Rt_Cols_InvF_InvAuditInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                var lst_InvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                if (objRT_InvF_InvAudit != null)
                {
                    if (objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                    {
                        var lstProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Select(x => x.ProductCode).Distinct();
                        if (lstProductCode != null)
                        {
                            foreach (var productcode in lstProductCode)
                            {
                                var lstInvF_InvAuditDtlProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productcode)).ToList();
                                if (lstInvF_InvAuditDtlProductCode == null || lstInvF_InvAuditDtlProductCode.Count == 0) continue;
                                var it = lstInvF_InvAuditDtlProductCode.FirstOrDefault();
                                #region Lấy danh sách đơn vị theo Product
                                var strWhereProduct = "";
                                var sbSql = new StringBuilder();
                                var Tbl_Mst_Product = "Mst_Product.";
                                if (it.mp_ProductCodeBase != null && it.mp_ProductCodeBase.ToString() != "")
                                {
                                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_ProductCodeBase.ToString()), "=");
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
                                var lst_Mst_ProductUI_ByProduct = new List<Mst_ProductUI>();
                                foreach (var item in lsMst_Product)
                                {
                                    var productUI = new Mst_ProductUI();
                                    productUI.ProductCode = item.ProductCode;
                                    productUI.ProductName = item.ProductName;
                                    productUI.UnitCode = item.UnitCode;
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
                                    productUI.SellPrice = item.UPBuy == null ? 0 : item.UPBuy;
                                    lst_Mst_ProductUI_ByProduct.Add(productUI);
                                }
                                foreach (var itInvAuditDtl in lstInvF_InvAuditDtlProductCode)
                                {
                                    itInvAuditDtl.lstUnitCodeUIByProduct = lst_Mst_ProductUI_ByProduct;

                                    lst_InvF_InvAuditDtl.Add(itInvAuditDtl);
                                }
                                #endregion
                            }
                        }
                    }
                    objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl = lst_InvF_InvAuditDtl;
                    return View(objRT_InvF_InvAudit);
                }
                else
                {
                    return View(new RT_InvF_InvAudit());
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

        public ActionResult Action(string IF_InvAudNo)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MNU_INVF_INVAUDIT_ACTION");
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

                #region Thông tin kho xuất
                var lst_Mst_Inventory = new List<Mst_Inventory>();
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
                    Ft_RecordCount = Ft_RecordCount
                };
                var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
                {
                    lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
                }
                ViewBag.Lst_Mst_Inventory = lst_Mst_Inventory;
                #endregion

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
                    Ft_WhereClause = "Mst_Product.FlagActive = 1 AND Mst_Product.ProductType != 'COMBO'"
                };
                var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
                if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
                {
                    lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
                }
                #endregion

                #region Lấy thông tin sản phẩm   
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
                #endregion

                #region Thông tin phiếu kiểm kê
                InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
                {
                    IF_InvAudNo = IF_InvAudNo
                };

                var strWhere = WhereClause(objInvF_InvAudit, null, null, null, null);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "*",
                    Rt_Cols_InvF_InvAuditInstLot = "*",
                    Rt_Cols_InvF_InvAuditInstSerial = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                var lst_InvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                if (objRT_InvF_InvAudit != null)
                {
                    if (objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                    {
                        var lstProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Select(x => x.ProductCode).Distinct();
                        if (lstProductCode != null)
                        {
                            foreach (var productcode in lstProductCode)
                            {
                                var lstInvF_InvAuditDtlProductCode = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productcode)).ToList();
                                if (lstInvF_InvAuditDtlProductCode == null || lstInvF_InvAuditDtlProductCode.Count == 0) continue;
                                var it = lstInvF_InvAuditDtlProductCode.FirstOrDefault();
                                #region Lấy danh sách đơn vị theo Product
                                var strWhereProduct = "";
                                var sbSql = new StringBuilder();
                                var Tbl_Mst_Product = "Mst_Product.";
                                if (it.mp_ProductCodeBase != null && it.mp_ProductCodeBase.ToString() != "")
                                {
                                    sbSql.AddWhereClause(Tbl_Mst_Product + "ProductCodeBase", CUtils.StrValue(it.mp_ProductCodeBase.ToString()), "=");
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
                                var lst_Mst_ProductUI_ByProduct = new List<Mst_ProductUI>();
                                foreach (var item in lsMst_Product)
                                {
                                    var productUI = new Mst_ProductUI();
                                    productUI.ProductCode = item.ProductCode;
                                    productUI.ProductName = item.ProductName;
                                    productUI.ProductCodeBase = item.ProductCodeBase;
                                    productUI.UnitCode = item.UnitCode;
                                    productUI.ProductCodeUser = item.ProductCodeUser;
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
                                    productUI.SellPrice = item.UPBuy == null ? 0 : item.UPBuy;
                                    lst_Mst_ProductUI_ByProduct.Add(productUI);
                                }
                                foreach (var itInvAuditDtl in lstInvF_InvAuditDtlProductCode)
                                {
                                    itInvAuditDtl.lstUnitCodeUIByProduct = lst_Mst_ProductUI_ByProduct;

                                    lst_InvF_InvAuditDtl.Add(itInvAuditDtl);
                                }
                                #endregion
                            }
                        }
                    }
                    objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl = lst_InvF_InvAuditDtl;
                    return View(objRT_InvF_InvAudit);
                }
                else
                {
                    return View(new RT_InvF_InvAudit());
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

        [HttpPost]
        public ActionResult ActionIF_InvAudNo(string model)
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
                string flagIsDelete = "0";
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit();
                if (model != null && model != "")
                {
                    var objRQ = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InvAudit>(model);
                    if (objRQ != null)
                    {
                        objRQ_InvF_InvAudit = objRQ;
                        // Thêm một số thông tin
                        objRQ_InvF_InvAudit.InvF_InvAudit.OrgID = orgId;
                        objRQ_InvF_InvAudit.InvF_InvAudit.NetworkID = networkID;
                        //
                        if (objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                        {
                            foreach (var it in objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl)
                            {
                                var invCodeInit = it.InvCodeInit == null ? "" : it.InvCodeInit.ToString();
                                var invCodeActual = it.InvCodeActual == null ? "" : it.InvCodeActual.ToString();

                                var qtyinit = it.QtyInit == null ? 0 : Convert.ToDouble(it.QtyInit);
                                var qtyActual = it.QtyActual == null ? 0 : Convert.ToDouble(it.QtyActual);

                                if (invCodeInit == invCodeActual)
                                {
                                    if (qtyinit > qtyActual)
                                    {
                                        it.InventoryAction = "OUT";
                                    }
                                    else if (qtyinit < qtyActual)
                                    {
                                        it.InventoryAction = "IN";
                                    }
                                    else
                                    {
                                        it.InventoryAction = "NORMAL";
                                    }
                                }
                                else
                                {
                                    it.InventoryAction = "MOVE";
                                }
                                it.FlagExist = Client_Flag.Active;
                            }
                        }

                        if (objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot != null)
                        {
                            foreach (var it in objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot)
                            {
                                var invCodeInit = it.InvCodeInit == null ? "" : it.InvCodeInit.ToString();
                                var invCodeActual = it.InvCodeActual == null ? "" : it.InvCodeActual.ToString();

                                var qtyinit = it.QtyInit == null ? 0 : Convert.ToDouble(it.QtyInit);
                                var qtyActual = it.QtyActual == null ? 0 : Convert.ToDouble(it.QtyActual);

                                if (invCodeInit == invCodeActual)
                                {
                                    if (qtyinit > qtyActual)
                                    {
                                        it.InventoryAction = "OUT";
                                    }
                                    else if (qtyinit < qtyActual)
                                    {
                                        it.InventoryAction = "IN";
                                    }
                                    else
                                    {
                                        it.InventoryAction = "NORMAL";
                                    }
                                }
                                else
                                {
                                    it.InventoryAction = "MOVE";
                                }

                                it.FlagExist = Client_Flag.Active;
                            }
                        }

                        //if (objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial != null)
                        //{
                        //    foreach (var it in objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial)
                        //    {
                        //        var invCodeInit = it.InvCodeInit == null ? "" : it.InvCodeInit.ToString();
                        //        var invCodeActual = it.InvCodeActual == null ? "" : it.InvCodeActual.ToString();

                        //        //var qtyinit = it.QtyInit == null ? 0 : Convert.ToDouble(it.QtyInit);
                        //        //var qtyActual = it.QtyActual == null ? 0 : Convert.ToDouble(it.QtyActual);

                        //        //if (invCodeInit == invCodeActual)
                        //        //{
                        //        //    if (qtyinit > qtyActual)
                        //        //    {
                        //        //        it.InventoryAction = "OUT";
                        //        //    }
                        //        //    else if (qtyinit < qtyActual)
                        //        //    {
                        //        //        it.InventoryAction = "IN";
                        //        //    }
                        //        //    else
                        //        //    {
                        //        //        it.InventoryAction = "NORMAL";
                        //        //    }
                        //        //}
                        //        //else
                        //        //{
                        //        //    it.InventoryAction = "MOVE";
                        //        //}
                        //    }
                        //}
                    }
                }

                // Thêm 1 số thông tin khác
                objRQ_InvF_InvAudit.Tid = GetNextTId();
                objRQ_InvF_InvAudit.GwUserCode = GwUserCode;
                objRQ_InvF_InvAudit.GwPassword = GwPassword;
                objRQ_InvF_InvAudit.AccessToken = CUtils.StrValue(UserState.AccessToken);
                objRQ_InvF_InvAudit.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_InvF_InvAudit.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_InvF_InvAudit.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objRQ_InvF_InvAudit.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objRQ_InvF_InvAudit.UtcOffset = CUtils.StrValue(UserState.UtcOffset);
                //
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_InvAudit);
                InvF_InvAuditService.Instance.InvF_InvAuditDtl_FlagAudit(objRQ_InvF_InvAudit, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Thực hiện phiếu kiểm kê thành công!");
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
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult SerialKiemKe(string productCode, string productCodeUser, string productName, string invBUPattern, string type, string IF_InvAudNo, string viewonly)
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
                ViewBag.viewonly = viewonly;

                #region ["Lấy Danh sách Serial theo hàng hóa và mã kho"]
                var strWhere = "";
                var sbSql = new StringBuilder();
                var Tbl_Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial.";
                var Tbl_Mst_Inventory = "Mst_Inventory.";
                if (!string.IsNullOrEmpty(productCode))
                {
                    sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + "ProductCode", CUtils.StrValue(productCode), "=");
                }
                if (!string.IsNullOrEmpty(invBUPattern))
                {
                    sbSql.AddWhereClause(Tbl_Mst_Inventory + "InvBUPattern", CUtils.StrValue(invBUPattern), "like");
                }
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

                ViewBag.ProductCode = productCode;
                ViewBag.type = type;

                #region Thông tin phiếu kiểm kê
                InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
                {
                    IF_InvAudNo = IF_InvAudNo
                };

                var strWhereAudit = WhereClause(objInvF_InvAudit, null, null, null, null);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "*",
                    Rt_Cols_InvF_InvAuditInstLot = "*",
                    Rt_Cols_InvF_InvAuditInstSerial = "*",
                    Ft_WhereClause = strWhereAudit
                };
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
                var Lst_InvF_InvAuditInstSerial_Actual = new List<InvF_InvAuditInstSerial>();
                if (objRT_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAuditInstSerial != null)
                {
                    var lstSerialTT = objRT_InvF_InvAudit.Lst_InvF_InvAuditInstSerial.Where(x => x.InventoryAction != null && (x.InventoryAction.ToString() == "MOVE" || x.InventoryAction.ToString() == "NORMAL" || x.InventoryAction.ToString() == "IN") && x.ProductCode.Equals(productCode)).ToList();
                    if (lstSerialTT != null)
                    {
                        Lst_InvF_InvAuditInstSerial_Actual = lstSerialTT;
                    }
                }

                if (objRT_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                {
                    var itAuditByProduct = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
                    if (itAuditByProduct != null)
                    {
                        ViewBag.FlagAudit = itAuditByProduct.FlagAudit;
                    }
                }
                #endregion
                ViewBag.Lst_InvF_InvAuditInstSerial_Actual = Lst_InvF_InvAuditInstSerial_Actual;

                string strWhereClause = "Mst_Inventory.FlagActive = 1 and Mst_Inventory.InvBUCode like '" + invBUPattern + "'";
                var listInventory = Mst_Inventory_Get(addressAPIs, strWhereClause);
                ViewBag.Lst_Mst_Inventory = listInventory;
                return JsonView("SerialKiemKe", lstInv_InventoryBalanceSerial);
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
            return JsonViewError("SerialKiemKe", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Lo(string productCode, string productCodeBase, string invBUPattern, string IF_InvAudNo, string productCodeUser, string productName, string viewonly)
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
                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                //
                #region Lấy thông tin Lô theo số kiểm kê
                var objRT_InvF_InvAudit = InvF_InvAuditGetAll(IF_InvAudNo, addressAPIs);
                var Lst_InvF_InvAuditInstLot = new List<InvF_InvAuditInstLot>();
                if (objRT_InvF_InvAudit.Lst_InvF_InvAuditInstLot != null)
                {
                    Lst_InvF_InvAuditInstLot = objRT_InvF_InvAudit.Lst_InvF_InvAuditInstLot.Where(x => x.ProductCode.Equals(productCode)).ToList();
                };
                ViewBag.Lst_InvF_InvAuditInstLot = Lst_InvF_InvAuditInstLot;
                #endregion
                ViewBag.viewonly = viewonly;
                return JsonView("Lo"/*, lstInv_InventoryBalanceLot*/);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }

        [HttpPost]
        public ActionResult LoKiemKe(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string productName, string IF_InvAudNo, string viewonly, string flagAudit)
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
                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                var mstProduct = GetMstProduct(productCode);
                ViewBag.valconvert = mstProduct.ValConvert;
                //
                #region Lấy thông tin Lô theo số kiểm kê
                var objRT_InvF_InvAudit = InvF_InvAuditGetAll(IF_InvAudNo, addressAPIs);
                var Lst_InvF_InvAuditInstLot = new List<InvF_InvAuditInstLot>();
                if (objRT_InvF_InvAudit.Lst_InvF_InvAuditInstLot != null)
                {
                    foreach (var it in objRT_InvF_InvAudit.Lst_InvF_InvAuditInstLot)
                    {
                        if (productCode.Equals(CUtils.StrValue(it.ProductCode)))
                        {
                            Lst_InvF_InvAuditInstLot.Add(it);
                        }
                    }
                };
                ViewBag.Lst_InvF_InvAuditInstLot = Lst_InvF_InvAuditInstLot;
                ViewBag.flagAudit = flagAudit;
                #endregion
                ViewBag.viewonly = viewonly;

                var strWhereMstInventory = "";
                var sqlInventory = new StringBuilder();
                if (invBUPattern != null && invBUPattern != "")
                {
                    sqlInventory.AddWhereClause("Mst_Inventory." + "InvBUPattern", invBUPattern, "like");
                }
                sqlInventory.AddWhereClause("Mst_Inventory." + "FlagActive", "1", "=");
                strWhereMstInventory = sqlInventory.ToString();
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get(addressAPIs, strWhereMstInventory);//Mst_Inventory_Get_Inventory(addressAPIs);
                return JsonView("LoKiemKe");
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
        public ActionResult LoNoKK(string productCode, string productCodeBase, string invBUPattern, string productCodeUser, string productName)
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
                var product = GetMstProduct(productCode);
                var valconvert = 1.0;
                if (product != null)
                {
                    valconvert = product.ValConvert == null ? 1 : Convert.ToDouble(product.ValConvert);
                }

                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
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

                ViewBag.lstInv_InventoryBalanceLot = lstInv_InventoryBalanceLot;
                //

                ViewBag.valconvert = valconvert;
                return JsonView("LoNoKK"/*, lstInv_InventoryBalanceLot*/);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Lo", null, resultEntry);
        }


        [HttpPost]
        public ActionResult Serial(string productCode, string productCodeBase, string invBUPattern, string type, string productCodeUser, string productName,string IF_InvAudNo, string viewonly)
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
                //sbSql.AddWhereClause(Tbl_Mst_Inventory + "FlagActive", "1", "=");
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
                #region Lấy thông tin Serial theo số kiểm kê
                var objRT_InvF_InvAudit = InvF_InvAuditGetAll(IF_InvAudNo, addressAPIs);
                var Lst_InvF_InvAuditInstSerial = new List<InvF_InvAuditInstSerial>();
                if (objRT_InvF_InvAudit.Lst_InvF_InvAuditInstSerial != null)
                {
                    Lst_InvF_InvAuditInstSerial = objRT_InvF_InvAudit.Lst_InvF_InvAuditInstSerial;
                };
                ViewBag.Lst_InvF_InvAuditInstSerial = Lst_InvF_InvAuditInstSerial;
                #endregion
                ViewBag.ProductCode = productCode;
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
                ViewBag.type = type;
                ViewBag.viewonly = viewonly;
                return JsonView("Serial", lstInv_InventoryBalanceSerial);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Serial", null, resultEntry);
        }

        public string WhereClause(InvF_InvAudit objInvF_InvAudit, string strdatefrom, string strdateto, string strApprdatefrom, string strApprdateto)
        {
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
            var strWhere = "";
            var sb = new StringBuilder();
            var tbl_InvF_InvAudit = "InvF_InvAudit.";
            if (objInvF_InvAudit.IF_InvAudNo != null)
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.IF_InvAudNo, objInvF_InvAudit.IF_InvAudNo, "=");
            }
            if (objInvF_InvAudit.InvCodeAudit != null)
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.InvCodeAudit, objInvF_InvAudit.InvCodeAudit, "=");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(datefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(dateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdatefrom))
            {
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(apprdatefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdateto))
            {
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(apprdateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, strApprDTimeUTCTo, "<=");
            }
            strWhere = sb.ToString();
            return strWhere;
        }

        public string WhereClauseExport(InvF_InvAudit objInvF_InvAudit, string lstIF_InvAudNo, string strdatefrom, string strdateto, string strApprdatefrom, string strApprdateto)
        {
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
            var strWhere = "";
            var sb = new StringBuilder();
            var tbl_InvF_InvAudit = "InvF_InvAudit.";
            //if (objInvF_InvAudit.IF_InvAudNo != null)
            //{
            //    sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.IF_InvAudNo, objInvF_InvAudit.IF_InvAudNo, "=");
            //}
            if (lstIF_InvAudNo != null)
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.IF_InvAudNo, lstIF_InvAudNo, "IN");
            }
            if (objInvF_InvAudit.InvCodeAudit != null)
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.InvCodeAudit, objInvF_InvAudit.InvCodeAudit, "=");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, CUtils.StrValue(datefrom), ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, CUtils.StrValue(dateto), "<=");
            }
            if (!CUtils.IsNullOrEmpty(apprdatefrom))
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, CUtils.StrValue(apprdatefrom), ">=");
            }
            if (!CUtils.IsNullOrEmpty(apprdateto))
            {
                sb.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, CUtils.StrValue(apprdateto), "<=");
            }
            strWhere = sb.ToString();
            return strWhere;
        }
        public string strWhereClause_InvFInvAudit_Search(string if_InvAudNo, string createdtimefrom, string createdtimeto, string invCodeAudit, string approvedtimefrom, string approvedtimeto, string productCode, string orgID, string chkPending, string chkApproved, string chkCanceled, string chkFinished)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var tbl_InvF_InvAudit = TableName.InvF_InvAudit + ".";
            var Tbl_Mst_Product = TableName.Mst_Product + ".";


            var datefrom = "";
            if (createdtimefrom != null && createdtimefrom != "")
            {
                datefrom = createdtimefrom + " 00:00:00";
            }
            var dateto = "";
            if (createdtimeto != null && createdtimeto != "")
            {
                dateto = createdtimeto + " 23:59:59";
            }

            if (!CUtils.IsNullOrEmpty(if_InvAudNo))
            {
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.IF_InvAudNo, "%" + CUtils.StrValue(if_InvAudNo) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(datefrom))
            {
                var strCreateDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(datefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, strCreateDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(dateto))
            {
                var strCreateDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(dateto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.CreateDTimeUTC, strCreateDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(approvedtimefrom))
            {
                var strApprDTimeUTCFrom = CUtils.ConvertingLocalTimeToUTC(approvedtimefrom, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, strApprDTimeUTCFrom, ">=");
            }
            if (!CUtils.IsNullOrEmpty(approvedtimeto))
            {
                var strApprDTimeUTCTo = CUtils.ConvertingLocalTimeToUTC(approvedtimeto, UserState.UtcOffset).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.ApprDTimeUTC, strApprDTimeUTCTo, "<=");
            }
            if (!CUtils.IsNullOrEmpty(productCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, "%" + CUtils.StrValue(productCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(invCodeAudit))
            {
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.InvCodeAudit, CUtils.StrValue(invCodeAudit), "=");
            }
            if (!CUtils.IsNullOrEmpty(orgID))
            {
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.OrgID, CUtils.StrValue(orgID), "=");
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
            if (!CUtils.IsNullOrEmpty(chkFinished) && chkFinished.Equals("1"))
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",FINISH";
                }
                else
                {
                    status = "FINISH";
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
                sbSql.AddWhereClause(tbl_InvF_InvAudit + Tbl_InvF_InvAudit.IF_InvAuditStatus, status, "in");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        public RT_InvF_InvAudit InvF_InvAuditGetAll(string IF_InvAudNo, string addressAPIs)
        {
            RT_InvF_InvAudit objRT_InvF_InvAudit = new RT_InvF_InvAudit();
            InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
            {
                IF_InvAudNo = IF_InvAudNo
            };

            var strWhere = WhereClause(objInvF_InvAudit, null, null, null, null);
            var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
            {
                // WARQBase
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
                Rt_Cols_InvF_InvAudit = "*",
                Rt_Cols_InvF_InvAuditDtl = "*",
                Rt_Cols_InvF_InvAuditInstLot = "*",
                Rt_Cols_InvF_InvAuditInstSerial = "*",
                Ft_WhereClause = strWhere
            };
            var result = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);
            if (result != null)
            {
                objRT_InvF_InvAudit = result;
            }
            return objRT_InvF_InvAudit;
        }

        [HttpPost]
        public ActionResult GetTonKho(string productCode, string productCodeBase, string InvBUPattern, string ValConvert, string flagAudit, string IF_InvAudNo, string productCodeUser, string productName)
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
                ViewBag.FlagAudit = flagAudit;
                ViewBag.ProductCodeUser = productCodeUser;
                ViewBag.ProductName = productName;
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
                #region Lấy thông tin tồn kho theo số kiểm kê
                var objRT_InvF_InvAudit = InvF_InvAuditGetAll(IF_InvAudNo, addressAPIs);
                var Lst_InvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                if (objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                {
                    Lst_InvF_InvAuditDtl = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(x => x.ProductCode.Equals(productCode)).ToList();
                };
                ViewBag.Lst_InvF_InvAuditDtl = Lst_InvF_InvAuditDtl;
                if (objRT_InvF_InvAudit.Lst_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAudit.Count > 0)
                {
                    ViewBag.IF_InvAuditStatus = objRT_InvF_InvAudit.Lst_InvF_InvAudit[0].IF_InvAuditStatus;
                }
                #endregion

                #region Lấy thông tin kho con
                var strWhereMstInventory = "";
                var sqlInventory = new StringBuilder();
                if (InvBUPattern != null && InvBUPattern != "")
                {
                    sqlInventory.AddWhereClause("Mst_Inventory." + "InvBUPattern", InvBUPattern, "like");
                }
                sqlInventory.AddWhereClause("Mst_Inventory." + "FlagActive", "1", "=");
                strWhereMstInventory = sqlInventory.ToString();
                ViewBag.Lst_Mst_Inventory = Mst_Inventory_Get(addressAPIs, strWhereMstInventory);
                #endregion
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
        public ActionResult GetTonKhoNoKK(string productcode, string productcodebase, string invbupattern, string valconvert, string productcodeuser, string productname, string viewonly)
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
                var valConvert = valconvert == null ? 0 : Convert.ToDouble(valconvert);
                var strWhereClause = "";
                var tblInv_InventoryBalance = "Inv_InventoryBalance.";
                var tbMst_Inventory = "Mst_Inventory.";
                var sb_SQL = new StringBuilder();
                if (productcodebase != null && productcodebase != "")
                {
                    sb_SQL.AddWhereClause(tblInv_InventoryBalance + "ProductCode", productcodebase, "=");
                }
                if (invbupattern != null && invbupattern != "")
                {
                    //Mst_Inventory.InvBUPattern like '%KHO2%'
                    sb_SQL.AddWhereClause(tbMst_Inventory + "InvBUPattern", invbupattern, "like");
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
                ViewBag.ProductCodeBase = productcodebase;
                ViewBag.ProductCode = productcode;
                ViewBag.viewonly = viewonly;
                ViewBag.ProductCodeUser = productcodeuser;
                ViewBag.ProductName = productname;
                return JsonView("GetTonKhoNoKK", lstInv_InventoryBalance);
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

        #region ["Search autocomplete"]
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
            sbSql.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductType, "PRODUCT", "=");
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
        public ActionResult SearchProduct(string productcode, string productgroup, string InvBUPattern, int recordcount = 10, int page = 0)
        {
            #region ["Product Center Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs;
            #endregion



            var qtytotalok = 0.0;
            var qtytotalok1 = 0.0;
            var invCodeMax = "";
            var invCodeMax1 = "";
            var qtymax = 0.0;

            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_ProductUI>(0, recordcount)
            {
                DataList = new List<Mst_ProductUI>()
            };
            var strWhereClause_ProductGroup = "";
            var sb_sqlProductGroup = new StringBuilder();
            var Tbl_Mst_Product = "Mst_Product.";
            var tbMst_Inventory = "Mst_Inventory.";
            if (!CUtils.IsNullOrEmpty(productgroup))
            {
                sb_sqlProductGroup.AddWhereClause(Tbl_Mst_Product + "ProductGrpCode", productgroup, "IN");
                strWhereClause_ProductGroup = sb_sqlProductGroup.ToString();
            }            

            var strWhere_Mst_Product = strWhereClause_Mst_Product(productcode, "1", "PRODUCT", "");
            if(!CUtils.IsNullOrEmpty(strWhereClause_ProductGroup) && strWhereClause_ProductGroup.Length > 0)
            {
                if (!CUtils.IsNullOrEmpty(strWhere_Mst_Product) && strWhere_Mst_Product.Length > 0)
                {
                    strWhere_Mst_Product = strWhere_Mst_Product + " and " + strWhereClause_ProductGroup;
                }
            }
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
            #region Lấy thông tin tồn kho



          



            #region["Lấy thông tin tồn kho lớn nhất"]
            var listProductCode = List_Mst_Product.Where(it => !CUtils.IsNullOrEmpty(it.ProductCode)).Select(item => string.Format("'{0}'", item.ProductCode)).ToList();
            if(listProductCode != null && listProductCode.Count > 0)
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
                var objInv_InventoryBalance = new Inv_InventoryBalance();
                if (objRT_Inv_InventoryBalance1 != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance != null && objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance.Count > 0)
                {
                    lstInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance;
                    objInv_InventoryBalance = objRT_Inv_InventoryBalance1.Lst_Inv_InventoryBalance[0];
                }




            }

            #endregion
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


                #region["Lấy thông tin tồn kho của từng hàng hoá"]
                var strWhereClauseTonKho1 = "";
                var tblInv_InventoryBalance13 = "Inv_InventoryBalance.";
                var sb_SQL13 = new StringBuilder();
                if (productUI.ProductCodeBase != null && productUI.ProductCodeBase != "")
                {
                    sb_SQL13.AddWhereClause(tblInv_InventoryBalance13 + "ProductCode", CUtils.StrValue(it.ProductCodeBase), "=");
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


                        productUI.InvCode = invCodeMax1;
                        productUI.InvCodeInActual = invCodeMax1;
                    }
                }



                    #endregion
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
                    if (objRT_Mst_ProductBase != null && objRT_Mst_ProductBase.Lst_Mst_Product != null && objRT_Mst_ProductBase.Lst_Mst_Product.Count > 0)
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

        private string strWhereClause_Mst_Product(string productcode = "", string flagactive = "", string producttype = "", string flagsell = "")
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Product = TableName.Mst_Product + ".";
            if (!CUtils.IsNullOrEmpty(productcode))
            {
                var sbSqlProductCode = new StringBuilder();
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, CUtils.StrValue(productcode), "=");
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductName, CUtils.StrValue(productcode), "=", "OR");
                //sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCode, CUtils.StrValue(productcode), "=", "OR");


                sbSqlProductCode.AddWhereClause(Tbl_Mst_Product + TblMst_Product.ProductCodeUser, "%" +  CUtils.StrValue(productcode) + "%", "like");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetInventoryBalance(string productcode, string productcodebase, string valconvert, string bupatternout)
        {
            var lst_Mst_ProductUI = new List<Mst_ProductUI>();
            var lstInv_InventoryBalance1 = new Inv_InventoryBalance();
            var lstPhanBo = new List<Inv_InventoryBalance>();
            var objInv_InventoryMax = new Inv_InventoryBalance();

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
                            else
                            {
                                productUI.QtyTotalOK = 0;
                            }
                            lst_Mst_ProductUI.Add(productUI);
                        }
                        #region ["Vị trí tồn max"]
                        if (lstInv_InventoryBalance != null && lstInv_InventoryBalance.Count > 0)
                        {
                            var qtymax = lstInv_InventoryBalance.Max(x => x.QtyTotalOK == null ? 0 : Convert.ToDouble(x.QtyTotalOK));
                            objInv_InventoryMax = lstInv_InventoryBalance.Where(x => x.QtyTotalOK.Equals(qtymax)).FirstOrDefault();
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                return Json(new { Success = true, Data = lst_Mst_ProductUI, ListPhanBo = lstPhanBo, Inv_InventoryMax = objInv_InventoryMax });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        #endregion

        #region ["In"]
        [HttpPost]
        public ActionResult PrintTemp(string IF_InvAudNo)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]            
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                string logofilepath = "";
                #region Thông tin chi nhánh
                string strWhereClauseNNT = "Mst_NNT.OrgID = '" + CUtils.StrValue(UserState.Mst_NNT.OrgID) + "'";
                var listMst_NNT = List_Mst_NNT(strWhereClauseNNT);
                #endregion
                InvF_InvAudit objInvF_InvAudit = new InvF_InvAudit()
                {
                    IF_InvAudNo = IF_InvAudNo
                };
                var strWhere = WhereClause(objInvF_InvAudit, null, null, null, null);
                var objRQ_InvF_InvAudit = new RQ_InvF_InvAudit()
                {
                    // WARQBase
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
                    Rt_Cols_InvF_InvAudit = "*",
                    Rt_Cols_InvF_InvAuditDtl = "*",
                    Ft_WhereClause = strWhere
                };
                var objRT_InvF_InvAudit = InvF_InvAuditService.Instance.InvF_InvAudit_Get(objRQ_InvF_InvAudit, addressAPIs);

                #region Get List đơn vị tính theo Product + Xử lý list detail UI

                var listInvF_InvAuditDtlUI = new List<InvF_InvAuditDtlUI>();

                var lstInvF_InvAuditDtl = new List<InvF_InvAuditDtl>();
                if (objRT_InvF_InvAudit != null && objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl != null)
                {
                    lstInvF_InvAuditDtl = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl;
                    objInvF_InvAudit = objRT_InvF_InvAudit.Lst_InvF_InvAudit[0];
                }
                var lstPrdDistinct = new List<string>();
                int idx = 1;
                foreach (var item in lstInvF_InvAuditDtl)
                {
                    if (lstPrdDistinct.Contains(item.ProductCode.ToString()))
                        continue;
                    var listInvInDtlCur = lstInvF_InvAuditDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    var listInvDtl = objRT_InvF_InvAudit.Lst_InvF_InvAuditDtl.Where(m => m.ProductCode.ToString() == item.ProductCode.ToString()).ToList();
                    double dQtyInit = 0;
                    double dQtyActual = 0;
                    //var qty = 0.0;
                    var valOutAfterDesc = 0.0;
                    var invCodeInit = "";
                    var invCodeInitName = "";
                    var invCodeActual = "";
                    var invCodeActualName = "";
                    var listViTriInit = new List<string>();
                    var listViTriActual = new List<string>();
                    foreach (var it in listInvDtl)
                    {
                        dQtyInit += Convert.ToDouble(it.QtyInit);
                        dQtyActual += Convert.ToDouble(it.QtyActual);
                        if (!listViTriInit.Contains(it.InvCodeInit.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeInit))
                            {
                                invCodeInit += ", " + it.InvCodeInit.ToString();
                                invCodeInitName += ", " + it.InvCodeInit.ToString(); // Tam de

                            }
                            else
                            {

                                invCodeInit = it.InvCodeInit.ToString();
                                invCodeInitName = it.InvCodeInit.ToString(); // Tam de

                            }
                            listViTriInit.Add(it.InvCodeActual.ToString());
                        }
                        if (!listViTriActual.Contains(it.InvCodeActual.ToString()))
                        {
                            if (!string.IsNullOrEmpty(invCodeActual))
                            {
                                invCodeActual += ", " + it.InvCodeActual.ToString();
                                invCodeActualName += ", " + it.InvCodeActual.ToString(); // Tam de
                            }
                            else
                            {
                                invCodeActual = it.InvCodeActual.ToString();
                                invCodeActualName = it.InvCodeActual.ToString();
                            }
                            listViTriActual.Add(it.InvCodeActual.ToString());
                        }
                    }
                    var flagAuditName = "Chưa kiểm";
                    var flagAudit = CUtils.StrValue(item.FlagAudit);
                    if (flagAudit.Equals("1"))
                    {
                        flagAuditName = "Đã kiểm";
                    }
                    
                    double dQtyRemain = dQtyActual - dQtyInit;
                    var itemUI = new InvF_InvAuditDtlUI()
                    {
                        IF_InvAudNo = item.IF_InvAudNo,
                        ProductCode = item.ProductCode,
                        NetworkID = NetworkID,
                        UnitCode = item.UnitCode,
                        IF_InvAuditStatusDtl = item.IF_InvAuditStatusDtl,
                        Remark = item.Remark,
                        mp_ProductName = item.mp_ProductName,
                        mp_ProductCodeUser = item.mp_ProductCodeUser,
                        InvName = invCodeActualName,
                        Idx = idx,
                        QtyInit = dQtyInit,
                        QtyActual = dQtyActual,
                        QtyRemain = dQtyRemain,
                        InvCodeInit = invCodeInit,
                        InvCodeActual = invCodeActual,
                        InvCodeInitName = invCodeInitName,
                        InvCodeActualName = invCodeActualName,
                        FlagAuditName = flagAuditName,
                    };
                    //
                    listInvF_InvAuditDtlUI.Add(itemUI);
                    idx++;
                    lstPrdDistinct.Add(item.ProductCode.ToString());
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

                InvF_InvAuditPrint objPrint = new InvF_InvAuditPrint();
                objPrint.NNTFullName = strNNTFullName;
                objPrint.NNTAddress = strNNTAddress;
                objPrint.NNTPhone = strNNTPhone;
                objPrint.NNTFullName = strNNTFullName;
                objPrint.IF_InvAudNo = objInvF_InvAudit.IF_InvAudNo;
                DateTime dtNow = DateTime.Now;
                objPrint.DatePrint = dtNow.ToString("dd");
                objPrint.MonthPrint = dtNow.ToString("MM");
                objPrint.YearPrint = dtNow.ToString("yyyy");
                objPrint.Remark = objInvF_InvAudit.Remark;
                objPrint.InvCodeAudit = objInvF_InvAudit.InvCodeAudit;
                objPrint.LogoFilePath = "";
                objPrint.Lst_InvF_InvAuditDtlUI = listInvF_InvAuditDtlUI;
                
                #region Lấy mẫu in
                string linkPdf = "";
                var listInvF_TempPrint = ListInvF_TempPrint("KK");
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    var printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        logofilepath = addressAPIs + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objPrint.LogoFilePath = logofilepath;
                        objPrint.LogoFilePath = @"https://icdn.dantri.com.vn/zoom/516_344/2023/01/05/2904beffc76c1f32467d-1672885961775.jpg";
                    }
                    data.data = CreateDataObjectReportServer(objPrint, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    string content = PostReport(data);

                    string serverUrl = ReportBro_ServerUrl;
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        if (!content.Contains("errors"))
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
                        else
                        {
                            resultEntry.SetFailed();
                            resultEntry.AddMessage("File In view lỗi '" + logofilepath + "'");
                            return Json(resultEntry);
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
        public dynamic CreateDataObjectReportServer(InvF_InvAuditPrint objTempPrint, ref string watermark)
        {
            string defaultFormat = "{0:0,0}";
            var tableName = TableName.InvF_InvAuditDtl;
            string strFormatQty = NumericFormat(tableName, "TotalQty", defaultFormat);
            var MyDynamic = new InvF_InvAuditReportServer();
            if (objTempPrint != null)
            {
                MyDynamic.NNTFullName = CUtils.StrValueNew(objTempPrint.IF_InvAudNo);
                MyDynamic.NNTAddress = CUtils.StrValueNew(objTempPrint.NNTAddress);
                MyDynamic.NNTPhone = CUtils.StrValueNew(objTempPrint.NNTPhone);
                MyDynamic.IF_InvAudNo = CUtils.StrValueNew(objTempPrint.IF_InvAudNo);
                MyDynamic.DatePrint = CUtils.StrValueNew(objTempPrint.DatePrint);
                MyDynamic.MonthPrint = CUtils.StrValueNew(objTempPrint.MonthPrint);
                MyDynamic.YearPrint = CUtils.StrValueNew(objTempPrint.YearPrint);
                MyDynamic.Remark = CUtils.StrValueNew(objTempPrint.Remark);
                MyDynamic.CreateUserName = CUtils.StrValueNew(objTempPrint.CreateUserName);
                MyDynamic.LogoFilePath = CUtils.StrValueNew(objTempPrint.LogoFilePath);
            }

            MyDynamic.DataTable = new List<InvF_InvAuditDtlReportServer>();

            if (objTempPrint != null && objTempPrint.Lst_InvF_InvAuditDtlUI != null && objTempPrint.Lst_InvF_InvAuditDtlUI.Count > 0)
            {
                var listDtl_ReportServer = new List<InvF_InvAuditDtlReportServer>();
                foreach (var item in objTempPrint.Lst_InvF_InvAuditDtlUI)
                {
                    var objDtl_ReportServer = new InvF_InvAuditDtlReportServer
                    {
                        Idx = CUtils.StrValue(item.Idx),
                        ProductCodeUser = CUtils.StrValue(item.mp_ProductCodeUser),
                        ProductName = CUtils.StrValue(item.mp_ProductName),
                        UnitCode = CUtils.StrValue(item.UnitCode),
                        InvName = CUtils.StrValue(item.InvName),
                        QtyActual = CUtils.StrValueFormatNumber(item.QtyActual, NumericFormat(tableName, "QtyActual", defaultFormat)),
                        QtyInit = CUtils.StrValueFormatNumber(item.QtyInit, NumericFormat(tableName, "QtyInit", defaultFormat)),
                        QtyRemain = CUtils.StrValueFormatNumber(item.QtyRemain, NumericFormat(tableName, "QtyRemain", defaultFormat)),
                        InvCodeInit = CUtils.StrValue(item.InvCodeInit),
                        InvCodeActual = CUtils.StrValue(item.InvCodeActual),
                        InvCodeInitName = CUtils.StrValue(item.InvCodeInitName),
                        InvCodeActualName = CUtils.StrValue(item.InvCodeActualName),

                        FlagAuditName = CUtils.StrValue(item.FlagAuditName),
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