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
        #region // Mst_PartType:
        private void Mst_PartType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPartType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_PartType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_PartType t --//[mylock]
					where (1=1)
						and t.PartType = @objPartType
					;
				");
            dtDB_Mst_PartType = _cf.db.ExecQuery(
                strSqlExec
                , "@objPartType", objPartType
                ).Tables[0];
            dtDB_Mst_PartType.TableName = "Mst_PartType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_PartType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartType", objPartType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartType_CheckDB_PartTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_PartType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartType", objPartType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartType_CheckDB_PartTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_PartType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PartType", objPartType
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_PartType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_PartType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_PartType_GetX(
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
            , string strRt_Cols_Mst_PartType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartType_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartType", strRt_Cols_Mst_PartType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_PartType = (strRt_Cols_Mst_PartType != null && strRt_Cols_Mst_PartType.Length > 0);

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
						---- #tbl_Mst_PartType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mptp.PartType
						into #tbl_Mst_PartType_Filter_Draft
						from Mst_PartType mptp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mptp.PartType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_PartType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_PartType_Filter:
						select
							t.*
						into #tbl_Mst_PartType_Filter
						from #tbl_Mst_PartType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_PartType --------:
						zzB_Select_Mst_PartType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_PartType_Filter_Draft;
						--drop table #tbl_Mst_PartType_Filter;
					"
                );
            ////
            string zzB_Select_Mst_PartType_zzE = "-- Nothing.";
            if (bGet_Mst_PartType)
            {
                #region // bGet_Mst_PartType:
                zzB_Select_Mst_PartType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_PartType:
                        select
                            t.MyIdxSeq
	                        , mptp.*
                        from #tbl_Mst_PartType_Filter t --//[mylock]
	                        inner join Mst_PartType mptp --//[mylock]
		                        on t.PartType = mptp.PartType
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
                        , "Mst_PartType" // strTableNameDB
                        , "Mst_PartType." // strPrefixStd
                        , "mptp." // strPrefixAlias
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
                , "zzB_Select_Mst_PartType_zzE", zzB_Select_Mst_PartType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_PartType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_PartType";
            }
            #endregion
        }

        public DataSet Mst_PartType_Get(
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
            , string strRt_Cols_Mst_PartType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartType", strRt_Cols_Mst_PartType
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_PartType_GetX(
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
                        , strRt_Cols_Mst_PartType // strRt_Cols_Mst_PartType
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

        public DataSet WAS_Mst_PartType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartType objRQ_Mst_PartType
            ////
            , out RT_Mst_PartType objRT_Mst_PartType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartType.Tid;
            objRT_Mst_PartType = new RT_Mst_PartType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartType_Get;
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
                List<Mst_PartType> lst_Mst_PartType = new List<Mst_PartType>();
                #endregion

                #region // WAS_Mst_PartType_Get:
                mdsResult = Mst_PartType_Get(
                    objRQ_Mst_PartType.Tid // strTid
                    , objRQ_Mst_PartType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartType.GwPassword // strGwPassword
                    , objRQ_Mst_PartType.WAUserCode // strUserCode
                    , objRQ_Mst_PartType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_PartType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_PartType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_PartType.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_PartType.Rt_Cols_Mst_PartType // strRt_Cols_Mst_PartType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_PartType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_PartType = mdsResult.Tables["Mst_PartType"].Copy();
                    lst_Mst_PartType = TUtils.DataTableCmUtils.ToListof<Mst_PartType>(dt_Mst_PartType);
                    objRT_Mst_PartType.Lst_Mst_PartType = lst_Mst_PartType;
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

        public DataSet WAS_Mst_PartType_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartType objRQ_Mst_PartType
            ////
            , out RT_Mst_PartType objRT_Mst_PartType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartType.Tid;
            objRT_Mst_PartType = new RT_Mst_PartType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartType_Create;
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
                List<Mst_PartType> lst_Mst_PartType = new List<Mst_PartType>();
                #endregion

                #region // Mst_PartType_Create:
                mdsResult = Mst_PartType_Create(
                    objRQ_Mst_PartType.Tid // strTid
                    , objRQ_Mst_PartType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartType.GwPassword // strGwPassword
                    , objRQ_Mst_PartType.WAUserCode // strUserCode
                    , objRQ_Mst_PartType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartType.Mst_PartType.PartType // objPartType
                    , objRQ_Mst_PartType.Mst_PartType.PartTypeName // objPartTypeName
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
        public DataSet Mst_PartType_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartType
            , object objPartTypeName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartType_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
                    , "objPartTypeName", objPartTypeName
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

                #region // Mst_PartType_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartType_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
                        , objPartTypeName // objPartTypeName
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
        private void Mst_PartType_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            , object objPartTypeName
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartType_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPartType", objPartType
                , "objPartTypeName", objPartTypeName
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartType = TUtils.CUtils.StdParam(objPartType);
            string strPartTypeName = string.Format("{0}", objPartTypeName).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_PartType = null;

            {
                ////
                if (strPartType == null || strPartType.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartType", strPartType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartType_CreateX_InvalidPartType
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_PartType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartType // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_PartType // dtDB_Mst_PartType
                    );
                ////
                if (strPartTypeName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartTypeName", strPartTypeName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartType_CreateX_InvalidPartTypeName
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
                DataRow drDB = dtDB_Mst_PartType.NewRow();
                strFN = "PartType"; drDB[strFN] = strPartType;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "PartTypeName"; drDB[strFN] = strPartTypeName;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_PartType.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartType" // strTableName
                    , dtDB_Mst_PartType // dtData
                                        //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_Mst_PartType_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartType objRQ_Mst_PartType
            ////
            , out RT_Mst_PartType objRT_Mst_PartType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartType.Tid;
            objRT_Mst_PartType = new RT_Mst_PartType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartType_Update;
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
                List<Mst_PartType> lst_Mst_PartType = new List<Mst_PartType>();
                #endregion

                #region // Mst_PartType_Update:
                mdsResult = Mst_PartType_Update(
                    objRQ_Mst_PartType.Tid // strTid
                    , objRQ_Mst_PartType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartType.GwPassword // strGwPassword
                    , objRQ_Mst_PartType.WAUserCode // strUserCode
                    , objRQ_Mst_PartType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartType.Mst_PartType.PartType // objPartType
                    , objRQ_Mst_PartType.Mst_PartType.PartTypeName // objPartTypeName
                    , objRQ_Mst_PartType.Mst_PartType.FlagActive // objFlagActive
                                                                 ////
                    , objRQ_Mst_PartType.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_PartType_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartType
            , object objPartTypeName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartType_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
                    , "objPartTypeName", objPartTypeName
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartType_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartType_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
                        , objPartTypeName // objPartTypeName
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
        private void Mst_PartType_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            , object objPartTypeName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartType_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPartType", objPartType
                , "objPartTypeName", objPartTypeName
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
            string strPartType = TUtils.CUtils.StdParam(objPartType);
            string strPartTypeName = string.Format("{0}", objPartTypeName).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_PartType = strFt_Cols_Upd.Contains("Mst_PartType.PartType".ToUpper());
            bool bUpd_PartTypeName = strFt_Cols_Upd.Contains("Mst_PartType.PartTypeName".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_PartType.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_PartType = null;
            {
                ////
                Mst_PartType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartType // objPartType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartType // dtDB_Mst_PartType
                    );
                ////
                if (strPartTypeName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartTypeName", strPartTypeName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartType_UpdateX_InvalidPartTypeName
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
                DataRow drDB = dtDB_Mst_PartType.Rows[0];
                if (bUpd_PartType) { strFN = "PartType"; drDB[strFN] = strPartType; alColumnEffective.Add(strFN); }
                if (bUpd_PartTypeName) { strFN = "PartTypeName"; drDB[strFN] = strPartTypeName; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartType"
                    , dtDB_Mst_PartType
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet WAS_Mst_PartType_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartType objRQ_Mst_PartType
            ////
            , out RT_Mst_PartType objRT_Mst_PartType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartType.Tid;
            objRT_Mst_PartType = new RT_Mst_PartType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartType_Delete;
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
                List<Mst_PartType> lst_Mst_PartType = new List<Mst_PartType>();
                #endregion

                #region // Mst_PartType_Delete:
                mdsResult = Mst_PartType_Delete(
                    objRQ_Mst_PartType.Tid // strTid
                    , objRQ_Mst_PartType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartType.GwPassword // strGwPassword
                    , objRQ_Mst_PartType.WAUserCode // strUserCode
                    , objRQ_Mst_PartType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartType.Mst_PartType.PartType // objPartType
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
        public DataSet Mst_PartType_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPartType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartType_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
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

                #region // Mst_PartType_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_PartType_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
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
        private void Mst_PartType_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartType_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartType", objPartType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartType = TUtils.CUtils.StdParam(objPartType);

            ////
            DataTable dtDB_Mst_PartType = null;
            {
                ////
                Mst_PartType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartType // strSupCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartType // dtDB_Mst_Supplier
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_PartType.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_PartType"
                    , dtDB_Mst_PartType
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_PartMaterialType:
        private void Mst_PartMaterialType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPMType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_PartMaterialType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_PartMaterialType t --//[mylock]
					where (1=1)
						and t.PMType = @objPMType
					;
				");
            dtDB_Mst_PartMaterialType = _cf.db.ExecQuery(
                strSqlExec
                , "@objPMType", objPMType
                ).Tables[0];
            dtDB_Mst_PartMaterialType.TableName = "Mst_PartMaterialType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_PartMaterialType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PMType", objPMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartMaterialType_CheckDB_PMTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_PartMaterialType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PMType", objPMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartMaterialType_CheckDB_PMTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_PartMaterialType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PMType", objPMType
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_PartMaterialType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_PartMaterialType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_PartMaterialType_GetX(
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
            , string strRt_Cols_Mst_PartMaterialType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartMaterialType_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartMaterialType", strRt_Cols_Mst_PartMaterialType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_PartMaterialType = (strRt_Cols_Mst_PartMaterialType != null && strRt_Cols_Mst_PartMaterialType.Length > 0);

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
						---- #tbl_Mst_PartMaterialType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mpmtp.PMType
						into #tbl_Mst_PartMaterialType_Filter_Draft
						from Mst_PartMaterialType mpmtp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mpmtp.PMType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_PartMaterialType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_PartMaterialType_Filter:
						select
							t.*
						into #tbl_Mst_PartMaterialType_Filter
						from #tbl_Mst_PartMaterialType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_PartMaterialType --------:
						zzB_Select_Mst_PartMaterialType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_PartMaterialType_Filter_Draft;
						--drop table #tbl_Mst_PartMaterialType_Filter;
					"
                );
            ////
            string zzB_Select_Mst_PartMaterialType_zzE = "-- Nothing.";
            if (bGet_Mst_PartMaterialType)
            {
                #region // bGet_Mst_PartMaterialType:
                zzB_Select_Mst_PartMaterialType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_PartMaterialType:
                        select
                            t.MyIdxSeq
	                        , mpmtp.*
                        from #tbl_Mst_PartMaterialType_Filter t --//[mylock]
	                        inner join Mst_PartMaterialType mpmtp --//[mylock]
		                        on t.PMType = mpmtp.PMType
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
                        , "Mst_PartMaterialType" // strTableNameDB
                        , "Mst_PartMaterialType." // strPrefixStd
                        , "mpmtp." // strPrefixAlias
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
                , "zzB_Select_Mst_PartMaterialType_zzE", zzB_Select_Mst_PartMaterialType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_PartMaterialType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_PartMaterialType";
            }
            #endregion
        }

        public DataSet Mst_PartMaterialType_Get(
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
            , string strRt_Cols_Mst_PartMaterialType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartMaterialType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartMaterialType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartMaterialType", strRt_Cols_Mst_PartMaterialType
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartMaterialType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_PartMaterialType_GetX(
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
                        , strRt_Cols_Mst_PartMaterialType // strRt_Cols_Mst_PartMaterialType
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

        public DataSet WAS_Mst_PartMaterialType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType
            ////
            , out RT_Mst_PartMaterialType objRT_Mst_PartMaterialType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartMaterialType.Tid;
            objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartMaterialType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartMaterialType_Get;
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
                List<Mst_PartMaterialType> lst_Mst_PartMaterialType = new List<Mst_PartMaterialType>();
                #endregion

                #region // WAS_Mst_PartMaterialType_Get:
                mdsResult = Mst_PartMaterialType_Get(
                    objRQ_Mst_PartMaterialType.Tid // strTid
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    , objRQ_Mst_PartMaterialType.WAUserCode // strUserCode
                    , objRQ_Mst_PartMaterialType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_PartMaterialType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_PartMaterialType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_PartMaterialType.Ft_WhereClause // strFt_WhereClause
                                                                //// Return:
                    , objRQ_Mst_PartMaterialType.Rt_Cols_Mst_PartMaterialType // strRt_Cols_Mst_PartMaterialType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_PartMaterialType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_PartMaterialType = mdsResult.Tables["Mst_PartMaterialType"].Copy();
                    lst_Mst_PartMaterialType = TUtils.DataTableCmUtils.ToListof<Mst_PartMaterialType>(dt_Mst_PartMaterialType);
                    objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType = lst_Mst_PartMaterialType;
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

        public DataSet WAS_Mst_PartMaterialType_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType
            ////
            , out RT_Mst_PartMaterialType objRT_Mst_PartMaterialType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartMaterialType.Tid;
            objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartMaterialType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartMaterialType_Create;
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
                List<Mst_PartMaterialType> lst_Mst_PartMaterialType = new List<Mst_PartMaterialType>();
                #endregion

                #region // Mst_PartMaterialType_Create:
                mdsResult = Mst_PartMaterialType_Create(
                    objRQ_Mst_PartMaterialType.Tid // strTid
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    , objRQ_Mst_PartMaterialType.WAUserCode // strUserCode
                    , objRQ_Mst_PartMaterialType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.PMType // objPMType
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.PMTypeName // objPMTypeName
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
        public DataSet Mst_PartMaterialType_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPMType
            , object objPMTypeName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartMaterialType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartMaterialType_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPMType", objPMType
                    , "objPMTypeName", objPMTypeName
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

                #region // Mst_PartMaterialType_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartMaterialType_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPMType // objPMType
                        , objPMTypeName // objPMTypeName
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
        private void Mst_PartMaterialType_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPMType
            , object objPMTypeName
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartMaterialType_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPMType", objPMType
                , "objPMTypeName", objPMTypeName
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strPMTypeName = string.Format("{0}", objPMTypeName).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_PartMaterialType = null;

            {
                ////
                if (strPMType == null || strPMType.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPMType", strPMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartMaterialType_CreateX_InvalidPMType
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_PartMaterialType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPMType // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_PartMaterialType // dtDB_Mst_PartMaterialType
                    );
                ////
                if (strPMTypeName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPMTypeName", strPMTypeName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartMaterialType_CreateX_InvalidPMTypeName
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
                DataRow drDB = dtDB_Mst_PartMaterialType.NewRow();
                strFN = "PMType"; drDB[strFN] = strPMType;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "PMTypeName"; drDB[strFN] = strPMTypeName;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_PartMaterialType.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartMaterialType" // strTableName
                    , dtDB_Mst_PartMaterialType // dtData
                                                //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }
        public DataSet WAS_Mst_PartMaterialType_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType
            ////
            , out RT_Mst_PartMaterialType objRT_Mst_PartMaterialType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartMaterialType.Tid;
            objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartMaterialType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartMaterialType_Update;
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
                List<Mst_PartMaterialType> lst_Mst_PartMaterialType = new List<Mst_PartMaterialType>();
                #endregion

                #region // Mst_PartMaterialType_Update:
                mdsResult = Mst_PartMaterialType_Update(
                    objRQ_Mst_PartMaterialType.Tid // strTid
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    , objRQ_Mst_PartMaterialType.WAUserCode // strUserCode
                    , objRQ_Mst_PartMaterialType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.PMType // objPMType
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.PMTypeName // objPMTypeName
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.FlagActive // objFlagActive
                                                                                 ////
                    , objRQ_Mst_PartMaterialType.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_PartMaterialType_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPMType
            , object objPMTypeName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartMaterialType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartMaterialType_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPMType", objPMType
                    , "objPMTypeName", objPMTypeName
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartMaterialType_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartMaterialType_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPMType // objPMType
                        , objPMTypeName // objPMTypeName
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
        private void Mst_PartMaterialType_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPMType
            , object objPMTypeName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartMaterialType_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPMType", objPMType
                , "objPMTypeName", objPMTypeName
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
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strPMTypeName = string.Format("{0}", objPMTypeName).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_PMType = strFt_Cols_Upd.Contains("Mst_PartMaterialType.PMType".ToUpper());
            bool bUpd_PMTypeName = strFt_Cols_Upd.Contains("Mst_PartMaterialType.PMTypeName".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_PartMaterialType.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_PartMaterialType = null;
            {
                ////
                Mst_PartMaterialType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPMType // strPMType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartMaterialType // dtDB_Mst_PartMaterialType
                    );
                ////
                if (strPMTypeName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPMTypeName", strPMTypeName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartMaterialType_Update_InvalidPMTypeName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }

            #endregion

            #region // Save Mst_PartMaterialType:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_PartMaterialType.Rows[0];
                if (bUpd_PMType) { strFN = "PMType"; drDB[strFN] = strPMType; alColumnEffective.Add(strFN); }
                if (bUpd_PMTypeName) { strFN = "PMTypeName"; drDB[strFN] = strPMTypeName; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartMaterialType"
                    , dtDB_Mst_PartMaterialType
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet WAS_Mst_PartMaterialType_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType
            ////
            , out RT_Mst_PartMaterialType objRT_Mst_PartMaterialType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartMaterialType.Tid;
            objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartMaterialType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartMaterialType_Delete;
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
                List<Mst_PartMaterialType> lst_Mst_PartMaterialType = new List<Mst_PartMaterialType>();
                #endregion

                #region // Mst_PartMaterialType_Delete:
                mdsResult = Mst_PartMaterialType_Delete(
                    objRQ_Mst_PartMaterialType.Tid // strTid
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    , objRQ_Mst_PartMaterialType.WAUserCode // strUserCode
                    , objRQ_Mst_PartMaterialType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartMaterialType.Mst_PartMaterialType.PMType // objPMType
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
        public DataSet Mst_PartMaterialType_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPMType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartMaterialType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartMaterialType_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPMType", objPMType
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

                #region // Mst_PartMaterialType_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_PartMaterialType_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPMType // objPMType
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
        private void Mst_PartMaterialType_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPMType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartMaterialType_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPMType", objPMType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPMType = TUtils.CUtils.StdParam(objPMType);

            ////
            DataTable dtDB_Mst_PartMaterialType = null;
            {
                ////
                Mst_PartMaterialType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPMType // strSupCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartMaterialType // dtDB_Mst_Supplier
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_PartMaterialType.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_PartMaterialType"
                    , dtDB_Mst_PartMaterialType
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_BOMType:
        private void Mst_BOMType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objBOMType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_BOMType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_BOMType t --//[mylock]
					where (1=1)
						and t.BOMType = @objBOMType
					;
				");
            dtDB_Mst_BOMType = _cf.db.ExecQuery(
                strSqlExec
                , "@objBOMType", objBOMType
                ).Tables[0];
            dtDB_Mst_BOMType.TableName = "Mst_BOMType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_BOMType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.BOMType", objBOMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_BOMType_CheckDB_BOMTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_BOMType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.BOMType", objBOMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_BOMType_CheckDB_BOMTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_BOMType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.BOMType", objBOMType
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_BOMType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_BOMType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_BOMType_GetX(
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
            , string strRt_Cols_Mst_BOMType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_BOMType_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_BOMType", strRt_Cols_Mst_BOMType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_BOMType = (strRt_Cols_Mst_BOMType != null && strRt_Cols_Mst_BOMType.Length > 0);

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
						---- #tbl_Mst_BOMType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mpmtp.BOMType
						into #tbl_Mst_BOMType_Filter_Draft
						from Mst_BOMType mpmtp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mpmtp.BOMType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_BOMType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_BOMType_Filter:
						select
							t.*
						into #tbl_Mst_BOMType_Filter
						from #tbl_Mst_BOMType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_BOMType --------:
						zzB_Select_Mst_BOMType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_BOMType_Filter_Draft;
						--drop table #tbl_Mst_BOMType_Filter;
					"
                );
            ////
            string zzB_Select_Mst_BOMType_zzE = "-- Nothing.";
            if (bGet_Mst_BOMType)
            {
                #region // bGet_Mst_BOMType:
                zzB_Select_Mst_BOMType_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_BOMType:
                        select
                            t.MyIdxSeq
	                        , mpmtp.*
                        from #tbl_Mst_BOMType_Filter t --//[mylock]
	                        inner join Mst_BOMType mpmtp --//[mylock]
		                        on t.BOMType = mpmtp.BOMType
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
                        , "Mst_BOMType" // strTableNameDB
                        , "Mst_BOMType." // strPrefixStd
                        , "mpmtp." // strPrefixAlias
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
                , "zzB_Select_Mst_BOMType_zzE", zzB_Select_Mst_BOMType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_BOMType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_BOMType";
            }
            #endregion
        }

        public DataSet Mst_BOMType_Get(
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
            , string strRt_Cols_Mst_BOMType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_BOMType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_BOMType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_BOMType", strRt_Cols_Mst_BOMType
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_BOMType_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_BOMType_GetX(
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
                        , strRt_Cols_Mst_BOMType // strRt_Cols_Mst_BOMType
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

        public DataSet WAS_Mst_BOMType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_BOMType objRQ_Mst_BOMType
            ////
            , out RT_Mst_BOMType objRT_Mst_BOMType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_BOMType.Tid;
            objRT_Mst_BOMType = new RT_Mst_BOMType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_BOMType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_BOMType_Get;
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
                List<Mst_BOMType> lst_Mst_BOMType = new List<Mst_BOMType>();
                #endregion

                #region // WAS_Mst_BOMType_Get:
                mdsResult = Mst_BOMType_Get(
                    objRQ_Mst_BOMType.Tid // strTid
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    , objRQ_Mst_BOMType.WAUserCode // strUserCode
                    , objRQ_Mst_BOMType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_BOMType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_BOMType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_BOMType.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_Mst_BOMType.Rt_Cols_Mst_BOMType // strRt_Cols_Mst_BOMType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_BOMType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_BOMType = mdsResult.Tables["Mst_BOMType"].Copy();
                    lst_Mst_BOMType = TUtils.DataTableCmUtils.ToListof<Mst_BOMType>(dt_Mst_BOMType);
                    objRT_Mst_BOMType.Lst_Mst_BOMType = lst_Mst_BOMType;
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

        public DataSet WAS_Mst_BOMType_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_BOMType objRQ_Mst_BOMType
            ////
            , out RT_Mst_BOMType objRT_Mst_BOMType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_BOMType.Tid;
            objRT_Mst_BOMType = new RT_Mst_BOMType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_BOMType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_BOMType_Create;
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
                List<Mst_BOMType> lst_Mst_BOMType = new List<Mst_BOMType>();
                #endregion

                #region // Mst_BOMType_Create:
                mdsResult = Mst_BOMType_Create(
                    objRQ_Mst_BOMType.Tid // strTid
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    , objRQ_Mst_BOMType.WAUserCode // strUserCode
                    , objRQ_Mst_BOMType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_BOMType.Mst_BOMType.BOMType // objBOMType
                    , objRQ_Mst_BOMType.Mst_BOMType.BOMTypeDesc // objBOMTypeDesc
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
        public DataSet Mst_BOMType_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objBOMType
            , object objBOMTypeDesc
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_BOMType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_BOMType_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objBOMType", objBOMType
                    , "objBOMTypeDesc", objBOMTypeDesc
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

                #region // Mst_BOMType_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_BOMType_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objBOMType // objBOMType
                        , objBOMTypeDesc // objBOMTypeDesc
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
        private void Mst_BOMType_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objBOMType
            , object objBOMTypeDesc
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_BOMType_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objBOMType", objBOMType
                , "objBOMTypeDesc", objBOMTypeDesc
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strBOMType = TUtils.CUtils.StdParam(objBOMType);
            string strBOMTypeDesc = string.Format("{0}", objBOMTypeDesc).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_BOMType = null;

            {
                ////
                if (strBOMType == null || strBOMType.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strBOMType", strBOMType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_BOMType_CreateX_InvalidBOMType
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_BOMType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strBOMType // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_BOMType // dtDB_Mst_BOMType
                    );
                ////
                if (strBOMTypeDesc.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strBOMTypeDesc", strBOMTypeDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_BOMType_CreateX_InvalidBOMTypeDesc
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_BOMType:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_BOMType.NewRow();
                strFN = "BOMType"; drDB[strFN] = strBOMType;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "BOMTypeDesc"; drDB[strFN] = strBOMTypeDesc;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_BOMType.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_BOMType" // strTableName
                    , dtDB_Mst_BOMType // dtData
                                       //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }


        public DataSet WAS_Mst_BOMType_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_BOMType objRQ_Mst_BOMType
            ////
            , out RT_Mst_BOMType objRT_Mst_BOMType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_BOMType.Tid;
            objRT_Mst_BOMType = new RT_Mst_BOMType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_BOMType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_BOMType_Update;
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
                List<Mst_BOMType> lst_Mst_BOMType = new List<Mst_BOMType>();
                #endregion

                #region // Mst_BOMType_Update:
                mdsResult = Mst_BOMType_Update(
                    objRQ_Mst_BOMType.Tid // strTid
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    , objRQ_Mst_BOMType.WAUserCode // strUserCode
                    , objRQ_Mst_BOMType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_BOMType.Mst_BOMType.BOMType // objBOMType
                    , objRQ_Mst_BOMType.Mst_BOMType.BOMTypeDesc // objBOMTypeDesc
                    , objRQ_Mst_BOMType.Mst_BOMType.FlagActive // objFlagActive
                                                               ////
                    , objRQ_Mst_BOMType.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_BOMType_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objBOMType
            , object objBOMTypeDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_BOMType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_BOMType_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objBOMType", objBOMType
                    , "objBOMTypeDesc", objBOMTypeDesc
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_BOMType_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_BOMType_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objBOMType // objBOMType
                        , objBOMTypeDesc // objBOMTypeDesc
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
        private void Mst_BOMType_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objBOMType
            , object objBOMTypeDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_BOMType_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objBOMType", objBOMType
                , "objBOMTypeDesc", objBOMTypeDesc
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
            string strBOMType = TUtils.CUtils.StdParam(objBOMType);
            string strBOMTypeDesc = string.Format("{0}", objBOMTypeDesc).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_BOMType = strFt_Cols_Upd.Contains("Mst_BOMType.BOMType".ToUpper());
            bool bUpd_BOMTypeDesc = strFt_Cols_Upd.Contains("Mst_BOMType.BOMTypeDesc".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_BOMType.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_BOMType = null;
            {
                ////
                Mst_BOMType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strBOMType // objBOMType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_BOMType // dtDB_Mst_BOMType
                    );
                ////
                if (strBOMTypeDesc.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strBOMTypeDesc", strBOMTypeDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_BOMType_UpdateX_InvalidBOMTypeDesc
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
                DataRow drDB = dtDB_Mst_BOMType.Rows[0];
                if (bUpd_BOMType) { strFN = "BOMType"; drDB[strFN] = strBOMType; alColumnEffective.Add(strFN); }
                if (bUpd_BOMTypeDesc) { strFN = "BOMTypeDesc"; drDB[strFN] = strBOMTypeDesc; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_BOMType"
                    , dtDB_Mst_BOMType
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet WAS_Mst_BOMType_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_BOMType objRQ_Mst_BOMType
            ////
            , out RT_Mst_BOMType objRT_Mst_BOMType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_BOMType.Tid;
            objRT_Mst_BOMType = new RT_Mst_BOMType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_BOMType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_BOMType_Delete;
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
                List<Mst_BOMType> lst_Mst_BOMType = new List<Mst_BOMType>();
                #endregion

                #region // Mst_BOMType_Delete:
                mdsResult = Mst_BOMType_Delete(
                    objRQ_Mst_BOMType.Tid // strTid
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    , objRQ_Mst_BOMType.WAUserCode // strUserCode
                    , objRQ_Mst_BOMType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_BOMType.Mst_BOMType.BOMType // objBOMType
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
        public DataSet Mst_BOMType_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPartType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_BOMType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_BOMType_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
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

                #region // Mst_BOMType_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_BOMType_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
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
        private void Mst_BOMType_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_BOMType_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartType", objPartType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartType = TUtils.CUtils.StdParam(objPartType);

            ////
            DataTable dtDB_Mst_BOMType = null;
            {
                ////
                Mst_BOMType_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartType // strPartType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_BOMType // dtDB_Mst_BOMType
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_BOMType.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_BOMType"
                    , dtDB_Mst_BOMType
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_PartColor:
        private void Mst_PartColor_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPartColorCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_PartColor
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_PartColor t --//[mylock]
					where (1=1)
						and t.PartColorCode = @objPartColorCode
					;
				");
            dtDB_Mst_PartColor = _cf.db.ExecQuery(
                strSqlExec
                , "@objPartColorCode", objPartColorCode
                ).Tables[0];
            dtDB_Mst_PartColor.TableName = "Mst_PartColor";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_PartColor.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartColorCode", objPartColorCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartColor_CheckDB_PartColorCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_PartColor.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartColorCode", objPartColorCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartColor_CheckDB_PartColorCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_PartColor.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PartColorCode", objPartColorCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_PartColor.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_PartColor_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_PartColor_GetX(
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
            , string strRt_Cols_Mst_PartColor
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartColor_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartColor", strRt_Cols_Mst_PartColor
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_PartColor = (strRt_Cols_Mst_PartColor != null && strRt_Cols_Mst_PartColor.Length > 0);

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
						---- #tbl_Mst_PartColor_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mpc.PartColorCode
						into #tbl_Mst_PartColor_Filter_Draft
						from Mst_PartColor mpc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mpc.PartColorCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_PartColor_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_PartColor_Filter:
						select
							t.*
						into #tbl_Mst_PartColor_Filter
						from #tbl_Mst_PartColor_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_PartColor --------:
						zzB_Select_Mst_PartColor_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_PartColor_Filter_Draft;
						--drop table #tbl_Mst_PartColor_Filter;
					"
                );
            ////
            string zzB_Select_Mst_PartColor_zzE = "-- Nothing.";
            if (bGet_Mst_PartColor)
            {
                #region // bGet_Mst_PartColor:
                zzB_Select_Mst_PartColor_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_PartColor:
                        select
                            t.MyIdxSeq
	                        , mpc.*
                        from #tbl_Mst_PartColor_Filter t --//[mylock]
	                        inner join Mst_PartColor mpc --//[mylock]
		                        on t.PartColorCode = mpc.PartColorCode
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
                        , "Mst_PartColor" // strTableNameDB
                        , "Mst_PartColor." // strPrefixStd
                        , "mpc." // strPrefixAlias
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
                , "zzB_Select_Mst_PartColor_zzE", zzB_Select_Mst_PartColor_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_PartColor)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_PartColor";
            }
            #endregion
        }

        public DataSet Mst_PartColor_Get(
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
            , string strRt_Cols_Mst_PartColor
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartColor_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartColor_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartColor", strRt_Cols_Mst_PartColor
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartColor_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_PartColor_GetX(
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
                        , strRt_Cols_Mst_PartColor // strRt_Cols_Mst_PartColor
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

        public DataSet WAS_Mst_PartColor_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartColor objRQ_Mst_PartColor
            ////
            , out RT_Mst_PartColor objRT_Mst_PartColor
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartColor.Tid;
            objRT_Mst_PartColor = new RT_Mst_PartColor();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartColor_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartColor_Get;
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
                List<Mst_PartColor> lst_Mst_PartColor = new List<Mst_PartColor>();
                #endregion

                #region // WAS_Mst_PartColor_Get:
                mdsResult = Mst_PartColor_Get(
                    objRQ_Mst_PartColor.Tid // strTid
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    , objRQ_Mst_PartColor.WAUserCode // strUserCode
                    , objRQ_Mst_PartColor.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_PartColor.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_PartColor.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_PartColor.Ft_WhereClause // strFt_WhereClause
                                                         //// Return:
                    , objRQ_Mst_PartColor.Rt_Cols_Mst_PartColor // strRt_Cols_Mst_PartColor
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_PartColor.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_PartColor = mdsResult.Tables["Mst_PartColor"].Copy();
                    lst_Mst_PartColor = TUtils.DataTableCmUtils.ToListof<Mst_PartColor>(dt_Mst_PartColor);
                    objRT_Mst_PartColor.Lst_Mst_PartColor = lst_Mst_PartColor;
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


        public DataSet WAS_Mst_PartColor_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartColor objRQ_Mst_PartColor
            ////
            , out RT_Mst_PartColor objRT_Mst_PartColor
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartColor.Tid;
            objRT_Mst_PartColor = new RT_Mst_PartColor();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartColor_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartColor_Create;
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
                List<Mst_PartColor> lst_Mst_PartColor = new List<Mst_PartColor>();
                #endregion

                #region // Mst_PartColor_Create:
                mdsResult = Mst_PartColor_Create(
                    objRQ_Mst_PartColor.Tid // strTid
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    , objRQ_Mst_PartColor.WAUserCode // strUserCode
                    , objRQ_Mst_PartColor.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorCode // objPartColorCode
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorName // objPartColorName
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorNameVN // objPartColorNameVN
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
        public DataSet Mst_PartColor_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartColorCode
            , object objPartColorName
            , object objPartColorNameVN
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartColor_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartColor_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartColorCode", objPartColorCode
                    , "objPartColorName", objPartColorName
                    , "objPartColorNameVN", objPartColorNameVN
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

                #region // Mst_PartColor_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartColor_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartColorCode // objPartColorCode
                        , objPartColorName // objPartColorName
                        , objPartColorNameVN // objPartColorNameVN
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
        private void Mst_PartColor_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartColorCode
            , object objPartColorName
            , object objPartColorNameVN
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartColor_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPartColorCode", objPartColorCode
                , "objPartColorName", objPartColorName
                , "objPartColorNameVN", objPartColorNameVN
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartColorCode = TUtils.CUtils.StdParam(objPartColorCode);
            string strPartColorName = string.Format("{0}", objPartColorName).Trim();
            string strPartColorNameVN = string.Format("{0}", objPartColorNameVN).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_PartColor = null;

            {
                ////
                if (strPartColorCode == null || strPartColorCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartColorCode", strPartColorCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartColor_Create_InvalidPartColorCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_PartColor_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartColorCode // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_PartColor // dtDB_Mst_PartColor
                    );
                ////
                if (strPartColorName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartColorName", strPartColorName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartColor_Create_InvalidPartColorName
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
                DataRow drDB = dtDB_Mst_PartColor.NewRow();
                strFN = "PartColorCode"; drDB[strFN] = strPartColorCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "PartColorName"; drDB[strFN] = strPartColorName;
                strFN = "PartColorNameVN"; drDB[strFN] = strPartColorNameVN;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_PartColor.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartColor" // strTableName
                    , dtDB_Mst_PartColor // dtData
                                         //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_Mst_PartColor_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartColor objRQ_Mst_PartColor
            ////
            , out RT_Mst_PartColor objRT_Mst_PartColor
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartColor.Tid;
            objRT_Mst_PartColor = new RT_Mst_PartColor();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartColor_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartColor_Update;
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
                List<Mst_PartColor> lst_Mst_PartColor = new List<Mst_PartColor>();
                #endregion

                #region // Mst_PartColor_Update:
                mdsResult = Mst_PartColor_Update(
                    objRQ_Mst_PartColor.Tid // strTid
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    , objRQ_Mst_PartColor.WAUserCode // strUserCode
                    , objRQ_Mst_PartColor.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorCode // objPartColorCode
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorName // objPartColorCode
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorNameVN //objPartColorName
                    , objRQ_Mst_PartColor.Mst_PartColor.FlagActive // objFlagActive
                                                                   ////
                    , objRQ_Mst_PartColor.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_PartColor_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartColorCode
            , object objPartColorName
            , object objPartColorNameVN
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartColor_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartColor_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartColorCode", objPartColorCode
                    , "objPartColorName", objPartColorName
                    , "objPartColorNameVN", objPartColorNameVN
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartColor_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartColor_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartColorCode // objPartColorCode
                        , objPartColorName // objPartColorName
                        , objPartColorNameVN // objPartColorNameVN
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
        private void Mst_PartColor_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartColorCode
            , object objPartColorName
            , object objPartColorNameVN
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartColor_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPartColorCode", objPartColorCode
                , "objPartColorName", objPartColorName
                , "objPartColorNameVN", objPartColorNameVN
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
            string strPartColorCode = TUtils.CUtils.StdParam(objPartColorCode);
            string strPartColorName = string.Format("{0}", objPartColorName).Trim();
            string strPartColorNameVN = string.Format("{0}", objPartColorNameVN).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_PartColorCode = strFt_Cols_Upd.Contains("Mst_PartColor.PartColorCode".ToUpper());
            bool bUpd_PartColorName = strFt_Cols_Upd.Contains("Mst_PartColor.PartColorName".ToUpper());
            bool bUpd_PartColorNameVN = strFt_Cols_Upd.Contains("Mst_PartColor.PartColorNameVN".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_PartColor.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_PartColor = null;
            {
                ////
                Mst_PartColor_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartColorCode // objPartColorCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartColor // dtDB_Mst_PartColor
                    );
                ////
                if (strPartColorName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartColorName", strPartColorName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartColor_UpdateX_InvalidPartColorName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }

            #endregion

            #region // Save Mst_PartColor:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_PartColor.Rows[0];
                if (bUpd_PartColorCode) { strFN = "PartColorCode"; drDB[strFN] = strPartColorCode; alColumnEffective.Add(strFN); }
                if (bUpd_PartColorName) { strFN = "PartColorName"; drDB[strFN] = strPartColorName; alColumnEffective.Add(strFN); }
                if (bUpd_PartColorNameVN) { strFN = "PartColorNameVN"; drDB[strFN] = strPartColorNameVN; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartColor"
                    , dtDB_Mst_PartColor
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet WAS_Mst_PartColor_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartColor objRQ_Mst_PartColor
            ////
            , out RT_Mst_PartColor objRT_Mst_PartColor
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartColor.Tid;
            objRT_Mst_PartColor = new RT_Mst_PartColor();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartColor_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartColor_Delete;
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
                List<Mst_PartColor> lst_Mst_PartColor = new List<Mst_PartColor>();
                #endregion

                #region // Mst_PartColor_Delete:
                mdsResult = Mst_PartColor_Delete(
                    objRQ_Mst_PartColor.Tid // strTid
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    , objRQ_Mst_PartColor.WAUserCode // strUserCode
                    , objRQ_Mst_PartColor.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartColor.Mst_PartColor.PartColorCode // objPartColorCode
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
        public DataSet Mst_PartColor_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPartColorCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartColor_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartColor_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartColorCode", objPartColorCode
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

                #region // Mst_PartColor_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_PartColor_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartColorCode // objPartColorCode
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
        private void Mst_PartColor_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartColorCode
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartColor_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartColorCode", objPartColorCode
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartColorCode = TUtils.CUtils.StdParam(objPartColorCode);

            ////
            DataTable dtDB_Mst_PartColor = null;
            {
                ////
                Mst_PartColor_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartColorCode // strSupCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartColor // dtDB_Mst_Supplier
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_PartColor.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_PartColor"
                    , dtDB_Mst_PartColor
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_PartUnit:
        private void Mst_PartUnit_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPartUnitCode
            , string strFlagExistToCheck
            , string strFlagUnitStdListToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_PartUnit
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_PartUnit t --//[mylock]
					where (1=1)
						and t.PartUnitCode = @objPartUnitCode
					;
				");
            dtDB_Mst_PartUnit = _cf.db.ExecQuery(
                strSqlExec
                , "@objPartUnitCode", objPartUnitCode
                ).Tables[0];
            dtDB_Mst_PartUnit.TableName = "Mst_PartUnit";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_PartUnit.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartUnitCode", objPartUnitCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartUnit_CheckDB_PartUnitCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_PartUnit.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartUnitCode", objPartUnitCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartUnit_CheckDB_PartUnitCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_PartUnit.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PartUnitCode", objPartUnitCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_PartUnit.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_PartUnit_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            // strFlagUnitStdListToCheck:
            if (strFlagUnitStdListToCheck.Length > 0 && !strFlagUnitStdListToCheck.Contains(Convert.ToString(dtDB_Mst_PartUnit.Rows[0]["FlagUnitStd"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PartUnitCode", objPartUnitCode
                    , "Check.strFlagUnitStdListToCheck", strFlagUnitStdListToCheck
                    , "DB.FlagUnitStd", dtDB_Mst_PartUnit.Rows[0]["FlagUnitStd"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_PartUnit_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_PartUnit_GetX(
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
            , string strRt_Cols_Mst_PartUnit
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartUnit_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartUnit", strRt_Cols_Mst_PartUnit
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_PartUnit = (strRt_Cols_Mst_PartUnit != null && strRt_Cols_Mst_PartUnit.Length > 0);

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
						---- #tbl_Mst_PartUnit_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mpu.PartUnitCode
						into #tbl_Mst_PartUnit_Filter_Draft
						from Mst_PartUnit mpu --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mpu.PartUnitCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_PartUnit_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_PartUnit_Filter:
						select
							t.*
						into #tbl_Mst_PartUnit_Filter
						from #tbl_Mst_PartUnit_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_PartUnit --------:
						zzB_Select_Mst_PartUnit_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_PartUnit_Filter_Draft;
						--drop table #tbl_Mst_PartUnit_Filter;
					"
                );
            ////
            string zzB_Select_Mst_PartUnit_zzE = "-- Nothing.";
            if (bGet_Mst_PartUnit)
            {
                #region // bGet_Mst_PartUnit:
                zzB_Select_Mst_PartUnit_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_PartUnit:
                        select
                            t.MyIdxSeq
	                        , mpu.*
                        from #tbl_Mst_PartUnit_Filter t --//[mylock]
	                        inner join Mst_PartUnit mpu --//[mylock]
		                        on t.PartUnitCode = mpu.PartUnitCode
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
                        , "Mst_PartUnit" // strTableNameDB
                        , "Mst_PartUnit." // strPrefixStd
                        , "mpu." // strPrefixAlias
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
                , "zzB_Select_Mst_PartUnit_zzE", zzB_Select_Mst_PartUnit_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_PartUnit)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_PartUnit";
            }
            #endregion
        }

        public DataSet Mst_PartUnit_Get(
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
            , string strRt_Cols_Mst_PartUnit
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_PartUnit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartUnit_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PartUnit", strRt_Cols_Mst_PartUnit
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartUnit_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_PartUnit_GetX(
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
                        , strRt_Cols_Mst_PartUnit // strRt_Cols_Mst_PartUnit
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

        public DataSet WAS_Mst_PartUnit_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartUnit objRQ_Mst_PartUnit
            ////
            , out RT_Mst_PartUnit objRT_Mst_PartUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartUnit.Tid;
            objRT_Mst_PartUnit = new RT_Mst_PartUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartUnit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartUnit_Get;
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
                List<Mst_PartUnit> lst_Mst_PartUnit = new List<Mst_PartUnit>();
                #endregion

                #region // WAS_Mst_PartUnit_Get:
                mdsResult = Mst_PartUnit_Get(
                    objRQ_Mst_PartUnit.Tid // strTid
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    , objRQ_Mst_PartUnit.WAUserCode // strUserCode
                    , objRQ_Mst_PartUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_PartUnit.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_PartUnit.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_PartUnit.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_PartUnit.Rt_Cols_Mst_PartUnit // strRt_Cols_Mst_PartUnit
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_PartUnit.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_PartUnit = mdsResult.Tables["Mst_PartUnit"].Copy();
                    lst_Mst_PartUnit = TUtils.DataTableCmUtils.ToListof<Mst_PartUnit>(dt_Mst_PartUnit);
                    objRT_Mst_PartUnit.Lst_Mst_PartUnit = lst_Mst_PartUnit;
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


        public DataSet WAS_Mst_PartUnit_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartUnit objRQ_Mst_PartUnit
            ////
            , out RT_Mst_PartUnit objRT_Mst_PartUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartUnit.Tid;
            objRT_Mst_PartUnit = new RT_Mst_PartUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartUnit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartUnit_Create;
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
                List<Mst_PartUnit> lst_Mst_PartUnit = new List<Mst_PartUnit>();
                #endregion

                #region // Mst_PartUnit_Create:
                mdsResult = Mst_PartUnit_Create(
                    objRQ_Mst_PartUnit.Tid // strTid
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    , objRQ_Mst_PartUnit.WAUserCode // strUserCode
                    , objRQ_Mst_PartUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartUnit.Mst_PartUnit.PartUnitCode // objPartUnitCode
                    , objRQ_Mst_PartUnit.Mst_PartUnit.PartUnitName // objPartUnitName
                    , objRQ_Mst_PartUnit.Mst_PartUnit.FlagUnitStd // objFlagUnitStd
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
        public DataSet Mst_PartUnit_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartUnitCode
            , object objPartUnitName
            , object objFlagUnitStd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartUnit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartUnit_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartUnitCode", objPartUnitCode
                    , "objPartUnitName", objPartUnitName
                    , "objFlagUnitStd", objFlagUnitStd
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

                #region // Mst_PartUnit_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartUnit_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartUnitCode // objPartUnitCode
                        , objPartUnitName // objPartUnitName
                        , objFlagUnitStd // objFlagUnitStd
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
        private void Mst_PartUnit_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartUnitCode
            , object objPartUnitName
            , object objFlagUnitStd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartUnit_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPartUnitCode", objPartUnitCode
                , "objPartUnitName", objPartUnitName
                , "objFlagUnitStd", objFlagUnitStd
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartUnitCode = TUtils.CUtils.StdParam(objPartUnitCode);
            string strPartUnitName = string.Format("{0}", objPartUnitName).Trim();
            string strFlagUnitStd = TUtils.CUtils.StdFlag(objFlagUnitStd);

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_PartUnit = null;

            {
                ////
                if (strPartUnitCode == null || strPartUnitCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartUnitCode", strPartUnitCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartUnit_CreateX_InvalidPartUnitCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_PartUnit_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartUnitCode // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagUnitStdListToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_PartUnit // dtDB_Mst_PartUnit
                    );
                ////
                if (strPartUnitName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartUnitName", strPartUnitName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartUnit_CreateX_InvalidPartUnitName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_PartUnit:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_PartUnit.NewRow();
                strFN = "PartUnitCode"; drDB[strFN] = strPartUnitCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "PartUnitName"; drDB[strFN] = strPartUnitName;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "FlagUnitStd"; drDB[strFN] = strFlagUnitStd;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_PartUnit.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartUnit" // strTableName
                    , dtDB_Mst_PartUnit // dtData
                                        //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_Mst_PartUnit_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartUnit objRQ_Mst_PartUnit
            ////
            , out RT_Mst_PartUnit objRT_Mst_PartUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartUnit.Tid;
            objRT_Mst_PartUnit = new RT_Mst_PartUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartUnit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartUnit_Update;
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
                List<Mst_PartUnit> lst_Mst_PartUnit = new List<Mst_PartUnit>();
                #endregion

                #region // Mst_PartUnit_Update:
                mdsResult = Mst_PartUnit_Update(
                    objRQ_Mst_PartUnit.Tid // strTid
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    , objRQ_Mst_PartUnit.WAUserCode // strUserCode
                    , objRQ_Mst_PartUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartUnit.Mst_PartUnit.PartUnitCode // objPartUnitCode
                    , objRQ_Mst_PartUnit.Mst_PartUnit.PartUnitName // objPartUnitName
                    , objRQ_Mst_PartUnit.Mst_PartUnit.FlagUnitStd // objFlagUnitStd
                    , objRQ_Mst_PartUnit.Mst_PartUnit.FlagActive // objFlagActive
                                                                 ////
                    , objRQ_Mst_PartUnit.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_PartUnit_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartUnitCode
            , object objPartUnitName
            , object objFlagUnitStd
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartUnit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartUnit_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartUnitCode", objPartUnitCode
                    , "objPartUnitName", objPartUnitName
                    , "objFlagUnitStd", objFlagUnitStd
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_PartUnit_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_PartUnit_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartUnitCode // objPartUnitCode
                        , objPartUnitName // objPartUnitName
                        , objFlagUnitStd // objFlagUnitStd
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
        private void Mst_PartUnit_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartUnitCode
            , object objPartUnitName
            , object objFlagUnitStd
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartUnit_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPartUnitCode", objPartUnitCode
                , "objPartUnitName", objPartUnitName
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
            string strPartUnitCode = TUtils.CUtils.StdParam(objPartUnitCode);
            string strPartUnitName = string.Format("{0}", objPartUnitName).Trim();
            string strFlagUnitStd = TUtils.CUtils.StdFlag(objFlagUnitStd);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_PartUnitCode = strFt_Cols_Upd.Contains("Mst_PartUnit.PartUnitCode".ToUpper());
            bool bUpd_PartUnitName = strFt_Cols_Upd.Contains("Mst_PartUnit.PartUnitName".ToUpper());
            bool bUpd_FlagUnitStd = strFt_Cols_Upd.Contains("Mst_PartUnit.FlagUnitStd".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_PartUnit.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_PartUnit = null;
            {
                ////
                Mst_PartUnit_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartUnitCode // objPartUnitCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagUnitStdListToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartUnit // dtDB_Mst_PartUnit
                    );
                ////
                if (strPartUnitName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartUnitName", strPartUnitName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_PartUnit_UpdateX_InvalidPartUnitName
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
                DataRow drDB = dtDB_Mst_PartUnit.Rows[0];
                if (bUpd_PartUnitCode) { strFN = "PartUnitCode"; drDB[strFN] = strPartUnitCode; alColumnEffective.Add(strFN); }
                if (bUpd_PartUnitName) { strFN = "PartUnitName"; drDB[strFN] = strPartUnitName; alColumnEffective.Add(strFN); }
                if (bUpd_FlagUnitStd) { strFN = "FlagUnitStd"; drDB[strFN] = strFlagUnitStd; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_PartUnit"
                    , dtDB_Mst_PartUnit
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet WAS_Mst_PartUnit_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_PartUnit objRQ_Mst_PartUnit
            ////
            , out RT_Mst_PartUnit objRT_Mst_PartUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_PartUnit.Tid;
            objRT_Mst_PartUnit = new RT_Mst_PartUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_PartUnit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_PartUnit_Delete;
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
                List<Mst_PartUnit> lst_Mst_PartUnit = new List<Mst_PartUnit>();
                #endregion

                #region // Mst_PartUnit_Delete:
                mdsResult = Mst_PartUnit_Delete(
                    objRQ_Mst_PartUnit.Tid // strTid
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    , objRQ_Mst_PartUnit.WAUserCode // strUserCode
                    , objRQ_Mst_PartUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_PartUnit.Mst_PartUnit.PartUnitCode // objPartUnitCode
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
        public DataSet Mst_PartUnit_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPartType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_PartUnit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_PartUnit_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
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

                #region // Mst_PartUnit_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_PartUnit_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
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
        private void Mst_PartUnit_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_PartUnit_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartType", objPartType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartType = TUtils.CUtils.StdParam(objPartType);

            ////
            DataTable dtDB_Mst_PartUnit = null;
            {
                ////
                Mst_PartUnit_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartType // strPartType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagUnitStdListToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_PartUnit // dtDB_Mst_PartUnit
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_PartUnit.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_PartUnit"
                    , dtDB_Mst_PartUnit
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_Part:
        private void Mst_Part_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPartCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Part
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Part t --//[mylock]
					where (1=1)
						and t.PartCode = @objPartCode
					;
				");
            dtDB_Mst_Part = _cf.db.ExecQuery(
                strSqlExec
                , "@objPartCode", objPartCode
                ).Tables[0];
            dtDB_Mst_Part.TableName = "Mst_Part";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Part.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartCode", objPartCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CheckDB_PartNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Part.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PartCode", objPartCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CheckDB_PartExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Part.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PartCode", objPartCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Part.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Part_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        private void Mst_Part_GetX(
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
            , string strRt_Cols_Mst_Part
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_Part_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Part", strRt_Cols_Mst_Part
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_Part = (strRt_Cols_Mst_Part != null && strRt_Cols_Mst_Part.Length > 0);

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
						---- #tbl_Mst_Part_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mp.PartCode
						into #tbl_Mst_Part_Filter_Draft
						from Mst_Part mp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mp.PartCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Part_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Part_Filter:
						select
							t.*
						into #tbl_Mst_Part_Filter
						from #tbl_Mst_Part_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Part --------:
						zzB_Select_Mst_Part_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Part_Filter_Draft;
						--drop table #tbl_Mst_Part_Filter;
					"
                );
            ////
            string zzB_Select_Mst_Part_zzE = "-- Nothing.";
            if (bGet_Mst_Part)
            {
                #region // bGet_Mst_Part:
                zzB_Select_Mst_Part_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_Part:
                        select
                            t.MyIdxSeq
	                        , mp.*
                        from #tbl_Mst_Part_Filter t --//[mylock]
	                        inner join Mst_Part mp --//[mylock]
		                        on t.PartCode = mp.PartCode
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
                        , "Mst_Part" // strTableNameDB
                        , "Mst_Part." // strPrefixStd
                        , "mp." // strPrefixAlias
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
                , "zzB_Select_Mst_Part_zzE", zzB_Select_Mst_Part_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_Part)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_Part";
            }
            #endregion
        }

        public DataSet Mst_Part_Get(
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
            , string strRt_Cols_Mst_Part
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Part_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Part_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Part", strRt_Cols_Mst_Part
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_Part_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Mst_Part_GetX(
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
                        , strRt_Cols_Mst_Part // strRt_Cols_Mst_Part
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

        public DataSet WAS_Mst_Part_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Part objRQ_Mst_Part
            ////
            , out RT_Mst_Part objRT_Mst_Part
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Part.Tid;
            objRT_Mst_Part = new RT_Mst_Part();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Part_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Part_Get;
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
                List<Mst_Part> lst_Mst_Part = new List<Mst_Part>();
                #endregion

                #region // WAS_Mst_Part_Get:
                mdsResult = Mst_Part_Get(
                    objRQ_Mst_Part.Tid // strTid
                    , objRQ_Mst_Part.GwUserCode // strGwUserCode
                    , objRQ_Mst_Part.GwPassword // strGwPassword
                    , objRQ_Mst_Part.WAUserCode // strUserCode
                    , objRQ_Mst_Part.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Part.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Part.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Part.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Mst_Part.Rt_Cols_Mst_Part // strRt_Cols_Mst_Part
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Part.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_Part = mdsResult.Tables["Mst_Part"].Copy();
                    lst_Mst_Part = TUtils.DataTableCmUtils.ToListof<Mst_Part>(dt_Mst_Part);
                    objRT_Mst_Part.Lst_Mst_Part = lst_Mst_Part;
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
        public DataSet WAS_Mst_Part_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Part objRQ_Mst_Part
            ////
            , out RT_Mst_Part objRT_Mst_Part
            )
            {
                #region // Temp:
                string strTid = objRQ_Mst_Part.Tid;
                objRT_Mst_Part = new RT_Mst_Part();
                DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
                DateTime dtimeSys = DateTime.UtcNow;
                //DataSet mdsExec = null;
                //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Part.Tid);
                //int nTidSeq = 0;
                //bool bNeedTransaction = true;
                string strFunctionName = "WAS_Mst_Part_Create";
                string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Part_Create;
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
                    List<Mst_Part> lst_Mst_Part = new List<Mst_Part>();
                    #endregion

                    #region // Mst_Part_Create:
                    mdsResult = Mst_Part_Create(
                        objRQ_Mst_Part.Tid // strTid
                        , objRQ_Mst_Part.GwUserCode // strGwUserCode
                        , objRQ_Mst_Part.GwPassword // strGwPassword
                        , objRQ_Mst_Part.WAUserCode // strUserCode
                        , objRQ_Mst_Part.WAUserPassword // strUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                                                    ////
                        , objRQ_Mst_Part.Mst_Part.PartCode // objPartCode
                        , objRQ_Mst_Part.Mst_Part.PartBarCode // objPartBarCode
                        , objRQ_Mst_Part.Mst_Part.PartName // objPartName
                        , objRQ_Mst_Part.Mst_Part.PartNameFS // objPartNameFS
                        , objRQ_Mst_Part.Mst_Part.PartDesc // objPartDesc
                        , objRQ_Mst_Part.Mst_Part.PartType // objPartType
                        , objRQ_Mst_Part.Mst_Part.PMType // objPMType
                        , objRQ_Mst_Part.Mst_Part.PartUnitCodeStd // objPartUnitCodeStd
                        , objRQ_Mst_Part.Mst_Part.PartUnitCodeDefault // objPartUnitCodeDefault
                        , objRQ_Mst_Part.Mst_Part.QtyMaxSt // objQtyMaxSt
                        , objRQ_Mst_Part.Mst_Part.QtyMinSt // objQtyMinSt
                        , objRQ_Mst_Part.Mst_Part.QtyEffSt // objQtyEffSt
                        , objRQ_Mst_Part.Mst_Part.UPIn // objUPIn
                        , objRQ_Mst_Part.Mst_Part.UPOut // objUPOut
                        , objRQ_Mst_Part.Mst_Part.FilePath // objFilePath
                        , objRQ_Mst_Part.Mst_Part.ImagePath // objImagePath
                        , objRQ_Mst_Part.Mst_Part.QtyEffMonth // objQtyEffMonth
                        , objRQ_Mst_Part.Mst_Part.PartOrigin // objPartOrigin
                        , objRQ_Mst_Part.Mst_Part.PartComponents // objPartComponents
                        , objRQ_Mst_Part.Mst_Part.InstructionForUse // objInstructionForUse
                        , objRQ_Mst_Part.Mst_Part.PartStorage // objPartStorage
                        , objRQ_Mst_Part.Mst_Part.UrlMnfSequence // objUrlMnfSequence
                        , objRQ_Mst_Part.Mst_Part.MnfStandard // objMnfStandard
                        , objRQ_Mst_Part.Mst_Part.PartStyle // objPartStyle
                        , objRQ_Mst_Part.Mst_Part.PartIntroduction // objPartIntroduction
                        , objRQ_Mst_Part.Mst_Part.FlagBOM // objFlagBOM
                        , objRQ_Mst_Part.Mst_Part.FlagVirtual // objFlagVirtual
                        , objRQ_Mst_Part.Mst_Part.FlagInputLot // objFlagInputLot
                        , objRQ_Mst_Part.Mst_Part.FlagInputSerial // objFlagInputSerial
                        , objRQ_Mst_Part.Mst_Part.RemarkForEffUsed // objRemarkForEffUsed 
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
        public DataSet Mst_Part_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartCode
            , object objPartBarCode
            , object objPartName
            , object objPartNameFS
            , object objPartDesc
            , object objPartType
            , object objPMType
            , object objPartUnitCodeStd
            , object objPartUnitCodeDefault
            , object objQtyMaxSt
            , object objQtyMinSt
            , object objQtyEffSt
            , object objUPIn
            , object objUPOut
            , object objFilePath
            , object objImagePath
            , object objQtyEffMonth
            , object objPartOrigin
            , object objPartComponents
            , object objInstructionForUse
            , object objPartStorage
            , object objUrlMnfSequence
            , object objMnfStandard
            , object objPartStyle
            , object objPartIntroduction
            , object objFlagBOM
            , object objFlagVirtual
            , object objFlagInputLot
            , object objFlagInputSerial
            , object objRemarkForEffUsed
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Part_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Part_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objPartCode", objPartCode
                    , "objPartBarCode", objPartBarCode
                    , "objPartName", objPartName
                    , "objPartNameFS", objPartNameFS
                    , "objPartDesc", objPartDesc
                    , "objPartType", objPartType
                    , "objPMType", objPMType
                    , "objPartUnitCodeStd", objPartUnitCodeStd
                    , "objPartUnitCodeDefault", objPartUnitCodeDefault
                    , "objQtyMaxSt", objQtyMaxSt
                    , "objQtyMinSt", objQtyMinSt
                    , "objQtyEffSt", objQtyEffSt
                    , "objUPIn", objUPIn
                    , "objUPOut", objUPOut
                    , "objFilePath", objFilePath
                    , "objImagePath", objImagePath
                    , "objQtyEffMonth", objQtyEffMonth
                    , "objPartOrigin", objPartOrigin
                    , "objPartComponents", objPartComponents
                    , "objInstructionForUse", objInstructionForUse
                    , "objPartStorage", objPartStorage
                    , "objUrlMnfSequence", objUrlMnfSequence
                    , "objMnfStandard", objMnfStandard
                    , "objPartStyle", objPartStyle
                    , "objPartIntroduction", objPartIntroduction
                    , "objFlagBOM", objFlagBOM
                    , "objFlagVirtual", objFlagVirtual
                    , "objFlagInputLot", objFlagInputLot
                    , "objFlagInputSerial", objFlagInputSerial
                    , "objRemarkForEffUsed", objRemarkForEffUsed
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

                #region // Mst_Part_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_Part_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartCode // objPartCode
                        , objPartBarCode // objPartBarCode
                        , objPartName // objPartName
                        , objPartNameFS // objPartNameFS
                        , objPartDesc // objPartDesc
                        , objPartType // objPartType
                        , objPMType // objPMType
                        , objPartUnitCodeStd // objPartUnitCodeStd
                        , objPartUnitCodeDefault // objPartUnitCodeDefault
                        , objQtyMaxSt // objQtyMaxSt
                        , objQtyMinSt // objQtyMinSt
                        , objQtyEffSt // objQtyEffSt
                        , objUPIn // objUPIn
                        , objUPOut // objUPOut
                        , objFilePath // objFilePath
                        , objImagePath // objImagePath
                        , objQtyEffMonth // objQtyEffMonth
                        , objPartOrigin // objPartOrigin
                        , objPartComponents // objPartComponents
                        , objInstructionForUse // objInstructionForUse
                        , objPartStorage // objPartStorage
                        , objUrlMnfSequence // objUrlMnfSequence
                        , objMnfStandard // objMnfStandard
                        , objPartStyle // objPartStyle
                        , objPartIntroduction // objPartIntroduction
                        , objFlagBOM // objFlagBOM
                        , objFlagVirtual // objFlagVirtual
                        , objFlagInputLot // objFlagInputLot
                        , objFlagInputSerial // objFlagInputSerial
                        , objRemarkForEffUsed // objRemarkForEffUsed
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
        private void Mst_Part_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartCode
            , object objPartBarCode
            , object objPartName
            , object objPartNameFS
            , object objPartDesc
            , object objPartType
            , object objPMType
            , object objPartUnitCodeStd
            , object objPartUnitCodeDefault
            , object objQtyMaxSt
            , object objQtyMinSt
            , object objQtyEffSt
            , object objUPIn
            , object objUPOut
            , object objFilePath
            , object objImagePath
            , object objQtyEffMonth
            , object objPartOrigin
            , object objPartComponents
            , object objInstructionForUse
            , object objPartStorage
            , object objUrlMnfSequence
            , object objMnfStandard
            , object objPartStyle
            , object objPartIntroduction
            , object objFlagBOM
            , object objFlagVirtual
            , object objFlagInputLot
            , object objFlagInputSerial
            , object objRemarkForEffUsed
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Part_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPartCode", objPartCode
                , "objPartBarCode", objPartBarCode
                , "objPartName", objPartName
                , "objPartNameFS", objPartNameFS
                , "objPartDesc", objPartDesc
                , "objPartType", objPartType
                , "objPMType", objPMType
                , "objPartUnitCodeStd", objPartUnitCodeStd
                , "objPartUnitCodeDefault", objPartUnitCodeDefault
                , "objQtyMaxSt", objQtyMaxSt
                , "objQtyMinSt", objQtyMinSt
                , "objQtyEffSt", objQtyEffSt
                , "objUPIn", objUPIn
                , "objUPOut", objUPOut
                , "objFilePath", objFilePath
                , "objImagePath", objImagePath
                , "objQtyEffMonth", objQtyEffMonth
                , "objPartOrigin", objPartOrigin
                , "objPartComponents", objPartComponents
                , "objInstructionForUse", objInstructionForUse
                , "objPartStorage", objPartStorage
                , "objUrlMnfSequence", objUrlMnfSequence
                , "objMnfStandard", objMnfStandard
                , "objPartStyle", objPartStyle
                , "objPartIntroduction", objPartIntroduction
                , "objFlagBOM", objFlagBOM
                , "objFlagVirtual", objFlagVirtual
                , "objFlagInputLot", objFlagInputLot
                , "objFlagInputSerial", objFlagInputSerial
                , "objRemarkForEffUsed", objRemarkForEffUsed
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartCode = TUtils.CUtils.StdParam(objPartCode);
            string strPartBarCode = TUtils.CUtils.StdParam(objPartBarCode);
            string strPartName = string.Format("{0}", objPartName).Trim();
            string strPartNameFS = string.Format("{0}", objPartNameFS).Trim();
            string strPartDesc = string.Format("{0}", objPartDesc).Trim();
            string strPartType = TUtils.CUtils.StdParam(objPartType);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strPartUnitCodeStd = TUtils.CUtils.StdParam(objPartUnitCodeStd);
            string strPartUnitCodeDefault = TUtils.CUtils.StdParam(objPartUnitCodeDefault);
            string strFilePath = string.Format("{0}", objFilePath).Trim();
            string strImagePath = string.Format("{0}", objImagePath).Trim();
            string strPartOrigin = string.Format("{0}", objPartOrigin).Trim();
            string strPartComponents = string.Format("{0}", objPartComponents).Trim();
            string strInstructionForUse = string.Format("{0}", objInstructionForUse).Trim();
            string strPartStorage = string.Format("{0}", objPartStorage).Trim();
            string strUrlMnfSequence = string.Format("{0}", objUrlMnfSequence).Trim();
            string strMnfStandard = string.Format("{0}", objMnfStandard).Trim();
            //string strPartSpecification = string.Format("{0}", string strPartSpecification).Trim();
            string strPartStyle = string.Format("{0}", objPartStyle).Trim();
            string strPartIntroduction = string.Format("{0}", objPartIntroduction).Trim();
            string strFlagBOM = TUtils.CUtils.StdFlag(objFlagBOM);
            string strFlagVirtual = TUtils.CUtils.StdFlag(objFlagVirtual);
            string strFlagInputLot = TUtils.CUtils.StdFlag(objFlagInputLot);
            string strFlagInputSerial = TUtils.CUtils.StdFlag(objFlagInputSerial);
            string strRemarkForEffUsed = string.Format("{0}", objRemarkForEffUsed);

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_Part = null;
            {
                ////
                if (strPartCode == null || strPartCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartCode", strPartCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CreateX_InvalidPartCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_Part_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartCode // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Part // dtDB_Mst_Part
                    );
                ////
                DataTable dtDB_Mst_PartType = null;

                Mst_PartType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartType // objPartType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_PartType // dtDB_Mst_PartType
                    );
                ////
                DataTable dtDB_Mst_PartMaterialType = null;

                Mst_PartMaterialType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPMType // objPMType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_PartMaterialType // dtDB_Mst_PartMaterialType
                    );
                ////
                DataTable dtDB_Mst_PartUnit_Std = null;

                Mst_PartUnit_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartUnitCodeStd // objPartUnitCodeStd
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagUnitStdListToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_PartUnit_Std // dtDB_Mst_PartUnit_Std
                    );
                ////
                DataTable dtDB_Mst_PartUnit_Default = null;

                Mst_PartUnit_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPartUnitCodeDefault // objPartUnitCodeDefault
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagUnitStdListToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_PartUnit_Default // dtDB_Mst_PartUnit_Default
                    );
                ////
                if (strPartName == null || strPartName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartName", strPartName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CreateX_InvalidPartName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                //// 20200210: By ThomPTT nâng cấp nghiệp vụ không bắt buộc nhập tên tiếng anh
                //if (strPartNameFS == null || strPartNameFS.Length < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strPartNameFS", strPartNameFS
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Mst_Part_CreateX_InvalidPartNameFS
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                ////
                //if (strPartNameFS == null || strPartNameFS.Length < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strPartNameFS", strPartNameFS
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Mst_Part_CreateX_InvalidPartNameFS
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                ////
                double dblQtyMaxSt = Convert.ToDouble(objQtyMaxSt);
                if (dblQtyMaxSt < 0.0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblQtyMaxSt", dblQtyMaxSt
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CreateX_InvalidQtyMaxSt
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                /////
                double dblQtyMinSt = Convert.ToDouble(objQtyMinSt);
                if (dblQtyMinSt < 0.0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblQtyMinSt", objQtyMinSt
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CreateX_InvalidQtyMinSt
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                /////
                double dblQtyEffSt = Convert.ToDouble(objQtyEffSt);
                if (dblQtyEffSt < 0.0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblQtyEffSt", dblQtyEffSt
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_CreateX_InvalidQtyEffSt
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // SaveDB Mst_Part:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_Part.NewRow();
                strFN = "PartCode"; drDB[strFN] = strPartCode;
                strFN = "PartBarCode"; drDB[strFN] = strPartBarCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "PartName"; drDB[strFN] = strPartName;
                strFN = "PartNameFS"; drDB[strFN] = strPartNameFS;
                strFN = "PartDesc"; drDB[strFN] = strPartDesc;
                strFN = "PartType"; drDB[strFN] = strPartType;
                strFN = "PMType"; drDB[strFN] = strPMType;
                strFN = "PartUnitCodeStd"; drDB[strFN] = strPartUnitCodeStd;
                strFN = "PartUnitCodeDefault"; drDB[strFN] = strPartUnitCodeDefault;
                strFN = "QtyMaxSt"; drDB[strFN] = objQtyMaxSt;
                strFN = "QtyMinSt"; drDB[strFN] = objQtyMinSt;
                strFN = "QtyEffSt"; drDB[strFN] = objQtyEffSt;
                strFN = "UPIn"; drDB[strFN] = objUPIn;
                strFN = "UPOut"; drDB[strFN] = objUPOut;
                strFN = "FilePath"; drDB[strFN] = strFilePath;
                strFN = "ImagePath"; drDB[strFN] = strImagePath;
                strFN = "QtyEffMonth"; drDB[strFN] = objQtyEffMonth;
                strFN = "PartOrigin"; drDB[strFN] = strPartOrigin;
                strFN = "PartComponents"; drDB[strFN] = strPartComponents;
                strFN = "InstructionForUse"; drDB[strFN] = strInstructionForUse;
                strFN = "PartStorage"; drDB[strFN] = strPartStorage;
                strFN = "UrlMnfSequence"; drDB[strFN] = strUrlMnfSequence;
                strFN = "MnfStandard"; drDB[strFN] = strMnfStandard;
                strFN = "PartStyle"; drDB[strFN] = strPartStyle;
                strFN = "PartIntroduction"; drDB[strFN] = strPartIntroduction;
                strFN = "FlagBOM"; drDB[strFN] = strFlagBOM;
                strFN = "FlagVirtual"; drDB[strFN] = strFlagVirtual;
                strFN = "FlagInputLot"; drDB[strFN] = strFlagInputLot;
                strFN = "FlagInputSerial"; drDB[strFN] = strFlagInputSerial;
                strFN = "RemarkForEffUsed"; drDB[strFN] = strRemarkForEffUsed;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_Part.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_Part" // strTableName
                    , dtDB_Mst_Part // dtData
                                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_Mst_Part_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Part objRQ_Mst_Part
            ////
            , out RT_Mst_Part objRT_Mst_Part
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Part.Tid;
            objRT_Mst_Part = new RT_Mst_Part();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Part.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Part_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Part_Update;
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
                List<Mst_Part> lst_Mst_Part = new List<Mst_Part>();
                #endregion

                #region // Mst_Part_Update:
                mdsResult = Mst_Part_Update(
                    objRQ_Mst_Part.Tid // strTid
                    , objRQ_Mst_Part.GwUserCode // strGwUserCode
                    , objRQ_Mst_Part.GwPassword // strGwPassword
                    , objRQ_Mst_Part.WAUserCode // strUserCode
                    , objRQ_Mst_Part.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Part.Mst_Part.PartCode
                    , objRQ_Mst_Part.Mst_Part.PartBarCode
                    , objRQ_Mst_Part.Mst_Part.PartName
                    , objRQ_Mst_Part.Mst_Part.PartNameFS
                    , objRQ_Mst_Part.Mst_Part.PartDesc
                    //, object objPartType
                    //, object objPMType
                    //, object objPartUnitCodeStd
                    //, object objPartUnitCodeDefault
                    , objRQ_Mst_Part.Mst_Part.QtyMaxSt
                    , objRQ_Mst_Part.Mst_Part.QtyMinSt
                    , objRQ_Mst_Part.Mst_Part.QtyEffSt
                    , objRQ_Mst_Part.Mst_Part.UPIn
                    , objRQ_Mst_Part.Mst_Part.UPOut
                    , objRQ_Mst_Part.Mst_Part.FilePath
                    , objRQ_Mst_Part.Mst_Part.ImagePath
                    , objRQ_Mst_Part.Mst_Part.QtyEffMonth
                    , objRQ_Mst_Part.Mst_Part.PartOrigin
                    , objRQ_Mst_Part.Mst_Part.PartComponents
                    , objRQ_Mst_Part.Mst_Part.InstructionForUse
                    , objRQ_Mst_Part.Mst_Part.PartStorage
                    , objRQ_Mst_Part.Mst_Part.UrlMnfSequence
                    , objRQ_Mst_Part.Mst_Part.MnfStandard
                    , objRQ_Mst_Part.Mst_Part.PartStyle
                    , objRQ_Mst_Part.Mst_Part.PartIntroduction
                    , objRQ_Mst_Part.Mst_Part.FlagBOM
                    , objRQ_Mst_Part.Mst_Part.FlagVirtual
                    , objRQ_Mst_Part.Mst_Part.FlagInputLot
                    , objRQ_Mst_Part.Mst_Part.FlagInputSerial
                    , objRQ_Mst_Part.Mst_Part.RemarkForEffUsed
                    , objRQ_Mst_Part.Mst_Part.FlagActive // objFlagActive
                                                         ////
                    , objRQ_Mst_Part.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet Mst_Part_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objPartCode
            , object objPartBarCode
            , object objPartName
            , object objPartNameFS
            , object objPartDesc
            //, object objPartType
            //, object objPMType
            //, object objPartUnitCodeStd
            //, object objPartUnitCodeDefault
            , object objQtyMaxSt
            , object objQtyMinSt
            , object objQtyEffSt
            , object objUPIn
            , object objUPOut
            , object objFilePath
            , object objImagePath
            , object objQtyEffMonth
            , object objPartOrigin
            , object objPartComponents
            , object objInstructionForUse
            , object objPartStorage
            , object objUrlMnfSequence
            , object objMnfStandard
            , object objPartStyle
            , object objPartIntroduction
            , object objFlagBOM
            , object objFlagVirtual
            , object objFlagInputLot
            , object objFlagInputSerial
            , object objRemarkForEffUsed
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Part_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Part_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objPartCode", objPartCode
                    , "objPartBarCode", objPartBarCode
                    , "objPartName", objPartName
                    , "objPartNameFS", objPartNameFS
                    , "objPartDesc", objPartDesc
                    //, "objPartType", objPartType
                    //, "objPMType", objPMType
                    //, "objPartUnitCodeStd", objPartUnitCodeStd
                    //, "objPartUnitCodeDefault", objPartUnitCodeDefault
                    , "objQtyMaxSt", objQtyMaxSt
                    , "objQtyMinSt", objQtyMinSt
                    , "objQtyEffSt", objQtyEffSt
                    , "objUPIn", objUPIn
                    , "objUPOut", objUPOut
                    , "objFilePath", objFilePath
                    , "objImagePath", objImagePath
                    , "objQtyEffMonth", objQtyEffMonth
                    , "objPartOrigin", objPartOrigin
                    , "objPartComponents", objPartComponents
                    , "objInstructionForUse", objInstructionForUse
                    , "objPartStorage", objPartStorage
                    , "objUrlMnfSequence", objUrlMnfSequence
                    , "objMnfStandard", objMnfStandard
                    , "objPartStyle", objPartStyle
                    , "objPartIntroduction", objPartIntroduction
                    , "objFlagBOM", objFlagBOM
                    , "objFlagVirtual", objFlagVirtual
                    , "objFlagInputLot", objFlagInputLot
                    , "objFlagInputSerial", objFlagInputSerial
                    , "objRemarkForEffUsed", objRemarkForEffUsed
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_Part_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_Part_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartCode // objPartCode
                        , objPartBarCode // objPartBarCode
                        , objPartName // objPartName
                        , objPartNameFS // objPartNameFS
                        , objPartDesc // objPartDesc
                        //, objPartType // objPartType
                        //, objPMType // objPMType
                        //, objPartUnitCodeStd // objPartUnitCodeStd
                        //, objPartUnitCodeDefault // objPartUnitCodeDefault
                        , objQtyMaxSt // objQtyMaxSt
                        , objQtyMinSt // objQtyMinSt
                        , objQtyEffSt // objQtyEffSt
                        , objUPIn // objUPIn
                        , objUPOut // objUPOut
                        , objFilePath // objFilePath
                        , objImagePath // objImagePath
                        , objQtyEffMonth // objQtyEffMonth
                        , objPartOrigin // objPartOrigin
                        , objPartComponents // objPartComponents
                        , objInstructionForUse // objInstructionForUse
                        , objPartStorage // objPartStorage
                        , objUrlMnfSequence // objUrlMnfSequence
                        , objMnfStandard // objMnfStandard
                        , objPartStyle // objPartStyle
                        , objPartIntroduction // objPartIntroduction
                        , objFlagBOM // objFlagBOM
                        , objFlagVirtual // objFlagVirtual
                        , objFlagInputLot // objFlagInputLot
                        , objFlagInputSerial // objFlagInputSerial
                        , objRemarkForEffUsed // objRemarkForEffUsed
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
        private void Mst_Part_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartCode
            , object objPartBarCode
            , object objPartName
            , object objPartNameFS
            , object objPartDesc
            //, object objPartType
            //, object objPMType
            //, object objPartUnitCodeStd
            //, object objPartUnitCodeDefault
            , object objQtyMaxSt
            , object objQtyMinSt
            , object objQtyEffSt
            , object objUPIn
            , object objUPOut
            , object objFilePath
            , object objImagePath
            , object objQtyEffMonth
            , object objPartOrigin
            , object objPartComponents
            , object objInstructionForUse
            , object objPartStorage
            , object objUrlMnfSequence
            , object objMnfStandard
            , object objPartStyle
            , object objPartIntroduction
            , object objFlagBOM
            , object objFlagVirtual
            , object objFlagInputLot
            , object objFlagInputSerial
            , object objRemarkForEffUsed
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Part_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPartCode", objPartCode
                , "objPartBarCode", objPartBarCode
                , "objPartName", objPartName
                , "objPartNameFS", objPartNameFS
                , "objPartDesc", objPartDesc
                //, "objPartType", objPartType
                //, "objPMType", objPMType
                //, "objPartUnitCodeStd", objPartUnitCodeStd
                //, "objPartUnitCodeDefault", objPartUnitCodeDefault
                , "objQtyMaxSt", objQtyMaxSt
                , "objQtyMinSt", objQtyMinSt
                , "objQtyEffSt", objQtyEffSt
                , "objUPIn", objUPIn
                , "objUPOut", objUPOut
                , "objFilePath", objFilePath
                , "objImagePath", objImagePath
                , "objQtyEffMonth", objQtyEffMonth
                , "objPartOrigin", objPartOrigin
                , "objPartComponents", objPartComponents
                , "objInstructionForUse", objInstructionForUse
                , "objPartStorage", objPartStorage
                , "objUrlMnfSequence", objUrlMnfSequence
                , "objMnfStandard", objMnfStandard
                , "objPartStyle", objPartStyle
                , "objPartIntroduction", objPartIntroduction
                , "objFlagBOM", objFlagBOM
                , "objFlagVirtual", objFlagVirtual
                , "objFlagInputLot", objFlagInputLot
                , "objFlagInputSerial", objFlagInputSerial
                , "objRemarkForEffUsed", objRemarkForEffUsed
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
            string strPartCode = TUtils.CUtils.StdParam(objPartCode);
            string strPartBarCode = TUtils.CUtils.StdParam(objPartBarCode);
            string strPartName = string.Format("{0}", objPartName).Trim();
            string strPartNameFS = string.Format("{0}", objPartNameFS).Trim();
            string strPartDesc = string.Format("{0}", objPartDesc).Trim();
            //string strPartType = TUtils.CUtils.StdParam(objPartType);
            //string strPMType = TUtils.CUtils.StdParam(objPMType);
            //string strPartUnitCodeStd = TUtils.CUtils.StdParam(objPartUnitCodeStd);
           // string strPartUnitCodeDefault = TUtils.CUtils.StdParam(objPartUnitCodeDefault);
            string strFilePath = string.Format("{0}", objFilePath).Trim();
            string strImagePath = string.Format("{0}", objImagePath).Trim();
            string strPartOrigin = string.Format("{0}", objPartOrigin).Trim();
            string strPartComponents = string.Format("{0}", objPartComponents).Trim();
            string strInstructionForUse = string.Format("{0}", objInstructionForUse).Trim();
            string strPartStorage = string.Format("{0}", objPartStorage).Trim();
            string strUrlMnfSequence = string.Format("{0}", objUrlMnfSequence).Trim();
            string strMnfStandard = string.Format("{0}", objMnfStandard).Trim();
            //string strPartSpecification = string.Format("{0}", string strPartSpecification).Trim();
            string strPartStyle = string.Format("{0}", objPartStyle).Trim();
            string strPartIntroduction = string.Format("{0}", objPartIntroduction).Trim();
            string strFlagBOM = TUtils.CUtils.StdFlag(objFlagBOM);
            string strFlagVirtual = TUtils.CUtils.StdFlag(objFlagVirtual);
            string strFlagInputLot = TUtils.CUtils.StdFlag(objFlagInputLot);
            string strFlagInputSerial = TUtils.CUtils.StdFlag(objFlagInputSerial);
            string strRemarkForEffUsed = string.Format("{0}", objRemarkForEffUsed).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_PartCode = strFt_Cols_Upd.Contains("Mst_Part.PartCode".ToUpper());
            bool bUpd_PartBarCode = strFt_Cols_Upd.Contains("Mst_Part.PartBarCode".ToUpper());
            bool bUpd_PartName = strFt_Cols_Upd.Contains("Mst_Part.PartName".ToUpper());
            bool bUpd_PartNameFS = strFt_Cols_Upd.Contains("Mst_Part.PartNameFS".ToUpper());
            bool bUpd_PartDesc = strFt_Cols_Upd.Contains("Mst_Part.PartDesc".ToUpper());
            bool bUpd_QtyMaxSt = strFt_Cols_Upd.Contains("Mst_Part.QtyMaxSt".ToUpper());
            bool bUpd_QtyMinSt = strFt_Cols_Upd.Contains("Mst_Part.QtyMinSt".ToUpper());
            bool bUpd_QtyEffSt = strFt_Cols_Upd.Contains("Mst_Part.QtyEffSt".ToUpper());
            bool bUpd_UPIn = strFt_Cols_Upd.Contains("Mst_Part.UPIn".ToUpper());
            bool bUpd_UPOut = strFt_Cols_Upd.Contains("Mst_Part.UPOut".ToUpper());
            bool bUpd_FilePath = strFt_Cols_Upd.Contains("Mst_Part.FilePath".ToUpper());
            bool bUpd_ImagePath = strFt_Cols_Upd.Contains("Mst_Part.ImagePath".ToUpper());
            bool bUpd_QtyEffMonth = strFt_Cols_Upd.Contains("Mst_Part.QtyEffMonth".ToUpper());
            bool bUpd_PartOrigin = strFt_Cols_Upd.Contains("Mst_Part.PartOrigin".ToUpper());
            bool bUpd_PartComponents = strFt_Cols_Upd.Contains("Mst_Part.PartComponents".ToUpper());
            bool bUpd_InstructionForUse = strFt_Cols_Upd.Contains("Mst_Part.InstructionForUse".ToUpper());
            bool bUpd_PartStorage = strFt_Cols_Upd.Contains("Mst_Part.PartStorage".ToUpper());
            bool bUpd_UrlMnfSequence = strFt_Cols_Upd.Contains("Mst_Part.UrlMnfSequence".ToUpper());
            bool bUpd_MnfStandard = strFt_Cols_Upd.Contains("Mst_Part.MnfStandard".ToUpper());
            bool bUpd_PartStyle = strFt_Cols_Upd.Contains("Mst_Part.PartStyle".ToUpper());
            bool bUpd_PartIntroduction = strFt_Cols_Upd.Contains("Mst_Part.PartIntroduction".ToUpper());
            bool bUpd_FlagBOM = strFt_Cols_Upd.Contains("Mst_Part.FlagBOM".ToUpper());
            bool bUpd_FlagVirtual = strFt_Cols_Upd.Contains("Mst_Part.FlagVirtual".ToUpper());
            bool bUpd_FlagInputLot = strFt_Cols_Upd.Contains("Mst_Part.FlagInputLot".ToUpper());
            bool bUpd_FlagInputSerial = strFt_Cols_Upd.Contains("Mst_Part.FlagInputSerial".ToUpper());
            bool bUpd_RemarkForEffUsed = strFt_Cols_Upd.Contains("Mst_Part.RemarkForEffUsed".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Part.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_Part = null;
            {
                ////
                Mst_Part_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartCode // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Part // dtDB_Mst_Part
                    );
                ////
                if (strPartName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPartName", strPartName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Part_Update_InvalidPartName
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
                DataRow drDB = dtDB_Mst_Part.Rows[0];
                if (bUpd_PartBarCode) { strFN = "PartBarCode"; drDB[strFN] = strPartBarCode; alColumnEffective.Add(strFN); }
                if (bUpd_PartName) { strFN = "PartName"; drDB[strFN] = strPartName; alColumnEffective.Add(strFN); }
                if (bUpd_PartNameFS) { strFN = "PartNameFS"; drDB[strFN] = strPartNameFS; alColumnEffective.Add(strFN); }
                if (bUpd_PartDesc) { strFN = "PartDesc"; drDB[strFN] = strPartDesc; alColumnEffective.Add(strFN); }
                if (bUpd_QtyMaxSt) { strFN = "QtyMaxSt"; drDB[strFN] = objQtyMaxSt; alColumnEffective.Add(strFN); }
                if (bUpd_QtyMinSt) { strFN = "QtyMinSt"; drDB[strFN] = objQtyMinSt; alColumnEffective.Add(strFN); }
                if (bUpd_QtyEffSt) { strFN = "QtyEffSt"; drDB[strFN] = objQtyEffSt; alColumnEffective.Add(strFN); }
                if (bUpd_UPIn) { strFN = "UPIn"; drDB[strFN] = objUPIn; alColumnEffective.Add(strFN); }
                if (bUpd_UPOut) { strFN = "UPOut"; drDB[strFN] = objUPOut; alColumnEffective.Add(strFN); }
                if (bUpd_FilePath) { strFN = "FilePath"; drDB[strFN] = strFilePath; alColumnEffective.Add(strFN); }
                if (bUpd_ImagePath) { strFN = "ImagePath"; drDB[strFN] = strImagePath; alColumnEffective.Add(strFN); }
                if (bUpd_QtyEffMonth) { strFN = "QtyEffMonth"; drDB[strFN] = objQtyEffMonth; alColumnEffective.Add(strFN); }
                if (bUpd_PartOrigin) { strFN = "PartOrigin"; drDB[strFN] = strPartOrigin; alColumnEffective.Add(strFN); }
                if (bUpd_PartComponents) { strFN = "PartComponents"; drDB[strFN] = strPartComponents; alColumnEffective.Add(strFN); }
                if (bUpd_InstructionForUse) { strFN = "InstructionForUse"; drDB[strFN] = strInstructionForUse; alColumnEffective.Add(strFN); }
                if (bUpd_PartStorage) { strFN = "PartStorage"; drDB[strFN] = strPartStorage; alColumnEffective.Add(strFN); }
                if (bUpd_UrlMnfSequence) { strFN = "UrlMnfSequence"; drDB[strFN] = strUrlMnfSequence; alColumnEffective.Add(strFN); }
                if (bUpd_MnfStandard) { strFN = "MnfStandard"; drDB[strFN] = strMnfStandard; alColumnEffective.Add(strFN); }
                if (bUpd_PartStyle) { strFN = "PartStyle"; drDB[strFN] = strPartStyle; alColumnEffective.Add(strFN); }
                if (bUpd_PartIntroduction) { strFN = "PartIntroduction"; drDB[strFN] = strPartIntroduction; alColumnEffective.Add(strFN); }
                if (bUpd_FlagBOM) { strFN = "FlagBOM"; drDB[strFN] = strFlagBOM; alColumnEffective.Add(strFN); }
                if (bUpd_FlagVirtual) { strFN = "FlagVirtual"; drDB[strFN] = strFlagVirtual; alColumnEffective.Add(strFN); }
                if (bUpd_FlagInputLot) { strFN = "FlagInputLot"; drDB[strFN] = strFlagInputLot; alColumnEffective.Add(strFN); }
                if (bUpd_FlagInputSerial) { strFN = "FlagInputSerial"; drDB[strFN] = strFlagInputSerial; alColumnEffective.Add(strFN); }
                if (bUpd_RemarkForEffUsed) { strFN = "RemarkForEffUsed"; drDB[strFN] = strRemarkForEffUsed; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Mst_Part"
                    , dtDB_Mst_Part
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }
        public DataSet WAS_Mst_Part_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Part objRQ_Mst_Part
            ////
            , out RT_Mst_Part objRT_Mst_Part
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Part.Tid;
            objRT_Mst_Part = new RT_Mst_Part();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Part.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Part_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Part_Delete;
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
                List<Mst_Part> lst_Mst_Part = new List<Mst_Part>();
                #endregion

                #region // Mst_Part_Delete:
                mdsResult = Mst_Part_Delete(
                    objRQ_Mst_Part.Tid // strTid
                    , objRQ_Mst_Part.GwUserCode // strGwUserCode
                    , objRQ_Mst_Part.GwPassword // strGwPassword
                    , objRQ_Mst_Part.WAUserCode // strUserCode
                    , objRQ_Mst_Part.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Part.Mst_Part.PartCode // objPartUnitCode
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
        public DataSet Mst_Part_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPartType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Part_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Part_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartType", objPartType
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

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_Part_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPartType // objPartType
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
        private void Mst_Part_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objPartType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Part_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartType", objPartType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strPartType = TUtils.CUtils.StdParam(objPartType);

            ////
            DataTable dtDB_Mst_Part = null;
            {
                ////
                Mst_Part_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strPartType // strPartType 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Part // dtDB_Mst_Part
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_Part.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_Part"
                    , dtDB_Mst_Part
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_MapPartColor:
        private void Mst_MapPartColor_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objPartCode
			, object objPartColorCode
			, string strFlagExistToCheck
			, string strFlagDefaultListToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_MapPartColor
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_MapPartColor t --//[mylock]
					where (1=1)
						and t.PartCode = @objPartCode
						and t.PartColorCode = @objPartColorCode
					;
				");
			dtDB_Mst_MapPartColor = _cf.db.ExecQuery(
				strSqlExec
				, "@objPartCode", objPartCode
				, "@objPartColorCode", objPartColorCode
				).Tables[0];
			dtDB_Mst_MapPartColor.TableName = "Mst_MapPartColor";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_MapPartColor.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.PartCode", objPartCode
						, "Check.PartColorCode", objPartColorCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MapPartColor_CheckDB_MapPartColorNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_MapPartColor.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.PartCode", objPartCode
						, "Check.PartColorCode", objPartColorCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MapPartColor_CheckDB_MapPartColorExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagDefaultListToCheck:
			if (strFlagDefaultListToCheck.Length > 0 && !strFlagDefaultListToCheck.Contains(Convert.ToString(dtDB_Mst_MapPartColor.Rows[0]["FlagDefault"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.PartCode", objPartCode
					, "Check.PartColorCode", objPartColorCode
					, "Check.strFlagDefaultListToCheck", strFlagDefaultListToCheck
					, "DB.FlagDefault", dtDB_Mst_MapPartColor.Rows[0]["FlagDefault"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_MapPartColor_CheckDB_FlagDefaultNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_MapPartColor.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.PartCode", objPartCode
					, "Check.PartColorCode", objPartColorCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_MapPartColor.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_MapPartColor_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		private void Mst_MapPartColor_GetX(
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
			, string strRt_Cols_Mst_MapPartColor
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_MapPartColor_GetX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
					//// Return
					, "strRt_Cols_Mst_MapPartColor", strRt_Cols_Mst_MapPartColor
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Mst_MapPartColor = (strRt_Cols_Mst_MapPartColor != null && strRt_Cols_Mst_MapPartColor.Length > 0);

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
						---- #tbl_Mst_MapPartColor_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mmpcl.PartCode
							, mmpcl.PartColorCode
						into #tbl_Mst_MapPartColor_Filter_Draft
						from Mst_MapPartColor mmpcl --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mmpcl.PartCode asc 
							, mmpcl.PartColorCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_MapPartColor_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_MapPartColor_Filter:
						select
							t.*
						into #tbl_Mst_MapPartColor_Filter
						from #tbl_Mst_MapPartColor_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_MapPartColor --------:
						zzB_Select_Mst_MapPartColor_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_MapPartColor_Filter_Draft;
						--drop table #tbl_Mst_MapPartColor_Filter;
					"
				);
			////
			string zzB_Select_Mst_MapPartColor_zzE = "-- Nothing.";
			if (bGet_Mst_MapPartColor)
			{
				#region // bGet_Mst_MapPartColor:
				zzB_Select_Mst_MapPartColor_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Mst_MapPartColor:
                        select
                            t.MyIdxSeq
	                        , mmpcl.*
                        from #tbl_Mst_MapPartColor_Filter t --//[mylock]
	                        inner join Mst_MapPartColor mmpcl --//[mylock]
		                        on t.PartCode = mmpcl.PartCode
									and t.PartColorCode = mmpcl.PartColorCode
							left join Mst_PartColor mpc --//[mylock]
								on t.PartColorCode = mpc.PartColorCode
							left join Mst_Part mp --//[mylock]
								on t.PartCode = mp.PartCode
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
						, "Mst_MapPartColor" // strTableNameDB
						, "Mst_MapPartColor." // strPrefixStd
						, "mmpcl." // strPrefixAlias
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
				, "zzB_Select_Mst_MapPartColor_zzE", zzB_Select_Mst_MapPartColor_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_MapPartColor)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_MapPartColor";
			}
			#endregion
		}

		public DataSet Mst_MapPartColor_Get(
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
			, string strRt_Cols_Mst_MapPartColor
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Mst_MapPartColor_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_MapPartColor_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_MapPartColor", strRt_Cols_Mst_MapPartColor
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

				#region // Mst_MapPartColor_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_MapPartColor_GetX(
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
						, strRt_Cols_Mst_MapPartColor // strRt_Cols_Mst_MapPartColor
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

		public DataSet WAS_Mst_MapPartColor_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_MapPartColor objRQ_Mst_MapPartColor
			////
			, out RT_Mst_MapPartColor objRT_Mst_MapPartColor
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_MapPartColor.Tid;
			objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_MapPartColor_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_MapPartColor_Get;
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
				List<Mst_MapPartColor> lst_Mst_MapPartColor = new List<Mst_MapPartColor>();
				#endregion

				#region // WAS_Mst_MapPartColor_Get:
				mdsResult = Mst_MapPartColor_Get(
					objRQ_Mst_MapPartColor.Tid // strTid
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					, objRQ_Mst_MapPartColor.WAUserCode // strUserCode
					, objRQ_Mst_MapPartColor.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_MapPartColor.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_MapPartColor.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_MapPartColor.Ft_WhereClause // strFt_WhereClause
															//// Return:
					, objRQ_Mst_MapPartColor.Rt_Cols_Mst_MapPartColor // strRt_Cols_Mst_MapPartColor
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_MapPartColor.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_MapPartColor = mdsResult.Tables["Mst_MapPartColor"].Copy();
					lst_Mst_MapPartColor = TUtils.DataTableCmUtils.ToListof<Mst_MapPartColor>(dt_Mst_MapPartColor);
					objRT_Mst_MapPartColor.Lst_Mst_MapPartColor = lst_Mst_MapPartColor;
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

		public DataSet WAS_Mst_MapPartColor_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_MapPartColor objRQ_Mst_MapPartColor
			////
			, out RT_Mst_MapPartColor objRT_Mst_MapPartColor
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_MapPartColor.Tid;
			objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_MapPartColor_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_MapPartColor_Create;
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
				List<Mst_MapPartColor> lst_Mst_MapPartColor = new List<Mst_MapPartColor>();
				#endregion

				#region // WS_Mst_MapPartColor_Create:
				mdsResult = Mst_MapPartColor_Create(
					objRQ_Mst_MapPartColor.Tid // strTid
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					, objRQ_Mst_MapPartColor.WAUserCode // strUserCode
					, objRQ_Mst_MapPartColor.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartCode // objPartCode
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartColorCode // objPartColorCode
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
		public DataSet Mst_MapPartColor_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objPartCode
			, object objPartColorCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_MapPartColor_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_MapPartColor_Create;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartCode", objPartCode
					, "objPartColorCode", objPartColorCode
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

				#region // Mst_MapPartColor_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_MapPartColor_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objPartCode // objPartCode
						, objPartColorCode // objPartColorCode
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

		private void Mst_MapPartColor_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objPartCode
			, object objPartColorCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_MapPartColor_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objPartCode", objPartCode
				, "objPartColorCode", objPartColorCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strPartCode = TUtils.CUtils.StdParam(objPartCode);
			string strPartColorCode = TUtils.CUtils.StdParam(objPartColorCode);

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_MapPartColor = null;

			{
				////
				if (strPartCode == null || strPartCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strPartCode", strPartCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MapPartColor_Create_InvalidPartCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strPartColorCode == null || strPartColorCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strPartCode", strPartCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_MapPartColor_Create_InvalidPartColorCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_MapPartColor_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strPartCode // objPartCode
					, strPartColorCode // objPartColorCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagDefaultListToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_MapPartColor // dtDB_Mst_MapPartColor
					);
				////
			}
			#endregion

			#region // SaveDB Mst_MapPartColor:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_MapPartColor.NewRow();
				strFN = "PartCode"; drDB[strFN] = strPartCode;
				strFN = "PartColorCode"; drDB[strFN] = strPartColorCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "FlagDefault"; drDB[strFN] = TConst.Flag.Active;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_MapPartColor.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_MapPartColor" // strTableName
					, dtDB_Mst_MapPartColor // dtData
					//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		public DataSet WAS_Mst_MapPartColor_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_MapPartColor objRQ_Mst_MapPartColor
			////
			, out RT_Mst_MapPartColor objRT_Mst_MapPartColor
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_MapPartColor.Tid;
			objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_MapPartColor_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_MapPartColor_Update;
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
				List<Mst_MapPartColor> lst_Mst_MapPartColor = new List<Mst_MapPartColor>();
				#endregion

				#region // Mst_MapPartColor_Update:
				mdsResult = Mst_MapPartColor_Update(
					objRQ_Mst_MapPartColor.Tid // strTid
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					, objRQ_Mst_MapPartColor.WAUserCode // strUserCode
					, objRQ_Mst_MapPartColor.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartCode // objPartCode
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartColorCode // objPartColorCode
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.FlagDefault // objFlagDefault
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.FlagActive // objFlagActive
					////
					, objRQ_Mst_MapPartColor.Ft_Cols_Upd // Ft_Cols_Upd
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
		public DataSet Mst_MapPartColor_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			//// 
			, object objPartCode
			, object objPartColorCode
			, object objFlagDefault
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_MapPartColor_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_MapPartColor_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartCode", objPartCode
					, "objPartColorCode", objPartColorCode
					, "objFlagDefault", objFlagDefault
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
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_MapPartColor_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_MapPartColor_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, objPartCode // objPartCode
						, objPartColorCode // objPartColorCode
						, objFlagDefault // objFlagDefault
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

		private void Mst_MapPartColor_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objPartCode
			, object objPartColorCode
			, object objFlagDefault
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_MapPartColor_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mst_MapPartColor_UpdateX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objPartCode", objPartCode
				, "objPartColorCode", objPartColorCode
				, "objFlagDefault", objFlagDefault
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
			string strPartCode = TUtils.CUtils.StdParam(objPartCode);
			string strPartColorCode = TUtils.CUtils.StdParam(objPartColorCode);
			string strFlagDefault = TUtils.CUtils.StdFlag(objFlagDefault);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_FlagDefault = strFt_Cols_Upd.Contains("Mst_MapPartColor.FlagDefault".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_MapPartColor.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_MapPartColor = null;
			{
				////
				Mst_MapPartColor_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , strPartCode // objPartCode 
					 , strPartColorCode // objPartColorCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagDefaultListToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_MapPartColor // dtDB_Mst_Organ
					);
				////
			}

			#endregion

			#region // Save Mst_MapPartColor:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_MapPartColor.Rows[0];
				if (bUpd_FlagDefault) { strFN = "FlagDefault"; drDB[strFN] = strFlagDefault; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_MapPartColor"
					, dtDB_Mst_MapPartColor
					, alColumnEffective.ToArray()
					);
			}
			#endregion

		}
		public DataSet WAS_Mst_MapPartColor_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_MapPartColor objRQ_Mst_MapPartColor
			////
			, out RT_Mst_MapPartColor objRT_Mst_MapPartColor
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_MapPartColor.Tid;
			objRT_Mst_MapPartColor = new RT_Mst_MapPartColor();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_MapPartColor.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_MapPartColor_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_MapPartColor_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Mst_MapPartColor", TJson.JsonConvert.SerializeObject(objRQ_Mst_MapPartColor.Mst_MapPartColor)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<Mst_MapPartColor> lst_Mst_MapPartColor = new List<Mst_MapPartColor>();

				//List<Mst_MapPartColorInGroup> lst_Mst_MapPartColorInGroup = new List<Mst_MapPartColorInGroup>();
				#endregion

				#region // Mst_MapPartColor_Delete:
				mdsResult = Mst_MapPartColor_Delete(
					objRQ_Mst_MapPartColor.Tid // strTid
					, objRQ_Mst_MapPartColor.GwUserCode // strGwUserCode
					, objRQ_Mst_MapPartColor.GwPassword // strGwPassword
					, objRQ_Mst_MapPartColor.WAUserCode // strUserCode
					, objRQ_Mst_MapPartColor.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartCode // objPartCode
					, objRQ_Mst_MapPartColor.Mst_MapPartColor.PartColorCode // objPartColorCode
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
		public DataSet Mst_MapPartColor_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objPartCode
			, object objPartColorCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_MapPartColor_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_MapPartColor_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objPartCode", objPartCode
					, "objPartColorCode", objPartColorCode
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

				#region // Mst_MapPartColor_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_MapPartColor_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objPartCode // objPartCode
						, objPartColorCode // objPartColorCode
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

		private void Mst_MapPartColor_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objPartCode
			, object objPartColorCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_MapPartColor_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objPartCode", objPartCode
				, "objPartColorCode", objPartColorCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strPartCode = TUtils.CUtils.StdParam(objPartCode);

			////
			DataTable dtDB_Mst_MapPartColor = null;
			{
				////
				Mst_MapPartColor_CheckDB(
					 ref alParamsCoupleError // alParamsCoupleError
					 , objPartCode // objPartCode 
					 , objPartColorCode // objPartColorCode
					 , TConst.Flag.Yes // strFlagExistToCheck
					 , "" // strFlagDefaultListToCheck
					 , "" // strFlagActiveListToCheck
					 , out dtDB_Mst_MapPartColor // dtDB_Mst_MapPartColor
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_MapPartColor.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_MapPartColor"
					, dtDB_Mst_MapPartColor
					);
			}
			#endregion
		}
		#endregion
	}
}
