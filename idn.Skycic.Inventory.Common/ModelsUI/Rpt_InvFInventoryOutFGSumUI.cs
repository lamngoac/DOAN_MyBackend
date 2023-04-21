using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_InvFInventoryOutFGSumUI : Rpt_InvFInventoryOutFGSum
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
