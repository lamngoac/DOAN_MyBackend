using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MapDealerDiscountController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Map_DealerDiscount> WA_Map_DealerDiscount_Get(RQ_Map_DealerDiscount objRQ_Map_DealerDiscount)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Map_DealerDiscount>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Map_DealerDiscount.Tid);
			RT_Map_DealerDiscount objRT_Map_DealerDiscount = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Map_DealerDiscount_Get";
			string strErrorCodeDefault = "WA_Map_DealerDiscount_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Map_DealerDiscount.GwUserCode // strGwUserCode
					, objRQ_Map_DealerDiscount.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Map_DealerDiscount_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Map_DealerDiscount // objRQ_Map_DealerDiscount
											   ////
					, out objRT_Map_DealerDiscount // objRT_Map_DealerDiscount
					);

				if (CmUtils.CMyDataSet.HasError(mdsReturn))
				{
					throw CmUtils.CMyException.Raise(
						(string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
						, null
						, null
						);
				}
				#endregion

				// Return Good:
				objRT_Map_DealerDiscount.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Map_DealerDiscount>(objRT_Map_DealerDiscount);
			}
			catch (Exception ex)
			{
				#region // Catch of try:
				////
				TUtils.CProcessExc.Process(
					ref mdsReturn // mdsFinal
					, ex // exc
					, strErrorCodeDefault // strErrorCode
					, alParamsCoupleError.ToArray() // arrobjErrorParams
					);

				// Return Bad:
				if (objRT_Map_DealerDiscount == null) objRT_Map_DealerDiscount = new RT_Map_DealerDiscount();
				objRT_Map_DealerDiscount.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Map_DealerDiscount>(ex, objRT_Map_DealerDiscount);
				#endregion
			}
		}


	}
}
