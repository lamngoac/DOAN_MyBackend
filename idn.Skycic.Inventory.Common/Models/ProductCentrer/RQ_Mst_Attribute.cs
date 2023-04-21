using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
	public class RQ_Mst_Attribute : WARQBase
	{
		public string Rt_Cols_Mst_Attribute { get; set; }
		
		public Mst_Attribute Mst_Attribute { get; set; }

        public List<Mst_Attribute> Lst_Mst_Attribute { get; set; }
    }
}
