using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_ROInvoice
    {
        public object RONoSys { get; set; } // 

        public object ObjectType { get; set; } // Đối tượng xuất hóa đơn

        public object ObjectTypeName { get; set; } // Tên đối tượng

        public object InvoiceCode { get; set; } // Mã tra cứu

        public object InvoiceNo { get; set; } // Số tra cứu

        public object FormNo { get; set; } // Mẫu số

        public object Sign { get; set; } // Ký hiệu

        public object TInvoiceCode { get; set; } // Mã temp

        public object TInvoiceName { get; set; } // Tên tem

        public object InvoiceDateUTC { get; set; } // Ngày hóa đơn

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
