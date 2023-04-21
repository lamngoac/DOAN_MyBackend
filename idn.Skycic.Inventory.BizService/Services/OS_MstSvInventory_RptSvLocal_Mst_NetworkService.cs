using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_MstSvInventory_RptSvLocal_Mst_NetworkService : ClientServiceBase<OS_MstSvInventory_RptSvLocal_Mst_NetworkService>
	{
		public static OS_MstSvInventory_RptSvLocal_Mst_NetworkService Instance
		{
			get
			{
				return GetInstance<OS_MstSvInventory_RptSvLocal_Mst_NetworkService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MstSvMstNetwork";
			}
		}

		public RT_MstSv_Mst_Network WA_OS_RptSvLocal_Mst_Network_InsertMQ(string strUrl, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			var result = MstSvRoute_PostData<RT_MstSv_Mst_Network, RQ_MstSv_Mst_Network>(strUrl, "MstSvMstNetwork", "WA_RptSvLocal_Mst_Network_InsertMQ", new { }, objRQ_MstSv_Mst_Network);
			return result;
		}
	}
}
