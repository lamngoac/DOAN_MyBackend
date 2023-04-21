using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PartUnit : WARQBase
    {
        public string Rt_Cols_Mst_PartUnit { get; set; }

        public Mst_PartUnit Mst_PartUnit { get; set; }
    }
}
