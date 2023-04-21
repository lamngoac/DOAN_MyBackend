using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_Inos_OrgLicense : WARQBase
    {
        public string Rt_Cols_Invoice_licenseCreHist { get; set; }
        public string Rt_Cols_OS_Inos_OrgLicense { get; set; }
        public string Rt_Cols_OS_Inos_Package { get; set; }
        public string Rt_Cols_OS_Inos_OrgSolution { get; set; }
        public string Rt_Cols_OS_Inos_Modules { get; set; }
        public OS_Inos_OrgLicense OS_Inos_OrgLicense { get; set; }
        public OS_Inos_Package OS_Inos_Package { get; set; }
        public OS_Inos_OrgSolution OS_Inos_OrgSolution { get; set; }
        public OS_Inos_Modules OS_Inos_Modules { get; set; }
    }
}
