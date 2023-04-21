using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS3A_TVAN_Invoice_InvoiceDtl
    {
        public object InvoiceCode { get; set; } // Mã tham chiếu hệ thống bán hàng

        public object InventoryCode { get; set; } // Mã kho

        public object SpecCode { get; set; } // Mã vật tư hàng hóa

        public object SpecName { get; set; } // Tên vật tư hàng hóa

        public object Remark { get; set; } // Remark

        public object UnitCode { get; set; } // Đơn vị tính

        public object Qty { get; set; } // Số lượng bán

        public object UnitPrice { get; set; } // Giá bán

        public object ValInvoice { get; set; } // Tiền hàng

        public object ValTax { get; set; } // Tiền thuế hàng

        public object DiscountRate { get; set; } // Tỷ lệ chiết khấu

        public object ValDiscount { get; set; } // Tiền chiết khấu

        public object VATRateCode { get; set; } // Mã thuế suất

        public object VATRate { get; set; } // Thuế suất

        public object Idx { get; set; } // Số khung

        public object ProductID { get; set; } // Số khung

        public object InvoiceDCF5 { get; set; } // Số máy
    }
}
