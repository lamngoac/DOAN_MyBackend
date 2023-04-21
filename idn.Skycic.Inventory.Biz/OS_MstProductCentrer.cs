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
using System.Web;
using System.IO;
using System.Diagnostics;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Mst_Spec:
        public DataSet OS_PrdCenter_Mst_Spec_Get(
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
            , string strRt_Cols_Mst_Spec
            , string strRt_Cols_Mst_SpecImage
            , string strRt_Cols_Mst_SpecFiles
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			Stopwatch stopWatchFunc = new Stopwatch();
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Get;
			ArrayList alParamsCoupleSW = new ArrayList();
			alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Spec", strRt_Cols_Mst_Spec
                , "strRt_Cols_Mst_SpecImage", strRt_Cols_Mst_SpecImage
                , "strRt_Cols_Mst_SpecFiles", strRt_Cols_Mst_SpecFiles
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Spec> lst_Mst_Spec = new List<OS_PrdCenter_Mst_Spec>();
                List<OS_PrdCenter_Mst_SpecImage> lst_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                List<OS_PrdCenter_Mst_SpecFiles> lst_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                ////
                bool bGet_Mst_Spec = (strRt_Cols_Mst_Spec != null && strRt_Cols_Mst_Spec.Length > 0);
                bool bGet_Mst_SpecImage = (strRt_Cols_Mst_SpecImage != null && strRt_Cols_Mst_SpecImage.Length > 0);
                bool bGet_Mst_SpecFiles = (strRt_Cols_Mst_SpecFiles != null && strRt_Cols_Mst_SpecFiles.Length > 0);
                //bool bGet_Mst_SpecDtl = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Spec = strRt_Cols_Mst_Spec,
                        Rt_Cols_Mst_SpecImage = strRt_Cols_Mst_SpecImage,
                        Rt_Cols_Mst_SpecFiles = strRt_Cols_Mst_SpecFiles,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Get(objRQ_OS_PrdCenter_Mst_Spec);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Spec.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Spec)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_Spec, "Mst_Spec");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                    if (bGet_Mst_SpecImage)
                    {
                        //////
                        DataTable dt_Mst_SpecImage = new DataTable();
                        dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecImage>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
                        dsGetData.Tables.Add(dt_Mst_SpecImage.Copy());
                    }
                    ////
                    if (bGet_Mst_SpecFiles)
                    {
                        //////
                        DataTable dt_Mst_SpecFiles = new DataTable();
                        dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecFiles>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
                        dsGetData.Tables.Add(dt_Mst_SpecFiles.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Spec_Get_New20191113(
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
            , string strRt_Cols_Mst_Spec
            , string strRt_Cols_Mst_SpecImage
            , string strRt_Cols_Mst_SpecFiles
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Spec", strRt_Cols_Mst_Spec
                , "strRt_Cols_Mst_SpecImage", strRt_Cols_Mst_SpecImage
                , "strRt_Cols_Mst_SpecFiles", strRt_Cols_Mst_SpecFiles
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Spec> lst_Mst_Spec = new List<OS_PrdCenter_Mst_Spec>();
                List<OS_PrdCenter_Mst_SpecImage> lst_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                List<OS_PrdCenter_Mst_SpecFiles> lst_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                ////
                bool bGet_Mst_Spec = (strRt_Cols_Mst_Spec != null && strRt_Cols_Mst_Spec.Length > 0);
                bool bGet_Mst_SpecImage = (strRt_Cols_Mst_SpecImage != null && strRt_Cols_Mst_SpecImage.Length > 0);
                bool bGet_Mst_SpecFiles = (strRt_Cols_Mst_SpecFiles != null && strRt_Cols_Mst_SpecFiles.Length > 0);
                //bool bGet_Mst_SpecDtl = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Spec = strRt_Cols_Mst_Spec,
                        Rt_Cols_Mst_SpecImage = strRt_Cols_Mst_SpecImage,
                        Rt_Cols_Mst_SpecFiles = strRt_Cols_Mst_SpecFiles,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Get(objRQ_OS_PrdCenter_Mst_Spec);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Spec.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Spec)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_Spec, "Mst_Spec");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                    if (bGet_Mst_SpecImage)
                    {
                        //////
                        DataTable dt_Mst_SpecImage = new DataTable();
                        dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecImage>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
                        dsGetData.Tables.Add(dt_Mst_SpecImage.Copy());
                    }
                    ////
                    if (bGet_Mst_SpecFiles)
                    {
                        //////
                        DataTable dt_Mst_SpecFiles = new DataTable();
                        dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecFiles>(objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
                        dsGetData.Tables.Add(dt_Mst_SpecFiles.Copy());
                    }
                }
                ////
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

        public DataSet WAS_OS_PrdCenter_Mst_Spec_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec
            ////
            , out RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Spec.Tid;
            objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Spec_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Spec_Get;
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
                List<OS_PrdCenter_Mst_Spec> lst_Mst_Spec = new List<OS_PrdCenter_Mst_Spec>();
                List<OS_PrdCenter_Mst_SpecImage> lst_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                List<OS_PrdCenter_Mst_SpecFiles> lst_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                bool bGet_Mst_Spec = (objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_Spec != null && objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_Spec.Length > 0);
                bool bGet_Mst_SpecImage = (objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecImage != null && objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecImage.Length > 0);
                bool bGet_Mst_SpecFiles = (objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecFiles != null && objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecFiles.Length > 0);
                //bool bGet_Mst_SpecDtl = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_Spec_Get:
                mdsResult = OS_PrdCenter_Mst_Spec_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_Spec.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Spec.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_Spec.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_Spec.Ft_WhereClause // strFt_WhereClause
                                                                 //// Return:
                    , objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_Spec // strRt_Cols_Mst_Spec
                    , objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecImage // strRt_Cols_Mst_SpecImage
                    , objRQ_OS_PrdCenter_Mst_Spec.Rt_Cols_Mst_SpecFiles // strRt_Cols_Mst_SpecFiles
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_Spec.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Spec)
                    {
                        ////
                        DataTable dt_Mst_Spec = mdsResult.Tables["Mst_Spec"].Copy();
                        lst_Mst_Spec = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_Spec>(dt_Mst_Spec);
                        objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_Spec = lst_Mst_Spec;
                    }
                    ////
                    if (bGet_Mst_SpecImage)
                    {
                        ////
                        DataTable dt_Mst_SpecImage = mdsResult.Tables["Mst_SpecImage"].Copy();
                        lst_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecImage>(dt_Mst_SpecImage);
                        objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage = lst_Mst_SpecImage;
                    }
                    ////
                    if (bGet_Mst_SpecFiles)
                    {
                        ////
                        DataTable dt_Mst_SpecFiles = mdsResult.Tables["Mst_SpecFiles"].Copy();
                        lst_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecFiles>(dt_Mst_SpecFiles);
                        objRT_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles = lst_Mst_SpecFiles;
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
        public DataSet OS_PrdCenter_Mst_Spec_Add(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objSpecName
            , object objSpecDesc
            , object objModelCode
            , object objSpecType1
            , object objSpecType2
            , object objColor
            , object objFlagHasSerial
            , object objFlagHasLOT
            , object objDefaultUnitCode
            , object objStandardUnitCode
            , object objNetworkSpecCode
            , object objRemark
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            , object objCustomField6
            , object objCustomField7
            , object objCustomField8
            , object objCustomField9
            , object objCustomField10
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Add";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Add;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objSpecName", objSpecName
                , "objSpecDesc", objSpecDesc
                , "objModelCode", objModelCode
                , "objSpecType1", objSpecType1
                , "objSpecType2", objSpecType2
                , "objColor", objColor
                , "objFlagHasSerial", objFlagHasSerial
                , "objFlagHasLOT", objFlagHasLOT
                , "objDefaultUnitCode", objDefaultUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objNetworkSpecCode", objNetworkSpecCode
                , "objRemark", objRemark
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
                , "objCustomField6", objCustomField6
                , "objCustomField7", objCustomField7
                , "objCustomField8", objCustomField8
                , "objCustomField9", objCustomField9
                , "objCustomField10", objCustomField10
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_Spec.SpecName = objSpecName;
                    objOS_PrdCenter_Mst_Spec.SpecDesc = objSpecDesc;
                    objOS_PrdCenter_Mst_Spec.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Spec.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_Spec.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_Spec.Color = objColor;
                    objOS_PrdCenter_Mst_Spec.FlagHasSerial = objFlagHasSerial;
                    objOS_PrdCenter_Mst_Spec.FlagHasLOT = objFlagHasLOT;
                    objOS_PrdCenter_Mst_Spec.DefaultUnitCode = objDefaultUnitCode;
                    objOS_PrdCenter_Mst_Spec.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_Spec.NetworkSpecCode = objNetworkSpecCode;
                    objOS_PrdCenter_Mst_Spec.Remark = objRemark;
                    objOS_PrdCenter_Mst_Spec.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Mst_Spec.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Mst_Spec.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Mst_Spec.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Mst_Spec.CustomField5 = objCustomField5;
                    objOS_PrdCenter_Mst_Spec.CustomField6 = objCustomField6;
                    objOS_PrdCenter_Mst_Spec.CustomField7 = objCustomField7;
                    objOS_PrdCenter_Mst_Spec.CustomField8 = objCustomField8;
                    objOS_PrdCenter_Mst_Spec.CustomField9 = objCustomField9;
                    objOS_PrdCenter_Mst_Spec.CustomField10 = objCustomField10;
                    ////
                    List<OS_PrdCenter_Mst_SpecFiles> LstOS_PrdCenter_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    {
                        ////
                        DataTable dt_Mst_SpecImage = dsData.Tables["Mst_SpecFiles"].Copy();
                        LstOS_PrdCenter_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecFiles>(dt_Mst_SpecImage);
                        ////

                    }
                    ////
                    List<OS_PrdCenter_Mst_SpecImage> LstOS_PrdCenter_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    {
                        ////
                        DataTable dt_Mst_SpecFiles = dsData.Tables["Mst_SpecImage"].Copy();
                        LstOS_PrdCenter_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecImage>(dt_Mst_SpecFiles);
                        ////

                    }
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                        Lst_Mst_SpecFiles = LstOS_PrdCenter_Mst_SpecFiles,
                        Lst_Mst_SpecImage = LstOS_PrdCenter_Mst_SpecImage,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Add(objRQ_OS_PrdCenter_Mst_Spec);

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

        public DataSet OS_PrdCenter_Mst_Spec_Add_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objSpecName
            , object objSpecDesc
            , object objModelCode
            , object objSpecType1
            , object objSpecType2
            , object objColor
            , object objFlagHasSerial
            , object objFlagHasLOT
            , object objDefaultUnitCode
            , object objStandardUnitCode
            , object objNetworkSpecCode
            , object objRemark
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            , object objCustomField6
            , object objCustomField7
            , object objCustomField8
            , object objCustomField9
            , object objCustomField10
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Add";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Add;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objSpecName", objSpecName
                , "objSpecDesc", objSpecDesc
                , "objModelCode", objModelCode
                , "objSpecType1", objSpecType1
                , "objSpecType2", objSpecType2
                , "objColor", objColor
                , "objFlagHasSerial", objFlagHasSerial
                , "objFlagHasLOT", objFlagHasLOT
                , "objDefaultUnitCode", objDefaultUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objNetworkSpecCode", objNetworkSpecCode
                , "objRemark", objRemark
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
                , "objCustomField6", objCustomField6
                , "objCustomField7", objCustomField7
                , "objCustomField8", objCustomField8
                , "objCustomField9", objCustomField9
                , "objCustomField10", objCustomField10
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_Spec.SpecName = objSpecName;
                    objOS_PrdCenter_Mst_Spec.SpecDesc = objSpecDesc;
                    objOS_PrdCenter_Mst_Spec.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Spec.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_Spec.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_Spec.Color = objColor;
                    objOS_PrdCenter_Mst_Spec.FlagHasSerial = objFlagHasSerial;
                    objOS_PrdCenter_Mst_Spec.FlagHasLOT = objFlagHasLOT;
                    objOS_PrdCenter_Mst_Spec.DefaultUnitCode = objDefaultUnitCode;
                    objOS_PrdCenter_Mst_Spec.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_Spec.NetworkSpecCode = objNetworkSpecCode;
                    objOS_PrdCenter_Mst_Spec.Remark = objRemark;
                    objOS_PrdCenter_Mst_Spec.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Mst_Spec.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Mst_Spec.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Mst_Spec.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Mst_Spec.CustomField5 = objCustomField5;
                    objOS_PrdCenter_Mst_Spec.CustomField6 = objCustomField6;
                    objOS_PrdCenter_Mst_Spec.CustomField7 = objCustomField7;
                    objOS_PrdCenter_Mst_Spec.CustomField8 = objCustomField8;
                    objOS_PrdCenter_Mst_Spec.CustomField9 = objCustomField9;
                    objOS_PrdCenter_Mst_Spec.CustomField10 = objCustomField10;
                    ////
                    List<OS_PrdCenter_Mst_SpecFiles> LstOS_PrdCenter_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    {
                        ////
                        DataTable dt_Mst_SpecImage = dsData.Tables["Mst_SpecFiles"].Copy();
                        LstOS_PrdCenter_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecFiles>(dt_Mst_SpecImage);
                        ////

                    }
                    ////
                    List<OS_PrdCenter_Mst_SpecImage> LstOS_PrdCenter_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    {
                        ////
                        DataTable dt_Mst_SpecFiles = dsData.Tables["Mst_SpecImage"].Copy();
                        LstOS_PrdCenter_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecImage>(dt_Mst_SpecFiles);
                        ////

                    }
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                        Lst_Mst_SpecFiles = LstOS_PrdCenter_Mst_SpecFiles,
                        Lst_Mst_SpecImage = LstOS_PrdCenter_Mst_SpecImage,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Add(objRQ_OS_PrdCenter_Mst_Spec);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_Spec_Add(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec
            ////
            , out RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Spec.Tid;
            objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Spec_Add";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Spec_Add;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec)
                , "Lst_Mst_SpecImage", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage)
                , "Lst_Mst_SpecFiles", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles)
            ////
            });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage == null) objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    DataTable dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecImage>(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
                    dsData.Tables.Add(dt_Mst_SpecImage);
                    ////
                    if (objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles == null) objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    DataTable dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecFiles>(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
                    dsData.Tables.Add(dt_Mst_SpecFiles);

                }
                #endregion

                #region // OS_PrdCenter_Mst_Spec_Add:
                mdsResult = OS_PrdCenter_Mst_Spec_Add_New20191113(
                    objRQ_OS_PrdCenter_Mst_Spec.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecName // objSpecName
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecDesc // objSpecDesc
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.ModelCode // objModelCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecType1 // objSpecType1
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecType2 // objSpecType2
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.Color // objColor
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.FlagHasSerial // objFlagHasSerial
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.FlagHasLOT // objFlagHasLOT
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.DefaultUnitCode // objDefaultUnitCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.StandardUnitCode // objStandardUnitCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.NetworkSpecCode // objNetworkSpecCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField1 // objCustomField1
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField2 // objCustomField2
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField3 // objCustomField3
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField4 // objCustomField4	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField5 // objCustomField5
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField6 // objCustomField6	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField7 // objCustomField7
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField8 // objCustomField8	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField9 // objCustomField9
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField10 // objCustomField10		
                    , dsData // dsData
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
        public DataSet OS_PrdCenter_Mst_Spec_Upd(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objSpecName
            , object objSpecDesc
            , object objColor
            , object objFlagHasSerial
            , object objFlagHasLOT
            , object objDefaultUnitCode
            , object objStandardUnitCode
            , object objNetworkSpecCode
            , object objRemark
            , object objFlagActive
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            , object objCustomField6
            , object objCustomField7
            , object objCustomField8
            , object objCustomField9
            , object objCustomField10
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Upd";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Upd;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objSpecName", objSpecName
                , "objSpecDesc", objSpecDesc
                , "objColor", objColor
                , "objFlagHasSerial", objFlagHasSerial
                , "objFlagHasLOT", objFlagHasLOT
                , "objDefaultUnitCode", objDefaultUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objNetworkSpecCode", objNetworkSpecCode
                , "objRemark", objRemark
                , "objFlagActive", objFlagActive
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
                , "objCustomField6", objCustomField6
                , "objCustomField7", objCustomField7
                , "objCustomField8", objCustomField8
                , "objCustomField9", objCustomField9
                , "objCustomField10", objCustomField10
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_Spec.SpecName = objSpecName;
                    objOS_PrdCenter_Mst_Spec.SpecDesc = objSpecDesc;
                    objOS_PrdCenter_Mst_Spec.Color = objColor;
                    objOS_PrdCenter_Mst_Spec.FlagHasSerial = objFlagHasSerial;
                    objOS_PrdCenter_Mst_Spec.FlagHasLOT = objFlagHasLOT;
                    objOS_PrdCenter_Mst_Spec.DefaultUnitCode = objDefaultUnitCode;
                    objOS_PrdCenter_Mst_Spec.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_Spec.NetworkSpecCode = objNetworkSpecCode;
                    objOS_PrdCenter_Mst_Spec.Remark = objRemark;
                    objOS_PrdCenter_Mst_Spec.FlagActive = objFlagActive;
                    objOS_PrdCenter_Mst_Spec.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Mst_Spec.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Mst_Spec.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Mst_Spec.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Mst_Spec.CustomField5 = objCustomField5;
                    objOS_PrdCenter_Mst_Spec.CustomField6 = objCustomField6;
                    objOS_PrdCenter_Mst_Spec.CustomField7 = objCustomField7;
                    objOS_PrdCenter_Mst_Spec.CustomField8 = objCustomField8;
                    objOS_PrdCenter_Mst_Spec.CustomField9 = objCustomField9;
                    objOS_PrdCenter_Mst_Spec.CustomField10 = objCustomField10;
                    /////
                    List<OS_PrdCenter_Mst_SpecFiles> LstOS_PrdCenter_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    {
                        ////
                        DataTable dt_Mst_SpecImage = dsData.Tables["Mst_SpecFiles"].Copy();
                        LstOS_PrdCenter_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecFiles>(dt_Mst_SpecImage);
                        ////

                    }
                    ////
                    List<OS_PrdCenter_Mst_SpecImage> LstOS_PrdCenter_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    {
                        ////
                        DataTable dt_Mst_SpecFiles = dsData.Tables["Mst_SpecImage"].Copy();
                        LstOS_PrdCenter_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecImage>(dt_Mst_SpecFiles);
                        ////

                    }
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                        Lst_Mst_SpecFiles = LstOS_PrdCenter_Mst_SpecFiles,
                        Lst_Mst_SpecImage = LstOS_PrdCenter_Mst_SpecImage,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Upd(objRQ_OS_PrdCenter_Mst_Spec);

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

        public DataSet OS_PrdCenter_Mst_Spec_Upd20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objSpecName
            , object objSpecDesc
            , object objColor
            , object objFlagHasSerial
            , object objFlagHasLOT
            , object objDefaultUnitCode
            , object objStandardUnitCode
            , object objNetworkSpecCode
            , object objRemark
            , object objFlagActive
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            , object objCustomField6
            , object objCustomField7
            , object objCustomField8
            , object objCustomField9
            , object objCustomField10
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Upd";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Upd;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objSpecName", objSpecName
                , "objSpecDesc", objSpecDesc
                , "objColor", objColor
                , "objFlagHasSerial", objFlagHasSerial
                , "objFlagHasLOT", objFlagHasLOT
                , "objDefaultUnitCode", objDefaultUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objNetworkSpecCode", objNetworkSpecCode
                , "objRemark", objRemark
                , "objFlagActive", objFlagActive
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
                , "objCustomField6", objCustomField6
                , "objCustomField7", objCustomField7
                , "objCustomField8", objCustomField8
                , "objCustomField9", objCustomField9
                , "objCustomField10", objCustomField10
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_Spec.SpecName = objSpecName;
                    objOS_PrdCenter_Mst_Spec.SpecDesc = objSpecDesc;
                    objOS_PrdCenter_Mst_Spec.Color = objColor;
                    objOS_PrdCenter_Mst_Spec.FlagHasSerial = objFlagHasSerial;
                    objOS_PrdCenter_Mst_Spec.FlagHasLOT = objFlagHasLOT;
                    objOS_PrdCenter_Mst_Spec.DefaultUnitCode = objDefaultUnitCode;
                    objOS_PrdCenter_Mst_Spec.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_Spec.NetworkSpecCode = objNetworkSpecCode;
                    objOS_PrdCenter_Mst_Spec.Remark = objRemark;
                    objOS_PrdCenter_Mst_Spec.FlagActive = objFlagActive;
                    objOS_PrdCenter_Mst_Spec.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Mst_Spec.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Mst_Spec.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Mst_Spec.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Mst_Spec.CustomField5 = objCustomField5;
                    objOS_PrdCenter_Mst_Spec.CustomField6 = objCustomField6;
                    objOS_PrdCenter_Mst_Spec.CustomField7 = objCustomField7;
                    objOS_PrdCenter_Mst_Spec.CustomField8 = objCustomField8;
                    objOS_PrdCenter_Mst_Spec.CustomField9 = objCustomField9;
                    objOS_PrdCenter_Mst_Spec.CustomField10 = objCustomField10;
                    /////
                    List<OS_PrdCenter_Mst_SpecFiles> LstOS_PrdCenter_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    {
                        ////
                        DataTable dt_Mst_SpecImage = dsData.Tables["Mst_SpecFiles"].Copy();
                        LstOS_PrdCenter_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecFiles>(dt_Mst_SpecImage);
                        ////

                    }
                    ////
                    List<OS_PrdCenter_Mst_SpecImage> LstOS_PrdCenter_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    {
                        ////
                        DataTable dt_Mst_SpecFiles = dsData.Tables["Mst_SpecImage"].Copy();
                        LstOS_PrdCenter_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecImage>(dt_Mst_SpecFiles);
                        ////

                    }
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                        Lst_Mst_SpecFiles = LstOS_PrdCenter_Mst_SpecFiles,
                        Lst_Mst_SpecImage = LstOS_PrdCenter_Mst_SpecImage,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Upd(objRQ_OS_PrdCenter_Mst_Spec);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_Spec_Upd(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec
            ////
            , out RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Spec.Tid;
            objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Spec_Upd";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Spec_Upd;
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
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    if (objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage == null) objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage = new List<OS_PrdCenter_Mst_SpecImage>();
                    DataTable dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecImage>(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
                    dsData.Tables.Add(dt_Mst_SpecImage);
                    ////
                    if (objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles == null) objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles = new List<OS_PrdCenter_Mst_SpecFiles>();
                    DataTable dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecFiles>(objRQ_OS_PrdCenter_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
                    dsData.Tables.Add(dt_Mst_SpecFiles);
                    ////
                }
                #endregion

                #region // OS_PrdCenter_Mst_Spec_Upd:
                mdsResult = OS_PrdCenter_Mst_Spec_Upd20191113(
                    objRQ_OS_PrdCenter_Mst_Spec.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecName // objSpecName
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecDesc // objSpecDesc
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.Color // objColor
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.FlagHasSerial // objFlagHasSerial
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.FlagHasLOT // objFlagHasLOT
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.DefaultUnitCode // objDefaultUnitCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.StandardUnitCode // objStandardUnitCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.NetworkSpecCode // objNetworkSpecCode
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.FlagActive // objFlagActive
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField1 // objCustomField1
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField2 // objCustomField2
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField3 // objCustomField3
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField4 // objCustomField4	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField5 // objCustomField5
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField6 // objCustomField6	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField7 // objCustomField7
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField8 // objCustomField8	
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField9 // objCustomField9
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.CustomField10 // objCustomField10		
                    , dsData // dsData
                             ///
                    , objRQ_OS_PrdCenter_Mst_Spec.Ft_Cols_Upd // Ft_Cols_Upd
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
        public DataSet OS_PrdCenter_Mst_Spec_Del(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Del";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Del;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Del(objRQ_OS_PrdCenter_Mst_Spec);

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

        public DataSet OS_PrdCenter_Mst_Spec_Del_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Spec_Del";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Spec_Del;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Spec objOS_PrdCenter_Mst_Spec = new OS_PrdCenter_Mst_Spec();
                    objOS_PrdCenter_Mst_Spec.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Spec.SpecCode = objSpecCode;
                    #endregion

                    #region // WA_Mst_Spec_Get:
                    RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec = new RQ_OS_PrdCenter_Mst_Spec()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Spec = objOS_PrdCenter_Mst_Spec,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_OS_PrdCenter_Mst_Spec_Del(objRQ_OS_PrdCenter_Mst_Spec);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_Spec_Del(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec
            ////
            , out RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Spec.Tid;
            objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Spec_Del";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Spec_Del;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec)
                ////
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // OS_PrdCenter_Mst_Spec_Del:
                mdsResult = OS_PrdCenter_Mst_Spec_Del_New20191113(
                    objRQ_OS_PrdCenter_Mst_Spec.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
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

        #region // Mst_SpecType1:
        public DataSet OS_PrdCenter_Mst_SpecType1_Get(
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
            , string strRt_Cols_Mst_SpecType1
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecType1", strRt_Cols_Mst_SpecType1
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecType1> lst_Mst_SpecType1 = new List<OS_PrdCenter_Mst_SpecType1>();
                ////
                bool bGet_Mst_SpecType1 = (strRt_Cols_Mst_SpecType1 != null && strRt_Cols_Mst_SpecType1.Length > 0);
                //bool bGet_Mst_SpecType1Dtl = (objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl != null && objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecType1 = strRt_Cols_Mst_SpecType1,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Get(objRQ_OS_PrdCenter_Mst_SpecType1);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecType1.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecType1)
                    {
                        ////
                        DataTable dt_Mst_SpecType1 = new DataTable();
                        dt_Mst_SpecType1 = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1.Lst_Mst_SpecType1, "Mst_SpecType1");
                        dsGetData.Tables.Add(dt_Mst_SpecType1.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecType1_Get_New20191113(
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
            , string strRt_Cols_Mst_SpecType1
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecType1", strRt_Cols_Mst_SpecType1
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecType1> lst_Mst_SpecType1 = new List<OS_PrdCenter_Mst_SpecType1>();
                ////
                bool bGet_Mst_SpecType1 = (strRt_Cols_Mst_SpecType1 != null && strRt_Cols_Mst_SpecType1.Length > 0);
                //bool bGet_Mst_SpecType1Dtl = (objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl != null && objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecType1 = strRt_Cols_Mst_SpecType1,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Get(objRQ_OS_PrdCenter_Mst_SpecType1);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecType1.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecType1)
                    {
                        ////
                        DataTable dt_Mst_SpecType1 = new DataTable();
                        dt_Mst_SpecType1 = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1.Lst_Mst_SpecType1, "Mst_SpecType1");
                        dsGetData.Tables.Add(dt_Mst_SpecType1.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecType1_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            , object objSpecType1Name
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
                , "objSpecType1Name", objSpecType1Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1Name = objSpecType1Name;
                    objOS_PrdCenter_Mst_SpecType1.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Create(objRQ_OS_PrdCenter_Mst_SpecType1);

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

        public DataSet OS_PrdCenter_Mst_SpecType1_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            , object objSpecType1Name
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
                , "objSpecType1Name", objSpecType1Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1Name = objSpecType1Name;
                    objOS_PrdCenter_Mst_SpecType1.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Create(objRQ_OS_PrdCenter_Mst_SpecType1);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecType1_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            , object objSpecType1Name
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
                , "objSpecType1Name", objSpecType1Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1Name = objSpecType1Name;
                    objOS_PrdCenter_Mst_SpecType1.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecType1.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Update(objRQ_OS_PrdCenter_Mst_SpecType1);

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

        public DataSet OS_PrdCenter_Mst_SpecType1_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            , object objSpecType1Name
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
                , "objSpecType1Name", objSpecType1Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1Name = objSpecType1Name;
                    objOS_PrdCenter_Mst_SpecType1.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecType1.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Update(objRQ_OS_PrdCenter_Mst_SpecType1);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecType1_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Delete(objRQ_OS_PrdCenter_Mst_SpecType1);

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

        public DataSet OS_PrdCenter_Mst_SpecType1_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType1
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType1_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType1_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType1", objSpecType1
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType1 objOS_PrdCenter_Mst_SpecType1 = new OS_PrdCenter_Mst_SpecType1();
                    objOS_PrdCenter_Mst_SpecType1.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType1.SpecType1 = objSpecType1;
                    #endregion

                    #region // WA_Mst_SpecType1_Get:
                    RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1 = new RQ_OS_PrdCenter_Mst_SpecType1()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType1 = objOS_PrdCenter_Mst_SpecType1,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType1 = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_OS_PrdCenter_Mst_SpecType1_Delete(objRQ_OS_PrdCenter_Mst_SpecType1);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType1_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1
            ////
            , out RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType1.Tid;
            objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType1.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType1_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType1_Get;
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
                List<OS_PrdCenter_Mst_SpecType1> lst_Mst_SpecType1 = new List<OS_PrdCenter_Mst_SpecType1>();
                bool bGet_Mst_SpecType1 = (objRQ_OS_PrdCenter_Mst_SpecType1.Rt_Cols_Mst_SpecType1 != null && objRQ_OS_PrdCenter_Mst_SpecType1.Rt_Cols_Mst_SpecType1.Length > 0);
                //bool bGet_Mst_SpecType1Dtl = (objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl != null && objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1Dtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecType1_Get:
                mdsResult = OS_PrdCenter_Mst_SpecType1_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType1.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Rt_Cols_Mst_SpecType1 // strRt_Cols_Mst_SpecType1
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_SpecType1.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_SpecType1)
                    {
                        ////
                        DataTable dt_Mst_SpecType1 = mdsResult.Tables["Mst_SpecType1"].Copy();
                        lst_Mst_SpecType1 = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecType1>(dt_Mst_SpecType1);
                        objRT_OS_PrdCenter_Mst_SpecType1.Lst_Mst_SpecType1 = lst_Mst_SpecType1;
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType1_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1
            ////
            , out RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType1.Tid;
            objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType1.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType1_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType1_Create;
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

                #region // OS_PrdCenter_Mst_SpecType1_Create:
                mdsResult = OS_PrdCenter_Mst_SpecType1_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType1.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.SpecType1 // objSpecType1
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.SpecType1Name // objSpecType1Name
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType1_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1
            ////
            , out RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType1.Tid;
            objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType1.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType1_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType1_Update;
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

                #region // OS_PrdCenter_Mst_SpecType1_Create:
                mdsResult = OS_PrdCenter_Mst_SpecType1_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType1.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.SpecType1 // objSpecType1
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.SpecType1Name // objSpecType1Name
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.FlagActive // objRemark
                                                                        ///
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType1_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1
            ////
            , out RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType1.Tid;
            objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType1.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType1_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType1_Delete;
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

                #region // OS_PrdCenter_Mst_SpecType1_Delete:
                mdsResult = OS_PrdCenter_Mst_SpecType1_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType1.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecType1.Mst_SpecType1.SpecType1 // objSpecType1
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

        #region // Mst_SpecType2:
        public DataSet OS_PrdCenter_Mst_SpecType2_Get(
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
            , string strRt_Cols_Mst_SpecType2
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecType2", strRt_Cols_Mst_SpecType2
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecType2> lst_Mst_SpecType2 = new List<OS_PrdCenter_Mst_SpecType2>();
                ////
                bool bGet_Mst_SpecType2 = (strRt_Cols_Mst_SpecType2 != null && strRt_Cols_Mst_SpecType2.Length > 0);
                //bool bGet_Mst_SpecType2Dtl = (objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl != null && objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecType2 = strRt_Cols_Mst_SpecType2,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Get(objRQ_OS_PrdCenter_Mst_SpecType2);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecType2.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecType2)
                    {
                        ////
                        DataTable dt_Mst_SpecType2 = new DataTable();
                        dt_Mst_SpecType2 = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2.Lst_Mst_SpecType2, "Mst_SpecType2");
                        dsGetData.Tables.Add(dt_Mst_SpecType2.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecType2_Get_New20191113(
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
            , string strRt_Cols_Mst_SpecType2
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecType2", strRt_Cols_Mst_SpecType2
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecType2> lst_Mst_SpecType2 = new List<OS_PrdCenter_Mst_SpecType2>();
                ////
                bool bGet_Mst_SpecType2 = (strRt_Cols_Mst_SpecType2 != null && strRt_Cols_Mst_SpecType2.Length > 0);
                //bool bGet_Mst_SpecType2Dtl = (objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl != null && objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecType2 = strRt_Cols_Mst_SpecType2,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Get(objRQ_OS_PrdCenter_Mst_SpecType2);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecType2.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecType2)
                    {
                        ////
                        DataTable dt_Mst_SpecType2 = new DataTable();
                        dt_Mst_SpecType2 = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2.Lst_Mst_SpecType2, "Mst_SpecType2");
                        dsGetData.Tables.Add(dt_Mst_SpecType2.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecType2_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            , object objSpecType2Name
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
                , "objSpecType2Name", objSpecType2Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2Name = objSpecType2Name;
                    objOS_PrdCenter_Mst_SpecType2.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Create(objRQ_OS_PrdCenter_Mst_SpecType2);

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

        public DataSet OS_PrdCenter_Mst_SpecType2_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            , object objSpecType2Name
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
                , "objSpecType2Name", objSpecType2Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2Name = objSpecType2Name;
                    objOS_PrdCenter_Mst_SpecType2.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Create(objRQ_OS_PrdCenter_Mst_SpecType2);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecType2_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            , object objSpecType2Name
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
                , "objSpecType2Name", objSpecType2Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2Name = objSpecType2Name;
                    objOS_PrdCenter_Mst_SpecType2.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecType2.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Update(objRQ_OS_PrdCenter_Mst_SpecType2);

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

        public DataSet OS_PrdCenter_Mst_SpecType2_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            , object objSpecType2Name
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
                , "objSpecType2Name", objSpecType2Name
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2Name = objSpecType2Name;
                    objOS_PrdCenter_Mst_SpecType2.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecType2.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Update(objRQ_OS_PrdCenter_Mst_SpecType2);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecType2_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Delete(objRQ_OS_PrdCenter_Mst_SpecType2);

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

        public DataSet OS_PrdCenter_Mst_SpecType2_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecType2
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecType2_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecType2_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecType2", objSpecType2
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecType2 objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2();
                    objOS_PrdCenter_Mst_SpecType2.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecType2.SpecType2 = objSpecType2;
                    #endregion

                    #region // WA_Mst_SpecType2_Get:
                    RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2 = new RQ_OS_PrdCenter_Mst_SpecType2()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecType2 = objOS_PrdCenter_Mst_SpecType2,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecType2 = OS_PrdCenter_Mst_SpecType2Service.Instance.WA_OS_PrdCenter_Mst_SpecType2_Delete(objRQ_OS_PrdCenter_Mst_SpecType2);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType2_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2
            ////
            , out RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType2.Tid;
            objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType2.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType2_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType2_Get;
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
                List<OS_PrdCenter_Mst_SpecType2> lst_Mst_SpecType2 = new List<OS_PrdCenter_Mst_SpecType2>();
                bool bGet_Mst_SpecType2 = (objRQ_OS_PrdCenter_Mst_SpecType2.Rt_Cols_Mst_SpecType2 != null && objRQ_OS_PrdCenter_Mst_SpecType2.Rt_Cols_Mst_SpecType2.Length > 0);
                //bool bGet_Mst_SpecType2Dtl = (objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl != null && objRQ_Mst_SpecType2.Rt_Cols_Mst_SpecType2Dtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecType2_Get:
                mdsResult = OS_PrdCenter_Mst_SpecType2_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType2.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Ft_WhereClause // strFt_WhereClause
                                                                      //// Return:
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Rt_Cols_Mst_SpecType2 // strRt_Cols_Mst_SpecType2
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_SpecType2.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_SpecType2)
                    {
                        ////
                        DataTable dt_Mst_SpecType2 = mdsResult.Tables["Mst_SpecType2"].Copy();
                        lst_Mst_SpecType2 = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecType2>(dt_Mst_SpecType2);
                        objRT_OS_PrdCenter_Mst_SpecType2.Lst_Mst_SpecType2 = lst_Mst_SpecType2;
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType2_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2
            ////
            , out RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType2.Tid;
            objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType2.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType2_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType2_Create;
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

                #region // OS_PrdCenter_Mst_SpecType2_Create:
                mdsResult = OS_PrdCenter_Mst_SpecType2_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType2.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.SpecType2 // objSpecType2
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.SpecType2Name // objSpecType2Name
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType2_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2
            ////
            , out RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType2.Tid;
            objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType2.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType2_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType2_Update;
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

                #region // OS_PrdCenter_Mst_SpecType2_Create:
                mdsResult = OS_PrdCenter_Mst_SpecType2_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType2.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.SpecType2 // objSpecType2
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.SpecType2Name // objSpecType2Name
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.FlagActive // objRemark
                                                                                ///
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecType2_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2
            ////
            , out RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecType2.Tid;
            objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecType2.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecType2_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecType2_Delete;
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

                #region // OS_PrdCenter_Mst_SpecType2_Delete:
                mdsResult = OS_PrdCenter_Mst_SpecType2_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecType2.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.OrgID // objSpecType2
                    , objRQ_OS_PrdCenter_Mst_SpecType2.Mst_SpecType2.SpecType2 // objSpecType2
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

        #region // Mst_SpecCustomField:
        public DataSet OS_PrdCenter_Mst_SpecCustomField_Get(
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
            , string strRt_Cols_Mst_SpecCustomField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecCustomField_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecCustomField", strRt_Cols_Mst_SpecCustomField
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecCustomField> lst_Mst_SpecCustomField = new List<OS_PrdCenter_Mst_SpecCustomField>();
                ////
                bool bGet_Mst_SpecCustomField = (strRt_Cols_Mst_SpecCustomField != null && strRt_Cols_Mst_SpecCustomField.Length > 0);
                //bool bGet_Mst_SpecCustomFieldDtl = (objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl != null && objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl.Length > 0);
                ////

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
                {
                    #region // WA_Mst_SpecCustomField_Get:
                    RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField = new RQ_OS_PrdCenter_Mst_SpecCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Rt_Cols_Mst_SpecCustomField = strRt_Cols_Mst_SpecCustomField,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecCustomField = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_OS_PrdCenter_Mst_SpecCustomField_Get(objRQ_OS_PrdCenter_Mst_SpecCustomField);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecCustomField.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecCustomField)
                    {
                        ////
                        DataTable dt_Mst_SpecCustomField = new DataTable();
                        dt_Mst_SpecCustomField = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecCustomField>(objRT_OS_PrdCenter_Mst_SpecCustomField.Lst_Mst_SpecCustomField, "Mst_SpecCustomField");
                        dsGetData.Tables.Add(dt_Mst_SpecCustomField.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecCustomField_Get_New20191113(
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
            , string strRt_Cols_Mst_SpecCustomField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecCustomField_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecCustomField", strRt_Cols_Mst_SpecCustomField
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_SpecCustomField> lst_Mst_SpecCustomField = new List<OS_PrdCenter_Mst_SpecCustomField>();
                ////
                bool bGet_Mst_SpecCustomField = (strRt_Cols_Mst_SpecCustomField != null && strRt_Cols_Mst_SpecCustomField.Length > 0);
                //bool bGet_Mst_SpecCustomFieldDtl = (objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl != null && objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl.Length > 0);
                ////

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
                {
                    #region // WA_Mst_SpecCustomField_Get:
                    RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField = new RQ_OS_PrdCenter_Mst_SpecCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Rt_Cols_Mst_SpecCustomField = strRt_Cols_Mst_SpecCustomField,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecCustomField = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_OS_PrdCenter_Mst_SpecCustomField_Get(objRQ_OS_PrdCenter_Mst_SpecCustomField);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecCustomField.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecCustomField)
                    {
                        ////
                        DataTable dt_Mst_SpecCustomField = new DataTable();
                        dt_Mst_SpecCustomField = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecCustomField>(objRT_OS_PrdCenter_Mst_SpecCustomField.Lst_Mst_SpecCustomField, "Mst_SpecCustomField");
                        dsGetData.Tables.Add(dt_Mst_SpecCustomField.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecCustomField_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objSpecCustomFieldCode
            , object objSpecCustomFieldName
            , object objDBPhysicalType
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecCustomField_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objSpecCustomFieldCode", objSpecCustomFieldCode
                , "objSpecCustomFieldName", objSpecCustomFieldName
                , "objDBPhysicalType", objDBPhysicalType
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecCustomField objOS_PrdCenter_Mst_SpecCustomField = new OS_PrdCenter_Mst_SpecCustomField();
                    objOS_PrdCenter_Mst_SpecCustomField.SpecCustomFieldCode = objSpecCustomFieldCode;
                    objOS_PrdCenter_Mst_SpecCustomField.OrgID = strOrgID;
                    objOS_PrdCenter_Mst_SpecCustomField.SpecCustomFieldName = objSpecCustomFieldName;
                    objOS_PrdCenter_Mst_SpecCustomField.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecCustomField.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecCustomField_Get:
                    RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField = new RQ_OS_PrdCenter_Mst_SpecCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecCustomField = objOS_PrdCenter_Mst_SpecCustomField,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecCustomField = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_OS_PrdCenter_Mst_SpecCustomField_Update(objRQ_OS_PrdCenter_Mst_SpecCustomField);

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

        public DataSet OS_PrdCenter_Mst_SpecCustomField_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objSpecCustomFieldCode
            , object objSpecCustomFieldName
            , object objDBPhysicalType
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecCustomField_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objSpecCustomFieldCode", objSpecCustomFieldCode
                , "objSpecCustomFieldName", objSpecCustomFieldName
                , "objDBPhysicalType", objDBPhysicalType
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecCustomField objOS_PrdCenter_Mst_SpecCustomField = new OS_PrdCenter_Mst_SpecCustomField();
                    objOS_PrdCenter_Mst_SpecCustomField.SpecCustomFieldCode = objSpecCustomFieldCode;
                    objOS_PrdCenter_Mst_SpecCustomField.OrgID = strOrgID;
                    objOS_PrdCenter_Mst_SpecCustomField.SpecCustomFieldName = objSpecCustomFieldName;
                    objOS_PrdCenter_Mst_SpecCustomField.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecCustomField.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecCustomField_Get:
                    RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField = new RQ_OS_PrdCenter_Mst_SpecCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecCustomField = objOS_PrdCenter_Mst_SpecCustomField,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecCustomField = OS_PrdCenter_Mst_SpecCustomFieldService.Instance.WA_OS_PrdCenter_Mst_SpecCustomField_Update(objRQ_OS_PrdCenter_Mst_SpecCustomField);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecCustomField_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField
            ////
            , out RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid;
            objRT_OS_PrdCenter_Mst_SpecCustomField = new RT_OS_PrdCenter_Mst_SpecCustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecCustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecCustomField_Get;
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
                List<OS_PrdCenter_Mst_SpecCustomField> lst_Mst_SpecCustomField = new List<OS_PrdCenter_Mst_SpecCustomField>();
                bool bGet_Mst_SpecCustomField = (objRQ_OS_PrdCenter_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomField != null && objRQ_OS_PrdCenter_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomField.Length > 0);
                //bool bGet_Mst_SpecCustomFieldDtl = (objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl != null && objRQ_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomFieldDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecCustomField_Get:
                mdsResult = OS_PrdCenter_Mst_SpecCustomField_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Rt_Cols_Mst_SpecCustomField // strRt_Cols_Mst_SpecCustomField
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_SpecCustomField.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_SpecCustomField)
                    {
                        ////
                        DataTable dt_Mst_SpecCustomField = mdsResult.Tables["Mst_SpecCustomField"].Copy();
                        lst_Mst_SpecCustomField = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecCustomField>(dt_Mst_SpecCustomField);
                        objRT_OS_PrdCenter_Mst_SpecCustomField.Lst_Mst_SpecCustomField = lst_Mst_SpecCustomField;
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecCustomField_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField
            ////
            , out RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid;
            objRT_OS_PrdCenter_Mst_SpecCustomField = new RT_OS_PrdCenter_Mst_SpecCustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecCustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecCustomField_Update;
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

                #region // OS_PrdCenter_Mst_SpecCustomField_Update:
                mdsResult = OS_PrdCenter_Mst_SpecCustomField_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Mst_SpecCustomField.SpecCustomFieldCode // objSpecCustomFieldCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Mst_SpecCustomField.SpecCustomFieldName // objSpecCustomFieldName
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Mst_SpecCustomField.DBPhysicalType // objDBPhysicalType
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Mst_SpecCustomField.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Mst_SpecCustomField.FlagActive // objRemark
                                                                               ///
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.Ft_Cols_Upd // Ft_Cols_Upd
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

        #region // Mst_SpecUnit:
        public DataSet OS_PrdCenter_Mst_SpecUnit_Get(
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
            , string strRt_Cols_Mst_SpecUnit
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecUnit", strRt_Cols_Mst_SpecUnit
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<OS_PrdCenter_Mst_SpecUnit> lst_OS_PrdCenter_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                ////
                bool bGet_Mst_SpecUnit = (strRt_Cols_Mst_SpecUnit != null && strRt_Cols_Mst_SpecUnit.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                {
                    #region // WA_Mst_SpecUnit_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecUnit = strRt_Cols_Mst_SpecUnit,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Get(objRQ_OS_PrdCenter_Mst_SpecUnit);

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
                    #endregion

                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecUnit.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecUnit)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecUnit>(objRT_OS_PrdCenter_Mst_SpecUnit.Lst_Mst_SpecUnit, "Mst_SpecUnit");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Get_New20191113(
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
            , string strRt_Cols_Mst_SpecUnit
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecUnit", strRt_Cols_Mst_SpecUnit
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<OS_PrdCenter_Mst_SpecUnit> lst_OS_PrdCenter_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                ////
                bool bGet_Mst_SpecUnit = (strRt_Cols_Mst_SpecUnit != null && strRt_Cols_Mst_SpecUnit.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                {
                    #region // WA_Mst_SpecUnit_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecUnit = strRt_Cols_Mst_SpecUnit,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Get(objRQ_OS_PrdCenter_Mst_SpecUnit);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    #endregion

                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecUnit.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecUnit)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecUnit>(objRT_OS_PrdCenter_Mst_SpecUnit.Lst_Mst_SpecUnit, "Mst_SpecUnit");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objStandardUnitCode
            , object objSpecUnitDesc
            , object objQty
            , object objLength
            , object objWidth
            , object objHeight
            , object objVolume
            , object objWeight
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objSpecUnitDesc", objSpecUnitDesc
                , "objQty", objQty
                , "objLength", objLength
                , "objWidth", objWidth
                , "objHeight", objHeight
                , "objVolume", objVolume
                , "objWeight", objWeight
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.SpecUnitDesc = objSpecUnitDesc;
                    objOS_PrdCenter_Mst_SpecUnit.Qty = objQty;
                    objOS_PrdCenter_Mst_SpecUnit.Length = objLength;
                    objOS_PrdCenter_Mst_SpecUnit.Width = objWidth;
                    objOS_PrdCenter_Mst_SpecUnit.Height = objHeight;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Volume = objVolume;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Create(objRQ_OS_PrdCenter_Mst_SpecUnit);

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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objStandardUnitCode
            , object objSpecUnitDesc
            , object objQty
            , object objLength
            , object objWidth
            , object objHeight
            , object objVolume
            , object objWeight
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objSpecUnitDesc", objSpecUnitDesc
                , "objQty", objQty
                , "objLength", objLength
                , "objWidth", objWidth
                , "objHeight", objHeight
                , "objVolume", objVolume
                , "objWeight", objWeight
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.SpecUnitDesc = objSpecUnitDesc;
                    objOS_PrdCenter_Mst_SpecUnit.Qty = objQty;
                    objOS_PrdCenter_Mst_SpecUnit.Length = objLength;
                    objOS_PrdCenter_Mst_SpecUnit.Width = objWidth;
                    objOS_PrdCenter_Mst_SpecUnit.Height = objHeight;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Volume = objVolume;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Create(objRQ_OS_PrdCenter_Mst_SpecUnit);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objStandardUnitCode
            , object objSpecUnitDesc
            , object objQty
            , object objLength
            , object objWidth
            , object objHeight
            , object objVolume
            , object objWeight
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objSpecUnitDesc", objSpecUnitDesc
                , "objQty", objQty
                , "objLength", objLength
                , "objWidth", objWidth
                , "objHeight", objHeight
                , "objVolume", objVolume
                , "objWeight", objWeight
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.SpecUnitDesc = objSpecUnitDesc;
                    objOS_PrdCenter_Mst_SpecUnit.Qty = objQty;
                    objOS_PrdCenter_Mst_SpecUnit.Length = objLength;
                    objOS_PrdCenter_Mst_SpecUnit.Width = objWidth;
                    objOS_PrdCenter_Mst_SpecUnit.Height = objHeight;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Volume = objVolume;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecUnit.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Update(objRQ_OS_PrdCenter_Mst_SpecUnit);

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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objStandardUnitCode
            , object objSpecUnitDesc
            , object objQty
            , object objLength
            , object objWidth
            , object objHeight
            , object objVolume
            , object objWeight
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objStandardUnitCode", objStandardUnitCode
                , "objSpecUnitDesc", objSpecUnitDesc
                , "objQty", objQty
                , "objLength", objLength
                , "objWidth", objWidth
                , "objHeight", objHeight
                , "objVolume", objVolume
                , "objWeight", objWeight
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.StandardUnitCode = objStandardUnitCode;
                    objOS_PrdCenter_Mst_SpecUnit.SpecUnitDesc = objSpecUnitDesc;
                    objOS_PrdCenter_Mst_SpecUnit.Qty = objQty;
                    objOS_PrdCenter_Mst_SpecUnit.Length = objLength;
                    objOS_PrdCenter_Mst_SpecUnit.Width = objWidth;
                    objOS_PrdCenter_Mst_SpecUnit.Height = objHeight;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Volume = objVolume;
                    objOS_PrdCenter_Mst_SpecUnit.Weight = objWeight;
                    objOS_PrdCenter_Mst_SpecUnit.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecUnit.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Update(objRQ_OS_PrdCenter_Mst_SpecUnit);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Delete(objRQ_OS_PrdCenter_Mst_SpecUnit);

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

        public DataSet OS_PrdCenter_Mst_SpecUnit_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_SpecUnit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecUnit_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecUnit objOS_PrdCenter_Mst_SpecUnit = new OS_PrdCenter_Mst_SpecUnit();
                    objOS_PrdCenter_Mst_SpecUnit.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecUnit.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecUnit.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit = new RQ_OS_PrdCenter_Mst_SpecUnit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecUnit = objOS_PrdCenter_Mst_SpecUnit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecUnit = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_OS_PrdCenter_Mst_SpecUnit_Delete(objRQ_OS_PrdCenter_Mst_SpecUnit);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecUnit_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit
            ////
            , out RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecUnit.Tid;
            objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecUnit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecUnit_Get;
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
                List<OS_PrdCenter_Mst_SpecUnit> lst_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                bool bGet_Mst_SpecUnit = (objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit != null && objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecUnit_Get:
                mdsResult = OS_PrdCenter_Mst_SpecUnit_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecUnit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit // strRt_Cols_Mst_SpecUnit
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_SpecUnit.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_SpecUnit)
                    {
                        ////
                        DataTable dt_Mst_SpecUnit = mdsResult.Tables["Mst_SpecUnit"].Copy();
                        lst_Mst_SpecUnit = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecUnit>(dt_Mst_SpecUnit);
                        objRT_OS_PrdCenter_Mst_SpecUnit.Lst_Mst_SpecUnit = lst_Mst_SpecUnit;
                    }
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecUnit_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit
            ////
            , out RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecUnit.Tid;
            objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecUnit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecUnit_Create;
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
                List<OS_PrdCenter_Mst_SpecUnit> lst_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                bool bGet_Mst_SpecUnit = (objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit != null && objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecUnit_Create:
                mdsResult = OS_PrdCenter_Mst_SpecUnit_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecUnit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.OrgID //objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.SpecCode //objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.UnitCode //objUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.StandardUnitCode //objStandardUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.SpecUnitDesc //objSpecUnitDesc
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Qty //objQty
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Length //objLength
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Width //objWidth
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Height //objHeight
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Volume //objVolume
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Weight //objWeight
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Remark //objRemark
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecUnit_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit
            ////
            , out RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecUnit.Tid;
            objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecUnit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecUnit_Update;
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
                List<OS_PrdCenter_Mst_SpecUnit> lst_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                bool bGet_Mst_SpecUnit = (objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit != null && objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecUnit_Update:
                mdsResult = OS_PrdCenter_Mst_SpecUnit_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecUnit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.OrgID //objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.SpecCode //objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.UnitCode //objUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.StandardUnitCode //objStandardUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.SpecUnitDesc //objSpecUnitDesc
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Qty //objQty
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Length //objLength
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Width //objWidth
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Height //objHeight
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Volume //objVolume
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Weight //objWeight
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.Remark //objRemark
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.FlagActive //objFlagActive
                                                                              ////
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecUnit_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit
            ////
            , out RT_OS_PrdCenter_Mst_SpecUnit objRT_OS_PrdCenter_Mst_SpecUnit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecUnit.Tid;
            objRT_OS_PrdCenter_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecUnit.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecUnit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecUnit_Delete;
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
                List<OS_PrdCenter_Mst_SpecUnit> lst_Mst_SpecUnit = new List<OS_PrdCenter_Mst_SpecUnit>();
                bool bGet_Mst_SpecUnit = (objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit != null && objRQ_OS_PrdCenter_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecUnit_Delete:
                mdsResult = OS_PrdCenter_Mst_SpecUnit_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecUnit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.OrgID //objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.SpecCode //objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.Mst_SpecUnit.UnitCode //objUnitCode
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

        #region // Mst_SpecPrice:
        public DataSet OS_PrdCenter_Mst_SpecPrice_Get(
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
            , string strRt_Cols_Mst_SpecPrice
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecPrice", strRt_Cols_Mst_SpecPrice
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<OS_PrdCenter_Mst_SpecPrice> lst_OS_PrdCenter_Mst_SpecPrice = new List<OS_PrdCenter_Mst_SpecPrice>();
                ////
                bool bGet_Mst_SpecPrice = (strRt_Cols_Mst_SpecPrice != null && strRt_Cols_Mst_SpecPrice.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecPrice = strRt_Cols_Mst_SpecPrice,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Get(objRQ_OS_PrdCenter_Mst_SpecPrice);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecPrice.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecPrice)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice.Lst_Mst_SpecPrice, "Mst_SpecPrice");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Get_New20191113(
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
            , string strRt_Cols_Mst_SpecPrice
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_SpecPrice", strRt_Cols_Mst_SpecPrice
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<OS_PrdCenter_Mst_SpecPrice> lst_OS_PrdCenter_Mst_SpecPrice = new List<OS_PrdCenter_Mst_SpecPrice>();
                ////
                bool bGet_Mst_SpecPrice = (strRt_Cols_Mst_SpecPrice != null && strRt_Cols_Mst_SpecPrice.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_SpecPrice = strRt_Cols_Mst_SpecPrice,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Get(objRQ_OS_PrdCenter_Mst_SpecPrice);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_SpecPrice.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_SpecPrice)
                    {
                        ////
                        DataTable dt_Mst_Spec = new DataTable();
                        dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice.Lst_Mst_SpecPrice, "Mst_SpecPrice");
                        dsGetData.Tables.Add(dt_Mst_Spec.Copy());
                    }
                    ////
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objBuyPrice
            , object objSellPrice
            , object objCurrencyCode
            , object objVATRateCode
            //, object objEffectDTimeStart
            , object objDiscountVND
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objBuyPrice", objBuyPrice
                , "objSellPrice", objSellPrice
                , "objCurrencyCode", objCurrencyCode
                , "objVATRateCode", objVATRateCode
                , "objDiscountVND", objDiscountVND
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecPrice.BuyPrice = objBuyPrice;
                    objOS_PrdCenter_Mst_SpecPrice.SellPrice = objSellPrice;
                    objOS_PrdCenter_Mst_SpecPrice.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_SpecPrice.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_SpecPrice.DiscountVND = objDiscountVND;
                    objOS_PrdCenter_Mst_SpecPrice.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Create(objRQ_OS_PrdCenter_Mst_SpecPrice);

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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objBuyPrice
            , object objSellPrice
            , object objCurrencyCode
            , object objVATRateCode
            //, object objEffectDTimeStart
            , object objDiscountVND
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objBuyPrice", objBuyPrice
                , "objSellPrice", objSellPrice
                , "objCurrencyCode", objCurrencyCode
                , "objVATRateCode", objVATRateCode
                , "objDiscountVND", objDiscountVND
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecPrice.BuyPrice = objBuyPrice;
                    objOS_PrdCenter_Mst_SpecPrice.SellPrice = objSellPrice;
                    objOS_PrdCenter_Mst_SpecPrice.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_SpecPrice.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_SpecPrice.DiscountVND = objDiscountVND;
                    objOS_PrdCenter_Mst_SpecPrice.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Create(objRQ_OS_PrdCenter_Mst_SpecPrice);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objBuyPrice
            , object objSellPrice
            //, object objCurrencyCode
            //, object objEffectDTimeStart
            , object objDiscountVND
            , object objVATRateCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objBuyPrice", objBuyPrice
                , "objSellPrice", objSellPrice
                //, "objCurrencyCode", objCurrencyCode
                , "objDiscountVND", objDiscountVND
                , "objVATRateCode", objVATRateCode
                , "objRemark", objRemark
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecPrice.BuyPrice = objBuyPrice;
                    objOS_PrdCenter_Mst_SpecPrice.SellPrice = objSellPrice;
                    //objOS_PrdCenter_Mst_SpecPrice.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_SpecPrice.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_SpecPrice.DiscountVND = objDiscountVND;
                    objOS_PrdCenter_Mst_SpecPrice.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecPrice.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Update(objRQ_OS_PrdCenter_Mst_SpecPrice);

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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            , object objBuyPrice
            , object objSellPrice
            //, object objCurrencyCode
            //, object objEffectDTimeStart
            , object objDiscountVND
            , object objVATRateCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
                , "objBuyPrice", objBuyPrice
                , "objSellPrice", objSellPrice
                //, "objCurrencyCode", objCurrencyCode
                , "objDiscountVND", objDiscountVND
                , "objVATRateCode", objVATRateCode
                , "objRemark", objRemark
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_SpecPrice.BuyPrice = objBuyPrice;
                    objOS_PrdCenter_Mst_SpecPrice.SellPrice = objSellPrice;
                    //objOS_PrdCenter_Mst_SpecPrice.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_SpecPrice.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_SpecPrice.DiscountVND = objDiscountVND;
                    objOS_PrdCenter_Mst_SpecPrice.Remark = objRemark;
                    objOS_PrdCenter_Mst_SpecPrice.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Update(objRQ_OS_PrdCenter_Mst_SpecPrice);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Delete(objRQ_OS_PrdCenter_Mst_SpecPrice);

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

        public DataSet OS_PrdCenter_Mst_SpecPrice_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// 
            , object objOrgID
            , object objSpecCode
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "OS_PrdCenter_Mst_SpecPrice_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_SpecPrice_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objSpecCode", objSpecCode
                , "objUnitCode", objUnitCode
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_SpecPrice objOS_PrdCenter_Mst_SpecPrice = new OS_PrdCenter_Mst_SpecPrice();
                    objOS_PrdCenter_Mst_SpecPrice.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_SpecPrice.SpecCode = objSpecCode;
                    objOS_PrdCenter_Mst_SpecPrice.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_SpecPrice_Get:
                    RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice = new RQ_OS_PrdCenter_Mst_SpecPrice()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_SpecPrice = objOS_PrdCenter_Mst_SpecPrice,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_SpecPrice = OS_PrdCenter_Mst_SpecPriceService.Instance.WA_OS_PrdCenter_Mst_SpecPrice_Delete(objRQ_OS_PrdCenter_Mst_SpecPrice);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_SpecPrice_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice
            ////
            , out RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecPrice.Tid;
            objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecPrice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecPrice_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecPrice_Get;
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
                List<OS_PrdCenter_Mst_SpecPrice> lst_Mst_SpecPrice = new List<OS_PrdCenter_Mst_SpecPrice>();
                bool bGet_Mst_SpecPrice = (objRQ_OS_PrdCenter_Mst_SpecPrice.Rt_Cols_Mst_SpecPrice != null && objRQ_OS_PrdCenter_Mst_SpecPrice.Rt_Cols_Mst_SpecPrice.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_SpecPrice_Get:
                mdsResult = OS_PrdCenter_Mst_SpecPrice_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecPrice.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Ft_WhereClause // strFt_WhereClause
                                                         //// Return:
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Rt_Cols_Mst_SpecPrice // strRt_Cols_Mst_SpecPrice
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_SpecPrice.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_SpecPrice)
                    {
                        ////
                        DataTable dt_Mst_SpecPrice = mdsResult.Tables["Mst_SpecPrice"].Copy();
                        lst_Mst_SpecPrice = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_SpecPrice>(dt_Mst_SpecPrice);
                        objRT_OS_PrdCenter_Mst_SpecPrice.Lst_Mst_SpecPrice = lst_Mst_SpecPrice;
                    }
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecPrice_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice
            ////
            , out RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecPrice.Tid;
            objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecPrice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecPrice_Create;
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

                #region // WAS_OS_PrdCenter_Mst_SpecPrice_Create:
                mdsResult = OS_PrdCenter_Mst_SpecPrice_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecPrice.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //////
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.UnitCode // objUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.BuyPrice // objBuyPrice
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.SellPrice // objSellPrice
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.CurrencyCode // objCurrencyCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.VATRateCode // objVATRateCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.DiscountVND // objDiscountVND
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.Remark // objRemark
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecPrice_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice
            ////
            , out RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecPrice.Tid;
            objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecPrice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecPrice_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecPrice_Update;
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

                #region // OS_PrdCenter_Mst_SpecPrice_Update:
                mdsResult = OS_PrdCenter_Mst_SpecPrice_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecPrice.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //////
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.UnitCode // objUnitCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.BuyPrice // objBuyPrice
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.SellPrice // objSellPrice
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.DiscountVND // objDiscountVND
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.VATRateCode // objDiscountVND
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.FlagActive // objFlagActive
                                                                                //////
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet WAS_OS_PrdCenter_Mst_SpecPrice_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice
            ////
            , out RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_SpecPrice.Tid;
            objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_SpecPrice.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_SpecPrice_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_SpecPrice_Delete;
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

                #region // OS_PrdCenter_Mst_SpecPrice_Delete:
                mdsResult = OS_PrdCenter_Mst_SpecPrice_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_SpecPrice.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //////
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.Mst_SpecPrice.UnitCode // objUnitCode
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

        #region // Mst_Brand:
        public DataSet OS_PrdCenter_Mst_Brand_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strOrgID
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Brand
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Brand", strRt_Cols_Mst_Brand
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Brand> lst_Mst_Brand = new List<OS_PrdCenter_Mst_Brand>();
                ////
                bool bGet_Mst_Brand = (strRt_Cols_Mst_Brand != null && strRt_Cols_Mst_Brand.Length > 0);
                //bool bGet_Mst_BrandDtl = (objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl != null && objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Brand = strRt_Cols_Mst_Brand,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Get(objRQ_OS_PrdCenter_Mst_Brand);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Brand.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Brand)
                    {
                        ////
                        DataTable dt_Mst_Brand = new DataTable();
                        dt_Mst_Brand = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Brand>(objRT_OS_PrdCenter_Mst_Brand.Lst_Mst_Brand, "Mst_Brand");
                        dsGetData.Tables.Add(dt_Mst_Brand.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Brand_Get_New201113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strOrgID
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_Brand
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Brand", strRt_Cols_Mst_Brand
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Brand> lst_Mst_Brand = new List<OS_PrdCenter_Mst_Brand>();
                ////
                bool bGet_Mst_Brand = (strRt_Cols_Mst_Brand != null && strRt_Cols_Mst_Brand.Length > 0);
                //bool bGet_Mst_BrandDtl = (objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl != null && objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Brand = strRt_Cols_Mst_Brand,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Get(objRQ_OS_PrdCenter_Mst_Brand);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Brand.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Brand)
                    {
                        ////
                        DataTable dt_Mst_Brand = new DataTable();
                        dt_Mst_Brand = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Brand>(objRT_OS_PrdCenter_Mst_Brand.Lst_Mst_Brand, "Mst_Brand");
                        dsGetData.Tables.Add(dt_Mst_Brand.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Brand_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            , object objBrandName
            , object objNetworkBrandCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
                , "objBrandName", objBrandName
                , "objNetworkBrandCode", objNetworkBrandCode
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);

                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Brand.BrandName = objBrandName;
                    objOS_PrdCenter_Mst_Brand.NetworkBrandCode = objNetworkBrandCode;
                    objOS_PrdCenter_Mst_Brand.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Create(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet OS_PrdCenter_Mst_Brand_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            , object objBrandName
            , object objNetworkBrandCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
                , "objBrandName", objBrandName
                , "objNetworkBrandCode", objNetworkBrandCode
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);

                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Brand.BrandName = objBrandName;
                    objOS_PrdCenter_Mst_Brand.NetworkBrandCode = objNetworkBrandCode;
                    objOS_PrdCenter_Mst_Brand.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Create(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet OS_PrdCenter_Mst_Brand_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            , object objBrandName
            , object objNetworkBrandCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
                , "objBrandName", objBrandName
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Brand.BrandName = objBrandName;
                    objOS_PrdCenter_Mst_Brand.NetworkBrandCode = objNetworkBrandCode;
                    objOS_PrdCenter_Mst_Brand.Remark = objRemark;
                    objOS_PrdCenter_Mst_Brand.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Update(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet OS_PrdCenter_Mst_Brand_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            , object objBrandName
            , object objNetworkBrandCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
                , "objBrandName", objBrandName
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Brand.BrandName = objBrandName;
                    objOS_PrdCenter_Mst_Brand.NetworkBrandCode = objNetworkBrandCode;
                    objOS_PrdCenter_Mst_Brand.Remark = objRemark;
                    objOS_PrdCenter_Mst_Brand.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Update(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet OS_PrdCenter_Mst_Brand_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Delete(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet OS_PrdCenter_Mst_Brand_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objBrandCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Brand_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Brand_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objBrandCode", objBrandCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Brand objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand();
                    objOS_PrdCenter_Mst_Brand.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Brand.BrandCode = objBrandCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Brand()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Brand = objOS_PrdCenter_Mst_Brand,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Brand = OS_PrdCenter_Mst_BrandService.Instance.WA_OS_PrdCenter_Mst_Brand_Delete(objRQ_OS_PrdCenter_Mst_Brand);

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

        public DataSet WAS_OS_PrdCenter_Mst_Brand_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand
            ////
            , out RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Brand.Tid;
            objRT_OS_PrdCenter_Mst_Brand = new RT_OS_PrdCenter_Mst_Brand();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Brand.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Brand_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Brand_Get;
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
                List<OS_PrdCenter_Mst_Brand> lst_Mst_Brand = new List<OS_PrdCenter_Mst_Brand>();
                bool bGet_Mst_Brand = (objRQ_OS_PrdCenter_Mst_Brand.Rt_Cols_Mst_Brand != null && objRQ_OS_PrdCenter_Mst_Brand.Rt_Cols_Mst_Brand.Length > 0);
                //bool bGet_Mst_BrandDtl = (objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl != null && objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_Brand_Get:
                mdsResult = OS_PrdCenter_Mst_Brand_Get_New201113(
                    objRQ_OS_PrdCenter_Mst_Brand.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Brand.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Brand.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_Brand.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_Brand.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_Brand.Rt_Cols_Mst_Brand // strRt_Cols_Mst_Brand
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_Brand.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Brand)
                    {
                        ////
                        DataTable dt_Mst_Brand = mdsResult.Tables["Mst_Brand"].Copy();
                        lst_Mst_Brand = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_Brand>(dt_Mst_Brand);
                        objRT_OS_PrdCenter_Mst_Brand.Lst_Mst_Brand = lst_Mst_Brand;
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

        public DataSet WAS_OS_PrdCenter_Mst_Brand_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand
            ////
            , out RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Brand.Tid;
            objRT_OS_PrdCenter_Mst_Brand = new RT_OS_PrdCenter_Mst_Brand();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Brand.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Brand_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Brand_Create;
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

                #region // OS_PrdCenter_Mst_Brand_Create:
                mdsResult = OS_PrdCenter_Mst_Brand_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_Brand.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Brand.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.BrandCode // objBrandCode
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.BrandName // objBrandName
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.NetworkBrandCode // objNetworkBrandCode
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_Brand_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand
            ////
            , out RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Brand.Tid;
            objRT_OS_PrdCenter_Mst_Brand = new RT_OS_PrdCenter_Mst_Brand();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Brand.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Brand_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Brand_Update;
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

                #region // OS_PrdCenter_Mst_Brand_Update:
                mdsResult = OS_PrdCenter_Mst_Brand_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_Brand.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Brand.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.BrandCode // objBrandCode
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.BrandName // objBrandName
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.NetworkBrandCode // objNetworkBrandCode
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.FlagActive // objRemark
                                                                        ///
                    , objRQ_OS_PrdCenter_Mst_Brand.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_Brand_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand
            ////
            , out RT_OS_PrdCenter_Mst_Brand objRT_OS_PrdCenter_Mst_Brand
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Brand.Tid;
            objRT_OS_PrdCenter_Mst_Brand = new RT_OS_PrdCenter_Mst_Brand();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Brand.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Brand_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Brand_Delete;
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

                #region // OS_PrdCenter_Mst_Brand_Delete:
                mdsResult = OS_PrdCenter_Mst_Brand_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_Brand.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Brand.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Brand.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Brand.Mst_Brand.BrandCode // objBrandCode
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

        #region // Mst_Model:
        public DataSet OS_PrdCenter_Mst_Model_Get(
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
            , string strRt_Cols_Mst_Model
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Model", strRt_Cols_Mst_Model
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Model> lst_Mst_Model = new List<OS_PrdCenter_Mst_Model>();
                ////
                bool bGet_Mst_Model = (strRt_Cols_Mst_Model != null && strRt_Cols_Mst_Model.Length > 0);
                //bool bGet_Mst_ModelDtl = (objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl != null && objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // WA_Mst_Model_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Model = strRt_Cols_Mst_Model,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Get(objRQ_OS_PrdCenter_Mst_Model);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Model.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Model)
                    {
                        ////
                        DataTable dt_Mst_Model = new DataTable();
                        dt_Mst_Model = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model.Lst_Mst_Model, "Mst_Model");
                        dsGetData.Tables.Add(dt_Mst_Model.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Model_Get_New20191113(
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
            , string strRt_Cols_Mst_Model
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Model", strRt_Cols_Mst_Model
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Model> lst_Mst_Model = new List<OS_PrdCenter_Mst_Model>();
                ////
                bool bGet_Mst_Model = (strRt_Cols_Mst_Model != null && strRt_Cols_Mst_Model.Length > 0);
                //bool bGet_Mst_ModelDtl = (objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl != null && objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl.Length > 0);
                ////
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // WA_Mst_Model_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Mst_Model = strRt_Cols_Mst_Model,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Get(objRQ_OS_PrdCenter_Mst_Model);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Model.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Model)
                    {
                        ////
                        DataTable dt_Mst_Model = new DataTable();
                        dt_Mst_Model = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model.Lst_Mst_Model, "Mst_Model");
                        dsGetData.Tables.Add(dt_Mst_Model.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Model_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            , object objModelName
            , object objOrgModelCode
            , object objBrandCode
            , object objNetworkModelCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
                , "objModelName", objModelName
                , "objOrgModelCode", objOrgModelCode
                , "objBrandCode", objBrandCode
                , "objNetworkModelCode", objNetworkModelCode
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Model.ModelName = objModelName;
                    objOS_PrdCenter_Mst_Model.OrgModelCode = objOrgModelCode;
                    objOS_PrdCenter_Mst_Model.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Model.NetworkModelCode = objNetworkModelCode;
                    objOS_PrdCenter_Mst_Model.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Create(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet OS_PrdCenter_Mst_Model_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            , object objModelName
            , object objOrgModelCode
            , object objBrandCode
            , object objNetworkModelCode
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
                , "objModelName", objModelName
                , "objOrgModelCode", objOrgModelCode
                , "objBrandCode", objBrandCode
                , "objNetworkModelCode", objNetworkModelCode
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Model.ModelName = objModelName;
                    objOS_PrdCenter_Mst_Model.OrgModelCode = objOrgModelCode;
                    objOS_PrdCenter_Mst_Model.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Model.NetworkModelCode = objNetworkModelCode;
                    objOS_PrdCenter_Mst_Model.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Create(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet OS_PrdCenter_Mst_Model_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            , object objModelName
            , object objOrgModelCode
            , object objBrandCode
            , object objNetworkModelCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
                , "objModelName", objModelName
                , "objOrgModelCode", objOrgModelCode
                , "objBrandCode", objBrandCode
                , "objNetworkModelCode", objNetworkModelCode
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Model.ModelName = objModelName;
                    objOS_PrdCenter_Mst_Model.OrgModelCode = objOrgModelCode;
                    objOS_PrdCenter_Mst_Model.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Model.NetworkModelCode = objNetworkModelCode;
                    objOS_PrdCenter_Mst_Model.Remark = objRemark;
                    objOS_PrdCenter_Mst_Model.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Update(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet OS_PrdCenter_Mst_Model_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            , object objModelName
            , object objOrgModelCode
            , object objBrandCode
            , object objNetworkModelCode
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
                , "objModelName", objModelName
                , "objOrgModelCode", objOrgModelCode
                , "objBrandCode", objBrandCode
                , "objNetworkModelCode", objNetworkModelCode
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    objOS_PrdCenter_Mst_Model.ModelName = objModelName;
                    objOS_PrdCenter_Mst_Model.OrgModelCode = objOrgModelCode;
                    objOS_PrdCenter_Mst_Model.BrandCode = objBrandCode;
                    objOS_PrdCenter_Mst_Model.NetworkModelCode = objNetworkModelCode;
                    objOS_PrdCenter_Mst_Model.Remark = objRemark;
                    objOS_PrdCenter_Mst_Model.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Update(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet OS_PrdCenter_Mst_Model_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Delete(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet OS_PrdCenter_Mst_Model_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objModelCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Model_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Model_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objModelCode", objModelCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Model objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model();
                    objOS_PrdCenter_Mst_Model.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Model.ModelCode = objModelCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model = new RQ_OS_PrdCenter_Mst_Model()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Model = objOS_PrdCenter_Mst_Model,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Model = OS_PrdCenter_Mst_ModelService.Instance.WA_OS_PrdCenter_Mst_Model_Delete(objRQ_OS_PrdCenter_Mst_Model);

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

        public DataSet WAS_OS_PrdCenter_Mst_Model_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model
            ////
            , out RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Model.Tid;
            objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Model_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Model_Get;
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
                List<OS_PrdCenter_Mst_Model> lst_Mst_Model = new List<OS_PrdCenter_Mst_Model>();
                bool bGet_Mst_Model = (objRQ_OS_PrdCenter_Mst_Model.Rt_Cols_Mst_Model != null && objRQ_OS_PrdCenter_Mst_Model.Rt_Cols_Mst_Model.Length > 0);
                //bool bGet_Mst_ModelDtl = (objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl != null && objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_Model_Get:
                mdsResult = OS_PrdCenter_Mst_Model_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_Model.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Model.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_Model.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_Model.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_Model.Rt_Cols_Mst_Model // strRt_Cols_Mst_Model
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_Model.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Model)
                    {
                        ////
                        DataTable dt_Mst_Model = mdsResult.Tables["Mst_Model"].Copy();
                        lst_Mst_Model = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_Model>(dt_Mst_Model);
                        objRT_OS_PrdCenter_Mst_Model.Lst_Mst_Model = lst_Mst_Model;
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

        public DataSet WAS_OS_PrdCenter_Mst_Model_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model
            ////
            , out RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Model.Tid;
            objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Model_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Model_Create;
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

                #region // OS_PrdCenter_Mst_Model_Creare:
                mdsResult = OS_PrdCenter_Mst_Model_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_Model.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.ModelCode // objModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.ModelName // objModelName
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.OrgModelCode // objOrgModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.BrandCode // objBrandCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.NetworkModelCode // objNetworkModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_Model_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model
            ////
            , out RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Model.Tid;
            objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Model_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Model_Update;
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

                #region // OS_PrdCenter_Mst_Model_Update:
                mdsResult = OS_PrdCenter_Mst_Model_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_Model.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.ModelCode // objModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.ModelName // objModelName
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.OrgModelCode // objOrgModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.BrandCode // objBrandCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.NetworkModelCode // objNetworkModelCode
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.Remark // objRemark
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.FlagActive // objFlagActive
                                                                        ////
                    , objRQ_OS_PrdCenter_Mst_Model.Ft_Cols_Upd // objFt_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_Model_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model
            ////
            , out RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Model.Tid;
            objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Model_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Model_Delete;
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

                #region // OS_PrdCenter_Mst_Model_Delete:
                mdsResult = OS_PrdCenter_Mst_Model_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_Model.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Mst_Model.Mst_Model.ModelCode // objModelCode
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

        #region // Mst_Unit:
        public DataSet OS_PrdCenter_Mst_Unit_Get(
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
            , string strRt_Cols_Mst_Unit
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Brand", strRt_Cols_Mst_Unit
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Unit> lst_Mst_Unit = new List<OS_PrdCenter_Mst_Unit>();
                ////
                bool bGet_Mst_Unit = (strRt_Cols_Mst_Unit != null && strRt_Cols_Mst_Unit.Length > 0);
                //bool bGet_Mst_BrandDtl = (objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl != null && objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_Unit = strRt_Cols_Mst_Unit,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Get(objRQ_OS_PrdCenter_Mst_Brand);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Unit.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Unit)
                    {
                        ////
                        DataTable dt_Mst_Unit = new DataTable();
                        dt_Mst_Unit = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit.Lst_Mst_Unit, "Mst_Unit");
                        dsGetData.Tables.Add(dt_Mst_Unit.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Unit_Get_New20191113(
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
            , string strRt_Cols_Mst_Unit
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Brand", strRt_Cols_Mst_Unit
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_Unit> lst_Mst_Unit = new List<OS_PrdCenter_Mst_Unit>();
                ////
                bool bGet_Mst_Unit = (strRt_Cols_Mst_Unit != null && strRt_Cols_Mst_Unit.Length > 0);
                //bool bGet_Mst_BrandDtl = (objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl != null && objRQ_Mst_Brand.Rt_Cols_Mst_BrandDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Brand = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_Unit = strRt_Cols_Mst_Unit,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Get(objRQ_OS_PrdCenter_Mst_Brand);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_Unit.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_Unit)
                    {
                        ////
                        DataTable dt_Mst_Unit = new DataTable();
                        dt_Mst_Unit = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit.Lst_Mst_Unit, "Mst_Unit");
                        dsGetData.Tables.Add(dt_Mst_Unit.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_Unit_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            , object objUnitName
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
                , "objUnitName", objUnitName
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_Unit.UnitName = objUnitName;
                    objOS_PrdCenter_Mst_Unit.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Create(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet OS_PrdCenter_Mst_Unit_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            , object objUnitName
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
                , "objUnitName", objUnitName
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_Unit.UnitName = objUnitName;
                    objOS_PrdCenter_Mst_Unit.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Create(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet OS_PrdCenter_Mst_Unit_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            , object objUnitName
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
                , "objUnitName", objUnitName
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_Unit.UnitName = objUnitName;
                    objOS_PrdCenter_Mst_Unit.Remark = objRemark;
                    objOS_PrdCenter_Mst_Unit.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Update(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet OS_PrdCenter_Mst_Unit_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            , object objUnitName
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
                , "objUnitName", objUnitName
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    objOS_PrdCenter_Mst_Unit.UnitName = objUnitName;
                    objOS_PrdCenter_Mst_Unit.Remark = objRemark;
                    objOS_PrdCenter_Mst_Unit.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Update(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet OS_PrdCenter_Mst_Unit_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Delete(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet OS_PrdCenter_Mst_Unit_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUnitCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Unit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Unit_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objUnitCode", objUnitCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Unit objOS_PrdCenter_Mst_Unit = new OS_PrdCenter_Mst_Unit();
                    objOS_PrdCenter_Mst_Unit.UnitCode = objUnitCode;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit = new RQ_OS_PrdCenter_Mst_Unit()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_Unit = objOS_PrdCenter_Mst_Unit,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Unit = OS_PrdCenter_Mst_UnitService.Instance.WA_OS_PrdCenter_Mst_Unit_Delete(objRQ_OS_PrdCenter_Mst_Unit);

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

        public DataSet WAS_OS_PrdCenter_Mst_Unit_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit
            ////
            , out RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Unit.Tid;
            objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Unit_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Unit_Get;
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
                List<OS_PrdCenter_Mst_Unit> lstOS_PrdCenter_Mst_Unit = new List<OS_PrdCenter_Mst_Unit>();
                bool bGet_Mst_Unit = (objRQ_OS_PrdCenter_Mst_Unit.Rt_Cols_Mst_Unit != null && objRQ_OS_PrdCenter_Mst_Unit.Rt_Cols_Mst_Unit.Length > 0);
                //bool bGet_Mst_ModelDtl = (objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl != null && objRQ_Mst_Model.Rt_Cols_Mst_ModelDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_Unit_Get:
                mdsResult = OS_PrdCenter_Mst_Unit_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_Unit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Unit.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_Unit.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_Unit.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_Unit.Rt_Cols_Mst_Unit // strRt_Cols_Mst_Unit
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_Unit.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_Unit)
                    {
                        ////
                        DataTable dt_Mst_Unit = mdsResult.Tables["Mst_Unit"].Copy();
                        lstOS_PrdCenter_Mst_Unit = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_Unit>(dt_Mst_Unit);
                        objRT_OS_PrdCenter_Mst_Unit.Lst_Mst_Unit = lstOS_PrdCenter_Mst_Unit;
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

        public DataSet WAS_OS_PrdCenter_Mst_Unit_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit
            ////
            , out RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Unit.Tid;
            objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Unit_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Unit_Create;
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

                #region // OS_PrdCenter_Mst_Unit_Create:
                mdsResult = OS_PrdCenter_Mst_Unit_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_Unit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.UnitCode // objUnitCode
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.UnitName // objUnitName
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_Unit_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit
            ////
            , out RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Unit.Tid;
            objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Unit_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Unit_Update;
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

                #region // OS_PrdCenter_Mst_Unit_Update:
                mdsResult = OS_PrdCenter_Mst_Unit_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_Unit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.UnitCode // objUnitCode
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.UnitName // objUnitName
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.Remark // objRemark
                    ////
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.FlagActive // objFlagActive
                    ////
                    , objRQ_OS_PrdCenter_Mst_Unit.Ft_Cols_Upd // objFt_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_Unit_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit
            ////
            , out RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_Unit.Tid;
            objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Model.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_Unit_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_Unit_Delete;
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

                #region // OS_PrdCenter_Mst_Unit_Delete:
                mdsResult = OS_PrdCenter_Mst_Unit_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_Unit.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_Unit.Mst_Unit.UnitCode // objUnitCode
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

        #region // Mst_VATRate:
        public DataSet OS_PrdCenter_Mst_VATRate_Get(
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
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Get;
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_VATRate> lst_Mst_VATRate = new List<OS_PrdCenter_Mst_VATRate>();
                ////
                bool bGet_Mst_VATRate = (strRt_Cols_Mst_VATRate != null && strRt_Cols_Mst_VATRate.Length > 0);
                //bool bGet_Mst_VATRateDtl = (objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl != null && objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_VATRate = strRt_Cols_Mst_VATRate,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Get(objRQ_OS_PrdCenter_Mst_VATRate);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_VATRate.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_VATRate)
                    {
                        ////
                        DataTable dt_Mst_VATRate = new DataTable();
                        dt_Mst_VATRate = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_VATRate>(objRT_OS_PrdCenter_Mst_VATRate.Lst_Mst_VATRate, "Mst_VATRate");
                        dsGetData.Tables.Add(dt_Mst_VATRate.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_VATRate_Get_New20191113(
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
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Get;
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_VATRate> lst_Mst_VATRate = new List<OS_PrdCenter_Mst_VATRate>();
                ////
                bool bGet_Mst_VATRate = (strRt_Cols_Mst_VATRate != null && strRt_Cols_Mst_VATRate.Length > 0);
                //bool bGet_Mst_VATRateDtl = (objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl != null && objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_VATRate = strRt_Cols_Mst_VATRate,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Get(objRQ_OS_PrdCenter_Mst_VATRate);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_VATRate.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_VATRate)
                    {
                        ////
                        DataTable dt_Mst_VATRate = new DataTable();
                        dt_Mst_VATRate = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_VATRate>(objRT_OS_PrdCenter_Mst_VATRate.Lst_Mst_VATRate, "Mst_VATRate");
                        dsGetData.Tables.Add(dt_Mst_VATRate.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_VATRate_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            , object objVATRate
            , object objVATDesc
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
                , "objVATRateName", objVATRate
                , "objVATDesc", objVATDesc
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_VATRate.VATRate = objVATRate;
                    objOS_PrdCenter_Mst_VATRate.VATDesc = objVATDesc;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Create(objRQ_OS_PrdCenter_Mst_VATRate);

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

        public DataSet OS_PrdCenter_Mst_VATRate_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            , object objVATRate
            , object objVATDesc
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
                , "objVATRateName", objVATRate
                , "objVATDesc", objVATDesc
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_VATRate.VATRate = objVATRate;
                    objOS_PrdCenter_Mst_VATRate.VATDesc = objVATDesc;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Create(objRQ_OS_PrdCenter_Mst_VATRate);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_VATRate_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            , object objVATRate
            , object objVATDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
                , "objVATRateName", objVATRate
                , "objVATDesc", objVATDesc
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_VATRate.VATRate = objVATRate;
                    objOS_PrdCenter_Mst_VATRate.VATDesc = objVATDesc;
                    objOS_PrdCenter_Mst_VATRate.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Update(objRQ_OS_PrdCenter_Mst_VATRate);

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

        public DataSet OS_PrdCenter_Mst_VATRate_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            , object objVATRate
            , object objVATDesc
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
                , "objVATRateName", objVATRate
                , "objVATDesc", objVATDesc
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    objOS_PrdCenter_Mst_VATRate.VATRate = objVATRate;
                    objOS_PrdCenter_Mst_VATRate.VATDesc = objVATDesc;
                    objOS_PrdCenter_Mst_VATRate.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Update(objRQ_OS_PrdCenter_Mst_VATRate);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Mst_VATRate_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Delete(objRQ_OS_PrdCenter_Mst_VATRate);

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

        public DataSet OS_PrdCenter_Mst_VATRate_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objVATRateCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_VATRate_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_VATRate_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objVATRateCode", objVATRateCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_VATRate objOS_PrdCenter_Mst_VATRate = new OS_PrdCenter_Mst_VATRate();
                    objOS_PrdCenter_Mst_VATRate.VATRateCode = objVATRateCode;
                    #endregion

                    #region // WA_Mst_VATRate_Get:
                    RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate = new RQ_OS_PrdCenter_Mst_VATRate()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_VATRate = objOS_PrdCenter_Mst_VATRate,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_VATRate = OS_PrdCenter_Mst_VATRateService.Instance.WA_OS_PrdCenter_Mst_VATRate_Delete(objRQ_OS_PrdCenter_Mst_VATRate);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Mst_VATRate_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate
            ////
            , out RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_VATRate.Tid;
            objRT_OS_PrdCenter_Mst_VATRate = new RT_OS_PrdCenter_Mst_VATRate();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_VATRate_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_VATRate_Get;
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
                List<OS_PrdCenter_Mst_VATRate> lst_Mst_VATRate = new List<OS_PrdCenter_Mst_VATRate>();
                bool bGet_Mst_VATRate = (objRQ_OS_PrdCenter_Mst_VATRate.Rt_Cols_Mst_VATRate != null && objRQ_OS_PrdCenter_Mst_VATRate.Rt_Cols_Mst_VATRate.Length > 0);
                //bool bGet_Mst_VATRateDtl = (objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl != null && objRQ_Mst_VATRate.Rt_Cols_Mst_VATRateDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Mst_VATRate_Get:
                mdsResult = OS_PrdCenter_Mst_VATRate_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_VATRate.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_VATRate.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_VATRate.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_VATRate.Ft_WhereClause // strFt_WhereClause
                                                                  //// Return:
                    , objRQ_OS_PrdCenter_Mst_VATRate.Rt_Cols_Mst_VATRate // strRt_Cols_Mst_VATRate
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Mst_VATRate.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Mst_VATRate)
                    {
                        ////
                        DataTable dt_Mst_VATRate = mdsResult.Tables["Mst_VATRate"].Copy();
                        lst_Mst_VATRate = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_VATRate>(dt_Mst_VATRate);
                        objRT_OS_PrdCenter_Mst_VATRate.Lst_Mst_VATRate = lst_Mst_VATRate;
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

        public DataSet WAS_OS_PrdCenter_Mst_VATRate_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate
            ////
            , out RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_VATRate.Tid;
            objRT_OS_PrdCenter_Mst_VATRate = new RT_OS_PrdCenter_Mst_VATRate();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_VATRate_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_VATRate_Create;
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

                #region // OS_PrdCenter_Mst_VATRate_Create:
                mdsResult = OS_PrdCenter_Mst_VATRate_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_VATRate.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATRateCode // objVATRateCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATRate // VATRate
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATDesc // objVATDesc
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

        public DataSet WAS_OS_PrdCenter_Mst_VATRate_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate
            ////
            , out RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_VATRate.Tid;
            objRT_OS_PrdCenter_Mst_VATRate = new RT_OS_PrdCenter_Mst_VATRate();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_VATRate_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_VATRate_Update;
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

                #region // OS_PrdCenter_Mst_VATRate_Create:
                mdsResult = OS_PrdCenter_Mst_VATRate_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_VATRate.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATRateCode // objVATRateCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATRate // objVATRateName
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATDesc // objVATDesc
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.FlagActive // objRemark
                                                                        ///
                    , objRQ_OS_PrdCenter_Mst_VATRate.Ft_Cols_Upd // Ft_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_VATRate_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate
            ////
            , out RT_OS_PrdCenter_Mst_VATRate objRT_OS_PrdCenter_Mst_VATRate
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_VATRate.Tid;
            objRT_OS_PrdCenter_Mst_VATRate = new RT_OS_PrdCenter_Mst_VATRate();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_VATRate_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_VATRate_Delete;
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

                #region // OS_PrdCenter_Mst_VATRate_Delete:
                mdsResult = OS_PrdCenter_Mst_VATRate_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_VATRate.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_VATRate.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_VATRate.Mst_VATRate.VATRateCode // objVATRateCode
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

        #region // Mst_CurrencyEx:
        public DataSet OS_PrdCenter_Mst_CurrencyEx_Get(
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
            , string strRt_Cols_Mst_CurrencyEx
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_CurrencyEx", strRt_Cols_Mst_CurrencyEx
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_CurrencyEx> lst_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                ////
                bool bGet_Mst_CurrencyEx = (strRt_Cols_Mst_CurrencyEx != null && strRt_Cols_Mst_CurrencyEx.Length > 0);
                //bool bGet_Mst_CurrencyExDtl = (objRQ_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyExDtl != null && objRQ_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyExDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_CurrencyEx = strRt_Cols_Mst_CurrencyEx,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Get(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_CurrencyEx.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_CurrencyEx)
                    {
                        ////
                        DataTable dt_Mst_CurrencyEx = new DataTable();
                        dt_Mst_CurrencyEx = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx.Lst_Mst_CurrencyEx, "Mst_CurrencyEx");
                        dsGetData.Tables.Add(dt_Mst_CurrencyEx.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Get_New20191113(
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
            , string strRt_Cols_Mst_CurrencyEx
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_CurrencyEx", strRt_Cols_Mst_CurrencyEx
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Mst_CurrencyEx> lst_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                ////
                bool bGet_Mst_CurrencyEx = (strRt_Cols_Mst_CurrencyEx != null && strRt_Cols_Mst_CurrencyEx.Length > 0);
                //bool bGet_Mst_CurrencyExDtl = (objRQ_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyExDtl != null && objRQ_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyExDtl.Length > 0);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Rt_Cols_Mst_CurrencyEx = strRt_Cols_Mst_CurrencyEx,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Get(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Mst_CurrencyEx.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Mst_CurrencyEx)
                    {
                        ////
                        DataTable dt_Mst_CurrencyEx = new DataTable();
                        dt_Mst_CurrencyEx = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx.Lst_Mst_CurrencyEx, "Mst_CurrencyEx");
                        dsGetData.Tables.Add(dt_Mst_CurrencyEx.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            , object objCurrencyName
            , object objBaseCurrencyCode
            , object objBuyRate
            , object objSellRate
            //, object objUpdatedTime
            , object objInterEx
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
                , "objCurrencyName", objCurrencyName
                , "objBaseCurrencyCode", objBaseCurrencyCode
                , "objBuyRate", objBuyRate
                , "objSellRate", objSellRate
                //, "objUpdatedTime", objUpdatedTime
                , "objInterEx", objInterEx
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyName = objCurrencyName;
                    objOS_PrdCenter_Mst_CurrencyEx.BaseCurrencyCode = objBaseCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.BuyRate = objBuyRate;
                    objOS_PrdCenter_Mst_CurrencyEx.SellRate = objSellRate;
                    objOS_PrdCenter_Mst_CurrencyEx.InterEx = objInterEx;
                    objOS_PrdCenter_Mst_CurrencyEx.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Create(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            , object objCurrencyName
            , object objBaseCurrencyCode
            , object objBuyRate
            , object objSellRate
            //, object objUpdatedTime
            , object objInterEx
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
                , "objCurrencyName", objCurrencyName
                , "objBaseCurrencyCode", objBaseCurrencyCode
                , "objBuyRate", objBuyRate
                , "objSellRate", objSellRate
                //, "objUpdatedTime", objUpdatedTime
                , "objInterEx", objInterEx
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyName = objCurrencyName;
                    objOS_PrdCenter_Mst_CurrencyEx.BaseCurrencyCode = objBaseCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.BuyRate = objBuyRate;
                    objOS_PrdCenter_Mst_CurrencyEx.SellRate = objSellRate;
                    objOS_PrdCenter_Mst_CurrencyEx.InterEx = objInterEx;
                    objOS_PrdCenter_Mst_CurrencyEx.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Create(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            , object objCurrencyName
            //, object objBaseCurrencyCode
            , object objBuyRate
            , object objSellRate
            //, object objUpdatedTime
            , object objInterEx
            , object objRemark
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
                , "objCurrencyName", objCurrencyName
                //, "objBaseCurrencyCode", objBaseCurrencyCode
                , "objBuyRate", objBuyRate
                , "objSellRate", objSellRate
                //, "objUpdatedTime", objUpdatedTime
                , "objInterEx", objInterEx
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyName = objCurrencyName;
                    //objOS_PrdCenter_Mst_CurrencyEx.BaseCurrencyCode = objBaseCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.BuyRate = objBuyRate;
                    objOS_PrdCenter_Mst_CurrencyEx.SellRate = objSellRate;
                    objOS_PrdCenter_Mst_CurrencyEx.InterEx = objInterEx;
                    objOS_PrdCenter_Mst_CurrencyEx.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                       Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Update(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            , object objCurrencyName
            //, object objBaseCurrencyCode
            , object objBuyRate
            , object objSellRate
            //, object objUpdatedTime
            , object objInterEx
            , object objRemark
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
                , "objCurrencyName", objCurrencyName
                //, "objBaseCurrencyCode", objBaseCurrencyCode
                , "objBuyRate", objBuyRate
                , "objSellRate", objSellRate
                //, "objUpdatedTime", objUpdatedTime
                , "objInterEx", objInterEx
                , "objRemark", objRemark
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyName = objCurrencyName;
                    //objOS_PrdCenter_Mst_CurrencyEx.BaseCurrencyCode = objBaseCurrencyCode;
                    objOS_PrdCenter_Mst_CurrencyEx.BuyRate = objBuyRate;
                    objOS_PrdCenter_Mst_CurrencyEx.SellRate = objSellRate;
                    objOS_PrdCenter_Mst_CurrencyEx.InterEx = objInterEx;
                    objOS_PrdCenter_Mst_CurrencyEx.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Update(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Delete(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet OS_PrdCenter_Mst_CurrencyEx_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCurrencyCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_CurrencyEx_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_CurrencyEx_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objCurrencyCode", objCurrencyCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_CurrencyEx objOS_PrdCenter_Mst_CurrencyEx = new OS_PrdCenter_Mst_CurrencyEx();
                    objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode = objCurrencyCode;
                    #endregion

                    #region // WA_Mst_CurrencyEx_Get:
                    RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx = new RQ_OS_PrdCenter_Mst_CurrencyEx()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Mst_CurrencyEx = objOS_PrdCenter_Mst_CurrencyEx,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_CurrencyEx = OS_PrdCenter_Mst_CurrencyExService.Instance.WA_OS_PrdCenter_Mst_CurrencyEx_Delete(objRQ_OS_PrdCenter_Mst_CurrencyEx);

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

        public DataSet WAS_OS_PrdCenter_Mst_CurrencyEx_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx
            ////
            , out RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid;
            objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_CurrencyEx_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_CurrencyEx_Get;
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
                List<OS_PrdCenter_Mst_CurrencyEx> lst_OS_PrdCenter_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                bool bGet_OS_PrdCenter_Mst_CurrencyEx = (objRQ_OS_PrdCenter_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyEx != null && objRQ_OS_PrdCenter_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyEx.Length > 0);
                #endregion

                #region // WS_OS_PrdCenter_Mst_CurrencyEx_Get:
                mdsResult = OS_PrdCenter_Mst_CurrencyEx_Get_New20191113(
                    objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Ft_WhereClause // strFt_WhereClause
                                                                       //// Return:
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Rt_Cols_Mst_CurrencyEx // strRt_Cols_Mst_CurrencyEx
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    if (bGet_OS_PrdCenter_Mst_CurrencyEx)
                    {
                        ////
                        DataTable dt_OS_PrdCenter_Mst_CurrencyEx = mdsResult.Tables["Mst_CurrencyEx"].Copy();
                        lst_OS_PrdCenter_Mst_CurrencyEx = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Mst_CurrencyEx>(dt_OS_PrdCenter_Mst_CurrencyEx);
                        objRT_OS_PrdCenter_Mst_CurrencyEx.Lst_Mst_CurrencyEx = lst_OS_PrdCenter_Mst_CurrencyEx;
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

        public DataSet WAS_OS_PrdCenter_Mst_CurrencyEx_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx
            ////
            , out RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid;
            objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_CurrencyEx_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_CurrencyEx_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "OS_PrdCenter_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<OS_PrdCenter_Mst_CurrencyEx> lst_OS_PrdCenter_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                //List<OS_PrdCenter_Mst_CurrencyExInGroup> lst_OS_PrdCenter_Mst_CurrencyExInGroup = new List<OS_PrdCenter_Mst_CurrencyExInGroup>();
                #endregion

                #region // OS_PrdCenter_Mst_CurrencyEx_Create:
                mdsResult = OS_PrdCenter_Mst_CurrencyEx_Create_New20191113(
                    objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.CurrencyCode // objCurrencyCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.CurrencyName // objCurrencyName
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.BaseCurrencyCode // objBaseCurrencyCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.BuyRate // objBuyRate
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.SellRate // objSellRate
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.InterEx // objInterEx
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.Remark // objRemark
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

        public DataSet WAS_OS_PrdCenter_Mst_CurrencyEx_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx
            ////
            , out RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid;
            objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_CurrencyEx_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_CurrencyEx_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "OS_PrdCenter_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<OS_PrdCenter_Mst_CurrencyEx> lst_OS_PrdCenter_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                //List<OS_PrdCenter_Mst_CurrencyExInGroup> lst_OS_PrdCenter_Mst_CurrencyExInGroup = new List<OS_PrdCenter_Mst_CurrencyExInGroup>();
                #endregion

                #region // OS_PrdCenter_Mst_CurrencyEx_Update:
                mdsResult = OS_PrdCenter_Mst_CurrencyEx_Update_New20191113(
                    objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.CurrencyCode // objCurrencyCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.CurrencyName // objCurrencyName
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.BuyRate // objBuyRate
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.SellRate // objSellRate
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.InterEx // objInterEx
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.Remark // objRemark
                                                                                           ////
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Ft_Cols_Upd// objFt_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Mst_CurrencyEx_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx
            ////
            , out RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid;
            objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Mst_CurrencyEx_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Mst_CurrencyEx_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "OS_PrdCenter_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_OS_PrdCenter_Mst_CurrencyEx.OS_PrdCenter_Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<OS_PrdCenter_Mst_CurrencyEx> lst_OS_PrdCenter_Mst_CurrencyEx = new List<OS_PrdCenter_Mst_CurrencyEx>();
                //List<OS_PrdCenter_Mst_CurrencyExInGroup> lst_OS_PrdCenter_Mst_CurrencyExInGroup = new List<OS_PrdCenter_Mst_CurrencyExInGroup>();
                #endregion

                #region // OS_PrdCenter_Mst_CurrencyEx_Delete:
                mdsResult = OS_PrdCenter_Mst_CurrencyEx_Delete_New20191113(
                    objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid // strTid
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.Mst_CurrencyEx.CurrencyCode // objCurrencyCode
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

        #region // Prd_PrdIDCustomField:
        public DataSet OS_PrdCenter_Prd_PrdIDCustomField_Get(
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
            , string strRt_Cols_Prd_PrdIDCustomField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_PrdIDCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_PrdIDCustomField_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Prd_PrdIDCustomField", strRt_Cols_Prd_PrdIDCustomField
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Prd_PrdIDCustomField> lst_Prd_PrdIDCustomField = new List<OS_PrdCenter_Prd_PrdIDCustomField>();
                ////
                bool bGet_Prd_PrdIDCustomField = (strRt_Cols_Prd_PrdIDCustomField != null && strRt_Cols_Prd_PrdIDCustomField.Length > 0);
                //bool bGet_Prd_PrdIDCustomFieldDtl = (objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl != null && objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl.Length > 0);
                
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
                {
                    #region // WA_Prd_PrdIDCustomField_Get:
                    RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new RQ_OS_PrdCenter_Prd_PrdIDCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Rt_Cols_Prd_PrdIDCustomField = strRt_Cols_Prd_PrdIDCustomField,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_OS_PrdCenter_Prd_PrdIDCustomField_Get(objRQ_OS_PrdCenter_Prd_PrdIDCustomField);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Prd_PrdIDCustomField.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Prd_PrdIDCustomField)
                    {
                        ////
                        DataTable dt_Prd_PrdIDCustomField = new DataTable();
                        dt_Prd_PrdIDCustomField = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Prd_PrdIDCustomField>(objRT_OS_PrdCenter_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField, "Prd_PrdIDCustomField");
                        dsGetData.Tables.Add(dt_Prd_PrdIDCustomField.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Prd_PrdIDCustomField_Get_New20191113(
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
            , string strRt_Cols_Prd_PrdIDCustomField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_PrdIDCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_PrdIDCustomField_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Prd_PrdIDCustomField", strRt_Cols_Prd_PrdIDCustomField
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Prd_PrdIDCustomField> lst_Prd_PrdIDCustomField = new List<OS_PrdCenter_Prd_PrdIDCustomField>();
                ////
                bool bGet_Prd_PrdIDCustomField = (strRt_Cols_Prd_PrdIDCustomField != null && strRt_Cols_Prd_PrdIDCustomField.Length > 0);
                //bool bGet_Prd_PrdIDCustomFieldDtl = (objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl != null && objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl.Length > 0);

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
                {
                    #region // WA_Prd_PrdIDCustomField_Get:
                    RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new RQ_OS_PrdCenter_Prd_PrdIDCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Rt_Cols_Prd_PrdIDCustomField = strRt_Cols_Prd_PrdIDCustomField,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_OS_PrdCenter_Prd_PrdIDCustomField_Get(objRQ_OS_PrdCenter_Prd_PrdIDCustomField);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Prd_PrdIDCustomField.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Prd_PrdIDCustomField)
                    {
                        ////
                        DataTable dt_Prd_PrdIDCustomField = new DataTable();
                        dt_Prd_PrdIDCustomField = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Prd_PrdIDCustomField>(objRT_OS_PrdCenter_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField, "Prd_PrdIDCustomField");
                        dsGetData.Tables.Add(dt_Prd_PrdIDCustomField.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Prd_PrdIDCustomField_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPrdCustomFieldCode
            , object objPrdCustomFieldName
            , object objDBPhysicalType
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_PrdIDCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_PrdIDCustomField_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objPrdCustomFieldCode", objPrdCustomFieldCode
                , "objPrdCustomFieldName", objPrdCustomFieldName
                , "objDBPhysicalType", objDBPhysicalType
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_PrdIDCustomField objOS_PrdCenter_Prd_PrdIDCustomField = new OS_PrdCenter_Prd_PrdIDCustomField();
                    objOS_PrdCenter_Prd_PrdIDCustomField.OrgID = strOrgID;
                    objOS_PrdCenter_Prd_PrdIDCustomField.PrdCustomFieldCode = objPrdCustomFieldCode;
                    objOS_PrdCenter_Prd_PrdIDCustomField.PrdCustomFieldName = objPrdCustomFieldName;
                    objOS_PrdCenter_Prd_PrdIDCustomField.Remark = objRemark;
                    objOS_PrdCenter_Prd_PrdIDCustomField.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Prd_PrdIDCustomField_Get:
                    RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new RQ_OS_PrdCenter_Prd_PrdIDCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Prd_PrdIDCustomField = objOS_PrdCenter_Prd_PrdIDCustomField,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_OS_PrdCenter_Prd_PrdIDCustomField_Update(objRQ_OS_PrdCenter_Prd_PrdIDCustomField);

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

        public DataSet OS_PrdCenter_Prd_PrdIDCustomField_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objPrdCustomFieldCode
            , object objPrdCustomFieldName
            , object objDBPhysicalType
            , object objRemark
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_PrdIDCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_PrdIDCustomField_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objPrdCustomFieldCode", objPrdCustomFieldCode
                , "objPrdCustomFieldName", objPrdCustomFieldName
                , "objDBPhysicalType", objDBPhysicalType
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_PrdIDCustomField objOS_PrdCenter_Prd_PrdIDCustomField = new OS_PrdCenter_Prd_PrdIDCustomField();
                    objOS_PrdCenter_Prd_PrdIDCustomField.OrgID = strOrgID;
                    objOS_PrdCenter_Prd_PrdIDCustomField.PrdCustomFieldCode = objPrdCustomFieldCode;
                    objOS_PrdCenter_Prd_PrdIDCustomField.PrdCustomFieldName = objPrdCustomFieldName;
                    objOS_PrdCenter_Prd_PrdIDCustomField.Remark = objRemark;
                    objOS_PrdCenter_Prd_PrdIDCustomField.FlagActive = objFlagActive;
                    #endregion

                    #region // WA_Prd_PrdIDCustomField_Get:
                    RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField = new RQ_OS_PrdCenter_Prd_PrdIDCustomField()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        Prd_PrdIDCustomField = objOS_PrdCenter_Prd_PrdIDCustomField,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_OS_PrdCenter_Prd_PrdIDCustomField_Update(objRQ_OS_PrdCenter_Prd_PrdIDCustomField);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Prd_PrdIDCustomField_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField
            ////
            , out RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid;
            objRT_OS_PrdCenter_Prd_PrdIDCustomField = new RT_OS_PrdCenter_Prd_PrdIDCustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_PrdIDCustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_PrdIDCustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_PrdIDCustomField_Get;
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
                List<OS_PrdCenter_Prd_PrdIDCustomField> lst_Prd_PrdIDCustomField = new List<OS_PrdCenter_Prd_PrdIDCustomField>();
                bool bGet_Prd_PrdIDCustomField = (objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomField != null && objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomField.Length > 0);
                //bool bGet_Prd_PrdIDCustomFieldDtl = (objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl != null && objRQ_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomFieldDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Prd_PrdIDCustomField_Get:
                mdsResult = OS_PrdCenter_Prd_PrdIDCustomField_Get_New20191113(
                    objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Ft_WhereClause // strFt_WhereClause
                                                                             //// Return:
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Rt_Cols_Prd_PrdIDCustomField // strRt_Cols_Prd_PrdIDCustomField
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Prd_PrdIDCustomField.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Prd_PrdIDCustomField)
                    {
                        ////
                        DataTable dt_Prd_PrdIDCustomField = mdsResult.Tables["Prd_PrdIDCustomField"].Copy();
                        lst_Prd_PrdIDCustomField = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Prd_PrdIDCustomField>(dt_Prd_PrdIDCustomField);
                        objRT_OS_PrdCenter_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField = lst_Prd_PrdIDCustomField;
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

        public DataSet WAS_OS_PrdCenter_Prd_PrdIDCustomField_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField
            ////
            , out RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid;
            objRT_OS_PrdCenter_Prd_PrdIDCustomField = new RT_OS_PrdCenter_Prd_PrdIDCustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_PrdIDCustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_PrdIDCustomField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_PrdIDCustomField_Update;
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

                #region // OS_PrdCenter_Prd_PrdIDCustomField_Update:
                mdsResult = OS_PrdCenter_Prd_PrdIDCustomField_Update_New20191113(
                    objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Prd_PrdIDCustomField.PrdCustomFieldCode // objPrdCustomFieldCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Prd_PrdIDCustomField.PrdCustomFieldName // objPrdCustomFieldName
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Prd_PrdIDCustomField.DBPhysicalType // objDBPhysicalType
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Prd_PrdIDCustomField.Remark // objRemark
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Prd_PrdIDCustomField.FlagActive // objRemark
                                                                                              ///
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Ft_Cols_Upd // Ft_Cols_Upd
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

        #region // Prd_ProductID:
        public DataSet OS_PrdCenter_Prd_ProductID_Get(
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
            , string strRt_Cols_Prd_ProductID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Prd_ProductID", strRt_Cols_Prd_ProductID
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Prd_ProductID> lst_Prd_ProductID = new List<OS_PrdCenter_Prd_ProductID>();
                ////
                bool bGet_Prd_ProductID = (strRt_Cols_Prd_ProductID != null && strRt_Cols_Prd_ProductID.Length > 0);
                //bool bGet_Prd_ProductIDDtl = (objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl != null && objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl.Length > 0);

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Prd_ProductID = strRt_Cols_Prd_ProductID,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Get(objRQ_OS_PrdCenter_Prd_ProductID);

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

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Prd_ProductID.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Prd_ProductID)
                    {
                        ////
                        DataTable dt_Prd_ProductID = new DataTable();
                        dt_Prd_ProductID = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID.Lst_Prd_ProductID, "Prd_ProductID");
                        dsGetData.Tables.Add(dt_Prd_ProductID.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Prd_ProductID_Get_New20191113(
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
            , string strRt_Cols_Prd_ProductID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Get";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Prd_ProductID", strRt_Cols_Prd_ProductID
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<OS_PrdCenter_Prd_ProductID> lst_Prd_ProductID = new List<OS_PrdCenter_Prd_ProductID>();
                ////
                bool bGet_Prd_ProductID = (strRt_Cols_Prd_ProductID != null && strRt_Cols_Prd_ProductID.Length > 0);
                //bool bGet_Prd_ProductIDDtl = (objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl != null && objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl.Length > 0);

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                ////
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        OrgID = strOrgID,
                        Tid = strTid,
                        Rt_Cols_Prd_ProductID = strRt_Cols_Prd_ProductID,
                        Ft_RecordStart = strFt_RecordStart,
                        Ft_RecordCount = strFt_RecordCount,
                        Ft_WhereClause = strFt_WhereClause
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Get(objRQ_OS_PrdCenter_Prd_ProductID);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    #endregion
                }
                #endregion

                #region // GetData:
                DataSet dsGetData = new DataSet();
                {
                    ////
                    DataTable dt_MySummaryTable = new DataTable();
                    List<MySummaryTable> lstMySummaryTable = new List<MySummaryTable>();
                    lstMySummaryTable.Add(objRT_OS_PrdCenter_Prd_ProductID.MySummaryTable);
                    dt_MySummaryTable = TUtils.DataTableCmUtils.ToDataTable<MySummaryTable>(lstMySummaryTable, "MySummaryTable");
                    dsGetData.Tables.Add(dt_MySummaryTable.Copy());

                    ////
                    if (bGet_Prd_ProductID)
                    {
                        ////
                        DataTable dt_Prd_ProductID = new DataTable();
                        dt_Prd_ProductID = TUtils.DataTableCmUtils.ToDataTable<OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID.Lst_Prd_ProductID, "Prd_ProductID");
                        dsGetData.Tables.Add(dt_Prd_ProductID.Copy());
                    }
                }
                ////
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

        public DataSet OS_PrdCenter_Prd_ProductID_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            , object objProductionDate
            , object objLOTNo
            , object objBuyDate
            , object objSecretNo
            , object objWarrantyStartDate
            , object objWarrantyExpiredDate
            , object objWarrantyDuration
            , object objRefNo1
            , object objRefBiz1
            , object objRefNo2
            , object objRefBiz2
            , object objRefNo3
            , object objRefBiz3
            , object objBuyer
            , object objNetworkProductIDCode
            , object objProductIDStatus
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
                , "objProductionDate", objProductionDate
                , "objLOTNo", objLOTNo
                , "objBuyDate", objBuyDate
                , "objSecretNo", objSecretNo
                , "objWarrantyStartDate", objWarrantyStartDate
                , "objWarrantyExpiredDate", objWarrantyExpiredDate
                , "objWarrantyDuration", objWarrantyDuration
                , "objRefNo1", objRefNo1
                , "objRefBiz1", objRefBiz1
                , "objRefNo2", objRefNo2
                , "objRefBiz2", objRefBiz2
                , "objRefNo3", objRefNo3
                , "objRefBiz3", objRefBiz3
                , "objBuyer", objBuyer
                , "objNetworkModelCode", objNetworkProductIDCode
                , "objProductIDStatus", objProductIDStatus
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    objOS_PrdCenter_Prd_ProductID.ProductionDate = objProductionDate;
                    objOS_PrdCenter_Prd_ProductID.LOTNo = objLOTNo;
                    objOS_PrdCenter_Prd_ProductID.BuyDate = objBuyDate;
                    objOS_PrdCenter_Prd_ProductID.SecretNo = objSecretNo;
                    objOS_PrdCenter_Prd_ProductID.WarrantyStartDate = objWarrantyStartDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyExpiredDate = objWarrantyExpiredDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyDuration = objWarrantyDuration;
                    objOS_PrdCenter_Prd_ProductID.RefNo1 = objRefNo1;
                    objOS_PrdCenter_Prd_ProductID.RefBiz1 = objRefBiz1;
                    objOS_PrdCenter_Prd_ProductID.RefNo2 = objRefNo2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz2 = objRefBiz2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz3 = objRefBiz3;
                    objOS_PrdCenter_Prd_ProductID.Buyer = objBuyer;
                    objOS_PrdCenter_Prd_ProductID.NetworkProductIDCode = objNetworkProductIDCode;
                    objOS_PrdCenter_Prd_ProductID.ProductIDStatus = objProductIDStatus;
                    objOS_PrdCenter_Prd_ProductID.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Prd_ProductID.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Prd_ProductID.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Prd_ProductID.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Prd_ProductID.CustomField5 = objCustomField5;
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Create(objRQ_OS_PrdCenter_Prd_ProductID);

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

        public DataSet OS_PrdCenter_Prd_ProductID_Create_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            , object objProductionDate
            , object objLOTNo
            , object objBuyDate
            , object objSecretNo
            , object objWarrantyStartDate
            , object objWarrantyExpiredDate
            , object objWarrantyDuration
            , object objRefNo1
            , object objRefBiz1
            , object objRefNo2
            , object objRefBiz2
            , object objRefNo3
            , object objRefBiz3
            , object objBuyer
            , object objNetworkProductIDCode
            , object objProductIDStatus
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
                , "objProductionDate", objProductionDate
                , "objLOTNo", objLOTNo
                , "objBuyDate", objBuyDate
                , "objSecretNo", objSecretNo
                , "objWarrantyStartDate", objWarrantyStartDate
                , "objWarrantyExpiredDate", objWarrantyExpiredDate
                , "objWarrantyDuration", objWarrantyDuration
                , "objRefNo1", objRefNo1
                , "objRefBiz1", objRefBiz1
                , "objRefNo2", objRefNo2
                , "objRefBiz2", objRefBiz2
                , "objRefNo3", objRefNo3
                , "objRefBiz3", objRefBiz3
                , "objBuyer", objBuyer
                , "objNetworkModelCode", objNetworkProductIDCode
                , "objProductIDStatus", objProductIDStatus
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    objOS_PrdCenter_Prd_ProductID.ProductionDate = objProductionDate;
                    objOS_PrdCenter_Prd_ProductID.LOTNo = objLOTNo;
                    objOS_PrdCenter_Prd_ProductID.BuyDate = objBuyDate;
                    objOS_PrdCenter_Prd_ProductID.SecretNo = objSecretNo;
                    objOS_PrdCenter_Prd_ProductID.WarrantyStartDate = objWarrantyStartDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyExpiredDate = objWarrantyExpiredDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyDuration = objWarrantyDuration;
                    objOS_PrdCenter_Prd_ProductID.RefNo1 = objRefNo1;
                    objOS_PrdCenter_Prd_ProductID.RefBiz1 = objRefBiz1;
                    objOS_PrdCenter_Prd_ProductID.RefNo2 = objRefNo2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz2 = objRefBiz2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz3 = objRefBiz3;
                    objOS_PrdCenter_Prd_ProductID.Buyer = objBuyer;
                    objOS_PrdCenter_Prd_ProductID.NetworkProductIDCode = objNetworkProductIDCode;
                    objOS_PrdCenter_Prd_ProductID.ProductIDStatus = objProductIDStatus;
                    objOS_PrdCenter_Prd_ProductID.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Prd_ProductID.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Prd_ProductID.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Prd_ProductID.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Prd_ProductID.CustomField5 = objCustomField5;
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Create(objRQ_OS_PrdCenter_Prd_ProductID);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Prd_ProductID_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            , object objProductionDate
            , object objLOTNo
            , object objBuyDate
            , object objSecretNo
            , object objWarrantyStartDate
            , object objWarrantyExpiredDate
            , object objWarrantyDuration
            , object objRefNo1
            , object objRefBiz1
            , object objRefNo2
            , object objRefBiz2
            , object objRefNo3
            , object objRefBiz3
            , object objBuyer
            , object objNetworkProductIDCode
            , object objProductIDStatus
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
                , "objProductionDate", objProductionDate
                , "objLOTNo", objLOTNo
                , "objBuyDate", objBuyDate
                , "objSecretNo", objSecretNo
                , "objWarrantyStartDate", objWarrantyStartDate
                , "objWarrantyExpiredDate", objWarrantyExpiredDate
                , "objWarrantyDuration", objWarrantyDuration
                , "objRefNo1", objRefNo1
                , "objRefBiz1", objRefBiz1
                , "objRefNo2", objRefNo2
                , "objRefBiz2", objRefBiz2
                , "objRefNo3", objRefNo3
                , "objRefBiz3", objRefBiz3
                , "objBuyer", objBuyer
                , "objNetworkModelCode", objNetworkProductIDCode
                , "objProductIDStatus", objProductIDStatus
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    objOS_PrdCenter_Prd_ProductID.ProductionDate = objProductionDate;
                    objOS_PrdCenter_Prd_ProductID.LOTNo = objLOTNo;
                    objOS_PrdCenter_Prd_ProductID.BuyDate = objBuyDate;
                    objOS_PrdCenter_Prd_ProductID.SecretNo = objSecretNo;
                    objOS_PrdCenter_Prd_ProductID.WarrantyStartDate = objWarrantyStartDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyExpiredDate = objWarrantyExpiredDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyDuration = objWarrantyDuration;
                    objOS_PrdCenter_Prd_ProductID.RefNo1 = objRefNo1;
                    objOS_PrdCenter_Prd_ProductID.RefBiz1 = objRefBiz1;
                    objOS_PrdCenter_Prd_ProductID.RefNo2 = objRefNo2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz2 = objRefBiz2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz3 = objRefBiz3;
                    objOS_PrdCenter_Prd_ProductID.Buyer = objBuyer;
                    objOS_PrdCenter_Prd_ProductID.NetworkProductIDCode = objNetworkProductIDCode;
                    objOS_PrdCenter_Prd_ProductID.ProductIDStatus = objProductIDStatus;
                    objOS_PrdCenter_Prd_ProductID.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Prd_ProductID.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Prd_ProductID.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Prd_ProductID.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Prd_ProductID.CustomField5 = objCustomField5;
                    ////
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Update(objRQ_OS_PrdCenter_Prd_ProductID);

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

        public DataSet OS_PrdCenter_Prd_ProductID_Update_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            , object objProductionDate
            , object objLOTNo
            , object objBuyDate
            , object objSecretNo
            , object objWarrantyStartDate
            , object objWarrantyExpiredDate
            , object objWarrantyDuration
            , object objRefNo1
            , object objRefBiz1
            , object objRefNo2
            , object objRefBiz2
            , object objRefNo3
            , object objRefBiz3
            , object objBuyer
            , object objNetworkProductIDCode
            , object objProductIDStatus
            , object objCustomField1
            , object objCustomField2
            , object objCustomField3
            , object objCustomField4
            , object objCustomField5
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Update";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
                , "objProductionDate", objProductionDate
                , "objLOTNo", objLOTNo
                , "objBuyDate", objBuyDate
                , "objSecretNo", objSecretNo
                , "objWarrantyStartDate", objWarrantyStartDate
                , "objWarrantyExpiredDate", objWarrantyExpiredDate
                , "objWarrantyDuration", objWarrantyDuration
                , "objRefNo1", objRefNo1
                , "objRefBiz1", objRefBiz1
                , "objRefNo2", objRefNo2
                , "objRefBiz2", objRefBiz2
                , "objRefNo3", objRefNo3
                , "objRefBiz3", objRefBiz3
                , "objBuyer", objBuyer
                , "objNetworkModelCode", objNetworkProductIDCode
                , "objProductIDStatus", objProductIDStatus
                , "objCustomField1", objCustomField1
                , "objCustomField2", objCustomField2
                , "objCustomField3", objCustomField3
                , "objCustomField4", objCustomField4
                , "objCustomField5", objCustomField5
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
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    objOS_PrdCenter_Prd_ProductID.ProductionDate = objProductionDate;
                    objOS_PrdCenter_Prd_ProductID.LOTNo = objLOTNo;
                    objOS_PrdCenter_Prd_ProductID.BuyDate = objBuyDate;
                    objOS_PrdCenter_Prd_ProductID.SecretNo = objSecretNo;
                    objOS_PrdCenter_Prd_ProductID.WarrantyStartDate = objWarrantyStartDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyExpiredDate = objWarrantyExpiredDate;
                    objOS_PrdCenter_Prd_ProductID.WarrantyDuration = objWarrantyDuration;
                    objOS_PrdCenter_Prd_ProductID.RefNo1 = objRefNo1;
                    objOS_PrdCenter_Prd_ProductID.RefBiz1 = objRefBiz1;
                    objOS_PrdCenter_Prd_ProductID.RefNo2 = objRefNo2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz2 = objRefBiz2;
                    objOS_PrdCenter_Prd_ProductID.RefBiz3 = objRefBiz3;
                    objOS_PrdCenter_Prd_ProductID.Buyer = objBuyer;
                    objOS_PrdCenter_Prd_ProductID.NetworkProductIDCode = objNetworkProductIDCode;
                    objOS_PrdCenter_Prd_ProductID.ProductIDStatus = objProductIDStatus;
                    objOS_PrdCenter_Prd_ProductID.CustomField1 = objCustomField1;
                    objOS_PrdCenter_Prd_ProductID.CustomField2 = objCustomField2;
                    objOS_PrdCenter_Prd_ProductID.CustomField3 = objCustomField3;
                    objOS_PrdCenter_Prd_ProductID.CustomField4 = objCustomField4;
                    objOS_PrdCenter_Prd_ProductID.CustomField5 = objCustomField5;
                    ////
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                        Ft_Cols_Upd = objFt_Cols_Upd.ToString(),
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Update(objRQ_OS_PrdCenter_Prd_ProductID);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet OS_PrdCenter_Prd_ProductID_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    ////
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Delete(objRQ_OS_PrdCenter_Prd_ProductID);

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

        public DataSet OS_PrdCenter_Prd_ProductID_Delete_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objProductID
            , object objSpecCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Prd_ProductID_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Prd_ProductID_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "objOrgID", objOrgID
                , "objProductID", objProductID
                , "objSpecCode", objSpecCode
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
                {
                    #region // Init:
                    OS_PrdCenter_Prd_ProductID objOS_PrdCenter_Prd_ProductID = new OS_PrdCenter_Prd_ProductID();
                    objOS_PrdCenter_Prd_ProductID.OrgID = objOrgID;
                    objOS_PrdCenter_Prd_ProductID.ProductID = objProductID;
                    objOS_PrdCenter_Prd_ProductID.SpecCode = objSpecCode;
                    ////
                    #endregion

                    #region // WA_Prd_ProductID_Get:
                    RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID = new RQ_OS_PrdCenter_Prd_ProductID()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Prd_ProductID = objOS_PrdCenter_Prd_ProductID,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_OS_PrdCenter_Prd_ProductID_Delete(objRQ_OS_PrdCenter_Prd_ProductID);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        public DataSet WAS_OS_PrdCenter_Prd_ProductID_Get(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID
            ////
            , out RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_ProductID.Tid;
            objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_ProductID.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_ProductID_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_ProductID_Get;
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
                List<OS_PrdCenter_Prd_ProductID> lst_Prd_ProductID = new List<OS_PrdCenter_Prd_ProductID>();
                bool bGet_Prd_ProductID = (objRQ_OS_PrdCenter_Prd_ProductID.Rt_Cols_Prd_ProductID != null && objRQ_OS_PrdCenter_Prd_ProductID.Rt_Cols_Prd_ProductID.Length > 0);
                //bool bGet_Prd_ProductIDDtl = (objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl != null && objRQ_Prd_ProductID.Rt_Cols_Prd_ProductIDDtl.Length > 0);
                #endregion

                #region // OS_PrdCenter_Prd_ProductID_Get:
                mdsResult = OS_PrdCenter_Prd_ProductID_Get_New20191113(
                    objRQ_OS_PrdCenter_Prd_ProductID.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_ProductID.Ft_RecordStart // strFt_RecordStart
                    , objRQ_OS_PrdCenter_Prd_ProductID.Ft_RecordCount // strFt_RecordCount
                    , objRQ_OS_PrdCenter_Prd_ProductID.Ft_WhereClause // strFt_WhereClause
                                                                      //// Return:
                    , objRQ_OS_PrdCenter_Prd_ProductID.Rt_Cols_Prd_ProductID // strRt_Cols_Prd_ProductID
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_OS_PrdCenter_Prd_ProductID.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Prd_ProductID)
                    {
                        ////
                        DataTable dt_Prd_ProductID = mdsResult.Tables["Prd_ProductID"].Copy();
                        lst_Prd_ProductID = TUtils.DataTableCmUtils.ToListof<OS_PrdCenter_Prd_ProductID>(dt_Prd_ProductID);
                        objRT_OS_PrdCenter_Prd_ProductID.Lst_Prd_ProductID = lst_Prd_ProductID;
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

        public DataSet WAS_OS_PrdCenter_Prd_ProductID_Create(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID
            ////
            , out RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_ProductID.Tid;
            objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_ProductID.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_ProductID_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_ProductID_Create;
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

                #region // OS_PrdCenter_Prd_ProductID_Create:
                mdsResult = OS_PrdCenter_Prd_ProductID_Create_New20191113(
                    objRQ_OS_PrdCenter_Prd_ProductID.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductID // objProductID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductionDate // objProductionDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.LOTNo // objLOTNo
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.BuyDate // objBuyDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.SecretNo // objSecretNo
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyStartDate // objWarrantyStartDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyExpiredDate // objWarrantyExpiredDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyDuration // objWarrantyDuration
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo1 // objRefNo1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz1 // objRefBiz1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo2 // objRefNo2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz2 // objRefBiz2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo3 // objRefNo3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz3 // objRefBiz3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.Buyer // objBuyer
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.NetworkProductIDCode // objNetworkModelCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductIDStatus // objProductIDStatus
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField1 // objCustomField1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField2 // objCustomField2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField3 // objCustomField3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField4 // objCustomField4
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField5 // objCustomField5
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

        public DataSet WAS_OS_PrdCenter_Prd_ProductID_Update(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID
            ////
            , out RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_ProductID.Tid;
            objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_ProductID.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_ProductID_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_ProductID_Update;
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

                #region // OS_PrdCenter_Prd_ProductID_Create:
                mdsResult = OS_PrdCenter_Prd_ProductID_Update_New20191113(
                    objRQ_OS_PrdCenter_Prd_ProductID.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductID // objProductID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.SpecCode // objSpecCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductionDate // objProductionDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.LOTNo // objLOTNo
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.BuyDate // objBuyDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.SecretNo // objSecretNo
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyStartDate // objWarrantyStartDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyExpiredDate // objWarrantyExpiredDate
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.WarrantyDuration // objWarrantyDuration
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo1 // objRefNo1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz1 // objRefBiz1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo2 // objRefNo2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz2 // objRefBiz2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefNo3 // objRefNo3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.RefBiz3 // objRefBiz3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.Buyer // objBuyer
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.NetworkProductIDCode // objNetworkModelCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductIDStatus // objProductIDStatus
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField1 // objCustomField1
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField2 // objCustomField2
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField3 // objCustomField3
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField4 // objCustomField4
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.CustomField5 // objCustomField5
                                                                                  ////
                    , objRQ_OS_PrdCenter_Prd_ProductID.Ft_Cols_Upd// objFt_Cols_Upd
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

        public DataSet WAS_OS_PrdCenter_Prd_ProductID_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID
            ////
            , out RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_Prd_ProductID.Tid;
            objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_ProductID.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_Prd_ProductID_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_Prd_ProductID_Delete;
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

                #region // OS_PrdCenter_Prd_ProductID_Delete:
                mdsResult = OS_PrdCenter_Prd_ProductID_Delete_New20191113(
                    objRQ_OS_PrdCenter_Prd_ProductID.Tid // strTid
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserCode // strUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.OrgID // objOrgID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.ProductID // objProductID
                    , objRQ_OS_PrdCenter_Prd_ProductID.Prd_ProductID.SpecCode // objSpecCode
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

        #region // OS_PrdCenter_UploadFile:
        public DataSet OS_PrdCenter_UploadFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objuploadFileAsBase64String
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_UploadFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_UploadFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objuploadFileAsBase64String", objuploadFileAsBase64String
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_UploadFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        folderUpload = objfolderUpload,
                        fileName = objfileName,
                        uploadFileAsBase64String = objuploadFileAsBase64String,
                        sourceFileName = objsourceFileName,
                        destFileName = objdestFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_UploadFile(objRQ_OS_PrdCenter_File);
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
                string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
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

        public DataSet OS_PrdCenter_UploadFile_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objuploadFileAsBase64String
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_UploadFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_UploadFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objuploadFileAsBase64String", objuploadFileAsBase64String
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_UploadFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        folderUpload = objfolderUpload,
                        fileName = objfileName,
                        uploadFileAsBase64String = objuploadFileAsBase64String,
                        sourceFileName = objsourceFileName,
                        destFileName = objdestFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_UploadFile(objRQ_OS_PrdCenter_File);
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
                string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
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

        public DataSet WAS_OS_PrdCenter_UploadFile(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File
            ////
            , out RT_OS_PrdCenter_File objRT_OS_PrdCenter_File
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_File.Tid;
            objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_UploadFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_UploadFile;
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
                    mdsResult = OS_PrdCenter_UploadFile_New20191113(
                        objRQ_OS_PrdCenter_File.Tid // strTid
                        , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                        , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                        , objRQ_OS_PrdCenter_File.WAUserCode // strUserCode
                        , objRQ_OS_PrdCenter_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_OS_PrdCenter_File.folderUpload // objfolderUpload
                        , objRQ_OS_PrdCenter_File.fileName // objfileName
                        , objRQ_OS_PrdCenter_File.uploadFileAsBase64String // objuploadFileAsBase64String
                        , objRQ_OS_PrdCenter_File.sourceFileName // objsourceFileName
                        , objRQ_OS_PrdCenter_File.destFileName  // objdestFileName
                        );
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

        public DataSet OS_PrdCenter_MoveFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_MoveFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_MoveFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        folderUpload = objfolderUpload,
                        fileName = objfileName,
                        sourceFileName = objsourceFileName,
                        destFileName = objdestFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_MoveFile(objRQ_OS_PrdCenter_File);
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
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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

        public DataSet OS_PrdCenter_MoveFile_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_MoveFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_MoveFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        folderUpload = objfolderUpload,
                        fileName = objfileName,
                        sourceFileName = objsourceFileName,
                        destFileName = objdestFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_MoveFile(objRQ_OS_PrdCenter_File);
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
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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
        public DataSet WAS_OS_PrdCenter_MoveFile(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File
            ////
            , out RT_OS_PrdCenter_File objRT_OS_PrdCenter_File
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_File.Tid;
            objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_MoveFile;
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
                    mdsResult = OS_PrdCenter_MoveFile_New20191113(
                        objRQ_OS_PrdCenter_File.Tid // strTid
                        , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                        , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                        , objRQ_OS_PrdCenter_File.WAUserCode // strUserCode
                        , objRQ_OS_PrdCenter_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_OS_PrdCenter_File.folderUpload // objfolderMove
                        , objRQ_OS_PrdCenter_File.fileName // objfileName
                        , objRQ_OS_PrdCenter_File.sourceFileName // objsourceFileName
                        , objRQ_OS_PrdCenter_File.destFileName  // objdestFileName
                        );
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


        public DataSet OS_PrdCenter_DeleteFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objsourceFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_DeleteFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objsourceFileName", objsourceFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_DeleteFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        sourceFileName = objsourceFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_DeleteFile(objRQ_OS_PrdCenter_File);
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
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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

        public DataSet OS_PrdCenter_DeleteFile_New20191113(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objsourceFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_DeleteFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objsourceFileName", objsourceFileName
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
                RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
                {
                    #region // WA_OS_PrdCenter_DeleteFile:
                    RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File = new RQ_OS_PrdCenter_File()
                    {
                        Tid = strTid,
                        TokenID = strOS_MasterServer_PrdCenter_TokenID,
                        NetworkID = nNetworkID.ToString(),
                        GwUserCode = strOS_MasterServer_PrdCenter_GwUserCode,
                        GwPassword = strOS_MasterServer_PrdCenter_GwPassword,
                        WAUserCode = strOS_MasterServer_PrdCenter_WAUserCode,
                        WAUserPassword = strOS_MasterServer_PrdCenter_WAUserPassword,
                        sourceFileName = objsourceFileName,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_File = OS_PrdCenter_FileService.Instance.WA_OS_PrdCenter_DeleteFile(objRQ_OS_PrdCenter_File);
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
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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

        public DataSet WAS_OS_PrdCenter_DeleteFile(
            ref ArrayList alParamsCoupleError
            , RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File
            ////
            , out RT_OS_PrdCenter_File objRT_OS_PrdCenter_File
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_File.Tid;
            objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_DeleteFile;
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
                    mdsResult = OS_PrdCenter_DeleteFile_New20191113(
                        objRQ_OS_PrdCenter_File.Tid // strTid
                        , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                        , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                        , objRQ_OS_PrdCenter_File.WAUserCode // strUserCode
                        , objRQ_OS_PrdCenter_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_OS_PrdCenter_File.sourceFileName // objsourceFileName
                        );
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

        #region // OS_DMS_UploadFile:

        public string CheckFolderExists(string path)
        {
            var strFolder = "";
            //var _path = "";
            if (!string.IsNullOrEmpty(path))
            {
                bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
                strFolder = path.Trim();
            }
            else
            {
                var strpath = "UploadedFiles";
                bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(strpath));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strpath));
                }
                strFolder = strpath.Trim();
            }
            return strFolder;
        }
        public DataSet OS_DMS_UploadFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objuploadFileAsBase64String
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_UploadFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_UploadFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objuploadFileAsBase64String", objuploadFileAsBase64String
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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

                #region // Convert Input:
                GC.Collect(); // Mục đích: tránh đầy bộ nhớ.
                string struploadFileAsBase64String = string.Format("{0}", objuploadFileAsBase64String).Trim();
                var subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                var strFolder = objfolderUpload + @"\" + subFolder;
                var strSubFolder = CheckFolderExists(strFolder);
                byte[] fileContent = Convert.FromBase64String(struploadFileAsBase64String);
                var appPath = strSubFolder + "\\" + objfileName;
                string strResult = appPath;
                appPath = HttpContext.Current.Server.MapPath(appPath);
                System.IO.File.WriteAllBytes(appPath, fileContent);
                #endregion

                #region // Get Remark:
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

        public DataSet WAS_OS_DMS_UploadFile(
            ref ArrayList alParamsCoupleError
            , RQ_File objRQ_File
            ////
            , out RT_File objRT_File
            )
        {
            #region // Temp:
            string strTid = objRQ_File.Tid;
            objRT_File = new RT_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_DMS_UploadFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_UploadFile;
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
                    mdsResult = OS_DMS_UploadFile(
                        objRQ_File.Tid // strTid
                        , objRQ_File.GwUserCode // strGwUserCode
                        , objRQ_File.GwPassword // strGwPassword
                        , objRQ_File.WAUserCode // strUserCode
                        , objRQ_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_File.folderUpload // objfolderUpload
                        , objRQ_File.fileName // objfileName
                        , objRQ_File.uploadFileAsBase64String // objuploadFileAsBase64String
                        , objRQ_File.sourceFileName // objsourceFileName
                        , objRQ_File.destFileName  // objdestFileName
                        );
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

        public DataSet OS_DMS_MoveFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objfolderUpload
            , object objfileName
            , object objsourceFileName
            , object objdestFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_MoveFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objfolderUpload", objfolderUpload
                , "objfileName", objfileName
                , "objsourceFileName", objsourceFileName
                , "objdestFileName", objdestFileName
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //Check Access/ Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and check:
                string strsourceFileName = string.Format("{0}", objsourceFileName).Trim();
                string strdestFileName = string.Format("{0}", objdestFileName).Trim();
                ////
                #endregion

                #region // Call Func:
                var strSubFolder = CheckFolderExists(objfolderUpload.ToString());
                strsourceFileName = HttpContext.Current.Server.MapPath(strsourceFileName);
                strsourceFileName.Replace(@"\10.Common\MyWebsites\", @"\");
                objdestFileName = HttpContext.Current.Server.MapPath(strdestFileName);
                objdestFileName.ToString().Replace(@"\10.Common\MyWebsites\", @"\");
                string str = strSubFolder + "ZZZZZ" + objsourceFileName + "ZZZZZ" + objdestFileName.ToString();
                // Ensure that the target does not exist.
                if (File.Exists(strdestFileName))
                    File.Delete(strsourceFileName);

                // Move the file.

                File.Move(strsourceFileName, strdestFileName);
                #endregion

                #region // Get Remark:
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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
        public DataSet WAS_OS_DMS_MoveFile(
            ref ArrayList alParamsCoupleError
            , RQ_File objRQ_File
            ////
            , out RT_File objRT_File
            )
        {
            #region // Temp:
            string strTid = objRQ_File.Tid;
            objRT_File = new RT_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_MoveFile;
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
                    mdsResult = OS_DMS_MoveFile(
                        objRQ_File.Tid // strTid
                        , objRQ_File.GwUserCode // strGwUserCode
                        , objRQ_File.GwPassword // strGwPassword
                        , objRQ_File.WAUserCode // strUserCode
                        , objRQ_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_File.folderUpload // objfolderMove
                        , objRQ_File.fileName // objfileName
                        , objRQ_File.sourceFileName // objsourceFileName
                        , objRQ_File.destFileName  // objdestFileName
                        );
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
        public DataSet OS_DMS_DeleteFile(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , object objsourceFileName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_DeleteFile;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objsourceFileName", objsourceFileName
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

                //Check Access/ Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Call Func:
                // Ensure that the target does not exist.
                string strsourceFileName = string.Format("{0}", objsourceFileName).Trim();
                if (File.Exists(strsourceFileName))
                    File.Delete(strsourceFileName);
                #endregion

                #region // Get Remark:
                //string strResult = objRT_OS_PrdCenter_File.AppPath.ToString();
                //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
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
        public DataSet WAS_OS_DMS_DeleteFile(
            ref ArrayList alParamsCoupleError
            , RQ_File objRQ_OS_PrdCenter_File
            ////
            , out RT_File objRT_OS_PrdCenter_File
            )
        {
            #region // Temp:
            string strTid = objRQ_OS_PrdCenter_File.Tid;
            objRT_OS_PrdCenter_File = new RT_File();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_OS_PrdCenter_DeleteFile;
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

                #region // OS_DMS_DeleteFile:
                {
                    /////
                    mdsResult = OS_DMS_DeleteFile(
                        objRQ_OS_PrdCenter_File.Tid // strTid
                        , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                        , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                        , objRQ_OS_PrdCenter_File.WAUserCode // strUserCode
                        , objRQ_OS_PrdCenter_File.WAUserPassword // strUserPassword
                        , nNetworkID.ToString() // strNetworkID
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , objRQ_OS_PrdCenter_File.sourceFileName // objsourceFileName
                        );
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

        #region // Mst_Org:
        public DataSet OS_PrdCenter_Mst_Org_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objOrgParent
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "OS_PrdCenter_Mst_Org_Create";
            string strErrorCodeDefault = TError.ErridnInventory.OS_PrdCenter_Mst_Org_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objOrgID", objOrgID
                , "objOrgParent", objOrgParent
                , "objRemark", objRemark
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                string strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
                #endregion

                #region // Call Func:
                RT_OS_PrdCenter_Mst_Org objRT_OS_PrdCenter_Mst_Org = null;
                {
                    #region // Init:
                    OS_PrdCenter_Mst_Org objOS_PrdCenter_Mst_Org = new OS_PrdCenter_Mst_Org();
                    objOS_PrdCenter_Mst_Org.OrgID = objOrgID;
                    objOS_PrdCenter_Mst_Org.OrgParent = objOrgParent;
                    objOS_PrdCenter_Mst_Org.Remark = objRemark;
                    #endregion

                    #region // WA_Mst_Brand_Get:
                    RQ_OS_PrdCenter_Mst_Org objRQ_OS_PrdCenter_Mst_Org = new RQ_OS_PrdCenter_Mst_Org()
                    {
                        WAUserCode = strOS_ProductCentrer_WAUserCode,
                        WAUserPassword = strOS_ProductCentrer_WAUserPassword,
                        GwUserCode = strOS_ProductCentrer_GwUserCode,
                        GwPassword = strOS_ProductCentrer_GwPassword,
                        Tid = strTid,
                        OrgID = strOrgID,
                        Mst_Org = objOS_PrdCenter_Mst_Org,
                    };
                    ////
                    try
                    {
                        objRT_OS_PrdCenter_Mst_Org = OS_PrdCenter_Mst_OrgService.Instance.WA_OS_PrdCenter_Mst_Org_Create(objRQ_OS_PrdCenter_Mst_Org);

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
                            TError.ErridnInventory.CmSys_InvalidOutSite + "." + strErrorCodeOS
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

        #region // Prd_DynamicField:
        public DataSet WAS_Prd_DynamicField_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Prd_DynamicField objRQ_Prd_DynamicField
            ////
            , out RT_Prd_DynamicField objRT_Prd_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Prd_DynamicField.Tid;
            objRT_Prd_DynamicField = new RT_Prd_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Prd_DynamicField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Prd_DynamicField_Get;
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
                List<Prd_DynamicField> lst_Prd_DynamicField = new List<Prd_DynamicField>();
                bool bGet_Prd_DynamicField = (objRQ_Prd_DynamicField.Rt_Cols_Prd_DynamicField != null && objRQ_Prd_DynamicField.Rt_Cols_Prd_DynamicField.Length > 0);
                #endregion

                #region // MasterServerLogin:
                string strWebAPIUrl = null;
                {
                    ////
                    strWebAPIUrl = "http://14.232.244.217:12088/idocNet.Test.ProductCenter.V10.Local.WA/";


                }
                #endregion

                #region // WS_Prd_DynamicField_Get:
                mdsResult = Prd_DynamicField_Get(
                    objRQ_Prd_DynamicField.Tid // strTid
                    , objRQ_Prd_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Prd_DynamicField.GwPassword // strGwPassword
                    , objRQ_Prd_DynamicField.WAUserCode // strUserCode
                    , objRQ_Prd_DynamicField.WAUserPassword // strUserPassword
					, objRQ_Prd_DynamicField.AccessToken // strAccessToken
					, objRQ_Prd_DynamicField.NetworkID // strNetworkID
                    , objRQ_Prd_DynamicField.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Prd_DynamicField.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Prd_DynamicField.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Prd_DynamicField.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Prd_DynamicField.Rt_Cols_Prd_DynamicField // strRt_Cols_Prd_DynamicField
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Prd_DynamicField.MySummaryTable = lst_MySummaryTable[0];

                    if (bGet_Prd_DynamicField)
                    {
                        ////
                        DataTable dt_Prd_DynamicField = mdsResult.Tables["Prd_DynamicField"].Copy();
                        lst_Prd_DynamicField = TUtils.DataTableCmUtils.ToListof<Prd_DynamicField>(dt_Prd_DynamicField);
                        objRT_Prd_DynamicField.Lst_Prd_DynamicField = lst_Prd_DynamicField;
                        /////
                    }

                    // Assign:
                    CmUtils.CMyDataSet.SetRemark(ref mdsResult, strWebAPIUrl);
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
        public DataSet Prd_DynamicField_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Prd_DynamicField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Prd_DynamicField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Prd_DynamicField_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Prd_DynamicField", strRt_Cols_Prd_DynamicField
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
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				////
				Sys_User_CheckAuthorize(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
									//, strWAUserPassword // strWAUserPassword
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					, strAccessToken // strAccessToken
					, strNetworkID // strNetworkID
					, strOrgID // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Prd_DynamicField_GetX:
                DataSet dsGetData = null;
                {
                    Prd_DynamicField_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Prd_DynamicField // strRt_Cols_Prd_DynamicField
                        , out dsGetData  // dsGetData
                        );
                }
                ////
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
        private void Prd_DynamicField_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Prd_DynamicField
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Prd_DynamicField_GetX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Prd_DynamicField = (strRt_Cols_Prd_DynamicField != null && strRt_Cols_Prd_DynamicField.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@nFilterRecordStart", nFilterRecordStart
                , "@nFilterRecordEnd", nFilterRecordEnd
                });
            ////
            zzzzClauseSelect_Mst_Org_ViewAbility_Get(
                strOrgID // strOrgID
                , ref alParamsCoupleError // alParamsCoupleError
                );
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Prd_DynamicField_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.OrgID
					into #tbl_Prd_DynamicField_Filter_Draft
					from Prd_DynamicField mo --//[mylock]
						inner join #tbl_Mst_Org_ViewAbility t_mo --//[mylock]
	                            on mo.OrgID = t_mo.OrgID
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.OrgID asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Prd_DynamicField_Filter_Draft t --//[mylock]
					;

					---- #tbl_Prd_DynamicField_Filter:
					select
						t.*
					into #tbl_Prd_DynamicField_Filter
					from #tbl_Prd_DynamicField_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Prd_DynamicField -----:
					zzB_Select_Prd_DynamicField_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Prd_DynamicField_Filter_Draft;
					--drop table #tbl_Prd_DynamicField_Filter;
					"
                );
            ////
            string zzB_Select_Prd_DynamicField_zzE = "-- Nothing.";
            if (bGet_Prd_DynamicField)
            {
                #region // bGet_Prd_DynamicField:
                zzB_Select_Prd_DynamicField_zzE = CmUtils.StringUtils.Replace(@"
					---- Prd_DynamicField:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Prd_DynamicField_Filter t --//[mylock]
						inner join Prd_DynamicField mo --//[mylock]
							on t.OrgID = mo.OrgID
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
                        , "Prd_DynamicField" // strTableNameDB
                        , "Prd_DynamicField." // strPrefixStd
                        , "mo." // strPrefixAlias
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
                , "zzB_Select_Prd_DynamicField_zzE", zzB_Select_Prd_DynamicField_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Prd_DynamicField)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Prd_DynamicField";
            }
            #endregion
        }

        public void Prd_DynamicField_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Prd_DynamicField_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Prd_DynamicField_SaveAllX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objFlagIsDelete",objFlagIsDelete
				////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
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
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            ////
            #endregion

            #region // Refine and Check Input Prd_DynamicField:
            ////

            ////
            DataTable dtInput_Prd_DynamicField = null;
            {
                ////
                string strTableCheck = "Prd_DynamicField";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Prd_DynamicField_SaveX_Input_DynamicFieldTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Prd_DynamicField = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Prd_DynamicField.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Prd_DynamicField_SaveX_Input_DynamicFieldTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Prd_DynamicField // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "", "Detail" // arrstrCouple
                    , "StdFlag", "FlagActive" // arrstrCouple
                    , "StdDate", "LogLUDTimeUTC" // arrstrCouple
                    , "", "LogLUBy" // arrstrCouple
                    );
                ////

                ////
            }
            #endregion

            #region //// SaveTemp Invoice_Invoice For Check:
            //if (!bIsDelete)
            {
                // Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db // db
                    , "#input_Prd_DynamicField" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Detail", TConst.BizMix.MyText_DBColType
                            , "FlagActive", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Prd_DynamicField // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Prd_DynamicField:
							    delete t
							    from Prd_DynamicField t --//[mylock]
								    inner join #input_Prd_DynamicField f --//[mylock]
									    on t.OrgID = f.OrgID
							    where (1=1)
							    ;
						");
                    _cf.db.ExecQuery(
                        strSqlDelete
                        );
                }

                //// Insert All:
                if (!bIsDelete)
                {
                    #region // Insert:
                    {

                        ////
                        string zzzzClauseInsert_Prd_DynamicField_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Prd_DynamicField
                                (
                                    OrgID
                                    , NetworkID
                                    , Detail
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.OrgID
                                    , t.NetworkID
                                    , t.Detail
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Prd_DynamicField t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Prd_DynamicField_zSave
							"
                            , "zzzzClauseInsert_Prd_DynamicField_zSave", zzzzClauseInsert_Prd_DynamicField_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion

                    #region //// Clear For Debug:
                    if (!bIsDelete)
                    {
                        ////
                        string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						        ---- Clear for Debug:
						        drop table #input_Prd_DynamicField;
					        ");

                        _cf.db.ExecQuery(
                            strSqlClearForDebug
                            );
                        ////
                    }
                    else
                    {
                        ////
                        string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						        ---- Clear for Debug:
						        drop table #input_Prd_DynamicField;
					        ");

                        _cf.db.ExecQuery(
                            strSqlClearForDebug
                            );
                        ////

                    }
                    #endregion
                }
            }
            #endregion
        }

        public DataSet Prd_DynamicField_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Invoice_Invoice_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.Prd_DynamicField_Save;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    /////
                    , "objFlagIsDelete", objFlagIsDelete
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
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Invoice_Invoice_SaveX:
                //DataSet dsGetData = null;
                {
                    Prd_DynamicField_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                                          ////
                        , dsData // dsData
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
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
                #endregion
            }
        }

        public DataSet WAS_Prd_DynamicField_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Prd_DynamicField objRQ_Prd_DynamicField
            ////
            , out RT_Prd_DynamicField objRT_Prd_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Prd_DynamicField.Tid;
            objRT_Prd_DynamicField = new RT_Prd_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Prd_DynamicField_Save_Root";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Prd_DynamicField_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Prd_DynamicField.FlagIsDelete
                , "Lst_Prd_DynamicField", TJson.JsonConvert.SerializeObject(objRQ_Prd_DynamicField.Lst_Prd_DynamicField)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Prd_DynamicField> lst_Prd_DynamicField = new List<Prd_DynamicField>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Prd_DynamicField = TUtils.DataTableCmUtils.ToDataTable<Prd_DynamicField>(objRQ_Prd_DynamicField.Lst_Prd_DynamicField, "Prd_DynamicField");
                    dsData.Tables.Add(dt_Prd_DynamicField);
                    ////
                }
                #endregion

                #region // WS_Prd_DynamicField_Create: 
                // Prd_DynamicField_Save_Root_New20190704
                mdsResult = Prd_DynamicField_Save(
                    objRQ_Prd_DynamicField.Tid // strTid
                    , objRQ_Prd_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Prd_DynamicField.GwPassword // strGwPassword
                    , objRQ_Prd_DynamicField.WAUserCode // strUserCode
                    , objRQ_Prd_DynamicField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Prd_DynamicField.FlagIsDelete // objFlagIsDelete
                                                   ////
                    , dsData // dsData
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
