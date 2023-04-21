using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Mst_Product : WARTBase
	{
		public List<Mst_Product> Lst_Mst_Product { get; set; }
		
		public List<Mst_ProductImages> Lst_Mst_ProductImages { get; set; }

		public List<Mst_ProductFiles> Lst_Mst_ProductFiles { get; set; }

		public List<Prd_BOM> Lst_Prd_BOM { get; set; }

		public List<Prd_Attribute> Lst_Prd_Attribute { get; set; }
	}
}
