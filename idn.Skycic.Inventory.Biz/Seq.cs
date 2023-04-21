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
		///*
		private long Seq_Common_Raw(string strTableName)
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
		private string Seq_Common_MyGet(
			ref ArrayList alParamsCoupleError
			, string strSequenceType
			, string strParam_Prefix
			, string strParam_Postfix
			)
		{
			#region // Get and Check Map:
			Hashtable htMap = new Hashtable();
			htMap.Add(TConst.SeqType.Id, new string[] { "Seq_Id", "{0}{1}{2}", "999000000000" });
			//htMap.Add(TConst.SeqType.DiscountDBCode, new string[] { "Seq_MasterDataId", "{0}DCDB.{3}.{1:0000}{2}", "10000" });
			htMap.Add(TConst.SeqType.InsuranceClaimDocCode, new string[] { "Seq_TransactionDataId", "{0}ICDC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.InsuranceClaimNo, new string[] { "Seq_TransactionDataId", "{0}ICN.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.WorkingRecordNo, new string[] { "Seq_TransactionDataId", "{0}WRN.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.LevelCode, new string[] { "Seq_TransactionDataId", "{0}LVC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.CampaignCrAwardCode, new string[] { "Seq_TransactionDataId", "{0}CCRAC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.CampaignCode, new string[] { "Seq_TransactionDataId", "{0}CCRAC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.CICode, new string[] { "Seq_TransactionDataId", "{0}CIC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.InputCode, new string[] { "Seq_InputCode", "{0}IPC.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.PrdStateNo, new string[] { "Seq_PrdStateNo", "{0}PRDNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.ColReleaseNo, new string[] { "Seq_ColReleaseNo", "{0}COLRELEASENO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.MDCrtBatchNo, new string[] { "Seq_MDCrtBatchNo", "{0}MDCRTBATCHNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.MDAutoBatchNo, new string[] { "Seq_MDAutoBatchNo", "{0}MDAUTOBATCHNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.CBABatchNo, new string[] { "Seq_CBABatchNo", "{0}CBABATCHNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.TranNo, new string[] { "Seq_TranNo", "{0}TRANNO.{3}.{1:00000}{2}", "100000" });

			htMap.Add(TConst.SeqType.TaxRegNo, new string[] { "Seq_TaxRegNo", "{0}REGNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.TaxRegStopNo, new string[] { "Seq_TaxRegStopNo", "{0}REGSTOPNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.TaxSubNo, new string[] { "Seq_TaxSubNo", "{0}SUBNO.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.TInvoiceCode, new string[] { "Seq_TInvoiceCode ", "{0}TINVOICODE.{3}.{1:00000}{2}", "100000" });
			htMap.Add(TConst.SeqType.BatchNo, new string[] { "Seq_BatchNo ", "{0}BATCHNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.PRTCode, new string[] { "Seq_PrintTempCode ", "{0}PRTCODE.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.SerialNo, new string[] { "Seq_SerialNo  ", "{0}SERIALNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.GenTimesNo, new string[] { "Seq_GenTimesNo", "{0}GENTIMESNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvInFGNo, new string[] { "Seq_IFInvInFGNo", "{0}IFINVINFGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvOutFGNo, new string[] { "Seq_IFInvOutFGNo", "{0}IFINVOUTFGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvInNo, new string[] { "Seq_IF_InvInNo", "{0}PN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvOutNo, new string[] { "Seq_IF_InvOutNo", "{0}PX.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFMONo, new string[] { "Seq_IF_MONo", "{0}DC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvCusReturnNo, new string[] { "Seq_IF_InvCusReturnNo", "{0}KT.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvAudNo, new string[] { "Seq_IF_InvAudNo", "{0}KK.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvReturnSupNo, new string[] { "Seq_IF_InvReturnSupNo", "{0}TN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFTempPrintNo, new string[] { "Seq_IF_TempPrintNo", "{0}IFTEMPPRINTNO.{3}.{1:00000}{2}", "100000" });

            ////
            htMap.Add(TConst.MstSvSeqType.InvoiceCode, new string[] { "MstSv_Seq_InvoiceCode ", "{0}INVOICECODE.{3}.{1:00000}{2}", "100000" });

			//

			if (!htMap.ContainsKey(strSequenceType))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.strSequenceType", strSequenceType
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Seq_Common_MyGet_InvalidSequenceType
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

		public DataSet Seq_Common_Get(
			string strTid
			, DataRow drSession
			////
			, string strSequenceType
			, string strParam_Prefix
			, string strParam_Postfix
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "Seq_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
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
				dbLocal.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) dbLocal.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);
				#endregion

				#region // Refine and Check Input:
				////
				strSequenceType = TUtils.CUtils.StdParam(strSequenceType);
				strParam_Prefix = TUtils.CUtils.StdParam(strParam_Prefix);
				strParam_Postfix = TUtils.CUtils.StdParam(strParam_Postfix);
				////
				#endregion

				#region // SequenceGet:
				////
				string strResult = Seq_Common_MyGet(
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet Seq_Common_Get_NoSess(
			string strGwUserCode
			, string strGwPassword
			, string strTid
			//, DataRow drSession
			////
			, string strSequenceType
			, string strParam_Prefix
			, string strParam_Postfix
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "Seq_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
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
				//dbLocal.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) dbLocal.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strGwUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);
				#endregion

				#region // Refine and Check Input:
				////
				strSequenceType = TUtils.CUtils.StdParam(strSequenceType);
				strParam_Prefix = TUtils.CUtils.StdParam(strParam_Prefix);
				strParam_Postfix = TUtils.CUtils.StdParam(strParam_Postfix);
				////
				#endregion

				#region // SequenceGet:
				////
				string strResult = Seq_Common_MyGet(
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
					, strGwUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet Seq_Common_Get(
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
			string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get;
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
				string strResult = Seq_Common_MyGet(
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


		public DataSet Seq_FormNo_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			// //
			, object objInvoiceType
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_FormNo_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_FormNo_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "objInvoiceType", objInvoiceType
				, "objMST", objMST
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
				string strInvoiceType = TUtils.CUtils.StdParam(objInvoiceType);
				string strMST = TUtils.CUtils.StdParam(objMST);
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				GetFormNo(
					ref alParamsCoupleError // alParamsCoupleError
					, strInvoiceType // strInvoiceType
					, strMST // strMST
					, ref strResult // strResult
					);
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult.Trim());
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

		public DataSet Seq_TCTTranNo_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_TCTTranNo_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_TCTTranNo_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
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
				string strTableName = "Seq_TCTTranNo";
				string strFormat = "{0}{1}{2}";
				string strParam0 = null;
				string strParam1 = null;
				string strParam2 = null;

				////
				{
					// //
					strParam0 = TConst.BizMix.TVAN;
					// //
					strParam1 = dtimeSys.Year.ToString();
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					strParam2 = TUtils.MyStringUtils.To10Mask(nSeq, 10);
				}
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				{
					strResult = string.Format(
						strFormat // Format
						, strParam0 // {0}
						, strParam1 // {1}
						, strParam2 // {2}
						);
				}



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

		public DataSet Seq_InvoiceCode_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, string strOrgID
			, ref ArrayList alParamsCoupleError
		   ////
		   )
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_InvoiceCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_InvoiceCode_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
				, "strOrgID", strOrgID
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
				string strOrgIDSln = null;
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.NetworkID = strNetworkID;
					objMstSv_OrgInNetwork.OrgID = strOrgID;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						OrgID = strOrgID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					};
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
					////
					try
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
							});
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
						strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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


				string strTableName = "Seq_InvoiceCode";
				string strFormat = "{0}{1}{2}{3}{4}";
				string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
				string strOrgID36 = strOrgIDSln;
				string strYear36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Year), 2);
				string strMonth36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Month), 1);
				string strDay36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Day), 1);
				string strRandom36 = null;
				string strChecksum36 = null;

				////
				{
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					long nMaxSeq = 10000000;
					string strRandom = Convert.ToString(nSeq % nMaxSeq);
					Int32 nRandom = Convert.ToInt32(strRandom);

					strRandom36 = TUtils.CMyBase36.To36(nRandom, 5);
				}
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				string strResultFinal = "";
				{
					////
					strResult = string.Format(
						strFormat // Format
						, strOrgID36 // {0}
						, strYear36 // {1}
						, strMonth36 // {2}
						, strDay36 // {4}
						, strRandom36 // {5}
						);

					////
					List<int> lstChar = new List<int>();
					for (int nScan = 0; nScan < strResult.Length; nScan++)
					{
						////
						char objchar = strResult[nScan];

						int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
						lstChar.Add(nchar);

					}

					Int32 nSum = lstChar.AsQueryable().Sum();
					string strMod = Convert.ToString(nSum % 36);
					strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

					strResultFinal = string.Format(
						strFormatFinal // Format
						, strOrgID36 // {0}
						, strYear36 // {1}
						, strMonth36 // {2}
						, strDay36 // {3}
						, strRandom36 // {4}
						, strChecksum36 // {5}
						);
				}



				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
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

        public DataSet Seq_GenEngine_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
           ////
           )
        {
            #region Danh sách khởi tạo ban đầu
            string strOrgID = strNetworkID;
            string strVersion = "0";
            string strSolution = "1";
            #endregion

            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_GenEngine_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenEngine_Get;
            alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID                
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
                //string strOrgIDSln = null;
                //RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
                //{
                //    #region // WA_MstSv_OrgInNetwork_Login:
                //    MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
                //    objMstSv_OrgInNetwork.NetworkID = strNetworkID;
                //    objMstSv_OrgInNetwork.OrgID = strOrgID;

                //    /////
                //    RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
                //    {

                //        Tid = strTid,
                //        TokenID = strOS_MasterServer_Solution_API_Url,
                //        NetworkID = strNetworkID,
                //        OrgID = strOrgID,
                //        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                //        GwPassword = strOS_MasterServer_Solution_GwPassword,
                //        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                //        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
                //    };
                //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
                //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
                //    ////
                //    try
                //    {
                //        alParamsCoupleError.AddRange(new object[]{
                //            "Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
                //            });
                //        objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
                //        strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                //        ////
                //    }
                //    catch (Exception cex)
                //    {
                //        TUtils.CProcessExc.BizShowException(
                //            ref alParamsCoupleError // alParamsCoupleError
                //            , cex // cex
                //            );

                //        throw CmUtils.CMyException.Raise(
                //            TError.ErridnInventory.CmSys_InvalidOutSite
                //            , null
                //            , alParamsCoupleError.ToArray()
                //            );
                //    }
                //    ////
                //    #endregion
                //}


                string strTableName = "Seq_InvoiceCode";
                string strFormat = "{0}{1}{2}{3}";
                string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
                string strOrgID36 = "00002";//strOrgIDSln;                
                string strYear36 = TUtils.CMyBase36.To36((Convert.ToInt32(dtimeSys.Year) - 2010), 1);                
                string strRandom36 = null;
                string strChecksum36 = null;

                ////
                {
                    // //
                    long nSeq = Seq_Common_Raw(strTableName);
                    long nMaxSeq = 10000000;
                    string strRandom = Convert.ToString(nSeq % nMaxSeq);
                    Int32 nRandom = Convert.ToInt32(strRandom);

                    strRandom36 = TUtils.CMyBase36.To36(nRandom, 6);
                }
                #endregion

                #region // SequenceGet:
                ////
                string strResult = "";
                string strResultFinal = "";
                {
                    ////
                    strResult = string.Format(
                        strFormat // Format
                        , strVersion // {0}
                        , strYear36 // {1}
                        , strSolution // {2}
                        , strOrgID36.Substring(0,1) // {3}
                        );

                    ////
                    List<int> lstChar = new List<int>();
                    for (int nScan = 0; nScan < strResult.Length; nScan++)
                    {
                        ////
                        char objchar = strResult[nScan];

                        int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
                        lstChar.Add(nchar);

                    }

                    Int32 nSum = lstChar[0] * 3 + lstChar[1] * 5 + lstChar[2] * 7 + lstChar[3] * 11;
                    string strMod = Convert.ToString(nSum % 36);                    
                    strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

                    strResultFinal = string.Format(
                        strFormatFinal // Format
                        , strVersion
                        , strYear36
                        , strSolution
                        , strOrgID36 
                        , strChecksum36                                               
                        , strRandom36                 
                        );
                }
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
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

        public DataSet Seq_GenObjCode_GetOld(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objObjCodeAmount
            ////
            , out List<string> lstSeq_ObjCode
           )
        {
            #region Danh sách khởi tạo ban đầu
            //string strOrgID = "10000";
            string strVersion = "0";
            string strSolution = "1";
            Int64 nObjAmount = Convert.ToInt64(objObjCodeAmount);
            lstSeq_ObjCode = new List<string>();
            #endregion

            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_GenObjCode_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenObjCode_Get;
            alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
                , "objObjCodeAmount", objObjCodeAmount
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
                //string strOrgIDSln = null;
                //RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
                //{
                //    #region // WA_MstSv_OrgInNetwork_Login:
                //    MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
                //    objMstSv_OrgInNetwork.NetworkID = strNetworkID;
                //    objMstSv_OrgInNetwork.OrgID = strOrgID;

                //    /////
                //    RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
                //    {

                //        Tid = strTid,
                //        TokenID = strOS_MasterServer_Solution_API_Url,
                //        NetworkID = strNetworkID,
                //        OrgID = strOrgID,
                //        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                //        GwPassword = strOS_MasterServer_Solution_GwPassword,
                //        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                //        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
                //    };
                //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
                //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
                //    ////
                //    try
                //    {
                //        alParamsCoupleError.AddRange(new object[]{
                //            "Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
                //            });
                //        objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
                //        strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                //        ////
                //    }
                //    catch (Exception cex)
                //    {
                //        TUtils.CProcessExc.BizShowException(
                //            ref alParamsCoupleError // alParamsCoupleError
                //            , cex // cex
                //            );

                //        throw CmUtils.CMyException.Raise(
                //            TError.ErridnInventory.CmSys_InvalidOutSite
                //            , null
                //            , alParamsCoupleError.ToArray()
                //            );
                //    }
                //    ////
                //    #endregion
                //}


                string strTableName = "Seq_InvoiceCode";
                string strFormat = "{0}{1}{2}{3}";
                string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
                string strOrgID36 = "10002";//strOrgIDSln;                
                string strYear36 = TUtils.CMyBase36.To36((Convert.ToInt32(dtimeSys.Year) - 2010), 1);
                string strRandom36 = null;
                string strChecksum36 = null;              
                #endregion

                #region // SequenceGet:
                ////
                string strResult = "";
                string strResultFinal = "";
                for (int nCount = 0; nCount < nObjAmount; nCount++)
                {
                    ////
                    {
                        // //
                        long nSeq = Seq_Common_Raw(strTableName);
                        long nMaxSeq = 10000000;
                        string strRandom = Convert.ToString(nSeq % nMaxSeq);
                        Int32 nRandom = Convert.ToInt32(strRandom);

                        strRandom36 = TUtils.CMyBase36.To36(nRandom, 6);
                    }
                    ////
                    strResult = string.Format(
                        strFormat // Format
                        , strVersion // {0}
                        , strYear36 // {1}
                        , strSolution // {2}
                        , strOrgID36.Substring(0, 1) // {3}
                        );

                    ////
                    List<int> lstChar = new List<int>();
                    for (int nScan = 0; nScan < strResult.Length; nScan++)
                    {
                        ////
                        char objchar = strResult[nScan];

                        int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
                        lstChar.Add(nchar);
                    }

                    Int32 nSum = lstChar[0] * 3 + lstChar[1] * 5 + lstChar[2] * 7 + lstChar[3] * 11;
                    string strMod = Convert.ToString(nSum % 36);
                    strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

                    strResultFinal = string.Format(
                        strFormatFinal // Format
                        , strVersion
                        , strYear36
                        , strSolution
                        , strOrgID36
                        , strChecksum36
                        , strRandom36
                        );
                    lstSeq_ObjCode.Add(strResultFinal);
                }
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
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

		public DataSet Seq_GenObjCode_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID            
            , ref ArrayList alParamsCoupleError
            ////
            , string objOrgIDSln
            , object objObjCodeAmount
			////
			, out List<string> lstSeq_ObjCode
		   )
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_GenObjCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenObjCode_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
                , "objOrgIDSln", objOrgIDSln
                , "strNetworkID", strNetworkID
				, "objObjCodeAmount", objObjCodeAmount
				});
			lstSeq_ObjCode = new List<string>();

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
				Seq_GenObjCode_GetX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, strNetworkID // strNetworkID                    
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objOrgIDSln // objOrgIDSln
                    , objObjCodeAmount // objObjCodeAmount
					////
					, out lstSeq_ObjCode // lstSeq_ObjCode
					);
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

		private void Seq_GenObjCode_GetX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
            , string objOrgIDSln
            , object objObjCodeAmount
			////
			, out List<string> lstSeq_ObjCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_GenObjCode_Get";
			//string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenObjCode_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
				, "objObjCodeAmount", objObjCodeAmount
				});
			//TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
			#endregion

			#region // Convert Input:
			DateTime dtimeTDate = DateTime.UtcNow;
			#endregion

			#region // Refine and Check Input:
			////
			//string strOrgIDSln = null;
			//RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			//{
			//    #region // WA_MstSv_OrgInNetwork_Login:
			//    MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
			//    objMstSv_OrgInNetwork.NetworkID = strNetworkID;
			//    objMstSv_OrgInNetwork.OrgID = strOrgID;

			//    /////
			//    RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
			//    {

			//        Tid = strTid,
			//        TokenID = strOS_MasterServer_Solution_API_Url,
			//        NetworkID = strNetworkID,
			//        OrgID = strOrgID,
			//        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
			//        GwPassword = strOS_MasterServer_Solution_GwPassword,
			//        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
			//        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
			//    };
			//    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
			//    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
			//    ////
			//    try
			//    {
			//        alParamsCoupleError.AddRange(new object[]{
			//            "Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
			//            });
			//        objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
			//        strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
			//        ////
			//    }
			//    catch (Exception cex)
			//    {
			//        TUtils.CProcessExc.BizShowException(
			//            ref alParamsCoupleError // alParamsCoupleError
			//            , cex // cex
			//            );

			//        throw CmUtils.CMyException.Raise(
			//            TError.ErridnInventory.CmSys_InvalidOutSite
			//            , null
			//            , alParamsCoupleError.ToArray()
			//            );
			//    }
			//    ////
			//    #endregion
			//}
			//string strOrgID = "10000";
			//string strVersion = "0";
			//string strSolution = "1";
			// NC.20200413:
			string strVersion = TConst.GenEngineVersion.V0;
			string strSolution = TConst.GenEngineSolution.INVENTORY;
			Int64 nObjAmount = Convert.ToInt64(objObjCodeAmount);
			lstSeq_ObjCode = new List<string>();

			string strTableName = "Seq_InvoiceCode";
			string strFormat = "{0}{1}{2}{3}";
			string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
			string strOrgID36 = TUtils.CUtils.StdParam(objOrgIDSln);//strOrgIDSln;                
			string strYear36 = TUtils.CMyBase36.To36((Convert.ToInt32(dtimeSys.Year) - 2010), 1);
			string strRandom36 = null;
			string strChecksum36 = null;
			#endregion

			#region // SequenceGet:
			////
			string strResult = "";
			string strResultFinal = "";
			for (int nCount = 0; nCount < nObjAmount; nCount++)
			{
				////
				{
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					long nMaxSeq = 10000000;
					string strRandom = Convert.ToString(nSeq % nMaxSeq);
					Int32 nRandom = Convert.ToInt32(strRandom);

					strRandom36 = TUtils.CMyBase36.To36(nRandom, 6);
				}
				////
				strResult = string.Format(
					strFormat // Format
					, strVersion // {0}
					, strYear36 // {1}
					, strSolution // {2}
					, strOrgID36.Substring(0, 1) // {3}
					);

				////
				List<int> lstChar = new List<int>();
				for (int nScan = 0; nScan < strResult.Length; nScan++)
				{
					////
					char objchar = strResult[nScan];

					int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
					lstChar.Add(nchar);
				}

				Int32 nSum = lstChar[0] * 3 + lstChar[1] * 5 + lstChar[2] * 7 + lstChar[3] * 11;
				string strMod = Convert.ToString(nSum % 36);
				strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

				strResultFinal = string.Format(
					strFormatFinal // Format
					, strVersion
					, strYear36
					, strSolution
					, strOrgID36
					, strChecksum36
					, strRandom36
					);
				lstSeq_ObjCode.Add(strResultFinal);
			}
			//CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
			#endregion
		}

		public DataSet Seq_UUID_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, string strOrgID
			, ref ArrayList alParamsCoupleError
		   ////
		   )
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_InvoiceCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_InvoiceCode_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
				, "strOrgID", strOrgID
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
				string strOrgIDSln = null;
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.NetworkID = strNetworkID;
					objMstSv_OrgInNetwork.OrgID = strOrgID;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						OrgID = strOrgID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					};
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
					////
					try
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
							});
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
						strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
							TError.ErridnInventory.CmSys_InvalidOutSite
							, null
							, alParamsCoupleError.ToArray() + "." + "INVENTORY" + "." + strErrorCodeOS
                            );
					}
					////
					#endregion
				}


				string strTableName = "Seq_UUID";
				string strFormat = "{0}.{1}";
				string strOrgID36 = strOrgIDSln;
				string strRandom36 = null;

				////
				{
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					long nMaxSeq = 10000000;
					string strRandom = Convert.ToString(nSeq % nMaxSeq);
					Int32 nRandom = Convert.ToInt32(strRandom);

					strRandom36 = TUtils.CMyBase36.To36(nRandom, 5);
				}
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				{
					////
					strResult = string.Format(
						strFormat // Format
						, strOrgID36 // {0}
						, strRandom36 // {1}
						);
				}



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

		public DataSet Seq_InvoiceCode_GetByAmount(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, string strOrgID
			, ref ArrayList alParamsCoupleError
			////
			, object objInvoiceAmount
			////
			, out List<string> lstSeq_InvoiceCode
		   )
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_InvoiceCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_InvoiceCode_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
				, "strOrgID", strOrgID
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
				Int64 nInvoiceAmount = Convert.ToInt64(objInvoiceAmount);
				lstSeq_InvoiceCode = new List<string>();

				////
				string strOrgIDSln = null;
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.NetworkID = strNetworkID;
					objMstSv_OrgInNetwork.OrgID = strOrgID;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						OrgID = strOrgID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					};
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
					////
					try
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
							});
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
						strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
				//CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
				#endregion

				#region // Gen Data:
				for (int nCount = 0; nCount < nInvoiceAmount; nCount++)
				{
					string strTableName = "Seq_InvoiceCode";
					string strFormat = "{0}{1}{2}{3}{4}";
					string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
					string strOrgID36 = strOrgIDSln;
					string strYear36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Year), 2);
					string strMonth36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Month), 1);
					string strDay36 = TUtils.CMyBase36.To36(Convert.ToInt32(dtimeSys.Day), 1);
					string strRandom36 = null;
					string strChecksum36 = null;

					////
					{
						// //
						long nSeq = Seq_Common_Raw(strTableName);
						long nMaxSeq = 10000000;
						string strRandom = Convert.ToString(nSeq % nMaxSeq);
						Int32 nRandom = Convert.ToInt32(strRandom);

						strRandom36 = TUtils.CMyBase36.To36(nRandom, 5);
					}

					#region // SequenceGet:
					////
					string strResult = "";
					string strResultFinal = "";
					{
						////
						strResult = string.Format(
							strFormat // Format
							, strOrgID36 // {0}
							, strYear36 // {1}
							, strMonth36 // {2}
							, strDay36 // {4}
							, strRandom36 // {5}
							);

						////
						List<int> lstChar = new List<int>();
						for (int nScan = 0; nScan < strResult.Length; nScan++)
						{
							////
							char objchar = strResult[nScan];

							int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
							lstChar.Add(nchar);

						}

						Int32 nSum = lstChar.AsQueryable().Sum();
						string strMod = Convert.ToString(nSum % 36);
						strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

						strResultFinal = string.Format(
							strFormatFinal // Format
							, strOrgID36 // {0}
							, strYear36 // {1}
							, strMonth36 // {2}
							, strDay36 // {3}
							, strRandom36 // {4}
							, strChecksum36 // {5}
							);
					}
					#endregion

					lstSeq_InvoiceCode.Add(strResultFinal);
				}
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
				lstSeq_InvoiceCode = null;

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

		public DataSet Seq_TVANTranNo_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_TVANTranNo_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_TVANTranNo_Get;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
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
				string strTableName = "Seq_TVANTranNo";
				string strFormat = "{0}{1}{2}";
				string strParam0 = null;
				string strParam1 = null;
				string strParam2 = null;

				////
				{
					// //
					strParam0 = TConst.BizMix.TVAN;
					// //
					strParam1 = dtimeSys.Year.ToString();
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					strParam2 = TUtils.MyStringUtils.To10Mask(nSeq, 10);
				}
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				{
					strResult = string.Format(
						strFormat // Format
						, strParam0 // {0}
						, strParam1 // {1}
						, strParam2 // {2}
						);
				}



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

		public DataSet WAS_Seq_TCTTranNo_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_TCTTranNo objRQ_Seq_TCTTranNo
			////
			, out RT_Seq_TCTTranNo objRT_Seq_TCTTranNo
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_TCTTranNo.Tid;
			objRT_Seq_TCTTranNo = new RT_Seq_TCTTranNo();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_TCTTranNo.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_TCTTranNo_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_TCTTranNo_Get;
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

				#region // Seq_TCTTranNo_Get:
				mdsResult = Seq_TCTTranNo_Get(
					objRQ_Seq_TCTTranNo.Tid // strTid
					, objRQ_Seq_TCTTranNo.GwUserCode // strGwUserCode
					, objRQ_Seq_TCTTranNo.GwPassword // strGwPassword
					, objRQ_Seq_TCTTranNo.WAUserCode // strUserCode
					, objRQ_Seq_TCTTranNo.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// 
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

		public DataSet Seq_Common_Get_TranNo(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Seq_Common_Get_TranNo";
			string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get_TranNo;
			alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
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
				string strTableName = "Seq_TranNo";
				string strFormat = "{0}{1}{2}";
				string strParam0 = null;
				string strParam1 = null;
				string strParam2 = null;

				////
				{
					// //
					strParam0 = TConst.BizMix.TVAN;
					// //
					strParam1 = dtimeSys.Year.ToString();
					// //
					long nSeq = Seq_Common_Raw(strTableName);
					strParam2 = TUtils.MyStringUtils.To10Mask(nSeq, 10);
				}
				#endregion

				#region // SequenceGet:
				////
				string strResult = "";
				{
					strResult = string.Format(
						strFormat // Format
						, strParam0 // {0}
						, strParam1 // {1}
						, strParam2 // {2}
						);
				}



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

        private void Seq_Box_Carton_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSeqType
            , object objSeqYear
            ////
            , out List<string> lstSeq_ObjCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_GenObjCode_Get";
            //string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenObjCode_Get;
            alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
                });
            //TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
            #endregion

            #region // Convert Input:
            DateTime dtimeTDate = DateTime.UtcNow;
            #endregion

            #region // Refine and Check Input:
            ////
            //string strOrgIDSln = null;
            //RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
            //{
            //    #region // WA_MstSv_OrgInNetwork_Login:
            //    MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
            //    objMstSv_OrgInNetwork.NetworkID = strNetworkID;
            //    objMstSv_OrgInNetwork.OrgID = strOrgID;

            //    /////
            //    RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
            //    {

            //        Tid = strTid,
            //        TokenID = strOS_MasterServer_Solution_API_Url,
            //        NetworkID = strNetworkID,
            //        OrgID = strOrgID,
            //        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
            //        GwPassword = strOS_MasterServer_Solution_GwPassword,
            //        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
            //        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
            //    };
            //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
            //    objRQ_MstSv_OrgInNetwork.MstSv_OrgInNetwork = objMstSv_OrgInNetwork;
            //    ////
            //    try
            //    {
            //        alParamsCoupleError.AddRange(new object[]{
            //            "Check.OS_MasterServer_Solution_API_Url", _cf.nvcParams["OS_MasterServer_Solution_API_Url"]
            //            });
            //        objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(_cf.nvcParams["OS_MasterServer_Solution_API_Url"], objRQ_MstSv_OrgInNetwork);
            //        strOrgIDSln = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            //        ////
            //    }
            //    catch (Exception cex)
            //    {
            //        TUtils.CProcessExc.BizShowException(
            //            ref alParamsCoupleError // alParamsCoupleError
            //            , cex // cex
            //            );

            //        throw CmUtils.CMyException.Raise(
            //            TError.ErridnInventory.CmSys_InvalidOutSite
            //            , null
            //            , alParamsCoupleError.ToArray()
            //            );
            //    }
            //    ////
            //    #endregion
            //}
            //string strOrgID = "10000";
            string strVersion = "0";
            string strSolution = "1";
            Int64 objObjCodeAmount = 0;
            Int64 nObjAmount = Convert.ToInt64(objObjCodeAmount);
            lstSeq_ObjCode = new List<string>();

            string strTableName = "Seq_InvoiceCode";
            string strFormat = "{0}{1}{2}{3}";
            string strFormatFinal = "{0}{1}{2}{3}{4}{5}";
            string strOrgID36 = "10002";//strOrgIDSln;                
            string strYear36 = TUtils.CMyBase36.To36((Convert.ToInt32(dtimeSys.Year) - 2010), 1);
            string strRandom36 = null;
            string strChecksum36 = null;
            #endregion

            #region // SequenceGet:
            ////
            string strResult = "";
            string strResultFinal = "";
            for (int nCount = 0; nCount < nObjAmount; nCount++)
            {
                ////
                {
                    // //
                    long nSeq = Seq_Common_Raw(strTableName);
                    long nMaxSeq = 10000000;
                    string strRandom = Convert.ToString(nSeq % nMaxSeq);
                    Int32 nRandom = Convert.ToInt32(strRandom);

                    strRandom36 = TUtils.CMyBase36.To36(nRandom, 6);
                }
                ////
                strResult = string.Format(
                    strFormat // Format
                    , strVersion // {0}
                    , strYear36 // {1}
                    , strSolution // {2}
                    , strOrgID36.Substring(0, 1) // {3}
                    );

                ////
                List<int> lstChar = new List<int>();
                for (int nScan = 0; nScan < strResult.Length; nScan++)
                {
                    ////
                    char objchar = strResult[nScan];

                    int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
                    lstChar.Add(nchar);
                }

                Int32 nSum = lstChar[0] * 3 + lstChar[1] * 5 + lstChar[2] * 7 + lstChar[3] * 11;
                string strMod = Convert.ToString(nSum % 36);
                strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMod), 1);

                strResultFinal = string.Format(
                    strFormatFinal // Format
                    , strVersion
                    , strYear36
                    , strSolution
                    , strOrgID36
                    , strChecksum36
                    , strRandom36
                    );
                lstSeq_ObjCode.Add(strResultFinal);
            }
            //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResultFinal);
            #endregion
        }
        public DataSet Seq_Box_Carton_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSeqType
            , object objSeqYear
            /////
            , object objQty
            ////
            , out List<string> lstSeq_ObjCode
           )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_Box_Carton_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Seq_Box_Carton_Get;
            alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strNetworkID", strNetworkID
                , "objOrgID", objOrgID
                , "objSeqType", objSeqType
                , "objSeqYear", objSeqYear
                });
            lstSeq_ObjCode = new List<string>();

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
                Seq_Box_Carton_GetX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objOrgID // objOrgID
                    , objSeqType // objSeqType
                    , objSeqYear // objSeqYear
                                 ////
                    , out lstSeq_ObjCode // lstSeq_ObjCode
                    );
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
        public DataSet WAS_Seq_Common_Get_TranNo(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_TranNo objRQ_Seq_TranNo
			////
			, out RT_Seq_TranNo objRT_Seq_TranNo
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_TranNo.Tid;
			objRT_Seq_TranNo = new RT_Seq_TranNo();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_TranNo.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_Common_Get_TranNo";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_Common_Get_TranNo;
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

				#region // WS_Seq_Common_Get_CtrDpsCodeForTaiTuc:
				mdsResult = Seq_Common_Get_TranNo(
					objRQ_Seq_TranNo.Tid // strTid
					, objRQ_Seq_TranNo.GwUserCode // strGwUserCode
					, objRQ_Seq_TranNo.GwPassword // strGwPassword
					, objRQ_Seq_TranNo.WAUserCode // strUserCode
					, objRQ_Seq_TranNo.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// 
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

		public DataSet WAS_Seq_Common_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_Common objRQ_Seq_Common
			////
			, out RT_Seq_Common objRT_Seq_Common
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_Common.Tid;
			objRT_Seq_Common = new RT_Seq_Common();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_Common_Get;
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

				#region // WS_Seq_Common_Get:
				mdsResult = Seq_Common_Get(
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

		public DataSet WAS_Seq_InvoiceCode_GetByAmount(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode
			////
			, out RT_Seq_InvoiceCode objRT_Seq_InvoiceCode
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_InvoiceCode.Tid;
			objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_InvoiceCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_InvoiceCode_Get;
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
				List<string> lstSeq_InvoiceCode = null;
				#endregion

				#region // WS_Seq_InvoiceCode_Get:
				mdsResult = Seq_InvoiceCode_GetByAmount(
					objRQ_Seq_InvoiceCode.Tid // strTid
					, objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
					, objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
					, objRQ_Seq_InvoiceCode.WAUserCode // strUserCode
					, objRQ_Seq_InvoiceCode.WAUserPassword // strUserPassword
					, objRQ_Seq_InvoiceCode.NetworkID // strNetworkID
					, objRQ_Seq_InvoiceCode.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					////
					, objRQ_Seq_InvoiceCode.InvoiceAmount // objInvoiceAmount
					////
					, out lstSeq_InvoiceCode // lstSeq_InvoiceCode
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					objRT_Seq_InvoiceCode.Lst_Seq_InvoiceCode = lstSeq_InvoiceCode;
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

		public DataSet WAS_Seq_InvoiceCode_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode
			////
			, out RT_Seq_InvoiceCode objRT_Seq_InvoiceCode
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_InvoiceCode.Tid;
			objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_InvoiceCode_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_InvoiceCode_Get;
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

				#region // WS_Seq_InvoiceCode_Get:
				mdsResult = Seq_InvoiceCode_Get(
					objRQ_Seq_InvoiceCode.Tid // strTid
					, objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
					, objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
					, objRQ_Seq_InvoiceCode.WAUserCode // strUserCode
					, objRQ_Seq_InvoiceCode.WAUserPassword // strUserPassword
					, objRQ_Seq_InvoiceCode.NetworkID // strNetworkID
					, objRQ_Seq_InvoiceCode.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
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

        public DataSet WAS_Seq_GenObjCode_Get(
           ref ArrayList alParamsCoupleError
           , RQ_Seq_ObjCode objRQ_Seq_ObjCode
           ////
           , out RT_Seq_ObjCode objRT_Seq_ObjCode
           )
        {
            #region // Temp:
            string strTid = objRQ_Seq_ObjCode.Tid;
            objRT_Seq_ObjCode = new RT_Seq_ObjCode();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Seq_GenObjCode_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenObjCode_Get;
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
                List<string> lstSeq_ObjCode = null;
                #endregion

                #region // WS_Seq_InvoiceCode_Get:
                mdsResult = Seq_GenObjCode_Get(
                    objRQ_Seq_ObjCode.Tid // strTid
                    , objRQ_Seq_ObjCode.GwUserCode // strGwUserCode
                    , objRQ_Seq_ObjCode.GwPassword // strGwPassword
                    , objRQ_Seq_ObjCode.WAUserCode // strUserCode
                    , objRQ_Seq_ObjCode.WAUserPassword // strUserPassword
                    , objRQ_Seq_ObjCode.NetworkID // strNetworkID                    
                    , ref alParamsCoupleError // alParamsCoupleError
                    ////
                    , objRQ_Seq_ObjCode.OrgIDSln // objOrgIDSln
                    , objRQ_Seq_ObjCode.ObjCodeAmount
                    , out lstSeq_ObjCode
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    objRT_Seq_ObjCode.Lst_Seq_ObjCode = lstSeq_ObjCode;
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

        public DataSet WAS_Seq_GenEngine_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode
            ////
            , out RT_Seq_InvoiceCode objRT_Seq_InvoiceCode
            )
        {
            #region // Temp:
            string strTid = objRQ_Seq_InvoiceCode.Tid;
            objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Seq_GenEngine_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_GenEngine_Get;
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

                #region // WS_Seq_InvoiceCode_Get:
                mdsResult = Seq_GenEngine_Get(
                    objRQ_Seq_InvoiceCode.Tid // strTid
                    , objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
                    , objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
                    , objRQ_Seq_InvoiceCode.WAUserCode // strUserCode
                    , objRQ_Seq_InvoiceCode.WAUserPassword // strUserPassword
                    , objRQ_Seq_InvoiceCode.NetworkID // strNetworkID                    
                    , ref alParamsCoupleError // alParamsCoupleError
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

        public DataSet WAS_Seq_FormNo_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Seq_FormNo objRQ_Seq_FormNo
			////
			, out RT_Seq_FormNo objRT_Seq_FormNo
			)
		{
			#region // Temp:
			string strTid = objRQ_Seq_FormNo.Tid;
			objRT_Seq_FormNo = new RT_Seq_FormNo();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Seq_FormNo_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_FormNo_Get;
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

				#region // WS_Seq_Common_Get:
				mdsResult = Seq_FormNo_Get(
					objRQ_Seq_FormNo.Tid // strTid
					, objRQ_Seq_FormNo.GwUserCode // strGwUserCode
					, objRQ_Seq_FormNo.GwPassword // strGwPassword
					, objRQ_Seq_FormNo.WAUserCode // strUserCode
					, objRQ_Seq_FormNo.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  // //
					, objRQ_Seq_FormNo.Seq_FormNo.InvoiceType.ToString() // strInvoiceType
					, objRQ_Seq_FormNo.Seq_FormNo.MST.ToString() // strMST
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
        //*/
        public DataSet WAS_Seq_Common_GetMulti(
            ref ArrayList alParamsCoupleError
            , RQ_Seq_Common objRQ_Seq_Common
            ////
            , out RT_Seq_Common objRT_Seq_Common
            )
        {
            #region // Temp:
            string strTid = objRQ_Seq_Common.Tid;
            objRT_Seq_Common = new RT_Seq_Common();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Seq_Common_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Seq_Common_Get;
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
                List<string> lstSeq_ObjCode = null;
                #endregion

                #region // Seq_Common_GetMulti:
                mdsResult = Seq_Common_GetMulti(
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
                    ////
                    , objRQ_Seq_Common.Qty // objQty
                    , out lstSeq_ObjCode
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    objRT_Seq_Common.Lst_Seq_ObjCode = lstSeq_ObjCode;
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

        public DataSet Seq_Common_GetMulti(
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
            ////
            , object objQty
            , out List<string> lstSeq_ObjCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Seq_Common_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Seq_Common_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "strSequenceType", strSequenceType
                , "strParam_Prefix", strParam_Prefix
                , "strParam_Postfix", strParam_Postfix
                , "objQty", objQty
                });
            TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
            lstSeq_ObjCode = new List<string>();
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
                Seq_Common_MyGetMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strSequenceType // strSequenceType
                    , strParam_Prefix // strParam_Prefix
                    , strParam_Postfix // strParam_Postfix
                    ////
                    , objQty // objQty
                    , out lstSeq_ObjCode // lstSeq_ObjCode
                    );
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

        private void Seq_Common_MyGetMulti(
            ref ArrayList alParamsCoupleError
            , string strSequenceType
            , string strParam_Prefix
            , string strParam_Postfix
            ////
            , object objQty
            , out List<string> lstSeq_ObjCode
            )
        {
            #region // Get and Check Map:
            Hashtable htMap = new Hashtable();
            htMap.Add(TConst.SeqType.Id, new string[] { "Seq_Id", "{0}{1}{2}", "999000000000" });
            //htMap.Add(TConst.SeqType.DiscountDBCode, new string[] { "Seq_MasterDataId", "{0}DCDB.{3}.{1:0000}{2}", "10000" });
            htMap.Add(TConst.SeqType.InsuranceClaimDocCode, new string[] { "Seq_TransactionDataId", "{0}ICDC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.InsuranceClaimNo, new string[] { "Seq_TransactionDataId", "{0}ICN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.WorkingRecordNo, new string[] { "Seq_TransactionDataId", "{0}WRN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.LevelCode, new string[] { "Seq_TransactionDataId", "{0}LVC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.CampaignCrAwardCode, new string[] { "Seq_TransactionDataId", "{0}CCRAC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.CampaignCode, new string[] { "Seq_TransactionDataId", "{0}CCRAC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.CICode, new string[] { "Seq_TransactionDataId", "{0}CIC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.InputCode, new string[] { "Seq_InputCode", "{0}IPC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.PrdStateNo, new string[] { "Seq_PrdStateNo", "{0}PRDNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.ColReleaseNo, new string[] { "Seq_ColReleaseNo", "{0}COLRELEASENO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.MDCrtBatchNo, new string[] { "Seq_MDCrtBatchNo", "{0}MDCRTBATCHNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.MDAutoBatchNo, new string[] { "Seq_MDAutoBatchNo", "{0}MDAUTOBATCHNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.CBABatchNo, new string[] { "Seq_CBABatchNo", "{0}CBABATCHNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.TranNo, new string[] { "Seq_TranNo", "{0}TRANNO.{3}.{1:00000}{2}", "100000" });

            htMap.Add(TConst.SeqType.TaxRegNo, new string[] { "Seq_TaxRegNo", "{0}REGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.TaxRegStopNo, new string[] { "Seq_TaxRegStopNo", "{0}REGSTOPNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.TaxSubNo, new string[] { "Seq_TaxSubNo", "{0}SUBNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.TInvoiceCode, new string[] { "Seq_TInvoiceCode ", "{0}TINVOICODE.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.BatchNo, new string[] { "Seq_BatchNo ", "{0}BATCHNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.PRTCode, new string[] { "Seq_PrintTempCode ", "{0}PRTCODE.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.SerialNo, new string[] { "Seq_SerialNo  ", "{0}SERIALNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.GenTimesNo, new string[] { "Seq_GenTimesNo", "{0}GENTIMESNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvInFGNo, new string[] { "Seq_IFInvInFGNo", "{0}IFINVINFGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvOutFGNo, new string[] { "Seq_IFInvOutFGNo", "{0}IFINVOUTFGNO.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvInNo, new string[] { "Seq_IF_InvInNo", "{0}PN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvOutNo, new string[] { "Seq_IF_InvOutNo", "{0}PX.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFMONo, new string[] { "Seq_IF_MONo", "{0}DC.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvCusReturnNo, new string[] { "Seq_IF_InvCusReturnNo", "{0}KT.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvAudNo, new string[] { "Seq_IF_InvAudNo", "{0}KK.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFInvReturnSupNo, new string[] { "Seq_IF_InvReturnSupNo", "{0}TN.{3}.{1:00000}{2}", "100000" });
            htMap.Add(TConst.SeqType.IFTempPrintNo, new string[] { "Seq_IF_TempPrintNo", "{0}IFTEMPPRINTNO.{3}.{1:00000}{2}", "100000" });

            ////
            htMap.Add(TConst.MstSvSeqType.InvoiceCode, new string[] { "MstSv_Seq_InvoiceCode ", "{0}INVOICECODE.{3}.{1:00000}{2}", "100000" });

            //

            if (!htMap.ContainsKey(strSequenceType))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strSequenceType", strSequenceType
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Seq_Common_MyGet_InvalidSequenceType
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
            int nQty = Convert.ToInt16(objQty);
            lstSeq_ObjCode = new List<string>();
            string strResult = "";
            long nSeq = Seq_Common_Raw(strTableName);
            string strMyEncrypt = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < nQty; i++)
            {
                ////
                strResult = string.Format(
                    strFormat // Format
                    , strParam_Prefix // {0}
                    , nSeq % nMaxSeq // {1}
                    , strParam_Postfix // {2}
                    , string.Format("{0}{1}{2}", strMyEncrypt[DateTime.UtcNow.Year - TConst.BizMix.Default_RootYear], strMyEncrypt[DateTime.UtcNow.Month], strMyEncrypt[DateTime.UtcNow.Day]) // {3}
                    );
                lstSeq_ObjCode.Add(strResult);
            }
            #endregion

            // Return Good:
            //return strResult;
        }
    }
}
