using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Department : WARQBase
    {
        public string Rt_Cols_Mst_Department { get; set; }

        public Mst_Department Mst_Department { get; set; }
    }
}
