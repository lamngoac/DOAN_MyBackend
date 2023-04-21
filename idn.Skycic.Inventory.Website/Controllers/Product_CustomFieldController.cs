using idn.Skycic.Inventory.ClientService.Services;
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

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Product_CustomFieldController : AdminBaseController
    {
        // GET: Product_CustomField
        public ActionResult Index()
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_MNG_PRODUCT_CUTSOMFIELD");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var resultEntry = new JsonResultEntry() { Success = false };

            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.UserState = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var networkID = userState.SysUser.NetworkID;
            var Orgid = userState.Mst_NNT.OrgID;

            #region["get list trường động"]
            var listProduct_CustomField = new List<Product_CustomField>();

            var strWhereClause_Product_CustomField = strWhereClause_Product_CustomField_Search(new Product_CustomField()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
            });

            var objRQ_Product_CustomField = new RQ_Product_CustomField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                NetworkID = CUtils.StrValue(networkID),
                OrgID = CUtils.StrValue(Orgid),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = "5",
                Ft_WhereClause = strWhereClause_Product_CustomField,
                Ft_Cols_Upd = null,
                // RQ_Mst_SpecCustomField
                Rt_Cols_Product_CustomField = "*",
                Product_CustomField = null,
            };

            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Product_CustomField);
            var objRT_Mst_SpecCustomFieldCur = ProductCustomFieldService.Instance.WA_Product_CustomField_Get(objRQ_Product_CustomField, addressAPIs);
            if (objRT_Mst_SpecCustomFieldCur != null && objRT_Mst_SpecCustomFieldCur.Lst_Product_CustomField != null && objRT_Mst_SpecCustomFieldCur.Lst_Product_CustomField.Count > 0)
            {
                listProduct_CustomField.AddRange(objRT_Mst_SpecCustomFieldCur.Lst_Product_CustomField);
            }

            #endregion


            return View(listProduct_CustomField);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            #region["common"]
            var userState = this.UserState;
            ViewBag.UserState = userState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkID = userState.SysUser.NetworkID;
            var Orgid = userState.Mst_NNT.OrgID;
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listProduct_CustomField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product_CustomField>>(model);
                if (listProduct_CustomField != null && listProduct_CustomField.Count > 0)
                {
                    var objRQ_Product_CustomField = new RQ_Product_CustomField()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = CUtils.StrValue(OS_ProductCentrer_GwUserCode),
                        GwPassword = CUtils.StrValue(OS_ProductCentrer_GwPassword),
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        NetworkID = CUtils.StrValue(networkID),
                        OrgID = CUtils.StrValue(Orgid),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = "Product_CustomField.ProductCustomFieldName, Product_CustomField.DBPhysicalType, Product_CustomField.FlagActive",
                        // RQ_Mst_SpecCustomField
                        Rt_Cols_Product_CustomField = "",
                        Product_CustomField = null,
                        Lst_Product_CustomField = listProduct_CustomField
                    };
                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Product_CustomField);
                    ProductCustomFieldService.Instance.WA_Product_CustomField_Update(objRQ_Product_CustomField, userState.AddressAPIs_Product_Customer_Centrer);
                    resultEntry.AddMessage("Cập nhật dữ liệu thành công!");

                    resultEntry.RedirectUrl = Url.Action("Index", "Product_CustomField");
                }
                else
                {
                    resultEntry.AddMessage("Dữ liệu trống!");
                    resultEntry.RedirectUrl = Url.Action("Index", "Product_CustomField");
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


        #region["whereclause"]
        private string strWhereClause_Product_CustomField_Search(Product_CustomField data)
        {
            var strWhereClause = "";
            var Tbl_Mst_Product = TableName.Product_CustomField + ".";
            var sbSql = new StringBuilder();


            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Product + TblProduct_CustomField.OrgID, data.OrgID, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        #endregion
    }
}