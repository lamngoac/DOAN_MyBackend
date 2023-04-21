using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutQR
    {
        public object IF_InvOutNo { get; set; } // Số phiếu xuất

        public object QRCode { get; set; } // Mã tra cứu

        public object NetworkID { get; set; } // NetworkID

        public object ProductCode { get; set; } // Mã sản phẩm

        public object BoxNo { get; set; } // Mã hộp

        public object CanNo { get; set; } // Mã thùng

        public object QRType { get; set; } // Loại

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhập cuối cùng


        public object mp_ProductCodeUser { get; set; } // 


        public object mp_ProductName { get; set; } // 

    }
}
