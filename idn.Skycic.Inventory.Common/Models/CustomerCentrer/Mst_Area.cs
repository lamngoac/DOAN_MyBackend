using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_Area
	{
		public object OrgID { get; set; }
		public object AreaCode { get; set; }
		public object NetworkID { get; set; }
		public object AreaCodeParent { get; set; }
		public object AreaBUCode { get; set; }
		public object AreaBUPattern { get; set; }
		public object AreaLevel { get; set; }
		public object AreaName { get; set; }
		public object AreaDesc { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
