using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Mst_Customer : WARTBase
	{
		public List<Mst_Customer> Lst_Mst_Customer { get; set; }

		public List<UserOwner_Customer> Lst_UserOwner_Customer { get; set; }

        public List<Mst_CustomerInCustomerGroup> Lst_Mst_CustomerInCustomerGroup { get; set; }
        public List<Mst_CustomerInArea> Lst_Mst_CustomerInArea { get; set; }
    }
}
