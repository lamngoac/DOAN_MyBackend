using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    //public class Invoice_CustomFieldController : BaseController
    //{
    //    // GET: Invoice_CustomField
    //    public ActionResult Index(string netWorkID, string orgid, string init = "init")
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        //var addressAPIs = userState.AddressAPIs;
    //        //var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        //var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        //var netWorkID = userState.MstNNT.OrgID;
    //        var waUserCode = System.Configuration.ConfigurationManager.AppSettings["WAUserCode_Network"];
    //        var waUserPassword = System.Configuration.ConfigurationManager.AppSettings["WAUserPassword_Network"];

    //        List<Invoice_CustomField> lstCustomField = new List<Invoice_CustomField>();
    //        List<Invoice_DtlCustomField> lstDtlCustomField = new List<Invoice_DtlCustomField>();
    //        var listMst_NNT = new List<Mst_NNT>();

    //        if (init != "init")
    //        {
    //            if (!CUtils.IsNullOrEmpty(netWorkID))
    //            {
    //                try
    //                {
    //                    var addressAPIs = System.Configuration.ConfigurationManager.AppSettings["BaseNetworkAPIAddress"];
    //                    addressAPIs = addressAPIs.Replace("NETWORKIDIDN", netWorkID);

    //                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
    //                    {
    //                        // WARQBase
    //                        Tid = GetNextTId(),
    //                        GwUserCode = GwUserCode,
    //                        GwPassword = GwPassword,
    //                        FuncType = null,
    //                        Ft_RecordStart = Ft_RecordStart,
    //                        Ft_RecordCount = Ft_RecordCount,
    //                        Ft_WhereClause = null,
    //                        Ft_Cols_Upd = null,
    //                        WAUserCode = waUserCode,
    //                        WAUserPassword = waUserPassword,
    //                        UtcOffset = UserState.UtcOffset,
    //                        // RQ_Mst_GovTaxID
    //                        Rt_Cols_Mst_NNT = "*",
    //                        Mst_NNT = null,
    //                    };
    //                    var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
    //                    if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
    //                    {
    //                        listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
    //                    }

    //                    var strWhere = "Invoice_CustomField.NetworkID = '" + netWorkID + "'" + " and " + "Invoice_CustomField.OrgID = '" + orgid + "'";
    //                    var rq = new RQ_Invoice_CustomField()
    //                    {
    //                        Tid = GetNextTId(),
    //                        WAUserCode = waUserCode,
    //                        WAUserPassword = waUserPassword,
    //                        GwUserCode = GwUserCode,
    //                        GwPassword = GwPassword,
    //                        Ft_RecordStart = Ft_RecordStart,
    //                        Ft_RecordCount = Ft_RecordCount,
    //                        Ft_WhereClause = strWhere,
    //                        Rt_Cols_Invoice_CustomField = "*",
    //                    };
    //                    var rt = Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Get(rq, addressAPIs);
    //                    if (rt != null) if (rt.Lst_Invoice_CustomField != null)
    //                            lstCustomField = rt.Lst_Invoice_CustomField;

    //                    var strWheredtl = "Invoice_DtlCustomField.NetworkID = '" + netWorkID + "'" + " and " + "Invoice_DtlCustomField.OrgID = '" + orgid + "'";
    //                    var rqdtl = new RQ_Invoice_DtlCustomField()
    //                    {
    //                        Tid = GetNextTId(),
    //                        WAUserCode = waUserCode,
    //                        WAUserPassword = waUserPassword,
    //                        GwUserCode = GwUserCode,
    //                        GwPassword = GwPassword,
    //                        Ft_RecordStart = Ft_RecordStart,
    //                        Ft_RecordCount = Ft_RecordCount,
    //                        Ft_WhereClause = strWheredtl,
    //                        Rt_Cols_Invoice_DtlCustomField = "*",
    //                    };
    //                    var rtdtl = Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Get(rqdtl, addressAPIs);
    //                    if (rtdtl != null) if (rtdtl.Lst_Invoice_DtlCustomField != null)
    //                            lstDtlCustomField = rtdtl.Lst_Invoice_DtlCustomField;
    //                }
    //                catch (Exception ex)
    //                {

    //                }
    //            }
    //        }
    //        else
    //        {
    //            init = "search";
    //        }

    //        ViewBag.ListMst_NNT = listMst_NNT;
    //        ViewBag.LstCustomField = lstCustomField;
    //        ViewBag.LstDtlCustomField = lstDtlCustomField;
    //        ViewBag.netWorkID = netWorkID;
    //        ViewBag.orgid = orgid;
    //        return View();
    //    }
    //    [HttpGet]
    //    public ActionResult Create()
    //    {
    //        var userState = this.UserState;
    //        ViewBag.lstNNT = get_MstNNT("");
    //        ViewBag.NetworkID = System.Web.HttpContext.Current.Session["networkid"];
    //        //ViewBag.OrgID = userState.MstNNT.OrgID;           
    //        return View();
    //    }
    //    [HttpPost]

    //    public ActionResult Create(string invoice_CustomField, string invoice_DtlCustomField)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            var lstInvoice_CustomField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_CustomField>>(invoice_CustomField);
    //            var lstInvoice_DtlCustomField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_DtlCustomField>>(invoice_DtlCustomField);

    //            if (lstInvoice_CustomField.Count != 0)
    //            {
    //                // Luu cac truong tuy chinh trong hoa don
    //                var rqInvoice_CustomField = new RQ_Invoice_CustomField()
    //                {
    //                    Tid = GetNextTId(),
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    Lst_Invoice_CustomField = lstInvoice_CustomField
    //                };
    //                Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Create(rqInvoice_CustomField, addressAPIs);
    //            }
    //            //

    //            // Luu cac truong tuy chinh trong danh sach hang hoa
    //            if (lstInvoice_DtlCustomField.Count != 0)
    //            {
    //                var rqInvoiceDtl_CustomField = new RQ_Invoice_DtlCustomField()
    //                {
    //                    Tid = GetNextTId(),
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    Lst_Invoice_DtlCustomField = lstInvoice_DtlCustomField
    //                };
    //                Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Create(rqInvoiceDtl_CustomField, addressAPIs);
    //            }
    //            //
    //            resultEntry.Success = true;
    //            resultEntry.AddMessage("Tạo mới các trường tùy chỉnh thành công.");
    //            resultEntry.RedirectUrl = Url.Action("Index");
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        return Json(resultEntry);
    //    }

    //    [HttpPost]
    //    public ActionResult Update(string invoice_CustomField, string invoice_DtlCustomField)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            var lstInvoice_CustomField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_CustomField>>(invoice_CustomField);
    //            var lstInvoice_DtlCustomField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_DtlCustomField>>(invoice_DtlCustomField);

    //            // Luu cac truong tuy chinh trong hoa don
    //            var rqInvoice_CustomField = new RQ_Invoice_CustomField()
    //            {
    //                Tid = GetNextTId(),
    //                WAUserCode = waUserCode,
    //                WAUserPassword = waUserPassword,
    //                GwUserCode = GwUserCode,
    //                GwPassword = GwPassword,
    //                Lst_Invoice_CustomField = lstInvoice_CustomField,
    //                Rt_Cols_Invoice_CustomField = "Invoice_CustomField.InvoiceCustomFieldName, Invoice_CustomField.DBPhysicalType, Invoice_CustomField.FlagActive"
    //            };
    //            Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Update(rqInvoice_CustomField, addressAPIs);
    //            //

    //            // Luu cac truong tuy chinh trong danh sach hang hoa
    //            var rqInvoiceDtl_CustomField = new RQ_Invoice_DtlCustomField()
    //            {
    //                Tid = GetNextTId(),
    //                WAUserCode = waUserCode,
    //                WAUserPassword = waUserPassword,
    //                GwUserCode = GwUserCode,
    //                GwPassword = GwPassword,
    //                Lst_Invoice_DtlCustomField = lstInvoice_DtlCustomField,
    //                Rt_Cols_Invoice_DtlCustomField = "Invoice_DtlCustomField.InvoiceDtlCustomFieldName, Invoice_DtlCustomField.DBPhysicalType, Invoice_DtlCustomField.FlagActive"
    //            };
    //            Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Update(rqInvoiceDtl_CustomField, addressAPIs);
    //            //

    //            resultEntry.AddMessage("Cập nhật các trường tùy chỉnh thành công.");
    //            resultEntry.RedirectUrl = Url.Action("Index");
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        return Json(resultEntry);
    //    }
    //    public ActionResult Update(string orgID)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        //var netWorkID = userState.MstNNT.OrgID;
    //        List<Invoice_CustomField> lstCustomField = new List<Invoice_CustomField>();
    //        List<Invoice_DtlCustomField> lstDtlCustomField = new List<Invoice_DtlCustomField>();

    //        // Master
    //        var strWhere = "Invoice_CustomField.OrgID = '" + orgID + "'";
    //        var rq = new RQ_Invoice_CustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWhere,
    //            Rt_Cols_Invoice_CustomField = "*"
    //        };
    //        var rt = Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Get(rq, addressAPIs);
    //        if (rt != null) if (rt.Lst_Invoice_CustomField != null)
    //                lstCustomField = rt.Lst_Invoice_CustomField;
    //        ViewBag.lstCustomField = lstCustomField;
    //        //

    //        //Detail
    //        var strWheredtl = "Invoice_DtlCustomField.OrgID = '" + orgID + "'";
    //        var rqdtl = new RQ_Invoice_DtlCustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWheredtl,
    //            Rt_Cols_Invoice_DtlCustomField = "*"
    //        };
    //        var rtDtl = Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Get(rqdtl, addressAPIs);
    //        if (rtDtl != null) if (rtDtl.Lst_Invoice_DtlCustomField != null)
    //                lstDtlCustomField = rtDtl.Lst_Invoice_DtlCustomField;
    //        ViewBag.lstDtlCustomField = lstDtlCustomField;
    //        //
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(string model, string modeldtl, string netWorkID)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        //var addressAPIs = userState.AddressAPIs;
    //        //var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        //var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var waUserCode = System.Configuration.ConfigurationManager.AppSettings["WAUserCode_Network"];
    //        var waUserPassword = System.Configuration.ConfigurationManager.AppSettings["WAUserPassword_Network"];

    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            var addressAPIs = System.Configuration.ConfigurationManager.AppSettings["BaseNetworkAPIAddress"];
    //            addressAPIs = addressAPIs.Replace("NETWORKIDIDN", netWorkID);

    //            var listInvoice_CustomFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_CustomField>>(model);
    //            var listInvoice_DtlCustomFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_DtlCustomField>>(modeldtl);

    //            if (listInvoice_CustomFieldInput != null 
    //                && listInvoice_CustomFieldInput.Count > 0 
    //                && listInvoice_DtlCustomFieldInput != null 
    //                && listInvoice_DtlCustomFieldInput.Count > 0)
    //            {
    //                var strFt_Cols_Upd = "";
    //                var Tbl_Invoice_CustomField = TableName.Invoice_CustomField + ".";
    //                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Invoice_CustomField, TblInvoice_CustomField.InvoiceCustomFieldName);
    //                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Invoice_CustomField, TblInvoice_CustomField.FlagActive);
    //                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Invoice_CustomField, TblInvoice_CustomField.DBPhysicalType);

    //                var objRQ_Invoice_CustomField = new RQ_Invoice_CustomField
    //                {
    //                    FlagIsDelete = null,
    //                    Tid = GetNextTId(),
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    FuncType = null,
    //                    Ft_RecordStart = Ft_RecordStart,
    //                    Ft_RecordCount = Ft_RecordCount,
    //                    Ft_WhereClause = null,
    //                    Ft_Cols_Upd = strFt_Cols_Upd,
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    UtcOffset = userState.UtcOffset,
    //                    // RQ_OS_PrdCenter_Mst_SpecCustomField
    //                    Rt_Cols_Invoice_CustomField = null,
    //                    Lst_Invoice_CustomField = listInvoice_CustomFieldInput,
    //                };
    //                Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Update(objRQ_Invoice_CustomField, addressAPIs);

    //                var strFt_Cols_UpdDtl = "";
    //                var Tbl_Invoice_DtlCustomField = TableName.Invoice_DtlCustomField + ".";
    //                strFt_Cols_UpdDtl += string.Format("{0}{1},", Tbl_Invoice_DtlCustomField, TblInvoice_DtlCustomField.InvoiceDtlCustomFieldName);
    //                strFt_Cols_UpdDtl += string.Format("{0}{1},", Tbl_Invoice_DtlCustomField, TblInvoice_DtlCustomField.FlagActive);
    //                strFt_Cols_UpdDtl += string.Format("{0}{1},", Tbl_Invoice_DtlCustomField, TblInvoice_DtlCustomField.DBPhysicalType);

    //                var objRQ_Invoice_DtlCustomField = new RQ_Invoice_DtlCustomField
    //                {
    //                    FlagIsDelete = null,
    //                    Tid = GetNextTId(),
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    FuncType = null,
    //                    Ft_RecordStart = Ft_RecordStart,
    //                    Ft_RecordCount = Ft_RecordCount,
    //                    Ft_WhereClause = null,
    //                    Ft_Cols_Upd = strFt_Cols_UpdDtl,
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    UtcOffset = userState.UtcOffset,
    //                    // RQ_OS_PrdCenter_Mst_SpecCustomField
    //                    Rt_Cols_Invoice_DtlCustomField = null,
    //                    Lst_Invoice_DtlCustomField = listInvoice_DtlCustomFieldInput,
    //                };
    //                Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Update(objRQ_Invoice_DtlCustomField, addressAPIs);

    //                resultEntry.AddMessage("Cập nhật dữ liệu thành công!");

    //                resultEntry.RedirectUrl = Url.Action("Index");
    //            }
    //            else
    //            {
    //                resultEntry.AddMessage("Dữ liệu trống!");
    //                resultEntry.RedirectUrl = Url.Action("Index");
    //            }


    //            resultEntry.Success = true;

    //            return Json(resultEntry);
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        resultEntry.AddModelState(ViewData.ModelState);
    //        return JsonViewError("Index", null, resultEntry);
    //    }
    //    public ActionResult Delete(string orgID, string invoiceCustomFieldCode)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            var rqInvoice_CustomField = new RQ_Invoice_CustomField()
    //            {
    //                Tid = GetNextTId(),
    //                WAUserCode = waUserCode,
    //                WAUserPassword = waUserPassword,
    //                GwUserCode = GwUserCode,
    //                GwPassword = GwPassword,
    //                Invoice_CustomField = new Invoice_CustomField()
    //                {
    //                    OrgID = orgID,
    //                    InvoiceCustomFieldCode = invoiceCustomFieldCode
    //                }
    //            };
    //            Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Delete(rqInvoice_CustomField, addressAPIs);
    //            resultEntry.Success = true;
    //            resultEntry.AddMessage("Xóa các trường tùy chỉnh thành công.");
    //            resultEntry.RedirectUrl = Url.Action("Index");
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        return Json(resultEntry, JsonRequestBehavior.AllowGet);
    //    }

    //    private List<Mst_NNT> get_MstNNT(string _mst)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        List<Mst_NNT> lstMst_NNT = new List<Mst_NNT>();
    //        //var org = userState.MstNNT.OrgID;
    //        //Mst_NNT objNNT = new Mst_NNT()
    //        //{
    //        //    OrgID = org
    //        //};
    //        var strmst_NNT = ""; // strWhereClause_Mst_NNT(objNNT);
    //        if (_mst != null && _mst != "")
    //        {
    //            if (strmst_NNT != "")
    //            {
    //                strmst_NNT += " AND ";
    //            }
    //            strmst_NNT += "Mst_NNT.MST LIKE '%" + _mst + "%'";
    //        }

    //        var rqNNT = new RQ_Mst_NNT
    //        {
    //            Tid = GetNextTId(),
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = ""/*strmst_NNT*/,
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            UtcOffset = userState.UtcOffset,
    //            // RQ_Mst_NNT
    //            Rt_Cols_Mst_NNT = "*",
    //            Mst_NNT = null,
    //        };
    //        var rtNNT = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(rqNNT, addressAPIs);
    //        if (rtNNT != null) if (rtNNT.Lst_Mst_NNT != null && rtNNT.Lst_Mst_NNT.Count != 0)
    //            {
    //                lstMst_NNT = rtNNT.Lst_Mst_NNT;
    //            }
    //        return lstMst_NNT;
    //    }

    //    private string strWhereClause_Mst_NNT(Mst_NNT data)
    //    {
    //        var strWhereClause = "";
    //        var sbSql = new StringBuilder();
    //        var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
    //        if (!CUtils.IsNullOrEmpty(data.MST))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValue(data.MST), "=");
    //        }
    //        if (!CUtils.IsNullOrEmpty(data.MSTParent))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValue(data.MSTParent), "=");
    //        }
    //        if (!CUtils.IsNullOrEmpty(data.NNTFullName))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValue(data.NNTFullName) + "%", "like");
    //        }
    //        if (!CUtils.IsNullOrEmpty(data.ContactEmail))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValue(data.ContactEmail) + "%", "like");
    //        }
    //        if (!CUtils.IsNullOrEmpty(data.FlagActive))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
    //        }

    //        if (!CUtils.IsNullOrEmpty(data.OrgID))
    //        {
    //            sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValue(data.OrgID), "=");
    //        }
    //        strWhereClause = sbSql.ToString();
    //        return strWhereClause;
    //    }
    //}
}