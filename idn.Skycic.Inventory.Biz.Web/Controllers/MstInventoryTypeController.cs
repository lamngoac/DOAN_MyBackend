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
    public class MstInventoryTypeController : ApiControllerBase
    {
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryType> WA_Mst_InventoryType_Get(RQ_Mst_InventoryType objRQ_Mst_InventoryType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			RT_Mst_InventoryType objRT_Mst_InventoryType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryType_Get";
			string strErrorCodeDefault = "WA_Mst_InventoryType_Get";

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
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryType_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryType // RQ_Mst_InventoryType
											  ////
					, out objRT_Mst_InventoryType // RT_Mst_InventoryType
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
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryType>(objRT_Mst_InventoryType);
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
				if (objRT_Mst_InventoryType == null) objRT_Mst_InventoryType = new RT_Mst_InventoryType();
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryType>(ex, objRT_Mst_InventoryType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryType> WA_Mst_InventoryType_Create(RQ_Mst_InventoryType objRQ_Mst_InventoryType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			RT_Mst_InventoryType objRT_Mst_InventoryType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryType_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryType_Create";

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
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryType_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryType // RQ_Mst_InventoryType
											  ////
					, out objRT_Mst_InventoryType // RT_Mst_InventoryType
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
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryType>(objRT_Mst_InventoryType);
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
				if (objRT_Mst_InventoryType == null) objRT_Mst_InventoryType = new RT_Mst_InventoryType();
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryType>(ex, objRT_Mst_InventoryType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryType> WA_Mst_InventoryType_Update(RQ_Mst_InventoryType objRQ_Mst_InventoryType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			RT_Mst_InventoryType objRT_Mst_InventoryType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryType_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryType_Create";

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
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryType_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryType // RQ_Mst_InventoryType
											  ////
					, out objRT_Mst_InventoryType // RT_Mst_InventoryType
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
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryType>(objRT_Mst_InventoryType);
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
				if (objRT_Mst_InventoryType == null) objRT_Mst_InventoryType = new RT_Mst_InventoryType();
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryType>(ex, objRT_Mst_InventoryType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryType> WA_Mst_InventoryType_Delete(RQ_Mst_InventoryType objRQ_Mst_InventoryType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			RT_Mst_InventoryType objRT_Mst_InventoryType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryType_Delete";
			string strErrorCodeDefault = "WA_Mst_InventoryType_Delete";

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
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryType_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryType // RQ_Mst_InventoryType
											  ////
					, out objRT_Mst_InventoryType // RT_Mst_InventoryType
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
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryType>(objRT_Mst_InventoryType);
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
				if (objRT_Mst_InventoryType == null) objRT_Mst_InventoryType = new RT_Mst_InventoryType();
				objRT_Mst_InventoryType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryType>(ex, objRT_Mst_InventoryType);
				#endregion
			}
		}

	}
}
