using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Mst_CustomerSource : WARTBase
	{
		public List<Mst_CustomerSource> Lst_Mst_CustomerSource { get; set; }

        public List<Mst_CustomerSourceImages> Lst_Mst_CustomerSourceImages { get; set; }
    }
}
