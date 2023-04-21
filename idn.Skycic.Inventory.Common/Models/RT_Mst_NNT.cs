using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Mst_NNT : WARTBase
    {
        public List<Mst_NNT> Lst_Mst_NNT { get; set; }

		public Inos_LicOrder Inos_LicOrder { get; set; }
	}
}
