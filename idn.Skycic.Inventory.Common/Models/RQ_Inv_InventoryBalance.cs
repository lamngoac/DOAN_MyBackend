using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryBalance : WARQBase
    {
		public string Rt_Cols_Inv_InventoryBalance { get; set; }
		//public string Rt_Cols_Inv_InventoryBalanceSerial { get; set; }
		//public string Rt_Cols_Inv_InventoryBalanceLot { get; set; }
		public Inv_InventoryBalance Inv_InventoryBalance { get; set; }
		public List<Inv_InventoryBalance> Lst_Inv_InventoryBalance { get; set; }
		//public List<Inv_InventoryBalanceLot> Lst_Inv_InventoryBalanceLot { get; set; }
		//public List<Inv_InventoryBalanceSerial> Lst_Inv_InventoryBalanceSerial { get; set; }
	}
}
