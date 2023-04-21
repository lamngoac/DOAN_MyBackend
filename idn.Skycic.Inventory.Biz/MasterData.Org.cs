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



namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Mst_Org:
        private void Mst_Org_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Org
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Org t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
					;
				");
            dtDB_Mst_Org = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                ).Tables[0];
            dtDB_Mst_Org.TableName = "Mst_Org";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Org.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Org_CheckDB_OrgNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Org.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Org_CheckDB_OrgExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Org.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Org.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Org_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Mst_Org_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objOrgID
            , object objOrgParent
			, object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            dtimeSys = DateTime.Now;
            string strFunctionName = "Mst_Org_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Org_Create;
            alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objOrgID", objOrgID
                    , "objOrgParent", objOrgParent
                    //, "objOrgLevel", objOrgLevel
                    , "objRemark", objRemark
                    //, "objFlagActive", objFlagActive
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
                string strOrgID = TUtils.CUtils.StdParam(objOrgID);
                string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
				//string strOrgLevel = String.Format("{0}", objOrgLevel).Trim();
				string strRemark = String.Format("{0}", objRemark).Trim();
                ////
                DataTable dtDB_Mst_Org = null;
                DataTable dtDB_Mst_OrgParrent = null;
                {
                    ////
                    if (strOrgID == null || strOrgID.Length <1)
                    {
                        alParamsCoupleError.AddRange(new object[] {
                            "Check.strOrgID", strOrgID
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Mst_Org_Create_InvalidOrgID
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Mst_Org_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgID // strOrgID
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Mst_Org // dtDB_Mst_Org
                        );
                    ////
                    DataTable dtDB_Mst_OrgParent = null;
                    Mst_Org_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgParent // strOrgParent
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Mst_OrgParent // dtDB_Mst_Org
                        );
                    ////
                    if (strOrgParent != null && strOrgParent.Length > 0)
                    {
                        Mst_Org_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgParent // strOrgParent
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_OrgParrent // dtDB_Mst_Org
                        );
                    }
                    ////
                }
                #endregion

                #region // SaveDB Mst_Org:
                {
                    // Init:
                    // ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Mst_Org.NewRow();
                    strFN = "OrgID"; drDB[strFN] = strOrgID;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "OrgParent"; drDB[strFN] = strOrgParent;
                    strFN = "OrgBUCode"; drDB[strFN] = "X";
                    strFN = "OrgBUPattern"; drDB[strFN] = "X";
                    strFN = "OrgLevel"; drDB[strFN] = 1;
                    strFN = "Remark"; drDB[strFN] = strRemark;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Mst_Org.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Mst_Org"
                        , dtDB_Mst_Org
                        );
                }
                #endregion

                #region // Post Save:
                {
                    Mst_Org_UpdBU();
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

		public DataSet Mst_Org_Create_New20200208(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objOrgParent
			, object objOrgIDSln
			, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			dtimeSys = DateTime.Now;
			string strFunctionName = "Mst_Org_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Org_Create;
			alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "objOrgID", objOrgID
					, "objOrgParent", objOrgParent
                    //, "objOrgLevel", objOrgLevel
                    , "objRemark", objRemark
                    //, "objFlagActive", objFlagActive
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
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);
				string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				//string strOrgLevel = String.Format("{0}", objOrgLevel).Trim();
				string strRemark = String.Format("{0}", objRemark).Trim();
				////
				DataTable dtDB_Mst_Org = null;
				DataTable dtDB_Mst_OrgParrent = null;
				{
					////
					if (strOrgID == null || strOrgID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[] {
							"Check.strOrgID", strOrgID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Org_Create_InvalidOrgID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					Mst_Org_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Mst_Org // dtDB_Mst_Org
						);
					////
					DataTable dtDB_Mst_OrgParent = null;
					Mst_Org_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgParent // strOrgParent
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Mst_OrgParent // dtDB_Mst_Org
						);
					////
					if (strOrgParent != null && strOrgParent.Length > 0)
					{
						Mst_Org_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgParent // strOrgParent
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_OrgParrent // dtDB_Mst_Org
						);
					}
					////
				}
				#endregion

				#region // SaveDB Mst_Org:
				{
					// Init:
					// ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_Mst_Org.NewRow();
					strFN = "OrgID"; drDB[strFN] = strOrgID;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "OrgParent"; drDB[strFN] = strOrgParent;
					strFN = "OrgBUCode"; drDB[strFN] = "X";
					strFN = "OrgBUPattern"; drDB[strFN] = "X";
					strFN = "OrgLevel"; drDB[strFN] = 1;
					strFN = "Remark"; drDB[strFN] = strRemark;
					strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_Mst_Org.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"Mst_Org"
						, dtDB_Mst_Org
						);
				}
				#endregion

				#region // Post Save:
				{
					Mst_Org_UpdBU();
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

		public void Mst_Org_Create_X(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objOrgParent
			, object objOrgIDSln
			//, object objRemark
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			dtimeSys = DateTime.Now;
			string strFunctionName = "Mst_Org_Create";
			//string strErrorCodeDefault = TError.ErridQContract.Mst_Org_Create;
			alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "objOrgID", objOrgID
					, "objOrgParent", objOrgParent
                    //, "objOrgLevel", objOrgLevel
                    //, "objRemark", objRemark
                    //, "objFlagActive", objFlagActive
                    });
			#endregion

			#region // Refine and Check Input:
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strOrgParent = TUtils.CUtils.StdParam(objOrgParent);
			string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
			//string strOrgLevel = String.Format("{0}", objOrgLevel).Trim();
			string strRemark = "";
			////
			DataTable dtDB_Mst_Org = null;
			DataTable dtDB_Mst_OrgParrent = null;
			{
				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[] {
							"Check.strOrgID", strOrgID
							});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Org_Create_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // strOrgID
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Org // dtDB_Mst_Org
					);
				////
				DataTable dtDB_Mst_OrgParent = null;
				Mst_Org_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgParent // strOrgParent
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_OrgParent // dtDB_Mst_Org
					);
				////
				if (strOrgParent != null && strOrgParent.Length > 0)
				{
					Mst_Org_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgParent // strOrgParent
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_OrgParrent // dtDB_Mst_Org
						);
				}
				////
			}
			#endregion

			#region // SaveDB Mst_Org:
			{
				// Init:
				// ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Org.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "OrgParent"; drDB[strFN] = strOrgParent;
				strFN = "OrgBUCode"; drDB[strFN] = "X";
				strFN = "OrgBUPattern"; drDB[strFN] = "X";
				strFN = "OrgLevel"; drDB[strFN] = 1;
				strFN = "Remark"; drDB[strFN] = strRemark;
				strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Org.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Org"
					, dtDB_Mst_Org
					);
			}
			#endregion

			#region // Post Save:
			{
				Mst_Org_UpdBU();
			}
			#endregion
		}
		public void Mst_Org_UpdBU()
        {
            string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strOrgID_Root nvarchar(100); select @strOrgID_Root = '0';

                    update t
                    set
	                    t.OrgBUCode = @strOrgID_Root
	                    , t.OrgBUPattern = @strOrgID_Root + '%'
	                    , t.OrgLevel = 1
                    from Mst_Org t
	                    left join Mst_Org t_Parent
		                    on t.OrgParent = t_Parent.OrgID
                    where (1=1)
	                    and t.OrgID in (@strOrgID_Root)
                    ;

                    declare @nDeepOrg int; select @nDeepOrg = 0;
                    while (@nDeepOrg <= 6)
                    begin
	                    select @nDeepOrg = @nDeepOrg + 1;
	
	                    update t
	                    set
		                    t.OrgBUCode = IsNull(t_Parent.OrgBUCode + '.', '') + t.OrgID
		                    , t.OrgBUPattern = IsNull(t_Parent.OrgBUCode + '.', '') + t.OrgID + '%'
		                    , t.OrgLevel = IsNull(t_Parent.OrgLevel, 0) + 1
	                    from Mst_Org t
		                    left join Mst_Org t_Parent
			                    on t.OrgParent = t_Parent.OrgID
	                    where (1=1)
		                    and t.OrgID not in (@strOrgID_Root)
	                    ;
                    end;
                ");
            DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
        }

		private void Mst_Org_GetX(
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
			, string strRt_Cols_Mst_Org
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Org_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Org", strRt_Cols_Mst_Org
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_Org = (strRt_Cols_Mst_Org != null && strRt_Cols_Mst_Org.Length > 0);

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
						---- #tbl_Mst_Org_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mo.OrgID
						into #tbl_Mst_Org_Filter_Draft
						from Mst_Org mo --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mo.OrgID asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Org_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Org_Filter:
						select
							t.*
						into #tbl_Mst_Org_Filter
						from #tbl_Mst_Org_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Org --------:
						zzB_Select_Mst_Org_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Org_Filter_Draft;
						--drop table #tbl_Mst_Org_Filter;
					"
				);
			////
			string zzB_Select_Mst_Org_zzE = "-- Nothing.";
			if (bGet_Mst_Org)
			{
				#region // bGet_Mst_Org:
				zzB_Select_Mst_Org_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Org:
                        select
                            t.MyIdxSeq
	                        , mo.*
                        from #tbl_Mst_Org_Filter t --//[mylock]
	                        inner join Mst_Org mo --//[mylock]
		                        on t.OrgID = mo.OrgID
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
						, "Mst_Org" // strTableNameDB
						, "Mst_Org." // strPrefixStd
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
				, "zzB_Select_Mst_Org_zzE", zzB_Select_Mst_Org_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Org)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Org";
			}
			#endregion
		}

		public DataSet Mst_Org_Get(
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
			, string strRt_Cols_Mst_Org
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_Org_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Org_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Org", strRt_Cols_Mst_Org
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

				#region // Mst_Org_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_Org_GetX(
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
						, strRt_Cols_Mst_Org // strRt_Cols_Mst_Org
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

		public DataSet WAS_Mst_Org_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Org objRQ_Mst_Org
			////
			, out RT_Mst_Org objRT_Mst_Org
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Org.Tid;
			objRT_Mst_Org = new RT_Mst_Org();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Org_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Org_Get;
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
				List<Mst_Org> lst_Mst_Org = new List<Mst_Org>();
				#endregion

				#region // WAS_Mst_Org_Get:
				mdsResult = Mst_Org_Get(
					objRQ_Mst_Org.Tid // strTid
					, objRQ_Mst_Org.GwUserCode // strGwUserCode
					, objRQ_Mst_Org.GwPassword // strGwPassword
					, objRQ_Mst_Org.WAUserCode // strUserCode
					, objRQ_Mst_Org.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					//// Filter:
					, objRQ_Mst_Org.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Org.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Org.Ft_WhereClause // strFt_WhereClause
					//// Return:
					, objRQ_Mst_Org.Rt_Cols_Mst_Org // strRt_Cols_Mst_Org
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Org.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_Org = mdsResult.Tables["Mst_Org"].Copy();
					lst_Mst_Org = TUtils.DataTableCmUtils.ToListof<Mst_Org>(dt_Mst_Org);
					objRT_Mst_Org.Lst_Mst_Org = lst_Mst_Org;
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
	}
}
