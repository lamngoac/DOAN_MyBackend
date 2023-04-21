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
    public class MstParamPrivateController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_ParamPrivate> WA_Mst_ParamPrivate_Get(RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_ParamPrivate>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			RT_Mst_ParamPrivate objRT_Mst_ParamPrivate = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_ParamPrivate_Get";
			string strErrorCodeDefault = "WA_Mst_ParamPrivate_Get";

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
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_ParamPrivate_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_ParamPrivate // RQ_Mst_ParamPrivate
											 ////
					, out objRT_Mst_ParamPrivate // RT_Mst_ParamPrivate
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
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_ParamPrivate>(objRT_Mst_ParamPrivate);
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
				if (objRT_Mst_ParamPrivate == null) objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_ParamPrivate>(ex, objRT_Mst_ParamPrivate);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_ParamPrivate> WA_Mst_ParamPrivate_Create(RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_ParamPrivate>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			RT_Mst_ParamPrivate objRT_Mst_ParamPrivate = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_ParamPrivate_Create";
			string strErrorCodeDefault = "WA_Mst_ParamPrivate_Create";

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
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_ParamPrivate_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_ParamPrivate // RQ_Mst_ParamPrivate
					////
					, out objRT_Mst_ParamPrivate // RT_Mst_ParamPrivate
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
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_ParamPrivate>(objRT_Mst_ParamPrivate);
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
				if (objRT_Mst_ParamPrivate == null) objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_ParamPrivate>(ex, objRT_Mst_ParamPrivate);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_ParamPrivate> WA_Mst_ParamPrivate_Update(RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_ParamPrivate>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			RT_Mst_ParamPrivate objRT_Mst_ParamPrivate = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_ParamPrivate_Create";
			string strErrorCodeDefault = "WA_Mst_ParamPrivate_Create";

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
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_ParamPrivate_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_ParamPrivate // RQ_Mst_ParamPrivate
											 ////
					, out objRT_Mst_ParamPrivate // RT_Mst_ParamPrivate
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
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_ParamPrivate>(objRT_Mst_ParamPrivate);
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
				if (objRT_Mst_ParamPrivate == null) objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_ParamPrivate>(ex, objRT_Mst_ParamPrivate);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_ParamPrivate> WA_Mst_ParamPrivate_Delete(RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_ParamPrivate>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			RT_Mst_ParamPrivate objRT_Mst_ParamPrivate = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_ParamPrivate_Delete";
			string strErrorCodeDefault = "WA_Mst_ParamPrivate_Delete";

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
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_ParamPrivate_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_ParamPrivate // RQ_Mst_ParamPrivate
											 ////
					, out objRT_Mst_ParamPrivate // RT_Mst_ParamPrivate
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
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_ParamPrivate>(objRT_Mst_ParamPrivate);
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
				if (objRT_Mst_ParamPrivate == null) objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
				objRT_Mst_ParamPrivate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_ParamPrivate>(ex, objRT_Mst_ParamPrivate);
				#endregion
			}
		}

	}
}
