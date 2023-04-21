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
using idn.Skycic.Inventory.BizService.Services;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Mst_Region
        private void Mst_Region_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objRegionCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Region
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Region t --//[mylock]
					where (1=1)
						and t.RegionCode = @objRegionCode
					;
				");
            dtDB_Mst_Region = _cf.db.ExecQuery(
                strSqlExec
                , "@objRegionCode", objRegionCode
                ).Tables[0];
            dtDB_Mst_Region.TableName = "Mst_Region";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Region.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.RegionCode", objRegionCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Region_CheckDB_RegionNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Region.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.RegionCode", objRegionCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Region_CheckDB_RegionExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Region.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.RegionCode", objRegionCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Region.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_Region_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Mst_Region_Get(
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
            , string strRt_Cols_Mst_Region
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Region_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_Region_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Region", strRt_Cols_Mst_Region
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
                bool bGet_Mst_Region = (strRt_Cols_Mst_Region != null && strRt_Cols_Mst_Region.Length > 0);

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
						---- #tbl_Mst_Region_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mr.RegionCode
						into #tbl_Mst_Region_Filter_Draft
						from Mst_Region mr --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mr.RegionCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Region_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Region_Filter:
						select
							t.*
						into #tbl_Mst_Region_Filter
						from #tbl_Mst_Region_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Province --------:
						zzB_Select_Mst_Region_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Region_Filter_Draft;
						--drop table #tbl_Mst_Region_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_Region_zzE = "-- Nothing.";
                if (bGet_Mst_Region)
                {
                    #region // bGet_Mst_Region:
                    zzB_Select_Mst_Region_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Region:
							select
								t.MyIdxSeq
								, mr.*
							from #tbl_Mst_Region_Filter t --//[mylock]
								inner join Mst_Region mr --//[mylock]
									on t.RegionCode = mr.RegionCode
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
                            , "Mst_Region" // strTableNameDB
                            , "Mst_Region." // strPrefixStd
                            , "mr." // strPrefixAlias
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
                    , "zzB_Select_Mst_Region_zzE", zzB_Select_Mst_Region_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_Region)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_Region";
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
        #endregion

        #region // Mst_Region: WAS
        public DataSet WAS_Mst_Region_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Region objRQ_Mst_Region
            ////
            , out DA_RT_Mst_Region objRT_Mst_Region
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Region.Tid;
            objRT_Mst_Region = new DA_RT_Mst_Region();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Region_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Region_Get;
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
                List<DA_Mst_Region> lst_Mst_Region = new List<DA_Mst_Region>();
                bool bGet_Mst_Region = (objRQ_Mst_Region.Rt_Cols_Mst_Region != null && objRQ_Mst_Region.Rt_Cols_Mst_Region.Length > 0);
                #endregion

                #region // WS_Mst_Region_Get:
                mdsResult = Mst_Region_Get(
                    objRQ_Mst_Region.Tid // strTid
                    , objRQ_Mst_Region.GwUserCode // strGwUserCode
                    , objRQ_Mst_Region.GwPassword // strGwPassword
                    , objRQ_Mst_Region.WAUserCode // strUserCode
                    , objRQ_Mst_Region.WAUserPassword // strUserPassword
                    //, TUtils.CUtils.StdFlag(objRQ_Mst_Region.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    //// Filter:
                    , objRQ_Mst_Region.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Region.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Region.Ft_WhereClause // strFt_WhereClause
                    //// Return:
                    , objRQ_Mst_Region.Rt_Cols_Mst_Region // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Region.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Region)
                    {
                        DataTable dt_Mst_Region = mdsResult.Tables["Mst_Region"].Copy();
                        lst_Mst_Region = TUtils.DataTableCmUtils.ToListof<DA_Mst_Region>(dt_Mst_Region);
                        objRT_Mst_Region.Lst_Mst_Region = lst_Mst_Region;
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
        #endregion

        #region // Mst_TourType
        private void Mst_TourType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objTourTypeCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_TourType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_TourType t --//[mylock]
					where (1=1)
						and t.TourType = @objTourType
					;
				");
            dtDB_Mst_TourType = _cf.db.ExecQuery(
                strSqlExec
                , "@objTourType", objTourTypeCode
                ).Tables[0];
            dtDB_Mst_TourType.TableName = "Mst_TourType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_TourType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourTypeCode", objTourTypeCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourType_CheckDB_TourTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_TourType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourTypeCode", objTourTypeCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourType_CheckDB_TourTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_TourType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.TourTypeCode", objTourTypeCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_TourType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_TourType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Mst_TourType_Get(
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
            , string strRt_Cols_Mst_TourType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TourType_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourType_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TourType", strRt_Cols_Mst_TourType
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
                bool bGet_Mst_TourType = (strRt_Cols_Mst_TourType != null && strRt_Cols_Mst_TourType.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_TourType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mtt.TourType
						into #tbl_Mst_TourType_Filter_Draft
						from Mst_TourType mtt --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mtt.TourType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TourType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_TourType_Filter:
						select
							t.*
						into #tbl_Mst_TourType_Filter
						from #tbl_Mst_TourType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Tour --------:
						zzB_Select_Mst_TourType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TourType_Filter_Draft;
						--drop table #tbl_Mst_TourType_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_TourType_zzE = "-- Nothing.";
                if (bGet_Mst_TourType)
                {
                    #region // bGet_Mst_TourType:
                    zzB_Select_Mst_TourType_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Tour:
							select
								t.MyIdxSeq
								, mtt.*
							from #tbl_Mst_TourType_Filter t --//[mylock]
								inner join Mst_TourType mtt --//[mylock]
									on t.TourType = mtt.TourType
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
                            , "Mst_TourType" // strTableNameDB
                            , "Mst_TourType." // strPrefixStd
                            , "mtt." // strPrefixAlias
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
                    , "zzB_Select_Mst_TourType_zzE", zzB_Select_Mst_TourType_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_TourType)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourType";
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
        #endregion

        #region // Mst_TourType: WAS
        public DataSet WAS_Mst_TourType_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourType objRQ_Mst_TourType
            ////
            , out DA_RT_Mst_TourType objRT_Mst_TourType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourType.Tid;
            objRT_Mst_TourType = new DA_RT_Mst_TourType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourType_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourType_Get;
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
                List<DA_Mst_TourType> lst_Mst_TourType = new List<DA_Mst_TourType>();
                bool bGet_Mst_TourType = (objRQ_Mst_TourType.Rt_Cols_Mst_TourType != null && objRQ_Mst_TourType.Rt_Cols_Mst_TourType.Length > 0);
                #endregion

                #region // WS_Mst_TourType_Get:
                mdsResult = Mst_TourType_Get(
                    objRQ_Mst_TourType.Tid // strTid
                    , objRQ_Mst_TourType.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourType.GwPassword // strGwPassword
                    , objRQ_Mst_TourType.WAUserCode // strUserCode
                    , objRQ_Mst_TourType.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_Mst_Region.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_TourType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_TourType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_TourType.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_TourType.Rt_Cols_Mst_TourType 
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TourType.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_TourType)
                    {
                        DataTable dt_Mst_TourType = mdsResult.Tables["Mst_TourType"].Copy();
                        lst_Mst_TourType = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourType>(dt_Mst_TourType);
                        objRT_Mst_TourType.Lst_Mst_TourType = lst_Mst_TourType;
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
        #endregion

        #region // Mst_Tour
        private void Mst_Tour_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objTourCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Tour
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Tour t --//[mylock]
					where (1=1)
						and t.TourCode = @objTourCode
					;
				");
            dtDB_Mst_Tour = _cf.db.ExecQuery(
                strSqlExec
                , "@objTourCode", objTourCode
                ).Tables[0];
            dtDB_Mst_Tour.TableName = "Mst_Tour";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Tour.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourCode", objTourCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CheckDB_TourNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Tour.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourCode", objTourCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CheckDB_TourExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Tour.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.TourCode", objTourCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Tour.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_Tour_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Mst_Tour_Get(
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
            , string strRt_Cols_Mst_Tour
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Tour_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_Tour_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Tour", strRt_Cols_Mst_Tour
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
                bool bGet_Mst_Tour = (strRt_Cols_Mst_Tour != null && strRt_Cols_Mst_Tour.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Region_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mt.TourCode
						into #tbl_Mst_Tour_Filter_Draft
						from Mst_Tour mt --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mt.TourCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Tour_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Tour_Filter:
						select
							t.*
						into #tbl_Mst_Tour_Filter
						from #tbl_Mst_Tour_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Tour --------:
						zzB_Select_Mst_Tour_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Tour_Filter_Draft;
						--drop table #tbl_Mst_Tour_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_Tour_zzE = "-- Nothing.";
                if (bGet_Mst_Tour)
                {
                    #region // bGet_Mst_Tour:
                    zzB_Select_Mst_Tour_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Tour:
							select
								t.MyIdxSeq
								, mt.*
							from #tbl_Mst_Tour_Filter t --//[mylock]
								inner join Mst_Tour mt --//[mylock]
									on t.TourCode = mt.TourCode
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
                            , "Mst_Tour" // strTableNameDB
                            , "Mst_Tour." // strPrefixStd
                            , "mt." // strPrefixAlias
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
                    , "zzB_Select_Mst_Tour_zzE", zzB_Select_Mst_Tour_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_Tour)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_Tour";
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

        public DataSet Mst_Tour_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTourCode
            , object objTourName
            , object objTourType
            , object objTourDesc
            , object objTourThemePath
            , object objTourDuration
            , object objTourDayDuration
            , object objTourNightDuration
            , object objTourTouristNumber
            , object objTourTransport
            , object objTourListDest
            , object objTourFood
            , object objTourHotel
            , object objTourIdealTime
            , object objTourIdealPeople
            , object objTourPreferential
            , object objTourStartPoint
            , object objTourPrice
            , object objTourImage1Path
            , object objTourImage2Path
            , object objTourImage3Path
            , object objTourImage4Path
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Tour_Create";
            string strErrorCodeDefault = TError.ErrDA.Mst_Tour_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objTourCode", objTourCode
                    , "objTourName", objTourName
                    , "objTourTypeCode", objTourType
                    , "objTourDesc", objTourDesc
                    , "objTourThemePath", objTourThemePath
                    , "objTourDuration", objTourDuration
                    , "objTourDayDuration", objTourDayDuration
                    , "objTourNightDuration", objTourNightDuration
                    , "objTourTouristNumber", objTourTouristNumber
                    , "objTourTransport", objTourTransport
                    , "objTourListDest", objTourListDest
                    , "objTourFood", objTourFood
                    , "objTourHotel", objTourHotel
                    , "objTourIdealTime", objTourIdealTime
                    , "objTourIdealPeople", objTourIdealPeople
                    , "objTourPreferential", objTourPreferential
                    , "objTourStartPoint", objTourStartPoint
                    , "objTourTotalPrice", objTourPrice
                    , "objTourImage1Path", objTourImage1Path
                    , "objTourImage2Path", objTourImage2Path
                    , "objTourImage3Path", objTourImage3Path
                    , "objTourImage4Path", objTourImage4Path
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_Tour_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode
                        , objTourName
                        , objTourType
                        , objTourDesc
                        , objTourThemePath
                        , objTourDuration
                        , objTourDayDuration
                        , objTourNightDuration
                        , objTourTouristNumber
                        , objTourTransport
                        , objTourListDest
                        , objTourFood
                        , objTourHotel
                        , objTourIdealTime
                        , objTourIdealPeople
                        , objTourPreferential
                        , objTourStartPoint
                        , objTourPrice
                        , objTourImage1Path
                        , objTourImage2Path
                        , objTourImage3Path
                        , objTourImage4Path
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

        private void Mst_Tour_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            , object objTourName
            , object objTourType
            , object objTourDesc
            , object objTourThemePath
            , object objTourDuration
            , object objTourDayDuration
            , object objTourNightDuration
            , object objTourTouristNumber
            , object objTourTransport
            , object objTourListDest
            , object objTourFood
            , object objTourHotel
            , object objTourIdealTime
            , object objTourIdealPeople
            , object objTourPreferential
            , object objTourStartPoint
            , object objTourPrice
            , object objTourImage1Path
            , object objTourImage2Path
            , object objTourImage3Path
            , object objTourImage4Path
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Tour_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objTourCode", objTourCode
                , "objTourName", objTourName
                , "objTourTypeCode", objTourType
                , "objTourDesc", objTourDesc
                , "objTourThemePath", objTourThemePath
                , "objTourDuration", objTourDuration
                , "objTourDayDuration", objTourDayDuration
                , "objTourNightDuration", objTourNightDuration
                , "objTourTouristNumber", objTourTouristNumber
                , "objTourTransport", objTourTransport
                , "objTourListDest", objTourListDest
                , "objTourFood", objTourFood
                , "objTourHotel", objTourHotel
                , "objTourIdealTime", objTourIdealTime
                , "objTourIdealPeople", objTourIdealPeople
                , "objTourPreferential", objTourPreferential
                , "objTourStartPoint", objTourStartPoint
                , "objTourTotalPrice", objTourPrice
                , "objTourImage1Path", objTourImage1Path
                , "objTourImage2Path", objTourImage2Path
                , "objTourImage3Path", objTourImage3Path
                , "objTourImage4Path", objTourImage4Path
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);
            string strTourName = string.Format("{0}", objTourName).Trim();
            string strTourType = TUtils.CUtils.StdParam(objTourType);
            string strTourDesc = string.Format("{0}", objTourDesc).Trim();
            string strTourThemePath = string.Format("{0}", objTourThemePath).Trim();
            string strTourDuration = string.Format("{0}", objTourDuration).Trim();
            int iTourDayDuration = TUtils.CUtils.ConvertToInt32(objTourDayDuration);
            int iTourNightDuration = TUtils.CUtils.ConvertToInt32(objTourNightDuration);
            int iTourTouristNumber = TUtils.CUtils.ConvertToInt32(objTourTouristNumber);
            string strTourTransport = string.Format("{0}", objTourTransport).Trim();
            string strTourListDest = string.Format("{0}", objTourListDest).Trim();
            string strTourFood = string.Format("{0}", objTourFood).Trim();
            string strTourHotel = string.Format("{0}", objTourHotel).Trim();
            string strTourIdealTime = string.Format("{0}", objTourIdealTime).Trim();
            string strTourIdealPeople = string.Format("{0}", objTourIdealPeople).Trim();
            string strTourPreferential = string.Format("{0}", objTourPreferential).Trim();
            string strTourStartPoint = string.Format("{0}", objTourStartPoint).Trim();
            double dblTourPrice = TUtils.CUtils.ConvertToDouble(objTourPrice);
            string strTourImage1Path = string.Format("{0}", objTourImage1Path).Trim();
            string strTourImage2Path = string.Format("{0}", objTourImage2Path).Trim();
            string strTourImage3Path = string.Format("{0}", objTourImage3Path).Trim();
            string strTourImage4Path = string.Format("{0}", objTourImage4Path).Trim();

            ////
            DataTable dtDB_Mst_Tour = null;
            {
                ////
                if (strTourCode == null || strTourCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTourCode", strTourCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CreateX_InvalidTourCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_Tour_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTourCode // objSupCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Tour // dtDB_Mst_Part
                    );
                ////
                DataTable dtDB_Mst_TourType = null;

                Mst_TourType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTourType // objPartType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_TourType // dtDB_Mst_PartType
                    );
                ////
                if (strTourName == null || strTourName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTourName", strTourName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CreateX_InvalidTourName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (iTourTouristNumber <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.iTourTouristNumber", iTourTouristNumber
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CreateX_InvalidTouristNumber
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                /////
                if (dblTourPrice < 0.0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTourPrice", dblTourPrice
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_CreateX_InvalidTourTotalPrice
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_Tour.NewRow();
                strFN = "TourCode"; drDB[strFN] = strTourCode;
                strFN = "TourName"; drDB[strFN] = strTourName;
                strFN = "TourType"; drDB[strFN] = strTourType;
                strFN = "TourDesc"; drDB[strFN] = strTourDesc;
                strFN = "TourThemePath"; drDB[strFN] = strTourThemePath;
                strFN = "TourDuration"; drDB[strFN] = strTourDuration;
                strFN = "TourDayDuration"; drDB[strFN] = iTourDayDuration;
                strFN = "TourNightDuration"; drDB[strFN] = iTourNightDuration;
                strFN = "TourTouristNumber"; drDB[strFN] = iTourTouristNumber;
                strFN = "TourTransport"; drDB[strFN] = strTourTransport;
                strFN = "TourListDest"; drDB[strFN] = strTourListDest;
                strFN = "TourFood"; drDB[strFN] = strTourFood;
                strFN = "TourHotel"; drDB[strFN] = strTourHotel;
                strFN = "TourIdealTime"; drDB[strFN] = strTourIdealTime;
                strFN = "TourIdealPeople"; drDB[strFN] = strTourIdealPeople;
                strFN = "TourPreferential"; drDB[strFN] = strTourPreferential;
                strFN = "TourStartPoint"; drDB[strFN] = strTourStartPoint;
                strFN = "TourPrice"; drDB[strFN] = dblTourPrice;
                strFN = "TourImage1Path"; drDB[strFN] = strTourImage1Path;
                strFN = "TourImage2Path"; drDB[strFN] = strTourImage2Path;
                strFN = "TourImage3Path"; drDB[strFN] = strTourImage3Path;
                strFN = "TourImage4Path"; drDB[strFN] = strTourImage4Path;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "CreateDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "CreateBy"; drDB[strFN] = strWAUserCode;
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_Tour.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_Tour" // strTableName
                    , dtDB_Mst_Tour // dtData
                                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet Mst_Tour_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTourCode
            , object objTourName
            , object objTourType
            , object objTourDesc
            , object objTourThemePath
            , object objTourDuration
            , object objTourDayDuration
            , object objTourNightDuration
            , object objTourTouristNumber
            , object objTourTransport
            , object objTourListDest
            , object objTourFood
            , object objTourHotel
            , object objTourIdealTime
            , object objTourIdealPeople
            , object objTourPreferential
            , object objTourStartPoint
            , object objTourPrice
            , object objTourImage1Path
            , object objTourImage2Path
            , object objTourImage3Path
            , object objTourImage4Path
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Tour_Update";
            string strErrorCodeDefault = TError.ErrDA.Mst_Tour_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTourCode", objTourCode
                , "objTourName", objTourName
                , "objTourTypeCode", objTourType
                , "objTourDesc", objTourDesc
                , "objTourThemePath", objTourThemePath
                , "objTourDuration", objTourDuration
                , "objTourDayDuration", objTourDayDuration
                , "objTourNightDuration", objTourNightDuration
                , "objTourTouristNumber", objTourTouristNumber
                , "objTourTransport", objTourTransport
                , "objTourListDest", objTourListDest
                , "objTourFood", objTourFood
                , "objTourHotel", objTourHotel
                , "objTourIdealTime", objTourIdealTime
                , "objTourIdealPeople", objTourIdealPeople
                , "objTourPreferential", objTourPreferential
                , "objTourStartPoint", objTourStartPoint
                , "objTourTotalPrice", objTourPrice
                , "objTourImage1Path", objTourImage1Path
                , "objTourImage2Path", objTourImage2Path
                , "objTourImage3Path", objTourImage3Path
                , "objTourImage4Path", objTourImage4Path
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_Tour_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode
                        , objTourName
                        , objTourType
                        , objTourDesc
                        , objTourThemePath
                        , objTourDuration
                        , objTourDayDuration
                        , objTourNightDuration
                        , objTourTouristNumber
                        , objTourTransport
                        , objTourListDest
                        , objTourFood
                        , objTourHotel
                        , objTourIdealTime
                        , objTourIdealPeople
                        , objTourPreferential
                        , objTourStartPoint
                        , objTourPrice
                        , objTourImage1Path
                        , objTourImage2Path
                        , objTourImage3Path
                        , objTourImage4Path
                        , objFlagActive
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

        private void Mst_Tour_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            , object objTourName
            , object objTourType
            , object objTourDesc
            , object objTourThemePath
            , object objTourDuration
            , object objTourDayDuration
            , object objTourNightDuration
            , object objTourTouristNumber
            , object objTourTransport
            , object objTourListDest
            , object objTourFood
            , object objTourHotel
            , object objTourIdealTime
            , object objTourIdealPeople
            , object objTourPreferential
            , object objTourStartPoint
            , object objTourPrice
            , object objTourImage1Path
            , object objTourImage2Path
            , object objTourImage3Path
            , object objTourImage4Path
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Tour_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTourCode", objTourCode
                , "objTourName", objTourName
                , "objTourTypeCode", objTourType
                , "objTourDesc", objTourDesc
                , "objTourThemePath", objTourThemePath
                , "objTourDuration", objTourDuration
                , "objTourDayDuration", objTourDayDuration
                , "objTourNightDuration", objTourNightDuration
                , "objTourTouristNumber", objTourTouristNumber
                , "objTourTransport", objTourTransport
                , "objTourListDest", objTourListDest
                , "objTourFood", objTourFood
                , "objTourHotel", objTourHotel
                , "objTourIdealTime", objTourIdealTime
                , "objTourIdealPeople", objTourIdealPeople
                , "objTourPreferential", objTourPreferential
                , "objTourStartPoint", objTourStartPoint
                , "objTourTotalPrice", objTourPrice
                , "objTourImage1Path", objTourImage1Path
                , "objTourImage2Path", objTourImage2Path
                , "objTourImage3Path", objTourImage3Path
                , "objTourImage4Path", objTourImage4Path
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
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);
            string strTourName = string.Format("{0}", objTourName).Trim();
            string strTourType = TUtils.CUtils.StdParam(objTourType);
            string strTourDesc = string.Format("{0}", objTourDesc).Trim();
            string strTourThemePath = string.Format("{0}", objTourThemePath).Trim();
            string strTourDuration = string.Format("{0}", objTourDuration).Trim();
            int iTourDayDuration = TUtils.CUtils.ConvertToInt32(objTourDayDuration);
            int iTourNightDuration = TUtils.CUtils.ConvertToInt32(objTourNightDuration);
            int iTourTouristNumber = TUtils.CUtils.ConvertToInt32(objTourTouristNumber);
            string strTourTransport = string.Format("{0}", objTourTransport).Trim();
            string strTourListDest = string.Format("{0}", objTourListDest).Trim();
            string strTourFood = string.Format("{0}", objTourFood).Trim();
            string strTourHotel = string.Format("{0}", objTourHotel).Trim();
            string strTourIdealTime = string.Format("{0}", objTourIdealTime).Trim();
            string strTourIdealPeople = string.Format("{0}", objTourIdealPeople).Trim();
            string strTourPreferential = string.Format("{0}", objTourPreferential).Trim();
            string strTourStartPoint = string.Format("{0}", objTourStartPoint).Trim();
            double dblTourPrice = TUtils.CUtils.ConvertToDouble(objTourPrice);
            string strTourImage1Path = string.Format("{0}", objTourImage1Path).Trim();
            string strTourImage2Path = string.Format("{0}", objTourImage2Path).Trim();
            string strTourImage3Path = string.Format("{0}", objTourImage3Path).Trim();
            string strTourImage4Path = string.Format("{0}", objTourImage4Path).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_TourCode = strFt_Cols_Upd.Contains("Mst_Tour.TourCode".ToUpper());
            bool bUpd_TourName = strFt_Cols_Upd.Contains("Mst_Tour.TourName".ToUpper());
            bool bUpd_TourTypeCode = strFt_Cols_Upd.Contains("Mst_Tour.TourType".ToUpper());
            bool bUpd_TourDesc = strFt_Cols_Upd.Contains("Mst_Tour.TourDesc".ToUpper());
            bool bUpd_TourThemePath = strFt_Cols_Upd.Contains("Mst_Tour.TourThemePath".ToUpper());
            bool bUpd_TourDuration = strFt_Cols_Upd.Contains("Mst_Tour.TourDuration".ToUpper());
            bool bUpd_TourDayDuration = strFt_Cols_Upd.Contains("Mst_Tour.TourDayDuration".ToUpper());
            bool bUpd_TourNightDuration = strFt_Cols_Upd.Contains("Mst_Tour.TourNightDuration".ToUpper());
            bool bUpd_TourTouristNumber = strFt_Cols_Upd.Contains("Mst_Tour.TourTouristNumber".ToUpper());
            bool bUpd_TourTransport = strFt_Cols_Upd.Contains("Mst_Tour.TourTransport".ToUpper());
            bool bUpd_TourListDest = strFt_Cols_Upd.Contains("Mst_Tour.TourListDest".ToUpper());
            bool bUpd_TourFood = strFt_Cols_Upd.Contains("Mst_Tour.TourFood".ToUpper());
            bool bUpd_TourHotel = strFt_Cols_Upd.Contains("Mst_Tour.TourHotel".ToUpper());
            bool bUpd_TourIdealTime = strFt_Cols_Upd.Contains("Mst_Tour.TourIdealTime".ToUpper());
            bool bUpd_TourIdealPeople = strFt_Cols_Upd.Contains("Mst_Tour.TourIdealPeople".ToUpper());
            bool bUpd_TourPreferential = strFt_Cols_Upd.Contains("Mst_Tour.TourPreferential".ToUpper());
            bool bUpd_TourStartPoint = strFt_Cols_Upd.Contains("Mst_Tour.TourStartPoint".ToUpper());
            bool bUpd_TourPrice = strFt_Cols_Upd.Contains("Mst_Tour.TourPrice".ToUpper());
            bool bUpd_TourImage1Path = strFt_Cols_Upd.Contains("Mst_Tour.TourImage1Path".ToUpper());
            bool bUpd_TourImage2Path = strFt_Cols_Upd.Contains("Mst_Tour.TourImage2Path".ToUpper());
            bool bUpd_TourImage3Path = strFt_Cols_Upd.Contains("Mst_Tour.TourImage3Path".ToUpper());
            bool bUpd_TourImage4Path = strFt_Cols_Upd.Contains("Mst_Tour.TourImage4Path".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Tour.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_Tour = null;
            {
                ////
                Mst_Tour_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strTourCode // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Tour // dtDB_Mst_Tour
                    );
                ////
                if (strTourName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTourName", strTourName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Tour_Update_InvalidTourName
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
                DataRow drDB = dtDB_Mst_Tour.Rows[0];
                if (bUpd_TourCode) { strFN = "TourCode"; drDB[strFN] = strTourCode; alColumnEffective.Add(strFN); }
                if (bUpd_TourName) { strFN = "TourName"; drDB[strFN] = strTourName; alColumnEffective.Add(strFN); }
                if (bUpd_TourTypeCode) { strFN = "TourType"; drDB[strFN] = strTourType; alColumnEffective.Add(strFN); }
                if (bUpd_TourDesc) { strFN = "TourDesc"; drDB[strFN] = strTourDesc; alColumnEffective.Add(strFN); }
                if (bUpd_TourThemePath) { strFN = "TourThemePath"; drDB[strFN] = strTourThemePath; alColumnEffective.Add(strFN); }
                if (bUpd_TourDuration) { strFN = "TourDuration"; drDB[strFN] = strTourDuration; alColumnEffective.Add(strFN); }
                if (bUpd_TourDayDuration) { strFN = "TourDayDuration"; drDB[strFN] = iTourDayDuration; alColumnEffective.Add(strFN); }
                if (bUpd_TourNightDuration) { strFN = "TourNightDuration"; drDB[strFN] = iTourNightDuration; alColumnEffective.Add(strFN); }
                if (bUpd_TourTouristNumber) { strFN = "TourTouristNumber"; drDB[strFN] = iTourTouristNumber; alColumnEffective.Add(strFN); }
                if (bUpd_TourTransport) { strFN = "TourTransport"; drDB[strFN] = strTourTransport; alColumnEffective.Add(strFN); }
                if (bUpd_TourListDest) { strFN = "TourListDest"; drDB[strFN] = strTourListDest; alColumnEffective.Add(strFN); }
                if (bUpd_TourFood) { strFN = "TourFood"; drDB[strFN] = strTourFood; alColumnEffective.Add(strFN); }
                if (bUpd_TourHotel) { strFN = "TourHotel"; drDB[strFN] = strTourHotel; alColumnEffective.Add(strFN); }
                if (bUpd_TourIdealTime) { strFN = "TourIdealTime"; drDB[strFN] = strTourIdealTime; alColumnEffective.Add(strFN); }
                if (bUpd_TourIdealPeople) { strFN = "TourIdealPeople"; drDB[strFN] = strTourIdealPeople; alColumnEffective.Add(strFN); }
                if (bUpd_TourPreferential) { strFN = "TourPreferential"; drDB[strFN] = strTourPreferential; alColumnEffective.Add(strFN); }
                if (bUpd_TourStartPoint) { strFN = "TourStartPoint"; drDB[strFN] = strTourStartPoint; alColumnEffective.Add(strFN); }
                if (bUpd_TourPrice) { strFN = "TourPrice"; drDB[strFN] = dblTourPrice; alColumnEffective.Add(strFN); }
                if (bUpd_TourImage1Path) { strFN = "TourImage1Path"; drDB[strFN] = strTourImage1Path; alColumnEffective.Add(strFN); }
                if (bUpd_TourImage2Path) { strFN = "TourImage2Path"; drDB[strFN] = strTourImage2Path; alColumnEffective.Add(strFN); }
                if (bUpd_TourImage3Path) { strFN = "TourImage3Path"; drDB[strFN] = strTourImage3Path; alColumnEffective.Add(strFN); }
                if (bUpd_TourImage4Path) { strFN = "TourImage4Path"; drDB[strFN] = strTourImage4Path; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);
                // Save:
                _cf.db.SaveData(
                    "Mst_Tour"
                    , dtDB_Mst_Tour
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet Mst_Tour_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objTourCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Tour_Delete";
            string strErrorCodeDefault = TError.ErrDA.Mst_Tour_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objTourCode", objTourCode
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_Tour_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode // objTourCode
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

        private void Mst_Tour_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Tour_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objTourCode", objTourCode
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);

            ////
            DataTable dtDB_Mst_Tour = null;
            {
                ////
                Mst_Tour_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strTourCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Tour
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_Tour.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_Tour"
                    , dtDB_Mst_Tour
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_Tour: WAS
        public DataSet WAS_Mst_Tour_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Tour objRQ_Mst_Tour
            ////
            , out DA_RT_Mst_Tour objRT_Mst_Tour
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Tour.Tid;
            objRT_Mst_Tour = new DA_RT_Mst_Tour();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Tour_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Tour_Get;
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
                List<DA_Mst_Tour> lst_Mst_Tour = new List<DA_Mst_Tour>();
                bool bGet_Mst_Tour = (objRQ_Mst_Tour.Rt_Cols_Mst_Tour != null && objRQ_Mst_Tour.Rt_Cols_Mst_Tour.Length > 0);
                #endregion

                #region // WS_Mst_Tour_Get:
                mdsResult = Mst_Tour_Get(
                    objRQ_Mst_Tour.Tid // strTid
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    , objRQ_Mst_Tour.WAUserCode // strUserCode
                    , objRQ_Mst_Tour.WAUserPassword // strUserPassword
                                                      //, TUtils.CUtils.StdFlag(objRQ_Mst_Region.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Tour.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Tour.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Tour.Ft_WhereClause // strFt_WhereClause
                                                      //// Return:
                    , objRQ_Mst_Tour.Rt_Cols_Mst_Tour // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Tour.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Tour)
                    {
                        DataTable dt_Mst_Tour = mdsResult.Tables["Mst_Tour"].Copy();
                        lst_Mst_Tour = TUtils.DataTableCmUtils.ToListof<DA_Mst_Tour>(dt_Mst_Tour);
                        objRT_Mst_Tour.Lst_Mst_Tour = lst_Mst_Tour;
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

        public DataSet WAS_Mst_Tour_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Tour objRQ_Mst_Tour
            ////
            , out DA_RT_Mst_Tour objRT_Mst_Tour
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Tour.Tid;
            objRT_Mst_Tour = new DA_RT_Mst_Tour();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Tour_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Tour_Create;
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
                List<DA_Mst_Tour> lst_Mst_Tour = new List<DA_Mst_Tour>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = Mst_Tour_Create(
                    objRQ_Mst_Tour.Tid // strTid
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    , objRQ_Mst_Tour.WAUserCode // strUserCode
                    , objRQ_Mst_Tour.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Tour.Mst_Tour.TourCode
                    , objRQ_Mst_Tour.Mst_Tour.TourName
                    , objRQ_Mst_Tour.Mst_Tour.TourType
                    , objRQ_Mst_Tour.Mst_Tour.TourDesc
                    , objRQ_Mst_Tour.Mst_Tour.TourThemePath
                    , objRQ_Mst_Tour.Mst_Tour.TourDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourDayDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourNightDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourTouristNumber
                    , objRQ_Mst_Tour.Mst_Tour.TourTransport
                    , objRQ_Mst_Tour.Mst_Tour.TourListDest
                    , objRQ_Mst_Tour.Mst_Tour.TourFood
                    , objRQ_Mst_Tour.Mst_Tour.TourHotel
                    , objRQ_Mst_Tour.Mst_Tour.TourIdealTime
                    , objRQ_Mst_Tour.Mst_Tour.TourIdealPeople
                    , objRQ_Mst_Tour.Mst_Tour.TourPreferential
                    , objRQ_Mst_Tour.Mst_Tour.TourStartPoint
                    , objRQ_Mst_Tour.Mst_Tour.TourPrice
                    , objRQ_Mst_Tour.Mst_Tour.TourImage1Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage2Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage3Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage4Path
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

        public DataSet WAS_Mst_Tour_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Tour objRQ_Mst_Tour
            ////
            , out DA_RT_Mst_Tour objRT_Mst_Tour
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Tour.Tid;
            objRT_Mst_Tour = new DA_RT_Mst_Tour();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Tour_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Tour_Update;
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
                List<DA_Mst_Tour> lst_Mst_Tour = new List<DA_Mst_Tour>();
                #endregion

                #region // Mst_Part_Update:
                mdsResult = Mst_Tour_Update(
                    objRQ_Mst_Tour.Tid // strTid
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    , objRQ_Mst_Tour.WAUserCode // strUserCode
                    , objRQ_Mst_Tour.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Tour.Mst_Tour.TourCode
                    , objRQ_Mst_Tour.Mst_Tour.TourName
                    , objRQ_Mst_Tour.Mst_Tour.TourType
                    , objRQ_Mst_Tour.Mst_Tour.TourDesc
                    , objRQ_Mst_Tour.Mst_Tour.TourThemePath
                    , objRQ_Mst_Tour.Mst_Tour.TourDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourDayDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourNightDuration
                    , objRQ_Mst_Tour.Mst_Tour.TourTouristNumber
                    , objRQ_Mst_Tour.Mst_Tour.TourTransport
                    , objRQ_Mst_Tour.Mst_Tour.TourListDest
                    , objRQ_Mst_Tour.Mst_Tour.TourFood
                    , objRQ_Mst_Tour.Mst_Tour.TourHotel
                    , objRQ_Mst_Tour.Mst_Tour.TourIdealTime
                    , objRQ_Mst_Tour.Mst_Tour.TourIdealPeople
                    , objRQ_Mst_Tour.Mst_Tour.TourPreferential
                    , objRQ_Mst_Tour.Mst_Tour.TourStartPoint
                    , objRQ_Mst_Tour.Mst_Tour.TourPrice
                    , objRQ_Mst_Tour.Mst_Tour.TourImage1Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage2Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage3Path
                    , objRQ_Mst_Tour.Mst_Tour.TourImage4Path
                    , objRQ_Mst_Tour.Mst_Tour.FlagActive // objFlagActive
                                                         ////
                    , objRQ_Mst_Tour.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_Mst_Tour_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Tour objRQ_Mst_Tour
            ////
            , out DA_RT_Mst_Tour objRT_Mst_Tour
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Tour.Tid;
            objRT_Mst_Tour = new DA_RT_Mst_Tour();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Tour_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Tour_Delete;
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
                List<DA_Mst_Tour> lst_Mst_Part = new List<DA_Mst_Tour>();
                #endregion

                #region // Mst_Part_Delete:
                mdsResult = Mst_Tour_Delete(
                    objRQ_Mst_Tour.Tid // strTid
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    , objRQ_Mst_Tour.WAUserCode // strUserCode
                    , objRQ_Mst_Tour.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Tour.Mst_Tour.TourCode
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

        #region // Mst_CustomerType
        private void DAMst_CustomerType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objCustomerTypeCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_CustomerType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_CustomerType t --//[mylock]
					where (1=1)
						and t.CustomerTypeCode = @objCustomerTypeCode
					;
				");
            dtDB_Mst_CustomerType = _cf.db.ExecQuery(
                strSqlExec
                , "@objCustomerTypeCode", objCustomerTypeCode
                ).Tables[0];
            dtDB_Mst_CustomerType.TableName = "Mst_CustomerType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_CustomerType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerTypeCode", objCustomerTypeCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_CustomerType_CheckDB_CustomerTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_CustomerType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerTypeCode", objCustomerTypeCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_CustomerType_CheckDB_CustomerTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_CustomerType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CustomerTypeCode", objCustomerTypeCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_CustomerType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_CustomerType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_CustomerType_Get(
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
            , string strRt_Cols_Mst_CustomerType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_CustomerType_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_CustomerType_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    //// Filter
                    , "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
                    //// Return
                    , "strRt_Cols_Mst_CustomerType", strRt_Cols_Mst_CustomerType
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
                bool bGet_Mst_CustomerType = (strRt_Cols_Mst_CustomerType != null && strRt_Cols_Mst_CustomerType.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                        "@nFilterRecordStart", nFilterRecordStart
                        , "@nFilterRecordEnd", nFilterRecordEnd
                        });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
        		        ---- #tbl_Mst_CustomerType_Filter_Draft:
        		        select distinct
        			        identity(bigint, 0, 1) MyIdxSeq
        			        , mct.CustomerTypeCode
        		        into #tbl_Mst_CustomerType_Filter_Draft
        		        from Mst_CustomerType mct --//[mylock]
        		        where (1=1)
        			        zzB_Where_strFilter_zzE
        		        order by mct.CustomerTypeCode asc
        		        ;

        		        ---- Summary:
        		        select Count(0) MyCount from #tbl_Mst_CustomerType_Filter_Draft t --//[mylock]
        		        ;

        		        ---- #tbl_Mst_CustomerType_Filter:
        		        select
        			        t.*
        		        into #tbl_Mst_CustomerType_Filter
        		        from #tbl_Mst_CustomerType_Filter_Draft t --//[mylock]
        		        where
        			        (t.MyIdxSeq >= @nFilterRecordStart)
        			        and (t.MyIdxSeq <= @nFilterRecordEnd)
        		        ;

        		        -------- Mst_CustomerType --------:
        		        zzB_Select_Mst_CustomerType_zzE
        		        ----------------------------------------

        		        ---- Clear for debug:
        		        --drop table #tbl_Mst_CustomerType_Filter_Draft;
        		        --drop table #tbl_Mst_CustomerType_Filter;
        	        "
                    );
                ////
                string zzB_Select_Mst_CustomerType_zzE = "-- Nothing.";
                if (bGet_Mst_CustomerType)
                {
                    #region // bGet_Mst_CustomerType:
                    zzB_Select_Mst_CustomerType_zzE = CmUtils.StringUtils.Replace(@"
        			        ---- Mst_Tour:
        			        select
        				        t.MyIdxSeq
        				        , mct.*
        			        from #tbl_Mst_CustomerType_Filter t --//[mylock]
        				        inner join Mst_CustomerType mct --//[mylock]
        					        on t.CustomerTypeCode = mct.CustomerTypeCode
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
                            , "Mst_CustomerType" // strTableNameDB
                            , "Mst_CustomerType." // strPrefixStd
                            , "mct." // strPrefixAlias
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
                    , "zzB_Select_Mst_CustomerType_zzE", zzB_Select_Mst_CustomerType_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_CustomerType)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerType";
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
        #endregion

        #region // Mst_CustomerType: WAS
        public DataSet WAS_DAMst_CustomerType_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out DA_RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new DA_RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_CustomerType_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_CustomerType_Get;
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
                List<DA_Mst_CustomerType> lst_Mst_CustomerType = new List<DA_Mst_CustomerType>();
                bool bGet_Mst_CustomerType = (objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType != null && objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType.Length > 0);
                #endregion

                #region // WS_Mst_CustomerType_Get:
                mdsResult = DAMst_CustomerType_Get(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
                                                            //, TUtils.CUtils.StdFlag(objRQ_Mst_Region.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_CustomerType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_CustomerType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_CustomerType.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_CustomerType.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_CustomerType)
                    {
                        DataTable dt_Mst_CustomerType = mdsResult.Tables["Mst_CustomerType"].Copy();
                        lst_Mst_CustomerType = TUtils.DataTableCmUtils.ToListof<DA_Mst_CustomerType>(dt_Mst_CustomerType);
                        objRT_Mst_CustomerType.Lst_Mst_CustomerType = lst_Mst_CustomerType;
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
        #endregion

        #region // Mst_Customer
        private void DAMst_Customer_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objCustomerCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Customer
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Customer t --//[mylock]
					where (1=1)
						and t.CustomerCode = @objCustomerCode
					;
				");
            dtDB_Mst_Customer = _cf.db.ExecQuery(
                strSqlExec
                , "@objCustomerCode", objCustomerCode
                ).Tables[0];
            dtDB_Mst_Customer.TableName = "Mst_Customer";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Customer.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerCode", objCustomerCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CheckDB_CustomerNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Customer.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerCode", objCustomerCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CheckDB_CustomerExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Customer.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CustomerCode", objCustomerCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Customer.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_Customer_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_Customer_Get(
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
            , string strRt_Cols_Mst_Customer
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Customer_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_Customer_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Customer", strRt_Cols_Mst_Customer
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_Mst_Customer = (strRt_Cols_Mst_Customer != null && strRt_Cols_Mst_Customer.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Customer_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mc.CustomerCode
						into #tbl_Mst_Customer_Filter_Draft
						from Mst_Customer mc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mc.CustomerCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Customer_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Customer_Filter:
						select
							t.*
						into #tbl_Mst_Customer_Filter
						from #tbl_Mst_Customer_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Customer --------:
						zzB_Select_Mst_Customer_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Customer_Filter_Draft;
						--drop table #tbl_Mst_Customer_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_Customer_zzE = "-- Nothing.";
                if (bGet_Mst_Customer)
                {
                    #region // bGet_Mst_Customer:
                    zzB_Select_Mst_Customer_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Customer:
							select
								t.MyIdxSeq
								, mc.*
							from #tbl_Mst_Customer_Filter t --//[mylock]
								inner join Mst_Customer mc --//[mylock]
									on t.CustomerCode = mc.CustomerCode
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
                            , "Mst_Customer" // strTableNameDB
                            , "Mst_Customer." // strPrefixStd
                            , "mc." // strPrefixAlias
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
                    , "zzB_Select_Mst_Customer_zzE", zzB_Select_Mst_Customer_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_Customer)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_Customer";
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

        public DataSet DAMst_Customer_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objCustomerCode
            , object objCustomerTypeCode
            , object objCustomerName
            , object objCustomerGender
            , object objCustomerPhoneNo
            , object objCustomerMobileNo
            , object objCustomerAddress
            , object objCustomerEmail
            , object objCustomerBOD
            , object objCustomerAvatarPath
            , object objCustomerUserCode
            , object objCustomerIDCardNo
            //, object objFlagActive
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Customer_Create";
            string strErrorCodeDefault = TError.ErrDA.Mst_Customer_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objCustomerCode", objCustomerCode
                    , "objCustomerTypeCode", objCustomerTypeCode
                    , "objCustomerName", objCustomerName
                    , "objCustomerGender", objCustomerGender
                    , "objCustomerPhoneNo", objCustomerPhoneNo
                    , "objCustomerMobileNo", objCustomerMobileNo
                    , "objCustomerAddress", objCustomerAddress
                    , "objCustomerEmail", objCustomerEmail
                    , "objCustomerBOD", objCustomerBOD
                    , "objCustomerAvatarPath", objCustomerAvatarPath
                    , "objCustomerUserCode", objCustomerUserCode
                    , "objCustomerIDCardNo", objCustomerIDCardNo
                    //, "objFlagActive", objFlagActive
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Customer_CreateX:
                //DataSet dsGetData = null;
                {
                    DAMst_Customer_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerCode
                        , objCustomerTypeCode
                        , objCustomerName
                        , objCustomerGender
                        , objCustomerPhoneNo
                        , objCustomerMobileNo
                        , objCustomerAddress
                        , objCustomerEmail
                        , objCustomerBOD
                        , objCustomerAvatarPath
                        , objCustomerUserCode
                        , objCustomerIDCardNo
                        //, objFlagActive
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

        private void DAMst_Customer_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCustomerCode
            , object objCustomerTypeCode
            , object objCustomerName
            , object objCustomerGender
            , object objCustomerPhoneNo
            , object objCustomerMobileNo
            , object objCustomerAddress
            , object objCustomerEmail
            , object objCustomerBOD
            , object objCustomerAvatarPath
            , object objCustomerUserCode
            , object objCustomerIDCardNo
            //, object objFlagActive
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Customer_CreateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objCustomerCode", objCustomerCode
                , "objCustomerTypeCode", objCustomerTypeCode
                , "objCustomerName", objCustomerName
                , "objCustomerGender", objCustomerGender
                , "objCustomerPhoneNo", objCustomerPhoneNo
                , "objCustomerMobileNo", objCustomerMobileNo
                , "objCustomerAddress", objCustomerAddress
                , "objCustomerEmail", objCustomerEmail
                , "objCustomerBOD", objCustomerBOD
                , "objCustomerAvatarPath", objCustomerAvatarPath
                , "objCustomerUserCode", objCustomerUserCode
                , "objCustomerIDCardNo", objCustomerIDCardNo
                //, "objFlagActive", objFlagActive
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
            string strCustomerName = string.Format("{0}", objCustomerName).Trim();
            string strCustomerTypeCode = TUtils.CUtils.StdParam(objCustomerTypeCode);
            string strCustomerGender = string.Format("{0}", objCustomerGender).Trim();
            string strCustomerPhoneNo = string.Format("{0}", objCustomerPhoneNo).Trim();
            string strCustomerMobileNo = string.Format("{0}", objCustomerMobileNo).Trim();
            string strCustomerAddress = string.Format("{0}", objCustomerAddress).Trim();
            string strCustomerEmail = string.Format("{0}", objCustomerEmail).Trim();
            string strCustomerBOD = TUtils.CUtils.StdParam(objCustomerBOD);
            string strCustomerAvatarPath = string.Format("{0}", objCustomerAvatarPath).Trim();
            string strCustomerUserCode = TUtils.CUtils.StdParam(objCustomerUserCode);
            string strCustomerIDCardNo = TUtils.CUtils.StdParam(objCustomerIDCardNo);

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_Customer = null;
            {
                ////
                if (strCustomerCode == null || strCustomerCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerCode", strCustomerCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CreateX_InvalidCustomerCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAMst_Customer_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCustomerCode // strCustomerCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Customer // dtDB_Mst_Customer
                    );
                ////
                DataTable dtDB_Mst_CustomerType = null;

                DAMst_CustomerType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCustomerTypeCode // objCustomerType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    );
                ////
                if (strCustomerName == null || strCustomerName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerName", strCustomerName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CreateX_InvalidCustomerName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strCustomerMobileNo == null || strCustomerMobileNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerMobileNo", strCustomerMobileNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CreateX_InvalidCustomerMobileNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strCustomerEmail == null || strCustomerEmail.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerEmail", strCustomerEmail
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_CreateX_InvalidCustomerEmail
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_Customer.NewRow();
                strFN = "CustomerCode"; drDB[strFN] = strCustomerCode;
                strFN = "CustomerName"; drDB[strFN] = strCustomerName;
                strFN = "CustomerTypeCode"; drDB[strFN] = strCustomerTypeCode;
                strFN = "CustomerGender"; drDB[strFN] = strCustomerGender;
                strFN = "CustomerPhoneNo"; drDB[strFN] = strCustomerPhoneNo;
                strFN = "CustomerMobileNo"; drDB[strFN] = strCustomerMobileNo;
                strFN = "CustomerAddress"; drDB[strFN] = strCustomerAddress;
                strFN = "CustomerEmail"; drDB[strFN] = strCustomerEmail;
                strFN = "CustomerBOD"; drDB[strFN] = strCustomerBOD;
                strFN = "CustomerAvatarPath"; drDB[strFN] = strCustomerAvatarPath;
                strFN = "CustomerUserCode"; drDB[strFN] = strCustomerUserCode;
                strFN = "CustomerIDCardNo"; drDB[strFN] = strCustomerIDCardNo;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "CreateDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "CreateBy"; drDB[strFN] = strWAUserCode;
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_Customer.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_Customer" // strTableName
                    , dtDB_Mst_Customer // dtData
                                        //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAMst_Customer_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objCustomerCode
            , object objCustomerTypeCode
            , object objCustomerName
            , object objCustomerGender
            , object objCustomerPhoneNo
            , object objCustomerMobileNo
            , object objCustomerAddress
            , object objCustomerEmail
            , object objCustomerBOD
            , object objCustomerAvatarPath
            , object objCustomerUserCode
            , object objCustomerIDCardNo
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Customer_Update";
            string strErrorCodeDefault = TError.ErrDA.Mst_Customer_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objCustomerCode", objCustomerCode
                , "objCustomerTypeCode", objCustomerTypeCode
                , "objCustomerName", objCustomerName
                , "objCustomerGender", objCustomerGender
                , "objCustomerPhoneNo", objCustomerPhoneNo
                , "objCustomerMobileNo", objCustomerMobileNo
                , "objCustomerAddress", objCustomerAddress
                , "objCustomerEmail", objCustomerEmail
                , "objCustomerBOD", objCustomerBOD
                , "objCustomerAvatarPath", objCustomerAvatarPath
                , "objCustomerUserCode", objCustomerUserCode
                , "objCustomerIDCardNo", objCustomerIDCardNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAMst_Customer_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerCode
                        , objCustomerTypeCode
                        , objCustomerName
                        , objCustomerGender
                        , objCustomerPhoneNo
                        , objCustomerMobileNo
                        , objCustomerAddress
                        , objCustomerEmail
                        , objCustomerBOD
                        , objCustomerAvatarPath
                        , objCustomerUserCode
                        , objCustomerIDCardNo
                        , objFlagActive
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

        private void DAMst_Customer_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCustomerCode
            , object objCustomerTypeCode
            , object objCustomerName
            , object objCustomerGender
            , object objCustomerPhoneNo
            , object objCustomerMobileNo
            , object objCustomerAddress
            , object objCustomerEmail
            , object objCustomerBOD
            , object objCustomerAvatarPath
            , object objCustomerUserCode
            , object objCustomerIDCardNo
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Customer_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objCustomerCode", objCustomerCode
                , "objCustomerTypeCode", objCustomerTypeCode
                , "objCustomerName", objCustomerName
                , "objCustomerGender", objCustomerGender
                , "objCustomerPhoneNo", objCustomerPhoneNo
                , "objCustomerMobileNo", objCustomerMobileNo
                , "objCustomerAddress", objCustomerAddress
                , "objCustomerEmail", objCustomerEmail
                , "objCustomerBOD", objCustomerBOD
                , "objCustomerAvatarPath", objCustomerAvatarPath
                , "objCustomerUserCode", objCustomerUserCode
                , "objCustomerIDCardNo", objCustomerIDCardNo
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
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
            string strCustomerName = string.Format("{0}", objCustomerName).Trim();
            string strCustomerTypeCode = TUtils.CUtils.StdParam(objCustomerTypeCode);
            string strCustomerGender = string.Format("{0}", objCustomerGender).Trim();
            string strCustomerPhoneNo = string.Format("{0}", objCustomerPhoneNo).Trim();
            string strCustomerMobileNo = string.Format("{0}", objCustomerMobileNo).Trim();
            string strCustomerAddress = string.Format("{0}", objCustomerAddress).Trim();
            string strCustomerEmail = string.Format("{0}", objCustomerEmail).Trim();
            string strCustomerBOD = TUtils.CUtils.StdParam(objCustomerBOD);
            string strCustomerAvatarPath = string.Format("{0}", objCustomerAvatarPath).Trim();
            string strCustomerUserCode = TUtils.CUtils.StdParam(objCustomerUserCode);
            string strCustomerIDCardNo = TUtils.CUtils.StdParam(objCustomerIDCardNo);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_CustomerCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerCode".ToUpper());
            bool bUpd_CustomerName = strFt_Cols_Upd.Contains("Mst_Customer.CustomerName".ToUpper());
            bool bUpd_CustomerTypeCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerTypeCode".ToUpper());
            bool bUpd_CustomerGender = strFt_Cols_Upd.Contains("Mst_Customer.CustomerGender".ToUpper());
            bool bUpd_CustomerPhoneNo = strFt_Cols_Upd.Contains("Mst_Customer.CustomerPhoneNo".ToUpper());
            bool bUpd_CustomerMobileNo = strFt_Cols_Upd.Contains("Mst_Customer.CustomerMobileNo".ToUpper());
            bool bUpd_CustomerAddress = strFt_Cols_Upd.Contains("Mst_Customer.CustomerAddress".ToUpper());
            bool bUpd_CustomerEmail = strFt_Cols_Upd.Contains("Mst_Customer.CustomerEmail".ToUpper());
            bool bUpd_CustomerBOD = strFt_Cols_Upd.Contains("Mst_Customer.CustomerBOD".ToUpper());
            bool bUpd_CustomerAvatarPath = strFt_Cols_Upd.Contains("Mst_Customer.CustomerAvatarPath".ToUpper());
            bool bUpd_CustomerUserCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerUserCode".ToUpper());
            bool bUpd_CustomerIDCardNo = strFt_Cols_Upd.Contains("Mst_Customer.CustomerIDCardNo".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Customer.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_Customer = null;
            {
                ////
                DAMst_Customer_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strCustomerCode // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Customer // dtDB_Mst_Tour
                    );
                ////
                if (strCustomerMobileNo == null || strCustomerMobileNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerMobileNo", strCustomerMobileNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_UpdateX_InvalidCustomerMobileNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strCustomerEmail == null || strCustomerEmail.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerEmail", strCustomerEmail
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Customer_UpdateX_InvalidCustomerEmail
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
                DataRow drDB = dtDB_Mst_Customer.Rows[0];
                if (bUpd_CustomerCode) { strFN = "CustomerCode"; drDB[strFN] = strCustomerCode; alColumnEffective.Add(strFN); }
                if (bUpd_CustomerPhoneNo) { strFN = "CustomerPhoneNo"; drDB[strFN] = strCustomerPhoneNo; alColumnEffective.Add(strFN); }
                if (bUpd_CustomerMobileNo) { strFN = "CustomerMobileNo"; drDB[strFN] = strCustomerMobileNo; alColumnEffective.Add(strFN); }
                if (bUpd_CustomerEmail) { strFN = "CustomerEmail"; drDB[strFN] = strCustomerEmail; alColumnEffective.Add(strFN); }
                if (bUpd_CustomerAddress) { strFN = "CustomerAddress"; drDB[strFN] = strCustomerAddress; alColumnEffective.Add(strFN); }
                if (bUpd_CustomerBOD) { strFN = "CustomerBOD"; drDB[strFN] = strCustomerBOD; alColumnEffective.Add(strFN); }
                ////
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);
                // Save:
                _cf.db.SaveData(
                    "Mst_Customer"
                    , dtDB_Mst_Customer
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAMst_Customer_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCustomerCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Customer_Delete";
            string strErrorCodeDefault = TError.ErrDA.Mst_Customer_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objCustomerCode", objCustomerCode
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Customer_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAMst_Customer_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerCode // objCustomerCode
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

        private void DAMst_Customer_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCustomerCode
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Customer_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objCustomerCode", objCustomerCode
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);

            ////
            DataTable dtDB_Mst_Customer = null;
            {
                ////
                DAMst_Customer_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strCustomerCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_Customer
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_Customer.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_Customer"
                    , dtDB_Mst_Customer
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_Customer: WAS
        public DataSet WAS_DAMst_Customer_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out DA_RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new DA_RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Customer_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Customer_Get;
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
                List<DA_Mst_Customer> lst_Mst_Customer = new List<DA_Mst_Customer>();
                bool bGet_Mst_Customer = (objRQ_Mst_Customer.Rt_Cols_Mst_Customer != null && objRQ_Mst_Customer.Rt_Cols_Mst_Customer.Length > 0);
                #endregion

                #region // WS_Mst_Customer_Get:
                mdsResult = DAMst_Customer_Get(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_Mst_Region.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Customer.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Customer.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Customer.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_Customer.Rt_Cols_Mst_Customer // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Customer.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Customer)
                    {
                        DataTable dt_Mst_Customer = mdsResult.Tables["Mst_Customer"].Copy();
                        lst_Mst_Customer = TUtils.DataTableCmUtils.ToListof<DA_Mst_Customer>(dt_Mst_Customer);
                        objRT_Mst_Customer.Lst_Mst_Customer = lst_Mst_Customer;
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

        public DataSet WAS_DAMst_Customer_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out DA_RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new DA_RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Customer_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Customer_Create;
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
                List<DA_Mst_Customer> lst_Mst_Customer = new List<DA_Mst_Customer>();
                #endregion

                #region // Mst_Customer_Create:
                mdsResult = DAMst_Customer_Create(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Customer.Mst_Customer.CustomerCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerTypeCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerName
                    , objRQ_Mst_Customer.Mst_Customer.CustomerGender
                    , objRQ_Mst_Customer.Mst_Customer.CustomerPhoneNo
                    , objRQ_Mst_Customer.Mst_Customer.CustomerMobileNo
                    , objRQ_Mst_Customer.Mst_Customer.CustomerAddress
                    , objRQ_Mst_Customer.Mst_Customer.CustomerEmail
                    , objRQ_Mst_Customer.Mst_Customer.CustomerBOD
                    , objRQ_Mst_Customer.Mst_Customer.CustomerAvatarPath
                    , objRQ_Mst_Customer.Mst_Customer.CustomerUserCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerIDCardNo
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

        public DataSet WAS_DAMst_Customer_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out DA_RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new DA_RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Customer_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Customer_Update;
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
                List<DA_Mst_Customer> lst_Mst_Customer = new List<DA_Mst_Customer>();
                #endregion

                #region // Mst_Customer_Update:
                mdsResult = DAMst_Customer_Update(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Customer.Mst_Customer.CustomerCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerTypeCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerName
                    , objRQ_Mst_Customer.Mst_Customer.CustomerGender
                    , objRQ_Mst_Customer.Mst_Customer.CustomerPhoneNo
                    , objRQ_Mst_Customer.Mst_Customer.CustomerMobileNo
                    , objRQ_Mst_Customer.Mst_Customer.CustomerAddress
                    , objRQ_Mst_Customer.Mst_Customer.CustomerEmail
                    , objRQ_Mst_Customer.Mst_Customer.CustomerBOD
                    , objRQ_Mst_Customer.Mst_Customer.CustomerAvatarPath
                    , objRQ_Mst_Customer.Mst_Customer.CustomerUserCode
                    , objRQ_Mst_Customer.Mst_Customer.CustomerIDCardNo
                    , objRQ_Mst_Customer.Mst_Customer.FlagActive

                    , objRQ_Mst_Customer.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_DAMst_Customer_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out DA_RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new DA_RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Customer_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Customer_Delete;
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
                List<DA_Mst_Customer> lst_Mst_Customer = new List<DA_Mst_Customer>();
                #endregion

                #region // Mst_Customer_Update:
                mdsResult = DAMst_Customer_Delete(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Customer.Mst_Customer.CustomerCode
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

        #region // Mst_TourDetail
        private void Mst_TourDetail_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objTourCode
            , object objIDNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_TourDetail
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_TourDetail t --//[mylock]
					where (1=1)
						and t.TourCode = @objTourCode
						and t.IDNo = @objIDNo
					;
				");
            dtDB_Mst_TourDetail = _cf.db.ExecQuery(
                strSqlExec
                , "@objTourCode", objTourCode
                , "@objIDNo", objIDNo
                ).Tables[0];
            dtDB_Mst_TourDetail.TableName = "Mst_TourDetail";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_TourDetail.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourCode", objTourCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourDetail_CheckDB_TourDetailNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_TourDetail.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TourCode", objTourCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourDetail_CheckDB_TourDetailExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_TourDetail.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.TourCode", objTourCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_TourDetail.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_TourDetail_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_TourDetail_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_TourDetail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TourDetail_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TourDetail", strRt_Cols_Mst_TourDetail
                //, "strRt_Cols_Mst_Tour", strRt_Cols_Mst_Tour
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
                // Sys_User_CheckAuthentication(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strWAUserPassword
                // );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Mst_TourDetail = (strRt_Cols_Mst_TourDetail != null && strRt_Cols_Mst_TourDetail.Length > 0);
                //bool bGet_Mst_Tour = (strRt_Cols_Mst_Tour != null && strRt_Cols_Mst_Tour.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mtd.TourCode
							, mtd.IDNo
						into #tbl_Mst_TourDetail_Filter_Draft
						from Mst_TourDetail mtd --//[mylock]
							left join Mst_Tour mt --//[mylock]
								on mtd.TourCode = mt.TourCode
						where (1=1)
							zzzzClauseWhere_strFilterWhereClause
						order by mtd.TourCode, mtd.IDNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Mst_TourDetail_Filter
						from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_TourDetail --------:
						zzzzClauseSelect_Mst_TourDetail_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TourDetail_Filter_Draft;
						--drop table #tbl_Mst_TourDetail_Filter;
					"
                    );
                ////
                string zzzzClauseSelect_Mst_TourDetail_zOut = "-- Nothing.";
                if (bGet_Mst_TourDetail)
                {
                    #region // bGet_Mst_TourDetail:
                    zzzzClauseSelect_Mst_TourDetail_zOut = CmUtils.StringUtils.Replace(@"
							---- Mst_TourDetail:
							select
								t.MyIdxSeq
								, mtd.*
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								inner join Mst_TourDetail mtd --//[mylock]
									on t.TourCode = mtd.TourCode
										and t.IDNo = mtd.IDNo
                                left join Mst_Tour mt --//[mylock]
                                    on mt.TourCode = mtd.TourCode
                            where (1=1)
                                and mt.FlagActive = '1'
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
                            , "Mst_TourDetail" // strTableNameDB
                            , "Mst_TourDetail." // strPrefixStd
                            , "mtd." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Tour" // strTableNameDB
                            , "Mst_Tour." // strPrefixStd
                            , "mt." // strPrefixAlias
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
                    , "zzzzClauseSelect_Mst_TourDetail_zOut", zzzzClauseSelect_Mst_TourDetail_zOut
                    //, "zzzzClauseSelect_Mst_Tour_zOut", zzzzClauseSelect_Mst_Tour_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_TourDetail)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourDetail";
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

        public DataSet DAMst_TourDetail_GetForView(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_TourDetail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TourDetail_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_GetForView;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TourDetail", strRt_Cols_Mst_TourDetail
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
                // Sys_User_CheckAuthentication(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strWAUserPassword
                // );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Mst_TourDetail = (strRt_Cols_Mst_TourDetail != null && strRt_Cols_Mst_TourDetail.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mtd.TourCode
							, mtd.IDNo
						into #tbl_Mst_TourDetail_Filter_Draft
						from Mst_TourDetail mtd --//[mylock]
							left join Mst_Tour mt --//[mylock]
								on mtd.TourCode = mt.TourCode
						where (1=1)
							zzzzClauseWhere_strFilterWhereClause
						order by mtd.TourCode, mtd.IDNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Mst_TourDetail_Filter
						from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_TourDetail --------:
						zzzzClauseSelect_Mst_TourDetail_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TourDetail_Filter_Draft;
						--drop table #tbl_Mst_TourDetail_Filter;
					"
                    );
                ////
                string zzzzClauseSelect_Mst_TourDetail_zOut = "-- Nothing.";
                if (bGet_Mst_TourDetail)
                {
                    #region // bGet_Mst_TourDetail:
                    zzzzClauseSelect_Mst_TourDetail_zOut = CmUtils.StringUtils.Replace(@"
							---- Mst_TourDetail:
							select
								t.MyIdxSeq
								, mtd.*
								, mt.TourName mt_TourName
								, mt.TourType mt_TourType
								, mtt.TourTypeName mtt_TourTypeName
								, mt.TourDesc mt_TourDesc
								, mt.TourThemePath mt_TourThemePath
								, mt.TourImage1Path mt_TourImage1Path
								, mt.TourImage2Path mt_TourImage2Path
								, mt.TourImage3Path mt_TourImage3Path
								, mt.TourImage4Path mt_TourImage4Path
								, mt.TourDuration mt_TourDuration
								, mt.TourDayDuration mt_TourDayDuration
								, mt.TourNightDuration mt_TourNightDuration
								, mt.TourTouristNumber mt_TourTouristNumber
								, mt.TourTransport mt_TourTransport
								, mt.TourListDest mt_TourListDest
								, mt.TourFood mt_TourFood
								, mt.TourHotel mt_TourHotel
								, mt.TourIdealTime mt_TourIdealTime
								, mt.TourIdealPeople mt_TourIdealPeople
								, mt.TourPreferential mt_TourPreferential
								, mt.TourStartPoint mt_TourStartPoint
								, mt.TourPrice mt_TourPrice
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								inner join Mst_TourDetail mtd --//[mylock]
									on t.TourCode = mtd.TourCode
										and t.IDNo = mtd.IDNo
                                left join Mst_Tour mt --//[mylock]
                                    on mt.TourCode = mtd.TourCode
								left join Mst_TourType mtt --//[mylock]
                                    on mt.TourType = mtt.TourType
                            where (1=1)
                                and mt.FlagActive = '1'
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
                            , "Mst_TourDetail" // strTableNameDB
                            , "Mst_TourDetail." // strPrefixStd
                            , "mtd." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Tour" // strTableNameDB
                            , "Mst_Tour." // strPrefixStd
                            , "mt." // strPrefixAlias
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
                    , "zzzzClauseSelect_Mst_TourDetail_zOut", zzzzClauseSelect_Mst_TourDetail_zOut
                    //, "zzzzClauseSelect_Mst_Tour_zOut", zzzzClauseSelect_Mst_Tour_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_TourDetail)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourDetail";
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

        public DataSet DAMst_TourDetail_GetForViewAll(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_TourDetail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TourDetail_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_GetForViewAll;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TourDetail", strRt_Cols_Mst_TourDetail
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
                // Sys_User_CheckAuthentication(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strWAUserPassword
                // );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Mst_TourDetail = (strRt_Cols_Mst_TourDetail != null && strRt_Cols_Mst_TourDetail.Length > 0);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mtd.TourCode
							, mtd.IDNo
						into #tbl_Mst_TourDetail_Filter_Draft
						from Mst_TourDetail mtd --//[mylock]
							left join Mst_Tour mt --//[mylock]
								on mtd.TourCode = mt.TourCode
						where (1=1)
							zzzzClauseWhere_strFilterWhereClause
						order by mtd.TourCode, mtd.IDNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Mst_TourDetail_Filter
						from #tbl_Mst_TourDetail_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_TourDetail --------:
						zzzzClauseSelect_Mst_TourDetail_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TourDetail_Filter_Draft;
						--drop table #tbl_Mst_TourDetail_Filter;
					"
                    );
                ////
                string zzzzClauseSelect_Mst_TourDetail_zOut = "-- Nothing.";
                if (bGet_Mst_TourDetail)
                {
                    #region // bGet_Mst_TourDetail:
                    zzzzClauseSelect_Mst_TourDetail_zOut = CmUtils.StringUtils.Replace(@"
							---- Mst_TourDetail:
							select
								t.MyIdxSeq
								, mtd.*
								, mt.TourName mt_TourName
								, mt.TourType mt_TourType
								, mtt.TourTypeName mtt_TourTypeName
								, mt.TourDesc mt_TourDesc
								, mt.TourThemePath mt_TourThemePath
								, mt.TourImage1Path mt_TourImage1Path
								, mt.TourImage2Path mt_TourImage2Path
								, mt.TourImage3Path mt_TourImage3Path
								, mt.TourImage4Path mt_TourImage4Path
								, mt.TourDuration mt_TourDuration
								, mt.TourDayDuration mt_TourDayDuration
								, mt.TourNightDuration mt_TourNightDuration
								, mt.TourTouristNumber mt_TourTouristNumber
								, mt.TourTransport mt_TourTransport
								, mt.TourListDest mt_TourListDest
								, mt.TourFood mt_TourFood
								, mt.TourHotel mt_TourHotel
								, mt.TourIdealTime mt_TourIdealTime
								, mt.TourIdealPeople mt_TourIdealPeople
								, mt.TourPreferential mt_TourPreferential
								, mt.TourStartPoint mt_TourStartPoint
								, mt.TourPrice mt_TourPrice
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								inner join Mst_TourDetail mtd --//[mylock]
									on t.TourCode = mtd.TourCode
										and t.IDNo = mtd.IDNo
                                left join Mst_Tour mt --//[mylock]
                                    on mt.TourCode = mtd.TourCode
								left join Mst_TourType mtt --//[mylock]
                                    on mt.TourType = mtt.TourType
                            where (1=1)
                                and mt.FlagActive = '1'
							order by t.MyIdxSeq asc
							;
							
							---- Mst_TourDetailDate:
							select distinct
								mtdd.*
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								left join Mst_TourDetailDate mtdd --//[mylock]
									on t.TourCode = mtdd.TourCode
										and t.IDNo = mtdd.IDNo
							where (1=1)
							
							---- Mst_TourScheduleDetail:
							select
                                mtsd.*
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								left join Mst_TourScheduleDetail mtsd --//[mylock]
									on t.TourCode = mtsd.TourCode
							where (1=1)
							
							---- Mst_TourSchedule:
							select
								mts.*
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								left join Mst_TourSchedule mts --//[mylock]
									on t.TourCode = mts.TourCode
							where (1=1)
							
							---- Mst_TourDestImages:
							select
								mtdi.*
							from #tbl_Mst_TourDetail_Filter t --//[mylock]
								left join Mst_TourDestImages mtdi --//[mylock]
									on t.TourCode = mtdi.TourCode
							where (1=1)
							
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
                            , "Mst_TourDetail" // strTableNameDB
                            , "Mst_TourDetail." // strPrefixStd
                            , "mtd." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Tour" // strTableNameDB
                            , "Mst_Tour." // strPrefixStd
                            , "mt." // strPrefixAlias
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
                    , "zzzzClauseSelect_Mst_TourDetail_zOut", zzzzClauseSelect_Mst_TourDetail_zOut
                    //, "zzzzClauseSelect_Mst_Tour_zOut", zzzzClauseSelect_Mst_Tour_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_TourDetail)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourDetail";
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourDetailDate";
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourScheduleDetail";
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourSchedule";
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourDestImages";
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

        public DataSet DAMst_TourDetail_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTourCode
            , object objIDNo
            , object objDateStart
            , object objDateEnd
            , object objTouristNumberAll
            , object objTouristNumberLeft
            , object objTourGuide1
            , object objTourGuide2
            , object objGatherDate
            , object objGatherTime
            , object objGatherAddress
            , object objGoFlightNo
            , object objReturnFlightNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourDetail_Create";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objTourCode", objTourCode
                    , "objIDNo", objIDNo
                    , "objDateStart", objDateStart
                    , "objDateEnd", objDateEnd
                    , "objTouristNumberAll", objTouristNumberAll
                    , "objTouristNumberLeft", objTouristNumberLeft
                    , "objTourGuide1", objTourGuide1
                    , "objTourGuide2", objTourGuide2
                    , "objGatherDate", objGatherDate
                    , "objGatherTime", objGatherTime
                    , "objGatherAddress", objGatherAddress
                    , "objGoFlightNo", objGoFlightNo
                    , "objReturnFlightNo", objReturnFlightNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_TourDetail_CreateX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourDetail_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode
                        , objIDNo
                        , objDateStart
                        , objDateEnd
                        , objTouristNumberAll
                        , objTouristNumberLeft
                        , objTourGuide1
                        , objTourGuide2
                        , objGatherDate
                        , objGatherTime
                        , objGatherAddress
                        , objGoFlightNo
                        , objReturnFlightNo
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

        private void DAMst_TourDetail_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            , object objIDNo
            , object objDateStart
            , object objDateEnd
            , object objTouristNumberAll
            , object objTouristNumberLeft
            , object objTourGuide1
            , object objTourGuide2
            , object objGatherDate
            , object objGatherTime
            , object objGatherAddress
            , object objGoFlightNo
            , object objReturnFlightNo
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_TourDetail_CreateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objTourCode", objTourCode
                , "objIDNo", objIDNo
                , "objDateStart", objDateStart
                , "objDateEnd", objDateEnd
                , "objTouristNumberAll", objTouristNumberAll
                , "objTouristNumberLeft", objTouristNumberLeft
                , "objTourGuide1", objTourGuide1
                , "objTourGuide2", objTourGuide2
                , "objGatherDate", objGatherDate
                , "objGatherTime", objGatherTime
                , "objGatherAddress", objGatherAddress
                , "objGoFlightNo", objGoFlightNo
                , "objReturnFlightNo", objReturnFlightNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);
            string strIDNo = TUtils.CUtils.StdParam(objIDNo);
            string strDateStart = TUtils.CUtils.StdParam(objDateStart);
            string strDateEnd = TUtils.CUtils.StdParam(objDateEnd);
            int iTouristNumberAll = TUtils.CUtils.ConvertToInt32(objTouristNumberAll);
            int iTouristNumberLeft = TUtils.CUtils.ConvertToInt32(objTouristNumberLeft);
            string strTourGuide1 = TUtils.CUtils.StdParam(objTourGuide1);
            string strTourGuide2 = TUtils.CUtils.StdParam(objTourGuide2);
            string strGatherDate = TUtils.CUtils.StdParam(objGatherDate);
            string strGatherTime = TUtils.CUtils.StdParam(objGatherTime);
            string strGatherAddress = string.Format("{0}", objGatherAddress).Trim();
            string strGoFlightNo = TUtils.CUtils.StdParam(objGoFlightNo);
            string strReturnFlightNo = TUtils.CUtils.StdParam(objReturnFlightNo);

            ////
            DataTable dtDB_Mst_TourDetail = null;
            {
                DataTable dtDB_Mst_Tour = null;
                ////
                Mst_Tour_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTourCode // objSupCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active
                    , out dtDB_Mst_Tour // dtDB_Mst_Part
                    );
                ////
                Mst_TourDetail_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTourCode // objTourType
                    , strIDNo // objTDCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , ""
                    , out dtDB_Mst_TourDetail // dtDB_Mst_TourDetail
                    );
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_TourDetail.NewRow();
                strFN = "TourCode"; drDB[strFN] = strTourCode;
                strFN = "IDNo"; drDB[strFN] = strIDNo;
                strFN = "DateStart"; drDB[strFN] = strDateStart;
                strFN = "DateEnd"; drDB[strFN] = strDateEnd;
                strFN = "TouristNumberAll"; drDB[strFN] = iTouristNumberAll;
                strFN = "TouristNumberLeft"; drDB[strFN] = iTouristNumberLeft;
                strFN = "TourGuide1"; drDB[strFN] = strTourGuide1;
                strFN = "TourGuide2"; drDB[strFN] = strTourGuide2;
                strFN = "GatherDate"; drDB[strFN] = strGatherDate;
                strFN = "GatherTime"; drDB[strFN] = strGatherTime;
                strFN = "GatherAddress"; drDB[strFN] = strGatherAddress;
                strFN = "GoFlightNo"; drDB[strFN] = strGoFlightNo;
                strFN = "ReturnFlightNo"; drDB[strFN] = strReturnFlightNo;
                strFN = "CreateDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "CreateBy"; drDB[strFN] = strWAUserCode;
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_TourDetail.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_TourDetail" // strTableName
                    , dtDB_Mst_TourDetail // dtData
                                          //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAMst_TourDetail_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTourCode
            , object objIDNo
            , object objDateStart
            , object objDateEnd
            , object objTouristNumberAll
            , object objTouristNumberLeft
            , object objTourGuide1
            , object objTourGuide2
            , object objGatherDate
            , object objGatherTime
            , object objGatherAddress
            , object objGoFlightNo
            , object objReturnFlightNo
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourDetail_Update";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTourCode", objTourCode
                , "objIDNo", objIDNo
                , "objDateStart", objDateStart
                , "objDateEnd", objDateEnd
                , "objTouristNumberAll", objTouristNumberAll
                , "objTouristNumberLeft", objTouristNumberLeft
                , "objTourGuide1", objTourGuide1
                , "objTourGuide2", objTourGuide2
                , "objGatherDate", objGatherDate
                , "objGatherTime", objGatherTime
                , "objGatherAddress", objGatherAddress
                , "objGoFlightNo", objGoFlightNo
                , "objReturnFlightNo", objReturnFlightNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_TourDetail_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourDetail_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode
                        , objIDNo
                        , objDateStart
                        , objDateEnd
                        , objTouristNumberAll
                        , objTouristNumberLeft
                        , objTourGuide1
                        , objTourGuide2
                        , objGatherDate
                        , objGatherTime
                        , objGatherAddress
                        , objGoFlightNo
                        , objReturnFlightNo
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

        private void DAMst_TourDetail_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            , object objIDNo
            , object objDateStart
            , object objDateEnd
            , object objTouristNumberAll
            , object objTouristNumberLeft
            , object objTourGuide1
            , object objTourGuide2
            , object objGatherDate
            , object objGatherTime
            , object objGatherAddress
            , object objGoFlightNo
            , object objReturnFlightNo
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Tour_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTourCode", objTourCode
                , "objIDNo", objIDNo
                , "objDateStart", objDateStart
                , "objDateEnd", objDateEnd
                , "objTouristNumberAll", objTouristNumberAll
                , "objTouristNumberLeft", objTouristNumberLeft
                , "objTourGuide1", objTourGuide1
                , "objTourGuide2", objTourGuide2
                , "objGatherDate", objGatherDate
                , "objGatherTime", objGatherTime
                , "objGatherAddress", objGatherAddress
                , "objGoFlightNo", objGoFlightNo
                , "objReturnFlightNo", objReturnFlightNo
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
            ////
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);
            string strIDNo = TUtils.CUtils.StdParam(objIDNo);
            string strDateStart = TUtils.CUtils.StdParam(objDateStart);
            string strDateEnd = TUtils.CUtils.StdParam(objDateEnd);
            int iTouristNumberAll = TUtils.CUtils.ConvertToInt32(objTouristNumberAll);
            int iTouristNumberLeft = TUtils.CUtils.ConvertToInt32(objTouristNumberLeft);
            string strTourGuide1 = TUtils.CUtils.StdParam(objTourGuide1);
            string strTourGuide2 = TUtils.CUtils.StdParam(objTourGuide2);
            string strGatherDate = TUtils.CUtils.StdParam(objGatherDate);
            string strGatherTime = TUtils.CUtils.StdParam(objGatherTime);
            string strGatherAddress = string.Format("{0}", objGatherAddress).Trim();
            string strGoFlightNo = TUtils.CUtils.StdParam(objGoFlightNo);
            string strReturnFlightNo = TUtils.CUtils.StdParam(objReturnFlightNo);
            ////
            bool bUpd_DateStart = strFt_Cols_Upd.Contains("Mst_TourDetail.DateStart".ToUpper());
            bool bUpd_DateEnd = strFt_Cols_Upd.Contains("Mst_TourDetail.DateEnd".ToUpper());
            bool bUpd_TourGuide1 = strFt_Cols_Upd.Contains("Mst_TourDetail.TourGuide1".ToUpper());
            bool bUpd_TourGuide2 = strFt_Cols_Upd.Contains("Mst_TourDetail.TourGuide2".ToUpper());
            bool bUpd_GatherDate = strFt_Cols_Upd.Contains("Mst_TourDetail.GatherDate".ToUpper());
            bool bUpd_GatherTime = strFt_Cols_Upd.Contains("Mst_TourDetail.GatherTime".ToUpper());
            bool bUpd_GatherAddress = strFt_Cols_Upd.Contains("Mst_TourDetail.GatherAddress".ToUpper());
            bool bUpd_GoFlightNo = strFt_Cols_Upd.Contains("Mst_TourDetail.GoFlightNo".ToUpper());
            bool bUpd_ReturnFlightNo = strFt_Cols_Upd.Contains("Mst_TourDetail.ReturnFlightNo".ToUpper());

            ////
            DataTable dtDB_Mst_TourDetail = null;
            {
                ////
                Mst_TourDetail_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTourCode // objTourCode
                    , strIDNo
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_TourDetail // dtDB_Mst_TourDetail
                    );
            }

            #endregion

            #region // Save Mst_Supplier:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_TourDetail.Rows[0];
                if (bUpd_DateStart) { strFN = "DateStart"; drDB[strFN] = strDateStart; alColumnEffective.Add(strFN); }
                if (bUpd_DateEnd) { strFN = "DateEnd"; drDB[strFN] = strDateEnd; alColumnEffective.Add(strFN); }
                if (bUpd_TourGuide1) { strFN = "TourGuide1"; drDB[strFN] = strTourGuide1; alColumnEffective.Add(strFN); }
                if (bUpd_TourGuide2) { strFN = "TourGuide2"; drDB[strFN] = strTourGuide2; alColumnEffective.Add(strFN); }
                if (bUpd_GatherDate) { strFN = "GatherDate"; drDB[strFN] = strGatherDate; alColumnEffective.Add(strFN); }
                if (bUpd_GatherTime) { strFN = "GatherTime"; drDB[strFN] = strGatherTime; alColumnEffective.Add(strFN); }
                if (bUpd_GatherAddress) { strFN = "GatherAddress"; drDB[strFN] = strGatherAddress; alColumnEffective.Add(strFN); }
                if (bUpd_GoFlightNo) { strFN = "GoFlightNo"; drDB[strFN] = strGoFlightNo; alColumnEffective.Add(strFN); }
                if (bUpd_ReturnFlightNo) { strFN = "ReturnFlightNo"; drDB[strFN] = strReturnFlightNo; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("dd-MM-yyyy HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);
                // Save:
                _cf.db.SaveData(
                    "Mst_TourDetail"
                    , dtDB_Mst_TourDetail
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet Mst_TourDetail_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objTourCode
            , object objIDNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourDetail_Delete";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourDetail_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objTourCode", objTourCode
                    , "objIDNo", objIDNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourDetail_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTourCode // objTourCode
                        , objIDNo // objTDCode
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

        private void DAMst_TourDetail_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTourCode
            , object objIDNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_TourDetail_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objTourCode", objTourCode
                , "objIDNo", objIDNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTourCode = TUtils.CUtils.StdParam(objTourCode);
            string strIDNo = TUtils.CUtils.StdParam(objIDNo);

            ////
            DataTable dtDB_Mst_TourDetail = null;
            {
                ////
                Mst_TourDetail_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strTourCode
                     , strIDNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_TourDetail
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_TourDetail.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_TourDetail"
                    , dtDB_Mst_TourDetail
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_TourDetail: WAS
        public DataSet WAS_DAMst_TourDetail_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_Get;
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
                List<DA_Mst_TourDetail> lst_Mst_TourDetail = new List<DA_Mst_TourDetail>();
                //List<DA_Mst_Tour> lst_Mst_Tour = new List<DA_Mst_Tour>();
                List<DA_Mst_TourDestImages> lst_Mst_TourDestImages = new List<DA_Mst_TourDestImages>();
                bool bGet_Mst_TourDetail = (objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail != null && objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = DAMst_TourDetail_Get(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourDetail.Ft_RecordStart
                    , objRQ_Mst_TourDetail.Ft_RecordCount
                    , objRQ_Mst_TourDetail.Ft_WhereClause
                    ////
                    , objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TourDetail.MySummaryTable = lst_MySummaryTable[0];

                    if(bGet_Mst_TourDetail)
                    {
                        ////
                        DataTable dt_Mst_TourDetail = mdsResult.Tables["Mst_TourDetail"].Copy();
                        lst_Mst_TourDetail = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourDetail>(dt_Mst_TourDetail);
                        objRT_Mst_TourDetail.Lst_Mst_TourDetail = lst_Mst_TourDetail;
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

        public DataSet WAS_DAMst_TourDetail_GetForView(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_GetForView";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_GetForView;
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
                List<DA_Mst_TourDetail> lst_Mst_TourDetail = new List<DA_Mst_TourDetail>();
                bool bGet_Mst_TourDetail = (objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail != null && objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = DAMst_TourDetail_GetForView(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourDetail.Ft_RecordStart
                    , objRQ_Mst_TourDetail.Ft_RecordCount
                    , objRQ_Mst_TourDetail.Ft_WhereClause
                    ////
                    , objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TourDetail.MySummaryTable = lst_MySummaryTable[0];

                    if (bGet_Mst_TourDetail)
                    {
                        ////
                        DataTable dt_Mst_TourDetail = mdsResult.Tables["Mst_TourDetail"].Copy();
                        lst_Mst_TourDetail = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourDetail>(dt_Mst_TourDetail);
                        objRT_Mst_TourDetail.Lst_Mst_TourDetail = lst_Mst_TourDetail;
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

        public DataSet WAS_DAMst_TourDetail_GetForViewAll(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_GetForViewAll";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_GetForViewAll;
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
                List<DA_Mst_TourDetail> lst_Mst_TourDetail = new List<DA_Mst_TourDetail>();
                List<DA_Mst_TourDetailDate> lst_Mst_TourDetailDate = new List<DA_Mst_TourDetailDate>();
                List<DA_Mst_TourSchedule> lst_Mst_TourSchedule = new List<DA_Mst_TourSchedule>();
                List<DA_Mst_TourScheduleDetail> lst_Mst_TourScheduleDetail = new List<DA_Mst_TourScheduleDetail>();
                List<DA_Mst_TourDestImages> lst_Mst_TourDestImages = new List<DA_Mst_TourDestImages>();
                bool bGet_Mst_TourDetail = (objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail != null && objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = DAMst_TourDetail_GetForViewAll(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourDetail.Ft_RecordStart
                    , objRQ_Mst_TourDetail.Ft_RecordCount
                    , objRQ_Mst_TourDetail.Ft_WhereClause
                    ////
                    , objRQ_Mst_TourDetail.Rt_Cols_Mst_TourDetail
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TourDetail.MySummaryTable = lst_MySummaryTable[0];

                    if (bGet_Mst_TourDetail)
                    {
                        ////
                        DataTable dt_Mst_TourDetail = mdsResult.Tables["Mst_TourDetail"].Copy();
                        lst_Mst_TourDetail = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourDetail>(dt_Mst_TourDetail);
                        objRT_Mst_TourDetail.Lst_Mst_TourDetail = lst_Mst_TourDetail;
                        ////
                        DataTable dt_Mst_TourDetailDate = mdsResult.Tables["Mst_TourDetailDate"].Copy();
                        lst_Mst_TourDetailDate = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourDetailDate>(dt_Mst_TourDetailDate);
                        objRT_Mst_TourDetail.Lst_Mst_TourDetailDate = lst_Mst_TourDetailDate;
                        ////
                        DataTable dt_Mst_TourSchedule = mdsResult.Tables["Mst_TourSchedule"].Copy();
                        lst_Mst_TourSchedule = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourSchedule>(dt_Mst_TourSchedule);
                        objRT_Mst_TourDetail.Lst_Mst_TourSchedule = lst_Mst_TourSchedule;
                        ////
                        DataTable dt_Mst_TourScheduleDetail = mdsResult.Tables["Mst_TourScheduleDetail"].Copy();
                        lst_Mst_TourScheduleDetail = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourScheduleDetail>(dt_Mst_TourScheduleDetail);
                        objRT_Mst_TourDetail.Lst_Mst_TourScheduleDetail = lst_Mst_TourScheduleDetail;
                        ////
                        DataTable dt_Mst_TourDestImages = mdsResult.Tables["Mst_TourDestImages"].Copy();
                        lst_Mst_TourDestImages = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourDestImages>(dt_Mst_TourDestImages);
                        objRT_Mst_TourDetail.Lst_Mst_TourDestImages = lst_Mst_TourDestImages;
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

        public DataSet WAS_DAMst_TourDetail_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_Create;
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
                List<DA_Mst_TourDetail> lst_Mst_TourDetail = new List<DA_Mst_TourDetail>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = DAMst_TourDetail_Create(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourCode
                    , objRQ_Mst_TourDetail.Mst_TourDetail.IDNo
                    , objRQ_Mst_TourDetail.Mst_TourDetail.DateStart
                    , objRQ_Mst_TourDetail.Mst_TourDetail.DateEnd
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TouristNumberAll
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TouristNumberLeft
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourGuide1
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourGuide2
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherDate
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherTime
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherAddress
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GoFlightNo
                    , objRQ_Mst_TourDetail.Mst_TourDetail.ReturnFlightNo
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

        public DataSet WAS_DAMst_TourDetail_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_Update;
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
                List<DA_Mst_TourDetail> lst_Mst_TourDetail = new List<DA_Mst_TourDetail>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = DAMst_TourDetail_Update(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourCode
                    , objRQ_Mst_TourDetail.Mst_TourDetail.IDNo
                    , objRQ_Mst_TourDetail.Mst_TourDetail.DateStart
                    , objRQ_Mst_TourDetail.Mst_TourDetail.DateEnd
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TouristNumberAll
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TouristNumberLeft
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourGuide1
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourGuide2
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherDate
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherTime
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GatherAddress
                    , objRQ_Mst_TourDetail.Mst_TourDetail.GoFlightNo
                    , objRQ_Mst_TourDetail.Mst_TourDetail.ReturnFlightNo
                    ////
                    , objRQ_Mst_TourDetail.Ft_Cols_Upd
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

        public DataSet WAS_DAMst_TourDetail_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail
            ////
            , out DA_RT_Mst_TourDetail objRT_Mst_TourDetail
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourDetail.Tid;
            objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourDetail_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourDetail_Delete;
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
                List<DA_Mst_TourDetail> lst_Mst_Part = new List<DA_Mst_TourDetail>();
                #endregion

                #region // Mst_Part_Delete:
                mdsResult = Mst_TourDetail_Delete(
                    objRQ_Mst_TourDetail.Tid // strTid
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    , objRQ_Mst_TourDetail.WAUserCode // strUserCode
                    , objRQ_Mst_TourDetail.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourDetail.Mst_TourDetail.TourCode
                    , objRQ_Mst_TourDetail.Mst_TourDetail.IDNo
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

        #region // Mst_Province
        private void DAMst_Province_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objProvinceCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Province
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Province t --//[mylock]
					where (1=1)
						and t.ProvinceCode = @objProvinceCode
					;
				");
            dtDB_Mst_Province = _cf.db.ExecQuery(
                strSqlExec
                , "@objProvinceCode", objProvinceCode
                ).Tables[0];
            dtDB_Mst_Province.TableName = "Mst_Province";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Province.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ProvinceCode", objProvinceCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Province_CheckDB_ProvinceNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Province.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ProvinceCode", objProvinceCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Province_CheckDB_ProvinceExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Province.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ProvinceCode", objProvinceCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Province.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_Province_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_Province_Get(
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
            , string strRt_Cols_Mst_Province
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Province_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_Province_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Province", strRt_Cols_Mst_Province
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
                bool bGet_Mst_Province = (strRt_Cols_Mst_Province != null && strRt_Cols_Mst_Province.Length > 0);

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
						---- #tbl_Mst_Province_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mp.ProvinceCode
						into #tbl_Mst_Province_Filter_Draft
						from Mst_Province mp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mp.ProvinceCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Province_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Province_Filter:
						select
							t.*
						into #tbl_Mst_Province_Filter
						from #tbl_Mst_Province_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Province --------:
						zzB_Select_Mst_Province_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Province_Filter_Draft;
						--drop table #tbl_Mst_Province_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_Province_zzE = "-- Nothing.";
                if (bGet_Mst_Province)
                {
                    #region // bGet_Mst_Province:
                    zzB_Select_Mst_Province_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Province:
							select
								t.MyIdxSeq
								, mp.*
							from #tbl_Mst_Province_Filter t --//[mylock]
								inner join Mst_Province mp --//[mylock]
									on t.ProvinceCode = mp.ProvinceCode
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
                            , "Mst_Province" // strTableNameDB
                            , "Mst_Province." // strPrefixStd
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
                    , "zzB_Select_Mst_Province_zzE", zzB_Select_Mst_Province_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_Province)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_Province";
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
        #endregion

        #region // Mst_Province: WAS
        public DataSet WAS_DAMst_Province_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Province objRQ_Mst_Province
            ////
            , out DA_RT_Mst_Province objRT_Mst_Province
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Province.Tid;
            objRT_Mst_Province = new DA_RT_Mst_Province();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Province_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Province_Get;
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
                List<DA_Mst_Province> lst_Mst_Province = new List<DA_Mst_Province>();
                bool bGet_Mst_Province = (objRQ_Mst_Province.Rt_Cols_Mst_Province != null && objRQ_Mst_Province.Rt_Cols_Mst_Province.Length > 0);
                #endregion

                #region // WS_Mst_Province_Get:
                mdsResult = DAMst_Province_Get(
                    objRQ_Mst_Province.Tid // strTid
                    , objRQ_Mst_Province.GwUserCode // strGwUserCode
                    , objRQ_Mst_Province.GwPassword // strGwPassword
                    , objRQ_Mst_Province.WAUserCode // strUserCode
                    , objRQ_Mst_Province.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_Mst_Province.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Province.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Province.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Province.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_Province.Rt_Cols_Mst_Province // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Province.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Province)
                    {
                        DataTable dt_Mst_Province = mdsResult.Tables["Mst_Province"].Copy();
                        lst_Mst_Province = TUtils.DataTableCmUtils.ToListof<DA_Mst_Province>(dt_Mst_Province);
                        objRT_Mst_Province.Lst_Mst_Province = lst_Mst_Province;
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
        #endregion

        #region // Mst_TourGuide
        private void DAMst_TourGuide_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objTGNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_TourGuide
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_TourGuide t --//[mylock]
					where (1=1)
						and t.TGNo = @objTGNo
					;
				");
            dtDB_Mst_TourGuide = _cf.db.ExecQuery(
                strSqlExec
                , "@objTGNo", objTGNo
                ).Tables[0];
            dtDB_Mst_TourGuide.TableName = "Mst_TourGuide";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_TourGuide.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TGNo", objTGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CheckDB_RegionNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_TourGuide.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TGNo", objTGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CheckDB_RegionExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_TourGuide.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.TGNo", objTGNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_TourGuide.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_TourGuide_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_TourGuide_Get(
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
            , string strRt_Cols_Mst_TourGuide
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TourGuide_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourGuide_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TourGuide", strRt_Cols_Mst_TourGuide
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_Mst_TourGuide = (strRt_Cols_Mst_TourGuide != null && strRt_Cols_Mst_TourGuide.Length > 0);

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
						---- #tbl_Mst_TourGuide_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mtg.TGNo
						into #tbl_Mst_TourGuide_Filter_Draft
						from Mst_TourGuide mtg --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mtg.TGNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TourGuide_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_TourGuide_Filter:
						select
							t.*
						into #tbl_Mst_TourGuide_Filter
						from #tbl_Mst_TourGuide_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_TourGuide --------:
						zzB_Select_Mst_TourGuide_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TourGuide_Filter_Draft;
						--drop table #tbl_Mst_TourGuide_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_TourGuide_zzE = "-- Nothing.";
                if (bGet_Mst_TourGuide)
                {
                    #region // bGet_Mst_TourGuide:
                    zzB_Select_Mst_TourGuide_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_TourGuide:
							select
								t.MyIdxSeq
								, mtg.*
							from #tbl_Mst_TourGuide_Filter t --//[mylock]
								inner join Mst_TourGuide mtg --//[mylock]
									on t.TGNo = mtg.TGNo
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
                            , "Mst_TourGuide" // strTableNameDB
                            , "Mst_TourGuide." // strPrefixStd
                            , "mtg." // strPrefixAlias
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
                    , "zzB_Select_Mst_TourGuide_zzE", zzB_Select_Mst_TourGuide_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_TourGuide)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_TourGuide";
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

        public DataSet DAMst_TourGuide_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTGNo
            , object objTGName
            , object objTGIDCardNo
            , object objTGAddress
            , object objTGMobileNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourGuide_Create";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourGuide_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
				    , "objTGNo", objTGNo
                , "objTGName", objTGName
                , "objTGIDCardNo", objTGIDCardNo
                , "objTGAddress", objTGAddress
                , "objTGMobileNo", objTGMobileNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourGuide_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTGNo
                        , objTGName
                        , objTGIDCardNo
                        , objTGAddress
                        , objTGMobileNo
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

        private void DAMst_TourGuide_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTGNo
            , object objTGName
            , object objTGIDCardNo
            , object objTGAddress
            , object objTGMobileNo
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Tour_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objTGNo", objTGNo
                , "objTGName", objTGName
                , "objTGIDCardNo", objTGIDCardNo
                , "objTGAddress", objTGAddress
                , "objTGMobileNo", objTGMobileNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTGNo = TUtils.CUtils.StdParam(objTGNo);
            string strTGName = string.Format("{0}", objTGName).Trim();
            string strTGIDCardNo = TUtils.CUtils.StdParam(objTGIDCardNo);
            string strTGAddress = string.Format("{0}", objTGAddress).Trim();
            string strTGMobileNo = TUtils.CUtils.StdParam(objTGMobileNo);

            ////
            DataTable dtDB_Mst_TourGuide = null;
            {
                ////
                if (strTGNo == null || strTGNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGNo", strTGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CreateX_InvalidTGNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAMst_TourGuide_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTGNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_TourGuide // dtDB_Mst_TourGuide
                    );

                ////
                if (strTGName == null || strTGName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGName", strTGName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CreateX_InvalidTGName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strTGAddress == null || strTGAddress.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGAddress", strTGAddress
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CreateX_InvalidTGAddress
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strTGMobileNo == null || strTGMobileNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGMobileNo", strTGMobileNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_CreateX_InvalidTGMobileNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Mst_TourGuide.NewRow();
                strFN = "TGNo"; drDB[strFN] = strTGNo;
                strFN = "TGName"; drDB[strFN] = strTGName;
                strFN = "TGIDCardNo"; drDB[strFN] = strTGIDCardNo;
                strFN = "TGAddress"; drDB[strFN] = strTGAddress;
                strFN = "TGMobileNo"; drDB[strFN] = strTGMobileNo;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "CreateDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "CreateBy"; drDB[strFN] = strWAUserCode;
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Mst_TourGuide.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Mst_TourGuide" // strTableName
                    , dtDB_Mst_TourGuide // dtData
                                         //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAMst_TourGuide_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objTGNo
            , object objTGName
            , object objTGIDCardNo
            , object objTGAddress
            , object objTGMobileNo
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourGuide_Update";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourGuide_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTGNo", objTGNo
                , "objTGName", objTGName
                , "objTGIDCardNo", objTGIDCardNo
                , "objTGAddress", objTGAddress
                , "objTGMobileNo", objTGMobileNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourGuide_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTGNo
                        , objTGName
                        , objTGIDCardNo
                        , objTGAddress
                        , objTGMobileNo
                        , objFlagActive
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

        private void DAMst_TourGuide_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTGNo
            , object objTGName
            , object objTGIDCardNo
            , object objTGAddress
            , object objTGMobileNo
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_TourGuide_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mst_Supplier_UpdateX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objTGNo", objTGNo
                , "objTGName", objTGName
                , "objTGIDCardNo", objTGIDCardNo
                , "objTGAddress", objTGAddress
                , "objTGMobileNo", objTGMobileNo
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
            string strTGNo = TUtils.CUtils.StdParam(objTGNo);
            string strTGName = string.Format("{0}", objTGName).Trim();
            string strTGIDCardNo = TUtils.CUtils.StdParam(objTGIDCardNo);
            string strTGAddress = string.Format("{0}", objTGAddress).Trim();
            string strTGMobileNo = TUtils.CUtils.StdParam(objTGMobileNo);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_TGNo = strFt_Cols_Upd.Contains("Mst_TourGuide.TGNo".ToUpper());
            bool bUpd_TGName = strFt_Cols_Upd.Contains("Mst_TourGuide.TGName".ToUpper());
            bool bUpd_TGIDCardNo = strFt_Cols_Upd.Contains("Mst_TourGuide.TGIDCardNo".ToUpper());
            bool bUpd_TGAddress = strFt_Cols_Upd.Contains("Mst_TourGuide.TGAddress".ToUpper());
            bool bUpd_TGMobileNo = strFt_Cols_Upd.Contains("Mst_TourGuide.TGMobileNo".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_TourGuide.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_TourGuide = null;
            {
                ////
                DAMst_TourGuide_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strTGNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_TourGuide // dtDB_Mst_TourGuide
                    );
                ////
                if (strTGName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGName", strTGName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_Update_InvalidTGName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strTGAddress.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGAddress", strTGAddress
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_Update_InvalidTGAddress
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strTGMobileNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTGMobileNo", strTGMobileNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_TourGuide_Update_InvalidTGMobileNo
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
                DataRow drDB = dtDB_Mst_TourGuide.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_TGName) { strFN = "TGName"; drDB[strFN] = strTGName; alColumnEffective.Add(strFN); }
                if (bUpd_TGIDCardNo) { strFN = "TGIDCardNo"; drDB[strFN] = strTGIDCardNo; alColumnEffective.Add(strFN); }
                if (bUpd_TGAddress) { strFN = "TGAddress"; drDB[strFN] = strTGAddress; alColumnEffective.Add(strFN); }
                if (bUpd_TGMobileNo) { strFN = "TGMobileNo"; drDB[strFN] = strTGMobileNo; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);
                // Save:
                _cf.db.SaveData(
                    "Mst_TourGuide"
                    , dtDB_Mst_TourGuide
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAMst_TourGuide_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objTGNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_TourGuide_Delete";
            string strErrorCodeDefault = TError.ErrDA.Mst_TourGuide_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objTGNo", objTGNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAMst_TourGuide_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objTGNo // objTourCode
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

        private void DAMst_TourGuide_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objTGNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_TourGuide_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objTGNo", objTGNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strTGNo = TUtils.CUtils.StdParam(objTGNo);

            ////
            DataTable dtDB_Mst_TourGuide = null;
            {
                ////
                DAMst_TourGuide_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strTGNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_Mst_TourGuide // dtDB_Mst_TourGuide
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Mst_TourGuide.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Mst_TourGuide"
                    , dtDB_Mst_TourGuide
                    );
            }
            #endregion
        }
        #endregion

        #region // Mst_TourGuide: WAS
        public DataSet WAS_DAMst_TourGuide_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide
            ////
            , out DA_RT_Mst_TourGuide objRT_Mst_TourGuide
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourGuide.Tid;
            objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourGuide_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourGuide_Get;
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
                List<DA_Mst_TourGuide> lst_Mst_TourGuide = new List<DA_Mst_TourGuide>();
                bool bGet_Mst_TourGuide = (objRQ_Mst_TourGuide.Rt_Cols_Mst_TourGuide != null && objRQ_Mst_TourGuide.Rt_Cols_Mst_TourGuide.Length > 0);
                #endregion

                #region // WS_Mst_TourGuide_Get:
                mdsResult = DAMst_TourGuide_Get(
                    objRQ_Mst_TourGuide.Tid // strTid
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    , objRQ_Mst_TourGuide.WAUserCode // strUserCode
                    , objRQ_Mst_TourGuide.WAUserPassword // strUserPassword
                                                         //, TUtils.CUtils.StdFlag(objRQ_Mst_TourGuide.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_TourGuide.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_TourGuide.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_TourGuide.Ft_WhereClause // strFt_WhereClause
                                                         //// Return:
                    , objRQ_Mst_TourGuide.Rt_Cols_Mst_TourGuide // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TourGuide.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_TourGuide)
                    {
                        DataTable dt_Mst_TourGuide = mdsResult.Tables["Mst_TourGuide"].Copy();
                        lst_Mst_TourGuide = TUtils.DataTableCmUtils.ToListof<DA_Mst_TourGuide>(dt_Mst_TourGuide);
                        objRT_Mst_TourGuide.Lst_Mst_TourGuide = lst_Mst_TourGuide;
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

        public DataSet WAS_DAMst_TourGuide_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide
            ////
            , out DA_RT_Mst_TourGuide objRT_Mst_TourGuide
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourGuide.Tid;
            objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourGuide_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourGuide_Create;
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
                List<DA_Mst_TourGuide> lst_Mst_TourGuide = new List<DA_Mst_TourGuide>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = DAMst_TourGuide_Create(
                    objRQ_Mst_TourGuide.Tid // strTid
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    , objRQ_Mst_TourGuide.WAUserCode // strUserCode
                    , objRQ_Mst_TourGuide.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGNo
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGName
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGIDCardNo
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGAddress
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGMobileNo
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

        public DataSet WAS_DAMst_TourGuide_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide
            ////
            , out DA_RT_Mst_TourGuide objRT_Mst_TourGuide
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourGuide.Tid;
            objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourGuide_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourGuide_Update;
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
                List<DA_Mst_TourGuide> lst_Mst_TourGuide = new List<DA_Mst_TourGuide>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = DAMst_TourGuide_Update(
                    objRQ_Mst_TourGuide.Tid // strTid
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    , objRQ_Mst_TourGuide.WAUserCode // strUserCode
                    , objRQ_Mst_TourGuide.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGNo
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGName
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGIDCardNo
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGAddress
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGMobileNo
                    , objRQ_Mst_TourGuide.Mst_TourGuide.FlagActive
                    ////
                    , objRQ_Mst_TourGuide.Ft_Cols_Upd
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

        public DataSet WAS_DAMst_TourGuide_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide
            ////
            , out DA_RT_Mst_TourGuide objRT_Mst_TourGuide
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TourGuide.Tid;
            objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_TourGuide_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_TourGuide_Delete;
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
                List<DA_Mst_TourGuide> lst_Mst_TourGuide = new List<DA_Mst_TourGuide>();
                #endregion

                #region // Mst_Tour_Create:
                mdsResult = DAMst_TourGuide_Delete(
                    objRQ_Mst_TourGuide.Tid // strTid
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    , objRQ_Mst_TourGuide.WAUserCode // strUserCode
                    , objRQ_Mst_TourGuide.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_TourGuide.Mst_TourGuide.TGNo
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

        #region // POW_NewsType
        private void DAPOW_NewsType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objNewsType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_NewsType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_NewsType t --//[mylock]
					where (1=1)
						and t.NewsType = @objNewsType
					;
				");
            dtDB_POW_NewsType = _cf.db.ExecQuery(
                strSqlExec
                , "@objNewsType", objNewsType
                ).Tables[0];
            dtDB_POW_NewsType.TableName = "POW_NewsType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_NewsType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.NewsType", objNewsType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsType_CheckDB_RegionNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_NewsType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.NewsType", objNewsType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsType_CheckDB_RegionExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_NewsType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.NewsType", objNewsType
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_NewsType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_NewsType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_NewsType_Get(
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
            , string strRt_Cols_POW_NewsType
            )
        {
            #region // Tepnt:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_NewsType_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_NewsType_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_NewsType", strRt_Cols_POW_NewsType
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
                bool bGet_POW_NewsType = (strRt_Cols_POW_NewsType != null && strRt_Cols_POW_NewsType.Length > 0);

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
						---- #tbl_POW_NewsType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pnt.NewsType
						into #tbl_POW_NewsType_Filter_Draft
						from POW_NewsType pnt --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pnt.NewsType asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_NewsType_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_NewsType_Filter:
						select
							t.*
						into #tbl_POW_NewsType_Filter
						from #tbl_POW_NewsType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_NewsType --------:
						zzB_Select_POW_NewsType_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_NewsType_Filter_Draft;
						--drop table #tbl_POW_NewsType_Filter;
					"
                    );
                ////
                string zzB_Select_POW_NewsType_zzE = "-- Nothing.";
                if (bGet_POW_NewsType)
                {
                    #region // bGet_POW_NewsType:
                    zzB_Select_POW_NewsType_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_NewsType:
							select
								t.MyIdxSeq
								, pnt.*
							from #tbl_POW_NewsType_Filter t --//[mylock]
								inner join POW_NewsType pnt --//[mylock]
									on t.NewsType = pnt.NewsType
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
                            , "POW_NewsType" // strTableNameDB
                            , "POW_NewsType." // strPrefixStd
                            , "pnt." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParapntrefix
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
                    , "zzB_Select_POW_NewsType_zzE", zzB_Select_POW_NewsType_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_NewsType)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_NewsType";
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
        #endregion

        #region // POW_NewsType: WAS
        public DataSet WAS_DAPOW_NewsType_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_PowNewsType objRQ_POW_NewsType
            ////
            , out DA_RT_PowNewsType objRT_POW_NewsType
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_NewsType.Tid;
            objRT_POW_NewsType = new DA_RT_PowNewsType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_NewsType_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_NewsType_Get;
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
                List<DA_PowNewsType> lst_POW_NewsType = new List<DA_PowNewsType>();
                bool bGet_POW_NewsType = (objRQ_POW_NewsType.Rt_Cols_POW_NewsType != null && objRQ_POW_NewsType.Rt_Cols_POW_NewsType.Length > 0);
                #endregion

                #region // WS_POW_NewsType_Get:
                mdsResult = DAPOW_NewsType_Get(
                    objRQ_POW_NewsType.Tid // strTid
                    , objRQ_POW_NewsType.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsType.GwPassword // strGwPassword
                    , objRQ_POW_NewsType.WAUserCode // strUserCode
                    , objRQ_POW_NewsType.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_POW_NewsType.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_NewsType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_NewsType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_NewsType.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_POW_NewsType.Rt_Cols_POW_NewsType // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_NewsType.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_NewsType)
                    {
                        DataTable dt_POW_NewsType = mdsResult.Tables["POW_NewsType"].Copy();
                        lst_POW_NewsType = TUtils.DataTableCmUtils.ToListof<DA_PowNewsType>(dt_POW_NewsType);
                        objRT_POW_NewsType.Lst_POW_NewsType = lst_POW_NewsType;
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
        #endregion

        #region // POW_NewsNews
        private void DAPOW_NewsNews_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objNewsNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_NewsNews
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_NewsNews t --//[mylock]
					where (1=1)
						and t.NewsNo = @objNewsNo
					;
				");
            dtDB_POW_NewsNews = _cf.db.ExecQuery(
                strSqlExec
                , "@objNewsNo", objNewsNo
                ).Tables[0];
            dtDB_POW_NewsNews.TableName = "POW_NewsNews";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_NewsNews.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.NewsNo", objNewsNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CheckDB_RegionNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_NewsNews.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.NewsNo", objNewsNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CheckDB_RegionExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_NewsNews.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.NewsNo", objNewsNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_NewsNews.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_NewsNews_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_NewsNews_Get(
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
            , string strRt_Cols_POW_NewsNews
            )
        {
            #region // Tepnn:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_NewsNews_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_NewsNews_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_NewsNews", strRt_Cols_POW_NewsNews
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_NewsNews = (strRt_Cols_POW_NewsNews != null && strRt_Cols_POW_NewsNews.Length > 0);

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
						---- #tbl_POW_NewsNews_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pnn.NewsNo
						into #tbl_POW_NewsNews_Filter_Draft
						from POW_NewsNews pnn --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pnn.NewsNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_NewsNews_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_NewsNews_Filter:
						select
							t.*
						into #tbl_POW_NewsNews_Filter
						from #tbl_POW_NewsNews_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_NewsNews --------:
						zzB_Select_POW_NewsNews_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_NewsNews_Filter_Draft;
						--drop table #tbl_POW_NewsNews_Filter;
					"
                    );
                ////
                string zzB_Select_POW_NewsNews_zzE = "-- Nothing.";
                if (bGet_POW_NewsNews)
                {
                    #region // bGet_POW_NewsNews:
                    zzB_Select_POW_NewsNews_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_NewsNews:
							select
								t.MyIdxSeq
								, pnn.*
							from #tbl_POW_NewsNews_Filter t --//[mylock]
								inner join POW_NewsNews pnn --//[mylock]
									on t.NewsNo = pnn.NewsNo
							order by t.MyIdxSeq asc
							;

                            ---- POW_NewsDetail:
							select
                                pnd.*
							from #tbl_POW_NewsNews_Filter t --//[mylock]
								inner join POW_NewsNews pnn --//[mylock]
									on t.NewsNo = pnn.NewsNo
                                left join POW_NewsDetail pnd --//[mylock]
                                    on pnn.NewsNo = pnd.NewsNo
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
                            , "POW_NewsNews" // strTableNameDB
                            , "POW_NewsNews." // strPrefixStd
                            , "pnn." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParapnnrefix
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
                    , "zzB_Select_POW_NewsNews_zzE", zzB_Select_POW_NewsNews_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_NewsNews)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_NewsNews";
                    dsGetData.Tables[nIdxTable++].TableName = "POW_NewsDetail";
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

        public DataSet DAPOW_NewsNews_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objNewsNo
            , object objTitle
            , object objNewsType
            , object objThemeImage
            , object objContent
            , object objAuthor
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_NewsNews_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_NewsNews_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNewsNo", objNewsNo
                , "objTitle", objTitle
                , "objNewsType", objNewsType
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                , "objAuthor", objAuthor
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_NewsNews_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objNewsNo
                        , objTitle
                        , objNewsType
                        , objThemeImage
                        , objContent
                        , objAuthor
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

        private void DAPOW_NewsNews_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objNewsNo
            , object objTitle
            , object objNewsType
            , object objThemeImage
            , object objContent
            , object objAuthor
            )
        {
            #region // Temp:
            string strFunctionName = "POW_NewsNews_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objNewsNo", objNewsNo
                , "objTitle", objTitle
                , "objNewsType", objNewsType
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                , "objAuthor", objAuthor
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strNewsNo = TUtils.CUtils.StdParam(objNewsNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strNewsType = TUtils.CUtils.StdParam(objNewsType);
            string strThemeImage = string.Format("{0}", objThemeImage).Trim();
            string strContent = string.Format("{0}", objContent).Trim();
            string strAuthor = TUtils.CUtils.StdParam(objAuthor);

            ////
            DataTable dtDB_POW_NewsNews = null;
            {
                ////
                if (strNewsNo == null || strNewsNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strNewsNo", strNewsNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CreateX_InvalidNewsNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_NewsNews_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strNewsNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_NewsNews // dtDB_POW_NewsNews
                    );

                ////
                if (strTitle == null || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CreateX_InvalidTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strThemeImage == null || strThemeImage.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strThemeImage", strThemeImage
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CreateX_InvalidThemeImage
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strContent == null || strContent.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContent", strContent
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_CreateX_InvalidContent
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_NewsNews.NewRow();
                strFN = "NewsNo"; drDB[strFN] = strNewsNo;
                strFN = "Title"; drDB[strFN] = strTitle;
                strFN = "NewsType"; drDB[strFN] = strNewsType;
                strFN = "ThemeImage"; drDB[strFN] = strThemeImage;
                strFN = "Content"; drDB[strFN] = strContent;
                strFN = "Author"; drDB[strFN] = strAuthor;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_NewsNews.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_NewsNews" // strTableName
                    , dtDB_POW_NewsNews // dtData
                                        //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_NewsNews_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objNewsNo
            , object objTitle
            , object objNewsType
            , object objThemeImage
            , object objContent
            //, object objAuthor
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_NewsNews_Update";
            string strErrorCodeDefault = TError.ErrDA.POW_NewsNews_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNewsNo", objNewsNo
                , "objTitle", objTitle
                , "objNewsType", objNewsType
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                //, "objAuthor", objAuthor
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_NewsNews_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objNewsNo
                        , objTitle
                        , objNewsType
                        , objThemeImage
                        , objContent
                        //, objAuthor
                        , objFlagActive
                        ////
                        , objFt_Cols_Upd
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

        private void DAPOW_NewsNews_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objNewsNo
            , object objTitle
            , object objNewsType
            , object objThemeImage
            , object objContent
            //, object objAuthor
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_NewsNews_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objNewsNo", objNewsNo
                , "objTitle", objTitle
                , "objNewsType", objNewsType
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                //, "objAuthor", objAuthor
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
            string strNewsNo = TUtils.CUtils.StdParam(objNewsNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strNewsType = TUtils.CUtils.StdParam(objNewsType);
            string strThemeImage = string.Format("{0}", objThemeImage).Trim();
            string strContent = string.Format("{0}", objContent).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            //bool bUpd_NewsNo = strFt_Cols_Upd.Contains("POW_NewsNews.NewsNo".ToUpper());
            bool bUpd_Title = strFt_Cols_Upd.Contains("POW_NewsNews.Title".ToUpper());
            bool bUpd_NewsType = strFt_Cols_Upd.Contains("POW_NewsNews.NewsType".ToUpper());
            bool bUpd_ThemeImage = strFt_Cols_Upd.Contains("POW_NewsNews.ThemeImage".ToUpper());
            bool bUpd_Content = strFt_Cols_Upd.Contains("POW_NewsNews.Content".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("POW_NewsNews.FlagActive".ToUpper());

            ////
            DataTable dtDB_POW_NewsNews = null;
            {
                ////
                DAPOW_NewsNews_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strNewsNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_NewsNews // dtDB_POW_NewsNews
                    );
                ////
                if (string.IsNullOrEmpty(strThemeImage) || strThemeImage.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strThemeImage", strThemeImage
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_Update_InvalidThemeImage
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strTitle) || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_Update_InvalidTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strContent) || strContent.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContent", strContent
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_NewsNews_Update_InvalidContent
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
                DataRow drDB = dtDB_POW_NewsNews.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_Title) { strFN = "Title"; drDB[strFN] = strTitle; alColumnEffective.Add(strFN); }
                if (bUpd_NewsType) { strFN = "NewsType"; drDB[strFN] = strNewsType; alColumnEffective.Add(strFN); }
                if (bUpd_ThemeImage) { strFN = "ThemeImage"; drDB[strFN] = strThemeImage; alColumnEffective.Add(strFN); }
                if (bUpd_Content) { strFN = "Content"; drDB[strFN] = strContent; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                // Save:
                _cf.db.SaveData(
                    "POW_NewsNews"
                    , dtDB_POW_NewsNews
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAPOW_NewsNews_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objNewsNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_NewsNews_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_NewsNews_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objNewsNo", objNewsNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_NewsNews_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objNewsNo // objTourCode
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

        private void DAPOW_NewsNews_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objNewsNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_NewsNews_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objNewsNo", objNewsNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strNewsNo = TUtils.CUtils.StdParam(objNewsNo);

            ////
            DataTable dtDB_POW_NewsNews = null;
            {
                ////
                DAPOW_NewsNews_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strNewsNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_NewsNews // dtDB_POW_NewsNews
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_NewsNews.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_NewsNews"
                    , dtDB_POW_NewsNews
                    );
            }
            #endregion
        }
        #endregion

        #region // POW_NewsNews: WAS
        public DataSet WAS_DAPOW_NewsNews_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_NewsNews objRQ_POW_NewsNews
            ////
            , out DA_RT_POW_NewsNews objRT_POW_NewsNews
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_NewsNews.Tid;
            objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_NewsNews_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_NewsNews_Get;
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
                List<DA_POW_NewsNews> lst_POW_NewsNews = new List<DA_POW_NewsNews>();
                List<DA_POW_NewsDetail> lst_POW_NewsDetail = new List<DA_POW_NewsDetail>();
                bool bGet_POW_NewsNews = (objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews != null && objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews.Length > 0);
                #endregion

                #region // WS_POW_NewsNews_Get:
                mdsResult = DAPOW_NewsNews_Get(
                    objRQ_POW_NewsNews.Tid // strTid
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    , objRQ_POW_NewsNews.WAUserCode // strUserCode
                    , objRQ_POW_NewsNews.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_POW_NewsNews.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_NewsNews.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_NewsNews.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_NewsNews.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_NewsNews.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_NewsNews)
                    {
                        DataTable dt_POW_NewsNews = mdsResult.Tables["POW_NewsNews"].Copy();
                        lst_POW_NewsNews = TUtils.DataTableCmUtils.ToListof<DA_POW_NewsNews>(dt_POW_NewsNews);
                        objRT_POW_NewsNews.Lst_POW_NewsNews = lst_POW_NewsNews;

                        ////
                        DataTable dt_POW_NewsDetail = mdsResult.Tables["POW_NewsDetail"].Copy();
                        lst_POW_NewsDetail = TUtils.DataTableCmUtils.ToListof<DA_POW_NewsDetail>(dt_POW_NewsDetail);
                        objRT_POW_NewsNews.Lst_POW_NewsDetail = lst_POW_NewsDetail;
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

        public DataSet WAS_DAPOW_NewsNews_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_NewsNews objRQ_POW_NewsNews
            ////
            , out DA_RT_POW_NewsNews objRT_POW_NewsNews
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_NewsNews.Tid;
            objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_NewsNews_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_NewsNews_Create;
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
                List<DA_POW_NewsNews> lst_POW_NewsNews = new List<DA_POW_NewsNews>();
                bool bGet_POW_NewsNews = (objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews != null && objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews.Length > 0);
                #endregion

                #region // WS_POW_NewsNews_Get:
                mdsResult = DAPOW_NewsNews_Create(
                    objRQ_POW_NewsNews.Tid // strTid
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    , objRQ_POW_NewsNews.WAUserCode // strUserCode
                    , objRQ_POW_NewsNews.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_POW_NewsNews.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_NewsNews.POW_NewsNews.NewsNo
                    , objRQ_POW_NewsNews.POW_NewsNews.Title
                    , objRQ_POW_NewsNews.POW_NewsNews.NewsType
                    , objRQ_POW_NewsNews.POW_NewsNews.ThemeImage
                    , objRQ_POW_NewsNews.POW_NewsNews.Content
                    , objRQ_POW_NewsNews.POW_NewsNews.Author
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_NewsNews.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_NewsNews)
                    {
                        DataTable dt_POW_NewsNews = mdsResult.Tables["POW_NewsNews"].Copy();
                        lst_POW_NewsNews = TUtils.DataTableCmUtils.ToListof<DA_POW_NewsNews>(dt_POW_NewsNews);
                        objRT_POW_NewsNews.Lst_POW_NewsNews = lst_POW_NewsNews;
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

        public DataSet WAS_DAPOW_NewsNews_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_NewsNews objRQ_POW_NewsNews
            ////
            , out DA_RT_POW_NewsNews objRT_POW_NewsNews
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_NewsNews.Tid;
            objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_NewsNews_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_NewsNews_Update;
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
                List<DA_POW_NewsNews> lst_POW_NewsNews = new List<DA_POW_NewsNews>();
                bool bGet_POW_NewsNews = (objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews != null && objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews.Length > 0);
                #endregion

                #region // WS_POW_NewsNews_Get:
                mdsResult = DAPOW_NewsNews_Update(
                    objRQ_POW_NewsNews.Tid // strTid
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    , objRQ_POW_NewsNews.WAUserCode // strUserCode
                    , objRQ_POW_NewsNews.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_POW_NewsNews.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_NewsNews.POW_NewsNews.NewsNo
                    , objRQ_POW_NewsNews.POW_NewsNews.Title
                    , objRQ_POW_NewsNews.POW_NewsNews.NewsType
                    , objRQ_POW_NewsNews.POW_NewsNews.ThemeImage
                    , objRQ_POW_NewsNews.POW_NewsNews.Content
                    //, objRQ_POW_NewsNews.POW_NewsNews.Author
                    , objRQ_POW_NewsNews.POW_NewsNews.FlagActive
                    ////
                    , objRQ_POW_NewsNews.Ft_Cols_Upd
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_NewsNews.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_NewsNews)
                    {
                        DataTable dt_POW_NewsNews = mdsResult.Tables["POW_NewsNews"].Copy();
                        lst_POW_NewsNews = TUtils.DataTableCmUtils.ToListof<DA_POW_NewsNews>(dt_POW_NewsNews);
                        objRT_POW_NewsNews.Lst_POW_NewsNews = lst_POW_NewsNews;
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

        public DataSet WAS_DAPOW_NewsNews_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_NewsNews objRQ_POW_NewsNews
            ////
            , out DA_RT_POW_NewsNews objRT_POW_NewsNews
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_NewsNews.Tid;
            objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_NewsNews_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_NewsNews_Delete;
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
                List<DA_POW_NewsNews> lst_POW_NewsNews = new List<DA_POW_NewsNews>();
                bool bGet_POW_NewsNews = (objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews != null && objRQ_POW_NewsNews.Rt_Cols_POW_NewsNews.Length > 0);
                #endregion

                #region // WS_POW_NewsNews_Get:
                mdsResult = DAPOW_NewsNews_Delete(
                    objRQ_POW_NewsNews.Tid // strTid
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    , objRQ_POW_NewsNews.WAUserCode // strUserCode
                    , objRQ_POW_NewsNews.WAUserPassword // strUserPassword
                                                        //, TUtils.CUtils.StdFlag(objRQ_POW_NewsNews.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_NewsNews.POW_NewsNews.NewsNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_NewsNews.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_NewsNews)
                    {
                        DataTable dt_POW_NewsNews = mdsResult.Tables["POW_NewsNews"].Copy();
                        lst_POW_NewsNews = TUtils.DataTableCmUtils.ToListof<DA_POW_NewsNews>(dt_POW_NewsNews);
                        objRT_POW_NewsNews.Lst_POW_NewsNews = lst_POW_NewsNews;
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
        #endregion

        #region // POW_Recruitment
        private void DAPOW_Recruitment_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objRecNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_Recruitment
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_Recruitment t --//[mylock]
					where (1=1)
						and t.RecNo = @objRecNo
					;
				");
            dtDB_POW_Recruitment = _cf.db.ExecQuery(
                strSqlExec
                , "@objRecNo", objRecNo
                ).Tables[0];
            dtDB_POW_Recruitment.TableName = "POW_Recruitment";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_Recruitment.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.RecNo", objRecNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CheckDB_RecNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_Recruitment.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.RecNo", objRecNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CheckDB_RecNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_Recruitment.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.RecNo", objRecNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_Recruitment.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_Recruitment_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_Recruitment_Get(
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
            , string strRt_Cols_POW_Recruitment
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_Recruitment_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_Recruitment_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_Recruitment", strRt_Cols_POW_Recruitment
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_Recruitment = (strRt_Cols_POW_Recruitment != null && strRt_Cols_POW_Recruitment.Length > 0);

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
						---- #tbl_POW_Recruitment_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pr.RecNo
						into #tbl_POW_Recruitment_Filter_Draft
						from POW_Recruitment pr --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pr.RecNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_Recruitment_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_Recruitment_Filter:
						select
							t.*
						into #tbl_POW_Recruitment_Filter
						from #tbl_POW_Recruitment_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_Recruitment --------:
						zzB_Select_POW_Recruitment_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_Recruitment_Filter_Draft;
						--drop table #tbl_POW_Recruitment_Filter;
					"
                    );
                ////
                string zzB_Select_POW_Recruitment_zzE = "-- Nothing.";
                if (bGet_POW_Recruitment)
                {
                    #region // bGet_POW_Recruitment:
                    zzB_Select_POW_Recruitment_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_Recruitment:
							select
								t.MyIdxSeq
								, pr.*
							from #tbl_POW_Recruitment_Filter t --//[mylock]
								inner join POW_Recruitment pr --//[mylock]
									on t.RecNo = pr.RecNo
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
                            , "POW_Recruitment" // strTableNameDB
                            , "POW_Recruitment." // strPrefixStd
                            , "pr." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprrefix
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
                    , "zzB_Select_POW_Recruitment_zzE", zzB_Select_POW_Recruitment_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_Recruitment)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_Recruitment";
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

        public DataSet DAPOW_Recruitment_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objRecNo
            , object objTitle
            , object objThemeImage
            , object objContent
            , object objAuthor
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Recruitment_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_Recruitment_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objRecNo", objRecNo
                , "objTitle", objTitle
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                , "objAuthor", objAuthor
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Recruitment_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objRecNo
                        , objTitle
                        , objThemeImage
                        , objContent
                        , objAuthor
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

        private void DAPOW_Recruitment_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objRecNo
            , object objTitle
            , object objThemeImage
            , object objContent
            , object objAuthor
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Recruitment_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objRecNo", objRecNo
                , "objTitle", objTitle
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                , "objAuthor", objAuthor
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strRecNo = TUtils.CUtils.StdParam(objRecNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strThemeImage = string.Format("{0}", objThemeImage).Trim();
            string strContent = string.Format("{0}", objContent).Trim();
            string strAuthor = TUtils.CUtils.StdParam(objAuthor);

            ////
            DataTable dtDB_POW_Recruitment = null;
            {
                ////
                if (strRecNo == null || strRecNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strRecNo", strRecNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CreateX_InvalidRecNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_Recruitment_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strRecNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_Recruitment // dtDB_POW_Recruitment
                    );

                ////
                if (strTitle == null || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CreateX_InvalidTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strThemeImage == null || strThemeImage.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strThemeImage", strThemeImage
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CreateX_InvalidThemeImage
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strContent == null || strContent.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContent", strContent
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_CreateX_InvalidContent
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_Recruitment.NewRow();
                strFN = "RecNo"; drDB[strFN] = strRecNo;
                strFN = "Title"; drDB[strFN] = strTitle;
                strFN = "ThemeImage"; drDB[strFN] = strThemeImage;
                strFN = "Content"; drDB[strFN] = strContent;
                strFN = "Author"; drDB[strFN] = strAuthor;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_Recruitment.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_Recruitment" // strTableName
                    , dtDB_POW_Recruitment // dtData
                                           //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_Recruitment_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objRecNo
            , object objTitle
            , object objThemeImage
            , object objContent
            //, object objAuthor
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Recruitment_Update";
            string strErrorCodeDefault = TError.ErrDA.POW_Recruitment_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objRecNo", objRecNo
                , "objTitle", objTitle
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                //, "objAuthor", objAuthor
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // POW_Recruitment_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Recruitment_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objRecNo
                        , objTitle
                        , objThemeImage
                        , objContent
                        //, objAuthor
                        , objFlagActive
                        ////
                        , objFt_Cols_Upd
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

        private void DAPOW_Recruitment_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objRecNo
            , object objTitle
            , object objThemeImage
            , object objContent
            //, object objAuthor
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Recruitment_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objRecNo", objRecNo
                , "objTitle", objTitle
                , "objThemeImage", objThemeImage
                , "objContent", objContent
                //, "objAuthor", objAuthor
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
            string strRecNo = TUtils.CUtils.StdParam(objRecNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strThemeImage = string.Format("{0}", objThemeImage).Trim();
            string strContent = string.Format("{0}", objContent).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            //bool bUpd_RecNo = strFt_Cols_Upd.Contains("POW_Recruitment.RecNo".ToUpper());
            bool bUpd_Title = strFt_Cols_Upd.Contains("POW_Recruitment.Title".ToUpper());
            bool bUpd_ThemeImage = strFt_Cols_Upd.Contains("POW_Recruitment.ThemeImage".ToUpper());
            bool bUpd_Content = strFt_Cols_Upd.Contains("POW_Recruitment.Content".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("POW_Recruitment.FlagActive".ToUpper());

            ////
            DataTable dtDB_POW_Recruitment = null;
            {
                ////
                DAPOW_Recruitment_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strRecNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_Recruitment // dtDB_POW_Recruitment
                    );
                ////
                if (string.IsNullOrEmpty(strThemeImage) || strThemeImage.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strThemeImage", strThemeImage
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_Update_InvalidThemeImage
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strTitle) || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_Update_InvalidTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strContent) || strContent.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContent", strContent
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Recruitment_Update_InvalidContent
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
                DataRow drDB = dtDB_POW_Recruitment.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_Title) { strFN = "Title"; drDB[strFN] = strTitle; alColumnEffective.Add(strFN); }
                if (bUpd_ThemeImage) { strFN = "ThemeImage"; drDB[strFN] = strThemeImage; alColumnEffective.Add(strFN); }
                if (bUpd_Content) { strFN = "Content"; drDB[strFN] = strContent; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                // Save:
                _cf.db.SaveData(
                    "POW_Recruitment"
                    , dtDB_POW_Recruitment
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAPOW_Recruitment_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objRecNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Recruitment_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_Recruitment_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objRecNo", objRecNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Recruitment_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objRecNo // objTourCode
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

        private void DAPOW_Recruitment_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objRecNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Recruitment_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRecNo", objRecNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strRecNo = TUtils.CUtils.StdParam(objRecNo);

            ////
            DataTable dtDB_POW_Recruitment = null;
            {
                ////
                DAPOW_Recruitment_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strRecNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_Recruitment // dtDB_POW_Recruitment
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_Recruitment.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_Recruitment"
                    , dtDB_POW_Recruitment
                    );
            }
            #endregion
        }
        #endregion

        #region // POW_Recruitment: WAS
        public DataSet WAS_DAPOW_Recruitment_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Recruitment objRQ_POW_Recruitment
            ////
            , out DA_RT_POW_Recruitment objRT_POW_Recruitment
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Recruitment.Tid;
            objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Recruitment_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Recruitment_Get;
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
                List<DA_POW_Recruitment> lst_POW_Recruitment = new List<DA_POW_Recruitment>();
                bool bGet_POW_Recruitment = (objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment != null && objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment.Length > 0);
                #endregion

                #region // WS_POW_Recruitment_Get:
                mdsResult = DAPOW_Recruitment_Get(
                    objRQ_POW_Recruitment.Tid // strTid
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    , objRQ_POW_Recruitment.WAUserCode // strUserCode
                    , objRQ_POW_Recruitment.WAUserPassword // strUserPassword
                                                           //, TUtils.CUtils.StdFlag(objRQ_POW_Recruitment.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Recruitment.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_Recruitment.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_Recruitment.Ft_WhereClause // strFt_WhereClause
                                                           //// Return:
                    , objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Recruitment.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Recruitment)
                    {
                        DataTable dt_POW_Recruitment = mdsResult.Tables["POW_Recruitment"].Copy();
                        lst_POW_Recruitment = TUtils.DataTableCmUtils.ToListof<DA_POW_Recruitment>(dt_POW_Recruitment);
                        objRT_POW_Recruitment.Lst_POW_Recruitment = lst_POW_Recruitment;
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

        public DataSet WAS_DAPOW_Recruitment_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Recruitment objRQ_POW_Recruitment
            ////
            , out DA_RT_POW_Recruitment objRT_POW_Recruitment
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Recruitment.Tid;
            objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Recruitment_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Recruitment_Create;
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
                List<DA_POW_Recruitment> lst_POW_Recruitment = new List<DA_POW_Recruitment>();
                bool bGet_POW_Recruitment = (objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment != null && objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment.Length > 0);
                #endregion

                #region // WS_POW_Recruitment_Get:
                mdsResult = DAPOW_Recruitment_Create(
                    objRQ_POW_Recruitment.Tid // strTid
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    , objRQ_POW_Recruitment.WAUserCode // strUserCode
                    , objRQ_POW_Recruitment.WAUserPassword // strUserPassword
                                                           //, TUtils.CUtils.StdFlag(objRQ_POW_Recruitment.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Recruitment.POW_Recruitment.RecNo
                    , objRQ_POW_Recruitment.POW_Recruitment.Title
                    , objRQ_POW_Recruitment.POW_Recruitment.ThemeImage
                    , objRQ_POW_Recruitment.POW_Recruitment.Content
                    , objRQ_POW_Recruitment.POW_Recruitment.Author
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Recruitment.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Recruitment)
                    {
                        DataTable dt_POW_Recruitment = mdsResult.Tables["POW_Recruitment"].Copy();
                        lst_POW_Recruitment = TUtils.DataTableCmUtils.ToListof<DA_POW_Recruitment>(dt_POW_Recruitment);
                        objRT_POW_Recruitment.Lst_POW_Recruitment = lst_POW_Recruitment;
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

        public DataSet WAS_DAPOW_Recruitment_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Recruitment objRQ_POW_Recruitment
            ////
            , out DA_RT_POW_Recruitment objRT_POW_Recruitment
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Recruitment.Tid;
            objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Recruitment_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Recruitment_Update;
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
                List<DA_POW_Recruitment> lst_POW_Recruitment = new List<DA_POW_Recruitment>();
                bool bGet_POW_Recruitment = (objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment != null && objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment.Length > 0);
                #endregion

                #region // WS_POW_Recruitment_Get:
                mdsResult = DAPOW_Recruitment_Update(
                    objRQ_POW_Recruitment.Tid // strTid
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    , objRQ_POW_Recruitment.WAUserCode // strUserCode
                    , objRQ_POW_Recruitment.WAUserPassword // strUserPassword
                                                           //, TUtils.CUtils.StdFlag(objRQ_POW_Recruitment.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Recruitment.POW_Recruitment.RecNo
                    , objRQ_POW_Recruitment.POW_Recruitment.Title
                    , objRQ_POW_Recruitment.POW_Recruitment.ThemeImage
                    , objRQ_POW_Recruitment.POW_Recruitment.Content
                    //, objRQ_POW_Recruitment.POW_Recruitment.Author
                    , objRQ_POW_Recruitment.POW_Recruitment.FlagActive
                    ////
                    , objRQ_POW_Recruitment.Ft_Cols_Upd
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Recruitment.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Recruitment)
                    {
                        DataTable dt_POW_Recruitment = mdsResult.Tables["POW_Recruitment"].Copy();
                        lst_POW_Recruitment = TUtils.DataTableCmUtils.ToListof<DA_POW_Recruitment>(dt_POW_Recruitment);
                        objRT_POW_Recruitment.Lst_POW_Recruitment = lst_POW_Recruitment;
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

        public DataSet WAS_DAPOW_Recruitment_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Recruitment objRQ_POW_Recruitment
            ////
            , out DA_RT_POW_Recruitment objRT_POW_Recruitment
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Recruitment.Tid;
            objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Recruitment_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Recruitment_Delete;
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
                List<DA_POW_Recruitment> lst_POW_Recruitment = new List<DA_POW_Recruitment>();
                bool bGet_POW_Recruitment = (objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment != null && objRQ_POW_Recruitment.Rt_Cols_POW_Recruitment.Length > 0);
                #endregion

                #region // WS_POW_Recruitment_Get:
                mdsResult = DAPOW_Recruitment_Delete(
                    objRQ_POW_Recruitment.Tid // strTid
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    , objRQ_POW_Recruitment.WAUserCode // strUserCode
                    , objRQ_POW_Recruitment.WAUserPassword // strUserPassword
                                                           //, TUtils.CUtils.StdFlag(objRQ_POW_Recruitment.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Recruitment.POW_Recruitment.RecNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Recruitment.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Recruitment)
                    {
                        DataTable dt_POW_Recruitment = mdsResult.Tables["POW_Recruitment"].Copy();
                        lst_POW_Recruitment = TUtils.DataTableCmUtils.ToListof<DA_POW_Recruitment>(dt_POW_Recruitment);
                        objRT_POW_Recruitment.Lst_POW_Recruitment = lst_POW_Recruitment;
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
        #endregion

        #region // POW_AboutUs
        private void DAPOW_AboutUs_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objAUNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_AboutUs
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_AboutUs t --//[mylock]
					where (1=1)
						and t.AUNo = @objAUNo
					;
				");
            dtDB_POW_AboutUs = _cf.db.ExecQuery(
                strSqlExec
                , "@objAUNo", objAUNo
                ).Tables[0];
            dtDB_POW_AboutUs.TableName = "POW_AboutUs";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_AboutUs.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.AUNo", objAUNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_CheckDB_AUNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_AboutUs.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.AUNo", objAUNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_CheckDB_AUNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_AboutUs.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.AUNo", objAUNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_AboutUs.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_AboutUs_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_AboutUs_Get(
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
            , string strRt_Cols_POW_AboutUs
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_AboutUs_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_AboutUs_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_AboutUs", strRt_Cols_POW_AboutUs
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_AboutUs = (strRt_Cols_POW_AboutUs != null && strRt_Cols_POW_AboutUs.Length > 0);

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
						---- #tbl_POW_AboutUs_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pau.AUNo
						into #tbl_POW_AboutUs_Filter_Draft
						from POW_AboutUs pau --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pau.AUNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_AboutUs_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_AboutUs_Filter:
						select
							t.*
						into #tbl_POW_AboutUs_Filter
						from #tbl_POW_AboutUs_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_AboutUs --------:
						zzB_Select_POW_AboutUs_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_AboutUs_Filter_Draft;
						--drop table #tbl_POW_AboutUs_Filter;
					"
                    );
                ////
                string zzB_Select_POW_AboutUs_zzE = "-- Nothing.";
                if (bGet_POW_AboutUs)
                {
                    #region // bGet_POW_AboutUs:
                    zzB_Select_POW_AboutUs_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_AboutUs:
							select
								t.MyIdxSeq
								, pau.*
							from #tbl_POW_AboutUs_Filter t --//[mylock]
								inner join POW_AboutUs pau --//[mylock]
									on t.AUNo = pau.AUNo
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
                            , "POW_AboutUs" // strTableNameDB
                            , "POW_AboutUs." // strPrefixStd
                            , "pau." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprefix
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
                    , "zzB_Select_POW_AboutUs_zzE", zzB_Select_POW_AboutUs_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_AboutUs)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_AboutUs";
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

        public DataSet DAPOW_AboutUs_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objAUNo
            , object objTitle
            , object objVideoURL
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_AboutUs_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_AboutUs_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objAUNo", objAUNo
                , "objTitle", objTitle
                , "objVideoURL", objVideoURL
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_AboutUs_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objAUNo
                        , objTitle
                        , objVideoURL
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

        private void DAPOW_AboutUs_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objAUNo
            , object objTitle
            , object objVideoURL
            )
        {
            #region // Temp:
            string strFunctionName = "POW_AboutUs_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objAUNo", objAUNo
                , "objTitle", objTitle
                , "objVideoURL", objVideoURL
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strAUNo = TUtils.CUtils.StdParam(objAUNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strVideoURL = string.Format("{0}", objVideoURL).Trim();

            ////
            DataTable dtDB_POW_AboutUs = null;
            {
                ////
                if (strAUNo == null || strAUNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strAUNo", strAUNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_CreateX_InvalidAUNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_AboutUs_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strAUNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_AboutUs // dtDB_POW_AboutUs
                    );

                ////
                if (strTitle == null || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_CreateX_InvalidTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strVideoURL == null || strVideoURL.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strVideoURL", strVideoURL
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_CreateX_InvalidVideoURL
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_AboutUs.NewRow();
                strFN = "AUNo"; drDB[strFN] = strAUNo;
                strFN = "Title"; drDB[strFN] = strTitle;
                strFN = "VideoURL"; drDB[strFN] = strVideoURL;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_AboutUs.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_AboutUs" // strTableName
                    , dtDB_POW_AboutUs // dtData
                                       //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_AboutUs_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objAUNo
            , object objTitle
            , object objVideoURL
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_AboutUs_Update";
            string strErrorCodeDefault = TError.ErrDA.POW_AboutUs_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objAUNo", objAUNo
                , "objTitle", objTitle
                , "objVideoURL", objVideoURL
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // POW_AboutUs_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_AboutUs_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objAUNo
                        , objTitle
                        , objVideoURL
                        , objFlagActive
                        ////
                        , objFt_Cols_Upd
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

        private void DAPOW_AboutUs_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objAUNo
            , object objTitle
            , object objVideoURL
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_AboutUs_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objAUNo", objAUNo
                , "objTitle", objTitle
                , "objVideoURL", objVideoURL
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
            string strAUNo = TUtils.CUtils.StdParam(objAUNo);
            string strTitle = string.Format("{0}", objTitle).Trim();
            string strVideoURL = string.Format("{0}", objVideoURL).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            //bool bUpd_AUNo = strFt_Cols_Upd.Contains("POW_AboutUs.AUNo".ToUpper());
            bool bUpd_Title = strFt_Cols_Upd.Contains("POW_AboutUs.Title".ToUpper());
            bool bUpd_VideoURL = strFt_Cols_Upd.Contains("POW_AboutUs.VideoURL".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("POW_AboutUs.FlagActive".ToUpper());

            ////
            DataTable dtDB_POW_AboutUs = null;
            {
                ////
                DAPOW_AboutUs_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strAUNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_AboutUs // dtDB_POW_AboutUs
                    );
                ////
                if (string.IsNullOrEmpty(strVideoURL) || strVideoURL.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strVideoURL", strVideoURL
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_Update_InvalidVideoURL
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strTitle) || strTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strTitle", strTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_AboutUs_Update_InvalidTitle
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
                DataRow drDB = dtDB_POW_AboutUs.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_Title) { strFN = "Title"; drDB[strFN] = strTitle; alColumnEffective.Add(strFN); }
                if (bUpd_VideoURL) { strFN = "VideoURL"; drDB[strFN] = strVideoURL; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                // Save:
                _cf.db.SaveData(
                    "POW_AboutUs"
                    , dtDB_POW_AboutUs
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAPOW_AboutUs_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objAUNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_AboutUs_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_AboutUs_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objAUNo", objAUNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_AboutUs_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objAUNo // objTourCode
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

        private void DAPOW_AboutUs_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objAUNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_AboutUs_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objAUNo", objAUNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strAUNo = TUtils.CUtils.StdParam(objAUNo);

            ////
            DataTable dtDB_POW_AboutUs = null;
            {
                ////
                DAPOW_AboutUs_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strAUNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_AboutUs // dtDB_POW_AboutUs
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_AboutUs.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_AboutUs"
                    , dtDB_POW_AboutUs
                    );
            }
            #endregion
        }
        #endregion

        #region  // POW_AboutUs: WAS
        public DataSet WAS_DAPOW_AboutUs_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_AboutUs objRQ_POW_AboutUs
            ////
            , out DA_RT_POW_AboutUs objRT_POW_AboutUs
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_AboutUs.Tid;
            objRT_POW_AboutUs = new DA_RT_POW_AboutUs();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_AboutUs_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_AboutUs_Get;
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
                List<DA_POW_AboutUs> lst_POW_AboutUs = new List<DA_POW_AboutUs>();
                bool bGet_POW_AboutUs = (objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs != null && objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs.Length > 0);
                #endregion

                #region // WS_POW_AboutUs_Get:
                mdsResult = DAPOW_AboutUs_Get(
                    objRQ_POW_AboutUs.Tid // strTid
                    , objRQ_POW_AboutUs.GwUserCode // strGwUserCode
                    , objRQ_POW_AboutUs.GwPassword // strGwPassword
                    , objRQ_POW_AboutUs.WAUserCode // strUserCode
                    , objRQ_POW_AboutUs.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_AboutUs.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_AboutUs.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_AboutUs.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_AboutUs.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_AboutUs.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_AboutUs)
                    {
                        DataTable dt_POW_AboutUs = mdsResult.Tables["POW_AboutUs"].Copy();
                        lst_POW_AboutUs = TUtils.DataTableCmUtils.ToListof<DA_POW_AboutUs>(dt_POW_AboutUs);
                        objRT_POW_AboutUs.Lst_POW_AboutUs = lst_POW_AboutUs;
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

        public DataSet WAS_DAPOW_AboutUs_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_AboutUs objRQ_POW_AboutUs
            ////
            , out DA_RT_POW_AboutUs objRT_POW_AboutUs
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_AboutUs.Tid;
            objRT_POW_AboutUs = new DA_RT_POW_AboutUs();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_AboutUs_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_AboutUs_Create;
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
                List<DA_POW_AboutUs> lst_POW_AboutUs = new List<DA_POW_AboutUs>();
                bool bGet_POW_AboutUs = (objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs != null && objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs.Length > 0);
                #endregion

                #region // WS_POW_AboutUs_Get:
                mdsResult = DAPOW_AboutUs_Create(
                    objRQ_POW_AboutUs.Tid // strTid
                    , objRQ_POW_AboutUs.GwUserCode // strGwUserCode
                    , objRQ_POW_AboutUs.GwPassword // strGwPassword
                    , objRQ_POW_AboutUs.WAUserCode // strUserCode
                    , objRQ_POW_AboutUs.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_AboutUs.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_AboutUs.POW_AboutUs.AUNo
                    , objRQ_POW_AboutUs.POW_AboutUs.Title
                    , objRQ_POW_AboutUs.POW_AboutUs.VideoURL
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_AboutUs.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_AboutUs)
                    {
                        DataTable dt_POW_AboutUs = mdsResult.Tables["POW_AboutUs"].Copy();
                        lst_POW_AboutUs = TUtils.DataTableCmUtils.ToListof<DA_POW_AboutUs>(dt_POW_AboutUs);
                        objRT_POW_AboutUs.Lst_POW_AboutUs = lst_POW_AboutUs;
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

        public DataSet WAS_DAPOW_AboutUs_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_AboutUs objRQ_POW_AboutUs
            ////
            , out DA_RT_POW_AboutUs objRT_POW_AboutUs
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_AboutUs.Tid;
            objRT_POW_AboutUs = new DA_RT_POW_AboutUs();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_AboutUs_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_AboutUs_Update;
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
                List<DA_POW_AboutUs> lst_POW_AboutUs = new List<DA_POW_AboutUs>();
                bool bGet_POW_AboutUs = (objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs != null && objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs.Length > 0);
                #endregion

                #region // WS_POW_AboutUs_Get:
                mdsResult = DAPOW_AboutUs_Update(
                    objRQ_POW_AboutUs.Tid // strTid
                    , objRQ_POW_AboutUs.GwUserCode // strGwUserCode
                    , objRQ_POW_AboutUs.GwPassword // strGwPassword
                    , objRQ_POW_AboutUs.WAUserCode // strUserCode
                    , objRQ_POW_AboutUs.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_AboutUs.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_AboutUs.POW_AboutUs.AUNo
                    , objRQ_POW_AboutUs.POW_AboutUs.Title
                    , objRQ_POW_AboutUs.POW_AboutUs.VideoURL
                    //, objRQ_POW_AboutUs.POW_AboutUs.Author
                    , objRQ_POW_AboutUs.POW_AboutUs.FlagActive
                    ////
                    , objRQ_POW_AboutUs.Ft_Cols_Upd
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_AboutUs.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_AboutUs)
                    {
                        DataTable dt_POW_AboutUs = mdsResult.Tables["POW_AboutUs"].Copy();
                        lst_POW_AboutUs = TUtils.DataTableCmUtils.ToListof<DA_POW_AboutUs>(dt_POW_AboutUs);
                        objRT_POW_AboutUs.Lst_POW_AboutUs = lst_POW_AboutUs;
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

        public DataSet WAS_DAPOW_AboutUs_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_AboutUs objRQ_POW_AboutUs
            ////
            , out DA_RT_POW_AboutUs objRT_POW_AboutUs
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_AboutUs.Tid;
            objRT_POW_AboutUs = new DA_RT_POW_AboutUs();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_AboutUs_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_AboutUs_Delete;
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
                List<DA_POW_AboutUs> lst_POW_AboutUs = new List<DA_POW_AboutUs>();
                bool bGet_POW_AboutUs = (objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs != null && objRQ_POW_AboutUs.Rt_Cols_POW_AboutUs.Length > 0);
                #endregion

                #region // WS_POW_AboutUs_Get:
                mdsResult = DAPOW_AboutUs_Delete(
                    objRQ_POW_AboutUs.Tid // strTid
                    , objRQ_POW_AboutUs.GwUserCode // strGwUserCode
                    , objRQ_POW_AboutUs.GwPassword // strGwPassword
                    , objRQ_POW_AboutUs.WAUserCode // strUserCode
                    , objRQ_POW_AboutUs.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_AboutUs.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_AboutUs.POW_AboutUs.AUNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_AboutUs.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_AboutUs)
                    {
                        DataTable dt_POW_AboutUs = mdsResult.Tables["POW_AboutUs"].Copy();
                        lst_POW_AboutUs = TUtils.DataTableCmUtils.ToListof<DA_POW_AboutUs>(dt_POW_AboutUs);
                        objRT_POW_AboutUs.Lst_POW_AboutUs = lst_POW_AboutUs;
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
        #endregion

        #region // POW_Contact
        private void DAPOW_Contact_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objContactNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_Contact
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_Contact t --//[mylock]
					where (1=1)
						and t.ContactNo = @objContactNo
					;
				");
            dtDB_POW_Contact = _cf.db.ExecQuery(
                strSqlExec
                , "@objContactNo", objContactNo
                ).Tables[0];
            dtDB_POW_Contact.TableName = "POW_Contact";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_Contact.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ContactNo", objContactNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_CheckDB_ContactNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_Contact.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ContactNo", objContactNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_CheckDB_ContactNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_Contact.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ContactNo", objContactNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_Contact.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_Contact_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_Contact_Get(
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
            , string strRt_Cols_POW_Contact
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_Contact_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_Contact_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_Contact", strRt_Cols_POW_Contact
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_Contact = (strRt_Cols_POW_Contact != null && strRt_Cols_POW_Contact.Length > 0);

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
						---- #tbl_POW_Contact_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pc.ContactNo
						into #tbl_POW_Contact_Filter_Draft
						from POW_Contact pc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pc.ContactNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_Contact_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_Contact_Filter:
						select
							t.*
						into #tbl_POW_Contact_Filter
						from #tbl_POW_Contact_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_Contact --------:
						zzB_Select_POW_Contact_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_Contact_Filter_Draft;
						--drop table #tbl_POW_Contact_Filter;
					"
                    );
                ////
                string zzB_Select_POW_Contact_zzE = "-- Nothing.";
                if (bGet_POW_Contact)
                {
                    #region // bGet_POW_Contact:
                    zzB_Select_POW_Contact_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_Contact:
							select
								t.MyIdxSeq
								, pc.*
							from #tbl_POW_Contact_Filter t --//[mylock]
								inner join POW_Contact pc --//[mylock]
									on t.ContactNo = pc.ContactNo
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
                            , "POW_Contact" // strTableNameDB
                            , "POW_Contact." // strPrefixStd
                            , "pc." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprefix
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
                    , "zzB_Select_POW_Contact_zzE", zzB_Select_POW_Contact_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_Contact)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_Contact";
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

        public DataSet DAPOW_Contact_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objContactNo
            , object objContactAddress
            , object objContactPhoneNo
            , object objContactFax
            , object objContactEmail
            , object objContactMapAPI
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Contact_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_Contact_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objContactNo", objContactNo
                , "objContactAddress", objContactAddress
                , "objContactPhoneNo", objContactPhoneNo
                , "objContactFax", objContactFax
                , "objContactEmail", objContactEmail
                , "objContactMapAPI", objContactMapAPI
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Contact_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objContactNo
                        , objContactAddress
                        , objContactPhoneNo
                        , objContactFax
                        , objContactEmail
                        , objContactMapAPI
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

        private void DAPOW_Contact_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objContactNo
            , object objContactAddress
            , object objContactPhoneNo
            , object objContactFax
            , object objContactEmail
            , object objContactMapAPI
            //, object objFlagActive
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Contact_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objContactNo", objContactNo
                , "objContactAddress", objContactAddress
                , "objContactPhoneNo", objContactPhoneNo
                , "objContactFax", objContactFax
                , "objContactEmail", objContactEmail
                , "objContactMapAPI", objContactMapAPI
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strContactNo = TUtils.CUtils.StdParam(objContactNo);
            string strContactAddress = string.Format("{0}", objContactAddress).Trim();
            string strContactEmail = string.Format("{0}", objContactEmail).Trim();
            string strContactMapAPI = string.Format("{0}", objContactMapAPI).Trim();
            string strContactPhoneNo = TUtils.CUtils.StdParam(objContactPhoneNo);
            string strContactFax = TUtils.CUtils.StdParam(objContactFax);

            ////
            DataTable dtDB_POW_Contact = null;
            {
                ////
                if (strContactNo == null || strContactNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContactNo", strContactNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_CreateX_InvalidContactNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_Contact_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strContactNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_Contact // dtDB_POW_Contact
                    );

                ////
                if (strContactAddress == null || strContactAddress.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContactAddress", strContactAddress
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_CreateX_InvalidContactAddress
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strContactPhoneNo == null || strContactPhoneNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContactPhoneNo", strContactPhoneNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_CreateX_InvalidContactPhoneNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_Contact.NewRow();
                strFN = "ContactNo"; drDB[strFN] = strContactNo;
                strFN = "ContactAddress"; drDB[strFN] = strContactAddress;
                strFN = "ContactPhoneNo"; drDB[strFN] = strContactPhoneNo;
                strFN = "ContactFax"; drDB[strFN] = strContactFax;
                strFN = "ContactEmail"; drDB[strFN] = strContactEmail;
                strFN = "ContactMapAPI"; drDB[strFN] = strContactMapAPI;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_Contact.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_Contact" // strTableName
                    , dtDB_POW_Contact // dtData
                                       //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_Contact_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objContactNo
            , object objContactAddress
            , object objContactPhoneNo
            , object objContactFax
            , object objContactEmail
            , object objContactMapAPI
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Contact_Update";
            string strErrorCodeDefault = TError.ErrDA.POW_Contact_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objContactNo", objContactNo
                , "objContactAddress", objContactAddress
                , "objContactPhoneNo", objContactPhoneNo
                , "objContactFax", objContactFax
                , "objContactEmail", objContactEmail
                , "objContactMapAPI", objContactMapAPI
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // POW_Contact_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Contact_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objContactNo
                        , objContactAddress
                        , objContactPhoneNo
                        , objContactFax
                        , objContactEmail
                        , objContactMapAPI
                        , objFlagActive
                        ////
                        , objFt_Cols_Upd
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

        private void DAPOW_Contact_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objContactNo
            , object objContactAddress
            , object objContactPhoneNo
            , object objContactFax
            , object objContactEmail
            , object objContactMapAPI
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Contact_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objContactNo", objContactNo
                , "objContactAddress", objContactAddress
                , "objContactPhoneNo", objContactPhoneNo
                , "objContactFax", objContactFax
                , "objContactEmail", objContactEmail
                , "objContactMapAPI", objContactMapAPI
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
            string strContactNo = TUtils.CUtils.StdParam(objContactNo);
            string strContactAddress = string.Format("{0}", objContactAddress).Trim();
            string strContactEmail = string.Format("{0}", objContactEmail).Trim();
            string strContactMapAPI = string.Format("{0}", objContactMapAPI).Trim();
            string strContactPhoneNo = TUtils.CUtils.StdParam(objContactPhoneNo);
            string strContactFax = TUtils.CUtils.StdParam(objContactFax);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            //bool bUpd_ContactNo = strFt_Cols_Upd.Contains("POW_Contact.ContactNo".ToUpper());
            bool bUpd_ContactAddress = strFt_Cols_Upd.Contains("POW_Contact.ContactAddress".ToUpper());
            bool bUpd_ContactEmail = strFt_Cols_Upd.Contains("POW_Contact.ContactEmail".ToUpper());
            bool bUpd_ContactMapAPI = strFt_Cols_Upd.Contains("POW_Contact.ContactMapAPI".ToUpper());
            bool bUpd_ContactPhoneNo = strFt_Cols_Upd.Contains("POW_Contact.ContactPhoneNo".ToUpper());
            bool bUpd_ContactFax = strFt_Cols_Upd.Contains("POW_Contact.ContactFax".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("POW_Contact.FlagActive".ToUpper());

            ////
            DataTable dtDB_POW_Contact = null;
            {
                ////
                DAPOW_Contact_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strContactNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_Contact // dtDB_POW_Contact
                    );
                ////
                if (string.IsNullOrEmpty(strContactAddress) || strContactAddress.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContactAddress", strContactAddress
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_Update_InvalidContactAddress
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strContactPhoneNo) || strContactPhoneNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strContactPhoneNo", strContactPhoneNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_Contact_Update_InvalidContactPhoneNo
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
                DataRow drDB = dtDB_POW_Contact.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_ContactAddress) { strFN = "ContactAddress"; drDB[strFN] = strContactAddress; alColumnEffective.Add(strFN); }
                if (bUpd_ContactEmail) { strFN = "ContactEmail"; drDB[strFN] = strContactEmail; alColumnEffective.Add(strFN); }
                if (bUpd_ContactMapAPI) { strFN = "ContactMapAPI"; drDB[strFN] = strContactMapAPI; alColumnEffective.Add(strFN); }
                if (bUpd_ContactPhoneNo) { strFN = "ContactPhoneNo"; drDB[strFN] = strContactPhoneNo; alColumnEffective.Add(strFN); }
                if (bUpd_ContactFax) { strFN = "ContactFax"; drDB[strFN] = strContactFax; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                // Save:
                _cf.db.SaveData(
                    "POW_Contact"
                    , dtDB_POW_Contact
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAPOW_Contact_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objContactNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_Contact_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_Contact_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objContactNo", objContactNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_Contact_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objContactNo // objTourCode
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

        private void DAPOW_Contact_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objContactNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_Contact_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objContactNo", objContactNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strContactNo = TUtils.CUtils.StdParam(objContactNo);

            ////
            DataTable dtDB_POW_Contact = null;
            {
                ////
                DAPOW_Contact_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strContactNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_Contact // dtDB_POW_Contact
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_Contact.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_Contact"
                    , dtDB_POW_Contact
                    );
            }
            #endregion
        }
        #endregion

        #region // POW_Contact: WAS
        public DataSet WAS_DAPOW_Contact_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Contact objRQ_POW_Contact
            ////
            , out DA_RT_POW_Contact objRT_POW_Contact
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Contact.Tid;
            objRT_POW_Contact = new DA_RT_POW_Contact();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Contact_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Contact_Get;
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
                List<DA_POW_Contact> lst_POW_Contact = new List<DA_POW_Contact>();
                bool bGet_POW_Contact = (objRQ_POW_Contact.Rt_Cols_POW_Contact != null && objRQ_POW_Contact.Rt_Cols_POW_Contact.Length > 0);
                #endregion

                #region // WS_POW_Contact_Get:
                mdsResult = DAPOW_Contact_Get(
                    objRQ_POW_Contact.Tid // strTid
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    , objRQ_POW_Contact.WAUserCode // strUserCode
                    , objRQ_POW_Contact.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_Contact.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Contact.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_Contact.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_Contact.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_POW_Contact.Rt_Cols_POW_Contact // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Contact.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Contact)
                    {
                        DataTable dt_POW_Contact = mdsResult.Tables["POW_Contact"].Copy();
                        lst_POW_Contact = TUtils.DataTableCmUtils.ToListof<DA_POW_Contact>(dt_POW_Contact);
                        objRT_POW_Contact.Lst_POW_Contact = lst_POW_Contact;
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

        public DataSet WAS_DAPOW_Contact_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Contact objRQ_POW_Contact
            ////
            , out DA_RT_POW_Contact objRT_POW_Contact
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Contact.Tid;
            objRT_POW_Contact = new DA_RT_POW_Contact();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Contact_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Contact_Create;
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
                List<DA_POW_Contact> lst_POW_Contact = new List<DA_POW_Contact>();
                bool bGet_POW_Contact = (objRQ_POW_Contact.Rt_Cols_POW_Contact != null && objRQ_POW_Contact.Rt_Cols_POW_Contact.Length > 0);
                #endregion

                #region // WS_POW_Contact_Get:
                mdsResult = DAPOW_Contact_Create(
                    objRQ_POW_Contact.Tid // strTid
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    , objRQ_POW_Contact.WAUserCode // strUserCode
                    , objRQ_POW_Contact.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_Contact.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Contact.POW_Contact.ContactNo
                    , objRQ_POW_Contact.POW_Contact.ContactAddress
                    , objRQ_POW_Contact.POW_Contact.ContactPhoneNo
                    , objRQ_POW_Contact.POW_Contact.ContactFax
                    , objRQ_POW_Contact.POW_Contact.ContactEmail
                    , objRQ_POW_Contact.POW_Contact.ContactMapAPI
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Contact.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Contact)
                    {
                        DataTable dt_POW_Contact = mdsResult.Tables["POW_Contact"].Copy();
                        lst_POW_Contact = TUtils.DataTableCmUtils.ToListof<DA_POW_Contact>(dt_POW_Contact);
                        objRT_POW_Contact.Lst_POW_Contact = lst_POW_Contact;
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

        public DataSet WAS_DAPOW_Contact_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Contact objRQ_POW_Contact
            ////
            , out DA_RT_POW_Contact objRT_POW_Contact
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Contact.Tid;
            objRT_POW_Contact = new DA_RT_POW_Contact();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Contact_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Contact_Update;
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
                List<DA_POW_Contact> lst_POW_Contact = new List<DA_POW_Contact>();
                bool bGet_POW_Contact = (objRQ_POW_Contact.Rt_Cols_POW_Contact != null && objRQ_POW_Contact.Rt_Cols_POW_Contact.Length > 0);
                #endregion

                #region // WS_POW_Contact_Get:
                mdsResult = DAPOW_Contact_Update(
                    objRQ_POW_Contact.Tid // strTid
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    , objRQ_POW_Contact.WAUserCode // strUserCode
                    , objRQ_POW_Contact.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_Contact.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Contact.POW_Contact.ContactNo
                    , objRQ_POW_Contact.POW_Contact.ContactAddress
                    , objRQ_POW_Contact.POW_Contact.ContactPhoneNo
                    , objRQ_POW_Contact.POW_Contact.ContactFax
                    , objRQ_POW_Contact.POW_Contact.ContactEmail
                    , objRQ_POW_Contact.POW_Contact.ContactMapAPI
                    //, objRQ_POW_Contact.POW_Contact.Author
                    , objRQ_POW_Contact.POW_Contact.FlagActive
                    ////
                    , objRQ_POW_Contact.Ft_Cols_Upd
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Contact.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Contact)
                    {
                        DataTable dt_POW_Contact = mdsResult.Tables["POW_Contact"].Copy();
                        lst_POW_Contact = TUtils.DataTableCmUtils.ToListof<DA_POW_Contact>(dt_POW_Contact);
                        objRT_POW_Contact.Lst_POW_Contact = lst_POW_Contact;
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

        public DataSet WAS_DAPOW_Contact_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_Contact objRQ_POW_Contact
            ////
            , out DA_RT_POW_Contact objRT_POW_Contact
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_Contact.Tid;
            objRT_POW_Contact = new DA_RT_POW_Contact();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_Contact_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_Contact_Delete;
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
                List<DA_POW_Contact> lst_POW_Contact = new List<DA_POW_Contact>();
                bool bGet_POW_Contact = (objRQ_POW_Contact.Rt_Cols_POW_Contact != null && objRQ_POW_Contact.Rt_Cols_POW_Contact.Length > 0);
                #endregion

                #region // WS_POW_Contact_Get:
                mdsResult = DAPOW_Contact_Delete(
                    objRQ_POW_Contact.Tid // strTid
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    , objRQ_POW_Contact.WAUserCode // strUserCode
                    , objRQ_POW_Contact.WAUserPassword // strUserPassword
                                                       //, TUtils.CUtils.StdFlag(objRQ_POW_Contact.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_Contact.POW_Contact.ContactNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_Contact.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_Contact)
                    {
                        DataTable dt_POW_Contact = mdsResult.Tables["POW_Contact"].Copy();
                        lst_POW_Contact = TUtils.DataTableCmUtils.ToListof<DA_POW_Contact>(dt_POW_Contact);
                        objRT_POW_Contact.Lst_POW_Contact = lst_POW_Contact;
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
        #endregion

        #region // POW_ContactEmail
        private void DAPOW_ContactEmail_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objCENo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_ContactEmail
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_ContactEmail t --//[mylock]
					where (1=1)
						and t.CENo = @objCENo
					;
				");
            dtDB_POW_ContactEmail = _cf.db.ExecQuery(
                strSqlExec
                , "@objCENo", objCENo
                ).Tables[0];
            dtDB_POW_ContactEmail.TableName = "POW_ContactEmail";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_ContactEmail.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CENo", objCENo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_ContactEmail_CheckDB_CENoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_ContactEmail.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CENo", objCENo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_ContactEmail_CheckDB_CENoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_ContactEmail.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CENo", objCENo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_ContactEmail.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_ContactEmail_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_ContactEmail_Get(
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
            , string strRt_Cols_POW_ContactEmail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_ContactEmail_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_ContactEmail_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_ContactEmail", strRt_Cols_POW_ContactEmail
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_ContactEmail = (strRt_Cols_POW_ContactEmail != null && strRt_Cols_POW_ContactEmail.Length > 0);

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
						---- #tbl_POW_ContactEmail_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pce.CENo
						into #tbl_POW_ContactEmail_Filter_Draft
						from POW_ContactEmail pce --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pce.CENo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_ContactEmail_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_ContactEmail_Filter:
						select
							t.*
						into #tbl_POW_ContactEmail_Filter
						from #tbl_POW_ContactEmail_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_ContactEmail --------:
						zzB_Select_POW_ContactEmail_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_ContactEmail_Filter_Draft;
						--drop table #tbl_POW_ContactEmail_Filter;
					"
                    );
                ////
                string zzB_Select_POW_ContactEmail_zzE = "-- Nothing.";
                if (bGet_POW_ContactEmail)
                {
                    #region // bGet_POW_ContactEmail:
                    zzB_Select_POW_ContactEmail_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_ContactEmail:
							select
								t.MyIdxSeq
								, pce.*
							from #tbl_POW_ContactEmail_Filter t --//[mylock]
								inner join POW_ContactEmail pce --//[mylock]
									on t.CENo = pce.CENo
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
                            , "POW_ContactEmail" // strTableNameDB
                            , "POW_ContactEmail." // strPrefixStd
                            , "pce." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprefix
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
                    , "zzB_Select_POW_ContactEmail_zzE", zzB_Select_POW_ContactEmail_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_ContactEmail)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_ContactEmail";
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

        public DataSet DAPOW_ContactEmail_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objCENo
            , object objInformationType
            , object objCEName
            , object objCEEmail
            , object objCEMobileNo
            , object objCECompanyName
            , object objCETouristNumber
            , object objCEAddress
            , object objCETitle
            , object objCEContent
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_ContactEmail_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_ContactEmail_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objCENo", objCENo
                , "objInformationType", objInformationType
                , "objCEName", objCEName
                , "objCEEmail", objCEEmail
                , "objCEMobileNo", objCEMobileNo
                , "objCECompanyName", objCECompanyName
                , "objCETouristNumber", objCETouristNumber
                , "objCEAddress", objCEAddress
                , "objCETitle", objCETitle
                , "objCEContent", objCEContent
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_ContactEmail_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCENo
                        , objInformationType
                        , objCEName
                        , objCEEmail
                        , objCEMobileNo
                        , objCECompanyName
                        , objCETouristNumber
                        , objCEAddress
                        , objCETitle
                        , objCEContent
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

        private void DAPOW_ContactEmail_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCENo
            , object objInformationType
            , object objCEName
            , object objCEEmail
            , object objCEMobileNo
            , object objCECompanyName
            , object objCETouristNumber
            , object objCEAddress
            , object objCETitle
            , object objCEContent
            )
        {
            #region // Temp:
            string strFunctionName = "POW_ContactEmail_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objCENo", objCENo
                , "objInformationType", objInformationType
                , "objCEName", objCEName
                , "objCEEmail", objCEEmail
                , "objCEMobileNo", objCEMobileNo
                , "objCECompanyName", objCECompanyName
                , "objCETouristNumber", objCETouristNumber
                , "objCEAddress", objCEAddress
                , "objCETitle", objCETitle
                , "objCEContent", objCEContent
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCENo = TUtils.CUtils.StdParam(objCENo);
            string strInformationType = string.Format("{0}", objInformationType).Trim();
            string strCEName = string.Format("{0}", objCEName).Trim();
            string strCEEmail = string.Format("{0}", objCEEmail).Trim();
            string strCEMobileNo = TUtils.CUtils.StdParam(objCEMobileNo);
            string strCETouristNumber = TUtils.CUtils.StdParam(objCETouristNumber);
            string strCECompanyName = string.Format("{0}", objCECompanyName).Trim();
            string strCEAddress = string.Format("{0}", objCEAddress).Trim();
            string strCETitle = string.Format("{0}", objCETitle).Trim();
            string strCEContent = string.Format("{0}", objCEContent).Trim();

            ////
            DataTable dtDB_POW_ContactEmail = null;
            {
                ////
                if (strCENo == null || strCENo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strCENo", strCENo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_ContactEmail_CreateX_InvalidCENo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_ContactEmail_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCENo // strCENo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_ContactEmail // dtDB_POW_ContactEmail
                    );


            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_ContactEmail.NewRow();
                strFN = "CENo"; drDB[strFN] = strCENo;
                strFN = "InformationType"; drDB[strFN] = strInformationType;
                strFN = "CEName"; drDB[strFN] = strCEName;
                strFN = "CEEmail"; drDB[strFN] = strCEEmail;
                strFN = "CEMobileNo"; drDB[strFN] = strCEMobileNo;
                strFN = "CETouristNumber"; drDB[strFN] = strCETouristNumber;
                strFN = "CECompanyName"; drDB[strFN] = strCECompanyName;
                strFN = "CEAddress"; drDB[strFN] = strCEAddress;
                strFN = "CETitle"; drDB[strFN] = strCETitle;
                strFN = "CEContent"; drDB[strFN] = strCEContent;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_ContactEmail.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_ContactEmail" // strTableName
                    , dtDB_POW_ContactEmail // dtData
                                            //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_ContactEmail_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCENo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_ContactEmail_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_ContactEmail_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objCENo", objCENo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_ContactEmail_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCENo // objTourCode
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

        private void DAPOW_ContactEmail_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCENo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_ContactEmail_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objCENo", objCENo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCENo = TUtils.CUtils.StdParam(objCENo);

            ////
            DataTable dtDB_POW_ContactEmail = null;
            {
                ////
                DAPOW_ContactEmail_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strCENo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_ContactEmail // dtDB_POW_ContactEmail
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_ContactEmail.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_ContactEmail"
                    , dtDB_POW_ContactEmail
                    );
            }
            #endregion
        }
        #endregion

        #region // POW_ContactEmail: WAS
        public DataSet WAS_DAPOW_ContactEmail_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail
            ////
            , out DA_RT_POW_ContactEmail objRT_POW_ContactEmail
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_ContactEmail.Tid;
            objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_ContactEmail_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_ContactEmail_Get;
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
                List<DA_POW_ContactEmail> lst_POW_ContactEmail = new List<DA_POW_ContactEmail>();
                bool bGet_POW_ContactEmail = (objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail != null && objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail.Length > 0);
                #endregion

                #region // WS_POW_ContactEmail_Get:
                mdsResult = DAPOW_ContactEmail_Get(
                    objRQ_POW_ContactEmail.Tid // strTid
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    , objRQ_POW_ContactEmail.WAUserCode // strUserCode
                    , objRQ_POW_ContactEmail.WAUserPassword // strUserPassword
                                                            //, TUtils.CUtils.StdFlag(objRQ_POW_ContactEmail.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_ContactEmail.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_ContactEmail.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_ContactEmail.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_ContactEmail.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_ContactEmail)
                    {
                        DataTable dt_POW_ContactEmail = mdsResult.Tables["POW_ContactEmail"].Copy();
                        lst_POW_ContactEmail = TUtils.DataTableCmUtils.ToListof<DA_POW_ContactEmail>(dt_POW_ContactEmail);
                        objRT_POW_ContactEmail.Lst_POW_ContactEmail = lst_POW_ContactEmail;
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

        public DataSet WAS_DAPOW_ContactEmail_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail
            ////
            , out DA_RT_POW_ContactEmail objRT_POW_ContactEmail
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_ContactEmail.Tid;
            objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_ContactEmail_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_ContactEmail_Create;
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
                List<DA_POW_ContactEmail> lst_POW_ContactEmail = new List<DA_POW_ContactEmail>();
                bool bGet_POW_ContactEmail = (objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail != null && objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail.Length > 0);
                #endregion

                #region // WS_POW_ContactEmail_Get:
                mdsResult = DAPOW_ContactEmail_Create(
                    objRQ_POW_ContactEmail.Tid // strTid
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    , objRQ_POW_ContactEmail.WAUserCode // strUserCode
                    , objRQ_POW_ContactEmail.WAUserPassword // strUserPassword
                                                            //, TUtils.CUtils.StdFlag(objRQ_POW_ContactEmail.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CENo
                    , objRQ_POW_ContactEmail.POW_ContactEmail.InformationType
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CEName
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CEEmail
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CEMobileNo
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CECompanyName
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CETouristNumber
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CEAddress
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CETitle
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CEContent
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_ContactEmail.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_ContactEmail)
                    {
                        DataTable dt_POW_ContactEmail = mdsResult.Tables["POW_ContactEmail"].Copy();
                        lst_POW_ContactEmail = TUtils.DataTableCmUtils.ToListof<DA_POW_ContactEmail>(dt_POW_ContactEmail);
                        objRT_POW_ContactEmail.Lst_POW_ContactEmail = lst_POW_ContactEmail;
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

        public DataSet WAS_DAPOW_ContactEmail_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail
            ////
            , out DA_RT_POW_ContactEmail objRT_POW_ContactEmail
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_ContactEmail.Tid;
            objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_ContactEmail_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_ContactEmail_Delete;
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
                List<DA_POW_ContactEmail> lst_POW_ContactEmail = new List<DA_POW_ContactEmail>();
                bool bGet_POW_ContactEmail = (objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail != null && objRQ_POW_ContactEmail.Rt_Cols_POW_ContactEmail.Length > 0);
                #endregion

                #region // WS_POW_ContactEmail_Get:
                mdsResult = DAPOW_ContactEmail_Delete(
                    objRQ_POW_ContactEmail.Tid // strTid
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    , objRQ_POW_ContactEmail.WAUserCode // strUserCode
                    , objRQ_POW_ContactEmail.WAUserPassword // strUserPassword
                                                            //, TUtils.CUtils.StdFlag(objRQ_POW_ContactEmail.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_ContactEmail.POW_ContactEmail.CENo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_ContactEmail.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_ContactEmail)
                    {
                        DataTable dt_POW_ContactEmail = mdsResult.Tables["POW_ContactEmail"].Copy();
                        lst_POW_ContactEmail = TUtils.DataTableCmUtils.ToListof<DA_POW_ContactEmail>(dt_POW_ContactEmail);
                        objRT_POW_ContactEmail.Lst_POW_ContactEmail = lst_POW_ContactEmail;
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
        #endregion

        #region // POW_FAQ
        private void DAPOW_FAQ_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objFAQNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_POW_FAQ
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from POW_FAQ t --//[mylock]
					where (1=1)
						and t.FAQNo = @objFAQNo
					;
				");
            dtDB_POW_FAQ = _cf.db.ExecQuery(
                strSqlExec
                , "@objFAQNo", objFAQNo
                ).Tables[0];
            dtDB_POW_FAQ.TableName = "POW_FAQ";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_POW_FAQ.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.FAQNo", objFAQNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_CheckDB_FAQNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_POW_FAQ.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.FAQNo", objFAQNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_CheckDB_FAQNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_POW_FAQ.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.FAQNo", objFAQNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_POW_FAQ.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.POW_FAQ_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAPOW_FAQ_Get(
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
            , string strRt_Cols_POW_FAQ
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "POW_FAQ_Get";
            string strErrorCodeDefault = TError.ErrDA.POW_FAQ_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_POW_FAQ", strRt_Cols_POW_FAQ
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_POW_FAQ = (strRt_Cols_POW_FAQ != null && strRt_Cols_POW_FAQ.Length > 0);

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
						---- #tbl_POW_FAQ_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, pfaq.FAQNo
						into #tbl_POW_FAQ_Filter_Draft
						from POW_FAQ pfaq --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by pfaq.FAQNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_POW_FAQ_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_FAQ_Filter:
						select
							t.*
						into #tbl_POW_FAQ_Filter
						from #tbl_POW_FAQ_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- POW_FAQ --------:
						zzB_Select_POW_FAQ_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_POW_FAQ_Filter_Draft;
						--drop table #tbl_POW_FAQ_Filter;
					"
                    );
                ////
                string zzB_Select_POW_FAQ_zzE = "-- Nothing.";
                if (bGet_POW_FAQ)
                {
                    #region // bGet_POW_FAQ:
                    zzB_Select_POW_FAQ_zzE = CmUtils.StringUtils.Replace(@"
							---- POW_FAQ:
							select
								t.MyIdxSeq
								, pfaq.*
							from #tbl_POW_FAQ_Filter t --//[mylock]
								inner join POW_FAQ pfaq --//[mylock]
									on t.FAQNo = pfaq.FAQNo
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
                            , "POW_FAQ" // strTableNameDB
                            , "POW_FAQ." // strPrefixStd
                            , "pfaq." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprefix
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
                    , "zzB_Select_POW_FAQ_zzE", zzB_Select_POW_FAQ_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_POW_FAQ)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "POW_FAQ";
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

        public DataSet DAPOW_FAQ_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objFAQNo
            , object objQuestion
            , object objAnswer
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_FAQ_Create";
            string strErrorCodeDefault = TError.ErrDA.POW_FAQ_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objFAQNo", objFAQNo
                , "objQuestion", objQuestion
                , "objAnswer", objAnswer
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_FAQ_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFAQNo
                        , objQuestion
                        , objAnswer
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

        private void DAPOW_FAQ_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objFAQNo
            , object objQuestion
            , object objAnswer
            )
        {
            #region // Temp:
            string strFunctionName = "POW_FAQ_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objFAQNo", objFAQNo
                , "objQuestion", objQuestion
                , "objAnswer", objAnswer
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strFAQNo = TUtils.CUtils.StdParam(objFAQNo);
            string strQuestion = string.Format("{0}", objQuestion).Trim();
            string strAnswer = string.Format("{0}", objAnswer).Trim();

            ////
            DataTable dtDB_POW_FAQ = null;
            {
                ////
                if (strFAQNo == null || strFAQNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFAQNo", strFAQNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_CreateX_InvalidFAQNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAPOW_FAQ_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strFAQNo // objTGNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_POW_FAQ // dtDB_POW_FAQ
                    );

                ////
                if (strQuestion == null || strQuestion.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strQuestion", strQuestion
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_CreateX_InvalidQuestion
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strAnswer == null || strAnswer.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strAnswer", strAnswer
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_CreateX_InvalidAnswer
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_POW_FAQ.NewRow();
                strFN = "FAQNo"; drDB[strFN] = strFAQNo;
                strFN = "Question"; drDB[strFN] = strQuestion;
                strFN = "Answer"; drDB[strFN] = strAnswer;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "PostDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                dtDB_POW_FAQ.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "POW_FAQ" // strTableName
                    , dtDB_POW_FAQ // dtData
                                   //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet DAPOW_FAQ_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objFAQNo
            , object objQuestion
            , object objAnswer
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_FAQ_Update";
            string strErrorCodeDefault = TError.ErrDA.POW_FAQ_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objFAQNo", objFAQNo
                , "objQuestion", objQuestion
                , "objAnswer", objAnswer
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // POW_FAQ_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAPOW_FAQ_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFAQNo
                        , objQuestion
                        , objAnswer
                        , objFlagActive
                        ////
                        , objFt_Cols_Upd
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

        private void DAPOW_FAQ_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objFAQNo
            , object objQuestion
            , object objAnswer
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_FAQ_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objFAQNo", objFAQNo
                , "objQuestion", objQuestion
                , "objAnswer", objAnswer
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
            string strFAQNo = TUtils.CUtils.StdParam(objFAQNo);
            string strQuestion = string.Format("{0}", objQuestion).Trim();
            string strAnswer = string.Format("{0}", objAnswer).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            //bool bUpd_FAQNo = strFt_Cols_Upd.Contains("POW_FAQ.FAQNo".ToUpper());
            bool bUpd_Question = strFt_Cols_Upd.Contains("POW_FAQ.Question".ToUpper());
            bool bUpd_Answer = strFt_Cols_Upd.Contains("POW_FAQ.Answer".ToUpper());

            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("POW_FAQ.FlagActive".ToUpper());

            ////
            DataTable dtDB_POW_FAQ = null;
            {
                ////
                DAPOW_FAQ_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strFAQNo // objPartCode
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_FAQ // dtDB_POW_FAQ
                    );
                ////
                if (string.IsNullOrEmpty(strQuestion) || strQuestion.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strQuestion", strQuestion
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_Update_InvalidQuestion
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strAnswer) || strAnswer.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strAnswer", strAnswer
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.POW_FAQ_Update_InvalidAnswer
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
                DataRow drDB = dtDB_POW_FAQ.Rows[0];
                //if (bUpd_TGNo) { strFN = "TGNo"; drDB[strFN] = strTGNo; alColumnEffective.Add(strFN); }
                if (bUpd_Question) { strFN = "Question"; drDB[strFN] = strQuestion; alColumnEffective.Add(strFN); }
                if (bUpd_Answer) { strFN = "Answer"; drDB[strFN] = strAnswer; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                // Save:
                _cf.db.SaveData(
                    "POW_FAQ"
                    , dtDB_POW_FAQ
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }

        public DataSet DAPOW_FAQ_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFAQNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "POW_FAQ_Delete";
            string strErrorCodeDefault = TError.ErrDA.POW_FAQ_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objFAQNo", objFAQNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Part_DeleteX:
                //DataSet dsGetData = null;
                {
                    DAPOW_FAQ_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFAQNo // objTourCode
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

        private void DAPOW_FAQ_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objFAQNo
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "POW_FAQ_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objFAQNo", objFAQNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strFAQNo = TUtils.CUtils.StdParam(objFAQNo);

            ////
            DataTable dtDB_POW_FAQ = null;
            {
                ////
                DAPOW_FAQ_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strFAQNo
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , "" // strFlagActiveListToCheck
                     , out dtDB_POW_FAQ // dtDB_POW_FAQ
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_POW_FAQ.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "POW_FAQ"
                    , dtDB_POW_FAQ
                    );
            }
            #endregion
        }
        #endregion

        #region // POW_FAQ: WAS
        public DataSet WAS_DAPOW_FAQ_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_FAQ objRQ_POW_FAQ
            ////
            , out DA_RT_POW_FAQ objRT_POW_FAQ
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_FAQ.Tid;
            objRT_POW_FAQ = new DA_RT_POW_FAQ();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_FAQ_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_FAQ_Get;
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
                List<DA_POW_FAQ> lst_POW_FAQ = new List<DA_POW_FAQ>();
                bool bGet_POW_FAQ = (objRQ_POW_FAQ.Rt_Cols_POW_FAQ != null && objRQ_POW_FAQ.Rt_Cols_POW_FAQ.Length > 0);
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAPOW_FAQ_Get(
                    objRQ_POW_FAQ.Tid // strTid
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    , objRQ_POW_FAQ.WAUserCode // strUserCode
                    , objRQ_POW_FAQ.WAUserPassword // strUserPassword
                                                   //, TUtils.CUtils.StdFlag(objRQ_POW_FAQ.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_FAQ.Ft_RecordStart // strFt_RecordStart
                    , objRQ_POW_FAQ.Ft_RecordCount // strFt_RecordCount
                    , objRQ_POW_FAQ.Ft_WhereClause // strFt_WhereClause
                                                   //// Return:
                    , objRQ_POW_FAQ.Rt_Cols_POW_FAQ // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_FAQ.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_FAQ)
                    {
                        DataTable dt_POW_FAQ = mdsResult.Tables["POW_FAQ"].Copy();
                        lst_POW_FAQ = TUtils.DataTableCmUtils.ToListof<DA_POW_FAQ>(dt_POW_FAQ);
                        objRT_POW_FAQ.Lst_POW_FAQ = lst_POW_FAQ;
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

        public DataSet WAS_DAPOW_FAQ_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_FAQ objRQ_POW_FAQ
            ////
            , out DA_RT_POW_FAQ objRT_POW_FAQ
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_FAQ.Tid;
            objRT_POW_FAQ = new DA_RT_POW_FAQ();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_FAQ_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_FAQ_Create;
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
                List<DA_POW_FAQ> lst_POW_FAQ = new List<DA_POW_FAQ>();
                bool bGet_POW_FAQ = (objRQ_POW_FAQ.Rt_Cols_POW_FAQ != null && objRQ_POW_FAQ.Rt_Cols_POW_FAQ.Length > 0);
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAPOW_FAQ_Create(
                    objRQ_POW_FAQ.Tid // strTid
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    , objRQ_POW_FAQ.WAUserCode // strUserCode
                    , objRQ_POW_FAQ.WAUserPassword // strUserPassword
                                                   //, TUtils.CUtils.StdFlag(objRQ_POW_FAQ.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_FAQ.POW_FAQ.FAQNo
                    , objRQ_POW_FAQ.POW_FAQ.Question
                    , objRQ_POW_FAQ.POW_FAQ.Answer
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_FAQ.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_FAQ)
                    {
                        DataTable dt_POW_FAQ = mdsResult.Tables["POW_FAQ"].Copy();
                        lst_POW_FAQ = TUtils.DataTableCmUtils.ToListof<DA_POW_FAQ>(dt_POW_FAQ);
                        objRT_POW_FAQ.Lst_POW_FAQ = lst_POW_FAQ;
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

        public DataSet WAS_DAPOW_FAQ_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_FAQ objRQ_POW_FAQ
            ////
            , out DA_RT_POW_FAQ objRT_POW_FAQ
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_FAQ.Tid;
            objRT_POW_FAQ = new DA_RT_POW_FAQ();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_FAQ_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_FAQ_Update;
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
                List<DA_POW_FAQ> lst_POW_FAQ = new List<DA_POW_FAQ>();
                bool bGet_POW_FAQ = (objRQ_POW_FAQ.Rt_Cols_POW_FAQ != null && objRQ_POW_FAQ.Rt_Cols_POW_FAQ.Length > 0);
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAPOW_FAQ_Update(
                    objRQ_POW_FAQ.Tid // strTid
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    , objRQ_POW_FAQ.WAUserCode // strUserCode
                    , objRQ_POW_FAQ.WAUserPassword // strUserPassword
                                                   //, TUtils.CUtils.StdFlag(objRQ_POW_FAQ.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_FAQ.POW_FAQ.FAQNo
                    , objRQ_POW_FAQ.POW_FAQ.Question
                    , objRQ_POW_FAQ.POW_FAQ.Answer
                    , objRQ_POW_FAQ.POW_FAQ.FlagActive
                    ////
                    , objRQ_POW_FAQ.Ft_Cols_Upd
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_FAQ.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_FAQ)
                    {
                        DataTable dt_POW_FAQ = mdsResult.Tables["POW_FAQ"].Copy();
                        lst_POW_FAQ = TUtils.DataTableCmUtils.ToListof<DA_POW_FAQ>(dt_POW_FAQ);
                        objRT_POW_FAQ.Lst_POW_FAQ = lst_POW_FAQ;
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

        public DataSet WAS_DAPOW_FAQ_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_POW_FAQ objRQ_POW_FAQ
            ////
            , out DA_RT_POW_FAQ objRT_POW_FAQ
            )
        {
            #region // Temp:
            string strTid = objRQ_POW_FAQ.Tid;
            objRT_POW_FAQ = new DA_RT_POW_FAQ();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_POW_FAQ_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_POW_FAQ_Delete;
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
                List<DA_POW_FAQ> lst_POW_FAQ = new List<DA_POW_FAQ>();
                bool bGet_POW_FAQ = (objRQ_POW_FAQ.Rt_Cols_POW_FAQ != null && objRQ_POW_FAQ.Rt_Cols_POW_FAQ.Length > 0);
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAPOW_FAQ_Delete(
                    objRQ_POW_FAQ.Tid // strTid
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    , objRQ_POW_FAQ.WAUserCode // strUserCode
                    , objRQ_POW_FAQ.WAUserPassword // strUserPassword
                                                   //, TUtils.CUtils.StdFlag(objRQ_POW_FAQ.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_POW_FAQ.POW_FAQ.FAQNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_POW_FAQ.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_POW_FAQ)
                    {
                        DataTable dt_POW_FAQ = mdsResult.Tables["POW_FAQ"].Copy();
                        lst_POW_FAQ = TUtils.DataTableCmUtils.ToListof<DA_POW_FAQ>(dt_POW_FAQ);
                        objRT_POW_FAQ.Lst_POW_FAQ = lst_POW_FAQ;
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
        #endregion

        #region // Mst_Article:
        private void DAMst_Article_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objArticleNo
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Article
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Article t --//[mylock]
					where (1=1)
						and t.ArticleNo = @objArticleNo
					;
				");
            dtDB_Mst_Article = _cf.db.ExecQuery(
                strSqlExec
                , "@objArticleNo", objArticleNo
                ).Tables[0];
            dtDB_Mst_Article.TableName = "Mst_Article";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Article.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ArticleNo", objArticleNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CheckDB_ArticleNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Article.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ArticleNo", objArticleNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CheckDB_ArticleNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Article.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ArticleNo", objArticleNo
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Article.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Mst_Article_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet DAMst_Article_Get(
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
            , string strRt_Cols_Mst_Article
            , string strRt_Cols_Mst_ArticleDetail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_Article_Get";
            string strErrorCodeDefault = TError.ErrDA.Mst_Article_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Article", strRt_Cols_Mst_Article
                , "strRt_Cols_Mst_ArticleDetail", strRt_Cols_Mst_ArticleDetail
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

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
                bool bGet_Mst_Article = (strRt_Cols_Mst_Article != null && strRt_Cols_Mst_Article.Length > 0);
                bool bGet_Mst_ArticleDetail = (strRt_Cols_Mst_ArticleDetail != null && strRt_Cols_Mst_ArticleDetail.Length > 0);

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
						---- #tbl_Mst_Article_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, ma.ArticleNo
						into #tbl_Mst_Article_Filter_Draft
						from Mst_Article ma --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by ma.ArticleNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Article_Filter_Draft t --//[mylock]
						;

						---- #tbl_POW_FAQ_Filter:
						select
							t.*
						into #tbl_Mst_Article_Filter
						from #tbl_Mst_Article_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Article --------:
						zzB_Select_Mst_Article_zzE
						
						-------- Mst_ArticleDetail --------:
						zzB_Select_Mst_ArticleDetail_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Article_Filter_Draft;
						--drop table #tbl_Mst_Article_Filter;
					"
                    );
                ////
                string zzB_Select_Mst_Article_zzE = "-- Nothing.";
                string zzB_Select_Mst_ArticleDetail_zzE = "--Nothing.";
                if (bGet_Mst_Article)
                {
                    #region // bGet_Mst_Article:
                    zzB_Select_Mst_Article_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Article:
							select
								t.MyIdxSeq
								, ma.*
							from #tbl_Mst_Article_Filter t --//[mylock]
								inner join Mst_Article ma --//[mylock]
									on t.ArticleNo = ma.ArticleNo
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                if (bGet_Mst_ArticleDetail)
                {
                    #region // bGet_Mst_ArticleDetail:
                    zzB_Select_Mst_ArticleDetail_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_ArticleDetail:
							select
								t.MyIdxSeq
								, mad.*
							from #tbl_Mst_Article_Filter t --//[mylock]
								inner join Mst_Article ma --//[mylock]
									on t.ArticleNo = ma.ArticleNo
								inner join Mst_ArticleDetail mad --//[mylock]
									on ma.ArticleNo = mad.ArticleNo
							order by t.MyIdxSeq, mad.Idx asc
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
                            , "Mst_Article" // strTableNameDB
                            , "Mst_Article." // strPrefixStd
                            , "ma." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParaprefix
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
                    , "zzB_Select_Mst_Article_zzE", zzB_Select_Mst_Article_zzE
                    , "zzB_Select_Mst_ArticleDetail_zzE", zzB_Select_Mst_ArticleDetail_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Mst_Article)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_Article";
                }
                if (bGet_Mst_ArticleDetail)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_ArticleDetail";
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

        public DataSet DAMst_Article_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objArticleNo
            , object objArticleTitle
            , object objArticleDesc
            , object objArticleThemePath
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Article_Create";
            string strErrorCodeDefault = TError.ErrDA.Mst_Article_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objArticleNo", objArticleNo
                , "objArticleTitle", objArticleTitle
                , "objArticleDesc", objArticleDesc
                , "objArticleThemePath", objArticleThemePath
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Tour_CreateX:
                //DataSet dsGetData = null;
                {
                    DAMst_Article_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objArticleNo
                        , objArticleTitle
                        , objArticleDesc
                        , objArticleThemePath
                        ////
                        , dsData
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

        private void DAMst_Article_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objArticleNo
            , object objArticleTitle
            , object objArticleDesc
            , object objArticleThemePath
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Article_CreateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objArticleNo", objArticleNo
                , "objArticleTitle", objArticleTitle
                , "objArticleDesc", objArticleDesc
                , "objArticleThemePath", objArticleThemePath
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strArticleNo = TUtils.CUtils.StdParam(objArticleNo);
            string strArticleTitle = string.Format("{0}", objArticleTitle).Trim();
            string strArticleDesc = string.Format("{0}", objArticleDesc).Trim();
            string strArticleThemePath = string.Format("{0}", objArticleThemePath).Trim();
            string strAuthor = "Vietravel";

            ////
            DataTable dtDB_Mst_Article = null;
            {
                ////
                if (strArticleNo == null || strArticleNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strArticleNo", strArticleNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_InvalidArticleNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DAMst_Article_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strArticleNo // objArticleNo
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Article // dtDB_Mst_Article
                    );

                ////
                if (strArticleTitle == null || strArticleTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strArticleTitle", strArticleTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_InvalidArticleTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strArticleThemePath == null || strArticleThemePath.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strArticleThemePath", strArticleThemePath
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_InvalidArticleThemePath
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Save Temp Mst_Article:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_Article"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "ArticleNo"
                        , "ArticleTitle"
                        , "ArticleDesc"
                        , "ArticleThemePath"
                        , "Author"
                        , "PostDTime"
                        , "FlagShow"
                        , "FlagActive"
                        }
                    , new object[]{
                        new object[]{
                            strArticleNo // IF_InvInNo
                            , strArticleTitle
                            , strArticleDesc
                            , strArticleThemePath
                            , strAuthor
                            , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                            , TConst.Flag.Active
                            , TConst.Flag.Active
                            }
                        }
                    );

            }
            #endregion

            #region // Refine and Check Mst_ArticleDetail
            DataTable dtInput_Mst_ArticleDetail = null;
            {
                string strTableCheck = "Mst_ArticleDetail";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ArticleDetail = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_ArticleDetail.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ArticleDetail // dtData
                    , "StdParam", "ArticleNo" // arrstrCouple
                    , "int", "Idx" // arrstrCouple
                    , "", "ArticleContent" // arrstrCouple
                    , "", "ArticleImageName" // arrstrCouple
                    , "", "ArticleImagePath" // arrstrCouple
                    , "", "Title" // arrstrCouple
                    );
                ////
                for (int nScan = 0; nScan < dtInput_Mst_ArticleDetail.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ArticleDetail.Rows[nScan];
                    ////
                }
            }
            #endregion

            #region // Save Temp Mst_ArticleDetail
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ArticleDetail" // strTableName
                    , new object[] {
                            "ArticleNo", TConst.BizMix.Default_DBColType
                            , "Idx", TConst.BizMix.Default_DBColType
                            , "ArticleContent", TConst.BizMix.Default_DBColType
                            , "ArticleImageName", TConst.BizMix.Default_DBColType
                            , "ArticleImagePath", TConst.BizMix.Default_DBColType
                            , "Title", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ArticleDetail // dtData
                );
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                //// Insert:
                string strSqlInsert = CmUtils.StringUtils.Replace(@"
						---- Mst_Article:
						insert into Mst_Article(
							ArticleNo
							, ArticleTitle
							, ArticleDesc
							, ArticleThemePath
							, Author
							, PostDTime
							, FlagShow
							, FlagActive
						)
						select
							ArticleNo
							, ArticleTitle
							, ArticleDesc
							, ArticleThemePath
							, Author
							, PostDTime
							, FlagShow
							, FlagActive
						from #input_Mst_Article t --//[mylock]
						
						---- Mst_ArticleDetail:
						insert into Mst_ArticleDetail(
							ArticleNo
							, Idx
							, ArticleContent
							, ArticleImageName
							, ArticleImagePath
							, Title
						)
						select
							t.ArticleNo
							, t.Idx
							, t.ArticleContent
							, t.ArticleImageName
							, t.ArticleImagePath
							, t.Title
						from #input_Mst_ArticleDetail t --//[mylock]
					");

                DataSet dsExec = _cf.db.ExecQuery(strSqlInsert);
            }
            #endregion

            #region // Clear for Debug
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Article;
						drop table #input_Mst_ArticleDetail;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion
        }

        public DataSet DAMst_Article_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objArticleNo
            , object objArticleTitle
            , object objArticleDesc
            , object objArticleThemePath
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Article_Update";
            string strErrorCodeDefault = TError.ErrDA.Mst_Article_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objArticleNo", objArticleNo
                , "objArticleTitle", objArticleTitle
                , "objArticleDesc", objArticleDesc
                , "objArticleThemePath", objArticleThemePath
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Article_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAMst_Article_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objArticleNo
                        , objArticleTitle
                        , objArticleDesc
                        , objArticleThemePath
                        ////
                        , dsData
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

        private void DAMst_Article_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objArticleNo
            , object objArticleTitle
            , object objArticleDesc
            , object objArticleThemePath
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Article_UpdateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objArticleNo", objArticleNo
                , "objArticleTitle", objArticleTitle
                , "objArticleDesc", objArticleDesc
                , "objArticleThemePath", objArticleThemePath
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strArticleNo = TUtils.CUtils.StdParam(objArticleNo);
            string strArticleTitle = string.Format("{0}", objArticleTitle).Trim();
            string strArticleDesc = string.Format("{0}", objArticleDesc).Trim();
            string strArticleThemePath = string.Format("{0}", objArticleThemePath).Trim();

            ////
            DataTable dtDB_Mst_Article = null;
            string strAuthor = null;
            string strPostDTime = null;
            string strFlagShow = null;
            string strFlagActive = null;
            {
                ////
                DAMst_Article_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strArticleNo // objArticleNo
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Article // dtDB_Mst_Article
                    );

                strAuthor = dtDB_Mst_Article.Rows[0]["Author"].ToString();
                strPostDTime = dtDB_Mst_Article.Rows[0]["PostDTime"].ToString();
                strFlagShow = dtDB_Mst_Article.Rows[0]["FlagShow"].ToString();
                strFlagActive = dtDB_Mst_Article.Rows[0]["FlagActive"].ToString();

                ////
                if (strArticleTitle == null || strArticleTitle.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strArticleTitle", strArticleTitle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_UpdateX_InvalidArticleTitle
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strArticleThemePath == null || strArticleThemePath.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strArticleThemePath", strArticleThemePath
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_UpdateX_InvalidArticleThemePath
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Save Temp Mst_Article:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_Article"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "ArticleNo"
                        , "ArticleTitle"
                        , "ArticleDesc"
                        , "ArticleThemePath"
                        , "Author"
                        , "PostDTime"
                        , "FlagShow"
                        , "FlagActive"
                        }
                    , new object[]{
                        new object[]{
                            strArticleNo // IF_InvInNo
                            , strArticleTitle
                            , strArticleDesc
                            , strArticleThemePath
                            , strAuthor
                            , strPostDTime
                            , strFlagShow
                            , strFlagActive
                            }
                        }
                    );

            }
            #endregion

            #region // Refine and Check Mst_ArticleDetail
            DataTable dtInput_Mst_ArticleDetail = null;
            {
                string strTableCheck = "Mst_ArticleDetail";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ArticleDetail = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_ArticleDetail.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ArticleDetail // dtData
                    , "StdParam", "ArticleNo" // arrstrCouple
                    , "int", "Idx" // arrstrCouple
                    , "", "ArticleContent" // arrstrCouple
                    , "", "ArticleImageName" // arrstrCouple
                    , "", "ArticleImagePath" // arrstrCouple
                    , "", "Title" // arrstrCouple
                    );
                ////
                for (int nScan = 0; nScan < dtInput_Mst_ArticleDetail.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ArticleDetail.Rows[nScan];
                    ////
                }
            }
            #endregion

            #region // Save Temp Mst_ArticleDetail
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ArticleDetail" // strTableName
                    , new object[] {
                            "ArticleNo", TConst.BizMix.Default_DBColType
                            , "Idx", TConst.BizMix.Default_DBColType
                            , "ArticleContent", TConst.BizMix.Default_DBColType
                            , "ArticleImageName", TConst.BizMix.Default_DBColType
                            , "ArticleImagePath", TConst.BizMix.Default_DBColType
                            , "Title", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ArticleDetail // dtData
                );
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                //// Clear All:
                string strSqlClear = CmUtils.StringUtils.Replace(@"
						---- Mst_ArticleDetail:
						delete 
						from Mst_ArticleDetail
						where ArticleNo = '@strArticleNo'
						;
						
						---- Mst_Article:
						delete 
						from Mst_Article
						where ArticleNo = '@strArticleNo'
						;
						
				"
                , "@strArticleNo", strArticleNo);
                ////
                DataSet dsExecClear = _cf.db.ExecQuery(strSqlClear);

                //// Insert:
                string strSqlInsert = CmUtils.StringUtils.Replace(@"
						---- Mst_Article:
						insert into Mst_Article(
							ArticleNo
							, ArticleTitle
							, ArticleDesc
							, ArticleThemePath
							, Author
							, PostDTime
							, FlagShow
							, FlagActive
						)
						select
							ArticleNo
							, ArticleTitle
							, ArticleDesc
							, ArticleThemePath
							, Author
							, PostDTime
							, FlagShow
							, FlagActive
						from #input_Mst_Article t --//[mylock]
						
						---- Mst_ArticleDetail:
						insert into Mst_ArticleDetail(
							ArticleNo
							, Idx
							, ArticleContent
							, ArticleImageName
							, ArticleImagePath
							, Title
						)
						select
							t.ArticleNo
							, t.Idx
							, t.ArticleContent
							, t.ArticleImageName
							, t.ArticleImagePath
							, t.Title
						from #input_Mst_ArticleDetail t --//[mylock]
					");

                DataSet dsExec = _cf.db.ExecQuery(strSqlInsert);
            }
            #endregion

            #region // Clear for Debug
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Article;
						drop table #input_Mst_ArticleDetail;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion
        }

        public DataSet DAMst_Article_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objArticleNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Article_Delete";
            string strErrorCodeDefault = TError.ErrDA.Mst_Article_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objArticleNo", objArticleNo
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Mst_Article_UpdateX:
                //DataSet dsGetData = null;
                {
                    DAMst_Article_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objArticleNo
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

        private void DAMst_Article_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objArticleNo
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_Article_DeleteX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objArticleNo", objArticleNo
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strArticleNo = TUtils.CUtils.StdParam(objArticleNo);

            ////
            DataTable dtDB_Mst_Article = null;
            {
                ////
                DAMst_Article_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strArticleNo // objArticleNo
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_Article // dtDB_Mst_Article
                    );
            }
            #endregion

            #region // SaveDB Mst_Tour:
            {
                //// Clear All:
                string strSqlClear = CmUtils.StringUtils.Replace(@"
						---- Mst_ArticleDetail:
						delete 
						from Mst_ArticleDetail
						where ArticleNo = '@strArticleNo'
						;
						
						---- Mst_Article:
						delete 
						from Mst_Article
						where ArticleNo = '@strArticleNo'
						;
						
				"
                , "@strArticleNo", strArticleNo);
                ////
                DataSet dsExecClear = _cf.db.ExecQuery(strSqlClear);
            }
            #endregion
        }
        #endregion

        #region // Mst_Article: WAS
        public DataSet WAS_DAMst_Article_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Article objRQ_Mst_Article
            ////
            , out DA_RT_Mst_Article objRT_Mst_Article
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Article.Tid;
            objRT_Mst_Article = new DA_RT_Mst_Article();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Article_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Article_Get;
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
                List<DA_Mst_Article> lst_Mst_Article = new List<DA_Mst_Article>();
                List<DA_Mst_ArticleDetail> lst_Mst_ArticleDetail = new List<DA_Mst_ArticleDetail>();
                bool bGet_Mst_Article = (objRQ_Mst_Article.Rt_Cols_Mst_Article != null && objRQ_Mst_Article.Rt_Cols_Mst_Article.Length > 0);
                bool bGet_Mst_ArticleDetail = (objRQ_Mst_Article.Rt_Cols_Mst_ArticleDetail != null && objRQ_Mst_Article.Rt_Cols_Mst_ArticleDetail.Length > 0);
                #endregion

                #region // WSMst_Article_Get:
                mdsResult = DAMst_Article_Get(
                    objRQ_Mst_Article.Tid // strTid
                    , objRQ_Mst_Article.GwUserCode // strGwUserCode
                    , objRQ_Mst_Article.GwPassword // strGwPassword
                    , objRQ_Mst_Article.WAUserCode // strUserCode
                    , objRQ_Mst_Article.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Article.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Article.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Article.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_Mst_Article.Rt_Cols_Mst_Article // strRt_Cols_Sys_User
                    , objRQ_Mst_Article.Rt_Cols_Mst_ArticleDetail // strRt_Cols_Sys_User
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Article.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Article)
                    {
                        DataTable dt_Mst_Article = mdsResult.Tables["Mst_Article"].Copy();
                        lst_Mst_Article = TUtils.DataTableCmUtils.ToListof<DA_Mst_Article>(dt_Mst_Article);
                        objRT_Mst_Article.Lst_Mst_Article = lst_Mst_Article;
                    }
                    ////
                    if (bGet_Mst_ArticleDetail)
                    {
                        DataTable dt_Mst_ArticleDetail = mdsResult.Tables["Mst_ArticleDetail"].Copy();
                        lst_Mst_ArticleDetail = TUtils.DataTableCmUtils.ToListof<DA_Mst_ArticleDetail>(dt_Mst_ArticleDetail);
                        objRT_Mst_Article.Lst_Mst_ArticleDetail = lst_Mst_ArticleDetail;
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

        public DataSet WAS_DAMst_Article_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Article objRQ_Mst_Article
            ////
            , out DA_RT_Mst_Article objRT_Mst_Article
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Article.Tid;
            objRT_Mst_Article = new DA_RT_Mst_Article();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Article_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Article_Create;
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
                    if (objRQ_Mst_Article.Lst_Mst_ArticleDetail == null)
                        objRQ_Mst_Article.Lst_Mst_ArticleDetail = new List<DA_Mst_ArticleDetail>();
                    {
                        DataTable dt_Mst_ArticleDetail = TUtils.DataTableCmUtils.ToDataTable<DA_Mst_ArticleDetail>(objRQ_Mst_Article.Lst_Mst_ArticleDetail, "Mst_ArticleDetail");
                        dsData.Tables.Add(dt_Mst_ArticleDetail);
                    }
                }
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAMst_Article_Create(
                    objRQ_Mst_Article.Tid // strTid
                    , objRQ_Mst_Article.GwUserCode // strGwUserCode
                    , objRQ_Mst_Article.GwPassword // strGwPassword
                    , objRQ_Mst_Article.WAUserCode // strUserCode
                    , objRQ_Mst_Article.WAUserPassword // strUserPassword
                                                       ////
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// 
                    , objRQ_Mst_Article.Mst_Article.ArticleNo
                    , objRQ_Mst_Article.Mst_Article.ArticleTitle
                    , objRQ_Mst_Article.Mst_Article.ArticleDesc
                    , objRQ_Mst_Article.Mst_Article.ArticleThemePath
                    ////
                    , dsData
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

        public DataSet WAS_DAMst_Article_Update(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Article objRQ_Mst_Article
            ////
            , out DA_RT_Mst_Article objRT_Mst_Article
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Article.Tid;
            objRT_Mst_Article = new DA_RT_Mst_Article();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Article_Update";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Article_Update;
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
                    if (objRQ_Mst_Article.Lst_Mst_ArticleDetail == null)
                        objRQ_Mst_Article.Lst_Mst_ArticleDetail = new List<DA_Mst_ArticleDetail>();
                    {
                        DataTable dt_Mst_ArticleDetail = TUtils.DataTableCmUtils.ToDataTable<DA_Mst_ArticleDetail>(objRQ_Mst_Article.Lst_Mst_ArticleDetail, "Mst_ArticleDetail");
                        dsData.Tables.Add(dt_Mst_ArticleDetail);
                    }
                }
                #endregion

                #region // WS_POW_FAQ_Get:
                mdsResult = DAMst_Article_Update(
                    objRQ_Mst_Article.Tid // strTid
                    , objRQ_Mst_Article.GwUserCode // strGwUserCode
                    , objRQ_Mst_Article.GwPassword // strGwPassword
                    , objRQ_Mst_Article.WAUserCode // strUserCode
                    , objRQ_Mst_Article.WAUserPassword // strUserPassword
                                                       ////
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// 
                    , objRQ_Mst_Article.Mst_Article.ArticleNo
                    , objRQ_Mst_Article.Mst_Article.ArticleTitle
                    , objRQ_Mst_Article.Mst_Article.ArticleDesc
                    , objRQ_Mst_Article.Mst_Article.ArticleThemePath
                    ////
                    , dsData
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

        public DataSet WAS_DAMst_Article_Delete(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Mst_Article objRQ_Mst_Article
            ////
            , out DA_RT_Mst_Article objRT_Mst_Article
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Article.Tid;
            objRT_Mst_Article = new DA_RT_Mst_Article();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Mst_Article_Delete";
            string strErrorCodeDefault = TError.ErrDA.WAS_Mst_Article_Delete;
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

                #region // WS_POW_FAQ_Get:
                mdsResult = DAMst_Article_Delete(
                    objRQ_Mst_Article.Tid // strTid
                    , objRQ_Mst_Article.GwUserCode // strGwUserCode
                    , objRQ_Mst_Article.GwPassword // strGwPassword
                    , objRQ_Mst_Article.WAUserCode // strUserCode
                    , objRQ_Mst_Article.WAUserPassword // strUserPassword
                                                       ////
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// 
                    , objRQ_Mst_Article.Mst_Article.ArticleNo
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
