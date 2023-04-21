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
using idn.Skycic.Inventory.BizService.Services;
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
		#region // Inos:
		private void Inos_LicService_GetOrgSolutionModulesX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSolutionCode
			, object objOrgID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_GetOrgSolutionModulesX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
				, "objOrgID", objOrgID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
			long lgOrgID = Convert.ToInt64(objOrgID);
			DataTable dtDB_Mst_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_Org").Tables[0];
			//TUtils.CUtils.MyForceNewColumn(ref dtDB_Mst_Org, "OrgID", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dtDB_Mst_Org, "ModuleCode", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dtDB_Mst_Org, "Qty", typeof(object));

			////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				List<LicModule> lstLicModule = objLicService.GetOrgSolutionModules(strSolutionCode, lgOrgID);

				////
				if (lstLicModule != null)
				{
					foreach (var objItem in lstLicModule)
					{
						string strFN = "";
						DataRow drDB = dtDB_Mst_Org.NewRow();
						strFN = "OrgID"; drDB[strFN] = lgOrgID;
						strFN = "NetworkID"; drDB[strFN] = nNetworkID;
						strFN = "ModuleCode"; drDB[strFN] = objItem.Code;
						strFN = "Qty"; drDB[strFN] = objItem.Count;
						strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
						strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
						dtDB_Mst_Org.Rows.Add(drDB);
					}
				}

				dsData.Tables.Add(dtDB_Mst_Org.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_GetOrgSolutionModules
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_GetCurrentUserX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_GetCurrentUserX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			dsData = new DataSet();

			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);
				objAccountService.AccessToken = strAccessToken;
				DataTable dtDB_Sys_User = TDALUtils.DBUtils.GetSchema(_cf.db, "Sys_User").Tables[0];
				TUtils.CUtils.MyForceNewColumn(ref dtDB_Sys_User, "Id", typeof(object));

				alParamsCoupleError.AddRange(new object[]{
					"Check.strAccessToken", strAccessToken
					});

				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objInosEditProfileModel);
				var ret = objAccountService.GetCurrentUser();

				if (ret != null)
				{
					string strFN = "";
					DataRow drDB = dtDB_Sys_User.NewRow();
					strFN = "UserCode"; drDB[strFN] = ret.Email;
					strFN = "EMail"; drDB[strFN] = ret.Email;
					strFN = "Id"; drDB[strFN] = ret.Id;
					dtDB_Sys_User.Rows.Add(drDB);
				}

				dsData.Tables.Add(dtDB_Sys_User.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_GetCurrentUser
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_AccountService_GetUserX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objEmail
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_AccountService_GetCurrentUserX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objEmail", objEmail
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			dsData = new DataSet();

			#endregion

			#region // Call Service:
			try
			{
				////
				AccountService objAccountService = new AccountService(null);
				objAccountService.AccessToken = strAccessToken;
				DataTable dtDB_Sys_User = TDALUtils.DBUtils.GetSchema(_cf.db, "Sys_User").Tables[0];
				TUtils.CUtils.MyForceNewColumn(ref dtDB_Sys_User, "Id", typeof(object));

				InosUser objInosUser = new InosUser();
				objInosUser.Email = string.Format("{0}", objEmail).Trim();

				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objInosEditProfileModel);
				var ret = objAccountService.GetUser(objInosUser);

				if (ret != null)
				{
					string strFN = "";
					DataRow drDB = dtDB_Sys_User.NewRow();
					strFN = "UserCode"; drDB[strFN] = ret.Email;
					strFN = "EMail"; drDB[strFN] = ret.Email;
					strFN = "Id"; drDB[strFN] = ret.Id;
					dtDB_Sys_User.Rows.Add(drDB);
				}

				dsData.Tables.Add(dtDB_Sys_User.Copy());

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_AccountService_GetUser
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			#endregion
		}

		private void Inos_OrgService_AddInviteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objEmail
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_OrgService_AddInviteX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
			DataTable dtDB_OS_Inos_Org = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Org").Tables[0];

			////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				OrgService objOrgService = new OrgService(null);
				objOrgService.AccessToken = strAccessToken;

				OrgInvite orgInvite = new OrgInvite();
				orgInvite.Email = string.Format("{0}", objEmail).Trim();
				orgInvite.OrgId = Convert.ToInt64(objOrgID);

				var ret = objOrgService.AddInvite(orgInvite);

			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                if (!CmUtils.StringUtils.StringEqualIgnoreCase(myexc.ErrorMessage, "Email existed"))
                {
                    alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Inos_OrgService_AddInvite
                        , null
                        , alParamsCoupleError.ToArray()
                        ); 
                }
			}
			#endregion
		}
        
        public DataSet WAS_OS_Inos_OrgSolution_GetAndSave(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_OrgSolution objRQ_OS_Inos_OrgSolution
            ////
            , out RT_OS_Inos_OrgSolution objRT_OS_Inos_OrgSolution
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_OrgSolution.Tid;
            objRT_OS_Inos_OrgSolution = new RT_OS_Inos_OrgSolution();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_OrgSolution.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_OrgSolution_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_OrgSolution_GetAndSave;
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
                List<OS_Inos_OrgSolution> lst_OS_Inos_OrgSolution = new List<OS_Inos_OrgSolution>();
                List<OS_Inos_Modules> lst_OS_Inos_Modules = new List<OS_Inos_Modules>();
                #endregion

                #region // WS_OS_Inos_OrgSolution_Get:
                mdsResult = OS_Inos_OrgSolution_GetAndSave(
                    objRQ_OS_Inos_OrgSolution.Tid // strTid
                    , objRQ_OS_Inos_OrgSolution.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_OrgSolution.GwPassword // strGwPassword
                    , objRQ_OS_Inos_OrgSolution.WAUserCode // strUserCode
                    , objRQ_OS_Inos_OrgSolution.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_OrgSolution.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    , objRQ_OS_Inos_OrgSolution.Rt_Cols_OS_Inos_OrgSolution // strRt_Cols_OS_Inos_OrgSolution
                    , objRQ_OS_Inos_OrgSolution.Rt_Cols_OS_Inos_Modules // strRt_Cols_OS_Inos_Modules
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_OS_Inos_OrgSolution.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_OS_Inos_OrgSolution = mdsResult.Tables["OS_Inos_OrgSolution"].Copy();
                    lst_OS_Inos_OrgSolution = TUtils.DataTableCmUtils.ToListof<OS_Inos_OrgSolution>(dt_OS_Inos_OrgSolution);
                    objRT_OS_Inos_OrgSolution.Lst_OS_Inos_OrgSolution = lst_OS_Inos_OrgSolution;
                    ////
                    DataTable dt_OS_Inos_Modules = mdsResult.Tables["OS_Inos_Modules"].Copy();
                    lst_OS_Inos_Modules = TUtils.DataTableCmUtils.ToListof<OS_Inos_Modules>(dt_OS_Inos_Modules);
                    objRT_OS_Inos_OrgSolution.Lst_OS_Inos_Modules = lst_OS_Inos_Modules;
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

        public DataSet OS_Inos_OrgSolution_GetAndSave(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            , string strRt_Cols_OS_Inos_OrgSolution
            , string strRt_Cols_OS_Inos_Modules
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_Inos_OrgSolution_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_OrgSolution_GetAndSave;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_OrgSolution", strRt_Cols_OS_Inos_OrgSolution
                , "strRt_Cols_OS_Inos_Modules", strRt_Cols_OS_Inos_Modules
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                bool bGet_OS_Inos_OrgSolution = (strRt_Cols_OS_Inos_OrgSolution != null && strRt_Cols_OS_Inos_OrgSolution.Length > 0);
                bool bGet_OS_Inos_Modules = (strRt_Cols_OS_Inos_Modules != null && strRt_Cols_OS_Inos_Modules.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Sys_Solution: Get.
                ////
                DataTable dtDB_Sys_Solution = null;
                {
                    // GetInfo:
                    dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                        _cf.db // db
                        , "Sys_Solution" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                        );
                }
                #endregion

                #region // Mst_Org: Get.
                ////
                DataTable dtDB_Mst_Org = null;
                {
                    // GetInfo:
                    dtDB_Mst_Org = TDALUtils.DBUtils.GetTableContents(
                        _cf.db // db
                        , "Mst_Org" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "OrgParent", "=", "0" // arrobjParamsTriple item
                        );
                }
                #endregion

                #region // Get Data:
                DataSet dsGetData = null;
                
                if (bGet_OS_Inos_OrgSolution)
                {
                    // //
                    Inos_LicService_GetOrgSolutionAndSaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                        , dtDB_Mst_Org.Rows[0]["OrgID"] // objOrgID
                        , "0" // objPackageId
                             ////
                        , out dsGetData // dsData
                        );
                }
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
        private void Inos_LicService_GetOrgSolutionAndSaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objSolutionCode
            , object objOrgID
            , object objPackageId
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_LicService_GetOrgSolutionAndSaveX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
                , "objOrgID", objOrgID
                , "objPackageId", objPackageId
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            string strSolutionCode = string.Format("{0}", objSolutionCode).Trim();
            long lgOrgID = Convert.ToInt64(objOrgID);
            long lgPackageId = Convert.ToInt64(objPackageId);
            List<OS_Inos_OrgSolution> lst_OS_Inos_OrgSolution = new List<OS_Inos_OrgSolution>();
            List<OS_Inos_Modules> lst_OS_Inos_Modules = new List<OS_Inos_Modules>();
            DataTable dtDB_OS_Inos_OrgSolution = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_OrgSolution").Tables[0];
            DataTable dtDB_OS_Inos_Modules = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Modules").Tables[0];

            ////
            dsData = new DataSet();
            #endregion

            #region // Call Service:
            try
            {
                ////
                //AccountService objAccountService = new AccountService(null);

                //var state = objAccountService.RequestToken("QUYNHQUYNHABC1234@GMAIL.COM", "123456", new string[] { "test" });
                //strAccessToken = objAccountService.AccessToken;


                LicService objLicService = new LicService(null);
                objLicService.AccessToken = strAccessToken;

                List<OrgSolution> lstOrgSolutions = objLicService.GetOrgSolutions(lgPackageId, lgOrgID, strSolutionCode);

                //List<LicModule> lstLicModule = objLicService.GetOrgSolutionModules(strSolutionCode, lgOrgID);

                ////
                foreach (var objItem in lstOrgSolutions)
                {
                    #region // dtDB_OS_Inos_OrgSolution:
                    {
                        OS_Inos_OrgSolution objOS_Inos_OrgSolution = new OS_Inos_OrgSolution();
                        string strFN = "";
                        DataRow drDB = dtDB_OS_Inos_OrgSolution.NewRow();
                        strFN = "LicId"; drDB[strFN] = objItem.LicId;
                        strFN = "PackageId"; drDB[strFN] = objItem.PackageId;
                        strFN = "OrgId"; drDB[strFN] = objItem.OrgId;
                        strFN = "Name"; drDB[strFN] = objItem.Name;
                        strFN = "Domain"; drDB[strFN] = objItem.Domain;
                        strFN = "ImageUrl"; drDB[strFN] = objItem.ImageUrl;
                        strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                        //strFN = "ModulesCode"; drDB[strFN] = objItem.ModuleList;
                        dtDB_OS_Inos_OrgSolution.Rows.Add(drDB);
                        ////
                    }
                    #endregion

                    #region // dtDB_OS_Inos_Modules:
                    foreach (var objModuleList in objItem.ModuleList) 
                    {
                        OS_Inos_Modules objOS_Inos_Modules = new OS_Inos_Modules();
                        string strFN = "";
                        DataRow drDB = dtDB_OS_Inos_Modules.NewRow();
                        strFN = "LicId"; drDB[strFN] = objItem.LicId;
                        strFN = "PackageId"; drDB[strFN] = objItem.PackageId;
                        strFN = "OrgId"; drDB[strFN] = objItem.OrgId;
                        strFN = "ModulesCode"; drDB[strFN] = objModuleList.Code;
                        strFN = "ModulesCount"; drDB[strFN] = objModuleList.Count;
                        strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                        dtDB_OS_Inos_Modules.Rows.Add(drDB);

                    }
                    #endregion
                }

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_LicService_GetAllPackages
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            #region //Get and Save:
            {
                dsData.Tables.Add(dtDB_OS_Inos_OrgSolution.Copy());

                dsData.Tables.Add(dtDB_OS_Inos_Modules.Copy());

                #region // Save OS_Inos_OrgSolution:
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- #tbl_OS_Inos_OrgSolution:
                                select
	                                t.AutoId
                                into #tbl_OS_Inos_OrgSolution
                                from OS_Inos_OrgSolution t --//[mylock]
                                where (1=1)
	                                and t.OrgID = '@strOrgID'
                                ;
                                --- Delete:
                                ---- OS_Inos_OrgSolution:
                                delete t
                                from OS_Inos_OrgSolution t --//[mylock]
                                    inner join #tbl_OS_Inos_OrgSolution f --//[mylock]
                                        on t.AutoId = f.AutoId
                                where (1=1)
                                ;

						    "
                        , "@strOrgID", objOrgID
                        );
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //Save:
                _cf.db.SaveData(
                    "OS_Inos_OrgSolution"
                    , dtDB_OS_Inos_OrgSolution
                    //, alColumnEffective.ToArray()
                    );
                #endregion

                #region // Save OS_Inos_Modules:
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- #tbl_OS_Inos_Modules:
                                select
	                                t.AutoId
                                into #tbl_OS_Inos_Modules
                                from OS_Inos_Modules t --//[mylock]
                                where (1=1)
	                                and t.OrgID = '@strOrgID'
                                ;
                                --- Delete:
                                ---- OS_Inos_Modules:
                                delete t 
                                from OS_Inos_Modules t --//[mylock]
                                    inner join #tbl_OS_Inos_Modules f --//[mylock]
                                        on t.AutoId = f.AutoId
                                where (1=1)
                                ;

						    "
                        , "@strOrgID", objOrgID
                        );
                    DataSet dtDB = _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }
                // Save:
                _cf.db.SaveData(
                    "OS_Inos_Modules"
                    , dtDB_OS_Inos_Modules
                    //, alColumnEffective.ToArray()
                    );

                #endregion
            }
            #endregion

            #endregion
        }

        public DataSet WAS_OS_Inos_OrgLicense_GetAndSave(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_OrgLicense objRQ_OS_Inos_OrgLicense
            ////
            , out RT_OS_Inos_OrgLicense objRT_OS_Inos_OrgLicense
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_OrgLicense.Tid;
            objRT_OS_Inos_OrgLicense = new RT_OS_Inos_OrgLicense();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_OrgLicense.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_OrgLicense_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_OrgLicense_GetAndSave;
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
                List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
                List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
                #endregion

                #region // OS_Inos_OrgLicense_GetAndSave:
                mdsResult = OS_Inos_OrgLicense_GetAndSave(
                    objRQ_OS_Inos_OrgLicense.Tid // strTid
                    , objRQ_OS_Inos_OrgLicense.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_OrgLicense.GwPassword // strGwPassword
                    , objRQ_OS_Inos_OrgLicense.WAUserCode // strUserCode
                    , objRQ_OS_Inos_OrgLicense.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_OrgLicense.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    , objRQ_OS_Inos_OrgLicense.Rt_Cols_OS_Inos_OrgLicense // strRt_Cols_OS_Inos_OrgLicense
                    , objRQ_OS_Inos_OrgLicense.Rt_Cols_OS_Inos_Package // strRt_Cols_OS_Inos_Package
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_OS_Inos_OrgLicense.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_OS_Inos_OrgLicense = mdsResult.Tables["OS_Inos_OrgLicense"].Copy();
                    lst_OS_Inos_OrgLicense = TUtils.DataTableCmUtils.ToListof<OS_Inos_OrgLicense>(dt_OS_Inos_OrgLicense);
                    objRT_OS_Inos_OrgLicense.Lst_OS_Inos_OrgLicense = lst_OS_Inos_OrgLicense;
                    ////
                    DataTable dt_OS_Inos_Package = mdsResult.Tables["OS_Inos_Package"].Copy();
                    lst_OS_Inos_Package = TUtils.DataTableCmUtils.ToListof<OS_Inos_Package>(dt_OS_Inos_Package);
                    objRT_OS_Inos_OrgLicense.Lst_OS_Inos_Package = lst_OS_Inos_Package;
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

        public DataSet OS_Inos_OrgLicense_GetAndSave(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            , string strRt_Cols_OS_Inos_OrgLicense
            , string strRt_Cols_OS_Inos_Package
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_Inos_OrgLicense_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_OrgLicense_GetAndSave;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_OrgLicense", strRt_Cols_OS_Inos_OrgLicense
                , "strRt_Cols_OS_Inos_Package", strRt_Cols_OS_Inos_Package
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                bool bGet_OS_Inos_OrgLicense = (strRt_Cols_OS_Inos_OrgLicense != null && strRt_Cols_OS_Inos_OrgLicense.Length > 0);
                bool bGet_OS_Inos_Package = (strRt_Cols_OS_Inos_Package != null && strRt_Cols_OS_Inos_Package.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Mst_Org: Get.
                ////
                DataTable dtDB_Mst_Org = null;
                {
                    // GetInfo:
                    dtDB_Mst_Org = TDALUtils.DBUtils.GetTableContents(
                        _cf.db // db
                        , "Mst_Org" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "OrgParent", "=", "0" // arrobjParamsTriple item
                        );
                }
                #endregion

                #region // Get Data:
                DataSet dsGetData = null;

                if (bGet_OS_Inos_OrgLicense)
                {
                    // //
                    Inos_LicService_GetOrgLicenseAndSaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Mst_Org.Rows[0]["OrgID"] // objOrgID
                                                        ////
                        , out dsGetData // dsData
                        );
                }
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
        private void Inos_LicService_GetOrgLicenseAndSaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objOrgID
            ////
            , out DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_LicService_GetOrgLicenseAndSaveX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Refine and Check Input:
            ////
            long lgOrgID = Convert.ToInt64(objOrgID);
            List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
            List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
            DataTable dtDB_OS_Inos_OrgLicense = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_OrgLicense").Tables[0];
            DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

            ////
            dsData = new DataSet();
            #endregion

            #region // Call Service:
            try
            {
                ////
                //AccountService objAccountService = new AccountService(null);

                //var state = objAccountService.RequestToken("QUYNHQUYNHABC1234@GMAIL.COM", "123456", new string[] { "test" });
                //strAccessToken = objAccountService.AccessToken;


                LicService objLicService = new LicService(null);
                objLicService.AccessToken = strAccessToken;

                List<OrgLicense> lstOrgLicense = objLicService.GetOrgLicense(lgOrgID);

                ////
                foreach (var objItem in lstOrgLicense)
                {
                    #region // dtDB_OS_Inos_OrgLicense:
                    {
                        OS_Inos_OrgLicense objOS_Inos_OrgLicense = new OS_Inos_OrgLicense();
                        string strFN = "";
                        DataRow drDB = dtDB_OS_Inos_OrgLicense.NewRow();
                        strFN = "Id"; drDB[strFN] = objItem.Id;
                        strFN = "OrgId"; drDB[strFN] = objItem.OrgId;
                        strFN = "PackageId"; drDB[strFN] = objItem.PackageId;
                        strFN = "StartDate"; drDB[strFN] = objItem.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "EndDate"; drDB[strFN] = objItem.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "LicStatus"; drDB[strFN] = objItem.Status;
                        strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                        dtDB_OS_Inos_OrgLicense.Rows.Add(drDB);
                        ////
                    }
                    #endregion

                    #region // dtDB_OS_Inos_Package:
                    {
                        OS_Inos_Package objOS_Inos_Package = new OS_Inos_Package();
                        string strFN = "";
                        DataRow drDB = dtDB_OS_Inos_Package.NewRow();
                        strFN = "OrgId"; drDB[strFN] = objItem.OrgId;
                        strFN = "LicId"; drDB[strFN] = objItem.Id;
                        strFN = "Id"; drDB[strFN] = objItem.Package.Id;
                        strFN = "Name"; drDB[strFN] = objItem.Package.Name;
                        strFN = "LicenseType"; drDB[strFN] = objItem.Package.LicenseType;
                        strFN = "Subscription"; drDB[strFN] = objItem.Package.Subscription;
                        strFN = "Price"; drDB[strFN] = objItem.Package.Price;
                        strFN = "ImageUrl"; drDB[strFN] = objItem.Package.ImageUrl;
                        strFN = "IntroUrl"; drDB[strFN] = objItem.Package.IntroUrl;
                        strFN = "Detail"; drDB[strFN] = objItem.Package.Detail;
                        strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                        strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                        dtDB_OS_Inos_Package.Rows.Add(drDB);

                    }
                    #endregion
                }

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_LicService_GetAllPackages
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            ////
            #region // GetData and Save:
            ////
            dsData.Tables.Add(dtDB_OS_Inos_OrgLicense.Copy());

            dsData.Tables.Add(dtDB_OS_Inos_Package.Copy());

            #region // Save OS_Inos_OrgLicense:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_OS_Inos_OrgLicense:
                            select
	                            t.Id
                            into #tbl_OS_Inos_OrgLicense
                            from OS_Inos_OrgLicense t --//[mylock]
                            where (1=1)
	                            and t.OrgID = '@strOrgID'
                            ;
                            --- Delete:
                            ---- OS_Inos_OrgLicense:
                            delete t
                            from OS_Inos_OrgLicense t --//[mylock]
                                inner join #tbl_OS_Inos_OrgLicense f --//[mylock]
                                    on t.Id = f.Id
                            where (1=1)
                            ;

						"
                   , "@strOrgID", objOrgID
                   );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }

            //Save:
            _cf.db.SaveData(
                "OS_Inos_OrgLicense"
                , dtDB_OS_Inos_OrgLicense
                //, alColumnEffective.ToArray()
                );
            #endregion

            #region // Save OS_Inos_Package:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- #tbl_OS_Inos_Package:
                            select
	                            t.AutoId
                            into #tbl_OS_Inos_Package
                            from OS_Inos_Package t --//[mylock]
                            where (1=1)
	                            and t.OrgID = '@strOrgID'
                            ;
                            --- Delete:
                            ---- OS_Inos_Package:
                            delete t 
                            from OS_Inos_Package t --//[mylock]
                                inner join #tbl_OS_Inos_Package f --//[mylock]
                                    on t.AutoId = f.AutoId
                            where (1=1)
                            ;

						"
                    , "@strOrgID", objOrgID
                    );
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }
            // Save:
            _cf.db.SaveData(
                "OS_Inos_Package"
                , dtDB_OS_Inos_Package
                //, alColumnEffective.ToArray()
                );

            #endregion

            #endregion

            #endregion
        }

		private List<OrgLicense> Inos_LicService_GetOrgLicenseX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_GetOrgLicenseX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			long lgOrgID = Convert.ToInt64(objOrgID);
			List<OrgLicense> lstOrgLicense = null;
			//List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
			//List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
			//DataTable dtDB_OS_Inos_OrgLicense = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_OrgLicense").Tables[0];
			//DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

			//////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				//AccountService objAccountService = new AccountService(null);

				//var state = objAccountService.RequestToken("QUYNHQUYNHABC1234@GMAIL.COM", "123456", new string[] { "test" });
				//strAccessToken = objAccountService.AccessToken;


				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				lstOrgLicense = objLicService.GetOrgLicense(lgOrgID);

                var lstOrgLicenseStatus = lstOrgLicense.Where(x => x.Status == LicenseStatuses.Active).ToList();


                return lstOrgLicenseStatus;
			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_GetOrgLicense
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			#endregion
		}

		private Int32 Inos_LicService_AddOrgSolutionUsersX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSolutionCode
			, object objOrgID
			, List<OrgSolutionUser> lstOrgSolutionUser
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_AddOrgSolutionUsersX";
			//string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
				, "objOrgID", objOrgID
				, "lstOrgSolutionUser", TJson.JsonConvert.SerializeObject(lstOrgSolutionUser)
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strSolutionCode = Convert.ToString(objSolutionCode);
			long lgOrgID = Convert.ToInt64(objOrgID);
			Int32 result = 0;
			//List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
			//List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
			//DataTable dtDB_OS_Inos_OrgLicense = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_OrgLicense").Tables[0];
			//DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

			//////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				result = objLicService.AddOrgSolutionUsers(strSolutionCode, lgOrgID, lstOrgSolutionUser);

				return result;
			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_AddOrgSolutionUsers
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			#endregion
		}

		private OrgUser Inos_OrgServices_GetMyOrgUserList(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strOrgID
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			)
		{
			OrgService objOrgServices = new OrgService(null);
			objOrgServices.AccessToken = strAccessToken;
			long lOrgID = Convert.ToInt64(strOrgID);
			////
			List<OrgUser> lst_OrgUser = new List<OrgUser>();
			////
			lst_OrgUser = objOrgServices.GetMyOrgUserList();

			lst_OrgUser.FirstOrDefault(ou => ou.OrgId == lOrgID);

			OrgUser OrgUser = lst_OrgUser.FirstOrDefault(ou => ou.OrgId == lOrgID);

			return OrgUser;
		}

		private OrgUser Inos_OrgServices_GetAllOrgUser(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strOrgID
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, long lUserID
			)
		{
			OrgService objOrgServices = new OrgService(null);
			objOrgServices.AccessToken = strAccessToken;
			long lOrgID = Convert.ToInt64(strOrgID);
			////
			List<OrgUser> lst_OrgUser = new List<OrgUser>();
			////
			lst_OrgUser = objOrgServices.GetAllOrgUser(lOrgID, false);

			//lst_OrgUser.FirstOrDefault(ou => ou.OrgId == lOrgID);

			OrgUser OrgUser = lst_OrgUser.FirstOrDefault(ou => ou.OrgId == lOrgID && ou.UserId == lUserID);

			return OrgUser;
		}

		private Int32 Inos_LicService_DeleteOrgSolutionUsersX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSolutionCode
			, object objOrgID
			, List<OrgSolutionUser> lstOrgSolutionUser
			////
			, out DataSet dsData
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Inos_LicService_AddOrgSolutionUsersX";
			//string strErrorCodeDefault = TError.ErridQContract.Inos_AccountService_Register;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objSolutionCode", objSolutionCode
				, "objOrgID", objOrgID
				, "lstOrgSolutionUser", TJson.JsonConvert.SerializeObject(lstOrgSolutionUser)
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			////
			string strSolutionCode = Convert.ToString(objSolutionCode);
			long lgOrgID = Convert.ToInt64(objOrgID);
			Int32 result = 0;
			//List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
			//List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
			//DataTable dtDB_OS_Inos_OrgLicense = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_OrgLicense").Tables[0];
			//DataTable dtDB_OS_Inos_Package = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_Package").Tables[0];

			//////
			dsData = new DataSet();
			#endregion

			#region // Call Service:
			try
			{
				////
				LicService objLicService = new LicService(null);
				objLicService.AccessToken = strAccessToken;

				result = objLicService.DeleteOrgSolutionUsers(strSolutionCode, lgOrgID, lstOrgSolutionUser);

				return result;
			}
			catch (Exception exc)
			{
				mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

				alParamsCoupleError.AddRange(new object[]{
					"Check.InosExc.ErrorCode", myexc.ErrorCode
					, "Check.InosExc.ErrorDetail", myexc.ErrorDetail
					, "Check.InosExc.ErrorMessage", myexc.ErrorMessage
					, "Check.InosExc.InnerException", myexc.InnerException
					});

				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Inos_LicService_AddOrgSolutionUsers
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			#endregion
		}
		#endregion

		#region // OS_Inos_Org:
		public DataSet WAS_OS_Inos_Org_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_Org objRQ_OS_Inos_Org
            ////
            , out RT_OS_Inos_Org objRT_OS_Inos_Org
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_Org.Tid;
            objRT_OS_Inos_Org = new RT_OS_Inos_Org();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Org.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_Org_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_Org_Create;
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
                List<OS_Inos_Org> lst_OS_Inos_Org = new List<OS_Inos_Org>();
                List<iNOS_Mst_BizType> lst_iNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
                List<iNOS_Mst_BizField> lst_iNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
                #endregion

                #region // WS_OS_Inos_Org_Get:
                mdsResult = OS_Inos_Org_Create(
                    objRQ_OS_Inos_Org.Tid // strTid
                    , objRQ_OS_Inos_Org.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_Org.GwPassword // strGwPassword
                    , objRQ_OS_Inos_Org.WAUserCode // strUserCode
                    , objRQ_OS_Inos_Org.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_Org.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Return:
                    , objRQ_OS_Inos_Org.Rt_Cols_OS_Inos_Org // strRt_Cols_OS_Inos_Org
                    , objRQ_OS_Inos_Org.Rt_Cols_iNOS_Mst_BizType // strRt_Cols_iNOS_Mst_BizType
                    , objRQ_OS_Inos_Org.Rt_Cols_iNOS_Mst_BizField // strRt_Cols_iNOS_Mst_BizField
                    , objRQ_OS_Inos_Org.OS_Inos_Org.ParentId // objParentId
                    , objRQ_OS_Inos_Org.OS_Inos_Org.Name // objName
                    , objRQ_OS_Inos_Org.iNOS_Mst_BizType.BizType // objBizType
                    , objRQ_OS_Inos_Org.iNOS_Mst_BizType.BizTypeName // objBizTypeName
                    , objRQ_OS_Inos_Org.iNOS_Mst_BizField.BizFieldCode // objBizField
                    , objRQ_OS_Inos_Org.iNOS_Mst_BizField.BizFieldName // objBizFieldName
                    , objRQ_OS_Inos_Org.OS_Inos_Org.ContactName // objContactName
                    , objRQ_OS_Inos_Org.OS_Inos_Org.Email // objEmail
                    , objRQ_OS_Inos_Org.OS_Inos_Org.PhoneNo // objPhoneNo
                    , objRQ_OS_Inos_Org.OS_Inos_Org.Description // objDescription
                    , objRQ_OS_Inos_Org.OS_Inos_Org.Enable // objEnable
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_OS_Inos_Org.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_OS_Inos_Org = mdsResult.Tables["OS_Inos_Org"].Copy();
                    lst_OS_Inos_Org = TUtils.DataTableCmUtils.ToListof<OS_Inos_Org>(dt_OS_Inos_Org);
                    objRT_OS_Inos_Org.Lst_OS_Inos_Org = lst_OS_Inos_Org;
                    ////
                    DataTable dt_iNOS_Mst_BizType = mdsResult.Tables["iNOS_Mst_BizType"].Copy();
                    lst_iNOS_Mst_BizType = TUtils.DataTableCmUtils.ToListof<iNOS_Mst_BizType>(dt_iNOS_Mst_BizType);
                    objRT_OS_Inos_Org.Lst_iNOS_Mst_BizType = lst_iNOS_Mst_BizType;
                    ////
                    DataTable dt_iNOS_Mst_BizField = mdsResult.Tables["iNOS_Mst_BizField"].Copy();
                    lst_iNOS_Mst_BizField = TUtils.DataTableCmUtils.ToListof<iNOS_Mst_BizField>(dt_iNOS_Mst_BizField);
                    objRT_OS_Inos_Org.Lst_iNOS_Mst_BizField = lst_iNOS_Mst_BizField;
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

        public DataSet OS_Inos_Org_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Return:
            , string strRt_Cols_OS_Inos_Org
            , string strRt_Cols_iNOS_Mst_BizType
            , string strRt_Cols_iNOS_Mst_BizField
            ////
            , object objParentId
            , object objName
            , object objBizTypeId
            , object objBizTypeName
            , object objBizFieldId
            , object objBizFieldName
            ////
            , object objContactName
            , object objEmail
            , object objPhoneNo
            , object objDescription
            , object objEnable
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_Inos_Org_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_Inos_Org_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_OS_Inos_Org", strRt_Cols_OS_Inos_Org
                , "strRt_Cols_iNOS_Mst_BizType", strRt_Cols_iNOS_Mst_BizType
                , "strRt_Cols_iNOS_Mst_BizField", strRt_Cols_iNOS_Mst_BizField
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

                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                //// Refine:
                bool bGet_OS_Inos_Org = (strRt_Cols_OS_Inos_Org != null && strRt_Cols_OS_Inos_Org.Length > 0);
                bool bGet_iNOS_Mst_BizType = (strRt_Cols_OS_Inos_Org != null && strRt_Cols_iNOS_Mst_BizType.Length > 0);
                bool bGet_iNOS_Mst_BizField = (strRt_Cols_OS_Inos_Org != null && strRt_Cols_iNOS_Mst_BizField.Length > 0);
                
                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Get Data:
                DataSet dsGetData = null;

                if (bGet_OS_Inos_Org)
                {
                    ////
                    RptSv_Inos_OrgService_CreateOrgX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objParentId
                        , objName
                        , objBizTypeId
                        , objBizTypeName
                        , objBizFieldId
                        , objBizFieldName
                        ////
                        , objContactName
                        , objEmail
                        , objPhoneNo
                        , objDescription
                        , objEnable
                        ////
                        , out dsGetData // dsData
                        );
                }
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
        #endregion

        #region // OS_Inos_Discount:
        private Inos_DiscountCode Inos_OrderService_GetDiscountCodeX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objcode
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrderService_GetDiscountCodeX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objcode", objcode
                ////
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string code = Convert.ToString(objcode);
            Inos_DiscountCode objInos_DiscountCode = new Inos_DiscountCode();
            #endregion

            #region // Call Service:

            try
            {
                ////
                OrderService objOrderService = new OrderService(null);

                //object objJsonRQ = TJson.JsonConvert.SerializeObject(code);

                //alParamsCoupleError.AddRange(new object[]{
                //    "Check.InosCreateUserModel", objJsonRQ
                //    });

                var ret = objOrderService.GetDiscountCode(code);
                objInos_DiscountCode.Code = ret.Code;
                objInos_DiscountCode.RemainQty = ret.RemainQty;
                objInos_DiscountCode.DiscountType = new Inos_DiscountCodeTypes();
                objInos_DiscountCode.DiscountType = (Inos_DiscountCodeTypes)ret.DiscountType;
                objInos_DiscountCode.DiscountAmount = ret.DiscountAmount;
                objInos_DiscountCode.Description = ret.Description;
                objInos_DiscountCode.Enabled = ret.Enabled;
                objInos_DiscountCode.EffectDateFrom = ret.EffectDateFrom;
                objInos_DiscountCode.EffectDateTo = ret.EffectDateTo;
                return objInos_DiscountCode;
                
            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrderService_GetDiscountCode
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_OS_Inos_OrderService_GetDiscountCode(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_OrderService_GetDiscountCode";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_OrderService_GetDiscountCode;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_DiscountCode objInos_DiscountCode = Inos_OrderService_GetDiscountCodeX(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder.DiscountCode // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_DiscountCode != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_DiscountCode = objInos_DiscountCode;
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
       
        public DataSet WAS_RptSv_OS_Inos_OrderService_GetDiscountCode(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_OS_Inos_OrderService_GetDiscountCode";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_OS_Inos_OrderService_GetDiscountCode;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_DiscountCode objInos_DiscountCode = Inos_OrderService_GetDiscountCodeX(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder.DiscountCode // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_DiscountCode != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_DiscountCode = objInos_DiscountCode;
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

        #region // OS_Inos_Order:
        private Inos_LicOrder Inos_OrderService_CreateOrderX_New20190913(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , Inos_LicOrder objInos_LicOrder
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrderService_CreateOrderX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.objInos_LicOrder", TJson.JsonConvert.SerializeObject(objInos_LicOrder)
                ////
                });
            #endregion

            #region // Refine and Check Input:
            ////
            Inos_LicOrder objInos_LicOrderOut = new Inos_LicOrder();
            LicOrder objLicOrder = new LicOrder();
            {
                objLicOrder.Id = Convert.ToInt64(objInos_LicOrder.Id);
                objLicOrder.OrgId = Convert.ToInt64(objInos_LicOrder.OrgId);
                //objLicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objLicOrder.DiscountType = (Inos_DiscountCodeTypes)objLicOrder.DiscountType;
                objLicOrder.DiscountCode = Convert.ToString(objInos_LicOrder.DiscountCode);
                objLicOrder.TotalCost = Convert.ToDouble(objInos_LicOrder.TotalCost);
                objLicOrder.PaymentCode = Convert.ToString(objInos_LicOrder.PaymentCode);
                objLicOrder.PaymentStatusDesc = Convert.ToString(objInos_LicOrder.PaymentStatusDesc);
                objLicOrder.Status = new LicOrderStatuses();
                objLicOrder.Status = (LicOrderStatuses)objInos_LicOrder.Status;
                objLicOrder.CreateDTime = objInos_LicOrder.CreateDTime;
                objLicOrder.ApproveDTime = objInos_LicOrder.ApproveDTime;
                objLicOrder.CreateUserId = Convert.ToInt64(objInos_LicOrder.CreateUserId);
                objLicOrder.Remark = Convert.ToString(objInos_LicOrder.Remark);
                objLicOrder.DetailList = new List<LicOrderDetail>();
                foreach (var objItem in objInos_LicOrder.Inos_DetailList)
                {
                    var objLicOrderDetail = new LicOrderDetail();

                    //objLicOrderDetail.LicId = Convert.ToInt64(objItem.LicId);
                    objLicOrderDetail.PackageId = Convert.ToInt64(objItem.PackageId);
                    //objLicOrderDetail.OrderType = (LicOrderTypes)objItem.OrderType;
                    //objLicOrderDetail.Lic = new OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                    //objLicOrderDetail.Lic.Id = Convert.ToInt64(objItem.Lic.Id);
                    //objLicOrderDetail.Lic.OrgId = Convert.ToInt64(objItem.Lic.OrgId);
                    //objLicOrderDetail.Lic.PackageId = Convert.ToInt64(objItem.Lic.PackageId);
                    //objLicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                    //objLicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                    //objLicOrderDetail.Lic.Status = (LicenseStatuses)objItem.Lic.Status;
                    //objLicOrderDetail.Lic.Package = new InosPackage();
                    //objLicOrderDetail.Lic.Package.Id = Convert.ToInt64(objItem.Lic.Package.Id);
                    //objLicOrderDetail.Lic.Package.Name = Convert.ToString(objItem.Lic.Package.Name);
                    //objLicOrderDetail.Lic.Package.LicenseType = (LicTypes)objItem.Lic.Package.LicenseType;
                    //objLicOrderDetail.Lic.Package.Subscription = (LicSubscriptions)objItem.Lic.Package.Subscription;
                    //objLicOrderDetail.Lic.Package.Price = Convert.ToDouble(objItem.Lic.Package.Price);
                    //objLicOrderDetail.Lic.Package.ImageUrl = Convert.ToString(objItem.Lic.Package.ImageUrl);
                    //objLicOrderDetail.Lic.Package.IntroUrl = Convert.ToString(objItem.Lic.Package.IntroUrl);
                    //objLicOrderDetail.Lic.Package.Description = Convert.ToString(objItem.Lic.Package.Description);
                    //objLicOrderDetail.Lic.Package.Detail = Convert.ToString(objItem.Lic.Package.Detail);
                    objLicOrder.DetailList.Add(objLicOrderDetail);
                }

            }

            #endregion

            #region // Call Service:

            try
            {
                ////
                OrderService objOrderService = new OrderService(null);
                objOrderService.AccessToken = strAccessToken;

                object objJsonRQ = TJson.JsonConvert.SerializeObject(objLicOrder);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.objLicOrder", objJsonRQ
                    , "Check.strAccessToken", strAccessToken
                    });

                var ret = objOrderService.CreateOrder(objLicOrder);
                objInos_LicOrder.Id = ret.Id;
                objInos_LicOrder.OrgId = ret.OrgId;
                //objInos_LicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objInos_LicOrder.DiscountType = (Inos_DiscountCodeTypes)ret.DiscountType;
                objInos_LicOrder.DiscountCode = ret.DiscountCode;
                objInos_LicOrder.TotalCost = ret.TotalCost;
                objInos_LicOrder.PaymentCode = ret.PaymentCode;
                objInos_LicOrder.PaymentStatusDesc = ret.PaymentStatusDesc;
                objInos_LicOrder.Status = new Inos_LicOrderStatuses();
                objInos_LicOrder.Status = (Inos_LicOrderStatuses)ret.Status;
                objInos_LicOrder.CreateDTime = ret.CreateDTime;
                objInos_LicOrder.ApproveDTime = ret.ApproveDTime;
                objInos_LicOrder.CreateUserId = ret.CreateUserId;
                objInos_LicOrder.Remark = ret.Remark;
                objInos_LicOrder.Inos_DetailList = new List<Inos_LicOrderDetail>();
                if (ret.DetailList != null)
                {
                    foreach (var objItem in ret.DetailList)
                    {
                        var objInos_LicOrderDetail = new Inos_LicOrderDetail();

                        objInos_LicOrderDetail.LicId = objItem.LicId;
                        objInos_LicOrderDetail.PackageId = objItem.PackageId;
                        objInos_LicOrderDetail.OrderType = (Inos_LicOrderTypes)objItem.OrderType;
                        objInos_LicOrderDetail.Lic = new Inos_OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                        objInos_LicOrderDetail.Lic.Id = objItem.Lic.Id;
                        objInos_LicOrderDetail.Lic.OrgId = objItem.Lic.OrgId;
                        objInos_LicOrderDetail.Lic.PackageId = objItem.Lic.PackageId;
                        objInos_LicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                        objInos_LicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                        objInos_LicOrderDetail.Lic.Status = (Inos_LicenseStatuses)objItem.Lic.Status;
                        objInos_LicOrderDetail.Lic.Package = new Inos_Package();
                        objInos_LicOrderDetail.Lic.Package.Id = objItem.Lic.Package.Id;
                        objInos_LicOrderDetail.Lic.Package.Name = objItem.Lic.Package.Name;
                        objInos_LicOrderDetail.Lic.Package.LicenseType = (Inos_LicTypes)objItem.Lic.Package.LicenseType;
                        objInos_LicOrderDetail.Lic.Package.Subscription = (Inos_LicSubscriptions)objItem.Lic.Package.Subscription;
                        objInos_LicOrderDetail.Lic.Package.Price = objItem.Lic.Package.Price;
                        objInos_LicOrderDetail.Lic.Package.ImageUrl = objItem.Lic.Package.ImageUrl;
                        objInos_LicOrderDetail.Lic.Package.IntroUrl = objItem.Lic.Package.IntroUrl;
                        objInos_LicOrderDetail.Lic.Package.Description = objItem.Lic.Package.Description;
                        objInos_LicOrderDetail.Lic.Package.Detail = objItem.Lic.Package.Detail;
                    }
                }

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrderService_CreateOrder
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

            #region // UpdDB_MstSv_Inos_Org:
            {
                ////
                string strUpdDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
						---- MstSv_Inos_Org:
						update t 
						set
							t.OrderId = '@strOrderId'
						from MstSv_Inos_Org t --//[mylock]
						where (1=1)
							and t.Id = '@strId'
						;
					"
                    , "@strId", objInos_LicOrder.OrgId // Id => OrgID
                    , "@strOrderId", objInos_LicOrder.Id // OrderId => Id
                    );

                _cf.db.ExecQuery(
                    strUpdDB_MstSv_Inos_Org
                    );
            }
            #endregion

            return objInos_LicOrder;

        }

        private Inos_LicOrder Inos_OrderService_CreateOrderX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , Inos_LicOrder objInos_LicOrder
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrderService_CreateOrderX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.objInos_LicOrder", TJson.JsonConvert.SerializeObject(objInos_LicOrder)
                ////
                });
            #endregion

            #region // Refine and Check Input:
            ////
            Inos_LicOrder objInos_LicOrderOut = new Inos_LicOrder();
            LicOrder objLicOrder = new LicOrder();
            {
                objLicOrder.Id = Convert.ToInt64(objInos_LicOrder.Id);
                objLicOrder.OrgId = Convert.ToInt64(objInos_LicOrder.OrgId);
                //objLicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objLicOrder.DiscountType = (Inos_DiscountCodeTypes)objLicOrder.DiscountType;
                objLicOrder.DiscountCode = Convert.ToString(objInos_LicOrder.DiscountCode);
                objLicOrder.TotalCost = Convert.ToDouble(objInos_LicOrder.TotalCost);
                objLicOrder.PaymentCode = Convert.ToString(objInos_LicOrder.PaymentCode);
                objLicOrder.PaymentStatusDesc = Convert.ToString(objInos_LicOrder.PaymentStatusDesc);
                objLicOrder.Status = new LicOrderStatuses();
                objLicOrder.Status = (LicOrderStatuses)objInos_LicOrder.Status;
                objLicOrder.CreateDTime = objInos_LicOrder.CreateDTime;
                objLicOrder.ApproveDTime = objInos_LicOrder.ApproveDTime;
                objLicOrder.CreateUserId = Convert.ToInt64(objInos_LicOrder.CreateUserId);
                objLicOrder.Remark = Convert.ToString(objInos_LicOrder.Remark);
                objLicOrder.DetailList = new List<LicOrderDetail>();
                foreach (var objItem in objInos_LicOrder.Inos_DetailList)
                {
                    var objLicOrderDetail = new LicOrderDetail();

                    //objLicOrderDetail.LicId = Convert.ToInt64(objItem.LicId);
                    objLicOrderDetail.PackageId = Convert.ToInt64(objItem.PackageId);
                    //objLicOrderDetail.OrderType = (LicOrderTypes)objItem.OrderType;
                    //objLicOrderDetail.Lic = new OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                    //objLicOrderDetail.Lic.Id = Convert.ToInt64(objItem.Lic.Id);
                    //objLicOrderDetail.Lic.OrgId = Convert.ToInt64(objItem.Lic.OrgId);
                    //objLicOrderDetail.Lic.PackageId = Convert.ToInt64(objItem.Lic.PackageId);
                    //objLicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                    //objLicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                    //objLicOrderDetail.Lic.Status = (LicenseStatuses)objItem.Lic.Status;
                    //objLicOrderDetail.Lic.Package = new InosPackage();
                    //objLicOrderDetail.Lic.Package.Id = Convert.ToInt64(objItem.Lic.Package.Id);
                    //objLicOrderDetail.Lic.Package.Name = Convert.ToString(objItem.Lic.Package.Name);
                    //objLicOrderDetail.Lic.Package.LicenseType = (LicTypes)objItem.Lic.Package.LicenseType;
                    //objLicOrderDetail.Lic.Package.Subscription = (LicSubscriptions)objItem.Lic.Package.Subscription;
                    //objLicOrderDetail.Lic.Package.Price = Convert.ToDouble(objItem.Lic.Package.Price);
                    //objLicOrderDetail.Lic.Package.ImageUrl = Convert.ToString(objItem.Lic.Package.ImageUrl);
                    //objLicOrderDetail.Lic.Package.IntroUrl = Convert.ToString(objItem.Lic.Package.IntroUrl);
                    //objLicOrderDetail.Lic.Package.Description = Convert.ToString(objItem.Lic.Package.Description);
                    //objLicOrderDetail.Lic.Package.Detail = Convert.ToString(objItem.Lic.Package.Detail);
                    objLicOrder.DetailList.Add(objLicOrderDetail);
                }

            }

            #endregion

            #region // Call Service:

            try
            {
                ////
                OrderService objOrderService = new OrderService(null);
                objOrderService.AccessToken = strAccessToken;

                object objJsonRQ = TJson.JsonConvert.SerializeObject(objLicOrder);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.objLicOrder", objJsonRQ
                    , "Check.strAccessToken", strAccessToken
                    });

                var ret = objOrderService.CreateOrder(objLicOrder);
                objInos_LicOrder.Id = ret.Id;
                objInos_LicOrder.OrgId = ret.OrgId;
                //objInos_LicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objInos_LicOrder.DiscountType = (Inos_DiscountCodeTypes)ret.DiscountType;
                objInos_LicOrder.DiscountCode = ret.DiscountCode;
                objInos_LicOrder.TotalCost = ret.TotalCost;
                objInos_LicOrder.PaymentCode = ret.PaymentCode;
                objInos_LicOrder.PaymentStatusDesc = ret.PaymentStatusDesc;
                objInos_LicOrder.Status = new Inos_LicOrderStatuses();
                objInos_LicOrder.Status = (Inos_LicOrderStatuses)ret.Status;
                objInos_LicOrder.CreateDTime = ret.CreateDTime;
                objInos_LicOrder.ApproveDTime = ret.ApproveDTime;
                objInos_LicOrder.CreateUserId = ret.CreateUserId;
                objInos_LicOrder.Remark = ret.Remark;
                objInos_LicOrder.Inos_DetailList = new List<Inos_LicOrderDetail>();
                if (ret.DetailList != null)
                {
                    foreach (var objItem in ret.DetailList)
                    {
                        var objInos_LicOrderDetail = new Inos_LicOrderDetail();

                        objInos_LicOrderDetail.LicId = objItem.LicId;
                        objInos_LicOrderDetail.PackageId = objItem.PackageId;
                        objInos_LicOrderDetail.OrderType = (Inos_LicOrderTypes)objItem.OrderType;
                        objInos_LicOrderDetail.Lic = new Inos_OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                        objInos_LicOrderDetail.Lic.Id = objItem.Lic.Id;
                        objInos_LicOrderDetail.Lic.OrgId = objItem.Lic.OrgId;
                        objInos_LicOrderDetail.Lic.PackageId = objItem.Lic.PackageId;
                        objInos_LicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                        objInos_LicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                        objInos_LicOrderDetail.Lic.Status = (Inos_LicenseStatuses)objItem.Lic.Status;
                        objInos_LicOrderDetail.Lic.Package = new Inos_Package();
                        objInos_LicOrderDetail.Lic.Package.Id = objItem.Lic.Package.Id;
                        objInos_LicOrderDetail.Lic.Package.Name = objItem.Lic.Package.Name;
                        objInos_LicOrderDetail.Lic.Package.LicenseType = (Inos_LicTypes)objItem.Lic.Package.LicenseType;
                        objInos_LicOrderDetail.Lic.Package.Subscription = (Inos_LicSubscriptions)objItem.Lic.Package.Subscription;
                        objInos_LicOrderDetail.Lic.Package.Price = objItem.Lic.Package.Price;
                        objInos_LicOrderDetail.Lic.Package.ImageUrl = objItem.Lic.Package.ImageUrl;
                        objInos_LicOrderDetail.Lic.Package.IntroUrl = objItem.Lic.Package.IntroUrl;
                        objInos_LicOrderDetail.Lic.Package.Description = objItem.Lic.Package.Description;
                        objInos_LicOrderDetail.Lic.Package.Detail = objItem.Lic.Package.Detail;
                    }
                }                

                return objInos_LicOrder;

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrderService_CreateOrder
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        private Inos_LicOrder Inos_OrderService_PrecheckOrderX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , Inos_LicOrder objInos_LicOrder
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrderService_PrecheckOrderX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.objInos_LicOrder", TJson.JsonConvert.SerializeObject(objInos_LicOrder)
                ////
                });
            #endregion

            #region // Refine and Check Input:
            ////
            Inos_LicOrder objInos_LicOrderOut = new Inos_LicOrder();
            LicOrder objLicOrder = new LicOrder();
            {
                objLicOrder.Id = Convert.ToInt64(objInos_LicOrder.Id);
                objLicOrder.OrgId = Convert.ToInt64(objInos_LicOrder.OrgId);
                //objLicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objLicOrder.DiscountType = (Inos_DiscountCodeTypes)objLicOrder.DiscountType;
                objLicOrder.DiscountCode = Convert.ToString(objInos_LicOrder.DiscountCode);
                objLicOrder.TotalCost = Convert.ToDouble(objInos_LicOrder.TotalCost);
                objLicOrder.PaymentCode = Convert.ToString(objInos_LicOrder.PaymentCode);
                objLicOrder.PaymentStatusDesc = Convert.ToString(objInos_LicOrder.PaymentStatusDesc);
                objLicOrder.Status = new LicOrderStatuses();
                objLicOrder.Status = (LicOrderStatuses)objInos_LicOrder.Status;
                objLicOrder.CreateDTime = objInos_LicOrder.CreateDTime;
                objLicOrder.ApproveDTime = objInos_LicOrder.ApproveDTime;
                objLicOrder.CreateUserId = Convert.ToInt64(objInos_LicOrder.CreateUserId);
                objLicOrder.Remark = Convert.ToString(objInos_LicOrder.Remark);
                objLicOrder.DetailList = new List<LicOrderDetail>();
                foreach (var objItem in objInos_LicOrder.Inos_DetailList)
                {
                    var objLicOrderDetail = new LicOrderDetail();

                    //objLicOrderDetail.LicId = Convert.ToInt64(objItem.LicId);
                    objLicOrderDetail.PackageId = Convert.ToInt64(objItem.PackageId);
                    //objLicOrderDetail.OrderType = (LicOrderTypes)objItem.OrderType;
                    //objLicOrderDetail.Lic = new OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                    //objLicOrderDetail.Lic.Id = Convert.ToInt64(objItem.Lic.Id);
                    //objLicOrderDetail.Lic.OrgId = Convert.ToInt64(objItem.Lic.OrgId);
                    //objLicOrderDetail.Lic.PackageId = Convert.ToInt64(objItem.Lic.PackageId);
                    //objLicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                    //objLicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                    //objLicOrderDetail.Lic.Status = (LicenseStatuses)objItem.Lic.Status;
                    //objLicOrderDetail.Lic.Package = new InosPackage();
                    //objLicOrderDetail.Lic.Package.Id = Convert.ToInt64(objItem.Lic.Package.Id);
                    //objLicOrderDetail.Lic.Package.Name = Convert.ToString(objItem.Lic.Package.Name);
                    //objLicOrderDetail.Lic.Package.LicenseType = (LicTypes)objItem.Lic.Package.LicenseType;
                    //objLicOrderDetail.Lic.Package.Subscription = (LicSubscriptions)objItem.Lic.Package.Subscription;
                    //objLicOrderDetail.Lic.Package.Price = Convert.ToDouble(objItem.Lic.Package.Price);
                    //objLicOrderDetail.Lic.Package.ImageUrl = Convert.ToString(objItem.Lic.Package.ImageUrl);
                    //objLicOrderDetail.Lic.Package.IntroUrl = Convert.ToString(objItem.Lic.Package.IntroUrl);
                    //objLicOrderDetail.Lic.Package.Description = Convert.ToString(objItem.Lic.Package.Description);
                    //objLicOrderDetail.Lic.Package.Detail = Convert.ToString(objItem.Lic.Package.Detail);
                    objLicOrder.DetailList.Add(objLicOrderDetail);
                }

            }

            #endregion

            #region // Call Service:

            try
            {
                ////
                OrderService objOrderService = new OrderService(null);
                //objOrderService.AccessToken = strAccessToken;

                object objJsonRQ = TJson.JsonConvert.SerializeObject(objLicOrder);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.objLicOrder", objJsonRQ
                    , "Check.strAccessToken", strAccessToken
                    });

                var ret = objOrderService.PrecheckOrder(objLicOrder);
                objInos_LicOrder.Id = ret.Id;
                objInos_LicOrder.OrgId = ret.OrgId;
                //objInos_LicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objInos_LicOrder.DiscountType = (Inos_DiscountCodeTypes)ret.DiscountType;
                objInos_LicOrder.DiscountCode = ret.DiscountCode;
                objInos_LicOrder.TotalCost = ret.TotalCost;
                objInos_LicOrder.PaymentCode = ret.PaymentCode;
                objInos_LicOrder.PaymentStatusDesc = ret.PaymentStatusDesc;
                objInos_LicOrder.Status = new Inos_LicOrderStatuses();
                objInos_LicOrder.Status = (Inos_LicOrderStatuses)ret.Status;
                objInos_LicOrder.CreateDTime = ret.CreateDTime;
                objInos_LicOrder.ApproveDTime = ret.ApproveDTime;
                objInos_LicOrder.CreateUserId = ret.CreateUserId;
                objInos_LicOrder.Remark = ret.Remark;
                objInos_LicOrder.Inos_DetailList = new List<Inos_LicOrderDetail>();
                if (ret.DetailList != null)
                {
                    foreach (var objItem in ret.DetailList)
                    {
                        var objInos_LicOrderDetail = new Inos_LicOrderDetail();

                        objInos_LicOrderDetail.LicId = objItem.LicId;
                        objInos_LicOrderDetail.PackageId = objItem.PackageId;
                        objInos_LicOrderDetail.OrderType = (Inos_LicOrderTypes)objItem.OrderType;
                        objInos_LicOrderDetail.Lic = new Inos_OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                        objInos_LicOrderDetail.Lic.Id = objItem.Lic.Id;
                        objInos_LicOrderDetail.Lic.OrgId = objItem.Lic.OrgId;
                        objInos_LicOrderDetail.Lic.PackageId = objItem.Lic.PackageId;
                        objInos_LicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                        objInos_LicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                        objInos_LicOrderDetail.Lic.Status = (Inos_LicenseStatuses)objItem.Lic.Status;
                        objInos_LicOrderDetail.Lic.Package = new Inos_Package();
                        objInos_LicOrderDetail.Lic.Package.Id = objItem.Lic.Package.Id;
                        objInos_LicOrderDetail.Lic.Package.Name = objItem.Lic.Package.Name;
                        objInos_LicOrderDetail.Lic.Package.LicenseType = (Inos_LicTypes)objItem.Lic.Package.LicenseType;
                        objInos_LicOrderDetail.Lic.Package.Subscription = (Inos_LicSubscriptions)objItem.Lic.Package.Subscription;
                        objInos_LicOrderDetail.Lic.Package.Price = objItem.Lic.Package.Price;
                        objInos_LicOrderDetail.Lic.Package.ImageUrl = objItem.Lic.Package.ImageUrl;
                        objInos_LicOrderDetail.Lic.Package.IntroUrl = objItem.Lic.Package.IntroUrl;
                        objInos_LicOrderDetail.Lic.Package.Description = objItem.Lic.Package.Description;
                        objInos_LicOrderDetail.Lic.Package.Detail = objItem.Lic.Package.Detail;
                    }
                }

                return objInos_LicOrder;

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrderService_CreateOrder
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_OS_Inos_OrderService_CreateOrder(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_OrderService_CreateOrder";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_OrderService_CreateOrder;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_LicOrder objInos_LicOrder = Inos_OrderService_CreateOrderX(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToke
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_LicOrder != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_LicOrder = objInos_LicOrder;
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

        private Inos_LicOrder Inos_OrderService_CheckOrderStatusX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , Inos_LicOrder objInos_LicOrder
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Inos_OrderService_CheckOrderStatusX";
            //string strErrorCodeDefault = TError.ErridnInventory.Inos_AccountService_Register;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Check.objInos_LicOrder", TJson.JsonConvert.SerializeObject(objInos_LicOrder)
                ////
                });
            #endregion

            #region // Refine and Check Input:
            ////
            Inos_LicOrder objInos_LicOrderOut = new Inos_LicOrder();
            LicOrder objLicOrder = new LicOrder();
            {
                objLicOrder.Id = Convert.ToInt64(objInos_LicOrder.Id);
                //objLicOrder.OrgId = Convert.ToInt64(objInos_LicOrder.OrgId);
                ////objLicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                ////objLicOrder.DiscountType = (Inos_DiscountCodeTypes)objLicOrder.DiscountType;
                //objLicOrder.DiscountCode = Convert.ToString(objInos_LicOrder.DiscountCode);
                //objLicOrder.TotalCost = Convert.ToDouble(objInos_LicOrder.TotalCost);
                //objLicOrder.PaymentCode = Convert.ToString(objInos_LicOrder.PaymentCode);
                //objLicOrder.PaymentStatusDesc = Convert.ToString(objInos_LicOrder.PaymentStatusDesc);
                //objLicOrder.Status = new LicOrderStatuses();
                //objLicOrder.Status = (LicOrderStatuses)objInos_LicOrder.Status;
                //objLicOrder.CreateDTime = objInos_LicOrder.CreateDTime;
                //objLicOrder.ApproveDTime = objInos_LicOrder.ApproveDTime;
                //objLicOrder.CreateUserId = Convert.ToInt64(objInos_LicOrder.CreateUserId);
                //objLicOrder.Remark = Convert.ToString(objInos_LicOrder.Remark);
                //objLicOrder.DetailList = new List<LicOrderDetail>();
                //foreach (var objItem in objInos_LicOrder.Inos_DetailList)
                //{
                //    var objLicOrderDetail = new LicOrderDetail();

                //    //objLicOrderDetail.LicId = Convert.ToInt64(objItem.LicId);
                //    objLicOrderDetail.PackageId = Convert.ToInt64(objItem.PackageId);
                //    //objLicOrderDetail.OrderType = (LicOrderTypes)objItem.OrderType;
                //    //objLicOrderDetail.Lic = new OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                //    //objLicOrderDetail.Lic.Id = Convert.ToInt64(objItem.Lic.Id);
                //    //objLicOrderDetail.Lic.OrgId = Convert.ToInt64(objItem.Lic.OrgId);
                //    //objLicOrderDetail.Lic.PackageId = Convert.ToInt64(objItem.Lic.PackageId);
                //    //objLicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                //    //objLicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                //    //objLicOrderDetail.Lic.Status = (LicenseStatuses)objItem.Lic.Status;
                //    //objLicOrderDetail.Lic.Package = new InosPackage();
                //    //objLicOrderDetail.Lic.Package.Id = Convert.ToInt64(objItem.Lic.Package.Id);
                //    //objLicOrderDetail.Lic.Package.Name = Convert.ToString(objItem.Lic.Package.Name);
                //    //objLicOrderDetail.Lic.Package.LicenseType = (LicTypes)objItem.Lic.Package.LicenseType;
                //    //objLicOrderDetail.Lic.Package.Subscription = (LicSubscriptions)objItem.Lic.Package.Subscription;
                //    //objLicOrderDetail.Lic.Package.Price = Convert.ToDouble(objItem.Lic.Package.Price);
                //    //objLicOrderDetail.Lic.Package.ImageUrl = Convert.ToString(objItem.Lic.Package.ImageUrl);
                //    //objLicOrderDetail.Lic.Package.IntroUrl = Convert.ToString(objItem.Lic.Package.IntroUrl);
                //    //objLicOrderDetail.Lic.Package.Description = Convert.ToString(objItem.Lic.Package.Description);
                //    //objLicOrderDetail.Lic.Package.Detail = Convert.ToString(objItem.Lic.Package.Detail);
                //    objLicOrder.DetailList.Add(objLicOrderDetail);
                //}

            }

            #endregion

            #region // Call Service:

            try
            {
                ////
                OrderService objOrderService = new OrderService(null);
                objOrderService.AccessToken = strAccessToken;

                object objJsonRQ = TJson.JsonConvert.SerializeObject(objLicOrder);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.objLicOrder", objJsonRQ
                    , "Check.strAccessToken", strAccessToken
                    });

                var ret = objOrderService.CheckOrderStatus(objLicOrder);
                objInos_LicOrder.Id = ret.Id;
                objInos_LicOrder.OrgId = ret.OrgId;
                //objInos_LicOrder.DiscountCode = new Inos_DiscountCodeTypes();
                //objInos_LicOrder.DiscountType = (Inos_DiscountCodeTypes)ret.DiscountType;
                objInos_LicOrder.DiscountCode = ret.DiscountCode;
                objInos_LicOrder.TotalCost = ret.TotalCost;
                objInos_LicOrder.PaymentCode = ret.PaymentCode;
                objInos_LicOrder.PaymentStatusDesc = ret.PaymentStatusDesc;
                objInos_LicOrder.Status = new Inos_LicOrderStatuses();
                objInos_LicOrder.Status = (Inos_LicOrderStatuses)ret.Status;
                objInos_LicOrder.CreateDTime = ret.CreateDTime;
                objInos_LicOrder.ApproveDTime = ret.ApproveDTime;
                objInos_LicOrder.CreateUserId = ret.CreateUserId;
                objInos_LicOrder.Remark = ret.Remark;
                objInos_LicOrder.Inos_DetailList = new List<Inos_LicOrderDetail>();
                if (ret.DetailList != null)
                {
                    foreach (var objItem in ret.DetailList)
                    {
                        var objInos_LicOrderDetail = new Inos_LicOrderDetail();

                        objInos_LicOrderDetail.LicId = objItem.LicId;
                        objInos_LicOrderDetail.PackageId = objItem.PackageId;
                        objInos_LicOrderDetail.OrderType = (Inos_LicOrderTypes)objItem.OrderType;
                        objInos_LicOrderDetail.Lic = new Inos_OrgLicense(); // (Inos_OrgLicense)objItem.Lic;
                        objInos_LicOrderDetail.Lic.Id = objItem.Lic.Id;
                        objInos_LicOrderDetail.Lic.OrgId = objItem.Lic.OrgId;
                        objInos_LicOrderDetail.Lic.PackageId = objItem.Lic.PackageId;
                        objInos_LicOrderDetail.Lic.StartDate = objItem.Lic.StartDate;
                        objInos_LicOrderDetail.Lic.EndDate = objItem.Lic.EndDate;
                        objInos_LicOrderDetail.Lic.Status = (Inos_LicenseStatuses)objItem.Lic.Status;
                        objInos_LicOrderDetail.Lic.Package = new Inos_Package();
                        objInos_LicOrderDetail.Lic.Package.Id = objItem.Lic.Package.Id;
                        objInos_LicOrderDetail.Lic.Package.Name = objItem.Lic.Package.Name;
                        objInos_LicOrderDetail.Lic.Package.LicenseType = (Inos_LicTypes)objItem.Lic.Package.LicenseType;
                        objInos_LicOrderDetail.Lic.Package.Subscription = (Inos_LicSubscriptions)objItem.Lic.Package.Subscription;
                        objInos_LicOrderDetail.Lic.Package.Price = objItem.Lic.Package.Price;
                        objInos_LicOrderDetail.Lic.Package.ImageUrl = objItem.Lic.Package.ImageUrl;
                        objInos_LicOrderDetail.Lic.Package.IntroUrl = objItem.Lic.Package.IntroUrl;
                        objInos_LicOrderDetail.Lic.Package.Description = objItem.Lic.Package.Description;
                        objInos_LicOrderDetail.Lic.Package.Detail = objItem.Lic.Package.Detail;
                    }
                }

                return objInos_LicOrder;

            }
            catch (Exception exc)
            {
                mbiz.core.Exceptions.ServiceException myexc = new mbiz.core.Exceptions.ServiceException(exc);

                alParamsCoupleError.AddRange(new object[]{
                    "Check.InosExc.ErrorCode", myexc.ErrorCode
                    , "Check.InosExc.ErrorDetail", myexc.ErrorDetail
                    , "Check.InosExc.ErrorMessage", myexc.ErrorMessage
                    , "Check.InosExc.InnerException", myexc.InnerException
                    });

                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Inos_OrderService_CheckOrderStatus
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_OS_Inos_OrderService_CheckOrderStatus(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_Inos_OrderService_CheckOrderStatus";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_Inos_OrderService_CheckOrderStatus;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_LicOrder objInos_LicOrder = Inos_OrderService_CheckOrderStatusX(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToke
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_LicOrder != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_LicOrder = objInos_LicOrder;
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

        public DataSet WAS_RptSv_OS_Inos_OrderService_CreateOrder(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_OS_Inos_OrderService_CreateOrder";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_OS_Inos_OrderService_CreateOrder;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_LicOrder objInos_LicOrder = Inos_OrderService_CreateOrderX_New20190913(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToke
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_LicOrder != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_LicOrder = objInos_LicOrder;
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
        
        public DataSet WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus(
            ref ArrayList alParamsCoupleError
            , RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder
            ////
            , out RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_Inos_LicOrder.Tid;
            objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:                
                //// Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_OS_Inos_LicOrder.WAUserCode
                //    , objRQ_OS_Inos_LicOrder.WAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WAS_OS_Inos_OrderService_GetDiscountCode:
                Inos_LicOrder objInos_LicOrder = Inos_OrderService_CheckOrderStatusX(
                    objRQ_OS_Inos_LicOrder.Tid // strTid
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    , objRQ_OS_Inos_LicOrder.WAUserCode // strUserCode
                    , objRQ_OS_Inos_LicOrder.WAUserPassword // strUserPassword
                    , objRQ_OS_Inos_LicOrder.AccessToken // strAccessToke
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , objRQ_OS_Inos_LicOrder.Inos_LicOrder // strRt_Cols_OS_Inos_LicOrder
                    );
                #endregion

                #region // GetData:
                ////
                if (objInos_LicOrder != null)
                {
                    ////
                    objRT_OS_Inos_LicOrder.Inos_LicOrder = objInos_LicOrder;
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

        #region // Invoice_license:
        public DataSet WAS_Invoice_licenseCreHist_GetAndSave(
            ref ArrayList alParamsCoupleError
            , RQ_Invoice_licenseCreHist objRQ_Invoice_licenseCreHist
            ////
            , out RT_Invoice_licenseCreHist objRT_Invoice_licenseCreHist
            )
        {
            #region // Temp:
            string strTid = objRQ_Invoice_licenseCreHist.Tid;
            objRT_Invoice_licenseCreHist = new RT_Invoice_licenseCreHist();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_licenseCreHist.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Invoice_licenseCreHist_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Invoice_licenseCreHist_GetAndSave;
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
                List<OS_Inos_OrgLicense> lst_OS_Inos_OrgLicense = new List<OS_Inos_OrgLicense>();
                List<OS_Inos_Package> lst_OS_Inos_Package = new List<OS_Inos_Package>();
                ////
                List<OS_Inos_OrgSolution> lst_OS_Inos_OrgSolution = new List<OS_Inos_OrgSolution>();
                List<OS_Inos_Modules> lst_OS_Inos_Modules = new List<OS_Inos_Modules>();
                ////
                List<Invoice_licenseCreHist> lst_Invoice_licenseCreHist = new List<Invoice_licenseCreHist>();
                #endregion

                #region // WS_Invoice_licenseCreHist_Get:
                mdsResult = Invoice_licenseCreHist_GetAndSave(
                    objRQ_Invoice_licenseCreHist.Tid // strTid
                    , objRQ_Invoice_licenseCreHist.GwUserCode // strGwUserCode
                    , objRQ_Invoice_licenseCreHist.GwPassword // strGwPassword
                    , objRQ_Invoice_licenseCreHist.WAUserCode // strUserCode
                    , objRQ_Invoice_licenseCreHist.WAUserPassword // strUserPassword
                    , objRQ_Invoice_licenseCreHist.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    //// Filter:
                    , objRQ_Invoice_licenseCreHist.Ft_RecordStart
                    , objRQ_Invoice_licenseCreHist.Ft_RecordCount
                    , objRQ_Invoice_licenseCreHist.Ft_WhereClause
                    //// Return:
                    , objRQ_Invoice_licenseCreHist.Rt_Cols_Invoice_licenseCreHist // strRt_Cols_Invoice_licenseCreHist
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    //////
                    //DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    //lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    //objRT_Invoice_licenseCreHist.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    //DataTable dt_Invoice_licenseCreHist = mdsResult.Tables["Invoice_licenseCreHist"].Copy();
                    //lst_Invoice_licenseCreHist = TUtils.DataTableCmUtils.ToListof<Invoice_licenseCreHist>(dt_Invoice_licenseCreHist);
                    //objRT_Invoice_licenseCreHist.Lst_Invoice_licenseCreHist = lst_Invoice_licenseCreHist;
                    //////
                    //DataTable dt_OS_Inos_Package = mdsResult.Tables["OS_Inos_Package"].Copy();
                    //lst_OS_Inos_Package = TUtils.DataTableCmUtils.ToListof<OS_Inos_Package>(dt_OS_Inos_Package);
                    //objRT_Invoice_licenseCreHist.Lst_OS_Inos_Package = lst_OS_Inos_Package;

                    //////
                    //DataTable dt_OS_Inos_OrgSolution = mdsResult.Tables["OS_Inos_OrgSolution"].Copy();
                    //lst_OS_Inos_OrgSolution = TUtils.DataTableCmUtils.ToListof<OS_Inos_OrgSolution>(dt_OS_Inos_OrgSolution);
                    //objRT_Invoice_licenseCreHist.Lst_OS_Inos_OrgSolution = lst_OS_Inos_OrgSolution;
                    //////
                    //DataTable dt_OS_Inos_Modules = mdsResult.Tables["OS_Inos_Modules"].Copy();
                    //lst_OS_Inos_Modules = TUtils.DataTableCmUtils.ToListof<OS_Inos_Modules>(dt_OS_Inos_Modules);
                    //objRT_Invoice_licenseCreHist.Lst_OS_Inos_Modules = lst_OS_Inos_Modules;
                    ////
                    DataTable dt_Invoice_licenseCreHist = mdsResult.Tables["Invoice_licenseCreHist"].Copy();
                    lst_Invoice_licenseCreHist = TUtils.DataTableCmUtils.ToListof<Invoice_licenseCreHist>(dt_Invoice_licenseCreHist);
                    objRT_Invoice_licenseCreHist.Lst_Invoice_licenseCreHist = lst_Invoice_licenseCreHist;
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

        public DataSet Invoice_licenseCreHist_GetAndSave(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            ////
            , string strRt_Cols_Invoice_licenseCreHist
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Invoice_licenseCreHist_GetAndSave";
            string strErrorCodeDefault = TError.ErridnInventory.Invoice_licenseCreHist_GetAndSave;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Return
				, "strRt_Cols_Invoice_licenseCreHist", strRt_Cols_Invoice_licenseCreHist
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                //// Refine:
                bool bGet_Invoice_licenseCreHist = (strRt_Cols_Invoice_licenseCreHist != null && strRt_Cols_Invoice_licenseCreHist.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Sys_Solution: Get.
                ////
                DataTable dtDB_Sys_Solution = null;
                {
                    // GetInfo:
                    dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                        _cf.db // db
                        , "Sys_Solution" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                        );
                }
                #endregion

                #region // Mst_Org: Get.
                ////
                DataTable dtDB_Sys_User = null;
                {
                    Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strWAUserCode // strWAUserCode 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                }
                ////
                string strMST = Convert.ToString(dtDB_Sys_User.Rows[0]["MST"]);

                DataTable dtDB_Mst_NNT = null;
                {
                    Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // strMST 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );
                }
                ////
                //string strOrgID = Convert.ToString(dtDB_Mst_NNT.Rows[0]["OrgID"]);
                
                #endregion

                #region // Inos_LicService_GetOrgLicenseAndSaveX:
                DataSet dsGetData = null;
                {
                    // //
                    Inos_LicService_GetOrgLicenseAndSaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                    ////
                        , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
                                                        ////
                        , out dsGetData // dsData
                        );
                }
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // Inos_LicService_GetOrgSolutionAndSaveX:
                {
                    // //
                    Inos_LicService_GetOrgSolutionAndSaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                        , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
                        , "0" // objPackageId
                              ////
                        , out dsGetData // dsData
                        );
                }
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                #region // Invoice_licenseCreHist_GetX:
                if(bGet_Invoice_licenseCreHist)
                {
                    Invoice_licenseCreHist_GetX(
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
                        , strRt_Cols_Invoice_licenseCreHist // strRt_Cols_Invoice_licenseCreHist
                                                            ////
                        , out dsGetData // dsGetData
                        );
                    // //
                }
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
        #endregion

        #region // Notify:
        public void Inos_Notification_SendX(
            string strOrgID
            , string strMsgTitle
            , string strMsgDetail
            , string strTypeCode
            , string strSubType
            , string strObjectID
            , string strCustomerCodeSys
            , string strAccessToken
            , List<InosUser> Lst_InosUser
            )
        {
            #region // Refine and Check:
            if (string.IsNullOrEmpty(strAccessToken))
            {
                string strUser = htCacheMstParam[TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE].ToString();
                string strPass = htCacheMstParam[TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD].ToString();

                AccountService objAccountService = new AccountService(null);
                var ret = objAccountService.RequestToken(strUser, strPass, new string[] { "test" });
                strAccessToken = ret.AccessToken;
            }


            NotificationService objNotificationService = new NotificationService(null);
            objNotificationService.AccessToken = strAccessToken;
            List<InosNotification> lst_InosNotification = new List<InosNotification>();
            List<InosUser> Lst_InosUser_Distinct = new List<InosUser>();
            //List<InosUser> Lst_InosUser = new List<InosUser>();
            #endregion

            #region // Notify User by Type:
            if(!CmUtils.StringUtils.StringEqualIgnoreCase(strTypeCode, TConst.NotifyType.StockOutDL))
            {
                string strSql = CmUtils.StringUtils.Replace(@"
                        ---- :
                        select
                            t.UserCode
                        from Map_UserInNotifyType t --//[mylock]
                            inner join Sys_User su --//[mylock]
                                on t.UserCode = su.UserCode
                        where (1=1)
                            and t.NotifyType = '@strNotifyType'
                            and t.FlagNotify = '@FlagNotify'
                            and su.OrgID = '@strOrgID'
                            and su.CustomerCodeSys = '@strCustomerCodeSys'
                    "
                    , "@strNotifyType", strTypeCode
                    , "@FlagNotify", TConst.Flag.Active
                    , "@strOrgID", strOrgID
                    , "@strCustomerCodeSys", "ALL"
                    );

                DataTable dt_GetUser = _cf.db.ExecQuery(strSql).Tables[0];

                for (int nScan = 0; nScan < dt_GetUser.Rows.Count; nScan++)
                {
                    DataRow drScan = dt_GetUser.Rows[nScan];

                    string strUserCode = drScan["UserCode"].ToString();

                    InosUser objInosUser = new InosUser()
                    {
                        Email = strUserCode
                    };

                    Lst_InosUser.Add(objInosUser);
                }
            }
            else
            {
                string strSql = CmUtils.StringUtils.Replace(@"
                        ---- :
                        select distinct
                            t.UserCode
                        from Map_UserInNotifyType t --//[mylock]
                            inner join Sys_User su --//[mylock]
                                on t.UserCode = su.UserCode
                        where (1=1)
                            and t.NotifyType = '@strNotifyType'
                            and t.FlagNotify = '@FlagNotify'
                            and su.OrgID = '@strOrgID'
                            and su.CustomerCodeSys = '@strCustomerCodeSys'
                    "
                    , "@strNotifyType", strTypeCode
                    , "@FlagNotify", TConst.Flag.Active
                    , "@strOrgID", strOrgID
                    , "@strCustomerCodeSys", strCustomerCodeSys
                    );

                DataTable dt_GetUser = _cf.db.ExecQuery(strSql).Tables[0];

                for (int nScan = 0; nScan < dt_GetUser.Rows.Count; nScan++)
                {
                    DataRow drScan = dt_GetUser.Rows[nScan];

                    string strUserCode = drScan["UserCode"].ToString();

                    InosUser objInosUser = new InosUser()
                    {
                        Email = strUserCode
                    };

                    Lst_InosUser.Add(objInosUser);
                }
            }
            #endregion

            #region // Call Services:
            //Lst_InosUser_Distinct = Lst_InosUser.Distinct().ToList();
            Lst_InosUser_Distinct = Lst_InosUser.GroupBy(x => x.Email)
                              .Select(g => g.First())
                              .ToList();

            NotificationParams objNotificationParams = new NotificationParams()
            {
                ObjectId = strObjectID
            };

            foreach (InosUser item in Lst_InosUser_Distinct)
            {
                InosNotification inosNotification = new InosNotification()
                {
                    NetworkId = nNetworkID,
                    SolutionCode = TConst.SolutionCodeCloud.INVENTORY,
                    TypeCode = strTypeCode,
                    SubType = strSubType,
                    Detail = strMsgDetail,
                    Status = NotificationStatuses.UNREAD,
                    FirebaseStatus = NotificationPartyStatuses.Pending,
                    InosUser = item,
                    Params = objNotificationParams
                };
                //var result = objNotificationService.CreateNotifications(inosNotification);
                lst_InosNotification.Add(inosNotification);
            }

            //NotificationService objNotificationService = new NotificationService(null);
            if (lst_InosNotification.Count > 0)
            {
                var result = objNotificationService.CreateNotifications(lst_InosNotification);
            }
            #endregion
        }

        private void SkyCIC_NotificationApi_SendGeneralNotificationX(
            ref ArrayList alParamsCoupleError
            , string strSkyCICUrl
            , string strAuthorization
            , object model
            )
        {
            GeneneralNotificationBatch objGeneneralNotificationBatch = new GeneneralNotificationBatch();

            #region // WA_Invoice_Invoice_GetNoSession:
            ////
            try
            {
				bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);

				if (bTest)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSkyCICUrl", strSkyCICUrl
						, "Check.model", TJson.JsonConvert.SerializeObject(model)
						});
				}

                Thread th1 = new Thread(p =>
                {
                    //int i = 0;

                    //Send:
                    //if (i < 3)
                    //{
                        try
                        {
                            objGeneneralNotificationBatch = OS_SkyCICService.Instance.WA_SkyCIC_NotificationApi_SendGeneralNotification(strSkyCICUrl, strAuthorization, model);
                        }
                        catch (Exception exc)
                        {
                            return;
                            //goto Send;
                        }
                    //}
                    //else
                    //{
                    //    return;
                    //}
                });
                th1.Start();
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
}
