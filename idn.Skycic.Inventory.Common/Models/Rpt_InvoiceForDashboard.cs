using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvoiceForDashboard
    {
        public object MST { get; set; } // Mã số thuế

        public object ReportType { get; set; }

        public object TotalAmontAfterVAT { get; set; }

        public object TotalQtyInvoice { get; set; }

        public object TotalQty { get; set; }

        public object TotalQtyIssued { get; set; }

        public object TotalQtyUsed { get; set; }

        public object QtyRemain { get; set; }
    }
}
