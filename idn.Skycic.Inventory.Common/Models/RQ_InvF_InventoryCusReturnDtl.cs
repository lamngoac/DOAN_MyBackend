using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_InvF_InventoryCusReturnDtl : WARQBase
	{
		public string Rt_Cols_InvF_InventoryCusReturnDtl { get; set; }
		public InvF_InventoryCusReturnDtl InvF_InventoryCusReturnDtl { get; set; }
	}
}
