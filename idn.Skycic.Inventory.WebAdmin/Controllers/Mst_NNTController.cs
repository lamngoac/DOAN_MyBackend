using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.WebAdmin.Models;
using inos.common.Model;
using inos.common.Service;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_NNTController : BaseController
    {
        // GET: Mst_NNT
        [HttpGet]
        public ActionResult Index(string mst = "", string nntfullname = "", string dlcode = "", string fromdate = "", string todate = "", string networkid = "", string orgid = "", string contactemail = "", string registerStatus = "", string init = "init", int recordCount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var pageInfo = new PageInfo<Mst_NNT>(0, recordCount)
            {
                DataList = new List<Mst_NNT>()
            };
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true) //Bỏ check phân quyền check cờ quản trị
            {
                ViewBag.FlagidocNetAdmin = "1";
                var itemCount = 0;
                var pageCount = 0;

                #region["Mã đại lý"]

                var strWhereClauseMst_Dealer = "";
                if (!userState.RptSv_Sys_User.DLCode.Equals(Mst_Dealer_Client.IDOCNET))
                {
                    var md_DLBUPattern = userState.RptSv_Sys_User.md_DLBUPattern;
                    strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(new Mst_Dealer() { DLBUCode = md_DLBUPattern, FlagActive = FlagActive, });
                }
                else
                {
                    strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(new Mst_Dealer() { FlagActive = FlagActive });
                }
                var listMst_Dealer = new List<Mst_Dealer>();
                if (List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer) != null && List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer).Count > 0)
                {
                    listMst_Dealer.AddRange(List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer));
                }

                ViewBag.ListMst_Dealer = listMst_Dealer;
                #endregion

                if (init != "init")
                {
                    #region["Search"]
                    //if (!CUtils.IsNullOrEmpty(dlcode))
                    //{
                    var objRT_Mst_NNTCur = new RT_Mst_NNT();

                    var objMst_NNT = new Mst_NNTUI()
                    {
                        MST = mst,
                        NNTFullName = nntfullname,
                        DLCode = dlcode,
                        FromDate = fromdate,
                        ToDate = todate,
                        NetworkID = networkid,
                        OrgID = orgid,
                        ContactEmail = contactemail,
                        RegisterStatus = registerStatus,
                        MSTParent = ClientMix.MSTRoot,
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNTSearch(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordCount = recordCount.ToString(),
                        Ft_RecordStart = (Convert.ToInt32(page) * recordCount).ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);

                    if (objRT_Mst_NNTCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_NNTCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_NNTCur != null && objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        pageInfo.DataList.AddRange(objRT_Mst_NNTCur.Lst_Mst_NNT);
                        pageCount = (itemCount % recordCount == 0) ? itemCount / recordCount : itemCount / recordCount + 1;
                    }
                    //}
                    //else
                    //{
                    //    if (listMst_Dealer != null && listMst_Dealer.Count > 0)
                    //    {
                    //        foreach (var item in listMst_Dealer)
                    //        {
                    //            var objRT_Mst_NNTCur = new RT_Mst_NNT();

                    //            var objMst_NNT = new Mst_NNTUI()
                    //            {
                    //                MST = mst,
                    //                NNTFullName = nntfullname,
                    //                DLCode = item.DLCode,
                    //                FromDate = fromdate,
                    //                ToDate = todate,
                    //                NetworkID = networkid,
                    //                OrgID = orgid,
                    //                ContactEmail = contactemail,
                    //                RegisterStatus = registerStatus,
                    //                MSTParent = ClientMix.MSTRoot,
                    //            };

                    //            var strWhereClauseMst_NNT = strWhereClause_Mst_NNTSearch(objMst_NNT);

                    //            var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    //            {
                    //                // WARQBase
                    //                Tid = GetNextTId(),
                    //                GwUserCode = GwUserCode,
                    //                GwPassword = GwPassword,
                    //                FuncType = null,
                    //                Ft_RecordCount = recordCount.ToString(),
                    //                Ft_RecordStart = (Convert.ToInt32(page) * recordCount).ToString(),
                    //                Ft_WhereClause = strWhereClauseMst_NNT,
                    //                Ft_Cols_Upd = null,
                    //                WAUserCode = waUserCode,
                    //                WAUserPassword = waUserPassword,
                    //                UtcOffset = userState.UtcOffset,
                    //                // RQ_Mst_NNT
                    //                Rt_Cols_Mst_NNT = "*",
                    //                Mst_NNT = null,
                    //            };

                    //            objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);

                    //            if (objRT_Mst_NNTCur.MySummaryTable != null)
                    //            {
                    //                itemCount += Convert.ToInt32(objRT_Mst_NNTCur.MySummaryTable.MyCount);
                    //            }
                    //            if (objRT_Mst_NNTCur != null && objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    //            {
                    //                pageInfo.DataList.AddRange(objRT_Mst_NNTCur.Lst_Mst_NNT);
                    //            }
                    //        }

                    //        pageCount = (itemCount % recordCount == 0) ? itemCount / recordCount : itemCount / recordCount + 1;
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    init = "search";
                    dlcode = userState.RptSv_Sys_User.DLCode;
                }

                ViewBag.MST = mst;
                ViewBag.NNTFullName = nntfullname;
                ViewBag.ContactEmail = contactemail;
                ViewBag.RegisterStatus = registerStatus;
                ViewBag.DLCode = dlcode;
                ViewBag.FromDate = fromdate;
                ViewBag.ToDate = todate;
                ViewBag.NetworkID = networkid;
                ViewBag.OrgID = orgid;

                pageInfo.PageSize = recordCount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordCount).ToString();
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

            return View(pageInfo);
        }

        [HttpGet]
        public ActionResult Update(string mst)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var inosUserCode = "sysadmin";
            var inosUserPassword = "123456";
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
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

                #region["Quận/ Huyện"]
                var objMst_District = new Mst_District()
                {
                    FlagActive = FlagActive,
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                ViewBag.ListMst_District = listMst_District;
                #endregion

                #region["Đại lý"]
                var objMst_Dealer = new Mst_Dealer()
                {
                    FlagActive = FlagActive,
                };
                var listMst_Dealer = new List<Mst_Dealer>();
                var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(objMst_Dealer);
                listMst_Dealer.AddRange(List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer));
                ViewBag.ListMst_Dealer = listMst_Dealer;
                #endregion

                #region["Loại giấy tờ"]
                var objMst_GovIDType = new Mst_GovIDType()
                {
                    FlagActive = FlagActive,
                };
                var listMst_GovIDType = new List<Mst_GovIDType>();
                var strWhereClauseMst_GovIDType = strWhereClause_Mst_GovIDType(objMst_GovIDType);
                listMst_GovIDType.AddRange(List_Mst_GovIDType(strWhereClauseMst_GovIDType));
                ViewBag.ListMst_GovIDType = listMst_GovIDType;
                #endregion

                #region["BizType"]
                var objiNOS_Mst_BizType = new iNOS_Mst_BizType()
                {
                    FlagActive = "1",
                };
                var listiNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
                var strWhereClauseiNOS_Mst_BizType = strWhereClause_iNOS_Mst_BizType(objiNOS_Mst_BizType);

                var objRQ_iNOS_Mst_BizType = new RQ_iNOS_Mst_BizType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseiNOS_Mst_BizType,
                    Ft_Cols_Upd = null,
                    WAUserCode = inosUserCode,
                    WAUserPassword = inosUserPassword,
                    //UtcOffset = UserState.UtcOffset,
                    // RQ_iNOS_Mst_BizType
                    Rt_Cols_iNOS_Mst_BizType = "*",
                    iNOS_Mst_BizType = null,
                };
                var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_RptSv_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressAPIs);
                if (objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType != null && objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType.Count > 0)
                {
                    listiNOS_Mst_BizType.AddRange(objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType);
                }

                ViewBag.ListiNOS_Mst_BizType = listiNOS_Mst_BizType;
                #endregion

                #region["BizField"]
                var objiNOS_Mst_BizField = new iNOS_Mst_BizField()
                {
                    FlagActive = "1",
                };
                var listiNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
                var strWhereClauseiNOS_Mst_BizField = strWhereClause_iNOS_Mst_BizField(objiNOS_Mst_BizField);

                var objRQ_iNOS_Mst_BizField = new RQ_iNOS_Mst_BizField()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseiNOS_Mst_BizField,
                    Ft_Cols_Upd = null,
                    WAUserCode = inosUserCode,
                    WAUserPassword = inosUserPassword,
                    //UtcOffset = UserState.UtcOffset,
                    // RQ_iNOS_Mst_BizField
                    Rt_Cols_iNOS_Mst_BizField = "*",
                    iNOS_Mst_BizField = null,
                };
                var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_RptSv_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressAPIs);

                if (objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField != null && objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField.Count > 0)
                {
                    listiNOS_Mst_BizField.AddRange(objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField);
                }

                ViewBag.ListiNOS_Mst_BizField = listiNOS_Mst_BizField;
                #endregion

                #region["BizSize"]
                var objiNOS_Mst_BizSize = new iNOS_Mst_BizSize()
                {
                    FlagActive = "1",
                };
                var listiNOS_Mst_BizSize = new List<iNOS_Mst_BizSize>();
                var strWhereClauseiNOS_Mst_BizSize = strWhereClause_iNOS_Mst_BizSize(objiNOS_Mst_BizSize);

                var objRQ_iNOS_Mst_BizSize = new RQ_iNOS_Mst_BizSize()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseiNOS_Mst_BizSize,
                    Ft_Cols_Upd = null,
                    WAUserCode = inosUserCode,
                    WAUserPassword = inosUserPassword,
                    //UtcOffset = UserState.UtcOffset,
                    // RQ_iNOS_Mst_BizSize
                    Rt_Cols_iNOS_Mst_BizSize = "*",
                    iNOS_Mst_BizSize = null,
                };
                var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_RptSv_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressAPIs);
                if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
                {
                    listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
                }

                ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
                #endregion

                if (!CUtils.IsNullOrEmpty(mst))
                {
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = mst
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        #region["Mã chi cục thuế"]
                        //var objMst_GovTaxID = new Mst_GovTaxID()
                        //{
                        //    FlagActive = FlagActive,
                        //};
                        //var listMst_GovTaxID = new List<Mst_GovTaxID>();
                        //var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
                        //listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
                        //ViewBag.ListMst_GovTaxID = listMst_GovTaxID;
                        #endregion

                        #region["Loại khách hàng"]
                        var objMst_NNTType = new Mst_NNTType()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_NNTType = new List<Mst_NNTType>();
                        var strWhereClauseMst_NNTType = strWhereClause_Mst_NNTType(objMst_NNTType);
                        listMst_NNTType.AddRange(List_Mst_NNTType(strWhereClauseMst_NNTType));
                        ViewBag.ListMst_NNTType = listMst_NNTType;
                        #endregion
                        return View(objRT_Mst_NNTCur.Lst_Mst_NNT[0]);
                    }
                }
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            return View(new Mst_NNT());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = objMst_NNTInput.MST
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);

                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].RegisterStatus = objMst_NNTInput.RegisterStatus;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].Remark = objMst_NNTInput.Remark;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTFullName = objMst_NNTInput.NNTFullName;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].MSTParent = objMst_NNTInput.MSTParent;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].ProvinceCode = objMst_NNTInput.ProvinceCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].DistrictCode = objMst_NNTInput.DistrictCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].DLCode = objMst_NNTInput.DLCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTAddress = objMst_NNTInput.NNTAddress;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTMobile = objMst_NNTInput.NNTMobile;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTPhone = objMst_NNTInput.NNTPhone;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTFax = objMst_NNTInput.NNTFax;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentBy = objMst_NNTInput.PresentBy;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].BusinessRegNo = objMst_NNTInput.BusinessRegNo;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTPosition = objMst_NNTInput.NNTPosition;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentIDNo = objMst_NNTInput.PresentIDNo;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentIDType = objMst_NNTInput.PresentIDType;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].GovTaxID = objMst_NNTInput.GovTaxID;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactName = objMst_NNTInput.ContactName;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactPhone = objMst_NNTInput.ContactPhone;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactEmail = objMst_NNTInput.ContactEmail;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].Website = objMst_NNTInput.Website;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].BizType = objMst_NNTInput.BizType;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].BizFieldCode = objMst_NNTInput.BizFieldCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].BizSizeCode = objMst_NNTInput.BizSizeCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].CANumber = objMst_NNTInput.CANumber;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].CAOrg = objMst_NNTInput.CAOrg;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].CAEffDTimeUTCStart = objMst_NNTInput.CAEffDTimeUTCStart;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].CAEffDTimeUTCEnd = objMst_NNTInput.CAEffDTimeUTCEnd;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].PackageCode = objMst_NNTInput.PackageCode;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].AccNo = objMst_NNTInput.AccNo;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].AccHolder = objMst_NNTInput.AccHolder;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].BankName = objMst_NNTInput.BankName;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].TCTStatus = objMst_NNTInput.TCTStatus;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
                        //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.MST);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.RegisterStatus);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTFullName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.MSTParent);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ProvinceCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.DistrictCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.DLCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTAddress);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTMobile);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTPhone);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTFax);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentBy);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BusinessRegNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTPosition);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentIDNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentIDType);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.GovTaxID);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactPhone);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactEmail);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.Website);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BizType);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BizFieldCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BizSizeCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CANumber);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CAOrg);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CAEffDTimeUTCStart);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CAEffDTimeUTCEnd);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PackageCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.AccNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.AccHolder);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BankName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.TCTStatus);


                        objRQ_Mst_NNT.Ft_WhereClause = null;
                        objRQ_Mst_NNT.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_NNT.Rt_Cols_Mst_NNT = null;
                        objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                        Mst_NNTService.Instance.WA_RptSv_Mst_NNT_UpdateRegisterStatus(objRQ_Mst_NNT, addressAPIs);

                        //Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Update(objRQ_Mst_NNT, addressAPIs);

                        if (objMst_NNTInput.RegisterStatus.Equals("APPROVE"))
                        //if (objMst_NNTInput.msio_OrderId.Equals("APPROVE"))
                        {

                            #region["Sendmail"]
                            var strTitle = @"idocNet thông báo tài khoản đăng nhập và xác nhận đơn đặt hàng phần mềm Inbrand Clound của Quý khách.";

                            var strBodyHTML = BodyEMail.GetContentMailRegister(objRT_Mst_NNTCur.Lst_Mst_NNT[0]);

                            var listToMail = new List<string>() { objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactEmail.ToString(), };
                            var listCcMail = new List<string>();
                            var listBccMail = new List<string>();

                            var objSendMail = new SendMail()
                            {
                                ApiSendMail = APIsSendMail,
                                ApiKeySendMail = ApiKeySendMail,
                                DisplayNameMailFrom = DisplayNameMailFrom,
                                MailFrom = MailFrom,
                                ToMail = listToMail,
                                CcMail = listCcMail,
                                BccMail = listBccMail,
                                Subject = strTitle,
                                HtmlBody = strBodyHTML,
                                SolutionCode = SolutionCode,
                                OrgId = CUtils.StrValueNew(objRT_Mst_NNTCur.Lst_Mst_NNT[0].OrgID),
                            };
                            var objJsonResultUtil = new SendMailUtil().SendMail(objSendMail);
                            #endregion
                        }

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa NNT thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("MST '" + objMst_NNTInput.MST + " không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("MST trống!");
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = objMst_NNTInput.MST
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        string strInosBaseUrl = ConfigurationManager.AppSettings["InosBaseUrl"];

                        inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

                        OrderService objOrderService = new OrderService(null);
                        inos.common.Service.AccountService objAccountService = new AccountService(null);
                        var state = objAccountService.RequestToken("qinvoiceordadmin@inos.vn", "Csc123456", new string[] { "test" });

                        objOrderService.AccessToken = objAccountService.AccessToken;

                        var item = new LicOrder()
                        {
                            Id = Convert.ToInt64(objMst_NNTInput.msio_OrderId),
                            Status = objMst_NNTInput.ipmio_Status.Equals("APPROVE") ? LicOrderStatuses.Approved : LicOrderStatuses.Cancelled
                        };
                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                        LicOrder objCheckOrderStatus = objOrderService.CheckOrderStatus(item);

                        LicOrder objLicOrder = new LicOrder();

                        if (objMst_NNTInput.ipmio_Status.Equals("APPROVE") && !objCheckOrderStatus.Status.Equals(LicOrderStatuses.Approved))
                        {
                            objLicOrder = objOrderService.ApproveOrder(item);
                        }
                        else if (objMst_NNTInput.ipmio_Status.Equals("CANCELLED") && !objCheckOrderStatus.Status.Equals(LicOrderStatuses.Cancelled))
                        {
                            objLicOrder = objOrderService.CancelOrder(item);
                        }

                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].RegisterStatus = objMst_NNTInput.ipmio_Status;
                        objRT_Mst_NNTCur.Lst_Mst_NNT[0].Remark = objMst_NNTInput.Remark;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.RegisterStatus);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.Remark);

                        objRQ_Mst_NNT.Ft_WhereClause = null;
                        objRQ_Mst_NNT.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_NNT.Rt_Cols_Mst_NNT = null;
                        objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                        Mst_NNTService.Instance.WA_RptSv_Mst_NNT_UpdateRegisterStatus(objRQ_Mst_NNT, addressAPIs);

                        if (objMst_NNTInput.ipmio_Status.Equals("APPROVE"))
                        {
                            #region["Sendmail"]
                            var strTitle = @"idocNet thông báo tài khoản đăng nhập và xác nhận đơn đặt hàng phần mềm Inbrand Clound của Quý khách.";

                            var strBodyHTML = BodyEMail.GetContentMailRegister(objRT_Mst_NNTCur.Lst_Mst_NNT[0]);

                            var listToMail = new List<string>() { objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactEmail.ToString(), };
                            var listCcMail = new List<string>();
                            var listBccMail = new List<string>();

                            var objSendMail = new SendMail()
                            {
                                ApiSendMail = APIsSendMail,
                                ApiKeySendMail = ApiKeySendMail,
                                DisplayNameMailFrom = DisplayNameMailFrom,
                                MailFrom = MailFrom,
                                ToMail = listToMail,
                                CcMail = listCcMail,
                                BccMail = listBccMail,
                                Subject = strTitle,
                                HtmlBody = strBodyHTML,
                                SolutionCode = SolutionCode,
                                OrgId = CUtils.StrValueNew(objRT_Mst_NNTCur.Lst_Mst_NNT[0].OrgID),
                            };
                            var objJsonResultUtil = new SendMailUtil().SendMail(objSendMail);
                            #endregion
                        }

                        resultEntry.Success = true;
                        var message = "Duyệt đơn hàng thành công!";
                        if (!objMst_NNTInput.ipmio_Status.Equals("APPROVE"))
                        {
                            message = "Hủy đơn hàng thành công!";
                        }
                        resultEntry.AddMessage(message);
                        resultEntry.RedirectUrl = Url.Action("Update", "Mst_NNT", new { objRT_Mst_NNTCur.Lst_Mst_NNT[0].MST });
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("MST '" + objMst_NNTInput.MST + " không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("MST trống!");
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);

        }

        [HttpGet]
        public ActionResult Detail(string mst)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

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

            #region["Quận/ Huyện"]
            var objMst_District = new Mst_District()
            {
                FlagActive = FlagActive,
            };
            var listMst_District = new List<Mst_District>();
            var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
            listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
            ViewBag.ListMst_District = listMst_District;
            #endregion

            #region["Đại lý"]
            var objMst_Dealer = new Mst_Dealer()
            {
                FlagActive = FlagActive,
            };
            var listMst_Dealer = new List<Mst_Dealer>();
            var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(objMst_Dealer);
            listMst_Dealer.AddRange(List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer));
            ViewBag.ListMst_Dealer = listMst_Dealer;
            #endregion

            #region["Loại giấy tờ"]
            var objMst_GovIDType = new Mst_GovIDType()
            {
                FlagActive = FlagActive,
            };
            var listMst_GovIDType = new List<Mst_GovIDType>();
            var strWhereClauseMst_GovIDType = strWhereClause_Mst_GovIDType(objMst_GovIDType);
            listMst_GovIDType.AddRange(List_Mst_GovIDType(strWhereClauseMst_GovIDType));
            ViewBag.ListMst_GovIDType = listMst_GovIDType;
            #endregion

            #region["BizType"]
            var objiNOS_Mst_BizType = new iNOS_Mst_BizType()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
            var strWhereClauseiNOS_Mst_BizType = strWhereClause_iNOS_Mst_BizType(objiNOS_Mst_BizType);

            var objRQ_iNOS_Mst_BizType = new RQ_iNOS_Mst_BizType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizType
                Rt_Cols_iNOS_Mst_BizType = "*",
                iNOS_Mst_BizType = null,
            };
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_RptSv_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressAPIs);
            if (objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType != null && objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType.Count > 0)
            {
                listiNOS_Mst_BizType.AddRange(objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType);
            }

            ViewBag.ListiNOS_Mst_BizType = listiNOS_Mst_BizType;
            #endregion

            #region["BizField"]
            var objiNOS_Mst_BizField = new iNOS_Mst_BizField()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
            var strWhereClauseiNOS_Mst_BizField = strWhereClause_iNOS_Mst_BizField(objiNOS_Mst_BizField);

            var objRQ_iNOS_Mst_BizField = new RQ_iNOS_Mst_BizField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizField,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizField
                Rt_Cols_iNOS_Mst_BizField = "*",
                iNOS_Mst_BizField = null,
            };
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_RptSv_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressAPIs);

            if (objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField != null && objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField.Count > 0)
            {
                listiNOS_Mst_BizField.AddRange(objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField);
            }

            ViewBag.ListiNOS_Mst_BizField = listiNOS_Mst_BizField;
            #endregion

            #region["BizSize"]
            var objiNOS_Mst_BizSize = new iNOS_Mst_BizSize()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizSize = new List<iNOS_Mst_BizSize>();
            var strWhereClauseiNOS_Mst_BizSize = strWhereClause_iNOS_Mst_BizSize(objiNOS_Mst_BizSize);

            var objRQ_iNOS_Mst_BizSize = new RQ_iNOS_Mst_BizSize()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizSize,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizSize
                Rt_Cols_iNOS_Mst_BizSize = "*",
                iNOS_Mst_BizSize = null,
            };
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_RptSv_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressAPIs);
            if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
            {
                listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
            }

            ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
            #endregion

            if (!CUtils.IsNullOrEmpty(mst))
            {
                var objMst_NNT = new Mst_NNT()
                {
                    MST = mst
                };

                var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                var objRQ_Mst_NNT = new RQ_Mst_NNT()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_NNT,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    Mst_NNT = null,
                };

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    #region["Mã chi cục thuế"]
                    //var objMst_GovTaxID = new Mst_GovTaxID()
                    //{
                    //    FlagActive = FlagActive,
                    //};
                    //var listMst_GovTaxID = new List<Mst_GovTaxID>();
                    //var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
                    //listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
                    //ViewBag.ListMst_GovTaxID = listMst_GovTaxID;
                    #endregion

                    #region["Loại khách hàng"]
                    var objMst_NNTType = new Mst_NNTType()
                    {
                        FlagActive = FlagActive,
                    };
                    var listMst_NNTType = new List<Mst_NNTType>();
                    var strWhereClauseMst_NNTType = strWhereClause_Mst_NNTType(objMst_NNTType);
                    listMst_NNTType.AddRange(List_Mst_NNTType(strWhereClauseMst_NNTType));
                    ViewBag.ListMst_NNTType = listMst_NNTType;
                    #endregion
                    return View(objRT_Mst_NNTCur.Lst_Mst_NNT[0]);
                }
            }
            return View(new Mst_NNT());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string mst)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            if (!CUtils.IsNullOrEmpty(mst))
            {
                var objMst_NNT = new Mst_NNT()
                {
                    MST = mst
                };

                var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                var objRQ_Mst_NNT = new RQ_Mst_NNT()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_NNT,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    Mst_NNT = null,
                };

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                    Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Delete(objRQ_Mst_NNT, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa khách hàng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã số thuế '" + mst + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã số thuế trống!");
            }

            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KhoiTaoHeThong(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = objMst_NNTInput.MST
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        //#region["Lấy NetworkID"]
                        //var objRQ_MstSv_Inos_Org = new RQ_MstSv_Inos_Org()
                        //{
                        //    // WARQBase
                        //    Tid = GetNextTId(),
                        //    GwUserCode = GwUserCode,
                        //    GwPassword = GwPassword,
                        //    FuncType = null,
                        //    Ft_RecordStart = Ft_RecordStart,
                        //    Ft_RecordCount = Ft_RecordCount,
                        //    Ft_WhereClause = null,
                        //    Ft_Cols_Upd = null,
                        //    WAUserCode = waUserCode,
                        //    WAUserPassword = waUserPassword,
                        //    AccessToken = TokenID,
                        //    // RQ_MstSv_Inos_Org
                        //    MstSv_Inos_Org = new MstSv_Inos_Org() { MST = objRT_Mst_NNTCur.Lst_Mst_NNT[0].MST },
                        //};

                        //MasterServerService.Instance.WA_MstSv_Inos_Org_BuildAndCreate(objRQ_MstSv_Inos_Org, addressAPIs);
                        //#endregion

                        #region["Gen Network"]

                        var objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
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
                            AccessToken = TokenID,
                            // RQ_MstSv_Mst_Network
                            MstSv_Mst_Network = new MstSv_Mst_Network() { MST = objRT_Mst_NNTCur.Lst_Mst_NNT[0].MST }
                        };

                        MstSv_Mst_NetworkService.Instance.WA_MstSv_Mst_Network_Gen(objRQ_MstSv_Mst_Network, addressAPIs);
                        #endregion


                        resultEntry.Success = true;
                        resultEntry.AddMessage("Khởi tạo hệ thống thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("MST '" + objMst_NNTInput.MST + " không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("MST trống!");
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string mst = "", string nntfullname = "", string dlcode = "", string contactemail = "", string fromdate = "", string todate = "", string networkid = "", string orgid = "", string registerStatus = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var itemCount = 0;
            var pageCount = 0;
            string url = "";
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var listMst_NNTExport = new List<Mst_NNT>();
                    var objMst_NNT = new Mst_NNTUI()
                    {
                        MST = mst,
                        NNTFullName = nntfullname,
                        DLCode = dlcode,
                        ContactEmail = contactemail,
                        FromDate = fromdate,
                        ToDate = todate,
                        NetworkID = networkid,
                        OrgID = orgid,
                        RegisterStatus = registerStatus,
                        MSTParent = ClientMix.MSTRoot,
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNTSearch(objMst_NNT);
                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNT,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        listMst_NNTExport.AddRange(objRT_Mst_NNTCur.Lst_Mst_NNT);
                        if (objRT_Mst_NNTCur.MySummaryTable != null)
                        {
                            itemCount = Convert.ToInt32(objRT_Mst_NNTCur.MySummaryTable.MyCount);
                        }
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;

                        Dictionary<string, string> dicColNames = GetImportDicColums();
                        string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_User).Name), ref url);

                        ExcelExport.ExportToExcelFromList(listMst_NNTExport, dicColNames, filePath, string.Format("Mst_NNT"));

                        #region["Export các trang còn lại"]
                        listMst_NNTExport.Clear();
                        if (pageCount > 1)
                        {
                            for (var i = 1; i <= pageCount; i++)
                            {
                                objRQ_Mst_NNT.Ft_RecordStart = (i * RowsWorksheets).ToString();

                                var objRT_Mst_NNTExportCur = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                                if (objRT_Mst_NNTExportCur.Lst_Mst_NNT != null && objRT_Mst_NNTExportCur.Lst_Mst_NNT.Count > 0)
                                {
                                    listMst_NNTExport.AddRange(objRT_Mst_NNTExportCur.Lst_Mst_NNT);
                                    ExcelExport.ExportToExcelFromList(listMst_NNTExport, dicColNames, filePath, string.Format("Mst_NNT_" + i));

                                    listMst_NNTExport.Clear();
                                }
                            }
                        }
                        #endregion

                        return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                    }
                    else
                    {
                        return Json(new { Success = true, Title = "Không có dữ liệu!", CheckSuccess = "1" });
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

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {"MST","Mã khách hàng"},
                {"NNTFullName","Tên khách hàng"},
                {"NNTAddress","Địa chỉ"},
                {"mp_ProvinceName","Tỉnh/Thành phố"},
                {"ContactName","Người liên hệ"},
                {"ContactEmail","Email liên hệ"},
                {"ContactPhone","Điện thoại liên hệ"},
                {"Remark","Ghi chú"},
                {"RegisterStatus","TT đăng ký"}
            };
        }

        #region["strWhereClause"]
        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValue(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }


            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNTSearch(Mst_NNTUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, "%" + CUtils.StrValue(data.MST) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
            }

            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.DLCode, CUtils.StrValue(data.DLCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + "CreateDTimeUTC", CUtils.StrValue(data.FromDate + " 00:00:00"), ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + "CreateDTimeUTC", CUtils.StrValue(data.ToDate + " 23:59:59"), "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactPhone))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactPhone, "%" + CUtils.StrValue(data.ContactPhone) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.RegisterStatus))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.RegisterStatus, CUtils.StrValue(data.RegisterStatus), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Dealer(Mst_Dealer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLCode, CUtils.StrValue(data.DLCode), "=");
            }
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
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_GovIDType(Mst_GovIDType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovIDType = TableName.Mst_GovIDType + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovIDType + TblMst_GovIDType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizType(iNOS_Mst_BizType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizType = TableName.iNOS_Mst_BizType + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizType + TbliNOS_Mst_BizType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizSize(iNOS_Mst_BizSize data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizSize = TableName.iNOS_Mst_BizSize + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizSize + TbliNOS_Mst_BizSize.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizField(iNOS_Mst_BizField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizField = TableName.iNOS_Mst_BizField + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizField + TbliNOS_Mst_BizField.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_GovTaxID(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
            if (!CUtils.IsNullOrEmpty(data.GovTaxID))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxID, CUtils.StrValue(data.GovTaxID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.GovTaxName))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxName, "%" + CUtils.StrValue(data.GovTaxName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNTType(Mst_NNTType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNTType = TableName.Mst_NNTType + ".";
            if (!CUtils.IsNullOrEmpty(data.NNTType))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNTType + TblMst_NNTType.NNTType, CUtils.StrValue(data.NNTType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNTType + TblMst_NNTType.NNTTypeName, "%" + CUtils.StrValue(data.NNTTypeName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNTType + TblMst_NNTType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

    }
}