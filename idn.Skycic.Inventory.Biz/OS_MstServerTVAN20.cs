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
		#region // Mstv_Seq_Common_Get:
		public DataSet OS_MstSvTVAN_MstSv_Sys_User_Login(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			//, object objUserCode
			//, object objUserPassword
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_MstSvTVAN_MstSv_Sys_User_Login";
			string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvTVAN_MstSv_Sys_User_Login;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
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
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                RT_OS_MstSvTVAN_MstSv_Sys_User objRT_OS_MstSvTVAN_MstSv_Sys_User = null;
				{
                    #region // WA_MstSv_Sys_User_Login:
                    OS_MstSvTVAN_MstSv_Sys_User objMstSv_Sys_User = new OS_MstSvTVAN_MstSv_Sys_User();
					objMstSv_Sys_User.UserCode = strOS_MasterServer_PrdCenter_UserCode;
					objMstSv_Sys_User.UserPassword = strOS_MasterServer_PrdCenter_UserPassword;
                    /////
                    RQ_OS_MstSvTVAN_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_OS_MstSvTVAN_MstSv_Sys_User()
					{
						OS_MstSvTVAN_MstSv_Sys_User = objMstSv_Sys_User,
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
						objRT_OS_MstSvTVAN_MstSv_Sys_User = OS_MstSvTVAN_MstSvSysUserService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
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

				#region // Get Remark:
				string strResult = objRT_OS_MstSvTVAN_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
				CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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

		public DataSet OS_MstSvTVAN20_Mstv_Seq_Common_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , string strSequenceType
            , string strParam_Prefix
            , string strParam_Postfix
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_MstSvTVAN20_Mstv_Seq_Common_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvTVAN20_MstSv_Seq_Common_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strSequenceType", strSequenceType
                , "strParam_Prefix", strParam_Prefix
                , "strParam_Postfix", strParam_Postfix
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
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                RT_OS_MstSvTVAN20_MstSv_Seq_Common objRT_OS_MstSvTVAN20_Seq_Common = null;
                {
                    #region // WA_MstSv_Sys_User_Login:
                    OS_MstSvTVAN20_MstSv_Seq_Common objOS_MstSvTVAN20_Seq_Common = new OS_MstSvTVAN20_MstSv_Seq_Common();
                    objOS_MstSvTVAN20_Seq_Common.SequenceType = strSequenceType;
                    objOS_MstSvTVAN20_Seq_Common.Param_Prefix = strParam_Prefix;
                    objOS_MstSvTVAN20_Seq_Common.Param_Postfix = strParam_Postfix;
                    /////
                    RQ_OS_MstSvTVAN20_MstSv_Seq_Common objRQ_MstSv_Sys_User = new RQ_OS_MstSvTVAN20_MstSv_Seq_Common()
                    {
                        Seq_Common = objOS_MstSvTVAN20_Seq_Common,
                        Tid = strTid,
                        TokenID = strOS_MasterServer_Solution_API_Url,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                        GwPassword = strOS_MasterServer_Solution_GwPassword,
                        WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                        WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
                    };
                    ////
                    try
                    {
                        objRT_OS_MstSvTVAN20_Seq_Common = OS_MstSvTVAN20_MstSv_Seq_CommonService.Instance.WA_OS_MstSvTVAN20_Seq_Common(objRQ_MstSv_Sys_User);
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

                #region // Get Remark:
                string strResult = objRT_OS_MstSvTVAN20_Seq_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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
        public DataSet WAS_OS_MstSvTVAN20_MstSv_Seq_Common_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_MstSvTVAN20_MstSv_Seq_Common objRQ_OS_MstSvTVAN20_Seq_Common
            ////
            , out RT_OS_MstSvTVAN20_MstSv_Seq_Common objRT_OS_MstSvTVAN20_Seq_Common
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_MstSvTVAN20_Seq_Common.Tid;
            objRT_OS_MstSvTVAN20_Seq_Common = new RT_OS_MstSvTVAN20_MstSv_Seq_Common();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_MstSvTVAN20_MstSv_Seq_Common_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_MstSvTVAN20_MstSv_Seq_Common_Get;
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

                #region // OS_MstSvTVAN20_Mstv_Seq_Common_Get:
                mdsResult = OS_MstSvTVAN20_Mstv_Seq_Common_Get(
                    objRQ_OS_MstSvTVAN20_Seq_Common.Tid // strTid
                    , objRQ_OS_MstSvTVAN20_Seq_Common.GwUserCode // strGwUserCode
                    , objRQ_OS_MstSvTVAN20_Seq_Common.GwPassword // strGwPassword
                    , objRQ_OS_MstSvTVAN20_Seq_Common.WAUserCode // strUserCode
                    , objRQ_OS_MstSvTVAN20_Seq_Common.WAUserPassword // strUserPassword
                    , nNetworkID.ToString() // strNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                                            // //
                    , objRQ_OS_MstSvTVAN20_Seq_Common.Seq_Common.SequenceType.ToString() // strSequenceType
                    , objRQ_OS_MstSvTVAN20_Seq_Common.Seq_Common.Param_Prefix.ToString() // Param_Prefix
                    , objRQ_OS_MstSvTVAN20_Seq_Common.Seq_Common.Param_Postfix.ToString() // Param_Postfix
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

        public DataSet WAS_OS_MstSvTVAN_MstSv_Sys_User_Login(
            ref ArrayList alParamsCoupleError
            , RQ_OS_MstSvTVAN_MstSv_Sys_User objRQ_OS_MstSvTVAN_MstSv_Sys_User
            ////
            , out RT_OS_MstSvTVAN_MstSv_Sys_User objRT_OS_MstSvTVAN_MstSv_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_MstSvTVAN_MstSv_Sys_User.Tid;
            objRT_OS_MstSvTVAN_MstSv_Sys_User = new RT_OS_MstSvTVAN_MstSv_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_MstSvTVAN_MstSv_Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_MstSvTVAN_MstSv_Sys_User_Login;
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

                #region // OS_MstSvTVAN_MstSv_Sys_User_Login:
                mdsResult = OS_MstSvTVAN_MstSv_Sys_User_Login(
                    objRQ_OS_MstSvTVAN_MstSv_Sys_User.Tid // strTid
                    , objRQ_OS_MstSvTVAN_MstSv_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_OS_MstSvTVAN_MstSv_Sys_User.GwPassword // strGwPassword
                    , objRQ_OS_MstSvTVAN_MstSv_Sys_User.WAUserCode // strUserCode
                    , objRQ_OS_MstSvTVAN_MstSv_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_OS_MstSvTVAN_MstSv_Sys_User.OS_MstSvTVAN_MstSv_Sys_User.NetworkID.ToString() // strNetworkID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              // //
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
        #endregion
    }
}
