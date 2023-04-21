using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvoiceInvoice_ResultUsed : WARQBase
    {
        public string ReportDTimeFrom { get; set; }

        public string ReportDTimeTo { get; set; }

        public string InvoiceType { get; set; }

        public string strSign { get; set; }

        public string FormNo { get; set; }

        public string MST { get; set; }
    }
}
