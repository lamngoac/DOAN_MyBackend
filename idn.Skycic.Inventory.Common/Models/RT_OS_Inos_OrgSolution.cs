using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_OS_Inos_OrgSolution : WARTBase
    {
        public List<OS_Inos_OrgSolution> Lst_OS_Inos_OrgSolution { get; set; }
        public List<OS_Inos_Modules> Lst_OS_Inos_Modules { get; set; }
    }
}
