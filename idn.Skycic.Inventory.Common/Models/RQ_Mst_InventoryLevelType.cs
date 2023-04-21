using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_InventoryLevelType : WARQBase
    {
        public string Rt_Cols_Mst_InventoryLevelType { get; set; }

        public Mst_InventoryLevelType Mst_InventoryLevelType { get; set; }
    }
}
