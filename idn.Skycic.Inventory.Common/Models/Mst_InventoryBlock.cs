using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_InventoryBlock
	{
		public object InvBlockCode { get; set; }

		public object ShelfCode { get; set; }

		public object NetworkID { get; set; }

		public object InvBlockDesc { get; set; }

		public object Length { get; set; }

		public object Width { get; set; }

		public object Height { get; set; }
		
		public object FlagActive { get; set; }

		public object Remark { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }
	}
}
