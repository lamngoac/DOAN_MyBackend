using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_OS_MstSvTVAN_Invoice_Invoice : WARQBase
	{
		public string Rt_Cols_Invoice_Invoice { get; set; }

		public OS_MstSvTVAN_Invoice_Invoice Invoice_Invoice { get; set; }
	}
}
