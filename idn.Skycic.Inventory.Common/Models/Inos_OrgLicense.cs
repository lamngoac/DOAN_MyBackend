using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inos_OrgLicense
    {
        public object Id { get; set; }
        public object OrgId { get; set; }
        public object PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Inos_LicenseStatuses Status { get; set; }
        public Inos_Package Package { get; set; }
    }
}
