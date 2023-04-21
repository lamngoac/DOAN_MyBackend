using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_InvInType : WARQBase 
	{
		public string Rt_Cols_Mst_InvInType { get; set; }

		public Mst_InvInType Mst_InvInType { get; set; }
	}
}
