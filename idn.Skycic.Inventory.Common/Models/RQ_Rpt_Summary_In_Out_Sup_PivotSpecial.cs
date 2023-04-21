using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Rpt_Summary_In_Out_Sup_PivotSpecial : WARQBase
	{
		public Rpt_Summary_In_Out_Sup_PivotSpecial Rpt_Summary_In_Out_Sup_PivotSpecial { get; set; }
		public List<Mst_Product> Lst_Mst_Product { get; set; }
		public List<Mst_Inventory> Lst_Mst_Inventory { get; set; }
		public List<Mst_ProductGroup> Lst_Mst_ProductGroup { get; set; }
		public List<Mst_Customer> Lst_Mst_Customer { get; set; }

		public string ApprDTimeUTCFrom { get; set; }

		public string ApprDTimeUTCTo { get; set; }

		public string InventoryAction { get; set; } // In : Loại nhập/ Out:Loại xuất

        public object UserCode { get; set; } // 20201002. Mã user đăng nhập Dashboard
    }
}
