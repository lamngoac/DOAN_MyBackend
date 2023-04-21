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
    public class MstMapPartColorController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_MapPartColor> WA_Mst_MapPartColor_Get(RQ_Mst_MapPartColor objRQ_Mst_MapPartColor)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_MapPartColor>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			RT_Mst_MapPartColor objRT_Mst_MapPartColor = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_MapPartColor_Get";
			string strErrorCodeDefault = "WA_Mst_MapPartColor_Get";

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
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_MapPartColor_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MapPartColor // RQ_Mst_MapPartColor
											   ////
					, out objRT_Mst_MapPartColor // RT_Mst_MapPartColor
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
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_MapPartColor>(objRT_Mst_MapPartColor);
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
				if (objRT_Mst_MapPartColor == null) objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_MapPartColor>(ex, objRT_Mst_MapPartColor);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_MapPartColor> WA_Mst_MapPartColor_Create(RQ_Mst_MapPartColor objRQ_Mst_MapPartColor)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_MapPartColor>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			RT_Mst_MapPartColor objRT_Mst_MapPartColor = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_MapPartColor_Create";
			string strErrorCodeDefault = "WA_Mst_MapPartColor_Create";

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
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_MapPartColor_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MapPartColor // RQ_Mst_MapPartColor
											   ////
					, out objRT_Mst_MapPartColor // RT_Mst_MapPartColor
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
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_MapPartColor>(objRT_Mst_MapPartColor);
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
				if (objRT_Mst_MapPartColor == null) objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_MapPartColor>(ex, objRT_Mst_MapPartColor);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_MapPartColor> WA_Mst_MapPartColor_Update(RQ_Mst_MapPartColor objRQ_Mst_MapPartColor)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_MapPartColor>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			RT_Mst_MapPartColor objRT_Mst_MapPartColor = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_MapPartColor_Create";
			string strErrorCodeDefault = "WA_Mst_MapPartColor_Create";

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
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_MapPartColor_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MapPartColor // RQ_Mst_MapPartColor
											   ////
					, out objRT_Mst_MapPartColor // RT_Mst_MapPartColor
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
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_MapPartColor>(objRT_Mst_MapPartColor);
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
				if (objRT_Mst_MapPartColor == null) objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_MapPartColor>(ex, objRT_Mst_MapPartColor);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_MapPartColor> WA_Mst_MapPartColor_Delete(RQ_Mst_MapPartColor objRQ_Mst_MapPartColor)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_MapPartColor>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			RT_Mst_MapPartColor objRT_Mst_MapPartColor = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_MapPartColor_Delete";
			string strErrorCodeDefault = "WA_Mst_MapPartColor_Delete";

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
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_MapPartColor_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_MapPartColor // RQ_Mst_MapPartColor
											   ////
					, out objRT_Mst_MapPartColor // RT_Mst_MapPartColor
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
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_MapPartColor>(objRT_Mst_MapPartColor);
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
				if (objRT_Mst_MapPartColor == null) objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
				objRT_Mst_MapPartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_MapPartColor>(ex, objRT_Mst_MapPartColor);
				#endregion
			}
		}

	}
}
