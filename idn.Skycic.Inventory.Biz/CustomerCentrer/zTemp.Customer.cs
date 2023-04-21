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
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System.IO;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
		#region // Mst_Customer:
		private void Mst_Customer_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objCustomerCodeSys
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
						and t.OrgID = @objOrgID
						and t.CustomerCodeSys = @objCustomerCodeSys
					;
				");
			dtDB_Mst_Customer = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objCustomerCodeSys", objCustomerCodeSys
				).Tables[0];
			dtDB_Mst_Customer.TableName = "Mst_Customer";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Customer.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.CustomerCodeSys", objCustomerCodeSys
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_CheckDB_CustomerNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Customer.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.CustomerCodeSys", objCustomerCodeSys
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_CheckDB_CustomerExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Customer.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.CustomerCodeSys", objCustomerCodeSys
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Customer.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Customer_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

        public DataSet Mst_Customer_Get(
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
            , string strRt_Cols_Mst_Customer
            , string strRt_Cols_UserOwner_Customer
            , string strRt_Cols_Mst_CustomerInCustomerGroup
            , string strRt_Cols_bGet_Mst_CustomerInArea
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Customer_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Get;
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
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

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
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Mst_Customer = (strRt_Cols_Mst_Customer != null && strRt_Cols_Mst_Customer.Length > 0);
                bool bGet_UserOwner_Customer = (strRt_Cols_UserOwner_Customer != null && strRt_Cols_UserOwner_Customer.Length > 0);
                bool bGet_Mst_CustomerInCustomerGroup = (strRt_Cols_Mst_CustomerInCustomerGroup != null && strRt_Cols_Mst_CustomerInCustomerGroup.Length > 0);
                bool bGet_Mst_CustomerInArea = (strRt_Cols_bGet_Mst_CustomerInArea != null && strRt_Cols_bGet_Mst_CustomerInArea.Length > 0);
                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                //myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);
                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
                ////
                zzzzClauseSelect_Mst_Org_ViewAbility_Get(
                    strOrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Customer_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mc.OrgID
							, mc.CustomerCodeSys
						into #tbl_Mst_Customer_Filter_Draft
						from Mst_Customer mc --//[mylock]
							inner join #tbl_Mst_Org_ViewAbility t_mo --//[mylock]
	                            on mc.OrgID = t_mo.OrgID
							left join UserOwner_Customer uoc --//[mylock]
								on mc.OrgID = uoc.OrgID
									and mc.CustomerCodeSys = uoc.CustomerCodeSys
                            left join Mst_CustomerInCustomerGroup mcic --//[mylock]
								on mc.OrgID = mcic.OrgID
									and mc.CustomerCodeSys = mcic.CustomerCodeSys
                            left join Mst_CustomerInArea mcia --//[mylock]
								on mc.OrgID = mcia.OrgID
									and mc.CustomerCodeSys = mcia.CustomerCodeSys
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							mc.OrgID 
							, mc.CustomerCodeSys asc
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

						-------- UserOwner_Customer --------:
						zzB_Select_UserOwner_Customer_zzE
						----------------------------------------

                        -------- Mst_CustomerInCustomerGroup --------:
						zzB_Select_Mst_CustomerInCustomerGroup_zzE
						----------------------------------------

                        -------- Mst_CustomerInArea --------:
						zzB_Select_Mst_CustomerInArea_zzE
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
                                , mnnt_Org.NNTFullName Org_NNTFullName
	                            , nnt_Network.NNTFullName Network_NNTFullName
	                            ----
	                            , mct.CustomerTypeName mct_CustomerTypeName
	                            , STUFF((
	                                   select
		                                   ',' + mcg.CustomerGrpName
	                                   from Mst_CustomerInCustomerGroup t --//[mylock]
		                                  inner join #tbl_Mst_Customer_Filter f --//[mylock]
			                                 on t.OrgID = f.OrgID
				                                and t.CustomerCodeSys = f.CustomerCodeSys
		                                  inner join Mst_CustomerGroup mcg --//[mylock]
			                                 on t.OrgID = mcg.OrgID
				                                and t.CustomerGrpCode = mcg.CustomerGrpCode
	                                   where (1=1)
	                                   FOR
	                                   xml path('')
	                                   ), 1, 1, ''
	                                   ) mcg_CustomerGrpName
	                            , mcs.CustomerSourceName mcs_CustomerSourceName
	                            , mp.ProvinceName mp_ProvinceName
	                            , md.DistrictName md_DistrictName
	                            , mw.WardName mw_WardName
	                            , ma.AreaName ma_AreaName
	                            , mg.GovIDTypeName
                            from #tbl_Mst_Customer_Filter t --//[mylock]
	                            inner join Mst_Customer mc --//[mylock]
		                            on t.OrgID = mc.OrgID
			                            and t.CustomerCodeSys = mc.CustomerCodeSys
                                left join Mst_NNT mnnt_Org --//[mylock]
                                    on t.OrgID = mnnt_Org.OrgID
                                left join Mst_NNT nnt_Network --//[mylock]
                                    on mc.NetworkID = nnt_Network.OrgID
	                            left join Mst_CustomerType mct --//[mylock]
		                            on mc.CustomerType = mct.CustomerType
	                            left join Mst_CustomerSource mcs --//[mylock]
		                            on mc.CustomerSourceCode = mcs.CustomerSourceCode
                                        and mc.OrgID = mcs.OrgID
	                            left join Mst_Province mp --//[mylock]
		                            on mc.ProvinceCode = mp.ProvinceCode
	                            left join Mst_District md --//[mylock]
		                            on mc.DistrictCode = md.DistrictCode
	                            left join Mst_Ward mw --//[mylock]
		                            on mc.WardCode = mw.WardCode
	                            left join Mst_Area ma --//[mylock]
		                            on mc.AreaCode = ma.AreaCode
                                        and t.OrgID = ma.OrgID
	                            left join Mst_GovIDType mg --//[mylock]
		                            on mc.GovIDType = mg.GovIDType
                            order by t.MyIdxSeq asc
                            ;
						"
                        );
                    #endregion
                }
                ////
                string zzB_Select_UserOwner_Customer_zzE = "-- Nothing.";
                if (bGet_UserOwner_Customer)
                {
                    #region // bGet_UserOwner_Customer:
                    zzB_Select_UserOwner_Customer_zzE = CmUtils.StringUtils.Replace(@"
							---- UserOwner_Customer:
							select
								t.MyIdxSeq
								, uoc.*
							from #tbl_Mst_Customer_Filter t --//[mylock]
								inner join UserOwner_Customer uoc --//[mylock]
									on t.OrgID = uoc.OrgID
										and t.CustomerCodeSys = uoc.CustomerCodeSys
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzB_Select_Mst_CustomerInCustomerGroup_zzE = "-- Nothing.";
                if (bGet_Mst_CustomerInCustomerGroup)
                {
                    #region // bGet_Mst_CustomerInCustomerGroup:
                    zzB_Select_Mst_CustomerInCustomerGroup_zzE = CmUtils.StringUtils.Replace(@"
							---- UserOwner_Customer:
							select
	                            t.MyIdxSeq
	                            , mcic.*
	                            , mc.CustomerCodeSys mc_CustomerCodeSys
	                            , mc.CustomerCode mc_CustomerCode
	                            , mc.CustomerName mc_CustomerName
	                            , mcg.CustomerGrpCode mcg_CustomerGrpCode
	                            , mcg.CustomerGrpName mcg_CustomerGrpName
	                            , mcg.CustomerGrpDesc mcg_CustomerGrpDesc
	                            , mcg.FlagActive mcg_FlagActive
                            from #tbl_Mst_Customer_Filter t --//[mylock]
	                            inner join Mst_CustomerInCustomerGroup mcic --//[mylock]
		                            on t.OrgID = mcic.OrgID
			                            and t.CustomerCodeSys = mcic.CustomerCodeSys
	                            left join Mst_Customer mc --//[mylock]
		                            on mcic.CustomerCodeSys = mc.CustomerCodeSys
			                            and mcic.OrgID = mc.OrgID
	                            left join Mst_CustomerGroup mcg --//[mylock]
		                            on mcg.CustomerGrpCode = mcic.CustomerGrpCode
			                            and mcg.OrgID = mcic.OrgID
                            order by t.MyIdxSeq asc
                            ;
						"
                        );
                    #endregion
                }
                string zzB_Select_Mst_CustomerInArea_zzE = "-- Nothing.";
                if (bGet_Mst_CustomerInArea)
                {
                    #region // bGet_Mst_CustomerInArea:
                    zzB_Select_Mst_CustomerInArea_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_CustomerInArea:
							select
								t.MyIdxSeq
								, mcia.*
                                , mc.CustomerCode mc_CustomerCode
                                , mc.CustomerName mc_CustomerName
                                , ma.AreaName ma_AreaName
                                , ma.AreaCodeParent ma_AreaCodeParent
							from #tbl_Mst_Customer_Filter t --//[mylock]
								inner join Mst_CustomerInArea mcia --//[mylock]
									on t.OrgID = mcia.OrgID
										and t.CustomerCodeSys = mcia.CustomerCodeSys
                                left join Mst_Customer mc --//[mylock]
                                    on mcia.OrgID = mc.OrgID
                                        and mcia.CustomerCodeSys = mc.CustomerCodeSys
                                left join Mst_Area ma --//[mylock]
                                    on mcia.OrgID = ma.OrgID
                                        and mcia.AreaCode = ma.AreaCode
							order by t.MyIdxSeq asc
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
                            , "Mst_Customer" // strTableNameDB
                            , "Mst_Customer." // strPrefixStd
                            , "mc." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "UserOwner_Customer" // strTableNameDB
                            , "UserOwner_Customer." // strPrefixStd
                            , "uoc." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_CustomerInCustomerGroup" // strTableNameDB
                            , "Mst_CustomerInCustomerGroup." // strPrefixStd
                            , "mcic." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_CustomerInArea" // strTableNameDB
                            , "Mst_CustomerInArea." // strPrefixStd
                            , "mcia." // strPrefixAlias
                            );
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
                    , "zzB_Select_UserOwner_Customer_zzE", zzB_Select_UserOwner_Customer_zzE
                    , "zzB_Select_Mst_CustomerInCustomerGroup_zzE", zzB_Select_Mst_CustomerInCustomerGroup_zzE
                    , "zzB_Select_Mst_CustomerInArea_zzE", zzB_Select_Mst_CustomerInArea_zzE
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
                if (bGet_UserOwner_Customer)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "UserOwner_Customer";
                }
                if (bGet_Mst_CustomerInCustomerGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerInCustomerGroup";
                }
                if (bGet_Mst_CustomerInArea)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerInArea";
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
        public DataSet Mst_Customer_Create(
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Customer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Create;
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
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Call Func:
				////
				Mst_Customer_CreateX(
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
		private void Mst_Customer_CreateX(
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
			int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "Mst_Customer_CreateX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Create;
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

			#region //// Refine and Check Mst_Customer:
			string strCreateDTime = null;
			string strCreateBy = null;

			////
			DataTable dtDB_Mst_Customer = null;
			string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

			////
			//string strFunctionActionType = TConst.FunctionActionType.Add;
			//string strFunctionRemark = "";
			{

				////
				strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
				strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
				////
			}
			////
			#endregion

			#region //// Refine and Check Mst_Customer:
			////
			DataTable dtInput_Mst_Customer = null;
			{
				////
				string strTableCheck = "Mst_Customer";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Customer = dsData.Tables[strTableCheck];
				////
				if (dtInput_Mst_Customer.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerTblInvalid
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Customer // dtData
					, "StdParam", "OrgID" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					, "", "CustomerCode" // arrstrCouple
					, "StdParam", "CustomerType" // arrstrCouple
					, "", "CustomerGrpCode" // arrstrCouple
					, "StdParam", "CustomerSourceCode" // arrstrCouple
					, "", "CustomerName" // arrstrCouple
					, "", "CustomerName" // arrstrCouple
					, "", "CustomerNameEN" // arrstrCouple
					, "", "CustomerGender" // arrstrCouple
					, "", "CustomerPhoneNo" // arrstrCouple
					, "", "CustomerMobilePhone" // arrstrCouple
					, "StdParam", "ProvinceCode" // arrstrCouple
					//, "StdParam", "ProvinceCode" // arrstrCouple
					, "StdParam", "WardCode" // arrstrCouple
					, "StdParam", "AreaCode" // arrstrCouple
					, "", "CustomerAvatarName" // arrstrCouple
					, "", "CustomerAvatarSpec" // arrstrCouple
					, "", "FlagCustomerAvatarPath" // arrstrCouple
					, "", "CustomerAvatarPath" // arrstrCouple
					, "", "CustomerAddress" // arrstrCouple
					, "", "CustomerEmail" // arrstrCouple
					, "", "CustomerAddress" // arrstrCouple
					, "", "CustomerEmail" // arrstrCouple
					, "StdDate", "CustomerDateOfBirth" // arrstrCouple
					, "", "GovIDType" // arrstrCouple
					, "", "GovIDCardNo" // arrstrCouple
					, "", "GovIDCardDate" // arrstrCouple
					, "StdDate", "GovIDCardPlace" // arrstrCouple
					, "", "TaxCode" // arrstrCouple
					, "", "BankCode" // arrstrCouple
					, "", "BankName" // arrstrCouple
					, "", "BankAccountNo" // arrstrCouple
					, "", "RepresentName" // arrstrCouple
					, "", "RepresentPosition" // arrstrCouple
					//, "", "GPDKKDAddress" // arrstrCouple
					, "", "ContactName" // arrstrCouple
					, "", "ContactPhone" // arrstrCouple
					, "", "ContactEmail" // arrstrCouple
					, "", "Fax" // arrstrCouple
					, "", "Facebook" // arrstrCouple
					, "", "InvoiceCustomerName" // arrstrCouple
					, "", "InvoiceCustomerAddress" // arrstrCouple
					, "", "InvoiceOrgName" // arrstrCouple
					, "", "InvoiceEmailSend" // arrstrCouple
					, "", "MST" // arrstrCouple
					, "", "ListOfCustDynamicFieldValue" // arrstrCouple
					, "StdFlag", "FlagDealer" // arrstrCouple
					, "StdFlag", "FlagSupplier" // arrstrCouple
					, "StdFlag", "FlagEndUser" // arrstrCouple
					, "StdFlag", "FlagBank" // arrstrCouple
					, "StdFlag", "FlagInsurrance" // arrstrCouple
					, "", "Remark" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "UserCodeOwner", typeof(object));
				//TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CustomerAvatarPath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateBy", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUBy", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
					string strFlagCustomerAvatarPath = TUtils.CUtils.StdFlag(drScan["FlagCustomerAvatarPath"]);

					////
					Mst_Customer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, drScan["OrgID"] // objOrgID
						, drScan["CustomerCodeSys"] // objCustomerCodeSys
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strStatusListToCheck
						, out dtDB_Mst_Customer // dtDB_Mst_Customer
						);
					////
					string strImagePath = null;
					string strImageSpec = drScan["CustomerAvatarSpec"].ToString();
					string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["CustomerAvatarName"].ToString());
					////
					if (CmUtils.StringUtils.StringEqual(strFlagCustomerAvatarPath, TConst.Flag.Inactive))
					{
						string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
						string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
						string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
						byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
						string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
						string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
						strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

						drScan["CustomerAvatarPath"] = strImagePath;

						#region // WriteFile:
						////
						if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
						{
							bool exist = Directory.Exists(strFilePathBase);

							if (!exist)
							{
								Directory.CreateDirectory(strFilePathBase);
							}
							System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
						}
						#endregion
					}

					////
					//DataTable dtDB_Mst_CustomerType = null;

					//Mst_CustomerType_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, drScan["ProductType"] // objProductType
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
					//	);

					////
					drScan["NetworkID"] = nNetworkID;
					drScan["CustomerCode"] = TUtils.CUtils.StdParam(drScan["CustomerCode"]).Trim();
					drScan["UserCodeOwner"] = strWAUserCode;
					drScan["CreateDTimeUTC"] = strCreateDTime;
					drScan["CreateBy"] = strWAUserCode;
					drScan["LUDTimeUTC"] = strCreateDTime;
					drScan["LUBy"] = strWAUserCode;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Mst_Customer:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Customer"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"CustomerCodeSys", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"NetworkID", TConst.BizMix.Default_DBColType,
						"CustomerCode", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"CustomerType", TConst.BizMix.Default_DBColType, // Loại khách hàng
						"CustomerGrpCode", TConst.BizMix.Default_DBColType, // Nhóm khách hàng
						"CustomerSourceCode", TConst.BizMix.Default_DBColType, // Nguồn khách
						"CustomerName", TConst.BizMix.Default_DBColType, // Tên khách hàng
						"CustomerNameEN", TConst.BizMix.Default_DBColType, // Tên khách hàng EN
						"CustomerGender", TConst.BizMix.Default_DBColType, // Giới tính
						"CustomerPhoneNo", TConst.BizMix.Default_DBColType, // ĐT cố định
						"CustomerMobilePhone", TConst.BizMix.Default_DBColType, // ĐT di động
						"ProvinceCode", TConst.BizMix.Default_DBColType, // Tỉnh/Thành
						"DistrictCode", TConst.BizMix.MyText_DBColType, // Quận/Huyện
						"WardCode", TConst.BizMix.MyText_DBColType, // Phường/Xã
						"AreaCode", TConst.BizMix.Default_DBColType, // Khu vực
						"CustomerAvatarName", TConst.BizMix.Default_DBColType, // Tên ảnh Avatar
						"CustomerAvatarPath", TConst.BizMix.Default_DBColType, // Đường dẫn ảnh Avatar
						"CustomerAddress", TConst.BizMix.Default_DBColType, // Địa chỉ
						"CustomerEmail", TConst.BizMix.Default_DBColType, // Email
						"CustomerDateOfBirth", TConst.BizMix.Default_DBColType, // Ngày sinh/Ngày thành lập
						"GovIDType", TConst.BizMix.Default_DBColType, // Loại giấy tờ
						"GovIDCardNo", TConst.BizMix.Default_DBColType, // Số giấy tờ
						"GovIDCardDate", TConst.BizMix.Default_DBColType,
						"GovIDCardPlace", TConst.BizMix.Default_DBColType,
						"TaxCode", TConst.BizMix.Default_DBColType,
						"BankCode", TConst.BizMix.Default_DBColType,
						"BankName", TConst.BizMix.Default_DBColType,
						"BankAccountNo", TConst.BizMix.Default_DBColType, // Số tài khoản
						"RepresentName", TConst.BizMix.Default_DBColType, // Người đại diện
						"RepresentPosition", TConst.BizMix.Default_DBColType, // Chức vụ
						//"GPDKKDAddress", TConst.BizMix.Default_DBColType,
						"UserCodeOwner", TConst.BizMix.Default_DBColType,
						"ContactName", TConst.BizMix.Default_DBColType, // Người liên hệ
						"ContactPhone", TConst.BizMix.Default_DBColType, // ĐT liên hệ
						"ContactEmail", TConst.BizMix.Default_DBColType, // Email liên hệ
						"Fax", TConst.BizMix.Default_DBColType, // Fax
						"Facebook", TConst.BizMix.MyText_DBColType, // Facebook
						"InvoiceCustomerName", TConst.BizMix.MyText_DBColType, // Người mua hàng
						"InvoiceCustomerAddress", TConst.BizMix.MyText_DBColType, // Địa chỉ
						"InvoiceOrgName", TConst.BizMix.MyText_DBColType, // Tên tổ chức
						"InvoiceEmailSend", TConst.BizMix.MyText_DBColType, // Email nhận HĐ
						"MST", TConst.BizMix.MyText_DBColType, // MST
						"ListOfCustDynamicFieldValue", TConst.BizMix.MyText_DBColType, // Trường động
						"FlagDealer", TConst.BizMix.MyText_DBColType,
						"FlagSupplier", TConst.BizMix.MyText_DBColType,
						"FlagEndUser", TConst.BizMix.MyText_DBColType,
						"FlagBank", TConst.BizMix.MyText_DBColType,
						"FlagInsurrance", TConst.BizMix.MyText_DBColType,
						"CreateDTimeUTC", TConst.BizMix.Default_DBColType,
						"CreateBy", TConst.BizMix.Default_DBColType,
						"LUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LUBy", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_Customer
					);
			}
			#endregion

			#region //// Refine and Check UserOwner_Customer:
			////
			DataTable dtInput_UserOwner_Customer = null;
			{
				////
				string strTableCheck = "UserOwner_Customer";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Create_Input_UserOwner_CustomerTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_UserOwner_Customer = dsData.Tables[strTableCheck];
				////
				//if (dtInput_UserOwner_Customer.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Customer_Create_Input_UserOwner_CustomerTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_UserOwner_Customer // dtData
					, "StdParam", "UserCode" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUDTime", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_UserOwner_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_UserOwner_Customer.Rows[nScan];

					////
					drScan["NetworkID"] = nNetworkID;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp UserOwner_Customer:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_UserOwner_Customer"
					, new object[]{
						"UserCode", TConst.BizMix.Default_DBColType,
						"OrgID", TConst.BizMix.Default_DBColType,
						"CustomerCodeSys", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_UserOwner_Customer
					);
			}
			#endregion

			#region //// Save:
			//// Insert All:
			{
				////
				string zzzzClauseInsert_Mst_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_Customer:
						insert into Mst_Customer
						(
							OrgID
							, CustomerCodeSys
							, NetworkID
							, CustomerCode
							, CustomerType
							, CustomerGrpCode
							, CustomerSourceCode
							, CustomerName
							, CustomerNameEN
							, CustomerGender
							, CustomerPhoneNo
							, CustomerMobilePhone
							, ProvinceCode
							, DistrictCode
							, WardCode
							, AreaCode
							, CustomerAvatarName
							, CustomerAvatarPath
							, CustomerAddress
							, CustomerEmail
							, CustomerDateOfBirth
							, GovIDType
							, GovIDCardNo
							, GovIDCardDate
							, GovIDCardPlace
							, TaxCode
							, BankCode
							, BankName
							, BankAccountNo
							, RepresentName
							, RepresentPosition
							, UserCodeOwner
							, ContactName
							, ContactPhone
							, ContactEmail
							, Fax
							, Facebook
							, InvoiceCustomerName
							, InvoiceCustomerAddress
							, InvoiceOrgName
							, InvoiceEmailSend
							, MST
							, ListOfCustDynamicFieldValue
							, FlagDealer
							, FlagSupplier
							, FlagEndUser
							, FlagBank
							, FlagInsurrance
							, CreateDTimeUTC
							, CreateBy
							, LUDTimeUTC
							, LUBy
							, FlagActive
							, Remark
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.CustomerCodeSys
							, t.NetworkID
							, t.CustomerCode
							, t.CustomerType
							, t.CustomerGrpCode
							, t.CustomerSourceCode
							, t.CustomerName
							, t.CustomerNameEN
							, t.CustomerGender
							, t.CustomerPhoneNo
							, t.CustomerMobilePhone
							, t.ProvinceCode
							, t.DistrictCode
							, t.WardCode
							, t.AreaCode
							, t.CustomerAvatarName
							, t.CustomerAvatarPath
							, t.CustomerAddress
							, t.CustomerEmail
							, t.CustomerDateOfBirth
							, t.GovIDType
							, t.GovIDCardNo
							, t.GovIDCardDate
							, t.GovIDCardPlace
							, t.TaxCode
							, t.BankCode
							, t.BankName
							, t.BankAccountNo
							, t.RepresentName
							, t.RepresentPosition
							, t.UserCodeOwner
							, t.ContactName
							, t.ContactPhone
							, t.ContactEmail
							, t.Fax
							, t.Facebook
							, t.InvoiceCustomerName
							, t.InvoiceCustomerAddress
							, t.InvoiceOrgName
							, t.InvoiceEmailSend
							, t.MST
							, t.ListOfCustDynamicFieldValue
							, t.FlagDealer
							, t.FlagSupplier
							, t.FlagEndUser
							, t.FlagBank
							, t.FlagInsurrance
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.LUDTimeUTC
							, t.LUBy
							, t.FlagActive
							, t.Remark
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Mst_Customer t --//[mylock]
						order by
							t.OrgID
							, t.CustomerCodeSys asc
						;
					");
				////
				string zzzzClauseInsert_UserOwner_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- UserOwner_Customer:
						insert into UserOwner_Customer
						(
							UserCode
							, OrgID
							, CustomerCodeSys
							, NetworkID
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.UserCode
							, t.OrgID
							, t.CustomerCodeSys
							, t.NetworkID
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_UserOwner_Customer t --//[mylock]
						;
					");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_Customer_zSave

						----
						zzzzClauseInsert_UserOwner_Customer_zSave
					"
					, "zzzzClauseInsert_Mst_Customer_zSave", zzzzClauseInsert_Mst_Customer_zSave
					, "zzzzClauseInsert_UserOwner_Customer_zSave", zzzzClauseInsert_UserOwner_Customer_zSave
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

			#region // Check CustomerCode UK:
			{
				////
				//string strSqlCheck = CmUtils.StringUtils.Replace(@"
				//		---- Mst_Customer:
				//		select
				//			t.CustomerCodeSys CustomerCodeSys_DB
				//			, t.CustomerCode CustomerCode_DB
				//			, f.CustomerCodeSys CustomerCodeSys_Input
				//			, f.CustomerCode CustomerCode_Input
				//		into #tblCheckForStuff
				//		from Mst_Customer t --//[mylock]
				//			inner join #input_Mst_Customer f --//[mylock]
				//				on t.OrgID = f.OrgID
				//		where (1=1)
				//			and t.CustomerCodeSys != f.CustomerCodeSys
				//			and t.CustomerCode = f.CustomerCode
				//		;

				//		--select null tblCheckForStuff, * from #tblCheckForStuff t --//[mylock];
				//		--drop table #tblCheckForStuff;

				//		--- Return:					
				//		select 
				//			STUFF(( 
				//				SELECT ', ' + f.CustomerCode_Input
				//				FROM #tblCheckForStuff f --//[mylock]
				//				WHERE(1=1)
				//				FOR
				//				XML PATH('')
				//				), 1, 1, ''
				//			) AS ListCustomerCodeSys_Input
				//		where(1=1)
				//		;

				//		---- Clear For Debug:
				//		drop table #tblCheckForStuff;
				//	");

				//DataTable dtCheck = _cf.db.ExecQuery(strSqlCheck).Tables[0];
				//////
				//if (dtCheck.Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(dtCheck.Rows[0]["ListCustomerCodeSys_Input"])))
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.ListCustomerCodeSys_Input", dtCheck.Rows[0]["ListCustomerCodeSys_Input"]
				//		, "Check.NumberRows", dtCheck.Rows.Count
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Customer_Create_InvalidCustomerCodeSysUK
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}

			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Customer;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		public DataSet Mst_Customer_Update(
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Customer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Update;
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
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Call Func:
				////
				Mst_Customer_UpdateX(
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
		private void Mst_Customer_UpdateX(
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
			int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "Mst_Customer_UpdateX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Update;
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

			#region //// Refine and Check Mst_Customer:
			string strCreateDTime = null;
			string strCreateBy = null;

			////
			DataTable dtDB_Mst_Customer = null;
			string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

			////
			//string strFunctionActionType = TConst.FunctionActionType.Add;
			//string strFunctionRemark = "";
			{

				////
				strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
				strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
				////
			}
			////
			#endregion

			#region //// Refine and Check Mst_Customer:
			////
			DataTable dtInput_Mst_Customer = null;
			{
				////
				string strTableCheck = "Mst_Customer";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Customer = dsData.Tables[strTableCheck];
				////
				if (dtInput_Mst_Customer.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerTblInvalid
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Customer // dtData
					, "StdParam", "OrgID" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					, "", "CustomerCode" // arrstrCouple
					, "StdParam", "CustomerType" // arrstrCouple
					, "", "CustomerGrpCode" // arrstrCouple
					, "StdParam", "CustomerSourceCode" // arrstrCouple
					, "", "CustomerName" // arrstrCouple
					, "", "CustomerName" // arrstrCouple
					, "", "CustomerNameEN" // arrstrCouple
					, "", "CustomerGender" // arrstrCouple
					, "", "CustomerPhoneNo" // arrstrCouple
					, "", "CustomerMobilePhone" // arrstrCouple
					, "StdParam", "ProvinceCode" // arrstrCouple
												 //, "StdParam", "ProvinceCode" // arrstrCouple
					, "StdParam", "WardCode" // arrstrCouple
					, "StdParam", "AreaCode" // arrstrCouple
					, "", "CustomerAvatarName" // arrstrCouple
					, "", "CustomerAvatarSpec" // arrstrCouple
					, "", "CustomerAddress" // arrstrCouple
					, "", "CustomerEmail" // arrstrCouple
					, "", "CustomerAvatarSpec" // arrstrCouple
					, "", "CustomerAddress" // arrstrCouple
					, "", "CustomerEmail" // arrstrCouple
					, "StdDate", "CustomerDateOfBirth" // arrstrCouple
					, "", "GovIDType" // arrstrCouple
					, "", "GovIDCardNo" // arrstrCouple
					, "", "GovIDCardDate" // arrstrCouple
					, "StdDate", "GovIDCardPlace" // arrstrCouple
					, "", "TaxCode" // arrstrCouple
					, "", "BankCode" // arrstrCouple
					, "", "BankName" // arrstrCouple
					, "", "BankAccountNo" // arrstrCouple
					, "", "RepresentName" // arrstrCouple
					, "", "RepresentPosition" // arrstrCouple
											  //, "", "GPDKKDAddress" // arrstrCouple
					, "", "ContactName" // arrstrCouple
					, "", "ContactPhone" // arrstrCouple
					, "", "ContactEmail" // arrstrCouple
					, "", "Fax" // arrstrCouple
					, "", "Facebook" // arrstrCouple
					, "", "InvoiceCustomerName" // arrstrCouple
					, "", "InvoiceCustomerAddress" // arrstrCouple
					, "", "InvoiceOrgName" // arrstrCouple
					, "", "InvoiceEmailSend" // arrstrCouple
					, "", "MST" // arrstrCouple
					, "", "ListOfCustDynamicFieldValue" // arrstrCouple
					, "StdFlag", "FlagDealer" // arrstrCouple
					, "StdFlag", "FlagSupplier" // arrstrCouple
					, "StdFlag", "FlagEndUser" // arrstrCouple
					, "StdFlag", "FlagBank" // arrstrCouple
					, "StdFlag", "FlagInsurrance" // arrstrCouple
					, "StdFlag", "FlagActive" // arrstrCouple
					, "", "Remark" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "UserCodeOwner", typeof(object));
				//TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CustomerAvatarPath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateBy", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUBy", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
					string strFlagCustomerAvatarPath = TUtils.CUtils.StdFlag(drScan["FlagCustomerAvatarPath"]);

					////
					Mst_Customer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, drScan["OrgID"] // objOrgID
						, drScan["CustomerCodeSys"] // objCustomerCodeSys
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strStatusListToCheck
						, out dtDB_Mst_Customer // dtDB_Mst_Customer
						);
					////
					string strImagePath = null;
					string strImageSpec = drScan["CustomerAvatarSpec"].ToString();
					string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["CustomerAvatarName"].ToString());
					////
					if (CmUtils.StringUtils.StringEqual(strFlagCustomerAvatarPath, TConst.Flag.Inactive))
					{
						string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
						string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
						string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
						byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
						string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
						string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
						strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

						drScan["CustomerAvatarPath"] = strImagePath;

                        #region // WriteFile:
                        ////
                        if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                        {
                            bool exist = Directory.Exists(strFilePathBase);

                            if (!exist)
                            {
                                Directory.CreateDirectory(strFilePathBase);
                            }
                            System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                        }
                        #endregion
                    }

                    ////
                    //DataTable dtDB_Mst_CustomerType = null;

                    //Mst_CustomerType_CheckDB(
                    //	ref alParamsCoupleError // alParamsCoupleError
                    //	, drScan["ProductType"] // objProductType
                    //	, TConst.Flag.Yes // strFlagExistToCheck
                    //	, TConst.Flag.Active // strFlagActiveListToCheck
                    //	, out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    //	);

                    ////
                    drScan["NetworkID"] = nNetworkID;
					drScan["CustomerCode"] = TUtils.CUtils.StdParam(drScan["CustomerCode"]).Trim();
					drScan["UserCodeOwner"] = strWAUserCode;
					drScan["CreateDTimeUTC"] = strCreateDTime;
					drScan["CreateBy"] = strWAUserCode;
					drScan["LUDTimeUTC"] = strCreateDTime;
					drScan["LUBy"] = strWAUserCode;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Mst_Customer:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Customer"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"CustomerCodeSys", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"NetworkID", TConst.BizMix.Default_DBColType,
						"CustomerCode", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"CustomerType", TConst.BizMix.Default_DBColType, // Loại khách hàng
						"CustomerGrpCode", TConst.BizMix.Default_DBColType, // Nhóm khách hàng
						"CustomerSourceCode", TConst.BizMix.Default_DBColType, // Nguồn khách
						"CustomerName", TConst.BizMix.Default_DBColType, // Tên khách hàng
						"CustomerNameEN", TConst.BizMix.Default_DBColType, // Tên khách hàng EN
						"CustomerGender", TConst.BizMix.Default_DBColType, // Giới tính
						"CustomerPhoneNo", TConst.BizMix.Default_DBColType, // ĐT cố định
						"CustomerMobilePhone", TConst.BizMix.Default_DBColType, // ĐT di động
						"ProvinceCode", TConst.BizMix.Default_DBColType, // Tỉnh/Thành
						"DistrictCode", TConst.BizMix.MyText_DBColType, // Quận/Huyện
						"WardCode", TConst.BizMix.MyText_DBColType, // Phường/Xã
						"AreaCode", TConst.BizMix.Default_DBColType, // Khu vực
						"CustomerAvatarName", TConst.BizMix.Default_DBColType, // Tên ảnh Avatar
						"CustomerAvatarPath", TConst.BizMix.Default_DBColType, // Đường dẫn ảnh Avatar
						"CustomerAddress", TConst.BizMix.Default_DBColType, // Địa chỉ
						"CustomerEmail", TConst.BizMix.Default_DBColType, // Email
						"CustomerDateOfBirth", TConst.BizMix.Default_DBColType, // Ngày sinh/Ngày thành lập
						"GovIDType", TConst.BizMix.Default_DBColType, // Loại giấy tờ
						"GovIDCardNo", TConst.BizMix.Default_DBColType, // Số giấy tờ
						"GovIDCardDate", TConst.BizMix.Default_DBColType,
						"GovIDCardPlace", TConst.BizMix.Default_DBColType,
						"TaxCode", TConst.BizMix.Default_DBColType,
						"BankCode", TConst.BizMix.Default_DBColType,
						"BankName", TConst.BizMix.Default_DBColType,
						"BankAccountNo", TConst.BizMix.Default_DBColType, // Số tài khoản
						"RepresentName", TConst.BizMix.Default_DBColType, // Người đại diện
						"RepresentPosition", TConst.BizMix.Default_DBColType, // Chức vụ
						//"GPDKKDAddress", TConst.BizMix.Default_DBColType,
						"UserCodeOwner", TConst.BizMix.Default_DBColType,
						"ContactName", TConst.BizMix.Default_DBColType, // Người liên hệ
						"ContactPhone", TConst.BizMix.Default_DBColType, // ĐT liên hệ
						"ContactEmail", TConst.BizMix.Default_DBColType, // Email liên hệ
						"Fax", TConst.BizMix.Default_DBColType, // Fax
						"Facebook", TConst.BizMix.MyText_DBColType, // Facebook
						"InvoiceCustomerName", TConst.BizMix.MyText_DBColType, // Người mua hàng
						"InvoiceCustomerAddress", TConst.BizMix.MyText_DBColType, // Địa chỉ
						"InvoiceOrgName", TConst.BizMix.MyText_DBColType, // Tên tổ chức
						"InvoiceEmailSend", TConst.BizMix.MyText_DBColType, // Email nhận HĐ
						"MST", TConst.BizMix.MyText_DBColType, // MST
						"ListOfCustDynamicFieldValue", TConst.BizMix.MyText_DBColType, // Trường động
						"FlagDealer", TConst.BizMix.MyText_DBColType,
						"FlagSupplier", TConst.BizMix.MyText_DBColType,
						"FlagEndUser", TConst.BizMix.MyText_DBColType,
						"FlagBank", TConst.BizMix.MyText_DBColType,
						"FlagInsurrance", TConst.BizMix.MyText_DBColType,
						"CreateDTimeUTC", TConst.BizMix.Default_DBColType,
						"CreateBy", TConst.BizMix.Default_DBColType,
						"LUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LUBy", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_Customer
					);
			}
			#endregion

			#region //// Refine and Check UserOwner_Customer:
			////
			DataTable dtInput_UserOwner_Customer = null;
			{
				////
				string strTableCheck = "UserOwner_Customer";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Update_Input_UserOwner_CustomerTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_UserOwner_Customer = dsData.Tables[strTableCheck];
				////
				//if (dtInput_UserOwner_Customer.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Customer_Update_Input_UserOwner_CustomerTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_UserOwner_Customer // dtData
					, "StdParam", "UserCode" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "FlagActive", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUDTime", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_UserOwner_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_UserOwner_Customer.Rows[nScan];

					////
					drScan["NetworkID"] = nNetworkID;
					drScan["FlagActive"] = TConst.Flag.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp UserOwner_Customer:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_UserOwner_Customer"
					, new object[]{
						"UserCode", TConst.BizMix.Default_DBColType,
						"OrgID", TConst.BizMix.Default_DBColType,
						"CustomerCodeSys", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_UserOwner_Customer
					);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- UserOwner_Customer:
                            delete t
                            from UserOwner_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;

                            ---- Mst_Customer:
                            delete t
                            from Mst_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;
						");
				DataSet dtDB = _cf.db.ExecQuery(
					strSqlDelete
					);
			}
			//// Insert All:
			{
				////
				string zzzzClauseInsert_Mst_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_Customer:
						insert into Mst_Customer
						(
							OrgID
							, CustomerCodeSys
							, NetworkID
							, CustomerCode
							, CustomerType
							, CustomerGrpCode
							, CustomerSourceCode
							, CustomerName
							, CustomerNameEN
							, CustomerGender
							, CustomerPhoneNo
							, CustomerMobilePhone
							, ProvinceCode
							, DistrictCode
							, WardCode
							, AreaCode
							, CustomerAvatarName
							, CustomerAvatarPath
							, CustomerAddress
							, CustomerEmail
							, CustomerDateOfBirth
							, GovIDType
							, GovIDCardNo
							, GovIDCardDate
							, GovIDCardPlace
							, TaxCode
							, BankCode
							, BankName
							, BankAccountNo
							, RepresentName
							, RepresentPosition
							, UserCodeOwner
							, ContactName
							, ContactPhone
							, ContactEmail
							, Fax
							, Facebook
							, InvoiceCustomerName
							, InvoiceCustomerAddress
							, InvoiceOrgName
							, InvoiceEmailSend
							, MST
							, ListOfCustDynamicFieldValue
							, FlagDealer
							, FlagSupplier
							, FlagEndUser
							, FlagBank
							, FlagInsurrance
							, CreateDTimeUTC
							, CreateBy
							, LUDTimeUTC
							, LUBy
							, FlagActive
							, Remark
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.CustomerCodeSys
							, t.NetworkID
							, t.CustomerCode
							, t.CustomerType
							, t.CustomerGrpCode
							, t.CustomerSourceCode
							, t.CustomerName
							, t.CustomerNameEN
							, t.CustomerGender
							, t.CustomerPhoneNo
							, t.CustomerMobilePhone
							, t.ProvinceCode
							, t.DistrictCode
							, t.WardCode
							, t.AreaCode
							, t.CustomerAvatarName
							, t.CustomerAvatarPath
							, t.CustomerAddress
							, t.CustomerEmail
							, t.CustomerDateOfBirth
							, t.GovIDType
							, t.GovIDCardNo
							, t.GovIDCardDate
							, t.GovIDCardPlace
							, t.TaxCode
							, t.BankCode
							, t.BankName
							, t.BankAccountNo
							, t.RepresentName
							, t.RepresentPosition
							, t.UserCodeOwner
							, t.ContactName
							, t.ContactPhone
							, t.ContactEmail
							, t.Fax
							, t.Facebook
							, t.InvoiceCustomerName
							, t.InvoiceCustomerAddress
							, t.InvoiceOrgName
							, t.InvoiceEmailSend
							, t.MST
							, t.ListOfCustDynamicFieldValue
							, t.FlagDealer
							, t.FlagSupplier
							, t.FlagEndUser
							, t.FlagBank
							, t.FlagInsurrance
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.LUDTimeUTC
							, t.LUBy
							, t.FlagActive
							, t.Remark
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Mst_Customer t --//[mylock]
						order by
							t.OrgID
							, t.CustomerCodeSys asc
						;
					");
				////
				string zzzzClauseInsert_UserOwner_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- UserOwner_Customer:
						insert into UserOwner_Customer
						(
							UserCode
							, OrgID
							, CustomerCodeSys
							, NetworkID
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.UserCode
							, t.OrgID
							, t.CustomerCodeSys
							, t.NetworkID
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_UserOwner_Customer t --//[mylock]
						;
					");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_Customer_zSave

						----
						zzzzClauseInsert_UserOwner_Customer_zSave
					"
					, "zzzzClauseInsert_Mst_Customer_zSave", zzzzClauseInsert_Mst_Customer_zSave
					, "zzzzClauseInsert_UserOwner_Customer_zSave", zzzzClauseInsert_UserOwner_Customer_zSave
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

			#region // Check CustomerCode UK:
			{
				////
				//string strSqlCheck = CmUtils.StringUtils.Replace(@"
				//		---- Mst_Customer:
				//		select
				//			t.CustomerCodeSys CustomerCodeSys_DB
				//			, t.CustomerCode CustomerCode_DB
				//			, f.CustomerCodeSys CustomerCodeSys_Input
				//			, f.CustomerCode CustomerCode_Input
				//		into #tblCheckForStuff
				//		from Mst_Customer t --//[mylock]
				//			inner join #input_Mst_Customer f --//[mylock]
				//				on t.OrgID = f.OrgID
				//		where (1=1)
				//			and t.CustomerCodeSys != f.CustomerCodeSys
				//			and t.CustomerCode = f.CustomerCode
				//		;

				//		--select null tblCheckForStuff, * from #tblCheckForStuff t --//[mylock];
				//		--drop table #tblCheckForStuff;

				//		--- Return:					
				//		select 
				//			STUFF(( 
				//				SELECT ', ' + f.CustomerCode_Input
				//				FROM #tblCheckForStuff f --//[mylock]
				//				WHERE(1=1)
				//				FOR
				//				XML PATH('')
				//				), 1, 1, ''
				//			) AS ListCustomerCodeSys_Input
				//		where(1=1)
				//		;

				//		---- Clear For Debug:
				//		drop table #tblCheckForStuff;
				//	");

				//DataTable dtCheck = _cf.db.ExecQuery(strSqlCheck).Tables[0];
				//////
				//if (dtCheck.Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(dtCheck.Rows[0]["ListCustomerCodeSys_Input"])))
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.ListCustomerCodeSys_Input", dtCheck.Rows[0]["ListCustomerCodeSys_Input"]
				//		, "Check.NumberRows", dtCheck.Rows.Count
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Mst_Customer_Update_InvalidCustomerCodeSysUK
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}

			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Customer;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}
		public DataSet Mst_Customer_Delete(
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Customer_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Delete;
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
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Call Func:
				////
				Mst_Customer_DeleteX(
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
		private void Mst_Customer_DeleteX(
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
			string strFunctionName = "Mst_Customer_UpdateX";
			//string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Update;
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

			#region //// Refine and Check Mst_Customer:
			string strCreateDTime = null;
			string strCreateBy = null;

			////
			DataTable dtDB_Mst_Customer = null;
			string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

			////
			//string strFunctionActionType = TConst.FunctionActionType.Add;
			//string strFunctionRemark = "";
			{

				////
				strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
				strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
				////
			}
			////
			#endregion

			#region //// Refine and Check Mst_Customer:
			////
			DataTable dtInput_Mst_Customer = null;
			{
				////
				string strTableCheck = "Mst_Customer";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Update_Input_Mst_CustomerTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Customer = dsData.Tables[strTableCheck];
				////
				if (dtInput_Mst_Customer.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Customer_Update_Input_Mst_CustomerTblInvalid
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Customer // dtData
					, "StdParam", "OrgID" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Customer.Rows[nScan];

					////
					Mst_Customer_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, drScan["OrgID"] // objOrgID
						, drScan["CustomerCodeSys"] // objCustomerCodeSys
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strStatusListToCheck
						, out dtDB_Mst_Customer // dtDB_Mst_Customer
						);
					////
					//DataTable dtDB_Mst_CustomerType = null;

					//Mst_CustomerType_CheckDB(
					//	ref alParamsCoupleError // alParamsCoupleError
					//	, drScan["ProductType"] // objProductType
					//	, TConst.Flag.Yes // strFlagExistToCheck
					//	, TConst.Flag.Active // strFlagActiveListToCheck
					//	, out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
					//	);

					////
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Mst_Customer:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Customer"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"CustomerCodeSys", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_Customer
					);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- UserOwner_Customer:
                            delete t
                            from UserOwner_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;

                            ---- Mst_Customer:
                            delete t
                            from Mst_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;
						");
				DataSet dtDB = _cf.db.ExecQuery(
					strSqlDelete
					);
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_Customer;
					");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

        public DataSet Mst_Customer_Save(
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
            //bool bMyDebugSql = false;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_Customer_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Save;
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                ////
                Mst_Customer_SaveX(
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

        private void Mst_Customer_SaveX(
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
            , DataSet dsData
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_Customer_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_Update;
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

            #region //// Refine and Check Mst_Customer:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            //DataTable dtDB_Mst_Customer = null;
            string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

            ////
            //string strFunctionActionType = TConst.FunctionActionType.Update;
            //string strFunctionRemark = "";
            {

                ////
                strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            ////
            #endregion

            #region //// Refine and Check Mst_Customer:
            ////
            DataTable dtInput_Mst_Customer = null;
            {
                ////
                string strTableCheck = "Mst_Customer";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Customer_Save_Input_Mst_CustomerTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Customer = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_Customer.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Customer // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "", "CustomerCode" // arrstrCouple
                    , "StdParam", "CustomerType" // arrstrCouple
                    , "", "CustomerGrpCode" // arrstrCouple
                    , "StdParam", "CustomerSourceCode" // arrstrCouple
                    , "", "CustomerName" // arrstrCouple
                    , "", "CustomerName" // arrstrCouple
                    , "", "CustomerNameEN" // arrstrCouple
                    , "", "CustomerGender" // arrstrCouple
                    , "", "CustomerPhoneNo" // arrstrCouple
                    , "", "CustomerMobilePhone" // arrstrCouple
                    , "StdParam", "ProvinceCode" // arrstrCouple
                                                 //, "StdParam", "ProvinceCode" // arrstrCouple
                    , "StdParam", "WardCode" // arrstrCouple
                    , "StdParam", "AreaCode" // arrstrCouple
                    , "", "CustomerAvatarName" // arrstrCouple
                    , "", "CustomerAvatarSpec" // arrstrCouple
                    , "", "FlagCustomerAvatarPath" // arrstrCouple
                    , "", "CustomerAvatarPath" // arrstrCouple
                    , "", "CustomerAddress" // arrstrCouple
                    , "", "CustomerEmail" // arrstrCouple
                    , "", "CustomerAddress" // arrstrCouple
                    , "", "CustomerEmail" // arrstrCouple
                    , "StdDate", "CustomerDateOfBirth" // arrstrCouple
                    , "", "GovIDType" // arrstrCouple
                    , "", "GovIDCardNo" // arrstrCouple
                    , "", "GovIDCardDate" // arrstrCouple
                    , "StdDate", "GovIDCardPlace" // arrstrCouple
                    , "", "TaxCode" // arrstrCouple
                    , "", "BankCode" // arrstrCouple
                    , "", "BankName" // arrstrCouple
                    , "", "BankAccountNo" // arrstrCouple
                    , "", "RepresentName" // arrstrCouple
                    , "", "RepresentPosition" // arrstrCouple
                                              //, "", "GPDKKDAddress" // arrstrCouple
                    , "", "ContactName" // arrstrCouple
                    , "", "ContactPhone" // arrstrCouple
                    , "", "ContactEmail" // arrstrCouple
                    , "", "Fax" // arrstrCouple
                    , "", "Facebook" // arrstrCouple
                    , "", "InvoiceCustomerName" // arrstrCouple
                    , "", "InvoiceCustomerAddress" // arrstrCouple
                    , "", "InvoiceOrgName" // arrstrCouple
                    , "", "InvoiceEmailSend" // arrstrCouple
                    , "", "MST" // arrstrCouple
                    , "", "ListOfCustDynamicFieldValue" // arrstrCouple
                    , "StdFlag", "FlagDealer" // arrstrCouple 
                    , "StdFlag", "FlagShipper" // arrstrCouple
                    , "StdFlag", "FlagSupplier" // arrstrCouple
                    , "StdFlag", "FlagEndUser" // arrstrCouple
                    , "StdFlag", "FlagBank" // arrstrCouple
                    , "StdFlag", "FlagInsurrance" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    , "", "GLN"
                    , "", "Coordinates"
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "UserCodeOwner", typeof(object));
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CustomerAvatarPath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "CreateBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
                    string strFlagCustomerAvatarPath = TUtils.CUtils.StdFlag(drScan["FlagCustomerAvatarPath"]);

                    ////
                    //Mst_Customer_CheckDB(
                    //    ref alParamsCoupleError // alParamsCoupleError
                    //    , drScan["OrgID"] // objOrgID
                    //    , drScan["CustomerCodeSys"] // objCustomerCodeSys
                    //    , TConst.Flag.No // strFlagExistToCheck
                    //    , "" // strStatusListToCheck
                    //    , out dtDB_Mst_Customer // dtDB_Mst_Customer
                    //    );
                    //////
                    //DataTable dtDB_Mst_CustomerCheckCustomerCode = null;
                    //Mst_Customer_CheckCustomerCode(
                    //    ref alParamsCoupleError // alParamsCoupleError
                    //    , drScan["OrgID"] // objOrgID
                    //    , drScan["CustomerCode"].ToString().Normalize() // objCustomerCodeSys
                    //    , TConst.Flag.No // strFlagExistToCheck
                    //    , "" // strStatusListToCheck
                    //    , out dtDB_Mst_CustomerCheckCustomerCode // dtDB_Mst_Customer
                    //    );
                    ////

                    string strImagePath = null;
                    string strImageSpec = drScan["CustomerAvatarSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["CustomerAvatarName"].ToString());
                    ////
                    if (CmUtils.StringUtils.StringEqual(strFlagCustomerAvatarPath, TConst.Flag.Inactive))
                    {
                        string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                        string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                        string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
                        byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                        string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                        string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                        strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                        drScan["CustomerAvatarPath"] = strImagePath;

                        #region // WriteFile:
                        ////
                        if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                        {
                            bool exist = Directory.Exists(strFilePathBase);

                            if (!exist)
                            {
                                Directory.CreateDirectory(strFilePathBase);
                            }
                            System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                        }
                        #endregion
                    }

                    ////
                    //DataTable dtDB_Mst_CustomerType = null;

                    //Mst_CustomerType_CheckDB(
                    //	ref alParamsCoupleError // alParamsCoupleError
                    //	, drScan["ProductType"] // objProductType
                    //	, TConst.Flag.Yes // strFlagExistToCheck
                    //	, TConst.Flag.Active // strFlagActiveListToCheck
                    //	, out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    //	);

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["CustomerCode"] = TUtils.CUtils.StdParam(drScan["CustomerCode"]);
                    drScan["UserCodeOwner"] = strWAUserCode;
                    drScan["CreateDTimeUTC"] = strCreateDTime;
                    drScan["CreateBy"] = strWAUserCode;
                    drScan["LUDTimeUTC"] = strCreateDTime;
                    drScan["LUBy"] = strWAUserCode;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Mst_Customer:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_Customer"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerCode", TConst.BizMix.Default_DBColType, // Mã khách hàng
						"CustomerType", TConst.BizMix.Default_DBColType, // Loại khách hàng
						"CustomerGrpCode", TConst.BizMix.Default_DBColType, // Nhóm khách hàng
						"CustomerSourceCode", TConst.BizMix.Default_DBColType, // Nguồn khách
						"CustomerName", TConst.BizMix.Default_DBColType, // Tên khách hàng
						"CustomerNameEN", TConst.BizMix.Default_DBColType, // Tên khách hàng EN
						"CustomerGender", TConst.BizMix.Default_DBColType, // Giới tính
						"CustomerPhoneNo", TConst.BizMix.Default_DBColType, // ĐT cố định
						"CustomerMobilePhone", TConst.BizMix.Default_DBColType, // ĐT di động
						"ProvinceCode", TConst.BizMix.Default_DBColType, // Tỉnh/Thành
						"DistrictCode", TConst.BizMix.MyText_DBColType, // Quận/Huyện
						"WardCode", TConst.BizMix.MyText_DBColType, // Phường/Xã
						"AreaCode", TConst.BizMix.Default_DBColType, // Khu vực
						"CustomerAvatarName", TConst.BizMix.Default_DBColType, // Tên ảnh Avatar
						"CustomerAvatarPath", TConst.BizMix.Default_DBColType, // Đường dẫn ảnh Avatar
						"CustomerAddress", TConst.BizMix.Default_DBColType, // Địa chỉ
						"CustomerEmail", TConst.BizMix.Default_DBColType, // Email
						"CustomerDateOfBirth", TConst.BizMix.Default_DBColType, // Ngày sinh/Ngày thành lập
						"GovIDType", TConst.BizMix.Default_DBColType, // Loại giấy tờ
						"GovIDCardNo", TConst.BizMix.Default_DBColType, // Số giấy tờ
						"GovIDCardDate", TConst.BizMix.Default_DBColType,
                        "GovIDCardPlace", TConst.BizMix.Default_DBColType,
                        "TaxCode", TConst.BizMix.Default_DBColType,
                        "BankCode", TConst.BizMix.Default_DBColType,
                        "BankName", TConst.BizMix.Default_DBColType,
                        "BankAccountNo", TConst.BizMix.Default_DBColType, // Số tài khoản
						"RepresentName", TConst.BizMix.Default_DBColType, // Người đại diện
						"RepresentPosition", TConst.BizMix.Default_DBColType, // Chức vụ
						//"GPDKKDAddress", TConst.BizMix.Default_DBColType,
						"UserCodeOwner", TConst.BizMix.Default_DBColType,
                        "ContactName", TConst.BizMix.Default_DBColType, // Người liên hệ
						"ContactPhone", TConst.BizMix.Default_DBColType, // ĐT liên hệ
						"ContactEmail", TConst.BizMix.Default_DBColType, // Email liên hệ
						"Fax", TConst.BizMix.Default_DBColType, // Fax
						"Facebook", TConst.BizMix.MyText_DBColType, // Facebook
						"InvoiceCustomerName", TConst.BizMix.MyText_DBColType, // Người mua hàng
						"InvoiceCustomerAddress", TConst.BizMix.MyText_DBColType, // Địa chỉ
						"InvoiceOrgName", TConst.BizMix.MyText_DBColType, // Tên tổ chức
						"InvoiceEmailSend", TConst.BizMix.MyText_DBColType, // Email nhận HĐ
						"MST", TConst.BizMix.MyText_DBColType, // MST
						"ListOfCustDynamicFieldValue", TConst.BizMix.MyText_DBColType, // Trường động
						"FlagDealer", TConst.BizMix.MyText_DBColType,
                        "FlagShipper", TConst.BizMix.MyText_DBColType,
                        "FlagSupplier", TConst.BizMix.MyText_DBColType,
                        "FlagEndUser", TConst.BizMix.MyText_DBColType,
                        "FlagBank", TConst.BizMix.MyText_DBColType,
                        "FlagInsurrance", TConst.BizMix.MyText_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "LUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LUBy", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_Customer
                    );
            }
            #endregion

            #region //// Refine and Check UserOwner_Customer:
            ////
            DataTable dtInput_UserOwner_Customer = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "UserOwner_Customer";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Customer_Update_Input_UserOwner_CustomerTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_UserOwner_Customer = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_UserOwner_Customer.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Customer_Create_Input_UserOwner_CustomerTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_UserOwner_Customer // dtData
                    , "StdParam", "UserCode" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUDTime", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_UserOwner_Customer, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_UserOwner_Customer.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_UserOwner_Customer.Rows[nScan];

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
                ////
            }
            #endregion

            #region //// SaveTemp UserOwner_Customer:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_UserOwner_Customer"
                    , new object[]{
                        "UserCode", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_UserOwner_Customer
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerInCustomerGroup:
            ////
            DataTable dtInput_Mst_CustomerInCustomerGroup = null;
            {
                ////
                string strTableCheck = "Mst_CustomerInCustomerGroup";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerInCustomerGroupTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerInCustomerGroup = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_Mst_CustomerInCustomerGroup.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Customer_Create_Input_Mst_CustomerInCustomerGroupTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerInCustomerGroup // dtData
                    , "StdParam", "CustomerGrpCode" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerInCustomerGroup, "NetworkID", typeof(object));
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerInCustomerGroup, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerInCustomerGroup, "LogLUDTime", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerInCustomerGroup, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerInCustomerGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerInCustomerGroup.Rows[nScan];
                    DataTable dt_Mst_CustomerGroup_CheckDB = null;
                    Mst_CustomerGroup_CheckDB(
                        ref alParamsCoupleError
                        , drScan["OrgID"]
                        , drScan["CustomerGrpCode"]
                        , TConst.Flag.Yes
                        , TConst.Flag.Active
                        , out dt_Mst_CustomerGroup_CheckDB
                        );

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Mst_CustomerInCustomerGroup:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerInCustomerGroup"
                    , new object[]{
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerInCustomerGroup
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerInArea:
            ////
            DataTable dtInput_Mst_CustomerInArea = null;
            {
                ////
                string strTableCheck = "Mst_CustomerInArea";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Customer_Create_Input_Mst_CustomerInAreaTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerInArea = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_Mst_CustomerInArea.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErridnDMS.Mst_Customer_Create_Input_Mst_CustomerInAreaTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerInArea // dtData
                    , "StdParam", "AreaCode" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "", "LogLUDTime" // arrstrCouple
                    , "", "LogLUBy" // arrstrCouple
                    );
                ////
            }
            #endregion

            #region //// SaveTemp Mst_CustomerInArea:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerInArea"
                    , new object[]{
                        "AreaCode", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerInArea
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- UserOwner_Customer:
                            delete t
                            from UserOwner_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
                                        and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;


                            ---- Mst_Customer:
                            delete t
                            from Mst_Customer t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;

                            ---- Mst_Customer:
                            delete t
                            from Mst_CustomerInCustomerGroup t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;

                            ---- Mst_Customer:
                            delete t
                            from Mst_CustomerInArea t --//[mylock]
	                            inner join #input_Mst_Customer f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.CustomerCodeSys = f.CustomerCodeSys
                            where (1=1)
                            ;
						");
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }
            //// Insert All:
            if (!bIsDelete)
            {
                ////
                string zzzzClauseInsert_Mst_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_Customer:
						insert into Mst_Customer
                        (
                            OrgID
                            , CustomerCodeSys
                            , NetworkID
                            , CustomerCode
                            , CustomerType
                            , CustomerSourceCode
                            , CustomerName
                            , CustomerNameEN
                            , CustomerGender
                            , CustomerPhoneNo
                            , CustomerMobilePhone
                            , ProvinceCode
                            , DistrictCode
                            , WardCode
                            , AreaCode
                            , CustomerAvatarName
                            , CustomerAvatarPath
                            , CustomerAddress
                            , CustomerEmail
                            , CustomerDateOfBirth
                            , GovIDType
                            , GovIDCardNo
                            , GovIDCardDate
                            , GovIDCardPlace
                            , TaxCode
                            , BankCode
                            , BankName
                            , BankAccountNo
                            , RepresentName
                            , RepresentPosition
                            , UserCodeOwner
                            , ContactName
                            , ContactPhone
                            , ContactEmail
                            , Fax
                            , Facebook
                            , InvoiceCustomerName
                            , InvoiceCustomerAddress
                            , InvoiceOrgName
                            , InvoiceEmailSend
                            , MST
                            , ListOfCustDynamicFieldValue
                            , FlagDealer
                            , FlagSupplier
                            , FlagShipper
                            , FlagEndUser
                            , FlagBank
                            , FlagInsurrance
                            , CreateDTimeUTC
                            , CreateBy
                            , LUDTimeUTC
                            , LUBy
                            , FlagActive
                            , Remark
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.OrgID
                            , t.CustomerCodeSys
                            , t.NetworkID
                            , t.CustomerCode
                            , t.CustomerType
                            , t.CustomerSourceCode
                            , t.CustomerName
                            , t.CustomerNameEN
                            , t.CustomerGender
                            , t.CustomerPhoneNo
                            , t.CustomerMobilePhone
                            , t.ProvinceCode
                            , t.DistrictCode
                            , t.WardCode
                            , t.AreaCode
                            , t.CustomerAvatarName
                            , t.CustomerAvatarPath
                            , t.CustomerAddress
                            , t.CustomerEmail
                            , t.CustomerDateOfBirth
                            , t.GovIDType
                            , t.GovIDCardNo
                            , t.GovIDCardDate
                            , t.GovIDCardPlace
                            , t.TaxCode
                            , t.BankCode
                            , t.BankName
                            , t.BankAccountNo
                            , t.RepresentName
                            , t.RepresentPosition
                            , t.UserCodeOwner
                            , t.ContactName
                            , t.ContactPhone
                            , t.ContactEmail
                            , t.Fax
                            , t.Facebook
                            , t.InvoiceCustomerName
                            , t.InvoiceCustomerAddress
                            , t.InvoiceOrgName
                            , t.InvoiceEmailSend
                            , t.MST
                            , t.ListOfCustDynamicFieldValue
                            , t.FlagDealer
                            , t.FlagSupplier
                            , t.FlagShipper
                            , t.FlagEndUser
                            , t.FlagBank
                            , t.FlagInsurrance
                            , t.CreateDTimeUTC
                            , t.CreateBy
                            , t.LUDTimeUTC
                            , t.LUBy
                            , t.FlagActive
                            , t.Remark
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_Customer t --//[mylock]
						order by
							t.OrgID
							, t.CustomerCodeSys asc
						;
					");
                ////
                string zzzzClauseInsert_UserOwner_Customer_zSave = CmUtils.StringUtils.Replace(@"
						---- UserOwner_Customer:
						insert into UserOwner_Customer
                        (
                            UserCode
                            , OrgID
                            , CustomerCodeSys
                            , NetworkID
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.UserCode
                            , t.OrgID
                            , t.CustomerCodeSys
                            , t.NetworkID
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_UserOwner_Customer t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave = CmUtils.StringUtils.Replace(@"
                        ----Mst_CustomerInCustomerGroup:
						insert into Mst_CustomerInCustomerGroup
                        (
                            CustomerGrpCode
                            , CustomerCodeSys
                            , OrgID
                            , NetworkID
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.CustomerGrpCode
							, t.CustomerCodeSys
							, t.OrgID
							, t.NetworkID
							, t.LogLUDTimeUTC
							, t.LogLUBy
                        from #input_Mst_CustomerInCustomerGroup t --//[mylock]
						;
                ");
                ////
                string zzzzClauseInsert_Mst_CustomerInArea_zSave = CmUtils.StringUtils.Replace(@"
                        ----Mst_CustomerInArea:
						insert into Mst_CustomerInArea
                        (
                            AreaCode
                            , CustomerCodeSys
                            , OrgID
                            , NetworkID
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.AreaCode
							, t.CustomerCodeSys
							, t.OrgID
							, t.NetworkID
							, t.LogLUDTimeUTC
							, t.LogLUBy
                        from #input_Mst_CustomerInArea t --//[mylock]
						;
                    ");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_Customer_zSave

						----
						zzzzClauseInsert_UserOwner_Customer_zSave
						
						----
                        zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave

                        ----
                        zzzzClauseInsert_Mst_CustomerInArea_zSave
					"
                    , "zzzzClauseInsert_Mst_Customer_zSave", zzzzClauseInsert_Mst_Customer_zSave
                    , "zzzzClauseInsert_UserOwner_Customer_zSave", zzzzClauseInsert_UserOwner_Customer_zSave
                    , "zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave", zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave
                    , "zzzzClauseInsert_Mst_CustomerInArea_zSave", zzzzClauseInsert_Mst_CustomerInArea_zSave
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
						drop table #input_Mst_Customer;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            //return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet WAS_Mst_Customer_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Customer_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_Mst_Customer)
                , "Lst_UserOwner_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_UserOwner_Customer)
				////
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
                    DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Mst_Customer.Lst_Mst_Customer, "Mst_Customer");
                    dsData.Tables.Add(dt_Mst_Customer);
                    ////
                    DataTable dt_UserOwner_Customer = null;
                    if (objRQ_Mst_Customer.Lst_UserOwner_Customer != null)
                    {
                        dt_UserOwner_Customer = TUtils.DataTableCmUtils.ToDataTable<UserOwner_Customer>(objRQ_Mst_Customer.Lst_UserOwner_Customer, "UserOwner_Customer");
                        dsData.Tables.Add(dt_UserOwner_Customer);
                    }
                    else
                    {
                        dt_UserOwner_Customer = TDALUtils.DBUtils.GetSchema(_cf.db, "UserOwner_Customer").Tables[0];
                        dsData.Tables.Add(dt_UserOwner_Customer.Copy());
                    }
                    DataTable dt_Mst_CustomerInCustomerGroup = null;
                    if (objRQ_Mst_Customer.Lst_Mst_CustomerInCustomerGroup != null)
                    {
                        dt_Mst_CustomerInCustomerGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerInCustomerGroup>(objRQ_Mst_Customer.Lst_Mst_CustomerInCustomerGroup, "Mst_CustomerInCustomerGroup");
                        dsData.Tables.Add(dt_Mst_CustomerInCustomerGroup);
                    }
                    DataTable dt_Mst_CustomerInArea = null;
                    if (objRQ_Mst_Customer.Lst_Mst_CustomerInArea != null)
                    {
                        dt_Mst_CustomerInArea = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerInArea>(objRQ_Mst_Customer.Lst_Mst_CustomerInArea, "Mst_CustomerInArea");
                        dsData.Tables.Add(dt_Mst_CustomerInArea);
                    }
                }
                #endregion

                #region // Mst_Customer_Save:
                mdsResult = Mst_Customer_Save(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Customer.FlagIsDelete // objFlagIsDelete
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

        public DataSet WAS_Mst_Customer_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Customer objRQ_Mst_Customer
            ////
            , out RT_Mst_Customer objRT_Mst_Customer
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Customer.Tid;
            objRT_Mst_Customer = new RT_Mst_Customer();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Customer_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_Get;
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
                List<Mst_Customer> lst_Mst_Customer = new List<Mst_Customer>();
                List<Prd_BOM> lst_Prd_BOM = new List<Prd_BOM>();
                List<UserOwner_Customer> lst_UserOwner_Customer = new List<UserOwner_Customer>();
                List<Mst_CustomerInCustomerGroup> lst_Mst_CustomerInCustomerGroup = new List<Mst_CustomerInCustomerGroup>(); // 
                List<Mst_CustomerInArea> lst_Mst_CustomerInArea = new List<Mst_CustomerInArea>(); // 
                bool bGet_Mst_Customer = (objRQ_Mst_Customer.Rt_Cols_Mst_Customer != null && objRQ_Mst_Customer.Rt_Cols_Mst_Customer.Length > 0);
                bool bGet_UserOwner_Customer = (objRQ_Mst_Customer.Rt_Cols_UserOwner_Customer != null && objRQ_Mst_Customer.Rt_Cols_UserOwner_Customer.Length > 0);
                bool bGet_Mst_CustomerInCustomerGroup = (objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInCustomerGroup != null && objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInCustomerGroup.Length > 0);
                bool bGet_Mst_CustomerInArea = (objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInArea != null && objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInArea.Length > 0);
                #endregion

                #region // MasterServerLogin:
                string strWebAPIUrl = null;
                {
                    ////
                    strWebAPIUrl = "http://14.232.244.217:12088/idocNet.Test.ProductCenter.V10.Local.WA/";

                    ////
                }
                #endregion

                #region // WS_Mst_Customer_Get:
                mdsResult = Mst_Customer_Get(
                    objRQ_Mst_Customer.Tid // strTid
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    , objRQ_Mst_Customer.WAUserCode // strUserCode
                    , objRQ_Mst_Customer.WAUserPassword // strUserPassword
                    , objRQ_Mst_Customer.AccessToken
                    , objRQ_Mst_Customer.NetworkID
                    , objRQ_Mst_Customer.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Mst_Customer.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_Customer.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_Customer.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_Customer.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Mst_Customer.Rt_Cols_Mst_Customer // strRt_Cols_Mst_Customer
                    , objRQ_Mst_Customer.Rt_Cols_UserOwner_Customer // strRt_Cols_UserOwner_Customer
                    , objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInCustomerGroup // strRt_Cols_Mst_CustomerInCustomerGroup
                    , objRQ_Mst_Customer.Rt_Cols_Mst_CustomerInArea // strRt_Cols_Mst_CustomerInArea
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
                        ////
                        DataTable dt_Mst_Customer = mdsResult.Tables["Mst_Customer"].Copy();
                        lst_Mst_Customer = TUtils.DataTableCmUtils.ToListof<Mst_Customer>(dt_Mst_Customer);
                        objRT_Mst_Customer.Lst_Mst_Customer = lst_Mst_Customer;
                    }
                    ////
                    if (bGet_UserOwner_Customer)
                    {
                        ////
                        DataTable dt_UserOwner_Customer = mdsResult.Tables["UserOwner_Customer"].Copy();
                        lst_UserOwner_Customer = TUtils.DataTableCmUtils.ToListof<UserOwner_Customer>(dt_UserOwner_Customer);
                        objRT_Mst_Customer.Lst_UserOwner_Customer = lst_UserOwner_Customer;
                    }
                    if (bGet_Mst_CustomerInCustomerGroup)
                    {
                        ////
                        DataTable dt_Mst_CustomerInCustomerGroup = mdsResult.Tables["Mst_CustomerInCustomerGroup"].Copy();
                        lst_Mst_CustomerInCustomerGroup = TUtils.DataTableCmUtils.ToListof<Mst_CustomerInCustomerGroup>(dt_Mst_CustomerInCustomerGroup);
                        objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup = lst_Mst_CustomerInCustomerGroup;
                    }
                    if (bGet_Mst_CustomerInArea)
                    {
                        ////
                        DataTable dt_Mst_CustomerInArea = mdsResult.Tables["Mst_CustomerInArea"].Copy();
                        lst_Mst_CustomerInArea = TUtils.DataTableCmUtils.ToListof<Mst_CustomerInArea>(dt_Mst_CustomerInArea);
                        objRT_Mst_Customer.Lst_Mst_CustomerInArea = lst_Mst_CustomerInArea;
                    }
                }
                #endregion

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsResult, strWebAPIUrl);

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

        public DataSet WAS_Mst_Customer_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Customer objRQ_Mst_Customer
			////
			, out RT_Mst_Customer objRT_Mst_Customer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Customer.Tid;
			objRT_Mst_Customer = new RT_Mst_Customer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Customer_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_Mst_Customer)
				, "Lst_UserOwner_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_UserOwner_Customer)
				////
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
					DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Mst_Customer.Lst_Mst_Customer, "Mst_Customer");
					dsData.Tables.Add(dt_Mst_Customer);
					////
					
					////
					DataTable dt_UserOwner_Customer = null;
					if (objRQ_Mst_Customer.Lst_UserOwner_Customer != null)
					{
						dt_UserOwner_Customer = TUtils.DataTableCmUtils.ToDataTable<UserOwner_Customer>(objRQ_Mst_Customer.Lst_UserOwner_Customer, "UserOwner_Customer");
						dsData.Tables.Add(dt_UserOwner_Customer);
					}
					else
					{
						dt_UserOwner_Customer = TDALUtils.DBUtils.GetSchema(_cf.db, "UserOwner_Customer").Tables[0];
						dsData.Tables.Add(dt_UserOwner_Customer.Copy());
					}
				}
				#endregion

				#region // Mst_Customer_Create:
				mdsResult = Mst_Customer_Create(
					objRQ_Mst_Customer.Tid // strTid
					, objRQ_Mst_Customer.GwUserCode // strGwUserCode
					, objRQ_Mst_Customer.GwPassword // strGwPassword
					, objRQ_Mst_Customer.WAUserCode // strUserCode
					, objRQ_Mst_Customer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
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

		public DataSet WAS_Mst_Customer_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Customer objRQ_Mst_Customer
			////
			, out RT_Mst_Customer objRT_Mst_Customer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Customer.Tid;
			objRT_Mst_Customer = new RT_Mst_Customer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Customer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_Mst_Customer)
				, "Lst_UserOwner_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_UserOwner_Customer)
				////
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
					DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Mst_Customer.Lst_Mst_Customer, "Mst_Customer");
					dsData.Tables.Add(dt_Mst_Customer);
					////

					////
					DataTable dt_UserOwner_Customer = null;
					if (objRQ_Mst_Customer.Lst_UserOwner_Customer != null)
					{
						dt_UserOwner_Customer = TUtils.DataTableCmUtils.ToDataTable<UserOwner_Customer>(objRQ_Mst_Customer.Lst_UserOwner_Customer, "UserOwner_Customer");
						dsData.Tables.Add(dt_UserOwner_Customer);
					}
					else
					{
						dt_UserOwner_Customer = TDALUtils.DBUtils.GetSchema(_cf.db, "UserOwner_Customer").Tables[0];
						dsData.Tables.Add(dt_UserOwner_Customer.Copy());
					}
				}
				#endregion

				#region // Mst_Customer_Update:
				mdsResult = Mst_Customer_Update(
					objRQ_Mst_Customer.Tid // strTid
					, objRQ_Mst_Customer.GwUserCode // strGwUserCode
					, objRQ_Mst_Customer.GwPassword // strGwPassword
					, objRQ_Mst_Customer.WAUserCode // strUserCode
					, objRQ_Mst_Customer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
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
		public DataSet WAS_Mst_Customer_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Customer objRQ_Mst_Customer
			////
			, out RT_Mst_Customer objRT_Mst_Customer
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Customer.Tid;
			objRT_Mst_Customer = new RT_Mst_Customer();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Customer_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_Mst_Customer)
				//, "Lst_UserOwner_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer.Lst_UserOwner_Customer)
				////
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
					DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Mst_Customer.Lst_Mst_Customer, "Mst_Customer");
					dsData.Tables.Add(dt_Mst_Customer);
				}
				#endregion

				#region // Mst_Customer_Update:
				mdsResult = Mst_Customer_Delete(
					objRQ_Mst_Customer.Tid // strTid
					, objRQ_Mst_Customer.GwUserCode // strGwUserCode
					, objRQ_Mst_Customer.GwPassword // strGwPassword
					, objRQ_Mst_Customer.WAUserCode // strUserCode
					, objRQ_Mst_Customer.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
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

        #region // Mst_Customer:

        //public DataSet WAS_Mst_Customer_UpdateSingle(
        //	ref ArrayList alParamsCoupleError
        //	, RQ_Mst_Customer objRQ_Mst_Customer
        //	////
        //	, out RT_Mst_Customer objRT_Mst_Customer
        //	)
        //{
        //	#region // Temp:
        //	string strTid = objRQ_Mst_Customer.Tid;
        //	objRT_Mst_Customer = new RT_Mst_Customer();
        //	DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
        //	DateTime dtimeSys = DateTime.UtcNow;
        //	//DataSet mdsExec = null;
        //	//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
        //	//int nTidSeq = 0;
        //	//bool bNeedTransaction = true;
        //	string strFunctionName = "WAS_Mst_Customer_UpdateSingle";
        //	string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Customer_UpdateSingle;
        //	alParamsCoupleError.AddRange(new object[]{
        //		"strFunctionName", strFunctionName
        //		, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
        //		////
        //		});
        //	#endregion

        //	try
        //	{
        //		#region // Init:
        //		#endregion

        //		#region // Refine and Check Input:
        //		List<Mst_Customer> lst_Mst_Customer = new List<Mst_Customer>();
        //		#endregion

        //		#region // Mst_Customer_UpdateSingle:
        //		mdsResult = Mst_Customer_UpdateSingle(
        //			objRQ_Mst_Customer.Tid // strTid
        //			, objRQ_Mst_Customer.GwUserCode // strGwUserCode
        //			, objRQ_Mst_Customer.GwPassword // strGwPassword
        //			, objRQ_Mst_Customer.WAUserCode // strUserCode
        //			, objRQ_Mst_Customer.WAUserPassword // strUserPassword
        //			, ref alParamsCoupleError // alParamsCoupleError
        //									  ////
        //			, objRQ_Mst_Customer.Mst_Customer.OrgID // objOrgID
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerCodeSys // objCustomerCodeSys
        //			, objRQ_Mst_Customer.Mst_Customer.NetworkID // objNetworkID
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerCode // objCustomerCode
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerType // objCustomerType
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerGrpCode // objCustomerGrpCode
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerSourceCode // objCustomerSourceCode
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerName // objCustomerName
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerNameEN // objCustomerNameEN
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerGender // objCustomerGender
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerPhoneNo // objCustomerPhoneNo
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerMobilePhone // objCustomerMobilePhone
        //			, objRQ_Mst_Customer.Mst_Customer.ProvinceCode // objProvinceCode
        //			, objRQ_Mst_Customer.Mst_Customer.DistrictCode // objDistrictCode
        //			, objRQ_Mst_Customer.Mst_Customer.WardCode // objWardCode
        //			, objRQ_Mst_Customer.Mst_Customer.AreaCode // objAreaCode
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerAvatarName // objCustomerAvatarName
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerAvatarPath // objCustomerAvatarPath
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerAddress // objCustomerAddress
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerEmail // objCustomerEmail
        //			, objRQ_Mst_Customer.Mst_Customer.CustomerDateOfBirth // objCustomerDateOfBirth
        //			, objRQ_Mst_Customer.Mst_Customer.GovIDType // objGovIDType
        //			, objRQ_Mst_Customer.Mst_Customer.GovIDCardNo // objGovIDCardNo
        //			, objRQ_Mst_Customer.Mst_Customer.GovIDCardDate // objGovIDCardDate
        //			, objRQ_Mst_Customer.Mst_Customer.GovIDCardPlace // objGovIDCardPlace
        //			, objRQ_Mst_Customer.Mst_Customer.TaxCode // objTaxCode
        //			, objRQ_Mst_Customer.Mst_Customer.BankCode // objBankCode
        //			, objRQ_Mst_Customer.Mst_Customer.BankName // objBankName
        //			, objRQ_Mst_Customer.Mst_Customer.BankAccountNo // objBankAccountNo
        //			, objRQ_Mst_Customer.Mst_Customer.RepresentName // objRepresentName
        //			, objRQ_Mst_Customer.Mst_Customer.RepresentPosition // objRepresentPosition
        //			, objRQ_Mst_Customer.Mst_Customer.UserCodeOwner // objUserCodeOwner
        //			, objRQ_Mst_Customer.Mst_Customer.ContactName // objContactName
        //			, objRQ_Mst_Customer.Mst_Customer.ContactPhone // objContactPhone
        //			, objRQ_Mst_Customer.Mst_Customer.ContactEmail // objContactEmail
        //			, objRQ_Mst_Customer.Mst_Customer.Fax // objFax
        //			, objRQ_Mst_Customer.Mst_Customer.Facebook // objFacebook
        //			, objRQ_Mst_Customer.Mst_Customer.InvoiceCustomerName // objInvoiceCustomerName
        //			, objRQ_Mst_Customer.Mst_Customer.InvoiceCustomerAddress // objInvoiceCustomerAddress
        //			, objRQ_Mst_Customer.Mst_Customer.InvoiceOrgName // objInvoiceOrgName
        //			, objRQ_Mst_Customer.Mst_Customer.InvoiceEmailSend // objInvoiceEmailSend
        //			, objRQ_Mst_Customer.Mst_Customer.MST // objMST
        //			, objRQ_Mst_Customer.Mst_Customer.ListOfCustDynamicFieldValue // objListOfCustDynamicFieldValue
        //			, objRQ_Mst_Customer.Mst_Customer.FlagDealer // objFlagDealer
        //			, objRQ_Mst_Customer.Mst_Customer.FlagSupplier // objFlagSupplier
        //			, objRQ_Mst_Customer.Mst_Customer.FlagEndUser // objFlagEndUser
        //			, objRQ_Mst_Customer.Mst_Customer.FlagBank // objFlagBank
        //			, objRQ_Mst_Customer.Mst_Customer.FlagInsurrance // objFlagInsurrance
        //			, objRQ_Mst_Customer.Mst_Customer.CreateDTimeUTC // objCreateDTimeUTC
        //			, objRQ_Mst_Customer.Mst_Customer.CreateBy // objCreateBy
        //			, objRQ_Mst_Customer.Mst_Customer.LUDTimeUTC // objLUDTimeUTC
        //			, objRQ_Mst_Customer.Mst_Customer.LUBy // objLUBy
        //			, objRQ_Mst_Customer.Mst_Customer.FlagActive // objFlagActive
        //			, objRQ_Mst_Customer.Mst_Customer.Remark // objRemark
        //			, objRQ_Mst_Customer.Mst_Customer.LogLUDTimeUTC // objLogLUDTimeUTC
        //			, objRQ_Mst_Customer.Mst_Customer.LogLUBy // objLogLUBy
        //													  ////
        //			, objRQ_Mst_Customer.Ft_Cols_Upd // objFt_Cols_Upd
        //			);
        //		#endregion

        //		// Return Good:
        //		return mdsResult;
        //	}
        //	catch (Exception exc)
        //	{
        //		#region // Catch of try:
        //		// Return Bad:
        //		return TUtils.CProcessExc.Process(
        //			ref mdsResult
        //			, exc
        //			, strErrorCodeDefault
        //			, alParamsCoupleError.ToArray()
        //			);
        //		#endregion
        //	}
        //	finally
        //	{
        //		#region // Finally of try:
        //		// Write ReturnLog:
        //		//_cf.ProcessBizReturn(
        //		//	ref mdsResult // mdsFinal
        //		//	, strTid // strTid
        //		//	, strFunctionName // strFunctionName
        //		//	);
        //		#endregion
        //	}
        //}
        //public DataSet Mst_Customer_UpdateSingle(
        //	string strTid
        //	, string strGwUserCode
        //	, string strGwPassword
        //	, string strWAUserCode
        //	, string strWAUserPassword
        //	, ref ArrayList alParamsCoupleError
        //   ////
        //   , object objOrgID
        //	, object objCustomerCodeSys
        //	, object objNetworkID
        //	, object objCustomerCode
        //	, object objCustomerType
        //	, object objCustomerGrpCode
        //	, object objCustomerSourceCode
        //	, object objCustomerName
        //	, object objCustomerNameEN
        //	, object objCustomerGender
        //	, object objCustomerPhoneNo
        //	, object objCustomerMobilePhone
        //	, object objProvinceCode
        //	, object objDistrictCode
        //	, object objWardCode
        //	, object objAreaCode
        //	, object objCustomerAvatarName
        //	, object objCustomerAvatarPath
        //	, object objCustomerAddress
        //	, object objCustomerEmail
        //	, object objCustomerDateOfBirth
        //	, object objGovIDType
        //	, object objGovIDCardNo
        //	, object objGovIDCardDate
        //	, object objGovIDCardPlace
        //	, object objTaxCode
        //	, object objBankCode
        //	, object objBankName
        //	, object objBankAccountNo
        //	, object objRepresentName
        //	, object objRepresentPosition
        //	, object objUserCodeOwner
        //	, object objContactName
        //	, object objContactPhone
        //	, object objContactEmail
        //	, object objFax
        //	, object objFacebook
        //	, object objInvoiceCustomerName
        //	, object objInvoiceCustomerAddress
        //	, object objInvoiceOrgName
        //	, object objInvoiceEmailSend
        //	, object objMST
        //	, object objListOfCustDynamicFieldValue
        //	, object objFlagDealer
        //	, object objFlagSupplier
        //	, object objFlagEndUser
        //	, object objFlagBank
        //	, object objFlagInsurrance
        //	, object objCreateDTimeUTC
        //	, object objCreateBy
        //	, object objLUDTimeUTC
        //	, object objLUBy
        //	, object objFlagActive
        //	, object objRemark
        //	, object objLogLUDTimeUTC
        //	, object objLogLUBy
        //	////
        //	, object objFt_Cols_Upd
        //	)
        //{
        //	#region // Temp:
        //	DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
        //	//int nTidSeq = 0;
        //	DateTime dtimeSys = DateTime.UtcNow;
        //	string strFunctionName = "Mst_Customer_UpdateSingle";
        //	string strErrorCodeDefault = TError.ErridnInventory.Mst_Customer_UpdateSingle;
        //	alParamsCoupleError.AddRange(new object[]{
        //			"strFunctionName", strFunctionName
        //			, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
        //	        ////
        //			, "objOrgID", objOrgID
        //			, "objCustomerCodeSys", objCustomerCodeSys
        //			, "objNetworkID", objNetworkID
        //			, "objCustomerCode", objCustomerCode
        //			, "objCustomerType", objCustomerType
        //			, "objCustomerGrpCode", objCustomerGrpCode
        //			, "objCustomerSourceCode", objCustomerSourceCode
        //			, "objCustomerName", objCustomerName
        //			, "objCustomerNameEN", objCustomerNameEN
        //			, "objCustomerGender", objCustomerGender
        //			, "objCustomerPhoneNo", objCustomerPhoneNo
        //			, "objCustomerMobilePhone", objCustomerMobilePhone
        //			, "objProvinceCode", objProvinceCode
        //			, "objDistrictCode", objDistrictCode
        //			, "objWardCode", objWardCode
        //			, "objAreaCode", objAreaCode
        //			, "objCustomerAvatarName", objCustomerAvatarName
        //			, "objCustomerAvatarPath", objCustomerAvatarPath
        //			, "objCustomerAddress", objCustomerAddress
        //			, "objCustomerEmail", objCustomerEmail
        //			, "objCustomerDateOfBirth", objCustomerDateOfBirth
        //			, "objGovIDType", objGovIDType
        //			, "objGovIDCardNo", objGovIDCardNo
        //			, "objGovIDCardDate", objGovIDCardDate
        //			, "objGovIDCardPlace", objGovIDCardPlace
        //			, "objTaxCode", objTaxCode
        //			, "objBankCode", objBankCode
        //			, "objBankName", objBankName
        //			, "objBankAccountNo", objBankAccountNo
        //			, "objRepresentName", objRepresentName
        //			, "objRepresentPosition", objRepresentPosition
        //			, "objUserCodeOwner", objUserCodeOwner
        //			, "objContactName", objContactName
        //			, "objContactPhone", objContactPhone
        //			, "objContactEmail", objContactEmail
        //			, "objFax", objFax
        //			, "objFacebook", objFacebook
        //			, "objInvoiceCustomerName", objInvoiceCustomerName
        //			, "objInvoiceCustomerAddress", objInvoiceCustomerAddress
        //			, "objInvoiceOrgName", objInvoiceOrgName
        //			, "objInvoiceEmailSend", objInvoiceEmailSend
        //			, "objMST", objMST
        //			, "objListOfCustDynamicFieldValue", objListOfCustDynamicFieldValue
        //			, "objFlagDealer", objFlagDealer
        //			, "objFlagSupplier", objFlagSupplier
        //			, "objFlagEndUser", objFlagEndUser
        //			, "objFlagBank", objFlagBank
        //			, "objFlagInsurrance", objFlagInsurrance
        //			, "objCreateDTimeUTC", objCreateDTimeUTC
        //			, "objCreateBy", objCreateBy
        //			, "objLUDTimeUTC", objLUDTimeUTC
        //			, "objLUBy", objLUBy
        //			, "objFlagActive", objFlagActive
        //			, "objRemark", objRemark
        //			, "objLogLUDTimeUTC", objLogLUDTimeUTC
        //			, "objLogLUBy", objLogLUBy
        //                  ////
        //                  , "objFt_Cols_Upd", objFt_Cols_Upd
        //			});
        //	#endregion

        //	try
        //	{
        //		#region // Init:
        //		//_cf.db.LogUserId = _cf.sinf.strUserCode;
        //		_cf.db.BeginTransaction();

        //		// Write RequestLog:
        //		_cf.ProcessBizReq_OutSide(
        //			strTid // strTid
        //			, strGwUserCode // strGwUserCode
        //			, strGwPassword // strGwPassword
        //			, strWAUserCode // objUserCode
        //			, strFunctionName // strFunctionName
        //			, alParamsCoupleError // alParamsCoupleError
        //			);

        //		// Sys_User_CheckAuthentication:
        //		//Sys_User_CheckAuthentication(
        //		//    ref alParamsCoupleError
        //		//    , strWAUserCode
        //		//    , strWAUserPassword
        //		//    );

        //		// Check Access/Deny:
        //		//Sys_Access_CheckDenyV30(
        //		//    ref alParamsCoupleError
        //		//    , strWAUserCode
        //		//    , strFunctionName
        //		//    );
        //		#endregion

        //		#region // Mst_Customer_UpdateSingleX:
        //		//DataSet dsGetData = null;
        //		{
        //			Mst_Customer_UpdateSingleX(
        //				strTid // strTid
        //				, strGwUserCode // strGwUserCode
        //				, strGwPassword // strGwPassword
        //				, strWAUserCode // strWAUserCode
        //				, strWAUserPassword // strWAUserPassword
        //				, ref alParamsCoupleError // alParamsCoupleError
        //				, dtimeSys // dtimeSys
        //						   ////
        //				, objOrgID // objOrgID
        //				, objCustomerCodeSys // objCustomerCodeSys
        //				, objNetworkID // objNetworkID
        //				, objCustomerCode // objCustomerCode
        //				, objCustomerType // objCustomerType
        //				, objCustomerGrpCode // objCustomerGrpCode
        //				, objCustomerSourceCode // objCustomerSourceCode
        //				, objCustomerName // objCustomerName
        //				, objCustomerNameEN // objCustomerNameEN
        //				, objCustomerGender // objCustomerGender
        //				, objCustomerPhoneNo // objCustomerPhoneNo
        //				, objCustomerMobilePhone // objCustomerMobilePhone
        //				, objProvinceCode // objProvinceCode
        //				, objDistrictCode // objDistrictCode
        //				, objWardCode // objWardCode
        //				, objAreaCode // objAreaCode
        //				, objCustomerAvatarName // objCustomerAvatarName
        //				, objCustomerAvatarPath // objCustomerAvatarPath
        //				, objCustomerAddress // objCustomerAddress
        //				, objCustomerEmail // objCustomerEmail
        //				, objCustomerDateOfBirth // objCustomerDateOfBirth
        //				, objGovIDType // objGovIDType
        //				, objGovIDCardNo // objGovIDCardNo
        //				, objGovIDCardDate // objGovIDCardDate
        //				, objGovIDCardPlace // objGovIDCardPlace
        //				, objTaxCode // objTaxCode
        //				, objBankCode // objBankCode
        //				, objBankName // objBankName
        //				, objBankAccountNo // objBankAccountNo
        //				, objRepresentName // objRepresentName
        //				, objRepresentPosition // objRepresentPosition
        //				, objUserCodeOwner // objUserCodeOwner
        //				, objContactName // objContactName
        //				, objContactPhone // objContactPhone
        //				, objContactEmail // objContactEmail
        //				, objFax // objFax
        //				, objFacebook // objFacebook
        //				, objInvoiceCustomerName // objInvoiceCustomerName
        //				, objInvoiceCustomerAddress // objInvoiceCustomerAddress
        //				, objInvoiceOrgName // objInvoiceOrgName
        //				, objInvoiceEmailSend // objInvoiceEmailSend
        //				, objMST // objMST
        //				, objListOfCustDynamicFieldValue // objListOfCustDynamicFieldValue
        //				, objFlagDealer // objFlagDealer
        //				, objFlagSupplier // objFlagSupplier
        //				, objFlagEndUser // objFlagEndUser
        //				, objFlagBank // objFlagBank
        //				, objFlagInsurrance // objFlagInsurrance
        //				, objCreateDTimeUTC // objCreateDTimeUTC
        //				, objCreateBy // objCreateBy
        //				, objLUDTimeUTC // objLUDTimeUTC
        //				, objLUBy // objLUBy
        //				, objFlagActive // objFlagActive
        //				, objRemark // objRemark
        //				, objLogLUDTimeUTC // objLogLUDTimeUTC
        //				, objLogLUBy // objLogLUBy
        //							 ////
        //				, objFt_Cols_Upd // objFt_Cols_Upd
        //				);
        //		}
        //		////
        //		//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
        //		#endregion

        //		// Return Good:
        //		TDALUtils.DBUtils.CommitSafety(_cf.db);
        //		mdsFinal.AcceptChanges();
        //		return mdsFinal;
        //	}
        //	catch (Exception exc)
        //	{
        //		#region // Catch of try:
        //		// Rollback:
        //		TDALUtils.DBUtils.RollbackSafety(_cf.db);

        //		// Return Bad:
        //		return TUtils.CProcessExc.Process(
        //			ref mdsFinal
        //			, exc
        //			, strErrorCodeDefault
        //			, alParamsCoupleError.ToArray()
        //			);
        //		#endregion
        //	}
        //	finally
        //	{
        //		#region // Finally of try:
        //		// Rollback and Release resources:
        //		TDALUtils.DBUtils.RollbackSafety(_cf.db);
        //		TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

        //		// Write ReturnLog:
        //		_cf.ProcessBizReturn_OutSide(
        //			ref mdsFinal // mdsFinal
        //			, strTid // strTid
        //			, strGwUserCode // strGwUserCode
        //			, strGwPassword // strGwPassword
        //			, strWAUserCode // objUserCode
        //			, strFunctionName // strFunctionName
        //			);
        //		#endregion
        //	}
        //}

        //private void Mst_Customer_UpdateSingleX(
        //	string strTid
        //	, string strGwUserCode
        //	, string strGwPassword
        //	, string strWAUserCode
        //	, string strWAUserPassword
        //	, ref ArrayList alParamsCoupleError
        //	, DateTime dtimeSys
        //   //// 
        //   , object objOrgID
        //	, object objCustomerCodeSys
        //	, object objNetworkID
        //	, object objCustomerCode
        //	, object objCustomerType
        //	, object objCustomerGrpCode
        //	, object objCustomerSourceCode
        //	, object objCustomerName
        //	, object objCustomerNameEN
        //	, object objCustomerGender
        //	, object objCustomerPhoneNo
        //	, object objCustomerMobilePhone
        //	, object objProvinceCode
        //	, object objDistrictCode
        //	, object objWardCode
        //	, object objAreaCode
        //	, object objCustomerAvatarName
        //	, object objCustomerAvatarPath
        //	, object objCustomerAddress
        //	, object objCustomerEmail
        //	, object objCustomerDateOfBirth
        //	, object objGovIDType
        //	, object objGovIDCardNo
        //	, object objGovIDCardDate
        //	, object objGovIDCardPlace
        //	, object objTaxCode
        //	, object objBankCode
        //	, object objBankName
        //	, object objBankAccountNo
        //	, object objRepresentName
        //	, object objRepresentPosition
        //	, object objUserCodeOwner
        //	, object objContactName
        //	, object objContactPhone
        //	, object objContactEmail
        //	, object objFax
        //	, object objFacebook
        //	, object objInvoiceCustomerName
        //	, object objInvoiceCustomerAddress
        //	, object objInvoiceOrgName
        //	, object objInvoiceEmailSend
        //	, object objMST
        //	, object objListOfCustDynamicFieldValue
        //	, object objFlagDealer
        //	, object objFlagSupplier
        //	, object objFlagEndUser
        //	, object objFlagBank
        //	, object objFlagInsurrance
        //	, object objCreateDTimeUTC
        //	, object objCreateBy
        //	, object objLUDTimeUTC
        //	, object objLUBy
        //	, object objFlagActive
        //	, object objRemark
        //	, object objLogLUDTimeUTC
        //	, object objLogLUBy
        //	////
        //	, object objFt_Cols_Upd
        //	////
        //	)
        //{
        //	#region // Temp:
        //	string strFunctionName = "Mst_Customer_UpdateSingleX";
        //	//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
        //	alParamsCoupleError.AddRange(new object[]{
        //		"strFunctionName", strFunctionName
        //		, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
        //		////
        //              , "objOrgID", objOrgID
        //		, "objCustomerCodeSys", objCustomerCodeSys
        //		, "objNetworkID", objNetworkID
        //		, "objCustomerCode", objCustomerCode
        //		, "objCustomerType", objCustomerType
        //		, "objCustomerGrpCode", objCustomerGrpCode
        //		, "objCustomerSourceCode", objCustomerSourceCode
        //		, "objCustomerName", objCustomerName
        //		, "objCustomerNameEN", objCustomerNameEN
        //		, "objCustomerGender", objCustomerGender
        //		, "objCustomerPhoneNo", objCustomerPhoneNo
        //		, "objCustomerMobilePhone", objCustomerMobilePhone
        //		, "objProvinceCode", objProvinceCode
        //		, "objDistrictCode", objDistrictCode
        //		, "objWardCode", objWardCode
        //		, "objAreaCode", objAreaCode
        //		, "objCustomerAvatarName", objCustomerAvatarName
        //		, "objCustomerAvatarPath", objCustomerAvatarPath
        //		, "objCustomerAddress", objCustomerAddress
        //		, "objCustomerEmail", objCustomerEmail
        //		, "objCustomerDateOfBirth", objCustomerDateOfBirth
        //		, "objGovIDType", objGovIDType
        //		, "objGovIDCardNo", objGovIDCardNo
        //		, "objGovIDCardDate", objGovIDCardDate
        //		, "objGovIDCardPlace", objGovIDCardPlace
        //		, "objTaxCode", objTaxCode
        //		, "objBankCode", objBankCode
        //		, "objBankName", objBankName
        //		, "objBankAccountNo", objBankAccountNo
        //		, "objRepresentName", objRepresentName
        //		, "objRepresentPosition", objRepresentPosition
        //		, "objUserCodeOwner", objUserCodeOwner
        //		, "objContactName", objContactName
        //		, "objContactPhone", objContactPhone
        //		, "objContactEmail", objContactEmail
        //		, "objFax", objFax
        //		, "objFacebook", objFacebook
        //		, "objInvoiceCustomerName", objInvoiceCustomerName
        //		, "objInvoiceCustomerAddress", objInvoiceCustomerAddress
        //		, "objInvoiceOrgName", objInvoiceOrgName
        //		, "objInvoiceEmailSend", objInvoiceEmailSend
        //		, "objMST", objMST
        //		, "objListOfCustDynamicFieldValue", objListOfCustDynamicFieldValue
        //		, "objFlagDealer", objFlagDealer
        //		, "objFlagSupplier", objFlagSupplier
        //		, "objFlagEndUser", objFlagEndUser
        //		, "objFlagBank", objFlagBank
        //		, "objFlagInsurrance", objFlagInsurrance
        //		, "objCreateDTimeUTC", objCreateDTimeUTC
        //		, "objCreateBy", objCreateBy
        //		, "objLUDTimeUTC", objLUDTimeUTC
        //		, "objLUBy", objLUBy
        //		, "objFlagActive", objFlagActive
        //		, "objRemark", objRemark
        //		, "objLogLUDTimeUTC", objLogLUDTimeUTC
        //		, "objLogLUBy", objLogLUBy
        //              ////
        //              , "objFt_Cols_Upd", objFt_Cols_Upd
        //		});
        //	#endregion

        //	#region // Refine and Check Input:
        //	////
        //	string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
        //	strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
        //	////
        //	string strOrgID = TUtils.CUtils.StdParam(objOrgID);
        //	string strCustomerCodeSys = TUtils.CUtils.StdParam(objCustomerCodeSys);
        //	string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
        //	string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
        //	string strCustomerType = TUtils.CUtils.StdParam(objCustomerType);
        //	string strCustomerGrpCode = TUtils.CUtils.StdParam(objCustomerGrpCode);
        //	string strCustomerSourceCode = TUtils.CUtils.StdParam(objCustomerSourceCode);
        //	string strCustomerName = TUtils.CUtils.StdParam(objCustomerName);
        //	string strCustomerNameEN = TUtils.CUtils.StdParam(objCustomerNameEN);
        //	string strCustomerGender = TUtils.CUtils.StdParam(objCustomerGender);
        //	string strCustomerPhoneNo = TUtils.CUtils.StdParam(objCustomerPhoneNo);
        //	string strCustomerMobilePhone = TUtils.CUtils.StdParam(objCustomerMobilePhone);
        //	string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
        //	string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
        //	string strWardCode = TUtils.CUtils.StdParam(objWardCode);
        //	string strAreaCode = TUtils.CUtils.StdParam(objAreaCode);
        //	string strCustomerAvatarName = TUtils.CUtils.StdParam(objCustomerAvatarName);
        //	string strCustomerAvatarPath = TUtils.CUtils.StdParam(objCustomerAvatarPath);
        //	string strCustomerAddress = TUtils.CUtils.StdParam(objCustomerAddress);
        //	string strCustomerEmail = TUtils.CUtils.StdParam(objCustomerEmail);
        //	string strCustomerDateOfBirth = TUtils.CUtils.StdParam(objCustomerDateOfBirth);
        //	string strGovIDType = TUtils.CUtils.StdParam(objGovIDType);
        //	string strGovIDCardNo = TUtils.CUtils.StdParam(objGovIDCardNo);
        //	string strGovIDCardDate = TUtils.CUtils.StdParam(objGovIDCardDate);
        //	string strGovIDCardPlace = TUtils.CUtils.StdParam(objGovIDCardPlace);
        //	string strTaxCode = TUtils.CUtils.StdParam(objTaxCode);
        //	string strBankCode = TUtils.CUtils.StdParam(objBankCode);
        //	string strBankName = TUtils.CUtils.StdParam(objBankName);
        //	string strBankAccountNo = TUtils.CUtils.StdParam(objBankAccountNo);
        //	string strRepresentName = TUtils.CUtils.StdParam(objRepresentName);
        //	string strRepresentPosition = TUtils.CUtils.StdParam(objRepresentPosition);
        //	string strUserCodeOwner = TUtils.CUtils.StdParam(objUserCodeOwner);
        //	string strContactName = TUtils.CUtils.StdParam(objContactName);
        //	string strContactPhone = TUtils.CUtils.StdParam(objContactPhone);
        //	string strContactEmail = TUtils.CUtils.StdParam(objContactEmail);
        //	string strFax = TUtils.CUtils.StdParam(objFax);
        //	string strFacebook = TUtils.CUtils.StdParam(objFacebook);
        //	string strInvoiceCustomerName = TUtils.CUtils.StdParam(objInvoiceCustomerName);
        //	string strInvoiceCustomerAddress = TUtils.CUtils.StdParam(objInvoiceCustomerAddress);
        //	string strInvoiceOrgName = TUtils.CUtils.StdParam(objInvoiceOrgName);
        //	string strInvoiceEmailSend = TUtils.CUtils.StdParam(objInvoiceEmailSend);
        //	string strMST = TUtils.CUtils.StdParam(objMST);
        //	string strListOfCustDynamicFieldValue = TUtils.CUtils.StdParam(objListOfCustDynamicFieldValue);
        //	string strFlagDealer = TUtils.CUtils.StdParam(objFlagDealer);
        //	string strFlagSupplier = TUtils.CUtils.StdParam(objFlagSupplier);
        //	string strFlagEndUser = TUtils.CUtils.StdParam(objFlagEndUser);
        //	string strFlagBank = TUtils.CUtils.StdParam(objFlagBank);
        //	string strFlagInsurrance = TUtils.CUtils.StdParam(objFlagInsurrance);
        //	string strCreateDTimeUTC = TUtils.CUtils.StdParam(objCreateDTimeUTC);
        //	string strCreateBy = TUtils.CUtils.StdParam(objCreateBy);
        //	string strLUDTimeUTC = TUtils.CUtils.StdParam(objLUDTimeUTC);
        //	string strLUBy = TUtils.CUtils.StdParam(objLUBy);
        //	string strFlagActive = TUtils.CUtils.StdParam(objFlagActive);
        //	string strRemark = TUtils.CUtils.StdParam(objRemark);
        //	string strLogLUDTimeUTC = TUtils.CUtils.StdParam(objLogLUDTimeUTC);
        //	string strLogLUBy = TUtils.CUtils.StdParam(objLogLUBy);
        //	////
        //	bool bUpd_OrgID = strFt_Cols_Upd.Contains("Mst_Customer.OrgID".ToUpper());
        //	bool bUpd_CustomerCodeSys = strFt_Cols_Upd.Contains("Mst_Customer.CustomerCodeSys".ToUpper());
        //	bool bUpd_NetworkID = strFt_Cols_Upd.Contains("Mst_Customer.NetworkID".ToUpper());
        //	bool bUpd_CustomerCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerCode".ToUpper());
        //	bool bUpd_CustomerType = strFt_Cols_Upd.Contains("Mst_Customer.CustomerType".ToUpper());
        //	bool bUpd_CustomerGrpCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerGrpCode".ToUpper());
        //	bool bUpd_CustomerSourceCode = strFt_Cols_Upd.Contains("Mst_Customer.CustomerSourceCode".ToUpper());
        //	bool bUpd_CustomerName = strFt_Cols_Upd.Contains("Mst_Customer.CustomerName".ToUpper());
        //	bool bUpd_CustomerNameEN = strFt_Cols_Upd.Contains("Mst_Customer.CustomerNameEN".ToUpper());
        //	bool bUpd_CustomerGender = strFt_Cols_Upd.Contains("Mst_Customer.CustomerGender".ToUpper());
        //	bool bUpd_CustomerPhoneNo = strFt_Cols_Upd.Contains("Mst_Customer.CustomerPhoneNo".ToUpper());
        //	bool bUpd_CustomerMobilePhone = strFt_Cols_Upd.Contains("Mst_Customer.CustomerMobilePhone".ToUpper());
        //	bool bUpd_ProvinceCode = strFt_Cols_Upd.Contains("Mst_Customer.ProvinceCode".ToUpper());
        //	bool bUpd_DistrictCode = strFt_Cols_Upd.Contains("Mst_Customer.DistrictCode".ToUpper());
        //	bool bUpd_WardCode = strFt_Cols_Upd.Contains("Mst_Customer.WardCode".ToUpper());
        //	bool bUpd_AreaCode = strFt_Cols_Upd.Contains("Mst_Customer.AreaCode".ToUpper());
        //	bool bUpd_CustomerAvatarName = strFt_Cols_Upd.Contains("Mst_Customer.CustomerAvatarName".ToUpper());
        //	bool bUpd_CustomerAvatarPath = strFt_Cols_Upd.Contains("Mst_Customer.CustomerAvatarPath".ToUpper());
        //	bool bUpd_CustomerAddress = strFt_Cols_Upd.Contains("Mst_Customer.CustomerAddress".ToUpper());
        //	bool bUpd_CustomerEmail = strFt_Cols_Upd.Contains("Mst_Customer.CustomerEmail".ToUpper());
        //	bool bUpd_CustomerDateOfBirth = strFt_Cols_Upd.Contains("Mst_Customer.CustomerDateOfBirth".ToUpper());
        //	bool bUpd_GovIDType = strFt_Cols_Upd.Contains("Mst_Customer.GovIDType".ToUpper());
        //	bool bUpd_GovIDCardNo = strFt_Cols_Upd.Contains("Mst_Customer.GovIDCardNo".ToUpper());
        //	bool bUpd_GovIDCardDate = strFt_Cols_Upd.Contains("Mst_Customer.GovIDCardDate".ToUpper());
        //	bool bUpd_GovIDCardPlace = strFt_Cols_Upd.Contains("Mst_Customer.GovIDCardPlace".ToUpper());
        //	bool bUpd_TaxCode = strFt_Cols_Upd.Contains("Mst_Customer.TaxCode".ToUpper());
        //	bool bUpd_BankCode = strFt_Cols_Upd.Contains("Mst_Customer.BankCode".ToUpper());
        //	bool bUpd_BankName = strFt_Cols_Upd.Contains("Mst_Customer.BankName".ToUpper());
        //	bool bUpd_BankAccountNo = strFt_Cols_Upd.Contains("Mst_Customer.BankAccountNo".ToUpper());
        //	bool bUpd_RepresentName = strFt_Cols_Upd.Contains("Mst_Customer.RepresentName".ToUpper());
        //	bool bUpd_RepresentPosition = strFt_Cols_Upd.Contains("Mst_Customer.RepresentPosition".ToUpper());
        //	bool bUpd_UserCodeOwner = strFt_Cols_Upd.Contains("Mst_Customer.UserCodeOwner".ToUpper());
        //	bool bUpd_ContactName = strFt_Cols_Upd.Contains("Mst_Customer.ContactName".ToUpper());
        //	bool bUpd_ContactPhone = strFt_Cols_Upd.Contains("Mst_Customer.ContactPhone".ToUpper());
        //	bool bUpd_ContactEmail = strFt_Cols_Upd.Contains("Mst_Customer.ContactEmail".ToUpper());
        //	bool bUpd_Fax = strFt_Cols_Upd.Contains("Mst_Customer.Fax".ToUpper());
        //	bool bUpd_Facebook = strFt_Cols_Upd.Contains("Mst_Customer.Facebook".ToUpper());
        //	bool bUpd_InvoiceCustomerName = strFt_Cols_Upd.Contains("Mst_Customer.InvoiceCustomerName".ToUpper());
        //	bool bUpd_InvoiceCustomerAddress = strFt_Cols_Upd.Contains("Mst_Customer.InvoiceCustomerAddress".ToUpper());
        //	bool bUpd_InvoiceOrgName = strFt_Cols_Upd.Contains("Mst_Customer.InvoiceOrgName".ToUpper());
        //	bool bUpd_InvoiceEmailSend = strFt_Cols_Upd.Contains("Mst_Customer.InvoiceEmailSend".ToUpper());
        //	bool bUpd_MST = strFt_Cols_Upd.Contains("Mst_Customer.MST".ToUpper());
        //	bool bUpd_ListOfCustDynamicFieldValue = strFt_Cols_Upd.Contains("Mst_Customer.ListOfCustDynamicFieldValue".ToUpper());
        //	bool bUpd_FlagDealer = strFt_Cols_Upd.Contains("Mst_Customer.FlagDealer".ToUpper());
        //	bool bUpd_FlagSupplier = strFt_Cols_Upd.Contains("Mst_Customer.FlagSupplier".ToUpper());
        //	bool bUpd_FlagEndUser = strFt_Cols_Upd.Contains("Mst_Customer.FlagEndUser".ToUpper());
        //	bool bUpd_FlagBank = strFt_Cols_Upd.Contains("Mst_Customer.FlagBank".ToUpper());
        //	bool bUpd_FlagInsurrance = strFt_Cols_Upd.Contains("Mst_Customer.FlagInsurrance".ToUpper());
        //	bool bUpd_CreateDTimeUTC = strFt_Cols_Upd.Contains("Mst_Customer.CreateDTimeUTC".ToUpper());
        //	bool bUpd_CreateBy = strFt_Cols_Upd.Contains("Mst_Customer.CreateBy".ToUpper());
        //	bool bUpd_LUDTimeUTC = strFt_Cols_Upd.Contains("Mst_Customer.LUDTimeUTC".ToUpper());
        //	bool bUpd_LUBy = strFt_Cols_Upd.Contains("Mst_Customer.LUBy".ToUpper());
        //	bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Customer.FlagActive".ToUpper());
        //	bool bUpd_Remark = strFt_Cols_Upd.Contains("Mst_Customer.Remark".ToUpper());
        //	bool bUpd_LogLUDTimeUTC = strFt_Cols_Upd.Contains("Mst_Customer.LogLUDTimeUTC".ToUpper());
        //	bool bUpd_LogLUBy = strFt_Cols_Upd.Contains("Mst_Customer.LogLUBy".ToUpper());

        //	////
        //	DataTable dtDB_Mst_Customer = null;
        //	{
        //		////
        //		Mst_Customer_CheckDB(
        //			ref alParamsCoupleError // alParamsCoupleError
        //			, strOrgID // strOrgID 
        //			, TConst.Flag.Yes // strFlagExistToCheck
        //			, "" // strFlagActiveListToCheck
        //			, out dtDB_Mst_Customer // dtDB_Mst_Customer
        //			);
        //		////
        //		if (bUpd_OrganName && string.IsNullOrEmpty(strOrganName))
        //		{
        //			alParamsCoupleError.AddRange(new object[]{
        //				"Check.strOrganName", strOrganName
        //				});
        //			throw CmUtils.CMyException.Raise(
        //				TError.ErridnInventory.Mst_Customer_UpdateSingleX_InvalidOrganName
        //				, null
        //				, alParamsCoupleError.ToArray()
        //				);
        //		}
        //	}
        //	#endregion

        //	#region // Save Mst_Customer:
        //	{
        //		// Init:
        //		ArrayList alColumnEffective = new ArrayList();
        //		string strFN = "";
        //		DataRow drDB = dtDB_Mst_Customer.Rows[0];
        //		if (bUpd_OrgID) { strFN = "OrgID"; drDB[strFN] = strOrgID; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerCodeSys) { strFN = "CustomerCodeSys"; drDB[strFN] = strCustomerCodeSys; alColumnEffective.Add(strFN); }
        //		if (bUpd_NetworkID) { strFN = "NetworkID"; drDB[strFN] = nNetworkID; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerCode) { strFN = "CustomerCode"; drDB[strFN] = strCustomerCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerType) { strFN = "CustomerType"; drDB[strFN] = strCustomerType; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerGrpCode) { strFN = "CustomerGrpCode"; drDB[strFN] = strCustomerGrpCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerSourceCode) { strFN = "CustomerSourceCode"; drDB[strFN] = strCustomerSourceCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerName) { strFN = "CustomerName"; drDB[strFN] = strCustomerName; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerNameEN) { strFN = "CustomerNameEN"; drDB[strFN] = strCustomerNameEN; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerGender) { strFN = "CustomerGender"; drDB[strFN] = strCustomerGender; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerPhoneNo) { strFN = "CustomerPhoneNo"; drDB[strFN] = strCustomerPhoneNo; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerMobilePhone) { strFN = "CustomerMobilePhone"; drDB[strFN] = strCustomerMobilePhone; alColumnEffective.Add(strFN); }
        //		if (bUpd_ProvinceCode) { strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_DistrictCode) { strFN = "DistrictCode"; drDB[strFN] = strDistrictCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_WardCode) { strFN = "WardCode"; drDB[strFN] = strWardCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_AreaCode) { strFN = "AreaCode"; drDB[strFN] = strAreaCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerAvatarName) { strFN = "CustomerAvatarName"; drDB[strFN] = strCustomerAvatarName; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerAvatarPath) { strFN = "CustomerAvatarPath"; drDB[strFN] = strCustomerAvatarPath; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerAddress) { strFN = "CustomerAddress"; drDB[strFN] = strCustomerAddress; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerEmail) { strFN = "CustomerEmail"; drDB[strFN] = strCustomerEmail; alColumnEffective.Add(strFN); }
        //		if (bUpd_CustomerDateOfBirth) { strFN = "CustomerDateOfBirth"; drDB[strFN] = strCustomerDateOfBirth; alColumnEffective.Add(strFN); }
        //		if (bUpd_GovIDType) { strFN = "GovIDType"; drDB[strFN] = strGovIDType; alColumnEffective.Add(strFN); }
        //		if (bUpd_GovIDCardNo) { strFN = "GovIDCardNo"; drDB[strFN] = strGovIDCardNo; alColumnEffective.Add(strFN); }
        //		if (bUpd_GovIDCardDate) { strFN = "GovIDCardDate"; drDB[strFN] = strGovIDCardDate; alColumnEffective.Add(strFN); }
        //		if (bUpd_GovIDCardPlace) { strFN = "GovIDCardPlace"; drDB[strFN] = strGovIDCardPlace; alColumnEffective.Add(strFN); }
        //		if (bUpd_TaxCode) { strFN = "TaxCode"; drDB[strFN] = strTaxCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_BankCode) { strFN = "BankCode"; drDB[strFN] = strBankCode; alColumnEffective.Add(strFN); }
        //		if (bUpd_BankName) { strFN = "BankName"; drDB[strFN] = strBankName; alColumnEffective.Add(strFN); }
        //		if (bUpd_BankAccountNo) { strFN = "BankAccountNo"; drDB[strFN] = strBankAccountNo; alColumnEffective.Add(strFN); }
        //		if (bUpd_RepresentName) { strFN = "RepresentName"; drDB[strFN] = strRepresentName; alColumnEffective.Add(strFN); }
        //		if (bUpd_RepresentPosition) { strFN = "RepresentPosition"; drDB[strFN] = strRepresentPosition; alColumnEffective.Add(strFN); }
        //		if (bUpd_UserCodeOwner) { strFN = "UserCodeOwner"; drDB[strFN] = strUserCodeOwner; alColumnEffective.Add(strFN); }
        //		if (bUpd_ContactName) { strFN = "ContactName"; drDB[strFN] = strContactName; alColumnEffective.Add(strFN); }
        //		if (bUpd_ContactPhone) { strFN = "ContactPhone"; drDB[strFN] = strContactPhone; alColumnEffective.Add(strFN); }
        //		if (bUpd_ContactEmail) { strFN = "ContactEmail"; drDB[strFN] = strContactEmail; alColumnEffective.Add(strFN); }
        //		if (bUpd_Fax) { strFN = "Fax"; drDB[strFN] = strFax; alColumnEffective.Add(strFN); }
        //		if (bUpd_Facebook) { strFN = "Facebook"; drDB[strFN] = strFacebook; alColumnEffective.Add(strFN); }
        //		if (bUpd_InvoiceCustomerName) { strFN = "InvoiceCustomerName"; drDB[strFN] = strInvoiceCustomerName; alColumnEffective.Add(strFN); }
        //		if (bUpd_InvoiceCustomerAddress) { strFN = "InvoiceCustomerAddress"; drDB[strFN] = strInvoiceCustomerAddress; alColumnEffective.Add(strFN); }
        //		if (bUpd_InvoiceOrgName) { strFN = "InvoiceOrgName"; drDB[strFN] = strInvoiceOrgName; alColumnEffective.Add(strFN); }
        //		if (bUpd_InvoiceEmailSend) { strFN = "InvoiceEmailSend"; drDB[strFN] = strInvoiceEmailSend; alColumnEffective.Add(strFN); }
        //		if (bUpd_MST) { strFN = "MST"; drDB[strFN] = strMST; alColumnEffective.Add(strFN); }
        //		if (bUpd_ListOfCustDynamicFieldValue) { strFN = "ListOfCustDynamicFieldValue"; drDB[strFN] = strListOfCustDynamicFieldValue; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagDealer) { strFN = "FlagDealer"; drDB[strFN] = strFlagDealer; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagSupplier) { strFN = "FlagSupplier"; drDB[strFN] = strFlagSupplier; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagEndUser) { strFN = "FlagEndUser"; drDB[strFN] = strFlagEndUser; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagBank) { strFN = "FlagBank"; drDB[strFN] = strFlagBank; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagInsurrance) { strFN = "FlagInsurrance"; drDB[strFN] = strFlagInsurrance; alColumnEffective.Add(strFN); }
        //		if (bUpd_CreateDTimeUTC) { strFN = "CreateDTimeUTC"; drDB[strFN] = strCreateDTimeUTC; alColumnEffective.Add(strFN); }
        //		if (bUpd_CreateBy) { strFN = "CreateBy"; drDB[strFN] = strCreateBy; alColumnEffective.Add(strFN); }
        //		if (bUpd_LUDTimeUTC) { strFN = "LUDTimeUTC"; drDB[strFN] = strLUDTimeUTC; alColumnEffective.Add(strFN); }
        //		if (bUpd_LUBy) { strFN = "LUBy"; drDB[strFN] = strLUBy; alColumnEffective.Add(strFN); }
        //		if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
        //		if (bUpd_Remark) { strFN = "Remark"; drDB[strFN] = strRemark; alColumnEffective.Add(strFN); }
        //		if (bUpd_LogLUDTimeUTC) { strFN = "LogLUDTimeUTC"; drDB[strFN] = strLogLUDTimeUTC; alColumnEffective.Add(strFN); }
        //		if (bUpd_LogLUBy) { strFN = "LogLUBy"; drDB[strFN] = strLogLUBy; alColumnEffective.Add(strFN); }
        //		strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
        //		strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

        //		// Save:
        //		_cf.db.SaveData(
        //			"Mst_Customer"
        //			, dtDB_Mst_Customer
        //			, alColumnEffective.ToArray()
        //			);
        //	}
        //	#endregion
        //}
        #endregion

        #region // Mst_CustomerInArea:
        public void Mst_CustomerInArea_SaveX(
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
            string strFunctionName = "Mst_CustomerInArea_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Mst_CustomerInArea_SaveAllX;
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

            #region // Refine and Check Input Mst_CustomerInArea:
            ////

            ////
            DataTable dtInput_Mst_CustomerInArea = null;
            {
                ////
                string strTableCheck = "Mst_CustomerInArea";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerInArea_SaveX_Input_BrandTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerInArea = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_CustomerInArea.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerInArea_SaveX_Input_BrandTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerInArea // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "StdParam", "AreaCode" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
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
                    , "#input_Mst_CustomerInArea" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "CustomerCodeSys", TConst.BizMix.Default_DBColType
                            , "AreaCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_CustomerInArea // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Mst_CustomerInArea:
							    delete t
							    from Mst_CustomerInArea t --//[mylock]
								    inner join #input_Mst_CustomerInArea f --//[mylock]
									    on t.OrgID = f.OrgID
                                            and t.CustomerCodeSys = f.CustomerCodeSys
                                            --and t.AreaCode = f.AreaCode
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
                        string zzzzClauseInsert_Mst_CustomerInArea_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Mst_CustomerInArea
                                (
                                    OrgID
                                    , CustomerCodeSys
                                    , AreaCode
                                    , NetworkID
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.OrgID
                                    , t.CustomerCodeSys
                                    , t.AreaCode
                                    , t.NetworkID
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Mst_CustomerInArea t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Mst_CustomerInArea_zSave
							"
                            , "zzzzClauseInsert_Mst_CustomerInArea_zSave", zzzzClauseInsert_Mst_CustomerInArea_zSave
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
						        drop table #input_Mst_CustomerInArea;
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
						        drop table #input_Mst_CustomerInArea;
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

        public DataSet Mst_CustomerInArea_Save(
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
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerInArea_Save;
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
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Invoice_Invoice_SaveX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerInArea_SaveX(
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

        public DataSet WAS_Mst_CustomerInArea_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerInArea objRQ_Mst_CustomerInArea
            ////
            , out RT_Mst_CustomerInArea objRT_Mst_CustomerInArea
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerInArea.Tid;
            objRT_Mst_CustomerInArea = new RT_Mst_CustomerInArea();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerInArea.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerInArea_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerInArea_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Mst_CustomerInArea.FlagIsDelete
                , "Lst_Mst_CustomerInArea", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerInArea.Lst_Mst_CustomerInArea)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_CustomerInArea> lst_Mst_CustomerInArea = new List<Mst_CustomerInArea>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_CustomerInArea = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerInArea>(objRQ_Mst_CustomerInArea.Lst_Mst_CustomerInArea, "Mst_CustomerInArea");
                    dsData.Tables.Add(dt_Mst_CustomerInArea);
                    ////
                }
                #endregion

                #region // WS_Mst_CustomerInArea_Create: 
                // Mst_CustomerInArea_Save_Root_New20190704
                mdsResult = Mst_CustomerInArea_Save(
                    objRQ_Mst_CustomerInArea.Tid // strTid
                    , objRQ_Mst_CustomerInArea.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerInArea.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerInArea.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerInArea.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerInArea.FlagIsDelete // objFlagIsDelete
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
    }
}
