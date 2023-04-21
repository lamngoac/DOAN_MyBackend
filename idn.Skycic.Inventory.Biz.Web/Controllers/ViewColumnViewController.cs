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
    public class ViewColumnViewController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnView> WA_View_ColumnView_Get(RQ_View_ColumnView objRQ_View_ColumnView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			RT_View_ColumnView objRT_View_ColumnView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnView_Get";
			string strErrorCodeDefault = "WA_View_ColumnView_Get";

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
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnView_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnView // RQ_View_ColumnView
										 ////
					, out objRT_View_ColumnView // RT_View_ColumnView
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
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnView>(objRT_View_ColumnView);
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
				if (objRT_View_ColumnView == null) objRT_View_ColumnView = new RT_View_ColumnView();
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnView>(ex, objRT_View_ColumnView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnView> WA_View_ColumnView_Create(RQ_View_ColumnView objRQ_View_ColumnView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			RT_View_ColumnView objRT_View_ColumnView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnView_Create";
			string strErrorCodeDefault = "WA_View_ColumnView_Create";

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
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnView_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnView // RQ_View_ColumnView
										 ////
					, out objRT_View_ColumnView // RT_View_ColumnView
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
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnView>(objRT_View_ColumnView);
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
				if (objRT_View_ColumnView == null) objRT_View_ColumnView = new RT_View_ColumnView();
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnView>(ex, objRT_View_ColumnView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnView> WA_View_ColumnView_Update(RQ_View_ColumnView objRQ_View_ColumnView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			RT_View_ColumnView objRT_View_ColumnView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnView_Create";
			string strErrorCodeDefault = "WA_View_ColumnView_Create";

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
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnView_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnView // RQ_View_ColumnView
										 ////
					, out objRT_View_ColumnView // RT_View_ColumnView
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
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnView>(objRT_View_ColumnView);
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
				if (objRT_View_ColumnView == null) objRT_View_ColumnView = new RT_View_ColumnView();
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnView>(ex, objRT_View_ColumnView);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnView> WA_View_ColumnView_Delete(RQ_View_ColumnView objRQ_View_ColumnView)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnView>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			RT_View_ColumnView objRT_View_ColumnView = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnView_Delete";
			string strErrorCodeDefault = "WA_View_ColumnView_Delete";

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
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnView_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnView // RQ_View_ColumnView
										 ////
					, out objRT_View_ColumnView // RT_View_ColumnView
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
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnView>(objRT_View_ColumnView);
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
				if (objRT_View_ColumnView == null) objRT_View_ColumnView = new RT_View_ColumnView();
				objRT_View_ColumnView.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnView>(ex, objRT_View_ColumnView);
				#endregion
			}
		}

	}
}
