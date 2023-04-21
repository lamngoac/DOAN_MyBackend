using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_TempPrintGroup : WARQBase
    {
        public string Rt_Cols_Mst_TempPrintGroup { get; set; }

        public Mst_TempPrintGroup Mst_TempPrintGroup { get; set; }
    }
}
