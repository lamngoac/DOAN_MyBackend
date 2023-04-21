using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Rpt_InvoiceForDashboard : WARTBase
    {
        public List<Rpt_InvoiceForDashboard> Lst_Rpt_InvoiceForDashboard { get; set; }
        public List<Invoice_license> Lst_Invoice_license { get; set; }
    }
}
