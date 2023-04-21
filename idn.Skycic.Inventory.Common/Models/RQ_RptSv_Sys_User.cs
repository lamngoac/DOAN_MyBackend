using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_RptSv_Sys_User : WARQBase
	{
		public string Rt_Cols_RptSv_Sys_User { get; set; }

		public string Rt_Cols_RptSv_Sys_UserInGroup { get; set; }

		public RptSv_Sys_User RptSv_Sys_User { get; set; }

	}
}
