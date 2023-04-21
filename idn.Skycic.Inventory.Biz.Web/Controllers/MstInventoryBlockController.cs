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
    public class MstInventoryBlockController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryBlock> WA_Mst_InventoryBlock_Get(RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryBlock>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			RT_Mst_InventoryBlock objRT_Mst_InventoryBlock = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryBlock_Get";
			string strErrorCodeDefault = "WA_Mst_InventoryBlock_Get";

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
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryBlock_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryBlock // RQ_Mst_InventoryBlock
										 ////
					, out objRT_Mst_InventoryBlock // RT_Mst_InventoryBlock
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
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryBlock>(objRT_Mst_InventoryBlock);
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
				if (objRT_Mst_InventoryBlock == null) objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryBlock>(ex, objRT_Mst_InventoryBlock);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryBlock> WA_Mst_InventoryBlock_Create(RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryBlock>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			RT_Mst_InventoryBlock objRT_Mst_InventoryBlock = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryBlock_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryBlock_Create";

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
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryBlock_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryBlock // RQ_Mst_InventoryBlock
										 ////
					, out objRT_Mst_InventoryBlock // RT_Mst_InventoryBlock
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
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryBlock>(objRT_Mst_InventoryBlock);
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
				if (objRT_Mst_InventoryBlock == null) objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryBlock>(ex, objRT_Mst_InventoryBlock);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryBlock> WA_Mst_InventoryBlock_Update(RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryBlock>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			RT_Mst_InventoryBlock objRT_Mst_InventoryBlock = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryBlock_Create";
			string strErrorCodeDefault = "WA_Mst_InventoryBlock_Create";

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
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryBlock_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryBlock // RQ_Mst_InventoryBlock
										 ////
					, out objRT_Mst_InventoryBlock // RT_Mst_InventoryBlock
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
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryBlock>(objRT_Mst_InventoryBlock);
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
				if (objRT_Mst_InventoryBlock == null) objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryBlock>(ex, objRT_Mst_InventoryBlock);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InventoryBlock> WA_Mst_InventoryBlock_Delete(RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InventoryBlock>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			RT_Mst_InventoryBlock objRT_Mst_InventoryBlock = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InventoryBlock_Delete";
			string strErrorCodeDefault = "WA_Mst_InventoryBlock_Delete";

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
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InventoryBlock_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InventoryBlock // RQ_Mst_InventoryBlock
										 ////
					, out objRT_Mst_InventoryBlock // RT_Mst_InventoryBlock
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
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InventoryBlock>(objRT_Mst_InventoryBlock);
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
				if (objRT_Mst_InventoryBlock == null) objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
				objRT_Mst_InventoryBlock.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InventoryBlock>(ex, objRT_Mst_InventoryBlock);
				#endregion
			}
		}

	}
}
