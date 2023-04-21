using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_MstSv_Mst_Network : WARQBase
	{
		public string Rt_Cols_MstSv_Mst_Network { get; set; }

		public MstSv_Mst_Network MstSv_Mst_Network { get; set; }

		public MstSv_OrgInNetwork MstSv_OrgInNetwork { get; set; }
	}
}
