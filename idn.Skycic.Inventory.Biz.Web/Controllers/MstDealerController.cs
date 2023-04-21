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
	public class MstDealerController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Dealer_Get";
			string strErrorCodeDefault = "WA_Mst_Dealer_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Dealer_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // RQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // RT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_Mst_Dealer_Create(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Dealer_Create";
			string strErrorCodeDefault = "WA_Mst_Dealer_Create";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Dealer_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // RT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_Mst_Dealer_Update(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Dealer_Update";
			string strErrorCodeDefault = "WA_Mst_Dealer_Update";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Dealer_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // objRT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}


		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_Mst_Dealer_Delete(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Dealer_Delete";
			string strErrorCodeDefault = "WA_Mst_Dealer_Delete";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Dealer_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // objRT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_RptSv_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_Dealer_Get";
			string strErrorCodeDefault = "WA_RptSv_Mst_Dealer_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_Dealer_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // RQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // RT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_RptSv_Mst_Dealer_Create(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Rpt_Mst_Dealer_Create";
			string strErrorCodeDefault = "WA_Rpt_Mst_Dealer_Create";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_Dealer_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // RT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_RptSv_Mst_Dealer_Update(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Rpt_Mst_Dealer_Update";
			string strErrorCodeDefault = "WA_Rpt_Mst_Dealer_Update";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_Dealer_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // objRT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Dealer> WA_RptSv_Mst_Dealer_Delete(RQ_Mst_Dealer objRQ_Mst_Dealer)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Dealer>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			RT_Mst_Dealer objRT_Mst_Dealer = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Rpt_Mst_Dealer_Delete";
			string strErrorCodeDefault = "WA_Rpt_Mst_Dealer_Delete";

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
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_Dealer_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Dealer // objRQ_Mst_Dealer
									   ////
					, out objRT_Mst_Dealer // objRT_Mst_Dealer
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
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Dealer>(objRT_Mst_Dealer);
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
				if (objRT_Mst_Dealer == null) objRT_Mst_Dealer = new RT_Mst_Dealer();
				objRT_Mst_Dealer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Dealer>(ex, objRT_Mst_Dealer);
				#endregion
			}
		}

	}
}
