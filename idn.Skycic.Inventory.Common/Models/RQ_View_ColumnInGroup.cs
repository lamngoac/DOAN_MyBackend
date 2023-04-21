using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_View_ColumnInGroup : WARQBase
	{
		public string Rt_Cols_View_ColumnInGroup { get; set; }

		public View_ColumnInGroup View_ColumnInGroup { get; set; }

		public List<View_ColumnInGroup> Lst_View_ColumnInGroup { get; set; }
	}
}
