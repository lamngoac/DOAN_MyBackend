using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryBalanceSerial : WARQBase
    {
        public string Rt_Cols_Inv_InventoryBalanceSerial { get; set; }
		public string ObjectMixCode { get; set; }
		public Inv_InventoryBalanceSerial Inv_InventoryBalanceSerial { get; set; }
        public List<Inv_InventoryBalanceSerial> Lst_Inv_InventoryBalanceSerial { get; set; }
    }
}
