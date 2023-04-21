using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Param : WARQBase
	{
		public string Rt_Cols_Mst_Param { get; set; }

		public Mst_Param Mst_Param { get; set; }
	}
}
