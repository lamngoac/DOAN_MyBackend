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
    public class ViewGroupViewController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_GroupView> WA_View_GroupView_Get(RQ_View_GroupView objRQ_View_GroupView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_GroupView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			RT_View_GroupView objRT_View_GroupView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_GroupView_Get";
			string strErrorCodeDefault = "WA_View_GroupView_Get";

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
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_GroupView_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_GroupView // RQ_View_GroupView
										 ////
					, out objRT_View_GroupView // RT_View_GroupView
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
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_GroupView>(objRT_View_GroupView);
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
				if (objRT_View_GroupView == null) objRT_View_GroupView = new RT_View_GroupView();
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_GroupView>(ex, objRT_View_GroupView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_GroupView> WA_View_GroupView_Create(RQ_View_GroupView objRQ_View_GroupView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_GroupView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			RT_View_GroupView objRT_View_GroupView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_GroupView_Create";
			string strErrorCodeDefault = "WA_View_GroupView_Create";

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
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_GroupView_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_GroupView // RQ_View_GroupView
										 ////
					, out objRT_View_GroupView // RT_View_GroupView
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
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_GroupView>(objRT_View_GroupView);
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
				if (objRT_View_GroupView == null) objRT_View_GroupView = new RT_View_GroupView();
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_GroupView>(ex, objRT_View_GroupView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_GroupView> WA_View_GroupView_Update(RQ_View_GroupView objRQ_View_GroupView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_GroupView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			RT_View_GroupView objRT_View_GroupView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_GroupView_Create";
			string strErrorCodeDefault = "WA_View_GroupView_Create";

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
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_GroupView_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_GroupView // RQ_View_GroupView
										 ////
					, out objRT_View_GroupView // RT_View_GroupView
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
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_GroupView>(objRT_View_GroupView);
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
				if (objRT_View_GroupView == null) objRT_View_GroupView = new RT_View_GroupView();
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_GroupView>(ex, objRT_View_GroupView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_GroupView> WA_View_GroupView_Delete(RQ_View_GroupView objRQ_View_GroupView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_GroupView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			RT_View_GroupView objRT_View_GroupView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_GroupView_Delete";
			string strErrorCodeDefault = "WA_View_GroupView_Delete";

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
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_GroupView_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_GroupView // RQ_View_GroupView
										 ////
					, out objRT_View_GroupView // RT_View_GroupView
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
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_GroupView>(objRT_View_GroupView);
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
				if (objRT_View_GroupView == null) objRT_View_GroupView = new RT_View_GroupView();
				objRT_View_GroupView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_GroupView>(ex, objRT_View_GroupView);
				#endregion
			}
		}

	}
}
