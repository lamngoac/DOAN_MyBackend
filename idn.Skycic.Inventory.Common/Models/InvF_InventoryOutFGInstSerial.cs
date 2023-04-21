using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutFGInstSerial
    {

        public string IF_InvOutFGNo { get; set; }

        public string PartCode { get; set; }

        public string SerialNo { get; set; }

        public string IF_InvOutFGISStatus { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; } // Thời gian cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng
    }
}
