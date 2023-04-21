using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_OSDMS_InvF_InventoryOut : WARQBase
	{
		public string Rt_Cols_InvF_InventoryOut { get; set; }
		public string Rt_Cols_InvF_InventoryOutDtl { get; set; }
		public OSDMS_InvF_InventoryOut InvOut_OrderMixDtl { get; set; }
		public List<OSDMS_InvF_InventoryOutDtl> Lst_InvOut_OrderMixDtl { get; set; }
	}
}
