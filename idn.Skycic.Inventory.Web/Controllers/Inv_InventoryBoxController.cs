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
    public class Inv_InventoryBoxController : AdminBaseController
    {
        // GET: Inv_InventoryBox
        public ActionResult Index(string orgid = "", int qty = 100, string fromdate = "", string todate = "", string tt = "",string ttnk ="", string qr_hop ="", string init = "init", int page = 0, int recordcount = 10)
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
            var pageInfo = new PageInfo<Inv_InventoryBox>(0, PageSizeConfig)
            {
                DataList = new List<Inv_InventoryBox>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objInv_InventoryBoxUI = new Inv_InventoryBoxUI()
                {
                    OrgID = orgid,
                    FromDate = fromdate,
                    ToDate = todate,
                    FlagUsed = tt,
                    QR_BoxNo = qr_hop,
                    FlagMap = ttnk,
                };
                var strWhereClauseInv_InventoryBox = strWhereClause_Inv_InventoryBox(objInv_InventoryBoxUI);
                var objRQ_Inv_InventoryBox = new RQ_Inv_InventoryBox()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventoryBox,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_Inv_InventoryBox = "*",
                    Inv_InventoryBox = null,
                    nTop = qty.ToString().Trim(),
                };
                //var qt = qty.convert
                var objRT_Inv_InventoryBoxCur = Inv_InventoryBoxService.Instance.WA_Inv_InventoryBox_Get(objRQ_Inv_InventoryBox, addressAPIs);
                if (objRT_Inv_InventoryBoxCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Inv_InventoryBoxCur.MySummaryTable.MyCount);
                }
                if (objRT_Inv_InventoryBoxCur != null && objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox != null && objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox);
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
            ViewBag.FlagMap = ttnk;
            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.QR_BoxNo = qr_hop;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }
        
        #region ["Tao moi"]
        [HttpPost]
        public ActionResult ShowPopupCreate(string Orgid)
        {

            var resultEntry = new JsonResultEntry() { Success = false };
            var InvInventoryBox = new Inv_InventoryBox();
            InvInventoryBox.OrgID = Orgid;
            try
            {
                return JsonView("_Create", InvInventoryBox);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_Create", null, resultEntry);
        }
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPI = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var networkID = userState.SysUser.NetworkID;
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
                var gentimesboxno = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPI);
                var obj_Inv_GenTimesBox = Newtonsoft.Json.JsonConvert.DeserializeObject<Inv_GenTimesBox>(model);
                obj_Inv_GenTimesBox.GenTimesBoxNo = gentimesboxno.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                var objRQ_Inv_GenTimesBox = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Inv_GenTimesBox>(model);
                if (objRQ_Inv_GenTimesBox != null)
                {
                    objRQ_Inv_GenTimesBox.Tid = GetNextTId();
                    objRQ_Inv_GenTimesBox.GwUserCode = GwUserCode;
                    objRQ_Inv_GenTimesBox.GwPassword = GwPassword;
                    objRQ_Inv_GenTimesBox.FuncType = null;
                    objRQ_Inv_GenTimesBox.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_Inv_GenTimesBox.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_Inv_GenTimesBox.Ft_Cols_Upd = null;
                    objRQ_Inv_GenTimesBox.WAUserCode = waUserCode;
                    objRQ_Inv_GenTimesBox.WAUserPassword = waUserPassword;
                    objRQ_Inv_GenTimesBox.UtcOffset = userState.UtcOffset;
                    // RQ_objRQ_Inv_GenTimesBox
                    objRQ_Inv_GenTimesBox.Rt_Cols_Inv_GenTimesBox = null;
                    objRQ_Inv_GenTimesBox.Inv_GenTimesBox = obj_Inv_GenTimesBox;

                    Inv_GenTimesBoxService.Instance.WA_Inv_GenTimesBox_Add(objRQ_Inv_GenTimesBox, addressAPI);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sinh mã hộp thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
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
        public ActionResult Export(string orgid = "", int qty = 100, string fromdate = "", string todate = "", string tt = "", string ttnk = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listInv_InventoryBoxSearch = new List<Inv_InventoryBox>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var offset = Offset;

            try
            {
                #region["Search"]

                var objInv_InventoryBoxUI = new Inv_InventoryBoxUI()
                {
                    OrgID = orgid,
                    FromDate = fromdate,
                    ToDate = todate,
                    FlagUsed = tt,
                    FlagMap = ttnk,
                };
                var strWhereClauseInv_InventoryBox = strWhereClause_Inv_InventoryBox(objInv_InventoryBoxUI);
                var objRQ_Inv_InventoryBox = new RQ_Inv_InventoryBox()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventoryBox,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_Inv_InventoryBox = "*",
                    Inv_InventoryBox = null,
                    nTop=qty.ToString().Trim(),
                };
                var objRT_Inv_InventoryBoxCur = Inv_InventoryBoxService.Instance.WA_Inv_InventoryBox_Get(objRQ_Inv_InventoryBox, addressAPIs);
                if (objRT_Inv_InventoryBoxCur != null)
                {
                    if (objRT_Inv_InventoryBoxCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Inv_InventoryBoxCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox != null && objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }
                    listInv_InventoryBoxSearch.AddRange(objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox);
                    #endregion

                    var list = new List<Inv_InventoryBox>();
                    if (objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox != null)
                    {
                        var listCur = new List<Inv_InventoryBox>();
                        foreach (var it in objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox)
                        {
                            var obj = new Inv_InventoryBox();
                            if (it.FlagUsed.Equals("0"))
                                {
                                obj.QR_BoxNo = it.QR_BoxNo;
                                var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                obj.LogLUDTimeUTC = createDTimeUTC;
                                obj.FlagUsed = "đã sử dụng";
                                listCur.Add(obj);
                            }
                            else
                            { 
                                return Json(new { Success = true, Title = "mã hộp " + it.BoxNo + " đã được sử dụng!", CheckSuccess = "1" });
                            }

                        }
                        #region["UpdateFlagUsed"]
                        var objRQ_Inv_InventoryBoxupdate = new RQ_Inv_InventoryBox();
                        objRQ_Inv_InventoryBoxupdate.Tid = GetNextTId();
                        objRQ_Inv_InventoryBoxupdate.GwUserCode = GwUserCode;
                        objRQ_Inv_InventoryBoxupdate.GwPassword = GwPassword;
                        objRQ_Inv_InventoryBoxupdate.FuncType = null;
                        objRQ_Inv_InventoryBoxupdate.Ft_RecordStart = Ft_RecordStartExportExcel;
                        objRQ_Inv_InventoryBoxupdate.Ft_RecordCount = RowsWorksheets.ToString();
                        objRQ_Inv_InventoryBoxupdate.Ft_Cols_Upd = null;
                        objRQ_Inv_InventoryBoxupdate.WAUserCode = waUserCode;
                        objRQ_Inv_InventoryBoxupdate.WAUserPassword = waUserPassword;
                        objRQ_Inv_InventoryBoxupdate.UtcOffset = userState.UtcOffset;
                        // RQ_InvF_InventoryInFG
                        objRQ_Inv_InventoryBoxupdate.Rt_Cols_Inv_InventoryBox = null;
                        objRQ_Inv_InventoryBoxupdate.Lst_Inv_InventoryBox = objRT_Inv_InventoryBoxCur.Lst_Inv_InventoryBox;

                        Inv_InventoryBoxService.Instance.WA_Inv_InventoryBox_UpdateFlagUsed(objRQ_Inv_InventoryBoxupdate, addressAPIs);
                        #endregion

                        list.AddRange(listCur);
                    }
                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBox).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Inv_InventoryBox"));
                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }

                

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
                 {"QR_BoxNo","Mã hộp"},
                 {"LogLUDTimeUTC","Ngày tạo"},
                 {"FlagUsed","Trạng thái"},
            };
        }
        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_Inv_InventoryBox(Inv_InventoryBoxUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBox = TableName.Inv_InventoryBox + ".";

            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagUsed))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.FlagUsed, CUtils.StrValueOrNull(data.FlagUsed), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagMap))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.FlagMap, CUtils.StrValueOrNull(data.FlagMap), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                var CreateDTimeUTCFrom = data.FromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.LogLUDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                var CreateDTimeUTCTo = data.ToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.LogLUDTimeUTC, strToDate, "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.BoxNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.BoxNo, CUtils.StrValueOrNull(data.BoxNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.QR_BoxNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.QR_BoxNo, CUtils.StrValueOrNull(data.QR_BoxNo), "=");
            }
            //if (!CUtils.IsNullOrEmpty(data.IF_InvInFGStatus))
            //{
            //    sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.IF_InvInFGStatus, CUtils.StrValueOrNull(data.IF_InvInFGStatus), "=");
            //}
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
        
    }
}