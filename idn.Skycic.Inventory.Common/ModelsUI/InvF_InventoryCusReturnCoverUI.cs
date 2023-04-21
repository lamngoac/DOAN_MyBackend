using idn.Skycic.Inventory.Common.Models;
using System.Collections.Generic;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryCusReturnCoverUI : InvF_InventoryCusReturnCover
    {
        public object FlagCombo { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object Idx { get; set; }
        public object InvName { get; set; }
        public List<Mst_Product> Lst_Mst_ProductBase { get; set; }
    }
}
