using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Invoice_InvoiceUI: Invoice_Invoice
    {
        public string InvoiceDateUTCFrom { get; set; } // Ngày hóa đơn từ
        public string InvoiceDateUTCTo { get; set; } // Ngày hóa đơn đến

        public double SumBeforeVAT { get; set; } 
        public double VAT { get; set; }
        public double SumAfterVAT { get; set; }
        public double TongThanhToan { get; set; }
        //public string FormNo { get; set; }
        //public string Sign { get; set; }

        //public string InvoiceTGroupCode { get; set; }

        public string InvoiceType { get; set; }
        public List<Invoice_InvoiceDtl> lstInvoiceDtl { get; set; }


        //Tính tổng tiền
        public double TienHangKhongThue { get; set; }
        public double TienHangThue0 { get; set; }
        public double TienHangThue5 { get; set; }
        public double TienHangThue10 { get; set; }
        public double TienThueKhongThue { get; set; }
        public double TienThueThue0 { get; set; }
        public double TienThueThue5 { get; set; }
        public double TienThueThue10 { get; set; }
        public double TongKhongThue { get; set; }
        public double TongThue { get; set; }
        public double TongCoThue { get; set; }
        public string TienBangChu { get; set; }


        public double TongTienHang { get; set; }
    }
}
