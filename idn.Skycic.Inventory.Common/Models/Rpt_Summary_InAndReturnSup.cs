using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Summary_InAndReturnSup
    {
        public object OrgID { get; set; }

        public object ProductCode { get; set; }

        public object ProductCodeUser { get; set; }

        public object CustomerCode { get; set; }

        public object CustomerCodeSys { get; set; }

        public object CustomerName { get; set; }

        public object TotalQtyIn { get; set; }

        public object TotalQtyReturn { get; set; }

        public object TotalQtyRemain { get; set; }
    }
}
