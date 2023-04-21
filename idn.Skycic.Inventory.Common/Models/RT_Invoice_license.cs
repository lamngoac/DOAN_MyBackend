using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_license : WARTBase
    {
        public List<Invoice_license> Lst_Invoice_license { get; set; }
        public List<Invoice_licenseCreHist> Lst_Invoice_licenseCreHist { get; set; }
    }
}
