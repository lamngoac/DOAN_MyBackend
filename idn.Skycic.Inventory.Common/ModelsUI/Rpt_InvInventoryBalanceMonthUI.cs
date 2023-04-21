using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
   public class Rpt_InvInventoryBalanceMonthUI: Rpt_InvInventoryBalanceMonth
    {
        public string FromMonth { get; set; }
        public string ToMonth { get; set; }
    }
}
