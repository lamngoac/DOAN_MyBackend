using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_BOMType : WARQBase
    {
        public string Rt_Cols_Mst_BOMType { get; set; }

        public Mst_BOMType Mst_BOMType { get; set; }
    }
}
