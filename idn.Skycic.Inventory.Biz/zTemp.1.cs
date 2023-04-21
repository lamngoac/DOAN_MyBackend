using idn.Skycic.Inventory.BizService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TConst = idn.Skycic.Inventory.Constants;
using TDALUtils = EzDAL.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;
using TUtils = idn.Skycic.Inventory.Utils;
using System.IO;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Invoice_Invoice:
        public DataSet WAS_Invoice_Invoice_UpdMailSentDTimeUTC(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_Invoice objRQ_Invoice_Invoice
            ////
            , out RT_Invoice_Invoice objRT_Invoice_Invoice
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_Invoice.Tid;
            objRT_Invoice_Invoice = new RT_Invoice_Invoice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Invoice_Invoice_UpdMailSentDTimeUTC";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_Invoice_UpdMailSentDTimeUTC;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "Lst_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice.Lst_Invoice_Invoice)
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
                    if (objRQ_Invoice_Invoice.Lst_Invoice_Invoice == null)
                        objRQ_Invoice_Invoice.Lst_Invoice_Invoice = new List<Invoice_Invoice>();
                    {
                        DataTable dt_InvoiceInvoice = TUtils.DataTableCmUtils.ToDataTable<Invoice_Invoice>(objRQ_Invoice_Invoice.Lst_Invoice_Invoice, "Invoice_Invoice");
                        dsData.Tables.Add(dt_InvoiceInvoice);
                    }
                }
                #endregion

                #region // Invoice_Invoice_UpdMailSentDTimeUTC:
                mdsResult = Invoice_Invoice_UpdMailSentDTimeUTC(
                    objRQ_Invoice_Invoice.Tid // strTid
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    , objRQ_Invoice_Invoice.WAUserCode // strUserCode
                    , objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
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

        public DataSet Invoice_Invoice_UpdMailSentDTimeUTC(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_Invoice_UpdMailSentDTimeUTC";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTC;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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

                #region // Invoice_Invoice_UpdMailSentDTimeUTCX:
                //DataSet dsGetData = null;
                {
                    Invoice_Invoice_UpdMailSentDTimeUTCX(
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

        private void Invoice_Invoice_UpdMailSentDTimeUTCX(
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
            string strFunctionName = "Invoice_Invoice_UpdMailSentDTimeUTCX";

            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Convert Input: 
            //drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and Check Input Invoice_Invoice:
            ////
            DataTable dtInput_Invoice_Invoice = null;
            {
                ////
                string strTableCheck = "Invoice_Invoice";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_Input_InvoiceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_Invoice = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Invoice_Invoice.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_Input_InvoiceTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_Invoice // dtData
                    , "StdParam", "InvoiceCode" // arrstrCouple
                    , "StdDTime", "MailSentDTimeUTC" // arrstrCouple
                    );
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SendEmailBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Invoice_Invoice.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Invoice_Invoice.Rows[nScan];
                    ////
                    DataTable dtDB_Invoice_Invoice = null;

                    Invoice_Invoice_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , drScan["InvoiceCode"] // objInvoiceCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.InvoiceStatus.Issued // strInvoiceStatusListToCheck
                        , out dtDB_Invoice_Invoice // dtDB_Invoice_Invoice
                        );
                    ////
                    string strMailSentDTimeUTCDB = TUtils.CUtils.StdDTime(dtDB_Invoice_Invoice.Rows[0]["MailSentDTimeUTC"]);
                    ////
                    if (!string.IsNullOrEmpty(strMailSentDTimeUTCDB))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.InvoiceCode", drScan["InvoiceCode"]
                            , "Check.MailSentDTimeUTC", strMailSentDTimeUTCDB
                            //, "Check.InvoiceStatus", strInvoiceStatus
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_Invalid
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }

                    ////
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    drScan["SendEmailBy"] = strWAUserCode;
                }

            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Invoice_Invoice"
                    , new object[]{
                        "InvoiceCode", TConst.BizMix.Default_DBColType
                        , "MailSentDTimeUTC", TConst.BizMix.Default_DBColType
                        , "SendEmailBy", TConst.BizMix.Default_DBColType
                        , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                        , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Invoice_Invoice // dtData
                    );
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_Invoice_Invoice_ClauseSet_zzE = @"
                        t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
                        , t.MailSentDTimeUTC = f.MailSentDTimeUTC
                        , t.SendEmailDTimeUTC = f.MailSentDTimeUTC
                        , t.SendEmailBy = f.SendEmailBy
                        ";
                ////
                string zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						";
                ////
                string zzB_Update_Invoice_Invoice_zzE = CmUtils.StringUtils.Replace(@"
						---- Invoice_Invoice:
						update t
						set 
							zzB_Update_Invoice_Invoice_ClauseSet_zzE
						from Invoice_Invoice t --//[mylock]
							inner join #input_Invoice_Invoice f --//[mylock]
								on t.InvoiceCode = f.InvoiceCode
						where (1=1)
						;
					"
                    , "zzB_Update_Invoice_Invoice_ClauseSet_zzE", zzB_Update_Invoice_Invoice_ClauseSet_zzE
                    );
                ////
                string zzB_Update_Invoice_InvoiceDtl_zzE = CmUtils.StringUtils.Replace(@"
                        ---- Update:
					    update t
					    set 
						    zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
					    from Invoice_InvoiceDtl t --//[mylock]
						    inner join #input_Invoice_Invoice f --//[mylock]
							    on t.InvoiceCode = f.InvoiceCode
					    where (1=1)
					    ;
				    "
                    , "zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE", zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
                    );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_Invoice_Invoice_zzE
                        ----
						zzB_Update_Invoice_InvoiceDtl_zzE
                        ----
					"
                    , "zzB_Update_Invoice_Invoice_zzE", zzB_Update_Invoice_Invoice_zzE
                    , "zzB_Update_Invoice_InvoiceDtl_zzE", zzB_Update_Invoice_InvoiceDtl_zzE
                    //, "zzB_Update_Invoice_InvoicePrd_zzE", zzB_Update_Invoice_InvoicePrd_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion
        }

        private void Invoice_Invoice_ApprovedMultiX(
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
            string strFunctionName = "Invoice_Invoice_ApprovedMultiX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Invoice_Invoice_ApprovedX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
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
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and Check Input Invoice_Invoice:
            ////
            string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
            string strXMLFilePathBase = _cf.nvcParams["TVAN_XMLFilePath"];
            string strXMFilePath = strTVAN_FilePath + strXMLFilePathBase;
            ////
            DataTable dtInput_Invoice_Invoice = null;
            {
                ////
                string strTableCheck = "Invoice_Invoice";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_ApprovedMultiX_Input_InvoiceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_Invoice = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Invoice_Invoice.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_ApprovedMultiX_Input_InvoiceTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_Invoice // dtData
                    , "StdParam", "InvoiceCode" // arrstrCouple
                    , "", "InvoiceFileEncodeBase64" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceFilePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoicePDFFilePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ApprDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ApprBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceStatus", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Invoice_Invoice.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Invoice_Invoice.Rows[nScan];
                    ////
                    DataTable dtDB_Invoice_Invoice = null;

                    Invoice_Invoice_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , drScan["InvoiceCode"] // objInvoiceCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.InvoiceStatus.Pending // strInvoiceStatusListToCheck
                        , out dtDB_Invoice_Invoice // dtDB_Invoice_Invoice
                        );
                    ////
                    //string strInvoiceStatus = dtDB_Invoice_Invoice.Rows[0]["InvoiceStatus"].ToString();
                    string strInvoiceNo = dtDB_Invoice_Invoice.Rows[0]["InvoiceNo"].ToString();
                    ////
                    if (string.IsNullOrEmpty(strInvoiceNo))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.InvoiceCode", drScan["InvoiceCode"]
                            , "Check.InvoiceNo", strInvoiceNo
                            //, "Check.InvoiceStatus", strInvoiceStatus
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_ApprovedMultiX_InvoiceNoIsNotNull
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    /////
                    string strMST = TUtils.CUtils.StdParam(dtDB_Invoice_Invoice.Rows[0]["MST"]);
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST)
                        && !CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["FlagBG"], TConst.Flag.Active))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                            , "Check.strMST", strMST
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_ApprovedMultiX_InvalidMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                    string strNetworkId = dtDB_Invoice_Invoice.Rows[0]["NetworkID"].ToString();
                    string strTidFile = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq_InvoiceApprovedMulti++);
                    string strDeCodeBase64 = TUtils.CUtils.Base64_Decode(dtInput_Invoice_Invoice.Rows[0]["InvoiceFileEncodeBase64"].ToString());
                    string strXMLFilePathSave = string.Format("{0}{1}.xml", strXMFilePath, (strTidFile + "." + strNetworkId + ".KyHoaDon"));
                    ////Tạo File XML
                    File.WriteAllText(strXMLFilePathSave, strDeCodeBase64);
                    ////
                    drScan["InvoiceFilePath"] = strXMLFilePathSave.Replace(_cf.nvcParams["TVAN_FilePath"], "");
                    drScan["InvoicePDFFilePath"] = null;
                    drScan["ApprDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["ApprBy"] = strWAUserCode;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    drScan["InvoiceStatus"] = "APPROVED";
                }

            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Invoice_Invoice"
                    , new object[]{
                        "InvoiceCode", TConst.BizMix.Default_DBColType
                        , "InvoiceFileSpec", TConst.BizMix.Default_DBColType
                        , "InvoiceFilePath", TConst.BizMix.Default_DBColType
                        , "InvoicePDFFilePath", TConst.BizMix.Default_DBColType
                        , "Remark", TConst.BizMix.Default_DBColType
                        , "InvoiceStatus", TConst.BizMix.Default_DBColType
                        , "ApprDTimeUTC", TConst.BizMix.Default_DBColType
                        , "ApprBy", TConst.BizMix.Default_DBColType
                        , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                        , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Invoice_Invoice // dtData
                );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_Invoice_Invoice_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.ApprDTimeUTC = f.ApprDTimeUTC
						, t.ApprBy = f.ApprBy
						, t.InvoiceStatus = f.InvoiceStatus
						, t.InvoiceFileSpec = f.InvoiceFileSpec
						, t.InvoiceFilePath = f.InvoiceFilePath
						, t.InvoicePDFFilePath = f.InvoicePDFFilePath
						, t.Remark = f.Remark
						";
                ////
                string zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.InvoiceDtlStatus = f.InvoiceStatus
						";
                ////
                //          string zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE = @"
                //t.LogLUDTimeUTC = f.LogLUDTimeUTC
                //, t.LogLUBy = f.LogLUBy
                //, t.InvoicePrdStatus = f.InvoiceStatus
                //";
                ////
                string zzB_Update_Invoice_Invoice_zzE = CmUtils.StringUtils.Replace(@"
						---- Invoice_Invoice:
						update t
						set 
							zzB_Update_Invoice_Invoice_ClauseSet_zzE
						from Invoice_Invoice t --//[mylock]
							inner join #input_Invoice_Invoice f --//[mylock]
								on t.InvoiceCode = f.InvoiceCode
						where (1=1)
						;
					"
                    , "zzB_Update_Invoice_Invoice_ClauseSet_zzE", zzB_Update_Invoice_Invoice_ClauseSet_zzE
                    );
                ////
                string zzB_Update_Invoice_InvoiceDtl_zzE = CmUtils.StringUtils.Replace(@"
                    ---- #tbl_Invoice_InvoiceDtl_Temp: 
                    select 
                        t.InvoiceCode
                        , t.Idx
                        , f.InvoiceStatus
                        , f.LogLUDTimeUTC
                        , f.LogLUBy
                    into #tbl_Invoice_InvoiceDtl_Temp
					from Invoice_InvoiceDtl t --//[mylock]
						inner join #input_Invoice_Invoice f --//[mylock]
							on t.InvoiceCode = f.InvoiceCode
					where (1=1)
					;

                    ---- Update:
					update t
					set 
						zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
					from Invoice_InvoiceDtl t --//[mylock]
						inner join #tbl_Invoice_InvoiceDtl_Temp f --//[mylock]
							on t.InvoiceCode = f.InvoiceCode
								and t.Idx = f.Idx
					where (1=1)
					;
				"
                , "zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE", zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
                );
                //           ////
                //           string zzB_Update_Invoice_InvoicePrd_zzE = CmUtils.StringUtils.Replace(@"
                //                   ---- #tbl_Invoice_InvoicePrd_Temp: 
                //                   select 
                //                       t.InvoiceCode
                //                       , t.ProductID
                //                       , t.SpecCode
                //                       , f.InvoiceStatus
                //                       , f.LogLUDTimeUTC
                //                       , f.LogLUBy
                //                   into #tbl_Invoice_InvoicePrd_Temp
                //	from Invoice_InvoicePrd t --//[mylock]
                //		inner join #input_Invoice_Invoice f --//[mylock]
                //			on t.InvoiceCode = f.InvoiceCode
                //	where (1=1)
                //	;

                //                   ---- Update:
                //	update t
                //	set 
                //		zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE
                //	from Invoice_InvoicePrd t --//[mylock]
                //		inner join #tbl_Invoice_InvoicePrd_Temp f --//[mylock]
                //			on t.InvoiceCode = f.InvoiceCode
                //			    and t.ProductID = f.ProductID
                //			    and t.SpecCode = f.SpecCode
                //	where (1=1)
                //	;
                //"
                //               , "zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE", zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE
                //               );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_Invoice_Invoice_zzE
                        ----
						zzB_Update_Invoice_InvoiceDtl_zzE
                        ----
					"
                    , "zzB_Update_Invoice_Invoice_zzE", zzB_Update_Invoice_Invoice_zzE
                    , "zzB_Update_Invoice_InvoiceDtl_zzE", zzB_Update_Invoice_InvoiceDtl_zzE
                    //, "zzB_Update_Invoice_InvoicePrd_zzE", zzB_Update_Invoice_InvoicePrd_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Invoice_Invoice; 
						drop table #tbl_Invoice_InvoiceDtl_Temp;
						drop table #tbl_Mst_NNT_ViewAbility;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

        }

        public DataSet WAS_Invoice_Invoice_SaveAndAllocatedInv(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_Invoice objRQ_Invoice_Invoice
            ////
            , out RT_Invoice_Invoice objRT_Invoice_Invoice
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_Invoice.Tid;
            objRT_Invoice_Invoice = new RT_Invoice_Invoice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Invoice_Invoice_SaveAndAllocatedInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_Invoice_SaveAndAllocatedInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Invoice_Invoice.FlagIsDelete
                , "Lst_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice.Lst_Invoice_Invoice)
                , "Lst_Invoice_InvoiceDtl", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice.Lst_Invoice_InvoiceDtl)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Invoice_Invoice.Lst_Invoice_Invoice == null)
                        objRQ_Invoice_Invoice.Lst_Invoice_Invoice = new List<Invoice_Invoice>();
                    {
                        DataTable dt_InvoiceInvoice = TUtils.DataTableCmUtils.ToDataTable<Invoice_Invoice>(objRQ_Invoice_Invoice.Lst_Invoice_Invoice, "Invoice_Invoice");
                        dsData.Tables.Add(dt_InvoiceInvoice);
                    }
                    if (objRQ_Invoice_Invoice.Lst_Invoice_InvoiceDtl == null)
                        objRQ_Invoice_Invoice.Lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
                    {
                        DataTable dt_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToDataTable<Invoice_InvoiceDtl>(objRQ_Invoice_Invoice.Lst_Invoice_InvoiceDtl, "Invoice_InvoiceDtl");
                        dsData.Tables.Add(dt_Invoice_InvoiceDtl);
                    }
                }
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                /////
                bool bGet_Invoice_Invoice = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice.Length > 0);
                #endregion

                #region // Invoice_Invoice_SaveAndAllocatedInv:
                mdsResult = Invoice_Invoice_SaveAndAllocatedInv(
                    objRQ_Invoice_Invoice.Tid // strTid
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    , objRQ_Invoice_Invoice.WAUserCode // strUserCode
                    , objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objRQ_Invoice_Invoice.FlagIsDelete
                    , dsData // dsData
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Invoice_Invoice = mdsResult.Tables["Invoice_Invoice"].Copy();
                    lst_Invoice_Invoice = TUtils.DataTableCmUtils.ToListof<Invoice_Invoice>(dt_Invoice_Invoice);
                    objRT_Invoice_Invoice.Lst_Invoice_Invoice = lst_Invoice_Invoice;
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

        public DataSet Invoice_Invoice_SaveAndAllocatedInv(
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
            string strFunctionName = "Invoice_Invoice_SaveAndAllocatedInv";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_Invoice_SaveAndAllocatedInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                /////
                , "objFlagIsDelete", objFlagIsDelete
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
                    Invoice_Invoice_SaveX_New20190919(
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
                #endregion

                #region // Invoice_Invoice_AllocatedInvX:
                //DataSet dsGetData = null;
                {
                    DataTable dt_Invoice_Invoice = dsData.Tables["Invoice_Invoice"].Copy();

                    for (int nScan = 0; nScan < dt_Invoice_Invoice.Rows.Count; nScan++)
                    {
                        DataRow drScan = dt_Invoice_Invoice.Rows[nScan];
                        Invoice_Invoice_AllocatedInvX_New20190917(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        ////
                        , drScan["InvoiceCode"] // objInvoiceCode
                        , drScan["InvoiceDateUTC"] // objInvoiceDateUTC
                        );
                    }
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // Invoice_Invoice_GetInvoiceNoX
                DataSet dsGetData = new DataSet();
                {
                    Invoice_Invoice_GetInvoiceNoX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                        ////
                        , dsData
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
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

        private void Invoice_Invoice_SaveX_New20190919(
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
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Invoice_Invoice_SaveAllX";
            //string strErrorCodeDefault = TError.ErridnInventory.Invoice_Invoice_SaveAllX;
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
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            string strMST = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTMST"]);
            #endregion

            #region // Refine and Check Input Invoice_Invoice:
            ////
            DataTable dtInput_Invoice_Invoice = null;
            {
                ////
                string strTableCheck = "Invoice_Invoice";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_Invoice = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Invoice_Invoice.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_Invoice // dtData
                    , "StdParam", "InvoiceCode" // arrstrCouple
                    , "StdParam", "MST" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "StdParam", "RefNo" // arrstrCouple
                    , "StdParam", "FormNo" // arrstrCouple
                    , "StdParam", "Sign" // arrstrCouple
                    , "StdParam", "SourceInvoiceCode" // arrstrCouple
                    , "StdParam", "InvoiceAdjType" // arrstrCouple
                    , "StdParam", "PaymentMethodCode" // arrstrCouple
                    , "StdParam", "InvoiceType2" // arrstrCouple
                    , "StdParam", "CustomerNNTCode" // arrstrCouple
                    , "", "CustomerNNTName" // arrstrCouple
                    , "", "CustomerNNTAddress" // arrstrCouple
                    , "", "CustomerNNTPhone" // arrstrCouple
                    , "", "CustomerNNTBankName" // arrstrCouple
                    , "", "CustomerNNTEmail" // arrstrCouple
                    , "", "CustomerNNTAccNo" // arrstrCouple
                    , "", "CustomerNNTBuyerName" // arrstrCouple
                    , "StdParam", "CustomerMST" // arrstrCouple
                    , "StdParam", "TInvoiceCode" // arrstrCouple
                    , "StdParam", "InvoiceNo" // arrstrCouple
                    , "", "InvoiceDateUTC" // arrstrCouple
                    , "StdParam", "EmailSend" // arrstrCouple
                    , "", "InvoiceFileSpec" // arrstrCouple
                    , "", "InvoiceFilePath" // arrstrCouple
                    , "", "InvoicePDFFilePath" // arrstrCouple
                    , "float", "TotalValInvoice" // arrstrCouple
                    , "float", "TotalValVAT" // arrstrCouple
                    , "float", "TotalValPmt" // arrstrCouple
                    , "", "AttachedDelFilePath" // arrstrCouple
                    , "", "DeleteReason" // arrstrCouple
                    , "StdParam", "InvoiceVerifyCQTCode" // arrstrCouple
                    , "StdParam", "CurrencyCode" // arrstrCouple
                    , "float", "CurrencyRate" // arrstrCouple
                    , "float", "ValGoodsNotTaxable" // arrstrCouple
                    , "float", "ValGoodsNotChargeTax" // arrstrCouple
                    , "float", "ValGoodsVAT5" // arrstrCouple
                    , "float", "ValVAT5" // arrstrCouple
                    , "float", "ValGoodsVAT10" // arrstrCouple
                    , "float", "ValVAT10" // arrstrCouple
                    , "", "NNTFullName" // arrstrCouple
                    , "", "NNTFullAdress" // arrstrCouple
                    , "", "NNTPhone" // arrstrCouple
                    , "", "NNTFax" // arrstrCouple
                    , "", "NNTEmail" // arrstrCouple
                    , "", "NNTWebsite" // arrstrCouple
                    , "", "NNTAccNo" // arrstrCouple
                    , "", "NNTBankName" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    , "", "InvoiceCF1" // arrstrCouple
                    , "", "InvoiceCF2" // arrstrCouple
                    , "", "InvoiceCF3" // arrstrCouple
                    , "", "InvoiceCF4" // arrstrCouple
                    , "", "InvoiceCF5" // arrstrCouple
                    , "", "InvoiceCF6" // arrstrCouple
                    , "", "InvoiceCF7" // arrstrCouple
                    , "", "InvoiceCF8" // arrstrCouple
                    , "", "InvoiceCF9" // arrstrCouple
                    , "", "InvoiceCF10" // arrstrCouple
                    , "", "FlagConfirm" // arrstrCouple
                    , "", "FlagCheckCustomer" // arrstrCouple
                    );
                ////
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "EmailSend", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "CreateDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "CreateBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceNoDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceNoBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SignDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SignBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ApprDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ApprBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "CancelDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "CancelBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SendEmailDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SendEmailBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "IssuedDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "IssuedBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "DeleteDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "DeleteBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ChangeDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "ChangeBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "FlagChange", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "FlagPushOutSite", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUBy", typeof(object));
            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            //if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Invoice_Invoice" // strTableName
                    , new object[] {
                            "InvoiceCode", TConst.BizMix.Default_DBColType
                            , "MST", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "RefNo", TConst.BizMix.Default_DBColType
                            , "FormNo", TConst.BizMix.Default_DBColType
                            , "Sign", TConst.BizMix.Default_DBColType
                            , "SourceInvoiceCode", TConst.BizMix.Default_DBColType
                            , "InvoiceAdjType", TConst.BizMix.Default_DBColType
                            , "PaymentMethodCode", TConst.BizMix.Default_DBColType
                            , "InvoiceType2", TConst.BizMix.Default_DBColType
                            , "CustomerNNTCode", TConst.BizMix.Default_DBColType
                            , "CustomerNNTName", TConst.BizMix.Default_DBColType
                            , "CustomerNNTAddress", TConst.BizMix.Default_DBColType
                            , "CustomerNNTPhone", TConst.BizMix.Default_DBColType
                            , "CustomerNNTBankName", TConst.BizMix.Default_DBColType
                            , "CustomerNNTEmail", TConst.BizMix.Default_DBColType
                            , "CustomerNNTAccNo", TConst.BizMix.Default_DBColType
                            , "CustomerNNTBuyerName", TConst.BizMix.Default_DBColType
                            , "CustomerMST", TConst.BizMix.Default_DBColType
                            , "TInvoiceCode", TConst.BizMix.Default_DBColType
                            , "InvoiceNo", TConst.BizMix.Default_DBColType
                            , "InvoiceDateUTC", TConst.BizMix.Default_DBColType
                            , "EmailSend", TConst.BizMix.Default_DBColType
                            , "InvoiceFileSpec", TConst.BizMix.Default_DBColType
                            , "InvoiceFilePath", TConst.BizMix.Default_DBColType
                            , "InvoicePDFFilePath", TConst.BizMix.Default_DBColType
                            , "TotalValInvoice", "float"
                            , "TotalValVAT", "float"
                            , "TotalValPmt", "float"
                            , "CreateDTimeUTC", TConst.BizMix.Default_DBColType
                            , "CreateBy", TConst.BizMix.Default_DBColType
                            , "InvoiceNoDTimeUTC", TConst.BizMix.Default_DBColType
                            , "InvoiceNoBy", TConst.BizMix.Default_DBColType
                            , "SignDTimeUTC", TConst.BizMix.Default_DBColType
                            , "SignBy", TConst.BizMix.Default_DBColType
                            , "ApprDTimeUTC", TConst.BizMix.Default_DBColType
                            , "ApprBy", TConst.BizMix.Default_DBColType
                            , "CancelDTimeUTC", TConst.BizMix.Default_DBColType
                            , "CancelBy", TConst.BizMix.Default_DBColType
                            , "SendEmailDTimeUTC", TConst.BizMix.Default_DBColType
                            , "SendEmailBy", TConst.BizMix.Default_DBColType
                            , "IssuedDTimeUTC", TConst.BizMix.Default_DBColType
                            , "IssuedBy", TConst.BizMix.Default_DBColType
                            , "AttachedDelFilePath", TConst.BizMix.Default_DBColType
                            , "DeleteReason", TConst.BizMix.Default_DBColType
                            , "DeleteDTimeUTC", TConst.BizMix.Default_DBColType
                            , "DeleteBy", TConst.BizMix.Default_DBColType
                            , "ChangeDTimeUTC", TConst.BizMix.Default_DBColType
                            , "ChangeBy", TConst.BizMix.Default_DBColType
                            , "InvoiceVerifyCQTCode", TConst.BizMix.Default_DBColType
                            , "CurrencyCode", TConst.BizMix.Default_DBColType
                            , "CurrencyRate", TConst.BizMix.Default_DBColType
                            , "ValGoodsNotTaxable", "float"
                            , "ValGoodsNotChargeTax", "float"
                            , "ValGoodsVAT5", "float"
                            , "ValVAT5", "float"
                            , "ValGoodsVAT10", "float"
                            , "ValVAT10", "float"
                            , "NNTFullName", TConst.BizMix.Default_DBColType
                            , "NNTFullAdress", TConst.BizMix.Default_DBColType
                            , "NNTPhone", TConst.BizMix.Default_DBColType
                            , "NNTFax", TConst.BizMix.Default_DBColType
                            , "NNTEmail", TConst.BizMix.Default_DBColType
                            , "NNTWebsite", TConst.BizMix.Default_DBColType
                            , "NNTAccNo", TConst.BizMix.Default_DBColType
                            , "NNTBankName", TConst.BizMix.Default_DBColType
                            , "LUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LUBy", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "InvoiceCF1", TConst.BizMix.Default_DBColType
                            , "InvoiceCF2", TConst.BizMix.Default_DBColType
                            , "InvoiceCF3", TConst.BizMix.Default_DBColType
                            , "InvoiceCF4", TConst.BizMix.Default_DBColType
                            , "InvoiceCF5", TConst.BizMix.Default_DBColType
                            , "InvoiceCF6", TConst.BizMix.Default_DBColType
                            , "InvoiceCF7", TConst.BizMix.Default_DBColType
                            , "InvoiceCF8", TConst.BizMix.Default_DBColType
                            , "InvoiceCF9", TConst.BizMix.Default_DBColType
                            , "InvoiceCF10", TConst.BizMix.Default_DBColType
                            , "FlagConfirm", TConst.BizMix.Default_DBColType
                            , "FlagCheckCustomer", TConst.BizMix.Default_DBColType
                            , "FlagChange", TConst.BizMix.Default_DBColType
                            , "FlagPushOutSite", TConst.BizMix.Default_DBColType
                            , "FlagDeleteOutSite", TConst.BizMix.Default_DBColType
                            , "InvoiceStatus", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Invoice_Invoice // dtData
                );
            }
            #endregion

            #region /// Refine and check Input Invoice_Invoice:
            {
                #region ----// Check InvalidInvoiceCode:
                {
                    string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                            --- Check:
                            select distinct 
                                t.InvoiceCode
                            from #input_Invoice_Invoice t --//[mylock]
                            where(1=1)
                                and (t.InvoiceCode is null or LTRIM(RTRIM(t.InvoiceCode)) = '')
                            ;
						");
                    DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                        strSql_CheckInvalidInvoiceCode
                        ).Tables[0];
                    /////
                    if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.ErrConditionalRaise", "(t.InvoiceCode is null or LTRIM(RTRIM(t.InvoiceCode)) = '')"
                            , "Check.NumberRows", dt_CheckInvalidInvoiceCode.Rows.Count
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_SaveX_InvalidInvoiceCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region ----// Check InvalidInvoiceStatus:
                {
                    string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
                                t.InvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF
                            from #input_Invoice_Invoice t --//[mylock]
                                inner join Invoice_Invoice f --//[mylock]
                                    on t.InvoiceCode = f.InvoiceCode
                            where(1=1)
                                and f.InvoiceStatus not in ('PENDING') 
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.InvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListInvoiceCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF;
						");
                    DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                        strSql_CheckInvalidInvoiceCode
                        ).Tables[0];
                    /////
                    if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                    {
                        string strListInvoiceCode = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListInvoiceCode))
                        {
                            if (bIsDelete)
                            {
                                goto MyCodeLabel_Done; // Thành công
                            }
                            else // if (!string.IsNullOrEmpty(strListInvoiceCode))
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.ErrConditionalRaise", "f.InvoiceStatus not in ('PENDING')"
                                    , "Check.strListInvoiceCode", strListInvoiceCode
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Invoice_Invoice_SaveX_StatusNotMatched
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Đã cấp số hóa đơn thì không được Xóa:
                if (!bIsDelete)
                {
                    string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
                                t.InvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF
                            from #input_Invoice_Invoice t --//[mylock]
                                inner join Invoice_Invoice f --//[mylock]
                                    on t.InvoiceCode = f.InvoiceCode
                            where(1=1)
                                and f.InvoiceNo is not null 
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.InvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListInvoiceCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF;
						");
                    DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                        strSql_CheckInvalidInvoiceCode
                        ).Tables[0];
                    /////
                    if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                    {
                        /////
                        string strListInvoiceCode = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListInvoiceCode"]);
                        ////
                        if (!string.IsNullOrEmpty(strListInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "f.InvoiceNo is not null "
                                , "Check.strListInvoiceCode", strListInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_ExistInvoiceNo
                                , null
                                , alParamsCoupleError.ToArray()
                                );

                        }
                    }
                    ////
                }
                #endregion

                #region ----// Nếu là hóa đơn điều chỉnh thì RefNo không được Null:
                if (!bIsDelete)
                {
                    string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
                                t.InvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF
                            from #input_Invoice_Invoice t --//[mylock]
                            where(1=1)
                                and t.RefNo is null
                                and t.InvoiceAdjType in ('@strADJINCREASE', '@strADJDESCREASE')
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.InvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListInvoiceCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF;
						"
                        , "@strADJINCREASE", TConst.InvoiceAdjType.AdjInCrease
                        , "@strADJINCREASE", TConst.InvoiceAdjType.AdjDescrease
                        );
                    DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                        strSql_CheckInvalidInvoiceCode
                        ).Tables[0];
                    /////
                    if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                    {
                        string strListInvoiceCode = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "and t.InvoiceAdjType in ('@strADJINCREASE', '@strADJDESCREASE') and t.RefNo is null"
                                , "Check.strListInvoiceCode", strListInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_InvoiceAdjTypeIsNotNull
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Nếu là hóa đơn thay thế:
                {
                    #region ----// Nếu là hóa đơn thay thế thì RefNo không được Null:
                    {
                        string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                                --- #tbl_Invoice_Invoice_ForSTUFF:
                                select distinct 
                                    t.RefNo
                                into #tbl_Invoice_Invoice_ForSTUFF
                                from #input_Invoice_Invoice t --//[mylock]
                                where(1=1)
                                    and t.SourceInvoiceCode in ('@strInvoiceReplace') 
                                    and (t.RefNo is null or LTRIM(RTRIM(t.RefNo)) = '')
                                ;

                                --- Return:					
                                select 
                                    STUFF(( 
		                                SELECT ',' + f.RefNo
		                                FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                                WHERE(1=1)
		                                FOR
		                                XML PATH('')
		                                ), 1, 1, ''
                                    ) AS ListRefNo
                                where(1=1)
                                ;

                                --- Clear For Debug:
                                drop table #tbl_Invoice_Invoice_ForSTUFF;
						    "
                            , "@strInvoiceReplace", TConst.SourceInvoiceCode.InvoiceReplace
                            );
                        DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                            strSql_CheckInvalidInvoiceCode
                            ).Tables[0];
                        /////
                        if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                        {
                            /////
                            string strListRefNo = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListRefNo"]);
                            ////
                            if (!string.IsNullOrEmpty(strListRefNo))
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.ErrConditionalRaise", "and t.SourceInvoiceCode in ('@strInvoiceReplace') and (t.RefNo is null or LTRIM(RTRIM(t.RefNo)) = '')"
                                    , "Check.strListRefNo", strListRefNo
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvalidRefNo
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        ////
                    }
                    #endregion

                    #region ----// RefNo ở trạng thái Deleted:
                    {
                        string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                                --- #tbl_Invoice_Invoice_ForSTUFF:
                                select distinct 
                                    t.RefNo
                                into #tbl_Invoice_Invoice_ForSTUFF
                                from #input_Invoice_Invoice t --//[mylock]
                                    left join Invoice_Invoice f --//[mylock]
                                        on t.RefNo = f.InvoiceCode
                                where(1=1)
                                    and t.SourceInvoiceCode in ('@strInvoiceReplace') 
                                    and (f.InvoiceStatus not in ('DELETED', 'ISSUED')) 
                                ;

                                --- Return:					
                                select 
                                    STUFF(( 
		                                SELECT ',' + f.RefNo
		                                FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                                WHERE(1=1)
		                                FOR
		                                XML PATH('')
		                                ), 1, 1, ''
                                    ) AS ListRefNo
                                where(1=1)
                                ;

                                --- Clear For Debug:
                                drop table #tbl_Invoice_Invoice_ForSTUFF;
						    "
                            , "@strInvoiceReplace", TConst.SourceInvoiceCode.InvoiceReplace
                            );
                        DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                            strSql_CheckInvalidInvoiceCode
                            ).Tables[0];
                        /////
                        if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                        {
                            /////
                            string strListRefNo = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListRefNo"]);
                            ////
                            if (!string.IsNullOrEmpty(strListRefNo))
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.ErrConditionalRaise", "and t.SourceInvoiceCode in ('@strInvoiceReplace')  and (f.InvoiceStatus not in ('DELETED', 'ISSUED')) "
                                    , "Check.strListRefNo", strListRefNo
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvalidInvoiceStatusRefNo
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );

                            }
                        }
                        ////
                    }
                    #endregion

                    #region ----// Không cho phép xóa hóa đơn thay thế:
                    if (bIsDelete)
                    {
                        string strSql_CheckInvalidInvoiceCode = CmUtils.StringUtils.Replace(@"
                                --- #tbl_Invoice_Invoice_ForSTUFF:
                                select distinct 
                                    t.RefNo
                                into #tbl_Invoice_Invoice_ForSTUFF
                                from #input_Invoice_Invoice t --//[mylock]
                                where(1=1)
                                ;

                                --- Return:					
                                select 
                                    STUFF(( 
		                                SELECT ',' + f.RefNo
		                                FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
		                                WHERE(1=1)
		                                FOR
		                                XML PATH('')
		                                ), 1, 1, ''
                                    ) AS ListRefNo
                                where(1=1)
                                ;

                                --- Clear For Debug:
                                drop table #tbl_Invoice_Invoice_ForSTUFF;
						    "
                            , "@strInvoiceReplace", TConst.SourceInvoiceCode.InvoiceReplace
                            );
                        DataTable dt_CheckInvalidInvoiceCode = _cf.db.ExecQuery(
                            strSql_CheckInvalidInvoiceCode
                            ).Tables[0];
                        /////
                        if (dt_CheckInvalidInvoiceCode.Rows.Count > 0)
                        {
                            /////
                            string strListRefNo = TUtils.CUtils.StdParam(dt_CheckInvalidInvoiceCode.Rows[0]["ListRefNo"]);
                            ////
                            if (!string.IsNullOrEmpty(strListRefNo))
                            {
                                alParamsCoupleError.AddRange(new object[]{
                                    "Check.strListRefNo", strListRefNo
                                    });
                                throw CmUtils.CMyException.Raise(
                                    TError.ErridnInventory.Invoice_Invoice_SaveX_NotDelete
                                    , null
                                    , alParamsCoupleError.ToArray()
                                    );
                            }
                        }
                        ////
                    }
                    #endregion
                }
                #endregion

                #region ----// Check Mst_NNT:
                {
                    string strSql_CheckMstNNT = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
	                            t.MST
                            into #tbl_Invoice_Invoice_ForSTUFF_Exist
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_NNT f --//[mylock]
		                            on t.MST = f.MST
                            where(1=1)
	                            and f.MST is null
                            ;

                            --- #tbl_Invoice_Invoice_ForSTUFF_Active:
                            select distinct 
	                            t.MST
                            into #tbl_Invoice_Invoice_ForSTUFF_Active
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_NNT f --//[mylock]
		                            on t.MST = f.MST
                            where(1=1)
	                            and f.FlagActive = '0'
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.MST
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Exist f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListMST
                            where(1=1)
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.MST
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Active f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListMST
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Exist;
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Active;
						");
                    DataSet ds_CheckMstNNT = _cf.db.ExecQuery(
                        strSql_CheckMstNNT
                        );
                    ////
                    DataTable dt_Invoice_Invoice_ForSTUFF_Exist = ds_CheckMstNNT.Tables[0];
                    DataTable dt_Invoice_Invoice_ForSTUFF_Active = ds_CheckMstNNT.Tables[1];
                    /////
                    if (dt_Invoice_Invoice_ForSTUFF_Exist.Rows.Count > 0)
                    {
                        string strListMST = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Exist.Rows[0]["ListMST"]);
                        /////
                        if (!string.IsNullOrEmpty(strListMST))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_NNT.MST is null"
                                , "Check.strListMST", strListMST
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstNNT_NotFound
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                    if (dt_Invoice_Invoice_ForSTUFF_Active.Rows.Count > 0)
                    {
                        string strListMST = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Active.Rows[0]["ListMST"]);
                        /////
                        if (!string.IsNullOrEmpty(strListMST))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_NNT.FlagActive = '0'"
                                , "Check.strListMST", strListMST
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstNNT_FlagActive
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Check Mst_PaymentMethods:
                {
                    string strSql_CheckPaymentMethods = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
	                            t.PaymentMethodCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Exist
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_PaymentMethods f --//[mylock]
		                            on t.PaymentMethodCode = f.PaymentMethodCode
                            where(1=1)
	                            and f.PaymentMethodCode is null
                            ;

                            --- #tbl_Invoice_Invoice_ForSTUFF_Active:
                            select distinct 
	                            t.PaymentMethodCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Active
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_PaymentMethods f --//[mylock]
		                            on t.PaymentMethodCode = f.PaymentMethodCode
                            where(1=1)
	                            and f.FlagActive = '0'
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.PaymentMethodCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Exist f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListPaymentMethodCode
                            where(1=1)
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.PaymentMethodCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Active f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListPaymentMethodCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Exist;
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Active;
						");
                    DataSet ds_CheckMst_PaymentMethods = _cf.db.ExecQuery(
                        strSql_CheckPaymentMethods
                        );
                    ////
                    DataTable dt_Invoice_Invoice_ForSTUFF_Exist = ds_CheckMst_PaymentMethods.Tables[0];
                    DataTable dt_Invoice_Invoice_ForSTUFF_Active = ds_CheckMst_PaymentMethods.Tables[1];
                    /////
                    if (dt_Invoice_Invoice_ForSTUFF_Exist.Rows.Count > 0)
                    {
                        string strListPaymentMethodCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Exist.Rows[0]["ListPaymentMethodCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListPaymentMethodCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_PaymentMethods.PaymentMethodCode is null"
                                , "Check.strListPaymentMethodCode", strListPaymentMethodCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_PaymentMethods_NotFound
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                    if (dt_Invoice_Invoice_ForSTUFF_Active.Rows.Count > 0)
                    {
                        string strListPaymentMethodCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Active.Rows[0]["ListPaymentMethodCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListPaymentMethodCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_PaymentMethods.FlagActive = '0'"
                                , "Check.strListPaymentMethodCode", strListPaymentMethodCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_PaymentMethods_FlagActive
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Check Mst_InvoiceType2:
                {
                    string strSql_CheckInvoiceType2 = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
	                            t.InvoiceType2
                            into #tbl_Invoice_Invoice_ForSTUFF_Exist
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_InvoiceType2 f --//[mylock]
		                            on t.InvoiceType2 = f.InvoiceType2
                            where(1=1)
	                            and f.InvoiceType2 is null
                            ;

                            --- #tbl_Invoice_Invoice_ForSTUFF_Active:
                            select distinct 
	                            t.InvoiceType2
                            into #tbl_Invoice_Invoice_ForSTUFF_Active
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_InvoiceType2 f --//[mylock]
		                            on t.InvoiceType2 = f.InvoiceType2
                            where(1=1)
	                            and f.FlagActive = '0'
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.InvoiceType2
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Exist f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListInvoiceType2
                            where(1=1)
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.InvoiceType2
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Active f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListInvoiceType2
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Exist;
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Active;
						");
                    DataSet ds_CheckMst_InvoiceType2 = _cf.db.ExecQuery(
                        strSql_CheckInvoiceType2
                        );
                    ////
                    DataTable dt_Invoice_Invoice_ForSTUFF_Exist = ds_CheckMst_InvoiceType2.Tables[0];
                    DataTable dt_Invoice_Invoice_ForSTUFF_Active = ds_CheckMst_InvoiceType2.Tables[1];
                    /////
                    if (dt_Invoice_Invoice_ForSTUFF_Exist.Rows.Count > 0)
                    {
                        string strListInvoiceType2 = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Exist.Rows[0]["ListInvoiceType2"]);
                        /////
                        if (!string.IsNullOrEmpty(strListInvoiceType2))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_InvoiceType2.InvoiceType2 is null"
                                , "Check.strListInvoiceType2", strListInvoiceType2
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_InvoiceType2_NotFound
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                    if (dt_Invoice_Invoice_ForSTUFF_Active.Rows.Count > 0)
                    {
                        string strListInvoiceType2 = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Active.Rows[0]["ListInvoiceType2"]);
                        /////
                        if (!string.IsNullOrEmpty(strListInvoiceType2))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_InvoiceType2.FlagActive = '0'"
                                , "Check.strListInvoiceType2", strListInvoiceType2
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_InvoiceType2_FlagActive
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Check Mst_SourceInvoice:
                {
                    string strSql_CheckMst_SourceInvoice = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF:
                            select distinct 
	                            t.SourceInvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Exist
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_SourceInvoice f --//[mylock]
		                            on t.SourceInvoiceCode = f.SourceInvoiceCode
                            where(1=1)
	                            and f.SourceInvoiceCode is null
                            ;

                            --- #tbl_Invoice_Invoice_ForSTUFF_Active:
                            select distinct 
	                            t.SourceInvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Active
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Mst_SourceInvoice f --//[mylock]
		                            on t.SourceInvoiceCode = f.SourceInvoiceCode
                            where(1=1)
	                            and f.FlagActive = '0'
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.SourceInvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Exist f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListSourceInvoiceCode
                            where(1=1)
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.SourceInvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Active f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListSourceInvoiceCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Exist;
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Active;
						");
                    DataSet ds_CheckMst_PaymentMethods = _cf.db.ExecQuery(
                        strSql_CheckMst_SourceInvoice
                        );
                    ////
                    DataTable dt_Invoice_Invoice_ForSTUFF_Exist = ds_CheckMst_PaymentMethods.Tables[0];
                    DataTable dt_Invoice_Invoice_ForSTUFF_Active = ds_CheckMst_PaymentMethods.Tables[1];
                    /////
                    if (dt_Invoice_Invoice_ForSTUFF_Exist.Rows.Count > 0)
                    {
                        string strListSourceInvoiceCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Exist.Rows[0]["ListSourceInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListSourceInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_SourceInvoice.SourceInvoiceCode is null"
                                , "Check.strListSourceInvoiceCode", strListSourceInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_SourceInvoice_NotFound
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                    if (dt_Invoice_Invoice_ForSTUFF_Active.Rows.Count > 0)
                    {
                        string strListSourceInvoiceCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Active.Rows[0]["ListSourceInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListSourceInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Mst_SourceInvoice.FlagActive = '0'"
                                , "Check.strListPaymentMethodCode", strListSourceInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_SourceInvoice_FlagActive
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Check Invoice_TempInvoice:
                {
                    string strSql_CheckInvoice_TempInvoice = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_Invoice_ForSTUFF_Exist:
                            select distinct 
	                            t.TInvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Exist
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Invoice_TempInvoice f --//[mylock]
		                            on t.TInvoiceCode = f.TInvoiceCode
                            where(1=1)
	                            and f.TInvoiceCode is null
                            ;

                            --- #tbl_Invoice_Invoice_ForSTUFF_Active:
                            select distinct 
	                            t.TInvoiceCode
                            into #tbl_Invoice_Invoice_ForSTUFF_Active
                            from #input_Invoice_Invoice t --//[mylock]
	                            left join Invoice_TempInvoice f --//[mylock]
		                            on t.TInvoiceCode = f.TInvoiceCode
                            where(1=1)
	                            and f.TInvoiceStatus not in ('ISSUED')
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.TInvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Exist f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListTInvoiceCode
                            where(1=1)
                            ;

                            --- Return:					
                            select 
                                STUFF(( 
		                            SELECT ',' + f.TInvoiceCode
		                            FROM #tbl_Invoice_Invoice_ForSTUFF_Active f --//[mylock]
		                            WHERE(1=1)
		                            FOR
		                            XML PATH('')
		                            ), 1, 1, ''
                                ) AS ListTInvoiceCode
                            where(1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Exist;
                            drop table #tbl_Invoice_Invoice_ForSTUFF_Active;
						");
                    DataSet ds_CheckInvoice_TempInvoice = _cf.db.ExecQuery(
                        strSql_CheckInvoice_TempInvoice
                        );
                    ////
                    DataTable dt_Invoice_Invoice_ForSTUFF_Exist = ds_CheckInvoice_TempInvoice.Tables[0];
                    DataTable dt_Invoice_Invoice_ForSTUFF_Active = ds_CheckInvoice_TempInvoice.Tables[1];
                    /////
                    if (dt_Invoice_Invoice_ForSTUFF_Exist.Rows.Count > 0)
                    {
                        string strListTInvoiceCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Exist.Rows[0]["ListTInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListTInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Invoice_TempInvoice.TInvoiceCode is null"
                                , "Check.strListTInvoiceCode", strListTInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_TempInvoice_NotFound
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                    if (dt_Invoice_Invoice_ForSTUFF_Active.Rows.Count > 0)
                    {
                        string strListTInvoiceCode = TUtils.CUtils.StdParam(dt_Invoice_Invoice_ForSTUFF_Active.Rows[0]["ListTInvoiceCode"]);
                        /////
                        if (!string.IsNullOrEmpty(strListTInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.ErrConditionalRaise", "Invoice_TempInvoice.TInvoiceStatus not in ('ISSUED')"
                                , "Check.strListTInvoiceCode", strListTInvoiceCode
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_TempInvoice_StatusNotMatch
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        /////
                    }
                    ////
                }
                #endregion

                #region ----// Check SL hóa đơn sử dụng:
                {
                    string strSql_CheckInvoice_TempInvoice = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Invoice_TempInvoice:
                            select distinct 
	                            t.TInvoiceCode
                            into #tbl_Invoice_TempInvoice
                            from #input_Invoice_Invoice t --//[mylock]
	                            inner join Invoice_TempInvoice f --//[mylock]
		                            on t.TInvoiceCode = t.TInvoiceCode
                            where(1=1)
                            ;

							---- Return:
							select 
								t.TInvoiceCode
								, f.EndInvoiceNo
								, f.StartInvoiceNo
								, f.QtyUsed
								, (f.EndInvoiceNo - f.StartInvoiceNo + 1 - f.QtyUsed) QtyRemain
							from #tbl_Invoice_TempInvoice t --//[mylock]
								inner join Invoice_TempInvoice f --//[mylock]
									on t.TInvoiceCode = f.TInvoiceCode
							where(1=1)
								and (f.EndInvoiceNo - f.StartInvoiceNo + 1 - f.QtyUsed) < 1
							;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_TempInvoice;
						");
                    DataSet ds_Invoice_TempInvoice = _cf.db.ExecQuery(
                        strSql_CheckInvoice_TempInvoice
                        );
                    ////
                    DataTable dt_Invoice_TempInvoice = ds_Invoice_TempInvoice.Tables[0];
                    /////
                    if (dt_Invoice_TempInvoice.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strTInvoiceCode", dt_Invoice_TempInvoice.Rows[0]["TInvoiceCode"]
                            , "Check.DB.StartInvoiceNo", dt_Invoice_TempInvoice.Rows[0]["StartInvoiceNo"]
                            , "Check.DB.EndInvoiceNo", dt_Invoice_TempInvoice.Rows[0]["EndInvoiceNo"]
                            , "Check.DB.QtyUsed",  dt_Invoice_TempInvoice.Rows[0]["QtyUsed"]
                            , "Check.ErrConditionRaise", "((nEndInvoiceNo - nStartInvoiceNo - nQtyUsed)< 1)"
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_SaveX_Invalid_SourceInvoice_NotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region ----// 20190919. Check EmailSend:
                {
                    string strSqlCheck_InvalidEmailSend = CmUtils.StringUtils.Replace(@"
							--- Check:
							select distinct 
								t.InvoiceCode
							into #tbl_Invoice_Invoice_ForSTUFF
							from #input_Invoice_Invoice t --//[mylock]
							where(1=1)
								and (t.EmailSend is null or LTRIM(RTRIM(t.EmailSend)) = '')
							;

							--- Return:					
							select 
								STUFF(( 
							SELECT ',' + f.InvoiceCode
							FROM #tbl_Invoice_Invoice_ForSTUFF f --//[mylock]
							WHERE(1=1)
							FOR
							XML PATH('')
							), 1, 1, ''
								) AS ListInvoiceCode
							where(1=1)
							;

							--- Clear For Debug:
							drop table #tbl_Invoice_Invoice_ForSTUFF;
						");
                    DataTable dt_CheckInvalidEmailSend = _cf.db.ExecQuery(
                        strSqlCheck_InvalidEmailSend
                        ).Tables[0];
                    /////
                    if (dt_CheckInvalidEmailSend.Rows.Count > 0)
                    {
                        ////
                        string strListInvoiceCode = TUtils.CUtils.StdParam(dt_CheckInvalidEmailSend.Rows[0]["ListInvoiceCode"]);

                        if (!string.IsNullOrEmpty(strListInvoiceCode))
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.strListInvoiceCode", strListInvoiceCode
                                , "Check.ErrConditionalRaise", "and t.FlagCheckCustomer = '1' and (t.EmailSend is null or LTRIM(RTRIM(t.EmailSend)) = '')"
                                , "Check.NumberRows", dt_CheckInvalidEmailSend.Rows.Count
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_Invoice_SaveX_InvalidEmailSend
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region // Build Invoice_Invoice:
            {
                string strSql_Build = CmUtils.StringUtils.Replace(@"
                        ----- #tbl_Invoice_Invoice_Build:
                        select 
	                        t.InvoiceCode
	                        , t.MST
	                        , t.NetworkID
	                        , t.RefNo
	                        , t.FormNo
	                        , t.Sign
	                        , t.SourceInvoiceCode
	                        , t.InvoiceAdjType
	                        , t.InvoiceType2
	                        , t.PaymentMethodCode
	                        , t.CustomerNNTCode
	                        , t.CustomerNNTName
	                        , t.CustomerNNTAddress
	                        , t.CustomerNNTPhone
	                        , t.CustomerNNTBankName
	                        , t.CustomerNNTEmail
	                        , t.CustomerNNTAccNo
	                        , t.CustomerNNTBuyerName
	                        , t.CustomerMST
	                        , t.TInvoiceCode
	                        , t.InvoiceNo
	                        , t.InvoiceDateUTC
	                        , t.EmailSend
	                        , t.InvoiceFileSpec
	                        , t.InvoiceFilePath
	                        , null InvoicePDFFilePath
	                        , t.TotalValInvoice
	                        , t.TotalValVAT
	                        , t.TotalValPmt
	                        , IsNull(f.CreateDTimeUTC, '@strLogLUDTimeUTC') CreateDTimeUTC --t.CreateDTimeUTC
	                        , IsNull(f.CreateBy, '@strLogLUBy') CreateBy --t.CreateBy
	                        , t.InvoiceNoDTimeUTC
	                        , t.InvoiceNoBy
	                        , t.SignDTimeUTC
	                        , t.SignBy
	                        , t.ApprDTimeUTC
	                        , t.ApprBy
	                        , t.CancelDTimeUTC
	                        , t.CancelBy
	                        , t.SendEmailDTimeUTC
	                        , t.SendEmailBy
	                        , t.IssuedDTimeUTC
	                        , t.IssuedBy
                            , null AttachedDelFilePath
                            , null DeleteReason
                            , t.DeleteDTimeUTC
	                        , t.DeleteBy
	                        , t.ChangeDTimeUTC
	                        , t.ChangeBy
	                        , t.InvoiceVerifyCQTCode
	                        , t.CurrencyCode
	                        , t.CurrencyRate
	                        , t.ValGoodsNotTaxable
	                        , t.ValGoodsNotChargeTax
	                        , t.ValGoodsVAT5
	                        , t.ValVAT5
	                        , t.ValGoodsVAT10
	                        , t.ValVAT10
	                        , t.NNTFullName
	                        , t.NNTFullAdress
	                        , t.NNTPhone
	                        , t.NNTFax
	                        , t.NNTEmail
	                        , t.NNTWebsite
	                        , t.NNTAccNo
	                        , t.NNTBankName
	                        , '@strLogLUDTimeUTC' LUDTimeUTC --t.LUDTimeUTC
	                        , '@strLogLUBy' LUBy --t.LUBy
	                        , t.Remark
	                        , t.InvoiceCF1
	                        , t.InvoiceCF2
	                        , t.InvoiceCF3
	                        , t.InvoiceCF4
	                        , t.InvoiceCF5
	                        , t.InvoiceCF6
	                        , t.InvoiceCF7
	                        , t.InvoiceCF8
	                        , t.InvoiceCF9
	                        , t.InvoiceCF10
	                        , t.FlagConfirm
                            , t.FlagCheckCustomer
	                        , '1' FlagChange --t.FlagChange
	                        , null FlagPushOutSite -- t.FlagPushOutSite
	                        , null FlagDeleteOutSite -- t.FlagDeleteOutSite
	                        , '@strInvoiceStatus' InvoiceStatus --t.InvoiceStatus
	                        , '@strLogLUDTimeUTC' LogLUDTimeUTC --t.LogLUDTimeUTC
	                        , '@strLogLUBy' LogLUBy --t.LogLUBy
                        into #tbl_Invoice_Invoice_Build
                        from #input_Invoice_Invoice t --//[mylock]
                            left join Invoice_Invoice f --//[mylock]
                                on t.InvoiceCode = f.InvoiceCode
                        where(1=1)
                        ;

                        select null tbl_Invoice_Invoice_Build, t.* from #tbl_Invoice_Invoice_Build t --//[mylock];
                    ;"
                    , "@strInvoiceStatus", TConst.InvoiceStatus.Pending
                    , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd hh:mm:ss")
                    , "@strLogLUBy", strWAUserCode
                    );
                DataTable dt_Build = _cf.db.ExecQuery(
                    strSql_Build
                    ).Tables[0];
                /////
            }
            #endregion

            #region //// Refine and Check Input Invoice_InvoiceDtl:
            ////
            DataTable dtInput_Invoice_InvoiceDtl = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Invoice_InvoiceDtl";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtlTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_InvoiceDtl = dsData.Tables[strTableCheck];
                ////

                if (dtInput_Invoice_InvoiceDtl.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtlTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_InvoiceDtl // dtData
                    , "StdParam", "InvoiceCode" // arrstrCouple
                    , "StdParam", "Idx" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "StdParam", "SpecCode" // arrstrCouple
                    , "", "SpecName" // arrstrCouple
                    , "StdParam", "ProductID" // arrstrCouple
                    , "", "ProductName" // arrstrCouple
                    , "StdParam", "VATRateCode" // arrstrCouple
                    , "float", "VATRate" // arrstrCouple
                    , "StdParam", "UnitCode" // arrstrCouple
                    , "", "UnitName" // arrstrCouple
                    , "float", "UnitPrice" // arrstrCouple
                    , "float", "Qty" // arrstrCouple
                    , "float", "ValInvoice" // arrstrCouple
                    , "float", "ValTax" // arrstrCouple
                    , "StdParam", "InventoryCode" // arrstrCouple
                    , "float", "DiscountRate" // arrstrCouple
                    , "float", "ValDiscount" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    , "", "InvoiceDCF1" // arrstrCouple
                    , "", "InvoiceDCF2" // arrstrCouple
                    , "", "InvoiceDCF3" // arrstrCouple
                    , "", "InvoiceDCF4" // arrstrCouple
                    , "", "InvoiceDCF5" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_InvoiceDtl, "InvoiceDtlStatus", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_InvoiceDtl, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_InvoiceDtl, "LogLUBy", typeof(object));
                ////
            }
            #endregion

            #region //// SaveTemp Invoice_InvoiceDtl For Check:
            if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Invoice_InvoiceDtl" // strTableName
                    , new object[] {
                            "InvoiceCode", TConst.BizMix.Default_DBColType
                            , "Idx", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "SpecCode", TConst.BizMix.Default_DBColType
                            , "SpecName", TConst.BizMix.Default_DBColType
                            , "ProductID", TConst.BizMix.Default_DBColType
                            , "ProductName", TConst.BizMix.Default_DBColType
                            , "VATRateCode", TConst.BizMix.Default_DBColType
                            , "VATRate", "float"
                            , "UnitCode", TConst.BizMix.Default_DBColType
                            , "UnitName", TConst.BizMix.Default_DBColType
                            , "UnitPrice", "float"
                            , "Qty", "float"
                            , "ValInvoice", "float"
                            , "ValTax", "float"
                            , "InventoryCode", TConst.BizMix.Default_DBColType
                            , "DiscountRate", "float"
                            , "ValDiscount", "float"
                            , "InvoiceDtlStatus", TConst.BizMix.Default_DBColType
                            , "Remark", TConst.BizMix.Default_DBColType
                            , "InvoiceDCF1", TConst.BizMix.Default_DBColType
                            , "InvoiceDCF2", TConst.BizMix.Default_DBColType
                            , "InvoiceDCF3", TConst.BizMix.Default_DBColType
                            , "InvoiceDCF4", TConst.BizMix.Default_DBColType
                            , "InvoiceDCF5", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Invoice_InvoiceDtl // dtData
                );
            }
            #endregion

            #region // Refine and Check Input Invoice_InvoiceDtl:
            if (!bIsDelete)
            {
                #region ----// Check ProductID + SpecCode là duy nhất:
                {
                    string strSql_CheckProducID = CmUtils.StringUtils.Replace(@"                            
                            ---- #tbl_Invoice_InvoiceDtl_TotalProductID:
                            select 
	                            t.InvoiceCode
	                            , t.SpecCode
	                            , t.ProductID
	                            , count(0) QtyProductID
                            into #tbl_Invoice_InvoiceDtl_TotalProductID
                            from #input_Invoice_InvoiceDtl t --//[mylock]
                            where(1=1)
	                            and t.ProductID is not null
                            group by 
	                            t.InvoiceCode
	                            , t.SpecCode
	                            , t.ProductID
                            ;

                            ---- Return:
                            select top 1
	                            t.InvoiceCode
	                            , t.ProductID
	                            , t.SpecCode
	                            , t.QtyProductID
                            from #tbl_Invoice_InvoiceDtl_TotalProductID t --//[mylock]
                            where(1=1)
	                            and t.QtyProductID > 1
                            ;

                            -- Clear For Debug:
                            drop table #tbl_Invoice_InvoiceDtl_TotalProductID;

						");
                    DataTable dt_CheckProductID = _cf.db.ExecQuery(
                        strSql_CheckProducID
                        ).Tables[0];
                    /////
                    if (dt_CheckProductID.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.ErrConditionalRaise", "t.QtyProductID > 1"
                            //, "Check.NumberRows", dt_CheckProductID.Rows.Count
                            , "Check.InvoiceCode", dt_CheckProductID.Rows[0]["InvoiceCode"]
                            , "Check.SpecCode", dt_CheckProductID.Rows[0]["SpecCode"]
                            , "Check.ProductID", dt_CheckProductID.Rows[0]["ProductID"]
                            , "Check.QtyProductID", dt_CheckProductID.Rows[0]["QtyProductID"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtl_ProductIDDuplicate
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region ----// Check SpecCode là duy nhất:
                {
                    string strSql_CheckSpec = CmUtils.StringUtils.Replace(@"                         
                            ---- #tbl_Invoice_InvoiceDtl_TotalSpec:
                            select 
	                            t.InvoiceCode
	                            , t.SpecCode
	                            , count(0) QtySpecCode
                            into #tbl_Invoice_InvoiceDtl_TotalSpec
                            from #input_Invoice_InvoiceDtl t --//[mylock]
                            where(1=1)
	                            and t.ProductID is null
								and t.SpecCode is not null
                            group by 
	                            t.InvoiceCode
	                            , t.SpecCode
                            ;

                            ---- Return:
                            select top 1
	                            t.InvoiceCode
	                            , t.SpecCode
	                            , t.QtySpecCode
                            from #tbl_Invoice_InvoiceDtl_TotalSpec t --//[mylock]
                            where(1=1)
	                            and t.QtySpecCode > 1
                            ;

                            -- Clear For Debug:
                            drop table #tbl_Invoice_InvoiceDtl_TotalSpec;

						");
                    DataTable dt_CheckSpecCode = _cf.db.ExecQuery(
                        strSql_CheckSpec
                        ).Tables[0];
                    /////
                    if (dt_CheckSpecCode.Rows.Count > 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.ErrConditionalRaise", "t.QtyProductID > 1"
                            //, "Check.NumberRows", dt_CheckProductID.Rows.Count
                            , "Check.InvoiceCode", dt_CheckSpecCode.Rows[0]["InvoiceCode"]
                            , "Check.SpecCode", dt_CheckSpecCode.Rows[0]["SpecCode"]
                            , "Check.QtySpecCode", dt_CheckSpecCode.Rows[0]["QtySpecCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtl_SpecCodeDuplicate
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

            }
            #endregion

            #region // Build Invoice_InvoiceDtl:
            if (!bIsDelete)
            {
                string strSql_Build = CmUtils.StringUtils.Replace(@"
                        ----- #tbl_Invoice_InvoiceDtl_Build:
                        select 
	                        t.InvoiceCode
							, Row_Number() over( partition by t.InvoiceCode order by t.Idx asc) Idx 
							--, Row_Number() over (order by t.Idx desc) Idx 
	                        --, t.Idx
	                        , t.NetworkID
	                        , t.SpecCode
	                        , t.SpecName
	                        , t.ProductID
	                        , t.ProductName
	                        , t.VATRateCode
	                        , t.VATRate
	                        , t.UnitCode
	                        , t.UnitName
	                        , t.UnitPrice
	                        , t.Qty
	                        , t.ValInvoice
	                        , t.ValTax
	                        , t.InventoryCode
	                        , t.DiscountRate
	                        , t.ValDiscount
	                        , '@strInvoiceStatus' InvoiceDtlStatus --t.InvoiceDtlStatus
	                        , t.Remark
	                        , t.InvoiceDCF1
	                        , t.InvoiceDCF2
	                        , t.InvoiceDCF3
	                        , t.InvoiceDCF4
	                        , t.InvoiceDCF5
	                        , '@strLogLUDTimeUTC' LogLUDTimeUTC --t.LogLUDTimeUTC
	                        , '@strLogLUBy' LogLUBy --t.LogLUBy
                        into #tbl_Invoice_InvoiceDtl_Build
                        from #input_Invoice_InvoiceDtl t --//[mylock]

                        select null tbl_Invoice_InvoiceDtl_Build, t.* from #tbl_Invoice_InvoiceDtl_Build t --//[mylock];
                    ;"
                    , "@strInvoiceStatus", TConst.InvoiceStatus.Pending
                    , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "@strLogLUBy", strWAUserCode
                    );
                DataTable dt_Build = _cf.db.ExecQuery(
                    strSql_Build
                    ).Tables[0];
                /////
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_Invoice_InvoiceDtl:
                            select 
                                t.InvoiceCode
                                , t.Idx
                            into #tbl_Invoice_InvoiceDtl
                            from Invoice_InvoiceDtl t --//[mylock]
	                            inner join #input_Invoice_Invoice f --//[mylock]
		                            on t.InvoiceCode = f.InvoiceCode
                            where (1=1)
                            ;

                            --- Delete:
                            ---- Invoice_InvoiceDtl:
                            delete t 
                            from Invoice_InvoiceDtl t --//[mylock]
	                            inner join #tbl_Invoice_InvoiceDtl f --//[mylock]
		                            on t.InvoiceCode = f.InvoiceCode
		                                and t.Idx = f.Idx
                            where (1=1)
                            ;

                            ---- Invoice_Invoice:
                            delete t
                            from Invoice_Invoice t --//[mylock]
	                            inner join #input_Invoice_Invoice f --//[mylock]
		                            on t.InvoiceCode = f.InvoiceCode
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_InvoiceDtl;
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
                        string zzzzClauseInsert_Invoice_Invoice_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_Invoice:                                
                                insert into Invoice_Invoice(
	                                InvoiceCode
	                                , MST
	                                , NetworkID
	                                , RefNo
	                                , FormNo
	                                , Sign
	                                , SourceInvoiceCode
	                                , InvoiceAdjType
	                                , PaymentMethodCode
	                                , InvoiceType2
	                                , CustomerNNTCode
	                                , CustomerNNTName
	                                , CustomerNNTAddress
	                                , CustomerNNTPhone
	                                , CustomerNNTBankName
	                                , CustomerNNTEmail
	                                , CustomerNNTAccNo
	                                , CustomerNNTBuyerName
	                                , CustomerMST
	                                , TInvoiceCode
	                                , InvoiceNo
	                                , InvoiceDateUTC
	                                , EmailSend
	                                , InvoiceFileSpec
	                                , InvoiceFilePath
	                                , InvoicePDFFilePath
	                                , TotalValInvoice
	                                , TotalValVAT
	                                , TotalValPmt
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , InvoiceNoDTimeUTC
	                                , InvoiceNoBy
	                                , SignDTimeUTC
	                                , SignBy
	                                , ApprDTimeUTC
	                                , ApprBy
	                                , CancelDTimeUTC
	                                , CancelBy
	                                , SendEmailDTimeUTC
	                                , SendEmailBy
	                                , IssuedDTimeUTC
	                                , IssuedBy
	                                , AttachedDelFilePath
	                                , DeleteReason
	                                , DeleteDTimeUTC
	                                , DeleteBy
	                                , ChangeDTimeUTC
	                                , ChangeBy
	                                , InvoiceVerifyCQTCode
	                                , CurrencyCode
	                                , CurrencyRate
	                                , ValGoodsNotTaxable
	                                , ValGoodsNotChargeTax
	                                , ValGoodsVAT5
	                                , ValVAT5
	                                , ValGoodsVAT10
	                                , ValVAT10
	                                , NNTFullName
	                                , NNTFullAdress
	                                , NNTPhone
	                                , NNTFax
	                                , NNTEmail
	                                , NNTWebsite
	                                , NNTAccNo
	                                , NNTBankName
	                                , LUDTimeUTC
	                                , LUBy
	                                , Remark
	                                , InvoiceCF1
	                                , InvoiceCF2
	                                , InvoiceCF3
	                                , InvoiceCF4
	                                , InvoiceCF5
	                                , InvoiceCF6
	                                , InvoiceCF7
	                                , InvoiceCF8
	                                , InvoiceCF9
	                                , InvoiceCF10
	                                , FlagChange
	                                , FlagPushOutSite
	                                , FlagDeleteOutSite
	                                , InvoiceStatus
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                    , FlagCheckCustomer
                                    , FlagConfirm
                                )
                                select 
	                                t.InvoiceCode
	                                , t.MST
	                                , t.NetworkID
	                                , t.RefNo
	                                , t.FormNo
	                                , t.Sign
	                                , t.SourceInvoiceCode
	                                , t.InvoiceAdjType
	                                , t.PaymentMethodCode
	                                , t.InvoiceType2
	                                , t.CustomerNNTCode
	                                , t.CustomerNNTName
	                                , t.CustomerNNTAddress
	                                , t.CustomerNNTPhone
	                                , t.CustomerNNTBankName
	                                , t.CustomerNNTEmail
	                                , t.CustomerNNTAccNo
	                                , t.CustomerNNTBuyerName
	                                , t.CustomerMST
	                                , t.TInvoiceCode
	                                , t.InvoiceNo
	                                , t.InvoiceDateUTC
	                                , t.EmailSend
	                                , t.InvoiceFileSpec
	                                , t.InvoiceFilePath
	                                , t.InvoicePDFFilePath
	                                , t.TotalValInvoice
	                                , t.TotalValVAT
	                                , t.TotalValPmt
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.InvoiceNoDTimeUTC
	                                , t.InvoiceNoBy
	                                , t.SignDTimeUTC
	                                , t.SignBy
	                                , t.ApprDTimeUTC
	                                , t.ApprBy
	                                , t.CancelDTimeUTC
	                                , t.CancelBy
	                                , t.SendEmailDTimeUTC
	                                , t.SendEmailBy
	                                , t.IssuedDTimeUTC
	                                , t.IssuedBy
	                                , t.AttachedDelFilePath
	                                , t.DeleteReason
	                                , t.DeleteDTimeUTC
	                                , t.DeleteBy
	                                , t.ChangeDTimeUTC
	                                , t.ChangeBy
	                                , t.InvoiceVerifyCQTCode
	                                , t.CurrencyCode
	                                , t.CurrencyRate
	                                , t.ValGoodsNotTaxable
	                                , t.ValGoodsNotChargeTax
	                                , t.ValGoodsVAT5
	                                , t.ValVAT5
	                                , t.ValGoodsVAT10
	                                , t.ValVAT10
	                                , t.NNTFullName
	                                , t.NNTFullAdress
	                                , t.NNTPhone
	                                , t.NNTFax
	                                , t.NNTEmail
	                                , t.NNTWebsite
	                                , t.NNTAccNo
	                                , t.NNTBankName
	                                , t.LUDTimeUTC
	                                , t.LUBy
	                                , t.Remark
	                                , t.InvoiceCF1
	                                , t.InvoiceCF2
	                                , t.InvoiceCF3
	                                , t.InvoiceCF4
	                                , t.InvoiceCF5
	                                , t.InvoiceCF6
	                                , t.InvoiceCF7
	                                , t.InvoiceCF8
	                                , t.InvoiceCF9
	                                , t.InvoiceCF10
	                                , t.FlagChange
	                                , t.FlagPushOutSite
	                                , t.FlagDeleteOutSite
	                                , t.InvoiceStatus
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                    , t.FlagCheckCustomer
                                    , t.FlagConfirm
                                from #tbl_Invoice_Invoice_Build t --//[mylock]
                            ");
                        /////
                        string zzzzClauseInsert_Invoice_InvoiceDtl_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_InvoiceDtl:  
                                insert into Invoice_InvoiceDtl(
	                                InvoiceCode
	                                , Idx
	                                , NetworkID
	                                , SpecCode
	                                , SpecName
	                                , ProductID
	                                , ProductName
	                                , VATRateCode
	                                , VATRate
	                                , UnitCode
	                                , UnitName
	                                , UnitPrice
	                                , Qty
	                                , ValInvoice
	                                , ValTax
	                                , InventoryCode
	                                , DiscountRate
	                                , ValDiscount
	                                , InvoiceDtlStatus
	                                , Remark
	                                , InvoiceDCF1
	                                , InvoiceDCF2
	                                , InvoiceDCF3
	                                , InvoiceDCF4
	                                , InvoiceDCF5
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.InvoiceCode
	                                , t.Idx
	                                , t.NetworkID
	                                , t.SpecCode
	                                , t.SpecName
	                                , t.ProductID
	                                , t.ProductName
	                                , t.VATRateCode
	                                , t.VATRate
	                                , t.UnitCode
	                                , t.UnitName
	                                , t.UnitPrice
	                                , t.Qty
	                                , t.ValInvoice
	                                , t.ValTax
	                                , t.InventoryCode
	                                , t.DiscountRate
	                                , t.ValDiscount
	                                , t.InvoiceDtlStatus
	                                , t.Remark
	                                , t.InvoiceDCF1
	                                , t.InvoiceDCF2
	                                , t.InvoiceDCF3
	                                , t.InvoiceDCF4
	                                , t.InvoiceDCF5
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #tbl_Invoice_InvoiceDtl_Build t
                            ");

                        /////
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Invoice_Invoice_zSave			
								----
								zzzzClauseInsert_Invoice_InvoiceDtl_zSave			
								----
							"
                            , "zzzzClauseInsert_Invoice_Invoice_zSave", zzzzClauseInsert_Invoice_Invoice_zSave
                            , "zzzzClauseInsert_Invoice_InvoiceDtl_zSave", zzzzClauseInsert_Invoice_InvoiceDtl_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region // Check One RefNo:
            if (!bIsDelete)
            {
                myCheck_Invoice_Invoice_RefNo_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , "#tbl_Invoice_Invoice_Build"  //zzzz_tbl_Invoice_Invoice_RefNo
                    );
            }
            #endregion

            #region // myCheck_Invoice_Invoice_Total:
            if (!bIsDelete)
            {
                myCheck_Invoice_Invoice_Total_New20190905(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , "#tbl_Invoice_Invoice_Build"  //zzzz_tbl_Invoice_Invoice_RefNo
                    );
                ////
                myCheck_Invoice_InvoiceDtl_Total_New20190905(
                    ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , "#tbl_Invoice_Invoice_Build"  //zzzz_tbl_Invoice_Invoice_RefNo
                    );
            }
            #endregion

            #region // myCheck_OS_Invoice_Invoice_CheckMasterPrd:


            if (!bIsDelete)
            {
                /*Ngày 2019-08-02:
                * A @HuongNV chốt: Client truyền thêm 1 cờ xác nhận (FlagConfirm) check hàng hóa:
                    + Cờ FlagConfirm chỉ truyền vào không lưu trong DB.
                    + Nếu FlagConfirm = 1 => Không thay đổi luật check hiện tại tính đến trước ngày 2019-08-02.
                    + Nếu FlagConfirm = 0 => Bỏ luật check các FK hàng hóa trừ SpecCode.
                */
                ////
                string strFlagConfirm = dtInput_Invoice_Invoice.Rows[0]["FlagConfirm"].ToString();

                if (CmUtils.StringUtils.StringEqual(strFlagConfirm, TConst.Flag.Active))
                {
                    myCheck_OS_Invoice_Invoice_CheckMasterPrd(
                    ref alParamsCoupleError  //alParamsCoupleError
                    , dtimeSys  //dtimeSys
                    , strTid  //strTid
                    , "#tbl_Invoice_Invoice_Build"  //zzzz_tbl_Invoice_Invoice_RefNo
                    );
                }
                else
                {
                    myCheck_OS_Invoice_Invoice_CheckMasterPrd_NotConfirm(
                    ref alParamsCoupleError  //alParamsCoupleError
                    , dtimeSys  //dtimeSys
                    , strTid  //strTid
                    , "#tbl_Invoice_Invoice_Build"  //zzzz_tbl_Invoice_Invoice_RefNo
                    );
                }
            }
            #endregion

            #region //// Clear For Debug:
            if (!bIsDelete)
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Invoice_Invoice;
						drop table #input_Invoice_InvoiceDtl;
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
						drop table #input_Invoice_Invoice;
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

        public DataSet WAS_Invoice_Invoice_ApprovedAndIssued(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_Invoice objRQ_Invoice_Invoice
            ////
            , out RT_Invoice_Invoice objRT_Invoice_Invoice
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_Invoice.Tid;
            objRT_Invoice_Invoice = new RT_Invoice_Invoice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Invoice_Invoice_ApprovedAndIssued";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_Invoice_ApprovedAndIssued;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "Lst_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice.Lst_Invoice_Invoice)
                , "Lst_Invoice_InvoiceDtl", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice.Lst_Invoice_InvoiceDtl)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                //List<Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
                //List<Invoice_InvoicePrd> lst_Invoice_InvoicePrd = new List<Invoice_InvoicePrd>();
                /////
                bool bGet_Invoice_Invoice = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice.Length > 0);
                //bool bGet_Invoice_InvoiceDtl = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl.Length > 0);
                //bool bGet_Invoice_InvoicePrd = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd.Length > 0);
                #endregion

                #region // Refine and Check Input:
                //List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_Invoice_Invoice.Lst_Invoice_Invoice == null)
                        objRQ_Invoice_Invoice.Lst_Invoice_Invoice = new List<Invoice_Invoice>();
                    {
                        DataTable dt_InvoiceInvoice = TUtils.DataTableCmUtils.ToDataTable<Invoice_Invoice>(objRQ_Invoice_Invoice.Lst_Invoice_Invoice, "Invoice_Invoice");
                        dsData.Tables.Add(dt_InvoiceInvoice);
                    }
                    if (objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailTo == null)
                        objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailTo = new List<Email_BatchSendEmailTo>();
                    {
                        DataTable dt_Email_BatchSendEmailTo = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailTo>(objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailTo, "Email_BatchSendEmailTo");
                        dsData.Tables.Add(dt_Email_BatchSendEmailTo);
                    }
                    ////
                    if (objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailCC == null)
                        objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailCC = new List<Email_BatchSendEmailCC>();
                    {
                        DataTable dt_Email_BatchSendEmailCC = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailCC>(objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailCC, "Email_BatchSendEmailCC");
                        dsData.Tables.Add(dt_Email_BatchSendEmailCC);

                    }
                    ////
                    if (objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailBCC == null)
                        objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailBCC = new List<Email_BatchSendEmailBCC>();
                    {
                        DataTable dt_Email_BatchSendEmailBCC = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailBCC>(objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailBCC, "Email_BatchSendEmailBCC");
                        dsData.Tables.Add(dt_Email_BatchSendEmailBCC);

                    }
                    ////
                    if (objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailFileAttach == null)
                        objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailFileAttach = new List<Email_BatchSendEmailFileAttach>();
                    {
                        DataTable dt_Email_BatchSendEmailFileAttach = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailFileAttach>(objRQ_Invoice_Invoice.Lst_Email_BatchSendEmailFileAttach, "Email_BatchSendEmailFileAttach");
                        dsData.Tables.Add(dt_Email_BatchSendEmailFileAttach);

                    }
                    ////
                    if (objRQ_Invoice_Invoice.Email_BatchSendEmail == null)
                        objRQ_Invoice_Invoice.Email_BatchSendEmail = new Email_BatchSendEmail();
                }
                #endregion

                #region // Invoice_Invoice_ApprovedAndIssued:
                mdsResult = Invoice_Invoice_ApprovedAndIssued(
                    objRQ_Invoice_Invoice.Tid // strTid
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    , objRQ_Invoice_Invoice.WAUserCode // strUserCode
                    , objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
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

        public DataSet Invoice_Invoice_ApprovedAndIssued(
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
            string strFunctionName = "Invoice_Invoice_ApprovedAndIssued";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_Invoice_ApprovedAndIssued;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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

                #region // Invoice_Invoice_ApprovedX:
                //DataSet dsGetData = null;
                {
                    Invoice_Invoice_ApprovedMultiX(
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

                #region // Invoice_Invoice_IssuedX:
                //DataSet dsGetData = null;
                {
                    Invoice_Invoice_IssuedXMulti(
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

                #region // Email_BatchSendEmail_SaveAndSendX:
                {
                    //Email_BatchSendEmail_SaveAndSendX(
                    //    strTid // strTid
                    //    , strGwUserCode // strGwUserCode
                    //    , strGwPassword // strGwPassword
                    //    , strWAUserCode // strWAUserCode
                    //    , strWAUserPassword // strWAUserPassword
                    //    , ref alParamsCoupleError // alParamsCoupleError
                    //    , dtimeSys // dtimeSys
                    //               ////
                    //    , objSubject // objSubject
                    //    , objBodyText // objBodyText
                    //    , objBodyHTML // objBodyHTML
                    //    , dsData // dsData
                    //    );
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

        private void Invoice_Invoice_IssuedXMulti(
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
            string strFunctionName = "Invoice_Invoice_IssuedX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Invoice_Invoice_IssuedX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
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
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and Check Input Invoice_Invoice:
            ////
            DataTable dtInput_Invoice_Invoice = null;
            {
                ////
                string strTableCheck = "Invoice_Invoice";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_IssuedXMulti_Input_InvoiceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_Invoice = dsData.Tables[strTableCheck];
                if (dtInput_Invoice_Invoice.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[] {
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_IssuedMultiX_Input_InvoiceTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_Invoice //dtData
                    , "stdParam", "InvoiceCode" // arrstrCouple
                    , "", "EmailSend" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "LogLUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "IssuedDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "IssuedBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SendEmailDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "SendEmailBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_Invoice, "InvoiceStatus", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Invoice_Invoice.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Invoice_Invoice.Rows[nScan];
                    ////
                    DataTable dtDB_Invoice_Invoice = null;

                    Invoice_Invoice_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , drScan["InvoiceCode"] // objInvoiceCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.InvoiceStatus.Approved // strInvoiceStatusListToCheck
                            , out dtDB_Invoice_Invoice // dtDB_Invoice_Invoice
                            );
                    ////
                    string strMST = TUtils.CUtils.StdParam(dtDB_Invoice_Invoice.Rows[0]["MST"]);
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["MST"], strMST)
                        && !CmUtils.StringUtils.StringEqualIgnoreCase(drAbilityOfUser["FlagBG"], TConst.Flag.Active))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.DB.NNT.MST", drAbilityOfUser["MST"]
                            , "Check.strMST", strMST
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_Invoice_Issued_InvalidMST
                            , null
                            , alParamsCoupleError.ToArray()
                            );

                    }
                    ////
                    drScan["SendEmailDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["SendEmailBy"] = strWAUserCode;
                    drScan["IssuedDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["IssuedBy"] = strWAUserCode;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    drScan["InvoiceStatus"] = "ISSUED";

                }
            }

            #endregion

            #region // SaveTemp Invoice_Invoice:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Invoice_Invoice"
                    //, TConst.BizMix.Default_DBColType // strDefaultType
                    , new object[]{
                        "InvoiceCode", TConst.BizMix.Default_DBColType
                        , "EmailSend", TConst.BizMix.Default_DBColType
                        , "Remark", TConst.BizMix.Default_DBColType
                        , "InvoiceStatus", TConst.BizMix.Default_DBColType
                        , "SendEmailDTimeUTC", TConst.BizMix.Default_DBColType
                        , "SendEmailBy", TConst.BizMix.Default_DBColType
                        , "IssuedDTimeUTC", TConst.BizMix.Default_DBColType
                        , "IssuedBy", TConst.BizMix.Default_DBColType
                        , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                        , "LogLUBy", TConst.BizMix.Default_DBColType
                        }
                    , dtInput_Invoice_Invoice
                );

            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzB_Update_Invoice_Invoice_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.IssuedDTimeUTC = f.IssuedDTimeUTC
						, t.IssuedBy = f.IssuedBy
						--, t.SendEmailDTimeUTC = f.SendEmailDTimeUTC
						--, t.SendEmailBy = f.SendEmailBy
						, t.InvoiceStatus = f.InvoiceStatus
						, t.EmailSend = f.EmailSend
						, t.Remark = f.Remark
						";
                ////
                string zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE = @"
						t.LogLUDTimeUTC = f.LogLUDTimeUTC
						, t.LogLUBy = f.LogLUBy
						, t.InvoiceDtlStatus = f.InvoiceStatus
						";
                ////
                //          string zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE = @"
                //t.LogLUDTimeUTC = f.LogLUDTimeUTC
                //, t.LogLUBy = f.LogLUBy
                //, t.InvoicePrdStatus = f.InvoiceStatus
                //";
                ////
                string zzB_Update_Invoice_Invoice_zzE = CmUtils.StringUtils.Replace(@"
						---- Invoice_Invoice:
						update t
						set 
							zzB_Update_Invoice_Invoice_ClauseSet_zzE
						from Invoice_Invoice t --//[mylock]
							inner join #input_Invoice_Invoice f --//[mylock]
								on t.InvoiceCode = f.InvoiceCode
						where (1=1)
						;
					"
                    , "zzB_Update_Invoice_Invoice_ClauseSet_zzE", zzB_Update_Invoice_Invoice_ClauseSet_zzE
                    );
                ////
                string zzB_Update_Invoice_InvoiceDtl_zzE = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_Invoice_InvoiceDtl_Temp: 
                            select 
                                t.InvoiceCode
                                , t.Idx
                                , t.SpecCode
                                , f.InvoiceStatus
                                , f.LogLUDTimeUTC
                                , f.LogLUBy
                            into #tbl_Invoice_InvoiceDtl_Temp
							from Invoice_InvoiceDtl t --//[mylock]
							    inner join #input_Invoice_Invoice f --//[mylock]
								    on t.InvoiceCode = f.InvoiceCode
							where (1=1)
							;

                            ---- Update:
							update t
							set 
								zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
							from Invoice_InvoiceDtl t --//[mylock]
							    inner join #tbl_Invoice_InvoiceDtl_Temp f --//[mylock]
								    on t.InvoiceCode = f.InvoiceCode
								        and t.Idx = f.Idx
							where (1=1)
							;
						"
                        , "zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE", zzB_Update_Invoice_InvoiceDtl_ClauseSet_zzE
                        );
                ////				
                //           string zzB_Update_Invoice_InvoicePrd_zzE = CmUtils.StringUtils.Replace(@"
                //                   ---- #tbl_Invoice_InvoicePrd_Temp: 
                //                   select 
                //                       t.InvoiceCode
                //                       , t.ProductID
                //                       , t.SpecCode
                //                       , f.InvoiceStatus
                //                       , f.LogLUDTimeUTC
                //                       , f.LogLUBy
                //                   into #tbl_Invoice_InvoicePrd_Temp
                //	from Invoice_InvoicePrd t --//[mylock]
                //		inner join #input_Invoice_Invoice f --//[mylock]
                //			on t.InvoiceCode = f.InvoiceCode
                //	where (1=1)
                //	;

                //                   ---- Update:
                //	update t
                //	set 
                //		zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE
                //	from Invoice_InvoicePrd t --//[mylock]
                //		inner join #tbl_Invoice_InvoicePrd_Temp f --//[mylock]
                //			on t.InvoiceCode = f.InvoiceCode
                //			    and t.ProductID = f.ProductID
                //			    and t.SpecCode = f.SpecCode
                //	where (1=1)
                //	;
                //"
                //               , "zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE", zzB_Update_Invoice_InvoicePrd_ClauseSet_zzE
                //               );
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
						----
						zzB_Update_Invoice_Invoice_zzE
                        ----
						zzB_Update_Invoice_InvoiceDtl_zzE
                        ----
					"
                    , "zzB_Update_Invoice_Invoice_zzE", zzB_Update_Invoice_Invoice_zzE
                    , "zzB_Update_Invoice_InvoiceDtl_zzE", zzB_Update_Invoice_InvoiceDtl_zzE
                    );

                DataSet dsDB_Check = _cf.db.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion

            #region // Call WS All Network phục vụ hóa đơn đầu vào:
            string strCustomerMST = TUtils.CUtils.StdParam(dtInput_Invoice_Invoice.Rows[0]["CustomerMST"]);
            {
                #region // Get Data:
                DataSet dataSet_GetInvoice = new DataSet();
                {
                    string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
                            ---- Invoice_Invoice: 
                            select 
                                t.*
							from Invoice_Invoice t --//[mylock]
							    inner join #input_Invoice_Invoice f --//[mylock]
								    on t.InvoiceCode = f.InvoiceCode
							where (1=1)
							;

                            ---- Invoice_InvoiceDtl: 
                            select 
                                t.*
							from Invoice_InvoiceDtl t --//[mylock]
							    inner join #input_Invoice_Invoice f --//[mylock]
								    on t.InvoiceCode = f.InvoiceCode
							where (1=1)
							;
					    "
                        );

                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_SaveOnDB
                        );
                    ////
                    DataTable dtData_Invoice_Invoice = new DataTable("Invoice_InvoiceInput");
                    dtData_Invoice_Invoice = dsDB_Check.Tables[0].Copy();
                    dataSet_GetInvoice.Tables.Add(dtData_Invoice_Invoice);
                    ////
                    DataTable dtData_Invoice_InvoiceDtl = new DataTable("Invoice_InvoiceInputDtl");
                    dtData_Invoice_InvoiceDtl = dsDB_Check.Tables[1].Copy();
                    dataSet_GetInvoice.Tables.Add(dtData_Invoice_InvoiceDtl);
                    ////
                    dataSet_GetInvoice.Tables[0].TableName = "Invoice_InvoiceInput";
                    dataSet_GetInvoice.Tables[1].TableName = "Invoice_InvoiceInputDtl";

                }
                #endregion

                #region // OS_TVAN_Invoice_InvoiceInput_CallIssuesd:
                {
                    OS_TVAN_Invoice_InvoiceInput_CallIssuesd(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , nNetworkID.ToString() // nNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , dtimeSys // dtimeSys
                        , strCustomerMST // strCustomerMST
                        , dataSet_GetInvoice // dataSet
                        );
                }
                #endregion
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Invoice_Invoice;
						drop table #tbl_Invoice_InvoiceDtl_Temp;
						drop table #tbl_Mst_NNT_ViewAbility;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

        }

        public void Invoice_Invoice_GetInvoiceNoX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            , DataSet dsData
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "Invoice_Invoice_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                });
            #endregion

            #region // Check:
            #endregion

            #region // Refine and Check Input Invoice_Invoice:
            ////
            DataTable dtInput_Invoice_Invoice = null;
            {
                ////
                string strTableCheck = "Invoice_Invoice";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Invoice_Invoice = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Invoice_Invoice.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                TUtils.CUtils.StdDataInTable(
                    dtInput_Invoice_Invoice // dtData
                    , "StdParam", "InvoiceCode" // arrstrCouple
                    );
            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            //if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Invoice_Invoice" // strTableName
                    , new object[] {
                            "InvoiceCode", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Invoice_Invoice // dtData
                );
            }
            #endregion

            #region // Build Sql:
            
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #input_Invoice_Invoice:
						select
                            t.InvoiceCode
                            , f.InvoiceNo
                        from #input_Invoice_Invoice t --//[mylock]
                            inner join Invoice_Invoice f --//[mylock]
                                on t.InvoiceCode = f.InvoiceCode
                        where (1=1)
					"
                );
            ////
            DataTable dtInvoice_Invoice = _cf.db.ExecQuery(strSqlGetData).Tables[0];
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "Invoice_Invoice";
            //if (bGet_Invoice_Invoice)
            //{
            //    dsGetData.Tables[nIdxTable++].TableName = "Invoice_Invoice";
            //}
            #endregion

        }
        #endregion

        #region // InvoiceTGroupCode:
        public DataSet WAS_Invoice_TempGroup_Update_New20190925(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_TempGroup objRQ_Invoice_TempGroup
            ////
            , out RT_Invoice_TempGroup objRT_Invoice_TempGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_TempGroup.Tid;
            objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Invoice_TempGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_TempGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup.Invoice_TempGroup)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Invoice_TempGroup> lst_Invoice_TempGroup = new List<Invoice_TempGroup>();
                //List<Invoice_TempGroupInGroup> lst_Invoice_TempGroupInGroup = new List<Invoice_TempGroupInGroup>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Invoice_TempCustomField = TUtils.DataTableCmUtils.ToDataTable<Invoice_TempGroupField>(objRQ_Invoice_TempGroup.Lst_Invoice_TempGroupField, "Invoice_TempGroupField");
                    dsData.Tables.Add(dt_Invoice_TempCustomField);

                }
                #endregion

                #region // Invoice_TempGroup_Update:
                mdsResult = Invoice_TempGroup_Update_New20190925(
                    objRQ_Invoice_TempGroup.Tid // strTid
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    , objRQ_Invoice_TempGroup.WAUserCode // strUserCode
                    , objRQ_Invoice_TempGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupCode // objInvoiceTGroupCode
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupName // objInvoiceTGroupName
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupBody // objInvoiceTGroupBody
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.FilePathThumbnail // objFilePathThumbnail
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.Spec_Prd_Type // objSpec_Prd_Type
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.FlagActive // objFlagActive
                                                                           /////
                    , dsData // dsData
                             ////
                    , objRQ_Invoice_TempGroup.Ft_Cols_Upd// objFt_Cols_Upd
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

        public DataSet Invoice_TempGroup_Update_New20190925(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objInvoiceTGroupCode
            //, object objMST
            , object objInvoiceTGroupName
            , object objInvoiceTGroupBody
            , object objFilePathThumbnail
            , object objSpec_Prd_Type
            , object objFlagActive
            ////
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_TempGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_TempGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objInvoiceTGroupCode", objInvoiceTGroupCode
                //, "objMST", objMST
                , "objInvoiceTGroupName", objInvoiceTGroupName
                , "objInvoiceTGroupBody", objInvoiceTGroupBody
                , "objFilePathThumbnail", objFilePathThumbnail
                , "objSpec_Prd_Type", objSpec_Prd_Type
                , "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

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

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strInvoiceTGroupCode = TUtils.CUtils.StdParam(objInvoiceTGroupCode);
                //string strMST = TUtils.CUtils.StdParam(objMST);
                string strInvoiceTGroupName = string.Format("{0}", objInvoiceTGroupName).Trim();
                string strInvoiceTGroupBody = string.Format("{0}", objInvoiceTGroupBody).Trim();
                string strFilePathThumbnail = string.Format("{0}", objFilePathThumbnail).Trim();
                string strSpec_Prd_Type = TUtils.CUtils.StdParam(objSpec_Prd_Type);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                bool bUpd_InvoiceTGroupName = strFt_Cols_Upd.Contains("Invoice_TempGroup.InvoiceTGroupName".ToUpper());
                bool bUpd_InvoiceTGroupBody = strFt_Cols_Upd.Contains("Invoice_TempGroup.InvoiceTGroupBody".ToUpper());
                bool bUpd_FilePathThumbnail = strFt_Cols_Upd.Contains("Invoice_TempGroup.FilePathThumbnail".ToUpper());
                bool bUpd_Spec_Prd_Type = strFt_Cols_Upd.Contains("Invoice_TempGroup.Spec_Prd_Type".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Invoice_TempGroup.FlagActive".ToUpper());

                ////
                DataTable dtDB_Invoice_TempGroup = null;
                {
                    ////
                    Invoice_TempGroup_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strInvoiceTGroupCode // objInvoiceTGroupCode 
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Invoice_TempGroup // dtDB_Invoice_TempGroup
                        );
                    ////
                    if (bUpd_InvoiceTGroupName && string.IsNullOrEmpty(strInvoiceTGroupName))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strInvoiceTGroupName", strInvoiceTGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Update_InvalidProvinceName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    ////
                    if (CmUtils.StringUtils.StringEqualIgnoreCase(strSpec_Prd_Type, TConst.Spec_Prd_Type.Spec)
                        && CmUtils.StringUtils.StringEqualIgnoreCase(strSpec_Prd_Type, TConst.Spec_Prd_Type.ProductId))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strSpec_Prd_Type", strInvoiceTGroupName
                            , "Check.Expected", string.Format("{0}, {1}", TConst.Spec_Prd_Type.Spec, TConst.Spec_Prd_Type.ProductId).Trim()
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Update_InvalidSpec_Prd_Type
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveTemp Invoice_TempGroup:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#Input_Invoice_TempGroup"
                        , new object[]{
                            "InvoiceTGroupCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "MST", TConst.BizMix.Default_DBColType,
                            "VATType", TConst.BizMix.Default_DBColType,
                            "InvoiceTGroupName", TConst.BizMix.Default_DBColType,
                            "InvoiceTGroupBody", TConst.BizMix.Max_DBCol,
                            "FilePathThumbnail", TConst.BizMix.Default_DBColType,
                            "Spec_Prd_Type", TConst.BizMix.Default_DBColType,
                            "FlagActive", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , new object[] {
                                new object[]{
                                    strInvoiceTGroupCode, // InvoiceTGroupCode
                                    nNetworkID, // NetworkID
                                    dtDB_Invoice_TempGroup.Rows[0]["MST"], // MST
                                    dtDB_Invoice_TempGroup.Rows[0]["VATType"], // VATType
                                    strInvoiceTGroupName, // InvoiceTGroupName
                                    strInvoiceTGroupBody, // InvoiceTGroupBody
                                    strFilePathThumbnail, // FilePathThumbnail
                                    strSpec_Prd_Type, // Spec_Prd_Type
                                    TConst.Flag.Active, // FlagActive
                                    dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                    strWAUserCode, // LogLUBy
                                    }
                            }
                        );
                }
                #endregion

                #region // Refine and Check Invoice_TempGroupField:
                DataTable dtInput_Invoice_TempGroupField = null;
                {
                    ////
                    string strTableCheck = "Invoice_TempGroupField";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Update_InvalidSpec_TempGroupFieldNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Invoice_TempGroupField = dsData.Tables[strTableCheck];
                    ////
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Invoice_TempGroupField // dtData
                        , "StdParam", "DBFieldName" // arrstrCouple
                        , "StdParam", "TCFType" // arrstrCouple
                        );
                    ////
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "InvoiceTGroupCode", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "NetworkID", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "FlagActive", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "LogLUDTimeUTC", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "LogLUBy", typeof(object));
                    ////
                    for (int nScan = 0; nScan < dtInput_Invoice_TempGroupField.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_Invoice_TempGroupField.Rows[nScan];

                        string strDBFieldName = TUtils.CUtils.StdParam(drScan["DBFieldName"]);
                        string strTCFType = string.Format("{0}", drScan["TCFType"]).Trim();

                        ////
                        if (strDBFieldName == null || strDBFieldName.Length < 1)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.strDBFieldName", strDBFieldName
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_TempGroup_Update_TempGroupFieldTbl_InvalidDBFieldName
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        ////
                        if (strTCFType == null || strTCFType.Length < 1)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.strTCFType", strTCFType
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        ////
                        drScan["InvoiceTGroupCode"] = strInvoiceTGroupCode;
                        drScan["DBFieldName"] = strDBFieldName;
                        drScan["NetworkID"] = nNetworkID;
                        drScan["TCFType"] = strTCFType;
                        drScan["FlagActive"] = TConst.Flag.Active;
                        drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        drScan["LogLUBy"] = strWAUserCode;
                    }
                }
                #endregion

                #region //// SaveTemp Invoice_TempCustomField:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#input_Invoice_TempGroupField"
                        , new object[]{
                            "InvoiceTGroupCode", TConst.BizMix.Default_DBColType,
                            "DBFieldName", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "TCFType", TConst.BizMix.Default_DBColType,
                            "FlagActive", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , dtInput_Invoice_TempGroupField
                        );
                }
                #endregion

                #region // SaveDB Invoice_TempGroup:
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_Invoice_TempGroupField:
                            select 
                                t.InvoiceTGroupCode
                                , t.DBFieldName
                            into #tbl_Invoice_TempGroupField
                            from Invoice_TempGroupField t --//[mylock]
                            where (1=1)
                                and t.InvoiceTGroupCode = '@strInvoiceTGroupCode'
                            ;

                            --- Delete:
                            ---- Invoice_TempGroupField:
                            delete t 
                            from Invoice_TempGroupField t --//[mylock]
	                            inner join #tbl_Invoice_TempGroupField f --//[mylock]
		                            on t.InvoiceTGroupCode = f.InvoiceTGroupCode
		                                and t.DBFieldName = f.DBFieldName
                            where (1=1)
                            ;
                            
                            ---- Invoice_TempGroup
                            delete t
                            from Invoice_TempGroup t --//[mylock]
                                inner join #Input_Invoice_TempGroup f --//[mylock]
                                    on t.InvoiceTGroupCode = f.InvoiceTGroupCode
                            where (1=1)
                            ;
                            --- Clear For Debug:
                            drop table #tbl_Invoice_TempGroupField;
						"
                        , "@strInvoiceTGroupCode", strInvoiceTGroupCode
                        );
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }
                /////
                {
                    ////
                    string zzzzClauseInsert_Invoice_TempGroup_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_TempGroup:
                                insert into Invoice_TempGroup(
                                    InvoiceTGroupCode
                                    , NetworkID
                                    , MST
                                    , VATType
                                    , InvoiceTGroupName
                                    , InvoiceTGroupBody
                                    , FilePathThumbnail
                                    , Spec_Prd_Type
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.InvoiceTGroupCode
                                    , t.NetworkID
                                    , t.MST
                                    , t.VATType
                                    , t.InvoiceTGroupName
                                    , t.InvoiceTGroupBody
                                    , t.FilePathThumbnail
                                    , t.Spec_Prd_Type
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #Input_Invoice_TempGroup t
                            ");
                    /////
                    string zzzzClauseInsert_Invoice_TempGroupField_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_TempGroupField:  
                                insert into Invoice_TempGroupField(
	                                InvoiceTGroupCode
	                                , DBFieldName
	                                , NetworkID
	                                , TCFType
	                                , FlagActive
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.InvoiceTGroupCode
	                                , t.DBFieldName
	                                , t.NetworkID
	                                , t.TCFType
	                                , t.FlagActive
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Invoice_TempGroupField t
                            ");

                    string strSqlExec = CmUtils.StringUtils.Replace(@"
                                ----
                                zzzzClauseInsert_Invoice_TempGroup_zSave
								----
								zzzzClauseInsert_Invoice_TempGroupField_zSave
								----
							"
                        , "zzzzClauseInsert_Invoice_TempGroup_zSave", zzzzClauseInsert_Invoice_TempGroup_zSave
                        , "zzzzClauseInsert_Invoice_TempGroupField_zSave", zzzzClauseInsert_Invoice_TempGroupField_zSave
                        );
                    ////
                    DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                    ////

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

        public DataSet WAS_Invoice_TempGroup_Create_New20190924(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_TempGroup objRQ_Invoice_TempGroup
            ////
            , out RT_Invoice_TempGroup objRT_Invoice_TempGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_TempGroup.Tid;
            objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Invoice_TempGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_TempGroup_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup.Invoice_TempGroup)
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
                    DataTable dt_Invoice_TempCustomField = TUtils.DataTableCmUtils.ToDataTable<Invoice_TempGroupField>(objRQ_Invoice_TempGroup.Lst_Invoice_TempGroupField, "Invoice_TempGroupField");
                    dsData.Tables.Add(dt_Invoice_TempCustomField);
                }
                #endregion

                #region // Invoice_TempGroup_Create:
                mdsResult = Invoice_TempGroup_Create_New20190924(
                    objRQ_Invoice_TempGroup.Tid // strTid
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    , objRQ_Invoice_TempGroup.WAUserCode // strUserCode
                    , objRQ_Invoice_TempGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupCode // objInvoiceTGroupCode
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.MST // objMST
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.VATType // objVATType
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupName // objInvoiceTGroupName
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupBody // objInvoiceTGroupBody
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.FilePathThumbnail // objFilePathThumbnail
                    , objRQ_Invoice_TempGroup.Invoice_TempGroup.Spec_Prd_Type // objSpec_Prd_Type
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

        public DataSet Invoice_TempGroup_Create_New20190924(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objInvoiceTGroupCode
            , object objMST
            , object objVATType
            , object objInvoiceTGroupName
            , object objInvoiceTGroupBody
            , object objFilePathThumbnail
            , object objSpec_Prd_Type
            //, object objFlagActive
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_TempGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_TempGroup_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objInvoiceTGroupCode", objInvoiceTGroupCode
                //, "objMST", objMST
                , "objVATType", objVATType
                , "objInvoiceTGroupName", objInvoiceTGroupName
                , "objInvoiceTGroupBody", objInvoiceTGroupBody
                , "objFilePathThumbnail", objFilePathThumbnail
                , "objSpec_Prd_Type", objSpec_Prd_Type
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

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

                #region // Refine and Check Input:
                ////
                string strInvoiceTGroupCode = TUtils.CUtils.StdParam(objInvoiceTGroupCode);
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strVATType = TUtils.CUtils.StdParam(objVATType);
                string strInvoiceTGroupName = string.Format("{0}", objInvoiceTGroupName).Trim();
                string strInvoiceTGroupBody = string.Format("{0}", objInvoiceTGroupBody).Trim();
                string strFilePathThumbnail = string.Format("{0}", objFilePathThumbnail).Trim();
                string strSpec_Prd_Type = TUtils.CUtils.StdParam(objSpec_Prd_Type);

                // drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                ////
                DataTable dtDB_Invoice_TempGroup = null;
                {
                    ////
                    if (strInvoiceTGroupCode == null || strInvoiceTGroupCode.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strInvoiceTGroupCode", strInvoiceTGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Create_InvalidInvoiceTGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Invoice_TempGroup_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strInvoiceTGroupCode // strInvoiceTGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Invoice_TempGroup // dtDB_Invoice_TempGroup
                        );
                    ////
                    DataTable dtDB_Mst_NNT = null;

                    Mst_NNT_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strMST // objMST
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , "" // strTCTStatusListToCheck
                        , out dtDB_Mst_NNT // dtDB_Mst_NNT
                        );
                    ////
                    if (strVATType.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strVATType", strVATType
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Create_InvalidVATType
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strInvoiceTGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strInvoiceTGroupName", strInvoiceTGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Create_InvalidInvoiceTGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (CmUtils.StringUtils.StringEqualIgnoreCase(strSpec_Prd_Type, TConst.Spec_Prd_Type.Spec)
                        && CmUtils.StringUtils.StringEqualIgnoreCase(strSpec_Prd_Type, TConst.Spec_Prd_Type.ProductId))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strSpec_Prd_Type", strInvoiceTGroupName
                            , "Check.Expected", string.Format("{0}, {1}", TConst.Spec_Prd_Type.Spec, TConst.Spec_Prd_Type.ProductId).Trim()
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Create_InvalidSpec_Prd_Type
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveTemp Invoice_TempGroup:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#Input_Invoice_TempGroup"
                        , new object[]{
                            "InvoiceTGroupCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "MST", TConst.BizMix.Default_DBColType,
                            "VATType", TConst.BizMix.Default_DBColType,
                            "InvoiceTGroupName", TConst.BizMix.Default_DBColType,
                            "InvoiceTGroupBody", TConst.BizMix.Max_DBCol,
                            "FilePathThumbnail", TConst.BizMix.Default_DBColType,
                            "Spec_Prd_Type", TConst.BizMix.Default_DBColType,
                            "FlagActive", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , new object[] {
                                new object[]{
                                    strInvoiceTGroupCode, // InvoiceTGroupCode
                                    nNetworkID, // NetworkID
                                    strMST, // MST
                                    strVATType, // VATType
                                    strInvoiceTGroupName, // InvoiceTGroupName
                                    strInvoiceTGroupBody, // InvoiceTGroupBody
                                    strFilePathThumbnail, // FilePathThumbnail
                                    strSpec_Prd_Type, // Spec_Prd_Type
                                    TConst.Flag.Active, // FlagActive
                                    dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                    strWAUserCode, // LogLUBy
                                    }
                            }
                        );
                }
                #endregion

                #region // Refine and Check Invoice_TempGroupField:
                DataTable dtInput_Invoice_TempGroupField = null;
                {
                    ////
                    string strTableCheck = "Invoice_TempGroupField";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Invoice_TempGroupField = dsData.Tables[strTableCheck];
                    ////
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Invoice_TempGroupField // dtData
                        , "StdParam", "DBFieldName" // arrstrCouple
                        , "StdParam", "TCFType" // arrstrCouple
                        );
                    ////
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "InvoiceTGroupCode", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "NetworkID", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "FlagActive", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "LogLUDTimeUTC", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dtInput_Invoice_TempGroupField, "LogLUBy", typeof(object));
                    ////
                    for (int nScan = 0; nScan < dtInput_Invoice_TempGroupField.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_Invoice_TempGroupField.Rows[nScan];

                        string strDBFieldName = TUtils.CUtils.StdParam(drScan["DBFieldName"]);
                        string strTCFType = string.Format("{0}", drScan["TCFType"]).Trim();

                        ////
                        if (strDBFieldName == null || strDBFieldName.Length < 1)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.strDBFieldName", strDBFieldName
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        ////
                        if (strTCFType == null || strTCFType.Length < 1)
                        {
                            alParamsCoupleError.AddRange(new object[]{
                                "Check.strTCFType", strTCFType
                                });
                            throw CmUtils.CMyException.Raise(
                                TError.ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType
                                , null
                                , alParamsCoupleError.ToArray()
                                );
                        }
                        ////
                        drScan["InvoiceTGroupCode"] = strInvoiceTGroupCode;
                        drScan["DBFieldName"] = strDBFieldName;
                        drScan["NetworkID"] = nNetworkID;
                        drScan["TCFType"] = strTCFType;
                        drScan["FlagActive"] = TConst.Flag.Active;
                        drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        drScan["LogLUBy"] = strWAUserCode;


                    }
                }
                #endregion

                #region //// SaveTemp Invoice_TempCustomField:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#input_Invoice_TempGroupField"
                        , new object[]{
                            "InvoiceTGroupCode", TConst.BizMix.Default_DBColType,
                            "DBFieldName", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "TCFType", TConst.BizMix.Default_DBColType,
                            "FlagActive", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , dtInput_Invoice_TempGroupField
                        );
                }
                #endregion

                #region // SaveDB Invoice_TempGroup:
                {
                    ////
                    string zzzzClauseInsert_Invoice_TempGroup_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_TempGroup:
                                insert into Invoice_TempGroup(
                                    InvoiceTGroupCode
                                    , NetworkID
                                    , MST
                                    , VATType
                                    , InvoiceTGroupName
                                    , InvoiceTGroupBody
                                    , FilePathThumbnail
                                    , Spec_Prd_Type
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.InvoiceTGroupCode
                                    , t.NetworkID
                                    , t.MST
                                    , t.VATType
                                    , t.InvoiceTGroupName
                                    , t.InvoiceTGroupBody
                                    , t.FilePathThumbnail
                                    , t.Spec_Prd_Type
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #Input_Invoice_TempGroup t
                            ");
                    /////
                    string zzzzClauseInsert_Invoice_TempGroupField_zSave = CmUtils.StringUtils.Replace(@"
                                ---- Invoice_TempGroupField:  
                                insert into Invoice_TempGroupField(
	                                InvoiceTGroupCode
	                                , DBFieldName
	                                , NetworkID
	                                , TCFType
	                                , FlagActive
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select 
	                                t.InvoiceTGroupCode
	                                , t.DBFieldName
	                                , t.NetworkID
	                                , t.TCFType
	                                , t.FlagActive
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_Invoice_TempGroupField t
                            ");

                    string strSqlExec = CmUtils.StringUtils.Replace(@"
                                ----
                                zzzzClauseInsert_Invoice_TempGroup_zSave
								----
								zzzzClauseInsert_Invoice_TempGroupField_zSave
			                    ----
							"
                        , "zzzzClauseInsert_Invoice_TempGroup_zSave", zzzzClauseInsert_Invoice_TempGroup_zSave
                        , "zzzzClauseInsert_Invoice_TempGroupField_zSave", zzzzClauseInsert_Invoice_TempGroupField_zSave
                        );
                    ////
                    DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                    ////

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
        #endregion

        #region // HistSearch:
        public DataSet Rpt_SearchHis_Add(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objSearchCode
            , object objUserCode
            , object objSearchType
            , object objSkycicVisitID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objSearchCode
                , "objUserName", objUserCode
                , "objSearchType", objSearchType
                , "objPhoneNo", objSkycicVisitID
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

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

                #region // Refine and Check Input:
                ////
                string strSearchCode = TUtils.CUtils.StdParam(objSearchCode);
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strSearchType = TUtils.CUtils.StdParam(objSearchType);
                string strSkycicVisitID = string.Format("{0}", objSkycicVisitID).Trim();
                #endregion

                #region // SaveDB Sys_User:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataTable dtDB_Rpt_SearchHis = TDALUtils.DBUtils.GetSchema(_cf.db, "Rpt_SearchHis").Tables[0];
                    DataRow drDB = dtDB_Rpt_SearchHis.NewRow();
                    strFN = "SearchCode"; drDB[strFN] = strSearchCode;
                    strFN = "NetworkID"; drDB[strFN]= nNetworkID.ToString();
                    strFN = "UserCode"; drDB[strFN] = strUserCode;
                    strFN = "SearchType"; drDB[strFN] = strSearchType;
                    strFN = "SkycicVisitID"; drDB[strFN] = strSkycicVisitID;
                    strFN = "CreateDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "CreateBy"; drDB[strFN] = strWAUserCode;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Rpt_SearchHis.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Rpt_SearchHis"
                        , dtDB_Rpt_SearchHis
                        //, alColumnEffective.ToArray()
                        );
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

        public DataSet WAS_Rpt_SearchHis_Add(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_SearchHis objRQ_Rpt_SearchHis
            ////
            , out RT_Rpt_SearchHis objRT_Rpt_SearchHis
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_SearchHis.Tid;
            objRT_Rpt_SearchHis = new RT_Rpt_SearchHis();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_SearchHis.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Rpt_SearchHis_Add";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_SearchHis_Add;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Rpt_SearchHis", TJson.JsonConvert.SerializeObject(objRQ_Rpt_SearchHis.Rpt_SearchHis)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Rpt_SearchHis> lst_Rpt_SearchHis = new List<Rpt_SearchHis>();
                //List<Rpt_SearchHisInGroup> lst_Rpt_SearchHisInGroup = new List<Rpt_SearchHisInGroup>();
                #endregion

                #region // WAS_Rpt_SearchHis_Add:
                mdsResult = Rpt_SearchHis_Add(
                    objRQ_Rpt_SearchHis.Tid // strTid
                    , objRQ_Rpt_SearchHis.GwUserCode // strGwUserCode
                    , objRQ_Rpt_SearchHis.GwPassword // strGwPassword
                    , objRQ_Rpt_SearchHis.WAUserCode // strUserCode
                    , objRQ_Rpt_SearchHis.WAUserPassword // strUserPassword
                    , objRQ_Rpt_SearchHis.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Rpt_SearchHis.Rpt_SearchHis.SearchCode // objSearchCode
                    , objRQ_Rpt_SearchHis.Rpt_SearchHis.UserCode // objUserCode
                    , objRQ_Rpt_SearchHis.Rpt_SearchHis.SearchType // objSearchType
                    , objRQ_Rpt_SearchHis.Rpt_SearchHis.SkycicVisitID // objSkycicVisitID
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
        
        public DataSet Rpt_SearchHis_Get(
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
            , string strRt_Cols_Rpt_SearchHis
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Rpt_SearchHis_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Rpt_SearchHis_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Rpt_SearchHis", strRt_Cols_Rpt_SearchHis
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

                #region // Check:
                //// Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Rpt_SearchHis = (strRt_Cols_Rpt_SearchHis != null && strRt_Cols_Rpt_SearchHis.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

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
						---- #tbl_Rpt_SearchHis_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, Convert(nvarchar, mc.AutoId) AutoId
						into #tbl_Rpt_SearchHis_Filter_Draft
						from Rpt_SearchHis mc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by Convert(nvarchar, mc.AutoId) asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Rpt_SearchHis_Filter_Draft t --//[mylock]
						;

						---- #tbl_Rpt_SearchHis_Filter:
						select
							t.*
						into #tbl_Rpt_SearchHis_Filter
						from #tbl_Rpt_SearchHis_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Rpt_SearchHis ------:
						zzB_Select_Rpt_SearchHis_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Rpt_SearchHis_Filter_Draft;
						--drop table #tbl_Rpt_SearchHis_Filter;
					"
                    );
                ////
                string zzB_Select_Rpt_SearchHis_zzE = "-- Nothing.";
                if (bGet_Rpt_SearchHis)
                {
                    #region // bGet_Rpt_SearchHis:
                    zzB_Select_Rpt_SearchHis_zzE = CmUtils.StringUtils.Replace(@"
							---- Rpt_SearchHis:
							select
								t.MyIdxSeq
								, mc.*
							from #tbl_Rpt_SearchHis_Filter t --//[mylock]
								inner join Rpt_SearchHis mc --//[mylock]
									on t.AutoId = mc.AutoId
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
                            , "Rpt_SearchHis" // strTableNameDB
                            , "Rpt_SearchHis." // strPrefixStd
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
                    , "zzB_Select_Rpt_SearchHis_zzE", zzB_Select_Rpt_SearchHis_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Rpt_SearchHis)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Rpt_SearchHis";
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

        public DataSet WAS_Rpt_SearchHis_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Rpt_SearchHis objRQ_Rpt_SearchHis
            ////
            , out RT_Rpt_SearchHis objRT_Rpt_SearchHis
            )
        {
            #region // Temp:
            string strTid = objRQ_Rpt_SearchHis.Tid;
            objRT_Rpt_SearchHis = new RT_Rpt_SearchHis();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_SearchHis.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Rpt_SearchHis_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Rpt_SearchHis_Get;
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

                List<Rpt_SearchHis> lst_Rpt_SearchHis = new List<Rpt_SearchHis>();
                bool bGet_Rpt_SearchHis = (objRQ_Rpt_SearchHis.Rt_Cols_Rpt_SearchHis != null && objRQ_Rpt_SearchHis.Rt_Cols_Rpt_SearchHis.Length > 0);
                #endregion

                #region // WS_Rpt_SearchHis_Get:
                mdsResult = Rpt_SearchHis_Get(
                    objRQ_Rpt_SearchHis.Tid // strTid
                    , objRQ_Rpt_SearchHis.GwUserCode // strGwUserCode
                    , objRQ_Rpt_SearchHis.GwPassword // strGwPassword
                    , objRQ_Rpt_SearchHis.WAUserCode // strUserCode
                    , objRQ_Rpt_SearchHis.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Rpt_SearchHis.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Rpt_SearchHis.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Rpt_SearchHis.Ft_WhereClause // strFt_WhereClause
                                                         //// Return:
                    , objRQ_Rpt_SearchHis.Rt_Cols_Rpt_SearchHis // strRt_Cols_Rpt_SearchHis
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Rpt_SearchHis.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Rpt_SearchHis)
                    {
                        ////
                        DataTable dt_Rpt_SearchHis = mdsResult.Tables["Rpt_SearchHis"].Copy();
                        lst_Rpt_SearchHis = TUtils.DataTableCmUtils.ToListof<Rpt_SearchHis>(dt_Rpt_SearchHis);
                        objRT_Rpt_SearchHis.Lst_Rpt_SearchHis = lst_Rpt_SearchHis;
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
    }
}
