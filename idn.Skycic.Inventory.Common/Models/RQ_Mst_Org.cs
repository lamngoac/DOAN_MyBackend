using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Org : WARQBase
	{
		public string Rt_Cols_Mst_Org { get; set; }

		public Mst_Org Mst_Org { get; set; }
	}
}
