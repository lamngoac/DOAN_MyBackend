using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_InvF_InventoryReturnSup : WARQBase
	{
		public string Rt_Cols_InvF_InventoryReturnSup { get; set; }
		public string Rt_Cols_InvF_InventoryReturnSupDtl { get; set; }
		public string Rt_Cols_InvF_InventoryReturnSupInstLot { get; set; }
		public string Rt_Cols_InvF_InventoryReturnSupInstSerial { get; set; }
		public InvF_InventoryReturnSup InvF_InventoryReturnSup { get; set; }
		public List<InvF_InventoryReturnSupDtl> Lst_InvF_InventoryReturnSupDtl { get; set; }
		public List<InvF_InventoryReturnSupInstLot> Lst_InvF_InventoryReturnSupInstLot { get; set; }
		public List<InvF_InventoryReturnSupInstSerial> Lst_InvF_InventoryReturnSupInstSerial { get; set; }
		public object FlagIsCheckTotal { get; set; }
	}
}
