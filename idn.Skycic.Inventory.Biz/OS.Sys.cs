using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
using inos.common.Constants;
using inos.common.Model;
using inos.common.Service;


namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // OS_Sys:
		private void WAC_OS_Sys_AT_3A_Invoice_Delete(
			ref ArrayList alParamsCoupleError
			, string strWSUrlAddr
			, string strRefId
			)
		{
            #region // Temp:
            string strFunctionName = "WAC_OS_Sys_AT_3A_Invoice_Delete";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strWSUrlAddr", strWSUrlAddr
                , "strRefId", strRefId
                });
            #endregion

            #region // Init:
            OSSys_AT_3A_InputParam<OSSys_AT_3A_CheckDeleteData> objOSSys_AT_3A_InputParam = new OSSys_AT_3A_InputParam<OSSys_AT_3A_CheckDeleteData>();
			objOSSys_AT_3A_InputParam.Data = new OSSys_AT_3A_CheckDeleteData();
			objOSSys_AT_3A_InputParam.Data.RefId = strRefId;
			objOSSys_AT_3A_InputParam.Action = "CheckDelete";
			string JsonData = TJson.JsonConvert.SerializeObject(objOSSys_AT_3A_InputParam);
            ////
            ///// ThomPTT.20190719
            #region // Add alParamsCoupleError
            {
                alParamsCoupleError.AddRange(new object[]{
                    "JsonData", JsonData
                    });
            }
            #endregion
            #endregion

            #region // Call WA:
            string result = Post2Server(strWSUrlAddr, "", JsonData, "POST");
			OSSys_AT_3A_ServiceResult objOSSys_AT_3A_ServiceResult = TJson.JsonConvert.DeserializeObject<OSSys_AT_3A_ServiceResult>(result);
			//objOSSys_AT_3A_ServiceResult.status = "false";
			bool bIsNotError = Convert.ToBoolean(objOSSys_AT_3A_ServiceResult.status);
            ///// ThomPTT.20190719
            #region // Add alParamsCoupleError
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Result.objOSSys_AT_3A_ServiceResult.", TJson.JsonConvert.SerializeObject(objOSSys_AT_3A_ServiceResult)
                    });
            }
            #endregion
            /////
            if (!bIsNotError)
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.strRefId", strRefId
					, "Check.status", objOSSys_AT_3A_ServiceResult.status
					, "Check.message", objOSSys_AT_3A_ServiceResult.message
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.WAC_OS_Sys_AT_3A_Invoice_Delete_HasError
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		public void OS_Sys_AT_3A_Invoice_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objInvoiceCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_Sys_AT_3A_Invoice_DeleteX";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_BuildAndCreate;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objInvoiceCode", objInvoiceCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strInvoiceCode = TUtils.CUtils.StdParam(objInvoiceCode);
			string strWSUrlAddr = @"http://svn.3asoft.vn:8686/DataService.ashx";
			#endregion

			#region // WAC_OS_Sys_AT_3A_Invoice_Delete:
			WAC_OS_Sys_AT_3A_Invoice_Delete(
				ref alParamsCoupleError // alParamsCoupleError
				, strWSUrlAddr // strWSUrlAddr
				, strInvoiceCode // strRefId
				);
			#endregion

			// Return Good:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}

		public DataSet OS_Sys_AT_3A_Invoice_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objInvoiceCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_Sys_AT_3A_Invoice_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.OS_Sys_AT_3A_Invoice_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
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

				#region // OS_Sys_AT_3A_Invoice_DeleteX:
				{
					OS_Sys_AT_3A_Invoice_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
												  ////
						, objInvoiceCode // objInvoiceCode
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

		public DataSet WAS_OS_Sys_AT_3A_Invoice_Delete(
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
			string strFunctionName = "WA_OS_Sys_AT_3A_Invoice_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Sys_AT_3A_Invoice_Delete;
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
				List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
				#endregion

				#region // OS_Sys_AT_3A_Invoice_Delete:
				mdsResult = OS_Sys_AT_3A_Invoice_Delete(
					objRQ_Invoice_Invoice.Tid // strTid
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					, objRQ_Invoice_Invoice.WAUserCode // strUserCode
					, objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
					, objRQ_Invoice_Invoice.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Invoice_Invoice.Invoice_Invoice.InvoiceCode // objMST
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

		#region // OS_Sys_AT_3A:
		public string Post2Server(string server, string apiName, string sData, string sMethod)
		{


			string _urlAPI = server + apiName;
			string _strReturn = "";

			try
			{
				ServicePointManager.ServerCertificateValidationCallback -= new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
			}
			catch { }
			if (_urlAPI.Contains("https:"))
				ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
			using (WebClient wc = new WebClient())
			{


				wc.Headers[HttpRequestHeader.Accept] = "application/json; charset=UTF-8";
				wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=UTF-8";

				wc.Encoding = UTF8Encoding.UTF8;



				if (sMethod == "GET")
					_strReturn = System.Text.Encoding.UTF8.GetString(wc.DownloadData(_urlAPI + "?" + sData));
				else
					_strReturn = wc.UploadString(_urlAPI, sMethod, sData);
				//ServiceResult3A<OS3AInvoiceDel> objServiceResult3A = TJson.JsonConvert.DeserializeObject<ServiceResult3A<OS3AInvoiceDel>>(_strReturn);

			}


			return _strReturn;
		}

		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
		#endregion
	}
}
