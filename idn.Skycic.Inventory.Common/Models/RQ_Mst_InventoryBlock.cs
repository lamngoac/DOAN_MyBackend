using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_InventoryBlock : WARQBase
	{
		public string Rt_Cols_Mst_InventoryBlock { get; set; }

		public Mst_InventoryBlock Mst_InventoryBlock { get; set; }
	}
}
