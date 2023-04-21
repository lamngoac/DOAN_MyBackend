using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Inv_InventoryCartonUI : Inv_InventoryCarton
    {
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object Qty { get; set; }
    }
}
