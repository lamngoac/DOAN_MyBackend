using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryInInstSerial
    {
        public object IF_InvInNo { get; set; } // Mã phiếu nhập

        public object InvCodeInActual { get; set; } // Mã kho nhập

        public object ProductCode { get; set; } // Mã sản phẩm

        public object SerialNo { get; set; } // Serial

        public object NetworkID { get; set; } // NetworkID

        public object IF_InvInISStatus { get; set; } // Trạng thái serial 

        public object LogLUDTimeUTC { get; set; } //  Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng

        public object mp_ProductCode { get; set; } // Ma san pham

        public object mp_ProductName { get; set; } // Ten san pham 

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeBase { get; set; } // Ma san pham Base

        public object mp_FlagLot { get; set; } // Co Lot

        public object mp_FlagSerial { get; set; } // Co Serial

        public object mp_ValConvert { get; set; } // Giá trị quy đổi

    }
}
