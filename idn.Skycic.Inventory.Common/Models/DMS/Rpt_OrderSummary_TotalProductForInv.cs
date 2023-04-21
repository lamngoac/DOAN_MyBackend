using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.DMS
{
    public class Rpt_OrderSummary_TotalProductForInv
    {
        public object OrderNoSys { get; set; }
        public object OrderNo { get; set; }
        public object OrderType { get; set; }
        public object OrgID { get; set; }
        public object ProductCode { get; set; }
        public object QtyInvOutAvail { get; set; }
        public object CustomerCodeSys { get; set; } // Mã khách hàng
        public object QtyAppr { get; set; } // Số lượng duyệt từ đơn hàng

        // Thêm 1 số thông tin khác        
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object ProductCodeBase { get; set; }
        public object ProductCodeRoot { get; set; }
        public object FlagLo { get; set; }
        public object FlagCombo { get; set; }
        public object FlagSerial { get; set; }      
        public object UnitCode { get; set; }
        public object UPSell { get; set; }
        public object ValConvert { get; set; }
        public object QtyTotalOK { get; set; }
        //
    }
}
