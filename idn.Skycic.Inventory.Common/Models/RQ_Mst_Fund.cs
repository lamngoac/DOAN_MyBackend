using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Fund : WARQBase
	{
		public string Rt_Cols_Mst_Fund { get; set; }

		public Mst_Fund Mst_Fund { get; set; }
	}
}
