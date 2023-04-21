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
    public class MstAgentController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Agent> WA_Mst_Agent_Get(RQ_Mst_Agent objRQ_Mst_Agent)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Agent>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			RT_Mst_Agent objRT_Mst_Agent = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Agent_Get";
			string strErrorCodeDefault = "WA_Mst_Agent_Get";

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
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Agent_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Agent // RQ_Mst_Agent
										 ////
					, out objRT_Mst_Agent // RT_Mst_Agent
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
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Agent>(objRT_Mst_Agent);
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
				if (objRT_Mst_Agent == null) objRT_Mst_Agent = new RT_Mst_Agent();
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Agent>(ex, objRT_Mst_Agent);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Agent> WA_Mst_Agent_Create(RQ_Mst_Agent objRQ_Mst_Agent)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Agent>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			RT_Mst_Agent objRT_Mst_Agent = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Agent_Create";
			string strErrorCodeDefault = "WA_Mst_Agent_Create";

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
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Agent_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Agent // RQ_Mst_Agent
										 ////
					, out objRT_Mst_Agent // RT_Mst_Agent
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
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Agent>(objRT_Mst_Agent);
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
				if (objRT_Mst_Agent == null) objRT_Mst_Agent = new RT_Mst_Agent();
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Agent>(ex, objRT_Mst_Agent);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Agent> WA_Mst_Agent_Update(RQ_Mst_Agent objRQ_Mst_Agent)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Agent>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			RT_Mst_Agent objRT_Mst_Agent = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Agent_Create";
			string strErrorCodeDefault = "WA_Mst_Agent_Create";

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
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Agent_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Agent // RQ_Mst_Agent
										 ////
					, out objRT_Mst_Agent // RT_Mst_Agent
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
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Agent>(objRT_Mst_Agent);
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
				if (objRT_Mst_Agent == null) objRT_Mst_Agent = new RT_Mst_Agent();
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Agent>(ex, objRT_Mst_Agent);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Agent> WA_Mst_Agent_Delete(RQ_Mst_Agent objRQ_Mst_Agent)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Agent>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			RT_Mst_Agent objRT_Mst_Agent = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Agent_Delete";
			string strErrorCodeDefault = "WA_Mst_Agent_Delete";

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
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Agent_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Agent // RQ_Mst_Agent
										 ////
					, out objRT_Mst_Agent // RT_Mst_Agent
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
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Agent>(objRT_Mst_Agent);
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
				if (objRT_Mst_Agent == null) objRT_Mst_Agent = new RT_Mst_Agent();
				objRT_Mst_Agent.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Agent>(ex, objRT_Mst_Agent);
				#endregion
			}
		}

	}
}
