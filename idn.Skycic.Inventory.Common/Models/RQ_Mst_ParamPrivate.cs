using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_ParamPrivate : WARQBase
	{
		public string Rt_Cols_Mst_ParamPrivate { get; set; }

		public Mst_ParamPrivate Mst_ParamPrivate { get; set; }
	}
}
