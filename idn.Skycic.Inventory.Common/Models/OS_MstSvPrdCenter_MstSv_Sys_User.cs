using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OS_MstSvPrdCenter_MstSv_Sys_User
	{
		public object UserCode { get; set; }

        public object UserName { get; set; }

		public object UserPassword { get; set; }

		public object UserPasswordNew { get; set; }

		public object UserEmail { get; set; }

		public object CreateDTimeUTC { get; set; }

		public object EffDTimeUTC { get; set; }

		public object FlagActive { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }
	}
}
