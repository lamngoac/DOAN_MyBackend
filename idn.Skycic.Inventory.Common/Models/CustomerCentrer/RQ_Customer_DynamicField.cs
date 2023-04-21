using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Customer_DynamicField : WARQBase
	{
		public string Rt_Cols_Customer_DynamicField { get; set; }
		
		public Customer_DynamicField Customer_DynamicField { get; set; }
        public List<Customer_DynamicField> Lst_Customer_DynamicField { get; set; }
    }
}
