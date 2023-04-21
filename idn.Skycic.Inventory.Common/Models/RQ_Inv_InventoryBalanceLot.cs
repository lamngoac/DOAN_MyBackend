using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryBalanceLot : WARQBase
    {
		public string Rt_Cols_Inv_InventoryBalanceLot { get; set; }
		public Inv_InventoryBalanceLot Inv_InventoryBalanceLot { get; set; }
	}
}
