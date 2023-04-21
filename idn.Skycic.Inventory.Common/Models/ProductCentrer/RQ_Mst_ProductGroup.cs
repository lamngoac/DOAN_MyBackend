using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
	public class RQ_Mst_ProductGroup : WARQBase
	{
		public string Rt_Cols_Mst_ProductGroup { get; set; }
		
		public Mst_ProductGroup Mst_ProductGroup { get; set; }

        public List<Mst_ProductGroup> Lst_Mst_ProductGroup { get; set; }

    }
}
