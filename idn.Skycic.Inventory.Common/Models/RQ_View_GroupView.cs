using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_View_GroupView : WARQBase
	{
		public string Rt_Cols_View_GroupView { get; set; }

		public View_GroupView View_GroupView { get; set; }
	}
}
