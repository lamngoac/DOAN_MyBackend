using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

using CmUtils = CommonUtils;
using TDAL = EzDAL.MyDB;
using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.BizService.Services;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Data;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Mst_ProductGroup:
        public DataSet WAS_Mst_ProductGroup_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ProductGroup objRQ_Mst_ProductGroup
            ////
            , out RT_Mst_ProductGroup objRT_Mst_ProductGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ProductGroup.Tid;
            objRT_Mst_ProductGroup = new RT_Mst_ProductGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ProductGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ProductGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ProductGroup_Get;
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
                List<Mst_ProductGroup> lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                bool bGet_Mst_ProductGroup = (objRQ_Mst_ProductGroup.Rt_Cols_Mst_ProductGroup != null && objRQ_Mst_ProductGroup.Rt_Cols_Mst_ProductGroup.Length > 0);
                strOrgID_Login = objRQ_Mst_ProductGroup.OrgID;
                #endregion

                #region // MasterServerLogin:
                string strWebAPIUrl = null;
                {
                    ////
                    strWebAPIUrl = "http://14.232.244.217:12088/idocNet.Test.ProductCenter.V10.Local.WA/";


                }
                #endregion

                #region // WS_Mst_ProductGroup_Get:
                mdsResult = Mst_ProductGroup_Get(
                    objRQ_Mst_ProductGroup.Tid // strTid
                    , objRQ_Mst_ProductGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ProductGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ProductGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ProductGroup.WAUserPassword // strUserPassword
					, objRQ_Mst_ProductGroup.AccessToken // strAccessToken
					, objRQ_Mst_ProductGroup.NetworkID // strNetworkID
					, objRQ_Mst_ProductGroup.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_ProductGroup.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_ProductGroup.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_ProductGroup.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Mst_ProductGroup.Rt_Cols_Mst_ProductGroup // strRt_Cols_Mst_ProductGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_ProductGroup.MySummaryTable = lst_MySummaryTable[0];

                    if (bGet_Mst_ProductGroup)
                    {
                    ////
                    DataTable dt_Mst_ProductGroup = mdsResult.Tables["Mst_ProductGroup"].Copy();
                    lst_Mst_ProductGroup = TUtils.DataTableCmUtils.ToListof<Mst_ProductGroup>(dt_Mst_ProductGroup);
                    objRT_Mst_ProductGroup.Lst_Mst_ProductGroup = lst_Mst_ProductGroup;
                    /////
                    }
                    // Assign:
                    CmUtils.CMyDataSet.SetRemark(ref mdsResult, strWebAPIUrl);
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
        public DataSet Mst_ProductGroup_Get(
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
            , string strRt_Cols_Mst_ProductGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ProductGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ProductGroup_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_ProductGroup", strRt_Cols_Mst_ProductGroup
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

                #region // Mst_ProductGroup_GetX:
                DataSet dsGetData = null;
                {
                    Mst_ProductGroup_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strOrgID // strOrgID
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_ProductGroup // strRt_Cols_Mst_ProductGroup
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

        private void Mst_ProductGroup_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_ProductGroup
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_ProductGroup_GetX";
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
            bool bGet_Mst_ProductGroup = (strRt_Cols_Mst_ProductGroup != null && strRt_Cols_Mst_ProductGroup.Length > 0);

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
            zzzzClauseSelect_Mst_Org_ViewAbility_Get(
                strOrgID // strOrgID
                , ref alParamsCoupleError // alParamsCoupleError
                );
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_ProductGroup_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mpg.ProductGrpCode
						, mpg.OrgID
					into #tbl_Mst_ProductGroup_Filter_Draft
					from Mst_ProductGroup mpg --//[mylock]
                        inner join #tbl_Mst_Org_ViewAbility t_mo --//[mylock]
	                        on mpg.OrgID = t_mo.OrgID
					where (1=1)
						zzB_Where_strFilter_zzE
						and mpg.FlagFG = '0'
					order by mpg.ProductGrpCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_ProductGroup_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_ProductGroup_Filter:
					select
						t.*
					into #tbl_Mst_ProductGroup_Filter
					from #tbl_Mst_ProductGroup_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_ProductGroup -----:
					zzB_Select_Mst_ProductGroup_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_ProductGroup_Filter_Draft;
					--drop table #tbl_Mst_ProductGroup_Filter;
					"
                );
            ////
            string zzB_Select_Mst_ProductGroup_zzE = "-- Nothing.";
            if (bGet_Mst_ProductGroup)
            {
                #region // bGet_Mst_ProductGroup:
                zzB_Select_Mst_ProductGroup_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_ProductGroup:
					select
						t.MyIdxSeq
						, mpg.*
					from #tbl_Mst_ProductGroup_Filter t --//[mylock]
						inner join Mst_ProductGroup mpg --//[mylock]
							on t.OrgID = mpg.OrgID
								and t.ProductGrpCode = mpg.ProductGrpCode
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
                        , "Mst_ProductGroup" // strTableNameDB
                        , "Mst_ProductGroup." // strPrefixStd
                        , "mpg." // strPrefixAlias
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
                , "zzB_Select_Mst_ProductGroup_zzE", zzB_Select_Mst_ProductGroup_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_ProductGroup)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_ProductGroup";
            }
            #endregion
        }

        public void Mst_ProductGroup_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Mst_ProductGroup_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Mst_ProductGroup_SaveAllX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objFlagIsDelete",objFlagIsDelete
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
            #endregion

            #region // Refine and Check Input:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            #endregion

            #region // Refine and Check Input Mst_ProductGroup:
            ////

            ////
            DataTable dtInput_Mst_ProductGroup = null;
            {
                ////
                string strTableCheck = "Mst_ProductGroup";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ProductGroup_SaveX_Input_ProductGroupTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_ProductGroup.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_ProductGroup_SaveX_Input_ProductGroupTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "StdParam", "ProductGrpCodeParent" // arrstrCouple
                    , "", "ProductGrpBUCode" // arrstrCouple
                    , "", "ProductGrpBUPattern" // arrstrCouple
                    , "", "ProductGrpLevel" // arrstrCouple
                    , "", "ProductGrpName" // arrstrCouple
                    , "", "ProductGrpDesc" // arrstrCouple
                    , "StdParam", "BrandCode" // arrstrCouple
                    , "StdFlag", "FlagFG" // arrstrCouple
                    , "StdFlag", "FlagActive" // arrstrCouple
                    , "StdDate", "LogLUDTimeUTC" // arrstrCouple
                    , "", "LogLUBy" // arrstrCouple
                    );
                ////

                ////
            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            //if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "ProductGrpCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "ProductGrpCodeParent", TConst.BizMix.Default_DBColType
                            , "ProductGrpBUCode", TConst.BizMix.Default_DBColType
                            , "ProductGrpBUPattern", TConst.BizMix.Default_DBColType
                            , "ProductGrpLevel", TConst.BizMix.Default_DBColType
                            , "ProductGrpName", TConst.BizMix.Default_DBColType
                            , "ProductGrpDesc", TConst.BizMix.Default_DBColType
                            , "BrandCode", TConst.BizMix.Default_DBColType
                            , "FlagFG", TConst.BizMix.Default_DBColType
                            , "FlagActive", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
            }
            #endregion

            #region // Check ParentALL:
            {
                string strSql_CheckParent = CmUtils.StringUtils.Replace(@"
                        -- Mst_ProductGroup:
                        select distinct
	                        t.OrgID
                            , t.NetworkID
	                        , t.ProductGrpCodeParent ProductGrpCode
                        into #tbl_Mst_ProductGroup_Filter
                        from #input_Mst_ProductGroup t --//[mylock]
                        where (1=1)
	                        and t.ProductGrpCodeParent = '@strALL'
                        ;

                        select
                            t.OrgID
                            , t.NetworkID
                            , t.ProductGrpCode
                        into #tbl_Mst_ProductGroup_Summary
                        from #tbl_Mst_ProductGroup_Filter t --//[mylock]
                            left join Mst_ProductGroup f --//[mylock]
                                on t.OrgID = f.OrgID
                                    and t.ProductGrpCode = f.ProductGrpCode
                        where (1=1)
                            and f.ProductGrpCode is null
                        ;

                        select
                            t.*
                        from #tbl_Mst_ProductGroup_Summary t --//[mylock]
                        where (1=1)
                        ;
                    "
                    , "@strALL", "ALL"
                    );

                DataTable dt_CheckParent = _cf.db.ExecQuery(strSql_CheckParent).Tables[0];

                ////
                if (dt_CheckParent.Rows.Count > 0)
                {
                    string strSql_InsertParent = CmUtils.StringUtils.Replace(@"
                            ---- :
                            insert into Mst_ProductGroup
                            (
                                OrgID
                                , ProductGrpCode
                                , NetworkID
                                , ProductGrpCodeParent
                                , ProductGrpBUCode
                                , ProductGrpBUPattern
                                , ProductGrpLevel
                                , ProductGrpName
                                , ProductGrpDesc
                                , BrandCode
                                , FlagFG
                                , FlagActive
                                , LogLUDTimeUTC
                                , LogLUBy
                            )
                            select
                                f.OrgID
                                , t.ProductGrpCode
                                , f.NetworkID
                                , t.ProductGrpCodeParent
                                , t.ProductGrpBUCode
                                , t.ProductGrpBUPattern
                                , t.ProductGrpLevel
                                , t.ProductGrpName
                                , t.ProductGrpDesc
                                , t.BrandCode
                                , t.FlagFG
                                , t.FlagActive
                                , t.LogLUDTimeUTC
                                , t.LogLUBy
                            from Mst_ProductGroup t --//[mylock]
                                left join #tbl_Mst_ProductGroup_Summary f --//[mylock]
                                    on (1=1)
                            where (1=1)
                                and t.OrgID = '0'
                                and t.ProductGrpCode = 'ALL'
                            ;
                        ");

                    _cf.db.ExecQuery(strSql_InsertParent);
                }
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Mst_ProductGroup:
							    delete t
							    from Mst_ProductGroup t --//[mylock]
								    inner join #input_Mst_ProductGroup f --//[mylock]
									    on t.OrgID = f.OrgID
                                            and t.ProductGrpCode = f.ProductGrpCode
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
                        string zzzzClauseInsert_Mst_ProductGroup_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Mst_ProductGroup
                                (
                                    OrgID
                                    , ProductGrpCode
                                    , NetworkID
                                    , ProductGrpCodeParent
                                    , ProductGrpBUCode
                                    , ProductGrpBUPattern
                                    , ProductGrpLevel
                                    , ProductGrpName
                                    , ProductGrpDesc
                                    , BrandCode
                                    , FlagFG
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.OrgID
                                    , t.ProductGrpCode
                                    , t.NetworkID
                                    , t.ProductGrpCodeParent
                                    , t.ProductGrpBUCode
                                    , t.ProductGrpBUPattern
                                    , t.ProductGrpLevel
                                    , t.ProductGrpName
                                    , t.ProductGrpDesc
                                    , t.BrandCode
                                    , t.FlagFG
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Mst_ProductGroup t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Mst_ProductGroup_zSave
							"
                            , "zzzzClauseInsert_Mst_ProductGroup_zSave", zzzzClauseInsert_Mst_ProductGroup_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion

                    #region //// Clear For Debug:
                    if (!bIsDelete)
                    {
                        ////
                        string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						        ---- Clear for Debug:
						        drop table #input_Mst_ProductGroup;
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
						        drop table #input_Mst_ProductGroup;
					        ");

                        _cf.db.ExecQuery(
                            strSqlClearForDebug
                            );
                        ////

                    }
                    #endregion
                }
            }
            #endregion
        }

        public DataSet Mst_ProductGroup_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_Invoice_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ProductGroup_Save;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Invoice_Invoice_SaveX:
                //DataSet dsGetData = null;
                {
                    Mst_ProductGroup_SaveX(
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
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
                #endregion
            }
        }

        public DataSet WAS_Mst_ProductGroup_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ProductGroup objRQ_Mst_ProductGroup
            ////
            , out RT_Mst_ProductGroup objRT_Mst_ProductGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ProductGroup.Tid;
            objRT_Mst_ProductGroup = new RT_Mst_ProductGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ProductGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ProductGroup_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ProductGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Mst_ProductGroup.FlagIsDelete
                , "Lst_Mst_ProductGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_ProductGroup.Lst_Mst_ProductGroup)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_ProductGroup> lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Mst_ProductGroup.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                    dsData.Tables.Add(dt_Mst_ProductGroup);
                    ////
                }
                #endregion

                #region // WS_Mst_ProductGroup_Create: 
                // Mst_ProductGroup_Save_Root_New20190704
                mdsResult = Mst_ProductGroup_Save(
                    objRQ_Mst_ProductGroup.Tid // strTid
                    , objRQ_Mst_ProductGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ProductGroup.GwPassword // strGwPassword
                    , objRQ_Mst_ProductGroup.WAUserCode // strUserCode
                    , objRQ_Mst_ProductGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_ProductGroup.FlagIsDelete // objFlagIsDelete
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

        #region // Mst_Attribute:
        public void Mst_Attribute_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Mst_Attribute_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Mst_Attribute_SaveAllX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objFlagIsDelete",objFlagIsDelete
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
            #endregion

            #region // Refine and Check Input:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            #endregion

            #region // Refine and Check Input Mst_Attribute:
            ////

            ////
            DataTable dtInput_Mst_Attribute = null;
            {
                ////
                string strTableCheck = "Mst_Attribute";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Attribute_SaveX_Input_AttributeTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Attribute = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_Attribute.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Attribute_SaveX_Input_AttributeTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Attribute // dtData
                    , "StdParam", "AttributeCode" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "", "AttributeName" // arrstrCouple
                    , "StdFlag", "FlagActive" // arrstrCouple
                    , "StdDate", "LogLUDTimeUTC" // arrstrCouple
                    , "", "LogLUBy" // arrstrCouple
                    );
                ////

                ////
            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            //if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Attribute" // strTableName
                    , new object[] {
                            "AttributeCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "AttributeName", TConst.BizMix.Default_DBColType
                            , "FlagActive", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Attribute // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Mst_Attribute:
							    delete t
							    from Mst_Attribute t --//[mylock]
								    inner join #input_Mst_Attribute f --//[mylock]
									    on t.AttributeCode = f.AttributeCode
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
                        string zzzzClauseInsert_Mst_Attribute_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Mst_Attribute
                                (
                                    AttributeCode
                                    , NetworkID
                                    , AttributeName
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.AttributeCode
                                    , t.NetworkID
                                    , t.AttributeName
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Mst_Attribute t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Mst_Attribute_zSave
							"
                            , "zzzzClauseInsert_Mst_Attribute_zSave", zzzzClauseInsert_Mst_Attribute_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion

                    #region //// Clear For Debug:
                    if (!bIsDelete)
                    {
                        ////
                        string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						        ---- Clear for Debug:
						        drop table #input_Mst_Attribute;
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
						        drop table #input_Mst_Attribute;
					        ");

                        _cf.db.ExecQuery(
                            strSqlClearForDebug
                            );
                        ////

                    }
                    #endregion
                }
            }
            #endregion
        }

        public DataSet Mst_Attribute_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_Invoice_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Attribute_Save;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Invoice_Invoice_SaveX:
                //DataSet dsGetData = null;
                {
                    Mst_Attribute_SaveX(
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
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
                #endregion
            }
        }

        public DataSet WAS_Mst_Attribute_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Attribute objRQ_Mst_Attribute
            ////
            , out RT_Mst_Attribute objRT_Mst_Attribute
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Attribute.Tid;
            objRT_Mst_Attribute = new RT_Mst_Attribute();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Attribute.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Attribute_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Attribute_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Mst_Attribute.FlagIsDelete
                , "Lst_Mst_Attribute", TJson.JsonConvert.SerializeObject(objRQ_Mst_Attribute.Lst_Mst_Attribute)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_Attribute> lst_Mst_Attribute = new List<Mst_Attribute>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_Attribute = TUtils.DataTableCmUtils.ToDataTable<Mst_Attribute>(objRQ_Mst_Attribute.Lst_Mst_Attribute, "Mst_Attribute");
                    dsData.Tables.Add(dt_Mst_Attribute);
                    ////
                }
                #endregion

                #region // WS_Mst_Attribute_Create: 
                // Mst_Attribute_Save_Root_New20190704
                mdsResult = Mst_Attribute_Save(
                    objRQ_Mst_Attribute.Tid // strTid
                    , objRQ_Mst_Attribute.GwUserCode // strGwUserCode
                    , objRQ_Mst_Attribute.GwPassword // strGwPassword
                    , objRQ_Mst_Attribute.WAUserCode // strUserCode
                    , objRQ_Mst_Attribute.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Attribute.FlagIsDelete // objFlagIsDelete
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

		private void Mst_Attribute_GetX(
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
			, string strRt_Cols_Mst_Attribute
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Attribute_GetX";
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
			bool bGet_Mst_Attribute = (strRt_Cols_Mst_Attribute != null && strRt_Cols_Mst_Attribute.Length > 0);

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
					---- #tbl_Mst_Attribute_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, matb.AttributeCode
					into #tbl_Mst_Attribute_Filter_Draft
					from Mst_Attribute matb --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by matb.AttributeCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_Attribute_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_Attribute_Filter:
					select
						t.*
					into #tbl_Mst_Attribute_Filter
					from #tbl_Mst_Attribute_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_Attribute -----:
					zzB_Select_Mst_Attribute_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_Attribute_Filter_Draft;
					--drop table #tbl_Mst_Attribute_Filter;
					"
				);
			////
			string zzB_Select_Mst_Attribute_zzE = "-- Nothing.";
			if (bGet_Mst_Attribute)
			{
				#region // bGet_Mst_Attribute:
				zzB_Select_Mst_Attribute_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_Attribute:
					select
						t.MyIdxSeq
						, matb.*
					from #tbl_Mst_Attribute_Filter t --//[mylock]
						inner join Mst_Attribute matb --//[mylock]
							on t.AttributeCode = matb.AttributeCode
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
						, "Mst_Attribute" // strTableNameDB
						, "Mst_Attribute." // strPrefixStd
						, "matb." // strPrefixAlias
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
				, "zzB_Select_Mst_Attribute_zzE", zzB_Select_Mst_Attribute_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Attribute)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Attribute";
			}
			#endregion
		}

		public DataSet Mst_Attribute_Get(
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
			, string strRt_Cols_Mst_Attribute
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Attribute_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Attribute_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_Attribute", strRt_Cols_Mst_Attribute
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

				#region // Mst_Attribute_GetX:
				DataSet dsGetData = null;
				{
					Mst_Attribute_GetX(
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
						, strRt_Cols_Mst_Attribute // strRt_Cols_Mst_Attribute
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
		// Mst_Attribute_Get
		public DataSet WAS_Mst_Attribute_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Attribute objRQ_Mst_Attribute
			////
			, out RT_Mst_Attribute objRT_Mst_Attribute
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Attribute.Tid;
			objRT_Mst_Attribute = new RT_Mst_Attribute();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Attribute.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Attribute_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Attribute_Get;
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
				List<Mst_Attribute> lst_Mst_Attribute = new List<Mst_Attribute>();
				#endregion

				#region // WS_Mst_Attribute_Get:
				mdsResult = Mst_Attribute_Get(
					objRQ_Mst_Attribute.Tid // strTid
					, objRQ_Mst_Attribute.GwUserCode // strGwUserCode
					, objRQ_Mst_Attribute.GwPassword // strGwPassword
					, objRQ_Mst_Attribute.WAUserCode // strUserCode
					, objRQ_Mst_Attribute.WAUserPassword // strUserPassword
					, objRQ_Mst_Attribute.AccessToken
					, objRQ_Mst_Attribute.NetworkID
					, objRQ_Mst_Attribute.OrgID
					, TUtils.CUtils.StdFlag(objRQ_Mst_Attribute.FlagIsEndUser) // objFlagIsEndUser
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Attribute.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Attribute.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Attribute.Ft_WhereClause // strFt_WhereClause
														 //// Return:
					, objRQ_Mst_Attribute.Rt_Cols_Mst_Attribute // strRt_Cols_Mst_Attribute
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_Mst_Attribute = mdsResult.Tables["Mst_Attribute"].Copy();
					lst_Mst_Attribute = TUtils.DataTableCmUtils.ToListof<Mst_Attribute>(dt_Mst_Attribute);
					objRT_Mst_Attribute.Lst_Mst_Attribute = lst_Mst_Attribute;
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

		#region // Mst_ProductType:
		public DataSet WAS_Mst_ProductType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_ProductType objRQ_Mst_ProductType
            ////
            , out RT_Mst_ProductType objRT_Mst_ProductType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_ProductType.Tid;
            objRT_Mst_ProductType = new RT_Mst_ProductType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ProductType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_ProductType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_ProductType_Get;
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
                List<Mst_ProductType> lst_Mst_ProductType = new List<Mst_ProductType>();
                #endregion

                #region // WS_Mst_ProductType_Get:
                mdsResult = Mst_ProductType_Get(
                    objRQ_Mst_ProductType.Tid // strTid
                    , objRQ_Mst_ProductType.GwUserCode // strGwUserCode
                    , objRQ_Mst_ProductType.GwPassword // strGwPassword
                    , objRQ_Mst_ProductType.WAUserCode // strUserCode
                    , objRQ_Mst_ProductType.WAUserPassword // strUserPassword
					, objRQ_Mst_ProductType.AccessToken // strAccessToken
					, objRQ_Mst_ProductType.NetworkID // strNetworkID
					, objRQ_Mst_ProductType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_ProductType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_ProductType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_ProductType.Ft_WhereClause // strFt_WhereClause
                                                           //// Return:
                    , objRQ_Mst_ProductType.Rt_Cols_Mst_ProductType // strRt_Cols_Mst_ProductType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Mst_ProductType = mdsResult.Tables["Mst_ProductType"].Copy();
                    lst_Mst_ProductType = TUtils.DataTableCmUtils.ToListof<Mst_ProductType>(dt_Mst_ProductType);
                    objRT_Mst_ProductType.Lst_Mst_ProductType = lst_Mst_ProductType;
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
        public DataSet Mst_ProductType_Get(
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
            , string strRt_Cols_Mst_ProductType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_ProductType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_ProductType_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_ProductType", strRt_Cols_Mst_ProductType
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

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_ProductType_GetX:
				DataSet dsGetData = null;
                {
                    Mst_ProductType_GetX(
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
                        , strRt_Cols_Mst_ProductType // strRt_Cols_Mst_ProductType
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
        private void Mst_ProductType_GetX(
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
            , string strRt_Cols_Mst_ProductType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_ProductType_GetX";
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
            bool bGet_Mst_ProductType = (strRt_Cols_Mst_ProductType != null && strRt_Cols_Mst_ProductType.Length > 0);

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
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			//zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
			//	drAbilityOfUser // drAbilityOfUser
			//	, ref alParamsCoupleError // alParamsCoupleError
			//	);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_ProductType_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mpt.ProductType
					into #tbl_Mst_ProductType_Filter_Draft
					from Mst_ProductType mpt --//[mylock]
                        --inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            --on mpt.OrgID = t_MstNNT_View.OrgID
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mpt.ProductType asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_ProductType_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_ProductType_Filter:
					select
						t.*
					into #tbl_Mst_ProductType_Filter
					from #tbl_Mst_ProductType_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_ProductType -----:
					zzB_Select_Mst_ProductType_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_ProductType_Filter_Draft;
					--drop table #tbl_Mst_ProductType_Filter;
					"
				);
            ////
            string zzB_Select_Mst_ProductType_zzE = "-- Nothing.";
            if (bGet_Mst_ProductType)
            {
                #region // bGet_Mst_ProductType:
                zzB_Select_Mst_ProductType_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_ProductType:
					select
						t.MyIdxSeq
						, mpt.*
					from #tbl_Mst_ProductType_Filter t --//[mylock]
						inner join Mst_ProductType mpt --//[mylock]
							on t.ProductType = mpt.ProductType
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
                        , "Mst_ProductType" // strTableNameDB
                        , "Mst_ProductType." // strPrefixStd
                        , "mpt." // strPrefixAlias
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
                , "zzB_Select_Mst_ProductType_zzE", zzB_Select_Mst_ProductType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_ProductType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_ProductType";
            }
            #endregion
        }
        #endregion
    }
}
