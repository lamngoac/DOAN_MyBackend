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
        #region // Report:
        private void Rpt_InvBalLot_MaxExpiredDateByInvX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , string strOrgID // strOrgID
            , DataSet dsData
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvBalLot_MaxExpiredDateByInvX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Check:
            string strSysDate = dtimeSys.ToString("yyyy-MM-dd");
            /////
            #endregion

            #region //// Refine and Check Input Mst_Product:
            ////
            DataTable dtInput_Mst_Product = null;
            {
                ////
                string strTableCheck = "Mst_Product";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Product = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Product // dtData
                                        //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeUser" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Product.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Product:
            if (dtInput_Mst_Product.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Product" // strTableName
                    , new object[] {
                            "ProductCodeUser", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Product // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from Mst_Product t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_InvBalLot_MaxExpiredDateByInvX_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_ProductGroup:
                        select distinct
	                        t.ProductGrpCode
	                        , t.OrgID
                        into #input_Mst_ProductGroup
                        from Mst_ProductGroup t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ----- #input_Mst_Product_Filter:
                        select distinct
	                        t.ProductCode
	                        , t.OrgID
                        into #input_Mst_Product_Filter
                        from Mst_Product t --//[mylock]
	                        inner join #input_Mst_Product f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeUser = f.ProductCodeUser
                        where(1=1)
                        ;

						---- #tbl_Mst_Inventory_Filter:
						select 
							t.OrgID
							, t.InvCode
							, t.InvBUPattern
							, t.InvBUCode
						into #tbl_Mst_Inventory_Filter
						from Mst_Inventory t --//[mylock]
							inner join #input_Mst_Inventory f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
						where(1=1)
							and t.FlagIn_Out = '1'
						;

						--- #tbl_Mst_Inventory_Full:
						select distinct
							t.InvCode
							, t.OrgID
						into #tbl_Mst_Inventory_Full
						from Mst_Inventory t --//[mylock]
							inner join #tbl_Mst_Inventory_Filter f --//[mylock]
								on t.OrgID = f.OrgID
						where(1=1)
							and t.InvBUCode like f.InvBUPattern
						; 

						---- #tbl_Mst_Inventory_InvCodeInv:
						select 
							t.OrgID
							, t.InvCode
							, (
								select top 1 
									f.InvCode
								from Mst_Inventory f --//[mylock]
								where(1=1)
									and t.OrgID = f.OrgID
									and mi.InvBUCode like f.InvBUPattern     
									and f.FlagIn_Out = '1'
									and f.InvCodeParent is not null                                                                                                                                 
							) InvCode_Inv
						into #tbl_Mst_Inventory_InvCodeInv
						from #tbl_Mst_Inventory_Full t --//[mylock]
							inner join Mst_Inventory mi --//[mylock]
								on t.OrgID = mi.OrgID
									and t.InvCode = mi.InvCode
							inner join Inv_InventoryBalanceLot iibl --//[mylock]
								on mi.OrgID = iibl.OrgID
									and mi.InvCode = iibl.InvCode
	                        inner join Mst_Product mp --//[mylock]
		                        on iibl.OrgID = mp.OrgID
			                        and iibl.ProductCode = mp.ProductCode	
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
						where(1=1)
						;

						---- #tbl_Summary:
						select 
							iibl.OrgID
							, iibl.ProductCode
							, iibl.ProductLotNo
							, t.InvCode_Inv InvCode
							, max(iibl.LastInInvDTimeUTC) LastInInvDTimeUTC
							, sum(iibl.QtyTotalOK) TotalQtyTotalOK
							, sum(iibl.QtyBlockOK) TotalQtyBlockOK
							, sum(iibl.QtyAvailOK) TotalQtyAvailOK
							, max(iibl.ExpiredDate) MaxExpiredDate
						into #tbl_Summary
						from Inv_InventoryBalanceLot iibl --//[mylock]
							inner join #tbl_Mst_Inventory_InvCodeInv t --//[mylock]
								on iibl.OrgID = t.OrgID
									and iibl.InvCode = t.InvCode
							inner join #input_Mst_Product_Filter k --//[mylock]
								on iibl.OrgID = k.OrgID
									and iibl.ProductCode = k.ProductCode
						where(1=1)
						group by 
							iibl.OrgID
							, iibl.ProductCode
							, iibl.ProductLotNo
							, t.InvCode_Inv
						;

						--- Return:
						select 
							t.OrgID
							, t.ProductCode
							, f.ProductCodeUser
							, f.ProductName
							, f.UnitCode
							, t.ProductLotNo
							, t.InvCode
							, k.ProductGrpCode
							, k.ProductGrpName
							, Left(t.LastInInvDTimeUTC, 10) LastInInvDate
							, t.MaxExpiredDate
							, IsNull(DATEDIFF(day, Left(t.LastInInvDTimeUTC, 10), '@strSysDate'), 0.0) QtyDayInv
							, t.TotalQtyTotalOK
							, t.TotalQtyBlockOK
							, t.TotalQtyAvailOK
						from #tbl_Summary t --//[mylock]
							inner join Mst_Product f --//[mylock]
								on t.OrgID = f.OrgID
									and t.ProductCode = f.ProductCode
	                        inner join Mst_ProductGroup k --//[mylock]
		                        on f.OrgID = k.OrgID
			                        and f.ProductGrpCode = k.ProductGrpCode
						where(1=1)
						;
	                   

                        ---- Clear For Debug:
                        drop table #input_Mst_Inventory;
                        drop table #input_Mst_Product;
                        drop table #input_Mst_ProductGroup;
						drop table #input_Mst_Product_Filter;
						drop table #tbl_Mst_Inventory_Filter;
						drop table #tbl_Mst_Inventory_Full;
						drop table #tbl_Mst_Inventory_InvCodeInv;
						drop table #tbl_Summary;	
					"
                    , "@strSysDate", strSysDate
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvBalLot_MaxExpiredDateByInv";
            #endregion
        }

        public DataSet Rpt_InvBalLot_MaxExpiredDateByInv(
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
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvBalLot_MaxExpiredDateByInv";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvBalLot_MaxExpiredDateByInv;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
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
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Rpt_InvBalLot_MaxExpiredDateByInvX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvBalLot_MaxExpiredDateByInvX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID_RQ // strOrgID_RQ
                        , dsData // dsData
                                 ////
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_InvBalLot_MaxExpiredDateByInv(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvBalLot_MaxExpiredDateByInv objRQ_Rpt_InvBalLot_MaxExpiredDateByInv
            ////
            , out RT_Rpt_InvBalLot_MaxExpiredDateByInv objRT_Rpt_InvBalLot_MaxExpiredDateByInv
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Tid;
            objRT_Rpt_InvBalLot_MaxExpiredDateByInv = new RT_Rpt_InvBalLot_MaxExpiredDateByInv();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvBalLot_MaxExpiredDateByInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvBalLot_MaxExpiredDateByInv;
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
                List<Rpt_InvBalLot_MaxExpiredDateByInv> Lst_Rpt_InvBalLot_MaxExpiredDateByInv = new List<Rpt_InvBalLot_MaxExpiredDateByInv>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product = new List<Mst_Product>();
                    {
                        DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product, "Mst_Product");
                        dsData.Tables.Add(dt_Mst_Product);
                    }
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }
                    ////

                }
                #endregion

                #region // Rpt_InvBalLot_MaxExpiredDateByInv:
                mdsResult = Rpt_InvBalLot_MaxExpiredDateByInv(
                    objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Tid // strTid
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwPassword // strGwPassword
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.WAUserCode // strUserCode
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.WAUserPassword // strUserPassword
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.AccessToken // AccessToken
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.NetworkID // NetworkID
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.OrgID // OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvBalLot_MaxExpiredDateByInv = mdsResult.Tables["Rpt_InvBalLot_MaxExpiredDateByInv"].Copy();
                    Lst_Rpt_InvBalLot_MaxExpiredDateByInv = TUtils.DataTableCmUtils.ToListof<Rpt_InvBalLot_MaxExpiredDateByInv>(dt_Rpt_InvBalLot_MaxExpiredDateByInv);
                    objRT_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Rpt_InvBalLot_MaxExpiredDateByInv = Lst_Rpt_InvBalLot_MaxExpiredDateByInv;
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
        private void Rpt_Summary_In_Out_PivotX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strOrgID
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            , string strInventoryAction
            ////
            , DataSet dsData
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvInventoryBalanceMonthX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strOrgID", strOrgID
                , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "strApprDTimeUTCTo", strApprDTimeUTCTo
                , "strInventoryAction", strInventoryAction
                });
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Check:
            //// Refine:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            strInventoryAction = TUtils.CUtils.StdParam(strInventoryAction);
            strApprDTimeUTCFrom = TUtils.CUtils.StdDTimeBeginDay(strApprDTimeUTCFrom);
            strApprDTimeUTCTo = TUtils.CUtils.StdDTimeEndDay(strApprDTimeUTCTo);

            /////
            #endregion

            #region //// Refine and Check Input Mst_Product:
            ////
            DataTable dtInput_Mst_Product = null;
            {
                ////
                string strTableCheck = "Mst_Product";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Product = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Product // dtData
                                                   //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeUser" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Product.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
             }
            #endregion

            #region //// SaveTemp Mst_Product:
            if(dtInput_Mst_Product.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Product" // strTableName
                    , new object[] {
                            "ProductCodeUser", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Product // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from Mst_Product t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
            ////
            DataTable dtInput_Mst_ProductGroup = null;
            string strProductGrpCode = "";
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:

            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            else
            {
            }
            #endregion

            #region //// Refine and Check Input Mst_Customer:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_CustomerNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Customer = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Customer // dtData
                                         //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Customer:
            if (dtInput_Mst_Customer.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Customer" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType,
                            "CustomerCodeSys", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Customer // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Customer:
                        select distinct
	                        t.CustomerCodeSys
	                        , t.OrgID
                        into #input_Mst_Customer
                        from Mst_Customer t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ----- #input_Mst_Product_Filter:
                        select distinct
	                        t.ProductCode
	                        , t.OrgID
                        into #input_Mst_Product_Filter
                        from Mst_Product t --//[mylock]
	                        inner join #input_Mst_Product f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeUser = f.ProductCodeUser
                        where(1=1)
                        ;

                        

                        ---- Báo cáo tổng hợp nhập :
                        select 
	                        t.DocNo
	                        , t.OrgID
	                        , t.ApprDTimeUTC
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvCodeIn
			                        when t.InventoryAction = 'CUSRETURN' then invficr.InvCodeIn
			                        when t.InventoryAction = 'OUT' then invfo.InvCodeOut
		                        end 
	                        ) InvCode
	                        , t.CustomerCode
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then t.InventoryAction
			                        when t.InventoryAction = 'CUSRETURN' then'IN'
			                        when t.InventoryAction = 'OUT' then t.InventoryAction
		                        end 
	                        )  InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then 'CUSTOMERRETURN'
			                        when t.InventoryAction = 'OUT' then invfo.InvOutType
		                        end 
	                        ) Inv_In_Out_Type
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
		                        end 
	                        ) Inv_In_Out_TypeDesc
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then N'CUSRETURN'
		                        end 
	                        ) InvInType
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
		                        end 
	                        ) InvInTypeName
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutType
									else null
		                        end 
	                        ) InvOutType
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
									else null
		                        end 
	                        ) InvOutTypeName
	                        , t.ProductCodeBase ProductCode 
	                        , t.QtyConvert Qty
							, t.UnitCodeBase UnitCode
                        into #tbl_Rpt_Summary_In_Out
                        from InvF_WarehouseCard t --//[mylock]
	                        inner join #input_Mst_Product_Filter f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeBase = f.ProductCode
	                        inner join Mst_Product mp --//[mylock]
		                        on f.OrgID = mp.OrgID
			                        and f.ProductCode = mp.ProductCode	
	                        --left join #input_Mst_ProductGroup k --//[mylock]
		                    --    on mp.OrgID = k.OrgID
			                --        and mp.ProductGrpCode = k.ProductGrpCode
                            --------------------------------
                            zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                            ---------------------------------
	                        left join InvF_InventoryIn invfi --//[mylock]
		                        on t.DocNo = invfi.IF_InvInNo
	                        left join InvF_InventoryOut invfo --//[mylock]
		                        on t.DocNo = invfo.IF_InvOutNo
	                        left join InvF_InventoryCusReturn invficr --//[mylock]
		                        on t.DocNo = invficr.IF_InvCusReturnNo
	                        left join Mst_InvInType miitp --//[mylock]
		                        on invfi.OrgID = miitp.OrgID
			                        and invfi.InvInType = miitp.InvInType
	                        left join Mst_InvOutType miotp --//[mylock]
		                        on invfo.OrgID = miotp.OrgID
			                        and invfo.InvOutType = miotp.InvOutType
	                        inner join #input_Mst_Customer t_mc --//[mylock]
		                        on t.OrgID = t_mc.OrgID
			                        and t.CustomerCode = t_mc.CustomerCodeSys	
                        where(1=1)
	                        and t.InventoryAction in ('IN', 'OUT', 'CUSRETURN')
	                        and t.ApprDTimeUTC >= '@strApprDTimeUTCFrom'
	                        and t.ApprDTimeUTC <= '@strApprDTimeUTCTo'
                        ;

                        --- Return:
                        select 
	                        t.DocNo
	                        , Left(t.ApprDTimeUTC, 10) ApprDateUTC
	                        , mi.InvCode
	                        , mi.InvName
	                        , mc.CustomerCodeSys
	                        , mc.CustomerCode
	                        , mc.CustomerName
							, mc.AreaCode
							, ma.AreaName
							, mc.ProvinceCode
							, mpv.ProvinceName
	                        , mp.ProductCode
	                        , mp.ProductCodeUser
	                        , mp.ProductName
	                        , t.InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then N'Loại nhập'
			                        when t.InventoryAction = 'OUT' then N'Loại xuất'
		                        end 
	                        )  InventoryActionDesc
	                        , t.Inv_In_Out_Type
	                        , t.Inv_In_Out_TypeDesc
	                        , mpg.ProductGrpCode
	                        , mpg.ProductGrpName
	                        , mpg.ProductGrpDesc
	                        , t.Qty
							, t.UnitCode
							, t.InvInType
							, t.InvInTypeName
							, t.InvOutType
							, t.InvOutTypeName
                        from #tbl_Rpt_Summary_In_Out t --//[mylock]
	                        left join Mst_Inventory mi --//[mylock]
		                        on t.OrgID = mi.OrgID
			                        and t.InvCode = mi.InvCode
	                        left join Mst_Customer mc --//[mylock]
		                        on t.OrgID = mc.OrgID
			                        and t.CustomerCode = mc.CustomerCodeSys
	                        left join Mst_Area ma --//[mylock]
		                        on mc.OrgID = ma.OrgID
			                        and mc.AreaCode = ma.AreaCode
	                        left join Mst_Province mpv --//[mylock]
		                        on mc.ProvinceCode = mpv.ProvinceCode
	                        left join Mst_Product mp --//[mylock]
		                        on t.OrgID =mp.OrgID
			                        and t.ProductCode = mp.ProductCode	
	                        left join Mst_ProductGroup mpg --//[mylock]
		                        on t.OrgID = mpg.OrgID
			                        and mp.ProductGrpCode = mpg.ProductGrpCode
	                        inner join #input_Mst_Inventory k --//[mylock]
		                        on t.OrgID = k.OrgID
			                        and t.InvCode = k.InvCode
                        where(1=1)
							and ('@strInventoryAction'= '' or t.InventoryAction = '@strInventoryAction')
                        ;

                        ---- Clear For Debug:
                        drop table #input_Mst_Inventory;
                        drop table #input_Mst_Product;
                        --drop table #input_Mst_ProductGroup;
                        drop table #tbl_Rpt_Summary_In_Out;	
					"
					, "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                    , "@strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "@strApprDTimeUTCTo", strApprDTimeUTCTo
                    , "@strInventoryAction", strInventoryAction
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Summary_In_Out_Pivot";
            #endregion
        }
        public DataSet Rpt_Summary_In_Out_Pivot(
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
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            , string strInventoryAction
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Summary_In_Out_Pivot";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Summary_In_Out_Pivot;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "strApprDTimeUTCTo", strApprDTimeUTCTo
                    , "strInventoryAction", strInventoryAction
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

                //// Tạm thời bỏ để nhúng với inos
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

                #region // Convert Input:
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Rpt_Inv_InventoryBalanceSerialForSearchX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Summary_In_Out_PivotX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID_RQ // strOrgID_RQ
                        , strApprDTimeUTCFrom // strApprDTimeUTCFrom
                        , strApprDTimeUTCTo // strApprDTimeUTCTo
                        , strInventoryAction // strInventoryAction
                        , dsData // dsData
                                 ////
                        , out dsGetData // dsGetData
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


        public DataSet WAS_Rpt_Summary_In_Out_Pivot(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Summary_In_Out_Pivot objRQ_Rpt_Summary_In_Out_Pivot
            ////
            , out RT_Rpt_Summary_In_Out_Pivot objRT_Rpt_Summary_In_Out_Pivot
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Summary_In_Out_Pivot.Tid;
            objRT_Rpt_Summary_In_Out_Pivot = new RT_Rpt_Summary_In_Out_Pivot();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Summary_In_Out_Pivot";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Summary_In_Out_Pivot;
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
                List<Rpt_Summary_In_Out_Pivot> Lst_Rpt_Summary_In_Out_Pivot = new List<Rpt_Summary_In_Out_Pivot>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Inventory == null)
                        objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Product == null)
                        objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Product = new List<Mst_Product>();
                    {
                        DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Product, "Mst_Product");
                        dsData.Tables.Add(dt_Mst_Product);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Customer == null)
                        objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Customer = new List<Mst_Customer>();
                    {
                        DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Rpt_Summary_In_Out_Pivot.Lst_Mst_Customer, "Mst_Customer");
                        dsData.Tables.Add(dt_Mst_Customer);
                    }

                }
                #endregion

                #region // Rpt_Summary_In_Out_Pivot:
                mdsResult = Rpt_Summary_In_Out_Pivot(
                    objRQ_Rpt_Summary_In_Out_Pivot.Tid // strTid
                    , objRQ_Rpt_Summary_In_Out_Pivot.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out_Pivot.GwPassword // strGwPassword
                    , objRQ_Rpt_Summary_In_Out_Pivot.WAUserCode // strUserCode
                    , objRQ_Rpt_Summary_In_Out_Pivot.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Summary_In_Out_Pivot.AccessToken // AccessToken
                    , objRQ_Rpt_Summary_In_Out_Pivot.NetworkID // NetworkID
                    , objRQ_Rpt_Summary_In_Out_Pivot.OrgID // OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Summary_In_Out_Pivot.ApprDTimeUTCFrom // ApprDTimeUTCFrom
                    , objRQ_Rpt_Summary_In_Out_Pivot.ApprDTimeUTCTo // ApprDTimeUTCTo
                    , objRQ_Rpt_Summary_In_Out_Pivot.InventoryAction // InventoryAction
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Summary_In_Out_Pivot = mdsResult.Tables["Rpt_Summary_In_Out_Pivot"].Copy();
                    Lst_Rpt_Summary_In_Out_Pivot = TUtils.DataTableCmUtils.ToListof<Rpt_Summary_In_Out_Pivot>(dt_Rpt_Summary_In_Out_Pivot);
                    objRT_Rpt_Summary_In_Out_Pivot.Lst_Rpt_Summary_In_Out_Pivot = Lst_Rpt_Summary_In_Out_Pivot;
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

        public DataSet Rpt_Summary_In_Out_Sup_Pivot(
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
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            , string strInventoryAction
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Summary_In_Out_Sup_Pivot";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Summary_In_Out_Sup_Pivot;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "strApprDTimeUTCTo", strApprDTimeUTCTo
                    , "strInventoryAction", strInventoryAction
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

                ////
                //// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Convert Input:
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Rpt_Inv_InventoryBalanceSerialForSearchX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Summary_In_Out_PivotX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID_RQ // strOrgID_RQ
                        , strApprDTimeUTCFrom // strApprDTimeUTCFrom
                        , strApprDTimeUTCTo // strApprDTimeUTCTo
                        , strInventoryAction // strInventoryAction
                        , dsData // dsData
                                 ////
                        , out dsGetData // dsGetData
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
        public DataSet WAS_Rpt_Summary_In_Out_Sup_Pivot(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Summary_In_Out_Sup_Pivot objRQ_Rpt_Summary_In_Out_Sup_Pivot
            ////
            , out RT_Rpt_Summary_In_Out_Sup_Pivot objRT_Rpt_Summary_In_Out_Sup_Pivot
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Summary_In_Out_Sup_Pivot.Tid;
            objRT_Rpt_Summary_In_Out_Sup_Pivot = new RT_Rpt_Summary_In_Out_Sup_Pivot();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Summary_In_Out_Sup_Pivot";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Summary_In_Out_Sup_Pivot;
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
                List<Rpt_Summary_In_Out_Sup_Pivot> Lst_Rpt_Summary_In_Out_Sup_Pivot = new List<Rpt_Summary_In_Out_Sup_Pivot>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Inventory == null)
                        objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Product == null)
                        objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Product = new List<Mst_Product>();
                    {
                        DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Product, "Mst_Product");
                        dsData.Tables.Add(dt_Mst_Product);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Customer == null)
                        objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Customer = new List<Mst_Customer>();
                    {
                        DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Rpt_Summary_In_Out_Sup_Pivot.Lst_Mst_Customer, "Mst_Customer");
                        dsData.Tables.Add(dt_Mst_Customer);
                    }

                }
                #endregion

                #region // Rpt_Summary_In_Out_Sup_Pivot:
                mdsResult = Rpt_Summary_In_Out_Sup_Pivot(
                    objRQ_Rpt_Summary_In_Out_Sup_Pivot.Tid // strTid
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.GwPassword // strGwPassword
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.WAUserCode // strUserCode
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.AccessToken // AccessToken
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.NetworkID // NetworkID
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.OrgID // OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.ApprDTimeUTCFrom // ApprDTimeUTCFrom
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.ApprDTimeUTCTo // ApprDTimeUTCTo
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.InventoryAction // InventoryAction
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Summary_In_Out_Sup_Pivot = mdsResult.Tables["Rpt_Summary_In_Out_Pivot"].Copy();
                    Lst_Rpt_Summary_In_Out_Sup_Pivot = TUtils.DataTableCmUtils.ToListof<Rpt_Summary_In_Out_Sup_Pivot>(dt_Rpt_Summary_In_Out_Sup_Pivot);
                    objRT_Rpt_Summary_In_Out_Sup_Pivot.Lst_Rpt_Summary_In_Out_Sup_Pivot = Lst_Rpt_Summary_In_Out_Sup_Pivot;
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
        private void Rpt_Inv_InventoryBalanceSerialForSearchX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strObjectQRMix
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Inv_InventoryBalanceSerialForSearchX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strObjectQRMix", strObjectQRMix
                });
            #endregion

            #region // Check:
            //// Refine:
            strObjectQRMix = TUtils.CUtils.StdParam(strObjectQRMix);
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"                        
                    ---- #tbl_Inv_InventoryBalanceSerial_Filter_Draft:
                    select distinct 
	                    iibs.InvCode
	                    , iibs.PartCode
	                    , iibs.SerialNo
                    into #tbl_Inv_InventoryBalanceSerial_Filter_Draft
                    from Inv_InventoryBalanceSerial iibs --//[mylock]
                    where(1=1)
	                   and (iibs.SerialNo = '@strObjectQRMix' or iibs.BoxNo = '@strObjectQRMix' or iibs.CanNo = '@strObjectQRMix')
					   and iibs.FlagSales = '1'
                    ;

                    ---- #tbl_Inv_InventoryBalanceSerial_Filter:
                    select 
	                    iibs.*
                    into #tbl_Inv_InventoryBalanceSerial_Filter
                    from #tbl_Inv_InventoryBalanceSerial_Filter_Draft t --//[mylock]
	                    inner join Inv_InventoryBalanceSerial iibs --//[mylock]
		                    on t.InvCode = iibs.InvCode
			                    and t.PartCode = iibs.PartCode
			                    and t.SerialNo = iibs.SerialNo
                    where(1=1)
                    ;

                    ---- #tbl_Mst_Part_Filter_Draft:
                    select distinct
	                    mp.PartCode
                    into #tbl_Mst_Part_Filter_Draft
                    from Mst_Part mp --//[mylock]
	                    inner join #tbl_Inv_InventoryBalanceSerial_Filter t --//[mylock]
		                    on mp.PartCode = t.PartCode
                    where(1=1)
                    ;

                    ---- #tbl_Mst_Part_Filter:
                    select 
	                    mp.*
                    into #tbl_Mst_Part_Filter
                    from Mst_Part mp --//[mylock]
	                    inner join #tbl_Mst_Part_Filter_Draft t --//[mylock]
		                    on mp.PartCode = t.PartCode
                    where(1=1)
                    ;

                    ---- #tbl_Mst_Agent_Filter_Draft:
                    select distinct
	                    mg.AgentCode
                    into #tbl_Mst_Agent_Filter_Draft
                    from Mst_Agent mg --//[mylock]
	                    inner join #tbl_Inv_InventoryBalanceSerial_Filter t --//[mylock]
		                    on mg.AgentCode = t.AgentCode
                    where(1=1)
                    ;

                    ---- #tbl_Mst_Agent_Filter:
                    select 
	                    mg.*
                    into #tbl_Mst_Agent_Filter
                    from Mst_Agent mg --//[mylock]
	                    inner join #tbl_Mst_Agent_Filter_Draft t --//[mylock]
		                    on mg.AgentCode = t.AgentCode
                    where(1=1)
                    ;

                    ---- #tbl_Inv_InventoryBalanceSerial_Final:
                    select 
	                    f.PartCode
	                    , t.InvCode
	                    , t.SerialNo
	                    --, t.SerialNo_Actual
	                    , f.PartName
	                    , f.PartNameFS
	                    , f.FilePath
	                    , f.PartType
	                    , f.PMType
	                    , f.QtyEffMonth
	                    , f.PartOrigin
	                    , f.PartDesc
	                    , f.PartComponents
	                    , f.InstructionForUse
	                    , f.PartStorage
	                    , f.UrlMnfSequence
	                    , f.PartIntroduction
	                    , f.MnfStandard
	                    , f.ImagePath
	                    , f.PartStyle
	                    ,  null FGLotNo
	                    , t.SecretNo
	                    , t.WarrantyDateStart
	                    , null ProductionBatch
	                    , t.PackageDate
	                    , null PrintDTime
	                    , t.InvDTime
	                    , t.OutDTime
	                    , t.FlagSales
	                    , t.UserKCS
	                    , k.AgentCode
	                    , k.AgentName
						, iibs.NetworkID
                    into #tbl_Inv_InventoryBalanceSerial_Final
                    from #tbl_Inv_InventoryBalanceSerial_Filter t --//[mylock]
	                    inner join #tbl_Mst_Part_Filter f --//[mylock]
		                    on t.PartCode = f.PartCode
                        inner join Inv_InventoryBalanceSerial iibs --//[mylock]
		                    on t.SerialNo = iibs.SerialNo
	                    left join #tbl_Mst_Agent_Filter k --//[mylock]
		                    on t.AgentCode = k.AgentCode
                    where(1=1)
                    ;

                    ---- Return:
                    select 
	                    t.*
                    from #tbl_Inv_InventoryBalanceSerial_Final t --//[mylock]
                    where(1=1)
                    ;

					--- Lịch sử xuất kho:
					select 
						t.IF_InvOutHistNo IF_InvOutHistNo
						, '' AreaName -- Tên Vùng thị trường tạm thời trả ra null, nâng cấp sau.
						, mp.ProvinceCode 
						, mp.ProvinceName
						, iibs.InvDTime
						, iibs.OutDTime
						, iibs.PackageDate
						, iioh.AgentCode
						, (
							case 
								when iioh.AgentCode is not null then mnnt.NNTFullName
								else iioh.CustomerName
							end
						) CustomerName
						, iioh.PlateNo
						, iioh.MoocNo
						, iioh.DriverName
						, iioh.DriverPhoneNo
					from InvF_InventoryOutHistInstSerial t --//[mylock]
						inner join #tbl_Inv_InventoryBalanceSerial_Final f --//[mylock]
							on t.SerialNo = f.SerialNo
						inner join Inv_InventoryBalanceSerial iibs --//[mylock]
							on t.SerialNo = iibs.SerialNo
						inner join InvF_InventoryOutHist iioh --//[mylock]
							on t.IF_InvOutHistNo = iioh.IF_InvOutHistNo
						left join Mst_Agent mg --//[mylock]
							on iioh.AgentCode = mg.AgentCode
						left join Mst_NNT mnnt with(nolock)
							on iioh.AgentCode = mnnt.MST
						left join Mst_Province mp with(nolock)
							on mnnt.ProvinceCode = mp.ProvinceCode
					where(1=1)

                    ---- Clear For Debug:
                    drop table #tbl_Inv_InventoryBalanceSerial_Filter_Draft ;
                    drop table #tbl_Inv_InventoryBalanceSerial_Filter ;
                    drop table #tbl_Mst_Part_Filter_Draft ;
                    drop table #tbl_Mst_Part_Filter ;
                    drop table #tbl_Inv_InventoryBalanceSerial_Final ;
                    drop table #tbl_Mst_Agent_Filter ;
					drop table #tbl_Mst_Agent_Filter_Draft;			
					"
                    , "@strObjectQRMix", strObjectQRMix
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalanceSerialForSearch";
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvF_InventoryOutHistInstSerialForSearch";
            #endregion
        }

        public DataSet Rpt_Inv_InventoryBalanceSerialForSearch(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strObjectQRMix
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Inv_InventoryBalanceSerialForSearch";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalanceSerialForSearch;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strObjectQRMix", strObjectQRMix
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

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Rpt_Inv_InventoryBalanceSerialForSearchX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Inv_InventoryBalanceSerialForSearchX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , strObjectQRMix // strObjectQRMix
                        ////
                        , out dsGetData // dsGetData
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


        public DataSet WAS_Rpt_Inv_InventoryBalanceSerialForSearch(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalanceSerialForSearch objRQ_Rpt_Inv_InventoryBalanceSerialForSearch
            ////
            , out RT_Rpt_Inv_InventoryBalanceSerialForSearch objRT_Rpt_Inv_InventoryBalanceSerialForSearch
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid;
            objRT_Rpt_Inv_InventoryBalanceSerialForSearch = new RT_Rpt_Inv_InventoryBalanceSerialForSearch();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalanceSerialForSearch";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalanceSerialForSearch;
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
                List<Rpt_Inv_InventoryBalanceSerialForSearch> Lst_Rpt_Inv_InventoryBalanceSerialForSearch = new List<Rpt_Inv_InventoryBalanceSerialForSearch>();
                List<Rpt_InvF_InventoryOutHistInstSerialForSearch> Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = new List<Rpt_InvF_InventoryOutHistInstSerialForSearch>();
                #endregion

                #region // Rpt_Inv_InventoryBalanceSerialForSearch:
                mdsResult = Rpt_Inv_InventoryBalanceSerialForSearch(
                    objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.ObjectQRMix // ObjectQRMix
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalanceSerialForSearch = mdsResult.Tables["Rpt_Inv_InventoryBalanceSerialForSearch"].Copy();
                    Lst_Rpt_Inv_InventoryBalanceSerialForSearch = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalanceSerialForSearch>(dt_Rpt_Inv_InventoryBalanceSerialForSearch);
                    objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_Inv_InventoryBalanceSerialForSearch = Lst_Rpt_Inv_InventoryBalanceSerialForSearch;
                    /////
                    DataTable dt_Rpt_InvF_InventoryOutHistInstSerialForSearch = mdsResult.Tables["Rpt_InvF_InventoryOutHistInstSerialForSearch"].Copy();
                    Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = TUtils.DataTableCmUtils.ToListof<Rpt_InvF_InventoryOutHistInstSerialForSearch>(dt_Rpt_InvF_InventoryOutHistInstSerialForSearch);
                    objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch;
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
        private void Rpt_InvInventoryBalanceMonthX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strInvBalMonthFrom
            , string strInvBalMonthTo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvInventoryBalanceMonthX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strInvBalMonthFrom", strInvBalMonthFrom
                , "strInvBalMonthTo", strInvBalMonthTo
                });
            #endregion

            #region // Check:
            //// Refine:
            strInvBalMonthFrom = TUtils.CUtils.StdMonth(strInvBalMonthFrom);
            strInvBalMonthTo = TUtils.CUtils.StdMonth(strInvBalMonthTo);
            string strHRMonth_Next = TUtils.CUtils.StdMonth(Convert.ToDateTime(strInvBalMonthTo).AddMonths(1));
            string strHRMonthFrom_Prev = TUtils.CUtils.StdMonth(Convert.ToDateTime(strInvBalMonthFrom).AddMonths(-1));
            string strInvBalDTimeFrom = TUtils.CUtils.StdDTimeBeginDay(strInvBalMonthFrom);
            string strInvBalDTimeTo = TUtils.CUtils.StdDTimeEndDay(Convert.ToDateTime(strHRMonth_Next).AddDays(-1));
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_Inv_InventoryBalance_Filter:
                        select distinct
	                        t.InvCode
	                        , t.PartCode
                        into #tbl_Inv_InventoryBalance_Filter
                        from Inv_InventoryBalance t --//[mylock]
	                        --inner join Mst_Dealer md --//[mylock]
		                     --   on t.InvCode = md.InvCode
                            --inner join #tbl_Mst_Dealer_ViewAbility f --//[mylock]
                            --    on md.DLCode = f.DLCode
                        where(1=1)
                        ;

                        --select null tbl_Inv_InventoryBalance_Filter, t.* from #tbl_Inv_InventoryBalance_Filter t --//[mylock];

                        ---- #tbl_Inv_InventoryBalanceMonth_Filter:
                        select distinct
	                        t.InvBalMonth
                        into #tbl_Inv_InventoryBalanceMonth_Filter
                        from Inv_InventoryBalanceMonth t --//[mylock]
                        where(1=1)
	                        and t.InvBalMonth >= '@strInvBalMonthFrom'
	                        and t.InvBalMonth <= '@strInvBalMonthTo'
                        ;

                        --select null tbl_Inv_InventoryBalanceMonth_Filter, t.* from #tbl_Inv_InventoryBalanceMonth_Filter t --//[mylock];

                        ----- #tbl_Inv_InventoryBalanceMonthDtl_Filter:
                        select distinct
	                        t.InvBalMonth
	                        , t.InvCode
	                        , t.Partcode
                        into #tbl_Inv_InventoryBalanceMonthDtl_Filter
                        from Inv_InventoryBalanceMonthDtl t --//[mylock]
	                        inner join #tbl_Inv_InventoryBalanceMonth_Filter f --//[mylock]
		                        on t.InvBalMonth = f.InvBalMonth
                        where(1=1)
                        ;

                        --select null tbl_Inv_InventoryBalanceMonthDtl_Filter, t.* from #tbl_Inv_InventoryBalanceMonthDtl_Filter t --//[mylock];

                        ---- #tbl_Inv_InventoryBalanceMonthDtl_Begin:
                        select 
	                        '@strHRMonthFrom_Prev' InvBalMonth
	                        , f.InvCode
	                        , f.PartCode
	                        , f.QtyInvEnd QtyInvBegin
                        into #tbl_Inv_InventoryBalanceMonthDtl_Begin
                        from Inv_InventoryBalanceMonthDtl f --//[mylock]
                        where(1=1)
	                        and f.InvBalMonth = '@strHRMonthFrom_Prev'
                        ;

                        --select null tbl_Inv_InventoryBalanceMonthDtl_Begin, t.* from #tbl_Inv_InventoryBalanceMonthDtl_Begin t --//[mylock];

                        ---- #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin:
                        select 
	                        t.InvCode
	                        , t.PartCode
	                        , Sum(t.QtyInvBegin) TotalQtyInvBegin
                        into #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin
                        from #tbl_Inv_InventoryBalanceMonthDtl_Begin t --//[mylock]
                        where(1=1)
                        group by
	                        t.InvCode
	                        , t.PartCode
                        ;

                        --select null #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin, t.* from #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin t --//[mylock];

                        ---- #tbl_Inv_InventoryBalanceMonthDtl_End:
                        select distinct
	                        t.InvBalMonth
	                        , t.InvCode
	                        , t.PartCode
	                        , f.QtyInvEnd
                        into #tbl_Inv_InventoryBalanceMonthDtl_End
                        from #tbl_Inv_InventoryBalanceMonthDtl_Filter t --//[mylock]
	                        inner join Inv_InventoryBalanceMonthDtl f --//[mylock]
		                        on t.InvBalMonth = f.InvBalMonth
			                        and t.InvCode = f.InvCode
			                        and t.PartCode = f.PartCode
                        where(1=1)
	                        and t.InvBalMonth = '@strInvBalMonthTo'
                        ;

                        --select null tbl_Inv_InventoryBalanceMonthDtl_End, t.* from #tbl_Inv_InventoryBalanceMonthDtl_End t --//[mylock];


                        ---- #tbl_Inv_InventoryBalanceMonthDtl_TotalEnd:
                        select 
	                        t.InvCode
	                        , t.PartCode
	                        , Sum(t.QtyInvEnd) TotalQtyInvEnd
                        into #tbl_Inv_InventoryBalanceMonthDtl_TotalEnd
                        from #tbl_Inv_InventoryBalanceMonthDtl_End t --//[mylock]
                        where(1=1)
                        group by
	                        t.InvCode
	                        , t.PartCode
                        ;

                        --select null tbl_Inv_InventoryBalanceMonthDtl_TotalEnd, t.* from #tbl_Inv_InventoryBalanceMonthDtl_TotalEnd t --//[mylock];

                        ---- #tbl_Inv_InventoryTransaction_PSIN:
                        select 
	                        t.InvCode
	                        , t.PartCode
	                        , Sum(t.QtyPlanChTotal) TotalQtyIn
                        into #tbl_Inv_InventoryTransaction_PSIN
                        from Inv_InventoryTransaction t --//[mylock]
                        where(1=1)
	                        and t.CreateDTimeUTC >= '@strInvBalDTimeFrom'
	                        and t.CreateDTimeUTC <= '@strInvBalDTimeTo'
	                        and t.QtyPlanChTotal > 0
                        group by
	                        t.InvCode
	                        , t.PartCode
                        ;

                        --select null tbl_Inv_InventoryTransaction_PSIN, t.* from #tbl_Inv_InventoryTransaction_PSIN t --//[mylock];

                        ---- #tbl_InvF_InventoryOutFGDtl_PSOUT:
                        select 
	                        t.InvCode
	                        , t.PartCode
	                        , Sum(t.QtyPlanChTotal) TotalQtyOut
                        into #tbl_Inv_InventoryTransaction_PSOUT
                        from Inv_InventoryTransaction t --//[mylock]
                        where(1=1)
	                        and t.CreateDTimeUTC >= '@strInvBalDTimeFrom'
	                        and t.CreateDTimeUTC <= '@strInvBalDTimeTo'
	                        and t.QtyPlanChTotal < 0
                        group by
	                        t.InvCode
	                        , t.PartCode
                        ;

                        --select null tbl_Inv_InventoryTransaction_PSOUT, t.* from #tbl_Inv_InventoryTransaction_PSOUT t --//[mylock];


						---- #tbl_Mst_Part_Union:
						select 
							t.InvCode
							, t.PartCode
						into #tbl_Mst_Part_Union
						from #tbl_Inv_InventoryBalance_Filter t --//[mylock]
						union 
						select 
							t.InvCode
							, t.PartCode
						from #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin t --//[mylock]
						union 
						select 
							t.InvCode
							, t.PartCode
						from #tbl_Inv_InventoryBalanceMonthDtl_TotalEnd t --//[mylock]
						union 
						select 
							t.InvCode
							, t.PartCode
						from #tbl_Inv_InventoryTransaction_PSIn t --//[mylock]
						union 
						select 
							t.InvCode
							, t.PartCode
						from #tbl_Inv_InventoryTransaction_PSOUT t --//[mylock]
						;

                        ---- #tbl_Summary:
                        select 
	                        h.InvCode
	                        , h.PartCode
	                        , f.TotalQtyInvBegin
	                        , k.TotalQtyInvEnd
	                        , z_In.TotalQtyIn
	                        , z_Out.TotalQtyOut
                        into #tbl_Summary
                        from #tbl_Mst_Part_Union h --//[mylock]
							left join #tbl_Inv_InventoryBalance_Filter t --//[mylock]
								on h.InvCode = t.InvCode
									and h.PartCode = t.PartCode
	                        left join #tbl_Inv_InventoryBalanceMonthDtl_TotalBegin f --//[mylock]
		                        on h.InvCode = f.InvCode
			                        and h.PartCode = f.PartCode
	                        left join #tbl_Inv_InventoryBalanceMonthDtl_TotalEnd k --//[mylock]
		                        on h.InvCode = k.InvCode
			                        and h.PartCode = k.PartCode
	                        left join #tbl_Inv_InventoryTransaction_PSIn z_In --//[mylock]
		                        on h.InvCode = z_In.InvCode
			                        and h.PartCode = z_In.PartCode
	                        left join #tbl_Inv_InventoryTransaction_PSOUT z_Out --//[mylock]
		                        on h.InvCode = z_Out.InvCode
			                        and h.PartCode = z_Out.PartCode
	                        inner join Inv_InventoryBalance iib --//[mylock]
		                        on h.InvCode = iib.InvCode
			                        and h.PartCode = iib.PartCode
                        where(1=1)
                        ;

                        --select t.* from #tbl_Summary t

                        --- Return:
                        select 
	                        t.InvCode
	                        , t.PartCode
	                        , mp.PartName
	                        , mpt.PartType
	                        , mpt.PartTypeName
	                        , IsNull(t.TotalQtyInvBegin, 0.0) TotalQtyInvBegin
	                        , IsNull(t.TotalQtyIn, 0.0) TotalQtyIn
	                        , ABS(IsNull(t.TotalQtyOut, 0.0)) TotalQtyOut
	                        --, IsNull(t.TotalQtyInvEnd, iib.QtyPlanTotal) TotalQtyInvEnd ?? 
	                        , (IsNull(t.TotalQtyInvBegin, 0.0) + IsNull(t.TotalQtyIn, 0.0) - ABS(IsNull(t.TotalQtyOut, 0.0)))TotalQtyInvEnd
                        from #tbl_Summary t --//[mylock]
	                        left join Mst_Part mp --//[mylock]
		                        on t.PartCode = mp.PartCode
	                        left join Mst_Inventory mi --//[mylock]
		                        on t.InvCode = mi.InvCode
	                        left join Mst_PartType mpt --//[mylock]
		                        on mp.PartType = mpt.PartType
	                        left join Inv_InventoryBalance iib --//[mylock]
		                        on t.InvCode = iib.InvCode
			                        and t.PartCode = iib.PartCode
	                        --left join Mst_Dealer md 
		                       -- on iib.InvCode = md.InvCode
                            --inner join #tbl_Mst_Dealer_ViewAbility md_t --//[mylock]
                            --    on md.DLCode = md_t.DLCode
                        where(1=1)
                            and IsNull(t.TotalQtyInvBegin, 0.0) != 0.0
                            or IsNull(t.TotalQtyIn, 0.0) != 0.0
                            or IsNull(t.TotalQtyOut, 0.0) != 0.0
                        ; 

                        ---- Clear For Debug:
                        --drop table #tbl_Inv_InventoryBalance_Filter;			
					"
                    , "@strInvBalMonthFrom", strInvBalMonthFrom
                    , "@strInvBalMonthTo", strInvBalMonthTo
                    , "@strInvBalDTimeFrom", strInvBalDTimeFrom
                    , "@strInvBalDTimeTo", strInvBalDTimeTo
                    , "@strHRMonthFrom_Prev", strHRMonthFrom_Prev
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvInventoryBalanceMonth";
            #endregion
        }
        public DataSet Rpt_InvInventoryBalanceMonth(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strInvBalMonthFrom
            , string strInvBalMonthTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvFInventoryInFGSum";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvFInventoryInFGSum;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strInvBalMonthFrom", strInvBalMonthFrom
                    , "strInvBalMonthTo", strInvBalMonthTo
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

                #region // Rpt_InvInventoryBalanceMonthX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvInventoryBalanceMonthX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , strInvBalMonthFrom // strInvBalMonthFrom
                        , strInvBalMonthTo // strInvBalMonthTo
                        ////
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_InvInventoryBalanceMonth(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvInventoryBalanceMonth objRQ_Rpt_InvInventoryBalanceMonth
            ////
            , out RT_Rpt_InvInventoryBalanceMonth objRT_Rpt_InvInventoryBalanceMonth
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvInventoryBalanceMonth.Tid;
            objRT_Rpt_InvInventoryBalanceMonth = new RT_Rpt_InvInventoryBalanceMonth();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvInventoryBalanceMonth";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvInventoryBalanceMonth;
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
                List<Rpt_InvInventoryBalanceMonth> Lst_Rpt_InvInventoryBalanceMonth = new List<Rpt_InvInventoryBalanceMonth>();
                #endregion

                #region // Rpt_InvInventoryBalanceMonth:
                mdsResult = Rpt_InvInventoryBalanceMonth(
                    objRQ_Rpt_InvInventoryBalanceMonth.Tid // strTid
                    , objRQ_Rpt_InvInventoryBalanceMonth.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvInventoryBalanceMonth.GwPassword // strGwPassword
                    , objRQ_Rpt_InvInventoryBalanceMonth.WAUserCode // strUserCode
                    , objRQ_Rpt_InvInventoryBalanceMonth.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvInventoryBalanceMonth.InvBalMonthFrom // InvBalMonthFrom
                    , objRQ_Rpt_InvInventoryBalanceMonth.InvBalMonthTo // InvBalMonthTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvInventoryBalanceMonth = mdsResult.Tables["Rpt_InvInventoryBalanceMonth"].Copy();
                    Lst_Rpt_InvInventoryBalanceMonth = TUtils.DataTableCmUtils.ToListof<Rpt_InvInventoryBalanceMonth>(dt_Rpt_InvInventoryBalanceMonth);
                    objRT_Rpt_InvInventoryBalanceMonth.Lst_Rpt_InvInventoryBalanceMonth = Lst_Rpt_InvInventoryBalanceMonth;
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
        private void Rpt_InvFInventoryInFGSumX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strPartCode
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvFInventoryInFGSumX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strPartCode", strPartCode
                , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "strApprDTimeUTCTo", strApprDTimeUTCTo
                });
            #endregion

            #region // Check:
            //// Refine:
            strPartCode = TUtils.CUtils.StdParam(strPartCode);
            strApprDTimeUTCFrom = TUtils.CUtils.StdDTimeBeginDay(strApprDTimeUTCFrom);
            strApprDTimeUTCTo = TUtils.CUtils.StdDTimeEndDay(strApprDTimeUTCTo);
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"				
				
					-- #tbl_InvF_InventoryInFGDtl_Filter:
                    select distinct
	                    ifiif.IF_InvInFGNo
	                    , ifiifdt.PartCode
                    into #tbl_InvF_InventoryInFGDtl_Filter
                    from InvF_InventoryInFG ifiif --//[mylock]
                        --inner join #tbl_Mst_Dealer_ViewAbility t --//[mylock]
                        --    on ifiif.MST = t.MST
                        inner join InvF_InventoryInFGDtl ifiifdt --//[mylock]
                            on ifiif.IF_InvInFGNo = ifiifdt.IF_InvInFGNo
                    where(1=1)
	                    and ifiif.ApprDTimeUTC >= '@strApprDTimeUTCFrom'
	                    and ifiif.ApprDTimeUTC <= '@strApprDTimeUTCTo'
	                    and (N'@strPartCode' = '' or ifiifdt.PartCode = '@strPartCode')
	                    and ifiif.IF_InvInFGStatus in ('APPROVE')
                    ;

                    -- #tbl_InvF_InventoryInFGDtl_TotalQtyIn:
                    select 
	                    ifiif.InvCode
	                    , ifiif.MST
	                    , ifiifdt.PartCode
	                    , mp.PartName
	                    , Sum(ifiifdt.Qty) TotalQtyIn
                    into #tbl_InvF_InventoryInFGDtl_TotalQtyIn
                    from #tbl_InvF_InventoryInFGDtl_Filter t --//[mylock]
	                    inner join InvF_InventoryInFGDtl ifiifdt --//[mylock]
		                    on t.IF_InvInFGNo = ifiifdt.IF_InvInFGNo
		                        and t.PartCode = ifiifdt.PartCode
	                    inner join InvF_InventoryInFG ifiif --//[mylock]
		                    on t.IF_InvInFGNo = ifiif.IF_InvInFGNo
	                    inner join Mst_Part mp --//[mylock]
		                    on t.PartCode = mp.PartCode
                    where(1=1)
                    group by 
	                    ifiif.InvCode
	                    , ifiif.MST
	                    , ifiifdt.PartCode
	                    , mp.PartName
                    ;

                    ---- Return:
                    select
	                    t.* 
                    from #tbl_InvF_InventoryInFGDtl_TotalQtyIn t --//[mylock]
                    where(1=1)
                    ;

                    ---- Clear For Debug:
                    drop table #tbl_InvF_InventoryInFGDtl_Filter;
                    drop table #tbl_InvF_InventoryInFGDtl_TotalQtyIn ;				
					"
                , "@strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "@strApprDTimeUTCTo", strApprDTimeUTCTo
                , "@strPartCode", strPartCode
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvFInventoryInFGSum";
            #endregion
        }

        public DataSet Rpt_InvFInventoryInFGSum(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strPartCode
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvFInventoryInFGSum";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvFInventoryInFGSum;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strPartCode", strPartCode
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "strApprDTimeUTCTo", strApprDTimeUTCTo
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

                #region // Rpt_InvoiceInvoice_ResultUsedX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvFInventoryInFGSumX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strPartCode
                        , strApprDTimeUTCFrom
                        , strApprDTimeUTCTo
                        ////
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_InvFInventoryInFGSum(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvFInventoryInFGSum objRQ_Rpt_InvFInventoryInFGSum
            ////
            , out RT_Rpt_InvFInventoryInFGSum objRT_Rpt_InvFInventoryInFGSum
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvFInventoryInFGSum.Tid;
            objRT_Rpt_InvFInventoryInFGSum = new RT_Rpt_InvFInventoryInFGSum();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvFInventoryInFGSum";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvFInventoryInFGSum;
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
                List<Rpt_InvFInventoryInFGSum> Lst_Rpt_InvFInventoryInFGSum = new List<Rpt_InvFInventoryInFGSum>();
                #endregion

                #region // Rpt_InvFInventoryInFGSum:
                mdsResult = Rpt_InvFInventoryInFGSum(
                    objRQ_Rpt_InvFInventoryInFGSum.Tid // strTid
                    , objRQ_Rpt_InvFInventoryInFGSum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvFInventoryInFGSum.GwPassword // strGwPassword
                    , objRQ_Rpt_InvFInventoryInFGSum.WAUserCode // strUserCode
                    , objRQ_Rpt_InvFInventoryInFGSum.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvFInventoryInFGSum.PartCode // objPartCode
                    , objRQ_Rpt_InvFInventoryInFGSum.ApprDTimeUTCFrom // CreateDTimeUTCFrom
                    , objRQ_Rpt_InvFInventoryInFGSum.ApprDTimeUTCTo // CreateDTimeUTCTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvFInventoryInFGSum = mdsResult.Tables["Rpt_InvFInventoryInFGSum"].Copy();
                    Lst_Rpt_InvFInventoryInFGSum = TUtils.DataTableCmUtils.ToListof<Rpt_InvFInventoryInFGSum>(dt_Rpt_InvFInventoryInFGSum);
                    objRT_Rpt_InvFInventoryInFGSum.Lst_Rpt_InvFInventoryInFGSum = Lst_Rpt_InvFInventoryInFGSum;
                    /////
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

        private void Rpt_InvFInventoryOutFGSumX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strAgentCode
            , string strPartCode
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvFInventoryOutFGSumX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strAgentCode", strAgentCode
                , "strPartCode", strPartCode
                , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "strApprDTimeUTCTo", strApprDTimeUTCTo
                });
            #endregion

            #region // Check:
            //// Refine:
            strAgentCode = TUtils.CUtils.StdParam(strAgentCode);
            strPartCode = TUtils.CUtils.StdParam(strPartCode);
            strApprDTimeUTCFrom = TUtils.CUtils.StdDTimeBeginDay(strApprDTimeUTCFrom);
            strApprDTimeUTCTo = TUtils.CUtils.StdDTimeEndDay(strApprDTimeUTCTo);
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"	
					-- #tbl_InvF_InventoryOutFG_Filter:
                    select distinct
	                    ifiof.IF_InvOutFGNo
	                    , ifiof.InvCode
	                    , ifiof.MST
                    into #tbl_InvF_InventoryOutFG_Filter
                    from InvF_InventoryOutFG ifiof --//[mylock]
                        --inner join #tbl_Mst_Dealer_ViewAbility t --//[mylock]
                        --    on ifiof.MST = t.MST
                    where(1=1)
	                    and ifiof.ApprDTimeUTC >= '@strApprDTimeUTCFrom'
	                    and ifiof.ApprDTimeUTC <= '@strApprDTimeUTCTo'
	                   -- and ifiof.MST= '@strMST'
                    ;

					-- #tbl_InvF_InventoryOutFGDtl_Filter:
                    select distinct
	                    ifiofdt.IF_InvOutFGNo
	                    , ifiofdt.PartCode
	                    , t.InvCode
	                    , t.MST
                    into #tbl_InvF_InventoryOutFGDtl_Filter
                    from InvF_InventoryOutFGDtl ifiofdt --//[mylock]
	                    inner join #tbl_InvF_InventoryOutFG_Filter t --//[mylock]
		                    on ifiofdt.IF_InvOutFGNo = t.IF_InvOutFGNo
						left join Inv_InventoryBalanceSerial  k --//[mylock]
							on ifiofdt.IF_InvOutFGNo = k.IF_InvOutFGNo
						left join InvF_InventoryOutHist invfiohs --//[mylock]
							on k.IF_InvOutFGNo = invfiohs.IF_InvOutHistNo
                    where(1=1)
	                    and (N'@strAgentCode' = '' or invfiohs.AgentCode = '@strAgentCode')
	                    and (N'@strPartCode' = '' or ifiofdt.PartCode = '@strPartCode')
                    ;

                    -- #tbl_InvF_InventoryOutFGDtl_TotalQtyOut_MaVach_KhongMaVach:
                    select 
	                    ifiof_t.InvCode
	                    , ifiof_t.MST
	                    , t.PartCode
	                    --, mp.PartName
	                    , invfiohs.AgentCode
	                    --, mg.AgentName
						, ifiofg.FormOutType
	                    , count(iibs.SerialNo) TotalQtyOut
                    into #tbl_InvF_InventoryOutFGDtl_TotalQtyOut_MaVach_KhongMaVach
                    from #tbl_InvF_InventoryOutFGDtl_Filter t --//[mylock]
                        inner join #tbl_InvF_InventoryOutFG_Filter ifiof_t --//[mylock]
                            on t.IF_InvOutFGNo = ifiof_t.IF_InvOutFGNo
	                    --inner join Mst_Part mp --//[mylock]
		                --    on t.PartCode = mp.PartCode
                        inner join InvF_InventoryOutFG ifiofg --//[mylock]
                            on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
                        inner join InvF_InventoryOutFGDtl ifiofdt --//[mylock]
                            on t.IF_InvOutFGNo = ifiofdt.IF_InvOutFGNo
								and t.PartCode = ifiofdt.PartCode
						left join Inv_InventoryBalanceSerial iibs --//[mylock]
							on t.IF_InvOutFGNo = iibs.IF_InvOutFGNo 
								and t.PartCode = iibs.PartCode
	                    --inner join Mst_Agent mg --//[mylock]
		                --    on iibs_t.AgentCode = mg.AgentCode
						left join InvF_InventoryOutHist invfiohs --//[mylock]
							on iibs.IF_InvOutFGNo = invfiohs.IF_InvOutHistNo
                    where(1=1)
						and ifiofg.FormOutType in ('MAVACH')
                    group by 
	                    ifiof_t.InvCode
	                    , ifiof_t.MST
	                    , t.PartCode
	                    --, mp.PartName
	                    , invfiohs.AgentCode
						, ifiofg.FormOutType
	                    --, mg.AgentName
					union 
                    select 
	                    ifiof_t.InvCode
	                    , ifiof_t.MST
	                    , t.PartCode
	                    --, mp.PartName
	                    ,  null AgentCode
						, ifiofg.FormOutType
	                    --, mg.AgentName
	                    , sum(ifiofdt.Qty) TotalQtyOut
                    from #tbl_InvF_InventoryOutFGDtl_Filter t --//[mylock]
                        inner join #tbl_InvF_InventoryOutFG_Filter ifiof_t --//[mylock]
                            on t.IF_InvOutFGNo = ifiof_t.IF_InvOutFGNo
	                    --inner join Mst_Part mp --//[mylock]
		                --    on t.PartCode = mp.PartCode
                        inner join InvF_InventoryOutFG ifiofg --//[mylock]
                            on t.IF_InvOutFGNo = ifiofg.IF_InvOutFGNo
                        inner join InvF_InventoryOutFGDtl ifiofdt --//[mylock]
                            on t.IF_InvOutFGNo = ifiofdt.IF_InvOutFGNo
								and t.PartCode = ifiofdt.PartCode
	                    --inner join Mst_Agent mg --//[mylock]
		                --    on iibs_t.AgentCode = mg.AgentCode
                    where(1=1)
						and ifiofg.FormOutType in ('KHONGMAVACH')
                    group by 
	                    ifiof_t.InvCode
	                    , ifiof_t.MST
	                    , t.PartCode
						, ifiofg.FormOutType
	                    --, mp.PartName
	                    --, mg.AgentName
                    ;

					---- #tbl_InvF_InventoryOutFGDtl_TotalQtyOut:
					select 
	                    t.InvCode
	                    , t.MST
	                    , t.PartCode
	                    , t.AgentCode
						, sum(t.TotalQtyOut) TotalQtyOut
					into #tbl_InvF_InventoryOutFGDtl_TotalQtyOut
					from #tbl_InvF_InventoryOutFGDtl_TotalQtyOut_MaVach_KhongMaVach t --//[mylock]
					where(1=1)
					group by 
	                    t.InvCode
	                    , t.MST
	                    , t.PartCode
	                    , t.AgentCode
					;

                    ---- Return:
                    select
	                    t.* 
	                    , mp.PartName
	                    , mg.NNTFullName AgentName
                    from #tbl_InvF_InventoryOutFGDtl_TotalQtyOut t --//[mylock]
	                    inner join Mst_Part mp --//[mylock]
		                    on t.PartCode = mp.PartCode  
	                    left join Mst_NNT mg --//[mylock]
		                    on t.AgentCode = mg.MST           

                    where(1=1)
                    ;

                    ---- Clear For Debug:
                    drop table #tbl_InvF_InventoryOutFGDtl_Filter;
                    drop table #tbl_InvF_InventoryOutFGDtl_TotalQtyOut;			
					"
                , "@strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "@strApprDTimeUTCTo", strApprDTimeUTCTo
                , "@strAgentCode", strAgentCode
                , "@strPartCode", strPartCode
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvFInventoryOutFGSum";
            #endregion
        }

        public DataSet Rpt_InvFInventoryOutFGSum(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strAgentCode
            , string strPartCode
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvFInventoryOutFGSum";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvFInventoryOutFGSum;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strAgentCode", strAgentCode
                    , "strPartCode", strPartCode
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "strApprDTimeUTCTo", strApprDTimeUTCTo
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

                #region // Rpt_InvFInventoryOutFGSumX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvFInventoryOutFGSumX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strAgentCode
                        , strPartCode
                        , strApprDTimeUTCFrom
                        , strApprDTimeUTCTo
                        ////
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_InvFInventoryOutFGSum(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvFInventoryOutFGSum objRQ_Rpt_InvFInventoryOutFGSum
            ////
            , out RT_Rpt_InvFInventoryOutFGSum objRT_Rpt_InvFInventoryOutFGSum
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvFInventoryOutFGSum.Tid;
            objRT_Rpt_InvFInventoryOutFGSum = new RT_Rpt_InvFInventoryOutFGSum();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvFInventoryOutFGSum";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvFInventoryOutFGSum;
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
                List<Rpt_InvFInventoryOutFGSum> Lst_Rpt_InvFInventoryOutFGSum = new List<Rpt_InvFInventoryOutFGSum>();
                #endregion

                #region // Rpt_InvFInventoryOutFGSum:
                mdsResult = Rpt_InvFInventoryOutFGSum(
                    objRQ_Rpt_InvFInventoryOutFGSum.Tid // strTid
                    , objRQ_Rpt_InvFInventoryOutFGSum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvFInventoryOutFGSum.GwPassword // strGwPassword
                    , objRQ_Rpt_InvFInventoryOutFGSum.WAUserCode // strUserCode
                    , objRQ_Rpt_InvFInventoryOutFGSum.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvFInventoryOutFGSum.AgentCode // objAgentCode
                    , objRQ_Rpt_InvFInventoryOutFGSum.PartCode // objPartCode
                    , objRQ_Rpt_InvFInventoryOutFGSum.ApprDTimeUTCFrom // CreateDTimeUTCFrom
                    , objRQ_Rpt_InvFInventoryOutFGSum.ApprDTimeUTCTo // CreateDTimeUTCTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvFInventoryOutFGSum = mdsResult.Tables["Rpt_InvFInventoryOutFGSum"].Copy();
                    Lst_Rpt_InvFInventoryOutFGSum = TUtils.DataTableCmUtils.ToListof<Rpt_InvFInventoryOutFGSum>(dt_Rpt_InvFInventoryOutFGSum);
                    objRT_Rpt_InvFInventoryOutFGSum.Lst_Rpt_InvFInventoryOutFGSum = Lst_Rpt_InvFInventoryOutFGSum;
                    /////
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
        private void Rpt_InvoiceInvoice_ResultUsedX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            , string strInvoiceType
            , string strSign
            , string strFormNo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvoiceInvoice_ResultUsedX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeFrom", strReportDTimeFrom
                , "strReportDTimeTo", strReportDTimeTo
                , "strInvoiceType", strInvoiceType
                , "strSign", strSign
                , "strFormNo", strFormNo
                });
            #endregion

            #region // Check:
            //// Refine:
            strReportDTimeFrom = TUtils.CUtils.StdDTimeBeginDay(strReportDTimeFrom);
            strReportDTimeTo = TUtils.CUtils.StdDTimeEndDay(strReportDTimeTo);
            string strReportDateFrom = TUtils.CUtils.StdDate(strReportDTimeFrom);
            string strReportDateTo = TUtils.CUtils.StdDate(strReportDTimeTo);
            strInvoiceType = TUtils.CUtils.StdParam(strInvoiceType);
            strSign = string.Format("{0}", strSign).Trim();
            strFormNo = string.Format("{0}", strFormNo).Trim();
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"					
					--Báo cáo tình hình sử dụng hóa đơn của đơn vị (BC26/AC)
                    ---- 
                    --- B1: Các mẫu hóa đơn được phát hành trong kỳ + mẫu của hóa đơn có trạng thái phát hành hoặc Delete trong kỳ 
                    --- B1.1 : Mẫu hóa đơn có ngày Issued trong kỳ:
					-- Load all temp active trong hệ thống có ngày Issued <= Cuối kỳ & max số HĐ của temp tại Đầu ky - 1) < Đến số của Temp

					---- #tbl_Invoice_TempInvoice_AfterEndReport:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_AfterEndReport
                    from Invoice_TempInvoice iti --//[mylock]
                        --inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                        --    on iti.MST = t_MstNNT_View.MST
                    where(1=1)
	                    and iti.EffDateStart <= '@strReportDTimeTo'
						and iti.FlagActive = '1'
	                    --and iti.TInvoiceCode = 'TINVOICODE.94Q.10513'
	                    and iti.InvoiceType = '@strInvoiceType'
	                    and (N'@strSign' = '' or iti.Sign = '@strSign')
	                    and (N'@strFormNo' = '' or iti.FormNo = '@strFormNo')
                    ;   

                    --- select null tbl_Invoice_TempInvoice_AfterEndReport, t.* from #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_AfterEndReport;

					
					---- #tbl_Invoice_TempInvoice_InvoiceMax:
                    select 
	                    ii.TInvoiceCode
						, max (ii.InvoiceNo) MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_InvoiceMax
                    from Invoice_Invoice ii --//[mylock]
                        inner join #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock]
                            on ii.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and ii.InvoiceDateUTC < '@strReportDateFrom'
						and ii.InvoiceNo is not null
						--and ii.InvoiceStatus in ('ISSUED', 'DELETED')
					group by 
	                    ii.TInvoiceCode
                    ;   

                    --- select null tbl_Invoice_TempInvoice_InvoiceMax, t.* from #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InvoiceMax;					

					
					---- #tbl_Invoice_TempInvoice_Filter:
                    select 
	                    t_After.TInvoiceCode
						, t.MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_Filter
                    from #tbl_Invoice_TempInvoice_AfterEndReport t_After --//[mylock]
						left join #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock]
							on t_After.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_After.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and (t.MaxInvoiceNo < iti.EndInvoiceNo or t.TInvoiceCode is null)
                    ;   

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;


					--- #tbl_Invoice_TempInvoice_InPeriod:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_InPeriod
                    from Invoice_TempInvoice iti --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on iti.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and iti.EffDateStart >= '@strReportDateFrom'
	                    and iti.EffDateStart <= '@strReportDateTo'
	                    --and iti.TInvoiceCode = 'TINVOICODE.952.20586'
                    ;

                    --- select null tbl_Invoice_TempInvoice_InPeriod, t.* from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InPeriod;

                    --- #tbl_Invoice_Invoice_Filter:
                    select distinct
	                    ii.InvoiceCode
                    into #tbl_Invoice_Invoice_Filter
                    from Invoice_Invoice ii --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on ii.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and ii.InvoiceDateUTC >= '@strReportDateFrom'
	                    and ii.InvoiceDateUTC <= '@strReportDateTo'
						and ii.InvoiceNo is not null
						and ii.InvoiceStatus in ('ISSUED', 'DELETED')
                    ;

                    --- select null tbl_Invoice_Invoice_Filter, t.* from #tbl_Invoice_Invoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_Filter;


                    --- #tbl_Invoice_Invoice_TInvoiceInPeriod:
                    select distinct
	                    ii.TInvoiceCode
                    into #tbl_Invoice_Invoice_TInvoiceInPeriod
                    from Invoice_Invoice ii --//[mylock]
	                    inner join #tbl_Invoice_Invoice_Filter t --//[mylock] 
		                    on ii.InvoiceCode = t.InvoiceCode
                    where(1=1)
						and ii.InvoiceStatus in ('ISSUED', 'DELETED')
                    ;

                    --- select null tbl_Invoice_Invoice_TInvoiceInPeriod, t.* from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;

                    ------ B1.3: Tất cả các mẫu hóa đơn thỏa mãn điều kiện:
                    ----- #tbl_Invoice_TempInvoice_Filter:
                    --select distinct
	                   -- t.TInvoiceCode
                    --into #tbl_Invoice_TempInvoice_Filter
                    --from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock]
                    --where(1=1)
                    --union 
                    --select 
	                   -- t.TInvoiceCode
                    --from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock]
                    --where(1=1)
                    --;

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;

                    ----- B2: Tính các khối (K1, K2, K3):
                    ----------------------------------------------- K1: Số tồn đầu kỳ, phát hành trong kỳ  ------------------------------------------------------------
                    ---  Tính K1: Số tồn đầu kỳ, phát hành trong kỳ:
                    -- K1.I/ Số đầu kỳ:
	                    -- Từ số (6) =  Max (Số hóa đơn có ngày Issued <= đầu kỳ)
	                    -- Nếu ngày issued của Temp >= Đầu kỳ => Cột (6), (7) = NULL
	                    -- Nếu ngày issued của Temp < Đầu kỳ => Tính Cột (6), (7) như sau:
		                    -- (6) = max (Số HĐ của Temp đó có ngày issued <Đầu kỳ) + 1;
		                    -- (7) = Đến số của Temp : Đây là công thưc cho hệ thống hiện tại
                            --Nếu sau này 1 mẫu được phát hành nhiều lần:
                            --=> Có lịch sử phát hành: Ở trên phải so sánh với Ngày phát hành của lần phát hành đó(Dx);  Đến số của ngày phát hành đó.


                       -- Đến số(7) = Đến số theo mẫu
                    -- K2.II / Số phát sinh trong kỳ :
	                    --Từ số - đến số của mẫu Hóa đơn nếu Ngày phát hành của mãu HĐ  thuộc kỳ được xét

                        --Nếu ko thì = NULL
                        -- - Từ số(8) = Từ số của mẫu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét
                        -- - Đến số(9) = Đến số của mãu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét.
                    -- K3.III / Tổng số
                        -- - Tổng số(5) = A + B
                          --[A = (9) - (8) + 1] nếu(8) và(9) khác null; [A=0] nếu(8)= Null hoặc(9)=null.
	                    -- [B=(7)-(6) + 1] nếu(6) và(7) khác null; [B=0] nếu(6)= Null hoặc(7)=null.

                    -- #tbl_K1_BeginPeriod_6:
                    select
                        t.TInvoiceCode
                        , (Convert(int, max(ii.InvoiceNo))) K1_BeginPeriod_Start_6
                    into #tbl_K1_BeginPeriod_6
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.TInvoiceCode = ii.TInvoiceCode
                    where(1=1)

                        and ii.InvoiceDateUTC < '@strReportDateFrom'
                    group by
                        t.TInvoiceCode
                    ;

                    --- select null tbl_K1_BeginPeriod_6, t.* from #tbl_K1_BeginPeriod_6 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6;

                    -- #tbl_K1_BeginPeriod_6_7:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								when iti.EffDateStart < '@strReportDateFrom' and f.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else (IsNull(f.K1_BeginPeriod_Start_6, 0) + 1) 
							End
						) K1_BeginPeriod_Start_6
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								else iti.EndInvoiceNo
							End
						) K1_BeginPeriod_End_7
                    into #tbl_K1_BeginPeriod_6_7
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1_BeginPeriod_6 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_BeginPeriod_6_7, t.* from #tbl_K1_BeginPeriod_6_7 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6_7;

                    -- #tbl_K1_InPeriod_8_9:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when k.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else null
							End 
						) K1_InPeriod_Start_8
						, (
							Case 
								when k.K1_BeginPeriod_End_7 is null then iti.EndInvoiceNo
								else null
							End 
						) K1_InPeriod_End_9
                    into #tbl_K1_InPeriod_8_9
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join #tbl_Invoice_TempInvoice_InPeriod f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        inner join Invoice_TempInvoice iti --//[mylock]
                            on f.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1_BeginPeriod_6_7 k --//[mylock]
							on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_InPeriod_8_9, t.* from #tbl_K1_InPeriod_8_9 t --//[mylock];
                    --- drop table #tbl_K1_InPeriod_8_9;

                    -- #tbl_K1:
                    select
                        t.TInvoiceCode
                        , f.K1_BeginPeriod_Start_6

                        , (
                            Case
                                when f.K1_BeginPeriod_Start_6 is not null then f.K1_BeginPeriod_End_7
                            End
                        ) K1_BeginPeriod_End_7
	                    , k.K1_InPeriod_Start_8
	                    , k.K1_InPeriod_End_9
	                    , (
                            Case
                                when (k.K1_InPeriod_Start_8 is null or k.K1_InPeriod_End_9 is null) then(IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 != 0 then(IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 = 0 then(IsNull(k.K1_InPeriod_End_9, 0) - (IsNull(K1_InPeriod_Start_8, 0) + 1)   + 1)
			                    --else (IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1 + IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
		                    End 
	                    ) K1_TongSo_5
                    into #tbl_K1
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K1_BeginPeriod_6_7 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        left join #tbl_K1_InPeriod_8_9 k --//[mylock]
		                    on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1, t.* from #tbl_K1 t --//[mylock];
                    --- drop table #tbl_K1;

                    --------------------------------------------- K2 ---------------------------------------
                    --- Số sử dụng, điều chỉnh, thay thế, xóa bỏ:
                    --  Tổng số:
	                    -- Từ số(10) : Lấy số min của các HĐ có trạng thái ISSUED hoặc DELETED có ngày ISSUED trong kỳ.
	                    -- Đến số(11): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ
	                    -- Cộng(12) =(11)-(10)+1 
                    -- Trong đó: 
	                    -- Số lượng sử dụng(13): Tổng(count) các HĐ có trạng thái ISSUED hoặc DELETED & ngày ISSUED trong kỳ
	                    -- Xóa bỏ: 
		                    -- Số lượng(18): Các số HĐ có trạng thái Deleted được tạo ra trong kỳ
		                    -- Số(19) : 00000012, 000000018

                    ---- #tbl_K2_Used_Adj_Replace_Start_10:
                    select
                        ii.TInvoiceCode
                        , Convert(int, min(ii.InvoiceNo)) Start_10
                    into #tbl_K2_Used_Adj_Replace_Start_10
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Start_10, t.* from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Start_10;

                    ---- #tbl_K2_Used_Adj_Replace_End_11:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) End_11
                    into #tbl_K2_Used_Adj_Replace_End_11
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_End_11, t.* from #tbl_K2_Used_Adj_Replace_End_11 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_End_11;

                    ---- #tbl_K2_Used_Adj_Replace_Total_12:
                    select
                        t.TInvoiceCode
                        , (f.End_11 - t.Start_10 + 1) Total_12
                    into #tbl_K2_Used_Adj_Replace_Total_12
                    from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock]
	                    inner join #tbl_K2_Used_Adj_Replace_End_11 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_12, t.* from #tbl_K2_Used_Adj_Replace_Total_12 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_12;

                    ---- #tbl_K2_Used_Adj_Replace_Total_13:
                    select
                        ii.TInvoiceCode
                        , Count(ii.InvoiceCode) Total_13
                    into #tbl_K2_Used_Adj_Replace_Total_13
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_13, t.* from #tbl_K2_Used_Adj_Replace_Total_13 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_13;


                    ---- #tbl_K2_Used_Adj_Replace_Dtl_18:
                    select
                        ii.TInvoiceCode
                        , ii.InvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Dtl_18
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                        and ii.InvoiceStatus = 'DELETED'
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Dtl_18, t.* from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Dtl_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18:
                    select
                        t.TInvoiceCode
                        , count(t.InvoiceNo) Total_18
                    into #tbl_K2_Used_Adj_Replace_Total_18
                    from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock]
                    where(1=1)
                    group by

                        t.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18, t.* from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18_19:
                    select
                        t.TInvoiceCode
                        , t.Total_18
                        , STUFF((
                                SELECT ',' + f.InvoiceNo
                                FROM #tbl_K2_Used_Adj_Replace_Dtl_18 f --//[mylock]
		                        WHERE(1=1)
                                    and t.TInvoiceCode = f.TInvoiceCode
                                FOR
                                XML PATH('')
		                        ), 1, 1, ''
                            ) AS ListInvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Total_18_19
                    from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock]
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18_19, t.* from #tbl_K2_Used_Adj_Replace_Total_18_19 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18_19;

                    ---- #tbl_K2:
                    select
                        t.TInvoiceCode
                        , t_k2_10.Start_10
                        , t_k2_11.End_11
                        , t_k2_12.Total_12
                        , t_k2_13.Total_13
                        , t_k2_18_19.Total_18
                        , t_k2_18_19.ListInvoiceNo
                    into #tbl_K2
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K2_Used_Adj_Replace_Start_10 t_k2_10 --//[mylock]
		                    on t.TInvoiceCode = t_k2_10.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_End_11 t_k2_11 --//[mylock]
		                    on t.TInvoiceCode = t_k2_11.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_12 t_k2_12 --//[mylock]
		                    on t.TInvoiceCode = t_k2_12.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_13 t_k2_13 --//[mylock]
		                    on t.TInvoiceCode = t_k2_13.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_18_19 t_k2_18_19 --//[mylock]
		                    on t.TInvoiceCode = t_k2_18_19.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2, t.* from #tbl_K2 t --//[mylock];
                    --- drop table #tbl_K2;


                    --------------------------------- K3 -----------------------------------------
                    --- cuối kỳ:
                    --- Từ số(20): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ + 1
                    --- Đến số(21): = Đến số của phát hành mẫu có ngày phát hành <= Cuối Kỳ.
                    --- Số lượng (22): (21) - 20)
                    ---- #tbl_K3_EndPeriod_Start_Max:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) MaxInvoiceNo
                    into #tbl_K3_EndPeriod_Start_Max
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K3_EndPeriod_Start_Max, t.* from #tbl_K3_EndPeriod_Start_Max t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_Max;

                    ---- #tbl_K3_EndPeriod_Remain:
                    select
                        t_iti.TInvoiceCode
						, (iti.EndInvoiceNo - IsNull(t_k2.Total_13, 0) - ISNULL(t_k1.K1_BeginPeriod_Start_6, 0)) QtyRemain
                    into #tbl_K3_EndPeriod_Remain
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Remain, t.* from #tbl_K3_EndPeriod_Remain t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Remain;

                    ---- #tbl_K3_EndPeriod_Start_20:
                    select
                        t_iti.TInvoiceCode
						, (
							Case 
								when iti.EndInvoiceNo = t_remain.QtyRemain  and iti.EndInvoiceNo != 0 then iti.StartInvoiceNo
								when t.MaxInvoiceNo is null and t_k2.Start_10 is not null  then t_k2.Start_10
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_BeginPeriod_Start_6 is not null then t_k1.K1_BeginPeriod_Start_6
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_InPeriod_Start_8 is not null then t_k1.K1_InPeriod_Start_8
								when iti.EndInvoiceNo = 0 then '0'
								else t.MaxInvoiceNo + 1
							End 
						) Start_20
                    into #tbl_K3_EndPeriod_Start_20
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K3_EndPeriod_Remain t_remain --//[mylock]
							on t_iti.TInvoiceCode = t_remain.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Start_20, t.* from #tbl_K3_EndPeriod_Start_20 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_20;

                    ---- #tbl_K3_EndPeriod_End_21:
                    select
                        t.TInvoiceCode
						, (
							Case
								when f.Start_20 is null then 0 
								else iti.EndInvoiceNo
							End
						) End_21
                    into #tbl_K3_EndPeriod_End_21
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K3_EndPeriod_Start_20 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_End_21, t.* from #tbl_K3_EndPeriod_End_21 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_End_21; 

                    ---- #tbl_K3:
                    select
                        t.TInvoiceCode
                        , IsNull(t_k3_20.Start_20, 0) Start_20
	                    , IsNull(t_k3_21.End_21, 0) End_21
						, (
							Case 
								when t_k3_20.Start_20 is null then 0
								--when t_k3_20.Start_20 = 1 then (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 2)
								else (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 1)
							End
						) Remain_22
                    into #tbl_K3
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K3_EndPeriod_Start_20 t_k3_20 --//[mylock]
		                    on t.TInvoiceCode = t_k3_20.TInvoiceCode
                        left join #tbl_K3_EndPeriod_End_21 t_k3_21 --//[mylock]
		                    on t.TInvoiceCode = t_k3_21.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3, t.* from #tbl_K3 t --//[mylock];
                    --- drop table #tbl_K3;

                    ------ Return:
                    select
                        iti.TInvoiceCode
                        , iti.InvoiceType
                        , iti.FormNo
                        , iti.Sign
                        , k1.K1_TongSo_5 K1_TongSo
                        , Convert(nvarchar, k1.K1_BeginPeriod_Start_6) K1_BeginPeriod_Start
                        , Convert(nvarchar, k1.K1_BeginPeriod_End_7) K1_BeginPeriod_End
						, (
							Case 
								when (k1.K1_InPeriod_End_9 is not null and k1.K1_InPeriod_Start_8 is null or (k1.K1_InPeriod_Start_8 = 0 and k1.K1_InPeriod_End_9 != 0))  then '1'
								else Convert(nvarchar, k1.K1_InPeriod_Start_8)
							End
						) K1_InPeriod_Start
                        --, Convert(nvarchar, k1.K1_InPeriod_Start_8) K1_InPeriod_Start
                        , Convert(nvarchar, k1.K1_InPeriod_End_9) K1_InPeriod_End
	                    ----
	                    , Convert(nvarchar, k2.Start_10) K2_TongSo_Start
                        , Convert(nvarchar, k2.End_11) K2_TongSo_End
                        , k2.Total_12 K2_Total
                        , k2.Total_13 K2_TotalUsed
                        , k2.Total_18 K2_TotalDel
                        , k2.ListInvoiceNo K2_ListInvoiceNo
	                    -----
	                    , Convert(nvarchar, k3.Start_20) K3_EndPeriod_Start
                        , Convert(nvarchar, k3.End_21) K3_EndPeriod_End
                        , k3.Remain_22 K3_EndPeriod_Remain
                    into #tbl_Return
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1 k1 --//[mylock]
		                    on t.TInvoiceCode = k1.TInvoiceCode
                        left join #tbl_K2 k2 --//[mylock]
		                    on t.TInvoiceCode = k2.TInvoiceCode
                        left join #tbl_K3 k3 --//[mylock]
		                    on t.TInvoiceCode = k3.TInvoiceCode
                    where(1=1)
                    ;

                    --- Return:

                    select
                        t.TInvoiceCode
                        , t.InvoiceType
						, mit.InvoiceTypeName
                        , t.FormNo
                        , t.Sign
                        , t.K1_TongSo
                        , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_Start)) + t.K1_BeginPeriod_Start) K1_BeginPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_End)) + t.K1_BeginPeriod_End) K1_BeginPeriod_End
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_Start)) + t.K1_InPeriod_Start) K1_InPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_End)) + t.K1_InPeriod_End) K1_InPeriod_End
	                    ----
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_Start)) + t.K2_TongSo_Start) K2_TongSo_Start
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_End)) + t.K2_TongSo_End) K2_TongSo_End
	                    , t.K2_Total
	                    , t.K2_TotalUsed
	                    , t.K2_TotalDel
	                    , t.K2_ListInvoiceNo K2_ListInvoiceNoDel
	                    -----
	                    , (REPLICATE('0', 7 - LEN(t.K3_EndPeriod_Start)) + t.K3_EndPeriod_Start) K3_EndPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K3_EndPeriod_End)) + t.K3_EndPeriod_End) K3_EndPeriod_End
	                    , t.K3_EndPeriod_Remain
                    from #tbl_Return t --//[mylock]
						inner join Mst_InvoiceType mit --//[mylock]
							on t.InvoiceType = mit.InvoiceType
                    where(1=1)
                    ;


                    --------------- Clear For Debug:
                    drop table #tbl_Invoice_TempInvoice_InPeriod;
                    drop table #tbl_Invoice_Invoice_Filter;
                    drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;
                    drop table #tbl_Invoice_TempInvoice_Filter;
                    drop table #tbl_K1_BeginPeriod_6;
                    drop table #tbl_K1_BeginPeriod_6_7;
                    drop table #tbl_K1_InPeriod_8_9;
                    drop table #tbl_K1;
                    drop table #tbl_K2_Used_Adj_Replace_Start_10;
                    drop table #tbl_K2_Used_Adj_Replace_End_11;
                    drop table #tbl_K2_Used_Adj_Replace_Total_12;
                    drop table #tbl_K2_Used_Adj_Replace_Total_13;
                    drop table #tbl_K2_Used_Adj_Replace_Dtl_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18_19;
                    drop table #tbl_K2;
                    drop table #tbl_K3_EndPeriod_Start_20;
                    drop table #tbl_K3_EndPeriod_End_21;
                    drop table #tbl_K3;
                    drop table #tbl_Return;
                    drop table #tbl_Invoice_TempInvoice_AfterEndReport;
                    drop table #tbl_Invoice_TempInvoice_InvoiceMax;
                    drop table #tbl_K3_EndPeriod_Start_Max;		
                    drop table #tbl_K3_EndPeriod_Remain;				
					"
                , "@strReportDTimeFrom", strReportDTimeFrom
                , "@strReportDTimeTo", strReportDTimeTo
                , "@strReportDateFrom", strReportDateFrom
                , "@strReportDateTo", strReportDateTo
                , "@strInvoiceType", strInvoiceType
                , "@strSign", strSign
                , "@strFormNo", strFormNo
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvoiceInvoice_ResultUsed";
            #endregion
        }

        /// <summary>
        /// Nâng cấp 2019-10-07
        /// By: Thomptt
        /// Nội dung: Cột 13: Số lượng hóa đơn sử dụng chỉ thấy trạng thái ISSUED và ngày ISSUED trong kỳ
        /// </summary>
        private void Rpt_InvoiceInvoice_ResultUsedX_New20191007(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            , string strInvoiceType
            , string strSign
            , string strFormNo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvoiceInvoice_ResultUsedX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeFrom", strReportDTimeFrom
                , "strReportDTimeTo", strReportDTimeTo
                , "strInvoiceType", strInvoiceType
                , "strSign", strSign
                , "strFormNo", strFormNo
                });
            #endregion

            #region // Check:
            //// Refine:
            strReportDTimeFrom = TUtils.CUtils.StdDTimeBeginDay(strReportDTimeFrom);
            strReportDTimeTo = TUtils.CUtils.StdDTimeEndDay(strReportDTimeTo);
            string strReportDateFrom = TUtils.CUtils.StdDate(strReportDTimeFrom);
            string strReportDateTo = TUtils.CUtils.StdDate(strReportDTimeTo);
            strInvoiceType = TUtils.CUtils.StdParam(strInvoiceType);
            strSign = string.Format("{0}", strSign).Trim();
            strFormNo = string.Format("{0}", strFormNo).Trim();
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"					
					--Báo cáo tình hình sử dụng hóa đơn của đơn vị (BC26/AC)
                    ---- 
                    --- B1: Các mẫu hóa đơn được phát hành trong kỳ + mẫu của hóa đơn có trạng thái phát hành hoặc Delete trong kỳ 
                    --- B1.1 : Mẫu hóa đơn có ngày Issued trong kỳ:
					-- Load all temp active trong hệ thống có ngày Issued <= Cuối kỳ & max số HĐ của temp tại Đầu ky - 1) < Đến số của Temp

					---- #tbl_Invoice_TempInvoice_AfterEndReport:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_AfterEndReport
                    from Invoice_TempInvoice iti --//[mylock]
                        --inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                        --    on iti.MST = t_MstNNT_View.MST
                    where(1=1)
	                    and iti.EffDateStart <= '@strReportDTimeTo'
						and iti.FlagActive = '1'
	                    --and iti.TInvoiceCode = 'TINVOICODE.94Q.10513'
	                    and iti.InvoiceType = '@strInvoiceType'
	                    and (N'@strSign' = '' or iti.Sign = '@strSign')
	                    and (N'@strFormNo' = '' or iti.FormNo = '@strFormNo')
                    ;   

                    --- select null tbl_Invoice_TempInvoice_AfterEndReport, t.* from #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_AfterEndReport;

					
					---- #tbl_Invoice_TempInvoice_InvoiceMax:
                    select 
	                    ii.TInvoiceCode
						, max (ii.InvoiceNo) MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_InvoiceMax
                    from Invoice_Invoice ii --//[mylock]
                        inner join #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock]
                            on ii.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and ii.InvoiceDateUTC < '@strReportDateFrom'
						and ii.InvoiceNo is not null
						--and ii.InvoiceStatus in ('ISSUED', 'DELETED')
					group by 
	                    ii.TInvoiceCode
                    ;   

                    --- select null tbl_Invoice_TempInvoice_InvoiceMax, t.* from #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InvoiceMax;					

					
					---- #tbl_Invoice_TempInvoice_Filter:
                    select 
	                    t_After.TInvoiceCode
						, t.MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_Filter
                    from #tbl_Invoice_TempInvoice_AfterEndReport t_After --//[mylock]
						left join #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock]
							on t_After.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_After.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and (t.MaxInvoiceNo < iti.EndInvoiceNo or t.TInvoiceCode is null)
                    ;   

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;


					--- #tbl_Invoice_TempInvoice_InPeriod:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_InPeriod
                    from Invoice_TempInvoice iti --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on iti.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and iti.EffDateStart >= '@strReportDateFrom'
	                    and iti.EffDateStart <= '@strReportDateTo'
	                    --and iti.TInvoiceCode = 'TINVOICODE.952.20586'
                    ;

                    --- select null tbl_Invoice_TempInvoice_InPeriod, t.* from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InPeriod;

                    --- #tbl_Invoice_Invoice_Filter:
                    select distinct
	                    ii.InvoiceCode
                    into #tbl_Invoice_Invoice_Filter
                    from Invoice_Invoice ii --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on ii.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and ii.InvoiceDateUTC >= '@strReportDateFrom'
	                    and ii.InvoiceDateUTC <= '@strReportDateTo'
						and ii.InvoiceNo is not null
						and ii.InvoiceStatus in ('ISSUED', 'DELETED', 'CANCELED') -- 20191016- Anh Hương yêu cầu thêm trạng thái Canceled
                    ;

                    --- select null tbl_Invoice_Invoice_Filter, t.* from #tbl_Invoice_Invoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_Filter;


                    --- #tbl_Invoice_Invoice_TInvoiceInPeriod:
                    select distinct
	                    ii.TInvoiceCode
                    into #tbl_Invoice_Invoice_TInvoiceInPeriod
                    from Invoice_Invoice ii --//[mylock]
	                    inner join #tbl_Invoice_Invoice_Filter t --//[mylock] 
		                    on ii.InvoiceCode = t.InvoiceCode
                    where(1=1)
						and ii.InvoiceStatus in ('ISSUED', 'DELETED')
                    ;

                    --- select null tbl_Invoice_Invoice_TInvoiceInPeriod, t.* from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;

                    ------ B1.3: Tất cả các mẫu hóa đơn thỏa mãn điều kiện:
                    ----- #tbl_Invoice_TempInvoice_Filter:
                    --select distinct
	                   -- t.TInvoiceCode
                    --into #tbl_Invoice_TempInvoice_Filter
                    --from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock]
                    --where(1=1)
                    --union 
                    --select 
	                   -- t.TInvoiceCode
                    --from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock]
                    --where(1=1)
                    --;

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;

                    ----- B2: Tính các khối (K1, K2, K3):
                    ----------------------------------------------- K1: Số tồn đầu kỳ, phát hành trong kỳ  ------------------------------------------------------------
                    ---  Tính K1: Số tồn đầu kỳ, phát hành trong kỳ:
                    -- K1.I/ Số đầu kỳ:
	                    -- Từ số (6) =  Max (Số hóa đơn có ngày Issued <= đầu kỳ)
	                    -- Nếu ngày issued của Temp >= Đầu kỳ => Cột (6), (7) = NULL
	                    -- Nếu ngày issued của Temp < Đầu kỳ => Tính Cột (6), (7) như sau:
		                    -- (6) = max (Số HĐ của Temp đó có ngày issued <Đầu kỳ) + 1;
		                    -- (7) = Đến số của Temp : Đây là công thưc cho hệ thống hiện tại
                            --Nếu sau này 1 mẫu được phát hành nhiều lần:
                            --=> Có lịch sử phát hành: Ở trên phải so sánh với Ngày phát hành của lần phát hành đó(Dx);  Đến số của ngày phát hành đó.


                       -- Đến số(7) = Đến số theo mẫu
                    -- K2.II / Số phát sinh trong kỳ :
	                    --Từ số - đến số của mẫu Hóa đơn nếu Ngày phát hành của mãu HĐ  thuộc kỳ được xét

                        --Nếu ko thì = NULL
                        -- - Từ số(8) = Từ số của mẫu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét
                        -- - Đến số(9) = Đến số của mãu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét.
                    -- K3.III / Tổng số
                        -- - Tổng số(5) = A + B
                          --[A = (9) - (8) + 1] nếu(8) và(9) khác null; [A=0] nếu(8)= Null hoặc(9)=null.
	                    -- [B=(7)-(6) + 1] nếu(6) và(7) khác null; [B=0] nếu(6)= Null hoặc(7)=null.

                    -- #tbl_K1_BeginPeriod_6:
                    select
                        t.TInvoiceCode
                        , (Convert(int, max(ii.InvoiceNo))) K1_BeginPeriod_Start_6
                    into #tbl_K1_BeginPeriod_6
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.TInvoiceCode = ii.TInvoiceCode
                    where(1=1)

                        and ii.InvoiceDateUTC < '@strReportDateFrom'
                    group by
                        t.TInvoiceCode
                    ;

                    --- select null tbl_K1_BeginPeriod_6, t.* from #tbl_K1_BeginPeriod_6 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6;

                    -- #tbl_K1_BeginPeriod_6_7:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								when iti.EffDateStart < '@strReportDateFrom' and f.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else (IsNull(f.K1_BeginPeriod_Start_6, 0) + 1) 
							End
						) K1_BeginPeriod_Start_6
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								else iti.EndInvoiceNo
							End
						) K1_BeginPeriod_End_7
                    into #tbl_K1_BeginPeriod_6_7
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1_BeginPeriod_6 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_BeginPeriod_6_7, t.* from #tbl_K1_BeginPeriod_6_7 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6_7;

                    -- #tbl_K1_InPeriod_8_9:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when k.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else null
							End 
						) K1_InPeriod_Start_8
						, (
							Case 
								when k.K1_BeginPeriod_End_7 is null then iti.EndInvoiceNo
								else null
							End 
						) K1_InPeriod_End_9
                    into #tbl_K1_InPeriod_8_9
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join #tbl_Invoice_TempInvoice_InPeriod f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        inner join Invoice_TempInvoice iti --//[mylock]
                            on f.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1_BeginPeriod_6_7 k --//[mylock]
							on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_InPeriod_8_9, t.* from #tbl_K1_InPeriod_8_9 t --//[mylock];
                    --- drop table #tbl_K1_InPeriod_8_9;

                    -- #tbl_K1:
                    select
                        t.TInvoiceCode
                        , f.K1_BeginPeriod_Start_6

                        , (
                            Case
                                when f.K1_BeginPeriod_Start_6 is not null then f.K1_BeginPeriod_End_7
                            End
                        ) K1_BeginPeriod_End_7
	                    , k.K1_InPeriod_Start_8
	                    , k.K1_InPeriod_End_9
	                    , (
                            Case
                                when (k.K1_InPeriod_Start_8 is null or k.K1_InPeriod_End_9 is null) then(IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 != 0 then(IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 = 0 then(IsNull(k.K1_InPeriod_End_9, 0) - (IsNull(K1_InPeriod_Start_8, 0) + 1)   + 1)
			                    --else (IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1 + IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
		                    End 
	                    ) K1_TongSo_5
                    into #tbl_K1
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K1_BeginPeriod_6_7 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        left join #tbl_K1_InPeriod_8_9 k --//[mylock]
		                    on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1, t.* from #tbl_K1 t --//[mylock];
                    --- drop table #tbl_K1;

                    --------------------------------------------- K2 ---------------------------------------
                    --- Số sử dụng, điều chỉnh, thay thế, xóa bỏ:
                    --  Tổng số:
	                    -- Từ số(10) : Lấy số min của các HĐ có trạng thái ISSUED hoặc DELETED có ngày ISSUED trong kỳ.
	                    -- Đến số(11): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ
	                    -- Cộng(12) =(11)-(10)+1 
                    -- Trong đó: 
	                    -- Số lượng sử dụng(13): Tổng(count) các HĐ có trạng thái ISSUED & ngày ISSUED trong kỳ
	                    -- Xóa bỏ: 
		                    -- Số lượng(18): Các số HĐ có trạng thái Deleted và Cancelled có ngày HĐ ra trong kỳ
		                    -- Số(19) : 00000012, 000000018

                    ---- #tbl_K2_Used_Adj_Replace_Start_10:
                    select
                        ii.TInvoiceCode
                        , Convert(int, min(ii.InvoiceNo)) Start_10
                    into #tbl_K2_Used_Adj_Replace_Start_10
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Start_10, t.* from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Start_10;

                    ---- #tbl_K2_Used_Adj_Replace_End_11:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) End_11
                    into #tbl_K2_Used_Adj_Replace_End_11
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_End_11, t.* from #tbl_K2_Used_Adj_Replace_End_11 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_End_11;

                    ---- #tbl_K2_Used_Adj_Replace_Total_12:
                    select
                        t.TInvoiceCode
                        , (f.End_11 - t.Start_10 + 1) Total_12
                    into #tbl_K2_Used_Adj_Replace_Total_12
                    from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock]
	                    inner join #tbl_K2_Used_Adj_Replace_End_11 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_12, t.* from #tbl_K2_Used_Adj_Replace_Total_12 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_12;

                    ---- #tbl_K2_Used_Adj_Replace_Total_13:
                    select
                        ii.TInvoiceCode
                        , Count(ii.InvoiceCode) Total_13
                    into #tbl_K2_Used_Adj_Replace_Total_13
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
						and ii.InvoiceStatus in ('ISSUED')
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_13, t.* from #tbl_K2_Used_Adj_Replace_Total_13 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_13;


                    ---- #tbl_K2_Used_Adj_Replace_Dtl_18:
                    select
                        ii.TInvoiceCode
                        , ii.InvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Dtl_18
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                        --and ii.InvoiceStatus = 'DELETED'
						and ii.InvoiceStatus in ('DELETED', 'CANCELED') -- 20191016- Anh Hương yêu cầu thêm trạng thái Canceled
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Dtl_18, t.* from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Dtl_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18:
                    select
                        t.TInvoiceCode
                        , count(t.InvoiceNo) Total_18
                    into #tbl_K2_Used_Adj_Replace_Total_18
                    from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock]
                    where(1=1)
                    group by

                        t.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18, t.* from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18_19:
                    select
                        t.TInvoiceCode
                        , t.Total_18
                        , STUFF((
                                SELECT ',' + f.InvoiceNo
                                FROM #tbl_K2_Used_Adj_Replace_Dtl_18 f --//[mylock]
		                        WHERE(1=1)
                                    and t.TInvoiceCode = f.TInvoiceCode
                                FOR
                                XML PATH('')
		                        ), 1, 1, ''
                            ) AS ListInvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Total_18_19
                    from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock]
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18_19, t.* from #tbl_K2_Used_Adj_Replace_Total_18_19 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18_19;

                    ---- #tbl_K2:
                    select
                        t.TInvoiceCode
                        , t_k2_10.Start_10
                        , t_k2_11.End_11
                        , t_k2_12.Total_12
                        , t_k2_13.Total_13
                        , t_k2_18_19.Total_18
                        , t_k2_18_19.ListInvoiceNo
                    into #tbl_K2
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K2_Used_Adj_Replace_Start_10 t_k2_10 --//[mylock]
		                    on t.TInvoiceCode = t_k2_10.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_End_11 t_k2_11 --//[mylock]
		                    on t.TInvoiceCode = t_k2_11.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_12 t_k2_12 --//[mylock]
		                    on t.TInvoiceCode = t_k2_12.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_13 t_k2_13 --//[mylock]
		                    on t.TInvoiceCode = t_k2_13.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_18_19 t_k2_18_19 --//[mylock]
		                    on t.TInvoiceCode = t_k2_18_19.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2, t.* from #tbl_K2 t --//[mylock];
                    --- drop table #tbl_K2;


                    --------------------------------- K3 -----------------------------------------
                    --- cuối kỳ:
                    --- Từ số(20): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ + 1
                    --- Đến số(21): = Đến số của phát hành mẫu có ngày phát hành <= Cuối Kỳ.
                    --- Số lượng (22): (21) - 20)
                    ---- #tbl_K3_EndPeriod_Start_Max:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) MaxInvoiceNo
                    into #tbl_K3_EndPeriod_Start_Max
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K3_EndPeriod_Start_Max, t.* from #tbl_K3_EndPeriod_Start_Max t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_Max;

                    ---- #tbl_K3_EndPeriod_Remain:
                    select
                        t_iti.TInvoiceCode
						, (iti.EndInvoiceNo - IsNull(t_k2.Total_13, 0) - ISNULL(t_k1.K1_BeginPeriod_Start_6, 0)) QtyRemain
                    into #tbl_K3_EndPeriod_Remain
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Remain, t.* from #tbl_K3_EndPeriod_Remain t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Remain;

                    ---- #tbl_K3_EndPeriod_Start_20:
                    select
                        t_iti.TInvoiceCode
						, (
							Case 
								when iti.EndInvoiceNo = t_remain.QtyRemain  and iti.EndInvoiceNo != 0 then iti.StartInvoiceNo
								when t.MaxInvoiceNo is null and t_k2.Start_10 is not null  then t_k2.Start_10
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_BeginPeriod_Start_6 is not null then t_k1.K1_BeginPeriod_Start_6
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_InPeriod_Start_8 is not null then t_k1.K1_InPeriod_Start_8
								when iti.EndInvoiceNo = 0 then '0'
								else t.MaxInvoiceNo + 1
							End 
						) Start_20
                    into #tbl_K3_EndPeriod_Start_20
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K3_EndPeriod_Remain t_remain --//[mylock]
							on t_iti.TInvoiceCode = t_remain.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Start_20, t.* from #tbl_K3_EndPeriod_Start_20 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_20;

                    ---- #tbl_K3_EndPeriod_End_21:
                    select
                        t.TInvoiceCode
						, (
							Case
								when f.Start_20 is null then 0 
								else iti.EndInvoiceNo
							End
						) End_21
                    into #tbl_K3_EndPeriod_End_21
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K3_EndPeriod_Start_20 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_End_21, t.* from #tbl_K3_EndPeriod_End_21 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_End_21; 

                    ---- #tbl_K3:
                    select
                        t.TInvoiceCode
                        , IsNull(t_k3_20.Start_20, 0) Start_20
	                    , IsNull(t_k3_21.End_21, 0) End_21
						, (
							Case 
								when t_k3_20.Start_20 is null then 0
								--when t_k3_20.Start_20 = 1 then (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 2)
								else (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 1)
							End
						) Remain_22
                    into #tbl_K3
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K3_EndPeriod_Start_20 t_k3_20 --//[mylock]
		                    on t.TInvoiceCode = t_k3_20.TInvoiceCode
                        left join #tbl_K3_EndPeriod_End_21 t_k3_21 --//[mylock]
		                    on t.TInvoiceCode = t_k3_21.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3, t.* from #tbl_K3 t --//[mylock];
                    --- drop table #tbl_K3;

                    ------ Return:
                    select
                        iti.TInvoiceCode
                        , iti.InvoiceType
                        , iti.FormNo
                        , iti.Sign
                        , k1.K1_TongSo_5 K1_TongSo
                        , Convert(nvarchar, k1.K1_BeginPeriod_Start_6) K1_BeginPeriod_Start
                        , Convert(nvarchar, k1.K1_BeginPeriod_End_7) K1_BeginPeriod_End
						, (
							Case 
								when (k1.K1_InPeriod_End_9 is not null and k1.K1_InPeriod_Start_8 is null or (k1.K1_InPeriod_Start_8 = 0 and k1.K1_InPeriod_End_9 != 0))  then '1'
								else Convert(nvarchar, k1.K1_InPeriod_Start_8)
							End
						) K1_InPeriod_Start
                        --, Convert(nvarchar, k1.K1_InPeriod_Start_8) K1_InPeriod_Start
                        , Convert(nvarchar, k1.K1_InPeriod_End_9) K1_InPeriod_End
	                    ----
	                    , Convert(nvarchar, k2.Start_10) K2_TongSo_Start
                        , Convert(nvarchar, k2.End_11) K2_TongSo_End
                        , k2.Total_12 K2_Total
                        , k2.Total_13 K2_TotalUsed
                        , k2.Total_18 K2_TotalDel
                        , k2.ListInvoiceNo K2_ListInvoiceNo
	                    -----
	                    , Convert(nvarchar, k3.Start_20) K3_EndPeriod_Start
                        , Convert(nvarchar, k3.End_21) K3_EndPeriod_End
                        , k3.Remain_22 K3_EndPeriod_Remain
                    into #tbl_Return
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1 k1 --//[mylock]
		                    on t.TInvoiceCode = k1.TInvoiceCode
                        left join #tbl_K2 k2 --//[mylock]
		                    on t.TInvoiceCode = k2.TInvoiceCode
                        left join #tbl_K3 k3 --//[mylock]
		                    on t.TInvoiceCode = k3.TInvoiceCode
                    where(1=1)
                    ;

                    --- Return:

                    select
                        t.TInvoiceCode
                        , t.InvoiceType
						, mit.InvoiceTypeName
                        , t.FormNo
                        , t.Sign
                        , t.K1_TongSo
                        , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_Start)) + t.K1_BeginPeriod_Start) K1_BeginPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_End)) + t.K1_BeginPeriod_End) K1_BeginPeriod_End
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_Start)) + t.K1_InPeriod_Start) K1_InPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_End)) + t.K1_InPeriod_End) K1_InPeriod_End
	                    ----
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_Start)) + t.K2_TongSo_Start) K2_TongSo_Start
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_End)) + t.K2_TongSo_End) K2_TongSo_End
	                    , t.K2_Total
	                    , t.K2_TotalUsed
	                    , t.K2_TotalDel
	                    , t.K2_ListInvoiceNo K2_ListInvoiceNoDel
	                    -----
	                    , (REPLICATE('0', 7 - LEN(t.K3_EndPeriod_Start)) + t.K3_EndPeriod_Start) K3_EndPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K3_EndPeriod_End)) + t.K3_EndPeriod_End) K3_EndPeriod_End
	                    , t.K3_EndPeriod_Remain
                    from #tbl_Return t --//[mylock]
						inner join Mst_InvoiceType mit --//[mylock]
							on t.InvoiceType = mit.InvoiceType
                    where(1=1)
                    ;


                    --------------- Clear For Debug:
                    drop table #tbl_Invoice_TempInvoice_InPeriod;
                    drop table #tbl_Invoice_Invoice_Filter;
                    drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;
                    drop table #tbl_Invoice_TempInvoice_Filter;
                    drop table #tbl_K1_BeginPeriod_6;
                    drop table #tbl_K1_BeginPeriod_6_7;
                    drop table #tbl_K1_InPeriod_8_9;
                    drop table #tbl_K1;
                    drop table #tbl_K2_Used_Adj_Replace_Start_10;
                    drop table #tbl_K2_Used_Adj_Replace_End_11;
                    drop table #tbl_K2_Used_Adj_Replace_Total_12;
                    drop table #tbl_K2_Used_Adj_Replace_Total_13;
                    drop table #tbl_K2_Used_Adj_Replace_Dtl_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18_19;
                    drop table #tbl_K2;
                    drop table #tbl_K3_EndPeriod_Start_20;
                    drop table #tbl_K3_EndPeriod_End_21;
                    drop table #tbl_K3;
                    drop table #tbl_Return;
                    drop table #tbl_Invoice_TempInvoice_AfterEndReport;
                    drop table #tbl_Invoice_TempInvoice_InvoiceMax;
                    drop table #tbl_K3_EndPeriod_Start_Max;		
                    drop table #tbl_K3_EndPeriod_Remain;				
					"
                , "@strReportDTimeFrom", strReportDTimeFrom
                , "@strReportDTimeTo", strReportDTimeTo
                , "@strReportDateFrom", strReportDateFrom
                , "@strReportDateTo", strReportDateTo
                , "@strInvoiceType", strInvoiceType
                , "@strSign", strSign
                , "@strFormNo", strFormNo
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvoiceInvoice_ResultUsed";
            #endregion
        }

        private void Rpt_InvoiceInvoice_ResultUsedX_New20191106(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            , string strInvoiceType
            , string strSign
            , string strFormNo
            , string strMST
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvoiceInvoice_ResultUsedX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeFrom", strReportDTimeFrom
                , "strReportDTimeTo", strReportDTimeTo
                , "strInvoiceType", strInvoiceType
                , "strSign", strSign
                , "strFormNo", strFormNo
                });
            #endregion

            #region // Check:
            //// Refine:
            strReportDTimeFrom = TUtils.CUtils.StdDTimeBeginDay(strReportDTimeFrom);
            strReportDTimeTo = TUtils.CUtils.StdDTimeEndDay(strReportDTimeTo);
            string strReportDateFrom = TUtils.CUtils.StdDate(strReportDTimeFrom);
            string strReportDateTo = TUtils.CUtils.StdDate(strReportDTimeTo);
            strInvoiceType = TUtils.CUtils.StdParam(strInvoiceType);
            strSign = string.Format("{0}", strSign).Trim();
            strFormNo = string.Format("{0}", strFormNo).Trim();
            strMST = string.Format("{0}", strMST).Trim();
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"					
					--Báo cáo tình hình sử dụng hóa đơn của đơn vị (BC26/AC)
                    ---- 
                    --- B1: Các mẫu hóa đơn được phát hành trong kỳ + mẫu của hóa đơn có trạng thái phát hành hoặc Delete trong kỳ 
                    --- B1.1 : Mẫu hóa đơn có ngày Issued trong kỳ:
					-- Load all temp active trong hệ thống có ngày Issued <= Cuối kỳ & max số HĐ của temp tại Đầu ky - 1) < Đến số của Temp

					---- #tbl_Invoice_TempInvoice_AfterEndReport:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_AfterEndReport
                    from Invoice_TempInvoice iti --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on iti.MST = t_MstNNT_View.MST
                    where(1=1)
	                    and iti.EffDateStart <= '@strReportDTimeTo'
						and iti.FlagActive = '1'
	                    --and iti.TInvoiceCode = 'TINVOICODE.94Q.10513'
	                    and iti.InvoiceType = '@strInvoiceType'
	                    and (N'@strSign' = '' or iti.Sign = '@strSign')
	                    and (N'@strFormNo' = '' or iti.FormNo = '@strFormNo')
                        and (N'@strMST' = '' or iti.MST = '@strMST')
                    ;   

                    --- select null tbl_Invoice_TempInvoice_AfterEndReport, t.* from #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_AfterEndReport;

					
					---- #tbl_Invoice_TempInvoice_InvoiceMax:
                    select 
	                    ii.TInvoiceCode
						, max (ii.InvoiceNo) MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_InvoiceMax
                    from Invoice_Invoice ii --//[mylock]
                        inner join #tbl_Invoice_TempInvoice_AfterEndReport t --//[mylock]
                            on ii.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and ii.InvoiceDateUTC < '@strReportDateFrom'
						and ii.InvoiceNo is not null
						--and ii.InvoiceStatus in ('ISSUED', 'DELETED')
					group by 
	                    ii.TInvoiceCode
                    ;   

                    --- select null tbl_Invoice_TempInvoice_InvoiceMax, t.* from #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InvoiceMax;					

					
					---- #tbl_Invoice_TempInvoice_Filter:
                    select 
	                    t_After.TInvoiceCode
						, t.MaxInvoiceNo
                    into #tbl_Invoice_TempInvoice_Filter
                    from #tbl_Invoice_TempInvoice_AfterEndReport t_After --//[mylock]
						left join #tbl_Invoice_TempInvoice_InvoiceMax t --//[mylock]
							on t_After.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_After.TInvoiceCode = iti.TInvoiceCode
                    where(1=1)
						and (t.MaxInvoiceNo < iti.EndInvoiceNo or t.TInvoiceCode is null)
                    ;   

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;


					--- #tbl_Invoice_TempInvoice_InPeriod:
                    select distinct
	                    iti.TInvoiceCode
                    into #tbl_Invoice_TempInvoice_InPeriod
                    from Invoice_TempInvoice iti --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on iti.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and iti.EffDateStart >= '@strReportDateFrom'
	                    and iti.EffDateStart <= '@strReportDateTo'
	                    --and iti.TInvoiceCode = 'TINVOICODE.952.20586'
                    ;

                    --- select null tbl_Invoice_TempInvoice_InPeriod, t.* from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_InPeriod;

                    --- #tbl_Invoice_Invoice_Filter:
                    select distinct
	                    ii.InvoiceCode
                    into #tbl_Invoice_Invoice_Filter
                    from Invoice_Invoice ii --//[mylock]
						inner join #tbl_Invoice_TempInvoice_Filter t --//[mylock]
							on ii.TInvoiceCode = t.TInvoiceCode
                    where(1=1)
	                    and ii.InvoiceDateUTC >= '@strReportDateFrom'
	                    and ii.InvoiceDateUTC <= '@strReportDateTo'
						and ii.InvoiceNo is not null
						and ii.InvoiceStatus in ('ISSUED', 'DELETED', 'CANCELED') -- 20191016- Anh Hương yêu cầu thêm trạng thái Canceled
                    ;

                    --- select null tbl_Invoice_Invoice_Filter, t.* from #tbl_Invoice_Invoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_Filter;


                    --- #tbl_Invoice_Invoice_TInvoiceInPeriod:
                    select distinct
	                    ii.TInvoiceCode
                    into #tbl_Invoice_Invoice_TInvoiceInPeriod
                    from Invoice_Invoice ii --//[mylock]
	                    inner join #tbl_Invoice_Invoice_Filter t --//[mylock] 
		                    on ii.InvoiceCode = t.InvoiceCode
                    where(1=1)
						and ii.InvoiceStatus in ('ISSUED', 'DELETED')
                    ;

                    --- select null tbl_Invoice_Invoice_TInvoiceInPeriod, t.* from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock];
                    --- drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;

                    ------ B1.3: Tất cả các mẫu hóa đơn thỏa mãn điều kiện:
                    ----- #tbl_Invoice_TempInvoice_Filter:
                    --select distinct
	                   -- t.TInvoiceCode
                    --into #tbl_Invoice_TempInvoice_Filter
                    --from #tbl_Invoice_TempInvoice_InPeriod t --//[mylock]
                    --where(1=1)
                    --union 
                    --select 
	                   -- t.TInvoiceCode
                    --from #tbl_Invoice_Invoice_TInvoiceInPeriod t --//[mylock]
                    --where(1=1)
                    --;

                    --- select null tbl_Invoice_TempInvoice_Filter, t.* from #tbl_Invoice_TempInvoice_Filter t --//[mylock];
                    --- drop table #tbl_Invoice_TempInvoice_Filter;

                    ----- B2: Tính các khối (K1, K2, K3):
                    ----------------------------------------------- K1: Số tồn đầu kỳ, phát hành trong kỳ  ------------------------------------------------------------
                    ---  Tính K1: Số tồn đầu kỳ, phát hành trong kỳ:
                    -- K1.I/ Số đầu kỳ:
	                    -- Từ số (6) =  Max (Số hóa đơn có ngày Issued <= đầu kỳ)
	                    -- Nếu ngày issued của Temp >= Đầu kỳ => Cột (6), (7) = NULL
	                    -- Nếu ngày issued của Temp < Đầu kỳ => Tính Cột (6), (7) như sau:
		                    -- (6) = max (Số HĐ của Temp đó có ngày issued <Đầu kỳ) + 1;
		                    -- (7) = Đến số của Temp : Đây là công thưc cho hệ thống hiện tại
                            --Nếu sau này 1 mẫu được phát hành nhiều lần:
                            --=> Có lịch sử phát hành: Ở trên phải so sánh với Ngày phát hành của lần phát hành đó(Dx);  Đến số của ngày phát hành đó.


                       -- Đến số(7) = Đến số theo mẫu
                    -- K2.II / Số phát sinh trong kỳ :
	                    --Từ số - đến số của mẫu Hóa đơn nếu Ngày phát hành của mãu HĐ  thuộc kỳ được xét

                        --Nếu ko thì = NULL
                        -- - Từ số(8) = Từ số của mẫu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét
                        -- - Đến số(9) = Đến số của mãu hóa đơn nếu ngày phát hành của mẫu hóa đơn thuộc kỳ đang xét.
                    -- K3.III / Tổng số
                        -- - Tổng số(5) = A + B
                          --[A = (9) - (8) + 1] nếu(8) và(9) khác null; [A=0] nếu(8)= Null hoặc(9)=null.
	                    -- [B=(7)-(6) + 1] nếu(6) và(7) khác null; [B=0] nếu(6)= Null hoặc(7)=null.

                    -- #tbl_K1_BeginPeriod_6:
                    select
                        t.TInvoiceCode
                        , (Convert(int, max(ii.InvoiceNo))) K1_BeginPeriod_Start_6
                    into #tbl_K1_BeginPeriod_6
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.TInvoiceCode = ii.TInvoiceCode
                    where(1=1)

                        and ii.InvoiceDateUTC < '@strReportDateFrom'
                    group by
                        t.TInvoiceCode
                    ;

                    --- select null tbl_K1_BeginPeriod_6, t.* from #tbl_K1_BeginPeriod_6 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6;

                    -- #tbl_K1_BeginPeriod_6_7:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								when iti.EffDateStart < '@strReportDateFrom' and f.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else (IsNull(f.K1_BeginPeriod_Start_6, 0) + 1) 
							End
						) K1_BeginPeriod_Start_6
						, (
							Case 
								when iti.EffDateStart >= '@strReportDateFrom' then null 
								else iti.EndInvoiceNo
							End
						) K1_BeginPeriod_End_7
                    into #tbl_K1_BeginPeriod_6_7
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1_BeginPeriod_6 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_BeginPeriod_6_7, t.* from #tbl_K1_BeginPeriod_6_7 t --//[mylock];
                    --- drop table #tbl_K1_BeginPeriod_6_7;

                    -- #tbl_K1_InPeriod_8_9:
                    select
                        t.TInvoiceCode
						, (
							Case 
								when k.K1_BeginPeriod_Start_6 is null then iti.StartInvoiceNo
								else null
							End 
						) K1_InPeriod_Start_8
						, (
							Case 
								when k.K1_BeginPeriod_End_7 is null then iti.EndInvoiceNo
								else null
							End 
						) K1_InPeriod_End_9
                    into #tbl_K1_InPeriod_8_9
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join #tbl_Invoice_TempInvoice_InPeriod f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        inner join Invoice_TempInvoice iti --//[mylock]
                            on f.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1_BeginPeriod_6_7 k --//[mylock]
							on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1_InPeriod_8_9, t.* from #tbl_K1_InPeriod_8_9 t --//[mylock];
                    --- drop table #tbl_K1_InPeriod_8_9;

                    -- #tbl_K1:
                    select
                        t.TInvoiceCode
                        , f.K1_BeginPeriod_Start_6

                        , (
                            Case
                                when f.K1_BeginPeriod_Start_6 is not null then f.K1_BeginPeriod_End_7
                            End
                        ) K1_BeginPeriod_End_7
	                    , k.K1_InPeriod_Start_8
	                    , k.K1_InPeriod_End_9
	                    , (
                            Case
                                when (k.K1_InPeriod_Start_8 is null or k.K1_InPeriod_End_9 is null) then(IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 != 0 then(IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1)
			                    when(f.K1_BeginPeriod_Start_6 is null or f.K1_BeginPeriod_End_7 is null) and k.K1_InPeriod_Start_8 = 0 then(IsNull(k.K1_InPeriod_End_9, 0) - (IsNull(K1_InPeriod_Start_8, 0) + 1)   + 1)
			                    --else (IsNull(k.K1_InPeriod_End_9, 0) - IsNull(K1_InPeriod_Start_8, 0) + 1 + IsNull(f.K1_BeginPeriod_End_7, 0) - IsNull(f.K1_BeginPeriod_Start_6, 0) + 1)
		                    End 
	                    ) K1_TongSo_5
                    into #tbl_K1
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K1_BeginPeriod_6_7 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                        left join #tbl_K1_InPeriod_8_9 k --//[mylock]
		                    on t.TInvoiceCode = k.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K1, t.* from #tbl_K1 t --//[mylock];
                    --- drop table #tbl_K1;

                    --------------------------------------------- K2 ---------------------------------------
                    --- Số sử dụng, điều chỉnh, thay thế, xóa bỏ:
                    --  Tổng số:
	                    -- Từ số(10) : Lấy số min của các HĐ có trạng thái ISSUED hoặc DELETED có ngày ISSUED trong kỳ.
	                    -- Đến số(11): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ
	                    -- Cộng(12) =(11)-(10)+1 
                    -- Trong đó: 
	                    -- Số lượng sử dụng(13): Tổng(count) các HĐ có trạng thái ISSUED & ngày ISSUED trong kỳ
	                    -- Xóa bỏ: 
		                    -- Số lượng(18): Các số HĐ có trạng thái Deleted và Cancelled có ngày HĐ ra trong kỳ
		                    -- Số(19) : 00000012, 000000018

                    ---- #tbl_K2_Used_Adj_Replace_Start_10:
                    select
                        ii.TInvoiceCode
                        , Convert(int, min(ii.InvoiceNo)) Start_10
                    into #tbl_K2_Used_Adj_Replace_Start_10
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Start_10, t.* from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Start_10;

                    ---- #tbl_K2_Used_Adj_Replace_End_11:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) End_11
                    into #tbl_K2_Used_Adj_Replace_End_11
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_End_11, t.* from #tbl_K2_Used_Adj_Replace_End_11 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_End_11;

                    ---- #tbl_K2_Used_Adj_Replace_Total_12:
                    select
                        t.TInvoiceCode
                        , (f.End_11 - t.Start_10 + 1) Total_12
                    into #tbl_K2_Used_Adj_Replace_Total_12
                    from #tbl_K2_Used_Adj_Replace_Start_10 t --//[mylock]
	                    inner join #tbl_K2_Used_Adj_Replace_End_11 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_12, t.* from #tbl_K2_Used_Adj_Replace_Total_12 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_12;

                    ---- #tbl_K2_Used_Adj_Replace_Total_13:
                    select
                        ii.TInvoiceCode
                        , Count(ii.InvoiceCode) Total_13
                    into #tbl_K2_Used_Adj_Replace_Total_13
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
						and ii.InvoiceStatus in ('ISSUED')
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_13, t.* from #tbl_K2_Used_Adj_Replace_Total_13 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_13;


                    ---- #tbl_K2_Used_Adj_Replace_Dtl_18:
                    select
                        ii.TInvoiceCode
                        , ii.InvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Dtl_18
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
                            on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                        --and ii.InvoiceStatus = 'DELETED'
						and ii.InvoiceStatus in ('DELETED', 'CANCELED') -- 20191016- Anh Hương yêu cầu thêm trạng thái Canceled
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Dtl_18, t.* from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Dtl_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18:
                    select
                        t.TInvoiceCode
                        , count(t.InvoiceNo) Total_18
                    into #tbl_K2_Used_Adj_Replace_Total_18
                    from #tbl_K2_Used_Adj_Replace_Dtl_18 t --//[mylock]
                    where(1=1)
                    group by

                        t.TInvoiceCode
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18, t.* from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18;

                    ---- #tbl_K2_Used_Adj_Replace_Total_18_19:
                    select
                        t.TInvoiceCode
                        , t.Total_18
                        , STUFF((
                                SELECT ',' + f.InvoiceNo
                                FROM #tbl_K2_Used_Adj_Replace_Dtl_18 f --//[mylock]
		                        WHERE(1=1)
                                    and t.TInvoiceCode = f.TInvoiceCode
                                FOR
                                XML PATH('')
		                        ), 1, 1, ''
                            ) AS ListInvoiceNo
                    into #tbl_K2_Used_Adj_Replace_Total_18_19
                    from #tbl_K2_Used_Adj_Replace_Total_18 t --//[mylock]
                    where(1=1)
                    ;

                    --- select null tbl_K2_Used_Adj_Replace_Total_18_19, t.* from #tbl_K2_Used_Adj_Replace_Total_18_19 t --//[mylock];
                    --- drop table #tbl_K2_Used_Adj_Replace_Total_18_19;

                    ---- #tbl_K2:
                    select
                        t.TInvoiceCode
                        , t_k2_10.Start_10
                        , t_k2_11.End_11
                        , t_k2_12.Total_12
                        , t_k2_13.Total_13
                        , t_k2_18_19.Total_18
                        , t_k2_18_19.ListInvoiceNo
                    into #tbl_K2
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K2_Used_Adj_Replace_Start_10 t_k2_10 --//[mylock]
		                    on t.TInvoiceCode = t_k2_10.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_End_11 t_k2_11 --//[mylock]
		                    on t.TInvoiceCode = t_k2_11.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_12 t_k2_12 --//[mylock]
		                    on t.TInvoiceCode = t_k2_12.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_13 t_k2_13 --//[mylock]
		                    on t.TInvoiceCode = t_k2_13.TInvoiceCode
                        left join #tbl_K2_Used_Adj_Replace_Total_18_19 t_k2_18_19 --//[mylock]
		                    on t.TInvoiceCode = t_k2_18_19.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K2, t.* from #tbl_K2 t --//[mylock];
                    --- drop table #tbl_K2;


                    --------------------------------- K3 -----------------------------------------
                    --- cuối kỳ:
                    --- Từ số(20): Lấy số max của các HĐ có trạng thái ISSUED hoặc DELETED tại thời điểm Ngày cuối kỳ + 1
                    --- Đến số(21): = Đến số của phát hành mẫu có ngày phát hành <= Cuối Kỳ.
                    --- Số lượng (22): (21) - 20)
                    ---- #tbl_K3_EndPeriod_Start_Max:
                    select
                        ii.TInvoiceCode
                        , Convert(int, max(ii.InvoiceNo)) MaxInvoiceNo
                    into #tbl_K3_EndPeriod_Start_Max
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
                        ii.TInvoiceCode
                    ;

                    --- select null tbl_K3_EndPeriod_Start_Max, t.* from #tbl_K3_EndPeriod_Start_Max t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_Max;

                    ---- #tbl_K3_EndPeriod_Remain:
                    select
                        t_iti.TInvoiceCode
						, (iti.EndInvoiceNo - IsNull(t_k2.Total_13, 0) - ISNULL(t_k1.K1_BeginPeriod_Start_6, 0)) QtyRemain
                    into #tbl_K3_EndPeriod_Remain
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Remain, t.* from #tbl_K3_EndPeriod_Remain t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Remain;

                    ---- #tbl_K3_EndPeriod_Start_20:
                    select
                        t_iti.TInvoiceCode
						, (
							Case 
								when iti.EndInvoiceNo = t_remain.QtyRemain  and iti.EndInvoiceNo != 0 then iti.StartInvoiceNo
								when t.MaxInvoiceNo is null and t_k2.Start_10 is not null  then t_k2.Start_10
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_BeginPeriod_Start_6 is not null then t_k1.K1_BeginPeriod_Start_6
								when t.MaxInvoiceNo is null and t_k2.Start_10 is null  and t_k1.K1_InPeriod_Start_8 is not null then t_k1.K1_InPeriod_Start_8
								when iti.EndInvoiceNo = 0 then '0'
								else t.MaxInvoiceNo + 1
							End 
						) Start_20
                    into #tbl_K3_EndPeriod_Start_20
                    from #tbl_Invoice_TempInvoice_Filter t_iti --//[mylock]
						left join #tbl_K3_EndPeriod_Start_Max t --//[mylock]
							on t_iti.TInvoiceCode = t.TInvoiceCode
						inner join Invoice_TempInvoice iti --//[mylock]
							on t_iti.TInvoiceCode = iti.TInvoiceCode
						left join #tbl_K3_EndPeriod_Remain t_remain --//[mylock]
							on t_iti.TInvoiceCode = t_remain.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t_iti.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t_iti.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_Start_20, t.* from #tbl_K3_EndPeriod_Start_20 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_Start_20;

                    ---- #tbl_K3_EndPeriod_End_21:
                    select
                        t.TInvoiceCode
						, (
							Case
								when f.Start_20 is null then 0 
								else iti.EndInvoiceNo
							End
						) End_21
                    into #tbl_K3_EndPeriod_End_21
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K3_EndPeriod_Start_20 f --//[mylock]
		                    on t.TInvoiceCode = f.TInvoiceCode
						left join #tbl_K1 t_k1 --//[mylock]
							on t.TInvoiceCode = t_k1.TInvoiceCode
						left join #tbl_K2 t_k2 --//[mylock]
							on t.TInvoiceCode = t_k2.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3_EndPeriod_End_21, t.* from #tbl_K3_EndPeriod_End_21 t --//[mylock];
                    --- drop table #tbl_K3_EndPeriod_End_21; 

                    ---- #tbl_K3:
                    select
                        t.TInvoiceCode
                        , IsNull(t_k3_20.Start_20, 0) Start_20
	                    , IsNull(t_k3_21.End_21, 0) End_21
						, (
							Case 
								when t_k3_20.Start_20 is null then 0
								--when t_k3_20.Start_20 = 1 then (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 2)
								else (IsNull(t_k3_21.End_21, 0) - IsNull(t_k3_20.Start_20, 0) + 1)
							End
						) Remain_22
                    into #tbl_K3
                    from  #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    left join #tbl_K3_EndPeriod_Start_20 t_k3_20 --//[mylock]
		                    on t.TInvoiceCode = t_k3_20.TInvoiceCode
                        left join #tbl_K3_EndPeriod_End_21 t_k3_21 --//[mylock]
		                    on t.TInvoiceCode = t_k3_21.TInvoiceCode
                    where(1=1)
                    ;

                    --- select null tbl_K3, t.* from #tbl_K3 t --//[mylock];
                    --- drop table #tbl_K3;

                    ------ Return:
                    select
                        iti.TInvoiceCode
                        , iti.InvoiceType
                        , iti.FormNo
                        , iti.Sign
                        , k1.K1_TongSo_5 K1_TongSo
                        , Convert(nvarchar, k1.K1_BeginPeriod_Start_6) K1_BeginPeriod_Start
                        , Convert(nvarchar, k1.K1_BeginPeriod_End_7) K1_BeginPeriod_End
						, (
							Case 
								when (k1.K1_InPeriod_End_9 is not null and k1.K1_InPeriod_Start_8 is null or (k1.K1_InPeriod_Start_8 = 0 and k1.K1_InPeriod_End_9 != 0))  then '1'
								else Convert(nvarchar, k1.K1_InPeriod_Start_8)
							End
						) K1_InPeriod_Start
                        --, Convert(nvarchar, k1.K1_InPeriod_Start_8) K1_InPeriod_Start
                        , Convert(nvarchar, k1.K1_InPeriod_End_9) K1_InPeriod_End
	                    ----
	                    , Convert(nvarchar, k2.Start_10) K2_TongSo_Start
                        , Convert(nvarchar, k2.End_11) K2_TongSo_End
                        , k2.Total_12 K2_Total
                        , k2.Total_13 K2_TotalUsed
                        , k2.Total_18 K2_TotalDel
                        , k2.ListInvoiceNo K2_ListInvoiceNo
	                    -----
	                    , Convert(nvarchar, k3.Start_20) K3_EndPeriod_Start
                        , Convert(nvarchar, k3.End_21) K3_EndPeriod_End
                        , k3.Remain_22 K3_EndPeriod_Remain
                    into #tbl_Return
                    from #tbl_Invoice_TempInvoice_Filter t --//[mylock]
	                    inner join Invoice_TempInvoice iti --//[mylock]
                            on t.TInvoiceCode = iti.TInvoiceCode
                        left join #tbl_K1 k1 --//[mylock]
		                    on t.TInvoiceCode = k1.TInvoiceCode
                        left join #tbl_K2 k2 --//[mylock]
		                    on t.TInvoiceCode = k2.TInvoiceCode
                        left join #tbl_K3 k3 --//[mylock]
		                    on t.TInvoiceCode = k3.TInvoiceCode
                    where(1=1)
                    ;

                    --- Return:

                    select
                        t.TInvoiceCode
                        , t.InvoiceType
						, mit.InvoiceTypeName
                        , t.FormNo
                        , t.Sign
                        , t.K1_TongSo
                        , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_Start)) + t.K1_BeginPeriod_Start) K1_BeginPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_BeginPeriod_End)) + t.K1_BeginPeriod_End) K1_BeginPeriod_End
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_Start)) + t.K1_InPeriod_Start) K1_InPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K1_InPeriod_End)) + t.K1_InPeriod_End) K1_InPeriod_End
	                    ----
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_Start)) + t.K2_TongSo_Start) K2_TongSo_Start
	                    , (REPLICATE('0',7-LEN(t.K2_TongSo_End)) + t.K2_TongSo_End) K2_TongSo_End
	                    , t.K2_Total
	                    , t.K2_TotalUsed
	                    , t.K2_TotalDel
	                    , t.K2_ListInvoiceNo K2_ListInvoiceNoDel
	                    -----
	                    , (REPLICATE('0', 7 - LEN(t.K3_EndPeriod_Start)) + t.K3_EndPeriod_Start) K3_EndPeriod_Start
	                    , (REPLICATE('0',7-LEN(t.K3_EndPeriod_End)) + t.K3_EndPeriod_End) K3_EndPeriod_End
	                    , t.K3_EndPeriod_Remain
                    from #tbl_Return t --//[mylock]
						inner join Mst_InvoiceType mit --//[mylock]
							on t.InvoiceType = mit.InvoiceType
                    where(1=1)
                    ;


                    --------------- Clear For Debug:
                    drop table #tbl_Invoice_TempInvoice_InPeriod;
                    drop table #tbl_Invoice_Invoice_Filter;
                    drop table #tbl_Invoice_Invoice_TInvoiceInPeriod;
                    drop table #tbl_Invoice_TempInvoice_Filter;
                    drop table #tbl_K1_BeginPeriod_6;
                    drop table #tbl_K1_BeginPeriod_6_7;
                    drop table #tbl_K1_InPeriod_8_9;
                    drop table #tbl_K1;
                    drop table #tbl_K2_Used_Adj_Replace_Start_10;
                    drop table #tbl_K2_Used_Adj_Replace_End_11;
                    drop table #tbl_K2_Used_Adj_Replace_Total_12;
                    drop table #tbl_K2_Used_Adj_Replace_Total_13;
                    drop table #tbl_K2_Used_Adj_Replace_Dtl_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18;
                    drop table #tbl_K2_Used_Adj_Replace_Total_18_19;
                    drop table #tbl_K2;
                    drop table #tbl_K3_EndPeriod_Start_20;
                    drop table #tbl_K3_EndPeriod_End_21;
                    drop table #tbl_K3;
                    drop table #tbl_Return;
                    drop table #tbl_Invoice_TempInvoice_AfterEndReport;
                    drop table #tbl_Invoice_TempInvoice_InvoiceMax;
                    drop table #tbl_K3_EndPeriod_Start_Max;		
                    drop table #tbl_K3_EndPeriod_Remain;				
					"
                , "@strReportDTimeFrom", strReportDTimeFrom
                , "@strReportDTimeTo", strReportDTimeTo
                , "@strReportDateFrom", strReportDateFrom
                , "@strReportDateTo", strReportDateTo
                , "@strInvoiceType", strInvoiceType
                , "@strSign", strSign
                , "@strFormNo", strFormNo
                , "@strMST", @strMST
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvoiceInvoice_ResultUsed";
            #endregion
        }

        private void Rpt_InvoiceForDashboardX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvoiceForDashboardX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeFrom", strReportDTimeFrom
                , "strReportDTimeTo", strReportDTimeTo
                });
            #endregion

            #region // Check:
            //// Refine:
            ////
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd";
            dtfi.DateSeparator = "-";

            ///// DTime:
            strReportDTimeFrom = TUtils.CUtils.StdDTimeBeginDay(strReportDTimeFrom);
            strReportDTimeTo = TUtils.CUtils.StdDTimeEndDay(strReportDTimeTo);

            //// Date:
            string strReportDateFrom = TUtils.CUtils.StdDate(strReportDTimeFrom);
            string strReportDateTo = TUtils.CUtils.StdDate(strReportDTimeTo);

            /////
            DateTime dtimeReport = Convert.ToDateTime(strReportDTimeFrom, dtfi);

            //// Month:
            string strReportMonthFrom = dtimeReport.ToString("yyyy-MM-01");
            string strReportMonthNext = dtimeReport.AddMonths(1).ToString("yyyy-MM-01");
            DateTime dtimeReport_MonthNext = Convert.ToDateTime(strReportMonthNext, dtfi);
            string strReportMonthTo = dtimeReport_MonthNext.AddDays(-1).ToString("yyyy-MM-dd");

            ///// Year:
            string strReportYearFrom = dtimeReport.ToString("yyyy-01-01");
            string strReportYearTo = dtimeReport.ToString("yyyy-12-31");

            ////
            string strMonth = dtimeReport.Month.ToString();

            //// Quarter.
            string strReportQuarterFrom = "";
            string strReportQuarterTo = "";

            if (CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "1")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "2")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "3"))
            {
                strReportQuarterFrom = dtimeReport.ToString("yyyy-01-01");
                string strReportMonthQEnd = dtimeReport.ToString("yyyy-03-01");
                DateTime dtimeReport_MonthQEnd = Convert.ToDateTime(strReportMonthQEnd, dtfi);
                strReportQuarterTo = dtimeReport_MonthQEnd.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                ////
            }
            else if(CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "4")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "5")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "6"))
            {
                strReportQuarterFrom = dtimeReport.ToString("yyyy-04-01");
                string strReportMonthQEnd = dtimeReport.ToString("yyyy-06-01");
                DateTime dtimeReport_MonthQEnd = Convert.ToDateTime(strReportMonthQEnd, dtfi);
                strReportQuarterTo = dtimeReport_MonthQEnd.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if(CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "7")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "8")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "9"))
            {
                strReportQuarterFrom = dtimeReport.ToString("yyyy-07-01");
                string strReportMonthQEnd = dtimeReport.ToString("yyyy-09-01");
                DateTime dtimeReport_MonthQEnd = Convert.ToDateTime(strReportMonthQEnd, dtfi);
                strReportQuarterTo = dtimeReport_MonthQEnd.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "10")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "11")
                || CmUtils.StringUtils.StringEqualIgnoreCase(strMonth, "12"))
            {
                strReportQuarterFrom = dtimeReport.ToString("yyyy-10-01");
                string strReportMonthQEnd = dtimeReport.ToString("yyyy-12-01");
                DateTime dtimeReport_MonthQEnd = Convert.ToDateTime(strReportMonthQEnd, dtfi);
                strReportQuarterTo = dtimeReport_MonthQEnd.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"					
					--Dashboard
                    --(Mode: Report)	
                    --- Hóa đơn trong ngày:
                    --
                    select distinct
	                    t.MST
                    into #tbl_Mst_NNT
                    from Mst_NNT t --//[mylock]
	                    inner join #tbl_Mst_NNT_ViewAbility f --//[mylock]
		                    on t.MST = f.MST
                        inner join Sys_User su --//[mylock]
                            on t.MST = su.MST
                    where(1=1)
                        and su.UserCode = '@strUserCode'
                    ;

                    --select null #tbl_Mst_NNT, t.* from #tbl_Mst_NNT t --//[mylock];
                    --drop table #tbl_Mst_NNT;

                    ---- #tbl_Invoice_Invoice_InDayCreateDate:
                    select distinct
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_InDayCreateDate
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.CreateDTimeUTC >= '@strReportDTimeFrom'
	                    and t.CreateDTimeUTC <= '@strReportDTimeTo'
                    ;

                    --select null #tbl_Invoice_Invoice_InDayCreateDate, t.* from #tbl_Invoice_Invoice_InDayCreateDate t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_InDayCreateDate;


                    ----- #tbl_Invoice_Invoice_Pending:
                    select distinct
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_Pending
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.InvoiceStatus in ('PENDING')
                    ;

                    --select null tbl_Invoice_Invoice_Pending, t.* from #tbl_Invoice_Invoice_Pending t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_Pending;


                    ----- #tbl_Invoice_Invoice_InDayInvoiceDate:
                    select distinct
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_InDayInvoiceDate
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.InvoiceDateUTC >= '@strReportDateFrom'
	                    and t.InvoiceDateUTC <= '@strReportDateTo'
						and t.InvoiceStatus in ('ISSUED')
                    ;

                    --select null #tbl_Invoice_Invoice_InDayInvoiceDate, t.* from #tbl_Invoice_Invoice_InDayInvoiceDate t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_InDayInvoiceDate;


                    ----- #tbl_Invoice_Invoice_InMonthInvoiceDate:
                    select distinct
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_InMonthInvoiceDate
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.InvoiceDateUTC >= '@strReportMonthFrom'
	                    and t.InvoiceDateUTC <= '@strReportMonthTo'
						and t.InvoiceStatus in ('ISSUED')
                    ;

                    --select null tbl_Invoice_Invoice_InMonthInvoiceDate, t.* from #tbl_Invoice_Invoice_InMonthInvoiceDate t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_InMonthInvoiceDate;


                    ----- #tbl_Invoice_Invoice_InQuarterInvoiceDate:
                    select 
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_InQuarterInvoiceDate
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.InvoiceDateUTC >= '@strReportQuarterFrom'
	                    and t.InvoiceDateUTC <= '@strReportQuarterTo'
						and t.InvoiceStatus in ('ISSUED')
                    ;

                    --select null tbl_Invoice_Invoice_InQuarterInvoiceDate, t.* from #tbl_Invoice_Invoice_InQuarterInvoiceDate t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_InQuarterInvoiceDate;

                    ----- #tbl_Invoice_Invoice_InYearInvoiceDate:
                    select distinct
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_InYearInvoiceDate
                    from Invoice_Invoice t --//[mylock]
	                    inner join #tbl_Mst_NNT f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
	                    and t.InvoiceDateUTC >= '@strReportYearFrom'
	                    and t.InvoiceDateUTC <= '@strReportYearTo'
						and t.InvoiceStatus in ('ISSUED')
                    ;

                    --select null tbl_Invoice_Invoice_InYearInvoiceDate, t.* from #tbl_Invoice_Invoice_InYearInvoiceDate t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_InYearInvoiceDate;

                    --- #tbl_Invoice_Invoice_Filter:
                    select 
	                    t.InvoiceCode 
                    into #tbl_Invoice_Invoice_Filter
                    from #tbl_Invoice_Invoice_InDayCreateDate t --//[mylock]
                    union 
                    select 
	                    t.InvoiceCode 
                    from #tbl_Invoice_Invoice_Pending t --//[mylock]
                    union 
                    select 
	                    t.InvoiceCode 
                    from #tbl_Invoice_Invoice_InDayInvoiceDate t --//[mylock]
                    union 
                    select 
	                    t.InvoiceCode 
                    from #tbl_Invoice_Invoice_InMonthInvoiceDate t --//[mylock]
                    union 
                    select 
	                    t.InvoiceCode 
                    from #tbl_Invoice_Invoice_InQuarterInvoiceDate t --//[mylock]
                    union 
                    select 
	                    t.InvoiceCode 
                    from #tbl_Invoice_Invoice_InMonthInvoiceDate t --//[mylock]
                    ;
                    --select null tbl_Invoice_Invoice_Filter, t.* from #tbl_Invoice_Invoice_Filter t --//[mylock];
                    --drop table ##tbl_Invoice_Invoice_Filter;

                    --- #tbl_Invoice_Invoice_TotalAmontAfterVAT:
                    select 
	                    t.InvoiceCode
	                    , Sum(f.UnitPrice * f.Qty + f.UnitPrice * f.Qty * f.VATRate/100) TotalAmontAfterVAT
                    into #tbl_Invoice_Invoice_TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_Filter t --//[mylock]
	                    inner join Invoice_InvoiceDtl f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
                    where(1=1)
                    group by
	                    t.InvoiceCode
                    ;

                    --select null tbl_Invoice_Invoice_TotalAmontAfterVAT, t.* from #tbl_Invoice_Invoice_TotalAmontAfterVAT t --//[mylock];
                    --drop table #tbl_Invoice_Invoice_TotalAmontAfterVAT;

                    select 
	                    'InvoiceCreateDate' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    into #tbl_Summary
                    from #tbl_Invoice_Invoice_InDayCreateDate t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    union all
                    select 
	                    'InvoicePending' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_Pending t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    union all
                    select 
	                    'InvoiceInvoiceDate' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_InDayInvoiceDate t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    union all
                    select 
	                    'InvoiceInvoiceMonth' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_InMonthInvoiceDate t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    union all
                    select 
	                    'InvoiceInvoiceQuarter' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_InQuarterInvoiceDate t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    union all
                    select 
	                    'InvoiceInvoiceYear' ReportType
	                    , ii.MST
	                    , count(t.InvoiceCode) Qty
	                    , Sum(f.TotalAmontAfterVAT) TotalAmontAfterVAT
                    from #tbl_Invoice_Invoice_InYearInvoiceDate t --//[mylock]
	                    left join #tbl_Invoice_Invoice_TotalAmontAfterVAT f --//[mylock]
		                    on t.InvoiceCode = f.InvoiceCode
	                    left join Invoice_Invoice ii --//[mylock]
		                    on t.InvoiceCode = ii.InvoiceCode
                    where(1=1)
                    group by
	                    ii.MST
                    ;

                    --select null tbl_Summary, t.* from #tbl_Summary t --//[mylock];
                    --drop table #tbl_Summary;

                    --- Return:
                    select 
	                    t.MST
	                    , t.ReportType
	                    , t.TotalAmontAfterVAT
	                    , t.Qty TotalQtyInvoice
	                    , f.TotalQty
	                    , f.TotalQtyIssued
	                    , f.TotalQtyUsed
	                    , (f.TotalQtyIssued - f.TotalQtyUsed) QtyRemain
                    from #tbl_Summary t --//[mylock]
	                    inner join Invoice_license f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
                    ;
                    ----
                    select 
	                    t.MST
	                    , f.NetworkId
	                    , IsNull(f.TotalQty, 0.0) TotalQty
	                    , IsNull(f.TotalQtyIssued, 0.0) TotalQtyIssued
	                    , IsNull(f.TotalQtyUsed, 0.0)  TotalQtyUsed
	                    ,  IsNull(f.TotalQtyIssued - f.TotalQtyUsed, 0.0) QtyRemain
                    from #tbl_Mst_NNT t --//[mylock]
	                    left join Invoice_license f --//[mylock]
		                    on t.MST = f.MST
                    where(1=1)
                    ;

                    --- Clear For Debug:
                    drop table #tbl_Invoice_Invoice_InDayCreateDate;
                    drop table #tbl_Invoice_Invoice_Pending;
                    drop table #tbl_Invoice_Invoice_InDayInvoiceDate;
                    drop table #tbl_Invoice_Invoice_InMonthInvoiceDate;
                    drop table #tbl_Invoice_Invoice_InQuarterInvoiceDate;
                    drop table #tbl_Invoice_Invoice_InYearInvoiceDate;
                    drop table #tbl_Mst_NNT;
                    drop table #tbl_Invoice_Invoice_Filter;
                    drop table #tbl_Invoice_Invoice_TotalAmontAfterVAT;
                    drop table #tbl_Summary;			
					"
                , "@strReportDTimeFrom", strReportDTimeFrom
                , "@strReportDTimeTo", strReportDTimeTo
                ////
                , "@strReportDateFrom", strReportDateFrom
                , "@strReportDateTo", strReportDateTo
                ////
                , "@strReportMonthFrom", strReportMonthFrom
                , "@strReportMonthTo", strReportMonthTo
                /////
                , "@strReportQuarterFrom", strReportQuarterFrom
                , "@strReportQuarterTo", strReportQuarterTo
                /////
                , "@strReportYearFrom", strReportYearFrom
                , "@strReportYearTo", strReportYearTo
                ////
                , "@strUserCode", strWAUserCode
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvoiceForDashboard";
            dsGetData.Tables[nIdxTable++].TableName = "Invoice_license";
            #endregion
        }

        public DataSet Rpt_InvoiceInvoice_ResultUsed(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            , string strInvoiceType
            , string strSign
            , string strFormNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvoiceInvoice_ResultUsed";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvoiceInvoice_ResultUsed;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeFrom", strReportDTimeFrom
                    , "strReportDTimeTo", strReportDTimeTo
                    , "strInvoiceType", strInvoiceType
                    , "strSign", strSign
                    , "strFormNo", strFormNo
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

                #region // Rpt_InvoiceInvoice_ResultUsedX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvoiceInvoice_ResultUsedX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeFrom // strReportDTimeFrom
                        , strReportDTimeTo // strReportDTimeTo
                        , strInvoiceType // strInvoiceType
                        , strSign // strSign
                        , strFormNo // strFormNo
                                    ////
                        , out dsGetData // dsGetData
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


        public DataSet Rpt_InvoiceInvoice_ResultUsed_New20191007(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            , string strInvoiceType
            , string strSign
            , string strFormNo
            , string strMST
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvoiceInvoice_ResultUsed";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvoiceInvoice_ResultUsed;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeFrom", strReportDTimeFrom
                    , "strReportDTimeTo", strReportDTimeTo
                    , "strInvoiceType", strInvoiceType
                    , "strSign", strSign
                    , "strFormNo", strFormNo
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

                #region // Rpt_InvoiceInvoice_ResultUsedX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvoiceInvoice_ResultUsedX_New20191106(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeFrom // strReportDTimeFrom
                        , strReportDTimeTo // strReportDTimeTo
                        , strInvoiceType // strInvoiceType
                        , strSign // strSign
                        , strFormNo // strFormNo
                        , strMST // strMST
                                 ////
                        , out dsGetData // dsGetData
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

        public DataSet Rpt_InvoiceForDashboard(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strReportDTimeFrom
            , string strReportDTimeTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvoiceForDashboard";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvoiceForDashboard;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeFrom", strReportDTimeFrom
                    , "strReportDTimeTo", strReportDTimeTo
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

                #region // Rpt_InvoiceInvoice_ResultUsedX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvoiceForDashboardX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeFrom // strReportDTimeFrom
                        , strReportDTimeTo // strReportDTimeTo
                                    ////
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_InvoiceForDashboard(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvoiceForDashboard objRQ_Rpt_InvoiceForDashboard
            ////
            , out RT_Rpt_InvoiceForDashboard objRT_Rpt_InvoiceForDashboard
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvoiceForDashboard.Tid;
            objRT_Rpt_InvoiceForDashboard = new RT_Rpt_InvoiceForDashboard();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvoiceForDashboard";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvoiceForDashboard;
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
                List<Rpt_InvoiceForDashboard> Lst_Rpt_InvoiceForDashboard = new List<Rpt_InvoiceForDashboard>();
                List<Invoice_license> Lst_Invoice_license = new List<Invoice_license>();
                #endregion

                #region // Rpt_InvoiceForDashboard:
                mdsResult = Rpt_InvoiceForDashboard(
                    objRQ_Rpt_InvoiceForDashboard.Tid // strTid
                    , objRQ_Rpt_InvoiceForDashboard.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvoiceForDashboard.GwPassword // strGwPassword
                    , objRQ_Rpt_InvoiceForDashboard.WAUserCode // strUserCode
                    , objRQ_Rpt_InvoiceForDashboard.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvoiceForDashboard.ReportDTimeFrom // objReportDTimeFrom
                    , objRQ_Rpt_InvoiceForDashboard.ReportDTimeTo // objReportDTimeTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvoiceForDashboard = mdsResult.Tables["Rpt_InvoiceForDashboard"].Copy();
                    Lst_Rpt_InvoiceForDashboard = TUtils.DataTableCmUtils.ToListof<Rpt_InvoiceForDashboard>(dt_Rpt_InvoiceForDashboard);
                    objRT_Rpt_InvoiceForDashboard.Lst_Rpt_InvoiceForDashboard = Lst_Rpt_InvoiceForDashboard;
                    /////
                    DataTable dt_Invoice_license = mdsResult.Tables["Invoice_license"].Copy();
                    Lst_Invoice_license = TUtils.DataTableCmUtils.ToListof<Invoice_license>(dt_Invoice_license);
                    objRT_Rpt_InvoiceForDashboard.Lst_Invoice_license = Lst_Invoice_license;
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

        public DataSet WAS_Rpt_InvoiceInvoice_ResultUsed(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvoiceInvoice_ResultUsed objRQ_Rpt_InvoiceInvoice_ResultUsed
            ////
            , out RT_Rpt_InvoiceInvoice_ResultUsed objRT_Rpt_InvoiceInvoice_ResultUsed
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvoiceInvoice_ResultUsed.Tid;
            objRT_Rpt_InvoiceInvoice_ResultUsed = new RT_Rpt_InvoiceInvoice_ResultUsed();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RT_Rpt_InvoiceInvoice_ResultUsed";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvoiceInvoice_ResultUsed;
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
                List<Rpt_InvoiceInvoice_ResultUsed> Lst_Rpt_InvoiceInvoice_ResultUsed = new List<Rpt_InvoiceInvoice_ResultUsed>();
                #endregion

                #region // Rpt_InvoiceInvoice_ResultUsed:
                mdsResult = Rpt_InvoiceInvoice_ResultUsed(
                    objRQ_Rpt_InvoiceInvoice_ResultUsed.Tid // strTid
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwPassword // strGwPassword
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.WAUserCode // strUserCode
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.ReportDTimeFrom // objReportDTimeFrom
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.ReportDTimeTo // objReportDTimeTo
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.InvoiceType // objInvoiceType
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.strSign // objstrSign
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.FormNo // FormNo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvoiceInvoice_ResultUsed = mdsResult.Tables["Rpt_InvoiceInvoice_ResultUsed"].Copy();
                    Lst_Rpt_InvoiceInvoice_ResultUsed = TUtils.DataTableCmUtils.ToListof<Rpt_InvoiceInvoice_ResultUsed>(dt_Rpt_InvoiceInvoice_ResultUsed);
                    objRT_Rpt_InvoiceInvoice_ResultUsed.Lst_Rpt_InvoiceInvoice_ResultUsed = Lst_Rpt_InvoiceInvoice_ResultUsed;
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


        public DataSet WAS_Rpt_InvoiceInvoice_ResultUsed_New20191007(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvoiceInvoice_ResultUsed objRQ_Rpt_InvoiceInvoice_ResultUsed
            ////
            , out RT_Rpt_InvoiceInvoice_ResultUsed objRT_Rpt_InvoiceInvoice_ResultUsed
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvoiceInvoice_ResultUsed.Tid;
            objRT_Rpt_InvoiceInvoice_ResultUsed = new RT_Rpt_InvoiceInvoice_ResultUsed();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RT_Rpt_InvoiceInvoice_ResultUsed";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvoiceInvoice_ResultUsed;
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
                List<Rpt_InvoiceInvoice_ResultUsed> Lst_Rpt_InvoiceInvoice_ResultUsed = new List<Rpt_InvoiceInvoice_ResultUsed>();
                #endregion

                #region // Rpt_InvoiceInvoice_ResultUsed:
                mdsResult = Rpt_InvoiceInvoice_ResultUsed_New20191007(
                    objRQ_Rpt_InvoiceInvoice_ResultUsed.Tid // strTid
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwPassword // strGwPassword
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.WAUserCode // strUserCode
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.ReportDTimeFrom // objReportDTimeFrom
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.ReportDTimeTo // objReportDTimeTo
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.InvoiceType // objInvoiceType
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.strSign // objstrSign
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.FormNo // FormNo
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.MST // MST
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvoiceInvoice_ResultUsed = mdsResult.Tables["Rpt_InvoiceInvoice_ResultUsed"].Copy();
                    Lst_Rpt_InvoiceInvoice_ResultUsed = TUtils.DataTableCmUtils.ToListof<Rpt_InvoiceInvoice_ResultUsed>(dt_Rpt_InvoiceInvoice_ResultUsed);
                    objRT_Rpt_InvoiceInvoice_ResultUsed.Lst_Rpt_InvoiceInvoice_ResultUsed = Lst_Rpt_InvoiceInvoice_ResultUsed;
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

        public DataSet WAS_Rpt_InvF_WarehouseCard(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvF_WarehouseCard objRQ_Rpt_InvF_WarehouseCard
            ////
            , out RT_Rpt_InvF_WarehouseCard objRT_Rpt_InvF_WarehouseCard
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvF_WarehouseCard.Tid;
            objRT_Rpt_InvF_WarehouseCard = new RT_Rpt_InvF_WarehouseCard();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RT_Rpt_InvF_WarehouseCard";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvF_WarehouseCard;
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
                List<Rpt_InvF_WarehouseCard> Lst_Rpt_InvF_WarehouseCard = new List<Rpt_InvF_WarehouseCard>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_InvF_WarehouseCard.Lst_Mst_Inventory == null)
                        objRQ_Rpt_InvF_WarehouseCard.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_InvF_WarehouseCard.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }

                }
                #endregion

                #region // Rpt_InvF_WarehouseCard:
                mdsResult = Rpt_InvF_WarehouseCard(
                    objRQ_Rpt_InvF_WarehouseCard.Tid // strTid
                    , objRQ_Rpt_InvF_WarehouseCard.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvF_WarehouseCard.GwPassword // strGwPassword
                    , objRQ_Rpt_InvF_WarehouseCard.WAUserCode // strUserCode
                    , objRQ_Rpt_InvF_WarehouseCard.WAUserPassword // strUserPassword
					, objRQ_Rpt_InvF_WarehouseCard.AccessToken // strAccessToken
					, objRQ_Rpt_InvF_WarehouseCard.NetworkID // strNetworkID
					, objRQ_Rpt_InvF_WarehouseCard.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvF_WarehouseCard.ProductCode // objProductCode
                    , objRQ_Rpt_InvF_WarehouseCard.ApprDTimeUTCFrom // objApprDTimeUTCFrom
                    , objRQ_Rpt_InvF_WarehouseCard.ApprDTimeUTCTo // objApprDTimeUTCTo
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvF_WarehouseCard = mdsResult.Tables["Rpt_InvF_WarehouseCard"].Copy();
                    Lst_Rpt_InvF_WarehouseCard = TUtils.DataTableCmUtils.ToListof<Rpt_InvF_WarehouseCard>(dt_Rpt_InvF_WarehouseCard);
                    objRT_Rpt_InvF_WarehouseCard.Lst_Rpt_InvF_WarehouseCard = Lst_Rpt_InvF_WarehouseCard;
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

		/// <summary>
		/// 20200709.HTTT.Báo cáo sản lượng xuất kho theo đại lý 
		/// </summary>
		private void Rpt_Summary_In_Out_PivotSpecialX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, string strOrgID
			, string strApprDTimeUTCFrom
			, string strApprDTimeUTCTo
			//, string strInventoryAction
			////
			, DataSet dsData
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Rpt_Summary_In_Out_PivotSpecialX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strOrgID", strOrgID
				, "strApprDTimeUTCFrom", strApprDTimeUTCFrom
				, "strApprDTimeUTCTo", strApprDTimeUTCTo
				//, "strInventoryAction", strInventoryAction
				});
			#endregion

			#region // Convert Input:
			alParamsCoupleError.AddRange(new object[]{
					"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
					});
			#endregion

			#region // Check:
			//// Refine:
			// drAbilityOfUser:
			DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
			strOrgID = TUtils.CUtils.StdParam(strOrgID);
			//strInventoryAction = TUtils.CUtils.StdParam(strInventoryAction);
			strApprDTimeUTCFrom = TUtils.CUtils.StdDTimeBeginDay(strApprDTimeUTCFrom);
			strApprDTimeUTCTo = TUtils.CUtils.StdDTimeEndDay(strApprDTimeUTCTo);

			/////
			#endregion

			#region //// Refine and Check Input Mst_Product:
			////
			DataTable dtInput_Mst_Product = null;
			{
				////
				string strTableCheck = "Mst_Product";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Product = dsData.Tables[strTableCheck];
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Product // dtData
										//, "StdParam", "InvCodeOutActual" // arrstrCouple
					, "StdParam", "ProductCodeUser" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "OrgID", typeof(object));
				for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Product.Rows[nScan];
					drScan["OrgID"] = strOrgID;
				}
			}
			#endregion

			#region //// SaveTemp Mst_Product:
			if (dtInput_Mst_Product.Rows.Count > 0)
			{
				// Upload:
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db // db
					, "#input_Mst_Product" // strTableName
					, new object[] {
							"ProductCodeUser", TConst.BizMix.Default_DBColType,
							"OrgID", TConst.BizMix.Default_DBColType
						} // arrSingleStructure
					, dtInput_Mst_Product // dtData
				);
			}
			else
			{
				string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from Mst_Product t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
					, "@strOrgID", strOrgID
				   );
				////
				_cf.db.ExecQuery(
					strSqlBuild
					);
			}
			#endregion

			#region //// Refine and Check Input Mst_Inventory:
			////
			DataTable dtInput_Mst_Inventory = null;
			{
				////
				string strTableCheck = "Mst_Inventory";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Inventory // dtData
										  //, "StdParam", "InvCodeOutActual" // arrstrCouple
					, "StdParam", "InvCode" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
				for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
					drScan["OrgID"] = strOrgID;
				}
			}
			#endregion

			#region //// SaveTemp Mst_Inventory:
			if (dtInput_Mst_Inventory.Rows.Count > 0)
			{
				// Upload:
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db // db
					, "#input_Mst_Inventory" // strTableName
					, new object[] {
							"InvCode", TConst.BizMix.Default_DBColType,
							"OrgID", TConst.BizMix.Default_DBColType
						} // arrSingleStructure
					, dtInput_Mst_Inventory // dtData
				);
			}
			else
			{
				string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
					, "@strOrgID", strOrgID
				   );
				////
				_cf.db.ExecQuery(
					strSqlBuild
					);
			}
			#endregion

			#region //// Refine and Check Input Mst_ProductGroup:
			////
			DataTable dtInput_Mst_ProductGroup = null;
			string strProductGrpCode = "";
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
						TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_ProductGroup // dtData
											 //, "StdParam", "InvCodeOutActual" // arrstrCouple
					, "StdParam", "ProductGrpCode" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
				for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
					drScan["OrgID"] = strOrgID;
				}
			}
			#endregion

			#region //// SaveTemp Mst_ProductGroup:

			string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
			if (dtInput_Mst_ProductGroup.Rows.Count > 0)
			{
				// Upload:
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db // db
					, "#input_Mst_ProductGroup" // strTableName
					, new object[] {
							"ProductGrpCode", TConst.BizMix.Default_DBColType,
							"OrgID", TConst.BizMix.Default_DBColType
						} // arrSingleStructure
					, dtInput_Mst_ProductGroup // dtData
				);
				/////
				zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
			}
			else
			{
			}
			#endregion

			#region //// Refine and Check Input Mst_Customer:
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
						TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_CustomerNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_Customer = dsData.Tables[strTableCheck];
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_Customer // dtData
										 //, "StdParam", "InvCodeOutActual" // arrstrCouple
					, "StdParam", "CustomerCodeSys" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "OrgID", typeof(object));
				for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
					drScan["OrgID"] = strOrgID;
				}
			}
			#endregion

			#region //// SaveTemp Mst_Customer:
			if (dtInput_Mst_Customer.Rows.Count > 0)
			{
				// Upload:
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db // db
					, "#input_Mst_Customer" // strTableName
					, new object[] {
							"OrgID", TConst.BizMix.Default_DBColType,
							"CustomerCodeSys", TConst.BizMix.Default_DBColType
						} // arrSingleStructure
					, dtInput_Mst_Customer // dtData
				);
			}
			else
			{
				string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Customer:
                        select distinct
	                        t.CustomerCodeSys
	                        , t.OrgID
                        into #input_Mst_Customer
                        from Mst_Customer t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
					, "@strOrgID", strOrgID
				   );
				////
				_cf.db.ExecQuery(
					strSqlBuild
					);
			}
			#endregion

			#region // Build Sql:
			////
			ArrayList alParamsCoupleSql = new ArrayList();
			alParamsCoupleSql.AddRange(new object[] {
				});
			////
			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
			zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
				drAbilityOfUser // drAbilityOfUser
				, ref alParamsCoupleError // alParamsCoupleError
				);

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ----- #input_Mst_Product_Filter:
                        select distinct
	                        t.ProductCode
	                        , t.OrgID
                        into #input_Mst_Product_Filter
                        from Mst_Product t --//[mylock]
	                        inner join #input_Mst_Product f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeUser = f.ProductCodeUser
                        where(1=1)
                        ;

                        

                        ---- Báo cáo tổng hợp nhập :
                        select 
	                        t.DocNo
	                        , t.OrgID
	                        , t.ApprDTimeUTC
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvCodeIn
			                        when t.InventoryAction = 'CUSRETURN' then invficr.InvCodeIn
			                        when t.InventoryAction = 'OUT' then invfo.InvCodeOut
		                        end 
	                        ) InvCode
	                        , t.CustomerCode
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then t.InventoryAction
			                        when t.InventoryAction = 'CUSRETURN' then'IN'
			                        when t.InventoryAction = 'OUT' then t.InventoryAction
		                        end 
	                        )  InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then 'CUSTOMERRETURN'
			                        when t.InventoryAction = 'OUT' then invfo.InvOutType
		                        end 
	                        ) Inv_In_Out_Type
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
		                        end 
	                        ) Inv_In_Out_TypeDesc
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then N'CUSRETURN'
		                        end 
	                        ) InvInType
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
		                        end 
	                        ) InvInTypeName
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutType
									else null
		                        end 
	                        ) InvOutType
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
									else null
		                        end 
	                        ) InvOutTypeName
	                        , t.ProductCodeBase ProductCode 
	                        , t.QtyConvert Qty
							, t.UnitCodeBase UnitCode
                        into #tbl_Rpt_Summary_In_Out
                        from InvF_WarehouseCard t --//[mylock]
	                        inner join #input_Mst_Product_Filter f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeBase = f.ProductCode
	                        inner join Mst_Product mp --//[mylock]
		                        on f.OrgID = mp.OrgID
			                        and f.ProductCode = mp.ProductCode	
	                        --left join #input_Mst_ProductGroup k --//[mylock]
		                    --    on mp.OrgID = k.OrgID
			                --        and mp.ProductGrpCode = k.ProductGrpCode
                            --------------------------------
                            zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                            ---------------------------------
	                        left join InvF_InventoryIn invfi --//[mylock]
		                        on t.DocNo = invfi.IF_InvInNo
	                        left join InvF_InventoryOut invfo --//[mylock]
		                        on t.DocNo = invfo.IF_InvOutNo
	                        left join InvF_InventoryCusReturn invficr --//[mylock]
		                        on t.DocNo = invficr.IF_InvCusReturnNo
	                        left join Mst_InvInType miitp --//[mylock]
		                        on invfi.OrgID = miitp.OrgID
			                        and invfi.InvInType = miitp.InvInType
	                        left join Mst_InvOutType miotp --//[mylock]
		                        on invfo.OrgID = miotp.OrgID
			                        and invfo.InvOutType = miotp.InvOutType
	                        inner join #input_Mst_Customer t_mc --//[mylock]
		                        on t.OrgID = t_mc.OrgID
			                        and t.CustomerCode = t_mc.CustomerCodeSys	
                        where(1=1)
	                        and t.InventoryAction in ('IN', 'OUT', 'CUSRETURN')
	                        and t.ApprDTimeUTC >= '@strApprDTimeUTCFrom'
	                        and t.ApprDTimeUTC <= '@strApprDTimeUTCTo'
                        ;

                        --- Return:
                        select 
	                        t.DocNo
	                        , Left(t.ApprDTimeUTC, 10) ApprDateUTC
	                        , mi.InvCode
	                        , mi.InvName
	                        , mc.CustomerCodeSys
	                        , mc.CustomerCode
	                        , mc.CustomerName
							, mc.AreaCode
							, ma.AreaName
							, mc.ProvinceCode
							, mpv.ProvinceName
	                        , mp.ProductCode
	                        , mp.ProductCodeUser
	                        , mp.ProductName
	                        , t.InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then N'Loại nhập'
			                        when t.InventoryAction = 'OUT' then N'Loại xuất'
		                        end 
	                        )  InventoryActionDesc
	                        , t.Inv_In_Out_Type
	                        , t.Inv_In_Out_TypeDesc
	                        , mpg.ProductGrpCode
	                        , mpg.ProductGrpName
	                        , mpg.ProductGrpDesc
	                        , t.Qty
							, t.UnitCode
							, t.InvInType
							, t.InvInTypeName
							, t.InvOutType
							, t.InvOutTypeName
						into #tbl_Rpt_Summary_In_Out_Check
                        from #tbl_Rpt_Summary_In_Out t --//[mylock]
	                        left join Mst_Inventory mi --//[mylock]
		                        on t.OrgID = mi.OrgID
			                        and t.InvCode = mi.InvCode
	                        left join Mst_Customer mc --//[mylock]
		                        on t.OrgID = mc.OrgID
			                        and t.CustomerCode = mc.CustomerCodeSys
	                        left join Mst_Area ma --//[mylock]
		                        on mc.OrgID = ma.OrgID
			                        and mc.AreaCode = ma.AreaCode
	                        left join Mst_Province mpv --//[mylock]
		                        on mc.ProvinceCode = mpv.ProvinceCode
	                        left join Mst_Product mp --//[mylock]
		                        on t.OrgID =mp.OrgID
			                        and t.ProductCode = mp.ProductCode	
	                        left join Mst_ProductGroup mpg --//[mylock]
		                        on t.OrgID = mpg.OrgID
			                        and mp.ProductGrpCode = mpg.ProductGrpCode
	                        inner join #input_Mst_Inventory k --//[mylock]
		                        on t.OrgID = k.OrgID
			                        and t.InvCode = k.InvCode
                        where(1=1)
							and ('OUT'= '' or t.InventoryAction = 'OUT')
                        ;

						---- #tbl_Mst_Product_UnitCode:
						select top 1
							f.UnitCode
						into #tbl_Mst_Product_UnitCode
						from #input_Mst_Product_Filter t --//[mylock]
							left join Mst_Product f --//[mylock]
								on t.OrgID = f.OrgID
									and t.ProductCode = f.ProductCode
						where (1=1)
						;

						-- select * from #tbl_Mst_Product_UnitCode;

						select
							t.CustomerCodeSys
							, sum(t.Qty) TotalQty
						into #tbl_Rpt_Summary_In_Out_Check_Total
						from #tbl_Rpt_Summary_In_Out_Check t --//[mylock]
						where (1=1)
						group by
							t.CustomerCodeSys
						;
						-- 
						select
							t.*
							, f.CustomerName
							, f.AreaCode
							, ma.AreaName
							, k.UnitCode
						from #tbl_Rpt_Summary_In_Out_Check_Total t --//[mylock]
							left join Mst_Customer f --//[mylock]
								on t.CustomerCodeSys = f.CustomerCodeSys
							left join Mst_Area ma --//[mylock]
								on f.AreaCode = ma.AreaCode
							inner join #tbl_Mst_Product_UnitCode k --//[mylock]
								on (1=1)
						where(1=1)
						;

                        ---- Clear For Debug:
                        drop table #input_Mst_Inventory;
                        drop table #input_Mst_Product;
                        --drop table #input_Mst_ProductGroup;
                        drop table #tbl_Rpt_Summary_In_Out;	
						drop table #tbl_Mst_Product_UnitCode;
						drop table #tbl_Rpt_Summary_In_Out_Check;
						drop table #tbl_Rpt_Summary_In_Out_Check_Total;
					"
					, "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
					, "@strApprDTimeUTCFrom", strApprDTimeUTCFrom
					, "@strApprDTimeUTCTo", strApprDTimeUTCTo
					//, "@strInventoryAction", strInventoryAction
					);
			////
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "Rpt_Summary_In_Out_Sup_PivotSpecial";
			#endregion
		}

        /// <summary>
        /// 20201002.Báo cáo sản lượng xuất kho theo đại lý
        /// NC: Tầm nhìn theo kho của user đăng nhập(Truyền thêm tham số UserCode)
        /// </summary>
        private void Rpt_Summary_In_Out_PivotSpecialX_New20201002(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strOrgID
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            //, string strInventoryAction
            , object objUserCode
            ////
            , DataSet dsData
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Summary_In_Out_PivotSpecialX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strOrgID", strOrgID
                , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                , "strApprDTimeUTCTo", strApprDTimeUTCTo
				//, "strInventoryAction", strInventoryAction
                , "objUserCode", objUserCode
                , 
				});
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Check:
            //// Refine:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Sys_User_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            ////
            //strInventoryAction = TUtils.CUtils.StdParam(strInventoryAction);
            strApprDTimeUTCFrom = TUtils.CUtils.StdDTimeBeginDay(strApprDTimeUTCFrom);
            strApprDTimeUTCTo = TUtils.CUtils.StdDTimeEndDay(strApprDTimeUTCTo);
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            /////
            #endregion

            #region //// Refine and Check Input Mst_Product:
            ////
            DataTable dtInput_Mst_Product = null;
            {
                ////
                string strTableCheck = "Mst_Product";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Product = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Product // dtData
                                        //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeUser" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    );
                ////
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Product.Rows[nScan];
                    //drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Product:
            if (dtInput_Mst_Product.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Product_Fiter" // strTableName
                    , new object[] {
                            "ProductCodeUser", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Product // dtData
                );

                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from #input_Mst_Product_Fiter t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
                        ;
					"
                   //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from Mst_Product t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
                        ;
					"
                    //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    );
                ////
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    //drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory_Fiter" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );

                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from #input_Mst_Inventory_Fiter t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
	                    --    and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
                   //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
	                    --    and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
                    //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
            ////
            DataTable dtInput_Mst_ProductGroup = null;
            string strProductGrpCode = "";
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    );
                ////
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    //drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:

            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            else
            {
            }
            #endregion

            #region //// Refine and Check Input Mst_Customer:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_CustomerNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Customer = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Customer // dtData
                                         //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    );
                ////
               // TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Customer, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Customer.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Customer.Rows[nScan];
                    //drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Customer:
            if (dtInput_Mst_Customer.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Customer_Filter" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType,
                            "CustomerCodeSys", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Customer // dtData
                );

                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Customer:
                        select distinct
	                        t.CustomerCodeSys
	                        , t.OrgID
                        into #input_Mst_Customer
                        from #input_Mst_Customer_Filter t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
	                    --    and t.OrgID = '@strOrgID'
                        ;
					"
                   //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Customer:
                        select distinct
	                        t.CustomerCodeSys
	                        , t.OrgID
                        into #input_Mst_Customer
                        from Mst_Customer t --//[mylock]
                            inner join #tbl_Sys_User_ViewAbility t_SysUser_View --//[mylock]
                                on t.OrgID = t_SysUser_View.OrgID
                        where(1=1)
	                    --    and t.OrgID = '@strOrgID'
                        ;
					"
                    //, "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_GetByUser(
                strUserCode
                , ref alParamsCoupleSql
                , "#input_Mst_Inventory"
                );

            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ----- #input_Mst_Product_Filter:
                        select distinct
	                        t.ProductCode
	                        , t.OrgID
                        into #input_Mst_Product_Filter
                        from Mst_Product t --//[mylock]
	                        inner join #input_Mst_Product f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeUser = f.ProductCodeUser
                        where(1=1)
                        ;

                        

                        ---- Báo cáo tổng hợp nhập :
                        select 
	                        t.DocNo
	                        , t.OrgID
	                        , t.ApprDTimeUTC
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvCodeIn
			                        when t.InventoryAction = 'CUSRETURN' then invficr.InvCodeIn
			                        when t.InventoryAction = 'OUT' then invfo.InvCodeOut
		                        end 
	                        ) InvCode
	                        , t.CustomerCode
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then t.InventoryAction
			                        when t.InventoryAction = 'CUSRETURN' then'IN'
			                        when t.InventoryAction = 'OUT' then t.InventoryAction
		                        end 
	                        )  InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then invfi.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then 'CUSTOMERRETURN'
			                        when t.InventoryAction = 'OUT' then invfo.InvOutType
		                        end 
	                        ) Inv_In_Out_Type
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
		                        end 
	                        ) Inv_In_Out_TypeDesc
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInType
			                        when t.InventoryAction = 'CUSRETURN' then N'CUSRETURN'
		                        end 
	                        ) InvInType
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then miitp.InvInTypeName
			                        when t.InventoryAction = 'CUSRETURN' then N'Nhập trả lại'
		                        end 
	                        ) InvInTypeName
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutType
									else null
		                        end 
	                        ) InvOutType
	                        , (
		                        case
			                        when t.InventoryAction = 'OUT' then miotp.InvOutTypeName
									else null
		                        end 
	                        ) InvOutTypeName
	                        , t.ProductCodeBase ProductCode 
	                        , t.QtyConvert Qty
							, t.UnitCodeBase UnitCode
                        into #tbl_Rpt_Summary_In_Out
                        from InvF_WarehouseCard t --//[mylock]
	                        inner join #input_Mst_Product_Filter f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeBase = f.ProductCode
	                        inner join Mst_Product mp --//[mylock]
		                        on f.OrgID = mp.OrgID
			                        and f.ProductCode = mp.ProductCode	
	                        --left join #input_Mst_ProductGroup k --//[mylock]
		                    --    on mp.OrgID = k.OrgID
			                --        and mp.ProductGrpCode = k.ProductGrpCode
                            --------------------------------
                            zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                            ---------------------------------
	                        left join InvF_InventoryIn invfi --//[mylock]
		                        on t.DocNo = invfi.IF_InvInNo
	                        left join InvF_InventoryOut invfo --//[mylock]
		                        on t.DocNo = invfo.IF_InvOutNo
	                        left join InvF_InventoryCusReturn invficr --//[mylock]
		                        on t.DocNo = invficr.IF_InvCusReturnNo
	                        left join Mst_InvInType miitp --//[mylock]
		                        on invfi.OrgID = miitp.OrgID
			                        and invfi.InvInType = miitp.InvInType
	                        left join Mst_InvOutType miotp --//[mylock]
		                        on invfo.OrgID = miotp.OrgID
			                        and invfo.InvOutType = miotp.InvOutType
	                        inner join #input_Mst_Customer t_mc --//[mylock]
		                        on t.OrgID = t_mc.OrgID
			                        and t.CustomerCode = t_mc.CustomerCodeSys	
                        where(1=1)
	                        and t.InventoryAction in ('IN', 'OUT', 'CUSRETURN')
	                        and t.ApprDTimeUTC >= '@strApprDTimeUTCFrom'
	                        and t.ApprDTimeUTC <= '@strApprDTimeUTCTo'
                        ;

                        --- Return:
                        select 
	                        t.DocNo
	                        , Left(t.ApprDTimeUTC, 10) ApprDateUTC
                            , t.OrgID
	                        , mi.InvCode
	                        , mi.InvName
	                        , mc.CustomerCodeSys
	                        , mc.CustomerCode
	                        , mc.CustomerName
							, mc.AreaCode AreaCodeCus
							, ma.AreaName AreaNameCus
							, iio.InvFCFOutCode02 AreaCode -- 20201002. Khu vực theo phiếu xuất (hiện tại chưa dùng đến nhưng sau sẽ nâng cấp khu vực này)
							, mc.ProvinceCode
							, mpv.ProvinceName
	                        , mp.ProductCode
	                        , mp.ProductCodeUser
	                        , mp.ProductName
	                        , t.InventoryAction
	                        , (
		                        case
			                        when t.InventoryAction = 'IN' then N'Loại nhập'
			                        when t.InventoryAction = 'OUT' then N'Loại xuất'
		                        end 
	                        )  InventoryActionDesc
	                        , t.Inv_In_Out_Type
	                        , t.Inv_In_Out_TypeDesc
	                        , mpg.ProductGrpCode
	                        , mpg.ProductGrpName
	                        , mpg.ProductGrpDesc
	                        , t.Qty
							, t.UnitCode
							, t.InvInType
							, t.InvInTypeName
							, t.InvOutType
							, t.InvOutTypeName
						into #tbl_Rpt_Summary_In_Out_Check_Filter
                        from #tbl_Rpt_Summary_In_Out t --//[mylock]
	                        left join InvF_InventoryOut iio --//[mylock]
		                        on t.DocNo = iio.IF_InvOutNo
	                        left join Mst_Inventory mi --//[mylock]
		                        on t.OrgID = mi.OrgID
			                        and t.InvCode = mi.InvCode
	                        left join Mst_Customer mc --//[mylock]
		                        on t.OrgID = mc.OrgID
			                        and t.CustomerCode = mc.CustomerCodeSys
	                        left join Mst_Area ma --//[mylock]
		                        on mc.OrgID = ma.OrgID
			                        and mc.AreaCode = ma.AreaCode
	                        left join Mst_Province mpv --//[mylock]
		                        on mc.ProvinceCode = mpv.ProvinceCode
	                        left join Mst_Product mp --//[mylock]
		                        on t.OrgID =mp.OrgID
			                        and t.ProductCode = mp.ProductCode	
	                        left join Mst_ProductGroup mpg --//[mylock]
		                        on t.OrgID = mpg.OrgID
			                        and mp.ProductGrpCode = mpg.ProductGrpCode
	                        inner join #input_Mst_Inventory k --//[mylock]
		                        on t.OrgID = k.OrgID
			                        and t.InvCode = k.InvCode
                        where(1=1)
							and ('OUT'= '' or t.InventoryAction = 'OUT')
                        ;
            
                        ---- #tbl_Mst_UserMapInventory_Filter:
						--select
						--	t.*
						--into #tbl_Mst_UserMapInventory_Filter
						--from Mst_UserMapInventory t --//[mylock]
						--where (1=1)
						--	and t.UserCode = '@strUserCode'
						;

						---- #tbl_Rpt_Summary_In_Out_Check:
						select
							t.*
						into #tbl_Rpt_Summary_In_Out_Check
						from #tbl_Rpt_Summary_In_Out_Check_Filter t --//[mylock]
							--left join #tbl_Mst_UserMapInventory_Filter f --//[mylock]
							--	on t.OrgID = f.OrgID
							--		and t.InvCode = f.InvCode
							left join #tbl_Mst_Inventory_ViewAbilityByUser f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
						where(1=1)
							and f.InvCode is not null
						;

						---- #tbl_Mst_Product_UnitCode:
						select top 1
							f.UnitCode
						into #tbl_Mst_Product_UnitCode
						from #tbl_Rpt_Summary_In_Out_Check t --//[mylock]
							left join Mst_Product f --//[mylock]
								on t.OrgID = f.OrgID
									and t.ProductCode = f.ProductCode
						where (1=1)
						;

						-- select * from #tbl_Mst_Product_UnitCode;

						select
							t.CustomerCodeSys
							, sum(t.Qty) TotalQty
						into #tbl_Rpt_Summary_In_Out_Check_Total
						from #tbl_Rpt_Summary_In_Out_Check t --//[mylock]
						where (1=1)
						group by
							t.CustomerCodeSys
						;
						-- 
						select
							t.*
							, f.CustomerName
							, f.AreaCode
							, ma.AreaName
							, k.UnitCode
						from #tbl_Rpt_Summary_In_Out_Check_Total t --//[mylock]
							left join Mst_Customer f --//[mylock]
								on t.CustomerCodeSys = f.CustomerCodeSys
							left join Mst_Area ma --//[mylock]
								on f.AreaCode = ma.AreaCode
							inner join #tbl_Mst_Product_UnitCode k --//[mylock]
								on (1=1)
						where(1=1)
						;

                        select
							t.*
						from #tbl_Rpt_Summary_In_Out_Check t --//[mylock]
						where (1=1)
						;

                        ---- Clear For Debug:
                        drop table #input_Mst_Inventory;
                        drop table #input_Mst_Product;
                        --drop table #input_Mst_ProductGroup;
                        drop table #tbl_Rpt_Summary_In_Out;	
						drop table #tbl_Mst_Product_UnitCode;
						drop table #tbl_Rpt_Summary_In_Out_Check;
						drop table #tbl_Rpt_Summary_In_Out_Check_Total;
					"
                    , "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                    , "@strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "@strApprDTimeUTCTo", strApprDTimeUTCTo
                    //, "@strInventoryAction", strInventoryAction
                    , "@strUserCode", strUserCode
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Summary_In_Out_Sup_PivotSpecial";
            #endregion
        }
        public DataSet Rpt_Summary_In_Out_Sup_PivotSpecial(
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
			, string strApprDTimeUTCFrom
			, string strApprDTimeUTCTo
			//, string strInventoryAction
			////
			, DataSet dsData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Rpt_Summary_In_Out_Sup_PivotSpecial";
			string strErrorCodeDefault = TError.ErridnInventory.Rpt_Summary_In_Out_Sup_PivotSpecial;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
					, "strApprDTimeUTCTo", strApprDTimeUTCTo
					//, "strInventoryAction", strInventoryAction
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

				////
				//// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

				#region // Convert Input:
				alParamsCoupleError.AddRange(new object[]{
					"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
					});
				#endregion

				#region // Rpt_Inv_InventoryBalanceSerialForSearchX:
				DataSet dsGetData = null;
				string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
				{
					Rpt_Summary_In_Out_PivotSpecialX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strOrgID_RQ // strOrgID_RQ
						, strApprDTimeUTCFrom // strApprDTimeUTCFrom
						, strApprDTimeUTCTo // strApprDTimeUTCTo
						//, strInventoryAction // strInventoryAction
						, dsData // dsData
								 ////
						, out dsGetData // dsGetData
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
        public DataSet Rpt_Summary_In_Out_Sup_PivotSpecial_New20201002(
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
            , string strApprDTimeUTCFrom
            , string strApprDTimeUTCTo
            //, string strInventoryAction
            , object objUserCode
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Summary_In_Out_Sup_PivotSpecial";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Summary_In_Out_Sup_PivotSpecial;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strApprDTimeUTCFrom", strApprDTimeUTCFrom
                    , "strApprDTimeUTCTo", strApprDTimeUTCTo
					//, "strInventoryAction", strInventoryAction
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

                ////
                //// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Convert Input:
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Rpt_Inv_InventoryBalanceSerialForSearchX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Summary_In_Out_PivotSpecialX_New20201002(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID_RQ // strOrgID_RQ
                        , strApprDTimeUTCFrom // strApprDTimeUTCFrom
                        , strApprDTimeUTCTo // strApprDTimeUTCTo
                        //, strInventoryAction // strInventoryAction
                        , objUserCode // objUserCode
                        , dsData // dsData
                                 ////
                        , out dsGetData // dsGetData
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
        public DataSet WAS_Rpt_Summary_In_Out_Sup_PivotSpecial(
			ref ArrayList alParamsCoupleError
			, RQ_Rpt_Summary_In_Out_Sup_PivotSpecial objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial
			////
			, out RT_Rpt_Summary_In_Out_Sup_PivotSpecial objRT_Rpt_Summary_In_Out_Sup_PivotSpecial
			)
		{
			#region // Temp:
			string strTid = objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Tid;
			objRT_Rpt_Summary_In_Out_Sup_PivotSpecial = new RT_Rpt_Summary_In_Out_Sup_PivotSpecial();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "WAS_Rpt_Summary_In_Out_Sup_PivotSpecial";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Summary_In_Out_Sup_PivotSpecial;
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
				List<Rpt_Summary_In_Out_Sup_PivotSpecial> Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = new List<Rpt_Summary_In_Out_Sup_PivotSpecial>();
				#endregion

				#region // Refine and Check Input:
				////
				DataSet dsData = new DataSet();
				{
					////
					if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory == null)
						objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory = new List<Mst_Inventory>();
					{
						DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory, "Mst_Inventory");
						dsData.Tables.Add(dt_Mst_Inventory);
					}
					////
					if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product == null)
						objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product = new List<Mst_Product>();
					{
						DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product, "Mst_Product");
						dsData.Tables.Add(dt_Mst_Product);
					}
					////
					if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup == null)
						objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
					{
						DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup, "Mst_ProductGroup");
						dsData.Tables.Add(dt_Mst_ProductGroup);
					}
					////
					if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer == null)
						objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer = new List<Mst_Customer>();
					{
						DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer, "Mst_Customer");
						dsData.Tables.Add(dt_Mst_Customer);
					}

				}
				#endregion

				#region // Rpt_Summary_In_Out_Sup_PivotSpecial:
				mdsResult = Rpt_Summary_In_Out_Sup_PivotSpecial(
					objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Tid // strTid
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwUserCode // strGwUserCode
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwPassword // strGwPassword
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.WAUserCode // strUserCode
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.WAUserPassword // strUserPassword
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.AccessToken // AccessToken
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.NetworkID // NetworkID
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.OrgID // OrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.ApprDTimeUTCFrom // ApprDTimeUTCFrom
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.ApprDTimeUTCTo // ApprDTimeUTCTo
					//, objRQ_Rpt_Summary_In_Out_Sup_Pivot.InventoryAction // InventoryAction
					, dsData // dsData
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_Rpt_Summary_In_Out_Sup_PivotSpecial = mdsResult.Tables["Rpt_Summary_In_Out_Sup_PivotSpecial"].Copy();
					Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = TUtils.DataTableCmUtils.ToListof<Rpt_Summary_In_Out_Sup_PivotSpecial>(dt_Rpt_Summary_In_Out_Sup_PivotSpecial);
					objRT_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = Lst_Rpt_Summary_In_Out_Sup_PivotSpecial;
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
        public DataSet WAS_Rpt_Summary_In_Out_Sup_PivotSpecial_New20201002(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Summary_In_Out_Sup_PivotSpecial objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial
            ////
            , out RT_Rpt_Summary_In_Out_Sup_PivotSpecial objRT_Rpt_Summary_In_Out_Sup_PivotSpecial
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Tid;
            objRT_Rpt_Summary_In_Out_Sup_PivotSpecial = new RT_Rpt_Summary_In_Out_Sup_PivotSpecial();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Summary_In_Out_Sup_PivotSpecial";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Summary_In_Out_Sup_PivotSpecial;
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
                List<Rpt_Summary_In_Out_Sup_PivotSpecial> Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = new List<Rpt_Summary_In_Out_Sup_PivotSpecial>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory == null)
                        objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product == null)
                        objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product = new List<Mst_Product>();
                    {
                        DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Product, "Mst_Product");
                        dsData.Tables.Add(dt_Mst_Product);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }
                    ////
                    if (objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer == null)
                        objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer = new List<Mst_Customer>();
                    {
                        DataTable dt_Mst_Customer = TUtils.DataTableCmUtils.ToDataTable<Mst_Customer>(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Mst_Customer, "Mst_Customer");
                        dsData.Tables.Add(dt_Mst_Customer);
                    }

                }
                #endregion

                #region // Rpt_Summary_In_Out_Sup_PivotSpecial:
                mdsResult = Rpt_Summary_In_Out_Sup_PivotSpecial_New20201002(
                    objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Tid // strTid
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwPassword // strGwPassword
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.WAUserCode // strUserCode
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.AccessToken // AccessToken
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.NetworkID // NetworkID
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.OrgID // OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.ApprDTimeUTCFrom // ApprDTimeUTCFrom
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.ApprDTimeUTCTo // ApprDTimeUTCTo
                    //, objRQ_Rpt_Summary_In_Out_Sup_Pivot.InventoryAction // InventoryAction
                    , objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.UserCode // objUserCode
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Summary_In_Out_Sup_PivotSpecial = mdsResult.Tables["Rpt_Summary_In_Out_Sup_PivotSpecial"].Copy();
                    Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = TUtils.DataTableCmUtils.ToListof<Rpt_Summary_In_Out_Sup_PivotSpecial>(dt_Rpt_Summary_In_Out_Sup_PivotSpecial);
                    objRT_Rpt_Summary_In_Out_Sup_PivotSpecial.Lst_Rpt_Summary_In_Out_Sup_PivotSpecial = Lst_Rpt_Summary_In_Out_Sup_PivotSpecial;
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

        private void zzzzClauseSelect_Mst_Inventory_ViewAbility_GetByUser(
            string strUserCode
            , ref ArrayList alParamsCoupleSql
            , string strTableNameDBTemp
            )
        {

            ////
            string strSqlCheck = CmUtils.StringUtils.Replace(@"
					select
						su.UserCode
						, su.OrgID
						, su.CustomerCodeSys
					from Sys_User su --//[mylock]
					where (1=1)
                        and su.UserCode = '@strUserCode'
					;
				"
                , "@strUserCode", strUserCode
                );
            DataTable dt_Check = _cf.db.ExecQuery(strSqlCheck).Tables[0];
            string strCustomerCodeSys = null;
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
                    if object_id('tempdb..#tbl_Mst_Inventory_ViewAbilityByUser') is not null
                    drop table #tbl_Mst_Inventory_ViewAbilityByUser;

					select distinct
						su.UserCode
						, su.OrgID
						, su.CustomerCodeSys
					into #tbl_Mst_Inventory_ViewAbilityByUser
					from Sys_User su --//[mylock]
					where (0=1)
					;
				");

            // Cases:
            if (dt_Check.Rows.Count < 1)
            // User không nằm trong Network
            {
                strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbilityByUser') is not null
                        drop table #tbl_Mst_Inventory_ViewAbilityByUser;

						select distinct
							imi.OrgID
                            , imi.InvCode
						into #tbl_Mst_Inventory_ViewAbilityByUser
						from #input_Mst_Inventory imi --//[mylock]
						where (1=1)
						;
					");
                _cf.db.ExecQuery(strSql);
            }
            ////
            else
            {

                strCustomerCodeSys = TUtils.CUtils.StdParam(dt_Check.Rows[0]["CustomerCodeSys"]);

                if (string.IsNullOrEmpty(strCustomerCodeSys)
                    || CmUtils.StringUtils.StringEqual(strCustomerCodeSys, TConst.BizMix.CustomerCodeSysRoot))
                // User chi nhanh (User tổng)
                {
                    strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_UserMapInventory_Filter') is not null
                        drop table #tbl_Mst_UserMapInventory_Filter;

						---- #tbl_Mst_UserMapInventory_Filter:
						select distinct
							mnnt.OrgID
							, mnnt.InvCode
							, mi.InvBUCode
							, mi.InvBUPattern
						into #tbl_Mst_UserMapInventory_Filter
						from Mst_UserMapInventory mnnt --//[mylock]
							inner join Mst_Inventory mi --//[mylock]
								on mnnt.OrgID = mi.OrgID
									and mnnt.InvCode = mi.InvCode
						where (1=1)
							and mnnt.UserCode = '@strUserCode'
						;
						
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbilityByUser') is not null
                        drop table #tbl_Mst_Inventory_ViewAbilityByUser;
						select distinct
							t.OrgID
							, t.InvCode
						into #tbl_Mst_Inventory_ViewAbilityByUser
						from Mst_Inventory t --//[mylock]
							inner join #tbl_Mst_UserMapInventory_Filter f --//[mylock]
								on t.OrgID = f.OrgID
						where(1=1)
							and t.InvBUCode like f.InvBUPattern
						;

						--- Clear For Debug:
						drop table #tbl_Mst_UserMapInventory_Filter;
					"
                    , "@strUserCode", strUserCode
                    );
                    ////
                    _cf.db.ExecQuery(strSql);
                }
                else if (!string.IsNullOrEmpty(strCustomerCodeSys)
                    && !CmUtils.StringUtils.StringEqual(strCustomerCodeSys, TConst.BizMix.CustomerCodeSysRoot))
                // User đại lý
                {
                    strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbilityByUser') is not null
                        drop table #tbl_Mst_Inventory_ViewAbilityByUser;

						select distinct
							t.OrgID
							, t.InvCode
						into #tbl_Mst_Inventory_ViewAbilityByUser
						from zzzzClauseTableNameDBTemp t --//[mylock]
						where (1=1)
						;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );
                    ////
                    _cf.db.ExecQuery(strSql);
                }
            }

            // Return Good:
        }

        public DataSet OSLQDMS_Ord_OrderPD_Get(
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
            //, out RT_OS_Ord_OrderPD objRT_OS_Ord_OrderPD
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OSLQDMS_Ord_OrderPD_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OSLQDMS_Ord_OrderPD_Get;
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
                //_cf.db.BeginTransaction();

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
                //    , strOrgID // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Refine and check Input:
                // //
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );

                string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MST"]);
                RT_OS_Ord_OrderPD objRT_OS_Ord_OrderPD = null;
                // //
                #endregion

                #region // Call Func WA_Ord_OrderPD_Get:
                {
                    #region // Refine And Check:
                    string strLQDMSNetWorkUrl = string.Format("{0}", htCacheMstParam[TConst.Mst_Param.OS_LQDMS_API_URL]);// _cf.nvcParams["OS_LQDMS_API_Url"];

                    #endregion

                    #region // WA_Ord_OrderPD_Get:
                    /////
                    RQ_OS_Ord_OrderPD objRQ_OS_Ord_OrderPD = new RQ_OS_Ord_OrderPD()
                    {
                        FlagIsDelete = TConst.Flag.No,
                        Rt_Cols_Ord_OrderPD = "*",
                        Rt_Cols_Ord_OrderPDDtl = "*",
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
                        //Ft_WhereClause = ""
                        Ft_WhereClause = CmUtils.StringUtils.Replace("Ord_OrderPD.OrderStatus = 'APPROVE'")
                    };
                    ////
                    try
                    {
                        string json = TJson.JsonConvert.SerializeObject(objRQ_OS_Ord_OrderPD);
                        objRT_OS_Ord_OrderPD = OSLQDMS_Ord_OrderPDService.Instance.WA_OS_Ord_OrderPD_Get(strLQDMSNetWorkUrl, objRQ_OS_Ord_OrderPD);
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
                    #endregion
                }
                #endregion

                #region // GetData:
                DataTable dt_Ord_OrderPD = new DataTable();
                DataTable dt_Ord_OrderPDDtl = new DataTable();
                dt_Ord_OrderPD = TUtils.DataTableCmUtils.ToDataTable<OS_Ord_OrderPD>(objRT_OS_Ord_OrderPD.Lst_Ord_OrderPD, "Ord_OrderPD");
                dt_Ord_OrderPDDtl = TUtils.DataTableCmUtils.ToDataTable<OS_Ord_OrderPDDtl>(objRT_OS_Ord_OrderPD.Lst_Ord_OrderPDDtl, "Ord_OrderPDDtl");

                ////
                #endregion

                #region //// SaveTemp Ord_OrderPD:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#tbl_Ord_OrderPD"
                    , new object[]{
                        "OrderPDNoSys", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "OrderType", TConst.BizMix.Default_DBColType,
                        "OrderPDNo", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType,
                        "CustomerCode", TConst.BizMix.Default_DBColType,
                        "CustomerName", TConst.BizMix.Default_DBColType,
                        "ApprDTimeUTC", TConst.BizMix.Default_DBColType,
                        "ApprBy", TConst.BizMix.Default_DBColType,
                        "OrderStatus", TConst.BizMix.Default_DBColType
                        }
                    , dt_Ord_OrderPD
                    );
                }
                #endregion

                #region //// SaveTemp Ord_OrderPDDtl:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#tbl_Ord_OrderPDDtl"
                    , new object[]{
                        "OrderPDNoSys", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "CustomerCodeSys", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "NetworkIDKH", TConst.BizMix.Default_DBColType,
                        "OrgIDKH", TConst.BizMix.Default_DBColType,
                        "OrderSRNoSys", TConst.BizMix.Default_DBColType,
                        "PlanQty", "float",
                        "PrintedQty", "float",
                        "PlanRemainQty", "float",
                        "OffSetQty", "float",
                        "OffSetErrQty", "float",
                        "PGInvInQty", "float",
                        "PGInvInRemainQty", "float"
                        }
                    , dt_Ord_OrderPDDtl
                    );
                }
                #endregion

                // Return Good:
                //TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                //TDALUtils.DBUtils.RollbackSafety(_cf.db);

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
                //TDALUtils.DBUtils.RollbackSafety(_cf.db);
                //TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

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

        private void Rpt_Inv_InventoryBalance_ExtendX(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strInvCode
            , string strProductCode
            , string strProductGrpCode
            , string strFlagViewInvCode
            , DataSet dsData
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Inv_InventoryBalance_ExtendX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeUTC", strReportDTimeUTC
                , "strOrgID", strOrgID
                , "strInvCode", strInvCode
                , "strFlagViewInvCode", strFlagViewInvCode
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            //// Refine:
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            strInvCode = TUtils.CUtils.StdParam(strInvCode);
            strProductCode = TUtils.CUtils.StdParam(strProductCode);
            strProductGrpCode = TUtils.CUtils.StdParam(strProductGrpCode);
            strFlagViewInvCode = TUtils.CUtils.StdFlag(strFlagViewInvCode);
            strReportDTimeUTC = TUtils.CUtils.StdDTimeEndDay(strReportDTimeUTC);
            /////
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Inv_InventoryBalance_Extend_Mst_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = @strOrgID
	                        and t.FlagIn_Out = '1'
                            and t.InvCode = @strInvCode
                        ;
					"
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    , "@strOrgID", strOrgID
                    , "@strInvCode", strInvCode
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:

            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            #endregion

            #region // Get #tbl_Ord_OrderPD, #tbl_Ord_OrderPDDtl:
            OSLQDMS_Ord_OrderPD_Get(
                strTid
                , strGwUserCode
                , strGwPassword
                , strWAUserCode
                , strWAUserPassword
                , strAccessToken
                , strNetworkID
                , strOrgID_RQ
                ////
                , ref alParamsCoupleError // alParamsCoupleError
                );
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"	
					---- #tbl_Mst_Inventory_FilerInv:
                    select 
	                    t.*
                    into #tbl_Mst_Inventory_FilerInv
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                        inner join #input_Mst_Inventory mi --//[mylock]
                            on t.OrgID = mi.OrgID
                                and t.InvCode = mi.InvCode
                    where(1=1)
                    ;

                    --select null tbl_Mst_Inventory_FilerInv, * from #tbl_Mst_Inventory_FilerInv where(1=1);

                    --- #tbl_Mst_Inventory_Filter:
                    select distinct 
	                    t.OrgID
	                    , t.InvCode
                    into #tbl_Mst_Inventory_Filter
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
	                    inner join #tbl_Mst_Inventory_FilerInv f --//[mylock]
		                    on (1=1)
                    where(1=1)
	                    and t.InvBUCode like f.InvBUPattern
                    ;

                    --select null tbl_Mst_Inventory_Filter, * from #tbl_Mst_Inventory_Filter where(1=1);

                    ----- #tbl_Inv_InventoryBalance_Filter:
                    select 
	                    t.OrgID
	                    , t.InvCode
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Filter
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Mst_Inventory_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode 
	                    inner join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode 
                        --------------------------------
                        zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                        ---------------------------------
                    where(1=1)
	                    and (t.ProductCode = '@strProductCode' or '@strProductCode' = '')
	                    and (mp.ProductGrpCode = '@strProductGrpCode' or '@strProductGrpCode' = '')
                    ;

                    --select null tbl_Inv_InventoryBalance_Filter, * from #tbl_Inv_InventoryBalance_Filter where(1=1);

                    ---- Lấy giá gần nhất theo từng sản phẩm theo từng Org:
                    select distinct
	                    t.OrgID
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Product
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode
			                    and t.ProductCode = f.ProductCode
                    where(1=1)
                    ;

                    --select null tbl_Inv_InventoryBalance_Product, * from #tbl_Inv_InventoryBalance_Product where(1=1);

                    --- #tbl_InvF_WarehouseCard_AutoID_Max_In:
                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , max(f.AutoId) AutoID_Max_In
                    into #tbl_InvF_WarehouseCard_AutoID_Max_In
                    from #tbl_Inv_InventoryBalance_Product t --//[mylock]
	                    inner join InvF_WarehouseCard f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCodeBase
			                    and f.InventoryAction = 'IN'
                    where(1=1)
	                    and f.ApprDTimeUTC <= '@strReportDTime'
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    --select null tbl_InvF_WarehouseCard_AutoID_Max_In, * from #tbl_InvF_WarehouseCard_AutoID_Max_In where(1=1);

                    --- #tbl_Ord_OrderPD_QtyBackOrder:
                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , sum(f.PGInvInRemainQty) QtyBackOrder
                    into #tbl_Ord_OrderPD_QtyBackOrder
                    from #tbl_Inv_InventoryBalance_Product t --//[mylock]
                        inner join #tbl_Ord_OrderPDDtl f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCode
                        inner join #tbl_Ord_OrderPD g --//[mylock]
		                    on f.OrgID = g.OrgID
			                    and f.OrderPDNoSys = g.OrderPDNoSys
                    where(1=1)
	                    and g.ApprDTimeUTC <= '@strReportDTime'
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    --select null tbl_Ord_OrderPD_QtyBackOrder, * from #tbl_Ord_OrderPD_QtyBackOrder where(1=1);

                    ----- #tbl_Inv_InventoryBalance_ForReport:
                    select 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
	                    , sum(t.QtyChTotalOK) QtyTotalOK
	                    , sum(t.QtyChBlockOK) QtyBlockOK
                    into #tbl_Inv_InventoryBalance_ForReport
                    from Inv_InventoryTransaction t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
		                    on t.OrgID = t_mp.OrgID
			                    and t.ProductCode = t_mp.ProductCode
								and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
	                    and t.CreateDTimeUTC <= '@strReportDTime'
                    group by 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
                    ;

                    --select null tbl_Inv_InventoryBalance_ForReport, * from #tbl_Inv_InventoryBalance_ForReport where(1=1);

                    ---- Return:
                    select 
	                    iib.OrgID
	                    , iib.InvCode
	                    , iib.ProductCode
	                    , mp.ProductCodeUser mp_ProductCodeUser
	                    , mp.ProductName mp_ProductName
	                    , mp.ProductNameEN mp_ProductNameEN
	                    , mp.UnitCode mp_UnitCode
	                    , mp.ValConvert mp_ValConvert
						, mp.FlagLot mp_FlagLot
						, mp.FlagSerial mp_FlagSerial
						, mp.ProductType mp_ProductType
						, mp.ProductCodeRoot mp_ProductCodeRoot
						, mp.ProductCodeBase mp_ProductCodeBase
	                    , iib.QtyTotalOK Qty
	                    , iib.QtyTotalOK QtyTotalOK
	                    , (iib.QtyTotalOK - ABS(iib.QtyBlockOK)) QtyAvailOK
	                    , iib.QtyBlockOK QtyBlockOK
                        , IsNull(g.QtyBackOrder, 0.0) QtyBackOrder
                        , (iib.QtyTotalOK + IsNull(g.QtyBackOrder, 0.0)) QtyStockExt
                        , mp.QtyMinSt QtyMinSt
                        , mp.QtyMaxSt QtyMaxSt
	                    , ifwhc.ValMixBase 
	                    , ifwhc.ValMixBaseDesc
	                    , ifwhc.ValMixBaseAfterDesc
	                    , IsNull(ifwhc.ValMixBase, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBase
	                    , IsNull(ifwhc.ValMixBaseDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseDesc
	                    , IsNull(ifwhc.ValMixBaseAfterDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseAfterDesc
                    into #tbl_Return
                    from #tbl_Inv_InventoryBalance_ForReport iib --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t --//[mylock]
		                    on iib.OrgID = t.OrgID
			                    and iib.InvCode = t.InvCode
			                    and iib.ProductCode = t.ProductCode
	                    left join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode
                        left join #tbl_Ord_OrderPD_QtyBackOrder g --//[mylock]
		                    on t.OrgID = g.OrgID
			                    and t.ProductCode = g.ProductCode
	                    left join #tbl_InvF_WarehouseCard_AutoID_Max_In f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCode
	                    left join InvF_WarehouseCard ifwhc --//[mylock]
		                    on f.AutoID_Max_In = ifwhc.AutoId
                    where(1=1)
                    ;

                    --- Return
                    select 
	                    t.*
                    from #tbl_Return t --//[mylock]
                    where(1=1)
                        and (t.QtyTotalOK != 0 or t.QtyAvailOK != 0 or t.QtyBlockOK !=0  )
                    ;

                    --- Clear For Debug:
                    drop table #tbl_Mst_Inventory_Filter;
                    drop table #tbl_Inv_InventoryBalance_Filter;
                    drop table #tbl_Inv_InventoryBalance_Product;
                    drop table #tbl_InvF_WarehouseCard_AutoID_Max_In;
                    drop table #tbl_Ord_OrderPD_QtyBackOrder;
                    drop table #tbl_Return;

					"
                , "@strInvCode", strInvCode
                , "@strProductCode", strProductCode
                , "@strProductGrpCode", strProductGrpCode
                , "@strReportDTime", strReportDTimeUTC
                , "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance_Extend";
            #endregion
        }
        public DataSet Rpt_Inv_InventoryBalance_Extend(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strInvCode
            , string strProductCode
            , string strProductGrpCode
            , string strFlagViewInvCode
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Inv_InventoryBalance_Extend";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalance_Extend;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeUTC", strReportDTimeUTC
                    , "strOrgID", strOrgID
                    , "strInvCode", strInvCode
                    , "strProductCode", strProductCode
                    , "strProductGrpCode", strProductGrpCode
                    , "strFlagViewInvCode", strFlagViewInvCode
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

                //// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Rpt_Inv_InventoryBalance_ExtendX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Inv_InventoryBalance_ExtendX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken
                        , strNetworkID
                        , strOrgID_RQ
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeUTC
                        , strOrgID // strOrgID
                        , strInvCode // strInvCode
                        , strProductCode // strProductCode
                        , strProductGrpCode // strProductGrpCode
                        , strFlagViewInvCode // strFlagViewInvCode
                                             ////
                        , dsData // dsData
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_Inv_InventoryBalance_Extend(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalance_Extend objRQ_Rpt_Inv_InventoryBalance_Extend
            ////
            , out RT_Rpt_Inv_InventoryBalance_Extend objRT_Rpt_Inv_InventoryBalance_Extend
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalance_Extend.Tid;
            objRT_Rpt_Inv_InventoryBalance_Extend = new RT_Rpt_Inv_InventoryBalance_Extend();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalance_Extend";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalance_Extend;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_InvF_InventoryOut", TJson.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Extend)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_Inv_InventoryBalance_Extend> Lst_Rpt_Inv_InventoryBalance_Extend = new List<Rpt_Inv_InventoryBalance_Extend>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_Inventory == null)
                        objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Inv_InventoryBalance_Extend.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }

                }
                #endregion

                #region // Rpt_Inv_InventoryBalance_Extend:
                mdsResult = Rpt_Inv_InventoryBalance_Extend(
                    objRQ_Rpt_Inv_InventoryBalance_Extend.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.AccessToken // strAccessToken
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.NetworkID // strNetworkID
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.ReportDateUTC // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.OrgID // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.InvCode // objInvCode
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.ProductCode // objProductCode
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.ProductGrpCode // objProductGrpCode
                    , ""
                    , dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalance_Extend = mdsResult.Tables["Rpt_Inv_InventoryBalance_Extend"].Copy();
                    Lst_Rpt_Inv_InventoryBalance_Extend = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalance_Extend>(dt_Rpt_Inv_InventoryBalance_Extend);
                    objRT_Rpt_Inv_InventoryBalance_Extend.Lst_Rpt_Inv_InventoryBalance_Extend = Lst_Rpt_Inv_InventoryBalance_Extend;
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
        
        /// <summary>
        /// 20210521. Báo cáo Bản đồ lệnh giao hàng
        /// </summary>
        public DataSet WAS_Rpt_MapDeliveryOrder_ByInvFIOut(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_MapDeliveryOrder_ByInvFIOut objRQ_Rpt_MapDeliveryOrder_ByInvFIOut
            ////
            , out RT_Rpt_MapDeliveryOrder_ByInvFIOut objRT_Rpt_MapDeliveryOrder_ByInvFIOut
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.Tid;
            objRT_Rpt_MapDeliveryOrder_ByInvFIOut = new RT_Rpt_MapDeliveryOrder_ByInvFIOut();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RT_Rpt_MapDeliveryOrder_ByInvFIOut";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_MapDeliveryOrder_ByInvFIOut;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_InvF_InventoryOut", TJson.JsonConvert.SerializeObject(objRQ_Rpt_MapDeliveryOrder_ByInvFIOut)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_MapDeliveryOrder_ByInvFIOut> Lst_Rpt_MapDeliveryOrder_ByInvFIOut = new List<Rpt_MapDeliveryOrder_ByInvFIOut>();
                #endregion

                #region // Rpt_MapDeliveryOrder_ByInvFIOut:
                mdsResult = Rpt_MapDeliveryOrder_ByInvFIOut(
                    objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.Tid // strTid
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.GwUserCode // strGwUserCode
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.GwPassword // strGwPassword
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.WAUserCode // strUserCode
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.WAUserPassword // strUserPassword
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.AccessToken // strAccessToken
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.NetworkID // strNetworkID
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.DateFrom // objDateFrom
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.DateTo // objDateTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_MapDeliveryOrder_ByInvFIOut = mdsResult.Tables["Rpt_MapDeliveryOrder_ByInvFIOut"].Copy();
                    Lst_Rpt_MapDeliveryOrder_ByInvFIOut = TUtils.DataTableCmUtils.ToListof<Rpt_MapDeliveryOrder_ByInvFIOut>(dt_Rpt_MapDeliveryOrder_ByInvFIOut);
                    objRT_Rpt_MapDeliveryOrder_ByInvFIOut.Lst_Rpt_MapDeliveryOrder_ByInvFIOut = Lst_Rpt_MapDeliveryOrder_ByInvFIOut;
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

        public DataSet Rpt_MapDeliveryOrder_ByInvFIOut(
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
            , string strDateFrom
            , string strDateTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_MapDeliveryOrder_ByInvFIOut";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_MapDeliveryOrder_ByInvFIOut;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strDateFrom", strDateFrom
                    , "strDateTo", strDateTo
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

                //
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

                #region // Rpt_MapDeliveryOrder_ByInvFIOutX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_MapDeliveryOrder_ByInvFIOutX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strDateFrom // strDateFrom
                        , strDateTo // strDateTo
                        , out dsGetData // dsGetData
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

        private void Rpt_MapDeliveryOrder_ByInvFIOutX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strDateFrom
            , string strDateTo
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_MapDeliveryOrder_ByInvFIOutX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strDateFrom", strDateFrom
                , "strDateTo", strDateTo
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd";
            dtfi.DateSeparator = "-";
            //// Refine:
            DateTime dtimeDateFrom = Convert.ToDateTime(strDateFrom, dtfi);
            DateTime dtimeDateTo = Convert.ToDateTime(strDateTo, dtfi);
            /////
            #endregion

            #region //// Refine and Check dtDate_Begin_End:
            //// Refine:
            string strSqlGet = CmUtils.StringUtils.Replace(@"
                select
                    Datediff(Day, @strDateFrom , @strDateTo) DateDiffValue
                where(1=1)
                ;
                ");
            DataTable dtGet = _cf.db.ExecQuery(
                strSqlGet
                , "@strDateFrom", dtimeDateFrom
                , "@strDateTo", dtimeDateTo
                ).Tables[0];
            ////
            int nQtyDate = Convert.ToInt32(dtGet.Rows[0]["DateDiffValue"]);
            ////
            // Check số ngày user chọn không quá 31 ngày: cả clietn & biz check
            if (nQtyDate > 30) 
            {
                alParamsCoupleError.AddRange(new object[]{
                        "Check.strDateFrom", strDateFrom
                        , "Check.strDateTo", strDateTo
                        , "Check.Condition", "DateDiff > 31"
                        });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Rpt_MapDeliveryOrder_ByInvFIOutX_DateDiffIsInvalid
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            ////
            DataTable dtDate_Begin_End = new DataTable("dtDate_Begin_End");
            {
                dtDate_Begin_End.Columns.Add("DateValue", typeof(object));
                //dtDate_Begin_End.Columns.Add("StartDTime", typeof(object));
                //dtDate_Begin_End.Columns.Add("EndDTime", typeof(object));

            }
            for (int i = 0; i <= nQtyDate; i++)
            {
                string strDateValue = TUtils.CUtils.StdDate(Convert.ToString(dtimeDateFrom.AddDays(i)));
                //string strDateNext = dtimeDateFrom.AddDays(1).AddMinutes(-1).ToString("yyyy-MM-dd");
                //string strStartDTime = strDateValue;
                //string strEndDTime = strDateNext;
                ////
                //dtDate_Begin_End.Rows.Add(strDateValue, strStartDTime, strEndDTime);
                dtDate_Begin_End.Rows.Add(strDateValue);
            }
            ////
            #endregion

            #region //// SaveTemp dtDate_Begin_End:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Date_Begin_End"
                    , new object[]{
                        "DateValue", TConst.BizMix.Default_DBColType,
                        //"StartDTime", TConst.BizMix.Default_DBColType,
                        //"EndDTime", TConst.BizMix.Default_DBColType,
                    }
                    , dtDate_Begin_End
                    );
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"	
					select distinct
	                    t.IF_InvOutNo
	                    , t.ProductCode
                    into #tbl_InvF_InventoryOutDtl_Filter_Draft
                    from InvF_InventoryOutDtl t with(nolock)
	                    inner join InvF_InventoryOut f with(nolock)
		                    on t.IF_InvOutNo = f.IF_InvOutNo
                    where(1=1)
	                    and f.CreateDTimeUTC >= '@strDateFrom'
	                    and f.CreateDTimeUTC <= '@strDateTo'
	                    and f.IF_InvOutStatus not in ('CANCEL')
	                    --and t.RefType = 'PRINTORDER'
                    ;

                    ------------ #input_Date_Begin_End : chứa ngày bắt đầu kết thúc của tháng.
					select *
					into #tbl_InvF_InventoryOutDtl_Filter
					from #input_Date_Begin_End Cross join #tbl_InvF_InventoryOutDtl_Filter_Draft ---- n*m dòng.
					where (1=1)
					;

                    select
	                    (
							select
								STUFF((
								SELECT ', ' + t.AreaCode
								FROM Mst_Area t --//[mylock]
									left join Mst_CustomerInArea f --//[mylock]
										on t.AreaCode = f.AreaCode
								WHERE(1=1)
									and f.CustomerCodeSys = iio.CustomerCode
									and f.OrgID = iio.OrgID
								FOR
								XML PATH('')
								), 1, 1, ''
								) AS ListPRMCodeSys
								where(1=1)
                        ) AreaCode
	                    --, mcia.AreaCode AreaCodeUser
	                    , (
							select
								STUFF((
								SELECT ', ' + t.AreaName
								FROM Mst_Area t --//[mylock]
									left join Mst_CustomerInArea f --//[mylock]
										on t.AreaCode = f.AreaCode
								WHERE(1=1)
									and f.CustomerCodeSys = iio.CustomerCode
									and f.OrgID = iio.OrgID
								FOR
								XML PATH('')
								), 1, 1, ''
								) AS ListPRMCodeSys
								where(1=1)
                        ) AreaName
	                    , mc.CustomerCode
	                    , mc.CustomerCodeSys
	                    , mc.OrgID OrgID_CTM
	                    , mc.CustomerName
	                    , t.IF_InvOutNo
	                    , t.ProductCode
	                    , mp.OrgID OrgID_PD
	                    , mp.ProductCodeUser
	                    , mp.ProductName
	                    , mp.UnitCode
	                    , (
							case
								when (t.DateValue = SUBSTRING (iio.CreateDTimeUTC, 1, 10)) then f.Qty
								else 0
							end
						)Qty
	                    , iio.IF_InvOutStatus
	                    , iio.ProfileStatus
	                    , iio.CreateDTimeUTC
	                    , t.DateValue Rtp_Date
					--into  #tbl_Filter
                    from #tbl_InvF_InventoryOutDtl_Filter t --//[mylock]
	                    inner join InvF_InventoryOutDtl f --//[mylock]
		                    on t.IF_InvOutNo = f.IF_InvOutNo
			                    and t.ProductCode = f.ProductCode
	                    inner join InvF_InventoryOut iio --//[mylock]
		                    on t.IF_InvOutNo = iio.IF_InvOutNo
	                    left join Mst_Customer mc --//[mylock]
		                    on iio.OrgID = mc.OrgID
			                    and iio.CustomerCode = mc.CustomerCodeSys
	                    left join Mst_CustomerInArea mcia --//[mylock]
		                    on mc.OrgID = mcia.OrgID
			                    and mc.CustomerCodeSys = mcia.CustomerCodeSys
			                    and mc.AreaCode = mcia.AreaCode
	                    left join Mst_Area ma --//[mylock]
		                    on mcia.AreaCode = ma.AreaCode
	                    left join Mst_Product mp --//[mylock]
		                    on iio.OrgID = mp.OrgID
			                    and f.ProductCode = mp.ProductCode
                    where(1=1)
                    ;

					"
                , "@strDateFrom", strDateFrom
                , "@strDateTo", strDateTo
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_MapDeliveryOrder_ByInvFIOut";
            #endregion
        }

        private void Rpt_Inv_InventoryBalance_ByPeriodX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            /////
            , DataSet dsData
            , out DataSet dsGetData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Rpt_Inv_InventoryBalance_ByPeriodX";
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

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region //// Refine and Check Input:
            DataTable dtInv_InventoryBalance_ByPeriod = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalance";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByPeriodX_Inv_InventoryBalanceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInv_InventoryBalance_ByPeriod = dsData.Tables[strTableCheck];
                ////
                //if (dt_InvF_InventoryIn.Rows.Count < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.TableName", "InvF_InventoryIn"
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByPeriodX_InvF_InventoryInTblInvalid
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInv_InventoryBalance_ByPeriod // dtData
                    , "StdParam", "NetWorkID" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdDTime", "LogLUDTimeUTC" // arrstrCouple
                    );
                ////
                for (int nScan = 0; nScan < dtInv_InventoryBalance_ByPeriod.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInv_InventoryBalance_ByPeriod.Rows[nScan];
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalance:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalance_ByPeriod_Draft" // strTableName
                    , new object[] {
                            "NetWorkID", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType,
                            "ProductCode", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInv_InventoryBalance_ByPeriod // dtData
                );
            }
            #endregion

            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
            alParamsCoupleSql.AddRange(new object[] {
                    "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
            ////
            DataSet dsBuildTemp = new DataSet();
            DataTable dt_Inv_InventoryBalance_ReportDate = null;
            ////
            #region // Build Table Temp:
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_Rpt_Inv_InventoryBalance_ByPeriod:
						select 
	                        Cast(null as varchar(50)) NetworkID
	                        , Cast(null as varchar(50)) OrgID
	                        , Cast(null as varchar(50)) ProductCode
	                        , Cast(null as float) QtyTotalOK
	                        , Cast(null as varchar(30)) ReportDTimeUTC
                        into #tbl_Rpt_Inv_InventoryBalance_ByPeriod
                        where (0=1)
                        ;

                        select * from #tbl_Rpt_Inv_InventoryBalance_ByPeriod;
                    "
                );
                dsBuildTemp = _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            
            #endregion
            ////
            
            for (int nScanSL = 0; nScanSL < dtInv_InventoryBalance_ByPeriod.Rows.Count; nScanSL++)
            {
                ////
                DataRow drScanSL = dtInv_InventoryBalance_ByPeriod.Rows[nScanSL];
                string strNetWorkID = TUtils.CUtils.StdParam(drScanSL["NetWorkID"]);
                string strOrgID = TUtils.CUtils.StdParam(drScanSL["OrgID"]);
                string strProductCode = TUtils.CUtils.StdParam(drScanSL["ProductCode"]);
                string strReportDTimeUTC = TUtils.CUtils.StdDTime(drScanSL["LogLUDTimeUTC"]);

                #region // Inv_InventoryBalance_ReportDate:
                {
                    string strSqlGetData = CmUtils.StringUtils.Replace(@"
								---- #tbl_Inv_InventoryBalance_Filter:
								select
	                                t.*
                                into #tbl_Inv_InventoryBalance_Filter
                                from Inv_InventoryBalance t --//[mylock]
	                                inner join Mst_Product f --//[mylock]
		                                on t.ProductCode = f.ProductCode
			                                and t.OrgID = f.OrgID
                                where(1=1)
	                                and t.NetworkID = @strNetWorkID
	                                and t.OrgID = @strOrgID
	                                and t.ProductCode = @strProductCode
								;

								--select null tbl_Inv_InventoryBalance_Filter, * from #tbl_Inv_InventoryBalance_Filter;
		
                                ---- #tbl_Inv_InventoryBalance_ForReport:
                                select 
	                                f.NetworkID
	                                , f.OrgID
	                                , f.ProductCode
	                                , IsNull(sum(t.QtyChTotalOK), 0.0) QtyTotalOK
                                    , @strReportDTimeUTC ReportDTimeUTC
                                into #tbl_Inv_InventoryBalance_ForReport
                                from Inv_InventoryTransaction t --//[mylock]
	                                inner join #tbl_Inv_InventoryBalance_Filter f --//[mylock]
		                                on t.OrgID = f.OrgID
                                            and t.ProductCode = f.ProductCode
                                            and t.InvCode = f.InvCode
                                where(1=1)
                                    and t.NetworkID = @strNetWorkID
                                    and t.OrgID = @strOrgID
                                    and t.ProductCode = @strProductCode
	                                and t.CreateDTimeUTC <= @strReportDTimeUTC
                                group by 
	                                f.NetworkID
	                                , f.OrgID
	                                , f.ProductCode
                                ;
                                
                                insert into #tbl_Rpt_Inv_InventoryBalance_ByPeriod
                                (
	                                NetworkID,
	                                OrgID,
	                                ProductCode,
	                                QtyTotalOK,
	                                ReportDTimeUTC
                                )
                                select 
	                                t.NetworkID
	                                , t.OrgID
	                                , t.ProductCode
	                                , IsNull(t.QtyTotalOK, 0.0) QtyTotalOK
	                                , t.ReportDTimeUTC
                                from #tbl_Inv_InventoryBalance_ForReport t --//[mylock]
                                where(1=1)
							    ;

                                select null tbl_Rpt_Inv_InventoryBalance_ByPeriod, * from #tbl_Rpt_Inv_InventoryBalance_ByPeriod;
							"
                        );

                    dt_Inv_InventoryBalance_ReportDate = _cf.db.ExecQuery(
                        strSqlGetData
                        , "@strNetWorkID", strNetWorkID
                        , "@strOrgID", strOrgID
                        , "@strProductCode", strProductCode
                        , "@strReportDTimeUTC", strReportDTimeUTC
                        ).Tables[0];
                }
                #endregion
            }
            #endregion

            #region // Build Sql Get Report:
            string strSqlGetRpt = CmUtils.StringUtils.Replace(@"
                        ---- #tbl_Rpt_Inv_InventoryBalance_ByPeriod:
						select
	                        t.NetworkID
	                        , t.OrgID
	                        , t.ProductCode
	                        , IsNull(f.QtyTotalOK, 0.0) QtyTotalOK
	                        , t.LogLUDTimeUTC ReportDTimeUTC
                        from #input_Inv_InventoryBalance_ByPeriod_Draft t --//[mylock]
                            left join #tbl_Rpt_Inv_InventoryBalance_ByPeriod f --//[mylock]
                                on t.OrgID = f.OrgID
                                    and t.NetWorkID = f.NetWorkID
                                    and t.ProductCode = f.ProductCode
                        where(1=1)
						;
                    "
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetRpt
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance_ByPeriod";
            #endregion
        }

        private void Rpt_Inv_InventoryBalance_ByPeriodX_New202107151(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            /////
            , DataSet dsData
            , out DataSet dsGetData
           )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Rpt_Inv_InventoryBalance_ByPeriodX";
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

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region //// Refine and Check Input:
            DataTable dtInv_InventoryBalance_ByPeriod = null;
            {
                ////
                string strTableCheck = "Inv_InventoryBalance";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByPeriodX_Inv_InventoryBalanceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInv_InventoryBalance_ByPeriod = dsData.Tables[strTableCheck];
                ////
                //if (dt_InvF_InventoryIn.Rows.Count < 1)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.TableName", "InvF_InventoryIn"
                //        });
                //    throw CmUtils.CMyException.Raise(
                //        TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByPeriodX_InvF_InventoryInTblInvalid
                //        , null
                //        , alParamsCoupleError.ToArray()
                //        );
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInv_InventoryBalance_ByPeriod // dtData
                    , "StdParam", "NetWorkID" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "", "LogLUDTimeUTC" // arrstrCouple
                    );
                ////
                //for (int nScan = 0; nScan < dtInv_InventoryBalance_ByPeriod.Rows.Count; nScan++)
                //{
                //    ////
                //    DataRow drScan = dtInv_InventoryBalance_ByPeriod.Rows[nScan];
                //    ////
                //}
            }
            #endregion

            #region //// SaveTemp Inv_InventoryBalance:
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Inv_InventoryBalance_ByPeriod" // strTableName
                    , new object[] {
                            //"NetWorkID", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType,
                            "ProductCode", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInv_InventoryBalance_ByPeriod // dtData
                );
            }
            #endregion

            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
            alParamsCoupleSql.AddRange(new object[] {
                    "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
            ////
            DataSet dsBuildTemp = new DataSet();
            //DataTable dt_Inv_InventoryBalance_ReportDate = null;

            #region // Build Table Temp:
            string strSqlBuild = null;
            {
                strSqlBuild = CmUtils.StringUtils.Replace(@"
                        select
                            t.OrgID
                            , t.ProductCode
                            , t.LogLUDTimeUTC DTimeDelivery
                        into #input_Inv_InventoryBalance_ByPeriod_Draft
                        from #input_Inv_InventoryBalance_ByPeriod t --//[mylock]
                        where (1=1)
                        ;

                        -- select t.* from #input_Inv_InventoryBalance_ByPeriod_Draft t where (1=1) --//[mylock];
                        -- drop table #input_Inv_InventoryBalance_ByPeriod_Draft;                        

                        ----InvF_ IN:
                        select 
	                        t.ProductCode
	                        , t.Qty
	                        , t.QtyReturn
	                        , DATEADD (hour, 7, f.ApprDTimeUTC) ApprDTime_IN
	                        , f.NetworkID
	                        , f.OrgID
                        into #tbl_InvF_InventoryIn_Darft
                        from InvF_InventoryInDtl t --//[mylock]
	                        inner join InvF_InventoryIn f --//[mylock]
		                        on t.IF_InvInNo = f.IF_InvInNo
                        where (1=1)
	                        and f.IF_InvInStatus = 'APPROVE'
                        ;

                        -- select t.* from #tbl_InvF_InventoryIn_Darft t where (1=1) --//[mylock];
                        -- drop table #tbl_InvF_InventoryIn_Darft;

                        select
	                        t.ProductCode
	                        , t.OrgID
	                        , t.DTimeDelivery
	                        , (
		                        select
			                        sum (isnull (f.Qty, 0)) - SUM(isnull (f.QtyReturn, 0))
		                        from #tbl_InvF_InventoryIn_Darft f
			                        where (1=1)
				                        and t.ProductCode = f.ProductCode
				                        and t.OrgID = f.OrgID
				                        and f.ApprDTime_IN <= t.DTimeDelivery
	                        ) Qty_InvfIn
                        into #tbl_InvF_InventoryIn_Filter
                        from #input_Inv_InventoryBalance_ByPeriod_Draft t --//[mylock]
                        where(1=1)
                        ;
                        -- select t.* from #tbl_InvF_InventoryIn_Filter t where (1=1) --//[mylock];
                        -- drop table  #tbl_InvF_InventoryIn_Filter;

                        ----InvF_Out:
                        select 
	                        t.ProductCode
	                        , t.Qty
	                        , DATEADD (hour, 7, f.ApprDTimeUTC) as ApprDTime_OUT
	                        , f.NetworkID
	                        , f.OrgID
                        into #tbl_InvF_InventoryOut_Draft 
                        from InvF_InventoryOutDtl t --//[mylock]
	                        inner join InvF_Inventoryout f --//[mylock]
		                        on t.IF_InvOutNo = f.IF_InvOutNo
                        where (1=1)
	                        and f.IF_InvOutStatus = 'APPROVE'
                        ;

                        select
	                        t.ProductCode
	                        , t.OrgID
	                        , t.DTimeDelivery
	                        , (
		                        select
			                        sum (isnull (f.Qty, 0))
		                        from #tbl_InvF_InventoryOut_Draft f
			                        where (1=1)
				                        and t.ProductCode = f.ProductCode
				                        and t.OrgID = f.OrgID
				                        and f.ApprDTime_OUT <= t.DTimeDelivery
	                        ) Qty_InvfOut
                        into #tbl_InvF_InventoryOut_Filter
                        from #input_Inv_InventoryBalance_ByPeriod_Draft t
                        where(1=1)
                        ;

                        -- select t.* from #tbl_InvF_InventoryOut_Filter t where (1=1) and t.ProductCode = '0A30081E00000OU' and t.ApprDTime_IN <= '2021-05-10'
                        -- drop table #tbl_InvF_InventoryOut_Filter

                        select
	                        t.ProductCode
                            , t.OrgID
                            , t.DTimeDelivery ReportDTimeUTC
                            , mp.NetworkID
	                        , isnull (f.Qty_InvfIn, 0) - isnull (g.Qty_InvfOut, 0) QtyTotalOK
                        from #input_Inv_InventoryBalance_ByPeriod_Draft t --//[mylock]
	                        left join #tbl_InvF_InventoryIn_Filter f --//[mylock]
		                        on t.ProductCode = f.ProductCode
			                        and t.OrgID = f.OrgID
			                        and t.DTimeDelivery = f.DTimeDelivery
	                        left join #tbl_InvF_InventoryOut_Filter g --//[mylock]
		                        on t.ProductCode = g.ProductCode
			                        and t.OrgID = g.OrgID
			                        and t.DTimeDelivery = g.DTimeDelivery
                            left join Mst_Product mp --//[mylock]
                                on t.ProductCode = mp.ProductCode
			                        and t.OrgID = mp.OrgID
                        where  (1=1)
                        ;
                    "
                );
            }

            #endregion
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlBuild
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance_ByPeriod";
            #endregion
        }

        public DataSet Rpt_Inv_InventoryBalance_ByPeriod(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Inv_InventoryBalance_ByPeriod";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByPeriod;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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

                #region // Rpt_Inv_InventoryBalance_ByPeriodX:
                DataSet dsGetData = new DataSet();
                {
                    Rpt_Inv_InventoryBalance_ByPeriodX_New202107151(
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
        public DataSet WAS_Rpt_Inv_InventoryBalance_ByPeriod(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalance_ByPeriod objRQ_Rpt_Inv_InventoryBalance_ByPeriod
            ////
            , out RT_Rpt_Inv_InventoryBalance_ByPeriod objRT_Rpt_Inv_InventoryBalance_ByPeriod
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Tid;
            objRT_Rpt_Inv_InventoryBalance_ByPeriod = new RT_Rpt_Inv_InventoryBalance_ByPeriod();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryBalance.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalance_ByPeriod";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalance_ByPeriod;
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
                List<Rpt_Inv_InventoryBalance_ByPeriod> Lst_Rpt_Inv_InventoryBalance_ByPeriod = new List<Rpt_Inv_InventoryBalance_ByPeriod>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Lst_Inv_InventoryBalance == null)
                        objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Lst_Inv_InventoryBalance = new List<Inv_InventoryBalance>();
                    {
                        DataTable dt_Inv_InventoryBalance = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalance>(objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Lst_Inv_InventoryBalance, "Inv_InventoryBalance");
                        dsData.Tables.Add(dt_Inv_InventoryBalance);
                    }
                }
                #endregion

                #region // Rpt_Inv_InventoryBalance_ByPeriod:
                mdsResult = Rpt_Inv_InventoryBalance_ByPeriod(
                    objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalance_ByPeriod = mdsResult.Tables["Rpt_Inv_InventoryBalance_ByPeriod"].Copy();
                    Lst_Rpt_Inv_InventoryBalance_ByPeriod = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalance_ByPeriod>(dt_Rpt_Inv_InventoryBalance_ByPeriod);
                    objRT_Rpt_Inv_InventoryBalance_ByPeriod.Lst_Rpt_Inv_InventoryBalance_ByPeriod = Lst_Rpt_Inv_InventoryBalance_ByPeriod;
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

        /// <summary>
        /// 20210925. Báo cáo cũ đang sai kho và double số liệu
        /// Tham chiếu từ hàm WAS_Rpt_InvBalLot_MaxExpiredDateByInv
        /// </summary>
        public DataSet WAS_Rpt_InvBalLot_MaxExpiredDateByInv_New20210925(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvBalLot_MaxExpiredDateByInv objRQ_Rpt_InvBalLot_MaxExpiredDateByInv
            ////
            , out RT_Rpt_InvBalLot_MaxExpiredDateByInv objRT_Rpt_InvBalLot_MaxExpiredDateByInv
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Tid;
            objRT_Rpt_InvBalLot_MaxExpiredDateByInv = new RT_Rpt_InvBalLot_MaxExpiredDateByInv();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvBalLot_MaxExpiredDateByInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvBalLot_MaxExpiredDateByInv;
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
                List<Rpt_InvBalLot_MaxExpiredDateByInv> Lst_Rpt_InvBalLot_MaxExpiredDateByInv = new List<Rpt_InvBalLot_MaxExpiredDateByInv>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product = new List<Mst_Product>();
                    {
                        DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_Product, "Mst_Product");
                        dsData.Tables.Add(dt_Mst_Product);
                    }
                    ////
                    if (objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }
                    ////

                }
                #endregion

                #region // Rpt_InvBalLot_MaxExpiredDateByInv:
                mdsResult = Rpt_InvBalLot_MaxExpiredDateByInv_New20210925(
                    objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Tid // strTid
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwPassword // strGwPassword
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.WAUserCode // strUserCode
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.WAUserPassword // strUserPassword
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.AccessToken // AccessToken
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.NetworkID // NetworkID
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.OrgID // OrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvBalLot_MaxExpiredDateByInv = mdsResult.Tables["Rpt_InvBalLot_MaxExpiredDateByInv"].Copy();
                    Lst_Rpt_InvBalLot_MaxExpiredDateByInv = TUtils.DataTableCmUtils.ToListof<Rpt_InvBalLot_MaxExpiredDateByInv>(dt_Rpt_InvBalLot_MaxExpiredDateByInv);
                    objRT_Rpt_InvBalLot_MaxExpiredDateByInv.Lst_Rpt_InvBalLot_MaxExpiredDateByInv = Lst_Rpt_InvBalLot_MaxExpiredDateByInv;
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

        public DataSet Rpt_InvBalLot_MaxExpiredDateByInv_New20210925(
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
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvBalLot_MaxExpiredDateByInv";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvBalLot_MaxExpiredDateByInv;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
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
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Rpt_InvBalLot_MaxExpiredDateByInvX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvBalLot_MaxExpiredDateByInvX_New20210925(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID_RQ // strOrgID_RQ
                        , dsData // dsData
                                 ////
                        , out dsGetData // dsGetData
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

        private void Rpt_InvBalLot_MaxExpiredDateByInvX_New20210925(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , string strOrgID // strOrgID
            , DataSet dsData
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvBalLot_MaxExpiredDateByInvX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
            #endregion

            #region // Check:
            string strSysDate = dtimeSys.ToString("yyyy-MM-dd");
            /////
            #endregion

            #region //// Refine and Check Input Mst_Product:
            ////
            DataTable dtInput_Mst_Product = null;
            {
                ////
                string strTableCheck = "Mst_Product";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Product = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Product // dtData
                                        //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductCodeUser" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Product.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Product:
            if (dtInput_Mst_Product.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Product" // strTableName
                    , new object[] {
                            "ProductCodeUser", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Product // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Product:
                        select distinct
	                        t.ProductCodeUser
	                        , t.OrgID
                        into #input_Mst_Product
                        from Mst_Product t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_InvBalLot_MaxExpiredDateByInvX_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_ProductGroup:
                        select distinct
	                        t.ProductGrpCode
	                        , t.OrgID
                        into #input_Mst_ProductGroup
                        from Mst_ProductGroup t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                        ----- #input_Mst_Product_Filter:
                        select distinct
	                        t.ProductCode
	                        , t.OrgID
                        into #input_Mst_Product_Filter
                        from Mst_Product t --//[mylock]
	                        inner join #input_Mst_Product f --//[mylock]
		                        on t.OrgID = f.OrgID
			                        and t.ProductCodeUser = f.ProductCodeUser	
	                        inner join #input_Mst_ProductGroup impg --//[mylock]
		                        on t.OrgID = impg.OrgID
			                        and t.ProductGrpCode = impg.ProductGrpCode
                        where(1=1)
                        ;
						-- select null input_Mst_Product_Filter, * from #input_Mst_Product_Filter;
						-- drop table #input_Mst_Product_Filter;

						---- #tbl_Mst_Inventory_Filter:
						select 
							t.OrgID
							, t.InvCode
							, t.InvBUPattern
							, t.InvBUCode
						into #tbl_Mst_Inventory_Filter
						from Mst_Inventory t --//[mylock]
							inner join #input_Mst_Inventory f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
						where(1=1)
							and t.FlagIn_Out = '1'
						;
						-- select null tbl_Mst_Inventory_Filter, * from #tbl_Mst_Inventory_Filter;
						-- drop table #tbl_Mst_Inventory_Filter;

						--- #tbl_Mst_Inventory_Full:
						select distinct
							t.InvCode
							, t.OrgID
						into #tbl_Mst_Inventory_Full
						from Mst_Inventory t --//[mylock]
							inner join #tbl_Mst_Inventory_Filter f --//[mylock]
								on t.OrgID = f.OrgID
						where(1=1)
							and t.InvBUCode like f.InvBUPattern
						; 
						-- select null tbl_Mst_Inventory_Full, * from #tbl_Mst_Inventory_Full;
						-- drop table #tbl_Mst_Inventory_Full;

						---- #tbl_Mst_Inventory_InvCodeInv:
						select
							t.OrgID
							, t.InvCode
							, (
								select top 1 
									f.InvCode
								from Mst_Inventory f --//[mylock]
									inner join #tbl_Mst_Inventory_Full tblmi --//[mylock]
										on f.OrgID = tblmi.OrgID
											and f.InvCode = tblmi.InvCode
								where(1=1)
									and t.OrgID = f.OrgID
									and mi.InvBUCode like f.InvBUPattern     
									and f.FlagIn_Out = '1'
									and f.InvCodeParent is not null                                                                                                                                 
							) InvCode_Inv
						into #tbl_Mst_Inventory_InvCodeInv
						from #tbl_Mst_Inventory_Full t --//[mylock]
							inner join Mst_Inventory mi --//[mylock]
								on t.OrgID = mi.OrgID
									and t.InvCode = mi.InvCode
							--inner join Inv_InventoryBalanceLot iibl --//[mylock]
							--	on mi.OrgID = iibl.OrgID
							--		and mi.InvCode = iibl.InvCode
	                        --inner join Mst_Product mp --//[mylock]
		                    --    on iibl.OrgID = mp.OrgID
			                --        and iibl.ProductCode = mp.ProductCode	
	                        --inner join #input_Mst_ProductGroup k --//[mylock]
		                    --    on mp.OrgID = k.OrgID
			                --        and mp.ProductGrpCode = k.ProductGrpCode
						where(1=1)
						;
						-- select * from #tbl_Mst_Inventory_InvCodeInv;
						-- drop table #tbl_Mst_Inventory_InvCodeInv;

						---- #tbl_Summary:
						select 
							iibl.OrgID
							, iibl.ProductCode
							, iibl.ProductLotNo
							, t.InvCode_Inv InvCode
							, max(iibl.LastInInvDTimeUTC) LastInInvDTimeUTC
							, sum(iibl.QtyTotalOK) TotalQtyTotalOK
							, sum(iibl.QtyBlockOK) TotalQtyBlockOK
							, sum(iibl.QtyAvailOK) TotalQtyAvailOK
							, max(iibl.ExpiredDate) MaxExpiredDate
						into #tbl_Summary
						from Inv_InventoryBalanceLot iibl --//[mylock]
							inner join #tbl_Mst_Inventory_InvCodeInv t --//[mylock]
								on iibl.OrgID = t.OrgID
									and iibl.InvCode = t.InvCode
							inner join #input_Mst_Product_Filter k --//[mylock]
								on iibl.OrgID = k.OrgID
									and iibl.ProductCode = k.ProductCode
						where(1=1)
						group by 
							iibl.OrgID
							, iibl.ProductCode
							, iibl.ProductLotNo
							, t.InvCode_Inv
						;
						-- select * from #tbl_Summary;

						--- Return:
						select 
							t.OrgID
							, t.ProductCode
							, f.ProductCodeUser
							, f.ProductName
							, f.UnitCode
							, t.ProductLotNo
							, t.InvCode
							, k.ProductGrpCode
							, k.ProductGrpName
							, Left(t.LastInInvDTimeUTC, 10) LastInInvDate
							, t.MaxExpiredDate
							, IsNull(DATEDIFF(day, Left(t.LastInInvDTimeUTC, 10), '@strSysDate'), 0.0) QtyDayInv
							, t.TotalQtyTotalOK
							, t.TotalQtyBlockOK
							, t.TotalQtyAvailOK
						from #tbl_Summary t --//[mylock]
							inner join Mst_Product f --//[mylock]
								on t.OrgID = f.OrgID
									and t.ProductCode = f.ProductCode
	                        inner join Mst_ProductGroup k --//[mylock]
		                        on f.OrgID = k.OrgID
			                        and f.ProductGrpCode = k.ProductGrpCode
						where(1=1)
						;
	                   

                        ---- Clear For Debug:
                        drop table #input_Mst_Inventory;
                        drop table #input_Mst_Product;
                        drop table #input_Mst_ProductGroup;
						drop table #input_Mst_Product_Filter;
						drop table #tbl_Mst_Inventory_Filter;
						drop table #tbl_Mst_Inventory_Full;
						drop table #tbl_Mst_Inventory_InvCodeInv;
						drop table #tbl_Summary;	
					"
                    , "@strSysDate", strSysDate
                    );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvBalLot_MaxExpiredDateByInv";
            #endregion
        }

        /// <summary>
        /// 20220518. Báo cáo mới: Báo cáo giá trị hàng tồn kho
        /// Nâng cấp dựa theo Báo cáo tồn kho
        /// </summary>
        public DataSet WAS_Rpt_Inv_InventoryBalance_ByValue(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalance objRQ_Rpt_Inv_InventoryBalance
            ////
            , out RT_Rpt_Inv_InventoryBalance objRT_Rpt_Inv_InventoryBalance
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalance.Tid;
            objRT_Rpt_Inv_InventoryBalance = new RT_Rpt_Inv_InventoryBalance();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalance_ByValue";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalance_ByValue;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_InvF_InventoryOut", TJson.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_Inv_InventoryBalance> Lst_Rpt_Inv_InventoryBalance = new List<Rpt_Inv_InventoryBalance>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_Inventory == null)
                        objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_Inventory = new List<Mst_Inventory>();
                    {
                        DataTable dt_Mst_Inventory = TUtils.DataTableCmUtils.ToDataTable<Mst_Inventory>(objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_Inventory, "Mst_Inventory");
                        dsData.Tables.Add(dt_Mst_Inventory);
                    }
                    ////
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Inv_InventoryBalance.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }

                }
                #endregion

                #region // Rpt_Inv_InventoryBalance_ByValue:
                mdsResult = Rpt_Inv_InventoryBalance_ByValue(
                    objRQ_Rpt_Inv_InventoryBalance.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalance.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalance.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalance.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Inv_InventoryBalance.AccessToken // strAccessToken
                    , objRQ_Rpt_Inv_InventoryBalance.NetworkID // strNetworkID
                    , objRQ_Rpt_Inv_InventoryBalance.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Inv_InventoryBalance.ReportDateUTC // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance.OrgID // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance.InvCode // objInvCode
                    , objRQ_Rpt_Inv_InventoryBalance.ProductCode // objProductCode
                    , objRQ_Rpt_Inv_InventoryBalance.ProductGrpCode // objProductGrpCode
                    , ""
                    , dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalance = mdsResult.Tables["Rpt_Inv_InventoryBalance"].Copy();
                    Lst_Rpt_Inv_InventoryBalance = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalance>(dt_Rpt_Inv_InventoryBalance);
                    objRT_Rpt_Inv_InventoryBalance.Lst_Rpt_Inv_InventoryBalance = Lst_Rpt_Inv_InventoryBalance;
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

        public DataSet Rpt_Inv_InventoryBalance_ByValue(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strInvCode
            , string strProductCode
            , string strProductGrpCode
            , string strFlagViewInvCode
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Inv_InventoryBalance_ByValue";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalance_ByValue;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeUTC", strReportDTimeUTC
                    , "strOrgID", strOrgID
                    , "strInvCode", strInvCode
                    , "strProductCode", strProductCode
                    , "strProductGrpCode", strProductGrpCode
                    , "strFlagViewInvCode", strFlagViewInvCode
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

                // Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Rpt_Inv_InventoryBalance_ByValueX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Inv_InventoryBalance_ByValueX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeUTC
                        , strOrgID // strOrgID
                        , strInvCode // strInvCode
                        , strProductCode // strProductCode
                        , strProductGrpCode // strProductGrpCode
                        , strFlagViewInvCode // strFlagViewInvCode
                                             ////
                        , dsData // dsData
                        , out dsGetData // dsGetData
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

        private void Rpt_Inv_InventoryBalance_ByValueX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportDTimeUTC
            , string strOrgID
            , string strInvCode
            , string strProductCode
            , string strProductGrpCode
            , string strFlagViewInvCode
            , DataSet dsData
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvF_WarehouseCardX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeUTC", strReportDTimeUTC
                , "strOrgID", strOrgID
                , "strInvCode", strInvCode
                , "strFlagViewInvCode", strFlagViewInvCode
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            //// Refine:
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            strInvCode = TUtils.CUtils.StdParam(strInvCode);
            strProductCode = TUtils.CUtils.StdParam(strProductCode);
            strProductGrpCode = TUtils.CUtils.StdParam(strProductGrpCode);
            strFlagViewInvCode = TUtils.CUtils.StdFlag(strFlagViewInvCode);
            strReportDTimeUTC = TUtils.CUtils.StdDTimeEndDay(strReportDTimeUTC);
            /////
            #endregion

            #region //// Refine and Check Input Mst_Inventory:
            ////
            DataTable dtInput_Mst_Inventory = null;
            {
                ////
                string strTableCheck = "Mst_Inventory";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_InventoryNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Inventory = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Inventory // dtData
                                          //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "InvCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Inventory, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_Inventory.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Inventory.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_Inventory:
            if (dtInput_Mst_Inventory.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_Inventory" // strTableName
                    , new object[] {
                            "InvCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Inventory // dtData
                );
            }
            else
            {
                string strSqlBuild = CmUtils.StringUtils.Replace(@"	
                        ----- #input_Mst_Inventory:
                        select distinct
	                        t.InvCode
	                        , t.OrgID
                        into #input_Mst_Inventory
                        from Mst_Inventory t --//[mylock]
                        where(1=1)
	                        and t.OrgID = '@strOrgID'
	                        and t.FlagIn_Out = '1'
                            and t.InvCode = '@strInvCode'
                        ;
					"
                    , "@strOrgID", strOrgID
                   );
                ////
                _cf.db.ExecQuery(
                    strSqlBuild
                    );
            }
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:

            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            else
            {
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"	
					---- #tbl_Mst_Inventory_FilerInv:
                    select 
	                    t.*
                    into #tbl_Mst_Inventory_FilerInv
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                        inner join #input_Mst_Inventory mi --//[mylock]
                            on t.OrgID = mi.OrgID
                                and t.InvCode = mi.InvCode
                    where(1=1)


                    --- #tbl_Mst_Inventory_Filter:
                    select distinct -- 20210720. NC thêm distinct bởi trong TH kho input chứa cả kho cha và kho con thì đang bị xN số liệu.
	                    t.OrgID
	                    , t.InvCode
                    into #tbl_Mst_Inventory_Filter
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
	                    inner join #tbl_Mst_Inventory_FilerInv f --//[mylock]
		                    on (1=1)
                    where(1=1)
	                    and t.InvBUCode like f.InvBUPattern
                    ;

                    ----- #tbl_Inv_InventoryBalance_Filter:
                    select 
	                    t.OrgID
	                    , t.InvCode
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Filter
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Mst_Inventory_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode 
	                    inner join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode 
                        --------------------------------
                        zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                        ---------------------------------
                    where(1=1)
	                    and (t.ProductCode = '@strProductCode' or '@strProductCode' = '')
	                    and (mp.ProductGrpCode = '@strProductGrpCode' or '@strProductGrpCode' = '')
                    ;

                    ---- Lấy giá gần nhất theo từng sản phẩm theo từng Org:
                    select distinct
	                    t.OrgID
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Product
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode
			                    and t.ProductCode = f.ProductCode
                    where(1=1)
                    ;

                    --- #tbl_InvF_WarehouseCard_AutoID_Max_In:
                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , max(f.AutoId) AutoID_Max_In
                    into #tbl_InvF_WarehouseCard_AutoID_Max_In
                    from #tbl_Inv_InventoryBalance_Product t --//[mylock]
	                    inner join InvF_WarehouseCard f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCodeBase
			                    and f.InventoryAction = 'IN'
                    where(1=1)
	                    and f.ApprDTimeUTC <= '@strReportDTime'
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    ----- #tbl_Inv_InventoryBalance_ForReport:
                    select 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
	                    , sum(t.QtyChTotalOK) QtyTotalOK
	                    , sum(t.QtyChBlockOK) QtyBlockOK
                    into #tbl_Inv_InventoryBalance_ForReport
                    from Inv_InventoryTransaction t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
		                    on t.OrgID = t_mp.OrgID
			                    and t.ProductCode = t_mp.ProductCode
								and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
	                    and t.CreateDTimeUTC <= '@strReportDTime'
                    group by 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
                    ;

                    ---- Return:
                    select 
	                    iib.OrgID
	                    , iib.InvCode
	                    , iib.ProductCode
	                    , mp.ProductCodeUser mp_ProductCodeUser
	                    , mp.ProductName mp_ProductName
	                    , mp.ProductNameEN mp_ProductNameEN
	                    , mp.UnitCode mp_UnitCode
	                    , mp.ValConvert mp_ValConvert
						, mp.FlagLot mp_FlagLot
						, mp.FlagSerial mp_FlagSerial
						, mp.ProductType mp_ProductType
						, mp.ProductCodeRoot mp_ProductCodeRoot
						, mp.ProductCodeBase mp_ProductCodeBase
	                    , iib.QtyTotalOK Qty
	                    , iib.QtyTotalOK QtyTotalOK
	                    , (iib.QtyTotalOK - ABS(iib.QtyBlockOK)) QtyAvailOK
	                    , iib.QtyBlockOK QtyBlockOK
	                    , ifwhc.ValMixBase 
	                    , ifwhc.ValMixBaseDesc
	                    , ifwhc.ValMixBaseAfterDesc
	                    , IsNull(ifwhc.ValMixBase, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBase
	                    , IsNull(ifwhc.ValMixBaseDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseDesc
	                    , IsNull(ifwhc.ValMixBaseAfterDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseAfterDesc
	                    , iibl.UPInv
	                    , iibl.TotalValInv
                    into #tbl_Return
                    from #tbl_Inv_InventoryBalance_ForReport iib --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t --//[mylock]
		                    on iib.OrgID = t.OrgID
			                    and iib.InvCode = t.InvCode
			                    and iib.ProductCode = t.ProductCode
	                    inner join Inv_InventoryBalance iibl --//[mylock]
		                    on t.OrgID = iibl.OrgID
			                    and t.InvCode = iibl.InvCode
			                    and t.ProductCode = iibl.ProductCode
	                    left join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode
	                    left join #tbl_InvF_WarehouseCard_AutoID_Max_In f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCode
	                    left join InvF_WarehouseCard ifwhc --//[mylock]
		                    on f.AutoID_Max_In = ifwhc.AutoId
                    where(1=1)
                    ;

                    --- Return:
                    select 
	                    t.*
                    from #tbl_Return t --//[mylock]
                    where(1=1)
                        and (t.QtyTotalOK != 0 or t.QtyAvailOK != 0 or t.QtyAvailOK !=0  )
                    ;

                    --- Clear For Debug:
                    drop table #tbl_Mst_Inventory_Filter;
                    drop table #tbl_Inv_InventoryBalance_Filter;
                    drop table #tbl_Inv_InventoryBalance_Product;
                    drop table #tbl_InvF_WarehouseCard_AutoID_Max_In;
                    drop table #tbl_Return;

					"
                , "@strInvCode", strInvCode
                , "@strProductCode", strProductCode
                , "@strProductGrpCode", strProductGrpCode
                , "@strReportDTime", strReportDTimeUTC
                , "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance";
            #endregion
        }

        /// <summary>
        /// 20220518. Báo cáo mới: Báo cáo biến động giá trị tồn kho
        /// Nâng cấp dựa theo Báo cáo tồn kho
        /// </summary>
        public DataSet WAS_Rpt_InvBalanceValuationPeriodMonth(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_InvBalanceValuationPeriodMonth objRQ_Rpt_InvBalanceValuationPeriodMonth
            ////
            , out RT_Rpt_InvBalanceValuationPeriodMonth objRT_Rpt_InvBalanceValuationPeriodMonth
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_InvBalanceValuationPeriodMonth.Tid;
            objRT_Rpt_InvBalanceValuationPeriodMonth = new RT_Rpt_InvBalanceValuationPeriodMonth();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_InvBalanceValuationPeriodMonth";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_InvBalanceValuationPeriodMonth;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_InvF_InventoryOut", TJson.JsonConvert.SerializeObject(objRQ_Rpt_InvBalanceValuationPeriodMonth)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_InvBalanceValuationPeriodMonth> Lst_Rpt_InvBalanceValuationPeriodMonth = new List<Rpt_InvBalanceValuationPeriodMonth>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_InvBalanceValuationPeriodMonth.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_InvBalanceValuationPeriodMonth.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_InvBalanceValuationPeriodMonth.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }

                }
                #endregion

                #region // Rpt_InvBalanceValuationPeriodMonth:
                mdsResult = Rpt_InvBalanceValuationPeriodMonth(
                    objRQ_Rpt_InvBalanceValuationPeriodMonth.Tid // strTid
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.GwPassword // strGwPassword
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.WAUserCode // strUserCode
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.WAUserPassword // strUserPassword
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.AccessToken // strAccessToken
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.NetworkID // strNetworkID
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.ReportMonthFrom // strReportMonthFrom
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.ReportMonthTo //strReportMonthTo
                    , dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_InvBalanceValuationPeriodMonth = mdsResult.Tables["Rpt_InvBalanceValuationPeriodMonth"].Copy();
                    Lst_Rpt_InvBalanceValuationPeriodMonth = TUtils.DataTableCmUtils.ToListof<Rpt_InvBalanceValuationPeriodMonth>(dt_Rpt_InvBalanceValuationPeriodMonth);
                    objRT_Rpt_InvBalanceValuationPeriodMonth.Lst_Rpt_InvBalanceValuationPeriodMonth = Lst_Rpt_InvBalanceValuationPeriodMonth;
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

        public DataSet Rpt_InvBalanceValuationPeriodMonth(
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
            , string strReportMonthFrom
            , string strReportMonthTo
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_InvBalanceValuationPeriodMonth";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_InvBalanceValuationPeriodMonth;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    //// Filter
                , "strReportMonthFrom", strReportMonthFrom
                , "strReportMonthTo", strReportMonthTo
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

                // Sys_User_CheckAuthentication
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

                #region // Rpt_InvBalanceValuationPeriodMonthX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_InvBalanceValuationPeriodMonthX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strOrgID_RQ // strOrgID
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , strReportMonthFrom // strReportMonthFrom
                        , strReportMonthTo // strReportMonthTo
                        ////
                        , dsData // dsData
                        , out dsGetData // dsGetData
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

        private void Rpt_InvBalanceValuationPeriodMonthX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , string strReportMonthFrom
            , string strReportMonthTo
            ////
            , DataSet dsData
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_InvBalanceValuationPeriodMonthX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportMonthFrom", strReportMonthFrom
                , "strReportMonthTo", strReportMonthTo
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            //// Refine:
            //strReportMonthFrom = TUtils.CUtils.StdD (strOrgID);
            //strReportMonthTo = TUtils.CUtils.StdParam(strInvCode);
            /////
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_InvBalanceValuationPeriodMonthX_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:

            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            else
            {
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"	
					select
	                    t.*
                    from Rpt_InvBalanceValuationPeriodMonth t --//[mylock]
                    where(1=1)
                    ;
					"
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_InvBalanceValuationPeriodMonth";
            #endregion
        }

        public void Rpt_Inv_InventoryBalance_MinimumX(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strProductCode
            //, string strProductGrpCode
            //, string strFlagViewInvCode
            , DataSet dsData
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Inv_InventoryBalance_MinimumX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeUTC", strReportDTimeUTC
                , "strOrgID", strOrgID
                , "strProductCode", strProductCode
                //, "strProductGrpCode", strProductGrpCode
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            //strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            //// Refine:
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            strProductCode = TUtils.CUtils.StdParam(strProductCode);
            //strProductGrpCode = TUtils.CUtils.StdParam(strProductGrpCode);
            strReportDTimeUTC = TUtils.CUtils.StdDTimeEndDay(strReportDTimeUTC);
            /////
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:
            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                    ---- #input_Mst_Inventory:
                    select 
                        t.OrgID
                        , t.InvCode
                    into #input_Mst_Inventory
                    from Mst_Inventory t --//[mylock]
                    where (1=1)
                        --and t.InvLevel = '1'
                        and t.FlagIn_Out = '1'
                        and t.FlagActive = '1'
                        and t.OrgID = '@strOrgID'                    

                    ---- #tbl_Mst_Inventory_FilerInv:
                    select 
	                    t.*
                    into #tbl_Mst_Inventory_FilerInv
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                        inner join #input_Mst_Inventory mi --//[mylock]
                            on t.OrgID = mi.OrgID
                                and t.InvCode = mi.InvCode
                    where(1=1)
                    ;

                    --select null tbl_Mst_Inventory_FilerInv, * from #tbl_Mst_Inventory_FilerInv where(1=1);

                    --- #tbl_Mst_Inventory_Filter:
                    select distinct 
	                    t.OrgID
	                    , t.InvCode
                    into #tbl_Mst_Inventory_Filter
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
	                    inner join #tbl_Mst_Inventory_FilerInv f --//[mylock]
		                    on (1=1)
                    where(1=1)
	                    and t.InvBUCode like f.InvBUPattern
                    ;

                    --select null tbl_Mst_Inventory_Filter, * from #tbl_Mst_Inventory_Filter where(1=1);

                    ----- #tbl_Inv_InventoryBalance_Filter:
                    select 
	                    t.OrgID
	                    , t.InvCode
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Filter
                    from Inv_InventoryBalance t --//[mylock]
                        inner join #tbl_Mst_Inventory_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode 
	                    inner join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode 
	                    ------------------------------------------
                        zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                        ------------------------------------------
                    where(1=1)
	                    and mp.ProductType = 'PRODUCT'
	                    and mp.FlagFG = '0'
	                    and mp.FlagActive = '1'
	                    and ('@strProductCode' = '' or mp.ProductCodeUser like '%@strProductCode%')
                    ;

                    --select null tbl_Inv_InventoryBalance_Filter, * from #tbl_Inv_InventoryBalance_Filter where(1=1);

                    ---- Lấy giá gần nhất theo từng sản phẩm theo từng Org:
                    select distinct
	                    t.OrgID
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Product
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode
			                    and t.ProductCode = f.ProductCode
                    where(1=1)
                    ;

                    --select null tbl_Inv_InventoryBalance_Product, * from #tbl_Inv_InventoryBalance_Product where(1=1);

                    --- #tbl_InvF_WarehouseCard_AutoID_Max_In:
                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , max(f.AutoId) AutoID_Max_In
                    into #tbl_InvF_WarehouseCard_AutoID_Max_In
                    from #tbl_Inv_InventoryBalance_Product t --//[mylock]
	                    inner join InvF_WarehouseCard f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCodeBase
			                    and f.InventoryAction = 'IN'
                    where(1=1)
	                    and f.ApprDTimeUTC <= '@strReportDTime'
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    --select null tbl_InvF_WarehouseCard_AutoID_Max_In, * from #tbl_InvF_WarehouseCard_AutoID_Max_In where(1=1);


                    ----- #tbl_Inv_InventoryBalance_ForReport:
                    select 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
	                    , t_mp.InvCode
	                    , sum(t.QtyChTotalOK) QtyTotalOK
	                    , sum(t.QtyChBlockOK) QtyBlockOK
                    into #tbl_Inv_InventoryBalance_ForReport
                    from Inv_InventoryTransaction t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
		                    on t.OrgID = t_mp.OrgID
			                    and t.ProductCode = t_mp.ProductCode
			                    and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
	                    and t.CreateDTimeUTC <= '@strReportDTime'
                    group by 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
	                    , t_mp.InvCode
                    ;

                    --select null tbl_Inv_InventoryBalance_ForReport, * from #tbl_Inv_InventoryBalance_ForReport where(1=1);

                    ----- #tbl_Inv_InventoryBalance_ForReport_UPInv:
                    select
                        t_mp.OrgID
                        , t_mp.ProductCode
                        , t_mp.InvCode
                        --, t.UPInv
                        --, t.TotalValInv
                        , (
                            select top 1
                                iit.UPInv
                            from Inv_InventoryTransaction iit --//[mylock]
                            where(1=1)
                                and t_mp.OrgID = iit.OrgID
                                and t_mp.ProductCode = iit.ProductCode
                                and t_mp.InvCode = iit.InvCode
                            order by
                                iit.CreateDTimeUTC desc
                        ) UPInv
                        , (
                            select top 1
                                iit.TotalValInv
                            from Inv_InventoryTransaction iit --//[mylock]
                            where(1=1)
                                and t_mp.OrgID = iit.OrgID
                                and t_mp.ProductCode = iit.ProductCode
                                and t_mp.InvCode = iit.InvCode
                            order by
                                iit.CreateDTimeUTC desc
                        ) TotalValInv
                    into #tbl_Inv_InventoryBalance_ForReport_UPInv
                    from #tbl_Inv_InventoryBalance_ForReport t --//[mylock]
                            inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
                                on t.OrgID = t_mp.OrgID
                                    and t.ProductCode = t_mp.ProductCode
                                        and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
                    ;

                    ---- Return:
                    select 
	                    iib.OrgID
	                    , iib.InvCode
	                    , iib.ProductCode
	                    , mp.ProductCodeUser mp_ProductCodeUser
	                    , mp.ProductName mp_ProductName
	                    , mp.ProductNameEN mp_ProductNameEN
	                    , mp.UnitCode mp_UnitCode
	                    , mp.ValConvert mp_ValConvert
	                    , mp.FlagLot mp_FlagLot
	                    , mp.FlagSerial mp_FlagSerial
	                    , mp.ProductType mp_ProductType
	                    , mp.ProductCodeRoot mp_ProductCodeRoot
	                    , mp.ProductCodeBase mp_ProductCodeBase
	                    , iib.QtyTotalOK Qty
	                    , iib.QtyTotalOK QtyTotalOK
	                    , (iib.QtyTotalOK - ABS(iib.QtyBlockOK)) QtyAvailOK
	                    , iib.QtyBlockOK QtyBlockOK
                        , mp.QtyMinSt QtyMinSt
                        , mp.QtyMaxSt QtyMaxSt
	                    , ifwhc.ValMixBase 
	                    , ifwhc.ValMixBaseDesc
	                    , ifwhc.ValMixBaseAfterDesc
	                    , IsNull(ifwhc.ValMixBase, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBase
	                    , IsNull(ifwhc.ValMixBaseDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseDesc
	                    , IsNull(ifwhc.ValMixBaseAfterDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseAfterDesc
                        , tblup.UPInv
                        , tblup.TotalValInv
                    into #tbl_Return
                    from #tbl_Inv_InventoryBalance_ForReport iib --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t --//[mylock]
		                    on iib.OrgID = t.OrgID
			                    and iib.InvCode = t.InvCode
			                    and iib.ProductCode = t.ProductCode
                        inner join #tbl_Inv_InventoryBalance_ForReport_UPInv tblup --//[mylock]
                            on iib.OrgID = tblup.OrgID
                                and iib.InvCode = tblup.InvCode
                                and iib.ProductCode = tblup.ProductCode
	                    left join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode
	                    left join #tbl_InvF_WarehouseCard_AutoID_Max_In f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCode
	                    left join InvF_WarehouseCard ifwhc --//[mylock]
		                    on f.AutoID_Max_In = ifwhc.AutoId
                    where(1=1)
                    ;

                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , sum(t.QtyTotalOK) as QtyTotalOK
                    into #tbl_Result
                    from #tbl_Return t --//[mylock]
                    where(1=1)
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    --- Result:
                    select
	                    t.OrgID 
	                    , t.ProductCode
	                    , t.QtyTotalOK
	                    , f.QtyMinSt
	                    , f.QtyMaxSt
	                    , f.ProductCodeUser mp_ProductCodeUser
	                    , f.ProductName mp_ProductName
	                    , f.ProductNameEN mp_ProductNameEN
	                    , f.UnitCode mp_UnitCode
	                    , f.ValConvert mp_ValConvert
	                    , f.FlagLot mp_FlagLot
	                    , f.FlagSerial mp_FlagSerial
	                    , f.ProductType mp_ProductType
	                    , f.ProductCodeRoot mp_ProductCodeRoot
	                    , f.ProductCodeBase mp_ProductCodeBase
	                    , f.ProductGrpCode mp_ProductGrpCode
	                    , g.ProductGrpName mpc_ProductGrpName
                    from #tbl_Result t --//[mylock]
                    left join Mst_Product f --//[mylock]
	                    on t.OrgID = f.OrgID
		                    and t.ProductCode = f.ProductCode
                    left join Mst_ProductGroup g --//[mylock]
	                    on f.OrgID = g.OrgID
		                    and f.ProductGrpCode = g.ProductGrpCode
                    where(1=1)
	                    and t.QtyTotalOK <= f.QtyMinSt

                    --- Clear For Debug:
                    drop table #input_Mst_Inventory;
                    drop table #tbl_Mst_Inventory_Filter;
                    drop table #tbl_Inv_InventoryBalance_Filter;
                    drop table #tbl_Inv_InventoryBalance_Product;
                    drop table #tbl_InvF_WarehouseCard_AutoID_Max_In;
                    drop table #tbl_Return;
                    drop table #tbl_Result;

                    "
                , "@strProductCode", strProductCode
                , "@strOrgID", strOrgID
                //, "@strProductGrpCode", strProductGrpCode
                , "@strReportDTime", strReportDTimeUTC
                , "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                    );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance_Minimum";
            #endregion
        }

        public DataSet Rpt_Inv_InventoryBalance_Minimum(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strProductCode
            //, string strProductGrpCode
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Inv_InventoryBalance_Minimum";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalance_Minimum;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeUTC", strReportDTimeUTC
                    , "strOrgID", strOrgID
                    , "strProductCode", strProductCode
                    //, "strProductGrpCode", strProductGrpCode
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

                //// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Rpt_Inv_InventoryBalance_Minimum:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Inv_InventoryBalance_MinimumX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken
                        , strNetworkID
                        , strOrgID_RQ
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeUTC // ReportDTime
                        , strOrgID // strOrgID
                        , strProductCode // strProductCode
                        //, strProductGrpCode // strProductGrpCode
                                             ////
                        , dsData // dsData
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_Inv_InventoryBalance_Minimum(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalance_Minimum objRQ_Rpt_Inv_InventoryBalance_Minimum
            ////
            , out RT_Rpt_Inv_InventoryBalance_Minimum objRT_Rpt_Inv_InventoryBalance_Minimum
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalance_Minimum.Tid;
            objRT_Rpt_Inv_InventoryBalance_Minimum = new RT_Rpt_Inv_InventoryBalance_Minimum();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalance_Minimum";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalance_Minimum;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_Rpt_Inv_InventoryBalance_Minimum", TJson.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_Minimum)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_Inv_InventoryBalance_Minimum> Lst_Rpt_Inv_InventoryBalance_Minimum = new List<Rpt_Inv_InventoryBalance_Minimum>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance_Minimum.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Inv_InventoryBalance_Minimum.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Inv_InventoryBalance_Minimum.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }

                }
                #endregion

                #region // Rpt_Inv_InventoryBalance_Minimum:
                mdsResult = Rpt_Inv_InventoryBalance_Minimum(
                    objRQ_Rpt_Inv_InventoryBalance_Minimum.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.AccessToken // strAccessToken
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.NetworkID // strNetworkID
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.ReportDateUTC // objReportDateTime
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.OrgID // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.ProductCode // objProductCode
                    //, objRQ_Rpt_Inv_InventoryBalance_Minimum.ProductGrpCode // objProductGrpCode
                    , dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalance_Minimum = mdsResult.Tables["Rpt_Inv_InventoryBalance_Minimum"].Copy();
                    Lst_Rpt_Inv_InventoryBalance_Minimum = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalance_Minimum>(dt_Rpt_Inv_InventoryBalance_Minimum);
                    objRT_Rpt_Inv_InventoryBalance_Minimum.Lst_Rpt_Inv_InventoryBalance_Minimum = Lst_Rpt_Inv_InventoryBalance_Minimum;
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

        public void Rpt_Inv_InventoryBalance_StorageTimeX(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strProductCode
            //, string strProductGrpCode
            //, string strFlagViewInvCode
            , DataSet dsData
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Rpt_Inv_InventoryBalance_StorageTimeX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strReportDTimeUTC", strReportDTimeUTC
                , "strOrgID", strOrgID
                , "strProductCode", strProductCode
                //, "strProductGrpCode", strProductGrpCode
                });
            #endregion

            #region // Check:
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            //strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
            //// Refine:
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            strProductCode = TUtils.CUtils.StdParam(strProductCode);
            //strProductGrpCode = TUtils.CUtils.StdParam(strProductGrpCode);
            strReportDTimeUTC = TUtils.CUtils.StdDTimeEndDay(strReportDTimeUTC);
            /////
            #endregion

            #region //// Refine and Check Input Mst_ProductGroup:
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
                        TError.ErridnInventory.Rpt_Summary_In_Out_Pivot_ProductGroupNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductGroup = dsData.Tables[strTableCheck];
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductGroup // dtData
                                             //, "StdParam", "InvCodeOutActual" // arrstrCouple
                    , "StdParam", "ProductGrpCode" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductGroup, "OrgID", typeof(object));
                for (int nScan = 0; nScan < dtInput_Mst_ProductGroup.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductGroup.Rows[nScan];
                    drScan["OrgID"] = strOrgID;
                }
            }
            #endregion

            #region //// SaveTemp Mst_ProductGroup:
            string zzzz_SqlJoin_Mst_ProductGroup_zzzzz = "----Nothing";
            if (dtInput_Mst_ProductGroup.Rows.Count > 0)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Mst_ProductGroup" // strTableName
                    , new object[] {
                            "ProductGrpCode", TConst.BizMix.Default_DBColType,
                            "OrgID", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_ProductGroup // dtData
                );
                /////
                zzzz_SqlJoin_Mst_ProductGroup_zzzzz = CmUtils.StringUtils.Replace(@"
	                        inner join #input_Mst_ProductGroup k --//[mylock]
		                        on mp.OrgID = k.OrgID
			                        and mp.ProductGrpCode = k.ProductGrpCode
                            ");
            }
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );

            string strSqlGetData = CmUtils.StringUtils.Replace(@"
                    ---- #input_Mst_Inventory:
                    select 
                        t.OrgID
                        , t.InvCode
                    into #input_Mst_Inventory
                    from Mst_Inventory t --//[mylock]
                    where (1=1)
                        --and t.InvLevel = '1'
                        and t.FlagIn_Out = '1'
                        and t.FlagActive = '1'
                        and t.OrgID = '@strOrgID'

                    ---- #tbl_Mst_Inventory_FilerInv:
                    select 
	                    t.*
                    into #tbl_Mst_Inventory_FilerInv
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                        inner join #input_Mst_Inventory mi --//[mylock]
                            on t.OrgID = mi.OrgID
                                and t.InvCode = mi.InvCode
                    where(1=1)
                    ;

                    --select null tbl_Mst_Inventory_FilerInv, * from #tbl_Mst_Inventory_FilerInv where(1=1);

                    --- #tbl_Mst_Inventory_Filter:
                    select distinct 
	                    t.OrgID
	                    , t.InvCode
                    into #tbl_Mst_Inventory_Filter
                    from Mst_Inventory t --//[mylock]
                        inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            on t.OrgID = t_MstNNT_View.OrgID
	                    inner join #tbl_Mst_Inventory_FilerInv f --//[mylock]
		                    on (1=1)
                    where(1=1)
	                    and t.InvBUCode like f.InvBUPattern
                    ;

                    --select null tbl_Mst_Inventory_Filter, * from #tbl_Mst_Inventory_Filter where(1=1);

                    ----- #tbl_Inv_InventoryBalance_Filter:
                    select 
	                    t.OrgID
	                    , t.InvCode
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Filter
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Mst_Inventory_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode 
	                    inner join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode
                        ------------------------------------------
                        zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                        ------------------------------------------
                    where(1=1)
						and mp.FlagActive = '1'
                        and ('@strProductCode' = '' or mp.ProductCodeUser = '@strProductCode')
                    ;

                    --select null tbl_Inv_InventoryBalance_Filter, * from #tbl_Inv_InventoryBalance_Filter where(1=1);

                    ---- Lấy giá gần nhất theo từng sản phẩm theo từng Org:
                    select distinct
	                    t.OrgID
	                    , t.ProductCode
                    into #tbl_Inv_InventoryBalance_Product
                    from Inv_InventoryBalance t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.InvCode = f.InvCode
			                    and t.ProductCode = f.ProductCode
                    where(1=1)
                    ;

                    --select null tbl_Inv_InventoryBalance_Product, * from #tbl_Inv_InventoryBalance_Product where(1=1);

                    --- #tbl_InvF_WarehouseCard_AutoID_Max_In:
                    select 
	                    t.OrgID
	                    , t.ProductCode
	                    , max(f.AutoId) AutoID_Max_In
                    into #tbl_InvF_WarehouseCard_AutoID_Max_In
                    from #tbl_Inv_InventoryBalance_Product t --//[mylock]
	                    inner join InvF_WarehouseCard f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCodeBase
			                    and f.InventoryAction = 'IN'
                    where(1=1)
	                    and f.ApprDTimeUTC <= '@strReportDTime'
                    group by 
	                    t.OrgID
	                    , t.ProductCode
                    ;

                    --select null tbl_InvF_WarehouseCard_AutoID_Max_In, * from #tbl_InvF_WarehouseCard_AutoID_Max_In where(1=1);

                    ----- #tbl_Inv_InventoryBalance_ForReport:
                    select 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
	                    , sum(t.QtyChTotalOK) QtyTotalOK
	                    , sum(t.QtyChBlockOK) QtyBlockOK
                    into #tbl_Inv_InventoryBalance_ForReport
                    from Inv_InventoryTransaction t --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
		                    on t.OrgID = t_mp.OrgID
			                    and t.ProductCode = t_mp.ProductCode
								and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
	                    and t.CreateDTimeUTC <= '@strReportDTime'
                    group by 
	                    t_mp.OrgID
	                    , t_mp.ProductCode
						, t_mp.InvCode
                    ;

                    --select null tbl_Inv_InventoryBalance_ForReport, * from #tbl_Inv_InventoryBalance_ForReport where(1=1);

                    ----- #tbl_Inv_InventoryBalance_ForReport_UPInv:
                    select
                        t_mp.OrgID
                        , t_mp.ProductCode
                        , t_mp.InvCode
                        , (
                                select top 1
                                    iit.UPInv
                                from Inv_InventoryTransaction iit --//[mylock]
                                where(1=1)
                                    and t_mp.OrgID = iit.OrgID
                                    and t_mp.ProductCode = iit.ProductCode
                                    and t_mp.InvCode = iit.InvCode
                                order by
                                    iit.CreateDTimeUTC desc
                        ) UPInv
                        , (
                                select top 1
                                    iit.TotalValInv
                                from Inv_InventoryTransaction iit --//[mylock]
                                where(1=1)
                                    and t_mp.OrgID = iit.OrgID
                                    and t_mp.ProductCode = iit.ProductCode
                                    and t_mp.InvCode = iit.InvCode
                                order by
                                    iit.CreateDTimeUTC desc
                        ) TotalValInv
                    into #tbl_Inv_InventoryBalance_ForReport_UPInv
                    from #tbl_Inv_InventoryBalance_ForReport t --//[mylock]
                        inner join #tbl_Inv_InventoryBalance_Filter t_mp --//[mylock]
                            on t.OrgID = t_mp.OrgID
                                and t.ProductCode = t_mp.ProductCode
                                    and t.InvCode = t_mp.InvCode
                        inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                            on t.OrgID = t_mi_View.OrgID
                                and t.InvCode = t_mi_View.InvCode
                    where(1=1)
                    ;

                    ---- #tbl_Inv_InventoryTransaction_Filter:
                    select 
                        t.OrgID
                        , t.InvCode
                        , t.ProductCode
                        , MAX(iit.CreateDTimeUTC) LastDTimeInvIn
                    into #tbl_Inv_InventoryTransaction_Filter
                    from #tbl_Inv_InventoryBalance_Filter t --//[mylock]
                    left join Inv_InventoryTransaction iit --//[mylock]
						on t.OrgID = iit.OrgID
							and t.ProductCode = iit.ProductCode
                            and t.InvCode = iit.InvCode
                    where(1=1)
                        and iit.FunctionName = 'INVF_INVENTORYIN_APPRX'
                    group by 
	                    t.OrgID
                        , t.InvCode
                        , t.ProductCode
    
                    ---- Return:
                    select 
	                    iib.OrgID
	                    , iib.InvCode
	                    , iib.ProductCode
	                    , mp.ProductCodeUser mp_ProductCodeUser
	                    , mp.ProductName mp_ProductName
	                    , mp.ProductNameEN mp_ProductNameEN
	                    , mp.UnitCode mp_UnitCode
	                    , mp.ValConvert mp_ValConvert
						, mp.FlagLot mp_FlagLot
						, mp.FlagSerial mp_FlagSerial
						, mp.ProductType mp_ProductType
						, mp.ProductCodeRoot mp_ProductCodeRoot
						, mp.ProductCodeBase mp_ProductCodeBase
						, mp.ProductGrpCode mp_ProductGrpCode
						, mpc.ProductGrpName mpc_ProductGrpName
	                    , iib.QtyTotalOK Qty
	                    , iib.QtyTotalOK QtyTotalOK
	                    , (iib.QtyTotalOK - ABS(iib.QtyBlockOK)) QtyAvailOK
	                    , iib.QtyBlockOK QtyBlockOK
                        , mp.QtyMinSt QtyMinSt
                        , mp.QtyMaxSt QtyMaxSt
	                    , ifwhc.ValMixBase 
	                    , ifwhc.ValMixBaseDesc
	                    , ifwhc.ValMixBaseAfterDesc
	                    , IsNull(ifwhc.ValMixBase, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBase
	                    , IsNull(ifwhc.ValMixBaseDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseDesc
	                    , IsNull(ifwhc.ValMixBaseAfterDesc, 0.0 ) * IsNull(iib.QtyTotalOK, 0.0) TotalValMixBaseAfterDesc
                        , tblup.UPInv
                        , tblup.TotalValInv
                        , iit.LastDTimeInvIn
                        , DATEDIFF(DAY, iit.LastDTimeInvIn, GETDATE()) StorageTime
                    into #tbl_Return
                    from #tbl_Inv_InventoryBalance_ForReport iib --//[mylock]
	                    inner join #tbl_Inv_InventoryBalance_Filter t --//[mylock]
		                    on iib.OrgID = t.OrgID
			                    and iib.InvCode = t.InvCode
			                    and iib.ProductCode = t.ProductCode
                        inner join #tbl_Inv_InventoryBalance_ForReport_UPInv tblup --//[mylock]
                            on iib.OrgID = tblup.OrgID
                                and iib.InvCode = tblup.InvCode
                                and iib.ProductCode = tblup.ProductCode
	                    left join Mst_Product mp --//[mylock]
		                    on t.OrgID = mp.OrgID
			                    and t.ProductCode = mp.ProductCode
						left join Mst_ProductGroup mpc --//[mylock]
							on mp.OrgID = mpc.OrgID
								and mp.ProductGrpCode = mpc.ProductGrpCode
	                    left join #tbl_InvF_WarehouseCard_AutoID_Max_In f --//[mylock]
		                    on t.OrgID = f.OrgID
			                    and t.ProductCode = f.ProductCode
	                    left join InvF_WarehouseCard ifwhc --//[mylock]
		                    on f.AutoID_Max_In = ifwhc.AutoId
                        left join #tbl_Inv_InventoryTransaction_Filter iit --//[mylock]
						    on t.OrgID = iit.OrgID
							    and t.ProductCode = iit.ProductCode
                                and t.InvCode = iit.InvCode
                    where(1=1)
                    ;

                    select 
                        t.*
                    from #tbl_Return t --//[mylock]
                    where(1=1)
                        and (t.QtyTotalOK != 0 or t.QtyAvailOK != 0 or t.QtyAvailOK !=0  )
                    order by t.ProductCode asc, t.LastDTimeInvIn desc

                    --- Clear For Debug:
                    drop table #tbl_Mst_Inventory_Filter;
                    drop table #tbl_Inv_InventoryBalance_Filter;
                    drop table #tbl_Inv_InventoryBalance_Product;
                    drop table #tbl_InvF_WarehouseCard_AutoID_Max_In;
                    drop table #tbl_Inv_InventoryTransaction_Filter;
                    drop table #tbl_Return;

                    "
                , "@strProductCode", strProductCode
                //, "@strProductGrpCode", strProductGrpCode
                , "@strOrgID", strOrgID
                , "@strReportDTime", strReportDTimeUTC
                , "zzzz_SqlJoin_Mst_ProductGroup_zzzzz", zzzz_SqlJoin_Mst_ProductGroup_zzzzz
                    );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Rpt_Inv_InventoryBalance_StorageTime";
            #endregion
        }

        public DataSet Rpt_Inv_InventoryBalance_StorageTime(
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
            , string strReportDTimeUTC
            , string strOrgID
            , string strProductCode
            //, string strProductGrpCode
            /////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Rpt_Inv_InventoryBalance_StorageTime";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_Inv_InventoryBalance_StorageTime;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strReportDTimeUTC", strReportDTimeUTC
                    , "strOrgID", strOrgID
                    , "strProductCode", strProductCode
                    //, "strProductGrpCode", strProductGrpCode
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

                //// Tạm thời bỏ để chạy nhúng sang inos khi demo xong thì cung cấp đầu hàm khác.
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

                #region // Rpt_Inv_InventoryBalance_StorageTime:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    Rpt_Inv_InventoryBalance_StorageTimeX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken
                        , strNetworkID
                        , strOrgID_RQ
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strReportDTimeUTC // ReportDTime
                        , strOrgID // strOrgID
                        , strProductCode // strProductCode
                        //, strProductGrpCode // strProductGrpCode
                                         ////
                        , dsData // dsData
                        , out dsGetData // dsGetData
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

        public DataSet WAS_Rpt_Inv_InventoryBalance_StorageTime(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_Inv_InventoryBalance_StorageTime objRQ_Rpt_Inv_InventoryBalance_StorageTime
            ////
            , out RT_Rpt_Inv_InventoryBalance_StorageTime objRT_Rpt_Inv_InventoryBalance_StorageTime
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_Inv_InventoryBalance_StorageTime.Tid;
            objRT_Rpt_Inv_InventoryBalance_StorageTime = new RT_Rpt_Inv_InventoryBalance_StorageTime();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Rpt_Inv_InventoryBalance_StorageTime";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_Inv_InventoryBalance_StorageTime;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objRQ_Rpt_Inv_InventoryBalance_StorageTime", TJson.JsonConvert.SerializeObject(objRQ_Rpt_Inv_InventoryBalance_StorageTime)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Rpt_Inv_InventoryBalance_StorageTime> Lst_Rpt_Inv_InventoryBalance_StorageTime = new List<Rpt_Inv_InventoryBalance_StorageTime>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Rpt_Inv_InventoryBalance_StorageTime.Lst_Mst_ProductGroup == null)
                        objRQ_Rpt_Inv_InventoryBalance_StorageTime.Lst_Mst_ProductGroup = new List<Mst_ProductGroup>();
                    {
                        DataTable dt_Mst_ProductGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductGroup>(objRQ_Rpt_Inv_InventoryBalance_StorageTime.Lst_Mst_ProductGroup, "Mst_ProductGroup");
                        dsData.Tables.Add(dt_Mst_ProductGroup);
                    }

                }
                #endregion

                #region // Rpt_Inv_InventoryBalance_StorageTime:
                mdsResult = Rpt_Inv_InventoryBalance_StorageTime(
                    objRQ_Rpt_Inv_InventoryBalance_StorageTime.Tid // strTid
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.GwPassword // strGwPassword
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.WAUserCode // strUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.WAUserPassword // strUserPassword
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.AccessToken // strAccessToken
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.NetworkID // strNetworkID
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.ReportDateUTC // objReportDateTime
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.OrgID // objOrgID
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.ProductCode // objProductCode
                                                                         //, objRQ_Rpt_Inv_InventoryBalance_Minimum.ProductGrpCode // objProductGrpCode
                    , dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Rpt_Inv_InventoryBalance_StorageTime = mdsResult.Tables["Rpt_Inv_InventoryBalance_StorageTime"].Copy();
                    Lst_Rpt_Inv_InventoryBalance_StorageTime = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalance_StorageTime>(dt_Rpt_Inv_InventoryBalance_StorageTime);
                    objRT_Rpt_Inv_InventoryBalance_StorageTime.Lst_Rpt_Inv_InventoryBalance_StorageTime = Lst_Rpt_Inv_InventoryBalance_StorageTime;
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
