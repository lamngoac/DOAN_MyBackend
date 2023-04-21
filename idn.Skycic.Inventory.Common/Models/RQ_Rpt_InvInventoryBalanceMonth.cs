using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvInventoryBalanceMonth : WARQBase
    {
        public string InvBalMonthFrom { get; set; } // Từ kỳ
        public string InvBalMonthTo { get; set; } // Từ kỳ
        public Rpt_InvInventoryBalanceMonth Rpt_InventoryBalanceMonth { get; set; }
    }
}
