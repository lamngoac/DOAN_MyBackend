using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Prd_BOM : WARQBase
	{
		public string Rt_Cols_Prd_BOM { get; set; }
		
		public Prd_BOM Prd_BOM { get; set; }
	}
}
