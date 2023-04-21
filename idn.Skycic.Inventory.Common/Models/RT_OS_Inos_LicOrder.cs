using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// //

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_OS_Inos_LicOrder : WARTBase
    {
        public Inos_DiscountCode Inos_DiscountCode { get; set; }

        public Inos_LicOrder Inos_LicOrder { get; set; }
    }
}
