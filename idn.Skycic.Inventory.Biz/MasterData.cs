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
using OSiNOSSv = inos.common.Service;
using inos.common.Model;
using inos.common.Service;
using System.Diagnostics;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Mst_ParamPrivate:
		private void Mst_ParamPrivate_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objParamCode
			, string strFlagExistToCheck
			//, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_ParamPrivate
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_ParamPrivate t --//[mylock]
					where (1=1)
						and t.ParamCode = @objParamCode
					;
				");
			dtDB_Mst_ParamPrivate = _cf.db.ExecQuery(
				strSqlExec
				, "@objParamCode", objParamCode
				).Tables[0];
			dtDB_Mst_ParamPrivate.TableName = "Mst_ParamPrivate";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_ParamPrivate.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.ParamCode", objParamCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_ParamPrivate_CheckDB_ParamPrivateNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_ParamPrivate.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.ParamCode", objParamCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_ParamPrivate_CheckDB_ParamPrivateExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			
		}
		public DataSet Mst_ParamPrivate_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_ParamPrivate
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_ParamPrivate_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_ParamPrivate_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_ParamPrivate", strRt_Cols_Mst_ParamPrivate
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

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
				bool bGet_Mst_ParamPrivate = (strRt_Cols_Mst_ParamPrivate != null && strRt_Cols_Mst_ParamPrivate.Length > 0);

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
						---- #tbl_Mst_ParamPrivate_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mprpv.ParamCode
						into #tbl_Mst_ParamPrivate_Filter_Draft
						from Mst_ParamPrivate mprpv --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mprpv.ParamCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_ParamPrivate_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_ParamPrivate_Filter:
						select
							t.*
						into #tbl_Mst_ParamPrivate_Filter
						from #tbl_Mst_ParamPrivate_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_ParamPrivate --------:
						zzB_Select_Mst_ParamPrivate_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_ParamPrivate_Filter_Draft;
						--drop table #tbl_Mst_ParamPrivate_Filter;
					"
					);
				////
				string zzB_Select_Mst_ParamPrivate_zzE = "-- Nothing.";
				if (bGet_Mst_ParamPrivate)
				{
					#region // bGet_Mst_ParamPrivate:
					zzB_Select_Mst_ParamPrivate_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_ParamPrivate:
							select
								t.MyIdxSeq
								, mprpv.*
							from #tbl_Mst_ParamPrivate_Filter t --//[mylock]
								inner join Mst_ParamPrivate mprpv --//[mylock]
									on t.ParamCode = mprpv.ParamCode
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
							, "Mst_ParamPrivate" // strTableNameDB
							, "Mst_ParamPrivate." // strPrefixStd
							, "mprpv." // strPrefixAlias
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
					, "zzB_Select_Mst_ParamPrivate_zzE", zzB_Select_Mst_ParamPrivate_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_Mst_ParamPrivate)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_ParamPrivate";
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
		public DataSet WAS_Mst_ParamPrivate_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate
			////
			, out RT_Mst_ParamPrivate objRT_Mst_ParamPrivate
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_ParamPrivate.Tid;
			objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_ParamPrivate_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ParamPrivate_Get;
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
				List<Mst_ParamPrivate> lst_Mst_ParamPrivate = new List<Mst_ParamPrivate>();
				bool bGet_Mst_ParamPrivate = (objRQ_Mst_ParamPrivate.Rt_Cols_Mst_ParamPrivate != null && objRQ_Mst_ParamPrivate.Rt_Cols_Mst_ParamPrivate.Length > 0);
				#endregion

				#region // WS_Mst_ParamPrivate_Get:
				mdsResult = Mst_ParamPrivate_Get(
					objRQ_Mst_ParamPrivate.Tid // strTid
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					, objRQ_Mst_ParamPrivate.WAUserCode // strUserCode
					, objRQ_Mst_ParamPrivate.WAUserPassword // strUserPassword
					, objRQ_Mst_ParamPrivate.AccessToken // strAccessToken
					, objRQ_Mst_ParamPrivate.NetworkID // strNetworkID
					, objRQ_Mst_ParamPrivate.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_ParamPrivate.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_ParamPrivate.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_ParamPrivate.Ft_WhereClause // strFt_WhereClause
														//// Return:
					, objRQ_Mst_ParamPrivate.Rt_Cols_Mst_ParamPrivate // strRt_Cols_Mst_ParamPrivate
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_ParamPrivate.MySummaryTable = lst_MySummaryTable[0];
					////
					if (bGet_Mst_ParamPrivate)
					{
						////
						DataTable dt_Mst_ParamPrivate = mdsResult.Tables["Mst_ParamPrivate"].Copy();
						lst_Mst_ParamPrivate = TUtils.DataTableCmUtils.ToListof<Mst_ParamPrivate>(dt_Mst_ParamPrivate);
						objRT_Mst_ParamPrivate.Lst_Mst_ParamPrivate = lst_Mst_ParamPrivate;
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
		public DataSet Mst_ParamPrivate_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objParamCode
			, object objParamValue
			)

		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_ParamPrivate_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_ParamPrivate_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
					, "objParamCode", objParamCode
					, "objParamValue", objParamValue
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				string strParamValue = string.Format("{0}", objParamValue).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
				////
				DataTable dtDB_Mst_ParamPrivate = null;

				{
					////
					if (strParamCode == null || strParamCode.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
						"Check.strParamCode", strParamCode
						});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_ParamPrivate_Create_InvalidParamCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					Mst_ParamPrivate_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strParamCode // objParamCode
						, TConst.Flag.No // strFlagExistToCheck
										 //, "" // strFlagActiveListToCheck
						, out dtDB_Mst_ParamPrivate // dtDB_Mst_ParamPrivate
						);
					////
				}
				#endregion

				#region // SaveDB Mst_ParamPrivate:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_ParamPrivate.NewRow();
					strFN = "ParamCode"; drDB[strFN] = strParamCode;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "ParamValue"; drDB[strFN] = strParamValue;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_Mst_ParamPrivate.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"Mst_ParamPrivate" // strTableName
						, dtDB_Mst_ParamPrivate // dtData
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
		public DataSet WAS_Mst_ParamPrivate_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate
			////
			, out RT_Mst_ParamPrivate objRT_Mst_ParamPrivate
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_ParamPrivate.Tid;
			objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_ParamPrivate_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ParamPrivate_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_ParamPrivate", TJson.JsonConvert.SerializeObject(objRQ_Mst_ParamPrivate.Mst_ParamPrivate)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_ParamPrivate> lst_Mst_ParamPrivate = new List<Mst_ParamPrivate>();
				//List<Mst_ParamPrivateInGroup> lst_Mst_ParamPrivateInGroup = new List<Mst_ParamPrivateInGroup>();
				#endregion

				#region // Mst_ParamPrivate_Create:
				mdsResult = Mst_ParamPrivate_Create(
					objRQ_Mst_ParamPrivate.Tid // strTid
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					, objRQ_Mst_ParamPrivate.WAUserCode // strUserCode
					, objRQ_Mst_ParamPrivate.WAUserPassword // strUserPassword
					, objRQ_Mst_ParamPrivate.AccessToken // strAccessToken
					, objRQ_Mst_ParamPrivate.NetworkID // strNetworkID
					, objRQ_Mst_ParamPrivate.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_ParamPrivate.Mst_ParamPrivate.ParamCode // objParamCode
					, objRQ_Mst_ParamPrivate.Mst_ParamPrivate.ParamValue // objParamValue
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
		public DataSet Mst_ParamPrivate_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objParamCode
			, object objParamValue
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_ParamPrivate_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_ParamPrivate_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objParamCode", objParamCode
					, "objParamValue", objParamValue
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

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
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				string strParamValue = TUtils.CUtils.StdParam(objParamValue);
				////
				bool bUpd_ParamValue = strFt_Cols_Upd.Contains("Mst_ParamPrivate.ParamValue".ToUpper());

				////
				DataTable dtDB_Mst_ParamPrivate = null;
				{
					////
					Mst_ParamPrivate_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strParamCode // objParamCode 
						 , TConst.Flag.Yes // strFlagExistToCheck
						 //, "" // strFlagActiveListToCheck
						 , out dtDB_Mst_ParamPrivate // dtDB_Mst_ParamPrivate
						);
					////
				}
				#endregion

				#region // Save Mst_ParamPrivate:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_ParamPrivate.Rows[0];
					if (bUpd_ParamValue) { strFN = "ParamValue"; drDB[strFN] = strParamValue; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"Mst_ParamPrivate"
						, dtDB_Mst_ParamPrivate
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
		public DataSet WAS_Mst_ParamPrivate_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate
			////
			, out RT_Mst_ParamPrivate objRT_Mst_ParamPrivate
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_ParamPrivate.Tid;
			objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_ParamPrivate_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ParamPrivate_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_ParamPrivate", TJson.JsonConvert.SerializeObject(objRQ_Mst_ParamPrivate.Mst_ParamPrivate)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_ParamPrivate> lst_Mst_ParamPrivate = new List<Mst_ParamPrivate>();
				//List<Mst_ParamPrivateInGroup> lst_Mst_ParamPrivateInGroup = new List<Mst_ParamPrivateInGroup>();
				#endregion

				#region // Mst_ParamPrivate_Update:
				mdsResult = Mst_ParamPrivate_Update(
					objRQ_Mst_ParamPrivate.Tid // strTid
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					, objRQ_Mst_ParamPrivate.WAUserCode // strUserCode
					, objRQ_Mst_ParamPrivate.WAUserPassword // strUserPassword
					, objRQ_Mst_ParamPrivate.AccessToken // strAccessToken
					, objRQ_Mst_ParamPrivate.NetworkID // strNetworkID
					, objRQ_Mst_ParamPrivate.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_ParamPrivate.Mst_ParamPrivate.ParamCode // objParamCode
					, objRQ_Mst_ParamPrivate.Mst_ParamPrivate.ParamValue // objParamValue
					////
					, objRQ_Mst_ParamPrivate.Ft_Cols_Upd// objFt_Cols_Upd
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
		public DataSet Mst_ParamPrivate_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			/////
			, object objParamCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_ParamPrivate_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_ParamPrivate_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objParamCode", objParamCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				////
				DataTable dtDB_Mst_ParamPrivate = null;
				{
					////
					Mst_ParamPrivate_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strParamCode // objParamCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 //, "" // strFlagActiveListToCheck
						 , out dtDB_Mst_ParamPrivate // dtDB_Mst_ParamPrivate
						);
				}
				#endregion

				#region // SaveDB Mst_ParamPrivate:
				{
					// Init:
					dtDB_Mst_ParamPrivate.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"Mst_ParamPrivate"
						, dtDB_Mst_ParamPrivate
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
		public DataSet WAS_Mst_ParamPrivate_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_ParamPrivate objRQ_Mst_ParamPrivate
			////
			, out RT_Mst_ParamPrivate objRT_Mst_ParamPrivate
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_ParamPrivate.Tid;
			objRT_Mst_ParamPrivate = new RT_Mst_ParamPrivate();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ParamPrivate.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_ParamPrivate_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ParamPrivate_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_ParamPrivate", TJson.JsonConvert.SerializeObject(objRQ_Mst_ParamPrivate.Mst_ParamPrivate)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_ParamPrivate> lst_Mst_ParamPrivate = new List<Mst_ParamPrivate>();
				//List<Mst_ParamPrivateInGroup> lst_Mst_ParamPrivateInGroup = new List<Mst_ParamPrivateInGroup>();
				#endregion

				#region // Mst_ParamPrivate_Delete:
				mdsResult = Mst_ParamPrivate_Delete(
					objRQ_Mst_ParamPrivate.Tid // strTid
					, objRQ_Mst_ParamPrivate.GwUserCode // strGwUserCode
					, objRQ_Mst_ParamPrivate.GwPassword // strGwPassword
					, objRQ_Mst_ParamPrivate.WAUserCode // strUserCode
					, objRQ_Mst_ParamPrivate.WAUserPassword // strUserPassword
					, objRQ_Mst_ParamPrivate.AccessToken // strAccessToken
					, objRQ_Mst_ParamPrivate.NetworkID // strNetworkID
					, objRQ_Mst_ParamPrivate.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_ParamPrivate.Mst_ParamPrivate.ParamCode // objParamCode
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

		#region // Mst_Param:
		private void Mst_Param_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objParamCode
			, string strFlagExistToCheck
			//, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Param
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Param t --//[mylock]
					where (1=1)
						and t.ParamCode = @objParamCode
					;
				");
			dtDB_Mst_Param = _cf.db.ExecQuery(
				strSqlExec
				, "@objParamCode", objParamCode
				).Tables[0];
			dtDB_Mst_Param.TableName = "Mst_Param";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Param.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.ParamCode", objParamCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Param_CheckDB_ParamCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Param.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.ParamCode", objParamCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Param_CheckDB_ParamCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			
		}
		public DataSet WAS_Mst_Param_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Param objRQ_Mst_Param
			////
			, out RT_Mst_Param objRT_Mst_Param
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Param.Tid;
			objRT_Mst_Param = new RT_Mst_Param();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Param.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Param_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Param_Get;
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
				List<Mst_Param> lst_Mst_Param = new List<Mst_Param>();
				bool bGet_Mst_Param = (objRQ_Mst_Param.Rt_Cols_Mst_Param != null && objRQ_Mst_Param.Rt_Cols_Mst_Param.Length > 0);
				#endregion

				#region // WS_Mst_Param_Get:
				mdsResult = Mst_Param_Get(
					objRQ_Mst_Param.Tid // strTid
					, objRQ_Mst_Param.GwUserCode // strGwUserCode
					, objRQ_Mst_Param.GwPassword // strGwPassword
					, objRQ_Mst_Param.WAUserCode // strUserCode
					, objRQ_Mst_Param.WAUserPassword // strUserPassword
					, objRQ_Mst_Param.AccessToken // strAccessToken
					, objRQ_Mst_Param.NetworkID // strNetworkID
					, objRQ_Mst_Param.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Param.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Param.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Param.Ft_WhereClause // strFt_WhereClause
															//// Return:
					, objRQ_Mst_Param.Rt_Cols_Mst_Param // strRt_Cols_Mst_Param
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Param.MySummaryTable = lst_MySummaryTable[0];
					////
					if (bGet_Mst_Param)
					{
						////
						DataTable dt_Mst_Param = mdsResult.Tables["Mst_Param"].Copy();
						lst_Mst_Param = TUtils.DataTableCmUtils.ToListof<Mst_Param>(dt_Mst_Param);
						objRT_Mst_Param.Lst_Mst_Param = lst_Mst_Param;
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
		public DataSet Mst_Param_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Param
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Param_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Param_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Param", strRt_Cols_Mst_Param
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

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
				bool bGet_Mst_Param = (strRt_Cols_Mst_Param != null && strRt_Cols_Mst_Param.Length > 0);

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
						---- #tbl_Mst_Param_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mpr.ParamCode
						into #tbl_Mst_Param_Filter_Draft
						from Mst_Param mpr --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mpr.ParamCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Param_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Param_Filter:
						select
							t.*
						into #tbl_Mst_Param_Filter
						from #tbl_Mst_Param_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Param --------:
						zzB_Select_Mst_Param_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Param_Filter_Draft;
						--drop table #tbl_Mst_Param_Filter;
					"
					);
				////
				string zzB_Select_Mst_Param_zzE = "-- Nothing.";
				if (bGet_Mst_Param)
				{
					#region // bGet_Mst_Param:
					zzB_Select_Mst_Param_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Param:
							select
								t.MyIdxSeq
								, mpr.*
							from #tbl_Mst_Param_Filter t --//[mylock]
								inner join Mst_Param mpr --//[mylock]
									on t.ParamCode = mpr.ParamCode
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
							, "Mst_Param" // strTableNameDB
							, "Mst_Param." // strPrefixStd
							, "mpr." // strPrefixAlias
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
					, "zzB_Select_Mst_Param_zzE", zzB_Select_Mst_Param_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_Mst_Param)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_Param";
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
		public DataSet WAS_Mst_Param_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Param objRQ_Mst_Param
			////
			, out RT_Mst_Param objRT_Mst_Param
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Param.Tid;
			objRT_Mst_Param = new RT_Mst_Param();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Param.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Param_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Param_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Param", TJson.JsonConvert.SerializeObject(objRQ_Mst_Param.Mst_Param)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Param> lst_Mst_Param = new List<Mst_Param>();
				//List<Mst_ParamInGroup> lst_Mst_ParamInGroup = new List<Mst_ParamInGroup>();
				#endregion

				#region // Mst_Param_Create:
				mdsResult = Mst_Param_Create(
					objRQ_Mst_Param.Tid // strTid
					, objRQ_Mst_Param.GwUserCode // strGwUserCode
					, objRQ_Mst_Param.GwPassword // strGwPassword
					, objRQ_Mst_Param.WAUserCode // strUserCode
					, objRQ_Mst_Param.WAUserPassword // strUserPassword
					, objRQ_Mst_Param.AccessToken // strAccessToken
					, objRQ_Mst_Param.NetworkID // strNetworkID
					, objRQ_Mst_Param.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_Param.Mst_Param.ParamCode // objParamCode
					, objRQ_Mst_Param.Mst_Param.ParamValue // objParamValue
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
		public DataSet Mst_Param_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objParamCode
			, object objParamValue
			)

		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Param_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Param_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
					, "objParamCode", objParamCode
					, "objParamValue", objParamValue
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				string strParamValue = string.Format("{0}", objParamValue).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
				////
				DataTable dtDB_Mst_Param = null;
				{

					////
					if (strParamCode == null || strParamCode.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strParamCode", strParamCode
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Param_Create_InvalidParamCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					Mst_Param_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strParamCode // objParamCode
						, TConst.Flag.No // strFlagExistToCheck
						//, "" // strFlagActiveListToCheck
						, out dtDB_Mst_Param // dtDB_Mst_Param
						);
					////
				}
				#endregion

				#region // SaveDB Mst_Param:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Param.NewRow();
					strFN = "ParamCode"; drDB[strFN] = strParamCode;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "ParamValue"; drDB[strFN] = strParamValue;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_Mst_Param.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"Mst_Param"
						, dtDB_Mst_Param
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
		public DataSet WAS_Mst_Param_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Param objRQ_Mst_Param
			////
			, out RT_Mst_Param objRT_Mst_Param
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Param.Tid;
			objRT_Mst_Param = new RT_Mst_Param();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Param.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Param_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Param_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Param", TJson.JsonConvert.SerializeObject(objRQ_Mst_Param.Mst_Param)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Param> lst_Mst_Param = new List<Mst_Param>();
				//List<Mst_ParamInGroup> lst_Mst_ParamInGroup = new List<Mst_ParamInGroup>();
				#endregion

				#region // Mst_Param_Update:
				mdsResult = Mst_Param_Update(
					objRQ_Mst_Param.Tid // strTid
					, objRQ_Mst_Param.GwUserCode // strGwUserCode
					, objRQ_Mst_Param.GwPassword // strGwPassword
					, objRQ_Mst_Param.WAUserCode // strUserCode
					, objRQ_Mst_Param.WAUserPassword // strUserPassword
					, objRQ_Mst_Param.AccessToken // strAccessToken
					, objRQ_Mst_Param.NetworkID // strNetworkID
					, objRQ_Mst_Param.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Param.Mst_Param.ParamCode // objParamCode
					, objRQ_Mst_Param.Mst_Param.ParamValue // objParamValue
														   ////
					, objRQ_Mst_Param.Ft_Cols_Upd// objFt_Cols_Upd
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
		public DataSet Mst_Param_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objParamCode
			, object objParamValue
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Param_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Param_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objParamCode", objParamCode
					, "objParamValue", objParamValue
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

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
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				string strParamValue = string.Format("{0}", objParamValue).Trim();
				////
				bool bUpd_ParamValue = strFt_Cols_Upd.Contains("Mst_Param.ParamValue".ToUpper());

				////
				DataTable dtDB_Mst_Param = null;
				{
					////
					Mst_Param_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strParamCode // objParamCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 //, "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Param // dtDB_Mst_Param
						);
					////
				}
				#endregion

				#region // Save Mst_Param:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Param.Rows[0];
					if (bUpd_ParamValue) { strFN = "ParamValue"; drDB[strFN] = strParamValue; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"Mst_Param"
						, dtDB_Mst_Param
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
		public DataSet WAS_Mst_Param_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Param objRQ_Mst_Param
			////
			, out RT_Mst_Param objRT_Mst_Param
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Param.Tid;
			objRT_Mst_Param = new RT_Mst_Param();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Param.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Param_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Param_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_Param", TJson.JsonConvert.SerializeObject(objRQ_Mst_Param.Mst_Param)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Param> lst_Mst_Param = new List<Mst_Param>();
				//List<Mst_ParamInGroup> lst_Mst_ParamInGroup = new List<Mst_ParamInGroup>();
				#endregion

				#region // Mst_Param_Delete:
				mdsResult = Mst_Param_Delete(
					objRQ_Mst_Param.Tid // strTid
					, objRQ_Mst_Param.GwUserCode // strGwUserCode
					, objRQ_Mst_Param.GwPassword // strGwPassword
					, objRQ_Mst_Param.WAUserCode // strUserCode
					, objRQ_Mst_Param.WAUserPassword // strUserPassword
					, objRQ_Mst_Param.AccessToken // strAccessToken
					, objRQ_Mst_Param.NetworkID // strNetworkID
					, objRQ_Mst_Param.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Param.Mst_Param.ParamCode // objParamCode
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
		public DataSet Mst_Param_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			/////
			, object objParamCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Param_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Param_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objParamCode", objParamCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strParamCode = TUtils.CUtils.StdParam(objParamCode);
				////
				DataTable dtDB_Mst_Param = null;
				{
					////
					Mst_Param_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strParamCode // objParamCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 //, "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Param // dtDB_Mst_Param
						);
				}
				#endregion

				#region // SaveDB Mst_Param:
				{
					// Init:
					dtDB_Mst_Param.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"Mst_Param"
						, dtDB_Mst_Param
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
        #endregion

        #region // Mst_InventoryType:
        private void Mst_InventoryType_CheckDB(
            ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objInvType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_InventoryType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_InventoryType t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.InvType = @objInvType
					;
				");
            dtDB_Mst_InventoryType = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
				, "@objInvType", objInvType
				).Tables[0];
            dtDB_Mst_InventoryType.TableName = "Mst_InventoryType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_InventoryType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvType", objInvType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_InventoryType_CheckDB_InvTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_InventoryType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvType", objInvType
						});
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_InventoryType_CheckDB_InvTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_InventoryType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvType", objInvType
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_InventoryType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_InventoryType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_InventoryType_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_InventoryType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_InventoryType_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InventoryType", strRt_Cols_Mst_InventoryType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_InventoryType = (strRt_Cols_Mst_InventoryType != null && strRt_Cols_Mst_InventoryType.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
			////	
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_InventoryType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mitp.OrgID
							, mitp.InvType
						into #tbl_Mst_InventoryType_Filter_Draft
						from Mst_InventoryType mitp --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on mitp.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mitp.OrgID asc
							, mitp.InvType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_InventoryType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_InventoryType_Filter:
						select
							t.*
						into #tbl_Mst_InventoryType_Filter
						from #tbl_Mst_InventoryType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_InventoryType --------:
						zzB_Select_Mst_InventoryType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_InventoryType_Filter_Draft;
						--drop table #tbl_Mst_InventoryType_Filter;
					"
				);
            ////
            string zzB_Select_Mst_InventoryType_zzE = "-- Nothing.";
            if (bGet_Mst_InventoryType)
            {
                #region // bGet_Mst_InventoryType:
                zzB_Select_Mst_InventoryType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_InventoryType:
                        select
                            t.MyIdxSeq
	                        , mitp.*
                        from #tbl_Mst_InventoryType_Filter t --//[mylock]
	                        inner join Mst_InventoryType mitp --//[mylock]
		                        on t.OrgID = mitp.OrgID
									and t.InvType = mitp.InvType
                        order by 
							t.MyIdxSeq asc
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
                        , "Mst_InventoryType" // strTableNameDB
                        , "Mst_InventoryType." // strPrefixStd
                        , "mitp." // strPrefixAlias
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
                , "zzB_Select_Mst_InventoryType_zzE", zzB_Select_Mst_InventoryType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_InventoryType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_InventoryType";
            }
            #endregion
        }

        public DataSet Mst_InventoryType_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_InventoryType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_InventoryType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InventoryType", strRt_Cols_Mst_InventoryType
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
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_InventoryType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_InventoryType_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_InventoryType // strRt_Cols_Mst_InventoryType
                                                       ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet WAS_Mst_InventoryType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_InventoryType objRQ_Mst_InventoryType
            ////
            , out RT_Mst_InventoryType objRT_Mst_InventoryType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_InventoryType.Tid;
            objRT_Mst_InventoryType = new RT_Mst_InventoryType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_InventoryType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryType_Get;
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
                List<Mst_InventoryType> lst_Mst_InventoryType = new List<Mst_InventoryType>();
                #endregion

                #region // WAS_Mst_InventoryType_Get:
                mdsResult = Mst_InventoryType_Get(
                    objRQ_Mst_InventoryType.Tid // strTid
                    , objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
                    , objRQ_Mst_InventoryType.GwPassword // strGwPassword
                    , objRQ_Mst_InventoryType.WAUserCode // strUserCode
                    , objRQ_Mst_InventoryType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_InventoryType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_InventoryType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_InventoryType.Ft_WhereClause // strFt_WhereClause
                                                             //// Return:
                    , objRQ_Mst_InventoryType.Rt_Cols_Mst_InventoryType // strRt_Cols_Mst_InventoryType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_InventoryType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_InventoryType = mdsResult.Tables["Mst_InventoryType"].Copy();
                    lst_Mst_InventoryType = TUtils.DataTableCmUtils.ToListof<Mst_InventoryType>(dt_Mst_InventoryType);
                    objRT_Mst_InventoryType.Lst_Mst_InventoryType = lst_Mst_InventoryType;
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

		public DataSet WAS_Mst_InventoryType_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryType objRQ_Mst_InventoryType
			////
			, out RT_Mst_InventoryType objRT_Mst_InventoryType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryType.Tid;
			objRT_Mst_InventoryType = new RT_Mst_InventoryType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryType_Create;
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
				List<Mst_InventoryType> lst_Mst_InventoryType = new List<Mst_InventoryType>();
				#endregion

				#region // Mst_InventoryType_Create:
				mdsResult = Mst_InventoryType_Create(
					objRQ_Mst_InventoryType.Tid // strTid
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryType.Mst_InventoryType.OrgID // objOrgID
					, objRQ_Mst_InventoryType.Mst_InventoryType.InvType // objInvType
					, objRQ_Mst_InventoryType.Mst_InventoryType.InvTypeName // objInvTypeName
					, objRQ_Mst_InventoryType.Mst_InventoryType.FlagActive // objFlagActive
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
		public DataSet Mst_InventoryType_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvType
			, object objInvTypeName
			, object objFlagActive
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryType_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvType", objInvType
					, "objInvTypeName", objInvTypeName
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryType_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryType_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvType // objInvType
						, objInvTypeName // objInvTypeName
						, objFlagActive // objFlagActive
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
		private void Mst_InventoryType_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvType
			, object objInvTypeName
			, object objFlagActive
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryType_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objOrgID", objOrgID
				, "objInvType", objInvType
				, "objInvTypeName", objInvTypeName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvType = TUtils.CUtils.StdParam(objInvType);
			string strInvTypeName = string.Format("{0}", objInvTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_InventoryType = null;
			DataTable dtDB_Mst_Org = null;
			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryType_CreateX_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strInvType == null || strInvType.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvType", strInvType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryType_CreateX_InvalidInvType
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				
				////
				Mst_InventoryType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvType // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_InventoryType // dtDB_Mst_InventoryType
					);
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				if (strInvTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvTypeName", strInvTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryType_CreateX_InvalidInvTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_InventoryType:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryType.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "InvType"; drDB[strFN] = strInvType;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvTypeName"; drDB[strFN] = strInvTypeName;
				strFN = "FlagActive"; drDB[strFN] = strFlagActive;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_InventoryType.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryType" // strTableName
					, dtDB_Mst_InventoryType // dtData
											 //, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_Mst_InventoryType_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryType objRQ_Mst_InventoryType
			////
			, out RT_Mst_InventoryType objRT_Mst_InventoryType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryType.Tid;
			objRT_Mst_InventoryType = new RT_Mst_InventoryType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryType_Update;
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
				List<Mst_InventoryType> lst_Mst_InventoryType = new List<Mst_InventoryType>();
				#endregion

				#region // Mst_InventoryType_Update:
				mdsResult = Mst_InventoryType_Update(
					objRQ_Mst_InventoryType.Tid // strTid
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryType.Mst_InventoryType.OrgID // objOrgID
					, objRQ_Mst_InventoryType.Mst_InventoryType.InvType // objInvType
					, objRQ_Mst_InventoryType.Mst_InventoryType.InvTypeName // objInvTypeName
					, objRQ_Mst_InventoryType.Mst_InventoryType.FlagActive // objFlagActive
																		   ////
					, objRQ_Mst_InventoryType.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_InventoryType_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvType
			, object objInvTypeName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryType_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvType", objInvType
					, "objInvTypeName", objInvTypeName
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryType_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryType_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objOrgID // objOrgID
						, objInvType // objInvType
						, objInvTypeName // objInvTypeName
						, objFlagActive // objFlagActive
										/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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
		private void Mst_InventoryType_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvType
			, object objInvTypeName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryType_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvType", objInvType
				, "objInvTypeName", objInvTypeName
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
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvType = TUtils.CUtils.StdParam(objInvType);
			string strInvTypeName = string.Format("{0}", objInvTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_InvTypeName = strFt_Cols_Upd.Contains("Mst_InventoryType.InvTypeName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_InventoryType.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_InventoryType = null;
			DataTable dtDB_Mst_Org = null;
			{
				////
				Mst_InventoryType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvType // objInvType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryType // dtDB_Mst_InventoryType
					);
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				if (strInvTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvTypeName", strInvTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryType_UpdateX_InvalidInvTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_Supplier:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryType.Rows[0];
				if (bUpd_InvTypeName) { strFN = "InvTypeName"; drDB[strFN] = strInvTypeName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryType"
					, dtDB_Mst_InventoryType
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_Mst_InventoryType_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryType objRQ_Mst_InventoryType
			////
			, out RT_Mst_InventoryType objRT_Mst_InventoryType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryType.Tid;
			objRT_Mst_InventoryType = new RT_Mst_InventoryType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryType_Delete;
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
				List<Mst_InventoryType> lst_Mst_InventoryType = new List<Mst_InventoryType>();
				#endregion

				#region // Mst_InventoryType_Delete:
				mdsResult = Mst_InventoryType_Delete(
					objRQ_Mst_InventoryType.Tid // strTid
					, objRQ_Mst_InventoryType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryType.Mst_InventoryType.OrgID // objOrgID
					, objRQ_Mst_InventoryType.Mst_InventoryType.InvType // objInvType
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
		public DataSet Mst_InventoryType_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objInvType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryType_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvType", objInvType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryType_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryType_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvType // objInvType
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		private void Mst_InventoryType_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvType
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryType_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvType", objInvType
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvType = TUtils.CUtils.StdParam(objInvType);

			////
			DataTable dtDB_Mst_InventoryType = null;
			DataTable dtDB_Mst_Org = null;
			{
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InventoryType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvType // objInvType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryType // dtDB_Mst_Supplier
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_InventoryType.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryType"
					, dtDB_Mst_InventoryType
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_InventoryLevelType:
		private void Mst_InventoryLevelType_CheckDB(
            ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objInvLevelType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_InventoryLevelType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_InventoryLevelType t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.InvLevelType = @objInvLevelType
					;
				");
            dtDB_Mst_InventoryLevelType = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objInvLevelType", objInvLevelType
                ).Tables[0];
            dtDB_Mst_InventoryLevelType.TableName = "Mst_InventoryLevelType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_InventoryLevelType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvLevelType", objInvLevelType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_InventoryLevelType_CheckDB_InvLevelTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_InventoryLevelType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvLevelType", objInvLevelType
						});
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_InventoryLevelType_CheckDB_InvLevelTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_InventoryLevelType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvLevelType", objInvLevelType
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_InventoryLevelType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_InventoryLevelType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_InventoryLevelType_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_InventoryLevelType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_InventoryLevelType_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InventoryLevelType", strRt_Cols_Mst_InventoryLevelType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_InventoryLevelType = (strRt_Cols_Mst_InventoryLevelType != null && strRt_Cols_Mst_InventoryLevelType.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
			////		
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_InventoryLevelType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, miltp.OrgID
							, miltp.InvLevelType
						into #tbl_Mst_InventoryLevelType_Filter_Draft
						from Mst_InventoryLevelType miltp --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on miltp.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							miltp.OrgID asc
							, miltp.InvLevelType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_InventoryLevelType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_InventoryLevelType_Filter:
						select
							t.*
						into #tbl_Mst_InventoryLevelType_Filter
						from #tbl_Mst_InventoryLevelType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_InventoryLevelType --------:
						zzB_Select_Mst_InventoryLevelType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_InventoryLevelType_Filter_Draft;
						--drop table #tbl_Mst_InventoryLevelType_Filter;
					"
				);
            ////
            string zzB_Select_Mst_InventoryLevelType_zzE = "-- Nothing.";
            if (bGet_Mst_InventoryLevelType)
            {
                #region // bGet_Mst_InventoryLevelType:
                zzB_Select_Mst_InventoryLevelType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_InventoryLevelType:
                        select
                            t.MyIdxSeq
	                        , miltp.*
                        from #tbl_Mst_InventoryLevelType_Filter t --//[mylock]
	                        inner join Mst_InventoryLevelType miltp --//[mylock]
		                        on t.OrgID = miltp.OrgID
									and t.InvLevelType = miltp.InvLevelType
                        order by 
							t.MyIdxSeq asc
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
                        , "Mst_InventoryLevelType" // strTableNameDB
                        , "Mst_InventoryLevelType." // strPrefixStd
                        , "miltp." // strPrefixAlias
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
                , "zzB_Select_Mst_InventoryLevelType_zzE", zzB_Select_Mst_InventoryLevelType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_InventoryLevelType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_InventoryLevelType";
            }
            #endregion
        }

        public DataSet Mst_InventoryLevelType_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_InventoryLevelType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_InventoryLevelType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryLevelType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InventoryLevelType", strRt_Cols_Mst_InventoryLevelType
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
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_InventoryLevelType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_InventoryLevelType_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_InventoryLevelType // strRt_Cols_Mst_InventoryLevelType
                                                            ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet WAS_Mst_InventoryLevelType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType
            ////
            , out RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_InventoryLevelType.Tid;
            objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_InventoryLevelType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryLevelType_Get;
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
                List<Mst_InventoryLevelType> lst_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();
                #endregion

                #region // WAS_Mst_InventoryLevelType_Get:
                mdsResult = Mst_InventoryLevelType_Get(
                    objRQ_Mst_InventoryLevelType.Tid // strTid
                    , objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
                    , objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
                    , objRQ_Mst_InventoryLevelType.WAUserCode // strUserCode
                    , objRQ_Mst_InventoryLevelType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryLevelType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryLevelType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryLevelType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_InventoryLevelType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_InventoryLevelType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_InventoryLevelType.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_Mst_InventoryLevelType.Rt_Cols_Mst_InventoryLevelType // strRt_Cols_Mst_InventoryLevelType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_InventoryLevelType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_InventoryLevelType = mdsResult.Tables["Mst_InventoryLevelType"].Copy();
                    lst_Mst_InventoryLevelType = TUtils.DataTableCmUtils.ToListof<Mst_InventoryLevelType>(dt_Mst_InventoryLevelType);
                    objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType = lst_Mst_InventoryLevelType;
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

		public DataSet WAS_Mst_InventoryLevelType_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType
			////
			, out RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryLevelType.Tid;
			objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryLevelType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryLevelType_Create;
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
				List<Mst_InventoryLevelType> lst_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();
				#endregion

				#region // Mst_InventoryLevelType_Create:
				mdsResult = Mst_InventoryLevelType_Create(
					objRQ_Mst_InventoryLevelType.Tid // strTid
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryLevelType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryLevelType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryLevelType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryLevelType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryLevelType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.OrgID // objOrgID
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.InvLevelType // objInvLevelType
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.InvLevelTypeName // objInvLevelTypeName
					,objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.FlagActive // FlagActive
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
		public DataSet Mst_InventoryLevelType_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvLevelType
			, object objInvLevelTypeName
			, object objFlagActive
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryLevelType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryLevelType_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvLevelType", objInvLevelType
					, "objInvLevelTypeName", objInvLevelTypeName
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryLevelType_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryLevelType_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvLevelType // objInvLevelType
						, objInvLevelTypeName // objInvLevelTypeName
						, objFlagActive // objFlagActive
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
		private void Mst_InventoryLevelType_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvLevelType
			, object objInvLevelTypeName
			, object objFlagActive
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryLevelType_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objOrgID", objOrgID
				, "objInvLevelType", objInvLevelType
				, "objInvLevelTypeName", objInvLevelTypeName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvLevelType = TUtils.CUtils.StdParam(objInvLevelType);
			string strInvLevelTypeName = string.Format("{0}", objInvLevelTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_InventoryLevelType = null;
			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryLevelType_CreateX_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strInvLevelType == null || strInvLevelType.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvLevelType", strInvLevelType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryLevelType_CreateX_InvalidInvLevelType
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InventoryLevelType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvLevelType // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_InventoryLevelType // dtDB_Mst_InventoryLevelType
					);
				////
				if (strInvLevelTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvLevelTypeName", strInvLevelTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryLevelType_CreateX_InvalidInvLevelTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_InventoryLevelType:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryLevelType.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "InvLevelType"; drDB[strFN] = strInvLevelType;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvLevelTypeName"; drDB[strFN] = strInvLevelTypeName;
				strFN = "FlagActive"; drDB[strFN] = strFlagActive;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_InventoryLevelType.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryLevelType" // strTableName
					, dtDB_Mst_InventoryLevelType // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_Mst_InventoryLevelType_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType
			////
			, out RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryLevelType.Tid;
			objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryLevelType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryLevelType_Update;
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
				List<Mst_InventoryLevelType> lst_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();
				#endregion

				#region // Mst_InventoryLevelType_Update:
				mdsResult = Mst_InventoryLevelType_Update(
					objRQ_Mst_InventoryLevelType.Tid // strTid
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryLevelType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryLevelType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryLevelType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryLevelType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryLevelType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.OrgID // objOrgID
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.InvLevelType // objInvLevelType
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.InvLevelTypeName // objInvLevelTypeName
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.FlagActive // objFlagActive
																					 ////
					, objRQ_Mst_InventoryLevelType.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_InventoryLevelType_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvLevelType
			, object objInvLevelTypeName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryLevelType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryLevelType_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvLevelType", objInvLevelType
					, "objInvLevelTypeName", objInvLevelTypeName
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryLevelType_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryLevelType_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvLevelType // objInvLevelType
						, objInvLevelTypeName // objInvLevelTypeName
						, objFlagActive // objFlagActive
										/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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
		private void Mst_InventoryLevelType_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvLevelType
			, object objInvLevelTypeName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryLevelType_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvLevelType", objInvLevelType
				, "objInvLevelTypeName", objInvLevelTypeName
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
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvLevelType = TUtils.CUtils.StdParam(objInvLevelType);
			string strInvLevelTypeName = string.Format("{0}", objInvLevelTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_InvLevelTypeName = strFt_Cols_Upd.Contains("Mst_InventoryLevelType.InvLevelTypeName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_InventoryLevelType.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_InventoryLevelType = null;
			{
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InventoryLevelType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvLevelType // objInvLevelType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryLevelType // dtDB_Mst_InventoryLevelType
					);
				////
				if (strInvLevelTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvLevelTypeName", strInvLevelTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryLevelType_UpdateX_InvalidInvLevelTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_Supplier:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryLevelType.Rows[0];
				if (bUpd_InvLevelTypeName) { strFN = "InvLevelTypeName"; drDB[strFN] = strInvLevelTypeName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryLevelType"
					, dtDB_Mst_InventoryLevelType
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_Mst_InventoryLevelType_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType
			////
			, out RT_Mst_InventoryLevelType objRT_Mst_InventoryLevelType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryLevelType.Tid;
			objRT_Mst_InventoryLevelType = new RT_Mst_InventoryLevelType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryLevelType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryLevelType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryLevelType_Delete;
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
				List<Mst_InventoryLevelType> lst_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();
				#endregion

				#region // Mst_InventoryLevelType_Delete:
				mdsResult = Mst_InventoryLevelType_Delete(
					objRQ_Mst_InventoryLevelType.Tid // strTid
					, objRQ_Mst_InventoryLevelType.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryLevelType.GwPassword // strGwPassword
					, objRQ_Mst_InventoryLevelType.WAUserCode // strUserCode
					, objRQ_Mst_InventoryLevelType.WAUserPassword // strUserPassword
					, objRQ_Mst_InventoryLevelType.AccessToken // strAccessToken
					, objRQ_Mst_InventoryLevelType.NetworkID // strNetworkID
					, objRQ_Mst_InventoryLevelType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.OrgID // objOrgID
					, objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType.InvLevelType // objInvLevelType
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
		public DataSet Mst_InventoryLevelType_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objInvLevelType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryLevelType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryLevelType_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvLevelType", objInvLevelType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryLevelType_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryLevelType_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvLevelType // objInvLevelType
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		private void Mst_InventoryLevelType_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvLevelType
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryLevelType_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvLevelType", objInvLevelType
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvLevelType = TUtils.CUtils.StdParam(objInvLevelType);

			////
			DataTable dtDB_Mst_InventoryLevelType = null;
			{
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InventoryLevelType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvLevelType // objInvLevelType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryLevelType // dtDB_Mst_Supplier
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_InventoryLevelType.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryLevelType"
					, dtDB_Mst_InventoryLevelType
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_InvInType:
		private void Mst_InvInType_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objInvInType
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, string strFlagStatisticListToCheck
			, out DataTable dtDB_Mst_InvInType
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_InvInType t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.InvInType = @objInvInType
					;
				");
			dtDB_Mst_InvInType = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objInvInType", objInvInType
				).Tables[0];
			dtDB_Mst_InvInType.TableName = "Mst_InvInType";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_InvInType.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvInType", objInvInType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_CheckDB_InvInTypeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_InvInType.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvInType", objInvInType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_CheckDB_InvInTypeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_InvInType.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvInType", objInvInType
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_InvInType.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_InvInType_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			// strFlagStatisticListToCheck:
			if (strFlagStatisticListToCheck.Length > 0 && !strFlagStatisticListToCheck.Contains(Convert.ToString(dtDB_Mst_InvInType.Rows[0]["FlagStatistic"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvInType", objInvInType
					, "Check.strFlagStatisticListToCheck", strFlagStatisticListToCheck
					, "DB.FlagStatistic", dtDB_Mst_InvInType.Rows[0]["FlagStatistic"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_InvInType_CheckDB_FlagStatisticNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_InvInType_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_InvInType
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_InvInType_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InvInType", strRt_Cols_Mst_InvInType
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_InvInType = (strRt_Cols_Mst_InvInType != null && strRt_Cols_Mst_InvInType.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

			#endregion

			#region // Build Sql:
			////
			ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					});
			////		
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_InvInType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, miitp.OrgID
							, miitp.InvInType
						into #tbl_Mst_InvInType_Filter_Draft
						from Mst_InvInType miitp --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on miitp.OrgID = t_MstNNT_View.OrgID
						where (1=1)
                            and miitp.OrgID <> '0' -- 20200803.HTTT.Không hiển thị Loại nhập kho OrgID = 0
							zzB_Where_strFilter_zzE
						order by 
							miitp.OrgID asc
							, miitp.InvInType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_InvInType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_InvInType_Filter:
						select
							t.*
						into #tbl_Mst_InvInType_Filter
						from #tbl_Mst_InvInType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_InvInType --------:
						zzB_Select_Mst_InvInType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_InvInType_Filter_Draft;
						--drop table #tbl_Mst_InvInType_Filter;
					"
                );
			////
			string zzB_Select_Mst_InvInType_zzE = "-- Nothing.";
			if (bGet_Mst_InvInType)
			{
				#region // bGet_Mst_InvInType:
				zzB_Select_Mst_InvInType_zzE = CmUtils.StringUtils.Replace(@"
						---- Mst_InvInType:
						select
							t.MyIdxSeq
							, miitp.*
						from #tbl_Mst_InvInType_Filter t --//[mylock]
							inner join Mst_InvInType miitp --//[mylock]
								on t.OrgID = miitp.OrgID
									and t.InvInType = miitp.InvInType
						order by 
							t.MyIdxSeq asc
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
						, "Mst_InvInType" // strTableNameDB
						, "Mst_InvInType." // strPrefixStd
						, "miitp." // strPrefixAlias
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
				, "zzB_Select_Mst_InvInType_zzE", zzB_Select_Mst_InvInType_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_InvInType)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_InvInType";
			}
			#endregion
		}

		public DataSet Mst_InvInType_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_InvInType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_InvInType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvInType_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InvInType", strRt_Cols_Mst_InvInType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvInType_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_InvInType_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_InvInType // strRt_Cols_Mst_InvInType
												   ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_Mst_InvInType_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvInType objRQ_Mst_InvInType
			////
			, out RT_Mst_InvInType objRT_Mst_InvInType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvInType.Tid;
			objRT_Mst_InvInType = new RT_Mst_InvInType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvInType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvInType_Get;
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
				List<Mst_InvInType> lst_Mst_InvInType = new List<Mst_InvInType>();
				#endregion

				#region // WAS_Mst_InvInType_Get:
				mdsResult = Mst_InvInType_Get(
					objRQ_Mst_InvInType.Tid // strTid
					, objRQ_Mst_InvInType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvInType.GwPassword // strGwPassword
					, objRQ_Mst_InvInType.WAUserCode // strUserCode
					, objRQ_Mst_InvInType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvInType.AccessToken // strAccessToken
					, objRQ_Mst_InvInType.NetworkID // strNetworkID
					, objRQ_Mst_InvInType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_InvInType.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_InvInType.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_InvInType.Ft_WhereClause // strFt_WhereClause
														 //// Return:
					, objRQ_Mst_InvInType.Rt_Cols_Mst_InvInType // strRt_Cols_Mst_InvInType
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_InvInType.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_InvInType = mdsResult.Tables["Mst_InvInType"].Copy();
					lst_Mst_InvInType = TUtils.DataTableCmUtils.ToListof<Mst_InvInType>(dt_Mst_InvInType);
					objRT_Mst_InvInType.Lst_Mst_InvInType = lst_Mst_InvInType;
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

		public DataSet WAS_Mst_InvInType_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvInType objRQ_Mst_InvInType
			////
			, out RT_Mst_InvInType objRT_Mst_InvInType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvInType.Tid;
			objRT_Mst_InvInType = new RT_Mst_InvInType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvInType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvInType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvInType_Create;
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
				List<Mst_InvInType> lst_Mst_InvInType = new List<Mst_InvInType>();
				#endregion

				#region // Mst_InvInType_Create:
				mdsResult = Mst_InvInType_Create(
					objRQ_Mst_InvInType.Tid // strTid
					, objRQ_Mst_InvInType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvInType.GwPassword // strGwPassword
					, objRQ_Mst_InvInType.WAUserCode // strUserCode
					, objRQ_Mst_InvInType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvInType.AccessToken // strAccessToken
					, objRQ_Mst_InvInType.NetworkID // strNetworkID
					, objRQ_Mst_InvInType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvInType.Mst_InvInType.OrgID // objOrgID
					, objRQ_Mst_InvInType.Mst_InvInType.InvInType // objInvInType
					, objRQ_Mst_InvInType.Mst_InvInType.InvInTypeName // objInvInTypeName
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
		public DataSet Mst_InvInType_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvInType
			, object objInvInTypeName
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvInType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvInType_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvInType", objInvInType
					, "objInvInTypeName", objInvInTypeName
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvInType_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_InvInType_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvInType // objInvInType
						, objInvInTypeName // objInvInTypeName
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
		private void Mst_InvInType_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvInType
			, object objInvInTypeName
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvInType_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objOrgID", objOrgID
				, "objInvInType", objInvInType
				, "objInvInTypeName", objInvInTypeName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvInType = TUtils.CUtils.StdParam(objInvInType);
			string strInvInTypeName = string.Format("{0}", objInvInTypeName).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_InvInType = null;

			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_CreateX_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strInvInType == null || strInvInType.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvInType", strInvInType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_CreateX_InvalidInvInType
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvInType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvInType // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, "" // strFlagStatisticListToCheck
					, out dtDB_Mst_InvInType // dtDB_Mst_InvInType
					);
				////
				if (strInvInTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvInTypeName", strInvInTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_CreateX_InvalidInvInTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_InvInType:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InvInType.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "InvInType"; drDB[strFN] = strInvInType;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvInTypeName"; drDB[strFN] = strInvInTypeName;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "FlagStatistic"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_InvInType.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_InvInType" // strTableName
					, dtDB_Mst_InvInType // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_Mst_InvInType_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvInType objRQ_Mst_InvInType
			////
			, out RT_Mst_InvInType objRT_Mst_InvInType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvInType.Tid;
			objRT_Mst_InvInType = new RT_Mst_InvInType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvInType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvInType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvInType_Update;
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
				List<Mst_InvInType> lst_Mst_InvInType = new List<Mst_InvInType>();
				#endregion

				#region // Mst_InvInType_Update:
				mdsResult = Mst_InvInType_Update(
					objRQ_Mst_InvInType.Tid // strTid
					, objRQ_Mst_InvInType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvInType.GwPassword // strGwPassword
					, objRQ_Mst_InvInType.WAUserCode // strUserCode
					, objRQ_Mst_InvInType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvInType.AccessToken // strAccessToken
					, objRQ_Mst_InvInType.NetworkID // strNetworkID
					, objRQ_Mst_InvInType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvInType.Mst_InvInType.OrgID // objOrgID
					, objRQ_Mst_InvInType.Mst_InvInType.InvInType // objInvInType
					, objRQ_Mst_InvInType.Mst_InvInType.InvInTypeName // objInvInTypeName
					, objRQ_Mst_InvInType.Mst_InvInType.FlagActive // objFlagActive
					, objRQ_Mst_InvInType.Mst_InvInType.FlagStatistic // FlagStatistic
																	  ////
					, objRQ_Mst_InvInType.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_InvInType_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvInType
			, object objInvInTypeName
			, object objFlagActive
			, object objFlagStatistic
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvInType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvInType_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvInType", objInvInType
					, "objInvInTypeName", objInvInTypeName
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvInType_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_InvInType_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvInType // objInvInType
						, objInvInTypeName // objInvInTypeName
						, objFlagActive // objFlagActive
						, objFlagStatistic // objFlagStatistic
										   /////
						, objFt_Cols_Upd // objFt_Cols_Upd
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
		private void Mst_InvInType_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvInType
			, object objInvInTypeName
			, object objFlagActive
			, object objFlagStatistic
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvInType_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvInType", objInvInType
				, "objInvInTypeName", objInvInTypeName
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
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvInType = TUtils.CUtils.StdParam(objInvInType);
			string strInvInTypeName = string.Format("{0}", objInvInTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strFlagStatistic = TUtils.CUtils.StdFlag(objFlagStatistic);
			////
			bool bUpd_InvInTypeName = strFt_Cols_Upd.Contains("Mst_InvInType.InvInTypeName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_InvInType.FlagActive".ToUpper());
			bool bUpd_FlagStatistic = strFt_Cols_Upd.Contains("Mst_InvInType.FlagStatistic".ToUpper());

			////
			DataTable dtDB_Mst_InvInType = null;
			{
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvInType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvInType // objInvInType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , "" // strFlagStatisticListToCheck
					 , out dtDB_Mst_InvInType // dtDB_Mst_InvInType
					);
				////
				if (strInvInTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvInTypeName", strInvInTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_UpdateX_InvalidInvInTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_InvInType:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InvInType.Rows[0];
				if (bUpd_InvInTypeName) { strFN = "InvInTypeName"; drDB[strFN] = strInvInTypeName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_FlagStatistic) { strFN = "FlagStatistic"; drDB[strFN] = strFlagStatistic; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_InvInType"
					, dtDB_Mst_InvInType
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_Mst_InvInType_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvInType objRQ_Mst_InvInType
			////
			, out RT_Mst_InvInType objRT_Mst_InvInType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvInType.Tid;
			objRT_Mst_InvInType = new RT_Mst_InvInType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvInType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvInType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvInType_Delete;
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
				List<Mst_InvInType> lst_Mst_InvInType = new List<Mst_InvInType>();
				#endregion

				#region // Mst_InvInType_Delete:
				mdsResult = Mst_InvInType_Delete(
					objRQ_Mst_InvInType.Tid // strTid
					, objRQ_Mst_InvInType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvInType.GwPassword // strGwPassword
					, objRQ_Mst_InvInType.WAUserCode // strUserCode
					, objRQ_Mst_InvInType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvInType.AccessToken // strAccessToken
					, objRQ_Mst_InvInType.NetworkID // strNetworkID
					, objRQ_Mst_InvInType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvInType.Mst_InvInType.OrgID // objOrgID
					, objRQ_Mst_InvInType.Mst_InvInType.InvInType // objInvInType
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
		public DataSet Mst_InvInType_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objInvInType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvInType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvInType_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvInType", objInvInType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvInType_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_InvInType_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvInType // objInvInType
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		private void Mst_InvInType_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvInType
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvInType_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvInType", objInvInType
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvInType = TUtils.CUtils.StdParam(objInvInType);

			////
			DataTable dtDB_Mst_InvInType = null;
			{
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvInType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvInType // objInvInType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , "" // strFlagStatisticListToCheck
					 , out dtDB_Mst_InvInType // dtDB_Mst_Supplier
					);
				////
				// 20200715.HTTT. Loại nhập kho đã liên kết nghiệp vụ không cho phép xóa
				string strSqlCheck = CmUtils.StringUtils.Replace(@"
					select top 1
						t.IF_InvInNo
					from InvF_InventoryIn t --//[mylock]
					where (1=1)
						and t.InvInType = '@strInvInType'
						and t.OrgID = '@strOrgID'
					;
					"
					, "@strInvInType", strInvInType
					, "@strOrgID", strOrgID
					);
				DataTable dtDB_InvF_InventoryIn = _cf.db.ExecQuery(strSqlCheck).Tables[0];
				if(dtDB_InvF_InventoryIn.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.IF_InvInNo", dtDB_InvF_InventoryIn.Rows[0]["IF_InvInNo"]
						, "Check.InvInType", strInvInType
						, "Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvInType_Delete_InvInTypeNotAllowDelete
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_InvInType.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_InvInType"
					, dtDB_Mst_InvInType
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_InvOutType:
		private void Mst_InvOutType_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objInvOutType
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, string strFlagStatisticListToCheck
			, out DataTable dtDB_Mst_InvOutType
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_InvOutType t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.InvOutType = @objInvOutType
					;
				");
			dtDB_Mst_InvOutType = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objInvOutType", objInvOutType
				).Tables[0];
			dtDB_Mst_InvOutType.TableName = "Mst_InvOutType";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_InvOutType.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvOutType", objInvOutType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_CheckDB_InvOutTypeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_InvOutType.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvOutType", objInvOutType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_CheckDB_InvOutTypeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_InvOutType.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvOutType", objInvOutType
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_InvOutType.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_InvOutType_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			// strFlagStatisticListToCheck:
			if (strFlagStatisticListToCheck.Length > 0 && !strFlagStatisticListToCheck.Contains(Convert.ToString(dtDB_Mst_InvOutType.Rows[0]["FlagStatistic"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvOutType", objInvOutType
					, "Check.strFlagStatisticListToCheck", strFlagStatisticListToCheck
					, "DB.FlagStatistic", dtDB_Mst_InvOutType.Rows[0]["FlagStatistic"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_InvOutType_CheckDB_FlagStatisticNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_InvOutType_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_InvOutType
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_InvOutType_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_Mst_InvOutType", strRt_Cols_Mst_InvOutType
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_InvOutType = (strRt_Cols_Mst_InvOutType != null && strRt_Cols_Mst_InvOutType.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

			#endregion

			#region // Build Sql:
			////
			ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
						"@nFilterRecordStart", nFilterRecordStart
						, "@nFilterRecordEnd", nFilterRecordEnd
						});
			////		
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
							---- #tbl_Mst_InvOutType_Filter_Draft:
							select distinct
								identity(bigint, 0, 1) MyIdxSeq
								, miotp.OrgID
								, miotp.InvOutType
							into #tbl_Mst_InvOutType_Filter_Draft
							from Mst_InvOutType miotp --//[mylock]
								inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
									on miotp.OrgID = t_MstNNT_View.OrgID
							where (1=1)
                                and miotp.OrgID <> '0' -- 20200803.HTTT.Không hiển thị Loại xuất kho OrgID = 0
								zzB_Where_strFilter_zzE
							order by 
								miotp.OrgID asc
								, miotp.InvOutType asc
							;

							---- Summary:
							select Count(0) MyCount from #tbl_Mst_InvOutType_Filter_Draft t --//[mylock]
							;

							---- #tbl_Mst_InvOutType_Filter:
							select
								t.*
							into #tbl_Mst_InvOutType_Filter
							from #tbl_Mst_InvOutType_Filter_Draft t --//[mylock]
							where
								(t.MyIdxSeq >= @nFilterRecordStart)
								and (t.MyIdxSeq <= @nFilterRecordEnd)
							;

							-------- Mst_InvOutType --------:
							zzB_Select_Mst_InvOutType_zzE
							----------------------------------------

							---- Clear for debug:
							--drop table #tbl_Mst_InvOutType_Filter_Draft;
							--drop table #tbl_Mst_InvOutType_Filter;
						"
                );
			////
			string zzB_Select_Mst_InvOutType_zzE = "-- Nothing.";
			if (bGet_Mst_InvOutType)
			{
				#region // bGet_Mst_InvOutType:
				zzB_Select_Mst_InvOutType_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_InvOutType:
							select
								t.MyIdxSeq
								, miotp.*
							from #tbl_Mst_InvOutType_Filter t --//[mylock]
								inner join Mst_InvOutType miotp --//[mylock]
									on t.OrgID = miotp.OrgID
										and t.InvOutType = miotp.InvOutType
							order by 
								t.MyIdxSeq asc
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
						, "Mst_InvOutType" // strTableNameDB
						, "Mst_InvOutType." // strPrefixStd
						, "miotp." // strPrefixAlias
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
				, "zzB_Select_Mst_InvOutType_zzE", zzB_Select_Mst_InvOutType_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_InvOutType)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_InvOutType";
			}
			#endregion
		}

		public DataSet Mst_InvOutType_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_InvOutType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_InvOutType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvOutType_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InvOutType", strRt_Cols_Mst_InvOutType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvOutType_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_InvOutType_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_InvOutType // strRt_Cols_Mst_InvOutType
													////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet OS_Mst_InvOutType_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID_RQ
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_InvOutType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_Mst_InvOutType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_Mst_InvOutType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InvOutType", strRt_Cols_Mst_InvOutType
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

                ////
                //Sys_User_CheckAuthorize(
                //    strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // strWAUserCode
                //                    //, strWAUserPassword // strWAUserPassword
                //    , ref mdsFinal // mdsFinal
                //    , ref alParamsCoupleError // alParamsCoupleError
                //    , dtimeSys // dtimeSys
                //    , strAccessToken // strAccessToken
                //    , strNetworkID // strNetworkID
                //    , strOrgID_RQ // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_InvOutType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_InvOutType_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_InvOutType // strRt_Cols_Mst_InvOutType
                                                    ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet WAS_Mst_InvOutType_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvOutType objRQ_Mst_InvOutType
			////
			, out RT_Mst_InvOutType objRT_Mst_InvOutType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvOutType.Tid;
			objRT_Mst_InvOutType = new RT_Mst_InvOutType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvOutType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvOutType_Get;
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
				List<Mst_InvOutType> lst_Mst_InvOutType = new List<Mst_InvOutType>();
				#endregion

				#region // WAS_Mst_InvOutType_Get:
				mdsResult = Mst_InvOutType_Get(
					objRQ_Mst_InvOutType.Tid // strTid
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					, objRQ_Mst_InvOutType.WAUserCode // strUserCode
					, objRQ_Mst_InvOutType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvOutType.AccessToken // strAccessToken
					, objRQ_Mst_InvOutType.NetworkID // strNetworkID
					, objRQ_Mst_InvOutType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_InvOutType.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_InvOutType.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_InvOutType.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_Mst_InvOutType.Rt_Cols_Mst_InvOutType // strRt_Cols_Mst_InvOutType
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_InvOutType.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_InvOutType = mdsResult.Tables["Mst_InvOutType"].Copy();
					lst_Mst_InvOutType = TUtils.DataTableCmUtils.ToListof<Mst_InvOutType>(dt_Mst_InvOutType);
					objRT_Mst_InvOutType.Lst_Mst_InvOutType = lst_Mst_InvOutType;
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

        public DataSet WAS_OS_Mst_InvOutType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_InvOutType objRQ_Mst_InvOutType
            ////
            , out RT_Mst_InvOutType objRT_Mst_InvOutType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_InvOutType.Tid;
            objRT_Mst_InvOutType = new RT_Mst_InvOutType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Mst_InvOutType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Mst_InvOutType_Get;
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
                List<Mst_InvOutType> lst_Mst_InvOutType = new List<Mst_InvOutType>();
                #endregion

                #region // WAS_OS_Mst_InvOutType_Get:
                mdsResult = OS_Mst_InvOutType_Get(
                    objRQ_Mst_InvOutType.Tid // strTid
                    , objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
                    , objRQ_Mst_InvOutType.GwPassword // strGwPassword
                    , objRQ_Mst_InvOutType.WAUserCode // strUserCode
                    , objRQ_Mst_InvOutType.WAUserPassword // strUserPassword
                    , objRQ_Mst_InvOutType.AccessToken // strAccessToken
                    , objRQ_Mst_InvOutType.NetworkID // strNetworkID
                    , objRQ_Mst_InvOutType.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_InvOutType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_InvOutType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_InvOutType.Ft_WhereClause // strFt_WhereClause
                                                          //// Return:
                    , objRQ_Mst_InvOutType.Rt_Cols_Mst_InvOutType // strRt_Cols_Mst_InvOutType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_InvOutType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_InvOutType = mdsResult.Tables["Mst_InvOutType"].Copy();
                    lst_Mst_InvOutType = TUtils.DataTableCmUtils.ToListof<Mst_InvOutType>(dt_Mst_InvOutType);
                    objRT_Mst_InvOutType.Lst_Mst_InvOutType = lst_Mst_InvOutType;
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

        public DataSet WAS_Mst_InvOutType_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvOutType objRQ_Mst_InvOutType
			////
			, out RT_Mst_InvOutType objRT_Mst_InvOutType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvOutType.Tid;
			objRT_Mst_InvOutType = new RT_Mst_InvOutType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvOutType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvOutType_Create;
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
				List<Mst_InvOutType> lst_Mst_InvOutType = new List<Mst_InvOutType>();
				#endregion

				#region // Mst_InvOutType_Create:
				mdsResult = Mst_InvOutType_Create(
					objRQ_Mst_InvOutType.Tid // strTid
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					, objRQ_Mst_InvOutType.WAUserCode // strUserCode
					, objRQ_Mst_InvOutType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvOutType.AccessToken // strAccessToken
					, objRQ_Mst_InvOutType.NetworkID // strNetworkID
					, objRQ_Mst_InvOutType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvOutType.Mst_InvOutType.OrgID // objOrgID
					, objRQ_Mst_InvOutType.Mst_InvOutType.InvOutType // objInvOutType
					, objRQ_Mst_InvOutType.Mst_InvOutType.InvOutTypeName // objInvOutTypeName
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
		public DataSet Mst_InvOutType_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvOutType
			, object objInvOutTypeName
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvOutType_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvOutType_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvOutType", objInvOutType
					, "objInvOutTypeName", objInvOutTypeName
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvOutType_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_InvOutType_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvOutType // objInvOutType
						, objInvOutTypeName // objInvOutTypeName
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
		private void Mst_InvOutType_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvOutType
			, object objInvOutTypeName
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvOutType_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objOrgID", objOrgID
				, "objInvOutType", objInvOutType
				, "objInvOutTypeName", objInvOutTypeName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
			string strInvOutTypeName = string.Format("{0}", objInvOutTypeName).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_InvOutType = null;

			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_CreateX_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strInvOutType == null || strInvOutType.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvOutType", strInvOutType
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_CreateX_InvalidInvOutType
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvOutType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvOutType // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, "" // strFlagStatisticListToCheck
					, out dtDB_Mst_InvOutType // dtDB_Mst_InvOutType
					);
				////
				if (strInvOutTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvOutTypeName", strInvOutTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_CreateX_InvalidInvOutTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_InvOutType:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InvOutType.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "InvOutType"; drDB[strFN] = strInvOutType;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvOutTypeName"; drDB[strFN] = strInvOutTypeName;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "FlagStatistic"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_InvOutType.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_InvOutType" // strTableName
					, dtDB_Mst_InvOutType // dtData
										  //, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_Mst_InvOutType_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvOutType objRQ_Mst_InvOutType
			////
			, out RT_Mst_InvOutType objRT_Mst_InvOutType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvOutType.Tid;
			objRT_Mst_InvOutType = new RT_Mst_InvOutType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvOutType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvOutType_Update;
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
				List<Mst_InvOutType> lst_Mst_InvOutType = new List<Mst_InvOutType>();
				#endregion

				#region // Mst_InvOutType_Update:
				mdsResult = Mst_InvOutType_Update(
					objRQ_Mst_InvOutType.Tid // strTid
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					, objRQ_Mst_InvOutType.WAUserCode // strUserCode
					, objRQ_Mst_InvOutType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvOutType.AccessToken // strAccessToken
					, objRQ_Mst_InvOutType.NetworkID // strNetworkID
					, objRQ_Mst_InvOutType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvOutType.Mst_InvOutType.OrgID // objOrgID
					, objRQ_Mst_InvOutType.Mst_InvOutType.InvOutType // objInvOutType
					, objRQ_Mst_InvOutType.Mst_InvOutType.InvOutTypeName // objInvOutTypeName
					, objRQ_Mst_InvOutType.Mst_InvOutType.FlagActive // objFlagActive
					, objRQ_Mst_InvOutType.Mst_InvOutType.FlagStatistic // objFlagStatistic
																		////
					, objRQ_Mst_InvOutType.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_InvOutType_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objOrgID
			, object objInvOutType
			, object objInvOutTypeName
			, object objFlagActive
			, object objFlagStatistic
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvOutType_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvOutType_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvOutType", objInvOutType
					, "objInvOutTypeName", objInvOutTypeName
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvOutType_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_InvOutType_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvOutType // objInvOutType
						, objInvOutTypeName // objInvOutTypeName
						, objFlagActive // objFlagActive
						, objFlagStatistic // objFlagStatistic
										   /////
						, objFt_Cols_Upd // objFt_Cols_Upd
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
		private void Mst_InvOutType_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvOutType
			, object objInvOutTypeName
			, object objFlagActive
			, object objFlagStatistic
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvOutType_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvOutType", objInvOutType
				, "objInvOutTypeName", objInvOutTypeName
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
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
			string strInvOutTypeName = string.Format("{0}", objInvOutTypeName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strFlagStatistic = TUtils.CUtils.StdFlag(objFlagStatistic);
			////
			bool bUpd_InvOutTypeName = strFt_Cols_Upd.Contains("Mst_InvOutType.InvOutTypeName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_InvOutType.FlagActive".ToUpper());
			bool bUpd_FlagStatistic = strFt_Cols_Upd.Contains("Mst_InvOutType.FlagStatistic".ToUpper());

			////
			DataTable dtDB_Mst_InvOutType = null;
			{
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvOutType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvOutType // objInvOutType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , "" // strFlagStatisticListToCheck
					 , out dtDB_Mst_InvOutType // dtDB_Mst_InvOutType
					);
				////
				if (strInvOutTypeName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvOutTypeName", strInvOutTypeName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_UpdateX_InvalidInvOutTypeName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_InvOutType:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InvOutType.Rows[0];
				if (bUpd_InvOutTypeName) { strFN = "InvOutTypeName"; drDB[strFN] = strInvOutTypeName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_FlagStatistic) { strFN = "FlagStatistic"; drDB[strFN] = strFlagStatistic; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_InvOutType"
					, dtDB_Mst_InvOutType
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_Mst_InvOutType_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InvOutType objRQ_Mst_InvOutType
			////
			, out RT_Mst_InvOutType objRT_Mst_InvOutType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvOutType.Tid;
			objRT_Mst_InvOutType = new RT_Mst_InvOutType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InvOutType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InvOutType_Delete;
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
				List<Mst_InvOutType> lst_Mst_InvOutType = new List<Mst_InvOutType>();
				#endregion

				#region // Mst_InvOutType_Delete:
				mdsResult = Mst_InvOutType_Delete(
					objRQ_Mst_InvOutType.Tid // strTid
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					, objRQ_Mst_InvOutType.WAUserCode // strUserCode
					, objRQ_Mst_InvOutType.WAUserPassword // strUserPassword
					, objRQ_Mst_InvOutType.AccessToken // strAccessToken
					, objRQ_Mst_InvOutType.NetworkID // strNetworkID
					, objRQ_Mst_InvOutType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InvOutType.Mst_InvOutType.OrgID // objOrgID
					, objRQ_Mst_InvOutType.Mst_InvOutType.InvOutType // objInvOutType
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
		public DataSet Mst_InvOutType_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objInvOutType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InvOutType_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InvOutType_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objInvOutType", objInvOutType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvOutType_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_InvOutType_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvOutType // objInvOutType
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		private void Mst_InvOutType_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvOutType
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InvOutType_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvOutType", objInvOutType
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);

			////
			DataTable dtDB_Mst_InvOutType = null;
			{
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_InvOutType_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID 
					 , strInvOutType // objInvOutType 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , "" // strFlagStatisticListToCheck
					 , out dtDB_Mst_InvOutType // dtDB_Mst_Supplier
					);
				//// 
				//20200715.HTTT.Loại xuất kho đã liên kết nghiệp vụ kho không cho phép xóa
				string strSqlCheck = CmUtils.StringUtils.Replace(@"
					select top 1
						t.IF_InvOutNo
					from InvF_InventoryOut t --//[mylock]
					where (1=1)
						and t.InvOutType = '@strInvOutType'
						and t.OrgID = '@strOrgID'
					;
					"
					, "@strInvOutType", strInvOutType
					, "@strOrgID", strOrgID
					);
				DataTable dtDB_InvF_InventoryOut_Check = _cf.db.ExecQuery(strSqlCheck).Tables[0];
				if (dtDB_InvF_InventoryOut_Check.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.IF_InvOutNo", dtDB_InvF_InventoryOut_Check.Rows[0]["IF_InvOutNo"]
						, "Check.InvOutType", strInvOutType
						, "Check.OrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InvOutType_Delete_InvOutTypeNotAllowDelete
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_InvOutType.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_InvOutType"
					, dtDB_Mst_InvOutType
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_Inventory:
		private void Mst_Inventory_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objInvCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, string strFlagIn_OutListToCheck
			, out DataTable dtDB_Mst_Inventory
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Inventory t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.InvCode = @objInvCode
					;
				");
			dtDB_Mst_Inventory = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objInvCode", objInvCode
				).Tables[0];
			dtDB_Mst_Inventory.TableName = "Mst_Inventory";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Inventory.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvCode", objInvCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Inventory_CheckDB_InventoryNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Inventory.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.InvCode", objInvCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Inventory_CheckDB_InventoryExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Inventory.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvCode", objInvCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Inventory.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Inventory_CheckDB_InventoryStatusNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			// strFlagIn_OutListToCheck:
			if (strFlagIn_OutListToCheck.Length > 0 && !strFlagIn_OutListToCheck.Contains(Convert.ToString(dtDB_Mst_Inventory.Rows[0]["FlagIn_Out"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.InvCode", objInvCode
					, "Check.strFlagIn_OutListToCheck", strFlagIn_OutListToCheck
					, "DB.FlagIn_Out", dtDB_Mst_Inventory.Rows[0]["FlagIn_Out"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Inventory_CheckDB_InventoryStatusNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void Mst_Inventory_UpdBU()
		{
			string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strInvCode_Root nvarchar(100); select @strInvCode_Root = '@strInvRoot';

                    update t
                    set
	                    t.InvBUCode = @strInvCode_Root
	                    , t.InvBUPattern = @strInvCode_Root + '%'
	                    , t.InvLevel = 1
                    from Mst_Inventory t
	                    left join Mst_Inventory t_Parent
		                    on t.OrgID = t_Parent.OrgID
								and t.InvCodeParent = t_Parent.InvCode
                    where (1=1)
	                    and t.InvCode in (@strInvCode_Root)
                    ;

                    declare @nDeepInventory int; select @nDeepInventory = 0;
                    while (@nDeepInventory <= 10)
                    begin
	                    select @nDeepInventory = @nDeepInventory + 1;
	
	                    update t
	                    set
		                    t.InvBUCode = IsNull(t_Parent.InvBUCode + '.', '') + t.InvCode
		                    , t.InvBUPattern = IsNull(t_Parent.InvBUCode + '.', '') + t.InvCode + '%'
		                    , t.InvLevel = IsNull(t_Parent.InvLevel, 0) + 1
	                    from Mst_Inventory t
		                    left join Mst_Inventory t_Parent
			                    on t.OrgID = t_Parent.OrgID
									and t.InvCodeParent = t_Parent.InvCode
	                    where (1=1)
		                    and t.InvCode not in (@strInvCode_Root)
	                    ;
                    end;
                "
				, "@strInvRoot", TConst.BizMix.InvRoot
				);
			DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
		}
		private void Mst_Inventory_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Inventory
            , string strRt_Cols_Mst_UserMapInventory
            ////
            , out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Inventory_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Inventory", strRt_Cols_Mst_Inventory
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Inventory = (strRt_Cols_Mst_Inventory != null && strRt_Cols_Mst_Inventory.Length > 0);
            bool bGet_Mst_UserMapInventory = (strRt_Cols_Mst_UserMapInventory != null && strRt_Cols_Mst_UserMapInventory.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					});
			////// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get_New20200827(
                strWAUserCode // drAbilityOfUser
                , TUtils.CUtils.StdFlag(drAbilityOfUser["FlagBG"])
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Inventory_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mi.OrgID
							, mi.InvCode
						into #tbl_Mst_Inventory_Filter_Draft
						from Mst_Inventory mi --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on mi.OrgID = t_MstNNT_View.OrgID
                            inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                                on mi.OrgID = t_mi_View.OrgID
                                    and mi.InvCode = t_mi_View.InvCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mi.OrgID asc 
							, mi.InvCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Inventory_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Inventory_Filter:
						select
							t.*
						into #tbl_Mst_Inventory_Filter
						from #tbl_Mst_Inventory_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Inventory --------:
						zzB_Select_Mst_Inventory_zzE
						----------------------------------------

						-------- Mst_UserMapInventory --------:
						zzB_Select_Mst_UserMapInventory_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Inventory_Filter_Draft;
						--drop table #tbl_Mst_Inventory_Filter;
					"
                );
			////
			string zzB_Select_Mst_Inventory_zzE = "-- Nothing.";
			if (bGet_Mst_Inventory)
			{
				#region // bGet_Mst_Inventory:
				zzB_Select_Mst_Inventory_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Inventory:
                        select
                            t.MyIdxSeq
	                        , mi.*
							, mgln.GLNName mgln_GLNName
							, mgln.GPSLat mgln_GPSLat
							, mgln.GPSLong mgln_GPSLong
                        from #tbl_Mst_Inventory_Filter t --//[mylock]
	                        inner join Mst_Inventory mi --//[mylock]
		                        on t.OrgID = mi.OrgID
									and t.InvCode = mi.InvCode
							left join Mst_GLN mgln --//[mylock]
								on mi.GLNCode = mgln.GLNCode
                        order by 
							t.MyIdxSeq asc
                        ;
						"
                    );
				#endregion
			}
            ////
            string zzB_Select_Mst_UserMapInventory_zzE = "-- Nothing.";
            if (bGet_Mst_UserMapInventory)
            {
                #region // bGet_Mst_UserMapInventory:
                zzB_Select_Mst_UserMapInventory_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_UserMapInventory:
                        select
                            t.MyIdxSeq
	                        , mumi.*
                        from #tbl_Mst_Inventory_Filter t --//[mylock]
	                        inner join Mst_UserMapInventory mumi --//[mylock]
		                        on t.OrgID = mumi.OrgID
									and t.InvCode = mumi.InvCode
                        order by 
							t.MyIdxSeq asc
                        ;
						"
                    );
                #endregion
            }
            string zzB_Where_strFilter_zzE = "";
			{
				Hashtable htSpCols = new Hashtable();
				{
					#region // htSpCols:
					////
					TUtils.CUtils.MyBuildHTSupportedColumns(
						_cf.db // db
						, ref htSpCols // htSupportedColumns
						, "Mst_Inventory" // strTableNameDB
						, "Mst_Inventory." // strPrefixStd
						, "mi." // strPrefixAlias
						);
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_UserMapInventory" // strTableNameDB
                        , "Mst_UserMapInventory." // strPrefixStd
                        , "mumi." // strPrefixAlias
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
				, "zzB_Select_Mst_Inventory_zzE", zzB_Select_Mst_Inventory_zzE
                , "zzB_Select_Mst_UserMapInventory_zzE", zzB_Select_Mst_UserMapInventory_zzE
                );
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Inventory)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Inventory";
			}
            if (bGet_Mst_UserMapInventory)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_UserMapInventory";
            }
            #endregion
        }

        private void Mst_Inventory_GetByUserX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objUserCode
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_Inventory_GetByUserX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUserCode", objUserCode
                });
            #endregion

            #region // Check:
            //// Refine:
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    });
            ////// drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get_New20200827(
                strWAUserCode // drAbilityOfUser
                , TUtils.CUtils.StdFlag(drAbilityOfUser["FlagBG"])
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Inventory_Filter_Draft:
						select distinct
							mi.OrgID
							, mi.InvCode
						into #tbl_Mst_Inventory_Filter_Draft
						from Mst_Inventory mi --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on mi.OrgID = t_MstNNT_View.OrgID
                            inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                                on mi.OrgID = t_mi_View.OrgID
                                    and mi.InvCode = t_mi_View.InvCode
						where (1=1)
						;

						---- #tbl_Mst_Inventory_Filter:
						select
							t.*
						into #tbl_Mst_Inventory_Filter
						from Mst_UserMapInventory t --//[mylock]
							inner join #tbl_Mst_Inventory_Filter_Draft f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
						where(1=1)
							and t.UserCode = '@strUserCode'
						;

						---- Return:
						select
							f.*
						from #tbl_Mst_Inventory_Filter t --//[mylock]
							inner join Mst_Inventory f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCodeParent
						where(1=1)
                            and f.FlagActive = '1'
						;

						---- Clear for debug:
						--drop table #tbl_Mst_Inventory_Filter_Draft;
						--drop table #tbl_Mst_Inventory_Filter;
					"
                , "@strUserCode", strUserCode
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Mst_Inventory";
            #endregion
        }

        public DataSet Mst_Inventory_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Inventory
            , string strRt_Cols_Mst_UserMapInventory
            )
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Inventory_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Inventory", strRt_Cols_Mst_Inventory
                , "strRt_Cols_Mst_UserMapInventory", strRt_Cols_Mst_UserMapInventory
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

				////// Tạm thời bỏ do nhúng với inos
				//Sys_User_CheckAuthorize(
				//	strTid // strTid
				//	, strGwUserCode // strGwUserCode
				//	, strGwPassword // strGwPassword
				//	, strWAUserCode // strWAUserCode
				//					//, strWAUserPassword // strWAUserPassword
				//	, ref mdsFinal // mdsFinal
				//	, ref alParamsCoupleError // alParamsCoupleError
				//	, dtimeSys // dtimeSys
				//	, strAccessToken // strAccessToken
				//	, strNetworkID // strNetworkID
				//	, strOrgID_RQ // strOrgID
				//	, TConst.Flag.Active // strFlagUserCodeToCheck
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Mst_Inventory_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_Inventory_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_Inventory // strRt_Cols_Mst_Inventory
                        , strRt_Cols_Mst_UserMapInventory // strRt_Cols_Mst_UserMapInventory
                                                          ////
                        , out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet Mst_Inventory_GetByUser(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID_RQ
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Inventory_GetByUser";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_GetByUser;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objUserCode", objUserCode
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

                ////
                //Sys_User_CheckAuthorize(
                //    strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // strWAUserCode
                //                    //, strWAUserPassword // strWAUserPassword
                //    , ref mdsFinal // mdsFinal
                //    , ref alParamsCoupleError // alParamsCoupleError
                //    , dtimeSys // dtimeSys
                //    , strAccessToken // strAccessToken
                //    , strNetworkID // strNetworkID
                //    , strOrgID_RQ // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Mst_Inventory_GetByUserX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_Inventory_GetByUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , objUserCode // objUserCode
                        ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        public DataSet WAS_Mst_Inventory_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Inventory objRQ_Mst_Inventory
			////
			, out RT_Mst_Inventory objRT_Mst_Inventory
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Inventory.Tid;
			objRT_Mst_Inventory = new RT_Mst_Inventory();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Inventory_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_Get;
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
				List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
                List<Mst_UserMapInventory> lst_Mst_UserMapInventory = new List<Mst_UserMapInventory>();
                ////
                bool bGet_Mst_Inventory = (objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory != null && objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory.Length > 0);
                bool bGet_Mst_UserMapInventory = (objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory != null && objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory.Length > 0);

                #endregion

                #region // WAS_Mst_Inventory_Get:
                mdsResult = Mst_Inventory_Get(
					objRQ_Mst_Inventory.Tid // strTid
					, objRQ_Mst_Inventory.GwUserCode // strGwUserCode
					, objRQ_Mst_Inventory.GwPassword // strGwPassword
					, objRQ_Mst_Inventory.WAUserCode // strUserCode
					, objRQ_Mst_Inventory.WAUserPassword // strUserPassword
					, objRQ_Mst_Inventory.AccessToken // strAccessToken
					, objRQ_Mst_Inventory.NetworkID // strNetworkID
					, objRQ_Mst_Inventory.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Inventory.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Inventory.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Inventory.Ft_WhereClause // strFt_WhereClause
														 //// Return:
					, objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory // strRt_Cols_Mst_Inventory
                    , objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory // strRt_Cols_Mst_UserMapInventory
                    );
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Inventory.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    if (bGet_Mst_Inventory)
                    {
                        DataTable dt_Mst_Inventory = mdsResult.Tables["Mst_Inventory"].Copy();
                        lst_Mst_Inventory = TUtils.DataTableCmUtils.ToListof<Mst_Inventory>(dt_Mst_Inventory);
                        objRT_Mst_Inventory.Lst_Mst_Inventory = lst_Mst_Inventory;
                        ////
                    }
                    ////
                    if (bGet_Mst_UserMapInventory)
                    {
                        DataTable dt_Mst_UserMapInventory = mdsResult.Tables["Mst_UserMapInventory"].Copy();
                        lst_Mst_UserMapInventory = TUtils.DataTableCmUtils.ToListof<Mst_UserMapInventory>(dt_Mst_UserMapInventory);
                        objRT_Mst_Inventory.Lst_Mst_UserMapInventory = lst_Mst_UserMapInventory;
                        /////
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

        public DataSet WAS_Mst_Inventory_GetByUser(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Inventory objRQ_Mst_Inventory
            ////
            , out RT_Mst_Inventory objRT_Mst_Inventory
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Inventory.Tid;
            objRT_Mst_Inventory = new RT_Mst_Inventory();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Inventory_GetByUser";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_GetByUser;
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
                List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
                List<Mst_UserMapInventory> lst_Mst_UserMapInventory = new List<Mst_UserMapInventory>();
                ////
                #endregion

                #region // WAS_Mst_Inventory_Get:
                mdsResult = Mst_Inventory_GetByUser(
                    objRQ_Mst_Inventory.Tid // strTid
                    , objRQ_Mst_Inventory.GwUserCode // strGwUserCode
                    , objRQ_Mst_Inventory.GwPassword // strGwPassword
                    , objRQ_Mst_Inventory.WAUserCode // strUserCode
                    , objRQ_Mst_Inventory.WAUserPassword // strUserPassword
                    , objRQ_Mst_Inventory.AccessToken // strAccessToken
                    , objRQ_Mst_Inventory.NetworkID // strNetworkID
                    , objRQ_Mst_Inventory.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Inventory.UserCode // UserCode
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Mst_Inventory = mdsResult.Tables["Mst_Inventory"].Copy();
                    lst_Mst_Inventory = TUtils.DataTableCmUtils.ToListof<Mst_Inventory>(dt_Mst_Inventory);
                    objRT_Mst_Inventory.Lst_Mst_Inventory = lst_Mst_Inventory;
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

        public DataSet Mst_Inventory_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objInvCode
			, object objInvCodeParent
			, object objInvLevelType
			, object objInvType
			, object objInvName
			, object objInvAddress
			, object objInvContactName
			, object objInvContactPhone
			, object objInvContactEmail
			, object objFlagIn_Out
			, object objFlagActive
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			DateTime dtimeSys = DateTime.Now;
			string strFunctionName = "Mst_Inventory_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_Create;
			alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "objInvCode", objInvCode
					, "objInvCodeParent", objInvCodeParent
					, "objInvLevelType", objInvLevelType
					, "objInvType", objInvType
					, "objInvName", objInvName
					, "objInvAddress", objInvAddress
					, "objInvContactName", objInvContactName
					, "objInvContactPhone", objInvContactPhone
					, "objInvContactEmail", objInvContactEmail
					, "objFlagActive", objFlagActive
					, "objFlagIn_Out", objFlagIn_Out
					, "objRemark", objRemark
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryType_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_Inventory_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objOrgID // objOrgID
						, objInvCode // objInvCode
						, objInvCodeParent // objInvCodeParent
						, objInvLevelType // objInvLevelType
						, objInvType // objInvType
						, objInvName // objInvName
						, objInvAddress // objInvAddress
						, objInvContactName // objInvContactName
						, objInvContactPhone // objInvContactPhone
						, objInvContactEmail // objInvContactEmail
						, objFlagIn_Out // objFlagIn_Out
						, objFlagActive // objFlagActive
						, objRemark // objRemark
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

		private void Mst_Inventory_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvCode
			, object objInvCodeParent
			, object objInvLevelType
			, object objInvType
			, object objInvName
			, object objInvAddress
			, object objInvContactName
			, object objInvContactPhone
			, object objInvContactEmail
			, object objFlagIn_Out
			, object objFlagActive
			, object objRemark
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Inventory_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "objInvCode", objInvCode
				, "objInvCodeParent", objInvCodeParent
				, "objInvLevelType", objInvLevelType
				, "objInvType", objInvType
				, "objInvName", objInvName
				, "objInvAddress", objInvAddress
				, "objInvContactName", objInvContactName
				, "objInvContactPhone", objInvContactPhone
				, "objInvContactEmail", objInvContactEmail
				, "objFlagActive", objFlagActive
				, "objFlagIn_Out", objFlagIn_Out
				, "objRemark", objRemark
				});
			#endregion

			#region // Refine and Check Input:
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvCode = TUtils.CUtils.StdParam(objInvCode);
			string strInvCodeParent = TUtils.CUtils.StdParam(objInvCodeParent);
			string strInvLevelType = TUtils.CUtils.StdParam(objInvLevelType);
			string strInvType = TUtils.CUtils.StdParam(objInvType);
			string strInvName = string.Format("{0}", objInvName).Trim();
			string strInvAddress = string.Format("{0}", objInvAddress).Trim();
			string strInvContactName = string.Format("{0}", objInvContactName).Trim();
			string strInvContactPhone = string.Format("{0}", objInvContactPhone).Trim();
			string strInvContactEmail = string.Format("{0}", objInvContactEmail).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strFlagIn_Out = TUtils.CUtils.StdFlag(objFlagIn_Out);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			DataTable dtDB_Mst_Inventory = null;
			DataTable dtDB_Mst_InventoryParrent = null;
			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[] {
							"Check.OrgID", strOrgID
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Inventory_Create_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strInvCode == null || strInvCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[] {
							"Check.InvCode", strInvCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Inventory_Create_InvalidInvCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Mst_Org = null;
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_Inventory_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvCode // objInvCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, "" // strFlagIn_OutListToCheck
					, out dtDB_Mst_Inventory // dtDB_Mst_Inventory
					);
				////
				if (strInvCodeParent != null && strInvCodeParent.Length > 0)
				{
					Mst_Inventory_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // objOrgID
						, strInvCodeParent // objInvCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, "" // strFlagIn_OutListToCheck
						, out dtDB_Mst_InventoryParrent // dtDB_Mst_Inventory
						);
				}
				////
				DataTable dtDB_Mst_InventoryLevelType = null;

				Mst_InventoryLevelType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvLevelType // objInvLevelType
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_InventoryLevelType // dtDB_Mst_InventoryLevelType
					);
				////
				DataTable dtDB_Mst_InventoryType = null;

				Mst_InventoryType_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvType // objInvType
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_InventoryType // dtDB_Mst_InventoryType
					);
				////
				if (strInvName == null || strInvName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.strInvName", strInvName
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Inventory_Create_InvalidInvName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}
			#endregion

			#region // SaveDB  Mst_Inventory:
			{
				// Init:
				// ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Inventory.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "InvCode"; drDB[strFN] = strInvCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvCodeParent"; drDB[strFN] = strInvCodeParent;
				strFN = "InvBUCode"; drDB[strFN] = "X";
				strFN = "InvBUPattern"; drDB[strFN] = "X";
				strFN = "InvLevel"; drDB[strFN] = 1;
				strFN = "InvLevelType"; drDB[strFN] = strInvLevelType;
				strFN = "InvType"; drDB[strFN] = strInvType;
				strFN = "InvName"; drDB[strFN] = strInvName;
				strFN = "InvAddress"; drDB[strFN] = strInvAddress;
				strFN = "InvContactName"; drDB[strFN] = strInvContactName;
				strFN = "InvContactPhone"; drDB[strFN] = strInvContactPhone;
				strFN = "InvContactEmail"; drDB[strFN] = strInvContactEmail;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "FlagIn_Out"; drDB[strFN] = strFlagIn_Out;
				strFN = "FlagActive"; drDB[strFN] = strFlagActive; 
				 strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Inventory.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Inventory"
					, dtDB_Mst_Inventory
					);
			}
			#endregion

			#region // Post Save:
			{
				Mst_Inventory_UpdBU();
			}
            #endregion

            #region // Check InvCode:
            // Check InvCode input phải không có InvBUCode match InvBUPatern với kho cùng cấp
            string strSqlCheckInvCode = CmUtils.StringUtils.Replace(@"
                    --- #input_Mst_Inventory:
                    select
	                    t.OrgID
	                    , t.InvCode
	                    , t.InvCodeParent
	                    , t.InvBUCode
	                    , t.InvBUPattern
	                    , t.InvLevel
                    into #input_Mst_Inventory
                    from Mst_Inventory t with(nolock)
                    where(1=1)
	                    and t.OrgID = @strOrgID
	                    and t.InvCode = @strInvCode
                    ;
                    -- select null input_Mst_Inventory, * from #input_Mst_Inventory;
                    -- drop table #input_Mst_Inventory;

                    --- #tbl_Mst_Inventory_DirectSibling:
                    select
	                    t.OrgID
	                    , t.InvCode
	                    , t.InvCodeParent
	                    , t.InvBUCode
	                    , t.InvBUPattern
	                    , t.InvLevel
                    into #tbl_Mst_Inventory_DirectSibling
                    from Mst_Inventory t with(nolock)
	                    left join #input_Mst_Inventory f with(nolock)
		                    on (1=1)
                    where(1=1)
	                    and t.InvCodeParent = f.InvCodeParent
	                    and t.InvCode <> f.InvCode
                    ;
                    -- select null tbl_Mst_Inventory_DirectSibling, * from #tbl_Mst_Inventory_DirectSibling;
                    -- drop table #tbl_Mst_Inventory_DirectSibling;

                    --- Check:
                    select
	                    t.*
                    from #tbl_Mst_Inventory_DirectSibling t with(nolock)
	                    left join #input_Mst_Inventory f with(nolock)
		                    on (1=1)
                    where(1=1)
	                    and f.InvBUCode like t.InvBUPattern
                    ;

                    --- Clear For Debug:
                    drop table #input_Mst_Inventory;
                    drop table #tbl_Mst_Inventory_DirectSibling;
                ");
            DataTable dtCheckInvCode = _cf.db.ExecQuery(
                strSqlCheckInvCode
                , "@strInvCode", strInvCode
                , "@strOrgID", strOrgID
                ).Tables[0];
            if (dtCheckInvCode.Rows.Count > 0)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.Input.InvCode", strInvCode
                    , "Check.Input.OrgID", strOrgID
                    , "Check.DB.InvCode", dtCheckInvCode.Rows[0]["InvCode"]
                    , "Check.DB.OrgID", dtCheckInvCode.Rows[0]["OrgID"]
                    , "Check.DB.InvBUCode", dtCheckInvCode.Rows[0]["InvBUCode"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Inventory_Create_InvalidInvCode
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

		public DataSet WAS_Mst_Inventory_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Inventory objRQ_Mst_Inventory
			////
			, out RT_Mst_Inventory objRT_Mst_Inventory
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Inventory.Tid;
			objRT_Mst_Inventory = new RT_Mst_Inventory();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Inventory.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Inventory_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Inventory", TJson.JsonConvert.SerializeObject(objRQ_Mst_Inventory.Mst_Inventory)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
				//List<Mst_InventoryInGroup> lst_Mst_InventoryInGroup = new List<Mst_InventoryInGroup>();
				#endregion

				#region // Mst_Inventory_Create:
				mdsResult = Mst_Inventory_Create(
					objRQ_Mst_Inventory.Tid // strTid
					, objRQ_Mst_Inventory.GwUserCode // strGwUserCode
					, objRQ_Mst_Inventory.GwPassword // strGwPassword
					, objRQ_Mst_Inventory.WAUserCode // strUserCode
					, objRQ_Mst_Inventory.WAUserPassword // strUserPassword
					, objRQ_Mst_Inventory.AccessToken // strAccessToken
					, objRQ_Mst_Inventory.NetworkID // strNetworkID
					, objRQ_Mst_Inventory.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Inventory.Mst_Inventory.OrgID // objOrgID
					, objRQ_Mst_Inventory.Mst_Inventory.InvCode // objInvCode
					, objRQ_Mst_Inventory.Mst_Inventory.InvCodeParent // objInvCodeParent
					, objRQ_Mst_Inventory.Mst_Inventory.InvLevelType // objInvLevelType
					, objRQ_Mst_Inventory.Mst_Inventory.InvType // objInvType
					, objRQ_Mst_Inventory.Mst_Inventory.InvName // objInvName
					, objRQ_Mst_Inventory.Mst_Inventory.InvAddress // objInvAddress
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactName // objInvContactName
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactPhone // objInvContactPhone
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactEmail // objInvContactEmail
					, objRQ_Mst_Inventory.Mst_Inventory.FlagIn_Out // objFlagIn_Out
					, objRQ_Mst_Inventory.Mst_Inventory.FlagActive // objFlagActive
					, objRQ_Mst_Inventory.Mst_Inventory.Remark // objRemark
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

		public DataSet Mst_Inventory_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//, DateTime dtimeSys
			////
			, object objOrgID
			, object objInvCode
			//, object objInvCodeParent
			, object objInvLevelType
			, object objInvType
			, object objInvName
			, object objInvAddress
			, object objInvContactName
			, object objInvContactPhone
			, object objInvContactEmail
			, object objFlagIn_Out
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			DateTime dtimeSys = DateTime.Now;
			string strFunctionName = "Mst_Inventory_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_Update;
			alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "objInvCode", objInvCode
					//, "objInvCodeParent", objInvCodeParent
					, "objInvLevelType", objInvLevelType
					, "objInvType", objInvType
					, "objInvName", objInvName
					, "objInvAddress", objInvAddress
					, "objInvContactName", objInvContactName
					, "objInvContactPhone", objInvContactPhone
					, "objInvContactEmail", objInvContactEmail
					, "objFlagActive", objFlagActive
					, "objFlagIn_Out", objFlagIn_Out
					, "objRemark", objRemark
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Inventory_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_Inventory_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvCode // objInvCode
						//, objInvCodeParent
						, objInvLevelType // objInvLevelType
						, objInvType // objInvType
						, objInvName // objInvName
						, objInvAddress // objInvAddress
						, objInvContactName // objInvContactName
						, objInvContactPhone // objInvContactPhone
						, objInvContactEmail // objInvContactEmail
						, objFlagIn_Out // objFlagIn_Out
						, objFlagActive // objFlagActive
						, objRemark // objRemark
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void Mst_Inventory_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvCode
			//, object objInvCodeParent
			, object objInvLevelType
			, object objInvType
			, object objInvName
			, object objInvAddress
			, object objInvContactName
			, object objInvContactPhone
			, object objInvContactEmail
			, object objFlagIn_Out
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Inventory_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objInvCode", objInvCode
				//, "objInvCodeParent", objInvCodeParent
				, "objInvLevelType", objInvLevelType
				, "objInvType", objInvType
				, "objInvName", objInvName
				, "objInvAddress", objInvAddress
				, "objInvContactName", objInvContactName
				, "objInvContactPhone", objInvContactPhone
				, "objInvContactEmail", objInvContactEmail
				, "objFlagActive", objFlagActive
				, "objFlagIn_Out", objFlagIn_Out
				, "objRemark", objRemark
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvCode = TUtils.CUtils.StdParam(objInvCode);
			//string strInvCodeParent = TUtils.CUtils.StdParam(objInvCodeParent);
			string strInvLevelType = TUtils.CUtils.StdParam(objInvLevelType);
			string strInvType = TUtils.CUtils.StdParam(objInvType);
			string strInvName = string.Format("{0}", objInvName).Trim();
			string strInvAddress = string.Format("{0}", objInvAddress).Trim();
			string strInvContactName = string.Format("{0}", objInvContactName).Trim();
			string strInvContactPhone = string.Format("{0}", objInvContactPhone).Trim();
			string strInvContactEmail = string.Format("{0}", objInvContactEmail).Trim();
			string strFlagIn_Out = TUtils.CUtils.StdFlag(objFlagIn_Out);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			bool bUpd_InvLevelType = strFt_Cols_Upd.Contains("Mst_Inventory.InvLevelType".ToUpper());
            bool bUpd_InvType = strFt_Cols_Upd.Contains("Mst_Inventory.InvType".ToUpper());
            bool bUpd_InvName = strFt_Cols_Upd.Contains("Mst_Inventory.InvName".ToUpper());
            bool bUpd_InvAddress = strFt_Cols_Upd.Contains("Mst_Inventory.InvAddress".ToUpper());
			bool bUpd_InvContactName = strFt_Cols_Upd.Contains("Mst_Inventory.InvContactName".ToUpper());
			bool bUpd_InvContactPhone = strFt_Cols_Upd.Contains("Mst_Inventory.InvContactPhone".ToUpper());
			bool bUpd_InvContactEmail = strFt_Cols_Upd.Contains("Mst_Inventory.InvContactEmail".ToUpper());
			bool bUpd_FlagIn_Out = strFt_Cols_Upd.Contains("Mst_Inventory.FlagIn_Out".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Inventory.FlagActive".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("Mst_Inventory.Remark".ToUpper());
			////
			DataTable dtDB_Mst_Inventory = null;
			DataTable dtDB_Mst_Org = null;
			{
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_Inventory_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strInvCode // objInvCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, "" // strFlagIn_OutListToCheck
					, out dtDB_Mst_Inventory // dtDB_Mst_Inventory
					);
				////
			}
			#endregion

			#region // SaveDB  Mst_Inventory:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Inventory.Rows[0];
				if (bUpd_InvLevelType) { strFN = "InvLevelType"; drDB[strFN] = strInvLevelType; alColumnEffective.Add(strFN); }
                if (bUpd_InvType) { strFN = "InvType"; drDB[strFN] = strInvType; alColumnEffective.Add(strFN); }
                if (bUpd_InvName) { strFN = "InvName"; drDB[strFN] = strInvName; alColumnEffective.Add(strFN); }
                if (bUpd_InvAddress) { strFN = "InvAddress"; drDB[strFN] = strInvAddress; alColumnEffective.Add(strFN); }
				if (bUpd_InvContactName) { strFN = "InvContactName"; drDB[strFN] = strInvContactName; alColumnEffective.Add(strFN); }
				if (bUpd_InvContactPhone) { strFN = "InvContactPhone"; drDB[strFN] = strInvContactPhone; alColumnEffective.Add(strFN); }
				if (bUpd_InvContactEmail) { strFN = "InvContactEmail"; drDB[strFN] = strInvContactEmail; alColumnEffective.Add(strFN); }
				if (bUpd_FlagIn_Out) { strFN = "FlagIn_Out"; drDB[strFN] = strFlagIn_Out; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_Inventory"
					, dtDB_Mst_Inventory
					, alColumnEffective.ToArray()
					);
			}
			#endregion

			#region // Post Save:
			{
				Mst_Inventory_UpdBU();
			}
			#endregion

		}

		public DataSet WAS_Mst_Inventory_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Inventory objRQ_Mst_Inventory
			////
			, out RT_Mst_Inventory objRT_Mst_Inventory
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Inventory.Tid;
			objRT_Mst_Inventory = new RT_Mst_Inventory();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Inventory.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Inventory_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Inventory", TJson.JsonConvert.SerializeObject(objRQ_Mst_Inventory.Mst_Inventory)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
				//List<Mst_InventoryInGroup> lst_Mst_InventoryInGroup = new List<Mst_InventoryInGroup>();
				#endregion

				#region // Mst_Inventory_Update:
				mdsResult = Mst_Inventory_Update(
					objRQ_Mst_Inventory.Tid // strTid
					, objRQ_Mst_Inventory.GwUserCode // strGwUserCode
					, objRQ_Mst_Inventory.GwPassword // strGwPassword
					, objRQ_Mst_Inventory.WAUserCode // strUserCode
					, objRQ_Mst_Inventory.WAUserPassword // strUserPassword
					, objRQ_Mst_Inventory.AccessToken // strAccessToken
					, objRQ_Mst_Inventory.NetworkID // strNetworkID
					, objRQ_Mst_Inventory.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Inventory.Mst_Inventory.OrgID // objOrgID
					, objRQ_Mst_Inventory.Mst_Inventory.InvCode // objInvCode
					, objRQ_Mst_Inventory.Mst_Inventory.InvLevelType // objInvLevelType
					, objRQ_Mst_Inventory.Mst_Inventory.InvType // objInvType
					, objRQ_Mst_Inventory.Mst_Inventory.InvName // objInvName
					, objRQ_Mst_Inventory.Mst_Inventory.InvAddress // objInvAddress
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactName // objInvContactName
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactPhone // objInvContactPhone
					, objRQ_Mst_Inventory.Mst_Inventory.InvContactEmail // objInvContactEmail
					, objRQ_Mst_Inventory.Mst_Inventory.FlagIn_Out // objFlagIn_Out
					, objRQ_Mst_Inventory.Mst_Inventory.FlagActive // objFlagActive
					, objRQ_Mst_Inventory.Mst_Inventory.Remark // objRemark
															   ////
					, objRQ_Mst_Inventory.Ft_Cols_Upd// objFt_Cols_Upd
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

		public DataSet Mst_Inventory_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			/////
			, object objOrgID
			, object objInvCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Inventory_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objOrgID", objOrgID
					, "objInvCode", objInvCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InventoryType_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_Inventory_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objInvCode // objInvCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void Mst_Inventory_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objInvCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Inventory_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				, "objInvCode", objInvCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strInvCode = TUtils.CUtils.StdParam(objInvCode);
			////
			DataTable dtDB_Mst_Inventory = null;
			DataTable dtDB_Mst_Org = null;
			{
				////
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				Mst_Inventory_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strOrgID // objOrgID
					 , strInvCode // objInvCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , "" // strFlagIn_OutListToCheck
					 , out dtDB_Mst_Inventory // dtDB_Mst_Inventory
					);
			}
			#endregion

			#region // SaveDB Mst_Inventory:
			{
				// Init:
				dtDB_Mst_Inventory.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_Inventory"
					, dtDB_Mst_Inventory
					);
			}
			#endregion
		}

		public DataSet WAS_Mst_Inventory_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Inventory objRQ_Mst_Inventory
			////
			, out RT_Mst_Inventory objRT_Mst_Inventory
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Inventory.Tid;
			objRT_Mst_Inventory = new RT_Mst_Inventory();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Inventory.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Inventory_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Inventory", TJson.JsonConvert.SerializeObject(objRQ_Mst_Inventory.Mst_Inventory)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
				//List<Mst_InventoryInGroup> lst_Mst_InventoryInGroup = new List<Mst_InventoryInGroup>();
				#endregion

				#region // Mst_Inventory_Update:
				mdsResult = Mst_Inventory_Delete(
					objRQ_Mst_Inventory.Tid // strTid
					, objRQ_Mst_Inventory.GwUserCode // strGwUserCode
					, objRQ_Mst_Inventory.GwPassword // strGwPassword
					, objRQ_Mst_Inventory.WAUserCode // strUserCode
					, objRQ_Mst_Inventory.WAUserPassword // strUserPassword
					, objRQ_Mst_Inventory.AccessToken // strAccessToken
					, objRQ_Mst_Inventory.NetworkID // strNetworkID
					, objRQ_Mst_Inventory.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Inventory.Mst_Inventory.OrgID // objOrgID
					, objRQ_Mst_Inventory.Mst_Inventory.InvCode // objInvCode
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

        public DataSet WAS_Mst_Inventory_GetForUserMapInv(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Inventory objRQ_Mst_Inventory
            ////
            , out RT_Mst_Inventory objRT_Mst_Inventory
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Inventory.Tid;
            objRT_Mst_Inventory = new RT_Mst_Inventory();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Inventory_GetForUserMapInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Inventory_GetForUserMapInv;
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
                List<Mst_Inventory> lst_Mst_Inventory = new List<Mst_Inventory>();
                List<Mst_UserMapInventory> lst_Mst_UserMapInventory = new List<Mst_UserMapInventory>();
                ////
                bool bGet_Mst_Inventory = (objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory != null && objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory.Length > 0);
                bool bGet_Mst_UserMapInventory = (objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory != null && objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory.Length > 0);

                #endregion

                #region // WAS_Mst_Inventory_GetForUserMapInv:
                mdsResult = Mst_Inventory_GetForUserMapInv(
                    objRQ_Mst_Inventory.Tid // strTid
                    , objRQ_Mst_Inventory.GwUserCode // strGwUserCode
                    , objRQ_Mst_Inventory.GwPassword // strGwPassword
                    , objRQ_Mst_Inventory.WAUserCode // strUserCode
                    , objRQ_Mst_Inventory.WAUserPassword // strUserPassword
                    , objRQ_Mst_Inventory.AccessToken // strAccessToken
                    , objRQ_Mst_Inventory.NetworkID // strNetworkID
                    , objRQ_Mst_Inventory.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Inventory.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Inventory.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Inventory.Ft_WhereClause // strFt_WhereClause
                                                         //// Return:
                    , objRQ_Mst_Inventory.Rt_Cols_Mst_Inventory // strRt_Cols_Mst_Inventory
                    , objRQ_Mst_Inventory.Rt_Cols_Mst_UserMapInventory // strRt_Cols_Mst_UserMapInventory
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Inventory.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    if (bGet_Mst_Inventory)
                    {
                        DataTable dt_Mst_Inventory = mdsResult.Tables["Mst_Inventory"].Copy();
                        lst_Mst_Inventory = TUtils.DataTableCmUtils.ToListof<Mst_Inventory>(dt_Mst_Inventory);
                        objRT_Mst_Inventory.Lst_Mst_Inventory = lst_Mst_Inventory;
                        ////
                    }
                    ////
                    if (bGet_Mst_UserMapInventory)
                    {
                        DataTable dt_Mst_UserMapInventory = mdsResult.Tables["Mst_UserMapInventory"].Copy();
                        lst_Mst_UserMapInventory = TUtils.DataTableCmUtils.ToListof<Mst_UserMapInventory>(dt_Mst_UserMapInventory);
                        objRT_Mst_Inventory.Lst_Mst_UserMapInventory = lst_Mst_UserMapInventory;
                        /////
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

        public DataSet Mst_Inventory_GetForUserMapInv(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID_RQ
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Inventory
            , string strRt_Cols_Mst_UserMapInventory
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Inventory_GetForUserMapInv";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Inventory_GetForUserMapInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Inventory", strRt_Cols_Mst_Inventory
                , "strRt_Cols_Mst_UserMapInventory", strRt_Cols_Mst_UserMapInventory
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

                ////// Tạm thời bỏ do nhúng với inos
                //Sys_User_CheckAuthorize(
                //	strTid // strTid
                //	, strGwUserCode // strGwUserCode
                //	, strGwPassword // strGwPassword
                //	, strWAUserCode // strWAUserCode
                //					//, strWAUserPassword // strWAUserPassword
                //	, ref mdsFinal // mdsFinal
                //	, ref alParamsCoupleError // alParamsCoupleError
                //	, dtimeSys // dtimeSys
                //	, strAccessToken // strAccessToken
                //	, strNetworkID // strNetworkID
                //	, strOrgID_RQ // strOrgID
                //	, TConst.Flag.Active // strFlagUserCodeToCheck
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Mst_Inventory_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_Inventory_GetForUserMapInvX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_Inventory // strRt_Cols_Mst_Inventory
                        , strRt_Cols_Mst_UserMapInventory // strRt_Cols_Mst_UserMapInventory
                                                          ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        private void Mst_Inventory_GetForUserMapInvX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Inventory
            , string strRt_Cols_Mst_UserMapInventory
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_Inventory_GetForUserMapInvX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Inventory", strRt_Cols_Mst_Inventory
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_Inventory = (strRt_Cols_Mst_Inventory != null && strRt_Cols_Mst_Inventory.Length > 0);
            bool bGet_Mst_UserMapInventory = (strRt_Cols_Mst_UserMapInventory != null && strRt_Cols_Mst_UserMapInventory.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
            ////// drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_GetForInventory(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            //zzzzClauseSelect_Mst_Inventory_ViewAbility_Get_New20200827(
            //    strWAUserCode // drAbilityOfUser
            //    , TUtils.CUtils.StdFlag(drAbilityOfUser["FlagBG"])
            //    , ref alParamsCoupleError // alParamsCoupleError
            //    );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Inventory_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mi.OrgID
							, mi.InvCode
						into #tbl_Mst_Inventory_Filter_Draft
						from Mst_Inventory mi --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on mi.OrgID = t_MstNNT_View.OrgID
                            --inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            --    on mi.OrgID = t_mi_View.OrgID
                            --        and mi.InvCode = t_mi_View.InvCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mi.OrgID asc 
							, mi.InvCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Inventory_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Inventory_Filter:
						select
							t.*
						into #tbl_Mst_Inventory_Filter
						from #tbl_Mst_Inventory_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Inventory --------:
						zzB_Select_Mst_Inventory_zzE
						----------------------------------------

						-------- Mst_UserMapInventory --------:
						zzB_Select_Mst_UserMapInventory_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Inventory_Filter_Draft;
						--drop table #tbl_Mst_Inventory_Filter;
					"
                );
            ////
            string zzB_Select_Mst_Inventory_zzE = "-- Nothing.";
            if (bGet_Mst_Inventory)
            {
                #region // bGet_Mst_Inventory:
                zzB_Select_Mst_Inventory_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Inventory:
                        select
                            t.MyIdxSeq
	                        , mi.*
                        from #tbl_Mst_Inventory_Filter t --//[mylock]
	                        inner join Mst_Inventory mi --//[mylock]
		                        on t.OrgID = mi.OrgID
									and t.InvCode = mi.InvCode
                        order by 
							t.MyIdxSeq asc
                        ;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_Mst_UserMapInventory_zzE = "-- Nothing.";
            if (bGet_Mst_UserMapInventory)
            {
                #region // bGet_Mst_UserMapInventory:
                zzB_Select_Mst_UserMapInventory_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_UserMapInventory:
                        select
                            t.MyIdxSeq
	                        , mumi.*
                        from #tbl_Mst_Inventory_Filter t --//[mylock]
	                        inner join Mst_UserMapInventory mumi --//[mylock]
		                        on t.OrgID = mumi.OrgID
									and t.InvCode = mumi.InvCode
                        order by 
							t.MyIdxSeq asc
                        ;
						"
                    );
                #endregion
            }
            string zzB_Where_strFilter_zzE = "";
            {
                Hashtable htSpCols = new Hashtable();
                {
                    #region // htSpCols:
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_Inventory" // strTableNameDB
                        , "Mst_Inventory." // strPrefixStd
                        , "mi." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_UserMapInventory" // strTableNameDB
                        , "Mst_UserMapInventory." // strPrefixStd
                        , "mumi." // strPrefixAlias
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
                , "zzB_Select_Mst_Inventory_zzE", zzB_Select_Mst_Inventory_zzE
                , "zzB_Select_Mst_UserMapInventory_zzE", zzB_Select_Mst_UserMapInventory_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_Inventory)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_Inventory";
            }
            if (bGet_Mst_UserMapInventory)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_UserMapInventory";
            }
            #endregion
        }
        #endregion

        #region // Mst_Supplier:
        private void Mst_Supplier_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objSupCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Supplier
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
						select top 1
							t.*
						from Mst_Supplier t --//[mylock]
						where (1=1)
							and t.SupCode = @objSupCode
						;
					");
			dtDB_Mst_Supplier = _cf.db.ExecQuery(
				strSqlExec
				, "@objSupCode", objSupCode
				).Tables[0];
			dtDB_Mst_Supplier.TableName = "Mst_Supplier";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Supplier.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.SupCode", objSupCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Supplier_CheckDB_SupCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Supplier.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.SupCode", objSupCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Supplier_CheckDB_SupCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Supplier.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
						"Check.SupCode", objSupCode
						, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
						, "DB.FlagActive", dtDB_Mst_Supplier.Rows[0]["FlagActive"]
						});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Supplier_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_Supplier_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Supplier
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Supplier_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Supplier", strRt_Cols_Mst_Supplier
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Supplier = (strRt_Cols_Mst_Supplier != null && strRt_Cols_Mst_Supplier.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_Mst_Supplier_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mspl.SupCode
						into #tbl_Mst_Supplier_Filter_Draft
						from Mst_Supplier mspl --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mspl.SupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Supplier_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Supplier_Filter:
						select
							t.*
						into #tbl_Mst_Supplier_Filter
						from #tbl_Mst_Supplier_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Supplier --------:
						zzB_Select_Mst_Supplier_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Supplier_Filter_Draft;
						--drop table #tbl_Mst_Supplier_Filter;
					"
				);
			////
			string zzB_Select_Mst_Supplier_zzE = "-- Nothing.";
			if (bGet_Mst_Supplier)
			{
				#region // bGet_Mst_Supplier:
				zzB_Select_Mst_Supplier_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Supplier:
                        select
                            t.MyIdxSeq
	                        , mspl.*
                        from #tbl_Mst_Supplier_Filter t --//[mylock]
	                        inner join Mst_Supplier mspl --//[mylock]
		                        on t.SupCode = mspl.SupCode
                        order by 
							t.MyIdxSeq asc
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
						, "Mst_Supplier" // strTableNameDB
						, "Mst_Supplier." // strPrefixStd
						, "mspl." // strPrefixAlias
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
				, "zzB_Select_Mst_Supplier_zzE", zzB_Select_Mst_Supplier_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Supplier)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Supplier";
			}
			#endregion
		}

		public DataSet Mst_Supplier_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Supplier
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Supplier_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Supplier_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Supplier", strRt_Cols_Mst_Supplier
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Supplier_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_Supplier_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_Supplier // strRt_Cols_Mst_Supplier
												  ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_Mst_Supplier_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Supplier objRQ_Mst_Supplier
			////
			, out RT_Mst_Supplier objRT_Mst_Supplier
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Supplier.Tid;
			objRT_Mst_Supplier = new RT_Mst_Supplier();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Supplier_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Supplier_Get;
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
				List<Mst_Supplier> lst_Mst_Supplier = new List<Mst_Supplier>();
				#endregion

				#region // WAS_Mst_Supplier_Get:
				mdsResult = Mst_Supplier_Get(
					objRQ_Mst_Supplier.Tid // strTid
					, objRQ_Mst_Supplier.GwUserCode // strGwUserCode
					, objRQ_Mst_Supplier.GwPassword // strGwPassword
					, objRQ_Mst_Supplier.WAUserCode // strUserCode
					, objRQ_Mst_Supplier.WAUserPassword // strUserPassword
					, objRQ_Mst_Supplier.AccessToken // strAccessToken
					, objRQ_Mst_Supplier.NetworkID // strNetworkID
					, objRQ_Mst_Supplier.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Supplier.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Supplier.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Supplier.Ft_WhereClause // strFt_WhereClause
														//// Return:
					, objRQ_Mst_Supplier.Rt_Cols_Mst_Supplier // strRt_Cols_Mst_Supplier
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Supplier.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_Supplier = mdsResult.Tables["Mst_Supplier"].Copy();
					lst_Mst_Supplier = TUtils.DataTableCmUtils.ToListof<Mst_Supplier>(dt_Mst_Supplier);
					objRT_Mst_Supplier.Lst_Mst_Supplier = lst_Mst_Supplier;
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
		public DataSet WAS_Mst_Supplier_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Supplier objRQ_Mst_Supplier
			////
			, out RT_Mst_Supplier objRT_Mst_Supplier
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Supplier.Tid;
			objRT_Mst_Supplier = new RT_Mst_Supplier();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Supplier.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Supplier_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Supplier_Create;
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
				List<Mst_Supplier> lst_Mst_Supplier = new List<Mst_Supplier>();
				#endregion

				#region // WS_Mst_Supplier_Create:
				mdsResult = Mst_Supplier_Create(
					objRQ_Mst_Supplier.Tid // strTid
					, objRQ_Mst_Supplier.GwUserCode // strGwUserCode
					, objRQ_Mst_Supplier.GwPassword // strGwPassword
					, objRQ_Mst_Supplier.WAUserCode // strUserCode
					, objRQ_Mst_Supplier.WAUserPassword // strUserPassword
					, objRQ_Mst_Supplier.AccessToken // strAccessToken
					, objRQ_Mst_Supplier.NetworkID // strNetworkID
					, objRQ_Mst_Supplier.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Supplier.Mst_Supplier.SupCode // objSupCode
					, objRQ_Mst_Supplier.Mst_Supplier.SupType // objSupType
					, objRQ_Mst_Supplier.Mst_Supplier.SupName // objSupName
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
		public DataSet Mst_Supplier_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objSupCode
			, object objSupType
			, object objSupName
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Supplier_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Supplier_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objSupCode", objSupCode
					, "objSupType", objSupType
					, "objSupName", objSupName
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Supplier_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_Supplier_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objSupCode // objSupCode
						, objSupType // objSupType
						, objSupName // objSupName        
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

		private void Mst_Supplier_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objSupCode
			, object objSupType
			, object objSupName
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Supplier_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objSupCode", objSupCode
				, "objSupType", objSupType
				, "objSupName", objSupName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strSupCode = TUtils.CUtils.StdParam(objSupCode);
			string strSupType = TUtils.CUtils.StdParam(objSupType);
			string strSupName = string.Format("{0}", objSupName).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_Supplier = null;

			{
				////
				if (strSupCode == null || strSupCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSupCode", strSupCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Supplier_Create_InvalidSupCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_Supplier_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strSupCode // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Supplier // dtDB_Mst_Supplier
					);
				////
				//if (strSupType.Length < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.strSupType", strSupType
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Supplier_Create_InvalidSupType
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				if (strSupName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSupName", strSupName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Supplier_Create_InvalidSupName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_Supplier:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Supplier.NewRow();
				strFN = "SupCode"; drDB[strFN] = strSupCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "SupType"; drDB[strFN] = strSupType;
				strFN = "SupName"; drDB[strFN] = strSupName;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Supplier.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Supplier" // strTableName
					, dtDB_Mst_Supplier // dtData
										//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_Mst_Supplier_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Supplier objRQ_Mst_Supplier
			////
			, out RT_Mst_Supplier objRT_Mst_Supplier
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Supplier.Tid;
			objRT_Mst_Supplier = new RT_Mst_Supplier();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Supplier.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Supplier_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Supplier_Update;
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
				List<Mst_Supplier> lst_Mst_Supplier = new List<Mst_Supplier>();
				#endregion

				#region // Mst_Supplier_Update:
				mdsResult = Mst_Supplier_Update(
					objRQ_Mst_Supplier.Tid // strTid
					, objRQ_Mst_Supplier.GwUserCode // strGwUserCode
					, objRQ_Mst_Supplier.GwPassword // strGwPassword
					, objRQ_Mst_Supplier.WAUserCode // strUserCode
					, objRQ_Mst_Supplier.WAUserPassword // strUserPassword
					, objRQ_Mst_Supplier.AccessToken // strAccessToken
					, objRQ_Mst_Supplier.NetworkID // strNetworkID
					, objRQ_Mst_Supplier.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Supplier.Mst_Supplier.SupCode // objSupCode
					, objRQ_Mst_Supplier.Mst_Supplier.SupType // objSupType
					, objRQ_Mst_Supplier.Mst_Supplier.SupName // objSupName
					, objRQ_Mst_Supplier.Mst_Supplier.FlagActive // objFlagActive
																 ////
					, objRQ_Mst_Supplier.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_Supplier_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objSupCode
			, object objSupType
			, object objSupName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Supplier_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Supplier_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objSupCode", objSupCode
					, "objSupType", objSupType
					, "objSupName", objSupName
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Supplier_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_Supplier_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objSupCode // objSupCode
						, objSupType // objSupType
						, objSupName // objSupName
						, objFlagActive // objFlagActive
										/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void Mst_Supplier_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objSupCode
			, object objSupType
			, object objSupName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Supplier_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSupCode", objSupCode
				, "objSupType", objSupType
				, "objSupName", objSupName
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
			string strSupCode = TUtils.CUtils.StdParam(objSupCode);
			string strSupType = TUtils.CUtils.StdParam(objSupType);
			string strSupName = string.Format("{0}", objSupName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_SupType = strFt_Cols_Upd.Contains("Mst_Supplier.SupType".ToUpper());
			bool bUpd_SupName = strFt_Cols_Upd.Contains("Mst_Supplier.SupName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Supplier.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_Supplier = null;
			{
				////
				Mst_Supplier_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strSupCode // objSupCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_Supplier // dtDB_Mst_Organ
					);
				////
				//if (strSupType.Length < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.strSupType", strSupType
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Supplier_Update_InvalidSupType
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				if (strSupName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSupName", strSupName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Supplier_Update_InvalidSupName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
		
			#endregion

			#region // Save Mst_Supplier:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Supplier.Rows[0];
				if (bUpd_SupType) { strFN = "SupType"; drDB[strFN] = strSupType; alColumnEffective.Add(strFN); }
				if (bUpd_SupName) { strFN = "SupName"; drDB[strFN] = strSupName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_Supplier"
					, dtDB_Mst_Supplier
					, alColumnEffective.ToArray()
					);
			}
			#endregion
			
		}
		public DataSet WAS_Mst_Supplier_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Supplier objRQ_Mst_Supplier
			////
			, out RT_Mst_Supplier objRT_Mst_Supplier
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Supplier.Tid;
			objRT_Mst_Supplier = new RT_Mst_Supplier();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Supplier.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Supplier_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Supplier_Delete;
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
				List<Mst_Supplier> lst_Mst_Supplier = new List<Mst_Supplier>();
				#endregion

				#region // Mst_Supplier_Delete:
				mdsResult = Mst_Supplier_Delete(
					objRQ_Mst_Supplier.Tid // strTid
					, objRQ_Mst_Supplier.GwUserCode // strGwUserCode
					, objRQ_Mst_Supplier.GwPassword // strGwPassword
					, objRQ_Mst_Supplier.WAUserCode // strUserCode
					, objRQ_Mst_Supplier.WAUserPassword // strUserPassword
					, objRQ_Mst_Supplier.AccessToken // strAccessToken
					, objRQ_Mst_Supplier.NetworkID // strNetworkID
					, objRQ_Mst_Supplier.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_Supplier.Mst_Supplier.SupCode // objSupCode
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
		public DataSet Mst_Supplier_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objSupCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Supplier_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Supplier_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objSupCode", objSupCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Supplier_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_Supplier_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objSupCode // objSupCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void Mst_Supplier_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objSupCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Supplier_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objSupCode", objSupCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strSupCode = TUtils.CUtils.StdParam(objSupCode);

			////
			DataTable dtDB_Mst_Supplier = null;
			{
				////
				Mst_Supplier_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strSupCode // strSupCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_Supplier // dtDB_Mst_Supplier
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_Supplier.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_Supplier"
					, dtDB_Mst_Supplier
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_MoveOrdType:
		private void Mst_MoveOrdType_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objMoveOrdType
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_MoveOrdType
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
						select top 1
							t.*
						from Mst_MoveOrdType t --//[mylock]
						where (1=1)
							and t.MoveOrdType = @objMoveOrdType
						;
					");
			dtDB_Mst_MoveOrdType = _cf.db.ExecQuery(
				strSqlExec
				, "@objMoveOrdType", objMoveOrdType
				).Tables[0];
			dtDB_Mst_MoveOrdType.TableName = "Mst_MoveOrdType";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_MoveOrdType.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.MoveOrdType", objMoveOrdType
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MoveOrdType_CheckDB_MoveOrdTypeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_MoveOrdType.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.MoveOrdType", objMoveOrdType
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MoveOrdType_CheckDB_MoveOrdTypeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_MoveOrdType.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
						"Check.MoveOrdType", objMoveOrdType
						, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
						, "DB.FlagActive", dtDB_Mst_MoveOrdType.Rows[0]["FlagActive"]
						});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_MoveOrdType_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_MoveOrdType_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_MoveOrdType
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_MoveOrdType_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_MoveOrdType", strRt_Cols_Mst_MoveOrdType
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_MoveOrdType = (strRt_Cols_Mst_MoveOrdType != null && strRt_Cols_Mst_MoveOrdType.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

			#endregion

			#region // Build Sql:
			////
			ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					});
			////		
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_MoveOrdType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mmot.MoveOrdType
						into #tbl_Mst_MoveOrdType_Filter_Draft
						from Mst_MoveOrdType mmot --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on mmot.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mmot.MoveOrdType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_MoveOrdType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_MoveOrdType_Filter:
						select
							t.*
						into #tbl_Mst_MoveOrdType_Filter
						from #tbl_Mst_MoveOrdType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_MoveOrdType --------:
						zzB_Select_Mst_MoveOrdType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_MoveOrdType_Filter_Draft;
						--drop table #tbl_Mst_MoveOrdType_Filter;
					"
				);
			////
			string zzB_Select_Mst_MoveOrdType_zzE = "-- Nothing.";
			if (bGet_Mst_MoveOrdType)
			{
				#region // bGet_Mst_MoveOrdType:
				zzB_Select_Mst_MoveOrdType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_MoveOrdType:
                        select
                            t.MyIdxSeq
	                        , mmot.*
                        from #tbl_Mst_MoveOrdType_Filter t --//[mylock]
	                        inner join Mst_MoveOrdType mmot --//[mylock]
		                        on t.MoveOrdType = mmot.MoveOrdType
                        order by 
							t.MyIdxSeq asc
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
						, "Mst_MoveOrdType" // strTableNameDB
						, "Mst_MoveOrdType." // strPrefixStd
						, "mmot." // strPrefixAlias
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
				, "zzB_Select_Mst_MoveOrdType_zzE", zzB_Select_Mst_MoveOrdType_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_MoveOrdType)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_MoveOrdType";
			}
			#endregion
		}

		public DataSet Mst_MoveOrdType_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_MoveOrdType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_MoveOrdType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_MoveOrdType_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_MoveOrdType", strRt_Cols_Mst_MoveOrdType
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_MoveOrdType_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_MoveOrdType_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_MoveOrdType // strRt_Cols_Mst_MoveOrdType
													 ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_Mst_MoveOrdType_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_MoveOrdType objRQ_Mst_MoveOrdType
			////
			, out RT_Mst_MoveOrdType objRT_Mst_MoveOrdType
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_MoveOrdType.Tid;
			objRT_Mst_MoveOrdType = new RT_Mst_MoveOrdType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_MoveOrdType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_MoveOrdType_Get;
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
				List<Mst_MoveOrdType> lst_Mst_MoveOrdType = new List<Mst_MoveOrdType>();
				#endregion

				#region // WAS_Mst_MoveOrdType_Get:
				mdsResult = Mst_MoveOrdType_Get(
					objRQ_Mst_MoveOrdType.Tid // strTid
					, objRQ_Mst_MoveOrdType.GwUserCode // strGwUserCode
					, objRQ_Mst_MoveOrdType.GwPassword // strGwPassword
					, objRQ_Mst_MoveOrdType.WAUserCode // strUserCode
					, objRQ_Mst_MoveOrdType.WAUserPassword // strUserPassword
					, objRQ_Mst_MoveOrdType.AccessToken // strAccessToken
					, objRQ_Mst_MoveOrdType.NetworkID // strNetworkID
					, objRQ_Mst_MoveOrdType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_MoveOrdType.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_MoveOrdType.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_MoveOrdType.Ft_WhereClause // strFt_WhereClause
														   //// Return:
					, objRQ_Mst_MoveOrdType.Rt_Cols_Mst_MoveOrdType // strRt_Cols_Mst_MoveOrdType
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_MoveOrdType.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_MoveOrdType = mdsResult.Tables["Mst_MoveOrdType"].Copy();
					lst_Mst_MoveOrdType = TUtils.DataTableCmUtils.ToListof<Mst_MoveOrdType>(dt_Mst_MoveOrdType);
					objRT_Mst_MoveOrdType.Lst_Mst_MoveOrdType = lst_Mst_MoveOrdType;
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
		#endregion

		#region // Mst_Agent:
		private void Mst_Agent_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objAgentCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Agent
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Agent t --//[mylock]
					where (1=1)
						and t.AgentCode = @objAgentCode
					;
				");
			dtDB_Mst_Agent = _cf.db.ExecQuery(
				strSqlExec
				, "@objAgentCode", objAgentCode
				).Tables[0];
			dtDB_Mst_Agent.TableName = "Mst_Agent";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Agent.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.AgentCode", objAgentCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Agent_CheckDB_AgentCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Agent.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.AgentCode", objAgentCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Agent_CheckDB_AgentCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Agent.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.AgentCode", objAgentCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Agent.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Agent_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_Agent_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Agent
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Agent_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_Mst_Agent", strRt_Cols_Mst_Agent
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Agent = (strRt_Cols_Mst_Agent != null && strRt_Cols_Mst_Agent.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_Mst_Agent_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mag.AgentCode
						into #tbl_Mst_Agent_Filter_Draft
						from Mst_Agent mag --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mag.AgentCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Agent_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Agent_Filter:
						select
							t.*
						into #tbl_Mst_Agent_Filter
						from #tbl_Mst_Agent_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Agent --------:
						zzB_Select_Mst_Agent_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Agent_Filter_Draft;
						--drop table #tbl_Mst_Agent_Filter;
					"
				);
			////
			string zzB_Select_Mst_Agent_zzE = "-- Nothing.";
			if (bGet_Mst_Agent)
			{
				#region // bGet_Mst_Agent:
				zzB_Select_Mst_Agent_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Agent:
                        select
                            t.MyIdxSeq
	                        , mag.*
							, md.DistrictCode md_DistrictCode
							, md.DistrictName md_DistrictName
							, mp.ProvinceCode mp_ProvinceCode
							, mp.ProvinceName mp_ProvinceName
                        from #tbl_Mst_Agent_Filter t --//[mylock]
	                        inner join Mst_Agent mag --//[mylock]
		                        on t.AgentCode = mag.AgentCode
	                        inner join Mst_District md --//[mylock]
		                        on mag.DistrictCode = md.DistrictCode
									and mag.ProvinceCode = md.ProvinceCode
	                        inner join Mst_Province mp --//[mylock]
		                        on md.ProvinceCode = mp.ProvinceCode
                        order by 
							t.MyIdxSeq asc
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
						, "Mst_Agent" // strTableNameDB
						, "Mst_Agent." // strPrefixStd
						, "mag." // strPrefixAlias
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
				, "zzB_Select_Mst_Agent_zzE", zzB_Select_Mst_Agent_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Agent)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Agent";
			}
			#endregion
		}

		public DataSet Mst_Agent_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Agent
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Agent_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Agent_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Agent", strRt_Cols_Mst_Agent
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Agent_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_Agent_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_Agent // strRt_Cols_Mst_Agent
											   ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_Mst_Agent_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Agent objRQ_Mst_Agent
			////
			, out RT_Mst_Agent objRT_Mst_Agent
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Agent.Tid;
			objRT_Mst_Agent = new RT_Mst_Agent();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Agent_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Agent_Get;
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
				List<Mst_Agent> lst_Mst_Agent = new List<Mst_Agent>();
				#endregion

				#region // WAS_Mst_Agent_Get:
				mdsResult = Mst_Agent_Get(
					objRQ_Mst_Agent.Tid // strTid
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					, objRQ_Mst_Agent.WAUserCode // strUserCode
					, objRQ_Mst_Agent.WAUserPassword // strUserPassword
					, objRQ_Mst_Agent.AccessToken // strAccessToken
					, objRQ_Mst_Agent.NetworkID // strNetworkID
					, objRQ_Mst_Agent.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					//// Filter:
					, objRQ_Mst_Agent.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Agent.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Agent.Ft_WhereClause // strFt_WhereClause
					//// Return:
					, objRQ_Mst_Agent.Rt_Cols_Mst_Agent // strRt_Cols_Mst_Agent
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Agent.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_Agent = mdsResult.Tables["Mst_Agent"].Copy();
					lst_Mst_Agent = TUtils.DataTableCmUtils.ToListof<Mst_Agent>(dt_Mst_Agent);
					objRT_Mst_Agent.Lst_Mst_Agent = lst_Mst_Agent;
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
		public DataSet WAS_Mst_Agent_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Agent objRQ_Mst_Agent
			////
			, out RT_Mst_Agent objRT_Mst_Agent
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Agent.Tid;
			objRT_Mst_Agent = new RT_Mst_Agent();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Agent_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Agent_Create;
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
				List<Mst_Agent> lst_Mst_Agent = new List<Mst_Agent>();
				#endregion

				#region // Mst_Agent_Create:
				mdsResult = Mst_Agent_Create(
					objRQ_Mst_Agent.Tid // strTid
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					, objRQ_Mst_Agent.WAUserCode // strUserCode
					, objRQ_Mst_Agent.WAUserPassword // strUserPassword
					, objRQ_Mst_Agent.AccessToken // strAccessToken
					, objRQ_Mst_Agent.NetworkID // strNetworkID
					, objRQ_Mst_Agent.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_Agent.Mst_Agent.AgentCode // objAgentCode
					, objRQ_Mst_Agent.Mst_Agent.ProvinceCode // objProvinceCode
					, objRQ_Mst_Agent.Mst_Agent.DistrictCode // objDistrictCode
					, objRQ_Mst_Agent.Mst_Agent.AgentName // objAgentName
					, objRQ_Mst_Agent.Mst_Agent.AgentAddress // objAgentAddress
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
		public DataSet Mst_Agent_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objAgentCode
			, object objProvinceCode
			, object objDistrictCode
			, object objAgentName
			, object objAgentAddress
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Agent_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Agent_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objAgentCode", objAgentCode
					, "objProvinceCode", objProvinceCode
					, "objDistrictCode", objDistrictCode
					, "objAgentName", objAgentName
					, "objAgentAddress", objAgentAddress
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Agent_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_Agent_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objAgentCode // objAgentCode
						, objProvinceCode // objProvinceCode
						, objDistrictCode // objDistrictCode
						, objAgentName // objAgentName
						, objAgentAddress // objAgentAddress
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
		private void Mst_Agent_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objAgentCode
			, object objProvinceCode
			, object objDistrictCode
			, object objAgentName
			, object objAgentAddress
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Agent_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objAgentCode", objAgentCode
				, "objProvinceCode", objProvinceCode
				, "objDistrictCode", objDistrictCode
				, "objAgentName", objAgentName
				, "objAgentAddress", objAgentAddress
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);
			string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
			string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
			string strAgentName = string.Format("{0}", objAgentName).Trim();
			string strAgentAddress = string.Format("{0}", objAgentAddress).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_Agent = null;

			{
				////
				if (strAgentCode == null || strAgentCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strAgentCode", strAgentCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Agent_Create_InvalidAgentCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_Agent_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strAgentCode // objSupCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Agent // dtDB_Mst_Agent
					);
				////
				if (strAgentName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strAgentName", strAgentName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Agent_Create_InvalidAgentName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_Agent:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Agent.NewRow();
				strFN = "AgentCode"; drDB[strFN] = strAgentCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
				strFN = "DistrictCode"; drDB[strFN] = strDistrictCode;
				strFN = "AgentName"; drDB[strFN] = strAgentName;
				strFN = "AgentAddress"; drDB[strFN] = strAgentAddress;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Agent.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Agent" // strTableName
					, dtDB_Mst_Agent // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_Mst_Agent_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Agent objRQ_Mst_Agent
			////
			, out RT_Mst_Agent objRT_Mst_Agent
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Agent.Tid;
			objRT_Mst_Agent = new RT_Mst_Agent();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Agent_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Agent_Update;
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
				List<Mst_Agent> lst_Mst_Agent = new List<Mst_Agent>();
				#endregion

				#region // Mst_Agent_Update:
				mdsResult = Mst_Agent_Update(
					objRQ_Mst_Agent.Tid // strTid
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					, objRQ_Mst_Agent.WAUserCode // strUserCode
					, objRQ_Mst_Agent.WAUserPassword // strUserPassword
					, objRQ_Mst_Agent.AccessToken // strAccessToken
					, objRQ_Mst_Agent.NetworkID // strNetworkID
					, objRQ_Mst_Agent.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_Agent.Mst_Agent.AgentCode // objAgentCode
					, objRQ_Mst_Agent.Mst_Agent.AgentName // objAgentName
					, objRQ_Mst_Agent.Mst_Agent.AgentAddress // AgentAddress
					, objRQ_Mst_Agent.Mst_Agent.FlagActive // objFlagActive
					////
					, objRQ_Mst_Agent.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_Agent_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objAgentCode
			, object objAgentName
			, object objAgentAddress
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Agent_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Agent_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objAgentCode", objAgentCode
					, "objAgentName", objAgentName
					, "objAgentAddress", objAgentAddress
					, "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Agent_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_Agent_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objAgentCode // objAgentCode
						, objAgentName // objAgentName
						, objAgentAddress // objAgentAddress
						, objFlagActive // objFlagActive
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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
		private void Mst_Agent_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objAgentCode
			, object objAgentName
			, object objAgentAddress
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Agent_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objAgentCode", objAgentCode
				, "objAgentName", objAgentName
				, "objAgentAddress", objAgentAddress
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
			string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);
			string strAgentName = string.Format("{0}", objAgentName).Trim();
			string strAgentAddress = string.Format("{0}", objAgentAddress).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_AgentName = strFt_Cols_Upd.Contains("Mst_Agent.AgentName".ToUpper());
			bool bUpd_AgentAddress = strFt_Cols_Upd.Contains("Mst_Agent.AgentAddress".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Agent.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_Agent = null;
			{
				////
				Mst_Agent_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strAgentCode // objAgentCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_Agent // dtDB_Mst_Agent
					);
				////
				if (strAgentName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strAgentName", strAgentName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Agent_Update_InvalidAgentName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_Agent:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Agent.Rows[0];
				if (bUpd_AgentName) { strFN = "AgentName"; drDB[strFN] = strAgentName; alColumnEffective.Add(strFN); }
				if (bUpd_AgentAddress) { strFN = "AgentAddress"; drDB[strFN] = strAgentAddress; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_Agent"
					, dtDB_Mst_Agent
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}
		public DataSet WAS_Mst_Agent_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Agent objRQ_Mst_Agent
			////
			, out RT_Mst_Agent objRT_Mst_Agent
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Agent.Tid;
			objRT_Mst_Agent = new RT_Mst_Agent();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Agent.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Agent_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Agent_Delete;
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
				List<Mst_Agent> lst_Mst_Agent = new List<Mst_Agent>();
				#endregion

				#region // Mst_Agent_Delete:
				mdsResult = Mst_Agent_Delete(
					objRQ_Mst_Agent.Tid // strTid
					, objRQ_Mst_Agent.GwUserCode // strGwUserCode
					, objRQ_Mst_Agent.GwPassword // strGwPassword
					, objRQ_Mst_Agent.WAUserCode // strUserCode
					, objRQ_Mst_Agent.WAUserPassword // strUserPassword
					, objRQ_Mst_Agent.AccessToken // strAccessToken
					, objRQ_Mst_Agent.NetworkID // strNetworkID
					, objRQ_Mst_Agent.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Agent.Mst_Agent.AgentCode // objAgentCode
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
		public DataSet Mst_Agent_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objAgentCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Agent_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Agent_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objAgentCode", objAgentCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Agent_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_Agent_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objAgentCode // objAgentCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		private void Mst_Agent_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objAgentCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Agent_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objAgentCode", objAgentCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);

			////
			DataTable dtDB_Mst_Agent = null;
			{
				////
				Mst_Agent_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strAgentCode // strAgentCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_Agent // dtDB_Mst_Agent
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_Agent.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_Agent"
					, dtDB_Mst_Agent
					);
			}
			#endregion
		}
		#endregion

		#region // View_ColumnView:
		private void View_ColumnView_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objColumnViewCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_View_ColumnView
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
						select top 1
							t.*
						from View_ColumnView t --//[mylock]
						where (1=1)
							and t.ColumnViewCode = @objColumnViewCode
						;
					");
			dtDB_View_ColumnView = _cf.db.ExecQuery(
				strSqlExec
				, "@objColumnViewCode", objColumnViewCode
				).Tables[0];
			dtDB_View_ColumnView.TableName = "View_ColumnView";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_View_ColumnView.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.ColumnViewCode", objColumnViewCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnView_CheckDB_ColumnViewCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_View_ColumnView.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.ColumnViewCode", objColumnViewCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnView_CheckDB_ColumnViewCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_View_ColumnView.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
						"Check.ColumnViewCode", objColumnViewCode
						, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
						, "DB.FlagActive", dtDB_View_ColumnView.Rows[0]["FlagActive"]
						});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.View_ColumnView_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void View_ColumnView_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_ColumnView
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "View_ColumnView_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_View_ColumnView", strRt_Cols_View_ColumnView
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_View_ColumnView = (strRt_Cols_View_ColumnView != null && strRt_Cols_View_ColumnView.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_View_ColumnView_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, vclv.ColumnViewCode
						into #tbl_View_ColumnView_Filter_Draft
						from View_ColumnView vclv --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							vclv.ColumnViewCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_View_ColumnView_Filter_Draft t --//[mylock]
						;

						---- #tbl_View_ColumnView_Filter:
						select
							t.*
						into #tbl_View_ColumnView_Filter
						from #tbl_View_ColumnView_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- View_ColumnView --------:
						zzB_Select_View_ColumnView_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_View_ColumnView_Filter_Draft;
						--drop table #tbl_View_ColumnView_Filter;
					"
				);
			////
			string zzB_Select_View_ColumnView_zzE = "-- Nothing.";
			if (bGet_View_ColumnView)
			{
				#region // bGet_View_ColumnView:
				zzB_Select_View_ColumnView_zzE = CmUtils.StringUtils.Replace(@"
                        ---- View_ColumnView:
                        select
                            t.MyIdxSeq
	                        , vclv.*
                        from #tbl_View_ColumnView_Filter t --//[mylock]
	                        inner join View_ColumnView vclv --//[mylock]
		                        on t.ColumnViewCode = vclv.ColumnViewCode
                        order by 
							t.MyIdxSeq asc
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
						, "View_ColumnView" // strTableNameDB
						, "View_ColumnView." // strPrefixStd
						, "vclv." // strPrefixAlias
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
				, "zzB_Select_View_ColumnView_zzE", zzB_Select_View_ColumnView_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_View_ColumnView)
			{
				dsGetData.Tables[nIdxTable++].TableName = "View_ColumnView";
			}
			#endregion
		}

		public DataSet View_ColumnView_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_ColumnView
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "View_ColumnView_Get";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnView_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_View_ColumnView", strRt_Cols_View_ColumnView
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnView_GetX:
				DataSet dsGetData = null;
				{
					////
					View_ColumnView_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_View_ColumnView // strRt_Cols_View_ColumnView
													 ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_View_ColumnView_Get(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnView objRQ_View_ColumnView
			////
			, out RT_View_ColumnView objRT_View_ColumnView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnView.Tid;
			objRT_View_ColumnView = new RT_View_ColumnView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnView_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnView_Get;
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
				List<View_ColumnView> lst_View_ColumnView = new List<View_ColumnView>();
				#endregion

				#region // WAS_View_ColumnView_Get:
				mdsResult = View_ColumnView_Get(
					objRQ_View_ColumnView.Tid // strTid
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					, objRQ_View_ColumnView.WAUserCode // strUserCode
					, objRQ_View_ColumnView.WAUserPassword // strUserPassword
					, objRQ_View_ColumnView.AccessToken // strAccessToken
					, objRQ_View_ColumnView.NetworkID // strNetworkID
					, objRQ_View_ColumnView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_View_ColumnView.Ft_RecordStart // strFt_RecordStart
					, objRQ_View_ColumnView.Ft_RecordCount // strFt_RecordCount
					, objRQ_View_ColumnView.Ft_WhereClause // strFt_WhereClause
														   //// Return:
					, objRQ_View_ColumnView.Rt_Cols_View_ColumnView // strRt_Cols_View_ColumnView
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_View_ColumnView.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_View_ColumnView = mdsResult.Tables["View_ColumnView"].Copy();
					lst_View_ColumnView = TUtils.DataTableCmUtils.ToListof<View_ColumnView>(dt_View_ColumnView);
					objRT_View_ColumnView.Lst_View_ColumnView = lst_View_ColumnView;
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

		public DataSet WAS_View_ColumnView_Create(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnView objRQ_View_ColumnView
			////
			, out RT_View_ColumnView objRT_View_ColumnView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnView.Tid;
			objRT_View_ColumnView = new RT_View_ColumnView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnView_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnView_Create;
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
				List<View_ColumnView> lst_View_ColumnView = new List<View_ColumnView>();
				#endregion

				#region // WS_View_ColumnView_Create:
				mdsResult = View_ColumnView_Create(
					objRQ_View_ColumnView.Tid // strTid
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					, objRQ_View_ColumnView.WAUserCode // strUserCode
					, objRQ_View_ColumnView.WAUserPassword // strUserPassword
					, objRQ_View_ColumnView.AccessToken // strAccessToken
					, objRQ_View_ColumnView.NetworkID // strNetworkID
					, objRQ_View_ColumnView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_View_ColumnView.View_ColumnView.ColumnViewCode // objColumnViewCode
					, objRQ_View_ColumnView.View_ColumnView.ColumnViewName // objColumnViewName
					, objRQ_View_ColumnView.View_ColumnView.Remark // objRemark
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

		public DataSet View_ColumnView_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objColumnViewCode
			, object objColumnViewName
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnView_Create";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnView_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnViewCode", objColumnViewCode
					, "objColumnViewName", objColumnViewName
					, "objRemark", objRemark
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnView_CreateX:
				//DataSet dsGetData = null;
				{
					View_ColumnView_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objColumnViewCode // objColumnViewCode
						, objColumnViewName // objColumnViewName
						, objRemark // objRemark        
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

		private void View_ColumnView_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objColumnViewCode
			, object objColumnViewName
			, object objRemark
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnView_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objColumnViewCode", objColumnViewCode
				, "objColumnViewName", objColumnViewName
				, "objRemark", objRemark
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strColumnViewCode = TUtils.CUtils.StdParam(objColumnViewCode);
			string strColumnViewName = string.Format("{0}", objColumnViewName).Trim();
			string strRemark = string.Format("{0}", objRemark).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_View_ColumnView = null;

			{
				////
				if (strColumnViewCode == null || strColumnViewCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strColumnViewCode", strColumnViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnView_Create_InvalidColumnViewCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				View_ColumnView_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strColumnViewCode // objColumnViewCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_View_ColumnView // dtDB_View_ColumnView
					);
				////
				if (strColumnViewName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strColumnViewName", strColumnViewName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnView_Create_InvalidColumnViewName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB View_ColumnView:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_ColumnView.NewRow();
				strFN = "ColumnViewCode"; drDB[strFN] = strColumnViewCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "ColumnViewName"; drDB[strFN] = strColumnViewName;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active; ;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_View_ColumnView.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"View_ColumnView" // strTableName
					, dtDB_View_ColumnView // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_View_ColumnView_Update(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnView objRQ_View_ColumnView
			////
			, out RT_View_ColumnView objRT_View_ColumnView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnView.Tid;
			objRT_View_ColumnView = new RT_View_ColumnView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnView_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnView_Update;
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
				List<View_ColumnView> lst_View_ColumnView = new List<View_ColumnView>();
				#endregion

				#region // View_ColumnView_Update:
				mdsResult = View_ColumnView_Update(
					objRQ_View_ColumnView.Tid // strTid
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					, objRQ_View_ColumnView.WAUserCode // strUserCode
					, objRQ_View_ColumnView.WAUserPassword // strUserPassword
					, objRQ_View_ColumnView.AccessToken // strAccessToken
					, objRQ_View_ColumnView.NetworkID // strNetworkID
					, objRQ_View_ColumnView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_ColumnView.View_ColumnView.ColumnViewCode // objColumnViewCode
					, objRQ_View_ColumnView.View_ColumnView.ColumnViewName // objColumnViewName
					, objRQ_View_ColumnView.View_ColumnView.FlagActive // objFlagActive
					, objRQ_View_ColumnView.View_ColumnView.Remark // objRemark
					////
					, objRQ_View_ColumnView.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet View_ColumnView_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objColumnViewCode
			, object objColumnViewName
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnView_Update";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnView_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnViewCode", objColumnViewCode
					, "objColumnViewName", objColumnViewName
					, "objFlagActive", objFlagActive
					, "objRemark", objRemark
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnView_UpdateX:
				//DataSet dsGetData = null;
				{
					View_ColumnView_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objColumnViewCode // objColumnViewCode
						, objColumnViewName // objColumnViewName
						, objFlagActive // objFlagActive
						, objRemark // objRemark
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void View_ColumnView_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objColumnViewCode
			, object objColumnViewName
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnView_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.View_ColumnView_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objColumnViewCode", objColumnViewCode
					, "objColumnViewName", objColumnViewName
					, "objFlagActive", objFlagActive
					, "objRemark", objRemark
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
					});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strColumnViewCode = TUtils.CUtils.StdParam(objColumnViewCode);
			string strColumnViewName = string.Format("{0}", objColumnViewName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			bool bUpd_ColumnViewName = strFt_Cols_Upd.Contains("View_ColumnView.ColumnViewName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("View_ColumnView.FlagActive".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("View_ColumnView.Remark".ToUpper());

			////
			DataTable dtDB_View_ColumnView = null;
			{
				////
				View_ColumnView_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strColumnViewCode // objColumnViewCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_ColumnView // dtDB_Mst_Organ
					);
				////
				if (strColumnViewName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strColumnViewName", strColumnViewName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnView_Update_InvalidColumnViewName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save View_ColumnView:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_ColumnView.Rows[0];
				if (bUpd_ColumnViewName) { strFN = "ColumnViewName"; drDB[strFN] = strColumnViewName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"View_ColumnView"
					, dtDB_View_ColumnView
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_View_ColumnView_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnView objRQ_View_ColumnView
			////
			, out RT_View_ColumnView objRT_View_ColumnView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnView.Tid;
			objRT_View_ColumnView = new RT_View_ColumnView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnView_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnView_Delete;
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
				List<View_ColumnView> lst_View_ColumnView = new List<View_ColumnView>();
				#endregion

				#region // View_ColumnView_Delete:
				mdsResult = View_ColumnView_Delete(
					objRQ_View_ColumnView.Tid // strTid
					, objRQ_View_ColumnView.GwUserCode // strGwUserCode
					, objRQ_View_ColumnView.GwPassword // strGwPassword
					, objRQ_View_ColumnView.WAUserCode // strUserCode
					, objRQ_View_ColumnView.WAUserPassword // strUserPassword
					, objRQ_View_ColumnView.AccessToken // strAccessToken
					, objRQ_View_ColumnView.NetworkID // strNetworkID
					, objRQ_View_ColumnView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_ColumnView.View_ColumnView.ColumnViewCode // objColumnViewCode
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
		public DataSet View_ColumnView_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objColumnViewCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnView_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnView_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnViewCode", objColumnViewCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnView_DeleteX:
				//DataSet dsGetData = null;
				{
					View_ColumnView_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objColumnViewCode // objColumnViewCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void View_ColumnView_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objColumnViewCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnView_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objColumnViewCode", objColumnViewCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strColumnViewCode = TUtils.CUtils.StdParam(objColumnViewCode);

			////
			DataTable dtDB_View_ColumnView = null;
			{
				////
				View_ColumnView_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strColumnViewCode // strColumnViewCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_ColumnView // dtDB_View_ColumnView
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_View_ColumnView.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"View_ColumnView"
					, dtDB_View_ColumnView
					);
			}
			#endregion
		}
		#endregion

		#region // View_GroupView:
		private void View_GroupView_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objGroupViewCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_View_GroupView
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
						select top 1
							t.*
						from View_GroupView t --//[mylock]
						where (1=1)
							and t.GroupViewCode = @objGroupViewCode
						;
					");
			dtDB_View_GroupView = _cf.db.ExecQuery(
				strSqlExec
				, "@objGroupViewCode", objGroupViewCode
				).Tables[0];
			dtDB_View_GroupView.TableName = "View_GroupView";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_View_GroupView.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.GroupViewCode", objGroupViewCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_GroupView_CheckDB_GroupViewCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_View_GroupView.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
							"Check.GroupViewCode", objGroupViewCode
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_GroupView_CheckDB_GroupViewCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_View_GroupView.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
						"Check.GroupViewCode", objGroupViewCode
						, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
						, "DB.FlagActive", dtDB_View_GroupView.Rows[0]["FlagActive"]
						});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.View_GroupView_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void View_GroupView_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_GroupView
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "View_GroupView_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_View_GroupView", strRt_Cols_View_GroupView
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_View_GroupView = (strRt_Cols_View_GroupView != null && strRt_Cols_View_GroupView.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_View_GroupView_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, vgrv.GroupViewCode
						into #tbl_View_GroupView_Filter_Draft
						from View_GroupView vgrv --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							vgrv.GroupViewCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_View_GroupView_Filter_Draft t --//[mylock]
						;

						---- #tbl_View_GroupView_Filter:
						select
							t.*
						into #tbl_View_GroupView_Filter
						from #tbl_View_GroupView_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- View_GroupView --------:
						zzB_Select_View_GroupView_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_View_GroupView_Filter_Draft;
						--drop table #tbl_View_GroupView_Filter;
					"
				);
			////
			string zzB_Select_View_GroupView_zzE = "-- Nothing.";
			if (bGet_View_GroupView)
			{
				#region // bGet_View_GroupView:
				zzB_Select_View_GroupView_zzE = CmUtils.StringUtils.Replace(@"
                        ---- View_GroupView:
                        select
                            t.MyIdxSeq
	                        , vgrv.*
                        from #tbl_View_GroupView_Filter t --//[mylock]
	                        inner join View_GroupView vgrv --//[mylock]
		                        on t.GroupViewCode = vgrv.GroupViewCode
                        order by 
							t.MyIdxSeq asc
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
						, "View_GroupView" // strTableNameDB
						, "View_GroupView." // strPrefixStd
						, "vgrv." // strPrefixAlias
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
				, "zzB_Select_View_GroupView_zzE", zzB_Select_View_GroupView_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_View_GroupView)
			{
				dsGetData.Tables[nIdxTable++].TableName = "View_GroupView";
			}
			#endregion
		}

		public DataSet View_GroupView_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_GroupView
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "View_GroupView_Get";
			string strErrorCodeDefault = TError.ErridnInventory.View_GroupView_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_View_GroupView", strRt_Cols_View_GroupView
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_GroupView_GetX:
				DataSet dsGetData = null;
				{
					////
					View_GroupView_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_View_GroupView // strRt_Cols_View_GroupView
													////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_View_GroupView_Get(
			ref ArrayList alParamsCoupleError
			, RQ_View_GroupView objRQ_View_GroupView
			////
			, out RT_View_GroupView objRT_View_GroupView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_GroupView.Tid;
			objRT_View_GroupView = new RT_View_GroupView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_GroupView_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_GroupView_Get;
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
				List<View_GroupView> lst_View_GroupView = new List<View_GroupView>();
				#endregion

				#region // WAS_View_GroupView_Get:
				mdsResult = View_GroupView_Get(
					objRQ_View_GroupView.Tid // strTid
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					, objRQ_View_GroupView.WAUserCode // strUserCode
					, objRQ_View_GroupView.WAUserPassword // strUserPassword
					, objRQ_View_GroupView.AccessToken // strAccessToken
					, objRQ_View_GroupView.NetworkID // strNetworkID
					, objRQ_View_GroupView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_View_GroupView.Ft_RecordStart // strFt_RecordStart
					, objRQ_View_GroupView.Ft_RecordCount // strFt_RecordCount
					, objRQ_View_GroupView.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_View_GroupView.Rt_Cols_View_GroupView // strRt_Cols_View_GroupView
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_View_GroupView.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_View_GroupView = mdsResult.Tables["View_GroupView"].Copy();
					lst_View_GroupView = TUtils.DataTableCmUtils.ToListof<View_GroupView>(dt_View_GroupView);
					objRT_View_GroupView.Lst_View_GroupView = lst_View_GroupView;
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

		public DataSet WAS_View_GroupView_Create(
			ref ArrayList alParamsCoupleError
			, RQ_View_GroupView objRQ_View_GroupView
			////
			, out RT_View_GroupView objRT_View_GroupView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_GroupView.Tid;
			objRT_View_GroupView = new RT_View_GroupView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_GroupView_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_GroupView_Create;
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
				List<View_GroupView> lst_View_GroupView = new List<View_GroupView>();
				#endregion

				#region // WS_View_GroupView_Create:
				mdsResult = View_GroupView_Create(
					objRQ_View_GroupView.Tid // strTid
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					, objRQ_View_GroupView.WAUserCode // strUserCode
					, objRQ_View_GroupView.WAUserPassword // strUserPassword
					, objRQ_View_GroupView.AccessToken // strAccessToken
					, objRQ_View_GroupView.NetworkID // strNetworkID
					, objRQ_View_GroupView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_View_GroupView.View_GroupView.GroupViewCode // objGroupViewCode
					, objRQ_View_GroupView.View_GroupView.GroupViewName // objGroupViewName
					, objRQ_View_GroupView.View_GroupView.Remark // objRemark
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

		public DataSet View_GroupView_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objGroupViewCode
			, object objGroupViewName
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_GroupView_Create";
			string strErrorCodeDefault = TError.ErridnInventory.View_GroupView_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
					, "objGroupViewName", objGroupViewName
					, "objRemark", objRemark
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_GroupView_CreateX:
				//DataSet dsGetData = null;
				{
					View_GroupView_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						, objGroupViewName // objGroupViewName
						, objRemark // objRemark        
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

		private void View_GroupView_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			, object objGroupViewName
			, object objRemark
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_GroupView_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objGroupViewCode", objGroupViewCode
					, "objGroupViewName", objGroupViewName
					, "objRemark", objRemark
					});
			#endregion

			#region // Refine and Check Input:
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);
			string strGroupViewName = string.Format("{0}", objGroupViewName).Trim();
			string strRemark = string.Format("{0}", objRemark).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_View_GroupView = null;

			{
				////
				if (strGroupViewCode == null || strGroupViewCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strGroupViewCode", strGroupViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_GroupView_Create_InvalidGroupViewCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				View_GroupView_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strGroupViewCode // objGroupViewCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_View_GroupView // dtDB_View_GroupView
					);
				////
				if (strGroupViewName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strGroupViewName", strGroupViewName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_GroupView_Create_InvalidGroupViewName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB View_GroupView:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_GroupView.NewRow();
				strFN = "GroupViewCode"; drDB[strFN] = strGroupViewCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "GroupViewName"; drDB[strFN] = strGroupViewName;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_View_GroupView.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"View_GroupView" // strTableName
					, dtDB_View_GroupView // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}

		public DataSet WAS_View_GroupView_Update(
			ref ArrayList alParamsCoupleError
			, RQ_View_GroupView objRQ_View_GroupView
			////
			, out RT_View_GroupView objRT_View_GroupView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_GroupView.Tid;
			objRT_View_GroupView = new RT_View_GroupView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_GroupView_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_GroupView_Update;
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
				List<View_GroupView> lst_View_GroupView = new List<View_GroupView>();
				#endregion

				#region // View_GroupView_Update:
				mdsResult = View_GroupView_Update(
					objRQ_View_GroupView.Tid // strTid
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					, objRQ_View_GroupView.WAUserCode // strUserCode
					, objRQ_View_GroupView.WAUserPassword // strUserPassword
					, objRQ_View_GroupView.AccessToken // strAccessToken
					, objRQ_View_GroupView.NetworkID // strNetworkID
					, objRQ_View_GroupView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_GroupView.View_GroupView.GroupViewCode // objGroupViewCode
					, objRQ_View_GroupView.View_GroupView.GroupViewName // objGroupViewName
					, objRQ_View_GroupView.View_GroupView.FlagActive // objFlagActive
					, objRQ_View_GroupView.View_GroupView.Remark // objRemark
					////
					, objRQ_View_GroupView.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet View_GroupView_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objGroupViewCode
			, object objGroupViewName
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_GroupView_Update";
			string strErrorCodeDefault = TError.ErridnInventory.View_GroupView_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
					, "objGroupViewName", objGroupViewName
					, "objFlagActive", objFlagActive
					, "objRemark", objRemark
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_GroupView_UpdateX:
				//DataSet dsGetData = null;
				{
					View_GroupView_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						, objGroupViewName // objGroupViewName
						, objFlagActive // objFlagActive
						, objRemark // objRemark
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void View_GroupView_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			, object objGroupViewName
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_GroupView_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.View_GroupView_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupViewCode", objGroupViewCode
				, "objGroupViewName", objGroupViewName
				, "objFlagActive", objFlagActive
				, "objRemark", objRemark
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);
			string strGroupViewName = string.Format("{0}", objGroupViewName).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			bool bUpd_GroupViewName = strFt_Cols_Upd.Contains("View_GroupView.GroupViewName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("View_GroupView.FlagActive".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("View_GroupView.Remark".ToUpper());

			////
			DataTable dtDB_View_GroupView = null;
			{
				////
				View_GroupView_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strGroupViewCode // objGroupViewCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_GroupView // dtDB_Mst_Organ
					);
				////
				if (strGroupViewName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strGroupViewName", strGroupViewName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_GroupView_Update_InvalidGroupViewName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			#endregion

			#region // Save View_GroupView:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_GroupView.Rows[0];
				if (bUpd_GroupViewName) { strFN = "GroupViewName"; drDB[strFN] = strGroupViewName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"View_GroupView"
					, dtDB_View_GroupView
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}

		public DataSet WAS_View_GroupView_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_View_GroupView objRQ_View_GroupView
			////
			, out RT_View_GroupView objRT_View_GroupView
			)
		{
			#region // Temp:
			string strTid = objRQ_View_GroupView.Tid;
			objRT_View_GroupView = new RT_View_GroupView();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_GroupView.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_GroupView_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_GroupView_Delete;
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
				List<View_GroupView> lst_View_GroupView = new List<View_GroupView>();
				#endregion

				#region // View_GroupView_Delete:
				mdsResult = View_GroupView_Delete(
					objRQ_View_GroupView.Tid // strTid
					, objRQ_View_GroupView.GwUserCode // strGwUserCode
					, objRQ_View_GroupView.GwPassword // strGwPassword
					, objRQ_View_GroupView.WAUserCode // strUserCode
					, objRQ_View_GroupView.WAUserPassword // strUserPassword
					, objRQ_View_GroupView.AccessToken // strAccessToken
					, objRQ_View_GroupView.NetworkID // strNetworkID
					, objRQ_View_GroupView.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_GroupView.View_GroupView.GroupViewCode // objGroupViewCode
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
		public DataSet View_GroupView_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objGroupViewCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_GroupView_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.View_GroupView_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_GroupView_DeleteX:
				//DataSet dsGetData = null;
				{
					View_GroupView_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void View_GroupView_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_GroupView_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objGroupViewCode", objGroupViewCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);

			////
			DataTable dtDB_View_GroupView = null;
			{
				////
				View_GroupView_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strGroupViewCode // strGroupViewCode 
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_GroupView // dtDB_View_GroupView
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_View_GroupView.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"View_GroupView"
					, dtDB_View_GroupView
					);
			}
			#endregion
		}
		#endregion

		#region // View_ColumnInGroup:
		private void View_ColumnInGroup_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objGroupViewCode
			, object objColumnViewCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_View_ColumnInGroup
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from View_ColumnInGroup t --//[mylock]
					where (1=1)
						and t.GroupViewCode = @objGroupViewCode
						and t.ColumnViewCode = @objColumnViewCode
					;
				");
			dtDB_View_ColumnInGroup = _cf.db.ExecQuery(
				strSqlExec
				, "@objGroupViewCode", objGroupViewCode
				, "@objColumnViewCode", objColumnViewCode
				).Tables[0];
			dtDB_View_ColumnInGroup.TableName = "View_ColumnInGroup";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_View_ColumnInGroup.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.GroupViewCode", objGroupViewCode
						, "Check.ColumnViewCode", objColumnViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnInGroup_CheckDB_DistrictNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_View_ColumnInGroup.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.GroupViewCode", objGroupViewCode
						, "Check.ColumnViewCode", objColumnViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnInGroup_CheckDB_DistrictExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_View_ColumnInGroup.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.GroupViewCode", objGroupViewCode
					, "Check.ColumnViewCode", objColumnViewCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_View_ColumnInGroup.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.View_ColumnInGroup_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void View_ColumnInGroup_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_ColumnInGroup
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "View_ColumnInGroup_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_View_ColumnInGroup", strRt_Cols_View_ColumnInGroup
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_View_ColumnInGroup = (strRt_Cols_View_ColumnInGroup != null && strRt_Cols_View_ColumnInGroup.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_View_ColumnInGroup_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, vcligr.GroupViewCode
							, vcligr.ColumnViewCode
						into #tbl_View_ColumnInGroup_Filter_Draft
						from View_ColumnInGroup vcligr --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							vcligr.GroupViewCode asc 
							, vcligr.ColumnViewCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_View_ColumnInGroup_Filter_Draft t --//[mylock]
						;

						---- #tbl_View_ColumnInGroup_Filter:
						select
							t.*
						into #tbl_View_ColumnInGroup_Filter
						from #tbl_View_ColumnInGroup_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- View_ColumnInGroup --------:
						zzB_Select_View_ColumnInGroup_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_View_ColumnInGroup_Filter_Draft;
						--drop table #tbl_View_ColumnInGroup_Filter;
					"
				);
			////
			string zzB_Select_View_ColumnInGroup_zzE = "-- Nothing.";
			if (bGet_View_ColumnInGroup)
			{
				#region // bGet_View_ColumnInGroup:
				zzB_Select_View_ColumnInGroup_zzE = CmUtils.StringUtils.Replace(@"
                        ---- View_ColumnInGroup:
                        select
                            t.MyIdxSeq
	                        , vcligr.*
							, vgrv.GroupViewCode vgrv_GroupViewCode
							, vgrv.GroupViewName vgrv_GroupViewName
							, vclv.ColumnViewCode vclv_ColumnViewCode
							, vclv.ColumnViewName vclv_ColumnViewName
                        from #tbl_View_ColumnInGroup_Filter t --//[mylock]
	                        inner join View_ColumnInGroup vcligr --//[mylock]
		                        on t.GroupViewCode = vcligr.GroupViewCode
									and t.ColumnViewCode = vcligr.ColumnViewCode
							left join View_ColumnView vclv --//[mylock]
								on t.ColumnViewCode = vcligr.ColumnViewCode
							left join View_GroupView vgrv --//[mylock]
								on t.GroupViewCode = vgrv.GroupViewCode
                        order by 
							t.MyIdxSeq asc
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
						, "View_ColumnInGroup" // strTableNameDB
						, "View_ColumnInGroup." // strPrefixStd
						, "vcligr." // strPrefixAlias
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
				, "zzB_Select_View_ColumnInGroup_zzE", zzB_Select_View_ColumnInGroup_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_View_ColumnInGroup)
			{
				dsGetData.Tables[nIdxTable++].TableName = "View_ColumnInGroup";
			}
			#endregion
		}

		public DataSet View_ColumnInGroup_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_View_ColumnInGroup
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "View_ColumnInGroup_Get";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnInGroup_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_View_ColumnInGroup", strRt_Cols_View_ColumnInGroup
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnInGroup_GetX:
				DataSet dsGetData = null;
				{
					////
					View_ColumnInGroup_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
						////
						, strRt_Cols_View_ColumnInGroup // strRt_Cols_View_ColumnInGroup
						////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_View_ColumnInGroup_Get(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnInGroup objRQ_View_ColumnInGroup
			////
			, out RT_View_ColumnInGroup objRT_View_ColumnInGroup
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnInGroup.Tid;
			objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnInGroup_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnInGroup_Get;
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
				List<View_ColumnInGroup> lst_View_ColumnInGroup = new List<View_ColumnInGroup>();
				#endregion

				#region // WAS_View_ColumnInGroup_Get:
				mdsResult = View_ColumnInGroup_Get(
					objRQ_View_ColumnInGroup.Tid // strTid
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					, objRQ_View_ColumnInGroup.WAUserCode // strUserCode
					, objRQ_View_ColumnInGroup.WAUserPassword // strUserPassword
					, objRQ_View_ColumnInGroup.AccessToken // strAccessToken
					, objRQ_View_ColumnInGroup.NetworkID // strNetworkID
					, objRQ_View_ColumnInGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_View_ColumnInGroup.Ft_RecordStart // strFt_RecordStart
					, objRQ_View_ColumnInGroup.Ft_RecordCount // strFt_RecordCount
					, objRQ_View_ColumnInGroup.Ft_WhereClause // strFt_WhereClause
															  //// Return:
					, objRQ_View_ColumnInGroup.Rt_Cols_View_ColumnInGroup // strRt_Cols_View_ColumnInGroup
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_View_ColumnInGroup.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_View_ColumnInGroup = mdsResult.Tables["View_ColumnInGroup"].Copy();
					lst_View_ColumnInGroup = TUtils.DataTableCmUtils.ToListof<View_ColumnInGroup>(dt_View_ColumnInGroup);
					objRT_View_ColumnInGroup.Lst_View_ColumnInGroup = lst_View_ColumnInGroup;
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

		public DataSet WAS_View_ColumnInGroup_Create(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnInGroup objRQ_View_ColumnInGroup
			////
			, out RT_View_ColumnInGroup objRT_View_ColumnInGroup
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnInGroup.Tid;
			objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnInGroup_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnInGroup_Create;
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
				List<View_ColumnInGroup> lst_View_ColumnInGroup = new List<View_ColumnInGroup>();
				#endregion

				#region // WS_View_ColumnInGroup_Create:
				mdsResult = View_ColumnInGroup_Create(
					objRQ_View_ColumnInGroup.Tid // strTid
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					, objRQ_View_ColumnInGroup.WAUserCode // strUserCode
					, objRQ_View_ColumnInGroup.WAUserPassword // strUserPassword
					, objRQ_View_ColumnInGroup.AccessToken // strAccessToken
					, objRQ_View_ColumnInGroup.NetworkID // strNetworkID
					, objRQ_View_ColumnInGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode // objGroupViewCode
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.ColumnViewCode // objColumnViewCode
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.Remark // objRemark
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
		public DataSet View_ColumnInGroup_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objGroupViewCode
			, object objColumnViewCode
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnInGroup_Create";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnInGroup_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
					, "objColumnViewCode", objColumnViewCode
					, "objRemark", objRemark
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnInGroup_CreateX:
				//DataSet dsGetData = null;
				{
					View_ColumnInGroup_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						, objColumnViewCode // objColumnViewCode
						, objRemark // objRemark        
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

		private void View_ColumnInGroup_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			, object objColumnViewCode
			, object objRemark
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnInGroup_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objGroupViewCode", objGroupViewCode
				, "objColumnViewCode", objColumnViewCode
				, "objRemark", objRemark
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);
			string strColumnViewCode = TUtils.CUtils.StdParam(objColumnViewCode);
			string strRemark = string.Format("{0}", objRemark).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_View_ColumnInGroup = null;

			{
				////
				if (strGroupViewCode == null || strGroupViewCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strGroupViewCode", strGroupViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnInGroup_Create_InvalidGroupViewCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strColumnViewCode == null || strColumnViewCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strGroupViewCode", strGroupViewCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnInGroup_Create_InvalidColumnViewCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				View_ColumnInGroup_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strGroupViewCode // objGroupViewCode
					, strColumnViewCode // objColumnViewCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_View_ColumnInGroup // dtDB_View_ColumnInGroup
					);
				////
			}
			#endregion

			#region // SaveDB View_ColumnInGroup:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_ColumnInGroup.NewRow();
				strFN = "GroupViewCode"; drDB[strFN] = strGroupViewCode;
				strFN = "ColumnViewCode"; drDB[strFN] = strColumnViewCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_View_ColumnInGroup.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"View_ColumnInGroup" // strTableName
					, dtDB_View_ColumnInGroup // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_View_ColumnInGroup_Update(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnInGroup objRQ_View_ColumnInGroup
			////
			, out RT_View_ColumnInGroup objRT_View_ColumnInGroup
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnInGroup.Tid;
			objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnInGroup_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnInGroup_Update;
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
				List<View_ColumnInGroup> lst_View_ColumnInGroup = new List<View_ColumnInGroup>();
				#endregion

				#region // View_ColumnInGroup_Update:
				mdsResult = View_ColumnInGroup_Update(
					objRQ_View_ColumnInGroup.Tid // strTid
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					, objRQ_View_ColumnInGroup.WAUserCode // strUserCode
					, objRQ_View_ColumnInGroup.WAUserPassword // strUserPassword
					, objRQ_View_ColumnInGroup.AccessToken // strAccessToken
					, objRQ_View_ColumnInGroup.NetworkID // strNetworkID
					, objRQ_View_ColumnInGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode // objGroupViewCode
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.ColumnViewCode // objColumnViewCode
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.FlagActive // objFlagActive
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.Remark // objRemark
					////
					, objRQ_View_ColumnInGroup.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet View_ColumnInGroup_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// 
			, object objGroupViewCode
			, object objColumnViewCode
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnInGroup_Update";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnInGroup_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
					, "objColumnViewCode", objColumnViewCode
					, "objFlagActive", objFlagActive
					, "objRemark", objRemark
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnInGroup_UpdateX:
				//DataSet dsGetData = null;
				{
					View_ColumnInGroup_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						, objColumnViewCode // objColumnViewCode
						, objFlagActive // objFlagActive
						, objRemark // objRemark
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void View_ColumnInGroup_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			, object objColumnViewCode
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnInGroup_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.View_ColumnInGroup_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupViewCode", objGroupViewCode
				, "objColumnViewCode", objColumnViewCode
				, "objFlagActive", objFlagActive
				, "objRemark", objRemark
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);
			string strColumnViewCode = TUtils.CUtils.StdParam(objColumnViewCode);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("View_ColumnInGroup.FlagActive".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("View_ColumnInGroup.Remark".ToUpper());

			////
			DataTable dtDB_View_ColumnInGroup = null;
			{
				////
				View_ColumnInGroup_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strGroupViewCode // objGroupViewCode 
					 , strColumnViewCode // objColumnViewCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_ColumnInGroup // dtDB_Mst_Organ
					);
				////
			}

			#endregion

			#region // Save View_ColumnInGroup:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_View_ColumnInGroup.Rows[0];
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"View_ColumnInGroup"
					, dtDB_View_ColumnInGroup
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}
		public DataSet WAS_View_ColumnInGroup_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnInGroup objRQ_View_ColumnInGroup
			////
			, out RT_View_ColumnInGroup objRT_View_ColumnInGroup
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnInGroup.Tid;
			objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_View_ColumnInGroup.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnInGroup_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnInGroup_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "View_ColumnInGroup", TJson.JsonConvert.SerializeObject(objRQ_View_ColumnInGroup.View_ColumnInGroup)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<View_ColumnInGroup> lst_View_ColumnInGroup = new List<View_ColumnInGroup>();

				//List<View_ColumnInGroupInGroup> lst_View_ColumnInGroupInGroup = new List<View_ColumnInGroupInGroup>();
				#endregion

				#region // View_ColumnInGroup_Delete:
				mdsResult = View_ColumnInGroup_Delete(
					objRQ_View_ColumnInGroup.Tid // strTid
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					, objRQ_View_ColumnInGroup.WAUserCode // strUserCode
					, objRQ_View_ColumnInGroup.WAUserPassword // strUserPassword
					, objRQ_View_ColumnInGroup.AccessToken // strAccessToken
					, objRQ_View_ColumnInGroup.NetworkID // strNetworkID
					, objRQ_View_ColumnInGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode // objGroupViewCode
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.ColumnViewCode // objColumnViewCode
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
		public DataSet View_ColumnInGroup_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objGroupViewCode
			, object objColumnViewCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "View_ColumnInGroup_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnInGroup_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objGroupViewCode", objGroupViewCode
					, "objColumnViewCode", objColumnViewCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // View_ColumnInGroup_DeleteX:
				//DataSet dsGetData = null;
				{
					View_ColumnInGroup_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objGroupViewCode // objGroupViewCode
						, objColumnViewCode // objColumnViewCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void View_ColumnInGroup_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objGroupViewCode
			, object objColumnViewCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "View_ColumnInGroup_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objGroupViewCode", objGroupViewCode
				, "objColumnViewCode", objColumnViewCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);

			////
			DataTable dtDB_View_ColumnInGroup = null;
			{
				////
				View_ColumnInGroup_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , objGroupViewCode // objGroupViewCode 
					 , objColumnViewCode // objColumnViewCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_View_ColumnInGroup // dtDB_View_ColumnInGroup
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_View_ColumnInGroup.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"View_ColumnInGroup"
					, dtDB_View_ColumnInGroup
					);
			}
			#endregion
		}
		private void View_ColumnInGroup_SaveX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objFlagIsDelete
			////
			, object objGroupViewCode
			, DataSet dsData
			////
			, out DataSet mdsFinal
			)
		{
			#region // Temp:
			mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "View_ColumnInGroup_SaveX";
			//string strErrorCodeDefault = TError.ErrTCGQLTV.View_ColumnInGroup_Save;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objFlagIsDelete", objFlagIsDelete
				////
				, "objGroupViewCode", objGroupViewCode
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

			#region //// Refine and Check:
			////
			bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
			////
			string strGroupViewCode = TUtils.CUtils.StdParam(objGroupViewCode);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			//myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
			////
			DataTable dtDB_View_GroupView = null;
			{
				////
				View_GroupView_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strGroupViewCode // objGroupViewCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strStatusListToCheck
					, out dtDB_View_GroupView // dtDB_View_GroupView
					);
				
				////
			}
			////
			#endregion

			#region //// Refine and Check View_ColumnInGroup:
			////
			DataTable dtInput_View_ColumnInGroup = null;
			if (!bIsDelete)
			{
				////
				string strTableCheck = "View_ColumnInGroup";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.View_ColumnInGroup_Save_Input_View_ColumnInGroupTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_View_ColumnInGroup = dsData.Tables[strTableCheck];
				////
				//if (dtInput_View_ColumnInGroup.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.View_ColumnInGroup_Save_Input_View_ColumnInGroupTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_View_ColumnInGroup // dtData
					, "StdParam", "ColumnViewCode" // arrstrCouple
					, "", "Remark" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_View_ColumnInGroup, "GroupViewCode", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_View_ColumnInGroup, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_View_ColumnInGroup, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_View_ColumnInGroup, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_View_ColumnInGroup, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_View_ColumnInGroup.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_View_ColumnInGroup.Rows[nScan];

					////
					DataTable dtDB_View_ColumnView = null;

					View_ColumnView_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, drScan["ColumnViewCode"] // objColumnViewCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_View_ColumnView // dtDB_View_ColumnView
						);
					////

					////
					drScan["GroupViewCode"] = strGroupViewCode;
					drScan["NetworkID"] = nNetworkID;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp View_ColumnInGroup:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_View_ColumnInGroup"
					, new object[]{
						"GroupViewCode", TConst.BizMix.Default_DBColType,
						"ColumnViewCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_View_ColumnInGroup
					);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- #tbl_View_ColumnInGroup:
							select 
								t.GroupViewCode
								, t.ColumnViewCode
							into #tbl_View_ColumnInGroup
							from View_ColumnInGroup t --//[mylock]
							where (1=1)
								and t.GroupViewCode = @strGroupViewCode
							;

							--- Delete:
							---- View_ColumnInGroup:
							delete t 
							from View_ColumnInGroup t --//[mylock]
								inner join #tbl_View_ColumnInGroup f --//[mylock]
									on t.GroupViewCode = f.GroupViewCode
										and t.ColumnViewCode = f.ColumnViewCode
							where (1=1)
							;

						");
				_cf.db.ExecQuery(
					strSqlDelete
					, "@strGroupViewCode", strGroupViewCode
					);
			}

			//// Insert All:
			if (!bIsDelete)
			{
				////
				string zzzzClauseInsert_View_ColumnInGroup_zSave = CmUtils.StringUtils.Replace(@"
						---- View_ColumnInGroup:
						insert into View_ColumnInGroup
						(
							GroupViewCode
							, ColumnViewCode
							, NetworkID
							, FlagActive
							, Remark
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.GroupViewCode
							, t.ColumnViewCode
							, t.NetworkID
							, t.FlagActive
							, t.Remark
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_View_ColumnInGroup t --//[mylock]
						;
					");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_View_ColumnInGroup_zSave
						----
					"
					, "zzzzClauseInsert_View_ColumnInGroup_zSave", zzzzClauseInsert_View_ColumnInGroup_zSave
					);
				////
				DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
			}
			#endregion

			#region //// Clear For Debug:
			if (!bIsDelete)
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_View_ColumnInGroup;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
		#endregion
			
			return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}
		public DataSet View_ColumnInGroup_Save(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objFlagIsDelete
			////
			, object objGroupViewCode
			, DataSet dsData
			/////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.Now;
			bool bNeedTransaction = true;
			string strFunctionName = "View_ColumnInGroup_Save";
			string strErrorCodeDefault = TError.ErridnInventory.View_ColumnInGroup_Save;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objFlagIsDelete", objFlagIsDelete
				    ////
				    , "objGroupViewCode", objGroupViewCode
					});
			//ArrayList alPCErrEx = new ArrayList();
			////
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Convert Input:
				//DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
				//if (dsData == null) dsData = new DataSet("dsData");
				//dsData.AcceptChanges();
				//alParamsCoupleError.AddRange(new object[]{
				//	"Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
				//	});
				#endregion

				#region // View_ColumnInGroup_SaveX:
				DataSet dsGetData = null;
				{
					View_ColumnInGroup_SaveX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						/////
						, objFlagIsDelete // objFlagIsDelete
						/////
						, objGroupViewCode // objGroupViewCode
						//////
						, dsData
						////
						, out dsGetData // dsGetData
						);
					////
					//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
					////
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
		public DataSet WAS_View_ColumnInGroup_Save(
			ref ArrayList alParamsCoupleError
			, RQ_View_ColumnInGroup objRQ_View_ColumnInGroup
			////
			, out RT_View_ColumnInGroup objRT_View_ColumnInGroup
			)
		{
			#region // Temp:
			string strTid = objRQ_View_ColumnInGroup.Tid;
			objRT_View_ColumnInGroup = new RT_View_ColumnInGroup();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_View_ColumnInGroup_Save";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_View_ColumnInGroup_Save;
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
				DataSet dsData = new DataSet();
				{
					////
					if (objRQ_View_ColumnInGroup.Lst_View_ColumnInGroup == null) objRQ_View_ColumnInGroup.Lst_View_ColumnInGroup = new List<View_ColumnInGroup>();
					DataTable dt_View_ColumnInGroup = TUtils.DataTableCmUtils.ToDataTable<View_ColumnInGroup>(objRQ_View_ColumnInGroup.Lst_View_ColumnInGroup, "View_ColumnInGroup");
					dsData.Tables.Add(dt_View_ColumnInGroup);
				}
				#endregion

				#region // View_ColumnInGroup_Save:
				mdsResult = View_ColumnInGroup_Save(
					objRQ_View_ColumnInGroup.Tid // strTid
					, objRQ_View_ColumnInGroup.GwUserCode // strGwUserCode
					, objRQ_View_ColumnInGroup.GwPassword // strGwPassword
					, objRQ_View_ColumnInGroup.WAUserCode // strUserCode
					, objRQ_View_ColumnInGroup.WAUserPassword // strUserPassword
					, objRQ_View_ColumnInGroup.AccessToken // strAccessToken
					, objRQ_View_ColumnInGroup.NetworkID // strNetworkID
					, objRQ_View_ColumnInGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_View_ColumnInGroup.FlagIsDelete // objFlagIsDelete
					////
					, objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode // objGroupViewCode
					////
					, dsData // dsData
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

		#region // Mst_InventoryBlock:
		private void Mst_InventoryBlock_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objInvBlockCode
			, object objShelfCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_InventoryBlock
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_InventoryBlock t --//[mylock]
					where (1=1)
						and t.InvBlockCode = @objInvBlockCode
						and t.ShelfCode = @objShelfCode
					;
				");
			dtDB_Mst_InventoryBlock = _cf.db.ExecQuery(
				strSqlExec
				, "@objInvBlockCode", objInvBlockCode
				, "@objShelfCode", objShelfCode
				).Tables[0];
			dtDB_Mst_InventoryBlock.TableName = "Mst_InventoryBlock";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_InventoryBlock.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.InvBlockCode", objInvBlockCode
						, "Check.ShelfCode", objShelfCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_CheckDB_DistrictNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_InventoryBlock.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.InvBlockCode", objInvBlockCode
						, "Check.ShelfCode", objShelfCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_CheckDB_DistrictExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_InventoryBlock.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.InvBlockCode", objInvBlockCode
					, "Check.ShelfCode", objShelfCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_InventoryBlock.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_InventoryBlock_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_InventoryBlock_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_InventoryBlock
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_InventoryBlock_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_Mst_InventoryBlock", strRt_Cols_Mst_InventoryBlock
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_InventoryBlock = (strRt_Cols_Mst_InventoryBlock != null && strRt_Cols_Mst_InventoryBlock.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_Mst_InventoryBlock_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mivbl.InvBlockCode
							, mivbl.ShelfCode
						into #tbl_Mst_InventoryBlock_Filter_Draft
						from Mst_InventoryBlock mivbl --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mivbl.InvBlockCode asc 
							, mivbl.ShelfCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_InventoryBlock_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_InventoryBlock_Filter:
						select
							t.*
						into #tbl_Mst_InventoryBlock_Filter
						from #tbl_Mst_InventoryBlock_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_InventoryBlock --------:
						zzB_Select_Mst_InventoryBlock_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_InventoryBlock_Filter_Draft;
						--drop table #tbl_Mst_InventoryBlock_Filter;
					"
				);
			////
			string zzB_Select_Mst_InventoryBlock_zzE = "-- Nothing.";
			if (bGet_Mst_InventoryBlock)
			{
				#region // bGet_Mst_InventoryBlock:
				zzB_Select_Mst_InventoryBlock_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_InventoryBlock:
                        select
                            t.MyIdxSeq
	                        , mivbl.*
							, mi.InvCode mi_InvCode
							, mi.InvName mi_InvName
                        from #tbl_Mst_InventoryBlock_Filter t --//[mylock]
	                        inner join Mst_InventoryBlock mivbl --//[mylock]
		                        on t.InvBlockCode = mivbl.InvBlockCode
									and t.ShelfCode = mivbl.ShelfCode
							left join Mst_Inventory mi --//[mylock]
								on t.ShelfCode = mi.InvCode
                        order by 
							t.MyIdxSeq asc
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
						, "Mst_InventoryBlock" // strTableNameDB
						, "Mst_InventoryBlock." // strPrefixStd
						, "mivbl." // strPrefixAlias
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
				, "zzB_Select_Mst_InventoryBlock_zzE", zzB_Select_Mst_InventoryBlock_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_InventoryBlock)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_InventoryBlock";
			}
			#endregion
		}

		public DataSet Mst_InventoryBlock_Get(
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
			, string strRt_Cols_Mst_InventoryBlock
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_InventoryBlock_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryBlock_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InventoryBlock", strRt_Cols_Mst_InventoryBlock
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

				#region // Mst_InventoryBlock_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_InventoryBlock_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_InventoryBlock // strRt_Cols_Mst_InventoryBlock
														////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet WAS_Mst_InventoryBlock_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock
			////
			, out RT_Mst_InventoryBlock objRT_Mst_InventoryBlock
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryBlock.Tid;
			objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryBlock_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryBlock_Get;
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
				List<Mst_InventoryBlock> lst_Mst_InventoryBlock = new List<Mst_InventoryBlock>();
				#endregion

				#region // WAS_Mst_InventoryBlock_Get:
				mdsResult = Mst_InventoryBlock_Get(
					objRQ_Mst_InventoryBlock.Tid // strTid
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					, objRQ_Mst_InventoryBlock.WAUserCode // strUserCode
					, objRQ_Mst_InventoryBlock.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_InventoryBlock.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_InventoryBlock.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_InventoryBlock.Ft_WhereClause // strFt_WhereClause
															  //// Return:
					, objRQ_Mst_InventoryBlock.Rt_Cols_Mst_InventoryBlock // strRt_Cols_Mst_InventoryBlock
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_InventoryBlock.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_InventoryBlock = mdsResult.Tables["Mst_InventoryBlock"].Copy();
					lst_Mst_InventoryBlock = TUtils.DataTableCmUtils.ToListof<Mst_InventoryBlock>(dt_Mst_InventoryBlock);
					objRT_Mst_InventoryBlock.Lst_Mst_InventoryBlock = lst_Mst_InventoryBlock;
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
		public DataSet WAS_Mst_InventoryBlock_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock
			////
			, out RT_Mst_InventoryBlock objRT_Mst_InventoryBlock
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryBlock.Tid;
			objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryBlock_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryBlock_Create;
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
				List<Mst_InventoryBlock> lst_Mst_InventoryBlock = new List<Mst_InventoryBlock>();
				#endregion

				#region // WS_Mst_InventoryBlock_Create:
				mdsResult = Mst_InventoryBlock_Create(
					objRQ_Mst_InventoryBlock.Tid // strTid
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					, objRQ_Mst_InventoryBlock.WAUserCode // strUserCode
					, objRQ_Mst_InventoryBlock.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.InvBlockCode // objInvBlockCode
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.ShelfCode // objShelfCode
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.InvBlockDesc // objInvBlockDesc
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Length // objLength
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Width // objWidth
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Height // objHeight
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Remark // objRemark
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
		public DataSet Mst_InventoryBlock_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objInvBlockCode
			, object objShelfCode
			, object objInvBlockDesc
			, object objLength
			, object objWidth
			, object objHeight
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryBlock_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryBlock_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objInvBlockCode", objInvBlockCode
					, "objShelfCode", objShelfCode
					, "objInvBlockDesc", objInvBlockDesc
					, "objLength", objLength
					, "objWidth", objWidth
					, "objHeight", objHeight
					, "objRemark", objRemark
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

				#region // Mst_InventoryBlock_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryBlock_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objInvBlockCode // objInvBlockCode
						, objShelfCode // objShelfCode
						, objInvBlockDesc // objInvBlockDesc
						, objLength // objLength
						, objWidth // objWidth
						, objHeight // objHeight
						, objRemark // objRemark        
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

		private void Mst_InventoryBlock_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objInvBlockCode
			, object objShelfCode
			, object objInvBlockDesc
			, object objLength
			, object objWidth
			, object objHeight
			, object objRemark
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryBlock_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objInvBlockCode", objInvBlockCode
				, "objShelfCode", objShelfCode
				, "objInvBlockDesc", objInvBlockDesc
				, "objLength", objLength
				, "objWidth", objWidth
				, "objHeight", objHeight
				, "objRemark", objRemark
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strInvBlockCode = TUtils.CUtils.StdParam(objInvBlockCode);
			string strShelfCode = TUtils.CUtils.StdParam(objShelfCode);
			string strInvBlockDesc = string.Format("{0}", objInvBlockDesc).Trim();
			double dblLength = Convert.ToDouble(objLength);
			double dblWidth = Convert.ToDouble(objWidth);
			double dblHeight = Convert.ToDouble(objHeight);
			string strRemark = string.Format("{0}", objRemark).Trim();

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_InventoryBlock = null;

			{
				////
				if (strInvBlockCode == null || strInvBlockCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvBlockCode", strInvBlockCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidInvBlockCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strShelfCode == null || strShelfCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strInvBlockCode", strInvBlockCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidShelfCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_InventoryBlock_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strInvBlockCode // objInvBlockCode
					, strShelfCode // objShelfCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_InventoryBlock // dtDB_Mst_InventoryBlock
					);
				////
				if (dblLength < 0.0 )
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueLength
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (dblWidth < 0.0)
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueWidth
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (dblHeight < 0.0)
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueHeight
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}
			#endregion

			#region // SaveDB Mst_InventoryBlock:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryBlock.NewRow();
				strFN = "InvBlockCode"; drDB[strFN] = strInvBlockCode;
				strFN = "ShelfCode"; drDB[strFN] = strShelfCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "InvBlockDesc"; drDB[strFN] = strInvBlockDesc;
				strFN = "Length"; drDB[strFN] = dblLength;
				strFN = "Width"; drDB[strFN] = dblWidth;
				strFN = "Height"; drDB[strFN] = dblHeight;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_InventoryBlock.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryBlock" // strTableName
					, dtDB_Mst_InventoryBlock // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_Mst_InventoryBlock_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock
			////
			, out RT_Mst_InventoryBlock objRT_Mst_InventoryBlock
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryBlock.Tid;
			objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryBlock_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryBlock_Update;
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
				List<Mst_InventoryBlock> lst_Mst_InventoryBlock = new List<Mst_InventoryBlock>();
				#endregion

				#region // Mst_InventoryBlock_Update:
				mdsResult = Mst_InventoryBlock_Update(
					objRQ_Mst_InventoryBlock.Tid // strTid
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					, objRQ_Mst_InventoryBlock.WAUserCode // strUserCode
					, objRQ_Mst_InventoryBlock.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.InvBlockCode // objInvBlockCode
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.ShelfCode // objShelfCode
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.InvBlockDesc // objInvBlockDesc
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Length // objLength
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Width // objWidth
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Height // objHeight
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.FlagActive // objFlagActive
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.Remark // objRemark
					////
					, objRQ_Mst_InventoryBlock.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_InventoryBlock_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			//// 
			, object objInvBlockCode
			, object objShelfCode
			, object objInvBlockDesc
			, object objLength
			, object objWidth
			, object objHeight
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryBlock_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryBlock_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objInvBlockCode", objInvBlockCode
					, "objShelfCode", objShelfCode
					, "objInvBlockDesc", objInvBlockDesc
					, "objLength", objLength
					, "objWidth", objWidth
					, "objHeight", objHeight
					, "objFlagActive", objFlagActive
					, "objRemark", objRemark
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

				#region // Mst_InventoryBlock_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryBlock_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objInvBlockCode // objInvBlockCode
						, objShelfCode // objShelfCode
						, objInvBlockDesc // objInvBlockDesc 
						, objLength // objLength
						, objWidth // objWidth
						, objHeight // objHeight
						, objFlagActive // objFlagActive
						, objRemark // objRemark
						/////
						, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void Mst_InventoryBlock_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objInvBlockCode
			, object objShelfCode
			, object objInvBlockDesc
			, object objLength
			, object objWidth
			, object objHeight
			, object objFlagActive
			, object objRemark
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryBlock_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_InventoryBlock_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objInvBlockCode", objInvBlockCode
				, "objShelfCode", objShelfCode
				, "objInvBlockDesc", objInvBlockDesc
				, "objLength", objLength
				, "objWidth", objWidth
				, "objHeight", objHeight
				, "objFlagActive", objFlagActive
				, "objRemark", objRemark
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strInvBlockCode = TUtils.CUtils.StdParam(objInvBlockCode);
			string strShelfCode = TUtils.CUtils.StdParam(objShelfCode);
			string strInvBlockDesc = string.Format("{0}", objInvBlockDesc).Trim();
			double dblLength = Convert.ToDouble(objLength);
			double dblWidth = Convert.ToDouble(objWidth);
			double dblHeight = Convert.ToDouble(objHeight);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strRemark = string.Format("{0}", objRemark).Trim();
			////
			bool bUpd_InvBlockDesc = strFt_Cols_Upd.Contains("Mst_InventoryBlock.InvBlockDesc".ToUpper());
			bool bUpd_Length = strFt_Cols_Upd.Contains("Mst_InventoryBlock.Length".ToUpper());
			bool bUpd_Width = strFt_Cols_Upd.Contains("Mst_InventoryBlock.Width".ToUpper());
			bool bUpd_Height = strFt_Cols_Upd.Contains("Mst_InventoryBlock.Height".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_InventoryBlock.FlagActive".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("Mst_InventoryBlock.Remark".ToUpper());

			////
			DataTable dtDB_Mst_InventoryBlock = null;
			{
				////
				Mst_InventoryBlock_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strInvBlockCode // objInvBlockCode 
					 , strShelfCode // objShelfCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryBlock // dtDB_Mst_InventoryBlock
					);
				////
				//DataTable dtDB_Mst_Inventory = null;

				//Mst_Inventory_CheckDB(
				//	 ref alParamsCoupleError // alParamsCoupleError
				//	 , strShelfCode // objInvCode
				//	 , TConst.Flag.Yes // strFlagExistToCheck
				//	 , TConst.Flag.Active // strFlagActiveListToCheck
				//	 , out dtDB_Mst_Inventory // dtDB_Mst_Inventory
				//	);
				////
				if (dblLength < 0.0)
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueLength
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (dblWidth < 0.0)
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueWidth
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (dblHeight < 0.0)
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_InventoryBlock_Create_InvalidValueHeight
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
			}

			#endregion

			#region // Save Mst_InventoryBlock:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_InventoryBlock.Rows[0];
				if (bUpd_InvBlockDesc) { strFN = "InvBlockDesc"; drDB[strFN] = strInvBlockDesc; alColumnEffective.Add(strFN); }
				if (bUpd_Length) { strFN = "Length"; drDB[strFN] = dblLength; alColumnEffective.Add(strFN); }
				if (bUpd_Width) { strFN = "Width"; drDB[strFN] = dblWidth; alColumnEffective.Add(strFN); }
				if (bUpd_Height) { strFN = "Height"; drDB[strFN] = dblHeight; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryBlock"
					, dtDB_Mst_InventoryBlock
					, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_Mst_InventoryBlock_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_InventoryBlock objRQ_Mst_InventoryBlock
			////
			, out RT_Mst_InventoryBlock objRT_Mst_InventoryBlock
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_InventoryBlock.Tid;
			objRT_Mst_InventoryBlock = new RT_Mst_InventoryBlock();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InventoryBlock.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_InventoryBlock_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_InventoryBlock_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_InventoryBlock", TJson.JsonConvert.SerializeObject(objRQ_Mst_InventoryBlock.Mst_InventoryBlock)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_InventoryBlock> lst_Mst_InventoryBlock = new List<Mst_InventoryBlock>();

				//List<Mst_InventoryBlockInGroup> lst_Mst_InventoryBlockInGroup = new List<Mst_InventoryBlockInGroup>();
				#endregion

				#region // Mst_InventoryBlock_Delete:
				mdsResult = Mst_InventoryBlock_Delete(
					objRQ_Mst_InventoryBlock.Tid // strTid
					, objRQ_Mst_InventoryBlock.GwUserCode // strGwUserCode
					, objRQ_Mst_InventoryBlock.GwPassword // strGwPassword
					, objRQ_Mst_InventoryBlock.WAUserCode // strUserCode
					, objRQ_Mst_InventoryBlock.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.InvBlockCode // objInvBlockCode
					, objRQ_Mst_InventoryBlock.Mst_InventoryBlock.ShelfCode // objShelfCode
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
		public DataSet Mst_InventoryBlock_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objInvBlockCode
			, object objShelfCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_InventoryBlock_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_InventoryBlock_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objInvBlockCode", objInvBlockCode
					, "objShelfCode", objShelfCode
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

				#region // Mst_InventoryBlock_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_InventoryBlock_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objInvBlockCode // objInvBlockCode
						, objShelfCode // objShelfCode
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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

		private void Mst_InventoryBlock_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objInvBlockCode
			, object objShelfCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_InventoryBlock_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objInvBlockCode", objInvBlockCode
				, "objShelfCode", objShelfCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strInvBlockCode = TUtils.CUtils.StdParam(objInvBlockCode);

			////
			DataTable dtDB_Mst_InventoryBlock = null;
			{
				////
				Mst_InventoryBlock_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , objInvBlockCode // objInvBlockCode 
					 , objShelfCode // objShelfCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_InventoryBlock // dtDB_Mst_InventoryBlock
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_InventoryBlock.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_InventoryBlock"
					, dtDB_Mst_InventoryBlock
					);
			}
			#endregion
		}
		#endregion

		#region // Map_DealerDiscount:
		private void Map_DealerDiscount_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Map_DealerDiscount
			, out DataSet dsGetData
		   )
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Map_DealerDiscount_GetX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Map_DealerDiscount_GetX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Map_DealerDiscount = (strRt_Cols_Map_DealerDiscount != null && strRt_Cols_Map_DealerDiscount.Length > 0);

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
						---- #tbl_Map_DealerDiscount_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mdldc.DLCode
							, mdldc.DiscountCode
						into #tbl_Map_DealerDiscount_Filter_Draft
						from Map_DealerDiscount mdldc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mdldc.DLCode asc
								, mdldc.DiscountCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Map_DealerDiscount_Filter_Draft t --//[mylock]
						;

						---- #tbl_Map_DealerDiscount_Filter:
						select
							t.*
						into #tbl_Map_DealerDiscount_Filter
						from #tbl_Map_DealerDiscount_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Map_DealerDiscount --------:
						zzB_Select_Map_DealerDiscount_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Map_DealerDiscount_Filter_Draft;
						--drop table #tbl_Map_DealerDiscount_Filter;
					"
				);
			////
			string zzB_Select_Map_DealerDiscount_zzE = "-- Nothing.";
			if (bGet_Map_DealerDiscount)
			{
				#region // bGet_Map_DealerDiscount:
				zzB_Select_Map_DealerDiscount_zzE = CmUtils.StringUtils.Replace(@"
							---- Map_DealerDiscount:
							select
								t.MyIdxSeq
								, mdldc.*
							from #tbl_Map_DealerDiscount_Filter t --//[mylock]
								inner join Map_DealerDiscount mdldc --//[mylock]
									on t.DLCode = mdldc.DLCode
										and t.DiscountCode = mdldc.DiscountCode
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
						, "Map_DealerDiscount" // strTableNameDB
						, "Map_DealerDiscount." // strPrefixStd
						, "mdldc." // strPrefixAlias
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
				, "zzB_Select_Map_DealerDiscount_zzE", zzB_Select_Map_DealerDiscount_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Map_DealerDiscount)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Map_DealerDiscount";
			}
			#endregion
		}
		public DataSet Map_DealerDiscount_Get(
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
			, string strRt_Cols_Map_DealerDiscount
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			// bool bNeedTransaction = true;
			string strFunctionName = "Map_DealerDiscount_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Map_DealerDiscount_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Map_DealerDiscount", strRt_Cols_Map_DealerDiscount
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

				#region // Map_DealerDiscount_GetX:
				DataSet dsGetData = null;
				{
					////
					Map_DealerDiscount_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Map_DealerDiscount // strRt_Cols_Map_DealerDiscount
														////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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
		public DataSet WAS_Map_DealerDiscount_Get(
		   ref ArrayList alParamsCoupleError
		   , RQ_Map_DealerDiscount objRQ_Map_DealerDiscount
		   ////
		   , out RT_Map_DealerDiscount objRT_Map_DealerDiscount
		   )
		{
			#region // Temp:
			string strTid = objRQ_Map_DealerDiscount.Tid;
			objRT_Map_DealerDiscount = new RT_Map_DealerDiscount();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvoiceType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Map_DealerDiscount_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Map_DealerDiscount_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

				List<Map_DealerDiscount> lst_Map_DealerDiscount = new List<Map_DealerDiscount>();
				#endregion

				#region // Map_DealerDiscount_Get:
				mdsResult = Map_DealerDiscount_Get(
					objRQ_Map_DealerDiscount.Tid // strTid
					, objRQ_Map_DealerDiscount.GwUserCode // strGwUserCode
					, objRQ_Map_DealerDiscount.GwPassword // strGwPassword
					, objRQ_Map_DealerDiscount.WAUserCode // strUserCode
					, objRQ_Map_DealerDiscount.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Map_DealerDiscount.Ft_RecordStart // strFt_RecordStart
					, objRQ_Map_DealerDiscount.Ft_RecordCount // strFt_RecordCount
					, objRQ_Map_DealerDiscount.Ft_WhereClause // strFt_WhereClause
															  //// Return:
					, objRQ_Map_DealerDiscount.Rt_Cols_Map_DealerDiscount // Rt_Cols_Map_DealerDiscount
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Map_DealerDiscount.MySummaryTable = lst_MySummaryTable[0];

					////
					DataTable dt_Map_DealerDiscount = mdsResult.Tables["Map_DealerDiscount"].Copy();
					lst_Map_DealerDiscount = TUtils.DataTableCmUtils.ToListof<Map_DealerDiscount>(dt_Map_DealerDiscount);
					objRT_Map_DealerDiscount.Lst_Map_DealerDiscount = lst_Map_DealerDiscount;
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
		#endregion

		#region // Mst_Dealer:
		private void Mst_Dealer_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objDLCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Dealer
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Dealer t --//[mylock]
					where (1=1)
						and t.DLCode = @DLCode
					;
				");
			dtDB_Mst_Dealer = _cf.db.ExecQuery(
				strSqlExec
				, "@DLCode", objDLCode
				).Tables[0];
			dtDB_Mst_Dealer.TableName = "Mst_Dealer";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Dealer.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.DLCode", objDLCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Dealer_CheckDB_DLCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Dealer.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.DLCode", objDLCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Dealer_CheckDB_DLCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Dealer.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.DLCode", objDLCode
					, "Check.FlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Dealer.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Dealer_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet RptSv_Mst_Dealer_Get(
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
			, string strRt_Cols_Mst_Dealer
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Mst_Dealer_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Dealer", strRt_Cols_Mst_Dealer
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
				RptSv_Sys_User_CheckAuthentication(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Mst_Dealer_GetX:
				DataSet dsGetData = null;
				{
					////
					//Mst_Dealer_GetX
					RptSv_Mst_Dealer_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_Dealer // strRt_Cols_Mst_Dealer
												////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		public DataSet Mst_Dealer_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Dealer
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Dealer_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Dealer", strRt_Cols_Mst_Dealer
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_Dealer_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_Dealer_GetX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Mst_Dealer // strRt_Cols_Mst_Dealer
												////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

		private void Mst_Dealer_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Dealer
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Dealer_Get";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Dealer", strRt_Cols_Mst_Dealer
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Dealer = (strRt_Cols_Mst_Dealer != null && strRt_Cols_Mst_Dealer.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
						---- #tbl_Mst_Dealer_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, md.DLCode
						into #tbl_Mst_Dealer_Filter_Draft
						from Mst_Dealer md --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by md.DLCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Dealer_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Dealer_Filter:
						select
							t.*
						into #tbl_Mst_Dealer_Filter
						from #tbl_Mst_Dealer_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Dealer --------:
						zzB_Select_Mst_Dealer_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Dealer_Filter_Draft;
						--drop table #tbl_Mst_Dealer_Filter;
					"
				);
			////
			string zzB_Select_Mst_Dealer_zzE = "-- Nothing.";
			if (bGet_Mst_Dealer)
			{
				#region // bGet_Mst_Dealer:
				zzB_Select_Mst_Dealer_zzE = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_NNT_Filter:
                        select
	                        1 MyTotal
	                        , t.MST
	                        , t.DLCode
                        into #tbl_Mst_NNT_Filter
                        from Mst_NNT t --//[mylock]
	                        inner join #tbl_Mst_Dealer_Filter f --//[mylock]
		                        on t.DLCode = f.DLCode
                        where (1=1)
                        ;

                        select
	                        t.DLCode
	                        , Isnull(count(t.MST) , 0.0) count_MST
                        into #tbl_Mst_NNT_Filter_Count
                        from #tbl_Mst_NNT_Filter t --//[mylock]
                        where (1=1)
                        group by 
	                        t.DLCode
                        ;


                        ---- Mst_Dealer:
                        select
                            t.MyIdxSeq
	                        , md.*
							, mp.ProvinceCode mp_ProvinceCode
							, mp.ProvinceName mp_ProvinceName
	                        , Isnull(f.count_MST, 0.0) count_MST
                        from #tbl_Mst_Dealer_Filter t --//[mylock]
	                        inner join Mst_Dealer md --//[mylock]
		                        on t.DLCode = md.DLCode
	                        left join #tbl_Mst_NNT_Filter_Count f --//[mylock]
		                        on md.DLCode = f.DLCode
							left join Mst_Province mp --//[mylock]
								on md.ProvinceCode = mp.ProvinceCode
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
						, "Mst_Dealer" // strTableNameDB
						, "Mst_Dealer." // strPrefixStd
						, "md." // strPrefixAlias
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
				, "zzB_Select_Mst_Dealer_zzE", zzB_Select_Mst_Dealer_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Dealer)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Dealer";
			}
			#endregion
		}

		private void RptSv_Mst_Dealer_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Mst_Dealer
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Dealer_Get";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Dealer", strRt_Cols_Mst_Dealer
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Dealer = (strRt_Cols_Mst_Dealer != null && strRt_Cols_Mst_Dealer.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);
			DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(strWAUserCode);
			#endregion

			#region // Build Sql:
			////
			ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					});
			////
			myCache_Mst_Dealer_ViewAbility_Get(drAbilityOfUser);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Dealer_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, md.DLCode
						into #tbl_Mst_Dealer_Filter_Draft
						from Mst_Dealer md --//[mylock]
                            inner join #tbl_Mst_Dealer_ViewAbility va_mdl --//[mylock]
                                on md.DLCode = va_mdl.DLCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by md.DLCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Dealer_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Dealer_Filter:
						select
							t.*
						into #tbl_Mst_Dealer_Filter
						from #tbl_Mst_Dealer_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Dealer --------:
						zzB_Select_Mst_Dealer_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Dealer_Filter_Draft;
						--drop table #tbl_Mst_Dealer_Filter;
					"
				);
			////
			string zzB_Select_Mst_Dealer_zzE = "-- Nothing.";
			if (bGet_Mst_Dealer)
			{
				#region // bGet_Mst_Dealer:
				zzB_Select_Mst_Dealer_zzE = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_NNT_Filter:
                        select
	                        1 MyTotal
	                        , t.MST
	                        , t.DLCode
                        into #tbl_Mst_NNT_Filter
                        from Mst_NNT t --//[mylock]
	                        inner join #tbl_Mst_Dealer_Filter f --//[mylock]
		                        on t.DLCode = f.DLCode
                        where (1=1)
                        ;

                        select
	                        t.DLCode
	                        , Isnull(count(t.MST) , 0.0) count_MST
                        into #tbl_Mst_NNT_Filter_Count
                        from #tbl_Mst_NNT_Filter t --//[mylock]
                        where (1=1)
                        group by 
	                        t.DLCode
                        ;


                        ---- Mst_Dealer:
                        select
                            t.MyIdxSeq
	                        , md.*
							, mp.ProvinceCode mp_ProvinceCode
							, mp.ProvinceName mp_ProvinceName
	                        , Isnull(f.count_MST, 0.0) count_MST
                        from #tbl_Mst_Dealer_Filter t --//[mylock]
	                        inner join Mst_Dealer md --//[mylock]
		                        on t.DLCode = md.DLCode
	                        left join #tbl_Mst_NNT_Filter_Count f --//[mylock]
		                        on md.DLCode = f.DLCode
							left join Mst_Province mp --//[mylock]
								on md.ProvinceCode = mp.ProvinceCode
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
						, "Mst_Dealer" // strTableNameDB
						, "Mst_Dealer." // strPrefixStd
						, "md." // strPrefixAlias
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
				, "zzB_Select_Mst_Dealer_zzE", zzB_Select_Mst_Dealer_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Dealer)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Dealer";
			}
			#endregion
		}

		public DataSet Mst_Dealer_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objDLCode
			, object objProvinceCode
			, object objDLName
			, object objDLAddress
			, object objDLPresentBy
			, object objDLGovIDNumber
			, object objDLEmail
			, object objDLPhoneNo
			)

		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Dealer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
					, "objDLCode", objDLCode
					, "objProvinceCode", objProvinceCode
					, "objDLName", objDLName
					, "objDLAddress", objDLAddress
					, "objDLPresentBy", objDLPresentBy
					, "objDLGovIDNumber", objDLGovIDNumber
					, "objDLEmail", objDLEmail
					, "objDLPhoneNo", objDLPhoneNo
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
				string strDLName = string.Format("{0}", objDLName).Trim();
				string strDLAddress = string.Format("{0}", objDLAddress).Trim();
				string strDLPresentBy = string.Format("{0}", objDLPresentBy).Trim();
				string strDLGovIDNumber = string.Format("{0}", objDLGovIDNumber).Trim();
				string strDLEmail = string.Format("{0}", objDLEmail).Trim();
				string strDLPhoneNo = string.Format("{0}", objDLPhoneNo).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
				////
				DataTable dtDB_Mst_Dealer = null;
				{

					////
					if (strDLCode == null || strDLCode.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLCode", strDLCode
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Create_InvalidDLCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					Mst_Dealer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strDLCode // objDLCode
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
					////
					DataTable dtDB_Mst_Province = null;

					Mst_Province_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strProvinceCode // objDLCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_Province // dtDB_Mst_Dealer
						);
					////
					if (strDLName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLName", strDLName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Create_InvalidDLName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // SaveDB Mst_Dealer:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Dealer.NewRow();
					strFN = "DLCode"; drDB[strFN] = strDLCode;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
					strFN = "DLName"; drDB[strFN] = strDLName;
					strFN = "DLAddress"; drDB[strFN] = strDLAddress;
					strFN = "DLPresentBy"; drDB[strFN] = strDLPresentBy;
					strFN = "DLGovIDNumber"; drDB[strFN] = strDLGovIDNumber;
					strFN = "DLEmail"; drDB[strFN] = strDLEmail;
					strFN = "DLPhoneNo"; drDB[strFN] = strDLPhoneNo;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_Mst_Dealer.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
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

		private void Mst_Dealer_UpdBU()
		{
			string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strDLCode_Root nvarchar(100); select @strDLCode_Root = 'VN';

                    update t
                    set
	                    t.DLBUCode = @strDLCode_Root
	                    , t.DLBUPattern = @strDLCode_Root + '%'
	                    , t.DLLevel = 1
                    from Mst_Dealer t
	                    left join Mst_Dealer t_Parent
		                    on t.DLCodeParent = t_Parent.DLCode
                    where (1=1)
	                    and t.DLCode in (@strDLCode_Root)
                    ;

                    declare @nDeepDealer int; select @nDeepDealer = 0;
                    while (@nDeepDealer <= 6)
                    begin
	                    select @nDeepDealer = @nDeepDealer + 1;
	
	                    update t
	                    set
		                    t.DLBUCode = IsNull(t_Parent.DLBUCode + '.', '') + t.DLCode
		                    , t.DLBUPattern = IsNull(t_Parent.DLBUCode + '.', '') + t.DLCode + '%'
		                    , t.DLLevel = IsNull(t_Parent.DLLevel, 0) + 1
	                    from Mst_Dealer t
		                    left join Mst_Dealer t_Parent
			                    on t.DLCodeParent = t_Parent.DLCode
	                    where (1=1)
		                    and t.DLCode not in (@strDLCode_Root)
	                    ;
                    end;
                ");
			DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
		}

		public DataSet RptSv_Mst_Dealer_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objDLCode
			, object objDLCodeParent
			, object objDLType
			, object objProvinceCode
			, object objDLName
			, object objDLAddress
			, object objDLPresentBy
			, object objDLGovIDNumber
			, object objDLEmail
			, object objDLPhoneNo
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_Dealer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
					, "objDLCode", objDLCode
					, "objDLCodeParent", objDLCodeParent
					, "objDLType", objDLType
					, "objProvinceCode", objProvinceCode
					, "objDLName", objDLName
					, "objDLAddress", objDLAddress
					, "objDLPresentBy", objDLPresentBy
					, "objDLGovIDNumber", objDLGovIDNumber
					, "objDLEmail", objDLEmail
					, "objDLPhoneNo", objDLPhoneNo
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
				RptSv_Sys_User_CheckAuthentication(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strDLCodeParent = TUtils.CUtils.StdParam(objDLCodeParent);
				string strDLType = TUtils.CUtils.StdParam(objDLType);
				string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
				string strDLName = string.Format("{0}", objDLName).Trim();
				string strDLAddress = string.Format("{0}", objDLAddress).Trim();
				string strDLPresentBy = string.Format("{0}", objDLPresentBy).Trim();
				string strDLGovIDNumber = string.Format("{0}", objDLGovIDNumber).Trim();
				string strDLEmail = string.Format("{0}", objDLEmail).Trim();
				string strDLPhoneNo = string.Format("{0}", objDLPhoneNo).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
				////
				DataTable dtDB_Mst_Dealer = null;
				DataTable dtDB_Mst_DealerParrent = null;
				{

					////
					if (strDLCode == null || strDLCode.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLCode", strDLCode
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Create_InvalidDLCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					Mst_Dealer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strDLCode // objDLCode
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
					////
					DataTable dtDB_Mst_DealerParent = null;
					Mst_Dealer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strDLCodeParent // objDLCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Mst_DealerParent // dtDB_Mst_Dealer
						);
					////
					if (strDLCodeParent != null && strDLCodeParent.Length > 0)
					{
						Mst_Dealer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strDLCodeParent // objDLCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_DealerParrent // dtDB_Mst_Dealer
						);
					}
					DataTable dtDB_Mst_Province = null;

					Mst_Province_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strProvinceCode // objDLCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_Province // dtDB_Mst_Dealer
						);
					////
					if (strDLName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLName", strDLName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Create_InvalidDLName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // SaveDB Mst_Dealer:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Dealer.NewRow();
					strFN = "DLCode"; drDB[strFN] = strDLCode;
					strFN = "DLCodeParent"; drDB[strFN] = strDLCodeParent;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "DLBUCode"; drDB[strFN] = "X";
					strFN = "DLBUPattern"; drDB[strFN] = "X";
					strFN = "DLLevel"; drDB[strFN] = 1;
					strFN = "DLType"; drDB[strFN] = strDLType;
					strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
					strFN = "DLName"; drDB[strFN] = strDLName;
					strFN = "DLAddress"; drDB[strFN] = strDLAddress;
					strFN = "DLPresentBy"; drDB[strFN] = strDLPresentBy;
					strFN = "DLGovIDNumber"; drDB[strFN] = strDLGovIDNumber;
					strFN = "DLEmail"; drDB[strFN] = strDLEmail;
					strFN = "DLPhoneNo"; drDB[strFN] = strDLPhoneNo;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_Mst_Dealer.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
						//, alColumnEffective.ToArray()
						);
				}
				#endregion

				#region // Post Save:
				{
					Mst_Dealer_UpdBU();
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

		public DataSet RptSv_Mst_Dealer_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objDLCode
			, object objProvinceCode
			, object objDLName
			, object objDLAddress
			, object objDLPresentBy
			, object objDLGovIDNumber
			, object objDLEmail
			, object objDLPhoneNo
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_Dealer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objDLCode", objDLCode
					, "objProvinceCode", objProvinceCode
					, "objDLName", objDLName
					, "objDLAddress", objDLAddress
					, "objDLPresentBy", objDLPresentBy
					, "objDLGovIDNumber", objDLGovIDNumber
					, "objDLEmail", objDLEmail
					, "objDLPhoneNo", objDLPhoneNo
					, "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
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
				RptSv_Sys_Access_CheckDeny(
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
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
				string strDLName = string.Format("{0}", objDLName).Trim();
				string strDLAddress = string.Format("{0}", objDLAddress).Trim();
				string strDLPresentBy = string.Format("{0}", objDLPresentBy).Trim();
				string strDLGovIDNumber = string.Format("{0}", objDLGovIDNumber).Trim();
				string strDLEmail = string.Format("{0}", objDLEmail).Trim();
				string strDLPhoneNo = string.Format("{0}", objDLPhoneNo).Trim();
				string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
				////
				bool bUpd_ProvinceCode = strFt_Cols_Upd.Contains("Mst_Dealer.ProvinceCode".ToUpper());
				bool bUpd_DLName = strFt_Cols_Upd.Contains("Mst_Dealer.DLName".ToUpper());
				bool bUpd_DLAddress = strFt_Cols_Upd.Contains("Mst_Dealer.DLAddress".ToUpper());
				bool bUpd_DLPresentBy = strFt_Cols_Upd.Contains("Mst_Dealer.DLPresentBy".ToUpper());
				bool bUpd_DLGovIDNumber = strFt_Cols_Upd.Contains("Mst_Dealer.DLGovIDNumber".ToUpper());
				bool bUpd_DLEmail = strFt_Cols_Upd.Contains("Mst_Dealer.DLEmail".ToUpper());
				bool bUpd_DLPhoneNo = strFt_Cols_Upd.Contains("Mst_Dealer.DLPhoneNo".ToUpper());
				bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Dealer.FlagActive".ToUpper());

				////
				DataTable dtDB_Mst_Dealer = null;
				{
					////
					Mst_Dealer_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strDLCode // objDLCode 
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
					////
					DataTable dtDB_Mst_Province = null;

					Mst_Province_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strProvinceCode // objstrProvinceCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , TConst.Flag.Yes // strFlagActiveListToCheck
						 , out dtDB_Mst_Province // dtDB_Mst_Province
						);
					////
					if (bUpd_DLName && string.IsNullOrEmpty(strDLName))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLName", strDLName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Update_InvalidDLName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // Save Mst_Dealer:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Dealer.Rows[0];
					if (bUpd_ProvinceCode) { strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode; alColumnEffective.Add(strFN); }
					if (bUpd_DLName) { strFN = "DLName"; drDB[strFN] = strDLName; alColumnEffective.Add(strFN); }
					if (bUpd_DLAddress) { strFN = "DLAddress"; drDB[strFN] = strDLAddress; alColumnEffective.Add(strFN); }
					if (bUpd_DLPresentBy) { strFN = "DLPresentBy"; drDB[strFN] = strDLPresentBy; alColumnEffective.Add(strFN); }
					if (bUpd_DLGovIDNumber) { strFN = "DLGovIDNumber"; drDB[strFN] = strDLGovIDNumber; alColumnEffective.Add(strFN); }
					if (bUpd_DLEmail) { strFN = "DLEmail"; drDB[strFN] = strDLEmail; alColumnEffective.Add(strFN); }
					if (bUpd_DLPhoneNo) { strFN = "DLPhoneNo"; drDB[strFN] = strDLPhoneNo; alColumnEffective.Add(strFN); }
					if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
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

		public DataSet Mst_Dealer_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			////
			, object objDLCode
			, object objProvinceCode
			, object objDLName
			, object objDLAddress
			, object objDLPresentBy
			, object objDLGovIDNumber
			, object objDLEmail
			, object objDLPhoneNo
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Dealer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objDLCode", objDLCode
					, "objProvinceCode", objProvinceCode
					, "objDLName", objDLName
					, "objDLAddress", objDLAddress
					, "objDLPresentBy", objDLPresentBy
					, "objDLGovIDNumber", objDLGovIDNumber
					, "objDLEmail", objDLEmail
					, "objDLPhoneNo", objDLPhoneNo
					, "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

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
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
				string strDLName = string.Format("{0}", objDLName).Trim();
				string strDLAddress = string.Format("{0}", objDLAddress).Trim();
				string strDLPresentBy = string.Format("{0}", objDLPresentBy).Trim();
				string strDLGovIDNumber = string.Format("{0}", objDLGovIDNumber).Trim();
				string strDLEmail = string.Format("{0}", objDLEmail).Trim();
				string strDLPhoneNo = string.Format("{0}", objDLPhoneNo).Trim();
				string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
				////
				bool bUpd_ProvinceCode = strFt_Cols_Upd.Contains("Mst_Dealer.ProvinceCode".ToUpper());
				bool bUpd_DLName = strFt_Cols_Upd.Contains("Mst_Dealer.DLName".ToUpper());
				bool bUpd_DLAddress = strFt_Cols_Upd.Contains("Mst_Dealer.DLAddress".ToUpper());
				bool bUpd_DLPresentBy = strFt_Cols_Upd.Contains("Mst_Dealer.DLPresentBy".ToUpper());
				bool bUpd_DLGovIDNumber = strFt_Cols_Upd.Contains("Mst_Dealer.DLGovIDNumber".ToUpper());
				bool bUpd_DLEmail = strFt_Cols_Upd.Contains("Mst_Dealer.DLEmail".ToUpper());
				bool bUpd_DLPhoneNo = strFt_Cols_Upd.Contains("Mst_Dealer.DLPhoneNo".ToUpper());
				bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Dealer.FlagActive".ToUpper());

				////
				DataTable dtDB_Mst_Dealer = null;
				{
					////
					Mst_Dealer_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strDLCode // objDLCode 
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
					////
					DataTable dtDB_Mst_Province = null;

					Mst_Province_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strProvinceCode // objstrProvinceCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , TConst.Flag.Yes // strFlagActiveListToCheck
						 , out dtDB_Mst_Province // dtDB_Mst_Province
						);
					////
					if (bUpd_DLName && string.IsNullOrEmpty(strDLName))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strDLName", strDLName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Dealer_Update_InvalidDLName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
				#endregion

				#region // Save Mst_Dealer:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Dealer.Rows[0];
					if (bUpd_ProvinceCode) { strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode; alColumnEffective.Add(strFN); }
					if (bUpd_DLName) { strFN = "DLName"; drDB[strFN] = strDLName; alColumnEffective.Add(strFN); }
					if (bUpd_DLAddress) { strFN = "DLAddress"; drDB[strFN] = strDLAddress; alColumnEffective.Add(strFN); }
					if (bUpd_DLPresentBy) { strFN = "DLPresentBy"; drDB[strFN] = strDLPresentBy; alColumnEffective.Add(strFN); }
					if (bUpd_DLGovIDNumber) { strFN = "DLGovIDNumber"; drDB[strFN] = strDLGovIDNumber; alColumnEffective.Add(strFN); }
					if (bUpd_DLEmail) { strFN = "DLEmail"; drDB[strFN] = strDLEmail; alColumnEffective.Add(strFN); }
					if (bUpd_DLPhoneNo) { strFN = "DLPhoneNo"; drDB[strFN] = strDLPhoneNo; alColumnEffective.Add(strFN); }
					if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
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

		public DataSet RptSv_Mst_Dealer_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			/////
			, object objDLCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_Dealer_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objDLCode", objDLCode
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
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				////
				DataTable dtDB_Mst_Dealer = null;
				{
					////
					Mst_Dealer_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strDLCode // objDLCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
				}
				#endregion

				#region // SaveDB Mst_Dealer:
				{
					// Init:
					dtDB_Mst_Dealer.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
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

		public DataSet Mst_Dealer_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			/////
			, object objDLCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Dealer_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objDLCode", objDLCode
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

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				////
				DataTable dtDB_Mst_Dealer = null;
				{
					////
					Mst_Dealer_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strDLCode // objDLCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_Mst_Dealer // dtDB_Mst_Dealer
						);
				}
				#endregion

				#region // SaveDB Mst_Dealer:
				{
					// Init:
					dtDB_Mst_Dealer.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"Mst_Dealer"
						, dtDB_Mst_Dealer
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

		public DataSet WAS_Mst_Dealer_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Get;
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

				List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				bool bGet_Mst_Dealer = (objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer != null && objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer.Length > 0);
				#endregion

				#region // WS_Mst_Dealer_Get:
				mdsResult = Mst_Dealer_Get(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, objRQ_Mst_Dealer.AccessToken // strAccessToken
					, objRQ_Mst_Dealer.NetworkID // strNetworkID
					, objRQ_Mst_Dealer.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Dealer.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Dealer.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Dealer.Ft_WhereClause // strFt_WhereClause
													  //// Return:
					, objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer // strRt_Cols_Mst_Dealer
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Dealer.MySummaryTable = lst_MySummaryTable[0];

					////
					if (bGet_Mst_Dealer)
					{
						////
						DataTable dt_Mst_Dealer = mdsResult.Tables["Mst_Dealer"].Copy();
						lst_Mst_Dealer = TUtils.DataTableCmUtils.ToListof<Mst_Dealer>(dt_Mst_Dealer);
						objRT_Mst_Dealer.Lst_Mst_Dealer = lst_Mst_Dealer;
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

		public DataSet WAS_RptSv_Mst_Dealer_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_Dealer_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Get;
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

				List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				bool bGet_Mst_Dealer = (objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer != null && objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer.Length > 0);
				#endregion

				#region // WS_Mst_Dealer_Get:
				mdsResult = RptSv_Mst_Dealer_Get(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Dealer.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Dealer.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Dealer.Ft_WhereClause // strFt_WhereClause
													  //// Return:
					, objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer // strRt_Cols_Mst_Dealer
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Dealer.MySummaryTable = lst_MySummaryTable[0];

					////
					if (bGet_Mst_Dealer)
					{
						////
						DataTable dt_Mst_Dealer = mdsResult.Tables["Mst_Dealer"].Copy();
						lst_Mst_Dealer = TUtils.DataTableCmUtils.ToListof<Mst_Dealer>(dt_Mst_Dealer);
						objRT_Mst_Dealer.Lst_Mst_Dealer = lst_Mst_Dealer;
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

		public DataSet WAS_Mst_Dealer_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Create:
				mdsResult = Mst_Dealer_Create(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, objRQ_Mst_Dealer.AccessToken // strAccessToken
					, objRQ_Mst_Dealer.NetworkID // strNetworkID
					, objRQ_Mst_Dealer.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
					, objRQ_Mst_Dealer.Mst_Dealer.ProvinceCode // objProvinceCode
					, objRQ_Mst_Dealer.Mst_Dealer.DLName // objDLName
					, objRQ_Mst_Dealer.Mst_Dealer.DLAddress // objDLAddress
					, objRQ_Mst_Dealer.Mst_Dealer.DLPresentBy // objDLPresentBy
					, objRQ_Mst_Dealer.Mst_Dealer.DLGovIDNumber // objDLGovIDNumber
					, objRQ_Mst_Dealer.Mst_Dealer.DLEmail // objDLEmail
					, objRQ_Mst_Dealer.Mst_Dealer.DLPhoneNo // objDLPhoneNo
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

		public DataSet WAS_RptSv_Mst_Dealer_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Create:
				mdsResult = RptSv_Mst_Dealer_Create(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
					, objRQ_Mst_Dealer.Mst_Dealer.DLCodeParent // objDLCodeParent
					, objRQ_Mst_Dealer.Mst_Dealer.DLType // objDLType
					, objRQ_Mst_Dealer.Mst_Dealer.ProvinceCode // objProvinceCode
					, objRQ_Mst_Dealer.Mst_Dealer.DLName // objDLName
					, objRQ_Mst_Dealer.Mst_Dealer.DLAddress // objDLAddress
					, objRQ_Mst_Dealer.Mst_Dealer.DLPresentBy // objDLPresentBy
					, objRQ_Mst_Dealer.Mst_Dealer.DLGovIDNumber // objDLGovIDNumber
					, objRQ_Mst_Dealer.Mst_Dealer.DLEmail // objDLEmail
					, objRQ_Mst_Dealer.Mst_Dealer.DLPhoneNo // objDLPhoneNo
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

		public DataSet WAS_Mst_Dealer_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Update:
				mdsResult = Mst_Dealer_Update(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, objRQ_Mst_Dealer.AccessToken // strAccessToken
					, objRQ_Mst_Dealer.NetworkID // strNetworkID
					, objRQ_Mst_Dealer.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
					, objRQ_Mst_Dealer.Mst_Dealer.ProvinceCode // objProvinceCode
					, objRQ_Mst_Dealer.Mst_Dealer.DLName // objDLName
					, objRQ_Mst_Dealer.Mst_Dealer.DLAddress // objDLAddress
					, objRQ_Mst_Dealer.Mst_Dealer.DLPresentBy // objDLPresentBy
					, objRQ_Mst_Dealer.Mst_Dealer.DLGovIDNumber // objDLGovIDNumber
					, objRQ_Mst_Dealer.Mst_Dealer.DLEmail // objDLEmail
					, objRQ_Mst_Dealer.Mst_Dealer.DLPhoneNo // objDLPhoneNo
					, objRQ_Mst_Dealer.Mst_Dealer.FlagActive // objFlagActive
															 ////
					, objRQ_Mst_Dealer.Ft_Cols_Upd// objFt_Cols_Upd
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

		public DataSet WAS_RptSv_Mst_Dealer_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Update:
				mdsResult = RptSv_Mst_Dealer_Update(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
					, objRQ_Mst_Dealer.Mst_Dealer.ProvinceCode // objProvinceCode
					, objRQ_Mst_Dealer.Mst_Dealer.DLName // objDLName
					, objRQ_Mst_Dealer.Mst_Dealer.DLAddress // objDLAddress
					, objRQ_Mst_Dealer.Mst_Dealer.DLPresentBy // objDLPresentBy
					, objRQ_Mst_Dealer.Mst_Dealer.DLGovIDNumber // objDLGovIDNumber
					, objRQ_Mst_Dealer.Mst_Dealer.DLEmail // objDLEmail
					, objRQ_Mst_Dealer.Mst_Dealer.DLPhoneNo // objDLPhoneNo
					, objRQ_Mst_Dealer.Mst_Dealer.FlagActive // objFlagActive
															 ////
					, objRQ_Mst_Dealer.Ft_Cols_Upd// objFt_Cols_Upd
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

		public DataSet WAS_Mst_Dealer_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Delete:
				mdsResult = Mst_Dealer_Delete(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, objRQ_Mst_Dealer.AccessToken // strAccessToken
					, objRQ_Mst_Dealer.NetworkID // strNetworkID
					, objRQ_Mst_Dealer.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
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

		public DataSet WAS_RptSv_Mst_Dealer_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Dealer objRQ_Mst_Dealer
			////
			, out RT_Mst_Dealer objRT_Mst_Dealer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Dealer.Tid;
			objRT_Mst_Dealer = new RT_Mst_Dealer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Dealer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Dealer_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Dealer_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer.Mst_Dealer)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_Dealer> lst_Mst_Dealer = new List<Mst_Dealer>();
				//List<Mst_DealerInGroup> lst_Mst_DealerInGroup = new List<Mst_DealerInGroup>();
				#endregion

				#region // Mst_Dealer_Delete:
				mdsResult = RptSv_Mst_Dealer_Delete(
					objRQ_Mst_Dealer.Tid // strTid
					, objRQ_Mst_Dealer.GwUserCode // strGwUserCode
					, objRQ_Mst_Dealer.GwPassword // strGwPassword
					, objRQ_Mst_Dealer.WAUserCode // strUserCode
					, objRQ_Mst_Dealer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Dealer.Mst_Dealer.DLCode // objDLCode
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

        #region // Mst_ColumnConfig:
        private void Mst_ColumnConfig_CheckDB_New20200220(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , object objTableName
            , object objColumnName
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_ColumnConfig
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_ColumnConfig t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.TableName = @objTableName
						and t.ColumnName = @objColumnName
					;
				");
            dtDB_Mst_ColumnConfig = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                , "@objTableName", objTableName
                , "@objColumnName", objColumnName
                ).Tables[0];
            dtDB_Mst_ColumnConfig.TableName = "Mst_ColumnConfig";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_ColumnConfig.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.TableName", objTableName
                        , "Check.ColumnName", objColumnName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfig_CheckDB_ColumnConfigNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_ColumnConfig.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.TableName", objTableName
                        , "Check.ColumnName", objColumnName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfig_CheckDB_ColumnConfigExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_ColumnConfig.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.TableName", objTableName
                    , "Check.ColumnName", objColumnName
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_ColumnConfig.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_ColumnConfig_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        public DataSet WAS_Mst_ColumnConfig_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig
            ////
            , out RT_Mst_ColumnConfig objRT_Mst_ColumnConfig
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfig.Tid;
            objRT_Mst_ColumnConfig = new RT_Mst_ColumnConfig();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfig.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfig_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfig_Get;
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

                List<Mst_ColumnConfig> lst_Mst_ColumnConfig = new List<Mst_ColumnConfig>();
                bool bGet_Mst_ColumnConfig = (objRQ_Mst_ColumnConfig.Rt_Cols_Mst_ColumnConfig != null && objRQ_Mst_ColumnConfig.Rt_Cols_Mst_ColumnConfig.Length > 0);
                #endregion

                #region // WS_Mst_ColumnConfig_Get:
                mdsResult = Mst_ColumnConfig_Get(
                    objRQ_Mst_ColumnConfig.Tid // strTid
                    , objRQ_Mst_ColumnConfig.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfig.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfig.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfig.WAUserPassword // strUserPassword
                    , objRQ_Mst_ColumnConfig.AccessToken
                    , objRQ_Mst_ColumnConfig.NetworkID
                    , objRQ_Mst_ColumnConfig.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Mst_ColumnConfig.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_ColumnConfig.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_ColumnConfig.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_ColumnConfig.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Mst_ColumnConfig.Rt_Cols_Mst_ColumnConfig // strRt_Cols_Mst_ColumnConfig
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    if (bGet_Mst_ColumnConfig)
                    {
                        ////
                        DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                        lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                        objRT_Mst_ColumnConfig.MySummaryTable = lst_MySummaryTable[0];

                        ////
                        DataTable dt_Mst_ColumnConfig = mdsResult.Tables["Mst_ColumnConfig"].Copy();
                        lst_Mst_ColumnConfig = TUtils.DataTableCmUtils.ToListof<Mst_ColumnConfig>(dt_Mst_ColumnConfig);
                        objRT_Mst_ColumnConfig.Lst_Mst_ColumnConfig = lst_Mst_ColumnConfig;
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

        public DataSet Mst_ColumnConfig_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_ColumnConfig
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_ColumnConfig_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfig_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_ColumnConfig", strRt_Cols_Mst_ColumnConfig
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

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                //// Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Mst_ColumnConfig = (strRt_Cols_Mst_ColumnConfig != null && strRt_Cols_Mst_ColumnConfig.Length > 0);

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
						---- #tbl_Mst_ColumnConfig_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mcc.TableName
                            , mcc.ColumnName
                            , mcc.OrgID
						into #tbl_Mst_ColumnConfig_Filter_Draft
						from Mst_ColumnConfig mcc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mcc.TableName asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_ColumnConfig_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_ColumnConfig_Filter:
						select
							t.*
						into #tbl_Mst_ColumnConfig_Filter
						from #tbl_Mst_ColumnConfig_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_ColumnConfig --------:
						zzB_Select_Mst_ColumnConfig_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_ColumnConfig_Filter_Draft;
						--drop table #tbl_Mst_ColumnConfig_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_ColumnConfig_zzE = "-- Nothing.";
                if (bGet_Mst_ColumnConfig)
                {
                    #region // bGet_Mst_ColumnConfig:
                    zzB_Select_Mst_ColumnConfig_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_ColumnConfig:
							select
								t.MyIdxSeq
								, mcc.*
							from #tbl_Mst_ColumnConfig_Filter t --//[mylock]
								inner join Mst_ColumnConfig mcc --//[mylock]
									on t.TableName = mcc.TableName
                                        and t.ColumnName = mcc.ColumnName
                                        and t.OrgID = mcc.OrgID
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
                            , "Mst_ColumnConfig" // strTableNameDB
                            , "Mst_ColumnConfig." // strPrefixStd
                            , "mcc." // strPrefixAlias
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
                    , "zzB_Select_Mst_ColumnConfig_zzE", zzB_Select_Mst_ColumnConfig_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_ColumnConfig)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_ColumnConfig";
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
        public DataSet WAS_Mst_ColumnConfig_Update_New20200220(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig
            ////
            , out RT_Mst_ColumnConfig objRT_Mst_ColumnConfig
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfig.Tid;
            objRT_Mst_ColumnConfig = new RT_Mst_ColumnConfig();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfig.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfig_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfig_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Mst_ColumnConfig", TJson.JsonConvert.SerializeObject(objRQ_Mst_ColumnConfig.Mst_ColumnConfig)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Mst_ColumnConfig.WAUserCode
                //    , objRQ_Mst_ColumnConfig.WAUserPassword
                //    );

                #endregion

                #region // Refine and Check Input:
                //List<Mst_ColumnConfig> lst_Mst_ColumnConfig = new List<Mst_ColumnConfig>();
                //List<Mst_ColumnConfigInGroup> lst_Mst_ColumnConfigInGroup = new List<Mst_ColumnConfigInGroup>();
                #endregion

                #region // Mst_ColumnConfig_Update:
                mdsResult = Mst_ColumnConfig_Update_New20200220(
                    objRQ_Mst_ColumnConfig.Tid // strTid
                    , objRQ_Mst_ColumnConfig.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfig.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfig.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfig.WAUserPassword // strUserPassword
                    , objRQ_Mst_ColumnConfig.AccessToken
                    , objRQ_Mst_ColumnConfig.NetworkID
                    , objRQ_Mst_ColumnConfig.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Mst_ColumnConfig.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_ColumnConfig.Mst_ColumnConfig.TableName // objTableName
                    , objRQ_Mst_ColumnConfig.Mst_ColumnConfig.ColumnName // objColumnName
                    , objRQ_Mst_ColumnConfig.Mst_ColumnConfig.ColumnFormat // objColumnFormat
                    , objRQ_Mst_ColumnConfig.Mst_ColumnConfig.ColumnDesc // objColumnDesc
                    , objRQ_Mst_ColumnConfig.Mst_ColumnConfig.FlagActive // objFlagActive
                                                                         ////
                    , objRQ_Mst_ColumnConfig.Ft_Cols_Upd// objFt_Cols_Upd
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

        public DataSet Mst_ColumnConfig_Update_New20200220(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objTableName
            , object objColumnName
            , object objColumnFormat
            , object objColumnDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ColumnConfig_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfig_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strOrgID", strOrgID
                , "objTableName", objTableName
                , "objColumnName", objColumnName
                , "objColumnFormat", objColumnFormat
                , "objColumnDesc", objColumnDesc
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
                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

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
                string strTableName = string.Format("{0}", objTableName).Trim();
                string strColumnName = string.Format("{0}", objColumnName).Trim();
                string strColumnFormat = string.Format("{0}", objColumnFormat).Trim();
                string strColumnDesc = string.Format("{0}", objColumnDesc).Trim();
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                //bool bUpd_TableName = strFt_Cols_Upd.Contains("Mst_ColumnConfig.TableName".ToUpper());
                //bool bUpd_ColumnName = strFt_Cols_Upd.Contains("Mst_ColumnConfig.ColumnName".ToUpper());
                bool bUpd_ColumnFormat = strFt_Cols_Upd.Contains("Mst_ColumnConfig.ColumnFormat".ToUpper());
                bool bUpd_ColumnDesc = strFt_Cols_Upd.Contains("Mst_ColumnConfig.ColumnDesc".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_ColumnConfig.FlagActive".ToUpper());

                ////
                DataTable dtDB_Mst_ColumnConfig = null;
                {
                    ////
                    Mst_ColumnConfig_CheckDB_New20200220(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgID // objOrgID
                        , strTableName // objTableName 
                        , strColumnName // objColumnName
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Mst_ColumnConfig // dtDB_Mst_ColumnConfig
                        );

                    ////
                    if (dtDB_Mst_ColumnConfig.Rows.Count <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strTableName", strTableName
                            , "Check.strColumnName", strColumnName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Mst_ColumnConfig_Update_InvalidTableName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    if (Convert.ToDouble(strColumnFormat) <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strTableName", strTableName
                            , "Check.strColumnName", strColumnFormat
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Mst_ColumnConfig_Update_InvalidColumnFormat
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB Mst_ColumnConfig:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Mst_ColumnConfig.Rows[0];
                    if (bUpd_ColumnFormat) { strFN = "ColumnFormat"; drDB[strFN] = strColumnFormat; alColumnEffective.Add(strFN); }
                    if (bUpd_ColumnDesc) { strFN = "ColumnDesc"; drDB[strFN] = strColumnDesc; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Mst_ColumnConfig"
                        , dtDB_Mst_ColumnConfig
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
        #endregion

        #region // Mst_ColumnConfigGroup:
        private void Mst_ColumnConfigGroup_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objColumnConfigGrpCode
            , object objOrgID
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_ColumnConfigGroup
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_ColumnConfigGroup t --//[mylock]
					where (1=1)
						and t.ColumnConfigGrpCode = @objColumnConfigGrpCode
                        and t.OrgID = @objOrgID
					;
				");
            dtDB_Mst_ColumnConfigGroup = _cf.db.ExecQuery(
                strSqlExec
                , "@objColumnConfigGrpCode", objColumnConfigGrpCode
                , "@objOrgID", objOrgID
                ).Tables[0];
            dtDB_Mst_ColumnConfigGroup.TableName = "Mst_ColumnConfigGroup";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_ColumnConfigGroup.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ColumnConfigGrpCode", objColumnConfigGrpCode
                        , "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_ColumnConfigGroup.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ColumnConfigGrpCode", objColumnConfigGrpCode
                        , "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_ColumnConfigGroup.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ColumnConfigGrpCode", objColumnConfigGrpCode
                    , "Check.OrgID", objOrgID
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_ColumnConfigGroup.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_ColumnConfigGroup_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Mst_ColumnConfigGroup_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup
            ////
            , out RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfigGroup.Tid;
            objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfigGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfigGroup_Get;
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
                List<Mst_ColumnConfigGroup> lst_Mst_ColumnConfigGroup = new List<Mst_ColumnConfigGroup>();
                #endregion

                #region // WS_Mst_ColumnConfigGroup_Get:
                mdsResult = Mst_ColumnConfigGroup_Get(
                    objRQ_Mst_ColumnConfigGroup.Tid // strTid
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfigGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfigGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_ColumnConfigGroup.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_ColumnConfigGroup.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_ColumnConfigGroup.Ft_WhereClause // strFt_WhereClause
                                                                 //// Return:
                    , objRQ_Mst_ColumnConfigGroup.Rt_Cols_Mst_ColumnConfigGroup // strRt_Cols_Mst_ColumnConfigGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Mst_ColumnConfigGroup = mdsResult.Tables["Mst_ColumnConfigGroup"].Copy();
                    lst_Mst_ColumnConfigGroup = TUtils.DataTableCmUtils.ToListof<Mst_ColumnConfigGroup>(dt_Mst_ColumnConfigGroup);
                    objRT_Mst_ColumnConfigGroup.Lst_Mst_ColumnConfigGroup = lst_Mst_ColumnConfigGroup;
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
        public DataSet Mst_ColumnConfigGroup_Get(
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
            , string strRt_Cols_Mst_ColumnConfigGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ColumnConfigGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfigGroup_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_ColumnConfigGroup", strRt_Cols_Mst_ColumnConfigGroup
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

                #region // Mst_ColumnConfigGroup_GetX:
                DataSet dsGetData = null;
                {
                    Mst_ColumnConfigGroup_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_ColumnConfigGroup // strRt_Cols_Mst_ColumnConfigGroup
                        , out dsGetData  // dsGetData
                        );
                }
                ////
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

        public DataSet WAS_Mst_ColumnConfigGroup_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup
            ////
            , out RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfigGroup.Tid;
            objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfigGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfigGroup_Create;
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
                List<Mst_ColumnConfigGroup> lst_Mst_ColumnConfigGroup = new List<Mst_ColumnConfigGroup>();
                #endregion

                #region // WS_Mst_ColumnConfigGroup_Get:
                mdsResult = Mst_ColumnConfigGroup_Create(
                    objRQ_Mst_ColumnConfigGroup.Tid // strTid
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfigGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfigGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnConfigGrpCode // objColumnConfigGrpCode
                                                                                            //, objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.OrgID // strOrgID
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpName // objColumnGrpName
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpFormat // objColumnGrpFormat
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpDesc // objColumnGrpDesc
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
        public DataSet Mst_ColumnConfigGroup_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objColumnConfigGrpCode
            //, object objOrgID
            , object objColumnGrpName
            , object objColumnGrpFormat
            , object objColumnGrpDesc
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ColumnConfigGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfigGroup_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnConfigGrpCode", objColumnConfigGrpCode
                    //, "objOrgID", objOrgID
                    , "objColumnGrpName", objColumnGrpName
                    , "objColumnGrpFormat", objColumnGrpFormat
                    , "objColumnGrpDesc", objColumnGrpDesc
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

                #region // Mst_ColumnConfigGroup_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_ColumnConfigGroup_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objColumnConfigGrpCode // objColumnConfigGrpCode
                                                 //, objOrgID
                        , objColumnGrpName // objColumnGrpName
                        , objColumnGrpFormat // objColumnGrpFormat
                        , objColumnGrpDesc // objColumnGrpDesc
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
        public DataSet WAS_Mst_ColumnConfigGroup_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup
            ////
            , out RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfigGroup.Tid;
            objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfigGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfigGroup_Update;
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
                List<Mst_ColumnConfigGroup> lst_Mst_ColumnConfigGroup = new List<Mst_ColumnConfigGroup>();
                #endregion

                #region // Mst_ColumnConfigGroup_Update:
                mdsResult = Mst_ColumnConfigGroup_Update(
                    objRQ_Mst_ColumnConfigGroup.Tid // strTid
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfigGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfigGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnConfigGrpCode // objColumnConfigGrpCode
                                                                                            //, objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.OrgID
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpName // objColumnGrpName
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpFormat // objColumnGrpFormat
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnGrpDesc // objColumnGrpDesc
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.FlagActive // objFlagActive
                                                                                   ////
                    , objRQ_Mst_ColumnConfigGroup.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet Mst_ColumnConfigGroup_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objColumnConfigGrpCode
            //, object objOrgID
            , object objColumnGrpName
            , object objColumnGrpFormat
            , object objColumnGrpDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ColumnConfigGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfigGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnConfigGrpCode", objColumnConfigGrpCode
                    //, "objOrgID", objOrgID
                    , "objColumnGrpName", objColumnGrpName
                    , "objColumnGrpFormat", objColumnGrpFormat
                    , "objColumnGrpDesc", objColumnGrpDesc
                    , "objFlagActive", objFlagActive
                    ////
                    , "objFt_Cols_Upd", objFt_Cols_Upd
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

                #region // Mst_ColumnConfigGroup_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_ColumnConfigGroup_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objColumnConfigGrpCode // objColumnConfigGrpCode
                                                 //, objOrgID
                        , objColumnGrpName // objColumnGrpName
                        , objColumnGrpFormat // objColumnGrpFormat
                        , objColumnGrpDesc // objColumnGrpDesc
                        , objFlagActive // objFlagActive
                                        ////
                        , objFt_Cols_Upd // objFt_Cols_Upd
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
        public DataSet WAS_Mst_ColumnConfigGroup_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup
            ////
            , out RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ColumnConfigGroup.Tid;
            objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ColumnConfigGroup_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ColumnConfigGroup_Delete;
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
                List<Mst_ColumnConfigGroup> lst_Mst_ColumnConfigGroup = new List<Mst_ColumnConfigGroup>();
                #endregion

                #region // Mst_ColumnConfigGroup_Delete:
                mdsResult = Mst_ColumnConfigGroup_Delete(
                    objRQ_Mst_ColumnConfigGroup.Tid // strTid
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ColumnConfigGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ColumnConfigGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.ColumnConfigGrpCode // objColumnConfigGrpCode
                                                                                            //, objRQ_Mst_ColumnConfigGroup.Mst_ColumnConfigGroup.OrgID
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
        public DataSet Mst_ColumnConfigGroup_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objColumnConfigGrpCode
            //, object objOrgID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ColumnConfigGroup_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ColumnConfigGroup_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objColumnConfigGrpCode", objColumnConfigGrpCode
                    //, "objOrgID", objOrgID
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

                #region // Mst_ColumnConfigGroup_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_ColumnConfigGroup_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objColumnConfigGrpCode // objColumnConfigGrpCode
                                                 //, objOrgID
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
        private void Mst_ColumnConfigGroup_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_ColumnConfigGroup
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_ColumnConfigGroup_GetX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_ColumnConfigGroup = (strRt_Cols_Mst_ColumnConfigGroup != null && strRt_Cols_Mst_ColumnConfigGroup.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
					---- #tbl_Mst_ColumnConfigGroup_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.ColumnConfigGrpCode
                        , mo.OrgID
					into #tbl_Mst_ColumnConfigGroup_Filter_Draft
					from Mst_ColumnConfigGroup mo --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.ColumnConfigGrpCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_ColumnConfigGroup_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_ColumnConfigGroup_Filter:
					select
						t.*
					into #tbl_Mst_ColumnConfigGroup_Filter
					from #tbl_Mst_ColumnConfigGroup_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_ColumnConfigGroup -----:
					zzB_Select_Mst_ColumnConfigGroup_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_ColumnConfigGroup_Filter_Draft;
					--drop table #tbl_Mst_ColumnConfigGroup_Filter;
					"
                );
            ////
            string zzB_Select_Mst_ColumnConfigGroup_zzE = "-- Nothing.";
            if (bGet_Mst_ColumnConfigGroup)
            {
                #region // bGet_Mst_ColumnConfigGroup:
                zzB_Select_Mst_ColumnConfigGroup_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_ColumnConfigGroup:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Mst_ColumnConfigGroup_Filter t --//[mylock]
						inner join Mst_ColumnConfigGroup mo --//[mylock]
							on t.ColumnConfigGrpCode = mo.ColumnConfigGrpCode
                                and t.OrgID = mo.OrgID
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
                        , "Mst_ColumnConfigGroup" // strTableNameDB
                        , "Mst_ColumnConfigGroup." // strPrefixStd
                        , "mo." // strPrefixAlias
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
                , "zzB_Select_Mst_ColumnConfigGroup_zzE", zzB_Select_Mst_ColumnConfigGroup_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_ColumnConfigGroup)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_ColumnConfigGroup";
            }
            #endregion
        }
        private void Mst_ColumnConfigGroup_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objColumnConfigGrpCode
            //, object objOrgID
            , object objColumnGrpName
            , object objColumnGrpFormat
            , object objColumnGrpDesc
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_ColumnConfigGroup_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objColumnConfigGrpCode", objColumnConfigGrpCode
                , "objColumnGrpName", objColumnGrpName
                , "objColumnGrpFormat", objColumnGrpFormat
                , "objColumnGrpDesc", objColumnGrpDesc
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strColumnConfigGrpCode = TUtils.CUtils.StdParam(objColumnConfigGrpCode);
            string strOrgID = null;
            string strColumnGrpName = string.Format("{0}", objColumnGrpName);
            string strColumnGrpFormat = TUtils.CUtils.StdParam(objColumnGrpFormat);
            string strColumnGrpDesc = string.Format("{0}", objColumnGrpDesc);

            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            strOrgID = drAbilityOfUser["OrgID"].ToString();
            ////
            DataTable dtDB_Mst_ColumnConfigGroup = null;
            {

                ////
                if (strColumnConfigGrpCode == null || strColumnConfigGrpCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strColumnConfigGrpCode", strColumnConfigGrpCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_Create_InvalidColumnConfigGrpCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_ColumnConfigGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strColumnConfigGrpCode // objColumnConfigGrpCode
                    , strOrgID
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_ColumnConfigGroup // dtDB_Mst_ColumnConfigGroup
                    );

                if (string.IsNullOrEmpty(strOrgID) || strOrgID.Length < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strColumnConfigGrpCode", strColumnConfigGrpCode
                        , "Check.strOrgID", strOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_Create_Invalid_OrgID_NotNull
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (Convert.ToDouble(strColumnGrpFormat) < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strColumnGrpFormat", strColumnGrpFormat
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_Create_Invalid_GrpFormat
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // SaveDB Mst_ColumnConfigGroup:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_ColumnConfigGroup.NewRow();
                strFN = "ColumnConfigGrpCode"; drDB[strFN] = strColumnConfigGrpCode;
                strFN = "OrgID"; drDB[strFN] = strOrgID;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "ColumnGrpName"; drDB[strFN] = strColumnGrpName;
                strFN = "ColumnGrpFormat"; drDB[strFN] = strColumnGrpFormat;
                strFN = "ColumnGrpDesc"; drDB[strFN] = strColumnGrpDesc;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_ColumnConfigGroup.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_ColumnConfigGroup" // strTableName
                    , dtDB_Mst_ColumnConfigGroup // dtData
                                                 //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }
        private void Mst_ColumnConfigGroup_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
           //// 
           , object objColumnConfigGrpCode
            //, object objOrgID
            , object objColumnGrpName
            , object objColumnGrpFormat
            , object objColumnGrpDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_ColumnConfigGroup_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objColumnConfigGrpCode", objColumnConfigGrpCode
                , "objColumnGrpName", objColumnGrpName
                , "objColumnGrpFormat", objColumnGrpFormat
                , "objColumnGrpDesc", objColumnGrpDesc
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            #region // Refine and Check Input:
            ////
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            string strOrgID = drAbilityOfUser["OrgID"].ToString();
            ////
            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
            ////
            string strColumnConfigGrpCode = TUtils.CUtils.StdParam(objColumnConfigGrpCode);
            string strColumnGrpName = string.Format("{0}", objColumnGrpName);
            string strColumnGrpFormat = TUtils.CUtils.StdParam(objColumnGrpFormat);
            string strColumnGrpDesc = string.Format("{0}", objColumnGrpDesc);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_ColumnGrpName = strFt_Cols_Upd.Contains("Mst_ColumnConfigGroup.ColumnGrpName".ToUpper());
            bool bUpd_ColumnGrpFormat = strFt_Cols_Upd.Contains("Mst_ColumnConfigGroup.ColumnGrpFormat".ToUpper());
            bool bUpd_ColumnGrpDesc = strFt_Cols_Upd.Contains("Mst_ColumnConfigGroup.ColumnGrpDesc".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_ColumnConfigGroup.FlagActive".ToUpper());
            ////

            ////
            DataTable dtDB_Mst_ColumnConfigGroup = null;
            {
                ////
                Mst_ColumnConfigGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strColumnConfigGrpCode // strColumnConfigGrpCode 
                    , strOrgID
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_ColumnConfigGroup // dtDB_Mst_ColumnConfigGroup
                    );
                ////
                if (Convert.ToDouble(strColumnGrpFormat) < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strColumnConfigGrpCode", strColumnGrpFormat
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ColumnConfigGroup_Update_Invalid_GrpFormat
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Save Mst_ColumnConfigGroup:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_ColumnConfigGroup.Rows[0];
                if (bUpd_ColumnGrpName) { strFN = "ColumnGrpName"; drDB[strFN] = strColumnGrpName; alColumnEffective.Add(strFN); }
                if (bUpd_ColumnGrpFormat) { strFN = "ColumnGrpFormat"; drDB[strFN] = strColumnGrpFormat; alColumnEffective.Add(strFN); }
                if (bUpd_ColumnGrpDesc) { strFN = "ColumnGrpDesc"; drDB[strFN] = strColumnGrpDesc; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_ColumnConfigGroup"
                    , dtDB_Mst_ColumnConfigGroup
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

            #region // Save Mst_ColumnConfig:
            {
                string strSql = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_Mst_ColumnConfig:
                        select
	                        t.*
                        into #tbl_Mst_ColumnConfig
                        from Mst_ColumnConfig t --//[mylock]
                        where (1=1)
	                        and t.ColumnConfigGrpCode = '@strColumnConfigGrpCode'
                            and t.OrgID = '@strOrgID'
                        ;

                        ---- Upd Mst_ColumnConfig:
                        update t
                        set
                            t.ColumnFormat = '@strColumnFormat'
                        from Mst_ColumnConfig t --//[mylock]
                            inner join #tbl_Mst_ColumnConfig f --//[mylock]
                                on t.OrgID = f.OrgID
                                    and t.TableName = f.TableName
                                    and t.ColumnName = f.ColumnName
                        where (1=1)
                        ;
                    "
                    , "@strColumnConfigGrpCode", strColumnConfigGrpCode
                    , "@strColumnFormat", strColumnGrpFormat
                    , "@strOrgID", strOrgID
                    );

                _cf.db.ExecQuery(strSql);
            }
            #endregion
        }
        private void Mst_ColumnConfigGroup_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objColumnConfigGrpCode
            //, object objOrgID
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_ColumnConfigGroup_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objColumnConfigGrpCode", objColumnConfigGrpCode
                //, "objOrgID", objOrgID
                });
            #endregion

            #region // Refine and Check Input:
            ////
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            string strOrgID = drAbilityOfUser["OrgID"].ToString();
            ////
            string strColumnConfigGrpCode = TUtils.CUtils.StdParam(objColumnConfigGrpCode);
            //string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            ////
            DataTable dtDB_Mst_ColumnConfigGroup = null;
            {
                ////
                Mst_ColumnConfigGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strColumnConfigGrpCode // strColumnConfigGrpCode 
                    , strOrgID
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_ColumnConfigGroup // dtDB_Mst_ColumnConfigGroup
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_ColumnConfigGroup.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_ColumnConfigGroup"
                    , dtDB_Mst_ColumnConfigGroup
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_Sys_Config:
        private void Mst_Sys_Config_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objSysConfigID
            , object objOrgID
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Sys_Config
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Sys_Config t --//[mylock]
					where (1=1)
						and t.SysConfigID = @objSysConfigID
                        and t.OrgID = @objOrgID
					;
				");
            dtDB_Mst_Sys_Config = _cf.db.ExecQuery(
                strSqlExec
                , "@objSysConfigID", objSysConfigID
                , "@objOrgID", objOrgID
                ).Tables[0];
            dtDB_Mst_Sys_Config.TableName = "Mst_Sys_Config";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Sys_Config.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.SysConfigID", objSysConfigID
                        , "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Sys_Config_CheckDB_SysConfigNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Sys_Config.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.SysConfigID", objSysConfigID
                        , "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Sys_Config_CheckDB_SysConfigExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Sys_Config.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.SysConfigID", objSysConfigID
                    , "Check.OrgID", objOrgID
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Sys_Config.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Sys_Config_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        public DataSet WAS_Mst_Sys_Config_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Sys_Config objRQ_Mst_Sys_Config
            ////
            , out RT_Mst_Sys_Config objRT_Mst_Sys_Config
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Sys_Config.Tid;
            objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Sys_Config.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Sys_Config_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Sys_Config_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "Lst_Mst_Sys_Config", TJson.JsonConvert.SerializeObject(objRQ_Mst_Sys_Config.Lst_Mst_Sys_Config)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_Sys_Config> lst_Mst_Sys_Config = new List<Mst_Sys_Config>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Mst_Sys_Config.Lst_Mst_Sys_Config != null)
                    {
                        DataTable dt_Mst_Sys_Config = TUtils.DataTableCmUtils.ToDataTable<Mst_Sys_Config>(objRQ_Mst_Sys_Config.Lst_Mst_Sys_Config, "Mst_Sys_Config");
                        dsData.Tables.Add(dt_Mst_Sys_Config);
                    }
                }
                #endregion

                #region // Mst_Sys_Config_Update:
                mdsResult = Mst_Sys_Config_Update(
                    objRQ_Mst_Sys_Config.Tid // strTid
                    , objRQ_Mst_Sys_Config.GwUserCode // strGwUserCode
                    , objRQ_Mst_Sys_Config.GwPassword // strGwPassword
                    , objRQ_Mst_Sys_Config.WAUserCode // strUserCode
                    , objRQ_Mst_Sys_Config.WAUserPassword // strUserPassword
                    , objRQ_Mst_Sys_Config.AccessToken
                    , objRQ_Mst_Sys_Config.NetworkID
                    , objRQ_Mst_Sys_Config.OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData // dsData
                    , objRQ_Mst_Sys_Config.Ft_Cols_Upd // objFt_Cols_Upd
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

        public DataSet Mst_Sys_Config_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            ////
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Sys_Config_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Sys_Config_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objFt_Cols_Upd", objFt_Cols_Upd
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                ////
                Sys_User_CheckAuthorize(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strAccessToken // strAccessToken
                    , strNetworkID // strNetworkID
                    , strOrgID // strOrgID
                    , TConst.Flag.Active // strFlagUserCodeToCheck
                    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_Sys_Config_UpdateX:
                //DataSet dsGetData = null;
                {

                    Mst_Sys_Config_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dsData // dsData
                                 ////
                        , objFt_Cols_Upd // objFt_Cols_Upd
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

        private void Mst_Sys_Config_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Mst_Sys_Config_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Sys_Config_SaveAllX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);

            ////
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Sys_Config.FlagActive".ToUpper());
            #endregion

            #region // Refine and Check Input Mst_Sys_Config:
            DataTable dtInput_Mst_Sys_Config = null;
            ////
            {
                ////
                string strTableCheck = "Mst_Sys_Config";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Sys_Config = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_Sys_Config.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Sys_Config // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "SysConfigID" // arrstrCouple
                    , "", "FlagActive" // arrstrCouple
                    );

                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Sys_Config, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Sys_Config, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_Sys_Config.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Sys_Config.Rows[nScan];

                    ////
                    DataTable dtDB_Mst_Sys_Config = null;

                    Mst_Sys_Config_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , drScan["SysConfigID"] // objSysConfigID
                        , drScan["OrgID"] // objOrgID
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Mst_Sys_Config // dtDB_Mst_Sys_Config
                        );
                    ////

                    //drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                }
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Sys_Config" // strTableName
                    , new object[] {
                            "SysConfigID", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType,
                            "FlagActive", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                        } // arrSingleStructure
                    , dtInput_Mst_Sys_Config // dtData
                    );
                ////
            }
            #endregion

            #region // SaveDB Mst_Sys_Config:
            {
                ////
                string zzB_Update_Mst_Sys_Config_ClauseSet_zzE = @"
								t.LogLUDTimeUTC = f.LogLUDTimeUTC
								, t.LogLUBy = f.LogLUBy
							";
                if (bUpd_FlagActive) zzB_Update_Mst_Sys_Config_ClauseSet_zzE += ", t.FlagActive = f.FlagActive";
                ////
                string zzB_Update_Mst_Sys_Config_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Sys_Config:
							update t
							set 
								zzB_Update_Mst_Sys_Config_ClauseSet_zzE
							from Mst_Sys_Config t --//[mylock]
								inner join #input_Mst_Sys_Config f --//[mylock]
									on t.OrgID = f.OrgID
									    and t.SysConfigID = f.SysConfigID
							where (1=1)
							;
							
						"
                    , "zzB_Update_Mst_Sys_Config_ClauseSet_zzE", zzB_Update_Mst_Sys_Config_ClauseSet_zzE
                    );
                ////
                string strSql_Exec = CmUtils.StringUtils.Replace(@"
							----
							zzB_Update_Mst_Sys_Config_zzE
							----
						"
                    , "zzB_Update_Mst_Sys_Config_zzE", zzB_Update_Mst_Sys_Config_zzE
                    );
                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_Exec
                    );
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Sys_Config;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            //MyCodeLabel_Done:
            return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }
        public DataSet WAS_Mst_Sys_Config_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Sys_Config objRQ_Mst_Sys_Config
            ////
            , out RT_Mst_Sys_Config objRT_Mst_Sys_Config
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Sys_Config.Tid;
            objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Sys_Config.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Sys_Config_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Sys_Config_Get;
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
                List<Mst_Sys_Config> lst_Mst_Sys_Config = new List<Mst_Sys_Config>();
                #endregion

                #region // WS_Mst_Sys_Config_Get:
                mdsResult = Mst_Sys_Config_Get(
                    objRQ_Mst_Sys_Config.Tid // strTid
                    , objRQ_Mst_Sys_Config.GwUserCode // strGwUserCode
                    , objRQ_Mst_Sys_Config.GwPassword // strGwPassword
                    , objRQ_Mst_Sys_Config.WAUserCode // strUserCode
                    , objRQ_Mst_Sys_Config.WAUserPassword // strUserPassword
                    , objRQ_Mst_Sys_Config.AccessToken
                    , objRQ_Mst_Sys_Config.NetworkID
                    , objRQ_Mst_Sys_Config.OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Sys_Config.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Sys_Config.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Sys_Config.Ft_WhereClause // strFt_WhereClause
                                                          //// Return:
                    , objRQ_Mst_Sys_Config.Rt_Cols_Mst_Sys_Config // strRt_Cols_Mst_Sys_Config
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Mst_Sys_Config = mdsResult.Tables["Mst_Sys_Config"].Copy();
                    lst_Mst_Sys_Config = TUtils.DataTableCmUtils.ToListof<Mst_Sys_Config>(dt_Mst_Sys_Config);
                    objRT_Mst_Sys_Config.Lst_Mst_Sys_Config = lst_Mst_Sys_Config;
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

        public DataSet Mst_Sys_Config_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Sys_Config
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Sys_Config_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Sys_Config_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_Sys_Config", strRt_Cols_Mst_Sys_Config
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                ////
                Sys_User_CheckAuthorize(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strAccessToken // strAccessToken
                    , strNetworkID // strNetworkID
                    , strOrgID // strOrgID
                    , TConst.Flag.Active // strFlagUserCodeToCheck
                    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Mst_Sys_Config_GetX:
                DataSet dsGetData = null;
                {
                    Mst_Sys_Config_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_Sys_Config // strRt_Cols_Mst_Sys_Config
                        , out dsGetData  // dsGetData
                        );
                }
                ////
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

        private void Mst_Sys_Config_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Sys_Config
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_Sys_Config_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_Sys_Config = (strRt_Cols_Mst_Sys_Config != null && strRt_Cols_Mst_Sys_Config.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

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
					---- #tbl_Mst_Sys_Config_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, msc.SysConfigID
                        , msc.OrgID
					into #tbl_Mst_Sys_Config_Filter_Draft
					from Mst_Sys_Config msc --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by msc.SysConfigID asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_Sys_Config_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_Sys_Config_Filter:
					select
						t.*
					into #tbl_Mst_Sys_Config_Filter
					from #tbl_Mst_Sys_Config_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_Sys_Config -----:
					zzB_Select_Mst_Sys_Config_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_Sys_Config_Filter_Draft;
					--drop table #tbl_Mst_Sys_Config_Filter;
					"
                );
            ////
            string zzB_Select_Mst_Sys_Config_zzE = "-- Nothing.";
            if (bGet_Mst_Sys_Config)
            {
                #region // bGet_Mst_Sys_Config:
                zzB_Select_Mst_Sys_Config_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_Sys_Config:
					select
						t.MyIdxSeq
						, msc.*
					from #tbl_Mst_Sys_Config_Filter t --//[mylock]
						inner join Mst_Sys_Config msc --//[mylock]
							on t.SysConfigID = msc.SysConfigID
                                and t.OrgID = msc.OrgID
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
                        , "Mst_Sys_Config" // strTableNameDB
                        , "Mst_Sys_Config." // strPrefixStd
                        , "msc." // strPrefixAlias
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
                , "zzB_Select_Mst_Sys_Config_zzE", zzB_Select_Mst_Sys_Config_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_Sys_Config)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_Sys_Config";
            }
            #endregion
        }
        #endregion
    }
}
