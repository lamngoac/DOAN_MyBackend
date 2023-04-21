using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerGroup : WARQBase
	{
		public string Rt_Cols_Mst_CustomerGroup { get; set; }
        public string Rt_Cols_Mst_CustomerGroupImages { get; set; }
        public string Rt_Cols_Mst_CustomerInCustomerGroup { get; set; }


        public Mst_CustomerGroup Mst_CustomerGroup { get; set; }
        public List<Mst_CustomerGroup> Lst_Mst_CustomerGroup { get; set; }
        public List<Mst_CustomerGroupImages> Lst_Mst_CustomerGroupImages { get; set; }
        public List<Mst_CustomerInCustomerGroup> Lst_Mst_CustomerInCustomerGroup { get; set; }

    }
}
