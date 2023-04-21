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
	public class OS_MstSvTVAN_Inv_InventoryBalanceSerial : ClientServiceBase<OS_MstSvTVAN_Inv_InventoryBalanceSerial>
	{
		public static OS_MstSvTVAN_Inv_InventoryBalanceSerial Instance
		{
			get
			{
				return GetInstance<OS_MstSvTVAN_Inv_InventoryBalanceSerial>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "InvInventoryBalanceSerial";
			}
		}

		public RT_Inv_InventoryBalanceSerial WA_Inv_InventoryBalanceSerial_Get(string strNetWorkUrl, RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
		{
			var result = MstSvRoute_PostData<RT_Inv_InventoryBalanceSerial, RQ_Inv_InventoryBalanceSerial>(strNetWorkUrl, "InvInventoryBalanceSerial", "WA_Inv_InventoryBalanceSerial_Get", new { }, objRQ_Inv_InventoryBalanceSerial);
			return result;
		}
	}
}
