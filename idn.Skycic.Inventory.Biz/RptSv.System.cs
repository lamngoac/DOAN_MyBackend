using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TConst = idn.Skycic.Inventory.Constants;
using TDALUtils = EzDAL.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;
using TUtils = idn.Skycic.Inventory.Utils;

using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
        #region // RptSv_Sys_User:
        #endregion

        #region // RptSv_Sys_Group:
        private void RptSv_Sys_Group_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strGroupCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_RptSv_Sys_Group
            )
        {
            // GetInfo:
            dtDB_RptSv_Sys_Group = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "RptSv_Sys_Group" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "GroupCode", "=", strGroupCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_RptSv_Sys_Group.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GroupCodeNotFound", strGroupCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.RptSv_Sys_Group_CheckDB_GroupCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_RptSv_Sys_Group.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GroupCodeExist", strGroupCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.RptSv_Sys_Group_CheckDB_GroupCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_RptSv_Sys_Group.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.GroupCode", strGroupCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_RptSv_Sys_Group.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_Group_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }

        public DataSet RptSv_Sys_Group_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_RptSv_Sys_Group
            , string strRt_Cols_RptSv_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_Group", strRt_Cols_RptSv_Sys_Group
                    , "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_RptSv_Sys_Group = (strRt_Cols_RptSv_Sys_Group != null && strRt_Cols_RptSv_Sys_Group.Length > 0);
                bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

                // drAbilityOfGroup:
                //DataRow drAbilityOfGroup = myRptSv_Sys_Group_GetAbilityViewBankOfGroup(_cf.sinf.strGroupCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfGroup", drAbilityOfGroup["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_Group_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, rsg.GroupCode
						into #tbl_RptSv_Sys_Group_Filter_Draft
						from RptSv_Sys_Group rsg --//[mylock]
							left join RptSv_Sys_UserInGroup suig --//[mylock]
								on rsg.GroupCode = suig.GroupCode
							left join RptSv_Sys_User rsu --//[mylock]
								on suig.UserCode = rsu.UserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfGroup
							zzzzClauseWhere_strFilterWhereClause
						order by rsg.GroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_Group_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_Group_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_Group_Filter
						from #tbl_RptSv_Sys_Group_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_Group --------:
						zzzzClauseSelect_RptSv_Sys_Group_zOut
						----------------------------------------

						-------- RptSv_Sys_UserInGroup --------:
						zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_Group_Filter_Draft;
						--drop table #tbl_RptSv_Sys_Group_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_RptSv_Sys_Group_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_Group)
                {
                    #region // bGet_RptSv_Sys_Group:
                    zzzzClauseSelect_RptSv_Sys_Group_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_Group:
							select
								t.MyIdxSeq
								, rsg.*
							from #tbl_RptSv_Sys_Group_Filter t --//[mylock]
								inner join RptSv_Sys_Group rsg --//[mylock]
									on t.GroupCode = rsg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_UserInGroup)
                {
                    #region // bGet_RptSv_Sys_UserInGroup:
                    zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, rsu.UserCode su_UserCode
								, rsu.BankCode su_BankCode
								, rsu.UserName su_UserName 
								, rsu.FlagSysAdmin su_FlagSysAdmin 
								, rsu.FlagActive su_FlagActive 
								, rsg.GroupCode sg_GroupCode
								, rsg.GroupName sg_GroupName 
								, rsg.FlagActive sg_FlagActive 
							from #tbl_RptSv_Sys_Group_Filter t --//[mylock]
								inner join RptSv_Sys_UserInGroup suig --//[mylock]
									on t.GroupCode = suig.GroupCode
								left join RptSv_Sys_User rsu --//[mylock]
									on suig.UserCode = rsu.UserCode
								left join RptSv_Sys_Group rsg --//[mylock]
									on suig.GroupCode = rsg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_Group" // strTableNameDB
                            , "RptSv_Sys_Group." // strPrefixStd
                            , "rsg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_UserInGroup" // strTableNameDB
                            , "RptSv_Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_User" // strTableNameDB
                            , "RptSv_Sys_User." // strPrefixStd
                            , "rsu." // strPrefixAlias
                            );
                        htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_RptSv_Sys_Group_zOut", zzzzClauseSelect_RptSv_Sys_Group_zOut
                    , "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_RptSv_Sys_Group)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_Group";
                }
                if (bGet_RptSv_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Group_Get(
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
            , string strRt_Cols_RptSv_Sys_Group
            , string strRt_Cols_RptSv_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_RptSv_Sys_Group", strRt_Cols_RptSv_Sys_Group
                , "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
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

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_RptSv_Sys_Group = (strRt_Cols_RptSv_Sys_Group != null && strRt_Cols_RptSv_Sys_Group.Length > 0);
                bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

                // drAbilityOfGroup:
                //DataRow drAbilityOfGroup = myRptSv_Sys_Group_GetAbilityViewBankOfGroup(_cf.sinf.strGroupCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfGroup", drAbilityOfGroup["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_Group_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, rsg.GroupCode
						into #tbl_RptSv_Sys_Group_Filter_Draft
						from RptSv_Sys_Group rsg --//[mylock]
							left join RptSv_Sys_UserInGroup suig --//[mylock]
								on rsg.GroupCode = suig.GroupCode
							left join RptSv_Sys_User rsu --//[mylock]
								on suig.UserCode = rsu.UserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfGroup
							zzzzClauseWhere_strFilterWhereClause
						order by rsg.GroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_Group_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_Group_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_Group_Filter
						from #tbl_RptSv_Sys_Group_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_Group --------:
						zzzzClauseSelect_RptSv_Sys_Group_zOut
						----------------------------------------

						-------- RptSv_Sys_UserInGroup --------:
						zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_Group_Filter_Draft;
						--drop table #tbl_RptSv_Sys_Group_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_RptSv_Sys_Group_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_Group)
                {
                    #region // bGet_RptSv_Sys_Group:
                    zzzzClauseSelect_RptSv_Sys_Group_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_Group:
							select
								t.MyIdxSeq
								, rsg.*
							from #tbl_RptSv_Sys_Group_Filter t --//[mylock]
								inner join RptSv_Sys_Group rsg --//[mylock]
									on t.GroupCode = rsg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_UserInGroup)
                {
                    #region // bGet_RptSv_Sys_UserInGroup:
                    zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, rsu.UserCode su_UserCode
								, rsu.UserName su_UserName 
								, rsu.FlagSysAdmin su_FlagSysAdmin 
								, rsu.FlagActive su_FlagActive 
								, rsg.GroupCode sg_GroupCode
								, rsg.GroupName sg_GroupName 
								, rsg.FlagActive sg_FlagActive
							from #tbl_RptSv_Sys_Group_Filter t --//[mylock]
								inner join RptSv_Sys_UserInGroup suig --//[mylock]
									on t.GroupCode = suig.GroupCode
								left join RptSv_Sys_User rsu --//[mylock]
									on suig.UserCode = rsu.UserCode
								left join RptSv_Sys_Group rsg --//[mylock]
									on suig.GroupCode = rsg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_Group" // strTableNameDB
                            , "RptSv_Sys_Group." // strPrefixStd
                            , "rsg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_UserInGroup" // strTableNameDB
                            , "RptSv_Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_User" // strTableNameDB
                            , "RptSv_Sys_User." // strPrefixStd
                            , "rsu." // strPrefixAlias
                            );
                        htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_RptSv_Sys_Group_zOut", zzzzClauseSelect_RptSv_Sys_Group_zOut
                    , "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_RptSv_Sys_Group)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_Group";
                }
                if (bGet_RptSv_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

        public DataSet RptSv_Sys_Group_Create(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object objGroupName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Create;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    , "objGroupName", objGroupName
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //    ref alParamsCoupleError
                //    , strFunctionName
                //    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                {
                    ////
                    if (strGroupCode == null || strGroupCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupCode", strGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagPublicListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    if (strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_RptSv_Sys_Group.NewRow();
                    strFN = "GroupCode"; drDB[strFN] = strGroupCode;
                    strFN = "GroupName"; drDB[strFN] = strGroupName;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode;
                    dtDB_RptSv_Sys_Group.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet RptSv_Sys_Group_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , object objGroupName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                , "objGroupName", objGroupName
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                {
                    ////
                    if (strGroupCode == null || strGroupCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupCode", strGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagPublicListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    if (strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_RptSv_Sys_Group.NewRow();
                    strFN = "GroupCode"; drDB[strFN] = strGroupCode;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "GroupName"; drDB[strFN] = strGroupName;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_RptSv_Sys_Group.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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
        public DataSet RptSv_Sys_Group_Update(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object objGroupName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Update;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    , "objGroupName", objGroupName
                    , "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //    ref alParamsCoupleError
                //    , strFunctionName
                //    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                bool bUpd_GroupName = strFt_Cols_Upd.Contains("RptSv_Sys_Group.GroupName".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("RptSv_Sys_Group.FlagActive".ToUpper());
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    if (bUpd_GroupName && strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Update_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_Group:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_RptSv_Sys_Group.Rows[0];
                    if (bUpd_GroupName) { strFN = "GroupName"; drDB[strFN] = strGroupName; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Group_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , object objGroupName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                , "objGroupName", objGroupName
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

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
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                bool bUpd_GroupName = strFt_Cols_Upd.Contains("RptSv_Sys_Group.GroupName".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("RptSv_Sys_Group.FlagActive".ToUpper());
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    if (bUpd_GroupName && strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Group_Update_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_Group:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_RptSv_Sys_Group.Rows[0];
                    if (bUpd_GroupName) { strFN = "GroupName"; drDB[strFN] = strGroupName; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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

        public DataSet RptSv_Sys_Group_Delete(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Delete;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //    ref alParamsCoupleError
                //    , strFunctionName
                //    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    //// Delete RptSv_Sys_GroupInGroup:
                    Sys_UserInGroup_Delete_ByGroup(
                        strGroupCode // strGroupCode
                        );
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    dtDB_RptSv_Sys_Group.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Group_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Group_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    //// Delete RptSv_Sys_GroupInGroup:
                    Sys_UserInGroup_Delete_ByGroup(
                        strGroupCode // strGroupCode
                        );
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    dtDB_RptSv_Sys_Group.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "RptSv_Sys_Group"
                        , dtDB_RptSv_Sys_Group
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

        public DataSet WAS_RptSv_Sys_Group_Get(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group
            ////
            , out RT_RptSv_Sys_Group objRT_RptSv_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Group.Tid;
            objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Group_Get;
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
                List<RptSv_Sys_Group> lst_RptSv_Sys_Group = new List<RptSv_Sys_Group>();
                List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
                bool bGet_RptSv_Sys_Group = (objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_Group != null && objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_Group.Length > 0);
                bool bGet_RptSv_Sys_UserInGroup = (objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_UserInGroup != null && objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_UserInGroup.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = RptSv_Sys_Group_Get(
                    objRQ_RptSv_Sys_Group.Tid // strTid
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Group.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Group.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_RptSv_Sys_Group.Ft_RecordStart // strFt_RecordStart
                    , objRQ_RptSv_Sys_Group.Ft_RecordCount // strFt_RecordCount
                    , objRQ_RptSv_Sys_Group.Ft_WhereClause // strFt_WhereClause
                                                     //// Return:
                    , objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_Group // strRt_Cols_RptSv_Sys_Group
                    , objRQ_RptSv_Sys_Group.Rt_Cols_RptSv_Sys_UserInGroup // strRt_Cols_RptSv_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {

                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_RptSv_Sys_Group.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_RptSv_Sys_Group)
                    {
                        ////
                        DataTable dt_Sys_User = mdsResult.Tables["RptSv_Sys_Group"].Copy();
                        lst_RptSv_Sys_Group = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_Group>(dt_Sys_User);
                        objRT_RptSv_Sys_Group.Lst_RptSv_Sys_Group = lst_RptSv_Sys_Group;
                    }
                    // //
                    if (bGet_RptSv_Sys_UserInGroup)
                    {
                        ////
                        DataTable dt_RptSv_Sys_UserInGroup = mdsResult.Tables["RptSv_Sys_UserInGroup"].Copy();
                        lst_RptSv_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_UserInGroup>(dt_RptSv_Sys_UserInGroup);
                        objRT_RptSv_Sys_Group.Lst_RptSv_Sys_UserInGroup = lst_RptSv_Sys_UserInGroup;
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

        public DataSet WAS_RptSv_Sys_Group_Create(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group
            ////
            , out RT_RptSv_Sys_Group objRT_RptSv_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Group.Tid;
            objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Group_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group.RptSv_Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<RptSv_Sys_Group> lst_RptSv_Sys_Group = new List<RptSv_Sys_Group>();
                //List<RptSv_Sys_GroupInGroup> lst_RptSv_Sys_GroupInGroup = new List<RptSv_Sys_GroupInGroup>();
                #endregion

                #region // RptSv_Sys_Group_Create:
                mdsResult = RptSv_Sys_Group_Create(
                    objRQ_RptSv_Sys_Group.Tid // strTid
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Group.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Group.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.GroupCode // objGroupCode
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.GroupName // objGroupName
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

        public DataSet WAS_RptSv_Sys_Group_Update(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group
            ////
            , out RT_RptSv_Sys_Group objRT_RptSv_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Group.Tid;
            objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Group_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group.RptSv_Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<RptSv_Sys_Group> lst_RptSv_Sys_Group = new List<RptSv_Sys_Group>();
                //List<RptSv_Sys_GroupInGroup> lst_RptSv_Sys_GroupInGroup = new List<RptSv_Sys_GroupInGroup>();
                #endregion

                #region // RptSv_Sys_Group_Update:
                mdsResult = RptSv_Sys_Group_Update(
                    objRQ_RptSv_Sys_Group.Tid // strTid
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Group.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Group.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.GroupCode // objGroupCode
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.GroupName // objGroupName
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.FlagActive // objFlagActive
                                                           ////
                    , objRQ_RptSv_Sys_Group.Ft_Cols_Upd // objFt_Cols_Upd
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

        public DataSet WAS_RptSv_Sys_Group_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group
            ////
            , out RT_RptSv_Sys_Group objRT_RptSv_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Group.Tid;
            objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Group_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group.RptSv_Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<RptSv_Sys_Group> lst_RptSv_Sys_Group = new List<RptSv_Sys_Group>();
                //List<RptSv_Sys_GroupInGroup> lst_RptSv_Sys_GroupInGroup = new List<RptSv_Sys_GroupInGroup>();
                #endregion

                #region // RptSv_Sys_Group_Delete:
                mdsResult = RptSv_Sys_Group_Delete(
                    objRQ_RptSv_Sys_Group.Tid // strTid
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Group.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Group.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_RptSv_Sys_Group.RptSv_Sys_Group.GroupCode // objGroupCode
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

        #region // RptSv_Sys_UserInGroup:
        private void RptSv_Sys_UserInGroup_Delete_ByUser(
            object strUserCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from RptSv_Sys_UserInGroup t --//[mylock]
					where (1=1)
						and t.UserCode = @strUserCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strUserCode", strUserCode
                );
        }
        private void RptSv_Sys_UserInGroup_Delete_ByGroup(
            object strGroupCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from RptSv_Sys_UserInGroup t --//[mylock]
					where (1=1)
						and t.GroupCode = @strGroupCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strGroupCode", strGroupCode
                );
        }

        public DataSet RptSv_Sys_UserInGroup_Save(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object[] arrobjDSData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_UserInGroup_Save;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                if (dsData == null) dsData = new DataSet("dsData");
                dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                    });
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = _cf.sinf.strUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_RptSv_Sys_UserInGroup = null;
                ////
                {
                    ////
                    string strTableCheck = "RptSv_Sys_UserInGroup";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_UserInGroup_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_RptSv_Sys_UserInGroup = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_RptSv_Sys_UserInGroup // dtData
                        , "StdParam", "UserCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_UserInGroup" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "UserCode" } // arrSingleStructure
                        , dtInput_RptSv_Sys_UserInGroup // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_UserInGroup:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from RptSv_Sys_UserInGroup t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into RptSv_Sys_UserInGroup(
							GroupCode
							, UserCode
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_suig.UserCode
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_RptSv_Sys_UserInGroup t_suig --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_UserInGroup_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_UserInGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = strWAUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_RptSv_Sys_UserInGroup = null;
                ////
                {
                    ////
                    string strTableCheck = "RptSv_Sys_UserInGroup";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_UserInGroup_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_RptSv_Sys_UserInGroup = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_RptSv_Sys_UserInGroup // dtData
                        , "StdParam", "UserCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_UserInGroup" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "UserCode" } // arrSingleStructure
                        , dtInput_RptSv_Sys_UserInGroup // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_UserInGroup:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from RptSv_Sys_UserInGroup t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into RptSv_Sys_UserInGroup(
							GroupCode
							, UserCode
							, NetworkID
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_suig.UserCode
							, t_sg.NetworkID
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_RptSv_Sys_UserInGroup t_suig --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
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

        public DataSet WAS_RptSv_Sys_UserInGroup_Save(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_UserInGroup objRQ_RptSv_Sys_UserInGroup
            ////
            , out RT_RptSv_Sys_UserInGroup objRT_RptSv_Sys_UserInGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_UserInGroup.Tid;
            objRT_RptSv_Sys_UserInGroup = new RT_RptSv_Sys_UserInGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_UserInGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_UserInGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_UserInGroup.RptSv_Sys_Group)
                , "Lst_RptSv_Sys_UserInGroup", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_UserInGroup.Lst_RptSv_Sys_UserInGroup)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_RptSv_Sys_UserInGroup = TUtils.DataTableCmUtils.ToDataTable<RptSv_Sys_UserInGroup>(objRQ_RptSv_Sys_UserInGroup.Lst_RptSv_Sys_UserInGroup, "RptSv_Sys_UserInGroup");
                    dsData.Tables.Add(dt_RptSv_Sys_UserInGroup);
                }
                #endregion

                #region // RptSv_Sys_UserInGroup_Delete:
                mdsResult = RptSv_Sys_UserInGroup_Save(
                    objRQ_RptSv_Sys_UserInGroup.Tid // strTid
                    , objRQ_RptSv_Sys_UserInGroup.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_UserInGroup.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_UserInGroup.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_UserInGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_RptSv_Sys_UserInGroup.RptSv_Sys_Group.GroupCode // objGroupCode
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

        #region // RptSv_Sys_Access:
        private void RptSv_Sys_Access_CheckDB(
            ref ArrayList alParamsCoupleError
            , string strFunctionName
            )
        {

        }
        private void RptSv_Sys_Access_CheckDeny(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , object strObjectCode
            )
        {
            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@strUserCode", strUserCode
                , "@strObjectCode", strObjectCode
                });
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_Access:
						select distinct
							rso.ObjectCode
						from RptSv_Sys_User rsu --//[mylock]
							inner join RptSv_Sys_UserInGroup rsuig --//[mylock]
								on rsu.UserCode = rsuig.UserCode
							inner join RptSv_Sys_Group rsg --//[mylock]
								on rsuig.GroupCode = rsg.GroupCode and rsg.FlagActive = '1'
							inner join RptSv_Sys_Access rsa --//[mylock]
								on rsg.GroupCode = rsa.GroupCode
							inner join RptSv_Sys_Object rso --//[mylock]
								on rsa.ObjectCode = rso.ObjectCode and rso.FlagActive = '1'
						where (1=1)
							and rsu.UserCode = @strUserCode
							and rsu.FlagActive = '1'
							and rso.ObjectCode = @strObjectCode
							and rso.FlagActive = '1'
						union -- distinct
						select distinct
							rso.ObjectCode
						from RptSv_Sys_User rsu --//[mylock]
							inner join RptSv_Sys_Object rso --//[mylock]
								on (1=1)
									and rsu.FlagSysAdmin = '1' 
									and rsu.UserCode = @strUserCode
									and rsu.FlagActive = '1'
									and rso.ObjectCode = @strObjectCode
									and rso.FlagActive = '1'
						;
					"
                );
            #endregion

            #region // Get Data and Check:
            DataTable dtGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                ).Tables[0];
            if (dtGetData.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strObjectCode", strObjectCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_Access_CheckDeny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

        }

        private void RptSv_Sys_Access_CheckDenyV30(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , object strObjectCode
            )
        {
            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@strUserCode", strUserCode
                , "@strObjectCode", strObjectCode
                });
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_Access:
						select 
							soim.ObjectCode
						from RptSv_Sys_User rsu --//[mylock] 
							inner join Sys_UserLicenseModules sulm --//[mylock] 
								on rsu.UserCode = sulm.UserCode
							inner join Sys_ObjectInModules soim --//[mylock]
								on sulm.ModuleCode = soim.ModuleCode
							inner join RptSv_Sys_Object rso --//[mylock]
								on soim.ObjectCode = rso.ObjectCode and rso.FlagActive = '1' 
						where (1=1)
							and rsu.UserCode = @strUserCode
							and rsu.FlagActive = '1'
							and rso.ObjectCode = @strObjectCode
							and rso.FlagActive = '1'
						union -- distinct
						select distinct
							rso.ObjectCode
						from RptSv_Sys_User rsu --//[mylock]
							inner join RptSv_Sys_Object rso --//[mylock]
								on (1=1)
									and (rsu.FlagSysAdmin = '1' or rsu.FlagBG = '1')
									and rsu.UserCode = @strUserCode
									and rsu.FlagActive = '1'
									and rso.ObjectCode = @strObjectCode
									and rso.FlagActive = '1'
						;
					"
                );
            #endregion

            #region // Get Data and Check:
            DataTable dtGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                ).Tables[0];
            if (dtGetData.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strObjectCode", strObjectCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_Access_CheckDeny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

        }

        private void RptSv_RptSv_Sys_Access_CheckDeny(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , object strObjectCode
            )
        {
            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@strUserCode", strUserCode
                , "@strObjectCode", strObjectCode
                });

            ////
            DataTable dt_RptSv_Sys_User = null;

            RptSv_Sys_User_CheckDB(
                ref alParamsCoupleError // alParamsCoupleError
                , strUserCode // strUserCode
                , TConst.Flag.Yes // strFlagExistToCheck
                , TConst.Flag.Active // strFlagActiveListToCheck
                , out dt_RptSv_Sys_User // dt_RptSv_Sys_User
                );
            #endregion

            #region // Get Data and Check:
            if (!CmUtils.StringUtils.StringEqual(dt_RptSv_Sys_User.Rows[0]["FlagSysAdmin"], TConst.Flag.Active))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strObjectCode", strObjectCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_Access_CheckDeny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

        }

        public DataSet RptSv_Sys_Access_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_RptSv_Sys_Access
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Access_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_Access", strRt_Cols_RptSv_Sys_Access
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //    ref alParamsCoupleError
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_RptSv_Sys_Access = (strRt_Cols_RptSv_Sys_Access != null && strRt_Cols_RptSv_Sys_Access.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = myRptSv_Sys_Access_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_Access_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sa.GroupCode
                            , sa.ObjectCode
						into #tbl_RptSv_Sys_Access_Filter_Draft
						from RptSv_Sys_Access sa --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by sa.GroupCode asc
                                , sa.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_Access_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_Access_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_Access_Filter
						from #tbl_RptSv_Sys_Access_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_Access --------:
						zzzzClauseSelect_RptSv_Sys_Access_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_Access_Filter_Draft;
						--drop table #tbl_RptSv_Sys_Access_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_RptSv_Sys_Access_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_Access)
                {
                    #region // bGet_RptSv_Sys_Access:
                    zzzzClauseSelect_RptSv_Sys_Access_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_Access:
							select
								t.MyIdxSeq
								, sa.*
								, rso.ObjectCode so_ObjectCode
                                , rso.ObjectName so_ObjectName 
                                , rso.ServiceCode so_ServiceCode 
                                , rso.ObjectType so_ObjectType 
                                , rso.FlagExecModal so_FlagExecModal 
                                , rso.FlagActive so_FlagActive 
							from #tbl_RptSv_Sys_Access_Filter t --//[mylock]
								inner join RptSv_Sys_Access sa --//[mylock]
									on t.GroupCode = sa.GroupCode
                                        and t.ObjectCode = sa.ObjectCode
                                left join RptSv_Sys_Object rso --//[mylock]
                                    on t.ObjectCode = rso.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_Access" // strTableNameDB
                            , "RptSv_Sys_Access." // strPrefixStd
                            , "sa." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_RptSv_Sys_Access_zOut", zzzzClauseSelect_RptSv_Sys_Access_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_RptSv_Sys_Access)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_Access";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Access_Get(
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
            , string strRt_Cols_RptSv_Sys_Access
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Access_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
			//// Return
				, "strRt_Cols_RptSv_Sys_Access", strRt_Cols_RptSv_Sys_Access
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_RptSv_Sys_Access = (strRt_Cols_RptSv_Sys_Access != null && strRt_Cols_RptSv_Sys_Access.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = myRptSv_Sys_Access_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_Access_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sa.GroupCode
                            , sa.ObjectCode
						into #tbl_RptSv_Sys_Access_Filter_Draft
						from RptSv_Sys_Access sa --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by sa.GroupCode asc
                                , sa.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_Access_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_Access_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_Access_Filter
						from #tbl_RptSv_Sys_Access_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_Access --------:
						zzzzClauseSelect_RptSv_Sys_Access_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_Access_Filter_Draft;
						--drop table #tbl_RptSv_Sys_Access_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_RptSv_Sys_Access_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_Access)
                {
                    #region // bGet_RptSv_Sys_Access:
                    zzzzClauseSelect_RptSv_Sys_Access_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_Access:
							select
								t.MyIdxSeq
								, sa.*
								, rso.ObjectCode so_ObjectCode
                                , rso.ObjectName so_ObjectName 
                                , rso.ServiceCode so_ServiceCode 
                                , rso.ObjectType so_ObjectType 
                                , rso.FlagExecModal so_FlagExecModal 
                                , rso.FlagActive so_FlagActive 
							from #tbl_RptSv_Sys_Access_Filter t --//[mylock]
								inner join RptSv_Sys_Access sa --//[mylock]
									on t.GroupCode = sa.GroupCode
                                        and t.ObjectCode = sa.ObjectCode
                                left join RptSv_Sys_Object rso --//[mylock]
                                    on t.ObjectCode = rso.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_Access" // strTableNameDB
                            , "RptSv_Sys_Access." // strPrefixStd
                            , "sa." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_RptSv_Sys_Access_zOut", zzzzClauseSelect_RptSv_Sys_Access_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_RptSv_Sys_Access)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_Access";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

        public DataSet WAS_RptSv_Sys_Access_Get(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Access objRQ_RptSv_Sys_Access
            ////
            , out RT_RptSv_Sys_Access objRT_RptSv_Sys_Access
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Access.Tid;
            objRT_RptSv_Sys_Access = new RT_RptSv_Sys_Access();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Access.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Access_Get;
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
                List<RptSv_Sys_Access> lst_RptSv_Sys_Access = new List<RptSv_Sys_Access>();
                #endregion

                #region // WS_RptSv_Sys_Access_Get:
                mdsResult = RptSv_Sys_Access_Get(
                    objRQ_RptSv_Sys_Access.Tid // strTid
                    , objRQ_RptSv_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Access.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Access.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Access.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_RptSv_Sys_Access.Ft_RecordStart // strFt_RecordStart
                    , objRQ_RptSv_Sys_Access.Ft_RecordCount // strFt_RecordCount
                    , objRQ_RptSv_Sys_Access.Ft_WhereClause // strFt_WhereClause
                                                      //// Return:
                    , objRQ_RptSv_Sys_Access.Rt_Cols_RptSv_Sys_Access // strRt_Cols_RptSv_Sys_Access
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_RptSv_Sys_Access.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_RptSv_Sys_Access = mdsResult.Tables["RptSv_Sys_Access"].Copy();
                    lst_RptSv_Sys_Access = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_Access>(dt_RptSv_Sys_Access);
                    objRT_RptSv_Sys_Access.Lst_RptSv_Sys_Access = lst_RptSv_Sys_Access;
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

        public DataSet RptSv_Sys_Access_Save(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object[] arrobjDSData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "RptSv_Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Access_Save;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                if (dsData == null) dsData = new DataSet("dsData");
                dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                    });
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //    ref alParamsCoupleError
                //    , strFunctionName
                //    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    dtDB_RptSv_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_RptSv_Sys_Group.Rows[0]["LogLUBy"] = _cf.sinf.strUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_RptSv_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_RptSv_Sys_Access = null;
                ////
                {
                    ////
                    string strTableCheck = "RptSv_Sys_Access";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Access_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_RptSv_Sys_Access = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_RptSv_Sys_Access // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );
                    ////
                    for (int nScan = 0; nScan < dtInput_RptSv_Sys_Access.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_RptSv_Sys_Access.Rows[nScan];

                        ////
                        DataTable dtDB_Sys_Object = null;

                        Sys_Object_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , drScan["ObjectCode"] // strObjectCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Sys_Object // dtDB_Sys_Object
                            );
                    }

                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_Access" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_RptSv_Sys_Access // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_Access:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from RptSv_Sys_Access t --//[mylock]
							inner join #tbl_RptSv_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into RptSv_Sys_Access(
							GroupCode
							, ObjectCode
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_sa.ObjectCode
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_RptSv_Sys_Group t_sg --//[mylock]
							inner join #tbl_RptSv_Sys_Access t_sa --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
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
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Access_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Access_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
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
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_RptSv_Sys_Group = null;
                ////
                {
                    ////
                    RptSv_Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_RptSv_Sys_Group // dtDB_RptSv_Sys_Group
                        );
                    ////
                    dtDB_RptSv_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_RptSv_Sys_Group.Rows[0]["LogLUBy"] = strWAUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_RptSv_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_RptSv_Sys_Access = null;
                ////
                {
                    ////
                    string strTableCheck = "RptSv_Sys_Access";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.RptSv_Sys_Access_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_RptSv_Sys_Access = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_RptSv_Sys_Access // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );

                    ////
                    for (int nScan = 0; nScan < dtInput_RptSv_Sys_Access.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_RptSv_Sys_Access.Rows[nScan];

                        ////
                        DataTable dtDB_Sys_Object = null;

                        RptSv_Sys_Object_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , drScan["ObjectCode"] // strObjectCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Sys_Object // dtDB_Sys_Object
                            );
                    }

                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_RptSv_Sys_Access" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_RptSv_Sys_Access // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB RptSv_Sys_Access:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from RptSv_Sys_Access t --//[mylock]
							inner join #tbl_RptSv_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into RptSv_Sys_Access(
							GroupCode
							, ObjectCode
							, NetworkID
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_sa.ObjectCode
							, t_sg.NetworkID
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_RptSv_Sys_Group t_sg --//[mylock]
							inner join #tbl_RptSv_Sys_Access t_sa --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
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

        public DataSet WAS_RptSv_Sys_Access_Save(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Access objRQ_RptSv_Sys_Access
            ////
            , out RT_RptSv_Sys_Access objRT_RptSv_Sys_Access
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Access.Tid;
            objRT_RptSv_Sys_Access = new RT_RptSv_Sys_Access();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_UserInGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Access_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Access.RptSv_Sys_Group)
                , "Lst_RptSv_Sys_Access", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_RptSv_Sys_Access = TUtils.DataTableCmUtils.ToDataTable<RptSv_Sys_Access>(objRQ_RptSv_Sys_Access.Lst_RptSv_Sys_Access, "RptSv_Sys_Access");
                    dsData.Tables.Add(dt_RptSv_Sys_Access);
                }
                #endregion

                #region // RptSv_Sys_Access_Delete:
                mdsResult = RptSv_Sys_Access_Save(
                    objRQ_RptSv_Sys_Access.Tid // strTid
                    , objRQ_RptSv_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Access.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Access.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Access.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_RptSv_Sys_Access.RptSv_Sys_Group.GroupCode // objGroupCode
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

        #region // RptSv_Sys_Object:
        private void RptSv_Sys_Object_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strObjectCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_RptSv_Sys_Object
            )
        {
            // GetInfo:
            dtDB_RptSv_Sys_Object = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "RptSv_Sys_Object" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "ObjectCode", "=", strObjectCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_RptSv_Sys_Object.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ObjectCodeNotFound", strObjectCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.RptSv_Sys_Object_CheckDB_ObjectCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_RptSv_Sys_Object.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ObjectCodeExist", strObjectCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.RptSv_Sys_Object_CheckDB_ObjectCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_RptSv_Sys_Object.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ObjectCode", strObjectCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_RptSv_Sys_Object.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_Object_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }
        public DataSet RptSv_Sys_Object_Get(
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
            , string strRt_Cols_RptSv_Sys_Object
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Object_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Object_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_RptSv_Sys_Object", strRt_Cols_RptSv_Sys_Object
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

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_RptSv_Sys_Object = (strRt_Cols_RptSv_Sys_Object != null && strRt_Cols_RptSv_Sys_Object.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = myRptSv_Sys_Object_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_Object_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
                            , rso.ObjectCode
						into #tbl_RptSv_Sys_Object_Filter_Draft
						from RptSv_Sys_Object rso --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by rso.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_Object_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_Object_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_Object_Filter
						from #tbl_RptSv_Sys_Object_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_Object --------:
						zzzzClauseSelect_RptSv_Sys_Object_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_Object_Filter_Draft;
						--drop table #tbl_RptSv_Sys_Object_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_RptSv_Sys_Object_zOut = "-- Nothing.";
                if (bGet_RptSv_Sys_Object)
                {
                    #region // bGet_RptSv_Sys_Object:
                    zzzzClauseSelect_RptSv_Sys_Object_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_Object:
							select
								t.MyIdxSeq
								, rso.*
							from #tbl_RptSv_Sys_Object_Filter t --//[mylock]
								inner join RptSv_Sys_Object rso --//[mylock]
                                     on  t.ObjectCode = rso.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "RptSv_Sys_Object" // strTableNameDB
                            , "RptSv_Sys_Object." // strPrefixStd
                            , "rso." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_RptSv_Sys_Object_zOut", zzzzClauseSelect_RptSv_Sys_Object_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_RptSv_Sys_Object)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_Object";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

        public DataSet WAS_RptSv_Sys_Object_Get(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_Object objRQ_RptSv_Sys_Object
            ////
            , out RT_RptSv_Sys_Object objRT_RptSv_Sys_Object
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_Object.Tid;
            objRT_RptSv_Sys_Object = new RT_RptSv_Sys_Object();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Object.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Object_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Object_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_RptSv_Sys_Object.WAUserCode
                //    , objRQ_RptSv_Sys_Object.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<RptSv_Sys_Object> lst_RptSv_Sys_Object = new List<RptSv_Sys_Object>();
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = RptSv_Sys_Object_Get(
                    objRQ_RptSv_Sys_Object.Tid // strTid
                    , objRQ_RptSv_Sys_Object.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Object.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_Object.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_Object.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              // // Filter:
                    , objRQ_RptSv_Sys_Object.Ft_RecordStart // strFt_RecordStart
                    , objRQ_RptSv_Sys_Object.Ft_RecordCount // strFt_RecordCount
                    , objRQ_RptSv_Sys_Object.Ft_WhereClause // strFt_WhereClause
                                                      // // Return:
                    , objRQ_RptSv_Sys_Object.Rt_Cols_RptSv_Sys_Object // strRt_Cols_RptSv_Sys_Object
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_RptSv_Sys_Object.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_Sys_User = mdsResult.Tables["RptSv_Sys_Object"].Copy();
                    lst_RptSv_Sys_Object = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_Object>(dt_Sys_User);
                    objRT_RptSv_Sys_Object.Lst_RptSv_Sys_Object = lst_RptSv_Sys_Object;
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
