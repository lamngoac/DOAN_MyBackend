using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SerRO_InvF_Transaction
    {
        public object RONoSys { get; set; }

        public object AutoID { get; set; }

        public object OrgID { get; set; }

        public object NetworkID { get; set; }

        public object ProductCode { get; set; }

        public object FunctionName { get; set; }

        public object RefType { get; set; }

        public object RefCode00 { get; set; }

        public object RefCode01 { get; set; }

        public object RefCode02 { get; set; }

        public object RefCode03 { get; set; }

        public object RefCode04 { get; set; }

        public object RefCode05 { get; set; }

        public object Qty { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
