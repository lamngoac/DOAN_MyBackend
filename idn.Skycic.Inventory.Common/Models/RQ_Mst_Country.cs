using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Country : WARQBase
    {
        public string Rt_Cols_Mst_Country { get; set; }

        public Mst_Country Mst_Country { get; set; }
    }
}
