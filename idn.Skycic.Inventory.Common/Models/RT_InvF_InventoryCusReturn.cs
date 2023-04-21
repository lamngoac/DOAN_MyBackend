using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_InvF_InventoryCusReturn : WARTBase
	{
		public List<InvF_InventoryCusReturn> Lst_InvF_InventoryCusReturn { get; set; }

		public List<InvF_InventoryCusReturnCover> Lst_InvF_InventoryCusReturnCover { get; set; }

		public List<InvF_InventoryCusReturnDtl> Lst_InvF_InventoryCusReturnDtl { get; set; }

		public List<InvF_InventoryCusReturnInstLot> Lst_InvF_InventoryCusReturnInstLot { get; set; }

		public List<InvF_InventoryCusReturnInstSerial> Lst_InvF_InventoryCusReturnInstSerial { get; set; }
	}
}
