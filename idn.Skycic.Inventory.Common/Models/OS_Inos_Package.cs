using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OS_Inos_Package
    {
        public object OrgID { get; set; }
        public object Id { get; set; }
        public object Name { get; set; }
        public object LicenseType { get; set; }
        public object Subscription { get; set; }
        public object Price { get; set; }
		public object ImageUrl { get; set; }
		public object IntroUrl { get; set; }
		public object Description { get; set; }
		public object Detail { get; set; }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
        public object IsDiscountable { get; set; }
    }
}
