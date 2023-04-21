using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_MstSvInventory_MstSv_Mst_NetworkService : ClientServiceBase<OS_MstSvInventory_MstSv_Mst_NetworkService>
	{
		public static OS_MstSvInventory_MstSv_Mst_NetworkService Instance
		{
			get
			{
				return GetInstance<OS_MstSvInventory_MstSv_Mst_NetworkService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MstSvMstNetwork";
			}
		}

		public RT_MstSv_Mst_Network WA_OS_MstSv_Mst_Network_Add(string strUrl, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			var result = MstSvRoute_PostData<RT_MstSv_Mst_Network, RQ_MstSv_Mst_Network>(strUrl, "MstSvMstNetwork", "WA_MstSv_Mst_Network_Add", new { }, objRQ_MstSv_Mst_Network);
			return result;
		}
	}
}
