using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerTypeImages : WARQBase
	{
		public string Rt_Cols_Mst_CustomerTypeImages { get; set; }
		
		public Mst_CustomerTypeImages Mst_CustomerTypeImages { get; set; }
	}
}
