using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_Mst_VATRate : WARQBase
    {

        public string Rt_Cols_Mst_VATRate { get; set; }

        public OS_PrdCenter_Mst_VATRate Mst_VATRate { get; set; }
    }
}
