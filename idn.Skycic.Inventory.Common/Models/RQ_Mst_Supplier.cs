using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Supplier : WARQBase
	{
		public string Rt_Cols_Mst_Supplier { get; set; }

		public Mst_Supplier Mst_Supplier { get; set; }
	}
}
