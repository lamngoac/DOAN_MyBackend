using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_Sys_Group : WARTBase
	{
		public List<Sys_Group> Lst_Sys_Group { get; set; }

		public List<Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }
	}
}
