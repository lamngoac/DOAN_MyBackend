using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_ProductFiles
	{
		public object AutoID { get; set; }
		public object Idx { get; set; }
		public object OrgID { get; set; }
		public object ProductCode { get; set; }
		public object NetworkID { get; set; }
		public object ProductFileSpec { get; set; }
		public object ProductFilePath { get; set; }
		public object FlagIsFilePath { get; set; }
		public object ProductFileName { get; set; }
		public object ProductFileDesc { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
