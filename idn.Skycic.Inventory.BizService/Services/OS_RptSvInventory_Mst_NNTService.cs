using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_RptSvInventory_Mst_NNTService : ClientServiceBase<OS_RptSvInventory_Mst_NNTService>
	{
		public static OS_RptSvInventory_Mst_NNTService Instance
		{
			get
			{
				return GetInstance<OS_RptSvInventory_Mst_NNTService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MstNNT";
			}
		}

		public RT_Mst_NNT WA_OS_RptSv_Mst_NNT_Create(string strUrl, RQ_Mst_NNT objRQ_Mst_NNT)
		{
			var result = MstSvRoute_PostData<RT_Mst_NNT, RQ_Mst_NNT>(strUrl, "MstNNT", "WA_RptSv_Mst_NNT_CreateForNetwork", new { }, objRQ_Mst_NNT);
			return result;
		}
	}
}
