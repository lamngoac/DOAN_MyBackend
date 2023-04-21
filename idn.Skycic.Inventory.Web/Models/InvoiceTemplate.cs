using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Web.Models
{
    public class InvoiceTemplateInput
    {
        public string FlagQRCode { get; set; }
        public Mst_NNT ObjMstNNT { get; set; }
        public Mst_CustomerNNT ObjMstCustomerNNT { get; set; }

        public Invoice_Invoice ObjInvoice { get; set; }
        public Invoice_TempInvoice ObjTempInvoice { get; set; }

        public List<Invoice_InvoiceDtl> ListInvoiceDtl { get; set; }

        public Invoice_Invoice ObjInvoiceRefNo { get; set; }

        //public List<Invoice_InvoicePrd> ListInvoice_InvoicePrd { get; set; }
    }

    //[XmlRoot(Namespace = "http://www.w3.org/2000/09/xmldsig#",
    //    ElementName = "Signature",
    //    DataType = "string",
    //    IsNullable = true)]
    public class Signature
    {
        //public SignedInfo SignedInfo { get; set; }
        public string SignatureValue { get; set; }
        public KeyInfo KeyInfo { get; set; }
    }

    public class SignedInfo
    {
        public string CanonicalizationMethod { get; set; }
        public string SignatureMethod { get; set; }
        public Reference Reference { get; set; }
    }

    public class Reference
    {
        public Transforms Transforms { get; set; }
        public string DigestMethod { get; set; }
        public string DigestValue { get; set; }

    }

    public class Transforms
    {
        public string Transform { get; set; }
    }

    public class KeyInfo
    {
        public KeyValue KeyValue { get; set; }
        public X509Data X509Data { get; set; }
    }

    public class KeyValue
    {
        public RSAKeyValue RSAKeyValue { get; set; }
    }

    public class RSAKeyValue
    {
        public string Modulus { get; set; }
        public string Exponent { get; set; }
    }

    public class X509Data
    {
        public string X509SubjectName { get; set; }
        public string X509Certificate { get; set; }
    }


    [XmlRoot(Namespace = "http://kekhaithue.gdt.gov.vn/HSoDKy",
        ElementName = "Invoice",
        DataType = "string",
        IsNullable = true)]
    public class Invoice
    {
        public InvoiceTemplate InvoiceTemplate { get; set; }
        public List<Signature> CKyDTu { get; set; }

        //public List<Products> ListProducts { get; set; }
    }

    public class Products
    {
        public string CateProId { get; set; }
        public string ProductName { get; set; }

        //public CategoriesPro CategoriesPro { get; set; }
    }

    public class CategoriesPro
    {
        public string CateProId { get; set; }
        public string CateProName { get; set; }
    }

    public class InvoiceTemplate
    {
        public ObjTempInvoice ObjTempInvoice { get; set; }
        public ObjInvoice ObjInvoice { get; set; }
        public ObjInvoiceRefNo ObjInvoiceRefNo { get; set; }
        public ObjMstNNT ObjMstNNT { get; set; }
        public ObjMstCustomerNNT ObjMstCustomerNNT { get; set; }

        public List<ObjInvoiceDtl> ListInvoiceDtl { get; set; }

        public string TienHHKhongChiuThue { get; set; }
        public string TienHHThueSuat0 { get; set; }
        public string TienHHThueSuat5 { get; set; }
        public string TienHHThueSuat10 { get; set; }
        public string TienThueKhongChiuThue { get; set; }
        public string TienThueThueSuat0 { get; set; }
        public string TienThueThueSuat5 { get; set; }
        public string TienThueThueSuat10 { get; set; }
        public string TongTienChuaThue { get; set; }
        public string TongTienThue { get; set; }
        public string TongTienCoThue { get; set; }
        public string SoTienBangChu { get; set; }

        public string ThueSuat { get; set; } // Mẫu 1VAT

        public string TongTienHang { get; set; } // Mẫu bảng 1VAT

        // phần chung
        public string FlagInChuyenDoi { get; set; } // 1: in chuyển đổi; 0: các trường hợp khác
        public string FlagMau { get; set; } // 1: là mẫu; 0: hóa đơn
        public string InvDay { get; set; }
        public string InvMonth { get; set; }
        public string InvYear { get; set; }
        // ngày tháng chuyển đổi
        public string InvCDDay { get; set; }
        public string InvCDMonth { get; set; }
        public string InvCDYear { get; set; }
        //
        public string NNTSignedSticker { get; set; }
        public string NNTSignedName { get; set; }
        public string NNTSignedDate { get; set; }
        public string NNTCustomerSignedSticker { get; set; }
        public string NNTCustomerSignedName { get; set; }
        public string NNTCustomerSignedDate { get; set; }
        public string InvTotalValPmtCountVN { get; set; }
    }

    public class ObjTempInvoice
    {
        public string FormNo { get; set; }
        public string Sign { get; set; }
        public string LogoFilePath { get; set; }
        public string WatermarkFilePath { get; set; }
        public string TInvoiceCode { get; set; }
        public string TInvoiceName { get; set; }
        public string InvoiceTGroupCode { get; set; }
        public string VATType { get; set; }
    }

    public class ObjInvoiceRefNo
    {
        public string FormNo { get; set; }
        public string Sign { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDateUTC { get; set; }
        public string DeleteReason { get; set; }
    }

    public class ObjInvoice
    {
        public string InvoiceCode { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDateUTC { get; set; }
        public string mpm_PaymentMethodName { get; set; }

        public string FlagQRCode { get; set; }
        public string itg_InvoiceTGroupCode { get; set; }
        public string itg_Spec_Prd_Type { get; set; }

        public string TienHHKhongChiuThue { get; set; }
        public string TienHHThueSuat0 { get; set; }
        public string TienHHThueSuat5 { get; set; }
        public string TienHHThueSuat10 { get; set; }
        public string TienThueKhongChiuThue { get; set; }
        public string TienThueThueSuat0 { get; set; }
        public string TienThueThueSuat5 { get; set; }
        public string TienThueThueSuat10 { get; set; }
        public string TongTienChuaThue { get; set; }
        public string TongTienThue { get; set; }
        public string TongTienCoThue { get; set; }
        public string SoTienBangChu { get; set; }

        public string ThueSuat { get; set; } // Mẫu 1VAT

        public string SourceInvoiceCode { get; set; } // Nguồn hóa đơn : gốc, điều chỉnh, thay thế
        public string InvoiceAdjType { get; set; } // Loại điều chỉnh (Dùng khi hóa đơn điều chỉnh)
    }

    public class ObjMstNNT
    {
        public string NNTFullName { get; set; }
        public string MST { get; set; }
        public string NNTAddress { get; set; }
        public string NNTPhone { get; set; }
        public string NNTFax { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string AccNo { get; set; }
        public string mp_ProvinceName { get; set; }
        public string Website { get; set; }
        public string UserActionName { get; set; } // Lay ten nguoi chuyen doi, hoac nguoi xoa
        public string NNTEmail { get; set; }
    }

    public class ObjMstCustomerNNT
    {
        public string ContactName { get; set; }
        public string CustomerNNTName { get; set; }
        public string MST { get; set; }
        public string CustomerNNTAddress { get; set; }
        public string AccNo { get; set; }
    }

    public class ObjInvoiceDtl
    {
        public string STT { get; set; }
        public string ms_SpecName { get; set; }
        public string UnitCode { get; set; }
        public string VATRate { get; set; }
        public string Qty { get; set; }
        public string UnitPrice { get; set; }
        public string ThanhTien { get; set; }
        public string VATRateCode { get; set; }

        // Dùng khi type = ProductID
        public string Brand { get; set; } // Nhãn hiệu
        public string ProductID { get; set; } // số khung
        public string SoMay { get; set; } // customfield1 productid
        public string NamSX { get; set; }
        public string Spec { get; set; } // loại xe
    }


}