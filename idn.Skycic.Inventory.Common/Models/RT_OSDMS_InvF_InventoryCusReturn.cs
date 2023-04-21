using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_OSDMS_InvF_InventoryCusReturn : WARTBase
	{
		public List<OSDMS_InvF_InventoryCusReturn> Lst_InvReturn_OrderMix { get; set; }

		public List<OSDMS_InvF_InventoryCusReturnDtl> Lst_InvReturn_OrderMixDtl { get; set; }
	}
}
