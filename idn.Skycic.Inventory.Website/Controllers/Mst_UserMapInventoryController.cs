using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_UserMapInventoryController : AdminBaseController
    {
        // GET: Mst_UserMapInventory
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = this.UserState;
            #endregion

            #region Lấy thông tin kho
            //ViewBag.lstInventory = Mst_Inventory_Get_Inventory(addressAPIs);
            #endregion

            

            #region Lấy danh sách kho xuất        
            var pageInfo = new PageInfo<Mst_Inventory>(0, recordcount)
            {
                DataList = new List<Mst_Inventory>()
            };
            var itemCount = 0;
            var pageCount = 0;
            var strWhereClauseMstInventory = "Mst_Inventory.FlagActive = 1 and Mst_Inventory.FlagIn_Out = 1";
            #region["Search"]            
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Ft_WhereClause = strWhereClauseMstInventory,
                Ft_Cols_Upd = null,
                // RQ_Sys_Group
                Rt_Cols_Mst_Inventory = "*"                    
            };
            var rjson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Inventory);
            var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_GetForUserMapInv(objRQ_Mst_Inventory, addressAPIs);                
            if (rtInventory.MySummaryTable != null)
            {
                itemCount = Convert.ToInt32(rtInventory.MySummaryTable.MyCount);
            }
            if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null && rtInventory.Lst_Mst_Inventory.Count > 0)
            {
                pageInfo.DataList.AddRange(rtInventory.Lst_Mst_Inventory);
                pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
            }                
            #endregion
            
            #endregion
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }

        #region["02.Phân quyền Menu - Button cho nhóm người dùng"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUserMapInventory(string invcode = "") // Lấy danh sách người dùng
        {         
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region["Danh sách Menu - Button"]
                #region["Danh sách Menu - Button gán vào nhóm"]
                var lstMst_UserMapInventory = new List<Mst_UserMapInventory>();
                var strWhereClauseUserMapInventory = "Mst_UserMapInventory.InvCode = '"+ invcode +"'";
                var objRQ_Mst_UserMapInventory = new RQ_Mst_UserMapInventory()
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
                    Ft_WhereClause = strWhereClauseUserMapInventory,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_Group
                    Rt_Cols_Mst_UserMapInventory = "*"
                };             
                var objRT_Mst_UserMapInventory = Mst_UserMapInventoryService.Instance.WA_Mst_UserMapInventory_Get(objRQ_Mst_UserMapInventory, addressAPIs);                
                if(objRT_Mst_UserMapInventory.Lst_Mst_UserMapInventory != null)
                {
                    lstMst_UserMapInventory = objRT_Mst_UserMapInventory.Lst_Mst_UserMapInventory;
                }
                ViewBag.Lst_Mst_UserMapInventory = lstMst_UserMapInventory;
                #endregion
                #region Thông tin người dùng
                var lstMstUser = new List<Sys_User>();
                var strWhereClause = "";
                var Tbl_Sys_User = TableName.Sys_User + ".";
                strWhereClause = Tbl_Sys_User + "FlagActive = 1";

                var objRQ_Sys_User = new RQ_Sys_User()
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
                    Ft_WhereClause = strWhereClause,
                    Ft_Cols_Upd = null,
                    // RQ_Sys_User
                    Rt_Cols_Sys_User = "*",
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = null,
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);
                var objRT_Sys_UserCur = Sys_UserService.Instance.WA_Sys_User_GetForUserMapInv(objRQ_Sys_User, addressAPIs);
                if (objRT_Sys_UserCur != null && objRT_Sys_UserCur.Lst_Sys_User != null && objRT_Sys_UserCur.Lst_Sys_User.Count > 0)
                {
                    lstMstUser = objRT_Sys_UserCur.Lst_Sys_User;
                }
                ViewBag.lstMstUser = lstMstUser;

                ViewBag.invCode = invcode;
                #endregion
                #endregion

                return JsonView("GetUserMapInventory", lstMstUser);
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysObject", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Mst_UserMapInventory_Save(string lst_Mst_UserMapInventory)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_UserMapInventory = new List<Mst_UserMapInventory>();
                if (lst_Mst_UserMapInventory != null)
                {
                    listMst_UserMapInventory = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_UserMapInventory>>(lst_Mst_UserMapInventory);
                    // Thêm tham số khác
                    foreach (var it in listMst_UserMapInventory)
                    {
                        it.OrgID = orgId.ToString();
                    }
                    //

                    var invCode = "";
                    if(listMst_UserMapInventory.Count > 0)
                    {
                        invCode = listMst_UserMapInventory[0].InvCode;
                    }

                    var objRQMst_UserMapInventory = new RQ_Mst_UserMapInventory()
                    {
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
                        Lst_Mst_UserMapInventory = listMst_UserMapInventory,
                        InvCode = invCode
                    };                    
                    Mst_UserMapInventoryService.Instance.WA_Mst_UserMapInventory_Save(objRQMst_UserMapInventory, addressAPIs);
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Phân quyền kho thành công.");
                    return Json(resultEntry);
                }
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("GetSysObject", null, resultEntry);
        }
        #endregion
    }
}