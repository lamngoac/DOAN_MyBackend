using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PartMaterialType : WARQBase
    {
        public string Rt_Cols_Mst_PartMaterialType { get; set; }

        public Mst_PartMaterialType Mst_PartMaterialType { get; set; }
    }
}
