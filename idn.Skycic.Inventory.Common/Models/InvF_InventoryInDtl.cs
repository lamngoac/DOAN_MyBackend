using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryInDtl
    {
        public object IF_InvInNo { get; set; } // Mã phiếu nhập

        public object InvCodeInActual { get; set; } // Mã kho nhập thực tế

        public object ProductCode { get; set; } // Mã sản phẩm

        public object NetworkID { get; set; } // NetworkID

        public object Qty { get; set; } // Số lượng

        public object QtyReturn { get; set; } // Số lượng đã trả

        public object UPIn { get; set; } // đơn giá nhập

        public object UPInDesc { get; set; } // giá giảm

        public object ValInvIn { get; set; } // Tiền nhập kho

        public object ValInDesc { get; set; } // Tiền giảm

        public object ValInAfterDesc { get; set; } // Thành tiền sau giảm

        public object UnitCode { get; set; } // đơn vị

        public object IF_InvInStatusDtl { get; set; } // Trạng thái chi tiết phiếu nhập

        public object Remark { get; set; } // ghi chú

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng

		public object mp_ProductCode { get; set; } // Ma san pham

		public object mp_ProductName { get; set; } // Ten san pham 

		public object mp_ProductCodeUser { get; set; } 

		public object mp_ProductCodeBase { get; set; } // Ma san pham Base

		public object mp_FlagLot { get; set; } // Co Lot

		public object mp_FlagSerial { get; set; } // Co Serial

        public object mp_ValConvert { get; set; } // Giá trị quy đổi


        public object mii_InvCode { get; set; } // Mã kho

        public object mii_InvName { get; set; } // Tên kho

    }
}
