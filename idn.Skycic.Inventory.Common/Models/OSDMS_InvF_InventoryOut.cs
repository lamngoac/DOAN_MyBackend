using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OSDMS_InvF_InventoryOut
	{
		public object IF_InvOutNo { get; set; } // Số phiếu xuất

		public object NetworkID { get; set; } // NetworkID

		public object OrgID { get; set; } // OrgID

		public object InvOutType { get; set; } // Loại nhập kho

		public object InvCodeOut { get; set; } // Mã kho xuất

		public object IF_InvAudNo { get; set; } // Số phiếu kiểm kê

		public object CustomerCode { get; set; } // Mã khách hàng

		public object OrderNo { get; set; } // Số đơn hàng

		public object OrderType { get; set; } // Loại đơn hàng

		public object UseReceive { get; set; } // Người nhận hàng

		public object TotalValOut { get; set; } // Tổng tiền hàng

		public object TotalValOutDesc { get; set; } // Tổng tiền giảm

		public object TotalValOutAfterDesc { get; set; } // Tiền trả nhà cung cấp

		public object CreateDTimeUTC { get; set; } // Thời điểm tạo

		public object CreateBy { get; set; } // Người tạo

		public object LUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

		public object LUBy { get; set; } // Người cập nhật cuối cùng

		public object ApprDTimeUTC { get; set; } // Thời điểm duyệt

		public object ApprBy { get; set; } // Người duyệt

		public object IF_InvOutStatus { get; set; } // Trạng thái phiếu xuất

		public object Remark { get; set; } // Ghi chú

		public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

		public object LogLUBy { get; set; } // Người cập nhật cuối cùng

		public object InvOutDate { get; set; } // Ngay xuat kho
	}
}
