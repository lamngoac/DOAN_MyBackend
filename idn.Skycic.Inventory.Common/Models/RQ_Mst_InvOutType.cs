using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_InvOutType : WARQBase
	{
		public string Rt_Cols_Mst_InvOutType { get; set; }

		public Mst_InvOutType Mst_InvOutType { get; set; }
	}
}
