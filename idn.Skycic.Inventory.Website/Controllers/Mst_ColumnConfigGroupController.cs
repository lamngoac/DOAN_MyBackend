using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_ColumnConfigGroupController : AdminBaseController
    {
        // GET: Mst_ColumnConfigGroup
        public ActionResult Index(
            string init = "init",
            int page = 0,
            int recordcount = 10)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_CAU_HINH_HIEN_THI_SO_THAP_PHAN");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var userState = this.UserState;
            ViewBag.UserState = userState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waOrgID = userState.Mst_NNT.OrgID.ToString();

            ViewBag.PageCur = page.ToString();
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_ColumnConfigGroup>(0, recordcount)
            {
                DataList = new List<Mst_ColumnConfigGroup>()
            };
            init = "search";


            if(init != "init")
            {
                #region["Search"]
                var objMst_ColumnConfigGroup = new Mst_ColumnConfigGroup()
                {
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                };
                var strWhereClauseMst_ColumnConfigGroup = strWhereClause_Mst_ColumnConfigGroup(objMst_ColumnConfigGroup);

                var objRQ_Mst_ColumnConfigGroup = new RQ_Mst_ColumnConfigGroup()
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
                    Ft_WhereClause = strWhereClauseMst_ColumnConfigGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ColumnConfigGroup
                    Rt_Cols_Mst_ColumnConfigGroup = "*"
                };


                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_ColumnConfigGroup);
                var objRT_Mst_ColumnConfigGroupCur = Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Get(objRQ_Mst_ColumnConfigGroup, addressAPIs);
                if(objRT_Mst_ColumnConfigGroupCur != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup.Count > 0)
                {
                    pageInfo.DataList = objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup;
                    itemCount = objRT_Mst_ColumnConfigGroupCur.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_ColumnConfigGroupCur.MySummaryTable.MyCount);
                }
                
                #endregion
            }
            pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            ViewBag.Offset = 7;




            return View(pageInfo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult DoSearch(string init = "init",
            int page = 0,
            int recordcount = 10)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waOrgID = userState.Mst_NNT.OrgID.ToString();

            ViewBag.PageCur = page.ToString();
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_ColumnConfigGroup>(0, recordcount)
            {
                DataList = new List<Mst_ColumnConfigGroup>()
            };
            init = "search";
            var resultEntry = new JsonResultEntry() { Success = false };


            try
            {
                if(init != "init")
                {
                    #region["Search"]
                    var objMst_ColumnConfigGroup = new Mst_ColumnConfigGroup()
                    {
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    };

                    var strWhereClauseMst_ColumnConfigGroup = strWhereClause_Mst_ColumnConfigGroup(objMst_ColumnConfigGroup);
                    var objRQ_Mst_ColumnConfigGroup = new RQ_Mst_ColumnConfigGroup()
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
                        Ft_WhereClause = strWhereClauseMst_ColumnConfigGroup,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_ColumnConfigGroup
                        Rt_Cols_Mst_ColumnConfigGroup = "*"
                    };

                    var objRT_Mst_ColumnConfigGroupCur = Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Get(objRQ_Mst_ColumnConfigGroup, addressAPIs);
                    if(objRT_Mst_ColumnConfigGroupCur != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup.Count > 0)
                    {
                        pageInfo.DataList = objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup;
                        itemCount = objRT_Mst_ColumnConfigGroupCur.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_ColumnConfigGroupCur.MySummaryTable.MyCount);
                    }



                    #endregion
                }

                pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                ViewBag.Offset = 7;
                return JsonView("BindDataMst_ColumnConfigGroup", pageInfo);

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region["Sửa nhóm cấu hình"]
        [HttpGet]
        public ActionResult Update(string columnConfigGrpCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_CAU_HINH_HIEN_THI_SO_THAP_PHAN_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var userState = this.UserState;
            ViewBag.UserState = userState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(columnConfigGrpCode))
            {
                var objMst_ColumnConfigGroup = new Mst_ColumnConfigGroup()
                {
                    ColumnConfigGrpCode = columnConfigGrpCode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                };
                var strWhereClauseMst_ColumnConfigGroup = strWhereClause_Mst_ColumnConfigGroup(objMst_ColumnConfigGroup);


                var objRQ_Mst_ColumnConfigGroup = new RQ_Mst_ColumnConfigGroup()
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_ColumnConfigGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ColumnConfigGroup
                    Rt_Cols_Mst_ColumnConfigGroup = "*"
                };

                var objRT_Mst_ColumnConfigGroupCur = Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Get(objRQ_Mst_ColumnConfigGroup, addressAPIs);
                if(objRT_Mst_ColumnConfigGroupCur != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup.Count > 0)
                {
                    return View(objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0]);
                }


            }
            return View(new Mst_ColumnConfigGroup());


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;


            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_ColumnConfigGroupInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_ColumnConfigGroup>(model);

                var objMst_ColumnConfigGroup = new Mst_ColumnConfigGroup()
                {
                    ColumnConfigGrpCode = objMst_ColumnConfigGroupInput.ColumnConfigGrpCode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                };



                var strWhereClauseMst_ColumnConfigGroup = strWhereClause_Mst_ColumnConfigGroup(objMst_ColumnConfigGroup);
                var objRQ_Mst_ColumnConfigGroup = new RQ_Mst_ColumnConfigGroup()
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_ColumnConfigGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ColumnConfigGroup
                    Rt_Cols_Mst_ColumnConfigGroup = "*",
                    Mst_ColumnConfigGroup = null,
                };

                var objRT_Mst_ColumnConfigGroupCur = Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Get(objRQ_Mst_ColumnConfigGroup, addressAPIs);
                if(objRT_Mst_ColumnConfigGroupCur != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup.Count > 0)
                {
                    objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0].ColumnGrpName = objMst_ColumnConfigGroupInput.ColumnGrpName;
                    objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0].ColumnGrpDesc = objMst_ColumnConfigGroupInput.ColumnGrpDesc;
                    objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0].FlagActive = objMst_ColumnConfigGroupInput.FlagActive;
                    objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0].ColumnGrpFormat = objMst_ColumnConfigGroupInput.ColumnGrpFormat;
                    objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0].FlagActive = objMst_ColumnConfigGroupInput.FlagActive;


                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_ColumnConfigGroup = TableName.Mst_ColumnConfigGroup + ".";
                    //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_ColumnConfigGroup, TblMst_ColumnConfigGroup.ColumnGrpName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_ColumnConfigGroup, TblMst_ColumnConfigGroup.ColumnGrpDesc);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_ColumnConfigGroup, TblMst_ColumnConfigGroup.FlagActive);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_ColumnConfigGroup, TblMst_ColumnConfigGroup.ColumnGrpFormat);


                    objRQ_Mst_ColumnConfigGroup.Ft_WhereClause = null;
                    objRQ_Mst_ColumnConfigGroup.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_ColumnConfigGroup.Rt_Cols_Mst_ColumnConfigGroup = null;
                    objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup = objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0];

                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_ColumnConfigGroup);
                    Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Update(objRQ_Mst_ColumnConfigGroup, addressAPIs);

                    var listMst_ColumnConfig = new List<Mst_ColumnConfig>();
                    var strWhereClauseMst_ColumnConfig = strWhereClause_Mst_ColumnConfig_Base(new Mst_ColumnConfig()
                    {
                        FlagActive = Client_Flag.Active,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                    });
                    listMst_ColumnConfig = List_Mst_ColumnConfig(strWhereClauseMst_ColumnConfig);
                    if(listMst_ColumnConfig != null && listMst_ColumnConfig.Count > 0)
                    {
                        System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] = listMst_ColumnConfig;
                        
                    }

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa nhóm cấu hình thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");



                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã nhóm cấu hình '" + objMst_ColumnConfigGroupInput.ColumnConfigGrpCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Nhóm cấu hình trống!");
            }
            return Json(resultEntry);
        }


        #endregion


        #region["Chi tiết cấu hình"]

        [HttpGet]
        public ActionResult Detail(string columnConfigGrpCode)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_CAU_HINH_HIEN_THI_SO_THAP_PHAN_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;


            if (!CUtils.IsNullOrEmpty(columnConfigGrpCode))
            {
                var objMst_ColumnConfigGroup = new Mst_ColumnConfigGroup()
                {
                    ColumnConfigGrpCode = columnConfigGrpCode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                };


                var strWhereClauseMst_ColumnConfigGroup = strWhereClause_Mst_ColumnConfigGroup(objMst_ColumnConfigGroup);


                var objRQ_Mst_ColumnConfigGroup = new RQ_Mst_ColumnConfigGroup()
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
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_ColumnConfigGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ColumnConfigGroup
                    Rt_Cols_Mst_ColumnConfigGroup = "*",
                    Mst_ColumnConfigGroup = null,
                };


                var objRT_Mst_ColumnConfigGroupCur = Mst_ColumnConfigGroupService.Instance.WA_Mst_ColumnConfigGroup_Get(objRQ_Mst_ColumnConfigGroup, addressAPIs);
                if (objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup != null && objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup.Count > 0)
                {
                    return View(objRT_Mst_ColumnConfigGroupCur.Lst_Mst_ColumnConfigGroup[0]);
                }
            }
            return View(new Mst_ColumnConfigGroup());
        }

        #endregion


        #region["strWhereClause"]
        private string strWhereClause_Mst_ColumnConfigGroup(Mst_ColumnConfigGroup data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_ColumnConfigGroup = TableName.Mst_ColumnConfigGroup + ".";
            if (!CUtils.IsNullOrEmpty(data.ColumnConfigGrpCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfigGroup + TblMst_ColumnConfigGroup.ColumnConfigGrpCode, CUtils.StrValue(data.ColumnConfigGrpCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ColumnGrpName))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfigGroup + TblMst_ColumnConfigGroup.ColumnGrpName, "%" + CUtils.StrValue(data.ColumnGrpName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfigGroup + TblMst_ColumnConfigGroup.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfigGroup + TblMst_ColumnConfigGroup.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfigGroup + TblMst_ColumnConfigGroup.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        private string strWhereClause_Mst_ColumnConfig_Base(Mst_ColumnConfig obj)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_ColumnConfig = TableName.Mst_ColumnConfig + ".";
            if (!CUtils.IsNullOrEmpty(obj.TableName))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.TableName, obj.TableName, "=");
            }
            //if (!CUtils.IsNullOrEmpty(obj.NetworkID))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.NetworkID, obj.NetworkID, "=");
            //}
            if (!CUtils.IsNullOrEmpty(obj.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.OrgId, obj.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.FlagActive, obj.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        #endregion
    }
}