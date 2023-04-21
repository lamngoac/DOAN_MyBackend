using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_CustomerSource
	{
		public object OrgID { get; set; }
		public object CustomerSourceCode { get; set; }
		public object NetworkID { get; set; }
		public object CustomerSourceCodeParent { get; set; }
		public object CustomerSourceBUCode { get; set; }
		public object CustomerSourceBUPattern { get; set; }
		public object CustomerSourceLevel { get; set; }
		public object CustomerSourceName { get; set; }
		public object CustomerSourceDesc { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
    }
}
