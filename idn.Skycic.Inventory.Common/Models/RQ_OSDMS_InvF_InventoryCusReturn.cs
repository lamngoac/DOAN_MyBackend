using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_OSDMS_InvF_InventoryCusReturn : WARQBase
	{
		public string Rt_Cols_OSDMS_InvF_InventoryCusReturn { get; set; }
		public string Rt_Cols_OSDMS_InvF_InventoryCusReturnDtl { get; set; }
		public OSDMS_InvF_InventoryCusReturn InvReturn_OrderMixDtl { get; set; }
		public List<OSDMS_InvF_InventoryCusReturnDtl> Lst_InvReturn_OrderMixDtl { get; set; }
	}
}
