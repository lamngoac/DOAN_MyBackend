using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Extensions;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.Utils;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_DealerController : BaseController
    {
        // Đại lý
        // GET: Mst_Dealer
        [HttpGet]
        public ActionResult Index(string dlcode = "", string dlname = "", string dlphoneno = "", string dlemail = "", string flagactive = "", string fromdate = "", string todate = "", string init = "init", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var waDLCode = userState.RptSv_Sys_User.DLCode;
            var pageInfo = new PageInfo<Mst_Dealer>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Dealer>()
            };
            var itemCount = 0;
            var pageCount = 0;

            #region["Search"]
            if (CUtils.IsNullOrEmpty(dlcode) || dlcode == waDLCode)
            {
                #region["Parent Dealer"]
                var objMst_DealerUI = new Mst_DealerUI()
                {
                    //DLCode = waDLCode,
                    //DLCodeParent = waDLCode,
                    DLName = dlname,
                    DLPhoneNo = dlphoneno,
                    DLEmail = dlemail,
                    FlagActive = flagactive,
                    CreateDTimeFrom = fromdate,
                    CreateDTimeTo = todate,
                    DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                };

                var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_DealerUI,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };

                var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                #endregion

                if (objRT_Mst_DealerCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_DealerCur.MySummaryTable.MyCount);

                }

                if (objRT_Mst_DealerCur != null && objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_DealerCur.Lst_Mst_Dealer);
                }

                pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;

                ViewBag.FlagActive = flagactive;
                ViewBag.DLCode = waDLCode;
            }
            else
            {
                var objMst_DealerUI = new Mst_DealerUI()
                {
                    DLCode = dlcode,
                    DLCodeParent = waDLCode,
                    DLName = dlname,
                    DLPhoneNo = dlphoneno,
                    DLEmail = dlemail,
                    FlagActive = flagactive,
                    CreateDTimeFrom = fromdate,
                    CreateDTimeTo = todate,
                    DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                };

                var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_DealerUI,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };

                var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                if (objRT_Mst_DealerCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_DealerCur.MySummaryTable.MyCount);
                }

                if (objRT_Mst_DealerCur != null && objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_DealerCur.Lst_Mst_Dealer);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                ViewBag.FlagActive = flagactive;
                ViewBag.DLCode = dlcode;
            }
            #endregion


            ViewBag.DLName = dlname;
            ViewBag.DLPhoneNo = dlphoneno;
            ViewBag.DLEmail = dlemail;

            ViewBag.CreateDTimeFrom = fromdate;
            ViewBag.CreateDTimeTo = todate;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();
            return View(pageInfo);
        }

        #region["Tạo mới đại lý"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waDLCode = userState.RptSv_Sys_User.DLCode;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var addressAPIs = userState.AddressAPIs;

            var listMst_Dealer = new List<Mst_Dealer>();

            #region["Parent Dealer"]
            var objMst_DealerParentUI = new Mst_DealerUI()
            {
                //DLCode = waDLCode,
                DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
            };

            var strWhereClauseMst_DealerParentUI = strWhereClause_Mst_DealerUI(objMst_DealerParentUI);

            var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_DealerParentUI,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };

            var objRT_Mst_DealerParentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
            #endregion

            //#region["Childrent Dealer"]
            //var objMst_DealerChildrentUI = new Mst_DealerUI()
            //{
            //    DLCodeParent = waDLCode,
            //    DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
            //};

            //var strWhereClauseMst_DealerChildrentUI = strWhereClause_Mst_DealerUI(objMst_DealerChildrentUI);

            //var objRQ_Mst_DealerChildrent = new RQ_Mst_Dealer()
            //{
            //    // WARQBase
            //    Tid = GetNextTId(),
            //    GwUserCode = GwUserCode,
            //    GwPassword = GwPassword,
            //    FuncType = null,
            //    Ft_RecordStart = Ft_RecordStart,
            //    Ft_RecordCount = Ft_RecordCount,
            //    Ft_WhereClause = strWhereClauseMst_DealerChildrentUI,
            //    Ft_Cols_Upd = null,
            //    WAUserCode = waUserCode,
            //    WAUserPassword = waUserPassword,
            //    UtcOffset = userState.UtcOffset,
            //    // RQ_Mst_Dealer
            //    Rt_Cols_Mst_Dealer = "*",
            //    Mst_Dealer = null,
            //};

            //var objRT_Mst_DealerChildrentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerChildrent, addressAPIs);
            //#endregion

            if (objRT_Mst_DealerParentCur != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_DealerParentCur.Lst_Mst_Dealer);
            }

            //if (objRT_Mst_DealerChildrentCur != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer.Count > 0)
            //{
            //    listMst_Dealer.AddRange(objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer);
            //}

            ViewBag.ListMst_Dealer = listMst_Dealer;

            #region["Tỉnh/Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = FlagActive,
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
            listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
            ViewBag.ListMst_Province = listMst_Province;
            #endregion

            ViewBag.DLCodeParent = waDLCode;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var objMst_DealerInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Dealer>(model);
                    //var title = "";
                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer
                    {
                        FlagIsDelete = null,
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = null,
                        Mst_Dealer = objMst_DealerInput
                    };
                    Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Create(objRQ_Mst_Dealer, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Tạo mới đại lý thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region["Sửa đại lý"]
        [HttpGet]
        public ActionResult Update(string dlcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var waDLCode = userState.RptSv_Sys_User.DLCode;
            if (!CUtils.IsNullOrEmpty(dlcode))
            {
                var objMst_DealerUI = new Mst_DealerUI()
                {
                    DLCode = dlcode,
                };

                var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_DealerUI,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };

                var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                {
                    var listMst_Dealer = new List<Mst_Dealer>();

                    #region["Parent Dealer"]
                    var objMst_DealerParentUI = new Mst_DealerUI()
                    {
                        //DLCode = waDLCode,
                        DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                    };

                    var strWhereClauseMst_DealerParentUI = strWhereClause_Mst_DealerUI(objMst_DealerParentUI);

                    var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_DealerParentUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };

                    var objRT_Mst_DealerParentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
                    #endregion

                    //#region["Childrent Dealer"]
                    //var objMst_DealerChildrentUI = new Mst_DealerUI()
                    //{
                    //    DLCodeParent = waDLCode,
                    //    DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                    //};

                    //var strWhereClauseMst_DealerChildrentUI = strWhereClause_Mst_DealerUI(objMst_DealerChildrentUI);

                    //var objRQ_Mst_DealerChildrent = new RQ_Mst_Dealer()
                    //{
                    //    // WARQBase
                    //    Tid = GetNextTId(),
                    //    GwUserCode = GwUserCode,
                    //    GwPassword = GwPassword,
                    //    FuncType = null,
                    //    Ft_RecordStart = Ft_RecordStart,
                    //    Ft_RecordCount = Ft_RecordCount,
                    //    Ft_WhereClause = strWhereClauseMst_DealerChildrentUI,
                    //    Ft_Cols_Upd = null,
                    //    WAUserCode = waUserCode,
                    //    WAUserPassword = waUserPassword,
                    //    UtcOffset = userState.UtcOffset,
                    //    // RQ_Mst_Dealer
                    //    Rt_Cols_Mst_Dealer = "*",
                    //    Mst_Dealer = null,
                    //};

                    //var objRT_Mst_DealerChildrentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerChildrent, addressAPIs);
                    //#endregion

                    if (objRT_Mst_DealerParentCur != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer.Count > 0)
                    {
                        listMst_Dealer.AddRange(objRT_Mst_DealerParentCur.Lst_Mst_Dealer);
                    }

                    //if (objRT_Mst_DealerChildrentCur != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer.Count > 0)
                    //{
                    //    listMst_Dealer.AddRange(objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer);
                    //}

                    ViewBag.ListMst_Dealer = listMst_Dealer;
                    #region["Tỉnh/Thành phố"]
                    var objMst_Province = new Mst_Province()
                    {
                        FlagActive = FlagActive,
                    };
                    var listMst_Province = new List<Mst_Province>();
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                    ViewBag.ListMst_Province = listMst_Province;
                    #endregion
                    return View(objRT_Mst_DealerCur.Lst_Mst_Dealer[0]);
                }
            }

            return View(new Mst_Dealer());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    if (!CUtils.IsNullOrEmpty(model))
                    {
                        var objMst_DealerInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Dealer>(model);
                        var objMst_DealerUI = new Mst_DealerUI()
                        {
                            DLCode = objMst_DealerInput.DLCode,
                        };

                        var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                        var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = RowsWorksheets.ToString(),
                            Ft_WhereClause = strWhereClauseMst_DealerUI,
                            Ft_Cols_Upd = null,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_Mst_Dealer
                            Rt_Cols_Mst_Dealer = "*",
                            Mst_Dealer = null,
                        };

                        var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                        if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                        {
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLCodeParent = objMst_DealerInput.DLCodeParent;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLType = objMst_DealerInput.DLType;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLName = objMst_DealerInput.DLName;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].ProvinceCode = objMst_DealerInput.ProvinceCode;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLAddress = objMst_DealerInput.DLAddress;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLPresentBy = objMst_DealerInput.DLPresentBy;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLGovIDNumber = objMst_DealerInput.DLGovIDNumber;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLEmail = objMst_DealerInput.DLEmail;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].DLPhoneNo = objMst_DealerInput.DLPhoneNo;
                            objRT_Mst_DealerCur.Lst_Mst_Dealer[0].FlagActive = objMst_DealerInput.FlagActive;

                            var strFt_Cols_Upd = "";
                            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLCodeParent);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLType);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLName);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.ProvinceCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLAddress);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLPresentBy);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLGovIDNumber);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLEmail);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLPhoneNo);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.FlagActive);

                            objRQ_Mst_Dealer.Ft_WhereClause = null;
                            objRQ_Mst_Dealer.Ft_Cols_Upd = strFt_Cols_Upd;
                            objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer = null;
                            objRQ_Mst_Dealer.Mst_Dealer = objRT_Mst_DealerCur.Lst_Mst_Dealer[0];

                            Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Update(objRQ_Mst_Dealer, addressAPIs);

                            resultEntry.Success = true;
                            resultEntry.AddMessage("Sửa đại lý thành công!");
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                        else
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Đại lý '" + objMst_DealerInput.DLCode + "' - '" + objMst_DealerInput.DLName + "' không có trong hệ thống!");
                        }
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đại lý trống!");
                    }
                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa đại lý"]
        [HttpGet]
        public ActionResult Detail(string dlcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var waDLCode = userState.RptSv_Sys_User.DLCode;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if(true)
            {
                if (!CUtils.IsNullOrEmpty(dlcode))
                {
                    var objMst_DealerUI = new Mst_DealerUI()
                    {
                        DLCode = dlcode,
                        DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                    };

                    var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_DealerUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };

                    var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                    if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                    {
                        var listMst_Dealer = new List<Mst_Dealer>();

                        #region["Parent Dealer"]
                        var objMst_DealerParentUI = new Mst_DealerUI()
                        {
                            //DLCode = waDLCode,
                            DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                        };

                        var strWhereClauseMst_DealerParentUI = strWhereClause_Mst_DealerUI(objMst_DealerParentUI);

                        var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = strWhereClauseMst_DealerParentUI,
                            Ft_Cols_Upd = null,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_Mst_Dealer
                            Rt_Cols_Mst_Dealer = "*",
                            Mst_Dealer = null,
                        };

                        var objRT_Mst_DealerParentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
                        #endregion

                        //#region["Childrent Dealer"]
                        //var objMst_DealerChildrentUI = new Mst_DealerUI()
                        //{
                        //    DLCodeParent = waDLCode,
                        //    DLBUCode = userState.RptSv_Sys_User.md_DLBUPattern
                        //};

                        //var strWhereClauseMst_DealerChildrentUI = strWhereClause_Mst_DealerUI(objMst_DealerChildrentUI);

                        //var objRQ_Mst_DealerChildrent = new RQ_Mst_Dealer()
                        //{
                        //    // WARQBase
                        //    Tid = GetNextTId(),
                        //    GwUserCode = GwUserCode,
                        //    GwPassword = GwPassword,
                        //    FuncType = null,
                        //    Ft_RecordStart = Ft_RecordStart,
                        //    Ft_RecordCount = Ft_RecordCount,
                        //    Ft_WhereClause = strWhereClauseMst_DealerChildrentUI,
                        //    Ft_Cols_Upd = null,
                        //    WAUserCode = waUserCode,
                        //    WAUserPassword = waUserPassword,
                        //    UtcOffset = userState.UtcOffset,
                        //    // RQ_Mst_Dealer
                        //    Rt_Cols_Mst_Dealer = "*",
                        //    Mst_Dealer = null,
                        //};

                        //var objRT_Mst_DealerChildrentCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_DealerChildrent, addressAPIs);
                        //#endregion

                        if (objRT_Mst_DealerParentCur != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerParentCur.Lst_Mst_Dealer.Count > 0)
                        {
                            listMst_Dealer.AddRange(objRT_Mst_DealerParentCur.Lst_Mst_Dealer);
                        }

                        //if (objRT_Mst_DealerChildrentCur != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer != null && objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer.Count > 0)
                        //{
                        //    listMst_Dealer.AddRange(objRT_Mst_DealerChildrentCur.Lst_Mst_Dealer);
                        //}

                        ViewBag.ListMst_Dealer = listMst_Dealer;
                        #region["Tỉnh/Thành phố"]
                        var objMst_Province = new Mst_Province()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_Province = new List<Mst_Province>();
                        var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                        listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                        ViewBag.ListMst_Province = listMst_Province;
                        #endregion
                        return View(objRT_Mst_DealerCur.Lst_Mst_Dealer[0]);
                    }
                }
                return View(new Mst_Dealer());
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string dlcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if(true)
            {
                if (!CUtils.IsNullOrEmpty(dlcode))
                {
                    var objMst_DealerUI = new Mst_DealerUI()
                    {
                        DLCode = dlcode,
                    };

                    var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_DealerUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };

                    var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                    if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                    {
                        objRQ_Mst_Dealer.Mst_Dealer = objRT_Mst_DealerCur.Lst_Mst_Dealer[0];

                        Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Delete(objRQ_Mst_Dealer, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa đại lý thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đại lý '" + dlcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đại lý trống!");
                }

                return Json(resultEntry);
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

        }
        #endregion

        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var exitsData = "";
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if(true)
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
                    var listMst_Dealer = new List<Mst_Dealer>();
                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 8)
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

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLCode]))
                                {
                                    exitsData = "Mã đại lý không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLName]))
                                {
                                    exitsData = "Tên đại lý không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.ProvinceCode]))
                                {
                                    exitsData = "Mã tỉnh/ thành phố không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLAddress]))
                                {
                                    exitsData = "Địa chỉ không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLPresentBy]))
                                {
                                    exitsData = "Người đại diện không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLGovIDNumber]))
                                {
                                    exitsData = "Số CMT/Hộ chiếu không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLEmail]))
                                {
                                    exitsData = "Email không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var email = dr[TblMst_Dealer.DLEmail].ToString().Trim();
                                    if (!CUtils.IsValidEmail(email))
                                    {
                                        exitsData = "Email không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLPhoneNo]))
                                {
                                    exitsData = "Điện thoại không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var phone = dr[TblMst_Dealer.DLPhoneNo].ToString().Trim();
                                    if (phone.Length <= 10)
                                    {
                                        if (CUtils.IsNumber(phone))
                                        {
                                            var index = phone.IndexOf(".");
                                            if (index >= 0)
                                            {
                                                exitsData = "Điện thoại không hợp lệ!";
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        else
                                        {
                                            exitsData = "Điện thoại không hợp lệ!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Điện thoại không hợp lệ (10 số)!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }

                                idx++;
                            }
                            #endregion

                            #region["Check duplicate"]
                            iRows = 2;
                            strRows = " - Dòng ";
                            var jRows = 2;
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                iRows = 2;
                                iRows = (iRows + i + 1);
                                strRows = " - Dòng ";
                                strRows += iRows;
                                var dlcodeCur = table.Rows[i][TblMst_Dealer.DLCode].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    jRows = 2;
                                    jRows = (jRows + j + 1);
                                    if (i != j)
                                    {
                                        var _dlcodeCur = table.Rows[j][TblMst_Dealer.DLCode].ToString().Trim();
                                        if (dlcodeCur.Equals(_dlcodeCur))
                                        {
                                            strRows += (" - " + jRows);
                                            exitsData = "Mã đại lý '" + dlcodeCur + "' bị lặp trong file excel!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        listMst_Dealer = DataTableCmUtils.ToListof<Mst_Dealer>(table); ;
                        // Gọi hàm save data
                        if (listMst_Dealer != null && listMst_Dealer.Count > 0)
                        {
                            foreach (var item in listMst_Dealer)
                            {
                                //item.FlagActive = "1";
                                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = null,
                                    Ft_Cols_Upd = null,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    // RQ_Mst_Dealer
                                    Rt_Cols_Mst_Dealer = null,
                                    Mst_Dealer = item,
                                };

                                Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Create(objRQ_Mst_Dealer, addressAPIs);
                            }
                        }

                        resultEntry.Success = true;
                        exitsData = "Đã nhập dữ liệu excel thành công!";
                        resultEntry.AddMessage(exitsData);
                    }
                    else
                    {
                        exitsData = "File excel import không có dữ liệu!";
                        resultEntry.AddMessage(exitsData);
                    }
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region["Export excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Dealer>();
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if(true)
                {
                    Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Dealer).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Dealer"));

                    return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
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
        public ActionResult Export(string dlcode = "", string dlname = "", string dlphoneno = "", string dlemail = "", string flagactive = "", string fromdate = "", string todate = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Dealer = new List<Mst_Dealer>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if(true)
                {
                    var objMst_DealerUI = new Mst_DealerUI()
                    {
                        DLCode = dlcode,
                        DLName = dlname,
                        DLPhoneNo = dlphoneno,
                        DLEmail = dlemail,
                        FlagActive = flagactive,
                        CreateDTimeFrom = fromdate,
                        CreateDTimeTo = todate,
                    };

                    #region["Search"]
                    var strWhereClauseMst_DealerUI = strWhereClause_Mst_DealerUI(objMst_DealerUI);

                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_DealerUI,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };

                    var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                    if (objRT_Mst_DealerCur != null && objRT_Mst_DealerCur.Lst_Mst_Dealer != null)
                    {
                        if (objRT_Mst_DealerCur.MySummaryTable != null)
                        {
                            itemCount = Convert.ToInt32(objRT_Mst_DealerCur.MySummaryTable.MyCount);
                        }
                        if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                        {
                            pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        }

                        listMst_Dealer.AddRange(objRT_Mst_DealerCur.Lst_Mst_Dealer);

                        Dictionary<string, string> dicColNames = GetImportDicColums();
                        string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Dealer).Name), ref url);
                        ExcelExport.ExportToExcelFromList(listMst_Dealer, dicColNames, filePath, string.Format("Mst_Dealer"));


                        #region["Export các trang còn lại"]
                        listMst_Dealer.Clear();
                        if (pageCount > 1)
                        {
                            for (var i = 1; i <= pageCount; i++)
                            {
                                objRQ_Mst_Dealer.Ft_RecordStart = (i * RowsWorksheets).ToString();

                                var objRT_Mst_DealerExportCur = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                                if (objRT_Mst_DealerExportCur != null && objRT_Mst_DealerExportCur.Lst_Mst_Dealer != null && objRT_Mst_DealerExportCur.Lst_Mst_Dealer.Count > 0)
                                {
                                    listMst_Dealer.AddRange(objRT_Mst_DealerExportCur.Lst_Mst_Dealer);
                                    ExcelExport.ExportToExcelFromList(listMst_Dealer, dicColNames, filePath, string.Format("Mst_Dealer_" + i));
                                    listMst_Dealer.Clear();
                                }
                            }
                        }
                        #endregion

                        return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                    }
                    #endregion

                    else
                    {
                        return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
                    }
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
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
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Dealer.DLCode,"Mã đại lý (*)"},
                {TblMst_Dealer.DLName,"Tên đại lý (*)"},
                {TblMst_Dealer.ProvinceCode,"Mã tỉnh/thành phố (*)"},
                {TblMst_Dealer.DLAddress,"Địa chỉ (*)"},
                {TblMst_Dealer.DLPresentBy,"Người đại diện (*)"},
                {TblMst_Dealer.DLGovIDNumber,"Số CMT/Hộ chiếu (*)"},
                {TblMst_Dealer.DLEmail,"Email (*)"},
                {TblMst_Dealer.DLPhoneNo,"Điện thoại (*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Dealer.DLCode,"Mã đại lý (*)"},
                {TblMst_Dealer.DLName,"Tên đại lý (*)"},
                {TblMst_Dealer.ProvinceCode,"Mã tỉnh/thành phố (*)"},
                {TblMst_Dealer.DLAddress,"Địa chỉ (*)"},
                {TblMst_Dealer.DLPresentBy,"Người đại diện (*)"},
                {TblMst_Dealer.DLGovIDNumber,"Số CMT/Hộ chiếu (*)"},
                {TblMst_Dealer.DLEmail,"Email (*)"},
                {TblMst_Dealer.DLPhoneNo,"Điện thoại (*)"},
                {TblMst_Dealer.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region[""]
        private string strWhereClause_Mst_DealerUI(Mst_DealerUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLCode, CUtils.StrValue(data.DLCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DLCodeParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLCodeParent, CUtils.StrValue(data.DLCodeParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DLName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLName, "%" + CUtils.StrValue(data.DLName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.DLPhoneNo))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLPhoneNo, "%" + CUtils.StrValue(data.DLPhoneNo) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.DLEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLEmail, "%" + CUtils.StrValue(data.DLEmail) + "%", "like");
            }
            //if (!CUtils.IsNullOrEmpty(data.CreateDTimeFrom))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.LogLUDTimeUTC, CUtils.ConvertingLocalTimeToUTC(data.CreateDTimeFrom).ToString(Nonsense.DATE_TIME_FORMAT), ">=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.CreateDTimeTo))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.LogLUDTimeUTC, CUtils.ConvertingLocalTimeToUTC(data.CreateDTimeTo).ToString(Nonsense.DATE_TIME_FORMAT), "<=");
            //}
            if (!CUtils.IsNullOrEmpty(data.DLBUCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLBUCode, CUtils.StrValue(data.DLBUCode), "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}