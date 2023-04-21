using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InvAuditPrint
    {
        public object NNTFullName { get; set; }
        public object NNTAddress { get; set; }
        public object NNTPhone { get; set; }
        public object IF_InvAudNo { get; set; }
        public object DatePrint { get; set; }
        public object MonthPrint { get; set; }
        public object YearPrint { get; set; }
        public object CustomerName { get; set; }
        public object OrderNo { get; set; }
        public object InvCodeAudit { get; set; }
        public object InvCodeAuditName { get; set; }
        public object Remark { get; set; }
        public object Idx { get; set; }        
        public object CreateUserName { get; set; }
        public object LogoFilePath { get; set; }

        public List<InvF_InvAuditDtlUI> Lst_InvF_InvAuditDtlUI { get; set; }
    }
}
