using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_OS3A_TVAN_Invoice_Invoice : WARTBase
    {
        public List<OS3A_TVAN_Invoice_Invoice> Lst_Invoice_Invoice { get; set; }

        public List<OS3A_TVAN_Invoice_InvoiceDtl> Lst_Invoice_InvoiceDtl { get; set; }
    }
}
