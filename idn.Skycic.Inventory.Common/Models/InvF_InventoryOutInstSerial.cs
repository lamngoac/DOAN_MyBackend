using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutInstSerial
    {
        public object IF_InvOutNo { get; set; } // Số phiếu xuất

        public object InvCodeOutActual { get; set; } // Mã kho xuất

        public object ProductCodeRoot { get; set; } // Mã hàng hóa gốc

        public object ProductCode { get; set; } // Mã hàng hóa

        public object SerialNo { get; set; } // SerialNo

        public object NetworkID { get; set; } // NetworkID

        public object ProductLotNo { get; set; } // Số LOT

        public object IF_InvOutISStatus { get; set; } // Trạng thái phiếu xuất

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng
    }
}
