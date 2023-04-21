using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OSDMS_InvF_InventoryOutDtl
	{
		public object IF_InvOutNo { get; set; } // Số phiếu xuất

		public object InvCodeOutActual { get; set; } // Mã kho

		public object ProductCodeRoot { get; set; } // Mã hàng hóa gốc

		public object ProductCode { get; set; } // Mã hàng hóa

		public object NetworkID { get; set; } // NetworkID

		public object Qty { get; set; } // Số lượng

		public object UPOut { get; set; } // đơn giá xuất

		public object UPOutDesc { get; set; } // giảm giá

		public object ValOut { get; set; } // Tổng tiền hàng detail theo từng hàng hóa

		public object ValOutDesc { get; set; } // Tổng tiền giảm theo từng detail hàng hóa

		public object ValOutAfterDesc { get; set; } // Tổng tiền trả sau giảm giá

		public object UnitCode { get; set; } // đơn vị

		public object IF_InvOutStatusDtl { get; set; } // Trạng thái chi tiết

		public object Remark { get; set; } // Ghi chú

		public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

		public object LogLUBy { get; set; } // Người cập nhật cuối cùng


		public object FlagLot { get; set; } // Cờ LOT


		public object FlagSerial { get; set; } // Cờ Serial
		
		public object ProductType { get; set; } // 

		public object OrderMixNoSys { get; set; } // Ma don hang

		public object OrderMixType { get; set; } // Loai don hang

		public object OrgID { get; set; }
	}
}
