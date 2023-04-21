using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Agent : WARQBase
	{
		public string Rt_Cols_Mst_Agent { get; set; }

		public Mst_Agent Mst_Agent { get; set; }
	}
}
