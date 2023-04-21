using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_InvF_InventoryCusReturnInstLot : WARQBase
	{
		public string Rt_Cols_InvF_InventoryCusReturnInstLot { get; set; }
		public InvF_InventoryCusReturnInstLot InvF_InventoryCusReturnInstLot { get; set; }
	}
}
