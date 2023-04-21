using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Ward : WARQBase
	{
		public string Rt_Cols_Mst_Ward { get; set; }
		
		public Mst_Ward Mst_Ward { get; set; }
	}
}
