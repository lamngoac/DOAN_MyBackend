using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Sys_Access : WARQBase
	{
		public string Rt_Cols_Sys_Access { get; set; }

		public Sys_Group Sys_Group { get; set; }

		public List<Sys_Access> Lst_Sys_Access { get; set; }
	}
}
