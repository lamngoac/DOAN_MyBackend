using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Inv_InventoryBoxUI : Inv_InventoryBox
    {
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object Qty { get; set; }
    }
}
