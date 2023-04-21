using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_InvoiceInput : WARTBase
    {
        public List<Invoice_InvoiceInput> Lst_Invoice_InvoiceInput { get; set; }
        public List<Invoice_InvoiceInputDtl> Lst_Invoice_InvoiceInputDtl { get; set; }
    }
}
