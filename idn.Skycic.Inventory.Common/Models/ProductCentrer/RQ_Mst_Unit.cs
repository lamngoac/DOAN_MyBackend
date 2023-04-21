using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class RQ_Mst_Unit : WARQBase
    {
        public string Rt_Cols_Mst_Unit { get; set; }

        public Mst_Unit Mst_Unit { get; set; }
    }
}
