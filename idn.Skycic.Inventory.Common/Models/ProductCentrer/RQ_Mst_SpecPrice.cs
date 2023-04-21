using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class RQ_Mst_SpecPrice : WARQBase
    {
        public string Rt_Cols_Mst_SpecPrice { get; set; }

        public Mst_SpecPrice Mst_SpecPrice { get; set; }
    }
}
