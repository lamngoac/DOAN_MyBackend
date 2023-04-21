using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvFInventoryInFGSum : WARQBase
    {
        public string ApprDTimeUTCFrom { get; set; }
        public string ApprDTimeUTCTo { get; set; }
        public string PartCode { get; set; }

        public Rpt_InvFInventoryInFGSum Rpt_InvFInventoryInFGSum { get; set; }
    }
}
