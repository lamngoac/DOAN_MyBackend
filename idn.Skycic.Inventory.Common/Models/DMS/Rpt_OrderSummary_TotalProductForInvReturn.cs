using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.DMS
{
	public class Rpt_OrderSummary_TotalProductForInvReturn
	{
		public object OrderNoSys { get; set; }
		public object OrderNo { get; set; }
        public object OrderType { get; set; }
        public object OrgID { get; set; }
		public object ProductCode { get; set; }
		public object ProductCodeUser { get; set; }
		public object ProductName { get; set; }
		public object QtyAppr { get; set; }
        public object QtyInvOut { get; set; } // Số lượng Đã Xuất.
        public object QtyInvReturn { get; set; } // Số lượng Đã Trả.
        public object QtyInvReturnAvail { get; set; } // Số lượng Có thể Trả.

        public object CustomerCodeSys { get; set; }
		public object CustomerCode { get; set; }
		public object CustomerName { get; set; }
	}
}
