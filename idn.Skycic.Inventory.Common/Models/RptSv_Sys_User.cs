using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RptSv_Sys_User
	{
		public string UserCode { get; set; }

		public string NetworkID { get; set; }

		public string DLCode { get; set; }

		public string UserName { get; set; }

		public string UserPassword { get; set; }

		public string UserPasswordNew { get; set; }

		public string PhoneNo { get; set; }

		//public string EMail { get; set; }

		//public object MST { get; set; }

		//public string OrganCode { get; set; }

		public string UserID { get; set; }

		//public string FlagDLAdmin { get; set; }

		public string md_DLBUCode { get; set; }

		public string md_DLBUPattern { get; set; }

		public string FlagSysAdmin { get; set; }

		//public string FlagNNTAdmin { get; set; }

		public string FlagActive { get; set; }

		public string LogLUDTimeUTC { get; set; }

		public string LogLUBy { get; set; }
	}
}
