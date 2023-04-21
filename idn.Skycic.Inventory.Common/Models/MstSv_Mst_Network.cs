using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class MstSv_Mst_Network
	{
		public object NetworkID { get; set; }

		public object NetworkName { get; set; }

		public object GroupNetworkID { get; set; }

		public object CoreAddr { get; set; }

		public object PingAddr { get; set; }

		public object XSysAddr { get; set; }

		public object WSUrlAddr { get; set; }

		public object DBUrlAddr { get; set; }

		public object FlagActive { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

		////
		public object MST { get; set; }
		
		public object OrgIDSln { get; set; }
	}
}
