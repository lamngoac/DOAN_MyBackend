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
    public class RptSvSysUserController : ApiControllerBase
	{
		#region // RptSv_Sys_User:
		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_Login(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Login";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Login";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_Login(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_Get(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Get";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_GetForCurrentUser(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Get";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_GetForCurrentUser(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_ChangePassword(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_ChangePassword";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_ChangePassword";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_ChangePassword(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_Create(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Create";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Create";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_Update(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Update";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Update";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_RptSv_Sys_User> WA_RptSv_Sys_User_Delete(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Sys_User_Delete";
			string strErrorCodeDefault = "WA_RptSv_Sys_User_Delete";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Sys_User_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_RptSv_Sys_User // objRQ_RptSv_Sys_User
										   ////
					, out objRT_RptSv_Sys_User // RT_RptSv_Sys_User
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
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_RptSv_Sys_User>(objRT_RptSv_Sys_User);
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
				if (objRT_RptSv_Sys_User == null) objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_RptSv_Sys_User>(ex, objRT_RptSv_Sys_User);
				#endregion
			}
		}
        #endregion
    }
}
