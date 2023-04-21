using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_TempPrintType : WARQBase
    {
        public string Rt_Cols_Mst_TempPrintType { get; set; }

        public Mst_TempPrintType Mst_TempPrintType { get; set; }
    }
}
