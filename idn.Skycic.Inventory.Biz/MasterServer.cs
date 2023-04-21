using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
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
using idn.Skycic.Inventory.BizService.Services;
using inos.common.Constants;
using inos.common.Model;
using inos.common.Service;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
    {
		#region // MstSv_Sys_Administrator:
		private void MstSv_Sys_Administrator_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objUserCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Sys_Administrator
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Sys_Administrator t --//[mylock]
					where (1=1)
						and t.UserCode = @objUserCode
					;
				");
			dtDB_MstSv_Sys_Administrator = _cf.db.ExecQuery(
				strSqlExec
				, "@objUserCode", objUserCode
				, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
				).Tables[0];
			dtDB_MstSv_Sys_Administrator.TableName = "MstSv_Sys_Administrator";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Sys_Administrator.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_Administrator_CheckDB_AdministratorNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Sys_Administrator.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_Administrator_CheckDB_AdministratorExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Sys_Administrator.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.UserCode", objUserCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Sys_Administrator.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Sys_Administrator_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void MstSv_Sys_Administrator_CheckDeny(
			ref ArrayList alParamsCoupleError
			, object strUserCode
			, object strUserPassword
			)
		{
			////
			DataTable dtDB_MstSv_Sys_Administrator = null;

			MstSv_Sys_Administrator_CheckDB(
				ref alParamsCoupleError // alParamsCoupleError
				, strUserCode // objUserCode
				, TConst.Flag.Yes // strFlagExistToCheck
				, TConst.Flag.Active // strFlagActiveListToCheck
				, out dtDB_MstSv_Sys_Administrator // dtDB_MstSv_Sys_Administrator
				);

			////
			if (!CmUtils.StringUtils.StringEqual(strUserPassword, dtDB_MstSv_Sys_Administrator.Rows[0]["UserPassword"]))
			{
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Sys_Administrator_CheckDeny // strErrorCode
					, null // excInner
					, alParamsCoupleError.ToArray() // arrobjParamsCouple
					);
			}

		}
		#endregion

		#region // MstSv_Sys_User:
		private void MstSv_Sys_User_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objUserCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Sys_User
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Sys_User t --//[mylock]
					where (1=1)
						and t.UserCode = @objUserCode
					;
				");
			dtDB_MstSv_Sys_User = _cf.db.ExecQuery(
				strSqlExec
				, "@objUserCode", objUserCode
				, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
				).Tables[0];
			dtDB_MstSv_Sys_User.TableName = "MstSv_Sys_User";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Sys_User.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_User_CheckDB_UserNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Sys_User.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_User_CheckDB_UserExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Sys_User.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.UserCode", objUserCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Sys_User.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Sys_User_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet MstSv_Sys_User_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_MstSv_Sys_User
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Sys_User_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_MstSv_Sys_User", strRt_Cols_MstSv_Sys_User
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Check:
				//// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_MstSv_Sys_User = (strRt_Cols_MstSv_Sys_User != null && strRt_Cols_MstSv_Sys_User.Length > 0);

				#endregion

				#region // Build Sql:
				////
				ArrayList alParamsCoupleSql = new ArrayList();
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
					});
				////
				//myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_MstSv_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, msu.UserCode
						into #tbl_MstSv_Sys_User_Filter_Draft
						from MstSv_Sys_User msu --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by msu.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_MstSv_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_MstSv_Sys_User_Filter:
						select
							t.*
						into #tbl_MstSv_Sys_User_Filter
						from #tbl_MstSv_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- MstSv_Sys_User --------:
						zzB_Select_MstSv_Sys_User_zzE
						----------------------------------------

						-------- MstSv_Sys_UserImage --------:
						zzB_Select_MstSv_Sys_UserImage_zzE
						----------------------------------------

						-------- MstSv_Sys_UserFiles --------:
						zzB_Select_MstSv_Sys_UserFiles_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_MstSv_Sys_User_Filter_Draft;
						--drop table #tbl_MstSv_Sys_User_Filter;
					"
					);
				////
				string zzB_Select_MstSv_Sys_User_zzE = "-- Nothing.";
				if (bGet_MstSv_Sys_User)
				{
					#region // bGet_MstSv_Sys_User:
					zzB_Select_MstSv_Sys_User_zzE = CmUtils.StringUtils.Replace(@"
                            ---- MstSv_Sys_User:
							select
								t.MyIdxSeq
								, msu.*
							from #tbl_MstSv_Sys_User_Filter t --//[mylock]
								inner join MstSv_Sys_User msu --//[mylock]
									on t.UserCode = msu.UserCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Where_strFilter_zzE = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "MstSv_Sys_User" // strTableNameDB
							, "MstSv_Sys_User." // strPrefixStd
							, "msu." // strPrefixAlias
							);
						////
						#endregion
					}
					zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
					alParamsCoupleError.AddRange(new object[]{
						"zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
					, "zzB_Select_MstSv_Sys_User_zzE", zzB_Select_MstSv_Sys_User_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_MstSv_Sys_User)
				{
					dsGetData.Tables[nIdxTable++].TableName = "MstSv_Sys_User";
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

		public DataSet MstSv_Sys_User_Login(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, string strOrgID
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objUserPassword
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Sys_User_Login;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
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
                _cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserPassword = string.Format("{0}", objUserPassword).Trim();

				////
				DataTable dtDB_MstSv_Mst_Network = null;
				string strWSUrlAddr = null;
				//DataTable dtDB_MQ_Mst_Network = null;
				{
					//////
					//DataTable dtDB_MstSv_Sys_User = null;

					//MstSv_Sys_User_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, strUserCode // objUserCode
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_MstSv_Sys_User // dtDB_MstSv_Sys_User
					//	);
					//////
					//if (!CmUtils.StringUtils.StringEqual(strUserPassword, dtDB_MstSv_Sys_User.Rows[0]["UserPassword"]))
					//{
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.MstSv_Sys_User_Login_InvalidPassword // strErrorCode
					//		, null // excInner
					//		, alParamsCoupleError.ToArray() // arrobjParamsCouple
					//		);
					//}
					//////
					//DataTable dtDB_MstSv_Sys_UserInNetWork = null;

					//MstSv_Sys_UserInNetWork_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, strUserCode // objUserCode
					//	, strNetworkID // objNetworkID
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_MstSv_Sys_UserInNetWork // dtDB_MstSv_Sys_UserInNetWork
					//	);
					////
					//MQ_Mst_Network_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, strNetworkID // objNetworkID
					//	, strOrgID // objOrgID
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_MQ_Mst_Network // dtDB_MQ_Mst_Network
					//	);
					////
					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, "" // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);

					if (dtDB_MstSv_Mst_Network.Rows.Count > 0)
					{
						strWSUrlAddr = Convert.ToString(dtDB_MstSv_Mst_Network.Rows[0]["WSUrlAddr"]);
					}

				}

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strWSUrlAddr);
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

		public DataSet MstSv_Sys_User_GetAccessToken(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objUserPassword
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Sys_User_GetAccessToken";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Sys_User_GetAccessToken;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
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
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserPassword = string.Format("{0}", objUserPassword).Trim();

				////
				object objAccessToken = null;

				Inos_AccountService_GetAccessTokenX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					////
					, strUserCode // objEmail
					, strUserPassword // objPassword
					////
					, out objAccessToken // objAccessToken
					);

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, objAccessToken);
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

		public DataSet WAS_MstSv_Sys_User_Login(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Sys_User objRQ_MstSv_Sys_User
			////
			, out RT_MstSv_Sys_User objRT_MstSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Sys_User.Tid;
			objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Sys_User_Login;
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
				////
				string strTokenID = TUtils.CUtils.StdParam(objRQ_MstSv_Sys_User.TokenID);
				string strUserCode = null;
				string strUserPassword = null;

				{
					////
					if (CmUtils.StringUtils.StringEqual(strTokenID, _cf.nvcParams["Biz_TokenId_idocNet"]))
					{
						strUserCode = "DUNGND";
						strUserPassword = "1";
					}
				}
				#endregion

				#region // WS_MstSv_Sys_User_Get:
				mdsResult = MstSv_Sys_User_Login(
					objRQ_MstSv_Sys_User.Tid // strTid
					, objRQ_MstSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Sys_User.GwPassword // strGwPassword
					, objRQ_MstSv_Sys_User.WAUserCode // strUserCode
					, objRQ_MstSv_Sys_User.WAUserPassword // strUserPassword
					, objRQ_MstSv_Sys_User.NetworkID // strNetworkID
					, objRQ_MstSv_Sys_User.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, strUserCode // objUserCode
					, strUserPassword // objUserPassword
					);
				#endregion

				#region // GetData:
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

		public DataSet WAS_MstSv_Sys_User_GetAccessToken(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Sys_User objRQ_MstSv_Sys_User
			////
			, out RT_MstSv_Sys_User objRT_MstSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Sys_User.Tid;
			objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Sys_User_GetAccessToken";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Sys_User_GetAccessToken;
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
				////
				//string strTokenID = TUtils.CUtils.StdParam(objRQ_MstSv_Sys_User.TokenID);
				//string strUserCode = null;
				//string strUserPassword = null;

				//{
				//	////
				//	if (CmUtils.StringUtils.StringEqual(strTokenID, _cf.nvcParams["Biz_TokenId_idocNet"]))
				//	{
				//		strUserCode = "DUNGND";
				//		strUserPassword = "1";
				//	}
				//}
				#endregion

				#region // WS_MstSv_Sys_User_Get:
				mdsResult = MstSv_Sys_User_GetAccessToken(
					objRQ_MstSv_Sys_User.Tid // strTid
					, objRQ_MstSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Sys_User.GwPassword // strGwPassword
					, objRQ_MstSv_Sys_User.WAUserCode // strUserCode
					, objRQ_MstSv_Sys_User.WAUserPassword // strUserPassword
					, objRQ_MstSv_Sys_User.NetworkID // strNetworkID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Sys_User.WAUserCode // objUserCode
					, objRQ_MstSv_Sys_User.WAUserPassword // objUserPassword
					);
				#endregion

				#region // GetData:
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

		public DataSet MstSv_Sys_User_Add(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objNetworkID
			, object objUserName
			, object objUserPassword
			, object objUserEmail
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Sys_User_Add";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Sys_User_Add;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Call Func:
				////
				MstSv_Sys_User_AddX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, objUserCode
					, objNetworkID
					, objUserName
					, objUserPassword
					, objUserEmail
					////
					);
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
				//alParamsCoupleError.AddRange(alPCErrEx);
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

		private void MstSv_Sys_User_AddX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objUserCode
			, object objNetworkID
			, object objUserName
			, object objUserPassword
			, object objUserEmail
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "MstSv_Sys_User_AddX";
			//string strErrorCodeDefault = TError.ErrTCGQLTV.MstSv_Sys_User_Add;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objUserCode", objUserCode
				, "objNetworkID", objNetworkID
				, "objUserName", objUserName
				, "objUserPassword", objUserPassword
				, "objUserEmail", objUserEmail
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			#endregion

			#region //// Refine and Check MstSv_Sys_User:
			////
			string strUserCode = TUtils.CUtils.StdParam(objUserCode);
			string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
			string strUserName = TUtils.CUtils.StdParam(objUserName);
			string strUserPassword = string.Format("{0}", objUserPassword).Trim();
			string strUserEmail = string.Format("{0}", objUserEmail).Trim();

			DataTable dtDB_MstSv_Sys_User = null;
			{
				////
				MstSv_Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strUserCode // objUserCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_MstSv_Sys_User // dtDB_MstSv_Sys_User
					);
				////
				if (strUserCode == null || strUserCode.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserCode", strUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_User_Add_InvalidUserCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strUserName == null || strUserName.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserName", strUserName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_User_Add_InvalidUserName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			////
			#endregion

			#region //// SaveTemp MstSv_Sys_User:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Sys_User"
					, new object[]{
						"UserCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"UserName", TConst.BizMix.Default_DBColType,
						"UserPassword", TConst.BizMix.Default_DBColType,
						"UserEmail", TConst.BizMix.Default_DBColType,
						"CreateDTimeUTC", TConst.BizMix.Default_DBColType,
						"EffDTimeUTC", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strUserCode, // UserCode
        			        nNetworkID, // NetworkID
        			        strUserName, // UserName								
        			        strUserPassword , // UserPassword
                            strUserEmail , // UserEmail
        			        dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // CreateDTimeUTC
        			        dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // EffDTimeUTC
        			        TConst.Flag.Active, // FlagActive
        			        dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
        			        }
						}
					);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				//string strSqlDelete = CmUtils.StringUtils.Replace(@"
				//			---- MstSv_Sys_UserDtl:
				//			delete t
				//			from MstSv_Sys_UserDtl t
				//			where (1=1)
				//				and t.KUNNNo = @strKUNNNo
				//			;

				//			---- MstSv_Sys_User:
				//			delete t
				//			from MstSv_Sys_User t
				//			where (1=1)
				//				and t.KUNNNo = @strKUNNNo
				//			;

				//		");
				//_cf.db.ExecQuery(
				//	strSqlDelete
				//	, "@strKUNNNo", strKUNNNo
				//	);
			}

			//// Insert All:
			{
				////
				string zzzzClauseInsert_MstSv_Sys_User_zSave = CmUtils.StringUtils.Replace(@"
        		---- MstSv_Sys_User:
        		insert into MstSv_Sys_User
        		(
                    UserCode
                    , NetworkID
                    , UserName
                    , UserPassword
                    , UserEmail
                    , SpecType1
                    , SpecType2
                    , Color
                    , FlagHasSerial
                    , FlagHasLOT
                    , DefaultUnitCode
                    , StandardUnitCode
                    , Remark
                    , CustomField1
                    , CustomField2
                    , CustomField3
                    , CustomField4
                    , CustomField5
                    , CustomField6
                    , CustomField7
                    , CustomField8
                    , CustomField9
                    , CustomField10
        			, LogLUDTimeUTC
        			, LogLUBy
        		)
        		select 
        			t.UserCode
        			, t.NetworkID
        			, t.UserName
        			, t.UserPassword
                    , t.UserEmail
        			, t.SpecType1
        			, t.SpecType2
        			, t.Color
        			, t.FlagHasSerial
        			, t.FlagHasLOT
        			, t.DefaultUnitCode
        			, t.StandardUnitCode
        			, t.Remark
        			, t.CustomField1
        			, t.CustomField2
        			, t.CustomField3
        			, t.CustomField4
        			, t.CustomField5
        			, t.CustomField6
        			, t.CustomField7
        			, t.CustomField8
        			, t.CustomField9
        			, t.CustomField10
        			, t.LogLUDTimeUTC
        			, t.LogLUBy
        		from #input_MstSv_Sys_User t --//[mylock]
        		;
        	");
				////
				string zzzzClauseInsert_MstSv_Sys_UserImage_zSave = CmUtils.StringUtils.Replace(@"
        		---- MstSv_Sys_UserImage:
        		insert into MstSv_Sys_UserImage
        		(
        			AutoID
        			, NetworkID
        			, UserCode
        			, SpecImagePath
        			, SpecImageName
        			, SpecImageDesc
        			, FlagPrimaryImage
        			, LogLUDTimeUTC
        			, LogLUBy
        		)
        		select 
        			t.AutoID
        			, t.NetworkID
        			, t.UserCode
        			, t.SpecImagePath
        			, t.SpecImageName
        			, t.SpecImageDesc
        			, t.FlagPrimaryImage
        			, t.LogLUDTimeUTC
        			, t.LogLUBy
        		from #input_MstSv_Sys_UserImage t --//[mylock]
        		;
        	");
				////
				string zzzzClauseInsert_MstSv_Sys_UserFiles_zSave = CmUtils.StringUtils.Replace(@"
        		---- MstSv_Sys_UserFiles:
        		insert into MstSv_Sys_UserFiles
        		(
        			AutoID
        			, NetworkID
        			, UserCode
        			, SpecFilePath
        			, SpecFileName
        			, SpecFileDesc
        			, LogLUDTimeUTC
        			, LogLUBy
        		)
        		select 
        			t.AutoID
        			, t.NetworkID
        			, t.UserCode
        			, t.SpecFilePath
        			, t.SpecFileName
        			, t.SpecFileDesc
        			, t.LogLUDTimeUTC
        			, t.LogLUBy
        		from #input_MstSv_Sys_UserFiles t --//[mylock]
        		;
            ");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
        		----
        		zzzzClauseInsert_MstSv_Sys_User_zSave

        		----
        		zzzzClauseInsert_MstSv_Sys_UserImage_zSave

        		----
        		zzzzClauseInsert_MstSv_Sys_UserFiles_zSave
        	"
					, "zzzzClauseInsert_MstSv_Sys_User_zSave", zzzzClauseInsert_MstSv_Sys_User_zSave
					, "zzzzClauseInsert_MstSv_Sys_UserImage_zSave", zzzzClauseInsert_MstSv_Sys_UserImage_zSave
					, "zzzzClauseInsert_MstSv_Sys_UserFiles_zSave", zzzzClauseInsert_MstSv_Sys_UserFiles_zSave
					);
				////
				if (bMyDebugSql)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.strSqlExec", strSqlExec
							});
				}
				DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        				---- Clear for Debug:
        				drop table #input_MstSv_Sys_User;
        				drop table #input_MstSv_Sys_UserImage;
        				drop table #input_MstSv_Sys_UserFiles;
        			");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}
		#endregion

		#region // MstSv_Mst_Network:
		private void MstSv_Mst_Network_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objNetworkID
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Mst_Network
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Mst_Network t --//[mylock]
					where (1=1)
						and t.NetworkID = @objNetworkID
					;
				");
			dtDB_MstSv_Mst_Network = _cf.db.ExecQuery(
				strSqlExec
				, "@objNetworkID", objNetworkID
				).Tables[0];
			dtDB_MstSv_Mst_Network.TableName = "MstSv_Mst_Network";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Mst_Network.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Mst_Network_CheckDB_NetworkNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Mst_Network.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Mst_Network_CheckDB_NetworkExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Mst_Network.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.NetworkID", objNetworkID
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Mst_Network.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Mst_Network_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet MstSv_Mst_Network_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_MstSv_Mst_Network
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "MstSv_Mst_Network_Get";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_MstSv_Mst_Network", strRt_Cols_MstSv_Mst_Network
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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Check:
				//// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_MstSv_Mst_Network = (strRt_Cols_MstSv_Mst_Network != null && strRt_Cols_MstSv_Mst_Network.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

				#endregion

				#region // Build Sql:
				////
				ArrayList alParamsCoupleSql = new ArrayList();
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					});
				////
				//myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_MstSv_Mst_Network_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mmb.NetworkID
						into #tbl_MstSv_Mst_Network_Filter_Draft
						from MstSv_Mst_Network mmb --//[mylock]
						where (1=1)
							and mmb.NetworkID not in ('@strAccountRoot')
							zzB_Where_strFilter_zzE
						order by mmb.NetworkID asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_MstSv_Mst_Network_Filter_Draft t --//[mylock]
						;

						---- #tbl_MstSv_Mst_Network_Filter:
						select
							t.*
						into #tbl_MstSv_Mst_Network_Filter
						from #tbl_MstSv_Mst_Network_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- MstSv_Mst_Network --------:
						zzB_Select_MstSv_Mst_Network_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_MstSv_Mst_Network_Filter_Draft;
						--drop table #tbl_MstSv_Mst_Network_Filter;
					"
					, "@strAccountRoot", TConst.BizMix.AccountRoot
					);
				////
				string zzB_Select_MstSv_Mst_Network_zzE = "-- Nothing.";
				if (bGet_MstSv_Mst_Network)
				{
					#region // bGet_MstSv_Mst_Network:
					zzB_Select_MstSv_Mst_Network_zzE = CmUtils.StringUtils.Replace(@"
							---- MstSv_Mst_Network:
							select distinct
								t.MyIdxSeq
								, mmb.*
							from #tbl_MstSv_Mst_Network_Filter t --//[mylock]
								inner join MstSv_Mst_Network mmb --//[mylock]
									on t.NetworkID = mmb.NetworkID
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Where_strFilter_zzE = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "MstSv_Mst_Network" // strTableNameDB
							, "MstSv_Mst_Network." // strPrefixStd
							, "mmb." // strPrefixAlias
							);
						////
						#endregion
					}
					zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
					alParamsCoupleError.AddRange(new object[]{
						"zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
					, "zzB_Select_MstSv_Mst_Network_zzE", zzB_Select_MstSv_Mst_Network_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_MstSv_Mst_Network)
				{
					dsGetData.Tables[nIdxTable++].TableName = "MstSv_Mst_Network";
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

		public DataSet MstSv_Mst_Network_GetByMST(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, string strOrgID
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_GetByMST";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_GetByMST;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
				////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strWSUrlAddr = null;

				////
				DataTable dtDB_MstSv_Mst_Network = null;
				{
					////
					string strSqlGetDB_MstSv_OrgInNetwork = CmUtils.StringUtils.Replace(@"
							---- MstSv_OrgInNetwork:
							select top 1
								t.*
							from MstSv_OrgInNetwork t --//[mylock]
							where (1=1)
								and t.MST = '@strMST'
								and t.FlagActive = '@strFlagActive'
							;
						"
						, "@strMST", strMST
						, "@strFlagActive", TConst.Flag.Active
						);

					DataTable dtDB_MstSv_OrgInNetwork = _cf.db.ExecQuery(strSqlGetDB_MstSv_OrgInNetwork).Tables[0];

					//////
					//DataTable dtDB_MstSv_Sys_User = null;

					//MstSv_Sys_User_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, strUserCode // objUserCode
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_MstSv_Sys_User // dtDB_MstSv_Sys_User
					//	);
					//////
					//if (!CmUtils.StringUtils.StringEqual(strUserPassword, dtDB_MstSv_Sys_User.Rows[0]["UserPassword"]))
					//{
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.MstSv_Mst_Network_GetByMST_InvalidPassword // strErrorCode
					//		, null // excInner
					//		, alParamsCoupleError.ToArray() // arrobjParamsCouple
					//		);
					//}
					//////
					//DataTable dtDB_MstSv_Sys_UserInNetWork = null;

					//MstSv_Sys_UserInNetWork_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, strUserCode // objUserCode
					//	, strNetworkID // objNetworkID
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_MstSv_Sys_UserInNetWork // dtDB_MstSv_Sys_UserInNetWork
					//	);
					////
					if (dtDB_MstSv_OrgInNetwork.Rows.Count > 0)
					{
						//alParamsCoupleError.AddRange(new object[]{
						//	"Check.strMST", strMST
						//	});
						//throw CmUtils.CMyException.Raise(
						//	TError.ErridnInventory.MstSv_Mst_Network_GetByMST_NetworkNotFound
						//	, null
						//	, alParamsCoupleError.ToArray()
						//	);

						////
						MstSv_Mst_Network_CheckDB(
							ref alParamsCoupleError // alParamsCoupleError
							, dtDB_MstSv_OrgInNetwork.Rows[0]["NetworkID"] // objNetworkID
							, TConst.Flag.Yes // strFlagExistToCheck
							, TConst.Flag.Active // strFlagActiveListToCheck
							, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
							);

						strWSUrlAddr = Convert.ToString(dtDB_MstSv_Mst_Network.Rows[0]["WSUrlAddr"]);
					}
				}

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strWSUrlAddr);
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

		public DataSet MstSv_Mst_Network_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objNetworkName
			, object objWSUrlAddr
			, object objDBUrlAddr
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Create";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objNetworkID", objNetworkID
				, "objNetworkName", objNetworkName
				, "objWSUrlAddr", objWSUrlAddr
				, "objDBUrlAddr", objDBUrlAddr
				});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				MstSv_Sys_Administrator_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				string strNetworkName = string.Format("{0}", objNetworkName).Trim();
				string strWSUrlAddr = string.Format("{0}", objWSUrlAddr).Trim();
				string strDBUrlAddr = string.Format("{0}", objDBUrlAddr).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
				////
				DataTable dtDB_MstSv_Mst_Network = null;
				{
					////
					if (strNetworkID == null || strNetworkID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strNetworkID", strNetworkID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);
					////
					if (strNetworkName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strNetworkName", strNetworkName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // SaveDB MstSv_Mst_Network:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_MstSv_Mst_Network.NewRow();
					strFN = "NetworkID"; drDB[strFN] = strNetworkID;
					//strFN = "TenantId"; drDB[strFN] = nTenantId;
					strFN = "NetworkName"; drDB[strFN] = strNetworkName;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_MstSv_Mst_Network.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"MstSv_Mst_Network"
						, dtDB_MstSv_Mst_Network
						//, alColumnEffective.ToArray()
						);
				}
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
		public DataSet MstSv_Mst_Network_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objNetworkName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Update";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNetworkID", objNetworkID
				, "objNetworkName", objNetworkName
				, "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
				strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
				////
				string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				string strNetworkName = TUtils.CUtils.StdParam(objNetworkName);
				string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

				////
				bool bUpd_NetworkName = strFt_Cols_Upd.Contains("MstSv_Mst_Network.NetworkName".ToUpper());
				bool bUpd_FlagActive = strFt_Cols_Upd.Contains("MstSv_Mst_Network.FlagActive".ToUpper());

				////
				DataTable dtDB_MstSv_Mst_Network = null;
				{
					////
					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // objAreaMarketStatusListToCheck                        
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);
					////
					if (bUpd_NetworkName && strNetworkName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strNetworkName", strNetworkName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Update_InvalidNetworkName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
				}
				#endregion

				#region // SaveDB MstSv_Mst_Network:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_MstSv_Mst_Network.Rows[0];
					if (bUpd_NetworkName) { strFN = "NetworkName"; drDB[strFN] = strNetworkName; alColumnEffective.Add(strFN); }
					if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"MstSv_Mst_Network"
						, dtDB_MstSv_Mst_Network
						, alColumnEffective.ToArray()
						);
				}
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
		public DataSet MstSv_Mst_Network_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			/////
			, object objNetworkID
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNetworkID", objNetworkID
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				////
				DataTable dtDB_MstSv_Mst_Network = null;
				{
					////
					MstSv_Mst_Network_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strNetworkID // objNetworkID
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);
					////
				}
				#endregion

				#region // SaveDB MstSv_Mst_Network:
				{
					// Init:
					dtDB_MstSv_Mst_Network.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"MstSv_Mst_Network"
						, dtDB_MstSv_Mst_Network
						);
				}
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

		public DataSet MstSv_Mst_Network_Gen(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				bool bNetworkInitDone = false;

				////
				{
					////


					////
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select top 1 
								t.*
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_User.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_User.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select top 1 
								t.*
							from MstSv_Inos_Org t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_Org.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_Org.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org
							, null
							, alParamsCoupleError.ToArray()
							);
					}


				}
				#endregion

				#region // MQ_Mst_Network: Check.
				{
					string strSqlCheckDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
									and t.FlagActive = '1'
								;

							"
							, "@strMST", strMST
							);
					DataTable dtCheck= _cf.db.ExecQuery(
						strSqlCheckDB_MQ_Mst_Network
						).Tables[0];

					if (dtCheck.Rows.Count > 0)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST							
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_NetworkGenFinish
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // Save MQ_Mst_Network:
				////
				{
					string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								delete t
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
                                    and t.MST = @strMST
								;

							");
					_cf.db.ExecQuery(
						strSqlDelete
						, "@strMST", strMST
						);
				}

				//// Insert All:
				{
					////
					string zzzzClauseInsert_MQ_Mst_Network_zSave = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								insert into MQ_Mst_Network
								(
									NetworkID
									, MST
									, OrgID
									, OrgIDSln
									, WSUrlAddr
									, DBUrlAddr
									, FlagActive
									, LogLUDTimeUTC
									, LogLUBy
								)
								select
									msio.Id NetworkID
									, mnnt.MST MST
									, msio.Id OrgID
									, -1 OrgIDSln
									, null WSUrlAddr
									, null DBUrlAddr
									, '0' FlagActive
									, '@objLogLUDTimeUTC' LogLUDTimeUTC
									, '@objLogLUBy' LogLUBy 
								from Mst_NNT mnnt --//[mylock]
									left join MstSv_Inos_Org msio --//[mylock]
										on mnnt.MST = msio.MST
								where (1=1)
									and mnnt.MST = '@strMST'
								;
						"
						, "@strMST", strMST
						, "@objLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@objLogLUBy", strWAUserCode
						);

					////
					string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_MQ_Mst_Network_zSave
							----							
						"
						, "zzzzClauseInsert_MQ_Mst_Network_zSave", zzzzClauseInsert_MQ_Mst_Network_zSave
						);
					////
					DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

				}
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);

				#region // Check Init OK:
				_cf.db.BeginTransaction();
				////
				{
					////
					for (int nCheck = 0; nCheck < 10; nCheck++)
					{
						////
						string strGetDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
								;
							"
							, "@strMST", strMST
							);

						DataTable dt_MQ_Mst_Network = _cf.db.ExecQuery(strGetDB_MQ_Mst_Network).Tables[0];

						if (!CmUtils.StringUtils.StringEqual(dt_MQ_Mst_Network.Rows[0]["FlagActive"], TConst.Flag.Active))
						{
							Thread.Sleep(10000);
							continue;
						}
						else
						{
							string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
									---- Mst_NNT:
									update t
									set
										t.RegisterStatus = '@strRegisterStatus'
									from Mst_NNT t --//[mylock]
									where (1=1)
										and t.MST = '@strMST'
									;
								"
								, "@strMST", strMST
								, "@strRegisterStatus", TConst.RegisterStatus.Finish
								);

							_cf.db.ExecQuery(
								strSqlUpd_Mst_NNT
								);


							bNetworkInitDone = true;
							break;
						}
					}

					if (!bNetworkInitDone)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_InitFail
							, null
							, alParamsCoupleError.ToArray()
							);
					}

				}

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);
				#endregion

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

		public DataSet MstSv_Mst_Network_Gen_New20190913(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				bool bNetworkInitDone = false;

				////
				{
					////


					////
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select top 1 
								t.*
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_User.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_User.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select top 1 
								t.*
							from MstSv_Inos_Org t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_Org.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_Org.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org
							, null
							, alParamsCoupleError.ToArray()
							);
					}


				}
				#endregion

				#region // MQ_Mst_Network: Check.
				{
					string strSqlCheckDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
									and t.FlagActive = '1'
								;

							"
							, "@strMST", strMST
							);
					DataTable dtCheck = _cf.db.ExecQuery(
						strSqlCheckDB_MQ_Mst_Network
						).Tables[0];

					if (dtCheck.Rows.Count > 0)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_NetworkGenFinish
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // Save MQ_Mst_Network:
				////
				{
					string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								delete t
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
                                    and t.MST = @strMST
								;

							");
					_cf.db.ExecQuery(
						strSqlDelete
						, "@strMST", strMST
						);
				}

				//// Insert All:
				{
					////
					string zzzzClauseInsert_MQ_Mst_Network_zSave = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								insert into MQ_Mst_Network
								(
									NetworkID
									, MST
									, OrgID
									, OrgIDSln
									, WSUrlAddr
									, DBUrlAddr
									, FlagActive
									, LogLUDTimeUTC
									, LogLUBy
								)
								select
									msio.Id NetworkID
									, mnnt.MST MST
									, msio.Id OrgID
									, -1 OrgIDSln
									, null WSUrlAddr
									, null DBUrlAddr
									, '0' FlagActive
									, '@objLogLUDTimeUTC' LogLUDTimeUTC
									, '@objLogLUBy' LogLUBy 
								from Mst_NNT mnnt --//[mylock]
									left join MstSv_Inos_Org msio --//[mylock]
										on mnnt.MST = msio.MST
								where (1=1)
									and mnnt.MST = '@strMST'
								;
						"
						, "@strMST", strMST
						, "@objLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@objLogLUBy", strWAUserCode
						);

					////
					string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_MQ_Mst_Network_zSave
							----							
						"
						, "zzzzClauseInsert_MQ_Mst_Network_zSave", zzzzClauseInsert_MQ_Mst_Network_zSave
						);
					////
					DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

				}
				#endregion

				#region // Mst_NNT: Upd.
				{
					string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
							---- Mst_NNT:
							update t
							set
								t.RegisterStatus = '@strRegisterStatus'
							from Mst_NNT t --//[mylock]
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						, "@strRegisterStatus", TConst.RegisterStatus.InsertMQ
						);

					_cf.db.ExecQuery(
						strSqlUpd_Mst_NNT
						);
				}
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);

				#region // Check Init OK:
				_cf.db.BeginTransaction();
				////
				{
					////
					for (int nCheck = 0; nCheck < 10; nCheck++)
					{
						////
						string strGetDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
								;
							"
							, "@strMST", strMST
							);

						DataTable dt_MQ_Mst_Network = _cf.db.ExecQuery(strGetDB_MQ_Mst_Network).Tables[0];

						if (!CmUtils.StringUtils.StringEqual(dt_MQ_Mst_Network.Rows[0]["FlagActive"], TConst.Flag.Active))
						{
							Thread.Sleep(10000);
							continue;
						}
						else
						{
							string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
									---- Mst_NNT:
									update t
									set
										t.RegisterStatus = '@strRegisterStatus'
									from Mst_NNT t --//[mylock]
									where (1=1)
										and t.MST = '@strMST'
									;
								"
								, "@strMST", strMST
								, "@strRegisterStatus", TConst.RegisterStatus.Finish
								);

							_cf.db.ExecQuery(
								strSqlUpd_Mst_NNT
								);


							bNetworkInitDone = true;
							break;
						}
					}

					if (!bNetworkInitDone)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_InitFail
							, null
							, alParamsCoupleError.ToArray()
							);
					}

				}

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);
				#endregion

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

		public DataSet MstSv_Mst_Network_Gen_New20190829(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // MstSv_Mst_Network_GenX:
				MstSv_Mst_Network_GenX(
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
					, objMST // objMST
					);
				#endregion

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

		public DataSet MstSv_Mst_Network_GenAuto(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_NNT:
				////
				DataTable dt_Mst_NNT = null;
				{
					string strSqlGetDB_Mst_NNT = CmUtils.StringUtils.Replace(@"
							---- Mst_NNT:
							select 
								t.MST
							from Mst_NNT t --//[mylock]
							where (1=1)
								and t.RegisterStatus = '@strRegisterStatus'
							;
						"
						, "@strRegisterStatus", TConst.RegisterStatus.InsertMQ
						);

					dt_Mst_NNT = _cf.db.ExecQuery(strSqlGetDB_Mst_NNT).Tables[0];
				}
				#endregion

				#region // MstSv_Mst_Network_GenX:
				{
					for (int nScan = 0; nScan < dt_Mst_NNT.Rows.Count; nScan++)
					{
						try
						{
							// //
							DataRow drScan = dt_Mst_NNT.Rows[nScan];

							MstSv_Mst_Network_GenX(
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
								, objMST // objMST
								);
						}
						catch (Exception)
						{
							continue;
						}
						
					}
				}
				
				#endregion

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

		private void MstSv_Mst_Network_GenX(
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
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_GenX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			bool bNetworkInitDone = false;

			////
			DataTable dtDB_Mst_NNT = null;
			{
				////
				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, TConst.RegisterStatus.InsertMQ // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT
					);

				////
				string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select top 1 
								t.*
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
					, "@strMST", strMST
					);

				DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

				if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_User.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_User.Rows[0]["Id"]
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select top 1 
								t.*
							from MstSv_Inos_Org t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
					, "@strMST", strMST
					);

				DataTable dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

				if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_Org.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strMST", strMST
						, "Check.Id", dtDB_MstSv_Inos_Org.Rows[0]["Id"]
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org
						, null
						, alParamsCoupleError.ToArray()
						);
				}


			}
			#endregion

			#region // Check Init OK:
			//_cf.db.BeginTransaction();
			////
			{
				////
				for (int nCheck = 0; nCheck < 5; nCheck++)
				{
					////
					string strGetDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
								;
							"
						, "@strMST", strMST
						);

					DataTable dt_MQ_Mst_Network = _cf.db.ExecQuery(strGetDB_MQ_Mst_Network).Tables[0];

					if (!CmUtils.StringUtils.StringEqual(dt_MQ_Mst_Network.Rows[0]["FlagActive"], TConst.Flag.Active))
					{
						Thread.Sleep(10000);
						continue;
					}
					else
					{
						string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
									---- Mst_NNT:
									update t
									set
										t.RegisterStatus = '@strRegisterStatus'
									from Mst_NNT t --//[mylock]
									where (1=1)
										and t.MST = '@strMST'
									;
								"
							, "@strMST", strMST
							, "@strRegisterStatus", TConst.RegisterStatus.Finish
							);

						_cf.db.ExecQuery(
							strSqlUpd_Mst_NNT
							);


						bNetworkInitDone = true;
						break;
					}
				}

				if (!bNetworkInitDone)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strMST", strMST
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Mst_Network_Gen_InitFail
						, null
						, alParamsCoupleError.ToArray()
						);
				}

			}

			// Return Good:
			TDALUtils.DBUtils.CommitSafety(_cf.db);
			#endregion

			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}

		public DataSet RptSv_Mst_Network_InsertMQ(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_Network_InsertMQ";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_Network_InsertMQ;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				//bool bNetworkInitDone = false;

				////
				DataTable dtDB_Mst_NNT = null;
				{
					////
					Mst_NNT_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strMST // objMST
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, TConst.RegisterStatus.Approve // strTCTStatusListToCheck
						, out dtDB_Mst_NNT // dtDB_Mst_NNT
						);


					////
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select top 1 
								t.*
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_User.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_User.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_User
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select top 1 
								t.*
							from MstSv_Inos_Org t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_Org.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_Org.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_Org
							, null
							, alParamsCoupleError.ToArray()
							);
					}


				}
				#endregion

				#region // Save MQ_Mst_Network:
				////
				{
					string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								delete t
								from MQ_Mst_Network t
								where (1=1)
                                    and t.MST = @strMST
								;

							");
					_cf.db.ExecQuery(
						strSqlDelete
						, "@strMST", strMST
						);
				}

				//// Insert All:
				{
					////
					string zzzzClauseInsert_MQ_Mst_Network_zSave = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								insert into MQ_Mst_Network
								(
									NetworkID
									, MST
									, OrgID
									, OrgIDSln
									, WSUrlAddr
									, DBUrlAddr
									, FlagActive
									, LogLUDTimeUTC
									, LogLUBy
								)
								select
									msio.Id NetworkID
									, mnnt.MST MST
									, msio.Id OrgID
									, -1 OrgIDSln
									, null WSUrlAddr
									, null DBUrlAddr
									, '0' FlagActive
									, '@objLogLUDTimeUTC' LogLUDTimeUTC
									, '@objLogLUBy' LogLUBy 
								from Mst_NNT mnnt --//[mylock]
									left join MstSv_Inos_Org msio --//[mylock]
										on mnnt.MST = msio.MST
								where (1=1)
									and mnnt.MST = '@strMST'
								;
						"
						, "@strMST", strMST
						, "@objLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@objLogLUBy", strWAUserCode
						);

					////
					string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_MQ_Mst_Network_zSave
							----							
						"
						, "zzzzClauseInsert_MQ_Mst_Network_zSave", zzzzClauseInsert_MQ_Mst_Network_zSave
						);
					////
					DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

				}
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

		public DataSet WAS_MstSv_Mst_Network_Get(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Get;
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

                List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				bool bGet_MstSv_Mst_Network = (objRQ_MstSv_Mst_Network.Rt_Cols_MstSv_Mst_Network != null && objRQ_MstSv_Mst_Network.Rt_Cols_MstSv_Mst_Network.Length > 0);
				#endregion

				#region // WS_MstSv_Mst_Network_Get:
				mdsResult = MstSv_Mst_Network_Get(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_MstSv_Mst_Network.Ft_RecordStart // strFt_RecordStart
					, objRQ_MstSv_Mst_Network.Ft_RecordCount // strFt_RecordCount
					, objRQ_MstSv_Mst_Network.Ft_WhereClause // strFt_WhereClause
															 //// Return:
					, objRQ_MstSv_Mst_Network.Rt_Cols_MstSv_Mst_Network // strRt_Cols_MstSv_Mst_Network
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_MstSv_Mst_Network.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_MstSv_Mst_Network)
					{
						////
						DataTable dt_MstSv_Mst_Network = mdsResult.Tables["MstSv_Mst_Network"].Copy();
						lst_MstSv_Mst_Network = TUtils.DataTableCmUtils.ToListof<MstSv_Mst_Network>(dt_MstSv_Mst_Network);
						objRT_MstSv_Mst_Network.Lst_MstSv_Mst_Network = lst_MstSv_Mst_Network;
					}
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

		public DataSet WAS_MstSv_Mst_Network_GetByMST(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_GetByMST";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_GetByMST;
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

				List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				bool bGet_MstSv_Mst_Network = (objRQ_MstSv_Mst_Network.Rt_Cols_MstSv_Mst_Network != null && objRQ_MstSv_Mst_Network.Rt_Cols_MstSv_Mst_Network.Length > 0);
				#endregion

				#region // WS_MstSv_Mst_Network_GetByMST:
				mdsResult = MstSv_Mst_Network_GetByMST(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, objRQ_MstSv_Mst_Network.NetworkID // strNetworkID
					, objRQ_MstSv_Mst_Network.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST // objMST
					);
				#endregion

				#region // GetData:
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

		public DataSet WAS_MstSv_Mst_Network_Create(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MstSv_Mst_Network_Create:
				mdsResult = MstSv_Mst_Network_Create(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkID // objNetworkID
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkName // objNetworkName
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.WSUrlAddr // objWSUrlAddr
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.DBUrlAddr // objDBUrlAddr
					);
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

		public DataSet WAS_MstSv_Mst_Network_Update(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MstSv_Mst_Network_Update:
				mdsResult = MstSv_Mst_Network_Update(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkID // objNetworkID
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkName // objNetworkName
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.FlagActive // objFlagActive
																		   ////
					, objRQ_MstSv_Mst_Network.Ft_Cols_Upd// objFt_Cols_Upd
					);
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

		public DataSet WAS_MstSv_Mst_Network_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MstSv_Mst_Network_Delete:
				mdsResult = MstSv_Mst_Network_Delete(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkID // objNetworkID
					);
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

		public DataSet WAS_MstSv_Mst_Network_Gen(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MstSv_Mst_Network_Gen:
				mdsResult = MstSv_Mst_Network_Gen_New20190913(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, objRQ_MstSv_Mst_Network.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST // objNetworkID
					);
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

		public DataSet WAS_RptSv_Mst_Network_InsertMQ(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_Network_InsertMQ";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_Network_InsertMQ;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // RptSv_Mst_Network_InsertMQ:
				mdsResult = RptSv_Mst_Network_InsertMQ(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, objRQ_MstSv_Mst_Network.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST // objNetworkID
					);
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
		#endregion

		#region // MstSv_Sys_UserInNetWork:
		private void MstSv_Sys_UserInNetWork_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objUserCode
			, object objNetworkID
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Sys_UserInNetWork
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Sys_UserInNetWork t --//[mylock]
					where (1=1)
						and t.UserCode = @objUserCode
						and t.NetworkID = @objNetworkID
					;
				");
			dtDB_MstSv_Sys_UserInNetWork = _cf.db.ExecQuery(
				strSqlExec
				, "@objUserCode", objUserCode
				, "@objNetworkID", objNetworkID
				).Tables[0];
			dtDB_MstSv_Sys_UserInNetWork.TableName = "MstSv_Sys_UserInNetWork";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Sys_UserInNetWork.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						, "Check.NetworkID", objNetworkID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Sys_UserInNetWork.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCode", objUserCode
						, "Check.NetworkID", objNetworkID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Sys_UserInNetWork.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.UserCode", objUserCode
					, "Check.NetworkID", objNetworkID
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Sys_UserInNetWork.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Mstv_Seq_Common_Get:
		public DataSet MstSv_Seq_Common_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			// //
			, string strSequenceType
			, string strParam_Prefix
			, string strParam_Postfix
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strSequenceType", strSequenceType
				, "strParam_Prefix", strParam_Prefix
				, "strParam_Postfix", strParam_Postfix
				});
			TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
			#endregion

			try
			{
				#region // Convert Input:
				DateTime dtimeTDate = DateTime.UtcNow;
				#endregion

				#region // Init:
				dbLocal.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);
				#endregion

				#region // Refine and Check Input:
				////
				strSequenceType = TUtils.CUtils.StdParam(strSequenceType);
				strParam_Prefix = TUtils.CUtils.StdParam(strParam_Prefix);
				strParam_Postfix = TUtils.CUtils.StdParam(strParam_Postfix);
				#endregion

				#region // SequenceGet:
				////
				string strResult = Seq_Common_MyGet(
					ref alParamsCoupleError // alParamsCoupleError
					, strSequenceType // strSequenceType
					, strParam_Prefix // strParam_Prefix
					, strParam_Postfix // strParam_Postfix
					);
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(dbLocal); // Always Rollback.
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(dbLocal);

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
				TDALUtils.DBUtils.RollbackSafety(dbLocal);
				TDALUtils.DBUtils.ReleaseAllSemaphore(dbLocal, true);

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

		public DataSet WAS_Mstv_Seq_Common_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_Common objRQ_Seq_Common
			////
			, out RT_Seq_Common objRT_Seq_Common
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_Common.Tid;
			objRT_Seq_Common = new RT_Seq_Common();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mstv_Seq_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_Common_Get;
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
				#endregion

				#region // WS_Seq_Common_Get:
				mdsResult = MstSv_Seq_Common_Get(
					objRQ_Seq_Common.Tid // strTid
					, objRQ_Seq_Common.GwUserCode // strGwUserCode
					, objRQ_Seq_Common.GwPassword // strGwPassword
					, objRQ_Seq_Common.WAUserCode // strUserCode
					, objRQ_Seq_Common.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  // //
					, objRQ_Seq_Common.Seq_Common.SequenceType.ToString() // strSequenceType
					, objRQ_Seq_Common.Seq_Common.Param_Prefix.ToString() // Param_Prefix
					, objRQ_Seq_Common.Seq_Common.Param_Postfix.ToString() // Param_Postfix
					);
				#endregion

				#region // GetData:
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
		#endregion

		#region // MstSv_Inos_User:
		private void MstSv_Inos_User_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objMST
			, object objEmail
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Inos_User
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Inos_User t --//[mylock]
					where (1=1)
						and t.MST = @objMST
						and t.Email = @objEmail
					;
				");
			dtDB_MstSv_Inos_User = _cf.db.ExecQuery(
				strSqlExec
				, "@objMST", objMST
				, "@objEmail", objEmail
				).Tables[0];
			dtDB_MstSv_Inos_User.TableName = "MstSv_Inos_User";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Inos_User.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.MST", objMST
						, "Check.Email", objEmail
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_CheckDB_UserNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Inos_User.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.MST", objMST
						, "Check.Email", objEmail
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_CheckDB_UserExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Inos_User.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.MST", objMST
					, "Check.Email", objEmail
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Inos_User.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Inos_User_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void MstSv_Inos_User_BuildX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
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
			string strFunctionName = "MstSv_Inos_User_BuildX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Build;
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

			#region // Convert Input:
			//alParamsCoupleError.AddRange(new object[]{
			//	"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
			//	});
			#endregion

			#region //// Refine and Check:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);

            ////
            dsData = new DataSet();

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            //myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
            ////
            DataTable dtDB_Mst_NNT = null;
			{
                ////
                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );
                ////
            }
            ////
            #endregion

            #region //// SaveTemp Mst_NNT:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_NNT"
                    , new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strMST, // MST
							TConst.Flag.Active, // FlagActive
							dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
							strWAUserCode, // LogLUBy
							}
						}
					);
			}
            #endregion

            #region // Build MstSv_Inos_User:
            ////
            DataTable dt_MstSv_Inos_User = null;
            {
                ////
                string strSqlBuild_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_User:
                        select 
	                        t.MST
	                        , t.ContactEmail Email
	                        , t.ContactName Name
	                        , '@objPassword' Password
	                        , '@objLanguage' Language
	                        , @objTimeZone TimeZone
	                        , '@objUUID' UUID
	                        , @objId Id
	                        , '@objFlagEmailActivate' FlagEmailActivate
							, '@objFlagEmailSend' FlagEmailSend
							, '@objFlagAdmin' FlagAdmin
	                        , f.FlagActive
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from Mst_NNT t --//[mylock]
	                        inner join #input_Mst_NNT f --//[mylock]
		                        on t.MST = f.MST
                        where (1=1)
                        ;
                    "
					, "@objPassword", TConst.InosMix.Default_Password
                    , "@objLanguage", TConst.InosMix.Default_Language
                    , "@objTimeZone", TConst.InosMix.Default_TimeZone
                    , "@objUUID", TConst.InosMix.Default_Anonymous
                    , "@objId", TConst.InosMix.Default_Anonymous
                    , "@objFlagEmailActivate", TConst.Flag.Active
					, "@objFlagEmailSend", TConst.Flag.Active
					, "@objFlagAdmin", TConst.Flag.Active
					);

                dt_MstSv_Inos_User = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_User).Tables[0];
                dt_MstSv_Inos_User.TableName = "MstSv_Inos_User";
                dsData.Tables.Add(dt_MstSv_Inos_User.Copy());
            }
            #endregion

            #region // Build MstSv_Inos_Org:
            ////
            DataTable dt_MstSv_Inos_Org = null;
            {
                ////
                string strSqlBuild_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_Org:
                        select 
	                        t.MST
	                        , @objId Id
	                        , @objParentId ParentId
	                        , t.NNTFullName Name
	                        , t.BizType BizType
	                        , t.BizFieldCode BizField
	                        , t.BizSizeCode OrgSize
	                        , t.ContactName ContactName
	                        , t.ContactEmail Email
	                        , t.ContactPhone PhoneNo
	                        , null Description
	                        , '@objEnable' Enable
	                        , '@objCurrentUserRole' CurrentUserRole
	                        , f.FlagActive
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from Mst_NNT t --//[mylock]
	                        inner join #input_Mst_NNT f --//[mylock]
		                        on t.MST = f.MST
                        where (1=1)
                        ;
                    "
                    , "@objId", TConst.InosMix.Default_Anonymous
                    , "@objParentId", TConst.InosMix.Default_ParentId
                    , "@objEnable", TConst.Flag.Active
                    , "@objCurrentUserRole", OrgUserRoles.Admin
                    );

                dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_Org).Tables[0];
                dt_MstSv_Inos_Org.TableName = "MstSv_Inos_Org";
                dsData.Tables.Add(dt_MstSv_Inos_Org.Copy());
            }
            #endregion

            #region // Build MstSv_Inos_OrgUser:
            ////
            DataTable dt_MstSv_Inos_OrgUser = null;
            {
                ////
                string strSqlBuild_MstSv_Inos_OrgUser = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_OrgUser:
                        select 
							t.MST
							, @objUserId UserId
							, @objOrgId OrgId
							, t.ContactName Name
							, t.ContactEmail Email
							, '@objStatus' Status
							, '@objRole' Role
							, f.FlagActive
							, f.LogLUDTimeUTC
							, f.LogLUBy
						from Mst_NNT t --//[mylock]
							inner join #input_Mst_NNT f --//[mylock]
								on t.MST = f.MST
						where (1=1)
						;
                    "
					, "@objUserId", TConst.InosMix.Default_Anonymous
					, "@objOrgId", TConst.InosMix.Default_Anonymous
                    , "@objStatus", OrgUserStatuses.Active
                    , "@objRole", OrgUserRoles.Admin
                    );

                dt_MstSv_Inos_OrgUser = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_OrgUser).Tables[0];
                dt_MstSv_Inos_OrgUser.TableName = "MstSv_Inos_OrgUser";
                dsData.Tables.Add(dt_MstSv_Inos_OrgUser.Copy());
            }
			#endregion

			#region // Build MstSv_Inos_OrgInvite:
			////
			DataTable dt_MstSv_Inos_OrgInvite = null;
			{
				////
				string strSqlBuild_MstSv_Inos_OrgInvite = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_OrgInvite:
                        select 
							t.*
						from MstSv_Inos_OrgInvite t --//[mylock]
							inner join #input_Mst_NNT f --//[mylock]
								on t.MST = f.MST
						where (0=1)
						;
                    "
					, "@objUserId", TConst.InosMix.Default_Anonymous
					, "@objOrgId", TConst.InosMix.Default_Anonymous
					, "@objStatus", OrgUserStatuses.Active
					, "@objRole", OrgUserRoles.Admin
					);

				dt_MstSv_Inos_OrgInvite = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_OrgInvite).Tables[0];
				dt_MstSv_Inos_OrgInvite.TableName = "MstSv_Inos_OrgInvite";
				dsData.Tables.Add(dt_MstSv_Inos_OrgInvite.Copy());
			}
			#endregion

			#region // Build MstSv_Inos_Package:
			////
			DataTable dt_MstSv_Inos_Package = null;
			{
				////
				string strSqlBuild_MstSv_Inos_Package = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_Package:
                        select 
							t.MST
							, @PkgId PkgId
							, f.FlagActive
							, f.LogLUDTimeUTC
							, f.LogLUBy
						from Mst_NNT t --//[mylock]
							inner join #input_Mst_NNT f --//[mylock]
								on t.MST = f.MST
						where (1=1)
						;
                    "
					, "@PkgId", TConst.InosMix.Default_PkgId
					);

				dt_MstSv_Inos_Package = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_Package).Tables[0];
				dt_MstSv_Inos_Package.TableName = "MstSv_Inos_Package";
				dsData.Tables.Add(dt_MstSv_Inos_Package.Copy());
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_NNT;
						--drop table #input_MstSv_Inos_User;
						--drop table #input_MstSv_Inos_Org;
						--drop table #input_MstSv_Inos_OrgUser;
						--drop table #input_MstSv_Inos_OrgInvite;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		private void MstSv_Inos_User_BuildX_New20190817(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, DataSet dsData
			////
			, out DataSet dsDataOut
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "MstSv_Inos_User_BuildX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Build;
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

			#region // Convert Input:
			//alParamsCoupleError.AddRange(new object[]{
			//	"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
			//	});
			#endregion

			#region //// Refine and Check:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);

			////
			dsDataOut = new DataSet();

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			//myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
			////
			DataTable dtDB_Mst_NNT = null;
			{
				////
				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, "" // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT
					);
				////
			}
			////
			#endregion

			#region //// SaveTemp Mst_NNT:
			{
				////
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_NNT"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strMST, // MST
							TConst.Flag.Active, // FlagActive
							dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
							strWAUserCode, // LogLUBy
							}
						}
					);
			}
			#endregion

			#region // Build MstSv_Inos_User:
			////
			DataTable dt_MstSv_Inos_User = null;
			{
				////
				string strSqlBuild_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_User:
                        select 
	                        t.MST
	                        , t.ContactEmail Email
	                        , t.ContactName Name
	                        , '@objPassword' Password
	                        , '@objLanguage' Language
	                        , @objTimeZone TimeZone
	                        , '@objUUID' UUID
	                        , @objId Id
	                        , '@objFlagEmailActivate' FlagEmailActivate
							, '@objFlagEmailSend' FlagEmailSend
							, '@objFlagAdmin' FlagAdmin
	                        , f.FlagActive
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from Mst_NNT t --//[mylock]
	                        inner join #input_Mst_NNT f --//[mylock]
		                        on t.MST = f.MST
                        where (1=1)
                        ;
                    "
					, "@objPassword", TConst.InosMix.Default_Password
					, "@objLanguage", TConst.InosMix.Default_Language
					, "@objTimeZone", TConst.InosMix.Default_TimeZone
					, "@objUUID", TConst.InosMix.Default_Anonymous
					, "@objId", TConst.InosMix.Default_Anonymous
					, "@objFlagEmailActivate", TConst.Flag.Active
					, "@objFlagEmailSend", TConst.Flag.Active
					, "@objFlagAdmin", TConst.Flag.Active
					);

				dt_MstSv_Inos_User = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_User).Tables[0];
				dt_MstSv_Inos_User.TableName = "MstSv_Inos_User";
				dsDataOut.Tables.Add(dt_MstSv_Inos_User.Copy());
			}
			#endregion

			#region // Build MstSv_Inos_Org:
			////
			DataTable dt_MstSv_Inos_Org = null;
			{
				////
				string strSqlBuild_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_Org:
                        select 
	                        t.MST
	                        , @objId Id
	                        , @objParentId ParentId
	                        , t.NNTFullName Name
	                        , t.BizType BizType
	                        , t.BizFieldCode BizField
	                        , t.BizSizeCode OrgSize
	                        , t.ContactName ContactName
	                        , t.ContactEmail Email
	                        , t.ContactPhone PhoneNo
	                        , null Description
	                        , '@objEnable' Enable
	                        , '@objCurrentUserRole' CurrentUserRole
	                        , f.FlagActive
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from Mst_NNT t --//[mylock]
	                        inner join #input_Mst_NNT f --//[mylock]
		                        on t.MST = f.MST
                        where (1=1)
                        ;
                    "
					, "@objId", TConst.InosMix.Default_Anonymous
					, "@objParentId", TConst.InosMix.Default_ParentId
					, "@objEnable", TConst.Flag.Active
					, "@objCurrentUserRole", OrgUserRoles.Admin
					);

				dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_Org).Tables[0];
				dt_MstSv_Inos_Org.TableName = "MstSv_Inos_Org";
				dsDataOut.Tables.Add(dt_MstSv_Inos_Org.Copy());
			}
			#endregion

			#region // Build MstSv_Inos_OrgUser:
			////
			DataTable dt_MstSv_Inos_OrgUser = null;
			{
				////
				string strSqlBuild_MstSv_Inos_OrgUser = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_OrgUser:
                        select 
							t.MST
							, @objUserId UserId
							, @objOrgId OrgId
							, t.ContactName Name
							, t.ContactEmail Email
							, '@objStatus' Status
							, '@objRole' Role
							, f.FlagActive
							, f.LogLUDTimeUTC
							, f.LogLUBy
						from Mst_NNT t --//[mylock]
							inner join #input_Mst_NNT f --//[mylock]
								on t.MST = f.MST
						where (1=1)
						;
                    "
					, "@objUserId", TConst.InosMix.Default_Anonymous
					, "@objOrgId", TConst.InosMix.Default_Anonymous
					, "@objStatus", OrgUserStatuses.Active
					, "@objRole", OrgUserRoles.Admin
					);

				dt_MstSv_Inos_OrgUser = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_OrgUser).Tables[0];
				dt_MstSv_Inos_OrgUser.TableName = "MstSv_Inos_OrgUser";
				dsDataOut.Tables.Add(dt_MstSv_Inos_OrgUser.Copy());
			}
			#endregion

			#region // Build MstSv_Inos_OrgInvite:
			////
			DataTable dt_MstSv_Inos_OrgInvite = null;
			{
				////
				string strSqlBuild_MstSv_Inos_OrgInvite = CmUtils.StringUtils.Replace(@"
                        ---- MstSv_Inos_OrgInvite:
                        select 
							t.*
						from MstSv_Inos_OrgInvite t --//[mylock]
							inner join #input_Mst_NNT f --//[mylock]
								on t.MST = f.MST
						where (0=1)
						;
                    "
					, "@objUserId", TConst.InosMix.Default_Anonymous
					, "@objOrgId", TConst.InosMix.Default_Anonymous
					, "@objStatus", OrgUserStatuses.Active
					, "@objRole", OrgUserRoles.Admin
					);

				dt_MstSv_Inos_OrgInvite = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_OrgInvite).Tables[0];
				dt_MstSv_Inos_OrgInvite.TableName = "MstSv_Inos_OrgInvite";
				dsDataOut.Tables.Add(dt_MstSv_Inos_OrgInvite.Copy());
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_Package:
			////
			DataTable dtInput_MstSv_Inos_Package = null;
			{
				////
				string strTableCheck = "MstSv_Inos_Package";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_Package = dsData.Tables[strTableCheck];
				////
				if (dtInput_MstSv_Inos_Package.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblInvalid
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_Package // dtData
					, "StdParam", "PackageId" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "MST", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUDTime", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_Package.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_Package.Rows[nScan];

					
					////
					drScan["MST"] = strMST;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTime"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region // Build MstSv_Inos_Package:
			////
			//DataTable dt_MstSv_Inos_Package = null;
			//{
			//	////
			//	string strSqlBuild_MstSv_Inos_Package = CmUtils.StringUtils.Replace(@"
			//                     ---- MstSv_Inos_Package:
			//                     select 
			//				t.MST
			//				, @PkgId PkgId
			//				, f.FlagActive
			//				, f.LogLUDTimeUTC
			//				, f.LogLUBy
			//			from Mst_NNT t --//[mylock]
			//				inner join #input_Mst_NNT f --//[mylock]
			//					on t.MST = f.MST
			//			where (1=1)
			//			;
			//                 "
			//		, "@PkgId", TConst.InosMix.Default_PkgId
			//		);

			//	dt_MstSv_Inos_Package = _cf.db.ExecQuery(strSqlBuild_MstSv_Inos_Package).Tables[0];
			//	dt_MstSv_Inos_Package.TableName = "MstSv_Inos_Package";
			//	dsDataOut.Tables.Add(dt_MstSv_Inos_Package.Copy());
			//}

			dsDataOut.Tables.Add(dtInput_MstSv_Inos_Package.Copy());
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_NNT;
						--drop table #input_MstSv_Inos_User;
						--drop table #input_MstSv_Inos_Org;
						--drop table #input_MstSv_Inos_OrgUser;
						--drop table #input_MstSv_Inos_OrgInvite;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		private void MstSv_Inos_User_AddX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			, object objFlagEmailSend
			, object objFlagEmailActivate
			, object objFlagAdmin
			////
			, DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "MstSv_Inos_User_AddX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Add;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "objPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			alParamsCoupleError.AddRange(new object[]{
				"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
				});
			#endregion

			#region //// Refine and Check MstSv_Inos_User:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			string strName = string.Format("{0}", objName).Trim();
			string strPassword = string.Format("{0}", objPassword).Trim();
			string strLanguage = string.Format("{0}", objLanguage).Trim();
			Int32 nTimeZone = Convert.ToInt32(objTimeZone);
			string strFlagEmailSend = TUtils.CUtils.StdFlag(objFlagEmailSend);
			string strFlagEmailActivate = TUtils.CUtils.StdFlag(objFlagEmailActivate);
			string strFlagAdmin = TUtils.CUtils.StdFlag(objFlagAdmin);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			//myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
			////
			DataTable dtDB_MstSv_Inos_User = null;
			{
				////
				MstSv_Inos_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, strEmail // objEmail
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_MstSv_Inos_User // dtDB_MstSv_Inos_User
					);
				////
			}
			////
			#endregion

			#region //// SaveTemp MstSv_Inos_User:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_User"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"Email", TConst.BizMix.Default_DBColType,
						"Name", TConst.BizMix.Default_DBColType,
						"Password", TConst.BizMix.Default_DBColType,
						"Language", TConst.BizMix.Default_DBColType,
						"TimeZone", TConst.BizMix.Default_DBColType,
						"UUID", TConst.BizMix.Default_DBColType,
						"Id", TConst.BizMix.Default_DBColType,
						"FlagEmailSend", TConst.BizMix.Default_DBColType,
						"FlagEmailActivate", TConst.BizMix.Default_DBColType,
						"FlagAdmin", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strMST, // MST
							strEmail, // MST
							strName, // Name
							strPassword, // Password
							strLanguage, // Language
							nTimeZone, // TimeZone
							-1, // UUID
							-1, // Id
							strFlagEmailSend, // FlagEmailActivate
							strFlagEmailActivate, // FlagEmailActivate
							strFlagAdmin, // FlagAdmin
							TConst.Flag.Active, // FlagActive
							dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
							strWAUserCode, // LogLUBy
							}
						}
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_Org:
			////
			DataTable dtInput_MstSv_Inos_Org = null;
			{
				////
				string strTableCheck = "MstSv_Inos_Org";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_Org = dsData.Tables[strTableCheck];
				////
				//if (dtInput_MstSv_Inos_Org.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_Org // dtData
					, "StdParam", "MST" // arrstrCouple
					, "", "Id" // arrstrCouple
					, "", "ParentId" // arrstrCouple
					, "", "Name" // arrstrCouple
					, "StdParam", "BizType" // arrstrCouple
					, "StdParam", "BizField" // arrstrCouple
					, "StdParam", "OrgSize" // arrstrCouple
					, "", "ContactName" // arrstrCouple
					, "", "Email" // arrstrCouple
					, "", "PhoneNo" // arrstrCouple
					, "", "Description" // arrstrCouple
					, "", "Enable" // arrstrCouple
					, "", "CurrentUserRole" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_Org.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_Org.Rows[nScan];

					////
					drScan["FlagActive"] = TConst.CtrStatus.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp MstSv_Inos_Org:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_Org"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"Id", "float",
						"ParentId", "float",
						"Name", TConst.BizMix.Default_DBColType,
						"BizType", TConst.BizMix.Default_DBColType,
						"BizField", TConst.BizMix.Default_DBColType,
						"OrgSize", TConst.BizMix.Default_DBColType,
						"ContactName", TConst.BizMix.Default_DBColType,
						"Email", TConst.BizMix.Default_DBColType,
						"PhoneNo", TConst.BizMix.Default_DBColType,
						"Description", TConst.BizMix.Default_DBColType,
						"Enable", TConst.BizMix.Default_DBColType,
						"CurrentUserRole", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_MstSv_Inos_Org
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_OrgUser:
			////
			DataTable dtInput_MstSv_Inos_OrgUser = null;
			{
				////
				string strTableCheck = "MstSv_Inos_OrgUser";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_OrgUser = dsData.Tables[strTableCheck];
				////
				//if (dtInput_MstSv_Inos_OrgUser.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_OrgUser // dtData
					, "StdParam", "MST" // arrstrCouple
					, "", "UserId" // arrstrCouple
					, "", "OrgId" // arrstrCouple
					, "", "Name" // arrstrCouple
					, "", "Email" // arrstrCouple
					, "", "Status" // arrstrCouple
					, "", "Role" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_OrgUser.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_OrgUser.Rows[nScan];

					////
					drScan["FlagActive"] = TConst.CtrStatus.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp MstSv_Inos_OrgUser:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_OrgUser"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"UserId", "float",
						"OrgId", "float",
						"Name", TConst.BizMix.Default_DBColType,
						"Email", TConst.BizMix.Default_DBColType,
						"Status", TConst.BizMix.Default_DBColType,
						"Role", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_MstSv_Inos_OrgUser
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_OrgInvite:
			////
			DataTable dtInput_MstSv_Inos_OrgInvite = null;
			{
				////
				string strTableCheck = "MstSv_Inos_OrgInvite";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_OrgInvite = dsData.Tables[strTableCheck];
				////
				//if (dtInput_MstSv_Inos_OrgInvite.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_OrgInvite // dtData
					, "StdParam", "MST" // arrstrCouple
					, "", "UUID" // arrstrCouple
					, "", "UserId" // arrstrCouple
					, "", "OrgId" // arrstrCouple
					, "", "Email" // arrstrCouple
					, "", "OrgAdmin" // arrstrCouple
					, "", "Active" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_OrgInvite.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_OrgInvite.Rows[nScan];

					////
					drScan["FlagActive"] = TConst.CtrStatus.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp MstSv_Inos_OrgInvite:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_OrgInvite"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"UUID", TConst.BizMix.Default_DBColType,
						"UserId", "float",
						"OrgId", "float",
						"Email", TConst.BizMix.Default_DBColType,
						"OrgAdmin", TConst.BizMix.Default_DBColType,
						"Active", TConst.BizMix.Default_DBColType,
						"Role", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_MstSv_Inos_OrgInvite
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_Package:
			////
			DataTable dtInput_MstSv_Inos_Package = null;
			{
				////
				string strTableCheck = "MstSv_Inos_Package";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_Package = dsData.Tables[strTableCheck];
				////
				//if (dtInput_MstSv_Inos_Package.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_Package // dtData
					, "StdParam", "MST" // arrstrCouple
					, "", "PkgId" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_Package.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_Package.Rows[nScan];

					////
					drScan["FlagActive"] = TConst.CtrStatus.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp MstSv_Inos_Package:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_Package"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"PkgId", "float",
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_MstSv_Inos_Package
					);
			}
			#endregion

			#region // Check:
			{
				string strSqlCheck = CmUtils.StringUtils.Replace(@"	
						select * from #input_MstSv_Inos_User
					");

				//DataSet ds = _cf.db.ExecQuery(strSqlCheck);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				//string strSqlDelete = CmUtils.StringUtils.Replace(@"
				//			---- MstSv_Inos_UserDtl:
				//			delete t
				//			from MstSv_Inos_UserDtl t
				//			where (1=1)
				//				and t.MST = @strMST
				//			;

				//			---- MstSv_Inos_User:
				//			delete t
				//			from MstSv_Inos_User t
				//			where (1=1)
				//				and t.MST = @strMST
				//			;

				//		");
				//_cf.db.ExecQuery(
				//	strSqlDelete
				//	, "@strMST", strMST
				//	);
			}

			//// Insert All:
			{
				////
				string zzzzClauseInsert_MstSv_Inos_User_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_User:
						insert into MstSv_Inos_User
						(
							MST
							, Email
							, Name
							, Password
							, Language
							, TimeZone
							, UUID
							, Id
							, FlagEmailSend
							, FlagEmailActivate
							, FlagAdmin
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.Email
							, t.Name
							, t.Password
							, t.Language
							, t.TimeZone
							, t.UUID
							, t.Id
							, t.FlagEmailSend
							, t.FlagEmailActivate
							, t.FlagAdmin
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_User t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_Org_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Org:
						insert into MstSv_Inos_Org
						(
							MST
							, Id
							, ParentId
							, Name
							, BizType
							, BizField
							, OrgSize
							, ContactName
							, Email
							, PhoneNo
							, Description
							, Enable
							, CurrentUserRole
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.Id
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
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_Org t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_OrgUser_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_OrgUser:
						insert into MstSv_Inos_OrgUser
						(
							MST
							, UserId
							, OrgId
							, Name
							, Email
							, Status
							, Role
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.UserId
							, t.OrgId
							, t.Name
							, t.Email
							, t.Status
							, t.Role
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_OrgUser t --//[mylock]
						;
					");

				////
				string zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_OrgInvite:
						insert into MstSv_Inos_OrgInvite
						(
							MST
							, UUID
							, UserId
							, OrgId
							, Email
							, OrgAdmin
							, Active
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.UUID
							, t.UserId
							, t.OrgId
							, t.Email
							, t.OrgAdmin
							, t.Active
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_OrgInvite t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_Package_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Package:
						insert into MstSv_Inos_Package
						(
							MST
							, PkgId
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.PkgId
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_Package t --//[mylock]
						;
					");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_MstSv_Inos_User_zSave
			
						----
						zzzzClauseInsert_MstSv_Inos_Org_zSave

						----
						zzzzClauseInsert_MstSv_Inos_OrgUser_zSave

						----
						zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave

						----
						zzzzClauseInsert_MstSv_Inos_Package_zSave
					"
					, "zzzzClauseInsert_MstSv_Inos_User_zSave", zzzzClauseInsert_MstSv_Inos_User_zSave
					, "zzzzClauseInsert_MstSv_Inos_Org_zSave", zzzzClauseInsert_MstSv_Inos_Org_zSave
					, "zzzzClauseInsert_MstSv_Inos_OrgUser_zSave", zzzzClauseInsert_MstSv_Inos_OrgUser_zSave
					, "zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave", zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave
					, "zzzzClauseInsert_MstSv_Inos_Package_zSave", zzzzClauseInsert_MstSv_Inos_Package_zSave
					);
				////
				if (bMyDebugSql)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSqlExec", strSqlExec
						});
				}
				DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_MstSv_Inos_User;
						drop table #input_MstSv_Inos_Org;
						drop table #input_MstSv_Inos_OrgUser;
						drop table #input_MstSv_Inos_OrgInvite;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		private void RptSv_Inos_User_AddX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			, object objId
			, object objFlagEmailSend
			, object objFlagEmailActivate
			, object objFlagAdmin
			////
			, DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "RptSv_Inos_User_AddX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Add;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "objPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			alParamsCoupleError.AddRange(new object[]{
				"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
				});
			#endregion

			#region //// Refine and Check MstSv_Inos_User:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			string strName = string.Format("{0}", objName).Trim();
			string strPassword = string.Format("{0}", objPassword).Trim();
			string strLanguage = string.Format("{0}", objLanguage).Trim();
			Int32 nTimeZone = Convert.ToInt32(objTimeZone);
			string strFlagEmailSend = TUtils.CUtils.StdFlag(objFlagEmailSend);
			string strFlagEmailActivate = TUtils.CUtils.StdFlag(objFlagEmailActivate);
			string strFlagAdmin = TUtils.CUtils.StdFlag(objFlagAdmin);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			//myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
			////
			DataTable dtDB_MstSv_Inos_User = null;
			{
				////
				MstSv_Inos_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, strEmail // objEmail
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_MstSv_Inos_User // dtDB_MstSv_Inos_User
					);
				////
			}
			////
			#endregion

			#region //// SaveTemp MstSv_Inos_User:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_User"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"Email", TConst.BizMix.Default_DBColType,
						"Name", TConst.BizMix.Default_DBColType,
						"Password", TConst.BizMix.Default_DBColType,
						"Language", TConst.BizMix.Default_DBColType,
						"TimeZone", TConst.BizMix.Default_DBColType,
						"UUID", TConst.BizMix.Default_DBColType,
						"Id", TConst.BizMix.Default_DBColType,
						"FlagEmailSend", TConst.BizMix.Default_DBColType,
						"FlagEmailActivate", TConst.BizMix.Default_DBColType,
						"FlagAdmin", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strMST, // MST
							strEmail, // MST
							strName, // Name
							strPassword, // Password
							strLanguage, // Language
							nTimeZone, // TimeZone
							0, // UUID
							objId, // Id
							strFlagEmailSend, // FlagEmailActivate
							strFlagEmailActivate, // FlagEmailActivate
							strFlagAdmin, // FlagAdmin
							TConst.Flag.Active, // FlagActive
							dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
							strWAUserCode, // LogLUBy
							}
						}
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_Org:
			////
			DataTable dtInput_MstSv_Inos_Org = null;
			{
				////
				string strTableCheck = "MstSv_Inos_Org";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_MstSv_Inos_Org = dsData.Tables[strTableCheck];
				////
				//if (dtInput_MstSv_Inos_Org.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_MstSv_Inos_Org // dtData
					, "StdParam", "MST" // arrstrCouple
					, "", "Id" // arrstrCouple
					, "", "ParentId" // arrstrCouple
					, "", "Name" // arrstrCouple
					, "StdParam", "BizType" // arrstrCouple
					, "StdParam", "BizField" // arrstrCouple
					, "StdParam", "OrgSize" // arrstrCouple
					, "", "ContactName" // arrstrCouple
					, "", "Email" // arrstrCouple
					, "", "PhoneNo" // arrstrCouple
					, "", "Description" // arrstrCouple
					, "", "Enable" // arrstrCouple
					, "", "CurrentUserRole" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Org, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_MstSv_Inos_Org.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_MstSv_Inos_Org.Rows[nScan];

					////
					drScan["FlagActive"] = TConst.CtrStatus.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp MstSv_Inos_Org:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_MstSv_Inos_Org"
					, new object[]{
						"MST", TConst.BizMix.Default_DBColType,
						"Id", "float",
						"ParentId", "float",
						"Name", TConst.BizMix.Default_DBColType,
						"BizType", TConst.BizMix.Default_DBColType,
						"BizField", TConst.BizMix.Default_DBColType,
						"OrgSize", TConst.BizMix.Default_DBColType,
						"ContactName", TConst.BizMix.Default_DBColType,
						"Email", TConst.BizMix.Default_DBColType,
						"PhoneNo", TConst.BizMix.Default_DBColType,
						"Description", TConst.BizMix.Default_DBColType,
						"Enable", TConst.BizMix.Default_DBColType,
						"CurrentUserRole", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_MstSv_Inos_Org
					);
			}
			#endregion

			#region //// Refine and Check MstSv_Inos_OrgUser:
			//////
			//DataTable dtInput_MstSv_Inos_OrgUser = null;
			//{
			//	////
			//	string strTableCheck = "MstSv_Inos_OrgUser";
			//	////
			//	if (!dsData.Tables.Contains(strTableCheck))
			//	{
			//		alParamsCoupleError.AddRange(new object[]{
			//			"Check.TableName", strTableCheck
			//			});
			//		throw CmUtils.CMyException.Raise(
			//			TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblNotFound
			//			, null
			//			, alParamsCoupleError.ToArray()
			//			);
			//	}
			//	dtInput_MstSv_Inos_OrgUser = dsData.Tables[strTableCheck];
			//	////
			//	//if (dtInput_MstSv_Inos_OrgUser.Rows.Count < 1)
			//	//{
			//	//	alParamsCoupleError.AddRange(new object[]{
			//	//		"Check.TableName", strTableCheck
			//	//		});
			//	//	throw CmUtils.CMyException.Raise(
			//	//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblInvalid
			//	//		, null
			//	//		, alParamsCoupleError.ToArray()
			//	//		);
			//	//}
			//	////
			//	TUtils.CUtils.StdDataInTable(
			//		dtInput_MstSv_Inos_OrgUser // dtData
			//		, "StdParam", "MST" // arrstrCouple
			//		, "", "UserId" // arrstrCouple
			//		, "", "OrgId" // arrstrCouple
			//		, "", "Name" // arrstrCouple
			//		, "", "Email" // arrstrCouple
			//		, "", "Status" // arrstrCouple
			//		, "", "Role" // arrstrCouple
			//		);
			//	////
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "FlagActive", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "LogLUDTimeUTC", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgUser, "LogLUBy", typeof(object));
			//	////
			//	for (int nScan = 0; nScan < dtInput_MstSv_Inos_OrgUser.Rows.Count; nScan++)
			//	{
			//		////
			//		DataRow drScan = dtInput_MstSv_Inos_OrgUser.Rows[nScan];

			//		////
			//		drScan["FlagActive"] = TConst.CtrStatus.Active;
			//		drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
			//		drScan["LogLUBy"] = strWAUserCode;
			//		////
			//	}
			//}
			#endregion

			#region //// SaveTemp MstSv_Inos_OrgUser:
			//{
			//	TUtils.CUtils.MyBuildDBDT_Common(
			//		_cf.db
			//		, "#input_MstSv_Inos_OrgUser"
			//		, new object[]{
			//			"MST", TConst.BizMix.Default_DBColType,
			//			"UserId", "float",
			//			"OrgId", "float",
			//			"Name", TConst.BizMix.Default_DBColType,
			//			"Email", TConst.BizMix.Default_DBColType,
			//			"Status", TConst.BizMix.Default_DBColType,
			//			"Role", TConst.BizMix.Default_DBColType,
			//			"FlagActive", TConst.BizMix.Default_DBColType,
			//			"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
			//			"LogLUBy", TConst.BizMix.Default_DBColType,
			//			}
			//		, dtInput_MstSv_Inos_OrgUser
			//		);
			//}
			#endregion

			#region //// Refine and Check MstSv_Inos_OrgInvite:
			//////
			//DataTable dtInput_MstSv_Inos_OrgInvite = null;
			//{
			//	////
			//	string strTableCheck = "MstSv_Inos_OrgInvite";
			//	////
			//	if (!dsData.Tables.Contains(strTableCheck))
			//	{
			//		alParamsCoupleError.AddRange(new object[]{
			//			"Check.TableName", strTableCheck
			//			});
			//		throw CmUtils.CMyException.Raise(
			//			TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblNotFound
			//			, null
			//			, alParamsCoupleError.ToArray()
			//			);
			//	}
			//	dtInput_MstSv_Inos_OrgInvite = dsData.Tables[strTableCheck];
			//	////
			//	//if (dtInput_MstSv_Inos_OrgInvite.Rows.Count < 1)
			//	//{
			//	//	alParamsCoupleError.AddRange(new object[]{
			//	//		"Check.TableName", strTableCheck
			//	//		});
			//	//	throw CmUtils.CMyException.Raise(
			//	//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblInvalid
			//	//		, null
			//	//		, alParamsCoupleError.ToArray()
			//	//		);
			//	//}
			//	////
			//	TUtils.CUtils.StdDataInTable(
			//		dtInput_MstSv_Inos_OrgInvite // dtData
			//		, "StdParam", "MST" // arrstrCouple
			//		, "", "UUID" // arrstrCouple
			//		, "", "UserId" // arrstrCouple
			//		, "", "OrgId" // arrstrCouple
			//		, "", "Email" // arrstrCouple
			//		, "", "OrgAdmin" // arrstrCouple
			//		, "", "Active" // arrstrCouple
			//		);
			//	////
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "FlagActive", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "LogLUDTimeUTC", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_OrgInvite, "LogLUBy", typeof(object));
			//	////
			//	for (int nScan = 0; nScan < dtInput_MstSv_Inos_OrgInvite.Rows.Count; nScan++)
			//	{
			//		////
			//		DataRow drScan = dtInput_MstSv_Inos_OrgInvite.Rows[nScan];

			//		////
			//		drScan["FlagActive"] = TConst.CtrStatus.Active;
			//		drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
			//		drScan["LogLUBy"] = strWAUserCode;
			//		////
			//	}
			//}
			#endregion

			#region //// SaveTemp MstSv_Inos_OrgInvite:
			//{
			//	TUtils.CUtils.MyBuildDBDT_Common(
			//		_cf.db
			//		, "#input_MstSv_Inos_OrgInvite"
			//		, new object[]{
			//			"MST", TConst.BizMix.Default_DBColType,
			//			"UUID", TConst.BizMix.Default_DBColType,
			//			"UserId", "float",
			//			"OrgId", "float",
			//			"Email", TConst.BizMix.Default_DBColType,
			//			"OrgAdmin", TConst.BizMix.Default_DBColType,
			//			"Active", TConst.BizMix.Default_DBColType,
			//			"Role", TConst.BizMix.Default_DBColType,
			//			"FlagActive", TConst.BizMix.Default_DBColType,
			//			"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
			//			"LogLUBy", TConst.BizMix.Default_DBColType,
			//			}
			//		, dtInput_MstSv_Inos_OrgInvite
			//		);
			//}
			#endregion

			#region //// Refine and Check MstSv_Inos_Package:
			//////
			//DataTable dtInput_MstSv_Inos_Package = null;
			//{
			//	////
			//	string strTableCheck = "MstSv_Inos_Package";
			//	////
			//	if (!dsData.Tables.Contains(strTableCheck))
			//	{
			//		alParamsCoupleError.AddRange(new object[]{
			//			"Check.TableName", strTableCheck
			//			});
			//		throw CmUtils.CMyException.Raise(
			//			TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblNotFound
			//			, null
			//			, alParamsCoupleError.ToArray()
			//			);
			//	}
			//	dtInput_MstSv_Inos_Package = dsData.Tables[strTableCheck];
			//	////
			//	//if (dtInput_MstSv_Inos_Package.Rows.Count < 1)
			//	//{
			//	//	alParamsCoupleError.AddRange(new object[]{
			//	//		"Check.TableName", strTableCheck
			//	//		});
			//	//	throw CmUtils.CMyException.Raise(
			//	//		TError.ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblInvalid
			//	//		, null
			//	//		, alParamsCoupleError.ToArray()
			//	//		);
			//	//}
			//	////
			//	TUtils.CUtils.StdDataInTable(
			//		dtInput_MstSv_Inos_Package // dtData
			//		, "StdParam", "MST" // arrstrCouple
			//		, "", "PkgId" // arrstrCouple
			//		);
			//	////
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "FlagActive", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUDTimeUTC", typeof(object));
			//	TUtils.CUtils.MyForceNewColumn(ref dtInput_MstSv_Inos_Package, "LogLUBy", typeof(object));
			//	////
			//	for (int nScan = 0; nScan < dtInput_MstSv_Inos_Package.Rows.Count; nScan++)
			//	{
			//		////
			//		DataRow drScan = dtInput_MstSv_Inos_Package.Rows[nScan];

			//		////
			//		drScan["FlagActive"] = TConst.CtrStatus.Active;
			//		drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
			//		drScan["LogLUBy"] = strWAUserCode;
			//		////
			//	}
			//}
			#endregion

			#region //// SaveTemp MstSv_Inos_Package:
			//{
			//	TUtils.CUtils.MyBuildDBDT_Common(
			//		_cf.db
			//		, "#input_MstSv_Inos_Package"
			//		, new object[]{
			//			"MST", TConst.BizMix.Default_DBColType,
			//			"PkgId", "float",
			//			"FlagActive", TConst.BizMix.Default_DBColType,
			//			"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
			//			"LogLUBy", TConst.BizMix.Default_DBColType,
			//			}
			//		, dtInput_MstSv_Inos_Package
			//		);
			//}
			#endregion

			#region // Check:
			{
				string strSqlCheck = CmUtils.StringUtils.Replace(@"	
						select * from #input_MstSv_Inos_User
					");

				//DataSet ds = _cf.db.ExecQuery(strSqlCheck);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				//string strSqlDelete = CmUtils.StringUtils.Replace(@"
				//			---- MstSv_Inos_UserDtl:
				//			delete t
				//			from MstSv_Inos_UserDtl t
				//			where (1=1)
				//				and t.MST = @strMST
				//			;

				//			---- MstSv_Inos_User:
				//			delete t
				//			from MstSv_Inos_User t
				//			where (1=1)
				//				and t.MST = @strMST
				//			;

				//		");
				//_cf.db.ExecQuery(
				//	strSqlDelete
				//	, "@strMST", strMST
				//	);
			}

			//// Insert All:
			{
				////
				string zzzzClauseInsert_MstSv_Inos_User_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_User:
						insert into MstSv_Inos_User
						(
							MST
							, Email
							, Name
							, Password
							, Language
							, TimeZone
							, UUID
							, Id
							, FlagEmailSend
							, FlagEmailActivate
							, FlagAdmin
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.Email
							, t.Name
							, t.Password
							, t.Language
							, t.TimeZone
							, t.UUID
							, t.Id
							, t.FlagEmailSend
							, t.FlagEmailActivate
							, t.FlagAdmin
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_User t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_Org_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Org:
						insert into MstSv_Inos_Org
						(
							MST
							, Id
							, ParentId
							, Name
							, BizType
							, BizField
							, OrgSize
							, ContactName
							, Email
							, PhoneNo
							, Description
							, Enable
							, CurrentUserRole
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.Id
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
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_Org t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_OrgUser_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_OrgUser:
						insert into MstSv_Inos_OrgUser
						(
							MST
							, UserId
							, OrgId
							, Name
							, Email
							, Status
							, Role
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.UserId
							, t.OrgId
							, t.Name
							, t.Email
							, t.Status
							, t.Role
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_OrgUser t --//[mylock]
						;
					");

				////
				string zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_OrgInvite:
						insert into MstSv_Inos_OrgInvite
						(
							MST
							, UUID
							, UserId
							, OrgId
							, Email
							, OrgAdmin
							, Active
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.UUID
							, t.UserId
							, t.OrgId
							, t.Email
							, t.OrgAdmin
							, t.Active
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_OrgInvite t --//[mylock]
						;
					");
				////
				string zzzzClauseInsert_MstSv_Inos_Package_zSave = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Package:
						insert into MstSv_Inos_Package
						(
							MST
							, PkgId
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.MST
							, t.PkgId
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_MstSv_Inos_Package t --//[mylock]
						;
					");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_MstSv_Inos_User_zSave
			
						----
						zzzzClauseInsert_MstSv_Inos_Org_zSave

						----
						--zzzzClauseInsert_MstSv_Inos_OrgUser_zSave

						----
						--zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave

						----
						--zzzzClauseInsert_MstSv_Inos_Package_zSave
					"
					, "zzzzClauseInsert_MstSv_Inos_User_zSave", zzzzClauseInsert_MstSv_Inos_User_zSave
					, "zzzzClauseInsert_MstSv_Inos_Org_zSave", zzzzClauseInsert_MstSv_Inos_Org_zSave
					//, "zzzzClauseInsert_MstSv_Inos_OrgUser_zSave", zzzzClauseInsert_MstSv_Inos_OrgUser_zSave
					//, "zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave", zzzzClauseInsert_MstSv_Inos_OrgInvite_zSave
					//, "zzzzClauseInsert_MstSv_Inos_Package_zSave", zzzzClauseInsert_MstSv_Inos_Package_zSave
					);
				////
				if (bMyDebugSql)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSqlExec", strSqlExec
						});
				}
				DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_MstSv_Inos_User;
						drop table #input_MstSv_Inos_Org;
						--drop table #input_MstSv_Inos_OrgUser;
						--drop table #input_MstSv_Inos_OrgInvite;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		private void MstSv_Inos_User_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			, object objName
			, object objPassword
			, object objLanguage
			, object objTimeZone
			, object objUUID
			, object objId
			, object objFlagEmailActivate
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "MstSv_Inos_User_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.MstSv_Inos_User_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objMST", objMST
				, "objEmail", objEmail
				, "objName", objName
				, "objPassword", objPassword
				, "objLanguage", objLanguage
				, "objTimeZone", objTimeZone
				, "objUUID", objUUID
				, "objId", objId
				, "objFlagEmailActivate", objFlagEmailActivate
				, "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();
			string strName = string.Format("{0}", objName).Trim();
			string strPassword = string.Format("{0}", objPassword).Trim();
			string strLanguage = string.Format("{0}", objLanguage).Trim();
			Int32 nTimeZone = Convert.ToInt32(objTimeZone);
			string strUUID = string.Format("{0}", objUUID).Trim();
			double dblId = Convert.ToDouble(objId);
			string strFlagEmailActivate = TUtils.CUtils.StdFlag(objFlagEmailActivate);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_UUID = strFt_Cols_Upd.Contains("MstSv_Inos_User.UUID".ToUpper());
			bool bUpd_Id = strFt_Cols_Upd.Contains("MstSv_Inos_User.Id".ToUpper());
			bool bUpd_FlagEmailActivate = strFt_Cols_Upd.Contains("MstSv_Inos_User.FlagEmailActivate".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("MstSv_Inos_User.FlagActive".ToUpper());

			////
			DataTable dtDB_MstSv_Inos_User = null;
			{
				////
				MstSv_Inos_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, strEmail // objEmail
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_MstSv_Inos_User // dtDB_MstSv_Inos_User
					);
				////
				DataTable dtDB_Mst_NNT = null;

				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // strMST 
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, "" // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT
					);
				////
			}
			#endregion

			#region // Save Mst_Chain:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_MstSv_Inos_User.Rows[0];
				if (bUpd_UUID) { strFN = "UUID"; drDB[strFN] = strUUID; alColumnEffective.Add(strFN); }
				if (bUpd_Id) { strFN = "Id"; drDB[strFN] = objId; alColumnEffective.Add(strFN); }
				if (bUpd_FlagEmailActivate) { strFN = "FlagEmailActivate"; drDB[strFN] = strFlagEmailActivate; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"MstSv_Inos_User"
					, dtDB_MstSv_Inos_User
					, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		private void MstSv_Inos_User_ActivateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			//, string strAccessToken
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objEmail
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_User_ActivateX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Activate;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					, "objEmail", objEmail
					});
			#endregion

			#region // Init:
			//_cf.db.LogUserId = _cf.sinf.strUserCode;
			//_cf.db.BeginTransaction();

			// Write RequestLog:
			//_cf.ProcessBizReq_OutSide(
			//	strTid // strTid
			//	, strGwUserCode // strGwUserCode
			//	, strGwPassword // strGwPassword
			//	, strWAUserCode // objUserCode
			//	, strFunctionName // strFunctionName
			//	, alParamsCoupleError // alParamsCoupleError
			//	);

			//// rem lại do khong can dung user/pass để đăng ký NNT
			//// Sys_User_CheckAuthentication:
			//Sys_User_CheckAuthentication(
			//    ref alParamsCoupleError
			//    , strWAUserCode
			//    , strWAUserPassword
			//    );

			//// Check Access/Deny:
			//Sys_Access_CheckDenyV30(
			//    ref alParamsCoupleError
			//    , strWAUserCode
			//    , strFunctionName
			//    );
			#endregion

			#region // Refine and Check Input:
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strEmail = string.Format("{0}", objEmail).Trim();

			DataTable dtDB_MstSv_Inos_User = null;
			{
				////
				string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_User:
						select top 1
							t.*
						from MstSv_Inos_User t --//[mylock]
						where (1=1)
							and t.MST = '@strMST'
						;
					"
					, "@strMST", strMST
					);

				dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];
				////
			}
			#endregion

			#region // MasterServer:
			{
				// //
				DataSet dsData = new DataSet();

				Inos_AccountService_ActivateX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
										//, strAccessToken // strAccessToken
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   // //
					, dtDB_MstSv_Inos_User.Rows[0]["MST"] // objMST
					, dtDB_MstSv_Inos_User.Rows[0]["Email"] // objEmail
					, dtDB_MstSv_Inos_User.Rows[0]["UUID"] // objUUID
														   // //
					, out dsData // dsData
					);

				// //
				DataTable dtUpd_MstSv_Inos_User = dsData.Tables["MstSv_Inos_User"];

				if (dtUpd_MstSv_Inos_User.Rows.Count > 0)
				{
					object objFt_Cols_Upd = "MstSv_Inos_User.Id";

					MstSv_Inos_User_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   // //
						, objMST // objMST
						, dtUpd_MstSv_Inos_User.Rows[0]["Email"] // objEmail
						, dtUpd_MstSv_Inos_User.Rows[0]["Name"] // objName
						, dtUpd_MstSv_Inos_User.Rows[0]["Password"] // objPassword
						, dtUpd_MstSv_Inos_User.Rows[0]["Language"] // objLanguage
						, dtUpd_MstSv_Inos_User.Rows[0]["TimeZone"] // objTimeZone
						, dtUpd_MstSv_Inos_User.Rows[0]["UUID"] // objUUID
						, dtUpd_MstSv_Inos_User.Rows[0]["Id"] // objId
						, dtUpd_MstSv_Inos_User.Rows[0]["FlagEmailActivate"] // objFlagEmailActivate
						, dtUpd_MstSv_Inos_User.Rows[0]["FlagActive"] // objFlagActive
																	  ////
						, objFt_Cols_Upd // objFt_Cols_Upd
						);
				}

			}
			#endregion

			// Return Good:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}

		public DataSet MstSv_Inos_User_Activate(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			, object objEmail
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_User_Activate";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Activate;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					, "objEmail", objEmail
					});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // MstSv_Inos_User_ActivateX:
				//DataSet dsGetData = null;
				{
					MstSv_Inos_User_ActivateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objMST // objMST
						, objEmail // objEmail
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet MstSv_Inos_User_Activate_New20190611(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			, object objEmail
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_User_Activate";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_User_Activate;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					, "objEmail", objEmail
					});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Test Call Func:
				string strNetWorkUrl = null;
				string strInvoiceCode = "8797988080";
				#region // Call Func:
				RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
				{
					#region // WA_MstSv_Sys_User_Login:
					MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

					/////
					RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
					};
					////
					try
					{
						objRT_MstSv_Sys_User = OS_MstSvTVANService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
						strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
						////
					}
					catch (Exception cex)
					{
						TUtils.CProcessExc.BizShowException(
							ref alParamsCoupleError // alParamsCoupleError
							, cex // cex
							);

						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.CmSys_InvalidOutSite
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
				#endregion

				{
					RT_Invoice_Invoice objRT_Invoice_Invoice = null;
					{
						#region // WA_Invoice_Invoice_GetNoSession:
						RQ_Invoice_Invoice objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
						{
							WAUserCode = "SYSADMIN",
							WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
							GwUserCode = strOS_MasterServer_Solution_GwUserCode,
							GwPassword = strOS_MasterServer_Solution_GwPassword,
							//OrgID = strOrgID,
							Tid = strTid,
							Rt_Cols_Invoice_Invoice = "*",
							Ft_RecordStart = "0",
							Ft_RecordCount = "123456000",
							Ft_WhereClause = CmUtils.StringUtils.Replace(@"Invoice_Invoice.InvoiceCode = '@strInvoiceCode'", "@strInvoiceCode", strInvoiceCode)
						};

						////
						try
						{
							objRT_Invoice_Invoice = OS_MstSvTVAN_Invoice_InvoiceService.Instance.WA_Invoice_Invoice_GetNoSession(strNetWorkUrl, objRQ_Invoice_Invoice);

						}
						catch (Exception cex)
						{
							TUtils.CProcessExc.BizShowException(
								ref alParamsCoupleError // alParamsCoupleError
								, cex // cex
								);

							throw CmUtils.CMyException.Raise(
								TError.ErridnInventory.CmSys_InvalidOutSite
								, null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
				}
				#endregion

				#region // MstSv_Inos_User_ActivateX:
				//DataSet dsGetData = null;
				{
					MstSv_Inos_User_ActivateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objMST // objMST
						, objEmail // objEmail
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_MstSv_Inos_User_Activate(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Inos_User objRQ_MstSv_Inos_User
			////
			, out RT_MstSv_Inos_User objRT_MstSv_Inos_User
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Inos_User.Tid;
			objRT_MstSv_Inos_User = new RT_MstSv_Inos_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WA_MstSv_Inos_User_Activate";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Inos_User_Activate;
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
				List<MstSv_Inos_User> lst_MstSv_Inos_User = new List<MstSv_Inos_User>();
				#endregion

				#region // MstSv_Inos_User_Activate:
				mdsResult = MstSv_Inos_User_Activate(
					objRQ_MstSv_Inos_User.Tid // strTid
					, objRQ_MstSv_Inos_User.GwUserCode // strGwUserCode
					, objRQ_MstSv_Inos_User.GwPassword // strGwPassword
					, objRQ_MstSv_Inos_User.WAUserCode // strUserCode
					, objRQ_MstSv_Inos_User.WAUserPassword // strUserPassword
					, objRQ_MstSv_Inos_User.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Inos_User.MstSv_Inos_User.MST // objMST
					, objRQ_MstSv_Inos_User.MstSv_Inos_User.Email // objEmail
					);
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
		#endregion

		#region // MstSv_Inos_Org:
		private void MstSv_Inos_Org_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objMST
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_Inos_Org
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_Inos_Org t --//[mylock]
					where (1=1)
						and t.MST = @objMST
					;
				");
			dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(
				strSqlExec
				, "@objMST", objMST
				).Tables[0];
			dtDB_MstSv_Inos_Org.TableName = "MstSv_Inos_Org";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_Inos_Org.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.MST", objMST
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_Org_CheckDB_OrgNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_Inos_Org.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.MST", objMST
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_Inos_Org_CheckDB_OrgExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_Inos_Org.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.MST", objMST
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_Inos_Org.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_Inos_Org_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void MstSv_Inos_Org_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_Org_CreateX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_Activate;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					});
			#endregion

			#region // Init:
			//_cf.db.LogUserId = _cf.sinf.strUserCode;
			//_cf.db.BeginTransaction();

			// Write RequestLog:
			//_cf.ProcessBizReq_OutSide(
			//	strTid // strTid
			//	, strGwUserCode // strGwUserCode
			//	, strGwPassword // strGwPassword
			//	, strWAUserCode // objUserCode
			//	, strFunctionName // strFunctionName
			//	, alParamsCoupleError // alParamsCoupleError
			//	);

			//// rem lại do khong can dung user/pass để đăng ký NNT
			//// Sys_User_CheckAuthentication:
			//Sys_User_CheckAuthentication(
			//    ref alParamsCoupleError
			//    , strWAUserCode
			//    , strWAUserPassword
			//    );

			//// Check Access/Deny:
			//Sys_Access_CheckDenyV30(
			//    ref alParamsCoupleError
			//    , strWAUserCode
			//    , strFunctionName
			//    );
			#endregion

			#region // Refine and Check Input:
			string strMST = TUtils.CUtils.StdParam(objMST);

			// //
			DataTable dtDB_Mst_NNT = null;
			{
				/////
				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMSTParent
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, "" // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT_Parent
					);
				////
			}
			#endregion

			#region // MasterServer:
			{
				// //
				DataSet dsData = null;

				Inos_OrgService_CreateOrgX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, strAccessToken // strAccessToken
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   // //
					, objMST // objMST
					// //
					, out dsData // dsData
					);

				// //
				DataTable dtUpd_MstSv_Inos_Org = dsData.Tables["MstSv_Inos_Org"];

				if (dtUpd_MstSv_Inos_Org.Rows.Count > 0)
				{
					object objFt_Cols_Upd = "MstSv_Inos_Org.Id";

					MstSv_Inos_Org_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						// //
						, objMST // objMST
						, dtUpd_MstSv_Inos_Org.Rows[0]["Id"] // objId
						, dtUpd_MstSv_Inos_Org.Rows[0]["Id"] // objParentId
						, dtUpd_MstSv_Inos_Org.Rows[0]["Name"] // objName
						, dtUpd_MstSv_Inos_Org.Rows[0]["BizType"] // objBizType
						, dtUpd_MstSv_Inos_Org.Rows[0]["BizField"] // objBizField
						, dtUpd_MstSv_Inos_Org.Rows[0]["OrgSize"] // objOrgSize
						, dtUpd_MstSv_Inos_Org.Rows[0]["ContactName"] // objContactName
						, dtUpd_MstSv_Inos_Org.Rows[0]["Email"] // objEmail
						, dtUpd_MstSv_Inos_Org.Rows[0]["PhoneNo"] // objPhoneNo
						, dtUpd_MstSv_Inos_Org.Rows[0]["Description"] // objDescription
						, dtUpd_MstSv_Inos_Org.Rows[0]["CurrentUserRole"] // objCurrentUserRole
						, dtUpd_MstSv_Inos_Org.Rows[0]["FlagActive"] // objFlagActive
																	  ////
						, objFt_Cols_Upd // objFt_Cols_Upd
						);

					// //
					Inos_LicService_GetAllPackagesX(
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
						, strSolutionCode // objSolutionCode
						////
						, out dsData // dsData
						);

					DataTable dt_OS_Inos_Package = dsData.Tables["OS_Inos_Package"].Copy();

					// //
					List<long> lstPackageIds = new List<long>();

					for (int nScan = 0; nScan < dt_OS_Inos_Package.Rows.Count; nScan++)
					{
						////
						DataRow drScan = dt_OS_Inos_Package.Rows[nScan];
						long lgPackageId = Convert.ToInt64(drScan["Id"]);

						lstPackageIds.Add(lgPackageId);
					}

					Inos_LicService_RegisterPackagesX(
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
						, dtUpd_MstSv_Inos_Org.Rows[0]["Id"]  // objOrgId
						, lstPackageIds // lstPackageIds
						);
				}
			}
			#endregion

			// Return Good:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}

		private void MstSv_Inos_Org_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			, object objId
			, object objParentId
			, object objName
			, object objBizType
			, object objBizField
			, object objOrgSize
			, object objContactName
			, object objEmail
			, object objPhoneNo
			, object objDescription
			, object objCurrentUserRole
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "MstSv_Inos_Org_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.MstSv_Inos_Org_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objMST", objMST
				, "objId", objId
				, "objParentId", objParentId
				, "objName", objName
				, "objBizType", objBizType
				, "objBizField", objBizField
				, "objOrgSize", objOrgSize
				, "objContactName", objContactName
				, "objEmail", objEmail
				, "objPhoneNo", objPhoneNo
				, "objDescription", objDescription
				, "objCurrentUserRole", objCurrentUserRole
				, "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_Id = strFt_Cols_Upd.Contains("MstSv_Inos_Org.Id".ToUpper());

			////
			DataTable dtDB_MstSv_Inos_Org = null;
			{
				////
				MstSv_Inos_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_MstSv_Inos_Org // dtDB_MstSv_Inos_Org
					);
				////
				DataTable dtDB_Mst_NNT = null;

				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // strMST 
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, "" // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT
					);
				////
			}
			#endregion

			#region // Save Mst_Chain:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_MstSv_Inos_Org.Rows[0];
				if (bUpd_Id) { strFN = "Id"; drDB[strFN] = objId; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"MstSv_Inos_Org"
					, dtDB_MstSv_Inos_Org
					, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_MstSv_Inos_Org_BuildAndCreate(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Inos_Org objRQ_MstSv_Inos_Org
			////
			, out RT_MstSv_Inos_Org objRT_MstSv_Inos_Org
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Inos_Org.Tid;
			objRT_MstSv_Inos_Org = new RT_MstSv_Inos_Org();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_Org.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WA_MstSv_Inos_Org_BuildAndCreate";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Inos_Org_BuildAndCreate;
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
				List<MstSv_Inos_Org> lst_MstSv_Inos_Org = new List<MstSv_Inos_Org>();
				#endregion

				#region // MstSv_Inos_Org_BuildAndCreate:
				mdsResult = MstSv_Inos_Org_BuildAndCreate(
					objRQ_MstSv_Inos_Org.Tid // strTid
					, objRQ_MstSv_Inos_Org.GwUserCode // strGwUserCode
					, objRQ_MstSv_Inos_Org.GwPassword // strGwPassword
					, objRQ_MstSv_Inos_Org.WAUserCode // strUserCode
					, objRQ_MstSv_Inos_Org.WAUserPassword // strUserPassword
					, objRQ_MstSv_Inos_Org.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_MstSv_Inos_Org.MstSv_Inos_Org.MST // objMST
					);
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
		#endregion

		#region // MstSv_OrgInNetwork:
		private void MstSv_OrgInNetwork_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objNetworkID
			, object objOrgID
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MstSv_OrgInNetwork
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MstSv_OrgInNetwork t --//[mylock]
					where (1=1)
						and t.NetworkID = @objNetworkID
						and t.OrgID = @objOrgID
					;
				");
			dtDB_MstSv_OrgInNetwork = _cf.db.ExecQuery(
				strSqlExec
				, "@objNetworkID", objNetworkID
				, "@objOrgID", objOrgID
				).Tables[0];
			dtDB_MstSv_OrgInNetwork.TableName = "MstSv_OrgInNetwork";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MstSv_OrgInNetwork.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_OrgInNetwork_CheckDB_OrgInNetworkNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MstSv_OrgInNetwork.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MstSv_OrgInNetwork_CheckDB_OrgInNetworkExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MstSv_OrgInNetwork.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.NetworkID", objNetworkID
					, "Check.OrgID", objOrgID
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MstSv_OrgInNetwork.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MstSv_OrgInNetwork_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet MstSv_OrgInNetwork_GetOrgIDSln(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objOrgID
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_OrgInNetwork_GetOrgIDSln";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_OrgInNetwork_GetOrgIDSln;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNetworkID", objNetworkID
				, "objOrgID", objOrgID
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strNetworkIDInput = TUtils.CUtils.StdParam(objNetworkID);
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);

				////
				DataTable dtDB_MstSv_Mst_Network = null;
				DataTable dtDB_MstSv_OrgInNetwork = null;
				{
					////
					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkIDInput // objNetworkID
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);

					////
					MstSv_OrgInNetwork_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkIDInput // objNetworkID
						, strOrgID // objOrgID
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_MstSv_OrgInNetwork // dtDB_MstSv_OrgInNetwork
						);

				}

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dtDB_MstSv_OrgInNetwork.Rows[0]["OrgIDSln"]);
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

		public DataSet MstSv_OrgInNetwork_GetByOrgIDSln(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_OrgInNetwork_GetByOrgIDSln";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_OrgInNetwork_GetByOrgIDSln;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgIDSln", objOrgIDSln
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);

				////
				DataTable dtDB_MstSv_OrgInNetwork = null;
				{
					////
					string strSqlGetDB_MstSv_OrgInNetwork = CmUtils.StringUtils.Replace(@"
							---- MstSv_OrgInNetwork:
							select 
								t.*
							from MstSv_OrgInNetwork t --//[mylock]
							where (1=1)
								and t.OrgIDSln = '@strOrgIDSln'
							;
						"
						, "@strOrgIDSln", strOrgIDSln
						);
					////
					dtDB_MstSv_OrgInNetwork = _cf.db.ExecQuery(strSqlGetDB_MstSv_OrgInNetwork).Tables[0];
					dtDB_MstSv_OrgInNetwork.TableName = "MstSv_OrgInNetwork";
				}

				// Assign:
				if (dtDB_MstSv_OrgInNetwork.Rows.Count > 0)
				{
					CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dtDB_MstSv_OrgInNetwork.Rows[0]["NetworkID"]);
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dtDB_MstSv_OrgInNetwork);
				}
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

        public DataSet MstSv_OrgInNetwork_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objNetworkID
            , object objOrgParent
            , object objOrgID
            , object objMST
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "MstSv_OrgInNetwork_Create";
            string strErrorCodeDefault = TError.ErridnInventory.MstSv_OrgInNetwork_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objNetworkID", objNetworkID
                , "objOrgParent", objOrgParent
                , "objOrgID", objOrgID
                , "objMST", objMST
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

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
                //MstSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                MstSv_Sys_Administrator_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strNNetworkID = TUtils.CUtils.StdParam(objNetworkID);
                string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
                string strOrgID = TUtils.CUtils.StdParam(objOrgID);
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strOrgIDSln = null;
                ////
                // drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                ////
                DataTable dtDB_MstSv_OrgInNetwork = null;
                {
                    ////
                    if (strNNetworkID == null || strNNetworkID.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[] {
                            "Check.strNetworkID", strNNetworkID
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidNetworkID
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    MstSv_OrgInNetwork_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strNNetworkID // objNetworkID
                        , strOrgParent // objOrgParent
                        , TConst.Flag.Yes
                        , TConst.Flag.Active
                        , out dtDB_MstSv_OrgInNetwork
                        );
                    ////
                    strOrgIDSln = dtDB_MstSv_OrgInNetwork.Rows[0]["OrgIDSln"].ToString();
                    ////
                    if (strOrgIDSln.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strOrgIDSln", strOrgIDSln
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgIDSln
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strMST.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strMST", strMST
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strOrgID.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strOrgID", strOrgID
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgID
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveDB MstSv_OrgInNetwork
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_MstSv_OrgInNetwork.NewRow();
                    strFN = "NetworkID"; drDB[strFN] = strNNetworkID;
                    strFN = "OrgID"; drDB[strFN] = strOrgID;
                    strFN = "MST"; drDB[strFN] = strMST;
                    strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_MstSv_OrgInNetwork.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "MstSv_OrgInNetwork"
                        , dtDB_MstSv_OrgInNetwork
                        //, alColumnEffective.ToArray()
                        );
                }
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

		public DataSet MstSv_OrgInNetwork_Create_New20200207(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objOrgParent
			, object objOrgID
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_OrgInNetwork_Create";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_OrgInNetwork_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objNetworkID", objNetworkID
				, "objOrgParent", objOrgParent
				, "objOrgID", objOrgID
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//MstSv_Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				// Check Access/Deny:
				MstSv_Sys_Administrator_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strNNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = null;
				////
				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
				////
				DataTable dtDB_MstSv_OrgInNetwork = null;
				{
					////
					if (strNNetworkID == null || strNNetworkID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[] {
							"Check.strNetworkID", strNNetworkID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidNetworkID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					//MstSv_OrgInNetwork_CheckDB(
					//    ref alParamsCoupleError // alParamsCoupleError
					//    , strNNetworkID // objNetworkID
					//    , strOrgParent // objOrgParent
					//    , TConst.Flag.Yes
					//    , TConst.Flag.Active
					//    , out dtDB_MstSv_OrgInNetwork
					//    );
					string strSqlGet_MstSv_OrgIDSln = CmUtils.StringUtils.Replace(@"
							---- #tbl_MstSv_OrgIDSln:
							select top 1
								t.*
							from MstSv_OrgIDSln t --//[mylock]
							where (1=1)
								and t.FlagActive = '1'
							;
						"
						);
					DataTable dtGet_MstSv_OrgIDSln = _cf.db.ExecQuery(strSqlGet_MstSv_OrgIDSln).Tables[0];
					////
					strOrgIDSln = dtGet_MstSv_OrgIDSln.Rows[0]["OrgIDSln"].ToString();
					////
					if (strOrgIDSln.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strOrgIDSln", strOrgIDSln
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgIDSln
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strMST.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidMST
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strOrgID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strOrgID", strOrgID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // SaveDB MstSv_OrgInNetwork
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					//string strFN = "";
					//DataRow drDB = dtDB_MstSv_OrgInNetwork.NewRow();
					//strFN = "NetworkID"; drDB[strFN] = strNNetworkID;
					//strFN = "OrgID"; drDB[strFN] = strOrgID;
					//strFN = "MST"; drDB[strFN] = strMST;
					//strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
					//strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					//strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					//strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					//dtDB_MstSv_OrgInNetwork.Rows.Add(drDB);

					//// Save:
					//_cf.db.SaveData(
					//	"MstSv_OrgInNetwork"
					//	, dtDB_MstSv_OrgInNetwork
					//	//, alColumnEffective.ToArray()
					//	);
				}
				#endregion

				#region // Upd MstSv_OrgIDSln:
				string strSqlUpd_MstSv_OrgIDSln = CmUtils.StringUtils.Replace(@"
							---- Upd_MstSv_OrgIDSln:
							update t 
							set
								t.FlagActive = '0'
							from MstSv_OrgIDSln t --//[mylock]
							where(1=1)
								and t.OrgIDSln = '@strOrgIDSln'
							;

						"
						, "@strOrgIDSln", strOrgIDSln
						);
				_cf.db.ExecQuery(strSqlUpd_MstSv_OrgIDSln);
				#endregion

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strOrgIDSln);

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
		public DataSet WAS_MstSv_OrgInNetwork_Create(
            ref ArrayList alParamsCoupleError
            , RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork
            ////
            , out RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork
            )
        {
            #region // Temp:
            string strTid = objRQ_MstSv_OrgInNetwork.Tid;
            objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_MstSv_OrgInNetwork_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_OrgInNetwork_Create;
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
                List<MstSv_OrgInNetwork> lst_MstSv_OrgInNetwork = new List<MstSv_OrgInNetwork>();
                #endregion

                #region // WS_MstSv_OrgInNetwork_Create:
                mdsResult = MstSv_OrgInNetwork_Create_New20200207(
                    objRQ_MstSv_OrgInNetwork.Tid // strTid
                    , objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
                    , objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
                    , objRQ_MstSv_OrgInNetwork.WAUserCode // strUserCode
                    , objRQ_MstSv_OrgInNetwork.WAUserPassword // strUserPassword
                    , objRQ_MstSv_OrgInNetwork.NetworkID // NetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.NetworkID // NetworkID
                    , objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgParent // OrgID
                    , objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgID // objMSTParent
                    , objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.MST // objProvinceCode
                    );
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
        public DataSet WAS_MstSv_OrgInNetwork_GetOrgIDSln(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork
			////
			, out RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_OrgInNetwork.Tid;
			objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_OrgInNetwork_GetOrgIDSln";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_OrgInNetwork_GetOrgIDSln;
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
				////
				string strTokenID = TUtils.CUtils.StdParam(objRQ_MstSv_OrgInNetwork.TokenID);
				#endregion

				#region // WS_MstSv_OrgInNetwork_Get:
				mdsResult = MstSv_OrgInNetwork_GetOrgIDSln(
					objRQ_MstSv_OrgInNetwork.Tid // strTid
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					, objRQ_MstSv_OrgInNetwork.WAUserCode // strUserCode
					, objRQ_MstSv_OrgInNetwork.WAUserPassword // strUserPassword
					, objRQ_MstSv_OrgInNetwork.NetworkID // strNetworkID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.NetworkID // objNetworkID
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgID // objOrgID
					);
				#endregion

				#region // GetData:
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
		public DataSet WAS_MstSv_OrgInNetwork_GetByOrgIDSln(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork
			////
			, out RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_OrgInNetwork.Tid;
			objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_OrgInNetwork_GetByOrgIDSln";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_OrgInNetwork_GetByOrgIDSln;
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
				List<MstSv_OrgInNetwork> lst_MstSv_OrgInNetwork = new List<MstSv_OrgInNetwork>();
				////
				string strTokenID = TUtils.CUtils.StdParam(objRQ_MstSv_OrgInNetwork.TokenID);
				#endregion

				#region // WS_MstSv_OrgInNetwork_Get:
				mdsResult = MstSv_OrgInNetwork_GetByOrgIDSln(
					objRQ_MstSv_OrgInNetwork.Tid // strTid
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					, objRQ_MstSv_OrgInNetwork.WAUserCode // strUserCode
					, objRQ_MstSv_OrgInNetwork.WAUserPassword // strUserPassword
					, objRQ_MstSv_OrgInNetwork.NetworkID // strNetworkID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgIDSln // objOrgIDSln
					);
				#endregion

				#region // GetData:
				////
				DataTable dt_MstSv_OrgInNetwork = mdsResult.Tables["MstSv_OrgInNetwork"].Copy();
				lst_MstSv_OrgInNetwork = TUtils.DataTableCmUtils.ToListof<MstSv_OrgInNetwork>(dt_MstSv_OrgInNetwork);
				objRT_MstSv_OrgInNetwork.Lst_MstSv_OrgInNetwork = lst_MstSv_OrgInNetwork;
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

        public DataSet OS_MstSv_OrgInNetwork_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            //, object objNetworkID
            //, object objOrgParent
            , object objOrgID
            , object objMST
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_MstSv_OrgInNetwork_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSv_OrgInNetwork_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                //, "objNetworkID", objNetworkID
                //, "objOrgParent", objOrgParent
                , "objOrgID", objOrgID
                , "objMST", objMST
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string OrgParent = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                string strOrgID = TUtils.CUtils.StdParam(objOrgID);
                string strMST = TUtils.CUtils.StdParam(objMST);
				string strUrl = htCacheMstParam[TConst.Mst_Param.INVENTORY_RPTSV_URL].ToString();
				#endregion

				#region // Call Func:
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
                {
                    #region // Init:
                    MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
                    objMstSv_OrgInNetwork.OrgParent = OrgParent;
                    objMstSv_OrgInNetwork.OrgID = strOrgID;
                    objMstSv_OrgInNetwork.MST = strMST;
                    objMstSv_OrgInNetwork.NetworkID = nNetworkID.ToString();
                    /////
                    RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_Solution_API_Url,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                        GwPassword = strOS_MasterServer_Solution_GwPassword,
                        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
                        MstSv_OrgInNetwork = objMstSv_OrgInNetwork,
                    };
                    ////
                    try
                    {
						objRT_MstSv_OrgInNetwork = OS_RptSv_MstSv_OrgInNetworkServices.Instance.WA_OS_MstSv_OrgInNetwork_Create(strUrl, objRQ_MstSv_OrgInNetwork);
						////
						string strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark.ToString();

						CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strOrgIDSln);
					}
                    catch (Exception cex)
                    {
                        string strErrorCodeOS = null;

                        TUtils.CProcessExc.BizShowException(
                            ref alParamsCoupleError // alParamsCoupleError
                            , cex // cex
                            , out strErrorCodeOS
                            );

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
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
		public DataSet OS_MstSv_OrgInNetwork_Create_MstSv(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			//, object objNetworkID
			//, object objOrgParent
			, object objOrgID
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_MstSv_OrgInNetwork_Create_MstSv";
			string strErrorCodeDefault = TError.ErridnInventory.OS_MstSv_OrgInNetwork_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                //, "objNetworkID", objNetworkID
                //, "objOrgParent", objOrgParent
                , "objOrgID", objOrgID
				, "objMST", objMST
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				// drAbilityOfUser:
				DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
				zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
					drAbilityOfUser // drAbilityOfUser
					, ref alParamsCoupleError // alParamsCoupleError
					);
				string OrgParent = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				string strUrl = htCacheMstParam[TConst.Mst_Param.INVENTORY_RPTSV_URL].ToString();
				#endregion

				#region // Call Func:
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // Init:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.OrgParent = OrgParent;
					objMstSv_OrgInNetwork.OrgID = strOrgID;
					objMstSv_OrgInNetwork.MST = strMST;
					objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;
					objMstSv_OrgInNetwork.NetworkID = nNetworkID.ToString();
					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{
						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
						MstSv_OrgInNetwork = objMstSv_OrgInNetwork,
					};
					////
					try
					{
						string js = TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork);
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_Create_MstSv(objRQ_MstSv_OrgInNetwork);
						////
						strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark.ToString();

						CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strOrgIDSln);
					}
					catch (Exception cex)
					{
						TUtils.CProcessExc.BizShowException(
							ref alParamsCoupleError // alParamsCoupleError
							, cex // cex
							);

						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.CmSys_InvalidOutSite
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
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

		public DataSet MstSv_OrgInNetwork_Create_MstSv(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objOrgParent
			, object objOrgID
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_OrgInNetwork_Create";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_OrgInNetwork_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objNetworkID", objNetworkID
				, "objOrgParent", objOrgParent
				, "objOrgID", objOrgID
				, "objMST", objMST
				, "objOrgIDSln", objOrgIDSln
				});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//MstSv_Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				// Check Access/Deny:
				MstSv_Sys_Administrator_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strNNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				////
				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
				////
				DataTable dtDB_MstSv_OrgInNetwork = null;
				{
					////
					if (strNNetworkID == null || strNNetworkID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[] {
							"Check.strNetworkID", strNNetworkID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidNetworkID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					MstSv_OrgInNetwork_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNNetworkID // objNetworkID
						, strOrgParent // objOrgParent
						, TConst.Flag.Yes
						, TConst.Flag.Active
						, out dtDB_MstSv_OrgInNetwork
						);
					//              string strSqlGet_MstSv_OrgIDSln = CmUtils.StringUtils.Replace(@"
					//	---- #tbl_MstSv_OrgIDSln:
					//	select top 1
					//		t.*
					//	from MstSv_OrgIDSln t --//[mylock]
					//	where (1=1)
					//		and t.FlagActive = '1'
					//	;
					//"
					//                  );
					//              DataTable dtGet_MstSv_OrgIDSln = _cf.db.ExecQuery(strSqlGet_MstSv_OrgIDSln).Tables[0];
					//              ////
					//              strOrgIDSln = dtGet_MstSv_OrgIDSln.Rows[0]["OrgIDSln"].ToString();
					//              ////
					if (strOrgIDSln.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strOrgIDSln", strOrgIDSln
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgIDSln
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strMST.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidMST
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strOrgID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strOrgID", strOrgID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // SaveDB MstSv_OrgInNetwork
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_MstSv_OrgInNetwork.NewRow();
					strFN = "NetworkID"; drDB[strFN] = strNNetworkID;
					strFN = "OrgID"; drDB[strFN] = strOrgID;
					strFN = "MST"; drDB[strFN] = strMST;
					strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_MstSv_OrgInNetwork.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"MstSv_OrgInNetwork"
						, dtDB_MstSv_OrgInNetwork
						//, alColumnEffective.ToArray()
						);
				}
				#endregion

				#region // Upd MstSv_OrgIDSln:
				string strSqlUpd_MstSv_OrgIDSln = CmUtils.StringUtils.Replace(@"
							---- Upd_MstSv_OrgIDSln:
							update t 
							set
								t.FlagActive = '0'
							from MstSv_OrgIDSln t --//[mylock]
							where(1=1)
								and t.OrgIDSln = '@strOrgIDSln'
							;

						"
						, "@strOrgIDSln", strOrgIDSln
						);
				_cf.db.ExecQuery(strSqlUpd_MstSv_OrgIDSln);
				#endregion

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strOrgIDSln);

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

		public DataSet WAS_MstSv_OrgInNetwork_Create_MstSv(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork
			////
			, out RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_OrgInNetwork.Tid;
			objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_OrgInNetwork_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_OrgInNetwork_Create;
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
				List<MstSv_OrgInNetwork> lst_MstSv_OrgInNetwork = new List<MstSv_OrgInNetwork>();
				#endregion

				#region // WS_MstSv_OrgInNetwork_Create:
				mdsResult = MstSv_OrgInNetwork_Create_MstSv(
					objRQ_MstSv_OrgInNetwork.Tid // strTid
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					, objRQ_MstSv_OrgInNetwork.WAUserCode // strUserCode
					, objRQ_MstSv_OrgInNetwork.WAUserPassword // strUserPassword
					, objRQ_MstSv_OrgInNetwork.NetworkID // NetworkID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.NetworkID // NetworkID
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgParent // OrgID
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgID // objMSTParent
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.MST // objProvinceCode
					, objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork.OrgIDSln // objOrgIDSln
					);
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
		public void OS_MstSv_OrgInNetwork_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ // OrgID đăng nhập
			, ref ArrayList alParamsCoupleError
			////
			//, object objNetworkID
			//, object objOrgParent
			, object objOrgID // lấy trên Inos
			, object objMST
			, out string strOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_MstSv_OrgInNetwork_Create";
			//string strErrorCodeDefault = TError.ErridnInventory.OS_MstSv_OrgInNetwork_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                //, "objNetworkID", objNetworkID
                //, "objOrgParent", objOrgParent
                , "objOrgID", objOrgID
				, "objMST", objMST
				});
			#endregion

			#region // Refine and Check Input:
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			string OrgParent = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strUrl = htCacheMstParam[TConst.Mst_Param.INVENTORY_RPTSV_URL].ToString();

			#endregion

			#region // Call Func:
			RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			{
				#region // Init:
				MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
				objMstSv_OrgInNetwork.OrgParent = OrgParent;
				objMstSv_OrgInNetwork.OrgID = strOrgID;
				objMstSv_OrgInNetwork.MST = strMST;
				objMstSv_OrgInNetwork.NetworkID = nNetworkID.ToString();
				/////
				RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
				{
					Tid = strTid,
					TokenID = strOS_MasterServer_Solution_API_Url,
					NetworkID = nNetworkID.ToString(),
					GwUserCode = strOS_MasterServer_Solution_GwUserCode,
					GwPassword = strOS_MasterServer_Solution_GwPassword,
					WAUserCode = strOS_MasterServer_Solution_WAUserCode,
					WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					MstSv_OrgInNetwork = objMstSv_OrgInNetwork,
				};
				////
				try
				{
					objRT_MstSv_OrgInNetwork = OS_RptSv_MstSv_OrgInNetworkServices.Instance.WA_OS_MstSv_OrgInNetwork_Create(strUrl, objRQ_MstSv_OrgInNetwork);
					////
					strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark.ToString();

					//CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strOrgIDSln);
				}
				catch (Exception cex)
				{
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
						ref alParamsCoupleError // alParamsCoupleError
						, cex // cex
                        , out strErrorCodeOS
                        );

					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                        , null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				#endregion
			}
			#endregion
		}

		public void OS_MstSv_OrgInNetwork_Create_MstSvX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			//, object objNetworkID
			//, object objOrgParent
			, object objOrgID
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_MstSv_OrgInNetwork_Create_MstSvX";
			//string strErrorCodeDefault = TError.ErridnInventory.OS_MstSv_OrgInNetwork_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                //, "objNetworkID", objNetworkID
                //, "objOrgParent", objOrgParent
                , "objOrgID", objOrgID
				, "objMST", objMST
				});
			#endregion

			#region // Refine and Check Input:
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			string OrgParent = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
			//string strUrl = htCacheMstParam[TConst.Mst_Param.DMSPLUS_RPTSV_URL].ToString();
			#endregion

			#region // Call Func:
			RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			{
				#region // Init:
				MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
				objMstSv_OrgInNetwork.OrgParent = OrgParent;
				objMstSv_OrgInNetwork.OrgID = strOrgID;
				objMstSv_OrgInNetwork.MST = strMST;
				objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;
				objMstSv_OrgInNetwork.NetworkID = nNetworkID.ToString();
				/////
				RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
				{
					Tid = strTid,
					TokenID = strOS_MasterServer_Solution_API_Url,
					NetworkID = nNetworkID.ToString(),
					GwUserCode = strOS_MasterServer_Solution_GwUserCode,
					GwPassword = strOS_MasterServer_Solution_GwPassword,
					WAUserCode = strOS_MasterServer_Solution_WAUserCode,
					WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					MstSv_OrgInNetwork = objMstSv_OrgInNetwork,
				};
				////
				try
				{
					string js = TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork);
					objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_Create_MstSv(objRQ_MstSv_OrgInNetwork);
				}
				catch (Exception cex)
				{
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
						ref alParamsCoupleError // alParamsCoupleError
						, cex // cex
                        , out strErrorCodeOS
                        );

					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                        , null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				#endregion
			}
			#endregion
		}
		#endregion

		#region // MQ_Mst_Network:
		private void MQ_Mst_Network_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objNetworkID
			, object objOrgID
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_MQ_Mst_Network
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from MQ_Mst_Network t --//[mylock]
					where (1=1)
						and t.NetworkID = @objNetworkID
						and t.OrgID = @objOrgID
					;
				");
			dtDB_MQ_Mst_Network = _cf.db.ExecQuery(
				strSqlExec
				, "@objNetworkID", objNetworkID
				, "@objOrgID", objOrgID
				).Tables[0];
			dtDB_MQ_Mst_Network.TableName = "MQ_Mst_Network";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_MQ_Mst_Network.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MQ_Mst_Network_CheckDB_NetworkNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_MQ_Mst_Network.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.MQ_Mst_Network_CheckDB_NetworkExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_MQ_Mst_Network.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.NetworkID", objNetworkID
					, "Check.OrgID", objOrgID
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_MQ_Mst_Network.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.MQ_Mst_Network_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Map_Network_SysOutSide:
		private void Map_Network_SysOutSide_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objNetworkID
			, object objOrgID
			, object objSysOSCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Map_Network_SysOutSide
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Map_Network_SysOutSide t --//[mylock]
					where (1=1)
						and t.NetworkID = @objNetworkID
						and t.OrgID = @objOrgID
						and t.SysOSCode = @objSysOSCode
					;
				");
			dtDB_Map_Network_SysOutSide = _cf.db.ExecQuery(
				strSqlExec
				, "@objNetworkID", objNetworkID
				, "@objOrgID", objOrgID
				, "@objSysOSCode", objSysOSCode
				).Tables[0];
			dtDB_Map_Network_SysOutSide.TableName = "Map_Network_SysOutSide";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Map_Network_SysOutSide.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						, "Check.SysOSCode", objSysOSCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Map_Network_SysOutSide_CheckDB_NetworkSysOutSideNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Map_Network_SysOutSide.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.NetworkID", objNetworkID
						, "Check.OrgID", objOrgID
						, "Check.SysOSCode", objSysOSCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Map_Network_SysOutSide_CheckDB_NetworkSysOutSideExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Map_Network_SysOutSide.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.NetworkID", objNetworkID
					, "Check.OrgID", objOrgID
					, "Check.SysOSCode", objSysOSCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Map_Network_SysOutSide.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Map_Network_SysOutSide_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet Map_Network_SysOutSide_GetBySysOS(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objOrgID
			, object objSysOSCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Map_Network_SysOutSide_GetBySysOS";
			string strErrorCodeDefault = TError.ErridnInventory.Map_Network_SysOutSide_GetBySysOS;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNetworkID", objNetworkID
				, "objSysOSCode", objSysOSCode
				});
			#endregion

			try
			{
				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

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
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Refing and Check:
				////
				string strNetworkIDInput = TUtils.CUtils.StdParam(objNetworkID);
				string strOrgIDInput = TUtils.CUtils.StdParam(objOrgID);
				string strSysOSCode = TUtils.CUtils.StdParam(objSysOSCode);
				string strWSUrlAddr = "";

				////
				DataTable dtDB_Map_Network_SysOutSide = null;
				{
					////
					Map_Network_SysOutSide_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkIDInput // objNetworkID
						, strOrgIDInput // objOrgID
						, strSysOSCode // objSysOSCode
						, "" // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Map_Network_SysOutSide // dtDB_Map_Network_SysOutSide
						);

					if (dtDB_Map_Network_SysOutSide.Rows.Count > 0)
					{
						////
						Map_Network_SysOutSide_CheckDB(
							ref alParamsCoupleError // alParamsCoupleError
							, strNetworkIDInput // objNetworkID
							, strOrgIDInput // objOrgID
							, strSysOSCode // objSysOSCode
							, TConst.Flag.Yes // strFlagExistToCheck
							, TConst.Flag.Active // strFlagActiveListToCheck
							, out dtDB_Map_Network_SysOutSide // dtDB_Map_Network_SysOutSide
							);

						strWSUrlAddr = Convert.ToString(dtDB_Map_Network_SysOutSide.Rows[0]["WSUrlAddr"]);
					}

				}

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strWSUrlAddr);
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

		public DataSet WAS_Map_Network_SysOutSide_GetBySysOS(
			ref ArrayList alParamsCoupleError
			, RQ_Map_Network_SysOutSide objRQ_Map_Network_SysOutSide
			////
			, out RT_Map_Network_SysOutSide objRT_Map_Network_SysOutSide
			)
		{
			#region // Temp:
			string strTid = objRQ_Map_Network_SysOutSide.Tid;
			objRT_Map_Network_SysOutSide = new RT_Map_Network_SysOutSide();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Map_Network_SysOutSide.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Map_Network_SysOutSide_GetBySysOS";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Map_Network_SysOutSide_GetBySysOS;
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
				////
				string strTokenID = TUtils.CUtils.StdParam(objRQ_Map_Network_SysOutSide.TokenID);
				#endregion

				#region // WS_Map_Network_SysOutSide_Get:
				mdsResult = Map_Network_SysOutSide_GetBySysOS(
					objRQ_Map_Network_SysOutSide.Tid // strTid
					, objRQ_Map_Network_SysOutSide.GwUserCode // strGwUserCode
					, objRQ_Map_Network_SysOutSide.GwPassword // strGwPassword
					, objRQ_Map_Network_SysOutSide.WAUserCode // strUserCode
					, objRQ_Map_Network_SysOutSide.WAUserPassword // strUserPassword
					, objRQ_Map_Network_SysOutSide.NetworkID // strNetworkID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Map_Network_SysOutSide.NetworkID // objNetworkID
					, objRQ_Map_Network_SysOutSide.OrgID // objOrgID
					, objRQ_Map_Network_SysOutSide.Map_Network_SysOutSide.SysOSCode // objSysOSCode
					);
				#endregion

				#region // GetData:
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
		#endregion
	}
}
