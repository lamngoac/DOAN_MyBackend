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
using inos.common.Constants;
using inos.common.Model;
using inos.common.Service;
using System.Diagnostics;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Report Server:
		public DataSet RptSv_Invoice_Invoice_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objInvoiceCode
			//// Return:
			, string strRt_Cols_Invoice_Invoice
			, string strRt_Cols_Invoice_InvoiceDtl
			, string strRt_Cols_Invoice_InvoicePrd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Invoice_Invoice_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Invoice_Invoice_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objInvoiceCode", objInvoiceCode
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//RptSv_Sys_Access_CheckDeny(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strInvoiceCode = TUtils.CUtils.StdParam(objInvoiceCode);
				string strOrgIDSln = null;

				////
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
				List<Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
				List<Invoice_InvoicePrd> lst_Invoice_InvoicePrd = new List<Invoice_InvoicePrd>();

				bool bGet_Invoice_Invoice = (strRt_Cols_Invoice_Invoice != null && strRt_Cols_Invoice_Invoice.Length > 0);
				bool bGet_Invoice_InvoiceDtl = (strRt_Cols_Invoice_InvoiceDtl != null && strRt_Cols_Invoice_InvoiceDtl.Length > 0);
				bool bGet_Invoice_InvoicePrd = (strRt_Cols_Invoice_InvoicePrd != null && strRt_Cols_Invoice_InvoicePrd.Length > 0);

				#endregion

				#region // Unpack:
				{
					strOrgIDSln = strInvoiceCode.Substring(0, 4);
				}
				#endregion

				#region // Call Func:
				////
				string strNetWorkUrl = null;
				string strNetworkID = null;
				////
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
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
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetByOrgIDSln(objRQ_MstSv_OrgInNetwork);
						strNetworkID = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
				////
				RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
				{
					#region // WA_MstSv_Sys_User_Login:
					MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

					/////
					RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
					};
					////
					try
					{
						objRT_MstSv_Sys_User = OS_MstSvTVANService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
						strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
				////
				RT_Invoice_Invoice objRT_Invoice_Invoice = null;
				{
					{
						#region // WA_Invoice_Invoice_GetNoSession:
						RQ_Invoice_Invoice objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
						{
							WAUserCode = _cf.nvcParams["OS_Network_BG_WAUserCode"],
							WAUserPassword = _cf.nvcParams["OS_Network_BG_WAUserPassword"],
							GwUserCode = strOS_MasterServer_Solution_GwUserCode,
							GwPassword = strOS_MasterServer_Solution_GwPassword,
							//OrgID = strOrgID,
							Tid = strTid,
							Rt_Cols_Invoice_Invoice = strRt_Cols_Invoice_Invoice,
							Rt_Cols_Invoice_InvoiceDtl = strRt_Cols_Invoice_InvoiceDtl,
							Rt_Cols_Invoice_InvoicePrd = strRt_Cols_Invoice_InvoicePrd,
							Ft_RecordStart = "0",
							Ft_RecordCount = "123456000",
							Ft_WhereClause = CmUtils.StringUtils.Replace(@"Invoice_Invoice.InvoiceCode = '@strInvoiceCode'", "@strInvoiceCode", strInvoiceCode)
						};

						////
						try
						{
							objRT_Invoice_Invoice = OS_MstSvTVAN_Invoice_InvoiceService.Instance.WA_Invoice_Invoice_GetNoSession(strNetWorkUrl, objRQ_Invoice_Invoice);

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
				}
				#endregion

				#region // GetData:
				DataSet dsGetData = new DataSet();
				{
					////
					DataTable dt_MySummaryTable = new DataTable();
					List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
					lstMySummaryTable.Add(objRT_Invoice_Invoice.MySummaryTable);
					dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
					dsGetData.Tables.Add(dt_MySummaryTable.Copy());
					////
					if (bGet_Invoice_Invoice)
					{
						////
						DataTable dt_Invoice_Invoice = new DataTable();
						dt_Invoice_Invoice = TUtils.DataTableCmUtils.ToDataTable<Invoice_Invoice>(objRT_Invoice_Invoice.Lst_Invoice_Invoice, "Invoice_Invoice");
						dsGetData.Tables.Add(dt_Invoice_Invoice.Copy());
					}
					////
					if (bGet_Invoice_InvoiceDtl)
					{
						////
						DataTable dt_Invoice_InvoiceDtl = new DataTable();
						dt_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToDataTable<Invoice_InvoiceDtl>(objRT_Invoice_Invoice.Lst_Invoice_InvoiceDtl, "Invoice_InvoiceDtl");
						dsGetData.Tables.Add(dt_Invoice_InvoiceDtl.Copy());
					}
					////
					if (bGet_Invoice_InvoicePrd)
					{
						////
						DataTable dt_Invoice_InvoicePrd = new DataTable();
						dt_Invoice_InvoicePrd = TUtils.DataTableCmUtils.ToDataTable<Invoice_InvoicePrd>(objRT_Invoice_Invoice.Lst_Invoice_InvoicePrd, "Invoice_InvoicePrd");
						dsGetData.Tables.Add(dt_Invoice_InvoicePrd.Copy());
					}
				}
				////
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strNetWorkUrl);
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

		public DataSet RptSv_Inv_InventoryBalanceSerial_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objObjectMixCode
			//// Return:
			, string strRt_Cols_Inv_InventoryBalanceSerial
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Inv_InventoryBalanceSerial_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Inv_InventoryBalanceSerial_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objObjectMixCode", objObjectMixCode
				//// Return
				, "strRt_Cols_Inv_InventoryBalanceSerial", strRt_Cols_Inv_InventoryBalanceSerial
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//RptSv_Sys_Access_CheckDeny(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strObjectMixCode = TUtils.CUtils.StdParam(objObjectMixCode);
				string strOrgIDSln = null;

				////
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<Inv_InventoryBalanceSerial> lst_Inv_InventoryBalanceSerial = new List<Inv_InventoryBalanceSerial>();

				bool bGet_Inv_InventoryBalanceSerial = (strRt_Cols_Inv_InventoryBalanceSerial != null && strRt_Cols_Inv_InventoryBalanceSerial.Length > 0);

				#endregion

				#region // Unpack:
				{
					strOrgIDSln = strObjectMixCode.Substring(3, 5);
				}
				#endregion

				#region // Call Func:
				////
				string strNetWorkUrl = null;
				string strNetworkID = null;
				////
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
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
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetByOrgIDSln(objRQ_MstSv_OrgInNetwork);
						strNetworkID = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
							TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
				////
				RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
				{
					#region // WA_MstSv_Sys_User_Login:
					MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

					/////
					RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
					};
					////
					try
					{
						objRT_MstSv_Sys_User = OS_MstSvTVANService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
						strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
							TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
				////
				RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
				{
					{
						#region // WA_Invoice_Invoice_GetNoSession:
						RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
						{
							WAUserCode = _cf.nvcParams["OS_Network_BG_WAUserCode"],
							WAUserPassword = _cf.nvcParams["OS_Network_BG_WAUserPassword"],
							GwUserCode = strOS_MasterServer_Solution_GwUserCode,
							GwPassword = strOS_MasterServer_Solution_GwPassword,
							//OrgID = strOrgID,
							Tid = strTid,
							Rt_Cols_Inv_InventoryBalanceSerial = strRt_Cols_Inv_InventoryBalanceSerial,
							Ft_RecordStart = "0",
							Ft_RecordCount = "123456000",
							Ft_WhereClause = CmUtils.StringUtils.Replace(@"(Inv_InventoryBalanceSerial.SerialNo = '@strObjectMixCode' or Inv_InventoryBalanceSerial.BoxNo = '@strObjectMixCode' or Inv_InventoryBalanceSerial.CanNo = '@strObjectMixCode') and Inv_InventoryBalanceSerial.FlagSales = '1'", "@strObjectMixCode", strObjectMixCode)
						};

						////
						try
						{
							objRT_Inv_InventoryBalanceSerial = OS_MstSvTVAN_Inv_InventoryBalanceSerial.Instance.WA_Inv_InventoryBalanceSerial_Get(strNetWorkUrl, objRQ_Inv_InventoryBalanceSerial);

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
								TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                , null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
				}
				#endregion

				#region // GetData:
				DataSet dsGetData = new DataSet();
				{
					////
					DataTable dt_MySummaryTable = new DataTable();
					List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
					lstMySummaryTable.Add(objRT_Inv_InventoryBalanceSerial.MySummaryTable);
					dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
					dsGetData.Tables.Add(dt_MySummaryTable.Copy());
					////
					if (bGet_Inv_InventoryBalanceSerial)
					{
						////
						DataTable dt_Inv_InventoryBalanceSerial = new DataTable();
						dt_Inv_InventoryBalanceSerial = TUtils.DataTableCmUtils.ToDataTable<Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial, "Inv_InventoryBalanceSerial");
						dsGetData.Tables.Add(dt_Inv_InventoryBalanceSerial.Copy());
					}
				}
				////
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strNetWorkUrl);
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

		public DataSet WAS_RptSv_Inv_InventoryBalanceSerial_Get(
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
			string strFunctionName = "WAS_RptSv_Inv_InventoryBalanceSerial_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Inv_InventoryBalanceSerial_Get;
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
				mdsResult = RptSv_Inv_InventoryBalanceSerial_Get(
					objRQ_Inv_InventoryBalanceSerial.Tid // strTid
					, objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
					, objRQ_Inv_InventoryBalanceSerial.WAUserCode // strUserCode
					, objRQ_Inv_InventoryBalanceSerial.WAUserPassword // strUserPassword
					, objRQ_Inv_InventoryBalanceSerial.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Inv_InventoryBalanceSerial.ObjectMixCode // objObjectMixCode
																	 ////
					, objRQ_Inv_InventoryBalanceSerial.Rt_Cols_Inv_InventoryBalanceSerial // strRt_Cols_Inv_InventoryBalanceSerial
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
		public DataSet RptSv_Rpt_Inv_InventoryBalanceSerialForSearch(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, object objObjectMixCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Rpt_Inv_InventoryBalanceSerialForSearch";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Rpt_Inv_InventoryBalanceSerialForSearch;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objObjectMixCode", objObjectMixCode
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//RptSv_Sys_Access_CheckDeny(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strObjectMixCode = TUtils.CUtils.StdParam(objObjectMixCode);
				string strOrgIDSln = null;
				#endregion

				#region // Unpack:
				{
					strOrgIDSln = strObjectMixCode.Substring(3, 5);
				}
				#endregion

				#region // Call Func:
				////
				string strNetWorkUrl = null;
				string strNetworkID = null;
				////
				RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;

					/////
					RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork = new RQ_MstSv_OrgInNetwork()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
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
						objRT_MstSv_OrgInNetwork = OS_MstSvTVANService.Instance.WA_OS_MstSv_OrgInNetwork_GetByOrgIDSln(objRQ_MstSv_OrgInNetwork);
						strNetworkID = objRT_MstSv_OrgInNetwork.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
							TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
				////
				RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
				{
					#region // WA_MstSv_Sys_User_Login:
					MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

					/////
					RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = strNetworkID,
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
					};
					////
					try
					{
						objRT_MstSv_Sys_User = OS_MstSvTVANService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
						strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
							TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
				////
				RT_Rpt_Inv_InventoryBalanceSerialForSearch objRT_Rpt_Inv_InventoryBalanceSerialForSearch = null;
				{
					{
						#region // WA_Invoice_Invoice_GetNoSession:
						RQ_Rpt_Inv_InventoryBalanceSerialForSearch objRQ_Rpt_Inv_InventoryBalanceSerialForSearch = new RQ_Rpt_Inv_InventoryBalanceSerialForSearch()
						{
							WAUserCode = _cf.nvcParams["OS_Network_BG_WAUserCode"],
							WAUserPassword = _cf.nvcParams["OS_Network_BG_WAUserPassword"],
							GwUserCode = strOS_MasterServer_Solution_GwUserCode,
							GwPassword = strOS_MasterServer_Solution_GwPassword,
							//OrgID = strOrgID,
							Tid = strTid,
							Ft_RecordStart = "0",
							Ft_RecordCount = "123456000",
							ObjectQRMix = strObjectMixCode
						};

						////
						try
						{
							objRT_Rpt_Inv_InventoryBalanceSerialForSearch = OS_MstSvTVAN_Report.Instance.WA_Rpt_Inv_InventoryBalanceSerialForSearch(strNetWorkUrl, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch);

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
								TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                , null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
				}
				#endregion

				#region // GetData:
				DataSet dsGetData = new DataSet();
				{
					////
					DataTable dt_Rpt_Inv_InventoryBalanceSerialForSearch = new DataTable();
					dt_Rpt_Inv_InventoryBalanceSerialForSearch = TUtils.DataTableCmUtils.ToDataTable<Rpt_Inv_InventoryBalanceSerialForSearch>(objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_Inv_InventoryBalanceSerialForSearch, "Rpt_Inv_InventoryBalanceSerialForSearch");
					dsGetData.Tables.Add(dt_Rpt_Inv_InventoryBalanceSerialForSearch.Copy());
					/////
					DataTable dt_Rpt_InvF_InventoryOutHistInstSerialForSearch = new DataTable();
					dt_Rpt_InvF_InventoryOutHistInstSerialForSearch = TUtils.DataTableCmUtils.ToDataTable<Rpt_InvF_InventoryOutHistInstSerialForSearch>(objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch, "Rpt_InvF_InventoryOutHistInstSerialForSearch");
					dsGetData.Tables.Add(dt_Rpt_InvF_InventoryOutHistInstSerialForSearch.Copy());
				}
				////
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strNetWorkUrl);
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

		public DataSet WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearch(
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
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch;
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
				List<Rpt_Inv_InventoryBalanceSerialForSearch> lst_Rpt_Inv_InventoryBalanceSerialForSearch = new List<Rpt_Inv_InventoryBalanceSerialForSearch>();
				List<Rpt_InvF_InventoryOutHistInstSerialForSearch> lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = new List<Rpt_InvF_InventoryOutHistInstSerialForSearch>();
				/////
				#endregion

				#region // WS_Rpt_Inv_InventoryBalanceSerialForSearch:
				mdsResult = RptSv_Rpt_Inv_InventoryBalanceSerialForSearch(
					objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid // strTid
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwUserCode // strGwUserCode
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwPassword // strGwPassword
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.WAUserCode // strUserCode
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.WAUserPassword // strUserPassword
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.ObjectQRMix // objObjectMixCode
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_Rpt_Inv_InventoryBalanceSerialForSearch = mdsResult.Tables["Rpt_Inv_InventoryBalanceSerialForSearch"].Copy();
					lst_Rpt_Inv_InventoryBalanceSerialForSearch = TUtils.DataTableCmUtils.ToListof<Rpt_Inv_InventoryBalanceSerialForSearch>(dt_Rpt_Inv_InventoryBalanceSerialForSearch);
					objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_Inv_InventoryBalanceSerialForSearch = lst_Rpt_Inv_InventoryBalanceSerialForSearch;
					////
					DataTable dt_Rpt_InvF_InventoryOutHistInstSerialForSearch = mdsResult.Tables["Rpt_InvF_InventoryOutHistInstSerialForSearch"].Copy();
					lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = TUtils.DataTableCmUtils.ToListof<Rpt_InvF_InventoryOutHistInstSerialForSearch>(dt_Rpt_InvF_InventoryOutHistInstSerialForSearch);
					objRT_Rpt_Inv_InventoryBalanceSerialForSearch.Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch = lst_Rpt_InvF_InventoryOutHistInstSerialForSearch;
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
		public DataSet RptSv_Invoice_Invoice_GetByMST(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_Invoice_Invoice
			, string strRt_Cols_Invoice_InvoiceDtl
			, string strRt_Cols_Invoice_InvoicePrd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Invoice_Invoice_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Invoice_Invoice_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//RptSv_Sys_Access_CheckDeny(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = null;

				////
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
				List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
				List<Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
				List<Invoice_InvoicePrd> lst_Invoice_InvoicePrd = new List<Invoice_InvoicePrd>();

				bool bGet_Invoice_Invoice = (strRt_Cols_Invoice_Invoice != null && strRt_Cols_Invoice_Invoice.Length > 0);
				bool bGet_Invoice_InvoiceDtl = (strRt_Cols_Invoice_InvoiceDtl != null && strRt_Cols_Invoice_InvoiceDtl.Length > 0);
				bool bGet_Invoice_InvoicePrd = (strRt_Cols_Invoice_InvoicePrd != null && strRt_Cols_Invoice_InvoicePrd.Length > 0);

				#endregion

				#region // Call Func:
				////
				string strNetWorkUrl = null;
				//string strNetworkID = null;
				////
				RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
				{
					#region // WA_MstSv_OrgInNetwork_Login:
					MstSv_OrgInNetwork objMstSv_OrgInNetwork = new MstSv_OrgInNetwork();
					objMstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;

					/////
					RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
					{

						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strOS_MasterServer_Solution_WAUserCode,
						WAUserPassword = strOS_MasterServer_Solution_WAUserPassword,
					};
					objRQ_MstSv_Mst_Network.MstSv_Mst_Network = new MstSv_Mst_Network();
					objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST = objMST;
					////
					try
					{
						objRT_MstSv_Mst_Network = OS_MstSvTVANService.Instance.WA_OS_MstSv_Mst_Network_GetByMST(objRQ_MstSv_Mst_Network);
						strNetWorkUrl = objRT_MstSv_Mst_Network.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
				////
				RT_Invoice_Invoice objRT_Invoice_Invoice = null;
				{
					{
						#region // WA_Invoice_Invoice_GetNoSession:
						RQ_Invoice_Invoice objRQ_Invoice_Invoice = new RQ_Invoice_Invoice()
						{
							WAUserCode = _cf.nvcParams["OS_Network_BG_WAUserCode"],
							WAUserPassword = _cf.nvcParams["OS_Network_BG_WAUserPassword"],
							GwUserCode = strOS_MasterServer_Solution_GwUserCode,
							GwPassword = strOS_MasterServer_Solution_GwPassword,
							//OrgID = strOrgID,
							Tid = strTid,
							Rt_Cols_Invoice_Invoice = strRt_Cols_Invoice_Invoice,
							Rt_Cols_Invoice_InvoiceDtl = strRt_Cols_Invoice_InvoiceDtl,
							Rt_Cols_Invoice_InvoicePrd = strRt_Cols_Invoice_InvoicePrd,
							Ft_RecordStart = strFt_RecordStart,
							Ft_RecordCount = strFt_RecordCount,
							Ft_WhereClause = strFt_WhereClause
						};

						////
						try
						{
							objRT_Invoice_Invoice = OS_MstSvTVAN_Invoice_InvoiceService.Instance.WA_Invoice_Invoice_GetNoSession(strNetWorkUrl, objRQ_Invoice_Invoice);

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
				}
				#endregion

				#region // GetData:
				DataSet dsGetData = new DataSet();
				{
					////
					DataTable dt_MySummaryTable = new DataTable();
					List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
					lstMySummaryTable.Add(objRT_Invoice_Invoice.MySummaryTable);
					dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
					dsGetData.Tables.Add(dt_MySummaryTable.Copy());
					////
					if (bGet_Invoice_Invoice)
					{
						////
						DataTable dt_Invoice_Invoice = new DataTable();
						dt_Invoice_Invoice = TUtils.DataTableCmUtils.ToDataTable<Invoice_Invoice>(objRT_Invoice_Invoice.Lst_Invoice_Invoice, "Invoice_Invoice");
						dsGetData.Tables.Add(dt_Invoice_Invoice.Copy());
					}
					////
					if (bGet_Invoice_InvoiceDtl)
					{
						////
						DataTable dt_Invoice_InvoiceDtl = new DataTable();
						dt_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToDataTable<Invoice_InvoiceDtl>(objRT_Invoice_Invoice.Lst_Invoice_InvoiceDtl, "Invoice_InvoiceDtl");
						dsGetData.Tables.Add(dt_Invoice_InvoiceDtl.Copy());
					}
					////
					if (bGet_Invoice_InvoicePrd)
					{
						////
						DataTable dt_Invoice_InvoicePrd = new DataTable();
						dt_Invoice_InvoicePrd = TUtils.DataTableCmUtils.ToDataTable<Invoice_InvoicePrd>(objRT_Invoice_Invoice.Lst_Invoice_InvoicePrd, "Invoice_InvoicePrd");
						dsGetData.Tables.Add(dt_Invoice_InvoicePrd.Copy());
					}
				}
				////
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strNetWorkUrl);
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

		public DataSet WAS_RptSv_Invoice_Invoice_Get(
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
			string strFunctionName = "WAS_RptSv_Invoice_Invoice_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_Invoice_Get;
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
				List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
				List<Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
				List<Invoice_InvoicePrd> lst_Invoice_InvoicePrd = new List<Invoice_InvoicePrd>();
				/////
				bool bGet_Invoice_Invoice = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice.Length > 0);
				bool bGet_Invoice_InvoiceDtl = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl.Length > 0);
				bool bGet_Invoice_InvoicePrd = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd.Length > 0);
				#endregion

				#region // WS_Invoice_Invoice_Get:
				mdsResult = RptSv_Invoice_Invoice_Get(
					objRQ_Invoice_Invoice.Tid // strTid
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					, objRQ_Invoice_Invoice.WAUserCode // strUserCode
					, objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
					, objRQ_Invoice_Invoice.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Invoice_Invoice.Invoice_Invoice.InvoiceCode // objInvoiceCode
																		////
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice // strRt_Cols_Invoice_Invoice
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl // Rt_Cols_Invoice_InvoiceDtl
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd // Rt_Cols_Invoice_InvoicePrd
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Invoice_Invoice.MySummaryTable = lst_MySummaryTable[0];
					////
					////
					if (bGet_Invoice_Invoice)
					{
						////
						DataTable dt_Invoice_Invoice = mdsResult.Tables["Invoice_Invoice"].Copy();
						lst_Invoice_Invoice = TUtils.DataTableCmUtils.ToListof<Invoice_Invoice>(dt_Invoice_Invoice);
						objRT_Invoice_Invoice.Lst_Invoice_Invoice = lst_Invoice_Invoice;
					}
					////
					if (bGet_Invoice_InvoiceDtl)
					{
						////
						DataTable dt_Invoice_InvoiceDtl = mdsResult.Tables["Invoice_InvoiceDtl"].Copy();
						lst_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToListof<Invoice_InvoiceDtl>(dt_Invoice_InvoiceDtl);
						objRT_Invoice_Invoice.Lst_Invoice_InvoiceDtl = lst_Invoice_InvoiceDtl;
					}
					////
					if (bGet_Invoice_InvoicePrd)
					{
						////
						DataTable dt_Invoice_InvoicePrd = mdsResult.Tables["Invoice_InvoicePrd"].Copy();
						lst_Invoice_InvoicePrd = TUtils.DataTableCmUtils.ToListof<Invoice_InvoicePrd>(dt_Invoice_InvoicePrd);
						objRT_Invoice_Invoice.Lst_Invoice_InvoicePrd = lst_Invoice_InvoicePrd;
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
		public DataSet WAS_RptSv_Invoice_Invoice_GetByMST(
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
			string strFunctionName = "WAS_RptSv_Invoice_Invoice_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_Invoice_Get;
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
				List<Invoice_Invoice> lst_Invoice_Invoice = new List<Invoice_Invoice>();
				List<Invoice_InvoiceDtl> lst_Invoice_InvoiceDtl = new List<Invoice_InvoiceDtl>();
				List<Invoice_InvoicePrd> lst_Invoice_InvoicePrd = new List<Invoice_InvoicePrd>();
				/////
				bool bGet_Invoice_Invoice = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice.Length > 0);
				bool bGet_Invoice_InvoiceDtl = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl.Length > 0);
				bool bGet_Invoice_InvoicePrd = (objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd != null && objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd.Length > 0);
				#endregion

				#region // WS_Invoice_Invoice_Get:
				mdsResult = RptSv_Invoice_Invoice_GetByMST(
					objRQ_Invoice_Invoice.Tid // strTid
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					, objRQ_Invoice_Invoice.WAUserCode // strUserCode
					, objRQ_Invoice_Invoice.WAUserPassword // strUserPassword
					, objRQ_Invoice_Invoice.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Invoice_Invoice.Invoice_Invoice.MST // objInvoiceCode
					, objRQ_Invoice_Invoice.Ft_RecordStart // strFt_RecordStart
					, objRQ_Invoice_Invoice.Ft_RecordCount // strFt_RecordCount
					, objRQ_Invoice_Invoice.Ft_WhereClause // strFt_WhereClause
														   ////
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_Invoice // strRt_Cols_Invoice_Invoice
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoiceDtl // Rt_Cols_Invoice_InvoiceDtl
					, objRQ_Invoice_Invoice.Rt_Cols_Invoice_InvoicePrd // Rt_Cols_Invoice_InvoicePrd
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Invoice_Invoice.MySummaryTable = lst_MySummaryTable[0];
					////
					////
					if (bGet_Invoice_Invoice)
					{
						////
						DataTable dt_Invoice_Invoice = mdsResult.Tables["Invoice_Invoice"].Copy();
						lst_Invoice_Invoice = TUtils.DataTableCmUtils.ToListof<Invoice_Invoice>(dt_Invoice_Invoice);
						objRT_Invoice_Invoice.Lst_Invoice_Invoice = lst_Invoice_Invoice;
					}
					////
					if (bGet_Invoice_InvoiceDtl)
					{
						////
						DataTable dt_Invoice_InvoiceDtl = mdsResult.Tables["Invoice_InvoiceDtl"].Copy();
						lst_Invoice_InvoiceDtl = TUtils.DataTableCmUtils.ToListof<Invoice_InvoiceDtl>(dt_Invoice_InvoiceDtl);
						objRT_Invoice_Invoice.Lst_Invoice_InvoiceDtl = lst_Invoice_InvoiceDtl;
					}
					////
					if (bGet_Invoice_InvoicePrd)
					{
						////
						DataTable dt_Invoice_InvoicePrd = mdsResult.Tables["Invoice_InvoicePrd"].Copy();
						lst_Invoice_InvoicePrd = TUtils.DataTableCmUtils.ToListof<Invoice_InvoicePrd>(dt_Invoice_InvoicePrd);
						objRT_Invoice_Invoice.Lst_Invoice_InvoicePrd = lst_Invoice_InvoicePrd;
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

		#region // RptSv_Sys_User:
		private void RptSv_Sys_User_CheckDB(
			ref ArrayList alParamsCoupleError
			, object strUserCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dt_RptSv_Sys_User
			)
		{
			// GetInfo:
			dt_RptSv_Sys_User = TDALUtils.DBUtils.GetTableContents(
				_cf.db // db
				, "RptSv_Sys_User" // strTableName
				, "top 1 *" // strColumnList
				, "" // strClauseOrderBy
				, "UserCode", "=", strUserCode // arrobjParamsTriple item
				);

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dt_RptSv_Sys_User.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCodeNotFound", strUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.RptSv_Sys_User_CheckDB_UserCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dt_RptSv_Sys_User.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UserCodeExist", strUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.RptSv_Sys_User_CheckDB_UserCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dt_RptSv_Sys_User.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.UserCodeError", strUserCode
					, "Check.FlagActiveListToCheck", strFlagActiveListToCheck
					, "Check.FlagActiveCurrent", dt_RptSv_Sys_User.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.RptSv_Sys_User_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

		}
		public DataSet RptSv_Sys_User_Login(
			string strTid
			, string strRootSvCode
			, string strRootUserCode
			, string strServiceCode
			, string strUserCode
			, string strLanguageCode
			, string strUserPassword
			, string strOtherInfo
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = false;
			//string strErrorCode = null;
			string strFunctionName = "RptSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Login;
			strUserCode = TUtils.CUtils.StdParam(strUserCode);
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "strTid", strTid
				, "strRootSvCode", strRootSvCode
				, "strRootUserCode", strRootUserCode
				, "strServiceCode", strServiceCode
				, "strUserCode", strUserCode
				, "strLanguageCode", strLanguageCode
				, "strOtherInfo", strOtherInfo
				});

			// Manual SessionInfo:
			DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
			drSessionInfo["RootSvCode"] = strRootSvCode;
			drSessionInfo["RootUserCode"] = strRootUserCode;
			drSessionInfo["ServiceCode"] = strServiceCode;
			drSessionInfo["UserCode"] = strUserCode;
			drSessionInfo["LanguageCode"] = strLanguageCode;
			drSessionInfo["InfoExternal"] = strOtherInfo;
			_cf.sinf = new CSessionInfo(drSessionInfo);
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:
				string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
				bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);
				#endregion

				#region // Process:
				// RptSv_Sys_User_CheckDB:
				DataTable dt_RptSv_Sys_User = null;
				RptSv_Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strUserCode // strUserCode
					, TConst.Flag.Active // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dt_RptSv_Sys_User // dt_RptSv_Sys_User
					);

				if (!bTest)
				{
					//////
					//WSLDAP.ldap wsLDAP = new WSLDAP.ldap();
					//wsLDAP.Url = _cf.nvcParams["WSLDAP_Url"];

					//string strUserNick = Convert.ToString(dt_RptSv_Sys_User.Rows[0]["UserNick"]);

					//////
					//string strResult = wsLDAP.ldapLogin(
					//	strUserNick // username
					//	, strUserPassword // password
					//	);

					//if (!string.IsNullOrEmpty(strResult))
					//{
					//	alParamsCoupleError.AddRange(new object[]{
					//		"Check.strUserNick", strUserNick
					//		, "Check.LDAP.Url", wsLDAP.Url
					//		, "Check.LDAP.UserName", strUserNick
					//		, "Check.LDAP.ErrorMsg", strResult
					//		});
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.RptSv_Sys_User_Login_InvalidLDAP
					//		, null
					//		, alParamsCoupleError.ToArray()
					//		);
					//}
				}
				else
				{
					// CheckPassword:
					string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPassword);
					if (!CmUtils.StringUtils.StringEqual(strUserPwCheck, dt_RptSv_Sys_User.Rows[0]["UserPassword"]))
					{
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Login_InvalidPassword // strErrorCode
							, null // excInner
							, alParamsCoupleError.ToArray() // arrobjParamsCouple
							);
					}
				}

				////

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_RptSv_Sys_User.Rows[0]["UserCode"]);
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_Sys_User_Login(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, string strUserCode
			, string strUserPassword
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = false;
			//string strErrorCode = null;
			string strFunctionName = "RptSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Login;
			strUserCode = TUtils.CUtils.StdParam(strUserCode);
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "strTid", strTid
				////
				, "strUserCode", strUserCode
				});

			// Manual SessionInfo:
			//DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
			//drSessionInfo["RootSvCode"] = strRootSvCode;
			//drSessionInfo["RootUserCode"] = strRootUserCode;
			//drSessionInfo["ServiceCode"] = strServiceCode;
			//drSessionInfo["UserCode"] = strUserCode;
			//drSessionInfo["LanguageCode"] = strLanguageCode;
			//drSessionInfo["InfoExternal"] = strOtherInfo;
			//_cf.sinf = new CSessionInfo(drSessionInfo);
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:
				//string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
				bool bTest = true;
				#endregion

				#region // Process:
				// RptSv_Sys_User_CheckDB:
				DataTable dt_RptSv_Sys_User = null;
				RptSv_Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strUserCode // strUserCode
					, TConst.Flag.Active // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dt_RptSv_Sys_User // dt_RptSv_Sys_User
					);

				if (!bTest)
				{
					//////
					//WSLDAP.ldap wsLDAP = new WSLDAP.ldap();
					//wsLDAP.Url = _cf.nvcParams["WSLDAP_Url"];

					//string strUserNick = Convert.ToString(dt_RptSv_Sys_User.Rows[0]["UserNick"]);

					//////
					//string strResult = wsLDAP.ldapLogin(
					//	strUserNick // username
					//	, strUserPassword // password
					//	);

					//if (!string.IsNullOrEmpty(strResult))
					//{
					//	alParamsCoupleError.AddRange(new object[]{
					//		"Check.strUserNick", strUserNick
					//		, "Check.LDAP.Url", wsLDAP.Url
					//		, "Check.LDAP.UserName", strUserNick
					//		, "Check.LDAP.ErrorMsg", strResult
					//		});
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.RptSv_Sys_User_Login_InvalidLDAP
					//		, null
					//		, alParamsCoupleError.ToArray()
					//		);
					//}
				}
				else
				{
					// CheckPassword:
					//string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPassword);
					if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strWAUserPassword), dt_RptSv_Sys_User.Rows[0]["UserPassword"]))
					{
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Login_InvalidPassword // strErrorCode
							, null // excInner
							, alParamsCoupleError.ToArray() // arrobjParamsCouple
							);
					}
				}

				// Assign:
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_RptSv_Sys_User.Rows[0]["UserCode"]);
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

		public DataSet WAS_RptSv_Sys_User_Login(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_Login;
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

				#region // WS_RptSv_Sys_User_Get:
				mdsResult = RptSv_Sys_User_Login(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
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
		public DataSet RptSv_Sys_User_Logout(
			string strTid
			, DataRow drSession
			////
			, object strSessionId
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = false;
			string strFunctionName = "RptSv_Sys_User_Logout";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Logout;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "strTid", strTid
				////
                , "strSessionId", strSessionId
				});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:

				#endregion

				#region // Logout:
				_cf.sess.Remove(false, strSessionId);
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet RptSv_Sys_User_GetForCurrentUser(
			string strTid
			, DataRow drSession
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_GetForCurrentUser";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_GetForCurrentUser;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				alParamsCoupleSql.AddRange(new object[] {
					"@strUserCode", _cf.sinf.strUserCode
					});
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_User:
						select
							su.UserCode
							--, su.DBCode
							--, su.AreaCode
							, su.UserName
							--, su.MST
							--, su.FlagDLAdmin
							, su.FlagSysAdmin
							--, su.FlagDBAdmin
							, su.FlagActive
							, md.DLBUCode md_DLBUCode
							, md.DLBUPattern md_DLBUPattern
							--, md.DBCode md_DBCode 
							--, md.DBCodeParent md_DBCodeParent 
							--, md.DBName md_DBName 
							--, md.DBLevel md_DBLevel 
							--, md.DBStatus md_DBStatus 
							--, mam.AreaCode mam_AreaCode
							--, mam.AreaCodeParent mam_AreaCodeParent
							--, mam.AreaDesc mam_AreaDesc
							--, mam.AreaLevel mam_AreaLevel
							--, mam.AreaStatus mam_AreaStatus
						into #tbl_RptSv_Sys_User
						from RptSv_Sys_User su --//[mylock]
							--left join Mst_Distributor md --//[mylock]
								--on su.DBCode = md.DBCode
							--left join Mst_AreaMarket mam --//[mylock] 
								--on md.AreaCode = mam.AreaCode
							left join Mst_Dealer md --//[mylock]
								on su.DLCode = md.DLCode
						where
							su.UserCode = @strUserCode
						;
						select * from #tbl_RptSv_Sys_User t --//[mylock]
						;
					"
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				dsGetData.Tables[0].TableName = "RptSv_Sys_User";
				//dsGetData.Tables[1].TableName = "Sys_Access";
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_Sys_User_GetForCurrentUser(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_GetForCurrentUser";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_GetForCurrentUser;
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//RptSv_Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				alParamsCoupleSql.AddRange(new object[] {
					"@strUserCode", strWAUserCode
					});
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_User:
						select
							rssu.UserCode
							, rssu.NetworkID
							--, rssu.DBCode
                            --, rssu.AreaCode
							, rssu.UserName
							--, rssu.MST
							, rssu.DLCode
							--, rssu.FlagDLAdmin
							, rssu.FlagSysAdmin
							--, rssu.FlagDBAdmin
							, rssu.FlagActive
							, md.DLCode md_DLCode
							, md.DLBUCode md_DLBUCode
							, md.DLBUPattern md_DLBUPattern
							--, mdept.DLCode mdept_DLCode
							--, mdept.DepartmentName mdept_DepartmentName
						into #tbl_RptSv_Sys_User
						from RptSv_Sys_User rssu --//[mylock]
							left join Mst_Dealer md --//[mylock]
								on rssu.DLCode = md.DLCode
						where
							rssu.UserCode = @strUserCode
						;
						select * from #tbl_RptSv_Sys_User t --//[mylock]
						;
					"
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				dsGetData.Tables[0].TableName = "RptSv_Sys_User";
				//dsGetData.Tables[1].TableName = "Sys_Access";
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

        public DataSet RptSv_Sys_User_GetForCurrentUser_New20191106(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_GetForCurrentUser;
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

                // RptSv_Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@strUserCode", strWAUserCode
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_User:
						select
							rssu.UserCode
							, rssu.NetworkID
							--, rssu.DBCode
                            --, rssu.AreaCode
							, rssu.UserName
							--, rssu.MST
							, rssu.DLCode
							--, rssu.FlagDLAdmin
							, rssu.FlagSysAdmin
							--, rssu.FlagDBAdmin
							, rssu.FlagActive
							, md.DLCode md_DLCode
							, md.DLBUCode md_DLBUCode
							, md.DLBUPattern md_DLBUPattern
							--, mdept.DLCode mdept_DLCode
							--, mdept.DepartmentName mdept_DepartmentName
						into #tbl_RptSv_Sys_User
						from RptSv_Sys_User rssu --//[mylock]
							left join Mst_Dealer md --//[mylock]
								on rssu.DLCode = md.DLCode
						where
							rssu.UserCode = @strUserCode
						;
						select * from #tbl_RptSv_Sys_User t --//[mylock]
						;

                        ---- RptSv_Sys_Access:
						select distinct
							sa.ObjectCode
						into #tbl_RptSv_Sys_Access
						from RptSv_Sys_User su --//[mylock]
							inner join RptSv_Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							inner join RptSv_Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode and sg.FlagActive = '1'
							inner join RptSv_Sys_Access sa --//[mylock]
								on sg.GroupCode = sa.GroupCode
							inner join RptSv_Sys_Object so --//[mylock]
								on sa.ObjectCode = so.ObjectCode and so.FlagActive = '1'
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from #tbl_RptSv_Sys_User f --//[mylock]
							inner join RptSv_Sys_Object so --//[mylock]
								on f.FlagSysAdmin = '1' and f.FlagActive = '1' and so.FlagActive = '1'
						;
						select 
							f.*
							, so.ObjectCode so_ObjectCode
                            , so.ObjectName so_ObjectName 
                            , so.ServiceCode so_ServiceCode 
                            , so.ObjectType so_ObjectType 
                            , so.FlagExecModal so_FlagExecModal 
                            , so.FlagActive so_FlagActive
						from #tbl_RptSv_Sys_Access f --//[mylock]
							inner join RptSv_Sys_Object so --//[mylock]
								on f.ObjectCode = so.ObjectCode
						;
					"
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                dsGetData.Tables[0].TableName = "RptSv_Sys_User";
                dsGetData.Tables[1].TableName = "RptSv_Sys_Access";
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

        public DataSet WAS_RptSv_Sys_User_GetForCurrentUser(
            ref ArrayList alParamsCoupleError
            , RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
            ////
            , out RT_RptSv_Sys_User objRT_RptSv_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_RptSv_Sys_User.Tid;
            objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_GetForCurrentUser;
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
                List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
                List<RptSv_Sys_Access> lst_RptSv_Sys_Access = new List<RptSv_Sys_Access>();
                #endregion

                #region // WS_RptSv_Sys_User_Get:
                mdsResult = RptSv_Sys_User_GetForCurrentUser_New20191106(
                    objRQ_RptSv_Sys_User.Tid // strTid
                    , objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_User.GwPassword // strGwPassword
                    , objRQ_RptSv_Sys_User.WAUserCode // strUserCode
                    , objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_RptSv_Sys_User = mdsResult.Tables["RptSv_Sys_User"].Copy();
                    lst_RptSv_Sys_User = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_User>(dt_RptSv_Sys_User);
                    objRT_RptSv_Sys_User.Lst_RptSv_Sys_User = lst_RptSv_Sys_User;

                    ////
                    DataTable dt_Sys_Access = mdsResult.Tables["RptSv_Sys_Access"].Copy();
                    lst_RptSv_Sys_Access = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_Access>(dt_Sys_Access);
                    objRT_RptSv_Sys_User.Lst_RptSv_Sys_Access = lst_RptSv_Sys_Access;
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
        public DataSet RptSv_Sys_User_ChangePassword(
			string strTid
			, DataRow drSession
			, string strUserPasswordOld
			, string strUserPasswordNew
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_ChangePassword";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_ChangePassword;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:

				// RptSv_Sys_User_CheckDB:
				DataTable dt_RptSv_Sys_User = null;
				RptSv_Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, _cf.sinf.strUserCode // strUserCode
					, TConst.Flag.Active // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dt_RptSv_Sys_User
					);

				// CheckPassword:
				string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPasswordOld);
				if (!CmUtils.StringUtils.StringEqual(strUserPwCheck, dt_RptSv_Sys_User.Rows[0]["UserPassword"]))
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordOld // strErrorCode
						, null // excInner
						, alParamsCoupleError.ToArray() // arrobjParamsCouple
						);
				}
				////
				if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPasswordNew)))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserPasswordNew", strUserPasswordNew
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordNew
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				#endregion

				#region // dt_RptSv_Sys_User:
				ArrayList alColumnEffective = new ArrayList();
				dt_RptSv_Sys_User.Rows[0]["UserPassword"] = TUtils.CUtils.GetSimpleHash(strUserPasswordNew); alColumnEffective.Add("UserPassword");
				_cf.db.SaveData("RptSv_Sys_User", dt_RptSv_Sys_User, alColumnEffective.ToArray());
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_Sys_User_ChangePassword(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, string strUserPasswordOld
			, string strUserPasswordNew
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_ChangePassword";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_ChangePassword;
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

				//// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:

				// RptSv_Sys_User_CheckDB:
				DataTable dt_RptSv_Sys_User = null;
				RptSv_Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strWAUserCode // strUserCode
					, TConst.Flag.Active // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dt_RptSv_Sys_User
					);

				// CheckPassword:
				//string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPasswordOld);
				if (!CmUtils.StringUtils.StringEqual(strWAUserPassword, dt_RptSv_Sys_User.Rows[0]["UserPassword"]))
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordOld // strErrorCode
						, null // excInner
						, alParamsCoupleError.ToArray() // arrobjParamsCouple
						);
				}
				////
				//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPasswordNew)))
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.strUserPasswordNew", strUserPasswordNew
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordNew
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				#endregion

				#region // dt_RptSv_Sys_User:
				ArrayList alColumnEffective = new ArrayList();
				dt_RptSv_Sys_User.Rows[0]["UserPassword"] = strUserPasswordNew; alColumnEffective.Add("UserPassword");
				_cf.db.SaveData("RptSv_Sys_User", dt_RptSv_Sys_User, alColumnEffective.ToArray());
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

		public DataSet WAS_RptSv_Sys_User_ChangePassword(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_ChangePassword";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_ChangePassword;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				//List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
				#endregion

				#region // RptSv_Sys_User_Create:
				mdsResult = RptSv_Sys_User_ChangePassword(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPasswordOld
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserPasswordNew // strUserPasswordNew
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

		public DataSet RptSv_Sys_User_Get(
			string strTid
			, DataRow drSession
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_RptSv_Sys_User
			, string strRt_Cols_RptSv_Sys_UserInGroup
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
					, "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
					});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strFunctionName
					);
				#endregion

				#region // Check:
				// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_RptSv_Sys_User = (strRt_Cols_RptSv_Sys_User != null && strRt_Cols_RptSv_Sys_User.Length > 0);
				bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				//alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@Today", DateTime.Today.ToString("yyyy-MM-dd")
					});
				////
				//myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

				//myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_RptSv_Sys_User_Filter_Draft
						from RptSv_Sys_User su --//[mylock]
							left join RptSv_Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_User_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_User_Filter
						from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_User --------:
						zzzzClauseSelect_RptSv_Sys_User_zOut
						----------------------------------------

						-------- RptSv_Sys_UserInGroup --------:
						zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_User_Filter_Draft;
						--drop table #tbl_RptSv_Sys_User_Filter;
					"
					, "zzzzClauseWhere_FilterAbilityOfUser", ""
					);
				////
				string zzzzClauseSelect_RptSv_Sys_User_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_User)
				{
					#region // bGet_RptSv_Sys_User:
					zzzzClauseSelect_RptSv_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode
								, su.UserNick
                                , su.BankCode                             
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin                       
								, su.FlagActive
								, mb.BankCode mb_BankCode
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Bank mb --//[mylock]
									on su.BankCode = mb.BankCode
							order by t.MyIdxSeq asc
							;
						"
						, "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
						);
					#endregion
				}
				////
				string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_UserInGroup)
				{
					#region // bGet_RptSv_Sys_UserInGroup:
					zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.BankCode su_BankCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join RptSv_Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzzzClauseWhere_strFilterWhereClause = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_User" // strTableNameDB
							, "RptSv_Sys_User." // strPrefixStd
							, "su." // strPrefixAlias
							);
						htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_UserInGroup" // strTableNameDB
							, "RptSv_Sys_UserInGroup." // strPrefixStd
							, "suig." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Sys_Group" // strTableNameDB
							, "Sys_Group." // strPrefixStd
							, "sg." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_Bank" // strTableNameDB
							, "Mst_Bank." // strPrefixStd
							, "mb." // strPrefixAlias
							);
						////
						#endregion
					}
					zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
					alParamsCoupleError.AddRange(new object[]{
						"zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
					, "zzzzClauseSelect_RptSv_Sys_User_zOut", zzzzClauseSelect_RptSv_Sys_User_zOut
					, "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_RptSv_Sys_User)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_User";
				}
				if (bGet_RptSv_Sys_UserInGroup)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_Sys_User_Get(
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
			, string strRt_Cols_RptSv_Sys_User
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Check:
				// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_RptSv_Sys_User = (strRt_Cols_RptSv_Sys_User != null && strRt_Cols_RptSv_Sys_User.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				//alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@Today", DateTime.Today.ToString("yyyy-MM-dd")
					});
				////
				//myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

				//myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, rssu.UserCode
						into #tbl_RptSv_Sys_User_Filter_Draft
						from RptSv_Sys_User rssu --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by rssu.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_User_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_User_Filter
						from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_User --------:
						zzzzClauseSelect_RptSv_Sys_User_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_User_Filter_Draft;
						--drop table #tbl_RptSv_Sys_User_Filter;
					"
					, "zzzzClauseWhere_FilterAbilityOfUser", ""
					);
				////
				string zzzzClauseSelect_RptSv_Sys_User_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_User)
				{
					#region // bGet_RptSv_Sys_User:
					zzzzClauseSelect_RptSv_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_User:
							select
								t.MyIdxSeq
								, rssu.UserCode
								, rssu.DLCode
								, rssu.UserName                      
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
								, rssu.PhoneNo
								, rssu.UserID
								, rssu.FlagSysAdmin                         
								, rssu.FlagActive
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_User rssu --//[mylock]
									on t.UserCode = rssu.UserCode
							order by t.MyIdxSeq asc
							;
						"
						, "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
						);
					#endregion
				}
				////
				////
				string zzzzClauseWhere_strFilterWhereClause = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_User" // strTableNameDB
							, "RptSv_Sys_User." // strPrefixStd
							, "rssu." // strPrefixAlias
							);
						htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
						////
						#endregion
					}
					zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
					alParamsCoupleError.AddRange(new object[]{
						"zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
					, "zzzzClauseSelect_RptSv_Sys_User_zOut", zzzzClauseSelect_RptSv_Sys_User_zOut
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_RptSv_Sys_User)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_User";
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

		public DataSet WAS_RptSv_Sys_User_Get(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_Get;
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
				List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				//List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
				bool bGet_RptSv_Sys_User = (objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User != null && objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User.Length > 0);
				#endregion

				#region // WS_RptSv_Sys_User_Get:
				mdsResult = RptSv_Sys_User_Get(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_RptSv_Sys_User.Ft_RecordStart // strFt_RecordStart
					, objRQ_RptSv_Sys_User.Ft_RecordCount // strFt_RecordCount
					, objRQ_RptSv_Sys_User.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User // strRt_Cols_RptSv_Sys_User
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_RptSv_Sys_User.MySummaryTable = lst_MySummaryTable[0];

					////
					if (bGet_RptSv_Sys_User)
					{
						////
						DataTable dt_RptSv_Sys_User = mdsResult.Tables["RptSv_Sys_User"].Copy();
						lst_RptSv_Sys_User = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_User>(dt_RptSv_Sys_User);
						objRT_RptSv_Sys_User.Lst_RptSv_Sys_User = lst_RptSv_Sys_User;
					}
					//////
					//if (bGet_RptSv_Sys_UserInGroup)
					//{
					//	DataTable dt_RptSv_Sys_UserInGroup = mdsResult.Tables["RptSv_Sys_UserInGroup"].Copy();
					//	lst_RptSv_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_UserInGroup>(dt_RptSv_Sys_UserInGroup);
					//	objRT_RptSv_Sys_User.Lst_RptSv_Sys_UserInGroup = lst_RptSv_Sys_UserInGroup;
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

		public DataSet RptSv_Sys_User_Get_01(
			string strTid
			, DataRow drSession
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_RptSv_Sys_User
			, string strRt_Cols_RptSv_Sys_UserInGroup
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Get_01";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Get_01;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
					, "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
					});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strFunctionName
					);
				#endregion

				#region // Check:
				// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_RptSv_Sys_User = (strRt_Cols_RptSv_Sys_User != null && strRt_Cols_RptSv_Sys_User.Length > 0);
				bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				//alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@Today", DateTime.Today.ToString("yyyy-MM-dd")
					});
				////
				//myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_RptSv_Sys_User_Filter_Draft
						from RptSv_Sys_User su --//[mylock]
							left join RptSv_Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
                            left join Mst_Distributor md --//[mylock]
								on su.DBCode = md.DBCode
                            inner join #tbl_Mst_Distributor_ViewAbility va_md --//[mylock]
								on md.DBCode = va_md.DBCode
							left join Mst_AreaMarket mam --//[mylock]
								on md.AreaCode = mam.AreaCode 
							left join Aud_CampaignOLDtl acoldt --//[mylock] 
								on su.UserCode = acoldt.AuditUserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_User_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_User_Filter
						from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_User --------:
						zzzzClauseSelect_RptSv_Sys_User_zOut
						----------------------------------------

						-------- RptSv_Sys_UserInGroup --------:
						zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_User_Filter_Draft;
						--drop table #tbl_RptSv_Sys_User_Filter;
					"
					, "zzzzClauseWhere_FilterAbilityOfUser", ""
					);
				////
				string zzzzClauseSelect_RptSv_Sys_User_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_User)
				{
					#region // bGet_RptSv_Sys_User:
					zzzzClauseSelect_RptSv_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_User:
							select distinct
								t.MyIdxSeq
								, su.UserCode
                                , su.DBCode
                                , su.AreaCode
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin
                                , su.FlagDBAdmin
								, su.FlagActive
								, md.DBCode md_DBCode
								, mam.AreaCode mam_AreaCode
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								inner join Aud_CampaignOLDtl acoldt --//[mylock] 
									on su.UserCode = acoldt.AuditUserCode
								left join Mst_Distributor md --//[mylock]
									on su.DBCode = md.DBCode
								left join Mst_AreaMarket mam --//[mylock]
									on su.AreaCode = mam.AreaCode
							order by t.MyIdxSeq asc
							;
						"
						, "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
						);
					#endregion
				}
				////
				string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_UserInGroup)
				{
					#region // bGet_RptSv_Sys_UserInGroup:
					zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName
                                , su.FlagDLAdmin su_FlagDLAdmin 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join RptSv_Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzzzClauseWhere_strFilterWhereClause = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_User" // strTableNameDB
							, "RptSv_Sys_User." // strPrefixStd
							, "su." // strPrefixAlias
							);
						htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_UserInGroup" // strTableNameDB
							, "RptSv_Sys_UserInGroup." // strPrefixStd
							, "suig." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Sys_Group" // strTableNameDB
							, "Sys_Group." // strPrefixStd
							, "sg." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_Distributor" // strTableNameDB
							, "Mst_Distributor." // strPrefixStd
							, "md." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_AreaMarket" // strTableNameDB
							, "Mst_AreaMarket." // strPrefixStd
							, "mam." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Aud_CampaignOLDtl" // strTableNameDB
							, "Aud_CampaignOLDtl." // strPrefixStd
							, "acoldt." // strPrefixAlias
							);
						////
						#endregion
					}
					zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
					alParamsCoupleError.AddRange(new object[]{
						"zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
					, "zzzzClauseSelect_RptSv_Sys_User_zOut", zzzzClauseSelect_RptSv_Sys_User_zOut
					, "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_RptSv_Sys_User)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_User";
				}
				if (bGet_RptSv_Sys_UserInGroup)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		public DataSet RptSv_Sys_User_Create_Old(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objUserName
			, object objUserPassword
			, object objPhoneNo
			, object objEMail
			, object objMST
			, object objDLCode
			, object objUserID
			, object objFlagDLAdmin
			, object objFlagSysAdmin
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Create";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				, "objUserName", objUserName
				, "objPhoneNo", objPhoneNo
				, "objEMail", objEMail
				, "objMST", objMST
				, "objDLCode", objDLCode
				, "objUserID", objUserID
				, "objFlagDLAdmin", objFlagDLAdmin
				, "objFlagSysAdmin", objFlagSysAdmin
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserName = string.Format("{0}", objUserName).Trim();
				string strUserPassword = string.Format("{0}", objUserPassword);
				string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
				string strEMail = string.Format("{0}", objEMail).Trim();
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strUserID = string.Format("{0}", objUserID);
				string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
				string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					if (strUserCode == null || strUserCode.Length <= 0)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserCode", strUserCode
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					////
					if (strUserName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserName", strUserName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strUserPassword.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserPassword", strUserPassword
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					//// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
					//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
					//{
					//	alParamsCoupleError.AddRange(new object[]{
					//		"Check.strUserPassword", strUserPassword
					//		});
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
					//		, null
					//		, alParamsCoupleError.ToArray()
					//		);
					//}
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
					DataTable dtDB_Mst_Department = null;

					Mst_Department_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , strDLCode // strDLCode 
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , TConst.Flag.Active // strFlagActiveListToCheck
						 , out dtDB_Mst_Department // dtDB_Mst_Organ
						);
					////
					if (strFlagDLAdmin.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strFlagDLAdmin", strFlagDLAdmin
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidFlagDLAdmin
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strFlagSysAdmin.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strFlagSysAdmin", strFlagSysAdmin
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidFlagSysAdmin
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
				}
				#endregion

				#region // SaveDB:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_RptSv_Sys_User.NewRow();
					strFN = "UserCode"; drDB[strFN] = strUserCode;
					strFN = "NetworkID"; drDB[strFN] = nNetworkID;
					strFN = "UserName"; drDB[strFN] = strUserName;
					strFN = "UserPassword"; drDB[strFN] = strUserPassword;
					strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
					strFN = "EMail"; drDB[strFN] = strEMail;
					strFN = "MST"; drDB[strFN] = strMST;
					strFN = "DLCode"; drDB[strFN] = strDLCode;
					strFN = "UserID"; drDB[strFN] = strUserID;
					strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
					strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_RptSv_Sys_User.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
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

		public DataSet RptSv_Sys_User_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objDLCode
			, object objUserName
			, object objUserPassword
			, object objPhoneNo
			, object objUserID
			, object objFlagSysAdmin
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Create";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				, "objDLCode", objDLCode
				, "objUserName", objUserName
				, "objUserPassword", objUserPassword
				, "objPhoneNo", objPhoneNo
				, "objUserID", objUserID
				, "objFlagSysAdmin", objFlagSysAdmin
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // RptSv_Sys_User_CreateX:
				{
					//RptSv_Sys_User_CreateX(
					//	strTid // strTid
					//	, strGwUserCode // strGwUserCode
					//	, strGwPassword // strGwPassword
					//	, strWAUserCode // strWAUserCode
					//	, strWAUserPassword // strWAUserPassword
					//	, ref alParamsCoupleError // alParamsCoupleError
					//	, dtimeSys // dtimeSys
					//			   ////
					//	, objUserCode // objUserCode
					//	, objDLCode // objDLCode
					//	, objUserName // objUserName
					//	, objUserPassword // objUserPassword
					//	, objPhoneNo // objPhoneNo
					//	, objUserID // objUserID
					//	, objFlagSysAdmin // objFlagSysAdmin
					//					  ////
					//	);
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

		//private void RptSv_Sys_User_CreateX(
		//	string strTid
		//	, string strGwUserCode
		//	, string strGwPassword
		//	, string strWAUserCode
		//	, string strWAUserPassword
		//	, ref ArrayList alParamsCoupleError
		//	, DateTime dtimeSys
		//	//// 
		//	, object objUserCode
		//	, object objDLCode
		//	, object objUserName
		//	, object objUserPassword
		//	, object objPhoneNo
		//	, object objUserID
		//	, object objFlagSysAdmin
		//	////
		//	)
		//{
		//	#region // Temp:
		//	string strFunctionName = "RptSv_Sys_User_CreateX";
		//	//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
		//	alParamsCoupleError.AddRange(new object[]{
		//		"strFunctionName", strFunctionName
		//		, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
		//		////
		//		, "objUserCode", objUserCode
		//		, "objDLCode", objDLCode
		//		, "objUserName", objUserName
		//		, "objUserPassword", objUserPassword
		//		, "objPhoneNo", objPhoneNo
		//		, "objUserID", objUserID
		//		, "objFlagSysAdmin", objFlagSysAdmin
		//		});
		//	#endregion

		//	#region // Refine and Check Input:
		//	////
		//	string strUserCode = TUtils.CUtils.StdParam(objUserCode);
		//	string strUserName = string.Format("{0}", objUserName).Trim();
		//	string strUserPassword = string.Format("{0}", objUserPassword);
		//	string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
		//	string strDLCode = TUtils.CUtils.StdParam(objDLCode);
		//	string strUserID = string.Format("{0}", objUserID);
		//	string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
		//	////
		//	DataTable dtDB_RptSv_Sys_User = null;
		//	{
		//		////
		//		if (strUserCode == null || strUserCode.Length <= 0)
		//		{
		//			alParamsCoupleError.AddRange(new object[]{
		//				"Check.strUserCode", strUserCode
		//				});
		//			throw CmUtils.CMyException.Raise(
		//				TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserCode
		//				, null
		//				, alParamsCoupleError.ToArray()
		//				);
		//		}
		//		RptSv_Sys_User_CheckDB(
		//			ref alParamsCoupleError // alParamsCoupleError
		//			, objUserCode // objUserCode
		//			, TConst.Flag.No // strFlagExistToCheck
		//			, "" // strFlagActiveListToCheck
		//			, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
		//			);
		//		////
		//		if (strUserName.Length < 1)
		//		{
		//			alParamsCoupleError.AddRange(new object[]{
		//				"Check.strUserName", strUserName
		//				});
		//			throw CmUtils.CMyException.Raise(
		//				TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserName
		//				, null
		//				, alParamsCoupleError.ToArray()
		//				);
		//		}
		//		////
		//		if (strUserPassword.Length < 1)
		//		{
		//			alParamsCoupleError.AddRange(new object[]{
		//				"Check.strUserPassword", strUserPassword
		//				});
		//			throw CmUtils.CMyException.Raise(
		//				TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
		//				, null
		//				, alParamsCoupleError.ToArray()
		//				);
		//		}
		//		//// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
		//		//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
		//		//{
		//		//	alParamsCoupleError.AddRange(new object[]{
		//		//		"Check.strUserPassword", strUserPassword
		//		//		});
		//		//	throw CmUtils.CMyException.Raise(
		//		//		TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
		//		//		, null
		//		//		, alParamsCoupleError.ToArray()
		//		//		);
		//		//}
		//		////
		//		DataTable dtDB_Mst_Dealer = null;

		//		Mst_Dealer_CheckDB(
		//			 ref alParamsCoupleError // alParamsCoupleError
		//			 , strDLCode // objDLCode 
		//			 , TConst.Flag.Yes // strFlagExistToCheck
		//			 , TConst.Flag.Active // strFlagActiveListToCheck
		//			 , out dtDB_Mst_Dealer // dtDB_Mst_Dealer
		//			);
		//		////
		//		if (strFlagSysAdmin.Length < 1)
		//		{
		//			alParamsCoupleError.AddRange(new object[]{
		//				"Check.strFlagSysAdmin", strFlagSysAdmin
		//				});
		//			throw CmUtils.CMyException.Raise(
		//				TError.ErridnInventory.RptSv_Sys_User_Create_InvalidFlagSysAdmin
		//				, null
		//				, alParamsCoupleError.ToArray()
		//				);
		//		}
		//		////
		//	}
		//	#endregion

		//	#region // SaveDB:
		//	{
		//		// Init:
		//		//ArrayList alColumnEffective = new ArrayList();
		//		string strFN = "";
		//		DataRow drDB = dtDB_RptSv_Sys_User.NewRow();
		//		strFN = "UserCode"; drDB[strFN] = strUserCode;
		//		strFN = "NetworkID"; drDB[strFN] = nNetworkID;
		//		strFN = "DLCode"; drDB[strFN] = strDLCode;
		//		strFN = "UserName"; drDB[strFN] = strUserName;
		//		strFN = "UserPassword"; drDB[strFN] = strUserPassword;
		//		strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
		//		strFN = "UserID"; drDB[strFN] = strUserID;
		//		strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
		//		strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
		//		strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
		//		strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
		//		dtDB_RptSv_Sys_User.Rows.Add(drDB);

		//		// Save:
		//		_cf.db.SaveData(
		//			"RptSv_Sys_User"
		//			, dtDB_RptSv_Sys_User
		//			//, alColumnEffective.ToArray()
		//			);
		//	}
		//	#endregion
		//}
		public DataSet WAS_RptSv_Sys_User_Create(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				//List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
				#endregion

				#region // RptSv_Sys_User_Create:
				mdsResult = RptSv_Sys_User_Create(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserCode // objUserCode
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.DLCode // objDLCode
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserName // objUserName
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserPassword // objUserPassword
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.PhoneNo //object objPhoneNo
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserID //object objUserID
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.FlagSysAdmin // objFlagSysAdmin
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
		public DataSet RptSv_Sys_User_Create(
			string strTid
			, DataRow drSession
			////
			, object objUserCode
			, object objUserNick
			, object objBankCode
			, object objUserName
			, object objUserPassword
			, object objFlagDLAdmin
			, object objFlagSysAdmin
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Create";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Create;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objUserCode", objUserCode
					, "objUserNick", objUserNick
					, "objBankCode", objBankCode
					, "objUserName", objUserName
					//, "objUserPassword", objUserPassword
                    , "objFlagDLAdmin", objFlagDLAdmin
					, "objFlagSysAdmin", objFlagSysAdmin
					});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserNick = Convert.ToString(objUserNick);
				string strBankCode = TUtils.CUtils.StdParam(objBankCode);
				string strUserName = string.Format("{0}", objUserName).Trim();
				string strUserPassword = string.Format("{0}", objUserPassword);
				string strFlagDLAdmin = TUtils.CUtils.StdParam(objFlagDLAdmin);
				string strFlagSysAdmin = TUtils.CUtils.StdParam(objFlagSysAdmin);
				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					if (strUserCode == null || strUserCode.Length <= 0)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserCode", strUserCode
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserCode
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					////
					if (string.IsNullOrEmpty(strUserNick))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserNick", strUserNick
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserNick
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strUserName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserName", strUserName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strUserPassword.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserPassword", strUserPassword
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					//// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
					//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
					//{
					//	alParamsCoupleError.AddRange(new object[]{
					//		"Check.strUserPassword", strUserPassword
					//		});
					//	throw CmUtils.CMyException.Raise(
					//		TError.ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword
					//		, null
					//		, alParamsCoupleError.ToArray()
					//		);
					//}
					////
					if (strFlagDLAdmin.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strFlagDLAdmin", strFlagDLAdmin
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidFlagDLAdmin
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (strFlagSysAdmin.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strFlagSysAdmin", strFlagSysAdmin
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Create_InvalidFlagSysAdmin
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
				}
				#endregion

				#region // SaveDB UserCode:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_RptSv_Sys_User.NewRow();
					strFN = "UserCode"; drDB[strFN] = strUserCode;
					strFN = "UserNick"; drDB[strFN] = strUserNick;
					strFN = "BankCode"; drDB[strFN] = strBankCode;
					strFN = "UserName"; drDB[strFN] = strUserName;
					strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetSimpleHash(strUserPassword);
					strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
					strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode;
					dtDB_RptSv_Sys_User.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet RptSv_Sys_User_Update(
			string strTid
			, DataRow drSession
			////
			, object objUserCode
			, object objUserNick
			, object objBankCode
			, object objUserName
			, object objUserPassword
			, object objFlagDLAdmin
			, object objFlagSysAdmin
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Update";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Update;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					////
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					, "objUserCode", objUserCode
					, "objBankCode", objBankCode
					, "objUserName", objUserName
					, "objUserPassword", objUserPassword
					, "objFlagDLAdmin", objFlagDLAdmin
					, "objFlagSysAdmin", objFlagSysAdmin
					, "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
					});
			#endregion

			try
			{
				#region // Convert Input:
				DateTime dtimeTDate = DateTime.UtcNow;
				#endregion

				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
				strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserNick = Convert.ToString(objUserNick);
				string strBankCode = TUtils.CUtils.StdParam(objBankCode);
				string strUserName = string.Format("{0}", objUserName).Trim();
				string strUserPassword = string.Format("{0}", objUserPassword);
				string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
				string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
				string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

				////
				//bool bUpd_UserNick = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserNick".ToUpper());
				bool bUpd_UserName = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserName".ToUpper());
				bool bUpd_UserPassword = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserPassword".ToUpper());
				bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagDLAdmin".ToUpper());
				bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagSysAdmin".ToUpper());
				bool bUpd_FlagActive = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagActive".ToUpper());

				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					////
					if (bUpd_UserName && strUserName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserName", strUserName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Update_InvalidUserName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (bUpd_UserPassword)
					{
						//// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
						//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
						//{
						//	alParamsCoupleError.AddRange(new object[]{
						//		"Check.strUserPassword", strUserPassword
						//		});
						//	throw CmUtils.CMyException.Raise(
						//		TError.ErridnInventory.RptSv_Sys_User_Update_InvalidUserPassword
						//		, null
						//		, alParamsCoupleError.ToArray()
						//		);
						//}
					}
					////
				}
				#endregion

				#region // SaveDB RptSv_Sys_User:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_RptSv_Sys_User.Rows[0];
					//if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
					if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
					if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetSimpleHash(strUserPassword); alColumnEffective.Add(strFN); }
					if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
					if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
					if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
						, alColumnEffective.ToArray()
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet RptSv_Sys_User_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objDLCode
			, object objUserName
			, object objUserPassword
			, object objPhoneNo
			//, object objEMail
			//, object objMST
			, object objUserID
			//, object objFlagDLAdmin
			, object objFlagSysAdmin
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Update";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				, "objDLCode", objDLCode
				, "objUserName", objUserName
				, "objPhoneNo", objPhoneNo
				//, "objEMail", objEMail
				//, "objMST", objMST
				, "objUserID", objUserID
				//, "objFlagDLAdmin", objFlagDLAdmin
				, "objFlagSysAdmin", objFlagSysAdmin
				, "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
				strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				string strUserName = string.Format("{0}", objUserName).Trim();
				string strUserPassword = string.Format("{0}", objUserPassword);
				string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
				//string strEMail = string.Format("{0}", objEMail).Trim();
				//string strMST = TUtils.CUtils.StdParam(objMST);
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strUserID = string.Format("{0}", objUserID);
				//string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
				string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
				string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

				////
				//bool bUpd_UserNick = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserNick".ToUpper());
				bool bUpd_UserName = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserName".ToUpper());
				bool bUpd_UserPassword = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserPassword".ToUpper());
				bool bUpd_PhoneNo = strFt_Cols_Upd.Contains("RptSv_Sys_User.PhoneNo".ToUpper());
				//bool bUpd_EMail = strFt_Cols_Upd.Contains("RptSv_Sys_User.EMail".ToUpper());
				bool bUpd_UserID = strFt_Cols_Upd.Contains("RptSv_Sys_User.UserID".ToUpper());
				//bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagDLAdmin".ToUpper());
				bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagSysAdmin".ToUpper());
				bool bUpd_FlagActive = strFt_Cols_Upd.Contains("RptSv_Sys_User.FlagActive".ToUpper());

				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					////
					if (bUpd_UserName && strUserName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strUserName", strUserName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.RptSv_Sys_User_Update_InvalidUserName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					if (bUpd_UserPassword)
					{
						//// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
						//if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
						//{
						//	alParamsCoupleError.AddRange(new object[]{
						//		"Check.strUserPassword", strUserPassword
						//		});
						//	throw CmUtils.CMyException.Raise(
						//		TError.ErridnInventory.RptSv_Sys_User_Update_InvalidUserPassword
						//		, null
						//		, alParamsCoupleError.ToArray()
						//		);
						//}
					}
					////
				}
				#endregion

				#region // SaveDB RptSv_Sys_User:
				{
					// Init:
					ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_RptSv_Sys_User.Rows[0];
					//if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
					if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
					if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = strUserPassword; alColumnEffective.Add(strFN); }
					if (bUpd_PhoneNo) { strFN = "PhoneNo"; drDB[strFN] = strPhoneNo; alColumnEffective.Add(strFN); }
					//if (bUpd_EMail) { strFN = "EMail"; drDB[strFN] = strEMail; alColumnEffective.Add(strFN); }
					if (bUpd_UserID) { strFN = "UserID"; drDB[strFN] = strUserID; alColumnEffective.Add(strFN); }
					//if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
					if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
					if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
						, alColumnEffective.ToArray()
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

		public DataSet WAS_RptSv_Sys_User_Update(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_Update;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				//List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
				#endregion

				#region // RptSv_Sys_User_Create:
				mdsResult = RptSv_Sys_User_Update(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserCode // objUserCode
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.DLCode //object objDLCode
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserName // objUserName
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserPassword // objUserPassword
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.PhoneNo //object objPhoneNo
																  //, objRQ_RptSv_Sys_User.RptSv_Sys_User.EMail //object objEMail
																  //, objRQ_RptSv_Sys_User.RptSv_Sys_User.MST //object objMST
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserID //object objUserID
																 //, objRQ_RptSv_Sys_User.RptSv_Sys_User.FlagDLAdmin // objFlagDLAdmin
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.FlagSysAdmin // objFlagSysAdmin
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.FlagActive // objFlagActive
																	 ////
					, objRQ_RptSv_Sys_User.Ft_Cols_Upd// objFt_Cols_Upd
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

		public DataSet RptSv_Sys_User_Delete(
			string strTid
			, DataRow drSession
			////
			, object objUserCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Delete;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "objUserCode", objUserCode
					});
			#endregion

			try
			{
				#region // Convert Input:
				DateTime dtimeTDate = DateTime.UtcNow;
				#endregion

				#region // Init:
				_cf.db.LogUserId = _cf.sinf.strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq(
					strTid // strTid
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					//// Delete RptSv_Sys_UserInGroup:
					//RptSv_Sys_UserInGroup_Delete_ByUser(
					//	strUserCode // strUserCode
					//	);
					////
				}
				#endregion

				#region // SaveDB UserCode:
				{
					// Init:
					dtDB_RptSv_Sys_User.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
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
				_cf.ProcessBizReturn(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet RptSv_Sys_User_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Sys_User_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				});
			#endregion

			try
			{
				#region // Convert Input:
				DateTime dtimeTDate = DateTime.UtcNow;
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				////
				DataTable dtDB_RptSv_Sys_User = null;
				{
					////
					RptSv_Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_RptSv_Sys_User // dtDB_RptSv_Sys_User
						);
					//// Delete RptSv_Sys_UserInGroup:
					//RptSv_Sys_UserInGroup_Delete_ByUser(
					//	strUserCode // strUserCode
					//	);
					////
				}
				#endregion

				#region // SaveDB UserCode:
				{
					// Init:
					dtDB_RptSv_Sys_User.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"RptSv_Sys_User"
						, dtDB_RptSv_Sys_User
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
		public DataSet WAS_RptSv_Sys_User_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Sys_User_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_User_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				//List<RptSv_Sys_UserInGroup> lst_RptSv_Sys_UserInGroup = new List<RptSv_Sys_UserInGroup>();
				#endregion

				#region // RptSv_Sys_User_Create:
				mdsResult = RptSv_Sys_User_Delete(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_RptSv_Sys_User.RptSv_Sys_User.UserCode // objUserCode
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

		public DataSet WebAPI_RptSv_Sys_User_GetX(
			string strTid
			//, DataRow drSession
			, string strUserCode
			, string strUserPassword
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_RptSv_Sys_User
			, string strRt_Cols_RptSv_Sys_UserInGroup
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
					, "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
					});
			#endregion

			try
			{
				#region // Init:
				_cf.db.LogUserId = strUserCode;
				if (bNeedTransaction) _cf.db.BeginTransaction();



				// Check Access/Deny:
				//Sys_Access_CheckDeny(
				//	ref alParamsCoupleError
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_RptSv_Sys_User = (strRt_Cols_RptSv_Sys_User != null && strRt_Cols_RptSv_Sys_User.Length > 0);
				bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

				//// drAbilityOfUser:
				//DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

				#endregion

				#region // Build Sql:
				ArrayList alParamsCoupleSql = new ArrayList();
				//alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@Today", DateTime.Today.ToString("yyyy-MM-dd")
					});
				////
				//myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

				//myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_RptSv_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_RptSv_Sys_User_Filter_Draft
						from RptSv_Sys_User su --//[mylock]
							left join RptSv_Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_RptSv_Sys_User_Filter:
						select
							t.*
						into #tbl_RptSv_Sys_User_Filter
						from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- RptSv_Sys_User --------:
						zzzzClauseSelect_RptSv_Sys_User_zOut
						----------------------------------------

						-------- RptSv_Sys_UserInGroup --------:
						zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_RptSv_Sys_User_Filter_Draft;
						--drop table #tbl_RptSv_Sys_User_Filter;
					"
					, "zzzzClauseWhere_FilterAbilityOfUser", ""
					);
				////
				string zzzzClauseSelect_RptSv_Sys_User_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_User)
				{
					#region // bGet_RptSv_Sys_User:
					zzzzClauseSelect_RptSv_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode
								, su.UserNick
                                , su.BankCode                             
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin                       
								, su.FlagActive
								, mb.BankCode mb_BankCode
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Bank mb --//[mylock]
									on su.BankCode = mb.BankCode
							order by t.MyIdxSeq asc
							;
						"
						, "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
						);
					#endregion
				}
				////
				string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
				if (bGet_RptSv_Sys_UserInGroup)
				{
					#region // bGet_RptSv_Sys_UserInGroup:
					zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- RptSv_Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.BankCode su_BankCode
								, su.UserName su_UserName
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_RptSv_Sys_User_Filter t --//[mylock]
								inner join RptSv_Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join RptSv_Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzzzClauseWhere_strFilterWhereClause = "";
				{
					Hashtable htSpCols = new Hashtable();
					{
						#region // htSpCols:
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_User" // strTableNameDB
							, "RptSv_Sys_User." // strPrefixStd
							, "su." // strPrefixAlias
							);
						htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "RptSv_Sys_UserInGroup" // strTableNameDB
							, "RptSv_Sys_UserInGroup." // strPrefixStd
							, "suig." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Sys_Group" // strTableNameDB
							, "Sys_Group." // strPrefixStd
							, "sg." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_Bank" // strTableNameDB
							, "Mst_Bank." // strPrefixStd
							, "mb." // strPrefixAlias
							);
						////
						#endregion
					}
					zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
						htSpCols // htSpCols
						, strFt_WhereClause // strClause
						, "@p_" // strParamPrefix
						, ref alParamsCoupleSql // alParamsCoupleSql
						);
					zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
					alParamsCoupleError.AddRange(new object[]{
						"zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
						});
				}
				////
				strSqlGetData = CmUtils.StringUtils.Replace(
					strSqlGetData
					, "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
					, "zzzzClauseSelect_RptSv_Sys_User_zOut", zzzzClauseSelect_RptSv_Sys_User_zOut
					, "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_RptSv_Sys_User)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_User";
				}
				if (bGet_RptSv_Sys_UserInGroup)
				{
					dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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

				//// Write ReturnLog:
				//_cf.ProcessBizReturn_OutSide(
				//	ref mdsFinal // mdsFinal
				//	, strTid // strTid
				//	, strGwUserCode // strGwUserCode
				//	, strGwPassword // strGwPassword
				//	, strUserCode // objUserCode
				//	, strFunctionName // strFunctionName
				//	);
				#endregion
			}
		}

		public DataSet WS_RptSv_Sys_User_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strUserCode
			, string strUserPassword
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_RptSv_Sys_User
			, string strRt_Cols_RptSv_Sys_UserInGroup
			)
		{
			#region // Temp:
			DataSet mdsFinal = null;
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_User_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			////// Filter
			//		, "strFt_RecordStart", strFt_RecordStart
			//		, "strFt_RecordCount", strFt_RecordCount
			//		, "strFt_WhereClause", strFt_WhereClause
			////// Return
			//		, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
			//		, "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
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
					, strUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strUserCode
					, strFunctionName
					);
				#endregion

				#region // WS_RptSv_Sys_User_GetX:
				WS_RptSv_Sys_User_GetX(
					strTid // strTid
					, dtimeSys // dtimeSys
					, ref alParamsCoupleError
					//// Filter:
					, strFt_RecordStart // strFt_RecordStart
					, strFt_RecordCount // strFt_RecordCount
					, strFt_WhereClause // strFt_WhereClause
										//// Return:
					, strRt_Cols_RptSv_Sys_User // strRt_Cols_RptSv_Sys_User
					, strRt_Cols_RptSv_Sys_UserInGroup // strRt_Cols_RptSv_Sys_UserInGroup
													   ////
					, out mdsFinal // mdsFinal
					);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
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
					, strUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		private void WS_RptSv_Sys_User_GetX(
			string strTid
			, DateTime dtimeSys
			, ref ArrayList alParamsCoupleError
			//// Filter:
			, string strFt_RecordStart
			, string strFt_RecordCount
			, string strFt_WhereClause
			//// Return:
			, string strRt_Cols_RptSv_Sys_User
			, string strRt_Cols_RptSv_Sys_UserInGroup
			////
			, out DataSet mdsFinal
			)
		{
			#region // Temp:
			mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			string strFunctionName = "WS_RptSv_Sys_User_GetX";
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_RptSv_Sys_User", strRt_Cols_RptSv_Sys_User
				, "strRt_Cols_RptSv_Sys_UserInGroup", strRt_Cols_RptSv_Sys_UserInGroup
				});
			#endregion

			#region // Init:
			//_cf.db.LogUserId = _cf.sinf.strUserCode;
			//if (bNeedTransaction) _cf.db.BeginTransaction();

			//// Write RequestLog:
			//_cf.ProcessBizReq(
			//	strTid // strTid
			//	, strFunctionName // strFunctionName
			//	, alParamsCoupleError // alParamsCoupleError
			//	);

			//// Check Access/Deny:
			//Sys_Access_CheckDeny(
			//	ref alParamsCoupleError
			//	, strFunctionName
			//	);
			#endregion

			#region // Check:
			// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_RptSv_Sys_User = (strRt_Cols_RptSv_Sys_User != null && strRt_Cols_RptSv_Sys_User.Length > 0);
			bool bGet_RptSv_Sys_UserInGroup = (strRt_Cols_RptSv_Sys_UserInGroup != null && strRt_Cols_RptSv_Sys_UserInGroup.Length > 0);

			//// drAbilityOfUser:
			//DataRow drAbilityOfUser = RptSv_Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

			#endregion

			#region // Build Sql:
			ArrayList alParamsCoupleSql = new ArrayList();
			//alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
			alParamsCoupleSql.AddRange(new object[] {
				"@nFilterRecordStart", nFilterRecordStart
				, "@nFilterRecordEnd", nFilterRecordEnd
				, "@Today", DateTime.Today.ToString("yyyy-MM-dd")
				});
			////
			//myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

			//myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_RptSv_Sys_User_Filter_Draft:
					--select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, su.UserCode
					into #tbl_RptSv_Sys_User_Filter_Draft
					from RptSv_Sys_User su --//[mylock]
						left join RptSv_Sys_UserInGroup suig --//[mylock]
							on su.UserCode = suig.UserCode
						left join Sys_Group sg --//[mylock]
							on suig.GroupCode = sg.GroupCode
						left join Mst_Bank mb --//[mylock]
							on su.BankCode = mb.BankCode
					where (1=1)
						zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
						zzzzClauseWhere_strFilterWhereClause
					order by su.UserCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
					;

					---- #tbl_RptSv_Sys_User_Filter:
					select
						t.*
					into #tbl_RptSv_Sys_User_Filter
					from #tbl_RptSv_Sys_User_Filter_Draft t --//[mylock]
					where
						(t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- RptSv_Sys_User --------:
					zzzzClauseSelect_RptSv_Sys_User_zOut
					----------------------------------------

					-------- RptSv_Sys_UserInGroup --------:
					zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
					----------------------------------------

					---- Clear for debug:
					--drop table #tbl_RptSv_Sys_User_Filter_Draft;
					--drop table #tbl_RptSv_Sys_User_Filter;
				"
				, "zzzzClauseWhere_FilterAbilityOfUser", ""
				);
			////
			string zzzzClauseSelect_RptSv_Sys_User_zOut = "-- Nothing.";
			if (bGet_RptSv_Sys_User)
			{
				#region // bGet_RptSv_Sys_User:
				zzzzClauseSelect_RptSv_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_User:
						select
							t.MyIdxSeq
							, su.UserCode
							, su.UserNick
							, su.BankCode                             
							, su.UserName
							, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                            , su.FlagDLAdmin
							, su.FlagSysAdmin                       
							, su.FlagActive
							, mb.BankCode mb_BankCode
						from #tbl_RptSv_Sys_User_Filter t --//[mylock]
							inner join RptSv_Sys_User su --//[mylock]
								on t.UserCode = su.UserCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						order by t.MyIdxSeq asc
						;
					"
					, "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
					);
				#endregion
			}
			////
			string zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = "-- Nothing.";
			if (bGet_RptSv_Sys_UserInGroup)
			{
				#region // bGet_RptSv_Sys_UserInGroup:
				zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
						---- RptSv_Sys_UserInGroup:
						select
							t.MyIdxSeq
							, suig.*
							, su.UserCode su_UserCode
							, su.BankCode su_BankCode
							, su.UserName su_UserName 
							, su.FlagSysAdmin su_FlagSysAdmin 
							, su.FlagActive su_FlagActive 
							, sg.GroupCode sg_GroupCode
							, sg.GroupName sg_GroupName 
							, sg.FlagActive sg_FlagActive 
						from #tbl_RptSv_Sys_User_Filter t --//[mylock]
							inner join RptSv_Sys_UserInGroup suig --//[mylock]
								on t.UserCode = suig.UserCode
							left join RptSv_Sys_User su --//[mylock]
								on suig.UserCode = su.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
						order by t.MyIdxSeq asc
						;
					"
					);
				#endregion
			}
			////
			string zzzzClauseWhere_strFilterWhereClause = "";
			{
				Hashtable htSpCols = new Hashtable();
				{
					#region // htSpCols:
					////
					TUtils.CUtils.MyBuildHTSupportedColumns(
						_cf.db // db
						, ref htSpCols // htSupportedColumns
						, "RptSv_Sys_User" // strTableNameDB
						, "RptSv_Sys_User." // strPrefixStd
						, "su." // strPrefixAlias
						);
					htSpCols.Remove("RptSv_Sys_User.UserPassword".ToUpper());
					////
					TUtils.CUtils.MyBuildHTSupportedColumns(
						_cf.db // db
						, ref htSpCols // htSupportedColumns
						, "RptSv_Sys_UserInGroup" // strTableNameDB
						, "RptSv_Sys_UserInGroup." // strPrefixStd
						, "suig." // strPrefixAlias
						);
					////
					TUtils.CUtils.MyBuildHTSupportedColumns(
						_cf.db // db
						, ref htSpCols // htSupportedColumns
						, "Sys_Group" // strTableNameDB
						, "Sys_Group." // strPrefixStd
						, "sg." // strPrefixAlias
						);
					////
					TUtils.CUtils.MyBuildHTSupportedColumns(
						_cf.db // db
						, ref htSpCols // htSupportedColumns
						, "Mst_Bank" // strTableNameDB
						, "Mst_Bank." // strPrefixStd
						, "mb." // strPrefixAlias
						);
					////
					#endregion
				}
				zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
					htSpCols // htSpCols
					, strFt_WhereClause // strClause
					, "@p_" // strParamPrefix
					, ref alParamsCoupleSql // alParamsCoupleSql
					);
				zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
				alParamsCoupleError.AddRange(new object[]{
					"zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
					});
			}
			////
			strSqlGetData = CmUtils.StringUtils.Replace(
				strSqlGetData
				, "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
				, "zzzzClauseSelect_RptSv_Sys_User_zOut", zzzzClauseSelect_RptSv_Sys_User_zOut
				, "zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut", zzzzClauseSelect_RptSv_Sys_UserInGroup_zOut
				);
			#endregion

			#region // Get Data:
			DataSet dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_RptSv_Sys_User)
			{
				dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_User";
			}
			if (bGet_RptSv_Sys_UserInGroup)
			{
				dsGetData.Tables[nIdxTable++].TableName = "RptSv_Sys_UserInGroup";
			}
			CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
			#endregion
		}

		public RT_RptSv_Sys_User WA_RptSv_Sys_User_Get(
			RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			RT_RptSv_Sys_User objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			objRT_RptSv_Sys_User.c_K_DT_Sys = new c_K_DT_Sys();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WA_RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WA_RptSv_Sys_User_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				objRT_RptSv_Sys_User.Lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				objRT_RptSv_Sys_User.c_K_DT_Sys = new c_K_DT_Sys();
				#endregion

				#region // WS_RptSv_Sys_User_Get:
				mdsExec = WS_RptSv_Sys_User_Get(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
													  ////
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserCode // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_RptSv_Sys_User.Ft_RecordStart // strFt_RecordStart
					, objRQ_RptSv_Sys_User.Ft_RecordCount // strFt_RecordCount
					, objRQ_RptSv_Sys_User.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User // strRt_Cols_RptSv_Sys_User
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_UserInGroup // strRt_Cols_RptSv_Sys_UserInGroup
					);
				////
				if (CmUtils.CMyDataSet.HasError(mdsExec))
				{
					////
					TUtils.CUtils.ProcessMyDSError(ref mdsResult, mdsExec);

					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.WA_RptSv_Sys_User_Get
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				#endregion

				#region // GetData:
				{
					////
					DataTable dt_RptSv_Sys_User = mdsExec.Tables["RptSv_Sys_User"].Copy();
					List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
					lst_RptSv_Sys_User = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_User>(dt_RptSv_Sys_User);
					objRT_RptSv_Sys_User.Lst_RptSv_Sys_User = lst_RptSv_Sys_User;
				}
				#endregion

				// Return Good:
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
				return objRT_RptSv_Sys_User;
			}
			catch (Exception exc)
			{
				////
				TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				// Return Bad:
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
				return objRT_RptSv_Sys_User;
			}
			finally
			{
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
			}
		}

		public DataSet WS_RptSv_Sys_User_Get(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WS_RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WA_RptSv_Sys_User_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				#endregion

				#region // WS_RptSv_Sys_User_Get:
				mdsResult = WS_RptSv_Sys_User_Get(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserCode // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_RptSv_Sys_User.Ft_RecordStart // strFt_RecordStart
					, objRQ_RptSv_Sys_User.Ft_RecordCount // strFt_RecordCount
					, objRQ_RptSv_Sys_User.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User // strRt_Cols_RptSv_Sys_User
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_UserInGroup // strRt_Cols_RptSv_Sys_UserInGroup
					);
				#endregion

				return mdsResult;
			}
			catch (Exception exc)
			{
				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
			}
			finally
			{
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
			}
		}

		public DataSet WS_Mix_RptSv_Sys_User_Get(
			ref ArrayList alParamsCoupleError
			, RQ_RptSv_Sys_User objRQ_RptSv_Sys_User
			////
			, out RT_RptSv_Sys_User objRT_RptSv_Sys_User
			)
		{
			#region // Temp:
			string strTid = objRQ_RptSv_Sys_User.Tid;
			objRT_RptSv_Sys_User = new RT_RptSv_Sys_User();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_User.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WS_RptSv_Sys_User_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WA_RptSv_Sys_User_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "RptSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_User.RptSv_Sys_User)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				#endregion

				#region // WS_RptSv_Sys_User_Get:
				mdsResult = WS_RptSv_Sys_User_Get(
					objRQ_RptSv_Sys_User.Tid // strTid
					, objRQ_RptSv_Sys_User.GwUserCode // strGwUserCode
					, objRQ_RptSv_Sys_User.GwPassword // strGwPassword
					, objRQ_RptSv_Sys_User.WAUserCode // strUserCode
					, objRQ_RptSv_Sys_User.WAUserCode // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_RptSv_Sys_User.Ft_RecordStart // strFt_RecordStart
					, objRQ_RptSv_Sys_User.Ft_RecordCount // strFt_RecordCount
					, objRQ_RptSv_Sys_User.Ft_WhereClause // strFt_WhereClause
														  //// Return:
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User // strRt_Cols_RptSv_Sys_User
					, objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_UserInGroup // strRt_Cols_RptSv_Sys_UserInGroup
					);
				#endregion

				#region // GetData:
				////
				DataTable dt_RptSv_Sys_User = mdsResult.Tables["RptSv_Sys_User"].Copy();
				List<RptSv_Sys_User> lst_RptSv_Sys_User = new List<RptSv_Sys_User>();
				lst_RptSv_Sys_User = TUtils.DataTableCmUtils.ToListof<RptSv_Sys_User>(dt_RptSv_Sys_User);
				objRT_RptSv_Sys_User.Lst_RptSv_Sys_User = lst_RptSv_Sys_User;
				objRT_RptSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
				#endregion

				return mdsResult;
			}
			catch (Exception exc)
			{
				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsResult
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
			}
			finally
			{
				// Write ReturnLog:
				//_cf.ProcessBizReturn(
				//	ref mdsResult // mdsFinal
				//	, strTid // strTid
				//	, strFunctionName // strFunctionName
				//	);
			}
		}
		#endregion

		#region // RptSv_Invoice_TempGroup:
		public DataSet RptSv_Invoice_TempGroup_Get(
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
			, string strRt_Cols_Invoice_TempGroup
			, string strRt_Cols_Invoice_TempGroupField
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Invoice_TempGroup_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Invoice_TempGroup_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Invoice_TempGroup", strRt_Cols_Invoice_TempGroup
				, "strRt_Cols_Invoice_TempGroupField", strRt_Cols_Invoice_TempGroupField
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Check:
				//// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_Invoice_TempGroup = (strRt_Cols_Invoice_TempGroup != null && strRt_Cols_Invoice_TempGroup.Length > 0);
				bool bGet_Invoice_TempGroupField = (strRt_Cols_Invoice_TempGroupField != null && strRt_Cols_Invoice_TempGroupField.Length > 0);

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
						---- #tbl_Invoice_TempGroup_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, itgrp.InvoiceTGroupCode
						into #tbl_Invoice_TempGroup_Filter_Draft
						from Invoice_TempGroup itgrp --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by itgrp.InvoiceTGroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Invoice_TempGroup_Filter_Draft t --//[mylock]
						;

						---- #tbl_Invoice_TempGroup_Filter:
						select
							t.*
						into #tbl_Invoice_TempGroup_Filter
						from #tbl_Invoice_TempGroup_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Invoice_TempGroup -------:
						zzB_Select_Invoice_TempGroup_zzE
						---------------------------------

						-------- Invoice_TempGroupField -------:
						zzB_Select_Invoice_TempGroupField_zzE
						---------------------------------------

						---- Clear for debug:
						--drop table #tbl_Invoice_TempGroup_Filter_Draft;
						--drop table #tbl_Invoice_TempGroup_Filter;
					"
					);
				////
				string zzB_Select_Invoice_TempGroup_zzE = "-- Nothing.";
				if (bGet_Invoice_TempGroup)
				{
					#region // bGet_Invoice_TempGroup:
					zzB_Select_Invoice_TempGroup_zzE = CmUtils.StringUtils.Replace(@"
							---- Invoice_TempGroup:
							select
								t.MyIdxSeq
								, itgrp.*
							from #tbl_Invoice_TempGroup_Filter t --//[mylock]
								inner join Invoice_TempGroup itgrp --//[mylock]
									on t.InvoiceTGroupCode = itgrp.InvoiceTGroupCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Invoice_TempGroupField_zzE = "-- Nothing.";
				if (bGet_Invoice_TempGroupField)
				{
					#region // bGet_Invoice_TempGroupField:
					zzB_Select_Invoice_TempGroupField_zzE = CmUtils.StringUtils.Replace(@"
							---- Invoice_TempGroupField:
							select
								t.MyIdxSeq
								, itgrpf.*
								, icf.InvoiceCustomFieldCode icf_InvoiceCustomFieldCode
								, icf.InvoiceCustomFieldName icf_InvoiceCustomFieldName
								, idcf.InvoiceDtlCustomFieldCode idcf_InvoiceDtlCustomFieldCode
								, idcf.InvoiceDtlCustomFieldName idcf_InvoiceDtlCustomFieldName
							from #tbl_Invoice_TempGroup_Filter t --//[mylock]
								inner join Invoice_TempGroupField itgrpf --//[mylock]
									on t.InvoiceTGroupCode = itgrpf.InvoiceTGroupCode
								left join Invoice_CustomField icf --//[mylock]
									on itgrpf.DBFieldName = icf.InvoiceCustomFieldCode
								left join Invoice_DtlCustomField idcf --//[mylock]
									on itgrpf.DBFieldName = idcf.InvoiceDtlCustomFieldCode
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
							, "Invoice_TempGroup" // strTableNameDB
							, "Invoice_TempGroup." // strPrefixStd
							, "itgrp." // strPrefixAlias
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
					, "zzB_Select_Invoice_TempGroup_zzE", zzB_Select_Invoice_TempGroup_zzE
					, "zzB_Select_Invoice_TempGroupField_zzE", zzB_Select_Invoice_TempGroupField_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_Invoice_TempGroup)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Invoice_TempGroup";
				}
				if (bGet_Invoice_TempGroupField)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Invoice_TempGroupField";
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
		public DataSet WAS_RptSv_Invoice_TempGroup_Get(
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
			string strFunctionName = "WAS_RptSv_Invoice_TempGroup_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_TempGroup_Get;
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

				List<Invoice_TempGroup> lst_Invoice_TempGroup = new List<Invoice_TempGroup>();
				bool bGet_Invoice_TempGroup = (objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroup != null && objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroup.Length > 0);

				List<Invoice_TempGroupField> lst_Invoice_TempGroupField = new List<Invoice_TempGroupField>();
				bool bGet_Invoice_TempGroupField = (objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroupField != null && objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroupField.Length > 0);
				#endregion

				#region // RptSv_Invoice_TempGroup_Get:
				mdsResult = RptSv_Invoice_TempGroup_Get(
					objRQ_Invoice_TempGroup.Tid // strTid
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					, objRQ_Invoice_TempGroup.WAUserCode // strUserCode
					, objRQ_Invoice_TempGroup.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Invoice_TempGroup.Ft_RecordStart // strFt_RecordStart
					, objRQ_Invoice_TempGroup.Ft_RecordCount // strFt_RecordCount
					, objRQ_Invoice_TempGroup.Ft_WhereClause // strFt_WhereClause
															 //// Return:
					, objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroup // strRt_Cols_Invoice_TempGroup
					, objRQ_Invoice_TempGroup.Rt_Cols_Invoice_TempGroupField // strRt_Cols_Invoice_TempGroupField
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					if (bGet_Invoice_TempGroup)
					{
						////
						DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
						lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
						objRT_Invoice_TempGroup.MySummaryTable = lst_MySummaryTable[0];

						////
						DataTable dt_Invoice_TempGroup = mdsResult.Tables["Invoice_TempGroup"].Copy();
						lst_Invoice_TempGroup = TUtils.DataTableCmUtils.ToListof<Invoice_TempGroup>(dt_Invoice_TempGroup);
						objRT_Invoice_TempGroup.Lst_Invoice_TempGroup = lst_Invoice_TempGroup;
					}

					////
					if (bGet_Invoice_TempGroupField)
					{
						////
						DataTable dt_Invoice_TempGroupField = mdsResult.Tables["Invoice_TempGroupField"].Copy();
						lst_Invoice_TempGroupField = TUtils.DataTableCmUtils.ToListof<Invoice_TempGroupField>(dt_Invoice_TempGroupField);
						objRT_Invoice_TempGroup.Lst_Invoice_TempGroupField = lst_Invoice_TempGroupField;
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
		public DataSet RptSv_Invoice_TempGroup_Create(
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
			string strFunctionName = "RptSv_Invoice_TempGroup_Create";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Invoice_TempGroup_Create;
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
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
							TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupCode
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
							TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidVATType
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
							TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupName
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
							TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidSpec_Prd_Type
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
							TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldNotFound
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
								TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName
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
								TError.ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType
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
		public DataSet WAS_RptSv_Invoice_TempGroup_Create(
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
			string strFunctionName = "WAS_RptSv_Invoice_TempGroup_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_TempGroup_Create;
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

				#region // RptSv_Invoice_TempGroup_Create:
				mdsResult = RptSv_Invoice_TempGroup_Create(
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
		public DataSet RptSv_Invoice_TempGroup_Update(
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
			string strFunctionName = "RptSv_Invoice_TempGroup_Update";
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
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
		public DataSet WAS_RptSv_Invoice_TempGroup_Update(
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
			string strFunctionName = "WAS_RptSv_Invoice_TempGroup_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_TempGroup_Update;
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

				#region // RptSv_Invoice_TempGroup_Update:
				mdsResult = RptSv_Invoice_TempGroup_Update(
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
		public DataSet RptSv_Invoice_TempGroup_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			/////
			, object objInvoiceTGroupCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Invoice_TempGroup_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Invoice_TempGroup_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objProvinceCode", objInvoiceTGroupCode
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				////
				string strInvoiceTGroupCode = TUtils.CUtils.StdParam(objInvoiceTGroupCode);
				////
				DataTable dtDB_Invoice_TempGroup = null;
				{
					////
					Invoice_TempGroup_CheckDB(
						 ref alParamsCoupleError // alParamsCoupleError
						 , objInvoiceTGroupCode // objProvinceCode
						 , TConst.Flag.Yes // strFlagExistToCheck
						 , "" // strFlagActiveListToCheck
						 , out dtDB_Invoice_TempGroup // dtDB_Invoice_TempGroup
						);
					////
				}
				#endregion

				#region // SaveDB Invoice_TempGroup:
				//// Clear All:
				{
					string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_Invoice_TempGroup:
                            select 
                                t.InvoiceTGroupCode
                            into #tbl_Invoice_TempGroup
                            from Invoice_TempGroup t --//[mylock]
                            where (1=1)
                                and t.InvoiceTGroupCode = '@strInvoiceTGroupCode'
                            ;

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

                            ---- Invoice_TempGroup:
                            delete t 
                            from Invoice_TempGroup t --//[mylock]
	                            inner join #tbl_Invoice_TempGroup f --//[mylock]
		                            on t.InvoiceTGroupCode = f.InvoiceTGroupCode
                            where (1=1)
                            ;

                            --- Clear For Debug:
                            drop table #tbl_Invoice_TempGroup;
                            drop table #tbl_Invoice_TempGroupField;
						"
						, "@strInvoiceTGroupCode", strInvoiceTGroupCode
						);
					DataSet dtDB = _cf.db.ExecQuery(
						strSqlDelete
						);
				}
				/////
				//{
				//    // Init:
				//    dtDB_Invoice_TempGroup.Rows[0].Delete();

				//    // Save:
				//    _cf.db.SaveData(
				//        "Invoice_TempGroup"
				//        , dtDB_Invoice_TempGroup
				//        );
				//}
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
		public DataSet WAS_RptSv_Invoice_TempGroup_Delete(
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
			string strFunctionName = "WAS_RptSv_Invoice_TempGroup_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Invoice_TempGroup_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup.Invoice_TempGroup)
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

				#region // RptSv_Invoice_TempGroup_Delete:
				mdsResult = RptSv_Invoice_TempGroup_Delete(
					objRQ_Invoice_TempGroup.Tid // strTid
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					, objRQ_Invoice_TempGroup.WAUserCode // strUserCode
					, objRQ_Invoice_TempGroup.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Invoice_TempGroup.Invoice_TempGroup.InvoiceTGroupCode // objInvoiceTGroupCode
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

		#region // RptSv_Mst_VATRate:
		public DataSet RptSv_Mst_VATRate_Get(
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
			, string strRt_Cols_Mst_VATRate
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			// bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Mst_VATRate_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_VATRate_Get;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_VATRate", strRt_Cols_Mst_VATRate
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_VATRate_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_VATRate_GetX(
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
						, strRt_Cols_Mst_VATRate // strRt_Cols_Mst_VATRate
												 ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				}
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
		public DataSet WAS_RptSv_Mst_VATRate_Get(
		  ref ArrayList alParamsCoupleError
		  , RQ_Mst_VATRate objRQ_Mst_VATRate
		  ////
		  , out RT_Mst_VATRate objRT_Mst_VATRate
		  )
		{
			#region // Temp:
			string strTid = objRQ_Mst_VATRate.Tid;
			objRT_Mst_VATRate = new RT_Mst_VATRate();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_VATRate_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_VATRate_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

				List<Mst_VATRate> lst_Mst_VATRate = new List<Mst_VATRate>();
				#endregion

				#region // RptSv_Mst_VATRate_Get:
				mdsResult = RptSv_Mst_VATRate_Get(
					objRQ_Mst_VATRate.Tid // strTid
					, objRQ_Mst_VATRate.GwUserCode // strGwUserCode
					, objRQ_Mst_VATRate.GwPassword // strGwPassword
					, objRQ_Mst_VATRate.WAUserCode // strUserCode
					, objRQ_Mst_VATRate.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_VATRate.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_VATRate.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_VATRate.Ft_WhereClause // strFt_WhereClause
													   //// Return:
					, objRQ_Mst_VATRate.Rt_Cols_Mst_VATRate // Rt_Cols_Mst_VATRate
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_VATRate.MySummaryTable = lst_MySummaryTable[0];

					////
					DataTable dt_Mst_VATRate = mdsResult.Tables["Mst_VATRate"].Copy();
					lst_Mst_VATRate = TUtils.DataTableCmUtils.ToListof<Mst_VATRate>(dt_Mst_VATRate);
					objRT_Mst_VATRate.Lst_Mst_VATRate = lst_Mst_VATRate;
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

		#region // RptSv_Mst_PaymentMethods:
		public DataSet RptSv_Mst_PaymentMethods_Get(
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
			, string strRt_Cols_Mst_PaymentMethods
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			// bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Mst_PaymentMethods_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_PaymentMethods_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_PaymentMethods", strRt_Cols_Mst_PaymentMethods
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
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_PaymentMethods_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_PaymentMethods_GetX(
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
						, strRt_Cols_Mst_PaymentMethods // strRt_Cols_Mst_PaymentMethods
														////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				}
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
		public DataSet WAS_RptSv_Mst_PaymentMethods_Get(
		  ref ArrayList alParamsCoupleError
		  , RQ_Mst_PaymentMethods objRQ_Mst_PaymentMethods
		  ////
		  , out RT_Mst_PaymentMethods objRT_Mst_PaymentMethods
		  )
		{
			#region // Temp:
			string strTid = objRQ_Mst_PaymentMethods.Tid;
			objRT_Mst_PaymentMethods = new RT_Mst_PaymentMethods();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PaymentMethods.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_PaymentMethods_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_PaymentMethods_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
		});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

				List<Mst_PaymentMethods> lst_Mst_PaymentMethods = new List<Mst_PaymentMethods>();
				#endregion

				#region // RptSv_Mst_PaymentMethods_Get:
				mdsResult = RptSv_Mst_PaymentMethods_Get(
					objRQ_Mst_PaymentMethods.Tid // strTid
					, objRQ_Mst_PaymentMethods.GwUserCode // strGwUserCode
					, objRQ_Mst_PaymentMethods.GwPassword // strGwPassword
					, objRQ_Mst_PaymentMethods.WAUserCode // strUserCode
					, objRQ_Mst_PaymentMethods.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_PaymentMethods.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_PaymentMethods.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_PaymentMethods.Ft_WhereClause // strFt_WhereClause
															  //// Return:
					, objRQ_Mst_PaymentMethods.Rt_Cols_Mst_PaymentMethods // Rt_Cols_Mst_PaymentMethods
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_PaymentMethods.MySummaryTable = lst_MySummaryTable[0];
					////
					DataTable dt_Mst_PaymentMethods = mdsResult.Tables["Mst_PaymentMethods"].Copy();
					lst_Mst_PaymentMethods = TUtils.DataTableCmUtils.ToListof<Mst_PaymentMethods>(dt_Mst_PaymentMethods);
					objRT_Mst_PaymentMethods.Lst_Mst_PaymentMethods = lst_Mst_PaymentMethods;
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

		#region // RptSv_Mst_InvoiceType:
		public DataSet RptSv_Mst_InvoiceType_Get(
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
			, string strRt_Cols_Mst_InvoiceType
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			// bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Mst_InvoiceType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_InvoiceType_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_InvoiceType", strRt_Cols_Mst_InvoiceType
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Mst_InvoiceType_GetX:
				DataSet dsGetData = null;
				{
					////
					Mst_InvoiceType_GetX(
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
						, strRt_Cols_Mst_InvoiceType // strRt_Cols_Mst_InvoiceType
													 ////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				}
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
		public DataSet WAS_RptSv_Mst_InvoiceType_Get(
		   ref ArrayList alParamsCoupleError
		   , RQ_Mst_InvoiceType objRQ_Mst_InvoiceType
		   ////
		   , out RT_Mst_InvoiceType objRT_Mst_InvoiceType
		   )
		{
			#region // Temp:
			string strTid = objRQ_Mst_InvoiceType.Tid;
			objRT_Mst_InvoiceType = new RT_Mst_InvoiceType();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvoiceType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_InvoiceType_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_InvoiceType_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

				List<Mst_InvoiceType> lst_Mst_InvoiceType = new List<Mst_InvoiceType>();
				#endregion

				#region // RptSv_Mst_InvoiceType_Get:
				mdsResult = RptSv_Mst_InvoiceType_Get(
					objRQ_Mst_InvoiceType.Tid // strTid
					, objRQ_Mst_InvoiceType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvoiceType.GwPassword // strGwPassword
					, objRQ_Mst_InvoiceType.WAUserCode // strUserCode
					, objRQ_Mst_InvoiceType.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_InvoiceType.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_InvoiceType.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_InvoiceType.Ft_WhereClause // strFt_WhereClause
														   //// Return:
					, objRQ_Mst_InvoiceType.Rt_Cols_Mst_InvoiceType // Rt_Cols_Mst_InvoiceType
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_InvoiceType.MySummaryTable = lst_MySummaryTable[0];

					////
					DataTable dt_Mst_InvoiceType = mdsResult.Tables["Mst_InvoiceType"].Copy();
					lst_Mst_InvoiceType = TUtils.DataTableCmUtils.ToListof<Mst_InvoiceType>(dt_Mst_InvoiceType);
					objRT_Mst_InvoiceType.Lst_Mst_InvoiceType = lst_Mst_InvoiceType;
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

		#region // Map_DealerDiscount:
		private void RptSv_Map_DealerDiscount_GetX(
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
			, string strRt_Cols_Map_DealerDiscount
			, out DataSet dsGetData
		   )
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Map_DealerDiscount_GetX";
			//string strErrorCodeDefault = TError.ErrHTCNM.RptSv_Map_DealerDiscount_GetX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					});
			#endregion

			#region // Check:
			//// Refine:
			long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
			long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
			bool bGet_Map_DealerDiscount = (strRt_Cols_Map_DealerDiscount != null && strRt_Cols_Map_DealerDiscount.Length > 0);

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
						---- #tbl_Map_DealerDiscount_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mdldc.DLCode
							, mdldc.DiscountCode
						into #tbl_Map_DealerDiscount_Filter_Draft
						from Map_DealerDiscount mdldc --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by mdldc.DLCode asc
								, mdldc.DiscountCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Map_DealerDiscount_Filter_Draft t --//[mylock]
						;

						---- #tbl_Map_DealerDiscount_Filter:
						select
							t.*
						into #tbl_Map_DealerDiscount_Filter
						from #tbl_Map_DealerDiscount_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Map_DealerDiscount --------:
						zzB_Select_Map_DealerDiscount_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Map_DealerDiscount_Filter_Draft;
						--drop table #tbl_Map_DealerDiscount_Filter;
					"
				);
			////
			string zzB_Select_Map_DealerDiscount_zzE = "-- Nothing.";
			if (bGet_Map_DealerDiscount)
			{
				#region // bGet_Map_DealerDiscount:
				zzB_Select_Map_DealerDiscount_zzE = CmUtils.StringUtils.Replace(@"
							---- Map_DealerDiscount:
							select
								t.MyIdxSeq
								, mdldc.*
							from #tbl_Map_DealerDiscount_Filter t --//[mylock]
								inner join Map_DealerDiscount mdldc --//[mylock]
									on t.DLCode = mdldc.DLCode
										and t.DiscountCode = mdldc.DiscountCode
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
						, "Map_DealerDiscount" // strTableNameDB
						, "Map_DealerDiscount." // strPrefixStd
						, "mdldc." // strPrefixAlias
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
				, "zzB_Select_Map_DealerDiscount_zzE", zzB_Select_Map_DealerDiscount_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Map_DealerDiscount)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Map_DealerDiscount";
			}
			#endregion
		}
		public DataSet RptSv_Map_DealerDiscount_Get(
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
			, string strRt_Cols_Map_DealerDiscount
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			// bool bNeedTransaction = true;
			string strFunctionName = "RptSv_Map_DealerDiscount_Get";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Map_DealerDiscount_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Map_DealerDiscount", strRt_Cols_Map_DealerDiscount
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

				// RptSv_Sys_User_CheckAuthentication:
				//RptSv_Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				RptSv_Sys_Access_CheckDeny(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // RptSv_Map_DealerDiscount_GetX:
				DataSet dsGetData = null;
				{
					////
					RptSv_Map_DealerDiscount_GetX(
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
						, strRt_Cols_Map_DealerDiscount // strRt_Cols_Map_DealerDiscount
														////
						, out dsGetData // dsGetData
						);
					////
					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				}
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
		public DataSet WAS_RptSv_Map_DealerDiscount_Get(
		   ref ArrayList alParamsCoupleError
		   , RQ_Map_DealerDiscount objRQ_Map_DealerDiscount
		   ////
		   , out RT_Map_DealerDiscount objRT_Map_DealerDiscount
		   )
		{
			#region // Temp:
			string strTid = objRQ_Map_DealerDiscount.Tid;
			objRT_Map_DealerDiscount = new RT_Map_DealerDiscount();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvoiceType.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Map_DealerDiscount_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Map_DealerDiscount_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

				List<Map_DealerDiscount> lst_Map_DealerDiscount = new List<Map_DealerDiscount>();
				#endregion

				#region // RptSv_Map_DealerDiscount_Get:
				mdsResult = RptSv_Map_DealerDiscount_Get(
					objRQ_Map_DealerDiscount.Tid // strTid
					, objRQ_Map_DealerDiscount.GwUserCode // strGwUserCode
					, objRQ_Map_DealerDiscount.GwPassword // strGwPassword
					, objRQ_Map_DealerDiscount.WAUserCode // strUserCode
					, objRQ_Map_DealerDiscount.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Map_DealerDiscount.Ft_RecordStart // strFt_RecordStart
					, objRQ_Map_DealerDiscount.Ft_RecordCount // strFt_RecordCount
					, objRQ_Map_DealerDiscount.Ft_WhereClause // strFt_WhereClause
														   //// Return:
					, objRQ_Map_DealerDiscount.Rt_Cols_Map_DealerDiscount // Rt_Cols_Map_DealerDiscount
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Map_DealerDiscount.MySummaryTable = lst_MySummaryTable[0];

					////
					DataTable dt_Map_DealerDiscount = mdsResult.Tables["Map_DealerDiscount"].Copy();
					lst_Map_DealerDiscount = TUtils.DataTableCmUtils.ToListof<Map_DealerDiscount>(dt_Map_DealerDiscount);
					objRT_Map_DealerDiscount.Lst_Map_DealerDiscount = lst_Map_DealerDiscount;
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

		#region // OS_RptSv_Mst_NNT:
		public DataSet OS_RptSv_Mst_NNT_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			, object objOrgID
			, object objNNTFullName
			, object objMSTParent
			, object objProvinceCode
			, object objDistrictCode
			//, object objNNTType
			, object objDLCode
			, object objNNTAddress
			, object objNNTMobile
			, object objNTTPhone
			, object objNNTFax
			, object objPresentBy
			, object objBusinessRegNo
			, object objNNTPosition
			, object objPresentIDNo
			, object objPresentIDType
			, object objGovTaxID
			, object objContactName
			, object objContactPhone
			, object objContactEmail
			, object objWebsite
			, object objCANumber
			, object objCAOrg
			, object objCAEffDTimeUTCStart
			, object objCAEffDTimeUTCEnd
			, object objPackageCode
			, object objCreatedDate
			, object objAccNo
			, object objAccHolder
			, object objBankName
			, object objBizType // objBizType
			, object objBizFieldCode // objBizFieldCode
			, object objBizSizeCode // objBizSizeCode
			, object objDealerType // objDealerType
			//
			, object objOrgIDSln // strOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_RptSv_Mst_NNT_Create";
			string strErrorCodeDefault = TError.ErridnInventory.OS_RptSv_Mst_NNT_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                //, "objNetworkID", objNetworkID
                //, "objOrgParent", objOrgParent
                , "objMST", objMST
				, "objOrgID", objOrgID
				, "objNNTFullName", objNNTFullName
				, "objMSTParent", objMSTParent
				, "objProvinceCode", objProvinceCode
				, "objDistrictCode", objDistrictCode
				, "objDLCode", objDLCode
				, "objNNTAddress", objNNTAddress
				, "objNNTMobile", objNNTMobile
				, "objNTTPhone", objNTTPhone
				, "objNNTFax", objNNTFax
				, "objPresentBy", objPresentBy
				, "objBusinessRegNo", objBusinessRegNo
				, "objNNTPosition", objNNTPosition
				, "objPresentIDNo", objPresentIDNo
				, "objPresentIDType", objPresentIDType
				, "objGovTaxID", objGovTaxID
				, "objContactName", objContactName
				, "objContactPhone", objContactPhone
				, "objContactEmail", objContactEmail
				, "objWebsite", objWebsite
				, "objCANumber", objCANumber
				, "objCAOrg", objCAOrg
				, "objCAEffDTimeUTCStart", objCAEffDTimeUTCStart
				, "objCAEffDTimeUTCEnd", objCAEffDTimeUTCEnd
				, "objPackageCode", objPackageCode
				, "objCreatedDate", objCreatedDate
				, "objAccNo", objAccNo
				, "objAccHolder", objAccHolder
				, "objBankName", objBankName
				, "objBizType", objBizType // objBizType
                , "objBizFieldCode", objBizFieldCode // objBizFieldCode
                , "objBizSizeCode", objBizSizeCode // objBizSizeCode
				, "objDealerType", objDealerType
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
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
					ref alParamsCoupleError
					, strWAUserCode
					, strFunctionName
					);
				#endregion

				#region // Refine and Check Input:
				// drAbilityOfUser:
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgID = TUtils.CUtils.StdParam(objOrgID);
				string strNNTFullName = string.Format("{0}", objNNTFullName).Trim();
				string strMSTParent = TUtils.CUtils.StdParam(objMSTParent);
				string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
				string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
				//string strNNTType = TUtils.CUtils.StdParam(objNNTType);
				string strDLCode = TUtils.CUtils.StdParam(objDLCode);
				string strNNTAddress = string.Format("{0}", objNNTAddress).Trim();
				string strNNTMobile = string.Format("{0}", objNNTMobile).Trim();
				string strNNTPhone = string.Format("{0}", objNTTPhone).Trim();
				string strNNTFax = string.Format("{0}", objNNTFax).Trim();
				string strPresentBy = string.Format("{0}", objPresentBy).Trim();
				string strBusinessRegNo = string.Format("{0}", objBusinessRegNo).Trim();
				string strNNTPosition = string.Format("{0}", objNNTPosition).Trim();
				string strPresentIDNo = string.Format("{0}", objPresentIDNo).Trim();
				string strPresentIDType = string.Format("{0}", objPresentIDType).Trim();
				string strGovTaxID = TUtils.CUtils.StdParam(objGovTaxID);
				string strContactName = string.Format("{0}", objContactName).Trim();
				string strContactPhone = string.Format("{0}", objContactPhone).Trim();
				string strContactEmail = string.Format("{0}", objContactEmail).Trim();
				string strWebsite = string.Format("{0}", objWebsite).Trim();
				string strCANumber = string.Format("{0}", objCANumber).Trim();
				string strCAOrg = string.Format("{0}", objCAOrg).Trim();
				string strCAEffDTimeUTCStart = TUtils.CUtils.StdDTime(objCAEffDTimeUTCStart);
				string strCAEffDTimeUTCEnd = TUtils.CUtils.StdDTime(objCAEffDTimeUTCEnd);
				string strPackageCode = string.Format("{0}", objPackageCode).Trim();
				string strCreatedDate = TUtils.CUtils.StdDate(objCreatedDate);
				string strAccNo = string.Format("{0}", objAccNo).Trim();
				string strAccHolder = string.Format("{0}", objAccHolder).Trim();
				string strBankName = string.Format("{0}", objBankName).Trim();
				string strBizType = TUtils.CUtils.StdParam(objBizType);
				string strBizFieldCode = TUtils.CUtils.StdParam(objBizFieldCode);
				string strBizSizeCode = TUtils.CUtils.StdParam(objBizSizeCode);
				string strDealerType = TUtils.CUtils.StdParam(objDealerType);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				////
				DataTable dtDB_Mst_Param_CheckDB = null;

				Mst_Param_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, TConst.Mst_Param.INVENTORY_RPTSV_URL //INVENTORY_RPTSV_URL
					, TConst.Flag.Yes // strFlagExistToCheck
					, out dtDB_Mst_Param_CheckDB // dtDB_Mst_Param_CheckDB
					);

				string strUrl = dtDB_Mst_Param_CheckDB.Rows[0]["ParamValue"].ToString();
				////
				Mst_Param_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, TConst.Mst_Param.PARAM_OS_REPORTSERVER_BG_WAUSERCODE // PARAM_OS_REPORTSERVER_BG_WAUSERCODE
					, TConst.Flag.Yes // strFlagExistToCheck
					, out dtDB_Mst_Param_CheckDB // dtDB_Mst_Param_CheckDB
					);

				string strRptSvUserCode = dtDB_Mst_Param_CheckDB.Rows[0]["ParamValue"].ToString();
				////
				Mst_Param_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, TConst.Mst_Param.PARAM_OS_REPORTSERVER_BG_WAUSERPASSWORD // PARAM_OS_REPORTSERVER_BG_WAUSERPASSWORD
					, TConst.Flag.Yes // strFlagExistToCheck
					, out dtDB_Mst_Param_CheckDB // dtDB_Mst_Param_CheckDB
					);

				string strRptSvUserPasswork = dtDB_Mst_Param_CheckDB.Rows[0]["ParamValue"].ToString();
				////
				#endregion

				#region // Call Func:
				RT_Mst_NNT objRT_Mst_NNT = null;
				{
					#region // Init:
					Mst_NNT objMst_NNT = new Mst_NNT();
					objMst_NNT.MST = strMST;
					objMst_NNT.OrgID = strOrgID;
					objMst_NNT.NNTFullName = strNNTFullName;
					objMst_NNT.MSTParent = strMSTParent;
					objMst_NNT.ProvinceCode = strProvinceCode;
					objMst_NNT.DistrictCode = strDistrictCode;
					//objMst_NNT.NNTType = strNNTType;
					objMst_NNT.DLCode = strDLCode;
					objMst_NNT.NNTAddress = strNNTAddress;
					objMst_NNT.NNTMobile = strNNTMobile;
					objMst_NNT.NNTPhone = strNNTPhone;
					objMst_NNT.NNTFax = strNNTFax;
					objMst_NNT.PresentBy = strPresentBy;
					objMst_NNT.BusinessRegNo = strBusinessRegNo;
					objMst_NNT.NNTPosition = strNNTPosition;
					objMst_NNT.PresentIDNo = strPresentIDNo;
					objMst_NNT.PresentIDType = strPresentIDType;
					objMst_NNT.GovTaxID = strGovTaxID;
					objMst_NNT.ContactName = strContactName;
					objMst_NNT.ContactPhone = strContactPhone;
					objMst_NNT.ContactEmail = strContactEmail;
					objMst_NNT.Website = strWebsite;
					objMst_NNT.CANumber = strCANumber;
					objMst_NNT.CAOrg = strCAOrg;
					objMst_NNT.CAEffDTimeUTCStart = strCAEffDTimeUTCStart;
					objMst_NNT.CAEffDTimeUTCEnd = strCAEffDTimeUTCEnd;
					objMst_NNT.PackageCode = strPackageCode;
					objMst_NNT.CreatedDate = strCreatedDate;
					objMst_NNT.AccNo = strAccNo;
					objMst_NNT.AccHolder = strAccHolder;
					objMst_NNT.BankName = strBankName;
					objMst_NNT.BizType = strBizType;
					objMst_NNT.BizFieldCode = strBizFieldCode;
					objMst_NNT.BizSizeCode = strBizSizeCode;
					objMst_NNT.DealerType = strDealerType;
					objMst_NNT.OrgIDSln = strOrgIDSln;
					/////
					RQ_Mst_NNT objRQ_Mst_NNT = new RQ_Mst_NNT()
					{
						Tid = strTid,
						TokenID = strOS_MasterServer_Solution_API_Url,
						NetworkID = nNetworkID.ToString(),
						GwUserCode = strOS_MasterServer_Solution_GwUserCode,
						GwPassword = strOS_MasterServer_Solution_GwPassword,
						WAUserCode = strRptSvUserCode,
						WAUserPassword = strRptSvUserPasswork,
						Mst_NNT = objMst_NNT,
					};
					////
					try
					{

						objRT_Mst_NNT = OS_RptSvInBrand_Mst_NNTService.Instance.WA_OS_RptSv_Mst_NNT_Create(strUrl, objRQ_Mst_NNT);
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
							TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                            , null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					#endregion
				}
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
		#endregion
	}
}
