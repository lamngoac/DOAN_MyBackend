using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerInArea : WARQBase
	{
        public string Rt_Cols_Mst_CustomerInArea { get; set; }

        public Mst_Customer Mst_Customer { get; set; }
        public List<Mst_CustomerInArea> Lst_Mst_CustomerInArea { get; set; }
    }
}
