using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    //public class InvoiceTempGroupController : BaseController
    //{
    //    // GET: InvoiceTempGroup
    //    public ActionResult Index()
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        //var waMST = userState.RptSv_Sys_User.MST;

    //        var lstInvoiceTempGroup = new List<Invoice_TempGroup>();
    //        var rq = new RQ_Invoice_TempGroup()
    //        {
    //            Rt_Cols_Invoice_TempGroup = "*",
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = ""
    //        };
    //        var rt = Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Get(rq, UserState.AddressAPIs);
    //        if (rt != null) if (rt.Lst_Invoice_TempGroup != null) lstInvoiceTempGroup = rt.Lst_Invoice_TempGroup.OrderByDescending(x => x.FlagActive).ToList();
    //        return View(lstInvoiceTempGroup);
    //    }

    //    [HttpGet]
    //    public ActionResult Create()
    //    {
    //        ViewBag.mstNNT = GetMstNNT();
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var networkid = System.Web.HttpContext.Current.Session["networkid"];
    //        // Customfield invoice
    //        List<string> lstInvoiceField = new List<string>() { "NNTFullName" };
    //        Dictionary<string, string> dicCode_NameNNT = new Dictionary<string, string>()
    //        {
    //            {"NNTFullName", "Đơn vị bán hàng" },
    //            {"MST", "Mã số thuế" },
    //            {"NNTFullAdress", "Địa chỉ" },
    //            {"NNTPhone", "Điện thoại" },
    //            {"NNTBankNo", "Số tài khoản" },
    //            {"NNTEmail", "Email" },

    //            {"NNTFax", "Fax" },
    //            {"NNTWebsite", "Websites" }
    //        };

    //        Dictionary<string, string> dicCode_NameCustomer = new Dictionary<string, string>()
    //        {
    //            {"CustomerNNTBuyerName", "Họ tên người mua hàng" },
    //            {"CustomerNNTName", "Tên đơn vị" },
    //            {"CustomerMST", "Mã số thuế" },
    //            {"CustomerNNTAddress", "Địa chỉ" },
    //            {"PaymentMethodCode", "Hình thức thanh toán" },
    //            {"CustomerNNTAccNo", "Số tài khoản" }
    //        };

    //        ViewBag.dicNNT = dicCode_NameNNT;
    //        ViewBag.dicCustomer = dicCode_NameCustomer;
    //        //

    //        // Load Customfield động Invoice               
    //        List<Invoice_CustomField> lstCustomField = new List<Invoice_CustomField>();

    //        var strWhereCustomfield = "Invoice_CustomField.NetworkID = '" + networkid + "' AND Invoice_CustomField.FlagActive = '1'";
    //        var rq = new RQ_Invoice_CustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWhereCustomfield,
    //            Rt_Cols_Invoice_CustomField = "*"
    //        };
    //        var rt = Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Get(rq, userState.AddressAPIs);
    //        if (rt != null) if (rt.Lst_Invoice_CustomField != null)
    //                lstCustomField = rt.Lst_Invoice_CustomField;

    //        ViewBag.lstCustomField = lstCustomField;
    //        //

    //        // Load customfield động invoiceTemp
    //        List<Invoice_DtlCustomField> lstCustomFieldDtl = new List<Invoice_DtlCustomField>();

    //        var strWhereCustomfieldDtl = "Invoice_DtlCustomField.NetworkID = '" + networkid + "' AND Invoice_DtlCustomField.FlagActive = '1'";
    //        var rqDtl = new RQ_Invoice_DtlCustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWhereCustomfieldDtl,
    //            Rt_Cols_Invoice_DtlCustomField = "*"
    //        };
    //        var rtDtl = Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Get(rqDtl, userState.AddressAPIs);
    //        if (rtDtl != null) if (rtDtl.Lst_Invoice_DtlCustomField != null)
    //                lstCustomFieldDtl = rtDtl.Lst_Invoice_DtlCustomField;
    //        ViewBag.lstCustomFieldDtl = lstCustomFieldDtl;
    //        //

    //        return View();
    //    }
    //    [HttpPost]
    //    [ValidateInput(false)]
    //    public ActionResult Create(string model, string lst_Invoice_TempGroupField)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var data = new Invoice_TempGroup();
    //        var lstInvoiceTempGroupField = new List<Invoice_TempGroupField>();

    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            if (model != null)
    //            {
    //                if (lst_Invoice_TempGroupField != null)
    //                {
    //                    lstInvoiceTempGroupField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_TempGroupField>>(lst_Invoice_TempGroupField);
    //                }
    //                data = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice_TempGroup>(model);
    //                var rq = new RQ_Invoice_TempGroup()
    //                {
    //                    Tid = GetNextTId(),
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    Invoice_TempGroup = data,
    //                    Lst_Invoice_TempGroupField = lstInvoiceTempGroupField
    //                };
    //                Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Create(rq, userState.AddressAPIs);
    //                resultEntry.Success = true;
    //                resultEntry.AddMessage("Tạo mới thành công!");
    //                resultEntry.RedirectUrl = Url.Action("Index", "InvoiceTempGroup");
    //                return Json(resultEntry);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.AddMessage(ex.StackTrace.ToString());
    //            resultEntry.Success = false;
    //            resultEntry.SetFailed().AddException(this,ex);
    //        }
    //        return Json(resultEntry);
    //    }
    //    [HttpGet]
    //    public ActionResult Update(string InvoiceTGroupCode)
    //    {
    //        ViewBag.mstNNT = GetMstNNT();
    //        var Invoice_TempGroup = new Invoice_TempGroup();
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var networkid = System.Web.HttpContext.Current.Session["networkid"];
    //        // Customfield invoice
    //        List<string> lstInvoiceField = new List<string>() { "NNTFullName" };
    //        Dictionary<string, string> dicCode_NameNNT = new Dictionary<string, string>()
    //        {
    //            {"NNTFullName", "Đơn vị bán hàng" },
    //            {"MST", "Mã số thuế" },
    //            {"NNTFullAdress", "Địa chỉ" },
    //            {"NNTPhone", "Điện thoại" },
    //            {"NNTBankNo", "Số tài khoản" },
    //            {"NNTEmail", "Email" },

    //            {"NNTFax", "Fax" },
    //            {"NNTWebsite", "Websites" }
    //        };

    //        Dictionary<string, string> dicCode_NameCustomer = new Dictionary<string, string>()
    //        {
    //            {"CustomerNNTBuyerName", "Họ tên người mua hàng" },
    //            {"CustomerNNTName", "Tên đơn vị" },
    //            {"CustomerMST", "Mã số thuế" },
    //            {"CustomerNNTAddress", "Địa chỉ" },
    //            {"PaymentMethodCode", "Hình thức thanh toán" },
    //            {"CustomerNNTAccNo", "Số tài khoản" }
    //        };

    //        ViewBag.dicNNT = dicCode_NameNNT;
    //        ViewBag.dicCustomer = dicCode_NameCustomer;
    //        //

    //        // Load Customfield động Invoice               
    //        List<Invoice_CustomField> lstCustomField = new List<Invoice_CustomField>();

    //        var strWhereCustomfield = "Invoice_CustomField.NetworkID = '" + networkid + "' AND Invoice_CustomField.FlagActive = '1'";
    //        var rqInvoice_CustomField = new RQ_Invoice_CustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWhereCustomfield,
    //            Rt_Cols_Invoice_CustomField = "*"
    //        };
    //        var rtInvoice_CustomField = Invoice_CustomFieldService.Instance.WA_Invoice_CustomField_Get(rqInvoice_CustomField, userState.AddressAPIs);
    //        if (rtInvoice_CustomField != null) if (rtInvoice_CustomField.Lst_Invoice_CustomField != null)
    //                lstCustomField = rtInvoice_CustomField.Lst_Invoice_CustomField;

    //        ViewBag.lstCustomField = lstCustomField;
    //        //

    //        // Load customfield động invoiceTemp
    //        List<Invoice_DtlCustomField> lstCustomFieldDtl = new List<Invoice_DtlCustomField>();

    //        var strWhereCustomfieldDtl = "Invoice_DtlCustomField.NetworkID = '" + networkid + "' AND Invoice_DtlCustomField.FlagActive = '1'";
    //        var rqDtl = new RQ_Invoice_DtlCustomField()
    //        {
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = strWhereCustomfieldDtl,
    //            Rt_Cols_Invoice_DtlCustomField = "*"
    //        };
    //        var rtDtl = Invoice_DtlCustomFieldService.Instance.WA_Invoice_DtlCustomField_Get(rqDtl, userState.AddressAPIs);
    //        if (rtDtl != null) if (rtDtl.Lst_Invoice_DtlCustomField != null)
    //                lstCustomFieldDtl = rtDtl.Lst_Invoice_DtlCustomField;
    //        ViewBag.lstCustomFieldDtl = lstCustomFieldDtl;
    //        //

    //        var rq = new RQ_Invoice_TempGroup()
    //        {
    //            Rt_Cols_Invoice_TempGroup = "*",
    //            Rt_Cols_Invoice_TempGroupField = "*",
    //            Tid = GetNextTId(),
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = TableName.Invoice_TempGroup + ".InvoiceTGroupCode = '" + InvoiceTGroupCode + "'"
    //        };
    //        var rt = Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Get(rq, userState.AddressAPIs);
    //        if (rt != null) if (rt.Lst_Invoice_TempGroup != null && rt.Lst_Invoice_TempGroup.Count > 0) Invoice_TempGroup = rt.Lst_Invoice_TempGroup[0];
    //        ViewBag.lstInvoice_TempGroupField = rt.Lst_Invoice_TempGroupField;
    //        ViewBag.host = ServiceAddress.BaseServiceAddress + "api/File/";
    //        return View(Invoice_TempGroup);
    //    }

    //    [HttpPost]
    //    [ValidateInput(false)]
    //    public ActionResult Update(string model, string lst_Invoice_TempGroupField/*, params string[] type*/)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        var lstInvoiceTempGroupField = new List<Invoice_TempGroupField>();
    //        try
    //        {
    //            if (model != null)
    //            {
    //                if (lst_Invoice_TempGroupField != null)
    //                {
    //                    lstInvoiceTempGroupField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice_TempGroupField>>(lst_Invoice_TempGroupField);
    //                }
    //                var strColUpdate = "";
    //                var lst = new List<string>() { "InvoiceTGroupCode", "InvoiceTGroupName", "MST", "InvoiceTGroupBody", "FilePathThumbnail", "Spec_Prd_Type", "FlagActive" };
    //                foreach (var it in lst)
    //                {
    //                    if (strColUpdate == "")
    //                    {
    //                        strColUpdate += TableName.Invoice_TempGroup + "." + it;
    //                    }
    //                    else
    //                    {
    //                        strColUpdate += ", " + TableName.Invoice_TempGroup + "." + it;
    //                    }
    //                }
    //                //var strColUpdate = TableName.Invoice_TempGroup + "." + tblInvoice_TempGroup.InvoiceTGroupCode
    //                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice_TempGroup>(model);
    //                var rq = new RQ_Invoice_TempGroup()
    //                {
    //                    Tid = GetNextTId(),
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    Invoice_TempGroup = data,
    //                    Lst_Invoice_TempGroupField = lstInvoiceTempGroupField,
    //                    Ft_Cols_Upd = strColUpdate
    //                };
    //                Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Update(rq, userState.AddressAPIs);
    //                resultEntry.Success = true;
    //                resultEntry.AddMessage("Cập nhật thành công!");
    //                resultEntry.RedirectUrl = Url.Action("Index", "InvoiceTempGroup");
    //                return Json(resultEntry);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.AddMessage(ex.StackTrace.ToString());
    //            resultEntry.Success = false;
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        return Json(resultEntry);
    //    }

    //    public ActionResult Delete(string model)
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        var data = new Invoice_TempGroup();
    //        var resultEntry = new JsonResultEntry() { Success = false };
    //        try
    //        {
    //            if (model != null)
    //            {
    //                data = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice_TempGroup>(model);
    //                var rq = new RQ_Invoice_TempGroup()
    //                {
    //                    Tid = GetNextTId(),
    //                    WAUserCode = waUserCode,
    //                    WAUserPassword = waUserPassword,
    //                    GwUserCode = GwUserCode,
    //                    GwPassword = GwPassword,
    //                    Invoice_TempGroup = data
    //                };
    //                Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Delete(rq, userState.AddressAPIs);
    //                resultEntry.Success = true;
    //                resultEntry.AddMessage("Xóa thành công!");
    //                resultEntry.RedirectUrl = Url.Action("Index", "InvoiceTempGroup");
    //                return Json(resultEntry);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            resultEntry.AddMessage(ex.StackTrace.ToString());
    //            resultEntry.Success = false;
    //            resultEntry.SetFailed().AddException(this, ex);
    //        }
    //        return Json(resultEntry);
    //    }

    //    #region Common
    //    private List<Mst_NNT> GetMstNNT()
    //    {
    //        var userState = this.UserState;
    //        Session["UserState"] = userState;
    //        var addressAPIs = userState.AddressAPIs;
    //        var waUserCode = userState.RptSv_Sys_User.UserCode;
    //        var waUserPassword = userState.RptSv_Sys_User.UserPassword;
    //        //var waMST = userState.SysUser.MST;

    //        var mstNNT = new List<Mst_NNT>();
    //        var objRQ_Mst_NNT = new RQ_Mst_NNT()
    //        {
    //            // WARQBase
    //            Tid = GetNextTId(),
    //            GwUserCode = GwUserCode,
    //            GwPassword = GwPassword,
    //            FuncType = null,
    //            Ft_RecordStart = Ft_RecordStart,
    //            Ft_RecordCount = Ft_RecordCount,
    //            Ft_WhereClause = "Mst_NNT.FlagActive = 1",
    //            Ft_Cols_Upd = null,
    //            WAUserCode = waUserCode,
    //            WAUserPassword = waUserPassword,
    //            // RQ_Mst_NNT
    //            Rt_Cols_Mst_NNT = "*",
    //            Mst_NNT = null
    //        };

    //        var rt_Mst_NNT = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Get(objRQ_Mst_NNT, UserState.AddressAPIs);
    //        if (rt_Mst_NNT != null) if (rt_Mst_NNT.Lst_Mst_NNT != null) mstNNT = rt_Mst_NNT.Lst_Mst_NNT;
    //        return mstNNT;
    //    }
    //    #endregion
    //}
}