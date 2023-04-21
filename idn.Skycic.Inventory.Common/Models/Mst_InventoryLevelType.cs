using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_InventoryLevelType
	{
		public object OrgID { get; set; }
	
		public object InvLevelType { get; set; }

        public object InvLevelTypeName { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
