using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_Inos_OrgLicense
    {
        public object Id { get; set; }
        public object OrgID { get; set; }
        public object PackageId { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public object LicStatus { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
