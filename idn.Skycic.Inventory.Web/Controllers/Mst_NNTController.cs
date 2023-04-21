using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
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
    public class Mst_NNTController : AdminBaseController
    {
        // GET: Mst_NNT
        [HttpGet]
        public ActionResult Index(string mst = "", string nntfullname = "", string contactemail = "", string flagactive = "", string init = "init", int RecordCount = 10, int page = 0)
        {
            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;

            var pageInfo = new PageInfo<Mst_NNT>(0, RecordCount)
            {
                DataList = new List<Mst_NNT>()
            };
            var itemCount = 0;
            var pageCount = 0;

            #region["Mã số thuế"]
            var objMst_NNTCur = new Mst_NNT()
            {
                MST = waMST,
                FlagActive = FlagActive,
            };
            var listMst_NNTCur = new List<Mst_NNT>();
            var strWhereClauseMst_NNTCur = strWhereClause_Mst_NNT(objMst_NNTCur);
            listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCur));

            var objMst_NNTCurChil = new Mst_NNT()
            {
                MSTParent = waMST,
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_NNTCurChil = strWhereClause_Mst_NNTCurChil(objMst_NNTCurChil);
            listMst_NNTCur.AddRange(List_Mst_NNT(strWhereClauseMst_NNTCurChil));

            ViewBag.ListMst_NNTCur = listMst_NNTCur;
            #endregion

            if (init != "init")
            {
                #region["Search"]
                var objRT_Mst_NNTCur = new RT_Mst_NNT();

                if (!CUtils.IsNullOrEmpty(mst))
                {
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = mst,
                        NNTFullName = nntfullname,
                        ContactEmail = contactemail,
                        FlagActive = flagactive
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
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

                    objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                }
                else
                {
                    var objMst_NNT = new Mst_NNT()
                    {
                        MST = waMST,
                        NNTFullName = nntfullname,
                        ContactEmail = contactemail,
                        FlagActive = flagactive
                    };

                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
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

                    objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);

                    var objMst_NNTChil = new Mst_NNT()
                    {
                        MSTParent = waMST,
                        NNTFullName = nntfullname,
                        ContactEmail = contactemail,
                        FlagActive = flagactive
                    };

                    var strWhereClauseMst_NNTChil = strWhereClause_Mst_NNT(objMst_NNTChil);

                    var objRQ_Mst_NNTChil = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordCount = RecordCount.ToString(),
                        Ft_RecordStart = (Convert.ToInt32(page) * RecordCount).ToString(),
                        Ft_WhereClause = strWhereClauseMst_NNTChil,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };

                    var objRT_Mst_NNTCurChil = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNTChil, addressAPIs);


                    if (objRT_Mst_NNTCurChil != null && objRT_Mst_NNTCurChil.Lst_Mst_NNT != null && objRT_Mst_NNTCurChil.Lst_Mst_NNT.Count > 0)
                    {
                        objRT_Mst_NNTCur.Lst_Mst_NNT.AddRange(objRT_Mst_NNTCurChil.Lst_Mst_NNT);
                    }
                }
                if (objRT_Mst_NNTCur != null && objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_NNTCur.Lst_Mst_NNT.Count);
                    pageInfo.DataList.AddRange(objRT_Mst_NNTCur.Lst_Mst_NNT);
                    pageCount = (itemCount % RecordCount == 0) ? itemCount / RecordCount : itemCount / RecordCount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }

            ViewBag.MST = mst;
            ViewBag.NNTFullName = nntfullname;
            ViewBag.ContactEmail = contactemail;
            ViewBag.FlagActive = flagactive;

            pageInfo.PageSize = RecordCount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * RecordCount).ToString();

            return View(pageInfo);
        }



        #region["Tạo mới khách hàng (Người nộp thuế)"]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            ViewBag.MSTPARENT = waMST;
            var networkid = System.Web.HttpContext.Current.Session["networkid"];
            ViewBag.NetworkID = networkid;


            #region["Loại khách hàng"]
            var objMst_NNTType = new Mst_NNTType()
            {
                FlagActive = FlagActive,
            };
            var listMst_NNTType = new List<Mst_NNTType>();
            var strWhereClauseMst_NNTType = strWhereClause_Mst_NNTType(objMst_NNTType);

            var objRQ_Mst_NNTType = new RQ_Mst_NNTType()
            {
                Rt_Cols_Mst_NNTType = "*",
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_NNTType
            };
            var objRT_Mst_NNTType = Mst_NNTTypeService.Instance.WA_Mst_NNTType_Get(objRQ_Mst_NNTType, addressAPIs);
            if (objRT_Mst_NNTType.Lst_Mst_NNTType != null && objRT_Mst_NNTType.Lst_Mst_NNTType.Count > 0)
            {
                listMst_NNTType.AddRange(objRT_Mst_NNTType.Lst_Mst_NNTType);
            }

            ViewBag.ListMst_NNTType = listMst_NNTType;
            #endregion

            #region["Đại lý"]
            var objMst_Dealer = new Mst_Dealer()
            {
                FlagActive = FlagActive,
            };
            var listMst_Dealer = new List<Mst_Dealer>();
            var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(objMst_Dealer);

            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Dealer,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                //UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
            }

            ViewBag.ListMst_Dealer = listMst_Dealer;
            #endregion

            #region["Tổ chức cha"]
            var objMst_NNT = new Mst_NNT()
            {
                MST = waMST,
                FlagActive = FlagActive,
            };
            var listMst_NNT = new List<Mst_NNT>();
            var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);
            listMst_NNT.AddRange(List_Mst_NNT(strWhereClauseMst_NNT));

            if (listMst_NNT != null && listMst_NNT.Count > 0)
            {
                ViewBag.DLCode = listMst_NNT[0].DLCode;
            }
            #endregion

            #region["Tỉnh/Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = FlagActive,
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

            var objRQ_Mst_Province = new RQ_Mst_Province()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Province,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, addressAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }

            ViewBag.ListMst_Province = listMst_Province;
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
                // RQ_iNOS_Mst_BizType
                Rt_Cols_iNOS_Mst_BizType = "*",
                iNOS_Mst_BizType = null,
            };
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressAPIs);
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
                // RQ_iNOS_Mst_BizField
                Rt_Cols_iNOS_Mst_BizField = "*",
                iNOS_Mst_BizField = null,
            };
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressAPIs);

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
                // RQ_iNOS_Mst_BizSize
                Rt_Cols_iNOS_Mst_BizSize = "*",
                iNOS_Mst_BizSize = null,
            };
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressAPIs);
            if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
            {
                listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
            }

            ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
            #endregion

            #region["GovIDType"]
            var objMst_GovIDType = new Mst_GovIDType()
            {
                FlagActive = "1",
            };
            var listMst_GovIDType = new List<Mst_GovIDType>();
            var strWhereClauseMst_GovIDType = strWhereClause_Mst_GovIDType(objMst_GovIDType);

            var objRQ_Mst_GovIDType = new RQ_Mst_GovIDType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_GovIDType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                // RQ_Mst_GovIDType
                Rt_Cols_Mst_GovIDType = "*",
                Mst_GovIDType = null,
            };
            var objRT_Mst_GovIDType = Mst_GovIDTypeService.Instance.WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType, addressAPIs);
            if (objRT_Mst_GovIDType.Lst_Mst_GovIDType != null && objRT_Mst_GovIDType.Lst_Mst_GovIDType.Count > 0)
            {
                listMst_GovIDType.AddRange(objRT_Mst_GovIDType.Lst_Mst_GovIDType);
            }

            ViewBag.ListMst_GovIDType = listMst_GovIDType;
            #endregion

            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            #region ["Gen MST"]
            var mstnnt = "";
            var objSeq = new Seq_Common { SequenceType = "", Param_Postfix = "", Param_Prefix = "" };
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
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                Seq_Common = objSeq,
            };
            var seq_common = Seq_CommonService.Instance.WA_RptSv_Seq_MST_Get(objRQ_Seq_Common, addressReportAPIs);
            if (seq_common != null && seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null)
            {
                mstnnt = seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            }
            ViewBag.MST = mstnnt;
            #endregion

            return View();
        }

        #region[Tạo mới sau khi đăng nhập]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var resultEntry = new JsonResultEntry() { Success = false };
            resultEntry.RedirectUrl = null;
            try
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                // Thêm 1 số trường
                objMst_NNTInput.TCTStatus = "1";
                //
                var objRQ_Mst_NNT = new RQ_Mst_NNT
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Mst_NNT = objMst_NNTInput
                };
                Mst_NNTService.Instance.WA_Mst_NNT_CreateForNetwork(objRQ_Mst_NNT, addressAPIs);

                try
                {
                    #region["Tạo org bên biz"]
                    //#region["Tạo Org INOS và gán người đăng nhập làm ADMIN"]   
                    ////inos.common.Paths.SetInosServerBaseAddress(ServiceAddress.BaseINOSAddress);
                    //var objOrgService = new inos.common.Service.OrgService(null);
                    //var objAccountService = new inos.common.Service.AccountService(null);
                    //objOrgService.AccessToken = userState.TokenID;

                    //InosUser dummy = new InosUser();
                    //dummy.Email = userState.SysUser.UserCode == null ? "" : userState.SysUser.UserCode.ToString();
                    //InosUser inosUser = objAccountService.GetUser(dummy);
                    //if (inosUser != null)
                    //{
                    //    #region Tạo Org trên INOS
                    //    inos.common.Model.Org objOrg = new inos.common.Model.Org();
                    //    objOrg.ParentId = long.Parse(userState.SysUser.NetworkID.ToString());
                    //    objOrg.Name = objMst_NNTInput.NNTFullName == null ? "" : objMst_NNTInput.NNTFullName.ToString();

                    //    ////
                    //    objOrg.BizType = new inos.common.Model.BizType();
                    //    objOrg.BizType.Id = objMst_NNTInput.BizType == null ? 0 : Convert.ToInt32(objMst_NNTInput.BizType.ToString());
                    //    objOrg.BizType.Name = "Default";

                    //    ////
                    //    objOrg.BizField = new BizField();
                    //    objOrg.BizField.Id = objMst_NNTInput.BizFieldCode == null ? 0 : Convert.ToInt32(objMst_NNTInput.BizFieldCode.ToString());
                    //    objOrg.BizField.Name = "Default";

                    //    ////
                    //    object objOrgSize = objMst_NNTInput.BizSizeCode == null ? 0 : Convert.ToInt32(objMst_NNTInput.BizSizeCode.ToString());
                    //    objOrg.OrgSize = new OrgSizes();
                    //    objOrg.OrgSize = (OrgSizes)objOrgSize;
                    //    objOrg.ContactName = objMst_NNTInput.ContactName == null ? "" : objMst_NNTInput.ContactName.ToString();
                    //    objOrg.Email = objMst_NNTInput.ContactEmail == null ? "" : objMst_NNTInput.ContactEmail.ToString();
                    //    objOrg.PhoneNo = objMst_NNTInput.ContactPhone == null ? "" : objMst_NNTInput.ContactPhone.ToString();
                    //    objOrg.Description = "";
                    //    objOrg.Enable = true;

                    //    ////
                    //    objOrg.UserList = new List<OrgUser>();


                    //    #region Anh Tuyến bảo không truyền UserList 2019_10_21
                    //    //OrgUser objOrgUser = new OrgUser();

                    //    ////********* Anh Dũng béo bảo ko cần truyền 2019_10_21
                    //    ////objOrgUser.UserId = userState.SysUser.NetworkID == null ? 0 : long.Parse(userState.SysUser.NetworkID);
                    //    ////objOrgUser.Name = "IDNDEMO";
                    //    ////*********
                    //    //objOrgUser.Email = userState.SysUser.UserCode;
                    //    //objOrgUser.Status = OrgUserStatuses.Active;
                    //    //objOrgUser.Role = OrgUserRoles.Admin;

                    //    //objOrg.UserList.Add(objOrgUser);
                    //    #endregion

                    //    ////
                    //    objOrg.InviteList = null;
                    //    ////
                    //    objOrg.CurrentUserRole = OrgUserRoles.Admin;
                    //    ////
                    //    //object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);
                    //    var ret = objOrgService.CreateOrg(objOrg);
                    //    #endregion

                    //    #region Gán user login thành admin
                    //    OrgInvite orgInvite = new OrgInvite();
                    //    //orgInvite.Email = string.Format("{0}", userState.SysUser.UserCode).Trim();
                    //    // Không cho user đăng nhập làm admin nữa = > email liên hệ 2019_10_21
                    //    orgInvite.Email = objMst_NNTInput.ContactEmail == null ? "" : objMst_NNTInput.ContactEmail.ToString();
                    //    orgInvite.OrgId = Convert.ToInt64(ret.Id);
                    //    var result = objOrgService.AddInvite(orgInvite);
                    //    #endregion
                    //}
                    //#endregion
                    #endregion
                }
                catch (mbiz.core.Exceptions.ServiceException ex)
                {
                    var errcode = ex.ErrorCode;
                    var errDetail = ex.ErrorDetail;
                    var errMessage = ex.ErrorMessage;
                    var innerException = ex.InnerException;
                    var str = errcode + "\n " + errDetail + "\n " + errMessage + "\n " + innerException;
                    resultEntry.SetFailed();

                    if (str.Contains("Email existed"))
                    {
                        resultEntry.AddDetail("Tài khoản đã tồn tại, vui lòng nhập tài khoản/email khác.");
                    }
                    else
                    {
                        resultEntry.AddDetail(str);
                    }
                    return Json(resultEntry);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới đơn vị thành viên thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string model, string grecaptcharesponse)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            resultEntry.RedirectUrl = null;
            try
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                // Thêm 1 số trường
                objMst_NNTInput.TCTStatus = "1";
                //
                var objRQ_Mst_NNT = new RQ_Mst_NNT
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Mst_NNT = objMst_NNTInput
                };
                Mst_NNTService.Instance.WA_Mst_NNT_CreateNNTAndDepartment(objRQ_Mst_NNT, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới người nộp thuế thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }


        #endregion

        #endregion

        #region["Sửa danh mục người nộp thuế"]
        [HttpGet]
        public ActionResult Update(string mst)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkid = System.Web.HttpContext.Current.Session["networkid"];
            ViewBag.NetworkID = networkid;
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

            #region["Đại lý"]
            var objMst_Dealer = new Mst_Dealer()
            {
                FlagActive = FlagActive,
            };
            var listMst_Dealer = new List<Mst_Dealer>();
            var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(objMst_Dealer);
            listMst_Dealer.AddRange(List_Mst_Dealer(strWhereClauseMst_Dealer));
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
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressAPIs);
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
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressAPIs);

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
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressAPIs);
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
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

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    #region["Quận/ Huyện"]
                    var objMst_District = new Mst_District()
                    {
                        FlagActive = FlagActive,
                        ProvinceCode = objRT_Mst_NNTCur.Lst_Mst_NNT[0].ProvinceCode
                    };
                    var listMst_District = new List<Mst_District>();
                    var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                    listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                    ViewBag.ListMst_District = listMst_District;
                    #endregion

                    #region["Mã chi cục thuế"]
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        FlagActive = FlagActive,
                        DistrictCode = objRT_Mst_NNTCur.Lst_Mst_NNT[0].DistrictCode
                    };
                    var listMst_GovTaxID = new List<Mst_GovTaxID>();
                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
                    listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
                    ViewBag.ListMst_GovTaxID = listMst_GovTaxID;
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
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
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

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
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
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].DealerType = objMst_NNTInput.DealerType;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.MST);
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
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.DealerType);

                    objRQ_Mst_NNT.Ft_WhereClause = null;
                    objRQ_Mst_NNT.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_NNT.Rt_Cols_Mst_NNT = null;
                    objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                    Mst_NNTService.Instance.WA_Mst_NNT_Update(objRQ_Mst_NNT, addressAPIs);


                    // Cập nhật lại thông tin NNT userstate                   
                    userState.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];
                    //

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

        #endregion

        #region Create XML file _Cập nhật thông tin người nộp thuế
        [HttpPost]
        public ActionResult GetContentXML(string model)
        {
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                string strXML = CreateXML_UpdateNNT(objMst_NNTInput);
                var content = CUtils.Base64_Encode(strXML);
                var data = Uri.EscapeDataString(content);
                return Json(new { Success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, data = "", Detail = "Dữ liệu không hợp lệ." }, JsonRequestBehavior.AllowGet);
        }

        private string CreateXML_UpdateNNT(Mst_NNT obj)
        {
            #region["Mã chi cục thuế"]
            var objMst_GovTaxID = new Mst_GovTaxID()
            {
                FlagActive = FlagActive,
                GovTaxID = this.UserState.Mst_NNT.GovTaxID
            };
            var listMst_GovTaxID = new List<Mst_GovTaxID>();
            var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
            listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
            Mst_GovTaxID mstGovTax = new Mst_GovTaxID();
            if (listMst_GovTaxID != null) if (listMst_GovTaxID.Count > 0) mstGovTax = listMst_GovTaxID[0];
            #endregion
            string dataXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            dataXML += "<DKyThueDTu xmlns=\"http://kekhaithue.gdt.gov.vn/HSoDKy\"><DKyThue id=\"_NODE_TO_SIGN\">";

            dataXML += "<TTinChung>";
            dataXML += "<CQT>";
            dataXML += "<maCQT>" + obj.GovTaxID;
            dataXML += "</maCQT>";
            dataXML += "<tenCQT>" + mstGovTax.GovTaxName;
            dataXML += "</tenCQT>";
            dataXML += "<DVu>";
            dataXML += "<maDVu>0019";
            dataXML += "</maDVu>";
            dataXML += "<tenDVu>idocNet";
            dataXML += "</tenDVu>";
            dataXML += "<soGPhepKDoanh>" + obj.BusinessRegNo;
            dataXML += "</soGPhepKDoanh>";
            dataXML += "</DVu>";
            dataXML += "</CQT>";

            dataXML += "<TTinDKyThue>";
            dataXML += "<maDKy>217";
            dataXML += "</maDKy>";
            dataXML += "<mauDKy>02-DK_T-VAN";
            dataXML += "</mauDKy>";
            dataXML += "<tenDKy>Đăng ký thay đổi chứng thư số";
            dataXML += "</tenDKy>";
            dataXML += "<pBanDKy>2.0.9";
            dataXML += "</pBanDKy>";
            //dataXML += "<ngayDKy>" + obj.CreatedDate;
            //dataXML += "</ngayDKy>";
            dataXML += "<tIN>" + obj.MST;
            dataXML += "</tIN>";
            //dataXML += "<tenNNT>" + obj.FullName;
            //dataXML += "</tenNNT>";
            dataXML += "</TTinDKyThue>";
            dataXML += "</TTinChung>";

            dataXML += "<NDungDKy>";
            dataXML += "<diaDiemTB>";
            dataXML += "</diaDiemTB>";
            dataXML += "<issuer>" + obj.MSTParent;
            //dataXML += "</issuer>";
            //dataXML += "<subject>" + obj.FullName;
            //dataXML += "</subject>";
            dataXML += "<serial>" + obj.CANumber;
            dataXML += "</serial>";
            dataXML += "<email>" + obj.ContactEmail;
            dataXML += "</email>";
            //dataXML += "<fromDateCA>" + obj.CAStartEff;
            //dataXML += "</fromDateCA>";
            //dataXML += "<toDateCA>" + obj.CAEndEff;
            //dataXML += "</toDateCA>";
            dataXML += "<tenToChuc>" + obj.MSTParent;
            dataXML += "</tenToChuc>";
            dataXML += "</NDungDKy>";
            dataXML += "</DKyThue></DKyThueDTu>";
            return dataXML;
        }



        #endregion

        #region["Chi tiết - Xóa danh mục người nộp thuế"]
        [HttpGet]
        public ActionResult Detail(string mst)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

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
            listMst_Dealer.AddRange(List_Mst_Dealer(strWhereClauseMst_Dealer));
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
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressAPIs);
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
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressAPIs);

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
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressAPIs);
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
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

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    #region["Mã chi cục thuế"]
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        FlagActive = FlagActive,
                    };
                    var listMst_GovTaxID = new List<Mst_GovTaxID>();
                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
                    listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
                    ViewBag.ListMst_GovTaxID = listMst_GovTaxID;
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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
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

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                    Mst_NNTService.Instance.WA_Mst_NNT_Delete(objRQ_Mst_NNT, addressAPIs);

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
        #endregion

        #region[""]
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

        private string strWhereClause_Mst_NNTSearch(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
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
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactPhone))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactPhone, "%" + CUtils.StrValue(data.ContactPhone) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNTCurChil(Mst_NNT data)
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
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.DistrictCode, CUtils.StrValue(data.DistrictCode), "=");
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
        private string strWhereClause_Mst_Dealer(Mst_Dealer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
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
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
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

        #endregion
    }
}