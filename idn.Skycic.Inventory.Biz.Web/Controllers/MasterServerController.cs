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
    public class MasterServerController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Sys_User> WA_MstSv_Sys_User_Login(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
			RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Sys_User_Login";
			string strErrorCodeDefault = "WA_MstSv_Sys_User_Login";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Sys_User_Login(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Sys_User // objRQ_MstSv_Sys_User
										   ////
					, out objRT_MstSv_Sys_User // RT_MstSv_Sys_User
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
				objRT_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Sys_User>(objRT_MstSv_Sys_User);
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
				if (objRT_MstSv_Sys_User == null) objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
				objRT_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Sys_User>(ex, objRT_MstSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Sys_User> WA_MstSv_Sys_User_GetAccessToken(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
			RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Sys_User_GetAccessToken";
			string strErrorCodeDefault = "WA_MstSv_Sys_User_GetAccessToken";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Sys_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Sys_User_GetAccessToken(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Sys_User // objRQ_MstSv_Sys_User
										   ////
					, out objRT_MstSv_Sys_User // RT_MstSv_Sys_User
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
				objRT_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Sys_User>(objRT_MstSv_Sys_User);
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
				if (objRT_MstSv_Sys_User == null) objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
				objRT_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Sys_User>(ex, objRT_MstSv_Sys_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_Common> WA_MstSv_Seq_Common_Get(RQ_Seq_Common objRQ_Seq_Common)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
			RT_Seq_Common objRT_Seq_Common = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Seq_Common_Get";
			string strErrorCodeDefault = "WA_MstSv_Seq_Common_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_Common.GwUserCode // strGwUserCode
					, objRQ_Seq_Common.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mstv_Seq_Common_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_Common // objRQ_Seq_Common
									   ////
					, out objRT_Seq_Common // RT_Seq_Common
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
				objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_Common>(objRT_Seq_Common);
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
				if (objRT_Seq_Common == null) objRT_Seq_Common = new RT_Seq_Common();
				objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_Common>(ex, objRT_Seq_Common);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Inos_User> WA_MstSv_Inos_User_Activate(RQ_MstSv_Inos_User objRQ_MstSv_Inos_User)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Inos_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_User.Tid);
			RT_MstSv_Inos_User objRT_MstSv_Inos_User = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Inos_User_Activate";
			string strErrorCodeDefault = "WA_MstSv_Inos_User_Activate";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Inos_User", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Inos_User)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Inos_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Inos_User.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Inos_User_Activate(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Inos_User // objRQ_MstSv_Inos_User
										   ////
					, out objRT_MstSv_Inos_User // RT_MstSv_Inos_User
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
				objRT_MstSv_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Inos_User>(objRT_MstSv_Inos_User);
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
				if (objRT_MstSv_Inos_User == null) objRT_MstSv_Inos_User = new RT_MstSv_Inos_User();
				objRT_MstSv_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Inos_User>(ex, objRT_MstSv_Inos_User);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Inos_Org> WA_MstSv_Inos_Org_BuildAndCreate(RQ_MstSv_Inos_Org objRQ_MstSv_Inos_Org)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Inos_Org>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_Org.Tid);
			RT_MstSv_Inos_Org objRT_MstSv_Inos_Org = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Inos_Org_BuildAndCreate";
			string strErrorCodeDefault = "WA_MstSv_Inos_Org_BuildAndCreate";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Inos_Org", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Inos_Org)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Inos_Org.GwUserCode // strGwUserCode
					, objRQ_MstSv_Inos_Org.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Inos_Org_BuildAndCreate(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Inos_Org // objRQ_MstSv_Inos_Org
											////
					, out objRT_MstSv_Inos_Org // RT_MstSv_Inos_Org
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
				objRT_MstSv_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Inos_Org>(objRT_MstSv_Inos_Org);
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
				if (objRT_MstSv_Inos_Org == null) objRT_MstSv_Inos_Org = new RT_MstSv_Inos_Org();
				objRT_MstSv_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Inos_Org>(ex, objRT_MstSv_Inos_Org);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_OrgInNetwork> WA_MstSv_OrgInNetwork_GetOrgIDSln(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_OrgInNetwork>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_OrgInNetwork_GetOrgIDSln";
			string strErrorCodeDefault = "WA_MstSv_OrgInNetwork_GetOrgIDSln";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_OrgInNetwork", TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_OrgInNetwork_GetOrgIDSln(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork // objRQ_MstSv_OrgInNetwork
											////
					, out objRT_MstSv_OrgInNetwork // RT_MstSv_OrgInNetwork
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
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_OrgInNetwork>(objRT_MstSv_OrgInNetwork);
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
				if (objRT_MstSv_OrgInNetwork == null) objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_OrgInNetwork>(ex, objRT_MstSv_OrgInNetwork);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_OrgInNetwork> WA_MstSv_OrgInNetwork_GetByOrgIDSln(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_OrgInNetwork>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_OrgInNetwork_GetByOrgIDSln";
			string strErrorCodeDefault = "WA_MstSv_OrgInNetwork_GetByOrgIDSln";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_OrgInNetwork", TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_OrgInNetwork_GetByOrgIDSln(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork // objRQ_MstSv_OrgInNetwork
											   ////
					, out objRT_MstSv_OrgInNetwork // RT_MstSv_OrgInNetwork
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
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_OrgInNetwork>(objRT_MstSv_OrgInNetwork);
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
				if (objRT_MstSv_OrgInNetwork == null) objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_OrgInNetwork>(ex, objRT_MstSv_OrgInNetwork);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Map_Network_SysOutSide> WA_Map_Network_SysOutSide_GetBySysOS(RQ_Map_Network_SysOutSide objRQ_Map_Network_SysOutSide)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Map_Network_SysOutSide>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Map_Network_SysOutSide.Tid);
			RT_Map_Network_SysOutSide objRT_Map_Network_SysOutSide = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_Map_Network_SysOutSide_GetBySysOS";
			string strErrorCodeDefault = "WA_OS_Map_Network_SysOutSide_GetBySysOS";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Map_Network_SysOutSide", TJson.JsonConvert.SerializeObject(objRQ_Map_Network_SysOutSide)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Map_Network_SysOutSide.GwUserCode // strGwUserCode
					, objRQ_Map_Network_SysOutSide.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Map_Network_SysOutSide_GetBySysOS(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Map_Network_SysOutSide // objRQ_Map_Network_SysOutSide
											   ////
					, out objRT_Map_Network_SysOutSide // RT_Map_Network_SysOutSide
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
				objRT_Map_Network_SysOutSide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Map_Network_SysOutSide>(objRT_Map_Network_SysOutSide);
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
				if (objRT_Map_Network_SysOutSide == null) objRT_Map_Network_SysOutSide = new RT_Map_Network_SysOutSide();
				objRT_Map_Network_SysOutSide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Map_Network_SysOutSide>(ex, objRT_Map_Network_SysOutSide);
				#endregion
			}
		}

	}
}
