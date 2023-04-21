using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryOutDtlUI : InvF_InventoryOutDtl
    {        
        public object ProductCodeUser { get; set; } // Mã hàng hóa User            
        public object InvCodeMax { get; set; }
        public object QtyTotalOkMax { get; set; }
        public object Idx { get; set; }    
        public object ProductName { get; set; }

        public object ProductLotNo { get; set; }
        public object SerialNo { get; set; }
        public object FlagCombo { get; set; }
    }
}
