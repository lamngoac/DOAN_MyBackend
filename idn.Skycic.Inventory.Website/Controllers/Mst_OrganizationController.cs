using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using inos.common.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_OrganizationController : AdminBaseController
    {
        // GET: Mst_Organization
        [HttpGet]
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_CHI_NHANH");
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
            var pageInfo = new PageInfo<Mst_NNT>(0, recordcount)
            {
                DataList = new List<Mst_NNT>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                    Ft_WhereClause = "",
                    Ft_Cols_Upd = null,
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*"
                };

                var objRT_RQ_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_RQ_Mst_NNTCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_RQ_Mst_NNTCur.MySummaryTable.MyCount);
                }
                if (objRT_RQ_Mst_NNTCur != null && objRT_RQ_Mst_NNTCur.Lst_Mst_NNT != null && objRT_RQ_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_RQ_Mst_NNTCur.Lst_Mst_NNT);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }

        #region[Tạo mới chi nhánh]
        [HttpGet]
        public ActionResult Create()
        {
            #region["Tỉnh/ thành phố"]
            var objMst_Province = new Mst_Province
            {
                FlagActive = Client_Flag.Active
            };
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
            var listMst_Province = List_Mst_Province_Async(strWhereClauseMst_Province);
            ViewBag.ListMst_Province = listMst_Province.Result;
            #endregion

            #region["Quận/ huyện"]
            var obj_Mst_District = new Mst_District
            {
                FlagActive = Client_Flag.Active
            };
            var strWhereClauseMst_District = strWhereClause_Mst_District(obj_Mst_District);
            var listMst_District = List_Mst_District_Async(strWhereClauseMst_District);
            ViewBag.ListMst_District = listMst_District.Result;
            #endregion

            #region["Vùng thị trường"]
            var objMst_Area = new Mst_Area
            {
                FlagActive = Client_Flag.Active
            };
            var strWhereClauseMst_Area = strWhereClause_Mst_Area(objMst_Area);
            var listMst_Area = List_Mst_Area_Async("");
            ViewBag.ListMst_Area = listMst_Area.Result;
            #endregion



            #region["Get Org in INOS"]
            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var listOS_Inos_Org = new List<OS_Inos_Org>();
            var objRQ_OS_Inos_Org = new RQ_OS_Inos_Org()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = waUserCode_MstSV,
                WAUserPassword = waUserPassword_MstSV,
                // RQ_OS_Inos_Org
                Rt_Cols_OS_Inos_Org = "*",
            };
            var objRT_OS_Inos_Org = OS_Inos_OrgService.Instance.WA_OS_Inos_Org_GetMyOrgList(objRQ_OS_Inos_Org, addressMasterServerAPIs);
            if (objRT_OS_Inos_Org.Lst_OS_Inos_Org != null && objRT_OS_Inos_Org.Lst_OS_Inos_Org.Count > 0)
            {
                listOS_Inos_Org.AddRange(objRT_OS_Inos_Org.Lst_OS_Inos_Org);
            }
            #endregion

            #region["Get List Org in QContract"]

            var listOS_Inos_OrgCur = new List<OS_Inos_Org>();
            var listOS_Inos_Mst_Org = new List<OS_Inos_Org>();

            var objRQ_Mst_Org = new RQ_Mst_Org()
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
                Ft_WhereClause = null,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Org = "*",
            };
            var objRT_Mst_Org = Mst_OrgService.Instance.WA_Mst_Org_Get(objRQ_Mst_Org, CUtils.StrValue(UserState.AddressAPIs));
            if(objRT_Mst_Org != null && objRT_Mst_Org.Lst_Mst_Org != null && objRT_Mst_Org.Lst_Mst_Org.Count > 0)
            {
                foreach (var item in objRT_Mst_Org.Lst_Mst_Org)
                {
                    var os_Inos_Org = new OS_Inos_Org()
                    {
                        Id = item.OrgID,
                    };
                    listOS_Inos_OrgCur.Add(os_Inos_Org);
                }
            }


            if (listOS_Inos_Org != null && listOS_Inos_Org.Count > 0)
            {
                if (listOS_Inos_OrgCur != null && listOS_Inos_Org.Count > 0)
                {
                    listOS_Inos_Mst_Org = listOS_Inos_Org.Where(p => !listOS_Inos_OrgCur.Any(x => x.Id.ToString() == p.Id.ToString())).ToList();
                }
            }
            #endregion


            ViewBag.ListMst_OrgINOS = listOS_Inos_Mst_Org.Where(x => x.ParentId.ToString().Equals(CUtils.StrValue(UserState.Mst_NNT.NetworkID))).ToList();

            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

            ViewBag.OrgID = orgID;


            var userState = this.UserState;
            Session["UserState"] = userState;
            var dlcode = CUtils.StrValue(userState.Mst_NNT.DLCode);
            ViewBag.DLCode = dlcode;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {

            #region["Get MSTParent "]
            var objMst_NNTSub = new Mst_NNT
            {
                MSTParent = ClientMix.MSTRoot
            };
            var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNTSub);
            var listMst_NNT = List_Mst_NNT_Async(strWhereClauseMst_NNT);
            var mstparent = listMst_NNT.Result[0].MST;
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objMst_NNT = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
            objMst_NNT.MSTParent = mstparent;
            var objRQMst_NNT = new RQ_Mst_NNT()
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
                Ft_WhereClause = null,
                Ft_Cols_Upd = null,
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = null,
                //Rt_Cols_Sys_UserInGroup = null,
                Mst_NNT = objMst_NNT,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQMst_NNT);
            Mst_NNTService.Instance.WA_Mst_NNT_CreateForNetwork(objRQMst_NNT, addressAPIs);

            resultEntry.Success = true;
            resultEntry.AddMessage("Tạo chi nhánh thành công!");
            resultEntry.RedirectUrl = Url.Action("Index");
            return Json(resultEntry);
        }
        #endregion

        #region["LoadOS_Inos_Org"]


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadOS_Inos_Org(long orgId)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objOS_Inos_Org = new inos.common.Model.Org();

                if (!CUtils.IsNullOrEmpty(orgId))
                {
                    string strInosBaseUrl = ConfigurationManager.AppSettings["InosBaseUrl"];

                    inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

                    inos.common.Service.OrgService objOrgService = new OrgService(null);
                    objOrgService.AccessToken = CUtils.StrValue(UserState.AccessToken);
                    objOS_Inos_Org = objOrgService.GetOrg(new inos.common.Model.Org() { Id = orgId });
                    if (objOS_Inos_Org != null)
                    {
                        return Json(new { Success = true, Data = objOS_Inos_Org });
                    }
                }
                resultEntry.Success = false;
                resultEntry.AddMessage("Không có dữ liệu");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("Create", null, resultEntry);
        }

        #endregion


        #region[Cập nhật chi nhánh]
        [HttpGet]
        public ActionResult Update(string mst)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_SUA_CHI_NHANH");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

            ViewBag.OrgID = orgID;

            ViewBag.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
            if (!CUtils.IsNullOrEmpty(mst))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_NNT(mst);

                var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    //Rt_Cols_Sys_UserInGroup = null,
                    Mst_NNT = null,
                };

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    #region["Tỉnh/ thành phố"]
                    var objMst_Province = new Mst_Province
                    {
                        FlagActive = Client_Flag.Active
                    };
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    var listMst_Province = List_Mst_Province_Async(strWhereClauseMst_Province);
                    ViewBag.ListMst_Province = listMst_Province.Result;
                    #endregion

                    #region["Quận/ huyện"]
                    var obj_Mst_District = new Mst_District
                    {
                        FlagActive = Client_Flag.Active
                    };
                    var strWhereClauseMst_District = strWhereClause_Mst_District(obj_Mst_District);
                    var listMst_District = List_Mst_District_Async(strWhereClauseMst_District);
                    ViewBag.ListMst_District = listMst_District.Result;
                    #endregion

                    #region["Vùng thị trường"]
                    var objMst_Area = new Mst_Area
                    {
                        FlagActive = Client_Flag.Active
                    };
                    var strWhereClauseMst_Area = strWhereClause_Mst_Area(objMst_Area);
                    var listMst_Area = List_Mst_Area_Async(strWhereClauseMst_Area);
                    ViewBag.ListMst_Area = listMst_Area.Result;
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
            var objMst_NNT = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);

            if (model != null && !CUtils.IsNullOrEmpty(objMst_NNT.MST))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_NNT(objMst_NNT.MST.ToString());
                var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    Mst_NNT = null
                };
                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].MST = objMst_NNT.MST;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTFullName = objMst_NNT.NNTFullName;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentBy = objMst_NNT.PresentBy;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTPosition = objMst_NNT.NNTPosition;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].ProvinceCode = objMst_NNT.ProvinceCode;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].DistrictCode = objMst_NNT.DistrictCode;
                    //
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactName = objMst_NNT.ContactName;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactPhone = objMst_NNT.ContactPhone;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].ContactEmail = objMst_NNT.ContactEmail;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentIDNo = objMst_NNT.PresentIDNo;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].PresentIDType = objMst_NNT.PresentIDType;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTMobile = objMst_NNT.NNTMobile;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTPhone = objMst_NNT.NNTPhone;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTFax = objMst_NNT.NNTFax;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].Website = objMst_NNT.Website;
                    //
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].AccNo = objMst_NNT.AccNo;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].AccHolder = objMst_NNT.AccHolder;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].BankName = objMst_NNT.BankName;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].CANumber = objMst_NNT.CANumber;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].CAOrg = objMst_NNT.CAOrg;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].FlagActive = objMst_NNT.FlagActive;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].AreaCode = objMst_NNT.AreaCode;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].OrgID_MA = objMst_NNT.OrgID_MA;
                    objRT_Mst_NNTCur.Lst_Mst_NNT[0].NNTAddress = objMst_NNT.NNTAddress;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.MST);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTFullName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentBy);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTPosition);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ProvinceCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.DistrictCode);
                    //
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactPhone);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.ContactEmail);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentIDNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.PresentIDType);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTMobile);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTPhone);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.NNTFax);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.Website);
                    //
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.AccNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.AccHolder);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.BankName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CANumber);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_NNT, TblMst_NNT.CAOrg);
                    strFt_Cols_Upd += string.Format("{0}{1}", Tbl_Mst_NNT, TblMst_NNT.FlagActive);
                    strFt_Cols_Upd += string.Format("{0}{1}", Tbl_Mst_NNT, TblMst_NNT.AreaCode);
                    strFt_Cols_Upd += string.Format("{0}{1}", Tbl_Mst_NNT, TblMst_NNT.NNTAddress);

                    objRQ_Mst_NNT.Ft_WhereClause = null;
                    objRQ_Mst_NNT.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_NNT.Rt_Cols_Mst_NNT = null;
                    objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];

                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_NNT);
                    Mst_NNTService.Instance.WA_Mst_NNT_Update(objRQ_Mst_NNT, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa chi nhánh thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã chi nhánh '" + objMst_NNT.OrgID + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã chi nhánh trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region[chi tiết + xóa chi nhánh]
        [HttpGet]
        public ActionResult Detail(string mst)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_CHI_TIET_CHI_NHANH");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var orgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

            ViewBag.OrgID = orgID;

            ViewBag.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);

            if (!CUtils.IsNullOrEmpty(mst))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_NNT(mst);

                var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    Mst_NNT = null
                };

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    return View(objRT_Mst_NNTCur.Lst_Mst_NNT[0]);
                }
            }
            return View(new Mst_NNT());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string mst = "", string orgid = "")
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
                if (!CUtils.IsNullOrEmpty(mst) && !CUtils.IsNullOrEmpty(orgid))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_Mst_NNT(mst);

                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                        // RQ_Mst_NNT
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null
                    };

                    var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                    {
                        objRQ_Mst_NNT.Ft_WhereClause = null;
                        objRQ_Mst_NNT.Ft_Cols_Upd = null;
                        objRQ_Mst_NNT.Rt_Cols_Mst_NNT = null;
                        objRQ_Mst_NNT.Mst_NNT = objRT_Mst_NNTCur.Lst_Mst_NNT[0];
                        Mst_NNTService.Instance.WA_Mst_NNT_Delete(objRQ_Mst_NNT, addressAPIs);
                        title = "Xóa chi nhánh '" + orgid + "' thành công!";

                        resultEntry.Success = true;
                        resultEntry.AddMessage(title);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        title = "Mã chi nhánh '" + orgid + "' không có trong hệ thống!";
                        resultEntry.AddMessage(title);
                    }
                }
                else
                {
                    title = "Mã chi nhánh trống!";
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

        #region["strWhereClause"]
        private string strWhereClause_Mst_NNT(string mst)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(mst))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, mst, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
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

        private string strWhereClause_Mst_Area(Mst_Area data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Area = TableName.Mst_Area + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + "FlagActive", CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        #endregion

        #region["Import Excel"]
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
                var list = new List<Mst_NNT>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 26)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        foreach (DataRow dr in table.Rows)
                        {
                            if (CUtils.IsNullOrEmpty(dr[TblMst_NNT.OrgID]))
                            {
                                exitsData = "Mã tổ chức không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_NNT.NNTFullName]))
                            {
                                exitsData = "Tên tên tổ chức không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_NNT.NNTFullName]))
                            //{
                            //    exitsData = "Tên viết tắt không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            if (CUtils.IsNullOrEmpty(dr["AreaCode"]))
                            {
                                exitsData = "Vùng thị trường không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var orgIDCur = table.Rows[i][TblMst_NNT.OrgID].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _orgIDCurCur = table.Rows[j][TblMst_NNT.OrgID].ToString().Trim();
                                    if (orgIDCur.Equals(_orgIDCurCur))
                                    {
                                        exitsData = "Mã chi nhánh '" + orgIDCur + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<Mst_NNT>(table);

                    #region["Get MSTParent "]
                    var objMst_NNTSub = new Mst_NNT
                    {
                        MSTParent = ClientMix.MSTRoot
                    };
                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNTSub);
                    var listMst_NNT = List_Mst_NNT_Async(strWhereClauseMst_NNT);
                    var mstparent = listMst_NNT.Result[0].MST;
                    #endregion

                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
                            item.MSTParent = mstparent;
                            var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                // RQ_Mst_NNT
                                Rt_Cols_Mst_NNT = null,
                                Mst_NNT = item
                            };
                            Mst_NNTService.Instance.WA_Mst_NNT_Create(objRQ_Mst_NNT, addressAPIs);
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
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;
                var list = new List<Mst_NNT>();
                var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_NNT
                    Rt_Cols_Mst_NNT = "*",
                    Mst_NNT = null
                };

                var objRT_Mst_NNTCur = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                if (objRT_Mst_NNTCur.Lst_Mst_NNT != null && objRT_Mst_NNTCur.Lst_Mst_NNT.Count > 0)
                {
                    list.AddRange(objRT_Mst_NNTCur.Lst_Mst_NNT);

                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_NNT).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_NNT"));

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_NNT>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_NNT).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_NNT"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
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
                 {"OrgID","Mã tổ chức"},
                 {"NNTFullName","Tên tổ chức"},
                 //{"NNTFullName","Tên viết tắt"},
                 {"DLCode","Mã đại lý"},
                 {"AreaCode","Vùng thị trường"},
                 {"MST","MST"},
                 {"PresentBy","Người đại diện"},
                 {"NNTPosition","Chức vụ"},
                 {"PresentIDNo","Số giấy tờ"},
                 {"PresentIDType","Loại giấy tờ"},
                 {"NNTAddress","Địa chỉ"},
                 {"DM_DistrictName","Quận/Huyện"},
                 {"PM_ProvinceName","Tỉnh/Thành"},
                 {"Website","Website"},
                 //{"ContactEmail","Email"},
                 {"NNTPhone","Điện thoại cố định"},
                 {"NNTMobile","Điện thoại di động"},
                 {"Fax","Fax"},
                 {"ContactName","Người liên hệ"},
                 {"ContactEmail","Email liên hệ"},
                 {"ContactPhone","ĐT liên hệ"},
                 {"AccNo","Số tài khoản"},
                 {"AccHolder","Chủ tài khoản"},
                 {"BankName","Ngân hàng"},
                 {"CANumber","Chứng thư số"},
                 {"CAOrg","Tổ chức cấp CTS"},
                 {"CAEffDTimeUTCStart","Thời gian bắt đầu hiệu lực"},
                 {"CAEffDTimeUTCEnd","Thời gian hết hiệu lực"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"OrgID","Mã tổ chức"},
                 {"NNTFullName","Tên tổ chức"},
                 //{"NNTFullName","Tên viết tắt"},
                 {"DLCode","Mã đại lý"},
                 {"AreaCode","Vùng thị trường"},
                 {"MST","MST"},
                 {"PresentBy","Người đại diện"},
                 {"NNTPosition","Chức vụ"},
                 {"PresentIDNo","Số giấy tờ"},
                 {"PresentIDType","Loại giấy tờ"},
                 {"NNTAddress","Địa chỉ"},
                 {"DistrictCode","Mã Quận/Huyện"},
                 {"ProvinceCode","Mã Tỉnh/Thành"},
                 {"Website","Website"},
                 //{"ContactEmail","Email"},
                 {"NNTPhone","Điện thoại cố định"},
                 {"NNTMobile","Điện thoại di động"},
                 {"Fax","Fax"},
                 {"ContactName","Người liên hệ"},
                 {"ContactEmail","Email liên hệ"},
                 {"ContactPhone","ĐT liên hệ"},
                 {"AccNo","Số tài khoản"},
                 {"AccHolder","Chủ tài khoản"},
                 {"BankName","Ngân hàng"},
                 {"CANumber","Chứng thư số"},
                 {"CAOrg","Tổ chức cấp CTS"},
                 {"CAEffDTimeUTCStart","Thời gian bắt đầu hiệu lực"},
                 {"CAEffDTimeUTCEnd","Thời gian hết hiệu lực"},
            };
        }

        #endregion
    }
}