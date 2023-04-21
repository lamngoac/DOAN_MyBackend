using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Rpt_Summary_In_Out_Sup_PivotSpecial
	{
		public object CustomerCodeSys { get; set; } // mã khách hàng hệ thống sinh
		//public object CustomerCode { get; set; } // Mã khách hàng
		public object CustomerName { get; set; } // Tên khách hàng
		public object AreaCode { get; set; } // Vùng thị trường
		public object AreaName { get; set; } // Tên vùng thị trường
		public object TotalQty { get; set; } // Số lượng
		public object UnitCode { get; set; } // Đơn vị
	}
}
