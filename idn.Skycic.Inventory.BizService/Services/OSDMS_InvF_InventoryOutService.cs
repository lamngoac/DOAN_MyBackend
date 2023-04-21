using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OSDMS_InvF_InventoryOutService : ClientServiceBase<OSDMS_InvF_InventoryOutService>
	{
		public static OSDMS_InvF_InventoryOutService Instance
		{
			get
			{
				return GetInstance<OSDMS_InvF_InventoryOutService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "InvFInventoryOut";
			}
		}

		public RT_OSDMS_InvF_InventoryOut WA_Ord_OrderDLMix_UpdQty(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryOut objRQ_OSDMS_InvF_InventoryOut)
		{
			//strNetWorkUrl = @"http://localhost:1850/";
			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryOut, RQ_OSDMS_InvF_InventoryOut>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderDLMix_UpdQty", new { }, objRQ_OSDMS_InvF_InventoryOut);
			return result;
		}

		public RT_OSDMS_InvF_InventoryOut WA_Ord_OrderSOMix_UpdQty(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryOut objRQ_OSDMS_InvF_InventoryOut)
		{
			
			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryOut, RQ_OSDMS_InvF_InventoryOut>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderSOMix_UpdQty", new { }, objRQ_OSDMS_InvF_InventoryOut);
			return result;
		}

		public RT_OSDMS_InvF_InventoryOut WA_Ord_OrderSRMix_UpdQty(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryOut objRQ_OSDMS_InvF_InventoryOut)
		{

			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryOut, RQ_OSDMS_InvF_InventoryOut>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderSRMix_UpdQty", new { }, objRQ_OSDMS_InvF_InventoryOut);
			return result;
		}
	}
}
