using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OSDMS_InvF_InventoryCusReturnDtl
	{
		public object OrderMixNoSys { get; set; } // Ma don hang

		public object OrderMixType { get; set; } // Loai don hang

		public object OrgID { get; set; } // OrgID

		public object ProductCode { get; set; } // Ma hang hoa

		public object Qty { get; set; } // So luong tra lai

	}
}
