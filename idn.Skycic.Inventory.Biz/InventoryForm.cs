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
        #region // InvF_InventoryInFG:
        public void InvF_InventoryInFG_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objIF_InvInFGNo
            , string strFlagExistToCheck
            , string strStatusListToCheck
            , out DataTable dtDB_InvF_InventoryInFG
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from InvF_InventoryInFG t --//[mylock]
					where (1=1)
						and t.IF_InvInFGNo = @objIF_InvInFGNo
					;
				");
            dtDB_InvF_InventoryInFG = _cf.db.ExecQuery(
                strSqlExec
                , "@objIF_InvInFGNo", objIF_InvInFGNo
                ).Tables[0];
            dtDB_InvF_InventoryInFG.TableName = "InvF_InventoryInFG";

            // strFlagExistToCheck
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_InvF_InventoryInFG.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvInFGNo", objIF_InvInFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_CheckDB_InvInFGNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_InvF_InventoryInFG.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvInFGNo", objIF_InvInFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_CheckDB_InvInFGNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_InvF_InventoryInFG.Rows[0]["IF_InvInFGStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.IF_InvInFGNo", objIF_InvInFGNo
                    , "Check.strFlagActiveListToCheck", strStatusListToCheck
                    , "DB.IF_InvInFGStatus", dtDB_InvF_InventoryInFG.Rows[0]["IF_InvInFGStatus"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.InvF_InventoryInFG_CheckDB_StatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public void InvF_InventoryInFG_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_InventoryInFG
            , string strRt_Cols_InvF_InventoryInFGDtl
            , string strRt_Cols_InvF_InventoryInFGInstSerial
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_InventoryInFG_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_InventoryInFG", strRt_Cols_InvF_InventoryInFG
                , "strRt_Cols_InvF_InventoryInFGDtl", strRt_Cols_InvF_InventoryInFGDtl
                , "strRt_Cols_InvF_InventoryInFGInstSerial", strRt_Cols_InvF_InventoryInFGInstSerial
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_InventoryInFG = (strRt_Cols_InvF_InventoryInFG != null && strRt_Cols_InvF_InventoryInFG.Length > 0);
            bool bGet_InvF_InventoryInFGDtl = (strRt_Cols_InvF_InventoryInFGDtl != null && strRt_Cols_InvF_InventoryInFGDtl.Length > 0);
            bool bGet_InvF_InventoryInFGInstSerial = (strRt_Cols_InvF_InventoryInFGInstSerial != null && strRt_Cols_InvF_InventoryInFGInstSerial.Length > 0);
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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_InvF_InventoryInFG_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiif.IF_InvInFGNo
							, iiif.CreateDTimeUTC
						into #tbl_InvF_InventoryInFG_Filter_Draft
						from InvF_InventoryInFG iiif --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiif.MST = t_MstNNT_View.MST
							inner join InvF_InventoryInFGDtl iiifdt --//[mylock]
								on iiif.IF_InvInFGNo = iiifdt.IF_InvInFGNo
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiif.CreateDTimeUTC desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_InventoryInFG_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_InventoryInFG_Filter:
						select
							t.*
						into #tbl_InvF_InventoryInFG_Filter
						from #tbl_InvF_InventoryInFG_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_InventoryInFG ------:
						zzB_Select_InvF_InventoryInFG_zzE
						--------------------------------

						-------- InvF_InventoryInFGDtl ------:
						zzB_Select_InvF_InventoryInFGDtl_zzE
						--------------------------------------

						-------- InvF_InventoryInFGInstSerial ------:
						zzB_Select_InvF_InventoryInFGInstSerial_zzE
						--------------------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_InventoryInFG_Filter_Draft;
						--drop table #tbl_InvF_InventoryInFG_Filter;
					"
                );
            ////
            string zzB_Select_InvF_InventoryInFG_zzE = "-- Nothing.";
            if (bGet_InvF_InventoryInFG)
            {
                #region // bGet_InvF_InventoryInFG:
                zzB_Select_InvF_InventoryInFG_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryInFG:
							select
								t.MyIdxSeq
								, iiif.*
							from #tbl_InvF_InventoryInFG_Filter t --//[mylock]
								inner join InvF_InventoryInFG iiif --//[mylock]
									on t.IF_InvInFGNo = iiif.IF_InvInFGNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryInFGDtl_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryInFGDtl)
            {
                #region // bGet_InvF_InventoryInFGDtl:
                zzB_Select_InvF_InventoryInFGDtl_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryInFGDtl:
							select
								t.MyIdxSeq
								, iiifdt.*
							from #tbl_InvF_InventoryInFG_Filter t --//[mylock]
								inner join InvF_InventoryInFG iiif --//[mylock]
									on t.IF_InvInFGNo = iiif.IF_InvInFGNo
								inner join InvF_InventoryInFGDtl iiifdt --//[mylock]
									on t.IF_InvInFGNo = iiifdt.IF_InvInFGNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryInFGInstSerial_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryInFGInstSerial)
            {
                #region // bGet_InvF_InventoryInFGInstSerial:
                zzB_Select_InvF_InventoryInFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryInFGDtl:
							select
								t.MyIdxSeq
								, iiifs.*
							from #tbl_InvF_InventoryInFG_Filter t --//[mylock]
								inner join InvF_InventoryInFG iiif --//[mylock]
									on t.IF_InvInFGNo = iiif.IF_InvInFGNo
								inner join InvF_InventoryInFGInstSerial iiifs --//[mylock]
									on t.IF_InvInFGNo = iiifs.IF_InvInFGNo
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
                        , "InvF_InventoryInFG" // strTableNameDB
                        , "InvF_InventoryInFG." // strPrefixStd
                        , "iiif." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "InvF_InventoryInFGDtl" // strTableNameDB
                        , "InvF_InventoryInFGDtl." // strPrefixStd
                        , "iiifdt." // strPrefixAlias
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
                , "zzB_Select_InvF_InventoryInFG_zzE", zzB_Select_InvF_InventoryInFG_zzE
                , "zzB_Select_InvF_InventoryInFGDtl_zzE", zzB_Select_InvF_InventoryInFGDtl_zzE
                , "zzB_Select_InvF_InventoryInFGInstSerial_zzE", zzB_Select_InvF_InventoryInFGInstSerial_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_InventoryInFG)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryInFG";
            }
            if (bGet_InvF_InventoryInFGDtl)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryInFGDtl";
            }
            if (bGet_InvF_InventoryInFGInstSerial)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryInFGInstSerial";
            }
            #endregion
        }
        public DataSet InvF_InventoryInFG_Get(
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
            , string strRt_Cols_InvF_InventoryInFG
            , string strRt_Cols_InvF_InventoryInFGDtl
            , string strRt_Cols_InvF_InventoryInFGInstSerial
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryInFG_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryInFG_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_InvF_InventoryInFG", strRt_Cols_InvF_InventoryInFG
                , "strRt_Cols_InvF_InventoryInFGDtl", strRt_Cols_InvF_InventoryInFGDtl
                , "strRt_Cols_InvF_InventoryInFGInstSerial", strRt_Cols_InvF_InventoryInFGInstSerial
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_InventoryInFG_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryInFG_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                        ////
                        , strRt_Cols_InvF_InventoryInFG // strRt_Cols_InvF_InventoryInFG
                        , strRt_Cols_InvF_InventoryInFGDtl // strRt_Cols_InvF_InventoryInFGDtl
                        , strRt_Cols_InvF_InventoryInFGInstSerial // strRt_Cols_InvF_InventoryInFGInstSerial
                        /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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
        public DataSet WAS_InvF_InventoryInFG_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG
            ////
            , out RT_InvF_InventoryInFG objRT_InvF_InventoryInFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryInFG.Tid;
            objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryInFG_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryInFG_Get;
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
                List<InvF_InventoryInFG> lst_InvF_InventoryInFG = new List<InvF_InventoryInFG>();
                List<InvF_InventoryInFGDtl> lst_InvF_InventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
                List<InvF_InventoryInFGInstSerial> lst_InvF_InventoryInFGInstSerial = new List<InvF_InventoryInFGInstSerial>();
                /////
                bool bGet_InvF_InventoryInFG = (objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG != null && objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG.Length > 0);
                bool bGet_InvF_InventoryInFGDtl = (objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl != null && objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl.Length > 0);
                bool bGet_InvF_InventoryInFGInstSerial = (objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGInstSerial != null && objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGInstSerial.Length > 0);
                #endregion

                #region // WS_InvF_InventoryInFG_Get:
                mdsResult = InvF_InventoryInFG_Get(
                    objRQ_InvF_InventoryInFG.Tid // strTid
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryInFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryInFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryInFG.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_InventoryInFG.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_InventoryInFG.Ft_WhereClause // strFt_WhereClause
                                                           //// Return:
                    , objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFG // strRt_Cols_InvF_InventoryInFG
                    , objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGDtl // Rt_Cols_InvF_InventoryInFGDtl
                    , objRQ_InvF_InventoryInFG.Rt_Cols_InvF_InventoryInFGInstSerial // Rt_Cols_InvF_InventoryInFGInstSerial
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_InventoryInFG.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    if (bGet_InvF_InventoryInFG)
                    {
                        ////
                        DataTable dt_InvF_InventoryInFG = mdsResult.Tables["InvF_InventoryInFG"].Copy();
                        lst_InvF_InventoryInFG = TUtils.DataTableCmUtils.ToListof<InvF_InventoryInFG>(dt_InvF_InventoryInFG);
                        objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFG = lst_InvF_InventoryInFG;
                    }
                    ////
                    if (bGet_InvF_InventoryInFGDtl)
                    {
                        ////
                        DataTable dt_InvF_InventoryInFGDtl = mdsResult.Tables["InvF_InventoryInFGDtl"].Copy();
                        lst_InvF_InventoryInFGDtl = TUtils.DataTableCmUtils.ToListof<InvF_InventoryInFGDtl>(dt_InvF_InventoryInFGDtl);
                        objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl = lst_InvF_InventoryInFGDtl;
                    }
                    ////
                    if (bGet_InvF_InventoryInFGInstSerial)
                    {
                        ////
                        DataTable dt_InvF_InventoryInFGInstSerial = mdsResult.Tables["InvF_InventoryInFGInstSerial"].Copy();
                        lst_InvF_InventoryInFGInstSerial = TUtils.DataTableCmUtils.ToListof<InvF_InventoryInFGInstSerial>(dt_InvF_InventoryInFGInstSerial);
                        objRT_InvF_InventoryInFG.Lst_InvF_InventoryInFGInstSerial = lst_InvF_InventoryInFGInstSerial;
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
        private void InvF_InventoryInFG_SaveX(
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
            , object objIF_InvInFGNo
            , object objMST
            , object objInvInType
            , object objFormInType
            , object objInvCode
            , object objPMType
            , object objRemark
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryInFG_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvInFGNo", objIF_InvInFGNo
                , "objMST", objMST
                , "objInvInType", objInvInType
                , "objFormInType", objFormInType
                , "objInvCode", objInvCode
                , "objPMType", objPMType
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryInFG:
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strIF_InvInFGNo = TUtils.CUtils.StdParam(objIF_InvInFGNo);
            string strInvInType = TUtils.CUtils.StdParam(objInvInType);
            string strFormInType = TUtils.CUtils.StdParam(objFormInType);
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strInvCode = TUtils.CUtils.StdParam(objInvCode);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryInFG = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            {
                ////
                if(string.IsNullOrEmpty(strIF_InvInFGNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvInFGNo", strIF_InvInFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Save_InvalidIF_InvInFGNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryInFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvInFGNo // objInvoiceCode
                    , "" // strFlagExistToCheck
                    , "" // strInvoiceStatusListToCheck
                    , out dtDB_InvF_InventoryInFG // dtDB_Invoice_Invoice
                    );
                ////
                if (dtDB_InvF_InventoryInFG.Rows.Count < 1) // Chưa Tồn tại.
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công.
                    }
                    else
                    {
                        // 
                        strCreateDTimeUTC = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strCreateBy = strWAUserCode;
                    }
                }
                else // Đã Tồn tại.
                {
                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryInFG.Rows[0]["IF_InvInFGStatus"], TConst.IF_InvInFGStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvInFGStatus", dtDB_InvF_InventoryInFG.Rows[0]["IF_InvInFGStatus"]
                            , "Check.IF_InvInFGStatus.Expected", TConst.IF_InvInFGStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryInFG_Save_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryInFG.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryInFG.Rows[0]["CreateBy"]);
                    ////
                }
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.DB.NNT.MST", drAbilityOfUser["strMST"]
                        , "Check.strMST", strMST
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Save_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////

                DataTable dtDB_Mst_NNT = new DataTable();
                if (string.IsNullOrEmpty(strFormInType)) strFormInType = TConst.FormInType.KhongMaVach;
            }
            #endregion

            #region // SaveTemp InvF_InventoryInFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryInFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvInFGNo"
                        , "NetworkID"
                        , "FormInType"
                        , "InvInType"
                        , "MST"
                        , "InvCode"
                        , "PMType"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvInFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvInFGNo, // IF_InvInFGNo
                                nNetworkID, // NetworkID
								strFormInType, // FormInType
								strInvInType, // InvInType
								strMST, // MST
								strInvCode, // InvCode
								strPMType, // PMType
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // CreateDTimeUTC
                                strWAUserCode, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                null, // ApprDTimeUTC
                                null, // ApprBy
                                TConst.IF_InvInFGStatus.Pending, // IF_InvInFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryInFGDtl:
            ////
            DataTable dtInput_InvF_InventoryInFGDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInFGDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInFGDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryInFGDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInFGDtl // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "StdDate", "ProductionDate" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGDtl, "IF_InvInFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGDtl, "IF_InvInFGStatusDtl", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInFGDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInFGDtl.Rows[nScan];
                    ////
                    drScan["IF_InvInFGNo"] = strIF_InvInFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvInFGStatusDtl"] = TConst.IF_InvInFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryInFGDtl:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInFGDtl" // strTableName
                    , new object[] {
                            "IF_InvInFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "ProductionDate", TConst.BizMix.Default_DBColType
                            , "IF_InvInFGStatusDtl", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInFGDtl // dtData
                );
            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryInFGInstSerial:
            ////
            DataTable dtInput_InvF_InventoryInFGInstSerial = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInFGInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Save_InvF_InventoryInFGInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInFGInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInFGInstSerial // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    , "", "ProductionDate" // arrstrCouple
                    , "", "PackageDate" // arrstrCouple
                    , "StdParam", "AgentCode" // arrstrCouple
                    , "StdParam", "PartLotNo" // arrstrCouple
                    , "StdParam", "ShiftInCode" // arrstrCouple
                    , "", "PrintDate" // arrstrCouple
                    , "StdParam", "CanNo" // arrstrCouple
                    , "StdParam", "BoxNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGInstSerial, "IF_InvInFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGInstSerial, "IF_InvInFGISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInFGInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInFGInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInFGInstSerial.Rows[nScan];
                    ////
                    //string strVATRateCode = TUtils.CUtils.StdParam(drScan["VATRateCode"]);
                    ////
                    drScan["IF_InvInFGNo"] = strIF_InvInFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvInFGISStatus"] = TConst.IF_InvInFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryInFGInstSerial:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInFGInstSerial" // strTableName
                    , new object[] {
                            "IF_InvInFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "ProductionDate", TConst.BizMix.Default_DBColType
                            , "PackageDate", TConst.BizMix.Default_DBColType
                            , "AgentCode", TConst.BizMix.Default_DBColType
                            , "PartLotNo", TConst.BizMix.Default_DBColType
                            , "ShiftInCode", TConst.BizMix.Default_DBColType
                            , "PrintDate", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "BoxNo", TConst.BizMix.Default_DBColType
                            , "IF_InvInFGISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInFGInstSerial // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInFGInstSerial:
                            select 
                                t.IF_InvInFGNo
                                , t.PartCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryInFGInstSerial
                            from InvF_InventoryInFGInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryInFG f --//[mylock]
		                            on t.IF_InvInFGNo = f.IF_InvInFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInFGInstSerial:
                            delete t 
                            from InvF_InventoryInFGInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryInFGInstSerial f --//[mylock]
		                            on t.IF_InvInFGNo = f.IF_InvInFGNo
		                                and t.PartCode = f.PartCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryInFGDtl:
                            select 
                                t.IF_InvInFGNo
                                , t.PartCode
                            into #tbl_InvF_InventoryInFGDtl
                            from InvF_InventoryInFGDtl t --//[mylock]
	                            inner join #input_InvF_InventoryInFG f --//[mylock]
		                            on t.IF_InvInFGNo = f.IF_InvInFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInFGDtl:
                            delete t 
                            from InvF_InventoryInFGDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryInFGDtl f --//[mylock]
		                            on t.IF_InvInFGNo = f.IF_InvInFGNo
		                                and t.PartCode = f.PartCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryInFG:
                            delete t
                            from InvF_InventoryInFG t --//[mylock]
	                            inner join #input_InvF_InventoryInFG f --//[mylock]
		                            on t.IF_InvInFGNo = f.IF_InvInFGNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryInFGInstSerial;
                            drop table #tbl_InvF_InventoryInFGDtl;
							");
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {
                        ////
                        string zzzzClauseInsert_InvF_InventoryInFG_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInFG:                                
                                insert into InvF_InventoryInFG(
	                                IF_InvInFGNo
	                                , NetworkID
	                                , MST
	                                , InvInType
	                                , FormInType
	                                , InvCode
	                                , PMType
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , IF_InvInFGStatus
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInFGNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.InvInType
	                                , t.FormInType
	                                , t.InvCode
	                                , t.PMType
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.IF_InvInFGStatus
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInFG t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInFGDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInFG:                                
                                insert into InvF_InventoryInFGDtl(
	                                IF_InvInFGNo 
	                                , PartCode
	                                , NetworkID
	                                , Qty
	                                , ProductionDate
	                                , IF_InvInFGStatusDtl
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInFGNo 
	                                , t.PartCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.ProductionDate
	                                , t.IF_InvInFGStatusDtl
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInFGDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInFGInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInFGInstSerial:                                
                                insert into InvF_InventoryInFGInstSerial(
	                                IF_InvInFGNo 
	                                , PartCode
	                                , SerialNo
	                                , NetworkID
	                                , ProductionDate
	                                , PackageDate
	                                , AgentCode
	                                , PartLotNo
	                                , ShiftInCode
	                                , PrintDate
	                                , CanNo
	                                , BoxNo
	                                , IF_InvInFGISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInFGNo 
	                                , t.PartCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.ProductionDate
	                                , t.PackageDate
	                                , t.AgentCode
	                                , t.PartLotNo
	                                , t.ShiftInCode
	                                , t.PrintDate
	                                , t.CanNo
	                                , t.BoxNo
	                                , t.IF_InvInFGISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInFGInstSerial t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryInFG_zSave			
								----
								zzzzClauseInsert_InvF_InventoryInFGDtl_zSave			
								----
								zzzzClauseInsert_InvF_InventoryInFGInstSerial_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryInFG_zSave", zzzzClauseInsert_InvF_InventoryInFG_zSave
                            , "zzzzClauseInsert_InvF_InventoryInFGDtl_zSave", zzzzClauseInsert_InvF_InventoryInFGDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryInFGInstSerial_zSave", zzzzClauseInsert_InvF_InventoryInFGInstSerial_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region //// Clear For Debug:.
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryInFG;
						drop table #input_InvF_InventoryInFGDtl;
						drop table #input_InvF_InventoryInFGInstSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            else
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryInFG;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////

            }
            #endregion


        // Return Good:
        MyCodeLabel_Done:
            return;

        }
        public DataSet InvF_InventoryInFG_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            ////
            , object objIF_InvInFGNo
            , object objMST
            , object objInvInType
            , object objFormInType
            , object objInvCode
            , object objPMType
            , object objRemark
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryInFG_Save";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryInFG_Save;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
			        ////
                    , "objIF_InvInFGNo", objIF_InvInFGNo
                    , "objMST", objMST
                    , "objInvInType", objInvInType
                    , "objFormInType", objFormInType
                    , "objInvCode", objInvCode
                    , "objPMType", objPMType
                    , "objRemark", objRemark
                    ////
                    //, "objTInvoiceFilePathXML", objTInvoiceFilePathXML
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

                #region // Temp_PrintTemp_SaveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryInFG_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                                          ////
                        , objIF_InvInFGNo // objIF_InvInFGNo
                        , objMST // objMST
                        , objInvInType // objInvInType
                        , TConst.FormInType.KhongMaVach // objFormInType
                        , objInvCode // objInvCode
                        , objPMType // objPMType
                        , objRemark // objRemark
                                    ////
                        , dsData  // dsData
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

        public DataSet WAS_InvF_InventoryInFG_Save(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG
            ////
            , out RT_InvF_InventoryInFG objRT_InvF_InventoryInFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryInFG.Tid;
            objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryInFG_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryInFG_Save;
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
                //List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl == null)
                        objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl = new List<InvF_InventoryInFGDtl>();
                    {
                        DataTable dt_InvF_InventoryInFGDtl = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInFGDtl>(objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGDtl, "InvF_InventoryInFGDtl");
                        dsData.Tables.Add(dt_InvF_InventoryInFGDtl);
                    }
                    ////
                    if (objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGInstSerial == null)
                        objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGInstSerial = new List<InvF_InventoryInFGInstSerial>();
                    {
                        DataTable dt_InvF_InventoryInFGInstSerial = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInFGInstSerial>(objRQ_InvF_InventoryInFG.Lst_InvF_InventoryInFGInstSerial, "InvF_InventoryInFGInstSerial");
                        dsData.Tables.Add(dt_InvF_InventoryInFGInstSerial);
                    }
                    ////
                }
                #endregion

                #region // InvF_InventoryInFG_Save:
                mdsResult = InvF_InventoryInFG_Save(
                    objRQ_InvF_InventoryInFG.Tid // strTid
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryInFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryInFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryInFG.FlagIsDelete // objFlagIsDelete
                                                        /////
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.IF_InvInFGNo // objIF_InvInFGNo
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.MST // objMST
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.InvInType // objInvInType
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.FormInType // objFormInType
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.InvCode // objInvCode
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.PMType // objPMType
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.Remark // objRemark
                    ////
                    , dsData
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
        private void InvF_InventoryInFG_ApproveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objIF_InvInFGNo
            , object objFormInType
            , object objRemark
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryInFG_ApproveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvInFGNo", objIF_InvInFGNo
                , "objFormInType", objFormInType
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryInFG:
            ////
            string strIF_InvInFGNo = TUtils.CUtils.StdParam(objIF_InvInFGNo);
            string strFormInType = TUtils.CUtils.StdParam(objFormInType);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryInFG = null;
            {
                /////
                InvF_InventoryInFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvInFGNo // objInvoiceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvInFGStatus.Pending // strIF_InvInFGStatusListToCheck
                    , out dtDB_InvF_InventoryInFG // dtDB_Invoice_Invoice
                    );
                ////
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], dtDB_InvF_InventoryInFG.Rows[0]["MST"]))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                        , "Check.InvF_InventoryInFG.MST",  dtDB_InvF_InventoryInFG.Rows[0]["MST"]
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryInFG_Approve_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryInFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryInFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvInFGNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvInFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvInFGNo, // IF_InvInFGNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvInFGStatus.Approve, // IF_InvInFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_InvF_InventoryInFG_ClauseSet_zzE = @"
                        t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
                        , t.Remark = f.Remark
                        , t.ApprDTimeUTC = f.ApprDTimeUTC
                        , t.ApprBy = f.ApprBy
                        , t.IF_InvInFGStatus = f.IF_InvInFGStatus
                        ";
                ////
                string zzB_Update_InvF_InventoryInFGDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvInFGStatusDtl = f.IF_InvInFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryInFGInstSerial_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvInFGISStatus = f.IF_InvInFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryInFG_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryInFG:
						update t
						set 
							zzB_Update_InvF_InventoryInFG_ClauseSet_zzE
						from InvF_InventoryInFG t --//[mylock]
							inner join #input_InvF_InventoryInFG f --//[mylock]
								on t.IF_InvInFGNo = f.IF_InvInFGNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryInFG_ClauseSet_zzE", zzB_Update_InvF_InventoryInFG_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryInFGDtl_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInFGDtl_Temp: 
                            select 
                                t.IF_InvInFGNo
                                , t.PartCode
                                , f.IF_InvInFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryInFGDtl_Temp
							from InvF_InventoryInFGDtl t --//[mylock]
							    inner join #input_InvF_InventoryInFG f --//[mylock]
								    on t.IF_InvInFGNo = f.IF_InvInFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryInFGDtl_ClauseSet_zzE
							from InvF_InventoryInFGDtl t --//[mylock]
							    inner join #tbl_InvF_InventoryInFGDtl_Temp f --//[mylock]
								    on t.IF_InvInFGNo = f.IF_InvInFGNo
								        and t.PartCode = f.PartCode
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryInFGDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryInFGDtl_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryInFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInFGInstSerial_Temp: 
                            select 
                                t.IF_InvInFGNo
                                , t.PartCode
                                , t.SerialNo
                                , f.IF_InvInFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryInFGInstSerial_Temp
							from InvF_InventoryInFGInstSerial t --//[mylock]
							    inner join #input_InvF_InventoryInFG f --//[mylock]
								    on t.IF_InvInFGNo = f.IF_InvInFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryInFGInstSerial_ClauseSet_zzE
							from InvF_InventoryInFGInstSerial t --//[mylock]
							    inner join #tbl_InvF_InventoryInFGInstSerial_Temp f --//[mylock]
								    on t.IF_InvInFGNo = f.IF_InvInFGNo
								        and t.PartCode = f.PartCode
								        and t.SerialNo = f.SerialNo
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryInFGInstSerial_ClauseSet_zzE", zzB_Update_InvF_InventoryInFGInstSerial_ClauseSet_zzE
                    );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryInFG_zzE
                        ----
						zzB_Update_InvF_InventoryInFGDtl_zzE
                        ----
						zzB_Update_InvF_InventoryInFGInstSerial_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryInFG_zzE", zzB_Update_InvF_InventoryInFG_zzE
                    , "zzB_Update_InvF_InventoryInFGDtl_zzE", zzB_Update_InvF_InventoryInFGDtl_zzE
                    , "zzB_Update_InvF_InventoryInFGInstSerial_zzE", zzB_Update_InvF_InventoryInFGInstSerial_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            #region // Inv_InventoryTransaction_Perform:
            {
                ////
                if (string.IsNullOrEmpty(strFormInType)) strFormInType = TConst.FormInType.MaVach;
                ////
                if (CmUtils.StringUtils.StringEqualIgnoreCase(TConst.FormInType.KhongMaVach, strFormInType))
                {
                    InvFInventory_InventoryTransaction_Exec(
                        strFunctionName // zzzzClauseCol_FunctionName
                        );
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(TConst.FormInType.MaVach, strFormInType))
                {
                    InvFInventory_InventoryTransaction_Exec__AddSerialActual(
                        strFunctionName // zzzzClauseCol_FunctionName
                        );
                }
                ////
                ///
                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // alParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Add // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            // Return Good:
            return;
        }

        public DataSet InvF_InventoryInFG_Approve(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objIF_InvInFGNo
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryInFG_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryInFG_Approve;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objIF_InvInFGNo", objIF_InvInFGNo
                    , "objRemark", objRemark
                    ////
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

                #region // InvF_InventoryInFG_ApproveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryInFG_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objIF_InvInFGNo // objIF_InvInFGNo
                        , TConst.FormInType.KhongMaVach // 
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

        public DataSet WAS_InvF_InventoryInFG_Approve(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG
            ////
            , out RT_InvF_InventoryInFG objRT_InvF_InventoryInFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryInFG.Tid;
            objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryInFG_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryInFG_Approve;
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

                #region // InvF_InventoryInFG_Approve:
                mdsResult = InvF_InventoryInFG_Approve(
                    objRQ_InvF_InventoryInFG.Tid // strTid
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryInFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryInFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.IF_InvInFGNo // objIF_InvInFGNo
                    , objRQ_InvF_InventoryInFG.InvF_InventoryInFG.Remark // objRemark
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
        public void InvFInventory_InventoryTransaction_Exec(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiifg.InvCode InvCode
						, ifiifgdt.PartCode PartCode
						, ifiifgdt.NetworkID NetworkID
						, ifiifg.MST MST
						, 0.0 QtyChTotalOK
						, 0.0 QtyChBlockOK
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG
						, ifiifgdt.Qty QtyPlanChTotal
						, 0.0 QtyPlanChBlock
						, t.LUDTimeUTC CreateDTimeUTC
 						, t.LUBy CreateBy
						, ifiifgdt.Qty QtyPlan
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYINFGDTL' RefType
 						, ifiifg.IF_InvInFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryInFG t --//[mylock]
						inner join InvF_InventoryInFG ifiifg --//[mylock]
							on t.IF_InvInFGNo = ifiifg.IF_InvInFGNo
						inner join InvF_InventoryInFGDtl ifiifgdt --//[mylock]
							on ifiifg.IF_InvInFGNo = ifiifgdt.IF_InvInFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];						

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiifg.InvCode InvCode
						, ifiifgdt.PartCode PartCode
						, ifiifgis.SerialNo SerialNo
						, ifiifgdt.NetworkID NetworkID
						, ifiifg.MST MST
						, ifiifg.PMType PMType
						--, ifiifgis.SerialNo SerialNo_Actual
						, ifiifgis.SerialNo SecretNo
						, ifiifgis.PartLotNo PartLotNo
						, ifiifgis.BoxNo
						, ifiifgis.CanNo
						, ifiifgis.AgentCode
						, null WarrantyDateStart
						, ifiifgis.PackageDate PackageDate
						, ifiifgis.ProductionDate ProductionDate
						, ifiifgis.LogLUBy UserBox
						, ifiifgis.LogLUBy UserCan
						, null UserKCS
						, null UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiifgis.IF_InvInFGISStatus = 'APPROVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, '1' FlagMap
						, '0' FlagSales
						, (
							case
								when ifiifgis.BoxNo is null then '0'
								else '1'
							end 	
						) FlagBox
						, (
							case
								when ifiifgis.CanNo is null then '0'	
								else '1'
							end 	
						) FlagCan
						, '0' FlagUI
						, ifiifg.FormInType FormInType
						, ifiifgis.IF_InvInFGNo IF_InvInFGNo
						, null FormOutType
						, null IF_InvOutFGNo
						, t.LUDTimeUTC LUDTimeUTC
						, t.LUBy LUBy
						, t.LUDTimeUTC CreateDTimeUTC
						, t.LUBy CreateBy
						, null InvDTime
						, null InvBy
						, null BoxDTime
						, null BoxBy
						, null CanDTime
						, null CanBy
						, null OutDTime
						, null OutBy
						, null RefNo_Type
						, null RefNo_PK
						, ifiifgis.ShiftInCode ShiftInCode
						, ifiifgis.PrintDate PrintDate
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYINFGINSTSERIAL' RefType
						, ifiifg.IF_InvInFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LUDTimeUTC LogLUDTime
						, t.LUBy LogLUBy
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryInFG t --//[mylock]
						inner join InvF_InventoryInFG ifiifg --//[mylock]
							on t.IF_InvInFGNo = ifiifg.IF_InvInFGNo
						inner join InvF_InventoryInFGDtl ifiifgdt --//[mylock]
							on ifiifg.IF_InvInFGNo = ifiifgdt.IF_InvInFGNo
						inner join InvF_InventoryInFGInstSerial ifiifgis --//[mylock]
							on ifiifgdt.IF_InvInFGNo = ifiifgis.IF_InvInFGNo
								and  ifiifgdt.PartCode = ifiifgis.PartCode
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );

        }

        public void InvFInventory_InventoryTransaction_Exec__AddSerialActual(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiifg.InvCode InvCode
						, ifiifgdt.PartCode PartCode
						, ifiifgdt.NetworkID NetworkID
						, ifiifg.MST MST
						, ifiifgdt.Qty QtyChTotalOK
						, 0.0 QtyChBlockOK
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG
						, ifiifgdt.Qty QtyPlanChTotal
						, 0.0 QtyPlanChBlock
						, t.LUDTimeUTC CreateDTimeUTC
 						, t.LUBy CreateBy
						, ifiifgdt.Qty QtyPlan
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYINFGDTL' RefType
 						, ifiifg.IF_InvInFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryInFG t --//[mylock]
						inner join InvF_InventoryInFG ifiifg --//[mylock]
							on t.IF_InvInFGNo = ifiifg.IF_InvInFGNo
						inner join InvF_InventoryInFGDtl ifiifgdt --//[mylock]
							on ifiifg.IF_InvInFGNo = ifiifgdt.IF_InvInFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];						

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiifg.InvCode InvCode
						, ifiifgdt.PartCode PartCode
						, ifiifgis.SerialNo SerialNo
						, ifiifgdt.NetworkID NetworkID
						, ifiifg.MST MST
						, ifiifg.PMType PMType
						--, ifiifgis.SerialNo SerialNo_Actual
						, ifiifgis.SerialNo SecretNo
						, ifiifgis.PartLotNo PartLotNo
						, ifiifgis.BoxNo
						, ifiifgis.CanNo
						, ifiifgis.AgentCode
						, null WarrantyDateStart
						, ifiifgis.PackageDate PackageDate
						, ifiifgis.ProductionDate ProductionDate
						, ifiifgis.LogLUBy UserBox
						, ifiifgis.LogLUBy UserCan
						, null UserKCS
						, null UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiifgis.IF_InvInFGISStatus = 'APPROVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, '1' FlagMap
						, '0' FlagSales
						, '0' FlagBox
						, '0' FlagCan
						, '0' FlagUI
						, ifiifg.FormInType FormInType
						, ifiifgis.IF_InvInFGNo IF_InvInFGNo
						, null FormOutType
						, null IF_InvOutFGNo
						, t.LUDTimeUTC LUDTimeUTC
						, t.LUBy LUBy
						, t.LUDTimeUTC CreateDTimeUTC
						, t.LUBy CreateBy
						, t.LUDTimeUTC InvDTime
						, t.LUBy InvBy
						, t.LUDTimeUTC BoxDTime
						, t.LUBy BoxBy
						, t.LUDTimeUTC CanDTime
						, t.LUBy CanBy
						, null OutDTime
						, null OutBy
						, null RefNo_Type
						, null RefNo_PK
						, ifiifgis.ShiftInCode ShiftInCode
						, ifiifgis.PrintDate PrintDate
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYINFGINSTSERIAL' RefType
						, ifiifg.IF_InvInFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LUDTimeUTC LogLUDTime
						, t.LUBy LogLUBy
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryInFG t --//[mylock]
						inner join InvF_InventoryInFG ifiifg --//[mylock]
							on t.IF_InvInFGNo = ifiifg.IF_InvInFGNo
						inner join InvF_InventoryInFGDtl ifiifgdt --//[mylock]
							on ifiifg.IF_InvInFGNo = ifiifgdt.IF_InvInFGNo
						inner join InvF_InventoryInFGInstSerial ifiifgis --//[mylock]
							on ifiifgdt.IF_InvInFGNo = ifiifgis.IF_InvInFGNo
								and  ifiifgdt.PartCode = ifiifgis.PartCode
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );

        }
        #endregion

        #region // InvF_InventoryInFGInstSerial:
        public void InvF_InventoryInFGInstSerial_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_InventoryInFGInstSerial
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_InventoryInFGInstSerial_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_InventoryInFGInstSerial", strRt_Cols_InvF_InventoryInFGInstSerial
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_InventoryInFGInstSerial = (strRt_Cols_InvF_InventoryInFGInstSerial != null && strRt_Cols_InvF_InventoryInFGInstSerial.Length > 0);
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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"						
						---- #tbl_InvF_InventoryInFGInstSerial_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiifis.IF_InvInFGNo
							, iiifis.PartCode
							, iiifis.SerialNo
						into #tbl_InvF_InventoryInFGInstSerial_Filter_Draft
						from InvF_InventoryInFGInstSerial iiifis --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							iiifis.IF_InvInFGNo desc
							, iiifis.PartCode desc
							, iiifis.SerialNo desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_InventoryInFGInstSerial_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_InventoryInFGInstSerial_Filter:
						select
							t.*
						into #tbl_InvF_InventoryInFGInstSerial
						from #tbl_InvF_InventoryInFGInstSerial_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_InventoryInFGInstSerial ------:
						zzB_Select_InvF_InventoryInFGInstSerial_zzE
						--------------------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_InventoryInFGInstSerial_Filter_Draft;
						--drop table #tbl_InvF_InventoryInFGInstSerial;
					"
                );
            ////
            string zzB_Select_InvF_InventoryInFGInstSerial_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryInFGInstSerial)
            {
                #region // bGet_InvF_InventoryInFGInstSerial:
                zzB_Select_InvF_InventoryInFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryInFGDtl:
							select
								t.MyIdxSeq
								, iiifis.*
							from #tbl_InvF_InventoryInFGInstSerial t --//[mylock]
								inner join InvF_InventoryInFGInstSerial iiifis --//[mylock]
									on t.IF_InvInFGNo = iiifis.IF_InvInFGNo
									    and t.PartCode = iiifis.PartCode
									    and t.SerialNo = iiifis.SerialNo
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
                        , "InvF_InventoryInFGInstSerial" // strTableNameDB
                        , "InvF_InventoryInFGInstSerial." // strPrefixStd
                        , "iiifis." // strPrefixAlias
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
                , "zzB_Select_InvF_InventoryInFGInstSerial_zzE", zzB_Select_InvF_InventoryInFGInstSerial_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_InventoryInFGInstSerial)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryInFGInstSerial";
            }
            #endregion
        }

        public DataSet InvF_InventoryInFGInstSerial_Get(
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
            , string strRt_Cols_InvF_InventoryInFGInstSerial
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryInFGInstSerial_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryInFGInstSerial_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
                , "strRt_Cols_InvF_InventoryInFGInstSerial", strRt_Cols_InvF_InventoryInFGInstSerial
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_InventoryInFGInstSerial_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryInFGInstSerial_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_InvF_InventoryInFGInstSerial // strRt_Cols_InvF_InventoryInFGInstSerial
                                                                  /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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
        public DataSet WAS_InvF_InventoryInFGInstSerial_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryInFGInstSerial objRQ_InvF_InventoryInFGInstSerial
            ////
            , out RT_InvF_InventoryInFGInstSerial objRT_InvF_InventoryInFGInstSerial
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryInFGInstSerial.Tid;
            objRT_InvF_InventoryInFGInstSerial = new RT_InvF_InventoryInFGInstSerial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFGInstSerial.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryInFGInstSerial_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryInFGInstSerial_Get;
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
                List<InvF_InventoryInFGInstSerial> lst_InvF_InventoryInFGInstSerial = new List<InvF_InventoryInFGInstSerial>();
                /////
                bool bGet_InvF_InventoryInFGInstSerialInstSerial = (objRQ_InvF_InventoryInFGInstSerial.Rt_Cols_InvF_InventoryInFGInstSerial != null && objRQ_InvF_InventoryInFGInstSerial.Rt_Cols_InvF_InventoryInFGInstSerial.Length > 0);
                #endregion

                #region // WS_InvF_InventoryInFGInstSerial_Get:
                mdsResult = InvF_InventoryInFGInstSerial_Get(
                    objRQ_InvF_InventoryInFGInstSerial.Tid // strTid
                    , objRQ_InvF_InventoryInFGInstSerial.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFGInstSerial.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryInFGInstSerial.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryInFGInstSerial.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryInFGInstSerial.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_InventoryInFGInstSerial.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_InventoryInFGInstSerial.Ft_WhereClause // strFt_WhereClause
                                                                        //// Return:
                    , objRQ_InvF_InventoryInFGInstSerial.Rt_Cols_InvF_InventoryInFGInstSerial // Rt_Cols_InvF_InventoryInFGInstSerialInstSerial
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_InventoryInFGInstSerial.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    if (bGet_InvF_InventoryInFGInstSerialInstSerial)
                    {
                        ////
                        DataTable dt_InvF_InventoryInFGInstSerial = mdsResult.Tables["InvF_InventoryInFGInstSerial"].Copy();
                        lst_InvF_InventoryInFGInstSerial = TUtils.DataTableCmUtils.ToListof<InvF_InventoryInFGInstSerial>(dt_InvF_InventoryInFGInstSerial);
                        objRT_InvF_InventoryInFGInstSerial.Lst_InvF_InventoryInFGInstSerial = lst_InvF_InventoryInFGInstSerial;
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

        #region // InvF_InventoryOutFG:
        public void InvF_InventoryOutFG_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objIF_InvOutFGNo
            , string strFlagExistToCheck
            , string strStatusListToCheck
            , out DataTable dtDB_InvF_InventoryOutFG
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from InvF_InventoryOutFG t --//[mylock]
					where (1=1)
						and t.IF_InvOutFGNo = @objIF_InvOutFGNo
					;
				");
            dtDB_InvF_InventoryOutFG = _cf.db.ExecQuery(
                strSqlExec
                , "@objIF_InvOutFGNo", objIF_InvOutFGNo
                ).Tables[0];
            dtDB_InvF_InventoryOutFG.TableName = "InvF_InventoryOutFG";

            // strFlagExistToCheck
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_InvF_InventoryOutFG.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvOutFGNo", objIF_InvOutFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_CheckDB_InvInFGNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_InvF_InventoryOutFG.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvOutFGNo", objIF_InvOutFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_CheckDB_InvInFGNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.IF_InvOutFGNo", objIF_InvOutFGNo
                    , "Check.strFlagActiveListToCheck", strStatusListToCheck
                    , "DB.IF_InvOutFGStatus", dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.InvF_InventoryOutFG_CheckDB_StatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public void InvF_InventoryOutFG_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_InventoryOutFG
            , string strRt_Cols_InvF_InventoryOutFGDtl
            , string strRt_Cols_InvF_InventoryOutFGInstSerial
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_InventoryOutFG_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_InventoryOutFG", strRt_Cols_InvF_InventoryOutFG
                , "strRt_Cols_InvF_InventoryOutFGDtl", strRt_Cols_InvF_InventoryOutFGDtl
                , "strRt_Cols_InvF_InventoryOutFGInstSerial", strRt_Cols_InvF_InventoryOutFGInstSerial
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_InventoryOutFG = (strRt_Cols_InvF_InventoryOutFG != null && strRt_Cols_InvF_InventoryOutFG.Length > 0);
            bool bGet_InvF_InventoryOutFGDtl = (strRt_Cols_InvF_InventoryOutFGDtl != null && strRt_Cols_InvF_InventoryOutFGDtl.Length > 0);
            bool bGet_InvF_InventoryOutFGInstSerial = (strRt_Cols_InvF_InventoryOutFGInstSerial != null && strRt_Cols_InvF_InventoryOutFGInstSerial.Length > 0);
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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_InvF_InventoryOutFG_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiof.IF_InvOutFGNo
							, iiof.CreateDTimeUTC
						into #tbl_InvF_InventoryOutFG_Filter_Draft
						from InvF_InventoryOutFG iiof --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiof.MST = t_MstNNT_View.MST
							inner join InvF_InventoryOutFGDtl iiofdt --//[mylock]
								on iiof.IF_InvOutFGNo = iiofdt.IF_InvOutFGNo
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiof.CreateDTimeUTC desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_InventoryOutFG_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_InventoryOutFG_Filter:
						select
							t.*
						into #tbl_InvF_InventoryOutFG_Filter
						from #tbl_InvF_InventoryOutFG_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_InventoryOutFG ------:
						zzB_Select_InvF_InventoryOutFG_zzE
						--------------------------------

						-------- InvF_InventoryOutFGDtl ------:
						zzB_Select_InvF_InventoryOutFGDtl_zzE
						--------------------------------------

						-------- InvF_InventoryOutFGInstSerial ------:
						zzB_Select_InvF_InventoryOutFGInstSerial_zzE
						--------------------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_InventoryOutFG_Filter_Draft;
						--drop table #tbl_InvF_InventoryOutFG_Filter;
					"
                );
            ////
            string zzB_Select_InvF_InventoryOutFG_zzE = "-- Nothing.";
            if (bGet_InvF_InventoryOutFG)
            {
                #region // bGet_InvF_InventoryOutFG:
                zzB_Select_InvF_InventoryOutFG_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutFG:
							select
								t.MyIdxSeq
								, iiof.*
							from #tbl_InvF_InventoryOutFG_Filter t --//[mylock]
								inner join InvF_InventoryOutFG iiof --//[mylock]
									on t.IF_InvOutFGNo = iiof.IF_InvOutFGNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryOutFGDtl_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryOutFGDtl)
            {
                #region // bGet_InvF_InventoryOutFGDtl:
                zzB_Select_InvF_InventoryOutFGDtl_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutFGDtl:
							select
								t.MyIdxSeq
								, iiofdt.*
							from #tbl_InvF_InventoryOutFG_Filter t --//[mylock]
								inner join InvF_InventoryOutFG iiof --//[mylock]
									on t.IF_InvOutFGNo = iiof.IF_InvOutFGNo
								inner join InvF_InventoryOutFGDtl iiofdt --//[mylock]
									on t.IF_InvOutFGNo = iiofdt.IF_InvOutFGNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryOutFGInstSerial_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryOutFGInstSerial)
            {
                #region // bGet_InvF_InventoryOutFGInstSerial:
                zzB_Select_InvF_InventoryOutFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutFGDtl:
							select
								t.MyIdxSeq
								, iiofs.*
							from #tbl_InvF_InventoryOutFG_Filter t --//[mylock]
								inner join InvF_InventoryOutFG iiof --//[mylock]
									on t.IF_InvOutFGNo = iiof.IF_InvOutFGNo
								inner join InvF_InventoryOutFGInstSerial iiofs --//[mylock]
									on t.IF_InvOutFGNo = iiofs.IF_InvOutFGNo
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
                        , "InvF_InventoryOutFG" // strTableNameDB
                        , "InvF_InventoryOutFG." // strPrefixStd
                        , "iiof." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "InvF_InventoryOutFGDtl" // strTableNameDB
                        , "InvF_InventoryOutFGDtl." // strPrefixStd
                        , "iiofdt." // strPrefixAlias
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
                , "zzB_Select_InvF_InventoryOutFG_zzE", zzB_Select_InvF_InventoryOutFG_zzE
                , "zzB_Select_InvF_InventoryOutFGDtl_zzE", zzB_Select_InvF_InventoryOutFGDtl_zzE
                , "zzB_Select_InvF_InventoryOutFGInstSerial_zzE", zzB_Select_InvF_InventoryOutFGInstSerial_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_InventoryOutFG)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutFG";
            }
            if (bGet_InvF_InventoryOutFGDtl)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutFGDtl";
            }
            if (bGet_InvF_InventoryOutFGInstSerial)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutFGInstSerial";
            }
            #endregion
        }

        public DataSet InvF_InventoryOutFG_Get(
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
            , string strRt_Cols_InvF_InventoryOutFG
            , string strRt_Cols_InvF_InventoryOutFGDtl
            , string strRt_Cols_InvF_InventoryOutFGInstSerial
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryOutFG_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutFG_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_InvF_InventoryOutFG", strRt_Cols_InvF_InventoryOutFG
                , "strRt_Cols_InvF_InventoryOutFGDtl", strRt_Cols_InvF_InventoryOutFGDtl
                , "strRt_Cols_InvF_InventoryOutFGInstSerial", strRt_Cols_InvF_InventoryOutFGInstSerial
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_InventoryOutFG_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryOutFG_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_InvF_InventoryOutFG // strRt_Cols_InvF_InventoryOutFG
                        , strRt_Cols_InvF_InventoryOutFGDtl // strRt_Cols_InvF_InventoryOutFGDtl
                        , strRt_Cols_InvF_InventoryOutFGInstSerial // strRt_Cols_InvF_InventoryOutFGInstSerial
                                                                  /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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

        public DataSet WAS_InvF_InventoryOutFG_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG
            ////
            , out RT_InvF_InventoryOutFG objRT_InvF_InventoryOutFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutFG.Tid;
            objRT_InvF_InventoryOutFG = new RT_InvF_InventoryOutFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutFG_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutFG_Get;
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
                List<InvF_InventoryOutFG> lst_InvF_InventoryOutFG = new List<InvF_InventoryOutFG>();
                List<InvF_InventoryOutFGDtl> lst_InvF_InventoryOutFGDtl = new List<InvF_InventoryOutFGDtl>();
                List<InvF_InventoryOutFGInstSerial> lst_InvF_InventoryOutFGInstSerial = new List<InvF_InventoryOutFGInstSerial>();
                /////
                bool bGet_InvF_InventoryOutFG = (objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFG != null && objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFG.Length > 0);
                bool bGet_InvF_InventoryOutFGDtl = (objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGDtl != null && objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGDtl.Length > 0);
                bool bGet_InvF_InventoryOutFGInstSerial = (objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGInstSerial != null && objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGInstSerial.Length > 0);
                #endregion

                #region // WS_InvF_InventoryOutFG_Get:
                mdsResult = InvF_InventoryOutFG_Get(
                    objRQ_InvF_InventoryOutFG.Tid // strTid
                    , objRQ_InvF_InventoryOutFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutFG.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_InventoryOutFG.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_InventoryOutFG.Ft_WhereClause // strFt_WhereClause
                                                              //// Return:
                    , objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFG // strRt_Cols_InvF_InventoryOutFG
                    , objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGDtl // Rt_Cols_InvF_InventoryOutFGDtl
                    , objRQ_InvF_InventoryOutFG.Rt_Cols_InvF_InventoryOutFGInstSerial // Rt_Cols_InvF_InventoryOutFGInstSerial
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_InventoryOutFG.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    if (bGet_InvF_InventoryOutFG)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutFG = mdsResult.Tables["InvF_InventoryOutFG"].Copy();
                        lst_InvF_InventoryOutFG = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutFG>(dt_InvF_InventoryOutFG);
                        objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFG = lst_InvF_InventoryOutFG;
                    }
                    ////
                    if (bGet_InvF_InventoryOutFGDtl)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutFGDtl = mdsResult.Tables["InvF_InventoryOutFGDtl"].Copy();
                        lst_InvF_InventoryOutFGDtl = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutFGDtl>(dt_InvF_InventoryOutFGDtl);
                        objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl = lst_InvF_InventoryOutFGDtl;
                    }
                    ////
                    if (bGet_InvF_InventoryOutFGInstSerial)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutFGInstSerial = mdsResult.Tables["InvF_InventoryOutFGInstSerial"].Copy();
                        lst_InvF_InventoryOutFGInstSerial = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutFGInstSerial>(dt_InvF_InventoryOutFGInstSerial);
                        objRT_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGInstSerial = lst_InvF_InventoryOutFGInstSerial;
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

        private void InvF_InventoryOutFG_SaveX_Old(
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
            , object objIF_InvOutFGNo
            , object objMST
            , object objInvOutType
            , object objFormOutType
            , object objInvCode
            , object objPMType
            , object objRemark
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutFG_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutFGNo", objIF_InvOutFGNo
                , "objMST", objMST
                , "objInvOutType", objInvOutType
                , "objFormOutType", objFormOutType
                , "objInvCode", objInvCode
                , "objPMType", objPMType
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });

            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryOutFG:
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strIF_InvOutFGNo = TUtils.CUtils.StdParam(objIF_InvOutFGNo);
            string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
            string strFormOutType = TUtils.CUtils.StdParam(objFormOutType);
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strInvCode = TUtils.CUtils.StdParam(objInvCode);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutFG = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            {
                ////
                if (string.IsNullOrEmpty(strIF_InvOutFGNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvOutFGNo", strIF_InvOutFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidIF_InvOutFGNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryOutFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutFGNo // objIF_InvOutFGNo
                    , "" // strFlagExistToCheck
                    , "" // strStatusListToCheck
                    , out dtDB_InvF_InventoryOutFG // dtDB_Invoice_Invoice
                    );
                ////
                if (dtDB_InvF_InventoryOutFG.Rows.Count < 1) // Chưa Tồn tại.
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công.
                    }
                    else
                    {
                        // 
                        strCreateDTimeUTC = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strCreateBy = strWAUserCode;
                    }
                }
                else // Đã Tồn tại.
                {
                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"], TConst.IF_InvOutFGStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvOutFGStatus", dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"]
                            , "Check.IF_InvOutFGStatus.Expected", TConst.IF_InvOutFGStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryOutFG.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOutFG.Rows[0]["CreateBy"]);
                    ////
                }
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.DB.NNT.MST", drAbilityOfUser["strMST"]
                        , "Check.strMST", strMST
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////

                DataTable dtDB_Mst_NNT = new DataTable();
                if (string.IsNullOrEmpty(strFormOutType)) strFormOutType = TConst.FormOutType.KhongMaVach;
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutFGNo"
                        , "NetworkID"
                        , "FormOutType"
                        , "InvOutType"
                        , "MST"
                        , "InvCode"
                        , "PMType"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutFGNo, // IF_InvOutFGNo
                                nNetworkID, // NetworkID
								strFormOutType, // FormOutType
								strInvOutType, // InvOutType
								strMST, // MST
								strInvCode, // InvCode
								strPMType, // PMType
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // CreateDTimeUTC
                                strWAUserCode, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvOutFGStatus.Pending, // IF_InvOutFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutFGDtl:
            ////
            DataTable dtInput_InvF_InventoryOutFGDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutFGDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutFGDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryOutFGDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutFGDtl // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "IF_InvOutFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutFGDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutFGDtl.Rows[nScan];
                    ////
                    drScan["IF_InvOutFGNo"] = strIF_InvOutFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutFGStatusDtl"] = TConst.IF_InvOutFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutFGDtl:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutFGDtl" // strTableName
                    , new object[] {
                            "IF_InvOutFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "IF_InvOutFGStatusDtl", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutFGDtl // dtData
                );
            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutFGInstSerial:
            ////
            DataTable dtInput_InvF_InventoryOutFGInstSerial = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutFGInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvF_InventoryOutFGInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutFGInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutFGDtl // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "SerialNo", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "IF_InvOutFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "IF_InvOutFGISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutFGInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutFGDtl.Rows[nScan];
                    ////
                    //string strVATRateCode = TUtils.CUtils.StdParam(drScan["VATRateCode"]);
                    ////
                    drScan["IF_InvOutFGNo"] = strIF_InvOutFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutFGISStatus"] = TConst.IF_InvOutFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutFGInstSerial:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutFGInstSerial" // strTableName
                    , new object[] {
                            "IF_InvOutFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "IF_InvOutFGISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutFGInstSerial // dtData
                );
            }
            #endregion

            #region // InventoryTransaction:
            {
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.MaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Clear(strFunctionName);
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.KhongMaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Clear_NoSerial(strFunctionName);
                }

                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // lstParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGInstSerial:
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryOutFGInstSerial
                            from InvF_InventoryOutFGInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutFGInstSerial:
                            delete t 
                            from InvF_InventoryOutFGInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutFGInstSerial f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
		                                and t.PartCode = f.PartCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutFGDtl:
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                            into #tbl_InvF_InventoryOutFGDtl
                            from InvF_InventoryOutFGDtl t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutFGDtl:
                            delete t 
                            from InvF_InventoryOutFGDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutFGDtl f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
		                                and t.PartCode = f.PartCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryOutFG:
                            delete t
                            from InvF_InventoryOutFG t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutFGInstSerial;
                            drop table #tbl_InvF_InventoryOutFGDtl;
							");
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {
                        ////
                        string zzzzClauseInsert_InvF_InventoryOutFG_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFG:                                
                                insert into InvF_InventoryOutFG(
	                                IF_InvOutFGNo
	                                , NetworkID
	                                , MST
	                                , InvOutType
	                                , FormOutType
	                                , InvCode
	                                , PMType
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , IF_InvOutFGStatus
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.InvOutType
	                                , t.FormOutType
	                                , t.InvCode
	                                , t.PMType
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.IF_InvOutFGStatus
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFG t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFG:                                
                                insert into InvF_InventoryOutFGDtl(
	                                IF_InvOutFGNo 
	                                , PartCode
	                                , NetworkID
	                                , Qty
	                                , ProductionDate
	                                , IF_InvOutFGStatusDtl
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo 
	                                , t.PartCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.ProductionDate
	                                , t.IF_InvOutFGStatusDtl
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFGDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFGInstSerial:                                
                                insert into InvF_InventoryOutFGInstSerial(
	                                IF_InvOutFGNo 
	                                , PartCode
	                                , SerialNo
	                                , NetworkID
	                                , IF_InvOutFGISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo 
	                                , t.PartCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.IF_InvOutFGISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFGInstSerial t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryOutFG_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryOutFG_zSave", zzzzClauseInsert_InvF_InventoryOutFG_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave", zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave", zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region // InventoryTransaction:
            if (!bIsDelete)
            {
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.MaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Block(strFunctionName);
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.KhongMaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Block_NoSerial(strFunctionName);
                }
                ////
                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // alParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            #region //// Clear For Debug:.
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutFG;
						drop table #input_InvF_InventoryOutFGDtl;
						drop table #input_InvF_InventoryOutFGInstSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            else
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutFG;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////

            }
        #endregion

        // Return Good:
        MyCodeLabel_Done:
            return;
        }

        private void InvF_InventoryOutFG_ApproveX_Old(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objIF_InvOutFGNo
            , object objRemark
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutFG_ApproveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutFGNo", objIF_InvOutFGNo
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryOutFG:
            ////
            string strIF_InvOutFGNo = TUtils.CUtils.StdParam(objIF_InvOutFGNo);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutFG = null;
            {
                /////
                InvF_InventoryOutFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutFGNo // objInvoiceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvOutFGStatus.Pending // strIF_InvOutFGStatusListToCheck
                    , out dtDB_InvF_InventoryOutFG // dtDB_Invoice_Invoice
                    );
                ////
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], dtDB_InvF_InventoryOutFG.Rows[0]["MST"]))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                        , "Check.InvF_InventoryOutFG.MST",  dtDB_InvF_InventoryOutFG.Rows[0]["MST"]
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Approve_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutFGNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutFGNo, // IF_InvOutFGNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvOutFGStatus.Approve, // IF_InvOutFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE = @"
                        t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
                        , t.Remark = f.Remark
                        , t.ApprDTimeUTC = f.ApprDTimeUTC
                        , t.ApprBy = f.ApprBy
                        , t.IF_InvOutFGStatus = f.IF_InvOutFGStatus
                        ";
                ////
                string zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutFGStatusDtl = f.IF_InvOutFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvInFGISStatus = f.IF_InvOutFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutFG_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryOutFG:
						update t
						set 
							zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE
						from InvF_InventoryOutFG t --//[mylock]
							inner join #input_InvF_InventoryOutFG f --//[mylock]
								on t.IF_InvOutFGNo = f.IF_InvOutFGNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutFGDtl_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGDtl_Temp: 
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , f.IF_InvOutFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutFGDtl_Temp
							from InvF_InventoryOutFGDtl t --//[mylock]
							    inner join #input_InvF_InventoryOutFG f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE
							from InvF_InventoryOutFGDtl t --//[mylock]
							    inner join #tbl_InvF_InventoryOutFGDtl_Temp f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
								        and t.PartCode = f.PartCode
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGInstSerial_Temp: 
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , t.SerialNo
                                , f.IF_InvOutFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutFGInstSerial_Temp
							from InvF_InventoryOutFGInstSerial t --//[mylock]
							    inner join #input_InvF_InventoryOutFG f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE
							from InvF_InventoryOutFGInstSerial t --//[mylock]
							    inner join #tbl_InvF_InventoryOutFGInstSerial_Temp f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
								        and t.PartCode = f.PartCode
								        and t.SerialNo = f.SerialNo
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE
                    );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryOutFG_zzE
                        ----
						zzB_Update_InvF_InventoryOutFGDtl_zzE
                        ----
						zzB_Update_InvF_InventoryOutFGInstSerial_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryOutFG_zzE", zzB_Update_InvF_InventoryOutFG_zzE
                    , "zzB_Update_InvF_InventoryOutFGDtl_zzE", zzB_Update_InvF_InventoryOutFGDtl_zzE
                    , "zzB_Update_InvF_InventoryOutFGInstSerial_zzE", zzB_Update_InvF_InventoryOutFGInstSerial_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            #region // InventoryTransaction:
            {
                ///InvFInventory_InventoryTransaction_Exec_Delete_New20180120(strFunctionName, htParamsSql);

                if (CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOutFG.Rows[0]["FormOutType"], TConst.FormOutType.MaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Delete(strFunctionName, htParamsSql);
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOutFG.Rows[0]["FormOutType"], TConst.FormOutType.KhongMaVach))
                {
                    InvFInventory_InventoryTransaction_Exec_Delete_NoSerial(strFunctionName, htParamsSql);
                    /////
                }
                ////
                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // alParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            // Return Good:
            return;
        }

        private void InvF_InventoryOutFG_SaveX(
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
            , object objIF_InvOutFGNo
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutFG_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutFGNo", objIF_InvOutFGNo
                , "objMST", objMST
                , "objFormOutType", objFormOutType
                , "objInvOutType", objInvOutType
                , "objInvCode", objInvCode
                , "objPMType", objPMType
                , "objInvFOutType", objInvFOutType
                , "objPlateNo", objPlateNo
                , "objMoocNo", objMoocNo
                , "objDriverName", objDriverName
                , "objDriverPhoneNo", objDriverPhoneNo
                , "objAgentCode", objAgentCode
                , "objCustomerName", objCustomerName
                ////
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            #endregion

            #region // Refine and Check Input InvF_InventoryOutFG:
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strIF_InvOutFGNo = TUtils.CUtils.StdParam(objIF_InvOutFGNo);
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strFormOutType = TUtils.CUtils.StdParam(objFormOutType);
            string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
            string strInvCode = TUtils.CUtils.StdParam(objInvCode);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strInvFOutType = TUtils.CUtils.StdParam(objInvFOutType);
            string strPlateNo = string.Format("{0}", objPlateNo).Trim();
            string strMoocNo = string.Format("{0}", objMoocNo).Trim();
            string strDriverName = string.Format("{0}", objDriverName).Trim();
            string strDriverPhoneNo = string.Format("{0}", objDriverPhoneNo).Trim();
            string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);
            string strCustomerName = string.Format("{0}", objCustomerName).Trim();
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutFG = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            {
                ////
                if (string.IsNullOrEmpty(strIF_InvOutFGNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvOutFGNo", strIF_InvOutFGNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidIF_InvOutFGNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryOutFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutFGNo // objInvoiceCode
                    , "" // strFlagExistToCheck
                    , "" // strInvoiceStatusListToCheck
                    , out dtDB_InvF_InventoryOutFG // dtDB_Invoice_Invoice
                    );
                ////
                if (dtDB_InvF_InventoryOutFG.Rows.Count < 1) // Chưa Tồn tại.
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công.
                    }
                    else
                    {
                        // 
                        strCreateDTimeUTC = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strCreateBy = strWAUserCode;
                    }
                }
                else // Đã Tồn tại.
                {
                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"], TConst.IF_InvOutFGStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvOutFGStatus", dtDB_InvF_InventoryOutFG.Rows[0]["IF_InvOutFGStatus"]
                            , "Check.IF_InvOutFGStatus.Expected", TConst.IF_InvOutFGStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryOutFG.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOutFG.Rows[0]["CreateBy"]);
                    ////
                }

                DataTable dtDB_Mst_NNT = new DataTable();
                if (string.IsNullOrEmpty(strFormOutType)) strFormOutType = TConst.FormOutType.KhongMaVach;
                ////
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(strInvFOutType, TConst.InvFOutType.OutThuongMai)
                    && !CmUtils.StringUtils.StringEqualIgnoreCase(strInvFOutType, TConst.InvFOutType.OutEndCus))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strInvFOutType", strInvFOutType
                        , "Check.InvFOutType.Expected", "OutThuongMai Or OutEndCus"
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvalidInvFOutType
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutFGNo"
                        , "NetworkID"
                        , "MST"
                        , "FormOutType"
                        , "InvOutType"
                        , "InvCode"
                        , "PMType"
                        , "InvFOutType"
                        , "PlateNo"
                        , "MoocNo"
                        , "DriverName"
                        , "DriverPhoneNo"
                        , "AgentCode"
                        , "CustomerName"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutFGNo, // IF_InvOutFGNo
                                nNetworkID, // NetworkID
								strMST, // MST
								strFormOutType, // FormOutType
								strInvOutType, // InvOutType
								strInvCode, // InvCode
								strPMType, // PMType
								strInvFOutType, // InvFOutType
								strPlateNo, // PlateNo
                                strMoocNo, // MoocNo
                                strDriverName, // DriverName
                                strDriverPhoneNo, // DriverPhoneNo
                                strAgentCode, // AgentCode
                                strCustomerName, // CustomerName
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                null, // ApprDTimeUTC
                                null, // ApprBy
                                TConst.IF_InvOutFGStatus.Pending, // IF_InvOutFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutFGDtl:
            ////
            DataTable dtInput_InvF_InventoryOutFGDtl = new DataTable();
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutFGDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutFGDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryOutFGDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvFInventoryOutDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutFGDtl // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "IF_InvOutFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "IF_InvOutFGStatusDtl", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutFGDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutFGDtl.Rows[nScan];
                    ////
                    drScan["IF_InvOutFGNo"] = strIF_InvOutFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutFGStatusDtl"] = TConst.IF_InvOutFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutFGDtl:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutFGDtl" // strTableName
                    , new object[] {
                            "IF_InvOutFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "IF_InvOutFGStatusDtl", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutFGDtl // dtData
                );
            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutFGInstSerial:
            ////
            DataTable dtInput_InvF_InventoryOutFGInstSerial = new DataTable();
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutFGInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_InvF_InventoryOutFGInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutFGInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutFGInstSerial // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "IF_InvOutFGNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "IF_InvOutFGISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutFGInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutFGInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutFGInstSerial.Rows[nScan];
                    ////
                    //string strVATRateCode = TUtils.CUtils.StdParam(drScan["VATRateCode"]);
                    ////
                    drScan["IF_InvOutFGNo"] = strIF_InvOutFGNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutFGISStatus"] = TConst.IF_InvOutFGStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutFGInstSerial:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutFGInstSerial" // strTableName
                    , new object[] {
                            "IF_InvOutFGNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "IF_InvOutFGISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutFGInstSerial // dtData
                );
            }
            #endregion

            #region // Check : Serial phải cùng MST.
            if (!bIsDelete)
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                                , f.MST
                            from #input_InvF_InventoryOutFGInstSerial t --//[mylock]
                                inner join Inv_InventoryBalanceSerial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.MST <> '@strMST'
                            ;
                        "
                    , "@strMST", strMST
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.Serial", dtCheck.Rows[0]["SerialNo"]
                        , "Check.DB.SerialInv.MST", dtCheck.Rows[0]["MST"]
                        , "Check.Input.MST", strMST
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutFG_Save_SerialNoNotExistInInvOfMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // InventoryTransaction:
            {
                ////
                if (dtInput_InvF_InventoryOutFGInstSerial.Rows.Count > 0) strFormOutType = TConst.FormOutType.MaVach;
                else if(dtInput_InvF_InventoryOutFGInstSerial.Rows.Count < 1 && dtInput_InvF_InventoryOutFGDtl.Rows.Count > 0) strFormOutType = TConst.FormOutType.KhongMaVach;
                /////
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.MaVach))
                    InvFInventory_InventoryTransaction_Exec_Clear(strFunctionName);

                else if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.KhongMaVach))
                    InvFInventory_InventoryTransaction_Exec_Clear_NoSerial(strFunctionName);
                /////

                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // lstParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGInstSerial:
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryOutFGInstSerial_Del
                            from InvF_InventoryOutFGInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutFGInstSerial:
                            delete t 
                            from InvF_InventoryOutFGInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutFGInstSerial_Del f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
		                                and t.PartCode = f.PartCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutFGDtl:
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                            into #tbl_InvF_InventoryOutFGDtl
                            from InvF_InventoryOutFGDtl t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutFGDtl:
                            delete t 
                            from InvF_InventoryOutFGDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutFGDtl f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
		                                and t.PartCode = f.PartCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryOutFG:
                            delete t
                            from InvF_InventoryOutFG t --//[mylock]
	                            inner join #input_InvF_InventoryOutFG f --//[mylock]
		                            on t.IF_InvOutFGNo = f.IF_InvOutFGNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutFGInstSerial_Del;
                            drop table #tbl_InvF_InventoryOutFGDtl;
							");
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {
                        ////
                        string zzzzClauseInsert_InvF_InventoryOutFG_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFG:                                
                                insert into InvF_InventoryOutFG(
	                                IF_InvOutFGNo
	                                , NetworkID
	                                , MST
	                                , FormOutType
	                                , InvOutType
	                                , InvCode
	                                , PMType
	                                , InvFOutType
	                                , PlateNo
	                                , MoocNo
	                                , DriverName
	                                , DriverPhoneNo
	                                , AgentCode
	                                , CustomerName
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , IF_InvOutFGStatus
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.FormOutType
	                                , t.InvOutType
	                                , t.InvCode
	                                , t.PMType
	                                , t.InvFOutType
	                                , t.PlateNo
	                                , t.MoocNo
	                                , t.DriverName
	                                , t.DriverPhoneNo
	                                , t.AgentCode
	                                , t.CustomerName
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.IF_InvOutFGStatus
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFG t 
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFG:                                
                                insert into InvF_InventoryOutFGDtl(
	                                IF_InvOutFGNo
	                                , PartCode
	                                , NetworkID
	                                , Qty
	                                , IF_InvOutFGStatusDtl
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo
	                                , t.PartCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.IF_InvOutFGStatusDtl
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFGDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutFGInstSerial:                                
                                insert into InvF_InventoryOutFGInstSerial(
	                                IF_InvOutFGNo
	                                , PartCode
	                                , SerialNo
	                                , NetworkID
	                                , IF_InvOutFGISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutFGNo
	                                , t.PartCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.IF_InvOutFGISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutFGInstSerial t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryOutFG_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryOutFG_zSave", zzzzClauseInsert_InvF_InventoryOutFG_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave", zzzzClauseInsert_InvF_InventoryOutFGDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave", zzzzClauseInsert_InvF_InventoryOutFGInstSerial_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region // InventoryTransaction:
            if (!bIsDelete)
            {
                ////
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.MaVach))
                    InvFInventory_InventoryTransaction_Exec_Block(strFunctionName);

                else if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.KhongMaVach))
                    InvFInventory_InventoryTransaction_Exec_Block_NoSerial(strFunctionName);
                ////
                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // alParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            #region // Check AllConditional:
            {
                #region // Kho phải tồn tại trên hệ thống:
                {

                }
                #endregion

                #region // Part phải tồn tại trên hệ thống:
                {

                }
                #endregion 
            }
            #endregion 

            #region //// Clear For Debug:
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutFG;
						drop table #input_InvF_InventoryOutFGDtl;
						drop table #input_InvF_InventoryOutFGInstSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            else
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutFG;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////

            }
        #endregion

        // Return Good:
        MyCodeLabel_Done:
            return;

        }

        public DataSet InvF_InventoryOutFG_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            , object objFlagAppr
            ////
            , object objIF_InvOutFGNo
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryOutFG_Save";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutFG_Save;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
			        ////
                    , "objIF_InvOutFGNo", objIF_InvOutFGNo
                    , "objMST", objMST
                    , "objFormOutType", objFormOutType
                    , "objInvOutType", objInvOutType
                    , "objInvCode", objInvCode
                    , "objPMType", objPMType
                    , "objInvFOutType", objInvFOutType
                    , "objPlateNo", objPlateNo
                    , "objMoocNo", objMoocNo
                    , "objDriverName", objDriverName
                    , "objDriverPhoneNo", objDriverPhoneNo
                    , "objAgentCode", objAgentCode
                    , "objCustomerName", objCustomerName
                    , "objRemark", objRemark
                    ////
                    //, "objTInvoiceFilePathXML", objTInvoiceFilePathXML
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

                #region // InvF_InventoryOutFG_SaveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryOutFG_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                                          ////
                        , objIF_InvOutFGNo // objIF_InvOutFGNo
                        , objMST // objMST
                        , objFormOutType // objFormOutType
                        , objInvOutType // objInvOutType
                        , objInvCode // objInvCode
                        , objPMType // objPMType
                        , objInvFOutType // objInvFOutType
                        , objPlateNo // objPlateNo
                        , objMoocNo // objMoocNo
                        , objDriverName // objDriverName
                        , objDriverPhoneNo // objDriverPhoneNo
                        , objAgentCode // objAgentCode
                        , objCustomerName // objCustomerName
                        , objRemark // objRemark
                                    //// 
                        , dsData  // dsData
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

        public DataSet WAS_InvF_InventoryOutFG_Save(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG
            ////
            , out RT_InvF_InventoryOutFG objRT_InvF_InventoryOutFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutFG.Tid;
            objRT_InvF_InventoryOutFG = new RT_InvF_InventoryOutFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutFG_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutFG_SaveSpecial;
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
                //List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl == null)
                        objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl = new List<InvF_InventoryOutFGDtl>();
                    {
                        DataTable dt_InvF_InventoryOutFG = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutFGDtl>(objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGDtl, "InvF_InventoryOutFGDtl");
                        dsData.Tables.Add(dt_InvF_InventoryOutFG);
                    }
                    ////
                    if (objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGInstSerial == null)
                        objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGInstSerial = new List<InvF_InventoryOutFGInstSerial>();
                    {
                        DataTable dt_InvF_InventoryOutFGInstSerial = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutFGInstSerial>(objRQ_InvF_InventoryOutFG.Lst_InvF_InventoryOutFGInstSerial, "InvF_InventoryOutFGInstSerial");
                        dsData.Tables.Add(dt_InvF_InventoryOutFGInstSerial);
                    }
                    ////
                }
                #endregion

                #region // InvF_InventoryInFG_Save:
                mdsResult = InvF_InventoryOutFG_Save(
                    objRQ_InvF_InventoryOutFG.Tid // strTid
                    , objRQ_InvF_InventoryOutFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutFG.FlagIsDelete // objFlagIsDelete
                    , objRQ_InvF_InventoryOutFG.FlagAppr // objFlagAppr
                                                         /////
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.IF_InvOutFGNo // objIF_InvOutFGNo
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.MST // objMST
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.FormOutType // objFormOutType
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.InvOutType // objInvOutType
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.InvCode // objInvCode
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.PMType // objPMType
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.InvFOutType // objInvFOutType
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.PlateNo // objPlateNo
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.MoocNo // objMoocNo
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.DriverName // objDriverName
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.DriverPhoneNo // objDriverPhoneNo
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.AgentCode // objAgentCode
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.CustomerName // objCustomerName
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.Remark // objRemark
                                                                           /////
                    , dsData
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
        private void InvF_InventoryOutFG_ApproveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objIF_InvOutFGNo
            , object objRemark
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutFG_ApproveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutFGNo", objIF_InvOutFGNo
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            #endregion

            #region // Refine and Check Input InvF_InventoryOutFG:
            ////
            string strIF_InvOutFGNo = TUtils.CUtils.StdParam(objIF_InvOutFGNo);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutFG = null;
            {
                /////
                InvF_InventoryOutFG_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutFGNo // objInvoiceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvOutFGStatus.Pending // strIF_InvOutFGStatusListToCheck
                    , out dtDB_InvF_InventoryOutFG // dtDB_Invoice_Invoice
                    );
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutFG:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutFG"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutFGNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutFGStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutFGNo, // IF_InvOutFGNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvOutFGStatus.Approve, // IF_InvOutFGStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE = @"
                        t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
                        , t.Remark = f.Remark
                        , t.ApprDTimeUTC = f.ApprDTimeUTC
                        , t.ApprBy = f.ApprBy
                        , t.IF_InvOutFGStatus = f.IF_InvOutFGStatus
                        ";
                ////
                string zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutFGStatusDtl = f.IF_InvOutFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutFGISStatus = f.IF_InvOutFGStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutFG_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryOutFG:
						update t
						set 
							zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE
						from InvF_InventoryOutFG t --//[mylock]
							inner join #input_InvF_InventoryOutFG f --//[mylock]
								on t.IF_InvOutFGNo = f.IF_InvOutFGNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFG_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutFGDtl_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGDtl_Temp: 
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , f.IF_InvOutFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutFGDtl_Temp
							from InvF_InventoryOutFGDtl t --//[mylock]
							    inner join #input_InvF_InventoryOutFG f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE
							from InvF_InventoryOutFGDtl t --//[mylock]
							    inner join #tbl_InvF_InventoryOutFGDtl_Temp f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
								        and t.PartCode = f.PartCode
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFGDtl_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutFGInstSerial_Temp: 
                            select 
                                t.IF_InvOutFGNo
                                , t.PartCode
                                , t.SerialNo
                                , f.IF_InvOutFGStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutFGInstSerial_Temp
							from InvF_InventoryOutFGInstSerial t --//[mylock]
							    inner join #input_InvF_InventoryOutFG f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE
							from InvF_InventoryOutFGInstSerial t --//[mylock]
							    inner join #tbl_InvF_InventoryOutFGInstSerial_Temp f --//[mylock]
								    on t.IF_InvOutFGNo = f.IF_InvOutFGNo
								        and t.PartCode = f.PartCode
								        and t.SerialNo = f.SerialNo
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE", zzB_Update_InvF_InventoryOutFGInstSerial_ClauseSet_zzE
                    );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryOutFG_zzE
                        ----
						zzB_Update_InvF_InventoryOutFGDtl_zzE
                        ----
						zzB_Update_InvF_InventoryOutFGInstSerial_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryOutFG_zzE", zzB_Update_InvF_InventoryOutFG_zzE
                    , "zzB_Update_InvF_InventoryOutFGDtl_zzE", zzB_Update_InvF_InventoryOutFGDtl_zzE
                    , "zzB_Update_InvF_InventoryOutFGInstSerial_zzE", zzB_Update_InvF_InventoryOutFGInstSerial_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            #region // InventoryTransaction:
            {
                /////
                string strFormOutType = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOutFG.Rows[0]["FormOutType"]);
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.MaVach))
                    InvFInventory_InventoryTransaction_Exec_Delete(strFunctionName, htParamsSql);
                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFormOutType, TConst.FormOutType.KhongMaVach))
                    InvFInventory_InventoryTransaction_Exec_Delete_NoSerial(strFunctionName, htParamsSql);
                ////
                Inv_InventoryTransaction_Perform(
                    ref alParamsCoupleError // alParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , 0.0 // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    , 0.0 // dblMinQtyPlanTotal
                    , int.MinValue // dblMinQtyPlanBlock
                    , 0.0 // dblMinQtyPlanAvail
                    );
            }
            #endregion

            // Return Good:
            return;
        }

        public DataSet InvF_InventoryOutFG_Approve(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objIF_InvOutFGNo
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryOutFG_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutFG_Approve;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objIF_InvOutFGNo", objIF_InvOutFGNo
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

                #region // InvF_InventoryOutFG_ApproveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryOutFG_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objIF_InvOutFGNo // objIF_InvOutFGNo
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

        public DataSet WAS_InvF_InventoryOutFG_Approve(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG
            ////
            , out RT_InvF_InventoryOutFG objRT_InvF_InventoryOutFG
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutFG.Tid;
            objRT_InvF_InventoryOutFG = new RT_InvF_InventoryOutFG();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutFG_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutFG_Approve;
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

                #region // InvF_InventoryOutFG_Approve:
                mdsResult = InvF_InventoryOutFG_Approve(
                    objRQ_InvF_InventoryOutFG.Tid // strTid
                    , objRQ_InvF_InventoryOutFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutFG.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutFG.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutFG.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.IF_InvOutFGNo // objIF_InvOutFGNo
                    , objRQ_InvF_InventoryOutFG.InvF_InventoryOutFG.Remark // objRemark
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
        public void InvFInventory_InventoryTransaction_Exec_Delete(string zzzzClauseCol_FunctionName, Hashtable htParamsSql)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, -ifiofgdt.Qty QtyChTotalOK -- Xuất.
						, -ifiofgdt.Qty QtyChBlockOK -- Xuất.
						, 0.0 QtyChTotalNG -- Xuất.
						, 0.0 QtyChBlockNG -- Xuất.
						, -ifiofgdt.Qty QtyPlanChTotal -- Xuất.
						, -ifiofgdt.Qty QtyPlanChBlock -- Xuất.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, ifiofg.MST MST
 						, ifiofg.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];						

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, iibs.SerialNo SerialNo
						, ifiofg.PMType PMType
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, ifiofg.AgentCode AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPORVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, t.LogLUDTimeUTC OutDTime
						, t.LogLUBy OutBy
						, '@strRefNo_Type' RefNo_Type -- Xuất.
						, '@strRefNo_PK' RefNo_PK -- Xuất.
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYOUTINSTSERIAL' RefType
						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTimeUTC
						, t.LogLUBy LogLUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
						, t.LogLUDTimeUTC UpdateDTimeUTC
						, t.LogLUBy UpdateBy
 						, ifiofg.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on ifiofgdt.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
						--inner join #tbl_Inv_InventoryBalanceSerial_UserBlock iibdt_ub --//[mylock]
						--	on ifiofg.InvCode = iibdt_ub.InvCode
						--		and  ifiofgdt.PartCode = iibdt_ub.PartCode
                        --inner join #tbl_InvInventoryBalanceSerial_Input tbliibs_input --//[mylock]
                        --    on iibs.InvCode = tbliibs_input.InvCode
						--	    and iibs.PartCode = tbliibs_input.PartCode
						--	    and iibs.SerialNo = tbliibs_input.SerialNo
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "@strRefNo_Type", TUtils.CUtils.StdParam(htParamsSql["@strRefNo_Type"])
                , "@strRefNo_PK", TUtils.CUtils.StdParam(htParamsSql["@strRefNo_PK"])
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }

        public void InvFInventory_InventoryTransaction_Exec_Delete_NoSerial(string zzzzClauseCol_FunctionName, Hashtable htParamsSql)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, 0.0 QtyChTotalOK -- -ifiofgdt.Qty QtyChTotalOK -- Xuất.
						, 0.0 QtyChBlockOK -- -ifiofgdt.Qty QtyChBlockOK -- Xuất.
						, 0.0 QtyChTotalNG -- Xuất.
						, 0.0 QtyChBlockNG -- Xuất.
						, -ifiofgdt.Qty QtyPlanChTotal -- Xuất.
						, -ifiofgdt.Qty QtyPlanChBlock -- Xuất.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, ifiofg.MST MST
 						, ifiofg.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];						

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, iibs.SerialNo SerialNo
						, ifiofg.PMType PMType
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, null AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPORVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, t.LogLUDTimeUTC OutDTime
						, t.LogLUBy OutBy
						, '@strRefNo_Type' RefNo_Type -- Xuất.
						, '@strRefNo_PK' RefNo_PK -- Xuất.
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYOUTINSTSERIAL' RefType
						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTimeUTC
						, t.LogLUBy LogLUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
						, t.LogLUDTimeUTC UpdateDTime
						, t.LogLUBy UpdateBy
 						, ifiofg.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on ifiofgdt.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
						--inner join #tbl_Inv_InventoryBalanceSerial_UserBlock iibdt_ub --//[mylock]
						--	on ifiofg.InvCode] = iibdt_ub.InvCode
						--		and  ifiofgdt.PartCode] = iibdt_ub.PartCode
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "@strRefNo_Type", TUtils.CUtils.StdParam(htParamsSql["@strRefNo_Type"])
                , "@strRefNo_PK", TUtils.CUtils.StdParam(htParamsSql["@strRefNo_PK"])
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }
        public void InvFInventory_InventoryTransaction_Exec_Clear(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, 0.0 QtyChTotalOK 
						, -ifiofgdt.Qty QtyChBlockOK -- Giải tỏa.
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG -- Giải tỏa.
						, 0.0 QtyPlanChTotal 
						, -ifiofgdt.Qty QtyPlanChBlock -- Giải tỏa.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, t.MST MST
 						, t.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on ifiofg.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];					

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, ifiofgis.SerialNo SerialNo
						, ifiofg.PMType PMType
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, iibs.AgentCode AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPROVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.CreateDTimeUTC CreateDTimeSv
						, iibs.CreateBy CreateBySv
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, t.LogLUDTimeUTC OutDTime
						, t.LogLUBy OutBy
						, null RefNo_Type 
						, null RefNo_PK
						, 'zzzzClauseCol_FunctionName' FunctionName
						, null RefType
						, null RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTime
						, t.LogLUBy LogLUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
						, t.LogLUDTimeUTC UpdateDTime
						, t.LogLUBy UpdateBy
 						, t.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on t.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                , "@strFormOutType", TConst.FormOutType.MaVach
                );


            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }

        public void InvFInventory_InventoryTransaction_Exec_Clear_NoSerial(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, 0.0 QtyChTotalOK 
						, 0.0 QtyChBlockOK -- QtyChBlockOK -- Giải tỏa.
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG -- Giải tỏa.
						, 0.0 QtyPlanChTotal 
						, -ifiofgdt.Qty QtyPlanChBlock -- Giải tỏa.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, t.MST MST
 						, t.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on ifiofg.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];					

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, ifiofgis.SerialNo SerialNo
						, ifiofg.PMType PMType
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, iibs.AgentCode AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, @strBlockStatus_N BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPROVE' then '0'	
								else '1'
							end 	
						) FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, t.LogLUDTimeUTC OutDTime
						, t.LogLUBy OutBy
						, null RefNo_Type 
						, null RefNo_PK
						, 'zzzzClauseCol_FunctionName' FunctionName
						, null RefType
						, null RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTimeUTC
						, t.LogLUBy LogLUBy
 						, t.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on ifiofgdt.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.No
                , "@strFormOutType", TConst.FormOutType.MaVach
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }

        public void InvFInventory_InventoryTransaction_Exec_Block(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, 0.0 QtyChTotalOK 
						, ifiofgdt.Qty QtyChBlockOK -- Phong tỏa.
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG -- Phong tỏa.
						, 0.0 QtyPlanChTotal
						, ifiofgdt.Qty QtyPlanChBlock -- Phong tỏa.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, t.MST MST
 						, t.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];					

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, ifiofg.PMType PMType
						, iibs.SerialNo SerialNo
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, iibs.AgentCode AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, '@strBlockStatus_N' BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPORVE' then '0'	
								else '1'
							end 	
						) FlagNG
						--, iibdt_ub.BlockStatus BlockStatus -- Phong tỏa.
						--, iibdt_ub.FlagNG FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, null OutDTime
						, null OutBy
						, null RefNo_Type -- Phong tỏa.
						, null RefNo_PK -- Phong tỏa.
						--, iibdt_ub.RefNo_Type RefNo_Type -- Phong tỏa.
						--, iibdt_ub.RefNo_PK RefNo_PK -- Phong tỏa.
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYOUTINSTSERIAL' RefType
						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTimeUTC
						, t.LogLUBy LogLUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
 						, t.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on ifiofgdt.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
						--inner join #tbl_Inv_InventoryBalanceSerial_UserBlock iibdt_ub --//[mylock]
						--	on ifiofg.InvCode = iibdt_ub.InvCode
						--		and  ifiofgdtDtl.PartCode = iibdt_ub.PartCode
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];	
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.SYS
                );

            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }

        public void InvFInventory_InventoryTransaction_Exec_Block_NoSerial(string zzzzClauseCol_FunctionName)
        {
            string strSqlSaveTemp = CmUtils.StringUtils.Replace(@"
					---- #tbl_InventoryTransaction:
					select
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, 0.0 QtyChTotalOK 
						, 0.0 QtyChBlockOK -- ifiofgdt.Qty QtyChBlockOK -- Phong tỏa.
						, 0.0 QtyChTotalNG
						, 0.0 QtyChBlockNG -- Phong tỏa.
						, 0.0 QtyPlanChTotal
						, ifiofgdt.Qty QtyPlanChBlock -- Phong tỏa.
						, t.LogLUDTimeUTC CreateDTimeUTC
 						, t.LogLUBy CreateBy
 						, 'zzzzClauseCol_FunctionName' FunctionName
 						, 'INVF_INVENTORYOUTDTL' RefType
 						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
 						, t.MST MST
 						, t.NetworkId NetworkId
					into #tbl_InventoryTransaction 
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
					where (1=1)
					;
 
					select null tbl_InventoryTransaction, * from #tbl_InventoryTransaction t --//[mylock];					

					---- #tbl_Inv_InventoryBalanceSerial:
					select 
						ifiofg.InvCode InvCode
						, ifiofgdt.PartCode PartCode
						, iibs.SerialNo SerialNo
						--, iibdt_ub.SerialNo SerialNo
						, ifiofg.PMType PMType
						, ifiofg.MST MST
						, iibs.PartLotNo PartLotNo
						, iibs.BoxNo BoxNo
						, iibs.CanNo CanNo
						, iibs.AgentCode AgentCode
						, iibs.SecretNo SecretNo
						, iibs.WarrantyDateStart WarrantyDateStart
						, iibs.PackageDate PackageDate
						, iibs.ProductionDate ProductionDate
						, iibs.UserBox UserBox
						, iibs.UserCan UserCan
						, iibs.UserKCS UserKCS
						, iibs.UserCheckPart UserCheckPart
						, '@strBlockStatus_N' BlockStatus
						, (
							case
								when ifiofgis.IF_InvOutFGISStatus = 'APPORVE' then '0'	
								else '1'
							end 	
						) FlagNG
						--, iibdt_ub.BlockStatus BlockStatus -- Phong tỏa.
						--, iibdt_ub.FlagNG FlagNG
						, iibs.FlagMap FlagMap
						, '1' FlagSales
						, '1' FlagUI
						, iibs.FlagBox FlagBox
						, iibs.FlagCan FlagCan
						, iibs.FormInType FormInType
						, iibs.IF_InvInFGNo IF_InvInFGNo
						, ifiofg.FormOutType FormOutType
						, ifiofg.IF_InvOutFGNo IF_InvOutFGNo
						, t.LogLUDTimeUTC LUDTimeUTC
						, t.LogLUBy LUBy
						, iibs.CreateDTimeUTC CreateDTimeUTC
						, iibs.CreateBy CreateBy
						, iibs.InvDTime InvDTime
						, iibs.InvBy InvBy
						, iibs.BoxDTime BoxDTime
						, iibs.BoxBy BoxBy
						, iibs.CanDTime CanDTime
						, iibs.CanBy CanBy
						, null OutDTime
						, null OutBy
						, null RefNo_Type -- Phong tỏa.
						, null RefNo_PK -- Phong tỏa.
						--, iibdt_ub.RefNo_Type RefNo_Type -- Phong tỏa.
						--, iibdt_ub.RefNo_PK RefNo_PK -- Phong tỏa.
						, 'zzzzClauseCol_FunctionName' FunctionName
						, 'INVF_INVENTORYOUTINSTSERIAL' RefType
						, ifiofg.IF_InvOutFGNo RefCode00
 						, null RefCode01
 						, null RefCode02
 						, null RefCode03
 						, null RefCode04
 						, null RefCode05
						, t.LogLUDTimeUTC LogLUDTimeUTC
						, t.LogLUBy LogLUBy
 						, t.NetworkId NetworkId
						, iibs.ShiftInCode ShiftInCode
						, iibs.PrintDate PrintDate
					into #tbl_InventoryTransactionSerial
					from #input_InvF_InventoryOutFG t --//[mylock]
						inner join InvF_InventoryOutFG ifiofg --//[mylock]
							on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
						inner join InvF_InventoryOutFGDtl ifiofgdt --//[mylock]
							on t.IF_InvOutFGNo = ifiofgdt.IF_InvOutFGNo
						inner join InvF_InventoryOutFGInstSerial ifiofgis --//[mylock]
							on t.IF_InvOutFGNo = ifiofgis.IF_InvOutFGNo
								and  ifiofgdt.PartCode = ifiofgis.PartCode
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on ifiofgis.SerialNo = iibs.SerialNo
						--inner join #tbl_Inv_InventoryBalanceSerial_UserBlock iibdt_ub --//[mylock]
						--	on ifiofg.InvCode = iibdt_ub.InvCode
						--		and  ifiofgdt.PartCode = iibdt_ub.PartCode
					where (1=1)
					;
	
					select null tbl_InventoryTransactionSerial, * from #tbl_InventoryTransactionSerial t --//[mylock];	
				"
                , "zzzzClauseCol_FunctionName", zzzzClauseCol_FunctionName.ToUpper()
                , "@strBlockStatus_N", TConst.BlockStatus.SYS
                );


            DataSet dsExec = _cf.db.ExecQuery(
                strSqlSaveTemp
                );
            ////
        }
        #endregion

        #region // InvF_InventoryOutFGInstSerial:
        public void InvF_InventoryOutFGInstSerial_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_InventoryOutFGInstSerial
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_InventoryOutFGInstSerial_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_InventoryOutFGInstSerial", strRt_Cols_InvF_InventoryOutFGInstSerial
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_InventoryOutFGInstSerial = (strRt_Cols_InvF_InventoryOutFGInstSerial != null && strRt_Cols_InvF_InventoryOutFGInstSerial.Length > 0);
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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"						
						---- #tbl_InvF_InventoryOutFGInstSerial_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiifis.IF_InvOutFGNo
							, iiifis.PartCode
							, iiifis.SerialNo
						into #tbl_InvF_InventoryOutFGInstSerial_Filter_Draft
						from InvF_InventoryOutFGInstSerial iiifis --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							iiifis.IF_InvOutFGNo desc
							, iiifis.PartCode desc
							, iiifis.SerialNo desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_InventoryOutFGInstSerial_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_InventoryOutFGInstSerial_Filter:
						select
							t.*
						into #tbl_InvF_InventoryOutFGInstSerial
						from #tbl_InvF_InventoryOutFGInstSerial_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_InventoryOutFGInstSerial ------:
						zzB_Select_InvF_InventoryOutFGInstSerial_zzE
						--------------------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_InventoryOutFGInstSerial_Filter_Draft;
						--drop table #tbl_InvF_InventoryOutFGInstSerial;
					"
                );
            ////
            string zzB_Select_InvF_InventoryOutFGInstSerial_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryOutFGInstSerial)
            {
                #region // bGet_InvF_InventoryOutFGInstSerial:
                zzB_Select_InvF_InventoryOutFGInstSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryInFGDtl:
							select
								t.MyIdxSeq
								, iiifis.*
							from #tbl_InvF_InventoryOutFGInstSerial t --//[mylock]
								inner join InvF_InventoryOutFGInstSerial iiifis --//[mylock]
									on t.IF_InvOutFGNo = iiifis.IF_InvOutFGNo
									    and t.PartCode = iiifis.PartCode
									    and t.SerialNo = iiifis.SerialNo
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
                        , "InvF_InventoryOutFGInstSerial" // strTableNameDB
                        , "InvF_InventoryOutFGInstSerial." // strPrefixStd
                        , "iiifis." // strPrefixAlias
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
                , "zzB_Select_InvF_InventoryOutFGInstSerial_zzE", zzB_Select_InvF_InventoryOutFGInstSerial_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_InventoryOutFGInstSerial)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutFGInstSerial";
            }
            #endregion
        }

        public DataSet InvF_InventoryOutFGInstSerial_Get(
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
            , string strRt_Cols_InvF_InventoryOutFGInstSerial
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryOutFGInstSerial_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutFGInstSerial_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
                , "strRt_Cols_InvF_InventoryOutFGInstSerial", strRt_Cols_InvF_InventoryOutFGInstSerial
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_InventoryOutFGInstSerial_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryOutFGInstSerial_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_InvF_InventoryOutFGInstSerial // strRt_Cols_InvF_InventoryOutFGInstSerial
                                                                   /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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
        public DataSet WAS_InvF_InventoryOutFGInstSerial_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutFGInstSerial objRQ_InvF_InventoryOutFGInstSerial
            ////
            , out RT_InvF_InventoryOutFGInstSerial objRT_InvF_InventoryOutFGInstSerial
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutFGInstSerial.Tid;
            objRT_InvF_InventoryOutFGInstSerial = new RT_InvF_InventoryOutFGInstSerial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutFGInstSerial.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutFGInstSerial_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutFGInstSerial_Get;
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
                List<InvF_InventoryOutFGInstSerial> lst_InvF_InventoryOutFGInstSerial = new List<InvF_InventoryOutFGInstSerial>();
                /////
                bool bGet_InvF_InventoryOutFGInstSerialInstSerial = (objRQ_InvF_InventoryOutFGInstSerial.Rt_Cols_InvF_InventoryOutFGInstSerial != null && objRQ_InvF_InventoryOutFGInstSerial.Rt_Cols_InvF_InventoryOutFGInstSerial.Length > 0);
                #endregion

                #region // WS_InvF_InventoryOutFGInstSerial_Get:
                mdsResult = InvF_InventoryOutFGInstSerial_Get(
                    objRQ_InvF_InventoryOutFGInstSerial.Tid // strTid
                    , objRQ_InvF_InventoryOutFGInstSerial.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutFGInstSerial.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutFGInstSerial.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutFGInstSerial.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutFGInstSerial.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_InventoryOutFGInstSerial.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_InventoryOutFGInstSerial.Ft_WhereClause // strFt_WhereClause
                                                                         //// Return:
                    , objRQ_InvF_InventoryOutFGInstSerial.Rt_Cols_InvF_InventoryOutFGInstSerial // Rt_Cols_InvF_InventoryOutFGInstSerialInstSerial
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_InventoryOutFGInstSerial.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    if (bGet_InvF_InventoryOutFGInstSerialInstSerial)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutFGInstSerial = mdsResult.Tables["InvF_InventoryOutFGInstSerial"].Copy();
                        lst_InvF_InventoryOutFGInstSerial = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutFGInstSerial>(dt_InvF_InventoryOutFGInstSerial);
                        objRT_InvF_InventoryOutFGInstSerial.Lst_InvF_InventoryOutFGInstSerial = lst_InvF_InventoryOutFGInstSerial;
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

        #region // Inv_InventoryBalanceSerial:
    //    public void Inv_InventoryBalanceSerial_GetX(
    //        ref ArrayList alParamsCoupleError
    //        , DateTime dtimeTDateTime
    //        , string strTid
    //        , string strWAUserCode
    //        ////
    //        //// Filter:
    //        , string strFt_RecordStart
    //        , string strFt_RecordCount
    //        , string strFt_WhereClause
    //        //// Return:
    //        , string strRt_Cols_Inv_InventoryBalanceSerial
    //        ////
    //        , out DataSet dsGetData
    //        )
    //    {
    //        #region // Temp:
    //        string strFunctionName = "Inv_InventoryBalanceSerial_GetX";
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName // FunctionName
				//, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
    //            ////
				//, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				//, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				//, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
    //            ////
    //            , "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
    //            });
    //        #endregion

    //        #region // Check:
    //        //// Refine:
    //        long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
    //        long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
    //        bool bGet_Inv_InventoryBalanceSerial = (strRt_Cols_Inv_InventoryBalanceSerial != null && strRt_Cols_Inv_InventoryBalanceSerial.Length > 0);
    //        #endregion

    //        #region // Build Sql:
    //        ////
    //        ArrayList alParamsCoupleSql = new ArrayList();
    //        alParamsCoupleSql.AddRange(new object[] {
    //                "@nFilterRecordStart", nFilterRecordStart
    //                , "@nFilterRecordEnd", nFilterRecordEnd
    //                });
    //        ////		
    //        // drAbilityOfUser:
    //        DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
    //        zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
    //            drAbilityOfUser // drAbilityOfUser
    //            , ref alParamsCoupleError // alParamsCoupleError
    //            );
    //        string strSqlGetData = CmUtils.StringUtils.Replace(@"
				//		---- #tbl_Inv_InventoryBalanceSerial_Filter_Draft:
				//		select distinct
				//			identity(bigint, 0, 1) MyIdxSeq
				//			, iiof.SerialNo
				//		into #tbl_Inv_InventoryBalanceSerial_Filter_Draft
				//		from Inv_InventoryBalanceSerial iiof --//[mylock]
    //                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
    //                            on iiof.MST = t_MstNNT_View.MST
				//			left join Inv_InventorySecret iisc --//[mylock]
				//				on iiof.SerialNo = iisc.SerialNo
				//			left join Inv_InventoryBox iib --//[mylock]
				//				on iiof.BoxNo = iib.BoxNo
				//			left join Inv_InventoryCarton iican --//[mylock]
				//				on iiof.CanNo = iican.CanNo
				//		where (1=1)
				//			zzB_Where_strFilter_zzE
				//		order by iiof.SerialNo desc
				//		;

				//		---- Summary:
				//		select Count(0) MyCount from #tbl_Inv_InventoryBalanceSerial_Filter_Draft t --//[mylock]
				//		;

				//		---- #tbl_Inv_InventoryBalanceSerial_Filter:
				//		select
				//			t.*
				//		into #tbl_Inv_InventoryBalanceSerial_Filter
				//		from #tbl_Inv_InventoryBalanceSerial_Filter_Draft t --//[mylock]
				//		where
				//			(t.MyIdxSeq >= @nFilterRecordStart)
				//			and (t.MyIdxSeq <= @nFilterRecordEnd)
				//		;

				//		-------- Inv_InventoryBalanceSerial ------:
				//		zzB_Select_Inv_InventoryBalanceSerial_zzE
				//		-----------------------------------------

				//		---- Clear for debug:
				//		--drop table #tbl_Inv_InventoryBalanceSerial_Filter_Draft;
				//		--drop table #tbl_Inv_InventoryBalanceSerial_Filter;
				//	"
    //            );
    //        ////
    //        string zzB_Select_Inv_InventoryBalanceSerial_zzE = "-- Nothing.";
    //        if (bGet_Inv_InventoryBalanceSerial)
    //        {
    //            #region // bGet_Inv_InventoryBalanceSerial:
    //            zzB_Select_Inv_InventoryBalanceSerial_zzE = CmUtils.StringUtils.Replace(@"
				//			---- Inv_InventoryBalanceSerial:
				//			select
				//				t.MyIdxSeq
				//				, iiof.*
				//				, mg.MST mg_AgentCode
				//				, mg.NNTFullName mg_AgentName
				//				, mp.PartCode mp_PartCode
				//				, mp.PartName mp_PartName
				//				, iisc.QR_SerialNo
				//				, iib.QR_BoxNo
				//				, iican.QR_CanNo
				//			from #tbl_Inv_InventoryBalanceSerial_Filter t --//[mylock]
				//				inner join Inv_InventoryBalanceSerial iiof --//[mylock]
				//					on t.SerialNo = iiof.SerialNo
				//				left join MST_NNT mg --//[mylock]
				//					on iiof.AgentCode = mg.MST
				//				left join Mst_Part mp --//[mylock]
				//					on iiof.Partcode = mp.Partcode
				//				left join Inv_InventorySecret iisc --//[mylock]
				//					on iiof.SerialNo = iisc.SerialNo
				//				left join Inv_InventoryBox iib --//[mylock]
				//					on iiof.BoxNo = iib.BoxNo
				//				left join Inv_InventoryCarton iican --//[mylock]
				//					on iiof.CanNo = iican.CanNo
				//			order by t.MyIdxSeq asc
				//			;
				//		"
    //                );
    //            #endregion
    //        }
    //        ////
    //        string zzB_Where_strFilter_zzE = "";
    //        {
    //            Hashtable htSpCols = new Hashtable();
    //            {
    //                #region // htSpCols:
    //                ////
    //                TUtils.CUtils.MyBuildHTSupportedColumns(
    //                    _cf.db // db
    //                    , ref htSpCols // htSupportedColumns
    //                    , "Inv_InventoryBalanceSerial" // strTableNameDB
    //                    , "Inv_InventoryBalanceSerial." // strPrefixStd
    //                    , "iiof." // strPrefixAlias
    //                    );
    //                ////
    //                TUtils.CUtils.MyBuildHTSupportedColumns(
    //                    _cf.db // db
    //                    , ref htSpCols // htSupportedColumns
    //                    , "Inv_InventorySecret" // strTableNameDB
    //                    , "Inv_InventorySecret." // strPrefixStd
    //                    , "iisc." // strPrefixAlias
    //                    );
    //                ////
    //                TUtils.CUtils.MyBuildHTSupportedColumns(
    //                    _cf.db // db
    //                    , ref htSpCols // htSupportedColumns
    //                    , "Inv_InventoryBox" // strTableNameDB
    //                    , "Inv_InventoryBox." // strPrefixStd
    //                    , "iib." // strPrefixAlias
    //                    );
    //                ////
    //                TUtils.CUtils.MyBuildHTSupportedColumns(
    //                    _cf.db // db
    //                    , ref htSpCols // htSupportedColumns
    //                    , "Inv_InventoryCarton" // strTableNameDB
    //                    , "Inv_InventoryCarton." // strPrefixStd
    //                    , "iican." // strPrefixAlias
    //                    );
    //                ////
    //                #endregion
    //            }
    //            zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
    //                htSpCols // htSpCols
    //                , strFt_WhereClause // strClause
    //                , "@p_" // strParamPrefix
    //                , ref alParamsCoupleSql // alParamsCoupleSql
    //                );
    //            zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
    //            alParamsCoupleError.AddRange(new object[]{
    //                    "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                    });
    //        }
    //        ////
    //        strSqlGetData = CmUtils.StringUtils.Replace(
    //            strSqlGetData
    //            , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //            , "zzB_Select_Inv_InventoryBalanceSerial_zzE", zzB_Select_Inv_InventoryBalanceSerial_zzE
    //            );
    //        #endregion

    //        #region // Get Data:
    //        dsGetData = _cf.db.ExecQuery(
    //            strSqlGetData
    //            , alParamsCoupleSql.ToArray()
    //            );
    //        int nIdxTable = 0;
    //        dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
    //        if (bGet_Inv_InventoryBalanceSerial)
    //        {
    //            dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalanceSerial";
    //        }
    //        #endregion
    //    }

    //    public DataSet Inv_InventoryBalanceSerial_Get(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        //// Filter:
    //        , string strFt_RecordStart
    //        , string strFt_RecordCount
    //        , string strFt_WhereClause
    //        //// Return:
    //        , string strRt_Cols_Inv_InventoryBalanceSerial
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        Stopwatch stopWatchFunc = new Stopwatch();
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        bool bNeedTransaction = true;
    //        string strFunctionName = "Inv_InventoryBalanceSerial_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_Get;
    //        ArrayList alParamsCoupleSW = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // SW:				
    //            stopWatchFunc.Start();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            if (bNeedTransaction) _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            // Sys_User_CheckAuthentication:
    //            Sys_User_CheckAuthentication(
    //                ref alParamsCoupleError
    //                , strWAUserCode
    //                , strWAUserPassword
    //                );

    //            // Check Access/Deny:
    //            Sys_Access_CheckDenyV30(
    //                ref alParamsCoupleError
    //                , strWAUserCode
    //                , strFunctionName
    //                );
    //            #endregion

    //            #region // Inv_InventoryBalanceSerial_GetX:
    //            DataSet dsGetData = new DataSet();
    //            {
    //                Inv_InventoryBalanceSerial_GetX(
    //                    ref alParamsCoupleError // alParamsCoupleError
    //                    , dtimeSys // dtimeSys
    //                    , strTid // strTid
    //                    , strWAUserCode // strWAUserCode
    //                                    ////
    //                    , strFt_RecordStart // strFt_RecordStart
    //                    , strFt_RecordCount // strFt_RecordCount
    //                    , strFt_WhereClause // strFt_WhereClause
    //                                        ////
    //                    , strRt_Cols_Inv_InventoryBalanceSerial // strRt_Cols_Inv_InventoryBalanceSerial
    //                                                               /////
    //                    , out dsGetData // dsGetData
    //                    );
    //            }
    //            #endregion

    //            #region // Get Data:
    //            CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            stopWatchFunc.Stop();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
    //                });

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleSW // alParamsCoupleSW
    //                );
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Inv_InventoryBalanceSerial_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial
    //        ////
    //        , out RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Inv_InventoryBalanceSerial.Tid;
    //        objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Inv_InventoryBalanceSerial_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
    //            List<Inv_InventoryBalanceSerial> lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
    //            /////
    //            bool bGet_Inv_InventoryBalanceSerial = (objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial != null && objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial.Length > 0);
    //            #endregion

    //            #region // WS_Inv_InventoryBalanceSerial_Get:
    //            mdsResult = Inv_InventoryBalanceSerial_Get(
    //                objRQ_Inv_InventoryBalanceSerial.Tid // strTid
    //                , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
    //                , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
    //                , objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
    //                , objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Inv_InventoryBalanceSerial.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Inv_InventoryBalanceSerial.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Inv_InventoryBalanceSerial.Ft_WhereClause // strFt_WhereClause
    //                                                           //// Return:
    //                , objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial // strRt_Cols_Inv_InventoryBalanceSerial
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                objRT_Inv_InventoryBalanceSerial.MySummaryTable = lst_MySummaryTable[0];
    //                ////
    //                if (bGet_Inv_InventoryBalanceSerial)
    //                {
    //                    ////
    //                    DataTable dt_Inv_InventoryBalanceSerial = mdsResult.Tables["Inv_InventoryBalanceSerial"].Copy();
    //                    lst_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalanceSerial>(dt_Inv_InventoryBalanceSerial);
    //                    objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial = lst_Inv_InventoryBalanceSerial;
    //                }
    //                ////
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }
        private void Inv_InventoryBalanceSerial_MapX(
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
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_InventoryBalanceSerial_MapX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input:
            string strMST = TUtils.CUtils.StdParam(objMST);
            /////
            {
                ////
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                        , "Check.strMST", strMST
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////
            }
            #endregion

            #region //// Refine and Check Input Inv_InventoryBalanceSerial:
            ////
            DataTable dtInput_Inv_InventoryBalanceSerial = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalanceSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBalanceSerial = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Inv_InventoryBalanceSerial.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBalanceSerial // dtData
                    , "StdParam", "InvCode" // arrstrCouple
                    , "StdParam", "PartCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    , "", "ProductionDate" // arrstrCouple
                    , "", "PackageDate" // arrstrCouple
                    , "StdParam", "AgentCode" // arrstrCouple
                    , "StdParam", "PartLotNo" // arrstrCouple
                    , "StdParam", "ShiftInCode" // arrstrCouple
                    , "", "PrintDate" // arrstrCouple
                    , "StdParam", "BoxNo" // arrstrCouple
                    , "StdParam", "CanNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBalanceSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBalanceSerial.Rows[nScan];
                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalanceSerial:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalanceSerial" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "ProductionDate", TConst.BizMix.Default_DBColType
                            , "PackageDate", TConst.BizMix.Default_DBColType
                            , "AgentCode", TConst.BizMix.Default_DBColType
                            , "PartLotNo", TConst.BizMix.Default_DBColType
                            , "ShiftInCode", TConst.BizMix.Default_DBColType
                            , "PrintDate", TConst.BizMix.Default_DBColType
                            , "BoxNo", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBalanceSerial // dtData
                );
            }
            #endregion

            #region // Check All Conditional:
            {
                #region // Check : Serial chưa nhập kho.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                left join Inv_InventoryBalanceSerial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.SerialNo is not null
                            ;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.Serial.ExistInInv", dtCheck.Rows[0]["SerialNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_ExistSerialInInv
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Serial phải có trong kho sinh số.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                left join Inv_InventorySecret f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.SerialNo is null
                            ;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.Serial.ExistInInv", dtCheck.Rows[0]["SerialNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_SerialNoExistInInvGen
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Serial đã được xuất excel.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                                , f.FlagUsed
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                left join Inv_InventorySecret f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.SerialNo is not null
                                and f.FlagUsed = '0'
                            ;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.Serial", dtCheck.Rows[0]["SerialNo"]
                            , "Check.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_SerialNoNotExportExcel
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Serial trong kho gen phải có MST trùng với MST của user xuất.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                                , f.MST
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                inner join Inv_InventorySecret f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.MST <> '@strMST'
                            ;
                        "
                        , "@strMST", strMST
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.Serial", dtCheck.Rows[0]["SerialNo"]
                            , "Check.DB.SerialInvGen.MST", dtCheck.Rows[0]["MST"]
                            , "Check.Input.MST", strMST
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_SerialNoNotExistInInvGenOfMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Box phải tồn tại trong kho sinh số.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial_Box t --//[mylock]
                                left join Inv_InventoryBox f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                                and f.BoxNo is null
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.BoxNo.ExistInInv", dtCheck.Rows[0]["BoxNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGen
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Box Phải đã xuất.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagUsed
                            from #input_Inv_InventoryBalanceSerial_Box t --//[mylock]
                                left join Inv_InventoryBox f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                                and f.BoxNo is not null
                                and f.FlagUsed = '0'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.BoxNo", dtCheck.Rows[0]["BoxNo"]
                            , "Check.DB.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_BoxNoNoOutExcel
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : BoxNo phải tồn tại trong kho của MST.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
	                            , mnnt.MST
                            from #input_Inv_InventoryBalanceSerial_Box t --//[mylock]
                                inner join Inv_InventoryBox f --//[mylock]
                                    on t.BoxNo = f.BoxNo
	                            inner join Mst_NNT mnnt --//[mylock]
		                            on f.OrgID = mnnt.OrgID
                            where(1=1)
                                and mnnt.MST <> '@strMST'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        , "@strMST", strMST
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.BoxNo", dtCheck.Rows[0]["BoxNo"]
                            , "Check.DB.BOXInvGen.MST", dtCheck.Rows[0]["MST"]
                            , "Check.Input.MST", strMST
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGenOfMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Box phải chưa được map.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagMap
                            from #input_Inv_InventoryBalanceSerial_Box t --//[mylock]
                                left join Inv_InventoryBox f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                                and f.BoxNo is not null
                                and f.FlagMap = '1'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.BoxNo", dtCheck.Rows[0]["BoxNo"]
                            , "Check.DB.FlagMap", dtCheck.Rows[0]["FlagMap"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_BoxNoExistMap
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải tồn tại trong kho sinh số.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is null
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo.ExistInInv", dtCheck.Rows[0]["CanNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGen
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải đã xuất.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagUsed
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is not null
                                and f.FlagUsed = '0'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_CanNoNoExportExcel
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải tồn tại trong kho sinh số của MST.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
	                            , mnnt.MST
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                inner join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
	                            inner join Mst_NNT mnnt --//[mylock]
		                            on f.OrgID = mnnt.OrgID
                            where(1=1)
                                and mnnt.MST <> '@strMST'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        , "@strMST", strMST
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.DB.CanInvGen.MST", dtCheck.Rows[0]["MST"]
                            , "Check.Input.MST", strMST
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGenOfMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải chưa được map.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagMap
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is not null
                                and f.FlagMap = '1'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.FlagMap", dtCheck.Rows[0]["FlagMap"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_CanNoExistedMap
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion
            }
            #endregion

            #region // Build InvF_InventoryInFG and Save InvF_InventoryInFG:
            {
                #region // Build: InvF_InventoryInFG.
                string strIF_InvInFGNo = "";
                string strInvInType = "FG";
                string strInvCode = TUtils.CUtils.StdParam(dtInput_Inv_InventoryBalanceSerial.Rows[0]["InvCode"]);
                //string strFormInType = TConst.FormInType.MaVach;
                string strRemark = "";
                DataSet dsInput = new DataSet();
                /////
                {
                    #region // Build: IF_InvInFGNo.
                    strIF_InvInFGNo = Seq_Common_MyGet(
                        ref alParamsCoupleError // alParamsCoupleError
                        , TConst.SeqType.IFInvInFGNo // strSequenceType
                        , "" // strParam_Prefix
                        , "" // strParam_Postfix
                        );
                    ////
                    #endregion

                    #region // Build: InvF_InventoryInFG.
                    #endregion

                    #region // Build: InvF_InventoryInFGDtl.
                    DataTable dtInput_InvF_InventoryInFGDtl = new DataTable("InvF_InventoryInFGDtl");
                    {
                        string strSqlCheckInvF_InventoryInFGDtl = CmUtils.StringUtils.Replace(@"
                                ---- #tbl_InvF_InventoryInFGDtl_Build:
                                select 
                                    t.PartCode
                                    , Count(t.SerialNo) Qty
                                into #tbl_InvF_InventoryInFGDtl_Build
                                from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                where(1=1)
                                group by
                                    t.PartCode
                                ;

                                --- Return:
                                select 
                                    t.PartCode
                                    , '@strLogLUDTimeUTC' ProductionDate
                                    , t.Qty
                                from #tbl_InvF_InventoryInFGDtl_Build t --//[mylock]
                                where(1=1)
                                ;

                                ---- Clear For Debug:
                                drop table #tbl_InvF_InventoryInFGDtl_Build;
					            
				            "
                            , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd")
                            );

                        dtInput_InvF_InventoryInFGDtl = _cf.db.ExecQuery(strSqlCheckInvF_InventoryInFGDtl).Tables[0].Copy();
                        dtInput_InvF_InventoryInFGDtl.TableName = "InvF_InventoryInFGDtl";
                        dsInput.Tables.Add(dtInput_InvF_InventoryInFGDtl);
                    }
                    #endregion

                    #region // Build: InvF_InventoryInFGInstSerial.
                    DataTable dtInput_InvF_InventoryInFGInstSerial = new DataTable("InvF_InventoryInFGInstSerial");
                    {
                        string strSqlCheckInvF_InventoryInFGInstSerial = CmUtils.StringUtils.Replace(@"
                                --- Return:
                                select 
                                    t.PartCode
                                    , t.SerialNo
                                    , t.ProductionDate
                                    , t.PackageDate
                                    , t.AgentCode
                                    , t.PartLotNo
                                    , t.ShiftInCode
                                    , t.PrintDate
                                    , t.CanNo
                                    , t.BoxNo
                                from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                where(1=1)
                                ;					            
				            "
                            );

                        dtInput_InvF_InventoryInFGInstSerial = _cf.db.ExecQuery(strSqlCheckInvF_InventoryInFGInstSerial).Tables[0].Copy();
                        dtInput_InvF_InventoryInFGInstSerial.TableName = "InvF_InventoryInFGInstSerial";
                        dsInput.Tables.Add(dtInput_InvF_InventoryInFGInstSerial);

                    }
                    #endregion

                }
                #endregion

                #region // InvF_InventoryInFG_SaveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryInFG_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , "0" // objFlagIsDelete
                                          ////
                        , strIF_InvInFGNo // objIF_InvInFGNo
                        , objMST // objMST
                        , strInvInType // objInvInType
                        , TConst.FormInType.MaVach // objFormInType
                        , strInvCode // objInvCode
                        , "" // objPMType
                        , strRemark // objRemark
                                    ////
                        , dsInput  // dsData
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // InvF_InventoryInFG_ApproveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryInFG_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strIF_InvInFGNo // objIF_InvInFGNo
                        , TConst.FormInType.MaVach
                        , strRemark // objRemark
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion
            }
            #endregion 

            #region // Check All Conditional:
            {
                #region // Check : 1 Serial - 1 Box.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- check:
                            select 
                                t.BoxNo
                                , count(t.SerialNo) QtySerialNo
                            from Inv_InventoryBalanceSerial t --//[mylock]
                                inner join #input_Inv_InventoryBalanceSerial_Box f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                            group by 
                                t.BoxNo
                            having count(t.SerialNo) > 1
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.BoxNo", dtCheck.Rows[0]["BoxNo"]
                            , "Check.RowNumber", dtCheck.Rows[0]["QtySerialNo"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGen
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion
            }
            #endregion

            #region // Update FlagMap:
            {
                #region // Inv_InventoryBox : FlagMap.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.BoxNo
                            into #input_Inv_InventoryBalanceSerial_Box
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.BoxNo is not null
                            ;

                            ---- update:
                            update t 
                            set
                                t.FlagMap = '1'
                                , t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
                                , t.LogLUBy = '@strLogLUBy'
                            from Inv_InventoryBox t --//[mylock]
                                inner join #input_Inv_InventoryBalanceSerial_Box f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Box;
                        "
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );
                    //
                    _cf.db.ExecQuery(strSql_Check);
                    ////
                }
                #endregion

                #region // Inv_InventoryCarton : FlagMap.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- update:
                            update t 
                            set
                                t.FlagMap = '1'
                                , t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
                                , t.LogLUBy = '@strLogLUBy'
                            from Inv_InventoryCarton t --//[mylock]
                                inner join #input_Inv_InventoryBalanceSerial_CanNo f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );
                    //
                    _cf.db.ExecQuery(strSql_Check);
                    ////
                }
                #endregion

                #region // Inv_InventorySecret : FlagMap.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.SerialNo
                            into #input_Inv_InventoryBalanceSerial_Serial
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                            ;

                            ---- update:
                            update t 
                            set
                                t.FlagMap = '1'
                                , t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
                                , t.LogLUBy = '@strLogLUBy'
                            from Inv_InventorySecret t --//[mylock]
                                inner join #input_Inv_InventoryBalanceSerial_Serial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_Serial;
                        "
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );
                    //
                    _cf.db.ExecQuery(strSql_Check);
                    ////
                }
                #endregion
            }
            #endregion 

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventoryBalanceSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
        #endregion

        // Return Good:
            return;
        }
        public DataSet Inv_InventoryBalanceSerial_Map(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objMST
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_InventoryBalanceSerial_Map";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_Map;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objMST", objMST
                    ////
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

                #region // Inv_InventoryBalanceSerial_MapX:
                //DataSet dsGetData = null;
                {
                    Inv_InventoryBalanceSerial_MapX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objMST // objMST
                        /////
                        , dsData // dsData
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

        public DataSet WAS_Inv_InventoryBalanceSerial_Map(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial
            ////
            , out RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBalanceSerial.Tid;
            objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBalanceSerial_Map";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_Map;
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
                //List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial == null)
                        objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
                    {
                        DataTable dt_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalanceSerial>(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial, "Inv_InventoryBalanceSerial");
                        dsData.Tables.Add(dt_Inv_InventoryBalanceSerial);
                    }
                }
                #endregion

                #region // Inv_InventoryBalanceSerial_Map:
                //string strMST = TUtils.CUtils.StdParam(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial[0].MST);

                //mdsResult = Inv_InventoryBalanceSerial_Map(
                //    objRQ_Inv_InventoryBalanceSerial.Tid // strTid
                //    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                //    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                //    , objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
                //    , objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
                //    , ref alParamsCoupleError // alParamsCoupleError
                //                              //// Filter:
                //    , strMST // objMST
                //    , dsData // dsData
                //    );
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

        private void Inv_InventoryBalanceSerial_UpdCanX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_InventoryBalanceSerial_UpdCanX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
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

            #region // Refine and Check Inv_InventoryBalanceSerial:
            DataTable dtInput_Inv_InventoryBalanceSerial = null;
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            {
                ////
                string strTableCheck = "Inv_InventoryBalanceSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBalanceSerial = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Inv_InventoryBalanceSerial.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBalanceSerial // dtData
                    , "StdParam", "SerialNo" // arrstrCouple
                    , "StdParam", "CanNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBalanceSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBalanceSerial.Rows[nScan];
                    ////
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventorySecret:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalanceSerial" // strTableName
                    , new object[] {
                            "SerialNo", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBalanceSerial // dtData
                );
            }
            #endregion

            #region // Check All Conditional:
            {
                #region // Check : Serial tồn tại.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                left join Inv_InventoryBalanceSerial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and f.SerialNo is null
                            ;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.Serial.NotExist", dtCheck.Rows[0]["SerialNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InvalidSerial
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải tồn tại trong kho sinh số.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is null
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo.ExistInInv", dtCheck.Rows[0]["CanNo"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGen
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải đã xuất.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagUsed
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is not null
                                and f.FlagUsed = '0'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_CanNoNoExportExcel
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải chưa được map.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
                                , f.FlagMap
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                left join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is not null
                                and f.FlagMap = '1'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.FlagMap", dtCheck.Rows[0]["FlagMap"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_CanNoExistedMap
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // Check : Can phải tồn tại trong kho sinh số của MST.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_CanNo:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- check:
                            select 
                                t.*
	                            , mnnt.MST
                            from #input_Inv_InventoryBalanceSerial_CanNo t --//[mylock]
                                inner join Inv_InventoryCarton f --//[mylock]
                                    on t.CanNo = f.CanNo
	                            inner join Mst_NNT mnnt --//[mylock]
		                            on f.OrgID = mnnt.OrgID
                            where(1=1)
                                and mnnt.MST <> '@strMST'
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        , "@strMST", drAbilityOfUser["MST"]
                        );
                    //
                    DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                    ////
                    if (dtCheck.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.CanNo", dtCheck.Rows[0]["CanNo"]
                            , "Check.DB.CanInvGen.MST", dtCheck.Rows[0]["MST"]
                            , "Check.Input.MST", drAbilityOfUser["MST"]
                            , "Check.RowNumber", dtCheck.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGenOfMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion
            }
            #endregion

            #region // Check : Serial Chưa được sử dụng.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                            , f.FlagCan
                        from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            inner join Inv_InventoryBalanceSerial f --//[mylock]
                                on t.SerialNo = f.SerialNo
                        where(1=1)
                            and f.FlagCan = '1' -- đã sử dụng
                        ;
                    "
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.Serial", dtCheck.Rows[0]["SerialNo"]
                        , "Check.FlagCan", dtCheck.Rows[0]["FlagCan"]
                        , "Check.FlagCan.Expected", TConst.Flag.No
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InvalidFlagCan
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // SaveDB:
            {
                ////
                string zzzzClauseUpdate_Inv_InventoryBalanceSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Update:
                                update t 
                                set
                                    t.FlagCan = '1'
                                    , t.CanNo = f.CanNo
                                    , t.LogLUDTimeUTC = f.LogLUDTimeUTC
                                    , t.LogLUBy = f.LogLUBy
                                from Inv_InventoryBalanceSerial t --//[mylock]
                                    inner join #input_Inv_InventoryBalanceSerial f --//[mylock]
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                ;
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseUpdate_Inv_InventoryBalanceSerial_zSave			
								----
							"
                    , "zzzzClauseUpdate_Inv_InventoryBalanceSerial_zSave", zzzzClauseUpdate_Inv_InventoryBalanceSerial_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region // Check : Mã số thuế khác MST đăng nhập:
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                            , f.MST
                        from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            inner join Inv_InventoryBalanceSerial f --//[mylock]
                                on t.SerialNo = f.SerialNo
                        where(1=1)
                            and f.MST != '@strMST'
                        ;
                    "
                    , "@strMST", drAbilityOfUser["MST"]
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.Serial", dtCheck.Rows[0]["SerialNo"]
                        , "Check.MST", dtCheck.Rows[0]["MST"]
                        , "Check.MST.Expected", drAbilityOfUser["MST"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Update FlagMap:
            {
                #region // Inv_InventoryCarton : FlagMap.
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- #input_Inv_InventoryBalanceSerial_Box:
                            select distinct
                                t.CanNo
                            into #input_Inv_InventoryBalanceSerial_CanNo
                            from #input_Inv_InventoryBalanceSerial t --//[mylock]
                            where(1=1)
                                and t.CanNo is not null
                            ;

                            ---- update:
                            update t 
                            set
                                t.FlagMap = '1'
                                , t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
                                , t.LogLUBy = '@strLogLUBy'
                            from Inv_InventoryCarton t --//[mylock]
                                inner join #input_Inv_InventoryBalanceSerial_CanNo f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                            ;

                            ---- Clear For Debug:
                            drop table #input_Inv_InventoryBalanceSerial_CanNo;
                        "
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );
                    //
                    _cf.db.ExecQuery(strSql_Check);
                    ////
                }
                #endregion
            }
            #endregion 

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventoryBalanceSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet Inv_InventoryBalanceSerial_UpdCan(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_InventoryBalanceSerial_UpdCan";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCan;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
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

                #region // Convert Input:
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Inv_InventoryBalanceSerial_UpdCanX:
                //DataSet dsGetData = null;
                {
                    Inv_InventoryBalanceSerial_UpdCanX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dsData // dsData
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

        public DataSet WAS_Inv_InventoryBalanceSerial_UpdCan(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial
            ////
            , out RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBalanceSerial.Tid;
            objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBalanceSerial_UpdCan";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_UpdCan;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Lst_Inv_InventoryBalanceSerial", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Invoice_TempInvoice> lst_Invoice_TempInvoice = new List<Invoice_TempInvoice>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalanceSerial>(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial, "Inv_InventoryBalanceSerial");
                    dsData.Tables.Add(dt_Inv_InventoryBalanceSerial);

                }
                #endregion

                #region // Inv_InventoryBalanceSerial_UpdCan:
                mdsResult = Inv_InventoryBalanceSerial_UpdCan(
                    objRQ_Inv_InventoryBalanceSerial.Tid // strTid
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData
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

        public DataSet Inv_InventoryBalanceSerial_UpdCanFromBox(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_InventoryBalanceSerial_UpdCanFromBox";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanFromBox;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
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

                #region // Refine and Check Inv_InventoryBalanceSerial:
                DataTable dtInput_Inv_InventoryBalanceSerial = null;
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                {
                    ////
                    string strTableCheck = "Inv_InventoryBalanceSerial";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Inv_InventoryBalanceSerial = dsData.Tables[strTableCheck];
                    ////
                    if (dtInput_Inv_InventoryBalanceSerial.Rows.Count < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialInvalid
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Inv_InventoryBalanceSerial // dtData
                        , "StdParam", "BoxNo" // arrstrCouple
                        , "StdParam", "CanNo" // arrstrCouple
                        );
                    ////
                }
                #endregion

                #region //// SaveTemp Inv_InventoryBalanceSerial:
                {
                    // Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#input_Box_Can" // strTableName
                        , new object[] {
                            "BoxNo", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                            } // arrSingleStructure
                        , dtInput_Inv_InventoryBalanceSerial // dtData
                    );
                }
                #endregion

                #region // Build Inv_InventoryBalanceSerial and Save Inv_InventoryBalanceSerial:
                {
                    #region // Build: Inv_InventoryBalanceSerial.
                    DataSet dsInput = new DataSet();
                    /////
                    {
                        #region // Build: Inv_InventoryBalanceSerial.
                        DataTable dt_Inv_InventoryBalanceSerial = new DataTable("Inv_InventoryBalanceSerial");
                        {
                            string strSqlCheckInv_InventoryBalanceSerial = CmUtils.StringUtils.Replace(@"                                   
                                    ---- #tbl_Inv_InventoryBalanceSerial_Box:
                                    select distinct 
	                                    t.SerialNo
	                                    , t.BoxNo
                                    into #tbl_Inv_InventoryBalanceSerial_Box
                                    from Inv_InventoryBalanceSerial t --//[mylock]
	                                    inner join #input_Box_Can f --//[mylock]
		                                    on t.BoxNo = f.BoxNo
                                    where(1=1)
                                    ;

                                    ---- Return:
                                    select 
	                                    t.SerialNo
	                                    , t.BoxNo
	                                    , f.CanNo
                                    from #tbl_Inv_InventoryBalanceSerial_Box t --//[mylock]
	                                    inner join #input_Box_Can f --//[mylock]
		                                    on t.BoxNo = f.BoxNo
                                    where(1=1)
                                    ;

                                    --- Clear For Debug:
                                    drop table #tbl_Inv_InventoryBalanceSerial_Box;
					            
				                "
                                );

                            dt_Inv_InventoryBalanceSerial = _cf.db.ExecQuery(strSqlCheckInv_InventoryBalanceSerial).Tables[0].Copy();
                            dt_Inv_InventoryBalanceSerial.TableName = "Inv_InventoryBalanceSerial";
                            dsInput.Tables.Add(dt_Inv_InventoryBalanceSerial);
                        }
                        #endregion

                    }
                    #endregion

                    #region // Inv_InventoryBalanceSerial_UpdCanX:
                    //DataSet dsGetData = null;
                    {
                        Inv_InventoryBalanceSerial_UpdCanX(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , dsInput // dsData
                            );
                    }
                    ////
                    //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                    #endregion
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

        public DataSet WAS_Inv_InventoryBalanceSerial_UpdCanFromBox(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial
            ////
            , out RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBalanceSerial.Tid;
            objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBalanceSerial_UpdCanFromBox";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_UpdCanFromBox;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Lst_Inv_InventoryBalanceSerial", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Invoice_TempInvoice> lst_Invoice_TempInvoice = new List<Invoice_TempInvoice>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalanceSerial>(objRQ_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial, "Inv_InventoryBalanceSerial");
                    dsData.Tables.Add(dt_Inv_InventoryBalanceSerial);

                }
                #endregion

                #region // Inv_InventoryBalanceSerial_UpdCanFromBox:
                mdsResult = Inv_InventoryBalanceSerial_UpdCanFromBox(
                    objRQ_Inv_InventoryBalanceSerial.Tid // strTid
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData
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

        private void Inv_InventoryBalanceSerial_OutInvX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_InventoryBalanceSerial_OutInvX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objMST", objMST
                , "objFormOutType", objFormOutType
                , "objInvOutType", objInvOutType
                , "objInvCode", objInvCode
                , "objPMType", objPMType
                , "objInvFOutType", objInvFOutType
                , "objPlateNo", objPlateNo
                , "objMoocNo", objMoocNo
                , "objDriverName", objDriverName
                , "objDriverPhoneNo", objDriverPhoneNo
                , "objAgentCode", objAgentCode
                , "objCustomerName", objCustomerName
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

            #region // Refine and Check Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            ////
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strFormOutType = TUtils.CUtils.StdParam(objFormOutType);
            string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
            string strInvCode = TUtils.CUtils.StdParam(objInvCode);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strInvFOutType = TUtils.CUtils.StdParam(objInvFOutType);
            string strPlateNo = string.Format("{0}", objPlateNo).Trim();
            string strMoocNo = string.Format("{0}", objMoocNo).Trim();
            string strDriverName = string.Format("{0}", objDriverName).Trim();
            string strDriverPhoneNo = string.Format("{0}", objDriverPhoneNo).Trim();
            string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);
            string strCustomerName = string.Format("{0}", objCustomerName).Trim();
            string strRemark = string.Format("{0}", objRemark).Trim();
            /////
            {
                //////
                //if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST))
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                //        , "Check.strMST", strMST
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Inv_InventoryBalanceSerial_Map_InvalidMST
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );

                //}
                //////
            }
            #endregion

            #region //// Refine and Check Input Inv_InventoryBalanceSerial:
            ////
            DataTable dtInput_Inv_InventoryBalanceSerial = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalanceSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBalanceSerial = dsData.Tables[strTableCheck];
                //////
                //if (dtInput_Inv_InventoryBalanceSerial.Rows.Count < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.TableName", strTableCheck
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblInvalid
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                //////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBalanceSerial // dtData
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBalanceSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBalanceSerial.Rows[nScan];
                    ////
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalanceSerial:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalanceSerial" // strTableName
                    , new object[] {
                            "SerialNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBalanceSerial // dtData
                );
            }
            #endregion

            #region //// Refine and Check Input Inv_InventoryBalanceBox:
            ////
            DataTable dtInput_Inv_InventoryBalanceBox = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalanceBox";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBalanceBox = dsData.Tables[strTableCheck];
                //////
                //if (dtInput_Inv_InventoryBalanceBox.Rows.Count < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.TableName", strTableCheck
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblInvalid
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                //////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBalanceBox // dtData
                    , "StdParam", "BoxNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceBox, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceBox, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBalanceBox.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBalanceBox.Rows[nScan];
                    ////
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalanceBox:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalanceBox" // strTableName
                    , new object[] {
                            "BoxNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBalanceBox // dtData
                );
            }
            #endregion

            #region // Check All điều kiện về Box:
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceBox t --//[mylock]
                                left join Inv_InventoryBalanceSerial f --//[mylock]
                                    on t.BoxNo = f.BoxNo
                            where(1=1)
                                and f.BoxNo is null
                            ;
                        "
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.BoxNo.NoExistInInv", dtCheck.Rows[0]["BoxNo"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_BoxNoNotExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region //// Refine and Check Input Inv_InventoryBalanceCan:
            ////
            DataTable dtInput_Inv_InventoryBalanceCan = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalanceCan";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBalanceCan = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_Inv_InventoryBalanceBox.Rows.Count < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.TableName", strTableCheck
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblInvalid
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBalanceCan // dtData
                    , "StdParam", "CanNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceCan, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBalanceCan, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBalanceCan.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBalanceCan.Rows[nScan];
                    ////
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalanceCan:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalanceCan" // strTableName
                    , new object[] {
                            "CanNo", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBalanceCan // dtData
                );
            }
            #endregion

            #region // Check  All điều kiện về Can:
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                            ---- check:
                            select 
                                t.*
                            from #input_Inv_InventoryBalanceCan t --//[mylock]
                                left join Inv_InventoryBalanceSerial f --//[mylock]
                                    on t.CanNo = f.CanNo
                            where(1=1)
                                and f.CanNo is null
                            ;
                        "
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CanNo.NoExistInInv", dtCheck.Rows[0]["CanNo"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_CanNoNotExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Build InvF_InventoryInFG and Save InvF_InventoryOutFG:
            {
                #region // Build: InvF_InventoryInFG.
                string strIF_InvOutFGNo = "";
                DataSet dsInput = new DataSet();
                bool bInvOutNCC = false;
                /////
                {
                    #region // Build: IF_InvInFGNo.
                    strIF_InvOutFGNo = Seq_Common_MyGet(
                        ref alParamsCoupleError // alParamsCoupleError
                        , TConst.SeqType.IFInvOutFGNo // strSequenceType
                        , "" // strParam_Prefix
                        , "" // strParam_Postfix
                        );
                    ////
                    #endregion

                    #region // Build: InvF_InventoryInFG.
                    #endregion

                    #region // bInvOutNCC:
                    {
                        string strSqlCheckInvOut = CmUtils.StringUtils.Replace(@"
                                ---- #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial:
                                select 
                                    t.SerialNo
                                into #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial
                                from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                where(1=1)
                                union 
                                select 
                                    t.SerialNo
                                from Inv_InventoryBalanceSerial t --//[mylock]
                                    inner join #input_Inv_InventoryBalanceBox f --//[mylock]
                                        on t.BoxNo = f.BoxNo
                                where(1=1)
                                union 
                                select 
                                    t.SerialNo
                                from Inv_InventoryBalanceSerial t --//[mylock]
                                    inner join #input_Inv_InventoryBalanceCan f --//[mylock]
                                        on t.CanNo = f.CanNo
                                where(1=1)
                                ;   

                                --- Check:
                                select distinct
                                    f.SerialNo
                                from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
                                    inner join Inv_InventoryBalanceSerial f --//[mylock]
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                    and f.IF_InvOutFGNo is null
                                    and t.SerialNo is not null
                                ;

                                --- Check:
                                select distinct
                                    f.IF_InvOutFGNo
                                from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
                                    inner join Inv_InventoryBalanceSerial f --//[mylock]
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                    and f.IF_InvOutFGNo is not null
                                    and t.SerialNo is not null
                                ;

                                ---- Clear For Debug:
                                drop table #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial;
				            "
                            );

                        DataSet dscheck = _cf.db.ExecQuery(strSqlCheckInvOut);
                        DataTable dtDB_NoInvOut = dscheck.Tables[0];
                        DataTable dtDB_InvOut = dscheck.Tables[1];
                        ////
                        if (dtDB_InvOut.Rows.Count > 0 && dtDB_NoInvOut.Rows.Count > 0)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.RowNumber.InvOut", dtDB_InvOut.Rows.Count
                                , "Check.RowNumber.dtDB_NoInvOut", dtDB_NoInvOut.Rows.Count
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvlidInvFOut
                                , null
                                , alParamsCoupleError.ToArray()
                                );

                        }
                        if (dtDB_NoInvOut.Rows.Count > 0) bInvOutNCC = true;
                    }
                    #endregion

                    #region // Build Full Inv_InventoryBalanceSerial:
                    DataTable dtDB_Inv_InventoryBalanceSerial_FromBox_Can_Serial = null;
                    {
                        if (bInvOutNCC)
                        {
                            string strSqlCheckInv_InventoryBalanceSerial = CmUtils.StringUtils.Replace(@"
                                    ---- #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial:
                                    select 
                                        t.SerialNo
                                    into #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                    where(1=1)
                                    union 
                                    select 
                                        t.SerialNo
                                    from Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join #input_Inv_InventoryBalanceBox f --//[mylock]
                                            on t.BoxNo = f.BoxNo
                                    where(1=1)
                                        --and (t.AgentCode = '@strAgentCode' or '@strAgentCode' = '')
                                    union 
                                    select 
                                        t.SerialNo
                                    from Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join #input_Inv_InventoryBalanceCan f --//[mylock]
                                            on t.CanNo = f.CanNo
                                    where(1=1)
                                        --and (t.AgentCode = '@strAgentCode' or '@strAgentCode' = '')
                                    ;   

                                    select null tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial, t.* from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
					            
				                "
                                , "@strAgentCode", strMST
                                );

                            DataSet ds_BuildSerialFormBox_Can_Serial = _cf.db.ExecQuery(strSqlCheckInv_InventoryBalanceSerial);
                            dtDB_Inv_InventoryBalanceSerial_FromBox_Can_Serial = ds_BuildSerialFormBox_Can_Serial.Tables[0];

                        }
                        else

                        {
                            string strSqlCheckInv_InventoryBalanceSerial = CmUtils.StringUtils.Replace(@"
                                    ---- #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial:
                                    select 
                                        t.SerialNo
                                    into #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join Inv_InventoryBalanceSerial f --//[mylock]
                                            on t.SerialNo = f.SerialNo
                                    where(1=1)
                                        and (f.AgentCode = '@strAgentCode')
                                    union 
                                    select 
                                        t.SerialNo
                                    from Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join #input_Inv_InventoryBalanceBox f --//[mylock]
                                            on t.BoxNo = f.BoxNo
                                    where(1=1)
                                        and (t.AgentCode = '@strAgentCode')
                                    union 
                                    select 
                                        t.SerialNo
                                    from Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join #input_Inv_InventoryBalanceCan f --//[mylock]
                                            on t.CanNo = f.CanNo
                                    where(1=1)
                                        and (t.AgentCode = '@strAgentCode')
                                    ;   

                                    select null tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial, t.* from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
					            
				                "
                                , "@strAgentCode", strMST
                                );

                            DataSet ds_BuildSerialFormBox_Can_Serial = _cf.db.ExecQuery(strSqlCheckInv_InventoryBalanceSerial);
                            dtDB_Inv_InventoryBalanceSerial_FromBox_Can_Serial = ds_BuildSerialFormBox_Can_Serial.Tables[0];

                        }
                    }
                    #endregion

                    #region // Check All Conditional:
                    {
                        #region // Không có cả Nếu ListSerial, ListBox, ListCan đều rỗng:
                        {
                            if (dtInput_Inv_InventoryBalanceBox.Rows.Count < 1
                                && dtInput_Inv_InventoryBalanceCan.Rows.Count < 1
                                && dtInput_Inv_InventoryBalanceSerial.Rows.Count < 1
                                && dtDB_Inv_InventoryBalanceSerial_FromBox_Can_Serial.Rows.Count < 1)
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.RowNumber.Inv_InventoryBalanceSerial", dtInput_Inv_InventoryBalanceSerial.Rows.Count
                                    , "Check.RowNumber.dtInput_Inv_InventoryBalanceBoxs", dtInput_Inv_InventoryBalanceBox.Rows.Count
                                    , "Check.RowNumber.dtInput_Inv_InventoryBalanceCan", dtInput_Inv_InventoryBalanceCan.Rows.Count
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvlidListSerialAndCanAndBox
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        #endregion

                        #region // Nếu xuất serial từ Agent thì các serial phải thuộc cùng Agent:
                        {
                            string strSqlCheckAgent = CmUtils.StringUtils.Replace(@"
                                    --- Check:
                                    select 
                                        t.SerialNo
                                        , f.AgentCode
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join Inv_InventoryBalanceSerial f --//[mylock]
                                            on t.SerialNo = f.SerialNo
                                    where(1=1)
                                        and f.AgentCode != '@strAgentCode'
                                        and f.AgentCode is not null
                                    ;
				                 "
                                , "@strAgentCode", strMST
                                );

                            DataTable dtDB_Check_Agent = _cf.db.ExecQuery(strSqlCheckAgent).Tables[0];
                            ////
                            if (dtDB_Check_Agent.Rows.Count > 01)
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.SerialNo", dtDB_Check_Agent.Rows[0]["SerialNo"]
                                    , "Check.DB.AgentCode", dtDB_Check_Agent.Rows[0]["AgentCode"]
                                    , "Check.Expected.MST", strMST
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvlidSerialNoBelongToNNTOrther
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        #endregion

                        #region // Nếu xuất serial phải chưa bán:
                        {
                            string strSqlCheckAgent = CmUtils.StringUtils.Replace(@"
                                    --- Check:
                                    select 
                                        t.SerialNo
                                        , f.FlagSales
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join Inv_InventoryBalanceSerial f --//[mylock]
                                            on t.SerialNo = f.SerialNo
                                    where(1=1)
                                        and f.FlagSales = '1'
                                    ;
				                 "
                                );

                            DataTable dtDB_Check_Agent = _cf.db.ExecQuery(strSqlCheckAgent).Tables[0];
                            ////
                            if (dtDB_Check_Agent.Rows.Count > 0 && bInvOutNCC == true)
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.SerialNo", dtDB_Check_Agent.Rows[0]["SerialNo"]
                                    , "Check.DB.FlagSales", dtDB_Check_Agent.Rows[0]["FlagSales"]
                                    , "Check.Expected.FlagSales", TConst.Flag.No
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvlidFlagSales
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        #endregion

                        #region // nếu đã bán tới endCustomer rồi không được xuất nữa:
                        {
                            string strSqlCheckInvOut = CmUtils.StringUtils.Replace(@"
                                    --- Check:
                                    select distinct
                                        f.SerialNo
                                        , f.AgentCode
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join Inv_InventoryBalanceSerial f --//[mylock]
                                            on t.SerialNo = f.SerialNo
                                    where(1=1)
                                        and f.AgentCode is null
										and f.FlagSales = '1'
                                    ;
				                 "
                                );

                            DataSet dscheck = _cf.db.ExecQuery(strSqlCheckInvOut);
                            DataTable dtDB_InvCode = dscheck.Tables[0];
                            if (dtDB_InvCode.Rows.Count > 0)
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.SerialNo", dtDB_InvCode.Rows[0]["SerialNo"]
                                    , "Check.Expected.AgentCode", "AgentCode is not null"
                                    , "Check.DB.AgentCode", dtDB_InvCode.Rows[0]["AgentCode"]
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvalidAgentCode_Current
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        #endregion

                        #region // nếu xuất từ NCC thì các serial phải cùng 1 mã kho:
                        {
                            string strSqlCheckInvOut = CmUtils.StringUtils.Replace(@"
                                    --- Check:
                                    select distinct
                                        f.InvCode
                                    from #input_Inv_InventoryBalanceSerial t --//[mylock]
                                        inner join Inv_InventoryBalanceSerial f --//[mylock]
                                            on t.SerialNo = f.SerialNo
                                    where(1=1)
                                    ;
				                 "
                                );

                            DataSet dscheck = _cf.db.ExecQuery(strSqlCheckInvOut);
                            DataTable dtDB_InvCode = dscheck.Tables[0];
                            if (dtDB_InvCode.Rows.Count > 1 && bInvOutNCC)
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.InvCode.01", dtDB_InvCode.Rows[0]["InvCode"]
                                    , "Check.InvCode.02", dtDB_InvCode.Rows[1]["InvCode"]
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvalidInvCode
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                            /////
                            string strInvCodeDB = "";
                            if (dtDB_InvCode.Rows.Count > 0) strInvCodeDB = TUtils.CUtils.StdParam(dtDB_InvCode.Rows[0]["InvCode"]);
                            ////
                            if (bInvOutNCC)
                            {
                                if (!CmUtils.StringUtils.StringEqualIgnoreCase(strInvCode, strInvCodeDB))
                                {
                                    alParamsCoupleError.AddRange(new object[]{
                                        "Check.DB.InvCodeDB", strInvCodeDB
                                        , "Check.Input.InvCode", strInvCode
                                        });
                                    throw CmUtils.CMyException.Raise(
                                        TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInvX_InvalidInvCode
                                        , null
                                        , alParamsCoupleError.ToArray()
                                        );
                                }

                            }
                            /////
                            if(dtDB_InvCode.Rows.Count > 0) strInvCode = TUtils.CUtils.StdParam(dtDB_InvCode.Rows[0]["InvCode"]);
                        }
                        #endregion
                    }
                    #endregion

                    #region // Build: InvF_InventoryInFGDtl.
                    DataTable dtInput_InvF_InventoryOutFGDtl = new DataTable("InvF_InventoryOutFGDtl");
                    {
                        string strSqlCheckInvF_InventoryOutFGDtl = CmUtils.StringUtils.Replace(@"
                                ---- #tbl_InvF_InventoryOutFGDtl_Build:
                                select 
                                    f.PartCode
                                    , Count(t.SerialNo) Qty
                                into #tbl_InvF_InventoryOutFGDtl_Build
                                from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
                                    inner join Inv_InventoryBalanceSerial  f --//[mylock]  
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                group by
                                    f.PartCode
                                ;

                                --- Return:
                                select 
                                    t.PartCode
                                    , t.Qty
                                from #tbl_InvF_InventoryOutFGDtl_Build t --//[mylock]
                                where(1=1)
                                ;

                                ---- Clear For Debug:
                                drop table #tbl_InvF_InventoryOutFGDtl_Build;
					            
				            "
                            , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd")
                            );

                        dtInput_InvF_InventoryOutFGDtl = _cf.db.ExecQuery(strSqlCheckInvF_InventoryOutFGDtl).Tables[0].Copy();
                        dtInput_InvF_InventoryOutFGDtl.TableName = "InvF_InventoryOutFGDtl";
                        DataTable dtInput_InvF_InventoryOutHistDtl = dtInput_InvF_InventoryOutFGDtl.Copy();
                        dtInput_InvF_InventoryOutHistDtl.TableName = "InvF_InventoryOutHistDtl";
                        dsInput.Tables.Add(dtInput_InvF_InventoryOutFGDtl);
                        dsInput.Tables.Add(dtInput_InvF_InventoryOutHistDtl);
                    }
                    #endregion

                    #region // Build: InvF_InventoryOutFGInstSerial.
                    DataTable dtInput_InvF_InventoryOutFGInstSerial = new DataTable("InvF_InventoryOutFGInstSerial");
                    {
                        string strSqlCheckInvF_InventoryInFGInstSerial = CmUtils.StringUtils.Replace(@"
                                --- Return:
                                select 
                                    f.PartCode
                                    , t.SerialNo
                                from #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial t --//[mylock]
                                    inner join Inv_InventoryBalanceSerial  f --//[mylock]  
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                ;					            
				            "
                            );

                        dtInput_InvF_InventoryOutFGInstSerial = _cf.db.ExecQuery(strSqlCheckInvF_InventoryInFGInstSerial).Tables[0].Copy();
                        dtInput_InvF_InventoryOutFGInstSerial.TableName = "InvF_InventoryOutFGInstSerial";
                        //else
                        DataTable dtInput_InvF_InventoryOutHistInstSerial = dtInput_InvF_InventoryOutFGInstSerial.Copy();
                        dtInput_InvF_InventoryOutHistInstSerial.TableName = "InvF_InventoryOutHistInstSerial";
                        dsInput.Tables.Add(dtInput_InvF_InventoryOutFGInstSerial);
                        dsInput.Tables.Add(dtInput_InvF_InventoryOutHistInstSerial);

                    }
                    #endregion

                }
                #endregion

                #region // InvF_InventoryOutFG_SaveX:
                //DataSet dsGetData = null;
                if(bInvOutNCC)
                {
                    InvF_InventoryOutFG_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , "0" // objFlagIsDelete
                              ////
                        , strIF_InvOutFGNo // strIF_InvOutFGNo
                        , objMST // objMST
                        , TConst.FormOutType.MaVach // strFormOutType
                        , strInvOutType // strInvOutType
                        , strInvCode // strInvCode
                        , "" // objPMType
                        , strInvFOutType // strInvFOutType
                        , strPlateNo // strPlateNo
                        , strMoocNo // strMoocNo
                        , strDriverName // strDriverName
                        , strDriverPhoneNo // strDriverPhoneNo
                        , strAgentCode // strAgentCode
                        , strCustomerName // strCustomerName
                        , strRemark // objRemark
                                    ////
                        , dsInput  // dsData
                        );
                }
                //else
                {
                    InvF_InventoryOutHist_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , "0" // objFlagIsDelete
                              ////
                        , strIF_InvOutFGNo // strIF_InvOutFGNo
                        , objMST // objMST
                        , TConst.FormOutType.MaVach // strFormOutType
                        , strInvOutType // strInvOutType
                        , strInvCode // strInvCode
                        , "" // objPMType
                        , strInvFOutType // strInvFOutType
                        , strPlateNo // strPlateNo
                        , strMoocNo // strMoocNo
                        , strDriverName // strDriverName
                        , strDriverPhoneNo // strDriverPhoneNo
                        , strAgentCode // strAgentCode
                        , strCustomerName // strCustomerName
                        , strRemark // objRemark
                                    ////
                        , dsInput  // dsData
                        );
                }

                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // InvF_InventoryOutFG_ApproveX:
                //DataSet dsGetData = null;
                if(bInvOutNCC)
                {
                    InvF_InventoryOutFG_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strIF_InvOutFGNo // strIF_InvOutFGNo
                        , strRemark // objRemark
                        );
                }
                //else
                {
                    InvF_InventoryOutHist_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strIF_InvOutFGNo // strIF_InvOutFGNo
                        , strRemark // objRemark
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // Update Inv_InventoryBalanceSerial:
                if(!bInvOutNCC
                    && !string.IsNullOrEmpty(strAgentCode))
                {
                    string strSqlUpdate_Inv_InventoryBalanceSerial = CmUtils.StringUtils.Replace(@"
                            ----- update:
                            update t 
                            set
                                t.AgentCode = '@strAgent_New'
                            from Inv_InventoryBalanceSerial t --//[mylock]
                                inner join #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and t.AgentCode = '@strAgentCode'
                            ;   
					            
				            "
                        , "@strAgentCode", strMST
                        , "@strAgent_New", strAgentCode
                        );

                    DataSet ds_BuildSerialFormBox_Can_Serial = _cf.db.ExecQuery(strSqlUpdate_Inv_InventoryBalanceSerial);
                }
                else if (!bInvOutNCC
                    && string.IsNullOrEmpty(strAgentCode))
                {
                    string strSqlUpdate_Inv_InventoryBalanceSerial = CmUtils.StringUtils.Replace(@"
                            ----- update:
                            update t 
                            set
                                t.AgentCode = null
                            from Inv_InventoryBalanceSerial t --//[mylock]
                                inner join #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial f --//[mylock]
                                    on t.SerialNo = f.SerialNo
                            where(1=1)
                                and t.AgentCode = '@strAgentCode'
                            ;   
					            
				            "
                        , "@strAgentCode", strMST
                        , "@strAgent_New", strAgentCode
                        );

                    DataSet ds_BuildSerialFormBox_Can_Serial = _cf.db.ExecQuery(strSqlUpdate_Inv_InventoryBalanceSerial);
                }
                #endregion

            }
            #endregion 

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventoryBalanceSerial;
						drop table #input_Inv_InventoryBalanceBox;
						drop table #input_Inv_InventoryBalanceCan;
						drop table #tbl_Inv_InventoryBalanceSerial_FromBox_Can_Serial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            return;

        }

        public DataSet Inv_InventoryBalanceSerial_OutInv(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_InventoryBalanceSerial_OutInv";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_OutInv;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objMST", objMST
                    , "objFormOutType", objFormOutType
                    , "objInvOutType", objInvOutType
                    , "objInvCode", objInvCode
                    , "objPMType", objPMType
                    , "objInvFOutType", objInvFOutType
                    , "objPlateNo", objPlateNo
                    , "objMoocNo", objMoocNo
                    , "objDriverName", objDriverName
                    , "objDriverPhoneNo", objDriverPhoneNo
                    , "objAgentCode", objAgentCode
                    , "objCustomerName", objCustomerName
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

                #region // Convert Input:
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Inv_InventoryBalanceSerial_OutInvX:
                //DataSet dsGetData = null;
                {
                    Inv_InventoryBalanceSerial_OutInvX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objMST // objMST
                        , objFormOutType // objFormOutType
                        , objInvOutType // objInvOutType
                        , objInvCode // objInvCode
                        , objPMType // objPMType
                        , objInvFOutType // objInvFOutType
                        , objPlateNo // objPlateNo
                        , objMoocNo // objMoocNo
                        , objDriverName // objDriverName
                        , objDriverPhoneNo // objDriverPhoneNo
                        , objAgentCode // objAgentCode
                        , objCustomerName // objCustomerName
                                          ////
                        , objRemark // objRemark
                                    ////
                        , dsData // dsData
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


        public DataSet WAS_Inv_InventoryBalanceSerial_OutInv(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBalanceSerial_OutInv objRQ_Inv_InventoryBalanceSerial_OutInv
            ////
            , out RT_Inv_InventoryBalanceSerial_OutInv objRT_Inv_InventoryBalanceSerial_OutInv
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBalanceSerial_OutInv.Tid;
            objRT_Inv_InventoryBalanceSerial_OutInv = new RT_Inv_InventoryBalanceSerial_OutInv();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBalanceSerial_OutInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_OutInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Lst_Inv_InventoryBalanceSerial", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBalanceSerial)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Invoice_TempInvoice> lst_Invoice_TempInvoice = new List<Invoice_TempInvoice>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if(objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBalanceSerial == null)
                    objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
                    DataTable dt_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalanceSerial>(objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBalanceSerial, "Inv_InventoryBalanceSerial");
                    dsData.Tables.Add(dt_Inv_InventoryBalanceSerial);

                    if (objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBox == null)
                        objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBox = new List<Inv_InventoryBox>();
                    DataTable dt_Inv_InventoryBox = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBox>(objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryBox, "Inv_InventoryBalanceBox");
                    dsData.Tables.Add(dt_Inv_InventoryBox);

                    if (objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryCarton == null)
                        objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryCarton = new List<Inv_InventoryCarton>();
                    DataTable dt_Inv_InventoryCarton = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryCarton>(objRQ_Inv_InventoryBalanceSerial_OutInv.Lst_Inv_InventoryCarton, "Inv_InventoryBalanceCan");
                    dsData.Tables.Add(dt_Inv_InventoryCarton);

                }
                #endregion

                #region // Inv_InventoryBalanceSerial_OutInv:
                mdsResult = Inv_InventoryBalanceSerial_OutInv(
                    objRQ_Inv_InventoryBalanceSerial_OutInv.Tid // strTid
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.MST
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.FormOutType
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.InvOutType
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.InvCode
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.PMType
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.InvFOutType
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.PlateNo
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.MoocNo
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.DriverName
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.DriverPhoneNo
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.AgentCode
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.CustomerName
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.Remark
                    , dsData
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

        #region // InvF_InventoryOutHist:
        public void InvF_InventoryOutHist_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objIF_InvOutHistNo
            , string strFlagExistToCheck
            , string strStatusListToCheck
            , out DataTable dtDB_InvF_InventoryOutHist
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from InvF_InventoryOutHist t --//[mylock]
					where (1=1)
						and t.IF_InvOutHistNo = @objIF_InvOutHistNo
					;
				");
            dtDB_InvF_InventoryOutHist = _cf.db.ExecQuery(
                strSqlExec
                , "@objIF_InvOutHistNo", objIF_InvOutHistNo
                ).Tables[0];
            dtDB_InvF_InventoryOutHist.TableName = "InvF_InventoryOutHist";

            // strFlagExistToCheck
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_InvF_InventoryOutHist.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvOutHistNo", objIF_InvOutHistNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_CheckDB_InvOutHistNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_InvF_InventoryOutHist.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_InvOutHistNo", objIF_InvOutHistNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_CheckDB_InvOutHistNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_InvF_InventoryOutHist.Rows[0]["IF_InvOutHistStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.IF_InvOutHistNo", objIF_InvOutHistNo
                    , "Check.strFlagActiveListToCheck", strStatusListToCheck
                    , "DB.IF_InvOutHistStatus", dtDB_InvF_InventoryOutHist.Rows[0]["IF_InvOutHistStatus"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.InvF_InventoryOutHist_CheckDB_StatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public void InvF_InventoryOutHist_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_InventoryOutHist
            , string strRt_Cols_InvF_InventoryOutHistDtl
            , string strRt_Cols_InvF_InventoryOutHistInstSerial
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_InventoryOutHist_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_InventoryOutHist", strRt_Cols_InvF_InventoryOutHist
                , "strRt_Cols_InvF_InventoryOutHistDtl", strRt_Cols_InvF_InventoryOutHistDtl
                , "strRt_Cols_InvF_InventoryOutHistInstSerial", strRt_Cols_InvF_InventoryOutHistInstSerial
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_InventoryOutHist = (strRt_Cols_InvF_InventoryOutHist != null && strRt_Cols_InvF_InventoryOutHist.Length > 0);
            bool bGet_InvF_InventoryOutHistDtl = (strRt_Cols_InvF_InventoryOutHistDtl != null && strRt_Cols_InvF_InventoryOutHistDtl.Length > 0);
            bool bGet_InvF_InventoryOutHistInstSerial = (strRt_Cols_InvF_InventoryOutHistInstSerial != null && strRt_Cols_InvF_InventoryOutHistInstSerial.Length > 0);
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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_InvF_InventoryOutHist_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiif.IF_InvOutHistNo
							, iiif.CreateDTimeUTC
						into #tbl_InvF_InventoryOutHist_Filter_Draft
						from InvF_InventoryOutHist iiif --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiif.MST = t_MstNNT_View.MST
							left join InvF_InventoryOutHistDtl iiifdt --//[mylock]
								on iiif.IF_InvOutHistNo = iiifdt.IF_InvOutHistNo
							left join InvF_InventoryOutHistInstSerial iiifi --//[mylock]
								on iiif.IF_InvOutHistNo = iiifi.IF_InvOutHistNo
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiif.CreateDTimeUTC desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_InventoryOutHist_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_InventoryOutHist_Filter:
						select
							t.*
						into #tbl_InvF_InventoryOutHist_Filter
						from #tbl_InvF_InventoryOutHist_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_InventoryOutHist ------:
						zzB_Select_InvF_InventoryOutHist_zzE
						-------------------------------------

						-------- InvF_InventoryOutHistDtl ------:
						zzB_Select_InvF_InventoryOutHistDtl_zzE
						-----------------------------------------

						-------- InvF_InventoryOutHistInstSerial ------:
						zzB_Select_InvF_InventoryOutHistInstSerial_zzE
						-----------------------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_InventoryOutHist_Filter_Draft;
						--drop table #tbl_InvF_InventoryOutHist_Filter;
					"
                );
            ////
            string zzB_Select_InvF_InventoryOutHist_zzE = "-- Nothing.";
            if (bGet_InvF_InventoryOutHist)
            {
                #region // bGet_InvF_InventoryOutHist:
                zzB_Select_InvF_InventoryOutHist_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutHist:
							select
								t.MyIdxSeq
								, iiif.*
							from #tbl_InvF_InventoryOutHist_Filter t --//[mylock]
								inner join InvF_InventoryOutHist iiif --//[mylock]
									on t.IF_InvOutHistNo = iiif.IF_InvOutHistNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryOutHistDtl_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryOutHistDtl)
            {
                #region // bGet_InvF_InventoryOutHistDtl:
                zzB_Select_InvF_InventoryOutHistDtl_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutHistDtl:
							select
								t.MyIdxSeq
								, iiifdt.*
							from #tbl_InvF_InventoryOutHist_Filter t --//[mylock]
								inner join InvF_InventoryOutHist iiif --//[mylock]
									on t.IF_InvOutHistNo = iiif.IF_InvOutHistNo
								inner join InvF_InventoryOutHistDtl iiifdt --//[mylock]
									on t.IF_InvOutHistNo = iiifdt.IF_InvOutHistNo
							order by t.MyIdxSeq asc
							;
						"
                    );
                #endregion
            }
            ////
            string zzB_Select_InvF_InventoryOutHistInstSerial_zzE = "-- Nothing.";

            if (bGet_InvF_InventoryOutHistInstSerial)
            {
                #region // bGet_InvF_InventoryOutHistInstSerial:
                zzB_Select_InvF_InventoryOutHistInstSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_InventoryOutHistDtl:
							select
								t.MyIdxSeq
								, iiifs.*
							from #tbl_InvF_InventoryOutHist_Filter t --//[mylock]
								inner join InvF_InventoryOutHist iiif --//[mylock]
									on t.IF_InvOutHistNo = iiif.IF_InvOutHistNo
								inner join InvF_InventoryOutHistInstSerial iiifs --//[mylock]
									on t.IF_InvOutHistNo = iiifs.IF_InvOutHistNo
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
                        , "InvF_InventoryOutHist" // strTableNameDB
                        , "InvF_InventoryOutHist." // strPrefixStd
                        , "iiif." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "InvF_InventoryOutHistDtl" // strTableNameDB
                        , "InvF_InventoryOutHistDtl." // strPrefixStd
                        , "iiifdt." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "InvF_InventoryOutHistInstSerial" // strTableNameDB
                        , "InvF_InventoryOutHistInstSerial." // strPrefixStd
                        , "iiifi." // strPrefixAlias
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
                , "zzB_Select_InvF_InventoryOutHist_zzE", zzB_Select_InvF_InventoryOutHist_zzE
                , "zzB_Select_InvF_InventoryOutHistDtl_zzE", zzB_Select_InvF_InventoryOutHistDtl_zzE
                , "zzB_Select_InvF_InventoryOutHistInstSerial_zzE", zzB_Select_InvF_InventoryOutHistInstSerial_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_InventoryOutHist)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutHist";
            }
            if (bGet_InvF_InventoryOutHistDtl)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutHistDtl";
            }
            if (bGet_InvF_InventoryOutHistInstSerial)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_InventoryOutHistInstSerial";
            }
            #endregion
        }

        public DataSet InvF_InventoryOutHist_Get(
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
            , string strRt_Cols_InvF_InventoryOutHist
            , string strRt_Cols_InvF_InventoryOutHistDtl
            , string strRt_Cols_InvF_InventoryOutHistInstSerial
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryOutHist_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutHist_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_InvF_InventoryOutHist", strRt_Cols_InvF_InventoryOutHist
                , "strRt_Cols_InvF_InventoryOutHistDtl", strRt_Cols_InvF_InventoryOutHistDtl
                , "strRt_Cols_InvF_InventoryOutHistInstSerial", strRt_Cols_InvF_InventoryOutHistInstSerial
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

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_InventoryOutHist_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryOutHist_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_InvF_InventoryOutHist // strRt_Cols_InvF_InventoryOutHist
                        , strRt_Cols_InvF_InventoryOutHistDtl // strRt_Cols_InvF_InventoryOutHistDtl
                        , strRt_Cols_InvF_InventoryOutHistInstSerial // strRt_Cols_InvF_InventoryOutHistInstSerial
                                                                  /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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

        public DataSet WAS_InvF_InventoryOutHist_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist
            ////
            , out RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutHist.Tid;
            objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutHist.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutHist_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutHist_Get;
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
                List<InvF_InventoryOutHist> lst_InvF_InventoryOutHist = new List<InvF_InventoryOutHist>();
                List<InvF_InventoryOutHistDtl> lst_InvF_InventoryOutHistDtl = new List<InvF_InventoryOutHistDtl>();
                List<InvF_InventoryOutHistInstSerial> lst_InvF_InventoryOutHistInstSerial = new List<InvF_InventoryOutHistInstSerial>();
                /////
                bool bGet_InvF_InventoryOutHist = (objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHist != null && objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHist.Length > 0);
                bool bGet_InvF_InventoryOutHistDtl = (objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistDtl != null && objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistDtl.Length > 0);
                bool bGet_InvF_InventoryOutHistInstSerial = (objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistInstSerial != null && objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistInstSerial.Length > 0);
                #endregion

                #region // WS_InvF_InventoryOutHist_Get:
                mdsResult = InvF_InventoryOutHist_Get(
                    objRQ_InvF_InventoryOutHist.Tid // strTid
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutHist.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutHist.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutHist.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_InventoryOutHist.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_InventoryOutHist.Ft_WhereClause // strFt_WhereClause
                                                              //// Return:
                    , objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHist // strRt_Cols_InvF_InventoryOutHist
                    , objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistDtl // Rt_Cols_InvF_InventoryOutHistDtl
                    , objRQ_InvF_InventoryOutHist.Rt_Cols_InvF_InventoryOutHistInstSerial // Rt_Cols_InvF_InventoryOutHistInstSerial
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_InventoryOutHist.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    if (bGet_InvF_InventoryOutHist)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutHist = mdsResult.Tables["InvF_InventoryOutHist"].Copy();
                        lst_InvF_InventoryOutHist = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutHist>(dt_InvF_InventoryOutHist);
                        objRT_InvF_InventoryOutHist.Lst_InvF_InventoryOutHist = lst_InvF_InventoryOutHist;
                    }
                    ////
                    if (bGet_InvF_InventoryOutHistDtl)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutHistDtl = mdsResult.Tables["InvF_InventoryOutHistDtl"].Copy();
                        lst_InvF_InventoryOutHistDtl = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutHistDtl>(dt_InvF_InventoryOutHistDtl);
                        objRT_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistDtl = lst_InvF_InventoryOutHistDtl;
                    }
                    ////
                    if (bGet_InvF_InventoryOutHistInstSerial)
                    {
                        ////
                        DataTable dt_InvF_InventoryOutHistInstSerial = mdsResult.Tables["InvF_InventoryOutHistInstSerial"].Copy();
                        lst_InvF_InventoryOutHistInstSerial = TUtils.DataTableCmUtils.ToListof<InvF_InventoryOutHistInstSerial>(dt_InvF_InventoryOutHistInstSerial);
                        objRT_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistInstSerial = lst_InvF_InventoryOutHistInstSerial;
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

        private void InvF_InventoryOutHist_SaveX(
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
            , object objIF_InvOutHistNo
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutHist_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutHistNo", objIF_InvOutHistNo
                , "objMST", objMST
                , "objFormOutType", objFormOutType
                , "objInvOutType", objInvOutType
                , "objInvCode", objInvCode
                , "objPMType", objPMType
                , "objInvFOutType", objInvFOutType
                , "objPlateNo", objPlateNo
                , "objMoocNo", objMoocNo
                , "objDriverName", objDriverName
                , "objDriverPhoneNo", objDriverPhoneNo
                , "objAgentCode", objAgentCode
                , "objCustomerName", objCustomerName
                ////
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            #endregion

            #region // Refine and Check Input InvF_InventoryOutHist:
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strIF_InvOutHistNo = TUtils.CUtils.StdParam(objIF_InvOutHistNo);
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strFormOutType = TUtils.CUtils.StdParam(objFormOutType);
            string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
            string strInvCode = TUtils.CUtils.StdParam(objInvCode);
            string strPMType = TUtils.CUtils.StdParam(objPMType);
            string strInvFOutType = TUtils.CUtils.StdParam(objInvFOutType);
            string strPlateNo = string.Format("{0}", objPlateNo).Trim();
            string strMoocNo = string.Format("{0}", objMoocNo).Trim();
            string strDriverName = string.Format("{0}", objDriverName).Trim();
            string strDriverPhoneNo = string.Format("{0}", objDriverPhoneNo).Trim();
            string strAgentCode = TUtils.CUtils.StdParam(objAgentCode);
            string strCustomerName = string.Format("{0}", objCustomerName).Trim();
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutHist = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            {
                ////
                if (string.IsNullOrEmpty(strIF_InvOutHistNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvOutHistNo", strIF_InvOutHistNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_Save_InvalidIF_InvOutHistNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryOutHist_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutHistNo // objInvoiceCode
                    , "" // strFlagExistToCheck
                    , "" // strInvoiceStatusListToCheck
                    , out dtDB_InvF_InventoryOutHist // dtDB_Invoice_Invoice
                    );
                ////
                if (dtDB_InvF_InventoryOutHist.Rows.Count < 1) // Chưa Tồn tại.
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công.
                    }
                    else
                    {
                        // 
                        strCreateDTimeUTC = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strCreateBy = strWAUserCode;
                    }
                }
                else // Đã Tồn tại.
                {
                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOutHist.Rows[0]["IF_InvOutHistStatus"], TConst.IF_InvOutHistStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvOutHistStatus", dtDB_InvF_InventoryOutHist.Rows[0]["IF_InvOutHistStatus"]
                            , "Check.IF_InvOutHistStatus.Expected", TConst.IF_InvOutHistStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutHist_Save_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryOutHist.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOutHist.Rows[0]["CreateBy"]);
                    ////
                }

                DataTable dtDB_Mst_NNT = new DataTable();
                if (string.IsNullOrEmpty(strFormOutType)) strFormOutType = TConst.FormOutType.KhongMaVach;
                ////
                if(!CmUtils.StringUtils.StringEqualIgnoreCase(strInvFOutType, TConst.InvFOutType.OutThuongMai)
                    && !CmUtils.StringUtils.StringEqualIgnoreCase(strInvFOutType, TConst.InvFOutType.OutEndCus))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strInvFOutType", strInvFOutType
                        , "Check.InvFOutType.Expected", "OutThuongMai Or OutEndCus"
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_Save_InvalidInvFOutType
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutHist:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutHist"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutHistNo"
                        , "NetworkID"
                        , "MST"
                        , "FormOutType"
                        , "InvOutType"
                        , "InvCode"
                        , "PMType"
                        , "InvFOutType"
                        , "PlateNo"
                        , "MoocNo"
                        , "DriverName"
                        , "DriverPhoneNo"
                        , "AgentCode"
                        , "CustomerName"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutHistStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutHistNo, // IF_InvOutHistNo
                                nNetworkID, // NetworkID
								strMST, // MST
								strFormOutType, // FormOutType
								strInvOutType, // InvOutType
								strInvCode, // InvCode
								strPMType, // PMType
								strInvFOutType, // InvFOutType
								strPlateNo, // PlateNo
                                strMoocNo, // MoocNo
                                strDriverName, // DriverName
                                strDriverPhoneNo, // DriverPhoneNo
                                strAgentCode, // AgentCode
                                strCustomerName, // CustomerName
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                null, // ApprDTimeUTC
                                null, // ApprBy
                                TConst.IF_InvOutHistStatus.Pending, // IF_InvOutHistStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutHistDtl:
            ////
            DataTable dtInput_InvF_InventoryOutHistDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutHistDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_Save_InvFInventoryOutHistDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutHistDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryOutHistDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_Save_InvFInventoryOutDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutHistDtl // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistDtl, "IF_InvOutHistNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistDtl, "IF_InvOutHistStatusDtl", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutHistDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutHistDtl.Rows[nScan];
                    ////
                    drScan["IF_InvOutHistNo"] = strIF_InvOutHistNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutHistStatusDtl"] = TConst.IF_InvOutHistStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutHistDtl:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutHistDtl" // strTableName
                    , new object[] {
                            "IF_InvOutHistNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "IF_InvOutHistStatusDtl", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutHistDtl // dtData
                );
            }
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutHistInstSerial:
            ////
            DataTable dtInput_InvF_InventoryOutHistInstSerial = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutHistInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOutHist_Save_InvF_InventoryOutHistInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutHistInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutHistInstSerial // dtData
                    , "StdParam", "PartCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "IF_InvOutHistNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "IF_InvOutHistPrevNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "FlagInitBase", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "FlagCurrent", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "IF_InvOutHistISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutHistInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutHistInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutHistInstSerial.Rows[nScan];
                    ////
                    //string strVATRateCode = TUtils.CUtils.StdParam(drScan["VATRateCode"]);
                    ////
                    drScan["IF_InvOutHistNo"] = strIF_InvOutHistNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutHistPrevNo"] = DBNull.Value;
                    drScan["IF_InvOutHistISStatus"] = TConst.IF_InvOutHistStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp InvF_InventoryOutHistInstSerial:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutHistInstSerial" // strTableName
                    , new object[] {
                            "IF_InvOutHistNo", TConst.BizMix.Default_DBColType
                            , "PartCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "IF_InvOutHistISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutHistInstSerial // dtData
                );
            }
            #endregion

            #region // Check and Build InvF_InventoryOutHistInstSerial.
            {
                #region // Check InvF_InventoryOutHistInstSerial: 
                #region // Serial phải tồn tại trên hệ thống:
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                                ----
                                select distinct
	                                f.SerialNo
	                                , f.FlagSales
                                from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	                                left join Inv_InventoryBalanceSerial f --//[mylock]
		                                on t.SerialNo = f.SerialNo
                                where(1=1)
                                    and f.SerialNo is null
                                ;
                            ");
                    DataTable dt_Check = _cf.db.ExecQuery(
                        strSql_Check
                        ).Tables[0];
                    ////
                    if (dt_Check.Rows.Count > 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.SerialNo", dt_Check.Rows[0]["SerialNo"]
                            , "Check.DB.FlagSales", dt_Check.Rows[0]["FlagSales"]
                            , "Check.FlagSales.Expected", TConst.Flag.No
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutHist_Save_InvF_SerialNoExist
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                }
                #endregion 

                #region // Check Serial cùng lần xuất thành phẩm hoặc xuất từ nhà phân phối:
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                                    ----
                                    select distinct
	                                    f.IF_InvOutFGNo
                                    from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	                                    left join Inv_InventoryBalanceSerial f --//[mylock]
		                                    on t.SerialNo = f.SerialNo
                                    where(1=1)
                                    ;
                                ");
                    DataTable dt_Check = _cf.db.ExecQuery(
                        strSql_Check
                        ).Tables[0];
                    ////
                   if(dt_Check.Rows.Count > 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.IF_InvOutFGNo.01", dt_Check.Rows[0]["IF_InvOutFGNo"]
                            , "Check.IF_InvOutFGNo.02", dt_Check.Rows[1]["IF_InvOutFGNo"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutHist_Save_InvF_InvalidSerialNotEqualSource
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                   ////
                }
                #endregion 

                #region // Serial Chưa bán cho EndCustomer:
                //if(!bIsDelete)
                //{
                //    string strSql_Check = CmUtils.StringUtils.Replace(@"
                //                ----
                //                select distinct
	               //                 f.SerialNo
	               //                 , f.FlagSales
                //                from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	               //                 left join Inv_InventoryBalanceSerial f --//[mylock]
		              //                  on t.SerialNo = f.SerialNo
                //                where(1=1)
                //                    and f.FlagSales = '1'
                //                ;
                //            ");
                //    DataTable dt_Check = _cf.db.ExecQuery(
                //        strSql_Check
                //        ).Tables[0];
                //    ////
                //    if (dt_Check.Rows.Count > 1)
                //    {
                //        alParamsCoupleError.AddRange(new object[]{
                //            "Check.SerialNo", dt_Check.Rows[0]["SerialNo"]
                //            , "Check.DB.FlagSales", dt_Check.Rows[0]["FlagSales"]
                //            , "Check.FlagSales.Expected", TConst.Flag.No
                //            });
                //        throw CmUtils.CMyException.Raise(
                //            TError.ErridnInventory.InvF_InventoryOutHist_Save_SerialFlagSalesNotMatch
                //            , null
                //            , alParamsCoupleError.ToArray()
                //            );

                //    }
                //    ////
                //}
                #endregion

                #region // Nếu là phiếu phân phối thứ n thì phải cùng AgentCode:
                {
                    string strSql_Check = CmUtils.StringUtils.Replace(@"
                                ----
                                select distinct
	                                f.AgentCode
                                from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	                                inner join Inv_InventoryBalanceSerial f --//[mylock]
		                                on t.SerialNo = f.SerialNo
                                where(1=1)
                                    and f.IF_InvOutFGNo is not null
                                ;
                            ");
                    DataTable dt_Check = _cf.db.ExecQuery(
                        strSql_Check
                        ).Tables[0];
                    ////
                    if (dt_Check.Rows.Count > 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.AgentCode.01", dt_Check.Rows[0]["AgentCode"]
                            , "Check.AgentCode.02", dt_Check.Rows[0]["AgentCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOutHist_Save_InvF_AgentCodeNotEqual
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                }
                #endregion
                #endregion

                #region // Build InvF_InventoryOutHistInstSerial:
                {
                    string strSql_Build_InvF_InventoryOutHistInstSerial = CmUtils.StringUtils.Replace(@"
                            --- #tbl_InvF_InventoryOutHistInstSerial_Prev:
                            select 
	                            f.IF_InvOutHistNo IF_InvOutHistPrevNo
	                            , f.PartCode PartPrevCode
	                            , f.SerialNo SerialNo
	                            , iibs.IF_InvOutFGNo 
                            into #tbl_InvF_InventoryOutHistInstSerial_Prev
                            from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	                            left join InvF_InventoryOutHistInstSerial f --//[mylock]
		                            on t.SerialNo = f.SerialNo
			                            and f.FlagCurrent = '1'
	                            inner join Inv_InventoryBalanceSerial iibs --//[mylock]
		                            on t.SerialNo = iibs.SerialNo
                            where(1=1)
                            ;

                            --- #tbl_InvF_InventoryOutHistInstSerial:
                            select 
	                            t.IF_InvOutHistNo
	                            , t.PartCode
	                            , t.SerialNo
	                            , t.NetworkID
	                            , null IF_InvOutHistPrevNo
	                            , '0' FlagInitBase
	                            , '1' FlagCurrent
	                            , t.IF_InvOutHistISStatus IF_InvOutHistISStatus
	                            , t.LogLUDTimeUTC
	                            , t.LogLUBy
                            into #tbl_InvF_InventoryOutHistInstSerial
                            from #input_InvF_InventoryOutHistInstSerial t --//[mylock]
	                            --left join #tbl_InvF_InventoryOutHistInstSerial_Prev f --//[mylock]
		                         --   on t.SerialNo = f.SerialNo
                            where(1=1)
                            ;

                            ---select t.* from #tbl_InvF_InventoryOutHistInstSerial t 
                            ---- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutHistInstSerial_Prev;
                                
                            ");
                    DataSet ds_Build = _cf.db.ExecQuery(
                        strSql_Build_InvF_InventoryOutHistInstSerial
                        );
                    ////
                }
                #endregion 

            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutHistInstSerial:
                            select 
                                t.IF_InvOutHistNo
                                , t.PartCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryOutHistInstSerial_Del
                            from InvF_InventoryOutHistInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOutHist f --//[mylock]
		                            on t.IF_InvOutHistNo = f.IF_InvOutHistNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutHistInstSerial:
                            delete t 
                            from InvF_InventoryOutHistInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutHistInstSerial_Del f --//[mylock]
		                            on t.IF_InvOutHistNo = f.IF_InvOutHistNo
		                                and t.PartCode = f.PartCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutHistDtl:
                            select 
                                t.IF_InvOutHistNo
                                , t.PartCode
                            into #tbl_InvF_InventoryOutHistDtl
                            from InvF_InventoryOutHistDtl t --//[mylock]
	                            inner join #input_InvF_InventoryOutHist f --//[mylock]
		                            on t.IF_InvOutHistNo = f.IF_InvOutHistNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutHistDtl:
                            delete t 
                            from InvF_InventoryOutHistDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutHistDtl f --//[mylock]
		                            on t.IF_InvOutHistNo = f.IF_InvOutHistNo
		                                and t.PartCode = f.PartCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryOutHist:
                            delete t
                            from InvF_InventoryOutHist t --//[mylock]
	                            inner join #input_InvF_InventoryOutHist f --//[mylock]
		                            on t.IF_InvOutHistNo = f.IF_InvOutHistNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutHistInstSerial_Del;
                            drop table #tbl_InvF_InventoryOutHistDtl;
							");
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {
                        ////
                        string zzzzClauseInsert_InvF_InventoryOutHist_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutHist:                                
                                insert into InvF_InventoryOutHist(
	                                IF_InvOutHistNo
	                                , NetworkID
	                                , MST
	                                , FormOutType
	                                , InvOutType
	                                , InvCode
	                                , PMType
	                                , InvFOutType
	                                , PlateNo
	                                , MoocNo
	                                , DriverName
	                                , DriverPhoneNo
	                                , AgentCode
	                                , CustomerName
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , IF_InvOutHistStatus
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutHistNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.FormOutType
	                                , t.InvOutType
	                                , t.InvCode
	                                , t.PMType
	                                , t.InvFOutType
	                                , t.PlateNo
	                                , t.MoocNo
	                                , t.DriverName
	                                , t.DriverPhoneNo
	                                , t.AgentCode
	                                , t.CustomerName
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.IF_InvOutHistStatus
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutHist t 
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutHistDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutHist:                                
                                insert into InvF_InventoryOutHistDtl(
	                                IF_InvOutHistNo
	                                , PartCode
	                                , NetworkID
	                                , Qty
	                                , IF_InvOutHistStatusDtl
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutHistNo
	                                , t.PartCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.IF_InvOutHistStatusDtl
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutHistDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutHistInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutHistInstSerial:                                
                                insert into InvF_InventoryOutHistInstSerial(
	                                IF_InvOutHistNo
	                                , PartCode
	                                , SerialNo
	                                , NetworkID
	                                , IF_InvOutHistPrevNo
	                                , FlagInitBase
	                                , FlagCurrent
	                                , IF_InvOutHistISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutHistNo
	                                , t.PartCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.IF_InvOutHistPrevNo
	                                , t.FlagInitBase
	                                , t.FlagCurrent
	                                , t.IF_InvOutHistISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #tbl_InvF_InventoryOutHistInstSerial t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryOutHist_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutHistDtl_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutHistInstSerial_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryOutHist_zSave", zzzzClauseInsert_InvF_InventoryOutHist_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutHistDtl_zSave", zzzzClauseInsert_InvF_InventoryOutHistDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutHistInstSerial_zSave", zzzzClauseInsert_InvF_InventoryOutHistInstSerial_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region //// Clear For Debug:
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutHist;
						drop table #input_InvF_InventoryOutHistDtl;
						drop table #input_InvF_InventoryOutHistInstSerial;
						drop table #tbl_InvF_InventoryOutHistInstSerial;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            else
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOutHist;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////

            }
        #endregion

        // Return Good:
        MyCodeLabel_Done:
            return;

        }

        public DataSet InvF_InventoryOutHist_SaveSpecial(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            , object objFlagAppr
            ////
            , object objIF_InvOutHistNo
            , object objMST
            , object objFormOutType
            , object objInvOutType
            , object objInvCode
            , object objPMType
            , object objInvFOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objAgentCode
            , object objCustomerName
            ////
            , object objRemark
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryOutHist_SaveSpecial";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutHist_SaveSpecial;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
			        ////
                    , "objIF_InvOutHistNo", objIF_InvOutHistNo
                    , "objMST", objMST
                    , "objFormOutType", objFormOutType
                    , "objInvOutType", objInvOutType
                    , "objInvCode", objInvCode  
                    , "objPMType", objPMType
                    , "objInvFOutType", objInvFOutType
                    , "objPlateNo", objPlateNo
                    , "objMoocNo", objMoocNo
                    , "objDriverName", objDriverName
                    , "objDriverPhoneNo", objDriverPhoneNo
                    , "objAgentCode", objAgentCode
                    , "objCustomerName", objCustomerName
                    , "objRemark", objRemark
                    ////
                    //, "objTInvoiceFilePathXML", objTInvoiceFilePathXML
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

                #region // InvF_InventoryOutHist_SaveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryOutHist_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                                          ////
                        , objIF_InvOutHistNo // objIF_InvOutHistNo
                        , objMST // objMST
                        , objFormOutType // objFormOutType
                        , objInvOutType // objInvOutType
                        , objInvCode // objInvCode
                        , objPMType // objPMType
                        , objFormOutType // objFormOutType
                        , objPlateNo // objPlateNo
                        , objMoocNo // objMoocNo
                        , objDriverName // objDriverName
                        , objDriverPhoneNo // objDriverPhoneNo
                        , objAgentCode // objAgentCode
                        , objCustomerName // objCustomerName
                        , objRemark // objRemark
                                    //// 
                        , dsData  // dsData
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

        public DataSet WAS_InvF_InventoryOutHist_SaveSpecial(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist
            ////
            , out RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutHist.Tid;
            objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutHist_SaveSpecial";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutHist_SaveSpecial;
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
                //List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistDtl == null)
                        objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistDtl = new List<InvF_InventoryOutHistDtl>();
                    {
                        DataTable dt_InvF_InventoryOutHist = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutHistDtl>(objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistDtl, "InvF_InventoryOutHistDtl");
                        dsData.Tables.Add(dt_InvF_InventoryOutHist);
                    }
                    ////
                    if (objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistInstSerial == null)
                        objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistInstSerial = new List<InvF_InventoryOutHistInstSerial>();
                    {
                        DataTable dt_InvF_InventoryOutHistInstSerial = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutHistInstSerial>(objRQ_InvF_InventoryOutHist.Lst_InvF_InventoryOutHistInstSerial, "InvF_InventoryOutHistInstSerial");
                        dsData.Tables.Add(dt_InvF_InventoryOutHistInstSerial);
                    }
                    ////
                }
                #endregion

                #region // InvF_InventoryInFG_Save:
                mdsResult = InvF_InventoryOutHist_SaveSpecial(
                    objRQ_InvF_InventoryOutHist.Tid // strTid
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutHist.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutHist.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutHist.FlagIsDelete // objFlagIsDelete
                    , objRQ_InvF_InventoryOutHist.FlagAppr // objFlagAppr
                                                           /////
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.IF_InvOutHistNo // objIF_InvOutHistNo
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.MST // objMST
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.FormOutType // objFormOutType
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.InvOutType // objInvOutType
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.InvCode // objInvCode
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.PMType // objPMType
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.InvFOutType // objInvFOutType
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.PlateNo // objPlateNo
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.MoocNo // objMoocNo
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.DriverName // objDriverName
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.DriverPhoneNo // objDriverPhoneNo
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.AgentCode // objAgentCode
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.CustomerName // objCustomerName
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.Remark // objRemark
                    /////
                    , dsData
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
        private void InvF_InventoryOutHist_ApproveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objIF_InvOutHistNo
            , object objRemark
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOutHist_ApproveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "objIF_InvOutHistNo", objIF_InvOutHistNo
                , "objRemark", objRemark
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            #endregion

            #region // Refine and Check Input InvF_InventoryOutHist:
            ////
            string strIF_InvOutHistNo = TUtils.CUtils.StdParam(objIF_InvOutHistNo);
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////
            DataTable dtDB_InvF_InventoryOutHist = null;
            {
                /////
                InvF_InventoryOutHist_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutHistNo // objInvoiceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvOutHistStatus.Pending // strIF_InvOutFGStatusListToCheck
                    , out dtDB_InvF_InventoryOutHist // dtDB_Invoice_Invoice
                    );
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryOutHist:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOutHist"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutHistNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutHistStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutHistNo, // IF_InvOutHistNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvOutHistStatus.Approve, // IF_InvOutHistStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_InvF_InventoryOutHist_ClauseSet_zzE = @"
                        t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
                        , t.Remark = f.Remark
                        , t.ApprDTimeUTC = f.ApprDTimeUTC
                        , t.ApprBy = f.ApprBy
                        , t.IF_InvOutHistStatus = f.IF_InvOutHistStatus
                        ";
                ////
                string zzB_Update_InvF_InventoryOutHistDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutHistStatusDtl = f.IF_InvOutHistStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutHistInstSerial_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutHistISStatus = f.IF_InvOutHistStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutHist_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryOutHist:
						update t
						set 
							zzB_Update_InvF_InventoryOutHist_ClauseSet_zzE
						from InvF_InventoryOutHist t --//[mylock]
							inner join #input_InvF_InventoryOutHist f --//[mylock]
								on t.IF_InvOutHistNo = f.IF_InvOutHistNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryOutHist_ClauseSet_zzE", zzB_Update_InvF_InventoryOutHist_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutHistDtl_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutHistDtl_Temp: 
                            select 
                                t.IF_InvOutHistNo
                                , t.PartCode
                                , f.IF_InvOutHistStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutHistDtl_Temp
							from InvF_InventoryOutHistDtl t --//[mylock]
							    inner join #input_InvF_InventoryOutHist f --//[mylock]
								    on t.IF_InvOutHistNo = f.IF_InvOutHistNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutHistDtl_ClauseSet_zzE
							from InvF_InventoryOutHistDtl t --//[mylock]
							    inner join #tbl_InvF_InventoryOutHistDtl_Temp f --//[mylock]
								    on t.IF_InvOutHistNo = f.IF_InvOutHistNo
								        and t.PartCode = f.PartCode
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutHistDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryOutHistDtl_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutHistInstSerial_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutHistInstSerial_Temp: 
                            select 
                                t.IF_InvOutHistNo
                                , t.PartCode
                                , t.SerialNo
                                , f.IF_InvOutHistStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_InvF_InventoryOutHistInstSerial_Temp
							from InvF_InventoryOutHistInstSerial t --//[mylock]
							    inner join #input_InvF_InventoryOutHist f --//[mylock]
								    on t.IF_InvOutHistNo = f.IF_InvOutHistNo
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_InvF_InventoryOutHistInstSerial_ClauseSet_zzE
							from InvF_InventoryOutHistInstSerial t --//[mylock]
							    inner join #tbl_InvF_InventoryOutHistInstSerial_Temp f --//[mylock]
								    on t.IF_InvOutHistNo = f.IF_InvOutHistNo
								        and t.PartCode = f.PartCode
								        and t.SerialNo = f.SerialNo
							where (1=1)
							;
				    "
                    , "zzB_Update_InvF_InventoryOutHistInstSerial_ClauseSet_zzE", zzB_Update_InvF_InventoryOutHistInstSerial_ClauseSet_zzE
                    );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryOutHist_zzE
                        ----
						zzB_Update_InvF_InventoryOutHistDtl_zzE
                        ----
						zzB_Update_InvF_InventoryOutHistInstSerial_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryOutHist_zzE", zzB_Update_InvF_InventoryOutHist_zzE
                    , "zzB_Update_InvF_InventoryOutHistDtl_zzE", zzB_Update_InvF_InventoryOutHistDtl_zzE
                    , "zzB_Update_InvF_InventoryOutHistInstSerial_zzE", zzB_Update_InvF_InventoryOutHistInstSerial_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            // Return Good:
            return;
        }


        public DataSet InvF_InventoryOutHist_Approve(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objIF_InvOutHistNo
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "InvF_InventoryOutHist_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOutHist_Approve;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objIF_InvOutHistNo", objIF_InvOutHistNo
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

                #region // InvF_InventoryOutHist_ApproveX:
                //DataSet dsGetData = null;
                {
                    InvF_InventoryOutHist_ApproveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objIF_InvOutHistNo // objIF_InvOutHistNo
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


        public DataSet WAS_InvF_InventoryOutHist_Approve(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist
            ////
            , out RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOutHist.Tid;
            objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOutHist_Approve";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOutHist_Approve;
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

                #region // InvF_InventoryOutHist_Approve:
                mdsResult = InvF_InventoryOutHist_Approve(
                    objRQ_InvF_InventoryOutHist.Tid // strTid
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOutHist.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOutHist.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.IF_InvOutHistNo // objIF_InvOutHistNo
                    , objRQ_InvF_InventoryOutHist.InvF_InventoryOutHist.Remark // objRemark
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
    }
}
