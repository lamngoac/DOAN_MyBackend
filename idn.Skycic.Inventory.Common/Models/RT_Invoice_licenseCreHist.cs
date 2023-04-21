using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Invoice_licenseCreHist : WARTBase
    {
        public List<OS_Inos_OrgLicense> Lst_OS_Inos_OrgLicense { get; set; }
        public List<OS_Inos_Package> Lst_OS_Inos_Package { get; set; }
        ////
        public List<OS_Inos_OrgSolution> Lst_OS_Inos_OrgSolution { get; set; }
        public List<OS_Inos_Modules> Lst_OS_Inos_Modules { get; set; }
        ////
        public List<Invoice_licenseCreHist> Lst_Invoice_licenseCreHist { get; set; }
    }
}
