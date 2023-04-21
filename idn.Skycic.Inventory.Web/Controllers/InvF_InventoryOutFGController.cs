using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Controllers;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class InvF_InventoryOutFGController : AdminBaseController
    {
        // GET: InvF_InventoryOutFG
        public ActionResult Index(string ifinvoutfgno = "", string partcode = "", string fromdate = "", string todate = "",string apprfromdate ="",string apprtodate ="",
            string formouttype = "", string invfouttype = "", string status = "", string init = "init", int page = 0, int recordcount = 10)
        {
            ViewBag.PageCur = page.ToString();

            //init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = userState.AddressAPIs;

            #region ["Danh sách sản phẩm"]
            var objRQ_Mst_Part = new RQ_Mst_Part()
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
                // RQ_InvF_InventoryOutFG
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var listPart = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            ViewBag.ListPart = listPart.Lst_Mst_Part;
            #endregion
            ViewBag.Offset = Offset;

            var pageInfo = new PageInfo<InvF_InventoryOutFG>(0, recordcount)
            {
                DataList = new List<InvF_InventoryOutFG>()
            };
            var itemCount = 0;
            var pageCount = 0;

            if (init != "init")
            {
                var objInvF_InventoryOutFGUI = new InvF_InventoryOutFGUI()
                {
                    FromDate = fromdate,
                    ToDate = todate,
                    PartCode = partcode,
                    FormOutType = formouttype,
                    InvFOutType = invfouttype,
                    IF_InvOutFGStatus = status,
                    IF_InvOutFGNo = ifinvoutfgno,
                    ApprFromDate = apprfromdate,
                    ApprToDate = apprtodate,
                };
                var strWhereClause_InvF_InventoryOutFG = strWhereClause_InvFInventoryOutFG(objInvF_InventoryOutFGUI);
                var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause_InvF_InventoryOutFG,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_InvF_InventoryOutFG = "*",
                    Rt_Cols_InvF_InventoryOutFGDtl = "*",
                    Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
                };
                var objRT_InvF_InventoryOutFG = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                if (objRT_InvF_InventoryOutFG.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_InvF_InventoryOutFG.MySummaryTable.MyCount);
                }
                if (objRT_InvF_InventoryOutFG != null && objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG != null && objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
                apprfromdate = DateToSearch();
            }
            ViewBag.IF_InvOutFGNo = ifinvoutfgno;
            ViewBag.PartCode = partcode;
            ViewBag.ApprFromDate = apprfromdate;
            ViewBag.ApprToDate = apprtodate;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.FormOutType = formouttype;
            ViewBag.InvFOutType = invfouttype;
            ViewBag.IF_InvOutFGStatus = status;

            #region ["PageInfo"]
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            #endregion

            return View(pageInfo);
        }

        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            ViewBag.Offset = Offset;

            var objSeq = new Seq_Common { SequenceType = SeqType.IFInvOutFGNo, Param_Postfix = "", Param_Prefix = "" };
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
            var ifInvOutFGNo = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPI);
            ViewBag.Today = Today;
            ViewBag.IF_InvOutFGNo = ifInvOutFGNo.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            ViewBag.MST = userState.MST;
            #region[kho]
            var objMst_Inventory = new Mst_Inventory()
            {
                FlagActive = "1",
            };
            var strWhere_Mst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhere_Mst_Inventory,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_Inventory = "*"
            };
            var listInven = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPI);
            ViewBag.LstInven = listInven.Lst_Mst_Inventory;
            #endregion

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                //var objInvF_InventoryOutFG = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryOutFG>(model);
                var objRQ_InvF_InventoryOutFG = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryOutFG>(model);
                foreach (var qty in objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl)
                {
                    if (Convert.ToInt32(qty.Qty) <= 0)
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Số lượng của " + qty.PartCode + " phải là số nguyên dương!");
                        return Json(resultEntry);
                    }

                }
                if (objRQ_InvF_InventoryOutFG != null)
                {
                    objRQ_InvF_InventoryOutFG.Tid = GetNextTId();
                    objRQ_InvF_InventoryOutFG.GwUserCode = GwUserCode;
                    objRQ_InvF_InventoryOutFG.GwPassword = GwPassword;
                    objRQ_InvF_InventoryOutFG.FuncType = null;
                    objRQ_InvF_InventoryOutFG.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_InvF_InventoryOutFG.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_InvF_InventoryOutFG.Ft_Cols_Upd = null;
                    objRQ_InvF_InventoryOutFG.WAUserCode = waUserCode;
                    objRQ_InvF_InventoryOutFG.WAUserPassword = waUserPassword;
                    objRQ_InvF_InventoryOutFG.UtcOffset = userState.UtcOffset;

                    objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.MST = userState.MST;
                    objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.InvFOutType = InvFOutType.OutThuongMai;
                    objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.FormOutType = FormOutType.KhongMaVach;

                    InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Save(objRQ_InvF_InventoryOutFG, addressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Tạo phiếu xuất kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Phiếu xuất kho không hợp lệ!");
                }

            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["Sửa"]
        [HttpGet]
        public ActionResult Update(string ifinvoutfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            ViewBag.Offset = Offset;
            var mst = userState.MST;
            ViewBag.MST = userState.MST;

            if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
            {
                var LstInvF_InventoryOutFG = new List<InvF_InventoryOutFG>();
                var LstInvF_InventoryOutFGDtl = new List<InvF_InventoryOutFGDtl>();

                var objInvF_InventoryOutFGUI = new InvF_InventoryOutFGUI()
                {
                    IF_InvOutFGNo = ifinvoutfgno,
                };
                var strWhereInvF_InventoryOutFGGetDetal = strWhereInvF_InventoryOutFGGetDtl(objInvF_InventoryOutFGUI);
                var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereInvF_InventoryOutFGGetDetal,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_InvF_InventoryOutFG = "*",
                    Rt_Cols_InvF_InventoryOutFGDtl = "*",
                    Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
                    InvF_InventoryOutFG = null,
                };
                var objRT_InvF_InventoryOutFG = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                var listDtl = new List<InvF_InventoryOutFGDtlUI>();
                if (objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl != null)
                {
                    var listDtlCur = new List<InvF_InventoryOutFGDtlUI>();
                    foreach (var it in objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl)
                    {
                        var obj = new InvF_InventoryOutFGDtlUI();
                        obj.IF_InvOutFGNo = it.IF_InvOutFGNo;
                        obj.PartCode = it.PartCode;

                        var objMst_Part = new Mst_Part()
                        {
                            PartCode = it.PartCode,
                        };
                        var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
                        var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                        obj.PartName = listPart[0].PartName;
                        obj.PartNameFS = listPart[0].PartNameFS;
                        obj.CreateDTimeSv = it.LogLUDTimeUTC;
                        obj.Qty = it.Qty;

                        listDtlCur.Add(obj);
                    }
                    listDtl.AddRange(listDtlCur);
                }

                if (objRT_InvF_InventoryOutFG != null)
                {
                    ViewBag.List = objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG;
                    ViewBag.ListDtl = listDtl;

                }
                var objMst_Inventory = new Mst_Inventory()
                {
                    InvCode = objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG[0].InvCode,
                };
                var strWhereMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereMst_Inventory,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_Inventory = "*"
                };
                var listInven = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                ViewBag.LstInven = listInven.Lst_Mst_Inventory;
                return View(objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG[0]);
            }

            return View(new InvF_InventoryOutFGDtlUI());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var objRQ_InvF_InventoryOutFG = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryOutFG>(model);
                foreach (var qty in objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl)
                {
                    if (Convert.ToInt32(qty.Qty) <= 0)
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Số lượng của " + qty.PartCode + " phải là số nguyên dương!");
                        return Json(resultEntry);
                    }

                }
                if (objRQ_InvF_InventoryOutFG != null)
                {
                    objRQ_InvF_InventoryOutFG.Tid = GetNextTId();
                    objRQ_InvF_InventoryOutFG.GwUserCode = GwUserCode;
                    objRQ_InvF_InventoryOutFG.GwPassword = GwPassword;
                    objRQ_InvF_InventoryOutFG.FuncType = null;
                    objRQ_InvF_InventoryOutFG.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_InvF_InventoryOutFG.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_InvF_InventoryOutFG.Ft_Cols_Upd = null;
                    objRQ_InvF_InventoryOutFG.WAUserCode = waUserCode;
                    objRQ_InvF_InventoryOutFG.WAUserPassword = waUserPassword;
                    objRQ_InvF_InventoryOutFG.UtcOffset = userState.UtcOffset;

                    InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Save(objRQ_InvF_InventoryOutFG, addressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa phiếu xuất kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Phiếu xuất kho không hợp lệ!");
                }

            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["Duyệt"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approved(string ifinvoutfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {

                if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
                {
                    var title = "";
                    if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
                    {
                        var InvF_InventoryOutFGNO = new InvF_InventoryOutFG()
                        {
                            IF_InvOutFGNo = ifinvoutfgno,
                        };
                        var strWhereInvF_InventoryOutFGGetDetal = strWhereInvF_InventoryOutFGGetDtl(InvF_InventoryOutFGNO);
                        var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStartExportExcel,
                            Ft_RecordCount = RowsWorksheets.ToString(),
                            Ft_WhereClause = strWhereInvF_InventoryOutFGGetDetal,
                            Ft_Cols_Upd = null,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_InvF_InventoryOutFG
                            Rt_Cols_InvF_InventoryOutFG = "*",
                            Rt_Cols_InvF_InventoryOutFGDtl = "*",
                            InvF_InventoryOutFG = InvF_InventoryOutFGNO,

                        };
                        var list = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                        if (list != null)
                        {
                            InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Approve(objRQ_InvF_InventoryOutFG, addressAPIs);
                            title = "Duyệt phiếu xuất '" + ifinvoutfgno + "' thành công!";
                            resultEntry.Success = true;
                            resultEntry.AddMessage(title);
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                        else
                        {
                            title = "Mã phiếu xuất '" + ifinvoutfgno + "' không có trong hệ thống!";
                            resultEntry.AddMessage(title);
                        }
                    }
                    else
                    {
                        title = "Mã phiếu xuất trống!";
                        resultEntry.AddMessage(title);
                    }
                }
            }
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string ifinvoutfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;

            ViewBag.IsEdit = false;
            var lstInvF_InventoryOutFG = new List<InvF_InventoryOutFG>();
            var lstInvF_InventoryOutFGDtl = new List<InvF_InventoryOutFGDtl>();

            if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
            {
                var objInvF_InventoryOutFGUI = new InvF_InventoryOutFGUI()
                {
                    IF_InvOutFGNo = ifinvoutfgno,
                };
                var strWhereClause_InvF_InventoryOutFG = strWhereClause_InvFInventoryOutFG(objInvF_InventoryOutFGUI);
                var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
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
                    Ft_WhereClause = strWhereClause_InvF_InventoryOutFG,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_InvF_InventoryOutFG = "*",
                    Rt_Cols_InvF_InventoryOutFGDtl = "*",
                    Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
                };
                var objRT_InvF_InventoryOutFG = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);

                var listDtl = new List<InvF_InventoryOutFGDtlUI>();
                if (objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl != null)
                {
                    var listDtlCur = new List<InvF_InventoryOutFGDtlUI>();
                    foreach (var it in objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl)
                    {
                        var obj = new InvF_InventoryOutFGDtlUI();
                        obj.IF_InvOutFGNo = it.IF_InvOutFGNo;
                        obj.PartCode = it.PartCode;

                        #region ["Danh sách sản phẩm"]
                        var objMst_Part = new Mst_Part()
                        {
                            PartCode = it.PartCode,
                        };
                        var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);

                        var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                        #endregion

                        obj.PartName = listPart[0].PartName;
                        obj.PartNameFS = listPart[0].PartNameFS;
                        obj.CreateDTimeSv = it.LogLUDTimeUTC;
                        obj.Qty = it.Qty;
                        obj.FormOutType = objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG[0].FormOutType;

                        listDtlCur.Add(obj);
                    }
                    listDtl.AddRange(listDtlCur);
                }

                if (objRT_InvF_InventoryOutFG != null)
                {
                    objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl.AddRange(lstInvF_InventoryOutFGDtl);
                    //objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG.AddRange(lstInvF_InventoryOutFG);

                    ViewBag.ListData = objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG;
                    ViewBag.ListDtl = listDtl;
                }
            }

            return View(new InvF_InventoryOutFGDtlUI());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string ifinvoutfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var title = "";
                if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
                {
                    var objInvF_InventoryOutFG = new InvF_InventoryOutFG()
                    {
                        IF_InvOutFGNo = ifinvoutfgno,
                    };
                    var strWhereInvF_InventoryOutFGGetDetal = strWhereInvF_InventoryOutFGGetDtl(objInvF_InventoryOutFG);
                    var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereInvF_InventoryOutFGGetDetal,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryOutFG
                        Rt_Cols_InvF_InventoryOutFG = "*",
                        Rt_Cols_InvF_InventoryOutFGDtl = "*",
                        InvF_InventoryOutFG = null,

                    };
                    var list = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                    if (list != null && list.Lst_InvF_InventoryOutFG.Count > 0)
                    {
                        objRQ_InvF_InventoryOutFG.Ft_WhereClause = null;
                        objRQ_InvF_InventoryOutFG.FlagIsDelete = "1";
                        objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFG = null;
                        objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGDtl = null;
                        objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG = list.Lst_InvF_InventoryOutFG[0];
                        objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl = list.Lst_InvF_InventoryOutFGDtl;

                        InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Save(objRQ_InvF_InventoryOutFG, addressAPIs);
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa phiếu xuất thành công");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Phiếu xuất kho không hợp lệ!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã phiếu xuất trống!");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        [HttpPost]
        public ActionResult ShowPopupSerial(string ifinvoutfgno, string partcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;

            ViewBag.IsEdit = false;
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.IsDetail = true;
            if (!CUtils.IsNullOrEmpty(ifinvoutfgno) && !CUtils.IsNullOrEmpty(partcode))
            {
                try
                {
                    #region [""]
                    var objInvF_InventoryOutFGInstSerial = new InvF_InventoryOutFGInstSerial()
                    {
                        IF_InvOutFGNo = ifinvoutfgno,
                        PartCode = partcode,
                    };
                    var strWhereInvF_InventoryOutFGGetDetal = strWhereInvF_InventoryOutFGGetDtlPart(objInvF_InventoryOutFGInstSerial);
                    var objRQ_InvF_InventoryOutFGInstSerial = new RQ_InvF_InventoryOutFGInstSerial()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereInvF_InventoryOutFGGetDetal,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryInFG
                        Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
                    };
                    var list = InvF_InventoryOutFGInstSerialService.Instance.WA_InvF_InventoryOutFGInstSerial_Get(objRQ_InvF_InventoryOutFGInstSerial, addressAPIs);

                    if (list != null)
                    {
                        ViewBag.ListInvF_InventoryInFG = list.Lst_InvF_InventoryOutFGInstSerial;
                    }
                    #endregion
                    return JsonView("ShowPopupSerial", list.Lst_InvF_InventoryOutFGInstSerial);
                }
                catch (Exception e)
                {
                    resultEntry.SetFailed().AddException(this, e);
                }
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupSerial", null, resultEntry);
        }
        #endregion

        #region ["Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {

                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                var exitsData = "";
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
                var list = new List<InvF_InventoryOutFGDtlUI>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {

                    if (table.Columns.Count != 2)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        if (!table.Columns.Contains("PartName"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("PartName", typeof (string))
                                               });
                        }
                        #region["Check null"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            object partname = "";
                            var partcode = dr["PartCode"].ToString().Trim();
                            dr["PartCode"] = partcode;

                            var objMst_Part = new Mst_Part()
                            {
                                PartCode = partcode,
                            };
                            var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
                            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                            if (listPart != null && listPart.Count > 0)
                            {
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryOutFGDtl.PartCode]))
                                {
                                    exitsData = "Mã sản phẩm không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryOutFGDtl.Qty]))
                                {
                                    exitsData = "Số lượng không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var qty = dr["Qty"].ToString().Trim();
                                    dr["Qty"] = qty;
                                    if (!CUtils.IsInteger(qty) || Convert.ToInt32(qty) <= 0)
                                    {
                                        exitsData = "Số lượng là số nguyên dương!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            else
                            {
                                exitsData = "Mã sản phẩm " + partcode + " không tồn tại!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            idx++;
                            //if (CUtils.IsNullOrEmpty(dr["PartCode"]))
                            //{
                            //    exitsData = "Mã sản phẩm không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var partname = "";
                            //    var partcode = dr["PartCode"].ToString().Trim();
                            //    var objMst_Part = new Mst_Part()
                            //    {
                            //        PartCode = partcode,
                            //    };
                            //    var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
                            //    var listMst_Part = List_Mst_Part(strWhereClause_Mst_Part);
                            //    if (listMst_Part != null)
                            //    {
                            //        partname = listMst_Part[0].PartName.ToString().Trim();
                            //    }
                            //    dr["PartName"] = partname;
                            //}
                            //if (CUtils.IsNullOrEmpty(dr["Qty"]))
                            //{
                            //    exitsData = "Số lượng không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //else
                            //{
                            //    var qty = dr["Qty"].ToString().Trim();
                            //    dr["Qty"] = qty;
                            //    if (!CUtils.IsInteger(qty) || Convert.ToInt32(qty) <= 0)
                            //    {
                            //        exitsData = "Số lượng là số nguyên dương!";
                            //        resultEntry.AddMessage(exitsData);
                            //        return Json(resultEntry);
                            //    }
                            //}
                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var partcode = table.Rows[i]["PartCode"].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _partcode = table.Rows[j]["PartCode"].ToString().Trim();
                                    if (partcode == _partcode)
                                    {
                                        exitsData = "Mã sản phẩm '" + partcode + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<InvF_InventoryOutFGDtlUI>(table);

                    return JsonView("ImportExcel", list);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ImportExcel", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string ifinvoutfgno = "", string partcode = "", string fromdate = "", string todate = "",
            string formouttype = "", string status = "", int page = 0, int recordcount = 10)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                var listInvFInventoryOutFGCur = new List<InvF_InventoryOutFG>();
                var listInvFInventoryOutFGDtlCur = new List<InvF_InventoryOutFGDtl>();
                string url = "";
                var itemCount = 0;
                var pageCount = 0;
                var offset = Offset;
                #region["Search"]
                var objInvF_InventoryOutFGUI = new InvF_InventoryOutFGUI()
                {
                    FromDate = fromdate,
                    ToDate = todate,
                    PartCode = partcode,
                    InvOutType = formouttype,
                    IF_InvOutFGStatus = status,
                    IF_InvOutFGNo = ifinvoutfgno,
                };
                var strWhereClause_InvF_InventoryOutFG = strWhereClause_InvFInventoryOutFG(objInvF_InventoryOutFGUI);
                var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause_InvF_InventoryOutFG,
                    // RQ_InvF_InventoryOutFG
                    Rt_Cols_InvF_InventoryOutFG = "*",
                    Rt_Cols_InvF_InventoryOutFGDtl = "*",
                    Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
                };
                var objRT_InvF_InventoryOutFG = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                #endregion

                #region ["Export excel"]
                if (objRT_InvF_InventoryOutFG != null && objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG != null)
                {
                    if (objRT_InvF_InventoryOutFG.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_InvF_InventoryOutFG.MySummaryTable.MyCount);
                    }
                    if (objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG != null && objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    foreach (var time in objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG)
                    {
                        var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(time.CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                        time.CreateDTimeUTC = createDTimeUTC;
                    }
                    listInvFInventoryOutFGCur.AddRange(objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG);
                    listInvFInventoryOutFGDtlCur.AddRange(objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl);

                    var listDtl = new List<InvF_InventoryOutFGDtlUI>();
                    if (objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl != null)
                    {
                        var listDtlCur = new List<InvF_InventoryOutFGDtlUI>();

                        foreach (var it in objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl)
                        {
                            var obj = new InvF_InventoryOutFGDtlUI();
                            obj.IF_InvOutFGNo = it.IF_InvOutFGNo;
                            obj.PartCode = it.PartCode;

                            var objMst_Part = new Mst_Part()
                            {
                                PartCode = it.PartCode,
                            };
                            var logLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                            var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
                            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                            obj.PartName = listPart[0].PartName;
                            obj.PartNameFS = listPart[0].PartNameFS;
                            obj.Qty = it.Qty;
                            var objInvF_InventoryOutFG = new InvF_InventoryOutFG()
                            {
                                IF_InvOutFGNo = obj.IF_InvOutFGNo,
                            };
                            var strWhereClause_InvF_InventoryOutFGDtl = strWhereInvF_InventoryOutFGGetDtl(objInvF_InventoryOutFG);
                            var lstinvF_InventoryOutFGDtl = List_InvF_InventoryOutFGDtl(strWhereClause_InvF_InventoryOutFGDtl);
                            var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(lstinvF_InventoryOutFGDtl[0].CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                            obj.CreateDTimeSv = createDTimeUTC;
                            obj.CreateBy = objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG[0].CreateBy;
                            obj.IF_InvOutFGNo = it.IF_InvOutFGNo;
                            listDtlCur.Add(obj);
                        }
                        listDtl.AddRange(listDtlCur);
                    }

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    Dictionary<string, string> dicColNamesDtl = GetImportDicColumsDtl();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryOutFG).Name), ref url);
                    var ds = new DataSet();
                    var table = ExcelExport.ConstructDataTable(listInvFInventoryOutFGCur, dicColNames);
                    var tableDtl = ExcelExport.ConstructDataTable(listDtl, dicColNamesDtl);

                    if (table != null && table.Rows.Count > 0)
                    {
                        table.TableName = "InvFInventoryOutFG";
                        ds.Tables.Add(table);
                    }
                    if (tableDtl != null && tableDtl.Rows.Count > 0)
                    {
                        tableDtl.TableName = "InvFInventoryOutFGDtl";
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
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<InvF_InventoryOutFGDtl>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryOutFGDtl).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvFInventoryOutFGDtl"));

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportDtl(string ifinvoutfgno = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            var listInvF_InventoryOutFGSearch = new List<InvF_InventoryOutFG>();
            var ListInv_FInventoryOutFGDtl = new List<InvF_InventoryOutFGDtl>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var offset = Offset;

            try
            {
                #region["Search"]
                if (!CUtils.IsNullOrEmpty(ifinvoutfgno))
                {
                    var objInvF_InventoryOutFGUIUI = new InvF_InventoryOutFGUI()
                    {
                        IF_InvOutFGNo = ifinvoutfgno,
                    };

                    var strWhereClauseInvF_InventoryOutFGUI = strWhereClause_InvFInventoryOutFG(objInvF_InventoryOutFGUIUI);
                    var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseInvF_InventoryOutFGUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryOutFG
                        Rt_Cols_InvF_InventoryOutFG = "*",
                        Rt_Cols_InvF_InventoryOutFGDtl = "*",
                        InvF_InventoryOutFG = null,
                    };
                    var listInv_FInventoryOutFGSearch = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
                    if (listInv_FInventoryOutFGSearch != null)
                    {
                        if (listInv_FInventoryOutFGSearch.MySummaryTable != null)
                        {
                            itemCount = Convert.ToInt32(listInv_FInventoryOutFGSearch.MySummaryTable.MyCount);
                        }
                        if (listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFG != null && listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFG.Count > 0)
                        {
                            pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        }
                        ListInv_FInventoryOutFGDtl.AddRange(listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFGDtl);
                        var listDtl = new List<InvF_InventoryOutFGDtlUI>();
                        if (listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFGDtl != null)
                        {
                            var listDtlCur = new List<InvF_InventoryOutFGDtlUI>();
                            foreach (var it in listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFGDtl)
                            {
                                var obj = new InvF_InventoryOutFGDtlUI();
                                obj.IF_InvOutFGNo = it.IF_InvOutFGNo;
                                obj.PartCode = it.PartCode;

                                var objMst_Part = new Mst_Part()
                                {
                                    PartCode = it.PartCode,
                                };
                                var logLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
                                var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                                obj.PartName = listPart[0].PartName;
                                obj.PartNameFS = listPart[0].PartNameFS;
                                var objInvF_InventoryOutFG = new InvF_InventoryOutFG()
                                {
                                    IF_InvOutFGNo = obj.IF_InvOutFGNo,
                                };
                                var strWhereClause_InvF_InventoryOutFGDtl = strWhereInvF_InventoryOutFGGetDtl(objInvF_InventoryOutFG);
                                var lstinvF_InventoryOutFGDtl = List_InvF_InventoryOutFGDtl(strWhereClause_InvF_InventoryOutFGDtl);
                                var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(lstinvF_InventoryOutFGDtl[0].CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                obj.CreateDTimeSv = createDTimeUTC;
                                obj.Qty = it.Qty;
                                obj.CreateBy = listInv_FInventoryOutFGSearch.Lst_InvF_InventoryOutFG[0].CreateBy;

                                listDtlCur.Add(obj);
                            }
                            listDtl.AddRange(listDtlCur);
                        }
                        Dictionary<string, string> dicColNamesDtl = GetImportDicColumsDtl();
                        string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryOutFGDtl).Name), ref url);
                        var ds = new DataSet();
                        ExcelExport.ExportToExcelFromList(listDtl, dicColNamesDtl, filePath, string.Format("InvF_InventoryOutFGDtl"));

                        return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                    }
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
                 {"IF_InvOutFGNo","Số phiếu xuất"},
                 {"InvFOutType", "Loại phiếu xuất" },
                 {"CreateBy","Người nhập phiếu"},
                 {"CreateDTimeUTC","Ngày tạo phiếu"},
                 {"IF_InvOutFGStatus","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                 {"IF_InvOutFGNo","Số phiếu xuất" },
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"Qty","Số lượng"},
                 {"CreateBy","Người nhập phiếu"},
                 {"CreateDTimeSv","Ngày tạo phiếu"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PartCode","Mã sản phẩm"},
                 {"Qty","Số lượng"},
            };
        }
        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_InvFInventoryOutFG(InvF_InventoryOutFGUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryOutFG = TableName.InvF_InventoryOutFG + ".";
            var Tbl_InvF_InventoryOutFGDtl = TableName.InvF_InventoryOutFGDtl + ".";

            if (!CUtils.IsNullOrEmpty(data.IF_InvOutFGNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.IF_InvOutFGNo, "%" + CUtils.StrValueOrNull(data.IF_InvOutFGNo) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFGDtl + TblInvF_InventoryOutFGDtl.PartCode, "%" + CUtils.StrValueOrNull(data.PartCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FormOutType))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.FormOutType, data.FormOutType, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvFOutType))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.InvFOutType, data.InvFOutType, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                data.CreateDTimeUTC = data.FromDate.ToString().Trim() + " 00:00:00";
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.CreateDTimeUTC, CUtils.StrValueOrNull(data.CreateDTimeUTC), ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                data.CreateDTimeUTC = data.ToDate.ToString().Trim() + " 23:59:59";
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.CreateDTimeUTC, CUtils.StrValueOrNull(data.CreateDTimeUTC), "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.ApprFromDate))
            {
                var CreateDTimeUTCFrom = data.ApprFromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.ApprDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ApprToDate))
            {
                var CreateDTimeUTCTo = data.ApprToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.ApprDTimeUTC, strToDate, "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.IF_InvOutFGStatus))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.IF_InvOutFGStatus, CUtils.StrValueOrNull(data.IF_InvOutFGStatus), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_MstPart(Mst_Part data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Part = TableName.Mst_Part + ".";
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Inventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvCode, CUtils.StrValueOrNull(data.InvCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereInvF_InventoryOutFGGetDtl(InvF_InventoryOutFG data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryOutFG = TableName.InvF_InventoryOutFG + ".";
            if (!CUtils.IsNullOrEmpty(data.IF_InvOutFGNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryOutFG + TblInvF_InventoryOutFG.IF_InvOutFGNo, CUtils.StrValueOrNull(data.IF_InvOutFGNo), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereInvF_InventoryOutFGGetDtlPart(InvF_InventoryOutFGInstSerial data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var InvF_InventoryOutFGInstSerial = TableName.InvF_InventoryOutFGInstSerial + ".";
            if (!CUtils.IsNullOrEmpty(data.IF_InvOutFGNo))
            {
                sbSql.AddWhereClause(InvF_InventoryOutFGInstSerial + TblInvF_InventoryOutFGInstSerial.IF_InvOutFGNo, CUtils.StrValueOrNull(data.IF_InvOutFGNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.IF_InvOutFGNo))
            {
                sbSql.AddWhereClause(InvF_InventoryOutFGInstSerial + TblInvF_InventoryOutFGInstSerial.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private List<InvF_InventoryOutFG> List_InvF_InventoryOutFGDtl(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listInvF_InventoryOutFG = new List<InvF_InventoryOutFG>();
            var objRQ_InvF_InventoryOutFG = new RQ_InvF_InventoryOutFG()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_InvF_InventoryOutFG
                Rt_Cols_InvF_InventoryOutFG = "*",
                Rt_Cols_InvF_InventoryOutFGDtl = "*",
                InvF_InventoryOutFG = null,
            };
            var objRT_InvF_InventoryOutFG = InvF_InventoryOutFGService.Instance.WA_InvF_InventoryOutFG_Get(objRQ_InvF_InventoryOutFG, addressAPIs);
            if (objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl != null && objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl.Count > 0)
            {
                listInvF_InventoryOutFG.AddRange(objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG);
            }
            return listInvF_InventoryOutFG;
        }
        #endregion
    }
}