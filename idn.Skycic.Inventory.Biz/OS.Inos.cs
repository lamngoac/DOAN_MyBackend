using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Linq;
using System.Threading;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TDAL = EzDAL.MyDB;
using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using inos.common.Constants;
using inos.common.Model;
using inos.common.Service;
using OSiNOSSv = inos.common.Service;
using System.Diagnostics;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Inos:

		private void Inos_User_CreateUserX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_RegisterForNetworkX";
			//string strErrorCodeDefault = TError.ErridnDMS.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "strPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			dsData = new DataSet();

			////
			//InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel();
			//{
			//    objInosCreateUserModel.Email = string.Format("{0}", objEmail).Trim();
			//    objInosCreateUserModel.Name = string.Format("{0}", objName).Trim();
			//    objInosCreateUserModel.Password = string.Format("{0}", objPassword).Trim();
			//    objInosCreateUserModel.Language = string.Format("{0}", objLanguage).Trim();
			//    objInosCreateUserModel.TimeZone = Convert.ToInt32(objTimeZone);
			//}

			AccountService objAccountService = new AccountService(null);
			objAccountService.AccessToken = strAccessToken;

			InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel()
			{
				Email = string.Format("{0}", objEmail).Trim(),
				Name = string.Format("{0}", objName).Trim(),
				Password = string.Format("{0}", objPassword).Trim(),
				Language = string.Format("{0}", objLanguage).Trim(),
				TimeZone = Convert.ToInt32(objTimeZone),
			};
			//objAccountService.CreateUser(objInosCreateUserModel);
			#endregion

			#region // Call Service:
			alParamsCoupleError.AddRange(new object[]{
				"objInosCreateUserModel", TJson.JsonConvert.SerializeObject(objInosCreateUserModel)
				});

			try
			{
				////
				//AccountService objAccountService = new AccountService(null);

				var ret = objAccountService.CreateUser(objInosCreateUserModel);

				#region // Fill Data:
				{
					// //
					//              string strSqlGen_Sys_User = CmUtils.StringUtils.Replace(@"
					//	---- Sys_User:
					//	select
					//		t.UserCode 
					//		, t.MST
					//		, t.Email
					//		, '@strUUID' UUID
					//	from Sys_User t
					//	where (1=1)
					//		and t.UserCode = '@strUserCode'
					//	;
					//"
					//                  , "@strUserCode", strEmail
					//                  , "@strUUID", ret.UUID
					//                  );

					//              DataTable dt_Sys_User = _cf.db.ExecQuery(strSqlGen_Sys_User).Tables[0];
					//              dt_Sys_User.TableName = "Sys_User";

					//              dsData.Tables.Add(dt_Sys_User.Copy());
				}
				#endregion

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_Register
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}
		public DataSet OS_Inos_Package_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			//// Return:
			, string strRt_Cols_OS_Inos_Package
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "OS_Inos_Package_Get";
			string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_Package_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_Package", strRt_Cols_OS_Inos_Package
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				//// Refine:
				bool bGet_OS_Inos_Package = (strRt_Cols_OS_Inos_Package != null && strRt_Cols_OS_Inos_Package.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

				#endregion

				#region // Sys_Solution: Get.
				////
				DataTable dtDB_Sys_Solution = null;
				{
					// GetInfo:
					dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
						_cf.db // db
						, "Sys_Solution" // strTableName
						, "top 1 *" // strColumnList
						, "" // strClauseOrderBy
						, "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
						);
				}
				#endregion

				#region // Get Data:
				DataSet dsGetData = null;

				if (bGet_OS_Inos_Package)
				{
					// //
					Inos_LicService_GetAllPackagesX_New20191113(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
						////
						, out dsGetData // dsData
						);
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);

				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Rollback and Release resources:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
				TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

				// Write ReturnLog:
				_cf.ProcessBizReturn_OutSide(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_OS_Inos_Package_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			//// Return:
			, string strRt_Cols_OS_Inos_Package
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "OS_Inos_Package_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_OS_Inos_Package_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_Package", strRt_Cols_OS_Inos_Package
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				//// Refine:
				bool bGet_OS_Inos_Package = (strRt_Cols_OS_Inos_Package != null && strRt_Cols_OS_Inos_Package.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

				#endregion

				#region // Sys_Solution: Get.
				////
				DataTable dtDB_Sys_Solution = null;
				{
					// GetInfo:
					dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
						_cf.db // db
						, "Sys_Solution" // strTableName
						, "top 1 *" // strColumnList
						, "" // strClauseOrderBy
						, "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
						);
				}
				#endregion

				#region // Get Data:
				DataSet dsGetData = null;

				if (bGet_OS_Inos_Package)
				{
					// //
					Inos_LicService_GetAllPackagesX_New20191113(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
																	////
						, out dsGetData // dsData
						);
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);

				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Rollback and Release resources:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
				TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

				// Write ReturnLog:
				_cf.ProcessBizReturn_OutSide(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet OS_Inos_Org_GetMyOrgList(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			//// Return:
			, string strRt_Cols_OS_Inos_Org
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "OS_Inos_Org_GetMyOrgList";
			string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_Org_GetMyOrgList;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_Org", strRt_Cols_OS_Inos_Org
				});
			#endregion

			try
			{
                #region // SW:				
                stopWatchFunc.Start();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				//// Refine:
				bool bGet_OS_Inos_Org = (strRt_Cols_OS_Inos_Org != null && strRt_Cols_OS_Inos_Org.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

				#endregion

				#region // Sys_Solution: Get.
				////
				#endregion

				#region // Get Data:
				DataSet dsGetData = null;

				if (bGet_OS_Inos_Org)
				{
					// //
					Inos_OrgService_GetMyOrgListX_New20200207(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, out dsGetData // dsData
						);
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);

				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Rollback and Release resources:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
				TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                stopWatchFunc.Stop();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
                    });

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
				#endregion
			}
		}

		private void Inos_AccountService_RegisterX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			//, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_RegisterX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "strPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			dsData = new DataSet();

			////
			InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel();
			{
				objInosCreateUserModel.Email = string.Format("{0}", objEmail).Trim();
				objInosCreateUserModel.Name = string.Format("{0}", objName).Trim();
				objInosCreateUserModel.Password = string.Format("{0}", objPassword).Trim();
				objInosCreateUserModel.Language = string.Format("{0}", objLanguage).Trim();
				objInosCreateUserModel.TimeZone = Convert.ToInt32(objTimeZone);
			}
			#endregion

			#region // Call Service:
			alParamsCoupleError.AddRange(new object[]{
				"objInosCreateUserModel", TJson.JsonConvert.SerializeObject(objInosCreateUserModel)
				});

			try
			{
				////
				AccountService objAccountService = new AccountService(null);

				var ret = objAccountService.Register(objInosCreateUserModel);

				#region // Fill Data:
				{
					// //
					string strSqlGen_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select 
								t.MST
								, t.Email
								, t.Name
								, t.Password
								, t.Language
								, t.TimeZone
								, '@strUUID' UUID
								, t.Id
								, t.FlagEmailActivate
								, t.FlagActive
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
								and t.Email = '@strEmail'
							;
						"
						, "@strMST", strMST
						, "@strEmail", strEmail
						, "@strUUID", ret.UUID
						);

					DataTable dt_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGen_MstSv_Inos_User).Tables[0];
					dt_MstSv_Inos_User.TableName = "MstSv_Inos_User";

					dsData.Tables.Add(dt_MstSv_Inos_User.Copy());
				}
				#endregion

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_Register
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_RegisterForNetworkX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			//, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_RegisterForNetworkX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "strPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			dsData = new DataSet();

			////
			InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel();
			{
				objInosCreateUserModel.Email = string.Format("{0}", objEmail).Trim();
				objInosCreateUserModel.Name = string.Format("{0}", objName).Trim();
				objInosCreateUserModel.Password = string.Format("{0}", objPassword).Trim();
				objInosCreateUserModel.Language = string.Format("{0}", objLanguage).Trim();
				objInosCreateUserModel.TimeZone = Convert.ToInt32(objTimeZone);
			}
			#endregion

			#region // Call Service:
			alParamsCoupleError.AddRange(new object[]{
				"objInosCreateUserModel", TJson.JsonConvert.SerializeObject(objInosCreateUserModel)
				});

			try
			{
				////
				AccountService objAccountService = new AccountService(null);

				var ret = objAccountService.Register(objInosCreateUserModel);

				#region // Fill Data:
				{
					// //
					string strSqlGen_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.UserCode 
								, t.MST
								, t.Email
								, '@strUUID' UUID
							from Sys_User t
							where (1=1)
								and t.UserCode = '@strUserCode'
							;
						"
						, "@strUserCode", strEmail
						, "@strUUID", ret.UUID
						);

					DataTable dt_Sys_User = _cf.db.ExecQuery(strSqlGen_Sys_User).Tables[0];
					dt_Sys_User.TableName = "Sys_User";

					dsData.Tables.Add(dt_Sys_User.Copy());
				}
				#endregion

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_Register
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_ActivateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			//, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objUUID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_ActivateX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objUUID", objUUID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			string strUUID = string.Format("{0}", objUUID).Trim();
			dsData = new DataSet();

			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);

				var ret = objAccountService.Activate(strUUID);

				#region // Fill Data:
				{
					// //
					string strSqlGen_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select 
								t.MST
								, t.Email
								, t.Name
								, t.Password
								, t.Language
								, t.TimeZone
								, t.UUID
								, @objId Id
								, t.FlagEmailActivate
								, t.FlagActive
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
								and t.Email = '@strEmail'
							;
						"
						, "@objId", ret.Id
						, "@strMST", strMST
						, "@strEmail", strEmail
						);

					DataTable dt_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGen_MstSv_Inos_User).Tables[0];
					dt_MstSv_Inos_User.TableName = "MstSv_Inos_User";

					dsData.Tables.Add(dt_MstSv_Inos_User.Copy());
				}
				#endregion

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_Activate
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_ActivateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			//, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objUUID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_ActivateX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUUID", objUUID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strUUID = string.Format("{0}", objUUID).Trim();
			dsData = new DataSet();

			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);

				var ret = objAccountService.Activate(strUUID);

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_Activate
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_EditProfileX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objName
			, object objOldPassword
			, object objNewPassword
			, object objChangePassword
			, object objChangeAvatar
			, object objChangeName
			, object objAvatarBase64
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_EditProfileX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objName", objName
				, "objOldPassword", objOldPassword
				, "objNewPassword", objNewPassword
				, "objChangePassword", objChangePassword
				, "objChangeAvatar", objChangeAvatar
				, "objChangeName", objChangeName
				, "objAvatarBase64", objAvatarBase64
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strName = string.Format("{0}", objName).Trim();
			string strOldPassword = string.Format("{0}", objOldPassword).Trim();
			string strNewPassword = string.Format("{0}", objNewPassword).Trim();
			bool bChangePassword = Convert.ToBoolean(objChangePassword);
			bool bChangeAvatar = Convert.ToBoolean(objChangeAvatar);
			bool bChangeName = Convert.ToBoolean(objChangeName);
			string strAvatarBase64 = string.Format("{0}", objAvatarBase64).Trim();
			dsData = new DataSet();

			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);
				objAccountService.AccessToken = strAccessToken;
				InosEditProfileModel objInosEditProfileModel = new InosEditProfileModel();
				objInosEditProfileModel.Name = strName;
				objInosEditProfileModel.OldPassword = strOldPassword;
				objInosEditProfileModel.NewPassword = strNewPassword;
				objInosEditProfileModel.ChangePassword = bChangePassword;
				objInosEditProfileModel.ChangeAvatar = bChangeAvatar;
				objInosEditProfileModel.ChangeName = bChangeName;
				objInosEditProfileModel.AvatarBase64 = strAvatarBase64;

				object objJsonRQ = TJson.JsonConvert.SerializeObject(objInosEditProfileModel);
				var ret = objAccountService.EditProfile(objInosEditProfileModel);

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_EditProfile
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

        #region // Xuân Create Org Con 2019-10-24, Tạm dừng
        public void Inos_OrgService_CreateOrgForNetwork(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objMST
            , object objNNTFullName
            , object objContactName
            , object objContactPhone
            , object objContactEmail
            , object objBizType // objBizType
            , object objBizFieldCode // objBizFieldCode
            , object objBizSizeCode // objBizSizeCode
            , out object objOrgId
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inos_OrgService_CreateOrgForNetwork";
            //string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_BuildAndCreate;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
                , "objContactName", objContactName
                , "objContactPhone", objContactPhone
                , "objContactEmail", objContactEmail
                , "objBizFieldCode", objBizFieldCode
                , "objBizSizeCode", objBizSizeCode
                , "objBizType", objBizType
                , "objNNTFullName", objNNTFullName
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strContactName = string.Format("{0}", objContactName).Trim();
            string strContactPhone = string.Format("{0}", objContactPhone).Trim();
			string strContactEmail = string.Format("{0}", objContactEmail).Trim();
            string strNNTFullName = string.Format("{0}", objNNTFullName).Trim();
            string strBizFieldCode = TUtils.CUtils.StdParam(objBizFieldCode);
            string strBizSizeCode = TUtils.CUtils.StdParam(objBizSizeCode);
            string strBizType = TUtils.CUtils.StdParam(objBizType);
            #endregion

            #region // MstSv_Inos_Org_CreateX:
            //DataSet dsGetData = null;
            {
                    ////
                //object objAccessToken = null;

                //Inos_AccountService_GetAccessTokenX(
                //        strTid // strTid
                //        , strGwUserCode // strGwUserCode
                //        , strGwPassword // strGwPassword
                //        , strWAUserCode // strWAUserCode
                //        , strWAUserPassword // strWAUserPassword
                //        , ref mdsFinal // mdsFinal
                //        , ref alParamsCoupleError // alParamsCoupleError
                //        , dtimeSys // dtimeSys
                //                   ////
                //        , strWAUserCode // objEmail
                //        , strWAUserPassword // objPassword
                //        ////
                //        , out objAccessToken // objAccessToken
                //        );

                ////
                objOrgId = null;
                Inos_OrgService_CreateOrgForNetworkX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // (string)objAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strMST
                        , strNNTFullName
                        , strContactName
                        , strContactPhone
                        , strContactEmail
                        , strBizType
                        , strBizFieldCode
                        , strBizSizeCode
                        , out objOrgId
                        );
            }
            ////
            //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
            #endregion

            // Return Good:
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            mdsFinal.AcceptChanges();
            //return mdsFinal;
        }

        private void Inos_OrgService_CreateOrgForNetworkX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objMST
            , object objNNTFullName
            , object objContactName
            , object objContactPhone
            , object objContactEmail
            , object objBizType // objBizType
            , object objBizFieldCode // objBizFieldCode
            , object objBizSizeCode // objBizSizeCode
            ////
            //, out DataSet dsData
            , out object objOrgId
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrgService_CreateOrgForNetworkX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objMST", objMST
                , "objContactName", objContactName
                , "objContactEmail", objContactEmail
                , "objContactPhone", objContactPhone
                , "objBizType", objBizType
                , "objBizFieldCode", objBizFieldCode
                , "objBizSizeCode", objBizSizeCode
                , "objNNTFullName", objNNTFullName
                ////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            string strMst = TUtils.CUtils.StdParam(objMST);
            string strContactEmail = string.Format("{0}", objContactEmail).Trim();
            string strContactName = string.Format("{0}", objContactName).Trim();
            string strContactPhone = string.Format("{0}", objContactPhone).Trim();
            Int32 nBizType = Convert.ToInt32(objBizType);
            Int32 nBizFieldCode = Convert.ToInt32(objBizFieldCode);
            Int32 nBizSizeCode = Convert.ToInt32(objBizSizeCode);
            string strNNTFullName = string.Format("{0}", objNNTFullName).Trim();
            Org objOrg = new Org();
            //dsData = new DataSet();
            OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);

            ////
            objOrg.ParentId = nNetworkID;
            objOrg.Name = strNNTFullName;

            ////
            objOrg.BizType = new BizType();
            objOrg.BizType.Id = nBizType;
            objOrg.BizType.Name = "Default";

            ////
            objOrg.BizField = new BizField();
            objOrg.BizField.Id = nBizFieldCode;
            objOrg.BizField.Name = "Default";

            ////
            object objOrgSize = nBizSizeCode;
            objOrg.OrgSize = new OrgSizes();
            objOrg.OrgSize = (OrgSizes)objOrgSize;
            objOrg.ContactName = strContactName;
            objOrg.Email = strContactEmail;
            objOrg.PhoneNo = strContactPhone;
            objOrg.Description = "";
            objOrg.Enable = true;

            ////
            objOrg.UserList = new List<OrgUser>();

            ////
            objOrg.InviteList = null;
            objOrg.CurrentUserRole = OrgUserRoles.Admin;
            ////
            #endregion

            #region // Call Service:
            alParamsCoupleError.AddRange(new object[]{
                "Check.objOrg", TJson.JsonConvert.SerializeObject(objOrg)
                });
            try
            {
                ////
                OrgService objOrgService = new OrgService(null);
                objOrgService.AccessToken = strAccessToken;

                var ret = objOrgService.CreateOrg(objOrg);

                objOrgId = ret.Id;

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrgService_CreateOrg
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }
        #endregion

        private void Inos_OrgService_CreateOrgX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_CreateOrgX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			Org objOrg = new Org();
			dsData = new DataSet();
			#endregion

			#region // MstSv_Inos_Org : Fill Data.
			{
				// //
				string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Org:
						select	
							t.*
						from MstSv_Inos_Org t --//[mylock]
						where (1=1)
							and t.MST = '@strMST'
						;
					"
					, "@strMST", strMST
					);

				DataTable dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

				// //
				//objOrg.Id = 123; // Tạo Mới Chưa có.
				objOrg.ParentId = 0;
				objOrg.Name = Convert.ToString(dt_MstSv_Inos_Org.Rows[0]["MST"]);

				////
				objOrg.BizType = new BizType();
				if(!string.IsNullOrEmpty(TUtils.CUtils.StdParam(dt_MstSv_Inos_Org.Rows[0]["BizType"])))
				{
					objOrg.BizType.Id = Convert.ToInt32(dt_MstSv_Inos_Org.Rows[0]["BizType"]);
					//objOrg.BizType.Name = "Default";
				}

				////
				objOrg.BizField = new BizField();
				if (!string.IsNullOrEmpty(TUtils.CUtils.StdParam(dt_MstSv_Inos_Org.Rows[0]["BizField"])))
				{
					objOrg.BizField.Id = Convert.ToInt32(dt_MstSv_Inos_Org.Rows[0]["BizField"]);
					//objOrg.BizField.Name = "Default";
				}


				////
				objOrg.OrgSize = new OrgSizes();
				if (!string.IsNullOrEmpty(TUtils.CUtils.StdParam(dt_MstSv_Inos_Org.Rows[0]["BizField"])))
				{
					objOrg.OrgSize = (OrgSizes)dt_MstSv_Inos_Org.Rows[0]["BizField"];
				}				
				objOrg.ContactName = Convert.ToString(dt_MstSv_Inos_Org.Rows[0]["ContactName"]);
				objOrg.Email = Convert.ToString(dt_MstSv_Inos_Org.Rows[0]["Email"]);
				objOrg.PhoneNo = Convert.ToString(dt_MstSv_Inos_Org.Rows[0]["PhoneNo"]);
				objOrg.Description = "idocNet";
				objOrg.Enable = true;
			}
			#endregion

			#region // MstSv_Inos_OrgUser : Fill Data.
			{
				// //
				string strSqlGetDB_MstSv_Inos_OrgUser = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_OrgUser:
						select	
							t.*
							, f.Id
						from MstSv_Inos_OrgUser t --//[mylock]
							left join MstSv_Inos_User f --//[mylock]
								on t.MST = f.MST
						where (1=1)
							and t.MST = '@strMST'
						;
					"
					, "@strMST", strMST
					);

				DataTable dt_MstSv_Inos_OrgUser = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_OrgUser).Tables[0];

				// //
				objOrg.UserList = new List<OrgUser>();
				OrgUser objOrgUser = new OrgUser();
				objOrgUser.UserId = (long)(dt_MstSv_Inos_OrgUser.Rows[0]["Id"]);
				objOrgUser.Name = Convert.ToString(dt_MstSv_Inos_OrgUser.Rows[0]["Name"]);
				objOrgUser.Email = Convert.ToString(dt_MstSv_Inos_OrgUser.Rows[0]["Email"]);
				objOrgUser.Status = OrgUserStatuses.Active;
				objOrgUser.Role = OrgUserRoles.Admin;

				objOrg.UserList.Add(objOrgUser);
			}
			#endregion

			#region // Call Service:
			alParamsCoupleError.AddRange(new object[]{
				"Check.objOrg", TJson.JsonConvert.SerializeObject(objOrg)
				});

			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				var ret = objOrgService.CreateOrg(objOrg);

				#region // Fill Data:
				{
					// //
					string strSqlGen_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select 
								t.MST
								, @objId Id
								, t.ParentId
								, t.Name
								, t.BizType
								, t.BizField
								, t.OrgSize
								, t.ContactName
								, t.Email
								, t.PhoneNo
								, t.Description
								, t.Enable
								, t.CurrentUserRole
								, t.FlagActive
								, t.LogLUDTimeUTC, t.LogLUBy
							from MstSv_Inos_Org t --//[mylock]
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						, "@objId", ret.Id
						);

					DataTable dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGen_MstSv_Inos_Org).Tables[0];
					dt_MstSv_Inos_Org.TableName = "MstSv_Inos_Org";

					dsData.Tables.Add(dt_MstSv_Inos_Org.Copy());
				}
				#endregion

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_OrgService_CreateOrg
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

        private void RptSv_Inos_OrgService_CreateOrgX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objParentId
            , object objName
            , object objBizTypeId
            , object objBizTypeName
            , object objBizFieldId
            , object objBizFieldName
            ////
            , object objContactName
            , object objEmail
            , object objPhoneNo
            , object objDescription
            , object objEnable
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "RptSv_Inos_OrgService_CreateOrgX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objParentId", objParentId
                , "objName", objName
                , "objBizTypeId", objBizTypeId
                , "objBizTypeName", objBizTypeName
                , "objBizFieldId", objBizFieldId
                , "objBizFieldName", objBizFieldName
                ////
                , "objContactName", objContactName
                , "objEmail", objEmail
                , "objPhoneNo", objPhoneNo
                , "objDescription", objDescription
                , "objEnable", objEnable
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////

            Org objOrg = new Org();

            //objOrg.Id = 123; // Tạo Mới Chưa có.
            //objOrg.ParentId = 3232411000;
            //objOrg.Name = "5801407597";

            objOrg.ParentId = Convert.ToInt64(objParentId);
            objOrg.Name = Convert.ToString(objName);

            ////
            objOrg.BizType = new BizType();
            objOrg.BizType.Id = Convert.ToInt32(objBizTypeId);
            objOrg.BizType.Name = Convert.ToString(objBizTypeName);

            ////
            objOrg.BizField = new BizField();
            objOrg.BizField.Id = Convert.ToInt32(objBizFieldId);
            objOrg.BizField.Name = Convert.ToString(objBizFieldName);

            ////
            objOrg.OrgSize = new OrgSizes();
            //objOrg.OrgSize = (OrgSizes)objOrgSize;
            objOrg.ContactName = Convert.ToString(objContactName);
            objOrg.Email = Convert.ToString(objEmail);
            objOrg.PhoneNo = Convert.ToString(objPhoneNo);
            objOrg.Description = Convert.ToString(objDescription);
            objOrg.Enable = Convert.ToBoolean(objEnable);
            ////
            //objOrg.UserList = new List<OrgUser>();

            //OrgUser objOrgUser = new OrgUser();
            //objOrgUser.UserId = Convert.ToInt64(objOrgUserId);
            //objOrgUser.Name = Convert.ToString(objOrgUserName);
            //objOrgUser.Email = Convert.ToString(objOrgUserEmail);
            //objOrgUser.Status = OrgUserStatuses.Active;
            //objOrgUser.Role = OrgUserRoles.Admin;

            //objOrg.UserList.Add(objOrgUser);
            //////
            //objOrg.InviteList = null;

            ////
            objOrg.CurrentUserRole = OrgUserRoles.Admin;
            
            dsData = new DataSet();
            
            List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
            DataTable dtDB_OS_Inos_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Org").Tables[0];
            ////
            List<BizField> lst_iNOS_Mst_BizField = new List<BizField>();
            DataTable dtDB_iNOS_Mst_BizField = TDALUtils.DBUtils.GetSchema(_cf.db, "iNOS_Mst_BizField").Tables[0];
            ////
            List<BizType> lst_iNOS_Mst_BizType = new List<BizType>();
            DataTable dtDB_iNOS_Mst_BizType = TDALUtils.DBUtils.GetSchema(_cf.db, "iNOS_Mst_BizType").Tables[0];
            #endregion

            #region // Call Service:
            alParamsCoupleError.AddRange(new object[]{
                "Check.objOrg", TJson.JsonConvert.SerializeObject(objOrg)
                });

            try
            {
                ////
                OrgService objOrgService = new OrgService(null);
                objOrgService.AccessToken = strAccessToken;
                
                object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

                var ret = objOrgService.CreateOrg(objOrg);
                
                #region // dtDB_OS_Inos_Org:
                {
                    OS_Inos_Org objOS_Inos_Org = new OS_Inos_Org();
                    string strFN = "";
                    DataRow drDB = dtDB_OS_Inos_Org.NewRow();
                    strFN = "Id"; drDB[strFN] = ret.Id;
                    strFN = "ParentId"; drDB[strFN] = ret.ParentId;
                    strFN = "Name"; drDB[strFN] = ret.Name;
                    strFN = "BizType"; drDB[strFN] = ret.BizType.Id;
                    strFN = "BizField"; drDB[strFN] = ret.BizField.Id;
                    strFN = "OrgSize"; drDB[strFN] = ret.OrgSize;
                    strFN = "ContactName"; drDB[strFN] = ret.ContactName;
                    strFN = "Email"; drDB[strFN] = ret.Email;
                    strFN = "PhoneNo"; drDB[strFN] = ret.PhoneNo;
                    strFN = "Description"; drDB[strFN] = ret.Description;
                    strFN = "Enable"; drDB[strFN] = ret.Enable;
                    strFN = "CurrentUserRole"; drDB[strFN] = ret.CurrentUserRole;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_OS_Inos_Org.Rows.Add(drDB);
                }
                #endregion

                #region // dtDB_iNOS_Mst_BizType:
                {
                    iNOS_Mst_BizType objiNOS_Mst_BizType = new iNOS_Mst_BizType();
                    string strFN = "";
                    DataRow drDB = dtDB_iNOS_Mst_BizType.NewRow();
                    strFN = "BizType"; drDB[strFN] = ret.BizType.Id;
                    strFN = "BizTypeName"; drDB[strFN] = ret.BizType.Name;
                    dtDB_iNOS_Mst_BizType.Rows.Add(drDB);
                }
                #endregion

                #region // dtDB_iNOS_Mst_BizField:
                {
                    iNOS_Mst_BizField objiNOS_Mst_BizField = new iNOS_Mst_BizField();
                    string strFN = "";
                    DataRow drDB = dtDB_iNOS_Mst_BizField.NewRow();
                    strFN = "BizFieldCode"; drDB[strFN] = ret.BizField.Id;
                    strFN = "BizFieldName"; drDB[strFN] = ret.BizField.Name;
                    dtDB_iNOS_Mst_BizField.Rows.Add(drDB);
                }
                #endregion
            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrgService_CreateOrg
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            ////
            dsData.Tables.Add(dtDB_OS_Inos_Org.Copy());
            dsData.Tables.Add(dtDB_iNOS_Mst_BizType.Copy());
            dsData.Tables.Add(dtDB_iNOS_Mst_BizField.Copy());
            #endregion
        }

        private void Inos_LicService_RegisterPackagesX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgId
			, List<long> lstPackageIds
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_RegisterPackagesX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgId", objOrgId
				, "lstPackageIds", TJson.JsonConvert.SerializeObject(lstPackageIds)
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			long lgOrgId = Convert.ToInt64(objOrgId);
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				var ret = objLicService.RegisterPackages(lgOrgId, lstPackageIds);

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_RegisterPackages
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_LicService_GetAllPackagesX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSolutionCode
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_GetAllPackagesX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
			List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
			DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

			////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				List<InosPackage> lstInosPackage = objLicService.GetAllPackages(strSolutionCode);

				////
				foreach (var objItem in lstInosPackage)
				{
					////
					OS_Inos_Package objOS_Inos_Package = new OS_Inos_Package();
					string strFN = "";
					DataRow drDB = dtDB_OS_Inos_Package.NewRow();
					strFN = "Id"; drDB[strFN] = objItem.Id;
					strFN = "Name"; drDB[strFN] = objItem.Name;
					strFN = "Price"; drDB[strFN] = objItem.Price;
					strFN = "ImageUrl"; drDB[strFN] = objItem.ImageUrl;
					strFN = "IntroUrl"; drDB[strFN] = objItem.IntroUrl;
					strFN = "Description"; drDB[strFN] = objItem.Description;
					strFN = "Detail"; drDB[strFN] = objItem.Detail;
					dtDB_OS_Inos_Package.Rows.Add(drDB);
				}

				dsData.Tables.Add(dtDB_OS_Inos_Package.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_GetAllPackages
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

        private void Inos_LicService_GetAllPackagesX_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objSolutionCode
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_LicService_GetAllPackagesX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
            List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
            DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

            ////
            dsData = new DataSet();
            #endregion

            #region // Call Service:
            try
            {
                ////
                LicService objLicService = new LicService(null);
                objLicService.AccessToken = strAccessToken;

                List<InosPackage> lstInosPackage = objLicService.GetAllPackages(strSolutionCode);

                ////
                foreach (var objItem in lstInosPackage)
                {
                    ////
                    OS_Inos_Package objOS_Inos_Package = new OS_Inos_Package();
                    string strFN = "";
                    DataRow drDB = dtDB_OS_Inos_Package.NewRow();
                    strFN = "Id"; drDB[strFN] = objItem.Id;
                    strFN = "Name"; drDB[strFN] = objItem.Name;
                    strFN = "Price"; drDB[strFN] = objItem.Price;
                    strFN = "ImageUrl"; drDB[strFN] = objItem.ImageUrl;
                    strFN = "IntroUrl"; drDB[strFN] = objItem.IntroUrl;
                    strFN = "Description"; drDB[strFN] = objItem.Description;
                    strFN = "Detail"; drDB[strFN] = objItem.Detail;
                    strFN = "IsDiscountable"; drDB[strFN] = objItem.IsDiscountable;
                    dtDB_OS_Inos_Package.Rows.Add(drDB);
                }

                dsData.Tables.Add(dtDB_OS_Inos_Package.Copy());

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_LicService_GetAllPackages
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        private void Inos_LicService_GetCurrentUserLicenseX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSolutionCode
			, object objOrgID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_GetCurrentUserLicenseX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
				, "objOrgID", objOrgID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
			long lgOrgID = Convert.ToInt64(objOrgID);
			DataTable dtDB_Sys_Modules = TDALUtils.DBUtils.GetSchema(_cf.db, "Sys_Modules").Tables[0];
			TUtils.CUtils.MyForceNewColumn(ref dtDB_Sys_Modules, "Qty", typeof(object));

			////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				UserLicense objUserLicense = objLicService.GetCurrentUserLicense(strSolutionCode, lgOrgID);

				////
				if (objUserLicense != null)
				{
					foreach (var objItem in objUserLicense.Modules)
					{
						string strFN = "";
						DataRow drDB = dtDB_Sys_Modules.NewRow();
						strFN = "ModuleCode"; drDB[strFN] = objItem.Code;
						strFN = "Qty"; drDB[strFN] = objItem.Count;
						dtDB_Sys_Modules.Rows.Add(drDB);
					}
				}

				dsData.Tables.Add(dtDB_Sys_Modules.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_GetCurrentUserLicense
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_OrgService_GetMyOrgListX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_GetMyOrgListX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
			DataTable dtDB_OS_Inos_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Org").Tables[0];

			////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				List<Org> lstInosOrg = objOrgService.GetMyOrgList();

				////
				foreach (var objItem in lstInosOrg)
				{
					////
					OS_Inos_Org objOS_Inos_Org = new OS_Inos_Org();
					string strFN = "";
					DataRow drDB = dtDB_OS_Inos_Org.NewRow();
					strFN = "Id"; drDB[strFN] = objItem.Id;
					strFN = "ParentId"; drDB[strFN] = objItem.ParentId;
					strFN = "Name"; drDB[strFN] = objItem.Name;
					strFN = "BizType"; drDB[strFN] = objItem.BizType.Id;
					strFN = "BizField"; drDB[strFN] = objItem.BizField.Id;
					strFN = "OrgSize"; drDB[strFN] = objItem.OrgSize;
					strFN = "ContactName"; drDB[strFN] = objItem.ContactName;
					strFN = "Email"; drDB[strFN] = objItem.Email;
					strFN = "PhoneNo"; drDB[strFN] = objItem.PhoneNo;
					strFN = "Description"; drDB[strFN] = objItem.Description;
					strFN = "Enable"; drDB[strFN] = objItem.Enable;
					strFN = "CurrentUserRole"; drDB[strFN] = objItem.CurrentUserRole;
					dtDB_OS_Inos_Org.Rows.Add(drDB);
				}

				dsData.Tables.Add(dtDB_OS_Inos_Org.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_OrgService_GetMyOrgList
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_OrgService_GetMyOrgListX_New20200207(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_GetMyOrgListX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
			DataTable dtDB_OS_Inos_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Org").Tables[0];
			TUtils.CUtils.MyForceNewColumn(ref dtDB_OS_Inos_Org, "FlagNetworkExist", typeof(object));

			////
			dsData = new DataSet();
			string strNetworkID = null;
			#endregion

			#region // Call Service:
			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				List<Org> lstInosOrg = objOrgService.GetMyOrgList();

				////
				foreach (var objItem in lstInosOrg)
				{
					////
					OS_Inos_Org objOS_Inos_Org = new OS_Inos_Org();
					string strFN = "";
					DataRow drDB = dtDB_OS_Inos_Org.NewRow();
					strFN = "Id"; drDB[strFN] = objItem.Id;
					strFN = "ParentId"; drDB[strFN] = objItem.ParentId;
					strFN = "Name"; drDB[strFN] = objItem.Name;
					strFN = "BizType"; drDB[strFN] = objItem.BizType.Id;
					strFN = "BizField"; drDB[strFN] = objItem.BizField.Id;
					strFN = "OrgSize"; drDB[strFN] = objItem.OrgSize;
					strFN = "ContactName"; drDB[strFN] = objItem.ContactName;
					strFN = "Email"; drDB[strFN] = objItem.Email;
					strFN = "PhoneNo"; drDB[strFN] = objItem.PhoneNo;
					strFN = "Description"; drDB[strFN] = objItem.Description;
					strFN = "Enable"; drDB[strFN] = objItem.Enable;
					strFN = "CurrentUserRole"; drDB[strFN] = objItem.CurrentUserRole;

					////
					strNetworkID = Convert.ToString((objItem.ParentId == 0) ? objItem.Id : objItem.ParentId);

					DataTable dtDB_MstSv_Mst_Network = null;

					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, "" // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);

					strFN = "FlagNetworkExist"; drDB[strFN] = (dtDB_MstSv_Mst_Network.Rows.Count > 0) ? TConst.Flag.Active : TConst.Flag.Inactive;

					/////
					dtDB_OS_Inos_Org.Rows.Add(drDB);
				}

				dsData.Tables.Add(dtDB_OS_Inos_Org.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_OrgService_GetMyOrgList
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_GetAccessTokenX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objEmail
			, object objPassword
			////
			, out object objAccessToken
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_GetAccessTokenX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objEmail", objEmail
				, "objPassword", objPassword
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strEmail = string.Format("{0}", objEmail).Trim();
			string strPassword = string.Format("{0}", objPassword).Trim();
			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);
				var ret = objAccountService.RequestToken(strEmail, strPassword, new string[] { "test" });
				objAccessToken = ret.AccessToken;

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_RequestToken
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_OrgService_DeleteUserX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objUserID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_DeleteUserX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objUserID", objUserID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			long lgOrgID = Convert.ToInt64(objOrgID);
			long lgUserID = Convert.ToInt64(objUserID);
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			//alParamsCoupleError.AddRange(new object[]{
			//	"Check.objOrg", TJson.JsonConvert.SerializeObject(objUserID)
			//	});

			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				OrgUser objOrgUser = new OrgUser();
				objOrgUser.OrgId = lgOrgID;
				objOrgUser.UserId = lgUserID;

				var ret = objOrgService.DeleteUser(objOrgUser);
			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_OrgService_DeleteUser
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_OS_Inos_Package_Get(
			ref ArrayList alParamsCoupleError
			, RQ_OS_Inos_Package objRQ_OS_Inos_Package
			////
			, out RT_OS_Inos_Package objRT_OS_Inos_Package
			)
		{
			#region // Temp:
			string strTid = objRQ_OS_Inos_Package.Tid;
			objRT_OS_Inos_Package = new RT_OS_Inos_Package();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Package.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_OS_Inos_Package_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_Package_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
				#endregion

				#region // WS_OS_Inos_Package_Get:
				mdsResult = OS_Inos_Package_Get(
					objRQ_OS_Inos_Package.Tid // strTid
					, objRQ_OS_Inos_Package.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Package.GwPassword // strGwPassword
					, objRQ_OS_Inos_Package.WAUserCode // strUserCode
					, objRQ_OS_Inos_Package.WAUserPassword // strUserPassword
					, objRQ_OS_Inos_Package.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
					//// Return:
					, objRQ_OS_Inos_Package.Rt_Cols_OS_Inos_Package // strRt_Cols_OS_Inos_Package
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					//////
					//DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					//lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					//objRT_OS_Inos_Package.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_OS_Inos_Package = mdsResult.Tables["OS_Inos_Package"].Copy();
					lst_OS_Inos_Package = TUtils.DataTableCmUtils.ToListof<OS_Inos_Package>(dt_OS_Inos_Package);
					objRT_OS_Inos_Package.Lst_OS_Inos_Package = lst_OS_Inos_Package;
					/////
				}
				#endregion

				// Return Good:
				return mdsResult;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
				#endregion
			}
		}

		public DataSet WAS_RptSv_OS_Inos_Package_Get(
			ref ArrayList alParamsCoupleError
			, RQ_OS_Inos_Package objRQ_OS_Inos_Package
			////
			, out RT_OS_Inos_Package objRT_OS_Inos_Package
			)
		{
			#region // Temp:
			string strTid = objRQ_OS_Inos_Package.Tid;
			objRT_OS_Inos_Package = new RT_OS_Inos_Package();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Package.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_OS_Inos_Package_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_OS_Inos_Package_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
				#endregion

				#region // WS_OS_Inos_Package_Get:
				mdsResult = RptSv_OS_Inos_Package_Get(
					objRQ_OS_Inos_Package.Tid // strTid
					, objRQ_OS_Inos_Package.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Package.GwPassword // strGwPassword
					, objRQ_OS_Inos_Package.WAUserCode // strUserCode
					, objRQ_OS_Inos_Package.WAUserPassword // strUserPassword
					, objRQ_OS_Inos_Package.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Return:
					, objRQ_OS_Inos_Package.Rt_Cols_OS_Inos_Package // strRt_Cols_OS_Inos_Package
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					//////
					//DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					//lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					//objRT_OS_Inos_Package.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_OS_Inos_Package = mdsResult.Tables["OS_Inos_Package"].Copy();
					lst_OS_Inos_Package = TUtils.DataTableCmUtils.ToListof<OS_Inos_Package>(dt_OS_Inos_Package);
					objRT_OS_Inos_Package.Lst_OS_Inos_Package = lst_OS_Inos_Package;
					/////
				}
				#endregion

				// Return Good:
				return mdsResult;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
				#endregion
			}
		}

		public DataSet WAS_OS_Inos_Org_GetMyOrgList(
			ref ArrayList alParamsCoupleError
			, RQ_OS_Inos_Org objRQ_OS_Inos_Org
			////
			, out RT_OS_Inos_Org objRT_OS_Inos_Org
			)
		{
			#region // Temp:
			string strTid = objRQ_OS_Inos_Org.Tid;
			objRT_OS_Inos_Org = new RT_OS_Inos_Org();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Org.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_OS_Inos_Org_GetMyOrgList";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_Org_GetMyOrgList;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
				#endregion

				#region // WS_OS_Inos_Org_GetMyOrgList:
				mdsResult = OS_Inos_Org_GetMyOrgList(
					objRQ_OS_Inos_Org.Tid // strTid
					, objRQ_OS_Inos_Org.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Org.GwPassword // strGwPassword
					, objRQ_OS_Inos_Org.WAUserCode // strUserCode
					, objRQ_OS_Inos_Org.WAUserPassword // strUserPassword
					, objRQ_OS_Inos_Org.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Return:
					, objRQ_OS_Inos_Org.Rt_Cols_OS_Inos_Org // strRt_Cols_OS_Inos_Org
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					//////
					//DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					//lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					//objRT_OS_Inos_Org.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_OS_Inos_Org = mdsResult.Tables["OS_Inos_Org"].Copy();
					lst_OS_Inos_Org = TUtils.DataTableCmUtils.ToListof<OS_Inos_Org>(dt_OS_Inos_Org);
					objRT_OS_Inos_Org.Lst_OS_Inos_Org = lst_OS_Inos_Org;
					/////
				}
				#endregion

				// Return Good:
				return mdsResult;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
				#endregion
			}
		}

        public DataSet WAS_OS_Inos_SendEmailVerificationEmail(
            ref ArrayList alParamsCoupleError
            , RQ_VerificationEmail objRQ_VerificationEmail
            ////
            , out RT_VerificationEmail objRT_VerificationEmail
            )
        {
            #region // Temp:
            string strTid = objRQ_VerificationEmail.Tid;
            objRT_VerificationEmail = new RT_VerificationEmail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_VerificationEmail.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_SendEmailVerificationEmail";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_SendEmailVerificationEmail;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<VerificationEmail> lst_VerificationEmail = new List<VerificationEmail>();
                #endregion

                #region // WS_VerificationEmail_Get:
                mdsResult = Inos_AccountService_SendEmailVerificationEmail(
                    objRQ_VerificationEmail.Tid // strTid
                    , objRQ_VerificationEmail.GwUserCode // strGwUserCode
                    , objRQ_VerificationEmail.GwPassword // strGwPassword
                    , objRQ_VerificationEmail.WAUserCode // strUserCode
                    , objRQ_VerificationEmail.WAUserPassword // strUserPassword
                    , objRQ_VerificationEmail.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    , objRQ_VerificationEmail.Rt_Cols_VerificationEmail // strRt_Cols_VerificationEmail
                    , objRQ_VerificationEmail.VerificationEmail.email // objemail
                    , objRQ_VerificationEmail.VerificationEmail.emailSubject // objemailSubject
                    , objRQ_VerificationEmail.VerificationEmail.emailTemplate // objemailTemplate
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_VerificationEmail.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    //DataTable dt_VerificationEmail = mdsResult.Tables["VerificationEmail"].Copy();
                    //lst_VerificationEmail = TUtils.DataTableCmUtils.ToListof<VerificationEmail>(dt_VerificationEmail);
                    //objRT_VerificationEmail.Lst_VerificationEmail = lst_VerificationEmail;
                    ///////
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Inos_AccountService_SendEmailVerificationEmail(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            , string strRt_Cols_VerificationEmail
            ////
            , object objemail
            , object objemailSubject
            , object objemailTemplate
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inos_AccountService_SendEmailVerificationEmail";
            string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_SendEmailVerificationEmail;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_VerificationEmail", strRt_Cols_VerificationEmail
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                //bool bGet_VerificationEmail = (strRt_Cols_VerificationEmail != null && strRt_Cols_VerificationEmail.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Get Data:
                DataSet dsGetData = null;
                {
                    // //
                    Inos_AccountService_SendEmailVerificationEmailX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objemail // objemail
                        , objemailSubject // objemailSubject
                        , objemailTemplate // objemailTemplate
                                           ////
                        , out dsGetData // dsData
                        );
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Inos_AccountService_SendEmailVerificationEmailX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objemail
            , object objemailSubject
            , object objemailTemplate
            //, object objVerifyEmailId
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_AccountService_SendEmailVerificationEmailX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objemail", objemail
                , "objemailSubject", objemailSubject
                , "objemailTemplate", objemailTemplate
                //, "objVerifyEmailId", objVerifyEmailId
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            string stremail = Convert.ToString(objemail);
            string stremailSubject = Convert.ToString(objemailSubject);
            string stremailTemplate = Convert.ToString(objemailTemplate);
            //bool bVerifyEmailId = Convert.ToBoolean(objVerifyEmailId);
            //string str = Convert.ToString(objemailTemplate);
            List<VerificationEmail> lst_VerificationEmail = new List<VerificationEmail>();
            //DataTable dtDB_VerificationEmail = TDALUtils.DBUtils.GetSchema(_cf.db, "VerificationEmail").Tables[0];

            ////
            dsData = new DataSet();
            #endregion

            #region // Call Service:
            try
            {
                ////
                AccountService objAccountService = new AccountService(null);


                var ret = objAccountService.SendEmailVerificationEmail(stremail, stremailSubject, stremailTemplate);

                //object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
                //bool  = AccountService.SendEmailVerificationEmail(stremail, stremailSubject, stremailTemplate);

                ////
                //foreach (var objItem in ret)
                //{
                //    ////
                //    VerificationEmail objVerificationEmail = new VerificationEmail();
                //    string strFN = "";
                //    DataRow drDB = dtDB_VerificationEmail.NewRow();
                //    strFN = "VerifyEmailId"; drDB[strFN] = ret;
                //    dtDB_VerificationEmail.Rows.Add(drDB);
                //}

                //dsData.Tables.Add(dtDB_VerificationEmail.Copy());

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, ret);

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_AccountService_SendEmailVerificationEmail
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_OS_Inos_User_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_User objRQ_OS_Inos_User
            ////
            , out RT_OS_Inos_User objRT_OS_Inos_User
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_User.Tid;
            objRT_OS_Inos_User = new RT_OS_Inos_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////

                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_Inos_User> lst_OS_Inos_User = new List<OS_Inos_User>();
                //List<iNOS_Mst_BizType> lst_iNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
                //List<iNOS_Mst_BizField> lst_iNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
                #endregion

                #region // WS_OS_Inos_User_Get:
                mdsResult = OS_Inos_User_Create(
                    objRQ_OS_Inos_User.Tid // strTid
                    , objRQ_OS_Inos_User.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_User.GwPassword // strGwPassword
                    , objRQ_OS_Inos_User.WAUserCode // strUserCode
                    , objRQ_OS_Inos_User.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    , objRQ_OS_Inos_User.Rt_Cols_OS_Inos_User // strRt_Cols_OS_Inos_User
                    , objRQ_OS_Inos_User.OS_Inos_User.Name // objName
                    , objRQ_OS_Inos_User.OS_Inos_User.Email // objEmail
                    , objRQ_OS_Inos_User.OS_Inos_User.Password // objPassword
                    , objRQ_OS_Inos_User.OS_Inos_User.Language // objLanguage
                    , objRQ_OS_Inos_User.OS_Inos_User.TimeZone // objTimeZone
                    , objRQ_OS_Inos_User.OS_Inos_User.VerificationCode // objVerificationCode
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_OS_Inos_User.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_OS_Inos_User = mdsResult.Tables["OS_Inos_User"].Copy();
                    lst_OS_Inos_User = TUtils.DataTableCmUtils.ToListof<OS_Inos_User>(dt_OS_Inos_User);
                    objRT_OS_Inos_User.Lst_OS_Inos_User = lst_OS_Inos_User;
                    ////
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet OS_Inos_User_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            , string strRt_Cols_OS_Inos_User
            ////
            , object objName
            , object objEmail
            , object objPassword
            , object objLanguage
            , object objTimeZone
            , object objVerificationCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_Inos_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_User", strRt_Cols_OS_Inos_User
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                bool bGet_OS_Inos_User = (strRt_Cols_OS_Inos_User != null && strRt_Cols_OS_Inos_User.Length > 0);
                
                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Get Data:
                DataSet dsGetData = null;

                if (bGet_OS_Inos_User)
                {
                    ////
                    RptSv_Inos_OrgService_CreateUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objName
                        , objEmail
                        , objPassword
                        , objLanguage
                        , objTimeZone
                        , objVerificationCode
                        ////
                        , out dsGetData // dsData
                        );
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void RptSv_Inos_OrgService_CreateUserX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objName
            , object objEmail
            , object objPassword
            , object objLanguage
            , object objTimeZone
            , object objVerificationCode
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "RptSv_Inos_OrgService_CreateUserX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objEmail", objEmail
                , "objName", objName
                , "objPassword", objPassword
                , "objLanguage", objLanguage
                , "objTimeZone", objTimeZone
                , "objVerificationCode", objVerificationCode
                ////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////

            InosCreateUserModel objUser = new InosCreateUserModel();

            //objOrg.Id = 123; // Tạo Mới Chưa có.
            //objOrg.ParentId = 3232411000;
            //objOrg.Name = "5801407597";
            
            objUser.Name = Convert.ToString(objName);
            
            objUser.Email = Convert.ToString(objEmail);
            objUser.Password = Convert.ToString(objPassword);
            objUser.Language = Convert.ToString(objLanguage);
            objUser.TimeZone= Convert.ToInt32(objTimeZone);
            objUser.VerificationCode = Convert.ToString(objVerificationCode);

            dsData = new DataSet();

            List<OS_Inos_User> lst_OS_Inos_User = new List<OS_Inos_User>();
            DataTable dtDB_OS_Inos_User = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_User").Tables[0];
            #endregion

            #region // Call Service:
            alParamsCoupleError.AddRange(new object[]{
                "Check.objUser", TJson.JsonConvert.SerializeObject(objUser)
                });

            try
            {
                ////
                AccountService objAccountService = new AccountService(null);

                object objJsonRQ = TJson.JsonConvert.SerializeObject(objUser);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosCreateUserModel", objJsonRQ
                    });

                var ret = objAccountService.CreateUser(objUser);

                #region // dtDB_OS_Inos_User:
                {
                    OS_Inos_User objOS_Inos_User = new OS_Inos_User();
                    string strFN = "";
                    DataRow drDB = dtDB_OS_Inos_User.NewRow();
                    strFN = "Id"; drDB[strFN] = ret.Id;
                    strFN = "Name"; drDB[strFN] = ret.Name;
                    strFN = "Email"; drDB[strFN] = ret.Email;
                    strFN = "Language"; drDB[strFN] = ret.Language;
                    strFN = "TimeZone"; drDB[strFN] = ret.TimeZone;
                    strFN = "Avatar"; drDB[strFN] = ret.Avatar;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_OS_Inos_User.Rows.Add(drDB);
                }
                #endregion
            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_AccountService_CreateUser
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            ////
            dsData.Tables.Add(dtDB_OS_Inos_User.Copy());
            #endregion
        }

        public DataSet WAS_OS_Inos_VerifyEmail(
            ref ArrayList alParamsCoupleError
            , RQ_VerificationEmail objRQ_VerificationEmail
            ////
            , out RT_VerificationEmail objRT_VerificationEmail
            )
        {
            #region // Temp:
            string strTid = objRQ_VerificationEmail.Tid;
            objRT_VerificationEmail = new RT_VerificationEmail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_VerificationEmail.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_VerifyEmail";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_VerifyEmail;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<VerificationEmail> lst_VerifyEmail = new List<VerificationEmail>();
                #endregion

                #region // WS_VerificationEmail_Get:
                mdsResult = Inos_AccountService_VerifyEmail(
                    objRQ_VerificationEmail.Tid // strTid
                    , objRQ_VerificationEmail.GwUserCode // strGwUserCode
                    , objRQ_VerificationEmail.GwPassword // strGwPassword
                    , objRQ_VerificationEmail.WAUserCode // strUserCode
                    , objRQ_VerificationEmail.WAUserPassword // strUserPassword
                    , objRQ_VerificationEmail.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    //, objRQ_VerificationEmail.Rt_Cols_VerificationEmail // strRt_Cols_VerificationEmail
                    , objRQ_VerificationEmail.VerificationEmail.email // objemail
                    , objRQ_VerificationEmail.VerificationEmail.code // objemailSubject
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_VerificationEmail.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    //DataTable dt_VerificationEmail = mdsResult.Tables["VerificationEmail"].Copy();
                    //lst_VerificationEmail = TUtils.DataTableCmUtils.ToListof<VerificationEmail>(dt_VerificationEmail);
                    //objRT_VerificationEmail.Lst_VerificationEmail = lst_VerificationEmail;
                    ///////
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Inos_AccountService_VerifyEmail(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            //, string strRt_Cols_VerificationEmail
            ////
            , object objemail
            , object objcode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inos_AccountService_VerifyEmail";
            string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_VerifyEmail;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				//, "strRt_Cols_VerificationEmail", strRt_Cols_VerificationEmail
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                //bool bGet_VerificationEmail = (strRt_Cols_VerificationEmail != null && strRt_Cols_VerificationEmail.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Get Data:
                DataSet dsGetData = null;

                //if (bGet_VerificationEmail)
                {
                    // //
                    Inos_AccountService_VerifyEmailX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objemail // objemail
                        , objcode // objcode
                                           ////
                        , out dsGetData // dsData
                        );
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Inos_AccountService_VerifyEmailX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objemail
            , object objcode
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_AccountService_SendEmailVerificationEmailX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objemail", objemail
                , "objcode", objcode
                //, "objVerifyEmailId", objVerifyEmailId
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            string stremail = Convert.ToString(objemail);
            string strcode = Convert.ToString(objcode);
            //bool bVerifyEmailId = Convert.ToBoolean(objVerifyEmailId);
            //string str = Convert.ToString(objemailTemplate);
            List<VerificationEmail> lst_VerificationEmail = new List<VerificationEmail>();
            //DataTable dtDB_VerificationEmail = TDALUtils.DBUtils.GetSchema(_cf.db, "VerificationEmail").Tables[0];

            ////
            dsData = new DataSet();
            #endregion

            #region // Call Service:
            try
            {
                ////
                AccountService objAccountService = new AccountService(null);

                var ret = objAccountService.VerifyEmail(stremail, strcode);

                //object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
                //bool  = AccountService.SendEmailVerificationEmail(stremail, stremailSubject, stremailTemplate);

                ////
                //foreach (var objItem in ret)
                //{
                //    ////
                //    VerificationEmail objVerificationEmail = new VerificationEmail();
                //    string strFN = "";
                //    DataRow drDB = dtDB_VerificationEmail.NewRow();
                //    strFN = "VerifyEmailId"; drDB[strFN] = ret;
                //    dtDB_VerificationEmail.Rows.Add(drDB);
                //}

                //dsData.Tables.Add(dtDB_VerificationEmail.Copy());

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, ret);

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_AccountService_VerifyEmail
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

		private void Inos_OrgService_GetOrgX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objId
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_GetOrgX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objId", objId
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////

			Org objOrg = new Org();

			objOrg.Id = Convert.ToInt64(objId);

			dsData = new DataSet();

			List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
			DataTable dtDB_OS_Inos_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Org").Tables[0];
			////
			List<BizField> lst_iNOS_Mst_BizField = new List<BizField>();
			DataTable dtDB_iNOS_Mst_BizField = TDALUtils.DBUtils.GetSchema(_cf.db, "iNOS_Mst_BizField").Tables[0];
			////
			List<BizType> lst_iNOS_Mst_BizType = new List<BizType>();
			DataTable dtDB_iNOS_Mst_BizType = TDALUtils.DBUtils.GetSchema(_cf.db, "iNOS_Mst_BizType").Tables[0];
			#endregion

			#region // Call Service:
			alParamsCoupleError.AddRange(new object[]{
				"Check.objOrg", TJson.JsonConvert.SerializeObject(objOrg)
				});

			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.GetOrg(objOrg);

				#region // dtDB_OS_Inos_Org:
				{
					OS_Inos_Org objOS_Inos_Org = new OS_Inos_Org();
					string strFN = "";
					DataRow drDB = dtDB_OS_Inos_Org.NewRow();
					strFN = "Id"; drDB[strFN] = ret.Id;
					strFN = "ParentId"; drDB[strFN] = ret.ParentId;
					strFN = "Name"; drDB[strFN] = ret.Name;
					strFN = "BizType"; drDB[strFN] = ret.BizType.Id;
					strFN = "BizField"; drDB[strFN] = ret.BizField.Id;
					strFN = "OrgSize"; drDB[strFN] = ret.OrgSize;
					strFN = "ContactName"; drDB[strFN] = ret.ContactName;
					strFN = "Email"; drDB[strFN] = ret.Email;
					strFN = "PhoneNo"; drDB[strFN] = ret.PhoneNo;
					strFN = "Description"; drDB[strFN] = ret.Description;
					strFN = "Enable"; drDB[strFN] = ret.Enable;
					strFN = "CurrentUserRole"; drDB[strFN] = ret.CurrentUserRole;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_OS_Inos_Org.Rows.Add(drDB);
				}
				#endregion

				#region // dtDB_iNOS_Mst_BizType:
				{
					iNOS_Mst_BizType objiNOS_Mst_BizType = new iNOS_Mst_BizType();
					string strFN = "";
					DataRow drDB = dtDB_iNOS_Mst_BizType.NewRow();
					strFN = "BizType"; drDB[strFN] = ret.BizType.Id;
					strFN = "BizTypeName"; drDB[strFN] = ret.BizType.Name;
					dtDB_iNOS_Mst_BizType.Rows.Add(drDB);
				}
				#endregion

				#region // dtDB_iNOS_Mst_BizField:
				{
					iNOS_Mst_BizField objiNOS_Mst_BizField = new iNOS_Mst_BizField();
					string strFN = "";
					DataRow drDB = dtDB_iNOS_Mst_BizField.NewRow();
					strFN = "BizFieldCode"; drDB[strFN] = ret.BizField.Id;
					strFN = "BizFieldName"; drDB[strFN] = ret.BizField.Name;
					dtDB_iNOS_Mst_BizField.Rows.Add(drDB);
				}
				#endregion
			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_OrgService_GetOrg
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			////
			dsData.Tables.Add(dtDB_OS_Inos_Org.Copy());
			dsData.Tables.Add(dtDB_iNOS_Mst_BizType.Copy());
			dsData.Tables.Add(dtDB_iNOS_Mst_BizField.Copy());
			#endregion
		}

		#endregion
	}
}
