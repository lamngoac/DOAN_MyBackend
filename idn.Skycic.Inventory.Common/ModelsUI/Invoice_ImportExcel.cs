using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Invoice_ImportExcel
    {
        public object InvoiceCode { get; set; }

        public object InvoiceNo { get; set; }
        //public object NetworkID { get; set; }
        //public object MST { get; set; }
        public object FormNo { get; set; }
        public object Sign { get; set; }
        public object InvoiceStatus { get; set; }
        public object MailSentDateTime { get; set; }

        public object CustomerNNTBankName { get; set; }

        // Master (22)
        public object Idx { get; set; } // Số thứ tự
        public object InvoiceDateUTC { get; set; } // Ngày hóa đơn (*)
        public object CustomerNNTCode { get; set; } // Mã khách hàng (*)
        public object CustomerNNTName { get; set; } // Tên khách hàng
        public object CustomerNNTAddress { get; set; } // Địa chỉ
        public object CustomerNNTBuyerName { get; set; } // Người mua hàng
        public object CustomerMST { get; set; } // Mã số thuế
        public object CustomerNNTPhone { get; set; } // Điện thoại
        public object CustomerNNTAccNo { get; set; } // Tài khoản
        public object EmailSend { get; set; } // Email nhận HĐ      
        public object PaymentMethodCode { get; set; } // Hình thức thanh toán (*)
        public object Remark { get; set; } // Diễn giải
        public object InvoiceCF1 { get; set; } // InvoiceCF1
        public object InvoiceCF2 { get; set; } // InvoiceCF2
        public object InvoiceCF3 { get; set; } // InvoiceCF3
        public object InvoiceCF4 { get; set; } // InvoiceCF4
        public object InvoiceCF5 { get; set; } // InvoiceCF5
        public object InvoiceCF6 { get; set; } // InvoiceCF6
        public object InvoiceCF7 { get; set; } // InvoiceCF7
        public object InvoiceCF8 { get; set; } // InvoiceCF8
        public object InvoiceCF9 { get; set; } // InvoiceCF9
        public object InvoiceCF10 { get; set; } // InvoiceCF10

        // Detail (19)
        public object SpecCode { get; set; } // Mã HH,DV (*)
        public object SpecName { get; set; } // Tên hàng hóa, dịch vụ
        public object VATRateCode { get; set; } // Mã thuế suất
        public object VATRate { get; set; } // Thuế suất (*)
        public object UnitCode { get; set; } // Mã đơn vị (*)
        public object UnitName { get; set; } // Tên đơn vị
        public object UnitPrice { get; set; } // Đơn giá (*)
        public object Qty { get; set; } // Số lượng
        public object ValInvoice { get; set; } // Thành tiền (*)
        public object ValTax { get; set; } // Tiền thuế (*)
        public object InventoryCode { get; set; } // Mã kho
        public object DiscountRate { get; set; } // Tỉ lệ chiết khấu
        public object ValDiscount { get; set; } // Tiền chiết khấu
        public object RemarkDtl { get; set; } // Diễn giải
        public object InvoiceDCF1 { get; set; } // InvoiceDCF1
        public object InvoiceDCF2 { get; set; } // InvoiceDCF2
        public object InvoiceDCF3 { get; set; } // InvoiceDCF3
        public object InvoiceDCF4 { get; set; } // InvoiceDCF4
        public object InvoiceDCF5 { get; set; } // InvoiceDCF5

        //
        public object ImportResult { get; set; } // Kết quả

        //FlagResult: 0, 1, 2
        // 0: tinh toan thu loi => bo qua khong truyen cho Biz;
        // 1: tinh toan thu thanh cong => truyen cho Biz (ket qua thanh cong)
        // 2: tinh toan thu thanh cong => truyen cho Biz (ket qua KO thanh cong)
        public object FlagResult { get; set; } 
    }
}
