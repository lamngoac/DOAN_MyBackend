using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_CustomerGroup
	{
		public object OrgID { get; set; }
		public object CustomerGrpCode { get; set; }
		public object NetworkID { get; set; }
		public object CustomerGrpCodeParent { get; set; }
		public object CustomerGrpBUCode { get; set; }
		public object CustomerGrpBUPattern { get; set; }
		public object CustomerGrpLevel { get; set; }
		public object CustomerGrpName { get; set; }
		public object CustomerGrpDesc { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
