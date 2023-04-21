using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Inv_InventoryBalanceLotUI: Inv_InventoryBalanceLot
    {
        public object Qty { get; set; } //Số lượng xuất
    }
}
