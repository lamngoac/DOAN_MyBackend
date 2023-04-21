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
using idn.Skycic.Inventory.BizService.Services;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Invoice_InvoiceInput:
        public void OS_TVAN_Invoice_InvoiceInput_CallIssuesd(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strMST
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "OS_TVAN_Invoice_InvoiceInput_CallIssuesd";
            string strErrorCodeDefault = "OS_TVAN_Invoice_InvoiceInput_CallIssuesd"; // TError.ErridnInventory.OS_TVAN_Invoice_InvoiceInput_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                , "strMST", strMST
                ////
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // OS_MstSvTVAN_MstSv_Mst_Network_GetByMST:
            string strUrl_OS_TVAN_AllNetWork = "";
            if(!string.IsNullOrEmpty(strMST))
            {
                OS_MstSvTVAN_MstSv_Mst_Network_GetByMST(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                                    ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , out strUrl_OS_TVAN_AllNetWork // strUrl_OS_TVAN_AllNetWork
                    );
            }
            #endregion

            #region // OS_TVAN_Invoice_InvoiceInput_Save:
            if(!string.IsNullOrEmpty(strUrl_OS_TVAN_AllNetWork.Trim()))
            {
                OS_TVAN_Invoice_InvoiceInput_Save(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                                   ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strUrl_OS_TVAN_AllNetWork // strUrl_OS_TVAN_AllNetWork
                    , dsData // dsData
                    );
            }
            #endregion
        }
        public void OS_TVAN_Invoice_InvoiceInput_CallDeleted(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strMST
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "OS_TVAN_Invoice_InvoiceInput_CallIssuesd";
            string strErrorCodeDefault = "OS_TVAN_Invoice_InvoiceInput_CallIssuesd"; // TError.ErridnInventory.OS_TVAN_Invoice_InvoiceInput_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // OS_MstSvTVAN_MstSv_Mst_Network_GetByMST:
            string strUrl_OS_TVAN_AllNetWork = "";
            if (!string.IsNullOrEmpty(strMST))
            {
                OS_MstSvTVAN_MstSv_Mst_Network_GetByMST(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                                    ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , out strUrl_OS_TVAN_AllNetWork // strUrl_OS_TVAN_AllNetWork
                    );
            }
            #endregion

            #region // OS_TVAN_Invoice_InvoiceInput_Save:
            if (!string.IsNullOrEmpty(strUrl_OS_TVAN_AllNetWork.Trim()))
            {
                OS_TVAN_Invoice_InvoiceInput_Delete(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                                   ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strUrl_OS_TVAN_AllNetWork // strUrl_OS_TVAN_AllNetWork
                    , dsData // dsData
                    );
            }
            #endregion
        }
        public void OS_TVAN_Invoice_InvoiceInput_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strUrl_OS_TVAN_AllNetWork
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "OS_TVAN_Invoice_InvoiceInput_Save";
            string strErrorCodeDefault = TError.ErridnInventory.OS_TVAN_Invoice_InvoiceInput_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strUrl_OS_TVAN_AllNetWork", strUrl_OS_TVAN_AllNetWork
                });
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
            List<Invoice_InvoiceInput> Lst_Invoice_InvoiceInput = new List<Invoice_InvoiceInput>();
            List<Invoice_InvoiceInputDtl> Lst_Invoice_InvoiceInputDtl = new List<Invoice_InvoiceInputDtl>();
            ////
            {
                ////
                DataTable dtInput_Invoice_InvoiceInput = dsData.Tables["Invoice_InvoiceInput"];
                Lst_Invoice_InvoiceInput = TUtils.DataTableCmUtils.ToListof<Invoice_InvoiceInput>(dtInput_Invoice_InvoiceInput);
                ////
                DataTable dtInput_Invoice_InvoiceInputDtl = dsData.Tables["Invoice_InvoiceInputDtl"];
                Lst_Invoice_InvoiceInputDtl = TUtils.DataTableCmUtils.ToListof<Invoice_InvoiceInputDtl>(dtInput_Invoice_InvoiceInputDtl);
                ////

            }
            #endregion

            #region // Call Func:
            RT_Invoice_InvoiceInput objRT_Invoice_InvoiceInput = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                /////
                RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput = new RQ_Invoice_InvoiceInput()
                {
                    Lst_Invoice_InvoiceInput = Lst_Invoice_InvoiceInput,
                    Lst_Invoice_InvoiceInputDtl = Lst_Invoice_InvoiceInputDtl,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = strNetworkID,
                    GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                    GwPassword = strOS_MasterServer_Solution_GwPassword,
                    WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                    WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
                };
                ////
                try
                {
                    //strUrl_OS_TVAN_AllNetWork = "http://localhost:1800/";
                    objRT_Invoice_InvoiceInput = OS_TVAN_Invoice_InvoiceInputService.Instance.WA_OS_TVAN_Invoice_InvoiceInput_Save(strUrl_OS_TVAN_AllNetWork, objRQ_Invoice_InvoiceInput);
                    ////
                }
                catch (Exception cex)
                {
                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion
        }

        public void OS_TVAN_Invoice_InvoiceInput_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strUrl_OS_TVAN_AllNetWork
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            string strFunctionName = "OS_TVAN_Invoice_InvoiceInput_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_TVAN_Invoice_InvoiceInput_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strUrl_OS_TVAN_AllNetWork", strUrl_OS_TVAN_AllNetWork
                });
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
            List<Invoice_InvoiceInput> Lst_Invoice_InvoiceInput = new List<Invoice_InvoiceInput>();
            List<Invoice_InvoiceInputDtl> Lst_Invoice_InvoiceInputDtl = new List<Invoice_InvoiceInputDtl>();
            ////
            {
                ////
                DataTable dtInput_Invoice_InvoiceInput = dsData.Tables["Invoice_InvoiceInput"];
                Lst_Invoice_InvoiceInput = TUtils.DataTableCmUtils.ToListof<Invoice_InvoiceInput>(dtInput_Invoice_InvoiceInput);
                ////

            }
            #endregion

            #region // Call Func:
            RT_Invoice_InvoiceInput objRT_Invoice_InvoiceInput = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                /////
                string strFt_Cols_Upd = "Invoice_InvoiceInput.InvoicePDFFilePath, Invoice_InvoiceInput.DeleteDTimeUTC, Invoice_InvoiceInput.DeleteBy, Invoice_InvoiceInput.InvoiceStatus";
                RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput = new RQ_Invoice_InvoiceInput()
                {
                    Lst_Invoice_InvoiceInput = Lst_Invoice_InvoiceInput,
                    //Lst_Invoice_InvoiceInputDtl = Lst_Invoice_InvoiceInputDtl,
                    Ft_Cols_Upd = strFt_Cols_Upd,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = strNetworkID,
                    GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                    GwPassword = strOS_MasterServer_Solution_GwPassword,
                    WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                    WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
                };
                ////
                try
                {
                    objRT_Invoice_InvoiceInput = OS_TVAN_Invoice_InvoiceInputService.Instance.WA_OS_TVAN_Invoice_InvoiceInput_Delete(strUrl_OS_TVAN_AllNetWork, objRQ_Invoice_InvoiceInput);
                    ////
                }
                catch (Exception cex)
                {
                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion
        }

        #endregion
    }
}
