using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_MstSv_Invoice_license : WARQBase
	{
		public string Rt_Cols_MstSv_Invoice_license { get; set; }
		
		public MstSv_Invoice_license MstSv_Invoice_license { get; set; }
	}
}
