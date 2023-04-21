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
    public class MasterServerCenterController : ApiControllerBase
    {
		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_MstSvCenter_Mst_Network_Create(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Mst_Network_Create";
			string strErrorCodeDefault = "WA_MstSv_Mst_Network_Create";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Mst_Network_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
											  ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSvCenter_Mst_NNT_AddByUserExist(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSvCenter_Mst_NNT_AddByUserExist";
			string strErrorCodeDefault = "WA_RptSvCenter_Mst_NNT_AddByUserExist";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion


			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSvCenter_Mst_NNT_AddByUserExist(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

	}
}
