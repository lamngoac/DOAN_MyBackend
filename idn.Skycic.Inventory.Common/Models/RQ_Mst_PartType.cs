using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PartType : WARQBase
    {
        public string Rt_Cols_Mst_PartType { get; set; }

        public Mst_PartType Mst_PartType { get; set; }
    }
}
