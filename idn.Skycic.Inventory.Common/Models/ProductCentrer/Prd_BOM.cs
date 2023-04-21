using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Prd_BOM
	{
		public object OrgID { get; set; }
		public object ProductCode { get; set; }
		public object NetworkID { get; set; }
		public object OrgIDParent { get; set; }
		public object ProductCodeParent { get; set; }
		public object Qty { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }

		public object mp_ProductCode { get; set; }

		public object mp_ProductName { get; set; }

		public object mp_UPBuy { get; set; }

		public object mp_UPSell { get; set; }
        public object mp_UnitCode { get; set; }
        public object mp_ProductCodeUser { get; set; }
        public object mp_ProductCodeBase { get; set; }
        public object mp_ValConvert { get; set; }
    }
}
