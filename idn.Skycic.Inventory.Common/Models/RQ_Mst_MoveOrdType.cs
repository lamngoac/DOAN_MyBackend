using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_MoveOrdType : WARQBase
	{
		public string Rt_Cols_Mst_MoveOrdType { get; set; }

		public Mst_MoveOrdType Mst_MoveOrdType { get; set; }
	}
}
