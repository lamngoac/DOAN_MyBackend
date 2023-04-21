using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InvAuditDtlUI: InvF_InvAuditDtl
    {
        public object NetworkID { get; set; }
        public object InvName { get; set; }       
        public object Idx { get; set; }
        public object QtyRemain { get; set; }
        public object FlagAuditName { get; set; }
        public object InvCodeInitName { get; set; }
        public object InvCodeActualName { get; set; }

    }
}
