using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace idn.Skycic.Inventory.Common.Models
{
	public class c_K_DT_Sys
	{
		public List<c_K_DT_SysInfo> Lst_c_K_DT_SysInfo { get; set; }

		public List<c_K_DT_SysError> Lst_c_K_DT_SysError { get; set; }

		public List<c_K_DT_SysWarning> Lst_c_K_DT_SysWarning { get; set; }
	}
}
