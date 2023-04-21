using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Product_CustomField : WARQBase
	{
		public string Rt_Cols_Product_CustomField { get; set; }
		
		public Product_CustomField Product_CustomField { get; set; }
        public List<Product_CustomField> Lst_Product_CustomField { get; set; }
    }
}
