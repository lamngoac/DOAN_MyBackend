using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_TempGroup : WARTBase
    {
        public List<Invoice_TempGroup> Lst_Invoice_TempGroup { get; set; }
        public List<Invoice_TempGroupField> Lst_Invoice_TempGroupField { get; set; }
    }
}
