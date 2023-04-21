using idN.TVAN.ClientService.Services;
using idN.TVAN.Common.Models;
using idN.TVAN.Constants;
using idN.TVAN.Utils;
using idN.TVAN.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using idN.TVAN.Common.ModelsUI;
using idN.TVAN.Web.Utils;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using idN.TVAN.Web.State;
using System.Collections.Specialized;

namespace idN.TVAN.Web.Controllers
{
    public class Invoice_ImportExcelController : AdminBaseController
    {
        // GET: Invoice_ImportExcel
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session["List_Invoice_Invoice"] = null;
            System.Web.HttpContext.Current.Session["List_Invoice_InvoiceDtl"] = null;
            System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = null;
            var listMst_InvoiceType = WA_Mst_InvoiceType_Get();
            ViewBag.ListMst_InvoiceType = listMst_InvoiceType;
            //ViewBag.ListInvoice_TempGroup = GetInvoiceTempGroup();
            var listInvoice_TempInvoice = new List<Invoice_TempInvoice>();
            if (listMst_InvoiceType != null && listMst_InvoiceType.Count > 0)
            {
                if (!CUtils.IsNullOrEmpty(listMst_InvoiceType[0].InvoiceType))
                {
                    listInvoice_TempInvoice = Invoice_TempInvoice_Get(listMst_InvoiceType[0].InvoiceType.ToString().Trim());
                }
            }

            ViewBag.ListInvoice_TempInvoice = listInvoice_TempInvoice;
            return View();
        }
        #region["Common"]
        private List<Invoice_TempGroup> GetInvoiceTempGroup()
        {
            var listInvoice_TempGroup = new List<Invoice_TempGroup>();
            var userState = this.UserState;
            var strWhereClauseInvoice_TempGroup = strWhereClause_Invoice_TempGroup(new Invoice_TempGroup() { MST = this.UserState.SysUser.MST, FlagActive = FlagActive });
            var objRQ_Invoice_TempGroup = new RQ_Invoice_TempGroup()
            {
                //Rt_Cols_Invoice_TempGroup = "Invoice_TempGroup.InvoiceTGroupCode, Invoice_TempGroup.InvoiceTGroupName",//"*",
                Rt_Cols_Invoice_TempGroup = "*",
                Tid = GetNextTId(),
                WAUserCode = userState.SysUser.UserCode,
                WAUserPassword = userState.SysUser.UserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseInvoice_TempGroup
            };
            var objRT_Invoice_TempGroup = Invoice_TempGroupService.Instance.WA_Invoice_TempGroup_Get(objRQ_Invoice_TempGroup, userState.AddressAPIs);
            if (objRT_Invoice_TempGroup != null && objRT_Invoice_TempGroup.Lst_Invoice_TempGroup != null &&
                objRT_Invoice_TempGroup.Lst_Invoice_TempGroup.Count > 0)
            {
                listInvoice_TempGroup.AddRange(objRT_Invoice_TempGroup.Lst_Invoice_TempGroup);
            }
            return listInvoice_TempGroup;
        }

        private List<Invoice_TempInvoice> Invoice_TempInvoice_Get(string invoiceType)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            //var resultEntry = new JsonResultEntry() { Success = false };
            var waUserPassword = userState.SysUser.UserPassword;
            var netWorkID = userState.SysUser.NetworkID;
            var listInvoice_TempInvoice = new List<Invoice_TempInvoice>();
            Invoice_TempInvoice objsearch = new Invoice_TempInvoice()
            {
                InvoiceType = invoiceType,
                TInvoiceStatus = "ISSUED",
                MST = userState.MST,
                NetworkID = netWorkID
            };
            var strWhere = strWhereClause_Invoice_TempInvoice(objsearch);
            var objRQ_Invoice_TempInvoice = new RQ_Invoice_TempInvoice()
            {
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Invoice_TempInvoice = "FormNo, EffDateStart, Sign, TInvoiceCode, InvoiceTGroupCode, TInvoiceName, itg_Spec_Prd_Type",
                Ft_WhereClause = strWhere
            };
            var objRT_Invoice_TempInvoice = Invoice_TempInvoiceService.Instance.WA_Invoice_TempInvoice_Get(objRQ_Invoice_TempInvoice, userState.AddressAPIs);
            if (objRT_Invoice_TempInvoice != null && objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice != null)
            {
                listInvoice_TempInvoice = objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice.Where(x => x.StartInvoiceNo != null && x.EndInvoiceNo != null).ToList();
            }
            return listInvoice_TempInvoice;
        }
        #endregion

        #region["Import Excel"]


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file, string flagcheckcustomer, string flagconfirm, string invoicetype, string tinvoicecode, string formno, string sign)
        {
            System.Web.HttpContext.Current.Session["List_Invoice_Invoice"] = null;
            System.Web.HttpContext.Current.Session["List_Invoice_InvoiceDtl"] = null;
            System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = null;
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
            var listInvoice_Invoice = new List<Invoice_Invoice>();
            var listInvoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();

            var listInvoice_Invoice_Sign = new List<Invoice_Invoice>();

            var fileName = "";
            var iTongDongDuLieu = 0;
            var iTongSLHD = 0;
            var iTongSLHDBoQua = 0;
            var iTongSLHDThanhCong = 0;
            var iTongSLHDKoThanhCong = 0;

            #region["Danh sách Columns bắt buộc phải có"]
            var listColumns = new List<string>()
            {
                TblInvoice_ImportExcel.Idx, // Số thứ tự HĐ
                TblInvoice_ImportExcel.InvoiceDateUTC, // Ngày hóa đơn
                TblInvoice_ImportExcel.CustomerNNTCode, // Mã khách hàng
                TblInvoice_ImportExcel.CustomerNNTName, // Tên khách hàng
                TblInvoice_ImportExcel.CustomerNNTAddress, // Địa chỉ
                TblInvoice_ImportExcel.EmailSend, // Email nhận HĐ
                TblInvoice_ImportExcel.SpecCode, // Mã HH,DV
                TblInvoice_ImportExcel.SpecName, // Tên hàng hóa, dịch vụ
                TblInvoice_ImportExcel.VATRate, // Thuế suất (%)
                TblInvoice_ImportExcel.UnitName, // Tên đơn vị
                TblInvoice_ImportExcel.UnitPrice, // Đơn giá
                TblInvoice_ImportExcel.Qty, // Số lượng
                TblInvoice_ImportExcel.ValInvoice, // Thành tiền
            };
            #endregion


            var iMinColumns = listColumns.Count;
            var iMaxColums = GetImportDicColumsTemplateFull().Count;
            var iMaxRows = 1500;

            #region[""]
            var listBoQua = new List<Invoice_Invoice>();
            var listInvoiceUI = new List<InvoiceUI>();
            var listInvoiceDtlUI = new List<InvoiceDtlUI>();
            

            var listMst_VATRate = new List<Mst_VATRate>();
            var listMst_PaymentMethods = new List<Mst_PaymentMethods>();
            var listCustomerNNT = new List<Mst_CustomerNNT>();
            var listSpec = new List<OS_PrdCenter_Mst_Spec>();

            // Master
            string idx, invoiceDateUTC, customerNNTCode, customerNNTName, customerNNTAddress, customerNNTBuyerName, customerMST, customerNNTPhone
                , customerNNTAccNo, emailSend, paymentMethodCode, remark, invoiceCF1, invoiceCF2, invoiceCF3, invoiceCF4, invoiceCF5, invoiceCF6
                , invoiceCF7, invoiceCF8, invoiceCF9, invoiceCF10 = null;
            
            // Detail
            string specCode, specName, vatRateCode, vatRate, unitCode, unitName, unitPrice, qty, valInvoice, valTax, inventoryCode, discountRate, valDiscount
                , remarkDtl, invoiceDCF1, invoiceDCF2, invoiceDCF3, invoiceDCF4, invoiceDCF5 = null;

            double fVATRate, fUnitPrice, fQty, fValInvoice, fValTax, fDiscountRate, fValDiscount = 0.0;
            #endregion

            var exitsData = "";
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                fileName = oldFileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx")
                {
                    exitsData = "File excel import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                else
                {
                    throw new Exception("File excel import không hợp lệ!");
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                var list = new List<Invoice_ImportExcel>();
                var table = ExcelImportNew.Query(filePath, "A2");

                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count < iMinColumns || table.Columns.Count > iMaxColums)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        if (table.Rows.Count <= iMaxRows)
                        {
                            var idxCur = 0;
                            var iRows = 2;
                            var strRows = " - Dòng ";

                            fVATRate = fUnitPrice = fQty = fValInvoice = fValTax = fDiscountRate = fValDiscount = 0.0;

                            string[] columnNames = (from dc in table.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();
                            var listColumnsName = (from dc in table.Columns.Cast<DataColumn>() select dc.ColumnName).ToList();
                            foreach (var columnName in listColumns)
                            {
                                var checkColumnName = listColumnsName.Contains(columnName);
                                if (!checkColumnName)
                                {
                                    exitsData = "File excel import không hợp lệ!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            listMst_VATRate = WA_Mst_VATRate_Get(); // Thuế
                            listMst_PaymentMethods = WA_Mst_PaymentMethods_Get(); // Hình thức TT

                            listInvoice_ImportExcel = DataTableCmUtils.ToListof<Invoice_ImportExcel>(table);

                            #region["Check: flagcheckcustomer - flagconfirm"]
                            var listCustomerNNTCode = new List<string>();
                            var listSpecCode = new List<string>();
                            var strCustomerNNTCode = "";
                            var strSpecCode = "";
                            if (flagcheckcustomer.Equals("1") && flagconfirm.Equals("1"))
                            {
                                foreach (var it in listInvoice_ImportExcel)
                                {
                                    if (!CUtils.IsNullOrEmpty(it.CustomerNNTCode))
                                    {
                                        listCustomerNNTCode.Add(it.CustomerNNTCode.ToString().Trim());
                                    }
                                    if (!CUtils.IsNullOrEmpty(it.SpecCode))
                                    {
                                        listSpecCode.Add(it.SpecCode.ToString().Trim());
                                    }
                                }
                                if (listCustomerNNTCode != null && listCustomerNNTCode.Count > 0)
                                {
                                    strCustomerNNTCode = string.Join(",", listCustomerNNTCode);

                                    var strWhereClauseMst_CustomerNNT = strWhereClause_Mst_CustomerNNT(new Mst_CustomerNNT() { CustomerNNTCode = strCustomerNNTCode });
                                    listCustomerNNT = List_Mst_CustomerNNT(strWhereClauseMst_CustomerNNT);
                                }
                                if (listSpecCode != null && listSpecCode.Count > 0)
                                {
                                    strSpecCode = string.Join(",", listSpecCode);

                                    var strWhereClauseOS_PrdCenter_Mst_Spec = strWhereClause_OS_PrdCenter_Mst_Spec(new OS_PrdCenter_Mst_Spec() { SpecCode = strSpecCode, OrgID = userState.MstNNT.OrgID });
                                    listSpec = List_OS_PrdCenter_Mst_Spec(strWhereClauseOS_PrdCenter_Mst_Spec);
                                }
                            }
                            else if (flagcheckcustomer.Equals("1"))
                            {
                                foreach (var it in listInvoice_ImportExcel)
                                {
                                    if (!CUtils.IsNullOrEmpty(it.CustomerNNTCode))
                                    {
                                        listCustomerNNTCode.Add(it.CustomerNNTCode.ToString().Trim());
                                    }
                                }
                                if (listCustomerNNTCode != null && listCustomerNNTCode.Count > 0)
                                {
                                    strCustomerNNTCode = string.Join(",", listCustomerNNTCode);

                                    var strWhereClauseMst_CustomerNNT = strWhereClause_Mst_CustomerNNT(new Mst_CustomerNNT() { CustomerNNTCode = strCustomerNNTCode });
                                    listCustomerNNT = List_Mst_CustomerNNT(strWhereClauseMst_CustomerNNT);
                                }
                            }
                            else if (flagconfirm.Equals("1"))
                            {
                                foreach (var it in listInvoice_ImportExcel)
                                {
                                    if (!CUtils.IsNullOrEmpty(it.SpecCode))
                                    {
                                        listSpecCode.Add(it.SpecCode.ToString().Trim());
                                    }
                                }
                                if (listSpecCode != null && listSpecCode.Count > 0)
                                {
                                    strSpecCode = string.Join(",", listSpecCode);
                                    var strWhereClauseOS_PrdCenter_Mst_Spec = strWhereClause_OS_PrdCenter_Mst_Spec(new OS_PrdCenter_Mst_Spec() { SpecCode = strSpecCode, OrgID = userState.MstNNT.OrgID });
                                    listSpec = List_OS_PrdCenter_Mst_Spec(strWhereClauseOS_PrdCenter_Mst_Spec);
                                }
                            }
                            #endregion
                            
                            #region[""]

                            foreach (var item in listInvoice_ImportExcel)
                            {
                                iRows = 2;
                                iRows = (iRows + idxCur + 1);
                                strRows = " - Dòng ";
                                strRows += iRows;

                                #region["Check null"]
                                #region["Idx"]
                                idx = CUtils.StrValue(item.Idx);
                                if (CUtils.IsNullOrEmpty(idx))
                                {
                                    exitsData = "Số thứ tự HĐ không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                #endregion
                                #region["InvoiceDateUTC"]
                                invoiceDateUTC = CUtils.StrValue(item.InvoiceDateUTC);
                                if (CUtils.IsNullOrEmpty(invoiceDateUTC))
                                {
                                    exitsData = "Ngày hóa đơn không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    if (!CUtils.IsDateTime(invoiceDateUTC))
                                    {
                                        exitsData = "Ngày hóa đơn không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        invoiceDateUTC = CUtils.FormatDate(invoiceDateUTC, Nonsense.DATE_TIME_FORMAT);
                                    }
                                }
                                #endregion
                                #region["CustomerNNTCode"]
                                customerNNTCode = CUtils.StrValue(item.CustomerNNTCode);
                                
                                #endregion
                                #region["CustomerNNTName - CustomerNNTAddress - CustomerNNTBuyerName - CustomerMST - CustomerNNTPhone - CustomerNNTAccNo - Null"]
                                customerNNTName = CUtils.StrValue(item.CustomerNNTName);
                                customerNNTAddress = CUtils.StrValue(item.CustomerNNTAddress);
                                customerNNTBuyerName = CUtils.StrValue(item.CustomerNNTBuyerName);
                                customerMST = CUtils.StrValue(item.CustomerMST);
                                customerNNTPhone = CUtils.StrValue(item.CustomerNNTPhone);
                                customerNNTAccNo = CUtils.StrValue(item.CustomerNNTAccNo);
                                #endregion
                                #region["EmailSend - Null"]
                                emailSend = CUtils.StrValue(item.EmailSend);
                                #endregion
                                #region["PaymentMethodCode"]
                                paymentMethodCode = CUtils.StrValue(item.PaymentMethodCode);
                                if (CUtils.IsNullOrEmpty(item.PaymentMethodCode))
                                {
                                    paymentMethodCode = Client_PaymentMethodCode.TMCK;
                                    //exitsData = "Hình thức TT không được trống!" + strRows;
                                    //resultEntry.AddMessage(exitsData);
                                    //return Json(resultEntry);
                                }
                                else
                                {
                                    var objMst_PaymentMethodsCur = listMst_PaymentMethods.FirstOrDefault(it =>
                                        (!CUtils.IsNullOrEmpty(it.PaymentMethodCode) && it.PaymentMethodCode.ToString()
                                             .Trim().Equals(paymentMethodCode)));
                                    if (objMst_PaymentMethodsCur == null)
                                    {
                                        exitsData = "Hình thức TT không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["Remark - Null"]
                                remark = CUtils.StrValue(item.Remark);
                                if (!CUtils.IsNullOrEmpty(item.Remark))
                                {
                                    if (remark.Length > Nonsense.RemarkLength)
                                    {
                                        exitsData = "Diễn giải > " + Nonsense.RemarkLength + " ký tự!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["InvoiceCF1 - InvoiceCF2 - InvoiceCF3 - InvoiceCF4 - InvoiceCF5 - InvoiceCF6 - InvoiceCF7 - InvoiceCF8 - InvoiceCF9 - InvoiceCF10 - Null"]
                                invoiceCF1 = CUtils.StrValue(item.InvoiceCF1);
                                invoiceCF2 = CUtils.StrValue(item.InvoiceCF2);
                                invoiceCF3 = CUtils.StrValue(item.InvoiceCF3);
                                invoiceCF4 = CUtils.StrValue(item.InvoiceCF4);
                                invoiceCF5 = CUtils.StrValue(item.InvoiceCF5);
                                invoiceCF6 = CUtils.StrValue(item.InvoiceCF6);
                                invoiceCF7 = CUtils.StrValue(item.InvoiceCF7);
                                invoiceCF8 = CUtils.StrValue(item.InvoiceCF8);
                                invoiceCF9 = CUtils.StrValue(item.InvoiceCF9);
                                invoiceCF10 = CUtils.StrValue(item.InvoiceCF10);
                                #endregion
                                #region["SpecCode"]
                                specCode = CUtils.StrValue(item.SpecCode);
                                #endregion
                                #region["SpecName"]
                                specName = CUtils.StrValue(item.SpecName);
                                #endregion
                                #region["VATRate: vẫn để (*) nếu người dùng ko nhập => set = VATNULL"]
                                vatRate = CUtils.StrValue(item.VATRate);
                                vatRateCode = CUtils.StrValue(item.VATRateCode);
                                if (CUtils.IsNullOrEmpty(vatRate))
                                {
                                    fVATRate = 0;
                                    vatRateCode = Client_Mst_VATRate.VATnull;
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(vatRate))
                                    {
                                        fVATRate = Convert.ToDouble(vatRate);
                                        var checkVATRate = false;
                                        foreach (var it in listMst_VATRate)
                                        {
                                            if (!CUtils.IsNullOrEmpty(it.VATRate) && CUtils.IsNumeric(it.VATRate))
                                            {
                                                var fVATRateCur = Convert.ToDouble(it.VATRate.ToString().Trim());
                                                
                                                if (fVATRateCur == fVATRate)
                                                {
                                                    checkVATRate = true;
                                                    vatRate = it.VATRate.ToString().Trim();
                                                    vatRateCode = it.VATRateCode.ToString().Trim();
                                                    break;
                                                }
                                            }
                                        }

                                        if (!checkVATRate)
                                        {
                                            exitsData = "Thuế suất không hợp lệ!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Thuế suất không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["UnitCode"]
                                unitCode = CUtils.StrValue(item.UnitCode);
                                #endregion
                                #region["UnitName"]
                                unitName = CUtils.StrValue(item.UnitName);
                                #endregion
                                #region["UnitPrice"]
                                unitPrice = CUtils.StrValue(item.UnitPrice);
                                #endregion
                                #region["Qty"]
                                qty = CUtils.StrValue(item.Qty);
                                if (CUtils.IsNullOrEmpty(qty))
                                {
                                    qty = "1";
                                    fQty = 1.0;
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(qty))
                                    {
                                        var arrQty = qty.ToString().Split('.');
                                        if (arrQty != null && arrQty.Length == 2)
                                        {
                                            var phandu = arrQty[1].Length;
                                            if (phandu.ToString().Trim().Length > 2)
                                            {
                                                exitsData = "Số lượng chỉ được nhập làm tròn đến 0.01!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        
                                        fQty = Convert.ToDouble(qty);
                                        //qty = fQty.ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        exitsData = "Số lượng không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["ValInvoice"]
                                valInvoice = CUtils.StrValue(item.ValInvoice);
                                if (CUtils.IsNullOrEmpty(valInvoice))
                                {
                                    fValInvoice = CUtils.LamTronSo((fUnitPrice * fQty), 0);
                                    valInvoice = fValInvoice.ToString(CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(valInvoice))
                                    {
                                        var arrValInvoice = valInvoice.ToString().Split('.');
                                        if (arrValInvoice != null && arrValInvoice.Length == 2)
                                        {
                                            var phandu = arrValInvoice[1].Length;
                                            if (phandu.ToString().Trim().Length > 0)
                                            {
                                                exitsData = "Thành tiền chỉ được nhập làm tròn đến phần nguyên!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                            
                                        }

                                        //fValInvoice = CUtils.LamTronSo((fUnitPrice * fQty / 100), 0);
                                        //valInvoice = fValInvoice.ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        exitsData = "Thành tiền không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["ValTax"]
                                valTax = CUtils.StrValue(item.ValTax);
                                if (CUtils.IsNullOrEmpty(valTax))
                                {
                                    fValTax = CUtils.LamTronSo((fValInvoice * fVATRate / 100), 0);
                                    valTax = fValTax.ToString(CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(valTax))
                                    {
                                        var arrValTax = valTax.ToString().Split('.');
                                        if (arrValTax != null && arrValTax.Length == 2)
                                        {
                                            var phandu = arrValTax[1].Length;
                                            if (phandu.ToString().Trim().Length > 0)
                                            {
                                                exitsData = "Tiền thuế chỉ được nhập làm tròn đến phần nguyên!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        //fValTax = CUtils.LamTronSo((fValInvoice * fVATRate / 100), 0);
                                        //valTax = fValTax.ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        exitsData = "Tiền thuế không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["InventoryCode"]
                                inventoryCode = CUtils.StrValue(item.InventoryCode);
                                #endregion
                                #region["DiscountRate"]
                                discountRate = CUtils.StrValue(item.DiscountRate);
                                if (CUtils.IsNullOrEmpty(discountRate))
                                {
                                    discountRate = "0";
                                    fDiscountRate = 0.0;
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(discountRate))
                                    {
                                        var arrDiscountRate = discountRate.ToString().Split('.');

                                        if (arrDiscountRate != null && arrDiscountRate.Length == 2)
                                        {
                                            var phandu = arrDiscountRate[1].Length;
                                            if (phandu.ToString().Trim().Length > 2)
                                            {
                                                exitsData = "Tỉ lệ chiết khấu chỉ được nhập làm tròn đến 0.01!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                            
                                        }
                                        else
                                        {
                                            fDiscountRate = Convert.ToDouble(discountRate);
                                            //fDiscountRate = CUtils.LamTronSo(fDiscountRate, 2);
                                            if (fDiscountRate >= 0 && fDiscountRate <= 100)
                                            {
                                                discountRate = fDiscountRate.ToString(CultureInfo.InvariantCulture);
                                            }
                                            else
                                            {
                                                exitsData = "Tỉ lệ chiết khấu không hợp lệ!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Tỉ lệ chiết khấu không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["ValDiscount"]
                                valDiscount = CUtils.StrValue(item.ValDiscount);
                                if (CUtils.IsNullOrEmpty(valDiscount))
                                {
                                    valDiscount = "0";
                                    fValDiscount = 0.0;
                                }
                                else
                                {
                                    if (CUtils.IsNumeric(valDiscount))
                                    {
                                        var arrValDiscount = valDiscount.ToString().Split('.');

                                        if (arrValDiscount != null && arrValDiscount.Length == 2)
                                        {
                                            var phandu = arrValDiscount[1].Length;
                                            if (phandu.ToString().Trim().Length > 0)
                                            {
                                                exitsData = "Tiền chiết khấu chỉ được nhập làm tròn đến phần nguyên!" + strRows;
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Tiền chiết khấu không hợp lệ!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["Remark - Null"]
                                remarkDtl = CUtils.StrValue(item.RemarkDtl);
                                if (!CUtils.IsNullOrEmpty(remarkDtl))
                                {
                                    if (remarkDtl.Length > Nonsense.RemarkLength)
                                    {
                                        exitsData = "Diễn giải HH, DV > " + Nonsense.RemarkLength + " ký tự!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion
                                #region["InvoiceDCF1 - InvoiceDCF2 - InvoiceDCF3 - InvoiceDCF4 - InvoiceDCF5 - Null"]
                                invoiceDCF1 = CUtils.StrValue(item.InvoiceDCF1);
                                invoiceDCF2 = CUtils.StrValue(item.InvoiceDCF2);
                                invoiceDCF3 = CUtils.StrValue(item.InvoiceDCF3);
                                invoiceDCF4 = CUtils.StrValue(item.InvoiceDCF4);
                                invoiceDCF5 = CUtils.StrValue(item.InvoiceDCF5);
                                #endregion
                                #endregion
                                
                                #region["Check khách hàng"]
                                if (flagcheckcustomer.Equals("1"))
                                {
                                    if (CUtils.IsNullOrEmpty(customerNNTCode))
                                    {
                                        exitsData = "Mã khách hàng không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        var objMst_CustomerNNTCur = listCustomerNNT.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.CustomerNNTCode) && it.CustomerNNTCode.ToString().Trim().Equals(customerNNTCode)));
                                        if (objMst_CustomerNNTCur != null)
                                        {
                                            customerNNTName = CUtils.StrValue(objMst_CustomerNNTCur.CustomerNNTName);
                                            customerNNTAddress = CUtils.StrValue(objMst_CustomerNNTCur.CustomerNNTAddress);
                                            customerNNTBuyerName = CUtils.StrValue(objMst_CustomerNNTCur.ContactName);
                                            customerMST = CUtils.StrValue(objMst_CustomerNNTCur.CustomerMST);
                                            customerNNTPhone = CUtils.StrValue(objMst_CustomerNNTCur.CustomerNNTPhone);
                                            customerNNTAccNo = CUtils.StrValue(objMst_CustomerNNTCur.AccNo);
                                            emailSend = CUtils.StrValue(objMst_CustomerNNTCur.CustomerNNTEmail);

                                            
                                        }
                                        else
                                        {
                                            exitsData = "Mã khách hàng chưa tồn tại trong hệ thống!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                                else
                                {
                                    if (CUtils.IsNullOrEmpty(customerNNTName))
                                    {
                                        exitsData = "Tên khách hàng không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }

                                    if (CUtils.IsNullOrEmpty(customerNNTAddress))
                                    {
                                        exitsData = "Địa chỉ không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }

                                    if (CUtils.IsNullOrEmpty(emailSend))
                                    {
                                        exitsData = "Email nhận hóa đơn không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        if (!CUtils.IsValidEmail(emailSend))
                                        {
                                            exitsData = "Email nhận HĐ không hợp lệ!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                                #endregion

                                #region["Check hàng hóa"]
                                if (flagconfirm.Equals("1"))
                                {
                                    if (CUtils.IsNullOrEmpty(specCode))
                                    {
                                        exitsData = "Mã hàng hóa, dịch vụ không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        var objMst_SpecCur = listSpec.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.SpecCode) && it.SpecCode.ToString().Trim().Equals(specCode)));

                                        if (objMst_SpecCur != null)
                                        {
                                            specName = CUtils.StrValue(objMst_SpecCur.SpecName);
                                            //unitCode = CUtils.StrValue(objMst_SpecCur.DefaultUnitCode);
                                            unitCode = CUtils.StrValue(objMst_SpecCur.msp_UnitCode);
                                            unitName = CUtils.StrValue(objMst_SpecCur.msp_UnitName);
                                            unitPrice = CUtils.StrValue(objMst_SpecCur.msp_SellPrice);
                                            if (!CUtils.IsNullOrEmpty(unitPrice))
                                            {
                                                fUnitPrice = Convert.ToDouble(unitPrice);
                                            }

                                            fValInvoice = CUtils.LamTronSo((fUnitPrice * fQty), 0);
                                            valInvoice = fValInvoice.ToString(CultureInfo.InvariantCulture);

                                            fValTax = CUtils.LamTronSo((fValInvoice * fVATRate / 100), 0);
                                            valTax = fValTax.ToString(CultureInfo.InvariantCulture);

                                        }
                                        else
                                        {
                                            exitsData = "Mã hàng hóa, dịch vụ chưa tồn tại trong hệ thống!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                                else
                                {
                                    if (CUtils.IsValidEmail(specName))
                                    {
                                        exitsData = "Tên hàng hóa, dịch vụ không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    if (CUtils.IsValidEmail(unitName))
                                    {
                                        exitsData = "Tên đơn vị không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    if (CUtils.IsValidEmail(unitPrice))
                                    {
                                        exitsData = "Đơn giá không được trống!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                #endregion

                                var objInvoice_Invoice_Check = listInvoiceUI.FirstOrDefault(it =>
                                    (!CUtils.IsNullOrEmpty(it.Invoice_Idx) && it.Invoice_Idx.ToString().Trim().Equals(idx)));
                                if (objInvoice_Invoice_Check == null)
                                {
                                    var objInvoice_InvoiceCur = new InvoiceUI()
                                    {
                                        Invoice_Idx = idx,
                                        InvoiceCode = null,
                                        MST = userState.MstNNT.MST.ToString().Trim(),
                                        NetworkID = userState.MstNNT.NetworkID.ToString().Trim(),
                                        RefNo = null,
                                        FormNo = formno, 
                                        Sign = sign, 
                                        SourceInvoiceCode = "INVOICEROOT",
                                        InvoiceAdjType = "NORMAL",
                                        PaymentMethodCode = paymentMethodCode,
                                        InvoiceType2 = null,
                                        InvoiceDateUTC = invoiceDateUTC,
                                        CustomerNNTCode = customerNNTCode,
                                        CustomerNNTName = customerNNTName,
                                        CustomerNNTAddress = customerNNTAddress,
                                        CustomerNNTPhone = customerNNTPhone,
                                        CustomerNNTBankName = null,
                                        CustomerNNTEmail = null,
                                        CustomerNNTAccNo = customerNNTAccNo,
                                        CustomerNNTBuyerName = customerNNTBuyerName,
                                        CustomerMST = customerMST,
                                        TInvoiceCode = tinvoicecode, 
                                        InvoiceNo = null,
                                        EmailSend = emailSend,
                                        InvoiceFileSpec = null,
                                        InvoiceFilePath = null,
                                        InvoicePDFFilePath = null,
                                        TotalValInvoice = null, //
                                        TotalValVAT = null, //
                                        TotalValPmt = null, //
                                        ValGoodsNotTaxable = null, //
                                        ValGoodsNotChargeTax = null, //
                                        ValGoodsVAT5 = null, //
                                        ValVAT5 = null, //
                                        ValGoodsVAT10 = null, //
                                        ValVAT10 = null, //
                                        CurrencyCode = "VND",
                                        CurrencyRate = "1",
                                        NNTFullName = userState.MstNNT.NNTFullName, //
                                        NNTFullAdress = userState.MstNNT.NNTAddress, //
                                        NNTPhone = userState.MstNNT.NNTPhone, //
                                        NNTFax = userState.MstNNT.NNTFax, //
                                        NNTEmail = userState.MstNNT.ContactEmail, //
                                        NNTWebsite = userState.MstNNT.Website, //
                                        NNTAccNo = userState.MstNNT.AccNo, //
                                        NNTBankName = userState.MstNNT.BankName, //
                                        FlagCheckCustomer = flagcheckcustomer, //
                                        FlagConfirm = flagconfirm, //
                                        InvoiceCF1 = invoiceCF1,
                                        InvoiceCF2 = invoiceCF2,
                                        InvoiceCF3 = invoiceCF3,
                                        InvoiceCF4 = invoiceCF4,
                                        InvoiceCF5 = invoiceCF5,
                                        InvoiceCF6 = invoiceCF6,
                                        InvoiceCF7 = invoiceCF7,
                                        InvoiceCF8 = invoiceCF8,
                                        InvoiceCF9 = invoiceCF9,
                                        InvoiceCF10 = invoiceCF10,
                                        Remark = remark,

                                    };

                                    listInvoiceUI.Add(objInvoice_InvoiceCur);

                                }
                                
                                var idxDtl = 0;
                                var listInvoice_InvoiceDtlCur = listInvoiceDtlUI.Where(it =>
                                    !CUtils.IsNullOrEmpty(it.Invoice_Idx) && it.Invoice_Idx.ToString().Trim().Equals(idx)).ToList();
                                if (listInvoice_InvoiceDtlCur != null && listInvoice_InvoiceDtlCur.Count > 0)
                                {
                                    idxDtl = listInvoice_InvoiceDtlCur.Count + 1;
                                }
                                else
                                {
                                    idxDtl = 1;
                                }
                                var objInvoice_InvoiceDtlCur = new InvoiceDtlUI()
                                {
                                    InvoiceCode = null,
                                    Idx = idxDtl,
                                    Invoice_Idx = idx,
                                    NetworkID = userState.MstNNT.NetworkID,
                                    SpecCode = specCode,
                                    SpecName = specName,
                                    VATRateCode = vatRateCode,
                                    VATRate = vatRate,
                                    UnitCode = unitCode,
                                    UnitName = unitName,
                                    UnitPrice = unitPrice,
                                    Qty = qty,
                                    ValInvoice = valInvoice,
                                    ValTax = valTax,
                                    InventoryCode = inventoryCode,
                                    DiscountRate = discountRate,
                                    ValDiscount = valDiscount,
                                    Remark = remarkDtl,
                                    InvoiceDCF1 = invoiceDCF1,
                                    InvoiceDCF2 = invoiceDCF2,
                                    InvoiceDCF3 = invoiceDCF3,
                                    InvoiceDCF4 = invoiceDCF4,
                                    InvoiceDCF5 = invoiceDCF5,
                                };

                                listInvoiceDtlUI.Add(objInvoice_InvoiceDtlCur);

                                #region["Update item trong file excel export"]

                                item.FormNo = formno;
                                item.Sign = sign;

                                item.CustomerNNTName = customerNNTName;
                                item.CustomerNNTAddress = customerNNTAddress;
                                item.CustomerNNTBuyerName = customerNNTBuyerName;
                                item.CustomerMST = customerMST;
                                item.CustomerNNTPhone = customerNNTPhone;
                                item.CustomerNNTAccNo = customerNNTAccNo;
                                item.EmailSend = emailSend;

                                item.SpecName = specName;
                                item.UnitCode = unitCode;
                                item.UnitName = unitName;
                                item.UnitPrice = unitPrice;
                                item.ValInvoice = valInvoice;
                                item.ValTax = valTax;
                                #endregion

                                idxCur++;
                            }

                            // Tinh tong gia tri hoa don

                            foreach (var item in listInvoiceUI)
                            {
                                var invoice_Idx = item.Invoice_Idx;

                                var totalValVAT5 = 0.0; // tien thue 5
                                var totalValVAT10 = 0.0; // tien thue 10

                                var totalValGoodsNotTaxable = 0.0; // tien hang VATNull
                                var totalValGoodsNotChargeTax = 0.0; // tien hang VAT0
                                var totalValGoodsVAT5 = 0.0; // Tổng tiền hàng thuế VAT5
                                var totalValGoodsVAT10 = 0.0; // Tổng tiền hàng thuế VAT10

                                var totalValInvoice = 0.0; // Tổng tiền hàng
                                var totalValVAT = 0.0; // Tổng tiền thuế VAT
                                var totalValPmt = 0.0; // Tổng tiền thanh toán (totalValInvoice + totalValVAT)
                                var listInvoiceDtl = new List<InvoiceDtlUI>();
                                if (listInvoiceDtlUI != null && listInvoiceDtlUI.Count > 0)
                                {
                                    foreach (var it in listInvoiceDtlUI)
                                    {
                                        if (!CUtils.IsNullOrEmpty(it.Invoice_Idx))
                                        {
                                            if (it.Invoice_Idx.ToString().Trim().Equals(invoice_Idx))
                                            {
                                                listInvoiceDtl.Add(it);
                                            }
                                        }
                                    }
                                }
                                
                                if (listInvoiceDtl != null)
                                {
                                    foreach (var it in listInvoiceDtl)
                                    {
                                        var fValInvoiceDtl = 0.0;
                                        fValInvoiceDtl = Convert.ToDouble(it.ValInvoice);

                                        var fValTaxDtl = 0.0;
                                        fValTaxDtl = Convert.ToDouble(it.ValTax);

                                        if (it.VATRateCode.Equals(Client_Mst_VATRate.VATnull))
                                        {
                                            totalValGoodsNotTaxable += fValInvoiceDtl;
                                        }
                                        else if(it.VATRateCode.Equals(Client_Mst_VATRate.VAT0))
                                        {
                                            totalValGoodsNotChargeTax += fValInvoiceDtl;
                                        }
                                        else if (it.VATRateCode.Equals(Client_Mst_VATRate.VAT5))
                                        {
                                            totalValGoodsVAT5 += fValInvoiceDtl;
                                            totalValVAT5 += fValTaxDtl;
                                        }
                                        else if (it.VATRateCode.Equals(Client_Mst_VATRate.VAT10))
                                        {
                                            totalValGoodsVAT10 += fValInvoiceDtl;
                                            totalValVAT10 += fValTaxDtl;
                                        }

                                        //totalValInvoice += fValInvoiceDtl;
                                    }

                                    totalValInvoice = totalValGoodsNotTaxable + totalValGoodsNotChargeTax +
                                                      totalValGoodsVAT5 + totalValGoodsVAT10;
                                    totalValVAT = totalValVAT5 + totalValVAT10;

                                    totalValPmt = totalValInvoice + totalValVAT;
                                }

                                item.ValVAT5 = totalValVAT5;
                                item.ValVAT10 = totalValVAT10;

                                item.ValGoodsNotTaxable = totalValGoodsNotTaxable;
                                item.ValGoodsNotChargeTax = totalValGoodsNotChargeTax;
                                item.ValGoodsVAT5 = totalValGoodsVAT5;
                                item.ValGoodsVAT10 = totalValGoodsVAT10;

                                item.TotalValInvoice = totalValInvoice;
                                item.TotalValVAT = totalValVAT;
                                item.TotalValPmt = totalValPmt;
                            }

                            #endregion

                            #region["Cap so tra cuu"]

                            var iCountListInvoice = listInvoiceUI.Count;
                            var objRQ_Seq_InvoiceCode = new RQ_Seq_InvoiceCode()
                            {
                                Tid = GetNextTId(),
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                NetworkID = userState.MstNNT.NetworkID.ToString().Trim(),
                                OrgID = userState.MstNNT.OrgID.ToString().Trim(),
                                InvoiceAmount = iCountListInvoice,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = ""
                            };
                            var objRT_Seq_InvoiceCode = Seq_InvoiceCodeService.Instance.WA_Seq_InvoiceCode_GetByAmount(objRQ_Seq_InvoiceCode, userState.AddressAPIs);
                            if (objRT_Seq_InvoiceCode != null && objRT_Seq_InvoiceCode.Lst_Seq_InvoiceCode != null &&
                                objRT_Seq_InvoiceCode.Lst_Seq_InvoiceCode.Count > 0)
                            {
                                var listSeq_InvoiceCode = objRT_Seq_InvoiceCode.Lst_Seq_InvoiceCode;
                                if (listInvoiceUI.Count == listSeq_InvoiceCode.Count)
                                {
                                    var i = 0;
                                    foreach (var item in listInvoiceUI)
                                    {
                                        item.InvoiceCode = listSeq_InvoiceCode[i];
                                        var invoice_Idx = item.Invoice_Idx.ToString().Trim();
                                        if (listInvoiceDtlUI != null && listInvoiceDtlUI.Count > 0)
                                        {
                                            var listInvoice_InvoiceDtlCur = listInvoiceDtlUI.Where(it =>
                                                !CUtils.IsNullOrEmpty(it.Invoice_Idx) && it.Invoice_Idx.ToString().Trim().Equals(invoice_Idx)).ToList();
                                            if (listInvoice_InvoiceDtlCur != null && listInvoice_InvoiceDtlCur.Count > 0)
                                            {
                                                foreach (var it in listInvoice_InvoiceDtlCur)
                                                {
                                                    it.InvoiceCode = item.InvoiceCode;
                                                }
                                            }

                                            // Add so tra cuu cho listInvoice_ImportExcel
                                            var listInvoice_ImportExcelCur = listInvoice_ImportExcel.Where(it => !CUtils.IsNullOrEmpty(it.Idx) && it.Idx.ToString().Trim().Equals(invoice_Idx)).ToList();
                                            if (listInvoice_ImportExcelCur != null && listInvoice_ImportExcelCur.Count > 0)
                                            {
                                                foreach (var it in listInvoice_ImportExcelCur)
                                                {
                                                    it.InvoiceCode = item.InvoiceCode;
                                                }
                                            }
                                        }

                                        i++;
                                    }
                                }
                                
                            }
                            #endregion

                            System.Web.HttpContext.Current.Session["List_Invoice_Invoice"] = listInvoiceUI;
                            System.Web.HttpContext.Current.Session["List_Invoice_InvoiceDtl"] = listInvoiceDtlUI;
                            #region["Tinh toan thu"]

                            foreach (var item in listInvoiceUI)
                            {
                                var objInvoice_Invoice = new Invoice_Invoice()
                                {
                                    InvoiceCode = item.InvoiceCode,
                                    MST = item.MST,
                                    NetworkID = item.NetworkID,
                                    RefNo = item.RefNo,
                                    FormNo = item.FormNo,
                                    Sign = item.Sign,
                                    SourceInvoiceCode = item.SourceInvoiceCode,
                                    InvoiceAdjType = item.InvoiceAdjType,
                                    PaymentMethodCode = item.PaymentMethodCode,
                                    InvoiceType2 = item.InvoiceType2,
                                    InvoiceDateUTC = item.InvoiceDateUTC,
                                    CustomerNNTCode = item.CustomerNNTCode,
                                    CustomerNNTName = item.CustomerNNTName,
                                    CustomerNNTAddress = item.CustomerNNTAddress,
                                    CustomerNNTPhone = item.CustomerNNTPhone,
                                    CustomerNNTBankName = item.CustomerNNTBankName,
                                    CustomerNNTEmail = item.CustomerNNTEmail,
                                    CustomerNNTAccNo = item.CustomerNNTAccNo,
                                    CustomerNNTBuyerName = item.CustomerNNTBuyerName,
                                    CustomerMST = item.CustomerMST,
                                    TInvoiceCode = item.TInvoiceCode,
                                    InvoiceNo = item.InvoiceNo,
                                    EmailSend = item.EmailSend,
                                    InvoiceFileSpec = item.InvoiceFileSpec,
                                    InvoiceFilePath = item.InvoiceFilePath,
                                    InvoicePDFFilePath = item.InvoicePDFFilePath,
                                    TotalValInvoice = item.TotalValInvoice, //
                                    TotalValVAT = item.TotalValVAT, //
                                    TotalValPmt = item.TotalValPmt, //
                                    ValGoodsNotTaxable = item.ValGoodsNotTaxable, //
                                    ValGoodsNotChargeTax = item.ValGoodsNotChargeTax, //
                                    ValGoodsVAT5 = item.ValGoodsVAT5, //
                                    ValVAT5 = item.ValVAT5, //
                                    ValGoodsVAT10 = item.ValGoodsVAT10, //
                                    ValVAT10 = item.ValVAT10, //
                                    CurrencyCode = item.CurrencyCode,
                                    CurrencyRate = item.CurrencyRate,
                                    NNTFullName = item.NNTFullName, //
                                    NNTFullAdress = item.NNTFullAdress, //
                                    NNTPhone = item.NNTPhone, //
                                    NNTFax = item.NNTFax, //
                                    NNTEmail = item.NNTEmail, //
                                    NNTWebsite = item.NNTWebsite, //
                                    NNTAccNo = item.NNTAccNo, //
                                    NNTBankName = item.NNTBankName, //
                                    FlagCheckCustomer = item.FlagCheckCustomer, //
                                    FlagConfirm = item.FlagConfirm, //
                                    InvoiceCF1 = item.InvoiceCF1,
                                    InvoiceCF2 = item.InvoiceCF2,
                                    InvoiceCF3 = item.InvoiceCF3,
                                    InvoiceCF4 = item.InvoiceCF4,
                                    InvoiceCF5 = item.InvoiceCF5,
                                    InvoiceCF6 = item.InvoiceCF6,
                                    InvoiceCF7 = item.InvoiceCF7,
                                    InvoiceCF8 = item.InvoiceCF8,
                                    InvoiceCF9 = item.InvoiceCF9,
                                    InvoiceCF10 = item.InvoiceCF10,
                                    Remark = item.Remark,
                                };
                                listInvoice_Invoice.Add(objInvoice_Invoice);
                            }

                            foreach (var item in listInvoiceDtlUI)
                            {
                                var objInvoice_InvoiceDtl = new Invoice_InvoiceDtl()
                                {
                                    InvoiceCode = item.InvoiceCode,
                                    Idx = item.Idx,
                                    NetworkID = item.NetworkID,
                                    SpecCode = item.SpecCode,
                                    SpecName = item.SpecName,
                                    VATRateCode = item.VATRateCode,
                                    VATRate = item.VATRate,
                                    UnitCode = item.UnitCode,
                                    UnitName = item.UnitName,
                                    UnitPrice = item.UnitPrice,
                                    Qty = item.Qty,
                                    ValInvoice = item.ValInvoice,
                                    ValTax = item.ValTax,
                                    InventoryCode = item.InventoryCode,
                                    DiscountRate = item.DiscountRate,
                                    ValDiscount = item.ValDiscount,
                                    Remark = item.Remark,
                                    InvoiceDCF1 = item.InvoiceDCF1,
                                    InvoiceDCF2 = item.InvoiceDCF2,
                                    InvoiceDCF3 = item.InvoiceDCF3,
                                    InvoiceDCF4 = item.InvoiceDCF4,
                                    InvoiceDCF5 = item.InvoiceDCF5,
                                };
                                listInvoice_InvoiceDtl.Add(objInvoice_InvoiceDtl);
                            }

                            var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
                            {
                                Tid = GetNextTId(),
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                Lst_Invoice_Invoice = listInvoice_Invoice,
                                Lst_Invoice_InvoiceDtl = listInvoice_InvoiceDtl,
                                
                            };

                            var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_Calc(objRQ_Invoice_Invoice, userState.AddressAPIs);
                            #endregion

                            #region[""]

                            if (objRT_Invoice_Invoice.Lst_Invoice_InvoiceCalc != null &&
                                objRT_Invoice_Invoice.Lst_Invoice_InvoiceCalc.Count > 0)
                            {
                                foreach (var item in objRT_Invoice_Invoice.Lst_Invoice_InvoiceCalc)
                                {
                                    var invoiceCodeCur = item.InvoiceCode.ToString().Trim();

                                    //var listInvoiceImportExcelCur = listInvoice_ImportExcel.Where(it => it.Idx.ToString().Trim().Equals(objInvoiceUI.Invoice_Idx)).ToList();
                                    var listInvoiceImportExcelCur = listInvoice_ImportExcel.Where(it => it.InvoiceCode.ToString().Trim().Equals(invoiceCodeCur)).ToList();

                                    if (item.ErrorCode.ToString().Trim().Equals("0"))
                                    {
                                        var objInvoice = listInvoice_Invoice.FirstOrDefault(it => it.InvoiceCode.ToString().Trim().Equals(invoiceCodeCur));
                                        listInvoice_Invoice_Sign.Add(objInvoice);
                                        if (listInvoiceImportExcelCur != null && listInvoiceImportExcelCur.Count > 0)
                                        {
                                            foreach (var it in listInvoiceImportExcelCur)
                                            {
                                                it.FlagResult = "1";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //var objInvoiceUI = listInvoiceUI.FirstOrDefault(it => it.InvoiceCode.ToString().Trim().Equals(invoiceCodeCur));
                                        
                                        if (listInvoiceImportExcelCur != null && listInvoiceImportExcelCur.Count > 0)
                                        {
                                            foreach (var it in listInvoiceImportExcelCur)
                                            {
                                                it.FlagResult = "0";
                                                var errorMessage = "";
                                                errorMessage = Constants.Error.GetErrorMessage(item.ErrorCode.ToString().Trim());
                                                if (CUtils.IsNullOrEmpty(errorMessage))
                                                {
                                                    errorMessage = item.ErrorCode.ToString().Trim();
                                                }
                                                it.ImportResult = errorMessage;
                                            }
                                        }
                                    }
                                }
                            }
                            System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = listInvoice_ImportExcel;
                            if (listInvoice_Invoice_Sign.Count > 0)
                            {
                                var iCount = listInvoice_Invoice_Sign.Count;
                                SingleSign(new List<Invoice_Invoice>(){ listInvoice_Invoice_Sign[0] }, listInvoice_InvoiceDtl);
                                
                                var listInvoice_Invoice_SignSkip_1 = listInvoice_Invoice_Sign.Skip(1).Take(iCount - 1).ToList();
                                var pagesize = 100;
                                var pagecount = 0;
                                var totalItem = listInvoice_Invoice_SignSkip_1.Count;
                                pagecount = (totalItem % pagesize == 0) ? (totalItem / pagesize) : (totalItem / pagesize + 1);
                                

                                for (int i = 0; i < pagecount; i++)
                                {
                                    var listInvoice_Invoice_SignSkipCur = listInvoice_Invoice_SignSkip_1.Skip(pagesize * i).Take(pagesize).ToList();
                                    MultiSign(listInvoice_Invoice_SignSkipCur, listInvoice_InvoiceDtl);
                                }
                                
                            }
                            #endregion

                            if (System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] != null)
                            {
                                listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
                                iTongDongDuLieu = listInvoice_ImportExcel.Count;
                            }
                            var listInvoice_ImportExcel_Distinct = listInvoice_ImportExcel.GroupBy(it => it.Idx.ToString().Trim()).Select(g => g.First()).ToList();

                            if (listInvoice_ImportExcel_Distinct != null && listInvoice_ImportExcel_Distinct.Count > 0)
                            {
                                iTongSLHD = listInvoice_ImportExcel_Distinct.Count;
                                var _listBoQua = listInvoice_ImportExcel_Distinct.Where(it => !CUtils.IsNullOrEmpty(it.FlagResult) && it.FlagResult.ToString().Trim().Equals("0")).ToList();
                                var _listThanhCong = listInvoice_ImportExcel_Distinct.Where(it => !CUtils.IsNullOrEmpty(it.FlagResult) && it.FlagResult.ToString().Trim().Equals("1")).ToList();
                                var _listKoThanhCong = listInvoice_ImportExcel_Distinct.Where(it => !CUtils.IsNullOrEmpty(it.FlagResult) && it.FlagResult.ToString().Trim().Equals("2")).ToList();

                                if (_listBoQua != null && _listBoQua.Count > 0)
                                {
                                    iTongSLHDBoQua = _listBoQua.Count;
                                }
                                if (_listThanhCong != null && _listThanhCong.Count > 0)
                                {
                                    iTongSLHDThanhCong = _listThanhCong.Count;
                                }
                                if (_listKoThanhCong != null && _listKoThanhCong.Count > 0)
                                {
                                    iTongSLHDKoThanhCong = _listKoThanhCong.Count;
                                }
                            }

                            return Json(new { Success = true, FileName = fileName, TongSLHD = iTongSLHD, TongSLHDBoQua = iTongSLHDBoQua, TongSLHDThanhCong = iTongSLHDThanhCong, TongSLHDKoThanhCong = iTongSLHDKoThanhCong });
                            //listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
                        }
                        else
                        {
                            exitsData = "Hệ thống chỉ hỗ trợ tối đa " + iMaxRows + " dòng dữ liệu trong 1 lần nhập!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        

                        
                    }
                    #region[""]

                    #endregion
                    resultEntry.Success = true;
                    exitsData = "Đã nhập dữ liệu excel thành công!";
                    resultEntry.AddMessage(exitsData);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateCompact()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateCompact();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Invoice_ImportExcel).Name), ref url);
                ExcelExport.ExportToExcelFromList_Qinvoice(listInvoice_ImportExcel, dicColNames, filePath, string.Format("Invoice_ImportExcel"), "Mã HH,DV");

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateFull();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Invoice_ImportExcel).Name), ref url);
                ExcelExport.ExportToExcelFromList_Qinvoice(listInvoice_ImportExcel, dicColNames, filePath, string.Format("Invoice_ImportExcel"), "Mã HH,DV");

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportDetailResult()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
                if (System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] != null)
                {
                    listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
                }
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                Dictionary<string, string> dicColNames = GetImportDicColumsDetailResult();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Invoice_ImportExcel).Name), ref url);
                ExcelExport.ExportToExcelFromList_Qinvoice(listInvoice_ImportExcel, dicColNames, filePath, string.Format("Invoice_ImportExcel"), "Mã HH,DV");

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["MultiSign"]

        private void MultiSign(List<Invoice_Invoice> listInvoice_Invoice, List<Invoice_InvoiceDtl> listInvoice_InvoiceDtl)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            List<Thread> listThreads = new List<Thread>();
            var nLoadSize = listInvoice_Invoice.Count;
            
            var exitsData = "";
            var contentsignOutput = "";
            var certdata = "";
            var errorMessage = "";
            var SerialNumber = "5404FFFEB7033FB316D672201B7F1BCA";
            var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
            if (System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] != null)
            {
                listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
            }
            for (int nScan = 0; nScan < nLoadSize; nScan++)
            {
                Thread t = new Thread((objParam) =>
                {
                    var errorMessageCur = "";
                    var errorCodeCur = "";
                    Invoice_Invoice nInvoice_InvoiceCur = (Invoice_Invoice)objParam;
                    var IsResult = false;
                    try
                    {
                        
                        var listInvoice_InvoiceDtlCur = listInvoice_InvoiceDtl.Where(it =>
                            !CUtils.IsNullOrEmpty(it.InvoiceCode) && it.InvoiceCode.ToString().Trim().Equals(nInvoice_InvoiceCur.InvoiceCode)).ToList();

                        #region["Luu hoa don"]
                        var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
                        {
                            Tid = GetNextTId(),
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            Lst_Invoice_Invoice = new List<Invoice_Invoice>() { nInvoice_InvoiceCur },
                            Lst_Invoice_InvoiceDtl = listInvoice_InvoiceDtlCur,

                        };

                        var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_SaveAndAllocatedInv(objRQ_Invoice_Invoice, userState.AddressAPIs);
                        #endregion

                        #region["Get"]
                        var objRT_Invoice_Invoice_Search = Invoice_Invoice_Get(objRQ_Invoice_Invoice.Lst_Invoice_Invoice[0].InvoiceCode.ToString().Trim());
                        #endregion

                        #region["Update trạng thái hóa đơn vào listInvoice_ImportExcel"]

                        var objInvoiceUI_Update_InvoiceStatus = new InvoiceUI()
                        {
                            InvoiceCode = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceCode.ToString().Trim(),
                            InvoiceNo = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceNo.ToString().Trim(),
                            InvoiceStatus = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceStatus.ToString().Trim(),
                            MailSentDateTime = null,
                        };
                        UpdateListInvoice_ImportExcel(ref listInvoice_ImportExcel, objInvoiceUI_Update_InvoiceStatus);
                        #endregion

                        #region["Sinh file XML"]

                        var fileBase64 = GetXMLBase64(objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceCode.ToString().Trim());
                        #endregion

                        #region["Sign"]

                        var decode = CUtils.Base64_Decode(fileBase64).Replace(@"&", "-");

                        var signOld = "xmlns=\"http://www.w3.org/2000/09/xmldsig#\"";
                        var contentsignOutputCur = decode.Replace(signOld, "");
                        var serCUr = new SerializerXML();
                        var contentsignOutputCurObj = serCUr.Deserialize<Invoice>(contentsignOutputCur);

                        if (ESign.Sign("0", ref SerialNumber, decode, ref contentsignOutput,
                                ref errorMessage, ref certdata) == true)
                        {
                            contentsignOutput = CUtils.Base64_Encode(contentsignOutput);
                            #region["Phat hanh"]
                            objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceFileEncodeBase64 = contentsignOutput;
                            Invoice_Invoice_ApprovedAndIssued(objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice);
                            #endregion
                            nInvoice_InvoiceCur.InvoiceStatus = "ISSUED";
                            IsResult = true;
                            
                            SendMailPost(contentsignOutputCurObj, objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0],
                                userState, ref IsResult, ref errorCodeCur, ref errorMessageCur);
                        }
                        else
                        {
                            resultEntry.Success = false;
                            resultEntry.AddMessage(errorMessage);
                            errorMessageCur = errorMessage;
                            IsResult = false;
                            //ImportResult(nInvoice_InvoiceCur, IsResult, errorCodeCur, errorMessageCur);
                        }
                        #endregion

                        IsResult = true;
                    }
                    catch (Exception ex)
                    {
                        resultEntry.SetFailed().AddException(this, ex);
                        IsResult = false;
                        errorCodeCur = resultEntry.ReturnErrorCode_ErrorMessage(ex, ref errorMessageCur);
                        if (CUtils.IsNullOrEmpty(errorMessageCur))
                        {
                            errorMessageCur = errorCodeCur;
                        }
                        else
                        {
                            errorMessageCur = errorMessageCur + " ZZZZZ " + resultEntry.Detail;
                        }
                    }
                    //getSession = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"];
                    ImportResult(ref listInvoice_ImportExcel, nInvoice_InvoiceCur, IsResult, errorCodeCur, errorMessageCur);


                });
                listThreads.Add(t);
                t.Start(listInvoice_Invoice[nScan]);
            }

            foreach (var tScan in listThreads)
            {
                tScan.Join();
            }

            System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = listInvoice_ImportExcel;
        }

        private void SingleSign(List<Invoice_Invoice> listInvoice_Invoice, List<Invoice_InvoiceDtl> listInvoice_InvoiceDtl)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var nLoadSize = listInvoice_Invoice.Count;
            
            var exitsData = "";
            var contentsignOutput = "";
            var certdata = "";
            var errorMessage = "";
            var SerialNumber = "5404FFFEB7033FB316D672201B7F1BCA";
            var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
            if (System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] != null)
            {
                listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
            }
            for (int nScan = 0; nScan < listInvoice_Invoice.Count; nScan++)
            {
                var errorMessageCur = "";
                var errorCodeCur = "";
                var IsResult = false;
                Invoice_Invoice nInvoice_InvoiceCur = listInvoice_Invoice[nScan];
                try
                {
                    
                    var listInvoice_InvoiceDtlCur = listInvoice_InvoiceDtl.Where(it =>
                        !CUtils.IsNullOrEmpty(it.InvoiceCode) && it.InvoiceCode.ToString().Trim().Equals(nInvoice_InvoiceCur.InvoiceCode)).ToList();

                    #region["Luu hoa don"]
                    var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
                    {
                        Tid = GetNextTId(),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        Lst_Invoice_Invoice = new List<Invoice_Invoice>() { nInvoice_InvoiceCur },
                        Lst_Invoice_InvoiceDtl = listInvoice_InvoiceDtlCur,

                    };

                    var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_SaveAndAllocatedInv(objRQ_Invoice_Invoice, userState.AddressAPIs);
                    #endregion

                    #region["Get"]
                    var objRT_Invoice_Invoice_Search = Invoice_Invoice_Get(objRQ_Invoice_Invoice.Lst_Invoice_Invoice[0].InvoiceCode.ToString().Trim());
                    #endregion
                    #region["Update trạng thái hóa đơn vào listInvoice_ImportExcel"]

                    var objInvoiceUI_Update_InvoiceStatus = new InvoiceUI()
                    {
                        InvoiceCode = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceCode.ToString().Trim(),
                        InvoiceNo = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceNo.ToString().Trim(),
                        InvoiceStatus = objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceStatus.ToString().Trim(),
                        MailSentDateTime = null,
                    };
                    UpdateListInvoice_ImportExcel(ref listInvoice_ImportExcel, objInvoiceUI_Update_InvoiceStatus);
                    #endregion
                    #region["Sinh file XML"]

                    var fileBase64 = GetXMLBase64(objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceCode.ToString());
                    #endregion

                    #region["Sign"]

                    var decode = CUtils.Base64_Decode(fileBase64).Replace(@"&", "-");

                    var signOld = "xmlns=\"http://www.w3.org/2000/09/xmldsig#\"";
                    var contentsignOutputCur = decode.Replace(signOld, "");
                    var serCUr = new SerializerXML();
                    var contentsignOutputCurObj = serCUr.Deserialize<Invoice>(contentsignOutputCur);
                    if (ESign.Sign("0", ref SerialNumber, decode, ref contentsignOutput, ref errorMessage, ref certdata) == true)
                    {
                        contentsignOutput = CUtils.Base64_Encode(contentsignOutput);
                        #region["Phat hanh"]
                        objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0].InvoiceFileEncodeBase64 = contentsignOutput;
                        Invoice_Invoice_ApprovedAndIssued(objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice);
                        #endregion
                        IsResult = true;
                        nInvoice_InvoiceCur.InvoiceStatus = "ISSUED";

                        SendMailPost(contentsignOutputCurObj, objRT_Invoice_Invoice_Search.Lst_Invoice_Invoice[0],
                            userState, ref IsResult, ref errorCodeCur, ref errorMessageCur);
                    }
                    else
                    {
                        resultEntry.Success = false;
                        resultEntry.AddMessage(errorMessage);
                        errorMessageCur = errorMessage;
                        IsResult = false;
                        //ImportResult(nInvoice_InvoiceCur, IsResult, errorCodeCur, errorMessageCur);
                    }
                    #endregion


                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(this, ex);
                    IsResult = false;
                    errorCodeCur = resultEntry.ReturnErrorCode_ErrorMessage(ex, ref errorMessageCur);
                    if (CUtils.IsNullOrEmpty(errorMessageCur))
                    {
                        errorMessageCur = errorCodeCur;
                    }
                    else
                    {
                        errorMessageCur = errorMessageCur + " ZZZZZ " + resultEntry.Detail;
                    }
                }

                ImportResult(ref listInvoice_ImportExcel, nInvoice_InvoiceCur, IsResult, errorCodeCur, errorMessageCur);

            }

            System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = listInvoice_ImportExcel;
        }

        private RT_Invoice_Invoice Invoice_Invoice_Get(string invoiceCode)
        {
            var listInvoice_Invoice = new List<Invoice_Invoice>();
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var strWhereClauseInvoice_Invoice = strWhereClause_Invoice_Invoice(new Invoice_Invoice() { InvoiceCode = invoiceCode });
            var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Invoice_Invoice = "*",
                Rt_Cols_Invoice_InvoiceDtl = "*",
                //Rt_Cols_Invoice_InvoicePrd = "*",
                Ft_WhereClause = strWhereClauseInvoice_Invoice
            };
            var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_Get(objRQ_Invoice_Invoice, userState.AddressAPIs);

            return objRT_Invoice_Invoice;
        }

        private void Invoice_Invoice_ApprovedAndIssued(List<Invoice_Invoice> listInvoice_Invoice)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Lst_Invoice_Invoice = listInvoice_Invoice,
                //Lst_Invoice_InvoiceDtl = listInvoice_InvoiceDtl,
            };
            var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_ApprovedAndIssued(objRQ_Invoice_Invoice, userState.AddressAPIs);

        }

        private string GetXMLBase64(string invoiceCode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var filename = "";
            var objInvoiceTemplate = new InvoiceTemplateInput()
            {
                ObjMstNNT = userState.MstNNT
            };
            #region["Invoice"]
            var strWhereClauseInvoice_Invoice = strWhereClause_Invoice_Invoice(new Invoice_Invoice() { InvoiceCode = invoiceCode });
            var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Invoice_Invoice = "*",
                Rt_Cols_Invoice_InvoiceDtl = "*",
                Rt_Cols_Invoice_InvoicePrd = "*",
                Ft_WhereClause = strWhereClauseInvoice_Invoice
            };
            var objRT_Invoice_InvoiceCur = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_Get(objRQ_Invoice_Invoice, userState.AddressAPIs);
            if (objRT_Invoice_InvoiceCur.Lst_Invoice_Invoice != null &&
                objRT_Invoice_InvoiceCur.Lst_Invoice_Invoice.Count > 0)
            {
                objInvoiceTemplate.ObjInvoice = objRT_Invoice_InvoiceCur.Lst_Invoice_Invoice[0];
                #region Lấy thông tin hóa đơn gốc
                var refNo = objInvoiceTemplate.ObjInvoice.RefNo;
                if (refNo != null && refNo.ToString() != "")
                {
                    objInvoiceTemplate.ObjInvoiceRefNo = new Invoice_Invoice();
                    var strWhereInvoiceRefNo = strWhereClause_Invoice_Invoice(new Invoice_Invoice() { InvoiceCode = refNo });
                    var objRQ_InvoiceRefNo = new RQ_Invoice_Invoice()
                    {
                        Tid = GetNextTId(),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Rt_Cols_Invoice_Invoice = "*",
                        Ft_WhereClause = strWhereInvoiceRefNo
                    };
                    var rtInvoiceRefNo = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_Get(objRQ_InvoiceRefNo, userState.AddressAPIs);
                    if (rtInvoiceRefNo != null) if (rtInvoiceRefNo.Lst_Invoice_Invoice != null)
                        {
                            objInvoiceTemplate.ObjInvoiceRefNo = rtInvoiceRefNo.Lst_Invoice_Invoice[0];
                        }
                }

                #endregion


                #region["TemInvoice"]
                var strWhereClauseInvoice_TempInvoice = strWhereClause_Invoice_TempInvoice(new Invoice_TempInvoice() { TInvoiceCode = objRT_Invoice_InvoiceCur.Lst_Invoice_Invoice[0].TInvoiceCode });
                var objRQ_Invoice_TempInvoice = new RQ_Invoice_TempInvoice()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Invoice_TempInvoice = "*",
                    Ft_WhereClause = strWhereClauseInvoice_TempInvoice
                };
                var objRT_Invoice_TempInvoice = Invoice_TempInvoiceService.Instance.WA_Invoice_TempInvoice_Get(objRQ_Invoice_TempInvoice, userState.AddressAPIs);
                if (objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice != null &&
                    objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice.Count > 0)
                {
                    var logoFilePath = "";
                    var watermarkFilePath = "";
                    if (!CUtils.IsNullOrEmpty(objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].LogoFilePath))
                    {
                        var iindex = objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].LogoFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            logoFilePath = objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].LogoFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            logoFilePath = userState.AddressAPIs + @"api/File/" + objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].LogoFilePath.ToString().Trim().Replace(@"\", "/");
                        }

                    }
                    if (!CUtils.IsNullOrEmpty(objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].WatermarkFilePath))
                    {

                        var iindex = objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].WatermarkFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            watermarkFilePath = objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].WatermarkFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            watermarkFilePath = userState.AddressAPIs + @"api/File/" + objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].WatermarkFilePath.ToString().Trim().Replace(@"\", "/");
                        }
                    }

                    objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].LogoFilePath = logoFilePath;
                    objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0].WatermarkFilePath = watermarkFilePath;
                    objInvoiceTemplate.ObjTempInvoice =
                        objRT_Invoice_TempInvoice.Lst_Invoice_TempInvoice[0];
                }
                #endregion
                #region["CustomerNNT"]                
                var invoice = objInvoiceTemplate.ObjInvoice;
                var mstCustomerNNT = new Mst_CustomerNNT()
                {
                    ContactName = invoice.CustomerNNTBuyerName,
                    CustomerNNTName = invoice.CustomerNNTName,
                    CustomerNNTAddress = invoice.CustomerNNTAddress,
                    CustomerMST = invoice.CustomerMST,
                    AccNo = invoice.CustomerNNTAccNo
                };
                objInvoiceTemplate.ObjMstCustomerNNT = mstCustomerNNT;
                #endregion
                #region["NNT"]                   
                var objNNT = new Mst_NNT()
                {
                    NNTFullName = invoice.NNTFullName == null ? "" : invoice.NNTFullName.ToString(),
                    MST = invoice.MST == null ? "" : invoice.MST,
                    NNTAddress = invoice.NNTFullAdress == null ? "" : invoice.NNTFullAdress.ToString(),
                    ContactPhone = invoice.NNTPhone == null ? "" : invoice.NNTPhone.ToString(),
                    ContactEmail = invoice.NNTEmail == null ? "" : invoice.NNTEmail.ToString(),
                    AccNo = invoice.NNTAccNo == null ? "" : invoice.NNTAccNo.ToString(),
                    NNTPhone = invoice.NNTPhone == null ? "" : invoice.NNTPhone.ToString(),
                    NNTFax = invoice.NNTFax == null ? "" : invoice.NNTFax.ToString(),
                    Website = invoice.NNTWebsite == null ? "" : invoice.NNTWebsite.ToString()
                };
                objInvoiceTemplate.ObjMstNNT = objNNT;
                #endregion
            }
            #endregion

            #region["InvoiceDtl"]

            if (objRT_Invoice_InvoiceCur.Lst_Invoice_InvoiceDtl != null &&
                objRT_Invoice_InvoiceCur.Lst_Invoice_InvoiceDtl.Count > 0)
            {
                objInvoiceTemplate.ListInvoiceDtl = new List<Invoice_InvoiceDtl>();
                objInvoiceTemplate.ListInvoiceDtl.AddRange(objRT_Invoice_InvoiceCur.Lst_Invoice_InvoiceDtl);
            }
            #endregion

            var fileBase64 = "";
            var invoiceTGroupCode = CUtils.StrValue(objInvoiceTemplate.ObjInvoice.itg_InvoiceTGroupCode);
            var spec_Prd_Type = CUtils.StrValue(objInvoiceTemplate.ObjInvoice.itg_Spec_Prd_Type);
            if (invoiceTGroupCode.ToString().Contains(Client_InvoiceTGroupCode.MAUNVAT))
            {

                Create_Mau_Hoa_Don_Spec_NVAT(objInvoiceTemplate, filename, ref fileBase64);

            }
            else if (invoiceTGroupCode.ToString().Contains(Client_InvoiceTGroupCode.MAU1VAT) ||
                     invoiceTGroupCode.Equals(Client_InvoiceTGroupCode.MAUX20))
            {
                Create_Mau_Hoa_Don_Spec_1VAT(objInvoiceTemplate, filename, ref fileBase64);
            }
            if (spec_Prd_Type.Equals(Spec_Prd_Type.Product_Id))
            {
                if (invoiceTGroupCode.Equals(Client_InvoiceTGroupCode.MAUHTC) || invoiceTGroupCode.Equals(Client_InvoiceTGroupCode.MAUHTCQR))
                {
                    objInvoiceTemplate.FlagQRCode = invoiceTGroupCode.Equals(Client_InvoiceTGroupCode.MAUHTCQR) ? "1" : "0";

                    Create_Mau_Hoa_Don_ProductId_1VATHTC(objInvoiceTemplate, filename, ref fileBase64);
                }
            }
            return fileBase64;
        }

        private void SendMail(Invoice contentsignOutputCurObj, Invoice_Invoice invoiceInvoice, UserState userState, ref bool IsResult, ref string errorCodeCur, ref string errorMessageCur)
        {
            object moniter = new object();
            lock (moniter)
            {
                IsResult = false;
                errorMessageCur = "";
                errorCodeCur = "";
                #region["Gui mail"]
                #region["Html"]
                #region[""]
                var FormNo = "";
                var Sign = "";
                var LogoFilePath = "";
                var WatermarkFilePath = "";
                var TInvoiceCode = "";
                var TInvoiceName = "";
                var InvoiceCode = "";
                var InvoiceNo = "";
                var InvoiceDateUTC = "";
                var day = "";
                var month = "";
                var year = "";
                var mpm_PaymentMethodName = "";
                // NNT
                var NNTFullName = "";
                var MST = "";
                var NNTAddress = "";
                var NNTPhone = "";
                var ContactName = "";
                var ContactPhone = "";
                var ContactEmail = "";
                var AccNo = "";
                var mp_ProvinceName = "";

                // CusNNT
                var CusContactName = "";
                var CustomerNNTName = "";
                var CusMST = "";
                var CustomerNNTAddress = "";
                var CusAccNo = "";
                var objInvoiceTemplate = contentsignOutputCurObj.InvoiceTemplate;

                var objTempInvoice = objInvoiceTemplate.ObjTempInvoice;
                var objInvoice = objInvoiceTemplate.ObjInvoice;
                var objMstNNT = objInvoiceTemplate.ObjMstNNT;
                var objMstCustomerNNT = objInvoiceTemplate.ObjMstCustomerNNT;


                if (objTempInvoice != null)
                {
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.FormNo))
                    {
                        FormNo = objTempInvoice.FormNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.Sign))
                    {
                        Sign = objTempInvoice.Sign.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.LogoFilePath))
                    {
                        var iindex = objTempInvoice.LogoFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            LogoFilePath = objTempInvoice.LogoFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            LogoFilePath = userState.AddressAPIs + @"api/File/" + objTempInvoice.LogoFilePath.ToString().Trim().Replace(@"\", "/");
                        }

                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.WatermarkFilePath))
                    {

                        var iindex = objTempInvoice.WatermarkFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            WatermarkFilePath = objTempInvoice.WatermarkFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            WatermarkFilePath = userState.AddressAPIs + @"api/File/" + objTempInvoice.WatermarkFilePath.ToString().Trim().Replace(@"\", "/");
                        }
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.TInvoiceCode))
                    {
                        TInvoiceCode = objTempInvoice.TInvoiceCode.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.TInvoiceName))
                    {
                        TInvoiceName = objTempInvoice.TInvoiceName.ToString().Trim();
                    }
                }

                if (objInvoice != null)
                {
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceCode))
                    {
                        InvoiceCode = objInvoice.InvoiceCode.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceNo))
                    {
                        InvoiceNo = objInvoice.InvoiceNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceDateUTC))
                    {
                        InvoiceDateUTC = objInvoice.InvoiceDateUTC.ToString().Trim();
                        //if (CUtils.IsDateTime(InvoiceDateUTC))
                        //{
                        //    var dInvoiceDateUTC = CUtils.ConvertToDateTime(InvoiceDateUTC);
                        //    day = dInvoiceDateUTC.Day.ToString();
                        //    month = dInvoiceDateUTC.Month.ToString();
                        //    year = dInvoiceDateUTC.Year.ToString();
                        //    InvoiceDateUTC = dInvoiceDateUTC.ToString(Nonsense.DATE_TIME_FORMAT_TVAN);
                        //}
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.mpm_PaymentMethodName))
                    {
                        mpm_PaymentMethodName = objInvoice.mpm_PaymentMethodName.ToString().Trim();
                    }
                }

                if (objMstNNT != null)
                {
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTFullName))
                    {
                        NNTFullName = objMstNNT.NNTFullName.ToString().ToUpper().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.MST))
                    {
                        MST = objMstNNT.MST.ToString().ToUpper().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTAddress))
                    {
                        NNTAddress = objMstNNT.NNTAddress.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTPhone))
                    {
                        NNTPhone = objMstNNT.NNTPhone.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactName))
                    {
                        ContactName = objMstNNT.ContactName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactPhone))
                    {
                        ContactPhone = objMstNNT.ContactPhone.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactEmail))
                    {
                        ContactEmail = objMstNNT.ContactEmail.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.AccNo))
                    {
                        AccNo = objMstNNT.AccNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.mp_ProvinceName))
                    {
                        mp_ProvinceName = objMstNNT.mp_ProvinceName.ToString().Trim();
                    }
                }

                if (objMstCustomerNNT != null)
                {
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.ContactName))
                    {
                        CusContactName = objMstCustomerNNT.ContactName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.CustomerNNTName))
                    {
                        CustomerNNTName = objMstCustomerNNT.CustomerNNTName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.MST))
                    {
                        CusMST = objMstCustomerNNT.MST.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.CustomerNNTAddress))
                    {
                        CustomerNNTAddress = objMstCustomerNNT.CustomerNNTAddress.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.AccNo))
                    {
                        CusAccNo = objMstCustomerNNT.AccNo.ToString().Trim();
                    }
                }
                #endregion
                #endregion
                var strTitle = @"NNT <" + MST + @"> gửi hóa đơn số <" + InvoiceNo + @">, mẫu <" + FormNo + @">, ký hiệu: <" + Sign + @"> cho <" + CustomerNNTName + ">";

                SendMailUI mail = new SendMailUI();
                mail.MST = MST;
                mail.NNTFullName = NNTFullName;
                mail.mp_ProvinceName = mp_ProvinceName;
                mail.Sign = Sign;
                mail.mp_ProvinceName = mp_ProvinceName;
                mail.InvoiceCode = InvoiceCode;
                mail.FormNo = FormNo;
                mail.ContactPhone = ContactPhone;
                mail.ContactName = ContactName;
                mail.ContactEmail = ContactEmail;
                mail.InvoiceDateUTC = InvoiceDateUTC;
                mail.InvoiceNo = InvoiceNo;
                mail.CustomerNNTName = CustomerNNTName;
                var strBodyHTML = BodyEMail.GetContentMailNVAT(mail);
                var url = APIsSendMail + "?key=" + KeySendMail + "&displayName=Qinvoice" + "&subject=" +
                          strTitle + "&from=" + EmailName + "&to=" +
                          invoiceInvoice.EmailSend.ToString() +
                          "&message=" + strBodyHTML;

                try
                {
                    WebRequest request = WebRequest.Create(url);
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    WebResponse response = request.GetResponse();
                    
                    Stream dataStream = response.GetResponseStream();

                    var reader = new StreamReader(dataStream);

                    string vData = reader.ReadToEnd();

                    //var sendMailResponse = JsonConvert.DeserializeObject<VelocaResponse>(vData);

                    reader.Close();
                    response.Close();
                    IsResult = true;
                }
                catch (Exception _ex)
                {
                    errorCodeCur = _ex.Message;
                    errorMessageCur = _ex.StackTrace;
                    IsResult = false;
                }
                #endregion
                
            }

        }

        private void SendMailPost(Invoice contentsignOutputCurObj, Invoice_Invoice invoiceInvoice, UserState userState, ref bool IsResult, ref string errorCodeCur, ref string errorMessageCur)
        {
            object moniter = new object();
            lock (moniter)
            {
                IsResult = false;
                errorMessageCur = "";
                errorCodeCur = "";
                #region["Gui mail"]
                #region["Html"]
                #region[""]
                var FormNo = "";
                var Sign = "";
                var LogoFilePath = "";
                var WatermarkFilePath = "";
                var TInvoiceCode = "";
                var TInvoiceName = "";
                var InvoiceCode = "";
                var InvoiceNo = "";
                var InvoiceDateUTC = "";
                var day = "";
                var month = "";
                var year = "";
                var mpm_PaymentMethodName = "";
                // NNT
                var NNTFullName = "";
                var MST = "";
                var NNTAddress = "";
                var NNTPhone = "";
                var ContactName = "";
                var ContactPhone = "";
                var ContactEmail = "";
                var AccNo = "";
                var mp_ProvinceName = "";

                // CusNNT
                var CusContactName = "";
                var CustomerNNTName = "";
                var CusMST = "";
                var CustomerNNTAddress = "";
                var CusAccNo = "";
                var objInvoiceTemplate = contentsignOutputCurObj.InvoiceTemplate;

                var objTempInvoice = objInvoiceTemplate.ObjTempInvoice;
                var objInvoice = objInvoiceTemplate.ObjInvoice;
                var objMstNNT = objInvoiceTemplate.ObjMstNNT;
                var objMstCustomerNNT = objInvoiceTemplate.ObjMstCustomerNNT;


                if (objTempInvoice != null)
                {
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.FormNo))
                    {
                        FormNo = objTempInvoice.FormNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.Sign))
                    {
                        Sign = objTempInvoice.Sign.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.LogoFilePath))
                    {
                        var iindex = objTempInvoice.LogoFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            LogoFilePath = objTempInvoice.LogoFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            LogoFilePath = userState.AddressAPIs + @"api/File/" + objTempInvoice.LogoFilePath.ToString().Trim().Replace(@"\", "/");
                        }

                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.WatermarkFilePath))
                    {

                        var iindex = objTempInvoice.WatermarkFilePath.ToString().Trim().IndexOf("http");
                        if (iindex >= 0)
                        {
                            WatermarkFilePath = objTempInvoice.WatermarkFilePath.ToString().Trim().Replace(@"\", @"/");
                        }
                        else
                        {
                            WatermarkFilePath = userState.AddressAPIs + @"api/File/" + objTempInvoice.WatermarkFilePath.ToString().Trim().Replace(@"\", "/");
                        }
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.TInvoiceCode))
                    {
                        TInvoiceCode = objTempInvoice.TInvoiceCode.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objTempInvoice.TInvoiceName))
                    {
                        TInvoiceName = objTempInvoice.TInvoiceName.ToString().Trim();
                    }
                }

                if (objInvoice != null)
                {
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceCode))
                    {
                        InvoiceCode = objInvoice.InvoiceCode.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceNo))
                    {
                        InvoiceNo = objInvoice.InvoiceNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.InvoiceDateUTC))
                    {
                        InvoiceDateUTC = objInvoice.InvoiceDateUTC.ToString().Trim();
                        //if (CUtils.IsDateTime(InvoiceDateUTC))
                        //{
                        //    var dInvoiceDateUTC = CUtils.ConvertToDateTime(InvoiceDateUTC);
                        //    day = dInvoiceDateUTC.Day.ToString();
                        //    month = dInvoiceDateUTC.Month.ToString();
                        //    year = dInvoiceDateUTC.Year.ToString();
                        //    InvoiceDateUTC = dInvoiceDateUTC.ToString(Nonsense.DATE_TIME_FORMAT_TVAN);
                        //}
                    }
                    if (!CUtils.IsNullOrEmpty(objInvoice.mpm_PaymentMethodName))
                    {
                        mpm_PaymentMethodName = objInvoice.mpm_PaymentMethodName.ToString().Trim();
                    }
                }

                if (objMstNNT != null)
                {
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTFullName))
                    {
                        NNTFullName = objMstNNT.NNTFullName.ToString().ToUpper().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.MST))
                    {
                        MST = objMstNNT.MST.ToString().ToUpper().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTAddress))
                    {
                        NNTAddress = objMstNNT.NNTAddress.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.NNTPhone))
                    {
                        NNTPhone = objMstNNT.NNTPhone.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactName))
                    {
                        ContactName = objMstNNT.ContactName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactPhone))
                    {
                        ContactPhone = objMstNNT.ContactPhone.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.ContactEmail))
                    {
                        ContactEmail = objMstNNT.ContactEmail.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.AccNo))
                    {
                        AccNo = objMstNNT.AccNo.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstNNT.mp_ProvinceName))
                    {
                        mp_ProvinceName = objMstNNT.mp_ProvinceName.ToString().Trim();
                    }
                }

                if (objMstCustomerNNT != null)
                {
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.ContactName))
                    {
                        CusContactName = objMstCustomerNNT.ContactName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.CustomerNNTName))
                    {
                        CustomerNNTName = objMstCustomerNNT.CustomerNNTName.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.MST))
                    {
                        CusMST = objMstCustomerNNT.MST.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.CustomerNNTAddress))
                    {
                        CustomerNNTAddress = objMstCustomerNNT.CustomerNNTAddress.ToString().Trim();
                    }
                    if (!CUtils.IsNullOrEmpty(objMstCustomerNNT.AccNo))
                    {
                        CusAccNo = objMstCustomerNNT.AccNo.ToString().Trim();
                    }
                }
                #endregion
                #endregion
                var strTitle = @"NNT <" + MST + @"> gửi hóa đơn số <" + InvoiceNo + @">, mẫu <" + FormNo + @">, ký hiệu: <" + Sign + @"> cho <" + CustomerNNTName + ">";

                SendMailUI mail = new SendMailUI();
                mail.MST = MST;
                mail.NNTFullName = NNTFullName;
                mail.mp_ProvinceName = mp_ProvinceName;
                mail.Sign = Sign;
                mail.mp_ProvinceName = mp_ProvinceName;
                mail.InvoiceCode = InvoiceCode;
                mail.FormNo = FormNo;
                mail.ContactPhone = ContactPhone;
                mail.ContactName = ContactName;
                mail.ContactEmail = ContactEmail;
                mail.InvoiceDateUTC = InvoiceDateUTC;
                mail.InvoiceNo = InvoiceNo;
                mail.CustomerNNTName = CustomerNNTName;
                var strBodyHTML = BodyEMail.GetContentMailNVAT(mail);
                //var url = APIsSendMail + "?key=" + KeySendMail + "&displayName=Qinvoice" + "&subject=" +
                //          strTitle + "&from=" + EmailName + "&to=" +
                //          invoiceInvoice.EmailSend.ToString() +
                //          "&message=" + strBodyHTML;
                var url = APIsSendMail;
                var resultEntry = new JsonResultEntry() { Success = false };
                try
                {
                    var strDateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();
                        data["key"] = KeySendMail;
                        data["displayName"] = "Qinvoice";
                        data["subject"] = strTitle;
                        data["from"] = EmailName;
                        data["to"] = invoiceInvoice.EmailSend.ToString();
                        data["message"] = strBodyHTML;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        var response = wb.UploadValues(url, "POST", data);
                        string responseInString = Encoding.UTF8.GetString(response);
                        var result = responseInString;
                    }

                    #region["Cập nhật thời gian gửi mail"]
                    var objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
                    {
                        Tid = GetNextTId(),
                        WAUserCode = userState.SysUser.UserCode,
                        WAUserPassword = userState.SysUser.UserPassword,
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        Lst_Invoice_Invoice = new List<Invoice_Invoice>() { new Invoice_Invoice(){InvoiceCode = invoiceInvoice.InvoiceCode, MailSentDTimeUTC = strDateTime} },
                        
                    };

                    var objRT_Invoice_Invoice = InvoiceInvoiceService.Instance.WA_Invoice_Invoice_UpdMailSentDTimeUTC(objRQ_Invoice_Invoice, userState.AddressAPIs);
                    #endregion

                    IsResult = true;

                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(this, ex);
                    IsResult = false;
                    errorCodeCur = resultEntry.ReturnErrorCode_ErrorMessage(ex, ref errorMessageCur);
                    if (CUtils.IsNullOrEmpty(errorMessageCur))
                    {
                        errorMessageCur = errorCodeCur;
                    }
                }
                #endregion

            }

        }

        private void ImportResult(ref List<Invoice_ImportExcel> listInvoice_ImportExcel, Invoice_Invoice invoice, bool isResult, string errorCode, string errorMessage)
        {
            object moniter = new object();
            lock (moniter)
            {
                if (listInvoice_ImportExcel != null && listInvoice_ImportExcel.Count > 0)
                {
                    var listInvoiceImportExcelCur = listInvoice_ImportExcel.Where(it => it.InvoiceCode.ToString().Trim().Equals(invoice.InvoiceCode)).ToList();
                    if (listInvoiceImportExcelCur != null && listInvoiceImportExcelCur.Count > 0)
                    {
                        foreach (var it in listInvoiceImportExcelCur)
                        {
                            if (isResult)
                            {
                                it.ImportResult = "Thành công";
                            }
                            else
                            {
                                it.FlagResult = "2";
                                if (CUtils.IsNullOrEmpty(errorMessage))
                                {
                                    errorMessage = errorCode;
                                }
                                it.ImportResult = "Lỗi: " + errorMessage;
                            }
                        }
                    }
                }
            }
            
        }

        private void UpdateListInvoice_ImportExcel(ref List<Invoice_ImportExcel> listInvoice_ImportExcel, InvoiceUI invoice)
        {
            object moniter = new object();
            lock (moniter)
            {
                if (listInvoice_ImportExcel != null && listInvoice_ImportExcel.Count > 0)
                {
                    var listInvoiceImportExcelCur = listInvoice_ImportExcel.Where(it => it.InvoiceCode.ToString().Trim().Equals(invoice.InvoiceCode)).ToList();
                    if (listInvoiceImportExcelCur != null && listInvoiceImportExcelCur.Count > 0)
                    {
                        foreach (var it in listInvoiceImportExcelCur)
                        {
                            it.InvoiceStatus = invoice.InvoiceStatus;
                            it.MailSentDateTime = invoice.MailSentDateTime;
                        }
                    }
                }
            }
        }

        //private void ImportResult(Invoice_Invoice invoice, bool isResult, string errorCode, string errorMessage)
        //{
        //    var abc = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"];
        //    var listInvoice_ImportExcel = new List<Invoice_ImportExcel>();
        //    if (System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] != null)
        //    {
        //        listInvoice_ImportExcel = System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] as List<Invoice_ImportExcel>;
        //    }
        //    if (listInvoice_ImportExcel != null && listInvoice_ImportExcel.Count > 0)
        //    {
        //        var listInvoiceImportExcelCur = listInvoice_ImportExcel.Where(it => it.InvoiceCode.ToString().Trim().Equals(invoice.InvoiceCode)).ToList();
        //        if (listInvoiceImportExcelCur != null && listInvoiceImportExcelCur.Count > 0)
        //        {
        //            foreach (var it in listInvoiceImportExcelCur)
        //            {
        //                if (isResult)
        //                {
        //                    it.ImportResult = "Thành công";
        //                }
        //                else
        //                {
        //                    it.FlagResult = "2";
        //                    if (CUtils.IsNullOrEmpty(errorMessage))
        //                    {
        //                        errorMessage = errorCode;
        //                    }
        //                    it.ImportResult = errorMessage;
        //                }
        //            }
        //        }
        //    }

        //    System.Web.HttpContext.Current.Session["List_Invoice_ImportExcel"] = listInvoice_ImportExcel;
        //}
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplateFull()
        {
            return new Dictionary<string, string>()
            {
                //Master
                {TblInvoice_ImportExcel.Idx,"Số thứ tự HĐ (*)"},
                {TblInvoice_ImportExcel.InvoiceDateUTC,"Ngày hóa đơn (*)"},
                {TblInvoice_ImportExcel.CustomerNNTCode,"Mã khách hàng (*)"},
                {TblInvoice_ImportExcel.CustomerNNTName,"Tên khách hàng"},
                {TblInvoice_ImportExcel.CustomerNNTAddress,"Địa chỉ"},
                {TblInvoice_ImportExcel.CustomerNNTBuyerName,"Người mua hàng"},
                {TblInvoice_ImportExcel.CustomerMST,"Mã số thuế"},
                {TblInvoice_ImportExcel.CustomerNNTPhone,"Điện thoại"},
                {TblInvoice_ImportExcel.CustomerNNTAccNo,"Tài khoản"},
                {TblInvoice_ImportExcel.EmailSend,"Email nhận HĐ"},
                {TblInvoice_ImportExcel.PaymentMethodCode,"Hình thức TT"},
                {TblInvoice_ImportExcel.Remark,"Diễn giải"},
                {TblInvoice_ImportExcel.InvoiceCF1,"InvoiceCF1"},
                {TblInvoice_ImportExcel.InvoiceCF2,"InvoiceCF2"},
                {TblInvoice_ImportExcel.InvoiceCF3,"InvoiceCF3"},
                {TblInvoice_ImportExcel.InvoiceCF4,"InvoiceCF4"},
                {TblInvoice_ImportExcel.InvoiceCF5,"InvoiceCF5"},
                {TblInvoice_ImportExcel.InvoiceCF6,"InvoiceCF6"},
                {TblInvoice_ImportExcel.InvoiceCF7,"InvoiceCF7"},
                {TblInvoice_ImportExcel.InvoiceCF8,"InvoiceCF8"},
                {TblInvoice_ImportExcel.InvoiceCF9,"InvoiceCF9"},
                {TblInvoice_ImportExcel.InvoiceCF10,"InvoiceCF10"},
                // Detail
                {TblInvoice_ImportExcel.SpecCode,"Mã HH,DV"},
                {TblInvoice_ImportExcel.SpecName,"Tên hàng hóa, dịch vụ (*)"},
                //{TblInvoice_ImportExcel.VATRateCode,"Mã thuế suất"},
                {TblInvoice_ImportExcel.VATRate,"Thuế suất (*)"},
                {TblInvoice_ImportExcel.UnitCode,"Mã đơn vị"},
                {TblInvoice_ImportExcel.UnitName,"Tên đơn vị (*)"},
                {TblInvoice_ImportExcel.UnitPrice," Đơn giá (*)"},
                {TblInvoice_ImportExcel.Qty,"Số lượng"}, // Người dùng ko nhập thì truyền 0, làm tròn 2 số sau dấu thập phân
                {TblInvoice_ImportExcel.ValInvoice,"Thành tiền (*)"},
                {TblInvoice_ImportExcel.ValTax,"Tiền thuế (*)"},
                {TblInvoice_ImportExcel.InventoryCode,"Mã kho"},
                {TblInvoice_ImportExcel.DiscountRate,"Tỉ lệ chiết khấu"}, // Người dùng ko nhập thì truyền 0, làm tròn 2 số sau dấu thập phân
                {TblInvoice_ImportExcel.ValDiscount,"Tiền chiết khấu"}, // Người dùng ko nhập thì truyền 0
                {TblInvoice_ImportExcel.RemarkDtl,"Diễn giải HH, DV"},
                {TblInvoice_ImportExcel.InvoiceDCF1,"InvoiceDCF1"},
                {TblInvoice_ImportExcel.InvoiceDCF2,"InvoiceDCF2"},
                {TblInvoice_ImportExcel.InvoiceDCF3,"InvoiceDCF3"},
                {TblInvoice_ImportExcel.InvoiceDCF4,"InvoiceDCF4"},
                {TblInvoice_ImportExcel.InvoiceDCF5,"InvoiceDCF5"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateCompact()
        {
            return new Dictionary<string, string>()
            {
                //Master
                {TblInvoice_ImportExcel.Idx,"Số thứ tự HĐ (*)"},
                {TblInvoice_ImportExcel.InvoiceDateUTC,"Ngày hóa đơn (*)"},
                {TblInvoice_ImportExcel.CustomerNNTCode,"Mã khách hàng (*)"},
                {TblInvoice_ImportExcel.CustomerNNTName,"Tên khách hàng"},
                {TblInvoice_ImportExcel.CustomerNNTAddress,"Địa chỉ"},
                {TblInvoice_ImportExcel.CustomerNNTBuyerName,"Người mua hàng"},
                {TblInvoice_ImportExcel.CustomerMST,"Mã số thuế"},
                {TblInvoice_ImportExcel.EmailSend,"Email nhận HĐ"},
                {TblInvoice_ImportExcel.PaymentMethodCode,"Hình thức TT"},
                
                // Detail
                {TblInvoice_ImportExcel.SpecCode,"Mã HH,DV"},
                {TblInvoice_ImportExcel.SpecName,"Tên hàng hóa, dịch vụ (*)"},
                //{TblInvoice_ImportExcel.VATRateCode,"Mã thuế suất"},
                {TblInvoice_ImportExcel.VATRate,"Thuế suất"}, //???
                {TblInvoice_ImportExcel.UnitCode,"Mã đơn vị"},
                {TblInvoice_ImportExcel.UnitName,"Tên đơn vị (*)"},
                {TblInvoice_ImportExcel.UnitPrice," Đơn giá (*)"},
                {TblInvoice_ImportExcel.Qty,"Số lượng"}, // Người dùng ko nhập thì truyền 0, làm tròn 2 số sau dấu thập phân
                {TblInvoice_ImportExcel.ValInvoice,"Thành tiền (*)"},
                {TblInvoice_ImportExcel.ValTax,"Tiền thuế (*)"},
                
            };
        }

        private Dictionary<string, string> GetImportDicColumsDetailResult()
        {
            return new Dictionary<string, string>()
            {
                //Master
                {TblInvoice_ImportExcel.InvoiceCode,"Số tra cứu HĐ"},
                {TblInvoice_ImportExcel.InvoiceNo,"Số hóa đơn"},
                {TblInvoice_ImportExcel.FormNo,"Mẫu số"},
                {TblInvoice_ImportExcel.Sign,"Ký hiệu"},
                {TblInvoice_ImportExcel.Idx,"Số thứ tự HĐ (*)"},
                {TblInvoice_ImportExcel.InvoiceDateUTC,"Ngày hóa đơn (*)"},
                {TblInvoice_ImportExcel.InvoiceStatus,"Trạng thái HĐ"},
                {TblInvoice_ImportExcel.CustomerNNTCode,"Mã khách hàng (*)"},
                {TblInvoice_ImportExcel.CustomerNNTName,"Tên khách hàng"},
                {TblInvoice_ImportExcel.CustomerNNTBuyerName,"Người mua hàng"},
                {TblInvoice_ImportExcel.CustomerMST,"Mã số thuế"},
                {TblInvoice_ImportExcel.CustomerNNTAddress,"Địa chỉ"},
                {TblInvoice_ImportExcel.CustomerNNTPhone,"Điện thoại"},
                {TblInvoice_ImportExcel.EmailSend,"Email nhận HĐ"},
                {TblInvoice_ImportExcel.MailSentDateTime,"Thời gian gửi mail"},
                {TblInvoice_ImportExcel.CustomerNNTAccNo,"Tài khoản"},
                {TblInvoice_ImportExcel.PaymentMethodCode,"Hình thức TT"},
                {TblInvoice_ImportExcel.Remark,"Diễn giải"},
                {TblInvoice_ImportExcel.InvoiceCF1,"InvoiceCF1"},
                {TblInvoice_ImportExcel.InvoiceCF2,"InvoiceCF2"},
                {TblInvoice_ImportExcel.InvoiceCF3,"InvoiceCF3"},
                {TblInvoice_ImportExcel.InvoiceCF4,"InvoiceCF4"},
                {TblInvoice_ImportExcel.InvoiceCF5,"InvoiceCF5"},
                {TblInvoice_ImportExcel.InvoiceCF6,"InvoiceCF6"},
                {TblInvoice_ImportExcel.InvoiceCF7,"InvoiceCF7"},
                {TblInvoice_ImportExcel.InvoiceCF8,"InvoiceCF8"},
                {TblInvoice_ImportExcel.InvoiceCF9,"InvoiceCF9"},
                {TblInvoice_ImportExcel.InvoiceCF10,"InvoiceCF10"},
                // Detail
                {TblInvoice_ImportExcel.SpecCode,"Mã HH,DV"},
                {TblInvoice_ImportExcel.SpecName,"Tên hàng hóa, dịch vụ (*)"},
                {TblInvoice_ImportExcel.VATRateCode,"Mã thuế suất"},
                {TblInvoice_ImportExcel.VATRate,"Thuế suất (*)"},
                {TblInvoice_ImportExcel.UnitCode,"Mã đơn vị"},
                {TblInvoice_ImportExcel.UnitName,"Tên đơn vị (*)"},
                {TblInvoice_ImportExcel.UnitPrice,"Đơn giá"},
                {TblInvoice_ImportExcel.Qty,"Số lượng"}, // Người dùng ko nhập thì truyền 0, làm tròn 2 số sau dấu thập phân
                {TblInvoice_ImportExcel.ValInvoice,"Thành tiền (*)"},
                {TblInvoice_ImportExcel.ValTax,"Tiền thuế (*)"},
                {TblInvoice_ImportExcel.InventoryCode,"Mã kho"},
                {TblInvoice_ImportExcel.DiscountRate,"Tỉ lệ chiết khấu"}, // Người dùng ko nhập thì truyền 0, làm tròn 2 số sau dấu thập phân
                {TblInvoice_ImportExcel.ValDiscount,"Tiền chiết khấu"}, // Người dùng ko nhập thì truyền 0
                {TblInvoice_ImportExcel.RemarkDtl,"Diễn giải HH, DV"},
                {TblInvoice_ImportExcel.InvoiceDCF1,"InvoiceDCF1"},
                {TblInvoice_ImportExcel.InvoiceDCF2,"InvoiceDCF2"},
                {TblInvoice_ImportExcel.InvoiceDCF3,"InvoiceDCF3"},
                {TblInvoice_ImportExcel.InvoiceDCF4,"InvoiceDCF4"},
                {TblInvoice_ImportExcel.InvoiceDCF5,"InvoiceDCF5"},
                {TblInvoice_ImportExcel.ImportResult,"Kết quả"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Invoice_TempGroup(Invoice_TempGroup data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Invoice_TempGroup = TableName.Invoice_TempGroup + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Invoice_TempGroup + tblInvoice_TempGroup.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }


            strWhereClause = sbSql.ToString();

            if (!CUtils.IsNullOrEmpty(strWhereClause))
            {
                var strWhere = "";

                strWhere += " and ";
                strWhere += " ( ";
                strWhere += (Tbl_Invoice_TempGroup + tblInvoice_TempGroup.MST + " = '" + ClientMix.MSTRoot + "'");
                if (!CUtils.IsNullOrEmpty(data.MST))
                {
                    strWhere += " or ";
                    strWhere += (Tbl_Invoice_TempGroup + tblInvoice_TempGroup.MST + " = '" + data.MST + "'");
                }
                strWhere += " ) ";
                strWhereClause += strWhere;
            }

            return strWhereClause;
        }

        private string strWhereClause_OS_PrdCenter_Mst_Spec(OS_PrdCenter_Mst_Spec data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";

            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecCode, CUtils.StrValue(data.SpecCode), "in");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_CustomerNNT(Mst_CustomerNNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerNNT = TableName.Mst_CustomerNNT + ".";

            if (!CUtils.IsNullOrEmpty(data.CustomerNNTCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerNNT + TblMst_CustomerNNT.CustomerNNTCode, CUtils.StrValue(data.CustomerNNTCode), "in");
            }
            
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Invoice_TempInvoice(Invoice_TempInvoice data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_TempInvoice = TableName.Invoice_TempInvoice + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceCode))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.InvoiceCode, CUtils.StrValue(data.InvoiceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceType))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.InvoiceType, CUtils.StrValue(data.InvoiceType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FormNo))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.FormNo, CUtils.StrValue(data.FormNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.Sign))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.Sign, CUtils.StrValue(data.Sign), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.TInvoiceStatus))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.TInvoiceStatus, CUtils.StrValue(data.TInvoiceStatus), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.TInvoiceCode))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.TInvoiceCode, CUtils.StrValue(data.TInvoiceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_TempInvoice + TblInvoice_TempInvoice.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Invoice_Invoice(Invoice_Invoice data )
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Invoice_Invoice = TableName.Invoice_Invoice + ".";
            var Tbl_Invoice_TempInvoice = TableName.Invoice_TempInvoice + ".";
            var Tbl_CustomerNNT = TableName.Mst_CustomerNNT + ".";
            if (!CUtils.IsNullOrEmpty(data.InvoiceCode))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.InvoiceCode, CUtils.StrValue(data.InvoiceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerNNTBuyerName))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.CustomerNNTBuyerName, "%" + CUtils.StrValue(data.CustomerNNTBuyerName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.Sign))
            {
                sbSql.AddWhereClause(Tbl_Invoice_TempInvoice + TblInvoice_TempInvoice.Sign, CUtils.StrValue(data.Sign), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FormNo))
            {
                sbSql.AddWhereClause(Tbl_Invoice_TempInvoice + TblInvoice_TempInvoice.FormNo, CUtils.StrValue(data.FormNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CreateDTimeUTC))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.CreateDTimeUTC, CUtils.StrValue(data.CreateDTimeUTC), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceDateUTC))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.InvoiceDateUTC, CUtils.StrValue(data.InvoiceDateUTC), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceAdjType))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.InvoiceAdjType, CUtils.StrValue(data.InvoiceAdjType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.MST, CUtils.StrValue(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceNo))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.InvoiceNo, CUtils.StrValue(data.InvoiceNo), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvoiceStatus))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.InvoiceStatus, CUtils.StrValue(data.InvoiceStatus), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SourceInvoiceCode))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.SourceInvoiceCode, CUtils.StrValue(data.SourceInvoiceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.TInvoiceCode))
            {
                sbSql.AddWhereClause(Tbl_Invoice_Invoice + TblInvoice_Invoice.TInvoiceCode, CUtils.StrValue(data.TInvoiceCode), "=");
            }

            if (!CUtils.IsNullOrEmpty(data.CustomerMST))
            {
                sbSql.AddWhereClause(Tbl_CustomerNNT + TblMst_CustomerNNT.CustomerMST, CUtils.StrValue(data.CustomerMST), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        
    }
}