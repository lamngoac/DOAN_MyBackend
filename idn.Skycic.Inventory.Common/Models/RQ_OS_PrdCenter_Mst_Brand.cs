using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_Mst_Brand : WARQBase
    {

        public string Rt_Cols_Mst_Brand { get; set; }

        public OS_PrdCenter_Mst_Brand Mst_Brand { get; set; }
    }
}
