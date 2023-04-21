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
    public class ViewColumnInGroupController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnInGroup> WA_View_ColumnInGroup_Get(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnInGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			RT_View_ColumnInGroup objRT_View_ColumnInGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnInGroup_Get";
			string strErrorCodeDefault = "WA_View_ColumnInGroup_Get";

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
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnInGroup_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup // RQ_View_ColumnInGroup
										 ////
					, out objRT_View_ColumnInGroup // RT_View_ColumnInGroup
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
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnInGroup>(objRT_View_ColumnInGroup);
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
				if (objRT_View_ColumnInGroup == null) objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnInGroup>(ex, objRT_View_ColumnInGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnInGroup> WA_View_ColumnInGroup_Create(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnInGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			RT_View_ColumnInGroup objRT_View_ColumnInGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnInGroup_Create";
			string strErrorCodeDefault = "WA_View_ColumnInGroup_Create";

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
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnInGroup_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup // RQ_View_ColumnInGroup
										 ////
					, out objRT_View_ColumnInGroup // RT_View_ColumnInGroup
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
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnInGroup>(objRT_View_ColumnInGroup);
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
				if (objRT_View_ColumnInGroup == null) objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnInGroup>(ex, objRT_View_ColumnInGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnInGroup> WA_View_ColumnInGroup_Update(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnInGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			RT_View_ColumnInGroup objRT_View_ColumnInGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnInGroup_Create";
			string strErrorCodeDefault = "WA_View_ColumnInGroup_Create";

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
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnInGroup_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup // RQ_View_ColumnInGroup
										 ////
					, out objRT_View_ColumnInGroup // RT_View_ColumnInGroup
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
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnInGroup>(objRT_View_ColumnInGroup);
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
				if (objRT_View_ColumnInGroup == null) objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnInGroup>(ex, objRT_View_ColumnInGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnInGroup> WA_View_ColumnInGroup_Delete(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_View_ColumnInGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			RT_View_ColumnInGroup objRT_View_ColumnInGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnInGroup_Delete";
			string strErrorCodeDefault = "WA_View_ColumnInGroup_Delete";

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
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnInGroup_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup // RQ_View_ColumnInGroup
										 ////
					, out objRT_View_ColumnInGroup // RT_View_ColumnInGroup
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
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnInGroup>(objRT_View_ColumnInGroup);
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
				if (objRT_View_ColumnInGroup == null) objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnInGroup>(ex, objRT_View_ColumnInGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_View_ColumnInGroup> WA_View_ColumnInGroup_Save(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup)
		{
			#region // Temp:
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			RT_View_ColumnInGroup objRT_View_ColumnInGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_View_ColumnInGroup_Save";
			string strErrorCodeDefault = "WA_View_ColumnInGroup_Save";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Model", TJson.JsonConvert.SerializeObject(objRQ_Mst_Model)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_View_ColumnInGroup_Save(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_View_ColumnInGroup // objRQ_Mst_Model
											   // //
					, out objRT_View_ColumnInGroup // objRT_View_ColumnInGroup
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
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_View_ColumnInGroup>(objRT_View_ColumnInGroup);
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
				if (objRT_View_ColumnInGroup == null) objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
				objRT_View_ColumnInGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_View_ColumnInGroup>(ex, objRT_View_ColumnInGroup);
				#endregion
			}
		}

	}
}
