using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Web.Models
{
    public class Invoice_InvoiceUIExport : Invoice_Invoice
    {
        public object Idx { get; set; } // Mã Hàng hóa

        public object SpecCode { get; set; } // Mã Hàng hóa

        public object SpecName { get; set; } // Tên Spec

        public object ProductID { get; set; } // Mã Prd

        public object ProductName { get; set; } // Tên Prd

        public object VATRateCode { get; set; } // Mã thuế suất

        public object VATRate { get; set; } // Tỷ lệ thuế

        public object UnitCode { get; set; } // Mã đơn vị

        public object UnitName { get; set; } // Tên đơn vị

        public object UnitPrice { get; set; } // Giá

        public object Qty { get; set; } // Số lượng

        public object ValTax { get; set; } // Tỷ lệ

        public object InventoryCode { get; set; } // Đơn vị 

        public object DiscountRate { get; set; } // Giá đơn vị

        public object ValDiscount { get; set; } // Số lượng

        public object InvoiceDtlStatus { get; set; } // trạng thái

        public object RemarkDtl { get; set; }

        public object InvoiceDCF1 { get; set; }

        public object InvoiceDCF2 { get; set; }

        public object InvoiceDCF3 { get; set; }

        public object InvoiceDCF4 { get; set; }

        public object InvoiceDCF5 { get; set; }
    } 
}