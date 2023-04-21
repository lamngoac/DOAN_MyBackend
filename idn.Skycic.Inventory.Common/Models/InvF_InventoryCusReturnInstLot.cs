using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class InvF_InventoryCusReturnInstLot
	{
		public object IF_InvCusReturnNo { get; set; }

		public object InvCodeInActual { get; set; }

		public object ProductCodeRoot { get; set; }

		public object ProductCode { get; set; }

		public object ProductLotNo { get; set; }

		public object NetworkID { get; set; }

		public object Qty { get; set; }

		public object ProductionDate { get; set; }

		public object ExpiredDate { get; set; }

		public object ValDateExpired { get; set; }

		public object IF_InvCusReturnILStatus { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

	}
}
