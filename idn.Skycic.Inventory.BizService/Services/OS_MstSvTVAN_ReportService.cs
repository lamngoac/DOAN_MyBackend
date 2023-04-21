using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_MstSvTVAN_Report : ClientServiceBase<OS_MstSvTVAN_Report>
	{
		public static OS_MstSvTVAN_Report Instance
		{
			get
			{
				return GetInstance<OS_MstSvTVAN_Report>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "Report";
			}
		}

		public RT_Rpt_Inv_InventoryBalanceSerialForSearch WA_Rpt_Inv_InventoryBalanceSerialForSearch(string strNetWorkUrl, RQ_Rpt_Inv_InventoryBalanceSerialForSearch objRQ_Rpt_Inv_InventoryBalanceSerialForSearch)
		{
			var result = MstSvRoute_PostData<RT_Rpt_Inv_InventoryBalanceSerialForSearch, RQ_Rpt_Inv_InventoryBalanceSerialForSearch>(strNetWorkUrl, "Report", "WA_Rpt_Inv_InventoryBalanceSerialForSearch", new { }, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch);
			return result;
		}
	}
}
