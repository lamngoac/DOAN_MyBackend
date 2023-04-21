using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryOutFGDtlUI : InvF_InventoryOutFGDtl
    {
        public object PartName { get; set; }
        
        public object PartNameFS { get; set; }
        
        public object CreateDTimeSv { get; set; }

        public object CreateBy { get; set; }

        public object FormOutType { get; set; }
    }
}
