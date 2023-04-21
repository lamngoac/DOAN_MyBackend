using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerSource : WARQBase
	{
		public string Rt_Cols_Mst_CustomerSource { get; set; }
        public string Rt_Cols_Mst_CustomerSourceImages { get; set; }
        public Mst_CustomerSource Mst_CustomerSource { get; set; } // Lst_Mst_CustomerSource
        public List<Mst_CustomerSource> Lst_Mst_CustomerSource { get; set; }
        public List<Mst_CustomerSourceImages> Lst_Mst_CustomerSourceImages { get; set; }
    }
}
