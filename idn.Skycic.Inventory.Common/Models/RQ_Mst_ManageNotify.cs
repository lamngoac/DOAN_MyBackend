using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_ManageNotify : WARQBase
	{
		public string Rt_Cols_Mst_ManageNotify { get; set; }
		
		public Mst_ManageNotify Mst_ManageNotify { get; set; }
	}
}
