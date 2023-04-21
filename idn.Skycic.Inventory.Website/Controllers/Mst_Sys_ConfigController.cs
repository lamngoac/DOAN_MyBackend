using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_Sys_ConfigController : AdminBaseController
    {
        // GET: Mst_Sys_Config
        public ActionResult Index()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_CAU_HINH_HE_THONG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            ViewBag.UserState = this.UserState;


            #region["Cấu hình cờ hiển thị giá"]
            var objMst_Sys_Config = new Mst_Sys_Config();
            var objRQ_Mst_Sys_Config = new RQ_Mst_Sys_Config()
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
                // RQ_Mst_Sys_Config
                Rt_Cols_Mst_Sys_Config = "*",
                Mst_Sys_Config = null,
            };

            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Sys_Config);
            var objRT_Mst_Sys_ConfigCur = Mst_Sys_ConfigService.Instance.WA_Mst_Sys_Config_Get(objRQ_Mst_Sys_Config, UserState.AddressAPIs);

            

            #endregion

            return View(objRT_Mst_Sys_ConfigCur );
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string model)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_CAU_HINH_HE_THONG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            ViewBag.UserState = UserState;
            try
            {
                var objMst_Sys_ConfigInput = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Mst_Sys_Config>(model);

                if(objMst_Sys_ConfigInput.Lst_Mst_Sys_Config != null && objMst_Sys_ConfigInput.Lst_Mst_Sys_Config.Count > 0)
                {
                    foreach(var item in objMst_Sys_ConfigInput.Lst_Mst_Sys_Config)
                    {
                        item.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                    }
                    var objRQ_Mst_Sys_Config = new RQ_Mst_Sys_Config()
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
                        Ft_Cols_Upd = "Mst_Sys_Config.FlagActive",
                        // RQ_Mst_Sys_Config
                        Rt_Cols_Mst_Sys_Config = "",
                        Mst_Sys_Config = null,
                        Lst_Mst_Sys_Config = objMst_Sys_ConfigInput.Lst_Mst_Sys_Config
                    };


                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Sys_Config);
                    Mst_Sys_ConfigService.Instance.WA_Mst_Sys_Config_Update(objRQ_Mst_Sys_Config, UserState.AddressAPIs);

                    #region["Tìm kiếm"]
                    var objRQ_Mst_Sys_Config1 = new RQ_Mst_Sys_Config()
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
                        // RQ_Mst_Sys_Config
                        Rt_Cols_Mst_Sys_Config = "*",
                        Mst_Sys_Config = null,
                    };
                    var objRT_Mst_Sys_ConfigCur = Mst_Sys_ConfigService.Instance.WA_Mst_Sys_Config_Get(objRQ_Mst_Sys_Config1, UserState.AddressAPIs);
                    UserState.RT_Mst_Sys_Config = objRT_Mst_Sys_ConfigCur;
                    #endregion

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Cập nhật cấu hình hệ thống thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index", "Mst_Sys_Config");
                    return Json(resultEntry);
                }


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


    }
}