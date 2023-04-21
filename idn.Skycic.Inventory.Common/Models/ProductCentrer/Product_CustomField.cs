using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Product_CustomField
	{
		public object OrgID { get; set; }
		public object ProductCustomFieldCode { get; set; }
		public object NetworkID { get; set; }
		public object ProductCustomFieldName { get; set; }
		public object DBPhysicalType { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
