using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_InvF_InventoryReturnSup : WARTBase
	{
		public List<InvF_InventoryReturnSup> Lst_InvF_InventoryReturnSup { get; set; }

		public List<InvF_InventoryReturnSupDtl> Lst_InvF_InventoryReturnSupDtl { get; set; }

		public List<InvF_InventoryReturnSupInstLot> Lst_InvF_InventoryReturnSupInstLot { get; set; }

		public List<InvF_InventoryReturnSupInstSerial> Lst_InvF_InventoryReturnSupInstSerial { get; set; }
	}
}
