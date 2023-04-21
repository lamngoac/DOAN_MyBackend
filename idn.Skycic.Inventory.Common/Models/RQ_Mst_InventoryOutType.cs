using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_InventoryOutType : WARQBase
	{
		public string Rt_Cols_Mst_InventoryOutType { get; set; }

		public Mst_InventoryOutType Mst_InventoryOutType { get; set; }
	}
}
