using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_InvFInventoryInFGSumUI : Rpt_InvFInventoryInFGSum
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreateDTimeUTCFrom { get; set; }
        public string CreateDTimeUTCTo { get; set; }
    }
}
