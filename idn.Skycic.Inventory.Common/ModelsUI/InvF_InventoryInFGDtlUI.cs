using idn.Skycic.Inventory.Common.Attributes;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryInFGDtlUI : InvF_InventoryInFGDtl
    {
        [DataColum]
        public object PartName { get; set; }

        [DataColum]
        public object PartNameFS { get; set; }

        [DataColum]
        public object CreateDTimeSv { get; set; }

        [DataColum]
        public object CreateBy { get; set; }

        [DataColum]
        public object FormInType { get; set; }
    }
}
