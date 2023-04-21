using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_ProductType : WARQBase
	{
		public string Rt_Cols_Mst_ProductType { get; set; }
		
		public Mst_ProductType Mst_ProductType { get; set; }
	}
}
