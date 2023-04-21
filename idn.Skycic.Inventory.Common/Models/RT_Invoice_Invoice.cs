using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_Invoice : WARTBase
    {
        public List<Invoice_Invoice> Lst_Invoice_Invoice { get; set; }
        public List<Invoice_InvoiceDtl> Lst_Invoice_InvoiceDtl { get; set; }
        public List<Invoice_InvoicePrd> Lst_Invoice_InvoicePrd { get; set; }

		public List<Invoice_InvoiceCalc> Lst_Invoice_InvoiceCalc { get; set; }
	}
}
