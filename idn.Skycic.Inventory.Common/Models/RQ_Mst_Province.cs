using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Province : WARQBase
    {
        public string Rt_Cols_Mst_Province { get; set; }

        public Mst_Province Mst_Province { get; set; }
    }
}
