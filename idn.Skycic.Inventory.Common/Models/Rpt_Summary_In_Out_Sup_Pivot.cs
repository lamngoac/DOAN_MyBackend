using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Summary_In_Out_Sup_Pivot
    {
        public object DocNo { get; set; } // Số chứng từ
        public object ApprDateUTC { get; set; } // Ngày chứng từ
        public object InvCode { get; set; } // Mã kho 
        public object InvName { get; set; } // Tên kho 
        public object CustomerCodeSys { get; set; } // mã khách hàng hệ thống sinh
        public object CustomerCode { get; set; } // Mã khách hàng
        public object CustomerName { get; set; } // Tên khách hàng
		public object AreaCode { get; set; } // Vùng thị trường
		public object AreaName { get; set; } // Tên vùng thị trường
		public object ProvinceCode { get; set; } // Tỉnh
		public object ProvinceName { get; set; } // Tên tỉnh
		public object ProductCode { get; set; } // Mã sản phẩm hệ thống sinh
        public object ProductCodeUser { get; set; } // Mã sản phẩm
        public object ProductName { get; set; } // Tên sản phẩm
        public object InventoryAction { get; set; } // Loại phiếu
        public object InventoryActionDesc { get; set; } // Mô tả loại phiếu
        public object Inv_In_Out_Type { get; set; } // Loại nhập xuất
        public object Inv_In_Out_TypeDesc { get; set; } // Mô tả loại nhập xuất
        public object ProductGrpCode { get; set; } // Nhóm sản phẩm
        public object ProductGrpName { get; set; } // Tên nhóm
        public object ProductGrpDesc { get; set; } // Mô tả 
        public object Qty { get; set; } // Số lượng
        public object UnitCode { get; set; } // Đơn vị
        public object InvInType { get; set; } // Loại nhập
        public object InvInTypeName { get; set; } // Tên loại nhập
        public object InvOutType { get; set; } // Loại xuất
        public object InvOutTypeName { get; set; } // Tên loại xuất
    }
}
