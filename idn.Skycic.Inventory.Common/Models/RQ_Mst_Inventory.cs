using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Inventory : WARQBase
    {
        public string Rt_Cols_Mst_Inventory { get; set; }
        public string Rt_Cols_Mst_UserMapInventory { get; set; }

        public Mst_Inventory Mst_Inventory { get; set; }
        public Mst_UserMapInventory Mst_UserMapInventory { get; set; }

        public object UserCode { get; set; }
    }
}
