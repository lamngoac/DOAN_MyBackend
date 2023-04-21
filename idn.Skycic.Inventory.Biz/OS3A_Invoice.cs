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



namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // OS3A_Invoice:
        private void OS3A_TVAN_InvoiceInvoice_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , string strFt_RecordStart
            , string strFt_RecordCount
            //// 
            , string strMST
            , string strInvoiceDateUTCFrom
            , string strInvoiceDateUTCTo
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "OS3A_TVAN_InvoiceInvoice_GetX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
				////
                , "strMST", strMST
                , "strInvoiceDateUTCFrom", strInvoiceDateUTCFrom
                , "strInvoiceDateUTCTo", strInvoiceDateUTCTo
                });
            #endregion

            #region // Check:
            //// Refine:
            strMST = TUtils.CUtils.StdParam(strMST);
            strInvoiceDateUTCFrom = TUtils.CUtils.StdDate(strInvoiceDateUTCFrom);
            strInvoiceDateUTCTo = TUtils.CUtils.StdDate(strInvoiceDateUTCTo);
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            /////
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                });
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Invoice_Invoice_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, ii.InvoiceCode
							, ii.CreateDTimeUTC
						into #tbl_Invoice_Invoice_Filter_Draft
						from Invoice_Invoice ii --//[mylock]
                            --inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                            --    on ii.MST = t_MstNNT_View.MST
							left join Mst_PaymentMethods mpm --//[mylock]
								on ii.PaymentMethodCode = mpm.PaymentMethodCode
							left join Mst_CustomerNNT mcnnt --//[mylock]
								on ii.CustomerNNTCode = mcnnt.CustomerNNTCode
									and ii.MST = mcnnt.MST
							left join Invoice_TempInvoice iti --//[mylock]
								on ii.TInvoiceCode = iti.TInvoiceCode
	                        left join Mst_SourceInvoice msi --//[mylock]
		                        on ii.SourceInvoiceCode = msi.SourceInvoiceCode
						where (1=1)
                            and ii.MST = '@strMST'
                            and ii.InvoiceDateUTC >= '@strInvoiceDateUTCFrom'
                            and ii.InvoiceDateUTC <= '@strInvoiceDateUTCTo'
                            and ii.FlagPushOutSite is null
                            and ii.InvoiceStatus in ('ISSUED', 'DELETED')
						order by ii.CreateDTimeUTC desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Invoice_Invoice_Filter_Draft t --//[mylock]
						;

						---- #tbl_Invoice_Invoice_Filter:
						select
							t.*
						into #tbl_Invoice_Invoice_Filter
						from #tbl_Invoice_Invoice_Filter_Draft t --//[mylock]
						where(1=1)
							--and (t.MyIdxSeq >= @nFilterRecordStart)
							--and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Invoice_Invoice ------:
						--- #tbl_VATRAT:
						select 
							t.InvoiceCode
							, (
								select top 1
									f.VATRateCode
								from Invoice_InvoiceDtl f --//[mylock]
								where(1=1)
									and f.InvoiceCode = t.InvoiceCode
							) VATRateCode
							, (
								select top 1
									f.VATRate
								from Invoice_InvoiceDtl f --//[mylock]
								where(1=1)
									and f.InvoiceCode = t.InvoiceCode
							) VATRate
						into #tbl_VATRAT
						from #tbl_Invoice_Invoice_Filter t --//[mylock]
						where(1=1)
						;

						---- Invoice_Invoice:
						select
							t.MyIdxSeq
							, ii.*
							----
							, mpm.PaymentMethodCode mpm_PaymentMethodCode
							, mpm.PaymentMethodName mpm_PaymentMethodName
							----
							, mcnnt.CustomerNNTCode mcnnt_CustomerNNTCode
							, mcnnt.CustomerNNTName mcnnt_CustomerNNTName
							, mcnnt.CustomerNNTAddress mcnnt_CustomerNNTAddress
							, mcnnt.CustomerNNTEmail mcnnt_CustomerNNTEmail
							, mcnnt.CustomerMST mcnnt_CustomerMST
							, mcnnt.ContactEmail mcnnt_ContactEmail
							----
							, iti.TInvoiceCode iti_TInvoiceCode
							, iti.TInvoiceName iti_TInvoiceName
							, iti.LogoFilePath iti_LogoFilePath
							, iti.WatermarkFilePath iti_WatermarkFilePath
							, iti.InvoiceTGroupCode iti_InvoiceTGroupCode
							, iti.InvoiceType iti_InvoiceType
							, iti.Sign iti_Sign
							, iti.FormNo iti_FormNo
                            ----
	                        , msi.SourceInvoiceCode msi_SourceInvoiceCode
	                        , msi.SourceInvoiceName msi_SourceInvoiceName
                            ----
	                        , itg.InvoiceTGroupCode itg_InvoiceTGroupCode
	                        , itg.Spec_Prd_Type itg_Spec_Prd_Type
							, f.VATRateCode 
							, f.VATRate 
						from #tbl_Invoice_Invoice_Filter t --//[mylock]
							inner join Invoice_Invoice ii --//[mylock]
								on t.InvoiceCode = ii.InvoiceCode
							left join Mst_PaymentMethods mpm --//[mylock]
								on ii.PaymentMethodCode = mpm.PaymentMethodCode
							left join Mst_CustomerNNT mcnnt --//[mylock]
								on ii.CustomerNNTCode = mcnnt.CustomerNNTCode
									and ii.MST = mcnnt.MST
							left join Invoice_TempInvoice iti --//[mylock]
								on ii.TInvoiceCode = iti.TInvoiceCode
	                        left join Mst_SourceInvoice msi --//[mylock]
		                        on ii.SourceInvoiceCode = msi.SourceInvoiceCode
							left join Invoice_TempGroup itg --//[mylock]
								on iti.InvoiceTGroupCode = itg.InvoiceTGroupCode
							left join #tbl_VATRAT f --//[mylock]
								on t.InvoiceCode = f.InvoiceCode
						order by t.MyIdxSeq asc
						;
                        --------------------------------

						-------- Invoice_InvoiceDtl ------:
						---- Invoice_InvoiceDtl:
						select
							t.MyIdxSeq
							, iidt.*
						from #tbl_Invoice_Invoice_Filter t --//[mylock]
							inner join Invoice_Invoice ii --//[mylock]
								on t.InvoiceCode = ii.InvoiceCode
							inner join Invoice_InvoiceDtl iidt --//[mylock]
								on t.InvoiceCode = iidt.InvoiceCode
						order by t.MyIdxSeq asc
						;
						-----------------------------------

						---- Clear for debug:
						--drop table #tbl_Invoice_Invoice_Filter_Draft;
						--drop table #tbl_Invoice_Invoice_Filter;			
					"
                , "@strInvoiceDateUTCFrom", strInvoiceDateUTCFrom
                , "@strInvoiceDateUTCTo", strInvoiceDateUTCTo
                , "@strMST", strMST
                , "@nFilterRecordStart", nFilterRecordStart
                , "@nFilterRecordEnd", nFilterRecordEnd
                );
            ////
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            /////
            {
                dsGetData.Tables[nIdxTable++].TableName = "Invoice_Invoice";
            }
            ////
            {
                dsGetData.Tables[nIdxTable++].TableName = "Invoice_InvoiceDtl";
            }
            #endregion
        }
        public DataSet OS3A_TVAN_InvoiceInvoice_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , string strFt_RecordStart
            , string strFt_RecordCount
            //// 
            , string strMST
            , string strInvoiceDateUTCFrom
            , string strInvoiceDateUTCTo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS3A_TVAN_InvoiceInvoice_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS3A_TVAN_InvoiceInvoice_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
                    , "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    ////
                    , "strReportDTimeFrom", strInvoiceDateUTCFrom
                    , "strReportDTimeTo", strInvoiceDateUTCTo
                    , "strInvoiceType", strMST
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

                #region // Rpt_InvoiceInvoice_ResultUsedX:
                DataSet dsGetData = null;
                string strReportDate = dtimeSys.ToString("yyyy-MM-dd");
                {
                    OS3A_TVAN_InvoiceInvoice_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        /////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                                            ////
                        , strMST // strInvoiceType
                        , strInvoiceDateUTCFrom // strReportDTimeFrom
                        , strInvoiceDateUTCTo // strReportDTimeTo
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


        public DataSet WAS_OS3A_TVAN_InvoiceInvoice_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS3A_TVAN_Invoice_Invoice objRQ_OS3A_TVAN_Invoice_Invoice
            ////
            , out RT_OS3A_TVAN_Invoice_Invoice objRT_OS3A_TVAN_Invoice_Invoice
            )
        {
            #region // Temp:
            string strTid = objRQ_OS3A_TVAN_Invoice_Invoice.Tid;
            objRT_OS3A_TVAN_Invoice_Invoice = new RT_OS3A_TVAN_Invoice_Invoice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS3A_TVAN_InvoiceInvoice_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS3A_TVAN_InvoiceInvoice_Get;
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
                List<OS3A_TVAN_Invoice_Invoice> lst_Invoice_Invoice = new List<OS3A_TVAN_Invoice_Invoice>();
                List<OS3A_TVAN_Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<OS3A_TVAN_Invoice_InvoiceDtl>();
                /////
                #endregion

                #region // WS_Invoice_Invoice_Get:
                mdsResult = OS3A_TVAN_InvoiceInvoice_Get(
                    objRQ_OS3A_TVAN_Invoice_Invoice.Tid // strTid
                    , objRQ_OS3A_TVAN_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_OS3A_TVAN_Invoice_Invoice.GwPassword // strGwPassword
                    , objRQ_OS3A_TVAN_Invoice_Invoice.WAUserCode // strUserCode
                    , objRQ_OS3A_TVAN_Invoice_Invoice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS3A_TVAN_Invoice_Invoice.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS3A_TVAN_Invoice_Invoice.Ft_RecordCount // strFt_RecordCount
                                                                     //// Return:
                    , objRQ_OS3A_TVAN_Invoice_Invoice.MST
                    , objRQ_OS3A_TVAN_Invoice_Invoice.InvoiceDateUTCFrom
                    , objRQ_OS3A_TVAN_Invoice_Invoice.InvoiceDateUTCTo
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS3A_TVAN_Invoice_Invoice.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    {
                        ////
                        DataTable dt_Invoice_Invoice = mdsResult.Tables["Invoice_Invoice"].Copy();
                        lst_Invoice_Invoice = TUtils.DataTableCmUtils.ToListof<OS3A_TVAN_Invoice_Invoice>(dt_Invoice_Invoice);
                        objRT_OS3A_TVAN_Invoice_Invoice.Lst_Invoice_Invoice = lst_Invoice_Invoice;
                    }
                    ////
                    {
                        ////
                        DataTable dt_Invoice_InvoiceDtl = mdsResult.Tables["Invoice_InvoiceDtl"].Copy();
                        lst_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToListof<OS3A_TVAN_Invoice_InvoiceDtl>(dt_Invoice_InvoiceDtl);
                        objRT_OS3A_TVAN_Invoice_Invoice.Lst_Invoice_InvoiceDtl = lst_Invoice_InvoiceDtl;
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
        #endregion

        #region // TVAN Call 3A:
        public void OSTVAN_3A_Invoice_Invoice_CallDelete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strInvoiceCode
            )
        {
            #region // Temp:
            string strFunctionName = "OSTVAN_3A_Invoice_Invoice_CallDelete";
            string strErrorCodeDefault = "OSTVAN_3A_Invoice_Invoice_CallDelete"; // TError.ErridnInventory.OS_TVAN_Invoice_InvoiceInput_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                , "strOrgID", strOrgID
                ////
                , "strInvoiceCode", strInvoiceCode
                ////
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            //alParamsCoupleError.AddRange(new object[]{
            //    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
            //    });
            #endregion

            #region // OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS:
            string strUrl_OS_TVAN_3A = "";
            if (!string.IsNullOrEmpty(strOrgID))
            {
                #region // Add alParamsCoupleError:
                alParamsCoupleError.AddRange(new object[]{
                    "strNetworkID", strNetworkID
                    , "strOrgID", strOrgID
                    ////
                    });
                #endregion

                #region // OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS:
                OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                                   ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strOrgID // strOrgID
                    , out strUrl_OS_TVAN_3A // strUrl_OS_TVAN_3A
                    );
                #endregion
            }
            #endregion

            #region // OS_TVAN_Invoice_InvoiceInput_Save:
            if (!string.IsNullOrEmpty(strUrl_OS_TVAN_3A.Trim()))
            {
                #region // Add alParamsCoupleError:
                alParamsCoupleError.AddRange(new object[]{
                    "strUrl_OS_TVAN_3A", strUrl_OS_TVAN_3A
                    , "strInvoiceCode", strInvoiceCode
                    ////
                    });
                #endregion

                #region // WAC_OS_Sys_AT_3A_Invoice_Delete:
                WAC_OS_Sys_AT_3A_Invoice_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUrl_OS_TVAN_3A // strWSUrlAddr
                    , strInvoiceCode // strRefId
                    );
                #endregion

                #region // Update Nếu call thành công:
                {
                    ////
                    string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
                            ---- 
                            update t 
                            set
                                t.FlagDeleteOutSite = '@strLogLUDTimeUTC'
                                , t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
                                , t.LogLUBy = '@strUserCode'
                            from Invoice_Invoice t --//[mylock]
                            where(1=1)
                                and t.InvoiceCode = '@strInvoiceCode'
                            ;
					    "
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strUserCode", strWAUserCode
                        , "@strInvoiceCode", strInvoiceCode
                        );

                    _cf.db.ExecQuery(
                        strSqlClearForDebug
                        );
                    ////

                }
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
