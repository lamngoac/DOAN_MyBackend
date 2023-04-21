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
        #region // Inv_InventoryTransaction_Perform:        
        public void Inv_InventoryTransaction_Perform(
            ref ArrayList alParamsCoupleError
            , string strTableNameDBTemp
            , string strInventoryTransactionAction
            , object strCreateDTimeUTC
            , object strCreateBy
            , object dblMinQtyTotalOK
            , object dblMinQtyBlockOK
            , object dblMinQtyAvailOK
            , object dblMinQtyTotalNG
            , object dblMinQtyBlockNG
            , object dblMinQtyAvailNG
            , object dblMinQtyPlanTotal
            , object dblMinQtyPlanBlock
            , object dblMinQtyPlanAvail
            )
        {
            #region // Upload and Check Input:
            {
                string strSqlCheckInput = CmUtils.StringUtils.Replace(@"
					    select
						    t.*
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    where (1=1)
						    and t.QtyChTotalOK = 0.0
						    and t.QtyChBlockOK = 0.0
						    and t.QtyChTotalNG = 0.0
						    and t.QtyChBlockNG = 0.0
						    and t.QtyPlanChTotal = 0.0
						    and t.QtyPlanChBlock = 0.0
					    ;
                        select t.* from zzzzClauseTableNameDBTemp t --//[mylock]
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );

                DataTable dtCheckInput = _cf.db.ExecQuery(strSqlCheckInput).Tables[0];
                ////
                if (dtCheckInput.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.InvCode", dtCheckInput.Rows[0]["InvCode"].ToString()
                        , "Check.ProductCode", dtCheckInput.Rows[0]["ProductCode"].ToString()
                        , "Check.QtyChTotalOK", dtCheckInput.Rows[0]["QtyChTotalOK"].ToString()
                        , "Check.QtyChBlockOK", dtCheckInput.Rows[0]["QtyChBlockOK"].ToString()
                        , "Check.QtyChTotalNG", dtCheckInput.Rows[0]["QtyChTotalNG"].ToString()
                        , "Check.QtyChBlockNG", dtCheckInput.Rows[0]["QtyChBlockNG"].ToString()
                        , "Check.QtyPlanChTotal", dtCheckInput.Rows[0]["QtyPlanChTotal"].ToString()
                        , "Check.QtyPlanChBlock", dtCheckInput.Rows[0]["QtyPlanChBlock"].ToString()
                        , "Check.ErrRows.Count", dtCheckInput.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyAllZero
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                string strSqlGen_InvTransaction_Threshold = CmUtils.StringUtils.Replace(@"
                        ---- zzzzClauseTableNameDBTemp_Threshold: Gen.
                        select
	                         Cast(null as nvarchar(100)) CreateDTimeUTC
	                         , Cast(null as nvarchar(100)) CreateBy
	                         , Cast(null as float) MinQtyTotalOK
	                         , Cast(null as float) MinQtyBlockOK
	                         , Cast(null as float) MinQtyAvailOK
	                         , Cast(null as float) MinQtyTotalNG
	                         , Cast(null as float) MinQtyBlockNG
	                         , Cast(null as float) MinQtyAvailNG
	                         , Cast(null as float) MinQtyPlanTotal
	                         , Cast(null as float) MinQtyPlanBlock
	                         , Cast(null as float) MinQtyPlanAvail
                        into zzzzClauseTableNameDBTemp_Threshold
                        where (0=1)
                        ;

                        ---- Insert:
                        insert zzzzClauseTableNameDBTemp_Threshold(
                            CreateDTimeUTC
                            , CreateBy
                            , MinQtyTotalOK
                            , MinQtyBlockOK
                            , MinQtyAvailOK
                            , MinQtyTotalNG
                            , MinQtyBlockNG
                            , MinQtyAvailNG
                            , MinQtyPlanTotal
                            , MinQtyPlanBlock
                            , MinQtyPlanAvail
                        )
                        values(
                            '@strCreateDTimeSv'
                            , '@strCreateBySv'
                            , dblMinQtyTotalOK
                            , dblMinQtyBlockOK
                            , dblMinQtyAvailOK
                            , dblMinQtyTotalNG
                            , dblMinQtyBlockNG
                            , dblMinQtyAvailNG
                            , dblMinQtyPlanTotal
                            , dblMinQtyPlanBlock
                            , dblMinQtyPlanAvail
                        );

                        select t.* from zzzzClauseTableNameDBTemp_Threshold t --//[mylock]

                        ---- 
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strCreateDTimeSv", strCreateDTimeUTC
                    , "@strCreateBySv", strCreateBy
                    , "dblMinQtyTotalOK", dblMinQtyTotalOK
                    , "dblMinQtyBlockOK", dblMinQtyBlockOK
                    , "dblMinQtyAvailOK", dblMinQtyAvailOK
                    , "dblMinQtyTotalNG", dblMinQtyTotalNG
                    , "dblMinQtyBlockNG", dblMinQtyBlockNG
                    , "dblMinQtyAvailNG", dblMinQtyAvailNG
                    , "dblMinQtyPlanTotal", dblMinQtyPlanTotal
                    , "dblMinQtyPlanBlock", dblMinQtyPlanBlock
                    , "dblMinQtyPlanAvail", dblMinQtyPlanAvail
                    );
                /////

                DataTable dtGen_InvTransaction_Threshold = _cf.db.ExecQuery(strSqlGen_InvTransaction_Threshold).Tables[0];
                ////
            }
            #endregion

            #region // Inv_InventoryBalance: Save and Check.
            {
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
					    ---- Inv_InventoryTransaction_Total:
					    select 
						    t.OrgID
						    , t.InvCode InvCode
						    , t.ProductCode ProductCode
						    , t.NetworkID NetworkID
						    --, t.MST MST
						    , Sum(t.QtyChTotalOK) QtyChTotalOK
						    , Sum(t.QtyChBlockOK) QtyChBlockOK
						    , Sum(t.QtyChTotalNG) QtyChTotalNG
						    , Sum(t.QtyChBlockNG) QtyChBlockNG
						    , Sum(t.QtyPlanChTotal) QtyPlanChTotal
						    , Sum(t.QtyPlanChBlock) QtyPlanChBlock
					    into zzzzClauseTableNameDBTemp_Total
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    group by
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , t.NetworkID 
						    --, t.MST 
					    ;
					
					    ---- Inv_InventoryBalance: Insert BlankRecord.
					    insert into Inv_InventoryBalance
					    (
							OrgID
							, InvCode
							, ProductCode
							, NetworkID
							, QtyTotalOK
							, QtyBlockOK
							, QtyAvailOK
							, QtyTotalNG
							, QtyAvailNG
							, LogLUDTimeUTC
							, LogLUBy
					    )
					    select
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , t.NetworkID
						    , 0.0 QtyTotalOK
						    , 0.0 QtyBlockOK
						    , 0.0 QtyAvailOK
						    , 0.0 QtyTotalNG
						    , 0.0 QtyBlockNG
						    , 0.0 QtyAvailNG
						    , th.CreateDTimeUTC
						    , th.CreateBy
					    from zzzzClauseTableNameDBTemp_Total t --//[mylock]
						    left join Inv_InventoryBalance iib --//[mylock]
							    on t.OrgID = iib.OrgID
                                    and t.InvCode = iib.InvCode
                                    and t.ProductCode = iib.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (iib.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
					    ;

					    ---- Inv_InventoryTransaction:
					    insert into Inv_InventoryTransaction
					    (
						     , OrgID
						     , InvCode
						     , ProductCode
						     , NetworkID
						     , QtyChTotalOK
						     , QtyChBlockOK
						     , QtyChTotalNG
						     , QtyChBlockNG
						     , Remark
						     , CreateDTimeUTC
						     , CreateBy
						     , FunctionName
						     , RefType
						     , RefCode00
						     , RefCode01
						     , RefCode02
						     , RefCode03
						     , RefCode04
						     , RefCode05
					    )
					    select 
						   t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , t.NetworkID 
						    , t.QtyChTotalOK
						    , t.QtyChBlockOK
						    , t.QtyChTotalNG
						    , t.QtyChBlockNG
						    , null Remark
						    , t.CreateDTimeUTC
						    , t.CreateBy
						    , t.FunctionName
						    , t.RefType
						    , t.RefCode00
						    , t.RefCode01
						    , t.RefCode02
						    , t.RefCode03
						    , t.RefCode04
						    , t.RefCode05
					    from zzzzClauseTableNameDBTemp t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Inv_InventoryBalance: Upd.
					    update t
					    set 
						    t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
						    , t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
						    , t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
						    , t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
						    , t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
						    , t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
						    , t.LogLUDTimeUTC = th.CreateDTimeUTC
						    , t.LogLUBy = th.CreateBy
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
							        and t.InvCode = f.InvCode 
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Check:
					    select 
						    t.*
						    , th.MinQtyTotalOK
						    , th.MinQtyBlockOK
						    , th.MinQtyAvailOK
						    , th.MinQtyTotalNG
						    , th.MinQtyBlockNG
						    , th.MinQtyAvailNG
						    , th.MinQtyPlanTotal
						    , th.MinQtyPlanBlock
						    , th.MinQtyPlanAvail
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (
                                    t.QtyTotalOK < th.MinQtyTotalOK 
                                        or t.QtyBlockOK < th.MinQtyBlockOK 
                                        or t.QtyAvailOK < th.MinQtyAvailOK 
                                        or t.QtyTotalNG < th.MinQtyTotalNG 
                                        or t.QtyBlockNG < th.MinQtyBlockNG 
                                        or t.QtyAvailNG < th.MinQtyAvailNG
                                        or t.QtyPlanTotal < th.MinQtyPlanTotal 
                                        or t.QtyPlanBlock < th.MinQtyPlanBlock 
                                        or t.QtyPlanAvail < th.MinQtyPlanAvail
                            ) 
					    ;

					    ---- Clear for Debug:
					    --drop table zzzzClauseTableNameDBTemp_Threshold;
					    --drop table zzzzClauseTableNameDBTemp_Total;
					    --drop table zzzzClauseTableNameDBTemp;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );

                DataTable dtCheckResult = _cf.db.ExecQuery(strSqlExec).Tables[0];
                ////
                if (dtCheckResult.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheckResult.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheckResult.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheckResult.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheckResult.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheckResult.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheckResult.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheckResult.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.MinQtyAvailOK", dtCheckResult.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheckResult.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheckResult.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheckResult.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheckResult.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheckResult.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheckResult.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheckResult.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyProduct
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////

            }
            #endregion

            #region // Inv_InventoryBalanceLot: Save and Check.
            {
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						---- Inv_InventoryBalanceLot_Total:
						select 
							t.OrgID OrgID
							, t.InvCode InvCode
							, t.ProductCode ProductCode
							, t.PartLotNo PartLotNo
							, Sum(t.QtyChTotalOK) QtyChTotalOK
							, Sum(t.QtyChBlockOK) QtyChBlockOK
							, Sum(t.QtyChTotalNG) QtyChTotalNG
							, Sum(t.QtyChBlockNG) QtyChBlockNG
						into zzzzClauseTableNameDBTempLot_Total
						from zzzzClauseTableNameDBTempLot t --//[mylock]
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
							, t.PartLotNo
						;

						---- Inv_InventoryBalance: Insert BlankRecord.
						insert into Inv_InventoryBalanceLot
						(
	                        OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , NetworkID
	                        , QtyTotalOK
	                        , QtyBlockOK
	                        , QtyAvailOK
	                        , QtyTotalNG
	                        , QtyBlockNG
	                        , QtyAvailNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , LogLUDTimeUTC
	                        , LogLUBy
						)
						select
	                        t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
							, 0.0 QtyTotalOK
							, 0.0 QtyBlockOK
							, 0.0 QtyAvailOK
							, 0.0 QtyTotalNG
							, 0.0 QtyBlockNG
							, 0.0 QtyAvailNG
							, f.ProductionDate
							, f.ExpiredDate
							, f.ValDateExpired
							, th.CreateDTimeUTC
							, th.CreateBy
						from zzzzClauseTableNameDBTempLot_Total t --//[mylock]
							left join Inv_InventoryBalanceLot iibl --//[mylock]
								on t.OrgID = iibl.OrgID
									and t.InvCode = iibl.InvCode
									and t.ProductCode = iibl.ProductCode
									and t.ProductLotNo = iibl.ProductLotNo
							inner join zzzzClauseTableNameDBTempLot f --//[mylock]
								on t.OrgID = iibl.OrgID
									and t.InvCode = iibl.InvCode
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo 
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (iibl.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
						;

						---- Inv_InventoryTransactionLot:
						insert into Inv_InventoryTransactionLot
						(
	                        NetworkID
	                        , OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , QtyChTotalOK
	                        , QtyChBlockOK
	                        , QtyChTotalNG
	                        , QtyChBlockNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , Remark
	                        , CreateDTimeUTC
	                        , CreateBy
	                        , FunctionName
	                        , RefType
	                        , RefCode00
	                        , RefCode01
	                        , RefCode02
	                        , RefCode03
	                        , RefCode04
	                        , RefCode05
						)
						select 
	                        t.NetworkID
	                        , t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
	                        , t.QtyChTotalOK
	                        , t.QtyChBlockOK
	                        , t.QtyChTotalNG
	                        , t.QtyChBlockNG
	                        , t.ProductionDate
	                        , t.ExpiredDate
	                        , t.ValDateExpired
							, t.Remark
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.FunctionName
							, t.RefType
							, t.RefCode00
							, t.RefCode01
							, t.RefCode02
							, t.RefCode03
							, t.RefCode04
							, t.RefCode05
						from zzzzClauseTableNameDBTempLot t --//[mylock]
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						;

						---- Inv_InventoryBalanceLot: Upd.
						update t
						set 
							t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
							, t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
							, t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
							, t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
							, t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
							, t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
							, t.LogLUDTimeUTC = th.CreateDTimeUTC
							, t.LogLUBy = th.CreateBy
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.OrgID = f.OrgID 
									and t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						;

						---- Check:
						select 
							t.*
							, th.MinQtyTotalOK
							, th.MinQtyBlockOK
							, th.MinQtyAvailOK
							, th.MinQtyTotalNG
							, th.MinQtyBlockNG
							, th.MinQtyAvailNG
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG) 
						;

						---- #tbl_Inv_InventoryBalanceLot_Draft:
						select
							t.OrgID
							, t.InvCode
							, t.ProductCode
							--, t.PartLotNo
							, Sum(t.QtyTotalOK) TotalQtyTotalOK_PartLot
							, Sum(t.QtyBlockOK) TotalQtyBlockOK_PartLot
							, Sum(t.QtyAvailOK) TotalQtyAvailOK_PartLot 
							, Sum(t.QtyTotalNG) TotalQtyTotalNG_PartLot
							, Sum(t.QtyBlockNG) TotalQtyBlockNG_PartLot
							, Sum(t.QtyAvailNG) TotalQtyAvailNG_PartLot
						into #tbl_Inv_InventoryBalanceLot_Draft	
						from Inv_InventoryBalanceLot t --//[mylock]
                            inner join zzzzClauseTableNameDBTemp f --//[mylock]
                                on t.InvCode = f.InvCode
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						where (1=1)
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
						;

						---- Check:
						select 
							t.*
							, IsNull(f.TotalQtyTotalOK_PartLot, 0.0) TotalQtyTotalOK_PartLot
							, IsNull(f.TotalQtyBlockOK_PartLot, 0.0) TotalQtyBlockOK_PartLot
							, IsNull(f.TotalQtyAvailOK_PartLot, 0.0) TotalQtyAvailOK_PartLot
							, IsNull(f.TotalQtyTotalNG_PartLot, 0.0) TotalQtyTotalNG_PartLot
							, IsNull(f.TotalQtyBlockNG_PartLot, 0.0) TotalQtyBlockNG_PartLot
							, IsNull(f.TotalQtyAvailNG_PartLot, 0.0) TotalQtyAvailNG_PartLot
						from Inv_InventoryBalance t --//[mylock]
							inner join Mst_Product mpa --//[mylock]
								on t.OrgID = mpa.OrgID
								    on t.ProductCode = mpa.ProductCode
							inner join #tbl_Inv_InventoryBalanceLot_Draft f --//[mylock] 
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
									and t.ProductCode = f.ProductCode
						where (1=1)
							and mpa.FlagLot = '@strFlagInputLot'
							and ((t.QtyTotalOK - IsNull(f.TotalQtyTotalOK_PartLot, 0.0)) > @dblDefault_Epsilon 
								or (t.QtyBlockOK - IsNull(f.TotalQtyBlockOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailOK - IsNull(f.TotalQtyAvailOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyTotalNG - IsNull(f.TotalQtyTotalNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyBlockNG - IsNull(f.TotalQtyBlockNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailNG - IsNull(f.TotalQtyAvailNG_PartLot, 0.0)) > @dblDefault_Epsilon
								)
					;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strFlagInputLot", TConst.Flag.Active
                    , "@dblDefault_Epsilon", TConst.BizMix.Default_Epsilon
                    );


                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

                DataTable dtCheck_QtyChangeOverThreshold = dsExec.Tables[dsExec.Tables.Count - 2];
                DataTable dtCheck_QtyPartLot = dsExec.Tables[dsExec.Tables.Count - 1];
                ////
                if (dtCheck_QtyChangeOverThreshold.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyChangeOverThreshold.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyChangeOverThreshold.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductCode"]
                        , "Check.PartLotNo", dtCheck_QtyChangeOverThreshold.Rows[0]["PartLotNo"]
                        , "Check.QtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheck_QtyChangeOverThreshold.Rows.Count
                        });


                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyChangeOverThreshold
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (dtCheck_QtyPartLot.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyPartLot.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyPartLot.Rows[0]["InvCode"]
                        , "Check.QtyTotalOK", dtCheck_QtyPartLot.Rows[0]["QtyTotalOK"]
                        , "Check.TotalQtyTotalOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalOK_PartLot"]
                        , "Check.QtyBlockOK", dtCheck_QtyPartLot.Rows[0]["QtyBlockOK"]
                        , "Check.TotalQtyBlockOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockOK_PartLot"]
                        , "Check.QtyAvailOK", dtCheck_QtyPartLot.Rows[0]["QtyAvailOK"]
                        , "Check.TotalQtyAvailOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailOK_PartLot"]
                        , "Check.QtyTotalNG", dtCheck_QtyPartLot.Rows[0]["QtyTotalNG"]
                        , "Check.TotalQtyTotalNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalNG_PartLot"]
                        , "Check.QtyBlockNG", dtCheck_QtyPartLot.Rows[0]["QtyBlockNG"]
                        , "Check.TotalQtyBlockNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockNG_PartLot"]
                        , "Check.QtyAvailNG", dtCheck_QtyPartLot.Rows[0]["QtyAvailNG"]
                        , "Check.TotalQtyAvailNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailNG_PartLot"]
                        , "Check.ConditionRaiseError", "Check.TotalQtyAvailNG_PartLot", (object)"(t.QtyTotalOK <> f.TotalQtyTotalOK_PartLot or t.QtyBlockOK <> f.TotalQtyBlockOK_PartLot or t.QtyAvailOK <> f.TotalQtyAvailOK_PartLot or t.QtyTotalNG <> f.TotalQtyTotalNG_PartLot or t.QtyBlockNG <> f.TotalQtyBlockNG_PartLot or t.QtyAvailNG <> f.TotalQtyAvailNG_PartLot)"
                        , "Check.ErrRows.Count", dtCheck_QtyPartLot.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyPartLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // Inv_InventoryBalanceSerial: Save and Check.
            {
                //// Action:
                if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Add))
                {
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Insert Rows.						   
						    insert into Inv_InventoryBalanceSerial
						    (
							    OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , NetworkID
							    , ProductLotNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , LogLUDTimeUTC
							    , LogLUBy
						    )
						    select 
							    t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
						        , t.NetworkID 
							    , t.ProductLotNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , th.CreateDTimeUTC LogLUDTimeUTC
							    , th.CreateBy LogLUBy
						    from #tbl_InventoryTransactionSerial t --//[mylock]
							    inner join #tbl_InventoryTransaction_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        );
                    ////

                     _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Update))
                {
                    ////
                    string zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE = @"
							    t.LUDTimeUTC = th.CreateDTimeUTC
							    , t.LUBy = th.CreateBy
							    , t.LogLUDTimeUTC = th.CreateDTimeUTC
							    , t.LogLUBy = th.CreateBy
							    , t.RefNo_Type = f.RefNo_Type
							    , t.RefNo_PK = f.RefNo_PK
							    , t.BlockStatus = f.BlockStatus
							    , t.FlagNG = f.FlagNG
							    ";
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Upd Rows.
						    update t
						    set
							    zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.OrgID = f.OrgID
									    and t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
							    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE", zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Delete))
                {
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Delete Rows.
						    delete t
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        );
                    ////

                    DataTable dtCheckResult = _cf.db.ExecQuery(strSqlExec).Tables[0];
                    ////
                }
                {
                    //// Check:
                    string strSqlCheck = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryTransactionSerial:
						    insert into Inv_InventoryTransactionSerial
						    (
								OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , Remark
							    , CreateDTimeUTC
							    , CreateBy
							    , FunctionName
							    , RefType
							    , RefCode00
							    , RefCode01
							    , RefCode02
							    , RefCode03
							    , RefCode04
							    , RefCode05	
						    )
						    select 
								t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , '@strRemark' Remark
							    , t.CreateDTimeUTC
							    , t.CreateBy
							    , t.FunctionName
							    , t.RefType
							    , t.RefCode00
							    , t.RefCode01
							    , t.RefCode02
							    , t.RefCode03
							    , t.RefCode04
							    , t.RefCode05
						    from zzzzClauseTableNameDBTempSerial t with(nolock)
							    inner join zzzzClauseTableNameDBTemp_Threshold th with(nolock)
								    on (1=1)
						    ;

						    ---- #tbl_Inv_InventoryBalanceSerial_Draft:
						    select 
							    t.InvCode InvCode
							    , t.ProductCode ProductCode
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Inactive' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailOK_Serial
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Active' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailNG_Serial 
						    into #tbl_Inv_InventoryBalanceSerial_Draft
						    from Inv_InventoryBalanceSerial t with(nolock)
                                inner join zzzzClauseTableNameDBTemp f with(nolock)
                                    on t.InvCode = f.InvCode
                                        and t.ProductCode = f.ProductCode
						    group by
							    t.InvCode
							    , t.ProductCode
						    ;

						    ---- Check:
						    select 
							    t.*
							    , f.QtyTotalOK_Serial
							    , f.QtyBlockOK_Serial
							    , f.QtyAvailOK_Serial
							    , f.QtyTotalNG_Serial
							    , f.QtyBlockNG_Serial
							    , f.QtyAvailNG_Serial
						    from Inv_InventoryBalance t with(nolock)
							    inner join Mst_Product mpa with(nolock)
								    on t.ProductCode = mpa.ProductCode
							    inner join #tbl_Inv_InventoryBalanceSerial_Draft f with(nolock) 
								    on t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
                                inner join zzzzClauseTableNameDBTemp k with(nolock)
                                    on t.InvCode = k.InvCode
                                        and t.ProductCode= k.ProductCode
						    where (1=1)
							    and mpa.FlagSerial = '@strFlagInputSerial'
							    and (t.QtyTotalOK <> f.QtyTotalOK_Serial 
								    or t.QtyBlockOK <> f.QtyBlockOK_Serial 
								    or t.QtyAvailOK <> f.QtyAvailOK_Serial
								    or t.QtyTotalNG <> f.QtyTotalNG_Serial
								    or t.QtyBlockNG <> f.QtyBlockNG_Serial
								    or t.QtyAvailNG <> f.QtyAvailNG_Serial
							    )
						    ;
					    "
                        , "@strFlagNG_Active", TConst.Flag.Active
                        , "@strFlagNG_Inactive", TConst.Flag.Inactive
                        , "@strFlagInputSerial", TConst.Flag.Active
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "@strRemark", strInventoryTransactionAction
                        );


                    _cf.db.ExecQuery(strSqlCheck);
                    ////
                }
            }
            #endregion

            #region //// Clear for Debug:
            {
                string strSql_ClearforDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzzClauseTableNameDBTemp_Threshold;
						drop table zzzzClauseTableNameDBTemp_Total;
						drop table zzzzClauseTableNameDBTempLot_Total;
						drop table zzzzClauseTableNameDBTempLot;
						drop table zzzzClauseTableNameDBTempSerial;
						drop table zzzzClauseTableNameDBTemp;
						drop table #tbl_Inv_InventoryBalanceLot_Draft;
						drop table #tbl_Inv_InventoryBalanceSerial_Draft;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );


                _cf.db.ExecQuery(strSql_ClearforDebug);
                ////
            }
            #endregion
        }

        public void Inv_InventoryTransaction_Perform_New20200225(
            ref ArrayList alParamsCoupleError
            , string strTableNameDBTemp
            , string strInventoryTransactionAction
            , object strCreateDTimeUTC
            , object strCreateBy
            , object dblMinQtyTotalOK
            , object dblMinQtyBlockOK
            , object dblMinQtyAvailOK
            , object dblMinQtyTotalNG
            , object dblMinQtyBlockNG
            , object dblMinQtyAvailNG
            )
        {
            #region // Upload and Check Input:
            {
                string strSqlCheckInput = CmUtils.StringUtils.Replace(@"
					    select
						    t.*
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    where (1=1)
						    and t.QtyChTotalOK = 0.0
						    and t.QtyChBlockOK = 0.0
						    and t.QtyChTotalNG = 0.0
						    and t.QtyChBlockNG = 0.0
					    ;
                        select t.* from zzzzClauseTableNameDBTemp t --//[mylock]
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );

                DataTable dtCheckInput = _cf.db.ExecQuery(strSqlCheckInput).Tables[0];
                ////
                if (dtCheckInput.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.InvCode", dtCheckInput.Rows[0]["InvCode"].ToString()
                        , "Check.ProductCode", dtCheckInput.Rows[0]["ProductCode"].ToString()
                        , "Check.QtyChTotalOK", dtCheckInput.Rows[0]["QtyChTotalOK"].ToString()
                        , "Check.QtyChBlockOK", dtCheckInput.Rows[0]["QtyChBlockOK"].ToString()
                        , "Check.QtyChTotalNG", dtCheckInput.Rows[0]["QtyChTotalNG"].ToString()
                        , "Check.QtyChBlockNG", dtCheckInput.Rows[0]["QtyChBlockNG"].ToString()
                        , "Check.ErrRows.Count", dtCheckInput.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyAllZero
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                string strSqlGen_InvTransaction_Threshold = CmUtils.StringUtils.Replace(@"
                        ---- zzzzClauseTableNameDBTemp_Threshold: Gen.
                        select
	                         Cast(null as nvarchar(100)) CreateDTimeUTC
	                         , Cast(null as nvarchar(100)) CreateBy
	                         , Cast(null as float) MinQtyTotalOK
	                         , Cast(null as float) MinQtyBlockOK
	                         , Cast(null as float) MinQtyAvailOK
	                         , Cast(null as float) MinQtyTotalNG
	                         , Cast(null as float) MinQtyBlockNG
	                         , Cast(null as float) MinQtyAvailNG
                        into zzzzClauseTableNameDBTemp_Threshold
                        where (0=1)
                        ;

                        ---- Insert:
                        insert zzzzClauseTableNameDBTemp_Threshold(
                            CreateDTimeUTC
                            , CreateBy
                            , MinQtyTotalOK
                            , MinQtyBlockOK
                            , MinQtyAvailOK
                            , MinQtyTotalNG
                            , MinQtyBlockNG
                            , MinQtyAvailNG
                        )
                        values(
                            '@strCreateDTimeSv'
                            , '@strCreateBySv'
                            , dblMinQtyTotalOK
                            , dblMinQtyBlockOK
                            , dblMinQtyAvailOK
                            , dblMinQtyTotalNG
                            , dblMinQtyBlockNG
                            , dblMinQtyAvailNG
                        );

                        select t.* from zzzzClauseTableNameDBTemp_Threshold t --//[mylock]

                        ---- 
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strCreateDTimeSv", strCreateDTimeUTC
                    , "@strCreateBySv", strCreateBy
                    , "dblMinQtyTotalOK", dblMinQtyTotalOK
                    , "dblMinQtyBlockOK", dblMinQtyBlockOK
                    , "dblMinQtyAvailOK", dblMinQtyAvailOK
                    , "dblMinQtyTotalNG", dblMinQtyTotalNG
                    , "dblMinQtyBlockNG", dblMinQtyBlockNG
                    , "dblMinQtyAvailNG", dblMinQtyAvailNG
                    );
                /////

                DataTable dtGen_InvTransaction_Threshold = _cf.db.ExecQuery(strSqlGen_InvTransaction_Threshold).Tables[0];
                ////
            }
            #endregion

            #region // Inv_InventoryBalance: Save and Check.
            {
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
					    ---- Inv_InventoryTransaction_Total:
					    select 
						    t.OrgID
						    , t.InvCode InvCode
						    , t.ProductCode ProductCode
						    --, t.NetworkID NetworkID
						    --, t.MST MST
						    , Sum(t.QtyChTotalOK) QtyChTotalOK
						    , Sum(t.QtyChBlockOK) QtyChBlockOK
						    , Sum(t.QtyChTotalNG) QtyChTotalNG
						    , Sum(t.QtyChBlockNG) QtyChBlockNG
					    into zzzzClauseTableNameDBTemp_Total
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    group by
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    --, t.NetworkID 
						    --, t.MST 
					    ;
					
					    ---- Inv_InventoryBalance: Insert BlankRecord.
					    insert into Inv_InventoryBalance
					    (
							OrgID
							, InvCode
							, ProductCode
							, NetworkID
							, QtyTotalOK
							, QtyBlockOK
							, QtyAvailOK
							, QtyTotalNG
							, QtyBlockNG
							, QtyAvailNG
							, LogLUDTimeUTC
							, LogLUBy
					    )
					    select
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , 0.0 QtyTotalOK
						    , 0.0 QtyBlockOK
						    , 0.0 QtyAvailOK
						    , 0.0 QtyTotalNG
						    , 0.0 QtyBlockNG
						    , 0.0 QtyAvailNG
						    , th.CreateDTimeUTC
						    , th.CreateBy
					    from zzzzClauseTableNameDBTemp_Total t --//[mylock]
						    left join Inv_InventoryBalance iib --//[mylock]
							    on t.OrgID = iib.OrgID
                                    and t.InvCode = iib.InvCode
                                    and t.ProductCode = iib.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (iib.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
					    ;

					    ---- Inv_InventoryTransaction:
					    insert into Inv_InventoryTransaction
					    (
						     OrgID
						     , InvCode
						     , ProductCode
						     , NetworkID
						     , QtyChTotalOK
						     , QtyChBlockOK
						     , QtyChTotalNG
						     , QtyChBlockNG
						     , Remark
						     , CreateDTimeUTC
						     , CreateBy
						     , FunctionName
						     , RefType
						     , RefCode00
						     , RefCode01
						     , RefCode02
						     , RefCode03
						     , RefCode04
						     , RefCode05
					    )
					    select 
						   t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , t.QtyChTotalOK
						    , t.QtyChBlockOK
						    , t.QtyChTotalNG
						    , t.QtyChBlockNG
						    , null Remark
						    , t.CreateDTimeUTC
						    , t.CreateBy
						    , t.FunctionName
						    , t.RefType
						    , t.RefCode00
						    , t.RefCode01
						    , t.RefCode02
						    , t.RefCode03
						    , t.RefCode04
						    , t.RefCode05
					    from zzzzClauseTableNameDBTemp t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Inv_InventoryBalance: Upd.
					    update t
					    set 
						    t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
						    , t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
						    , t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
						    , t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
						    , t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
						    , t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
						    , t.LogLUDTimeUTC = th.CreateDTimeUTC
						    , t.LogLUBy = th.CreateBy
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
							        and t.InvCode = f.InvCode 
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Check:
					    select 
						    t.*
						    , th.MinQtyTotalOK
						    , th.MinQtyBlockOK
						    , th.MinQtyAvailOK
						    , th.MinQtyTotalNG
						    , th.MinQtyBlockNG
						    , th.MinQtyAvailNG
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (
                                    t.QtyTotalOK < th.MinQtyTotalOK 
                                        or t.QtyBlockOK < th.MinQtyBlockOK 
                                        or t.QtyAvailOK < th.MinQtyAvailOK 
                                        or t.QtyTotalNG < th.MinQtyTotalNG 
                                        or t.QtyBlockNG < th.MinQtyBlockNG 
                                        or t.QtyAvailNG < th.MinQtyAvailNG
                            ) 
					    ;

					    ---- Clear for Debug:
					    --drop table zzzzClauseTableNameDBTemp_Threshold;
					    --drop table zzzzClauseTableNameDBTemp_Total;
					    --drop table zzzzClauseTableNameDBTemp;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@nNetworkID", nNetworkID
                    );

                DataTable dtCheckResult = _cf.db.ExecQuery(strSqlExec).Tables[0];
                ////
                if (dtCheckResult.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheckResult.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheckResult.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheckResult.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheckResult.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheckResult.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheckResult.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheckResult.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.MinQtyAvailOK", dtCheckResult.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheckResult.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheckResult.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheckResult.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheckResult.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheckResult.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheckResult.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheckResult.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyProductLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////

            }
            #endregion

            #region // Inv_InventoryBalanceLot: Save and Check.
            {
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						---- Inv_InventoryBalanceLot_Total:
						select 
							t.OrgID OrgID
							, t.InvCode InvCode
							, t.ProductCode ProductCode
							, t.ProductLotNo ProductLotNo
							, Sum(t.QtyChTotalOK) QtyChTotalOK
							, Sum(t.QtyChBlockOK) QtyChBlockOK
							, Sum(t.QtyChTotalNG) QtyChTotalNG
							, Sum(t.QtyChBlockNG) QtyChBlockNG
						into zzzzClauseTableNameDBTempLot_Total
						from zzzzClauseTableNameDBTempLot t --//[mylock]
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
							, t.ProductLotNo
						;

						---- Inv_InventoryBalance: Insert BlankRecord.
						insert into Inv_InventoryBalanceLot
						(
	                        OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , NetworkID
	                        , QtyTotalOK
	                        , QtyBlockOK
	                        , QtyAvailOK
	                        , QtyTotalNG
	                        , QtyBlockNG
	                        , QtyAvailNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , LogLUDTimeUTC
	                        , LogLUBy
						)
						select distinct
	                        t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
	                        , '@nNetworkID'
							, 0.0 QtyTotalOK
							, 0.0 QtyBlockOK
							, 0.0 QtyAvailOK
							, 0.0 QtyTotalNG
							, 0.0 QtyBlockNG
							, 0.0 QtyAvailNG
							, f.ProductionDate
							, f.ExpiredDate
							, f.ValDateExpired
							, th.CreateDTimeUTC
							, th.CreateBy
						from zzzzClauseTableNameDBTempLot_Total t --//[mylock]
							left join Inv_InventoryBalanceLot iibl --//[mylock]
								on t.OrgID = iibl.OrgID
									and t.InvCode = iibl.InvCode
									and t.ProductCode = iibl.ProductCode
									and t.ProductLotNo = iibl.ProductLotNo
							inner join zzzzClauseTableNameDBTempLot f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo 
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (iibl.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
						;

						---- Inv_InventoryTransactionLot:
						insert into Inv_InventoryTransactionLot
						(
	                        NetworkID
	                        , OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , QtyChTotalOK
	                        , QtyChBlockOK
	                        , QtyChTotalNG
	                        , QtyChBlockNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , Remark
	                        , CreateDTimeUTC
	                        , CreateBy
	                        , FunctionName
	                        , RefType
	                        , RefCode00
	                        , RefCode01
	                        , RefCode02
	                        , RefCode03
	                        , RefCode04
	                        , RefCode05
						)
						select 
	                        '@nNetworkID'
	                        , t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
	                        , t.QtyChTotalOK
	                        , t.QtyChBlockOK
	                        , t.QtyChTotalNG
	                        , t.QtyChBlockNG
	                        , t.ProductionDate
	                        , t.ExpiredDate
	                        , t.ValDateExpired
							, '' --t.Remark
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.FunctionName
							, t.RefType
							, t.RefCode00
							, t.RefCode01
							, t.RefCode02
							, t.RefCode03
							, t.RefCode04
							, t.RefCode05
						from zzzzClauseTableNameDBTempLot t --//[mylock]
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						;

						---- Inv_InventoryBalanceLot: Upd.
						update t
						set 
							t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
							, t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
							, t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
							, t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
							, t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
							, t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
							, t.ProductionDate = k.ProductionDate
							, t.ExpiredDate = k.ExpiredDate
							, t.LogLUDTimeUTC = th.CreateDTimeUTC
							, t.LogLUBy = th.CreateBy
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.OrgID = f.OrgID 
									and t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
							left join zzzzClauseTableNameDBTempLot k --//[mylock]
								on t.OrgID = k.OrgID 
									and t.InvCode = k.InvCode 
									and t.ProductCode = k.ProductCode
									and t.ProductLotNo = k.ProductLotNo
						where (1=1)
						;

						---- Check:
						select 
							t.*
							, th.MinQtyTotalOK
							, th.MinQtyBlockOK
							, th.MinQtyAvailOK
							, th.MinQtyTotalNG
							, th.MinQtyBlockNG
							, th.MinQtyAvailNG
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG) 
						;

						---- #tbl_Inv_InventoryBalanceLot_Draft:
						select
							t.OrgID
							, t.InvCode
							, t.ProductCode
							--, t.ProductLotNo
							, Sum(t.QtyTotalOK) TotalQtyTotalOK_PartLot
							, Sum(t.QtyBlockOK) TotalQtyBlockOK_PartLot
							, Sum(t.QtyAvailOK) TotalQtyAvailOK_PartLot 
							, Sum(t.QtyTotalNG) TotalQtyTotalNG_PartLot
							, Sum(t.QtyBlockNG) TotalQtyBlockNG_PartLot
							, Sum(t.QtyAvailNG) TotalQtyAvailNG_PartLot
						into #tbl_Inv_InventoryBalanceLot_Draft	
						from Inv_InventoryBalanceLot t --//[mylock]
                            inner join zzzzClauseTableNameDBTemp f --//[mylock]
                                on t.InvCode = f.InvCode
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						where (1=1)
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
						;

						---- Check:
						select 
							t.*
							, IsNull(f.TotalQtyTotalOK_PartLot, 0.0) TotalQtyTotalOK_PartLot
							, IsNull(f.TotalQtyBlockOK_PartLot, 0.0) TotalQtyBlockOK_PartLot
							, IsNull(f.TotalQtyAvailOK_PartLot, 0.0) TotalQtyAvailOK_PartLot
							, IsNull(f.TotalQtyTotalNG_PartLot, 0.0) TotalQtyTotalNG_PartLot
							, IsNull(f.TotalQtyBlockNG_PartLot, 0.0) TotalQtyBlockNG_PartLot
							, IsNull(f.TotalQtyAvailNG_PartLot, 0.0) TotalQtyAvailNG_PartLot
						from Inv_InventoryBalance t --//[mylock]
							inner join Mst_Product mpa --//[mylock]
								on t.OrgID = mpa.OrgID
								    and t.ProductCode = mpa.ProductCode
							inner join #tbl_Inv_InventoryBalanceLot_Draft f --//[mylock] 
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
									and t.ProductCode = f.ProductCode
						where (1=1)
							and mpa.FlagLot = '@strFlagInputLot'
							and ((t.QtyTotalOK - IsNull(f.TotalQtyTotalOK_PartLot, 0.0)) > @dblDefault_Epsilon 
								or (t.QtyBlockOK - IsNull(f.TotalQtyBlockOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailOK - IsNull(f.TotalQtyAvailOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyTotalNG - IsNull(f.TotalQtyTotalNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyBlockNG - IsNull(f.TotalQtyBlockNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailNG - IsNull(f.TotalQtyAvailNG_PartLot, 0.0)) > @dblDefault_Epsilon
								)
					;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strFlagInputLot", TConst.Flag.Active
                    , "@dblDefault_Epsilon", TConst.BizMix.Default_Epsilon
                    , "@nNetworkID", nNetworkID
                    );


                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

                DataTable dtCheck_QtyChangeOverThreshold = dsExec.Tables[dsExec.Tables.Count - 2];
                DataTable dtCheck_QtyPartLot = dsExec.Tables[dsExec.Tables.Count - 1];
                ////
                if (dtCheck_QtyChangeOverThreshold.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyChangeOverThreshold.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyChangeOverThreshold.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductCode"]
                        , "Check.ProductLotNo", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductLotNo"]
                        , "Check.QtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheck_QtyChangeOverThreshold.Rows.Count
                        });


                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyChangeOverThreshold
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (dtCheck_QtyPartLot.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyPartLot.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyPartLot.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheck_QtyPartLot.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheck_QtyPartLot.Rows[0]["QtyTotalOK"]
                        , "Check.TotalQtyTotalOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalOK_PartLot"]
                        , "Check.QtyBlockOK", dtCheck_QtyPartLot.Rows[0]["QtyBlockOK"]
                        , "Check.TotalQtyBlockOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockOK_PartLot"]
                        , "Check.QtyAvailOK", dtCheck_QtyPartLot.Rows[0]["QtyAvailOK"]
                        , "Check.TotalQtyAvailOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailOK_PartLot"]
                        , "Check.QtyTotalNG", dtCheck_QtyPartLot.Rows[0]["QtyTotalNG"]
                        , "Check.TotalQtyTotalNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalNG_PartLot"]
                        , "Check.QtyBlockNG", dtCheck_QtyPartLot.Rows[0]["QtyBlockNG"]
                        , "Check.TotalQtyBlockNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockNG_PartLot"]
                        , "Check.QtyAvailNG", dtCheck_QtyPartLot.Rows[0]["QtyAvailNG"]
                        , "Check.TotalQtyAvailNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailNG_PartLot"]
                        , "Check.ConditionRaiseError","(t.QtyTotalOK <> f.TotalQtyTotalOK_PartLot or t.QtyBlockOK <> f.TotalQtyBlockOK_PartLot or t.QtyAvailOK <> f.TotalQtyAvailOK_PartLot or t.QtyTotalNG <> f.TotalQtyTotalNG_PartLot or t.QtyBlockNG <> f.TotalQtyBlockNG_PartLot or t.QtyAvailNG <> f.TotalQtyAvailNG_PartLot)"
                        , "Check.ErrRows.Count", dtCheck_QtyPartLot.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyPartLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // Inv_InventoryBalanceSerial: Save and Check.
            {
                //// Action:
                if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Add))
                {
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Insert Rows.						   
						    insert into Inv_InventoryBalanceSerial
						    (
							    OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , NetworkID
							    , ProductLotNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , LogLUDTimeUTC
							    , LogLUBy
						    )
						    select 
							    t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
	                            , '@nNetworkID'
							    , null --t.ProductLotNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , th.CreateDTimeUTC LogLUDTimeUTC
							    , th.CreateBy LogLUBy
						    from #tbl_InventoryTransactionSerial t --//[mylock]
							    inner join #tbl_InventoryTransaction_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "@nNetworkID", nNetworkID
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Update))
                {
                    ////
                    string zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE = @"
							    t.LogLUDTimeUTC = th.CreateDTimeUTC
							    , t.LogLUBy = th.CreateBy
							    , t.RefNo_Type = f.RefNo_Type
							    , t.RefNo_PK = f.RefNo_PK
							    , t.BlockStatus = f.BlockStatus
							    , t.FlagNG = f.FlagNG
							    ";
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Upd Rows.
						    update t
						    set
							    zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.OrgID = f.OrgID
									    and t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
							    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE", zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Delete))
                {
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Delete Rows.
						    delete t
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.OrgID = f.OrgID
									    and t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                {
                    //// Check:
                    string strSqlCheck = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryTransactionSerial:
						    insert into Inv_InventoryTransactionSerial
						    (
								NetworkID
								, OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , Remark
							    , CreateDTimeUTC
							    , CreateBy
							    , FunctionName
							    , RefType
							    , RefCode00
							    , RefCode01
							    , RefCode02
							    , RefCode03
							    , RefCode04
							    , RefCode05	
						    )
						    select 
	                            '@nNetworkID'
								, t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , '@strRemark' Remark
							    , t.CreateDTimeUTC
							    , t.CreateBy
							    , t.FunctionName
							    , t.RefType
							    , t.RefCode00
							    , t.RefCode01
							    , t.RefCode02
							    , t.RefCode03
							    , t.RefCode04
							    , t.RefCode05
						    from zzzzClauseTableNameDBTempSerial t with(nolock)
							    inner join zzzzClauseTableNameDBTemp_Threshold th with(nolock)
								    on (1=1)
						    ;

						    ---- #tbl_Inv_InventoryBalanceSerial_Draft:
						    select 
							    t.InvCode InvCode
							    , t.ProductCode ProductCode
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Inactive' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailOK_Serial
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Active' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailNG_Serial 
						    into #tbl_Inv_InventoryBalanceSerial_Draft
						    from Inv_InventoryBalanceSerial t with(nolock)
                                inner join zzzzClauseTableNameDBTemp f with(nolock)
                                    on t.InvCode = f.InvCode
                                        and t.ProductCode = f.ProductCode
						    group by
							    t.InvCode
							    , t.ProductCode
						    ;

						    ---- Check:
						    select 
							    t.*
							    , f.QtyTotalOK_Serial
							    , f.QtyBlockOK_Serial
							    , f.QtyAvailOK_Serial
							    , f.QtyTotalNG_Serial
							    , f.QtyBlockNG_Serial
							    , f.QtyAvailNG_Serial
						    from Inv_InventoryBalance t with(nolock)
							    inner join Mst_Product mpa with(nolock)
								    on t.ProductCode = mpa.ProductCode
							    inner join #tbl_Inv_InventoryBalanceSerial_Draft f with(nolock) 
								    on t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
                                inner join zzzzClauseTableNameDBTemp k with(nolock)
                                    on t.InvCode = k.InvCode
                                        and t.ProductCode= k.ProductCode
						    where (1=1)
							    and mpa.FlagSerial = '@strFlagInputSerial'
							    and (t.QtyTotalOK <> f.QtyTotalOK_Serial 
								    or t.QtyBlockOK <> f.QtyBlockOK_Serial 
								    or t.QtyAvailOK <> f.QtyAvailOK_Serial
								    or t.QtyTotalNG <> f.QtyTotalNG_Serial
								    or t.QtyBlockNG <> f.QtyBlockNG_Serial
								    or t.QtyAvailNG <> f.QtyAvailNG_Serial
							    )
						    ;
					    "
                        , "@strFlagNG_Active", TConst.Flag.Active
                        , "@strFlagNG_Inactive", TConst.Flag.Inactive
                        , "@strFlagInputSerial", TConst.Flag.Active
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "@strRemark", strInventoryTransactionAction
                        , "@nNetworkID", nNetworkID
                        );


                    _cf.db.ExecQuery(strSqlCheck);
                    ////
                }
            }
            #endregion

            #region //// Clear for Debug:
            {
                string strSql_ClearforDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzzClauseTableNameDBTemp_Threshold;
						drop table zzzzClauseTableNameDBTemp_Total;
						drop table zzzzClauseTableNameDBTempLot_Total;
						drop table zzzzClauseTableNameDBTempLot;
						drop table zzzzClauseTableNameDBTempSerial;
						drop table zzzzClauseTableNameDBTemp;
						drop table #tbl_Inv_InventoryBalanceLot_Draft;
						drop table #tbl_Inv_InventoryBalanceSerial_Draft;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );


                _cf.db.ExecQuery(strSql_ClearforDebug);
                ////
            }
            #endregion
        }

        public void Inv_InventoryTransaction_Perform_New20220625(
            ref ArrayList alParamsCoupleError
            , string strTableNameDBTemp
            , string strInventoryTransactionAction
            , object strCreateDTimeUTC
            , object strCreateBy
            , object dblMinQtyTotalOK
            , object dblMinQtyBlockOK
            , object dblMinQtyAvailOK
            , object dblMinQtyTotalNG
            , object dblMinQtyBlockNG
            , object dblMinQtyAvailNG
            )
        {
            #region // Upload and Check Input:
            {
                string strSqlCheckInput = CmUtils.StringUtils.Replace(@"
					    select
						    t.*
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    where (1=1)
						    and t.QtyChTotalOK = 0.0
						    and t.QtyChBlockOK = 0.0
						    and t.QtyChTotalNG = 0.0
						    and t.QtyChBlockNG = 0.0
					    ;
                        select t.* from zzzzClauseTableNameDBTemp t --//[mylock]
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );

                DataTable dtCheckInput = _cf.db.ExecQuery(strSqlCheckInput).Tables[0];
                ////
                if (dtCheckInput.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.InvCode", dtCheckInput.Rows[0]["InvCode"].ToString()
                        , "Check.ProductCode", dtCheckInput.Rows[0]["ProductCode"].ToString()
                        , "Check.QtyChTotalOK", dtCheckInput.Rows[0]["QtyChTotalOK"].ToString()
                        , "Check.QtyChBlockOK", dtCheckInput.Rows[0]["QtyChBlockOK"].ToString()
                        , "Check.QtyChTotalNG", dtCheckInput.Rows[0]["QtyChTotalNG"].ToString()
                        , "Check.QtyChBlockNG", dtCheckInput.Rows[0]["QtyChBlockNG"].ToString()
                        , "Check.ErrRows.Count", dtCheckInput.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyAllZero
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                string strSqlGen_InvTransaction_Threshold = CmUtils.StringUtils.Replace(@"
                        ---- zzzzClauseTableNameDBTemp_Threshold: Gen.
                        select
	                         Cast(null as nvarchar(100)) CreateDTimeUTC
	                         , Cast(null as nvarchar(100)) CreateBy
	                         , Cast(null as float) MinQtyTotalOK
	                         , Cast(null as float) MinQtyBlockOK
	                         , Cast(null as float) MinQtyAvailOK
	                         , Cast(null as float) MinQtyTotalNG
	                         , Cast(null as float) MinQtyBlockNG
	                         , Cast(null as float) MinQtyAvailNG
                        into zzzzClauseTableNameDBTemp_Threshold
                        where (0=1)
                        ;

                        ---- Insert:
                        insert zzzzClauseTableNameDBTemp_Threshold(
                            CreateDTimeUTC
                            , CreateBy
                            , MinQtyTotalOK
                            , MinQtyBlockOK
                            , MinQtyAvailOK
                            , MinQtyTotalNG
                            , MinQtyBlockNG
                            , MinQtyAvailNG
                        )
                        values(
                            '@strCreateDTimeSv'
                            , '@strCreateBySv'
                            , dblMinQtyTotalOK
                            , dblMinQtyBlockOK
                            , dblMinQtyAvailOK
                            , dblMinQtyTotalNG
                            , dblMinQtyBlockNG
                            , dblMinQtyAvailNG
                        );

                        select t.* from zzzzClauseTableNameDBTemp_Threshold t --//[mylock]

                        ---- 
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strCreateDTimeSv", strCreateDTimeUTC
                    , "@strCreateBySv", strCreateBy
                    , "dblMinQtyTotalOK", dblMinQtyTotalOK
                    , "dblMinQtyBlockOK", dblMinQtyBlockOK
                    , "dblMinQtyAvailOK", dblMinQtyAvailOK
                    , "dblMinQtyTotalNG", dblMinQtyTotalNG
                    , "dblMinQtyBlockNG", dblMinQtyBlockNG
                    , "dblMinQtyAvailNG", dblMinQtyAvailNG
                    );
                /////

                DataTable dtGen_InvTransaction_Threshold = _cf.db.ExecQuery(strSqlGen_InvTransaction_Threshold).Tables[0];
                ////
            }
            #endregion

            #region // Inv_InventoryBalance: Save and Check.
            {
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
					    ---- Inv_InventoryTransaction_Total:
					    select 
						    t.OrgID
						    , t.InvCode InvCode
						    , t.ProductCode ProductCode
						    --, t.NetworkID NetworkID
						    --, t.MST MST
						    , Sum(t.QtyChTotalOK) QtyChTotalOK
						    , Sum(t.QtyChBlockOK) QtyChBlockOK
						    , Sum(t.QtyChTotalNG) QtyChTotalNG
						    , Sum(t.QtyChBlockNG) QtyChBlockNG
					    into zzzzClauseTableNameDBTemp_Total
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    group by
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    --, t.NetworkID 
						    --, t.MST 
					    ;
					
					    ---- Inv_InventoryBalance: Insert BlankRecord.
					    insert into Inv_InventoryBalance
					    (
							OrgID
							, InvCode
							, ProductCode
							, NetworkID
							, QtyTotalOK
							, QtyBlockOK
							, QtyAvailOK
							, QtyTotalNG
							, QtyBlockNG
							, QtyAvailNG
							, LogLUDTimeUTC
							, LogLUBy
					    )
					    select
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , 0.0 QtyTotalOK
						    , 0.0 QtyBlockOK
						    , 0.0 QtyAvailOK
						    , 0.0 QtyTotalNG
						    , 0.0 QtyBlockNG
						    , 0.0 QtyAvailNG
						    , th.CreateDTimeUTC
						    , th.CreateBy
					    from zzzzClauseTableNameDBTemp_Total t --//[mylock]
						    left join Inv_InventoryBalance iib --//[mylock]
							    on t.OrgID = iib.OrgID
                                    and t.InvCode = iib.InvCode
                                    and t.ProductCode = iib.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (iib.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
					    ;

					    ---- Inv_InventoryTransaction:
					    insert into Inv_InventoryTransaction
					    (
						     OrgID
						     , InvCode
						     , ProductCode
						     , NetworkID
						     , QtyChTotalOK
						     , QtyChBlockOK
						     , QtyChTotalNG
						     , QtyChBlockNG
						     , Remark
						     , CreateDTimeUTC
						     , CreateBy
						     , FunctionName
						     , RefType
						     , RefCode00
						     , RefCode01
						     , RefCode02
						     , RefCode03
						     , RefCode04
						     , RefCode05
					    )
					    select 
						   t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , t.QtyChTotalOK
						    , t.QtyChBlockOK
						    , t.QtyChTotalNG
						    , t.QtyChBlockNG
						    , null Remark
						    , t.CreateDTimeUTC
						    , t.CreateBy
						    , t.FunctionName
						    , t.RefType
						    , t.RefCode00
						    , t.RefCode01
						    , t.RefCode02
						    , t.RefCode03
						    , t.RefCode04
						    , t.RefCode05
					    from zzzzClauseTableNameDBTemp t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Inv_InventoryBalance: Upd.
					    update t
					    set 
						    t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
						    , t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
						    , t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
						    , t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
						    , t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
						    , t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
						    , t.LogLUDTimeUTC = th.CreateDTimeUTC
						    , t.LogLUBy = th.CreateBy
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
							        and t.InvCode = f.InvCode 
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Check:
					    select 
						    t.*
						    , th.MinQtyTotalOK
						    , th.MinQtyBlockOK
						    , th.MinQtyAvailOK
						    , th.MinQtyTotalNG
						    , th.MinQtyBlockNG
						    , th.MinQtyAvailNG
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (
                                    t.QtyTotalOK < th.MinQtyTotalOK 
                                        or t.QtyBlockOK < th.MinQtyBlockOK 
                                        or t.QtyAvailOK < th.MinQtyAvailOK 
                                        or t.QtyTotalNG < th.MinQtyTotalNG 
                                        or t.QtyBlockNG < th.MinQtyBlockNG 
                                        or t.QtyAvailNG < th.MinQtyAvailNG
                            ) 
					    ;

					    ---- Clear for Debug:
					    --drop table zzzzClauseTableNameDBTemp_Threshold;
					    --drop table zzzzClauseTableNameDBTemp_Total;
					    --drop table zzzzClauseTableNameDBTemp;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@nNetworkID", nNetworkID
                    );

                DataTable dtCheckResult = _cf.db.ExecQuery(strSqlExec).Tables[0];
                ////
                if (dtCheckResult.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheckResult.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheckResult.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheckResult.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheckResult.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheckResult.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheckResult.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheckResult.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.MinQtyAvailOK", dtCheckResult.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheckResult.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheckResult.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheckResult.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheckResult.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheckResult.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheckResult.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheckResult.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyProductLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////

            }
            #endregion

            #region // Inv_InventoryBalanceLot: Save and Check.
     //       {
     //           string strSqlExec = CmUtils.StringUtils.Replace(@"
					//	---- Inv_InventoryBalanceLot_Total:
					//	select 
					//		t.OrgID OrgID
					//		, t.InvCode InvCode
					//		, t.ProductCode ProductCode
					//		, t.ProductLotNo ProductLotNo
					//		, Sum(t.QtyChTotalOK) QtyChTotalOK
					//		, Sum(t.QtyChBlockOK) QtyChBlockOK
					//		, Sum(t.QtyChTotalNG) QtyChTotalNG
					//		, Sum(t.QtyChBlockNG) QtyChBlockNG
					//	into zzzzClauseTableNameDBTempLot_Total
					//	from zzzzClauseTableNameDBTempLot t --//[mylock]
					//	group by
					//		t.OrgID
					//		, t.InvCode
					//		, t.ProductCode
					//		, t.ProductLotNo
					//	;

					//	---- Inv_InventoryBalance: Insert BlankRecord.
					//	insert into Inv_InventoryBalanceLot
					//	(
	    //                    OrgID
	    //                    , InvCode
	    //                    , ProductCode
	    //                    , ProductLotNo
	    //                    , NetworkID
	    //                    , QtyTotalOK
	    //                    , QtyBlockOK
	    //                    , QtyAvailOK
	    //                    , QtyTotalNG
	    //                    , QtyBlockNG
	    //                    , QtyAvailNG
	    //                    , ProductionDate
	    //                    , ExpiredDate
	    //                    , ValDateExpired
	    //                    , LogLUDTimeUTC
	    //                    , LogLUBy
					//	)
					//	select distinct
	    //                    t.OrgID
	    //                    , t.InvCode
	    //                    , t.ProductCode
	    //                    , t.ProductLotNo
	    //                    , '@nNetworkID'
					//		, 0.0 QtyTotalOK
					//		, 0.0 QtyBlockOK
					//		, 0.0 QtyAvailOK
					//		, 0.0 QtyTotalNG
					//		, 0.0 QtyBlockNG
					//		, 0.0 QtyAvailNG
					//		, f.ProductionDate
					//		, f.ExpiredDate
					//		, f.ValDateExpired
					//		, th.CreateDTimeUTC
					//		, th.CreateBy
					//	from zzzzClauseTableNameDBTempLot_Total t --//[mylock]
					//		left join Inv_InventoryBalanceLot iibl --//[mylock]
					//			on t.OrgID = iibl.OrgID
					//				and t.InvCode = iibl.InvCode
					//				and t.ProductCode = iibl.ProductCode
					//				and t.ProductLotNo = iibl.ProductLotNo
					//		inner join zzzzClauseTableNameDBTempLot f --//[mylock]
					//			on t.OrgID = f.OrgID
					//				and t.InvCode = f.InvCode
					//				and t.ProductCode = f.ProductCode
					//				and t.ProductLotNo = f.ProductLotNo 
					//		inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
					//			on (1=1)
					//	where (1=1)
					//		and (iibl.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
					//	;

					//	---- Inv_InventoryTransactionLot:
					//	insert into Inv_InventoryTransactionLot
					//	(
	    //                    NetworkID
	    //                    , OrgID
	    //                    , InvCode
	    //                    , ProductCode
	    //                    , ProductLotNo
	    //                    , QtyChTotalOK
	    //                    , QtyChBlockOK
	    //                    , QtyChTotalNG
	    //                    , QtyChBlockNG
	    //                    , ProductionDate
	    //                    , ExpiredDate
	    //                    , ValDateExpired
	    //                    , Remark
	    //                    , CreateDTimeUTC
	    //                    , CreateBy
	    //                    , FunctionName
	    //                    , RefType
	    //                    , RefCode00
	    //                    , RefCode01
	    //                    , RefCode02
	    //                    , RefCode03
	    //                    , RefCode04
	    //                    , RefCode05
					//	)
					//	select 
	    //                    '@nNetworkID'
	    //                    , t.OrgID
	    //                    , t.InvCode
	    //                    , t.ProductCode
	    //                    , t.ProductLotNo
	    //                    , t.QtyChTotalOK
	    //                    , t.QtyChBlockOK
	    //                    , t.QtyChTotalNG
	    //                    , t.QtyChBlockNG
	    //                    , t.ProductionDate
	    //                    , t.ExpiredDate
	    //                    , t.ValDateExpired
					//		, '' --t.Remark
					//		, t.CreateDTimeUTC
					//		, t.CreateBy
					//		, t.FunctionName
					//		, t.RefType
					//		, t.RefCode00
					//		, t.RefCode01
					//		, t.RefCode02
					//		, t.RefCode03
					//		, t.RefCode04
					//		, t.RefCode05
					//	from zzzzClauseTableNameDBTempLot t --//[mylock]
					//		inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
					//			on (1=1)
					//	;

					//	---- Inv_InventoryBalanceLot: Upd.
					//	update t
					//	set 
					//		t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
					//		, t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
					//		, t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
					//		, t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
					//		, t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
					//		, t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
					//		, t.ProductionDate = k.ProductionDate
					//		, t.ExpiredDate = k.ExpiredDate
					//		, t.LogLUDTimeUTC = th.CreateDTimeUTC
					//		, t.LogLUBy = th.CreateBy
					//	from Inv_InventoryBalanceLot t --//mustlock
					//		inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
					//			on t.OrgID = f.OrgID 
					//				and t.InvCode = f.InvCode 
					//				and t.ProductCode = f.ProductCode
					//				and t.ProductLotNo = f.ProductLotNo
					//		inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
					//			on (1=1)
					//		left join zzzzClauseTableNameDBTempLot k --//[mylock]
					//			on t.OrgID = k.OrgID 
					//				and t.InvCode = k.InvCode 
					//				and t.ProductCode = k.ProductCode
					//				and t.ProductLotNo = k.ProductLotNo
					//	where (1=1)
					//	;

					//	---- Check:
					//	select 
					//		t.*
					//		, th.MinQtyTotalOK
					//		, th.MinQtyBlockOK
					//		, th.MinQtyAvailOK
					//		, th.MinQtyTotalNG
					//		, th.MinQtyBlockNG
					//		, th.MinQtyAvailNG
					//	from Inv_InventoryBalanceLot t --//mustlock
					//		inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
					//			on t.InvCode = f.InvCode 
					//				and t.ProductCode = f.ProductCode
					//				and t.ProductLotNo = f.ProductLotNo
					//		inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
					//			on (1=1)
					//	where (1=1)
					//		and (t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG) 
					//	;

					//	---- #tbl_Inv_InventoryBalanceLot_Draft:
					//	select
					//		t.OrgID
					//		, t.InvCode
					//		, t.ProductCode
					//		--, t.ProductLotNo
					//		, Sum(t.QtyTotalOK) TotalQtyTotalOK_PartLot
					//		, Sum(t.QtyBlockOK) TotalQtyBlockOK_PartLot
					//		, Sum(t.QtyAvailOK) TotalQtyAvailOK_PartLot 
					//		, Sum(t.QtyTotalNG) TotalQtyTotalNG_PartLot
					//		, Sum(t.QtyBlockNG) TotalQtyBlockNG_PartLot
					//		, Sum(t.QtyAvailNG) TotalQtyAvailNG_PartLot
					//	into #tbl_Inv_InventoryBalanceLot_Draft	
					//	from Inv_InventoryBalanceLot t --//[mylock]
     //                       inner join zzzzClauseTableNameDBTemp f --//[mylock]
     //                           on t.InvCode = f.InvCode
     //                               and t.InvCode = f.InvCode
     //                               and t.ProductCode = f.ProductCode
					//	where (1=1)
					//	group by
					//		t.OrgID
					//		, t.InvCode
					//		, t.ProductCode
					//	;

					//	---- Check:
					//	select 
					//		t.*
					//		, IsNull(f.TotalQtyTotalOK_PartLot, 0.0) TotalQtyTotalOK_PartLot
					//		, IsNull(f.TotalQtyBlockOK_PartLot, 0.0) TotalQtyBlockOK_PartLot
					//		, IsNull(f.TotalQtyAvailOK_PartLot, 0.0) TotalQtyAvailOK_PartLot
					//		, IsNull(f.TotalQtyTotalNG_PartLot, 0.0) TotalQtyTotalNG_PartLot
					//		, IsNull(f.TotalQtyBlockNG_PartLot, 0.0) TotalQtyBlockNG_PartLot
					//		, IsNull(f.TotalQtyAvailNG_PartLot, 0.0) TotalQtyAvailNG_PartLot
					//	from Inv_InventoryBalance t --//[mylock]
					//		inner join Mst_Product mpa --//[mylock]
					//			on t.OrgID = mpa.OrgID
					//			    and t.ProductCode = mpa.ProductCode
					//		inner join #tbl_Inv_InventoryBalanceLot_Draft f --//[mylock] 
					//			on t.OrgID = f.OrgID
					//				and t.InvCode = f.InvCode
					//				and t.ProductCode = f.ProductCode
					//	where (1=1)
					//		and mpa.FlagLot = '@strFlagInputLot'
					//		and ((t.QtyTotalOK - IsNull(f.TotalQtyTotalOK_PartLot, 0.0)) > @dblDefault_Epsilon 
					//			or (t.QtyBlockOK - IsNull(f.TotalQtyBlockOK_PartLot, 0.0)) > @dblDefault_Epsilon
					//			or (t.QtyAvailOK - IsNull(f.TotalQtyAvailOK_PartLot, 0.0)) > @dblDefault_Epsilon
					//			or (t.QtyTotalNG - IsNull(f.TotalQtyTotalNG_PartLot, 0.0)) > @dblDefault_Epsilon
					//			or (t.QtyBlockNG - IsNull(f.TotalQtyBlockNG_PartLot, 0.0)) > @dblDefault_Epsilon
					//			or (t.QtyAvailNG - IsNull(f.TotalQtyAvailNG_PartLot, 0.0)) > @dblDefault_Epsilon
					//			)
					//;
					//"
     //               , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
     //               , "@strFlagInputLot", TConst.Flag.Active
     //               , "@dblDefault_Epsilon", TConst.BizMix.Default_Epsilon
     //               , "@nNetworkID", nNetworkID
     //               );


     //           DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

     //           DataTable dtCheck_QtyChangeOverThreshold = dsExec.Tables[dsExec.Tables.Count - 2];
     //           DataTable dtCheck_QtyPartLot = dsExec.Tables[dsExec.Tables.Count - 1];
     //           ////
     //           if (dtCheck_QtyChangeOverThreshold.Rows.Count > 0)
     //           {

     //               alParamsCoupleError.AddRange(new object[]{
     //                   "Check.OrgID", dtCheck_QtyChangeOverThreshold.Rows[0]["OrgID"]
     //                   , "Check.InvCode", dtCheck_QtyChangeOverThreshold.Rows[0]["InvCode"]
     //                   , "Check.ProductCode", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductCode"]
     //                   , "Check.ProductLotNo", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductLotNo"]
     //                   , "Check.QtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalOK"]
     //                   , "Check.MinQtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalOK"]
     //                   , "Check.QtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockOK"]
     //                   , "Check.MinQtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockOK"]
     //                   , "Check.QtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailOK"]
     //                   , "Check.MinQtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailOK"]
     //                   , "Check.QtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalNG"]
     //                   , "Check.MinQtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalNG"]
     //                   , "Check.QtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockNG"]
     //                   , "Check.MinQtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockNG"]
     //                   , "Check.QtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailNG"]
     //                   , "Check.MinQtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailNG"]
     //                   , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
     //                   , "Check.ErrRows.Count", dtCheck_QtyChangeOverThreshold.Rows.Count
     //                   });


     //               throw CmUtils.CMyException.Raise(
     //                   TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyChangeOverThreshold
     //                   , null
     //                   , alParamsCoupleError.ToArray()
     //                   );
     //           }
     //           ////
     //           if (dtCheck_QtyPartLot.Rows.Count > 0)
     //           {

     //               alParamsCoupleError.AddRange(new object[]{
     //                   "Check.OrgID", dtCheck_QtyPartLot.Rows[0]["OrgID"]
     //                   , "Check.InvCode", dtCheck_QtyPartLot.Rows[0]["InvCode"]
     //                   , "Check.ProductCode", dtCheck_QtyPartLot.Rows[0]["ProductCode"]
     //                   , "Check.QtyTotalOK", dtCheck_QtyPartLot.Rows[0]["QtyTotalOK"]
     //                   , "Check.TotalQtyTotalOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalOK_PartLot"]
     //                   , "Check.QtyBlockOK", dtCheck_QtyPartLot.Rows[0]["QtyBlockOK"]
     //                   , "Check.TotalQtyBlockOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockOK_PartLot"]
     //                   , "Check.QtyAvailOK", dtCheck_QtyPartLot.Rows[0]["QtyAvailOK"]
     //                   , "Check.TotalQtyAvailOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailOK_PartLot"]
     //                   , "Check.QtyTotalNG", dtCheck_QtyPartLot.Rows[0]["QtyTotalNG"]
     //                   , "Check.TotalQtyTotalNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalNG_PartLot"]
     //                   , "Check.QtyBlockNG", dtCheck_QtyPartLot.Rows[0]["QtyBlockNG"]
     //                   , "Check.TotalQtyBlockNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockNG_PartLot"]
     //                   , "Check.QtyAvailNG", dtCheck_QtyPartLot.Rows[0]["QtyAvailNG"]
     //                   , "Check.TotalQtyAvailNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailNG_PartLot"]
     //                   , "Check.ConditionRaiseError","(t.QtyTotalOK <> f.TotalQtyTotalOK_PartLot or t.QtyBlockOK <> f.TotalQtyBlockOK_PartLot or t.QtyAvailOK <> f.TotalQtyAvailOK_PartLot or t.QtyTotalNG <> f.TotalQtyTotalNG_PartLot or t.QtyBlockNG <> f.TotalQtyBlockNG_PartLot or t.QtyAvailNG <> f.TotalQtyAvailNG_PartLot)"
     //                   , "Check.ErrRows.Count", dtCheck_QtyPartLot.Rows.Count
     //                   });

     //               throw CmUtils.CMyException.Raise(
     //                   TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyPartLot
     //                   , null
     //                   , alParamsCoupleError.ToArray()
     //                   );
     //           }
     //           ////
     //       }
            #endregion

            #region // Inv_InventoryBalanceSerial: Save and Check.
      //      {
      //          //// Action:
      //          if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Add))
      //          {
      //              string strSqlExec = CmUtils.StringUtils.Replace(@"
						//    ---- Inv_InventoryBalanceSerial: Insert Rows.						   
						//    insert into Inv_InventoryBalanceSerial
						//    (
						//	    OrgID
						//	    , InvCode
						//	    , ProductCode
						//	    , SerialNo
						//	    , NetworkID
						//	    , ProductLotNo
						//	    , RefNo_Type
						//	    , RefNo_PK
						//	    , BlockStatus
						//	    , FlagNG
						//	    , LogLUDTimeUTC
						//	    , LogLUBy
						//    )
						//    select 
						//	    t.OrgID
						//	    , t.InvCode
						//	    , t.ProductCode
						//	    , t.SerialNo
	     //                       , '@nNetworkID'
						//	    , null --t.ProductLotNo
						//	    , t.RefNo_Type
						//	    , t.RefNo_PK
						//	    , t.BlockStatus
						//	    , t.FlagNG
						//	    , th.CreateDTimeUTC LogLUDTimeUTC
						//	    , th.CreateBy LogLUBy
						//    from #tbl_InventoryTransactionSerial t --//[mylock]
						//	    inner join #tbl_InventoryTransaction_Threshold th --//[mylock]
						//		    on (1=1)
						//    ;

						//    ---- Clear for Debug:
						//    --drop table zzzzClauseTableNameDBTempSerial;
						//"
      //                  , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
      //                  , "@nNetworkID", nNetworkID
      //                  );
      //              ////

      //              _cf.db.ExecQuery(strSqlExec);
      //              ////
      //          }
      //          else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Update))
      //          {
      //              ////
      //              string zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE = @"
						//	    t.LogLUDTimeUTC = th.CreateDTimeUTC
						//	    , t.LogLUBy = th.CreateBy
						//	    , t.RefNo_Type = f.RefNo_Type
						//	    , t.RefNo_PK = f.RefNo_PK
						//	    , t.BlockStatus = f.BlockStatus
						//	    , t.FlagNG = f.FlagNG
						//	    ";
      //              ////
      //              string strSqlExec = CmUtils.StringUtils.Replace(@"
						//    ---- Inv_InventoryBalanceSerial: Upd Rows.
						//    update t
						//    set
						//	    zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
						//    from Inv_InventoryBalanceSerial t --//[mylock]
						//	    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
						//		    on t.OrgID = f.OrgID
						//			    and t.InvCode = f.InvCode
						//			    and t.ProductCode = f.ProductCode
						//			    and t.SerialNo = f.SerialNo
						//	    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
						//		    on (1=1)
						//    ;

						//    ---- Clear for Debug:
						//    --drop table zzzzClauseTableNameDBTempSerial;
						//"
      //                  , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
      //                  , "zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE", zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
      //                  );
      //              ////

      //              _cf.db.ExecQuery(strSqlExec);
      //              ////
      //          }
      //          else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Delete))
      //          {
      //              ////
      //              string strSqlExec = CmUtils.StringUtils.Replace(@"
						//    ---- Inv_InventoryBalanceSerial: Delete Rows.
						//    delete t
						//    from Inv_InventoryBalanceSerial t --//[mylock]
						//	    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
						//		    on t.OrgID = f.OrgID
						//			    and t.InvCode = f.InvCode
						//			    and t.ProductCode = f.ProductCode
						//			    and t.SerialNo = f.SerialNo
						//    ;

						//    ---- Clear for Debug:
						//    --drop table zzzzClauseTableNameDBTempSerial;
						//"
      //                  , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
      //                  );
      //              ////

      //              _cf.db.ExecQuery(strSqlExec);
      //              ////
      //          }
      //          {
      //              //// Check:
      //              string strSqlCheck = CmUtils.StringUtils.Replace(@"
						//    ---- Inv_InventoryTransactionSerial:
						//    insert into Inv_InventoryTransactionSerial
						//    (
						//		NetworkID
						//		, OrgID
						//	    , InvCode
						//	    , ProductCode
						//	    , SerialNo
						//	    , RefNo_Type
						//	    , RefNo_PK
						//	    , BlockStatus
						//	    , FlagNG
						//	    , Remark
						//	    , CreateDTimeUTC
						//	    , CreateBy
						//	    , FunctionName
						//	    , RefType
						//	    , RefCode00
						//	    , RefCode01
						//	    , RefCode02
						//	    , RefCode03
						//	    , RefCode04
						//	    , RefCode05	
						//    )
						//    select 
	     //                       '@nNetworkID'
						//		, t.OrgID
						//	    , t.InvCode
						//	    , t.ProductCode
						//	    , t.SerialNo
						//	    , t.RefNo_Type
						//	    , t.RefNo_PK
						//	    , t.BlockStatus
						//	    , t.FlagNG
						//	    , '@strRemark' Remark
						//	    , t.CreateDTimeUTC
						//	    , t.CreateBy
						//	    , t.FunctionName
						//	    , t.RefType
						//	    , t.RefCode00
						//	    , t.RefCode01
						//	    , t.RefCode02
						//	    , t.RefCode03
						//	    , t.RefCode04
						//	    , t.RefCode05
						//    from zzzzClauseTableNameDBTempSerial t with(nolock)
						//	    inner join zzzzClauseTableNameDBTemp_Threshold th with(nolock)
						//		    on (1=1)
						//    ;

						//    ---- #tbl_Inv_InventoryBalanceSerial_Draft:
						//    select 
						//	    t.InvCode InvCode
						//	    , t.ProductCode ProductCode
						//	    , Count(
						//		    case 
						//			    when t.FlagNG = '@strFlagNG_Inactive' then t.SerialNo
						//			    else null
						//		    end  	
						//	    ) QtyTotalOK_Serial
						//	    , Count(
						//		    case 
						//			    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
						//			    else null 
						//		    end 
						//	    ) QtyBlockOK_Serial
						//	    , Count(
						//		    case 
						//			    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
						//			    else null 
						//		    end 
						//	    ) QtyAvailOK_Serial
						//	    , Count(
						//		    case 
						//			    when t.FlagNG = '@strFlagNG_Active' then t.SerialNo
						//			    else null
						//		    end  	
						//	    ) QtyTotalNG_Serial
						//	    , Count(
						//		    case 
						//			    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
						//			    else null 
						//		    end 
						//	    ) QtyBlockNG_Serial
						//	    , Count(
						//		    case 
						//			    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
						//			    else null 
						//		    end 
						//	    ) QtyAvailNG_Serial 
						//    into #tbl_Inv_InventoryBalanceSerial_Draft
						//    from Inv_InventoryBalanceSerial t with(nolock)
      //                          inner join zzzzClauseTableNameDBTemp f with(nolock)
      //                              on t.InvCode = f.InvCode
      //                                  and t.ProductCode = f.ProductCode
						//    group by
						//	    t.InvCode
						//	    , t.ProductCode
						//    ;

						//    ---- Check:
						//    select 
						//	    t.*
						//	    , f.QtyTotalOK_Serial
						//	    , f.QtyBlockOK_Serial
						//	    , f.QtyAvailOK_Serial
						//	    , f.QtyTotalNG_Serial
						//	    , f.QtyBlockNG_Serial
						//	    , f.QtyAvailNG_Serial
						//    from Inv_InventoryBalance t with(nolock)
						//	    inner join Mst_Product mpa with(nolock)
						//		    on t.ProductCode = mpa.ProductCode
						//	    inner join #tbl_Inv_InventoryBalanceSerial_Draft f with(nolock) 
						//		    on t.InvCode = f.InvCode
						//			    and t.ProductCode = f.ProductCode
      //                          inner join zzzzClauseTableNameDBTemp k with(nolock)
      //                              on t.InvCode = k.InvCode
      //                                  and t.ProductCode= k.ProductCode
						//    where (1=1)
						//	    and mpa.FlagSerial = '@strFlagInputSerial'
						//	    and (t.QtyTotalOK <> f.QtyTotalOK_Serial 
						//		    or t.QtyBlockOK <> f.QtyBlockOK_Serial 
						//		    or t.QtyAvailOK <> f.QtyAvailOK_Serial
						//		    or t.QtyTotalNG <> f.QtyTotalNG_Serial
						//		    or t.QtyBlockNG <> f.QtyBlockNG_Serial
						//		    or t.QtyAvailNG <> f.QtyAvailNG_Serial
						//	    )
						//    ;
					 //   "
      //                  , "@strFlagNG_Active", TConst.Flag.Active
      //                  , "@strFlagNG_Inactive", TConst.Flag.Inactive
      //                  , "@strFlagInputSerial", TConst.Flag.Active
      //                  , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
      //                  , "@strRemark", strInventoryTransactionAction
      //                  , "@nNetworkID", nNetworkID
      //                  );


      //              _cf.db.ExecQuery(strSqlCheck);
      //              ////
      //          }
      //      }
            #endregion

            #region //// Clear for Debug:
            {
                string strSql_ClearforDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzzClauseTableNameDBTemp_Threshold;
						drop table zzzzClauseTableNameDBTemp_Total;
						--drop table zzzzClauseTableNameDBTempLot_Total;
						--drop table zzzzClauseTableNameDBTempLot;
						--drop table zzzzClauseTableNameDBTempSerial;
						drop table zzzzClauseTableNameDBTemp;
						--drop table #tbl_Inv_InventoryBalanceLot_Draft;
						--drop table #tbl_Inv_InventoryBalanceSerial_Draft;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );


                _cf.db.ExecQuery(strSql_ClearforDebug);
                ////
            }
            #endregion
        }

        /// <summary>
        /// 20220513. Nâng cấp từ Inv_InventoryTransaction_Perform_New20200225
        /// Nâng cấp Quản lý giá hàng hoá tồn kho theo phương pháp Bình quân gia quyền
        /// </summary>
        public void Inv_InventoryTransaction_Perform_New20220513(
            ref ArrayList alParamsCoupleError
            , string strTableNameDBTemp
            , string strInventoryTransactionAction
            , string strInvInOutAction
            , object strCreateDTimeUTC
            , object strCreateBy
            , object dblMinQtyTotalOK
            , object dblMinQtyBlockOK
            , object dblMinQtyAvailOK
            , object dblMinQtyTotalNG
            , object dblMinQtyBlockNG
            , object dblMinQtyAvailNG
            )
        {
            #region // Upload and Check Input:
            {
                string strSqlCheckInput = CmUtils.StringUtils.Replace(@"
					    select
						    t.*
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    where (1=1)
						    and t.QtyChTotalOK = 0.0
						    and t.QtyChBlockOK = 0.0
						    and t.QtyChTotalNG = 0.0
						    and t.QtyChBlockNG = 0.0
					    ;
                        select t.* from zzzzClauseTableNameDBTemp t --//[mylock]
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );

                DataTable dtCheckInput = _cf.db.ExecQuery(strSqlCheckInput).Tables[0];
                ////
                if (dtCheckInput.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.InvCode", dtCheckInput.Rows[0]["InvCode"].ToString()
                        , "Check.ProductCode", dtCheckInput.Rows[0]["ProductCode"].ToString()
                        , "Check.QtyChTotalOK", dtCheckInput.Rows[0]["QtyChTotalOK"].ToString()
                        , "Check.QtyChBlockOK", dtCheckInput.Rows[0]["QtyChBlockOK"].ToString()
                        , "Check.QtyChTotalNG", dtCheckInput.Rows[0]["QtyChTotalNG"].ToString()
                        , "Check.QtyChBlockNG", dtCheckInput.Rows[0]["QtyChBlockNG"].ToString()
                        , "Check.ErrRows.Count", dtCheckInput.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyAllZero
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                string strSqlGen_InvTransaction_Threshold = CmUtils.StringUtils.Replace(@"
                        ---- zzzzClauseTableNameDBTemp_Threshold: Gen.
                        select
	                         Cast(null as nvarchar(100)) CreateDTimeUTC
	                         , Cast(null as nvarchar(100)) CreateBy
	                         , Cast(null as float) MinQtyTotalOK
	                         , Cast(null as float) MinQtyBlockOK
	                         , Cast(null as float) MinQtyAvailOK
	                         , Cast(null as float) MinQtyTotalNG
	                         , Cast(null as float) MinQtyBlockNG
	                         , Cast(null as float) MinQtyAvailNG
                        into zzzzClauseTableNameDBTemp_Threshold
                        where (0=1)
                        ;

                        ---- Insert:
                        insert zzzzClauseTableNameDBTemp_Threshold(
                            CreateDTimeUTC
                            , CreateBy
                            , MinQtyTotalOK
                            , MinQtyBlockOK
                            , MinQtyAvailOK
                            , MinQtyTotalNG
                            , MinQtyBlockNG
                            , MinQtyAvailNG
                        )
                        values(
                            '@strCreateDTimeSv'
                            , '@strCreateBySv'
                            , dblMinQtyTotalOK
                            , dblMinQtyBlockOK
                            , dblMinQtyAvailOK
                            , dblMinQtyTotalNG
                            , dblMinQtyBlockNG
                            , dblMinQtyAvailNG
                        );

                        select t.* from zzzzClauseTableNameDBTemp_Threshold t --//[mylock]

                        ---- 
				    "
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strCreateDTimeSv", strCreateDTimeUTC
                    , "@strCreateBySv", strCreateBy
                    , "dblMinQtyTotalOK", dblMinQtyTotalOK
                    , "dblMinQtyBlockOK", dblMinQtyBlockOK
                    , "dblMinQtyAvailOK", dblMinQtyAvailOK
                    , "dblMinQtyTotalNG", dblMinQtyTotalNG
                    , "dblMinQtyBlockNG", dblMinQtyBlockNG
                    , "dblMinQtyAvailNG", dblMinQtyAvailNG
                    );
                /////

                DataTable dtGen_InvTransaction_Threshold = _cf.db.ExecQuery(strSqlGen_InvTransaction_Threshold).Tables[0];
                ////
            }
            #endregion

            #region // Inv_InventoryBalance: Save and Check.
            {
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
					    ---- Inv_InventoryTransaction_Total:
					    select 
						    t.OrgID
						    , t.InvCode InvCode
						    , t.ProductCode ProductCode
						    --, t.NetworkID NetworkID
						    --, t.MST MST
						    , Sum(t.QtyChTotalOK) QtyChTotalOK
						    , Sum(t.QtyChBlockOK) QtyChBlockOK
						    , Sum(t.QtyChTotalNG) QtyChTotalNG
						    , Sum(t.QtyChBlockNG) QtyChBlockNG
					    into zzzzClauseTableNameDBTemp_Total
					    from zzzzClauseTableNameDBTemp t --//[mylock]
					    group by
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    --, t.NetworkID 
						    --, t.MST 
					    ;
					
					    ---- Inv_InventoryBalance: Insert BlankRecord.
					    insert into Inv_InventoryBalance
					    (
							OrgID
							, InvCode
							, ProductCode
							, NetworkID
							, QtyTotalOK
							, QtyBlockOK
							, QtyAvailOK
							, QtyTotalNG
							, QtyBlockNG
							, QtyAvailNG
							, UPInv
							, TotalValInv
							, LogLUDTimeUTC
							, LogLUBy
					    )
					    select
						    t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , 0.0 QtyTotalOK
						    , 0.0 QtyBlockOK
						    , 0.0 QtyAvailOK
						    , 0.0 QtyTotalNG
						    , 0.0 QtyBlockNG
						    , 0.0 QtyAvailNG
							, 0.0 UPInv
							, 0.0 TotalValInv
						    , th.CreateDTimeUTC
						    , th.CreateBy
					    from zzzzClauseTableNameDBTemp_Total t --//[mylock]
						    left join Inv_InventoryBalance iib --//[mylock]
							    on t.OrgID = iib.OrgID
                                    and t.InvCode = iib.InvCode
                                    and t.ProductCode = iib.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (iib.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
					    ;

					    ---- Inv_InventoryTransaction:
					    insert into Inv_InventoryTransaction
					    (
						    OrgID
						    , InvCode
						    , ProductCode
						    , NetworkID
						    , QtyChTotalOK
						    , QtyChBlockOK
						    , QtyChTotalNG
						    , QtyChBlockNG
							, UPInv
							, TotalValInv
						    , Remark
						    , CreateDTimeUTC
						    , CreateBy
						    , FunctionName
						    , RefType
						    , RefCode00
						    , RefCode01
						    , RefCode02
						    , RefCode03
						    , RefCode04
						    , RefCode05
					    )
					    select 
						   t.OrgID
						    , t.InvCode
						    , t.ProductCode
						    , '@nNetworkID' --t.NetworkID 
						    , t.QtyChTotalOK
						    , t.QtyChBlockOK
						    , t.QtyChTotalNG
						    , t.QtyChBlockNG
							, t.UPInv
							, t.TotalValInv
						    , null Remark
						    , t.CreateDTimeUTC
						    , t.CreateBy
						    , t.FunctionName
						    , t.RefType
						    , t.RefCode00
						    , t.RefCode01
						    , t.RefCode02
						    , t.RefCode03
						    , t.RefCode04
						    , t.RefCode05
					    from zzzzClauseTableNameDBTemp t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Inv_InventoryBalance: Upd.
					    update t
					    set 
						    t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
						    , t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
						    , t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
						    , t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
						    , t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
						    , t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
						    , t.UPInv = g.UPInv
						    , t.TotalValInv = g.TotalValInv
						    , t.LogLUDTimeUTC = th.CreateDTimeUTC
						    , t.LogLUBy = th.CreateBy
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
							        and t.InvCode = f.InvCode 
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp g --//[mylock]
							    on t.OrgID = g.OrgID 
							        and t.InvCode = g.InvCode 
                                    and t.ProductCode = g.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    ;

					    ---- Inv_InventoryBalance: Upd UPInv. -- Nhậpkho: Giá hàng tồn kho: chung cho tất cả các kho
					    update t
					    set 
						    t.UPInv = g.UPInv
						    , t.TotalValInv = g.UPInv * t.QtyTotalOK -- 20230105. HuongTTT.
						    , t.LogLUDTimeUTC = th.CreateDTimeUTC
						    , t.LogLUBy = th.CreateBy
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp g --//[mylock]
							    on t.OrgID = g.OrgID 
                                    and t.ProductCode = g.ProductCode
                                    and '@strInvInOutAction' = 'IN'
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
                    
					    ;

					    ---- Check:
					    select 
						    t.*
						    , th.MinQtyTotalOK
						    , th.MinQtyBlockOK
						    , th.MinQtyAvailOK
						    , th.MinQtyTotalNG
						    , th.MinQtyBlockNG
						    , th.MinQtyAvailNG
					    from Inv_InventoryBalance t --//[mylock]
						    inner join zzzzClauseTableNameDBTemp_Total f --//[mylock]
							    on t.OrgID = f.OrgID 
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
							    on (1=1)
					    where (1=1)
						    and (
                                    t.QtyTotalOK < th.MinQtyTotalOK 
                                        or t.QtyBlockOK < th.MinQtyBlockOK 
                                        or t.QtyAvailOK < th.MinQtyAvailOK 
                                        or t.QtyTotalNG < th.MinQtyTotalNG 
                                        or t.QtyBlockNG < th.MinQtyBlockNG 
                                        or t.QtyAvailNG < th.MinQtyAvailNG
                            ) 
					    ;

					    ---- Clear for Debug:
					    --drop table zzzzClauseTableNameDBTemp_Threshold;
					    --drop table zzzzClauseTableNameDBTemp_Total;
					    --drop table zzzzClauseTableNameDBTemp;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@nNetworkID", nNetworkID
                    , "@strInvInOutAction", strInvInOutAction
                    );

                DataTable dtCheckResult = _cf.db.ExecQuery(strSqlExec).Tables[0];
                ////
                if (dtCheckResult.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheckResult.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheckResult.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheckResult.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheckResult.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheckResult.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheckResult.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheckResult.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyBlockOK", dtCheckResult.Rows[0]["MinQtyBlockOK"]
                        , "Check.MinQtyAvailOK", dtCheckResult.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheckResult.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheckResult.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheckResult.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheckResult.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheckResult.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheckResult.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheckResult.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyProductLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////

            }
            #endregion

            #region // Inv_InventoryBalanceLot: Save and Check.
            {
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						---- Inv_InventoryBalanceLot_Total:
						select 
							t.OrgID OrgID
							, t.InvCode InvCode
							, t.ProductCode ProductCode
							, t.ProductLotNo ProductLotNo
							, Sum(t.QtyChTotalOK) QtyChTotalOK
							, Sum(t.QtyChBlockOK) QtyChBlockOK
							, Sum(t.QtyChTotalNG) QtyChTotalNG
							, Sum(t.QtyChBlockNG) QtyChBlockNG
						into zzzzClauseTableNameDBTempLot_Total
						from zzzzClauseTableNameDBTempLot t --//[mylock]
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
							, t.ProductLotNo
						;

						---- Inv_InventoryBalance: Insert BlankRecord.
						insert into Inv_InventoryBalanceLot
						(
	                        OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , NetworkID
	                        , QtyTotalOK
	                        , QtyBlockOK
	                        , QtyAvailOK
	                        , QtyTotalNG
	                        , QtyBlockNG
	                        , QtyAvailNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , LogLUDTimeUTC
	                        , LogLUBy
						)
						select distinct
	                        t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
	                        , '@nNetworkID'
							, 0.0 QtyTotalOK
							, 0.0 QtyBlockOK
							, 0.0 QtyAvailOK
							, 0.0 QtyTotalNG
							, 0.0 QtyBlockNG
							, 0.0 QtyAvailNG
							, f.ProductionDate
							, f.ExpiredDate
							, f.ValDateExpired
							, th.CreateDTimeUTC
							, th.CreateBy
						from zzzzClauseTableNameDBTempLot_Total t --//[mylock]
							left join Inv_InventoryBalanceLot iibl --//[mylock]
								on t.OrgID = iibl.OrgID
									and t.InvCode = iibl.InvCode
									and t.ProductCode = iibl.ProductCode
									and t.ProductLotNo = iibl.ProductLotNo
							inner join zzzzClauseTableNameDBTempLot f --//[mylock]
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo 
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (iibl.InvCode is null) -- Sử dụng kỹ thuật lọc ngược.
						;

						---- Inv_InventoryTransactionLot:
						insert into Inv_InventoryTransactionLot
						(
	                        NetworkID
	                        , OrgID
	                        , InvCode
	                        , ProductCode
	                        , ProductLotNo
	                        , QtyChTotalOK
	                        , QtyChBlockOK
	                        , QtyChTotalNG
	                        , QtyChBlockNG
	                        , ProductionDate
	                        , ExpiredDate
	                        , ValDateExpired
	                        , Remark
	                        , CreateDTimeUTC
	                        , CreateBy
	                        , FunctionName
	                        , RefType
	                        , RefCode00
	                        , RefCode01
	                        , RefCode02
	                        , RefCode03
	                        , RefCode04
	                        , RefCode05
						)
						select 
	                        '@nNetworkID'
	                        , t.OrgID
	                        , t.InvCode
	                        , t.ProductCode
	                        , t.ProductLotNo
	                        , t.QtyChTotalOK
	                        , t.QtyChBlockOK
	                        , t.QtyChTotalNG
	                        , t.QtyChBlockNG
	                        , t.ProductionDate
	                        , t.ExpiredDate
	                        , t.ValDateExpired
							, '' --t.Remark
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.FunctionName
							, t.RefType
							, t.RefCode00
							, t.RefCode01
							, t.RefCode02
							, t.RefCode03
							, t.RefCode04
							, t.RefCode05
						from zzzzClauseTableNameDBTempLot t --//[mylock]
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						;

						---- Inv_InventoryBalanceLot: Upd.
						update t
						set 
							t.QtyTotalOK = t.QtyTotalOK + f.QtyChTotalOK 
							, t.QtyBlockOK = t.QtyBlockOK + f.QtyChBlockOK
							, t.QtyAvailOK = t.QtyTotalOK + f.QtyChTotalOK  - t.QtyBlockOK - f.QtyChBlockOK
							, t.QtyTotalNG = t.QtyTotalNG + f.QtyChTotalNG 
							, t.QtyBlockNG = t.QtyBlockNG + f.QtyChBlockNG
							, t.QtyAvailNG = t.QtyTotalNG + f.QtyChTotalNG - t.QtyBlockNG - f.QtyChBlockNG
							, t.ProductionDate = k.ProductionDate
							, t.ExpiredDate = k.ExpiredDate
							, t.LogLUDTimeUTC = th.CreateDTimeUTC
							, t.LogLUBy = th.CreateBy
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.OrgID = f.OrgID 
									and t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
							left join zzzzClauseTableNameDBTempLot k --//[mylock]
								on t.OrgID = k.OrgID 
									and t.InvCode = k.InvCode 
									and t.ProductCode = k.ProductCode
									and t.ProductLotNo = k.ProductLotNo
						where (1=1)
						;

						---- Check:
						select 
							t.*
							, th.MinQtyTotalOK
							, th.MinQtyBlockOK
							, th.MinQtyAvailOK
							, th.MinQtyTotalNG
							, th.MinQtyBlockNG
							, th.MinQtyAvailNG
						from Inv_InventoryBalanceLot t --//mustlock
							inner join zzzzClauseTableNameDBTempLot_Total f --//[mylock]
								on t.InvCode = f.InvCode 
									and t.ProductCode = f.ProductCode
									and t.ProductLotNo = f.ProductLotNo
							inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								on (1=1)
						where (1=1)
							and (t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG) 
						;

						---- #tbl_Inv_InventoryBalanceLot_Draft:
						select
							t.OrgID
							, t.InvCode
							, t.ProductCode
							--, t.ProductLotNo
							, Sum(t.QtyTotalOK) TotalQtyTotalOK_PartLot
							, Sum(t.QtyBlockOK) TotalQtyBlockOK_PartLot
							, Sum(t.QtyAvailOK) TotalQtyAvailOK_PartLot 
							, Sum(t.QtyTotalNG) TotalQtyTotalNG_PartLot
							, Sum(t.QtyBlockNG) TotalQtyBlockNG_PartLot
							, Sum(t.QtyAvailNG) TotalQtyAvailNG_PartLot
						into #tbl_Inv_InventoryBalanceLot_Draft	
						from Inv_InventoryBalanceLot t --//[mylock]
                            inner join zzzzClauseTableNameDBTemp f --//[mylock]
                                on t.InvCode = f.InvCode
                                    and t.InvCode = f.InvCode
                                    and t.ProductCode = f.ProductCode
						where (1=1)
						group by
							t.OrgID
							, t.InvCode
							, t.ProductCode
						;

						---- Check:
						select 
							t.*
							, IsNull(f.TotalQtyTotalOK_PartLot, 0.0) TotalQtyTotalOK_PartLot
							, IsNull(f.TotalQtyBlockOK_PartLot, 0.0) TotalQtyBlockOK_PartLot
							, IsNull(f.TotalQtyAvailOK_PartLot, 0.0) TotalQtyAvailOK_PartLot
							, IsNull(f.TotalQtyTotalNG_PartLot, 0.0) TotalQtyTotalNG_PartLot
							, IsNull(f.TotalQtyBlockNG_PartLot, 0.0) TotalQtyBlockNG_PartLot
							, IsNull(f.TotalQtyAvailNG_PartLot, 0.0) TotalQtyAvailNG_PartLot
						from Inv_InventoryBalance t --//[mylock]
							inner join Mst_Product mpa --//[mylock]
								on t.OrgID = mpa.OrgID
								    and t.ProductCode = mpa.ProductCode
							inner join #tbl_Inv_InventoryBalanceLot_Draft f --//[mylock] 
								on t.OrgID = f.OrgID
									and t.InvCode = f.InvCode
									and t.ProductCode = f.ProductCode
						where (1=1)
							and mpa.FlagLot = '@strFlagInputLot'
							and ((t.QtyTotalOK - IsNull(f.TotalQtyTotalOK_PartLot, 0.0)) > @dblDefault_Epsilon 
								or (t.QtyBlockOK - IsNull(f.TotalQtyBlockOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailOK - IsNull(f.TotalQtyAvailOK_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyTotalNG - IsNull(f.TotalQtyTotalNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyBlockNG - IsNull(f.TotalQtyBlockNG_PartLot, 0.0)) > @dblDefault_Epsilon
								or (t.QtyAvailNG - IsNull(f.TotalQtyAvailNG_PartLot, 0.0)) > @dblDefault_Epsilon
								)
					;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    , "@strFlagInputLot", TConst.Flag.Active
                    , "@dblDefault_Epsilon", TConst.BizMix.Default_Epsilon
                    , "@nNetworkID", nNetworkID
                    );


                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

                DataTable dtCheck_QtyChangeOverThreshold = dsExec.Tables[dsExec.Tables.Count - 2];
                DataTable dtCheck_QtyPartLot = dsExec.Tables[dsExec.Tables.Count - 1];
                ////
                if (dtCheck_QtyChangeOverThreshold.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyChangeOverThreshold.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyChangeOverThreshold.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductCode"]
                        , "Check.ProductLotNo", dtCheck_QtyChangeOverThreshold.Rows[0]["ProductLotNo"]
                        , "Check.QtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalOK"]
                        , "Check.MinQtyTotalOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalOK"]
                        , "Check.QtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockOK"]
                        , "Check.MinQtyBlockOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockOK"]
                        , "Check.QtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailOK"]
                        , "Check.MinQtyAvailOK", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailOK"]
                        , "Check.QtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyTotalNG"]
                        , "Check.MinQtyTotalNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyTotalNG"]
                        , "Check.QtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyBlockNG"]
                        , "Check.MinQtyBlockNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyBlockNG"]
                        , "Check.QtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["QtyAvailNG"]
                        , "Check.MinQtyAvailNG", dtCheck_QtyChangeOverThreshold.Rows[0]["MinQtyAvailNG"]
                        , "Check.ConditionRaiseError", "(t.QtyTotalOK < th.MinQtyTotalOK or t.QtyBlockOK < th.MinQtyBlockOK or t.QtyAvailOK < th.MinQtyAvailOK or t.QtyTotalNG < th.MinQtyTotalNG or t.QtyBlockNG < th.MinQtyBlockNG or t.QtyAvailNG < th.MinQtyAvailNG)"
                        , "Check.ErrRows.Count", dtCheck_QtyChangeOverThreshold.Rows.Count
                        });


                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_QtyChangeOverThreshold
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (dtCheck_QtyPartLot.Rows.Count > 0)
                {

                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", dtCheck_QtyPartLot.Rows[0]["OrgID"]
                        , "Check.InvCode", dtCheck_QtyPartLot.Rows[0]["InvCode"]
                        , "Check.ProductCode", dtCheck_QtyPartLot.Rows[0]["ProductCode"]
                        , "Check.QtyTotalOK", dtCheck_QtyPartLot.Rows[0]["QtyTotalOK"]
                        , "Check.TotalQtyTotalOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalOK_PartLot"]
                        , "Check.QtyBlockOK", dtCheck_QtyPartLot.Rows[0]["QtyBlockOK"]
                        , "Check.TotalQtyBlockOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockOK_PartLot"]
                        , "Check.QtyAvailOK", dtCheck_QtyPartLot.Rows[0]["QtyAvailOK"]
                        , "Check.TotalQtyAvailOK_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailOK_PartLot"]
                        , "Check.QtyTotalNG", dtCheck_QtyPartLot.Rows[0]["QtyTotalNG"]
                        , "Check.TotalQtyTotalNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyTotalNG_PartLot"]
                        , "Check.QtyBlockNG", dtCheck_QtyPartLot.Rows[0]["QtyBlockNG"]
                        , "Check.TotalQtyBlockNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyBlockNG_PartLot"]
                        , "Check.QtyAvailNG", dtCheck_QtyPartLot.Rows[0]["QtyAvailNG"]
                        , "Check.TotalQtyAvailNG_PartLot", dtCheck_QtyPartLot.Rows[0]["TotalQtyAvailNG_PartLot"]
                        , "Check.ConditionRaiseError","(t.QtyTotalOK <> f.TotalQtyTotalOK_PartLot or t.QtyBlockOK <> f.TotalQtyBlockOK_PartLot or t.QtyAvailOK <> f.TotalQtyAvailOK_PartLot or t.QtyTotalNG <> f.TotalQtyTotalNG_PartLot or t.QtyBlockNG <> f.TotalQtyBlockNG_PartLot or t.QtyAvailNG <> f.TotalQtyAvailNG_PartLot)"
                        , "Check.ErrRows.Count", dtCheck_QtyPartLot.Rows.Count
                        });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inv_InventoryTransaction_Perform_InvalidQtyPartLot
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // Inv_InventoryBalanceSerial: Save and Check.
            {
                //// Action:
                if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Add))
                {
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Insert Rows.						   
						    insert into Inv_InventoryBalanceSerial
						    (
							    OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , NetworkID
							    , ProductLotNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , LogLUDTimeUTC
							    , LogLUBy
						    )
						    select 
							    t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
	                            , '@nNetworkID'
							    , null --t.ProductLotNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , th.CreateDTimeUTC LogLUDTimeUTC
							    , th.CreateBy LogLUBy
						    from #tbl_InventoryTransactionSerial t --//[mylock]
							    inner join #tbl_InventoryTransaction_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "@nNetworkID", nNetworkID
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Update))
                {
                    ////
                    string zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE = @"
							    t.LogLUDTimeUTC = th.CreateDTimeUTC
							    , t.LogLUBy = th.CreateBy
							    , t.RefNo_Type = f.RefNo_Type
							    , t.RefNo_PK = f.RefNo_PK
							    , t.BlockStatus = f.BlockStatus
							    , t.FlagNG = f.FlagNG
							    ";
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Upd Rows.
						    update t
						    set
							    zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.OrgID = f.OrgID
									    and t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
							    inner join zzzzClauseTableNameDBTemp_Threshold th --//[mylock]
								    on (1=1)
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE", zzB_Update_Inv_InventoryBalanceSerial_ClauseSet_zzE
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                else if (CmUtils.StringUtils.StringEqual(strInventoryTransactionAction, TConst.InventoryTransactionAction.Delete))
                {
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryBalanceSerial: Delete Rows.
						    delete t
						    from Inv_InventoryBalanceSerial t --//[mylock]
							    inner join zzzzClauseTableNameDBTempSerial f --//[mylock]
								    on t.OrgID = f.OrgID
									    and t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
									    and t.SerialNo = f.SerialNo
						    ;

						    ---- Clear for Debug:
						    --drop table zzzzClauseTableNameDBTempSerial;
						"
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        );
                    ////

                    _cf.db.ExecQuery(strSqlExec);
                    ////
                }
                {
                    //// Check:
                    string strSqlCheck = CmUtils.StringUtils.Replace(@"
						    ---- Inv_InventoryTransactionSerial:
						    insert into Inv_InventoryTransactionSerial
						    (
								NetworkID
								, OrgID
							    , InvCode
							    , ProductCode
							    , SerialNo
							    , RefNo_Type
							    , RefNo_PK
							    , BlockStatus
							    , FlagNG
							    , Remark
							    , CreateDTimeUTC
							    , CreateBy
							    , FunctionName
							    , RefType
							    , RefCode00
							    , RefCode01
							    , RefCode02
							    , RefCode03
							    , RefCode04
							    , RefCode05	
						    )
						    select 
	                            '@nNetworkID'
								, t.OrgID
							    , t.InvCode
							    , t.ProductCode
							    , t.SerialNo
							    , t.RefNo_Type
							    , t.RefNo_PK
							    , t.BlockStatus
							    , t.FlagNG
							    , '@strRemark' Remark
							    , t.CreateDTimeUTC
							    , t.CreateBy
							    , t.FunctionName
							    , t.RefType
							    , t.RefCode00
							    , t.RefCode01
							    , t.RefCode02
							    , t.RefCode03
							    , t.RefCode04
							    , t.RefCode05
						    from zzzzClauseTableNameDBTempSerial t with(nolock)
							    inner join zzzzClauseTableNameDBTemp_Threshold th with(nolock)
								    on (1=1)
						    ;

						    ---- #tbl_Inv_InventoryBalanceSerial_Draft:
						    select 
							    t.InvCode InvCode
							    , t.ProductCode ProductCode
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Inactive' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockOK_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Inactive' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailOK_Serial
							    , Count(
								    case 
									    when t.FlagNG = '@strFlagNG_Active' then t.SerialNo
									    else null
								    end  	
							    ) QtyTotalNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is not null and t.RefNo_PK is not null) then t.SerialNo
									    else null 
								    end 
							    ) QtyBlockNG_Serial
							    , Count(
								    case 
									    when (t.FlagNG = '@strFlagNG_Active' and t.RefNo_Type is null and t.RefNo_PK is null) then t.SerialNo
									    else null 
								    end 
							    ) QtyAvailNG_Serial 
						    into #tbl_Inv_InventoryBalanceSerial_Draft
						    from Inv_InventoryBalanceSerial t with(nolock)
                                inner join zzzzClauseTableNameDBTemp f with(nolock)
                                    on t.InvCode = f.InvCode
                                        and t.ProductCode = f.ProductCode
						    group by
							    t.InvCode
							    , t.ProductCode
						    ;

						    ---- Check:
						    select 
							    t.*
							    , f.QtyTotalOK_Serial
							    , f.QtyBlockOK_Serial
							    , f.QtyAvailOK_Serial
							    , f.QtyTotalNG_Serial
							    , f.QtyBlockNG_Serial
							    , f.QtyAvailNG_Serial
						    from Inv_InventoryBalance t with(nolock)
							    inner join Mst_Product mpa with(nolock)
								    on t.ProductCode = mpa.ProductCode
							    inner join #tbl_Inv_InventoryBalanceSerial_Draft f with(nolock) 
								    on t.InvCode = f.InvCode
									    and t.ProductCode = f.ProductCode
                                inner join zzzzClauseTableNameDBTemp k with(nolock)
                                    on t.InvCode = k.InvCode
                                        and t.ProductCode= k.ProductCode
						    where (1=1)
							    and mpa.FlagSerial = '@strFlagInputSerial'
							    and (t.QtyTotalOK <> f.QtyTotalOK_Serial 
								    or t.QtyBlockOK <> f.QtyBlockOK_Serial 
								    or t.QtyAvailOK <> f.QtyAvailOK_Serial
								    or t.QtyTotalNG <> f.QtyTotalNG_Serial
								    or t.QtyBlockNG <> f.QtyBlockNG_Serial
								    or t.QtyAvailNG <> f.QtyAvailNG_Serial
							    )
						    ;
					    "
                        , "@strFlagNG_Active", TConst.Flag.Active
                        , "@strFlagNG_Inactive", TConst.Flag.Inactive
                        , "@strFlagInputSerial", TConst.Flag.Active
                        , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                        , "@strRemark", strInventoryTransactionAction
                        , "@nNetworkID", nNetworkID
                        );


                    _cf.db.ExecQuery(strSqlCheck);
                    ////
                }
            }
            #endregion

            #region //// Clear for Debug:
            {
                string strSql_ClearforDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table zzzzClauseTableNameDBTemp_Threshold;
						drop table zzzzClauseTableNameDBTemp_Total;
						drop table zzzzClauseTableNameDBTempLot_Total;
						drop table zzzzClauseTableNameDBTempLot;
						drop table zzzzClauseTableNameDBTempSerial;
						drop table zzzzClauseTableNameDBTemp;
						drop table #tbl_Inv_InventoryBalanceLot_Draft;
						drop table #tbl_Inv_InventoryBalanceSerial_Draft;
					"
                    , "zzzzClauseTableNameDBTemp", strTableNameDBTemp
                    );


                _cf.db.ExecQuery(strSql_ClearforDebug);
                ////
            }
            #endregion
        }
        #endregion

        #region // Inv_InventoryBalance:
        public DataSet WAS_Inv_InventoryBalance_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Inv_InventoryBalance objRQ_Inv_InventoryBalance
			////
			, out RT_Inv_InventoryBalance objRT_Inv_InventoryBalance
			)
		{
			#region // Temp:
			string strTid = objRQ_Inv_InventoryBalance.Tid;
			objRT_Inv_InventoryBalance = new RT_Inv_InventoryBalance();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalance.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Inv_InventoryBalance_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalance_Get;
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
				List<Inv_InventoryBalance> lst_Inv_InventoryBalance = new List<Inv_InventoryBalance>();
				//List<Inv_InventoryBalanceLot> lst_Inv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
				//List<Inv_InventoryBalanceSerial> lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
				/////
				bool bGet_Inv_InventoryBalance = (objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalance != null && objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalance.Length > 0);
				//bool bGet_Inv_InventoryBalanceLot = (objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceLot != null && objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceLot.Length > 0);
				//bool bGet_Inv_InventoryBalanceSerial = (objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceSerial != null && objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceSerial.Length > 0);
				#endregion

				#region // WS_Inv_InventoryBalance_Get:
				mdsResult = Inv_InventoryBalance_Get(
					objRQ_Inv_InventoryBalance.Tid // strTid
					, objRQ_Inv_InventoryBalance.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalance.GwPassword // strGwPassword
					, objRQ_Inv_InventoryBalance.WAUserCode // strUserCode
					, objRQ_Inv_InventoryBalance.WAUserPassword // strUserPassword
					, objRQ_Inv_InventoryBalance.AccessToken // strAccessToken
					, objRQ_Inv_InventoryBalance.NetworkID // strNetworkID
					, objRQ_Inv_InventoryBalance.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Inv_InventoryBalance.Ft_RecordStart // strFt_RecordStart
					, objRQ_Inv_InventoryBalance.Ft_RecordCount // strFt_RecordCount
					, objRQ_Inv_InventoryBalance.Ft_WhereClause // strFt_WhereClause
																//// Return:
					, objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalance // Rt_Cols_Inv_InventoryBalance
					//, objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceLot // Rt_Cols_Inv_InventoryBalanceLot
					//, objRQ_Inv_InventoryBalance.Rt_Cols_Inv_InventoryBalanceSerial // Rt_Cols_Inv_InventoryBalanceSerial
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Inv_InventoryBalance.MySummaryTable = lst_MySummaryTable[0];
					////
					////
					if (bGet_Inv_InventoryBalance)
					{
						////
						DataTable dt_Inv_InventoryBalance = mdsResult.Tables["Inv_InventoryBalance"].Copy();
						lst_Inv_InventoryBalance = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalance>(dt_Inv_InventoryBalance);
						objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalance = lst_Inv_InventoryBalance;
					}
					////
					//if (bGet_Inv_InventoryBalanceLot)
					//{
					//	////
					//	DataTable dt_Inv_InventoryBalanceLot = mdsResult.Tables["Inv_InventoryBalanceLot"].Copy();
					//	lst_Inv_InventoryBalanceLot = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalanceLot>(dt_Inv_InventoryBalanceLot);
					//	objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceLot = lst_Inv_InventoryBalanceLot;
					//}
					//////
					//if (bGet_Inv_InventoryBalanceSerial)
					//{
					//	////
					//	DataTable dt_Inv_InventoryBalanceSerial = mdsResult.Tables["Inv_InventoryBalanceSerial"].Copy();
					//	lst_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalanceSerial>(dt_Inv_InventoryBalanceSerial);
					//	objRT_Inv_InventoryBalance.Lst_Inv_InventoryBalanceSerial = lst_Inv_InventoryBalanceSerial;
					//}
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

		public DataSet Inv_InventoryBalance_Get(
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
			, string strRt_Cols_Inv_InventoryBalance
			//, string strRt_Cols_Inv_InventoryBalanceLot
			//, string strRt_Cols_Inv_InventoryBalanceSerial
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Inv_InventoryBalance_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalance_Get;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryBalance", strRt_Cols_Inv_InventoryBalance
				//, "strRt_Cols_Inv_InventoryBalanceLot", strRt_Cols_Inv_InventoryBalanceLot
				//, "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
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
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

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

				#region // Inv_InventoryBalance_GetX:
				DataSet dsGetData = new DataSet();
				{
					Inv_InventoryBalance_GetX(
						ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, strTid // strTid
						, strWAUserCode // strWAUserCode
										////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Inv_InventoryBalance // Rt_Cols_Inv_InventoryBalance
						//, strRt_Cols_Inv_InventoryBalanceLot // strRt_Cols_Inv_InventoryBalanceLot
						//, strRt_Cols_Inv_InventoryBalanceSerial // strRt_Cols_Inv_InventoryBalanceSerial
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

		public void Inv_InventoryBalance_GetX(
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
			, string strRt_Cols_Inv_InventoryBalance
			//, string strRt_Cols_Inv_InventoryBalanceLot
			//, string strRt_Cols_Inv_InventoryBalanceSerial
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			string strFunctionName = "Inv_InventoryBalance_GetX";
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_Inv_InventoryBalance", strRt_Cols_Inv_InventoryBalance
				//, "strRt_Cols_Inv_InventoryBalanceLot", strRt_Cols_Inv_InventoryBalanceLot
				//, "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Inv_InventoryBalance = (strRt_Cols_Inv_InventoryBalance != null && strRt_Cols_Inv_InventoryBalance.Length > 0);
			//bool bGet_Inv_InventoryBalanceLot = (strRt_Cols_Inv_InventoryBalanceLot != null && strRt_Cols_Inv_InventoryBalanceLot.Length > 0);
			//bool bGet_Inv_InventoryBalanceSerial = (strRt_Cols_Inv_InventoryBalanceSerial != null && strRt_Cols_Inv_InventoryBalanceSerial.Length > 0);
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
            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_InventoryBalance_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiif.OrgID
							, iiif.InvCode
							, iiif.ProductCode
						into #tbl_Inv_InventoryBalance_Filter_Draft
						from Inv_InventoryBalance iiif --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiif.OrgID = t_MstNNT_View.OrgID
                            inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                                on iiif.OrgID = t_mi_View.OrgID
                                    and iiif.InvCode = t_mi_View.InvCode
							--left join Inv_InventoryBalancelot iiifl --//[mylock]
								--on iiif.IF_InvInNo = iiifl.IF_InvInNo
							--left join Inv_InventoryBalanceSerial iiifs --//[mylock]
								--on iiif.IF_InvInNo = iiifs.IF_InvInNo
							inner join Mst_Inventory mi --//[mylock]
								on iiif.OrgID = mi.OrgID
									and iiif.InvCode = mi.InvCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiif.OrgID desc
								, iiif.InvCode desc
								, iiif.ProductCode desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventoryBalance_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventoryBalance_Filter:
						select
							t.*
						into #tbl_Inv_InventoryBalance_Filter
						from #tbl_Inv_InventoryBalance_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventoryBalance ------:
						zzB_Select_Inv_InventoryBalance_zzE
						--------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventoryBalance_Filter_Draft;
						--drop table #tbl_Inv_InventoryBalance_Filter;
					"
                );
			////
			string zzB_Select_Inv_InventoryBalance_zzE = "-- Nothing.";
			if (bGet_Inv_InventoryBalance)
			{
				#region // bGet_Inv_InventoryBalance:
				zzB_Select_Inv_InventoryBalance_zzE = CmUtils.StringUtils.Replace(@"
							---- Inv_InventoryBalance:
							select
								t.MyIdxSeq
								, iiif.*
							from #tbl_Inv_InventoryBalance_Filter t --//[mylock]
								inner join Inv_InventoryBalance iiif --//[mylock]
									on t.OrgID = iiif.OrgID
										and t.InvCode = iiif.InvCode
										and t.ProductCode = iiif.ProductCode
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
						, "Inv_InventoryBalance" // strTableNameDB
						, "Inv_InventoryBalance." // strPrefixStd
						, "iiif." // strPrefixAlias
						);
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_Inventory" // strTableNameDB
                        , "Mst_Inventory." // strPrefixStd
                        , "mi." // strPrefixAlias
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
				, "zzB_Select_Inv_InventoryBalance_zzE", zzB_Select_Inv_InventoryBalance_zzE
				//, "zzB_Select_Inv_InventoryBalanceLot_zzE", zzB_Select_Inv_InventoryBalanceLot_zzE
				//, "zzB_Select_Inv_InventoryBalanceSerial_zzE", zzB_Select_Inv_InventoryBalanceSerial_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Inv_InventoryBalance)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalance";
			}
			//if (bGet_Inv_InventoryBalanceLot)
			//{
			//	dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalanceLot";
			//}
			//if (bGet_Inv_InventoryBalanceSerial)
			//{
			//	dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalanceSerial";
			//}
			#endregion
		}
		#endregion

		#region // Inv_InventoryBalanceLot:
		public DataSet WAS_Inv_InventoryBalanceLot_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Inv_InventoryBalanceLot objRQ_Inv_InventoryBalanceLot
			////
			, out RT_Inv_InventoryBalanceLot objRT_Inv_InventoryBalanceLot
			)
		{
			#region // Temp:
			string strTid = objRQ_Inv_InventoryBalanceLot.Tid;
			objRT_Inv_InventoryBalanceLot = new RT_Inv_InventoryBalanceLot();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceLot.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Inv_InventoryBalanceLot_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceLot_Get;
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
				List<Inv_InventoryBalanceLot> lst_Inv_InventoryBalanceLot = new List<Inv_InventoryBalanceLot>();
				/////
				bool bGet_Inv_InventoryBalanceLot = (objRQ_Inv_InventoryBalanceLot.Rt_Cols_Inv_InventoryBalanceLot != null && objRQ_Inv_InventoryBalanceLot.Rt_Cols_Inv_InventoryBalanceLot.Length > 0);
				#endregion

				#region // WS_Inv_InventoryBalanceLot_Get:
				mdsResult = Inv_InventoryBalanceLot_Get(
					objRQ_Inv_InventoryBalanceLot.Tid // strTid
					, objRQ_Inv_InventoryBalanceLot.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalanceLot.GwPassword // strGwPassword
					, objRQ_Inv_InventoryBalanceLot.WAUserCode // strUserCode
					, objRQ_Inv_InventoryBalanceLot.WAUserPassword // strUserPassword
					, objRQ_Inv_InventoryBalanceLot.AccessToken // strAccessToken
					, objRQ_Inv_InventoryBalanceLot.NetworkID // strNetworkID
					, objRQ_Inv_InventoryBalanceLot.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Inv_InventoryBalanceLot.Ft_RecordStart // strFt_RecordStart
					, objRQ_Inv_InventoryBalanceLot.Ft_RecordCount // strFt_RecordCount
					, objRQ_Inv_InventoryBalanceLot.Ft_WhereClause // strFt_WhereClause
																   //// Return:
					, objRQ_Inv_InventoryBalanceLot.Rt_Cols_Inv_InventoryBalanceLot // Rt_Cols_Inv_InventoryBalanceLot
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Inv_InventoryBalanceLot.MySummaryTable = lst_MySummaryTable[0];
					////
					////
					if (bGet_Inv_InventoryBalanceLot)
					{
						////
						DataTable dt_Inv_InventoryBalanceLot = mdsResult.Tables["Inv_InventoryBalanceLot"].Copy();
						lst_Inv_InventoryBalanceLot = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalanceLot>(dt_Inv_InventoryBalanceLot);
						objRT_Inv_InventoryBalanceLot.Lst_Inv_InventoryBalanceLot = lst_Inv_InventoryBalanceLot;
					}
					////

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

		public DataSet Inv_InventoryBalanceLot_Get(
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
			, string strRt_Cols_Inv_InventoryBalanceLot
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Inv_InventoryBalanceLot_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceLot_Get;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryBalanceLot", strRt_Cols_Inv_InventoryBalanceLot
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
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

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

				#region // Inv_InventoryBalanceLot_GetX:
				DataSet dsGetData = new DataSet();
				{
					Inv_InventoryBalanceLot_GetX(
						ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, strTid // strTid
						, strWAUserCode // strWAUserCode
										////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Inv_InventoryBalanceLot // Rt_Cols_Inv_InventoryBalanceLot
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

		public void Inv_InventoryBalanceLot_GetX(
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
			, string strRt_Cols_Inv_InventoryBalanceLot
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			string strFunctionName = "Inv_InventoryBalanceLot_GetX";
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_Inv_InventoryBalanceLot", strRt_Cols_Inv_InventoryBalanceLot
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Inv_InventoryBalanceLot = (strRt_Cols_Inv_InventoryBalanceLot != null && strRt_Cols_Inv_InventoryBalanceLot.Length > 0);
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

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_InventoryBalanceLot_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiifl.OrgID
							, iiifl.InvCode
							, iiifl.ProductCode
							, iiifl.ProductLotNo
						into #tbl_Inv_InventoryBalanceLot_Filter_Draft
						from Inv_InventoryBalanceLot iiifl --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiifl.OrgID = t_MstNNT_View.OrgID
                            inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                                on iiifl.OrgID = t_mi_View.OrgID
                                    and iiifl.InvCode = t_mi_View.InvCode
							inner join Mst_Inventory mi --//[mylock]
								on iiifl.OrgID = mi.OrgID
									and iiifl.InvCode = mi.InvCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiifl.OrgID desc
								, iiifl.InvCode desc
								, iiifl.ProductCode desc
								, iiifl.ProductLotNo desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventoryBalanceLot_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventoryBalanceLot_Filter:
						select
							t.*
						into #tbl_Inv_InventoryBalanceLot_Filter
						from #tbl_Inv_InventoryBalanceLot_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventoryBalanceLot ------:
						zzB_Select_Inv_InventoryBalanceLot_zzE
						--------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventoryBalanceLot_Filter_Draft;
						--drop table #tbl_Inv_InventoryBalanceLot_Filter;
					"
                );
			////
			string zzB_Select_Inv_InventoryBalanceLot_zzE = "-- Nothing.";
			if (bGet_Inv_InventoryBalanceLot)
			{
				#region // bGet_Inv_InventoryBalanceLot:
				zzB_Select_Inv_InventoryBalanceLot_zzE = CmUtils.StringUtils.Replace(@"
							---- Inv_InventoryBalanceLot:
							select
								t.MyIdxSeq
								, iiifl.*
							from #tbl_Inv_InventoryBalanceLot_Filter t --//[mylock]
								inner join Inv_InventoryBalanceLot iiifl --//[mylock]
									on t.OrgID = iiifl.OrgID
										and t.InvCode = iiifl.InvCode
										and t.ProductCode = iiifl.ProductCode
										and t.ProductLotNo = iiifl.ProductLotNo
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
						, "Inv_InventoryBalanceLot" // strTableNameDB
						, "Inv_InventoryBalanceLot." // strPrefixStd
						, "iiifl." // strPrefixAlias
						);
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_Inventory" // strTableNameDB
                        , "Mst_Inventory." // strPrefixStd
                        , "mi." // strPrefixAlias
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
				, "zzB_Select_Inv_InventoryBalanceLot_zzE", zzB_Select_Inv_InventoryBalanceLot_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Inv_InventoryBalanceLot)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalanceLot";
			}
			#endregion
		}
		#endregion

		#region // Inv_InventoryBalanceSerial:
		public DataSet WAS_Inv_InventoryBalanceSerial_Get(
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
			string strFunctionName = "WAS_Inv_InventoryBalanceSerial_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Inv_InventoryBalanceSerial_Get;
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
				List<Inv_InventoryBalanceSerial> lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();
				/////
				bool bGet_Inv_InventoryBalanceSerial = (objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial != null && objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial.Length > 0);
				#endregion

				#region // WS_Inv_InventoryBalanceSerial_Get:
				mdsResult = Inv_InventoryBalanceSerial_Get(
					objRQ_Inv_InventoryBalanceSerial.Tid // strTid
					, objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
					, objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
					, objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
					, objRQ_Inv_InventoryBalanceSerial.AccessToken // strAccessToken
					, objRQ_Inv_InventoryBalanceSerial.NetworkID // strNetworkID
					, objRQ_Inv_InventoryBalanceSerial.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Inv_InventoryBalanceSerial.Ft_RecordStart // strFt_RecordStart
					, objRQ_Inv_InventoryBalanceSerial.Ft_RecordCount // strFt_RecordCount
					, objRQ_Inv_InventoryBalanceSerial.Ft_WhereClause // strFt_WhereClause
																	  //// Return:
					, objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial // Rt_Cols_Inv_InventoryBalanceSerial
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Inv_InventoryBalanceSerial.MySummaryTable = lst_MySummaryTable[0];
					////
					////
					if (bGet_Inv_InventoryBalanceSerial)
					{
						////
						DataTable dt_Inv_InventoryBalanceSerial = mdsResult.Tables["Inv_InventoryBalanceSerial"].Copy();
						lst_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToListof<Inv_InventoryBalanceSerial>(dt_Inv_InventoryBalanceSerial);
						objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial = lst_Inv_InventoryBalanceSerial;
					}
					////

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

		public DataSet Inv_InventoryBalanceSerial_Get(
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
			, string strRt_Cols_Inv_InventoryBalanceSerial
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "Inv_InventoryBalanceSerial_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Inv_InventoryBalanceSerial_Get;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
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
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

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

				#region // Inv_InventoryBalanceSerial_GetX:
				DataSet dsGetData = new DataSet();
				{
					Inv_InventoryBalanceSerial_GetX(
						ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, strTid // strTid
						, strWAUserCode // strWAUserCode
										////
						, strFt_RecordStart // strFt_RecordStart
						, strFt_RecordCount // strFt_RecordCount
						, strFt_WhereClause // strFt_WhereClause
											////
						, strRt_Cols_Inv_InventoryBalanceSerial // Rt_Cols_Inv_InventoryBalanceSerial
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

		public void Inv_InventoryBalanceSerial_GetX(
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
			, string strRt_Cols_Inv_InventoryBalanceSerial
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			string strFunctionName = "Inv_InventoryBalanceSerial_GetX";
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
				});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Inv_InventoryBalanceSerial = (strRt_Cols_Inv_InventoryBalanceSerial != null && strRt_Cols_Inv_InventoryBalanceSerial.Length > 0);
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

            zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
                strWAUserCode // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Inv_InventoryBalanceSerial_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, iiifl.OrgID
							, iiifl.InvCode
							, iiifl.ProductCode
							, iiifl.SerialNo
						into #tbl_Inv_InventoryBalanceSerial_Filter_Draft
						from Inv_InventoryBalanceSerial iiifl --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on iiifl.OrgID = t_MstNNT_View.OrgID
							inner join Mst_Inventory mi --//[mylock]
								on iiifl.OrgID = mi.OrgID
									and iiifl.InvCode = mi.InvCode
                            inner join #tbl_Mst_Inventory_ViewAbility t_mi_View --//[mylock]
                                on iiifl.OrgID = t_mi_View.OrgID
                                    and iiifl.InvCode = t_mi_View.InvCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by iiifl.OrgID desc
								, iiifl.InvCode desc
								, iiifl.ProductCode desc
								, iiifl.SerialNo desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Inv_InventoryBalanceSerial_Filter_Draft t --//[mylock]
						;

						---- #tbl_Inv_InventoryBalanceSerial_Filter:
						select
							t.*
						into #tbl_Inv_InventoryBalanceSerial_Filter
						from #tbl_Inv_InventoryBalanceSerial_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Inv_InventoryBalanceSerial ------:
						zzB_Select_Inv_InventoryBalanceSerial_zzE
						--------------------------------

						---- Clear for debug:
						--drop table #tbl_Inv_InventoryBalanceSerial_Filter_Draft;
						--drop table #tbl_Inv_InventoryBalanceSerial_Filter;
					"
                );
			////
			string zzB_Select_Inv_InventoryBalanceSerial_zzE = "-- Nothing.";
			if (bGet_Inv_InventoryBalanceSerial)
			{
				#region // bGet_Inv_InventoryBalanceSerial:
				zzB_Select_Inv_InventoryBalanceSerial_zzE = CmUtils.StringUtils.Replace(@"
							---- Inv_InventoryBalanceSerial:
							select
								t.MyIdxSeq
								, iiifl.*
							from #tbl_Inv_InventoryBalanceSerial_Filter t --//[mylock]
								inner join Inv_InventoryBalanceSerial iiifl --//[mylock]
									on t.OrgID = iiifl.OrgID
										and t.InvCode = iiifl.InvCode
										and t.ProductCode = iiifl.ProductCode
										and t.SerialNo = iiifl.SerialNo
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
						, "Inv_InventoryBalanceSerial" // strTableNameDB
						, "Inv_InventoryBalanceSerial." // strPrefixStd
						, "iiifl." // strPrefixAlias
						);
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_Inventory" // strTableNameDB
                        , "Mst_Inventory." // strPrefixStd
                        , "mi." // strPrefixAlias
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
				, "zzB_Select_Inv_InventoryBalanceSerial_zzE", zzB_Select_Inv_InventoryBalanceSerial_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Inv_InventoryBalanceSerial)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Inv_InventoryBalanceSerial";
			}
			#endregion
		}
		#endregion
	}
}
