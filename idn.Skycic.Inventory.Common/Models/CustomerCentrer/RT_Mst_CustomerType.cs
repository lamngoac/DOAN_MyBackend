using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Mst_CustomerType : WARTBase
	{
		public List<Mst_CustomerType> Lst_Mst_CustomerType { get; set; }

        public List<Mst_CustomerTypeImages> Lst_Mst_CustomerTypeImages { get; set; }
    }
}
