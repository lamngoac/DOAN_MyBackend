using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Map_DealerDiscount : WARQBase
	{
		public string Rt_Cols_Map_DealerDiscount { get; set; }

		public Map_DealerDiscount Map_DealerDiscount { get; set; }
	}
}
