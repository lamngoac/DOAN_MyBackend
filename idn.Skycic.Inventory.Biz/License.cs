using idn.Skycic.Inventory.BizService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TConst = idn.Skycic.Inventory.Constants;
using TDALUtils = EzDAL.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Inv_GenTimes:
        private void Inv_GenTimes_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objGenTimesNo
            , string strFlagExistToCheck
            , string strPrintTempStatusListToCheck
            , out DataTable dtDB_Inv_GenTimes
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Inv_GenTimes t --//[mylock]
					where (1=1)
						and t.GenTimesNo = @objGenTimesNo
					;
				");
            dtDB_Inv_GenTimes = _cf.db.ExecQuery(
                strSqlExec
                , "@objGenTimesNo", objGenTimesNo
                ).Tables[0];
            dtDB_Inv_GenTimes.TableName = "Inv_GenTimes";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Inv_GenTimes.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesNo", objGenTimesNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_CheckDB_GenTimesNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Inv_GenTimes.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesNo", objGenTimesNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_CheckDB_GenTimesNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strPrintTempStatusListToCheck.Length > 0 && !strPrintTempStatusListToCheck.Contains(Convert.ToString(dtDB_Inv_GenTimes.Rows[0]["PrintTempStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.GenTimesNo", objGenTimesNo
                    , "Check.strPrintTempStatusListToCheck", strPrintTempStatusListToCheck
                    , "DB.PrintTempStatus", dtDB_Inv_GenTimes.Rows[0]["PrintTempStatus"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inv_GenTimes_CheckDB_PrintTempStatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        private void Inv_GenTimes_GetX(
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
            , string strRt_Cols_Inv_GenTimes
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimes_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimes", strRt_Cols_Inv_GenTimes
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Inv_GenTimes = (strRt_Cols_Inv_GenTimes != null && strRt_Cols_Inv_GenTimes.Length > 0);

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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_GenTimes_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.GenTimesNo
						into #tbl_Inv_GenTimes_Filter_Draft
						from Inv_GenTimes tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.GenTimesNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_GenTimes_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_GenTimes_Filter:
						select
							t.*
						into #tbl_Inv_GenTimes_Filter
						from #tbl_Inv_GenTimes_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_GenTimes ------:
						zzB_Select_Inv_GenTimes_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_GenTimes_Filter_Draft;
						--drop table #tbl_Inv_GenTimes_Filter;
					"
                );
            ////
            string zzB_Select_Inv_GenTimes_zzE = "-- Nothing.";
            if (bGet_Inv_GenTimes)
            {
                #region // bGet_Inv_GenTimes:
                zzB_Select_Inv_GenTimes_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_GenTimes:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_GenTimes_Filter t --//[mylock]
	                        inner join Inv_GenTimes tpt --//[mylock]
		                        on t.GenTimesNo = tpt.GenTimesNo
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
                        , "Inv_GenTimes" // strTableNameDB
                        , "Inv_GenTimes." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "zzB_Select_Inv_GenTimes_zzE", zzB_Select_Inv_GenTimes_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_GenTimes)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_GenTimes";
            }
            #endregion
        }

        public DataSet Inv_GenTimes_Get(
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
            , string strRt_Cols_Inv_GenTimes
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimes_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimes_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimes", strRt_Cols_Inv_GenTimes
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

                #region // Inv_GenTimes_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_GenTimes_GetX(
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
                        , strRt_Cols_Inv_GenTimes // strRt_Cols_Inv_GenTimes
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

        public DataSet WAS_Inv_GenTimes_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimes objRQ_Inv_GenTimes
            ////
            , out RT_Inv_GenTimes objRT_Inv_GenTimes
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimes.Tid;
            objRT_Inv_GenTimes = new RT_Inv_GenTimes();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimes_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimes_Get;
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
                List<Inv_GenTimes> lst_Inv_GenTimes = new List<Inv_GenTimes>();
                #endregion

                #region // WAS_Inv_GenTimes_Get:
                mdsResult = Inv_GenTimes_Get(
                    objRQ_Inv_GenTimes.Tid // strTid
                    , objRQ_Inv_GenTimes.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimes.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimes.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimes.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_GenTimes.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_GenTimes.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_GenTimes.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Inv_GenTimes.Rt_Cols_Inv_GenTimes // strRt_Cols_Inv_GenTimes
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_GenTimes.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_GenTimes = mdsResult.Tables["Inv_GenTimes"].Copy();
                    lst_Inv_GenTimes = TUtils.DataTableCmUtils.ToListof<Inv_GenTimes>(dt_Inv_GenTimes);
                    objRT_Inv_GenTimes.Lst_Inv_GenTimes = lst_Inv_GenTimes;
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

        private void Inv_GenTimes_AddX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objGenTimesNo
            , object objMST
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_GenTimes_SaveX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objGenTimesNo",objGenTimesNo
                , "objMST",objMST
                , "objQty",objQty
                , "objRemark", objRemark
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            //alParamsCoupleError.AddRange(new object[]{
            //    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //    });
            #endregion

            #region // Refine and Check Input:
            ////
            string strGenTimesNo = TUtils.CUtils.StdParam(objGenTimesNo);
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strRemark = string.Format("{0}", objRemark).Trim();
            Int64 nQty = Convert.ToInt64(objQty);
            ////
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            ////
            DataTable dtDB_Inv_GenTimes = null;
            DataTable dtDB_Mst_NNT = null;
            string strConfigName = "";
            string strOrgID = "";
            {
                ////
                if (strGenTimesNo == null || strGenTimesNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strGenTimesNo", strGenTimesNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_AddX_InvalidGenTimesNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Inv_GenTimes_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strGenTimesNo // objShipNo
                    , "" // strFlagExistToCheck
                    , "" // strTInvoiceStatusListToCheck
                    , out dtDB_Inv_GenTimes // dtDB_Inv_GenTimes
                    );
                ////
                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError 
                    , objMST // objMST 
                    , TConst.Flag.Yes // strFlagExistListToCheck 
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );
                ////

                strOrgID = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["OrgID"]);
                ////
                DataTable dtDB_Invoice_license = null;

                Invoice_license_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // strMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Invoice_license // dtDB_Invoice_license
                    );
                /////
                Int64 nTotalQtyIssued = Convert.ToInt64(dtDB_Invoice_license.Rows[0]["TotalQtyIssued"]);
                Int64 nTotalQty = Convert.ToInt64(dtDB_Invoice_license.Rows[0]["TotalQty"]);
                Int64 nTotalQtyUsed = Convert.ToInt64(dtDB_Invoice_license.Rows[0]["TotalQtyUsed"]);
                Int64 nQtyRemain = nTotalQty - nTotalQtyIssued;
                ////
                if (nQty <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.nQty", nQty
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_AddX_InvalidQty
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                ////
                if(nQtyRemain < nQty)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.nTotalQty", nTotalQty
                        , "Check.nQtyRemain", nQtyRemain
                        , "Check.nQty", nQty
                        , "Check.ConditionalRaise", "nQtyRemain < nQty"
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_AddX_InvalidQty
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                };
                ////
                if (nQty > TConst.BizMix.nQtyMaxGen)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.nQty", nQty
                        , "Check.nQtyMaxGen", TConst.BizMix.nQtyMaxGen
                        , "Check.ConditionalRaise", "nQty > TConst.BizMix.nQtyMaxGen"
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_AddX_InvalidQty
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                };
                ////
                strCreateDTimeUTC = string.IsNullOrEmpty(strCreateDTimeUTC) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTimeUTC;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            #endregion

            #region // Get domain:
            {
                ////
                string strGet_ConfigName = CmUtils.StringUtils.Replace(@"
                                select 
                                    t.*
                                from Mst_Config  t --//[mylock]
                                where(1=1)
	                                and t.ConfigType = 'PRODID'
                                ;
                            ");
                ////
                DataSet dsExec = _cf.db.ExecQuery(strGet_ConfigName);
                ////
                DataTable dtDB_Get_ConfigName = dsExec.Tables[0];
                if (dtDB_Get_ConfigName.Rows.Count > 0)
                {
                    strConfigName = string.Format("{0}", dtDB_Get_ConfigName.Rows[0]["ConfigName"]).Trim();
                }
                ////
                if (string.IsNullOrEmpty(strConfigName))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strConfigName", strConfigName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimes_Add_InvalidConfigName
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // Get domain:
            string strOrgIDSln = "";
            {
                DataTable dtDB_Mst_Org = null;
                Mst_Org_CheckDB(
                    ref alParamsCoupleError
                    , strOrgID
                    , TConst.Flag.Yes
                    , TConst.Flag.Active
                    , out dtDB_Mst_Org
                    );

                if (dtDB_Mst_Org.Rows.Count > 0)
                {
                    strOrgIDSln = string.Format("{0}", dtDB_Mst_Org.Rows[0]["OrgIDSln"]).Trim();
                }
            }
            #endregion

            #region // SaveTemp Inv_GenTimes:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Inv_GenTimes"
                    , new object[]{
                        "GenTimesNo", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "MST", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ConfigDomain", TConst.BizMix.Default_DBColType,
                        "Qty", TConst.BizMix.Default_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Max_DBCol,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strGenTimesNo, // TInvoiceCode
                                nNetworkID, // NetworkID
                                strMST, // MST
                                strOrgID, // OrgID
                                strConfigName, // ConfigDomain
                                objQty, // Qty
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // Build Inv_InventorySecret:
            DataTable dt_Input_Inv_InventorySecret = new DataTable("Inv_InventorySecret");
            {
                dt_Input_Inv_InventorySecret.Columns.Add("SerialNo", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("NetworkID", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("MST", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("OrgID", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("SecretNo", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("GenTimesNo", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("QR_SerialNo", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("FlagMap", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("FlagUsed", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("Remark", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("LogLUDTimeUTC", typeof(object));
                dt_Input_Inv_InventorySecret.Columns.Add("LogLUBy", typeof(object));
                ////
                List<string> lstSeq_ObjCode  = new List<string>();

                Seq_GenObjCode_GetX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , nNetworkID.ToString() // nNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                    , strOrgIDSln // strOrgID
                    , nQty // nQty
                    , out lstSeq_ObjCode // lstSeq_ObjCode
                    );
                ////
                for (int nScan = 0; nScan < lstSeq_ObjCode.Count; nScan++)
                {
                    ////
                    string strQR_SerialNo = string.Format("{0}{1}", strConfigName, lstSeq_ObjCode[nScan]).Trim();
                    dt_Input_Inv_InventorySecret.Rows.Add(
                        lstSeq_ObjCode[nScan]// SerialNo
                        , nNetworkID // NetworkID
                        , strMST // MST
                        , strOrgID // OrgID
                        , lstSeq_ObjCode[nScan] // SecretNo
                        , strGenTimesNo // GenTimesNo
                        , strQR_SerialNo // QR_SerialNo
                        , 0 // FlagMap 0 Chưa map/1 đã map
                        , 0 // FlagUsed 0 chưa dùng/1 đã dùng
                        , "" // Remark
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // LogLUDTimeUTC
                        , strWAUserCode // LogLUBy
                        );
                    ////

                }
                //

            }
            #endregion 

            #region //// SaveTemp Inv_InventorySecret:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventorySecret" // strTableName
                    , new object[] {
                            "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "MST", TConst.BizMix.Default_DBColType
                            , "OrgID", TConst.BizMix.Default_DBColType
                            , "SecretNo", TConst.BizMix.Default_DBColType
                            , "GenTimesNo", TConst.BizMix.Default_DBColType
                            , "QR_SerialNo", TConst.BizMix.Default_DBColType
                            , "FlagMap", TConst.BizMix.Default_DBColType
                            , "FlagUsed", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dt_Input_Inv_InventorySecret // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzzzClauseInsert_Inv_GenTimes_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_GenTimes:
                                insert into Inv_GenTimes
                                (	
	                                GenTimesNo
	                                , NetworkID
	                                , MST
	                                , OrgID
	                                , ConfigDomain
	                                , Qty
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.GenTimesNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.OrgID
	                                , t.ConfigDomain
	                                , t.Qty
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_GenTimes t --//[mylock]
                                ;
                            ");

                string zzzzClauseInsert_Inv_InventorySecret_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_InventorySecret:
                                insert into Inv_InventorySecret
                                (	
	                                SerialNo
	                                , NetworkID
	                                , MST
	                                , OrgID
	                                , SecretNo
	                                , GenTimesNo
	                                , QR_SerialNo
	                                , FlagMap
	                                , FlagUsed
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.SerialNo
	                                , t.NetworkID
	                                , t.MST
	                                , t.OrgID
	                                , t.SecretNo
	                                , t.GenTimesNo
	                                , t.QR_SerialNo
	                                , t.FlagMap
	                                , t.FlagUsed
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_InventorySecret t --//[mylock]
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Inv_GenTimes_zSave			
								----
								zzzzClauseInsert_Inv_InventorySecret_zSave			
								----
							"
                    , "zzzzClauseInsert_Inv_GenTimes_zSave", zzzzClauseInsert_Inv_GenTimes_zSave
                    , "zzzzClauseInsert_Inv_InventorySecret_zSave", zzzzClauseInsert_Inv_InventorySecret_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region // myUpdate_Invoice_license_TotalQtyUsed:
            {
                myUpdate_Invoice_license_TotalQtyIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strWAUserCode // strWAUserCode
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , nQty // nQty
                    );
            }
            #endregion 

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_GenTimes;
						drop table #input_Inv_InventorySecret;
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

        public DataSet Inv_GenTimes_Add(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGenTimesNo
            , object objMST
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_GenTimes_Add";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimes_Add;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objGenTimesNo", objGenTimesNo
                    , "objMST", objMST
                    , "objQty", objQty
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

                #region // Inv_GenTimes_AddX:
                //DataSet dsGetData = null;
                {
                    Inv_GenTimes_AddX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objGenTimesNo // objGenTimesNo
                        , objMST // objMST
                        , objQty // objQty
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

        public DataSet WAS_Inv_GenTimes_Add(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimes objRQ_Inv_GenTimes
            ////
            , out RT_Inv_GenTimes objRT_Inv_GenTimes
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimes.Tid;
            objRT_Inv_GenTimes = new RT_Inv_GenTimes();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_GenTimes.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimes_Add";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimes_Add;
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
                #endregion

                #region // Inv_GenTimes_Save:
                mdsResult = Inv_GenTimes_Add(
                    objRQ_Inv_GenTimes.Tid // strTid
                    , objRQ_Inv_GenTimes.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimes.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimes.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimes.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    /////
                    , objRQ_Inv_GenTimes.Inv_GenTimes.GenTimesNo // objGenTimesNo
                    , objRQ_Inv_GenTimes.Inv_GenTimes.MST // objMST
                    , objRQ_Inv_GenTimes.Inv_GenTimes.Qty // objQty
                    , objRQ_Inv_GenTimes.Inv_GenTimes.Remark // objRemark
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

        #region // Inv_InventorySecret:
        private void Inv_InventorySecret_GetX(
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
            , string strRt_Cols_Inv_InventorySecret
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventorySecret_GetTimesNoX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inv_InventorySecret_GetTimesNoX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventorySecret", strRt_Cols_Inv_InventorySecret
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Inv_InventorySecret = (strRt_Cols_Inv_InventorySecret != null && strRt_Cols_Inv_InventorySecret.Length > 0);

            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

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
						---- #tbl_Inv_InventorySecret_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.SerialNo
						into #tbl_Inv_InventorySecret_Filter_Draft
						from Inv_InventorySecret tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.SerialNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventorySecret_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventorySecret_Filter:
						select
							t.*
						into #tbl_Inv_InventorySecret_Filter
						from #tbl_Inv_InventorySecret_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventorySecret ------:
						zzB_Select_Inv_InventorySecret_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventorySecret_Filter_Draft;
						--drop table #tbl_Inv_InventorySecret_Filter;
					"
                );
            ////
            string zzB_Select_Inv_InventorySecret_zzE = "-- Nothing.";
            if (bGet_Inv_InventorySecret)
            {
                #region // bGet_Inv_InventorySecret:
                zzB_Select_Inv_InventorySecret_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventorySecret:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_InventorySecret_Filter t --//[mylock]
	                        inner join Inv_InventorySecret tpt --//[mylock]
		                        on t.SerialNo = tpt.SerialNo
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
                        , "Inv_InventorySecret" // strTableNameDB
                        , "Inv_InventorySecret." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "zzB_Select_Inv_InventorySecret_zzE", zzB_Select_Inv_InventorySecret_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_InventorySecret)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_InventorySecret";
            }
            #endregion
        }

        public DataSet Inv_InventorySecret_Get(
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
            , string strRt_Cols_Inv_InventorySecret
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventorySecret_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventorySecret_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventorySecret", strRt_Cols_Inv_InventorySecret
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

                #region // Inv_InventorySecret_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_InventorySecret_GetX(
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
                        , strRt_Cols_Inv_InventorySecret // strRt_Cols_Inv_InventorySecret
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

        public DataSet WAS_Inv_InventorySecret_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventorySecret objRQ_Inv_InventorySecret
            ////
            , out RT_Inv_InventorySecret objRT_Inv_InventorySecret
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventorySecret.Tid;
            objRT_Inv_InventorySecret = new RT_Inv_InventorySecret();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventorySecret_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventorySecret_Get;
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
                List<Inv_InventorySecret> lst_Inv_InventorySecret = new List<Inv_InventorySecret>();
                #endregion

                #region // WAS_Inv_InventorySecret_Get:
                mdsResult = Inv_InventorySecret_Get(
                    objRQ_Inv_InventorySecret.Tid // strTid
                    , objRQ_Inv_InventorySecret.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventorySecret.GwPassword // strGwPassword
                    , objRQ_Inv_InventorySecret.WAUserCode // strUserCode
                    , objRQ_Inv_InventorySecret.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_InventorySecret.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_InventorySecret.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_InventorySecret.Ft_WhereClause // strFt_WhereClause
                                                               //// Return:
                    , objRQ_Inv_InventorySecret.Rt_Cols_Inv_InventorySecret // strRt_Cols_Inv_InventorySecret
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_InventorySecret.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_InventorySecret = mdsResult.Tables["Inv_InventorySecret"].Copy();
                    lst_Inv_InventorySecret = TUtils.DataTableCmUtils.ToListof<Inv_InventorySecret>(dt_Inv_InventorySecret);
                    objRT_Inv_InventorySecret.Lst_Inv_InventorySecret = lst_Inv_InventorySecret;
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


        private void Inv_InventorySecret_UpdateFlagUsedX(
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
            string strFunctionName = "Inv_InventorySecret_UpdateFlagUsedX";
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

            #region // Refine and Check Invoice_TempGroupField:
            DataTable dtInput_Inv_InventorySecret = null;
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            {
                ////
                string strTableCheck = "Inv_InventorySecret";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventorySecret = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Inv_InventorySecret.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventorySecret // dtData
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventorySecret, "FlagUsed", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventorySecret, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventorySecret, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventorySecret.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventorySecret.Rows[nScan];
                    ////
                    drScan["FlagUsed"] = TConst.Flag.Active;
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
                    , "#input_Inv_InventorySecret" // strTableName
                    , new object[] {
                            "SerialNo", TConst.BizMix.Default_DBColType
                            , "FlagUsed", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventorySecret // dtData
                );
            }
            #endregion

            #region // Check : Serial tồn tại.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                        from #input_Inv_InventorySecret t --//[mylock]
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
                        "Check.Serial.NotExist", dtCheck.Rows[0]["SerialNo"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed_InvalidSerial
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // Check : Serial Chưa được sử dụng.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                            , f.FlagUsed
                        from #input_Inv_InventorySecret t --//[mylock]
                            inner join Inv_InventorySecret f --//[mylock]
                                on t.SerialNo = f.SerialNo
                        where(1=1)
                            and f.FlagUsed = '1' -- đã sử dụng
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
                        , "Check.FlagUsed.Expected", TConst.Flag.No
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed_InvalidFlagUsed
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // SaveDB:
            {
                ////
                string zzzzClauseUpdate_Inv_InventorySecret_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Update:
                                update t 
                                set
                                    t.FlagUsed = f.FlagUsed
                                    , t.LogLUDTimeUTC = f.LogLUDTimeUTC
                                    , t.LogLUBy = f.LogLUBy
                                from Inv_InventorySecret t --//[mylock]
                                    inner join #input_Inv_InventorySecret f --//[mylock]
                                        on t.SerialNo = f.SerialNo
                                where(1=1)
                                ;
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseUpdate_Inv_InventorySecret_zSave			
								----
							"
                    , "zzzzClauseUpdate_Inv_InventorySecret_zSave", zzzzClauseUpdate_Inv_InventorySecret_zSave
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
                        from #input_Inv_InventorySecret t --//[mylock]
                            inner join Inv_InventorySecret f --//[mylock]
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
                        TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed_InvalidMST
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // Update: Invoice_license.
            {
                string strSql_Update = CmUtils.StringUtils.Replace(@"
                        -- Update:
                        update t 
                        set
	                        t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
	                        , t.LogLUBy = '@strLogLUBy'
	                        , t.TotalQtyUsed = t.TotalQtyUsed + @nTotalQtyUsed
                        from Invoice_license t --//[mylock]
                        where(1=1)
		                    and t.MST = '@strMST'
                        ;
                    "
                    , "@strMST", drAbilityOfUser["MST"]
                    , "@nTotalQtyUsed", dtInput_Inv_InventorySecret.Rows.Count
                    , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "@strLogLUBy", strWAUserCode
                    );
                //
                _cf.db.ExecQuery(strSql_Update);
            }
            #endregion 

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventorySecret;
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

        public DataSet Inv_InventorySecret_UpdateFlagUsed(
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
            string strFunctionName = "Inv_InventorySecret_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventorySecret_UpdateFlagUsed;
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

                #region // Inv_InventorySecret_UpdateFlagUsedX:
                //DataSet dsGetData = null;
                {
                    Inv_InventorySecret_UpdateFlagUsedX(
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

        public DataSet WAS_Inv_InventorySecret_UpdateFlagUsed(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventorySecret objRQ_Inv_InventorySecret
            ////
            , out RT_Inv_InventorySecret objRT_Inv_InventorySecret
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventorySecret.Tid;
            objRT_Inv_InventorySecret = new RT_Inv_InventorySecret();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventorySecret_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventorySecret_UpdateFlagUsed;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Inv_InventorySecret", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventorySecret.Lst_Inv_InventorySecret)
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
                    DataTable dt_Invoice_TempCustomField = TUtils.DataTableCmUtils.ToDataTable<Inv_InventorySecret>(objRQ_Inv_InventorySecret.Lst_Inv_InventorySecret, "Inv_InventorySecret");
                    dsData.Tables.Add(dt_Invoice_TempCustomField);

                }
                #endregion

                #region // Invoice_TempGroup_Create:
                mdsResult = Inv_InventorySecret_UpdateFlagUsed(
                    objRQ_Inv_InventorySecret.Tid // strTid
                    , objRQ_Inv_InventorySecret.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventorySecret.GwPassword // strGwPassword
                    , objRQ_Inv_InventorySecret.WAUserCode // strUserCode
                    , objRQ_Inv_InventorySecret.WAUserPassword // strUserPassword
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
        #endregion

        #region // Inv_GenTimesBox:
        private void Inv_GenTimesBox_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objGenTimesBoxNo
            , string strFlagExistToCheck
            , string strGenTimesBoxNoStatusListToCheck
            , out DataTable dtDB_Inv_GenTimesBox
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Inv_GenTimesBox t --//[mylock]
					where (1=1)
						and t.GenTimesBoxNo = @objGenTimesBoxNo
					;
				");
            dtDB_Inv_GenTimesBox = _cf.db.ExecQuery(
                strSqlExec
                , "@objGenTimesBoxNo", objGenTimesBoxNo
                ).Tables[0];
            dtDB_Inv_GenTimesBox.TableName = "Inv_GenTimesBox";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Inv_GenTimesBox.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesBoxNo", objGenTimesBoxNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesBox_CheckDB_GenTimesBoxNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Inv_GenTimesBox.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesBoxNo", objGenTimesBoxNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesBox_CheckDB_GenTimesBoxNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            //// strFlagActiveListToCheck:
            //if (strGenTimesBoxNoStatusListToCheck.Length > 0 && !strGenTimesBoxNoStatusListToCheck.Contains(Convert.ToString(dtDB_Inv_GenTimesBox.Rows[0]["GenTimesBoxNoStatus"])))
            //{
            //    alParamsCoupleError.AddRange(new object[]{
            //        "Check.GenTimesBoxNo", objGenTimesBoxNo
            //        , "Check.strGenTimesBoxNoStatusListToCheck", strGenTimesBoxNoStatusListToCheck
            //        , "DB.GenTimesBoxNoStatus", dtDB_Inv_GenTimesBox.Rows[0]["GenTimesBoxNoStatus"]
            //        });
            //    throw CmUtils.CMyException.Raise(
            //        TError.ErridnInventory.Inv_GenTimesBox_CheckDB_GenTimesBoxNoStatusNotMatched
            //        , null
            //        , alParamsCoupleError.ToArray()
            //        );
            //}
        }
        private void Inv_GenTimesBox_GetX(
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
            , string strRt_Cols_Inv_GenTimesBox
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimesBox_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimesBox", strRt_Cols_Inv_GenTimesBox
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Inv_GenTimesBox = (strRt_Cols_Inv_GenTimesBox != null && strRt_Cols_Inv_GenTimesBox.Length > 0);

            //// drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

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
						---- #tbl_Inv_GenTimesBox_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.GenTimesBoxNo
						into #tbl_Inv_GenTimesBox_Filter_Draft
						from Inv_GenTimesBox tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.GenTimesBoxNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_GenTimesBox_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_GenTimesBox_Filter:
						select
							t.*
						into #tbl_Inv_GenTimesBox_Filter
						from #tbl_Inv_GenTimesBox_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_GenTimesBox ------:
						zzB_Select_Inv_GenTimesBox_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_GenTimesBox_Filter_Draft;
						--drop table #tbl_Inv_GenTimesBox_Filter;
					"
                );
            ////
            string zzB_Select_Inv_GenTimesBox_zzE = "-- Nothing.";
            if (bGet_Inv_GenTimesBox)
            {
                #region // bGet_Inv_GenTimesBox:
                zzB_Select_Inv_GenTimesBox_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_GenTimesBox:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_GenTimesBox_Filter t --//[mylock]
	                        inner join Inv_GenTimesBox tpt --//[mylock]
		                        on t.GenTimesBoxNo = tpt.GenTimesBoxNo
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
                        , "Inv_GenTimesBox" // strTableNameDB
                        , "Inv_GenTimesBox." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "zzB_Select_Inv_GenTimesBox_zzE", zzB_Select_Inv_GenTimesBox_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_GenTimesBox)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_GenTimesBox";
            }
            #endregion
        }

        public DataSet Inv_GenTimesBox_Get(
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
            , string strRt_Cols_Inv_GenTimesBox
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimesBox_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimesBox_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimesBox", strRt_Cols_Inv_GenTimesBox
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

                #region // Inv_GenTimesBox_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_GenTimesBox_GetX(
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
                        , strRt_Cols_Inv_GenTimesBox // strRt_Cols_Inv_GenTimesBox
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

        public DataSet WAS_Inv_GenTimesBox_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimesBox objRQ_Inv_GenTimesBox
            ////
            , out RT_Inv_GenTimesBox objRT_Inv_GenTimesBox
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimesBox.Tid;
            objRT_Inv_GenTimesBox = new RT_Inv_GenTimesBox();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimesBox_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimesBox_Get;
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
                List<Inv_GenTimesBox> lst_Inv_GenTimesBox = new List<Inv_GenTimesBox>();
                #endregion

                #region // WAS_Inv_GenTimesBox_Get:
                mdsResult = Inv_GenTimesBox_Get(
                    objRQ_Inv_GenTimesBox.Tid // strTid
                    , objRQ_Inv_GenTimesBox.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesBox.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimesBox.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimesBox.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_GenTimesBox.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_GenTimesBox.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_GenTimesBox.Ft_WhereClause // strFt_WhereClause
                                                           //// Return:
                    , objRQ_Inv_GenTimesBox.Rt_Cols_Inv_GenTimesBox // strRt_Cols_Inv_GenTimesBox
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_GenTimesBox.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_GenTimesBox = mdsResult.Tables["Inv_GenTimesBox"].Copy();
                    lst_Inv_GenTimesBox = TUtils.DataTableCmUtils.ToListof<Inv_GenTimesBox>(dt_Inv_GenTimesBox);
                    objRT_Inv_GenTimesBox.Lst_Inv_GenTimesBox = lst_Inv_GenTimesBox;
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

        private void Inv_GenTimesBox_AddX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objGenTimesBoxNo
            , object objOrgID
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_GenTimesBox_SaveX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objGenTimesBoxNo",objGenTimesBoxNo
                , "objOrgID",objOrgID
                , "objQty",objQty
                , "objRemark", objRemark
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            //alParamsCoupleError.AddRange(new object[]{
            //    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //    });
            #endregion

            #region // Refine and Check Input:
            ////
            string strGenTimesBoxNo = TUtils.CUtils.StdParam(objGenTimesBoxNo);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strRemark = string.Format("{0}", objRemark).Trim();
            Int64 nQty = Convert.ToInt64(objQty);
            ////
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            ////
            DataTable dtDB_Inv_GenTimesBox = null;
            {
                ////
                if (strGenTimesBoxNo == null || strGenTimesBoxNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strGenTimesBoxNo", strGenTimesBoxNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesBox_AddX_InvalidGenTimesBoxNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Inv_GenTimesBox_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strGenTimesBoxNo // objShipNo
                    , "" // strFlagExistToCheck
                    , "" // strTInvoiceStatusListToCheck
                    , out dtDB_Inv_GenTimesBox // dtDB_Inv_GenTimesBox
                    );
                /////
                if(nQty > TConst.BizMix.nQtyMaxGen)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.nQty", nQty
                        , "Check.nQtyMaxGen", TConst.BizMix.nQtyMaxGen
                        , "Check.ConditionalRaise", "nQty > TConst.BizMix.nQtyMaxGen"
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesBox_AddX_InvalidQty
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                };
                ////
                strCreateDTimeUTC = string.IsNullOrEmpty(strCreateDTimeUTC) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTimeUTC;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            #endregion

            #region // Get domain:
            string strConfigName = "";
            {
                ////
                string strGet_ConfigName = CmUtils.StringUtils.Replace(@"
                            select 
                                t.*
                            from Mst_Config  t --//[mylock]
                            where(1=1)
	                            and t.ConfigType = 'BOX'
                            ;
                        ");
                ////
                DataSet dsExec = _cf.db.ExecQuery(strGet_ConfigName);
                ////
                DataTable dtDB_Get_ConfigName = dsExec.Tables[0];
                if (dtDB_Get_ConfigName.Rows.Count > 0)
                {
                    strConfigName = string.Format("{0}", dtDB_Get_ConfigName.Rows[0]["ConfigName"]).Trim();
                }
                ////
                if (string.IsNullOrEmpty(strConfigName))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strConfigName", strConfigName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesBox_Add_InvalidConfigName
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // Get OrgIDSl:
            string strOrgIDSln = "";
            {
                DataTable dtDB_Mst_Org = null;
                Mst_Org_CheckDB(
                    ref alParamsCoupleError
                    , strOrgID
                    , TConst.Flag.Yes
                    , TConst.Flag.Active
                    , out dtDB_Mst_Org
                    );

                if (dtDB_Mst_Org.Rows.Count > 0)
                {
                    strOrgIDSln = string.Format("{0}", dtDB_Mst_Org.Rows[0]["OrgIDSln"]).Trim();
                }
            }
            #endregion

            #region // SaveTemp Inv_GenTimesBox:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Inv_GenTimesBox"
                    , new object[]{
                        "GenTimesBoxNo", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ConfigDomain", TConst.BizMix.Default_DBColType,
                        "Qty", TConst.BizMix.Default_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Max_DBCol,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strGenTimesBoxNo, // TInvoiceCode
                                nNetworkID, // NetworkID
                                strOrgID, // OrgID
                                strConfigName, // ConfigDomain
                                objQty, // Qty
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // Build Inv_InventoryBox:
            DataTable dt_Input_Inv_InventoryBox = new DataTable("Inv_InventoryBox");
            {
                dt_Input_Inv_InventoryBox.Columns.Add("BoxNo", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("NetworkID", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("OrgID", typeof(object));
                //dt_Input_Inv_InventoryBox.Columns.Add("SecretNo", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("QR_BoxNo", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("GenTimesBoxNo", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("FlagMap", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("FlagUsed", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("Remark", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("LogLUDTimeUTC", typeof(object));
                dt_Input_Inv_InventoryBox.Columns.Add("LogLUBy", typeof(object));
                ////
                List<string> lstSeq_ObjCode = new List<string>();

                Seq_GenObjCode_GetX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , nNetworkID.ToString() // nNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                    , strOrgIDSln // strobjOrgID
                    , nQty // nQty
                    , out lstSeq_ObjCode // lstSeq_ObjCode
                    );
                ////
                for (int nScan = 0; nScan < lstSeq_ObjCode.Count; nScan++)
                {
                    ////
                    string strQR_BoxNo = string.Format("{0}{1}", strConfigName, lstSeq_ObjCode[nScan]).Trim();
                    /////
                    dt_Input_Inv_InventoryBox.Rows.Add(
                        lstSeq_ObjCode[nScan]// BoxNo
                        , nNetworkID // NetworkID
                        , strOrgID // MST
                        , strQR_BoxNo // QR_BoxNo
                        , strGenTimesBoxNo // GenTimesBoxNo
                        , 0 // FlagMap 0 Chưa map/1 đã map
                        , 0 // FlagUsed 0 chưa dùng/1 đã dùng
                        , "" // Remark
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // LogLUDTimeUTC
                        , strWAUserCode // LogLUBy
                        );
                    ////

                }
                //

            }
            #endregion 

            #region //// SaveTemp Inv_InventoryBox:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBox" // strTableName
                    , new object[] {
                            "BoxNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "OrgID", TConst.BizMix.Default_DBColType
                            //, "SecretNo", TConst.BizMix.Default_DBColType
                            , "QR_BoxNo", TConst.BizMix.Default_DBColType
                            , "GenTimesBoxNo", TConst.BizMix.Default_DBColType
                            , "FlagMap", TConst.BizMix.Default_DBColType
                            , "FlagUsed", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dt_Input_Inv_InventoryBox // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzzzClauseInsert_Inv_GenTimesBox_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_GenTimesBox:
                                insert into Inv_GenTimesBox
                                (	
	                                GenTimesBoxNo
	                                , NetworkID
	                                , OrgID
	                                , ConfigDomain
	                                , Qty
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.GenTimesBoxNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.ConfigDomain
	                                , t.Qty
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_GenTimesBox t --//[mylock]
                                ;
                            ");

                string zzzzClauseInsert_Inv_InventoryBox_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_InventoryBox:
                                insert into Inv_InventoryBox
                                (	
	                                BoxNo
	                                , NetworkID
	                                , OrgID
	                                , QR_BoxNo
	                                , GenTimesBoxNo
	                                , FlagMap
	                                , FlagUsed
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.BoxNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.QR_BoxNo
	                                , t.GenTimesBoxNo
	                                , t.FlagMap
	                                , t.FlagUsed
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_InventoryBox t --//[mylock]
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Inv_GenTimesBox_zSave			
								----
								zzzzClauseInsert_Inv_InventoryBox_zSave			
								----
							"
                    , "zzzzClauseInsert_Inv_GenTimesBox_zSave", zzzzClauseInsert_Inv_GenTimesBox_zSave
                    , "zzzzClauseInsert_Inv_InventoryBox_zSave", zzzzClauseInsert_Inv_InventoryBox_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_GenTimesBox;
						drop table #input_Inv_InventoryBox;
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

        public DataSet Inv_GenTimesBox_Add(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGenTimesNo
            , object objOrgID
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_GenTimesBox_Add";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimesBox_Add;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objGenTimesNo", objGenTimesNo
                    , "objOrgID", objOrgID
                    , "objQty", objQty
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

                #region // Inv_GenTimesBox_AddX:
                //DataSet dsGetData = null;
                {
                    Inv_GenTimesBox_AddX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objGenTimesNo // objGenTimesNo
                        , objOrgID // objOrgID
                        , objQty // objQty
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

        public DataSet WAS_Inv_GenTimesBox_Add(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimesBox objRQ_Inv_GenTimesBox
            ////
            , out RT_Inv_GenTimesBox objRT_Inv_GenTimesBox
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimesBox.Tid;
            objRT_Inv_GenTimesBox = new RT_Inv_GenTimesBox();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_GenTimesBox.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimesBox_Add";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimesBox_Add;
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
                #endregion

                #region // Inv_GenTimesBox_Save:
                mdsResult = Inv_GenTimesBox_Add(
                    objRQ_Inv_GenTimesBox.Tid // strTid
                    , objRQ_Inv_GenTimesBox.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesBox.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimesBox.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimesBox.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              /////
                    , objRQ_Inv_GenTimesBox.Inv_GenTimesBox.GenTimesBoxNo // GenTimesBoxNo
                    , objRQ_Inv_GenTimesBox.Inv_GenTimesBox.OrgID // OrgID
                    , objRQ_Inv_GenTimesBox.Inv_GenTimesBox.Qty // objQty
                    , objRQ_Inv_GenTimesBox.Inv_GenTimesBox.Remark // objRemark
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

        #region // Inv_GenTimesCarton:
        private void Inv_GenTimesCarton_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objGenTimesCartonNo
            , string strFlagExistToCheck
            , string strGenTimesCartonNoStatusListToCheck
            , out DataTable dtDB_Inv_GenTimesCarton
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Inv_GenTimesCarton t --//[mylock]
					where (1=1)
						and t.GenTimesCartonNo = @objGenTimesCartonNo
					;
				");
            dtDB_Inv_GenTimesCarton = _cf.db.ExecQuery(
                strSqlExec
                , "@objGenTimesCartonNo", objGenTimesCartonNo
                ).Tables[0];
            dtDB_Inv_GenTimesCarton.TableName = "Inv_GenTimesCarton";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Inv_GenTimesCarton.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesCartonNo", objGenTimesCartonNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Inv_GenTimesCarton.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GenTimesCartonNo", objGenTimesCartonNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            //// strFlagActiveListToCheck:
            //if (strGenTimesCartonNoStatusListToCheck.Length > 0 && !strGenTimesCartonNoStatusListToCheck.Contains(Convert.ToString(dtDB_Inv_GenTimesCarton.Rows[0]["GenTimesCartonNoStatus"])))
            //{
            //    alParamsCoupleError.AddRange(new object[]{
            //        "Check.GenTimesCartonNo", objGenTimesCartonNo
            //        , "Check.strGenTimesCartonNoStatusListToCheck", strGenTimesCartonNoStatusListToCheck
            //        , "DB.GenTimesCartonNoStatus", dtDB_Inv_GenTimesCarton.Rows[0]["GenTimesCartonNoStatus"]
            //        });
            //    throw CmUtils.CMyException.Raise(
            //        TError.ErridnInventory.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoStatusNotMatched
            //        , null
            //        , alParamsCoupleError.ToArray()
            //        );
            //}
        }
        private void Inv_GenTimesCarton_GetX(
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
            , string strRt_Cols_Inv_GenTimesCarton
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimesCarton_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimesCarton", strRt_Cols_Inv_GenTimesCarton
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Inv_GenTimesCarton = (strRt_Cols_Inv_GenTimesCarton != null && strRt_Cols_Inv_GenTimesCarton.Length > 0);

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
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_GenTimesCarton_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.GenTimesCartonNo
						into #tbl_Inv_GenTimesCarton_Filter_Draft
						from Inv_GenTimesCarton tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.GenTimesCartonNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_GenTimesCarton_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_GenTimesCarton_Filter:
						select
							t.*
						into #tbl_Inv_GenTimesCarton_Filter
						from #tbl_Inv_GenTimesCarton_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_GenTimesCarton ------:
						zzB_Select_Inv_GenTimesCarton_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_GenTimesCarton_Filter_Draft;
						--drop table #tbl_Inv_GenTimesCarton_Filter;
					"
                );
            ////
            string zzB_Select_Inv_GenTimesCarton_zzE = "-- Nothing.";
            if (bGet_Inv_GenTimesCarton)
            {
                #region // bGet_Inv_GenTimesCarton:
                zzB_Select_Inv_GenTimesCarton_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_GenTimesCarton:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_GenTimesCarton_Filter t --//[mylock]
	                        inner join Inv_GenTimesCarton tpt --//[mylock]
		                        on t.GenTimesCartonNo = tpt.GenTimesCartonNo
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
                        , "Inv_GenTimesCarton" // strTableNameDB
                        , "Inv_GenTimesCarton." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "zzB_Select_Inv_GenTimesCarton_zzE", zzB_Select_Inv_GenTimesCarton_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_GenTimesCarton)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_GenTimesCarton";
            }
            #endregion
        }

        public DataSet Inv_GenTimesCarton_Get(
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
            , string strRt_Cols_Inv_GenTimesCarton
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_GenTimesCarton_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimesCarton_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_GenTimesCarton", strRt_Cols_Inv_GenTimesCarton
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

                #region // Inv_GenTimesCarton_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_GenTimesCarton_GetX(
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
                        , strRt_Cols_Inv_GenTimesCarton // strRt_Cols_Inv_GenTimesCarton
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

        public DataSet WAS_Inv_GenTimesCarton_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton
            ////
            , out RT_Inv_GenTimesCarton objRT_Inv_GenTimesCarton
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimesCarton.Tid;
            objRT_Inv_GenTimesCarton = new RT_Inv_GenTimesCarton();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimesCarton_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimesCarton_Get;
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
                List<Inv_GenTimesCarton> lst_Inv_GenTimesCarton = new List<Inv_GenTimesCarton>();
                #endregion

                #region // WAS_Inv_GenTimesCarton_Get:
                mdsResult = Inv_GenTimesCarton_Get(
                    objRQ_Inv_GenTimesCarton.Tid // strTid
                    , objRQ_Inv_GenTimesCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesCarton.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimesCarton.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimesCarton.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_GenTimesCarton.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_GenTimesCarton.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_GenTimesCarton.Ft_WhereClause // strFt_WhereClause
                                                              //// Return:
                    , objRQ_Inv_GenTimesCarton.Rt_Cols_Inv_GenTimesCarton // strRt_Cols_Inv_GenTimesCarton
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_GenTimesCarton.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_GenTimesCarton = mdsResult.Tables["Inv_GenTimesCarton"].Copy();
                    lst_Inv_GenTimesCarton = TUtils.DataTableCmUtils.ToListof<Inv_GenTimesCarton>(dt_Inv_GenTimesCarton);
                    objRT_Inv_GenTimesCarton.Lst_Inv_GenTimesCarton = lst_Inv_GenTimesCarton;
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

        private void Inv_GenTimesCarton_AddX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objGenTimesCartonNo
            , object objOrgID
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inv_GenTimesCarton_SaveX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objGenTimesCartonNo",objGenTimesCartonNo
                , "objOrgID",objOrgID
                , "objQty",objQty
                , "objRemark", objRemark
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            //alParamsCoupleError.AddRange(new object[]{
            //    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //    });
            #endregion

            #region // Refine and Check Input:
            ////
            string strGenTimesCartonNo = TUtils.CUtils.StdParam(objGenTimesCartonNo);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strRemark = string.Format("{0}", objRemark).Trim();
            Int64 nQty = Convert.ToInt64(objQty);
            ////
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            ////
            DataTable dtDB_Inv_GenTimesCarton = null;
            {
                ////
                if (strGenTimesCartonNo == null || strGenTimesCartonNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strGenTimesCartonNo", strGenTimesCartonNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesCarton_AddX_InvalidGenTimesCartonNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Inv_GenTimesCarton_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strGenTimesCartonNo // objShipNo
                    , "" // strFlagExistToCheck
                    , "" // strTInvoiceStatusListToCheck
                    , out dtDB_Inv_GenTimesCarton // dtDB_Inv_GenTimesCarton
                    );
                ////
                if (nQty > TConst.BizMix.nQtyMaxGen)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.nQty", nQty
                        , "Check.nQtyMaxGen", TConst.BizMix.nQtyMaxGen
                        , "Check.ConditionalRaise", "nQty > TConst.BizMix.nQtyMaxGen"
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesCarton_AddX_InvalidQty
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                };
                ////
                strCreateDTimeUTC = string.IsNullOrEmpty(strCreateDTimeUTC) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTimeUTC;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            #endregion

            #region // Get domain:
            string strConfigName = "";
            {
                ////
                string strGet_ConfigName = CmUtils.StringUtils.Replace(@"
                                select 
                                    t.*
                                from Mst_Config  t --//[mylock]
                                where(1=1)
	                                and t.ConfigType = 'CARTON'
                                ;
                            ");
                ////
                DataSet dsExec = _cf.db.ExecQuery(strGet_ConfigName);
                ////
                DataTable dtDB_Get_ConfigName = dsExec.Tables[0];
                if (dtDB_Get_ConfigName.Rows.Count > 0)
                {
                    strConfigName = string.Format("{0}", dtDB_Get_ConfigName.Rows[0]["ConfigName"]).Trim();
                }
                ////
                if (string.IsNullOrEmpty(strConfigName))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strConfigName", strConfigName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_GenTimesCarton_Add_InvalidConfigName
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
            }
            #endregion

            #region // Get domain:
            string strOrgIDSln = "";
            {
                DataTable dtDB_Mst_Org = null;
                Mst_Org_CheckDB(
                    ref alParamsCoupleError
                    , strOrgID
                    , TConst.Flag.Yes
                    , TConst.Flag.Active
                    , out dtDB_Mst_Org
                    );

                if (dtDB_Mst_Org.Rows.Count > 0)
                {
                    strOrgIDSln = string.Format("{0}", dtDB_Mst_Org.Rows[0]["OrgIDSln"]).Trim();
                }
            }
            #endregion

            #region // SaveTemp Inv_GenTimesCarton:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Inv_GenTimesCarton"
                    , new object[]{
                        "GenTimesCartonNo", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ConfigDomain", TConst.BizMix.Default_DBColType,
                        "Qty", TConst.BizMix.Default_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Max_DBCol,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strGenTimesCartonNo, // TInvoiceCode
                                nNetworkID, // NetworkID
                                strOrgID, // MST
                                strConfigName, // ConfigDomain
                                objQty, // Qty
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // Build Inv_InventoryCarton:
            DataTable dt_Input_Inv_InventoryCarton = new DataTable("Inv_InventoryCarton");
            {
                dt_Input_Inv_InventoryCarton.Columns.Add("CanNo", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("NetworkID", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("OrgID", typeof(object));
                //dt_Input_Inv_InventoryCarton.Columns.Add("SecretNo", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("GenTimesCartonNo", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("QR_CanNo", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("FlagMap", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("FlagPrint", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("Remark", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("LogLUDTimeUTC", typeof(object));
                dt_Input_Inv_InventoryCarton.Columns.Add("LogLUBy", typeof(object));
                ////
                List<string> lstSeq_ObjCode = new List<string>();

                Seq_GenObjCode_GetX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , nNetworkID.ToString() // nNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                    , strOrgIDSln // strobjOrgID
                    , nQty // nQty
                    , out lstSeq_ObjCode // lstSeq_ObjCode
                    );
                ////
                for (int nScan = 0; nScan < lstSeq_ObjCode.Count; nScan++)
                {
                    ////
                    string strQR_CanNo = string.Format("{0}{1}", strConfigName, lstSeq_ObjCode[nScan]).Trim();
                    dt_Input_Inv_InventoryCarton.Rows.Add(
                        lstSeq_ObjCode[nScan]// BoxNo
                        , nNetworkID // NetworkID
                        , strOrgID // MST
                        , strGenTimesCartonNo // GenTimesCartonNo
                        , strQR_CanNo // SecretNo
                        , 0 // FlagMap 0 Chưa map/1 đã map
                        , 0 // FlagUsed 0 chưa dùng/1 đã dùng
                        , "" // Remark
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // LogLUDTimeUTC
                        , strWAUserCode // LogLUBy
                        );
                    ////

                }
                //

            }
            #endregion 

            #region //// SaveTemp Inv_InventoryCarton:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryCarton" // strTableName
                    , new object[] {
                            "CanNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "OrgID", TConst.BizMix.Default_DBColType
                            , "GenTimesCartonNo", TConst.BizMix.Default_DBColType
                            , "QR_CanNo", TConst.BizMix.Default_DBColType
                            , "FlagMap", TConst.BizMix.Default_DBColType
                            , "FlagPrint", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dt_Input_Inv_InventoryCarton // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzzzClauseInsert_Inv_GenTimesCarton_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_GenTimesCarton:
                                insert into Inv_GenTimesCarton
                                (	
	                                GenTimesCartonNo
	                                , NetworkID
	                                , OrgID
	                                , ConfigDomain
	                                , Qty
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.GenTimesCartonNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.ConfigDomain
	                                , t.Qty
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_GenTimesCarton t --//[mylock]
                                ;
                            ");

                string zzzzClauseInsert_Inv_InventoryCarton_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Inv_InventoryCarton:
                                insert into Inv_InventoryCarton
                                (	
	                                CanNo
	                                , NetworkID
	                                , OrgID
	                                , GenTimesCartonNo
	                                , QR_CanNo
	                                , FlagMap
	                                , FlagUsed
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.CanNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.GenTimesCartonNo
	                                , t.QR_CanNo
	                                , t.FlagMap
	                                , t.FlagPrint
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Inv_InventoryCarton t --//[mylock]
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Inv_GenTimesCarton_zSave			
								----
								zzzzClauseInsert_Inv_InventoryCarton_zSave			
								----
							"
                    , "zzzzClauseInsert_Inv_GenTimesCarton_zSave", zzzzClauseInsert_Inv_GenTimesCarton_zSave
                    , "zzzzClauseInsert_Inv_InventoryCarton_zSave", zzzzClauseInsert_Inv_InventoryCarton_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_GenTimesCarton;
						drop table #input_Inv_InventoryCarton;
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

        public DataSet Inv_GenTimesCarton_Add(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGenTimesCartonNo
            , object objOrgID
            , object objQty
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Inv_GenTimesCarton_Add";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_GenTimesCarton_Add;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objGenTimesCartonNo", objGenTimesCartonNo
                    , "objOrgID", objOrgID
                    , "objQty", objQty
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

                #region // Inv_GenTimesCarton_AddX:
                //DataSet dsGetData = null;
                {
                    Inv_GenTimesCarton_AddX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objGenTimesCartonNo // objGenTimesCartonNo
                        , objOrgID // objOrgID
                        , objQty // objQty
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

        public DataSet WAS_Inv_GenTimesCarton_Add(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton
            ////
            , out RT_Inv_GenTimesCarton objRT_Inv_GenTimesCarton
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_GenTimesCarton.Tid;
            objRT_Inv_GenTimesCarton = new RT_Inv_GenTimesCarton();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_GenTimesCarton.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_GenTimesCarton_Add";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_GenTimesCarton_Add;
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
                #endregion

                #region // Inv_GenTimesCarton_Save:
                mdsResult = Inv_GenTimesCarton_Add(
                    objRQ_Inv_GenTimesCarton.Tid // strTid
                    , objRQ_Inv_GenTimesCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesCarton.GwPassword // strGwPassword
                    , objRQ_Inv_GenTimesCarton.WAUserCode // strUserCode
                    , objRQ_Inv_GenTimesCarton.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              /////
                    , objRQ_Inv_GenTimesCarton.Inv_GenTimesCarton.GenTimesCartonNo // objGenTimesCartonNo
                    , objRQ_Inv_GenTimesCarton.Inv_GenTimesCarton.OrgID // OrgID
                    , objRQ_Inv_GenTimesCarton.Inv_GenTimesCarton.Qty // objQty
                    , objRQ_Inv_GenTimesCarton.Inv_GenTimesCarton.Remark // objRemark
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

        #region // Inv_InventoryBox:
        private void Inv_InventoryBox_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string nTop
            ////
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Inv_InventoryBox
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventoryBox_GetTimesNoX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBox_GetTimesNoX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "nTop", nTop // nTop
                , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryBox", strRt_Cols_Inv_InventoryBox
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Inv_InventoryBox = (strRt_Cols_Inv_InventoryBox != null && strRt_Cols_Inv_InventoryBox.Length > 0);
            long nnTop = Convert.ToInt64(nTop);

            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    //, "@nnTop", nnTop
                    });
            ////
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_InventoryBox_Filter_Draft:
						select distinct top @nnTop
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.BoxNo
						into #tbl_Inv_InventoryBox_Filter_Draft
						from Inv_InventoryBox tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.BoxNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventoryBox_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventoryBox_Filter:
						select
							t.*
						into #tbl_Inv_InventoryBox_Filter
						from #tbl_Inv_InventoryBox_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventoryBox ------:
						zzB_Select_Inv_InventoryBox_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventoryBox_Filter_Draft;
						--drop table #tbl_Inv_InventoryBox_Filter;
					"
                );
            ////
            string zzB_Select_Inv_InventoryBox_zzE = "-- Nothing.";
            if (bGet_Inv_InventoryBox)
            {
                #region // bGet_Inv_InventoryBox:
                zzB_Select_Inv_InventoryBox_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBox:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_InventoryBox_Filter t --//[mylock]
	                        inner join Inv_InventoryBox tpt --//[mylock]
		                        on t.BoxNo = tpt.BoxNo
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
                        , "Inv_InventoryBox" // strTableNameDB
                        , "Inv_InventoryBox." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "@nnTop", nnTop
                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                , "zzB_Select_Inv_InventoryBox_zzE", zzB_Select_Inv_InventoryBox_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_InventoryBox)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBox";
            }
            #endregion
        }

        public DataSet Inv_InventoryBox_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string nTop
            ////
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Inv_InventoryBox
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventoryBox_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBox_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "nTop" // nTop
                ////
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryBox", strRt_Cols_Inv_InventoryBox
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

                #region // Inv_InventoryBox_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_InventoryBox_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , nTop // nTop
                               ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Inv_InventoryBox // strRt_Cols_Inv_InventoryBox
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

        public DataSet WAS_Inv_InventoryBox_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBox objRQ_Inv_InventoryBox
            ////
            , out RT_Inv_InventoryBox objRT_Inv_InventoryBox
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBox.Tid;
            objRT_Inv_InventoryBox = new RT_Inv_InventoryBox();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBox_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBox_Get;
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
                List<Inv_InventoryBox> lst_Inv_InventoryBox = new List<Inv_InventoryBox>();
                #endregion

                #region // WAS_Inv_InventoryBox_Get:
                mdsResult = Inv_InventoryBox_Get(
                    objRQ_Inv_InventoryBox.Tid // strTid
                    , objRQ_Inv_InventoryBox.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBox.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryBox.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryBox.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_InventoryBox.nTop // strFt_RecordStart
                    /////
                    , objRQ_Inv_InventoryBox.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_InventoryBox.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_InventoryBox.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Inv_InventoryBox.Rt_Cols_Inv_InventoryBox // strRt_Cols_Inv_InventoryBox
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_InventoryBox.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_InventoryBox = mdsResult.Tables["Inv_InventoryBox"].Copy();
                    lst_Inv_InventoryBox = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBox>(dt_Inv_InventoryBox);
                    objRT_Inv_InventoryBox.Lst_Inv_InventoryBox = lst_Inv_InventoryBox;
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

        private void Inv_InventoryBox_UpdateFlagUsedX(
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
            string strFunctionName = "Inv_InventoryBox_UpdateFlagUsedX";
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

            #region // Refine and Check Invoice_TempGroupField:
            DataTable dtInput_Inv_InventoryBox = null;
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            {
                ////
                string strTableCheck = "Inv_InventoryBox";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryBox = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Inv_InventoryBox.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryBox // dtData
                    , "StdParam", "BoxNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBox, "FlagUsed", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBox, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryBox, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryBox.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryBox.Rows[nScan];
                    ////
                    drScan["FlagUsed"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBox:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBox" // strTableName
                    , new object[] {
                            "BoxNo", TConst.BizMix.Default_DBColType
                            , "FlagUsed", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryBox // dtData
                );
            }
            #endregion

            #region // Check : Serial tồn tại.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                        from #input_Inv_InventoryBox t --//[mylock]
                            left join Inv_InventoryBox f --//[mylock]
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
                        "Check.Serial.NotExist", dtCheck.Rows[0]["BoxNo"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBox_UpdateFlagUsed_InvalidSerial
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // Check : Serial Chưa được sử dụng.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                            , f.FlagUsed
                        from #input_Inv_InventoryBox t --//[mylock]
                            inner join Inv_InventoryBox f --//[mylock]
                                on t.BoxNo = f.BoxNo
                        where(1=1)
                            and f.FlagUsed = '1' -- đã sử dụng
                        ;
                    "
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.Serial", dtCheck.Rows[0]["BoxNo"]
                        , "Check.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                        , "Check.FlagUsed.Expected", TConst.Flag.No
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryBox_UpdateFlagUsed_InvalidFlagUsed
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // SaveDB:
            {
                ////
                string zzzzClauseUpdate_Inv_InventoryBox_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Update:
                                update t 
                                set
                                    t.FlagUsed = f.FlagUsed
                                    , t.LogLUDTimeUTC = f.LogLUDTimeUTC
                                    , t.LogLUBy = f.LogLUBy
                                from Inv_InventoryBox t --//[mylock]
                                    inner join #input_Inv_InventoryBox f --//[mylock]
                                        on t.BoxNo = f.BoxNo
                                where(1=1)
                                ;
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseUpdate_Inv_InventoryBox_zSave			
								----
							"
                    , "zzzzClauseUpdate_Inv_InventoryBox_zSave", zzzzClauseUpdate_Inv_InventoryBox_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventoryBox;
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

        public DataSet Inv_InventoryBox_UpdateFlagUsed(
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
            string strFunctionName = "Inv_InventoryBox_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBox_UpdateFlagUsed;
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

                #region // Inv_InventoryBox_UpdateFlagUsedX:
                //DataSet dsGetData = null;
                {
                    Inv_InventoryBox_UpdateFlagUsedX(
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

        public DataSet WAS_Inv_InventoryBox_UpdateFlagUsed(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryBox objRQ_Inv_InventoryBox
            ////
            , out RT_Inv_InventoryBox objRT_Inv_InventoryBox
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryBox.Tid;
            objRT_Inv_InventoryBox = new RT_Inv_InventoryBox();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryBox_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBox_UpdateFlagUsed;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Inv_InventoryBox", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventoryBox.Lst_Inv_InventoryBox)
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
                    DataTable dt_Invoice_TempCustomField = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBox>(objRQ_Inv_InventoryBox.Lst_Inv_InventoryBox, "Inv_InventoryBox");
                    dsData.Tables.Add(dt_Invoice_TempCustomField);

                }
                #endregion

                #region // Invoice_TempGroup_Create:
                mdsResult = Inv_InventoryBox_UpdateFlagUsed(
                    objRQ_Inv_InventoryBox.Tid // strTid
                    , objRQ_Inv_InventoryBox.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBox.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryBox.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryBox.WAUserPassword // strUserPassword
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
        #endregion

        #region // Inv_InventoryCarton:
        private void Inv_InventoryCarton_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            /////
            , string nTop
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Inv_InventoryCarton
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventoryCarton_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryCarton_GetTimesNoX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryCarton", strRt_Cols_Inv_InventoryCarton
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            long nnTop = Convert.ToInt64(nTop);
            bool bGet_Inv_InventoryCarton = (strRt_Cols_Inv_InventoryCarton != null && strRt_Cols_Inv_InventoryCarton.Length > 0);

            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    //, "@nnTop", nnTop
                    });
            ////
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_InventoryCarton_Filter_Draft:
						select distinct top @nnTop
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.CanNo
						into #tbl_Inv_InventoryCarton_Filter_Draft
						from Inv_InventoryCarton tpt --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on tpt.OrgID = t_MstNNT_View.OrgID
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.CanNo asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventoryCarton_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventoryCarton_Filter:
						select
							t.*
						into #tbl_Inv_InventoryCarton_Filter
						from #tbl_Inv_InventoryCarton_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventoryCarton ------:
						zzB_Select_Inv_InventoryCarton_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventoryCarton_Filter_Draft;
						--drop table #tbl_Inv_InventoryCarton_Filter;
					"
                );
            ////
            string zzB_Select_Inv_InventoryCarton_zzE = "-- Nothing.";
            if (bGet_Inv_InventoryCarton)
            {
                #region // bGet_Inv_InventoryCarton:
                zzB_Select_Inv_InventoryCarton_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryCarton:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Inv_InventoryCarton_Filter t --//[mylock]
	                        inner join Inv_InventoryCarton tpt --//[mylock]
		                        on t.CanNo = tpt.CanNo
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
                        , "Inv_InventoryCarton" // strTableNameDB
                        , "Inv_InventoryCarton." // strPrefixStd
                        , "tpt." // strPrefixAlias
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
                , "@nnTop", nnTop
                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                , "zzB_Select_Inv_InventoryCarton_zzE", zzB_Select_Inv_InventoryCarton_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Inv_InventoryCarton)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryCarton";
            }
            #endregion
        }

        public DataSet Inv_InventoryCarton_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , string nTop
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Inv_InventoryCarton
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Inv_InventoryCarton_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryCarton_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryCarton", strRt_Cols_Inv_InventoryCarton
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

                #region // Inv_InventoryCarton_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Inv_InventoryCarton_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , nTop // nTop
                        ///
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Inv_InventoryCarton // strRt_Cols_Inv_InventoryCarton
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

        public DataSet WAS_Inv_InventoryCarton_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryCarton objRQ_Inv_InventoryCarton
            ////
            , out RT_Inv_InventoryCarton objRT_Inv_InventoryCarton
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryCarton.Tid;
            objRT_Inv_InventoryCarton = new RT_Inv_InventoryCarton();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryCarton_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryCarton_Get;
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
                List<Inv_InventoryCarton> lst_Inv_InventoryCarton = new List<Inv_InventoryCarton>();
                #endregion

                #region // WAS_Inv_InventoryCarton_Get:
                mdsResult = Inv_InventoryCarton_Get(
                    objRQ_Inv_InventoryCarton.Tid // strTid
                    , objRQ_Inv_InventoryCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryCarton.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryCarton.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryCarton.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Inv_InventoryCarton.nTop // strFt_RecordStart
                    /////
                    , objRQ_Inv_InventoryCarton.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Inv_InventoryCarton.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Inv_InventoryCarton.Ft_WhereClause // strFt_WhereClause
                                                               //// Return:
                    , objRQ_Inv_InventoryCarton.Rt_Cols_Inv_InventoryCarton // strRt_Cols_Inv_InventoryCarton
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Inv_InventoryCarton.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Inv_InventoryCarton = mdsResult.Tables["Inv_InventoryCarton"].Copy();
                    lst_Inv_InventoryCarton = TUtils.DataTableCmUtils.ToListof<Inv_InventoryCarton>(dt_Inv_InventoryCarton);
                    objRT_Inv_InventoryCarton.Lst_Inv_InventoryCarton = lst_Inv_InventoryCarton;
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

        private void Inv_InventoryCarton_UpdateFlagUsedX(
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
            string strFunctionName = "Inv_InventoryCarton_UpdateFlagUsedX";
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

            #region // Refine and Check Invoice_TempGroupField:
            DataTable dtInput_Inv_InventoryCarton = null;
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            {
                ////
                string strTableCheck = "Inv_InventoryCarton";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Inv_InventoryCarton = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Inv_InventoryCarton.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Inv_InventoryCarton // dtData
                    , "StdParam", "CanNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryCarton, "FlagUsed", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryCarton, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Inv_InventoryCarton, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Inv_InventoryCarton.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Inv_InventoryCarton.Rows[nScan];
                    ////
                    drScan["FlagUsed"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryCarton:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryCarton" // strTableName
                    , new object[] {
                            "CanNo", TConst.BizMix.Default_DBColType
                            , "FlagUsed", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Inv_InventoryCarton // dtData
                );
            }
            #endregion

            #region // Check : Serial tồn tại.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                        from #input_Inv_InventoryCarton t --//[mylock]
                            left join Inv_InventoryCarton f --//[mylock]
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
                        "Check.Serial.NotExist", dtCheck.Rows[0]["CanNo"]
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryCarton_UpdateFlagUsed_InvalidSerial
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // Check : Serial Chưa được sử dụng.
            {
                string strSql_Check = CmUtils.StringUtils.Replace(@"
                        ---- check:
                        select 
                            t.*
                            , f.FlagUsed
                        from #input_Inv_InventoryCarton t --//[mylock]
                            inner join Inv_InventoryCarton f --//[mylock]
                                on t.CanNo = f.CanNo
                        where(1=1)
                            and f.FlagUsed = '1' -- đã sử dụng
                        ;
                    "
                    );
                //
                DataTable dtCheck = _cf.db.ExecQuery(strSql_Check).Tables[0];
                ////
                if (dtCheck.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.Serial", dtCheck.Rows[0]["CanNo"]
                        , "Check.FlagUsed", dtCheck.Rows[0]["FlagUsed"]
                        , "Check.FlagUsed.Expected", TConst.Flag.No
                        , "Check.RowNumber", dtCheck.Rows.Count
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryCarton_UpdateFlagUsed_InvalidFlagUsed
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion 

            #region // SaveDB:
            {
                ////
                string zzzzClauseUpdate_Inv_InventoryCarton_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Update:
                                update t 
                                set
                                    t.FlagUsed = f.FlagUsed
                                    , t.LogLUDTimeUTC = f.LogLUDTimeUTC
                                    , t.LogLUBy = f.LogLUBy
                                from Inv_InventoryCarton t --//[mylock]
                                    inner join #input_Inv_InventoryCarton f --//[mylock]
                                        on t.CanNo = f.CanNo
                                where(1=1)
                                ;
                                ;
                            ");
                string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseUpdate_Inv_InventoryCarton_zSave			
								----
							"
                    , "zzzzClauseUpdate_Inv_InventoryCarton_zSave", zzzzClauseUpdate_Inv_InventoryCarton_zSave
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            #region //// Clear For Debug:.
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Inv_InventoryCarton;
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

        public DataSet Inv_InventoryCarton_UpdateFlagUsed(
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
            string strFunctionName = "Inv_InventoryCarton_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryCarton_UpdateFlagUsed;
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

                #region // Inv_InventoryCarton_UpdateFlagUsedX:
                //DataSet dsGetData = null;
                {
                    Inv_InventoryCarton_UpdateFlagUsedX(
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

        public DataSet WAS_Inv_InventoryCarton_UpdateFlagUsed(
            ref ArrayList alParamsCoupleError
            , RQ_Inv_InventoryCarton objRQ_Inv_InventoryCarton
            ////
            , out RT_Inv_InventoryCarton objRT_Inv_InventoryCarton
            )
        {
            #region // Temp:
            string strTid = objRQ_Inv_InventoryCarton.Tid;
            objRT_Inv_InventoryCarton = new RT_Inv_InventoryCarton();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Inv_InventoryCarton_UpdateFlagUsed";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryCarton_UpdateFlagUsed;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Inv_InventoryCarton", TJson.JsonConvert.SerializeObject(objRQ_Inv_InventoryCarton.Lst_Inv_InventoryCarton)
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
                    DataTable dt_Invoice_TempCustomField = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryCarton>(objRQ_Inv_InventoryCarton.Lst_Inv_InventoryCarton, "Inv_InventoryCarton");
                    dsData.Tables.Add(dt_Invoice_TempCustomField);

                }
                #endregion

                #region // Invoice_TempGroup_Create:
                mdsResult = Inv_InventoryCarton_UpdateFlagUsed(
                    objRQ_Inv_InventoryCarton.Tid // strTid
                    , objRQ_Inv_InventoryCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryCarton.GwPassword // strGwPassword
                    , objRQ_Inv_InventoryCarton.WAUserCode // strUserCode
                    , objRQ_Inv_InventoryCarton.WAUserPassword // strUserPassword
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
        #endregion
    }
}
