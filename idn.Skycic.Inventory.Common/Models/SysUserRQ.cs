using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
	public class SysUserRQ
	{
		public string Tid { get; set; }

		public string UserCode { get; set; }

		public string UserPassword { get; set; }

		public string FuncType { get; set; }

		public string Ft_RecordStart { get; set; }

		public string Ft_RecordCount { get; set; }

		public string Ft_WhereClause { get; set; }

		public string Rt_Cols_Sys_User { get; set; }

		public string Rt_Cols_Sys_UserInGroup { get; set; }

		public List<SysUser> SysUserLst { get; set; }
	}
}
