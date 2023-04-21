using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstInventoryLevelTypeController : ApiControllerBase
    {
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryLevelType> WA_Mst_InventoryLevelType_Get(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryLevelType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryLevelType_Get";
			string strErrorCodeDefault = "WA_Mst_InventoryLevelType_Get";

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
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryLevelType_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryLevelType // RQ_Mst_InventoryLevelType
												   ////
					, out objRT_Mst_InventoryLevelType // RT_Mst_InventoryLevelType
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
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryLevelType>(objRT_Mst_InventoryLevelType);
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
				if (objRT_Mst_InventoryLevelType == null) objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryLevelType>(ex, objRT_Mst_InventoryLevelType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryLevelType> WA_Mst_InventoryLevelType_Create(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryLevelType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryLevelType_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryLevelType_Create";

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
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryLevelType_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryLevelType // RQ_Mst_InventoryLevelType
												   ////
					, out objRT_Mst_InventoryLevelType // RT_Mst_InventoryLevelType
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
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryLevelType>(objRT_Mst_InventoryLevelType);
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
				if (objRT_Mst_InventoryLevelType == null) objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryLevelType>(ex, objRT_Mst_InventoryLevelType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryLevelType> WA_Mst_InventoryLevelType_Update(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryLevelType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryLevelType_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryLevelType_Create";

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
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryLevelType_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryLevelType // RQ_Mst_InventoryLevelType
												   ////
					, out objRT_Mst_InventoryLevelType // RT_Mst_InventoryLevelType
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
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryLevelType>(objRT_Mst_InventoryLevelType);
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
				if (objRT_Mst_InventoryLevelType == null) objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryLevelType>(ex, objRT_Mst_InventoryLevelType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryLevelType> WA_Mst_InventoryLevelType_Delete(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryLevelType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryLevelType_Delete";
			string strErrorCodeDefault = "WA_Mst_InventoryLevelType_Delete";

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
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryLevelType_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryLevelType // RQ_Mst_InventoryLevelType
												   ////
					, out objRT_Mst_InventoryLevelType // RT_Mst_InventoryLevelType
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
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryLevelType>(objRT_Mst_InventoryLevelType);
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
				if (objRT_Mst_InventoryLevelType == null) objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
				objRT_Mst_InventoryLevelType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryLevelType>(ex, objRT_Mst_InventoryLevelType);
				#endregion
			}
		}
	}
}
