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
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Diagnostics;
using inos.common.Model;
using System.Globalization;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Sys_User
        private void DASys_User_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dt_Sys_User
            )
        {
            // GetInfo:
            dt_Sys_User = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "Sys_User" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "UserCode", "=", strUserCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dt_Sys_User.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeNotFound", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_CheckDB_UserCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dt_Sys_User.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeExist", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_CheckDB_UserCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dt_Sys_User.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.UserCodeError", strUserCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "Check.FlagActiveCurrent", dt_Sys_User.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErrDA.Sys_User_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }

        public DataSet DASys_User_Login(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , string strUserCode
            , string strUserPassword
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            //string strErrorCode = null;
            string strFunctionName = "Sys_User_Login";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_Login;
            strUserCode = TUtils.CUtils.StdParam(strUserCode);
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
				////
                , "strUserCode", strUserCode
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
                #endregion

                #region // Refine and Check Input:
                ////
                DataTable dt_Sys_User = null;
                {
                    // Sys_User_CheckDB:
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strUserCode // strUserCode
                        , TConst.Flag.Active // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dt_Sys_User // dt_Sys_User
                        );

                    string strFlagBG = TUtils.CUtils.StdParam(dt_Sys_User.Rows[0]["FlagBG"]);

                    if (CmUtils.StringUtils.StringEqual(strFlagBG, TConst.Flag.Active))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", dt_Sys_User.Rows[0]["UserCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErrDA.Sys_User_Login_InvalidFlagBG
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    string strCheckPass = TUtils.CUtils.GetEncodedHash(strWAUserPassword);
                    if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strWAUserPassword), dt_Sys_User.Rows[0]["UserPassword"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErrDA.Sys_User_Login_InvalidPassword // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }
                }
                #endregion

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_Sys_User.Rows[0]["UserCode"]);

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

        public DataSet DASys_User_ChangePassword(
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
            string strFunctionName = "Sys_User_ChangePassword";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_ChangePassword;
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

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
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

                // Sys_User_CheckDB:
                DataTable dt_Sys_User = null;
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strWAUserCode // strUserCode
                    , TConst.Flag.Active // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dt_Sys_User
                    );

                // CheckPassword:
                //strUserPasswordOld = TUtils.CUtils.GetEncodedHash(strUserPasswordOld);
                if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strUserPasswordOld), dt_Sys_User.Rows[0]["UserPassword"]))
                {
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_ChangePassword_InvalidPasswordOld // strErrorCode
                        , null // excInner
                        , alParamsCoupleError.ToArray() // arrobjParamsCouple
                        );
                }
                ////
                if (CmUtils.StringUtils.StringEqual(strUserPasswordOld, strUserPasswordNew))
                {
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_ChangePassword_InvalidPasswordNew // strErrorCode
                        , null // excInner
                        , alParamsCoupleError.ToArray() // arrobjParamsCouple
                        );
                }
                #endregion

                #region // dt_Sys_User:
                ArrayList alColumnEffective = new ArrayList();
                dt_Sys_User.Rows[0]["UserPassword"] = TUtils.CUtils.GetEncodedHash(strUserPasswordNew); alColumnEffective.Add("UserPassword");
                _cf.db.SaveData("Sys_User", dt_Sys_User, alColumnEffective.ToArray());
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

        public DataSet DASys_User_GetForCurrentUser(
            string strTid
            , string strServiceCode
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_GetForCurrentUser;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                #endregion

                #region // Build Sql:
                bool bIsHasServicesCodeNull = false;
                if (!string.IsNullOrEmpty(strServiceCode))
                {
                    bIsHasServicesCodeNull = true;
                }
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@strUserCode", strWAUserCode
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
                        select
	                        su.UserCode
	                        , su.UserName
							--, su.UserPhoneNo
							--, su.UserEmail
	                        , su.FlagSysAdmin
	                        , su.FlagActive
                            , mctm.CustomerCode mctm_CustomerCode
							, mctm.CustomerName mctm_CustomerName
							, mctm.CustomerGender mctm_CustomerGender
							, mctm.CustomerPhoneNo mctm_CustomerPhoneNo
							, mctm.CustomerMobileNo mctm_CustomerMobileNo
							, mctm.CustomerAddress mctm_CustomerAddress
							, mctm.CustomerEmail mctm_CustomerEmail
							, mctm.CustomerBOD mctm_CustomerBOD
							, mctm.CustomerAvatarPath mctm_CustomerAvatarPath
							, mctm.CustomerIDCardNo mctm_CustomerIDCardNo
                        into #tbl_Sys_User
                        from Sys_User su --//[mylock]
							left join Mst_Customer mctm --//[mylock]
								on su.UserCode = mctm.CustomerUserCode
                        where
	                        su.UserCode = @strUserCode
                        ;
						select * from #tbl_Sys_User t --//[mylock]
						;

						---- Sys_Access:
						select distinct
							sa.ObjectCode
						into #tbl_Sys_Access
						from Sys_User su --//[mylock]
							inner join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							inner join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode and sg.FlagActive = '1'
							inner join Sys_Access sa --//[mylock]
								on sg.GroupCode = sa.GroupCode
							inner join Sys_Object so --//[mylock]
								on sa.ObjectCode = so.ObjectCode and so.FlagActive = '1'
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from #tbl_Sys_User f --//[mylock]
							inner join Sys_Object so --//[mylock]
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
						from #tbl_Sys_Access f --//[mylock]
							inner join Sys_Object so --//[mylock]
								on f.ObjectCode = so.ObjectCode
                        where (1=1)
                            @strServicesCode
						;
					"
                    , "@strServicesCode", bIsHasServicesCodeNull ? "and so.ServiceCode = '@strFt_ServiceCode'".Replace("@strFt_ServiceCode", strServiceCode) : "" //  -- MOBILEAPP
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                dsGetData.Tables[0].TableName = "Sys_User";
                dsGetData.Tables[1].TableName = "Sys_Access";
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

        public DataSet DASys_User_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ////
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
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
						where (1=1)
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode                         
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
								--, su.PhoneNo
								--, su.EMail
								, su.FlagSysAdmin                       
								, su.FlagActive
								, mctm.CustomerCode mctm_CustomerCode
							    , mctm.CustomerName mctm_CustomerName
							    , mctm.CustomerGender mctm_CustomerGender
							    , mctm.CustomerPhoneNo mctm_CustomerPhoneNo
							    , mctm.CustomerMobileNo mctm_CustomerMobileNo
							    , mctm.CustomerAddress mctm_CustomerAddress
							    , mctm.CustomerEmail mctm_CustomerEmail
							    , mctm.CustomerBOD mctm_CustomerBOD
							    , mctm.CustomerAvatarPath mctm_CustomerAvatarPath
							    , mctm.CustomerIDCardNo mctm_CustomerIDCardNo
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
                                left join Mst_Customer mctm --//[mylock]
                                    on su.UserCode = mctm.CustomerUserCode
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
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
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
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
                            , "Mst_Customer" // strTableNameDB
                            , "Mst_Customer." // strPrefixStd
                            , "mctm." // strPrefixAlias
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
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
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

        private void DASys_User_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode // objUserCode
            , object objUserName // objUserName
            , object objUserPassword // objUserPassword
            , object objUserPhoneNo // objUserPhoneNo
            , object objUserEMail // objUserEMail
            , object objFlagSysAdmin // objFlagSysAdmin
            )
        {
            #region // Temp:
            //mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_CreateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objUserPassword", objUserPassword
                , "objUserPhoneNo", objUserPhoneNo
                , "objUserEMail", objUserEMail
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strUserPhoneNo = string.Format("{0}", objUserPhoneNo).Trim();
            string strUserEMail = string.Format("{0}", objUserEMail).Trim();
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);

            string strFlagUserExist = TConst.Flag.Inactive;

            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objUserCode // objUserCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_Create_InvalidUserName
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
                        TError.ErrDA.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (string.IsNullOrEmpty(strFlagSysAdmin))
                {
                    strFlagSysAdmin = "0";
                }
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetEncodedHash(strUserPassword);
                strFN = "UserPhoneNo"; drDB[strFN] = strUserPhoneNo;
                strFN = "UserEMail"; drDB[strFN] = strUserEMail;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagBG"; drDB[strFN] = TConst.Flag.Inactive;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion

            // Assign:
            CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strFlagUserExist);
        }

        public DataSet DASys_User_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objUserPhoneNo
            , object objUserEmail
            , object objFlagSysAdmin
            , object objCustomerCode
            , object objCustomerTypeCode
            //, object objCustomerName
            , object objCustomerGender
            //, object objCustomerPhoneNo
            , object objCustomerMobileNo
            , object objCustomerAddress
            //, object objCustomerEmail
            , object objCustomerBOD
            , object objCustomerAvatarPath
            , object objCustomerIDCardNo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Create";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objUserPhoneNo", objUserPhoneNo
                , "objUserEMail", objUserEmail
                , "objFlagSysAdmin", objFlagSysAdmin
                //, "objFlagBG", objFlagBG
                , "objCustomerCode", objCustomerCode
                , "objCustomerTypeCode", objCustomerTypeCode
                , "objCustomerGender", objCustomerGender
                , "objCustomerMobileNo", objCustomerMobileNo
                , "objCustomerAddress", objCustomerAddress
                , "objCustomerBOD", objCustomerBOD
                , "objCustomerAvatarPath", objCustomerAvatarPath
                , "objCustomerIDCardNo", objCustomerIDCardNo
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

                //Check Access/ Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Save:
                DASys_User_CreateX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , objUserCode // objUserCode
                    , objUserName // objUserName
                    , objUserPassword // objUserPassword
                    , objUserPhoneNo // objUserPhoneNo
                    , objUserEmail // objUserEMail
                    , objFlagSysAdmin // objFlagSysAdmin
                                      ////
                    );

                DAMst_Customer_CreateX(
                    strTid
                    , strGwUserCode
                    , strGwPassword
                    , strWAUserCode
                    , strWAUserPassword
                    , ref alParamsCoupleError
                    , dtimeSys
                    //// 
                    , objCustomerCode
                    , objCustomerTypeCode
                    , objUserName
                    , objCustomerGender
                    , objUserPhoneNo
                    , objCustomerMobileNo
                    , objCustomerAddress
                    , objUserEmail
                    , objCustomerBOD
                    , objCustomerAvatarPath
                    , objUserCode
                    , objCustomerIDCardNo
                    );
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

        public DataSet DASys_User_Activate(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objUserCode
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Activate";
            string strErrorCodeDefault = TError.ErrDA.Sys_User_Activate;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, objUserCode, objUserCode
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
                Sys_User_CheckAuthentication(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strWAUserPassword
                    );

                // Check Access/Deny:
                // Sys_Access_CheckDenyV30(
                // ref alParamsCoupleError
                // , strWAUserCode
                // , strFunctionName
                // );
                #endregion

                #region // Sys_User_ActivateX:
                //DataSet dsGetData = null;
                {
                    DASys_User_ActivateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objUserCode
                        , objFlagActive
                        /////
                        , objFt_Cols_Upd // objFt_Cols_Upd
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

        private void DASys_User_ActivateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Sys_User_ActivateX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_UserCode = strFt_Cols_Upd.Contains("Sys_User.UserCode".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_User.FlagActive".ToUpper());

            ////
            DataTable dtDB_Sys_User = null;
            DataTable dtDB_Sys_User_Handle = null;
            {
                ////
                DASys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );

                string strFlagSysAdmin = dtDB_Sys_User.Rows[0]["FlagSysAdmin"].ToString();
                ////
                DASys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strWAUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User_Handle // dtDB_Sys_User
                    );

                string strFlagSysAdmin_Handle = dtDB_Sys_User_Handle.Rows[0]["FlagSysAdmin"].ToString();
                if (!CmUtils.StringUtils.StringEqual(strFlagSysAdmin_Handle, TConst.Flag.Active))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeError", strWAUserCode
                        , "Check.FlagActiveCurrent", strFlagSysAdmin_Handle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_Activate_InvalidPowerful
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

                if (CmUtils.StringUtils.StringEqual(strFlagSysAdmin, TConst.Flag.Active) 
                    && !CmUtils.StringUtils.StringEqualIgnoreCase(strWAUserCode, "SYSADMIN"))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeError", strWAUserCode
                        , "Check.FlagActiveCurrent", strFlagSysAdmin_Handle
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErrDA.Sys_User_Activate_InvalidPowerful
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            #endregion

            #region // Save Mst_Supplier:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.Rows[0];
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);
                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion

        }
        #endregion

        #region // Sys_User: WAS
        public DataSet WAS_DASys_User_Login(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_Login";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_Login;
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

                #region // Sys_User_Login:
                mdsResult = DASys_User_Login(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                                                    //, objRQ_Sys_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
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

        public DataSet WAS_DASys_User_ChangePassword(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_ChangePassword";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_ChangePassword;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_User> lst_Sys_User = new List<Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_ChangePassword:
                mdsResult = DASys_User_ChangePassword(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserPassword // strUserPasswordOld
                    , objRQ_Sys_User.Sys_User.UserPasswordNew // strUserPasswordNew
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

        public DataSet WAS_DASys_User_GetForCurrentUser(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_GetForCurrentUser;
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
                List<DA_Sys_User> lst_Sys_User = new List<DA_Sys_User>();
                List<DA_Sys_Access> lst_Sys_Access = new List<DA_Sys_Access>();
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = DASys_User_GetForCurrentUser(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.ServiceCode
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                    lst_Sys_User = TUtils.DataTableCmUtils.ToListof<DA_Sys_User>(dt_Sys_User);
                    objRT_Sys_User.Lst_Sys_User = lst_Sys_User;

                    ////
                    DataTable dt_Sys_Access = mdsResult.Tables["Sys_Access"].Copy();
                    lst_Sys_Access = TUtils.DataTableCmUtils.ToListof<DA_Sys_Access>(dt_Sys_Access);
                    objRT_Sys_User.Lst_Sys_Access = lst_Sys_Access;
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

        public DataSet WAS_DASys_User_Get(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_Get";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_Get;
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
                List<DA_Sys_User> lst_Sys_User = new List<DA_Sys_User>();
                List<DA_Sys_UserInGroup> lst_Sys_UserInGroup = new List<DA_Sys_UserInGroup>();
                bool bGet_Sys_User = (objRQ_Sys_User.Rt_Cols_Sys_User != null && objRQ_Sys_User.Rt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (objRQ_Sys_User.Rt_Cols_Sys_UserInGroup != null && objRQ_Sys_User.Rt_Cols_Sys_UserInGroup.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = DASys_User_Get(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_User.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_User)
                    {
                        ////
                        DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                        lst_Sys_User = TUtils.DataTableCmUtils.ToListof<DA_Sys_User>(dt_Sys_User);
                        objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                    }
                    ////
                    if (bGet_Sys_UserInGroup)
                    {
                        DataTable dt_Sys_UserInGroup = mdsResult.Tables["Sys_UserInGroup"].Copy();
                        lst_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<DA_Sys_UserInGroup>(dt_Sys_UserInGroup);
                        objRT_Sys_User.Lst_Sys_UserInGroup = lst_Sys_UserInGroup;
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

        public DataSet WAS_DASys_User_Create(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<DA_Sys_User> lst_Sys_User = new List<DA_Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_Create:
                mdsResult = DASys_User_Create(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserCode // objUserCode
                    , objRQ_Sys_User.Sys_User.UserName // objUserName
                    , objRQ_Sys_User.Sys_User.UserPassword // objUserPassword
                    , objRQ_Sys_User.Sys_User.UserPhoneNo //object objPhoneNo
                    , objRQ_Sys_User.Sys_User.UserEmail //object objEMail
                    , objRQ_Sys_User.Sys_User.FlagSysAdmin // objFlagSysAdmin
                    , objRQ_Sys_User.Sys_User.mctm_CustomerCode // mctm_CustomerCode
                    , objRQ_Sys_User.Sys_User.mctm_CustomerTypeCode
                    , objRQ_Sys_User.Sys_User.mctm_CustomerGender
                    , objRQ_Sys_User.Sys_User.mctm_CustomerMobileNo
                    , objRQ_Sys_User.Sys_User.mctm_CustomerAddress
                    , objRQ_Sys_User.Sys_User.mctm_CustomerBOD
                    , objRQ_Sys_User.Sys_User.mctm_CustomerAvatarPath
                    , objRQ_Sys_User.Sys_User.mctm_CustomerIDCardNo
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

        public DataSet WAS_DASys_User_Activate(
            ref ArrayList alParamsCoupleError
            , DA_RQ_Sys_User objRQ_Sys_User
            ////
            , out DA_RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new DA_RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "WAS_Sys_User_Create";
            string strErrorCodeDefault = TError.ErrDA.WAS_Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<DA_Sys_User> lst_Sys_User = new List<DA_Sys_User>();
                #endregion

                #region // Sys_User_Activate:
                mdsResult = DASys_User_Activate(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserCode
                    , objRQ_Sys_User.Sys_User.FlagActive
                    ////
                    , objRQ_Sys_User.Ft_Cols_Upd
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
