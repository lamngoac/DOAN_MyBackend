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
    public class OS_PrdCenter_PrdPrdIDCustomFieldController : AdminBaseController
    {
        // GET: OS_PrdCenter_PrdPrdIDCustomField
        public ActionResult Index()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
            var objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
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
                // RQ_OS_PrdCenter_Prd_PrdIDCustomField
                Rt_Cols_Prd_PrdIDCustomField = "*",
                Prd_PrdIDCustomField = null,
            };

            var objRT_OS_PrdCenter_Prd_PrdIDCustomFieldCur = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_OS_PrdCenter_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);

            if (objRT_OS_PrdCenter_Prd_PrdIDCustomFieldCur.Lst_Prd_PrdIDCustomField != null &&
                objRT_OS_PrdCenter_Prd_PrdIDCustomFieldCur.Lst_Prd_PrdIDCustomField.Count > 0)
            {
                listPrd_PrdIDCustomField.AddRange(objRT_OS_PrdCenter_Prd_PrdIDCustomFieldCur.Lst_Prd_PrdIDCustomField);
            }
            return View(listPrd_PrdIDCustomField);
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
                var listOS_PrdCenter_Prd_PrdIDCustomFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductCentrer.Prd_PrdIDCustomField>>(model);
                if (listOS_PrdCenter_Prd_PrdIDCustomFieldInput != null &&
                    listOS_PrdCenter_Prd_PrdIDCustomFieldInput.Count > 0)
                {
                    var strFt_Cols_Upd = "";
                    var Tbl_Prd_PrdIDCustomField = TableName.Prd_PrdIDCustomField + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_PrdIDCustomField, TblPrd_PrdIDCustomField.PrdCustomFieldName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_PrdIDCustomField, TblPrd_PrdIDCustomField.FlagActive);
                    foreach (var item in listOS_PrdCenter_Prd_PrdIDCustomFieldInput)
                    {
                        item.OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID);
                        item.NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID);
                        var objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField
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
                            // RQ_OS_PrdCenter_Prd_PrdIDCustomField
                            Rt_Cols_Prd_PrdIDCustomField = null,
                            Prd_PrdIDCustomField = item
                        };
                        OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Update(objRQ_OS_PrdCenter_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
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