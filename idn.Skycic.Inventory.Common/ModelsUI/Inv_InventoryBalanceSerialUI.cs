using idn.Skycic.Inventory.Common.Attributes;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Inv_InventoryBalanceSerialUI : Inv_InventoryBalanceSerial
    {
        public object OutDTimeFrom { get; set; }
        public object OutDTimeTo { get; set; }
        public object PackageDateFrom { get; set; }
        public object PackageDateTo { get; set; }
    }
}
