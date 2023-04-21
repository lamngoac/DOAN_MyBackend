using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inos_LicOrderDetail
    {
      
        public object LicId { get; set; }
        public object PackageId { get; set; }
        public Inos_LicOrderTypes OrderType { get; set; }
        public Inos_OrgLicense Lic { get; set; }
    }
}
