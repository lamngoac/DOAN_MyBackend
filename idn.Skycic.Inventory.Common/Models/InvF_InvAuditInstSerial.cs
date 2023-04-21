using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InvAuditInstSerial
    {
        public object IF_InvAudNo { get; set; } // Mã phiếu kiểm kê

        public object InvCodeInit { get; set; } // Vị trí lý thuyết

        public object InvCodeActual { get; set; } // Vị trí thực tế

        public object ProductCode { get; set; } // Mã sản phảm

        public object SerialNo { get; set; } // Mã Serial

        public object ProductLotNo { get; set; } // Mã Lot

        public object FlagExist { get; set; } //

        public object InventoryAction { get; set; } //

        public object IF_InvAuditISStatus { get; set; } //

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // người cập nhật cuối cùng 

    }
}
