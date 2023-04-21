using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerSourceImages : WARQBase
	{
		public string Rt_Cols_Mst_CustomerSourceImages { get; set; }
		
		public Mst_CustomerSourceImages Mst_CustomerSourceImages { get; set; }
	}
}
