using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
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
    public class InvF_InventoryInFGController : AdminBaseController
    {
        // GET: InvF_InventoryInFG
        public ActionResult Index(string ifinvinfgno = "", string partcode = "", string fromdate = "", string todate = "", string apprfromdate = "", string apprtodate = "", string tt = "", string formintype = "", string init = "init", int page = 0, int recordcount = 10)
        {
            ViewBag.PageCur = page.ToString();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var listPart = List_Mst_Part(null);
            ViewBag.ListPart = listPart;
            ViewBag.Offset = Offset;
            var pageInfo = new PageInfo<InvF_InventoryInFG>(0, PageSizeConfig)
            {
                DataList = new List<InvF_InventoryInFG>()
            };
            var itemCount = 0;
            var pageCount = 0;
            // (không có nút tìm kiếm => load data luôn)
            //init = String.Format("{0}", "search");
            if (init != "init")
            {
                #region["Search"]
                var objInvF_InventoryInFGUIUI = new InvF_InventoryInFGUI()
                {
                    IF_InvInFGNo = ifinvinfgno,
                    PartCode = partcode,
                    FromDate = fromdate,
                    ToDate = todate,
                    IF_InvInFGStatus = tt,
                    FormInType = formintype,
                    ApprFromDate = apprfromdate,
                    ApprToDate = apprtodate,
                };

                var strWhereClauseInvF_InventoryInFGUI = strWhereClause_InvF_InventoryInFGUI(objInvF_InventoryInFGUIUI);
                var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClauseInvF_InventoryInFGUI,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_InvF_InventoryInFG = "*",
                    Rt_Cols_InvF_InventoryInFGDtl = "*",
                    InvF_InventoryInFG = null,
                };
                var objRT_InvF_InventoryInFGCur = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);

                if (objRT_InvF_InventoryInFGCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_InvF_InventoryInFGCur.MySummaryTable.MyCount);
                }
                if (objRT_InvF_InventoryInFGCur != null && objRT_InvF_InventoryInFGCur.Lst_InvF_InventoryInFG != null && objRT_InvF_InventoryInFGCur.Lst_InvF_InventoryInFG.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_InvF_InventoryInFGCur.Lst_InvF_InventoryInFG);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                    //pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
                apprfromdate = DateToSearch();
            }
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.ApprFromDate = apprfromdate;
            ViewBag.ApprToDate = apprtodate;
            ViewBag.IF_InvInFGNo = ifinvinfgno;
            ViewBag.IF_InvInFGStatus = tt;
            #region ["PageInfo"]
            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            #endregion
            return View(pageInfo);
        }

        #region["Tạo phiếu nhập kho thành phẩm"]
        [HttpPost]
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

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objSeq = new Seq_Common { SequenceType = SeqType.IFInvInFGNo, Param_Postfix = "", Param_Prefix = "" };
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
            var ifInvInFGNo = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPI);
            ViewBag.Today = Today;
            ViewBag.IF_InvInFGNo = ifInvInFGNo.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            ViewBag.MST = userState.MST;
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
            ViewBag.Offset = Offset;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                var mst = userState.MST;
                var exitsData = "";
                var objInvF_InventoryInFG = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInFG>(model);
                // var objInvF_InventoryInFGDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInFGDtl>(model);
                var objRQ_InvF_InventoryInFG = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryInFG>(model);
                foreach (var qty in objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl)
                {
                    if (Convert.ToInt32(qty.Qty) <= 0)
                    {
                        resultEntry.Success = true;
                        exitsData = "Số lượng của " + qty.PartCode + " phải là số nguyên dương!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }

                }
                if (objRQ_InvF_InventoryInFG != null)
                {
                    objRQ_InvF_InventoryInFG.Tid = GetNextTId();
                    objRQ_InvF_InventoryInFG.GwUserCode = GwUserCode;
                    objRQ_InvF_InventoryInFG.GwPassword = GwPassword;
                    objRQ_InvF_InventoryInFG.FuncType = null;
                    objRQ_InvF_InventoryInFG.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_InvF_InventoryInFG.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_InvF_InventoryInFG.Ft_Cols_Upd = null;
                    objRQ_InvF_InventoryInFG.WAUserCode = waUserCode;
                    objRQ_InvF_InventoryInFG.WAUserPassword = waUserPassword;
                    objRQ_InvF_InventoryInFG.UtcOffset = userState.UtcOffset;
                    // RQ_InvF_InventoryInFG
                    objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG = null;
                    objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl = null;
                    objRQ_InvF_InventoryInFG.InvF_InventoryInFG = objInvF_InventoryInFG;

                    InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Save(objRQ_InvF_InventoryInFG, addressAPI);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Tạo phiếu nhập kho thành phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    //return Json(new { Success = true, Title = "Tạo phiếu nhập kho thành phẩm thành công!" });
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Phiếu nhập kho thành phẩm không hợp lệ!");
                    //return Json(new { Success = false, Title = "Phiếu nhập kho thành phẩm không hợp lệ!" });
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

        #region["Sửa phiếu nhập kho thành phẩm"]
        [HttpGet]
        public ActionResult Update(string ifinvinfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var mst = userState.MST;
            ViewBag.MST = userState.MST;
            ViewBag.Offset = Offset;
            var LstInvF_InventoryInFG = new List<InvF_InventoryInFG>();
            var LstInvF_InventoryInFGDtl = new List<InvF_InventoryInFGDtl>();



            var objInvF_InventoryInFGUI = new InvF_InventoryInFGUI()
            {
                IF_InvInFGNo = ifinvinfgno,
            };
            var strWhereInvF_InventoryInFGGetDetal = strWhereInvF_InventoryInFGGetDtl(objInvF_InventoryInFGUI);
            var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereInvF_InventoryInFGGetDetal,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_InvF_InventoryInFG
                Rt_Cols_InvF_InventoryInFG = "*",
                Rt_Cols_InvF_InventoryInFGDtl = "*",
                InvF_InventoryInFG = null,
            };
            var list = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
            var listDtl = new List<InvF_InventoryInFGDtlUI>();
            if (list.Lst_InvF_InventoryInFGDtl != null)
            {
                var listDtlCur = new List<InvF_InventoryInFGDtlUI>();
                foreach (var it in list.Lst_InvF_InventoryInFGDtl)
                {
                    var obj = new InvF_InventoryInFGDtlUI();
                    obj.IF_InvInFGNo = it.IF_InvInFGNo;
                    obj.PartCode = it.PartCode;

                    var objMst_Part = new Mst_Part()
                    {
                        PartCode = it.PartCode,
                    };
                    var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                    var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                    obj.PartName = listPart[0].PartName;
                    obj.PartNameFS = listPart[0].PartNameFS;
                    obj.CreateDTimeSv = it.LogLUDTimeUTC;
                    obj.ProductionDate = it.ProductionDate;
                    obj.Qty = it.Qty;
                    listDtlCur.Add(obj);
                }
                listDtl.AddRange(listDtlCur);
            }

            if (list != null)
            {

                ViewBag.List = list.Lst_InvF_InventoryInFG;
                ViewBag.ListDtl = listDtl;

            }
            var objMst_Inventory = new Mst_Inventory()
            {
                InvCode = list.Lst_InvF_InventoryInFG[0].InvCode,
            };
            var strWhereMst_Inventory = strMst_Inventory(objMst_Inventory);
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
            return View(new InvF_InventoryInFGDtlUI());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPI = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var exitsData = "";
                var objInvF_InventoryInFG = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInFG>(model);
                // var objInvF_InventoryInFGDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_InventoryInFGDtl>(model);
                var objRQ_InvF_InventoryInFG = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_InvF_InventoryInFG>(model);
                foreach (var qty in objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl)
                {
                    if (Convert.ToInt32(qty.Qty) <= 0)
                    {
                        resultEntry.Success = true;
                        exitsData = "Số lượng của " + qty.PartCode + " phải là số nguyên dương!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }

                }
                if (objRQ_InvF_InventoryInFG != null)
                {
                    objRQ_InvF_InventoryInFG.Tid = GetNextTId();
                    objRQ_InvF_InventoryInFG.GwUserCode = GwUserCode;
                    objRQ_InvF_InventoryInFG.GwPassword = GwPassword;
                    objRQ_InvF_InventoryInFG.FuncType = null;
                    objRQ_InvF_InventoryInFG.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_InvF_InventoryInFG.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_InvF_InventoryInFG.Ft_Cols_Upd = null;
                    objRQ_InvF_InventoryInFG.WAUserCode = waUserCode;
                    objRQ_InvF_InventoryInFG.WAUserPassword = waUserPassword;
                    objRQ_InvF_InventoryInFG.UtcOffset = userState.UtcOffset;
                    // RQ_InvF_InventoryInFG
                    objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG = null;
                    objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl = null;
                    objRQ_InvF_InventoryInFG.InvF_InventoryInFG = objInvF_InventoryInFG;

                    InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Save(objRQ_InvF_InventoryInFG, addressAPI);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa phiếu nhập kho thành phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    //return Json(new { Success = true, Title = "Sửa phiếu nhập kho thành phẩm thành công!" });
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Phiếu nhập kho thành phẩm không hợp lệ!");
                    //return Json(new { Success = false, Title = "Phiếu nhập kho thành phẩm không hợp lệ!" });
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

        #region["Chi tiết - Duyệt - Xóa"]
        [HttpGet]
        public ActionResult Detail(string ifinvinfgno)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;
            var LstInvF_InventoryInFG = new List<InvF_InventoryInFG>();
            var LstInvF_InventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
            var objInvF_InventoryInFGUI = new InvF_InventoryInFGUI()
            {
                IF_InvInFGNo = ifinvinfgno,
            };
            var strWhereInvF_InventoryInFGGetDetal = strWhereInvF_InventoryInFGGetDtl(objInvF_InventoryInFGUI);
            var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereInvF_InventoryInFGGetDetal,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_InvF_InventoryInFG
                Rt_Cols_InvF_InventoryInFG = "*",
                Rt_Cols_InvF_InventoryInFGDtl = "*",
                InvF_InventoryInFG = null,
            };
            var list = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
            var listDtl = new List<InvF_InventoryInFGDtlUI>();
            if (list.Lst_InvF_InventoryInFGDtl != null)
            {
                var listDtlCur = new List<InvF_InventoryInFGDtlUI>();
                foreach (var it in list.Lst_InvF_InventoryInFGDtl)
                {
                    var obj = new InvF_InventoryInFGDtlUI();
                    obj.IF_InvInFGNo = it.IF_InvInFGNo;
                    obj.PartCode = it.PartCode;

                    var objMst_Part = new Mst_Part()
                    {
                        PartCode = it.PartCode,
                    };
                    var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                    var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                    obj.PartName = listPart[0].PartName;
                    obj.PartNameFS = listPart[0].PartNameFS;
                    obj.CreateDTimeSv = it.LogLUDTimeUTC;
                    obj.ProductionDate = it.ProductionDate;
                    obj.Qty = it.Qty;
                    obj.FormInType = list.Lst_InvF_InventoryInFG[0].FormInType;
                    listDtlCur.Add(obj);
                }
                listDtl.AddRange(listDtlCur);
            }

            if (list != null)
            {
                list.Lst_InvF_InventoryInFGDtl.AddRange(LstInvF_InventoryInFGDtl);
                list.Lst_InvF_InventoryInFG.AddRange(LstInvF_InventoryInFG);

                ViewBag.List = list.Lst_InvF_InventoryInFG;
                ViewBag.ListDtl = listDtl;
            }

            return View(new InvF_InventoryInFGDtlUI());
        }

        [HttpPost]
        public ActionResult ShowPopupSerial(string ifinvinfgno, string partcode)
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
            if (!CUtils.IsNullOrEmpty(ifinvinfgno) && !CUtils.IsNullOrEmpty(partcode))
            {
                try
                {
                    #region [""]
                    var objInvF_InventoryInFGInstSerial = new InvF_InventoryInFGInstSerial()
                    {
                        IF_InvInFGNo = ifinvinfgno,
                        PartCode = partcode,
                    };
                    var strWhereInvF_InventoryOutFGGetDetal = strWhereInvF_InventoryInFGGetDtlPart(objInvF_InventoryInFGInstSerial);
                    var objRQ_InvF_InventoryInFGInstSerial = new RQ_InvF_InventoryInFGInstSerial()
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
                        Rt_Cols_InvF_InventoryInFGInstSerial = "*",
                    };
                    var list = InvF_InventoryInFGInstSerialService.Instance.WA_InvF_InventoryInFGInstSerial_Get(objRQ_InvF_InventoryInFGInstSerial, addressAPIs);

                    if (list != null)
                    {
                        ViewBag.ListInvF_InventoryInFG = list.Lst_InvF_InventoryInFGInstSerial;
                    }
                    #endregion
                    return JsonView("ShowPopupSerial", list.Lst_InvF_InventoryInFGInstSerial);
                }
                catch (Exception e)
                {
                    resultEntry.SetFailed().AddException(this, e);
                }
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupSerial", null, resultEntry);
        }

        //[HttpGet]
        //public ActionResult Detail(string ifinvinfgno, string partcode)
        //{
        //    var userState = this.UserState;
        //    Session["UserState"] = userState;
        //    var addressAPIs = UserState.AddressAPIs;
        //    var waUserCode = userState.SysUser.UserCode;
        //    var waUserPassword = userState.SysUser.UserPassword;
        //    ViewBag.Offset = Offset;
        //    var LstInvF_InventoryInFG = new List<InvF_InventoryInFG>();
        //    var LstInvF_InventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
        //    var objInvF_InventoryInFGUI = new InvF_InventoryInFGUI()
        //    {
        //        IF_InvInFGNo = ifinvinfgno,
        //        PartCode = partcode,
        //    };
        //    var strWhereInvF_InventoryInFGGetDetal = strWhereInvF_InventoryInFGGetDtlPart(objInvF_InventoryInFGUI);
        //    var objRQ_InvF_InventoryOutFGInstSerial = new RQ_InvF_InventoryOutFGInstSerial()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStartExportExcel,
        //        Ft_RecordCount = RowsWorksheets.ToString(),
        //        Ft_WhereClause = strWhereInvF_InventoryInFGGetDetal,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = userState.UtcOffset,
        //        // RQ_InvF_InventoryInFG
        //        Rt_Cols_InvF_InventoryOutFGInstSerial = "*",
        //    };
        //    var list = InvF_InventoryInFGService.Instance.WA_InvF_InventoryOutFGInstSerial_Get(objRQ_InvF_InventoryOutFGInstSerial, addressAPIs);
        //    if (list.Lst_InvF_InventoryOutFGInstSerial != null)
        //    {
        //        foreach (var it in list.Lst_InvF_InventoryOutFGInstSerial)
        //        {
        //            var obj = new InvF_InventoryOutFGInstSerial();
        //            obj.IF_InvInFGNo = it.IF_InvInFGNo;
        //            obj.PartCode = it.PartCode;

        //            var objMst_Part = new Mst_Part()
        //            {
        //                PartCode = it.PartCode,
        //            };
        //            var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
        //            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
        //            obj.PartName = listPart[0].PartName;
        //            obj.PartNameFS = listPart[0].PartNameFS;
        //            obj.CreateDTimeSv = it.LogLUDTimeUTC;
        //            obj.ProductionDate = it.ProductionDate;
        //            obj.Qty = it.Qty;
        //            listDtlCur.Add(obj);
        //        }
        //        listDtl.AddRange(listDtlCur);
        //    }

        //    if (list != null)
        //    {
        //        list.Lst_InvF_InventoryInFGDtl.AddRange(LstInvF_InventoryInFGDtl);
        //        list.Lst_InvF_InventoryInFG.AddRange(LstInvF_InventoryInFG);

        //        ViewBag.List = list.Lst_InvF_InventoryInFG;
        //        ViewBag.ListDtl = listDtl;
        //    }

        //    return View(new InvF_InventoryInFGDtlUI());
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(string ifinvinfgno)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                var title = "";
                if (!CUtils.IsNullOrEmpty(ifinvinfgno))
                {
                    var InvF_InventoryInFGNO = new InvF_InventoryInFG()
                    {
                        IF_InvInFGNo = ifinvinfgno,
                    };
                    var strWhereInvF_InventoryInFGGetDetal = strWhereInvF_InventoryInFGGetDtl(InvF_InventoryInFGNO);
                    var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereInvF_InventoryInFGGetDetal,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryInFG
                        Rt_Cols_InvF_InventoryInFG = "*",
                        Rt_Cols_InvF_InventoryInFGDtl = "*",
                        InvF_InventoryInFG = InvF_InventoryInFGNO,

                    };
                    var list = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
                    if (list != null)
                    {
                        InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Approve(objRQ_InvF_InventoryInFG, addressAPIs);
                        title = "Duyệt phiếu nhập '" + ifinvinfgno + "' thành công!";
                    }
                    else
                    {
                        title = "Mã phiếu nhập '" + ifinvinfgno + "' không có trong hệ thống!";
                    }
                }
                else
                {
                    title = "Mã phiếu nhập trống!";
                }

                return Json(new { Success = true, Title = title });

            }

            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string ifinvinfgno)
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
                if (!CUtils.IsNullOrEmpty(ifinvinfgno))
                {
                    var objInvF_InventoryInFG = new InvF_InventoryInFG()
                    {
                        IF_InvInFGNo = ifinvinfgno,
                    };
                    var strWhereInvF_InventoryInFGGetDetal = strWhereInvF_InventoryInFGGetDtl(objInvF_InventoryInFG);
                    var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereInvF_InventoryInFGGetDetal,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryInFG
                        Rt_Cols_InvF_InventoryInFG = "*",
                        Rt_Cols_InvF_InventoryInFGDtl = "*",
                        InvF_InventoryInFG = null,

                    };
                    var list = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
                    if (list != null && list.Lst_InvF_InventoryInFG.Count > 0)
                    {
                        objRQ_InvF_InventoryInFG.Ft_WhereClause = null;
                        objRQ_InvF_InventoryInFG.FlagIsDelete = "1";
                        objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG = null;
                        objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl = null;
                        objRQ_InvF_InventoryInFG.InvF_InventoryInFG = objInvF_InventoryInFG;
                        objRQ_InvF_InventoryInFG.InvF_InventoryInFG.MST = UserState.MST;

                        InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Save(objRQ_InvF_InventoryInFG, addressAPIs);
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa phiếu nhập thành công");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Phiếu nhập kho thành phẩm không hợp lệ!");
                        //return Json(new { Success = false, Title = "Phiếu nhập kho thành phẩm không hợp lệ!" });
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã phiếu nhập trống!");
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

        #region["Excel"]
        #region["Import"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

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
                var listInvF_InventoryInFG = new List<InvF_InventoryInFGDtlUI>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 3)
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
                            var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                            if (listPart != null && listPart.Count > 0)
                            {
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.PartCode]))
                                {
                                    exitsData = "Mã sản phẩm không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.Qty]))
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
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.ProductionDate]))
                                {
                                    exitsData = "Ngày sản xuất không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            else
                            {
                                exitsData = "Mã sản phẩm " + partcode + " không tồn tại!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            idx++;
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

                        listInvF_InventoryInFG = DataTableCmUtils.ToListof<InvF_InventoryInFGDtlUI>(table);


                        return JsonView("ImportExcel", listInvF_InventoryInFG);
                    }

                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }

            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ImportExcel", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportDtl(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

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
                var listInvF_InventoryInFG = new List<InvF_InventoryInFGDtl>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 3)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
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
                            var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                            if (listPart != null)
                            {
                                partname = listPart[0].PartName;

                                dr["PartName"] = partname;
                                var flagactive = "0";

                                if (listPart[0].FlagActive.ToString() == flagactive)
                                {
                                    exitsData = "Mã sản phẩm" + listPart[0].PartCode + "không còn tồn tại!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.PartCode]))
                                {
                                    exitsData = "Mã sản phẩm không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.Qty]))
                                {
                                    exitsData = "Số lượng không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblInvF_InventoryInFGDtl.ProductionDate]))
                                {
                                    exitsData = "Ngày sản xuất không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            else
                            {
                                exitsData = "Mã sản phẩm " + partcode + " không tồn tại!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            idx++;
                        }
                        #endregion

                    }
                    listInvF_InventoryInFG = DataTableCmUtils.ToListof<InvF_InventoryInFGDtl>(table);


                    return JsonView("ImportExcel", listInvF_InventoryInFG);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }

            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }

            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ImportExcel", null, resultEntry);
        }
        #endregion
        #region["Export"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<InvF_InventoryInFG>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInFG).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("InvF_InventoryInFG"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
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
        public ActionResult ExportDtl(string ifinvinfgno = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listInvF_InventoryInFGSearch = new List<InvF_InventoryInFG>();
            var ListInv_FInventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var offset = Offset;

            try
            {
                #region["Search"]
                if (!CUtils.IsNullOrEmpty(ifinvinfgno))
                {
                    var objInvF_InventoryInFGUIUI = new InvF_InventoryInFGUI()
                    {
                        IF_InvInFGNo = ifinvinfgno,
                    };

                    var strWhereClauseInvF_InventoryInFGUI = strWhereClause_InvF_InventoryInFGUI(objInvF_InventoryInFGUIUI);
                    var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseInvF_InventoryInFGUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_InvF_InventoryInFG
                        Rt_Cols_InvF_InventoryInFG = "*",
                        Rt_Cols_InvF_InventoryInFGDtl = "*",
                        InvF_InventoryInFG = null,
                    };
                    var listInv_FInventoryInFGSearch = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
                    if (listInv_FInventoryInFGSearch != null)
                    {
                        if (listInv_FInventoryInFGSearch.MySummaryTable != null)
                        {
                            itemCount = Convert.ToInt32(listInv_FInventoryInFGSearch.MySummaryTable.MyCount);
                        }
                        if (listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG != null && listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG.Count > 0)
                        {
                            pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        }
                        ListInv_FInventoryInFGDtl.AddRange(listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl);
                        var listDtl = new List<InvF_InventoryInFGDtlUI>();
                        if (listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl != null)
                        {
                            var listDtlCur = new List<InvF_InventoryInFGDtlUI>();
                            foreach (var it in listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl)
                            {
                                var obj = new InvF_InventoryInFGDtlUI();
                                obj.IF_InvInFGNo = it.IF_InvInFGNo;
                                obj.PartCode = it.PartCode;

                                var objMst_Part = new Mst_Part()
                                {
                                    PartCode = it.PartCode,
                                };
                                var logLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                                var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                                obj.PartName = listPart[0].PartName;
                                obj.PartNameFS = listPart[0].PartNameFS;
                                var objInvF_InventoryInFG = new InvF_InventoryInFG()
                                {
                                    IF_InvInFGNo = obj.IF_InvInFGNo,
                                };
                                var strWhereClause_InvF_InventoryInFGDtl = strWhereInvF_InventoryInFGGetDtl(objInvF_InventoryInFG);
                                var lstinvF_InventoryInFGDtl = List_InvF_InventoryInFGDtl(strWhereClause_InvF_InventoryInFGDtl);
                                var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(lstinvF_InventoryInFGDtl[0].CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                                obj.CreateDTimeSv = createDTimeUTC;
                                obj.ProductionDate = it.ProductionDate;
                                obj.Qty = it.Qty;
                                obj.CreateBy = listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG[0].CreateBy;
                                listDtlCur.Add(obj);
                            }
                            listDtl.AddRange(listDtlCur);
                        }
                        Dictionary<string, string> dicColNamesDtl = GetImportDicColumsDtl();
                        string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInFG).Name), ref url);
                        var ds = new DataSet();
                        ExcelExport.ExportToExcelFromList(listDtl, dicColNamesDtl, filePath, string.Format("InvF_InventoryInFGDtl"));

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string ifinvinfgno = "", string partcode = "", string fromdate = "", string todate = "", string tt = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var offset = Offset;
            var ListInv_FInventoryInFG = new List<InvF_InventoryInFG>();
            var ListInv_FInventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
            try
            {
                #region["Search"]
                var objInvF_InventoryInFGUIUI = new InvF_InventoryInFGUI()
                {
                    IF_InvInFGNo = ifinvinfgno,
                    PartCode = partcode,
                    FromDate = fromdate,
                    ToDate = todate,
                    IF_InvInFGStatus = tt,
                };

                var strWhereClauseInvF_InventoryInFGUI = strWhereClause_InvF_InventoryInFGUI(objInvF_InventoryInFGUIUI);
                var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseInvF_InventoryInFGUI,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_InvF_InventoryInFG
                    Rt_Cols_InvF_InventoryInFG = "*",
                    Rt_Cols_InvF_InventoryInFGDtl = "*",
                    InvF_InventoryInFG = null,
                };
                var listInv_FInventoryInFGSearch = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);

                if (listInv_FInventoryInFGSearch != null && listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG != null)
                {
                    if (listInv_FInventoryInFGSearch.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(listInv_FInventoryInFGSearch.MySummaryTable.MyCount);
                    }
                    if (listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG != null && listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    foreach (var time in listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG)
                    {
                        var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(time.CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                        time.CreateDTimeUTC = createDTimeUTC;
                    }

                    ListInv_FInventoryInFG.AddRange(listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG);
                    ListInv_FInventoryInFGDtl.AddRange(listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl);

                    var listDtl = new List<InvF_InventoryInFGDtlUI>();
                    if (listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl != null)
                    {
                        var listDtlCur = new List<InvF_InventoryInFGDtlUI>();

                        foreach (var it in listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFGDtl)
                        {
                            var obj = new InvF_InventoryInFGDtlUI();
                            obj.IF_InvInFGNo = it.IF_InvInFGNo;
                            obj.PartCode = it.PartCode;

                            var objMst_Part = new Mst_Part()
                            {
                                PartCode = it.PartCode,
                            };
                            var logLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(it.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                            var strWhereClause_Mst_Part = strMst_Part(objMst_Part);
                            var listPart = List_Mst_Part(strWhereClause_Mst_Part);
                            obj.PartName = listPart[0].PartName;
                            obj.PartNameFS = listPart[0].PartNameFS;
                            obj.ProductionDate = it.ProductionDate;
                            obj.Qty = it.Qty;
                            var objInvF_InventoryInFG = new InvF_InventoryInFG()
                            {
                                IF_InvInFGNo = obj.IF_InvInFGNo,
                            };
                            var strWhereClause_InvF_InventoryInFGDtl = strWhereInvF_InventoryInFGGetDtl(objInvF_InventoryInFG);
                            var lstinvF_InventoryInFGDtl = List_InvF_InventoryInFGDtl(strWhereClause_InvF_InventoryInFGDtl);
                            var createDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(lstinvF_InventoryInFGDtl[0].CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, offset);
                            obj.CreateDTimeSv = createDTimeUTC;
                            obj.CreateBy = listInv_FInventoryInFGSearch.Lst_InvF_InventoryInFG[0].CreateBy;
                            listDtlCur.Add(obj);
                        }
                        listDtl.AddRange(listDtlCur);
                    }

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    Dictionary<string, string> dicColNamesDtl = GetImportDicColumsDtl();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(InvF_InventoryInFG).Name), ref url);
                    var ds = new DataSet();
                    var table = ExcelExport.ConstructDataTable(ListInv_FInventoryInFG, dicColNames);
                    var tableDtl = ExcelExport.ConstructDataTable(listDtl, dicColNamesDtl);
                    //ExcelExport.ExportToExcelFromList(ListInv_FInventoryInFGSearch, dicColNames, filePath, string.Format("InvF_InventoryInFG"));
                    if (table != null && table.Rows.Count > 0)
                    {
                        table.TableName = "InvFInventoryInFG";
                        ds.Tables.Add(table);
                    }
                    if (tableDtl != null && tableDtl.Rows.Count > 0)
                    {
                        tableDtl.TableName = "InvFInventoryInFGDtl";
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
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_InvF_InventoryInFGUI(InvF_InventoryInFGUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryInFG = TableName.InvF_InventoryInFG + ".";
            var Tbl_InvF_InventoryInFGDtl = TableName.InvF_InventoryInFGDtl + ".";

            if (!CUtils.IsNullOrEmpty(data.IF_InvInFGNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.IF_InvInFGNo, "%" + CUtils.StrValueOrNull(data.IF_InvInFGNo) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFGDtl + TblInvF_InventoryInFGDtl.PartCode, "%" + CUtils.StrValueOrNull(data.PartCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                var CreateDTimeUTCFrom = data.FromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.CreateDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                var CreateDTimeUTCTo = data.ToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.CreateDTimeUTC, strToDate, "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.ApprFromDate))
            {
                var CreateDTimeUTCFrom = data.ApprFromDate.ToString().Trim() + " 00:00:00";
                var strFromDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCFrom).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.ApprDTimeUTC, strFromDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ApprToDate))
            {
                var CreateDTimeUTCTo = data.ApprToDate.ToString().Trim() + " 23:59:59";
                var strToDate = CUtils.ConvertingLocalTimeToUTC(CreateDTimeUTCTo).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.ApprDTimeUTC, strToDate, "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.IF_InvInFGStatus))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.IF_InvInFGStatus, CUtils.StrValueOrNull(data.IF_InvInFGStatus), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FormInType))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.FormInType, CUtils.StrValueOrNull(data.FormInType), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereInvF_InventoryInFGGetDtl(InvF_InventoryInFG data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryInFG = TableName.InvF_InventoryInFG + ".";
            if (!CUtils.IsNullOrEmpty(data.IF_InvInFGNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.IF_InvInFGNo, CUtils.StrValueOrNull(data.IF_InvInFGNo), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereInvF_InventoryInFGGetDtlPart(InvF_InventoryInFGUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_InvF_InventoryInFG = TableName.InvF_InventoryInFG + ".";
            var Tbl_Mst_Part = TableName.Mst_Part + ".";
            if (!CUtils.IsNullOrEmpty(data.IF_InvInFGNo))
            {
                sbSql.AddWhereClause(Tbl_InvF_InventoryInFG + TblInvF_InventoryInFG.IF_InvInFGNo, CUtils.StrValueOrNull(data.IF_InvInFGNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereInvF_InventoryInFGGetDtlPart(InvF_InventoryInFGInstSerial data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var InvF_InventoryInFGInstSerial = TableName.InvF_InventoryInFGInstSerial + ".";
            if (!CUtils.IsNullOrEmpty(data.IF_InvInFGNo))
            {
                sbSql.AddWhereClause(InvF_InventoryInFGInstSerial + TblInvF_InventoryInFGInstSerial.IF_InvInFGNo, CUtils.StrValueOrNull(data.IF_InvInFGNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(InvF_InventoryInFGInstSerial + TblInvF_InventoryInFGInstSerial.PartCode, CUtils.StrValueOrNull(data.PartCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strMst_Inventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvCode, CUtils.StrValueOrNull(data.InvCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strMst_Part(Mst_Part data)
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
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PartCode","Mã sản phẩm"},
                 {"Qty","Số lượng"},
                 {"ProductionDate","Ngày sản xuất"},

            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"IF_InvInFGNo","Số phiếu nhập kho"},
                 {"CreateBy","Người nhập phiếu"},
                 {"CreateDTimeUTC","Ngày tạo phiếu"},
                 {"IF_InvInFGStatus","Trạng thái"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsDtl()
        {
            return new Dictionary<string, string>()
            {
                 //{"IF_InvInFGNo","Số phiếu nhập kho"},
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"Qty","Số lượng"},
                 {"ProductionDate","Ngày sản xuất"},
                 {"CreateBy","Người nhập phiếu"},
                 {"CreateDTimeSv","Ngày tạo phiếu"},
            };
        }

        #endregion
        private List<InvF_InventoryInFG> List_InvF_InventoryInFGDtl(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listInvF_InventoryInFG = new List<InvF_InventoryInFG>();
            var objRQ_InvF_InventoryInFG = new RQ_InvF_InventoryInFG()
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
                // RQ_InvF_InventoryInFG
                Rt_Cols_InvF_InventoryInFG = "*",
                Rt_Cols_InvF_InventoryInFGDtl = "*",
                InvF_InventoryInFG = null,
            };
            var objRT_InvF_InventoryInFG = InvF_InventoryInFGService.Instance.WA_InvF_InventoryInFG_Get(objRQ_InvF_InventoryInFG, addressAPIs);
            if (objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl != null && objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl.Count > 0)
            {
                listInvF_InventoryInFG.AddRange(objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFG);
            }
            return listInvF_InventoryInFG;
        }
    }
}