using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
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
    public class Map_UserInNotifyTypeController : AdminBaseController
    {
        // GET: Map_UserInNotifyType
        public async Task<ActionResult> Index()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_MNG_MAP_USERINNOTIFYTYPE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            ViewBag.MST = CUtils.StrValue(userState.Mst_NNT.MST);
            #region["Đơn vị"]
            var listMst_NNT = new List<Mst_NNT>();
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
                Ft_RecordCount = Ft_RecordCount,
                Ft_RecordStart = Ft_RecordStart,
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = "*"
            };

            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if(objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            ViewBag.ListMst_NNT = listMst_NNT;
            #endregion

            var strWhereClauseSys_UserSearch = strWhereClause_Sys_UserSearch(new Sys_User()
            {
                MST = CUtils.StrValue(userState.Mst_NNT.MST),
                UserName = "",
            });
            var objRQ_Map_UserInNotifyType = new RQ_Map_UserInNotifyType()
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
                Ft_WhereClause = strWhereClauseSys_UserSearch,
                Ft_Cols_Upd = null,
                // RQ_Map_UserInNotifyType
                Rt_Cols_Map_UserInNotifyType = "*",
                Rt_Cols_Rt_Cols_Mst_ManageNotify = "*",
                Rt_Cols_Rt_Cols_Mst_NotifyType = "*",
                
            };
            var objRT_Map_UserInNotifyType = Map_UserInNotifyTypeService.Instance.WA_Map_UserInNotifyType_Get(objRQ_Map_UserInNotifyType, addressAPIs);
            return View(objRT_Map_UserInNotifyType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch(string mst, string email)
        {
            ViewBag.UserState = UserState;
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            Session["UserState"] = userState;
            var resultEntry = new JsonResultEntry() { Success = false };

            var strWhereClauseSys_UserSearch = strWhereClause_Sys_UserSearch(new Sys_User() {
                MST = CUtils.StrValue(mst),
                UserName = CUtils.StrValue(email),
            });
            try
            {


                var objRQ_Map_UserInNotifyType = new RQ_Map_UserInNotifyType()
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
                    Ft_WhereClause = strWhereClauseSys_UserSearch,
                    Ft_Cols_Upd = null,
                    // RQ_Map_UserInNotifyType
                    Rt_Cols_Map_UserInNotifyType = "*",
                    Rt_Cols_Rt_Cols_Mst_ManageNotify = "*",
                    Rt_Cols_Rt_Cols_Mst_NotifyType = "*",

                };
                var objRT_Map_UserInNotifyType = Map_UserInNotifyTypeService.Instance.WA_Map_UserInNotifyType_Get(objRQ_Map_UserInNotifyType, addressAPIs);
                return JsonView("BindDataMap_UserInNotifyType", objRT_Map_UserInNotifyType);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_MNG_MAP_USERINNOTIFYTYPE");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMap_UserInNotifyTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Map_UserInNotifyType>>(model);

                var objRQ_Map_UserInNotifyType = new RQ_Map_UserInNotifyType()
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
                    FlagIsDelete = "0",
                    FuncType = null,
                    Ft_RecordStart = null,
                    Ft_RecordCount = null,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Map_UserInNotifyType
                    Lst_Map_UserInNotifyType = listMap_UserInNotifyTypeInput,
                    Rt_Cols_Map_UserInNotifyType = null,
                    Rt_Cols_Rt_Cols_Mst_ManageNotify = null,
                    Rt_Cols_Rt_Cols_Mst_NotifyType = null,

                };

                var objRT_Map_UserInNotifyType = Map_UserInNotifyTypeService.Instance.WA_Map_UserInNotifyType_Save(objRQ_Map_UserInNotifyType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Phân quyền thông báo thành công!");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region[""]
        private string strWhereClause_Sys_UserSearch(Sys_User data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.MST, CUtils.StrValue(data.MST), "=");
            }
            strWhereClause = sbSql.ToString();
            var strWhere = "";
            sbSql = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(data.UserName))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.EMail, "%" + CUtils.StrValue(data.UserName) + "%", "like");
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserName, "%" + CUtils.StrValue(data.UserName) + "%", "like", "or");

                strWhere = sbSql.ToString();
            }
            if (!CUtils.IsNullOrEmpty(strWhere))
            {
                if (!CUtils.IsNullOrEmpty(strWhereClause))
                {
                    strWhere = " ( " + strWhere + " )";
                    strWhereClause += " and " + strWhere;
                }
                else
                {
                    strWhereClause = strWhere;
                }
            }

            return strWhereClause;
        }
        #endregion
    }
}