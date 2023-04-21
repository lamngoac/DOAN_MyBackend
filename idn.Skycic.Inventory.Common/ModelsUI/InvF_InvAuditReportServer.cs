using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InvAuditReportServer
    {
        public string NNTFullName { get; set; }
        public string NNTAddress { get; set; }
        public string NNTPhone { get; set; }
        public string IF_InvAudNo { get; set; }
        public string DatePrint { get; set; }
        public string MonthPrint { get; set; }
        public string YearPrint { get; set; }      
        public string Remark { get; set; }        
        public string CreateUserName { get; set; }
        public string LogoFilePath { get; set; }

        public List<InvF_InvAuditDtlReportServer> DataTable { get; set; }
    }
    public class InvF_InvAuditDtlReportServer
    {
        public string Idx { get; set; }
        public string ProductCodeUser { get; set; }
        public string ProductName { get; set; }
        public string UnitCode { get; set; }
        public string InvName { get; set; }
        public string QtyActual { get; set; }
        public string QtyInit { get; set; }
        public string QtyRemain { get; set; }
        public string FlagAuditName { get; set; }
        public string InvCodeInit { get; set; }
        public string InvCodeActual { get; set; }

        public string InvCodeInitName { get; set; }
        public string InvCodeActualName { get; set; }
    }
}
