using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Part : WARQBase
    {
        public string Rt_Cols_Mst_Part { get; set; }

        public Mst_Part Mst_Part { get; set; }
    }
}
