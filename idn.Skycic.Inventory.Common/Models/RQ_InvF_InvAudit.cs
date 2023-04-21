using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InvAudit : WARQBase
    {
        public string FlagIsCheckTotal { get; set; }
        public string Rt_Cols_InvF_InvAudit { get; set; }
        public string Rt_Cols_InvF_InvAuditDtl { get; set; }
        public string Rt_Cols_InvF_InvAuditInstSerial { get; set; }
        public string Rt_Cols_InvF_InvAuditInstLot { get; set; }
        public InvF_InvAudit InvF_InvAudit { get; set; }
        public List<InvF_InvAuditDtl> Lst_InvF_InvAuditDtl { get; set; }
        public List<InvF_InvAuditInstLot> Lst_InvF_InvAuditInstLot { get; set; }
        public List<InvF_InvAuditInstSerial> Lst_InvF_InvAuditInstSerial { get; set; }
    }
}
