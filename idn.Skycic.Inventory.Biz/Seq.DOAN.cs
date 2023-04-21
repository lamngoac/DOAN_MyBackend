using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Collections;
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
// //
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.BizService.Services;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        private long DASeq_Common_Raw(string strTableName)
        {
            // Init:
            string strSql = string.Format(@"
					insert into {0} values (null);
					delete {0} where AutoID = @@Identity;
					select @@Identity Tid;
				", strTableName
            );
            DataSet ds = _cf.db.ExecQuery(strSql);

            // Return Good:
            return Convert.ToInt64(ds.Tables[0].Rows[0][0]);
        }

        private string DASeq_Common_MyGet(
            ref ArrayList alParamsCoupleError
            , string strSequenceType
            , string strParam_Prefix
            , string strParam_Postfix
            )
        {
            #region // Get and Check Map:
            Hashtable htMap = new Hashtable();
            htMap.Add(TConst.SeqTypeDA.Id, new string[] { "Seq_Id", "{0}{1}{2}", "999000000000" });
            htMap.Add(TConst.SeqTypeDA.CustomerCode, new string[] { "Seq_Mst_Customer", "{0}CTMC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.TourCode, new string[] { "Seq_Mst_Tour", "{0}TOUR.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.TourDetailCode, new string[] { "Seq_Mst_TourDetail", "{0}TDTC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.TourGuideNo, new string[] { "Seq_Mst_TourGuide", "{0}TGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.NewsNo, new string[] { "Seq_POW_NewsNews", "{0}NEWNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.RecNo, new string[] { "Seq_POW_Recruitment", "{0}RECNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.ContactNo, new string[] { "Seq_POW_Contact", "{0}CTNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.ContactEmailNo, new string[] { "Seq_POW_ContactEmail", "{0}CTENO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.FAQNo, new string[] { "Seq_POW_FAQ", "{0}FAQNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.AUNo, new string[] { "Seq_POW_AboutUs", "{0}AUNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqTypeDA.ArticleNo, new string[] { "Seq_Mst_Article", "{0}ATC.{3}.{1:00000}{2}", "100000" });
            ////
            //htMap.Add(TConst.MstSvSeqType.InvoiceCode, new string[] { "MstSv_Seq_InvoiceCode ", "{0}INVOICECODE.{3}.{1:00000}{2}", "100000" });

            //

            if (!htMap.ContainsKey(strSequenceType))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strSequenceType", strSequenceType
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Seq_Common_MyGet_InvalidSequenceType
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            string[] arrstrMap = (string[])htMap[strSequenceType];
            string strTableName = arrstrMap[0];
            string strFormat = arrstrMap[1];
            long nMaxSeq = Convert.ToInt64(arrstrMap[2]);

            #endregion

            #region // SequenceGet:
            string strResult = "";
            long nSeq = Seq_Common_Raw(strTableName);
            string strMyEncrypt = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            strResult = string.Format(
                strFormat // Format
                , strParam_Prefix // {0}
                , nSeq % nMaxSeq // {1}
                , strParam_Postfix // {2}
                , string.Format("{0}{1}{2}", strMyEncrypt[DateTime.UtcNow.Year - TConst.BizMix.Default_RootYear], strMyEncrypt[DateTime.UtcNow.Month], strMyEncrypt[DateTime.UtcNow.Day]) // {3}
                );
            #endregion

            // Return Good:
            return strResult;
        }

        public DataSet DASeq_Common_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            // //
            , string strSequenceType
            , string strParam_Prefix
            , string strParam_Postfix
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_Common_Get";
            string strErrorCodeDefault = TError.ErrDA.Seq_Common_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strSequenceType", strSequenceType
                , "strParam_Prefix", strParam_Prefix
                , "strParam_Postfix", strParam_Postfix
                });
            TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                dbLocal.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                strSequenceType = TUtils.CUtils.StdParam(strSequenceType);
                strParam_Prefix = TUtils.CUtils.StdParam(strParam_Prefix);
                strParam_Postfix = TUtils.CUtils.StdParam(strParam_Postfix);
                #endregion

                #region // SequenceGet:
                ////
                string strResult = DASeq_Common_MyGet(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strSequenceType // strSequenceType
                    , strParam_Prefix // strParam_Prefix
                    , strParam_Postfix // strParam_Postfix
                    );
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(dbLocal); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(dbLocal);

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
                TDALUtils.DBUtils.RollbackSafety(dbLocal);
                TDALUtils.DBUtils.ReleaseAllSemaphore(dbLocal, true);

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

        public DataSet WAS_DASeq_Common_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Seq_Common objRQ_Seq_Common
            ////
            , out DA_RT_Seq_Common objRT_Seq_Common
            )
        {
            #region // Temp:
            string strTid = objRQ_Seq_Common.Tid;
            objRT_Seq_Common = new DA_RT_Seq_Common();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Seq_Common_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Seq_Common_Get;
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

                #region // WS_DASeq_Common_Get:
                mdsResult = DASeq_Common_Get(
                    objRQ_Seq_Common.Tid // strTid
                    , objRQ_Seq_Common.GwUserCode // strGwUserCode
                    , objRQ_Seq_Common.GwPassword // strGwPassword
                    , objRQ_Seq_Common.WAUserCode // strUserCode
                    , objRQ_Seq_Common.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              // //
                    , objRQ_Seq_Common.Seq_Common.SequenceType.ToString() // strSequenceType
                    , objRQ_Seq_Common.Seq_Common.Param_Prefix.ToString() // Param_Prefix
                    , objRQ_Seq_Common.Seq_Common.Param_Postfix.ToString() // Param_Postfix
                    );
                #endregion

                #region // GetData:
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
    }
}
