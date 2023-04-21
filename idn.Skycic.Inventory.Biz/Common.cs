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
using idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Constructors and Destructors:
		public BizidNInventory()
		{

		}

		private static bool init_bSuccess = false;
		private static object init_objLock = new object();
		private static bool initLicense_bSuccess = false;
		private static object initLicense_objLock = new object();
		private static Int64 nNetworkID = -1;
		private static string strOrgID_Login = "-1";
		private static Int64 nTidSeq_InvoiceApprovedMulti = 0;
		//private static string nUrlSearch = null;
		//private static Int64 nNetworkIDSearch = -1;
		private static string strSolutionCode = null;
		private static CConfig init_cf = null;
		//private static string strAccCenterSessionId = null;
		private static CmUtils.SeqUtils init_seq = new CmUtils.SeqUtils();
		private static CBGProcess init_bgp = new CBGProcess();
		public CConfig _cf = null;
        private static Hashtable htCacheMstParam = (Hashtable)null;
        ///// ThomPTT. Config
        public string strOS_MasterServer_PrdCenter_API_Url = "";
        public string strOS_MasterServer_PrdCenter_UserCode = "";
        public string strOS_MasterServer_PrdCenter_UserPassword = "";
        public string strOS_MasterServer_PrdCenter_TokenID = "";
        public string strOS_MasterServer_PrdCenter_GwUserCode = "";
        public string strOS_MasterServer_PrdCenter_GwPassword = "";
        public string strOS_MasterServer_PrdCenter_WAUserCode = "";
        public string strOS_MasterServer_PrdCenter_WAUserPassword = "";
        /////
        public string strOS_ProductCentrer_GwUserCode = "";
        public string strOS_ProductCentrer_GwPassword = "";
        public string strOS_ProductCentrer_WAUserCode = "";
        public string strOS_ProductCentrer_WAUserPassword = "";
        /////
        public string strOS_MasterServer_Solution_API_Url = "";
        public string strOS_MasterServer_Solution_TokenID = "";
        public string strOS_MasterServer_Solution_GwUserCode = "";
        public string strOS_MasterServer_Solution_GwPassword = "";
        public string strOS_MasterServer_Solution_WAUserCode = "";
        public string strOS_MasterServer_Solution_WAUserPassword = "";
        public string strOS_MasterServer_Solution_SysOSCode3A = "";


        public string strOS_MasterServer_Solution_ForInput_API_Url = "";
        public void LoadConfig(
			System.Collections.Specialized.NameValueCollection nvcParams
			, string strSvCode
			)
		{
			// Init:
			if (!init_bSuccess)
			{
				lock (init_objLock)
				{
					if (!init_bSuccess)
					{
						// Init:
						CConfig cf = new CConfig();

						// Params:
						cf.nvcParams = nvcParams;

						// DB:
						cf.db = new TDAL.EzDALSqlSv(cf.nvcParams["Biz_DBConnStr"]);
						cf.db.LogAutoMode = "";
						cf.db.LogUserId = "";
						cf.db.InitCacheManual();
						cf.db_Sys = (TDAL.IEzDAL)cf.db.Clone();

						// Session:
						cf.sess = new TSession.Core.CSession(cf.nvcParams["Biz_LicenseCode"], cf.nvcParams["Biz_DBConnStr"]);

						// Log:
						cf.log = new TLog.Core.CLog(
							cf.nvcParams["TLog_ConnStr"] // strConnStr
							, cf.nvcParams["TLog_AccountList"] // strAccountList
							, Convert.ToInt32(cf.nvcParams["TLog_DelayForLazyMS"]) // nDelayForLazy
							);
						cf.log.StartBackGroundProcess();

						// Assign:
						init_cf = cf;

						// init_LoadBackgroundProcess:
						init_LoadBackgroundProcess();

						// NetworkID:
						nNetworkID = Convert.ToInt64(cf.nvcParams["Biz_NetworkID"]);

						// SolutionCode:
						strSolutionCode = TUtils.CUtils.StdParam(cf.nvcParams["SolutionCode"]);

						//// Inos:
						inos.common.Paths.SetInosServerBaseAddress(cf.nvcParams["InosBaseUrl"]);

                        ////
                        htCacheMstParam = InitMstParam(cf.db);
                        // Mark:
                        init_bSuccess = true;

						htCacheMstParam = InitMstParam(cf.db);
					}
				}
			}

			// Assign:
			_cf = init_cf.MyClone();
		}

        private Hashtable InitMstParam(
            EzSql.IDBEngine db
            )
        {
            ////
            Hashtable ht = new Hashtable(200);

            string strSqlGet = CmUtils.StringUtils.Replace(@"
					---- Mst_Param:
					select 
						t.ParamCode
						, t.ParamValue
					from Mst_Param t --//[mylock]
					where (1=1)
					;
				");

            DataTable dt = db.ExecQuery(strSqlGet).Tables[0];

            for (int nScan = 0; nScan < dt.Rows.Count; nScan++)
            {
                ////
                DataRow drScan = dt.Rows[nScan];

                ht.Add((object)drScan["ParamCode"], (object)drScan["ParamValue"]);
            }

            return ht;
        }

        public void InitLicense(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsResult
			, ref ArrayList alParamsCoupleError
			)
		{
			// Init:
			if (!initLicense_bSuccess)
			{
				lock (initLicense_objLock)
				{
					if (!initLicense_bSuccess)
					{
						mdsResult = Sys_UserLicense_Save(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strUserCode
							, strWAUserPassword // strUserPassword
							, strAccessToken // strAccessToken
							, ref alParamsCoupleError // alParamsCoupleError
							);

						// Mark:
						initLicense_bSuccess = true;
					}
				}
			}
		}

		#endregion

		#region // bgp_BizUpdStatus:
		private static void init_LoadBackgroundProcess()
		{
			// BizUpdStatus_t:
			if (init_bgp.BizUpdStatus_t != null && init_bgp.BizUpdStatus_t.IsAlive)
			{
				init_bgp.BizUpdStatus_t.Abort();
			}
			init_bgp.BizUpdStatus_t = new System.Threading.Thread(bgp_BizUpdStatus);
			init_bgp.BizUpdStatus_t.Name = "init_bgp.BizUpdStatus_t";
			init_bgp.BizUpdStatus_t.IsBackground = true;
			init_bgp.BizUpdStatus_t.Start();

			// Other ....
		}

		private static void bgp_BizUpdStatus()
		{
			while (true)
			{
				// Process:
				try
				{
					bgp_BizUpdStatus_Process();
				}
				catch (Exception exc)
				{
					string strExc = string.Format("\r\nexc.Message = {0}\r\nexc.StackTrace = {1}", exc.Message, exc.StackTrace);
					System.Console.WriteLine(strExc);
				}
				if (init_bgp.BizUpdStatus_nForceProcess > 0) init_bgp.BizUpdStatus_nForceProcess--;

				// Sleep:
				for (int nSleep = 0; init_bgp.BizUpdStatus_nForceProcess <= 0 && nSleep < init_bgp.BizUpdStatus_nSleepMax; nSleep += init_bgp.BizUpdStatus_nSleepStep)
				{
					Thread.Sleep(init_bgp.BizUpdStatus_nSleepStep);
				}
			}
		}
		private static void bgp_BizUpdStatus_ForceProcess()
		{
			init_bgp.BizUpdStatus_nForceProcess++;
		}
		private static void bgp_BizUpdStatus_Process()
		{
			// Init:
			CConfig cf = init_cf.MyClone();

            //// Put your task here.

        }
        public void LoadInitAPIUrl_OutSide(
            ref ArrayList alParamsCoupleError
            )
        {
            #region // Get Config:
            int nSeq = 0;
            // ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strFunctionName", "LoadInitUrt_ProductCenter"
                });
            ////
            string strTid = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nSeq++);
            /////
            Config_OutSite_Get(
                ref alParamsCoupleError // alParamsCoupleError
                                        ////
                , ref strOS_MasterServer_PrdCenter_API_Url // strOS_MasterServer_API_Url
                , ref strOS_MasterServer_PrdCenter_UserCode // strOS_MasterServer_UserCode
                , ref strOS_MasterServer_PrdCenter_UserPassword // strOS_MasterServer_UserPassword
                , ref strOS_MasterServer_PrdCenter_TokenID // strOS_MasterServer_TokenID
                , ref strOS_MasterServer_PrdCenter_GwUserCode // strOS_MasterServer_GwUserCode
                , ref strOS_MasterServer_PrdCenter_GwPassword // strOS_MasterServer_GwPassword
                , ref strOS_MasterServer_PrdCenter_WAUserCode // strOS_MasterServer_WAUserCode
                , ref strOS_MasterServer_PrdCenter_WAUserPassword // strOS_MasterServer_WAUserPassword
                                                                    /////
                , ref strOS_ProductCentrer_GwUserCode // strOS_ProductCentrer_GwUserCode
                , ref strOS_ProductCentrer_GwPassword // strOS_ProductCentrer_GwPassword
                , ref strOS_ProductCentrer_WAUserCode // strOS_ProductCentrer_WAUserCode
                , ref strOS_ProductCentrer_WAUserPassword // strOS_ProductCentrer_WAUserPassword
                                                            /////
                , ref strOS_MasterServer_Solution_API_Url // strOS_MasterServer_Solution_API_Url
                , ref strOS_MasterServer_Solution_TokenID // strOS_MasterServer_Solution_TokenID
                , ref strOS_MasterServer_Solution_GwUserCode // strOS_MasterServer_Solution_GwUserCode
                , ref strOS_MasterServer_Solution_GwPassword // strOS_MasterServer_Solution_GwPassword
                , ref strOS_MasterServer_Solution_WAUserCode // strOS_MasterServer_Solution_WAUserCode
                , ref strOS_MasterServer_Solution_WAUserPassword // strOS_MasterServer_Solution_WAUserPassword
                , ref strOS_MasterServer_Solution_SysOSCode3A // strOS_MasterServer_Solution_SysOSCode3A
                );
            ////
            #endregion

            #region // Get BizAPIAddress:
            {

                if (string.IsNullOrEmpty(BizMasterServerPrdCenterAPIAddress.BaseBizMasterServerAPIAddress.Trim()))
                {
                    BizMasterServerPrdCenterAPIAddress.BaseBizMasterServerAPIAddress = strOS_MasterServer_PrdCenter_API_Url;
                }
                ////
                if (string.IsNullOrEmpty(BizMasterServerSolutionAPIAddress.BaseBizMasterServerSolutionAPIAddress.Trim()))
                {
                    BizMasterServerSolutionAPIAddress.BaseBizMasterServerSolutionAPIAddress = strOS_MasterServer_Solution_API_Url;
                }
                ////
                //if (string.IsNullOrEmpty(BizProductCenterAPIAddress.BaseBizProductCenterAPIAddress.Trim()))
                //{
                //    ///////
                //    #region // MstSv_Sys_User_Login:
                //    //////
                //    //DataSet mdsResult = OS_MstSvPrdCenter_MstSv_Sys_User_Login(
                //    //    strTid // strTid
                //    //    , strOS_MasterServer_PrdCenter_GwUserCode // strGwUserCode
                //    //    , strOS_MasterServer_PrdCenter_GwPassword // strGwPassword
                //    //    , strOS_MasterServer_PrdCenter_WAUserCode // strWAUserCode
                //    //    , strOS_MasterServer_PrdCenter_WAUserPassword // strWAUserPassword
                //    //    , nNetworkID.ToString() // strNetworkID
                //    //    , ref alParamsCoupleError // alParamsCoupleError
                //    //    );
                //    //if (CmUtils.CMyDataSet.HasError(mdsResult))
                //    //{
                //    //    object[] arrobjCouple = CmUtils.CMyDataSet.GetErrorParams(mdsResult);
                //    //    if (arrobjCouple == null) arrobjCouple = new object[0];
                //    //    object[] arrobjTriple = new object[arrobjCouple.Length / 2 * 3];
                //    //    /////
                //    //    for (int i = 0, j = 0; i < arrobjCouple.Length; i += 2, j += 3)
                //    //    {
                //    //        alParamsCoupleError.AddRange(new object[] {
                //    //            string.Format("{0}", arrobjCouple[i]).Trim(), arrobjCouple[i + 1]
                //    //            });
                //    //    }

                //    //    throw CmUtils.CMyException.Raise(
                //    //        (string)CmUtils.CMyDataSet.GetErrorCode(mdsResult)
                //    //        , null
                //    //        , alParamsCoupleError.ToArray()
                //    //        );
                //    //}
                //    //else
                //    //{
                //    //    ////
                //    //    BizProductCenterAPIAddress.BaseBizProductCenterAPIAddress = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsResult));

                //    //}

                //    ///BizProductCenterAPIAddress.BaseBizProductCenterAPIAddress = "http://localhost:12308/";
                //    //BizProductCenterAPIAddress.BaseBizProductCenterAPIAddress = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_API_Url"];
                //    #endregion
                //}
                ////
            }
            #endregion

        }

        #endregion

        #region // Common:
        public static void Cm_Reinit()
		{
			init_bSuccess = false;
		}
		public string Cm_Test()
		{
			return DateTime.UtcNow.ToString("yyyyMMdd.HHmmss.ffffff");
		}
		public DataSet Cm_GetId(
			string strTid
			)
		{
			// Init:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			CmUtils.CMyDataSet.SetRemark(ref mdsFinal, init_seq.GetSeqDateBased("{0:yyyyMMdd.HHmmss}.{1:000}", 1000));
			mdsFinal.AcceptChanges();

			// Return Good:
			return mdsFinal;
		}
		public DataSet Cm_GetDTime(
			string strTid
			)
		{
			// Init:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			CmUtils.CMyDataSet.SetRemark(ref mdsFinal, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
			mdsFinal.AcceptChanges();

			// Return Good:
			return mdsFinal;
		}
		public DataSet Cm_ExecSql(
			string strTid
			, DataRow drSession
			, string strBiz_SpecialPw
			, string strSql
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = true;
			string strFunctionName = "Cm_ExecSql";
			string strErrorCodeDefault = TError.ErridnInventory.Cm_ExecSql;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
					"strFunctionName", strFunctionName
					, "strSql", strSql
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
				//strTableName = TUtils.CUtils.StdParam(strTableName);

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = mySys_User_GetAbilityViewBankOfUser(_cf.sinf.strUserCode);

				// CmSys_InvalidBizSpecialPw:
				if (!CmUtils.StringUtils.StringEqual(_cf.nvcParams["Biz_SpecialPw"], strBiz_SpecialPw))
				{
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.CmSys_InvalidBizSpecialPw
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				// strSql:
				int nPosErr_ParamMissing = strSql.ToUpper().IndexOf("zzzzClause".ToUpper());
				if (nPosErr_ParamMissing >= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.nPosErr_ParamMissing", nPosErr_ParamMissing
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Cm_ExecSql_ParamMissing
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				#endregion

				#region // Get Data:
				DataSet dsData = _cf.db.ExecQuery(strSql);
				for (int nIdxScan = 0; nIdxScan < dsData.Tables.Count; nIdxScan++)
				{
					dsData.Tables[nIdxScan].TableName = string.Format("MyTable{0}", nIdxScan);
				}
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsData);
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
		public DataSet Mst_Common_Get(
			string strTid
			, DataRow drSession
			////
			, string strTableName
			, object objFilter0
			, object objFilter1
			, object objFilter2
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bNeedTransaction = false;
			string strFunctionName = "Mst_Common_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Common_Get;
			ArrayList alParamsCoupleError = new ArrayList(new object[]{
				"strFunctionName", strFunctionName
				, "strTableName", strTableName
				, "objFilter0", objFilter0
				, "objFilter1", objFilter1
				, "objFilter2", objFilter2
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
				strTableName = TUtils.CUtils.StdParam(strTableName);
				////

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = mySys_User_GetAbilityViewBankOfUser(_cf.sinf.strUserCode);

				#endregion

				#region // Get Data:
				////
				string strSqlGetData = "";
				string zzzzClauseTableCheck = "";
				////
				zzzzClauseTableCheck = "Ins_ClaimDocType";
				if (CmUtils.StringUtils.StringEqualIgnoreCase(strTableName, zzzzClauseTableCheck))
				{
					strSqlGetData = CmUtils.StringUtils.Replace(@"
							select
								*
							from zzzzClauseTableCheck t --//[mylock]
							where (1=1)
								--and t.ClmDocType = @objFilter0
                            order by t.Idx
							;
						"
						, "zzzzClauseTableCheck", zzzzClauseTableCheck
						);
				}
				////
				zzzzClauseTableCheck = "Ins_ContractType";
				if (CmUtils.StringUtils.StringEqualIgnoreCase(strTableName, zzzzClauseTableCheck))
				{
					strSqlGetData = CmUtils.StringUtils.Replace(@"
							select
								*
							from zzzzClauseTableCheck t --//[mylock]
							where (1=1)
								--and t.ContractType = @objFilter0
							;
						"
						, "zzzzClauseTableCheck", zzzzClauseTableCheck
						);
				}
				////
				zzzzClauseTableCheck = "Mst_Param";
				if (CmUtils.StringUtils.StringEqualIgnoreCase(strTableName, zzzzClauseTableCheck))
				{
					strSqlGetData = CmUtils.StringUtils.Replace(@"
							select
								*
							from zzzzClauseTableCheck t --//[mylock]
							where (1=1)
								--and t.ParamCode = @objFilter0
							;
						"
						, "zzzzClauseTableCheck", zzzzClauseTableCheck
						);
				}
				////
				zzzzClauseTableCheck = "Sys_ObjectType";
				if (CmUtils.StringUtils.StringEqualIgnoreCase(strTableName, zzzzClauseTableCheck))
				{
					strSqlGetData = CmUtils.StringUtils.Replace(@"
							select
								*
							from zzzzClauseTableCheck t --//[mylock]
							where (1=1)
								--and t.ObjectType = @objFilter0
							;
						"
						, "zzzzClauseTableCheck", zzzzClauseTableCheck
						);
				}
				////
				zzzzClauseTableCheck = "Sys_Service";
				if (CmUtils.StringUtils.StringEqualIgnoreCase(strTableName, zzzzClauseTableCheck))
				{
					strSqlGetData = CmUtils.StringUtils.Replace(@"
							select
								*
							from zzzzClauseTableCheck t --//[mylock]
							where (1=1)
								--and t.ServiceCode = @objFilter0
							;
						"
						, "zzzzClauseTableCheck", zzzzClauseTableCheck
						);
				}
				////
				if (strSqlGetData.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strTableName", strTableName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Common_Get_NotSupportTable
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtGetData = _cf.db.ExecQuery(
					strSqlGetData
					, "@objFilter0", TUtils.CUtils.IsNullSql(objFilter0)
					, "@objFilter1", TUtils.CUtils.IsNullSql(objFilter1)
					, "@objFilter2", TUtils.CUtils.IsNullSql(objFilter2)
					).Tables[0];
				dtGetData.TableName = zzzzClauseTableCheck;
				CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dtGetData);
				////
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


        public void Config_OutSite_Get(
            ref ArrayList alParamsCoupleError
            , ref string strOS_MasterServer_PrdCenter_API_Url
            , ref string strOS_MasterServer_PrdCenter_UserCode
            , ref string strOS_MasterServer_PrdCenter_UserPassword
            , ref string strOS_MasterServer_PrdCenter_TokenID
            , ref string strOS_MasterServer_PrdCenter_GwUserCode
            , ref string strOS_MasterServer_PrdCenter_GwPassword
            , ref string strOS_MasterServer_PrdCenter_WAUserCode
            , ref string strOS_MasterServer_PrdCenter_WAUserPassword
            /////
            , ref string strOS_ProductCentrer_GwUserCode
            , ref string strOS_ProductCentrer_GwPassword
            , ref string strOS_ProductCentrer_WAUserCode
            , ref string strOS_ProductCentrer_WAUserPassword
            ////
            , ref string strOS_MasterServer_Solution_API_Url
            , ref string strOS_MasterServer_Solution_TokenID
            , ref string strOS_MasterServer_Solution_GwUserCode
            , ref string strOS_MasterServer_Solution_GwPassword
            , ref string strOS_MasterServer_Solution_WAUserCode
            , ref string strOS_MasterServer_Solution_WAUserPassword
            , ref string strOS_MasterServer_Solution_SysOSCode3A
            )
        {
            // GetInfo:
            strOS_MasterServer_PrdCenter_API_Url = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_API_Url"];
            strOS_MasterServer_PrdCenter_UserCode = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_UserCode"];
            strOS_MasterServer_PrdCenter_UserPassword = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_UserPassword"];
            strOS_MasterServer_PrdCenter_TokenID = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_TokenID"];
            strOS_MasterServer_PrdCenter_GwUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_GwUserCode"];
            strOS_MasterServer_PrdCenter_GwPassword = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_GwPassword"];
            strOS_MasterServer_PrdCenter_WAUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_WAUserCode"];
            strOS_MasterServer_PrdCenter_WAUserPassword = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_PrdCenter_WAUserPassword"];
            /////
            strOS_ProductCentrer_GwUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_ProductCentrer_GwUserCode"];
            strOS_ProductCentrer_GwPassword = System.Configuration.ConfigurationManager.AppSettings["OS_ProductCentrer_GwPassword"];
            strOS_ProductCentrer_WAUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_ProductCentrer_WAUserCode"];
            strOS_ProductCentrer_WAUserPassword = System.Configuration.ConfigurationManager.AppSettings["OS_ProductCentrer_WAUserPassword"];
            ////
            strOS_MasterServer_Solution_API_Url = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_API_Url"];
            strOS_MasterServer_Solution_TokenID = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_TokenID"];
            strOS_MasterServer_Solution_GwUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_GwUserCode"];
            strOS_MasterServer_Solution_GwPassword = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_GwPassword"];
            strOS_MasterServer_Solution_WAUserCode = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_WAUserCode"];
            strOS_MasterServer_Solution_WAUserPassword = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_WAUserPassword"];
            strOS_MasterServer_Solution_SysOSCode3A = System.Configuration.ConfigurationManager.AppSettings["OS_MasterServer_Solution_SysOSCode3A"];
            /////
        }
        #endregion

        #region // Common.WA:
        public DataSet WAS_Cm_GetDTime(
			ref ArrayList alParamsCoupleError
			, RQ_Common objRQ_Common
			////
			, out RT_Common objRT_Common
			)
		{
			#region // Temp:
			string strTid = objRQ_Common.Tid;
			objRT_Common = new RT_Common();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Common.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			//string strFunctionName = "WAS_Cm_GetDTime";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Cm_GetDTime;
			//alParamsCoupleError.AddRange(new object[]{
			//	"strFunctionName", strFunctionName
			//	, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//	, "Common", TJson.JsonConvert.SerializeObject(objRQ_Common.Common)
			//	//, "Lst_KUNN_ValLaiSuatChangeHist", TJson.JsonConvert.SerializeObject(objRQ_Common.Lst_KUNN_ValLaiSuatChangeHist)
			//	//, "Lst_KUNN_FileUpload", TJson.JsonConvert.SerializeObject(objRQ_Common.Lst_KUNN_FileUpload)
			//	////
			//	});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				////
				//DataSet dsData = new DataSet();
				//{
				//	////
				//	DataTable dt_KUNN_ValLaiSuatChangeHist = TUtils.DataTableCmUtils.ToDataTable<KUNN_ValLaiSuatChangeHist>(objRQ_Common.Lst_KUNN_ValLaiSuatChangeHist, "KUNN_ValLaiSuatChangeHist");
				//	dsData.Tables.Add(dt_KUNN_ValLaiSuatChangeHist);
				//	////
				//	DataTable dt_KUNN_FileUpload = TUtils.DataTableCmUtils.ToDataTable<KUNN_FileUpload>(objRQ_Common.Lst_KUNN_FileUpload, "KUNN_FileUpload");
				//	dsData.Tables.Add(dt_KUNN_FileUpload);
				//	////
				//	//DataTable dt_CommonDtl = TUtils.DataTableCmUtils.ToDataTable<CommonDtl>(objRQ_Common.Lst_CommonDtl, "CommonDtl");
				//	//dsData.Tables.Add(dt_CommonDtl);
				//}
				#endregion

				#region // Cm_GetDTime:
				mdsResult = Cm_GetDTime(
					objRQ_Common.Tid // strTid
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
