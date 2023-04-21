using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Summary_InAndReturnSup : WARQBase
    {
        public Rpt_Summary_InAndReturnSup Rpt_Summary_InAndReturnSup { get; set; }

        public string ProductCodeUser { get; set; }

        public string ApprDTimeUTCFrom { get; set; }

        public string ApprDTimeUTCTo { get; set; }

        public string CustomerCodeSys { get; set; }
    }
}
