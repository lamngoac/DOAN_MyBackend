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
        #region // MstSv_Sys_User:
        public DataSet OS_MstSvPrdCenter_MstSv_Sys_User_Login(
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
            string strFunctionName = "OS_MstSvPrdCenter_MstSv_Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvPrdCenter_MstSv_Sys_User_Login;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strUrl", strOS_MasterServer_PrdCenter_API_Url
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
                RT_OS_MstSvPrdCentrer_MstSv_Sys_User objRT_MstSv_Sys_User = null;
                {
                    #region // WA_MstSv_Sys_User_Login:
                    OS_MstSvPrdCenter_MstSv_Sys_User objMstSv_Sys_User = new OS_MstSvPrdCenter_MstSv_Sys_User();
                    objMstSv_Sys_User.UserCode = strOS_MasterServer_PrdCenter_UserCode;
                    objMstSv_Sys_User.UserPassword = strOS_MasterServer_PrdCenter_UserPassword;
                    /////
                    RQ_OS_MstSvPrdCenter_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_OS_MstSvPrdCenter_MstSv_Sys_User()
                    {
                        OS_MstPrdCenter_MstSv_Sys_User = objMstSv_Sys_User,
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword
                    };
                    ////
                    try
                    {
                        objRT_MstSv_Sys_User = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User);
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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + "PRODUCTCENTER" + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // Get Remark:
                string strResult = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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

        public DataSet WAS_OS_MstSvPrdCenter_MstSv_Sys_User_Login(
            ref ArrayList alParamsCoupleError
            , RQ_OS_MstSvPrdCenter_MstSv_Sys_User objRQ_OS_MstSvPrdCenter_MstSv_Sys_User
            ////
            , out RT_OS_MstSvPrdCentrer_MstSv_Sys_User objRT_OS_MstSvPrdCentrer_MstSv_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.Tid;
            objRT_OS_MstSvPrdCentrer_MstSv_Sys_User = new RT_OS_MstSvPrdCentrer_MstSv_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_MstSvPrdCenter_MstSv_Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_MstSvPrdCenter_MstSv_Sys_User_Login;
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

                #region // OS_MstSvPrdCenter_MstSv_Sys_User_Login:
                {
                    /////
                    mdsResult = OS_MstSvPrdCenter_MstSv_Sys_User_Login(
                        objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.Tid // strTid
                        , objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.GwUserCode // strGwUserCode
                        , objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.GwPassword // strGwPassword
                        , objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.WAUserCode // strUserCode
                        , objRQ_OS_MstSvPrdCenter_MstSv_Sys_User.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                        ////
                        //, strUserCode // objUserCode
                        //, strUserPassword // objUserPassword
                        );
                    /////
                }
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

		#region // Mst_Org_Create:
		public void OS_MstSvPrdCenter_MstOrgCreate(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strNetworkID
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objOrgParent
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "OS_MstSvPrdCenter_MstSv_Sys_User_Login";
			//string strErrorCodeDefault = TError.ErridQContract.OS_MstSvPrdCenter_MstSv_Sys_User_Login;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
				, "objOrgID", objOrgID
				, "objOrgParent", objOrgParent
				, "objMST", objMST
                ////
                , "strUrl", strOS_MasterServer_PrdCenter_API_Url
				});
			#endregion

			#region // Call Func:
			string strUrl = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_URL].ToString();
			string strGwUser = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_GWUSERCODE].ToString();
			string strGwPass = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_GWPASSWORD].ToString();
			string strWAUser = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_WAUSERCODE].ToString();
			string strWAPass = htCacheMstParam[TConst.Mst_Param.PRODUCTCENTER_MSTSV_WAUSERPASSWORD].ToString();
			string strURLNetwork = null;
			RT_MstSv_Sys_User objRT_MstSv_Sys_User = new RT_MstSv_Sys_User();
			{
				RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
				{
					NetworkID = nNetworkID.ToString(),
					Tid = strTid,
					GwUserCode = strGwUser,
					GwPassword = strGwPass,
					WAUserCode = strWAUser,
					WAUserPassword = strWAPass,
				};

				objRT_MstSv_Sys_User = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_MstSv_Sys_User_Login(strUrl, objRQ_MstSv_Sys_User);
				strURLNetwork = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
			}
			#endregion

			#region // Call Func CreateOrg:
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strOrgIDParent = TUtils.CUtils.StdParam(objOrgParent);
			string strMST = TUtils.CUtils.StdParam(objMST);
			//strURLNetwork = "http://localhost:12308/";
			////
			RT_Mst_Org objRT_Mst_Org = new RT_Mst_Org();
			{
				Mst_Org objMst_Org = new Mst_Org()
				{
					OrgID = strOrgID,
					OrgParent = strOrgIDParent,
					MST = strMST
				};

				RQ_Mst_Org objRQ_Mst_Org = new RQ_Mst_Org()
				{
					Mst_Org = objMst_Org,
					GwUserCode = strGwUser,
					GwPassword = strGwPass,
					WAUserCode = strWAUser,
					WAUserPassword = strWAPass,
				};

				objRT_Mst_Org = OS_MstSvPrdCenterService.Instance.WA_OS_MstPrdCenter_Mst_Org_Create(strURLNetwork, objRQ_Mst_Org);
			}
			#endregion
		}
		#endregion
	}
}
