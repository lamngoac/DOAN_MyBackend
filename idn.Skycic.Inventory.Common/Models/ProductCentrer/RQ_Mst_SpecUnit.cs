using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class RQ_Mst_SpecUnit : WARQBase
    {
        public string Rt_Cols_Mst_SpecUnit { get; set; }

        public Mst_SpecUnit Mst_SpecUnit { get; set; }
    }
}
