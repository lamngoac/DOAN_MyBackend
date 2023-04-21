using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Prd_DynamicField : WARQBase
	{
		public string Rt_Cols_Prd_DynamicField { get; set; }
		
		public Prd_DynamicField Prd_DynamicField { get; set; }

        public List<Prd_DynamicField> Lst_Prd_DynamicField { get; set; }

    }
}
