using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class MstSv_Inos_User
	{
		public object MST { get; set; }

		public object Email { get; set; }

		public object Name { get; set; }

		public object Password { get; set; }

		public object Language { get; set; }

		public object TimeZone { get; set; }

		public object UUID { get; set; }

		public object Id { get; set; }

		public object FlagEmailActivate { get; set; }

		public object FlagAdmin { get; set; }

		public object FlagEmailSend { get; set; }

		public object FlagActive { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }
	}
}
