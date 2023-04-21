using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Extensions;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_AreaController : AdminBaseController
    {
        // GET: Mst_Area

        #region["Quản lý"]
        public async Task<ActionResult> Index(string init = "init", int recordcount = 10, int page = 0)
        {
            ViewBag.PageCur = page.ToString();
            ViewBag.Recordcount = recordcount;
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_VUNG_THI_TRUONG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
     

            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.CurrentUser = userState.SysUser;
            ViewBag.UserState = UserState;


            var pageInfo = new PageInfo<Mst_Area>(0, recordcount)
            {
                DataList = new List<Mst_Area>()
            };
            var itemCount = 0;
            var pageCount = 0;

            if(init != "init")
            {
                #region["Search"]
                var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    //FlagActive = Client_Flag.Active
                });
                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    Ft_WhereClause = strWhereClauseMst_Area,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    //Mst_Area = null,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Area);
                //var list_Mst_AreaAsync = List_Mst_Area_Async(objRQ_Mst_Area);
                //var listMst_Area = new List<Mst_Area>();
                //await Task.WhenAll(list_Mst_AreaAsync);
                //if (list_Mst_AreaAsync != null && list_Mst_AreaAsync.Result != null && list_Mst_AreaAsync.Result.Count > 0)
                //{
                //    listMst_Area.AddRange(list_Mst_AreaAsync.Result);
                //}

                //if (listMst_Area != null && listMst_Area.Count > 0)
                //{
                //    itemCount = Convert.ToInt32(listMst_Area.Count);

                //    pageInfo.DataList.AddRange(listMst_Area);
                //    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                //}


                var objRT_RQ_Mst_AreaCur = Mst_AreaService.Instance.WA_Mst_Area_Get(objRQ_Mst_Area, addressAPIs);
                if (objRT_RQ_Mst_AreaCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_RQ_Mst_AreaCur.MySummaryTable.MyCount);
                }

                if (objRT_RQ_Mst_AreaCur != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_RQ_Mst_AreaCur.Lst_Mst_Area);
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
        #endregion


        #region["Export"]
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
                var list = new List<Mst_Area>();
                var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    //FlagActive = Client_Flag.Active
                });

                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    Ft_WhereClause = strWhereClauseMst_Area,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    Mst_Area = null,
                };

                var objRT_Mst_AreaCur = Mst_AreaService.Instance.WA_Mst_Area_Get(objRQ_Mst_Area, addressAPIs);
                if (objRT_Mst_AreaCur != null && objRT_Mst_AreaCur.Lst_Mst_Area != null && objRT_Mst_AreaCur.Lst_Mst_Area.Count > 0)
                {
                    list.AddRange(objRT_Mst_AreaCur.Lst_Mst_Area);
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Area).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Area"));

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

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"AreaCode","Mã khu vực"},
                 {"AreaName","Tên khu vực"},
                 {"AreaDesc","Mô tả"},
                 {"AreaCodeParent","Mã khu vực cha"},
                 {"FlagActive","Trạng thái"},

            };
        }



        #endregion

        #region["Tạo mới"]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_VUNG_THI_TRUONG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }


            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.CurrentUser = userState.SysUser;
            ViewBag.UserState = UserState;


            #region["Khu vực cha"]
            var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                //FlagActive = Client_Flag.Active
            });
            var objRQ_Mst_Area = new RQ_Mst_Area()
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
                Ft_WhereClause = strWhereClauseMst_Area,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_Area);

            #endregion
            await Task.WhenAll(list_Mst_AreaExt_Async);

            var listMst_AreaExt = new List<Mst_AreaExt>();

            if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null && list_Mst_AreaExt_Async.Result.Count > 0)
            {
                listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
            }

            ViewBag.ListMst_AreaExt = listMst_AreaExt;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_AreaInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Area>(model);
                var areaCode = WA_Seq_GenCommonCode_Get(SeqType.AreaCode);
                objMst_AreaInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_AreaInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objMst_AreaInput.AreaCode = areaCode;
                if (CUtils.IsNullOrEmpty(objMst_AreaInput.AreaCodeParent))
                {
                    objMst_AreaInput.AreaCodeParent = "ALL";
                }
                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = null,
                    Mst_Area = objMst_AreaInput,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Area);
                Mst_AreaService.Instance.WA_Mst_Area_Create(objRQ_Mst_Area, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới khu vực thành công!");
                resultEntry.RedirectUrl = Url.Action("Index", "Mst_Area");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }

        #endregion


        #region["Chi tiết"]
        [HttpGet]
        public async Task<ActionResult> Detail(string areaCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_VUNG_THI_TRUONG_CHI_TIET");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.UserState = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region["Khu vực cha"]
            var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                //FlagActive = Client_Flag.Active
            });
            var objRQ_Mst_Area = new RQ_Mst_Area()
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
                Ft_WhereClause = strWhereClauseMst_Area,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_Area);

            #endregion
            await Task.WhenAll(list_Mst_AreaExt_Async);

            var listMst_AreaExt = new List<Mst_AreaExt>();

            if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null && list_Mst_AreaExt_Async.Result.Count > 0)
            {
                listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
            }

            ViewBag.ListMst_AreaExt = listMst_AreaExt;

            if (!CUtils.IsNullOrEmpty(areaCode))
            {
                var strWhereClauseMst_Area1 = strWhereClause_Mst_Area(new Mst_Area()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    AreaCode = CUtils.StrValue(areaCode)
                    //FlagActive = Client_Flag.Active
                });

                var objRQ_Mst_AreaCur = new RQ_Mst_Area()
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
                    Ft_WhereClause = strWhereClauseMst_Area1,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    Mst_Area = null,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_AreaCur);
                var objRT_RQ_Mst_AreaCur = Mst_AreaService.Instance.WA_Mst_Area_Get(objRQ_Mst_AreaCur, addressAPIs);

                if (objRT_RQ_Mst_AreaCur != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area.Count > 0)
                {
                    return View(objRT_RQ_Mst_AreaCur.Lst_Mst_Area[0]);
                }

            }
            return View(new Mst_Area());
        }
        #endregion


        #region["Cập nhật"]
        [HttpGet]
        public async Task<ActionResult> Update(string areaCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_VUNG_THI_TRUONG_CAP_NHAT");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.UserState = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region["Khu vực cha"]
            var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                //FlagActive = Client_Flag.Active
            });
            var objRQ_Mst_Area = new RQ_Mst_Area()
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
                Ft_WhereClause = strWhereClauseMst_Area,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_Area);

            #endregion
            await Task.WhenAll(list_Mst_AreaExt_Async);

            var listMst_AreaExt = new List<Mst_AreaExt>();

            if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null && list_Mst_AreaExt_Async.Result.Count > 0)
            {
                listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
            }

            ViewBag.ListMst_AreaExt = listMst_AreaExt;

            if (!CUtils.IsNullOrEmpty(areaCode))
            {
                var strWhereClauseMst_Area1 = strWhereClause_Mst_Area(new Mst_Area()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    AreaCode = CUtils.StrValue(areaCode)
                    //FlagActive = Client_Flag.Active
                });

                var objRQ_Mst_AreaCur = new RQ_Mst_Area()
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
                    Ft_WhereClause = strWhereClauseMst_Area1,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    Mst_Area = null,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_AreaCur);
                var objRT_RQ_Mst_AreaCur = Mst_AreaService.Instance.WA_Mst_Area_Get(objRQ_Mst_AreaCur, addressAPIs);

                if (objRT_RQ_Mst_AreaCur != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area != null && objRT_RQ_Mst_AreaCur.Lst_Mst_Area.Count > 0)
                {
                    return View(objRT_RQ_Mst_AreaCur.Lst_Mst_Area[0]);
                }

            }
            return View(new Mst_Area());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_AreaInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Area>(model);
                objMst_AreaInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_AreaInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                var userState = this.UserState;
                var addressAPIs = userState.AddressAPIs;
                if (CUtils.IsNullOrEmpty(objMst_AreaInput.AreaCodeParent))
                {
                    objMst_AreaInput.AreaCodeParent = "ALL";
                }
                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    Ft_Cols_Upd = "Mst_Area.AreaName, Mst_Area.AreaCodeParent, Mst_Area.AreaDesc, Mst_Area.FlagActive",
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = null,
                    Mst_Area = objMst_AreaInput,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Area);
                Mst_AreaService.Instance.WA_Mst_Area_Update(objRQ_Mst_Area, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật mới khu vực thành công!");
                resultEntry.RedirectUrl = Url.Action("Index", "Mst_Area");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion

        #region["Xoá"]
        public ActionResult Delete(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_AreaInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Area>(model);
                objMst_AreaInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                //objMst_AreaInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                var userState = this.UserState;
                var addressAPIs = userState.AddressAPIs;
                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = null,
                    Mst_Area = objMst_AreaInput,
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Area);
                Mst_AreaService.Instance.WA_Mst_Area_Delete(objRQ_Mst_Area, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá khu vực thành công!");
                resultEntry.RedirectUrl = Url.Action("Index", "Mst_Area");
                return Json(resultEntry);
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
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion


        #region["whereclause"]
        private string strWhereClause_Mst_Area(Mst_Area data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Area = TableName.Mst_Area + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.AreaCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.AreaCode, CUtils.StrValue(data.AreaCode), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        #endregion
    }
}