using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryInQR
    {
        public object IF_InvInNo { get; set; } // Số phiếu nhập

        public object QRCode { get; set; } // Mã xác thực

        public object NetworkID { get; set; } // NetworkID

        public object ProductCode { get; set; } // Mã sản phẩm

        public object BoxNo { get; set; } // Mã hộp

        public object CanNo { get; set; } // Mã thùng

        public object ProductLotNo { get; set; } // Số lô

        public object ShiftInCode { get; set; } // Ca

        public object UserKCS { get; set; } // KCS

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cung

		public object mp_ProductCode { get; set; } // Ma san pham

		public object mp_Productname { get; set; } // Ten san pham

		public object mp_ProductCodeUser { get; set; }
	}
}
