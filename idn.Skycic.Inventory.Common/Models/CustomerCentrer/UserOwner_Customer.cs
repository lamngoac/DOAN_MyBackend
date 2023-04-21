using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class UserOwner_Customer
	{
		public object UserCode { get; set; }
		public object OrgID { get; set; }
		public object CustomerCodeSys { get; set; }
		public object NetworkID { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
