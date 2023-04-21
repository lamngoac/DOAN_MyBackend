using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Xml;
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
using System.Collections;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Temp_PrintTemp:
        private void Temp_PrintTemp_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objPrintTempCode
            , string strFlagExistToCheck
            , string strPrintTempStatusListToCheck
            , out DataTable dtDB_Temp_PrintTemp
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Temp_PrintTemp t --//[mylock]
					where (1=1)
						and t.PrintTempCode = @objPrintTempCode
					;
				");
            dtDB_Temp_PrintTemp = _cf.db.ExecQuery(
                strSqlExec
                , "@objPrintTempCode", objPrintTempCode
                ).Tables[0];
            dtDB_Temp_PrintTemp.TableName = "Temp_PrintTemp";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Temp_PrintTemp.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PrintTempCode", objPrintTempCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Temp_PrintTemp_CheckDB_PrintTempNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Temp_PrintTemp.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.PrintTempCode", objPrintTempCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Temp_PrintTemp_CheckDB_PrintTempExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strPrintTempStatusListToCheck.Length > 0 && !strPrintTempStatusListToCheck.Contains(Convert.ToString(dtDB_Temp_PrintTemp.Rows[0]["PrintTempStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.PrintTempCode", objPrintTempCode
                    , "Check.strPrintTempStatusListToCheck", strPrintTempStatusListToCheck
                    , "DB.PrintTempStatus", dtDB_Temp_PrintTemp.Rows[0]["PrintTempStatus"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Temp_PrintTemp_CheckDB_PrintTempStatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        private void Temp_PrintTemp_GetX(
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
            , string strRt_Cols_Temp_PrintTemp
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Temp_PrintTemp_GetX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Dealer_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Temp_PrintTemp", strRt_Cols_Temp_PrintTemp
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Temp_PrintTemp = (strRt_Cols_Temp_PrintTemp != null && strRt_Cols_Temp_PrintTemp.Length > 0);

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
						---- #tbl_Temp_PrintTemp_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, tpt.PrintTempCode
						into #tbl_Temp_PrintTemp_Filter_Draft
						from Temp_PrintTemp tpt --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							tpt.PrintTempCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Temp_PrintTemp_Filter_Draft t --//[mylock]
						;

						---- #tbl_Temp_PrintTemp_Filter:
						select
							t.*
						into #tbl_Temp_PrintTemp_Filter
						from #tbl_Temp_PrintTemp_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Temp_PrintTemp ------:
						zzB_Select_Temp_PrintTemp_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Temp_PrintTemp_Filter_Draft;
						--drop table #tbl_Temp_PrintTemp_Filter;
					"
                );
            ////
            string zzB_Select_Temp_PrintTemp_zzE = "-- Nothing.";
            if (bGet_Temp_PrintTemp)
            {
                #region // bGet_Temp_PrintTemp:
                zzB_Select_Temp_PrintTemp_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Temp_PrintTemp:
                        select
                            t.MyIdxSeq
	                        , tpt.*
                        from #tbl_Temp_PrintTemp_Filter t --//[mylock]
	                        inner join Temp_PrintTemp tpt --//[mylock]
		                        on t.PrintTempCode = tpt.PrintTempCode
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
                        , "Temp_PrintTemp" // strTableNameDB
                        , "Temp_PrintTemp." // strPrefixStd
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
                , "zzB_Select_Temp_PrintTemp_zzE", zzB_Select_Temp_PrintTemp_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Temp_PrintTemp)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Temp_PrintTemp";
            }
            #endregion
        }

        public DataSet Temp_PrintTemp_Get(
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
            , string strRt_Cols_Temp_PrintTemp
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Temp_PrintTemp_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Temp_PrintTemp_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Temp_PrintTemp", strRt_Cols_Temp_PrintTemp
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

                #region // Temp_PrintTemp_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Temp_PrintTemp_GetX(
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
                        , strRt_Cols_Temp_PrintTemp // strRt_Cols_Temp_PrintTemp
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

        public DataSet WAS_Temp_PrintTemp_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Temp_PrintTemp objRQ_Temp_PrintTemp
            ////
            , out RT_Temp_PrintTemp objRT_Temp_PrintTemp
            )
        {
            #region // Temp:
            string strTid = objRQ_Temp_PrintTemp.Tid;
            objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Temp_PrintTemp_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Temp_PrintTemp_Get;
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
                List<Temp_PrintTemp> lst_Temp_PrintTemp = new List<Temp_PrintTemp>();
                #endregion

                #region // WAS_Temp_PrintTemp_Get:
                mdsResult = Temp_PrintTemp_Get(
                    objRQ_Temp_PrintTemp.Tid // strTid
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    , objRQ_Temp_PrintTemp.WAUserCode // strUserCode
                    , objRQ_Temp_PrintTemp.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Temp_PrintTemp.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Temp_PrintTemp.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Temp_PrintTemp.Ft_WhereClause // strFt_WhereClause
                                                          //// Return:
                    , objRQ_Temp_PrintTemp.Rt_Cols_Temp_PrintTemp // strRt_Cols_Temp_PrintTemp
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Temp_PrintTemp.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Temp_PrintTemp = mdsResult.Tables["Temp_PrintTemp"].Copy();
                    lst_Temp_PrintTemp = TUtils.DataTableCmUtils.ToListof<Temp_PrintTemp>(dt_Temp_PrintTemp);
                    objRT_Temp_PrintTemp.Lst_Temp_PrintTemp_Get = lst_Temp_PrintTemp;
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

        private void Temp_PrintTemp_SaveX(
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
            , object objPrintTempCode
            , object objDLCode
            , object objOrgID
            , object objPrintTempDesc
            , object objPrintTempBody
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Temp_PrintTemp_SaveX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objFlagIsDelete",objFlagIsDelete
				////
                , "objPrintTempCode", objPrintTempCode
                , "objDLCode", objDLCode
                , "objOrgID", objOrgID
                , "objPrintTempDesc", objPrintTempDesc
                , "objPrintTempBody", objPrintTempBody
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
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strPrintTempCode = TUtils.CUtils.StdParam(objPrintTempCode);
            string strDLCode = TUtils.CUtils.StdParam(objDLCode);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strPrintTempDesc = string.Format("{0}", objPrintTempDesc);
            string strPrintTempBody = string.Format("{0}", objPrintTempBody);
            string strRemark = string.Format("{0}", objRemark);
            ////
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            ////
            DataTable dtDB_Temp_PrintTemp = null;
            {
                ////
                if (strPrintTempCode == null || strPrintTempCode.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strPrintTempCode", strPrintTempCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Temp_PrintTemp_SaveX_InvalidPrintTempCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Temp_PrintTemp_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPrintTempCode // objShipNo
                    , "" // strFlagExistToCheck
                    , "" // strTInvoiceStatusListToCheck
                    , out dtDB_Temp_PrintTemp // dtDB_Temp_PrintTemp
                    );
                ////
                if (dtDB_Temp_PrintTemp.Rows.Count < 1) // Chưa tồn tại
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công
                    }
                    else
                    {
                        // Nothing
                    }
                }
                else // Đã tồn tại Temp_PrintTemp:
                {
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_Temp_PrintTemp.Rows[0]["PrintTempStatus"], TConst.TInvoiceStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.PrintTempStatus", dtDB_Temp_PrintTemp.Rows[0]["PrintTempStatus"]
                            , "Check.PrintTempStatus.Expected", TConst.TInvoiceStatus.Pending
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Temp_PrintTemp_SaveX_StatusNotMatched
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    else
                    {
                        strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_Temp_PrintTemp.Rows[0]["CreateDTimeUTC"]);
                        strCreateBy = TUtils.CUtils.StdParam(dtDB_Temp_PrintTemp.Rows[0]["CreateBy"]);
                    }
                }
                strCreateDTimeUTC = string.IsNullOrEmpty(strCreateDTimeUTC) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTimeUTC;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            #endregion

            #region // SaveTemp Invoice_Invoice:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Temp_PrintTemp"
                    , new object[]{
                        "PrintTempCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "DLCode", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "PrintTempDesc", TConst.BizMix.MyText_DBColType,
                        "PrintTempBody", TConst.BizMix.MyText_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "LUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LUBy", TConst.BizMix.Default_DBColType,
                        "ApprDTimeUTC", TConst.BizMix.Default_DBColType,
                        "ApprBy", TConst.BizMix.Default_DBColType,
                        "CancelDTimeUTC", TConst.BizMix.Max_DBCol,
                        "CancelBy", TConst.BizMix.Max_DBCol,
                        "PrintTempStatus", TConst.BizMix.Max_DBCol,
                        "Remark", TConst.BizMix.Max_DBCol,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strPrintTempCode, // TInvoiceCode
                                nNetworkID, // NetworkID
                                strDLCode, // DLCode
                                strOrgID, // OrgID
                                strPrintTempDesc, // PrintTempDesc
                                strPrintTempBody, // PrintTempBody
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                                strWAUserCode, // LUBy
                                null, // ApprDTimeUTC
                                null, // ApprBy
                                null, // CancelDTimeUTC
                                null, // CancelBy
                                TConst.PrintTempStatus.Pending, // PrintTempStatus
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
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
							    ---- Temp_PrintTemp:
							    delete t
							    from Temp_PrintTemp t --//[mylock]
								    inner join #input_Temp_PrintTemp f --//[mylock]
									    on t.PrintTempCode = f.PrintTempCode
							    where (1=1)
							    ;
						");
                    _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {

                        ////
                        string zzzzClauseInsert_Temp_PrintTemp_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Temp_PrintTemp:
                                insert into Temp_PrintTemp
                                (	
	                                PrintTempCode
	                                , NetworkID
	                                , DLCode
	                                , OrgID
	                                , PrintTempDesc
	                                , PrintTempBody
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , CancelDTimeUTC
	                                , CancelBy
	                                , PrintTempStatus
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.PrintTempCode
	                                , t.NetworkID
	                                , t.DLCode
	                                , t.OrgID
	                                , t.PrintTempDesc
	                                , t.PrintTempBody
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.CancelDTimeUTC
	                                , t.CancelBy
	                                , t.PrintTempStatus
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Temp_PrintTemp t --//[mylock]
                                ;
                            ");
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Temp_PrintTemp_zSave
			
								----
							"
                            , "zzzzClauseInsert_Temp_PrintTemp_zSave", zzzzClauseInsert_Temp_PrintTemp_zSave
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
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Temp_PrintTemp;
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
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet Temp_PrintTemp_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            ////
            , object objPrintTempCode
            , object objDLCode
            , object objOrgID
            , object objPrintTempDesc
            , object objPrintTempBody
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Temp_PrintTemp_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Temp_PrintTemp_Save;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
			        ////
                    , "objPrintTempCode", objPrintTempCode
                    , "objDLCode", objDLCode
                    , "objOrgID", objOrgID
                    , "objPrintTempDesc", objPrintTempDesc
                    , "objPrintTempBody", objPrintTempBody
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
                    Temp_PrintTemp_SaveX(
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
                        , objPrintTempCode // objPrintTempCode
                        , objDLCode // objDLCode
                        , objOrgID // objOrgID
                        , objPrintTempDesc // objPrintTempDesc
                        , objPrintTempBody // objPrintTempBody
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

        public DataSet WAS_Temp_PrintTemp_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Temp_PrintTemp objRQ_Temp_PrintTemp
            ////
            , out RT_Temp_PrintTemp objRT_Temp_PrintTemp
            )
        {
            #region // Temp:
            string strTid = objRQ_Temp_PrintTemp.Tid;
            objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Temp_PrintTemp_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Temp_PrintTemp_Save;
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

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Temp_PrintTemp_Save:
                mdsResult = Temp_PrintTemp_Save(
                    objRQ_Temp_PrintTemp.Tid // strTid
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    , objRQ_Temp_PrintTemp.WAUserCode // strUserCode
                    , objRQ_Temp_PrintTemp.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Temp_PrintTemp.FlagIsDelete // objFlagIsDelete
                                                        /////
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.PrintTempCode // objPrintTempCode
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.DLCode // objDLCode
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.OrgID // objOrgID
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.PrintTempDesc // objPrintTempDesc
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.PrintTempBody // objPrintTempBody
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.Remark // objRemark
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

        private void Temp_PrintTemp_ApprovedX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objPrintTempCode
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Temp_PrintTemp_ApprovedX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                //, "objFlagIsDelete",objFlagIsDelete
				////
                , "objPrintTempCode", objPrintTempCode
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
            //bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strPrintTempCode = TUtils.CUtils.StdParam(objPrintTempCode);
            string strRemark = TUtils.CUtils.StdParam(objRemark);
            DataTable dtDB_Temp_PrintTemp = null;
            {
                ////
                Temp_PrintTemp_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPrintTempCode // objPrintTempCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.PrintTempStatus.Pending // strStatusListToCheck
                    , out dtDB_Temp_PrintTemp // dtDB_Temp_PrintTemp
                    );
                ////
            }
            #endregion

            #region // SaveTemp Temp_PrintTemp:
            ////
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Temp_PrintTemp"
                    , new object[]{
                        "PrintTempCode", TConst.BizMix.Default_DBColType,
                        "LUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LUBy", TConst.BizMix.Default_DBColType,
                        "ApprDTimeUTC", TConst.BizMix.Default_DBColType,
                        "ApprBy", TConst.BizMix.Default_DBColType,
                        "PrintTempStatus", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                        new object[]{
                            strPrintTempCode, // TInvoiceCode
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                            strWAUserCode, // LUBy
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                            strWAUserCode, // ApprBy
                            TConst.PrintTempStatus.Approve, // PrintTempStatus
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
                string zzB_Update_Temp_PrintTemp_ClauseSet_zzE = @"
								t.LogLUDTimeUTC = f.LogLUDTimeUTC
								, t.LogLUBy = f.LogLUBy
								, t.Remark = f.Remark
								, t.LUDTimeUTC = f.LUDTimeUTC
								, t.LUBy = f.LUBy
								, t.ApprDTimeUTC = f.ApprDTimeUTC
								, t.ApprBy = f.ApprBy
								, t.PrintTempStatus = f.PrintTempStatus
								";
                ////
                string zzB_Update_Temp_PrintTemp_zzE = CmUtils.StringUtils.Replace(@"
							---- Temp_PrintTemp:
							update t
							set 
								zzB_Update_Temp_PrintTemp_ClauseSet_zzE
							from Temp_PrintTemp t --//[mylock]
                                inner join #input_Temp_PrintTemp f --//[mylock]
                                    on t.PrintTempCode = f.PrintTempCode
							where (1=1)
							;
						"
                    , "zzB_Update_Temp_PrintTemp_ClauseSet_zzE", zzB_Update_Temp_PrintTemp_ClauseSet_zzE
                    );
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_Temp_PrintTemp_zzE
			
						----
						"
                    , "zzB_Update_Temp_PrintTemp_zzE", zzB_Update_Temp_PrintTemp_zzE
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            // Return Good:
            // MyCodeLabel_Done:
            // return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet Temp_PrintTemp_Approved(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPrintTempCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Temp_PrintTemp_Approved";
            string strErrorCodeDefault = TError.ErridnInventory.Temp_PrintTemp_Approved;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objPrintTempCode", objPrintTempCode
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

                #region // Temp_PrintTemp_ApprX:
                //DataSet dsGetData = null;
                {
                    Temp_PrintTemp_ApprovedX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPrintTempCode // objPrintTempCode
                        , objRemark //  objRemark
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

        public DataSet WAS_Temp_PrintTemp_Approved(
           ref ArrayList alParamsCoupleError
           , RQ_Temp_PrintTemp objRQ_Temp_PrintTemp
           ////
           , out RT_Temp_PrintTemp objRT_Temp_PrintTemp
           )
        {
            #region // Temp:
            string strTid = objRQ_Temp_PrintTemp.Tid;
            objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Temp_PrintTemp_Approved";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Temp_PrintTemp_Approved;
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
                //List<Temp_PrintTemp> lst_Temp_PrintTemp = new List<Temp_PrintTemp>();
                //DataSet dsData = new DataSet();
                //{
                //    ////
                //    DataTable dt_Transaction_KeKhaiThue = TUtils.DataTableCmUtils.ToDataTable<Transaction_KeKhaiThue>(objRQ_Mst_NNT.Lst_Transaction_KeKhaiThue, "Transaction_KeKhaiThue");
                //    dsData.Tables.Add(dt_Transaction_KeKhaiThue);
                //}
                #endregion

                #region // Temp_PrintTemp_Cancel:
                mdsResult = Temp_PrintTemp_Approved(
                    objRQ_Temp_PrintTemp.Tid // strTid
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    , objRQ_Temp_PrintTemp.WAUserCode // strUserCode
                    , objRQ_Temp_PrintTemp.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.PrintTempCode // objPrintTempCode
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.Remark // objRemark
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
        private void Temp_PrintTemp_CancelX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objPrintTempCode
            , object objRemark
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Temp_PrintTemp_CacncelX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                //, "objFlagIsDelete",objFlagIsDelete
				////
                , "objPrintTempCode", objPrintTempCode
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
            //bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strPrintTempCode = TUtils.CUtils.StdParam(objPrintTempCode);
            string strRemark = TUtils.CUtils.StdParam(objRemark);
            DataTable dtDB_Temp_PrintTemp = null;
            {
                ////
                Temp_PrintTemp_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strPrintTempCode // objPrintTempCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.PrintTempStatus.Approve // strStatusListToCheck
                    , out dtDB_Temp_PrintTemp // dtDB_Temp_PrintTemp
                    );
                ////
            }
            #endregion

            #region // SaveTemp Temp_PrintTemp:
            ////
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Temp_PrintTemp"
                    , new object[]{
                        "PrintTempCode", TConst.BizMix.Default_DBColType,
                        "LUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LUBy", TConst.BizMix.Default_DBColType,
                        "CancelDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CancelBy", TConst.BizMix.Default_DBColType,
                        "PrintTempStatus", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                        new object[]{
                            strPrintTempCode, // TInvoiceCode
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LUDTimeUTC
                            strWAUserCode, // LUBy
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                            strWAUserCode, // ApprBy
                            TConst.PrintTempStatus.Cancel, // PrintTempStatus
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
                string zzB_Update_Temp_PrintTemp_ClauseSet_zzE = @"
								t.LogLUDTimeUTC = f.LogLUDTimeUTC
								, t.LogLUBy = f.LogLUBy
								, t.Remark = f.Remark
								, t.LUDTimeUTC = f.LUDTimeUTC
								, t.LUBy = f.LUBy
								, t.CancelDTimeUTC = f.CancelDTimeUTC
								, t.CancelBy = f.CancelBy
								, t.PrintTempStatus = f.PrintTempStatus
								";
                ////
                string zzB_Update_Temp_PrintTemp_zzE = CmUtils.StringUtils.Replace(@"
							---- Temp_PrintTemp:
							update t
							set 
								zzB_Update_Temp_PrintTemp_ClauseSet_zzE
							from Temp_PrintTemp t --//[mylock]
                                inner join #input_Temp_PrintTemp f --//[mylock]
                                    on t.PrintTempCode = f.PrintTempCode
							where (1=1)
							;
						"
                    , "zzB_Update_Temp_PrintTemp_ClauseSet_zzE", zzB_Update_Temp_PrintTemp_ClauseSet_zzE
                    );
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_Temp_PrintTemp_zzE
			
						----
						"
                    , "zzB_Update_Temp_PrintTemp_zzE", zzB_Update_Temp_PrintTemp_zzE
                    );
                ////
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                ////
            }
            #endregion

            // Return Good:
            // MyCodeLabel_Done:
            // return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet Temp_PrintTemp_Cancel(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPrintTempCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Temp_PrintTemp_Cancel";
            string strErrorCodeDefault = TError.ErridnInventory.Temp_PrintTemp_Cancel;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objPrintTempCode", objPrintTempCode
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

                #region // Temp_PrintTemp_ApprX:
                //DataSet dsGetData = null;
                {
                    Temp_PrintTemp_CancelX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objPrintTempCode // objPrintTempCode
                        , objRemark //  objRemark
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


        public DataSet WAS_Temp_PrintTemp_Cancel(
           ref ArrayList alParamsCoupleError
           , RQ_Temp_PrintTemp objRQ_Temp_PrintTemp
           ////
           , out RT_Temp_PrintTemp objRT_Temp_PrintTemp
           )
        {
            #region // Temp:
            string strTid = objRQ_Temp_PrintTemp.Tid;
            objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Temp_PrintTemp_Cancel";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Temp_PrintTemp_Cancel;
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
                //List<Temp_PrintTemp> lst_Temp_PrintTemp = new List<Temp_PrintTemp>();
                //DataSet dsData = new DataSet();
                //{
                //    ////
                //    DataTable dt_Transaction_KeKhaiThue = TUtils.DataTableCmUtils.ToDataTable<Transaction_KeKhaiThue>(objRQ_Mst_NNT.Lst_Transaction_KeKhaiThue, "Transaction_KeKhaiThue");
                //    dsData.Tables.Add(dt_Transaction_KeKhaiThue);
                //}
                #endregion

                #region // Temp_PrintTemp_Cancel:
                mdsResult = Temp_PrintTemp_Cancel(
                    objRQ_Temp_PrintTemp.Tid // strTid
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    , objRQ_Temp_PrintTemp.WAUserCode // strUserCode
                    , objRQ_Temp_PrintTemp.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.PrintTempCode // objPrintTempCode
                    , objRQ_Temp_PrintTemp.Temp_PrintTemp.Remark // objRemark
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

        #region // InvF_InventoryIn:
        /// <summary>
        /// 20220625. Nâng Version từ WAS_InvF_InventoryIn_SaveAndAppr_New20210410
        /// Rem 1 vài đoạn để tăng tốc cho phần Demo eTEM
        /// </summary>
        public DataSet WAS_InvF_InventoryIn_SaveAndAppr_New20220625(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryIn objRQ_InvF_InventoryIn
            ////
            , out RT_InvF_InventoryIn objRT_InvF_InventoryIn
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryIn.Tid;
            objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryIn_SaveAndAppr";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryIn_SaveAndAppr;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                //, "objRQ_InvF_InventoryIn", TJson.JsonConvert.SerializeObject(objRQ_InvF_InventoryIn)
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
                    if (objRQ_InvF_InventoryIn.Lst_InvF_InventoryInDtl == null)
                        objRQ_InvF_InventoryIn.Lst_InvF_InventoryInDtl = new List<InvF_InventoryInDtl>();
                    {
                        DataTable dt_InvF_InventoryInDtl = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInDtl>(objRQ_InvF_InventoryIn.Lst_InvF_InventoryInDtl, "InvF_InventoryInDtl");
                        dsData.Tables.Add(dt_InvF_InventoryInDtl);
                    }
                    ////
                    if (objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstLot == null)
                        objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstLot = new List<InvF_InventoryInInstLot>();
                    {
                        DataTable dt_InvF_InventoryInInstLot = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInInstLot>(objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstLot, "InvF_InventoryInInstLot");
                        dsData.Tables.Add(dt_InvF_InventoryInInstLot);
                    }
                    ////
                    if (objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial == null)
                        objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial = new List<InvF_InventoryInInstSerial>();
                    {
                        DataTable dt_InvF_InventoryInInstSerial = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInInstSerial>(objRQ_InvF_InventoryIn.Lst_InvF_InventoryInInstSerial, "InvF_InventoryInInstSerial");
                        dsData.Tables.Add(dt_InvF_InventoryInInstSerial);
                    }
                    ////
                    if (objRQ_InvF_InventoryIn.Lst_InvF_InventoryInQR == null)
                        objRQ_InvF_InventoryIn.Lst_InvF_InventoryInQR = new List<InvF_InventoryInQR>();
                    {
                        DataTable dt_InvF_InventoryInQR = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryInQR>(objRQ_InvF_InventoryIn.Lst_InvF_InventoryInQR, "InvF_InventoryInQR");
                        dsData.Tables.Add(dt_InvF_InventoryInQR);
                    }

                }
                #endregion

                #region // InvF_InventoryIn_Save:
                mdsResult = InvF_InventoryIn_SaveAndAppr_New20220625(
                    objRQ_InvF_InventoryIn.Tid // strTid
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryIn.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryIn.WAUserPassword // strUserPassword
                    , objRQ_InvF_InventoryIn.AccessToken // strAccessToken
                    , objRQ_InvF_InventoryIn.NetworkID // strNetworkID
                    , objRQ_InvF_InventoryIn.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              /////
                    , objRQ_InvF_InventoryIn.FlagIsDelete // FlagIsDelete
                    , objRQ_InvF_InventoryIn.FlagIsCheckTotal // FlagIsCheckTotal
                                                              ////
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.IF_InvInNo // IF_InvInNo
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.NetworkID // NetworkID
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.OrgID // OrgID
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.InvInType // InvInType
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.InvCodeIn // InvCodeIn
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.CustomerCode // CustomerCode
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.InvoiceNo // InvoiceNo
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.OrderNo // OrderNo
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.OrderType // OrderType
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.UserDeliver // UserDeliver
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.TotalValIn // TotalValIn
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.TotalValInDesc // TotalValInDesc
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.TotalValInAfterDesc // TotalValInAfterDesc
                    , objRQ_InvF_InventoryIn.InvF_InventoryIn.Remark // Remark
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

        public DataSet InvF_InventoryIn_SaveAndAppr_New20220625(
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
            , object objFlagIsCheckTotal
            //// 
            , object objIF_InvInNo
            , object objNetworkID
            , object objOrgID
            , object objInvInType
            , object objInvCodeIn
            , object objCustomerCode
            , object objInvoiceNo
            , object objOrderNo
            , object objOrderType
            , object objUserDeliver
            , object objTotalValIn
            , object objTotalValInDesc
            , object objTotalValInAfterDesc
            , object objRemark
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryIn_SaveAndAppr";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryIn_SaveAndAppr;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objFlagIsDelete", objFlagIsDelete
                , "objFlagIsCheckTotal", objFlagIsCheckTotal
                //// 
                , "objIF_InvInNo", objIF_InvInNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objInvInType", objInvInType
                , "objInvCodeIn", objInvCodeIn
                , "objCustomerCode", objCustomerCode
                , "objInvoiceNo", objInvoiceNo
                , "objOrderNo", objOrderNo
                , "objOrderType", objOrderType
                , "objUserDeliver", objUserDeliver
                , "objTotalValIn", objTotalValIn
                , "objTotalValInDesc", objTotalValInDesc
                , "objTotalValInAfterDesc", objTotalValInAfterDesc
                , "objRemark", objRemark
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

                #region // InvF_InventoryIn_SaveX:
                DataSet dsGetData = new DataSet();
                {
                    // InvF_InventoryIn_SaveX
                    InvF_InventoryIn_SaveX_New20220625(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , ref alParamsCoupleSW // alParamsCoupleSW
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                        , objFlagIsCheckTotal // objFlagIsCheckTotal
                                              //// 
                        , objIF_InvInNo // objIF_InvInNo
                        , objNetworkID // objNetworkID
                        , objOrgID // objOrgID
                        , objInvInType // objInvInType
                        , objInvCodeIn // objInvCodeIn
                        , null // objIF_InvAudNo
                        , objCustomerCode // objCustomerCode
                        , objInvoiceNo // objInvoiceNo
                        , "" // objOrderNoSys
                        , objOrderNo // objOrderNo
                        , objOrderType // objOrderType
                        , "" // objRefNoSys
                        , objOrderNo // objRefNo
                        , objOrderType // objRefType
                        , objUserDeliver // objUserDeliver
                        , objTotalValIn // objTotalValIn
                        , objTotalValInDesc // objTotalValInDesc
                        , objTotalValInAfterDesc // objTotalValInAfterDesc
                        , objRemark // objRemark
                        , "" // objInvFCFOutCode01
                        , "" // objInvFCFOutCode02
                        , "" // objInvFCFOutCode03
                        , "" // objInvFCFOutCode04
                        , "" // objInvFCFOutCode05
                        , "" // objInvFCFOutCode06
                        , "" // objInvFCFOutCode07
                        , "" // objInvFCFOutCode08
                        , "" // objInvFCFOutCode09
                        , "" // objInvFCFOutCode10
                             /////
                        , dsData // dsData
                        );
                }
                #endregion

                #region // InvF_InventoryIn_ApprX:
                {
                    // InvF_InventoryIn_ApprX
                    InvF_InventoryIn_ApprX_New20210410(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , ref alParamsCoupleSW // alParamsCoupleSW
                        , dtimeSys // dtimeSys
                                   //// 
                        , objIF_InvInNo // objIF_InvInNo
                        , objRemark // objRemarkc
                        , TConst.Flag.No // objFlagInBrand
                        );
                }
                #endregion

                #region // Get Data:
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

        private void InvF_InventoryIn_SaveX_New20220625(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , ref ArrayList alParamsCoupleSW
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            , object objFlagIsCheckTotal
            //// 
            , object objIF_InvInNo
            , object objNetworkID
            , object objOrgID
            , object objInvInType
            , object objInvCodeIn
            , object objIF_InvAudNo
            , object objCustomerCode
            , object objInvoiceNo
            , object objOrderNoSys
            , object objOrderNo
            , object objOrderType
            , object objRefNoSys
            , object objRefNo
            , object objRefType
            , object objUserDeliver
            , object objTotalValIn
            , object objTotalValInDesc
            , object objTotalValInAfterDesc
            , object objRemark
            , object objInvFCFInCode01
            , object objInvFCFInCode02
            , object objInvFCFInCode03
            , object objInvFCFInCode04
            , object objInvFCFInCode05
            , object objInvFCFInCode06
            , object objInvFCFInCode07
            , object objInvFCFInCode08
            , object objInvFCFInCode09
            , object objInvFCFInCode10
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryIn_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objIF_InvInNo", objIF_InvInNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objInvInType", objInvInType
                , "objInvCodeIn", objInvCodeIn
                , "objIF_InvAudNo", objIF_InvAudNo
                , "objCustomerCode", objCustomerCode
                , "objInvoiceNo", objInvoiceNo
                , "objOrderNoSys", objOrderNoSys
                , "objOrderNo", objOrderNo
                , "objOrderType", objOrderType
                , "objRefNoSys", objRefNoSys
                , "objRefNo", objRefNo
                , "objRefType", objRefType
                , "objUserDeliver", objUserDeliver
                , "objTotalValIn", objTotalValIn
                , "objTotalValInDesc", objTotalValInDesc
                , "objTotalValInAfterDesc", objTotalValInAfterDesc
                , "objRemark", objRemark
                , "objInvFCFInCode01", objInvFCFInCode01
                , "objInvFCFInCode02", objInvFCFInCode02
                , "objInvFCFInCode03", objInvFCFInCode03
                , "objInvFCFInCode04", objInvFCFInCode04
                , "objInvFCFInCode05", objInvFCFInCode05
                , "objInvFCFInCode06", objInvFCFInCode06
                , "objInvFCFInCode07", objInvFCFInCode07
                , "objInvFCFInCode08", objInvFCFInCode08
                , "objInvFCFInCode09", objInvFCFInCode09
                , "objInvFCFInCode10", objInvFCFInCode10
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityWriteOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Refine and Check Input InvF_InventoryIn:
            Stopwatch stopWatchFunc1A = new Stopwatch();
            stopWatchFunc1A.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryIn"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            bool bIsCheckTotal = CmUtils.StringUtils.StringEqual(objFlagIsCheckTotal, TConst.Flag.Yes);
            ////
            string strIF_InvInNo = TUtils.CUtils.StdParam(objIF_InvInNo);
            string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strInvInType = TUtils.CUtils.StdParam(objInvInType);
            string strInvCodeIn = TUtils.CUtils.StdParam(objInvCodeIn);
            string strIF_InvAudNo = TUtils.CUtils.StdParam(objIF_InvAudNo);
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
            string strInvoiceNo = TUtils.CUtils.StdParam(objInvoiceNo);
            string strOrderNoSys = TUtils.CUtils.StdParam(objOrderNoSys);
            string strOrderNo = TUtils.CUtils.StdParam(objOrderNo);
            string strOrderType = TUtils.CUtils.StdParam(objOrderType);
            string strRefNoSys = TUtils.CUtils.StdParam(objRefNoSys);
            string strRefNo = TUtils.CUtils.StdParam(objRefNo);
            string strRefType = TUtils.CUtils.StdParam(objRefType);
            string strUserDeliver = string.Format("{0}", objUserDeliver).Trim();
            string strRemark = string.Format("{0}", objRemark).Trim();
            ///////
            string strInvFCFInCode01 = string.Format("{0}", objInvFCFInCode01).Trim();
            string strInvFCFInCode02 = string.Format("{0}", objInvFCFInCode02).Trim();
            string strInvFCFInCode03 = string.Format("{0}", objInvFCFInCode03).Trim();
            string strInvFCFInCode04 = string.Format("{0}", objInvFCFInCode04).Trim();
            string strInvFCFInCode05 = string.Format("{0}", objInvFCFInCode05).Trim();
            string strInvFCFInCode06 = string.Format("{0}", objInvFCFInCode06).Trim();
            string strInvFCFInCode07 = string.Format("{0}", objInvFCFInCode07).Trim();
            string strInvFCFInCode08 = string.Format("{0}", objInvFCFInCode08).Trim();
            string strInvFCFInCode09 = string.Format("{0}", objInvFCFInCode09).Trim();
            string strInvFCFInCode10 = string.Format("{0}", objInvFCFInCode10).Trim();
            ////
            DataTable dtDB_InvF_InventoryIn = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            {
                ////
                if (string.IsNullOrEmpty(strIF_InvInNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvInNo", strIF_InvInNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidIF_InvInNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryIn_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvInNo // objIF_InvInNo
                    , "" // strFlagExistToCheck
                    , "" // strInvoiceStatusListToCheck
                    , out dtDB_InvF_InventoryIn // dtDB_InvF_InventoryIn
                    );
                ////
                if (dtDB_InvF_InventoryIn.Rows.Count < 1) // Chưa Tồn tại.
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
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryIn.Rows[0]["IF_InvInStatus"], TConst.IF_InvInStatus.Pending))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvInStatus", dtDB_InvF_InventoryIn.Rows[0]["IF_InvInStatus"]
                            , "Check.IF_InvInStatus.Expected", TConst.IF_InvInStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryIn.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryIn.Rows[0]["CreateBy"]);
                    ////
                }
                if (!CmUtils.StringUtils.StringEqualIgnoreCase((TUtils.CUtils.StdFlag(drAbilityOfUser["FlagBG"])), TConst.Flag.Active))
                {
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MNNTOrgID"], strOrgID))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.NNT.MNNTOrgID", drAbilityOfUser["MNNTOrgID"]
                            , "Check.strOrgID", strOrgID
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidOrgID
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }

                ////
                DataTable dtDB_Mst_Org = null;

                Mst_Org_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_Org // dtDB_Mst_Org
                    );
                ////
                DataTable dtDB_Mst_InvInType = null;

                Mst_InvInType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , objInvInType // objInvInType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strFlagStatisticListToCheck
                    , out dtDB_Mst_InvInType // dtDB_Mst_InvInType
                    );
                ////
                DataTable dtDB_Mst_Inventory = null;

                Mst_Inventory_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , objInvCodeIn // objInvCodeIn
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , TConst.Flag.Active //strFlagIn_OutListToCheck // 1 : Nhập xuất : 0- Vị trí
                    , out dtDB_Mst_Inventory // dtDB_Mst_Inventory
                    );
                ////
                if (string.IsNullOrEmpty(strCustomerCode))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvInNo", strIF_InvInNo
                        , "Check.strCustomerCode", strCustomerCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidCustomerCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                //////
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(strCustomerCode, strOrgID)
                   && !string.IsNullOrEmpty(strCustomerCode))
                {
                    DataTable dtDB_Mst_Customer = null;
                    //// 
                    Mst_Customer_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objOrgID // objOrgID
                        , strCustomerCode // strCustomerCode
                        , TConst.Flag.Yes // strFlagExistListToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_Customer // dtDB_Mst_Customer
                        );
                }
                ////
                double dblTotalValIn = Convert.ToDouble(objTotalValIn);

                if (dblTotalValIn < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValIn", dblTotalValIn
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                double dblTotalValInDesc = Convert.ToDouble(objTotalValInDesc);

                if (dblTotalValInDesc < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValInDesc", dblTotalValInDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                double dblTotalValInAfterDesc = Convert.ToDouble(objTotalValInAfterDesc);

                if (dblTotalValInAfterDesc < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValInAfterDesc", dblTotalValInAfterDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

                ////
                //if (!string.IsNullOrEmpty(strOrderNo))
                //{
                //    if (string.IsNullOrEmpty(strOrderNoSys))
                //    {
                //        alParamsCoupleError.AddRange(new object[]{
                //            "Check.strIF_InvInNo", strIF_InvInNo
                //            , "Check.strOrderNo", strOrderNo
                //            , "Check.strOrderNoSys", strOrderNoSys
                //            });
                //        throw CmUtils.CMyException.Raise(
                //            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidOrderNoSys
                //            , null
                //            , alParamsCoupleError.ToArray()
                //            );

                //    }
                //    ////
                //    if (string.IsNullOrEmpty(strOrderType))
                //    {
                //        alParamsCoupleError.AddRange(new object[]{
                //            "Check.strIF_InvInNo", strIF_InvInNo
                //            , "Check.strOrderNo", strOrderNo
                //            , "Check.strOrderNoSys", strOrderNoSys
                //            , "Check.strOrderType", strOrderType
                //            });
                //        throw CmUtils.CMyException.Raise(
                //            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidOrderType
                //            , null
                //            , alParamsCoupleError.ToArray()
                //            );

                //    }
                //    ////
                //}
                /////
                if (!string.IsNullOrEmpty(strRefNo))
                {
                    if (string.IsNullOrEmpty(strRefNoSys))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strIF_InvInNo", strIF_InvInNo
                            , "Check.strRefNo", strRefNo
                            , "Check.strRefNoSys", strRefNoSys
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidRefNoSys
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                    if (string.IsNullOrEmpty(strRefType))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strIF_InvInNo", strIF_InvInNo
                            , "Check.strRefNo", strRefNo
                            , "Check.strRefNoSys", strRefNoSys
                            , "Check.stRefType", strRefType
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryIn_SaveX_InvalidRefType
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                }
                /////
            }
            stopWatchFunc1A.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryIn"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1A.ElapsedMilliseconds", stopWatchFunc1A.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveTemp InvF_InventoryIn:
            Stopwatch stopWatchFunc1B = new Stopwatch();
            stopWatchFunc1B.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryIn"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryIn"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvInNo"
                        , "NetworkID"
                        , "OrgID"
                        , "InvInType"
                        , "InvCodeIn"
                        , "IF_InvAudNo"
                        , "CustomerCode"
                        , "InvoiceNo"
                        , "OrderNoSys"
                        , "OrderNo"
                        , "OrderType"
                        , "RefNoSys"
                        , "RefNo"
                        , "RefType"
                        , "UserDeliver"
                        , "TotalValIn"
                        , "TotalValInDesc"
                        , "TotalValInAfterDesc"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "CancelDTimeUTC"
                        , "CancelBy"
                        , "FlagQR"
                        , "IF_InvInStatus"
                        , "Remark"
                        , "InvFCFInCode01"
                        , "InvFCFInCode02"
                        , "InvFCFInCode03"
                        , "InvFCFInCode04"
                        , "InvFCFInCode05"
                        , "InvFCFInCode06"
                        , "InvFCFInCode07"
                        , "InvFCFInCode08"
                        , "InvFCFInCode09"
                        , "InvFCFInCode10"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvInNo, // IF_InvInNo
                                nNetworkID, // NetworkID
								strOrgID, // OrgID
								strInvInType, // InvInType
								strInvCodeIn, // InvCodeIn
								strIF_InvAudNo , // IF_InvAudNo
								strCustomerCode, // CustomerCode
								strInvoiceNo, // InvoiceNo
                                strOrderNoSys, // OrderNoSys
                                strOrderNo, // OrderNo
								strOrderType, // OrderType
                                strRefNoSys, // RefNoSys
                                strRefNo, // RefNo
								strRefType, // RefType
								strUserDeliver, // UserDeliver
								objTotalValIn, // TotalValIn
								objTotalValInDesc, // TotalValInDesc
								objTotalValInAfterDesc, // TotalValInAfterDesc
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // CreateDTimeUTC
                                strWAUserCode, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                null, // ApprDTimeUTC
                                null, // ApprBy
                                null, // CancelDTimeUTC
                                null, // CancelBy
                                TConst.Flag.Inactive,  // FlagQR
                                TConst.IF_InvInStatus.Pending, // IF_InvInStatus
                                strRemark, // Remark
                                strInvFCFInCode01, // InvFCFInCode01
                                strInvFCFInCode02, // InvFCFInCode02
                                strInvFCFInCode03, // InvFCFInCode03
                                strInvFCFInCode04, // InvFCFInCode04
                                strInvFCFInCode05, // InvFCFInCode05
                                strInvFCFInCode06, // InvFCFInCode06
                                strInvFCFInCode07, // InvFCFInCode07
                                strInvFCFInCode08, // InvFCFInCode08
                                strInvFCFInCode09, // InvFCFInCode09
                                strInvFCFInCode10, // InvFCFInCode10
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            stopWatchFunc1B.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryIn"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1B.ElapsedMilliseconds", stopWatchFunc1B.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryInDtl:
            Stopwatch stopWatchFunc1C = new Stopwatch();
            stopWatchFunc1C.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInDtl"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryInDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvFInventoryInDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryInDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InvFInventoryInDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInDtl // dtData
                    , "StdParam", "InvCodeInActual" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "float", "UPIn" // arrstrCouple
                    , "float", "UPInDesc" // arrstrCouple
                    , "float", "ValInAfterDesc" // arrstrCouple
                    , "float", "UnitCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "IF_InvInNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "ValInvIn", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "ValInDesc", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "IF_InvInStatusDtl", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInDtl.Rows[nScan];
                    ////
                    drScan["IF_InvInNo"] = strIF_InvInNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["ValInvIn"] = 0.0;
                    drScan["ValInDesc"] = 0.0;
                    drScan["IF_InvInStatusDtl"] = TConst.IF_InvInStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1C.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInDtl"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1C.ElapsedMilliseconds", stopWatchFunc1C.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryInDtl:
            Stopwatch stopWatchFunc1D = new Stopwatch();
            stopWatchFunc1D.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInDtl"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInDtl" // strTableName
                    , new object[] {
                            "IF_InvInNo", TConst.BizMix.Default_DBColType
                            , "InvCodeInActual", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "UPIn", "float"
                            , "UPInDesc", "float"
                            , "ValInvIn", "float"
                            , "ValInDesc", "float"
                            , "ValInAfterDesc", "float"
                            , "UnitCode", TConst.BizMix.Default_DBColType
                            , "IF_InvInStatusDtl", TConst.BizMix.Default_DBColType
                            , "Remark",  TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInDtl // dtData
                );
            }
            stopWatchFunc1D.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInDtl"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1D.ElapsedMilliseconds", stopWatchFunc1D.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryInInstLot:
            Stopwatch stopWatchFunc1E = new Stopwatch();
            stopWatchFunc1E.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInInstLot"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryInInstLot = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInInstLot";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InventoryInInstLotNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInInstLot = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInInstLot // dtData
                    , "StdParam", "InvCodeInActual" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "ProductLotNo" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "StdDate", "ProductionDate" // arrstrCouple
                    , "StdDate", "ExpiredDate" // arrstrCouple
                    , "", "Remark" // arrstrCouple // 20200722.HTTT.Tạo phiếu nhập kho theo Lô cho lưu ghi chú theo từng Lô(c Đông)
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstLot, "IF_InvInNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstLot, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstLot, "IF_InvInILStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstLot, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstLot, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInInstLot.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInInstLot.Rows[nScan];
                    ////
                    drScan["IF_InvInNo"] = strIF_InvInNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvInILStatus"] = TConst.IF_InvInStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1E.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInInstLot"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1E.ElapsedMilliseconds", stopWatchFunc1E.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryInInstLot:
            Stopwatch stopWatchFunc1F = new Stopwatch();
            stopWatchFunc1F.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInInstLot"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInInstLot" // strTableName
                    , new object[] {
                            "IF_InvInNo", TConst.BizMix.Default_DBColType
                            , "InvCodeInActual", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "ProductLotNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "ProductionDate", TConst.BizMix.Default_DBColType
                            , "ExpiredDate", TConst.BizMix.Default_DBColType
                            , "ValDateExpired", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "IF_InvInILStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInInstLot // dtData
                );
            }
            stopWatchFunc1F.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInInstLot"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1F.ElapsedMilliseconds", stopWatchFunc1F.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryInInstSerial:
            Stopwatch stopWatchFunc1G = new Stopwatch();
            stopWatchFunc1G.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInInstSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryInInstSerial = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InventoryInInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInInstSerial // dtData
                    , "StdParam", "InvCodeInActual" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstSerial, "IF_InvInNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstSerial, "IF_InvInISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInInstSerial.Rows[nScan];
                    ////
                    drScan["IF_InvInNo"] = strIF_InvInNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvInISStatus"] = TConst.IF_InvInStatus.Pending;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1G.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInInstSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1G.ElapsedMilliseconds", stopWatchFunc1G.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryInInstSerial:
            Stopwatch stopWatchFunc1H = new Stopwatch();
            stopWatchFunc1H.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInInstSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInInstSerial" // strTableName
                    , new object[] {
                            "IF_InvInNo", TConst.BizMix.Default_DBColType
                            , "InvCodeInActual", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "IF_InvInISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInInstSerial // dtData
                );
            }
            stopWatchFunc1H.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInInstSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1H.ElapsedMilliseconds", stopWatchFunc1H.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryInQR:
            Stopwatch stopWatchFunc1I = new Stopwatch();
            stopWatchFunc1I.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInQR"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryInQR = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryInQR";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryIn_SaveX_InventoryInQRNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryInQR = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryInQR // dtData
                                               //, "StdParam", "IF_InvInNo" // arrstrCouple
                    , "StdParam", "QRCode" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "BoxNo" // arrstrCouple
                    , "StdParam", "CanNo" // arrstrCouple
                    , "StdParam", "ProductLotNo" // arrstrCouple
                    , "StdParam", "ShiftInCode" // arrstrCouple
                    , "", "UserKCS" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInQR, "IF_InvInNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInQR, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInQR, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryInQR, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryInQR.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryInQR.Rows[nScan];
                    ////
                    drScan["IF_InvInNo"] = strIF_InvInNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1I.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryInQR"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1I.ElapsedMilliseconds", stopWatchFunc1I.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryInQR:
            Stopwatch stopWatchFunc1K = new Stopwatch();
            stopWatchFunc1K.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInQR"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryInQR" // strTableName
                    , new object[] {
                            "IF_InvInNo", TConst.BizMix.Default_DBColType
                            , "QRCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "BoxNo", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "ProductLotNo", TConst.BizMix.Default_DBColType
                            , "ShiftInCode", TConst.BizMix.Default_DBColType
                            , "UserKCS", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryInQR // dtData
                );
            }
            stopWatchFunc1K.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryInQR"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1K.ElapsedMilliseconds", stopWatchFunc1K.ElapsedMilliseconds
                    });
            #endregion

            #region // Check all Conditional Input: // 20220625. HuongTa: Tạm rem lại để tăng tốc độ cho phần Demo.
            Stopwatch stopWatchFunc1L = new Stopwatch();
            stopWatchFunc1L.Start();
            alParamsCoupleSW.AddRange(new object[]{
                "strFunctionName", "Check all Conditional Input"
                , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
       //     if (!bIsDelete)
       //     {
       //         #region // InvF_InventoryInDtl:
       //         {
       //             #region // ProductCode: Phải Tồn tại và Active
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- #input_InvF_InventoryInDtl_DistinctProductCode:
       //                         select distinct
	      //                          f.OrgID
	      //                          , t.ProductCode
       //                         --into #input_InvF_InventoryInDtl_DistinctProductCode
       //                         from #input_InvF_InventoryInDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         ;

							//");
       //                 ////
       //                 DataTable dtInput_Mst_Product = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 Mst_Product_CheckListDB(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , dtInput_Mst_Product // dtInput_Mst_Product
       //                     , TConst.Flag.Yes // strFlagExistToCheck
       //                     , TConst.Flag.Active // strFlagActiveListToCheck
       //                     , "PRODUCT" // strProductTypeToCheck
       //                     );
       //                 /////

       //             }
       //             #endregion 

       //             #region // InvCodeInActual: Phải Tồn tại , Active và là phải là cờ vị trí.
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- #input_InvF_InventoryInDtl_DistinctInvCodeInActual:
       //                         select 
	      //                          f.OrgID
	      //                          , t.InvCodeInActual InvCode
       //                         --into #input_InvF_InventoryInDtl_DistinctInvCodeInActual
       //                         from #input_InvF_InventoryInDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         where(1=1)
       //                         ;

							//");

       //                 DataTable dtInput_Mst_Inventory = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];

       //                 Mst_Inventory_CheckListDB(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , dtInput_Mst_Inventory // dtInput_Mst_Inventory
       //                     , TConst.Flag.Yes  //strFlagExistToCheck
       //                     , TConst.Flag.Active // strFlagActiveListToCheck
       //                     , "" // strFlagIn_OutListToCheck 
       //                     );

       //             }
       //             #endregion

       //             #region // UnitCode  phải khớp với UnitCode trong ProductCode:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                              
       //                         ---- #input_InvF_InventoryInDtl_DistinctProductCode:
       //                         select 
	      //                          f.OrgID
	      //                          , t.ProductCode
	      //                          , t.UnitCode
       //                         --into #input_InvF_InventoryInDtl_DistinctProductCode
       //                         from #input_InvF_InventoryInDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         where(1=1)
       //                         ;

							//");

       //                 ////
       //                 DataTable dt_Input_Mst_Product = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];

       //                 myCheck_Mst_Product_UnitCode(
       //                     ref alParamsCoupleError
       //                     , dt_Input_Mst_Product
       //                     );
       //                 /////
       //             }
       //             #endregion

       //             #region // Check Qty và UP, UPDesc, ValInAfterDesc:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- Check:
       //                         select 
							//		t.IF_InvInNo
							//		, t.InvCodeInActual
							//		, t.ProductCode
							//		, mp.ProductCodeUser mp_ProductCodeUser
							//		, t.Qty
							//		, t.UPIn
							//		, t.UPInDesc
							//		, t.ValInAfterDesc
       //                         from #input_InvF_InventoryInDtl t --//[mylock]
							//		inner join InvF_InventoryIn ifii --//[mylock]
							//			on t.IF_InvInNo = ifii.IF_InvInNo
							//		left join Mst_Product mp --//[mylock]
							//			on ifii.OrgID = mp.OrgID
							//				and t.ProductCode = mp.ProductCode
       //                         where(1=1)
							//		and (t.Qty < 0 or t.UPIn < 0 or t.UPInDesc < 0 or t.ValInAfterDesc < 0)
       //                         ;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.IF_InvInNo", dtDB_Check.Rows[0]["IF_InvInNo"]
       //                         , "Check.InvCodeInActual", dtDB_Check.Rows[0]["InvCodeInActual"]
       //                         , "Check.ProductCode", dtDB_Check.Rows[0]["ProductCode"]
       //                         , "Check.mp_ProductCodeUser", dtDB_Check.Rows[0]["mp_ProductCodeUser"]
       //                         , "Check.Qty", dtDB_Check.Rows[0]["Qty"]
       //                         , "Check.UPIn", dtDB_Check.Rows[0]["UPIn"]
       //                         , "Check.UPInDesc", dtDB_Check.Rows[0]["UPInDesc"]
       //                         , "Check.ValInAfterDesc", dtDB_Check.Rows[0]["ValInAfterDesc"]
       //                         , "Check.ConditionRaiseError", "and (t.Qty < 0 or t.UPIn < 0 or t.UPInDesc < 0 or t.ValInAfterDesc < 0)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryIn_SaveX_InvF_InventoryInDtlTbl_UnitCodeNotEqualUnitCodeInMstProduct
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////
       //             }
       //             #endregion
       //         }
       //         #endregion

       //         #region // InvF_InventoryInInstLot:
       //         {
       //             #region // IF_InvInNo + InvCodeInActual + ProductCode phải khớp với key InvF_InventoryInDtl:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                

       //                         ---- #input_InvF_InventoryInInstLot_Distinct_InvCodeInActual_ProductCode:
       //                         select distinct
	      //                          t.IF_InvInNo
	      //                          , t.InvCodeInActual
	      //                          , t.ProductCode
       //                         into #input_InvF_InventoryInInstLot_Distinct_InvCodeInActual_ProductCode
       //                         from #input_InvF_InventoryInInstLot t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         where(1=1)
       //                         ;

       //                         ---- Check:
       //                         select 
	      //                          t_LOT.IF_InvInNo LOT_IF_InvInNo
	      //                          , t_LOT.InvCodeInActual LOT_InvCodeInActual
	      //                          , t_LOT.ProductCode LOT_ProductCode
	      //                          , mp.ProductCodeUser mp_ProductCodeUser
	      //                          ----
	      //                          , f_DTL.IF_InvInNo DTL_IF_InvInNo
	      //                          , f_DTL.InvCodeInActual DTL_InvCodeInActual
	      //                          , f_DTL.ProductCode DTL_ProductCode
       //                         from #input_InvF_InventoryInInstLot_Distinct_InvCodeInActual_ProductCode t_LOT --//[mylock]
	      //                          left join #input_InvF_InventoryInDtl f_DTL ---//[mylock]
		     //                           on t_LOT.IF_InvInNo = f_DTL.IF_InvInNo
			    //                            and t_LOT.InvCodeInActual = f_DTL.InvCodeInActual
			    //                            and t_LOT.ProductCode = f_DTL.ProductCode
							//		left join InvF_InventoryIn ifii --//[mylock]
							//			on t_LOT.IF_InvInNo = ifii.IF_InvInNo
							//		left join Mst_Product mp --//[mylock]
							//			on ifii.OrgID = mp.OrgID
							//				and t_LOT.ProductCode = mp.ProductCode
       //                         where(1=1)
	      //                          and f_DTL.IF_InvInNo is null
       //                         ;


       //                         --- Clear For Debug:
       //                         drop table #input_InvF_InventoryInInstLot_Distinct_InvCodeInActual_ProductCode;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.LOT_IF_InvInNo", dtDB_Check.Rows[0]["LOT_IF_InvInNo"]
       //                         , "Check.LOT_InvCodeInActual", dtDB_Check.Rows[0]["LOT_InvCodeInActual"]
       //                         , "Check.LOT_ProductCode", dtDB_Check.Rows[0]["LOT_ProductCode"]
       //                         , "Check.mp_ProductCodeUser", dtDB_Check.Rows[0]["mp_ProductCodeUser"]
       //                         , "Check.DTL_IF_InvInNo", dtDB_Check.Rows[0]["DTL_IF_InvInNo"]
       //                         , "Check.DTL_InvCodeInActual", dtDB_Check.Rows[0]["DTL_InvCodeInActual"]
       //                         , "Check.DTL_ProductCode", dtDB_Check.Rows[0]["DTL_ProductCode"]
       //                         , "Check.ConditionRaiseError", "and (f_DTL.IF_InvInNo is null)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryIn_SaveX_InvF_InventoryInInstLotTbl_LotNoExistDtl
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////

       //             }
       //             #endregion

       //         }
       //         #endregion 

       //         #region // InvF_InventoryInInstSerial:
       //         {
       //             #region // IF_InvInNo + InvCodeInActual + ProductCode phải khớp với key InvF_InventoryInDtl:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                 
       //                         ---- #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode:
       //                         select distinct
	      //                          t.IF_InvInNo
	      //                          , t.InvCodeInActual
	      //                          , t.ProductCode
       //                         into #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode
       //                         from #input_InvF_InventoryInInstSerial t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         where(1=1)
       //                         ;

       //                         ---- Check:
       //                         select 
	      //                          t_Serial.IF_InvInNo Serial_IF_InvInNo
	      //                          , t_Serial.InvCodeInActual Serial_InvCodeInActual
	      //                          , t_Serial.ProductCode Serial_ProductCode
							//		, mp.ProductCodeUser mp_ProductCodeUser
	      //                          ----
	      //                          , f_DTL.IF_InvInNo DTL_IF_InvInNo
	      //                          , f_DTL.InvCodeInActual DTL_InvCodeInActual
	      //                          , f_DTL.ProductCode DTL_ProductCode
       //                         from #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode t_Serial --//[mylock]
	      //                          left join #input_InvF_InventoryInDtl f_DTL ---//[mylock]
		     //                           on t_Serial.IF_InvInNo = f_DTL.IF_InvInNo
			    //                            and t_Serial.InvCodeInActual = f_DTL.InvCodeInActual
			    //                            and t_Serial.ProductCode = f_DTL.ProductCode
							//		left join InvF_InventoryIn ifii --//[mylock]
							//			on t_Serial.IF_InvInNo = ifii.IF_InvInNo
							//		left join Mst_Product mp --//[mylock]
							//			on ifii.OrgID = mp.OrgID
							//				and t_Serial.ProductCode = mp.ProductCode
       //                         where(1=1)
	      //                          and f_DTL.IF_InvInNo is null
       //                         ;

       //                         --- Clear For Debug:
       //                         drop table #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.Serial_IF_InvInNo", dtDB_Check.Rows[0]["Serial_IF_InvInNo"]
       //                         , "Check.Serial_InvCodeInActual", dtDB_Check.Rows[0]["Serial_InvCodeInActual"]
       //                         , "Check.Serial_ProductCode", dtDB_Check.Rows[0]["Serial_ProductCode"]
       //                         , "Check.mp_ProductCodeUser", dtDB_Check.Rows[0]["mp_ProductCodeUser"]
       //                         , "Check.DTL_IF_InvInNo", dtDB_Check.Rows[0]["DTL_IF_InvInNo"]
       //                         , "Check.DTL_InvCodeInActual", dtDB_Check.Rows[0]["DTL_InvCodeInActual"]
       //                         , "Check.DTL_ProductCode", dtDB_Check.Rows[0]["DTL_ProductCode"]
       //                         , "Check.ConditionRaiseError", "and (f_DTL.IF_InvInNo is null)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryIn_SaveX_InvF_InventoryInInstSerialTbl_SerialNoExistDtl
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////

       //             }
       //             #endregion

       //             #region // Serial không tồn tại trong kho:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"  
       //                         ---- #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode:
       //                         select distinct 
	      //                          f.OrgID
	      //                          , t.SerialNo
       //                         from #input_InvF_InventoryInInstSerial t --//[mylock]
	      //                          inner join #input_InvF_InventoryIn f --//[mylock]
		     //                           on t.IF_InvInNo = f.IF_InvInNo
       //                         where(1=1)
       //                         ;

							//");
       //                 DataTable dtInput_Inv_InventoryBalanceSerial = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 Inv_InventoryBalanceSerial_CheckListSerialExist(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , TConst.Flag.No // strFlagExistToCheck
       //                     , dtInput_Inv_InventoryBalanceSerial // dtInput_Inv_InventoryBalanceSerial
       //                     );
       //                 //////

       //             }
       //             #endregion

       //         }
       //         #endregion

       //         #region // InvF_InventoryInQR:
       //         {
       //             #region // OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn:
       //             {
       //                 DataSet dsData_Inv_InventoryVerifiedID = new DataSet();
       //                 {
       //                     #region // Refine and check Input:
       //                     string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
       //                     #endregion

       //                     #region // Get AccessToken:
       //                     // Hien tai chua NC SOS nen goi ham de lay AccessToken. Khi nao NC SOS thi bo di.
       //                     //string strAccessToken = null;
       //                     //{
       //                     //    OS_MstSv_Sys_User_GetAccessToken(
       //                     //        strTid // strTid
       //                     //        , strGwUserCode // strGwUserCode
       //                     //        , strGwPassword // strGwPassword
       //                     //        , strWAUserCode // strWAUserCode
       //                     //        , strWAUserPassword // strWAUserPassword
       //                     //        , nNetworkID.ToString() // nNetworkID
       //                     //        , strOrgID // strOrgID
       //                     //                   //, strAccessToken // strAccessToken
       //                     //        , ref alParamsCoupleError // alParamsCoupleError
       //                     //                                  ////
       //                     //        , dtimeSys // dtimeSys
       //                     //                   //, strMST // strMST
       //                     //                   ////
       //                     //        , out strAccessToken
       //                     //        ////
       //                     //        );
       //                     //}
       //                     #endregion

       //                     #region // Build Input: 
       //                     {
       //                         string strSql_Check = CmUtils.StringUtils.Replace(@"
       //             	                ---- Check:
       //             	                select 
       //             		                t.QRCode IDNo
       //             		                , '@strOrgID' OrgID
       //             	                from #input_InvF_InventoryInQR t --//[mylock]
       //             	                where(1=1)
       //             	                ;

       //                                 ---- Check:
       //                                 select 
       //                                     t.BoxNo BoxNo
       //                                     , '@strOrgID' OrgID
       //                                 from #input_InvF_InventoryInQR t --//[mylock]
       //                                 where(1=1)
       //                                     and (t.BoxNo is not null or t.BoxNo <> '')
       //                                 ;

       //                                 ---- Check:
       //                                 select 
       //                                     t.CanNo CanNo
       //                                     , '@strOrgID' OrgID
       //                                 from #input_InvF_InventoryInQR t --//[mylock]
       //                                 where(1=1)
       //                                     and (t.CanNo is not null or t.CanNo <> '')
       //                                 ;
       //                             "
       //                             , "@strOrgID", strOrgID
       //                             );
       //                         //
       //                         dsData_Inv_InventoryVerifiedID = _cf.db.ExecQuery(strSql_Check);
       //                         dsData_Inv_InventoryVerifiedID.Tables[0].TableName = "Inv_InventoryGenID";
       //                         dsData_Inv_InventoryVerifiedID.Tables[1].TableName = "Inv_InventoryGenBox";
       //                         dsData_Inv_InventoryVerifiedID.Tables[2].TableName = "Inv_InventoryGenCarton";
       //                         ////
       //                     }
       //                     #endregion

       //                     #region // OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn:
       //                     if (dsData_Inv_InventoryVerifiedID.Tables[0].Rows.Count > 0)
       //                     {
       //                         //OSInBrand_Inv_InventoryGenID_CheckListDB(
       //                         //    strTid // strTid
       //                         //    , strGwUserCode // strGwUserCode
       //                         //    , strGwPassword // strGwPassword
       //                         //    , strWAUserCode // strWAUserCode
       //                         //                    //, strWAUserPassword // strWAUserPassword
       //                         //    , nNetworkID.ToString() // nNetworkID
       //                         //    , strOrgID // strOrgID
       //                         //    , strAccessToken // strAccessToken
       //                         //    , ref alParamsCoupleError // alParamsCoupleError
       //                         //                              ////
       //                         //    , dtimeSys // dtimeSys
       //                         //    , strMST // strMST
       //                         //             ////
       //                         //     , TConst.Flag.Yes // objFlagExistToCheck
       //                         //     , TConst.Flag.No // objFlagMapListToCheck
       //                         //     , TConst.Flag.Yes // objFlagUsedListToCheck
       //                         //                       /////
       //                         //    , dsData_Inv_InventoryVerifiedID
       //                         //    ////
       //                         //    );

       //                     }
       //                     #endregion
       //                 }
       //             }
       //             #endregion
       //         }
       //         #endregion

       //     }
            stopWatchFunc1L.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                "strFunctionName", "Check all Conditional Input"
                , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "stopWatchFunc1L.ElapsedMilliseconds", stopWatchFunc1L.ElapsedMilliseconds
                });
            #endregion

            #region // Inv_InventoryBalanceFormBlock:
            Stopwatch stopWatchFunc1M = new Stopwatch();
            stopWatchFunc1M.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.IF_InvInNo RefNo
	                        , ifii.NetworkID
	                        , 'IN' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );
                // 20200805.HTTT.Trước SaveDB luôn xóa theo phiếu nhập
                //if (!bIsDelete) 
                //{
                //    ////
                //    Inv_InventoryBalanceFormBlock_Delete(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );
                //    /////
                //    Inv_InventoryBalanceFormBlock_Insert(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );

                //}
                //else
                //{
                //    Inv_InventoryBalanceFormBlock_Delete(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );

                //}
            }
            stopWatchFunc1M.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1M.ElapsedMilliseconds", stopWatchFunc1M.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            Stopwatch stopWatchFunc1N = new Stopwatch();
            stopWatchFunc1N.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            stopWatchFunc1N.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1N.ElapsedMilliseconds", stopWatchFunc1N.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveDB:
            Stopwatch stopWatchFunc1O = new Stopwatch();
            stopWatchFunc1O.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInInstSerial:
                            select 
                                t.IF_InvInNo
								, t.InvCodeInActual
                                , t.ProductCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryInInstSerial
                            from InvF_InventoryInInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInInstSerial:
                            delete t 
                            from InvF_InventoryInInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryInInstSerial f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
										and t.InvCodeInActual = f.InvCodeInActual
		                                and t.ProductCode = f.ProductCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;


                            ---- #tbl_InvF_InventoryInInstLot:
                            select 
                                t.IF_InvInNo
								, t.InvCodeInActual
                                , t.ProductCode
								, t.ProductLotNo
                            into #tbl_InvF_InventoryInInstLot
                            from InvF_InventoryInInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInInstLot:
                            delete t 
                            from InvF_InventoryInInstLot t --//[mylock]
	                            inner join #tbl_InvF_InventoryInInstLot f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
										and t.InvCodeInActual = f.InvCodeInActual
		                                and t.ProductCode = f.ProductCode
		                                and t.ProductLotNo = f.ProductLotNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryInDtl:
                            select 
                                t.IF_InvInNo
								, t.InvCodeInActual
                                , t.ProductCode
                            into #tbl_InvF_InventoryInDtl
                            from InvF_InventoryInDtl t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInDtl:
                            delete t 
                            from InvF_InventoryInDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryInDtl f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
		                                and t.InvCodeInActual = f.InvCodeInActual
		                                and t.ProductCode = f.ProductCode
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryInQR:
                            select 
                                t.IF_InvInNo
								, t.QRCode
                            into #tbl_InvF_InventoryInQR
                            from InvF_InventoryInQR t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryInQR:
                            delete t 
                            from InvF_InventoryInQR t --//[mylock]
	                            inner join #tbl_InvF_InventoryInQR f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
		                                and t.QRCode = f.QRCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryIn:
                            delete t
                            from InvF_InventoryIn t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryInInstSerial;
							drop table #tbl_InvF_InventoryInInstLot;
							drop table #tbl_InvF_InventoryInDtl;
							drop table #tbl_InvF_InventoryInQR;
							");
                    DataSet dset = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {
                        ////
                        string zzzzClauseInsert_InvF_InventoryIn_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryIn:                                
                                insert into InvF_InventoryIn(
	                                IF_InvInNo
	                                , NetworkID
	                                , OrgID
	                                , InvInType
	                                , InvCodeIn
	                                , IF_InvAudNo
	                                , CustomerCode
	                                , InvoiceNo
	                                , OrderNoSys
	                                , OrderNo
	                                , OrderType
                                    , RefNoSys
	                                , RefNo
	                                , RefType
	                                , UserDeliver
	                                , TotalValIn
	                                , TotalValInDesc
	                                , TotalValInAfterDesc
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , CancelDTimeUTC
	                                , CancelBy
	                                , FlagQR
	                                , IF_InvInStatus
	                                , Remark
	                                , InvFCFInCode01
	                                , InvFCFInCode02
	                                , InvFCFInCode03
	                                , InvFCFInCode04
	                                , InvFCFInCode05
	                                , InvFCFInCode06
	                                , InvFCFInCode07
	                                , InvFCFInCode08
	                                , InvFCFInCode09
	                                , InvFCFInCode10
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.InvInType
	                                , t.InvCodeIn
	                                , t.IF_InvAudNo
	                                , t.CustomerCode
	                                , t.InvoiceNo
	                                , t.OrderNoSys
	                                , t.OrderNo
	                                , t.OrderType
                                    , t.RefNoSys
                                    , t.RefNo
                                    , t.RefType
	                                , t.UserDeliver
	                                , t.TotalValIn
	                                , t.TotalValInDesc
	                                , t.TotalValInAfterDesc
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.CancelDTimeUTC
	                                , t.CancelBy
	                                , t.FlagQR
	                                , t.IF_InvInStatus
	                                , t.Remark
	                                , t.InvFCFInCode01
	                                , t.InvFCFInCode02
	                                , t.InvFCFInCode03
	                                , t.InvFCFInCode04
	                                , t.InvFCFInCode05
	                                , t.InvFCFInCode06
	                                , t.InvFCFInCode07
	                                , t.InvFCFInCode08
	                                , t.InvFCFInCode09
	                                , t.InvFCFInCode10
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryIn t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryIn:                                
                                insert into InvF_InventoryInDtl(
	                                IF_InvInNo
	                                , InvCodeInActual
	                                , ProductCode
	                                , NetworkID
	                                , Qty
	                                , UPIn
	                                , UPInDesc
	                                , ValInvIn
	                                , ValInDesc
	                                , ValInAfterDesc
	                                , UnitCode
	                                , IF_InvInStatusDtl
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInNo
	                                , t.InvCodeInActual
	                                , t.ProductCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.UPIn
	                                , t.UPInDesc
	                                , t.ValInvIn
	                                , t.ValInDesc
	                                , t.ValInAfterDesc
	                                , t.UnitCode
	                                , t.IF_InvInStatusDtl
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInInstLot_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInInstLot:                                
                                insert into InvF_InventoryInInstLot(
	                                IF_InvInNo
	                                , InvCodeInActual
	                                , ProductCode
	                                , ProductLotNo
	                                , NetworkID
	                                , Qty
	                                , ProductionDate
	                                , ExpiredDate
	                                , ValDateExpired
	                                , Remark
	                                , IF_InvInILStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInNo
	                                , t.InvCodeInActual
	                                , t.ProductCode
	                                , t.ProductLotNo
	                                , t.NetworkID
	                                , t.Qty
	                                , t.ProductionDate
	                                , t.ExpiredDate
	                                , t.ValDateExpired
	                                , t.Remark
	                                , t.IF_InvInILStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInInstLot t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInInstSerial:                                
                                insert into InvF_InventoryInInstSerial(
	                                IF_InvInNo
	                                , InvCodeInActual
	                                , ProductCode
	                                , SerialNo
	                                , NetworkID
	                                , IF_InvInISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInNo
	                                , t.InvCodeInActual
	                                , t.ProductCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.IF_InvInISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInInstSerial t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryInQR_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryInQR:                                
                                insert into InvF_InventoryInQR(
	                                IF_InvInNo
	                                , QRCode
	                                , NetworkID
	                                , ProductCode
	                                , BoxNo
	                                , CanNo
	                                , ProductLotNo
	                                , ShiftInCode
	                                , UserKCS
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvInNo
	                                , t.QRCode
	                                , t.NetworkID
	                                , t.ProductCode
	                                , t.BoxNo
	                                , t.CanNo
	                                , t.ProductLotNo
	                                , t.ShiftInCode
	                                , t.UserKCS
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryInQR t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryIn_zSave				
								----
								zzzzClauseInsert_InvF_InventoryInDtl_zSave	
								----
								zzzzClauseInsert_InvF_InventoryInInstLot_zSave			
								----
								zzzzClauseInsert_InvF_InventoryInInstSerial_zSave			
								----
								zzzzClauseInsert_InvF_InventoryInQR_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryIn_zSave", zzzzClauseInsert_InvF_InventoryIn_zSave
                            , "zzzzClauseInsert_InvF_InventoryInDtl_zSave", zzzzClauseInsert_InvF_InventoryInDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryInInstLot_zSave", zzzzClauseInsert_InvF_InventoryInInstLot_zSave
                            , "zzzzClauseInsert_InvF_InventoryInInstSerial_zSave", zzzzClauseInsert_InvF_InventoryInInstSerial_zSave
                            , "zzzzClauseInsert_InvF_InventoryInQR_zSave", zzzzClauseInsert_InvF_InventoryInQR_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            stopWatchFunc1O.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1O.ElapsedMilliseconds", stopWatchFunc1O.ElapsedMilliseconds
                    });
            #endregion

            #region // Inv_InventoryBalanceFormBlock:
            Stopwatch stopWatchFunc1P = new Stopwatch();
            stopWatchFunc1P.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.IF_InvInNo RefNo
	                        , ifii.NetworkID
	                        , 'IN' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                if (!bIsDelete)
                {
                    //// 20200805.HTTT.Sau SaveDB nếu tạo mới/ sửa thì chỉ insert mới
                    //Inv_InventoryBalanceFormBlock_Delete(
                    //    ref alParamsCoupleError
                    //    , dtimeSys
                    //    , dtDB_Inv_InventoryBalanceFormBlock
                    //    );
                    /////
                    Inv_InventoryBalanceFormBlock_Insert(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
                else
                {
                    Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
            }
            stopWatchFunc1P.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1P.ElapsedMilliseconds", stopWatchFunc1P.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            Stopwatch stopWatchFunc1Q = new Stopwatch();
            stopWatchFunc1Q.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            stopWatchFunc1Q.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1Q.ElapsedMilliseconds", stopWatchFunc1Q.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_InvF_InventoryIn_Total: // 20220625. HuongTa: Rem lại để thực hiện tăng tốc cho Demo eTEM:
            Stopwatch stopWatchFunc1R = new Stopwatch();
            stopWatchFunc1R.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryIn_Total"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            //{
            //    /*
            //        Nâng Cấp: -  Nếu FlagIsCheckInvoiceTotal = 1 hoặc null thì hệ thống sẽ Insert Vào DB mà không tính toán
            //                  -  Ngược lại nếu FlagIsCheckInvoiceTotal = 0 thì hệ thống sẽ tính toán rồi Insert vào DB
            //            FlagIsCheckInvoiceTotal = 0 => Không Check
            //            FlagIsCheckInvoiceTotal = 1 => Check
            //            FlagIsCheckInvoiceTotal = null => Check
            //    */
            //    //if (bIsCheckTotal)
            //    {
            //        myCheck_InvF_InventoryInDtl_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //        ////
            //        myCheck_InvF_InventoryIn_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    }
            //    ////
            //    myCheck_InvF_InventoryIn_TotalQtySerial(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    ////
            //    myCheck_InvF_InventoryIn_TotalQtyLot(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    /////

            //}
            stopWatchFunc1R.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryIn_Total"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1R.ElapsedMilliseconds", stopWatchFunc1R.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Mst_Product_FlagLotOrSerial:
            Stopwatch stopWatchFunc1S = new Stopwatch();
            stopWatchFunc1S.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                ////
                string zzzz_tbl_Mst_Product_Serial = "#tbl_Mst_Product_Serial";
                string zzzz_Mst_Product_LOT = "#tbl_Mst_Product_LOT";
                ////

                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInInstSerial:
                            select distinct
                                k.OrgID
                                , t.ProductCode
                            into zzzz_tbl_Mst_Product_Serial
                            from InvF_InventoryInInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
	                            inner join InvF_InventoryIn k --//[mylock]
		                            on t.IF_InvInNo = k.IF_InvInNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryInInstLot:
                            select 
                                k.OrgID
                                , t.ProductCode
                            into zzzz_Mst_Product_LOT
                            from InvF_InventoryInInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
	                            inner join InvF_InventoryIn k --//[mylock]
		                            on t.IF_InvInNo = k.IF_InvInNo
                            where (1=1)
                            ;

                            --- Return:
                            select t.* from zzzz_tbl_Mst_Product_Serial t --//[mylock]
                            select t.* from zzzz_Mst_Product_LOT t --//[mylock]
							"
                        , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                        , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                        );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
                ////
                myCheck_Mst_Product_FlagLotOrSerial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // alParamsCoupleError
                    , zzzz_tbl_Mst_Product_Serial // zzzz_tbl_Mst_Product_Serial
                    , zzzz_Mst_Product_LOT // zzzz_Mst_Product_LOT
                    );
                /////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzz_tbl_Mst_Product_Serial;
						drop table zzzz_Mst_Product_LOT;
					"
                    , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                    , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                    );

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            stopWatchFunc1S.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1S.ElapsedMilliseconds", stopWatchFunc1S.ElapsedMilliseconds
                    });
            #endregion

            #region // Call Services:
            {
                //    string strSql = CmUtils.StringUtils.Replace(@"
                //            ---- #input_InvF_InventoryProduct:
                //            select distinct
                //             t.ProductCode
                //             , f.NetworkID
                //            into #input_InvF_InventoryProduct
                //            from InvF_InventoryInDtl t --//[mylock]
                //             inner join #input_InvF_InventoryInDtl f --//[mylock]
                //              on t.IF_InvInNo = f.IF_InvInNo
                //            where (1=1)
                //            ;

                //            -- select null #input_InvF_InventoryProduct, *  from #input_InvF_InventoryProduct;

                //            ---- #tbl_Mst_Product :
                //            select
                //             t.OrgID
                //             , t.ProductCode
                //            into #tbl_Mst_Product
                //            from Mst_Product t --//[mylock]
                //             inner join #input_InvF_InventoryProduct f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.NetworkID = f.NetworkID
                //            where (1=1)
                //             and t.DTimeUsed is null
                //            ;

                //            -- select null #tbl_Mst_Product, *  from #tbl_Mst_Product;

                //            ---- Update DTimeUsed:
                //            update t
                //            set
                //                t.DTimeUsed = '@dtimeSys'
                //            from Mst_Product t --//[mylock]
                //                inner join #tbl_Mst_Product f --//[mylock]
                //                    on t.OrgID = f.OrgID
                //                        and t.ProductCode = f.ProductCode
                //            where (1=1)
                //            ;

                //            ---- Mst_Product:
                //            select
                //             t.*
                //            from Mst_Product t --//[mylock]
                //             inner join #tbl_Mst_Product f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;


                //            ---- Mst_ProductFiles:
                //            select
                //             t.*
                //            from Mst_ProductFiles t --//[mylock]
                //             inner join #tbl_Mst_Product f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;


                //            ---- Mst_ProductImages:
                //            select
                //             f.*
                //            from #tbl_Mst_Product t --//[mylock]
                //             inner join Mst_ProductImages f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;

                //            ---- Clear For Debug:
                //drop table #input_InvF_InventoryProduct;
                //drop table #tbl_Mst_Product;
                //        "
                //        , "@dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                //        );

                //    DataSet ds_Product = _cf.db.ExecQuery(strSql);
                //    DataTable dt_Product = ds_Product.Tables[0].Copy();
                //    DataTable dt_ProductFiles = ds_Product.Tables[1].Copy();
                //    DataTable dt_ProductImages = ds_Product.Tables[2].Copy();

                //    List<Mst_Product> lst_Mst_Product = TUtils.DataTableCmUtils.ToListof<Mst_Product>(dt_Product);
                //    List<Mst_ProductFiles> lst_Mst_ProductFiles = TUtils.DataTableCmUtils.ToListof<Mst_ProductFiles>(dt_ProductFiles);
                //    List<Mst_ProductImages> lst_Mst_ProductImages = TUtils.DataTableCmUtils.ToListof<Mst_ProductImages>(dt_ProductImages);

                //    // Login GetUrl
                //    string strUrlMstSv = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_URL].ToString();
                //    string strAPIUrl = null;

                //    if (dt_Product.Rows.Count > 0)
                //    {
                //        RT_MstSv_Sys_User objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
                //        {
                //            RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                //            {
                //                Tid = strTid,
                //                NetworkID = nNetworkID.ToString(),
                //                OrgID = strOrgID,
                //                GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                //                GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                //                WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                //                WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                //            };

                //            objRT_MstSv_Sys_User = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_MstSv_Sys_User_Login(strUrlMstSv, objRQ_MstSv_Sys_User);
                //            strAPIUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                //        }

                //        //string strAPIUrl = CmUtils.CMyDataSet.GetRemark(dt_GetAPi).ToString();
                //        ////
                //        //string Json = TJson.JsonConvert.SerializeObject(dt_GetAPi);
                //        //string JsonUpdate = null;
                //        {
                //            RT_Mst_Product objRT_Mst_Product = new RT_Mst_Product();
                //            {
                //                RQ_Mst_Product objRQ_Mst_Product = new RQ_Mst_Product()
                //                {
                //                    Lst_Mst_Product = lst_Mst_Product,
                //                    Lst_Mst_ProductFiles = lst_Mst_ProductFiles,
                //                    Lst_Mst_ProductImages = lst_Mst_ProductImages,
                //                    Tid = strTid,
                //                    NetworkID = nNetworkID.ToString(),
                //                    OrgID = strOrgID,
                //                    GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                //                    GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                //                    WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                //                    WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                //                    Ft_Cols_Upd = "Mst_Product.DTimeUsed"
                //                };
                //                //JsonUpdate = TJson.JsonConvert.SerializeObject(objRQ_Mst_Product);
                //                objRT_Mst_Product = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_Mst_Product_UpdateMaster(strAPIUrl, objRQ_Mst_Product);

                //            }
                //        }
                //    }
            }
            #endregion

            #region //// Clear For Debug:
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryIn;
						drop table #input_InvF_InventoryInDtl;
						drop table #input_InvF_InventoryInInstSerial;
						drop table #input_InvF_InventoryInInstLot;
						drop table #input_InvF_InventoryInQR;
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
						drop table #input_InvF_InventoryIn;
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

        private void InvF_InventoryIn_ApprX_New20220625(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , ref ArrayList alParamsCoupleSW
            , DateTime dtimeSys
            //// 
            , object objIF_InvInNo
            , object objRemark
            ////
            , object objFlagInBrand
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryIn_ApprX";
            //string strErrorCodeDefault = TError.ErrHTCNM.InvF_InventoryIn_ApprX;
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
            //drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryIn:
            Stopwatch stopWatchFunc2A = new Stopwatch();
            stopWatchFunc2A.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryIn"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            string strIF_InvInNo = TUtils.CUtils.StdParam(objIF_InvInNo);
            string strRemark = string.Format("{0}", objRemark).Trim();
            string strRefNoSys = "";
            string strRefType = "";
            string strNetworkID = "";
            string strOrgID = "";

            bool bIsInBrand = CmUtils.StringUtils.StringEqual(objFlagInBrand, TConst.Flag.Yes);
            ////
            DataTable dtDB_InvF_InventoryIn = null;
            {
                InvF_InventoryIn_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvInNo // objIF_InvInNo
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvInStatus.Pending // strInvInStatusListToCheck
                    , out dtDB_InvF_InventoryIn // dtDB_InvF_InventoryIn
                    );
                ////
                strRefNoSys = TUtils.CUtils.StdStr(dtDB_InvF_InventoryIn.Rows[0]["RefNoSys"]);
                strRefType = TUtils.CUtils.StdStr(dtDB_InvF_InventoryIn.Rows[0]["RefType"]);
                strNetworkID = TUtils.CUtils.StdStr(dtDB_InvF_InventoryIn.Rows[0]["NetworkID"]);
                strOrgID = TUtils.CUtils.StdStr(dtDB_InvF_InventoryIn.Rows[0]["OrgID"]);
            }
            stopWatchFunc2A.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryIn"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2A.ElapsedMilliseconds", stopWatchFunc2A.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveTemp InvF_InventoryIn:
            Stopwatch stopWatchFunc2B = new Stopwatch();
            stopWatchFunc2B.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryIn"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryIn"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvInNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvInStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvInNo, // IF_InvInNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvInStatus.Approve, // IF_InvInStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            stopWatchFunc2B.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryIn"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2B.ElapsedMilliseconds", stopWatchFunc2B.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveDB:
            Stopwatch stopWatchFunc2C = new Stopwatch();
            stopWatchFunc2C.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                ////
                string zzB_Update_InvF_InventoryIn_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.ApprDTimeUTC = f.ApprDTimeUTC
						, t.ApprBy = f.ApprBy
						, t.IF_InvInStatus = f.IF_InvInStatus
						, t.Remark = f.Remark
						";
                ////
                string zzB_Update_InvF_InventoryInDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvInStatusDtl = f.IF_InvInStatus
						";
                ////
                string zzB_Update_InvF_InventoryIn_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryIn:
						update t
						set 
							zzB_Update_InvF_InventoryIn_ClauseSet_zzE
						from InvF_InventoryIn t --//[mylock]
							inner join #input_InvF_InventoryIn f --//[mylock]
								on t.IF_InvInNo = f.IF_InvInNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryIn_ClauseSet_zzE", zzB_Update_InvF_InventoryIn_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryInDtl_zzE = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_InvF_InventoryInDtl_Temp: 
                        select 
                            t.IF_InvInNo
                            , t.InvCodeInActual
                            , t.ProductCode
                            , f.IF_InvInStatus
                            , f.LogLUDTimeUTC
                            , f.LogLUBy
                        into #tbl_InvF_InventoryInDtl_Temp
					    from InvF_InventoryInDtl t --//[mylock]
						    inner join #input_InvF_InventoryIn f --//[mylock]
							    on t.IF_InvInNo = f.IF_InvInNo
					    where (1=1)
					    ;

                        ---- Update:
					    update t
					    set 
						    zzB_Update_InvF_InventoryInDtl_ClauseSet_zzE
					    from InvF_InventoryInDtl t --//[mylock]
						    inner join #tbl_InvF_InventoryInDtl_Temp f --//[mylock]
							    on t.IF_InvInNo = f.IF_InvInNo
								    and t.InvCodeInActual = f.InvCodeInActual
								    and t.ProductCode = f.ProductCode
					    where (1=1)
					    ;
				    "
                , "zzB_Update_InvF_InventoryInDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryInDtl_ClauseSet_zzE
                );
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryIn_zzE
                        ----
						zzB_Update_InvF_InventoryInDtl_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryIn_zzE", zzB_Update_InvF_InventoryIn_zzE
                    , "zzB_Update_InvF_InventoryInDtl_zzE", zzB_Update_InvF_InventoryInDtl_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            stopWatchFunc2C.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2C.ElapsedMilliseconds", stopWatchFunc2C.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_InvF_InventoryIn_Total: // 20220625. HuongTa: Rem thực hiện tăng tốc xử lý cho Demo.
            Stopwatch stopWatchFunc2D = new Stopwatch();
            stopWatchFunc2D.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryIn_Total"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            //{
            //    /*
            //        Nâng Cấp: -  Nếu FlagIsCheckInvoiceTotal = 1 hoặc null thì hệ thống sẽ Insert Vào DB mà không tính toán
            //                  -  Ngược lại nếu FlagIsCheckInvoiceTotal = 0 thì hệ thống sẽ tính toán rồi Insert vào DB
            //            FlagIsCheckInvoiceTotal = 0 => Không Check
            //            FlagIsCheckInvoiceTotal = 1 => Check
            //            FlagIsCheckInvoiceTotal = null => Check
            //    */
            //    //if (bIsCheckTotal)
            //    {
            //        myCheck_InvF_InventoryInDtl_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //        //
            //        myCheck_InvF_InventoryIn_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    }
            //    ////
            //    myCheck_InvF_InventoryIn_TotalQtySerial(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    ////
            //    myCheck_InvF_InventoryIn_TotalQtyLot(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryIn"  //input_InvF_InventoryIn
            //            );
            //    /////

            //}
            stopWatchFunc2D.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryIn_Total"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2D.ElapsedMilliseconds", stopWatchFunc2D.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Mst_Product_FlagLotOrSerial:
            Stopwatch stopWatchFunc2E = new Stopwatch();
            stopWatchFunc2E.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            {
                ////
                string zzzz_tbl_Mst_Product_Serial = "#tbl_Mst_Product_Serial";
                string zzzz_Mst_Product_LOT = "#tbl_Mst_Product_LOT";
                ////

                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryInInstSerial:
                            select distinct
                                k.OrgID
                                , t.ProductCode
                            into zzzz_tbl_Mst_Product_Serial
                            from InvF_InventoryInInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
	                            inner join InvF_InventoryIn k --//[mylock]
		                            on t.IF_InvInNo = k.IF_InvInNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryInInstLot:
                            select 
                                k.OrgID
                                , t.ProductCode
                            into zzzz_Mst_Product_LOT
                            from InvF_InventoryInInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryIn f --//[mylock]
		                            on t.IF_InvInNo = f.IF_InvInNo
	                            inner join InvF_InventoryIn k --//[mylock]
		                            on t.IF_InvInNo = k.IF_InvInNo
                            where (1=1)
                            ;

                            --- Return:
                            select t.* from zzzz_tbl_Mst_Product_Serial t --//[mylock]
                            select t.* from zzzz_Mst_Product_LOT t --//[mylock]
							"
                        , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                        , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                        );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
                ////
                myCheck_Mst_Product_FlagLotOrSerial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // alParamsCoupleError
                    , zzzz_tbl_Mst_Product_Serial // zzzz_tbl_Mst_Product_Serial
                    , zzzz_Mst_Product_LOT // zzzz_Mst_Product_LOT
                    );
                /////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzz_tbl_Mst_Product_Serial;
						drop table zzzz_Mst_Product_LOT;
					"
                    , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                    , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                    );

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            stopWatchFunc2E.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2E.ElapsedMilliseconds", stopWatchFunc2E.ElapsedMilliseconds
                    });
            #endregion

            #region // Serial không tồn tại trong kho:
            Stopwatch stopWatchFunc2F = new Stopwatch();
            stopWatchFunc2F.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Serial không tồn tại trong kho"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                string strSqlCheck = CmUtils.StringUtils.Replace(@"  
                        ---- #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode:
                        select distinct 
	                        ifii.OrgID
	                        , t.SerialNo
                        from InvF_InventoryInInstSerial t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on t.IF_InvInNo = ifii.IF_InvInNo
                        where(1=1)
                        ;

					");
                DataTable dtInput_Inv_InventoryBalanceSerial = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                Inv_InventoryBalanceSerial_CheckListSerialExist(
                    ref alParamsCoupleError // alParamsCoupleError
                    , TConst.Flag.No // strFlagExistToCheck
                    , dtInput_Inv_InventoryBalanceSerial // dtInput_Inv_InventoryBalanceSerial
                    );
                ////

            }
            stopWatchFunc2F.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Serial không tồn tại trong kho"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2F.ElapsedMilliseconds", stopWatchFunc2F.ElapsedMilliseconds
                    });
            #endregion

            #region // InventoryTransaction:
            Stopwatch stopWatchFunc2G = new Stopwatch();
            stopWatchFunc2G.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                InvFInventory_InventoryTransaction_Exec_New20220625(
                    strFunctionName // zzzzClauseCol_FunctionName
                    );
                ////
                Inv_InventoryTransaction_Perform_New20220625(
                    ref alParamsCoupleError // lstParamsCoupleError
                    , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                  //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                    , TConst.InventoryTransactionAction.Add // strInventoryTransactionAction
                    , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                    , strWAUserCode // strCreateBy
                    , 0.0 // dblMinQtyTotalOK
                    , int.MinValue // dblMinQtyBlockOK
                    , int.MinValue // dblMinQtyAvailOK
                    , 0.0 // dblMinQtyTotalNG
                    , int.MinValue // dblMinQtyBlockNG
                    , 0.0 // dblMinQtyAvailNG
                    );
                ////
            }
            stopWatchFunc2G.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2G.ElapsedMilliseconds", stopWatchFunc2G.ElapsedMilliseconds
                    });
            #endregion

            #region // Inv_InventoryBalanceFormBlock:
            Stopwatch stopWatchFunc2H = new Stopwatch();
            stopWatchFunc2H.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            {

                string strSqlCheck = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , ifii.IF_InvInNo RefNo
	                        , ifii.NetworkID
	                        , 'IN' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                {
                    Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
            }
            stopWatchFunc2H.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2H.ElapsedMilliseconds", stopWatchFunc2H.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            Stopwatch stopWatchFunc2I = new Stopwatch();
            stopWatchFunc2I.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn ifii --//[mylock]
		                        on f.IF_InvInNo = ifii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            stopWatchFunc2I.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2I.ElapsedMilliseconds", stopWatchFunc2I.ElapsedMilliseconds
                    });
            #endregion

            #region // InvF_WarehouseCard: // 20220625. HuongTa: Rem lại để tăng tốc độ Demo -> Phải test xem có ảnh hưởng gì đến quá trình vận hành không.
            Stopwatch stopWatchFunc2K = new Stopwatch();
            stopWatchFunc2K.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_WarehouseCard"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //DataTable dtBuild_InvF_WarehouseCard = null;
            //{
            //    string strIF_InvAudNo = TUtils.CUtils.StdParam(dtDB_InvF_InventoryIn.Rows[0]["IF_InvAudNo"]);
            //    string strInventoryAction = TConst.InventoryAction.In;

            //    if (!string.IsNullOrEmpty(strIF_InvAudNo)) strInventoryAction = TConst.InventoryAction.AuditIn;

            //    InvF_InventoryIn_Build_InvF_WarehouseCard(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // alParamsCoupleError
            //        , strInventoryAction // alParamsCoupleError
            //        , dtDB_InvF_InventoryIn // dtDB_InvF_InventoryIn
            //        , out dtBuild_InvF_WarehouseCard // dtBuild_InvF_WarehouseCard
            //        );
            //    /////
            //    InvF_WarehouseCard_Insert(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , dtBuild_InvF_WarehouseCard // dtBuild_InvF_WarehouseCard
            //        );
            //    /////
            //    DataTable dt_Build_InvF_InventoryBalanceCard = null;

            //    Build_InvF_InventoryBalanceCard(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , strIF_InvInNo // strIF_InvInNo
            //        , out dt_Build_InvF_InventoryBalanceCard // dt_Build_InvF_InventoryBalanceCard
            //        );

            //    InvF_InventoryBalanceCard_Insert(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , dt_Build_InvF_InventoryBalanceCard // dt_Build_InvF_InventoryBalanceCard
            //        );
            //}
            stopWatchFunc2K.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_WarehouseCard"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2K.ElapsedMilliseconds", stopWatchFunc2K.ElapsedMilliseconds
                    });
            #endregion

            #region // Inv_InventoryBalance: LastInInvDTimeUTC.
            Stopwatch stopWatchFunc2L = new Stopwatch();
            stopWatchFunc2L.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalance"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                string strSqlCheck = CmUtils.StringUtils.Replace(@"  
                        ---- #tbl_Inv_InventoryBalance_ForUpdate:
                        select distinct
	                        invfii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , invfii.ApprDTimeUTC
                        into #tbl_Inv_InventoryBalance_ForUpdate
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn invfii --//[mylock]
		                        on f.IF_InvInNo = invfii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;

                        ---- #tbl_Inv_InventoryBalanceLot_ForUpdate:
                        select distinct
	                        invfii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.ProductLotNo
	                        , invfii.ApprDTimeUTC
                        into #tbl_Inv_InventoryBalanceLot_ForUpdate
                        from InvF_InventoryInInstLot t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn invfii --//[mylock]
		                        on f.IF_InvInNo = invfii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;


                        ---- #tbl_Inv_InventoryBalanceLot_ForUpdate:
                        select distinct
	                        invfii.OrgID
	                        , t.InvCodeInActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.SerialNo
	                        , invfii.ApprDTimeUTC
                        into #tbl_Inv_InventoryBalanceSerial_ForUpdate
                        from InvF_InventoryInInstSerial t --//[mylock]
	                        inner join #input_InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
	                        inner join InvF_InventoryIn invfii --//[mylock]
		                        on f.IF_InvInNo = invfii.IF_InvInNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;

                        ---- update:
                        ----- Inv_InventoryBalance:
                        update t 
                        set
	                        t.LastInInvDTimeUTC = f.ApprDTimeUTC
	                        , t.LogLUDTimeUTC = k.LogLUDTimeUTC
	                        , t.LogLUBy = k.LogLUBy
                        from Inv_InventoryBalance t --//[mylock]
	                        inner join #tbl_Inv_InventoryBalance_ForUpdate f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.InvCode = f.InvCode
			                        and t.ProductCode = f.ProductCode
	                        inner join #input_InvF_InventoryIn k --//[mylock]
		                        on (1=1)
                        where(1=1)
                        ;

                        ----- Inv_InventoryBalanceLot:
                        update t 
                        set
	                        t.LastInInvDTimeUTC = f.ApprDTimeUTC
	                        , t.LogLUDTimeUTC = k.LogLUDTimeUTC
	                        , t.LogLUBy = k.LogLUBy
                        from Inv_InventoryBalanceLot t --//[mylock]
	                        inner join #tbl_Inv_InventoryBalanceLot_ForUpdate f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.InvCode = f.InvCode
			                        and t.ProductCode = f.ProductCode
			                        and t.ProductLotNo = f.ProductLotNo
	                        inner join #input_InvF_InventoryIn k --//[mylock]
		                        on (1=1)
                        where(1=1)
                        ;

                        ----- Inv_InventoryBalanceLot:
                        update t 
                        set
	                        t.LastInInvDTimeUTC = f.ApprDTimeUTC
	                        , t.LogLUDTimeUTC = k.LogLUDTimeUTC
	                        , t.LogLUBy = k.LogLUBy
                        from Inv_InventoryBalanceSerial t --//[mylock]
	                        inner join #tbl_Inv_InventoryBalanceSerial_ForUpdate f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.InvCode = f.InvCode
			                        and t.ProductCode = f.ProductCode
			                        and t.SerialNo = f.SerialNo
	                        inner join #input_InvF_InventoryIn k --//[mylock]
		                        on (1=1)
                        where(1=1)
                        ;

                        --- Clear For Debug:
                        drop table #tbl_Inv_InventoryBalance_ForUpdate;
                        drop table #tbl_Inv_InventoryBalanceLot_ForUpdate;
                        drop table #tbl_Inv_InventoryBalanceSerial_ForUpdate;

					");
                /////
                _cf.db.ExecQuery(
                    strSqlCheck
                    );
                ////

            }
            stopWatchFunc2L.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalance"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2L.ElapsedMilliseconds", stopWatchFunc2L.ElapsedMilliseconds
                    });
            #endregion

            #region // OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn:
            Stopwatch stopWatchFunc2M = new Stopwatch();
            stopWatchFunc2M.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (bIsInBrand)
            {
                DataSet dsData_Inv_InventoryVerifiedID = new DataSet();
                {
                    #region // Refine and check Input:
                    string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                    //string strOrgID = TUtils.CUtils.StdParam(dtDB_InvF_InventoryIn.Rows[0]["OrgID"]);
                    #endregion

                    #region // Get AccessToken:
                    // Hien tai chua NC SOS nen goi ham de lay AccessToken. Khi nao NC SOS thi bo di.
                    //string strAccessToken = null;
                    //{
                    //    OS_MstSv_Sys_User_GetAccessToken(
                    //        strTid // strTid
                    //        , strGwUserCode // strGwUserCode
                    //        , strGwPassword // strGwPassword
                    //        , strWAUserCode // strWAUserCode
                    //        , strWAUserPassword // strWAUserPassword
                    //        , nNetworkID.ToString() // nNetworkID
                    //        , strOrgID // strOrgID
                    //                   //, strAccessToken // strAccessToken
                    //        , ref alParamsCoupleError // alParamsCoupleError
                    //                                  ////
                    //        , dtimeSys // dtimeSys
                    //                   //, strMST // strMST
                    //                   ////
                    //        , out strAccessToken
                    //        ////
                    //        );
                    //}
                    #endregion

                    #region // Build Input: 
                    {
                        string strSql_Check = CmUtils.StringUtils.Replace(@"
                                --- #tbl_InvF_InventoryOutDtl_Filter:
                                select 
	                                t.QRCode IDNo
	                                , ifii.OrgID
	                                , t.ProductCode
	                                , t.ProductLotNo ProductionLotNo
	                                , ifii.ApprDTimeUTC ProductionDate
	                                , ifii.ApprDTimeUTC PackageDate
	                                , ifii.ApprDTimeUTC PrintDate
	                                , ifii.CustomerCode
	                                , t.ShiftInCode ShiftInCode
	                                , t.UserKCS UserKCS
	                                , t.BoxNo BoxNo
	                                , t.CanNo CanNo
                                from InvF_InventoryInQR t --//[mylock]
	                                inner join #input_InvF_InventoryIn f --//[mylock]
		                                on t.IF_InvInNo = f.IF_InvInNo
	                                inner join InvF_InventoryIn ifii --//[mylock]
		                                on t.IF_InvInNo = ifii.IF_InvInNo
                                where(1=1)
                                ;
                            "
                            );
                        //
                        dsData_Inv_InventoryVerifiedID = _cf.db.ExecQuery(strSql_Check);
                        dsData_Inv_InventoryVerifiedID.Tables[0].TableName = "Inv_InventoryVerifiedID";
                        ////

                    }
                    #endregion

                    #region // OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn:
                    if (dsData_Inv_InventoryVerifiedID.Tables[0].Rows.Count > 0)
                    {
                        OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                                            //, strWAUserPassword // strWAUserPassword
                            , nNetworkID.ToString() // nNetworkID
                            , strOrgID // strOrgID
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                                                      ////
                            , dtimeSys // dtimeSys
                            , strMST // strMST
                                     ////
                            , strIF_InvInNo // strIF_InvInNo
                                            /////
                            , dsData_Inv_InventoryVerifiedID
                            ////
                            );

                    }
                    #endregion
                }
            }

            stopWatchFunc2M.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2M.ElapsedMilliseconds", stopWatchFunc2M.ElapsedMilliseconds
                    });

            #endregion

            #region // OSDMS_InvF_InventoryIn_UpdQtyOrder:
            Stopwatch stopWatchFunc2N = new Stopwatch();
            stopWatchFunc2N.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "OSDMS_InvF_InventoryIn_UpdQtyOrder"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!string.IsNullOrEmpty(strRefNoSys)
                && CmUtils.StringUtils.StringEqualIgnoreCase(strRefType, TConst.RefType.PrinterOrder))
            {

                #region // Call Func WA_Mst_PrintOrder_Get:
                string strLQDMSNetWorkUrl = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_API_URL]);// _cf.nvcParams["OS_LQDMS_API_Url"];
                string strProductOrder = null;
                {
                    RT_Mst_PrintOrder objRT_Mst_PrintOrder = null;
                    {
                        #region // WA_Mst_Inventory_Get:
                        /////
                        RQ_Mst_PrintOrder objRQ_Mst_PrintOrder = new RQ_Mst_PrintOrder()
                        {
                            FlagIsDelete = TConst.Flag.No,
                            Rt_Cols_Mst_PrintOrder = "*",
                            Tid = strTid,
                            //TokenID = strOS_MasterServer_Solution_TokenID,
                            //NetworkID = strNetworkID,
                            //OrgID = strOrgID,
                            GwUserCode = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_GWUSERCODE]), //_cf.nvcParams["OS_LQDMS_GwUserCode"],
                            GwPassword = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_GWPASSWORD]), //_cf.nvcParams["OS_LQDMS_GwPassword"],
                            WAUserCode = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_BG_WAUSERCODE]), //_cf.nvcParams["OS_LQDMS_BG_WAUserCode"],
                            WAUserPassword = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_BG_WAUSERPASSWORD]), //_cf.nvcParams["OS_LQDMS_BG_WAUserPassword"],
                            AccessToken = null,

                            Ft_RecordStart = "0",
                            Ft_RecordCount = "123456",
                            Ft_WhereClause = CmUtils.StringUtils.Replace("Mst_PrintOrder.PrintOrdNo = '@strPrintOrdNo' and Mst_PrintOrder.OrgIDKH = '@strOrgID' and Mst_PrintOrder.FlagActive = '1'", "@strPrintOrdNo", strRefNoSys, "@strOrgID", strOrgID)
                        };
                        ////
                        try
                        {
                            objRT_Mst_PrintOrder = OSDMS_Mst_PrintOrderService.Instance.WA_Mst_PrintOrder_Get(strLQDMSNetWorkUrl, objRQ_Mst_PrintOrder);
                            ////
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
                                TError.ErridnInventory.CmSys_InvalidOutSite + "." + "LQDMS" + "." + strErrorCodeOS
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        ////
                        if (objRT_Mst_PrintOrder.Lst_Mst_PrintOrder.Count < 1)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.PrintOrdNo", strRefNoSys
                                , "Expect.FlagActive", TConst.Flag.Active
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.InvF_InventoryIn_ApprX_InvalidPrintOrdNo
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        else
                        {
                            // 20210410. 1 Đơn hàng in( từ LQDMS) chỉ có 1 sản phẩm
                            strProductOrder = TUtils.CUtils.StdStr(objRT_Mst_PrintOrder.Lst_Mst_PrintOrder[0].mp_ProductCodeUser); // Mã sản phẩm người dùng từ đơn hàng
                        }
                        #endregion
                    }

                }

                #endregion

                #region // Call Func WA_Mst_PrintOrder_Update:
                DataSet dsData_InvF_InventoryIn = new DataSet();
                {
                    #region // Refine and check Input:
                    string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                    string strInvOutDate = TUtils.CUtils.StdDate(dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"));
                    //string strProductOrder = TUtils.CUtils.StdParam(htCacheMstParam[TConst.Mst_Param.INVENTORY_LQDMS_PRODUCTCODEUSER]);
                    Int64 nQtyIn = 0;
                    #endregion

                    #region // Get AccessToken:
                    // Hien tai chua NC SOS nen goi ham de lay AccessToken. Khi nao NC SOS thi bo di.
                    //string strAccessToken = null;
                    //{
                    //	OS_MstSv_Sys_User_GetAccessToken(
                    //		strTid // strTid
                    //		, strGwUserCode // strGwUserCode
                    //		, strGwPassword // strGwPassword
                    //		, strWAUserCode // strWAUserCode
                    //		, strWAUserPassword // strWAUserPassword
                    //		, strNetworkID // nNetworkID
                    //		, strOrgID // strOrgID
                    //				   //, strAccessToken // strAccessToken
                    //		, ref alParamsCoupleError // alParamsCoupleError
                    //								  ////
                    //		, dtimeSys // dtimeSys
                    //				   //, strMST // strMST
                    //				   ////
                    //		, out strAccessToken
                    //		////
                    //		);
                    //}
                    #endregion

                    #region // Build Input: 
                    {
                        string strSql_Check = CmUtils.StringUtils.Replace(@"
                                --- #tbl_InvF_InventoryInDtl_Filter:
                                select 
								    g.OrgID
	                                , t.IF_InvInNo
	                                , t.ProductCode
	                                , sum(t.Qty) Qty
                                into #tbl_InvF_InventoryInDtl_Filter
                                from InvF_InventoryInDtl t --//[mylock]
								    inner join #input_InvF_InventoryIn f --//[mylock]
									    on t.IF_InvInNo = f.IF_InvInNo
								    inner join InvF_InventoryIn g --//[mylock]
									    on t.IF_InvInNo = g.IF_InvInNo
								    left join Mst_Product h --//[mylock]
									    on g.OrgID = h.OrgID
											and t.ProductCode = h.ProductCode
                                where(1=1)
									and h.ProductCodeUser = '@strProductOrder'
                                group by 
								    g.OrgID
	                                , t.IF_InvInNo
	                                , t.ProductCode
                                ;

                                ---- #tbl_InvF_InventoryInDtl:
                                select  
	                                f.RefNoSys
								    , f.RefType
								    , f.OrgID
								    , t.ProductCode
								    , t.Qty
                                from #tbl_InvF_InventoryInDtl_Filter t --//[mylock]
								    inner join InvF_InventoryIn f --//[mylock]
									    on t.IF_InvInNo = f.IF_InvInNo
                                where(1=1)
                                ;

                                --- Clear For Debug:
                                drop table #tbl_InvF_InventoryInDtl_Filter;
                            "
                            , "@strProductOrder", strProductOrder
                            );
                        //
                        dsData_InvF_InventoryIn = _cf.db.ExecQuery(strSql_Check);
                        //dsData_InvF_InventoryOut.Tables[0].TableName = "InvF_InventoryOutDtl";
                        ////

                    }
                    #endregion

                    #region // OSDMS_Mst_PrintOrder_Update:
                    if (dsData_InvF_InventoryIn.Tables[0].Rows.Count > 0)
                    {

                        nQtyIn = Convert.ToInt64(dsData_InvF_InventoryIn.Tables[0].Rows[0]["Qty"]);
                        ////
                        OSDMS_Mst_PrintOrder_Update(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                                            //, strWAUserPassword // strWAUserPassword
                            , strNetworkID // nNetworkID
                            , strOrgID // strOrgID
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                                                      ////
                            , dtimeSys // dtimeSys
                                       ////
                            , strRefNoSys // objOrderNo
                            , nQtyIn
                            /////
                            );
                    }
                    #endregion
                }

                #endregion
            }
            stopWatchFunc2N.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "OSDMS_InvF_InventoryIn_UpdQtyOrder"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2N.ElapsedMilliseconds", stopWatchFunc2N.ElapsedMilliseconds
                    });
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryIn; 
						drop table #tbl_InvF_InventoryInDtl_Temp;
						drop table #tbl_Mst_NNT_ViewAbility;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            #region // Notify:
            //        {
            //            string strQInvoiceOrderAdmin_UserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE]);
            //            string strQInvoiceOrderAdmin_UserPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD]);
            //            string strSkyCICBaseURL = Convert.ToString(htCacheMstParam[TConst.Mst_Param.PARAM_SKYCICBASEURL]);

            //            var model = new
            //            {
            //                List = new List<dynamic>
            //                {

            //                }

            //            };

            //            string strSql_InvF_InventoryIn = CmUtils.StringUtils.Replace(@"
            //                    ---- Get InvF_InventoryOut:
            //                    select
            //                     t.IF_InvInNo
            //                        , t.OrgID
            //	        , t.CustomerCode
            //	        , f.CustomerName
            //	        , t.InvFCFInCode03
            //                    from InvF_InventoryIn t --//[mylock]
            //                        inner join Mst_Customer f --//[mylock]
            //                         on t.CustomerCode = f.CustomerCodeSys
            //                    where (1=1)
            //	    and t.IF_InvInNo = '@strIF_InvInNo'
            //                    ;
            //                "
            //                , "@strIF_InvInNo", strIF_InvInNo
            //                );

            //            DataTable dt_InvF_InventoryIn = _cf.db.ExecQuery(strSql_InvF_InventoryIn).Tables[0];
            //if(dt_InvF_InventoryIn.Rows.Count > 0)
            //{

            //	string strCustomerName = dt_InvF_InventoryIn.Rows[0]["CustomerName"].ToString();
            //	string strBSX = dt_InvF_InventoryIn.Rows[0]["InvFCFInCode03"].ToString();
            //	string strOrgID = dt_InvF_InventoryIn.Rows[0]["OrgID"].ToString();

            //	string strMessage = CmUtils.StringUtils.Replace(
            //			@"Đã nhập kho <IF_InvInNo> từ <CustomerName>; Biển số xe: <BSX>; Thời gian:<Time>"
            //		, "<IF_InvInNo>", strIF_InvInNo
            //		, "<CustomerName>", strCustomerName
            //		, "<BSX>", strBSX
            //		, "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss") // ApprDTimeUTC
            //                    ).Trim(); // Tên KH(CustomerName) || Biển số xe(BSX) ||

            //	string strSql_Sys_User = CmUtils.StringUtils.Replace(@"
            //                    ---- Get Sys_User:
            //                    select
            //                     t.UserCode
            //                    from Sys_User t --//[mylock]
            //                    where (1=1)
            //                        and t.OrgID = '@strOrgID'
            //                    ;
            //                "
            //		, "@strOrgID", strOrgID
            //		);

            //	DataTable dt_Sys_User = _cf.db.ExecQuery(strSql_Sys_User).Tables[0];

            //                //List<InosUser> lstInosUser = new List<InosUser>();

            //                //for (int nScan = 0; nScan < dt_Sys_User.Rows.Count; nScan++)
            //                //{
            //                //	DataRow drScan = dt_Sys_User.Rows[nScan];
            //                //	////
            //                //	InosUser objInosUser = new InosUser()
            //                //	{
            //                //		Email = drScan["UserCode"].ToString()
            //                //	};

            //                //	lstInosUser.Add(objInosUser);
            //                //}

            //                //Inos_Notification_SendX(
            //                //	TConst.NotifyTitle.TitleInventory
            //                //	, strMessage
            //                //	, lstInosUser
            //                //	);

            //                for (int nScan = 0; nScan < dt_Sys_User.Rows.Count; nScan++)
            //                {
            //                    DataRow drScan = dt_Sys_User.Rows[nScan];
            //                    ////
            //                    //InosUser objInosUser = new InosUser()
            //                    //{
            //                    //    Email = drScan["UserCode"].ToString()
            //                    //};

            //                    var objUserCreate = new GeneneralNotification()
            //                    {
            //                        Email = drScan["UserCode"].ToString(),
            //                        Detail = strMessage.Trim()
            //                    };

            //                    model.List.Add(objUserCreate);
            //                }

            //                if (model != null)
            //                {
            //                    ////
            //                    AccountService objAccountService = new AccountService(null);

            //                    var strAuthorization = objAccountService.RequestToken(strQInvoiceOrderAdmin_UserCode, strQInvoiceOrderAdmin_UserPassword, new string[] { "test" }).AccessToken;
            //                    ////
            //                    // if
            //                    //if (strAuthorization.Length < 0 || string.IsNullOrEmpty(strAuthorization))
            //                    //{
            //                    //    alParamsCoupleError.AddRange(new object[]{
            //                    //        "Check.strAuthorization", "null"
            //                    //        });
            //                    //    throw CmUtils.CMyException.Raise(
            //                    //        TError.ErridnInventory.Inv_InventoryBalanceCustomer_UpdQtyReturn_QtyBaseReturnOverQtyRemainIn
            //                    //        , null
            //                    //        , alParamsCoupleError.ToArray()
            //                    //        );
            //                    //}
            //                    ////
            //                    SkyCIC_NotificationApi_SendGeneralNotificationX(
            //                        ref alParamsCoupleError // alParamsCoupleError
            //                        , strSkyCICBaseURL // strSkyCICUrl
            //                        , strAuthorization // strAuthorization
            //                        , model // model
            //                        );
            //                }
            //            }
            //        }
            #endregion

            #region // Notify New: // 20220625. HuongTa: Rem để thực hiện tăng tốc Demo.
            Stopwatch stopWatchFunc2O = new Stopwatch();
            stopWatchFunc2O.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Notify New"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
         //   {
         //       string strSql_InvF_InventoryIn = CmUtils.StringUtils.Replace(@"
         //               ---- Get InvF_InventoryOut:
         //               select
	        //                t.IF_InvInNo
         //                   , t.OrgID
					    //    , t.CustomerCode
					    //    , f.CustomerName
					    //    , t.InvFCFInCode03
         //               from InvF_InventoryIn t --//[mylock]
         //                   inner join Mst_Customer f --//[mylock]
	        //                    on t.CustomerCode = f.CustomerCodeSys
         //               where (1=1)
					    //and t.IF_InvInNo = '@strIF_InvInNo'
         //               ;
         //           "
         //           , "@strIF_InvInNo", strIF_InvInNo
         //           );

         //       DataTable dt_InvF_InventoryIn = _cf.db.ExecQuery(strSql_InvF_InventoryIn).Tables[0];
         //       if (dt_InvF_InventoryIn.Rows.Count > 0)
         //       {

         //           string strCustomerName = dt_InvF_InventoryIn.Rows[0]["CustomerName"].ToString();
         //           string strBSX = dt_InvF_InventoryIn.Rows[0]["InvFCFInCode03"].ToString();
         //           strOrgID = dt_InvF_InventoryIn.Rows[0]["OrgID"].ToString();

         //           string strMessage = CmUtils.StringUtils.Replace(
         //                   @"Đã nhập kho <IF_InvInNo> từ <CustomerName>; Biển số xe: <BSX>; Thời gian:<Time>"
         //               , "<IF_InvInNo>", strIF_InvInNo
         //               , "<CustomerName>", strCustomerName
         //               , "<BSX>", strBSX
         //               , "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss") // ApprDTimeUTC
         //               ).Trim(); // Tên KH(CustomerName) || Biển số xe(BSX) ||

         //           //string strSql_Sys_User = CmUtils.StringUtils.Replace(@"
         //           //    ---- Get Sys_User:
         //           //    select
         //           //     t.UserCode
         //           //    from Sys_User t --//[mylock]
         //           //    where (1=1)
         //           //        and t.OrgID = '@strOrgID'
         //           //    ;
         //           //"
         //           //    , "@strOrgID", strOrgID
         //           //    );

         //           //DataTable dt_Sys_User = _cf.db.ExecQuery(strSql_Sys_User).Tables[0];

         //           List<InosUser> lstInosUser = new List<InosUser>();

         //           //for (int nScan = 0; nScan < dt_Sys_User.Rows.Count; nScan++)
         //           //{
         //           //    DataRow drScan = dt_Sys_User.Rows[nScan];
         //           //    ////
         //           //    InosUser objInosUser = new InosUser()
         //           //    {
         //           //        Email = drScan["UserCode"].ToString()
         //           //    };

         //           //    lstInosUser.Add(objInosUser);
         //           //}

         //           Inos_Notification_SendX(
         //               strOrgID
         //               , TConst.NotifyTitle.TitleInventory
         //               , strMessage
         //               , TConst.NotifyType.ApStockIn
         //               , TConst.NotifySubType.INV_IN
         //               , strIF_InvInNo
         //               , ""
         //               , strAccessToken
         //               , lstInosUser
         //               );
         //       }
         //   }
            stopWatchFunc2O.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Notify New"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2O.ElapsedMilliseconds", stopWatchFunc2O.ElapsedMilliseconds
                    });
            #endregion

        }
        #endregion

        #region // InvF_InventoryOut:
        public DataSet WAS_InvF_InventoryOut_SaveAndAppr_New20220625(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_InventoryOut objRQ_InvF_InventoryOut
            ////
            , out RT_InvF_InventoryOut objRT_InvF_InventoryOut
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_InventoryOut.Tid;
            objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_InventoryOut_SaveAndAppr";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_InventoryOut_SaveAndAppr;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_InvF_InventoryOut", TJson.JsonConvert.SerializeObject(objRQ_InvF_InventoryOut)
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
                    if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover == null)
                        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover = new List<InvF_InventoryOutCover>();
                    {
                        DataTable dt_InvF_InventoryOutCover = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutCover>(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover, "InvF_InventoryOutCover");
                        dsData.Tables.Add(dt_InvF_InventoryOutCover);
                    }
                    ////
                    if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutDtl == null)
                        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutDtl = new List<InvF_InventoryOutDtl>();
                    {
                        DataTable dt_InvF_InventoryOutDtl = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutDtl>(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutDtl, "InvF_InventoryOutDtl");
                        dsData.Tables.Add(dt_InvF_InventoryOutDtl);
                    }
                    ////
                    if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot == null)
                        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot = new List<InvF_InventoryOutInstLot>();
                    {
                        DataTable dt_InvF_InventoryOutInstLot = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutInstLot>(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot, "InvF_InventoryOutInstLot");
                        dsData.Tables.Add(dt_InvF_InventoryOutInstLot);
                    }
                    ////
                    if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial == null)
                        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial = new List<InvF_InventoryOutInstSerial>();
                    {
                        DataTable dt_InvF_InventoryOutInstSerial = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutInstSerial>(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial, "InvF_InventoryOutInstSerial");
                        dsData.Tables.Add(dt_InvF_InventoryOutInstSerial);
                    }
                    ////
                    if (objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutQR == null)
                        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutQR = new List<InvF_InventoryOutQR>();
                    {
                        DataTable dt_InvF_InventoryOutQR = TUtils.DataTableCmUtils.ToDataTable<InvF_InventoryOutQR>(objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutQR, "InvF_InventoryOutQR");
                        dsData.Tables.Add(dt_InvF_InventoryOutQR);
                    }

                }
                #endregion

                #region // InvF_InventoryOut_SaveAndAppr:
                mdsResult = InvF_InventoryOut_SaveAndAppr_New20220625(
                    objRQ_InvF_InventoryOut.Tid // strTid
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    , objRQ_InvF_InventoryOut.WAUserCode // strUserCode
                    , objRQ_InvF_InventoryOut.WAUserPassword // strUserPassword
                    , objRQ_InvF_InventoryOut.AccessToken // strAccessToken
                    , objRQ_InvF_InventoryOut.NetworkID // strNetworkID
                    , objRQ_InvF_InventoryOut.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              /////
                    , objRQ_InvF_InventoryOut.FlagIsDelete // FlagIsDelete
                    , objRQ_InvF_InventoryOut.FlagIsCheckTotal // FlagIsCheckTotal
                                                               ////
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.IF_InvOutNo // IF_InvOutNo
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.NetworkID // NetworkID
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.OrgID // OrgID
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvOutType // InvOutType
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvCodeOut // InvCodeOut
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.IF_InvAudNo // IF_InvAudNo
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.CustomerCode // CustomerCode
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.OrderNoSys // OrderNoSys
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.OrderNo // OrderNo
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.OrderType // OrderType
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.RefNoSys // RefNoSys
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.RefNo // RefNo
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.RefType // RefType
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.UseReceive // UseReceive
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.TotalValOut // TotalValOut
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.TotalValOutDesc // TotalValOutDesc
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.TotalValOutAfterDesc // TotalValOutAfterDesc
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.Remark // Remark
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.FlagNotify // FlagNotify
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode01 // InvFCFOutCode01
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode02 // InvFCFOutCode02
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode03 // InvFCFOutCode03
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode04 // InvFCFOutCode04
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode05 // InvFCFOutCode05
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode06 // InvFCFOutCode06
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode07 // InvFCFOutCode07
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode08 // InvFCFOutCode08
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode09 // InvFCFOutCode09
                    , objRQ_InvF_InventoryOut.InvF_InventoryOut.InvFCFOutCode10 // InvFCFOutCode10
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

        public DataSet InvF_InventoryOut_SaveAndAppr_New20220625(
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
            , object objFlagIsCheckTotal
            //// 
            , object objIF_InvOutNo
            , object objNetworkID
            , object objOrgID
            , object objInvOutType
            , object objInvCodeOut
            , object objIF_InvAudNo
            , object objCustomerCode
            , object objOrderNoSys
            , object objOrderNo
            , object objOrderType
            , object objRefNoSys
            , object objRefNo
            , object objRefType
            , object objUseReceive
            , object objTotalValOut
            , object objTotalValOutDesc
            , object objTotalValOutAfterDesc
            , object objRemark
            , object objFlagNotify
            , object objInvFCFOutCode01
            , object objInvFCFOutCode02
            , object objInvFCFOutCode03
            , object objInvFCFOutCode04
            , object objInvFCFOutCode05
            , object objInvFCFOutCode06
            , object objInvFCFOutCode07
            , object objInvFCFOutCode08
            , object objInvFCFOutCode09
            , object objInvFCFOutCode10
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_InventoryOut_SaveAndAppr";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_InventoryOut_SaveAndAppr;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objFlagIsDelete", objFlagIsDelete
                , "objFlagIsCheckTotal", objFlagIsCheckTotal
                //// 
                , "objIF_InvOutNo", objIF_InvOutNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objInvOutType", objInvOutType
                , "objInvCodeOut", objInvCodeOut
                , "objCustomerCode", objCustomerCode
                , "objOrderNoSys", objOrderNoSys
                , "objOrderNo", objOrderNo
                , "objOrderType", objOrderType
                , "objRefNoSys", objRefNoSys
                , "objRefNo", objRefNo
                , "objRefType", objRefType
                , "objUseReceive", objUseReceive
                , "objTotalValOut", objTotalValOut
                , "objTotalValOutDesc", objTotalValOutDesc
                , "objTotalValOutAfterDesc", objTotalValOutAfterDesc
                , "objRemark", objRemark
                , "objFlagNotify", objFlagNotify
                , "objInvFCFOutCode01", objInvFCFOutCode01
                , "objInvFCFOutCode02", objInvFCFOutCode02
                , "objInvFCFOutCode03", objInvFCFOutCode03
                , "objInvFCFOutCode04", objInvFCFOutCode04
                , "objInvFCFOutCode05", objInvFCFOutCode05
                , "objInvFCFOutCode06", objInvFCFOutCode06
                , "objInvFCFOutCode07", objInvFCFOutCode07
                , "objInvFCFOutCode08", objInvFCFOutCode08
                , "objInvFCFOutCode09", objInvFCFOutCode09
                , "objInvFCFOutCode10", objInvFCFOutCode10
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

                #region // InvF_InventoryOut_SaveX:
                Stopwatch stopWatchFunc1 = new Stopwatch();
                stopWatchFunc1.Start();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_InventoryOut_SaveX"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                DataSet dsGetData = new DataSet();
                {
                    InvF_InventoryOut_SaveX_New20220625(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , ref alParamsCoupleSW // alParamsCoupleSW
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                        , objFlagIsCheckTotal // objFlagIsCheckTotal
                                              //// 
                        , objIF_InvOutNo // objIF_InvOutNo
                        , objNetworkID // objNetworkID
                        , objOrgID // objOrgID
                        , objInvOutType // objInvOutType
                        , objInvCodeOut // objInvCodeOut
                        , objIF_InvAudNo // objIF_InvAudNo
                        , objCustomerCode // objCustomerCode
                        , objOrderNoSys // objOrderNoSys
                        , objOrderNo // objOrderNo
                        , objOrderType // objOrderType
                        , objRefNoSys // objRefNoSys
                        , objRefNo // objRefNo
                        , objRefType // objRefType
                        , objUseReceive // objUseReceive
                        , objTotalValOut // objTotalValOut
                        , objTotalValOutDesc // objTotalValOutDesc
                        , objTotalValOutAfterDesc // objTotalValOutAfterDesc
                        , objRemark // objRemark

                        , objInvFCFOutCode01 // objInvFCFOutCode01
                        , objInvFCFOutCode02 // objInvFCFOutCode02
                        , objInvFCFOutCode03 // objInvFCFOutCode03
                        , objInvFCFOutCode04 // objInvFCFOutCode04
                        , objInvFCFOutCode05 // objInvFCFOutCode05
                        , objInvFCFOutCode06 // objInvFCFOutCode06
                        , objInvFCFOutCode07 // objInvFCFOutCode07
                        , objInvFCFOutCode08 // objInvFCFOutCode08
                        , objInvFCFOutCode09 // objInvFCFOutCode09
                        , objInvFCFOutCode10 // objInvFCFOutCode10
                        , "" //objIF_InvOutNo_Root
                        , TConst.Flag.No
                        /////
                        , dsData // dsData
                        );
                }

                stopWatchFunc1.Stop();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_InventoryOut_SaveX"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc1.ElapsedMilliseconds
                    });
                #endregion

                #region // InvF_InventoryOut_ApprX:
                Stopwatch stopWatchFunc2 = new Stopwatch();
                stopWatchFunc2.Start();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_InventoryOut_ApprX"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                {
                    InvF_InventoryOut_ApprX_New20220625(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , ref alParamsCoupleSW // alParamsCoupleSW
                        , dtimeSys // dtimeSys
                                   //// 
                        , objIF_InvOutNo // objIF_InvOutNo
                        , TConst.Flag.Inactive
                        , TConst.Flag.Inactive // objFlagCallInBrand 
                        , objRemark // objRemark
                        , objFlagNotify
                        );
                }

                stopWatchFunc2.Stop();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_InventoryOut_ApprX"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2.ElapsedMilliseconds
                    });
                #endregion

                #region // Get Data:
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

        private void InvF_InventoryOut_SaveX_New20220625(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , ref ArrayList alParamsCoupleSW
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            , object objFlagIsCheckTotal
            //// 
            , object objIF_InvOutNo
            , object objNetworkID
            , object objOrgID
            , object objInvOutType
            , object objInvCodeOut
            , object objIF_InvAudNo
            , object objCustomerCode
            , object objOrderNoSys
            , object objOrderNo
            , object objOrderType
            , object objRefNoSys
            , object objRefNo
            , object objRefType
            , object objUseReceive
            , object objTotalValOut
            , object objTotalValOutDesc
            , object objTotalValOutAfterDesc
            , object objRemark
            , object objInvFCFOutCode01
            , object objInvFCFOutCode02
            , object objInvFCFOutCode03
            , object objInvFCFOutCode04
            , object objInvFCFOutCode05
            , object objInvFCFOutCode06
            , object objInvFCFOutCode07
            , object objInvFCFOutCode08
            , object objInvFCFOutCode09
            , object objInvFCFOutCode10
            ////
            , object objIF_InvOutNo_Root
            , object objFlagXuatCheo
            /////
            , DataSet dsData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOut_SaveX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objIF_InvOutNo", objIF_InvOutNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objInvOutType", objInvOutType
                , "objInvCodeOut", objInvCodeOut
                , "objIF_InvAudNo", objIF_InvAudNo
                , "objCustomerCode", objCustomerCode
                , "objOrderNoSys", objOrderNoSys
                , "objOrderNo", objOrderNo
                , "objOrderType", objOrderType
                , "objRefNoSys", objRefNoSys
                , "objRefNo", objRefNo
                , "objRefType", objRefType
                , "objUseReceive", objUseReceive
                , "objTotalValOut", objTotalValOut
                , "objTotalValOutDesc", objTotalValOutDesc
                , "objTotalValOutAfterDesc", objTotalValOutAfterDesc
                , "objRemark", objRemark
                , "objInvFCFOutCode01", objInvFCFOutCode01
                , "objInvFCFOutCode02", objInvFCFOutCode02
                , "objInvFCFOutCode03", objInvFCFOutCode03
                , "objInvFCFOutCode04", objInvFCFOutCode04
                , "objInvFCFOutCode05", objInvFCFOutCode05
                , "objInvFCFOutCode06", objInvFCFOutCode06
                , "objInvFCFOutCode07", objInvFCFOutCode07
                , "objInvFCFOutCode08", objInvFCFOutCode08
                , "objInvFCFOutCode09", objInvFCFOutCode09
                , "objInvFCFOutCode10", objInvFCFOutCode10
                ////
                , "objFlagXuatCheo", objFlagXuatCheo
                , "objIF_InvOutNo_Root", objIF_InvOutNo_Root
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

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Refine and Check Input InvF_InventoryOut:
            Stopwatch stopWatchFunc1A = new Stopwatch();
            stopWatchFunc1A.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOut"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            bool bIsCheckTotal = CmUtils.StringUtils.StringEqual(objFlagIsCheckTotal, TConst.Flag.Yes);
            ////
            string strIF_InvOutNo = TUtils.CUtils.StdParam(objIF_InvOutNo);
            string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strInvOutType = TUtils.CUtils.StdParam(objInvOutType);
            string strInvCodeOut = TUtils.CUtils.StdParam(objInvCodeOut);
            string strIF_InvAudNo = TUtils.CUtils.StdParam(objIF_InvAudNo);
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
            string strOrderNo = TUtils.CUtils.StdParam(objOrderNo);
            string strOrderNoSys = TUtils.CUtils.StdParam(objOrderNoSys);
            string strOrderType = TUtils.CUtils.StdParam(objOrderType);
            string strRefNo = TUtils.CUtils.StdParam(objRefNo);
            string strRefNoSys = TUtils.CUtils.StdParam(objRefNoSys);
            string strRefType = TUtils.CUtils.StdParam(objRefType);
            string strUseReceive = string.Format("{0}", objUseReceive).Trim();
            string strRemark = string.Format("{0}", objRemark).Trim();
            ////

            string strInvFCFOutCode01 = string.Format("{0}", objInvFCFOutCode01).Trim();
            string strInvFCFOutCode02 = string.Format("{0}", objInvFCFOutCode02).Trim();
            string strInvFCFOutCode03 = string.Format("{0}", objInvFCFOutCode03).Trim();
            string strInvFCFOutCode04 = string.Format("{0}", objInvFCFOutCode04).Trim();
            string strInvFCFOutCode05 = string.Format("{0}", objInvFCFOutCode05).Trim();
            string strInvFCFOutCode06 = string.Format("{0}", objInvFCFOutCode06).Trim();
            string strInvFCFOutCode07 = string.Format("{0}", objInvFCFOutCode07).Trim();
            string strInvFCFOutCode08 = string.Format("{0}", objInvFCFOutCode08).Trim();
            string strInvFCFOutCode09 = string.Format("{0}", objInvFCFOutCode09).Trim();
            string strInvFCFOutCode10 = string.Format("{0}", objInvFCFOutCode10).Trim();
            ////
            string strIF_InvOutNo_Root = string.Format("{0}", objIF_InvOutNo_Root).Trim();
            string strFlagXuatCheo = TUtils.CUtils.StdFlag(objFlagXuatCheo);
            /////
            DataTable dtDB_InvF_InventoryOut = null;
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            string strApprDTimeUTC = null;
            string strApprBy = null;
            string strIF_InvOutStatus = TConst.IF_InvOutStatus.Pending;
            string strFlagCheckBlock = TConst.Flag.No;
            {
                ////
                if (string.IsNullOrEmpty(strIF_InvOutNo))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvOutNo", strIF_InvOutNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidIF_InvOutNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                /////
                InvF_InventoryOut_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutNo // objIF_InvOutNo
                    , "" // strFlagExistToCheck
                    , "" // strIF_InvOutStatusListToCheck
                    , out dtDB_InvF_InventoryOut // dtDB_InvF_InventoryOut
                    );
                ////
                if (dtDB_InvF_InventoryOut.Rows.Count < 1) // Chưa Tồn tại.
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
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(dtDB_InvF_InventoryOut.Rows[0]["IF_InvOutStatus"], TConst.IF_InvOutStatus.Pending)
                        && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.IF_InvOutStatus", dtDB_InvF_InventoryOut.Rows[0]["IF_InvOutStatus"]
                            , "Check.IF_InvOutStatus.Expected", TConst.IF_InvOutStatus.Pending
                            });

                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidStatus
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryOut.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["CreateBy"]);
                    strApprDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_InventoryOut.Rows[0]["ApprDTimeUTC"]);
                    strApprBy = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["ApprBy"]);
                    strIF_InvOutStatus = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["IF_InvOutStatus"]);
                    ////
                }
                if (!CmUtils.StringUtils.StringEqualIgnoreCase((TUtils.CUtils.StdFlag(drAbilityOfUser["FlagBG"])), TConst.Flag.Active))
                {
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MNNTOrgID"], strOrgID))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.NNT.MNNTOrgID", drAbilityOfUser["MNNTOrgID"]
                            , "Check.strOrgID", strOrgID
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidOrgID
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }

                ////
                DataTable dtDB_Mst_Org = null;

                Mst_Org_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_Org // dtDB_Mst_Org
                    );
                ////
                DataTable dtDB_Mst_InvOutType = null;

                Mst_InvOutType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , strInvOutType // objInvOutType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strFlagStatisticListToCheck
                    , out dtDB_Mst_InvOutType // dtDB_Mst_InvOutType
                    );
                ////
                DataTable dtDB_Mst_Inventory = null;

                Mst_Inventory_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objOrgID // objOrgID
                    , objInvCodeOut // objInvCodeOut
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , TConst.Flag.Active //strFlagIn_OutListToCheck // 1 : Nhập xuất : 0- Vị trí
                    , out dtDB_Mst_Inventory // dtDB_Mst_Inventory
                    );
                ////
                if (string.IsNullOrEmpty(strCustomerCode))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_InvOutNo", strIF_InvOutNo
                        , "Check.strCustomerCode", strCustomerCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidCustomerCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (!CmUtils.StringUtils.StringEqualIgnoreCase(strCustomerCode, strOrgID)
                   && !string.IsNullOrEmpty(strCustomerCode))
                {
                    DataTable dtDB_Mst_Customer = null;
                    //// 
                    Mst_Customer_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objOrgID // objOrgID
                        , strCustomerCode // strCustomerCode
                        , TConst.Flag.Yes // strFlagExistListToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_Customer // dtDB_Mst_Customer
                        );
                }
                //
                ////
                double dblTotalValOut = Convert.ToDouble(objTotalValOut);

                if (dblTotalValOut < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValOut", dblTotalValOut
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                double dblTotalValOutDesc = Convert.ToDouble(objTotalValOutDesc);

                if (dblTotalValOutDesc < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValOutDesc", dblTotalValOutDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                double dblTotalValOutAfterDesc = Convert.ToDouble(objTotalValOutAfterDesc);

                if (dblTotalValOutAfterDesc < 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.dblTotalValOutAfterDesc", dblTotalValOutAfterDesc
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidValues
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (!string.IsNullOrEmpty(strRefNo))
                {
                    if (string.IsNullOrEmpty(strRefNoSys))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strIF_InvOutNo", strIF_InvOutNo
                            , "Check.strRefNo", strRefNo
                            , "Check.strRefNoSys", strRefNoSys
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidRefNoSys
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                    if (string.IsNullOrEmpty(strRefType))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strIF_InvOutNo", strIF_InvOutNo
                            , "Check.strRefNo", strRefNo
                            , "Check.strRefNoSys", strRefNoSys
                            , "Check.strRefType", strRefType
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidRefType
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                    if (!CmUtils.StringUtils.StringEqual(strRefType, TConst.RefType.OrderDL)
                        && !CmUtils.StringUtils.StringEqual(strRefType, TConst.RefType.OrderSO)
                        && !CmUtils.StringUtils.StringEqual(strRefType, TConst.RefType.OrderSR)
                        && !CmUtils.StringUtils.StringEqual(strRefType, TConst.RefType.RO)
                        && !CmUtils.StringUtils.StringEqual(strRefType, TConst.RefType.InvOut))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strIF_InvOutNo", strIF_InvOutNo
                            , "Check.strRefNo", strRefNo
                            , "Check.strRefNoSys", strRefNoSys
                            , "Check.strRefType", strRefType
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InvalidRefType
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                }
                /////
            }
            stopWatchFunc1A.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOut"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1A.ElapsedMilliseconds", stopWatchFunc1A.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveTemp InvF_InventoryOut:
            Stopwatch stopWatchFunc1B = new Stopwatch();
            stopWatchFunc1B.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOut"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOut"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutNo"
                        , "NetworkID"
                        , "OrgID"
                        , "InvOutType"
                        , "InvCodeOut"
                        , "IF_InvAudNo"
                        , "CustomerCode"
                        , "OrderNoSys"
                        , "OrderNo"
                        , "OrderType"
                        , "RefNoSys"
                        , "RefNo"
                        , "RefType"
                        , "UseReceive"
                        , "TotalValOut"
                        , "TotalValOutDesc"
                        , "TotalValOutAfterDesc"
                        , "CreateDTimeUTC"
                        , "CreateBy"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "CancelDTimeUTC"
                        , "CancelBy"
                        , "FlagQR"
                        , "IF_InvOutStatus"
                        , "Remark"
                        , "InvFCFOutCode01"
                        , "InvFCFOutCode02"
                        , "InvFCFOutCode03"
                        , "InvFCFOutCode04"
                        , "InvFCFOutCode05"
                        , "InvFCFOutCode06"
                        , "InvFCFOutCode07"
                        , "InvFCFOutCode08"
                        , "InvFCFOutCode09"
                        , "InvFCFOutCode10"
                        , "IF_InvOutNo_Root"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutNo, // IF_InvOutNo
                                nNetworkID, // NetworkID
								strOrgID, // OrgID
								strInvOutType, // InvOutType
								strInvCodeOut, // InvCodeOut
								strIF_InvAudNo, // IF_InvAudNo
								strCustomerCode, // CustomerCode
                                strOrderNoSys, // OrderNoSys
                                strOrderNo, // OrderNo
								strOrderType, // OrderType
                                strRefNoSys, // RefNoSys
                                strRefNo, // RefNo
								strRefType, // RefType
								strUseReceive, // UseReceive
								objTotalValOut, // TotalValOut
								objTotalValOutDesc, // TotalValOutDesc
								objTotalValOutAfterDesc, // TotalValOutAfterDesc
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // CreateDTimeUTC
                                strWAUserCode, // CreateBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                strApprDTimeUTC, // ApprDTimeUTC
                                strApprBy, // ApprBy
                                null, // CancelDTimeUTC
                                null, // CancelBy
                                TConst.Flag.Inactive,  // FlagQR
                                strIF_InvOutStatus, // IF_InvOutStatus
                                strRemark, // Remark
                                strInvFCFOutCode01, // InvFCFOutCode01
                                strInvFCFOutCode02, // InvFCFOutCode02
                                strInvFCFOutCode03, // InvFCFOutCode03
                                strInvFCFOutCode04, // InvFCFOutCode04
                                strInvFCFOutCode05, // InvFCFOutCode05
                                strInvFCFOutCode06, // InvFCFOutCode06
                                strInvFCFOutCode07, // InvFCFOutCode07
                                strInvFCFOutCode08, // InvFCFOutCode08
                                strInvFCFOutCode09, // InvFCFOutCode09
                                strInvFCFOutCode10, // InvFCFOutCode10
                                strIF_InvOutNo_Root, // IF_InvOutNo_Root
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            stopWatchFunc1B.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOut"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1B.ElapsedMilliseconds", stopWatchFunc1B.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutCover:
            Stopwatch stopWatchFunc1C = new Stopwatch();
            stopWatchFunc1C.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutCover"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryOutCover = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutCover";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvFInventoryOutCoverTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutCover = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryOutCover.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvFInventoryOutCoverTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutCover // dtData
                                                   //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeRoot" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "float", "UPOut" // arrstrCouple
                    , "float", "UPOutDesc" // arrstrCouple
                    , "float", "ValOutAfterDesc" // arrstrCouple
                    , "float", "UnitCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "IF_InvOutNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "ValOut", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "ValOutDesc", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "IF_InvOutStatusCover", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutCover, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutCover.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutCover.Rows[nScan];
                    ////
                    drScan["IF_InvOutNo"] = strIF_InvOutNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["ValOut"] = 0.0;
                    drScan["ValOutDesc"] = 0.0;
                    drScan["IF_InvOutStatusCover"] = strIF_InvOutStatus;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1C.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutCover"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1C.ElapsedMilliseconds", stopWatchFunc1C.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryOutCover:
            Stopwatch stopWatchFunc1D = new Stopwatch();
            stopWatchFunc1D.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutCover"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutCover" // strTableName
                    , new object[] {
                            "IF_InvOutNo", TConst.BizMix.Default_DBColType
                            //, "InvCodeOutActual", TConst.BizMix.Default_DBColType
                            , "ProductCodeRoot", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "UPOut", "float"
                            , "UPOutDesc", "float"
                            , "ValOut", "float"
                            , "ValOutDesc", "float"
                            , "ValOutAfterDesc", "float"
                            , "UnitCode", TConst.BizMix.Default_DBColType
                            , "IF_InvOutStatusCover", TConst.BizMix.Default_DBColType
                            , "Remark",  TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutCover // dtData
                );
            }
            stopWatchFunc1D.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutCover"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1D.ElapsedMilliseconds", stopWatchFunc1D.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutDtl:
            Stopwatch stopWatchFunc1E = new Stopwatch();
            stopWatchFunc1E.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutDtl"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryOutDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvFInventoryOutDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutDtl = dsData.Tables[strTableCheck];
                ////
                if (dtInput_InvF_InventoryOutDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InvFInventoryOutDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutDtl // dtData
                    , "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeRoot" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "float", "UnitCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutDtl, "IF_InvOutNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutDtl, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutDtl, "IF_InvOutStatusDtl", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutDtl, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutDtl.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutDtl.Rows[nScan];
                    ////
                    drScan["IF_InvOutNo"] = strIF_InvOutNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutStatusDtl"] = strIF_InvOutStatus;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1E.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutDtl"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1E.ElapsedMilliseconds", stopWatchFunc1E.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryOutDtl:
            Stopwatch stopWatchFunc1F = new Stopwatch();
            stopWatchFunc1F.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutDtl"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutDtl" // strTableName
                    , new object[] {
                            "IF_InvOutNo", TConst.BizMix.Default_DBColType
                            , "InvCodeOutActual", TConst.BizMix.Default_DBColType
                            , "ProductCodeRoot", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "UnitCode", TConst.BizMix.Default_DBColType
                            , "IF_InvOutStatusDtl", TConst.BizMix.Default_DBColType
                            , "Remark",  TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutDtl // dtData
                );
            }
            stopWatchFunc1F.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutDtl"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1F.ElapsedMilliseconds", stopWatchFunc1D.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutInstLot:
            Stopwatch stopWatchFunc1G = new Stopwatch();
            stopWatchFunc1G.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutInstLot"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryOutInstLot = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutInstLot";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InventoryOutInstLotNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutInstLot = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutInstLot // dtData
                    , "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeRoot" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "ProductLotNo" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                                     ////, "StdDate", "ProductionDate" // arrstrCouple
                                     ////, "StdDate", "ExpiredDate" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstLot, "IF_InvOutNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstLot, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstLot, "IF_InvOutILStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstLot, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstLot, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutInstLot.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutInstLot.Rows[nScan];
                    ////
                    drScan["IF_InvOutNo"] = strIF_InvOutNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutILStatus"] = strIF_InvOutStatus;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1G.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutInstLot"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1G.ElapsedMilliseconds", stopWatchFunc1G.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryOutInstLot:
            Stopwatch stopWatchFunc1H = new Stopwatch();
            stopWatchFunc1H.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutInstLot"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutInstLot" // strTableName
                    , new object[] {
                            "IF_InvOutNo", TConst.BizMix.Default_DBColType
                            , "InvCodeOutActual", TConst.BizMix.Default_DBColType
                            , "ProductCodeRoot", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "ProductLotNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Qty", "float"
                            , "IF_InvOutILStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutInstLot // dtData
                );
            }
            stopWatchFunc1H.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutInstLot"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1H.ElapsedMilliseconds", stopWatchFunc1H.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutInstSerial:
            Stopwatch stopWatchFunc1I = new Stopwatch();
            stopWatchFunc1I.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutInstSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryOutInstSerial = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutInstSerial";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InventoryOutInstSerialNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutInstSerial = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutInstSerial // dtData
                    , "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeRoot" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "SerialNo" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstSerial, "IF_InvOutNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstSerial, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstSerial, "IF_InvOutISStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstSerial, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutInstSerial, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutInstSerial.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutInstSerial.Rows[nScan];
                    ////
                    drScan["IF_InvOutNo"] = strIF_InvOutNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["IF_InvOutISStatus"] = strIF_InvOutStatus;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1I.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutInstSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1I.ElapsedMilliseconds", stopWatchFunc1I.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryOutInstSerial:
            Stopwatch stopWatchFunc1K = new Stopwatch();
            stopWatchFunc1K.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutInstSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutInstSerial" // strTableName
                    , new object[] {
                            "IF_InvOutNo", TConst.BizMix.Default_DBColType
                            , "InvCodeOutActual", TConst.BizMix.Default_DBColType
                            , "ProductCodeRoot", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "SerialNo", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "IF_InvOutISStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutInstSerial // dtData
                );
            }
            stopWatchFunc1K.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutInstSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1K.ElapsedMilliseconds", stopWatchFunc1K.ElapsedMilliseconds
                    });
            #endregion

            #region //// Refine and Check Input InvF_InventoryOutQR:
            Stopwatch stopWatchFunc1L = new Stopwatch();
            stopWatchFunc1L.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutQR"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            ////
            DataTable dtInput_InvF_InventoryOutQR = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "InvF_InventoryOutQR";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_InventoryOut_SaveX_InventoryOutQRNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_InvF_InventoryOutQR = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_InvF_InventoryOutQR // dtData
                                                //, "StdParam", "IF_InvOutNo" // arrstrCouple
                    , "StdParam", "QRCode" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "BoxNo" // arrstrCouple
                    , "StdParam", "CanNo" // arrstrCouple
                    , "StdParam", "QRType" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutQR, "IF_InvOutNo", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutQR, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutQR, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_InvF_InventoryOutQR, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_InvF_InventoryOutQR.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_InvF_InventoryOutQR.Rows[nScan];
                    string strQRType = TUtils.CUtils.StdParam(drScan["QRType"]);
                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(strQRType, TConst.QRType.Box)
                        && !CmUtils.StringUtils.StringEqualIgnoreCase(strQRType, TConst.QRType.Can)
                        && !CmUtils.StringUtils.StringEqualIgnoreCase(strQRType, TConst.QRType.Tem))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strQRType", strQRType
                            , "Check.QRType.Expected", "BOX or CARTON or TEM"
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.InvF_InventoryOut_SaveX_InventoryOutQRTbl_InvalidQRType
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    drScan["IF_InvOutNo"] = strIF_InvOutNo;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            stopWatchFunc1L.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Refine and Check Input InvF_InventoryOutQR"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1L.ElapsedMilliseconds", stopWatchFunc1L.ElapsedMilliseconds
                    });
            #endregion

            #region //// SaveTemp InvF_InventoryOutQR:
            Stopwatch stopWatchFunc1M = new Stopwatch();
            stopWatchFunc1M.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutQR"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_InvF_InventoryOutQR" // strTableName
                    , new object[] {
                            "IF_InvOutNo", TConst.BizMix.Default_DBColType
                            , "QRCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "ProductCode", TConst.BizMix.Default_DBColType
                            , "BoxNo", TConst.BizMix.Default_DBColType
                            , "CanNo", TConst.BizMix.Default_DBColType
                            , "QRType", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_InvF_InventoryOutQR // dtData
                );
            }
            stopWatchFunc1M.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveTemp InvF_InventoryOutQR"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1M.ElapsedMilliseconds", stopWatchFunc1M.ElapsedMilliseconds
                    });
            #endregion

            #region // Check all Conditional Input: => 285
            Stopwatch stopWatchFunc1N = new Stopwatch();
            stopWatchFunc1N.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Check all Conditional Input"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
       //     if (!bIsDelete)
       //     {
       //         #region // InvF_InventoryOutDtl:
       //         {
       //             #region // ProductCode: Phải Tồn tại và Active và ProductType = 'PRODUCT':
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- #input_InvF_InventoryOutDtl_DistinctProductCode:
       //                         select 
	      //                          f.OrgID
	      //                          , t.ProductCode
       //                         --into #input_InvF_InventoryOutDtl_DistinctProductCode
       //                         from #input_InvF_InventoryOutDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //                         ;

							//");
       //                 ////

       //                 DataTable dtInput_Mst_Product = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 Mst_Product_CheckListDB(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , dtInput_Mst_Product // dtInput_Mst_Product
       //                     , TConst.Flag.Yes // strFlagExistToCheck
       //                     , TConst.Flag.Active // strFlagActiveListToCheck
       //                     , "PRODUCT" // strProductTypeToCheck
       //                     );
       //                 /////

       //             }
       //             #endregion 

       //             #region // InvCodeOutActual: Phải Tồn tại , Active và là phải là cờ vị trí.
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- #input_InvF_InventoryOutDtl_DistinctInvCodeOutActual:
       //                         select 
	      //                          f.OrgID
	      //                          , t.InvCodeOutActual InvCode
       //                         --into #input_InvF_InventoryOutDtl_DistinctInvCodeOutActual
       //                         from #input_InvF_InventoryOutDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //                         where(1=1)
       //                         ;
							//");

       //                 DataTable dtInput_Mst_Inventory = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];

       //                 Mst_Inventory_CheckListDB(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , dtInput_Mst_Inventory // dtInput_Mst_Inventory
       //                     , TConst.Flag.Yes  //strFlagExistToCheck
       //                     , TConst.Flag.Active // strFlagActiveListToCheck
       //                     , "" // strFlagIn_OutListToCheck 
       //                     );

       //             }
       //             #endregion

       //             #region // UnitCode  phải khớp với UnitCode trong ProductCode:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                              
       //                         ---- #input_InvF_InventoryOutDtl_DistinctProductCode:
       //                         select 
	      //                          f.OrgID
	      //                          , t.ProductCode
	      //                          , t.UnitCode
       //                         --into #input_InvF_InventoryOutDtl_DistinctProductCode
       //                         from #input_InvF_InventoryOutDtl t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //                         where(1=1)
       //                         ;

							//");
       //                 ////
       //                 DataTable dt_Input_Mst_Product = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];

       //                 myCheck_Mst_Product_UnitCode(
       //                     ref alParamsCoupleError
       //                     , dt_Input_Mst_Product
       //                     );
       //                 /////
       //             }
       //             #endregion

       //             #region // Check Qty và UP, UPDesc, ValOutAfterDesc:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"
       //                         ---- Check:
       //                         select 
							//		t.IF_InvOutNo
							//		--, t.InvCodeOutActual
							//		, t.ProductCodeRoot
							//		, t.Qty
							//		, t.UPOut
							//		, t.UPOutDesc
							//		, t.ValOutAfterDesc
       //                         from #input_InvF_InventoryOutCover t --//[mylock]
       //                         where(1=1)
							//		and (t.Qty < 0 or t.UPOut < 0 or t.UPOutDesc < 0 or t.ValOutAfterDesc < 0)
       //                         ;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.IF_InvOutNo", dtDB_Check.Rows[0]["IF_InvOutNo"]
       //                         //, "Check.InvCodeOutActual", dtDB_Check.Rows[0]["InvCodeOutActual"]
       //                         , "Check.ProductCodeRoot", dtDB_Check.Rows[0]["ProductCodeRoot"]
       //                         , "Check.Qty", dtDB_Check.Rows[0]["Qty"]
       //                         , "Check.UPOut", dtDB_Check.Rows[0]["UPOut"]
       //                         , "Check.UPOutDesc", dtDB_Check.Rows[0]["UPOutDesc"]
       //                         , "Check.ValOutAfterDesc", dtDB_Check.Rows[0]["ValOutAfterDesc"]
       //                         , "Check.ConditionRaiseError", "and (t.Qty < 0 or t.UPOut < 0 or t.UPOutDesc < 0 or t.ValOutAfterDesc < 0)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryOut_SaveX_InvF_InventoryOutDtlTbl_UnitCodeNotEqualUnitCodeInMstProduct
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////
       //             }
       //             #endregion
       //         }
       //         #endregion

       //         #region // InvF_InventoryOutInstLot:
       //         {
       //             #region // IF_InvOutNo + InvCodeOutActual + ProductCode phải khớp với key InvF_InventoryOutDtl:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                

       //                         ---- #input_InvF_InventoryOutInstLot_Distinct_InvCodeOutActual_ProductCode:
       //                         select distinct
	      //                          t.IF_InvOutNo
	      //                          , t.InvCodeOutActual
	      //                          , t.ProductCodeRoot
	      //                          , t.ProductCode
       //                         into #input_InvF_InventoryOutInstLot_Distinct_InvCodeOutActual_ProductCode
       //                         from #input_InvF_InventoryOutInstLot t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //                         where(1=1)
       //                         ;

       //                         ---- Check:
       //                         select 
	      //                          t_LOT.IF_InvOutNo LOT_IF_InvOutNo
	      //                          , t_LOT.InvCodeOutActual LOT_InvCodeOutActual
	      //                          , t_LOT.ProductCodeRoot LOT_ProductCodeRoot
	      //                          , t_LOT.ProductCode LOT_ProductCode
	      //                          ----
	      //                          , f_DTL.IF_InvOutNo DTL_IF_InvOutNo
	      //                          , f_DTL.InvCodeOutActual DTL_InvCodeOutActual
	      //                          , f_DTL.ProductCode DTL_ProductCode
       //                         from #input_InvF_InventoryOutInstLot_Distinct_InvCodeOutActual_ProductCode t_LOT --//[mylock]
	      //                          left join #input_InvF_InventoryOutDtl f_DTL ---//[mylock]
		     //                           on t_LOT.IF_InvOutNo = f_DTL.IF_InvOutNo
			    //                            and t_LOT.InvCodeOutActual = f_DTL.InvCodeOutActual
			    //                            and t_LOT.ProductCodeRoot = f_DTL.ProductCodeRoot
			    //                            and t_LOT.ProductCode = f_DTL.ProductCode
       //                         where(1=1)
	      //                          and f_DTL.IF_InvOutNo is null
       //                         ;


       //                         --- Clear For Debug:
       //                         drop table #input_InvF_InventoryOutInstLot_Distinct_InvCodeOutActual_ProductCode;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.LOT_IF_InvOutNo", dtDB_Check.Rows[0]["LOT_IF_InvOutNo"]
       //                         , "Check.LOT_InvCodeOutActual", dtDB_Check.Rows[0]["LOT_InvCodeOutActual"]
       //                         , "Check.LOT_ProductCodeRoot", dtDB_Check.Rows[0]["LOT_ProductCodeRoot"]
       //                         , "Check.LOT_ProductCode", dtDB_Check.Rows[0]["LOT_ProductCode"]
       //                         , "Check.DTL_IF_InvOutNo", dtDB_Check.Rows[0]["DTL_IF_InvOutNo"]
       //                         , "Check.DTL_InvCodeOutActual", dtDB_Check.Rows[0]["DTL_InvCodeOutActual"]
       //                         , "Check.DTL_ProductCode", dtDB_Check.Rows[0]["DTL_ProductCode"]
       //                         , "Check.ConditionRaiseError", "and (f_DTL.IF_InvOutNo is null)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryOut_SaveX_InvF_InventoryOutInstLotTbl_LotNoExistDtl
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////

       //             }
       //             #endregion

       //         }
       //         #endregion 

       //         #region // InvF_InventoryOutInstSerial:
       //         {
       //             #region // IF_InvOutNo + InvCodeOutActual + ProductCode phải khớp với key InvF_InventoryOutDtl:
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"                 
       //                         ---- #input_InvF_InventoryOutInstSerial_Distinct_InvCodeOutActual_ProductCode:
       //                         select distinct
	      //                          t.IF_InvOutNo
	      //                          , t.InvCodeOutActual
	      //                          , t.ProductCodeRoot
	      //                          , t.ProductCode
       //                         into #input_InvF_InventoryOutInstSerial_Distinct_InvCodeOutActual_ProductCode
       //                         from #input_InvF_InventoryOutInstSerial t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //                         where(1=1)
       //                         ;

       //                         ---- Check:
       //                         select 
	      //                          t_Serial.IF_InvOutNo Serial_IF_InvOutNo
	      //                          , t_Serial.InvCodeOutActual Serial_InvCodeOutActual
	      //                          , t_Serial.ProductCodeRoot Serial_ProductCodeRoot
	      //                          , t_Serial.ProductCode Serial_ProductCode
	      //                          ----
	      //                          , f_DTL.IF_InvOutNo DTL_IF_InvOutNo
	      //                          , f_DTL.InvCodeOutActual DTL_InvCodeOutActual
	      //                          , f_DTL.ProductCode DTL_ProductCode
       //                         from #input_InvF_InventoryOutInstSerial_Distinct_InvCodeOutActual_ProductCode t_Serial --//[mylock]
	      //                          left join #input_InvF_InventoryOutDtl f_DTL ---//[mylock]
		     //                           on t_Serial.IF_InvOutNo = f_DTL.IF_InvOutNo
			    //                            and t_Serial.InvCodeOutActual = f_DTL.InvCodeOutActual
			    //                            and t_Serial.ProductCodeRoot = f_DTL.ProductCodeRoot
			    //                            and t_Serial.ProductCode = f_DTL.ProductCode
       //                         where(1=1)
	      //                          and f_DTL.IF_InvOutNo is null
       //                         ;

       //                         --- Clear For Debug:
       //                         drop table #input_InvF_InventoryOutInstSerial_Distinct_InvCodeOutActual_ProductCode;

							//");
       //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 if (dtDB_Check.Rows.Count > 0)
       //                 {
       //                     alParamsCoupleError.AddRange(new object[]{
       //                         "Check.Serial_IF_InvOutNo", dtDB_Check.Rows[0]["Serial_IF_InvOutNo"]
       //                         , "Check.Serial_InvCodeOutActual", dtDB_Check.Rows[0]["Serial_InvCodeOutActual"]
       //                         , "Check.Serial_ProductCodeRoot", dtDB_Check.Rows[0]["Serial_ProductCodeRoot"]
       //                         , "Check.Serial_ProductCode", dtDB_Check.Rows[0]["Serial_ProductCode"]
       //                         ////
       //                         , "Check.DTL_IF_InvOutNo", dtDB_Check.Rows[0]["DTL_IF_InvOutNo"]
       //                         , "Check.DTL_InvCodeOutActual", dtDB_Check.Rows[0]["DTL_InvCodeOutActual"]
       //                         , "Check.DTL_ProductCode", dtDB_Check.Rows[0]["DTL_ProductCode"]
       //                         , "Check.ConditionRaiseError", "and (f_DTL.IF_InvOutNo is null)"
       //                         , "Check.ErrRows.Count", dtDB_Check.Rows.Count
       //                         });
       //                     throw CmUtils.CMyException.Raise(
       //                         TError.ErridnInventory.InvF_InventoryOut_SaveX_InvF_InventoryOutInstLotTbl_SerialNoExistDtl
       //                         , null
       //                         , alParamsCoupleError.ToArray()
       //                         );
       //                 }
       //                 /////

       //             }
       //             #endregion

       //             #region // Serial tồn tại trong kho:
       //             if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
       //             {
       //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"  
       //                         ---- #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode:
       //                         select distinct 
	      //                          f.OrgID
	      //                          , t.InvCodeOutActual InvCode
	      //                          , mp.ProductCodeBase ProductCode
	      //                          , t.SerialNo
       //                         from #input_InvF_InventoryOutInstSerial t --//[mylock]
	      //                          inner join #input_InvF_InventoryOut f --//[mylock]
		     //                           on t.IF_InvOutNo = f.IF_InvOutNo
	      //                          inner join Mst_Product mp --//[mylock]
		     //                           on f.OrgID = mp.OrgID
			    //                            and t.ProductCode = mp.ProductCode
       //                         where(1=1)
       //                         ;
					  //      ");
       //                 DataTable dtInput_Inv_InventoryBalanceSerial = _cf.db.ExecQuery(
       //                     strSqlCheck
       //                     ).Tables[0];
       //                 ////
       //                 Inv_InventoryBalanceSerial_CheckListSerialAndInvCodeExist(
       //                     ref alParamsCoupleError // alParamsCoupleError
       //                     , TConst.Flag.Yes // strFlagExistToCheck
       //                     , dtInput_Inv_InventoryBalanceSerial // dtInput_Inv_InventoryBalanceSerial
       //                     );
       //                 //////

       //             }
       //             #endregion

       //         }
       //         #endregion

       //         #region // InvF_InventoryOutQR:
       //         {
       //             #region // QA  phải có trong lần nhập:
       //             //             {
       //             //                 string strSqlCheck = CmUtils.StringUtils.Replace(@"  
       //             //                         ---- #input_InvF_InventoryOutInstSerial_DistinctSerialNo:
       //             //                         select distinct 
       //             //                          t.SerialNo
       //             //                         into #input_InvF_InventoryOutInstSerial_DistinctSerialNo
       //             //                         from #input_InvF_InventoryOutInstSerial t --//[mylock]
       //             //                          inner join #input_InvF_InventoryOut f --//[mylock]
       //             //                           on t.IF_InvOutNo = f.IF_InvOutNo
       //             //                         where(1=1)
       //             //                         ;

       //             //                         ---- Check:
       //             //                         select 
       //             //                          t.QRCode
       //             //                         from #input_InvF_InventoryOutQR t --//[mylock]
       //             //                          inner join #input_InvF_InventoryOutInstSerial_DistinctSerialNo f ---//[mylock]
       //             //                           on t.QRCode = f.SerialNo
       //             //                         where(1=1)
       //             //                         ;

       //             //                         --- Clear For Debug:
       //             //                         drop table #input_InvF_InventoryOutInstSerial_DistinctSerialNo;

       //             //");
       //             //                 DataTable dtDB_Check = _cf.db.ExecQuery(
       //             //                     strSqlCheck
       //             //                     ).Tables[0];
       //             //                 ////
       //             //                 if (dtDB_Check.Rows.Count > 0)
       //             //                 {
       //             //                     alParamsCoupleError.AddRange(new object[]{
       //             //	"Check.QRCode", dtDB_Check.Rows[0]["QRCode"]
       //             //                         });
       //             //                     throw CmUtils.CMyException.Raise(
       //             //                         TError.ErridnInventory.InvF_InventoryOut_SaveX_InvF_InventoryOutQATbl_QANoExistListSeiralInvIn
       //             //                         , null
       //             //                         , alParamsCoupleError.ToArray()
       //             //                         );
       //             //                 }
       //             //                 /////

       //             //             }
       //             #endregion
       //         }
       //         #endregion

       //     }
            stopWatchFunc1N.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Check all Conditional Input"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1N.ElapsedMilliseconds", stopWatchFunc1N.ElapsedMilliseconds
                    });
            #endregion

            #region //// Save Params Sql:
            Stopwatch stopWatchFunc1O = new Stopwatch();
            stopWatchFunc1O.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Save Params Sql"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (dtInput_InvF_InventoryOutDtl != null && dtInput_InvF_InventoryOutDtl.Rows.Count > 0 && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                ////
                htParamsSql.Add("@strIF_InvOutNo", strIF_InvOutNo);
                htParamsSql.Add("@strRefNo_Type", "INVF_INVOUTINSTSERIAL");
                htParamsSql.Add("@strRefNo_PK", strIF_InvOutNo);
                htParamsSql.Add("@strBlockStatus_N", TConst.BlockStatus.No);
                htParamsSql.Add("@strBlockStatus_S", TConst.BlockStatus.SYS);
                htParamsSql.Add("@strBlockStatus_U", TConst.BlockStatus.User);
                htParamsSql.Add("@strFlagNG_Inactive", TConst.Flag.Inactive);
                for (int nScanDtl = 0; nScanDtl < dtInput_InvF_InventoryOutDtl.Rows.Count; nScanDtl++)
                {
                    {
                        htParamsSql.Add(string.Format("@strInvCode{0}", nScanDtl), dtInput_InvF_InventoryOutDtl.Rows[nScanDtl]["InvCodeOutActual"]);
                    }
                }
                ////
            }
            stopWatchFunc1O.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Save Params Sql"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1O.ElapsedMilliseconds", stopWatchFunc1O.ElapsedMilliseconds
                    });
            #endregion

            #region // InventoryTransaction: => 665
            Stopwatch stopWatchFunc1P = new Stopwatch();
            stopWatchFunc1P.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                InvFInventory_InventoryTransaction_Exec_Clear_New20220625(strFunctionName);

                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagCheckBlock, TConst.Flag.No))
                {
                    Inv_InventoryTransaction_Perform_New20220625(
                        ref alParamsCoupleError // lstParamsCoupleError
                        , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                      //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                        , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                        , strWAUserCode // strCreateBy
                        , 0.0 // dblMinQtyTotalOK
                        , int.MinValue // dblMinQtyBlockOK
                        , int.MinValue // dblMinQtyAvailOK
                        , 0.0 // dblMinQtyTotalNG
                        , int.MinValue // dblMinQtyBlockNG
                        , 0.0 // dblMinQtyAvailNG
                        );

                }
                else
                {
                    Inv_InventoryTransaction_Perform_New20220625(
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
                        );

                }
            }
            stopWatchFunc1P.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1P.ElapsedMilliseconds", stopWatchFunc1P.ElapsedMilliseconds
                    });
            #endregion

            #region // Inv_InventoryBalanceFormBlock:
            Stopwatch stopWatchFunc1Q = new Stopwatch();
            stopWatchFunc1Q.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {

                string strSqlCheck = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.IF_InvOutNo RefNo
	                        , ifio.NetworkID
	                        , 'OUT' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );
                // 20200805.HTTT.Trước SaveDB xóa luôn bản ghi trong Block theo mã phiếu
                //if (!bIsDelete)
                //{
                //    Inv_InventoryBalanceFormBlock_Delete(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );
                //    /////
                //    Inv_InventoryBalanceFormBlock_Insert(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );

                //}
                //else
                //{
                //    Inv_InventoryBalanceFormBlock_Delete(
                //        ref alParamsCoupleError
                //        , dtimeSys
                //        , dtDB_Inv_InventoryBalanceFormBlock
                //        );

                //}
            }
            stopWatchFunc1Q.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1Q.ElapsedMilliseconds", stopWatchFunc1Q.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            Stopwatch stopWatchFunc1R = new Stopwatch();
            stopWatchFunc1R.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalance:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            stopWatchFunc1R.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1R.ElapsedMilliseconds", stopWatchFunc1R.ElapsedMilliseconds
                    });
            #endregion

            #region // SaveDB: => 266
            Stopwatch stopWatchFunc1S = new Stopwatch();
            stopWatchFunc1S.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutInstSerial:
                            select 
                                t.IF_InvOutNo
								, t.InvCodeOutActual
                                , t.ProductCodeRoot
                                , t.ProductCode
                                , t.SerialNo
                            into #tbl_InvF_InventoryOutInstSerial_Del
                            from InvF_InventoryOutInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutInstSerial:
                            delete t 
                            from InvF_InventoryOutInstSerial t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutInstSerial_Del f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
										and t.InvCodeOutActual = f.InvCodeOutActual
		                                and t.ProductCodeRoot = f.ProductCodeRoot
		                                and t.ProductCode = f.ProductCode
		                                and t.SerialNo = f.SerialNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutInstLot:
                            select 
                                t.IF_InvOutNo
								, t.InvCodeOutActual
                                , t.ProductCodeRoot
                                , t.ProductCode
								, t.ProductLotNo
                            into #tbl_InvF_InventoryOutInstLot_Del
                            from InvF_InventoryOutInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutInstLot:
                            delete t 
                            from InvF_InventoryOutInstLot t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutInstLot_Del f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
										and t.InvCodeOutActual = f.InvCodeOutActual
		                                and t.ProductCodeRoot = f.ProductCodeRoot
		                                and t.ProductCode = f.ProductCode
		                                and t.ProductLotNo = f.ProductLotNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutDtl:
                            select 
                                t.IF_InvOutNo
								, t.InvCodeOutActual
                                , t.ProductCodeRoot
                                , t.ProductCode
                            into #tbl_InvF_InventoryOutDtl_Del
                            from InvF_InventoryOutDtl t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutDtl:
                            delete t 
                            from InvF_InventoryOutDtl t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutDtl_Del f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
		                                and t.InvCodeOutActual = f.InvCodeOutActual
		                                and t.ProductCodeRoot = f.ProductCodeRoot
		                                and t.ProductCode = f.ProductCode
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutCover:
                            select 
                                t.IF_InvOutNo
								--, t.InvCodeOutActual
                                , t.ProductCodeRoot
                            into #tbl_InvF_InventoryOutCover_Del
                            from InvF_InventoryOutCover t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutDtl:
                            delete t 
                            from InvF_InventoryOutCover t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutCover_Del f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
		                               -- and t.InvCodeOutActual = f.InvCodeOutActual
		                                and t.ProductCodeRoot = f.ProductCodeRoot
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutQR:
                            select 
                                t.IF_InvOutNo
								, t.QRCode
                            into #tbl_InvF_InventoryOutQR_Del
                            from InvF_InventoryOutQR t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Delete:
                            ---- InvF_InventoryOutQR:
                            delete t 
                            from InvF_InventoryOutQR t --//[mylock]
	                            inner join #tbl_InvF_InventoryOutQR_Del f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
		                                and t.QRCode = f.QRCode
                            where (1=1)
                            ;

                            ---- InvF_InventoryOut:
                            delete t
                            from InvF_InventoryOut t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutInstSerial_Del;
							drop table #tbl_InvF_InventoryOutInstLot_Del;
							drop table #tbl_InvF_InventoryOutDtl_Del;
							drop table #tbl_InvF_InventoryOutCover_Del;
							drop table #tbl_InvF_InventoryOutQR_Del;
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
                        string zzzzClauseInsert_InvF_InventoryOut_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOut:                                
                                insert into InvF_InventoryOut(
	                                IF_InvOutNo
	                                , NetworkID
	                                , OrgID
	                                , InvOutType
	                                , InvCodeOut
	                                , IF_InvAudNo
	                                , CustomerCode
	                                , OrderNoSys
	                                , OrderNo
	                                , OrderType
	                                , RefNoSys
	                                , RefNo
	                                , RefType
	                                , UseReceive
	                                , TotalValOut
	                                , TotalValOutDesc
	                                , TotalValOutAfterDesc
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , LUDTimeUTC
	                                , LUBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , CancelDTimeUTC
	                                , CancelBy
	                                , FlagQR
	                                , IF_InvOutStatus
	                                , Remark
	                                , InvFCFOutCode01
	                                , InvFCFOutCode02
	                                , InvFCFOutCode03
	                                , InvFCFOutCode04
	                                , InvFCFOutCode05
	                                , InvFCFOutCode06
	                                , InvFCFOutCode07
	                                , InvFCFOutCode08
	                                , InvFCFOutCode09
	                                , InvFCFOutCode10
	                                , IF_InvOutNo_Root
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.InvOutType
	                                , t.InvCodeOut
	                                , t.IF_InvAudNo
	                                , t.CustomerCode
	                                , t.OrderNoSys
	                                , t.OrderNo
	                                , t.OrderType
	                                , t.RefNoSys
	                                , t.RefNo
	                                , t.RefType
	                                , t.UseReceive
	                                , t.TotalValOut
	                                , t.TotalValOutDesc
	                                , t.TotalValOutAfterDesc
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.CancelDTimeUTC
	                                , t.CancelBy
	                                , t.FlagQR
	                                , t.IF_InvOutStatus
	                                , t.Remark
	                                , t.InvFCFOutCode01
	                                , t.InvFCFOutCode02
	                                , t.InvFCFOutCode03
	                                , t.InvFCFOutCode04
	                                , t.InvFCFOutCode05
	                                , t.InvFCFOutCode06
	                                , t.InvFCFOutCode07
	                                , t.InvFCFOutCode08
	                                , t.InvFCFOutCode09
	                                , t.InvFCFOutCode10
	                                , t.IF_InvOutNo_Root
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOut t --//[mylock]
                            ");
                        ////
                        string zzzzClauseInsert_InvF_InventoryOutCover_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOut:                                
                                insert into InvF_InventoryOutCover(
	                                IF_InvOutNo
	                                --, InvCodeOutActual
	                                , ProductCodeRoot
	                                , NetworkID
	                                , Qty
	                                , UPOut
	                                , UPOutDesc
	                                , ValOut
	                                , ValOutDesc
	                                , ValOutAfterDesc
	                                , UnitCode
	                                , IF_InvOutStatusCover
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                --, t.InvCodeOutActual
	                                , t.ProductCodeRoot
	                                , t.NetworkID
	                                , t.Qty
	                                , t.UPOut
	                                , t.UPOutDesc
	                                , t.ValOut
	                                , t.ValOutDesc
	                                , t.ValOutAfterDesc
	                                , t.UnitCode
	                                , t.IF_InvOutStatusCover
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutCover t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOut:                                
                                insert into InvF_InventoryOutDtl(
	                                IF_InvOutNo
	                                , InvCodeOutActual
	                                , ProductCodeRoot
	                                , ProductCode
	                                , NetworkID
	                                , Qty
	                                , UnitCode
	                                , IF_InvOutStatusDtl
	                                , Remark
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                , t.InvCodeOutActual
	                                , t.ProductCodeRoot
	                                , t.ProductCode
	                                , t.NetworkID
	                                , t.Qty
	                                , t.UnitCode
	                                , t.IF_InvOutStatusDtl
	                                , t.Remark
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutDtl t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutInstLot_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutInstLot:                                
                                insert into InvF_InventoryOutInstLot(
	                                IF_InvOutNo
	                                , InvCodeOutActual
	                                , ProductCodeRoot
	                                , ProductCode
	                                , ProductLotNo
	                                , NetworkID
	                                , Qty
	                                , IF_InvOutILStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                , t.InvCodeOutActual
	                                , t.ProductCodeRoot
	                                , t.ProductCode
	                                , t.ProductLotNo
	                                , t.NetworkID
	                                , t.Qty
	                                , t.IF_InvOutILStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutInstLot t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutInstSerial_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutInstSerial:                                
                                insert into InvF_InventoryOutInstSerial(
	                                IF_InvOutNo
	                                , InvCodeOutActual
	                                , ProductCodeRoot
	                                , ProductCode
	                                , SerialNo
	                                , NetworkID
	                                , IF_InvOutISStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                , t.InvCodeOutActual
	                                , t.ProductCodeRoot
	                                , t.ProductCode
	                                , t.SerialNo
	                                , t.NetworkID
	                                , t.IF_InvOutISStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutInstSerial t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_InvF_InventoryOutQR_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_InventoryOutQR:                                
                                insert into InvF_InventoryOutQR(
	                                IF_InvOutNo
	                                , QRCode
	                                , NetworkID
	                                , ProductCode
	                                , BoxNo
	                                , CanNo
	                                , QRType
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.IF_InvOutNo
	                                , t.QRCode
	                                , t.NetworkID
	                                , t.ProductCode
	                                , t.BoxNo
	                                , t.CanNo
	                                , t.QRType
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_InventoryOutQR t --//[mylock]
                            ");
                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_InventoryOut_zSave				
								----
								zzzzClauseInsert_InvF_InventoryOutCover_zSave				
								----
								zzzzClauseInsert_InvF_InventoryOutDtl_zSave	
								----
								zzzzClauseInsert_InvF_InventoryOutInstLot_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutInstSerial_zSave			
								----
								zzzzClauseInsert_InvF_InventoryOutQR_zSave			
								----
							"
                            , "zzzzClauseInsert_InvF_InventoryOut_zSave", zzzzClauseInsert_InvF_InventoryOut_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutCover_zSave", zzzzClauseInsert_InvF_InventoryOutCover_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutDtl_zSave", zzzzClauseInsert_InvF_InventoryOutDtl_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutInstLot_zSave", zzzzClauseInsert_InvF_InventoryOutInstLot_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutInstSerial_zSave", zzzzClauseInsert_InvF_InventoryOutInstSerial_zSave
                            , "zzzzClauseInsert_InvF_InventoryOutQR_zSave", zzzzClauseInsert_InvF_InventoryOutQR_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            stopWatchFunc1S.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "SaveDB"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1S.ElapsedMilliseconds", stopWatchFunc1S.ElapsedMilliseconds
                    });
            #endregion

            #region // Call Services:
            {
                //    string strSql = CmUtils.StringUtils.Replace(@"
                //            ---- #input_InvF_InventoryProduct:
                //            select distinct
                //             t.ProductCode
                //             , f.NetworkID
                //            into #input_InvF_InventoryProduct
                //            from InvF_InventoryOutDtl t --//[mylock]
                //             inner join #input_InvF_InventoryOutDtl f --//[mylock]
                //              on t.IF_InvOutNo = f.IF_InvOutNo
                //            where (1=1)
                //            ;

                //            -- select null #input_InvF_InventoryProduct, *  from #input_InvF_InventoryProduct;

                //            ---- #tbl_Mst_Product :
                //            select
                //             t.OrgID
                //             , t.ProductCode
                //            into #tbl_Mst_Product
                //            from Mst_Product t --//[mylock]
                //             inner join #input_InvF_InventoryProduct f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.NetworkID = f.NetworkID
                //            where (1=1)
                //             and t.DTimeUsed is null
                //            ;

                //            -- select null #tbl_Mst_Product, *  from #tbl_Mst_Product;

                //            ---- Update DTimeUsed:
                //            update t
                //            set
                //                t.DTimeUsed = '@dtimeSys'
                //            from Mst_Product t --//[mylock]
                //                inner join #tbl_Mst_Product f --//[mylock]
                //                    on t.OrgID = f.OrgID
                //                        and t.ProductCode = f.ProductCode
                //            where (1=1)
                //            ;

                //            ---- Mst_Product:
                //            select
                //             t.*
                //            from Mst_Product t --//[mylock]
                //             inner join #tbl_Mst_Product f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;


                //            ---- Mst_ProductFiles:
                //            select
                //             t.*
                //            from Mst_ProductFiles t --//[mylock]
                //             inner join #tbl_Mst_Product f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;


                //            ---- Mst_ProductImages:
                //            select
                //             f.*
                //            from #tbl_Mst_Product t --//[mylock]
                //             inner join Mst_ProductImages f --//[mylock]
                //              on t.ProductCode = f.ProductCode
                //               and t.OrgID = f.OrgID
                //            where (1=1)
                //            ;

                //            ---- Clear For Debug:
                //drop table #input_InvF_InventoryProduct;
                //drop table #tbl_Mst_Product;
                //        "
                //        , "@dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                //        );

                //    DataSet ds_Product = _cf.db.ExecQuery(strSql);
                //    DataTable dt_Product = ds_Product.Tables[0].Copy();
                //    DataTable dt_ProductFiles = ds_Product.Tables[1].Copy();
                //    DataTable dt_ProductImages = ds_Product.Tables[2].Copy();

                //    List<Mst_Product> lst_Mst_Product = TUtils.DataTableCmUtils.ToListof<Mst_Product>(dt_Product);
                //    List<Mst_ProductFiles> lst_Mst_ProductFiles = TUtils.DataTableCmUtils.ToListof<Mst_ProductFiles>(dt_ProductFiles);
                //    List<Mst_ProductImages> lst_Mst_ProductImages = TUtils.DataTableCmUtils.ToListof<Mst_ProductImages>(dt_ProductImages);

                //    // Login GetUrl
                //    string strUrlMstSv = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_URL].ToString();
                //    string strAPIUrl = null;

                //    if (dt_Product.Rows.Count > 0)
                //    {
                //        RT_MstSv_Sys_User objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
                //        {
                //            RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                //            {
                //                Tid = strTid,
                //                NetworkID = nNetworkID.ToString(),
                //                OrgID = strOrgID,
                //                GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                //                GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                //                WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                //                WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                //            };

                //            objRT_MstSv_Sys_User = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_MstSv_Sys_User_Login(strUrlMstSv, objRQ_MstSv_Sys_User);
                //            strAPIUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                //        }

                //        //string strAPIUrl = CmUtils.CMyDataSet.GetRemark(dt_GetAPi).ToString();
                //        ////
                //        //string Json = TJson.JsonConvert.SerializeObject(dt_GetAPi);
                //        //string JsonUpdate = null;
                //        {
                //            RT_Mst_Product objRT_Mst_Product = new RT_Mst_Product();
                //            {
                //                RQ_Mst_Product objRQ_Mst_Product = new RQ_Mst_Product()
                //                {
                //                    Lst_Mst_Product = lst_Mst_Product,
                //                    Lst_Mst_ProductFiles = lst_Mst_ProductFiles,
                //                    Lst_Mst_ProductImages = lst_Mst_ProductImages,
                //                    Tid = strTid,
                //                    NetworkID = nNetworkID.ToString(),
                //                    OrgID = strOrgID,
                //                    GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                //                    GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                //                    WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                //                    WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                //                    Ft_Cols_Upd = "Mst_Product.DTimeUsed"
                //                };
                //                //JsonUpdate = TJson.JsonConvert.SerializeObject(objRQ_Mst_Product);
                //                objRT_Mst_Product = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_Mst_Product_UpdateMaster(strAPIUrl, objRQ_Mst_Product);

                //            }
                //        }
                //    }
            }
            #endregion

            #region // Inv_InventoryBalanceFormBlock: => 115
            Stopwatch stopWatchFunc1T = new Stopwatch();
            stopWatchFunc1T.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {

                string strSqlCheck = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.IF_InvOutNo RefNo
	                        , ifio.NetworkID
	                        , 'OUT' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                ///
                if (!bIsDelete)
                {
                    // 20200805.HTTT.Tạo mới/ Sửa phiếu chỉ insert
                    //Inv_InventoryBalanceFormBlock_Delete(
                    //    ref alParamsCoupleError
                    //    , dtimeSys
                    //    , dtDB_Inv_InventoryBalanceFormBlock
                    //    );
                    /////
                    Inv_InventoryBalanceFormBlock_Insert(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
                else
                {
                    Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
            }
            stopWatchFunc1T.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceFormBlock"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1T.ElapsedMilliseconds", stopWatchFunc1T.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            Stopwatch stopWatchFunc1U = new Stopwatch();
            stopWatchFunc1U.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalance:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            stopWatchFunc1U.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Inv_InventoryBalanceFormBlock_ExistAud"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1U.ElapsedMilliseconds", stopWatchFunc1U.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_InvF_InventoryOut_Total: => 223
            Stopwatch stopWatchFunc1V = new Stopwatch();
            stopWatchFunc1V.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryOut_Total"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            //{
            //    /*
            //        Nâng Cấp: -  Nếu FlagIsCheckInvoiceTotal = 1 hoặc null thì hệ thống sẽ Insert Vào DB mà không tính toán
            //                  -  Ngược lại nếu FlagIsCheckInvoiceTotal = 0 thì hệ thống sẽ tính toán rồi Insert vào DB
            //            FlagIsCheckInvoiceTotal = 0 => Không Check
            //            FlagIsCheckInvoiceTotal = 1 => Check
            //            FlagIsCheckInvoiceTotal = null => Check
            //    */
            //    //if (bIsCheckTotal)
            //    {
            //        myCheck_InvF_InventoryOutDtl_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //        ////
            //        myCheck_InvF_InventoryOut_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    }
            //    ////
            //    myCheck_InvF_InventoryOut_TotalQtySerial(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    ////
            //    myCheck_InvF_InventoryOut_TotalQtyLot(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    /////

            //}
            stopWatchFunc1V.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryOut_Total"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1V.ElapsedMilliseconds", stopWatchFunc1V.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Mst_Product_FlagLotOrSerial:
            Stopwatch stopWatchFunc1W = new Stopwatch();
            stopWatchFunc1W.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete)
            {
                ////
                string zzzz_tbl_Mst_Product_Serial = "#tbl_Mst_Product_Serial";
                string zzzz_Mst_Product_LOT = "#tbl_Mst_Product_LOT";
                ////

                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutInstSerial:
                            select distinct
                                k.OrgID
                                , t.ProductCode
                            into zzzz_tbl_Mst_Product_Serial
                            from InvF_InventoryOutInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
	                            inner join InvF_InventoryOut k --//[mylock]
		                            on t.IF_InvOutNo = k.IF_InvOutNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutInstLot:
                            select 
                                k.OrgID
                                , t.ProductCode
                            into zzzz_Mst_Product_LOT
                            from InvF_InventoryOutInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
	                            inner join InvF_InventoryOut k --//[mylock]
		                            on t.IF_InvOutNo = k.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Return:
                            select t.* from zzzz_tbl_Mst_Product_Serial t --//[mylock]
                            select t.* from zzzz_Mst_Product_LOT t --//[mylock]
							"
                        , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                        , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                        );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
                ////
                myCheck_Mst_Product_FlagLotOrSerial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // alParamsCoupleError
                    , zzzz_tbl_Mst_Product_Serial // zzzz_tbl_Mst_Product_Serial
                    , zzzz_Mst_Product_LOT // zzzz_Mst_Product_LOT
                    );
                /////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzz_tbl_Mst_Product_Serial;
						drop table zzzz_Mst_Product_LOT;
					"
                    , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                    , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                    );

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            stopWatchFunc1W.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_Mst_Product_FlagLotOrSerial"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1W.ElapsedMilliseconds", stopWatchFunc1W.ElapsedMilliseconds
                    });
            #endregion

            #region // UserExchangeStatus:
            Stopwatch stopWatchFunc1Y = new Stopwatch();
            stopWatchFunc1Y.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "UserExchangeStatus"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                ////
                string strSqlCheck = CmUtils.StringUtils.Replace(@"               
                        ---- #tbl_InvF_InventoryOutInstSerial:
                        select 
							k.OrgID
							, t.InvCodeOutActual InvCode
							, mp.ProductCodeBase ProductCode
							, t.SerialNo
							, t.LogLUDTimeUTC
							, t.LogLUBy
							, k.IF_InvOutNo
							, t.IF_InvOutISStatus
                        from InvF_InventoryOutInstSerial t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut k --//[mylock]
		                        on t.IF_InvOutNo = k.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on k.OrgID = mp.OrgID
									and t.ProductCode = mp.ProductCode
                        where (1=1)
                        ;

					    ");
                DataTable dtDB_Check = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                ////
                DataTable dtTable_UserExchangeStatus = null;

                UserExchangeStatus_New20200225(
                    ref alParamsCoupleError // lstParamsCoupleError
                    , htParamsSql // htParamsSql
                    , dtDB_Check // lstSource_UserBlock
                    , out dtTable_UserExchangeStatus // dtTable_UserExchangeStatus
                    );
            }
            stopWatchFunc1Y.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "UserExchangeStatus"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1Y.ElapsedMilliseconds", stopWatchFunc1Y.ElapsedMilliseconds
                    });
            #endregion

            #region // InventoryTransaction: => 697
            Stopwatch stopWatchFunc1Z = new Stopwatch();
            stopWatchFunc1Z.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (!bIsDelete && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Inactive))
            {
                InvFInventory_InventoryTransaction_Exec_Block_New20220625(strFunctionName);

                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagCheckBlock, TConst.Flag.No))
                {
                    Inv_InventoryTransaction_Perform_New20220625(
                        ref alParamsCoupleError // lstParamsCoupleError
                        , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                      //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                        , TConst.InventoryTransactionAction.Update // strInventoryTransactionAction
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                        , strWAUserCode // strCreateBy
                        , 0.0 // dblMinQtyTotalOK
                        , int.MinValue // dblMinQtyBlockOK
                        , int.MinValue // dblMinQtyAvailOK
                        , 0.0 // dblMinQtyTotalNG
                        , int.MinValue // dblMinQtyBlockNG
                        , int.MinValue // dblMinQtyAvailNG
                        );

                }
                else
                {
                    Inv_InventoryTransaction_Perform_New20220625(
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
                        );

                }
            }
            stopWatchFunc1Z.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc1Z.ElapsedMilliseconds", stopWatchFunc1Z.ElapsedMilliseconds
                    });
            #endregion

            #region //// Clear For Debug:
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOut;
						drop table #input_InvF_InventoryOutCover;
						drop table #input_InvF_InventoryOutDtl;
						drop table #input_InvF_InventoryOutInstSerial;
						drop table #input_InvF_InventoryOutInstLot;
						drop table #input_InvF_InventoryOutQR;
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
						drop table #input_InvF_InventoryOut;
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

        private void InvF_InventoryOut_ApprX_New20220625(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , ref ArrayList alParamsCoupleSW
            , DateTime dtimeSys
            //// 
            , object objIF_InvOutNo
            , object objFlagXuatCheo
            , object objFlagCallInBrand
            , object objRemark
            , object objFlagNotify
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "InvF_InventoryOut_ApprX";
            //string strErrorCodeDefault = TError.ErrHTCNM.InvF_InventoryOut_ApprX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
                , "objIF_InvOutNo", objIF_InvOutNo
                , "objFlagXuatCheo", objFlagXuatCheo
                , "objFlagCallInBrand", objFlagCallInBrand
                , "objRemark", objRemark
                , "objFlagNotify", objFlagNotify
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input: 
            //drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Refine and Check Input InvF_InventoryOut:

            Stopwatch stopWatchFunc20 = new Stopwatch();
            stopWatchFunc20.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "CheckAndSave"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //// Notify
            //bool bIsNotify = true;//CmUtils.StringUtils.StringEqualIgnoreCase(FlagisNotify, TConst.Flag.Active);
            ////
            string strIF_InvOutNo = TUtils.CUtils.StdParam(objIF_InvOutNo);
            string strRemark = string.Format("{0}", objRemark).Trim();
            string strNetworkID = null;
            string strOrgID = null;
            string strRefNo = null;
            string strRefNoSys = null;
            string strRefType = null;
            string strIF_InvOutNo_Root = "";
            string strFlagXuatCheo = TUtils.CUtils.StdFlag(objFlagXuatCheo);
            string strFlagCallInBrand = TUtils.CUtils.StdFlag(objFlagCallInBrand);
            string strCustomerCodeSys = "";
            string strFlagNotify = TUtils.CUtils.StdFlag(objFlagNotify);
            bool bIsNotify = (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagNotify, TConst.Flag.Yes)); //? true : false;
            ////
            DataTable dtDB_InvF_InventoryOut = null;
            string strFlagBlock = TConst.Flag.No;
            {
                InvF_InventoryOut_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_InvOutNo // objIF_InvOutNo
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.IF_InvOutStatus.Pending // strInvInStatusListToCheck
                    , out dtDB_InvF_InventoryOut // dtDB_InvF_InventoryOut
                    );

                strCustomerCodeSys = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["CustomerCode"]);
                strNetworkID = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["NetworkID"]);
                strOrgID = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["OrgID"]);
                strRefNo = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["RefNo"]);
                strRefNoSys = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["RefNoSys"]);
                strRefType = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["RefType"]);
                strIF_InvOutNo_Root = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["IF_InvOutNo_Root"]);
                ////
            }
            #endregion

            #region // SaveTemp InvF_InventoryOut:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_InventoryOut"
                    , TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "IF_InvOutNo"
                        , "LUDTimeUTC"
                        , "LUBy"
                        , "ApprDTimeUTC"
                        , "ApprBy"
                        , "IF_InvOutStatus"
                        , "Remark"
                        , "LogLUDTimeUTC"
                        , "LogLUBy"
                        }
                    , new object[]{
                            new object[]{
                                strIF_InvOutNo, // IF_InvOutNo
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // ApprDTimeUTC
                                strWAUserCode, // ApprBy
                                TConst.IF_InvOutStatus.Approve, // IF_InvOutStatus
                                strRemark, // Remark
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region //// Save Params Sql:
            if (dtDB_InvF_InventoryOut.Rows.Count > 0 && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            {
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
                        ---- InvF_InventoryOutDtl:
                        select 
							t.*
                        from InvF_InventoryOutDtl t --//[mylock]
                            inner join #input_InvF_InventoryOut f --//[mylock]
                                on t.IF_InvOutNo = f.IF_InvOutNo 
                        where(1=1)
                        ;
					"
                    );

                DataTable dtDB_InvF_InventoryOutDtl = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    ).Tables[0];
                htParamsSql.Add("@strIF_InvOutNo", strIF_InvOutNo);
                htParamsSql.Add("@strRefNo_Type", "INVF_INVOUTINSTSERIAL");
                htParamsSql.Add("@strRefNo_PK", strIF_InvOutNo);
                htParamsSql.Add("@strBlockStatus_N", TConst.BlockStatus.No);
                htParamsSql.Add("@strBlockStatus_S", TConst.BlockStatus.SYS);
                htParamsSql.Add("@strBlockStatus_U", TConst.BlockStatus.User);
                htParamsSql.Add("@strFlagNG_Inactive", TConst.Flag.Inactive);
                for (int nScanDtl = 0; nScanDtl < dtDB_InvF_InventoryOutDtl.Rows.Count; nScanDtl++)
                {
                    {
                        htParamsSql.Add(string.Format("@strInvCode{0}", nScanDtl), dtDB_InvF_InventoryOutDtl.Rows[nScanDtl]["InvCodeOutActual"]);
                    }
                }
                ////
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_InvF_InventoryOut_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.ApprDTimeUTC = f.ApprDTimeUTC
						, t.ApprBy = f.ApprBy
						, t.IF_InvOutStatus = f.IF_InvOutStatus
						, t.Remark = f.Remark
						";
                ////
                string zzB_Update_InvF_InventoryOutCover_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutStatusCover = f.IF_InvOutStatus
						";
                ////
                string zzB_Update_InvF_InventoryOutDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IF_InvOutStatusDtl = f.IF_InvOutStatus
						";
                ////
                string zzB_Update_InvF_InventoryOut_zzE = CmUtils.StringUtils.Replace(@"
						---- InvF_InventoryOut:
						update t
						set 
							zzB_Update_InvF_InventoryOut_ClauseSet_zzE
						from InvF_InventoryOut t --//[mylock]
							inner join #input_InvF_InventoryOut f --//[mylock]
								on t.IF_InvOutNo = f.IF_InvOutNo
						where (1=1)
						;
					"
                    , "zzB_Update_InvF_InventoryOut_ClauseSet_zzE", zzB_Update_InvF_InventoryOut_ClauseSet_zzE
                    );
                ////
                string zzB_Update_InvF_InventoryOutCover_zzE = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_InvF_InventoryOutCover_Temp_Appr: 
                        select 
                            t.IF_InvOutNo
                           -- , t.InvCodeOutActual
                            , t.ProductCodeRoot
                            , f.IF_InvOutStatus
                            , f.LogLUDTimeUTC
                            , f.LogLUBy
                        into #tbl_InvF_InventoryOutCover_Temp_Appr
					    from InvF_InventoryOutCover t --//[mylock]
						    inner join #input_InvF_InventoryOut f --//[mylock]
							    on t.IF_InvOutNo = f.IF_InvOutNo
					    where (1=1)
					    ;

                        ---- Update:
					    update t
					    set 
						    zzB_Update_InvF_InventoryOutCover_ClauseSet_zzE
					    from InvF_InventoryOutCover t --//[mylock]
						    inner join #tbl_InvF_InventoryOutCover_Temp_Appr f --//[mylock]
							    on t.IF_InvOutNo = f.IF_InvOutNo
								    --and t.InvCodeOutActual = f.InvCodeOutActual
								    and t.ProductCodeRoot = f.ProductCodeRoot
					    where (1=1)
					    ;
				    "
                , "zzB_Update_InvF_InventoryOutCover_ClauseSet_zzE", zzB_Update_InvF_InventoryOutCover_ClauseSet_zzE
                );
                ////
                string zzB_Update_InvF_InventoryOutDtl_zzE = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_InvF_InventoryOutDtl_Temp: 
                        select 
                            t.IF_InvOutNo
                            , t.InvCodeOutActual
                            , t.ProductCodeRoot
                            , t.ProductCode
                            , f.IF_InvOutStatus
                            , f.LogLUDTimeUTC
                            , f.LogLUBy
                        into #tbl_InvF_InventoryOutDtl_Temp_Appr
					    from InvF_InventoryOutDtl t --//[mylock]
						    inner join #input_InvF_InventoryOut f --//[mylock]
							    on t.IF_InvOutNo = f.IF_InvOutNo
					    where (1=1)
					    ;

                        ---- Update:
					    update t
					    set 
						    zzB_Update_InvF_InventoryOutDtl_ClauseSet_zzE
					    from InvF_InventoryOutDtl t --//[mylock]
						    inner join #tbl_InvF_InventoryOutDtl_Temp_Appr f --//[mylock]
							    on t.IF_InvOutNo = f.IF_InvOutNo
								    and t.InvCodeOutActual = f.InvCodeOutActual
								    and t.ProductCodeRoot = f.ProductCodeRoot
								    and t.ProductCode = f.ProductCode
					    where (1=1)
					    ;
				    "
                , "zzB_Update_InvF_InventoryOutDtl_ClauseSet_zzE", zzB_Update_InvF_InventoryOutDtl_ClauseSet_zzE
                );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_InvF_InventoryOut_zzE
                        ----
						zzB_Update_InvF_InventoryOutCover_zzE
                        ----
						zzB_Update_InvF_InventoryOutDtl_zzE
                        ----
					"
                    , "zzB_Update_InvF_InventoryOut_zzE", zzB_Update_InvF_InventoryOut_zzE
                    , "zzB_Update_InvF_InventoryOutCover_zzE", zzB_Update_InvF_InventoryOutCover_zzE
                    , "zzB_Update_InvF_InventoryOutDtl_zzE", zzB_Update_InvF_InventoryOutDtl_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }

            stopWatchFunc20.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "CheckAndSave"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc20.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_InvF_InventoryOut_Total:
            Stopwatch stopWatchFunc2A = new Stopwatch();
            stopWatchFunc2A.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryOut_Total"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (!bIsDelete)
            //{
            //    /*
            //        Nâng Cấp: -  Nếu FlagIsCheckInvoiceTotal = 1 hoặc null thì hệ thống sẽ Insert Vào DB mà không tính toán
            //                  -  Ngược lại nếu FlagIsCheckInvoiceTotal = 0 thì hệ thống sẽ tính toán rồi Insert vào DB
            //            FlagIsCheckInvoiceTotal = 0 => Không Check
            //            FlagIsCheckInvoiceTotal = 1 => Check
            //            FlagIsCheckInvoiceTotal = null => Check
            //    */
            //    //if (bIsCheckTotal)
            //    {
            //        myCheck_InvF_InventoryOutDtl_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //        ////
            //        myCheck_InvF_InventoryOut_Total(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , strWAUserCode // strWAUserCode
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    }
            //    ////
            //    myCheck_InvF_InventoryOut_TotalQtySerial(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    ////
            //    myCheck_InvF_InventoryOut_TotalQtyLot(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //            , "#input_InvF_InventoryOut"  //input_InvF_InventoryOut
            //            );
            //    /////

            //}

            stopWatchFunc2A.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "myCheck_InvF_InventoryOut_Total"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc2A.ElapsedMilliseconds", stopWatchFunc2A.ElapsedMilliseconds
                    });
            #endregion

            #region // myCheck_Mst_Product_FlagLotOrSerial:
            //if (!bIsDelete)
            {
                ////
                string zzzz_tbl_Mst_Product_Serial = "#tbl_Mst_Product_Serial";
                string zzzz_Mst_Product_LOT = "#tbl_Mst_Product_LOT";
                ////

                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_InvF_InventoryOutInstSerial:
                            select distinct
                                k.OrgID
                                , t.ProductCode
                            into zzzz_tbl_Mst_Product_Serial
                            from InvF_InventoryOutInstSerial t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
	                            inner join InvF_InventoryOut k --//[mylock]
		                            on t.IF_InvOutNo = k.IF_InvOutNo
                            where (1=1)
                            ;

                            ---- #tbl_InvF_InventoryOutInstLot:
                            select 
                                k.OrgID
                                , t.ProductCode
                            into zzzz_Mst_Product_LOT
                            from InvF_InventoryOutInstLot t --//[mylock]
	                            inner join #input_InvF_InventoryOut f --//[mylock]
		                            on t.IF_InvOutNo = f.IF_InvOutNo
	                            inner join InvF_InventoryOut k --//[mylock]
		                            on t.IF_InvOutNo = k.IF_InvOutNo
                            where (1=1)
                            ;

                            --- Return:
                            select t.* from zzzz_tbl_Mst_Product_Serial t --//[mylock]
                            select t.* from zzzz_Mst_Product_LOT t --//[mylock]
							"
                        , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                        , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                        );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
                ////
                myCheck_Mst_Product_FlagLotOrSerial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // alParamsCoupleError
                    , zzzz_tbl_Mst_Product_Serial // zzzz_tbl_Mst_Product_Serial
                    , zzzz_Mst_Product_LOT // zzzz_Mst_Product_LOT
                    );
                /////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzz_tbl_Mst_Product_Serial;
						drop table zzzz_Mst_Product_LOT;
					"
                    , "zzzz_tbl_Mst_Product_Serial", zzzz_tbl_Mst_Product_Serial
                    , "zzzz_Mst_Product_LOT", zzzz_Mst_Product_LOT
                    );

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            #region // Serial tồn tại trong kho:
            Stopwatch stopWatchFunc2B = new Stopwatch();
            stopWatchFunc2B.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceSerial_CheckListSerialAndInvCodeExist"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            {
                string strSqlCheck = CmUtils.StringUtils.Replace(@"  
                        ---- #input_InvF_InventoryInInstSerial_Distinct_InvCodeInActual_ProductCode:
                        select distinct 
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.SerialNo
                        from InvF_InventoryOutInstSerial t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on ifio.OrgID = mp.OrgID
			                        and t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
					");
                DataTable dtInput_Inv_InventoryBalanceSerial = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                Inv_InventoryBalanceSerial_CheckListSerialAndInvCodeExist(
                    ref alParamsCoupleError // alParamsCoupleError
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , dtInput_Inv_InventoryBalanceSerial // dtInput_Inv_InventoryBalanceSerial
                    );
                //////

            }
            stopWatchFunc2B.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Inv_InventoryBalanceSerial_CheckListSerialAndInvCodeExist"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2B.ElapsedMilliseconds
                    });
            #endregion

            #region // InventoryTransaction:
            Stopwatch stopWatchFunc2C = new Stopwatch();
            stopWatchFunc2C.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            {

                InvFInventory_InventoryTransaction_Exec_Delete_New20220625(strFunctionName, htParamsSql);

                if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagBlock, TConst.Flag.No))
                {
                    Inv_InventoryTransaction_Perform_New20220625(
                        ref alParamsCoupleError // lstParamsCoupleError
                        , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                      //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                        , TConst.InventoryTransactionAction.Delete // strInventoryTransactionAction
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                        , strWAUserCode // strCreateBy
                        , 0.0 // dblMinQtyTotalOK
                        , int.MinValue // dblMinQtyBlockOK
                        , int.MinValue // dblMinQtyAvailOK
                        , 0.0 // dblMinQtyTotalNG
                        , int.MinValue // dblMinQtyBlockNG
                        , int.MinValue // dblMinQtyAvailNG
                        );

                }
                else
                {
                    Inv_InventoryTransaction_Perform_New20220625(
                        ref alParamsCoupleError // lstParamsCoupleError
                        , "#tbl_InventoryTransaction" // strTableNameDBTemp
                                                      //, "#tbl_Inv_InventoryBalanceSerial" // strTableNameDBTempDtl
                        , TConst.InventoryTransactionAction.Delete // strInventoryTransactionAction
                        , dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") // strCreateDTime
                        , strWAUserCode // strCreateBy
                        , 0.0 // dblMinQtyTotalOK
                        , int.MinValue // dblMinQtyBlockOK
                        , 0.0 // dblMinQtyAvailOK
                        , 0.0 // dblMinQtyTotalNG
                        , int.MinValue // dblMinQtyBlockNG
                        , 0.0 // dblMinQtyAvailNG
                        );
                }
            }

            stopWatchFunc2C.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InventoryTransaction"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2C.ElapsedMilliseconds
                    });
            #endregion

            #region // Inv_InventoryBalanceFormBlock:
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            {

                string strSqlCheck = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalanceFormBlock:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
	                        , t.IF_InvOutNo RefNo
	                        , ifio.NetworkID
	                        , 'OUT' RefType
	                        , f.LogLUDTimeUTC
	                        , f.LogLUBy
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_InventoryBalanceFormBlock = _cf.db.ExecQuery(
                    strSqlCheck
                    ).Tables[0];
                ////
                {
                    Inv_InventoryBalanceFormBlock_Delete(
                        ref alParamsCoupleError
                        , dtimeSys
                        , dtDB_Inv_InventoryBalanceFormBlock
                        );

                }
            }
            #endregion 

            #region // myCheck_Inv_InventoryBalanceFormBlock_ExistAud:
            if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- Inv_InventoryBalance:
                        select distinct
	                        ifio.OrgID
	                        , t.InvCodeOutActual InvCode
	                        , mp.ProductCodeBase ProductCode
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join #input_InvF_InventoryOut f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
	                        inner join InvF_InventoryOut ifio --//[mylock]
		                        on f.IF_InvOutNo = ifio.IF_InvOutNo
	                        inner join Mst_Product mp --//[mylock]
		                        on t.ProductCode = mp.ProductCode
                        where(1=1)
                        ;
                    ");
                ////
                DataTable dtDB_Inv_Inv_InventoryBalance = _cf.db.ExecQuery(
                    strSqlBuild
                    ).Tables[0];
                ////
                myCheck_Inv_InventoryBalanceFormBlock_ExistAud(
                    ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Inv_Inv_InventoryBalance
                    );
            }
            #endregion

            #region // InvF_WarehouseCard:
            Stopwatch stopWatchFunc2D = new Stopwatch();
            stopWatchFunc2D.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_WarehouseCard"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            DataTable dtBuild_InvF_WarehouseCard = null;
            string strIF_InvAudNo = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["IF_InvAudNo"]);
            //if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No))
            //{
            //    strIF_InvAudNo = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["IF_InvAudNo"]);
            //    string strInventoryAction = TConst.InventoryAction.Out;

            //    if (!string.IsNullOrEmpty(strIF_InvAudNo)) strInventoryAction = TConst.InventoryAction.AuditOut;
            //    ////
            //    Stopwatch stopWatchFunc2D1 = new Stopwatch();
            //    stopWatchFunc2D1.Start();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_InventoryOut_Build_InvF_WarehouseCard"
            //        , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        });
            //    InvF_InventoryOut_Build_InvF_WarehouseCard(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // alParamsCoupleError
            //        , strInventoryAction // alParamsCoupleError
            //        , dtDB_InvF_InventoryOut // dtDB_InvF_InventoryOut
            //        , out dtBuild_InvF_WarehouseCard // dtBuild_InvF_WarehouseCard
            //        );
            //    stopWatchFunc2D1.Stop();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_InventoryOut_Build_InvF_WarehouseCard"
            //        , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2D1.ElapsedMilliseconds
            //        });
            //    /////
            //    Stopwatch stopWatchFunc2D2 = new Stopwatch();
            //    stopWatchFunc2D2.Start();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_WarehouseCard_Insert"
            //        , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        });
            //    InvF_WarehouseCard_Insert(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , dtBuild_InvF_WarehouseCard // dtBuild_InvF_WarehouseCard
            //        );
            //    stopWatchFunc2D2.Stop();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_WarehouseCard_Insert"
            //        , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2D2.ElapsedMilliseconds
            //        });
            //    /////
            //    DataTable dt_Build_InvF_InventoryBalanceCard = null;

            //    Stopwatch stopWatchFunc2D3 = new Stopwatch();
            //    stopWatchFunc2D3.Start();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "Build_InvF_InventoryBalanceCard"
            //        , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        });
            //    Build_InvF_InventoryBalanceCard(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , strIF_InvOutNo // strIF_InvInNo
            //        , out dt_Build_InvF_InventoryBalanceCard // dt_Build_InvF_InventoryBalanceCard
            //        );
            //    stopWatchFunc2D3.Stop();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "Build_InvF_InventoryBalanceCard"
            //        , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2D3.ElapsedMilliseconds
            //        });
            //    ////
            //    Stopwatch stopWatchFunc2D4 = new Stopwatch();
            //    stopWatchFunc2D4.Start();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_InventoryBalanceCard_Insert"
            //        , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        });
            //    InvF_InventoryBalanceCard_Insert(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //        , dt_Build_InvF_InventoryBalanceCard // dt_Build_InvF_InventoryBalanceCard
            //        );
            //    stopWatchFunc2D4.Stop();
            //    alParamsCoupleSW.AddRange(new object[]{
            //        "strFunctionName", "InvF_InventoryBalanceCard_Insert"
            //        , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //        , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2D4.ElapsedMilliseconds
            //        });
            //    ////
            //}
            //if (CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.Active))
            //{
            //    strIF_InvAudNo = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["IF_InvAudNo"]);
            //    string strInventoryAction = TConst.InventoryAction.Out;

            //    if (!string.IsNullOrEmpty(strIF_InvAudNo)) strInventoryAction = TConst.InventoryAction.AuditOut;

            //    InvF_InventoryOut_Build_InvF_WarehouseCard_XuatCheo(
            //        ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // alParamsCoupleError
            //        , strInventoryAction // alParamsCoupleError
            //        , dtDB_InvF_InventoryOut // dtDB_InvF_InventoryOut
            //        );
            //    /////
            //}
            stopWatchFunc2D.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "InvF_WarehouseCard"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2D.ElapsedMilliseconds
                    });
            #endregion

            #region // OSInBrand_Inv_InventoryVerifiedID_OutInv:
            Stopwatch stopWatchFunc2F = new Stopwatch();
            stopWatchFunc2F.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "RegionsAfter"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            if (string.IsNullOrEmpty(strIF_InvAudNo) && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No)
                && string.IsNullOrEmpty(strIF_InvAudNo) && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagCallInBrand, TConst.Flag.Yes))
            {
                DataSet dsData_Inv_InventoryVerifiedID = new DataSet();
                {
                    #region // Refine and check Input:
                    string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                    strOrgID = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["OrgID"]);
                    string strCustomerCode = TUtils.CUtils.StdParam(dtDB_InvF_InventoryOut.Rows[0]["CustomerCode"]);
                    string strFormOutType = "OUTNCC";
                    string strPlateNo = "";
                    string strMoocNo = "";
                    string strDriverName = "";
                    string strDriverPhoneNo = "";
                    string strOrgID_Customer = strOrgID;
                    string strCustomerName = "";
                    string strCustomerAddress = "";
                    DataTable dtDB_Mst_Customer = null;
                    {
                        Mst_Customer_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , strOrgID // strOrgID
                            , strCustomerCode // strCustomerCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Mst_Customer // dtDB_Mst_Customer
                            );
                        /////
                        strCustomerName = string.Format("{0}", dtDB_Mst_Customer.Rows[0]["CustomerName"]).Trim();
                        strCustomerAddress = string.Format("{0}", dtDB_Mst_Customer.Rows[0]["CustomerAddress"]).Trim();
                    }
                    #endregion

                    #region // Get AccessToken:
                    // Hien tai chua NC SOS nen goi ham de lay AccessToken. Khi nao NC SOS thi bo di.
                    //string strAccessToken = null;
                    //{
                    //    OS_MstSv_Sys_User_GetAccessToken(
                    //        strTid // strTid
                    //        , strGwUserCode // strGwUserCode
                    //        , strGwPassword // strGwPassword
                    //        , strWAUserCode // strWAUserCode
                    //        , strWAUserPassword // strWAUserPassword
                    //        , nNetworkID.ToString() // nNetworkID
                    //        , strOrgID // strOrgID
                    //                   //, strAccessToken // strAccessToken
                    //        , ref alParamsCoupleError // alParamsCoupleError
                    //                                  ////
                    //        , dtimeSys // dtimeSys
                    //                   //, strMST // strMST
                    //                   ////
                    //        , out strAccessToken
                    //        ////
                    //        );
                    //}
                    #endregion

                    #region // Build Input: 
                    {
                        string strSql_Check = CmUtils.StringUtils.Replace(@"
                                --- Return:
                                select 
	                                t.QRCode IDNo
                                from InvF_InventoryOutQR t --//[mylock]
	                                inner join #input_InvF_InventoryOut f --//[mylock]
		                                on t.IF_InvOutNo = f.IF_InvOutNo
                                where(1=1)
									and t.QRType = 'TEM'
                                ;

                                --- Return:
                                select 
	                                t.QRCode IDNo
                                from InvF_InventoryOutQR t --//[mylock]
	                                inner join #input_InvF_InventoryOut f --//[mylock]
		                                on t.IF_InvOutNo = f.IF_InvOutNo
                                where(1=1)
									and t.QRType = 'BOX'
                                ;

                                --- Return:
                                select 
	                                t.QRCode IDNo
                                from InvF_InventoryOutQR t --//[mylock]
	                                inner join #input_InvF_InventoryOut f --//[mylock]
		                                on t.IF_InvOutNo = f.IF_InvOutNo
                                where(1=1)
									and t.QRType = 'CARTON'
                                ;
                            "
                            );
                        //
                        dsData_Inv_InventoryVerifiedID = _cf.db.ExecQuery(strSql_Check);
                        dsData_Inv_InventoryVerifiedID.Tables[0].TableName = "Inv_InventoryVerifiedID";
                        dsData_Inv_InventoryVerifiedID.Tables[1].TableName = "Inv_InventoryGenBox";
                        dsData_Inv_InventoryVerifiedID.Tables[2].TableName = "Inv_InventoryGenCan";
                        ////

                    }
                    #endregion

                    #region // OSInBrand_Inv_InventoryVerifiedID_OutInv:
                    /////
                    if (dsData_Inv_InventoryVerifiedID.Tables[0].Rows.Count > 0
                        || dsData_Inv_InventoryVerifiedID.Tables[1].Rows.Count > 0
                        || dsData_Inv_InventoryVerifiedID.Tables[2].Rows.Count > 0)
                    {
                        OSInBrand_Inv_InventoryVerifiedID_OutInv(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                                            //, strWAUserPassword // strWAUserPassword
                            , nNetworkID.ToString() // nNetworkID
                            , strOrgID // strOrgID
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                                                      ////
                            , dtimeSys // dtimeSys
                            , strMST // strMST
                                     ////
                            , strIF_InvOutNo // strIF_InvOutNo
                            , strFormOutType // strFormOutType
                            , strPlateNo // strPlateNo
                            , strMoocNo // strMoocNo
                            , strDriverName // strDriverName
                            , strDriverPhoneNo // strDriverPhoneNo
                            , strCustomerCode // strCustomerCode
                            , strOrgID_Customer // strOrgID_Customer
                            , strCustomerName // strCustomerName
                            , strCustomerAddress // strCustomerAddress
                            , strRemark
                            /////
                            , dsData_Inv_InventoryVerifiedID
                            ////
                            );

                    }
                    #endregion
                }
            }
            #endregion

            #region // OSDMS_InvF_InventoryOut_UpdQtyOrder:
            if (!string.IsNullOrEmpty(strRefNo)
                && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No)
                && !CmUtils.StringUtils.StringEqualIgnoreCase(strRefType, TConst.RefType.RO)
                && !CmUtils.StringUtils.StringEqualIgnoreCase(strRefType, TConst.RefType.InvOut))
            {
                DataSet dsData_InvF_InventoryOut = new DataSet();
                {
                    #region // Refine and check Input:
                    string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                    string strInvOutDate = TUtils.CUtils.StdDate(dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"));
                    string strLQDMSNetworkID = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.INVENTORY_LQDMS_NETWORKID]);
                    bool bIsCallLQDMS = CmUtils.StringUtils.StringEqual(strNetworkID, strLQDMSNetworkID);
                    #endregion

                    #region // Build Input: 
                    {
                        string strSql_Check = CmUtils.StringUtils.Replace(@"
                            --- #tbl_InvF_InventoryOutDtl_BOM_Filter:
							select
								t.IF_InvOutNo
								, t.ProductCodeRoot
								, t.ProductCode
								, g.OrgID
								, (
									case
										when (t.Qty/h.Qty) is null then 0
										else (t.Qty/h.Qty)
									end
								) Qty
							into #tbl_InvF_InventoryOutDtl_BOM_Filter
                            from InvF_InventoryOutDtl t --//[mylock]
								inner join #input_InvF_InventoryOut f --//[mylock]
									on t.IF_InvOutNo = f.IF_InvOutNo
								inner join InvF_InventoryOut g --//[mylock]
									on t.IF_InvOutNo = g.IF_InvOutNo
								left join Mst_Product k --//[mylock]
									on t.ProductCodeRoot = k.ProductCode
								left join Prd_BOM h --//[mylock]
									on t.ProductCode = h.ProductCode
							where(1=1)
								and k.ProductType = 'COMBO'
							;

							--- #tbl_InvF_InventoryOutDtl_BOM:
							select
								t.IF_InvOutNo
								, t.OrgID
								, t.ProductCodeRoot ProductCode
								, t.Qty
							into #tbl_InvF_InventoryOutDtl_BOM
							from #tbl_InvF_InventoryOutDtl_BOM_Filter t --//[mylock]
							where(1=1)
							group by 
								t.IF_InvOutNo
								, t.OrgID
								, t.ProductCodeRoot
								, t.Qty
							;

							--- #tbl_InvF_InventoryOutDtl_Product:
							select
								t.IF_InvOutNo
								, g.OrgID
								, t.ProductCode
								, sum(t.Qty) Qty
							into #tbl_InvF_InventoryOutDtl_Product
                            from InvF_InventoryOutDtl t --//[mylock]
								inner join #input_InvF_InventoryOut f --//[mylock]
									on t.IF_InvOutNo = f.IF_InvOutNo
								inner join InvF_InventoryOut g --//[mylock]
									on t.IF_InvOutNo = g.IF_InvOutNo
								left join Mst_Product k --//[mylock]
									on t.ProductCodeRoot = k.ProductCode
								--left join Prd_BOM h --//[mylock]
									--on t.ProductCode = h.ProductCode
							where(1=1)
								and k.ProductType = 'PRODUCT'
							group by 
								t.IF_InvOutNo
								, g.OrgID
								, t.ProductCode
							;

							--- Return:
							select
								t.*
							into #tbl_InvF_InventoryInDtl_Filter
							from #tbl_InvF_InventoryOutDtl_Product t --//[mylock]
							where(1=1)
							union
							select
								t.*
							from #tbl_InvF_InventoryOutDtl_BOM t --//[mylock]
							where(1=1)
							;

							-- select * from #tbl_InvF_InventoryInDtl_Filter;

                            ---- #tbl_InvF_InventoryInDtl:
                            select  
	                            f.RefNoSys OrderMixNoSys
								, f.RefType OrderMixType
								, f.OrgID
								, t.ProductCode
								, t.Qty
                            from #tbl_InvF_InventoryInDtl_Filter t --//[mylock]
								inner join InvF_InventoryOut f --//[mylock]
									on t.IF_InvOutNo = f.IF_InvOutNo
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutDtl_BOM_Filter;
                            drop table #tbl_InvF_InventoryOutDtl_BOM;
                            drop table #tbl_InvF_InventoryOutDtl_Product;
                        "
                            );
                        //
                        dsData_InvF_InventoryOut = _cf.db.ExecQuery(strSql_Check);
                        dsData_InvF_InventoryOut.Tables[0].TableName = "InvF_InventoryOutDtl";
                        ////

                    }
                    #endregion

                    #region // OSDMS_InvF_InventoryOut_UpdQtyOrder:
                    if (bIsCallLQDMS)
                    {
                        // OSLQDMS_InvF_InventoryOut_UpdQtyOrder:
                        OSLQDMS_InvF_InventoryOut_UpdQtyOrder(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                                            //, strWAUserPassword // strWAUserPassword
                            , strNetworkID // nNetworkID
                            , strOrgID // strOrgID
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                                                      ////
                            , dtimeSys // dtimeSys
                            , strMST // strMST
                                     ////
                            , strIF_InvOutNo // objIF_InvOutNo
                            , strInvOutDate // objInvOutDate
                            , strRefNo // objOrderNo
                                       /////
                            , dsData_InvF_InventoryOut
                            ////
                            );
                    }
                    else
                    {
                        // OSDMS_InvF_InventoryOut_UpdQtyOrder:
                        OSDMS_InvF_InventoryOut_UpdQtyOrder(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                                            //, strWAUserPassword // strWAUserPassword
                            , strNetworkID // nNetworkID
                            , strOrgID // strOrgID
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                                                      ////
                            , dtimeSys // dtimeSys
                            , strMST // strMST
                                     ////
                            , strIF_InvOutNo // objIF_InvOutNo
                            , strInvOutDate // objInvOutDate
                            , strRefNo // objOrderNo
                                       /////
                            , dsData_InvF_InventoryOut
                            ////
                            );
                    }
                    #endregion
                }
            }
            #endregion

            #region // OSVeloca_SerRO_InvF_Transaction_AddQtyMix:
            if (!string.IsNullOrEmpty(strRefNo)
                && CmUtils.StringUtils.StringEqualIgnoreCase(strFlagXuatCheo, TConst.Flag.No)
                && CmUtils.StringUtils.StringEqualIgnoreCase(strRefType, TConst.RefType.RO)
                && !CmUtils.StringUtils.StringEqualIgnoreCase(strRefType, TConst.RefType.InvOut))
            {
                DataSet dsData_InvF_InventoryOut = new DataSet();
                {
                    #region // Refine and check Input:
                    string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                    string strInvOutDate = TUtils.CUtils.StdDate(dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"));
                    #endregion

                    #region // Build Input: 
                    {
                        string strSql_Check = CmUtils.StringUtils.Replace(@"
                            --- #tbl_InvF_InventoryOutDtl_Filter:
                            select 
								g.OrgID
	                            , t.IF_InvOutNo
	                            , t.ProductCode
	                            , sum(t.Qty) Qty
                            into #tbl_InvF_InventoryOutDtl_Filter
                            from InvF_InventoryOutDtl t --//[mylock]
								inner join #input_InvF_InventoryOut f --//[mylock]
									on t.IF_InvOutNo = f.IF_InvOutNo
								inner join InvF_InventoryOut g --//[mylock]
									on t.IF_InvOutNo = g.IF_InvOutNo
                            where(1=1)
                            group by 
								g.OrgID
	                            , t.IF_InvOutNo
	                            , t.ProductCode
                            ;

                            ---- #tbl_InvF_InventoryInDtl:
                            select  
	                            f.RefNoSys OrderMixNoSys
								, f.RefType OrderMixType
								, f.OrgID
								, t.ProductCode ProductCode
								, 'OUT' RefType
								, f.IF_InvOutNo RefCode00
								, t.Qty
                            from #tbl_InvF_InventoryOutDtl_Filter t --//[mylock]
								inner join InvF_InventoryOut f --//[mylock]
									on t.IF_InvOutNo = f.IF_InvOutNo
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_InvF_InventoryOutDtl_Filter;
                        "
                            );
                        //
                        dsData_InvF_InventoryOut = _cf.db.ExecQuery(strSql_Check);
                        dsData_InvF_InventoryOut.Tables[0].TableName = "SerRO_InvF_Transaction";
                        ////

                    }
                    #endregion

                    #region // OSVeloca_SerRO_InvF_Transaction_AddQtyMix:
                    OSVeloca_SerRO_InvF_Transaction_AddQtyMix(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , strNetworkID // nNetworkID
                        , strOrgID // strOrgID
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , dtimeSys // dtimeSys
                        , strMST // strMST
                                 ////
                        , strIF_InvOutNo // objIF_InvOutNo
                        , strInvOutDate // objInvOutDate
                        , strRefNoSys // objOrderNo
                                      /////
                        , dsData_InvF_InventoryOut
                        ////
                        );
                    #endregion
                }
            }
            stopWatchFunc2F.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "RegionsAfter"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2F.ElapsedMilliseconds
                    });
            #endregion

            #region // Notify:
            Stopwatch stopWatchFunc2J = new Stopwatch();
            stopWatchFunc2J.Start();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Notify"
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            //if (bIsNotify) // Cờ gửi Notify
            //{
            //    string strSql_InvF_InventoryOut = CmUtils.StringUtils.Replace(@"
            //            ---- Get InvF_InventoryOut:
            //            select
	           //             t.IF_InvOutNo
	           //             , t.CustomerCode
	           //             , f.CustomerName
            //                , t.InvFCFOutCode03 -- Biển số xe
            //            from InvF_InventoryOut t --//[mylock]
            //                inner join Mst_Customer f --//[mylock]
	           //                 on t.CustomerCode = f.CustomerCodeSys
            //            where (1=1)
            //                and t.IF_InvOutNo = '@strIF_InvOutNo'
            //            ;
            //        "
            //        , "@strIF_InvOutNo", strIF_InvOutNo
            //        );

            //    DataTable dt_InvF_InventoryOut = _cf.db.ExecQuery(strSql_InvF_InventoryOut).Tables[0];

            //    if (dt_InvF_InventoryOut.Rows.Count > 0)
            //    {
            //        string strCustomerCode = dt_InvF_InventoryOut.Rows[0]["CustomerCode"].ToString(); // Mã khách hàng
            //        string strCustomerName = dt_InvF_InventoryOut.Rows[0]["CustomerName"].ToString(); // Tên khách hàng
            //        string strBSX = dt_InvF_InventoryOut.Rows[0]["InvFCFOutCode03"].ToString(); // Biển số xe

            //        Stopwatch stopWatchFunc2J1 = new Stopwatch();
            //        stopWatchFunc2J1.Start();
            //        alParamsCoupleSW.AddRange(new object[]{
            //            "strFunctionName", "strSql_GetALLProductInInvOut"
            //            , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //            });
            //        //// Thông tin Sản phẩm
            //        string strSql_GetALLProductInInvOut = CmUtils.StringUtils.Replace(@"
            //                ---- #tbl_InvF_InventoryOutDtl_Search
            //                --select
	           //             --    t.IF_InvOutNo
	           //             --    , t.ProductCode
	           //             --    , f.OrgID
            //                --    , t.Qty
            //                --    , t.UnitCode
            //                --into #tbl_InvF_InventoryOutDtl_Search
            //                --from InvF_InventoryOutDtl t --//[mylock]
            //                --    inner join InvF_InventoryOut f --//[mylock]
	           //             --       on t.IF_InvOutNo = f.IF_InvOutNo
            //                --where (1=1)
            //                --    and t.IF_InvOutNo = '@strIF_InvOutNo'
            //                --;
            //                select
	           //                 t.IF_InvOutNo
	           //                 , t.ProductCode
	           //                 , iio.OrgID
            //                    , t.Qty
            //                    , t.UnitCode
            //                into #tbl_InvF_InventoryOutDtl_Search
            //                from #input_InvF_InventoryOut f --//[mylock]
            //                    inner join InvF_InventoryOut iio --//[mylock]
	           //                    on f.IF_InvOutNo = iio.IF_InvOutNo
            //                    inner join InvF_InventoryOutDtl t --//[mylock]
	           //                    on f.IF_InvOutNo = t.IF_InvOutNo
            //                where (1=1)
            //                ;
            //                ----
            //                select count(0) QtyProduct from #tbl_InvF_InventoryOutDtl_Search;
            //                ----
            //                select --top 1
	           //                 f.*
	           //                 , t.ValConvert
            //                    , t.ProductName
	           //                 , mp.UnitCode
            //                    , mp.UnitCode UnitCodeBase
            //                    , ROUND( (f.Qty * t.ValConvert), 3) QtyBase
            //                from Mst_Product t --//[mylock]
            //                    inner join #tbl_InvF_InventoryOutDtl_Search f --//[mylock]
	           //                    on t.OrgID = f.OrgID
		          //                    and t.ProductCode = f.ProductCode
            //                    left join Mst_Product mp --//[mylock]
	           //                    on t.OrgID = mp.OrgID
		          //                    and t.ProductCodeBase = mp.ProductCode
            //                where (1=1)
            //                ;

            //                drop table #tbl_InvF_InventoryOutDtl_Search
            //            "
            //            , "@strIF_InvOutNo", strIF_InvOutNo
            //            );

            //        DataSet dsData = _cf.db.ExecQuery(strSql_GetALLProductInInvOut);

            //        stopWatchFunc2J1.Stop();
            //        alParamsCoupleSW.AddRange(new object[]{
            //            "strFunctionName", "strSql_GetALLProductInInvOut"
            //            , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
            //            , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2J1.ElapsedMilliseconds
            //            });
            //        int CountProduct = Convert.ToInt32(dsData.Tables[0].Rows[0]["QtyProduct"]); // Số hàng hóa trong Phiếu
            //        //string strProductName = "";//dsData.Tables[1].Rows[0]["ProductName"].ToString(); // Tên hàng hóa đầu tiên
            //        //string strUnitCode = dsData.Tables[1].Rows[0]["UnitCode"].ToString(); // Tên đơn vị Base
            //        //string strValConvert = "";// dsData.Tables[1].Rows[0]["ValConvert"].ToString(); // Giá trị đã quy đổi ra Base
            //        //string strDot = "";
            //        string strListProduct = "";
            //        //// 
            //        #region // Nội dung thông báo cũ:
            //        //if (CountProduct > 1)
            //        //{
            //        //    strDot = "...";
            //        //}
            //        ////
            //        // Nội dung của Chi Nhánh:
            //        //string strMessage_Org = CmUtils.StringUtils.Replace(
            //        //        @"Đã xuất kho cho <CustomerName>; Biển số xe: <BSX>; Thời gian:<Time>"
            //        //    , "<CustomerName>", strCustomerName
            //        //    , "<BSX>", strBSX
            //        //    , "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss") // ApprDTimeUTC
            //        //    ).Trim(); // Tên KH(CustomerName) || Biển số xe(BSX) ||

            //        ////// Nội dung của Đại lý
            //        //string strMessage_Customer = CmUtils.StringUtils.Replace(
            //        //        @"Đơn hàng <PrdName>: <SL> <UnitCode><...> được xuất kho lúc <Time>, biển số xe: <BSX>"
            //        //    , "<CustomerName>", strCustomerName
            //        //    , "<PrdName>", strProductName
            //        //    , "<SL>", strValConvert
            //        //    , "<UnitCode>", strUnitCode
            //        //    , "<BSX>", strBSX
            //        //    , "<...>", strDot
            //        //    , "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss") // ApprDTimeUTC
            //        //    ).Trim(); // Tên KH(CustomerName) || Biển số xe(BSX) || 
            //        #endregion

            //        // Tạo nội dung các sản phẩm
            //        for (int nScan = 0; nScan < dsData.Tables[1].Rows.Count; nScan++)
            //        {
            //            DataRow drScan = dsData.Tables[1].Rows[nScan];

            //            //< Tên hàng hóa 1 >: < SL dự kiến> Tấn
            //            //< Tên hàng hóa 2 >: < SL dự kiến> Tấn
            //            string strProductName = TUtils.CUtils.StrValueOrNull(drScan["ProductName"]); // Tên hàng hóa đầu tiên
            //            //string strValConvert = TUtils.CUtils.StrValueOrNull(drScan["Qty"]);
            //            //string strUnitCode = TUtils.CUtils.StrValueOrNull(drScan["UnitCode"]); // Tên đơn vị Base
            //            string strValConvert = TUtils.CUtils.StrValueOrNull(drScan["QtyBase"]);
            //            string strUnitCode = TUtils.CUtils.StrValueOrNull(drScan["UnitCodeBase"]); // Tên đơn vị Base

            //            strListProduct += CmUtils.StringUtils.Replace(
            //                "\n" + "<@strProductName>: <@strValConvert> <@strUnitCode>"
            //                , "<@strProductName>", strProductName
            //                , "<@strValConvert>", strValConvert
            //                , "<@strUnitCode>", strUnitCode
            //                );
            //        }

            //        // 2021-03-12 Anh Hoàng với Mẫn yêu cầu sửa Notify
            //        string NewMesseage = CmUtils.StringUtils.Replace(
            //            "<Time> \n" // Thời điểm
            //            + "Đã xuất kho cho <CustomerName>: \n" // Khách hàng
            //            + "<Product> \n" // Product
            //            + "Biển số xe: <BSX>"
            //            , "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss")
            //            , "<CustomerName>", strCustomerName
            //            , "<Product>", strListProduct.Trim()
            //            , "<BSX>", strBSX
            //            )
            //            ;

            //        string strSql_Sys_User = CmUtils.StringUtils.Replace(@"
            //                ---- :
            //                select
            //                    t.UserCode
            //                from Map_UserInNotifyType t --//[mylock]
            //                    inner join Sys_User su --//[mylock]
            //                        on t.UserCode = su.UserCode
            //                where (1=1)
            //                    and t.NotifyType = '@strNotifyType'
            //                    and t.FlagNotify = '@FlagNotify'
            //                    and su.CustomerCodeSys = '@CustomerCodeSys'
            //                "
            //            , "@strNotifyType", TConst.NotifyType.StockOutDL
            //            , "@FlagNotify", TConst.Flag.Active
            //            , "@CustomerCodeSys", strCustomerCode
            //            );

            //        //DataTable dt_Sys_User = _cf.db.ExecQuery(strSql_Sys_User).Tables[0];

            //        List<InosUser> lstInosUser_Customer = new List<InosUser>();

            //        //for (int nScan = 0; nScan < dt_Sys_User.Rows.Count; nScan++)
            //        //{
            //        //    DataRow drScan = dt_Sys_User.Rows[nScan];
            //        //    ////
            //        //    InosUser objInosUser = new InosUser()
            //        //    {
            //        //        Email = drScan["UserCode"].ToString()
            //        //    };

            //        //    lstInosUser_Customer.Add(objInosUser);
            //        //}

            //        List<InosUser> lstInosUser_Org = new List<InosUser>();
            //        Inos_Notification_SendX(
            //            strOrgID
            //            , TConst.NotifyTitle.TitleInventory
            //            , NewMesseage
            //            , TConst.NotifyType.ApStockOut
            //            , TConst.NotifySubType.INV_OUT
            //            , strIF_InvOutNo
            //            , ""
            //            , strAccessToken
            //            , lstInosUser_Org
            //            );

            //        Inos_Notification_SendX(
            //            strOrgID
            //            , TConst.NotifyTitle.TitleInventory
            //            , NewMesseage
            //            , TConst.NotifyType.StockOutDL
            //            , TConst.NotifySubType.INV_OUT
            //            , strIF_InvOutNo
            //            , strCustomerCodeSys
            //            , strAccessToken
            //            , lstInosUser_Customer
            //            );
            //    }
            //}

            stopWatchFunc2J.Stop();
            alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", "Notify"
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc2J.ElapsedMilliseconds
                    });
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_InventoryOut; 
						drop table #tbl_InvF_InventoryOutCover_Temp_Appr;
						drop table #tbl_InvF_InventoryOutDtl_Temp_Appr;
						drop table #tbl_Mst_NNT_ViewAbility;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

        }
        #endregion
    }
}
