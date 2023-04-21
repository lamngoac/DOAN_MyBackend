using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Inv_InventoryCartonController : AdminBaseController
    {
        // GET: Inv_InventoryCarton
        public ActionResult Index(string orgid = "", int qty = 100, string fromdate = "", string todate = "", string tt = "",string ttnk ="", string qr_thung ="", string init = "init", int page = 0, int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var NetworkID = userState.SysUser.NetworkID;
            var Orgid = userState.Mst_NNT.OrgID;
            ViewBag.OrgID = Orgid;
            ViewBag.Offset = Offset;
            var pageInfo = new PageInfo<Inv_InventoryCarton>(0, PageSizeConfig)
            {
                DataList = new List<Inv_InventoryCarton>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objInv_InventoryCartonUI = new Inv_InventoryCartonUI()
                {
                    OrgID = orgid,
                    FromDate = fromdate,
                    ToDate = todate,
                    FlagUsed = tt,
                    QR_CanNo = qr_thung,
                    FlagMap = ttnk,
                };
                var strWhereClauseInv_InventoryCarton = strWhereClause_Inv_InventoryCarton(objInv_InventoryCartonUI);
                var objRQ_Inv_InventoryCarton = new RQ_Inv_InventoryCarton()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventoryCarton,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_Inv_InventoryCarton = "*",
                    Inv_InventoryCarton = null,
                    nTop = qty.ToString().Trim(),
                };
                //var qt = qty.convert
                var objRT_Inv_InventoryCartonCur = Inv_InventoryCartonService.Instance.WA_Inv_InventoryCarton_Get(objRQ_Inv_InventoryCarton, addressAPIs);
                if (objRT_Inv_InventoryCartonCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Inv_InventoryCartonCur.MySummaryTable.MyCount);
                }
                if (objRT_Inv_InventoryCartonCur != null && objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton != null && objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                    //pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {

                init = "search";
                fromdate = DateToSearch();
            }
            ViewBag.PageCur = page.ToString();
            ViewBag.Qty = qty;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.FlagUsed = tt;
            ViewBag.QR_CanNo = qr_thung;
            ViewBag.FlagMap = ttnk;
            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }
        
        #region ["Tao moi"]
        [HttpPost]
        public ActionResult ShowPopupCreate(string Orgid)
        {

            var resultEntry = new JsonResultEntry() { Success = false };
            var InvInventoryCarton = new Inv_InventoryCarton();
            InvInventoryCarton.NetworkID = Orgid;
            try
            {
                return JsonView("_Create", InvInventoryCarton);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_Create", null, resultEntry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkID = userState.SysUser.NetworkID;
            try
            {
                var objSeq = new Seq_Common { SequenceType = SeqType.GenTimesNo, Param_Postfix = "", Param_Prefix = "" };
                var objRQ_Seq_Common = new RQ_Seq_Common()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Seq_Common = objSeq,
                };
                var gentimesCanNo = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPI);
                var obj_Inv_GenTimesCarton = Newtonsoft.Json.JsonConvert.DeserializeObject<Inv_GenTimesCarton>(model);
                obj_Inv_GenTimesCarton.GenTimesCartonNo = gentimesCanNo.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                obj_Inv_GenTimesCarton.NetworkID = userState.SysUser.NetworkID;
                var objRQ_Inv_GenTimesCarton = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Inv_GenTimesCarton>(model);
                if (objRQ_Inv_GenTimesCarton != null)
                {
                    objRQ_Inv_GenTimesCarton.Tid = GetNextTId();
                    objRQ_Inv_GenTimesCarton.GwUserCode = GwUserCode;
                    objRQ_Inv_GenTimesCarton.GwPassword = GwPassword;
                    objRQ_Inv_GenTimesCarton.FuncType = null;
                    objRQ_Inv_GenTimesCarton.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_Inv_GenTimesCarton.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_Inv_GenTimesCarton.Ft_Cols_Upd = null;
                    objRQ_Inv_GenTimesCarton.WAUserCode = waUserCode;
                    objRQ_Inv_GenTimesCarton.WAUserPassword = waUserPassword;
                    objRQ_Inv_GenTimesCarton.UtcOffset = userState.UtcOffset;
                    // RQ_objRQ_Inv_GenTimesCarton
                    objRQ_Inv_GenTimesCarton.Rt_Cols_Inv_GenTimesCarton = null;
                    objRQ_Inv_GenTimesCarton.Inv_GenTimesCarton = obj_Inv_GenTimesCarton;

                    Inv_GenTimesCartonService.Instance.WA_Inv_GenTimesCarton_Add(objRQ_Inv_GenTimesCarton, addressAPI);
                }
                resultEntry.Success = true;
                resultEntry.RedirectUrl = Url.Action("Index");
                resultEntry.AddMessage("Sinh mã hộp thành công!");
                
                //return Json(new { Success = true, Title = "Sinh mã hộp thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (ServiceException e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["Excel export"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string orgid = "", int qty = 100, string fromdate = "", string todate = "", string tt = "", string ttnk = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listInv_InventoryCartonSearch = new List<Inv_InventoryCarton>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var offset = Offset;

            try
            {
                #region["Search"]

                var objInv_InventoryCartonUI = new Inv_InventoryCartonUI()
                {
                    //OrgID = orgid,
                    FromDate = fromdate,
                    ToDate = todate,
                    FlagUsed = tt,
                    FlagMap = ttnk,
                };
                var strWhereClauseInv_InventoryCarton = strWhereClause_Inv_InventoryCarton(objInv_InventoryCartonUI);
                var objRQ_Inv_InventoryCarton = new RQ_Inv_InventoryCarton()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventoryCarton,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_Inv_InventoryCarton = "*",
                    Inv_InventoryCarton = null,
                    nTop = qty.ToString().Trim(),
                };
                //var qt = qty.convert
                var objRT_Inv_InventoryCartonCur = Inv_InventoryCartonService.Instance.WA_Inv_InventoryCarton_Get(objRQ_Inv_InventoryCarton, addressAPIs);
                if (objRT_Inv_InventoryCartonCur != null)
                {
                    if (objRT_Inv_InventoryCartonCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Inv_InventoryCartonCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton != null && objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }
                    listInv_InventoryCartonSearch.AddRange(objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton);
                    var list = new List<Inv_InventoryCarton>();
                    if (objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton != null)
                    {
                        var listCur = new List<Inv_InventoryCarton>();
                        foreach (var it in objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton)
                        {
                            var obj = new Inv_InventoryCarton();
                            if (it.FlagUsed.Equals("0"))
                            {
                                obj.QR_CanNo = it.QR_CanNo;
                                var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                obj.LogLUDTimeUTC = createDTimeUTC;
                                obj.FlagUsed = "đã sử dụng";
                                listCur.Add(obj);
                            }
                            else
                            {
                                return Json(new { Success = true, Title = "mã hộp " + it.CanNo + " đã được sử dụng!", CheckSuccess = "1" });
                            }

                        }
                        #region["UpdateFlagUsed"]
                        var objRQ_Inv_InventoryCartonupdate = new RQ_Inv_InventoryCarton();
                        objRQ_Inv_InventoryCartonupdate.Tid = GetNextTId();
                        objRQ_Inv_InventoryCartonupdate.GwUserCode = GwUserCode;
                        objRQ_Inv_InventoryCartonupdate.GwPassword = GwPassword;
                        objRQ_Inv_InventoryCartonupdate.FuncType = null;
                        objRQ_Inv_InventoryCartonupdate.Ft_RecordStart = Ft_RecordStartExportExcel;
                        objRQ_Inv_InventoryCartonupdate.Ft_RecordCount = RowsWorksheets.ToString();
                        objRQ_Inv_InventoryCartonupdate.Ft_Cols_Upd = null;
                        objRQ_Inv_InventoryCartonupdate.WAUserCode = waUserCode;
                        objRQ_Inv_InventoryCartonupdate.WAUserPassword = waUserPassword;
                        objRQ_Inv_InventoryCartonupdate.UtcOffset = userState.UtcOffset;
                        // RQ_InvF_InventoryInFG
                        objRQ_Inv_InventoryCartonupdate.Rt_Cols_Inv_InventoryCarton = null;
                        objRQ_Inv_InventoryCartonupdate.Lst_Inv_InventoryCarton = objRT_Inv_InventoryCartonCur.Lst_Inv_InventoryCarton;

                        Inv_InventoryCartonService.Instance.WA_Inv_InventoryCarton_UpdateFlagUsed(objRQ_Inv_InventoryCartonupdate, addressAPIs);
                        #endregion
                        
                        list.AddRange(listCur);
                    }
                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryCarton).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Inv_InventoryCarton"));

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

        #region ["GetDicColumns"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"QR_CanNo","Mã thùng"},
                 {"LogLUDTimeUTC","Ngày tạo"},
                 {"FlagUsed","Trạng thái"},
            };
        }
        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_Inv_InventoryCarton(Inv_InventoryCartonUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryCarton = TableName.Inv_InventoryCarton + ".";

            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagUsed))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.FlagUsed, CUtils.StrValueOrNull(data.FlagUsed), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagMap))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.FlagMap, CUtils.StrValueOrNull(data.FlagMap), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                var CreateDTimeUTCFrom = data.FromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.LogLUDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                var CreateDTimeUTCTo = data.ToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.LogLUDTimeUTC, strToDate, "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.CanNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.CanNo, CUtils.StrValueOrNull(data.CanNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.QR_CanNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.QR_CanNo, CUtils.StrValueOrNull(data.QR_CanNo), "=");
            }
            //if (!CUtils.IsNullOrEmpty(data.IF_InvInFGStatus))
            //{
            //    sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.IF_InvInFGStatus, CUtils.StrValueOrNull(data.IF_InvInFGStatus), "=");
            //}
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
        
    }
}