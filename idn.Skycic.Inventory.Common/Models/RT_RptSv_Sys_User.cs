using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_RptSv_Sys_User : WARTBase
	{
		public List<RptSv_Sys_User> Lst_RptSv_Sys_User { get; set; }

        public List<RptSv_Sys_UserInGroup> Lst_RptSv_Sys_UserInGroup { get; set; }

        public List<RptSv_Sys_Access> Lst_RptSv_Sys_Access { get; set; }
    }
}
