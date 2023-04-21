using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Mst_Customer : WARQBase
    {
        public string Rt_Cols_Mst_Customer { get; set; }

        public DA_Mst_Customer Mst_Customer { get; set; }
    }
}
