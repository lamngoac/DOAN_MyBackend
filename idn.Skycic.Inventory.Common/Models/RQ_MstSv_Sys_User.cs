using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_MstSv_Sys_User : WARQBase
	{
		public string Rt_Cols_MstSv_Sys_User { get; set; }

		public MstSv_Sys_User MstSv_Sys_User { get; set; }
	}
}
