using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PartColor : WARQBase
    {
        public string Rt_Cols_Mst_PartColor { get; set; }

        public Mst_PartColor Mst_PartColor { get; set; }
    }
}
