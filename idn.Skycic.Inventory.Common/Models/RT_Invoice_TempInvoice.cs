using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_TempInvoice : WARTBase
    {
        public List<Invoice_TempInvoice> Lst_Invoice_TempInvoice { get; set; }

        public List<Invoice_TempCustomField> Lst_Invoice_TempCustomField { get; set; }
    }
}
