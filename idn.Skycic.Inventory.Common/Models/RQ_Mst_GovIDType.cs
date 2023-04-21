using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_GovIDType : WARQBase
    {
        public string Rt_Cols_Mst_GovIDType { get; set; }

        public Mst_GovIDType Mst_GovIDType { get; set; }
    }
}
