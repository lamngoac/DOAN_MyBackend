using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvFInventoryOutFGSum : WARQBase
    {

        public string ApprDTimeUTCFrom { get; set; }
        public string ApprDTimeUTCTo { get; set; }
        public string MST { get; set; }
        public string AgentCode { get; set; }
        public string PartCode { get; set; }

        public Rpt_InvFInventoryOutFGSum Rpt_InvFInventoryOutFGSum { get; set; }
    }
}
