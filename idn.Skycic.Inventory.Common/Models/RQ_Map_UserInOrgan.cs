using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Map_UserInOrgan : WARQBase
	{
		public string Rt_Cols_Map_UserInOrgan { get; set; }

		public Sys_User Sys_User { get; set; }
	}
}
