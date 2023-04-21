using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS3A_TVAN_Invoice_Invoice : WARQBase
    {
        public string MST { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceDateUTCFrom { get; set; }
        public string InvoiceDateUTCTo { get; set; }
    }
}
