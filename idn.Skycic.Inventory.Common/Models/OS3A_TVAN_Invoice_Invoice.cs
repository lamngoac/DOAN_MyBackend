using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS3A_TVAN_Invoice_Invoice
    {

        public object InvoiceCode { get; set; } // Mã tham chiếu hệ thống bán hàng

        public object InvoiceNo { get; set; } // Mã tham chiếu hệ thống bán hàng

        public object CustomerNNTCode { get; set; } // Mã khách hàng

        public object CustomerNNTBuyerName { get; set; } // Người mua hàng 

        public object CustomerNNTName { get; set; } // Tên khách hàng

        public object CustomerNNTAddress { get; set; } // Địa chỉ

        public object CustomerMST { get; set; } // Mã số thuế khách hàng

        public object Remark { get; set; } // Diễn giải chung

        public object CurrencyCode { get; set; } // Mã ngoại tệ ( mặc định “VND”)

        public object CurrencyRate { get; set; } // Tỉ giá ngoại tệ so với VND

        public object VATRateCode { get; set; } // Mã thuế suất

        public object VATRate { get; set; } // Thuế suất

        public object TotalValInvoice { get; set; } // Tổng tiền hàng ( Chưa VAT)

        public object TotalValVAT { get; set; } // Tổng tiên thuế

        public object InvoiceCF9 { get; set; } // Loại hóa đơn(1. Hàng hóa vật tư, 2. Hàng hóa dịch vụ)

        public object InvoiceCF10 { get; set; } // Tổng chiết khấu

        public object TotalValPmt { get; set; } // Tổng thanh toán

        public object CreateBy { get; set; } // Mã người lập

        public object InvoiceDateUTC { get; set; } // Ngày hóa đơn

        public object CreateDTimeUTC { get; set; } // Ngay lập hóa đơn

        public object Sign { get; set; } // Seri hóa đơn ( ví dụ AA/18E…)

        public object PaymentMethodCode { get; set; } // Hinh thức thanh toán

        public object InvoiceStatus { get; set; } // Trạng thái hóa đơn

    }
}
