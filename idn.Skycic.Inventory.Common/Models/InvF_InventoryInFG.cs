using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryInFG
    {
        public object IF_InvInFGNo { get; set; } // Mã phiếu nhập

        public object FormInType { get; set; }

        public object InvInType { get; set; } // Kho nhập

        public object DLCode { get; set; } // đơn vị

        public object MST { get; set; } // đơn vị

        public object InvCode { get; set; } // Kho

        public object PMType { get; set; } // Nhóm sản phẩm

        public object CreateDTimeUTC { get; set; }

        public object CreateBy { get; set; }

        public object LUDTimeUTC { get; set; }

        public object LUBy { get; set; }

        public object ApprDTimeUTC { get; set; }

        public object ApprBy { get; set; }

        public object IF_InvInFGStatus { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; } // Thời gian cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng


    }
}
