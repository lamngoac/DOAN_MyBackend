using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutFGDtl
    {

        public object IF_InvOutFGNo { get; set; }

        public object PartCode { get; set; }

        public object Qty { get; set; }

        public object IF_InvOutFGStatusDtl { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; } // Thời gian cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng
    }
}
