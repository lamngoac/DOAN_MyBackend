using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutInstLot
    {
        public object IF_InvOutNo { get; set; } // Số phiếu xuất

        public object InvCodeOutActual { get; set; } // Mã kho xuất

        public object ProductCodeRoot { get; set; } // Hàng hóa gốc

        public object ProductCode { get; set; } // Mã hàng hóa

        public object ProductLotNo { get; set; } // Số LOT

        public object NetworkID { get; set; } // NetworkID

        public object Qty { get; set; } // Qty

        public object ExpiredDate { get; set; } // ExpiredDate

        public object ProductionDate { get; set; } // ProductionDate

        public object IF_InvOutILStatus { get; set; } // Trạng thái

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng


        public object QtyInv { get; set; } //SL tồn kho(đã quy đổi)

        public object ValConvert { get; set; } // Cờ Serial
    }
}
