using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_OSDMS_InvF_InventoryOut : WARTBase
	{
		public List<OSDMS_InvF_InventoryOut> Lst_InvOut_OrderMix { get; set; }

		public List<OSDMS_InvF_InventoryOutDtl> Lst_InvOut_OrderMixDtl { get; set; }
	}
}
