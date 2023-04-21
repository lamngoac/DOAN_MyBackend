using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InvAuditInstLot
    {
        public object IF_InvAudNo { get; set; } // Mã phiếu kiểm kê

        public object InvCodeInit { get; set; } // Mã kho lý thuyết

        public object InvCodeActual { get; set; } //   Mã kho thực tế

        public object ProductCode { get; set; } // Mã sản phẩm

        public object ProductLotNo { get; set; } // Mã LOT

        public object QtyInit { get; set; } // Số lượng lý thuyết

        public object QtyActual { get; set; } //  Số lượng thực tế

        public object ProductionDate { get; set; } //  ngày sản xuất

        public object ExpiredDate { get; set; } //  Ngày hết hạn

        public object ValDateExpired { get; set; } //  Số ngày hết hạn

        public object FlagExist { get; set; } // 

        public object InventoryAction { get; set; } // 

        public object IF_InvAuditILStatus { get; set; } // 

        public object LogLUDTimeUTC { get; set; } //  Thời điểm cập nhật

        public object LogLUBy { get; set; } // Người cập nhật

    }
}
