using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_MoveOrdDtl
    {
        public object IF_MONo { get; set; } // Số phiếu điều chuyển

        public object InvCodeOut { get; set; } // Mã kho xuất

        public object InvCodeIn { get; set; } // Mã kho nhập

        public object ProductCode { get; set; } // Mã hàng hóa

        public object NetworkID { get; set; }

        public object OrgID { get; set; }

        public object Qty { get; set; }

        public object UPMove { get; set; } // Đơn giá 

        public object ValMove { get; set; } // Thành tiền

        public object UnitCode { get; set; }

        public object IF_MOStatusDtl { get; set; } // Trạng thái phiếu

        public object Remark { get; set; } // ghi chú

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật

        public object LogLUBy { get; set; } // Người cập nhật

        public object mp_ProductCode { get; set; } // mp_ProductCode

        public object mp_ProductCodeUser { get; set; } // mp_ProductCode

        public object mp_ProductName { get; set; } // mp_ProductName

        public object mp_ProductNameEN { get; set; } // mp_ProductNameEN

        public object mp_FlagLot { get; set; } // mp_FlagLot

        public object mp_FlagSerial { get; set; } // mp_FlagSerial

        public object mp_ProductCodeBase { get; set; } // mp_ProductCodeBase

        public object mp_ValConvert { get; set; } // mp_ValConvert
    }
}
