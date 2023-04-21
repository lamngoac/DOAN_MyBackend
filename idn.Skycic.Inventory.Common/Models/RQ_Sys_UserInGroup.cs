using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Sys_UserInGroup : WARQBase
	{
		public Sys_Group Sys_Group { get; set; }

		public List<Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }
	}
}
