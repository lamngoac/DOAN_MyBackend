using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_InventoryType : WARQBase
    {
        public string Rt_Cols_Mst_InventoryType { get; set; }

        public Mst_InventoryType Mst_InventoryType { get; set; }
    }
}
