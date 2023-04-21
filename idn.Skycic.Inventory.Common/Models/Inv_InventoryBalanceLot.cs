using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inv_InventoryBalanceLot
    {
		public object OrgID { get; set; }

		public object InvCode { get; set; }

		public object ProductCode { get; set; }

		public object ProductLotNo { get; set; }

		public object NetworkID { get; set; }

		public object QtyTotalOK { get; set; }

		public object QtyBlockOK { get; set; }

		public object QtyAvailOK { get; set; }

		public object QtyTotalNG { get; set; }

		public object QtyBlockNG { get; set; }

		public object QtyAvailNG { get; set; }

		public object ProductionDate { get; set; }

		public object ExpiredDate { get; set; }

		public object ValDateExpired { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }
	}
}
