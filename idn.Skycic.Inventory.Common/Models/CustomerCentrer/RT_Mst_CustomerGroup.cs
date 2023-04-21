using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Mst_CustomerGroup : WARTBase
	{
		public List<Mst_CustomerGroup> Lst_Mst_CustomerGroup { get; set; }
        public List<Mst_CustomerGroupImages> Lst_Mst_CustomerGroupImages { get; set; }
    }
}
