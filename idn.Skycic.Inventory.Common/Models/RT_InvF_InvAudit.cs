using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_InvAudit : WARTBase
    {
        public List<InvF_InvAudit> Lst_InvF_InvAudit { get; set; }

        public List<InvF_InvAuditDtl> Lst_InvF_InvAuditDtl { get; set; }

        public List<InvF_InvAuditInstLot> Lst_InvF_InvAuditInstLot { get; set; }

        public List<InvF_InvAuditInstSerial> Lst_InvF_InvAuditInstSerial { get; set; }
    }
}
