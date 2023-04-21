using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Sys_User : WARTBase
	{
		public List<Sys_User> Lst_Sys_User { get; set; }

		public List<Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }

		public List<Sys_Access> Lst_Sys_Access { get; set; }
	}
}
