using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_PrintOrder : WARQBase
    {
        public string Rt_Cols_Mst_PrintOrder { get; set; }
        public Mst_PrintOrder Mst_PrintOrder { get; set; }
    }
}
