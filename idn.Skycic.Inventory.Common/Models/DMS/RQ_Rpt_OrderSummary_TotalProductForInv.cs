using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.DMS
{
    public class RQ_Rpt_OrderSummary_TotalProductForInv : WARQBase
    {
        public string Rt_Cols_Rpt_OrderSummary_TotalProductForInv { get; set; }

        public Rpt_OrderSummary_TotalProductForInv Rpt_OrderSummary_TotalProductForInv { get; set; }
    }
}
