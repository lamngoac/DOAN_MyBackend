using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inos_Package
    {
        public object Id { get; set; }
        public object Name { get; set; }
        public Inos_LicTypes LicenseType { get; set; }
        public Inos_LicSubscriptions Subscription { get; set; }
        public object Price { get; set; }
        public object ImageUrl { get; set; }
        public object IntroUrl { get; set; }
        public object Description { get; set; }
        public object Detail { get; set; }
    }
}
