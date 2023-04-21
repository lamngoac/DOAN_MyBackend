using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_Area : WARQBase
	{
		public string Rt_Cols_Mst_Area { get; set; }
		
		public Mst_Area Mst_Area { get; set; }
        public List<Mst_Area> Lst_Mst_Area { get; set; }
    }
}
