using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_Ward
	{
		public object WardCode { get; set; }
		public object NetworkID { get; set; }
		public object ProvinceCode { get; set; }
		public object DistrictCode { get; set; }
		public object WardName { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
