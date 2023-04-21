using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OSDMS_InvF_InventoryCusReturnService : ClientServiceBase<OSDMS_InvF_InventoryCusReturnService>
	{
		public static OSDMS_InvF_InventoryCusReturnService Instance
		{
			get
			{
				return GetInstance<OSDMS_InvF_InventoryCusReturnService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "InvFInventoryCusReturn";
			}
		}

		public RT_OSDMS_InvF_InventoryCusReturn WA_Ord_OrderDLMix_UpdQtyReturn(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryCusReturn objRQ_OSDMS_InvF_InventoryCusReturn)
		{
			//strNetWorkUrl = @"http://localhost:1850/";
			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryCusReturn, RQ_OSDMS_InvF_InventoryCusReturn>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderDLMix_UpdQtyReturn", new { }, objRQ_OSDMS_InvF_InventoryCusReturn);
			return result;
		}

		public RT_OSDMS_InvF_InventoryCusReturn WA_Ord_OrderSOMix_UpdQtyReturn(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryCusReturn objRQ_OSDMS_InvF_InventoryCusReturn)
		{

			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryCusReturn, RQ_OSDMS_InvF_InventoryCusReturn>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderSOMix_UpdQtyReturn", new { }, objRQ_OSDMS_InvF_InventoryCusReturn);
			return result;
		}

		public RT_OSDMS_InvF_InventoryCusReturn WA_Ord_OrderSRMix_UpdQtyReturn(string strNetWorkUrl, RQ_OSDMS_InvF_InventoryCusReturn objRQ_OSDMS_InvF_InventoryCusReturn)
		{

			var result = MstSvRoute_PostData<RT_OSDMS_InvF_InventoryCusReturn, RQ_OSDMS_InvF_InventoryCusReturn>(strNetWorkUrl, "OrdOrder", "WA_Ord_OrderSRMix_UpdQtyReturn", new { }, objRQ_OSDMS_InvF_InventoryCusReturn);
			return result;
		}
	}
}
