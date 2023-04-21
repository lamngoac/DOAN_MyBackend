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
    public class MstMoveOrdTypeController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_MoveOrdType> WA_Mst_MoveOrdType_Get(RQ_Mst_MoveOrdType objRQ_Mst_MoveOrdType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_MoveOrdType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MoveOrdType.Tid);
			RT_Mst_MoveOrdType objRT_Mst_MoveOrdType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_MoveOrdType_Get";
			string strErrorCodeDefault = "WA_Mst_MoveOrdType_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MoveOrdType.GwUserCode // strGwUserCode
					, objRQ_Mst_MoveOrdType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_MoveOrdType_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MoveOrdType // RQ_Mst_MoveOrdType
										 ////
					, out objRT_Mst_MoveOrdType // RT_Mst_MoveOrdType
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
				objRT_Mst_MoveOrdType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_MoveOrdType>(objRT_Mst_MoveOrdType);
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
				if (objRT_Mst_MoveOrdType == null) objRT_Mst_MoveOrdType = new RT_Mst_MoveOrdType();
				objRT_Mst_MoveOrdType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_MoveOrdType>(ex, objRT_Mst_MoveOrdType);
				#endregion
			}
		}

	}
}
