using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
	public class SysUserRT
	{
		public List<SysUser> SysUserLst { get; set; }

		public List<c_K_DT_SysInfo> c_K_DT_SysInfoLst { get; set; }

		public List<c_K_DT_SysError> c_K_DT_SysErrorLst { get; set; }

		public List<c_K_DT_SysWarning> c_K_DT_SysWarningLst { get; set; }
	}
}
