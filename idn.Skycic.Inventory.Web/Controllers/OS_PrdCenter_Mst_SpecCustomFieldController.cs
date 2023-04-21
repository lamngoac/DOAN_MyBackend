using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class OS_PrdCenter_Mst_SpecCustomFieldController : AdminBaseController
    {
        // GET: OS_PrdCenter_Mst_SpecCustomField
        public ActionResult Index()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var listMst_SpecCustomField = new List<ProductCentrer.Mst_SpecCustomField>();
            var objRQ_Mst_SpecCustomField = new ProductCentrer.RQ_Mst_SpecCustomField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = null,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_SpecCustomField
                Rt_Cols_Mst_SpecCustomField = "*",
                Mst_SpecCustomField = null,
            };

            var objRT_Mst_SpecCustomFieldCur = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_Mst_SpecCustomField_Get(objRQ_Mst_SpecCustomField, addressAPIs_ProductCentrer);

            if (objRT_Mst_SpecCustomFieldCur.Lst_Mst_SpecCustomField != null &&
                objRT_Mst_SpecCustomFieldCur.Lst_Mst_SpecCustomField.Count > 0)
            {
                listMst_SpecCustomField.AddRange(objRT_Mst_SpecCustomFieldCur.Lst_Mst_SpecCustomField);
            }
            return View(listMst_SpecCustomField);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_SpecCustomFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductCentrer.Mst_SpecCustomField>>(model);
                if (listMst_SpecCustomFieldInput != null && listMst_SpecCustomFieldInput.Count > 0)
                {
                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_SpecCustomField = TableName.Mst_SpecCustomField + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecCustomField, TblMst_SpecCustomField.SpecCustomFieldName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecCustomField, TblMst_SpecCustomField.FlagActive);
                    foreach (var item in listMst_SpecCustomFieldInput)
                    {
                        item.OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID);
                        item.NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID);
                        var objRQ_Mst_SpecCustomField = new ProductCentrer.RQ_Mst_SpecCustomField
                        {
                            FlagIsDelete = null,
                            Tid = GetNextTId(),
                            GwUserCode = OS_ProductCentrer_GwUserCode,
                            GwPassword = OS_ProductCentrer_GwPassword,
                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = null,
                            Ft_Cols_Upd = strFt_Cols_Upd,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_Mst_SpecCustomField
                            Rt_Cols_Mst_SpecCustomField = null,
                            Mst_SpecCustomField = item
                        };
                        OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_Mst_SpecCustomField_Update(objRQ_Mst_SpecCustomField, addressAPIs_ProductCentrer);
                    }
                    resultEntry.AddMessage("Cập nhật dữ liệu thành công!");

                    resultEntry.RedirectUrl = Url.ActionLocalized("Index");
                }
                else
                {
                    resultEntry.AddMessage("Dữ liệu trống!");
                    resultEntry.RedirectUrl = Url.ActionLocalized("Index");
                }
                
                
                resultEntry.Success = true;
                
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
    }
}