using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_NotifyType : WARQBase
	{
		public string Rt_Cols_Mst_NotifyType { get; set; }
		
		public Mst_NotifyType Mst_NotifyType { get; set; }
	}
}
